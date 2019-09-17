
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
            Me.SetLabelSum()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub SetLabelSum()
        Try
            'Label Sum
            If Me.grdPacking.RowCount > 0 Then
                Me.lblCountRows.Text = "รวม : " & FormatNumber(Me.grdPacking.RowCount, 0) & " รายการ"
            Else
                Me.lblCountRows.Text = "ไม่พบรายการถุง"
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub
    Private Sub Refreshs()
        Try
            Dim objDB As New WMS_STD_Formula.DBType_SQLServer
            Dim dtSource As New DataTable
            Dim strQuery As String = "select * from VIEW_RCP_OMS_Droppoint Where 1=1 "

            If Me.cboPriority.SelectedValue <> "-99" Then
                strQuery &= String.Format(" and OMS_transport_type = '" & Me.cboPriority.SelectedValue & "'")
            End If

            If rall.Checked Then
            ElseIf rx.Checked Then
                strQuery &= String.Format(" and SO_Type = 'X'")
            ElseIf ry.Checked Then
                strQuery &= String.Format(" and SO_Type = 'Y'")
            ElseIf rz.Checked Then
                strQuery &= String.Format(" and SO_Type = 'Z'")
            End If


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

            Me.getPriority()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub


    Private Sub getPriority()

        Dim objClassDB As New clsMasterPriority()
        Dim objDT As DataTable = New DataTable
        Try
            objDT = objClassDB.getPriority()
            Dim dtrow As DataRow = objDT.NewRow()
            dtrow.Item("Priority_Id") = "-99"
            dtrow.Item("display") = "ไม่ระบุ"
            objDT.Rows.InsertAt(dtrow, 0)
            cboPriority.BeginUpdate()

            With cboPriority
                .DisplayMember = "display"
                .ValueMember = "Priority_Id"
                .DataSource = objDT
            End With

            cboPriority.EndUpdate()
            If cboPriority.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub


    Private Sub btnCal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub grdPacking_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPacking.CellClick
        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "btnDroppoint"

                    Dim frm As New frmCus_Ship_Location_Popup
                    'frm.strAddStrWhere = " and Customer_Shipping_Index = '" & grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("Customer_Shipping_Index").Value & "'"
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
                    Else
                        grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("OMS_Droppoint_Id").Value = ""
                        grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("OMS_Droppoint_Name").Value = ""
                        grdPacking.Rows(grdPacking.CurrentRow.Index).Cells("OMS_Droppoint_Index").Value = ""
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

                        xQuery &= String.Format(" where SalesOrder_Index = '{0}' and Status <> -1", .Cells("SalesOrder_Index").Value.ToString)
                        xDb.DBExeNonQuery(xQuery)


                        ' --- STEP : Sy_Audit_Log
                        Dim obj_cls As New cls_syAditlog
                        obj_cls.Process_ID = 10
                        obj_cls.Description = String.Format("user name : {0} ,manual update drop point : {1}", WMS_STD_Formula.W_Module.WV_UserName, .Cells("OMS_Droppoint_Id").Value.ToString)
                        obj_cls.Document_Index = .Cells("SalesOrder_Index").Value.ToString
                        obj_cls.Document_No = .Cells("SalesOrder_No").Value.ToString
                        obj_cls.Log_Type_ID = 1101 'แก้ไข drop point
                        obj_cls.Insert_Master()
                        obj_cls = Nothing

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
                With Me.grdPacking.Rows(i)
                    'If IsDBNull(grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value) Then
                    '    grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = ""
                    'End If

                    'If grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = "" Then
                    Dim dtQuery As New DataTable
                    Dim xDb As New DBType_SQLServer
                    Dim xQuery As String = ""
                    xQuery = "select trandp_vehicle_type,trandp_droppoint,trandp_route,trandp_text,trandp_person"
                    xQuery &= " ,c.Customer_Shipping_Location_Index as Droppoint_Index,c.Customer_Shipping_Location_Id as Droppoint_Id,c.Shipping_Location_Name as Droppoint_Name"
                    xQuery &= " from tranf_drop_point"
                    xQuery &= " left outer join ms_Customer_Shipping_Location c on c.Customer_Shipping_Location_Id = tranf_drop_point.trandp_droppoint"
                    xQuery &= " where 1=1 "
                    xQuery &= String.Format(" AND trandp_branch = '{0}'", .Cells("Customer_Shipping_Location_Id").Value.ToString)
                    xQuery &= String.Format(" AND trandp_group = '{0}'", .Cells("OMS_transport_group").Value.ToString)
                    xQuery &= String.Format(" AND trandp_type = '{0}'", .Cells("OMS_transport_type").Value.ToString)

                    dtQuery = xDb.DBExeQuery(xQuery)
                    If dtQuery.Rows.Count > 0 Then
                        'trandp_branch()x
                        'trandp_group()x
                        'trandp_type()x
                        'trandp_vehicle_type()
                        'trandp_droppoint()x
                        'trandp_route()
                        'trandp_text()
                        'trandp_person()

                        .Cells("OMS_Droppoint_Index").Value = dtQuery.Rows(0)("Droppoint_Index").ToString
                        .Cells("OMS_Droppoint_Id").Value = dtQuery.Rows(0)("Droppoint_Id").ToString
                        .Cells("OMS_Droppoint_Name").Value = dtQuery.Rows(0)("Droppoint_Name").ToString

                        .Cells("OMS_trandp_vehicle_type").Value = dtQuery.Rows(0)("trandp_vehicle_type").ToString
                        .Cells("OMS_trandp_route").Value = dtQuery.Rows(0)("trandp_route").ToString
                        .Cells("OMS_trandp_text").Value = dtQuery.Rows(0)("trandp_text").ToString
                        .Cells("OMS_trandp_person").Value = dtQuery.Rows(0)("trandp_person").ToString


                    End If
                    'End If
                    If grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value.ToString = "" Then
                        If String.IsNullOrEmpty(grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value) And grdPacking.Rows(i).Cells("SAO_Type").Value = "SL" Then
                            grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Index").Value
                            grdPacking.Rows(i).Cells("OMS_Droppoint_Id").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Id").Value
                            grdPacking.Rows(i).Cells("OMS_Droppoint_Name").Value = grdPacking.Rows(i).Cells("Customer_Shipping").Value

                        ElseIf String.IsNullOrEmpty(grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value) And grdPacking.Rows(i).Cells("SAO_Type").Value = "CL" Then
                            grdPacking.Rows(i).Cells("OMS_Droppoint_Index").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Location_Index").Value
                            grdPacking.Rows(i).Cells("OMS_Droppoint_Id").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Location_Id").Value
                            grdPacking.Rows(i).Cells("OMS_Droppoint_Name").Value = grdPacking.Rows(i).Cells("Customer_Shipping_Location").Value
                        End If
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

  
    Private Sub btnDrop_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDrop.Click
        Try
            Dim frm As New frmDroppoint
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class