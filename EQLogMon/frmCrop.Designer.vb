<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCrop
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
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblBaseFile = New System.Windows.Forms.Label()
        Me.txtNew = New System.Windows.Forms.TextBox()
        Me.lblTimeZone = New System.Windows.Forms.Label()
        Me.lblCrop = New System.Windows.Forms.Label()
        Me.lblPrefix = New System.Windows.Forms.Label()
        Me.lblSuffix = New System.Windows.Forms.Label()
        Me.lblCropRange = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(29, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(86, 13)
        Me.Label6.TabIndex = 16
        Me.Label6.Text = "Current Filename"
        '
        'lblBaseFile
        '
        Me.lblBaseFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBaseFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseFile.Location = New System.Drawing.Point(29, 80)
        Me.lblBaseFile.Name = "lblBaseFile"
        Me.lblBaseFile.Size = New System.Drawing.Size(371, 20)
        Me.lblBaseFile.TabIndex = 17
        Me.lblBaseFile.Text = "filename"
        Me.lblBaseFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNew
        '
        Me.txtNew.Location = New System.Drawing.Point(177, 138)
        Me.txtNew.MaxLength = 16
        Me.txtNew.Name = "txtNew"
        Me.txtNew.Size = New System.Drawing.Size(114, 20)
        Me.txtNew.TabIndex = 1
        '
        'lblTimeZone
        '
        Me.lblTimeZone.AutoSize = True
        Me.lblTimeZone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTimeZone.Location = New System.Drawing.Point(26, 122)
        Me.lblTimeZone.Name = "lblTimeZone"
        Me.lblTimeZone.Size = New System.Drawing.Size(74, 13)
        Me.lblTimeZone.TabIndex = 19
        Me.lblTimeZone.Text = "New Filename"
        '
        'lblCrop
        '
        Me.lblCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCrop.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrop.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCrop.Location = New System.Drawing.Point(326, 178)
        Me.lblCrop.Name = "lblCrop"
        Me.lblCrop.Size = New System.Drawing.Size(74, 25)
        Me.lblCrop.TabIndex = 22
        Me.lblCrop.Text = "Save"
        Me.lblCrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblPrefix
        '
        Me.lblPrefix.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblPrefix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPrefix.Location = New System.Drawing.Point(29, 138)
        Me.lblPrefix.Name = "lblPrefix"
        Me.lblPrefix.Size = New System.Drawing.Size(142, 20)
        Me.lblPrefix.TabIndex = 23
        Me.lblPrefix.Text = "filename"
        Me.lblPrefix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblSuffix
        '
        Me.lblSuffix.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblSuffix.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSuffix.Location = New System.Drawing.Point(297, 138)
        Me.lblSuffix.Name = "lblSuffix"
        Me.lblSuffix.Size = New System.Drawing.Size(103, 20)
        Me.lblSuffix.TabIndex = 24
        Me.lblSuffix.Text = ".txt"
        Me.lblSuffix.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCropRange
        '
        Me.lblCropRange.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCropRange.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCropRange.Location = New System.Drawing.Point(29, 28)
        Me.lblCropRange.Name = "lblCropRange"
        Me.lblCropRange.Size = New System.Drawing.Size(371, 20)
        Me.lblCropRange.TabIndex = 26
        Me.lblCropRange.Text = "filename"
        Me.lblCropRange.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(29, 12)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Crop Range"
        '
        'frmFileSelect
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(424, 223)
        Me.Controls.Add(Me.lblCropRange)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.lblSuffix)
        Me.Controls.Add(Me.lblPrefix)
        Me.Controls.Add(Me.lblCrop)
        Me.Controls.Add(Me.txtNew)
        Me.Controls.Add(Me.lblTimeZone)
        Me.Controls.Add(Me.lblBaseFile)
        Me.Controls.Add(Me.Label6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmFileSelect"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Crop Log File"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label6 As Label
    Friend WithEvents lblBaseFile As Label
    Friend WithEvents txtNew As TextBox
    Friend WithEvents lblTimeZone As Label
    Friend WithEvents lblCrop As Label
    Friend WithEvents lblPrefix As Label
    Friend WithEvents lblSuffix As Label
    Friend WithEvents lblCropRange As Label
    Friend WithEvents Label2 As Label
End Class
