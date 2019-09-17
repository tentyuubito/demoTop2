Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms

Public Class frmMainDocumentTypeV5

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _DocumentType_Index As String = ""
    Public Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmDocumentTypeV5
        frm.SaveType = 0
        frm.ShowDialog()
        getSearchDocumentType()

    End Sub

    Sub ShowgrdDocumentType()
        Dim objms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Try
            objms_DocumentType.SearchData_Click("", "")
            grdDocumentType.DataSource = objms_DocumentType.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objms_DocumentType = Nothing
        End Try

    End Sub



    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdDocumentType.Rows.Count <> 0 Then

            Dim frm As New frmDocumentTypeV5
            frm.SaveType = 1
            frm.DocumentType_Index = grdDocumentType.Rows(grdDocumentType.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
            getSearchDocumentType()
        Else
            W_MSG_Information_ByIndex(77)
        End If


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim DocumentType_Index As String = ""
        Try
            If grdDocumentType.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.DELETE)
                DocumentType_Index = grdDocumentType.Rows(grdDocumentType.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objms_DocumentType.Delete_Master(DocumentType_Index)
                getSearchDocumentType()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmMainDocumentType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
           
            grdDocumentType.AutoGenerateColumns = False
            cboSearchDocumentType.SelectedIndex = 0
            ShowgrdDocumentType()
            AddProcessID()
            grdDocumentType.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub AddProcessID()
        Dim objClassDB As New ms_Process(ms_Process.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            Dim dtTemp As New DataTable
            dtTemp.Columns.Add(New DataColumn("process_Name", GetType(String)))
            dtTemp.Columns.Add(New DataColumn("Process_Id", GetType(Integer)))
            Dim drProcess As DataRow
            drProcess = dtTemp.NewRow
            drProcess("process_Name") = "--- All ---"
            drProcess("Process_Id") = -11
            dtTemp.Rows.Add(drProcess)


            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            dtTemp.Merge(objDT)

            cboProcess.DisplayMember = "process_Name"
            cboProcess.ValueMember = "Process_Id"
            cboProcess.DataSource = dtTemp


        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getSearchDocumentType()
        Catch ex As Exception
            W_MSG_Confirm(ex.Message)
        End Try

    End Sub

    Sub getSearchDocumentType()

        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""

            strSQL = "SELECT     *, dbo.ms_Process.Process_Name AS Process_Name"
            strSQL &= "  FROM         dbo.ms_DocumentType LEFT OUTER JOIN"
            strSQL &= "               dbo.ms_Process ON dbo.ms_DocumentType.Process_Id = dbo.ms_Process.Process_Id"

            strSQL &= "  WHERE(dbo.ms_DocumentType.status_id <> -1)"

            If Me.txtSearchKey.Text.Trim <> "" Then
                Select Case cboSearchDocumentType.SelectedIndex

                    Case 0
                        strWhere &= " and dbo.ms_DocumentType.DocumentType_Id = '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "' "
                    Case 1
                        strWhere &= " and Process_Name like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
                        'Case 2
                        '    strWhere &= " and dbo.ms_DocumentType.Description like '" & Me.txtSearchKey.Text.Replace("'", "''").Trim & "%' "
                End Select
            End If
            If cboProcess.SelectedValue <> "-11" Then
                strWhere &= " and dbo.ms_DocumentType.Process_Id = '" & cboProcess.SelectedValue.ToString & "' "
            End If
            'add data to datagrid
            strSQL = strSQL & strWhere
            Dim objms_DocumentType As New SQLCommands

            objms_DocumentType.SQLComand(strSQL)
            grdDocumentType.DataSource = objms_DocumentType.DataTable
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdDocumentType_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDocumentType.CellDoubleClick
        btnUpdate_Click(sender, e)
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnSearch_Click(New Object, New EventArgs)
        End If
    End Sub

    Private Sub cboProcess_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProcess.SelectedIndexChanged
        Try
            Me.getSearchDocumentType()
        Catch ex As Exception
            W_MSG_Confirm(ex.Message)
        End Try
    End Sub


    Private Sub frmMainDocumentType_Update_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class