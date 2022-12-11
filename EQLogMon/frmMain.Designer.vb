<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Me.components = New System.ComponentModel.Container()
        Me.pg = New ProGrid.ProGrid()
        Me.ss = New System.Windows.Forms.StatusStrip()
        Me.ssNonMelee = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssPets = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssServerLog = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssLogFile = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOpenLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuMerge = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem2 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuClearLog = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuServer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUploadParse = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuToggleLogging = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuUpload = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuServerReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ViewToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuOptions = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPetOwners = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuIncludePets = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuNonMelee = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuFAQ = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripMenuItem5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem()
        Me.splitMain = New System.Windows.Forms.SplitContainer()
        Me.lblCharts = New System.Windows.Forms.Label()
        Me.lblRefresh = New System.Windows.Forms.Label()
        Me.tv = New System.Windows.Forms.TreeView()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblTop = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.tmrFlash = New System.Windows.Forms.Timer(Me.components)
        Me.ss.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.splitMain.Panel1.SuspendLayout()
        Me.splitMain.Panel2.SuspendLayout()
        Me.splitMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'pg
        '
        Me.pg.AccessibleRole = Nothing
        Me.pg.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
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
        Me.pg.Location = New System.Drawing.Point(2, 70)
        Me.pg.Name = "pg"
        Me.pg.PrintTitle = "ProGrid Report"
        Me.pg.Size = New System.Drawing.Size(558, 320)
        Me.pg.TabIndex = 4
        Me.pg.UseWaitCursor = Nothing
        '
        'ss
        '
        Me.ss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ssNonMelee, Me.ssPets, Me.ssLogFile, Me.ssServerLog, Me.ssStatus})
        Me.ss.Location = New System.Drawing.Point(0, 417)
        Me.ss.Name = "ss"
        Me.ss.ShowItemToolTips = True
        Me.ss.Size = New System.Drawing.Size(784, 24)
        Me.ss.TabIndex = 2
        Me.ss.Text = "StatusStrip1"
        '
        'ssNonMelee
        '
        Me.ssNonMelee.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ssNonMelee.Name = "ssNonMelee"
        Me.ssNonMelee.Size = New System.Drawing.Size(106, 19)
        Me.ssNonMelee.Text = "Credit Non-Melee"
        Me.ssNonMelee.ToolTipText = "Click to toggle whether to credit DS damage to tank"
        '
        'ssPets
        '
        Me.ssPets.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ssPets.Name = "ssPets"
        Me.ssPets.Size = New System.Drawing.Size(68, 19)
        Me.ssPets.Text = "Credit Pets"
        Me.ssPets.ToolTipText = "Click to toggle whether to credit pet damage to owner"
        '
        'ssServerLog
        '
        Me.ssServerLog.Name = "ssServerLog"
        Me.ssServerLog.Size = New System.Drawing.Size(106, 19)
        Me.ssServerLog.Text = "Server Logging Off"
        Me.ssServerLog.ToolTipText = "Realtime transfer to database using current log file (click to toggle)"
        '
        'ssLogFile
        '
        Me.ssLogFile.AutoToolTip = True
        Me.ssLogFile.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ssLogFile.Name = "ssLogFile"
        Me.ssLogFile.Size = New System.Drawing.Size(71, 19)
        Me.ssLogFile.Text = "No Log File"
        Me.ssLogFile.ToolTipText = "Log File (click to selec)"
        '
        'ssStatus
        '
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(418, 19)
        Me.ssStatus.Spring = True
        Me.ssStatus.Text = "Ready"
        Me.ssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.mnuServer, Me.ViewToolStripMenuItem, Me.AboutToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(784, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOpenLog, Me.mnuMerge, Me.ToolStripMenuItem2, Me.mnuClearLog, Me.ToolStripMenuItem3, Me.mnuExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(37, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'mnuOpenLog
        '
        Me.mnuOpenLog.Name = "mnuOpenLog"
        Me.mnuOpenLog.Size = New System.Drawing.Size(138, 22)
        Me.mnuOpenLog.Text = "&Log File..."
        '
        'mnuMerge
        '
        Me.mnuMerge.Name = "mnuMerge"
        Me.mnuMerge.Size = New System.Drawing.Size(138, 22)
        Me.mnuMerge.Text = "&Merge File..."
        '
        'ToolStripMenuItem2
        '
        Me.ToolStripMenuItem2.Name = "ToolStripMenuItem2"
        Me.ToolStripMenuItem2.Size = New System.Drawing.Size(135, 6)
        '
        'mnuClearLog
        '
        Me.mnuClearLog.Name = "mnuClearLog"
        Me.mnuClearLog.Size = New System.Drawing.Size(138, 22)
        Me.mnuClearLog.Text = "&Clear Log..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(135, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(138, 22)
        Me.mnuExit.Text = "E&xit"
        '
        'mnuServer
        '
        Me.mnuServer.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuUploadParse, Me.mnuToggleLogging, Me.mnuUpload, Me.mnuServerReport})
        Me.mnuServer.Name = "mnuServer"
        Me.mnuServer.Size = New System.Drawing.Size(51, 20)
        Me.mnuServer.Text = "&Server"
        '
        'mnuUploadParse
        '
        Me.mnuUploadParse.Name = "mnuUploadParse"
        Me.mnuUploadParse.Size = New System.Drawing.Size(172, 22)
        Me.mnuUploadParse.Text = "&Upload Parse..."
        '
        'mnuToggleLogging
        '
        Me.mnuToggleLogging.Name = "mnuToggleLogging"
        Me.mnuToggleLogging.Size = New System.Drawing.Size(172, 22)
        Me.mnuToggleLogging.Text = "&Real-time Logging"
        '
        'mnuUpload
        '
        Me.mnuUpload.Name = "mnuUpload"
        Me.mnuUpload.Size = New System.Drawing.Size(169, 6)
        '
        'mnuServerReport
        '
        Me.mnuServerReport.Name = "mnuServerReport"
        Me.mnuServerReport.Size = New System.Drawing.Size(172, 22)
        Me.mnuServerReport.Text = "Server &Report..."
        '
        'ViewToolStripMenuItem
        '
        Me.ViewToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuOptions, Me.ToolStripMenuItem4, Me.mnuPetOwners, Me.mnuIncludePets, Me.ToolStripMenuItem1, Me.mnuNonMelee})
        Me.ViewToolStripMenuItem.Name = "ViewToolStripMenuItem"
        Me.ViewToolStripMenuItem.Size = New System.Drawing.Size(72, 20)
        Me.ViewToolStripMenuItem.Text = "&Configure"
        '
        'mnuOptions
        '
        Me.mnuOptions.Name = "mnuOptions"
        Me.mnuOptions.Size = New System.Drawing.Size(216, 22)
        Me.mnuOptions.Text = "&Encounters..."
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(213, 6)
        '
        'mnuPetOwners
        '
        Me.mnuPetOwners.Name = "mnuPetOwners"
        Me.mnuPetOwners.Size = New System.Drawing.Size(216, 22)
        Me.mnuPetOwners.Text = "&Pet Owners..."
        '
        'mnuIncludePets
        '
        Me.mnuIncludePets.Name = "mnuIncludePets"
        Me.mnuIncludePets.Size = New System.Drawing.Size(216, 22)
        Me.mnuIncludePets.Text = "&Credit Pet Damage"
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(213, 6)
        '
        'mnuNonMelee
        '
        Me.mnuNonMelee.Name = "mnuNonMelee"
        Me.mnuNonMelee.Size = New System.Drawing.Size(216, 22)
        Me.mnuNonMelee.Text = "Credit &Non-Melee Damage"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFAQ, Me.ToolStripMenuItem5, Me.mnuAbout})
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(44, 20)
        Me.AboutToolStripMenuItem.Text = "&Help"
        '
        'mnuFAQ
        '
        Me.mnuFAQ.Name = "mnuFAQ"
        Me.mnuFAQ.Size = New System.Drawing.Size(116, 22)
        Me.mnuFAQ.Text = "&FAQ..."
        '
        'ToolStripMenuItem5
        '
        Me.ToolStripMenuItem5.Name = "ToolStripMenuItem5"
        Me.ToolStripMenuItem5.Size = New System.Drawing.Size(113, 6)
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(116, 22)
        Me.mnuAbout.Text = "&About..."
        '
        'splitMain
        '
        Me.splitMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1
        Me.splitMain.Location = New System.Drawing.Point(0, 24)
        Me.splitMain.Name = "splitMain"
        '
        'splitMain.Panel1
        '
        Me.splitMain.Panel1.Controls.Add(Me.lblCharts)
        Me.splitMain.Panel1.Controls.Add(Me.lblRefresh)
        Me.splitMain.Panel1.Controls.Add(Me.tv)
        Me.splitMain.Panel1MinSize = 120
        '
        'splitMain.Panel2
        '
        Me.splitMain.Panel2.Controls.Add(Me.txtSearch)
        Me.splitMain.Panel2.Controls.Add(Me.lblTop)
        Me.splitMain.Panel2.Controls.Add(Me.lblSearch)
        Me.splitMain.Panel2.Controls.Add(Me.pg)
        Me.splitMain.Panel2MinSize = 480
        Me.splitMain.Size = New System.Drawing.Size(784, 393)
        Me.splitMain.SplitterDistance = 220
        Me.splitMain.TabIndex = 4
        '
        'lblCharts
        '
        Me.lblCharts.BackColor = System.Drawing.Color.AliceBlue
        Me.lblCharts.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblCharts.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblCharts.Location = New System.Drawing.Point(107, 25)
        Me.lblCharts.Name = "lblCharts"
        Me.lblCharts.Size = New System.Drawing.Size(74, 25)
        Me.lblCharts.TabIndex = 2
        Me.lblCharts.Text = "Charts"
        Me.lblCharts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblRefresh
        '
        Me.lblRefresh.BackColor = System.Drawing.Color.AliceBlue
        Me.lblRefresh.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblRefresh.Cursor = System.Windows.Forms.Cursors.Arrow
        Me.lblRefresh.Location = New System.Drawing.Point(12, 25)
        Me.lblRefresh.Name = "lblRefresh"
        Me.lblRefresh.Size = New System.Drawing.Size(74, 25)
        Me.lblRefresh.TabIndex = 0
        Me.lblRefresh.Text = "Refresh"
        Me.lblRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tv
        '
        Me.tv.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tv.FullRowSelect = True
        Me.tv.HideSelection = False
        Me.tv.Location = New System.Drawing.Point(0, 70)
        Me.tv.Name = "tv"
        Me.tv.ShowLines = False
        Me.tv.ShowNodeToolTips = True
        Me.tv.Size = New System.Drawing.Size(220, 320)
        Me.tv.TabIndex = 1
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(9, 25)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(448, 20)
        Me.txtSearch.TabIndex = 2
        '
        'lblTop
        '
        Me.lblTop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTop.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblTop.Location = New System.Drawing.Point(6, 8)
        Me.lblTop.Name = "lblTop"
        Me.lblTop.Size = New System.Drawing.Size(462, 59)
        Me.lblTop.TabIndex = 5
        Me.lblTop.Text = "Instructions"
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSearch.Location = New System.Drawing.Point(474, 25)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(74, 19)
        Me.lblSearch.TabIndex = 3
        Me.lblSearch.Text = "Search"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'tmrFlash
        '
        Me.tmrFlash.Interval = 400
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 441)
        Me.Controls.Add(Me.splitMain)
        Me.Controls.Add(Me.ss)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MinimumSize = New System.Drawing.Size(800, 480)
        Me.Name = "frmMain"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "EQ Log Analyzer"
        Me.ss.ResumeLayout(False)
        Me.ss.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.splitMain.Panel1.ResumeLayout(False)
        Me.splitMain.Panel2.ResumeLayout(False)
        Me.splitMain.Panel2.PerformLayout()
        CType(Me.splitMain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.splitMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pg As ProGrid.ProGrid
    Friend WithEvents ss As StatusStrip
    Friend WithEvents ssStatus As ToolStripStatusLabel
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents FileToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuExit As ToolStripMenuItem
    Friend WithEvents ssLogFile As ToolStripStatusLabel
    Friend WithEvents ViewToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuNonMelee As ToolStripMenuItem
    Friend WithEvents splitMain As SplitContainer
    Friend WithEvents tv As TreeView
    Friend WithEvents lblRefresh As Label
    Friend WithEvents mnuOpenLog As ToolStripMenuItem
    Friend WithEvents lblSearch As Label
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents ToolStripMenuItem2 As ToolStripSeparator
    Friend WithEvents mnuOptions As ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents mnuAbout As ToolStripMenuItem
    Friend WithEvents lblTop As Label
    Friend WithEvents ssNonMelee As ToolStripStatusLabel
    Friend WithEvents mnuMerge As ToolStripMenuItem
    Friend WithEvents lblCharts As Label
    Friend WithEvents mnuClearLog As ToolStripMenuItem
    Friend WithEvents ssServerLog As ToolStripStatusLabel
    Friend WithEvents ToolStripMenuItem3 As ToolStripSeparator
    Friend WithEvents mnuPetOwners As ToolStripMenuItem
    Friend WithEvents mnuServer As ToolStripMenuItem
    Friend WithEvents mnuUploadParse As ToolStripMenuItem
    Friend WithEvents mnuToggleLogging As ToolStripMenuItem
    Friend WithEvents mnuUpload As ToolStripSeparator
    Friend WithEvents mnuServerReport As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As ToolStripSeparator
    Friend WithEvents mnuIncludePets As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As ToolStripSeparator
    Friend WithEvents ssPets As ToolStripStatusLabel
    Friend WithEvents mnuFAQ As ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem5 As ToolStripSeparator
    Friend WithEvents tmrFlash As Timer
End Class
