Imports System.IO
Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Data.OleDb
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class bl_Import_SO_V2 : Inherits DBType_SQLServer

    Private _DEFAULT_IMPORT_SO_PATH As String
    Private _DEFAULT_DocumentType As String
    Private _DEFAULT_SALE_NORMAL As String
    Private _DEFAULT_SALE_RETURN As String
    Private _DEFAULT_CURRENCY_INDEX As String
    Dim strDestination As String = ""
    Dim SubFolder As String = "DO-" & Now.ToString("yyyyMMdd_HHmmss")
    Dim _osbNewSKU As New StringBuilder
    Dim _osbErr_Dup As New StringBuilder
    Dim _osbErr_Data_Incomplete As New StringBuilder
    Private _DataSource As New DataTable
    Private _dataTable As DataTable = New DataTable


    Private _FileMove As String = ""

    Private _Customer_Index As String = ""
    Private _DocumentType_Index As String = ""
    Private _EPSON_Location_Index As String = ""

    Dim dtHeader As New DataTable
    Private _ImportComplete As Boolean = True
    Private _Use_car As Integer
    Private _So_Date As Date

    Private _config_SO_No As Integer = 0
    Private _config_SO_Date As Integer = 0
    Private _config_Supplier_Code As Integer = 0
    Private _config_Supplier_Name As Integer = 0
    Private _config_Supplier_Address As Integer = 0
    Private _config_SO_Exp As Integer = 0
    Private _config_Supplier_Tel As Integer = 0
    Private _config_Supplier_Fax As Integer = 0
    Private _config_Sku_Id As Integer = 0
    Private _config_Sku_Name As Integer = 0
    Private _config_Sku_Package As Integer = 0
    Private _config_Sku_Qty As Integer = 0
    Private _config_Sku_Price As Integer = 0
    Private _config_Remark As Integer = 0

    Public Property Config_SO_No() As Integer
        Get
            Return _config_SO_No
        End Get
        Set(ByVal value As Integer)
            _config_SO_No = value
        End Set
    End Property
    Public Property Config_SO_Date() As Integer
        Get
            Return _config_SO_Date
        End Get
        Set(ByVal value As Integer)
            _config_SO_Date = value
        End Set
    End Property
    Public Property Config_Supplier_Code() As Integer
        Get
            Return _config_Supplier_Code
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Code = value
        End Set
    End Property
    Public Property Config_Supplier_Name() As Integer
        Get
            Return _config_Supplier_Name
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Name = value
        End Set
    End Property
    Public Property Config_Supplier_Address() As Integer
        Get
            Return _config_Supplier_Address
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Address = value
        End Set
    End Property
    Public Property Config_SO_Exp() As Integer
        Get
            Return _config_SO_Exp
        End Get
        Set(ByVal value As Integer)
            _config_SO_Exp = value
        End Set
    End Property
    Public Property Config_Supplier_Tel() As Integer
        Get
            Return _config_Supplier_Tel
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Tel = value
        End Set
    End Property
    Public Property Config_Supplier_Fax() As Integer
        Get
            Return _config_Supplier_Fax
        End Get
        Set(ByVal value As Integer)
            _config_Supplier_Fax = value
        End Set
    End Property
    Public Property Config_Sku_Id() As Integer
        Get
            Return _config_Sku_Id
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Id = value
        End Set
    End Property
    Public Property Config_Sku_Name() As Integer
        Get
            Return _config_Sku_Name
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Name = value
        End Set
    End Property
    Public Property Config_Sku_Package() As Integer
        Get
            Return _config_Sku_Package
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Package = value
        End Set
    End Property
    Public Property Config_Sku_Qty() As Integer
        Get
            Return _config_Sku_Qty
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Qty = value
        End Set
    End Property
    Public Property Config_Sku_Price() As Integer
        Get
            Return _config_Sku_Price
        End Get
        Set(ByVal value As Integer)
            _config_Sku_Price = value
        End Set
    End Property
    Public Property Config_Remark() As Integer
        Get
            Return _config_Remark
        End Get
        Set(ByVal value As Integer)
            _config_Remark = value
        End Set
    End Property

    Public Sub GetVIEW_Import_SO()

        Dim strSQL As String = ""

        Try
            strSQL = " select *  "
            strSQL &= " from VIEW_Import_SO "

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetConfig_Import_SO()

        Dim strSQL As String = ""

        Try
            strSQL = " select *  "
            strSQL &= " from config_Import_SO "

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub GetConfig_Import_SO(ByVal Format_Import_Index As String)

        Dim strSQL As String = ""

        Try
            strSQL = " select *  "
            strSQL &= " from config_Import_SO "

            If Format_Import_Index <> "" Then
                strSQL &= " WHERE Format_Import_Index ='" & Format_Import_Index & "' "
            End If
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub Getconfig_Format_Import()

        Dim strSQL As String = ""

        Try
            strSQL = " select *  "
            strSQL &= " from config_Format_Import "

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public Property So_Date() As Date
        Get
            Return _So_Date
        End Get
        Set(ByVal Value As Date)
            _So_Date = Value
        End Set
    End Property

    Public Property Use_car() As Integer
        Get
            Return _Use_car
        End Get
        Set(ByVal Value As Integer)
            _Use_car = Value
        End Set
    End Property

    Property ImportComplete() As Boolean
        Get
            Return _ImportComplete
        End Get
        Set(ByVal value As Boolean)
            _ImportComplete = value
        End Set
    End Property

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

    Property EPSON_Location_Index() As String
        Get
            Return _EPSON_Location_Index
        End Get
        Set(ByVal value As String)
            _EPSON_Location_Index = value
        End Set
    End Property
    Public Property FileMove() As String
        Get
            Return _FileMove
        End Get
        Set(ByVal value As String)
            _FileMove = value
        End Set
    End Property

    Public Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value
        End Set
    End Property
