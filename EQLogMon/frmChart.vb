Imports Utility

Public Class frmChart

    Friend bBuff As Bitmap = Nothing
    Friend gBuff As Graphics = Nothing

    Friend bTL As Bitmap = Nothing
    Friend gTL As Graphics = Nothing

    Friend bBoss As Bitmap = Nothing
    Friend gBoss As Graphics = Nothing

    Friend bDPS As Bitmap = Nothing
    Friend gDPS As Graphics = Nothing

    ' Assign Colors
    Dim cRaidBuff As Color = Color.Red
    Dim cGroupBuff As Color = Color.DarkGreen
    Dim cPersonalBuff As Color = Color.Sienna

    Dim TimeLeft As DateTime
    Dim TimeRight As DateTime
    Dim BufferLeft As Integer = 20
    Dim BufferRight As Integer = 60
    Dim SecsStart As Long
    Dim SecsFull As Long
    Dim SecsShown As Long
    Dim PlayerSelected As String = ""
    Dim PlayerMaxDPS As Long
    Dim PlayerTotalDamage As Long
    Dim EncMaxDPS As Long
    Dim EncTotalDamage As Long
    Dim dtDPS As New DataTable
    Dim dtLevels() As DataTable

    Dim MouseIsDown As Boolean = False
    Dim XDown As Integer

    Dim CanRedraw As Boolean = False

    Private Sub frmGraphics_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            ' Assign cursors
            crLeftRight = New Cursor(My.Resources.LeftRight.GetHicon)
            lblCrop.Cursor = crPress

            ' Restore window to last size and position
            If My.Settings.ChartLastPos.X > 0 Then
                If My.Settings.ChartLastPos.X < My.Computer.Screen.WorkingArea.Width AndAlso My.Settings.ChartLastPos.Y < My.Computer.Screen.WorkingArea.Height Then
                    Me.Location = My.Settings.ChartLastPos
                    Me.Size = My.Settings.ChartLastSize
                End If
            Else
                Me.Location = New Point(My.Computer.Screen.WorkingArea.Width / 2 - Me.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 - Me.Height / 2)
            End If
            chkRaid.Checked = My.Settings.ChartLastRaid
            chkGroup.Checked = My.Settings.ChartLastGroup
            chkPersonal.Checked = My.Settings.ChartLastPersonal
            numBossUptime.Value = My.Settings.LastChartUptime
            numDPS.Value = My.Settings.ChartLastAgg

            ' Set tooltips
            Dim tt1 As New ToolTip()
            tt1.AutomaticDelay = 100
            tt1.AutoPopDelay = 4000
            tt1.SetToolTip(lblCrop, "Save currently displayed region to a new log file")

            ' Assign cursors
            lblCrop.Cursor = crPress

            ' Load combo box
            cboPlayers.Items.Add("All Players")
            For Each sname As String In lstPlayers
                cboPlayers.Items.Add(sname)
            Next
            cboPlayers.SelectedIndex = 0
            PlayerSelected = "All Players"

            ' Initialize display
            dtDPS.Columns.Add("Seconds", GetType(Integer))
            dtDPS.Columns.Add("Damage", GetType(Long))
            TimeLeft = dtParse.Rows(0)("StartTime")
            TimeRight = dtParse.Rows(dtParse.Rows.Count - 1)("StartTime")
            SecsStart = 0
            SecsFull = DateDiff(DateInterval.Second, TimeLeft, TimeRight)
            SecsShown = SecsFull

            ' Show initial display
            GetEncDamage()
            GetDPS()
            RedrawAll()

            CanRedraw = True
            picDPS.AllowDrop = True

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DrawBuffs()
        Try
            Cursor = Cursors.WaitCursor

            If bBuff Is Nothing Then bBuff = New Bitmap(picBuffs.ClientSize.Width, picBuffs.ClientSize.Height)
            If gBuff Is Nothing Then gBuff = Graphics.FromImage(bBuff)

            ' Configure tables to assign buff rows
            Dim nlevels As Integer = (picBuffs.Height / 24) + 24
            ReDim dtLevels(nlevels)
            dtLevels(0) = New DataTable
            dtLevels(0).Columns.Add("Buff", GetType(String))
            dtLevels(0).Columns.Add("SecsStart", GetType(Integer))
            dtLevels(0).Columns.Add("SecsEnd", GetType(Integer))
            dtLevels(0).Columns.Add("Scope", GetType(String))
            dtLevels(0).Columns.Add("Player", GetType(String))
            dtLevels(0).Columns.Add("x1", GetType(Integer))
            dtLevels(0).Columns.Add("y1", GetType(Integer))
            dtLevels(0).Columns.Add("x2", GetType(Integer))
            dtLevels(0).Columns.Add("y2", GetType(Integer))
            For i As Integer = 1 To nlevels
                dtLevels(i) = dtLevels(0).Clone
            Next

            ' Assign buff locations
            Dim BornSecs As Integer
            Dim DiedSecs As Integer
            Dim j As Integer
            Dim level As Integer
            For i As Integer = 0 To dtBuffs.Rows.Count - 1

                ' Filter buffs to display
                Select Case dtBuffs.Rows(i)("Scope")
                    Case "Raid"
                        If Not chkRaid.Checked Then Continue For
                    Case "Group"
                        If Not chkGroup.Checked Then Continue For
                    Case "Personal"
                        If Not chkPersonal.Checked Then Continue For
                End Select

                ' Assign buffs to levels
                BornSecs = DateDiff(DateInterval.Second, dtParse.Rows(0)("StartTime"), dtBuffs.Rows(i)("StartTime"))
                DiedSecs = BornSecs + dtBuffs.Rows(i)("Duration")
                For level = 1 To nlevels
                    For j = 0 To dtLevels(level).Rows.Count - 1
                        If IsOverlap(BornSecs, DiedSecs, dtLevels(level).Rows(j)("SecsStart"), dtLevels(level).Rows(j)("SecsEnd")) Then Exit For
                    Next
                    If j = dtLevels(level).Rows.Count Then
                        dtLevels(level).Rows.Add(dtBuffs.Rows(i)("Buff"), BornSecs, DiedSecs, dtBuffs.Rows(i)("Scope"), dtBuffs.Rows(i)("Player"))
                        GoTo NextBuff
                    End If
                Next
