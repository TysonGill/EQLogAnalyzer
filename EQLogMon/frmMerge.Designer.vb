<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMerge
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblMergeFile = New System.Windows.Forms.Label()
        Me.lblCreatedDate = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblUpdatedDate = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.lblTimeZone = New System.Windows.Forms.Label()
        Me.txtHourAdjustment = New System.Windows.Forms.TextBox()
        Me.txtLagAllowance = New System.Windows.Forms.TextBox()
        Me.lblLagAllowance = New System.Windows.Forms.Label()
        Me.lblAnalyze = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.pg = New ProGrid.ProGrid()
        Me.spltMerge = New System.Windows.Forms.SplitContainer()
        Me.LblBaseCreatedDate = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.lblBaseFile = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.lblBaseUpdateDate = New System.Windows.Forms.Label()
        Me.ss = New System.Windows.Forms.StatusStrip()
        Me.ssStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.btnAnalyze = New System.Windows.Forms.Button()
        CType(Me.spltMerge, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.spltMerge.Panel1.SuspendLayout()
        Me.spltMerge.Panel2.SuspendLayout()
        Me.spltMerge.SuspendLayout()
        Me.ss.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(278, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(56, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Merge File"
        '
        'lblMergeFile
        '
        Me.lblMergeFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblMergeFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMergeFile.Location = New System.Drawing.Point(279, 42)
        Me.lblMergeFile.Name = "lblMergeFile"
        Me.lblMergeFile.Size = New System.Drawing.Size(230, 20)
        Me.lblMergeFile.TabIndex = 1
        Me.lblMergeFile.Text = "filename"
        Me.lblMergeFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCreatedDate
        '
        Me.lblCreatedDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblCreatedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCreatedDate.Location = New System.Drawing.Point(279, 96)
        Me.lblCreatedDate.Name = "lblCreatedDate"
        Me.lblCreatedDate.Size = New System.Drawing.Size(230, 20)
        Me.lblCreatedDate.TabIndex = 3
        Me.lblCreatedDate.Text = "filename"
        Me.lblCreatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(278, 80)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(57, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Created At"
        '
        'lblUpdatedDate
        '
        Me.lblUpdatedDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblUpdatedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblUpdatedDate.Location = New System.Drawing.Point(279, 148)
        Me.lblUpdatedDate.Name = "lblUpdatedDate"
        Me.lblUpdatedDate.Size = New System.Drawing.Size(230, 20)
        Me.lblUpdatedDate.TabIndex = 5
        Me.lblUpdatedDate.Text = "filename"
        Me.lblUpdatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(278, 130)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(65, 13)
        Me.Label5.TabIndex = 4
        Me.Label5.Text = "Last Update"
        '
        'lblTimeZone
        '
        Me.lblTimeZone.AutoSize = True
        Me.lblTimeZone.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblTimeZone.Location = New System.Drawing.Point(544, 26)
        Me.lblTimeZone.Name = "lblTimeZone"
        Me.lblTimeZone.Size = New System.Drawing.Size(113, 13)
        Me.lblTimeZone.TabIndex = 6
        Me.lblTimeZone.Text = "Time Zone Adjustment"
        '
        'txtHourAdjustment
        '
        Me.txtHourAdjustment.Location = New System.Drawing.Point(547, 42)
        Me.txtHourAdjustment.MaxLength = 2
        Me.txtHourAdjustment.Name = "txtHourAdjustment"
        Me.txtHourAdjustment.Size = New System.Drawing.Size(114, 20)
        Me.txtHourAdjustment.TabIndex = 1
        Me.txtHourAdjustment.Text = "0"
        '
        'txtLagAllowance
        '
        Me.txtLagAllowance.Location = New System.Drawing.Point(547, 96)
        Me.txtLagAllowance.MaxLength = 2
        Me.txtLagAllowance.Name = "txtLagAllowance"
        Me.txtLagAllowance.Size = New System.Drawing.Size(114, 20)
        Me.txtLagAllowance.TabIndex = 2
        Me.txtLagAllowance.Text = "4"
        '
        'lblLagAllowance
        '
        Me.lblLagAllowance.AutoSize = True
        Me.lblLagAllowance.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.lblLagAllowance.Location = New System.Drawing.Point(544, 80)
        Me.lblLagAllowance.Name = "lblLagAllowance"
        Me.lblLagAllowance.Size = New System.Drawing.Size(77, 13)
        Me.lblLagAllowance.TabIndex = 8
        Me.lblLagAllowance.Text = "Lag Allowance"
        '
        'lblAnalyze
        '
        Me.lblAnalyze.Location = New System.Drawing.Point(547, 130)
        Me.lblAnalyze.Name = "lblAnalyze"
        Me.lblAnalyze.Size = New System.Drawing.Size(114, 38)
        Me.lblAnalyze.TabIndex = 3
        Me.lblAnalyze.Text = "Analyze"
        Me.lblAnalyze.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(668, 46)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(33, 13)
        Me.Label6.TabIndex = 11
        Me.Label6.Text = "hours"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(668, 100)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(47, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "seconds"
        '
        'pg
        '
        Me.pg.AccessibleRole = Nothing
        Me.pg.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.pg.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.pg.AutoSizeMode = Nothing
        Me.pg.BackgroundImageLayout = Nothing
        Me.pg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.pg.CausesValidation = False
        Me.pg.CellTextMaxLength = "200"
        Me.pg.ContextMenuStrip = Nothing
        Me.pg.Cursor = System.Windows.Forms.Cursors.Default
        Me.pg.DefaultDateFormat = "M/d/yyyy"
        Me.pg.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pg.EnableAddButton = True
        Me.pg.EnableColorAdjustment = False
        Me.pg.EnableDeleteButton = True
        Me.pg.EnableEditHighlighting = True
        Me.pg.EnableEditing = False
        Me.pg.EnableFiltering = True
        Me.pg.EnableFullRowSelection = False
        Me.pg.EnableIdentityHide = True
        Me.pg.EnableInvalidCellHandling = ProGrid.ProGrid.ProGridInvalidMethods.MarkAndShowTip
        Me.pg.EnableOptionsButton = True
        Me.pg.EnableOptionsCopy = True
        Me.pg.EnableOptionsExport = True
        Me.pg.EnableOptionsFind = True
        Me.pg.EnableOptionsPrint = True
        Me.pg.EnableOptionsSort = True
        Me.pg.EnableRefreshButton = True
        Me.pg.LicenseFolder = ""
        Me.pg.Location = New System.Drawing.Point(0, 0)
        Me.pg.Name = "pg"
        Me.pg.PrintTitle = "ProGrid Report"
        Me.pg.Size = New System.Drawing.Size(734, 325)
        Me.pg.TabIndex = 13
        Me.pg.UseWaitCursor = Nothing
        '
        'spltMerge
        '
        Me.spltMerge.Dock = System.Windows.Forms.DockStyle.Fill
        Me.spltMerge.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.spltMerge.IsSplitterFixed = True
        Me.spltMerge.Location = New System.Drawing.Point(0, 0)
        Me.spltMerge.Name = "spltMerge"
        Me.spltMerge.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'spltMerge.Panel1
        '
        Me.spltMerge.Panel1.Controls.Add(Me.LblBaseCreatedDate)
        Me.spltMerge.Panel1.Controls.Add(Me.Label9)
        Me.spltMerge.Panel1.Controls.Add(Me.lblBaseFile)
        Me.spltMerge.Panel1.Controls.Add(Me.Label11)
        Me.spltMerge.Panel1.Controls.Add(Me.Label12)
        Me.spltMerge.Panel1.Controls.Add(Me.lblBaseUpdateDate)
        Me.spltMerge.Panel1.Controls.Add(Me.lblCreatedDate)
        Me.spltMerge.Panel1.Controls.Add(Me.Label1)
        Me.spltMerge.Panel1.Controls.Add(Me.lblAnalyze)
        Me.spltMerge.Panel1.Controls.Add(Me.Label7)
        Me.spltMerge.Panel1.Controls.Add(Me.lblMergeFile)
        Me.spltMerge.Panel1.Controls.Add(Me.Label6)
        Me.spltMerge.Panel1.Controls.Add(Me.txtLagAllowance)
        Me.spltMerge.Panel1.Controls.Add(Me.lblLagAllowance)
        Me.spltMerge.Panel1.Controls.Add(Me.Label3)
        Me.spltMerge.Panel1.Controls.Add(Me.Label5)
        Me.spltMerge.Panel1.Controls.Add(Me.lblUpdatedDate)
        Me.spltMerge.Panel1.Controls.Add(Me.txtHourAdjustment)
        Me.spltMerge.Panel1.Controls.Add(Me.lblTimeZone)
        '
        'spltMerge.Panel2
        '
        Me.spltMerge.Panel2.Controls.Add(Me.pg)
        Me.spltMerge.Size = New System.Drawing.Size(734, 519)
        Me.spltMerge.SplitterDistance = 190
        Me.spltMerge.TabIndex = 14
        '
        'LblBaseCreatedDate
        '
        Me.LblBaseCreatedDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.LblBaseCreatedDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblBaseCreatedDate.Location = New System.Drawing.Point(26, 96)
        Me.LblBaseCreatedDate.Name = "LblBaseCreatedDate"
        Me.LblBaseCreatedDate.Size = New System.Drawing.Size(206, 20)
        Me.LblBaseCreatedDate.TabIndex = 16
        Me.LblBaseCreatedDate.Text = "filename"
        Me.LblBaseCreatedDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label9.Location = New System.Drawing.Point(25, 26)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(50, 13)
        Me.Label9.TabIndex = 13
        Me.Label9.Text = "Base File"
        '
        'lblBaseFile
        '
        Me.lblBaseFile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBaseFile.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseFile.Location = New System.Drawing.Point(26, 42)
        Me.lblBaseFile.Name = "lblBaseFile"
        Me.lblBaseFile.Size = New System.Drawing.Size(230, 20)
        Me.lblBaseFile.TabIndex = 14
        Me.lblBaseFile.Text = "filename"
        Me.lblBaseFile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label11.Location = New System.Drawing.Point(25, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(57, 13)
        Me.Label11.TabIndex = 15
        Me.Label11.Text = "Created At"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.ForeColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Label12.Location = New System.Drawing.Point(25, 130)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(65, 13)
        Me.Label12.TabIndex = 17
        Me.Label12.Text = "Last Update"
        '
        'lblBaseUpdateDate
        '
        Me.lblBaseUpdateDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.lblBaseUpdateDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblBaseUpdateDate.Location = New System.Drawing.Point(26, 148)
        Me.lblBaseUpdateDate.Name = "lblBaseUpdateDate"
        Me.lblBaseUpdateDate.Size = New System.Drawing.Size(206, 20)
        Me.lblBaseUpdateDate.TabIndex = 18
        Me.lblBaseUpdateDate.Text = "filename"
        Me.lblBaseUpdateDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ss
        '
        Me.ss.Location = New System.Drawing.Point(0, 519)
        Me.ss.Name = "ss"
        Me.ss.ShowItemToolTips = True
        Me.ss.Size = New System.Drawing.Size(734, 22)
        Me.ss.TabIndex = 16
        Me.ss.Text = "StatusStrip1"
        '
        'ssStatus
        '
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(719, 17)
        Me.ssStatus.Spring = True
        Me.ssStatus.Text = "Ready"
        Me.ssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnAnalyze
        '
        Me.btnAnalyze.Location = New System.Drawing.Point(547, 130)
        Me.btnAnalyze.Name = "btnAnalyze"
        Me.btnAnalyze.Size = New System.Drawing.Size(114, 38)
        Me.btnAnalyze.TabIndex = 3
        Me.btnAnalyze.Text = "Analyze"
        Me.btnAnalyze.UseVisualStyleBackColor = True
        '
        'frmMerge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(734, 541)
        Me.Controls.Add(Me.spltMerge)
        Me.Controls.Add(Me.ss)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(750, 375)
        Me.Name = "frmMerge"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Log File Merge"
        Me.spltMerge.Panel1.ResumeLayout(False)
        Me.spltMerge.Panel1.PerformLayout()
        Me.spltMerge.Panel2.ResumeLayout(False)
        CType(Me.spltMerge, System.ComponentModel.ISupportInitialize).EndInit()
        Me.spltMerge.ResumeLayout(False)
        Me.ss.ResumeLayout(False)
        Me.ss.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents lblMergeFile As Label
    Friend WithEvents lblCreatedDate As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblUpdatedDate As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblTimeZone As Label
    Friend WithEvents txtHourAdjustment As TextBox
    Friend WithEvents txtLagAllowance As TextBox
    Friend WithEvents lblLagAllowance As Label
    Friend WithEvents lblAnalyze As Button
    Friend WithEvents Label6 As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents pg As ProGrid.ProGrid
    Friend WithEvents spltMerge As SplitContainer
    Friend WithEvents ss As StatusStrip
    Friend WithEvents ssStatus As ToolStripStatusLabel
    Friend WithEvents LblBaseCreatedDate As Label
    Friend WithEvents Label9 As Label
    Friend WithEvents lblBaseFile As Label
    Friend WithEvents Label11 As Label
    Friend WithEvents Label12 As Label
    Friend WithEvents lblBaseUpdateDate As Label
    Friend WithEvents btnAnalyze As Button
End Class
