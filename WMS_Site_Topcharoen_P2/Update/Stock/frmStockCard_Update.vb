Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_OAW_Report
Public Class frmStockCard_Update
    Private strSKUIndex As String = ""
    ' ================================================================= 
    ' VIEW used in this module:
    '  - VIEW_Tran_Card: For 1st Tab
    '  - VIEW_LocationBalance: 2nd Tab
    '  - VIEW_Tran_Order: 3rd Tab
    ' ================================================================= 
#Region " FORM LOAD "
    Private Sub frmStockCard_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ' ------ Set Language Begin ------
            Dim objLang As New W_Language
            Me.grdTran.AutoGenerateColumns = False

            objLang.SwitchLanguage(Me, 2057)
            objLang.SW_Language_Column(Me, Me.grdTran, 2057)
            objLang.SW_Language_Column(Me, Me.grdSumStock, 2057)
            objLang.SW_Language_Column(Me, Me.grdOrder, 2057)

            objLang = Nothing
            Application.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
            ' ------ Set Language End ------

            dtpFromDate_M.Value = (Now.Day & "/" & Now.Month & "/" & Now.Year)
            dtpToDate_M.Value = (Now.Day & "/" & Now.Month & "/" & Now.Year) ' = (Date.DaysInMonth(Date.Now.Year, Date.Now.Month) & "/" & Now.Month & "/" & Now.Year)
            Me.chkDate.Checked = True

            getItemStatus_To_ComboBox()
            cboItemStatus.Text = ""
            SetDEFAULT_CUSTOMER_INDEX()
            cbShowMode.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub
#End Region

#Region " INITIAL CONTROL "
    Private Sub getItemStatus_To_ComboBox()

        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatus()
            objDT = objClassDB.DataTable

            With Me.cboItemStatus
                .DisplayMember = "ItemStatus"
                .ValueMember = "ItemStatus_Index"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Public Sub SetDEFAULT_CUSTOMER_INDEX()
        Try

            Dim tCustomer_Index As String '= objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_CUSTOMER_INDEX")
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            tCustomer_Index = oUser.GetUserByCustomerDefault()

            If Not String.IsNullOrEmpty(tCustomer_Index) Then
                Me.txtCustomerID.Tag = tCustomer_Index
                Me.getCustomer()
            End If

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'objDT = Nothing
            'objCustomSetting = Nothing
        End Try

    End Sub

#End Region

#Region " TEXT & TAB EVENT "
    'Private Sub txtPack_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim NamePack As String = ""
    '        NamePack = txtPackage.Text
    '        lblUOMTotalQty.Text = NamePack

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub
    'Private Sub txtSkuID_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSKUID.Leave
    '    Try
    '        If txtSKUID.Text.Trim <> "" Then
    '            Me.getSukDetail()

    '            If txtCustomerID.Text.Trim <> "" Then
    '                Me.calTotalQty()
    '            End If
    '        End If


    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    Private Sub txtCustomer_Id_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtCustomerID.TextChanged

        'Try
        '    If txtSKUID.Text.Trim <> "" Then
        '        Me.getSukDetail()

        '        If txtCustomerID.Text.Trim <> "" Then
        '            Me.calTotalQty()
        '        End If
        '    End If
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try

    End Sub
    Private Sub tabStockCard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabStockCard.Click
        Try
            Dim intTabIndex As Integer = tabStockCard.SelectedIndex


            If intTabIndex = 1 Then
                Me.getLocation()
            Else : intTabIndex = 2
                Me.getOrderNotPutaway()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
#End Region

