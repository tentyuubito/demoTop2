'*** Create Date :  17/01/2008
'*** Update Date :  13-01-2010
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Text
Public Class ms_Location : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _location_Index As String = ""
    Private _location_Id As String = ""
    Private _branch_Id As Integer = 0
    Private _location_Alias As String = ""
    Private _warehouse_Index As String = ""
    Private _room As String = ""
    Private _lock As String = ""
    Private _row As String = ""
    Private _depth As Integer = 0
    Private _level As Integer = 0
    Private _max_Qty As Double = 0.0
    Private _max_Weight As Double = 0.0
    Private _max_Volume As Double = 0.0
    Private _current_Qty As Double = 0.0
    Private _current_Weight As Double = 0.0
    Private _current_Volume As Double = 0.0
    Private _space_Used As Integer = 0
    Private _add_by As String = ""
    Private _add_date As Date = Now
    Private _add_branch As Integer = 0
    Private _update_by As String = ""
    Private _update_date As Date = Now
    Private _update_branch As Integer = 0
    Private _score As String = ""
    Private _LocationType_Index As String = ""
    Private _status_id As Integer = 0
    Private _zone_index As String = ""
    Private _Layout_Index As String = ""
    Private _Action_Id As Integer = 0


    Private _objItemCollection As List(Of ms_Location)
    Private _objItemLocation As ms_Location
    '13-01-2010 by ja add new RatioLocation on ms_Location
    Private _RatioLocation As Double
    Private _Max_Pallet As Double


    'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
    Private _Allow_Sugguest_Putaway As Integer = 0
    Private _Allow_Sugguest_Pick As Integer = 0

#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum

    Private objActive As enuOperation_Active

    Public Enum enuOperation_Active
        ACTIVE
        BLOCK
        RESERV
    End Enum

#End Region

#Region " Properties Section "
    '*** Property Readonly

    '13-01-2010 by ja add new RatioLocation on ms_Location
    Public Property RatioLocation() As Double
        Get
            Return _RatioLocation
        End Get
        Set(ByVal Value As Double)
            _RatioLocation = Value
        End Set
    End Property

    Public Property Zone_Index() As String
        Get
            Return _zone_index
        End Get
        Set(ByVal value As String)
            _zone_index = value
        End Set
    End Property

    Public Property Status_Id() As Integer
        Get
            Return _status_id
        End Get
        Set(ByVal value As Integer)
            _status_id = value
        End Set
    End Property

    Public Property LocationType_Index() As String
        Get
            Return _LocationType_Index
        End Get
        Set(ByVal value As String)
            _LocationType_Index = value
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

    '*** Property Writeonly

    '*** Property Read and Write

    Public Property Layout_Index() As String
        Get
            Return _Layout_Index
        End Get
        Set(ByVal Value As String)
            _Layout_Index = Value
        End Set
    End Property

    Public Property Location_Index() As String
        Get
            Return _location_Index
        End Get
        Set(ByVal Value As String)
            _location_Index = Value
        End Set
    End Property
    Public Property Location_Id() As String
        Get
            Return _location_Id
        End Get
        Set(ByVal Value As String)
            _location_Id = Value
        End Set
    End Property
    Public Property Branch_Id() As Integer
        Get
            Return _branch_Id
        End Get
        Set(ByVal Value As Integer)
            _branch_Id = Value
        End Set
    End Property
    Public Property Location_Alias() As String
        Get
            Return _location_Alias
        End Get
        Set(ByVal Value As String)
            _location_Alias = Value
        End Set
    End Property
    Public Property Warehouse_Index() As String
        Get
            Return _warehouse_Index
        End Get
        Set(ByVal Value As String)
            _warehouse_Index = Value
        End Set
    End Property
    Public Property Room() As String
        Get
            Return _room
        End Get
        Set(ByVal Value As String)
            _room = Value
        End Set
    End Property

    Public Property Row() As String
        Get
            Return _row
        End Get
        Set(ByVal Value As String)
            _row = Value
        End Set
    End Property
    Public Property Lock() As String
        Get
            Return _lock
        End Get
        Set(ByVal Value As String)
            _lock = Value
        End Set
    End Property
    Public Property Depth() As Integer
        Get
            Return _depth
        End Get
        Set(ByVal Value As Integer)
            _depth = Value
        End Set
    End Property
    Public Property Level() As Integer
        Get
            Return _level
        End Get
        Set(ByVal Value As Integer)
            _level = Value
        End Set
    End Property
    Public Property Max_Qty() As Double
        Get
            Return _max_Qty
        End Get
        Set(ByVal Value As Double)
            _max_Qty = Value
        End Set
    End Property
    Public Property Max_Weight() As Double
        Get
            Return _max_Weight
        End Get
        Set(ByVal Value As Double)
            _max_Weight = Value
        End Set
    End Property
    Public Property Max_Volume() As Double
        Get
            Return _max_Volume
        End Get
        Set(ByVal Value As Double)
            _max_Volume = Value
        End Set
    End Property
    Public Property Current_Qty() As Double
        Get
            Return _current_Qty
        End Get
        Set(ByVal Value As Double)
            _current_Qty = Value
        End Set
    End Property
    Public Property Current_Weight() As Double
        Get
            Return _current_Weight
        End Get
        Set(ByVal Value As Double)
            _current_Weight = Value
        End Set
    End Property
    Public Property Current_Volume() As Double
        Get
            Return _current_Volume
        End Get
        Set(ByVal Value As Double)
            _current_Volume = Value
        End Set
    End Property
    Public Property Space_Used() As Integer
        Get
            Return _space_Used
        End Get
        Set(ByVal Value As Integer)
            _space_Used = Value
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
    Public Property score() As String
        Get
            Return _score
        End Get
        Set(ByVal Value As String)
            _score = Value
        End Set
    End Property
    Public Property Action_Id() As Integer
        Get
            Return _Action_Id
        End Get
        Set(ByVal value As Integer)
            _Action_Id = value
        End Set
    End Property

    Public Property Max_Pallet() As Double
        Get
            Return _Max_Pallet
        End Get
        Set(ByVal Value As Double)
            _Max_Pallet = Value
        End Set
    End Property


    'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
    Public Property Allow_Sugguest_Putaway() As Integer
        Get
            Return _Allow_Sugguest_Putaway
        End Get
        Set(ByVal value As Integer)
            _Allow_Sugguest_Putaway = value
        End Set
    End Property

    Public Property Allow_Sugguest_Pick() As Integer
        Get
            Return _Allow_Sugguest_Pick
        End Get
        Set(ByVal value As Integer)
            _Allow_Sugguest_Pick = value
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

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objItemCollection As List(Of ms_Location))
        MyBase.New()
        objStatus = Operation_Type
        _objItemCollection = objItemCollection
    End Sub



#End Region


    '*** Normal DB Method
