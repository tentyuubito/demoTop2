Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms

Public Class frmMainWarehouse

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _Province_Index As String = ""
    Public Property Province_Index() As String
        Get
            Return _Province_Index
        End Get
        Set(ByVal value As String)
            _Province_Index = value
        End Set
    End Property

    Private Sub frmfrmMainWarehouse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2024)
            oFunction.SW_Language_Column(Me, Me.grdWarehouse, 2024)

            grdWarehouse.AutoGenerateColumns = False

            cboSearchDocumentType.SelectedIndex = 0
            ShowgrdWarehouse()

            grdWarehouse.Text = Me.Text

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub ShowgrdProvince()
        Dim objms_Province As New ms_Province(ms_Province.enuOperation_Type.SEARCH)
        Try
            objms_Province.SearchData_Click("", "")
            grdWarehouse.DataSource = objms_Province.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objms_Province = Nothing
        End Try
    End Sub

    Sub ShowgrdWarehouse()
        Dim objms_Warehouse As New ms_Warehouse(ms_Warehouse.enuOperation_Type.SEARCH)
        Try
            objms_Warehouse.SearchData_Click("", "")
            grdWarehouse.DataSource = objms_Warehouse.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objms_Warehouse = Nothing
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmWarehouse
        frm.SaveType = 0
        frm.ShowDialog()
        ShowgrdWarehouse()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdWarehouse.Rows.Count <> 0 Then

            Dim frm As New frmWarehouse

            frm.SaveType = 1
            frm.Warehouse_Index = grdWarehouse.Rows(grdWarehouse.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
        Else
            W_MSG_Information_ByIndex(77)
        End If
        ShowgrdWarehouse()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            Dim Warehouse_Index As String = ""
            If grdWarehouse.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objms_Warehouse As New ms_Warehouse(ms_Warehouse.enuOperation_Type.DELETE)
                Warehouse_Index = grdWarehouse.Rows(grdWarehouse.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objms_Warehouse.Delete_Master(Warehouse_Index)
            End If
            ShowgrdWarehouse()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.getSearchProvince()
    End Sub

    Sub getSearchProvince()
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        strSQL += " select ms_Warehouse.*,ms_DistributionCenter.DistributionCenter_Id,ms_DistributionCenter.Description as [DistributionCenter_Desc] from ms_Warehouse left join ms_DistributionCenter on ms_Warehouse.DistributionCenter_Index=ms_DistributionCenter.DistributionCenter_Index where Warehouse_Index not in ('0') and ms_Warehouse.status_id not in (-1) "
        'strSQL += "  SELECT  * FROM      ms_Warehouse where  Warehouse_Index not in ('0') and status_id  not in (-1) "
        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchDocumentType.SelectedIndex

                Case 0 'Tag
                    strWhere += " and Warehouse_No like '%" & Me.txtSearchKey.Text.Trim & "%' "
                Case 1 'Lotº≈‘µ
                    strWhere += " and Description like '%" & Me.txtSearchKey.Text.Trim & "%' "
            End Select
        End If
        'add data to datagrid
        strSQL = strSQL + strWhere
        Dim objms_Province As New SQLCommands

        objms_Province.SQLComand(strSQL)
        grdWarehouse.DataSource = objms_Province.DataTable
    End Sub

    Private Sub grdProvince_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWarehouse.CellDoubleClick
        btnUpdate_Click(sender, e)

    End Sub

    Private Sub frmMainWarehouse_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigWarehouse
                    frm.ShowDialog()
                    Dim oFunction As New WMS_STD_Master.W_Language
                    oFunction.SwitchLanguage(Me, 2024)
                    oFunction.SW_Language_Column(Me, Me.grdWarehouse, 2024)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.getSearchProvince()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class