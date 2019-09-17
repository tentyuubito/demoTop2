Imports System.Windows.Forms
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
'Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_OUTB_WithDraw
Imports WMS_STD_Formula.W_Module
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration
Imports System.Threading
Imports System.Globalization

Public Class frmInvoice_Print

    Public _SqlExc As New DBType_SQLServer
    Private Sub getSOViewSearch()
        Dim objtb_SalesOrder As New tb_SalesOrder
        Dim objDT As DataTable = New DataTable
        'Dim objDTtb_SalesOrder As DataTable = New DataTable
        Dim strWhere As String = ""

        Try
            Dim chkCondition As Boolean = False
            'strWhere &= " AND (Status=" & Me.cboDocumentStatus.SelectedValue & ") "
            If chkSalesOrderDate.Checked Then
                chkCondition = True
                strWhere &= String.Format(" and (cast(SalesOrder_Date as date)>='{0}' and cast(SalesOrder_Date as date)<='{1}') ", Me.dtStart.Value.ToString("yyyy-MM-dd"), Me.dtEnd.Value.ToString("yyyy-MM-dd"))
            End If
            If Me.chkInvcoice_Date.Checked Then
                chkCondition = True
                strWhere &= String.Format(" and (cast(Invoice_Date as date)>='{0}' and cast(Invoice_Date as date)<='{1}') ", Me.dtInvcoice_Date_B.Value.ToString("yyyy-MM-dd"), Me.dtInvcoice_Date_E.Value.ToString("yyyy-MM-dd"))

            End If
            If chkCustomer.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And   Customer_index = '{0}' ", txtCustomer_Id.Tag)
            End If
            If chkSalesOrderNo.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  SalesOrder_No like '%{0}%' ", txtSalesOrderNo.Text)
            End If
            If chkConsignee.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  Customer_Shipping_Index = '{0}' ", txtConsignee_Id.Tag)
            End If
            If chkInvoiceNo.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  Invoice_No like '%{0}%' ", txtInvoice.Text)
            End If
            If chkInvoiceNotNull.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  Invoice_No <> ''")
            End If

            If chkInvoiceNull.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  Invoice_No = ''")
            End If

            If chkExpWin.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  TXT_EXP_NUM > 0")
            End If

            If chkExpWinNo.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  TXT_EXP_NUM = 0")
            End If


            If cbkSalerId.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  saleCode like '%{0}%' ", txtSalerId.Text)
            End If

            If cbkSalesArea.Checked Then
                chkCondition = True
                strWhere &= String.Format("  And  salesAreaCode like '%{0}%' ", txtSalesArea.Text)
            End If

            If Me.chkDistributionCenter.Checked Then
                chkCondition = True
                If Not Me.cboDistributionCenter.SelectedValue = "0010000000000" Then
                    strWhere &= String.Format("  And  DistributionCenter_Index = '{0}' ", Me.cboDistributionCenter.SelectedValue)
                End If

            End If

            If chkCondition = False Then
                W_MSG_Information("กรุณาระบุเงื่อนไข")
                Exit Sub
            End If

            If Me.chkNotRed.Checked Then
                chkCondition = True
                strWhere &= "  And  RGB_Check != 2 "
            End If
            'KSL
            strWhere &= " AND DocumentType_Index in (SELECT  DocumentType_Index FROM ms_DocumentType WHERE Document_Group_Name = 'SALE')"

            strWhere &= " AND (Process_Id=10)  "

            strWhere &= " And  SalesOrder_Index in"
            strWhere &= " ("
            strWhere &= " Select  WI.DocumentPlan_Index"
            strWhere &= " from tb_Withdraw W inner join tb_WithdrawItem WI on W.Withdraw_Index = WI.Withdraw_Index"
            strWhere &= " WHERE (WI.Plan_Process = 10) and (W.Status not in (-1))"
            strWhere &= " and (W.Activity_Id  > 3)"
            strWhere &= ")"
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()

            'ADD BY Pong 22/02/2017
            strWhere &= New clsUserByDC().GetDistributionCenterByUser()

            objtb_SalesOrder.getSOViewSearch(strWhere, -1, 0)
            'objtb_SalesOrder.getSOViewSearch(strWhere)
            'objDT = objtb_SalesOrder.DataTable

            'KSL
            'strWhere &= " AND DocumentType_Index in (SELECT  DocumentType_Index FROM ms_DocumentType WHERE Document_Group_Name = '" & Me.Document_Group_Name & "')"

            'ADD BY Pong 28/04/2015
            objDT = objtb_SalesOrder.GetDataTable

            objDT.AcceptChanges()
            Me.grdSOView.AutoGenerateColumns = False
            Me.grdSOView.DataSource = objDT
            ' Me.grdSOView.Update()
            Chang_RGB()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_SalesOrder = Nothing
        End Try
    End Sub

    Sub Chang_RGB()
        Dim objDT As New DataTable
        objDT = CType(grdSOView.DataSource, DataTable)
        For i As Integer = 0 To objDT.Rows.Count - 1
            With Me.grdSOView
                '.Rows(i).Cells("Col_SalesOrder_Date").Value = CDate(objDT.Rows(i).Item("SalesOrder_Date"))
                '.Rows(i).Cells("Col_SalesOrder_Date").Value = Format(CDate(objDT.Rows(i).Item("SalesOrder_Date")), "dd/MM/yyyy").ToString
                Dim intStatus As Integer = objDT.Rows(i).Item("Status")
                Dim RGB As Integer = IIf(objDT.Rows(i).Item("RGB_Check").ToString = Nothing, 0, objDT.Rows(i).Item("RGB_Check"))
                Select Case RGB
                    Case 1 'พอเบิก
                        grdSOView.Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.LightGreen
                    Case 2 'ไม่พอเบิก
                        grdSOView.Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.Red
                    Case Else
                End Select
                Select Case intStatus
                    Case -1 'ยกเลิก
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.Pink
                    Case 2 'รอเบิก
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightYellow
                    Case 3 'รอส่ง
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightGreen
                    Case 5 'เสร็จสิ้น
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightGreen
                    Case 6 'ค้างจ่าย
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightYellow
                    Case Else
                End Select

                If objDT.Rows(i).Item("Confirm_By").ToString.Trim.Length > 0 Then
                    .Rows(i).Cells("col_Confirm_By").Style.BackColor = Drawing.Color.LightGreen
                    .Rows(i).Cells("col_Confirm_Date").Style.BackColor = Drawing.Color.LightGreen
                End If

                Select Case objDT.Rows(i).Item("TXT_EXP_NUM")
                    Case 0
                    Case Else
                        .Rows(i).Cells("col_WinspeedExport").Style.BackColor = Drawing.Color.LightGreen
                End Select
            End With
        Next
        Me.grdSOView.ClearSelection()
    End Sub


    Private Sub btnSearchCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCustomer.Click
        Try

            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Index = "" Then
                txtCustomer_Id.Tag = tmpCustomer_Index
                txtCustomer_Id.Text = frm.strCustomer_Name_Id
                txtCustomer_Name.Text = frm.customerName
            Else
                txtCustomer_Id.Tag = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            Dim frm As New frmConsignee_Popup
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                If String.IsNullOrEmpty(txtCustomer_Id.Text) Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If
                frm.Customer_Index = Me.txtCustomer_Id.Tag
            Else
                frm.Customer_Index = ""
            End If
            frm.ShowDialog()
            Dim tmp_Index As String = ""
            tmp_Index = frm.Consignee_Index
            If Not tmp_Index = "" Then
                Me.txtConsignee_Id.Tag = tmp_Index
                txtConsignee_Id.Text = frm.Consignee_ID
                txtConsignee_Name.Text = frm.Consignee_Name
            Else
                Me.txtConsignee_Id.Tag = ""
                txtConsignee_Id.Text = ""
                txtConsignee_Name.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub chkCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomer.CheckedChanged
        Try
            btnSearchCustomer.Enabled = chkCustomer.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSalesOrderNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSalesOrderNo.CheckedChanged
        Try
            txtSalesOrderNo.Enabled = chkSalesOrderNo.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkConsignee_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkConsignee.CheckedChanged
        Try
            btnConsignee.Enabled = chkConsignee.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cbkSalerId_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbkSalerId.CheckedChanged
        Try
            txtSalerId.Enabled = cbkSalerId.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub chkSalesArea_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbkSalesArea.CheckedChanged
        Try
            txtSalesArea.Enabled = cbkSalesArea.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub chkInvoiceNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkInvoiceNo.CheckedChanged
        Try
            txtInvoice.Enabled = chkInvoiceNo.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print.Click
        Try
            If IsNothing(grdSOView.CurrentRow) Then
                W_MSG_Information("กรุณาเลือก SO ที่จะปริ้นก่อน")
                Exit Sub
            End If
            'If Not grdSOView.CurrentRow.Cells("Col_Customer_Index").Value.ToString = "0010000000010" Then
            '    If Not grdSOView.CurrentRow.Cells("Col_Customer_Index").Value.ToString = "0010000000012" Then
            '        W_MSG_Information("รายงานนี้ไม่มีของ บริษัท คิงส์สเตลล่า แลบบอราทอรี่ จำกัด ")
            '        Exit Sub
            '    End If
            'End If
            If String.IsNullOrEmpty(grdSOView.CurrentRow.Cells("col_Invoice_No").Value.ToString) Then
                W_MSG_Information("SO นี้ ยังไม่ได้ออก Invoice")
                Exit Sub
            End If
            Dim frm_Con As New frmConfigPrint_Invoice
            frm_Con.Customer_Shipping_Index = grdSOView.CurrentRow.Cells("Col_Customer_Shipping_Index").Value.ToString
            frm_Con.Customer_Shipping_Id = grdSOView.CurrentRow.Cells("Col_Customer_Shipping_Id").Value.ToString
            frm_Con.SalseOrder_Index = grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString
            frm_Con.Customer_Index = grdSOView.CurrentRow.Cells("Col_Customer_Index").Value.ToString
            frm_Con.Invoice_No = grdSOView.CurrentRow.Cells("col_Invoice_No").Value.ToString
            frm_Con.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

   
    Private Sub btnGenInvoice_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenInvoice.Click
        Try
            If IsNothing(grdSOView.CurrentRow) Then
                W_MSG_Information("กรุณาเลือก SO ที่จะต้องการออก Invoice ก่อน")
                Exit Sub
            End If
            If W_MSG_Confirm("ต้องการออก Invoice ใช่หรือไม่") = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            Dim _SqlExc2 As New DBType_SQLServer
            Dim _dt As New DataTable
            _dt = _SqlExc2.DBExeQuery(String.Format("select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' AND Total_Qty <> Total_Qty_Withdraw", grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString))
            If _dt.Rows.Count > 0 Then
                W_MSG_Information("เอกสารยังเบิกสินค้าไม่ครบ ไม่สามารถออก Invoice ได้")
                Exit Sub
            End If


            Dim frm1 As New frmInvoice_Date
            frm1.ShowDialog()
            If frm1.isCancel = True Then
                Exit Sub
            End If

            Dim syAutoGen As New Sy_AutoyyyyMM
            Dim DocNO As String = ""


            'Dim _SqlExc2 As New DBType_SQLServer
            _SqlExc2.DBExeNonQuery(String.Format("Update tb_SalesOrder set Invoice_Date= '{1}',User_Index_INV = '{2}' where SalesOrder_Index = '{0}'", grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString, frm1.dtpInvoice_Date.Value.ToString("yyyy/MM/dd"), WV_User_Index))


            _SqlExc.connectDB()
            Dim myTrans As SqlClient.SqlTransaction = _SqlExc.Connection.BeginTransaction()
            Dim _Customer_index As String = ""
            _Customer_index = grdSOView.CurrentRow.Cells("Col_Customer_Index").Value.ToString
            Dim _SalesOrder_Date As String = ""
            '_SalesOrder_Date = grdSOView.CurrentRow.Cells("Col_SalesOrder_Date").Value.ToString
            _SalesOrder_Date = frm1.dtpInvoice_Date.Value.ToString("yyyy/MM/dd")
            Try
                If _Customer_index = "0010000000010" Then 'BR
                    DocNO = syAutoGen.Auto_DocumentType_Number(_SqlExc.Connection, myTrans, "0010000000502", _SalesOrder_Date, "")
                ElseIf _Customer_index = "0010000000012" Then 'SP
                    DocNO = syAutoGen.Auto_DocumentType_Number(_SqlExc.Connection, myTrans, "0010000000501", _SalesOrder_Date, "")
                ElseIf _Customer_index = "0010000000011" Then 'KSL
                    DocNO = syAutoGen.Auto_DocumentType_Number(_SqlExc.Connection, myTrans, "0010000000503", _SalesOrder_Date, "")
                End If
                syAutoGen = Nothing
                myTrans.Commit()


                _SqlExc2 = New DBType_SQLServer
                _SqlExc2.DBExeNonQuery(String.Format("Update tb_SalesOrder set Invoice_No = '{0}' where SalesOrder_Index = '{1}'", DocNO, grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString))

                Dim obj_cls As New cls_syAditlog
                obj_cls.Process_ID = 10
                obj_cls.Description = "Create invoice"
                obj_cls.Document_Index = grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString
                obj_cls.Document_No = DocNO
                obj_cls.Log_Type_ID = 1006
                obj_cls.Insert_Master()


                W_MSG_Information("ออก Invoice เรียบร้อย")
                getSOViewSearch()

            Catch ex As Exception
                myTrans.Rollback()
                Throw ex
            End Try
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdSOView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOView.CellClick
        Try
            If String.IsNullOrEmpty(grdSOView.CurrentRow.Cells("col_Invoice_No").Value.ToString) Then
                btnGenInvoice.Enabled = True
            Else
                btnGenInvoice.Enabled = False
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmInvoice_Print_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.getDistributionCenter()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getDistributionCenter()
        Dim objms_DistributionCenter As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
        Dim objDTms_DistributionCenter As DataTable = New DataTable

        Try
            objms_DistributionCenter.GetAllAsDataTable("")
            objDTms_DistributionCenter = objms_DistributionCenter.DataTable

            cboDistributionCenter.DisplayMember = "Description"
            cboDistributionCenter.ValueMember = "DistributionCenter_Index"
            cboDistributionCenter.DataSource = objDTms_DistributionCenter

        Catch ex As Exception
            Throw ex
        Finally
            objms_DistributionCenter = Nothing
            objDTms_DistributionCenter = Nothing
        End Try

    End Sub
    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            Dim bool_chkSelect As Boolean = False
            For i As Integer = 0 To grdSOView.Rows.Count - 1
                If (grdSOView.Rows(i).Cells("chkselect").Value) Then
                    bool_chkSelect = True
                End If
            Next
            If Not bool_chkSelect Then
                W_MSG_Information_ByIndex(400060)
                Exit Sub
            End If
            Dim _dt As New DataTable
            'Dim _drArr() As DataRow
            '_drArr = CType(grdSOView.DataSource, DataTable).Select(" chkselect = 1")
            _dt = CType(grdSOView.DataSource, DataTable)
            Dim str_In As String = ""
            Dim j As Integer = 0
            For Each dr As DataRow In _dt.Rows '.Select(" chkselect = 1")

                If (grdSOView.Rows(j).Cells("chkselect").Value) Then
                   
                    str_In &= "'" & dr("SalesOrder_Index").ToString & "'"

                End If
                j += 1
            Next
            Dim str_Sql As String = str_In.Replace("''", "','")
            Dim ds As New DataSet
            Dim _exc As New DBType_SQLServer
            _dt = _exc.DBExeQuery("Select * from View_ExportInvoiceTOWin_Excel where SalesOrder_Index in (" & str_Sql & ")")
            Dim objExport As New Export_Excel_KC
            _dt.Columns.Remove("SalesOrder_Index")
            ds.Tables.Add(_dt)
            ds.Tables(0).TableName = "Invoice"
            objExport.export(ds, "Invoice_" & Now.ToString("yyyyMMdd_HHmm"))
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If grdSOView.RowCount > 0 Then
                For iRow As Integer = 0 To grdSOView.RowCount - 1
                    grdSOView.Rows(iRow).Cells("chkSelect").Value = Me.chkSelectAll.Checked
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnPrintPickingList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPickingList.Click
        Try
            Dim clsReport As New clsReport
            If grdSOView.Rows.Count <= 0 Then
                W_MSG_Error("กรุณาเลือกรายการ")
                Exit Sub
                '  Dim frme = ""
            End If
            Dim salesOrder_no As String = grdSOView.Rows(grdSOView.CurrentRow.Index.ToString()).Cells("Col_SalesOrder_No").Value.ToString()
            Dim Customer_Index As String = grdSOView.Rows(grdSOView.CurrentRow.Index.ToString()).Cells("Col_Customer_Index").Value.ToString()

            Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain

            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            cry = clsReport.GetReportInfoPickingLIst("rptPackingLIst", salesOrder_no)

            'Customer_Index = "0010000000012"
            cry.SetParameterValue("Customer_Index", Customer_Index)
            frm.CrystalReportViewer1.ReportSource = cry
            frm.ShowDialog()

        


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If (Me.grdSOView.Rows.Count = 0) Then
                W_MSG_Information(String.Format("ไม่พบข้อมูล"))
                Exit Sub
            End If
            Dim ds As New DataSet

            Dim objExport As New Export_Excel_KC
            ds.Tables.Add(objExport.DataGridViewToDataTable(Me.grdSOView))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm")
            objExport.export(ds, Me.Text)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportText.Click
        Try
            Dim bool_chkSelect As Boolean = False
            For i As Integer = 0 To grdSOView.Rows.Count - 1
                If (grdSOView.Rows(i).Cells("chkselect").Value) Then
                    bool_chkSelect = True
                End If
            Next
            If Not bool_chkSelect Then
                W_MSG_Information_ByIndex(400060)
                Exit Sub
            End If

            Dim _dt As New DataTable
            _dt = CType(grdSOView.DataSource, DataTable)
            Dim str_In As String = ""
            Dim j As Integer = 0
            For Each dr As DataRow In _dt.Rows '.Select(" chkselect = 1")
                If (grdSOView.Rows(j).Cells("chkselect").Value) Then
                    str_In &= "'" & dr("SalesOrder_Index").ToString & "'"
                End If
                j += 1
            Next

            'Thread.CurrentThread.CurrentCulture = New CultureInfo("th-TH")
            'Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture

            Dim str_Sql As String = str_In.Replace("''", "','")
            Dim ds As New DataSet
            Dim _exc As New DBType_SQLServer
            _dt = _exc.DBExeQuery("Select * from View_ExportInvoiceTOWin_Excel where SalesOrder_Index in (" & str_Sql & ") order by invno,seq ")
            _dt.Columns.Remove("SalesOrder_Index")
            _dt.Columns.Remove("Seq")

            'Dim myStream As IO.Stream
            Dim saveFileDialog1 As New SaveFileDialog()

            saveFileDialog1.Filter = "txt files (*.txt)|*.txt"
            saveFileDialog1.FilterIndex = 0
            saveFileDialog1.RestoreDirectory = True

            saveFileDialog1.FileName = "creditsale"
            If saveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                'Dim writer  = New IO.StreamWriter(saveFileDialog1.FileName, True)
                Dim writer As IO.StreamWriter = New IO.StreamWriter(saveFileDialog1.OpenFile(), System.Text.Encoding.Default) 'ภาษาไทยได้แต่เข้า winspeed(รับ ASCII) ไม่ได้
                If (writer IsNot Nothing) Then
                    ' Code to write the stream goes here.
                    If _dt.Rows.Count > 0 Then
                        'Header
                        Dim _data As String = ""
                        For jj As Integer = 0 To _dt.Columns.Count - 1
                            _data &= _dt.Columns(jj).ColumnName.ToString
                            If jj <> _dt.Columns.Count Then
                                _data &= vbTab
                            End If
                        Next
                        writer.WriteLine(_data)
                        ''Data
                        Dim invno As String = ""
                        Dim bFrist As Boolean = False
                        Dim iData As String = ""
                        For i As Integer = 0 To _dt.Rows.Count - 1
                            _data = ""
                            If invno <> _dt.Rows(i).Item("invno").ToString Then
                                bFrist = True
                            Else
                                bFrist = False
                            End If
                            invno = _dt.Rows(i).Item("invno").ToString
                            For jj As Integer = 0 To _dt.Columns.Count - 1
                                iData = _dt.Rows(i).Item(jj).ToString
                                Select Case _dt.Columns(jj).ColumnName.ToString
                                    Case "vatbase", "vatrate", "vat", "totalamount", "description"
                                        If Not bFrist Then
                                            iData = ""
                                        End If
                                End Select
                                _data &= iData
                                If jj <> _dt.Columns.Count Then
                                    _data &= vbTab
                                End If
                            Next
                            writer.WriteLine(_data)
                        Next
                    End If
                    writer.Close()

                    'TXT_EXP_NUM
                    _exc.DBExeQuery("UPDATE tb_SalesOrder SET TXT_EXP_NUM = ISNULL(TXT_EXP_NUM,0) + 1 where SalesOrder_Index in (" & str_Sql & ") ")
                    Dim obj_cls As New cls_syAditlog
                    obj_cls.Process_ID = 10
                    obj_cls.Description = "Export invoice text to winspeed index ( " & str_Sql & " )"
                    obj_cls.Document_Index = ""
                    obj_cls.Document_No = _dt.Rows(0)("invno").ToString
                    obj_cls.Log_Type_ID = 1007
                    If obj_cls.Insert_Master() Then
                        W_MSG_Information("Data Exported")
                        Process.Start("explorer.exe", IO.Path.GetDirectoryName(saveFileDialog1.FileName))

                    End If

                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally

            'Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB") 'New CultureInfo("en-US")
            'Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture

        End Try
    End Sub

    Private Sub txtSalerId_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSalerId.TextChanged

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelInvoice.Click
        Try
            If IsNothing(grdSOView.CurrentRow) Then
                W_MSG_Information("กรุณาเลือก SO ที่จะต้องการ Invoice ก่อน")
                Exit Sub
            End If
            If W_MSG_Confirm("ต้องการยกเลิก Invoice ใช่หรือไม่") = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If

            If grdSOView.CurrentRow.Cells("col_Invoice_No").Value.ToString = "" Then
                W_MSG_Information("กรุณาเลือก SO ที่จะมีการสร้าง Invoice ก่อน")
                Exit Sub
            End If

            Dim _SqlExc2 As New DBType_SQLServer
            _SqlExc2 = New DBType_SQLServer
            _SqlExc2.DBExeNonQuery(String.Format("Update tb_SalesOrder set Invoice_No = '{0}',Invoice_Date=Null,TXT_EXP_NUM=0 where SalesOrder_Index = '{1}'", "", grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString))

            Dim obj_cls As New cls_syAditlog
            obj_cls.Process_ID = 10
            obj_cls.Description = "Cancel invoice"
            obj_cls.Document_Index = grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString
            obj_cls.Document_No = grdSOView.CurrentRow.Cells("col_Invoice_No").Value.ToString
            obj_cls.Log_Type_ID = 1008
            obj_cls.Insert_Master()


            W_MSG_Information("ยกเบิก Invoice เรียบร้อย")
            getSOViewSearch()


        Catch ex As Exception

        End Try
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class