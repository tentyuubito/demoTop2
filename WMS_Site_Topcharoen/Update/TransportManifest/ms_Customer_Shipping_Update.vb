'*** Create Date :  17/01/2008
'*** Create BY   :  Pui
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data
Imports System.Data.SqlClient


Public Class ms_Customer_Shipping_Update : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _customer_Shipping_Index As String = ""
    Private _customer_Index As String = ""
    Private _title As String = ""
    Private _company_Name As String = ""
    Private _customer_Type_Index As String = ""
    Private _address As String = ""
    Private _district_Index As String = ""
    Private _province_Index As String = ""
    Private _postcode As String = ""
    Private _tel As String = ""
    Private _fax As String = ""
    Private _mobile As String = ""
    Private _email As String = ""
    Private _contact_Person As String = ""
    Private _contact_Person2 As String = ""
    Private _contact_Person3 As String = ""
    Private _barcode As String = ""
    Private _remark As String = ""
    Private _status As Integer = 0
    Private _str1 As String = ""
    Private _str2 As String = ""
    Private _str3 As String = ""
    Private _str4 As String = ""
    Private _str5 As String = ""
    Private _str6 As String = ""
    Private _str7 As String = ""
    Private _str8 As String = ""
    Private _str9 As String = ""
    Private _str10 As String = ""
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer = 0
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer = 0
    Private _status_id As Integer = 0
    

#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum
#End Region

#Region " Properties Section "
    '*** Property Readonly
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property

    '*** Property Writeonly

    '*** Property Read and Write
    Public Property Customer_Shipping_Index() As String
        Get
            Return _customer_Shipping_Index
        End Get
        Set(ByVal Value As String)
            _customer_Shipping_Index = Value
        End Set
    End Property
    Public Property Customer_Index() As String
        Get
            Return _customer_Index
        End Get
        Set(ByVal Value As String)
            _customer_Index = Value
        End Set
    End Property
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(ByVal Value As String)
            _title = Value
        End Set
    End Property
    Public Property Company_Name() As String
        Get
            Return _company_Name
        End Get
        Set(ByVal Value As String)
            _company_Name = Value
        End Set
    End Property
    Public Property Customer_Type_Index() As String
        Get
            Return _customer_Type_Index
        End Get
        Set(ByVal Value As String)
            _customer_Type_Index = Value
        End Set
    End Property
    Public Property Address() As String
        Get
            Return _address
        End Get
        Set(ByVal Value As String)
            _address = Value
        End Set
    End Property
    Public Property District_Index() As String
        Get
            Return _district_Index
        End Get
        Set(ByVal Value As String)
            _district_Index = Value
        End Set
    End Property
    Public Property Province_Index() As String
        Get
            Return _province_Index
        End Get
        Set(ByVal Value As String)
            _province_Index = Value
        End Set
    End Property
    Public Property Postcode() As String
        Get
            Return _postcode
        End Get
        Set(ByVal Value As String)
            _postcode = Value
        End Set
    End Property
    Public Property Tel() As String
        Get
            Return _tel
        End Get
        Set(ByVal Value As String)
            _tel = Value
        End Set
    End Property
    Public Property Fax() As String
        Get
            Return _fax
        End Get
        Set(ByVal Value As String)
            _fax = Value
        End Set
    End Property
    Public Property Mobile() As String
        Get
            Return _mobile
        End Get
        Set(ByVal Value As String)
            _mobile = Value
        End Set
    End Property
    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal Value As String)
            _email = Value
        End Set
    End Property
    Public Property Contact_Person() As String
        Get
            Return _contact_Person
        End Get
        Set(ByVal Value As String)
            _contact_Person = Value
        End Set
    End Property
    Public Property Contact_Person2() As String
        Get
            Return _contact_Person2
        End Get
        Set(ByVal Value As String)
            _contact_Person2 = Value
        End Set
    End Property
    Public Property Contact_Person3() As String
        Get
            Return _contact_Person3
        End Get
        Set(ByVal Value As String)
            _contact_Person3 = Value
        End Set
    End Property
    Public Property Barcode() As String
        Get
            Return _barcode
        End Get
        Set(ByVal Value As String)
            _barcode = Value
        End Set
    End Property
    Public Property Remark() As String
        Get
            Return _remark
        End Get
        Set(ByVal Value As String)
            _remark = Value
        End Set
    End Property
    Public Property Str1() As String
        Get
            Return _str1
        End Get
        Set(ByVal Value As String)
            _str1 = Value
        End Set
    End Property
    Public Property Str2() As String
        Get
            Return _str2
        End Get
        Set(ByVal Value As String)
            _str2 = Value
        End Set
    End Property
    Public Property Str3() As String
        Get
            Return _str3
        End Get
        Set(ByVal Value As String)
            _str3 = Value
        End Set
    End Property
    Public Property Str4() As String
        Get
            Return _str4
        End Get
        Set(ByVal Value As String)
            _str4 = Value
        End Set
    End Property
    Public Property Str5() As String
        Get
            Return _str5
        End Get
        Set(ByVal Value As String)
            _str5 = Value
        End Set
    End Property
    Public Property Str6() As String
        Get
            Return _str6
        End Get
        Set(ByVal Value As String)
            _str6 = Value
        End Set
    End Property
    Public Property Str7() As String
        Get
            Return _str7
        End Get
        Set(ByVal Value As String)
            _str7 = Value
        End Set
    End Property
    Public Property Str8() As String
        Get
            Return _str8
        End Get
        Set(ByVal Value As String)
            _str8 = Value
        End Set
    End Property
    Public Property Str9() As String
        Get
            Return _str9
        End Get
        Set(ByVal Value As String)
            _str9 = Value
        End Set
    End Property
    Public Property Str10() As String
        Get
            Return _str10
        End Get
        Set(ByVal Value As String)
            _str10 = Value
        End Set
    End Property
    Public Property Add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property
    Public Property Add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property
    Public Property Add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property
    Public Property Update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property
    Public Property Update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property
    Public Property Update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property
    Public Property Status_id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal Value As Integer)
            _status_id = Value
        End Set
    End Property

    Private _Route_Index As String = ""
    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal value As String)
            _Route_Index = value
        End Set
    End Property

    Private _SubRoute_Index As String = ""
    Public Property SubRoute_Index() As String
        Get
            Return _SubRoute_Index
        End Get
        Set(ByVal value As String)
            _SubRoute_Index = value
        End Set
    End Property

    Private _TransportRegion_Index As String = ""
    Public Property TransportRegion_Index() As String
        Get
            Return _TransportRegion_Index
        End Get
        Set(ByVal value As String)
            _TransportRegion_Index = value
        End Set
    End Property



#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "

    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
    End Sub




#End Region


    '*** Normal DB Method
