<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtQuietTime = New System.Windows.Forms.TextBox()
        Me.txtMinAttacks = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(27, 17)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(58, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Quiet Time"
        '
        'txtQuietTime
        '
        Me.txtQuietTime.Location = New System.Drawing.Point(30, 33)
        Me.txtQuietTime.MaxLength = 4
        Me.txtQuietTime.Name = "txtQuietTime"
        Me.txtQuietTime.Size = New System.Drawing.Size(119, 20)
        Me.txtQuietTime.TabIndex = 0
        '
        'txtMinAttacks
        '
        Me.txtMinAttacks.Location = New System.Drawing.Point(30, 116)
        Me.txtMinAttacks.MaxLength = 3
        Me.txtMinAttacks.Name = "txtMinAttacks"
        Me.txtMinAttacks.Size = New System.Drawing.Size(119, 20)
        Me.txtMinAttacks.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(27, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 3
        Me.Label2.Text = "Minimum Attacks"
        '
        'Label4
        '
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.Location = New System.Drawing.Point(27, 139)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(283, 36)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "The minimum number of attacks to constitute an encounter."
        '
        'Label3
        '
        Me.Label3.ForeColor = System.Drawing.Color.Blue
        Me.Label3.Location = New System.Drawing.Point(27, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(283, 36)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "The time in seconds with no attacks before starting a new enounter."
        '
        'frmOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(337, 188)
        Me.Controls.Add(Me.txtQuietTime)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtMinAttacks)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmOptions"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Encounter Configuration"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents txtQuietTime As TextBox
    Friend WithEvents txtMinAttacks As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
End Class
