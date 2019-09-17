Imports System.IO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Configuration.ConfigurationSettings

Public Class bl_Import_SO_Header : Inherits DBType_SQLServer
    Dim myReader As StreamReader
    Private _dataTable As DataTable = New DataTable
    Private _DataSource As New DataTable
    Private _FilePath As String = ""
    Private _SalesOrder_Index As String = ""
    Private _Invoice_No As String = ""
    Private _DO_No As String = ""
    Private _Do_Date As Date = Now
    Private _Expected_Delivery_Date As Date = Now
    Private _Customer_Shipping_No As String = ""
    Private _Customer_Shipping_Index As String = ""
    Private _Name1 As String = ""
    Private _Name2 As String = ""
    Private _Name4 As String = ""
    Private _Street As String = ""
    Private _City As String = ""
    Private _Distict As String = ""
    Private _PostCode As String = ""
    Private _Carrier As String = ""
    Private _DocumentType_Index As String = ""
    Private _Currency As String
    Private _Exchange_Rate As Double
    Private _Str8 As String = ""
    Private _Remark As String = ""
    Private _DistributionCenter As String = ""
    Private _Customer_Index As String = ""
    Private _Ref_No1 As String = ""
    Private _Ref_No2 As String = ""
    Private _EPSON_Location_Index As String = ""
    Private _Use_car As Integer
    Private _Str1 As String = ""

    Sub GetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            'ดึงจาก database ตาราง config_CustomSetting
            objCustomSetting.GetConfig_Value("DEFAULT_CUSTOMER_INDEX", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.CustomerIndex = objDT.Rows(0).Item("Config_Value").ToString
            End If


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

    Sub SetDEFAULT_DOCUMENTTYPE_SO()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Try

            objCustomSetting.GetConfig_Value("DEFAULT_IMPORT_DOCUMENTTYPE_SO_SHARP", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.DocumentTypeIndex = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DocumentTypeIndex = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    Sub SetDEFAULT_CURRENCY()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Try

            objCustomSetting.GetConfig_Value("DEFAULT_CURRENCY", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.Currency = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.Currency = ""
            End If
            SetExchangeRate(Me.Currency)
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    Public Sub SetExchangeRate(ByVal pstrCurrency As String)
        Try
            Dim strSQL As String
            strSQL = "  SELECT * FROM svms_Currency   "
            strSQL &= " WHERE Currency_Index='" & pstrCurrency & "'   "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            If _dataTable.Rows.Count > 0 Then
                Me.ExchangeRate = _dataTable.Rows(0)("ExRate")

            Else
                Me.ExchangeRate = 0.0
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub SetDEFAULT_DISTRIBUTION_CENTER()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Try

            objCustomSetting.GetConfig_Value("DEFAULT_DISTRIBUTION_CENTER_INDEX", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.DistributionCenter = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DistributionCenter = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub SetDEFAULT_SHARP_IMPORT_PATH()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            'ดึงจาก database ตาราง config_CustomSetting
            objCustomSetting.GetConfig_Value("DEFAULT_IMPORT_PATH", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.FilePath = objDT.Rows(0).Item("Config_Value").ToString
            End If


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
#Region "property"

    Public Property Use_car() As Integer
        Get
            Return _Use_car
        End Get
        Set(ByVal Value As Integer)
            _Use_car = Value
        End Set
    End Property

    Public Property CustomerIndex() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
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

    Public Property DistributionCenter() As String
        Get
            Return _DistributionCenter
        End Get
        Set(ByVal value As String)
            _DistributionCenter = value
        End Set
    End Property

    Public Property Str8() As String
        Get
            Return _Str8
        End Get
        Set(ByVal value As String)
            _Str8 = value
        End Set
    End Property

    Public Property Str1() As String
        Get
            Return _Str1
        End Get
        Set(ByVal value As String)
            _Str1 = value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property
    Public Property ExchangeRate() As Double
        Get
            Return _Exchange_Rate
        End Get
        Set(ByVal value As Double)
            _Exchange_Rate = value
        End Set
    End Property

    Public Property Currency() As String
        Get
            Return _Currency
        End Get
        Set(ByVal value As String)
            _Currency = value
        End Set
    End Property

    Public Property DocumentTypeIndex() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Public Property CustomerShippingIndex() As String
        Get
            Return _Customer_Shipping_Index
        End Get
        Set(ByVal value As String)
            _Customer_Shipping_Index = value
        End Set
    End Property

    Public Property SalesOrderIndex() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal value As String)
            _SalesOrder_Index = value
        End Set
    End Property

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value
        End Set
    End Property

    Property FilePath() As String
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property

    Public Property InvoiceNo() As String
        Get
            Return _Invoice_No
        End Get
        Set(ByVal value As String)
            _Invoice_No = value
        End Set
    End Property

    Public Property DoNo() As String
        Get
            Return _DO_No
        End Get
        Set(ByVal value As String)
            _DO_No = value
        End Set
    End Property

    Public Property DoDate() As Date
        Get
            Return _Do_Date
        End Get
        Set(ByVal value As Date)
            _Do_Date = value
        End Set
    End Property
    Public Property Expected_Delivery_Date() As Date
        Get
            Return _Expected_Delivery_Date
        End Get
        Set(ByVal value As Date)
            _Expected_Delivery_Date = value
        End Set
    End Property
    Public Property CustomerShippingNo() As String
        Get
            Return _Customer_Shipping_No
        End Get
        Set(ByVal value As String)
            _Customer_Shipping_No = value
        End Set
    End Property

    Public Property Name1() As String
        Get
            Return _Name1
        End Get
        Set(ByVal value As String)
            _Name1 = value
        End Set
    End Property

    Public Property Name2() As String
        Get
            Return _Name2
        End Get
        Set(ByVal value As String)
            _Name2 = value
        End Set
    End Property

    Public Property Name4() As String
        Get
            Return _Name4
        End Get
        Set(ByVal value As String)
            _Name4 = value
        End Set
    End Property

    Public Property Street() As String
        Get
            Return _Street
        End Get
        Set(ByVal value As String)
            _Street = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return _City
        End Get
        Set(ByVal value As String)
            _City = value
        End Set
    End Property

    Public Property Distict() As String
        Get
            Return _Distict
        End Get
        Set(ByVal value As String)
            _Distict = value
        End Set
    End Property

    Public Property PostCode() As String
        Get
            Return _PostCode
        End Get
        Set(ByVal value As String)
            _PostCode = value
        End Set
    End Property

    Public Property Carrier() As String
        Get
            Return _Carrier
        End Get
        Set(ByVal value As String)
            _Carrier = value
        End Set
    End Property

    Private _Item_Seq As Integer = 1
    Public Property Item_Seq() As Integer
        Get
            Return _Item_Seq
        End Get
        Set(ByVal value As Integer)
            _Item_Seq = value
        End Set
    End Property
    Private _Str2 As String = ""
    Public Property Str2() As String
        Get
            Return _Str2
        End Get
        Set(ByVal value As String)
            _Str2 = value
        End Set
    End Property

    Public Property Ref_No1() As String
        Get
            Return _Ref_No1
        End Get
        Set(ByVal value As String)
            _Ref_No1 = value
        End Set
    End Property

    Public Property Ref_No2() As String
        Get
            Return _Ref_No2
        End Get
        Set(ByVal value As String)
            _Ref_No2 = value
        End Set
    End Property
#End Region


    Public Function GetSubRoute(ByVal pstrPostCode As String) As String
        Try
            Dim strSQL As String
            strSQL = "  SELECT * FROM ms_SubRoute_Postcode     "
            strSQL &= " WHERE Postcode= '" & pstrPostCode & "'   "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return _dataTable.Rows(0)("SubRoute_Index").ToString()
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function GetKPIDelivery_Customer_Route(ByVal pstrCustomer_Index As String, ByVal pstrRoute_Index As String) As DataTable
    '    Try
    '        Dim strSQL As String
    '        strSQL = "  SELECT * FROM ms_CustomerTransportKPI_Delivery     "
    '        strSQL &= " WHERE       Route_Index= '" & pstrRoute_Index & "'   "
    '        strSQL &= "         AND Customer_Index= '" & pstrCustomer_Index & "'   "
    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '        Return _dataTable
    '        'If _dataTable.Rows.Count > 0 Then
    '        '    Return _dataTable.Rows(0)("Route_Index").ToString()
    '        'Else
    '        '    Return ""
    '        'End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function AddHoliday(ByVal pdtStart_Date As Date, ByVal pdtEnd_Date As Date) As Integer
        Try
            Dim strSQL As String
            strSQL = "  SELECT * FROM ms_Holiday_Customer     "
            strSQL &= " WHERE Holiday_date Between '" & pdtStart_Date.ToString("yyyy/MM/dd") & "' And  '" & pdtEnd_Date.ToString("yyyy/MM/dd") & "'"
            strSQL &= " And Status_id = 1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable.Rows.Count

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetRoute(ByVal pstrRoute_Index As String) As DataTable
        Try
            Dim strSQL As String
            strSQL = "  SELECT * FROM ms_Route     "
            strSQL &= " WHERE Route_Index= '" & pstrRoute_Index & "'   "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetRoute_Index(ByVal pstrSubRoute_Index As String) As String
        Try
            Dim strSQL As String
            strSQL = "  SELECT * FROM ms_SubRoute     "
            strSQL &= " WHERE SubRoute_Index= '" & pstrSubRoute_Index & "'   "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return _dataTable.Rows(0)("Route_Index").ToString()
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetCustomerShipping_Index(ByVal pstrCustomerShipping_No As String, ByVal pstrCompany_Name As String, ByVal pstrContact_Person As String, ByVal pstrAddress As String, ByVal pstrPostCode As String) As String

        Try

            Dim strCustomerShipping_Index As String = ""

            'ค้นหา Customer Index จาก Customer No 
            Dim strSQL As String
            strSQL = "  SELECT * FROM ms_Customer_Shipping    "
            strSQL &= " WHERE str1= '" & pstrCustomerShipping_No & "'   "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


            'ถ้าเจอ Update ms_Customer_Shipping + ms_Customer_Shipping_Location ที่มี ID เดียวกัน (ms_Customer_Shipping = Str1  ,Customer_Shipping_Location = Customer_Shipping_Location_Id)
            If _dataTable.Rows.Count > 0 Then

                strCustomerShipping_Index = _dataTable.Rows(0)("Customer_Shipping_Index").ToString

                '25/08/2010
                'ถ้าเป็นรหัสลูกค้าที่ขึ้นต้นด้วย ตัวอักษร ให้เป็น Insert อย่างเดียว
                If IsNumeric(pstrCustomerShipping_No) = False Then

                    'Insert CustomerShipping
                    strCustomerShipping_Index = InsertCustomerShipping(pstrCustomerShipping_No, pstrCompany_Name, pstrContact_Person, pstrAddress, pstrPostCode)

                    'Insert CustomerShipping_Location
                    InsertCustomerShippingLocation(strCustomerShipping_Index, pstrCustomerShipping_No, pstrCompany_Name, pstrContact_Person, pstrAddress, pstrPostCode)

                Else
                    'Update CustomerShipping
                    UpdateCustomerShippingAndShippingLocation(strCustomerShipping_Index, pstrCustomerShipping_No, pstrCompany_Name, pstrContact_Person, pstrAddress, pstrPostCode)

                End If

            Else

                'ถ้าไม่เจอ Insert ms_Customer_Shipping + ms_Customer_Shipping_Location ตาม Field ที่มี (ถ้าไม่มี ให้ใช้ค่า Defualt )
                GetDEFAULT_CUSTOMER_INDEX()
                'Insert CustomerShipping
                strCustomerShipping_Index = InsertCustomerShipping(pstrCustomerShipping_No, pstrCompany_Name, pstrContact_Person, pstrAddress, pstrPostCode)

                'Insert CustomerShipping_Location
                InsertCustomerShippingLocation(strCustomerShipping_Index, pstrCustomerShipping_No, pstrCompany_Name, pstrContact_Person, pstrAddress, pstrPostCode)

            End If


            Return strCustomerShipping_Index

        Catch ex As Exception

            Throw ex

        End Try

    End Function

#Region "    Update & Insert   "

    Public Sub UpdateCustomerShippingAndShippingLocation(ByVal pstrCustomerShipping_Index As String, ByVal pstrCustomer_Shipping_No As String, ByVal pstrCustomer_Name As String, ByVal pstrContact_Person As String, ByVal pstrAddress As String, ByVal pstrPostCode As String)

        Dim strSQL As String = ""
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try


            connectDB()


            'Update ms_Customer_Shipping
            strSQL = "  Update ms_Customer_Shipping "
            strSQL &= " SET Company_Name = @Customer_Name , "
            strSQL &= " Contact_Person = @Contact_Person ,  "
            strSQL &= " Address =   @Address , "
            strSQL &= " PostCode = @PostCode ,"
            strSQL &= " update_by = @update_by,                 "
            strSQL &= " update_date = getdate(),               "
            strSQL &= " update_branch = @update_branch             "
            strSQL &= " WHERE Customer_Shipping_Index = @Customer_Shipping_Index "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Customer_Name", SqlDbType.NVarChar).Value = pstrCustomer_Name
                .Parameters.Add("@Contact_Person", SqlDbType.NVarChar).Value = pstrContact_Person
                .Parameters.Add("@Address", SqlDbType.NVarChar).Value = (pstrContact_Person & " " & pstrAddress).Trim
                .Parameters.Add("@PostCode", SqlDbType.NVarChar).Value = pstrPostCode
                ' .Parameters.Add("@Customer_No", SqlDbType.NVarChar).Value = pstrCustomer_Shipping_No

                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.NVarChar).Value = pstrCustomerShipping_Index

                .Parameters.Add("@update_by", SqlDbType.VarChar).Value = WV_UserName
                '.Parameters.Add("@update_date", SqlDbType.SmallDateTime).Value = Nothing
                .Parameters.Add("@update_branch", SqlDbType.Int).Value = WV_Branch_ID

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '#################################################
            '     Update ms_Customer_Shipping_Location
            '#################################################
            strSQL = "  Update ms_Customer_Shipping_Location "
            strSQL &= " SET Shipping_Location_name = @Customer_Name , "
            strSQL &= " Contact_Person1 = @Contact_Person ,  "
            strSQL &= " Address =   @Address , "
            strSQL &= " PostCode = @PostCode ,"
            strSQL &= " update_by = @update_by,                 "
            strSQL &= " update_date = getdate(),               "
            strSQL &= " update_branch = @update_branch             "
            strSQL &= " WHERE Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Customer_Name", SqlDbType.NVarChar).Value = pstrCustomer_Name
                .Parameters.Add("@Contact_Person", SqlDbType.NVarChar).Value = pstrContact_Person
                .Parameters.Add("@Address", SqlDbType.NVarChar).Value = (pstrContact_Person & " " & pstrAddress).Trim
                .Parameters.Add("@PostCode", SqlDbType.NVarChar).Value = pstrPostCode
                .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.NVarChar).Value = pstrCustomerShipping_Index
                .Parameters.Add("@update_by", SqlDbType.VarChar).Value = WV_UserName
                '.Parameters.Add("@update_date", SqlDbType.SmallDateTime).Value = Nothing
                .Parameters.Add("@update_branch", SqlDbType.Int).Value = WV_Branch_ID

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()

        Catch ex As Exception

            myTrans.Rollback()
            Throw ex

        Finally

            disconnectDB()

        End Try

    End Sub

    Public Function InsertCustomerShipping(ByVal pstrCustomer_No As String, ByVal pstrCompany_Name As String, ByVal pstrContact_Person As String, ByVal pstrAddress As String, ByVal pstrPostCode As String) As String

        Dim strCustomer_Shipping_Index As String = ""
        Dim objSy_AutoNumber As New Sy_AutoNumber
        Dim strSQL = ""
        'Dim strCustomerShipping(2) As String
        Try

            'gen ค่า Customer_Shipping_index
            strCustomer_Shipping_Index = objSy_AutoNumber.getSys_Value("Customer_Shipping_Index")
            'strCustomerShipping(0) = strCustomer_Shipping_Index
            'strCustomerShipping(1) = Me.CustomerIndex
            objSy_AutoNumber = Nothing

            strSQL = "   INSERT INTO ms_Customer_Shipping   (   "
            strSQL &= "  Customer_Shipping_Index,Customer_Index,Title,Company_Name,Address,District_Index   "
            strSQL &= " ,Province_Index,PostCode,Tel,Fax,Mobile,Email,Contact_Person,Contact_Person2,Contact_Person3,BarCode    "
            strSQL &= " ,Remark,Status,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_date,add_branch,Customer_Type_Index,status_id)"
            strSQL &= "  VALUES("
            strSQL &= "         @Customer_Shipping_Index,   "
            strSQL &= "         @Customer_Index,            "
            strSQL &= "         @Title,                     "
            strSQL &= "         @Company_Name,              "
            strSQL &= "         @Address,                   "
            strSQL &= "         @District_Index,            "
            strSQL &= "         @Province_Index,            "
            strSQL &= "         @PostCode,                  "
            strSQL &= "         @Tel,                       "
            strSQL &= "         @Fax,                       "
            strSQL &= "         @Mobile,                    "
            strSQL &= "         @Email,                     "
            strSQL &= "         @Contact_Person,            "
            strSQL &= "         @Contact_Person2,           "
            strSQL &= "         @Contact_Person3,           "
            strSQL &= "         @BarCode,                   "
            strSQL &= "         @Remark,                    "
            strSQL &= "         @Status,                    "
            strSQL &= "         @Str1,                      "
            strSQL &= "         @Str2,                      "
            strSQL &= "         @Str3,                      "
            strSQL &= "         @Str4,                      "
            strSQL &= "         @Str5,                      "
            strSQL &= "         @Str6,                      "
            strSQL &= "         @Str7,                      "
            strSQL &= "         @Str8,                      "
            strSQL &= "         @Str9,                      "
            strSQL &= "         @Str10,                     "
            strSQL &= "         @add_by,                    "
            strSQL &= "         getdate(),                  "
            strSQL &= "         @add_branch,                "
            strSQL &= "         @Customer_Type_Index,       "
            strSQL &= "         @status_id                  "
            strSQL &= "         )                           "


            With SQLServerCommand
                .Parameters.Clear()
                ' Customer_Shipping_Index
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar).Value = strCustomer_Shipping_Index

                'Customer_Index
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar).Value = Me.CustomerIndex

                'Title
                .Parameters.Add("@Title", SqlDbType.NVarChar).Value = ""

                'Company_Name
                .Parameters.Add("@Company_Name", SqlDbType.NVarChar).Value = pstrCompany_Name

                'Address
                .Parameters.Add("@Address", SqlDbType.NVarChar).Value = (pstrContact_Person & " " & pstrAddress).Trim

                .Parameters.Add("@District_Index", SqlDbType.VarChar).Value = ""
                .Parameters.Add("@Province_Index", SqlDbType.VarChar).Value = ""

                'PostCode
                .Parameters.Add("@PostCode", SqlDbType.NVarChar).Value = pstrPostCode

                'Tel
                .Parameters.Add("@Tel", SqlDbType.NVarChar).Value = ""
                'Fax
                .Parameters.Add("@Fax", SqlDbType.NVarChar).Value = ""
                'Mobile
                .Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = ""
                'Email
                .Parameters.Add("@Email", SqlDbType.NVarChar).Value = ""

                'Contact_Person
                .Parameters.Add("@Contact_Person", SqlDbType.NVarChar).Value = pstrContact_Person
                'Contact_Person2
                .Parameters.Add("@Contact_Person2", SqlDbType.NVarChar).Value = ""
                'Contact_Person3
                .Parameters.Add("@Contact_Person3", SqlDbType.NVarChar).Value = ""

                .Parameters.Add("@BarCode", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Remark", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Status", SqlDbType.Int).Value = 1
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = pstrCustomer_No
                .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str3", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str4", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str5", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str6", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str7", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str8", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str9", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@Customer_Type_Index", SqlDbType.VarChar).Value = ""
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 1

            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
            Return strCustomer_Shipping_Index
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Sub InsertCustomerShippingLocation(ByVal pstrCustomer_Shipping_Index As String, ByVal pstrCustomer_Shipping_No As String, ByVal pstrCustomer_Name As String, ByVal pstrContact_Person As String, ByVal pstrAddress As String, ByVal pstrPostCode As String)

        Dim strCustomer_Shipping_Location_Index As String = ""
        Dim objSy_AutoNumber As New Sy_AutoNumber
        Dim strSQL = ""

        Try

            'gen ค่า Customer_Shipping_index
            ' strCustomer_Shipping_Location_Index = objSy_AutoNumber.getSys_Value("Customer_Shipping_Location_Index")

            strCustomer_Shipping_Location_Index = pstrCustomer_Shipping_Index

            strSQL = "   INSERT INTO ms_Customer_Shipping_Location(Customer_Shipping_Location_Index,Customer_Shipping_Location_Id,Route_Index,Customer_Shipping_Index   "
            strSQL &= "  ,Shipping_Location_Name,Address,District_Index,Province_Index,PostCode,Tel,Fax,Mobile,Email,Contact_Person1,Contact_Person2,Contact_Person3,Remark "
            strSQL &= " ,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_date,add_branch,status_id)   "

            strSQL &= "  VALUES("
            strSQL &= "         @Customer_Shipping_Location_Index,  "
            strSQL &= "         @Customer_Shipping_Location_Id,     "
            strSQL &= "         @Route_Index,               "
            strSQL &= "         @Customer_Shipping_Index,   "
            strSQL &= "         @Shipping_Location_Name,    "
            strSQL &= "         @Address,                   "
            strSQL &= "         @District_Index,            "
            strSQL &= "         @Province_Index,            "
            strSQL &= "         @PostCode,                  "
            strSQL &= "         @Tel,                       "
            strSQL &= "         @Fax,                       "
            strSQL &= "         @Mobile,                    "
            strSQL &= "         @Email,                     "
            strSQL &= "         @Contact_Person1,           "
            strSQL &= "         @Contact_Person2,           "
            strSQL &= "         @Contact_Person3,           "
            strSQL &= "         @Remark,                    "
            strSQL &= "         @Str1,                      "
            strSQL &= "         @Str2,                      "
            strSQL &= "         @Str3,                      "
            strSQL &= "         @Str4,                      "
            strSQL &= "         @Str5,                      "
            strSQL &= "         @Str6,                      "
            strSQL &= "         @Str7,                      "
            strSQL &= "         @Str8,                      "
            strSQL &= "         @Str9,                      "
            strSQL &= "         @Str10,                     "
            strSQL &= "         @add_by,                    "
            strSQL &= "         getdate(),                  "
            strSQL &= "         @add_branch,                "
            strSQL &= "         @status_id                 "
            strSQL &= "         )                           "


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar).Value = strCustomer_Shipping_Location_Index
                .Parameters.Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar).Value = pstrCustomer_Shipping_No
                .Parameters.Add("@Route_Index", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar).Value = pstrCustomer_Shipping_Index
                .Parameters.Add("@Shipping_Location_Name", SqlDbType.NVarChar).Value = pstrCustomer_Name
                .Parameters.Add("@Address", SqlDbType.VarChar).Value = (pstrContact_Person & " " & pstrAddress).Trim
                .Parameters.Add("@District_Index", SqlDbType.VarChar).Value = ""
                .Parameters.Add("@Province_Index", SqlDbType.VarChar).Value = ""
                .Parameters.Add("@PostCode", SqlDbType.NVarChar).Value = pstrPostCode
                .Parameters.Add("@Tel", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Fax", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Mobile", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Email", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Contact_Person1", SqlDbType.NVarChar).Value = pstrContact_Person
                .Parameters.Add("@Contact_Person2", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Contact_Person3", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Remark", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = pstrCustomer_Shipping_No
                .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str3", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str4", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str5", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str6", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str7", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str8", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str9", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 1

            End With
            objSy_AutoNumber = Nothing
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    Public Sub SalesOrderInsert()

        Try

            'SetDEFAULT_DOCUMENTTYPE_SO()
            'SetDEFAULT_CURRENCY()
            SetDEFAULT_DISTRIBUTION_CENTER()
            'GetDEFAULT_CUSTOMER_INDEX()

            Dim otb_SalesOrder As New tb_SalesOrder
            With otb_SalesOrder
                'SalesOrder_Index 

                'Invoice_No 
                '.Str1 = Me.InvoiceNo.Trim
                '.Str2 = Me.Str2
                '.Ref_No2 = Me.Ref_No2

                'DO_Date
                .SalesOrder_Date = Me.DoDate.ToString("yyyy/MM/dd")

                'Customer_No
                .Customer_Index = Me.CustomerIndex
                .Customer_Shipping_Index = Me.CustomerShippingIndex
                '.Customer_Shipping_Location_Index = Me.CustomerShippingIndex

                'DO_No
                .SalesOrder_No = "" 'Me.DoNo.Trim

                Dim objSy_AutoNumber As New Sy_AutoNumber
                Me.SalesOrderIndex = objSy_AutoNumber.getSys_Value("SalesOrder_Index")
                objSy_AutoNumber = Nothing

                .SalesOrder_Index = Me.SalesOrderIndex
                .DocumentType_Index = Me.DocumentTypeIndex
                .Currency_Index = Me.Currency
                .Str8 = Me.Str8
                .DistributionCenter_Index = Me.DistributionCenter
                .Exchange_Rate = Me.ExchangeRate
                .Expected_Delivery_Date = Me.Expected_Delivery_Date.ToString("yyyy/MM/dd")
                .SubRoute_Index = GetSubRoute(Me.PostCode)
                .Route_Index = GetRoute_Index(.SubRoute_Index)
                .Remark = Me.Remark
                '.Epson_ProductGroup_Index = Me.EPSON_Location_Index
                '.Use_car = Me.Use_car
                .Str1 = Me.Str1

                .Insert_Import()

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

    Public Function CheckSalesOrder_No(ByVal pstrSalesOrder_No As String) As Boolean
        Try
            Dim strSQL = ""

            strSQL &= " SELECT *    "
            strSQL &= " FROM tb_SalesOrder  "
            strSQL &= " WHERE SalesOrder_No = '" & pstrSalesOrder_No & "' And Status <> -1 "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then

                Return True

            Else

                Return False

            End If

        Catch ex As Exception

            Throw ex

        End Try
    End Function


    Public Function CheckInvoice_No_TIMCO(ByVal pstrCustomerOrderNo As String, ByVal pstrCustomerIndex As String) As Boolean
        Try
            Dim strSQL = ""

            strSQL = " select * from tb_Salesorder where status <> -1 and Ref_No2 = '" & pstrCustomerOrderNo & "' and Customer_Index = '" & pstrCustomerIndex & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then

                Return True

            Else

                Return False

            End If

        Catch ex As Exception

            Throw ex

        End Try
    End Function

End Class

