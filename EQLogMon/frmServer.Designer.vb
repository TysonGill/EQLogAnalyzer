<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmServer
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
        Me.pg = New ProGrid.ProGrid()
        Me.tv = New System.Windows.Forms.TreeView()
        Me.txtSearch = New System.Windows.Forms.TextBox()
        Me.lblTop = New System.Windows.Forms.Label()
        Me.lblSearch = New System.Windows.Forms.Label()
        Me.chkPets = New System.Windows.Forms.CheckBox()
        Me.ss = New System.Windows.Forms.StatusStrip()
        Me.ssLoggers = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ssStatus = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ss.SuspendLayout()
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
        Me.pg.Location = New System.Drawing.Point(185, 71)
        Me.pg.Name = "pg"
        Me.pg.PrintTitle = "ProGrid Report"
        Me.pg.Size = New System.Drawing.Size(475, 314)
        Me.pg.TabIndex = 5
        Me.pg.UseWaitCursor = Nothing
        '
        'tv
        '
        Me.tv.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.tv.FullRowSelect = True
        Me.tv.HideSelection = False
        Me.tv.Location = New System.Drawing.Point(0, 71)
        Me.tv.Name = "tv"
        Me.tv.ShowLines = False
        Me.tv.ShowNodeToolTips = True
        Me.tv.Size = New System.Drawing.Size(179, 314)
        Me.tv.TabIndex = 6
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.Location = New System.Drawing.Point(185, 35)
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Size = New System.Drawing.Size(320, 20)
        Me.txtSearch.TabIndex = 7
        '
        'lblTop
        '
        Me.lblTop.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblTop.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblTop.Location = New System.Drawing.Point(182, 15)
        Me.lblTop.Name = "lblTop"
        Me.lblTop.Size = New System.Drawing.Size(334, 17)
        Me.lblTop.TabIndex = 9
        Me.lblTop.Text = "Time Range"
        '
        'lblSearch
        '
        Me.lblSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSearch.BackColor = System.Drawing.Color.AliceBlue
        Me.lblSearch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.lblSearch.Cursor = System.Windows.Forms.Cursors.Hand
        Me.lblSearch.Location = New System.Drawing.Point(522, 36)
        Me.lblSearch.Name = "lblSearch"
        Me.lblSearch.Size = New System.Drawing.Size(74, 19)
        Me.lblSearch.TabIndex = 8
        Me.lblSearch.Text = "Search"
        Me.lblSearch.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'chkPets
        '
        Me.chkPets.AutoSize = True
        Me.chkPets.Location = New System.Drawing.Point(12, 35)
        Me.chkPets.Name = "chkPets"
        Me.chkPets.Size = New System.Drawing.Size(80, 17)
        Me.chkPets.TabIndex = 10
        Me.chkPets.Text = "Merge Pets"
        Me.chkPets.UseVisualStyleBackColor = True
        '
        'ss
        '
        Me.ss.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ssLoggers, Me.ssStatus})
        Me.ss.Location = New System.Drawing.Point(0, 388)
        Me.ss.Name = "ss"
        Me.ss.ShowItemToolTips = True
        Me.ss.Size = New System.Drawing.Size(662, 24)
        Me.ss.TabIndex = 11
        Me.ss.Text = "StatusStrip1"
        '
        'ssLoggers
        '
        Me.ssLoggers.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ssLoggers.Name = "ssLoggers"
        Me.ssLoggers.Size = New System.Drawing.Size(90, 19)
        Me.ssLoggers.Text = "Non-Melee On"
        Me.ssLoggers.ToolTipText = "Non-Melee Damage (click to toggle)"
        '
        'ssStatus
        '
        Me.ssStatus.Name = "ssStatus"
        Me.ssStatus.Size = New System.Drawing.Size(557, 19)
        Me.ssStatus.Spring = True
        Me.ssStatus.Text = "Ready"
        Me.ssStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(662, 412)
        Me.Controls.Add(Me.ss)
        Me.Controls.Add(Me.chkPets)
        Me.Controls.Add(Me.txtSearch)
        Me.Controls.Add(Me.lblTop)
        Me.Controls.Add(Me.lblSearch)
        Me.Controls.Add(Me.pg)
        Me.Controls.Add(Me.tv)
        Me.MinimumSize = New System.Drawing.Size(580, 320)
        Me.Name = "frmServer"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Merge Server Report"
        Me.ss.ResumeLayout(False)
        Me.ss.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pg As ProGrid.ProGrid
    Friend WithEvents tv As TreeView
    Friend WithEvents txtSearch As TextBox
    Friend WithEvents lblTop As Label
    Friend WithEvents lblSearch As Label
    Friend WithEvents chkPets As CheckBox
    Friend WithEvents ss As StatusStrip
    Friend WithEvents ssLoggers As ToolStripStatusLabel
    Friend WithEvents ssStatus As ToolStripStatusLabel
End Class
