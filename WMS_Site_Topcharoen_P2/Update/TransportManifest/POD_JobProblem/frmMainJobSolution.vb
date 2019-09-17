Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer

Public Class frmMainJobSolution
    Public Process_id As Integer = 23
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _JobSolution_Index As String = ""
    Public Property JobSolution_Index() As String
        Get
            Return _JobSolution_Index
        End Get
        Set(ByVal value As String)
            _JobSolution_Index = value
        End Set
    End Property
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmJobSolution
        frm.SaveType = 0
        frm.ShowDialog()
        getSearchJobSolution()

    End Sub

    Sub ShowgrdJobSolution()
        Dim objms_JobSolution As New ms_JobSolution(ms_JobSolution.enuOperation_Type.SEARCH)
        Try
            objms_JobSolution.SearchData_Click("", "")
            grdJobSolution.DataSource = objms_JobSolution.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objms_JobSolution = Nothing
        End Try

    End Sub



    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdJobSolution.Rows.Count <> 0 Then

            Dim frm As New frmJobSolution
            frm.SaveType = 1
            frm.JobSolution_Index = grdJobSolution.Rows(grdJobSolution.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
            getSearchJobSolution()
        Else
            W_MSG_Information_ByIndex(77)
        End If


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim JobSolution_Index As String = ""
        Try
            If grdJobSolution.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objms_JobSolution As New ms_JobSolution(ms_JobSolution.enuOperation_Type.DELETE)
                JobSolution_Index = grdJobSolution.Rows(grdJobSolution.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objms_JobSolution.Delete_Master(JobSolution_Index)
                getSearchJobSolution()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmMainJobSolution_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            

            grdJobSolution.AutoGenerateColumns = False
            cboSearchJobSolution.SelectedIndex = 0
            ShowgrdJobSolution()

            grdJobSolution.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.getSearchJobSolution()
    End Sub

    Sub getSearchJobSolution()
        Dim strSQL As String = ""
        Dim strWhere As String = ""

        strSQL = "SELECT     *, dbo.ms_Process.Process_Name AS Process_Name"
        strSQL &= "  FROM         dbo.ms_JobSolution LEFT OUTER JOIN"
        strSQL &= "               dbo.ms_Process ON dbo.ms_JobSolution.Process_Id = dbo.ms_Process.Process_Id"
        strSQL &= "  WHERE(dbo.ms_JobSolution.status_id <> -1)"

        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchJobSolution.SelectedIndex

                Case 0
                    strWhere &= " and dbo.ms_JobSolution.JobSolution_Id = '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "' "
                Case 1
                    strWhere &= " and Process_Name like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
                Case 2
                    strWhere &= " and dbo.ms_JobSolution.Description like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
            End Select
        End If
        'add data to datagrid
        strSQL = strSQL & strWhere
        Dim objms_JobSolution As New SQLCommands

        objms_JobSolution.SQLComand(strSQL)
        grdJobSolution.DataSource = objms_JobSolution.DataTable
    End Sub

    Private Sub grdJobSolution_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdJobSolution.CellDoubleClick
        btnUpdate_Click(sender, e)
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            btnSearch_Click(New Object, New EventArgs)
        End If
    End Sub
End Class