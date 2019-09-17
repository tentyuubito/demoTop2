Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer

Public Class ms_Customer_Shipping_Location : Inherits DBType_SQLServer

#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _Customer_Shipping_Location_Index As String
    Private _Customer_Shipping_Index As String
    Private _Title As String
    Private _Shipping_Location_Name As String
    Private _Address As String
    Private _District_Index As String
    Private _Province_Index As String
    Private _Town_index As String
    Private _Postcode As String
    Private _Tel As String
    Private _Fax As String
    Private _Mobile As String
    Private _Email As String
    Private _Remark As String
    Private _Status As Integer
    Private _Str1 As String
    Private _Str2 As String
    Private _Str3 As String
    Private _Str4 As String
    Private _Str5 As String
    Private _Str6 As String
    Private _Str7 As String
    Private _Str8 As String
    Private _Str9 As String
    Private _Str10 As String
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer
    Private _Customer_Type_Index As String
    Private _status_id As Integer
    Private _Customer_Shipping_Location_Id As String

    Private _contact_Person1 As String
    Private _contact_Person2 As String
    Private _contact_Person3 As String

    Private _Route_Index As String



    Private _is_gi_primarywh As Boolean
    Private _warehouse_index As String
    Private _is_gi_primarywhonly As Boolean
    Private _is_gi_remainingage As Boolean
    Private _remainingage_value As Integer
    Private _remainingage_unit As Integer
    Private _Is_GI_NotOlderThanLastIssue As Boolean
    Private _lastissue_option As String
    Private _is_gi_coa_req As Boolean
    Private _is_dl_mon As Boolean
    Private _is_dl_tue As Boolean
    Private _is_dl_wed As Boolean
    Private _is_dl_thu As Boolean
    Private _is_dl_fri As Boolean
    Private _is_dl_sat As Boolean
    Private _is_dl_sun As Boolean
    Private _dl_mon_remark As String
    Private _dl_tue_remark As String
    Private _dl_wed_remark As String
    Private _dl_thu_remark As String
    Private _dl_fri_remark As String
    Private _dl_sat_remark As String
    Private _dl_sun_remark As String
    Private _min_delivery_per_order As Integer
    Private _BeginFlag As String
    Private _txtName As String
    Private _txtCustomer_Shipping_ID As String




    Private _Customer_Shipping_Location_SKU_RuleL As List(Of ms_Customer_Shipping_Location_SKU_Rule)
  
    Private _Customer_Shipping_Location_SKU_BlockL As List(Of ms_Customer_Shipping_Location_SKU_Block)


#End Region

#Region " Properties "


    Public Property Customer_Shipping_Location_SKU_RuleL() As List(Of ms_Customer_Shipping_Location_SKU_Rule)
        Get
            Return _Customer_Shipping_Location_SKU_RuleL
        End Get
        Set(ByVal value As List(Of ms_Customer_Shipping_Location_SKU_Rule))
            _Customer_Shipping_Location_SKU_RuleL = value
        End Set
    End Property
 

    Public Property Customer_Shipping_Location_SKU_BlockL() As List(Of ms_Customer_Shipping_Location_SKU_Block)
        Get
            Return _Customer_Shipping_Location_SKU_BlockL
        End Get
        Set(ByVal value As List(Of ms_Customer_Shipping_Location_SKU_Block))
            _Customer_Shipping_Location_SKU_BlockL = value
        End Set
    End Property


    Public Property Contact_Person1() As String
        Get
            Return _contact_Person1
        End Get
        Set(ByVal Value As String)
            _contact_Person1 = Value
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

    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal Value As String)
            _Route_Index = Value
        End Set
    End Property
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
    Public Property Customer_Shipping_Location_Index() As String
        Get
            Return _Customer_Shipping_Location_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Location_Index = Value
        End Set
    End Property

    Public Property Customer_Shipping_Index() As String
        Get
            Return _Customer_Shipping_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Index = Value
        End Set
    End Property

    Public Property txtName() As String
        Get
            Return _txtName
        End Get
        Set(ByVal Value As String)
            _txtName = Value
        End Set
    End Property

    Public Property Title() As String
        Get
            Return _Title
        End Get
        Set(ByVal Value As String)
            _Title = Value
        End Set
    End Property

    Public Property Shipping_Location_Name() As String
        Get
            Return _Shipping_Location_Name
        End Get
        Set(ByVal Value As String)
            _Shipping_Location_Name = Value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _Address
        End Get
        Set(ByVal Value As String)
            _Address = Value
        End Set
    End Property

    Public Property District_Index() As String
        Get
            Return _District_Index
        End Get
        Set(ByVal Value As String)
            _District_Index = Value
        End Set
    End Property

    Public Property Province_Index() As String
        Get
            Return _Province_Index
        End Get
        Set(ByVal Value As String)
            _Province_Index = Value
        End Set
    End Property

    Public Property Town_index() As String
        Get
            Return _Town_index
        End Get
        Set(ByVal Value As String)
            _Town_index = Value
        End Set
    End Property
    Public Property Postcode() As String
        Get
            Return _Postcode
        End Get
        Set(ByVal Value As String)
            _Postcode = Value
        End Set
    End Property

    Public Property Tel() As String
        Get
            Return _Tel
        End Get
        Set(ByVal Value As String)
            _Tel = Value
        End Set
    End Property

    Public Property Fax() As String
        Get
            Return _Fax
        End Get
        Set(ByVal Value As String)
            _Fax = Value
        End Set
    End Property

    Public Property Mobile() As String
        Get
            Return _Mobile
        End Get
        Set(ByVal Value As String)
            _Mobile = Value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _Email
        End Get
        Set(ByVal Value As String)
            _Email = Value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal Value As String)
            _Remark = Value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal Value As Integer)
            _Status = Value
        End Set
    End Property

    Public Property Str1() As String
        Get
            Return _Str1
        End Get
        Set(ByVal Value As String)
            _Str1 = Value
        End Set
    End Property

    Public Property Str2() As String
        Get
            Return _Str2
        End Get
        Set(ByVal Value As String)
            _Str2 = Value
        End Set
    End Property

    Public Property Str3() As String
        Get
            Return _Str3
        End Get
        Set(ByVal Value As String)
            _Str3 = Value
        End Set
    End Property

    Public Property Str4() As String
        Get
            Return _Str4
        End Get
        Set(ByVal Value As String)
            _Str4 = Value
        End Set
    End Property

    Public Property Str5() As String
        Get
            Return _Str5
        End Get
        Set(ByVal Value As String)
            _Str5 = Value
        End Set
    End Property

    Public Property Str6() As String
        Get
            Return _Str6
        End Get
        Set(ByVal Value As String)
            _Str6 = Value
        End Set
    End Property

    Public Property Str7() As String
        Get
            Return _Str7
        End Get
        Set(ByVal Value As String)
            _Str7 = Value
        End Set
    End Property

    Public Property Str8() As String
        Get
            Return _Str8
        End Get
        Set(ByVal Value As String)
            _Str8 = Value
        End Set
    End Property

    Public Property Str9() As String
        Get
            Return _Str9
        End Get
        Set(ByVal Value As String)
            _Str9 = Value
        End Set
    End Property

    Public Property Str10() As String
        Get
            Return _Str10
        End Get
        Set(ByVal Value As String)
            _Str10 = Value
        End Set
    End Property

    Public Property add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property

    Public Property add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property

    Public Property add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property

    Public Property update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property

    Public Property update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property

    Public Property update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

    Public Property Customer_Type_Index() As String
        Get
            Return _Customer_Type_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Type_Index = Value
        End Set
    End Property

    Public Property status_id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal Value As Integer)
            _status_id = Value
        End Set
    End Property

    Public Property Customer_Shipping_Location_Id() As String
        Get
            Return _Customer_Shipping_Location_Id
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Location_Id = Value
        End Set
    End Property

    Private _SubRoute_Index As String = ""
    Public Property Subroute_Index() As String
        Get
            Return _SubRoute_Index
        End Get
        Set(ByVal Value As String)
            _SubRoute_Index = Value
        End Set
    End Property
    Private _TransportRegion_Index As String = ""
    Public Property TransportRegion_Index() As String
        Get
            Return _TransportRegion_Index
        End Get
        Set(ByVal Value As String)
            _TransportRegion_Index = Value
        End Set
    End Property

    Public Property Is_GI_PrimaryWH() As Boolean
        Get
            Return _is_gi_primarywh
        End Get
        Set(ByVal Value As Boolean)
            _is_gi_primarywh = Value
        End Set
    End Property

    Public Property Warehouse_Index() As String
        Get
            Return _warehouse_index
        End Get
        Set(ByVal Value As String)
            _warehouse_index = Value
        End Set
    End Property

    Public Property Is_GI_PrimaryWHOnly() As Boolean
        Get
            Return _is_gi_primarywhonly
        End Get
        Set(ByVal Value As Boolean)
            _is_gi_primarywhonly = Value
        End Set
    End Property

    Public Property Is_GI_RemainingAge() As Boolean
        Get
            Return _is_gi_remainingage
        End Get
        Set(ByVal Value As Boolean)
            _is_gi_remainingage = Value
        End Set
    End Property

    Public Property RemainingAge_Value() As Integer
        Get
            Return _remainingage_value
        End Get
        Set(ByVal Value As Integer)
            _remainingage_value = Value
        End Set
    End Property

    Public Property RemainingAge_Unit() As Integer
        Get
            Return _remainingage_unit
        End Get
        Set(ByVal Value As Integer)
            _remainingage_unit = Value
        End Set
    End Property

    Public Property Is_GI_NotOlderThanLastIssue() As Boolean
        Get
            Return _Is_GI_NotOlderThanLastIssue
        End Get
        Set(ByVal Value As Boolean)
            _Is_GI_NotOlderThanLastIssue = Value
        End Set
    End Property

    Public Property LastIssue_Option() As String
        Get
            Return _lastissue_option
        End Get
        Set(ByVal Value As String)
            _lastissue_option = Value
        End Set
    End Property

    Public Property Is_GI_COA_Req() As Boolean
        Get
            Return _is_gi_coa_req
        End Get
        Set(ByVal Value As Boolean)
            _is_gi_coa_req = Value
        End Set
    End Property

    Public Property Is_DL_Mon() As Boolean
        Get
            Return _is_dl_mon
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_mon = Value
        End Set
    End Property

    Public Property Is_DL_Tue() As Boolean
        Get
            Return _is_dl_tue
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_tue = Value
        End Set
    End Property

    Public Property Is_DL_Wed() As Boolean
        Get
            Return _is_dl_wed
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_wed = Value
        End Set
    End Property

    Public Property Is_DL_Thu() As Boolean
        Get
            Return _is_dl_thu
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_thu = Value
        End Set
    End Property

    Public Property Is_DL_Fri() As Boolean
        Get
            Return _is_dl_fri
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_fri = Value
        End Set
    End Property

    Public Property Is_DL_Sat() As Boolean
        Get
            Return _is_dl_sat
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_sat = Value
        End Set
    End Property

    Public Property Is_DL_Sun() As Boolean
        Get
            Return _is_dl_sun
        End Get
        Set(ByVal Value As Boolean)
            _is_dl_sun = Value
        End Set
    End Property

    Public Property DL_Mon_Remark() As String
        Get
            Return _dl_mon_remark
        End Get
        Set(ByVal Value As String)
            _dl_mon_remark = Value
        End Set
    End Property

    Public Property DL_Tue_Remark() As String
        Get
            Return _dl_tue_remark
        End Get
        Set(ByVal Value As String)
            _dl_tue_remark = Value
        End Set
    End Property

    Public Property DL_Wed_Remark() As String
        Get
            Return _dl_wed_remark
        End Get
        Set(ByVal Value As String)
            _dl_wed_remark = Value
        End Set
    End Property

    Public Property DL_Thu_Remark() As String
        Get
            Return _dl_thu_remark
        End Get
        Set(ByVal Value As String)
            _dl_thu_remark = Value
        End Set
    End Property

    Public Property DL_Fri_Remark() As String
        Get
            Return _dl_fri_remark
        End Get
        Set(ByVal Value As String)
            _dl_fri_remark = Value
        End Set
    End Property

    Public Property DL_Sat_Remark() As String
        Get
            Return _dl_sat_remark
        End Get
        Set(ByVal Value As String)
            _dl_sat_remark = Value
        End Set
    End Property

    Public Property DL_Sun_Remark() As String
        Get
            Return _dl_sun_remark
        End Get
        Set(ByVal Value As String)
            _dl_sun_remark = Value
        End Set
    End Property

    Public Property MinDeliveryPerOrder() As Integer
        Get
            Return _min_delivery_per_order
        End Get
        Set(ByVal Value As Integer)
            _min_delivery_per_order = Value
        End Set
    End Property
    Public Property BeginFlag() As String
        Get
            Return _BeginFlag
        End Get
        Set(ByVal Value As String)
            _BeginFlag = Value
        End Set
    End Property

