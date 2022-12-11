Public Class frmAbout

    Private Sub frmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        lblRefresh.Cursor = crPress
    End Sub

    Private Sub lblRefresh_Click(sender As Object, e As EventArgs) Handles lblRefresh.Click
        Me.Close()
    End Sub

End Class