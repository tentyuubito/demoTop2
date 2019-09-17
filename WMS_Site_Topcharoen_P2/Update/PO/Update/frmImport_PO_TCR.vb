Imports System.IO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_INB_PO_Datalayer

Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Data
Imports System.Globalization
'Imports Microsoft.Office.Interop

Public Class frmImport_PO_TCR
    Public Document_Group_Name As String = "" 'KSL
    Private _boolResult As String
    Private _Customer_Index As String = ""
    Private _DocumentType_Index As String = ""
    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Private _SafeFileName As String = ""
    Property SafeFileName() As String
        Get
            Return _SafeFileName
        End Get
        Set(ByVal value As String)
            _SafeFileName = value
        End Set
    End Property

    Private _dtHeader As New DataTable

    'Private Enum EcolumnName
    '    PO = 2
    '    PO_Date
    '    Document_Code
    '    Supplier_Code
    '    Supplier_Name
    '    Carrier_Id
    '    Carrier_Name
    '    Customer_Id
    '    SKU_Name
    '    Expected_Receive_Date
    '    Ref1
    '    Ref2
    '    Remark
    '    Qty
    '    PO_Unknonw
    '    Group
    '    ItemDesc
    '    Eye
    '    Add
    '    Tilted
    '    Color
    '    Degree
    '    BC
    '    VMI
    '    Generation
    '    Brand
    '    weightAll
    '    unitPrice
    '    buyPrice
    '    discountPrice
    '    persentDiscountPrice
    '    unitAll
    '    persentTax
    '    Tax
    '    priceAll
    '    itemRemark
    'End Enum

    'Private Sub AddcbProductType()
    '    Dim objClassDB As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable
    '    Try
    '        objClassDB.SearchData_Click("TIMCO_ShowBarcode", " AND TIMCO_ShowBarcode > 0")
    '        objDT = objClassDB.DataTable

    '        cbProductGroup.DisplayMember = "ProductType_ID"
    '        cbProductGroup.ValueMember = "ProductType_Index"
    '        cbProductGroup.DataSource = objDT

    '        If objDT.Rows.Count > 0 Then
    '            cbProductGroup.SelectedIndex = 0
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        objClassDB = Nothing
    '    End Try
    'End Sub

    Private Enum eColumnName
        Seq = 1
        PO_No
        PO_Date
        CustomerID
        SkuID
        TotalQty
        PO_Date_Expected
        Ref_No1
        Ref_No2
        Remark_Item
        Remark
    End Enum

    Private Sub frmImport_DO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language

            ''Insert
            'oFunction.Insert(Me)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me)
            '===============================
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            Me.getDocumentType(9)
            Me.btnImport.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        Dim xSQL As String = ""
        Try

            xSQL = " SELECT  DocumentType_Index,  Description FROM     ms_DocumentType"
            xSQL &= " WHERE Process_Id= " & Process_Id & " and status_id <> -1"

            objDT = objClassDB.DBExeQuery(xSQL)

            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

        'Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        'Dim objDT As DataTable = New DataTable

        'Try
        '    objClassDB.getDocumentType(Process_Id)
        '    objDT = objClassDB.DataTable

        '    With cboDocumentType
        '        .DisplayMember = "Description"
        '        .ValueMember = "DocumentType_Index"
        '        .DataSource = objDT
        '    End With

        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    objClassDB = Nothing
        '    objDT = Nothing
        'End Try

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If

            Dim objtb_PurchaseOrderItemCollection As New List(Of tb_PurchaseOrderItem)
            Dim objtb_PurchaseOrder_Discount As New tb_PurchaseOrder_Discount
            Dim objtb_PurchaseOrder_DiscountItem As New List(Of tb_PurchaseOrder_Discount)
            Dim objtb_PurchaseOrderOtherCollection As New List(Of tb_PurchaseOrderOther)

            Dim objDBIndex As New Sy_AutoNumber
            Dim oImport_PO As New bl_clsImport_TCR
            'Add on Query
            Try
                CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
                Dim dtDetail As New DataTable
                dtDetail = Me.grdPreviewData.DataSource
                Dim i As Integer = 0
                Dim groupHeader As DataTable = dtDetail.DefaultView.ToTable(True, "เลขที่ใบสั่งซื้อ", "วันที่ใบสั่งซื้อ", "เจ้าของสินค้า", "วันที่คาดว่าจะรับสินค้า", "เอกสารอ้างอิง1", "เอกสารอ้างอิง2", "หมายเหตุ/ส่วนหัว")
                For Each dtRowHeader As DataRow In groupHeader.Rows
                    Dim objtb_PurchaseOrder As New tb_PurchaseOrder
                    With dtRowHeader

                        ' ------ STEP 2: Prepare values for PO Header
                        objtb_PurchaseOrder.PurchaseOrder_Index = objDBIndex.getSys_Value("PurchaseOrder_Index") 'Me.PurchaseOrder_Index
                        objtb_PurchaseOrder.DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
                        objtb_PurchaseOrder.PurchaseOrder_No = .Item("เลขที่ใบสั่งซื้อ") 'dtDetail.item
                        objtb_PurchaseOrder.PurchaseOrder_Date = .Item("วันที่ใบสั่งซื้อ") 'Me.dtpPO_Date.Value
                        objtb_PurchaseOrder.Expected_Delivery_Date = .Item("วันที่คาดว่าจะรับสินค้า")  'Me.dtpDue_Date.Value
                        objtb_PurchaseOrder.Delivery_Date = .Item("วันที่คาดว่าจะรับสินค้า")
                        objtb_PurchaseOrder.Customer_Index = oImport_PO.GetCustomer_Index(.Item("เจ้าของสินค้า"))
                        objtb_PurchaseOrder.Supplier_Index = "0010000000001" 'oImport_PO.GetSupplier_Index(.Item("รหัสผู้ขายสินค้า"))
                        objtb_PurchaseOrder.Department_Index = ""
                        objtb_PurchaseOrder.Remark = IIf(IsDBNull(dtRowHeader.Item("หมายเหตุ/ส่วนหัว")), "", dtRowHeader.Item("หมายเหตุ/ส่วนหัว"))  ' if( isdbnull(.Item("หมายเหตุ"),"","")  'Me.txtRemark.Text
                        objtb_PurchaseOrder.Credit_Term = "" 'Me.txtCreditTerm.Text
                        objtb_PurchaseOrder.Currency_Index = "" 'IIf((1 = 1), "5", "2")
                        objtb_PurchaseOrder.Exchange_Rate = 0 'Me.txtExRate.Text
                        objtb_PurchaseOrder.PaymentMethod_Index = ""
                        objtb_PurchaseOrder.Amount = 0 'Me.txtSubtotal.Text
                        objtb_PurchaseOrder.Discount_Percent = 0 'Me.txtDiscount_Percent.Text
                        objtb_PurchaseOrder.Discount_Amt = 0 'Me.txtDiscount_Amt.Text
                        objtb_PurchaseOrder.Deposit_Amt = 0 'Me.txtDeposit_Amt.Text
                        objtb_PurchaseOrder.Total_Amt = 0 ' Me.txtSubtotal.Text
                        objtb_PurchaseOrder.VAT_Percent = 0 ' Me.txtVAT_Percent.Text
                        objtb_PurchaseOrder.VAT = 0 'Me.txtVAT.Text
                        objtb_PurchaseOrder.Net_Amt = 0 ' Me.txtNet_Amt.Text
                        objtb_PurchaseOrder.Supplier_Address = "" 'Me.txtSupplier_Address.Text.ToString.Trim
                        objtb_PurchaseOrder.Supplier_Tel = "" ' Me.txtSupplier_Phone.Text.ToString.Trim
                        objtb_PurchaseOrder.Supplier_Fax = "" ' Me.txtSupplier_Fax.Text.ToString.Trim
                        objtb_PurchaseOrder.Str1 = dtRowHeader.Item("เอกสารอ้างอิง1").ToString   ' Me.txtRef1.Text.ToString.Trim
                        objtb_PurchaseOrder.Str2 = dtRowHeader.Item("เอกสารอ้างอิง2").ToString  '""' Me.txtRef2.Text.ToString.Trim
                        objtb_PurchaseOrder.Str3 = "" ' Me.txtTax_No.Text.ToString.Trim
                        objtb_PurchaseOrder.Str4 = "" ' Me.txtShip_Phone.Text.ToString.Trim
                        objtb_PurchaseOrder.Str9 = "" 'Me.txtShip_Address.Text.ToString.Trim
                        objtb_PurchaseOrder.Str5 = "" ' Me.txtShip_Fax.Text.ToString.Trim
                        objtb_PurchaseOrder.Str10 = Me.SafeFileName
                        objtb_PurchaseOrder.add_by = WV_UserName ' Me.txtUser.Text
                        objtb_PurchaseOrder.Status = 1
                    End With

                    Dim dataDetail As DataRow() = dtDetail.Select("เลขที่ใบสั่งซื้อ = '" & objtb_PurchaseOrder.PurchaseOrder_No & "'")
                    Dim objPurchaseOrderItemCollection = New List(Of tb_PurchaseOrderItem)


                    For Each dtRowDetail As DataRow In dataDetail
                        Dim objtb_PurchaseOrderItem As New tb_PurchaseOrderItem
                        Dim dt_Sku As DataTable = oImport_PO.GetSKU_Detail(dtRowDetail(eColumnName.SkuID))

                        objtb_PurchaseOrderItem.PurchaseOrderItem_Index = objDBIndex.getSys_Value("PurchaseOrderItem_Index")

                        objtb_PurchaseOrderItem.Sku_Index = dt_Sku.Rows(0).Item("sku_index")
                        objtb_PurchaseOrderItem.Package_Index = dt_Sku.Rows(0).Item("package_index")
                        objtb_PurchaseOrderItem.Weight = dt_Sku.Rows(0).Item("UnitWeight_Index")
                        objtb_PurchaseOrderItem.UnitPrice = 0
                        objtb_PurchaseOrderItem.Discount_Amt = 0
                        objtb_PurchaseOrderItem.Amount = 0
                        objtb_PurchaseOrderItem.Qty = dtRowDetail(eColumnName.TotalQty)
                        objtb_PurchaseOrderItem.Total_Qty = dtRowDetail(eColumnName.TotalQty)
                        objtb_PurchaseOrderItem.Ratio = 1
                        objtb_PurchaseOrderItem.Remark = dtRowDetail(eColumnName.Remark_Item).ToString
                        objtb_PurchaseOrderItem.Str10 = dtRowDetail("Location").ToString
                        objtb_PurchaseOrderItem.Str1 = Trim(dtRowDetail.Item("วันที่ผลิต").ToString)
                        objtb_PurchaseOrderItem.Str2 = dtRowDetail("ItemCode").ToString
                        objtb_PurchaseOrderItem.Str3 = dtRowDetail("ขนาดแว่น").ToString
                        objtb_PurchaseOrderItem.Str4 = dtRowDetail("ประเทศที่ผลิต").ToString
                        objtb_PurchaseOrderItem.Str5 = dtRowDetail("รุ่น").ToString
                        objtb_PurchaseOrderItem.Str6 = dtRowDetail("สี").ToString
                        objtb_PurchaseOrderItem.Str7 = dtRowDetail("บริษัท").ToString

                        objtb_PurchaseOrderItemCollection.Add(objtb_PurchaseOrderItem)
                    Next

                    Dim objDBPOTransaction As New POTransaction(POTransaction.enuOperation_Type.ADDNEW, objtb_PurchaseOrder, objtb_PurchaseOrderItemCollection, objtb_PurchaseOrder_DiscountItem, objtb_PurchaseOrderOtherCollection)
                    objDBPOTransaction.SaveData()
                    objtb_PurchaseOrderItemCollection.Clear()
                    objtb_PurchaseOrder.Clear()
                    objDBPOTransaction = Nothing

                Next

                W_MSG_Information("บันทึกเสร็จสิ้น")
                grdPreviewData.DataSource = Nothing
                btnImport.Enabled = False

            Catch exx As Exception
                Throw exx
            End Try

            '  Me.grdPreviewData.DataSource = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        Finally

        End Try

    End Sub

    Private Function SearchInv(ByVal tmpCustomer_Index As String, ByVal tmpSkuID As String, ByVal tmpSkuQty As Double) As String

        Dim objClassDB As New bl_Import_SO
        Dim objDT As DataTable = New DataTable
        Dim intRatio As Integer = 0

        Try
            SearchInv = "-"

            objClassDB.SearchSkuInv(tmpCustomer_Index, tmpSkuID)
            objDT = objClassDB.GetDataTable

            If objDT.Rows.Count > 0 Then
                If Val(tmpSkuQty) > Val(objDT.Rows(0).Item("Qty_Free").ToString) Then
                    SearchInv = "จำนวนสินค้าที่เบิกได้ (Available) : " & Val(objDT.Rows(0).Item("Qty_Free").ToString)
                Else
                    SearchInv = "OK"
                End If
                Exit Function
            Else
                SearchInv = "จำนวนสินค้าคงเหลือ (Inventory) : 0 "
                Exit Function
            End If

            SearchInv = "-"

        Catch ex As Exception
            Throw ex
        Finally

            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    '#Region "   ReadTxT Import SO   "
    '    Public Function ReadAndSplit(ByVal myReader As String, ByVal strDelimited As String)
    '        Try

    '            Dim CurrLine As String = myReader
    '            Dim strSplit() As String
    '            Dim strSaleOrderNo As String = ""

    '            strSplit = CurrLine.Split(strDelimited)

    '            Return strSplit
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Sub ReadText_DO()
    '        Try
    '            Dim DetailFileList() As String = Directory.GetFiles(Me._DEFAULT_IMPORT_SO_PATH, "*.txt")
    '            For i As Integer = 0 To DetailFileList.Length - 1
    '                ReadDetail(DetailFileList(i))
    '            Next

    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Sub

    '    Public Sub ReadDetail(ByVal pstrFilePath As String)
    '        Dim strSalesOrder_No As String = ""
    '        Dim strDelimited As String = vbTab
    '        Dim obl_Import_SO_Item As New bl_Import_SO_Item

    '        Try
    '            Dim myReader As StreamReader
    '            Dim Current_Text_Line As String = ""
    '            Dim strCustomerShipping_Index As String = ""
    '            Dim strInvoice_No As String = ""
    '            Dim strSplit() As String
    '            Dim strTempText As New ArrayList
    '            Dim strSku_Index As String = ""
    '            Dim strArray(2) As String

    '            Dim iTotal_Header As Integer = 0
    '            Dim iTotal_Detail As Integer = 0

    '            Dim iCount_Comp_Header As Integer = 0
    '            Dim iCount_Comp_Detail As Integer = 0

    '            Dim iCount_Error_Header As Integer = 0
    '            Dim iCount_Error_Detail As Integer = 0
    '            ' Dim osb As New StringBuilder

    '            Dim iCurrent_Line_Number As Integer = 0

    '            Dim obl_Log As New bl_Log

    '            myReader = New StreamReader(pstrFilePath, System.Text.UnicodeEncoding.Default)


    '            _osbNewSKU = New StringBuilder
    '            _osbErr_Dup = New StringBuilder
    '            _osbErr_Data_Incomplete = New StringBuilder


    '            _osbErr_Dup.AppendLine("Error : Duplicate data")
    '            _osbErr_Dup.AppendLine("")

    '            _osbErr_Data_Incomplete.AppendLine("")
    '            _osbErr_Data_Incomplete.AppendLine("Error : Data Incomplete")
    '            _osbErr_Data_Incomplete.AppendLine("")

    '            _osbNewSKU.AppendLine("******************************************************************************")
    '            _osbNewSKU.AppendLine("[4] New SKU")
    '            _osbNewSKU.AppendLine("******************************************************************************")
    '            _osbNewSKU.AppendLine("")

    '            Dim strSplitDate As String = ""

    '            While myReader.Peek <> -1
    '                iTotal_Detail += 1
    '                iCurrent_Line_Number += 1
    '                Current_Text_Line = myReader.ReadLine
    '                strSplit = ReadAndSplit(Current_Text_Line, strDelimited)

    '                If strSplit(0).ToString = "ZZZ" Then
    '                    Continue While
    '                End If

    '                If strSalesOrder_No <> strSplit(0).ToString Then
    '                    '*********** BEGIN HEADER ********************
    '                    Dim obl_Import_HDO As New bl_Import_SO
    '                    If obl_Import_HDO.CheckSalesOrder_No(strSplit(0)) Then
    '                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        iCount_Error_Header += 1

    '                        'Cancel
    '                        If strSplit(15).Trim <> "1" Then
    '                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                            Dim oSo As New tb_SalesOrder
    '                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                            oSo.SalesOrder_Index = strArray(0)
    '                            oSo.SalesOrder_No = strSplit(0).ToString
    '                            oSaleorder.Cancel_SO(oSo)
    '                        End If

    '                        Continue While
    '                    Else 'ถ้าไม่ใช่ให้เก็บค่า DO_No ไว้ใน strSaleOrderNo
    '                        strSalesOrder_No = strSplit(0).Trim
    '                        'Cancel
    '                        If strSplit(15).Trim <> "1" Then
    '                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                            Dim oSo As New tb_SalesOrder
    '                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                            oSo.SalesOrder_Index = strArray(0)
    '                            oSo.SalesOrder_No = strSplit(0).ToString
    '                            oSaleorder.Cancel_SO(oSo)
    '                            Continue While
    '                        End If
    '                    End If
    '                    obl_Import_HDO.InvoiceNo = strSplit(0).Trim
    '                    obl_Import_HDO.Str2 = strSplit(3).Trim
    '                    obl_Import_HDO.DoNo = strSplit(0).Trim
    '                    strSplitDate = strSplit(4).Trim 'For Split Date Sure DD MM YYYY
    '                    obl_Import_HDO.DoDate = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))

    '                    strSplitDate = strSplit(13).Trim 'For Split Date Sure DD MM YYYY
    '                    obl_Import_HDO.Expected_Delivery_Date = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))
    '                    strCustomerShipping_Index = obl_Import_HDO.GetCustomerShipping_Index(strSplit(5).Trim, strSplit(6).Trim, "", _
    '                                                                   strSplit(7).Trim, strSplit(8).Trim)
    '                    obl_Import_HDO.Str8 = strSplit(9).Trim
    '                    obl_Import_HDO.CustomerShippingNo = strSplit(5).Trim
    '                    obl_Import_HDO.CustomerShippingIndex = strCustomerShipping_Index
    '                    obl_Import_HDO.PostCode = strSplit(8).Trim
    '                    obl_Import_HDO.Remark = strSplit(14).Trim
    '                    obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
    '                    If strSplit(2).Trim = "1" Then
    '                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_NORMAL
    '                    Else
    '                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_RETURN
    '                    End If
    '                    obl_Import_HDO.SalesOrderInsert()
    '                    iCount_Comp_Header += 1
    '                    '*********** END HEADER ********************
    '                End If

    '                '*********** BEGIN ITEM ********************

    '                obl_Import_SO_Item.RefNo1 = strSalesOrder_No
    '                strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSalesOrder_No)
    '                obl_Import_SO_Item.SaleOrderIndex = strArray(0)
    '                obl_Import_SO_Item.Currency = strArray(1)

    '                'ถ้าไม่มี SalesOrder_Index ให้ข้ามไปอ่านบรรทัดถัดไป
    '                If obl_Import_SO_Item.SaleOrderIndex = "" Then
    '                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    _osbErr_Data_Incomplete.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    iCount_Error_Detail += 1

    '                    'อ่านบรรทัดใหม่
    '                    Continue While

    '                Else

    '                    'Check Duplicate data in Sales Order Item
    '                    If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, strSplit(2).Trim, strSplit(1).Trim) Then
    '                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)

    '                        iCount_Error_Detail += 1

    '                        'อ่านบรรทัดใหม่
    '                        Continue While
    '                    End If

    '                    Dim objSy_AutoNumber As New Sy_AutoNumber
    '                    obl_Import_SO_Item.SalesOrderItemIndex = objSy_AutoNumber.getSys_Value("SalesOrderItem_Index")
    '                    objSy_AutoNumber = Nothing
    '                    obl_Import_SO_Item.ItemSeq = CInt(strSplit(1).Trim)
    '                    obl_Import_SO_Item.Qty = CDbl(strSplit(11).Trim)
    '                    obl_Import_SO_Item.TotalQty = obl_Import_SO_Item.Qty
    '                    obl_Import_SO_Item.Volume = 0 '
    '                    obl_Import_SO_Item.Str2 = "" ' strSplit(6).Trim
    '                    obl_Import_SO_Item.Remark = strSplit(14).Trim

    '                    '*** BEGIN SKU ***
    '                    obl_Import_SO_Item.SkuId = strSplit(10).Trim
    '                    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.GetSKU_Index(obl_Import_SO_Item.SkuId)
    '                    'Insert New Sku and Package
    '                    If obl_Import_SO_Item.SkuIndex = "" Then
    '                        obl_Import_SO_Item.Package_ID = strSplit(12).Trim
    '                        obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.InsertSKU()
    '                        obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, "")  'strArray(1)
    '                        _osbNewSKU.AppendLine("SKU : " & obl_Import_SO_Item.SkuId)
    '                    End If

    '                    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, strSplit(12).Trim)  'strArray(1)

    '                    If obl_Import_SO_Item.PackageIndex = "" Then
    '                        ' Auto Insert Package were Package and Update Package For Sku This
    '                        obl_Import_SO_Item.PackageIndex = SavePackage(strSplit(12).Trim)
    '                        Me.SaveSKURatio(obl_Import_SO_Item.SkuIndex, obl_Import_SO_Item.PackageIndex, 1) 'Default Ratio=1 ?
    '                    End If
    '                    'Get Package_Index with Package_Id



    '                    '*** END SKU ***

    '                    'Set property and Insert
    '                    obl_Import_SO_Item.SalesOrder_ItemInsert()

    '                    iCount_Comp_Detail += 1
    '                End If

    '                '*********** END ITEM ********************
    '            End While

    '            myReader.Close()

    '            strDestination = MoveFile(pstrFilePath)

    '            'Write Log
    '            With obl_Log
    '                'Move file
    '                .Write_To_Path = strDestination & "\" & pstrFilePath.ToUpper.Replace(Me._DEFAULT_IMPORT_SO_PATH.ToUpper, "") & ".log"
    '                .Process_Name = "Import"
    '                .Module_Name = "DO"
    '                .Start_Process = Now.ToString("dd/MM/yyyy HH:mm:ss")

    '                .Target = pstrFilePath
    '                .Destination = strDestination

    '                .Total_Header = iTotal_Header
    '                .Total_Detail = iTotal_Detail

    '                .Complete_Header = iCount_Comp_Header
    '                .Complete_Detail = iCount_Comp_Detail

    '                .Incomplete_Header = iCount_Error_Header
    '                .Incomplete_Detail = iCount_Error_Detail

    '                .Error_List = New StringBuilder

    '                .Error_List.AppendLine(_osbErr_Dup.ToString)
    '                .Error_List.AppendLine(_osbErr_Data_Incomplete.ToString)
    '                .Error_List.AppendLine(_osbNewSKU.ToString)

    '                .Write_Log()
    '            End With

    '        Catch ex As Exception
    '            obl_Import_SO_Item.UpdateSalesOrder_Cancel_From_Error_Interface(strSalesOrder_No)
    '            Throw ex
    '        End Try

    '    End Sub

    '    Sub SaveSKURatio(ByVal pSku_Index As String, ByVal pPackage_Index As String, ByVal ratio As Double)
    '        Try

    '            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
    '            objDBSKURatio.SaveData("", pSku_Index, pPackage_Index, CDbl(ratio))

    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End Sub

    '    Function SavePackage(ByVal ppackage_Id As String) As String

    '        Try
    '            Dim Package_Index As String = ""
    '            Dim package_Id As String = ppackage_Id
    '            Dim description As String = ppackage_Id
    '            Dim dimension_Hi As Double = 0.0
    '            Dim dimension_Wd As Double = 0.0
    '            Dim dimension_Len As Double = 0.0
    '            Dim Weight As Double = 0.0

    '            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
    '            Package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, 0, Weight, 0) ', txtUnit_id.Text

    '            Return Package_Index

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Function MoveFile(ByVal pstrSourceFile As String) As String
    '        Try

    '            Dim ImportFolder As String = Me._DEFAULT_IMPORT_SO_PATH


    '            Dim strResultFileName As String
    '            Dim filemove As String

    '            If Directory.Exists(ImportFolder & SubFolder) = False Then
    '                Directory.CreateDirectory(ImportFolder & SubFolder)
    '            End If

    '            If pstrSourceFile = "" Then
    '                Return ""
    '                'Continue For
    '            End If


    '            'For i As Integer = 0 To pstrSourceFile.Length - 1
    '            strResultFileName = Path.GetFileName(pstrSourceFile)

    '            filemove = ImportFolder & SubFolder & "\" & strResultFileName
    '            File.Move(pstrSourceFile, filemove)
    '            'Next

    '            Return ImportFolder & SubFolder

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Sub ShowError(ByVal pstrErrorList As ArrayList, ByVal pintCountSucc As Integer)

    '        Try

    '            Dim strResult As String
    '            Dim strError As String = ""
    '            For Each strResult In pstrErrorList
    '                strError &= strResult & vbNewLine
    '            Next
    '            If strError = "" Then
    '                strError = "Not found."
    '            End If

    '            MsgBox("ระบบทำการ Import ข้อมูลสำเร็จ " & vbNewLine _
    '            & "Total : " & pintCountSucc & " record." & vbNewLine & vbNewLine _
    '            & "----------------------------------------------Error list------------------------------------------" & vbNewLine _
    '            & strError & vbNewLine _
    '            & "Total : " & pstrErrorList.Count & " record.")

    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Sub

    '    Public Function ConvertToDate(ByVal pstrValue As String) As Date

    '        Try
    '            'strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4)
    '            pstrValue = pstrValue & "0"
    '            Dim strDate As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 1, 1), Mid(pstrValue, 1, 2))
    '            Dim strMonth As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 2, 2), Mid(pstrValue, 3, 2))
    '            Dim strYear As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 4, 4), Mid(pstrValue, 5, 4))

    '            Return CDate(strYear & "/" & strMonth & "/" & strDate)

    '        Catch ex As Exception

    '            Throw ex

    '        End Try

    '    End Function
    '#End Region

    'Old Code
    'Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
    '    Try
    '        'checking 
    '        If Me.txtCustomer_Id.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Customer")
    '            Exit Sub
    '        End If

    '        If Me.txtWorkSheet.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Work Sheet")
    '            Exit Sub
    '        End If
    '        Dim oImport_SO As New bl_Import_SO
    '        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
    '            Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

    '            'load data from excel

    '            Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
    '        Else
    '            Exit Sub
    '        End If

    '        With oImport_SO
    '            .DataSource = Me.grdPreviewData.DataSource
    '            '.Customer_Index = Me.cboCustomer.SelectedValue
    '            '.DocumentType_Index = Me.cboDocumentType.SelectedValue
    '            '.ItemStatus_Index = Me.cboItemStatus.SelectedValue


    '            'If .CheckingData Then

    '            '    _boolResult = True
    '            'Else
    '            '    _boolResult = False
    '            'End If
    '        End With
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try

    'End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
        Try
            Dim oImport_SO As New bl_Import_SO
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                Me.SafeFileName = OpenFileDialog1.SafeFileName.ToString
                Dim objWS As DataTable = New DataTable

                objWS = oImport_SO.LoadWorkSheet(Me.txtFilePath.Text)

                With cboWorkSheet
                    .DataSource = objWS
                    .DisplayMember = "TABLE_NAME"
                    .ValueMember = "TABLE_NAME"
                End With
                cboWorkSheet.SelectedIndex = 0

                If objWS.Rows.Count = 1 Then
                    LoadData()
                End If

                '====================
            Else
                Exit Sub
            End If

            Me.btnImport.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub frmImport_PO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub cboWorkSheet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWorkSheet.SelectedIndexChanged
        grdPreviewData.DataSource = Nothing
    End Sub

    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged
        Try
            Me._DocumentType_Index = cboDocumentType.SelectedValue.ToString
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub cbProductGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me._EPSON_Location_Index = cbProductGroup.SelectedValue.ToString
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub LoadData()
        'checking
        If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
            W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
            Exit Sub
        End If

        Dim oImport_SO As New bl_Import_SO

        '===============
        Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

        If Me.grdPreviewData.RowCount = 0 Then
            W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
            Exit Sub
        End If

        '===============

        oImport_SO.DataSource = grdPreviewData.DataSource

    End Sub

    Private Sub BntLoaddata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntLoaddata.Click
        LoadData()
        Me.btnImport.Enabled = False
    End Sub

    Private Sub txtFilePath_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePath.DoubleClick
        Try
            Dim AppPath As String
            AppPath = Application.StartupPath
            Dim ps As New ProcessStartInfo()
            With ps
                'Dim strApppath As String = ""
                'strApppath = AppPath.Replace("\bin", "")
                .FileName = AppPath + "\Import\Format\SO_IMPORT_TIMCO.xls"
                'D:\Project\WMS_Site\WMS_Site_TIMCO\WMS_Site_TIMCO\Import\Format\SO_IMPORT_TIMCO.xls
                .UseShellExecute = True
            End With

            Dim p As New Process
            p.StartInfo = ps
            p.Start()


        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try
            'checking 
            If Me.grdPreviewData.DataSource Is Nothing Then
                Exit Sub
            End If


            Me.btnImport.Enabled = False
            If Me.Check_Data() = True Then
                Me.btnImport.Enabled = True
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Function Check_Data() As Boolean
        Try
            Dim xQuery As String = ""
            Dim xDt As New DataTable
            Dim xObjDB As New DBType_SQLServer

            Dim i As Integer
            Dim oImport_PO As New bl_clsImport_TCR
            Check_Data = True

            _dtHeader = New DataTable
            _dtHeader.Columns.Add("PO_NO", GetType(String))


            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            Dim dtDetail As New DataTable
            dtDetail = Me.grdPreviewData.DataSource
            Dim groupHeader As DataTable = dtDetail.DefaultView.ToTable(True, "เลขที่ใบสั่งซื้อ", "วันที่ใบสั่งซื้อ", "เจ้าของสินค้า", "วันที่คาดว่าจะรับสินค้า", "เอกสารอ้างอิง1", "เอกสารอ้างอิง2", "หมายเหตุ/ส่วนหัว")

            Dim checkDupePO As String = ""
            Dim checkDupeString As String = ""
            For Each dtrow As DataRow In groupHeader.Rows

                Dim dataDetail As DataRow() = groupHeader.Select("เลขที่ใบสั่งซื้อ = '" & dtrow.Item("เลขที่ใบสั่งซื้อ").ToString & "'")
                If dataDetail.Length > 1 And checkDupeString <> dtrow.Item("เลขที่ใบสั่งซื้อ").ToString Then
                    checkDupeString = dtrow.Item("เลขที่ใบสั่งซื้อ").ToString
                    checkDupePO = checkDupePO & dtrow.Item("เลขที่ใบสั่งซื้อ").ToString & ", "
                End If
                ' checkDupePO
            Next

            If checkDupePO <> "" Then
                btnImport.Enabled = False
                W_MSG_Information("ไม่สามารถ Import ได้ เนื่องจาก ใบสั่งซื้อเลขที่ : " & checkDupePO.Substring(0, checkDupePO.Length - 1) & " Header ไม่เหมือนกันทั้งรายการใบสั่งซื้อ ")
                Exit Function
            End If


            For i = 0 To grdPreviewData.Rows.Count - 1
                With grdPreviewData
                    'Set Red
                    .Rows(i).Cells("Check_Result").Style.BackColor = Color.Pink
                    ''-------------------------------------------------------------------------------
                    'Document
                    If Not IsDBNull(grdPreviewData.Rows(i).Cells(eColumnName.PO_No).Value) AndAlso Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(eColumnName.PO_No).Value) Then
                        Dim dtSO As DataTable = oImport_PO.GetPO_No(.Rows(i).Cells(eColumnName.PO_No).Value.ToString)
                        If dtSO.Rows.Count > 0 Then
                            .Rows(i).Cells("Check_Result").Value = "เลขที่ PO ซ้ำในระบบ"
                            Check_Data = False
                            Continue For
                        Else
                            'Add Primary key
                            Dim drNewRow As DataRow
                            drNewRow = _dtHeader.NewRow
                            Dim xPO_NO As String = .Rows(i).Cells(eColumnName.PO_No).Value.ToString.Trim
                            drNewRow("PO_NO") = xPO_NO
                            Dim drCheck() As DataRow = _dtHeader.Select(String.Format("PO_NO='{0}'", xPO_NO))
                            If drCheck.Length = 0 Then
                                _dtHeader.Rows.Add(drNewRow)
                            End If
                            .Rows(i).Cells("Check_Result").Value = "OK"
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "ไม่ระบุ เลขที่  PO"
                        Check_Data = False
                        Continue For
                    End If

                    If IsDate(.Rows(i).Cells(EcolumnName.PO_Date).Value) Then
                        Dim strDate As String = CDate(.Rows(i).Cells(EcolumnName.PO_Date).Value).Year
                        If CInt(strDate) > 2500 Then
                            .Rows(i).Cells("Check_Result").Value = "วันที่ PO ต้องเป็น คศ."
                            Check_Data = False
                            Continue For
                        End If
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    Else
                        .Rows(i).Cells("Check_Result").Value = "ไม่ระบุ วันที่ PO"
                        Check_Data = False
                        Continue For
                    End If


                    'If Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(EcolumnName.Supplier_Code).Value) Then
                    '    Dim supplier_index_index As String = oImport_PO.GetSupplier_Index(grdPreviewData.Rows(i).Cells(EcolumnName.Supplier_Code).Value)
                    '    If supplier_index_index = "" Then
                    '        .Rows(i).Cells("Check_Result").Value = "ผิดพลาด ไม่พบรหัสผู้ขายสินค้า "
                    '        Check_Data = False
                    '        Continue For
                    '    Else
                    '        .Rows(i).Cells("Check_Result").Value = "OK"
                    '    End If

                    '    .Rows(i).Cells("Check_Result").Value = "OK"
                    'Else
                    '    .Rows(i).Cells("Check_Result").Value = "รหัสผู้ขายสินค้า ผิดพลาด"
                    '    Check_Data = False
                    '    Continue For
                    'End If

                    'If Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(EcolumnName.Supplier_Name).Value) Then

                    '    .Rows(i).Cells("Check_Result").Value = "OK"
                    'Else
                    '    .Rows(i).Cells("Check_Result").Value = "ชื่อผู้ขายสินค้า ผิดพลาด"
                    '    Check_Data = False
                    '    Continue For
                    'End If

                    If Not IsDBNull(grdPreviewData.Rows(i).Cells(eColumnName.CustomerID).Value) And Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(eColumnName.CustomerID).Value) Then

                        Dim customer_index As String = oImport_PO.GetCustomer_Index(grdPreviewData.Rows(i).Cells(eColumnName.CustomerID).Value)
                        If customer_index = "" Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ รหัสเจ้าของสินค้า ในระบบ"
                            Check_Data = False
                            Continue For
                        Else

                            .Rows(i).Cells("Check_Result").Value = "OK"
                        End If


                    Else
                        .Rows(i).Cells("Check_Result").Value = "ไม่ระบุ รหัสเจ้าของสินค้า"
                        Check_Data = False
                        Continue For
                    End If

                    'If Not IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.SKU_Name).Value) AndAlso Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(EcolumnName.SKU_Name).Value) Then

                    '    .Rows(i).Cells("Check_Result").Value = "OK"
                    'Else
                    '    .Rows(i).Cells("Check_Result").Value = "ชื่อของสินค้า ผิดพลาด"
                    '    Check_Data = False
                    '    Continue For
                    'End If
                    If Not IsDBNull(grdPreviewData.Rows(i).Cells(eColumnName.SkuID).Value) AndAlso Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(eColumnName.SkuID).Value) Then
                        Dim Sku_index As String = oImport_PO.GetSKU_Index(grdPreviewData.Rows(i).Cells(eColumnName.SkuID).Value)
                        If Sku_index = "" Then
                            .Rows(i).Cells("Check_Result").Value = "ไม่พบ รหัสสินค้า ในระบบ "
                            Check_Data = False
                            Continue For
                        Else

                            .Rows(i).Cells("Check_Result").Value = "OK"
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "ไม่ระบุ รหัสสินค้า"
                        Check_Data = False
                        Continue For
                    End If

                    If Not IsDBNull(grdPreviewData.Rows(i).Cells(eColumnName.TotalQty).Value) AndAlso Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(eColumnName.TotalQty).Value) Then
                        If IsNumeric(grdPreviewData.Rows(i).Cells(eColumnName.TotalQty).Value) Then

                            .Rows(i).Cells("Check_Result").Value = "OK"
                        Else
                            .Rows(i).Cells("Check_Result").Value = "จำนวนสินค้า ต้องเป็นตัวเลขเท่านั้น"
                            Check_Data = False
                            Continue For
                        End If
                    Else
                        .Rows(i).Cells("Check_Result").Value = "ไม่ระบุ จำนวนสินค้า"
                        Check_Data = False
                        Continue For
                    End If


                    If IsDate(.Rows(i).Cells(eColumnName.PO_Date_Expected).Value) Then
                        Dim strDate As String = CDate(.Rows(i).Cells(eColumnName.PO_Date_Expected).Value).Year
                        If CInt(strDate) > 2500 Then
                            .Rows(i).Cells("Check_Result").Value = "วันที่คาดว่าจะรับสินค้า ต้องเป็น คศ."
                            Check_Data = False
                            Continue For
                        End If
                        .Rows(i).Cells("Check_Result").Value = "OK"
                    Else
                        .Rows(i).Cells("Check_Result").Value = "ไม่ระบุ วันที่คาดว่าจะรับสินค้า"
                        Check_Data = False
                        Continue For
                    End If



                    'If Not IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.ItemDesc).Value) AndAlso Not String.IsNullOrEmpty(grdPreviewData.Rows(i).Cells(EcolumnName.ItemDesc).Value) Then

                    '    .Rows(i).Cells("Check_Result").Value = "OK"
                    'Else
                    '    .Rows(i).Cells("Check_Result").Value = "ชื่อของสินค้า ผิดพลาด"
                    '    Check_Data = False
                    '    Continue For
                    'End If

                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.ItemDesc).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.ItemDesc).Value = ""
                    'End If

                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Eye).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Eye).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Add).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Add).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Tilted).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Tilted).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Color).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Color).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Degree).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Degree).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.BC).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.BC).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.VMI).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.VMI).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Generation).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Generation).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Brand).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Brand).Value = ""
                    'End If
                    'If IsDBNull(grdPreviewData.Rows(i).Cells(EcolumnName.Group).Value) Then
                    '    grdPreviewData.Rows(i).Cells(EcolumnName.Group).Value = ""
                    'End If


                    'Dim sku_index As String = oImport_PO.GetSKU_Index_TCR(grdPreviewData.Rows(i).Cells(EcolumnName.ItemDesc).Value, _
                    'grdPreviewData.Rows(i).Cells(EcolumnName.Eye).Value, grdPreviewData.Rows(i).Cells(EcolumnName.Add).Value, _
                    'grdPreviewData.Rows(i).Cells(EcolumnName.Tilted).Value, grdPreviewData.Rows(i).Cells(EcolumnName.Color).Value, _
                    'grdPreviewData.Rows(i).Cells(EcolumnName.Degree).Value, grdPreviewData.Rows(i).Cells(EcolumnName.BC).Value, _
                    'grdPreviewData.Rows(i).Cells(EcolumnName.VMI).Value, grdPreviewData.Rows(i).Cells(EcolumnName.Generation).Value, _
                    'grdPreviewData.Rows(i).Cells(EcolumnName.Brand).Value, grdPreviewData.Rows(i).Cells(EcolumnName.Group).Value)

                    'If Not String.IsNullOrEmpty(sku_index) Then

                    '    .Rows(i).Cells("Check_Result").Value = "OK"
                    'Else
                    '    .Rows(i).Cells("Check_Result").Value = "ผิดพลาด ไม่พบสินค้านี้"
                    '    Check_Data = False
                    '    Continue For
                    'End If

                    '-------------------------------------------------------------------------------
                    .Rows(i).Cells("Check_Result").Style.BackColor = Color.YellowGreen
                    '-------------------------------------------------------------------------------
                End With
            Next



        Catch ex As Exception
            Throw ex
        End Try

    End Function





End Class