#End Region

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

#Region " SELECT DATA "

    Public Function isExistID(ByVal _Customer_Shipping_Location_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Customer_Shipping_Location where Customer_Shipping_Location_Id = @Customer_Shipping_Location_Id  and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 20).Value = _Customer_Shipping_Location_Id
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

    Public Function isChckID(ByVal _Customer_Receive_Location_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Customer_Receive_Location where Customer_Receive_Location_Id = @Customer_Receive_Location_Id and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Customer_Receive_Location_Id", SqlDbType.VarChar, 15).Value = _Customer_Receive_Location_Id

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

    Public Sub Searchms_Shipping_Location(ByVal ColumnName As String, ByVal pFilterValue As String, ByVal Shipping_Location_Index As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then

                strSQL = "SELECT     *    FROM       VIEW_MS_Shipping_Location where Shipping_Location_Index ='" & Shipping_Location_Index & "'"
                strWhere = pFilterValue
            Else

            End If

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchData_Click(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = " SELECT     *   " & _
                         " FROM       VIEW_MS_Shipping_Location "

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 

            End If




            SetSQLString = strSQL & strWhere
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
            '  strSQL = " select Shipping_location_index,Shipping_location_id,Shipping_location_name,address,postcode,tel,fax,mobile,email "
            ' strSQL += " from ms_Shipping_Location  where status_id  not in (-1)"

            strSQL = " select Shipping_Location_Index,Shipping_Location_Id,Shipping_Location_Name,Address,district,Province,Mobile,Email  from VIEW_MS_Shipping_Location"


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

    Public Sub getCus_Ship_Locartion_SearchPopUp(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

        
            strSQL = " select * from VIEW_MS_Cus_Ship_Location"


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
    Public Sub getCus_Ship_Locartion_Search(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim xQuery As String = ""
        Dim strWhere As String = ""
        Try


            'strSQL = "  SELECT    Customer_Shipping_Location_Index,Customer_Shipping_Location_Id,Company_Name,District,Province,Str1,Postcode,Mobile,Fax,Tel,Address ,status_id,"
            'strSQL += " isnull(Address+ ' ' + District +' '+ Province +' '+ Postcode,Address) as AddressShipping_Location,Shipping_Location_Name,Route_Index,SubRoute_Index,DistributionCenter_Index "

            'strSQL = "SELECT isnull(Address+ ' ' + District +' '+ Province +' '+ Postcode,Address) as AddressShipping_Location,*"
            'strSQL += " FROM         VIEW_MS_Cus_Ship_Location "
            'strSQL += "  WHERE   " & ColumnName & " ='" & pFilterValue & "' and status_id  not in (-1) "
            'strWhere = ""
            ' End If



            xQuery = " SELECT (isnull(Address + ' ' ,'') "
            xQuery &= " 		 + (case when isnull(Town_index,'') = '0010000000000' then '' else  isnull(Town_Name,'')  + ' ' end)"
            xQuery &= " 		 + (case when isnull(District_Index,'') = '0010000000000' then '' else  isnull(District,'')  + ' ' end)"
            xQuery &= " 		 + (case when isnull(Province_Index,'') = '0010000000000' then '' else  isnull(Province,'')  + ' ' end)"
            xQuery &= " 		+ isnull(Postcode,'')) as xAddressShipping_Location"
            xQuery &= " 	,*   "
            xQuery &= " FROM         VIEW_MS_Cus_Ship_Location "
            xQuery &= "  WHERE   " & ColumnName & " ='" & pFilterValue & "' and status_id  not in (-1) "
            strWhere = ""


            SetSQLString = xQuery + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getPopup_Customer(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" And pFilterValue = "" Then
                ' Select No Condition 
                'strSQL = " SELECT    Shipping_Location_Index, Shipping_Location_Id,Address,Tel,Fax " & _
                '         " FROM       ms_Shipping_Location where   status_id  not in (-1) "

                strSQL = "   SELECT     ms_Province.Province, ms_District.District, ms_Shipping_Location.Mobile, ms_Shipping_Location.Fax, "
                strSQL += "    ms_Shipping_Location.Tel, ms_Shipping_Location.Postcode, ms_Shipping_Location.Address, "
                strSQL += "     ms_Shipping_Location.Shipping_Location_Name, ms_Shipping_Location.Shipping_Location_Index"
                strSQL += "       ,ms_Shipping_Location.status_id,,ms_Shipping_Location.Shipping_Location_Id, "
                strSQL += "    ms_Shipping_Location.Address + ' ' + ms_District.District +' '+ ms_Province.Province +' '+ ms_Shipping_Location.Postcode as AddressShipping_Location"
                strSQL += "   FROM         ms_Shipping_Location LEFT OUTER JOIN"
                strSQL += "      ms_District ON ms_Shipping_Location.District_Index = ms_District.District_Index LEFT OUTER JOIN"
                strSQL += "    ms_Province ON ms_Shipping_Location.Province_Index = ms_Province.Province_Index"
                strSQL += "  where   ms_Shipping_Location.status_id  not in (-1) "

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 
                'strSQL = " SELECT    Shipping_Location_Index, Shipping_Location_Id,Address,Tel,Fax " & _
                '                        " FROM       ms_Shipping_Location  "


                strSQL = "   SELECT     ms_Province.Province, ms_District.District, ms_Shipping_Location.Mobile, ms_Shipping_Location.Fax, "
                strSQL += "    ms_Shipping_Location.Tel, ms_Shipping_Location.Postcode, ms_Shipping_Location.Address, "
                strSQL += "     ms_Shipping_Location.Shipping_Location_Name, ms_Shipping_Location.Shipping_Location_Index"
                strSQL += "       ,ms_Shipping_Location.status_id,ms_Shipping_Location.Shipping_Location_Id, "
                strSQL += "    ms_Shipping_Location.Address + ' ' + ms_District.District +' '+ ms_Province.Province +' '+ ms_Shipping_Location.Postcode as AddressShipping_Location"
                strSQL += "   FROM         ms_Shipping_Location LEFT OUTER JOIN"
                strSQL += "      ms_District ON ms_Shipping_Location.District_Index = ms_District.District_Index LEFT OUTER JOIN"
                strSQL += "    ms_Province ON ms_Shipping_Location.Province_Index = ms_Province.Province_Index "
                '  strSQL += "  where   ms_Shipping_Location.status_id  not in (-1) "


                strWhere = " WHERE   ms_Shipping_Location. " & ColumnName & " ='" & pFilterValue & "' and ms_Shipping_Location.status_id  not in (-1) "
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

    Public Sub SelectFor_EditData_Click(ByVal Customer_Shipping_Location_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "   SELECT     ms_Customer_Shipping_Location.*, ms_Country.Country_Code AS Country_Code, ms_Country.Country_Name AS Country_Name, "
            strSQL &= "             ms_Country.Country_Name_Th AS Country_Name_Th, ms_Province.Province AS Province, ms_District.District AS District, "
            strSQL &= "                        ms_Route.Description AS Route"
            strSQL &= "                        ,ms_Customer_Shipping_Location.CustomerType_Index,ms_DistributionCenter.Description as DistributionCenter"
            strSQL &= "            FROM         ms_Province RIGHT OUTER JOIN"
            strSQL &= "                     ms_Customer_Shipping_Location LEFT OUTER JOIN"
            strSQL &= "                      ms_Route ON ms_Customer_Shipping_Location.Route_Index = ms_Route.Route_Index ON "
            strSQL &= "                      ms_Province.Province_Index = ms_Customer_Shipping_Location.Province_Index LEFT OUTER JOIN"
            strSQL &= "                      ms_District ON ms_Customer_Shipping_Location.District_Index = ms_District.District_Index LEFT OUTER JOIN"
            strSQL &= "                       ms_Country ON ms_Customer_Shipping_Location.Str3 = ms_Country.Country_Index"
            strSQL &= "                       LEFT OUTER JOIN  ms_SubRoute ON ms_Customer_Shipping_Location.SubRoute_Index = ms_SubRoute.SubRoute_Index"
            strSQL &= "                       LEFT OUTER JOIN  ms_DistributionCenter ON ms_DistributionCenter.DistributionCenter_Index = ms_SubRoute.DistributionCenter_Index"
            '  strSQL &= "           LEFT OUTER JOIN ms_Customer_Shipping_Location_SKU_Rule  ON ms_Customer_Shipping_Location.Customer_Shipping_Location_Index =  ms_Customer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_Index    "
            ' strSQL &= "   LEFT OUTER JOIN MS_SKU ON ms_Customer_Shipping_Location_SKU_Rule.Sku_Index =MS_SKU.Sku_Index  "
            strSQL &= "  WHERE       ms_Customer_Shipping_Location.status_id != -1  and ms_Customer_Shipping_Location.Customer_Shipping_Location_Index = '" & Customer_Shipping_Location_Index & "'"

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
    Public Sub GetDatams_Customer_Shipping_Location_SKU_Block(ByVal pCustomer_Shipping_Location_Index As String)
        Try
            Dim strSQL As String = ""

            strSQL = " SELECT *,ms_sku.str1 as Sku_Name FROM ms_Customer_Shipping_Location_SKU_Block LEFT JOIN ms_sku ON ms_Customer_Shipping_Location_SKU_Block.Sku_Index = ms_Sku.Sku_Index WHERE Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index AND ms_Customer_Shipping_Location_SKU_Block.status_id <> -1  "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = pCustomer_Shipping_Location_Index
            End With


            _dataTable = DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub GetDataCustomer_Shipping_Location_SKU_Rule(ByVal pCustomer_Shipping_Location_Index As String)
        Try
            Dim strSQL As String = ""

            strSQL = " SELECT *,ms_sku.str1 as Sku_Name FROM ms_Customer_Shipping_Location_SKU_Rule LEFT JOIN ms_sku ON ms_Customer_Shipping_Location_SKU_Rule.Sku_Index = ms_Sku.Sku_Index WHERE Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index AND ms_Customer_Shipping_Location_SKU_Rule.status_id <> -1  "

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = pCustomer_Shipping_Location_Index
            End With


            _dataTable = DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SelectDataEditToCustomerShipping_Location(ByVal Customer_Shipping_Index As String, ByVal pstrWhere As String)
        '  
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            strSQL = " SELECT  TOP 500   dbo.ms_Province.Province AS Province, dbo.ms_District.District AS District, dbo.ms_Customer_Shipping_Location.*,ms_Country.Country_Name,ms_Country.Country_Code,dbo.ms_TransportRegion.Description AS TransportRegion,dbo.ms_Route.Description AS Route,dbo.ms_SubRoute.Description AS SubRoute "
            strSQL &= " ,dbo.ms_DistributionCenter.Description as DistributionCenter"
            strSQL &= " FROM         dbo.ms_District RIGHT OUTER JOIN"
            strSQL &= "           dbo.ms_Customer_Shipping_Location ON dbo.ms_District.District_Index = dbo.ms_Customer_Shipping_Location.District_Index LEFT OUTER JOIN"
            strSQL &= " dbo.ms_Province ON dbo.ms_Customer_Shipping_Location.Province_Index = dbo.ms_Province.Province_Index LEFT OUTER JOIN ms_Country ON "
            strSQL &= " ms_Country.Country_Index = ms_Customer_Shipping_Location.str3"
            strSQL &= " LEFT OUTER JOIN dbo.ms_TransportRegion ON dbo.ms_Customer_Shipping_Location.TransportRegion_Index = dbo.ms_TransportRegion.TransportRegion_Index"
            strSQL &= " LEFT OUTER JOIN dbo.ms_Route ON dbo.ms_Customer_Shipping_Location.Route_Index = dbo.ms_Route.Route_Index"
            strSQL &= "  LEFT OUTER JOIN dbo.ms_SubRoute ON dbo.ms_Customer_Shipping_Location.SubRoute_Index = dbo.ms_SubRoute.SubRoute_Index "
            strSQL &= "  LEFT OUTER JOIN dbo.ms_DistributionCenter ON dbo.ms_SubRoute.DistributionCenter_Index = dbo.ms_DistributionCenter.DistributionCenter_Index "
            strSQL &= " WHERE       ms_Customer_Shipping_Location.status_id != -1 "
            If Customer_Shipping_Index <> "" Then
                strSQL &= " and ms_Customer_Shipping_Location.Customer_Shipping_Index  ='" & Customer_Shipping_Index & "'"
            End If
            SetSQLString = strSQL + pstrWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SelectDataEditToCustomerShipping_Location(ByVal Customer_Shipping_Index As String) ', ByVal pstrWhere As String
        '  
        Dim strSQL As String = ""
        Dim pstrWhere As String = ""
        Try

            strSQL = " SELECT     dbo.ms_Province.Province AS Province, dbo.ms_District.District AS District, dbo.ms_Customer_Shipping_Location.*,ms_Country.Country_Name,ms_Country.Country_Code "
            strSQL &= "            ,ms_TransportRegion.Description as TransportRegion"
            strSQL &= "            ,ms_Route.Description as Route"
            strSQL &= "            ,ms_SubRoute.Description as SubRoute"
            strSQL &= " FROM         dbo.ms_District RIGHT OUTER JOIN"
            strSQL &= "           dbo.ms_Customer_Shipping_Location ON dbo.ms_District.District_Index = dbo.ms_Customer_Shipping_Location.District_Index LEFT OUTER JOIN"
            strSQL &= "             dbo.ms_Province ON dbo.ms_Customer_Shipping_Location.Province_Index = dbo.ms_Province.Province_Index LEFT OUTER JOIN ms_Country ON "
            strSQL &= "             ms_Country.Country_Index = ms_Customer_Shipping_Location.str3  LEFT OUTER JOIN ms_TransportRegion ON "
            strSQL &= "             ms_TransportRegion.TransportRegion_Index = ms_Customer_Shipping_Location.TransportRegion_Index  LEFT OUTER JOIN ms_Route ON"
            strSQL &= "             ms_Route.Route_Index = ms_Customer_Shipping_Location.Route_Index  LEFT OUTER JOIN ms_SubRoute ON"
            strSQL &= "             ms_SubRoute.SubRoute_Index = ms_Customer_Shipping_Location.SubRoute_Index  "
            strSQL &= " WHERE       ms_Customer_Shipping_Location.status_id != -1 "
            If Customer_Shipping_Index <> "" Then
                strSQL &= " and ms_Customer_Shipping_Location.Customer_Shipping_Index  ='" & Customer_Shipping_Index & "'"
            End If
            SetSQLString = strSQL + pstrWhere
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

    '#Region " INSERT DATA "
    '    Public Function Insert_Master() As Boolean
    '        Dim strSQL As String

    '        Try


    '            If Trim(_Customer_Shipping_Location_Id) = "" Then
    '                Dim objDocumentNumber As New Sy_AutoNumber
    '                _Customer_Shipping_Location_Id = objDocumentNumber.getSys_ID("Customer_Shipping_Location_Id")
    '                objDocumentNumber = Nothing

    '            End If

    '            strSQL = " INSERT INTO ms_Customer_Shipping_Location(Customer_Shipping_Location_Index,Customer_Shipping_Location_Id,Customer_Shipping_Index,Shipping_Location_Name,Address,District_Index,Province_Index,Postcode,Tel,Fax,Mobile,Email,Remark,contact_Person1,contact_Person2,contact_Person3,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_branch,Route_Index)" & _
    '            "       VALUES(@Customer_Shipping_Location_Index,@Customer_Shipping_Location_Id,@Customer_Shipping_Index,@Shipping_Location_Name,@Address,@District_Index,@Province_Index,@Postcode,@Tel,@Fax,@Mobile,@Email,@Remark,@contact_Person1,@contact_Person2,@contact_Person3,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@add_by,@add_branch,@Route_Index)"


    '            strSQL = strSQL
    '            With SQLServerCommand
    '                '        gSB_GetDBServerDateTime()
    '                .Parameters.Clear()
    '                .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
    '                .Parameters.Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 15).Value = Me._Customer_Shipping_Location_Id
    '                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Index
    '                .Parameters.Add("@Shipping_Location_Name", SqlDbType.VarChar, 100).Value = Me._Shipping_Location_Name
    '                .Parameters.Add("@Address", SqlDbType.VarChar, 255).Value = Me._Address
    '                .Parameters.Add("@District_Index", SqlDbType.VarChar, 13).Value = Me._District_Index
    '                .Parameters.Add("@Province_Index", SqlDbType.VarChar, 13).Value = Me._Province_Index
    '                .Parameters.Add("@Postcode", SqlDbType.NVarChar, 100).Value = _Postcode
    '                .Parameters.Add("@Tel", SqlDbType.NVarChar, 100).Value = _Tel
    '                .Parameters.Add("@Fax", SqlDbType.NVarChar, 100).Value = _Fax
    '                .Parameters.Add("@Mobile", SqlDbType.NVarChar, 100).Value = _Mobile
    '                .Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = _Email
    '                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _Remark
    '                .Parameters.Add("@Contact_Person1", SqlDbType.NVarChar, 100).Value = _contact_Person1
    '                .Parameters.Add("@Contact_Person2", SqlDbType.NVarChar, 100).Value = _contact_Person2
    '                .Parameters.Add("@Contact_Person3", SqlDbType.NVarChar, 100).Value = _contact_Person3
    '                .Parameters.Add("@Route_Index", SqlDbType.VarChar, 100).Value = Me._Route_Index
    '                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
    '                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
    '                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _Str3
    '                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _Str4
    '                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _Str5
    '                .Parameters.Add("@Str6", SqlDbType.NVarChar, 255).Value = ""
    '                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
    '                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
    '                .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = ""
    '                .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = ""
    '                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
    '                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
    '            End With

    '            SetSQLString = strSQL
    '            SetCommandType = DBType_SQLServer.enuCommandType.Text
    '            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '            connectDB()
    '            EXEC_Command()

    '            Return True

    '        Catch ex As Exception

    '            Throw ex
    '        Finally
    '            disconnectDB()
    '            strSQL = Nothing

    '        End Try
    '    End Function
    '#End Region

    '#Region " UPDATE DATA "


    '    Private Function Update_Master() As Boolean
    '        Dim strSQL As String
    '        Try

    '            strSQL = "update ms_Customer_Shipping_Location set Shipping_Location_Name='" & Me._Shipping_Location_Name & "'"
    '            strSQL &= ",Customer_Shipping_Location_Id = '" & Me._Customer_Shipping_Location_Id & "'"
    '            strSQL &= ",Address = '" & Me._Address & "'"
    '            strSQL &= ",District_Index = '" & Me._District_Index & "'"
    '            strSQL &= ",Province_Index = '" & Me._Province_Index & "'"
    '            strSQL &= ",Postcode = '" & Me._Postcode & "'"
    '            strSQL &= ",Tel = '" & Me._Tel & "'"
    '            strSQL &= ",Fax = '" & Me._Fax & "'"
    '            strSQL &= ",Mobile = '" & Me._Mobile & "'"
    '            strSQL &= ",Email = '" & Me._Email & "'"
    '            strSQL &= ",Remark = '" & Me._Remark & "'"
    '            strSQL &= ",Contact_Person1 = '" & Me._contact_Person1 & "'"
    '            strSQL &= ",Contact_Person2 = '" & Me._contact_Person2 & "'"
    '            strSQL &= ",Contact_Person3 = '" & Me._contact_Person3 & "'"
    '            strSQL &= ",Route_Index = '" & Me._Route_Index & "'"
    '            strSQL &= ",Str3 = '" & Me._Str3 & "'"
    '            strSQL &= ",Str4 = '" & Me._Str4 & "'"
    '            strSQL &= ",Str5 = '" & Me._Str5 & "'"
    '            strSQL &= ",Update_by = '" & WV_UserName & "'"
    '            strSQL &= ",update_date=getdate() "
    '            strSQL &= ",update_branch= " & WV_Branch_ID & " "
    '            strSQL &= " WHERE Customer_Shipping_Location_Index='" & Me._Customer_Shipping_Location_Index & "' "

    '            SetSQLString = strSQL
    '            SetCommandType = DBType_SQLServer.enuCommandType.Text
    '            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '            connectDB()
    '            EXEC_Command()

    '            Return True


    '        Catch ex As Exception

    '            Throw ex
    '        Finally
    '            disconnectDB()
    '            strSQL = Nothing
    '        End Try
    '    End Function
    '#End Region


#Region " INSERT DATA "
    Public Function Insert_Master() As Boolean
        Dim strSQL As String

        Try


            If Trim(_Customer_Shipping_Location_Id) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _Customer_Shipping_Location_Id = objDocumentNumber.getSys_ID("Customer_Shipping_Location_Id")
                objDocumentNumber = Nothing

            End If

            strSQL = " INSERT INTO ms_Customer_Shipping_Location(Customer_Shipping_Location_Index,Customer_Shipping_Location_Id,Customer_Shipping_Index,Shipping_Location_Name,Address,District_Index,Province_Index,Town_index,Postcode,Tel,Fax,Mobile,Email,Remark,contact_Person1,contact_Person2,contact_Person3,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_branch,Route_Index,SubRoute_Index,TransportRegion_Index)" & _
            "       VALUES(@Customer_Shipping_Location_Index,@Customer_Shipping_Location_Id,@Customer_Shipping_Index,@Shipping_Location_Name,@Address,@District_Index,@Province_Index,@Town_index,@Postcode,@Tel,@Fax,@Mobile,@Email,@Remark,@contact_Person1,@contact_Person2,@contact_Person3,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@add_by,@add_branch,@Route_Index,@SubRoute_Index,@TransportRegion_Index)"


            strSQL = strSQL
            With SQLServerCommand
                '        gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                .Parameters.Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Id
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Index
                .Parameters.Add("@Shipping_Location_Name", SqlDbType.VarChar, 100).Value = Me._Shipping_Location_Name
                .Parameters.Add("@Address", SqlDbType.VarChar, 30).Value = Me._Address
                .Parameters.Add("@District_Index", SqlDbType.VarChar, 30).Value = Me._District_Index
                .Parameters.Add("@Province_Index", SqlDbType.VarChar, 100).Value = Me._Province_Index
                .Parameters.Add("@Town_index", SqlDbType.VarChar, 100).Value = Me._Town_index
                .Parameters.Add("@Postcode", SqlDbType.NVarChar, 100).Value = _Postcode
                .Parameters.Add("@Tel", SqlDbType.NVarChar, 100).Value = _Tel
                .Parameters.Add("@Fax", SqlDbType.NVarChar, 100).Value = _Fax
                .Parameters.Add("@Mobile", SqlDbType.NVarChar, 100).Value = _Mobile
                .Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = _Email
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _Remark
                .Parameters.Add("@Contact_Person1", SqlDbType.NVarChar, 250).Value = _contact_Person1
                .Parameters.Add("@Contact_Person2", SqlDbType.NVarChar, 250).Value = _contact_Person2
                .Parameters.Add("@Contact_Person3", SqlDbType.NVarChar, 250).Value = _contact_Person3
                .Parameters.Add("@Route_Index", SqlDbType.VarChar, 100).Value = Me._Route_Index
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@SubRoute_Index", SqlDbType.NVarChar, 100).Value = Me._SubRoute_Index
                .Parameters.Add("@TransportRegion_Index", SqlDbType.NVarChar, 100).Value = Me._TransportRegion_Index
                '.Parameters.Add("@Begin_flag", SqlDbType.NVarChar, 100).Value = _BeginFlag
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


    Public Function Insert_Master_V3() As Boolean
        Dim strSQL As String
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            If Trim(_Customer_Shipping_Location_Id) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                _Customer_Shipping_Location_Id = objDocumentNumber.getSys_ID("Customer_Shipping_Location_Id")
                objDocumentNumber = Nothing
            End If

            strSQL = " INSERT INTO ms_Customer_Shipping_Location(Customer_Shipping_Location_Index,Customer_Shipping_Location_Id,Customer_Shipping_Index,Shipping_Location_Name,Address,District_Index,Province_Index,Postcode,Tel,Fax,Mobile,Email,Remark,contact_Person1,contact_Person2,contact_Person3,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_branch,Route_Index,SubRoute_Index,TransportRegion_Index,Is_GI_PrimaryWH,Warehouse_Index,Is_GI_PrimaryWHOnly,Is_GI_RemainingAge,RemainingAge_Value,RemainingAge_Unit,Is_GI_NotOlderThanLastIssue,LastIssue_Option,Is_GI_COA_Req," & _
                    " Is_DL_Mon,Is_DL_Tue,Is_DL_Wed,Is_DL_Thu,Is_DL_Fri,Is_DL_Sat,Is_DL_Sun,DL_Mon_Remark,DL_Tue_Remark,DL_Wed_Remark,DL_Thu_Remark,DL_Fri_Remark,DL_Sat_Remark,DL_Sun_Remark,MinDeliveryPerOrder,Begin_Flag)" & _
            "       VALUES(@Customer_Shipping_Location_Index,@Customer_Shipping_Location_Id,@Customer_Shipping_Index,@Shipping_Location_Name,@Address,@District_Index,@Province_Index,@Postcode,@Tel,@Fax,@Mobile,@Email,@Remark,@contact_Person1,@contact_Person2,@contact_Person3,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@add_by,@add_branch,@Route_Index,@SubRoute_Index,@TransportRegion_Index,@Is_GI_PrimaryWH,@Warehouse_Index,@Is_GI_PrimaryWHOnly,@Is_GI_RemainingAge,@RemainingAge_Value,@RemainingAge_Unit,@Is_GI_NotOlderThanLastIssue,@LastIssue_Option,@Is_GI_COA_Req," & _
                    " @Is_DL_Mon,@Is_DL_Tue,@Is_DL_Wed,@Is_DL_Thu,@Is_DL_Fri,@Is_DL_Sat,@Is_DL_Sun,@DL_Mon_Remark,@DL_Tue_Remark,@DL_Wed_Remark,@DL_Thu_Remark,@DL_Fri_Remark,@DL_Sat_Remark,@DL_Sun_Remark,@MinDeliveryPerOrder,@Begin_Flag)"
            ' strSQL = strSQL
            With SQLServerCommand
                '        gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                .Parameters.Add("@Customer_Shipping_Location_Id", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Id
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Index
                .Parameters.Add("@Shipping_Location_Name", SqlDbType.VarChar, 100).Value = Me._Shipping_Location_Name
                .Parameters.Add("@Address", SqlDbType.VarChar, 30).Value = Me._Address
                .Parameters.Add("@District_Index", SqlDbType.VarChar, 30).Value = Me._District_Index
                .Parameters.Add("@Province_Index", SqlDbType.VarChar, 100).Value = Me._Province_Index
                .Parameters.Add("@Postcode", SqlDbType.NVarChar, 100).Value = _Postcode
                .Parameters.Add("@Tel", SqlDbType.NVarChar, 100).Value = _Tel
                .Parameters.Add("@Fax", SqlDbType.NVarChar, 100).Value = _Fax
                .Parameters.Add("@Mobile", SqlDbType.NVarChar, 100).Value = _Mobile
                .Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = _Email
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _Remark
                .Parameters.Add("@Contact_Person1", SqlDbType.NVarChar, 250).Value = _contact_Person1
                .Parameters.Add("@Contact_Person2", SqlDbType.NVarChar, 250).Value = _contact_Person2
                .Parameters.Add("@Contact_Person3", SqlDbType.NVarChar, 250).Value = _contact_Person3
                .Parameters.Add("@Route_Index", SqlDbType.VarChar, 100).Value = Me._Route_Index
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = ""
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@SubRoute_Index", SqlDbType.NVarChar, 100).Value = Me._SubRoute_Index
                .Parameters.Add("@TransportRegion_Index", SqlDbType.NVarChar, 100).Value = Me._TransportRegion_Index

                '--AddNEW V3

                .Parameters.Add("@Is_GI_PrimaryWH", SqlDbType.Bit).Value = _is_gi_primarywh
                .Parameters.Add("@Warehouse_Index", SqlDbType.VarChar, 13).Value = _warehouse_index
                .Parameters.Add("@Is_GI_PrimaryWHOnly", SqlDbType.Bit).Value = _is_gi_primarywhonly
                .Parameters.Add("@Is_GI_RemainingAge", SqlDbType.Bit).Value = _is_gi_remainingage
                .Parameters.Add("@RemainingAge_Value", SqlDbType.Int).Value = _remainingage_value
                .Parameters.Add("@RemainingAge_Unit", SqlDbType.Int).Value = _remainingage_unit
                .Parameters.Add("@Is_GI_NotOlderThanLastIssue", SqlDbType.Bit).Value = _Is_GI_NotOlderThanLastIssue
                .Parameters.Add("@Begin_Flag", SqlDbType.VarChar, 10).Value = "0"
                If _lastissue_option Is Nothing Then
                    .Parameters.Add("@LastIssue_Option", SqlDbType.Char, 1).Value = ""
                Else
                    .Parameters.Add("@LastIssue_Option", SqlDbType.Char, 1).Value = _lastissue_option
                End If


                .Parameters.Add("@Is_GI_COA_Req", SqlDbType.Bit).Value = _is_gi_coa_req
                .Parameters.Add("@Is_DL_Mon", SqlDbType.Bit).Value = _is_dl_mon
                .Parameters.Add("@Is_DL_Tue", SqlDbType.Bit).Value = _is_dl_tue
                .Parameters.Add("@Is_DL_Wed", SqlDbType.Bit).Value = _is_dl_wed
                .Parameters.Add("@Is_DL_Thu", SqlDbType.Bit).Value = _is_dl_thu
                .Parameters.Add("@Is_DL_Fri", SqlDbType.Bit).Value = _is_dl_fri
                .Parameters.Add("@Is_DL_Sat", SqlDbType.Bit).Value = _is_dl_sat
                .Parameters.Add("@Is_DL_Sun", SqlDbType.Bit).Value = _is_dl_sun
                .Parameters.Add("@DL_Mon_Remark", SqlDbType.NVarChar, 100).Value = _dl_mon_remark
                .Parameters.Add("@DL_Tue_Remark", SqlDbType.NVarChar, 100).Value = _dl_tue_remark
                .Parameters.Add("@DL_Wed_Remark", SqlDbType.NVarChar, 100).Value = _dl_wed_remark
                .Parameters.Add("@DL_Thu_Remark", SqlDbType.NVarChar, 100).Value = _dl_thu_remark
                .Parameters.Add("@DL_Fri_Remark", SqlDbType.NVarChar, 100).Value = _dl_fri_remark
                .Parameters.Add("@DL_Sat_Remark", SqlDbType.NVarChar, 100).Value = _dl_sat_remark
                .Parameters.Add("@DL_Sun_Remark", SqlDbType.NVarChar, 100).Value = _dl_sun_remark
                .Parameters.Add("@MinDeliveryPerOrder", SqlDbType.Int).Value = _min_delivery_per_order



            End With


            DBExeNonQuery(strSQL, Connection, myTrans)



            For Each objCustomer_Shipping_Location_SKU_Rule As ms_Customer_Shipping_Location_SKU_Rule In Customer_Shipping_Location_SKU_RuleL

                strSQL = " INSERT INTO ms_Customer_Shipping_Location_SKU_Rule(Customer_Shipping_Location_SKU_Rule_Index,Customer_Shipping_Location_Index,SKU_Index,Is_GI_RemainingAge,RemainingAge_Value,RemainingAge_Unit,Str1,Str2,Str3,Str4,Str5,add_by,add_branch)VALUES "
                strSQL &= " (@Customer_Shipping_Location_SKU_Rule_Index,@Customer_Shipping_Location_Index,@SKU_Index,@Is_GI_RemainingAge,@RemainingAge_Value,@RemainingAge_Unit,@Str1,@Str2,@Str3,@Str4,@Str5,@add_by,@add_branch ) "
                With SQLServerCommand.Parameters
                    .Clear()
                    If objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_SKU_Rule_Index")
                        objDBIndex = Nothing
                    End If
                    .Add("@Customer_Shipping_Location_SKU_Rule_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index
                    .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                    .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Rule.SKU_Index
                    .Add("@Is_GI_RemainingAge", SqlDbType.Bit).Value = objCustomer_Shipping_Location_SKU_Rule.Is_GI_RemainingAge
                    .Add("@RemainingAge_Value", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Value
                    .Add("@RemainingAge_Unit", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Unit
                    .Add("@Str1", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str1
                    .Add("@Str2", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str2
                    .Add("@Str3", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str3
                    .Add("@Str4", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str4
                    .Add("@Str5", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str5
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                End With
                DBExeNonQuery(strSQL, Connection, myTrans)

            Next





            For Each objCustomer_Shipping_Location_SKU_Block As ms_Customer_Shipping_Location_SKU_Block In Customer_Shipping_Location_SKU_BlockL


                strSQL = " INSERT INTO ms_Customer_Shipping_Location_SKU_Block (Customer_Shipping_Location_SKU_Block_Index,Customer_Shipping_Location_Index,Block_Type,SKU_Index,PLot,Mfg_Date,Exp_Date,Order_Index,OrderItem_Index,Str1,Str2,Str3,Str4,Str5,add_by,add_date,add_branch,status_id ) VALUES "
                strSQL &= " (@Customer_Shipping_Location_SKU_Block_Index,@Customer_Shipping_Location_Index,@Block_Type,@SKU_Index,@PLot,@Mfg_Date,@Exp_Date,@Order_Index,@OrderItem_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@add_by,getdate(),@add_branch,@status_id )  "
                With SQLServerCommand.Parameters
                    .Clear()
                    If objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_SKU_Block_Index = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_SKU_Block_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_SKU_Block_Index")
                        objDBIndex = Nothing
                    End If
                    .Add("@Customer_Shipping_Location_SKU_Block_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_SKU_Block_Index
                    .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                    .Add("@Block_Type", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Block.Block_Type
                    .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Block.SKU_Index
                    .Add("@PLot", SqlDbType.NVarChar, 50).Value = objCustomer_Shipping_Location_SKU_Block.PLot

                    .Add("@Mfg_Date", SqlDbType.SmallDateTime).Value = objCustomer_Shipping_Location_SKU_Block.Mfg_Date
                    .Add("@Exp_Date", SqlDbType.SmallDateTime).Value = objCustomer_Shipping_Location_SKU_Block.Exp_Date

                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Block.Order_Index
                    .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Block.OrderItem_Index
                    .Add("@Str1", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Block.Str1
                    .Add("@Str2", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Block.Str2
                    .Add("@Str3", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Block.Str3
                    .Add("@Str4", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Block.Str4
                    .Add("@Str5", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Block.Str5
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    .Add("@status_id", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Block.status_id
                End With
                DBExeNonQuery(strSQL, Connection, myTrans)

            Next


            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function


#End Region

#Region " UPDATE DATA "


    Private Function Update_Master() As Boolean
        Dim strSQL As String
        Try

            strSQL = "update ms_Customer_Shipping_Location set Shipping_Location_Name='" & Me._Shipping_Location_Name & "'"
            strSQL &= ",Customer_Shipping_Location_Id = '" & Me._txtCustomer_Shipping_ID & "'"
            strSQL &= ",Address = '" & Me._Address & "'"
            strSQL &= ",District_Index = '" & Me._District_Index & "'"
            strSQL &= ",Province_Index = '" & Me._Province_Index & "'"
            strSQL &= ",Postcode = '" & Me._Postcode & "'"
            strSQL &= ",Tel = '" & Me._Tel & "'"
            strSQL &= ",Fax = '" & Me._Fax & "'"
            strSQL &= ",Mobile = '" & Me._Mobile & "'"
            strSQL &= ",Email = '" & Me._Email & "'"
            strSQL &= ",Remark = '" & Me._Remark & "'"
            strSQL &= ",Contact_Person1 = '" & Me._contact_Person1 & "'"
            strSQL &= ",Contact_Person2 = '" & Me._contact_Person2 & "'"
            strSQL &= ",Contact_Person3 = '" & Me._contact_Person3 & "'"
            strSQL &= ",Route_Index = '" & Me._Route_Index & "'"
            strSQL &= ",Str3 = '" & Me._Str3 & "'"
            strSQL &= ",Str4 = '" & Me._Str4 & "'"
            strSQL &= ",Str5 = '" & Me._Str5 & "'"
            strSQL &= ",Update_by = '" & WV_UserName & "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",SubRoute_Index= '" & Me._SubRoute_Index & "' "
            strSQL &= ",TransportRegion_Index= '" & Me._TransportRegion_Index & "' "
            strSQL &= " WHERE Customer_Shipping_Index='" & Me._Customer_Shipping_Index & "' "
            strSQL &= " AND Begin_flag = 1 "

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

    Private Function Update_Master_V3() As Boolean
        Dim strSQL As String
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try


            strSQL = "update ms_Customer_Shipping_Location set Shipping_Location_Name='" & Me._Shipping_Location_Name & "'"
            strSQL &= ",Customer_Shipping_Location_Id = '" & Me._Customer_Shipping_Location_Id & "'"
            strSQL &= ",Address = '" & Me._Address & "'"
            strSQL &= ",District_Index = '" & Me._District_Index & "'"
            strSQL &= ",Province_Index = '" & Me._Province_Index & "'"
            strSQL &= ",Postcode = '" & Me._Postcode & "'"
            strSQL &= ",Tel = '" & Me._Tel & "'"
            strSQL &= ",Fax = '" & Me._Fax & "'"
            strSQL &= ",Mobile = '" & Me._Mobile & "'"
            strSQL &= ",Email = '" & Me._Email & "'"
            strSQL &= ",Remark = '" & Me._Remark & "'"
            strSQL &= ",Contact_Person1 = '" & Me._contact_Person1 & "'"
            strSQL &= ",Contact_Person2 = '" & Me._contact_Person2 & "'"
            strSQL &= ",Contact_Person3 = '" & Me._contact_Person3 & "'"
            strSQL &= ",Route_Index = '" & Me._Route_Index & "'"
            strSQL &= ",Str3 = '" & Me._Str3 & "'"
            strSQL &= ",Str4 = '" & Me._Str4 & "'"
            strSQL &= ",Str5 = '" & Me._Str5 & "'"
            strSQL &= ",Update_by = '" & WV_UserName & "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",SubRoute_Index= '" & Me._SubRoute_Index & "' "
            strSQL &= ",TransportRegion_Index= '" & Me._TransportRegion_Index & "' "
            '--ADDNEW
            strSQL &= String.Format(",Is_GI_PrimaryWH = '{0}'", _is_gi_primarywh)
            strSQL &= String.Format(",Warehouse_Index = '{0}'", _warehouse_index)
            strSQL &= String.Format(",Is_GI_PrimaryWHOnly = '{0}'", _is_gi_primarywhonly)
            strSQL &= String.Format(",Is_GI_RemainingAge = '{0}'", _is_gi_remainingage)
            strSQL &= String.Format(",RemainingAge_Value = '{0}'", _remainingage_value)
            strSQL &= String.Format(",RemainingAge_Unit = '{0}'", _remainingage_unit)
            strSQL &= String.Format(",Is_GI_NotOlderThanLastIssue = '{0}'", _Is_GI_NotOlderThanLastIssue)
            strSQL &= String.Format(",LastIssue_Option = '{0}'", _lastissue_option)
            strSQL &= String.Format(",Is_GI_COA_Req = '{0}'", _is_gi_coa_req)
            strSQL &= String.Format(",Is_DL_Mon = '{0}'", _is_dl_mon)
            strSQL &= String.Format(",Is_DL_Tue = '{0}'", _is_dl_tue)
            strSQL &= String.Format(",Is_DL_Wed = '{0}'", _is_dl_wed)
            strSQL &= String.Format(",Is_DL_Thu = '{0}'", _is_dl_thu)
            strSQL &= String.Format(",Is_DL_Fri = '{0}'", _is_dl_fri)
            strSQL &= String.Format(",Is_DL_Sat = '{0}'", _is_dl_sat)
            strSQL &= String.Format(",Is_DL_Sun = '{0}'", _is_dl_sun)
            strSQL &= String.Format(",DL_Mon_Remark = '{0}'", _dl_mon_remark)
            strSQL &= String.Format(",DL_Tue_Remark = '{0}'", _dl_tue_remark)
            strSQL &= String.Format(",DL_Wed_Remark = '{0}'", _dl_wed_remark)
            strSQL &= String.Format(",DL_Thu_Remark = '{0}'", _dl_thu_remark)
            strSQL &= String.Format(",DL_Fri_Remark = '{0}'", _dl_fri_remark)
            strSQL &= String.Format(",DL_Sat_Remark = '{0}'", _dl_sat_remark)
            strSQL &= String.Format(",DL_Sun_Remark = '{0}'", _dl_sun_remark)
            strSQL &= String.Format(",MinDeliveryPerOrder = '{0}'", _min_delivery_per_order)
            strSQL &= " WHERE Customer_Shipping_Location_Index='" & Me._Customer_Shipping_Location_Index & "' "


            DBExeNonQuery(strSQL, Connection, myTrans)



            strSQL = String.Format(" UPDATE ms_Customer_Shipping_Location_SKU_Rule SET status_id = -1 WHERE Customer_Shipping_Location_Index= '{0}' ", Me._Customer_Shipping_Location_Index)
            DBExeNonQuery(strSQL, Connection, myTrans)


            For Each objCustomer_Shipping_Location_SKU_Rule As ms_Customer_Shipping_Location_SKU_Rule In Customer_Shipping_Location_SKU_RuleL

                If objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index <> "" Then
                    strSQL = "   UPDATE ms_Customer_Shipping_Location_SKU_Rule SET  Is_GI_RemainingAge=@Is_GI_RemainingAge ,RemainingAge_Value=@RemainingAge_Value ,RemainingAge_Unit=@RemainingAge_Unit  "
                    strSQL &= " ,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,update_by=@update_by,update_date=getdate(),update_branch=@update_branch,status_id=1 WHERE Customer_Shipping_Location_SKU_Rule_Index=@Customer_Shipping_Location_SKU_Rule_Index "

                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@Customer_Shipping_Location_SKU_Rule_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index
                        .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                        .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Rule.SKU_Index
                        .Add("@Is_GI_RemainingAge", SqlDbType.Bit).Value = objCustomer_Shipping_Location_SKU_Rule.Is_GI_RemainingAge
                        .Add("@RemainingAge_Value", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Value
                        .Add("@RemainingAge_Unit", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Unit
                        .Add("@Str1", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str1
                        .Add("@Str2", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str2
                        .Add("@Str3", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str3
                        .Add("@Str4", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str4
                        .Add("@Str5", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str5
                        .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    End With

                    DBExeNonQuery(strSQL, Connection, myTrans)
                Else

                    strSQL = " INSERT INTO ms_Customer_Shipping_Location_SKU_Rule(Customer_Shipping_Location_SKU_Rule_Index,Customer_Shipping_Location_Index,SKU_Index,Is_GI_RemainingAge,RemainingAge_Value,RemainingAge_Unit,Str1,Str2,Str3,Str4,Str5,update_by,update_branch,status_id)VALUES "
                    strSQL &= " (@Customer_Shipping_Location_SKU_Rule_Index,@Customer_Shipping_Location_Index,@SKU_Index,@Is_GI_RemainingAge,@RemainingAge_Value,@RemainingAge_Unit,@Str1,@Str2,@Str3,@Str4,@Str5,@update_by,@update_branch,1 ) "
                    With SQLServerCommand.Parameters
                        .Clear()
                        If objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index = "" Then
                            Dim objDBIndex As New Sy_AutoNumber
                            objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_SKU_Rule_Index")
                            objDBIndex = Nothing
                        End If
                        .Add("@Customer_Shipping_Location_SKU_Rule_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index
                        .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                        .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_Rule.SKU_Index
                        .Add("@Is_GI_RemainingAge", SqlDbType.Bit).Value = objCustomer_Shipping_Location_SKU_Rule.Is_GI_RemainingAge
                        .Add("@RemainingAge_Value", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Value
                        .Add("@RemainingAge_Unit", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Unit
                        .Add("@Str1", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str1
                        .Add("@Str2", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str2
                        .Add("@Str3", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str3
                        .Add("@Str4", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str4
                        .Add("@Str5", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_Rule.Str5

                        .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    End With
                    DBExeNonQuery(strSQL, Connection, myTrans)

                End If



            Next

            strSQL = String.Format(" UPDATE ms_Customer_Shipping_Location_SKU_Block SET status_id = -1 WHERE Customer_Shipping_Location_Index= '{0}' ", Me._Customer_Shipping_Location_Index)
            DBExeNonQuery(strSQL, Connection, myTrans)


            For Each objCustomer_Shipping_Location_SKU_BLOCK As ms_Customer_Shipping_Location_SKU_Block In Customer_Shipping_Location_SKU_BlockL

                If objCustomer_Shipping_Location_SKU_BLOCK.Customer_Shipping_Location_SKU_Block_Index = "" Then

                    strSQL = " INSERT INTO ms_Customer_Shipping_Location_SKU_Block (Customer_Shipping_Location_SKU_Block_Index,Customer_Shipping_Location_Index,Block_Type,SKU_Index,PLot,Mfg_Date,Exp_Date,Order_Index,OrderItem_Index,Str1,Str2,Str3,Str4,Str5,add_by,add_date,add_branch,status_id ) VALUES "
                    strSQL &= " (@Customer_Shipping_Location_SKU_Block_Index,@Customer_Shipping_Location_Index,@Block_Type,@SKU_Index,@PLot,@Mfg_Date,@Exp_Date,@Order_Index,@OrderItem_Index,@Str1,@Str2,@Str3,@Str4,@Str5,@add_by,getdate(),@add_branch,@status_id )  "
                    With SQLServerCommand.Parameters
                        .Clear()
                        If objCustomer_Shipping_Location_SKU_BLOCK.Customer_Shipping_Location_SKU_Block_Index = "" Then
                            Dim objDBIndex As New Sy_AutoNumber
                            objCustomer_Shipping_Location_SKU_BLOCK.Customer_Shipping_Location_SKU_Block_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_SKU_Block_Index")
                            objDBIndex = Nothing
                        End If
                        .Add("@Customer_Shipping_Location_SKU_Block_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.Customer_Shipping_Location_SKU_Block_Index
                        .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                        .Add("@Block_Type", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_BLOCK.Block_Type
                        .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.SKU_Index
                        .Add("@PLot", SqlDbType.NVarChar, 50).Value = objCustomer_Shipping_Location_SKU_BLOCK.PLot

                        .Add("@Mfg_Date", SqlDbType.SmallDateTime).Value = objCustomer_Shipping_Location_SKU_BLOCK.Mfg_Date
                        .Add("@Exp_Date", SqlDbType.SmallDateTime).Value = objCustomer_Shipping_Location_SKU_BLOCK.Exp_Date

                        .Add("@Order_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.Order_Index
                        .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.OrderItem_Index
                        .Add("@Str1", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str1
                        .Add("@Str2", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str2
                        .Add("@Str3", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str3
                        .Add("@Str4", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str4
                        .Add("@Str5", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str5
                        .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        .Add("@status_id", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_BLOCK.status_id
                    End With
                    DBExeNonQuery(strSQL, Connection, myTrans)
                Else

                    strSQL = " UPDATE ms_Customer_Shipping_Location_SKU_Block SET Block_Type=@Block_Type,SKU_Index=@SKU_Index,PLot=@PLot,Mfg_Date=@Mfg_Date,Exp_Date=@Exp_Date "
                    strSQL &= ",Order_Index=@Order_Index,OrderItem_Index=@OrderItem_Index,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,update_by=@update_by,update_date=getdate(),update_branch=@update_branch ,status_id=@status_id "
                    strSQL &= " WHERE Customer_Shipping_Location_SKU_Block_Index=@Customer_Shipping_Location_SKU_Block_Index "
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@Customer_Shipping_Location_SKU_Block_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.Customer_Shipping_Location_SKU_Block_Index
                        ' .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Me._Customer_Shipping_Location_Index
                        .Add("@Block_Type", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_BLOCK.Block_Type
                        .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.SKU_Index
                        .Add("@PLot", SqlDbType.NVarChar, 50).Value = objCustomer_Shipping_Location_SKU_BLOCK.PLot

                        .Add("@Mfg_Date", SqlDbType.SmallDateTime).Value = objCustomer_Shipping_Location_SKU_BLOCK.Mfg_Date
                        .Add("@Exp_Date", SqlDbType.SmallDateTime).Value = objCustomer_Shipping_Location_SKU_BLOCK.Exp_Date

                        .Add("@Order_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.Order_Index
                        .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = objCustomer_Shipping_Location_SKU_BLOCK.OrderItem_Index
                        .Add("@Str1", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str1
                        .Add("@Str2", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str2
                        .Add("@Str3", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str3
                        .Add("@Str4", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str4
                        .Add("@Str5", SqlDbType.NVarChar, 100).Value = objCustomer_Shipping_Location_SKU_BLOCK.Str5
                        .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        .Add("@status_id", SqlDbType.Int).Value = objCustomer_Shipping_Location_SKU_BLOCK.status_id
                    End With
                    DBExeNonQuery(strSQL, Connection, myTrans)
                End If



            Next








            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function


#End Region

#Region " DELETE DATA "
    Public Function Delete_Master(ByVal Customer_Shipping_Location_Index As String) As Boolean
        ' *** Define value from parameter
        Me._Customer_Shipping_Location_Index = Customer_Shipping_Location_Index

        Dim strSQL As String

        Try
            'strSQL = " Delete  " & _
            '         " From ms_UnitWeight " & _
            '         " WHERE unitWeight_Index='" + Me._unitWeight_Index + "' "

            strSQL = "update ms_Customer_Shipping_Location set status_id = -1"
            strSQL &= ",Update_by = '" + WV_UserName + "'"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= " WHERE Customer_Shipping_Location_Index='" & Me._Customer_Shipping_Location_Index & "' "

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
#End Region

#Region " TRANSACTION "
    'Public Function SaveData(ByVal _Customer_Shipping_Location_Index As String, ByVal _Customer_Shipping_Location_Id As String, ByVal _Customer_Shipping_Index As String, ByVal _Shipping_Location_Name As String, ByVal _Address As String, ByVal _District_Index As String, ByVal _Province_Index As String, ByVal _Postcode As String, ByVal _Tel As String, ByVal _Fax As String, ByVal _Mobile As String, ByVal _Email As String, ByVal _Remark As String, ByVal _Str4 As String, ByVal _Str5 As String, ByVal _contact_Person1 As String, ByVal _contact_Person2 As String, ByVal _contact_Person3 As String, ByVal _Route_Index As String, ByVal _Country_Index As String) As Boolean



    '     ***  define value to field ***
    '    Me._Customer_Shipping_Location_Id = _Customer_Shipping_Location_Id
    '    Me._Customer_Shipping_Index = _Customer_Shipping_Index
    '    Me._Shipping_Location_Name = _Shipping_Location_Name
    '    Me._Address = _Address
    '    Me._District_Index = _District_Index
    '    Me._Province_Index = _Province_Index
    '    Me._Postcode = _Postcode
    '    Me._Tel = _Tel
    '    Me._Fax = _Fax
    '    Me._Mobile = _Mobile
    '    Me._Email = _Email
    '    Me._Remark = _Remark
    '    Me._Str3 = _Country_Index
    '    Me._Str4 = _Str4
    '    Me._Str5 = _Str5
    '    Me._contact_Person1 = _contact_Person1
    '    Me._contact_Person2 = _contact_Person2
    '    Me._contact_Person3 = _contact_Person3
    '    Me._Route_Index = _Route_Index

    '    Select Case objStatus
    '        Case enuOperation_Type.ADDNEW
    '             get New Sys_Value 
    '            Dim objDBIndex As New Sy_AutoNumber
    '            Me._Customer_Shipping_Location_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_Index")
    '            objDBIndex = Nothing

    '        Case enuOperation_Type.UPDATE
    '             Assign value from parameter
    '            Me._size_Id = _size_Index
    '            Me.Customer_Shipping_Location_Index = _Customer_Shipping_Location_Index
    '    End Select

    '     *******************************
    '    Try

    '         *** check  Operation 
    '        Select Case objStatus
    '            Case enuOperation_Type.ADDNEW

    '                  *** check duplicate id 
    '                If isExistID(_department_Id) = True Then
    '                 Cannot Save data
    '                    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '                    Return False

    '                    Exit Function
    '                Else
    '                 Can Save data
    '                    If Me.Insert_Master = True Then
    '                        MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Return True
    '                        Exit Function
    '                    Else
    '                        MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                        Return False
    '                        Exit Function
    '                    End If
    '                End If


    '            Case enuOperation_Type.UPDATE
    '                 **** update value 
    '                If Me.Update_Master = True Then
    '                    MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Return True
    '                    Exit Function
    '                Else
    '                    MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Return False
    '                    Exit Function
    '                End If

    '            Case enuOperation_Type.DELETE
    '                ' **** check value some table if need !! 
    '                If Me.DeleteSize_Master() = True Then
    '                    MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Return True
    '                    Exit Function

    '                Else
    '                    MessageBox.Show("ไม่สามารถลบ", "ลบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '                    Return False
    '                    Exit Function
    '                End If

    '        End Select


    '    Catch ex As Exception

    '        Throw ex
    '    End Try


    'End Function

    Public Function SaveData(ByVal _Customer_Shipping_Location_Index As String, ByVal _Customer_Shipping_Location_Id As String, ByVal _Customer_Shipping_Index As String, ByVal _Shipping_Location_Name As String, ByVal _Address As String, ByVal _District_Index As String, ByVal _Province_Index As String, ByVal _Postcode As String, ByVal _Tel As String, ByVal _Fax As String, ByVal _Mobile As String, ByVal _Email As String, ByVal _Remark As String, ByVal _Str4 As String, ByVal _Str5 As String, ByVal _contact_Person1 As String, ByVal _contact_Person2 As String, ByVal _contact_Person3 As String, ByVal _Route_Index As String, ByVal _Country_Index As String, ByVal Begin_flag As String, ByVal txtCustomer_Shipping_ID As String, Optional ByVal pstrSubRoute_Index As String = "", Optional ByVal pstrTransportRegion As String = "") As Boolean

        ' ***  define value to field ***
        Me._Customer_Shipping_Location_Id = _Customer_Shipping_Location_Id
        Me._Customer_Shipping_Index = _Customer_Shipping_Index
        Me._Shipping_Location_Name = _Shipping_Location_Name
        Me._Address = _Address
        Me._District_Index = _District_Index
        Me._Province_Index = _Province_Index
        Me._Postcode = _Postcode
        Me._Tel = _Tel
        Me._Fax = _Fax
        Me._Mobile = _Mobile
        Me._Email = _Email
        Me._Remark = _Remark
        Me._Str3 = _Country_Index
        Me._Str4 = _Str4
        Me._Str5 = _Str5
        Me._contact_Person1 = _contact_Person1
        Me._contact_Person2 = _contact_Person2
        Me._contact_Person3 = _contact_Person3
        Me._Route_Index = _Route_Index
        Me._SubRoute_Index = pstrSubRoute_Index
        Me._TransportRegion_Index = pstrTransportRegion
        Me._BeginFlag = Begin_flag
        Me._txtCustomer_Shipping_ID = txtCustomer_Shipping_ID

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._Customer_Shipping_Location_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_Index")
                objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._size_Id = _size_Index
                Me.Customer_Shipping_Location_Index = _Customer_Shipping_Location_Index
        End Select

        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    ' If isExistID(_department_Id) = True Then
                    ' Cannot Save data
                    '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    ' Return False

                    '  Exit Function
                    ' Else
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
                    ' End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.Update_Master = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        Exit Function
                    Else
                        'MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
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

            Throw ex
        End Try


    End Function


    Public Function SaveDataV3()
        Try


            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    ' get New Sys_Value 
                    Dim objDBIndex As New Sy_AutoNumber
                    Me._Customer_Shipping_Location_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_Index")
                    objDBIndex = Nothing



                    If Me.Insert_Master_V3 = True Then
                        Return True
                        Exit Function
                    Else
                        Return False
                        Exit Function
                    End If

                Case enuOperation_Type.UPDATE

                    If Me.Update_Master_V3 = True Then
                        Return True
                        Exit Function
                    Else
                        Return False
                        Exit Function
                    End If




            End Select


            Return False
        Catch ex As Exception
            Throw ex
        End Try
    End Function



#End Region

End Class