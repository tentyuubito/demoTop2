
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master

Public Class frmPackingBox_Topcharoen

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
  
    End Sub


    Private Sub btnExcelDrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcelDrop.Click
        Try
            Dim frm As New frmExcelDroppiont(frmExcelDroppiont.Type_Import.DROPPIONT)
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.Refreshs()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub Refreshs()
        Try
            Dim objDB As New WMS_STD_Formula.DBType_SQLServer
            Dim dtSource As New DataTable
            Dim strQuery As String = "select * from VIEW_RCP_OMS_Droppoint Where 1=1 "

            If Not String.IsNullOrEmpty(txt_So_No.Text) Then
                strQuery &= String.Format(" and SalesOrder_No = '" & Me.txt_So_No.Text & "'") 'TODO : Waiting coding
            End If
            If Not String.IsNullOrEmpty(Me.txtShippingLocation.Tag) Then
                strQuery &= String.Format(" and Customer_Shipping_Location_Index = '" & Me.txtShippingLocation.Tag & "'") 'TODO : Waiting coding
            End If

            If Not String.IsNullOrEmpty(Me.txtShipping.Tag) Then
                strQuery &= String.Format(" and Customer_Shipping_Index = '" & Me.txtShipping.Tag & "'") 'TODO : Waiting coding
            End If

            If cbkCheckNull.Checked Then
                strQuery &= String.Format(" and isnull(OMS_Droppoint_Index,'') <> '' ") 'TODO : Waiting coding
            Else
                strQuery &= String.Format(" and isnull(OMS_Droppoint_Index,'') = '' ") 'TODO : Waiting coding
            End If

            dtSource = objDB.DBExeQuery(strQuery)

            Me.grdPacking.DataSource = dtSource
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmPackingBox_Topcharoen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdPacking.AllowUserToAddRows = False
            Me.grdPacking.AutoGenerateColumns = False
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdPacking_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPacking.CellClick
        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "btnDroppoint"

                    Dim frm As New frmCus_Ship_Location_Popup
                    frm.strAddStrWhere = " and Customer_Shipping_Index = '" & grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("Customer_Shipping_Index").Value & "'"
                    frm.ShowDialog()
                    grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("OMS_Droppoint_Index").Value = frm.Customer_Shipping_Location_Index

                    Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
                    Dim objDTms_Shipping_Location As DataTable = New DataTable
                    Dim _Postcode As String = ""

                    objms_Shipping_Location.getCus_Ship_Locartion_Search("Customer_Shipping_Location_Index", frm.Customer_Shipping_Location_Index)
                    objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable

                    If objDTms_Shipping_Location.Rows.Count > 0 Then
                        grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("OMS_Droppoint_Id").Value = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Id").ToString
                        grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("OMS_Droppoint_Name").Value = objDTms_Shipping_Location.Rows(0).Item("Shipping_Location_Name").ToString

                    End If
                
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim xDb As New DBType_SQLServer
            Dim xQuery As String = ""
            For i As Integer = 0 To Me.grdPacking.RowCount - 1
                With Me.grdPacking.Rows(i)
                    If .Cells("chkSelect").Value = True Then
                        xQuery = " update tb_SalesOrder SET "
                        xQuery &= String.Format(" OMS_document_type = '{0}'", .Cells("OMS_document_type").Value)
                        xQuery &= String.Format(" ,OMS_transport_group = '{0}'", .Cells("OMS_transport_group").Value)
                        xQuery &= String.Format(" ,OMS_transport_type = '{0}'", .Cells("OMS_transport_type").Value)
                        xQuery &= String.Format(" ,OMS_source_depart = '{0}'", .Cells("OMS_source_depart").Value)
                        xQuery &= String.Format(" ,OMS_Droppoint_Index = '{0}'", .Cells("OMS_Droppoint_Index").Value)

                        xQuery &= String.Format(" ,OMS_trandp_vehicle_type = '{0}'", .Cells("OMS_trandp_vehicle_type").Value)
                        xQuery &= String.Format(" ,OMS_trandp_route = '{0}'", .Cells("OMS_trandp_route").Value)
                        xQuery &= String.Format(" ,OMS_trandp_text = '{0}'", .Cells("OMS_trandp_text").Value)
                        xQuery &= String.Format(" ,OMS_trandp_person = '{0}'", .Cells("OMS_trandp_person").Value)
                        xQuery &= String.Format(" ,OMS_tranf_remark = '{0}'", .Cells("OMS_tranf_remark").Value)
                        xQuery &= String.Format(" ,OMS_employee_code = '{0}'", .Cells("OMS_employee_code").Value)
                        xQuery &= String.Format(" ,OMS_note = '{0}'", .Cells("OMS_note").Value)
                        xQuery &= String.Format(" ,OMS_last_update_by = '{0}'", WMS_STD_Formula.W_Module.WV_User_Index)
                        xQuery &= String.Format(" ,OMS_last_update_date = getdate()", "")
                        xQuery &= String.Format(" ,OMS_update_count = (select isnull(SO.OMS_update_count,0)+1 from tb_SalesOrder SO where SO.SalesOrder_Index = tb_SalesOrder.SalesOrder_Index)", "")

                        xQuery &= String.Format(" ,Urgent_Id = '{0}'", .Cells("OMS_transport_type").Value)

                        xQuery &= String.Format(" where SalesOrder_No = '{0}' and Status <> -1", .Cells("SalesOrder_No").Value.ToString)
                        xDb.DBExeNonQuery(xQuery)
                    End If

                End With
            Next

            W_MSG_Information("บันทึกเสร็จสิ้น")

            btnSearch.PerformClick()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShipping_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShipping.Click
        Try
            Dim frm As New frmConsignee_Popup
            frm.ShowDialog()

            If String.IsNullOrEmpty(frm.Consignee_Index) Then
                Me.txtShipping.Tag = Nothing
                Me.txtShipping.Clear()
            Else
                Me.txtShipping.Tag = frm.Consignee_Index
                Me.txtShipping.Text = frm.Consignee_ID & " : " & frm.Consignee_Name
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnShippingLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShippingLocation.Click
        Try
            Dim frm As New frmCus_Ship_Location_Popup
            frm.strAddStrWhere = String.Empty
            frm.ShowDialog()

            If String.IsNullOrEmpty(frm.Customer_Shipping_Location_Index) Then
                Me.txtShippingLocation.Tag = Nothing
                Me.txtShippingLocation.Clear()
            Else
                Me.txtShippingLocation.Tag = frm.Customer_Shipping_Location_Index
                Me.txtShippingLocation.Text = frm.strCustomer_Shipping_Location_Name
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCalDropPoint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCalDropPoint.Click
        Try
            For i As Integer = 0 To Me.grdPacking.RowCount - 1
                With Me.grdPacking.Rows
                    If IsDBNull(grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value) Then
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = ""
                    End If
                    If String.IsNullOrEmpty(grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value) And grdPacking.Rows(i).Cells("SAO_Type").Value = "SL" Then
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Index").Value
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Id").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Id").Value
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Name").Value = grdPacking.Rows(i).Cells("Customer_Shipping").Value

                    ElseIf String.IsNullOrEmpty(grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value) And grdPacking.Rows(i).Cells("SAO_Type").Value = "CL" Then
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Location_Index").Value
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Id").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Location_Id").Value
                        grdPacking.Rows(i).Cells("OMS_Droppoint_Name").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Location").Value
                    End If
                End With
            Next

            W_MSG_Information("คำนวณเสร็จสิ้น")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        Try
            For i As Integer = 0 To Me.grdPacking.RowCount - 1
                With Me.grdPacking.Rows(i)
                    If CheckBox1.Checked Then
                        .Cells("chkSelect").Value = True
                    Else
                        .Cells("chkSelect").Value = False
                    End If

                End With
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

  
End Class