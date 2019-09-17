Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer

Public Class frmMainResponsibleParty

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _ResponsibleParty_Index As String = ""
    Public Property ResponsibleParty_Index() As String
        Get
            Return _ResponsibleParty_Index
        End Get
        Set(ByVal value As String)
            _ResponsibleParty_Index = value
        End Set
    End Property
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmResponsibleParty
        frm.SaveType = 0
        frm.ShowDialog()
        getSearchResponsibleParty()

    End Sub

    Sub ShowgrdResponsibleParty()
        Dim objms_ResponsibleParty As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.SEARCH)
        Try
            objms_ResponsibleParty.SearchData_Click("", "")
            grdResponsibleParty.DataSource = objms_ResponsibleParty.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objms_ResponsibleParty = Nothing
        End Try

    End Sub



    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdResponsibleParty.Rows.Count <> 0 Then

            Dim frm As New frmResponsibleParty
            frm.SaveType = 1
            frm.ResponsibleParty_Index = grdResponsibleParty.Rows(grdResponsibleParty.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
            getSearchResponsibleParty()
        Else
            W_MSG_Information_ByIndex(77)
        End If


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim ResponsibleParty_Index As String = ""
        Try
            If grdResponsibleParty.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objms_ResponsibleParty As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.DELETE)
                ResponsibleParty_Index = grdResponsibleParty.Rows(grdResponsibleParty.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objms_ResponsibleParty.Delete_Master(ResponsibleParty_Index)
                getSearchResponsibleParty()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmMainResponsibleParty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            
            grdResponsibleParty.AutoGenerateColumns = False
            cboSearchResponsibleParty.SelectedIndex = 0
            ShowgrdResponsibleParty()

            grdResponsibleParty.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.getSearchResponsibleParty()
    End Sub

    Sub getSearchResponsibleParty()
        Dim strSQL As String = ""
        Dim strWhere As String = ""

        strSQL = "SELECT     *, dbo.ms_Process.Process_Name AS Process_Name"
        strSQL &= "  FROM         dbo.ms_ResponsibleParty LEFT OUTER JOIN"
        strSQL &= "               dbo.ms_Process ON dbo.ms_ResponsibleParty.Process_Id = dbo.ms_Process.Process_Id"
        strSQL &= "  WHERE(dbo.ms_ResponsibleParty.status_id <> -1)"

        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchResponsibleParty.SelectedIndex

                Case 0
                    strWhere &= " and dbo.ms_ResponsibleParty.ResponsibleParty_Id = '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "' "
                Case 1
                    strWhere &= " and Process_Name like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
                Case 2
                    strWhere &= " and dbo.ms_ResponsibleParty.Description like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
            End Select
        End If
        'add data to datagrid
        strSQL = strSQL & strWhere
        Dim objms_ResponsibleParty As New SQLCommands

        objms_ResponsibleParty.SQLComand(strSQL)
        grdResponsibleParty.DataSource = objms_ResponsibleParty.DataTable
    End Sub

    Private Sub grdResponsibleParty_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdResponsibleParty.CellDoubleClick
        btnUpdate_Click(sender, e)
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            btnSearch_Click(New Object, New EventArgs)
        End If
    End Sub
End Class