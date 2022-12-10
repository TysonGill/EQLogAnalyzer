<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPetOwners
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
        Me.SuspendLayout()
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
        Me.pg.EnableEditing = True
        Me.pg.EnableFiltering = True
        Me.pg.EnableFullRowSelection = False
        Me.pg.EnableIdentityHide = True
        Me.pg.EnableInvalidCellHandling = ProGrid.ProGrid.ProGridInvalidMethods.MarkAndShowTip
        Me.pg.EnableOptionsButton = False
        Me.pg.EnableOptionsCopy = False
        Me.pg.EnableOptionsExport = False
        Me.pg.EnableOptionsFind = False
        Me.pg.EnableOptionsPrint = False
        Me.pg.EnableOptionsSort = False
        Me.pg.EnableRefreshButton = False
        Me.pg.LicenseFolder = ""
        Me.pg.Location = New System.Drawing.Point(0, 0)
        Me.pg.Name = "pg"
        Me.pg.PrintTitle = "ProGrid Report"
        Me.pg.Size = New System.Drawing.Size(333, 406)
        Me.pg.TabIndex = 5
        Me.pg.UseWaitCursor = Nothing
        '
        'frmPetOwners
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(333, 406)
        Me.Controls.Add(Me.pg)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPetOwners"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Pet Owner Assignments"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pg As ProGrid.ProGrid
End Class