#Region "   ReadTxT Import SO   "
    Public Sub SearchSkuInv(ByVal tmpCustomer_Index As String, ByVal tmpSku_Id As String)
        Dim strSQL As String
        Dim strWhere As String
        Try

            strSQL = "SELECT TOP 1 * FROM VIEW_TIMCO_SOSearchInv "
            strWhere = ""

            strWhere += " WHERE 1=1 and Customer_Index = '" & tmpCustomer_Index & "' and Sku_Id = '" & tmpSku_Id & "'"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Function ReadAndSplit(ByVal myReader As String, ByVal strDelimited As String)
        Try

            Dim CurrLine As String = myReader
            Dim strSplit() As String
            Dim strSaleOrderNo As String = ""

            strSplit = CurrLine.Split(strDelimited)

            Return strSplit
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function InsertSOHeader(ByVal strCurr_LineSO As String, ByVal strCustomerIndexEDI As String) As String

    '    Dim strDelimited As String = vbTab
    '    Dim strSO_Index As String = ""
    '    Dim datenow As New DateTime
    '    Dim obl_Import_DO As New bl_Import_DO_Item
    '    Try
    '        Dim oblSO As New bl_Import_SO
    '        'GetDEFAULT_CUSTOMER_INDEX()
    '        'oblSO.Customer_Index = strCustomerIndexEDI
    '        'Get_DEFAULT_DOCUMENT_TYPE_SALE_ORDER()
    '        'oblSO.DocumentType_Index = Me.DocumentIndex
    '        Dim Customer_Index As String = objGetData.GetConfig_EDI("Customer_Index")
    '        Dim DocumentType_940 As String = objGetData.GetConfig_EDI("DocumentType_Index_940")
    '        With oblSO
    '            .SalesOrder_Index = ""
    '            .Customer_Index = Customer_Index
    '            '.SalesOrder_No = Mid(strCurr_LinePO, 1, 10).Trim.ToString
    '            .Ref_No2 = Mid(strCurr_LineSO, 1, 10).Trim.ToString
    '            .SalesOrder_Date = CDate(DateTime.Now.ToString("yyyy/MM/dd"))         'ใช้ เป็น Date ปัจจุบัน    ConvertToDate(Mid(strCurr_LineSO, 418, 12).Trim.ToString)
    '            .PODDoc_Copy1 = Mid(strCurr_LineSO, 854, 4).Trim.ToString 'Sales Organization
    '            .PODDoc_Copy2 = Mid(strCurr_LineSO, 858, 2).Trim.ToString 'Distribution Channel
    '            .PODDoc_Copy3 = Mid(strCurr_LineSO, 860, 3).Trim.ToString 'Order Type
    '            .Customer_Shipping_Index = obl_Import_DO.GetCareOf_Index(Mid(strCurr_LineSO, 15, 10).Trim.ToString).Trim.ToString
    '            .DocumentType_Index = DocumentType_940 ' outgoing
    '            .Str9 = "EDI"
    '            If .PODDoc_Copy1.Substring(0, 3) = ConfigurationManager.AppSettings("Epson_940_Main").ToString Or .PODDoc_Copy1.Substring(0, 3) = ConfigurationManager.AppSettings("Epson_850_Main").ToString Then
    '                .Epson_ProductGroup_Index = ConfigurationManager.AppSettings("Product_Group_Index_Main").ToString
    '            ElseIf .PODDoc_Copy1.Substring(0, 3) = ConfigurationManager.AppSettings("Epson_940_SparePart").ToString And .PODDoc_Copy1.Substring(0, 3) = ConfigurationManager.AppSettings("Epson_850_SparePart").ToString Then
    '                .Epson_ProductGroup_Index = ConfigurationManager.AppSettings("Product_Group_Index_SparePart").ToString
    '            End If

    '            strSO_Index = .saveHeader()

    '        End With

    '        Return strSO_Index

    '    Catch ex As Exception

    '        Throw ex

    '    End Try

    'End Function

    Public Sub ImportSOExcel()
        'Dim pstrFilePath As String
        Dim strSalesOrder_No As String = ""
        Dim strDelimited As String = vbTab
        Dim obl_Import_SO_Item As New bl_Import_SO_Item
        '===
        'Dim objImport_SO As New frmImport_SO
        Dim strCustomerInvNo As String = ""
        Me._ImportComplete = False
        '===

        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            'AppSettings("ConnectionString")
            Me._DEFAULT_IMPORT_SO_PATH = appSet.GetValue("DEFAULT_IMPORT_SO_PATH", GetType(String)) 'AppSettings("DEFAULT_IMPORT_SO_PATH")
            Me._DEFAULT_SALE_NORMAL = appSet.GetValue("DEFAULT_SALE_NORMAL", GetType(String)) 'AppSettings("DEFAULT_SALE_NORMAL")
            Me._DEFAULT_SALE_RETURN = appSet.GetValue("DEFAULT_SALE_RETURN", GetType(String)) 'AppSettings("DEFAULT_SALE_RETURN")
            Me._DEFAULT_CURRENCY_INDEX = appSet.GetValue("DEFAULT_CURRENCY_INDEX", GetType(String)) 'AppSettings("DEFAULT_CURRENCY_INDEX")

            'Dim myReader As StreamReader
            Dim Current_Text_Line As String = ""
            Dim strCustomerShipping_Index As String = ""
            Dim strInvoice_No As String = ""
            'Dim strSplit() As String
            Dim strTempText As New ArrayList
            Dim strSku_Index As String = ""
            Dim strArray(2) As String

            Dim iTotal_Header As Integer = 0
            Dim iTotal_Detail As Integer = 0

            Dim iCount_Comp_Header As Integer = 0
            Dim iCount_Comp_Detail As Integer = 0

            Dim iCount_Error_Header As Integer = 0
            Dim iCount_Error_Detail As Integer = 0
            Dim iCurrent_Line_Number As Integer = 0

            Dim obl_Log As New bl_Log
            Dim oDT As New DataTable
            oDT = Me.DataSource

            '************ Start Datatable Import
            For i As Integer = 0 To oDT.Rows.Count - 1

                Dim strSplitDate As String = ""
                If strCustomerInvNo <> oDT.Rows(i).Item(1).ToString Then
                    '*********** BEGIN HEADER ********************
                    Dim obl_Import_HDO As New bl_Import_SO_Header

                    If obl_Import_HDO.CheckInvoice_No_TIMCO(oDT.Rows(i).Item("Cust_Inv_No").ToString, Me.Customer_Index) Then
                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                        iCount_Error_Header += 1

                        Continue For

                    Else 'ถ้าไม่ใช่ให้เก็บค่า DO_No ไว้ใน strCustomerInvNo  'strSaleOrderNo
                        strCustomerInvNo = oDT.Rows(i).Item("Cust_Inv_No").ToString.Trim
                    End If
                    obl_Import_HDO.CustomerIndex = Me.Customer_Index
                    obl_Import_HDO.Ref_No2 = oDT.Rows(i).Item("Cust_Inv_No").ToString.Trim
                    obl_Import_HDO.DoNo = ""
                    obl_Import_HDO.DoDate = CDate(Me._So_Date).ToString("yyyy/MM/dd") 'Now
                    obl_Import_HDO.CustomerShippingNo = oDT.Rows(i).Item("Careof_No").ToString.Trim
                    obl_Import_HDO.CustomerShippingIndex = Me.GetCareOf_Index(oDT.Rows(i).Item("Careof_No").ToString.Trim, Me.Customer_Index).Trim.ToString
                    obl_Import_HDO.Remark = oDT.Rows(i).Item("Remark_Header").ToString.Trim
                    obl_Import_HDO.Str1 = oDT.Rows(i).Item("Reference1").ToString.Trim


                    If Me._DEFAULT_CURRENCY_INDEX Is Nothing Then
                        obl_Import_HDO.Currency = ""
                    Else
                        obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
                    End If

                    obl_Import_HDO.DocumentTypeIndex = Me.DocumentType_Index
                    obl_Import_HDO.EPSON_Location_Index = Me.EPSON_Location_Index
                    obl_Import_HDO.Use_car = Me.Use_car

                    obl_Import_HDO.SalesOrderInsert()
                    iCount_Comp_Header += 1

                    '*********** END HEADER ********************

                End If
                '===================================================================================

                '*********** BEGIN ITEM ********************

                'obl_Import_SO_Item.RefNo1 = strSalesOrder_No
                strArray = obl_Import_SO_Item.GetSalesOrder_Index_TIMCO(oDT.Rows(i).Item("Cust_Inv_No").ToString.Trim, Me.Customer_Index)
                obl_Import_SO_Item.SaleOrderIndex = strArray(0)
                obl_Import_SO_Item.Currency = strArray(1)

                'ถ้าไม่มี SalesOrder_Index ให้ข้ามไปอ่านบรรทัดถัดไป
                If obl_Import_SO_Item.SaleOrderIndex = "" Then
                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                    _osbErr_Data_Incomplete.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                    iCount_Error_Detail += 1

                    'อ่านบรรทัดใหม่
                    'Continue While
                    Continue For
                Else

                    ''Check Duplicate data in Sales Order Item
                    ''If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, strSplit(2).Trim, strSplit(1).Trim) Then
                    'If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, oDT.Rows(i).Item("SKU_ID").ToString.Trim, oDT.Rows(i).Item("Item_Seq").ToString.Trim) Then
                    '    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                    '    _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)

                    '    iCount_Error_Detail += 1

                    '    'อ่านบรรทัดใหม่
                    '    'Continue While
                    '    Continue For
                    'End If

                    Dim objSy_AutoNumber As New Sy_AutoNumber
                    obl_Import_SO_Item.SalesOrderItemIndex = objSy_AutoNumber.getSys_Value("SalesOrderItem_Index")
                    objSy_AutoNumber = Nothing
                    obl_Import_SO_Item.ItemSeq = Val(obl_Import_SO_Item.GetLast_ItemSeq(obl_Import_SO_Item.SaleOrderIndex)) + 1 * 10
                    obl_Import_SO_Item.Qty = CDbl(oDT.Rows(i).Item("Qty").ToString.Trim)
                    obl_Import_SO_Item.TotalQty = obl_Import_SO_Item.Qty
                    obl_Import_SO_Item.Volume = 0 '
                    obl_Import_SO_Item.Str2 = "" ' strSplit(6).Trim
                    obl_Import_SO_Item.Remark = oDT.Rows(i).Item("Remark").ToString.Trim
                    obl_Import_SO_Item.Plot = oDT.Rows(i).Item("Lot").ToString
                    '*** BEGIN SKU ***
                    obl_Import_SO_Item.SkuId = oDT.Rows(i).Item("Product_No").ToString.Trim
                    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.GetSKU_Index(obl_Import_SO_Item.SkuId, Me.Customer_Index)
                    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index_Import(obl_Import_SO_Item.SkuIndex, Me.Customer_Index)
                    obl_Import_SO_Item.Itemstatus_index = "0010000000001" 'GOOD

                    ''Insert New Sku and Package
                    'If obl_Import_SO_Item.SkuIndex = "" Then
                    '    obl_Import_SO_Item.Package_ID = oDT.Rows(i).Item("Package_ID").ToString.Trim.Trim
                    '    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.InsertSKU()
                    '    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, "")  'strArray(1)
                    '    _osbNewSKU.AppendLine("SKU : " & obl_Import_SO_Item.SkuId)
                    'End If

                    'obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, oDT.Rows(i).Item("Package_ID").ToString.Trim)  'strArray(1)

                    'If obl_Import_SO_Item.PackageIndex = "" Then
                    '    ' Auto Insert Package were Package and Update Package For Sku This
                    '    obl_Import_SO_Item.PackageIndex = SavePackage(oDT.Rows(i).Item("Package_ID").ToString.Trim)
                    '    Me.SaveSKURatio(obl_Import_SO_Item.SkuIndex, obl_Import_SO_Item.PackageIndex, 1) 'Default Ratio=1 ?
                    'End If
                    ''Get Package_Index with Package_Id

                    '*** END SKU ***

                    'Set property and Insert
                    obl_Import_SO_Item.SalesOrder_ItemInsert()

                    iCount_Comp_Detail += 1
                End If

                '*********** END ITEM *******************

                'strDestination = MoveFile(pstrFilePath)
                Me._ImportComplete = True
                '************* End Datatable
            Next

        Catch ex As Exception
            obl_Import_SO_Item.UpdateSalesOrder_Cancel_From_Error_Interface(obl_Import_SO_Item.SaleOrderIndex) 'strSalesOrder_No
            Me._ImportComplete = False
            Throw ex
        End Try

    End Sub

    'Old Code
    'Public Sub ImportSOExcel()
    '    Dim pstrFilePath As String
    '    Dim strSalesOrder_No As String = ""
    '    Dim strDelimited As String = vbTab
    '    Dim obl_Import_SO_Item As New bl_Import_SO_Item

    '    Try
    '        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
    '        WV_ConnectionString = AppSettings("ConnectionString")
    '        Me._DEFAULT_IMPORT_SO_PATH = AppSettings("DEFAULT_IMPORT_SO_PATH")
    '        Me._DEFAULT_SALE_NORMAL = AppSettings("DEFAULT_SALE_NORMAL")
    '        Me._DEFAULT_SALE_RETURN = AppSettings("DEFAULT_SALE_RETURN")
    '        Me._DEFAULT_CURRENCY_INDEX = AppSettings("DEFAULT_CURRENCY_INDEX")

    '        Dim myReader As StreamReader
    '        Dim Current_Text_Line As String = ""
    '        Dim strCustomerShipping_Index As String = ""
    '        Dim strInvoice_No As String = ""
    '        Dim strSplit() As String
    '        Dim strTempText As New ArrayList
    '        Dim strSku_Index As String = ""
    '        Dim strArray(2) As String

    '        Dim iTotal_Header As Integer = 0
    '        Dim iTotal_Detail As Integer = 0

    '        Dim iCount_Comp_Header As Integer = 0
    '        Dim iCount_Comp_Detail As Integer = 0

    '        Dim iCount_Error_Header As Integer = 0
    '        Dim iCount_Error_Detail As Integer = 0
    '        ' Dim osb As New StringBuilder

    '        Dim iCurrent_Line_Number As Integer = 0

    '        Dim obl_Log As New bl_Log
    '        Dim oDT As New DataTable
    '        oDT = Me.DataSource
    '        '************ Start Datatable Import
    '        For i As Integer = 0 To oDT.Rows.Count - 1


    '            'myReader = New StreamReader(pstrFilePath, System.Text.UnicodeEncoding.Default)


    '            '_osbNewSKU = New StringBuilder
    '            '_osbErr_Dup = New StringBuilder
    '            '_osbErr_Data_Incomplete = New StringBuilder


    '            '_osbErr_Dup.AppendLine("Error : Duplicate data")
    '            '_osbErr_Dup.AppendLine("")

    '            '_osbErr_Data_Incomplete.AppendLine("")
    '            '_osbErr_Data_Incomplete.AppendLine("Error : Data Incomplete")
    '            '_osbErr_Data_Incomplete.AppendLine("")

    '            '_osbNewSKU.AppendLine("******************************************************************************")
    '            '_osbNewSKU.AppendLine("[4] New SKU")
    '            '_osbNewSKU.AppendLine("******************************************************************************")
    '            '_osbNewSKU.AppendLine("")

    '            Dim strSplitDate As String = ""

    '            'While myReader.Peek <> -1
    '            '    iTotal_Detail += 1
    '            '    iCurrent_Line_Number += 1
    '            '    Current_Text_Line = myReader.ReadLine
    '            '    strSplit = ReadAndSplit(Current_Text_Line, strDelimited)

    '            '    If strSplit(0).ToString = "ZZZ" Then
    '            '        Continue While
    '            '    End If

    '            'If strSalesOrder_No <> strSplit(0).ToString Then
    '            If strSalesOrder_No <> oDT.Rows(i).Item("SalesOrder_No").ToString Then
    '                '*********** BEGIN HEADER ********************
    '                Dim obl_Import_HDO As New bl_Import_SO_Header
    '                'If obl_Import_HDO.CheckSalesOrder_No(strSplit(0)) Then
    '                If obl_Import_HDO.CheckSalesOrder_No(oDT.Rows(i).Item("SalesOrder_No").ToString) Then
    '                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    iCount_Error_Header += 1

    '                    'Cancel
    '                    'If strSplit(15).Trim <> "1" Then
    '                    'If oDT.Rows(i).Item("Status").ToString.Trim <> "1" Then
    '                    '    Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                    '    Dim oSo As New tb_SalesOrder
    '                    '    strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                    '    oSo.SalesOrder_Index = strArray(0)
    '                    '    oSo.SalesOrder_No = strSplit(0).ToString
    '                    '    oSaleorder.Cancel_SO(oSo)
    '                    'End If

    '                    'Continue While
    '                    Continue For
    '                Else 'ถ้าไม่ใช่ให้เก็บค่า DO_No ไว้ใน strSaleOrderNo
    '                    'strSalesOrder_No = strSplit(0).Trim
    '                    strSalesOrder_No = oDT.Rows(i).Item("SalesOrder_No").ToString.Trim
    '                    'Cancel
    '                    'If oDT.Rows(i).Item("Status").ToString.Trim <> "1" Then
    '                    '    Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                    '    Dim oSo As New tb_SalesOrder
    '                    '    strArray = obl_Import_SO_Item.GetSalesOrder_Index(oDT.Rows(i).Item("SalesOrder_No").ToString)
    '                    '    oSo.SalesOrder_Index = strArray(0)
    '                    '    oSo.SalesOrder_No = oDT.Rows(i).Item("SalesOrder_No").ToString
    '                    '    oSaleorder.Cancel_SO(oSo)
    '                    '    'Continue While
    '                    '    Continue For
    '                    'End If
    '                End If
    '                obl_Import_HDO.InvoiceNo = oDT.Rows(i).Item("SalesOrder_No").ToString.Trim
    '                obl_Import_HDO.Str2 = oDT.Rows(i).Item("Str2").ToString.Trim
    '                obl_Import_HDO.DoNo = oDT.Rows(i).Item("SalesOrder_No").ToString.Trim
    '                strSplitDate = oDT.Rows(i).Item("SalesOrder_Date").ToString.Trim 'For Split Date Sure DD MM YYYY
    '                obl_Import_HDO.DoDate = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))
    '                strSplitDate = oDT.Rows(i).Item("Expected_Delivery_Date").ToString.Trim 'For Split Date Sure DD MM YYYY
    '                obl_Import_HDO.Expected_Delivery_Date = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))
    '                strCustomerShipping_Index = obl_Import_HDO.GetCustomerShipping_Index(oDT.Rows(i).Item("CustomerShipping_No").ToString.Trim, oDT.Rows(i).Item("Company_Name").ToString.Trim, "", _
    '                                                               oDT.Rows(i).Item("Address").ToString.Trim, oDT.Rows(i).Item("PostCode").ToString.Trim)
    '                obl_Import_HDO.Str8 = oDT.Rows(i).Item("Str8").ToString.Trim
    '                obl_Import_HDO.CustomerShippingNo = oDT.Rows(i).Item("CustomerShipping_No").ToString.Trim
    '                obl_Import_HDO.CustomerShippingIndex = strCustomerShipping_Index
    '                obl_Import_HDO.PostCode = oDT.Rows(i).Item("PostCode").ToString.Trim
    '                obl_Import_HDO.Remark = oDT.Rows(i).Item("Remark").ToString.Trim

    '                If Me._DEFAULT_CURRENCY_INDEX Is Nothing Then
    '                    obl_Import_HDO.Currency = ""
    '                Else
    '                    obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
    '                End If
    '                If Me._DEFAULT_SALE_NORMAL Is Nothing Then
    '                    obl_Import_HDO.DocumentTypeIndex = "0010000000018"
    '                Else
    '                    obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_NORMAL
    '                End If

    '                'Else
    '                '    obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_RETURN
    '                'End If
    '                obl_Import_HDO.SalesOrderInsert()
    '                iCount_Comp_Header += 1
    '                '*********** END HEADER ********************
    '            End If

    '            '*********** BEGIN ITEM ********************

    '            obl_Import_SO_Item.RefNo1 = strSalesOrder_No
    '            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSalesOrder_No)
    '            obl_Import_SO_Item.SaleOrderIndex = strArray(0)
    '            obl_Import_SO_Item.Currency = strArray(1)

    '            'ถ้าไม่มี SalesOrder_Index ให้ข้ามไปอ่านบรรทัดถัดไป
    '            If obl_Import_SO_Item.SaleOrderIndex = "" Then
    '                'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                _osbErr_Data_Incomplete.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                iCount_Error_Detail += 1

    '                'อ่านบรรทัดใหม่
    '                'Continue While
    '                Continue For
    '            Else

    '                'Check Duplicate data in Sales Order Item
    '                'If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, strSplit(2).Trim, strSplit(1).Trim) Then
    '                If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, oDT.Rows(i).Item("SKU_ID").ToString.Trim, oDT.Rows(i).Item("Item_Seq").ToString.Trim) Then
    '                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)

    '                    iCount_Error_Detail += 1

    '                    'อ่านบรรทัดใหม่
    '                    'Continue While
    '                    Continue For
    '                End If

    '                Dim objSy_AutoNumber As New Sy_AutoNumber
    '                obl_Import_SO_Item.SalesOrderItemIndex = objSy_AutoNumber.getSys_Value("SalesOrderItem_Index")
    '                objSy_AutoNumber = Nothing
    '                obl_Import_SO_Item.ItemSeq = CInt(oDT.Rows(i).Item("Item_Seq").ToString.Trim)
    '                obl_Import_SO_Item.Qty = CDbl(oDT.Rows(i).Item("QTY").ToString.Trim)
    '                obl_Import_SO_Item.TotalQty = obl_Import_SO_Item.Qty
    '                obl_Import_SO_Item.Volume = 0 '
    '                obl_Import_SO_Item.Str2 = "" ' strSplit(6).Trim
    '                obl_Import_SO_Item.Remark = oDT.Rows(i).Item("Remark").ToString.Trim

    '                '*** BEGIN SKU ***
    '                obl_Import_SO_Item.SkuId = oDT.Rows(i).Item("Sku_ID").ToString.Trim
    '                obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.GetSKU_Index(obl_Import_SO_Item.SkuId)
    '                'Insert New Sku and Package
    '                If obl_Import_SO_Item.SkuIndex = "" Then
    '                    obl_Import_SO_Item.Package_ID = oDT.Rows(i).Item("Package_ID").ToString.Trim.Trim
    '                    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.InsertSKU()
    '                    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, "")  'strArray(1)
    '                    _osbNewSKU.AppendLine("SKU : " & obl_Import_SO_Item.SkuId)
    '                End If

    '                obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, oDT.Rows(i).Item("Package_ID").ToString.Trim)  'strArray(1)

    '                If obl_Import_SO_Item.PackageIndex = "" Then
    '                    ' Auto Insert Package were Package and Update Package For Sku This
    '                    obl_Import_SO_Item.PackageIndex = SavePackage(oDT.Rows(i).Item("Package_ID").ToString.Trim)
    '                    Me.SaveSKURatio(obl_Import_SO_Item.SkuIndex, obl_Import_SO_Item.PackageIndex, 1) 'Default Ratio=1 ?
    '                End If
    '                'Get Package_Index with Package_Id



    '                '*** END SKU ***

    '                'Set property and Insert
    '                obl_Import_SO_Item.SalesOrder_ItemInsert()

    '                iCount_Comp_Detail += 1
    '            End If

    '            '*********** END ITEM ********************
    '            'end While

    '            'myReader.Close()

    '            'strDestination = MoveFile(pstrFilePath)
    '            '************* End Datatable
    '        Next

    '        'Write Log
    '        'With obl_Log
    '        '    'Move file
    '        '    .Write_To_Path = strDestination & "\" & pstrFilePath.ToUpper.Replace(Me._DEFAULT_IMPORT_SO_PATH.ToUpper, "") & ".log"
    '        '    .Process_Name = "Import"
    '        '    .Module_Name = "DO"
    '        '    .Start_Process = Now.ToString("dd/MM/yyyy HH:mm:ss")

    '        '    .Target = pstrFilePath
    '        '    .Destination = strDestination

    '        '    .Total_Header = iTotal_Header
    '        '    .Total_Detail = iTotal_Detail

    '        '    .Complete_Header = iCount_Comp_Header
    '        '    .Complete_Detail = iCount_Comp_Detail

    '        '    .Incomplete_Header = iCount_Error_Header
    '        '    .Incomplete_Detail = iCount_Error_Detail

    '        '    .Error_List = New StringBuilder

    '        '    .Error_List.AppendLine(_osbErr_Dup.ToString)
    '        '    .Error_List.AppendLine(_osbErr_Data_Incomplete.ToString)
    '        '    .Error_List.AppendLine(_osbNewSKU.ToString)

    '        '    .Write_Log()
    '        'End With

    '    Catch ex As Exception
    '        obl_Import_SO_Item.UpdateSalesOrder_Cancel_From_Error_Interface(strSalesOrder_No)
    '        Throw ex
    '    End Try

    'End Sub
    Public Sub ReadText_DO()
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            'WV_ConnectionString = AppSettings("ConnectionString")
            Me._DEFAULT_IMPORT_SO_PATH = appSet.GetValue("DEFAULT_IMPORT_SO_PATH", GetType(String)) 'AppSettings("DEFAULT_IMPORT_SO_PATH")
            Me._DEFAULT_SALE_NORMAL = appSet.GetValue("DEFAULT_SALE_NORMAL", GetType(String)) ' AppSettings("DEFAULT_SALE_NORMAL")
            Me._DEFAULT_SALE_RETURN = appSet.GetValue("DEFAULT_SALE_RETURN", GetType(String)) 'AppSettings("DEFAULT_SALE_RETURN")
            Me._DEFAULT_CURRENCY_INDEX = appSet.GetValue("DEFAULT_CURRENCY_INDEX", GetType(String)) ' AppSettings("DEFAULT_CURRENCY_INDEX")

            Dim DetailFileList() As String = Directory.GetFiles(Me._DEFAULT_IMPORT_SO_PATH, "*.txt")
            For i As Integer = 0 To DetailFileList.Length - 1
                ReadDetail(DetailFileList(i))
            Next

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub ReadDetail(ByVal pstrFilePath As String)
        Dim strSalesOrder_No As String = ""
        Dim strDelimited As String = vbTab
        Dim obl_Import_SO_Item As New bl_Import_SO_Item

        Try
            Dim myReader As StreamReader
            Dim Current_Text_Line As String = ""
            Dim strCustomerShipping_Index As String = ""
            Dim strInvoice_No As String = ""
            Dim strSplit() As String
            Dim strTempText As New ArrayList
            Dim strSku_Index As String = ""
            Dim strArray(2) As String

            Dim iTotal_Header As Integer = 0
            Dim iTotal_Detail As Integer = 0

            Dim iCount_Comp_Header As Integer = 0
            Dim iCount_Comp_Detail As Integer = 0

            Dim iCount_Error_Header As Integer = 0
            Dim iCount_Error_Detail As Integer = 0
            ' Dim osb As New StringBuilder

            Dim iCurrent_Line_Number As Integer = 0

            Dim obl_Log As New bl_Log

            myReader = New StreamReader(pstrFilePath, System.Text.UnicodeEncoding.Default)


            _osbNewSKU = New StringBuilder
            _osbErr_Dup = New StringBuilder
            _osbErr_Data_Incomplete = New StringBuilder


            _osbErr_Dup.AppendLine("Error : Duplicate data")
            _osbErr_Dup.AppendLine("")

            _osbErr_Data_Incomplete.AppendLine("")
            _osbErr_Data_Incomplete.AppendLine("Error : Data Incomplete")
            _osbErr_Data_Incomplete.AppendLine("")

            _osbNewSKU.AppendLine("******************************************************************************")
            _osbNewSKU.AppendLine("[4] New SKU")
            _osbNewSKU.AppendLine("******************************************************************************")
            _osbNewSKU.AppendLine("")

            Dim strSplitDate As String = ""

            While myReader.Peek <> -1
                iTotal_Detail += 1
                iCurrent_Line_Number += 1
                Current_Text_Line = myReader.ReadLine
                strSplit = ReadAndSplit(Current_Text_Line, strDelimited)

                If strSplit(0).ToString = "ZZZ" Then
                    Continue While
                End If

                If strSalesOrder_No <> strSplit(0).ToString Then
                    '*********** BEGIN HEADER ********************
                    Dim obl_Import_HDO As New bl_Import_SO_Header
                    If obl_Import_HDO.CheckSalesOrder_No(strSplit(0)) Then
                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                        iCount_Error_Header += 1

                        'Cancel
                        If strSplit(15).Trim <> "1" Then
                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
                            Dim oSo As New tb_SalesOrder
                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
                            oSo.SalesOrder_Index = strArray(0)
                            oSo.SalesOrder_No = strSplit(0).ToString
                            oSaleorder.Cancel_SO(oSo)
                        End If

                        Continue While
                    Else 'ถ้าไม่ใช่ให้เก็บค่า DO_No ไว้ใน strSaleOrderNo
                        strSalesOrder_No = strSplit(0).Trim
                        'Cancel
                        If strSplit(15).Trim <> "1" Then
                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
                            Dim oSo As New tb_SalesOrder
                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
                            oSo.SalesOrder_Index = strArray(0)
                            oSo.SalesOrder_No = strSplit(0).ToString
                            oSaleorder.Cancel_SO(oSo)
                            Continue While
                        End If
                    End If
                    obl_Import_HDO.InvoiceNo = strSplit(0).Trim
                    obl_Import_HDO.Str2 = strSplit(3).Trim
                    obl_Import_HDO.DoNo = strSplit(0).Trim
                    strSplitDate = strSplit(4).Trim 'For Split Date Sure DD MM YYYY
                    obl_Import_HDO.DoDate = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))

                    strSplitDate = strSplit(13).Trim 'For Split Date Sure DD MM YYYY
                    obl_Import_HDO.Expected_Delivery_Date = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))
                    strCustomerShipping_Index = obl_Import_HDO.GetCustomerShipping_Index(strSplit(5).Trim, strSplit(6).Trim, "", _
                                                                   strSplit(7).Trim, strSplit(8).Trim)
                    obl_Import_HDO.Str8 = strSplit(9).Trim
                    obl_Import_HDO.CustomerShippingNo = strSplit(5).Trim
                    obl_Import_HDO.CustomerShippingIndex = strCustomerShipping_Index
                    obl_Import_HDO.PostCode = strSplit(8).Trim
                    obl_Import_HDO.Remark = strSplit(14).Trim
                    obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
                    If strSplit(2).Trim = "1" Then
                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_NORMAL
                    Else
                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_RETURN
                    End If
                    obl_Import_HDO.SalesOrderInsert()
                    iCount_Comp_Header += 1
                    '*********** END HEADER ********************
                End If

                '*********** BEGIN ITEM ********************

                obl_Import_SO_Item.RefNo1 = strSalesOrder_No
                strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSalesOrder_No)
                obl_Import_SO_Item.SaleOrderIndex = strArray(0)
                obl_Import_SO_Item.Currency = strArray(1)

                'ถ้าไม่มี SalesOrder_Index ให้ข้ามไปอ่านบรรทัดถัดไป
                If obl_Import_SO_Item.SaleOrderIndex = "" Then
                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                    _osbErr_Data_Incomplete.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                    iCount_Error_Detail += 1

                    'อ่านบรรทัดใหม่
                    Continue While

                Else

                    'Check Duplicate data in Sales Order Item
                    If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, strSplit(2).Trim, strSplit(1).Trim) Then
                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)

                        iCount_Error_Detail += 1

                        'อ่านบรรทัดใหม่
                        Continue While
                    End If

                    Dim objSy_AutoNumber As New Sy_AutoNumber
                    obl_Import_SO_Item.SalesOrderItemIndex = objSy_AutoNumber.getSys_Value("SalesOrderItem_Index")
                    objSy_AutoNumber = Nothing
                    obl_Import_SO_Item.ItemSeq = CInt(strSplit(1).Trim)
                    obl_Import_SO_Item.Qty = CDbl(strSplit(11).Trim)
                    obl_Import_SO_Item.TotalQty = obl_Import_SO_Item.Qty
                    obl_Import_SO_Item.Volume = 0 '
                    obl_Import_SO_Item.Str2 = "" ' strSplit(6).Trim
                    obl_Import_SO_Item.Remark = strSplit(14).Trim

                    '*** BEGIN SKU ***
                    obl_Import_SO_Item.SkuId = strSplit(10).Trim
                    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.GetSKU_Index(obl_Import_SO_Item.SkuId, Me.Customer_Index)
                    'Insert New Sku and Package
                    If obl_Import_SO_Item.SkuIndex = "" Then
                        obl_Import_SO_Item.Package_ID = strSplit(12).Trim
                        obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.InsertSKU()
                        obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, "")  'strArray(1)
                        _osbNewSKU.AppendLine("SKU : " & obl_Import_SO_Item.SkuId)
                    End If

                    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, strSplit(12).Trim)  'strArray(1)

                    If obl_Import_SO_Item.PackageIndex = "" Then
                        ' Auto Insert Package were Package and Update Package For Sku This
                        obl_Import_SO_Item.PackageIndex = SavePackage(strSplit(12).Trim)
                        Me.SaveSKURatio(obl_Import_SO_Item.SkuIndex, obl_Import_SO_Item.PackageIndex, 1) 'Default Ratio=1 ?
                    End If
                    'Get Package_Index with Package_Id



                    '*** END SKU ***

                    'Set property and Insert
                    obl_Import_SO_Item.SalesOrder_ItemInsert()

                    iCount_Comp_Detail += 1
                End If

                '*********** END ITEM ********************
            End While

            myReader.Close()

            strDestination = MoveFile(pstrFilePath)

            'Write Log
            With obl_Log
                'Move file
                .Write_To_Path = strDestination & "\" & pstrFilePath.ToUpper.Replace(Me._DEFAULT_IMPORT_SO_PATH.ToUpper, "") & ".log"
                .Process_Name = "Import"
                .Module_Name = "DO"
                .Start_Process = Now.ToString("dd/MM/yyyy HH:mm:ss")

                .Target = pstrFilePath
                .Destination = strDestination

                .Total_Header = iTotal_Header
                .Total_Detail = iTotal_Detail

                .Complete_Header = iCount_Comp_Header
                .Complete_Detail = iCount_Comp_Detail

                .Incomplete_Header = iCount_Error_Header
                .Incomplete_Detail = iCount_Error_Detail

                .Error_List = New StringBuilder

                .Error_List.AppendLine(_osbErr_Dup.ToString)
                .Error_List.AppendLine(_osbErr_Data_Incomplete.ToString)
                .Error_List.AppendLine(_osbNewSKU.ToString)

                .Write_Log()
            End With

        Catch ex As Exception
            obl_Import_SO_Item.UpdateSalesOrder_Cancel_From_Error_Interface(strSalesOrder_No)
            Throw ex
        End Try

    End Sub

    Sub SaveSKURatio(ByVal pSku_Index As String, ByVal pPackage_Index As String, ByVal ratio As Double)
        Try

            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
            objDBSKURatio.SaveData("", pSku_Index, pPackage_Index, CDbl(ratio))

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Function SavePackage(ByVal ppackage_Id As String) As String

        Try
            Dim Package_Index As String = ""
            Dim package_Id As String = ppackage_Id
            Dim description As String = ppackage_Id
            Dim dimension_Hi As Double = 0.0
            Dim dimension_Wd As Double = 0.0
            Dim dimension_Len As Double = 0.0
            Dim Weight As Double = 0.0

            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
            Package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, 0, Weight, 0) ', txtUnit_id.Text

            Return Package_Index

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function MoveFile(ByVal pstrSourceFile As String) As String
        Try

            Dim ImportFolder As String = Me._DEFAULT_IMPORT_SO_PATH


            Dim strResultFileName As String
            'Dim filemove As String

            If Directory.Exists(ImportFolder & SubFolder) = False Then
                Directory.CreateDirectory(ImportFolder & SubFolder)
            End If

            If pstrSourceFile = "" Then
                Return ""
                'Continue For
            End If


            'For i As Integer = 0 To pstrSourceFile.Length - 1
            strResultFileName = Path.GetFileName(pstrSourceFile)

            _FileMove = ImportFolder & SubFolder & "\" & strResultFileName
            File.Move(pstrSourceFile, _FileMove)
            'Next

            Return ImportFolder & SubFolder

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub ShowError(ByVal pstrErrorList As ArrayList, ByVal pintCountSucc As Integer)

        Try

            Dim strResult As String
            Dim strError As String = ""
            For Each strResult In pstrErrorList
                strError &= strResult & vbNewLine
            Next
            If strError = "" Then
                strError = "Not found."
            End If

            MsgBox("ระบบทำการ Import ข้อมูลสำเร็จ " & vbNewLine _
            & "Total : " & pintCountSucc & " record." & vbNewLine & vbNewLine _
            & "----------------------------------------------Error list------------------------------------------" & vbNewLine _
            & strError & vbNewLine _
            & "Total : " & pstrErrorList.Count & " record.")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Function ConvertToDate(ByVal pstrValue As String) As Date

        Try
            'strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4)
            pstrValue = pstrValue & "0"
            Dim strDate As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 1, 1), Mid(pstrValue, 1, 2))
            Dim strMonth As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 2, 2), Mid(pstrValue, 3, 2))
            Dim strYear As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 4, 4), Mid(pstrValue, 5, 4))

            Return CDate(strYear & "/" & strMonth & "/" & strDate)

        Catch ex As Exception

            Throw ex

        End Try

    End Function