NextBuff:
            Next

            ' Clear the image
            gBuff.FillRectangle(New SolidBrush(Color.White), 0, 0, picBuffs.Width, picBuffs.Height)

            ' Draw buff bars
            Dim y As Integer
            Dim BuffColor As Color
            Dim LabelFont As New Font("Microsoft Sans Serif", 11.25, FontStyle.Regular, GraphicsUnit.Pixel)
            For i As Integer = 1 To nlevels
                For j = 0 To dtLevels(i).Rows.Count - 1

                    ' Get color
                    Select Case dtLevels(i).Rows(j)("Scope")
                        Case "Raid"
                            BuffColor = cRaidBuff
                        Case "Group"
                            BuffColor = cGroupBuff
                        Case "Personal"
                            BuffColor = cPersonalBuff
                        Case Else
                            Stop
                    End Select

                    ' Draw buff uptime line
                    Dim x1 As Integer = SecsToPixel(picBosses, dtLevels(i).Rows(j)("SecsStart"))
                    Dim x2 As Integer = SecsToPixel(picBosses, dtLevels(i).Rows(j)("SecsEnd"))
                    y = picBuffs.Height - ((i - 1) * 24)
                    gBuff.DrawLine(New Pen(BuffColor), x1, y, x2, y)

                    ' Label line
                    Dim BuffLabel As String = dtLevels(i).Rows(j)("Buff")
                    Dim txtwid As Integer = gBuff.MeasureString(BuffLabel, LabelFont).Width
                    If txtwid > (x2 - x1) Then x2 = x1 + txtwid
                    Dim txthgt As Integer = gBuff.MeasureString(BuffLabel, LabelFont).Height

                    gBuff.DrawString(BuffLabel, LabelFont, New SolidBrush(BuffColor), New Point(x1, y + 2))

                    ' Store label region
                    dtLevels(i).Rows(j)("x1") = x1
                    dtLevels(i).Rows(j)("x2") = x2
                    dtLevels(i).Rows(j)("y1") = y
                    dtLevels(i).Rows(j)("y2") = y + txthgt + 2

                Next
            Next

            picBuffs.Image = bBuff

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function GetBuffPlayer(x, y) As String
        Dim row As DataRow
        For i As Integer = 0 To dtLevels.Count - 1
            For j As Integer = 0 To dtLevels(i).Rows.Count - 1
                row = dtLevels(i).Rows(j)
                If IsWithin(x, y, row("x1"), row("y1"), row("x2"), row("y2")) Then Return row("Player")
            Next
        Next
        Return ""
    End Function

    Private Function IsWithin(x As Integer, y As Integer, x1 As Integer, y1 As Integer, x2 As Integer, y2 As Integer) As Boolean
        Return (x >= x1) AndAlso (x <= x2) AndAlso (y >= y1) AndAlso (y <= y2)
    End Function

    Private Sub DrawTimeline(SecsLeft As Long)
        Try
            Cursor = Cursors.WaitCursor

            If bTL Is Nothing Then bTL = New Bitmap(picTL.ClientSize.Width, picTL.ClientSize.Height)
            If gTL Is Nothing Then gTL = Graphics.FromImage(bTL)

            Dim LinePen As New Pen(Color.Black)
            Dim LabelFont As New Font("Microsoft Sans Serif", 11.25, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim LabelBrush As New SolidBrush(Color.Black)

            Dim DrawWidth As Integer = picTL.Width - BufferLeft - BufferRight
            Dim nTicks As Integer = 10
            Dim TickHeight As Integer = 10
            Dim TickStep As Integer = DrawWidth / (nTicks + 1)
            Dim xTL As Integer
            Dim yTL As Integer = 0
            Dim SecsPerTick As Integer = SecsShown / (nTicks + 1)
            Dim TickLabel As String
            Dim txtwid As Integer
            Dim txthgt As Integer

            ' Clear the image
            gTL.FillRectangle(New SolidBrush(Color.White), 0, 0, picTL.Width, picTL.Height)

            ' Draw timeline main line
            gTL.DrawLine(LinePen, BufferLeft, yTL, picTL.Width - BufferRight, yTL)

            ' Draw ticks
            For i As Integer = 1 To nTicks
                xTL = TickStep * i
                TickLabel = FormatTick(SecsLeft + (SecsPerTick * i))
                txtwid = gTL.MeasureString(TickLabel, LabelFont).Width
                txthgt = gTL.MeasureString(TickLabel, LabelFont).Height
                gTL.DrawLine(LinePen, BufferLeft + xTL, yTL, BufferLeft + xTL, yTL + TickHeight)
                gTL.DrawString(TickLabel, LabelFont, LabelBrush, New Point(BufferLeft + xTL - txtwid / 2, yTL + txthgt))
            Next

            picTL.Image = bTL

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DrawBosses()
        Try
            Cursor = Cursors.WaitCursor

            If bBoss Is Nothing Then bBoss = New Bitmap(picBosses.ClientSize.Width, picBosses.ClientSize.Height)
            If gBoss Is Nothing Then gBoss = Graphics.FromImage(bBoss)

            ' Configure tables to assign boss rows
            Dim nlevels As Integer = 5
            Dim dtLevels(nlevels) As DataTable
            dtLevels(0) = New DataTable
            dtLevels(0).Columns.Add("Boss", GetType(String))
            dtLevels(0).Columns.Add("SecsStart", GetType(Integer))
            dtLevels(0).Columns.Add("SecsEnd", GetType(Integer))
            For i As Integer = 1 To nlevels
                dtLevels(i) = dtLevels(0).Clone
            Next

            ' Assign boss locations
            Dim BornSecs As Integer
            Dim DiedSecs As Integer
            Dim j As Integer
            Dim level As Integer
            For i As Integer = 0 To dtBosses.Rows.Count - 1

                BornSecs = DateDiff(DateInterval.Second, dtParse.Rows(i)("StartTime"), dtBosses.Rows(i)("Born"))
                DiedSecs = DateDiff(DateInterval.Second, dtParse.Rows(i)("StartTime"), dtBosses.Rows(i)("Died"))
                If DiedSecs - BornSecs < numBossUptime.Value Then Continue For

                For level = 1 To nlevels
                    For j = 0 To dtLevels(level).Rows.Count - 1
                        If IsOverlap(BornSecs, DiedSecs, dtLevels(level).Rows(j)("SecsStart"), dtLevels(level).Rows(j)("SecsEnd")) Then
                            Exit For
                        End If
                    Next
                    If j = dtLevels(level).Rows.Count Then
                        dtLevels(level).Rows.Add(dtBosses.Rows(i)("Boss"), BornSecs, DiedSecs)
                        Exit For
                    End If
                Next

            Next

            Dim LinePen As New Pen(Color.DarkBlue)
            Dim LabelFont As New Font("Microsoft Sans Serif", 11.25, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim LabelBrush As New SolidBrush(Color.DarkBlue)

            ' Clear the image
            gBoss.FillRectangle(New SolidBrush(Color.White), 0, 0, picBosses.Width, picBosses.Height)

            ' Draw boss bars
            Dim y As Integer
            For i As Integer = 1 To nlevels
                For j = 1 To dtLevels(i).Rows.Count - 1

                    ' Draw boss uptime line
                    Dim x1 As Integer = SecsToPixel(picBosses, dtLevels(i).Rows(j)("SecsStart"))
                    Dim x2 As Integer = SecsToPixel(picBosses, dtLevels(i).Rows(j)("SecsEnd"))
                    y = (i - 1) * 24
                    gBoss.DrawLine(LinePen, x1, y, x2, y)

                    ' Label line
                    gBoss.DrawString(dtLevels(i).Rows(j)("Boss"), LabelFont, LabelBrush, New Point(x1, y + 2))
                Next
            Next

            picBosses.Image = bBoss

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DrawDPS(SecsLeft As Long, SecsWidth As Long)
        Try
            Cursor = Cursors.WaitCursor

            If bDPS Is Nothing Then bDPS = New Bitmap(picDPS.ClientSize.Width, picDPS.ClientSize.Height)
            If gDPS Is Nothing Then gDPS = Graphics.FromImage(bDPS)

            Dim LinePen As New Pen(Color.DarkRed)
            Dim LabelFont As New Font("Microsoft Sans Serif", 11.25, FontStyle.Regular, GraphicsUnit.Pixel)
            Dim LabelBrush As New SolidBrush(Color.DarkRed)

            ' Clear the image
            gDPS.FillRectangle(New SolidBrush(Color.White), 0, 0, picDPS.Width, picDPS.Height)

            ' Draw dps spectrum
            For i As Integer = 0 To dtDPS.Rows.Count - 1
                If dtDPS(i)("Damage") > 0 Then
                    Dim x As Integer = SecsToPixel(picDPS, dtDPS(i)("Seconds"))
                    Dim y1 As Integer = picDPS.Height
                    Dim y2 As Integer = picDPS.Height - ((dtDPS(i)("Damage") / PlayerMaxDPS) * picDPS.Height)
                    gDPS.DrawLine(LinePen, x, y1, x, y2)
                End If
            Next

            ' Draw the heading
            gDPS.DrawLine(New Pen(Color.Black), 1, 0, 100, 0)
            Dim txt1 As String = "All Players"
            Dim txt2 As String
            If PlayerSelected <> "" Then
                txt1 = PlayerSelected
                txt2 = FormatDamage(PlayerMaxDPS) + " peak in " + FormatDamage(PlayerTotalDamage) + " total"
            Else
                txt2 = FormatDamage(EncMaxDPS) + " peak in " + FormatDamage(EncTotalDamage) + " total"
            End If
            gDPS.DrawString(txt1, LabelFont, New SolidBrush(Color.Black), New Point(1, 4))
            gDPS.DrawString(txt2, LabelFont, New SolidBrush(Color.Black), New Point(1, 18))

            picDPS.Image = bDPS
            picDPS.Refresh()

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub GetDPS()

        dtDPS.Rows.Clear()
        Dim LastTime As DateTime = dtParse.Rows(0)("StartTime")
        Dim TotSecs As Long = 0
        Dim CurSecs As Long = 0
        Dim CumDamage As Long = 0
        PlayerMaxDPS = 0
        PlayerTotalDamage = 0
        For i As Integer = 1 To dtParse.Rows.Count - 1

            If PlayerSelected <> "All Players" AndAlso dtParse.Rows(i)("Player") <> PlayerSelected Then Continue For

            CurSecs = DateDiff(DateInterval.Second, LastTime, dtParse.Rows(i)("StartTime"))
            If CurSecs <= numDPS.Value AndAlso i < dtParse.Rows.Count - 1 Then
                CumDamage += dtParse.Rows(i)("Damage")
            Else
                TotSecs = DateDiff(DateInterval.Second, dtParse.Rows(0)("StartTime"), dtParse.Rows(i - 1)("StartTime"))
                dtDPS.Rows.Add(TotSecs, CumDamage)
                PlayerTotalDamage += CumDamage
                If CumDamage > PlayerMaxDPS Then PlayerMaxDPS = CumDamage
                LastTime = dtParse.Rows(i)("StartTime")
                CumDamage = 0
            End If
        Next
    End Sub

    Private Sub GetEncDamage()
        Dim damage As Long
        EncMaxDPS = 0
        EncTotalDamage = 0
        For i As Integer = 1 To dtParse.Rows.Count - 1
            damage = dtParse.Rows(i)("Damage")
            EncTotalDamage += damage
            If damage > EncMaxDPS Then EncMaxDPS = damage
        Next
    End Sub

    Private Sub ShowDPS()
        GetDPS()
        bDPS = Nothing
        gDPS = Nothing
        DrawDPS(SecsStart, SecsShown)
    End Sub

    Private Sub RedrawAll()
        If Me.WindowState = FormWindowState.Minimized Then Exit Sub
        bBuff = Nothing
        gBuff = Nothing
        DrawBuffs()
        bTL = Nothing
        gTL = Nothing
        DrawTimeline(SecsStart)
        bDPS = Nothing
        gDPS = Nothing
        DrawDPS(SecsStart, SecsShown)
        bBoss = Nothing
        gBoss = Nothing
        DrawBosses()
    End Sub

    Private Sub DoZoom(pic As PictureBox, x As Integer)
        Try
            ' Get the selected range of pixels
            If Math.Abs(x - XDown) < 20 Then Exit Sub
            Dim xLeft As Integer = XDown
            Dim xRight As Integer = x
            If xRight < XDown Then
                xLeft = x
                xRight = XDown
            End If
            If xLeft < 0 Then Exit Sub
            If xRight > picDPS.Width Then Exit Sub

            ' Transform to seconds
            Dim SecsLeft = PixelToSecs(pic, xLeft)
            Dim SecsRight As Integer = PixelToSecs(pic, xRight)
            If SecsLeft = SecsRight Then Exit Sub

            ' Redraw zoomed image
            SecsStart = SecsLeft
            SecsShown = SecsRight - SecsStart
            CanRedraw = True
            RedrawAll()

        Finally
            MouseIsDown = False
            pic.Cursor = Cursors.Default
        End Try
    End Sub

    ' Format a tick nicely
    Private Function FormatTick(secs As Integer) As String
        Dim ts As TimeSpan = TimeSpan.FromSeconds(secs)
        Dim mydate As DateTime = New DateTime(ts.Ticks)
        If mydate.Hour > 0 Then
            Return mydate.Hour.ToString.PadLeft(2, " ") + ":" + mydate.Minute.ToString.PadLeft(2, " ") + ":" + mydate.Second.ToString.PadLeft(2, " ")
        ElseIf mydate.Second > 0 Then
            Return mydate.Minute.ToString.PadLeft(2, " ") + ":" + mydate.Second.ToString.PadLeft(2, " ")
        Else
            Return mydate.Second.ToString.PadLeft(2, " ")
        End If
    End Function

    Private Function SecsToPixel(pic As PictureBox, secs As Integer) As Integer
        Dim secsin As Integer = secs - SecsStart
        Dim percentin As Double = secsin / SecsShown
        Dim x As Integer = BufferLeft + percentin * (pic.Width - BufferLeft - BufferRight)
        Return x
    End Function

    Private Function PixelToSecs(pic As PictureBox, x As Integer) As Integer
        Dim percentin = (x - BufferLeft) / (pic.Width - BufferLeft - BufferRight)
        Dim secs As Integer = CInt((SecsStart + SecsShown) * percentin)
        If secs < 0 Then secs = 0
        If secs > SecsShown Then secs = SecsShown
        Return secs
    End Function

    Private Sub frmGraphics_Resize(sender As Object, e As EventArgs) Handles MyBase.Resize
        If CanRedraw Then RedrawAll()
    End Sub

    Private Function IsOverlap(A1 As Integer, A2 As Integer, B1 As Integer, B2 As Integer, Optional Buffer As Integer = 60) As Boolean
        Return Not ((A2 < (B1 - Buffer)) OrElse (A1 > (B2 + Buffer)))
    End Function

    Private Sub numBossUptime_ValueChanged(sender As Object, e As EventArgs) Handles numBossUptime.ValueChanged
        If Not CanRedraw Then Exit Sub
        DrawBosses()
    End Sub

    Private Sub numDPS_ValueChanged(sender As Object, e As EventArgs) Handles numDPS.ValueChanged
        If Not CanRedraw Then Exit Sub
        ShowDPS()
    End Sub

    Private Sub picDPS_MouseDown(sender As Object, e As MouseEventArgs) Handles picBuffs.MouseDown, picTL.MouseDown, picBosses.MouseDown, picDPS.MouseDown
        If sender.Image Is Nothing Then Exit Sub

        If e.Button = MouseButtons.Left Then
            XDown = e.X
            sender.Cursor = crLeftRight
            MouseIsDown = True
        ElseIf e.Button = MouseButtons.Right Then
            SecsStart = 0
            SecsFull = DateDiff(DateInterval.Second, TimeLeft, TimeRight)
            SecsShown = SecsFull
            RedrawAll()
        End If

    End Sub

    Private Sub picDPS_MouseUp(sender As Object, e As MouseEventArgs) Handles picBuffs.MouseUp, picTL.MouseUp, picBosses.MouseUp, picDPS.MouseUp
        If MouseIsDown Then DoZoom(sender, e.X)
    End Sub

    Private Sub picTL_MouseLeave(sender As Object, e As EventArgs) Handles picBuffs.MouseLeave, picTL.MouseLeave, picBosses.MouseLeave, picDPS.MouseLeave
        txtPop.Visible = False
    End Sub

    Private Sub picBuffs_MouseMove(sender As Object, e As MouseEventArgs) Handles picBuffs.MouseMove, picTL.MouseMove, picBosses.MouseMove, picDPS.MouseMove
        ShowPopup(sender, e.X, e.Y)
    End Sub

    Private Sub ShowPopup(pic As PictureBox, xNew As Integer, yNew As Integer)
        If txtPop.Tag = New Point(xNew, yNew) AndAlso txtPop.Visible Then Exit Sub

        Dim row As DataRow
        For i As Integer = 1 To dtLevels.Count - 1
            For j As Integer = 0 To dtLevels(i).Rows.Count - 1
                row = dtLevels(i).Rows(j)
                If IsWithin(xNew, yNew, row("x1"), row("y1"), row("x2"), row("y2")) Then
                    txtPop.Text = row("Player")
                    GoTo GotName
                End If
            Next
        Next
        txtPop.Text = FormatTime(PixelToSecs(pic, xNew).ToString)
GotName:
        Dim x As Integer = pic.Left + xNew - txtPop.Width - 4
        Dim y As Integer = yNew - 20
        If y < 1 Then y = 1
        If x < 0 Then x = 0
        txtPop.Left = x
        txtPop.Top = y
        txtPop.Parent = pic
        txtPop.BringToFront()
        txtPop.Visible = True
        txtPop.Tag = New Point(x, y)
    End Sub

    Private Sub frmGraphics_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Me.Location = My.Settings.ChartLastPos
        My.Settings.ChartLastSize = Me.Size
        My.Settings.ChartLastRaid = chkRaid.Checked
        My.Settings.ChartLastGroup = chkGroup.Checked
        My.Settings.ChartLastPersonal = chkPersonal.Checked
        My.Settings.LastChartUptime = numBossUptime.Value
        My.Settings.ChartLastAgg = numDPS.Value
        My.Settings.Save()
    End Sub

    Private Sub cboPlayers_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboPlayers.SelectedIndexChanged
        If Not CanRedraw Then Exit Sub
        PlayerSelected = cboPlayers.Text
        ShowDPS()
    End Sub

    Private Sub chkRaid_CheckedChanged(sender As Object, e As EventArgs) Handles chkRaid.CheckedChanged, chkGroup.CheckedChanged, chkPersonal.CheckedChanged
        If Not CanRedraw Then Exit Sub
        bBuff = Nothing
        gBuff = Nothing
        DrawBuffs()
    End Sub

    Private Sub lblCrop_Click(sender As Object, e As EventArgs) Handles lblCrop.Click
        frmCrop.TimeFrom = DateAdd(DateInterval.Second, SecsStart, dtParse.Rows(0)("StartTime"))
        frmCrop.TimeTo = DateAdd(DateInterval.Second, SecsStart + SecsShown, dtParse.Rows(0)("StartTime"))
        frmCrop.ShowDialog(Me)
    End Sub

End Class