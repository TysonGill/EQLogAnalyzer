Public Class frmOptions

    Public IsChanged As Boolean = False

    Private Sub frmOptions_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtQuietTime.Text = ENC_GAP_SECS.ToString
        txtMinAttacks.Text = ENC_MIN_ROWS.ToString
        txtQuietTime.Tag = txtQuietTime.Text
        txtMinAttacks.Tag = txtMinAttacks.Text
    End Sub

    Private Sub frmOptions_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        ENC_GAP_SECS = Val(txtQuietTime.Text)
        ENC_MIN_ROWS = Val(txtMinAttacks.Text)
        IsChanged = ENC_GAP_SECS <> txtQuietTime.Tag OrElse ENC_MIN_ROWS <> txtMinAttacks.Tag
    End Sub

    Private Sub txtQuietTime_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtQuietTime.KeyPress, txtMinAttacks.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) AndAlso Not Char.IsNumber(e.KeyChar) Then e.Handled = True
    End Sub

End Class