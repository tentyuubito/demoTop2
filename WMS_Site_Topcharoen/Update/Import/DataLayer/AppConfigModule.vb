Module AppConfigModule
#Region " config"
    Public Line_Err As String = ""
    Public _Err_Msg As String = ""
    Public DateServer As DateTime
    Public Carrier_Index As String = ""
    Public Customer_Receive_Location_Index As String = ""
    Public Customer_Index As String = ""
    Public Supplier_Index As String = ""
    Public Department_Index As String = ""
    Public DocumentType_Index_config As String = ""
    Public Currency_Index As String = ""
    Public PaymentMethod_Index As String = ""
    Public Discount_Percent As Integer = 0
    Public ChkNull_Discount_Amt As String = ""
    Public VAT_Percent As Integer = 0
    Public Auto_Insert_Sku As String = ""
    Public Auto_Insert_Supplier As String = ""
    Public Format_Name As String = ""
    Public Chk_isUSE As String = ""
    Public _dtConfig As New DataTable
    Public ItemStatus_Index As String = ""
    Public ImportPath As String = ""
    Public ImportErrorPath As String = ""
    Public ImportCompletePath As String = ""
    Public Delimited As String = ""
#End Region

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

#Region "Add Sku"
    Public Sku_Id As String = ""
    Public Sku_Name As String = ""
    Public Package_Name As String = ""
    Public ProductType_Id As String = ""
#End Region
#Region "Dt"
    Public _dtHeader As New DataTable
    Public _dtDetail As New DataTable
#End Region
#Region "Variable"
    Public _Import_Log_Index As String = ""
    Public _Complete_Row_Count As Integer = 0
    Public _Start_Timestamp As Date
    Public _Imp_Row_Count As Integer = 0
#End Region

#Region " Property"
    Public ReadOnly Property dtConfig() As DataTable
        Get
            Return _dtConfig
        End Get
    End Property
#End Region
#Region " Private"
    Private _Dt As New DataTable
    Private _strGuid As String
    Private _strFileName As String
    Private _status As Object
    Private _DtConfig_Default_Import As New DataTable
    Private _DocumentType_Index As String = String.Empty
    Private _File_Prefix As String = String.Empty
    Private _Process_ID As Integer = 0
#End Region

#Region " Property"
    Public Property DtTemp() As DataTable
        Get
            Return _Dt
        End Get
        Set(ByVal value As DataTable)
            _Dt = value
        End Set
    End Property

    Public Property strGuid() As String
        Get
            Return _strGuid
        End Get
        Set(ByVal value As String)
            _strGuid = value
        End Set
    End Property

    Public Property strFileName() As String
        Get
            Return _strFileName
        End Get
        Set(ByVal value As String)
            _strFileName = value
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

    Public Sub GetDateServer()
        Try
            Dim ObjDateServer As New config_Import
            DateServer = ObjDateServer.GetDateServer()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub GetConfig_Default_Import2()
        Try
            Dim Objconfig_Import As New Import_WMS_SO_EXCEL.config_Import
            _DtConfig_Default_Import = Objconfig_Import.fnGetConfig_Default_Import_All("SO", 10)
            If _DtConfig_Default_Import.Rows.Count > 0 Then
                Format_Name = _DtConfig_Default_Import.Rows(0)("File_Prefix")
                ImportPath = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ImportPath")
                ImportErrorPath = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ImportErrorPath")
                ImportCompletePath = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ImportCompletePath")
                Auto_Insert_Sku = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Auto_Insert_Sku")
                'Auto_Insert_Supplier = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Auto_Insert_Supplier")
                Carrier_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Carrier_Index")
                Customer_Receive_Location_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Customer_Receive_Location_Index")
                Customer_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Customer_Index")
                Supplier_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Supplier_Index")
                Department_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Department_Index")
                DocumentType_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "DocumentType_Index")
                Currency_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Currency_Index")
                PaymentMethod_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "PaymentMethod_Index")
                Discount_Percent = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Discount_Percent")
                VAT_Percent = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "VAT_Percent")
                Delimited = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Delimited")

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetConfig_Default_Import(ByVal strPrefix As String, ByVal intProcessID As Integer)
        Try
            Dim Objconfig_Import As New config_Import
            _DtConfig_Default_Import = Objconfig_Import.fnGetConfig_Default_Import_All(strPrefix, intProcessID)
            _dtConfig = _DtConfig_Default_Import
            If _DtConfig_Default_Import.Rows.Count > 0 Then
                Format_Name = _DtConfig_Default_Import.Rows(0)("File_Prefix")
                ' ImportPath = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ImportPath")
                'ImportErrorPath = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ImportErrorPath")
                'ImportCompletePath = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ImportCompletePath")
                Auto_Insert_Sku = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Auto_Insert_Sku")
                ' Auto_Insert_Supplier = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Auto_Insert_Supplier")
                Carrier_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Carrier_Index")
                Customer_Receive_Location_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Customer_Receive_Location_Index")
                Customer_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Customer_Index")
                Supplier_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Supplier_Index")
                Department_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Department_Index")
                DocumentType_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "DocumentType_Index")
                Currency_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Currency_Index")
                PaymentMethod_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "PaymentMethod_Index")
                Discount_Percent = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Discount_Percent")
                VAT_Percent = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "VAT_Percent")
                'Delimited = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Delimited")
                DateServer = Now
                ItemStatus_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ItemStatus_Index")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function GetConfig_Default_Import() As Boolean
        Try
            Dim Objconfig_Import As New KascoCLB.Import
            'รับจาก site
            If _File_Prefix = String.Empty Then
                _File_Prefix = "SO"
            End If
            'รับจาก site Default 0
            If _Process_ID = 0 Then
                _Process_ID = 10
            End If
            _DtConfig_Default_Import = Objconfig_Import.fnGetConfig_Default_Import_All(File_Prefix, _Process_ID)

            If _DtConfig_Default_Import.Rows.Count > 0 Then
                Auto_Insert_Sku = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Auto_Insert_Sku")
                Auto_Insert_Supplier = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Auto_Insert_Supplier")
                Carrier_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Carrier_Index")
                Customer_Receive_Location_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Customer_Receive_Location_Index")
                Customer_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Customer_Index")
                Supplier_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Supplier_Index")
                Department_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Department_Index")
                DocumentType_Index_config = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "DocumentType_Index")
                Currency_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Currency_Index")
                PaymentMethod_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "PaymentMethod_Index")
                Discount_Percent = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "Discount_Percent")
                VAT_Percent = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "VAT_Percent")
                ItemStatus_Index = Objconfig_Import.GetDefault_Value(_DtConfig_Default_Import, "ItemStatus_Index")
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function InsertTemp(ByVal DtDataToInsertTemp As DataTable, ByVal strFileName As String, ByVal strGUid As String) As Boolean
        Try
            Dim objDB As New SaveDataSO
            objDB.DtTemp = DtDataToInsertTemp
            objDB.FileName = strFileName
            objDB.GUid = strGUid

            If objDB.InsertTemp() Then
                Return True
                'objDB.Status = 2 'จองเพื่อ  Insert Temp To PO
                'If objDB.UpdateStatus() Then
                'If Insert_PO(DtDataToInsertTemp) Then
                '    'Insert Complete
                'Else
                '    'Insert Error
                'End If
                'Else
                '    'Update Error
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetTmpDataUpdateStatus(ByVal strFileName As String, ByVal strGUid As String) As DataTable
        Try
            Dim objDB As New SaveDataSO
            objDB.FileName = strFileName
            objDB.GUid = strGUid

            Return objDB.GetTmpDataUpdateStatus(strGUid, strFileName)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Module
