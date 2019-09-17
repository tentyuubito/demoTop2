Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data.SqlClient
Imports System.Globalization
Imports System.Threading
Imports System.Math
Imports System.Configuration.ConfigurationSettings

Public Class clsImportSO_V2 : Inherits DBType_SQLServer
    Dim appSet As New Configuration.AppSettingsReader()
#Region " variables"

#Region "ChkNull Header"
    Public ChkDupe_SalesOrder_No As String = ""
    Public ChkNull_SalesOrder_Date As String = ""
    Public ChkDupe_Carrier_Index As String = ""
    Public ChkDupe_Customer_Receive_Location_ID As String = ""
    Public ChkNull_Expected_Delivery_Date As String = ""
    Public ChkNull_Delivery_Date As String = ""
    Public ChkDupe_Customer_Index As String = ""
    Public ChkDupe_Supplier_Index As String = ""
    Public ChkDupe_Department_Index As String = ""
    Public ChkDupe_DocumentType_Index As String = ""
    Public ChkNull_Remark As String = ""
    Public ChkNull_Credit_Term As String = ""
    Public ChkDupe_Currency_Index As String = ""
    Public ChkNull_Exchange_Rate As String = ""
    Public ChkDupe_PaymentMethod_Index As String = ""
    Public ChkNull_Payment_Ref As String = ""
    Public ChkNull_FullPaid_Date As String = ""
    Public ChkNull_Amount As String = ""
    Public ChkNull_Discount_Percent As String = ""
    Public ChkNull_Deposit_Amt As String = ""
    Public ChkNull_Total_Amt As String = ""
    Public ChkNull_VAT_Percent As String = ""
    Public ChkNull_VAT As String = ""
    Public ChkNull_Net_Amt As String = ""
    Public ChkNull_Reserve_index As String = ""
    Public ChkNull_Supplier_Index As String = ""

    Public ChkNull_Str1 As String = ""
    Public ChkNull_Str2 As String = ""
    Public ChkNull_Str3 As String = ""
    Public ChkNull_Str4 As String = ""
    Public ChkNull_Str5 As String = ""
    Public ChkNull_Str6 As String = ""
    Public ChkNull_Str7 As String = ""
    Public ChkNull_Str8 As String = ""
    Public ChkNull_Str9 As String = ""
    Public ChkNull_Str10 As String = ""
    Public ChkNull_Flo1 As String = ""
    Public ChkNull_Flo2 As String = ""
    Public ChkNull_Flo3 As String = ""
    Public ChkNull_Flo4 As String = ""
    Public ChkNull_Flo5 As String = ""
    Public ChkNull_PO_No As String = ""
    Public ChkNull_PO_Date As String = ""
    Public ChkNull_Time_ExpectedDocPickup As String = ""
    Public ChkNull_Time_DocPickup As String = ""
    Public ChkNull_Time_DocTripConfirmed As String = ""
    Public ChkNull_Time_DeliveryToDestination As String = ""
    Public ChkNull_Time_DocReturnedToDC As String = ""
    Public ChkNull_Time_DocReturnedToSource As String = ""
    Public ChkNull_Time_DocReturnedToOwner As String = ""
    Public ChkNull_Time_DocConfirmedByOwner As String = ""
    Public ChkNull_Time_DestinationOutGate As String = ""
    Public ChkNull_Total_Qty_Pack As String = ""
    Public ChkNull_Last_Withdraw_Date As String = ""
    Public ChkDupe_Customer_Shipping_Index As String = ""
    Public ChkDupe_Customer_Shipping_Location_Index As String = ""

#End Region

#Region "ChkNull Detail"
    Public ChkNull_Item_Seq As String = ""
    Public ChkRatio As String = ""
    Public ChkNull_Total_Qty As String = ""
    Public ChkNull_Qty As String = ""
    Public ChkNull_Weight As String = ""
    Public ChkNull_Volume As String = ""
    Public ChkNull_Serial_No As String = ""
    Public ChkNull_Total_Qty_Withdraw As String = ""
    Public ChkNull_Qty_Withdraw As String = ""
    Public ChkNull_Weight_Withdraw As String = ""
    Public ChkNull_Volume_Withdraw As String = ""
    Public ChkNull_Last_Received_Date As String = ""
    Public ChkNull_UnitPrice As String = ""
    Public ChkNull_Amount_Detail As String = ""
    Public ChkNull_Discount_Amt_Detail As String = ""
    Public ChkNull_Total_Amt_Detail As String = ""
    Public ChkNull_Remark_Detail As String = ""
    Public ChkNull_Reason As String = ""
    Public ChkNull_Ref_No1 As String = ""
    Public ChkNull_Ref_No2 As String = ""
    Public ChkNull_Str1_Detail As String = ""
    Public ChkNull_Str2_Detail As String = ""
    Public ChkNull_Str3_Detail As String = ""
    Public ChkNull_Str4_Detail As String = ""
    Public ChkNull_Str5_Detail As String = ""
    Public ChkNull_Str6_Detail As String = ""
    Public ChkNull_Str7_Detail As String = ""
    Public ChkNull_Str8_Detail As String = ""
    Public ChkNull_Str9_Detail As String = ""
    Public ChkNull_Str10_Detail As String = ""
    Public ChkNull_Flo1_Detail As String = ""
    Public ChkNull_Flo2_Detail As String = ""
    Public ChkNull_Flo3_Detail As String = ""
    Public ChkNull_Flo4_Detail As String = ""
    Public ChkNull_Flo5_Detail As String = ""
    Public ChkNull_Charge_Status As String = ""
    Public ChkNull_PLot As String = ""
    Public ChkNull_ERP_Location As String = ""
    Public ChkNull_HandOver_Total_Qty As String = ""
    Public ChkNull_Total_Qty_Packed As String = ""
    Public ChkDupe_Package_Index As String = ""
    Public ChkNull_ItemStatus_Index As String = ""

#End Region

#Region "Variable"
    Public _Import_Log_Index As String = ""
    Public _Complete_Row_Count As Integer = 0
    Public _Start_Timestamp As Date
    Public _Imp_Row_Count As Integer = 0
#End Region

    Public _dtHeader As New DataTable
    Public _dtDetail As New DataTable
    Public _dtConfig As New DataTable
    Public DocumentType_Index_config As String = ""
    Public ItemStatus_Index As String = ""
    Public Chk_isUSE As String = ""

    Private _dataTable As DataTable = New DataTable
    Private _File_Prefix As String = String.Empty
    Private _Process_ID As Integer = 0
    Private _Err_Checkpoint As String = ""

    Private drChkDupe() As DataRow

    Public strFileName As String = "Data"
    Private _DtTemp As New DataTable

    Private remark2 As String = ""
    Public Remark_Log As String = ""
    Private _status As Object
    Private _GUid As String
    Private _SalesOrderItem_Index As String = Nothing
    Private _SalesOrder_Index As String = Nothing
    Private _SalesOrder_No As String = Nothing
    Private _DocumentType_Index As String = String.Empty
#End Region

#Region "  Property"

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public Property DtTemp() As DataTable
        Get
            Return _DtTemp
        End Get
        Set(ByVal value As DataTable)
            _DtTemp = value
        End Set
    End Property

    Public Property GUid() As String
        Get
            Return _GUid
        End Get
        Set(ByVal value As String)
            _GUid = value
        End Set
    End Property

    Public Property status() As Object
        Get
            Return _status
        End Get
        Set(ByVal value As Object)
            _status = value
        End Set
    End Property

    Public Property SalesOrder_No() As String
        Get
            Return _SalesOrder_No
        End Get
        Set(ByVal value As String)
            _SalesOrder_No = value
        End Set
    End Property

    Public Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property
    Public Property File_Prefix() As String
        Get
            Return _File_Prefix
        End Get
        Set(ByVal value As String)
            _File_Prefix = value
        End Set
    End Property

    Public Property Process_ID() As Integer
        Get
            Return _Process_ID
        End Get
        Set(ByVal value As Integer)
            _Process_ID = value
        End Set
    End Property
#End Region

