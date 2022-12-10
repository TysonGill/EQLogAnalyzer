<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChart
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                bBoss.Dispose()
                bBuff.Dispose()
                bDPS.Dispose()
                bTL.Dispose()
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
        Me.picBosses = New System.Windows.Forms.PictureBox()
        Me.picTL = New System.Windows.Forms.PictureBox()
        Me.picBuffs = New System.Windows.Forms.PictureBox()
        Me.pnlTop = New System.Windows.Forms.Panel()
        Me.lblCrop = New System.Windows.Forms.Label()
        Me.cboPlayers = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.chkPersonal = New System.Windows.Forms.CheckBox()
        Me.chkGroup = New System.Windows.Forms.CheckBox()
        Me.chkRaid = New System.Windows.Forms.CheckBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPop = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.numBossUptime = New System.Windows.Forms.NumericUpDown()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.numDPS = New System.Windows.Forms.NumericUpDown()
        Me.picDPS = New System.Windows.Forms.PictureBox()
        CType(Me.picBosses, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picTL, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picBuffs, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTop.SuspendLayout()
        CType(Me.numBossUptime, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.numDPS, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picDPS, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picBosses
        '
        Me.picBosses.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.picBosses.Location = New System.Drawing.Point(0, 396)
        Me.picBosses.Name = "picBosses"
        Me.picBosses.Size = New System.Drawing.Size(919, 125)
        Me.picBosses.TabIndex = 0
        Me.picBosses.TabStop = False
        '
        'picTL
        '
        Me.picTL.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picTL.Location = New System.Drawing.Point(0, 349)
        Me.picTL.Name = "picTL"
        Me.picTL.Size = New System.Drawing.Size(919, 41)
        Me.picTL.TabIndex = 3
        Me.picTL.TabStop = False
        '
        'picBuffs
        '
        Me.picBuffs.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picBuffs.Location = New System.Drawing.Point(0, 87)
        Me.picBuffs.Name = "picBuffs"
        Me.picBuffs.Size = New System.Drawing.Size(919, 137)
        Me.picBuffs.TabIndex = 7
        Me.picBuffs.TabStop = False
        '
        'pnlTop
        '
        Me.pnlTop.BackColor = System.Drawing.SystemColors.Control
        Me.pnlTop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pnlTop.Controls.Add(Me.lblCrop)
        Me.pnlTop.Controls.Add(Me.cboPlayers)
        Me.pnlTop.Controls.Add(Me.Label7)
        Me.pnlTop.Controls.Add(Me.chkPersonal)
        Me.pnlTop.Controls.Add(Me.chkGroup)
        Me.pnlTop.Controls.Add(Me.chkRaid)
        Me.pnlTop.Controls.Add(Me.Label6)
        Me.pnlTop.Controls.Add(Me.txtPop)
        Me.pnlTop.Controls.Add(Me.Label5)
        Me.pnlTop.Controls.Add(Me.Label3)
        Me.pnlTop.Controls.Add(Me.Label4)
        Me.pnlTop.Controls.Add(Me.numBossUptime)
        Me.pnlTop.Controls.Add(Me.Label2)
        Me.pnlTop.Controls.Add(Me.Label1)
        Me.pnlTop.Controls.Add(Me.numDPS)
        Me.pnlTop.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlTop.Location = New System.Drawing.Point(0, 0)
        Me.pnlTop.Name = "pnlTop"
        Me.pnlTop.Size = New System.Drawing.Size(919, 81)
        Me.pnlTop.TabIndex = 10
        '
        'lblCrop
        '
        Me.lblCrop.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCrop.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCrop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCrop.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCrop.Location = New System.Drawing.Point(832, 26)
        Me.lblCrop.Name = "lblCrop"
        Me.lblCrop.Size = New System.Drawing.Size(74, 25)
        Me.lblCrop.TabIndex = 21
        Me.lblCrop.Text = "Crop..."
        Me.lblCrop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboPlayers
        '
        Me.cboPlayers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPlayers.FormattingEnabled = True
        Me.cboPlayers.Location = New System.Drawing.Point(132, 26)
        Me.cboPlayers.Name = "cboPlayers"
        Me.cboPlayers.Size = New System.Drawing.Size(191, 21)
        Me.cboPlayers.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label7.Location = New System.Drawing.Point(129, 6)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(66, 13)
        Me.Label7.TabIndex = 19
        Me.Label7.Text = "DPS Players"
        '
        'chkPersonal
        '
        Me.chkPersonal.AutoSize = True
        Me.chkPersonal.ForeColor = System.Drawing.Color.Sienna
        Me.chkPersonal.Location = New System.Drawing.Point(14, 57)
        Me.chkPersonal.Name = "chkPersonal"
        Me.chkPersonal.Size = New System.Drawing.Size(67, 17)
        Me.chkPersonal.TabIndex = 3
        Me.chkPersonal.Text = "Personal"
        Me.chkPersonal.UseVisualStyleBackColor = True
        '
        'chkGroup
        '
        Me.chkGroup.AutoSize = True
        Me.chkGroup.Checked = True
        Me.chkGroup.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkGroup.ForeColor = System.Drawing.Color.Green
        Me.chkGroup.Location = New System.Drawing.Point(14, 40)
        Me.chkGroup.Name = "chkGroup"
        Me.chkGroup.Size = New System.Drawing.Size(55, 17)
        Me.chkGroup.TabIndex = 2
        Me.chkGroup.Text = "Group"
        Me.chkGroup.UseVisualStyleBackColor = True
        '
        'chkRaid
        '
        Me.chkRaid.AutoSize = True
        Me.chkRaid.Checked = True
        Me.chkRaid.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkRaid.ForeColor = System.Drawing.Color.Red
        Me.chkRaid.Location = New System.Drawing.Point(14, 23)
        Me.chkRaid.Name = "chkRaid"
        Me.chkRaid.Size = New System.Drawing.Size(48, 17)
        Me.chkRaid.TabIndex = 1
        Me.chkRaid.Text = "Raid"
        Me.chkRaid.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label6.Location = New System.Drawing.Point(11, 6)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(56, 13)
        Me.Label6.TabIndex = 15
        Me.Label6.Text = "Buff Filters"
        '
        'txtPop
        '
        Me.txtPop.AutoSize = True
        Me.txtPop.BackColor = System.Drawing.Color.White
        Me.txtPop.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtPop.Location = New System.Drawing.Point(647, 57)
        Me.txtPop.Name = "txtPop"
        Me.txtPop.Size = New System.Drawing.Size(30, 15)
        Me.txtPop.TabIndex = 14
        Me.txtPop.Text = "Map"
        Me.txtPop.Visible = False
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(683, 61)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(223, 13)
        Me.Label5.TabIndex = 13
        Me.Label5.Text = "Use left-drag to zoom and right-click to restore"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label3.Location = New System.Drawing.Point(554, 30)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(29, 13)
        Me.Label3.TabIndex = 12
        Me.Label3.Text = "secs"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label4.Location = New System.Drawing.Point(489, 6)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(86, 13)
        Me.Label4.TabIndex = 11
        Me.Label4.Text = "Boss Min Uptime"
        '
        'numBossUptime
        '
        Me.numBossUptime.Location = New System.Drawing.Point(492, 26)
        Me.numBossUptime.Maximum = New Decimal(New Integer() {120, 0, 0, 0})
        Me.numBossUptime.Name = "numBossUptime"
        Me.numBossUptime.Size = New System.Drawing.Size(56, 20)
        Me.numBossUptime.TabIndex = 2
        Me.numBossUptime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numBossUptime.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        Me.numBossUptime.Value = New Decimal(New Integer() {10, 0, 0, 0})
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label2.Location = New System.Drawing.Point(431, 30)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "secs"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.DarkBlue
        Me.Label1.Location = New System.Drawing.Point(366, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(89, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "DPS Aggregation"
        '
        'numDPS
        '
        Me.numDPS.Location = New System.Drawing.Point(369, 26)
        Me.numDPS.Maximum = New Decimal(New Integer() {18, 0, 0, 0})
        Me.numDPS.Minimum = New Decimal(New Integer() {1, 0, 0, 0})
        Me.numDPS.Name = "numDPS"
        Me.numDPS.Size = New System.Drawing.Size(56, 20)
        Me.numDPS.TabIndex = 1
        Me.numDPS.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.numDPS.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left
        Me.numDPS.Value = New Decimal(New Integer() {4, 0, 0, 0})
        '
        'picDPS
        '
        Me.picDPS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.picDPS.Location = New System.Drawing.Point(0, 230)
        Me.picDPS.Name = "picDPS"
        Me.picDPS.Size = New System.Drawing.Size(919, 115)
        Me.picDPS.TabIndex = 11
        Me.picDPS.TabStop = False
        '
        'frmGraphics
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(919, 521)
        Me.Controls.Add(Me.picDPS)
        Me.Controls.Add(Me.pnlTop)
        Me.Controls.Add(Me.picTL)
        Me.Controls.Add(Me.picBuffs)
        Me.Controls.Add(Me.picBosses)
        Me.MinimumSize = New System.Drawing.Size(935, 560)
        Me.Name = "frmGraphics"
        Me.ShowIcon = False
        Me.Text = "Encounter Chart"
        CType(Me.picBosses, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picTL, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picBuffs, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTop.ResumeLayout(False)
        Me.pnlTop.PerformLayout()
        CType(Me.numBossUptime, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.numDPS, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picDPS, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents picBosses As PictureBox
    Friend WithEvents picTL As PictureBox
    Friend WithEvents picBuffs As PictureBox
    Friend WithEvents pnlTop As Panel
    Friend WithEvents txtPop As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents numBossUptime As NumericUpDown
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents numDPS As NumericUpDown
    Friend WithEvents picDPS As PictureBox
    Friend WithEvents cboPlayers As ComboBox
    Friend WithEvents Label7 As Label
    Friend WithEvents chkPersonal As CheckBox
    Friend WithEvents chkGroup As CheckBox
    Friend WithEvents chkRaid As CheckBox
    Friend WithEvents Label6 As Label
    Friend WithEvents lblCrop As Label
End Class