#End Region
#Region "   Import Excel   "

    Public Function LoadWorkSheet(ByVal pstrFileName As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter

            '=============
            oConnSource.Open()

            Dim Objdt As DataTable = oConnSource.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            Dim Objds As New DataSet

            Me.DataSource = Objdt

            Return Objdt

        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function LoadDataFromFile(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
            'strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=No;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter


            With odaSource

                If pstrWorkSheet.EndsWith("$") Or pstrWorkSheet.EndsWith("$'") Then
                    pstrWorkSheet = pstrWorkSheet.Replace("''", "'") ' "Sheet1''s$"
                    '.SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "] WHERE SalesOrder_No <> '' ORDER BY SalesOrder_No ", oConnSource)
                    .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result, * FROM [" & pstrWorkSheet & "] ", oConnSource)
                Else
                    '.SelectCommand = New OleDbCommand("SELECT '' AS Check_Result,* FROM [" & pstrWorkSheet & "$] WHERE SalesOrder_No <> '' ORDER BY SalesOrder_No ", oConnSource)
                    .SelectCommand = New OleDbCommand("SELECT '' AS Check_Result, * FROM [" & pstrWorkSheet & "$] ", oConnSource)
                End If
                '===================
                .Fill(odtTemp)

            End With

            Select Case odtTemp.Columns.Count
                Case 6
                    odtTemp.Columns.Add(New DataColumn("Remark_Header"))
                    odtTemp.Columns.Add(New DataColumn("Reference1"))
                Case 7
                    odtTemp.Columns.Add(New DataColumn("Reference1"))
            End Select

            'If odtTemp.Columns.Count = 6 Then          ใช้ Select Case แทน
            '    odtTemp.Columns.Add(New DataColumn("Remark_Header"))
            '    odtTemp.Columns.Add(New DataColumn("Reference1"))
            'ElseIf odtTemp.Columns.Count = 7 Then
            '    odtTemp.Columns.Add(New DataColumn("Reference1"))
            'End If


            'If odtTemp.Columns.Contains("Remark_Header") = False Then
            '    odtTemp.Columns.Add(New DataColumn("Remark_Header"))
            'End If

            'If odtTemp.Columns.Contains("Reference1") = False Then
            '    odtTemp.Columns.Add(New DataColumn("Reference1"))
            'End If
            If odtTemp.Columns.Contains("Lot") = False Then
                odtTemp.Columns.Add("Lot", GetType(String))
            End If
            SetGridHeaderName(odtTemp)

            Me.DataSource = odtTemp

            Return odtTemp

        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Private Sub SetGridHeaderName(ByVal prtDataTable As DataTable)

        Dim ObjDT As New DataTable

        Dim column As New DataColumn

        ObjDT = prtDataTable

        ObjDT.Columns(1).ColumnName = "Cust_Inv_No"
        ObjDT.Columns(2).ColumnName = "Careof_No"
        ObjDT.Columns(3).ColumnName = "Product_No"
        ObjDT.Columns(4).ColumnName = "Qty"
        ObjDT.Columns(5).ColumnName = "Remark"
        '
        ObjDT.Columns(6).ColumnName = "Remark_Header"
        ObjDT.Columns(7).ColumnName = "Reference1"

        '
        ObjDT.Columns(1).DataType.ToString()
        ObjDT.Columns(2).DataType.ToString()
        ObjDT.Columns(3).DataType.ToString()
        'ObjDT.Columns(4).DataType.ToString()
        ObjDT.Columns(5).DataType.ToString()
        ObjDT.Columns(6).DataType.ToString()
        ObjDT.Columns(7).DataType.ToString()
    End Sub
    'Old Code
    'Public Function CheckingData() As Boolean
    '    Try
    '        Dim boolResult As Boolean = True

    '        For Each odr As DataRow In Me.DataSource.Rows
    '            For Each odc As DataColumn In Me.DataSource.Columns
    '                Select Case odc.ColumnName.Trim.ToLower
    '                    Case "Doc_No".ToLower
    '                        If IsExitData("tb_SalesOrder", "Ref_No2", odr(odc.ColumnName).ToString) = True Then
    '                            odr("Check_Result") = "ข้อมูลใบรับสินค้านี้มีอยู่ก่อนแล้ว !"
    '                            'boolResult = False
    '                            'Exit For
    '                        End If

    '                    Case "SKU_ID".ToLower

    '                        If IsExitData("ms_SKU", "Sku_Id", odr(odc.ColumnName).ToString) = False Then
    '                            odr("Check_Result") = "ไม่พบข้อมูล SKU นี้ !"
    '                            'boolResult = False
    '                            'Exit For
    '                        End If
    '                        If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
    '                            odr("Check_Result") = "กรุณากรอกข้อมูล SKU!"
    '                            boolResult = False
    '                            'Exit For
    '                        End If

    '                    Case "UOM".ToLower

    '                        '_Sku_Index = GetIndexByID("ms_SKU", "SKU_Index", "SKU_ID", odr("SKU_ID").ToString.Trim)

    '                        '_Package_Index = GetPackage_Index(_Sku_Index, odr(odc.ColumnName).ToString.Trim)

    '                        If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
    '                            odr("Check_Result") = "ไม่พบข้อมูล UOM นี้ !"
    '                            boolResult = False
    '                            'Exit For
    '                        End If



    '                    Case "QTY".ToLower
    '                        If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
    '                            odr("Check_Result") = "กรุณาป้อนจำนวน"
    '                            boolResult = False
    '                            'Exit For
    '                        ElseIf IsNumeric(odr(odc.ColumnName.Trim).ToString) = False Then
    '                            odr("Check_Result") = "กรุณาใหม่ป้อนจำนวนให้ถูกต้อง"
    '                            boolResult = False
    '                            'Exit For
    '                        End If
    '                    Case "Customer_ID".ToLower
    '                        If String.IsNullOrEmpty(odr(odc.ColumnName).ToString) Then
    '                            odr("Check_Result") = "กรุณาระบุ Customer_ID !"
    '                            boolResult = False
    '                            'Exit For
    '                        End If
    '                        'Case "width", "length", "height", "volume", "weight"
    '                        '    If odr(odc.ColumnName.Trim).ToString.Trim = "" Then
    '                        '        odr(odc.ColumnName.Trim) = 0
    '                        '    End If
    '                End Select
    '                odr("Check_Result") = "OK."
    '            Next
    '        Next

    '        Return boolResult
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Private Function GetPackage_Index(ByVal pstrSKU_Index As String, ByVal pstrPackage_ID As String) As String
    '    Try
    '        Dim oConnServer As New SqlConnection(WV_ConnectionString)
    '        Dim odtServer As New DataTable
    '        Dim odaServer As New SqlDataAdapter
    '        Dim strSQL As String

    '        strSQL = " SELECT P.Package_Index "
    '        strSQL &= " FROM ms_Package P INNER JOIN ms_SKU S ON P.Package_Index = S.Package_Index"
    '        strSQL &= " 	INNER JOIN ms_SKURatio SR ON SR.SKU_index = S.SKU_Index AND P.Package_Index = SR.Package_Index"
    '        strSQL &= " WHERE S.SKU_index = '" & pstrSKU_Index & "' AND  P.Description = '" & pstrPackage_ID.Replace("'", "''") & "'"

    '        odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

    '        odaServer.Fill(odtServer)

    '        If odtServer.Rows.Count > 0 Then
    '            Return odtServer.Rows(0)("Package_Index").ToString
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Private Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
    '    Try
    '        Dim oConnServer As New SqlConnection(WV_ConnectionString)
    '        Dim odtServer As New DataTable
    '        Dim odaServer As New SqlDataAdapter
    '        Dim strSQL As String

    '        strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = '" & pstrField_ID_Value.Trim & "' and Status_Id <> -1 "

    '        odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

    '        odaServer.Fill(odtServer)

    '        If odtServer.Rows.Count > 0 Then
    '            Return odtServer.Rows(0)(pstrField_Index).ToString
    '        Else
    '            Return ""
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    'Public Function IsExitData(ByVal pstrTableName As String, ByVal pstrFieldName As String, ByVal pstrFieldValue As String) As Boolean
    '    Try
    '        Dim oConnServer As New SqlConnection(WV_ConnectionString)
    '        Dim odtServer As New DataTable
    '        Dim odaServer As New SqlDataAdapter
    '        Dim strSQL As String

    '        strSQL = "SELECT " & pstrFieldName & " FROM " & pstrTableName & " WHERE " & pstrFieldName & " = '" & pstrFieldValue.Trim & "'"
    '        Select Case pstrTableName
    '            Case "tb_Withdraw", "tb_Order", "tb_AdvanceShipNotice"
    '                strSQL &= " And Status <> -1"
    '            Case Else
    '                strSQL &= " And Status_Id <> -1"
    '        End Select
    '        odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

    '        odaServer.Fill(odtServer)

    '        If odtServer.Rows.Count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function


    'Private Function IsCheckPackage(ByVal pstrpackage_Id As String, ByVal pstrsku_Index As String) As Boolean
    '    Try
    '        Dim strSQL As String

    '        strSQL = "SELECT    ms_Package.* "
    '        strSQL &= " FROM        ms_Package INNER JOIN"
    '        strSQL &= "            ms_SKURatio ON ms_Package.Package_Index = ms_SKURatio.Package_Index INNER JOIN"
    '        strSQL &= "    ms_SKU ON ms_SKURatio.Sku_Index = ms_SKU.Sku_Index"
    '        strSQL &= "   WHERE ms_SKU.status_id <> -1"
    '        strSQL &= "    and ms_Package.package_Id = '" & pstrpackage_Id & "' and ms_SKU.sku_Index ='" & pstrsku_Index & "' "


    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        If GetDataTable.Rows.Count > 0 Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function GetCareOf_Index(ByVal pstrModel As String, ByVal pstrCustomer As String) As String
        Try

            Dim strSQL As String = ""
            Dim strReturn As String


            strSQL &= " SELECT * " 'get careof_index
            strSQL &= " FROM ms_Customer_Shipping "
            strSQL &= " WHERE Str1 = '" & pstrModel & "' and Customer_Index = '" & pstrCustomer & "' and Status_id <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("Customer_Shipping_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function GetSKU_Index(ByVal pstrModel As String, ByVal pstrCustomer As String) As String
        Try

            Dim strSQL As String = ""
            Dim strReturn As String

            strSQL &= " SELECT * " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE SKU_Id = '" & pstrModel & "' and Customer_Index = '" & pstrCustomer & "' and Status_id <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("SKU_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function GetSaleOrder(ByVal pstrCustomerOrderNo As String, ByVal pstrCustomerIndex As String) As DataTable
        Try

            Dim strSQL As String = "" 'SalesOrder_Index,SalesOrder_No
            strSQL = " select * from tb_Salesorder where status <> -1 and Ref_No2 = '" & pstrCustomerOrderNo & "' and Customer_Index = '" & pstrCustomerIndex & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

#End Region
    Public Sub UpdateConfig()
        Try
            Dim strSQL As String = ""



            strSQL = " UPDATE config_Import_SO SET SOLD_TO_ID = " & Me._config_SO_No
            strSQL &= " ,SO_NO = " & Me._config_SO_Date
            strSQL &= " ,SOLD_TO_NAME = " & Me._config_Supplier_Code
            strSQL &= " ,DOC_DATE = " & Me._config_Supplier_Name
            strSQL &= " ,SOLD_TO_ADD1 = " & Me._config_Supplier_Address
            strSQL &= " ,SALE_NAME = " & Me._config_SO_Exp
            strSQL &= " ,SOLD_TO_ADD2 = " & Me._config_Supplier_Tel
            strSQL &= " ,SOLD_TO_TEL = " & Me._config_Supplier_Fax
            strSQL &= " ,EXPECT_DELIVERY_DATE = " & Me._config_Sku_Id
            strSQL &= " ,SEQ = " & Me._config_Sku_Name
            strSQL &= " ,SKU_ID = " & Me._config_Sku_Package
            strSQL &= " ,QTY = " & Me._config_Sku_Qty
            strSQL &= " ,PACKAGE = " & Me._config_Sku_Price
            strSQL &= " ,Remark = " & Me._config_Remark

            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub


    Public Sub UpdateConfig(ByVal Format_Import_Index As String)
        Try
            Dim strSQL As String = ""



            strSQL = " UPDATE config_Import_SO SET SOLD_TO_ID = " & Me._config_SO_No
            strSQL &= " ,SO_NO = " & Me._config_SO_Date
            strSQL &= " ,SOLD_TO_NAME = " & Me._config_Supplier_Code
            strSQL &= " ,DOC_DATE = " & Me._config_Supplier_Name
            strSQL &= " ,SOLD_TO_ADD1 = " & Me._config_Supplier_Address
            strSQL &= " ,SALE_NAME = " & Me._config_SO_Exp
            strSQL &= " ,SOLD_TO_ADD2 = " & Me._config_Supplier_Tel
            strSQL &= " ,SOLD_TO_TEL = " & Me._config_Supplier_Fax
            strSQL &= " ,EXPECT_DELIVERY_DATE = " & Me._config_Sku_Id
            strSQL &= " ,SEQ = " & Me._config_Sku_Name
            strSQL &= " ,SKU_ID = " & Me._config_Sku_Package
            strSQL &= " ,QTY = " & Me._config_Sku_Qty
            strSQL &= " ,PACKAGE = " & Me._config_Sku_Price
            strSQL &= " ,Remark = " & Me._config_Remark
            strSQL &= " WHERE Format_Import_Index = '" & Format_Import_Index & "'"

            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub




    Public Function fnExitsDocNoDup(ByVal pTableName As String, ByVal pFieldName As String, ByVal pDocNo As String, ByVal pstrWhere As String) As Boolean
        Try
            Dim Result As String = ""

            Result = DBExeQuery_Scalar(String.Format(" SELECT count(*) from {0} WHERE 1=1 AND {1} = '{2}' {3}  ", pTableName, pFieldName, pDocNo, pstrWhere))

            If IsNumeric(Result) Then
                If Result = 0 Then Return False
                Return True
            Else
                Return False
            End If




        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDataSKUPACKAGEByBarcode(ByVal pBarcodePackage As String) As DataTable
        Try

            Dim strSQL$ = ""


            ' CASE isnull(ms_SKU.Barcode2,'') WHEN ''  THEN '0' ELSE ms_SKU.Barcode2 END  as Barcode2

            'strSQL &= " select ms_sku.sku_id,ms_Package.Package_Id from ms_sku inner join ms_SKURatio on ms_sku.Sku_Index = ms_SKURatio.Sku_Index  "
            'strSQL &= " inner join ms_Package on ms_SKURatio.Package_Index = ms_SKURatio.Package_Index where ms_Package.status_id <> -1 and ms_SKU.status_id <> -1 and ms_SKURatio.status_id <> -1  "
            'strSQL &= String.Format(" and  ms_sku.sku_id = '{0}'  ", sku_id)

            strSQL &= " select ms_sku.sku_id,ms_Package.Package_Id from ms_sku inner join ms_SKURatio on ms_sku.Sku_Index = ms_SKURatio.Sku_Index  "
            strSQL &= " inner join ms_Package on ms_SKURatio.Package_Index = ms_Package.Package_Index where ms_Package.status_id <> -1 and ms_SKU.status_id <> -1 and ms_SKURatio.status_id <> -1  "
            strSQL &= String.Format(" and  ms_Package.Barcode = '{0}'  ", pBarcodePackage)


            Return DBExeQuery(strSQL)


        Catch ex As Exception
            Throw ex
        End Try
    End Function
End Class

