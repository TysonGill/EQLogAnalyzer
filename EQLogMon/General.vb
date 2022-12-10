Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Data.SqlClient
Imports Utility

Module General

    ' Server connection and enable
    Friend ServerEnabled As Boolean = True
    Friend ConnectionString As String = "Data Source=tcp:s14.winhost.com;Initial Catalog=DB_130918_bitistry;User ID=DB_130918_bitistry_user;Password=bplease;Integrated Security=False;"

    ' Friend declarations
    Friend LogFile As String = ""
    Friend FileOwner As String = ""
    Friend Logger As String = ""
    Friend ParseTime As Integer
    Friend lstPlayers As New List(Of String)
    Friend dtParse As New DataTable
    Friend dtEncounters As New DataTable
    Friend dtDamage As New DataTable
    Friend dtBosses As New DataTable
    Friend dtDeaths As New DataTable
    Friend dtBuffs As New DataTable
    Friend dtBuffList As New DataTable
    Friend dtPets As New DataTable
    Friend lstMobs As String()
    Friend RegexName = New Regex("^[A-Z][a-z]{3,14}$") ' Recognize a valid name format
    Friend DamageKeys As String() = {" hits ", " strikes ", " kicks ", " punches ", " pierces ", " slashes ", " crushes ", " bashes ", " shoots ", " backstabs ", " mauls ", " frenzies ", " bites ", " claws ", " gores ", " rends ", " slams ", " slices ", " stings ", " sweeps ", " non-melee ", " has taken ", " hit ", " strike ", " kick ", " punch ", " pierce ", " slash ", " crush ", " bash ", " shoot ", " backstab ", " maul ", " frenzy ", " bite ", " claw ", " gore ", " rend ", " slam ", " slice ", " sting ", " sweep "}
    Friend NonMelee As Boolean
    Friend dq As clsDataQuery
    Friend MergeNum As Integer
    Friend IncludePets As Boolean

    Friend crPress As Cursor
    Friend crLeftRight As Cursor

    Friend Structure DamageParseStruct
        Dim IsValid As Boolean
        Dim Source As String
        Dim StartTime As DateTime
        Dim Player As String
        Dim Target As String
        Dim DamageType As String
        Dim Damage As Integer
    End Structure
    Friend DamParse As New DamageParseStruct

    ' Private constants
    Friend ENC_GAP_SECS As Integer = 60 ' The 
    Friend ENC_MIN_ROWS As Integer = 30

    ' Parse a damage message
    Friend Function ParseDamageLine(s As String) As DamageParseStruct

        Dim PetOwner As String

        ' Replace smart quotes with standard quote
        s = s.Replace(Chr(96), "'") '"`"

        ' Initialize damage struct
        Dim dps As New DamageParseStruct
        dps.IsValid = False

        ' Check if message is an action line
        dps.DamageType = ContainsOne(s, DamageKeys)
        If dps.DamageType = "" Then Return dps
        If Not NonMelee AndAlso dps.DamageType = "non-melee" Then Return dps
        If s.Contains(" healed ") AndAlso s.Contains(" hit points") Then Return dps

        ' Get player name
        dps.Player = GetPlayer(s, dps.DamageType)
        If dps.Player = "" Then Return dps
        If BinarySearch(lstMobs, dps.Player.ToLower) >= 0 Then Return dps

        ' Include pet under owner
        If IncludePets Then
            PetOwner = GetPetOwner(dps.Player)
            If PetOwner <> "" Then dps.Player = PetOwner
        End If

        ' Add player to master player list
        If Not lstPlayers.Contains(dps.Player) Then lstPlayers.Add(dps.Player)

        ' Get the target name
        dps.Target = GetTarget(s, dps.DamageType)
        If dps.Target = "" Then Return dps
        If BinarySearch(lstMobs, dps.Target.ToLower) < 0 Then Return dps

        ' Get the event start time
        dps.StartTime = GetEventTime(s)

        ' Get the damage
        dps.Damage = GetDamage(s, dps.DamageType)
        If dps.Damage = 0 Then Return dps

        ' Update bosses table
        If Not dps.Target.ToLower.StartsWith("a ") AndAlso Not dps.Target.ToLower.StartsWith("an ") Then
            Dim BossRows As DataRow() = dtBosses.Select("[Boss]=" + fmt.q(dps.Target))
            If BossRows Is Nothing OrElse BossRows.Count = 0 Then
                dtBosses.Rows.Add(dps.Target, dps.StartTime, dps.Damage, dps.StartTime, "")
            Else
                BossRows(0)("Life (1000's)") += dps.Damage
                BossRows(0)("Died") = dps.StartTime
            End If
        End If

        ' Complete and return
        dps.Source = DropDate(s)
        dps.IsValid = True
        Return dps

    End Function

    Private Function GetPetOwner(PetName As String) As String
        For Each row In dtPets.Rows
            If row("Pet") = PetName Then Return row("Owner")
        Next
        Return ""
    End Function

    Friend Function ParseDeathLine(s As String) As Boolean
        If s.Contains(" has been slain by ") Then
            Dim sname As String = fmt.GetTextBetween(s, "] ", " has been slain by ")
            If Not RegexName.match(sname).success Then Return False
            dtDeaths.Rows.Add(sname, GetEventTime(s))
            Return True
        ElseIf s.EndsWith(" dies.") Then
            dtDeaths.Rows.Add(fmt.GetTextBetween(s, "] ", " dies."), GetEventTime(s))
            Return True
        ElseIf s.Contains("You have been slain by ") Then
            dtDeaths.Rows.Add(FileOwner, GetEventTime(s))
            Return True
        End If
        Return False
    End Function

    Friend Function ParseBuffLine(s As String) As Boolean
        Dim player As String
        For Each row As DataRow In dtBuffList.Rows
            If s.Contains(row("Contains")) Then
                player = fmt.GetTextBetween(s, "] ", row("Contains"))
                If player = "You" Then player = FileOwner
                dtBuffs.Rows.Add(GetEventTime(s), player, row("Buff"), row("Scope"), row("Duration"))
                Return True
            End If
        Next
        Return False
    End Function

    ' Get the owner of an EQ log file by parsing the file name
    Friend Function GetOwner(Filename As String) As String
        Dim spl() As String = Filename.Split("_")
        If spl.Count = 3 Then
            Return Filename.Split("_")(1)
        Else
            Return spl(1)
        End If
    End Function

    Private Function GetPlayer(s As String, Optional damagetype As String = "") As String
        Dim sname As String = ""
        If damagetype = "non-melee" Then
            sname = fmt.GetTextBetween(s, " by ", " for").Trim ' returns something like "Sayanara's thorns"
            Dim sp As String() = sname.Split(" ")
            If sp.Count > 1 Then sname = sp(0)
            If sname.EndsWith("'s") Then sname = sname.Substring(0, sname.Length - 2)
        ElseIf damagetype = "has taken" Then
            sname = fmt.GetTextBetween(s, " by ", ".").Trim
        Else
            sname = fmt.GetTextBetween(s, "] ", damagetype).Trim
        End If
        If sname.ToUpper = "YOURSELF" Then Return FileOwner
        If sname.ToUpper = "YOU" Then Return FileOwner
        If sname.EndsWith("'s pet") Then sname = sname.Substring(0, sname.Length - 6)
        If sname.EndsWith("'s warder") Then sname = sname.Substring(0, sname.Length - 9)
        If Not RegexName.match(sname).success Then Return ""
        Return sname
    End Function

    Private Function GetTarget(s As String, Optional damagetype As String = "") As String
        Dim sname As String = ""
        If damagetype = "non-melee" Then
            sname = fmt.GetTextBetween(s, "] ", " is").Trim
        ElseIf damagetype = "has taken" Then
            sname = fmt.GetTextBetween(s, "] ", " has").Trim
        Else
            sname = fmt.GetTextBetween(s, damagetype, " for").Trim
        End If
        If sname.ToUpper = "YOURSELF" Then Return FileOwner
        If sname.ToUpper = "YOU" Then Return FileOwner
        If sname.EndsWith("s pet") Then sname = sname.Substring(0, sname.Length - 6)
        If sname.EndsWith("s warder") Then sname = sname.Substring(0, sname.Length - 9)
        If sname.StartsWith("on ") Then sname = sname.Substring(2, sname.Length - 2)
        Return sname
    End Function

    Private Function GetDamage(s As String, Optional damagetype As String = "") As Integer
        If damagetype = "has taken" Then
            Return fmt.GetTextBetween(s, "taken ", " damage").Trim
        Else
            Return Val(fmt.GetTextBetween(s, " for ", " points ").Trim)
        End If
    End Function

    Friend Sub ExportToClipboard(Heading As String, dt As DataTable)
        Try
            Dim ClipString As String = "Damage Parse for " + Heading + ": "
            Dim percent As Double
            For i As Integer = 0 To dt.Rows.Count - 1
                percent = dt.Rows(i)("% Damage")
                If percent < 2 Then Exit For
                ClipString += "#" + (i + 1).ToString + " " + dt.Rows(i)("Damager") + " " + percent.ToString + "% |  "
            Next
            If ClipString.Length > 4 Then ClipString = ClipString.Substring(0, ClipString.Length - 3)
            Clipboard.SetText(ClipString)
        Catch ex As Exception
            ReportError(ex)
        End Try
    End Sub

    ' Find the first row at a desired datetime
    Friend Function FindParseAtDate(TargetDate As DateTime, Optional StartPos As Integer = 0) As Integer
        For i As Integer = StartPos To dtParse.Rows.Count - 1
            If dtParse.Rows(i)("StartTime") >= TargetDate Then Return i
        Next
        Return -1
    End Function

    ' Get the datetime from a log file message
    Friend Function GetEventTime(s As String) As DateTime
        Return ConvertLogDate(fmt.GetTextBetween(s, "[", "]"))
    End Function

    ' Get the event datetime
    Friend Function ConvertLogDate(EQDate As String) As DateTime
        Dim aTime() As String = EQDate.Split(" ")
        Return Date.Parse(aTime(1) + " " + aTime(2) + " " + aTime(4) + " " + aTime(3))
    End Function

    ' Drop the date from a log message
    Friend Function DropDate(s As String) As String
        Return s.Split("]")(1).Trim
    End Function

    ' Format a date nicely
    Friend Function FormatDate(d As DateTime) As String
        Return Format(d, "M/d/yyyy h:mm tt")
    End Function

    ' Format a time nicely
    Friend Function FormatTime(secs As Integer) As String
        Dim ts As TimeSpan = TimeSpan.FromSeconds(secs)
        Dim mydate As DateTime = New DateTime(ts.Ticks)
        If mydate.Hour > 0 Then
            Return mydate.Hour.ToString.PadLeft(2, " ") + " hr " + mydate.Minute.ToString.PadLeft(2, " ") + " min " + mydate.Second.ToString.PadLeft(2, " ") + " sec"
        ElseIf mydate.Second > 0 Then
            Return mydate.Minute.ToString.PadLeft(2, " ") + " min " + mydate.Second.ToString.PadLeft(2, " ") + " sec"
        Else
            Return mydate.Second.ToString.PadLeft(2, " ") + " sec"
        End If
    End Function

    Friend Function FormatDamage(damage As Long) As String
        If damage > 1000000 Then
            Return fmt.ToDecimalPlaces((damage / 1000000), 1) + "M"
        ElseIf damage > 1000 Then
            Return fmt.ToDecimalPlaces((damage / 1000), 1) + "K"
        Else
            Return damage.ToString
        End If
    End Function

    ' Return the directory path from a full file path and name
    Friend Function GetDirectory(FileName As String) As String
        Return fmt.AppendIfNeeded(Directory.GetParent(FileName).FullName, "\")
    End Function

    ' Return True if a any substring found in the list
    Friend Function ContainsAny(fullstring As String, substrings As String()) As Boolean
        If fullstring.Trim = "" OrElse substrings Is Nothing Then Return False
        For Each s As String In substrings
            If fullstring.Contains(s) Then Return True
        Next
        Return False
    End Function

    ' Return a matching substring from a list
    Friend Function ContainsOne(fullstring As String, substrings As String()) As String
        If fullstring.Trim = "" OrElse substrings Is Nothing Then Return ""
        For Each s As String In substrings
            If fullstring.Contains(s) Then Return s.Trim
        Next
        Return ""
    End Function

    ' Perform a binary search on a string array
    Friend Function BinarySearch(lst As String(), target As String) As Integer
        Dim First As Integer = 0
        Dim Last As Integer = lst.Length - 1
        Dim Middle As Integer = Last / 2
        While First <= Last
            If lst(Middle) < target Then
                First = Middle + 1
            ElseIf lst(Middle) = target Then
                Return Middle
            Else
                Last = Middle - 1
            End If
            Middle = (First + Last) / 2
        End While
        Return -1
    End Function

    Dim dsw As New Stopwatch
    Friend Sub Wait(ms As Double, Optional NonBlocking As Boolean = True)
        If ms = 0 Then Exit Sub
        Dim ticks As Long = CLng(ms * 10000) ' 1ms-10K ticks
        dsw.Restart()
        Do
            If NonBlocking Then Application.DoEvents()
        Loop While dsw.ElapsedTicks <= ticks
        dsw.Stop()
    End Sub

    ' Find the maximum number of hits per second in the log
    Friend Function FindPeakHits(Optional PeriodSecs As Integer = 1) As Integer
        Dim Hits As Integer = 1
        Dim MaxHits As Integer = 0
        Dim FromTime As DateTime = dtParse.Rows(0)("StartTime")
        For i As Integer = 1 To dtParse.Rows.Count - 1
            If DateDiff(DateInterval.Second, FromTime, dtParse.Rows(i)("StartTime")) < PeriodSecs Then
                Hits += 1
            Else
                If Hits > MaxHits Then MaxHits = Hits
                FromTime = dtParse.Rows(i)("StartTime")
                Hits = 1
            End If
        Next
        Return MaxHits
    End Function

#Region "Cursor Hot Spot"

    Private Structure IconInfo
        Public fIcon As Boolean
        Public xHotspot As Integer
        Public yHotspot As Integer
        Public hbmMask As IntPtr
        Public hbmColor As IntPtr
    End Structure

    Private Declare Function GetIconInfo Lib "user32.dll" (hIcon As IntPtr, ByRef pIconInfo As IconInfo) As Boolean
    Private Declare Function CreateIconIndirect Lib "user32.dll" (ByRef icon As IconInfo) As IntPtr

    ' Create a cursor from a bitmap with the specified hot spot
    Public Function CreateCursorWithHotspot(bmp As System.Drawing.Bitmap, xHotSpot As Integer, yHotSpot As Integer) As Cursor
        Dim ptr As IntPtr = bmp.GetHicon
        Dim tmp As IconInfo = New IconInfo()
        GetIconInfo(ptr, tmp)
        tmp.xHotspot = xHotSpot
        tmp.yHotspot = yHotSpot
        tmp.fIcon = False
        ptr = CreateIconIndirect(tmp)
        Return New Cursor(ptr)
    End Function

#End Region

    Private Sub Cache()
        ' Load cached copy if availble
        'Dim sMeta As String
        'Dim sXML As String
        'Dim info As FileInfo = New FileInfo(LogFile)
        'Dim CachePath As String = My.Settings.LastLogPath + "EQACache"
        'Dim CacheFile As String = CachePath + "\" + My.Settings.LastLogFile
        'Dim CacheMeta As String = CachePath + "\" + My.Settings.LastLogFile + ".dat"
        'If File.Exists(CacheMeta) AndAlso File.Exists(CacheFile) Then 'AndAlso Convert.ToDateTime(MetaParts(1)) = info.LastWriteTime
        '    sMeta = File.ReadAllText(CacheMeta)
        '    Dim MetaParts As String() = sMeta.Split("|")
        '    If MetaParts.Length = 3 AndAlso DateDiff(DateInterval.Second, Convert.ToDateTime(MetaParts(1)), info.LastWriteTime) = 0 AndAlso Val(MetaParts(2)) = info.Length Then
        '        ssStatus.Text = "Loading from cache..."
        '        Application.DoEvents()
        '        sXML = File.ReadAllText(CacheFile)
        '        dtParse = db.XmlToDatatable(sXML)
        '        Exit Sub
        '    End If
        'End If

        ' Write parsed table to cache folder
        'ssStatus.Text = "Caching parse..."
        'Application.DoEvents()
        'If Not Directory.Exists(CachePath) Then Directory.CreateDirectory(CachePath)
        'Dim sw As New StreamWriter(CacheMeta, False)
        'sMeta = LogFile + "|" + info.LastWriteTime.ToString + "|" + info.Length.ToString
        'sw.Write(sMeta)
        'sw.Close()
        'sXML = db.XmlFromDatatable(dtParse)
        'File.WriteAllText(CacheFile, sXML)
    End Sub

End Module