#Region " Configreport"
    Public Function fnGetConfigImport(ByVal pFile_Prefix As String, ByVal pProcess_id As Integer, ByVal pIsUse As Integer) As DataTable

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM config_import_text "
            strSQL &= " WHERE File_Prefix = '" & pFile_Prefix & "'"
            strSQL &= " And Process_id =" & pProcess_id
            strSQL &= " And IsUse = " & pIsUse
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

    Public Function ReadData() As Boolean
        Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        Dim objkascoCLB As New KascoCLB.Import
        Dim objkascoStructure As New KascoCLB.StructureTable
        Dim objDBSaveLog As New Insert_Log
        Dim oDS As New DataSet
        Dim DrHeader As DataRow
        Dim DrDetail As DataRow
        Dim strField_Type As String

        Dim DtSku As New DataTable
        Dim DtSkuPKRatio As New DataTable
        Me._Err_Checkpoint = "STEP GetConfig "
        oDS = objkascoStructure.getStructure_Table("tb_SalesOrder", "tb_SalesOrderItem")

        _dtHeader = oDS.Tables("Header")
        _dtDetail = oDS.Tables("Detail")

        If Not _dtDetail.Columns.Contains("SalesOrder_No") Then
            _dtDetail.Columns.Add("SalesOrder_No")
        End If
        _Start_Timestamp = Date.Now
        Dim iCurrent_Line_Number As Integer = 0

        Try
            Dim objAutonumber As New Sy_AutoNumber
            _Import_Log_Index = objAutonumber.getSys_Value("Import_Log_Index")

            ''''''''''''''GET Config Interface''''''''''''''''table --> Config_Import_Text
            _dtConfig = fnGetConfigImport("SO", 10, 1)
            If _dtConfig.Rows.Count = 0 Then
                objDBSaveLog.insertErrorLog("", " Can't Find Import Config", _Err_Checkpoint, "", _Import_Log_Index, "")
                Exit Function
            End If

            '***************************************************
            '------ Check  & Add Data in dataTable Header -----'
            '***************************************************
            Dim blChkHaveSoNo As Boolean = False

            For i As Integer = 0 To _DtTemp.Rows.Count - 1

                Line_Err = i
                strField_Type = "Header"
                DrDetail = _dtDetail.NewRow
                Me._Err_Checkpoint = "STEP 1 Add Header"
                _Err_Msg = "Add SalesOrder_No"
                Dim SalesOrder_No_FromTemp As String = ""
                SalesOrder_No_FromTemp = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "SalesOrder_No"))
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "SalesOrder_No") Then
                    If _dtHeader.Rows.Count > 0 Then
                        drChkDupe = _dtHeader.Select("SalesOrder_No ='" & SalesOrder_No_FromTemp & "'")
                        If drChkDupe.Length <= 0 Then
                            '----- Check Find in Database  ------'
                            ChkDupe_SalesOrder_No = objkascoCLB.check_Dupe("tb_SalesOrder", "SalesOrder_No", "SalesOrder_No = '" & SalesOrder_No_FromTemp & "' AND  Status <> -1")
                            If Not ChkDupe_SalesOrder_No Is Nothing OrElse Not ChkDupe_SalesOrder_No = "" Then
                                objDBSaveLog.insertErrorLog("", _Err_Msg, _Err_Checkpoint, "", _Import_Log_Index, "SalesOrder_No Duplicate")
                                Continue For
                            Else
                                ' New Row Header For Add Data
                                DrHeader = _dtHeader.NewRow
                                DrHeader("SalesOrder_No") = SalesOrder_No_FromTemp
                                DrDetail("SalesOrder_No") = DrHeader("SalesOrder_No")
                                blChkHaveSoNo = False
                            End If
                        Else
                            '--- Have So No . in TmpTable
                            blChkHaveSoNo = True
                            DrDetail("SalesOrder_No") = SalesOrder_No_FromTemp
                        End If
                    Else
                        ChkDupe_SalesOrder_No = objkascoCLB.check_Dupe("tb_SalesOrder", "SalesOrder_No", "SalesOrder_No = '" & SalesOrder_No_FromTemp & "' AND  Status <> -1")
                        If Not ChkDupe_SalesOrder_No Is Nothing OrElse Not ChkDupe_SalesOrder_No = "" Then
                            objDBSaveLog.insertErrorLog(ChkDupe_SalesOrder_No, _Err_Msg, _Err_Checkpoint, "", _Import_Log_Index, "SalesOrder_No Duplicate")
                            Continue For
                        End If

                        DrHeader = _dtHeader.NewRow
                        DrHeader("SalesOrder_No") = SalesOrder_No_FromTemp
                        DrDetail("SalesOrder_No") = DrHeader("SalesOrder_No")
                    End If
                Else
                    '--- IsUse = false
                    '--- Auto Gen PO No
                    DrHeader = _dtHeader.NewRow
                    Dim objDocumentNumber As New Sy_DocumentNumber
                    DrHeader("SalesOrder_No") = (objDocumentNumber.getAuto_DocumentNumber("SO", "SalesOrder_No", "tb_SalesOrder"))
                    objDocumentNumber = Nothing

                    DrDetail("SalesOrder_No") = SalesOrder_No_FromTemp
                    blChkHaveSoNo = False
                End If

                If blChkHaveSoNo = False Then
                    '================= PurchaseOrder_Date ==========================
                    _Err_Msg = "Add SalesOrder_Date"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "SalesOrder_Date") Then
                        ChkNull_SalesOrder_Date = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "SalesOrder_Date")).ToString
                        If Not ChkNull_SalesOrder_Date Is Nothing OrElse Not ChkNull_SalesOrder_Date = "" Then
                            DrHeader("SalesOrder_Date") = ChkNull_SalesOrder_Date
                        Else
                            DrHeader("SalesOrder_Date") = DateServer
                        End If
                    Else
                        DrHeader("SalesOrder_Date") = DateServer
                    End If

                    '================= Carrier_Index  // Compare //  Carrier_ID =================================
                    _Err_Msg = "Add Carrier_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Carrier_ID") = 1 Then
                        ChkDupe_Carrier_Index = objkascoCLB.check_Dupe("ms_Carrier", "Carrier_Index", "Carrier_id = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Carrier_id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Carrier_Index Is Nothing OrElse Not ChkDupe_Carrier_Index = "" Then
                            DrHeader("Carrier_Index") = ChkDupe_Carrier_Index
                        Else
                            DrHeader("Carrier_Index") = Carrier_Index
                        End If
                    Else
                        DrHeader("Carrier_Index") = Carrier_Index
                    End If
                    '================= Customer_Receive_Location_Index // Compare //  Customer_Receive_Location_ID =================
                    _Err_Msg = "Add Customer_Receive_Location_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Customer_Receive_Location_ID") Then
                        ChkDupe_Customer_Receive_Location_ID = objkascoCLB.check_Dupe("ms_Customer_Receive_Location", "Customer_Receive_Location_Index", "Customer_Receive_Location_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Receive_Location_Index")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Customer_Receive_Location_ID Is Nothing OrElse Not ChkDupe_Customer_Receive_Location_ID = "" Then
                            DrHeader("Customer_Receive_Location_Index") = ChkDupe_Customer_Receive_Location_ID
                        Else
                            DrHeader("Customer_Receive_Location_Index") = Customer_Receive_Location_Index
                        End If
                    Else
                        DrHeader("Customer_Receive_Location_Index") = Customer_Receive_Location_Index
                    End If
                    '================= Expected_Delivery_Date  =================
                    _Err_Msg = "Add Expected_Delivery_Date"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Expected_Delivery_Date") Then
                        ChkNull_Expected_Delivery_Date = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Expected_Delivery_Date")).ToString
                        If Not ChkNull_Expected_Delivery_Date Is Nothing OrElse Not ChkNull_Expected_Delivery_Date = "" Then
                            DrHeader("Expected_Delivery_Date") = ChkNull_Expected_Delivery_Date
                        Else
                            DrHeader("Expected_Delivery_Date") = DateServer
                        End If
                    Else
                        DrHeader("Expected_Delivery_Date") = DateServer
                    End If
                    '================= Delivery_Date =================
                    _Err_Msg = "Add Delivery_Date"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Delivery_Date") Then
                        ChkNull_Delivery_Date = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Delivery_Date")).ToString
                        If Not ChkNull_Delivery_Date Is Nothing OrElse Not ChkNull_Delivery_Date = "" Then
                            DrHeader("Delivery_Date") = ChkNull_Delivery_Date
                        Else
                            DrHeader("Delivery_Date") = DateServer
                        End If
                    Else
                        DrHeader("Delivery_Date") = DateServer
                    End If


                    '================= Customer_Shipping_Index  // Compare // Customer_Id ลูกค้าของลูกค้า =================
                    _Err_Msg = "Add Customer_Shipping_Index"

                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Customer_Id") Then
                        'Dim s As String = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Id"))
                        ChkDupe_Customer_Shipping_Index = objkascoCLB.check_Dupe("ms_Customer_Shipping", "Customer_Shipping_Index", "Str1 = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Customer_Shipping_Index Is Nothing OrElse Not ChkDupe_Customer_Shipping_Index = "" Then
                            DrHeader("Customer_Shipping_Index") = ChkDupe_Customer_Shipping_Index
                        Else
                            Dim Company_Name = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Company_Name"))
                            Dim ObjShipping As New ms_AutoCustomer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)
                            Dim ChkDupe_Customer_Shipping_Id = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Id"))
                            Dim Address = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Address"))
                            ChkDupe_Customer_Shipping_Index = ObjShipping.InsertCustomer_Shipping(ChkDupe_Customer_Shipping_Id, Company_Name, Address)
                            If ChkDupe_Customer_Shipping_Index Is Nothing OrElse ChkDupe_Customer_Shipping_Index = "" Then
                                'Update_Prepare_Imports()
                                'Return False
                                DrHeader("Customer_Shipping_Index") = ""
                            Else
                                DrHeader("Customer_Shipping_Index") = ChkDupe_Customer_Shipping_Index
                            End If
                        End If
                    Else
                        DrHeader("Customer_Shipping_Index") = ChkDupe_Customer_Shipping_Index
                    End If
                    '================= Customer_Shipping_Location_Index  // Compare // Customer_Shipping_Location_Id  =================
                    _Err_Msg = "Add Customer_Shipping_Location_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Customer_Shipping_Location_Id") Then
                        'Dim ss As String = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Shipping_Location_Id"))
                        ChkDupe_Customer_Shipping_Location_Index = objkascoCLB.check_Dupe("ms_Customer_Shipping_Location", "Customer_Shipping_Location_Index", "Customer_Shipping_Location_Id = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Shipping_Location_Id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Customer_Shipping_Location_Index Is Nothing OrElse Not ChkDupe_Customer_Shipping_Location_Index = "" Then
                            DrHeader("Customer_Shipping_Location_Index") = ChkDupe_Customer_Shipping_Location_Index
                        Else
                            Dim Shipping_Location_Name = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Shipping_Location_Name"))
                            Dim Shipping_Location_Address = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Address"))
                            Dim ObjShippingLocation As New ms_AutoCustomer_Shipping_Location(ms_Customer_Shipping.enuOperation_Type.ADDNEW)
                            Dim Customer_Shipping_Location_Id = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Shipping_Location_Id"))
                            ChkDupe_Customer_Shipping_Location_Index = ObjShippingLocation.InsertCustomer_Shipping_Location(Customer_Shipping_Location_Id, Shipping_Location_Name, Shipping_Location_Address)
                            If ChkDupe_Customer_Shipping_Location_Index Is Nothing OrElse ChkDupe_Customer_Shipping_Location_Index = "" Then
                                'Update_Prepare_Imports()
                                'Return False
                                DrHeader("Customer_Shipping_Location_Index") = ""
                            Else
                                DrHeader("Customer_Shipping_Location_Index") = ChkDupe_Customer_Shipping_Location_Index
                            End If
                        End If
                    Else
                        DrHeader("Customer_Shipping_Location_Index") = ChkDupe_Customer_Shipping_Location_Index
                    End If

                    '================= Customer_Index  // Compare // Customer_Id =================
                    '_Err_Msg = "Add Customer_Index"
                    'If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Customer_Id") Then
                    '    ' ChkDupe_Customer_Index = chkDupe("ms_Customer", "Customer_Index", "Customer_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Id")) & "' AND  status_id <> -1")
                    '    ChkDupe_Customer_Index = objkascoCLB.check_Dupe("ms_Customer", "Customer_Index", "Customer_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Customer_Id")) & "' AND  status_id <> -1")
                    '    If Not ChkDupe_Customer_Index Is Nothing OrElse Not ChkDupe_Customer_Index = "" Then
                    '        DrHeader("Customer_Index") = Customer_Index
                    '    Else
                    '        DrHeader("Customer_Index") = Customer_Index
                    '    End If
                    'Else
                    DrHeader("Customer_Index") = Customer_Index
                    'End If

                    '================= Customer_Name Check Null =================
                    'If chk_isuse(_dtConfig, strField_Type, "Customer_Name") = 1 Then
                    '    ChkNull_Customer_Name = _DtTemp.Rows(i)(get_TabIndex(_dtConfig, strField_Type, "Customer_Name", iProcess, strPrefix)).ToString
                    '    If Not ChkNull_Customer_Name Is Nothing OrElse Not ChkNull_Customer_Name = "" Then
                    '        DrHeader("Customer_Name") = ChkNull_Customer_Name
                    '    Else
                    '        DrHeader("Customer_Name") = ""
                    '    End If
                    'Else
                    '    DrHeader("Customer_Name") = ""
                    'End If
                    '================= Supplier_Index // Compare // Supplier_Id =================
                    _Err_Msg = "Add Supplier_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Supplier_Id") Then
                        ChkDupe_Supplier_Index = objkascoCLB.check_Dupe("ms_Supplier", "Supplier_Index", "Supplier_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Supplier_Id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Supplier_Index Is Nothing OrElse Not ChkDupe_Supplier_Index = "" Then
                            DrHeader("Supplier_Index") = ChkDupe_Supplier_Index
                        Else
                            'DrHeader("Supplier_Index") = Supplier_Index
                            If Auto_Insert_Supplier = "1" Then
                                Dim Supplier_Id = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Supplier_Id"))
                                Dim Supplier_Name = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Supplier_Name"))
                                Dim Address = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Address"))
                                Dim ObjSupplier As New ms_AutoCustomer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)
                                DrHeader("Supplier_Index") = ObjSupplier.Insert_Supplier(Supplier_Id, Supplier_Name, Address)
                            End If

                        End If
                    Else
                        DrHeader("Supplier_Index") = Supplier_Index
                    End If
                    '================= Department_Index // Compare // DocumentType_Id =================
                    _Err_Msg = "Add Department_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Department_Id") Then
                        ChkDupe_Department_Index = objkascoCLB.check_Dupe("ms_Department", "Department_Index", "Department_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Department_Id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Department_Index Is Nothing OrElse Not ChkDupe_Department_Index = "" Then
                            DrHeader("Department_Index") = ChkDupe_Department_Index
                        Else
                            DrHeader("Department_Index") = Department_Index
                        End If
                    Else
                        DrHeader("Department_Index") = Department_Index
                    End If
                    '================= DocumentType_Index // Compare // DocumentType_Id =================
                    _Err_Msg = "Add DocumentType_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Department_Id") Then
                        ChkDupe_DocumentType_Index = objkascoCLB.check_Dupe("ms_DocumentType", "DocumentType_Index", "DocumentType_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "DocumentType_Id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_DocumentType_Index Is Nothing OrElse Not ChkDupe_DocumentType_Index = "" Then
                            DrHeader("DocumentType_Index") = ChkDupe_DocumentType_Index
                        Else
                            If Not DocumentType_Index = String.Empty Then
                                DrHeader("DocumentType_Index") = DocumentType_Index
                            Else
                                DrHeader("DocumentType_Index") = DocumentType_Index_config
                            End If
                        End If
                    Else
                        If Not DocumentType_Index = String.Empty Then
                            DrHeader("DocumentType_Index") = DocumentType_Index
                        Else
                            DrHeader("DocumentType_Index") = DocumentType_Index_config
                        End If
                    End If
                    '================= Remark Check Null =================
                    _Err_Msg = "Add Remark"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Remark") Then
                        ChkNull_Remark = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Remark")).ToString
                        If Not ChkNull_Remark Is Nothing OrElse Not ChkNull_Remark = "" Then
                            DrHeader("Remark") = ChkNull_Remark
                        Else
                            DrHeader("Remark") = "" 'For Default value
                        End If
                    Else
                        If KascoCLB.Import.check_Isuse(_dtConfig, "Detail", "Remark") Then
                            ChkNull_Remark_Detail = _DtTemp.Rows(_DtTemp.Rows.Count - 1)(KascoCLB.Import.get_TabIndex(_dtConfig, "Detail", "Remark")).ToString
                            If Not ChkNull_Remark_Detail Is Nothing OrElse Not ChkNull_Remark_Detail = "" Then
                                DrHeader("Remark") = ChkNull_Remark_Detail
                            Else
                                DrHeader("Remark") = "" 'For Default value
                            End If
                        Else
                            DrHeader("Remark") = "" 'For Default value
                        End If
                    End If
                    '================= Credit_Term Check Null =================
                    _Err_Msg = "Add Credit_Term"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Credit_Term") Then
                        ChkNull_Credit_Term = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Credit_Term")).ToString
                        If Not ChkNull_Credit_Term Is Nothing OrElse Not ChkNull_Credit_Term = "" Then
                            DrHeader("Credit_Term") = ChkNull_Credit_Term
                        Else
                            DrHeader("Credit_Term") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Credit_Term") = "" 'For Default value
                    End If
                    '================= Currency_Index // Compare // Currency_Id =================
                    _Err_Msg = "Add Currency_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Currency_Id") Then
                        ChkDupe_Currency_Index = objkascoCLB.check_Dupe("svms_Currency", "Currency_Index", "Currency_Index = '" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Currency_Id")) & "' AND  status_id <> -1")
                        If Not ChkDupe_Currency_Index Is Nothing OrElse Not ChkDupe_Currency_Index = "" Then
                            DrHeader("Currency_Index") = ChkDupe_Currency_Index
                        Else
                            DrHeader("Currency_Index") = Currency_Index
                        End If
                    Else
                        DrHeader("Currency_Index") = Currency_Index
                    End If

                    '================= Exchange_Rate Check Null =================
                    _Err_Msg = "Add Exchange_Rate"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Exchange_Rate") Then
                        ChkNull_Exchange_Rate = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Exchange_Rate")).ToString
                        If Not ChkNull_Exchange_Rate Is Nothing OrElse Not ChkNull_Exchange_Rate = "" Then
                            DrHeader("Exchange_Rate") = ChkNull_Exchange_Rate
                        Else
                            DrHeader("Exchange_Rate") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Exchange_Rate") = 0 'For Default value
                    End If
                    '================= PaymentMethod_Index // Compare // PaymentMethod_Id =================
                    _Err_Msg = "Add PaymentMethod_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "PaymentMethod_Id") Then
                        'อาจจะมีการ check ภายหลัง
                        'ChkDupe_PaymentMethod_Index = chkDupe("svms_Currency", "PaymentMethod_Id", "PaymentMethod_Id = '" & _DtTemp.Rows(i)(get_TabIndex(_dtConfig, strField_Type, "PaymentMethod_Id", iProcess, strPrefix)) & "' AND  status_id <> -1")
                        'If Not ChkDupe_PaymentMethod_Index Is Nothing OrElse Not ChkDupe_PaymentMethod_Index = "" Then
                        '    DrHeader("PaymentMethod_Index") = ChkDupe_PaymentMethod_Index
                        'Else
                        DrHeader("PaymentMethod_Index") = PaymentMethod_Index 'For Default value
                        'End If
                    Else
                        DrHeader("PaymentMethod_Index") = PaymentMethod_Index 'For Default value
                    End If
                    '================= Payment_Ref Check Null =================
                    _Err_Msg = "Add Payment_Ref"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Payment_Ref") Then
                        ChkNull_Payment_Ref = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Payment_Ref")).ToString
                        If Not ChkNull_Payment_Ref Is Nothing OrElse Not ChkNull_Payment_Ref = "" Then
                            DrHeader("Payment_Ref") = ChkNull_Payment_Ref
                        Else
                            DrHeader("Payment_Ref") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Payment_Ref") = "" 'For Default value
                    End If
                    '================= FullPaid_Date Check Null =================
                    _Err_Msg = "Add FullPaid_Date"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "FullPaid_Date") Then
                        ChkNull_FullPaid_Date = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "FullPaid_Date")).ToString
                        If Not ChkNull_FullPaid_Date Is Nothing OrElse Not ChkNull_FullPaid_Date = "" Then
                            DrHeader("FullPaid_Date") = ChkNull_FullPaid_Date
                        Else
                            DrHeader("FullPaid_Date") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("FullPaid_Date") = DateServer 'For Default value
                    End If
                    '================= Amount Check Null =================
                    _Err_Msg = "Add Amount"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Amount") Then
                        ChkNull_Amount = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Amount")).ToString
                        If Not ChkNull_Amount Is Nothing OrElse Not ChkNull_Amount = "" Then
                            DrHeader("Amount") = ChkNull_Amount
                        Else
                            DrHeader("Amount") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Amount") = 0 'For Default value
                    End If
                    '================= Discount_Percent Check Null =================
                    _Err_Msg = "Add Discount_Percent"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Discount_Percent") Then
                        ChkNull_Discount_Percent = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Discount_Percent")).ToString
                        If Not ChkNull_Discount_Percent Is Nothing OrElse Not ChkNull_Discount_Percent = "" Then
                            DrHeader("Discount_Percent") = ChkNull_Discount_Percent
                        Else
                            DrHeader("Discount_Percent") = Discount_Percent 'For Default value
                        End If
                    Else
                        DrHeader("Discount_Percent") = Discount_Percent 'For Default value
                    End If

                    '================= Discount_Amt Check Null =================
                    _Err_Msg = "Add Discount_Amt"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Discount_Percent") Then
                        ChkNull_Discount_Amt = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Discount_Amt")).ToString
                        If Not ChkNull_Discount_Amt Is Nothing OrElse Not ChkNull_Discount_Amt = "" Then
                            DrHeader("Discount_Amt") = ChkNull_Discount_Amt
                        Else
                            DrHeader("Discount_Amt") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Discount_Amt") = 0 'For Default value
                    End If
                    '================= Deposit_Amt Check Null =================
                    _Err_Msg = "Add Deposit_Amt"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Deposit_Amt") Then
                        ChkNull_Deposit_Amt = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Deposit_Amt")).ToString
                        If Not ChkNull_Deposit_Amt Is Nothing OrElse Not ChkNull_Deposit_Amt = "" Then
                            DrHeader("Deposit_Amt") = ChkNull_Deposit_Amt
                        Else
                            DrHeader("Deposit_Amt") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Deposit_Amt") = 0 'For Default value
                    End If
                    '================= Total_Amt Check Null =================
                    _Err_Msg = "Add Total_Amt"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Total_Amt") Then
                        ChkNull_Total_Amt = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Total_Amt")).ToString
                        If Not ChkNull_Total_Amt Is Nothing OrElse Not ChkNull_Total_Amt = "" Then
                            DrHeader("Total_Amt") = ChkNull_Total_Amt
                        Else
                            DrHeader("Total_Amt") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Total_Amt") = 0 'For Default value
                    End If

                    '================= VAT_Percent Check Null =================
                    _Err_Msg = "Add VAT_Percent"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "VAT_Percent") Then
                        ChkNull_VAT_Percent = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "VAT_Percent")).ToString
                        If Not ChkNull_VAT_Percent Is Nothing OrElse Not ChkNull_VAT_Percent = "" Then
                            DrHeader("VAT_Percent") = ChkNull_Discount_Amt
                        Else
                            DrHeader("VAT_Percent") = VAT_Percent 'For Default value
                        End If
                    Else
                        DrHeader("VAT_Percent") = VAT_Percent 'For Default value
                    End If
                    '================= VAT Check Null =================
                    _Err_Msg = "Add VAT"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "VAT") Then
                        ChkNull_VAT = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "VAT")).ToString
                        If Not ChkNull_VAT Is Nothing OrElse Not ChkNull_VAT = "" Then
                            DrHeader("VAT") = ChkNull_VAT
                        Else
                            DrHeader("VAT") = 0 'For Default value
                        End If
                    Else
                        DrHeader("VAT") = 0 'For Default value
                    End If
                    '================= Net_Amt Check Null =================
                    _Err_Msg = "Add Net_Amt"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Net_Amt") Then
                        ChkNull_Net_Amt = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Net_Amt")).ToString
                        If Not ChkNull_Net_Amt Is Nothing OrElse Not ChkNull_Net_Amt = "" Then
                            DrHeader("Net_Amt") = ChkNull_Net_Amt
                        Else
                            DrHeader("Net_Amt") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Net_Amt") = 0 'For Default value
                    End If
                    '================= Reserve_index Check Null =================
                    _Err_Msg = "Add Reserve_index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Reserve_index") Then
                        ChkNull_Reserve_index = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Reserve_index")).ToString
                        If Not ChkNull_Reserve_index Is Nothing OrElse Not ChkNull_Reserve_index = "" Then
                            DrHeader("Reserve_index") = ChkNull_Reserve_index
                        Else
                            DrHeader("Reserve_index") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Reserve_index") = "" 'For Default value
                    End If

                    '================= Supplier_Index // Compare // Supplier_Id =================
                    _Err_Msg = "Add Supplier_Index"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Supplier_Id") Then
                        ChkNull_Supplier_Index = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Supplier_Id")).ToString
                        If Not ChkNull_Supplier_Index Is Nothing OrElse Not ChkNull_Supplier_Index = "" Then
                            DrHeader("Supplier_Index") = ChkNull_Supplier_Index
                        Else
                            DrHeader("Supplier_Index") = Supplier_Index 'For Default value
                        End If
                    Else
                        DrHeader("Supplier_Index") = Supplier_Index 'For Default value
                    End If

                    '================= Str1 Check Null =================
                    _Err_Msg = "Add Str1"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str1") Then
                        ChkNull_Str1 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str1")).ToString
                        If Not ChkNull_Str1 Is Nothing OrElse Not ChkNull_Str1 = "" Then
                            DrHeader("Str1") = ChkNull_Str1
                        Else
                            DrHeader("Str1") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str1") = "" 'For Default value
                    End If
                    '================= Str2 Check Null =================
                    _Err_Msg = "Add Str2"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str2") Then
                        ChkNull_Str2 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str2")).ToString
                        If Not ChkNull_Str2 Is Nothing OrElse Not ChkNull_Str2 = "" Then
                            DrHeader("Str2") = ChkNull_Str2
                        Else
                            DrHeader("Str2") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str2") = "" 'For Default value
                    End If
                    '================= Str3 Check Null =================
                    _Err_Msg = "Add Str3"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str3") Then
                        ChkNull_Str3 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str3")).ToString
                        If Not ChkNull_Str3 Is Nothing OrElse Not ChkNull_Str3 = "" Then
                            DrHeader("Str3") = ChkNull_Str3
                        Else
                            DrHeader("Str3") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str3") = "" 'For Default value
                    End If
                    '================= Str4 Check Null =================
                    _Err_Msg = "Add Str4"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str4") Then
                        ChkNull_Str4 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str4")).ToString
                        If Not ChkNull_Str4 Is Nothing OrElse Not ChkNull_Str4 = "" Then
                            DrHeader("Str4") = ChkNull_Str4
                        Else
                            DrHeader("Str4") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str4") = "" 'For Default value
                    End If
                    '================= Str5 Check Null =================
                    _Err_Msg = "Add Str5"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str5") Then
                        ChkNull_Str5 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str5")).ToString
                        If Not ChkNull_Str5 Is Nothing OrElse Not ChkNull_Str5 = "" Then
                            DrHeader("Str5") = ChkNull_Str5
                        Else
                            DrHeader("Str5") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str5") = "" 'For Default value
                    End If
                    '================= Str6 Check Null =================
                    _Err_Msg = "Add Str6"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str6") Then
                        ChkNull_Str6 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str6")).ToString
                        If Not ChkNull_Str6 Is Nothing OrElse Not ChkNull_Str6 = "" Then
                            DrHeader("Str6") = ChkNull_Str6
                        Else
                            DrHeader("Str6") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str6") = "" 'For Default value
                    End If

                    '================= Str7 Check Null =================
                    _Err_Msg = "Add Str7"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str7") Then
                        ChkNull_Str7 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str7")).ToString
                        If Not ChkNull_Str7 Is Nothing OrElse Not ChkNull_Str7 = "" Then
                            DrHeader("Str7") = ChkNull_Str7
                        Else
                            DrHeader("Str7") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str7") = "" 'For Default value
                    End If
                    '================= Str8 Check Null =================
                    _Err_Msg = "Add Str8"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str8") Then
                        ChkNull_Str8 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str8")).ToString
                        If Not ChkNull_Str8 Is Nothing OrElse Not ChkNull_Str8 = "" Then
                            DrHeader("Str8") = ChkNull_Str8
                        Else
                            DrHeader("Str8") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str8") = "" 'For Default value
                    End If
                    '================= Str9 Check Null =================
                    _Err_Msg = "Add Str9"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str9") Then
                        ChkNull_Str9 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str9")).ToString
                        If Not ChkNull_Str9 Is Nothing OrElse Not ChkNull_Str9 = "" Then
                            DrHeader("Str9") = ChkNull_Str9
                        Else
                            DrHeader("Str9") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str9") = "" 'For Default value
                    End If
                    '================= Str10 Check Null =================
                    _Err_Msg = "Add Str10"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str10") Then
                        ChkNull_Str10 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str10")).ToString
                        If Not ChkNull_Str10 Is Nothing OrElse Not ChkNull_Str10 = "" Then
                            DrHeader("Str10") = ChkNull_Str10
                        Else
                            DrHeader("Str10") = "" 'For Default value
                        End If
                    Else
                        DrHeader("Str10") = "" 'For Default value
                    End If
                    '================= Flo1 Check Null =================
                    _Err_Msg = "Add Flo1"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo1") Then
                        ChkNull_Flo1 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo1")).ToString
                        If Not ChkNull_Flo1 Is Nothing OrElse Not ChkNull_Flo1 = "" Then
                            DrHeader("Flo1") = ChkNull_Flo1
                        Else
                            DrHeader("Flo1") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Flo1") = 0 'For Default value
                    End If
                    '================= Flo2 Check Null =================
                    _Err_Msg = "Add Flo2"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo2") Then
                        ChkNull_Flo2 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo2")).ToString
                        If Not ChkNull_Flo2 Is Nothing OrElse Not ChkNull_Flo2 = "" Then
                            DrHeader("Flo2") = ChkNull_Flo2
                        Else
                            DrHeader("Flo2") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Flo2") = 0 'For Default value
                    End If
                    '================= Flo3 Check Null =================
                    _Err_Msg = "Add Flo3"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo3") Then
                        ChkNull_Flo3 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo3")).ToString
                        If Not ChkNull_Flo3 Is Nothing OrElse Not ChkNull_Flo3 = "" Then
                            DrHeader("Flo3") = ChkNull_Flo3
                        Else
                            DrHeader("Flo3") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Flo3") = 0 'For Default value
                    End If

                    '================= Flo4 Check Null =================
                    _Err_Msg = "Add Flo4"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo4") Then
                        ChkNull_Flo4 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo4")).ToString
                        If Not ChkNull_Flo4 Is Nothing OrElse Not ChkNull_Flo4 = "" Then
                            DrHeader("Flo4") = ChkNull_Flo4
                        Else
                            DrHeader("Flo4") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Flo4") = 0 'For Default value
                    End If
                    '================= Flo5 Check Null =================
                    _Err_Msg = "Add Flo5"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo5") Then
                        ChkNull_Flo5 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo5")).ToString
                        If Not ChkNull_Flo5 Is Nothing OrElse Not ChkNull_Flo5 = "" Then
                            DrHeader("Flo5") = ChkNull_Flo5
                        Else
                            DrHeader("Flo5") = 0 'For Default value
                        End If
                    Else
                        DrHeader("Flo5") = 0 'For Default value
                    End If

                    '_Err_Msg = "Add Total_Qty_Pack"
                    'If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Total_Qty_Pack") Then
                    '    ChkNull_Total_Qty_Pack = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Total_Qty_Pack")).ToString
                    '    If Not ChkNull_Total_Qty_Pack Is Nothing OrElse Not ChkNull_Total_Qty_Pack = "" Then
                    '        DrHeader("Total_Qty_Pack") = ChkNull_Total_Qty_Pack
                    '    Else
                    '        DrHeader("Total_Qty_Pack") = 0 'For Default value
                    '    End If
                    'Else
                    '    DrHeader("Total_Qty_Pack") = 0 'For Default value
                    'End If


                    _Err_Msg = "Add PO_No"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "PO_No") Then
                        ChkNull_PO_No = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "PO_No")).ToString
                        If Not ChkNull_PO_No Is Nothing OrElse Not ChkNull_PO_No = "" Then
                            DrHeader("PO_No") = ChkNull_PO_No
                        Else
                            DrHeader("PO_No") = "" 'For Default value
                        End If
                    Else
                        DrHeader("PO_No") = "" 'For Default value
                    End If
                    _Err_Msg = "Add PO_Date"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "PO_Date") Then
                        ChkNull_PO_Date = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "PO_Date")).ToString
                        If Not ChkNull_PO_Date Is Nothing OrElse Not ChkNull_PO_Date = "" Then
                            DrHeader("PO_Date") = ChkNull_PO_No
                        Else
                            DrHeader("PO_Date") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("PO_Date") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_ExpectedDocPickup"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_ExpectedDocPickup") Then
                        ChkNull_Time_ExpectedDocPickup = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_ExpectedDocPickup")).ToString
                        If Not ChkNull_Time_ExpectedDocPickup Is Nothing OrElse Not ChkNull_Time_ExpectedDocPickup = "" Then
                            DrHeader("Time_ExpectedDocPickup") = ChkNull_Time_ExpectedDocPickup
                        Else
                            DrHeader("Time_ExpectedDocPickup") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_ExpectedDocPickup") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_DocPickup"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocPickup") Then
                        ChkNull_Time_DocPickup = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DocPickup")).ToString
                        If Not ChkNull_Time_DocPickup Is Nothing OrElse Not ChkNull_Time_DocPickup = "" Then
                            DrHeader("Time_DocPickup") = ChkNull_Time_DocPickup
                        Else
                            DrHeader("Time_DocPickup") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DocPickup") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_DocTripConfirmed"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocTripConfirmed") Then
                        ChkNull_Time_DocTripConfirmed = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DocTripConfirmed")).ToString
                        If Not ChkNull_Time_DocTripConfirmed Is Nothing OrElse Not ChkNull_Time_DocTripConfirmed = "" Then
                            DrHeader("Time_DocTripConfirmed") = ChkNull_Time_DocTripConfirmed
                        Else
                            DrHeader("Time_DocTripConfirmed") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DocTripConfirmed") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_DeliveryToDestination"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DeliveryToDestination") Then
                        ChkNull_Time_DeliveryToDestination = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DeliveryToDestination")).ToString
                        If Not ChkNull_Time_DeliveryToDestination Is Nothing OrElse Not ChkNull_Time_DeliveryToDestination = "" Then
                            DrHeader("Time_DeliveryToDestination") = ChkNull_Time_DeliveryToDestination
                        Else
                            DrHeader("Time_DeliveryToDestination") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DeliveryToDestination") = DateServer 'For Default value
                    End If

                    _Err_Msg = "Add Time_DocReturnedToDC"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocReturnedToDC") Then
                        ChkNull_Time_DocReturnedToDC = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DocReturnedToDC")).ToString
                        If Not ChkNull_Time_DocReturnedToDC Is Nothing OrElse Not ChkNull_Time_DocReturnedToDC = "" Then
                            DrHeader("Time_DocReturnedToDC") = ChkNull_Time_DocReturnedToDC
                        Else
                            DrHeader("Time_DocReturnedToDC") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DocReturnedToDC") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_DocReturnedToSource"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocReturnedToSource") Then
                        ChkNull_Time_DocReturnedToSource = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DocReturnedToSource")).ToString
                        If Not ChkNull_Time_DocReturnedToSource Is Nothing OrElse Not ChkNull_Time_DocReturnedToSource = "" Then
                            DrHeader("Time_DocReturnedToSource") = ChkNull_Time_DocReturnedToSource
                        Else
                            DrHeader("Time_DocReturnedToSource") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DocReturnedToSource") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_DocReturnedToOwner"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocReturnedToOwner") Then
                        ChkNull_Time_DocReturnedToOwner = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DocReturnedToOwner")).ToString
                        If Not ChkNull_Time_DocReturnedToOwner Is Nothing OrElse Not ChkNull_Time_DocReturnedToOwner = "" Then
                            DrHeader("Time_DocReturnedToOwner") = ChkNull_Time_DocReturnedToOwner
                        Else
                            DrHeader("Time_DocReturnedToOwner") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DocReturnedToOwner") = DateServer 'For Default value
                    End If
                    _Err_Msg = "Add Time_DocConfirmedByOwner"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocConfirmedByOwner") Then
                        ChkNull_Time_DocConfirmedByOwner = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DocConfirmedByOwner")).ToString
                        If Not ChkNull_Time_DocConfirmedByOwner Is Nothing OrElse Not ChkNull_Time_DocConfirmedByOwner = "" Then
                            DrHeader("Time_DocConfirmedByOwner") = ChkNull_Time_DocConfirmedByOwner
                        Else
                            DrHeader("Time_DocConfirmedByOwner") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DocConfirmedByOwner") = DateServer 'For Default value
                    End If

                    _Err_Msg = "Add Time_DestinationOutGate"
                    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Time_DocConfirmedByOwner") Then
                        ChkNull_Time_DestinationOutGate = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Time_DestinationOutGate")).ToString
                        If Not ChkNull_Time_DestinationOutGate Is Nothing OrElse Not ChkNull_Time_DestinationOutGate = "" Then
                            DrHeader("Time_DestinationOutGate") = ChkNull_Time_DestinationOutGate
                        Else
                            DrHeader("Time_DestinationOutGate") = DateServer 'For Default value
                        End If
                    Else
                        DrHeader("Time_DestinationOutGate") = DateServer 'For Default value
                    End If


                Else
                    ' Have PO No. Insert Datail 
                End If

                '================= PurchaseOrder_No ===========================


                '****************************************************************************************************
                '------------------------------ Check  & Add Data in dataTable Detail ------------------------------'
                '****************************************************************************************************
                strField_Type = "Detail"

                '================= PurchaseOrderItem_Index Check Null =================


                '================= Item_Seq Check Null =================
                Me._Err_Checkpoint = "STEP 2 Add Detail"
                _Err_Msg = "Add Item_Seq"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Item_Seq") Then
                    ChkNull_Item_Seq = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Item_Seq")).ToString
                    If Not ChkNull_Item_Seq Is Nothing OrElse Not ChkNull_Item_Seq = "" Then
                        DrDetail("Item_Seq") = ChkNull_Item_Seq
                    Else
                        DrDetail("Item_Seq") = 0 'For Default value
                    End If
                Else
                    DrDetail("Item_Seq") = 0 'For Default value
                End If

                '================= Sku_Index // Compare // Sku_Id =================
                Me._Err_Checkpoint = "STEP 3 Add SKU"
                _Err_Msg = "Add Sku_Index"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Sku_Id") Then
                    Dim skuId_where As String = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Sku_Id"))
                    If skuId_where Is Nothing OrElse skuId_where = "" Then
                        Continue For
                    End If

                    'DtSku = Me.CheckPackage(" Sku_Index ='" & DtSku.Rows(0).Item("Sku_Index").ToString & "' AND Package_Id ='" & Package_Id_where & "' ")
                    DtSku = objkascoCLB.check_returnDataTable("ms_SKU INNER JOIN MS_SKURATIO ON ms_SKU.Sku_Index = MS_SKURATIO.Sku_Index INNER JOIN  MS_Package ON  MS_SKURATIO.Package_Index = MS_Package.Package_Index ", "*", " MS_Package.Barcode ='" & skuId_where & "' AND ms_SKU.status_id <> -1 AND ms_Package.status_id <> -1  AND ms_SKURATIO.status_id <> -1")

                    ' DtSku = chkDupe_returnDataTable("ms_SKU INNER JOIN MS_SKURATIO ON ms_SKU.Sku_Index = MS_SKURATIO.Sku_Index INNER JOIN  MS_Package ON  MS_SKURATIO.Package_Index = MS_Package.Package_Index ", "*", " MS_Package.Barcode ='" & _DtTemp.Rows(i)(get_TabIndex(_dtConfig, strField_Type, "Sku_Id", iProcess, strPrefix)) & "' AND ms_SKU.status_id <> -1 AND ms_Package.status_id <> -1  AND ms_SKURATIO.status_id <> -1  ")

                    If DtSku.Rows.Count > 0 Then
                        DrDetail("Sku_Index") = DtSku.Rows(0).Item("Sku_Index").ToString
                        DrDetail("Package_Index") = DtSku.Rows(0).Item("Package_Index").ToString
                    Else
                        Continue For
                    End If

                Else
                    ' Continue For
                End If

                '================= Package_Index // Compare // Package_Id =================
                '1. ChkUse
                '--- chkPackage 
                '----- ถ้าเจอ ใส่ค่า
                Dim iRatio As Integer = 1
                _Err_Msg = "Add Package_Index"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Package_Id") Then
                    Dim Package_Id_where As String = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Package_Id"))
                    DtSkuPKRatio = Me.CheckPackage(" Sku_Index ='" & DtSku.Rows(0).Item("Sku_Index").ToString & "' AND Package_Id ='" & Package_Id_where & "' ")
                    'DtSkuPKRatio = objkascoCLB.check_returnDataTable("View_GetPackage_Import", "*", " Sku_Index ='" & DtSku.Rows(0).Item("Sku_Index").ToString & "' AND Package_Id ='" & Package_Id_where & "' ")
                    If DtSkuPKRatio.Rows.Count > 0 Then
                        iRatio = DtSkuPKRatio.Rows(0).Item("Ratio")
                        DrDetail("Package_Index") = DtSkuPKRatio.Rows(0).Item("Package_Index").ToString
                    Else
                        Continue For
                    End If
                Else
                    'Default value
                End If

                '_Err_Msg = "Add Package_Index"
                'If DrDetail("Package_Index") = "" Then
                '    If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Package_Id") Then
                '        ChkDupe_Package_Index = objkascoCLB.check_Dupe("ms_Package", "Package_Index", " Description ='" & _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Package_Id")) & "' AND status_id <> -1")
                '        If Not ChkDupe_Package_Index Is Nothing OrElse Not ChkDupe_Package_Index = "" Then
                '            DrDetail("Package_Index") = ChkDupe_Package_Index
                '        Else
                '            ' get ms_sku.Package_Index where sku_Index

                '            DrDetail("Package_Index") = "0010000000001" '_DtTemp.Rows(i)(4).ToString
                '        End If
                '    Else
                '        'Continue For
                '        'Package_Name
                '    End If
                'End If


                '================= Qty Check Null =================
                _Err_Msg = "Add Qty"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Qty") Then
                    ChkNull_Qty = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Qty")).ToString
                    If Not ChkNull_Qty Is Nothing OrElse Not ChkNull_Qty = "" Then
                        DrDetail("Qty") = Math.Abs(CInt(ChkNull_Qty))
                        DrDetail("Total_Qty") = Math.Abs(CInt(ChkNull_Qty)) * iRatio
                        DrDetail("Ratio") = iRatio
                    Else
                        Continue For
                        'DrDetail("Qty") = 0 'For Default value
                    End If
                Else
                    Continue For
                    'DrDetail("Qty") = 0 'For Default value
                End If

                '***getratio****

                '================= Total_Qty Check Null =================
                'Qty * ratio
                '_Err_Msg = "Add Total_Qty"
                'If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Total_Qty") Then
                '    ChkNull_Total_Qty = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Total_Qty")).ToString
                '    If Not ChkNull_Total_Qty Is Nothing OrElse Not ChkNull_Total_Qty = "" Then
                '        DrDetail("Total_Qty") = ChkNull_Total_Qty
                '    Else
                '        DrDetail("Total_Qty") = Math.Abs(CInt(ChkNull_Qty)) * iRatio 'For Default value
                '    End If
                'Else
                '    DrDetail("Total_Qty") = 0 'For Default value
                'End If

                '================= Ratio Check Null =================
                '_Err_Msg = "Add Ratio"
                'If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Ratio") Then
                '    ChkRatio = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Ratio")).ToString
                '    If Not ChkRatio Is Nothing OrElse Not ChkRatio = "" Then
                '        DrDetail("Ratio") = ChkRatio
                '    Else
                '        DrDetail("Ratio") = 0 'For Default value
                '    End If
                'Else
                '    DrDetail("Ratio") = 0 'For Default value
                'End If

                '================= Weight Check Null =================
                _Err_Msg = "Add Weight"
                Dim UnitWeight_Index As String = ""
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Weight") Then
                    ChkNull_Weight = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Weight")).ToString
                    If Not ChkNull_Weight Is Nothing OrElse Not ChkNull_Weight = "" Then
                        DrDetail("Weight") = ChkNull_Weight
                    Else
                        UnitWeight_Index = DtSku.Rows(0).Item("UnitWeight_Index")
                        If Not UnitWeight_Index Is Nothing OrElse Not UnitWeight_Index = "" Then
                            DrDetail("Weight") = FormatNumber((CDbl(UnitWeight_Index) * Math.Abs(CInt(ChkNull_Qty))), 4, , TriState.True)  'For Default value
                        Else
                            DrDetail("Weight") = 0
                        End If
                    End If 'UnitWeight_Index
                Else
                    UnitWeight_Index = DtSku.Rows(0).Item("UnitWeight_Index")
                    If Not UnitWeight_Index Is Nothing OrElse Not UnitWeight_Index = "" Then
                        DrDetail("Weight") = FormatNumber((CDbl(UnitWeight_Index) * Math.Abs(CInt(ChkNull_Qty))), 4, , TriState.True)  'For Default value
                    Else
                        DrDetail("Weight") = 0
                    End If
                End If
                '================= Volume Check Null =================
                _Err_Msg = "Add Volume"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Volume") Then
                    ChkNull_Volume = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Volume")).ToString
                    If Not ChkNull_Volume Is Nothing OrElse Not ChkNull_Volume = "" Then
                        DrDetail("Volume") = ChkNull_Volume
                    Else
                        'Get  value
                        If IsNumeric(DtSku.Rows(0).Item("Unit_Volume")) Then
                            DrDetail("Volume") = FormatNumber(DtSku.Rows(0).Item("Unit_Volume") * DrDetail("Total_Qty"), 4)
                        End If
                    End If
                Else
                    If IsNumeric(DtSku.Rows(0).Item("Unit_Volume")) Then
                        DrDetail("Volume") = FormatNumber(DtSku.Rows(0).Item("Unit_Volume") * DrDetail("Total_Qty"), 4)
                    End If
                End If

                '================= Serial_No Check Null =================
                _Err_Msg = "Add Serial_No"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Serial_No") Then
                    ChkNull_Serial_No = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Serial_No")).ToString
                    If Not ChkNull_Serial_No Is Nothing OrElse Not ChkNull_Serial_No = "" Then
                        DrDetail("Serial_No") = ChkNull_Volume
                    Else
                        DrDetail("Serial_No") = 0 'For Default value
                    End If
                Else
                    DrDetail("Serial_No") = 0 'For Default value
                End If
                '================= Total_Received_Qty Check Null =================
                _Err_Msg = "Add Total_Qty_Withdraw"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Total_Qty_Withdraw") Then
                    ChkNull_Total_Qty_Withdraw = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Total_Qty_Withdraw")).ToString
                    If Not ChkNull_Total_Qty_Withdraw Is Nothing OrElse Not ChkNull_Total_Qty_Withdraw = "" Then
                        DrDetail("Total_Qty_Withdraw") = ChkNull_Total_Qty_Withdraw
                    Else
                        DrDetail("Total_Qty_Withdraw") = 0 'For Default value
                    End If
                Else
                    DrDetail("Total_Qty_Withdraw") = 0 'For Default value
                End If
                '================= Received_Qty Check Null =================
                _Err_Msg = "Add Qty_Withdraw"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Qty_Withdraw") Then
                    ChkNull_Qty_Withdraw = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Qty_Withdraw")).ToString
                    If Not ChkNull_Qty_Withdraw Is Nothing OrElse Not ChkNull_Qty_Withdraw = "" Then
                        DrDetail("Qty_Withdraw") = ChkNull_Qty_Withdraw
                    Else
                        DrDetail("Qty_Withdraw") = 0 'For Default value
                    End If
                Else
                    DrDetail("Qty_Withdraw") = 0 'For Default value
                End If
                '================= Received_Weight Check Null =================
                _Err_Msg = "Add Weight_Withdraw"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Weight_Withdraw") Then
                    ChkNull_Weight_Withdraw = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Weight_Withdraw")).ToString
                    If Not ChkNull_Weight_Withdraw Is Nothing OrElse Not ChkNull_Weight_Withdraw = "" Then
                        DrDetail("Weight_Withdraw") = ChkNull_Weight_Withdraw
                    Else
                        DrDetail("Weight_Withdraw") = 0 'For Default value
                    End If
                Else
                    DrDetail("Weight_Withdraw") = 0 'For Default value
                End If
                '================= Received_Volume Check Null =================
                _Err_Msg = "Add Volume_Withdraw"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Volume_Withdraw") Then
                    ChkNull_Volume_Withdraw = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Volume_Withdraw")).ToString
                    If Not ChkNull_Volume_Withdraw Is Nothing OrElse Not ChkNull_Volume_Withdraw = "" Then
                        DrDetail("Volume_Withdraw") = ChkNull_Volume_Withdraw
                    Else
                        DrDetail("Volume_Withdraw") = 0 'For Default value
                    End If
                Else
                    DrDetail("Volume_Withdraw") = 0 'For Default value
                End If

                ' ================= Last_Withdraw_Date Check Null =================
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Last_Withdraw_Date") Then
                    ChkNull_Last_Withdraw_Date = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Last_Withdraw_Date")).ToString
                    If Not ChkNull_Last_Withdraw_Date Is Nothing OrElse Not ChkNull_Last_Withdraw_Date = "" Then
                        DrDetail("Last_Withdraw_Date") = ChkNull_Last_Withdraw_Date
                    Else
                        DrDetail("Last_Withdraw_Date") = DateServer 'For Default value
                    End If
                Else
                    DrDetail("Last_Withdraw_Date") = DateServer 'For Default value
                End If
                '================= UnitPrice Check Null =================

                ' ================= Last_Received_Date Check Null =================
                'If chk_isuse(_dtConfig, strField_Type, "Last_Received_Date", iProcess, strPrefix) = 1 Then
                '    ChkNull_Last_Received_Date = _DtTemp.Rows(i)(get_TabIndex(_dtConfig, strField_Type, "Last_Received_Date", iProcess, strPrefix)).ToString
                '    If Not ChkNull_Last_Received_Date Is Nothing OrElse Not ChkNull_Last_Received_Date = "" Then
                '        DrDetail("Last_Received_Date") = ChkNull_Last_Received_Date
                '    Else
                '        DrDetail("Last_Received_Date") = DateServer 'For Default value
                '    End If
                'Else
                '    DrDetail("Last_Received_Date") = DateServer 'For Default value
                'End If
                '================= UnitPrice Check Null =================



                _Err_Msg = "Add UnitPrice"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "UnitPrice") Then
                    ChkNull_UnitPrice = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "UnitPrice")).ToString
                    If Not ChkNull_UnitPrice Is Nothing OrElse Not ChkNull_UnitPrice = "" Then
                        DrDetail("UnitPrice") = ChkNull_UnitPrice
                    Else
                        DrDetail("UnitPrice") = 0 'For Default value
                    End If
                Else
                    DrDetail("UnitPrice") = 0 'For Default value
                End If
                '================= Amount Check Null =================
                _Err_Msg = "Add Amount"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Amount") Then
                    ChkNull_Amount_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Amount")).ToString
                    If Not ChkNull_Amount_Detail Is Nothing OrElse Not ChkNull_Amount_Detail = "" Then
                        DrDetail("Amount") = ChkNull_Amount_Detail
                    Else
                        DrDetail("Amount") = 0 'For Default value
                    End If
                Else
                    DrDetail("Amount") = 0 'For Default value
                End If
                '================= Discount_Amt Check Null =================
                _Err_Msg = "Add Discount_Amt"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Discount_Amt") Then
                    ChkNull_Discount_Amt_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Discount_Amt")).ToString
                    If Not ChkNull_Discount_Amt_Detail Is Nothing OrElse Not ChkNull_Discount_Amt_Detail = "" Then
                        DrDetail("Discount_Amt") = ChkNull_Discount_Amt_Detail
                    Else
                        DrDetail("Discount_Amt") = 0 'For Default value
                    End If
                Else
                    DrDetail("Discount_Amt") = 0 'For Default value
                End If
                '================= Total_Amt Check Null =================
                _Err_Msg = "Add Total_Amt"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Total_Amt") Then
                    ChkNull_Total_Amt_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Total_Amt")).ToString
                    If Not ChkNull_Total_Amt_Detail Is Nothing OrElse Not ChkNull_Total_Amt_Detail = "" Then
                        DrDetail("Total_Amt") = ChkNull_Total_Amt_Detail
                    Else
                        DrDetail("Total_Amt") = 0 'For Default value
                    End If
                Else
                    DrDetail("Total_Amt") = 0 'For Default value
                End If
                '================= Remark Check Null =================
                _Err_Msg = "Add Remark"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Remark") Then
                    ChkNull_Remark_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Remark")).ToString
                    If Not ChkNull_Remark_Detail Is Nothing OrElse Not ChkNull_Remark_Detail = "" Then
                        DrDetail("Remark") = ChkNull_Remark_Detail
                    Else
                        DrDetail("Remark") = "" 'For Default value
                    End If
                Else
                    DrDetail("Remark") = "" 'For Default value
                End If
                '================= Reason Check Null =================
                _Err_Msg = "Add Reason"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Reason") Then
                    ChkNull_Reason = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Reason")).ToString
                    If Not ChkNull_Reason Is Nothing OrElse Not ChkNull_Reason = "" Then
                        DrDetail("Reason") = ChkNull_Reason
                    Else
                        DrDetail("Reason") = "" 'For Default value
                    End If
                Else
                    DrDetail("Reason") = "" 'For Default value
                End If
                '================= Ref_No1 Check Null =================
                _Err_Msg = "Add Ref_No1"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Ref_No1") Then
                    ChkNull_Ref_No1 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Ref_No1")).ToString
                    If Not ChkNull_Ref_No1 Is Nothing OrElse Not ChkNull_Ref_No1 = "" Then
                        DrDetail("Ref_No1") = ChkNull_Ref_No1
                    Else
                        DrDetail("Ref_No1") = 0 'For Default value
                    End If
                Else
                    DrDetail("Ref_No1") = 0 'For Default value
                End If
                '================= Ref_No2 Check Null =================
                _Err_Msg = "Add Ref_No2"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Ref_No2") Then
                    ChkNull_Ref_No2 = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Ref_No2")).ToString
                    If Not ChkNull_Ref_No2 Is Nothing OrElse Not ChkNull_Ref_No2 = "" Then
                        DrDetail("Ref_No2") = ChkNull_Ref_No2
                    Else
                        DrDetail("Ref_No2") = "" 'For Default value
                    End If
                Else
                    DrDetail("Ref_No2") = "" 'For Default value
                End If

                '================= Str1 Check Null =================
                _Err_Msg = "Add Str1"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str1") Then
                    ChkNull_Str1_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str1")).ToString
                    If Not ChkNull_Str1_Detail Is Nothing OrElse Not ChkNull_Str1_Detail = "" Then
                        DrDetail("Str1") = ChkNull_Str1_Detail
                    Else
                        DrDetail("Str1") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str1") = "" 'For Default value
                End If
                '================= Str2 Check Null =================
                _Err_Msg = "Add Str2"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str2") Then
                    ChkNull_Str2_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str2")).ToString
                    If Not ChkNull_Str2_Detail Is Nothing OrElse Not ChkNull_Str2_Detail = "" Then
                        DrDetail("Str2") = ChkNull_Str2_Detail
                    Else
                        DrDetail("Str2") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str2") = "" 'For Default value
                End If
                '================= Str3 Check Null =================
                _Err_Msg = "Add Str3"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str3") Then
                    ChkNull_Str3_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str3")).ToString
                    If Not ChkNull_Str3_Detail Is Nothing OrElse Not ChkNull_Str3_Detail = "" Then
                        DrDetail("Str3") = ChkNull_Str3_Detail
                    Else
                        DrDetail("Str3") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str3") = "" 'For Default value
                End If
                '================= Str4 Check Null =================
                _Err_Msg = "Add Str4"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str4") Then
                    ChkNull_Str4_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str4")).ToString
                    If Not ChkNull_Str4_Detail Is Nothing OrElse Not ChkNull_Str4_Detail = "" Then
                        DrDetail("Str4") = ChkNull_Str4_Detail
                    Else
                        DrDetail("Str4") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str4") = "" 'For Default value
                End If
                '================= Str5 Check Null =================
                _Err_Msg = "Add Str5"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str5") Then
                    ChkNull_Str5_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str5")).ToString
                    If Not ChkNull_Str5_Detail Is Nothing OrElse Not ChkNull_Str5_Detail = "" Then
                        DrDetail("Str5") = ChkNull_Str5_Detail
                    Else
                        DrDetail("Str5") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str5") = "" 'For Default value
                End If
                '================= Str6 Check Null =================
                _Err_Msg = "Add Str6"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str6") Then
                    ChkNull_Str6_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str6")).ToString
                    If Not ChkNull_Str6_Detail Is Nothing OrElse Not ChkNull_Str6_Detail = "" Then
                        DrDetail("Str6") = ChkNull_Str6_Detail
                    Else
                        DrDetail("Str6") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str6") = "" 'For Default value
                End If

                '================= Str7 Check Null =================
                _Err_Msg = "Add Str7"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str7") Then
                    ChkNull_Str7_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str7")).ToString
                    If Not ChkNull_Str7_Detail Is Nothing OrElse Not ChkNull_Str7_Detail = "" Then
                        DrDetail("Str7") = ChkNull_Str7_Detail
                    Else
                        DrDetail("Str7") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str7") = "" 'For Default value
                End If
                '================= Str8 Check Null =================
                _Err_Msg = "Add Str8"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str8") Then
                    ChkNull_Str8_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str8")).ToString
                    If Not ChkNull_Str8_Detail Is Nothing OrElse Not ChkNull_Str8_Detail = "" Then
                        DrDetail("Str8") = ChkNull_Str8_Detail
                    Else
                        DrDetail("Str8") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str8") = "" 'For Default value
                End If
                '================= Str9 Check Null =================
                _Err_Msg = "Add Str9"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str9") Then
                    ChkNull_Str9_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str9")).ToString
                    If Not ChkNull_Str9_Detail Is Nothing OrElse Not ChkNull_Str9_Detail = "" Then
                        DrDetail("Str9") = ChkNull_Str8_Detail
                    Else
                        DrDetail("Str9") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str9") = "" 'For Default value
                End If
                '================= Str10 Check Null =================
                _Err_Msg = "Add Str10"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Str10") Then
                    ChkNull_Str10_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Str10")).ToString
                    If Not ChkNull_Str10_Detail Is Nothing OrElse Not ChkNull_Str10_Detail = "" Then
                        DrDetail("Str10") = ChkNull_Str10_Detail
                    Else
                        DrDetail("Str10") = "" 'For Default value
                    End If
                Else
                    DrDetail("Str10") = "" 'For Default value
                End If
                '================= Flo1 Check Null =================
                _Err_Msg = "Add Flo1"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo1") Then
                    ChkNull_Flo1_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo1")).ToString
                    If Not ChkNull_Flo1_Detail Is Nothing OrElse Not ChkNull_Flo1_Detail = "" Then
                        DrDetail("Flo1") = ChkNull_Flo1_Detail
                    Else
                        DrDetail("Flo1") = 0 'For Default value
                    End If
                Else
                    DrDetail("Flo1") = 0 'For Default value
                End If
                '================= Flo2 Check Null =================
                _Err_Msg = "Add Flo2"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo2") Then
                    ChkNull_Flo2_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo2")).ToString
                    If Not ChkNull_Flo2_Detail Is Nothing OrElse Not ChkNull_Flo2_Detail = "" Then
                        DrDetail("Flo2") = ChkNull_Flo2_Detail
                    Else
                        DrDetail("Flo2") = 0 'For Default value
                    End If
                Else
                    DrDetail("Flo2") = 0 'For Default value
                End If
                '================= Flo3 Check Null =================
                _Err_Msg = "Add Flo3"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo3") Then
                    ChkNull_Flo3_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo3")).ToString
                    If Not ChkNull_Flo3_Detail Is Nothing OrElse Not ChkNull_Flo3_Detail = "" Then
                        DrDetail("Flo3") = ChkNull_Flo3_Detail
                    Else
                        DrDetail("Flo3") = 0 'For Default value
                    End If
                Else
                    DrDetail("Flo3") = 0 'For Default value
                End If

                '================= Flo4 Check Null =================
                _Err_Msg = "Add Flo4"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo4") Then
                    ChkNull_Flo4_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo4")).ToString
                    If Not ChkNull_Flo4_Detail Is Nothing OrElse Not ChkNull_Flo4_Detail = "" Then
                        DrDetail("Flo4") = ChkNull_Flo4_Detail
                    Else
                        DrDetail("Flo4") = 0 'For Default value
                    End If
                Else
                    DrDetail("Flo4") = 0 'For Default value
                End If
                '================= Flo5 Check Null =================
                _Err_Msg = "Add Flo5"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Flo5") Then
                    ChkNull_Flo5_Detail = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Flo5")).ToString
                    If Not ChkNull_Flo5_Detail Is Nothing OrElse Not ChkNull_Flo5_Detail = "" Then
                        DrDetail("Flo5") = ChkNull_Flo5_Detail
                    Else
                        DrDetail("Flo5") = 0 'For Default value
                    End If
                Else
                    DrDetail("Flo5") = 0 'For Default value
                End If

                '================= Charge_Status Check Null =================
                _Err_Msg = "Add Charge_Status"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Charge_Status") Then
                    ChkNull_Charge_Status = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Charge_Status")).ToString
                    If Not ChkNull_Charge_Status Is Nothing OrElse Not ChkNull_Charge_Status = "" Then
                        DrDetail("Charge_Status") = ChkNull_Charge_Status
                    Else
                        DrDetail("Charge_Status") = 0 'For Default value
                    End If
                Else
                    DrDetail("Charge_Status") = 0 'For Default value
                End If

                '================= PLot Check Null =================
                _Err_Msg = "Add PLot"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "PLot") Then
                    ChkNull_PLot = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "PLot")).ToString
                    If Not ChkNull_PLot Is Nothing OrElse Not ChkNull_PLot = "" Then
                        DrDetail("PLot") = ChkNull_PLot
                    Else
                        DrDetail("PLot") = "" 'For Default value
                    End If
                Else
                    DrDetail("PLot") = "" 'For Default value
                End If
                '================= ERP_Location Check Null =================
                _Err_Msg = "Add ERP_Location"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "ERP_Location") Then
                    ChkNull_ERP_Location = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "ERP_Location")).ToString
                    If Not ChkNull_ERP_Location Is Nothing OrElse Not ChkNull_ERP_Location = "" Then
                        DrDetail("ERP_Location") = ChkNull_ERP_Location
                    Else
                        DrDetail("ERP_Location") = "" 'For Default value
                    End If
                Else
                    DrDetail("ERP_Location") = "" 'For Default value
                End If
                '================= ItemStatus_Index Check Null =================
                _Err_Msg = "Add ItemStatus_Index"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "ItemStatus_Id") Then
                    ChkNull_ItemStatus_Index = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "ItemStatus_Index")).ToString
                    If Not ChkNull_ItemStatus_Index Is Nothing OrElse Not ChkNull_ItemStatus_Index = "" Then
                        DrDetail("ItemStatus_Index") = ChkNull_ItemStatus_Index
                    Else
                        DrDetail("ItemStatus_Index") = ItemStatus_Index 'For Default value
                    End If
                Else
                    If ItemStatus_Index = "" Then ItemStatus_Index = "0010000000001"
                    DrDetail("ItemStatus_Index") = ItemStatus_Index 'For Default value
                End If

                _Err_Msg = "Add isUSE"
                If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "isUSE") Then
                    Chk_isUSE = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "isUSE")).ToString
                    If Not Chk_isUSE Is Nothing OrElse Not Chk_isUSE = "" Then
                        DrDetail("isUSE") = Chk_isUSE
                    Else
                        DrDetail("isUSE") = 0 'For Default value
                    End If
                Else
                    DrDetail("isUSE") = 0 'For Default value
                End If

                _Err_Msg = "Add HandOver_Total_Qty"
                'If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "HandOver_Total_Qty") Then
                '    ChkNull_HandOver_Total_Qty = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "HandOver_Total_Qty")).ToString
                '    If Not ChkNull_HandOver_Total_Qty Is Nothing OrElse Not ChkNull_HandOver_Total_Qty = "" Then
                '        DrDetail("HandOver_Total_Qty") = ChkNull_HandOver_Total_Qty
                '    Else
                '        DrDetail("HandOver_Total_Qty") = 0 'For Default value
                '    End If
                'Else
                '    DrDetail("HandOver_Total_Qty") = 0 'For Default value
                'End If

                '_Err_Msg = "Add Total_Qty_Packed"
                'If KascoCLB.Import.check_Isuse(_dtConfig, strField_Type, "Total_Qty_Packed") Then
                '    ChkNull_Total_Qty_Packed = _DtTemp.Rows(i)(KascoCLB.Import.get_TabIndex(_dtConfig, strField_Type, "Total_Qty_Packed")).ToString
                '    If Not ChkNull_Total_Qty_Packed Is Nothing OrElse Not ChkNull_Total_Qty_Packed = "" Then
                '        DrDetail("Total_Qty_Packed") = ChkNull_HandOver_Total_Qty
                '    Else
                '        DrDetail("Total_Qty_Packed") = 0 'For Default value
                '    End If
                'Else
                '    DrDetail("Total_Qty_Packed") = 0 'For Default value
                'End If

                If blChkHaveSoNo = False Then
                    _dtHeader.Rows.Add(DrHeader)
                End If
                _dtDetail.Rows.Add(DrDetail)
            Next


            Return True
        Catch ex As Exception
            objDBSaveLog.Str1 = ex.Message
            objDBSaveLog.insertErrorLog(strFileName, _Err_Msg, _Err_Checkpoint, Line_Err, _Import_Log_Index, Remark_Log)
            Update_Prepare_Imports()
            Return False
        End Try
    End Function

    Public Sub Update_Prepare_Imports()
        Dim ObjImportInDLL As New KascoCLB.Import
        ObjImportInDLL.GUid = GUid
        ObjImportInDLL.status = -1
        ObjImportInDLL.UpdateStatus_Prepare_Imports()
        ObjImportInDLL = Nothing
    End Sub

    Public Function Insert_SO() As Boolean
        Dim strSQL As String = " "
        Dim objDBIndex As New Sy_AutoNumber
        Dim DtDetail_New As New DataTable
        Dim objDBSaveLog As New Insert_Log
        Dim drHeader As DataRow
        Dim drDetail As DataRow
        Dim chkLoag As String = ""
        _Err_Checkpoint = "Insert SO"
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim drArrHeader() As DataRow = _dtHeader.Select("SalesOrder_No = '" & _SalesOrder_No & "'")
            If drArrHeader.Length > 0 Then

                _Err_Checkpoint = "Insert SO Header"
                Me._SalesOrder_Index = objDBIndex.getSys_Value("SalesOrder_Index")
                drHeader = drArrHeader(0)
                '-----------------------
                strSQL = " INSERT INTO tb_SalesOrder"
                strSQL &= "  ("
                strSQL &= "SalesOrder_Index"
                strSQL &= " ,SalesOrder_No"
                strSQL &= " ,SalesOrder_Date"
                strSQL &= " ,Carrier_Index"
                strSQL &= " ,Customer_Receive_Location_Index"
                strSQL &= " ,Expected_Delivery_Date"
                strSQL &= " ,Time_ExpectedDocPickup"
                strSQL &= " ,Time_DocPickup"
                strSQL &= " ,Delivery_Date"
                strSQL &= " ,Customer_Shipping_Index"
                strSQL &= " ,Department_Index"
                strSQL &= " ,DocumentType_Index"
                strSQL &= " ,Remark"
                strSQL &= " ,Credit_Term"
                strSQL &= " ,Currency_Index"
                strSQL &= " ,Exchange_Rate"
                strSQL &= " ,PaymentMethod_Index"
                strSQL &= " ,Payment_Ref"
                strSQL &= " ,FullPaid_Date"
                strSQL &= " ,Amount"
                strSQL &= " ,Discount_Percent"
                strSQL &= " ,Discount_Amt"
                strSQL &= " ,Deposit_Amt"
                strSQL &= " ,Total_Amt"
                strSQL &= " ,VAT_Percent"
                strSQL &= " ,VAT"
                strSQL &= " ,Net_Amt"
                strSQL &= " ,Reserve_index"
                strSQL &= " ,Str1"
                strSQL &= " ,Str2"
                strSQL &= " ,Str3"
                strSQL &= " ,Str4"
                strSQL &= " ,Str5"
                strSQL &= " ,Str6"
                strSQL &= " ,Str7"
                strSQL &= " ,Str8"
                strSQL &= " ,Str9"
                strSQL &= " ,Str10"
                strSQL &= " ,Flo1"
                strSQL &= " ,Flo2"
                strSQL &= " ,Flo3"
                strSQL &= " ,Flo4"
                strSQL &= " ,Flo5"
                strSQL &= " ,add_by"
                strSQL &= " ,add_date"
                strSQL &= " ,add_branch"
                'strSQL &= " ,update_by"
                'strSQL &= " ,update_date"
                'strSQL &= " ,update_branch"
                'strSQL &= " ,cancel_by"
                'strSQL &= " ,cancel_date"
                'strSQL &= " ,cancel_branch"

                strSQL &= " ,Status"
                strSQL &= " ,Salesman_Index"
                strSQL &= " ,Sales_Region_Index"
                strSQL &= " ,Promotion_Index"
                strSQL &= " ,CommissionGroup_Index"
                strSQL &= " ,Supplier_Index"
                strSQL &= " ,Customer_Index"
                strSQL &= " ,Customer_Shipping_Location_Index"
                strSQL &= " ,DistributionCenter_Index"
                strSQL &= " ,SubRoute_Index"
                strSQL &= " ,Route_Index"
                strSQL &= " ,Time_DocTripConfirmed"
                strSQL &= " ,Time_DeliveryToDestination"
                strSQL &= " ,Time_DocReturnedToDC"
                strSQL &= " ,Time_DocReturnedToSource"
                strSQL &= " ,Time_DocReturnedToOwner"
                strSQL &= " ,Time_DocConfirmedByOwner"
                strSQL &= " ,Transport_Status"
                strSQL &= " ,chk_Problem"
                strSQL &= " ,JobProblem_Index"
                strSQL &= " ,JobProblem_Desc"
                strSQL &= " ,ResponsibleParty_Index"
                strSQL &= " ,JobSolution_Index"
                strSQL &= " ,JobSolution_Desc"
                strSQL &= " ,Process_Id"
                strSQL &= " ,Status_Manifest"
                strSQL &= " ,PO_No"
                strSQL &= " ,PO_Date"
                strSQL &= " ,Saletype_Id"
                strSQL &= " ,Shipby_Id"
                strSQL &= " ,Worker"
                strSQL &= " ,District_Index"
                strSQL &= " ,Province_Index"
                strSQL &= " ,VehicleType_Index"
                strSQL &= " ,PODRemark1"
                strSQL &= " ,PODDoc_Copy1"
                strSQL &= " ,PODDoc_Copy2"
                strSQL &= " ,PODDoc_Copy3"
                strSQL &= " ,PODDoc_Copy4"
                strSQL &= " ,PODDoc_Copy5"
                strSQL &= " ,GRRemark1"
                strSQL &= " ,GRDoc_Copy1"
                strSQL &= " ,GRDoc_Copy2"
                strSQL &= " ,GRDoc_Copy3"
                strSQL &= " ,GRDoc_Copy4"
                strSQL &= " ,GRDoc_Copy5"
                'strSQL &= " ,Dock"
                'strSQL &= " ,Mile_AtSource"
                'strSQL &= " ,Mile_AtDestination"
                'strSQL &= " ,Time_DestinationOutGate"
                'strSQL &= " ,TransportRegion_Index"
                'strSQL &= " ,DocumentPlanTO"
                'strSQL &= " ,IsTransportPaid"
                'strSQL &= " ,IsTransportCharged"
                'strSQL &= " ,Status_Pack"
                'strSQL &= " ,Total_Qty_Pack"
                'strSQL &= " ,TransportManifest_No"
                'strSQL &= " ,Status_Interface"
                strSQL &= "  ) VALUES ( "
                strSQL &= "@SalesOrder_Index"
                strSQL &= ",@SalesOrder_No"
                strSQL &= ",@SalesOrder_Date"
                strSQL &= ",@Carrier_Index"
                strSQL &= ",@Customer_Receive_Location_Index"
                strSQL &= ",@Expected_Delivery_Date"
                strSQL &= ",@Time_ExpectedDocPickup"
                strSQL &= ",@Time_DocPickup"
                strSQL &= ",@Delivery_Date"
                strSQL &= ",@Customer_Shipping_Index"
                strSQL &= ",@Department_Index"
                strSQL &= ",@DocumentType_Index"
                strSQL &= ",@Remark"
                strSQL &= ",@Credit_Term"
                strSQL &= ",@Currency_Index"
                strSQL &= ",@Exchange_Rate"
                strSQL &= ",@PaymentMethod_Index"
                strSQL &= ",@Payment_Ref"
                strSQL &= ",@FullPaid_Date"
                strSQL &= ",@Amount"
                strSQL &= ",@Discount_Percent"
                strSQL &= ",@Discount_Amt"
                strSQL &= ",@Deposit_Amt"
                strSQL &= ",@Total_Amt"
                strSQL &= ",@VAT_Percent"
                strSQL &= ",@VAT"
                strSQL &= ",@Net_Amt"
                strSQL &= ",@Reserve_index"
                strSQL &= ",@Str1"
                strSQL &= ",@Str2"
                strSQL &= ",@Str3"
                strSQL &= ",@Str4"
                strSQL &= ",@Str5"
                strSQL &= ",@Str6"
                strSQL &= ",@Str7"
                strSQL &= ",@Str8"
                strSQL &= ",@Str9"
                strSQL &= ",@Str10"
                strSQL &= ",@Flo1"
                strSQL &= ",@Flo2"
                strSQL &= ",@Flo3"
                strSQL &= ",@Flo4"
                strSQL &= ",@Flo5"
                strSQL &= ",@add_by"
                strSQL &= ",@add_date"
                strSQL &= ",@add_branch"
                'strSQL &= ",@update_by"
                'strSQL &= ",@update_date"
                'strSQL &= ",@update_branch"
                'strSQL &= ",@cancel_by"
                'strSQL &= ",@cancel_date"
                'strSQL &= ",@cancel_branch"

                strSQL &= ",@Status"
                strSQL &= ",@Salesman_Index"
                strSQL &= ",@Sales_Region_Index"
                strSQL &= ",@Promotion_Index"
                strSQL &= ",@CommissionGroup_Index"
                strSQL &= ",@Supplier_Index"
                strSQL &= ",@Customer_Index"
                strSQL &= ",@Customer_Shipping_Location_Index"
                strSQL &= ",@DistributionCenter_Index"
                strSQL &= ",@SubRoute_Index"
                strSQL &= ",@Route_Index"
                strSQL &= ",@Time_DocTripConfirmed"
                strSQL &= ",@Time_DeliveryToDestination"
                strSQL &= ",@Time_DocReturnedToDC"
                strSQL &= ",@Time_DocReturnedToSource"
                strSQL &= ",@Time_DocReturnedToOwner"
                strSQL &= ",@Time_DocConfirmedByOwner"
                strSQL &= ",@Transport_Status"
                strSQL &= ",@chk_Problem"
                strSQL &= ",@JobProblem_Index"
                strSQL &= ",@JobProblem_Desc"
                strSQL &= ",@ResponsibleParty_Index"
                strSQL &= ",@JobSolution_Index"
                strSQL &= ",@JobSolution_Desc"
                strSQL &= ",@Process_Id"
                strSQL &= ",@Status_Manifest"
                strSQL &= ",@PO_No"
                strSQL &= ",@PO_Date"
                strSQL &= ",@Saletype_Id"
                strSQL &= ",@Shipby_Id"
                strSQL &= ",@Worker"
                strSQL &= ",@District_Index"
                strSQL &= ",@Province_Index"
                strSQL &= ",@VehicleType_Index"
                strSQL &= ",@PODRemark1"
                strSQL &= ",@PODDoc_Copy1"
                strSQL &= ",@PODDoc_Copy2"
                strSQL &= ",@PODDoc_Copy3"
                strSQL &= ",@PODDoc_Copy4"
                strSQL &= ",@PODDoc_Copy5"
                strSQL &= ",@GRRemark1"
                strSQL &= ",@GRDoc_Copy1"
                strSQL &= ",@GRDoc_Copy2"
                strSQL &= ",@GRDoc_Copy3"
                strSQL &= ",@GRDoc_Copy4"
                strSQL &= ",@GRDoc_Copy5"
                'strSQL &= ",@Dock"
                'strSQL &= ",@Mile_AtSource"
                'strSQL &= ",@Mile_AtDestination"
                'strSQL &= ",@Time_DestinationOutGate"
                'strSQL &= ",@TransportRegion_Index"
                'strSQL &= ",@DocumentPlanTO"
                'strSQL &= ",@IsTransportPaid"
                'strSQL &= ",@IsTransportCharged"
                'strSQL &= ",@Status_Pack"
                'strSQL &= ",@Total_Qty_Pack"
                'strSQL &= ",@TransportManifest_No"
                'strSQL &= ",@Status_Interface"
                strSQL &= "  ) "

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _SalesOrder_Index
                    .Parameters.Add("@SalesOrder_No", SqlDbType.VarChar, 50).Value = drHeader("SalesOrder_No").ToString
                    .Parameters.Add("@SalesOrder_Date", SqlDbType.SmallDateTime, 16).Value = drHeader("SalesOrder_Date")
                    .Parameters.Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = drHeader("Carrier_Index").ToString
                    .Parameters.Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = drHeader("Customer_Receive_Location_Index").ToString
                    .Parameters.Add("@Expected_Delivery_Date", SqlDbType.SmallDateTime, 16).Value = drHeader("Expected_Delivery_Date")
                    .Parameters.Add("@Time_ExpectedDocPickup", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_ExpectedDocPickup").ToString
                    .Parameters.Add("@Time_DocPickup", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DocPickup").ToString
                    .Parameters.Add("@Delivery_Date", SqlDbType.SmallDateTime, 16).Value = drHeader("Delivery_Date")
                    .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = drHeader("Customer_Shipping_Index").ToString
                    .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = drHeader("Department_Index").ToString
                    'If drHeader("DocumentType_Index").ToString = "" Then

                    '    .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = appSet.GetValue("Config_ImportSO_DocumentType_Index", GetType(String))


                    'Else

                    '    .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = drHeader("DocumentType_Index").ToString
                    'End If

                    .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = Me._DocumentType_Index ' New Req 2015/02/10

                    .Parameters.Add("@Remark", SqlDbType.NVarChar, 510).Value = drHeader("Remark").ToString
                    .Parameters.Add("@Credit_Term", SqlDbType.NVarChar, 20).Value = drHeader("Credit_Term").ToString
                    .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = drHeader("Currency_Index").ToString
                    .Parameters.Add("@Exchange_Rate", SqlDbType.Float, 8).Value = drHeader("Exchange_Rate").ToString
                    .Parameters.Add("@PaymentMethod_Index", SqlDbType.NVarChar, 26).Value = drHeader("PaymentMethod_Index").ToString
                    .Parameters.Add("@Payment_Ref", SqlDbType.NVarChar, 100).Value = drHeader("Payment_Ref").ToString
                    .Parameters.Add("@FullPaid_Date", SqlDbType.SmallDateTime, 16).Value = drHeader("FullPaid_Date")
                    .Parameters.Add("@Amount", SqlDbType.Float, 8).Value = drHeader("Amount")
                    .Parameters.Add("@Discount_Percent", SqlDbType.Int, 4).Value = drHeader("Discount_Percent")
                    .Parameters.Add("@Discount_Amt", SqlDbType.Float, 8).Value = drHeader("Discount_Amt")
                    .Parameters.Add("@Deposit_Amt", SqlDbType.Float, 8).Value = drHeader("Deposit_Amt")
                    .Parameters.Add("@Total_Amt", SqlDbType.Float, 8).Value = drHeader("Total_Amt")
                    .Parameters.Add("@VAT_Percent", SqlDbType.Int, 4).Value = drHeader("VAT_Percent")
                    .Parameters.Add("@VAT", SqlDbType.Float, 8).Value = drHeader("VAT")
                    .Parameters.Add("@Net_Amt", SqlDbType.Float, 8).Value = drHeader("Net_Amt")
                    .Parameters.Add("@Reserve_index", SqlDbType.NVarChar, 26).Value = drHeader("Reserve_index").ToString
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = drHeader("Str1").ToString
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = drHeader("Str2").ToString
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = drHeader("Str3").ToString
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = drHeader("Str4").ToString
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = drHeader("Str5").ToString
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 200).Value = drHeader("Str6").ToString
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 200).Value = drHeader("Str7").ToString
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 200).Value = drHeader("Str8").ToString
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 4000).Value = drHeader("Str9").ToString
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 4000).Value = drHeader("Str10").ToString
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = drHeader("Flo1")
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = drHeader("Flo2")
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = drHeader("Flo3")
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = drHeader("Flo4")
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = drHeader("Flo5")
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = Now
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    '.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = drHeader("update_by").ToString
                    '.Parameters.Add("@update_date", SqlDbType.SmallDateTime, 16).Value = drHeader("update_date")
                    '.Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = drHeader("update_branch").ToString
                    '.Parameters.Add("@cancel_by", SqlDbType.VarChar, 50).Value = drHeader("cancel_by").ToString
                    '.Parameters.Add("@cancel_date", SqlDbType.SmallDateTime, 16).Value = drHeader("cancel_date")
                    '.Parameters.Add("@cancel_branch", SqlDbType.Int, 4).Value = drHeader("cancel_branch").ToString

                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                    .Parameters.Add("@Salesman_Index", SqlDbType.VarChar, 13).Value = drHeader("Salesman_Index").ToString
                    .Parameters.Add("@Sales_Region_Index", SqlDbType.VarChar, 13).Value = drHeader("Sales_Region_Index").ToString
                    .Parameters.Add("@Promotion_Index", SqlDbType.VarChar, 13).Value = drHeader("Promotion_Index").ToString
                    .Parameters.Add("@CommissionGroup_Index", SqlDbType.VarChar, 13).Value = drHeader("CommissionGroup_Index").ToString
                    .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = drHeader("Supplier_Index").ToString
                    .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = drHeader("Customer_Index").ToString
                    .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = drHeader("Customer_Shipping_Location_Index").ToString
                    .Parameters.Add("@DistributionCenter_Index", SqlDbType.VarChar, 13).Value = drHeader("DistributionCenter_Index").ToString
                    .Parameters.Add("@SubRoute_Index", SqlDbType.VarChar, 13).Value = drHeader("SubRoute_Index").ToString
                    .Parameters.Add("@Route_Index", SqlDbType.VarChar, 13).Value = drHeader("Route_Index").ToString
                    .Parameters.Add("@Time_DocTripConfirmed", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DocTripConfirmed").ToString
                    .Parameters.Add("@Time_DeliveryToDestination", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DeliveryToDestination").ToString
                    .Parameters.Add("@Time_DocReturnedToDC", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DocReturnedToDC").ToString
                    .Parameters.Add("@Time_DocReturnedToSource", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DocReturnedToSource").ToString
                    .Parameters.Add("@Time_DocReturnedToOwner", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DocReturnedToOwner").ToString
                    .Parameters.Add("@Time_DocConfirmedByOwner", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DocConfirmedByOwner").ToString
                    .Parameters.Add("@Transport_Status", SqlDbType.VarChar, 100).Value = drHeader("Transport_Status").ToString
                    .Parameters.Add("@chk_Problem", SqlDbType.Bit, 1).Value = 0 'drHeader("chk_Problem").ToString
                    .Parameters.Add("@JobProblem_Index", SqlDbType.VarChar, 13).Value = drHeader("JobProblem_Index").ToString
                    .Parameters.Add("@JobProblem_Desc", SqlDbType.VarChar, 500).Value = drHeader("JobProblem_Desc").ToString
                    .Parameters.Add("@ResponsibleParty_Index", SqlDbType.VarChar, 13).Value = drHeader("ResponsibleParty_Index").ToString
                    .Parameters.Add("@JobSolution_Index", SqlDbType.VarChar, 13).Value = drHeader("JobSolution_Index").ToString
                    .Parameters.Add("@JobSolution_Desc", SqlDbType.VarChar, 500).Value = drHeader("JobSolution_Desc").ToString
                    .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 10 'drHeader("Process_Id")
                    .Parameters.Add("@Status_Manifest", SqlDbType.Int, 4).Value = 1 'drHeader("Status_Manifest").ToString
                    .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = drHeader("PO_No").ToString
                    .Parameters.Add("@PO_Date", SqlDbType.SmallDateTime, 16).Value = drHeader("PO_Date")
                    .Parameters.Add("@Saletype_Id", SqlDbType.VarChar, 15).Value = drHeader("Saletype_Id").ToString
                    .Parameters.Add("@Shipby_Id", SqlDbType.VarChar, 15).Value = drHeader("Shipby_Id").ToString
                    .Parameters.Add("@Worker", SqlDbType.Int, 4).Value = 0 'drHeader("Worker").ToString
                    .Parameters.Add("@District_Index", SqlDbType.VarChar, 13).Value = drHeader("District_Index").ToString
                    .Parameters.Add("@Province_Index", SqlDbType.VarChar, 13).Value = drHeader("Province_Index").ToString
                    .Parameters.Add("@VehicleType_Index", SqlDbType.VarChar, 13).Value = drHeader("VehicleType_Index").ToString
                    .Parameters.Add("@PODRemark1", SqlDbType.VarChar, 1000).Value = drHeader("PODRemark1").ToString
                    .Parameters.Add("@PODDoc_Copy1", SqlDbType.VarChar, 500).Value = drHeader("PODDoc_Copy1").ToString
                    .Parameters.Add("@PODDoc_Copy2", SqlDbType.VarChar, 500).Value = drHeader("PODDoc_Copy2").ToString
                    .Parameters.Add("@PODDoc_Copy3", SqlDbType.VarChar, 500).Value = drHeader("PODDoc_Copy3").ToString
                    .Parameters.Add("@PODDoc_Copy4", SqlDbType.VarChar, 500).Value = drHeader("PODDoc_Copy4").ToString
                    .Parameters.Add("@PODDoc_Copy5", SqlDbType.VarChar, 500).Value = drHeader("PODDoc_Copy5").ToString
                    .Parameters.Add("@GRRemark1", SqlDbType.VarChar, 500).Value = drHeader("GRRemark1").ToString
                    .Parameters.Add("@GRDoc_Copy1", SqlDbType.VarChar, 500).Value = drHeader("GRDoc_Copy1").ToString
                    .Parameters.Add("@GRDoc_Copy2", SqlDbType.VarChar, 500).Value = drHeader("GRDoc_Copy2").ToString
                    .Parameters.Add("@GRDoc_Copy3", SqlDbType.VarChar, 500).Value = drHeader("GRDoc_Copy3").ToString
                    .Parameters.Add("@GRDoc_Copy4", SqlDbType.VarChar, 500).Value = drHeader("GRDoc_Copy4").ToString
                    .Parameters.Add("@GRDoc_Copy5", SqlDbType.VarChar, 500).Value = drHeader("GRDoc_Copy5").ToString
                    '.Parameters.Add("@Dock", SqlDbType.VarChar, 500).Value = drHeader("Dock").ToString
                    '.Parameters.Add("@Mile_AtSource", SqlDbType.Int, 4).Value = 0 'drHeader("Mile_AtSource").ToString
                    '.Parameters.Add("@Mile_AtDestination", SqlDbType.Int, 4).Value = 0 'drHeader("Mile_AtDestination").ToString
                    '.Parameters.Add("@Time_DestinationOutGate", SqlDbType.SmallDateTime, 16).Value = drHeader("Time_DestinationOutGate").ToString
                    '.Parameters.Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = drHeader("TransportRegion_Index").ToString
                    '.Parameters.Add("@DocumentPlanTO", SqlDbType.VarChar, 13).Value = drHeader("DocumentPlanTO").ToString
                    '.Parameters.Add("@IsTransportPaid", SqlDbType.Bit, 1).Value = 0 'drHeader("IsTransportPaid").ToString
                    '.Parameters.Add("@IsTransportCharged", SqlDbType.Bit, 1).Value = 0 'drHeader("IsTransportCharged").ToString
                    '.Parameters.Add("@Status_Pack", SqlDbType.Int, 4).Value = 0 'drHeader("Status_Pack").ToString
                    '.Parameters.Add("@Total_Qty_Pack", SqlDbType.Float, 8).Value = drHeader("Total_Qty_Pack").ToString
                    '.Parameters.Add("@TransportManifest_No", SqlDbType.VarChar, 50).Value = drHeader("TransportManifest_No").ToString
                    '.Parameters.Add("@Status_Interface", SqlDbType.Int, 4).Value = 1
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
                '----------------------
                _Err_Checkpoint = "Insert SO Detail"
                Dim Dt_DetailInsert As New DataTable
                Dim drArrItem() As DataRow = _dtDetail.Select("SalesOrder_No = '" & SalesOrder_No & "'")
                If drArrItem.Length > 0 Then
                    For j As Integer = 0 To drArrItem.Length - 1
                        If _SalesOrderItem_Index Is Nothing Then
                            Me._SalesOrderItem_Index = objDBIndex.getSys_Value("SalesOrderItem_Index")
                        End If
                        drDetail = _dtDetail.Rows(j)
                        strSQL = " INSERT INTO tb_SalesOrderItem"
                        strSQL &= "  ("
                        strSQL &= "SalesOrderItem_Index"
                        strSQL &= " ,Item_Seq"
                        strSQL &= " ,SalesOrder_Index"
                        strSQL &= " ,Sku_Index"
                        strSQL &= " ,Package_Index"
                        strSQL &= " ,Ratio"
                        strSQL &= " ,Total_Qty"
                        strSQL &= " ,Qty"
                        strSQL &= " ,Weight"
                        strSQL &= " ,Volume"
                        strSQL &= " ,Serial_No"
                        strSQL &= " ,Total_Qty_Withdraw"
                        strSQL &= " ,Qty_Withdraw"
                        strSQL &= " ,Weight_Withdraw"
                        strSQL &= " ,Volume_Withdraw"
                        strSQL &= " ,Last_Withdraw_Date"
                        strSQL &= " ,UnitPrice"
                        strSQL &= " ,Amount"
                        strSQL &= " ,Currency_Index"
                        strSQL &= " ,Discount_Amt"
                        strSQL &= " ,Total_Amt"
                        strSQL &= " ,Status"
                        strSQL &= " ,Remark"
                        strSQL &= " ,Reason"
                        strSQL &= " ,Ref_No1"
                        strSQL &= " ,Ref_No2"
                        strSQL &= " ,Str1"
                        strSQL &= " ,Str2"
                        strSQL &= " ,Str3"
                        strSQL &= " ,Str4"
                        strSQL &= " ,Str5"
                        strSQL &= " ,Str6"
                        strSQL &= " ,Str7"
                        strSQL &= " ,Str8"
                        strSQL &= " ,Str9"
                        strSQL &= " ,Str10"
                        strSQL &= " ,Flo1"
                        strSQL &= " ,Flo2"
                        strSQL &= " ,Flo3"
                        strSQL &= " ,Flo4"
                        strSQL &= " ,Flo5"
                        strSQL &= " ,add_by"
                        strSQL &= " ,add_date"

                        'strSQL &= " ,add_branch"
                        'strSQL &= " ,update_by"
                        'strSQL &= " ,update_date"
                        'strSQL &= " ,update_branch"
                        'strSQL &= " ,cancel_by"
                        'strSQL &= " ,cancel_date"
                        'strSQL &= " ,cancel_branch"

                        strSQL &= " ,Charge_Status"
                        strSQL &= " ,PLot"
                        strSQL &= " ,ERP_Location"
                        strSQL &= " ,ItemStatus_Index"
                        strSQL &= " ,isUSE"
                        strSQL &= " ,DocumentProcess"
                        'strSQL &= " ,HandOver_Total_Qty"
                        'strSQL &= " ,Total_Qty_Packed"
                        strSQL &= "  ) VALUES ( "
                        strSQL &= "@SalesOrderItem_Index"
                        strSQL &= ",@Item_Seq"
                        strSQL &= ",@SalesOrder_Index"
                        strSQL &= ",@Sku_Index"
                        strSQL &= ",@Package_Index"
                        strSQL &= ",@Ratio"
                        strSQL &= ",@Total_Qty"
                        strSQL &= ",@Qty"
                        strSQL &= ",@Weight"
                        strSQL &= ",@Volume"
                        strSQL &= ",@Serial_No"
                        strSQL &= ",@Total_Qty_Withdraw"
                        strSQL &= ",@Qty_Withdraw"
                        strSQL &= ",@Weight_Withdraw"
                        strSQL &= ",@Volume_Withdraw"
                        strSQL &= ",@Last_Withdraw_Date"
                        strSQL &= ",@UnitPrice"
                        strSQL &= ",@Amount"
                        strSQL &= ",@Currency_Index"
                        strSQL &= ",@Discount_Amt"
                        strSQL &= ",@Total_Amt"
                        strSQL &= ",@Status"
                        strSQL &= ",@Remark"
                        strSQL &= ",@Reason"
                        strSQL &= ",@Ref_No1"
                        strSQL &= ",@Ref_No2"
                        strSQL &= ",@Str1"
                        strSQL &= ",@Str2"
                        strSQL &= ",@Str3"
                        strSQL &= ",@Str4"
                        strSQL &= ",@Str5"
                        strSQL &= ",@Str6"
                        strSQL &= ",@Str7"
                        strSQL &= ",@Str8"
                        strSQL &= ",@Str9"
                        strSQL &= ",@Str10"
                        strSQL &= ",@Flo1"
                        strSQL &= ",@Flo2"
                        strSQL &= ",@Flo3"
                        strSQL &= ",@Flo4"
                        strSQL &= ",@Flo5"
                        strSQL &= ",@add_by"
                        strSQL &= ",@add_date"

                        'strSQL &= ",@add_branch"
                        'strSQL &= ",@update_by"
                        'strSQL &= ",@update_date"
                        'strSQL &= ",@update_branch"
                        'strSQL &= ",@cancel_by"
                        'strSQL &= ",@cancel_date"
                        'strSQL &= ",@cancel_branch"

                        strSQL &= ",@Charge_Status"
                        strSQL &= ",@PLot"
                        strSQL &= ",@ERP_Location"
                        strSQL &= ",@ItemStatus_Index"
                        strSQL &= ",@isUSE"
                        strSQL &= ",@DocumentProcess"
                        'strSQL &= ",@HandOver_Total_Qty"
                        'strSQL &= ",@Total_Qty_Packed"
                        strSQL &= "  ) "

                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar, 13).Value = _SalesOrderItem_Index
                            .Parameters.Add("@Item_Seq", SqlDbType.Int, 4).Value = drDetail("Item_Seq")
                            .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _SalesOrder_Index
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drDetail("Sku_Index").ToString
                            .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = drDetail("Package_Index").ToString
                            .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = drDetail("Ratio").ToString
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = drDetail("Total_Qty").ToString
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = drDetail("Qty").ToString
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = drDetail("Weight").ToString
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = drDetail("Volume").ToString
                            .Parameters.Add("@Serial_No", SqlDbType.NVarChar, 200).Value = drDetail("Serial_No").ToString
                            .Parameters.Add("@Total_Qty_Withdraw", SqlDbType.Float, 8).Value = drDetail("Total_Qty_Withdraw").ToString
                            .Parameters.Add("@Qty_Withdraw", SqlDbType.Float, 8).Value = drDetail("Qty_Withdraw").ToString
                            .Parameters.Add("@Weight_Withdraw", SqlDbType.Float, 8).Value = drDetail("Weight_Withdraw").ToString
                            .Parameters.Add("@Volume_Withdraw", SqlDbType.Float, 8).Value = drDetail("Volume_Withdraw").ToString
                            .Parameters.Add("@Last_Withdraw_Date", SqlDbType.SmallDateTime, 16).Value = drDetail("Last_Withdraw_Date")
                            .Parameters.Add("@UnitPrice", SqlDbType.Float, 8).Value = drDetail("UnitPrice").ToString
                            .Parameters.Add("@Amount", SqlDbType.Float, 8).Value = drDetail("Amount").ToString
                            .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = drDetail("Currency_Index").ToString
                            .Parameters.Add("@Discount_Amt", SqlDbType.Float, 8).Value = drDetail("Discount_Amt").ToString
                            .Parameters.Add("@Total_Amt", SqlDbType.Float, 8).Value = drDetail("Total_Amt").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1 ' drDetail("Status").ToString
                            .Parameters.Add("@Remark", SqlDbType.NVarChar, 2000).Value = drDetail("Remark").ToString
                            .Parameters.Add("@Reason", SqlDbType.NVarChar, 2000).Value = drDetail("Reason").ToString
                            .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = drDetail("Ref_No1").ToString
                            .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = drDetail("Ref_No2").ToString
                            .Parameters.Add("@Str1", SqlDbType.NVarChar, 200).Value = drDetail("Str1").ToString
                            .Parameters.Add("@Str2", SqlDbType.NVarChar, 200).Value = drDetail("Str2").ToString
                            .Parameters.Add("@Str3", SqlDbType.NVarChar, 200).Value = drDetail("Str3").ToString
                            .Parameters.Add("@Str4", SqlDbType.NVarChar, 200).Value = drDetail("Str4").ToString
                            .Parameters.Add("@Str5", SqlDbType.NVarChar, 200).Value = drDetail("Str5").ToString
                            .Parameters.Add("@Str6", SqlDbType.NVarChar, 200).Value = drDetail("Str6").ToString
                            .Parameters.Add("@Str7", SqlDbType.NVarChar, 200).Value = drDetail("Str7").ToString
                            .Parameters.Add("@Str8", SqlDbType.NVarChar, 200).Value = drDetail("Str8").ToString
                            .Parameters.Add("@Str9", SqlDbType.NVarChar, 4000).Value = drDetail("Str9").ToString
                            .Parameters.Add("@Str10", SqlDbType.NVarChar, 4000).Value = drDetail("Str10").ToString
                            .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = drDetail("Flo1").ToString
                            .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = drDetail("Flo2").ToString
                            .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = drDetail("Flo3").ToString
                            .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = drDetail("Flo4").ToString
                            .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = drDetail("Flo5").ToString
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = Now

                            '.Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = drDetail("add_branch").ToString
                            '.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = drDetail("update_by").ToString
                            '.Parameters.Add("@update_date", SqlDbType.SmallDateTime, 16).Value = drDetail("update_date")
                            '.Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = drDetail("update_branch").ToString
                            '.Parameters.Add("@cancel_by", SqlDbType.VarChar, 50).Value = drDetail("cancel_by").ToString
                            '.Parameters.Add("@cancel_date", SqlDbType.SmallDateTime, 16).Value = drDetail("cancel_date").ToString
                            '.Parameters.Add("@cancel_branch", SqlDbType.Int, 4).Value = drDetail("cancel_branch").ToString

                            .Parameters.Add("@Charge_Status", SqlDbType.Int, 4).Value = drDetail("Charge_Status")
                            .Parameters.Add("@PLot", SqlDbType.NVarChar, 100).Value = drDetail("PLot").ToString
                            .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = drDetail("ERP_Location").ToString
                            .Parameters.Add("@ItemStatus_Index", SqlDbType.NVarChar, 100).Value = drDetail("ItemStatus_Index").ToString
                            .Parameters.Add("@isUSE", SqlDbType.Int, 4).Value = drDetail("isUSE").ToString
                            .Parameters.Add("@DocumentProcess", SqlDbType.VarChar, 13).Value = drDetail("DocumentProcess").ToString
                            '.Parameters.Add("@HandOver_Total_Qty", SqlDbType.Float, 8).Value = drDetail("HandOver_Total_Qty").ToString
                            '.Parameters.Add("@Total_Qty_Packed", SqlDbType.Float, 8).Value = drDetail("Total_Qty_Packed").ToString
                        End With
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                        _SalesOrderItem_Index = Nothing

                    Next '--- Detail

                End If

                'krit update fix Relation 

                SetSQLString = "exec sp_gen_create_R_Customer_Shipping_And_Customer_Shipping_Location"
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                myTrans.Commit()  'Commit ทีละ SO
                Return True
            End If
        Catch ex As Exception
            objDBSaveLog.insertErrorLog(_SalesOrder_Index, ex.Message, _Err_Checkpoint, SalesOrder_No, _Import_Log_Index, Remark_Log)
            myTrans.Rollback()
            Throw ex
            Return False
        Finally
            objDBIndex = Nothing
            objDBSaveLog = Nothing
            disconnectDB()
        End Try
    End Function

    Public Sub GetDateServer()
        Try
            Dim obj As New KascoCLB.Import
            DateServer = obj.GetDateServer()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function CheckPackage(ByVal pWHERE As String) As DataTable
        Dim SQL = "SELECT * FROM ms_SKURatio WHERE Package_Index IN (SELECT Package_Index FROM ms_Package WHERE " & pWHERE & ")"
        Return DBExeQuery(SQL)
    End Function
End Class
