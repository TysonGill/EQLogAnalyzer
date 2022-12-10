Imports ProGrid.ProGrid
Imports Utility

Public Class frmServer

    Dim dtDamage As DataTable
    Dim dtEnc As DataTable

    Private Sub frmServer_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            ' Restore window to last size and position
            If My.Settings.LastServerPos.X > 0 Then
                If My.Settings.LastServerPos.X < My.Computer.Screen.WorkingArea.Width AndAlso My.Settings.LastServerPos.Y < My.Computer.Screen.WorkingArea.Height Then
                    Me.Location = My.Settings.LastServerPos
                    Me.Size = My.Settings.LastServerSize
                End If
            Else
                Me.Location = New Point(My.Computer.Screen.WorkingArea.Width / 2 - Me.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 - Me.Height / 2)
            End If
            chkPets.Checked = My.Settings.LastMergePets

            ' Configure the grid
            pg.EnableRefreshButton = False
            pg.StyleSet("Damage (1000's)", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right, FormatString:="N0"))
            pg.StyleSet("% Damage", New ProGridStyle(ProGridStyleTypes.Percent, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("NumHits", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("MaxDam", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("AvgDam", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right))

            ' Assign cursors
            lblSearch.Cursor = crPress

            ' Display the window
            Me.BringToFront()
            Me.Show()
            Application.DoEvents()

            ' Fetch the encounter definitions
            LoadEncounters()

            ' Initialize the treeview
            LoadTree()

            ssStatus.Text = "Ready"
            ssStatus.ForeColor = SystemColors.ControlText

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadEncounters()
        Try
            Cursor = Cursors.WaitCursor

            ssStatus.Text = "Loading encounters..."
            ssStatus.ForeColor = Color.Red
            Application.DoEvents()

            Dim sql As String
            sql = "Exec GetEncounters"
            dtEnc = dq.GetTable("Encounters", sql)
            For Each row As DataRow In dtEnc.Rows
                row("Start Date") = row("Start Date").tolocaltime
                row("End Date") = row("End Date").tolocaltime
            Next

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadTree()
        Try
            Cursor = Cursors.WaitCursor

            tv.Nodes.Clear()
            Dim node As TreeNode
            node = tv.Nodes.Add("Time Search")
            node.Tag = "Time Search"
            For Each row As DataRow In dtEnc.Rows
                node = tv.Nodes.Add(row("Target") + "+")
                node.Tag = row
                node.ToolTipText = row("Start Date") + " - " + row("End Date")
                node.Nodes.Add("Pending")
            Next
            tv.SelectedNode = tv.Nodes(0)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub tv_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tv.NodeMouseClick
        DoNodeClick(e.Node)
    End Sub

    Private Sub DoNodeClick(ClickedNode As TreeNode)
        Try
            Cursor = Cursors.WaitCursor

            If ClickedNode Is Nothing Then Exit Sub

            Dim First As DateTime
            Dim Last As DateTime

            If ClickedNode.Text = "Time Search" Then
                lblSearch.Visible = True
                First = dtEnc.Rows(0)("Start Date")
                Last = dtEnc.Rows(dtEnc.Rows.Count - 1)("End Date")
                txtSearch.Text = First.ToString + " - " + Last.ToString
                pg.GridClear()
                Exit Sub
            Else
                lblSearch.Visible = False
            End If

            Dim sql As String
            Dim row As DataRow
            Dim target As String = ""
            If ClickedNode.Level = 0 Then
                row = DirectCast(ClickedNode.Tag, DataRow)
            Else
                row = DirectCast(ClickedNode.Parent.Tag, DataRow)
                target = ClickedNode.Text
            End If

            ' Load DPS
            First = row("Start Date").ToUniversalTime
            Last = row("End Date").ToUniversalTime
            sql = "EXEC GetDPS " + fmt.q(First) + ", " + fmt.q(Last) + ", " + fmt.q(target) + IIf(chkPets.Checked, ",1", ",0")
            dtDamage = dq.GetTable("Damage Report", sql)
            txtSearch.Text = row("Start Date").ToString + " - " + row("End Date").ToString
            pg.ButtonSet(1, "Clip", "Send a text summary to clipboard", "NodeClip")
            pg.GridShow(dtDamage, ProGrid.ProGrid.ProGridOrienations.Horizontal)

            ' Display loggers
            sql = "SELECT DISTINCT([FileOwner]) FROM EQLogDamage WHERE LogID >= " + fmt.q(row("StartID")) + " AND LogID <= " + fmt.q(row("EndID")) + " ORDER BY [FileOwner]"
            Dim dtLoggers As DataTable = dq.GetTable("Loggers", sql)
            ssLoggers.Text = dtLoggers.Rows.Count.ToString + " merged logs"
            ssLoggers.ToolTipText = db.GetDistinctString(dtLoggers, "FileOwner", False)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Dynamically load target lists under each parent node
    Private Sub tv_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles tv.BeforeExpand
        Try
            Cursor = Cursors.WaitCursor

            If e.Node.Nodes(0).Text <> "Pending" Then Exit Sub

            Dim row As DataRow
            Dim sql As String
            row = DirectCast(e.Node.Tag, DataRow)
            sql = "SELECT DISTINCT([Target]) FROM EQLogDamage WHERE LogID >= " + fmt.q(row("StartID")) + " AND LogID <= " + fmt.q(row("EndID")) + " ORDER BY [Target]"
            Dim dt As DataTable = dq.GetTable("Targets", sql)
            e.Node.Nodes(0).Remove()
            For Each r As DataRow In dt.Rows
                e.Node.Nodes.Add(r("Target"))
            Next

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Load all records during the selected time period
    Private Sub DoTimePeriodLoad()
        Try
            Cursor = Cursors.WaitCursor

            If txtSearch.Text.Length = 0 Then Exit Sub

            ' Get the time range to report
            Dim Phrases As String()
            Dim FromTime As DateTime
            Dim ToTime As DateTime
            Phrases = txtSearch.Text.Split("-")
            If Phrases.Count <> 2 OrElse Not IsDate(Phrases(0)) OrElse Not IsDate(Phrases(1)) Then
                MessageBox.Show("Your date range does not seem to be in the correct format.", "EQ Log Analyzer", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            FromTime = Convert.ToDateTime(Phrases(0)).ToUniversalTime
            ToTime = Convert.ToDateTime(Phrases(1)).ToUniversalTime

            ' Get the parse
            Dim sql As String
            sql = "EXEC GetDPS " + fmt.q(FromTime) + ", " + fmt.q(ToTime) + ", " + fmt.q("") + IIf(chkPets.Checked, ",1", ",0")
            dtDamage = dq.GetTable("Damage Report", sql)
            pg.ButtonSet(1, "Clip", "Send a text summary to clipboard", "SearchClip",)
            pg.GridShow(dtDamage, ProGrid.ProGrid.ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub pg_UserClickedButton(ButtonTag As String) Handles pg.UserClickedButton
        Select Case ButtonTag
            Case "NodeClip"
                ExportToClipboard(tv.SelectedNode.Text, dtDamage)
            Case "SearchClip"
                ExportToClipboard(txtSearch.Text, dtDamage)
        End Select
    End Sub

    Private Sub frmServer_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        My.Settings.LastServerPos = Me.Location
        My.Settings.LastServerSize = Me.Size
        My.Settings.LastMergePets = chkPets.Checked
        My.Settings.Save()
    End Sub

    Private Sub lblSearch_Click(sender As Object, e As EventArgs) Handles lblSearch.Click
        DoTimePeriodLoad
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter AndAlso lblSearch.Visible Then
            DoTimePeriodLoad()
        End If
    End Sub

    Private Sub chkPets_CheckedChanged(sender As Object, e As EventArgs) Handles chkPets.CheckedChanged
        DoNodeClick(tv.SelectedNode)
    End Sub

End Class