#Region " SELECT DATA "

    Public Function getLocation_Alias(ByVal Location_Index As String) As String

        Dim strSQL As String
        Try
            strSQL = " select Location_Alias from ms_Location where Location_Index = @Location_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Index", SqlDbType.VarChar, 50).Value = Location_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()

            If GetScalarOutput Is Nothing Then
                _scalarOutput = ""
            Else
                _scalarOutput = GetScalarOutput
            End If


            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return ""
            Else
                Return _scalarOutput
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub Select_location(ByVal pstrWhere As String)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= "   FROM ms_Location "
            strSQL &= "   WHERE status_id != -1 "
            strSQL &= pstrWhere

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

    Public Sub SearchLocation_Summary(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT 	Num_Location =  Isnull((select count(*) from ms_Location where status_id != -1 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' ),0)"
            strSQL &= "  ,Num_Empty =           Isnull((select count(*) from ms_Location where status_id != -1 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' and Current_Qty = 0 ),0)"
            strSQL &= "  ,Num_Use =             Isnull((select count(*) from ms_Location where status_id != -1 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' and Current_Qty > 0 ),0)"
            strSQL &= "  ,isNonActive =         Isnull((select count(*) from ms_Location where status_id != -1 and Action_Id =0 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' ),0)"
            strSQL &= "  ,Action_Id =            Isnull((select count(*) from ms_Location where status_id != -1 and Action_Id =1 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' ),0)"
            strSQL &= "  ,isReserv =            Isnull((select count(*) from ms_Location where status_id != -1 and Action_Id =2 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' ),0)"
            strSQL &= "  ,isBlock =             Isnull((select count(*) from ms_Location where status_id != -1 and Action_Id =3 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' ),0)"
            strSQL &= "   FROM ms_Location "
            strSQL &= "   WHERE status_id != -1 and Warehouse_Index = '" & pWarehouse_Index & "' And Room = '" & pRoom_Index & "' "
            strSQL &= "  GROUP BY Warehouse_Index, Room"

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
    Public Function SearchLocation_Summary_ProductType_Top4(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String) As String
        '  
        Dim strSQL As String = ""
        Dim strWHERE As String = ""
        Dim strSQLReturn As String = ""
        Try
            strSQLReturn = " select  TOP 4 ProductType_Index from ( select *  FROM VIEW_RPT_SpaceUsedByProductType "

            strSQL = " select  TOP 4 * from ( select *  FROM VIEW_RPT_SpaceUsedByProductType "
            strWHERE = " WHERE Warehouse_Index = " & pWarehouse_Index & " And Room_Index = " & pRoom_Index & " "
            strSQL &= strWHERE
            strSQLReturn &= strWHERE
            strSQL &= " ) a order by Space_Count desc"
            strSQLReturn &= " ) a order by Space_Count desc"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return strSQLReturn

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Sub SearchLocation_Summary_ProductType_NoTop4(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String, ByVal sqlInTop4 As String)
        '  
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT     Warehouse_Index, Room_Index,'-11' AS  ProductType_Index,'Other' as ProductType_Des, COUNT(*) AS Space_Count, CAST(COUNT(*) AS Decimal(18, 2)) /"
            strSQL &= "                 (SELECT     CAST(COUNT(*) AS Decimal(18, 2))"
            strSQL &= "   FROM ms_Location"
            strSQL &= "               WHERE      Space_Used = 1) * 100 AS Percent_Use_ProductType, CAST(COUNT(*) AS Decimal(18, 2)) /"
            strSQL &= "          (SELECT     CAST(COUNT(*) AS Decimal(18, 2))"
            strSQL &= "                    FROM          ms_Location) * 100 AS Percent_All_ProductType, 0.0 AS Percent_Graph"
            strSQL &= "   FROM    (SELECT  ProductType_Index ,ProductType_Des ,Location_Index,Warehouse_Index,Room_Index"
            strSQL &= "            FROM  VIEW_LocationBalance "
            strSQL &= "                     WHERE (Qty_Bal > 0)"
            strSQL &= "                     AND  ProductType_Index not in (" & sqlInTop4 & ")"
            strSQL &= "             GROUP BY ProductType_Index ,ProductType_Des ,Location_Index,Warehouse_Index,Room_Index) CAL"

            'strSQL &= "   FROM         (SELECT     PT.ProductType_Index, PT.Description AS ProductType_Des, LB.Location_Index, CONVERT(varchar(13),ms_Location.Warehouse_Index) as  Warehouse_Index, "
            'strSQL &= "                                  CONVERT(varchar(13),ms_Location.Room) AS Room_Index"
            'strSQL &= "              FROM          tb_LocationBalance LB INNER JOIN"
            'strSQL &= "                                      ms_SKU SKU ON LB.Sku_Index = SKU.Sku_Index INNER JOIN"
            'strSQL &= "                                     ms_Product P ON SKU.Product_Index = P.Product_Index INNER JOIN"
            'strSQL &= "                                    ms_ProductType PT ON P.ProductType_Index = PT.ProductType_Index INNER JOIN"
            'strSQL &= "                                    ms_Location ON LB.Location_Index = ms_Location.Location_Index"
            'strSQL &= "   WHERE (LB.Qty_Bal > 0)"
            'strSQL &= "  and  PT.ProductType_Index not in (" & sqlInTop4 & ")"

            '  strSQL &= "   GROUP BY PT.ProductType_Index, PT.Description, LB.Location_Index, ms_Location.Warehouse_Index, ms_Location.Room) CAL"
            strSQL &= "  WHERE  Warehouse_Index = " & pWarehouse_Index & " and Room_Index = " & pRoom_Index & ""
            strSQL &= "  GROUP BY Warehouse_Index, Room_Index"





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
    Public Sub SearchLocation_Summary_LocationType(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String)
        '  
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT     * "
            strSQL &= " FROM  VIEW_RPT_SpaceUtilize_GroupByLocationType "

            strSQL &= " WHERE Warehouse_Index = '" & pWarehouse_Index & "' And Room_Index = '" & pRoom_Index & "' "
            strSQL &= " AND SumLoUse > 0   "
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
    Public Function Search_SpacUse_ByCustomer_Top4(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String) As String
        '  
        Dim strSQL As String = ""
        Dim strWHERE As String = ""
        Dim strSQLReturn As String = ""
        Try
            strSQLReturn = " select  TOP 4 Customer_Index from ( select *  FROM VIEW_RPT_SpaceUsedByCustomer "

            strSQL = " select  TOP 4 * from ( select *  FROM VIEW_RPT_SpaceUsedByCustomer "
            strWHERE = " WHERE Warehouse_Index = '" & pWarehouse_Index & "' And Room_Index = '" & pRoom_Index & "' "
            strSQL &= strWHERE
            strSQLReturn &= strWHERE
            strSQL &= " ) a order by bin_count desc"
            strSQLReturn &= " ) a order by bin_count desc"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return strSQLReturn
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Sub Search_SpacUse_ByCustomer_NotTop4(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String, ByVal sqlInTop4 As String)
        '  
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT   CAL.Warehouse_Index,CAL.Room_Index, '-11' as Customer_Index,   'Other' as Customer_Name"
            strSQL &= "  , COUNT(*) AS Bin_Count, CAST(COUNT(*) AS Decimal(18, 2)) / (SELECT     CAST(COUNT(*) AS Decimal(18, 2)) FROM  ms_Location WHERE      Space_Used = 1) * 100 AS Percent_All_Customer"
            strSQL &= "  , CAST(COUNT(*) AS Decimal(18, 2)) / (SELECT     CAST(COUNT(*) AS Decimal(18, 2)) FROM          ms_Location) * 100 AS Percent_All_Location, 0.0 AS Percent_Graph"
            strSQL &= "  FROM         (	SELECT     	C.Customer_Index, C.Customer_Name, LB.Location_Index, dbo.ms_Location.Warehouse_Index, dbo.ms_Location.Room AS Room_Index"
            strSQL &= "  	FROM         	dbo.tb_LocationBalance LB INNER JOIN"
            strSQL &= "             		dbo.tb_Order O ON LB.Order_Index = O.Order_Index INNER JOIN"
            strSQL &= "            		dbo.ms_Customer C ON O.Customer_Index = C.Customer_Index INNER JOIN"
            strSQL &= "            		dbo.ms_Location ON LB.Location_Index = dbo.ms_Location.Location_Index"
            strSQL &= "   WHERE(LB.Qty_Bal > 0)"
            strSQL &= "  and C.Customer_Index not in (" & sqlInTop4 & ")) CAL"
            strSQL &= "  WHERE  CAL.Warehouse_Index = '" & pWarehouse_Index & "' and CAL.Room_Index = '" & pRoom_Index & "'"
            strSQL &= "  GROUP BY CAL.Warehouse_Index,CAL.Room_Index"

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
    Public Function Search_SumQty_BallAll(ByVal pWarehouse_Index As String, ByVal pRoom_Index As String) As Double
        '  
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT    sum(Qty_Bal) "
            strSQL &= "  FROM VIEW_LocationBalance     "
            strSQL &= "  WHERE  VIEW_LocationBalance.Warehouse_Index = '" & pWarehouse_Index & "' And VIEW_LocationBalance.Room_Index = '" & pRoom_Index & "' "

            SetSQLString = strSQL.ToString
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.ToString = "" Or _scalarOutput.ToString = "0" Then
                Return 0
            Else
                Return Val(_scalarOutput)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Sub SearchData_Edit(ByVal ColumnName As String, ByVal strWhere As String)
        '  
        Dim strSQL As String = ""
        Try
            If ColumnName = "" Then
                strSQL = " SELECT     *   "
                strSQL &= " FROM  ms_Location where status_id != -1 "

            Else
                strSQL = " SELECT " & ColumnName
                strSQL &= " FROM   ms_Location where status_id != -1 "
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



    Public Sub Search_DataAllLocation_ByConfig(ByVal pstrWhere As String, ByVal oprActive As enuOperation_Active)

        Dim strSQL As String = ""
        Dim strWhere As String = pstrWhere
        Try


            Select Case oprActive
                Case enuOperation_Active.ACTIVE
                    strSQL = "  SELECT chkSelect = CASE Action_Id WHEN 1 THEN convert(bit,1) ELSE convert(bit,0) END,   *"
                    strSQL &= " FROM  VIEW_Location "
                    strSQL &= " WHERE status_id != -1 "
                    strSQL &= " AND Current_Qty = 0 "

                Case enuOperation_Active.BLOCK
                    strSQL = "  SELECT chkSelect = CASE Action_Id WHEN 2 THEN convert(bit,1) ELSE convert(bit,0) END,   *"
                    strSQL &= "  FROM VIEW_Location "
                    strSQL &= " WHERE status_id != -1 "


                Case enuOperation_Active.RESERV
                    strSQL = "  SELECT chkSelect = CASE Action_Id WHEN 3 THEN convert(bit,1) ELSE convert(bit,0) END,   *"
                    strSQL &= "  FROM VIEW_Location "
                    strSQL &= " WHERE status_id != -1 "
                    strSQL &= " AND Current_Qty = 0 "

            End Select

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
    Public Sub Search_Room(ByVal WareHouse_Index As String, ByVal Layout_Index As String)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT     Room ,RoomName  "
            strSQL &= " FROM  VIEW_Location where status_id != -1 "
            strSQL &= " AND Warehouse_Index = '" & WareHouse_Index & "'"
            strSQL &= " AND Layout_Index = '" & Layout_Index & "'"

            strSQL &= " Group by Room,RoomName"

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
    Public Sub Search_Lock(ByVal WareHouse_Index As String, ByVal Room As String)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT     Lock   "
            strSQL &= " FROM  VIEW_Location where status_id != -1 "
            strSQL &= " AND Warehouse_Index = '" & WareHouse_Index & "'"
            strSQL &= " AND Room = '" & Room & "'"
            strSQL &= " Group by Lock"

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
    Public Sub Search_Level(ByVal WareHouse_Index As String, ByVal Room As String, ByVal Lock As String)

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT     Level   "
            strSQL &= " FROM  VIEW_Location where status_id != -1 "
            strSQL &= " AND Warehouse_Index = '" & WareHouse_Index & "'"
            strSQL &= " AND Room = '" & Room & "'"
            strSQL &= " AND Lock = '" & Lock & "'"
            strSQL &= " Group by Level"

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
    Public Sub SearchLocation_Layout(ByVal pLayout_Index As String)
        '  
        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT ms_Location.*,ms_Zone.Description as Zone,ms_LocationType.Description as LocationType ,ms_Layout.Split"
            strSQL &= "  ,SortAsc_Des = case dbo.ms_Location.SortAsc"
            strSQL &= "  when 0 then 'DESC'"
            strSQL &= "  else 'ASC' end"
            strSQL &= "  FROM   ms_Location LEFT OUTER JOIN"
            strSQL &= "            ms_Zone ON dbo.ms_Location.Zone_Index = ms_Zone.Zone_Index LEFT OUTER JOIN"
            strSQL &= "             ms_LocationType ON dbo.ms_Location.LocationType_Index = ms_LocationType.LocationType_Index  LEFT OUTER JOIN "
            strSQL &= "    ms_Layout on  ms_Layout.Layout_Index = dbo.ms_Location.Layout_Index"
            strSQL &= "    where ms_Location.status_id != -1          "
            strSQL &= "     AND ms_Location.Layout_Index = '" & pLayout_Index & "'"

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
    Public Sub SelectShowData(ByVal condition As String, ByVal Key As String, Optional ByVal ptrActionID As String = "")
        Dim sbSql As New StringBuilder
        Try


            'sbSql.Append(" SELECT  ms_Location.Location_Alias, ms_Location.Location_Id, ms_Location.Location_Index, ms_LocationType.LocationType_Index, ")
            'sbSql.Append("              ms_LocationType.Description AS LocationType, ms_Warehouse.Warehouse_No, ms_Warehouse.Description AS warehose, ")
            'sbSql.Append("              ms_Location.Max_Qty, ms_Location.Max_Weight, ms_Location.Max_Volume,ms_Location.Max_Pallet, ms_Room.Room_Id, ms_Room.Description AS Room, ")
            'sbSql.Append("              ms_Location.Lock, ms_Location.Row, ms_Location.Level, ms_Location.depth,ms_Zone.Description AS Zone")
            'sbSql.Append(" FROM         dbo.ms_Location LEFT  JOIN")
            'sbSql.Append("              dbo.ms_Warehouse ON dbo.ms_Location.Warehouse_Index = dbo.ms_Warehouse.Warehouse_Index LEFT  JOIN")
            'sbSql.Append("              dbo.ms_Zone ON dbo.ms_Location.Zone_Index = dbo.ms_Zone.Zone_Index LEFT  JOIN")
            'sbSql.Append("              dbo.ms_Room ON dbo.ms_Location.room = dbo.ms_Room.room_Index LEFT  JOIN")
            'sbSql.Append("              dbo.ms_LocationType ON dbo.ms_Location.LocationType_Index = dbo.ms_LocationType.LocationType_Index")
            'sbSql.Append("  WHERE       ms_Location.status_id <> -1")

            sbSql.Append(" SELECT  ms_Location.Location_Alias, ms_Location.Location_Id, ms_Location.Location_Index, ms_LocationType.LocationType_Index, ")
            sbSql.Append("              ms_LocationType.Description AS LocationType, ms_Warehouse.Warehouse_No, ms_Warehouse.Description AS warehose, ")
            sbSql.Append("              ms_Location.Max_Qty, ms_Location.Max_Weight, ms_Location.Max_Volume,ms_Location.Max_Pallet, ms_Location.Action_ID, ms_Room.Room_Id, ms_Room.Description AS Room, ")
            sbSql.Append("              ms_Location.Lock, ms_Location.Row, ms_Location.Level, ms_Location.depth,ms_Zone.Description AS Zone, ms_LocationAction.Status_Th As Action_Des, ms_Location.Score,ms_Location.isNotStorageCharge ")
            sbSql.Append(" FROM         dbo.ms_Location LEFT  JOIN")
            sbSql.Append("              dbo.ms_Warehouse ON dbo.ms_Location.Warehouse_Index = dbo.ms_Warehouse.Warehouse_Index LEFT  JOIN")
            sbSql.Append("              dbo.ms_Zone ON dbo.ms_Location.Zone_Index = dbo.ms_Zone.Zone_Index LEFT  JOIN")
            sbSql.Append("              dbo.ms_Room ON dbo.ms_Location.room = dbo.ms_Room.room_Index LEFT  JOIN")
            sbSql.Append("              dbo.ms_LocationType ON dbo.ms_Location.LocationType_Index = dbo.ms_LocationType.LocationType_Index LEFT JOIN")
            sbSql.Append("              dbo.ms_LocationAction ON dbo.ms_Location.Action_Id = dbo.ms_LocationAction.Action_Id")
            sbSql.Append("  WHERE       ms_Location.status_id <> -1")

            If Not Key = "" Then
                If Key = "1" Then
                    sbSql.Append(" AND   ms_Warehouse.Description Like '" & condition & "%'")
                End If
                If Key = "2" Then
                    sbSql.Append(" AND  ms_LocationType.Description Like '" & condition & "%'")
                End If
                If Key = "3" Then
                    sbSql.Append(" AND  ms_Location.Location_Index = '" & condition & "'")
                End If
                If Key = "4" Then
                    sbSql.Append(" AND  ms_Location.Location_Alias Like '" & condition & "%'")
                End If
                If Key = "5" Then
                    sbSql.Append(" AND  ms_Room.Description Like '" & condition & "%'")
                End If
                If Key = "6" Then
                    sbSql.Append(" AND  ms_Zone.Description Like '" & condition & "%'")
                End If
            End If

            If ptrActionID <> "" Then
                sbSql.Append(" AND  ms_Location.Action_ID <> " & Val(ptrActionID))
            End If

            sbSql.Append("  Order By ms_Location.Location_Alias ")

            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Function getCloneLocationHeader() As DataTable
        Dim sbSql As New StringBuilder
        Try
            sbSql.Append(" SELECT TOP (1) ms_Location.Location_Alias, ms_Location.Location_Id, ms_Location.Location_Index, ms_LocationType.LocationType_Index, ")
            sbSql.Append("              ms_LocationType.Description AS LocationType, ms_Warehouse.Warehouse_No, ms_Warehouse.Description AS warehose, ")
            sbSql.Append("              ms_Location.Max_Qty, ms_Location.Max_Weight, ms_Location.Max_Volume,ms_Location.Max_Pallet, ms_Location.Action_ID, ms_Room.Room_Id, ms_Room.Description AS Room, ")
            sbSql.Append("              ms_Location.Lock, ms_Location.Row, ms_Location.Level, ms_Location.depth,ms_Zone.Description AS Zone, ms_LocationAction.Status_Th As Action_Des, ms_Location.Score,ms_Location.isNotStorageCharge ")
            sbSql.Append(" FROM         dbo.ms_Location LEFT  JOIN")
            sbSql.Append("              dbo.ms_Warehouse ON dbo.ms_Location.Warehouse_Index = dbo.ms_Warehouse.Warehouse_Index LEFT  JOIN")
            sbSql.Append("              dbo.ms_Zone ON dbo.ms_Location.Zone_Index = dbo.ms_Zone.Zone_Index LEFT  JOIN")
            sbSql.Append("              dbo.ms_Room ON dbo.ms_Location.room = dbo.ms_Room.room_Index LEFT  JOIN")
            sbSql.Append("              dbo.ms_LocationType ON dbo.ms_Location.LocationType_Index = dbo.ms_LocationType.LocationType_Index LEFT JOIN")
            sbSql.Append("              dbo.ms_LocationAction ON dbo.ms_Location.Action_Id = dbo.ms_LocationAction.Action_Id")
            Return Me.dataColumnAllowNull(Me.dataColumnReadOnly(DBExeQuery(sbSql.ToString()).Clone(), False), True)
        Catch ex As Exception
            Throw ex
        Finally
            sbSql = Nothing
        End Try
    End Function
    Public Function dataColumnReadOnly(ByVal dt As DataTable, ByVal Read As Boolean, ByVal ParamArray Column() As String) As DataTable
        Try
            For Each dc As DataColumn In dt.Columns
                If Column.Length = 0 Then
                    dc.ReadOnly = Read
                Else
                    For Each col As String In Column
                        If col.Equals(dc.ColumnName) Then
                            dc.ReadOnly = Read
                            Exit For
                        End If
                    Next
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function dataColumnAllowNull(ByVal dt As DataTable, ByVal Allow As Boolean, ByVal ParamArray Column() As String) As DataTable
        Try
            For Each dc As DataColumn In dt.Columns
                If Column.Length = 0 Then
                    dc.AllowDBNull = Allow
                Else
                    For Each col As String In Column
                        If col.Equals(dc.ColumnName) Then
                            dc.AllowDBNull = Allow
                            Exit For
                        End If
                    Next
                End If
            Next
            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Sub SelectShowData(ByVal condition As String, ByVal Key As String)
    '    Dim sbSql As New StringBuilder
    '    Try




    '        sbSql.Append(" SELECT  ms_Location.Location_Alias, ms_Location.Location_Id, ms_Location.Location_Index, ms_LocationType.LocationType_Index, ")
    '        sbSql.Append("              ms_LocationType.Description AS LocationType, ms_Warehouse.Warehouse_No, ms_Warehouse.Description AS warehose, ")
    '        sbSql.Append("              ms_Location.Max_Qty, ms_Location.Max_Weight, ms_Location.Max_Volume, ms_Room.Room_Id, ms_Room.Description AS Room, ")
    '        sbSql.Append("              ms_Location.Lock, ms_Location.Row, ms_Location.Level, ms_Location.depth,ms_Zone.Description AS Zone")
    '        sbSql.Append(" FROM         dbo.ms_Location LEFT  JOIN")
    '        sbSql.Append("              dbo.ms_Warehouse ON dbo.ms_Location.Warehouse_Index = dbo.ms_Warehouse.Warehouse_Index LEFT  JOIN")
    '        sbSql.Append("              dbo.ms_Zone ON dbo.ms_Location.Zone_Index = dbo.ms_Zone.Zone_Index LEFT  JOIN")
    '        sbSql.Append("              dbo.ms_Room ON dbo.ms_Location.room = dbo.ms_Room.room_Index LEFT  JOIN")
    '        sbSql.Append("              dbo.ms_LocationType ON dbo.ms_Location.LocationType_Index = dbo.ms_LocationType.LocationType_Index")
    '        sbSql.Append("  WHERE       ms_Location.status_id <> -1")

    '        If Not Key = "" Then
    '            If Key = "1" Then
    '                sbSql.Append(" AND   ms_Warehouse.Description Like '" & condition & "%'")
    '            End If
    '            If Key = "2" Then
    '                sbSql.Append(" AND  ms_LocationType.Description Like '" & condition & "%'")
    '            End If
    '            If Key = "3" Then
    '                sbSql.Append(" AND  ms_Location.Location_Index = '" & condition & "'")
    '            End If
    '            If Key = "4" Then
    '                sbSql.Append(" AND  ms_Location.Location_Alias Like '" & condition & "%'")
    '            End If
    '        End If


    '        SetSQLString = sbSql.ToString
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub
    Public Sub SelectShowDataNotJoin(ByVal condition As String, ByVal Key As String)
        Dim sbSql As New StringBuilder
        Try


            sbSql.Append(" SELECT ms_Location.Location_Alias, ms_Location.Location_Id, ms_Location.Location_Index, ms_Location.LocationType_Index, ")
            sbSql.Append(" ms_Location.Max_Qty, ms_Location.Max_Weight, ms_Location.Max_Volume, ms_Location.Room,ms_Location.Warehouse_Index,ms_Location.Zone_Index,  ")
            sbSql.Append(" ms_Location.Lock, ms_Location.Row, ms_Location.Level, ms_Location.Depth")

            'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
            sbSql.Append(" ,ms_Location.Allow_Sugguest_Putaway")
            sbSql.Append(" ,ms_Location.Allow_Sugguest_Pick")
            sbSql.Append(" ,ms_Location.Max_Pallet,ms_Location.Action_Id,ms_Location.Score")

            sbSql.Append(" FROM ms_Location")
            sbSql.Append(" WHERE ms_Location.status_id <> -1")

            If Not Key = "" Then
                If Key = "3" Then
                    sbSql.Append(" AND ms_Location.Location_Index = '" & condition & "'")
                End If
            End If


            SetSQLString = sbSql.ToString
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
                         " FROM       ms_Location where status_id != -1"

                strWhere = ""
            Else
                ' Sql for define ColumnName & Filter Value 
                strSQL = " SELECT " & ColumnName & _
                         " FROM       ms_Location where status_id != -1 "

                strWhere = pFilterValue
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
    Public Sub SearchDataLevel(ByVal location As String, ByVal WH As String, ByVal Room As String)
        '  
        'Dim strSQL As String = ""
        'Dim strWhere As String = ""
        'Dim Lock As String = ""
        'Dim row As String = ""

        'If location <> Nothing Then
        '    Lock = location.Substring(0, 1)
        '    row = location.Substring(1, location.Length - 1)
        'End If


        'Try
        '    Select Case CastType
        '        Case 0 'all 
        '        Case 1 'Customer ID
        '            strWhere = " and Customer_Id like '" & keyWord & "%'"
        '        Case 2 'Sku_ID
        '            strWhere = " and  Sku_Id like '" & keyWord & "%'"
        '        Case 3 'Lot ผลิต
        '            strWhere = " and  PLot like '" & keyWord & "%'"
        '        Case 4 'วันหมดอายุ
        '            strWhere = " and Exp_Date " & keyWord
        '        Case 5 ' zone
        '            strWhere = " and Zone like '" & keyWord & "%'"
        '        Case 6 ' location type
        '            strWhere = " and LocationType like '" & keyWord & "'"
        '    End Select
        '    strSQL = " SELECT max(level) as level ,max(Depth) as Depth" & _
        '             " FROM VIEW_LocationBalance where status_Location != -1 and lock ='" & Lock & "' and Row ='" & row & "'" & _
        '             " and Warehouse_Index = '" & WH & "' and Room = '" & Room & "'"
        '    SetSQLString = strSQL & strWhere

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim Lock As String = ""
        Dim row As String = ""

        If location <> Nothing Then
            Lock = location.Substring(0, 1)
            row = location.Substring(1, location.Length - 1)
        End If


        Try

            strSQL = " SELECT max(level) as level ,max(Depth) as Depth" & _
                     " FROM ms_Location where status_id != -1 and lock ='" & Lock & "' and Row ='" & row & "'" & _
                     " and Warehouse_Index = '" & WH & "' and Room = '" & Room & "'"
            strWhere = ""




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
    Public Sub SelectData_For_Edit(ByVal pFilterValue As String)
        Dim strSQL As String
        Dim strWhere As String
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       ms_Location "

            strWhere = ""

            strWhere += " WHERE  ms_Location.location_Index = '" & pFilterValue & "' and status_id != -1"

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
    Public Sub SelectDataPerBox1(ByVal location As String, ByVal depth As Integer, ByVal level As Integer, ByVal Warehouse_Index As String, ByVal Room As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim Lock As String = ""
        Dim row As String = ""

        If location <> Nothing Then
            Lock = location.Substring(0, 1)
            row = location.Substring(1, location.Length - 1)
        End If
        Try

            strSQL = " SELECT *"
            strSQL &= " FROM ms_Location where status_id != -1 and lock ='" & Lock & "' and Row ='" & row & "'"
            strSQL &= " and Depth =" & depth & " and level=" & level
            strSQL &= " and Warehouse_Index =" & Warehouse_Index & " and Room=" & Room
            strWhere = ""

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
    Public Sub SelectDataPerBox(ByVal location As String, ByVal depth As Integer, ByVal level As Integer)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim Lock As String = ""
        Dim row As String = ""

        If location <> Nothing Then
            Lock = location.Substring(0, 1)
            row = location.Substring(1, location.Length - 1)
        End If


        Try

            strSQL = " SELECT *"
            strSQL &= " FROM ms_Location where status_id != -1 and lock ='" & Lock & "' and Row ='" & row & "'"
            strSQL &= " and Depth =" & depth & " and level=" & level

            strWhere = ""




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
    Public Sub SelectSideViewData(ByVal fldWareHouse As String, ByVal fldRoom As String, ByVal fldLockRow As String)
        Dim strSQL As String = ""
        Try



            'strSQL = " SELECT LOCATION_ID,LEVEL,DEPTH,CURRENTWEIGHT,MAXWEIGHT,CURRENTVOLUME,MAXVOLUME " & _
            strSQL = " SELECT * " & _
                            " FROM ms_LOCATION " & _
                            " WHERE Warehouse_Index = '" & fldWareHouse & "'" & _
                            " AND ROOM = '" & fldRoom & "'" & _
                            " AND LOCK+ROW = '" & fldLockRow & "' and status_id != -1" & _
                            " ORDER BY  LEVEL DESC,DEPTH DESC"


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
    Public Sub SelectLocationHiglight1(ByVal fldRoom As String, ByVal fldWareHouse As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT     Lock+ convert(varchar,Row), Depth " & _
                     " FROM       ms_Location " & _
                     " WHERE     (Current_Qty > 0) AND (Room = '" & fldRoom & "') AND (Warehouse_Index = '" & fldWareHouse & "') and status_id != -1" & _
                     " GROUP BY Lock, Row, Depth "
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
    Public Sub SelectLocationHiglight2(ByVal fldRoom As String, ByVal fldWareHouse As String, ByVal fldPercent As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  Lock+ Row , Depth " & _
            " FROM    ms_Location as Lc1 " & _
            " WHERE   ( " & _
            "         SELECT SUM(Current_Weight) *100 / SUM(Max_Weight) AS cal1 FROM ms_Location " & _
            "         WHERE     (Room = '" & fldRoom & "') AND (Warehouse_Index = '" & fldWareHouse & "') AND (Lock + Row = Lc1.Lock+Lc1.Row)  and depth = Lc1.Depth " & _
            "         ) < " & fldPercent & " " & _
            "         AND Current_Weight > 0 " & _
            "         AND (Room = '" & fldRoom & "') AND (Warehouse_Index = '" & fldWareHouse & "') and status_id != -1" & _
            " GROUP BY  Lock, Row, Depth   Order by Lock+ Row DESC "

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
    Public Sub SelectLocation(ByVal pStrWareHouse As String, ByVal pStrRoom As String, ByVal pStrLock As String)
        Dim strSQL As String = ""
        Try
            'strSQL = "select L.*,R.Description As Room_Name ,W.Description As Warehouse_Name"
            'strSQL &= " from ms_location L LEFT JOIN ms_Warehouse W ON "
            'strSQL &= "     L.Warehouse_Index = W.Warehouse_Index "
            'strSQL &= "     LEFT JOIN ms_Room R ON L.Room = R.Room_Index  "
            strSQL &= " Select L.*,R.Description As Room_Name ,W.Description As Warehouse_Name,A.Status_th "
            strSQL &= " from ms_location L LEFT JOIN ms_Warehouse W ON "
            strSQL &= " L.Warehouse_Index = W.Warehouse_Index "
            strSQL &= " LEFT JOIN ms_Room R ON L.Room = R.Room_Index "
            strSQL &= " LEFT JOIN ms_locationAction A ON L.Action_Id=A.Action_Id "
            strSQL &= " where L.warehouse_index = '" & pStrWareHouse & "' and L.Room = '" & pStrRoom & "' AND L.LOCK='" & pStrLock & "' "
            strSQL &= " ORDER BY CAST(L.Row As INT)  , CAST (L.Level As INT) , CAST(L.Depth As INT) "
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
    Public Sub SelectLocationType(ByVal Filter As String)
        Dim strSQL As String = ""
        Try
            strSQL &= " Select * from ms_LocationAction "
            If Filter <> "" Then
                strSQL &= Filter
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
    Public Sub SelectLocationLayoutIndex(ByVal pStrWareHouse As String, ByVal pStrRoom As String, ByVal pStrLayoutIndex As String)
        Dim strSQL As String = ""
        Try
            strSQL = "select L.*,R.Description As Room_Name ,W.Description As Warehouse_Name"
            strSQL &= " from ms_location L LEFT JOIN ms_Warehouse W ON "
            strSQL &= "     L.Warehouse_Index = W.Warehouse_Index "
            strSQL &= "     LEFT JOIN ms_Room R ON L.Room = R.Room_Index  "
            'strSQL &= "     LEFT JOIN ms_LocationAction A ON L.Action_Id =A.Action_Id  "
            strSQL &= " where L.warehouse_index = '" & pStrWareHouse & "' and L.Room = '" & pStrRoom & "' and L.Layout_Index= '" & pStrLayoutIndex & "' "
            strSQL &= "     ORDER BY CAST(L.Row As INT)  , CAST (L.Level As INT) , CAST(L.Depth As INT)"
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
    Public Sub SelectLocationLock(ByVal pStrWareHouse As String, ByVal pStrRoom As String, ByVal Lock As String)
        Dim strSQL As String = ""
        Try
            strSQL = "select L.*,R.Description As Room_Name ,W.Description As Warehouse_Name"
            strSQL &= " from ms_location L LEFT JOIN ms_Warehouse W ON "
            strSQL &= "     L.Warehouse_Index = W.Warehouse_Index "
            strSQL &= "     LEFT JOIN ms_Room R ON L.Room = R.Room_Index  "
            'strSQL &= "     LEFT JOIN ms_LocationAction A ON L.Action_Id =A.Action_Id  "
            strSQL &= " where L.warehouse_index = '" & pStrWareHouse & "' and L.Room = '" & pStrRoom & "' and L.Lock= '" & Lock & "' "
            strSQL &= "     ORDER BY CAST(L.Row As INT)  , CAST (L.Level As INT) , CAST(L.Depth As INT)"
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
    Public Sub SelectLocationLockByDepth(ByVal pStrWareHouse As String, ByVal pStrRoom As String, ByVal Lock As String, ByVal Depth As String)
        Dim strSQL As String = ""
        Try
            strSQL = "select L.*,R.Description As Room_Name ,W.Description As Warehouse_Name"
            strSQL &= " from ms_location L LEFT JOIN ms_Warehouse W ON "
            strSQL &= "     L.Warehouse_Index = W.Warehouse_Index "
            strSQL &= "     LEFT JOIN ms_Room R ON L.Room = R.Room_Index  "
            'strSQL &= "     LEFT JOIN ms_LocationAction A ON L.Action_Id =A.Action_Id  "
            strSQL &= " where L.warehouse_index = '" & pStrWareHouse & "' and L.Room = '" & pStrRoom & "' and L.Lock= '" & Lock & "' and L.Depth= '" & Depth & "' "
            strSQL &= "     ORDER BY CAST(L.Row As INT)  , CAST (L.Level As INT) , CAST(L.Depth As INT)"
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
    Public Sub SelectLocationLockByRow(ByVal pStrWareHouse As String, ByVal pStrRoom As String, ByVal Lock As String, ByVal Row As String)
        Dim strSQL As String = ""
        Try
            strSQL = "select L.*,R.Description As Room_Name ,W.Description As Warehouse_Name"
            strSQL &= " from ms_location L LEFT JOIN ms_Warehouse W ON "
            strSQL &= "     L.Warehouse_Index = W.Warehouse_Index "
            strSQL &= "     LEFT JOIN ms_Room R ON L.Room = R.Room_Index  "
            'strSQL &= "     LEFT JOIN ms_LocationAction A ON L.Action_Id =A.Action_Id  "
            strSQL &= " where L.warehouse_index = '" & pStrWareHouse & "' and L.Room = '" & pStrRoom & "' and L.Lock= '" & Lock & "' and L.Row= '" & Row & "' "
            strSQL &= "     ORDER BY CAST(L.Row As INT)  , CAST (L.Level As INT) , CAST(L.Depth As INT)"
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
    Public Sub SelectRowByLayoutIndex(ByVal pLayout_Index As String)
        Dim strSQL As String = ""
        If pLayout_Index <> "" Then
            Try
                strSQL = "SELECT     Row "
                strSQL &= "FROM ms_Location "
                strSQL &= "WHERE     Layout_Index = '" & pLayout_Index & "' "
                strSQL &= "GROUP BY  Row "
                strSQL &= "ORDER BY  Row "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub SelectRowToBeRackByLock(ByVal Lock As String, ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String)
        Dim strSQL As String = ""
        If Lock <> "" Then
            Try
                strSQL = " SELECT     Row "
                strSQL &= " FROM ms_Location "
                strSQL &= " WHERE     Lock = '" & Lock & "' And Room='" & pStrRoom_index & "' And Warehouse_Index='" & pStrWareHouse_index & "' " 'ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String
                strSQL &= " GROUP BY  Row "
                strSQL &= " ORDER BY  Row "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub SelectDepthByLayoutIndex(ByVal pLayout_Index As String)
        Dim strSQL As String = ""
        If pLayout_Index <> "" Then
            Try
                strSQL = " SELECT     Depth "
                strSQL &= " FROM ms_Location "
                strSQL &= " WHERE     Layout_Index = '" & pLayout_Index & "' "
                strSQL &= " GROUP BY  Depth "
                strSQL &= " ORDER BY  Depth "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub SelectDepthToBeRackByLock(ByVal Lock As String, ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String)
        Dim strSQL As String = ""
        If Lock <> "" Then
            Try
                strSQL = " SELECT     Depth "
                strSQL &= " FROM ms_Location "
                strSQL &= " WHERE     Lock = '" & Lock & "' And Room='" & pStrRoom_index & "' And Warehouse_Index='" & pStrWareHouse_index & "' " 'ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String
                strSQL &= " GROUP BY  Depth "
                strSQL &= " ORDER BY  Depth "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select2DByRow(ByVal pLayout_Index As String, ByVal Row As String)
        Dim strSQL As String = ""
        If pLayout_Index <> "" And Row <> "" Then
            Try
                strSQL = "SELECT     Row, Depth, [LEVEL],Current_Qty,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "FROM  ms_Location "
                strSQL &= "WHERE     Layout_Index = '" & pLayout_Index & "' "
                strSQL &= "GROUP BY Row, Depth, [LEVEL],Current_Qty ,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "Having Row='" & Row & "'"
                strSQL &= "ORDER BY Row ASC, [LEVEL] DESC,Depth ASC "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select2DToBeRackByRow(ByVal Lock As String, ByVal Row As String, ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String)
        Dim strSQL As String = ""
        If Lock <> "" And Row <> "" Then
            Try
                strSQL = "SELECT     Row, Depth, [LEVEL],Current_Qty,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "FROM  ms_Location "
                strSQL &= "WHERE     Lock = '" & Lock & "' And Room='" & pStrRoom_index & "' And Warehouse_Index='" & pStrWareHouse_index & "'  " 'ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String
                strSQL &= "GROUP BY Row, Depth, [LEVEL],Current_Qty ,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "Having Row='" & Row & "'"
                strSQL &= "ORDER BY Row ASC, [LEVEL] DESC,Depth ASC "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select2DByDepth(ByVal pLayout_Index As String, ByVal Depth As String)
        Dim strSQL As String = ""
        If pLayout_Index <> "" And Depth <> "" Then
            Try
                strSQL = "SELECT     Depth,[LEVEL],Row,Current_Qty ,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "FROM ms_Location "
                strSQL &= "WHERE     Layout_Index = '" & pLayout_Index & "' "
                strSQL &= "GROUP BY Depth,[LEVEL],Row,Current_Qty ,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "Having Depth='" & Depth & "'"
                strSQL &= "ORDER BY Depth ASC,[LEVEL] DESC,Row ASC "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select2DToBeRackByDepth(ByVal Lock As String, ByVal Depth As String, ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String)
        Dim strSQL As String = ""
        If Lock <> "" And Depth <> "" Then
            Try
                strSQL = "SELECT     Depth,[LEVEL],Row,Current_Qty ,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "FROM ms_Location "
                strSQL &= "WHERE     Lock = '" & Lock & "' And Room='" & pStrRoom_index & "' And Warehouse_Index='" & pStrWareHouse_index & "'  "
                strSQL &= "GROUP BY Depth,[LEVEL],Row,Current_Qty ,Location_Index,Location_Alias,Action_Id,Max_Qty "
                strSQL &= "Having Depth='" & Depth & "'"
                strSQL &= "ORDER BY Depth ASC,[LEVEL] DESC,Row ASC "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select2DToBeRackByDepthV2(ByVal Lock As String, ByVal Depth As String, ByVal pStrRoom_index As String, ByVal pStrWareHouse_index As String, ByVal pBoolSortAsc As Boolean)
        Dim strSQL As String = ""
        Dim oCustomConfig As New config_CustomSetting
        If Lock <> "" And Depth <> "" Then
            Try

                strSQL &= " SELECT     ms_Location.Depth,ms_Location.[LEVEL],ms_Location.Row,ms_Location.Current_Qty "
                strSQL &= "             ,ms_Location.Location_Index,ms_Location.Location_Alias,ms_Location.Action_Id,ms_Location.Max_Qty "
                strSQL &= "             ,ms_Layout.Split"
                strSQL &= " FROM        ms_Location inner join "
                strSQL &= "             ms_Layout on ms_Layout.Layout_Index = ms_Location.Layout_Index"
                strSQL &= " WHERE       ms_Location.Lock = '" & Lock & "' And ms_Location.Room='" & pStrRoom_index & "' And ms_Location.Warehouse_Index='" & pStrWareHouse_index & "'  "
                strSQL &= " GROUP BY    ms_Location.Depth,ms_Location.[LEVEL],ms_Location.Row,ms_Location.Current_Qty ,ms_Location.Location_Index"
                strSQL &= "             ,ms_Location.Location_Alias,ms_Location.Action_Id,ms_Location.Max_Qty ,ms_Layout.Split"
                strSQL &= " Having      ms_Location.Depth='1'"

                Select Case pBoolSortAsc
                    Case False
                        strSQL &= " ORDER BY    ms_Location.Depth ASC,ms_Location.[LEVEL] ASC,ms_Location.Row desc "
                    Case True
                        strSQL &= " ORDER BY    ms_Location.Depth ASC,ms_Location.[LEVEL] ASC,ms_Location.Row ASC "
                End Select

                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select3dMaxByLayoutIndex(ByVal pLayout_Index As String)
        Dim strSQL As String = ""
        If pLayout_Index <> "" Then
            Try
                strSQL = "SELECT     Max(Row)As MaxRow, Max(Depth) As MaxDepth, Max([LEVEL]) As MaxLevel "
                strSQL &= "FROM ms_Location "
                strSQL &= "WHERE     Layout_Index = '" & pLayout_Index & "' "
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub Select3dMaxToBeRackByLock(ByVal Lock As String, ByVal Room As String, ByVal Warehouse_Index As String)
        Dim strSQL As String = ""
        If Lock <> "" Then
            Try
                strSQL = "SELECT     Max(Row)As MaxRow, Max(Depth) As MaxDepth, Max([LEVEL]) As MaxLevel "
                strSQL &= "FROM ms_Location "
                strSQL &= "WHERE     Lock = '" & Lock & "' And Room='" & Room & "' And Warehouse_Index='" & Warehouse_Index & "'"
                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        End If
    End Sub
    Public Sub selectLocationAliasByFloorLayoutIndex(ByVal LayoutIndex As String)
        Dim strSQL As String = ""
        Try
            strSQL &= "Select Location_Alias,Location_Index "
            strSQL &= "FROM ms_Location  "
            strSQL &= "WHERE Layout_Index = '" & LayoutIndex & "' "
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
    ''' <summary>
    ''' 
    ''' </summary>
    ''' 13-01-2010
    ''' by ja
    ''' update select ms_Location.Action_Id
    ''' <remarks></remarks>
    Public Sub SelectCurQtyByFloorloadLayoutIndex(ByVal Layout_Index As String)
        Dim strSQL As String = ""
        Try


            strSQL &= " SELECT     ms_Location.Location_index, ms_Location.Location_Id, ms_Layout.Layout_Index, ms_Location.Current_Qty, "
            strSQL &= " ms_Layout.ObjectType,ms_Location.Action_Id "
            strSQL &= " FROM       ms_Location INNER JOIN "
            strSQL &= " ms_Layout ON ms_Location.Layout_Index = ms_Layout.Layout_Index"
            strSQL &= " WHERE     ms_Layout.ObjectType = 'Floor_load'"
            strSQL &= " And  ms_Layout.Layout_Index ='" & Layout_Index & "'"
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


    Public Sub Search_Lock(ByVal pstrLock As String)
        '  
        Dim strSQL As String = ""
        Try
            If pstrLock = "" Then
                strSQL = " SELECT     Distinct  lock"
            Else
                strSQL = " SELECT     lock"
            End If

            strSQL &= " FROM  ms_Location"

            If pstrLock <> "" Then
                strSQL &= " WHERE  lock='" & pstrLock & "'"
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
    Public Sub getLocation_Lock(ByVal pAction_Id As String)
        Dim strSQL As String = ""
        Try
            strSQL = " select chkSelect = (convert(bit,0)) ,WH.Description as WareHouse,AC.Status_Th as Action,ML.*"
            strSQL &= "  from 	ms_Location ML inner join"
            strSQL &= "  ms_WareHouse WH on WH.WareHouse_Index = ML.WareHouse_Index Left outer join"
            strSQL &= "  ms_LocationAction AC on AC.Action_Id = ML.Action_Id"
            strSQL &= "   WHERE ML.status_id != -1"
            strSQL &= "  AND  	ML.Action_Id not in (1)"
            strSQL &= "  AND  	ML.Action_Id =" & pAction_Id
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

    Public Function isUSE_Location(ByVal pLocation_Alias As String) As Boolean
        Try

            Dim strSQL As String = ""
            Try

                strSQL = " SELECT * from tb_locationbalance inner join ms_location "
                strSQL &= " on tb_locationbalance.location_index=ms_location.location_index  "
                strSQL &= " WHERE qty_bal>0 and Location_Alias  = '" & pLocation_Alias & "'"

                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
                If _dataTable Is Nothing Then Return False

                If _dataTable.Rows.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function isUseLocation_Index(ByVal Location_Index As String) As Boolean
        Try
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Index", SqlDbType.VarChar).Value = Location_Index
            Return DBExeQuery("SELECT TOP (1) 1 FROM tb_LocationBalance WHERE Location_Index = @Location_Index AND Qty_Bal > 0").Rows.Count > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function getRoom() As DataTable
        Try
            Return DBExeQuery("SELECT Room_Index,Description As Room FROM ms_Room where status_id <> -1")
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region " INSERT DATA "

    Public Function InsertCopyLocation_Layout(ByVal pLayout_Index As String, ByVal count As Integer) As String
        Dim strSQL As String
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Dim newLayout_Index As String = ""
        Try

            strSQL = "     SELECT   Max_Qty,Max_Weight,Max_Volume,Layout_Index,Location_Index,Location_Id,Location_Alias,Warehouse_Index,Row,Room,Lock,Depth,Level,LocationType_Index,Zone_Index,add_by,add_branch,add_date,Update_by,Update_branch,Update_date"
            strSQL &= "     FROM    ms_Location"
            strSQL &= "     WHERE   Layout_Index='" & pLayout_Index & "'"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            DS = New DataSet
            DataAdapter.Fill(DS, "tbl")


            Dim objDBIndex As New Sy_AutoNumber
            newLayout_Index = objDBIndex.getSys_Value("Layout_Index")
            objDBIndex = Nothing


            If DS.Tables("tbl").Rows.Count <> 0 Then
                For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1
                    strSQL = " insert into ms_Location (Layout_Index,Location_Index,Location_Id,Location_Alias,Warehouse_Index,Row,Room,Lock,Depth,Level,Max_Qty,Max_Weight,Max_Volume,LocationType_Index,Zone_Index"
                    strSQL &= " ,add_by,add_branch,add_date,Update_by,Update_branch,Update_date) "
                    strSQL &= " values (@Layout_Index,@Location_Index,@Location_Id,@Location_Alias,@Warehouse_Index,@Row ,@Room,@Lock,@Depth,@Level,@Max_Qty,@Max_Weight,@Max_Volume,@LocationType_Index,@Zone_Index"
                    strSQL &= " ,@add_by,@add_branch,getdate(),@Update_by,@Update_branch,getdate())"

                    Dim objDBLIndex As New Sy_AutoNumber
                    DS.Tables("tbl").Rows(i).Item("Location_Index") = objDBLIndex.getSys_Value("Location_Index")
                    objDBLIndex = Nothing

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Layout_Index", SqlDbType.VarChar, 50).Value = newLayout_Index
                        .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Location_Id", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Warehouse_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Warehouse_Index").ToString
                        .Parameters.Add("@Row", SqlDbType.Int).Value = DS.Tables("tbl").Rows(i).Item("Row").ToString
                        .Parameters.Add("@Room", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Room").ToString

                        Me._lock = DS.Tables("tbl").Rows(i).Item("Lock").ToString & CDbl(count).ToString("0#") & CDbl(i).ToString("0#")

                        .Parameters.Add("@Lock", SqlDbType.VarChar, 50).Value = Me._lock
                        .Parameters.Add("@Depth", SqlDbType.Int).Value = CInt(DS.Tables("tbl").Rows(i).Item("Depth").ToString)
                        .Parameters.Add("@Level", SqlDbType.Int).Value = CInt(DS.Tables("tbl").Rows(i).Item("Level").ToString)
                        .Parameters.Add("@Location_Alias", SqlDbType.VarChar, 50).Value = Me._lock & "-" & CDbl(DS.Tables("tbl").Rows(i).Item("Row").ToString).ToString("0#") & "-" & CDbl(CInt(DS.Tables("tbl").Rows(i).Item("Depth").ToString)).ToString("0#") & "-" & CDbl(CInt(DS.Tables("tbl").Rows(i).Item("Level").ToString)).ToString("0#")
                        .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = CDbl(DS.Tables("tbl").Rows(i).Item("Max_Qty").ToString)
                        .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = CDbl(DS.Tables("tbl").Rows(i).Item("Max_Weight").ToString)
                        .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = CDbl(DS.Tables("tbl").Rows(i).Item("Max_Volume").ToString)
                        .Parameters.Add("@LocationType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("LocationType_Index").ToString
                        .Parameters.Add("@Zone_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Zone_Index").ToString
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                        .Parameters.Add("@Update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@Update_branch", SqlDbType.Int).Value = WV_Branch_ID
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                Next

            End If

            myTrans.Commit()
            Return newLayout_Index

        Catch ex As Exception
            myTrans.Rollback()
            Return False
        Finally
            disconnectDB()
        End Try

    End Function


    Public Function Insert() As String
        Dim strSQL As String
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            For Each _objItemLocation In _objItemCollection

                'strSQL = " DELETE ms_Location where Layout_Index = '" & _objItemLocation.Layout_Index & "'"
                'SetSQLString = strSQL
                'SetCommandType = DBType_SQLServer.enuCommandType.Text
                'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                'EXEC_Command()

                strSQL = " insert into ms_Location (Layout_Index,Location_Index,Location_Id,Location_Alias,Warehouse_Index,Row,Room,Lock,Depth,Level,Max_Pallet,Max_Qty,Max_Weight,Max_Volume,LocationType_Index,Zone_Index"

                'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
                strSQL &= " ,Allow_Sugguest_Putaway,Allow_Sugguest_Pick"
                strSQL &= " ,Current_Qty,Current_Weight,Current_Volume,Space_Used,Action_Id"

                strSQL &= " ,add_by,add_branch,add_date) "

                strSQL &= " values (@Layout_Index,@Location_Index,@Location_Id,@Location_Alias,@Warehouse_Index,@Row ,@Room,@Lock,@Depth,@Level,@Max_Pallet,@Max_Qty,@Max_Weight,@Max_Volume,@LocationType_Index,@Zone_Index"
                strSQL &= " ,@Allow_Sugguest_Putaway,@Allow_Sugguest_Pick"
                strSQL &= " ,@Current_Qty,@Current_Weight,@Current_Volume,@Space_Used,@Action_Id"

                strSQL &= " ,@add_by,@add_branch,getdate())"


                If _objItemLocation.Location_Index = "" Then
                    Dim objDBIndex As New Sy_AutoNumber
                    _objItemLocation.Location_Index = objDBIndex.getSys_Value("Location_Index")
                    objDBIndex = Nothing
                End If

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Layout_Index", SqlDbType.VarChar, 50).Value = _objItemLocation.Layout_Index
                    .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.Location_Index
                    .Parameters.Add("@Location_Id", SqlDbType.VarChar, 50).Value = _objItemLocation.Location_Index
                    .Parameters.Add("@Location_Alias", SqlDbType.VarChar, 50).Value = _objItemLocation.Location_Alias
                    .Parameters.Add("@Warehouse_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.Warehouse_Index
                    .Parameters.Add("@Row", SqlDbType.Int).Value = _objItemLocation.Row
                    .Parameters.Add("@Room", SqlDbType.VarChar, 50).Value = _objItemLocation.Room
                    .Parameters.Add("@Lock", SqlDbType.VarChar, 50).Value = _objItemLocation.Lock
                    .Parameters.Add("@Depth", SqlDbType.Int).Value = _objItemLocation.Depth
                    .Parameters.Add("@Level", SqlDbType.Int).Value = _objItemLocation.Level
                    .Parameters.Add("@Max_Pallet", SqlDbType.Float).Value = _objItemLocation.Max_Pallet
                    .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = _objItemLocation.Max_Qty
                    .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = _objItemLocation.Max_Weight
                    .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = _objItemLocation.Max_Volume
                    .Parameters.Add("@LocationType_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.LocationType_Index
                    .Parameters.Add("@Zone_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.Zone_Index
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    '.Parameters.Add("@Update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    '.Parameters.Add("@Update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                    'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
                    .Parameters.Add("@Allow_Sugguest_Putaway", SqlDbType.Int).Value = _objItemLocation.Allow_Sugguest_Putaway
                    .Parameters.Add("@Allow_Sugguest_Pick", SqlDbType.Int).Value = _objItemLocation.Allow_Sugguest_Pick

                    .Parameters.Add("@Current_Qty", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Current_Weight", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Current_Volume", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Space_Used", SqlDbType.Bit).Value = 0
                    .Parameters.Add("@Action_Id", SqlDbType.Int).Value = 1

                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next


            myTrans.Commit()
            Return _objItemLocation.Layout_Index

        Catch ex As Exception
            myTrans.Rollback()
            Return False
        Finally
            disconnectDB()
        End Try

    End Function
#End Region
#Region " UPDATE DATA "

    Public Sub UpdateUnlock(ByVal pLocation_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE ms_Location"
            strSQL &= " SET "
            strSQL &= "     Action_Id=1 "
            strSQL &= "   WHERE Location_Index = '" & pLocation_Index & "' "

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

    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Public Function Update() As String
        Dim strSQL As String = " "
        Try
            'strSQL = " UPDATE ms_Location" & _
            '" SET Location_Alias=@Location_Alias," & _
            '"     Warehouse_Index=@Warehouse_Index," & _
            '"     Room=@Room," & _
            '"     Lock=@Lock," & _
            '"     Depth=@Depth," & _
            '"     Max_Qty=@Max_Qty," & _
            '"     Max_Weight=@Max_Weight," & _
            '"     Max_Volume=@Max_Volume," & _
            '"     LocationType_Index=@LocationType_Index, " & _
            '"     Zone_index=@Zone_index " & _
            '"     WHERE Location_Index = @Location_Index"


            For Each _objItemLocation In _objItemCollection
                strSQL = " UPDATE ms_Location" & _
                                 " SET Location_Alias=@Location_Alias," & _
                                 "     Warehouse_Index=@Warehouse_Index," & _
                                 "     Row=@Row," & _
                                 "     Room=@Room," & _
                                 "     Lock=@Lock," & _
                                 "     Depth=@Depth," & _
                                 "     Level=@Level," & _
                                 "     Max_Qty=@Max_Qty," & _
                                 "     Max_Weight=@Max_Weight," & _
                                 "     Max_Volume=@Max_Volume," & _
                                 "     Max_Pallet=@Max_Pallet," & _
                                 "     LocationType_Index=@LocationType_Index, " & _
                                 "     Zone_index=@Zone_index " & _
                                 "     ,Allow_Sugguest_Putaway=@Allow_Sugguest_Putaway " & _
                                 "     ,Allow_Sugguest_Pick=@Allow_Sugguest_Pick " & _
                                 "     WHERE Location_Index = @Location_Index"

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Location_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.Location_Index
                    .Add("@Location_Alias", SqlDbType.VarChar, 50).Value = _objItemLocation.Location_Alias
                    .Add("@Warehouse_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.Warehouse_Index

                    .Add("@Row", SqlDbType.Int).Value = _objItemLocation.Row
                    .Add("@Room", SqlDbType.VarChar, 50).Value = _objItemLocation.Room
                    .Add("@Lock", SqlDbType.VarChar, 50).Value = _objItemLocation.Lock
                    .Add("@Depth", SqlDbType.Int).Value = _objItemLocation.Depth
                    .Add("@Level", SqlDbType.Int).Value = _objItemLocation.Level

                    .Add("@Max_Qty", SqlDbType.Float).Value = _objItemLocation.Max_Qty
                    .Add("@Max_Weight", SqlDbType.Float).Value = _objItemLocation.Max_Weight
                    .Add("@Max_Volume", SqlDbType.Float).Value = _objItemLocation.Max_Volume
                    .Add("@Max_Pallet", SqlDbType.Float).Value = _objItemLocation.Max_Pallet

                    .Add("@LocationType_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.LocationType_Index
                    .Add("@Zone_Index", SqlDbType.VarChar, 13).Value = _objItemLocation.Zone_Index


                    'krit update 30/08/2011 ; add เพิ่ม Allow_Sugguest_Putaway,Allow_Sugguest_Pick
                    .Add("@Allow_Sugguest_Putaway", SqlDbType.Int).Value = _objItemLocation.Allow_Sugguest_Putaway
                    .Add("@Allow_Sugguest_Pick", SqlDbType.Int).Value = _objItemLocation.Allow_Sugguest_Pick


                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                connectDB()
                EXEC_Command()
            Next



            Return _objItemLocation.Layout_Index

        Catch ex As Exception
            Return ""
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function

    Public Sub UpdateSplit(ByVal pLayout_Index As String, ByVal pSplit As Integer)
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE ms_Layout"
            strSQL &= " SET "
            strSQL &= "     Split=@Split "
            strSQL &= " WHERE Layout_Index = @Layout_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Layout_Index", SqlDbType.VarChar, 13).Value = pLayout_Index
                .Add("@Split", SqlDbType.Int, 10).Value = pSplit
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
    Public Sub UpdateSingle()
        Dim strSQL As String = " "
        Try
            strSQL = "      UPDATE ms_Location "
            strSQL &= "     SET Location_Alias=@Location_Alias, "
            strSQL &= "     Lock=@Lock, "
            strSQL &= "     Max_Qty=@Max_Qty, "
            strSQL &= "     Max_Weight=@Max_Weight, "
            strSQL &= "     Max_Volume=@Max_Volume, "
            strSQL &= "     LocationType_Index=@LocationType_Index, "
            strSQL &= "     Zone_index=@Zone_index, "
            strSQL &= "     Action_Id=@Action_Id, "
            strSQL &= "     Score=@Score, "
            strSQL &= "     Max_Pallet=@Max_Pallet "
            strSQL &= "     WHERE Location_Index = @Location_Index "
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = Me.Location_Index
                .Parameters.Add("@Location_Alias", SqlDbType.VarChar, 50).Value = Me.Location_Alias
                .Parameters.Add("@Lock", SqlDbType.VarChar, 50).Value = Me.Lock
                .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = Me.Max_Qty
                .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = Me.Max_Weight
                .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = Me.Max_Volume
                .Parameters.Add("@LocationType_Index", SqlDbType.VarChar, 13).Value = Me.LocationType_Index
                .Parameters.Add("@Action_Id", SqlDbType.Int).Value = Me.Action_Id
                .Parameters.Add("@Zone_Index", SqlDbType.VarChar, 13).Value = Me.Zone_Index
                .Parameters.Add("@Max_Pallet", SqlDbType.Float).Value = Me.Max_Pallet
                .Parameters.Add("@Score", SqlDbType.VarChar, 100).Value = Me.score
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
    Public Function Update_Action_Id(ByVal pAction_Id As Boolean, ByVal pLayout_Index As String, ByVal pLocation_Index As String, ByVal oprActive As enuOperation_Active) As Boolean
        Dim strSQL As String = " "
        Dim Action_Id As Integer = 0
        Try
            strSQL = " UPDATE ms_Location"
            strSQL &= " SET Action_Id=@Action_Id"
            If pLocation_Index <> "" Then
                strSQL &= "           WHERE          Location_Index = @Location_Index"
            Else
                strSQL &= "           WHERE          Layout_Index = @Layout_Index"
            End If

            Select Case oprActive

                Case enuOperation_Active.ACTIVE
                    strSQL &= " AND Current_Qty = 0"
                    If pAction_Id Then
                        Action_Id = 1
                    Else
                        Action_Id = 0
                    End If

                Case enuOperation_Active.RESERV
                    strSQL &= " AND Current_Qty = 0"
                    If pAction_Id Then
                        Action_Id = 2
                    Else
                        Action_Id = 1
                    End If
                Case enuOperation_Active.BLOCK
                    If pAction_Id Then
                        Action_Id = 3
                    Else
                        Action_Id = 1
                    End If

            End Select
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Layout_Index", SqlDbType.VarChar, 13).Value = pLayout_Index
                .Add("@Location_Index", SqlDbType.VarChar, 13).Value = pLocation_Index
                .Add("@Action_Id", SqlDbType.Int, 4).Value = Action_Id
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
            Return True
        Catch ex As Exception
            Return False
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function

    Public Sub Update_Sort(ByVal pLayout_Index As String, ByVal pSortAsc As Boolean)
        Dim strSQL As String = " "
        Try


            strSQL = " UPDATE ms_Location"
            strSQL &= " SET "
            strSQL &= "     SortAsc=@SortAsc "
            strSQL &= "           WHERE          Layout_Index in "
            strSQL &= "(  Select Layout_Index From ms_Location where Lock in (select Lock from ms_Location where Layout_Index = @Layout_Index) )"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Layout_Index", SqlDbType.VarChar, 13).Value = pLayout_Index
                .Add("@SortAsc", SqlDbType.Bit, 1).Value = pSortAsc
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Sub Delete_Location(ByVal pLocation_Alias As String)
        Dim strSQL As String = ""
        Try
            strSQL = "  update top (1)  ms_Location "
            strSQL &= " Set status_id = -1 "
            strSQL &= " ,Update_by ='" & WV_UserName & "'"
            strSQL &= " ,Update_date = getdate() "

            strSQL &= " WHERE Location_Alias='" & pLocation_Alias & "'"

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Function deleteLocation_Index(ByVal Location_Index As String) As Boolean
        Try
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@update_by", SqlDbType.VarChar).Value = WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Add("@Location_Index", SqlDbType.VarChar).Value = Location_Index
            End With
            Return DBExeNonQuery("UPDATE TOP (1) ms_Location SET status_id = -1,update_by = @update_by,update_branch = @update_branch,update_date = GETDATE() WHERE Location_Index = @Location_Index") > 0
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub reCalLocation()
        Dim strSQL As New StringBuilder
        Try
            strSQL.Length = 0
            strSQL.Append(" UPDATE LOC SET Current_Qty = ISNULL(LB.Qty_Bal,0),Current_Weight = ISNULL(LB.Weight_Bal,0),Current_Volume = ISNULL(LB.Volume_Bal,0)")
            strSQL.Append(" FROM ms_Location LOC")
            strSQL.Append(" LEFT OUTER JOIN (")
            strSQL.Append("     SELECT Location_Index")
            strSQL.Append("     ,SUM(Qty_Bal) As Qty_Bal")
            strSQL.Append("     ,SUM((Weight_Bal_Begin / Qty_Bal_Begin) * Qty_Bal) As Weight_Bal")
            strSQL.Append("     ,SUM((Volume_Bal_Begin / Qty_Bal_Begin) * Qty_Bal) As Volume_Bal")
            strSQL.Append("     FROM tb_LocationBalance ")
            strSQL.Append("     WHERE Qty_Bal > 0 ")
            strSQL.Append("     GROUP BY Location_Index")
            strSQL.Append(" ) LB ON LOC.Location_Index = LB.Location_Index")
            strSQL.Append(" WHERE status_id NOT IN (-1)")
            DBExeNonQuery(strSQL.ToString())
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function Delete_Location_Layout(ByVal pLayout_Index As String) As String
        Dim strSQL As String = ""
        Try
            strSQL = "  DELETE ms_Location "
            strSQL &= " WHERE Layout_Index='" & pLayout_Index & "'"

            strSQL &= "  DELETE ms_Layout "
            strSQL &= " WHERE Layout_Index='" & pLayout_Index & "'"

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
            Return pLayout_Index
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function

    '*** Return : ??
    Public Function Delete(ByVal oLocation_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = "  DELETE ms_Location "
            strSQL &= " WHERE Location_Index='" & oLocation_Index & "'"

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception
            Return False
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function

#End Region
#Region " CHECK DATA "

    '*** Create By  : Paponshet ; [11/02/2008] ; ??s
    '*** Return : ??
    Public Function isExistLocation_ALias(ByVal Location_Alias As String) As Boolean

        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_Location where Location_Alias = @Location_Alias  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Alias", SqlDbType.VarChar, 13).Value = Location_Alias

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
    Public Function isExistTag_Number(ByVal Tag_No As String) As Boolean

        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_OrderItemLocation where Tag_No = @Tag_No  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No

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

    Public Function getLocation_Index(ByVal Location_Alias As String) As String

        Dim strSQL As String
        Try
            strSQL = " select Location_Index from ms_Location where Location_Alias = @Location_Alias  and status_Id<>-1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Alias", SqlDbType.VarChar, 50).Value = Location_Alias

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()

            If GetScalarOutput Is Nothing Then
                _scalarOutput = ""
            Else
                _scalarOutput = GetScalarOutput
            End If


            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return ""
            Else
                Return _scalarOutput
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function isOverFlow_Pallet(ByVal Location_Alias As String, ByVal maxPallet As Integer) As Boolean
        Try
            Dim StrSQL As String
            StrSQL = " SELECT 1 FROM ms_Location WHERE Location_Alias = @Location_Alias"
            StrSQL &= " AND ((SELECT TOP 1 COUNT(LB.Tag_No) AS CountPallet FROM ("
            StrSQL &= " SELECT Tag_No FROM tb_LocationBalance WHERE Qty_Bal > 0 "
            StrSQL &= " AND Location_Index=(SELECT TOP 1 Location_Index FROM ms_Location WHERE Location_Alias = @Location_Alias) "
            StrSQL &= " GROUP BY Tag_No"
            StrSQL &= " ) AS LB) + @maxPallet) <= Max_Pallet"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Location_Alias", SqlDbType.VarChar).Value = Location_Alias
                .Add("@maxPallet", SqlDbType.Int).Value = maxPallet
            End With
            Return DBExeQuery(StrSQL).Rows.Count
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function updateSuggestLocationRealTime(ByVal Tag_Index As String, ByVal Location_Alias As String) As Boolean

        Try
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Tag_Index", SqlDbType.VarChar).Value = Tag_Index
                .Add("@Location_Alias", SqlDbType.VarChar).Value = IIf(String.IsNullOrEmpty(Location_Alias), DBNull.Value, Location_Alias)
            End With
            Return DBExeNonQuery(" UPDATE tb_Tag SET tb_Tag.Suggest_Location_Index	= (SELECT TOP 1 Location_Index FROM ms_Location WHERE Location_Alias = @Location_Alias) WHERE Tag_Index=@Tag_Index") > 0
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function isOverFlow_Qty(ByVal Location_Alias As String, ByVal Qty As Double) As Boolean

        Dim strSQL As String
        Try
            strSQL = "  select count(*) from ms_Location  "
            strSQL &= " where Location_Alias = @Location_Alias  "
            strSQL &= "  AND Max_Qty<Current_Qty+" & Qty & " "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Alias", SqlDbType.VarChar, 13).Value = Location_Alias

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


    Public Function isOverFlow_Weight(ByVal Location_Alias As String, ByVal Weight As Double) As Boolean

        Dim strSQL As String
        Try
            strSQL = "  select count(*) from ms_Location  "
            strSQL &= " where Location_Alias = @Location_Alias  "
            strSQL &= "  AND Max_Weight<Current_Weight+" & Weight & " "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Alias", SqlDbType.VarChar, 13).Value = Location_Alias

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

    '02-02-2010 ja Color.Red ULLLocation
    Public Function isUSE_FULLLocation(ByVal pLocation_Id As String) As Boolean
        Try

            Dim strSQL As String
            Try
                strSQL = "  select Isnull(ML.Max_Pallet,0) as Max_Pallet"
                strSQL &= "      ,Bal_Pallet = (SELECT     isnull(COUNT(DISTINCT LB.Tag_No),0)"
                strSQL &= "      		FROM      [tb_LocationBalance ]  LB"
                strSQL &= "      		WHERE     LB.Location_Index = ML.Location_Index)    "
                strSQL &= "      from ms_location  ML"
                strSQL &= "      where ML.Location_Index in (select MLL.Location_Index from ms_Location MLL where MLL.Location_Id ='" & pLocation_Id & "')"


                'strSQL = "   select count(*) as   Max_Pallet "
                'strSQL &= "      from ms_location  "
                'strSQL &= " where isnull(Max_Pallet,0) = "
                'strSQL &= " (SELECT     isnull(COUNT(DISTINCT Tag_No),0) AS Tag_No"
                'strSQL &= "  FROM         [tb_LocationBalance ]"
                'strSQL &= "  WHERE     (Qty_Bal > 0) AND (Location_Index in (select Location_Index from ms_Location where Location_Id ='" & pLocation_Id & "')))"


                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable

                If _dataTable.Rows.Count > 0 Then
                    If CDbl(_dataTable.Rows(0).Item("Max_Pallet")) = 0 Then Return False
                    If CDbl(_dataTable.Rows(0).Item("Bal_Pallet")) >= CDbl(_dataTable.Rows(0).Item("Max_Pallet")) Then Return True
                End If

                Return False

            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function isUSE_FULLLocation_DashBoard(ByVal pLocation_Index As String) As Boolean
        Try

            Dim strSQL As String
            Try
                strSQL = "  select Isnull(ML.Max_Pallet,0) as Max_Pallet"
                strSQL &= "      ,Bal_Pallet = (SELECT     isnull(COUNT(DISTINCT LB.Tag_No),0)"
                strSQL &= "      		FROM      [tb_LocationBalance ]  LB"
                strSQL &= "      		WHERE     LB.Location_Index = ML.Location_Index)    "
                strSQL &= "      from ms_location  ML"
                strSQL &= "      where ML.Location_Index in (select MLL.Location_Index from ms_Location MLL where MLL.Location_Index ='" & pLocation_Index & "')"


                'strSQL = "   select count(*) as   Max_Pallet "
                'strSQL &= "      from ms_location  "
                'strSQL &= " where isnull(Max_Pallet,0) = "
                'strSQL &= " (SELECT     isnull(COUNT(DISTINCT Tag_No),0) AS Tag_No"
                'strSQL &= "  FROM         [tb_LocationBalance ]"
                'strSQL &= "  WHERE     (Qty_Bal > 0) AND (Location_Index in (select Location_Index from ms_Location where Location_Id ='" & pLocation_Id & "')))"


                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable

                If _dataTable.Rows.Count > 0 Then
                    If CDbl(_dataTable.Rows(0).Item("Max_Pallet")) = 0 Then
                        Return False
                    End If
                    If CDbl(_dataTable.Rows(0).Item("Bal_Pallet")) >= CDbl(_dataTable.Rows(0).Item("Max_Pallet")) Then Return True
                End If

                Return False

            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function isUSE_FULLLocation_DashBoard(ByVal pLocation_Index As String, ByVal pLayout_Index As String) As Boolean
        Try

            Dim strSQL As String
            Try
                strSQL = "  select Isnull(ML.Max_Pallet,0) as Max_Pallet"
                strSQL &= "      ,Bal_Pallet = (SELECT     isnull(COUNT(DISTINCT LB.Tag_No),0)"
                strSQL &= "      		FROM      [tb_LocationBalance ]  LB"
                strSQL &= "      		WHERE     LB.Location_Index = ML.Location_Index)    "
                strSQL &= "      from ms_location  ML"
                strSQL &= "      where ML.Location_Index in (select MLL.Location_Index from ms_Location MLL where MLL.Location_Index ='" & pLocation_Index & "')"


                'strSQL = "   select count(*) as   Max_Pallet "
                'strSQL &= "      from ms_location  "
                'strSQL &= " where isnull(Max_Pallet,0) = "
                'strSQL &= " (SELECT     isnull(COUNT(DISTINCT Tag_No),0) AS Tag_No"
                'strSQL &= "  FROM         [tb_LocationBalance ]"
                'strSQL &= "  WHERE     (Qty_Bal > 0) AND (Location_Index in (select Location_Index from ms_Location where Location_Id ='" & pLocation_Id & "')))"


                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable

                If _dataTable.Rows.Count > 0 Then
                    If CDbl(_dataTable.Rows(0).Item("Max_Pallet")) = 0 Then
                        Return isUSE_FULL(pLayout_Index)
                    End If
                    If CDbl(_dataTable.Rows(0).Item("Bal_Pallet")) >= CDbl(_dataTable.Rows(0).Item("Max_Pallet")) Then Return True
                End If

                Return False

            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getSortAsc_Lock(ByVal pLock As String, ByVal pRoomIndex As String, ByVal pHouseIndex As String) As Boolean

        Dim strSQL As String
        Try
            strSQL = " select top 1 SortAsc from ms_Location where (Lock = @Lock) and (status_id <> -1) "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Lock", SqlDbType.VarChar, 50).Value = pLock

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            Select Case _scalarOutput
                Case Nothing
                    Return True
                Case Else
                    Return CBool(_scalarOutput)

            End Select

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
#End Region


#Region "Dong Update DATALAYER FOR PUTAWAY"

    Public Sub SearchData_OrderItem(ByVal OrderItem_Index As String, ByVal Zone As String)

        Dim strSQL As New StringBuilder

        Try

            strSQL.Append(" SELECT      " & Zone & ",Total_Qty")
            strSQL.Append(" FROM        VIEW_ZoneOrderItem ")
            strSQL.Append(" WHERE       OrderItem_Index ='" & OrderItem_Index & "'")
            'strSQL.Append(" and ")
            SetSQLString = strSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchData_Config(ByVal Where As String)
        '  
        Dim strSQL As New StringBuilder
        Dim strWhere As String = ""
        Try
            strSQL.Append(" select      *    ")
            strSQL.Append(" from        config_ZoneSystem   ")
            strSQL.Append(" Where       status = 1  ")
            strSQL.Append(" order by    seq ")

            SetSQLString = strSQL.ToString & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Function SearchZone_Index(ByVal Zone_Index As String, ByVal Table_Name As String, ByVal strZone_Condition As String, ByVal strIndexIn_Zone As String, ByVal dblQty As Double, ByVal KeepLocation_Alias As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim tmpQty As Double = 0
        If KeepLocation_Alias = "" Then KeepLocation_Alias = "''"


        Try

            strSQL.Append(" SELECT      *,ms_Location.Current_Qty + (ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = ms_Location.Location_Index GROUP BY Location_Index),0) + " & dblQty & ") as PutAway_Qty ")
            strSQL.Append(" FROM        ms_Zone INNER JOIN ")
            strSQL.Append("             " & Table_Name & " ON ms_Zone.Zone_Index = " & Table_Name & ".Zone_Index  INNER JOIN ")
            strSQL.Append("             ms_Location ON ms_Zone.Zone_Index = ms_Location.Zone_Index ")
            strSQL.Append(" WHERE       ms_Zone.Zone_Index='" & Zone_Index & "'")
            strSQL.Append("             and " & Table_Name & "." & strIndexIn_Zone)
            strSQL.Append(" 	 AND Location_Alias not in (" & KeepLocation_Alias & ")")
            strSQL.Append("             and ms_Location.Max_Qty > = ms_Location.Current_Qty + (ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = ms_Location.Location_Index GROUP BY Location_Index),0) +  " & dblQty & ")")
            SetSQLString = strSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            tmpQty = _dataTable.Rows.Count
            If tmpQty <= 0 Then
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

    Public Sub SearchZone_Location(ByVal strZone_Condition_Index As String, ByVal Where_Zone As String, ByVal Qty As Double, ByVal intPower As Integer, ByVal KeepLocation_Alias As String)
        Dim strSQL As New StringBuilder
        If KeepLocation_Alias = "" Then KeepLocation_Alias = "''"

        Try
            Dim Value As Integer = (10 ^ intPower) * 1

            strSQL.Append(" SELECT Zone_index,Value = CASE Customer_Index WHEN '-1' THEN 0 ELSE " & Value & " END ")
            strSQL.Append(" FROM ( SELECT *,(ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = VIEW_ZoneLocation.Location_Index GROUP BY Location_Index),0) + " & Qty & ") as PutAway_Qty ")
            strSQL.Append("             FROM VIEW_ZoneLocation ")
            strSQL.Append(" 	WHERE          Action_Id = 1 and " & Where_Zone)
            strSQL.Append(" 	 AND Location_Alias not in (" & KeepLocation_Alias & ")")
            strSQL.Append("      AND Balance_Qty >=  (ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = VIEW_ZoneLocation.Location_Index GROUP BY Location_Index),0) + " & Qty & "))  BALANCE ")
            strSQL.Append(" group by Zone_index,CASE Balance.Customer_Index WHEN '-1' THEN 0 ELSE  " & Value & " END ")

            SetSQLString = strSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub SearchZone_Value(ByVal TableName As String, ByVal strZone_Condition_Index As String, ByVal strWhereZone_Condition_Index As String, ByVal intPower As Integer)
        Dim strSQL As New StringBuilder

        Try

            Dim Value As Integer = (10 ^ intPower) * 1

            strSQL.Append(" select          Zone_Index ")
            strSQL.Append("                 ,Value =    CASE " & strZone_Condition_Index)
            strSQL.Append("                             WHEN '-1' THEN 0 ")
            strSQL.Append("                             ELSE " & Value)
            strSQL.Append("                End ")
            strSQL.Append(" From            " & TableName)
            strSQL.Append(" WHERE           " & strWhereZone_Condition_Index)

            'strSQL.Append(" and Balance_Qty >=" & Qty)

            SetSQLString = strSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Function SearchMaxQty_LocationAlias(ByVal Location_Alias As String) As Double
        Dim strSQL As New StringBuilder

        Try

            strSQL.Append("     SELECT   Max_Qty")
            strSQL.Append("     FROM ms_Location ")
            strSQL.Append("     WHERE       Location_Alias = '" & Location_Alias & "'")

            SetSQLString = strSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then
                Return 0
            Else
                Return CDbl(_dataTable.Rows(0).Item("Max_Qty").ToString)
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    'Updateby Dong_kk

    Public Sub SelectScore_Zone(ByVal Zone_Indexs As String, ByVal Qty As Double, ByVal KeepLocation_Alias As String, ByVal USE_AUTOPUTAWAY_EMTRY_LOCATION As Boolean)
        Dim sbSql As New StringBuilder
        If KeepLocation_Alias = "" Then KeepLocation_Alias = "''"
        Try

            sbSql.Append(" SELECT      Zone_Index,Location_Index,Location_Alias, convert(int,REPLACE(Score,',','')) as score")
            sbSql.Append(" FROM        ms_Location  ")
            sbSql.Append(" WHERE       ms_Location.Score !='' ")
            If Zone_Indexs <> "" Then
                sbSql.Append("  AND        Zone_Index in " & Zone_Indexs)
            End If
            sbSql.Append(" 	 AND Location_Alias not in (" & KeepLocation_Alias & ")")
            If USE_AUTOPUTAWAY_EMTRY_LOCATION Then
                sbSql.Append("             and ms_Location.Max_Qty > = (ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = ms_Location.Location_Index GROUP BY Location_Index),0) +  " & Qty & ")")
                sbSql.Append("             and ms_Location.Current_Qty = 0")
            Else
                sbSql.Append("             and ms_Location.Max_Qty > = ms_Location.Current_Qty + (ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = ms_Location.Location_Index GROUP BY Location_Index),0) +  " & Qty & ")")
            End If


            sbSql.Append(" ORDER BY convert(int,REPLACE(Score,',','')) DESC ")


            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub


    Public Function CheckScore_Zone(ByVal Zone_Indexs As String, ByVal Qty As Double) As Boolean
        Dim sbSql As New StringBuilder
        Try

            sbSql.Append(" SELECT      Count(*)")
            sbSql.Append(" FROM        ms_Location  ")
            sbSql.Append(" WHERE       ms_Location.Score !='' ")
            If Zone_Indexs <> "" Then
                sbSql.Append(" AND        Zone_Index in " & Zone_Indexs)
            End If
            sbSql.Append("             and ms_Location.Max_Qty > = ms_Location.Current_Qty + (ISNULL((Select Sum(qty) FROM tb_orderItemLocation WHERE Location_Index = ms_Location.Location_Index GROUP BY Location_Index),0) +  " & Qty & ")")



            SetSQLString = sbSql.ToString
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case 0
                    Return False
                Case Else
                    Return True
            End Select

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function SearchLocationAlias_ZoneNoScore(ByVal Zone_Index As String, ByVal Qty As Double, ByVal KeepLocation_Alias As String, ByVal USE_AUTOPUTAWAY_EMTRY_LOCATION As Boolean) As String
        Dim sbSql As New StringBuilder
        If KeepLocation_Alias = "" Then KeepLocation_Alias = "''"
        Try

            sbSql.Append(" select Location_Alias,Bal_Qty FROM (")
            sbSql.Append(" SELECT      Location_Alias,ms_Location.Current_Qty + (ISNULL((Select Sum(Qty_Bal) FROM tb_Locationbalance WHERE Location_Index = ms_Location.Location_Index AND Qty_Bal > 0 GROUP BY Location_Index),0) +  " & Qty & ") as Bal_Qty")
            sbSql.Append(" FROM        ms_Location  WHERE 1=1 ")
            If Zone_Index <> "" Then
                sbSql.Append(" AND       Zone_Index in " & Zone_Index)
            End If

            sbSql.Append(" 	 AND Location_Alias not in (" & KeepLocation_Alias & ")")
            If USE_AUTOPUTAWAY_EMTRY_LOCATION Then
                sbSql.Append("             and ms_Location.Max_Qty > = (ISNULL((Select Sum(Qty_Bal) FROM tb_Locationbalance WHERE Location_Index = ms_Location.Location_Index AND Qty_Bal > 0 GROUP BY Location_Index),0) +  " & Qty & ")")
                sbSql.Append("             and ms_Location.Current_Qty = 0")
            Else
                sbSql.Append("             and ms_Location.Max_Qty >= (ISNULL((Select Sum(Qty_Bal) FROM tb_Locationbalance WHERE Location_Index = ms_Location.Location_Index AND Qty_Bal > 0 GROUP BY Location_Index),0) +  " & Qty & ")")
            End If

            sbSql.Append(" ) Score ")
            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then
                Return ""
            Else
                Return _dataTable.Rows(0).Item("Location_Alias").ToString
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    'Updateby Dong_kk
    Public Sub SelectLock(ByVal fldWareHouse As String, ByVal fldRoom As String)
        Dim sbSql As New StringBuilder
        Try
            sbSql.Append(" SELECT     Distinct Lock,Warehouse_Index ")
            sbSql.Append(" FROM       ms_Location ")
            sbSql.Append(" WHERE     (Room = '" & fldRoom & "') AND (Warehouse_Index = '" & fldWareHouse & "') and status_id != -1")
            sbSql.Append(" Order BY Lock")


            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    'Updateby Dong_kk
    Public Sub SelectRow(ByVal fldWareHouse As String, ByVal fldRoom As String, ByVal fldLock As String)
        Dim sbSql As New StringBuilder
        Try

            sbSql.Append(" Select Distinct Lock+Row as Row ")
            sbSql.Append(" FROM ms_LOCATION ")
            sbSql.Append(" WHERE Warehouse_Index = '" & fldWareHouse & "'")
            sbSql.Append(" AND ROOM = '" & fldRoom & "'")
            sbSql.Append(" AND LOCK = '" & fldLock & "'")

            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    'Updateby Dong_kk
    Public Sub SelectDepth_Level(ByVal fldWareHouse As String, ByVal fldRoom As String, ByVal fldLockRow As String)
        Dim sbSql As New StringBuilder
        Try

            sbSql.Append(" Select max(Depth) as NumDepth,Max(Level) as NumLevel ")
            sbSql.Append(" FROM ms_LOCATION ")
            sbSql.Append(" WHERE Warehouse_Index = '" & fldWareHouse & "'")
            sbSql.Append(" AND ROOM = '" & fldRoom & "'")
            sbSql.Append(" AND Lock+Row='" & fldLockRow & "'")

            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    'Updateby Dong_kk
    Public Sub SelectViewScore(ByVal fldWareHouse As String, ByVal fldRoom As String, ByVal fldLockRow As String, ByVal fldDepth As String, ByVal fldLevel As String)
        Dim sbSql As New StringBuilder
        Try

            sbSql.Append(" Select Location_Index,Score ")
            sbSql.Append(" FROM ms_LOCATION ")
            sbSql.Append(" WHERE Warehouse_Index = '" & fldWareHouse & "'")
            sbSql.Append(" AND ROOM = '" & fldRoom & "'")
            sbSql.Append(" AND Lock+Row='" & fldLockRow & "'")
            sbSql.Append(" AND Depth = '" & fldDepth & "'")
            sbSql.Append(" AND Level = '" & fldLevel & "'")

            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    'Updateby Dong_kk
    Public Sub SelectScore()
        Dim sbSql As New StringBuilder
        Try

            sbSql.Append(" SELECT      Location_Index,Score,Location_Alias")
            sbSql.Append(" FROM        ms_Location  ")
            sbSql.Append(" WHERE       ms_Location.Score !='' ")
            SetSQLString = sbSql.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    Public Sub getProductSelection(ByVal OrderItem_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "SELECT *,'' as Location_Alias"
            strSQL &= "  FROM temp_Allocate_Qty "
            strWhere = " WHERE OrderItem_Index ='" & OrderItem_Index & "' "

            strSQL = strSQL + strWhere

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

    Public Sub getProductSelection_Order(ByVal OrderItem_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "SELECT *,'' as Location_Alias"
            strSQL &= "  FROM tb_OrderItem "
            strWhere = " WHERE OrderItem_Index ='" & OrderItem_Index & "' "

            strSQL = strSQL + strWhere

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

    Public Function ChkQty_Alias(ByVal Location_Alias As String, ByVal Total_Qty As Double) As Boolean
        Dim strSQL As New StringBuilder
        Dim tmpQty As Double
        Try

            strSQL.Append(" SELECT      count(*) ")
            strSQL.Append(" FROM        ms_Location   ")
            strSQL.Append(" WHERE       Location_Alias = '" & Location_Alias & "'")
            strSQL.Append("             and ms_Location.Max_Qty > = ms_Location.Current_Qty + (ISNULL((Select Sum(Total_Qty) FROM tb_orderItemLocation WHERE Location_Index = ms_Location.Location_Index  and  status not in (-1,2) GROUP BY Location_Index),0) +  " & Total_Qty & ")")


            SetSQLString = strSQL.ToString
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            tmpQty = CDbl(_scalarOutput)
            If tmpQty <= 0 Then
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



    Public Function Chk_Alias(ByVal Location_Alias As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim tmpQty As Double
        Try

            strSQL.Append(" SELECT      count(*)")
            strSQL.Append(" FROM        ms_Location   ")
            strSQL.Append(" WHERE       Location_Alias = '" & Location_Alias & "'")

            SetSQLString = strSQL.ToString
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            tmpQty = CDbl(_scalarOutput)
            If tmpQty <= 0 Then
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

    Public Function ChkBlock_Location(ByVal pLayout_Index As String) As Boolean
        Dim strSQL As New StringBuilder
        Dim tmpQty As Double
        Try
            strSQL.Append(" SELECT      count(*)")
            strSQL.Append(" FROM        ms_Location   ")
            strSQL.Append(" WHERE       Layout_Index = '" & pLayout_Index & "'")
            strSQL.Append(" AND       Action_Id =  3 ")

            SetSQLString = strSQL.ToString
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            tmpQty = CDbl(_scalarOutput)
            If tmpQty <= 0 Then
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

#Region "   CHECK LAYOUT TOPVIEW   "

    Public Function chkLayout_Status(ByVal pLayout_Index As String, ByVal intAction_Id As Integer) As Double
        Try

            Dim strSQL As String
            Try
                strSQL = "  select count(*) from ms_Location  "
                strSQL &= " where Layout_Index = @Layout_Index  "
                Select Case intAction_Id
                    Case 3
                        strSQL &= " AND Action_Id = 3  "
                    Case 2
                        strSQL &= " AND Action_Id = 2  "
                    Case 1
                        strSQL &= " AND Action_Id = 1  "
                    Case 0
                        strSQL &= " AND Action_Id = 0  "
                End Select

                SQLServerCommand.Parameters.Clear()
                SQLServerCommand.Parameters.Add("@Layout_Index", SqlDbType.VarChar, 13).Value = pLayout_Index

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
                connectDB()
                EXEC_Command()
                _scalarOutput = GetScalarOutput
                If _scalarOutput Is Nothing Then Return 1

                If _scalarOutput.Trim = "" Then Return 1

                Return CInt(_scalarOutput)



            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function numLayout_Used(ByVal pLayout_Index As String) As Double
        Try

            Dim strSQL As String
            Try
                strSQL = "  select count(Location_Index) from ms_Location  "
                strSQL &= " where Layout_Index = @Layout_Index  "
                strSQL &= " And Current_Qty > 0  "

                SQLServerCommand.Parameters.Clear()
                SQLServerCommand.Parameters.Add("@Layout_Index", SqlDbType.VarChar, 13).Value = pLayout_Index

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
                connectDB()
                EXEC_Command()


                _scalarOutput = GetScalarOutput
                If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                    Return 0
                Else
                    Return Val(_scalarOutput)
                End If
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function isUSE_FULL(ByVal pLayout_Index As String) As Boolean
        Try

            Dim strSQL As String
            Try
                strSQL = "  select  Sum(Current_Qty), Sum(Max_Qty)  from ms_Location  "
                strSQL &= " GROUP BY Layout_Index "
                strSQL &= " having Layout_Index = '" & pLayout_Index & "'  "
                strSQL &= " And sum(Current_Qty) >=  Sum(Max_Qty) "


                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
                If _dataTable Is Nothing Then Return False

                If _dataTable.Rows.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '02-02-2010 ja Color.Red ULLLocation
    Public Function isUSE_FULLLocation_Id(ByVal pLocation_Id As String) As Boolean
        Try

            Dim strSQL As String
            Try
                strSQL = "  select  Sum(Current_Qty), Sum(Max_Qty)  from ms_Location  "
                strSQL &= " GROUP BY Location_Id "
                strSQL &= " having Location_Id = '" & pLocation_Id & "'  "
                strSQL &= " And sum(Current_Qty) >=  Sum(Max_Qty) "


                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
                If _dataTable Is Nothing Then Return False

                If _dataTable.Rows.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function isUSE_EMPTY(ByVal pLayout_Index As String) As Boolean
        Try

            Dim strSQL As String
            Try
                strSQL = "  select  Sum(Current_Qty)  from ms_Location  "
                strSQL &= " GROUP BY Layout_Index "
                strSQL &= " having Layout_Index = '" & pLayout_Index & "'  "
                strSQL &= " And sum(Current_Qty) =  0 "


                SetSQLString = strSQL
                connectDB()
                EXEC_DataAdapter()
                _dataTable = GetDataTable
                If _dataTable Is Nothing Then Return False

                If _dataTable.Rows.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Catch ex As Exception
                Throw ex
            Finally
                disconnectDB()
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region


End Class