#Region " BUTTON EVENT "
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If Me.chkCustomer.Checked Then
                If Me.txtCustomerID.Text.Trim = "" Then
                    W_MSG_Information("กรุณาป้อนข้อมูลลูกค้า")
                    Exit Sub
                End If
            End If
            If Me.chkSKU.Checked = True And txtSKUID.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนข้อมูลสินค้า")
                Exit Sub
            End If

            'If txtSKUID.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาป้อนข้อมูลสินค้า")
            '    Exit Sub
            'End If

            If chkLot.Checked = True And txtLot.Text = "" Then
                W_MSG_Information_ByIndex(11)
                Exit Sub
            End If

            If chkStatus.Checked = True And cboItemStatus.Text = "" Then
                W_MSG_Information_ByIndex(12)
                Exit Sub
            End If

            Me.getSearchForTran()

            'Me.getSukDetail()
            'Me.calTotalQty()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnCustomer_PopUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_PopUp.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            ' - Receive value back from popup -
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index
            If Not tmpCustomer_Index = "" Then
                Me.txtCustomerID.Tag = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me.txtCustomerID.Tag = ""
                Me.txtCustomerID.Text = ""
                Me.txtCustomerName.Text = ""
            End If
            If Me.txtCustomerID.Text = "" Or Me.txtCustomerName.Text = "" Then
                Me.txtCustomerID.Tag = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub btnPullSKU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If txtSKUID.Text = "" Then
                W_MSG_Information("กรุณากรอกรหัสสินค้า")
            End If
            'Me.getSukDetail()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSku_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Popup.Click
        Try
            Dim frmPopup As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me.txtCustomerID.Tag)
            frmPopup.ShowDialog()
            frmPopup.Close()
            txtSKUID.Text = frmPopup.Sku_ID
            txtSKUID.Tag = frmPopup.Sku_Index
            txtSKUNameThai.Text = frmPopup.Sku_Des_th

            Me.grdOrder.DataSource = Nothing
            Me.grdSumStock.DataSource = Nothing
            Me.grdTran.DataSource = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            'Dim frmReport As New frmReportViewerMain
            'frmReport.Report_Name = "rptStockCard"
            'If Me.chkSKU.Checked = True Then
            '    frmReport.ParameterSku = txtSKUID.Text
            '    frmReport.ParameterSkuDes = txtProductName.Text
            'Else
            '    frmReport.ParameterSku = " - "
            '    frmReport.ParameterSkuDes = " - "
            'End If

            'frmReport.ParameterRPT_2 = dtpToDate_M.Value.ToString
            'frmReport.ParameterRPT_1 = dtpFromDate_M.Value.ToString
            'frmReport.ParameterRPT_2 = dtpToDate_M.Value.ToString
            'frmReport.Document_Index = Set_strSQL()
            'frmReport.ShowDialog()


            Dim oconfig_Report As New WMS_STD_OAW_Report.config_Report
            Dim frm As New frmReportViewerMain
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oconfig_Report.GetReportInfo_ReportMaster("rptStockCard", Set_strSQL())
            'Dim str
            'If Me.grdTran.RowCount > 0 Then

            'End If
            If Me.chkSKU.Checked = True Then
                cry.SetParameterValue("Ssku", txtSKUID.Text)
                cry.SetParameterValue("SskuDes", txtSKUNameThai.Text)
            Else
                cry.SetParameterValue("Ssku", "-")
                cry.SetParameterValue("SskuDes", "-")
            End If


            cry.SetParameterValue("BDate", dtpFromDate_M.Value.ToString)
            cry.SetParameterValue("EDate", dtpToDate_M.Value.ToString)
            frm.CrystalReportViewer1.ReportSource = cry
            frm.ShowDialog()



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region " GENERIC FUNCTIONS AND SUBS "
    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me.txtCustomerID.Tag.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me.txtCustomerID.Tag = objDT.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomerID.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomerName.Text = objDT.Rows(0).Item("Customer_Name").ToString
            Else
                Me.txtCustomerID.Tag = ""
                Me.txtCustomerID.Text = ""
                Me.txtCustomerName.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    'Private Sub getSukDetail()
    '    Try
    '        Dim strSQL As String = ""

    '        ' Todd: 01 Apr 2009 - Change form using VIEW to be direct SQL statement.
    '        ' This is because we want to get rid of bad VIEW with bad name.
    '        'StrSql = "select *"
    '        'StrSql += " from  VSC_Sku_Header"
    '        'StrSql += " where Sku_Id='" & Me.txtSKUID.Text & "' "

    '        strSQL = "SELECT ms_SKU.Sku_Index, ms_SKU.UnitWeight_Index As UnitWeight, ms_Package.Description, ms_Product.Product_Name_th, "
    '        strSQL &= "       ms_SKU.Product_Index, ms_SKU.Str1 As SKU_Name_Thai, ms_SKU.Sku_Id, ms_Product.Product_Id, ms_ProductType.Description AS ProductType_Description "
    '        strSQL &= "FROM ms_SKU LEFT OUTER JOIN "
    '        strSQL &= "     ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT OUTER JOIN "
    '        strSQL &= "     ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index LEFT OUTER JOIN "
    '        strSQL &= "     ms_ProductType ON ms_Product.ProductType_Index = ms_ProductType.ProductType_Index "
    '        strSQL &= " WHERE SKU_Id = '" & Me.txtSKUID.Text.Replace("'", "''").Trim & "' and ms_SKU.Status_Id <> -1"

    '        Dim objDb As New SQLCommands
    '        Dim objDT As New DataTable
    '        objDb.SQLComand(strSQL)
    '        objDT = objDb.DataTable

    '        'txtProductID.Clear()
    '        'txtProductName.Clear()
    '        'txtSKUNameThai.Clear()
    '        'txtProductType.Clear()
    '        'txtPackage.Clear()
    '        'txtUnitWeight.Clear()
    '        'txtTotalQty.Clear()
    '        'txtTotalVolume.Clear()
    '        'txtTotalWeight.Clear()

    '        'If objDT.Rows.Count > 0 Then
    '        '    strSKUIndex = objDT.Rows(0).Item("Sku_Index").ToString
    '        '    txtProductID.Text = objDT.Rows(0).Item("Product_Id").ToString
    '        '    txtProductName.Text = objDT.Rows(0).Item("Product_Name_th").ToString
    '        '    txtSKUNameThai.Text = objDT.Rows(0).Item("SKU_Name_Thai").ToString
    '        '    txtProductType.Text = objDT.Rows(0).Item("ProductType_Description").ToString
    '        '    txtPackage.Text = objDT.Rows(0).Item("Description").ToString
    '        '    txtUnitWeight.Text = objDT.Rows(0).Item("UnitWeight").ToString
    '        'End If


    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Function SetDateTime(ByVal DTP As DateTimePicker) As String

        Try
            DTP.Format = DateTimePickerFormat.Custom
            '  DTP.CustomFormat = "MM/dd/yyyy"
            DTP.CustomFormat = "yyyy/dd/MM"
            Dim strDate As String = DTP.Text.Trim
            DTP.Format = DateTimePickerFormat.Long
            Return strDate

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Set_strSQL() As DataTable
        Try

            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim strDate As String = ""
            Dim strOrder As String = ""

            strSQL = "SELECT *,'' as Invoice_No "
            strSQL &= " FROM VIEW_Tran_Card "


            If (Me.chkLocation_Alias.Checked) Or (Me.chkLocation_Alias2.Checked) Then
                Dim arrLocation_Alias() As String = Me.txtLocation_Alias.Text.Split(vbNewLine)
                Dim Location_AliasList As New List(Of String)
                For Each str As String In arrLocation_Alias
                    If (Not str.Trim() = Nothing) Then
                        Location_AliasList.Add(str.Trim())
                    End If
                Next
                If (Location_AliasList.Count > 0) Then
                    Dim str As String = String.Join("','", Location_AliasList.ToArray())
                    If (Me.chkLocation_Alias.Checked) And (Me.chkLocation_Alias2.Checked) Then
                        strWhere &= String.Format(" and ( From_Location in ('{0}') or  To_Location in ('{0}') ) ", str)
                    ElseIf (Me.chkLocation_Alias.Checked) And (Me.chkLocation_Alias2.Checked = False) Then
                        strWhere &= String.Format(" and From_Location in ('{0}')", str)
                    ElseIf (Me.chkLocation_Alias.Checked = False) And (Me.chkLocation_Alias2.Checked) Then
                        strWhere &= String.Format(" and To_Location in ('{0}')", str)
                    End If

                End If
            End If


            If (Me.chkWarehouse.Checked) Or (Me.chkWarehouse2.Checked) Then
                Dim arrWarehouse() As String = Me.txtWarehouse.Text.Split(vbNewLine)
                Dim WarehouseList As New List(Of String)
                For Each str As String In arrWarehouse
                    If (Not str.Trim() = Nothing) Then
                        WarehouseList.Add(str.Trim())
                    End If
                Next
                If (WarehouseList.Count > 0) Then
                    Dim str As String = String.Join("','", WarehouseList.ToArray())
                    If (Me.chkWarehouse.Checked) And (Me.chkWarehouse2.Checked) Then
                        strWhere &= String.Format(" and ( From_Warehouse in ('{0}') or  To_Warehouse in ('{0}') ) ", str)
                    ElseIf (Me.chkWarehouse.Checked) And (Me.chkWarehouse2.Checked = False) Then
                        strWhere &= String.Format(" and From_Warehouse in ('{0}')", str)
                    ElseIf (Me.chkWarehouse.Checked = False) And (Me.chkWarehouse2.Checked) Then
                        strWhere &= String.Format(" and To_Warehouse in ('{0}')", str)
                    End If
                End If
            End If



            If Me.chkCustomer.Checked Then
                strWhere &= "  AND (Customer_Index ='" & Me.txtCustomerID.Tag & "') "
            End If
            If Me.chkSKU.Checked = True Then
                strWhere &= " AND (Sku_Id = '" & Me.txtSKUID.Text.Replace("'", "''").Trim & "') "
            End If
            If chkLot.Checked = True Then
                'strWhere &= " AND (PLot ='" & Me.txtLot.Text.Replace("'", "''").Trim & "') "
                strWhere &= " AND (PLot like '%" & Me.txtLot.Text & "') "
            End If
            If chkStatus.Checked = True Then
                strWhere &= " AND (ItemStatus_Index ='" & Me.cboItemStatus.SelectedValue & "') "
            End If
            'If chkpallte_No.Checked = True Then
            '    strWhere &= " AND (Pallet_No ='" & Me.txtpallteNo.Text & "') "
            'End If

            If chkpallte_No.Checked = True Then
                strWhere &= " AND (Tag_No ='" & Me.txtpallteNo.Text & "' or  Tag_NoNew ='" & Me.txtpallteNo.Text & "')  "
            End If


            Dim StartDate As String = Format(dtpFromDate_M.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
            Dim EndtDate As String = Format(dtpToDate_M.Value, "yyyy/MM/dd").ToString() + "  23:59"

            If chkDate.Checked Then
                strDate = " And Transation_Date BETWEEN '" & StartDate & "' AND '" & EndtDate & "'"
            End If
            Select Case Me.cbShowMode.SelectedIndex
                Case 1
                    'strWhere &= " AND Process_ID in(1,2) " 'and (isnull((select dbo.GETSTATUS_MoveMent(Process_Id,Transaction_id)),0) <> -1)"
                    strWhere &= " AND Process_ID in(1,2)   AND   Order_Index  NOT in (SELECT Order_Index  FROM tb_order where status = -1)"
            End Select

            Dim strWhereAll As String = " WHERE 1=1 " & strWhere

            strOrder = " ORDER BY Transation_Date, Transaction_Index ASC"
            strSQL = strSQL & strWhereAll & strDate & strOrder
            'Return strSQL

            Dim objDb1 As New SQLCommands
            Dim objDT1 As New DataTable
            Dim objDT2 As New DataTable
            objDb1.SQLComand(strSQL)
            objDT1 = objDb1.DataTable

            If objDT1.Rows.Count > 0 Then
                Dim dateMin As DateTime
                dateMin = CDate(objDT1.Rows(0)("Transation_Date"))
                Dim QtyBal_Begin As Double = 0
                strSQL = " SELECT isnull(sum(Qty_In)-Sum(Qty_Out),0) as Qty_Bal_Begin "
                strSQL &= " FROM VIEW_Tran_Card  WHERE Transation_Date < '" & dateMin.ToString("yyyy/MM/dd") & "' AND Process_Id in (1,2) "
                strSQL &= strWhere
                objDb1.SQLComand(strSQL)
                objDT2 = objDb1.DataTable
                If objDT2.Rows.Count > 0 Then
                    QtyBal_Begin = objDT2.Rows(0).Item("Qty_Bal_Begin")
                End If

                For Each drMoveMent As DataRow In objDT1.Rows
                    QtyBal_Begin = (QtyBal_Begin + drMoveMent("Qty_In")) - drMoveMent("Qty_Out")
                    drMoveMent("Qty_Sku_Bal") = QtyBal_Begin
                    If drMoveMent("Qty_Out") > 0 Then
                        drMoveMent("Invoice_No") = drMoveMent("Invoice_In")
                    Else
                        drMoveMent("Invoice_No") = drMoveMent("Invoice_Out")
                    End If
                Next
                'Dim dblQty_In As Double = objDT1.Compute("Sum(Qty_In)", "Qty_In >= 0")
                'Dim dblQty_Out As Double = objDT1.Compute("Sum(Qty_Out)", "Qty_Out >= 0")
                Dim dblQty_In As Double = objDT1.Compute("Sum(Qty_In)", "")
                'แก้ไข by bo แก้ไขส่วนของdblQty_Out เนื่องจากถ้าQty_Out = 0 โปรแกรมจะerror
                Dim qty_out As Integer = 0 ' objDT1.Rows(0)("Qty_Out")
                Dim dblQty_Out As Double
                'If qty_out = 0 Then
                '    dblQty_Out = 0
                'Else

                dblQty_Out = objDT1.Compute("Sum(Qty_Out)", "")

                'End If
                'Dim dblQty_Out As Double = objDT1.Compute("Sum(Qty_Out)", "Qty_Out <> 0")


                txtSum_In.Text = Format(dblQty_In, "#,##0.00")
                txtSum_Out.Text = Format(dblQty_Out, "#,##0.00")
                txtSum_Bal.Text = Format(QtyBal_Begin, "#,##0.00")

            End If

            Return objDT1

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Private Function getDatatable_Report() As DataTable
    '    Dim objDb As New SQLCommands
    '    Dim objDT As New DataTable
    '    objDb.SQLComand(Set_strSQL)
    '    objDT = objDb.DataTable
    '    Return objDT
    'End Function

    Private Sub getSearchForTran()
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim strDate As String = ""
            Dim strOrder As String = ""

            Me.grdTran.DataSource = Set_strSQL()
            Me.grdTran.Update()

            'txtSum_In.Text = Format(dblQty_In, "#,##0.00")
            'txtSum_Out.Text = Format(dblQty_Out, "#,##0.00")
            'txtSum_Bal.Text = Format(dblQty_Bal, "#,##0.00")

            'strSQL = Me.Set_strSQL

            'Dim objDb As New SQLCommands
            'Dim objDT As New DataTable
            'objDb.SQLComand(strSQL)
            'objDT = objDb.DataTable

            'Me.grdTran.Rows.Clear()
            'Me.grdTran.Refresh()


            'txtProductID.Clear()
            'txtProductName.Clear()
            'txtSKUNameThai.Clear()
            'txtProductType.Clear()
            'txtPackage.Clear()
            'txtUnitWeight.Clear()
            ''txtTotalQty.Clear()
            ''txtTotalVolume.Clear()
            ''txtTotalWeight.Clear()

            'If objDT.Rows.Count = 0 Then

            '    W_MSG_Information("ไม่พบข้อมูล")
            '    Exit Sub
            'End If

            'Dim dblTotal_Qty As Double = 0
            'Dim dblQty_In As Double = 0.0
            'Dim dblQty_Out As Double = 0.0
            'Dim dblQty_Bal As Double = 0.0

            'For i As Integer = 0 To objDT.Rows.Count - 1
            '    With Me.grdTran
            '        Me.grdTran.Rows.Add()

            '        .Rows(i).Cells("system_index").Value = objDT.Rows(i).Item("Transaction_Index").ToString
            '        .Rows(i).Cells("Tran_Date").Value = Format(CDate(objDT.Rows(i).Item("Transation_Date")), "dd/MM/yyyy").ToString()
            '        .Rows(i).Cells("Tran_Id").Value = objDT.Rows(i).Item("Transaction_Id").ToString
            '        .Rows(i).Cells("Col_Ref_No1").Value = objDT.Rows(i).Item("Ref_No1").ToString
            '        .Rows(i).Cells("ColDocumentType").Value = objDT.Rows(i).Item("DocuDes").ToString '
            '        .Rows(i).Cells("TagNo").Value = objDT.Rows(i).Item("Tag_No").ToString
            '        .Rows(i).Cells("Qty_In").Value = objDT.Rows(i).Item("Qty_In").ToString
            '        .Rows(i).Cells("Qty_out").Value = objDT.Rows(i).Item("Qty_Out").ToString

            '        dblQty_In += CDbl(objDT.Rows(i).Item("Qty_In").ToString)
            '        dblQty_Out += CDbl(objDT.Rows(i).Item("Qty_Out").ToString)

            '        '' set Lot and สถานะ = false
            '        'If chkLot.Checked = False And chkStatus.Checked = False Then
            '        '    .Rows(i).Cells("total_Qtv").Value = objDT.Rows(i).Item("Qty_Sku_Bal").ToString 'กรณีที่ ไม่เลือก lot กับสถานะ
            '        'End If
            '        '' set Lot = true
            '        'If chkLot.Checked = True And chkStatus.Checked = False Then
            '        '    .Rows(i).Cells("total_Qtv").Value = objDT.Rows(i).Item("Qty_PLot_Bal").ToString 'กรณีที่เลือก  lot
            '        'End If
            '        '' setสถานะ = true
            '        'If chkLot.Checked = True And chkStatus.Checked = True Then
            '        '    .Rows(i).Cells("total_Qtv").Value = objDT.Rows(i).Item("Qty_ItemStatus_Bal").ToString 'กรณีที่เลือก สินค้า สถานะ lot
            '        'End If

            '        'Dong_Edit 




            '        ' dblTotal_Qty = dblTotal_Qty + CDbl(objDT.Rows(i).Item("Qty_In").ToString) - CDbl(objDT.Rows(i).Item("Qty_Out").ToString)
            '        'If dblTotal_Qty < 0 Then
            '        '    .Rows(i).Cells("total_Qtv").Value = dblTotal_Qty * -1
            '        'Else
            '        '    .Rows(i).Cells("total_Qtv").Value = dblTotal_Qty
            '        'End If

            '        dblTotal_Qty = dblTotal_Qty + CDbl(objDT.Rows(i).Item("Qty_Sku_Bal").ToString)
            '        .Rows(i).Cells("total_Qtv").Value = CDbl(objDT.Rows(i).Item("Qty_Sku_Bal").ToString)



            '        .Rows(i).Cells("Pack").Value = objDT.Rows(i).Item("package_Name").ToString
            '        .Rows(i).Cells("Lot").Value = objDT.Rows(i).Item("PLot").ToString
            '        .Rows(i).Cells("Status").Value = objDT.Rows(i).Item("StatusName").ToString
            '        .Rows(i).Cells("Des_From").Value = objDT.Rows(i).Item("From_Location").ToString
            '        .Rows(i).Cells("Des_To").Value = objDT.Rows(i).Item("To_Location").ToString
            '        .Rows(i).Cells("ColItemDefinition").Value = objDT.Rows(i).Item("ItemDefinition_Name1").ToString
            '        .Rows(i).Cells("Coldate").Value = objDT.Rows(i).Item("add_date").ToString
            '        .Rows(i).Cells("Col_pallteNo").Value = objDT.Rows(i).Item("Pallet_No").ToString
            '        .Rows(i).Cells("Col_SKU_ID").Value = objDT.Rows(i).Item("SKU_ID").ToString
            '        .Rows(i).Cells("Col_Sku_name_en").Value = objDT.Rows(i).Item("Sku_name_en").ToString
            '        .Rows(i).Cells("Col_Sku_name_th").Value = objDT.Rows(i).Item("Sku_name_th").ToString
            '        dblQty_Bal = .Rows(i).Cells("total_Qtv").Value
            '    End With
            'Next

            'txtSum_In.Text = Format(dblQty_In, "#,##0.00")
            'txtSum_Out.Text = Format(dblQty_Out, "#,##0.00")
            'txtSum_Bal.Text = Format(dblQty_Bal, "#,##0.00")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub getLocation()
        Try
            Dim strSql As String = ""
            Dim strWhere As String = ""

            strSql = "SELECT *  "
            strWhere = " FROM VIEW_LocationBalance "
            strWhere &= " WHERE "
            strWhere &= " (Sku_Id = '" & Me.txtSKUID.Text.Trim & "') "
            strWhere &= " AND (Customer_Index ='" & Me.txtCustomerID.Tag & "') "
            strWhere &= " ORDER BY Location_Alias ASC "

            strSql = strSql + strWhere
            Dim objDb As New SQLCommands
            Dim objDT As New DataTable
            objDb.SQLComand(strSql)
            objDT = objDb.DataTable

            Me.grdSumStock.Rows.Clear()
            Me.grdSumStock.Refresh()

            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdSumStock
                    Me.grdSumStock.Rows.Add()

                    .Rows(i).Cells("LocationBalance_Index").Value = objDT.Rows(i).Item("LocationBalance_Index").ToString
                    .Rows(i).Cells("Tag_No").Value = objDT.Rows(i).Item("Tag_No").ToString
                    .Rows(i).Cells("Location_Index").Value = objDT.Rows(i).Item("Location_Alias").ToString
                    .Rows(i).Cells("Order_Date").Value = Format(CDate(objDT.Rows(i).Item("Order_Date")), "dd/MM/yyyy").ToString  '  ' Format(Today, "dd/MM/yyyy").ToString 
                    .Rows(i).Cells("Plot").Value = objDT.Rows(i).Item("Plot").ToString
                    .Rows(i).Cells("ItemStatus_Index").Value = objDT.Rows(i).Item("ItemStatus_Des").ToString
                    .Rows(i).Cells("ColQtyBegin").Value = Format(Val(objDT.Rows(i).Item("Qty_Bal_Begin").ToString), "#,##0.00").ToString
                    .Rows(i).Cells("Qty_Bal").Value = Format(Val(objDT.Rows(i).Item("Qty_Bal").ToString), "#,##0.00").ToString
                    .Rows(i).Cells("Sku_Package").Value = objDT.Rows(i).Item("Package_sku").ToString
                    .Rows(i).Cells("MfgDate").Value = Format(CDate(objDT.Rows(i).Item("Mfg_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("ExpDate").Value = Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("Weight_Bal").Value = Format(Val(objDT.Rows(i).Item("Weight_Bal").ToString), "#,##0.00").ToString
                    .Rows(i).Cells("Volume_Bal").Value = Format(Val(objDT.Rows(i).Item("Volume_Bal").ToString), "#,##0.00").ToString
                    .Rows(i).Cells("Pallet_Name").Value = objDT.Rows(i).Item("Pallet_Name").ToString
                    .Rows(i).Cells("Pallet_Qty").Value = Format(Val(objDT.Rows(i).Item("Pallet_Qty").ToString), "#,##0").ToString
                    .Rows(i).Cells("ColSupplier").Value = objDT.Rows(i).Item("Supplier_Name").ToString
                    '.Rows(i).Cells("ColPallNo").Value = objDT.Rows(i).Item("Str5").ToString 
                    .Rows(i).Cells("ColOrderNo").Value = objDT.Rows(i).Item("Order_No").ToString
                    .Rows(i).Cells("col_Addby2").Value = objDT.Rows(i).Item("add_by").ToString

                End With
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub getOrderNotPutaway()
        Try
            Dim strSql As String = ""
            Dim strWhere As String = ""

            strSql = "SELECT *  "
            strWhere = " FROM VIEW_Tran_Order "
            strWhere &= "  WHERE "
            strWhere &= "  (Sku_Id = '" & Me.txtSKUID.Text.Trim & "') "
            strWhere &= "   AND (Customer_Index ='" & Me.txtCustomerID.Tag & "') "
            strWhere &= " ORDER BY Order_No ASC, OrderItem_Index ASC"

            strSql = strSql + strWhere

            Dim objDb As New SQLCommands
            Dim objDT As New DataTable
            objDb.SQLComand(strSql)
            objDT = objDb.DataTable

            Me.grdOrder.Rows.Clear()
            Me.grdOrder.Refresh()

            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdOrder
                    Me.grdOrder.Rows.Add()
                    ' .Rows(i).Cells("ColOrderItem_Index").Value = objDT.Rows(i).Item("OrderItem_Index").ToString
                    .Rows(i).Cells("ColSku_Id").Value = objDT.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("ColDes").Value = objDT.Rows(i).Item("str1").ToString
                    .Rows(i).Cells("ColOrder_No").Value = objDT.Rows(i).Item("Order_No").ToString
                    .Rows(i).Cells("ColOrder_Date").Value = Format(CDate(objDT.Rows(i).Item("Order_Date")), "dd/MM/yyyy").ToString  '  ' Format(Today, "dd/MM/yyyy").ToString 
                    .Rows(i).Cells("ColLot").Value = objDT.Rows(i).Item("PLot").ToString
                    .Rows(i).Cells("ColQty").Value = objDT.Rows(i).Item("Total_Qty").ToString
                    .Rows(i).Cells("ColStatus").Value = objDT.Rows(i).Item("DesSta").ToString
                    .Rows(i).Cells("ColUnit").Value = objDT.Rows(i).Item("Description").ToString
                    .Rows(i).Cells("ColMfg").Value = Format(CDate(objDT.Rows(i).Item("Mfg_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("ColExp").Value = Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("ColPalletNo").Value = objDT.Rows(i).Item("Str5").ToString

                End With
            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Private Sub calTotalQty()
    '    Try
    '        'If txtSKUID.Text = "" Then
    '        '    W_MSG_Information_ByIndex(31)
    '        '    Exit Sub
    '        'End If

    '        'If txtCustomerID.Text = "" Then
    '        '    W_MSG_Information_ByIndex(18)
    '        '    Exit Sub
    '        'End If

    '        Dim strSql As String = ""
    '        Dim strWhere As String = ""

    '        strSql = "SELECT SUM(Qty_Bal) As Qty_Bal, SUM(Weight_Bal) As Weight_Bal, SUM(Volume_Bal) As Volume_Bal "
    '        strSql &= "FROM VIEW_LocationBalance "
    '        strWhere = "WHERE (Qty_Bal > 0) "

    '        If strSKUIndex <> "" Then
    '            strWhere &= "AND (SKU_Index = '" & strSKUIndex & "') "
    '        Else
    '            strWhere &= "AND (Sku_Id = '" & Me.txtSKUID.Text.Trim & "') "
    '        End If

    '        If Me.chkCustomer.Checked Then
    '            strWhere &= "AND (Customer_Index ='" & Me.txtCustomerID.Tag & "') "
    '        End If


    '        strSql = strSql & strWhere
    '        Dim objDb As New SQLCommands
    '        Dim objDT As New DataTable
    '        Dim odtCal As New DataTable
    '        objDb.SQLComand(strSql)
    '        objDT = objDb.DataTable

    '        If objDT.Rows.Count > 0 Then
    '            With objDT.Rows(0)
    '                If .Item("Qty_Bal").ToString <> "" Then
    '                    txtTotalQty.Text = .Item("Qty_Bal").ToString
    '                Else
    '                    txtTotalQty.Text = "0.00"
    '                End If

    '                If .Item("Weight_Bal").ToString <> "" Then
    '                    txtTotalWeight.Text = .Item("Weight_Bal").ToString
    '                Else
    '                    txtTotalWeight.Text = "0.00"
    '                End If

    '                If .Item("Volume_Bal").ToString <> "" Then
    '                    txtTotalVolume.Text = .Item("Volume_Bal").ToString
    '                Else
    '                    txtTotalVolume.Text = "0.00"
    '                End If

    '            End With
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub
    ' TODO : Find better way to calculate balance
    'Sub calTotalQty()
    '    Try
    '        If txtSKUID.Text = "" Then
    '            W_MSG_Information_ByIndex(31)
    '            Exit Sub
    '        End If

    '        If txtCustomerID.Text = "" Then
    '            W_MSG_Information_ByIndex(18)
    '            Exit Sub
    '        End If

    '        Dim strSql As String = ""
    '        Dim strWhere As String = ""

    '        strSql = "  select * "
    '        strWhere = "  from View_Tran_Card "
    '        strWhere &= "  where "
    '        strWhere &= "  Sku_Id = '" & Me.txtSKUID.Text & "' "
    '        strWhere &= "   And Customer_Index ='" & Me.txtCustomerID.Tag & "' "

    '        If chkLot.Checked = False And chkStatus.Checked = False Then
    '            strWhere &= " And Qty_Sku_Bal > 0"
    '        End If
    '        'set Lot = true
    '        If chkLot.Checked = True And chkStatus.Checked = False Then
    '            strWhere &= " And Qty_PLot_Bal > 0"
    '        End If
    '        'set สถานะ = true
    '        If chkLot.Checked = True And chkStatus.Checked = True Then
    '            strWhere &= " And Qty_ItemStatus_Bal > 0"
    '        End If

    '        strSql = strSql + strWhere
    '        Dim objDb As New SQLCommands
    '        Dim objDT As New DataTable
    '        Dim odtCal As New DataTable
    '        objDb.SQLComand(strSql)
    '        objDT = objDb.DataTable

    '        Dim ProductSum As Double = 0
    '        For i As Integer = 0 To objDT.Rows.Count - 1

    '            If chkLot.Checked = False And chkStatus.Checked = False Then
    '                Dim objBal As New CheckStock_By_Order
    '                Dim Qty_Sku_Bal As Double = 0
    '                Dim Customer_Index As String = Me.txtCustomerID.Tag
    '                Dim Sku_Id As String = Me.txtSKUID.Text

    '                objBal.getQty_Sku_Bal1(Customer_Index, Sku_Id)
    '                odtCal = objBal.GetDataTable
    '                If odtCal.Rows.Count > 0 Then
    '                    Qty_Sku_Bal = odtCal.Rows(0)("Sum_Qty")
    '                End If
    '                txtTotalQty.Text = Qty_Sku_Bal
    '            End If

    '            If chkLot.Checked = True And chkStatus.Checked = False Then
    '                Dim objBal As New CheckStock_By_Order
    '                Dim Qty_Plot_Bal As Double = 0
    '                Dim Customer_Index As String = Me.txtCustomerID.Tag
    '                Dim Sku_Id As String = Me.txtSKUID.Text
    '                Dim Plot_id As String = Me.txtLot.Text


    '                objBal.getQty_Plot_Bal(Customer_Index, Sku_Id, Plot_id)
    '                odtCal = objBal.GetDataTable
    '                If odtCal.Rows.Count > 0 Then
    '                    Qty_Plot_Bal = odtCal.Rows(0)("Sum_Qty")
    '                End If
    '                txtTotalQty.Text = Qty_Plot_Bal
    '            End If

    '            If chkLot.Checked = True And chkStatus.Checked = True Then

    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmStockCard_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigStockCard
                    frm.ShowDialog()
                    Dim objLang As New W_Language
                    objLang.SwitchLanguage(Me, 2057)
                    objLang.SW_Language_Column(Me, Me.grdTran, 2057)
                    objLang.SW_Language_Column(Me, Me.grdSumStock, 2057)
                    objLang.SW_Language_Column(Me, Me.grdOrder, 2057)
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnLocation_Alias_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocation_Alias.Click
        Try
            Dim _popup As New frmLocation_Popup
            _popup.ShowDialog()
            If Not String.IsNullOrEmpty(_popup.Col_Alias) Then
                Dim xLocation_Alias As String = Me.txtLocation_Alias.Text.ToString().Trim()
                If (Not xLocation_Alias = Nothing) Then
                    xLocation_Alias = _popup.Col_Alias + vbCrLf + xLocation_Alias
                Else
                    xLocation_Alias = _popup.Col_Alias
                End If
                Me.txtLocation_Alias.Text = xLocation_Alias
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


End Class