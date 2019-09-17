
Imports WMS_STD_Master.W_Language
Public Class frmMappingWareHouse
    Dim _Excute As New DBType_SQLServer

    Private Sub MappingWareHouse_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' getDistribution()
            ShowMappingST_WMS()

            ' btnSave.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Sub getDistribution()

    '    Try


    '        Dim odt_Items As New DataTable
    '        odt_Items = _Excute.DBExeQuery("Select DistributionCenter_Id,DistributionCenter_Index from ms_DistributionCenter where status_id<>-1")
    '        odt_Items.Rows.Add("ไม่ระบุ", "10000000000")

    '        With Col_Distri_id
    '            .DisplayMember = "DistributionCenter_Id"
    '            .ValueMember = "DistributionCenter_Index"
    '            .DataSource = odt_Items
    '        End With



    '    Catch ex As Exception
    '        Throw ex

    '    End Try

    'End Sub
    Public Sub ShowMappingST_WMS(Optional ByVal pwhere As String = "")
        Try
            Dim _dt As New DataTable
            Dim str_sql As String = ""
            str_sql = "Select WH.Map_index,WH.WAREHOUSEID,WH.WAREHOUSENAME,DS.DistributionCenter_Id,DS.DistributionCenter_Index   from ms_MappingWareHouseST_WMS WH "
            str_sql &= " left join ms_DistributionCenter DS "
            str_sql &= " on WH.DistributionCenter_Index = DS.DistributionCenter_Index and DS.status_id <> -1"
            str_sql &= " Where isnull(WH.Status_Id ,0)<> -1 " + pwhere
            _dt = _Excute.DBExeQuery(str_sql)

            grdMappingWareHouse.AutoGenerateColumns = False
            grdMappingWareHouse.DataSource = _dt
            'grdMappingWareHouse.Columns(grdMappingWareHouse.ColumnCount - 1).ReadOnly = False
       

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub chkMapped_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMapped.CheckedChanged
        Try
            If chkMapped.Checked Then
                ShowMappingST_WMS(" and DS.DistributionCenter_Index is not null")
            Else
                ShowMappingST_WMS()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    'Private Function Validates() As Boolean
    '    Try
    '        Dim dt As New DataTable
    '        dt = CType(grdMappingWareHouse.DataSource, DataTable)
    '        Dim Issave As Boolean = False
    '        For Each drCheck As DataRow In dt.Rows
    '            If drCheck("Col_Select") Then
    '                Issave = True
    '            End If
    '        Next
    '        If Issave Then
    '            W_MSG_Information("กรุณา เลือก รายการที่จะบันทึก")
    '            Exit Function
    '        End If

    '        Dim i As Integer = 0
    '        For Each dr As DataRow In dt.Rows
    '            Issave = True
    '            If CheckCustomer(dr("DistributionCenter_Id")).ToString.Length Then
    '                grdMappingWareHouse.Rows(i).DefaultCellStyle.BackColor = Color.Red
    '                grdMappingWareHouse.Rows(i).Cells("Col_Chacked_Data").Value = "ไม่พบ " & dr("DistributionCenter_Id") & " ในศุนย์กระจาย"
    '                Issave = False
    '            End If
    '            i += 1
    '        Next
    '        Return Issave
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Function

  

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

   

    Private Sub btnSreach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSreach.Click
        Try
            Select Case cboConition.SelectedIndex
                Case 0
                    ShowMappingST_WMS(" and WAREHOUSEID like '" & txtKey.Text.Trim & "%' ")

                Case 1
                    ShowMappingST_WMS(" and WAREHOUSENAME like '" & txtKey.Text.Trim & "%' ")
                Case Else
                    ShowMappingST_WMS()
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try

            If W_MSG_Confirm("ต้องการลบ รายการนี้ใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
                _Excute.DBExeNonQuery(String.Format("Update ms_MappingWareHouseST_WMS set Status_id = -1 where Map_index = '{0}'", grdMappingWareHouse.CurrentRow.Cells("Col_index").Value.ToString))
                W_MSG_Information("ลบข้อมูลเรียบร้อย")
                ShowMappingST_WMS()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmAddMappingWareHouse
            frm.SaveType = 1
            frm.ShowDialog()
            ShowMappingST_WMS()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Dim frm As New frmAddMappingWareHouse
            frm.index = grdMappingWareHouse.CurrentRow.Cells("Col_index").Value.ToString
            frm.SaveType = 2
            frm.ShowDialog()

            ShowMappingST_WMS()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdMappingWareHouse_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdMappingWareHouse.CellClick
        'Try
        '    btnEdit.PerformClick()
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try
    End Sub

    Private Sub grdMappingWareHouse_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdMappingWareHouse.CellDoubleClick
        Try
            btnEdit.PerformClick()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class