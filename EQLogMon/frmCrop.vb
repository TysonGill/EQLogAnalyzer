Imports System.IO
Imports Utility

Public Class frmCrop

    Public TimeFrom As DateTime
    Public TimeTo As DateTime

    Private Sub frmFileSelect_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            ' Assign cursors
            lblCrop.Cursor = crPress

            ' Initialize form
            lblCropRange.Text = TimeFrom.ToString + " - " + TimeTo.ToString
            lblBaseFile.Text = Path.GetFileName(LogFile)
            Dim parts As String() = lblBaseFile.Text.Split("_")
            lblPrefix.Text = parts(0) + "_" + parts(1) + "_"

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    ' Write out the file excluding the clipped messages
    Private Sub lblCrop_Click(sender As Object, e As EventArgs) Handles lblCrop.Click
        If txtNew.Text.Trim = "" Then Exit Sub

        ' Validate file
        Dim cropname As String = lblPrefix.Text + txtNew.Text + lblSuffix.Text
        Dim fullname As String = My.Settings.LastLogPath + cropname
        If File.Exists(fullname) Then
            If MessageBox.Show("The file " + cropname + " already exists in your log folder." + vbCrLf + "Do you want to overwrite it?", "Save Cropped File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> vbYes Then Exit Sub
            File.Delete(fullname)
        End If

        Try
            Cursor = Cursors.WaitCursor

            ' Write file
            Dim reader As StreamReader
            reader = My.Computer.FileSystem.OpenTextFileReader(LogFile)
            Dim croplog As TextWriter = New StreamWriter(fullname)
            Dim s As String
            Dim StartTime As DateTime
            Do
                ' Get the next line
                s = reader.ReadLine
                If s Is Nothing Then Exit Do

                ' Write line if in range
                StartTime = GetEventTime(s)
                If StartTime > TimeTo Then Exit Do
                If StartTime >= TimeFrom Then croplog.WriteLine(s)

            Loop
            reader.Close()
            croplog.Flush()
            croplog.Close()

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
            Me.Close()
        End Try
    End Sub

End Class