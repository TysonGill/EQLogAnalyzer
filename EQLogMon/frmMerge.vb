Imports System.IO
Imports Utility

Public Class frmMerge

    Friend MergeFile As String
    Friend dtMerge As New DataTable

    Private Sub frmMerge_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            ' Assign cursors
            lblAnalyze.Cursor = crPress

            ' Set tooltips
            Dim tt1 As New ToolTip()
            tt1.AutomaticDelay = 100
            tt1.AutoPopDelay = 4000
            tt1.SetToolTip(lblTimeZone, "Enter +/- hours to add to merged times")
            Dim tt2 As New ToolTip()
            tt2.AutomaticDelay = 100
            tt2.AutoPopDelay = 4000
            tt2.SetToolTip(lblLagAllowance, "Enter the lag allowance in seconds (applied both before and after)")

            ' Display the file to be merged
            lblBaseFile.Text = Path.GetFileName(LogFile)
            Dim info As New FileInfo(LogFile)
            LblBaseCreatedDate.Text = info.CreationTime.ToString
            lblBaseUpdateDate.Text = info.LastWriteTime.ToString
            lblMergeFile.Text = Path.GetFileName(MergeFile)
            info = New FileInfo(MergeFile)
            lblCreatedDate.Text = info.CreationTime.ToString
            lblUpdatedDate.Text = info.LastWriteTime.ToString
            pg.EnableRefreshButton = False
            ssStatus.ForeColor = Color.Red
            ssStatus.Text = "Click Analyze to preview merge..."

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Bring in the new file, purging any duplicate records and display the merge candidate list
    Private Sub DoMerge()
        Try
            Cursor = Cursors.WaitCursor

            Dim s As String
            Dim dps As New DamageParseStruct
            Dim MaxAllow As Integer = CInt(txtLagAllowance.Text)
            Dim ZoneAdj As Integer = CInt(txtHourAdjustment.Text)
            Dim BlockStart As DateTime
            Dim BlockEnd As DateTime
            Dim BlockStartPos As Integer
            Dim BlockEndPos As Integer
            Dim FilePointer As Integer = 0
            Dim InsertCount As Integer = 0
            Dim nParsed As Integer = 0
            Dim StartParsing As DateTime = Now
            Dim MainOwner As String = FileOwner
            FileOwner = GetOwner(Path.GetFileName(MergeFile))
            dtMerge = dtParse.Clone
            Dim reader As StreamReader
            reader = My.Computer.FileSystem.OpenTextFileReader(MergeFile)
            pg.GridClear()

            Do
                ' Get the next line
                s = reader.ReadLine
                If s Is Nothing Then Exit Do

                ' Update status
                nParsed += 1
                If nParsed Mod 1000 = 0 Then ssStatus.Text = "Checked " + nParsed.ToString("N0") + "..."
                Application.DoEvents()

                ' Parse for deaths
                'If ParseDeathLine(s) Then Continue Do ' should not do until join, and check for dups

                ' Parse for buffs
                'If ParseBuffLine(s) Then Continue Do ' should not do until join, and check for dups

                ' Parse for an action line
                dps = ParseDamageLine(s)
                If Not dps.IsValid Then Continue Do

                ' Adjust time for time zone if necessary
                If ZoneAdj <> 0 Then dps.StartTime = DateAdd(DateInterval.Hour, ZoneAdj, dps.StartTime)

                ' Compute the time block in which to search for this message
                BlockStart = DateAdd(DateInterval.Second, -MaxAllow, dps.StartTime)
                If BlockStart < dtParse.Rows(0)("StartTime") Then Continue Do
                BlockEnd = DateAdd(DateInterval.Second, MaxAllow, dps.StartTime)

                ' Check if merge message is before or after the time range of the first

                ' Find the parse indices for the time block
                BlockStartPos = FindParseAtDate(BlockStart, FilePointer)
                If BlockStartPos = -1 Then Continue Do
                BlockEndPos = FindParseAtDate(BlockEnd, BlockStartPos)
                If BlockEndPos = -1 Then BlockEndPos = dtParse.Rows.Count - 1

                ' Update the table pointer for the next loop
                FilePointer = BlockStartPos

                ' See if message matches within that date range
                For i As Integer = BlockStartPos To BlockEndPos
                    If dtParse.Rows(i)("Source") = dps.Source Then Continue Do
                Next

                ' Add row to merge table
                InsertCount += 1
                dtMerge.Rows.Add(InsertCount, dps.StartTime, dps.Player, dps.Target, dps.DamageType, dps.Damage, dps.Source)

            Loop
            reader.Close()

            ' Display the preview
            pg.GridShow(dtMerge)
            FileOwner = MainOwner

            ' Log the parse to the database
            ParseTime = DateDiff(DateInterval.Second, StartParsing, Now)
            TrackMerge()

            ' Update the status bar
            ssStatus.ForeColor = SystemColors.ControlText
            ssStatus.Text = "Click Join to accept merged records"

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Add the merge candidates to the base parse table
    Private Sub DoJoin()
        Try
            Cursor = Cursors.WaitCursor

            ssStatus.ForeColor = Color.Red
            ssStatus.Text = "Joining..."
            Application.DoEvents()

            ' Add a merge column if needed
            MergeNum += 1
            If Not dtParse.Columns.Contains("Merge") Then
                dtParse.Columns.Add("Merge", GetType(Integer))
                For i As Integer = 0 To dtParse.Rows.Count - 1
                    dtParse.Rows(i)("Merge") = 0
                Next
            End If
            If Not dtMerge.Columns.Contains("Merge") Then dtMerge.Columns.Add("Merge", GetType(String))

            ' Append merged table
            For i As Integer = 0 To dtMerge.Rows.Count - 1
                dtMerge.Rows(i)("Merge") = MergeNum
                dtParse.ImportRow(dtMerge.Rows(i))
            Next
            dtParse = db.SortDataTable(dtParse, "StartTime")

            ' Reset row numbers
            For i As Integer = 0 To dtParse.Rows.Count - 1
                dtParse.Rows(i)("RowNum") = i
            Next

            ' Close the merge window
            Me.Close()

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TrackMerge()
        Try
            Cursor = Cursors.WaitCursor

            Dim info As New FileInfo(MergeFile)
            Dim FileSize As Long = info.Length
            Dim FileDate As DateTime = info.CreationTime
            Dim sql As String
            sql = "INSERT INTO EQLog ([Owner],  [FileName], [FileDate], [FileSize], [ParseDate], [ParseMode], [ParseCount], [ParseSecs]) VALUES (" + fmt.q(FileOwner) + ", " + fmt.q(lblMergeFile.Text) + ", " + fmt.q(FileDate) + ", " + fmt.q(FileSize) + ", " + fmt.q(Now) + ", 'Merge', " + fmt.q(dtMerge.Rows.Count) + ", " + fmt.q(ParseTime) + ")"
            dq.Execute(sql)

        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#Region "Event Handlers"

    Private Sub txtHourAdjustment_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtHourAdjustment.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) AndAlso Not Char.IsNumber(e.KeyChar) AndAlso e.KeyChar <> "-" Then e.Handled = True
    End Sub

    Private Sub txtLagAllowance_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLagAllowance.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) AndAlso Not Char.IsNumber(e.KeyChar) Then e.Handled = True
    End Sub

    Private Sub btnMerge_Click(sender As Object, e As EventArgs) Handles lblAnalyze.Click, btnAnalyze.Click
        DoMerge()
        pg.ButtonSet(1, "Join", "Move the candidate records into the base file and close merge window")
    End Sub

    Private Sub pg_UserClickedButton(ButtonTag As String) Handles pg.UserClickedButton
        DoJoin
    End Sub

#End Region

End Class