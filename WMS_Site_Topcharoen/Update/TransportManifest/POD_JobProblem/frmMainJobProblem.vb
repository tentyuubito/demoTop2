Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer

Public Class frmMainJobProblem
    Public Process_id As Integer = 23

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _JobProblem_Index As String = ""
    Public Property JobProblem_Index() As String
        Get
            Return _JobProblem_Index
        End Get
        Set(ByVal value As String)
            _JobProblem_Index = value
        End Set
    End Property
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmJobProblem
        frm.SaveType = 0
        frm.ShowDialog()
        getSearchJobProblem()

    End Sub

    Sub ShowgrdJobProblem()
        Dim objms_JobProblem As New ms_JobProblem(ms_JobProblem.enuOperation_Type.SEARCH)
        Try
            objms_JobProblem.SearchData_Click("", "")
            grdJobProblem.DataSource = objms_JobProblem.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objms_JobProblem = Nothing
        End Try

    End Sub



    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdJobProblem.Rows.Count <> 0 Then

            Dim frm As New frmJobProblem
            frm.SaveType = 1
            frm.JobProblem_Index = grdJobProblem.Rows(grdJobProblem.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
            getSearchJobProblem()
        Else
            W_MSG_Information_ByIndex(77)
        End If


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim JobProblem_Index As String = ""
        Try
            If grdJobProblem.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objms_JobProblem As New ms_JobProblem(ms_JobProblem.enuOperation_Type.DELETE)
                JobProblem_Index = grdJobProblem.Rows(grdJobProblem.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objms_JobProblem.Delete_Master(JobProblem_Index)
                getSearchJobProblem()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmMainJobProblem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            

            grdJobProblem.AutoGenerateColumns = False
            cboSearchJobProblem.SelectedIndex = 0
            ShowgrdJobProblem()

            grdJobProblem.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.getSearchJobProblem()
    End Sub

    Sub getSearchJobProblem()
        Dim strSQL As String = ""
        Dim strWhere As String = ""

        strSQL = "SELECT     *, dbo.ms_Process.Process_Name AS Process_Name"
        strSQL &= "  FROM         dbo.ms_JobProblem LEFT OUTER JOIN"
        strSQL &= "               dbo.ms_Process ON dbo.ms_JobProblem.Process_Id = dbo.ms_Process.Process_Id"
        strSQL &= "  WHERE(dbo.ms_JobProblem.status_id <> -1)"

        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchJobProblem.SelectedIndex

                Case 0
                    strWhere &= " and dbo.ms_JobProblem.JobProblem_Id = '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "' "
                Case 1
                    strWhere &= " and Process_Name like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
                Case 2
                    strWhere &= " and dbo.ms_JobProblem.Description like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
            End Select
        End If
        'add data to datagrid
        strSQL = strSQL & strWhere
        Dim objms_JobProblem As New SQLCommands

        objms_JobProblem.SQLComand(strSQL)
        grdJobProblem.DataSource = objms_JobProblem.DataTable
    End Sub

    Private Sub grdJobProblem_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdJobProblem.CellDoubleClick
        btnUpdate_Click(sender, e)
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            btnSearch_Click(New Object, New EventArgs)
        End If
    End Sub
End Class