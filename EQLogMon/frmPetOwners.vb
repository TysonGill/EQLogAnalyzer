Imports ProGrid.ProGrid
Imports Utility

Public Class frmPetOwners
    Private Sub frmPetOwners_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Cursor = Cursors.WaitCursor

            ' Display in the grid
            pg.StyleSet("PetID", New ProGridStyle(ProGridStyleTypes.Numeric, Visible:=False))
            pg.StyleSet("Owner", New ProGridStyle(ProGridStyleTypes.Freeform, MaxStringLength:=15))
            pg.StyleSet("Pet", New ProGridStyle(ProGridStyleTypes.Freeform, MaxStringLength:=15))
            pg.GridShow(dtPets, ProGrid.ProGrid.ProGridOrienations.Horizontal)

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub pg_UserClickedDelete(ByRef DeletedRow As DataRow, ByRef SaveAction As ProGridSaveActions) Handles pg.UserClickedDelete
        Try
            Cursor = Cursors.WaitCursor
            If MessageBox.Show("Delete the selected pet association, are you sure?", "Delete Pet Association", MessageBoxButtons.YesNo, MessageBoxIcon.Question) <> vbYes Then
                SaveAction = ProGridSaveActions.UndoChanges
                Exit Sub
            End If

            Dim sql As String = "DELETE FROM EQPets WHERE [PetID] = " + fmt.q(DeletedRow("PetID"))
            dq.Execute(sql)
            SaveAction = ProGridSaveActions.CommitChanges

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub pg_UserEditedCell(ByRef EditedRow As DataRow, EditedColIndex As Integer, ByRef SaveAction As ProGridSaveActions) Handles pg.UserEditedCell
        Try
            Cursor = Cursors.WaitCursor

            Dim sql As String
            If EditedRow("PetID") = 0 Then
                sql = "INSERT INTO EQPets ([Owner], [Pet]) VALUES (" + fmt.q(EditedRow("Owner").trim) + ", " + fmt.q(EditedRow("Pet").trim) + ")"
                EditedRow("PetID") = dq.GetIdentity(sql)
            Else
                Dim ColName As String = pg.GridTable.Columns(EditedColIndex).ColumnName
                sql = "UPDATE EQPets SET [" + ColName + "] = " + fmt.q(EditedRow(ColName).Trim) + " WHERE PetID = " + fmt.q(EditedRow("PetID"))
                dq.Execute(sql)
            End If
            SaveAction = ProGridSaveActions.CommitChanges

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub pg_UserClickedAdd(ByRef AddedRow As DataRow, ByRef SaveAction As ProGridSaveActions) Handles pg.UserClickedAdd
        Try
            Cursor = Cursors.WaitCursor

            AddedRow("PetID") = 0
            AddedRow("Owner") = ""
            AddedRow("Pet") = ""
            SaveAction = ProGridSaveActions.CommitChanges

        Catch ex As Exception
            ReportError(ex)
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

End Class