#Region " SELECT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Public Sub ShowDataForMain(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT  Top 500 *"
            strSQL &= " FROM    VIEW_MS_Customer_Shipping_View"
            strSQL &= "  WHERE        status_id != -1 "

            strWhere = ""
            If Not pFilterValue = "" Then
                strWhere = pFilterValue
            Else


            End If

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

    Public Sub ShowDataForMain_location(ByVal ColumnName As String, ByVal pFilterValue As String, Optional ByVal all As Boolean = False)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = IIf(all, " SELECT * ", " SELECT  Top 500 *")
            strSQL &= " FROM    VIEW_MS_Cus_Ship_Location"
            strSQL &= "  WHERE        status_id != -1 "

            strWhere = ""
            If Not pFilterValue = "" Then
                strWhere = pFilterValue
            Else


            End If

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

    Public Sub SearchData_Click()
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Customer_Shipping where status_id != -1 "

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

    Public Sub SelectEditData_Click(ByVal Customer_Shipping_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = " SELECT     ms_Customer_Shipping.*, ms_Country.Country_Code AS Country_Code, ms_Country.Country_Name AS Country_Name, "
            strSQL &= "    ms_Country.Country_Name_Th AS Country_Name_Th, ms_Province.Province AS Province, ms_District.District AS District, "
            strSQL &= "    ms_Customer.Customer_Id AS Customer_Id, ms_Customer.Customer_Name AS Customer_Name"
            strSQL &= " FROM         ms_District RIGHT OUTER JOIN"
            strSQL &= "     ms_Customer RIGHT OUTER JOIN"
            strSQL &= "  ms_Customer_Shipping ON ms_Customer.Customer_Index = ms_Customer_Shipping.Customer_Index LEFT OUTER JOIN"
            strSQL &= "  ms_Province ON ms_Customer_Shipping.Province_Index = ms_Province.Province_Index ON "
            strSQL &= "   ms_District.District_Index = ms_Customer_Shipping.District_Index LEFT OUTER JOIN"
            strSQL &= "  ms_Country ON ms_Customer_Shipping.Str3 = ms_Country.Country_Index"
            strSQL &= "  WHERE       ms_Customer_Shipping.status_id != -1 and ms_Customer_Shipping.Customer_Shipping_Index in ('" & Customer_Shipping_Index & "','0')"

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

    Public Sub SelectQCEditData_Click(ByVal Customer_index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Customer_Shipping where status_id != -1" & _
                     " and Customer_index in ('" & Customer_index & "','0')"


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

    Public Sub SelectFor_EditData_Click(ByVal Customer_Shipping_index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = " SELECT       ms_Customer_Shipping.*, ms_Country.Country_Code AS Country_Code, ms_Country.Country_Name AS Country_Name, "
            strSQL &= "             ms_Country.Country_Name_Th AS Country_Name_Th, ms_Province.Province AS Province, ms_District.District AS District"
            strSQL &= " FROM         ms_Country RIGHT OUTER JOIN"
            strSQL &= "             ms_Province RIGHT OUTER JOIN"
            strSQL &= "             ms_Customer_Shipping ON ms_Province.Province_Index = ms_Customer_Shipping.Province_Index LEFT OUTER JOIN"
            strSQL &= "             ms_District ON ms_Customer_Shipping.District_Index = ms_District.District_Index ON "
            strSQL &= "             ms_Country.Country_Index = ms_Customer_Shipping.Str3"
            strSQL &= " WHERE       ms_Customer_Shipping.status_id != -1 and ms_Customer_Shipping.Customer_Shipping_index = '" & Customer_Shipping_index & "' "

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

    Public Sub SelectFor_EditData__SearchID(ByVal Customer_Shipping_ID As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = " SELECT       ms_Customer_Shipping.*, ms_Country.Country_Code AS Country_Code, ms_Country.Country_Name AS Country_Name, "
            strSQL &= "             ms_Country.Country_Name_Th AS Country_Name_Th, ms_Province.Province AS Province, ms_District.District AS District"
            strSQL &= " FROM         ms_Country RIGHT OUTER JOIN"
            strSQL &= "             ms_Province RIGHT OUTER JOIN"
            strSQL &= "             ms_Customer_Shipping ON ms_Province.Province_Index = ms_Customer_Shipping.Province_Index LEFT OUTER JOIN"
            strSQL &= "             ms_District ON ms_Customer_Shipping.District_Index = ms_District.District_Index ON "
            strSQL &= "             ms_Country.Country_Index = ms_Customer_Shipping.Str3"
            strSQL &= " WHERE       ms_Customer_Shipping.status_id != -1 and ms_Customer_Shipping.Str1 = '" & Customer_Shipping_ID & "' "

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

    Public Sub GetAllAsDataTable()
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     * " & _
             " FROM ms_Customer_Shipping " & _
             " WHERE     status_id <> -1 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex 'ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Sub SelectByIndex(ByVal index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Customer_Shipping where status_id != -1" & _
                     " and Customer_Shipping_Index in ('" & index & "')"


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

    Public Sub SelectCustomer_Shipping_ByID(ByVal Customer_Shipping_ID As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Customer_Shipping where status_id != -1" & _
                     " and str1 in ('" & Customer_Shipping_ID & "')"


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


    Public Sub SelectDataEditToCustomerShipping(ByVal Customer_index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     ms_Province.Province AS Province, ms_District.District AS District, ms_Customer_Shipping.*"
            strSQL &= "   FROM         ms_District RIGHT OUTER JOIN"
            strSQL &= "        ms_Customer_Shipping ON ms_District.District_Index = ms_Customer_Shipping.District_Index LEFT OUTER JOIN"
            strSQL &= "   ms_Province ON ms_Customer_Shipping.Province_Index = ms_Province.Province_Index"
            strSQL &= "   WHERE       ms_Customer_Shipping.status_id != -1 and ms_Customer_Shipping.Customer_index  ='" & Customer_index & "'"

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

#End Region

#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function Insert_Master() As Boolean
        Dim strSQL As String

        Try

            If Trim(_str1) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _str1 = objDocumentNumber.getSys_ID("customer_Shipping_Id")

                objDocumentNumber = Nothing

            End If

            strSQL = " insert into ms_Customer_Shipping (customer_Shipping_Index,customer_Index,title,company_Name,customer_Type_Index,address,district_Index,province_Index,postcode,tel,fax,mobile,email,contact_Person,contact_Person2,contact_Person3,barcode,remark,add_by,add_date,add_branch,str3,str4,str5,str6,str1,Route_Index,SubRoute_Index,TransportRegion_Index) "
            strSQL &= " values (@Customer_Shipping_Index,@Customer_Index,@Title,@Company_Name,@Customer_Type_Index,@Address,@District_Index,@Province_Index,@Postcode,@Tel,@Fax,@Mobile,@Email,@Contact_Person,@Contact_Person2,@Contact_Person3,@Barcode,@Remark,@add_by,getdate(),@add_branch,@str3,@str4,@str5,@str6,@str1,@Route_Index,@SubRoute_Index,@TransportRegion_Index)"


            strSQL = strSQL
            With SQLServerCommand
                'gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@customer_Shipping_Index", SqlDbType.VarChar, 30).Value = Me._customer_Shipping_Index
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 30).Value = Me._customer_Index
                .Parameters.Add("@title", SqlDbType.VarChar, 30).Value = Me._title
                .Parameters.Add("@Company_Name", SqlDbType.VarChar, 255).Value = Me._company_Name
                .Parameters.Add("@Customer_Type_Index", SqlDbType.VarChar, 30).Value = Me._customer_Type_Index
                .Parameters.Add("@address", SqlDbType.VarChar, 255).Value = Me._address
                .Parameters.Add("@district_Index", SqlDbType.VarChar, 30).Value = Me._district_Index
                .Parameters.Add("@province_Index", SqlDbType.VarChar, 30).Value = Me._province_Index
                .Parameters.Add("@postcode", SqlDbType.VarChar, 30).Value = Me._postcode
                .Parameters.Add("@tel", SqlDbType.VarChar, 30).Value = Me._tel
                .Parameters.Add("@fax", SqlDbType.VarChar, 30).Value = Me._fax
                .Parameters.Add("@mobile", SqlDbType.VarChar, 30).Value = Me._mobile
                .Parameters.Add("@email", SqlDbType.VarChar, 30).Value = Me._email
                .Parameters.Add("@contact_Person", SqlDbType.VarChar, 30).Value = Me._contact_Person
                .Parameters.Add("@contact_Person2", SqlDbType.VarChar, 30).Value = Me._contact_Person2
                .Parameters.Add("@contact_Person3", SqlDbType.VarChar, 30).Value = Me._contact_Person3
                .Parameters.Add("@barcode", SqlDbType.VarChar, 30).Value = Me._barcode
                .Parameters.Add("@remark", SqlDbType.VarChar, 30).Value = Me._remark
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                .Parameters.Add("@Str3", SqlDbType.VarChar, 30).Value = Me._str3
                .Parameters.Add("@Str4", SqlDbType.VarChar, 30).Value = Me._str4
                .Parameters.Add("@Str5", SqlDbType.VarChar, 30).Value = Me._str5
                .Parameters.Add("@Str6", SqlDbType.VarChar, 30).Value = Me._str6
                .Parameters.Add("@Str1", SqlDbType.VarChar, 30).Value = Me._str1

                .Parameters.Add("@Route_Index", SqlDbType.VarChar, 30).Value = Me._Route_Index
                .Parameters.Add("@SubRoute_Index", SqlDbType.VarChar, 30).Value = Me._SubRoute_Index
                .Parameters.Add("@TransportRegion_Index", SqlDbType.VarChar, 30).Value = Me._TransportRegion_Index
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function

    Private Function Insert_QCMaster() As Boolean
        Dim strSQL As String

        Try

            strSQL = " insert into ms_Customer_Shipping (customer_Shipping_Index,customer_Index,title,company_Name,customer_Type_Index,address,district_Index,province_Index,postcode,tel,fax,mobile,email,contact_Person,contact_Person2,contact_Person3,barcode,remark,add_by,add_date,add_branch,status_id) "
            strSQL += " values (@Customer_Shipping_Index,@Customer_Index,@Title,@Company_Name,@Customer_Type_Index,@Address,@District_Index,@Province_Index,@Postcode,@Tel,@Fax,@Mobile,@Email,@Contact_Person,@Contact_Person2,@Contact_Person3,@Barcode,@Remark,@add_by,getdate(),@add_branch,@status_id)"


            strSQL = strSQL
            With SQLServerCommand
                'gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@customer_Shipping_Index", SqlDbType.VarChar, 30).Value = Me._customer_Shipping_Index
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 30).Value = Me._customer_Index
                .Parameters.Add("@title", SqlDbType.VarChar, 30).Value = Me._title
                .Parameters.Add("@Company_Name", SqlDbType.VarChar, 30).Value = Me._company_Name
                .Parameters.Add("@Customer_Type_Index", SqlDbType.VarChar, 30).Value = Me._customer_Type_Index
                .Parameters.Add("@address", SqlDbType.VarChar, 30).Value = Me._address
                .Parameters.Add("@district_Index", SqlDbType.Int, 30).Value = Me._district_Index
                .Parameters.Add("@province_Index", SqlDbType.Int, 30).Value = Me._province_Index
                .Parameters.Add("@postcode", SqlDbType.VarChar, 30).Value = Me._postcode
                .Parameters.Add("@tel", SqlDbType.VarChar, 30).Value = Me._tel
                .Parameters.Add("@fax", SqlDbType.VarChar, 30).Value = Me._fax
                .Parameters.Add("@mobile", SqlDbType.VarChar, 30).Value = Me._mobile
                .Parameters.Add("@email", SqlDbType.VarChar, 30).Value = Me._email
                .Parameters.Add("@contact_Person", SqlDbType.VarChar, 30).Value = Me._contact_Person
                .Parameters.Add("@contact_Person2", SqlDbType.VarChar, 30).Value = Me._contact_Person2
                .Parameters.Add("@contact_Person3", SqlDbType.VarChar, 30).Value = Me._contact_Person3
                .Parameters.Add("@barcode", SqlDbType.VarChar, 30).Value = Me._barcode
                .Parameters.Add("@remark", SqlDbType.VarChar, 30).Value = Me._remark

                .Parameters.Add("@status", SqlDbType.VarChar, 30).Value = Me._status
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_User_Index
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int, 4).Value = Status_id
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function


    Public Function InsertCustomer_Shipping_CusRefId(ByVal pCustomer_Shipping_Index As String _
        , ByVal pCustomer_Index As String _
        , ByVal pRefId As String _
        , ByVal pStr1 As String, ByVal pStr2 As String, ByVal pStr3 As String, ByVal pStr4 As String, ByVal pStr5 As String _
        , ByVal pFlo1 As Double, ByVal pFlo2 As Double, ByVal pFlo3 As Double, ByVal pFlo4 As Double, ByVal pFlo5 As Double) As Boolean

        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            If isExistID_CusRefId(pCustomer_Index, pCustomer_Shipping_Index) Then

                strSQL = " UPDATE ms_Customer_Shipping_CusRefId "
                strSQL &= " SET  Customer_Index=@Customer_Index,RefId=@RefId "
                strSQL &= " ,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5"
                strSQL &= " ,Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5"
                strSQL &= " ,status_id=@status_id"
                strSQL &= " WHERE Customer_Shipping_Index=@Customer_Shipping_Index "
                strSQL &= " AND  Customer_Index=@Customer_Index "

            Else
                strSQL = " insert into ms_Customer_Shipping_CusRefId (Customer_Shipping_Index,Customer_Index,RefId "
                strSQL &= " ,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5"
                strSQL &= " ,status_id)"
                strSQL &= " values (@Customer_Shipping_Index,@Customer_Index,@RefId"
                strSQL &= " ,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5"
                strSQL &= " ,@status_id)"

                If pRefId = "" Then Exit Function

            End If


            With SQLServerCommand.Parameters
                'gSB_GetDBServerDateTime()
                .Clear()
                .Add("@customer_Shipping_Index", SqlDbType.VarChar, 30).Value = pCustomer_Shipping_Index
                .Add("@Customer_Index", SqlDbType.VarChar, 30).Value = pCustomer_Index

                If pRefId = "" Then
                    .Add("@RefId", SqlDbType.NVarChar, 100).Value = DBNull.Value
                Else
                    .Add("@RefId", SqlDbType.NVarChar, 100).Value = pRefId
                End If

                .Add("@Str1", SqlDbType.NVarChar, 100).Value = pStr1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = pStr2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = pStr3
                .Add("@Str4", SqlDbType.NVarChar, 100).Value = pStr4
                .Add("@Str5", SqlDbType.NVarChar, 100).Value = pStr5
                .Add("@Flo1", SqlDbType.Float, 15).Value = pFlo1
                .Add("@Flo2", SqlDbType.Float, 15).Value = pFlo2
                .Add("@Flo3", SqlDbType.Float, 15).Value = pFlo3
                .Add("@Flo4", SqlDbType.Float, 15).Value = pFlo4
                .Add("@Flo5", SqlDbType.Float, 15).Value = pFlo5
                .Add("@status_id", SqlDbType.Int, 4).Value = 1

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            '   connectDB()
            EXEC_Command()


            '*** Commit transaction
            myTrans.Commit()

            Return True

        Catch ex As Exception

            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function



#End Region

#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Private Function Update_Master() As Boolean
        Dim strSQL As String


        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            strSQL = "update ms_Customer_Shipping set "

            strSQL &= "title='" & Me._title.Replace("'", "''").Trim & "'"
            strSQL &= ",Company_Name='" & Me._company_Name.Replace("'", "''").Trim & "'"
            strSQL &= ",Customer_Type_Index='" & Me._customer_Type_Index & "'"
            strSQL &= ",address='" & Me._address.Replace("'", "''").Trim & "'"

            strSQL &= ",district_Index='" & Me._district_Index & "'"
            strSQL &= ",province_Index='" & Me._province_Index & "'"

            strSQL &= ",postcode='" & Me._postcode.Replace("'", "''").Trim & "'"
            strSQL &= ",tel ='" & Me._tel.Replace("'", "''").Trim & "'"
            strSQL &= ",fax='" & Me._fax.Replace("'", "''").Trim & "'"

            strSQL &= ",mobile='" & Me._mobile.Replace("'", "''").Trim & "'"
            strSQL &= ",email='" & Me._email.Replace("'", "''").Trim & "'"
            strSQL &= ",contact_Person='" & Me._contact_Person.Replace("'", "''").Trim & "'"
            strSQL &= ",contact_Person2='" & Me._contact_Person2.Replace("'", "''").Trim & "'"
            strSQL &= ",contact_Person3='" & Me._contact_Person3.Replace("'", "''").Trim & "'"
            strSQL &= ",barcode='" & Me._barcode.Replace("'", "''").Trim & "'"
            strSQL &= ",Remark='" & Me._remark.Replace("'", "''").Trim & "'"


            strSQL &= ",Update_by = '" & WV_UserName & "'"
            strSQL &= ",update_date=getdate()"
            strSQL &= ",update_branch= " & WV_Branch_ID

            strSQL &= ",str3 = '" & Me._str3 & "'"
            strSQL &= ",str4 = '" & Me._str4 & "'"
            strSQL &= ",str5 = '" & Me._str5 & "'"
            strSQL &= ",str6 = '" & Me._str6 & "'"

            strSQL &= ",Route_Index = '" & Me._Route_Index & "'"
            strSQL &= ",SubRoute_Index = '" & Me._SubRoute_Index & "'"
            strSQL &= ",TransportRegion_Index = '" & Me._TransportRegion_Index & "'"

            strSQL &= " WHERE Customer_Shipping_Index='" & Me._customer_Shipping_Index & "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'connectDB()
            EXEC_Command()


            'Update By Art Date 05-09-2012
            'Update Detail เพิ่มการ Update เขตพ.ท.จัดส่ง ใน SalesOrder Status ที่ยังไม่ได้จัดส่ง

            strSQL = "  UPDATE tb_SalesOrder SET "

            strSQL &= " Route_Index = '" & Me._Route_Index & "'"
            strSQL &= " ,SubRoute_Index = '" & Me._SubRoute_Index & "'"
            strSQL &= ",TransportRegion_Index = '" & Me._TransportRegion_Index & "'"

            strSQL &= "  WHERE Customer_Shipping_Index = '" & Me._customer_Shipping_Index & "'"

            strSQL &= " AND Status_Manifest  in (1,7)  "
            strSQL &= " AND Status in (1,2,3,6,7)"
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'connectDB()
            EXEC_Command()


            myTrans.Commit()
            Return True
        Catch ex As Exception
            Throw ex
            myTrans.Rollback()
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function Set_Status(ByVal status_id As Integer, ByVal Oldstatus_id As Integer, ByVal Customer_index As String) As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_Customer_Shipping set "
            strSQL &= " status_id='" & status_id & "' , Customer_index='" & Customer_index & "' "
            strSQL &= " WHERE status_id='" & Oldstatus_id & "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Sub Update()
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE ms_Customer_Shipping" & _
            " SET Customer_Shipping_Index=@Customer_Shipping_Index," & _
            "     Customer_Index=@Customer_Index," & _
            "     Title=@Title," & _
            "     Company_Name=@Company_Name," & _
            "     Address=@Address," & _
            "     District_Index=@District_Index," & _
            "     Province_Index=@Province_Index," & _
            "     Postcode=@Postcode," & _
            "     Tel=@Tel," & _
            "     Fax=@Fax," & _
            "     Mobile=@Mobile," & _
            "     Email=@Email," & _
            "     Contact_Person=@Contact_Person," & _
            "     Contact_Person2=@Contact_Person2," & _
            "     Contact_Person3=@Contact_Person3," & _
            "     Barcode=@Barcode," & _
            "     Remark=@Remark," & _
            "     Status=@Status," & _
            "     Str1=@Str1," & _
            "     Str2=@Str2," & _
            "     Str3=@Str3," & _
            "     Str4=@Str4," & _
            "     Str5=@Str5," & _
            "     Str6=@Str6," & _
            "     Str7=@Str7," & _
            "     Str8=@Str8," & _
            "     Str9=@Str9," & _
            "     Str10=@Str10," & _
            "     add_by=@add_by," & _
            "     add_date=@add_date," & _
            "     add_branch=@add_branch," & _
            "     update_by=@update_by," & _
            "     update_date=@update_date," & _
            "     update_branch=@update_branch," & _
            "     Customer_Type_Index=@Customer_Type_Index," & _
            "     status_id=@status_id " & _
            "           WHERE          Customer_Shipping_Index = @Customer_Shipping_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _customer_Shipping_Index
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _customer_Index
                .Add("@Title", SqlDbType.NVarChar, 50).Value = _title
                .Add("@Company_Name", SqlDbType.NVarChar, 100).Value = _company_Name
                .Add("@Address", SqlDbType.NVarChar, 255).Value = _address
                .Add("@District_Index", SqlDbType.VarChar, 10).Value = _district_Index
                .Add("@Province_Index", SqlDbType.VarChar, 10).Value = _province_Index
                .Add("@Postcode", SqlDbType.NVarChar, 100).Value = _postcode
                .Add("@Tel", SqlDbType.NVarChar, 100).Value = _tel
                .Add("@Fax", SqlDbType.NVarChar, 100).Value = _fax
                .Add("@Mobile", SqlDbType.NVarChar, 100).Value = _mobile
                .Add("@Email", SqlDbType.NVarChar, 100).Value = _email
                .Add("@Contact_Person", SqlDbType.NVarChar, 100).Value = _contact_Person
                .Add("@Contact_Person2", SqlDbType.NVarChar, 100).Value = _contact_Person2
                .Add("@Contact_Person3", SqlDbType.NVarChar, 100).Value = _contact_Person3
                .Add("@Barcode", SqlDbType.NVarChar, 100).Value = _barcode
                .Add("@Remark", SqlDbType.NVarChar, 255).Value = _remark
                .Add("@Status", SqlDbType.Int, 10).Value = _status
                .Add("@Str1", SqlDbType.NVarChar, 100).Value = _str1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = _str2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = _str3
                .Add("@Str4", SqlDbType.NVarChar, 100).Value = _str4
                .Add("@Str5", SqlDbType.NVarChar, 100).Value = _str5
                .Add("@Str6", SqlDbType.NVarChar, 100).Value = _str6
                .Add("@Str7", SqlDbType.NVarChar, 100).Value = _str7
                .Add("@Str8", SqlDbType.NVarChar, 100).Value = _str8
                .Add("@Str9", SqlDbType.NVarChar, 2000).Value = _str9
                .Add("@Str10", SqlDbType.NVarChar, 2000).Value = _str10
                .Add("@add_by", SqlDbType.VarChar, 50).Value = _add_by
                .Add("@add_date", SqlDbType.SmallDateTime, 16).Value = _add_date
                .Add("@add_branch", SqlDbType.Int, 10).Value = _add_branch
                .Add("@update_by", SqlDbType.VarChar, 50).Value = _update_by
                .Add("@update_date", SqlDbType.SmallDateTime, 16).Value = _update_date
                .Add("@update_branch", SqlDbType.Int, 10).Value = _update_branch
                .Add("@Customer_Type_Index", SqlDbType.VarChar, 13).Value = _customer_Type_Index
                .Add("@status_id", SqlDbType.Int, 10).Value = _status_id
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Private Function Update_QCMaster() As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_Customer_Shipping set "

            strSQL += "title='" & Me._title & "'"
            strSQL += ",Company_Name='" & Me._company_Name & "'"
            strSQL += ",Customer_Type_Index='" & Me._customer_Type_Index & "'"
            strSQL += ",address='" & Me._address & "'"
            strSQL += ",district_Index=" & Me._district_Index
            strSQL += ",province_Index=" & Me._province_Index
            strSQL += ",postcode='" & Me._postcode & "'"
            strSQL += ",tel ='" & Me._tel & "'"
            strSQL += ",fax='" & Me._fax & "'"

            strSQL += ",mobile='" & Me._mobile & "'"
            strSQL += ",email='" & Me._email & "'"
            strSQL += ",contact_Person='" & Me._contact_Person & "'"
            strSQL += ",contact_Person2='" & Me._contact_Person2 & "'"
            strSQL += ",contact_Person3='" & Me._contact_Person3 & "'"
            strSQL += ",barcode='" & Me._barcode & "'"
            strSQL += ",Remark='" & Me._remark & "'"


            strSQL += ",Update_by = '" & WV_User_Index & "'"
            strSQL += ",update_date=getdate()"
            strSQL += ",update_branch= " & WV_Branch_ID
            strSQL += " WHERE Customer_Shipping_Index='" + Me._customer_Shipping_Index + "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function Set_QCStatus(ByVal status_id As Integer, ByVal Oldstatus_id As Integer, ByVal Customer_index As String) As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_Customer_Shipping set "
            strSQL += " status_id='" & status_id & "' , Customer_index='" & Customer_index & "' "
            strSQL += " WHERE status_id='" & Oldstatus_id & "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Sub UpdateQC()
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE ms_Customer_Shipping" & _
            " SET Customer_Shipping_Index=@Customer_Shipping_Index," & _
            "     Customer_Index=@Customer_Index," & _
            "     Title=@Title," & _
            "     Company_Name=@Company_Name," & _
            "     Address=@Address," & _
            "     District_Index=@District_Index," & _
            "     Province_Index=@Province_Index," & _
            "     Postcode=@Postcode," & _
            "     Tel=@Tel," & _
            "     Fax=@Fax," & _
            "     Mobile=@Mobile," & _
            "     Email=@Email," & _
            "     Contact_Person=@Contact_Person," & _
            "     Contact_Person2=@Contact_Person2," & _
            "     Contact_Person3=@Contact_Person3," & _
            "     Barcode=@Barcode," & _
            "     Remark=@Remark," & _
            "     Status=@Status," & _
            "     Str1=@Str1," & _
            "     Str2=@Str2," & _
            "     Str3=@Str3," & _
            "     Str4=@Str4," & _
            "     Str5=@Str5," & _
            "     Str6=@Str6," & _
            "     Str7=@Str7," & _
            "     Str8=@Str8," & _
            "     Str9=@Str9," & _
            "     Str10=@Str10," & _
            "     add_by=@add_by," & _
            "     add_date=@add_date," & _
            "     add_branch=@add_branch," & _
            "     update_by=@update_by," & _
            "     update_date=@update_date," & _
            "     update_branch=@update_branch," & _
            "     Customer_Type_Index=@Customer_Type_Index," & _
            "     status_id=@status_id " & _
            "           WHERE          Customer_Shipping_Index = @Customer_Shipping_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _customer_Shipping_Index
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _customer_Index
                .Add("@Title", SqlDbType.NVarChar, 50).Value = _title
                .Add("@Company_Name", SqlDbType.NVarChar, 100).Value = _company_Name
                .Add("@Address", SqlDbType.NVarChar, 255).Value = _address
                .Add("@District_Index", SqlDbType.Int, 10).Value = _district_Index
                .Add("@Province_Index", SqlDbType.Int, 10).Value = _province_Index
                .Add("@Postcode", SqlDbType.NVarChar, 100).Value = _postcode
                .Add("@Tel", SqlDbType.NVarChar, 100).Value = _tel
                .Add("@Fax", SqlDbType.NVarChar, 100).Value = _fax
                .Add("@Mobile", SqlDbType.NVarChar, 100).Value = _mobile
                .Add("@Email", SqlDbType.NVarChar, 100).Value = _email
                .Add("@Contact_Person", SqlDbType.NVarChar, 100).Value = _contact_Person
                .Add("@Contact_Person2", SqlDbType.NVarChar, 100).Value = _contact_Person2
                .Add("@Contact_Person3", SqlDbType.NVarChar, 100).Value = _contact_Person3
                .Add("@Barcode", SqlDbType.NVarChar, 100).Value = _barcode
                .Add("@Remark", SqlDbType.NVarChar, 255).Value = _remark
                .Add("@Status", SqlDbType.Int, 10).Value = _status
                .Add("@Str1", SqlDbType.NVarChar, 100).Value = _str1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = _str2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = _str3
                .Add("@Str4", SqlDbType.NVarChar, 100).Value = _str4
                .Add("@Str5", SqlDbType.NVarChar, 100).Value = _str5
                .Add("@Str6", SqlDbType.NVarChar, 100).Value = _str6
                .Add("@Str7", SqlDbType.NVarChar, 100).Value = _str7
                .Add("@Str8", SqlDbType.NVarChar, 100).Value = _str8
                .Add("@Str9", SqlDbType.NVarChar, 2000).Value = _str9
                .Add("@Str10", SqlDbType.NVarChar, 2000).Value = _str10
                .Add("@add_by", SqlDbType.VarChar, 50).Value = _add_by
                .Add("@add_date", SqlDbType.SmallDateTime, 16).Value = _add_date
                .Add("@add_branch", SqlDbType.Int, 10).Value = _add_branch
                .Add("@update_by", SqlDbType.VarChar, 50).Value = _update_by
                .Add("@update_date", SqlDbType.SmallDateTime, 16).Value = _update_date
                .Add("@update_branch", SqlDbType.Int, 10).Value = _update_branch
                .Add("@Customer_Type_Index", SqlDbType.VarChar, 13).Value = _customer_Type_Index
                .Add("@status_id", SqlDbType.Int, 10).Value = _status_id
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex 'ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Function Update_Route_subRoute(ByVal pstrCustomer_Shipping_Location_Index As String, ByVal pstrRoute_index As String, ByVal pstrSubRoute_index As String, ByVal pstrTransportRegion_index As String) As Boolean
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE ms_Customer_Shipping_Location" & _
            " SET Route_Index=@Route_Index," & _
            "     SubRoute_Index=@SubRoute_Index," & _
            "     TransportRegion_Index=@TransportRegion_Index" & _
            "           WHERE          Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = pstrCustomer_Shipping_Location_Index
                .Add("@Route_Index", SqlDbType.VarChar, 13).Value = pstrRoute_index
                .Add("@SubRoute_Index", SqlDbType.VarChar, 13).Value = pstrSubRoute_index
                .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = pstrTransportRegion_index
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            'strSQL = " UPDATE    tb_SalesOrder " & _
            '    " SET Route_Index=@Route_Index," & _
            '    "     SubRoute_Index=@SubRoute_Index," & _
            '    "     TransportRegion_Index=@TransportRegion_Index " & _
            '    "           WHERE          isnuLL(SubRoute_Index,'') = '' AND   isnull(Route_Index,'')='-1'    "
            'With SQLServerCommand.Parameters
            '    .Clear()
            '    .Add("@Route_Index", SqlDbType.VarChar, 13).Value = pstrRoute_index
            '    .Add("@SubRoute_Index", SqlDbType.VarChar, 13).Value = pstrSubRoute_index
            '    .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = pstrTransportRegion_index
            'End With

            'best up date 06-09-22012   "     TransportRegion_Index=@TransportRegion_Index " & _
            strSQL = " UPDATE    tb_SalesOrder " & _
            " SET Route_Index=@Route_Index," & _
            "     SubRoute_Index=@SubRoute_Index," & _
            "     TransportRegion_Index=@TransportRegion_Index" & _
                "           WHERE Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Route_Index", SqlDbType.VarChar, 13).Value = pstrRoute_index
                .Add("@SubRoute_Index", SqlDbType.VarChar, 13).Value = pstrSubRoute_index
                .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = pstrTransportRegion_index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = pstrCustomer_Shipping_Location_Index
            End With
            'end best up date 06-09-2012
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
    End Function
#End Region

#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function Delete_Master(ByVal customer_Shipping_Index As String) As Boolean
        ' *** Define value from parameter
        Me._customer_Shipping_Index = customer_Shipping_Index

        Dim strSQL As String

        Try

            strSQL = "update ms_Customer_Shipping set status_id = -1"
            strSQL &= ",Update_by = '" + WV_UserName + "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= " WHERE Customer_Shipping_Index='" + Me._customer_Shipping_Index + "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function Delete_Shipping_CancelCustomer(ByVal _Customer_Index) As Boolean
        ' *** Define value from parameter
        Dim strSQL As String

        Try

            strSQL = "delete from ms_Customer_Shipping where status_id = 1 AND Customer_Index ='" & _Customer_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function Delete_ShippingZero() As Boolean
        ' *** Define value from parameter
        Me._customer_Shipping_Index = Customer_Shipping_Index

        Dim strSQL As String

        Try

            strSQL = "delete from ms_Customer_Shipping where status_id = 1"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Sub Delete()
        Dim strSQL As String = ""
        Try
            strSQL = " UPDATE ms_Customer_Shipping" & _
                      " SET Status_Id=-1" & _
                      " WHERE Customer_Shipping_Index='" & _customer_Shipping_Index & "'"
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Function Delete_QCMaster(ByVal customer_Shipping_Index As String) As Boolean
        ' *** Define value from parameter
        Me._customer_Shipping_Index = customer_Shipping_Index

        Dim strSQL As String

        Try

            strSQL = "update ms_Customer_Shipping set status_id = -1"
            strSQL += " WHERE Customer_Shipping_Index='" + Me._customer_Shipping_Index + "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function Delete_QCShippingZero() As Boolean
        ' *** Define value from parameter
        Me._customer_Shipping_Index = Customer_Shipping_Index

        Dim strSQL As String

        Try

            strSQL = "delete from ms_Customer_Shipping where status_id = 1"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True
        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Sub DeleteQC()
        Dim strSQL As String = ""
        Try
            strSQL = " UPDATE ms_Customer_Shipping" & _
                      " SET Status_Id=-1" & _
                      " WHERE Customer_Shipping_Index='" & _customer_Shipping_Index & "'"
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex ' ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

#End Region

#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Public Sub Checkcustomer_shippingANDlocation(ByVal CUS_ID As String, ByVal CUS_LO_ID As String)
        Dim strSQL As String = ""
        Try
            strSQL = " select COUNT(*) AS CHECKCUS from ms_Customer_Shipping CS "
            strSQL &= "  inner join ms_Customer_Shipping_Location CSL on CS.Customer_Shipping_Index = CSL.Customer_Shipping_Index"
            strSQL &= " where CS.Str1 = '" & CUS_ID & "' AND CSL.Customer_Shipping_Location_Id = '" & CUS_LO_ID & "'"
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


    Public Function isExistID_CusRefId(ByVal _Customer_Index As String, ByVal _Customer_Shipping_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "  SELECT  count(*) "
            strSQL &= " FROM    ms_Customer_Shipping_CusRefId "
            strSQL &= " WHERE   Customer_Index='" & _Customer_Index & "'"
            strSQL &= "         AND  Customer_Shipping_Index='" & _Customer_Shipping_Index & "'"


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
            SQLServerCommand.Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _Customer_Shipping_Index
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            ' connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function isExistID(ByVal _str1 As String, ByVal _Customer_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Customer_Shipping  where str1 = @_str1 AND Customer_Index='" & _Customer_Index & "'"

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@_str1", SqlDbType.VarChar, 20).Value = _str1
            SQLServerCommand.Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Private Function isQCExistID(ByVal _customer_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Customer_Shipping"

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@customer_Id", SqlDbType.VarChar, 20).Value = _customer_Id

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            _scalarOutput = "0"
            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "

    Public Function UpdateRountSubTrans(ByVal Customer_Shipping_Location_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " Update ms_Customer_Shipping_Location"
            strSQL &= " set Route_Index = '0010000000000' "
            strSQL &= " , SubRoute_Index = '' "
            strSQL &= " , TransportRegion_Index = '' "
            strSQL &= " where Customer_Shipping_Location_Index = '" & Customer_Shipping_Location_Index & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SaveData(ByVal _customer_Shipping_Index As String, ByVal _customer_Index As String, ByVal _title As String, ByVal _company_Name As String, ByVal _customer_Type_Index As String, ByVal _address As String, ByVal _district_Index As String, ByVal _province_Index As String, ByVal _postcode As String, ByVal _tel As String, ByVal _fax As String, ByVal _mobile As String, ByVal _email As String, ByVal _contact_Person As String, ByVal _contact_Person2 As String, ByVal _contact_Person3 As String, ByVal _barcode As String, ByVal _remark As String, ByVal _str1 As String, ByVal _str3 As String, ByVal _str4 As String, ByVal _str5 As String, ByVal _str6 As String) As Boolean

        ' ***  define value to field ***

        Me._customer_Shipping_Index = _customer_Shipping_Index
        Me._customer_Index = _customer_Index
        Me._title = _title
        Me._company_Name = _company_Name
        Me._customer_Type_Index = _customer_Type_Index
        Me._address = _address
        Me._district_Index = _district_Index
        Me._province_Index = _province_Index
        Me._postcode = _postcode
        Me._tel = _tel
        Me._fax = _fax
        Me._mobile = _mobile
        Me._email = _email
        Me._contact_Person = _contact_Person
        Me._contact_Person2 = _contact_Person2
        Me._contact_Person3 = _contact_Person3
        Me._barcode = _barcode
        Me._remark = _remark

        Me._str3 = _str3
        Me._str4 = _str4
        Me._str5 = _str5
        Me._str6 = _str6
        Me._str1 = _str1



        Try
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    ' get New Sys_Value 

                    Dim objDBIndex As New Sy_AutoNumber
                    Me._customer_Shipping_Index = objDBIndex.getSys_Value("customer_Shipping_Index")
                    objDBIndex = Nothing

                Case enuOperation_Type.UPDATE

                    ' Assign value from parameter
                    'Me._size_Id = _size_Index
                    Me._customer_Shipping_Index = _customer_Shipping_Index
            End Select



            ' *******************************


            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    ' Can Save data
                    If Me.Insert_Master = True Then

                        Return True
                        Exit Function
                    Else

                        Return False
                        Exit Function
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_Master = True Then
                        Return True
                        Exit Function
                    Else
                        Return False
                        Exit Function
                    End If


            End Select

        Catch ex As Exception

            Throw ex
        End Try


    End Function

    Public Function SaveQCData(ByVal customer_Shipping_Index As String, ByVal customer_Index As String, ByVal title As String, ByVal company_Name As String, ByVal customer_Type_Index As String, ByVal address As String, ByVal district_Index As String, ByVal province_Index As String, ByVal postcode As String, ByVal tel As String, ByVal fax As String, ByVal mobile As String, ByVal email As String, ByVal contact_Person As String, ByVal contact_Person2 As String, ByVal contact_Person3 As String, ByVal barcode As String, ByVal remark As String, ByVal status_id As Integer) As Boolean

        ' ***  define value to field ***

        Me._customer_Shipping_Index = customer_Shipping_Index
        Me._customer_Index = customer_Index
        Me._title = title
        Me._company_Name = company_Name
        Me._customer_Type_Index = customer_Type_Index
        Me._address = address
        Me._district_Index = district_Index
        Me._province_Index = province_Index
        Me._postcode = postcode
        Me._tel = tel
        Me._fax = fax
        Me._mobile = mobile
        Me._email = email
        Me._contact_Person = contact_Person
        Me._contact_Person2 = contact_Person2
        Me._contact_Person3 = contact_Person3
        Me._barcode = barcode
        Me._remark = remark
        Me._status_id = status_id

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._customer_Shipping_Index = objDBIndex.getSys_Value("customer_Shipping_Index")
                objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me._customer_Shipping_Index = _customer_Shipping_Index
        End Select



        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID("", "") = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        Exit Function
                    Else
                        ' Can Save data
                        If Me.Insert_Master = True Then
                            '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return True
                            Exit Function
                        Else
                            '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Return False
                            Exit Function
                        End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_Master = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        Exit Function
                    Else
                        'W_Language.W_MSG_Information("ไม่สามารถบันทึกข้อมูล")
                        'MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        ' Return False
                        Exit Function
                    End If

                    'Case enuOperation_Type.DELETE
                    '    ' **** check value some table if need !! 
                    '    If Me.DeleteSize_Master() = True Then
                    '        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return True
                    '        Exit Function

                    '    Else
                    '        MessageBox.Show("ไม่สามารถลบ", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    '        Return False
                    '        Exit Function
                    '    End If

            End Select

        Catch ex As Exception
            Return False
            Throw ex
        End Try


    End Function
#End Region

    Public Sub getPopup_CustomerShip(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" And pFilterValue = "" Then
                ' Select No Condition 
                strSQL = " SELECT   *" & _
                         " FROM       ms_Customer_Shipping where  Customer_Shipping_Index not in ('0') and status_id  not in (-1) "

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 
                strSQL = " SELECT     *" & _
                                        " FROM       ms_Customer_Shipping "

                strWhere = "WHERE  " & ColumnName & " ='" & pFilterValue & "' and customer_index not in ('0') and status_id not in (-1) "
            End If

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

    Public Sub getPopup_Search(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            ' Select No Condition 
            strSQL = " SELECT    CS.Customer_Shipping_Index, CS.Customer_Index , CS.Company_Name,CS.Address, C.Customer_ID,CS.Str1"
            strSQL &= " FROM  ms_Customer_Shipping  CS INNER JOIN ms_Customer C ON CS.Customer_Index = C.Customer_Index "
            strSQL &= " where  CS.Customer_Shipping_Index not in ('0') and CS.status_id  not in (-1) "


            SetSQLString = strSQL + WhereString

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getCustomer_Shipping_PopUp(ByVal pCustomer_Index As String, ByVal pCustomer_Shipping_Index As String, ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            '' Select No Condition 
            'strSQL &= " DECLARE @Customer_Shipping_Index	varchar(13)"
            'strSQL &= " DECLARE @Customer_Index				varchar(13)"
            'If pCustomer_Shipping_Index <> "" Then strSQL &= " SET @Customer_Shipping_Index = '" & pCustomer_Shipping_Index & "'"
            'If pCustomer_Index <> "" Then strSQL &= " SET @Customer_Index = '" & pCustomer_Index & "'"


            'strSQL &= "  SELECT      * FROM ( "
            'strSQL &= " SELECT		Null as Customer_Shipping_CusRefId"
            'strSQL &= " 			,Customer_Shipping_Index"
            'strSQL &= " 			,Company_Name as Customer_Shipping_Name"
            'strSQL &= " 			,str1 as  Customer_Shipping_Id"
            'strSQL &= " 			,Null as RefID"
            'strSQL &= " 			,str1 as RefID2"
            'strSQL &= " 			,Null as Customer_Id"
            'strSQL &= " 			,Null as Customer_Name"
            'strSQL &= " 			,Null as Customer_Index"
            'strSQL &= " FROM		ms_Customer_Shipping AS CSS "
            'strSQL &= " WHERE		Status_Id <> -1"
            'strSQL &= " 	AND      CSS.Customer_Shipping_Index not in (	select Customer_Shipping_Index "
            'strSQL &= " 												from ms_Customer_Shipping_CusRefId Where isnull(Status_Id,1) <> -1)"

            'If pCustomer_Shipping_Index <> "" Then strSQL &= " AND CSS.Customer_Shipping_Index = @Customer_Shipping_Index"
            'strSQL &= " UNION"
            'strSQL &= " SELECT     CSF.Customer_Shipping_CusRefId"
            'strSQL &= " 			, CSS.Customer_Shipping_Index"
            'strSQL &= " 			, CSS.Company_Name AS Customer_Shipping_Name"
            'strSQL &= " 			, CSS.Str1 AS Customer_Shipping_Id"
            'strSQL &= " 			, CSF.RefId"
            'strSQL &= " 			, ISNULL(CSF.RefId, CSS.Str1) AS RefID2"
            'strSQL &= " 			, CS.Customer_Id"
            'strSQL &= " 			, CS.Customer_Name"
            'strSQL &= " 			, CS.Customer_Index"
            'strSQL &= " FROM         ms_Customer AS CS INNER JOIN"
            'strSQL &= "                       ms_Customer_Shipping_CusRefId AS CSF ON CS.Customer_Index = CSF.Customer_Index INNER JOIN"
            'strSQL &= "                      ms_Customer_Shipping AS CSS ON CSF.Customer_Shipping_Index = CSS.Customer_Shipping_Index"
            'strSQL &= " WHERE		CSF.Status_Id <> -1  "
            'If pCustomer_Shipping_Index <> "" Then strSQL &= " AND CSS.Customer_Shipping_Index = @Customer_Shipping_Index"
            'If pCustomer_Index <> "" Then strSQL &= " AND CS.Customer_Index = @Customer_Index"
            'strSQL &= " )AS CSSF "
            'strSQL &= " WHERE 1=1 " & WhereString
            'strSQL &= " ORDER BY  Customer_Shipping_Id"


            'best up date 05-09-2012
            strSQL &= "select  ms_Customer_Shipping.Customer_Shipping_Index,ms_Customer_Shipping_Location.Customer_Shipping_Location_Index,ms_Customer_Shipping.str1 AS Customer_Shipping_Id,"
            strSQL &= "ms_Customer_Shipping.Company_Name AS Customer_Shipping_Name,ms_Customer_Shipping_Location.Address AS Customer_Shipping_Address"
            strSQL &= " from ms_Customer_Shipping inner join  ms_Customer_Shipping_Location ON"
            strSQL &= "(ms_Customer_Shipping.Customer_Shipping_Index = ms_Customer_Shipping_Location.Customer_Shipping_Index)"
            strSQL &= "where 1 = 1"
            If pCustomer_Index <> "" Then
                strSQL &= WhereString
            End If

            If pCustomer_Shipping_Index <> "" Then
                'Alert
            End If
            'end best up date 05-09-2012

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

    Public Sub getCustomer_Shipping_CusRefId(ByVal pCustomer_Index As String, ByVal pCustomer_Shipping_Index As String, ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            ' Select No Condition 
            strSQL = "  DECLARE @CusRef TABLE ("
            strSQL &= "             Customer_Shipping_CusRefId  varchar(50)"
            strSQL &= " 			,Customer_Shipping_Index	varchar(13)"
            strSQL &= " 			,Customer_Shipping_Name		varchar(500)"
            strSQL &= " 			,Customer_Shipping_Id		varchar(50)"
            strSQL &= " 			,RefID		varchar(50)"
            strSQL &= " 			,RefID2		varchar(50)"
            strSQL &= " 			,Customer_Id		varchar(50)"
            strSQL &= " 			,Customer_Name		varchar(500)"
            strSQL &= " 			,Customer_Index		varchar(13))"

            strSQL &= " DECLARE @Customer_Shipping_CusRefId	varchar(50)"
            strSQL &= " DECLARE @Customer_Shipping_Index	varchar(13)"
            strSQL &= " DECLARE @Customer_Shipping_Name		varchar(500)"
            strSQL &= " DECLARE @Customer_Shipping_Id		varchar(50)"
            strSQL &= " DECLARE @RefID						varchar(50)"
            strSQL &= " DECLARE @RefID2						varchar(50)"
            strSQL &= " DECLARE @Customer_Id				varchar(50)"
            strSQL &= " DECLARE @Customer_Name				varchar(500)"
            strSQL &= " DECLARE @Customer_Index				varchar(13)"
            strSQL &= " DECLARE @chkValue				varchar(500)"

            If pCustomer_Shipping_Index <> "" Then strSQL &= " SET @Customer_Shipping_Index = '" & pCustomer_Shipping_Index & "'"

            If pCustomer_Index <> "" Then strSQL &= " SET @Customer_Index = '" & pCustomer_Index & "'"



            strSQL &= " 	DECLARE CurCustomer_Shipping CURSOR FOR"
            strSQL &= " 	SELECT	Customer_Shipping_Index,Company_Name as Customer_Shipping_Name,Str1 as Customer_Shipping_Id"
            strSQL &= " 	FROM	ms_Customer_Shipping "
            strSQL &= " 	WHERE	Status_Id <> -1"

            If pCustomer_Shipping_Index <> "" Then strSQL &= " 	AND	Customer_Shipping_Index = @Customer_Shipping_Index "

            strSQL &= " 				OPEN CurCustomer_Shipping"
            strSQL &= " 			    FETCH NEXT FROM CurCustomer_Shipping"
            strSQL &= " 				INTO @Customer_Shipping_Index,@Customer_Shipping_Name,@Customer_Shipping_Id"
            strSQL &= " 				WHILE (@@FETCH_STATUS = 0) "
            strSQL &= " 				Begin"
            '---- BEGIN LOOP CUTOMER
            strSQL &= " 						DECLARE CurCustomer CURSOR FOR"
            strSQL &= " 						SELECT	Customer_Index,Customer_Id,Customer_Name"
            strSQL &= " 						FROM	ms_Customer"
            strSQL &= " 						WHERE	Status_Id <> -1"

            If pCustomer_Index <> "" Then strSQL &= " 	AND	Customer_Index = @Customer_Index "


            strSQL &= " 						OPEN CurCustomer"
            strSQL &= " 						FETCH NEXT FROM CurCustomer"
            strSQL &= " 						INTO @Customer_Index,@Customer_Id,@Customer_Name"
            strSQL &= " 						WHILE (@@FETCH_STATUS = 0) "
            strSQL &= " 						Begin"
            strSQL &= " 							SET @chkValue = (select	top 1 Customer_Shipping_CusRefId "
            strSQL &= " 																			from	ms_Customer_Shipping_CusRefId "
            strSQL &= " 																			where	Customer_Shipping_Index=@Customer_Shipping_Index"
            strSQL &= " 																			And		Customer_Index=@Customer_Index)"
            strSQL &= " 							SET @RefID = (select top 1 	RefID"
            strSQL &= " 																			from	ms_Customer_Shipping_CusRefId "
            strSQL &= " 																			where	Customer_Shipping_Index=@Customer_Shipping_Index"
            strSQL &= " 																			And		Customer_Index=@Customer_Index)"
            strSQL &= " 							Insert Into @CusRef"
            strSQL &= " 							SELECT			Customer_Shipping_CusRefId = @chkValue"
            strSQL &= " 											,@Customer_Shipping_Index"
            strSQL &= " 											,@Customer_Shipping_Name"
            strSQL &= " 											,@Customer_Shipping_Id"
            strSQL &= " 											,RefID =  isnull(@RefID,'')"
            strSQL &= " 											,RefID2 =  isnull(@RefID,@Customer_Shipping_Id)"
            strSQL &= " 											,Customer_Id =  @Customer_Id"
            strSQL &= " 											,Customer_Name = @Customer_Name"
            strSQL &= " 											,Customer_Index = @Customer_Index"
            strSQL &= " 							FROM       ms_Customer_Shipping"
            strSQL &= " 							WHERE	Status_Id <> -1"
            strSQL &= " 									AND  Customer_Shipping_Index=@Customer_Shipping_Index"
            strSQL &= " 							FETCH NEXT FROM CurCustomer"
            strSQL &= " 							INTO  @Customer_Index,@Customer_Id,@Customer_Name"
            strSQL &= "                                 End"
            strSQL &= " 						CLOSE CurCustomer"
            strSQL &= " 						DEALLOCATE CurCustomer "
            '---- END LOOP CUTOMER"
            strSQL &= " 					FETCH NEXT FROM CurCustomer_Shipping"
            strSQL &= " 					INTO @Customer_Shipping_Index,@Customer_Shipping_Name,@Customer_Shipping_Id"
            strSQL &= "                                End"

            strSQL &= " 				CLOSE CurCustomer_Shipping"
            strSQL &= " 				DEALLOCATE CurCustomer_Shipping "


            strSQL &= " SELECT * FROM @CusRef ORDER BY Isnull(Customer_Shipping_CusRefId,999999999)"




            SetSQLString = strSQL + WhereString

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub getQCPopup_CustomerShip(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" And pFilterValue = "" Then
                ' Select No Condition 
                strSQL = " SELECT    Customer_Shipping_Index, Company_Name ,Address" & _
                         " FROM       ms_Customer_Shipping where  Customer_Shipping_Index not in ('0') and status_id  not in (-1) "

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 
                strSQL = " SELECT      Customer_Shipping_Index, Company_Name,Address" & _
                                        " FROM       ms_Customer_Shipping "

                strWhere = "WHERE  " & ColumnName & " ='" & pFilterValue & "' and customer_index not in ('0') and status_id not in (-1) "
            End If

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

    Public Sub getQCPopup_Search(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            ' Select No Condition 
            strSQL = " SELECT    Customer_Shipping_Index, Company_Name,Address " & _
                     " FROM  ms_Customer_Shipping  where  Customer_Shipping_Index not in ('0') and status_id  not in (-1) "


            SetSQLString = strSQL + WhereString

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getcustomer_shippingANDlocation(ByVal CUS_ID As String, ByVal CUS_LO_ID As String)
        Dim strSQL As String = ""
        Try
            strSQL = "  select  CSL.Address,CS.Customer_Shipping_Index,CSL.Customer_Shipping_Location_Index,CSL.Route_Index,CSL.SubRoute_Index ,CSL.TransportRegion_Index from ms_Customer_Shipping CS  "
            strSQL &= "  inner join ms_Customer_Shipping_Location CSL on CS.Customer_Shipping_Index = CSL.Customer_Shipping_Index"
            strSQL &= " where CS.Str1 = '" & CUS_ID & "' AND CSL.Customer_Shipping_Location_Id = '" & CUS_LO_ID & "'"
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



    Public Sub getIndex_Customer_Shipping(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            ' Select No Condition 
            strSQL = " SELECT * " & _
                     " FROM  ms_Customer_Shipping_Location  where  1=1 AND "


            SetSQLString = strSQL & WhereString

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

End Class