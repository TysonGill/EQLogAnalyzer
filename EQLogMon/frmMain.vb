Imports System
Imports System.IO
Imports System.ComponentModel
Imports System.Data.SqlClient
Imports ProGrid.ProGrid
Imports Utility
Imports System.Text.RegularExpressions
Imports System.Net.Http.Headers

Public Class frmMain

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            ' My.Settings.Reset() ' Uncomment to clear User Settings

            ' Restore window to last size and position
            If My.Settings.MainLastPos.X > 0 Then
                If My.Settings.MainLastPos.X < My.Computer.Screen.WorkingArea.Width AndAlso My.Settings.MainLastPos.Y < My.Computer.Screen.WorkingArea.Height Then
                    Me.Location = My.Settings.MainLastPos
                    Me.Size = My.Settings.MainLastSize
                End If
            Else
                Me.Location = New Point(My.Computer.Screen.WorkingArea.Width / 2 - Me.Width / 2, My.Computer.Screen.WorkingArea.Height / 2 - Me.Height / 2)
            End If
            splitMain.SplitterDistance = My.Settings.LastSplitter
            NonMelee = My.Settings.LastNonMelee
            mnuNonMelee.Checked = NonMelee
            ENC_GAP_SECS = My.Settings.GapSecs
            ENC_MIN_ROWS = My.Settings.MinAttacks

            IncludePets = My.Settings.LastIncludePets
            mnuIncludePets.Checked = IncludePets
            If IncludePets Then ssPets.Text = "Credit Pets" Else ssPets.Text = "Pets Separate"

            NonMelee = My.Settings.LastNonMelee
            mnuNonMelee.Checked = NonMelee
            If NonMelee Then ssNonMelee.Text = "Credit Non-Melee" Else ssNonMelee.Text = "Non-Melee Ignored"

            ' Set tooltips
            Dim tt1 As New ToolTip()
            tt1.AutomaticDelay = 100
            tt1.AutoPopDelay = 4000
            tt1.SetToolTip(lblRefresh, "Load/Reload log file")

            ' Assign cursors
            crPress = CreateCursorWithHotspot(My.Resources.CrPointer, 22, 11)
            lblRefresh.Cursor = crPress
            lblCharts.Cursor = crPress

            ' Initialize parse table
            dtParse.TableName = "Parse"
            dtParse.Columns.Add("RowNum", GetType(Integer))
            dtParse.Columns.Add("StartTime", GetType(DateTime))
            dtParse.Columns.Add("Player", GetType(String))
            dtParse.Columns.Add("Target", GetType(String))
            dtParse.Columns.Add("DamageType", GetType(String))
            dtParse.Columns.Add("Damage", GetType(Integer))
            dtParse.Columns.Add("Source", GetType(String))

            ' Initialize encounters table
            dtEncounters.TableName = "Encounters"
            dtEncounters.Columns.Add("EncNum", GetType(Integer))
            dtEncounters.Columns.Add("StartTime", GetType(DateTime))
            dtEncounters.Columns.Add("EndTime", GetType(DateTime))
            dtEncounters.Columns.Add("ParseRowStart", GetType(Integer))
            dtEncounters.Columns.Add("ParseRowEnd", GetType(Integer))
            dtEncounters.Columns.Add("Duration", GetType(Integer))
            dtEncounters.Columns.Add("Main Target", GetType(String))
            dtEncounters.Columns.Add("Targets", GetType(String))

            ' Initialize DPS table
            dtDamage.TableName = "Damage"
            dtDamage.Columns.Add("Damager", GetType(String))
            dtDamage.Columns.Add("% Damage", GetType(Double))
            dtDamage.Columns.Add("Damage (1000's)", GetType(Long))

            ' Initialize death table
            dtDeaths.TableName = "Deaths"
            dtDeaths.Columns.Add("Deceased", GetType(String))
            dtDeaths.Columns.Add("Time of Death", GetType(DateTime))

            ' Initialize buffs table
            dtBuffs.TableName = "Buffs"
            dtBuffs.Columns.Add("StartTime", GetType(DateTime))
            dtBuffs.Columns.Add("Player", GetType(String))
            dtBuffs.Columns.Add("Buff", GetType(String))
            dtBuffs.Columns.Add("Scope", GetType(String))
            dtBuffs.Columns.Add("Duration", GetType(Integer))

            ' Initialize master buff list table
            dtBuffList.TableName = "Buff List"
            dtBuffList.Columns.Add("Buff", GetType(String))
            dtBuffList.Columns.Add("Contains", GetType(String))
            dtBuffList.Columns.Add("Scope", GetType(String))
            dtBuffList.Columns.Add("Duration", GetType(Integer))
            Dim s() As String
            For Each buff As String In My.Resources.Buffs.Replace(vbCrLf, "|").Split("|")
                s = buff.Split(",")
                dtBuffList.Rows.Add(s(0), s(1), s(2), CInt(s(3)))
            Next

            ' Initialize boss table
            dtBosses.TableName = "Bosses"
            dtBosses.Columns.Add("Boss", GetType(String))
            dtBosses.Columns.Add("Born", GetType(DateTime))
            dtBosses.Columns.Add("Life (1000's)", GetType(Long))
            dtBosses.Columns.Add("Died", GetType(DateTime))
            dtBosses.Columns.Add("Lifespan", GetType(String))

            ' Initialize pets table
            dtPets.TableName = "Pets"
            dtPets.Columns.Add("PetID", GetType(Integer))
            dtPets.Columns.Add("Owner", GetType(String))
            dtPets.Columns.Add("Pet", GetType(String))

            ' Set up data connection
            If ServerEnabled Then
                ' Establish a DataQuery connection. All database queries can be executed through the dq object.
                dq = New clsDataQuery(ConnectionString)
                ' Get the pet/owner table - this feature not available if not connected to a database
                dtPets = dq.GetTable("Pet Owners", "SELECT * FROM EQPets ORDER BY [Owner]")
            Else
                ' Disable server capabilities if no connectionstring is provided
                mnuServer.Enabled = False
                ssServerLog.Visible = False
                mnuPetOwners.Enabled = False
                ssPets.Visible = False
            End If

            ' Configure main window
            lblSearch.Visible = False
            txtSearch.Visible = False
            lblTop.Visible = False

            ' Configure grid
            pg.EnableRefreshButton = False
            pg.StyleSet("Damage (1000's)", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right, FormatString:="N0"))
            pg.StyleSet("% Damage", New ProGridStyle(ProGridStyleTypes.Percent, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("Duration", New ProGridStyle(ProGridStyleTypes.Freeform, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("Total Deaths", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right, FormatString:="N0"))
            pg.StyleSet("Deaths Per Hour", New ProGridStyle(ProGridStyleTypes.Numeric, DecimalPlaces:=2, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("Life (1000's)", New ProGridStyle(ProGridStyleTypes.Numeric, AlignmentMethod:=ProGridAlignmentStyles.Right))
            pg.StyleSet("Lifespan", New ProGridStyle(ProGridStyleTypes.Freeform, AlignmentMethod:=ProGridAlignmentStyles.Right))

            ' Build the mob lookup list
            lstMobs = My.Resources.MobNames.Replace(vbCrLf, "|").Split("|")

            ' Display main window
            Me.BringToFront()
            Me.Show()
            Application.DoEvents()

            ' Assign previous log file if it still exists
            If ValidateLogName(My.Settings.LastLogPath + My.Settings.LastLogFile) Then
                LogFile = My.Settings.LastLogPath + My.Settings.LastLogFile
                ssLogFile.Text = Path.GetFileName(LogFile)
                FileOwner = GetOwner(ssLogFile.Text)
                ssStatus.Text = "Click Refresh to load previous log or select a new one..."
            End If

        Catch ex As Exception
            ReportError(ex, True)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub LoadLog()
        Try
            Cursor = Cursors.WaitCursor

            ' Initialize the window
            tv.Nodes.Clear()
            pg.GridClear()
            txtSearch.Visible = False
            lblTop.Visible = False
            Application.DoEvents()

            ' Verify that a valid log file has been selected
            If LogFile = "" Then
                ssStatus.ForeColor = Color.Red
                ssStatus.Text = "Select a log file..."
                Exit Sub
            End If

            ' Get the time the parsing started
            Dim StartParsing As DateTime = Now

            ' Parse the log file
            ParseLogFile()
            If dtParse.Rows.Count = 0 Then
                ssStatus.ForeColor = SystemColors.ControlText
                ssStatus.Text = "No records"
                Exit Sub
            End If

            ' Create the encounter table
            lstPlayers.Sort()
            CreateEncounters()
            ShowTv()

            ' Log the parse to the database
            ParseTime = DateDiff(DateInterval.Second, StartParsing, Now)
            TrackParse()

            ' Update the status bar
            ssStatus.ForeColor = SystemColors.ControlText
            ssStatus.Text = "Ready"

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ParseLogFile()
        Try
            Cursor = Cursors.WaitCursor

            ' Initialize status bar
            ssStatus.ForeColor = Color.Red
            ssStatus.Text = "Loading..."
            Application.DoEvents()

            ' Reset globals
            MergeNum = 0
            dtParse.Rows.Clear()
            dtDeaths.Rows.Clear()
            dtBuffs.Rows.Clear()
            dtBosses.Rows.Clear()
            lstPlayers.Clear()

            ' Initialize local variables
            Dim s As String
            Dim dps As New DamageParseStruct
            Dim RowNum As Integer = 0
            Dim nParsed As Integer = 0
            Dim reader As StreamReader

            ' Parse log records
            reader = My.Computer.FileSystem.OpenTextFileReader(LogFile)
            Do
                ' Get the next line
                s = reader.ReadLine
                If s Is Nothing Then Exit Do

                ' Update progress on status bar
                nParsed += 1
                If nParsed Mod 1000 = 0 Then ssStatus.Text = "Parsing " + nParsed.ToString("N0") + "..."
                Application.DoEvents()

                ' Parse for deaths
                If ParseDeathLine(s) Then Continue Do

                ' Parse for buffs
                If ParseBuffLine(s) Then Continue Do

                ' Parse for an action line
                dps = ParseDamageLine(s)
                If Not dps.IsValid Then Continue Do

                ' Add row to parse table
                dtParse.Rows.Add(RowNum, dps.StartTime, dps.Player, dps.Target, dps.DamageType, dps.Damage, dps.Source)
                RowNum += 1

            Loop
            reader.Close()
            reader = Nothing

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub CreateEncounters()
        Try
            Cursor = Cursors.WaitCursor

            ' Initialize status bar
            ssStatus.ForeColor = Color.Red
            ssStatus.Text = "Finding Encounters..."
            Application.DoEvents()

            ' Initialize local variables
            Dim StartRowNum As Integer = 0
            Dim EncNum As Integer = 0
            Dim EndRowNum As Integer
            Dim Duration As Integer
            Dim MainTarget As String

            ' Populate encounters table
            dtEncounters.Rows.Clear()
            For i As Integer = 1 To dtParse.Rows.Count - 1

                If DateDiff(DateInterval.Second, dtParse.Rows(i - 1)("StartTime"), dtParse.Rows(i)("StartTime")) > ENC_GAP_SECS OrElse i = dtParse.Rows.Count - 1 Then
                    EndRowNum = i - 1
                    If EndRowNum - StartRowNum < ENC_MIN_ROWS Then
                        ' The block is too small, reset encounter starting position
                        StartRowNum = i
                    Else
                        ' Add encounter record
                        EncNum += 1
                        Duration = DateDiff(DateInterval.Second, dtParse.Rows(StartRowNum)("StartTime"), dtParse.Rows(EndRowNum)("StartTime"))
                        dtEncounters.Rows.Add(EncNum, dtParse.Rows(StartRowNum)("StartTime"), dtParse.Rows(EndRowNum)("StartTime"), StartRowNum, EndRowNum, Duration, "", "Pending")
                        StartRowNum = i

                        ssStatus.Text = "Finding Encounters " + EncNum.ToString + "..."
                        Application.DoEvents()

                    End If
                End If
            Next

            ' Determine the highest hp target to name the encounter
            For i As Integer = 0 To dtEncounters.Rows.Count - 1
                MainTarget = GetMainTarget(dtEncounters(i)("ParseRowStart"), dtEncounters(i)("ParseRowEnd"))
                dtEncounters.Rows(i)("Main Target") = MainTarget
            Next

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowTv()
        Try
            Cursor = Cursors.WaitCursor

            ' Initialize status bar
            ssStatus.ForeColor = Color.Red
            ssStatus.Text = "Building Tree..."
            Application.DoEvents()

            ' Initialize local variables
            Dim EncNode As TreeNode
            Dim TargetNode As TreeNode
            Dim CurNode As TreeNode
            Dim row As DataRow

            ' Populate tree
            tv.Nodes.Clear()
            Dim TopNode As TreeNode = tv.Nodes.Add("All Encounters")
            TopNode.Tag = "Utility: All Encounters"
            TopNode.ToolTipText = "Display damage parse across all encounters"

            CurNode = TopNode.Nodes.Add("Gap Report")
            CurNode.Tag = "Utility: Gap Report"
            CurNode.ToolTipText = "Display gap times between encounters"

            CurNode = TopNode.Nodes.Add("Boss Uptime")
            CurNode.Tag = "Utility: Boss Uptime"
            CurNode.ToolTipText = "Display boss uptime report"

            CurNode = TopNode.Nodes.Add("Death Report")
            CurNode.Tag = "Utility: Death Report"
            CurNode.ToolTipText = "Display a downtime report"

            CurNode = TopNode.Nodes.Add("Time Period")
            CurNode.Tag = "Utility: Time Period"
            CurNode.ToolTipText = "Display a damage report for a specific time period"

            CurNode = TopNode.Nodes.Add("Log Search")
            CurNode.Tag = "Utility: Log Search"
            CurNode.ToolTipText = "Perform a substring search on current log file"

            CurNode = TopNode.Nodes.Add("Parsed Data")
            CurNode.Tag = "Utility: Parsed Data"
            CurNode.ToolTipText = "Display all parsed metadata"

            For i As Integer = 0 To dtEncounters.Rows.Count - 1

                row = dtEncounters.Rows(i)

                ' Add encounter node to treeview
                EncNode = tv.Nodes.Add(row("Main Target"))
                EncNode.Tag = row
                EncNode.ToolTipText = FormatDate(row("StartTime")) + " : " + FormatTime(row("Duration"))

                ' Add a dummy node to be loaded dynamically
                TargetNode = EncNode.Nodes.Add("Targets")
                TargetNode.Tag = "Pending"

                Application.DoEvents()
            Next

            If tv.Nodes.Count > 0 Then tv.SelectedNode = tv.Nodes(0)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Dynamic loading of treeview
    Private Sub tv_BeforeExpand(sender As Object, e As TreeViewCancelEventArgs) Handles tv.BeforeExpand
        Try
            Cursor = Cursors.WaitCursor
            If e.Node.Level <> 0 OrElse e.Node.Tag.GetType() IsNot GetType(DataRow) Then Exit Sub
            If e.Node.Nodes.Count <> 1 OrElse e.Node.Nodes(0).Tag <> "Pending" Then Exit Sub

            e.Node.Nodes(0).Remove()
            Dim row As DataRow = e.Node.Tag
            If row("Targets") = "Pending" Then LoadEncPending(row)
            Dim Targets() As String = row("Targets").ToString.Split("|")
            Dim CurNode As TreeNode
            For Each target As String In Targets
                CurNode = e.Node.Nodes.Add(target)
                CurNode.Tag = "Target"
            Next

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Load pending Targets for an encounter
    Private Sub LoadEncPending(EncRow As DataRow)

        If EncRow("Targets") <> "Pending" Then Exit Sub

        ' Create a select for the parse records in the encounter
        Dim sql As String
        Dim dt As DataTable
        sql = "[RowNum]>=" + fmt.q(EncRow("ParseRowStart")) + " AND [RowNum]<=" + fmt.q(EncRow("ParseRowEnd"))
        dt = dtParse.Select(sql, "[Target] ASC").CopyToDataTable

        ' Get all targets present for that encounter
        Dim cTargets As Collection = db.GetDistinct(dt, "Target")
        Dim TargetList As String = ""
        For Each Target As String In cTargets
            TargetList += Target + "|"
        Next
        TargetList = TargetList.Substring(0, TargetList.Length - 1)
        EncRow("Targets") = TargetList

    End Sub

    Private Sub DoNodeClick(CurNode As TreeNode)
        Try
            Cursor = Cursors.WaitCursor

            lblSearch.Visible = False
            txtSearch.Visible = False
            lblTop.Visible = True
            pg.ButtonQuickSet()

            If CurNode.Tag.ToString.StartsWith("Utility") Then
                Select Case CurNode.Tag
                    Case "Utility: All Encounters"
                        DoDamageParse()
                        lblTop.Text = ""
                        pg.ButtonSet(1, "Clip", "Send a text summary to clipboard", "ExportToClipboard")
                        lblTop.Text = vbCrLf + vbCrLf + "Damage report spanning all encounters"
                    Case "Utility: Gap Report"
                        lblTop.Text = vbCrLf + vbCrLf + "Downtime between encounters"
                        ShowGaps()
                    Case "Utility: Death Report"
                        ShowDeathsPerHour()
                    Case "Utility: Time Period"
                        pg.ButtonSet(1, "Clip", "Send a text summary to clipboard", "ExportToClipboard")
                        lblSearch.Tag = "Time Period"
                        lblSearch.Visible = True
                        txtSearch.Visible = True
                        lblTop.Text = "Edit the time period for the range to report"
                        txtSearch.Text = FormatDate(dtParse.Rows(0)("StartTime")) + " - " + FormatDate(dtParse.Rows(dtParse.Rows.Count - 1)("StartTime"))
                        pg.GridClear()
                    Case "Utility: Log Search"
                        lblSearch.Tag = "Substring"
                        lblSearch.Visible = True
                        txtSearch.Visible = True
                        lblTop.Text = "Enter comma-separated 'AND' search phrases (spaces are included)"
                        txtSearch.Text = ""
                        pg.GridClear()
                    Case "Utility: Parsed Data"
                        lblTop.Text = vbCrLf + vbCrLf + "All parsed data from " + FormatDate(dtParse.Rows(0)("StartTime")) + " to " + FormatDate(dtParse.Rows(dtParse.Rows.Count - 1)("StartTime"))
                        pg.GridShow(dtParse, ProGridOrienations.Horizontal)
                    Case "Utility: Boss Uptime"
                        lblTop.Text = vbCrLf + vbCrLf + "Boss Uptime Report"
                        ShowBossUptime()
                End Select
                Exit Sub
            End If

            Dim TotalDamage As Long
            If CurNode.Level = 0 Then
                TotalDamage = DoDamageParse(CurNode.Tag, "All Targets")
                lblTop.Text = vbCrLf + "Damage report for the encounter that included " + CurNode.Text + vbCrLf + CurNode.ToolTipText + vbCrLf + FormatDamage(TotalDamage) + " total damage"
                pg.ButtonSet(1, "Clip", "Send a text summary to clipboard", "ExportToClipboard")
            Else
                Select Case CurNode.Tag
                    Case "Target"
                        Dim EncRow As DataRow = CurNode.Parent.Tag
                        TotalDamage = DoDamageParse(EncRow, CurNode.Text)
                        lblTop.Text = vbCrLf + "Damage report against " + CurNode.Text + vbCrLf + "during the encounter that included " + CurNode.Parent.Text + vbCrLf + FormatDamage(TotalDamage) + " total damage"
                        pg.ButtonSet(1, "Clip", "Send a text summary to clipboard", "ExportToClipboard")
                End Select
            End If
            tv.Focus()

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowBossUptime()
        Try
            Cursor = Cursors.WaitCursor

            pg.GridClear()
            If dtBosses.Rows.Count = 0 Then Exit Sub

            ' Prep lifespans
            If dtBosses.Rows(0)("Lifespan") = "" Then

                For i As Integer = 0 To dtBosses.Rows.Count - 1
                    dtBosses(i)("Life (1000's)") /= 1000
                    dtBosses(i)("Lifespan") = FormatTime(DateDiff(DateInterval.Second, dtBosses(i)("Born"), dtBosses(i)("Died")))
                Next
                dtBosses = db.SortDataTable(dtBosses, "Life (1000's) DESC")
            End If

            ' Populate table
            pg.GridShow(dtBosses, ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function DoDamageParse(Optional EncRow As DataRow = Nothing, Optional Target As String = "All Targets") As Long
        Dim ParseRowStart As Integer
        Dim ParseRowEnd As Integer
        If EncRow Is Nothing Then
            ParseRowStart = 0
            ParseRowEnd = dtParse.Rows.Count - 1
        Else
            ParseRowStart = EncRow("ParseRowStart")
            ParseRowEnd = EncRow("ParseRowEnd")
        End If
        Return DoDamageParse(ParseRowStart, ParseRowEnd, Target)
    End Function

    Private Function DoDamageParse(ParseRowStart As Integer, ParseRowEnd As Integer, Optional Target As String = "All Targets") As Long
        Try
            Cursor = Cursors.WaitCursor

            Dim sum As Long
            Dim percent As Double
            Dim obj As Object

            dtDamage.Rows.Clear()

            ' Get parse records for period requested
            Dim dt As New DataTable
            Dim clause As String = "[RowNum] >= " + fmt.q(ParseRowStart) + " AND [RowNum] <= " + fmt.q(ParseRowEnd)
            If Target <> "All Targets" Then clause += " AND [Target] = " + fmt.q(Target)
            dt = dtParse.Select(clause).CopyToDataTable

            ' Get total damage
            Dim TotalDamage As Long = dt.Compute("SUM([Damage])", "")

            ' Compute DPS
            For Each name As String In lstPlayers
                obj = dt.Compute("SUM([Damage])", "[Player] = " + fmt.q(name))
                If IsDBNull(obj) Then Continue For
                sum = Convert.ToInt64(obj)
                If sum = 0 Then Continue For
                percent = CInt(sum / TotalDamage * 10000) / 100
                dtDamage.Rows.Add(name, percent, sum / 1000)
            Next

            ' Sort and show results
            dtDamage = db.SortDataTable(dtDamage, "[Damage (1000's)] DESC")
            ShowDPSParse()
            Return TotalDamage

        Catch ex As Exception
            ReportError(ex)
            Return 0
        Finally
            Cursor = Cursors.Default
        End Try
    End Function

    ' Display the merge window
    Private Sub DoMerge()
        Try
            Cursor = Cursors.WaitCursor

            ' Make sure the user has already parsed a starting log file
            If dtParse Is Nothing OrElse dtParse.Rows.Count = 0 Then
                MessageBox.Show("You must first parse your initial log file.", "Log File Merge", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Prompt for a file to merge
            Dim MergeFile As String = PromptMergeFile()
            If MergeFile = "" Then Exit Sub
            If MergeFile = LogFile Then
                MessageBox.Show("You have selected the starting log file.", "Log File Merge", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            Dim nRows As Integer = dtParse.Rows.Count

            ' Show merge dialog
            Dim frm As New frmMerge
            frm.MergeFile = MergeFile
            frm.ShowDialog(Me)

            ' Redisplay
            If dtParse.Rows.Count > nRows Then

                ssStatus.ForeColor = Color.Red
                ssStatus.Text = "Redisplaying..."
                Application.DoEvents()

                pg.GridClear()
                CreateEncounters()
                ShowTv()

                ' Update the status bar
                ssStatus.ForeColor = SystemColors.ControlText
                ssStatus.Text = "Parsed " + dtParse.Rows.Count.ToString("N0") + " lines"

            End If

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DoMessageReport(EncRow As DataRow, Player As String)
        Try
            Cursor = Cursors.WaitCursor

            ' Get applicable parse rows from the encounter table
            Dim ParseRowStart As Integer = EncRow("ParseRowStart")
            Dim ParseRowEnd As Integer = EncRow("ParseRowEnd")

            ' Create a table to hold results
            Dim dt = New DataTable
            dt.Columns.Add("Message", GetType(String))

            ' Check all parse records
            For i As Integer = ParseRowStart To ParseRowEnd
                If Player <> "All Players" AndAlso dtParse.Rows(i)("Player") <> Player Then Continue For
                dt.Rows.Add(dtParse.Rows(i)("Source"))
            Next

            ' Show the results
            pg.GridShow(dt, ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DoSubstringSearch()
        Try
            Cursor = Cursors.WaitCursor

            If txtSearch.Text.Length = 0 Then Exit Sub

            ' Check the input parameters
            Dim Phrases As String()
            Phrases = txtSearch.Text.Split(",")
            If Phrases.Count = 0 Then Exit Sub
            For i As Integer = 0 To Phrases.Count - 1
                Phrases(i) = Phrases(i).ToUpper
                If Phrases(i).Length < 3 Then
                    MessageBox.Show("Each phrase must be at least 3 characters long.", "Log File Phrase Search", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
            Next

            ' Find all matching messages in file
            Dim s As String
            Dim dt As DataTable = New DataTable
            dt.Columns.Add("Message", GetType(String))
            Dim reader As StreamReader = My.Computer.FileSystem.OpenTextFileReader(LogFile)
            Do
                ' Get the next line
                s = reader.ReadLine
                If s Is Nothing Then Exit Do

                ' Look for the substring
                For Each phrase As String In Phrases
                    If Not s.ToUpper.Contains(phrase) Then Continue Do
                Next
                dt.Rows.Add(s)

            Loop

            ' Sort and display results
            dt = db.SortDataTable(dt, "[Message] ASC")
            pg.GridShow(dt, ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub DoTimePeriodSearch()
        Try
            Cursor = Cursors.WaitCursor

            If txtSearch.Text.Length = 0 Then Exit Sub

            ' Get the time range to report
            Dim Phrases As String()
            Dim FromTime As DateTime
            Dim ToTime As DateTime
            Phrases = txtSearch.Text.Split("-")
            If Phrases.Count <> 2 Then Exit Sub
            If Not IsDate(Phrases(0)) AndAlso Not IsDate(Phrases(1)) Then Exit Sub
            Dim LogStartDate As DateTime = dtParse.Rows(0)("StartTime")
            If IsDate(Phrases(0)) Then
                FromTime = Convert.ToDateTime(Phrases(0))
            Else
                FromTime = LogStartDate
            End If
            If IsDate(Phrases(1)) Then
                ToTime = Convert.ToDateTime(Phrases(1))
            Else
                ToTime = dtParse.Rows(dtParse.Rows.Count - 1)("StartTime")
            End If

            ' Find the parse start and end indices
            Dim StartRowIndex As Integer = FindParseAtDate(FromTime)
            If StartRowIndex = -1 Then Exit Sub
            Dim EndRowIndex As Integer = FindParseAtDate(ToTime, StartRowIndex) - 1
            If EndRowIndex < 1 Then Exit Sub

            ' Compute the parse
            DoDamageParse(StartRowIndex, EndRowIndex)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowGaps()
        Try
            Cursor = Cursors.WaitCursor

            pg.GridClear()
            If dtEncounters.Rows.Count < 2 Then Exit Sub

            ' Initialize utilization table
            Dim dt As New DataTable
            dt.Columns.Add("Gap", GetType(Integer))
            dt.Columns.Add("Duration", GetType(String))

            ' Compute gap durations
            Dim GapNum As Integer = 1
            Dim GapTime As Integer
            For i As Integer = 1 To dtEncounters.Rows.Count - 1
                GapTime = DateDiff(DateInterval.Second, dtEncounters.Rows(i - 1)("StartTime"), dtEncounters.Rows(i)("StartTime"))
                dt.Rows.Add(GapNum, FormatTime(GapTime))
                GapNum += 1
            Next

            ' Display report
            pg.GridShow(dt, ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowDeaths()
        Try
            Cursor = Cursors.WaitCursor

            pg.ButtonSet(1, "DPH", "Display number of deaths per hour", "DeathsPerHour")
            pg.ButtonSet(2, "Deaths", "Display time of deaths", "Deaths")
            pg.ButtonSet(3, "")
            lblTop.Text = vbCrLf + vbCrLf + "Deaths for each character"

            ' Display report
            lblTop.Text = vbCrLf + vbCrLf + "Death Reports"
            pg.GridShow(dtDeaths, ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub ShowDeathsPerHour()
        Try
            Cursor = Cursors.WaitCursor

            pg.ButtonSet(1, "DPH", "Display number of deaths per hour", "DeathsPerHour")
            pg.ButtonSet(2, "Deaths", "Display time of deaths", "Deaths")
            pg.ButtonSet(3, "Clip", "Send a text summary to clipboard", "DPHToClipboard")
            lblTop.Text = vbCrLf + vbCrLf + "Deaths for each character"

            ' Create a table for results
            Dim dtDPM As New DataTable
            dtDPM.Columns.Add("Deceased", GetType(String))
            dtDPM.Columns.Add("Total Deaths", GetType(Integer))
            dtDPM.Columns.Add("Deaths Per Hour", GetType(Double))

            ' Get distinct players who died
            Dim Deaths As Collection = db.GetDistinct(dtDeaths, "Deceased")

            ' Get time range
            Dim DeathPeriod As Integer = DateDiff(DateInterval.Minute, dtDeaths.Rows(0)("Time of Death"), dtDeaths.Rows(dtDeaths.Rows.Count - 1)("Time of Death"))

            ' Populate table
            Dim DeathCount As Integer
            For Each player As String In Deaths
                DeathCount = dtDeaths.Compute("COUNT(Deceased)", "[Deceased]=" + fmt.q(player))
                dtDPM.Rows.Add(player, DeathCount, DeathCount / DeathPeriod * 60)
            Next

            ' Display report
            lblTop.Text = vbCrLf + vbCrLf + "Death Leaderboard"
            dtDPM = db.SortDataTable(dtDPM, "[Total Deaths] DESC")
            pg.GridShow(dtDPM, ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Handle a grid button click
    Private Sub DoGridButton(ButtonTag As String)
        Try
            Cursor = Cursors.WaitCursor

            Select Case ButtonTag

                Case "ExportToClipboard"
                    ExportToClipboard(tv.SelectedNode.Text, dtDamage)

                Case "Deaths"
                    ShowDeaths()

                Case "DeathsPerHour"
                    ShowDeathsPerHour()

                Case "DPHToClipboard"
                    DPHToClipBoard()

            End Select

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Show the DPS Parse in the grid
    Private Sub ShowDPSParse()
        If dtDamage.Rows.Count = 0 Then Exit Sub
        pg.GridShow(dtDamage, ProGridOrienations.Horizontal)
    End Sub

    Private Sub DPHToClipBoard()
        Try
            Cursor = Cursors.WaitCursor
            Dim ClipString As String = "Death Leaderboard: "
            Dim dt As DataTable = pg.GridTable
            For i As Integer = 0 To dt.Rows.Count - 1
                If i < 10 Then ClipString += "#" + (i + 1).ToString + " " + dt.Rows(i)("Deceased") + " " + dt.Rows(i)("Total Deaths").ToString + " |  "
            Next
            If ClipString.Length > 4 Then ClipString = ClipString.Substring(0, ClipString.Length - 2)
            Clipboard.SetText(ClipString)
        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub TrackParse()
        Try
            Cursor = Cursors.WaitCursor

            Dim info As New FileInfo(LogFile)
            Dim FileSize As Long = info.Length
            Dim FileDate As DateTime = info.CreationTime
            Dim sql As String
            sql = "INSERT INTO EQLog ([Owner],  [FileName], [FileDate], [FileSize], [ParseDate], [ParseMode], [ParseCount], [ParseSecs])  VALUES (" + fmt.q(FileOwner) + ", " + fmt.q(ssLogFile.Text) + ", " + fmt.q(FileDate) + ", " + fmt.q(FileSize) + ", " + fmt.q(Now) + ", 'Load', " + fmt.q(dtParse.Rows.Count) + ", " + fmt.q(ParseTime) + ")"
            dq.Execute(sql)

        Catch ex As Exception
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

#Region "Worker Routines"

    Private Function ValidateLogName(fullname As String) As Boolean
        Dim filename As String = Path.GetFileName(fullname)
        If Not filename.StartsWith("eqlog_") OrElse filename.Split("_").Count < 3 Then Return False
        Return File.Exists(fullname)
    End Function

    ' Prompt the user to select a log file
    Private Sub PromptLogFile()
        Try
            Cursor = Cursors.WaitCursor

            ' Prompt for new log file
            Dim fd As OpenFileDialog = New OpenFileDialog()
            fd.Title = "Select EQ Log File"
            fd.InitialDirectory = My.Settings.LastLogPath
            fd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            fd.FilterIndex = 1
            fd.CheckPathExists = True
            fd.CheckFileExists = True
            If fd.ShowDialog() <> DialogResult.OK Then Exit Sub

            ' Validate file naming
            If Not ValidateLogName(fd.FileName) Then
                MessageBox.Show("Valid file names must start with eqlog_[fileowner]_.", "Improper Log File Name", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ' Load new log file
            LogFile = fd.FileName
            My.Settings.LastLogPath = GetDirectory(LogFile)
            My.Settings.LastLogFile = Path.GetFileName(LogFile)
            My.Settings.Save()
            ssLogFile.Text = Path.GetFileName(LogFile)
            FileOwner = GetOwner(ssLogFile.Text)

            LoadLog()

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Prompt the user to select a merge file
    Private Function PromptMergeFile() As String
        Try
            Cursor = Cursors.WaitCursor

            ' Prompt for log file to merge
            Dim fd As OpenFileDialog = New OpenFileDialog()
            fd.Title = "Select EQ Log File to Merge"
            fd.InitialDirectory = My.Settings.LastLogPath
            fd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            fd.FilterIndex = 1
            fd.CheckPathExists = True
            fd.CheckFileExists = True
            If fd.ShowDialog() <> DialogResult.OK Then Return ""

            ' Validate file naming
            If Not ValidateLogName(fd.FileName) Then
                MessageBox.Show("Valid file names must start with eqlog_[fileowner]_.", "Improper Log File Name", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return ""
            End If

            Return fd.FileName

        Catch ex As Exception
            ReportError(ex)
            Return ""
        Finally
            Cursor = Cursors.Default
        End Try
    End Function

    Private Function GetMainTarget(ParseRowStart As Integer, ParseRowEnd As Integer) As String
        Dim damage As New Dictionary(Of String, Integer)
        Dim Target As String = ""
        Dim row As DataRow
        For i As Integer = ParseRowStart To ParseRowEnd
            row = dtParse.Rows(i)
            Target = row("Target")
            If damage.ContainsKey(Target) Then
                damage(Target) += row("Damage")
            Else
                damage.Add(Target, row("Damage"))
            End If
        Next
        Dim MaxDamage As Integer = 0
        For Each kvp As KeyValuePair(Of String, Integer) In damage
            If kvp.Value > MaxDamage Then
                MaxDamage = kvp.Value
                Target = kvp.Key
            End If
        Next
        Return Target
    End Function

#End Region

#Region "Event Handlers"

    ' Handle a grid button click event
    Private Sub pg_UserClickedButton(ButtonTag As String) Handles pg.UserClickedButton
        DoGridButton(ButtonTag)
    End Sub

    ' Save the window state before closing
    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ServerLogStop()
        My.Settings.MainLastPos = Me.Location
        My.Settings.MainLastSize = Me.Size
        My.Settings.LastSplitter = splitMain.SplitterDistance
        My.Settings.LastNonMelee = NonMelee
        My.Settings.GapSecs = ENC_GAP_SECS
        My.Settings.MinAttacks = ENC_MIN_ROWS
        My.Settings.LastIncludePets = IncludePets
        My.Settings.Save()
    End Sub

    ' Exit the application
    Private Sub mnuExit_Click(sender As Object, e As EventArgs) Handles mnuExit.Click
        Application.Exit()
    End Sub

    Private Sub tv_AfterSelect(sender As Object, e As TreeViewEventArgs) Handles tv.AfterSelect
        DoNodeClick(e.Node)
        If txtSearch.Visible Then txtSearch.Focus() ' does not work
    End Sub

    Private Sub lblRefresh_Click(sender As Object, e As EventArgs) Handles lblRefresh.Click
        lblRefresh.BackColor = Color.SkyBlue
        Application.DoEvents()
        LoadLog()
        lblRefresh.BackColor = Color.AliceBlue
    End Sub

    Private Sub mnuOpenLog_Click(sender As Object, e As EventArgs) Handles mnuOpenLog.Click, ssLogFile.Click
        PromptLogFile()
    End Sub

    Private Sub mnuNonMelee_Click(sender As Object, e As EventArgs) Handles mnuNonMelee.Click, ssNonMelee.Click
        NonMelee = Not NonMelee
        mnuNonMelee.Checked = NonMelee
        If NonMelee Then ssNonMelee.Text = "Credit Non-Melee" Else ssNonMelee.Text = "Non-Melee Ignored"
    End Sub

    Private Sub ssPets_Click(sender As Object, e As EventArgs) Handles mnuIncludePets.Click, ssPets.Click
        IncludePets = Not IncludePets
        mnuIncludePets.Checked = IncludePets
        If IncludePets Then ssPets.Text = "Credit Pets" Else ssPets.Text = "Pets Separate"
    End Sub

    Private Sub lblSearch_Click(sender As Object, e As EventArgs) Handles lblSearch.Click
        DoSearch()
    End Sub

    Private Sub txtSearch_KeyDown(sender As Object, e As KeyEventArgs) Handles txtSearch.KeyDown
        If e.KeyCode = Keys.Enter Then
            DoSubstringSearch()
        End If
    End Sub

    Private Sub DoSearch()
        Select Case lblSearch.Tag
            Case "Time Period"
                DoTimePeriodSearch()
            Case "Substring"
                DoSubstringSearch()
        End Select
    End Sub

    Private Sub mnuAbout_Click(sender As Object, e As EventArgs) Handles mnuAbout.Click
        frmAbout.ShowDialog(Me)
    End Sub

    Private Sub DoConfigure() Handles mnuOptions.Click
        Dim frm As New frmOptions
        frm.ShowDialog(Me)
        If frm.IsChanged AndAlso dtParse IsNot Nothing AndAlso dtParse.Rows.Count > 0 Then LoadLog()
    End Sub

    Private Sub mnuMerge_Click(sender As Object, e As EventArgs) Handles mnuMerge.Click
        DoMerge()
    End Sub

    Private Sub lblCharts_Click(sender As Object, e As EventArgs) Handles lblCharts.Click
        If dtParse.Rows.Count = 0 Then Exit Sub
        If Application.OpenForms.OfType(Of Form).Contains(frmChart) Then
            frmChart.WindowState = FormWindowState.Normal
            frmChart.BringToFront()
        Else
            frmChart.Show()
        End If
    End Sub

    Private Sub mnuClearLog_Click(sender As Object, e As EventArgs) Handles mnuClearLog.Click
        Try
            If File.Exists(LogFile) Then
                If MessageBox.Show("Discard all events from " + Path.GetFileName(LogFile) + "?" + vbCrLf + vbCrLf + "Are you sure?", "Clear Log File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> vbYes Then Exit Sub
                Dim fs As FileStream = File.Create(LogFile)
                fs.Close()
                LoadLog()
            End If
        Catch ex As Exception
            If ex.Message.EndsWith("because it is being used by another process.") Then
                MessageBox.Show("Cannot clear the log file because it is locked by another application, probably GINA.", "Clear Log File Failed", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                ReportError(ex)
            End If
        End Try
    End Sub

    Private Sub mnuServerReport_Click(sender As Object, e As EventArgs) Handles mnuServerReport.Click
        Dim frm As New frmServer
        frm.Show(Me)
    End Sub

    Private Sub mnuPetOwners_Click(sender As Object, e As EventArgs) Handles mnuPetOwners.Click
        Dim frm As New frmPetOwners
        frm.ShowDialog(Me)
    End Sub

    Private Sub mnuFAQ_Click(sender As Object, e As EventArgs) Handles mnuFAQ.Click
        Try
            Dim TempName As String = Path.GetTempPath + "EQ Log Analyzer.pdf"
            File.WriteAllBytes(TempName, My.Resources.EQ_Log_Analyzer_FAQ)
            ui.OpenFile(TempName)
        Catch ex As Exception
            ReportError(ex)
        End Try
    End Sub

#End Region

#Region "Realtime Log Parsing"

    ' Real time logging
    Dim Logging As Boolean = False
        Dim monWorker As New BackgroundWorker
        Dim monCurBatch As String = ""
        Dim monCurSize As Integer = 0
        Dim monTimeMS As Integer = 200
        Dim monBatchSize As String = 40
        Dim monParsed As New DamageParseStruct
        Dim monCon As SqlConnection
        Dim monCmd As SqlCommand
        Dim monSW As New Stopwatch()

    Private Sub mnuToggleLogging_Click(sender As Object, e As EventArgs) Handles mnuToggleLogging.Click
        If Logging Then ServerLogStop() Else ServerLogStart()
    End Sub

    Private Sub ssServerLog_Click(sender As Object, e As EventArgs) Handles ssServerLog.Click
        If Logging Then ServerLogStop() Else ServerLogStart()
    End Sub

    Private Sub ServerLogStart()

        ' Make sure a fileowner is set
        If FileOwner = "" Then
            MessageBox.Show("You must select the log file to monitor.", "Server Logging", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        ' Make sure we do not log from multiple instances
        If Process.GetProcessesByName("EQLogAnalyzer").Count > 1 Then
            MessageBox.Show("You cannot start logging when another instance of this application is active.", "Server Logging", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        mnuToggleLogging.Checked = True
        Logger = FileOwner
        Logging = True
        ssServerLog.ForeColor = Color.Red
        ssServerLog.Text = "Server Logging On"
        Application.DoEvents()
        tmrFlash.Enabled = True

        ' Kick off a background worker to handle parsing
        monWorker.WorkerReportsProgress = False
        monWorker.WorkerSupportsCancellation = True
        AddHandler monWorker.DoWork, AddressOf DoServerLogging
        monWorker.RunWorkerAsync()

    End Sub

    Private Sub ServerLogStop()
        Logging = False
        tmrFlash.Enabled = False
        Do
            Application.DoEvents()
        Loop While monWorker.IsBusy
        mnuToggleLogging.Checked = False
        ssServerLog.ForeColor = SystemColors.ControlText
        ssServerLog.Text = "Server Logging Off"
    End Sub

    Private Sub DoServerLogging()
        Try
            ' Initialize the database
            monCurSize = 0
            monCurBatch = ""
            monCon = New SqlConnection(ConnectionString)
            monCmd = New SqlCommand("", monCon)
            monCon.Open()

            ' Initialize the reader
            Dim Logfs As FileStream = New FileStream(LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
            Dim LogReader As New StreamReader(Logfs)
            LogReader.BaseStream.Position = LogReader.BaseStream.Seek(0, SeekOrigin.End)

            Dim ic As Integer
            Dim c As Char
            Dim s As String
            monSW.Reset()
            Do
                ' Get the next line
                s = ""
                Do
                    ' Get the next character
                    Do
                        ic = LogReader.Read()
                        If ic <> -1 Then Exit Do
                        If monSW.ElapsedMilliseconds > monTimeMS Then BatchSubmit()
                        If Not Logging Then GoTo QuitLogging
                        Wait(0.1, False)
                        LogReader.BaseStream.Position = LogReader.BaseStream.Position
                    Loop
                    c = ChrW(ic)
                    If c = vbCr Then
                        LogReader.Read()
                        Exit Do
                    Else
                        s += c
                    End If
                Loop
                s = s.Trim
                If s = "" Then Continue Do

                ' Parse the line
                monParsed = ParseDamageLine(s)
                If Not monParsed.IsValid Then Continue Do

                ' Generate the insert action query and add to batch
                monCurBatch += "EXEC EQLogAttack " + fmt.q(Now.ToUniversalTime) + "," + fmt.q(monParsed.StartTime.ToUniversalTime) + "," + fmt.q(Logger) + "," + fmt.q(monParsed.Player) + "," + fmt.q(monParsed.Target) + "," + fmt.q(monParsed.DamageType) + "," + fmt.q(monParsed.Damage) + ";"
                monCurSize += 1

                ' Submit batch if complete or timed out
                If monCurSize = 1 AndAlso monBatchSize > 1 Then
                    monSW.Restart()
                ElseIf monCurSize = monBatchSize OrElse monSW.ElapsedMilliseconds > monTimeMS Then
                    BatchSubmit()
                End If

                Application.DoEvents()
QuitLogging:
            Loop While Logging

            ' Shut down
            monSW.Reset()
            LogReader.Close()
            monCon.Close()

        Catch ex As Exception
            ReportError(ex)
        End Try
    End Sub

    Private Sub BatchSubmit()

        ' Flash the indicator to show activity
        TimerFlash = True

        ' Submit the batch
        monCmd.CommandText = monCurBatch
        monCmd.ExecuteNonQuery()
        monCurBatch = ""
        monCurSize = 0
        monSW.Reset()

    End Sub

    Private Sub mnuUploadLogFile_Click(sender As Object, e As EventArgs)
        Try
            Cursor = Cursors.WaitCursor

            ' Prompt for new log file
            Dim fd As OpenFileDialog = New OpenFileDialog()
            fd.Title = "Select EQ Log File"
            fd.InitialDirectory = My.Settings.LastLogPath
            fd.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            fd.FilterIndex = 1
            fd.CheckPathExists = True
            fd.CheckFileExists = True
            If fd.ShowDialog() <> DialogResult.OK Then Exit Sub

            UpLoadLogFile(fd.FileName)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub mnuUploadParse_Click(sender As Object, e As EventArgs) Handles mnuUploadParse.Click

        If dtParse.Rows.Count = 0 Then
            MessageBox.Show("There are no parsed records to upload.", "Parse Upload", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        UpLoadParse()

    End Sub

    ' Transfer parse to the database
    Friend Sub UpLoadParse(Optional BatchSize As Integer = 100)
        Try
            Cursor = Cursors.WaitCursor

            ' Verify that the file has not previously been uploaded
            If UploadCount(FileOwner, dtParse.Rows(0)("StartTime"), dtParse.Rows(dtParse.Rows.Count - 1)("StartTime")) > 0 Then
                MessageBox.Show("It appears that these records have already been included in the merge.", "Parse Upload", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If

            ssStatus.ForeColor = Color.Red
            Dim row As DataRow
            Dim nBatch As Integer = 0
            Dim sBatch As String = ""

            ' Update the status bar
            ssStatus.Text = "Starting upload..."
            Application.DoEvents()

            For i As Integer = 0 To 1999 'dtParse.Rows.Count - 1

                row = dtParse.Rows(i)

                ' Prep batch
                sBatch += "EXEC EQLogParse " + fmt.q(row("StartTime").ToUniversalTime) + "," + fmt.q(FileOwner) + "," + fmt.q(row("Player")) + "," + fmt.q(row("Target")) + ", " + fmt.q(row("DamageType")) + "," + fmt.q(row("Damage")) + ";"
                nBatch += 1
                If nBatch = BatchSize Then

                    ' Write the batch to the database
                    dq.Execute(sBatch)
                    sBatch = ""
                    nBatch = 0

                    ' Update the status bar
                    ssStatus.Text = "Uploaded " + (i + 1).ToString("N0") + " of " + dtParse.Rows.Count.ToString + "..."
                    Application.DoEvents()

                End If

            Next

            ' Write the remaining batch to the database
            If sBatch <> "" Then dq.Execute(sBatch)

            ssStatus.ForeColor = SystemColors.ControlText
            ssStatus.Text = "Ready"

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Function UploadCount(Owner As String, StartDate As DateTime, EndDate As DateTime) As Integer
        Dim sql As String
        sql = "SELECT COUNT(*) FROM EQLogDamage WHERE [FileOwner] = " + fmt.q(Owner) + " AND [LogUTCDate] >= " + fmt.q(StartDate.ToUniversalTime) + " AND [LogUTCDate] <= " + fmt.q(EndDate.ToUniversalTime)
        Return Val(dq.GetValue(sql))
    End Function

    ' Transfer a log file direct to the database
    Friend Sub UpLoadLogFile(file As String, Optional BatchSize As Integer = 200)
        Try
            Cursor = Cursors.WaitCursor

            Dim s As String
            ssStatus.ForeColor = Color.Red
            Dim mon As New DamageParseStruct
            Dim nLines As Integer = 0
            Dim nBatch As Integer = 0
            Dim sBatch As String = ""
            Dim sqlcon As SqlConnection = New SqlConnection(ConnectionString)
            Dim sqlcom As New SqlCommand("", sqlcon)
            sqlcon.Open()
            Dim reader As StreamReader
            reader = My.Computer.FileSystem.OpenTextFileReader(file)
            Do
                ' Get the next line
                s = reader.ReadLine
                If s Is Nothing Then Exit Do

                ' Parse the line
                mon = ParseDamageLine(s)
                If Not mon.IsValid Then Continue Do
                nLines += 1

                ' Prep batch
                sBatch += "EXEC EQLogAttack " + fmt.q(mon.StartTime.ToUniversalTime) + "," + fmt.q(mon.StartTime.ToUniversalTime) + "," + fmt.q(FileOwner) + "," + fmt.q(mon.Player) + "," + fmt.q(mon.Target) + "," + fmt.q(mon.DamageType) + "," + fmt.q(mon.Damage) + ";"
                nBatch += 1
                If nBatch = BatchSize Then
                    ' Write the batch to the database
                    sqlcom.CommandText = sBatch
                    sqlcom.ExecuteNonQuery()
                    sBatch = ""
                    nBatch = 0

                    ' Update the status bar
                    ssStatus.Text = "Uploaded " + nLines.ToString("N0") + " records"
                    Application.DoEvents()

                End If

            Loop

            If sBatch <> "" Then
                ' Write the remaining batch to the database
                sqlcom.CommandText = sBatch
                sqlcom.ExecuteNonQuery()
            End If
            reader.Close()
            sqlcon.Close()

            ssStatus.ForeColor = SystemColors.ControlText
            ssStatus.Text = "Ready"

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Dim TimerFlash As Boolean = False
    Private Sub tmrFlash_Tick(sender As Object, e As EventArgs) Handles tmrFlash.Tick
        If TimerFlash Then
            ssServerLog.Text = "Sending..."
            TimerFlash = False
        Else
            ssServerLog.Text = "Server Logging On"
        End If
        ss.Refresh()
        Application.DoEvents()
    End Sub

#End Region

End Class
