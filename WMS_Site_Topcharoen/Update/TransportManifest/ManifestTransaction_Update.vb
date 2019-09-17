'Dong_kk
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data
Imports System.Data.SqlClient
Imports WMS_STD_OUTB_Transport_Datalayer
Public Class ManifestTransaction_Update : Inherits DBType_SQLServer

#Region " Private variables "
    'Public _DeleteType As Integer = 1
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _TransportManifestItem_Index As String = ""
    Private _TransportManifest_Index As String = ""
    Private _objHeader As New tb_TransportManifest_Update
    Private _objItemCollection As List(Of tb_TransportManifestItem)
    Private _objItem As tb_TransportManifestItem
    Private _objItemTransportManifestCharge As List(Of svar_TransportManifestCharge)
#End Region

#Region " Properties "


    Private _TransportManifest_No As String
    Public Property TransportManifest_No() As String
        Get
            Return _TransportManifest_No
        End Get
        Set(ByVal value As String)
            _TransportManifest_No = value
        End Set
    End Property

    Public Property TransportManifest_Index() As String
        Get
            Return _TransportManifest_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifest_Index = Value
        End Set
    End Property
    Public Property TransportManifestItem_Index() As String
        Get
            Return _TransportManifestItem_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifestItem_Index = Value
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
#End Region

#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        COPY
        SEARCH
        NULL
    End Enum

    Private objDelete As enuOperation_Type

    Public Enum enuDelete
        DELETEALL
        DELETEITEM
    End Enum
#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "
    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        objStatus = Operation_Type
    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objHeader As tb_TransportManifest_Update, ByVal objItemCollection As List(Of tb_TransportManifestItem), ByVal objItemTransportManifestCharge As List(Of svar_TransportManifestCharge))
        MyBase.New()
        objStatus = Operation_Type
        _objHeader = objHeader
        _objItemCollection = objItemCollection
        _objItemTransportManifestCharge = objItemTransportManifestCharge
    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal TransportManifest_Index As String, ByVal TransportManifestItem_Index As String)
        MyBase.New()
        objStatus = Operation_Type
        _TransportManifest_Index = TransportManifest_Index
        _TransportManifestItem_Index = TransportManifestItem_Index
    End Sub

#End Region

#Region "   SELECT  "



    Public Sub getSO_Totruck(ByVal WhereString As String, ByVal isSubManifest As Integer, ByVal Process_Id As String, ByVal selectItem As Boolean, ByVal pintTop As Integer)
        Dim strSQL As String = ""
        Dim oReader As Data.SqlClient.SqlDataReader
        oReader = Nothing
        Try
            Select Case selectItem
                Case False
                    strSQL = "select  top " & pintTop & "  * "
                    strSQL &= " , chkSelect = 0 ,Count=convert(int,0) from VIEW_SO_TruckLoad "
                    '  strSQL &= " , chkSelect=convert(bit,0),Count=convert(int,0) from VIEW_SO_TruckLoad "
                Case True
                    strSQL = "select  top " & pintTop & "  * "
                    ' strSQL &= ", chkSelect= 0 ,convert(bit,0)(int,0) from VIEW_SO_TruckLoad "
                    strSQL &= ", chkSelect= 1,Count=convert(int,0) from VIEW_SO_TruckLoad "
            End Select

            strSQL &= " WHERE  1=1  "
            If Process_Id <> "" Then
                strSQL &= " and isnull(Process_Id,1) = " & Process_Id
            End If


            Select Case isSubManifest
                Case 0
                    strSQL &= "  and   Status_Manifest IN"
                    strSQL &= "       (SELECT     Status"
                    strSQL &= "  FROM  dbo.config_TransportDespatchStatus "
                    strSQL &= "             WHERE      Despatch_Status = 1) "
                    strSQL &= " AND  Status not in (-1,1) "


                    'best up date 17-09-2012
                    'strSQL &= "  and   Status_Manifest IN"
                    'strSQL &= "       ('1','7') "
                    'strSQL &= " AND  Status not in (-1,1) "
                    'end best up date
                Case 1
                    strSQL &= "    and  Status_Manifest IN"
                    strSQL &= "          (SELECT     Status"
                    strSQL &= "      FROM dbo.config_TransportDespatchStatus "
                    strSQL &= "          WHERE      DespatchDc_Status = 1)"
                    strSQL &= " AND  Status not in (-1,1) "

                    'best up date 17-09-2012
                    'strSQL &= "  and   Status_Manifest IN"
                    'strSQL &= "       ('1','7') "
                    'strSQL &= " AND  Status not in (-1,1) "
                    'end best up date 17-09-2012

            End Select
            SetSQLString = strSQL & WhereString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            'EXEC_Command()
            'oReader = Me.GetDataReader
            '_dataTable.Load(oReader)
            '_dataTable.Columns("Count").ReadOnly = False
            '_dataTable.Columns("chkselect").ReadOnly = False
            'oReader.Close()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub chkSalesOrder_Notroute_Inmaster(ByVal SalesOrder_Index As String, ByVal Customer_Shipping_Location_Index As String)
        Try
            Dim strSQL As String = ""
            strSQL = " SELECT CUSSHIP.Customer_Shipping_Location_Id AS Customer_Shipping_Location_Id,CUSSHIP.Shipping_Location_Name,CUSSHIP.Customer_Shipping_Location_Index"
            strSQL &= " ,CUSSHIP.Route_Index,CUSSHIP.SubRoute_Index,CUSSHIP.TransportRegion_Index"
            strSQL &= " ,SO.SalesOrder_Index"
            strSQL &= " ,SO.Route_Index AS SO_ROUTE,SO.SubRoute_Index AS SO_SubRoute,SO.TransportRegion_Index AS  SO_TransportRegion "

            strSQL &= "  FROM ms_Customer_Shipping_Location CUSSHIP left join tb_SalesOrder SO "
            strSQL &= "             ON CUSSHIP.Customer_Shipping_Location_Index = SO.Customer_Shipping_Location_Index "
            strSQL &= "    WHERE(1 = 1)"
            strSQL &= " and SO.SalesOrder_Index = '" + SalesOrder_Index + "' "
            strSQL &= " and SO.Customer_Shipping_Location_Index  = '" + Customer_Shipping_Location_Index + "'"

            strSQL &= " and (isnull(CUSSHIP.Route_Index,'') =''"
            strSQL &= "         or isnull(CUSSHIP.SubRoute_Index,'') = ''"
            strSQL &= "         or isnull(CUSSHIP.TransportRegion_Index,'') = '')"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            'If _dataTable.Rows.Count > 0 Then

            'Else
            '    strSQL = " SELECT CUSSHIP.Customer_Shipping_Location_Id AS Customer_Shipping_Location_Id,CUSSHIP.Shipping_Location_Name,CUSSHIP.Customer_Shipping_Location_Index,"
            '    strSQL &= " CUSSHIP.Route_Index,CUSSHIP.SubRoute_Index,"
            '    strSQL &= " SO.SalesOrder_Index,"
            '    strSQL &= " SO.Route_Index AS SO_ROUTE,SO.SubRoute_Index AS SO_SubRoute,SO.TransportRegion_Index AS  SO_TransportRegion "
            '    strSQL &= "  FROM ms_Customer_Shipping_Location CUSSHIP left join tb_SalesOrder SO "
            '    strSQL &= "             ON CUSSHIP.Customer_Shipping_Location_Index = SO.Customer_Shipping_Location_Index "
            '    strSQL &= "    WHERE(1 = 1)"
            '    strSQL &= " and SO.SalesOrder_Index = '" + SalesOrder_Index + "' "
            '    strSQL &= " and SO.Customer_Shipping_Location_Index  = '" + Customer_Shipping_Location_Index + "'"
            '    strSQL &= " and CUSSHIP.Route_Index in ('0010000000000','')"
            '    strSQL &= " and CUSSHIP.SubRoute_Index is null"
            '    strSQL &= " and CUSSHIP.TransportRegion_Index is null"
            '    SetSQLString = strSQL
            '    connectDB()
            '    EXEC_DataAdapter()
            '    _dataTable = GetDataTable
            'End If

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
    Public Sub getManifestLoading_DocumentItem(ByVal Process_Id As Integer, ByVal Where As String)
        '  
        Dim strSQL As String = ""

        Try
            Select Case Process_Id
                Case 10, 25
                    strSQL = "  SELECT chkSelect = convert(bit,0) , * "
                    strSQL &= " FROM VIEW_MANIFESTLOAD_SO "
                    strSQL &= " WHERE 1=1 "
                    strSQL = strSQL & Where
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
    End Sub

    Public Sub getms_JobSolution(ByVal strwhere As String)
        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT  * "
            strSQL &= " FROM     ms_JobSolution "
            strSQL &= " WHERE 1=1 "
            SetSQLString = strSQL & strwhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getDocumentType_Loading(ByVal InProcess_id As String, ByVal strWhere As String)

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT  * "
            strSQL &= " FROM     ms_DocumentType "
            strSQL &= " WHERE 1=1 "
            If InProcess_id <> "" Then
                strSQL &= " AND Process_Id in (" & InProcess_id & ") and status_id not in ( -1 ) "
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
    Public Sub getCustomer_ShippingDrop_Region(ByVal pstrTransportManifest_Index As String)

        Dim strSQL As String = ""
        Try
            strSQL &= " SELECT	MCS.Customer_Shipping_Index,MCS.Str1 as Customer_Shipping_Id,MCS.Company_Name as Customer_Shipping_Name"
            strSQL &= " 		,TR.TransportRegion_Index,TR.Description as TransportRegion_desc, convert(Varchar(500),'') as Description"
            strSQL &= " 		,sum(TMI.Qty_Shipped) as Total_Qty"
            strSQL &= " 		,sum(TMI.Weight_Shipped) as Weight"
            strSQL &= " 		,sum(TMI.Volume_Shipped) as Volume"
            strSQL &= " 		,MSC.Customer_Index,MSC.Customer_Name,MSC.Customer_Id"
            strSQL &= "         ,ms_DocumentType.IsSend_OR_Recieve as  IsSendOrPickup"
            strSQL &= "         ,convert(float,0) as Rate,convert(float,0) as OverflowPerDrop,convert(float,0) as OverflowRate,convert(float,0) as RateTransportPerDrop,'' as Description"
            strSQL &= " FROM	tb_TransportManifestItem TMI inner join"
            strSQL &= " 		tb_SalesOrder SO ON  TMI.SalesOrder_Index = SO.SalesOrder_Index inner join"
            strSQL &= " 		ms_Customer MSC ON  MSC.Customer_Index = SO.Customer_Index inner join"
            strSQL &= " 		ms_Customer_Shipping MCS ON TMI.Customer_Shipping_Index = MCS.Customer_Shipping_Index left outer join"
            strSQL &= " 		ms_TransportRegion TR ON  TR.TransportRegion_Index = SO.TransportRegion_Index left outer join"
            strSQL &= " 		ms_DocumentType ON  ms_DocumentType.DocumentType_Index = SO.DocumentType_Index"
            strSQL &= " WHERE	TMI.TransportManifest_Index = '" & pstrTransportManifest_Index & "'"
            strSQL &= "         AND	TMI.IsTransportCharged=1  AND TMI.Status <>-1 "
            strSQL &= " GROUP BY MCS.Customer_Shipping_Index,MCS.Str1,MCS.Company_Name,TR.Description,TR.TransportRegion_Index"
            strSQL &= "         ,MSC.Customer_Index,MSC.Customer_Name,MSC.Customer_Id"
            strSQL &= "         ,TMI.IsSendOrPickup"
            strSQL &= " ORDER BY TR.TransportRegion_Index,MCS.Customer_Shipping_Index"

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

    Public Sub getCustomer_Carrier_Region(ByVal pstrTransportManifest_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL &= " SELECT	CRR.Carrier_Index,CRR.Carrier_Id as Carrier_Id,CRR.Description as Carrier_Name"
            strSQL &= " 		,TR.TransportRegion_Index,TR.Description as TransportRegion_desc, convert(Varchar(500),'') as Description"
            strSQL &= " 		,sum(TMI.Qty_Shipped) as Total_Qty"
            strSQL &= " 		,sum(TMI.Weight_Shipped) as Weight"
            strSQL &= " 		,sum(TMI.Volume_Shipped) as Volume"
            strSQL &= " 		,MSC.Customer_Index,MSC.Customer_Name,MSC.Customer_Id"
            strSQL &= "         ,MCS.Customer_Shipping_Index,MCS.Str1 as Customer_Shipping_Id,MCS.Company_Name as Customer_Shipping_Name"
            strSQL &= "         ,ms_DocumentType.IsSend_OR_Recieve as  IsSendOrPickup"
            strSQL &= "         ,convert(float,0) as Rate,convert(float,0) as OverflowPerDrop,convert(float,0) as OverflowRate,convert(float,0) as RateTransportPerDrop,'' as Description"
            strSQL &= " FROM	tb_TransportManifestItem TMI inner join"
            strSQL &= " 		tb_TransportManifest TM ON  TMI.TransportManifest_Index = TM.TransportManifest_Index inner join"
            strSQL &= " 		tb_SalesOrder SO ON  TMI.SalesOrder_Index = SO.SalesOrder_Index inner join"
            strSQL &= " 		ms_Customer MSC ON  MSC.Customer_Index = SO.Customer_Index inner join"
            strSQL &= " 		ms_Carrier CRR ON TM.Carrier_Index = CRR.Carrier_Index left outer join"
            strSQL &= " 		ms_Customer_Shipping MCS ON TMI.Customer_Shipping_Index = MCS.Customer_Shipping_Index left outer join"
            strSQL &= " 		ms_TransportRegion TR ON  TR.TransportRegion_Index = SO.TransportRegion_Index  left outer join"
            strSQL &= " 		ms_DocumentType ON  ms_DocumentType.DocumentType_Index = SO.DocumentType_Index"
            strSQL &= " WHERE	TMI.TransportManifest_Index = '" & pstrTransportManifest_Index & "'"
            strSQL &= "         AND	TMI.IsTransportPaid=1 AND TMI.Status <>-1 "
            strSQL &= " GROUP BY CRR.Carrier_Index,CRR.Carrier_Id,CRR.Description,TR.Description,TR.TransportRegion_Index"
            strSQL &= "         ,MSC.Customer_Index,MSC.Customer_Name,MSC.Customer_Id"
            strSQL &= "         ,MCS.Customer_Shipping_Index,MCS.Str1,MCS.Company_Name"
            strSQL &= "         ,TMI.IsSendOrPickup"
            strSQL &= " ORDER BY TR.TransportRegion_Index,CRR.Carrier_Index"


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


    Public Sub getCustomer_ShippingDrop_Region_SalesOrder(ByVal pstrSalesOrder_Indexs As String)

        Dim strSQL As String = ""
        Try
            'strSQL &= " SELECT     MCS.Customer_Shipping_Location_Index, MCS.Str1 AS Customer_Shipping_Location_Id, MCS.Shipping_Location_Name AS Customer_Shipping_Location_Name, TR.TransportRegion_Index, "
            strSQL &= " SELECT     MCS.Customer_Shipping_Location_Index,  Customer_Shipping_Location_Id, MCS.Shipping_Location_Name AS Customer_Shipping_Location_Name, TR.TransportRegion_Index, "
            strSQL &= "                      TR.Description AS TransportRegion_desc, CONVERT(Varchar(500), '') AS Description, MSC.Customer_Index, MSC.Customer_Name, MSC.Customer_Id, "
            strSQL &= "                      dbo.ms_DocumentType.IsSend_OR_Recieve AS IsSendOrPickup, CONVERT(float, 0) AS Rate, CONVERT(float, 0) AS OverflowPerDrop, CONVERT(float, 0) "
            strSQL &= "                      AS OverflowRate, CONVERT(float, 0) AS RateTransportPerDrop, SUM(dbo.tb_SalesOrderItem.Total_Qty) AS Total_Qty, SUM(dbo.tb_SalesOrderItem.Weight) AS Weight, "
            strSQL &= "                                SUM(dbo.tb_SalesOrderItem.Volume) AS Volume"
            strSQL &= " FROM         dbo.tb_SalesOrder AS SO INNER JOIN"
            strSQL &= "                      dbo.ms_Customer AS MSC ON MSC.Customer_Index = SO.Customer_Index INNER JOIN"
            strSQL &= "                      dbo.tb_SalesOrderItem ON SO.SalesOrder_Index = dbo.tb_SalesOrderItem.SalesOrder_Index LEFT OUTER JOIN"
            strSQL &= "                      dbo.ms_Customer_Shipping_Location AS MCS ON SO.Customer_Shipping_Location_Index = MCS.Customer_Shipping_Location_Index LEFT OUTER JOIN"
            strSQL &= "                      dbo.ms_TransportRegion AS TR ON MCS.TransportRegion_Index = TR.TransportRegion_Index LEFT OUTER JOIN"
            strSQL &= "                      dbo.ms_DocumentType ON dbo.ms_DocumentType.DocumentType_Index = SO.DocumentType_Index"
            strSQL &= " WHERE	SO.SalesOrder_Index in (" & pstrSalesOrder_Indexs & ")"
            strSQL &= " GROUP BY MCS.Customer_Shipping_Location_Index, Customer_Shipping_Location_Id, MCS.Shipping_Location_Name, TR.Description, TR.TransportRegion_Index, MSC.Customer_Index, MSC.Customer_Name, "
            strSQL &= "             MSC.Customer_Id, dbo.ms_DocumentType.IsSend_OR_Recieve"
            strSQL &= " ORDER BY TR.TransportRegion_Index, MCS.Customer_Shipping_Location_Index"


            'strSQL &= " SELECT     MCS.Customer_Shipping_Index, MCS.Str1 AS Customer_Shipping_Id, MCS.Company_Name AS Customer_Shipping_Name, TR.TransportRegion_Index, "
            'strSQL &= "                      TR.Description AS TransportRegion_desc, CONVERT(Varchar(500), '') AS Description, MSC.Customer_Index, MSC.Customer_Name, MSC.Customer_Id, "
            'strSQL &= "                      dbo.ms_DocumentType.IsSend_OR_Recieve AS IsSendOrPickup, CONVERT(float, 0) AS Rate, CONVERT(float, 0) AS OverflowPerDrop, CONVERT(float, 0) "
            'strSQL &= "                      AS OverflowRate, CONVERT(float, 0) AS RateTransportPerDrop, SUM(dbo.tb_SalesOrderItem.Total_Qty) AS Total_Qty, SUM(dbo.tb_SalesOrderItem.Weight) AS Weight, "
            'strSQL &= "                                SUM(dbo.tb_SalesOrderItem.Volume) AS Volume"
            'strSQL &= " FROM         dbo.tb_SalesOrder AS SO INNER JOIN"
            'strSQL &= "                      dbo.ms_Customer AS MSC ON MSC.Customer_Index = SO.Customer_Index INNER JOIN"
            'strSQL &= "                      dbo.tb_SalesOrderItem ON SO.SalesOrder_Index = dbo.tb_SalesOrderItem.SalesOrder_Index LEFT OUTER JOIN"
            'strSQL &= "                      dbo.ms_Customer_Shipping AS MCS ON SO.Customer_Shipping_Index = MCS.Customer_Shipping_Index LEFT OUTER JOIN"
            'strSQL &= "                      dbo.ms_TransportRegion AS TR ON MCS.TransportRegion_Index = TR.TransportRegion_Index LEFT OUTER JOIN"
            'strSQL &= "                      dbo.ms_DocumentType ON dbo.ms_DocumentType.DocumentType_Index = SO.DocumentType_Index"
            'strSQL &= " WHERE	SO.SalesOrder_Index in (" & pstrSalesOrder_Indexs & ")"
            'strSQL &= " GROUP BY MCS.Customer_Shipping_Index, MCS.Str1, MCS.Company_Name, TR.Description, TR.TransportRegion_Index, MSC.Customer_Index, MSC.Customer_Name, "
            'strSQL &= "             MSC.Customer_Id, dbo.ms_DocumentType.IsSend_OR_Recieve"
            'strSQL &= " ORDER BY TR.TransportRegion_Index, MCS.Customer_Shipping_Index"

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


    Public Sub getCustomer_Carrier_Region_SalesOrder(ByVal pstrSalesOrder_Indexs As String)

        Dim strSQL As String = ""
        Try
            strSQL &= " SELECT      CRR.Carrier_Index, CRR.Carrier_Id, CRR.Description AS Carrier_Name, TR.TransportRegion_Index, TR.Description AS TransportRegion_desc "
            strSQL &= "             ,CONVERT(Varchar(500), '') AS Description, MSC.Customer_Index, MSC.Customer_Name, MSC.Customer_Id, MCS.Customer_Shipping_Location_Index "
            'strSQL &= "             ,MCS.Str1 AS Customer_Shipping_Location_Id, MCS.Shipping_Location_Name AS Customer_Shipping_Location_Name, CONVERT(float, 0) AS Rate, CONVERT(float, 0) AS OverflowPerDrop "
            strSQL &= "             ,Customer_Shipping_Location_Id, MCS.Shipping_Location_Name AS Customer_Shipping_Location_Name, CONVERT(float, 0) AS Rate, CONVERT(float, 0) AS OverflowPerDrop "
            strSQL &= "             ,CONVERT(float, 0) AS OverflowRate, CONVERT(float, 0) AS RateTransportPerDrop, dbo.ms_DocumentType.IsSend_OR_Recieve AS IsSendOrPickup"
            strSQL &= "             ,SUM(SOI.Total_Qty) AS Total_Qty"
            strSQL &= "             ,SUM(SOI.Weight) AS Weight"
            strSQL &= "             ,SUM(SOI.Volume) AS Volume"
            strSQL &= " FROM         dbo.tb_SalesOrder AS SO LEFT OUTER JOIN"
            strSQL &= "                       dbo.ms_DocumentType ON SO.DocumentType_Index = dbo.ms_DocumentType.DocumentType_Index LEFT OUTER JOIN"
            strSQL &= "                       dbo.ms_Customer AS MSC ON MSC.Customer_Index = SO.Customer_Index LEFT OUTER JOIN"
            strSQL &= "                       dbo.ms_Carrier AS CRR ON SO.Carrier_Index = CRR.Carrier_Index LEFT OUTER JOIN"
            strSQL &= "                       dbo.ms_Customer_Shipping_Location AS MCS ON SO.Customer_Shipping_Location_Index = MCS.Customer_Shipping_Location_Index LEFT OUTER JOIN"
            strSQL &= "                       dbo.ms_TransportRegion AS TR ON TR.TransportRegion_Index = MCS.TransportRegion_Index LEFT OUTER JOIN"
            strSQL &= "                       dbo.tb_SalesOrderItem AS SOI  ON SOI.SalesOrder_Index = SO.SalesOrder_Index"
            strSQL &= " WHERE	SO.SalesOrder_Index in (" & pstrSalesOrder_Indexs & ")"
            strSQL &= "  GROUP BY CRR.Carrier_Index, CRR.Carrier_Id, CRR.Description, TR.Description, TR.TransportRegion_Index, MSC.Customer_Index, MSC.Customer_Name, MSC.Customer_Id, "
            'strSQL &= "             MCS.Customer_Shipping_Location_Index, MCS.Str1, MCS.Shipping_Location_Name, dbo.ms_DocumentType.IsSend_OR_Recieve"
            strSQL &= "             MCS.Customer_Shipping_Location_Index,Customer_Shipping_Location_Id , MCS.Shipping_Location_Name, dbo.ms_DocumentType.IsSend_OR_Recieve"
            strSQL &= " ORDER BY TR.TransportRegion_Index, CRR.Carrier_Index"

            'strSQL &= " SELECT      CRR.Carrier_Index, CRR.Carrier_Id, CRR.Description AS Carrier_Name, TR.TransportRegion_Index, TR.Description AS TransportRegion_desc "
            'strSQL &= "             ,CONVERT(Varchar(500), '') AS Description, MSC.Customer_Index, MSC.Customer_Name, MSC.Customer_Id, MCS.Customer_Shipping_Index "
            'strSQL &= "             ,MCS.Str1 AS Customer_Shipping_Id, MCS.Company_Name AS Customer_Shipping_Name, CONVERT(float, 0) AS Rate, CONVERT(float, 0) AS OverflowPerDrop "
            'strSQL &= "             ,CONVERT(float, 0) AS OverflowRate, CONVERT(float, 0) AS RateTransportPerDrop, dbo.ms_DocumentType.IsSend_OR_Recieve AS IsSendOrPickup"
            'strSQL &= "             ,SUM(SOI.Total_Qty) AS Total_Qty"
            'strSQL &= "             ,SUM(SOI.Weight) AS Weight"
            'strSQL &= "             ,SUM(SOI.Volume) AS Volume"
            'strSQL &= " FROM         dbo.tb_SalesOrder AS SO LEFT OUTER JOIN"
            'strSQL &= "                       dbo.ms_DocumentType ON SO.DocumentType_Index = dbo.ms_DocumentType.DocumentType_Index LEFT OUTER JOIN"
            'strSQL &= "                       dbo.ms_Customer AS MSC ON MSC.Customer_Index = SO.Customer_Index LEFT OUTER JOIN"
            'strSQL &= "                       dbo.ms_Carrier AS CRR ON SO.Carrier_Index = CRR.Carrier_Index LEFT OUTER JOIN"
            'strSQL &= "                       dbo.ms_Customer_Shipping AS MCS ON SO.Customer_Shipping_Index = MCS.Customer_Shipping_Index LEFT OUTER JOIN"
            'strSQL &= "                       dbo.ms_TransportRegion AS TR ON TR.TransportRegion_Index = MCS.TransportRegion_Index LEFT OUTER JOIN"
            'strSQL &= "                       dbo.tb_SalesOrderItem AS SOI  ON SOI.SalesOrder_Index = SO.SalesOrder_Index"
            'strSQL &= " WHERE	SO.SalesOrder_Index in (" & pstrSalesOrder_Indexs & ")"
            'strSQL &= "  GROUP BY CRR.Carrier_Index, CRR.Carrier_Id, CRR.Description, TR.Description, TR.TransportRegion_Index, MSC.Customer_Index, MSC.Customer_Name, MSC.Customer_Id, "
            'strSQL &= "             MCS.Customer_Shipping_Index, MCS.Str1, MCS.Company_Name, dbo.ms_DocumentType.IsSend_OR_Recieve"
            'strSQL &= " ORDER BY TR.TransportRegion_Index, CRR.Carrier_Index"

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

    Public Function Validate_Edit_TM(ByVal TransportManifestItem_Index As String) As Boolean
        Try
            Dim SQL As New System.Text.StringBuilder

            With SQL
                .Append(" SELECT SaleOrders_Status2 FROM VIEW_TransportManifest_OnTruck ")
                .Append(" WHERE TransportManifestItem_Index = @TransportManifestItem_Index ")
                .Append("")
            End With

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@TransportManifestItem_Index", SqlDbType.VarChar).Value = TransportManifestItem_Index
            End With

            If DBExeQuery_Scalar(SQL.ToString) = 2 Then
                Return True
            Else
                Return False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "SAVE ORDER "
    ' case new document : you will get generate document id
    Public Function SaveData() As String
        Try
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    Me._scalarOutput = Me.InsertData()

                    If Not Me._scalarOutput = "" Then
                        ' Success 
                        Return Me._scalarOutput
                    Else
                        ' Not Success 
                        Return ""
                    End If
                Case enuOperation_Type.COPY
                    Me._scalarOutput = Me.InsertData()

                    If Not Me._scalarOutput = "" Then
                        ' Success 
                        Return Me._scalarOutput
                    Else
                        ' Not Success 
                        Return ""
                    End If
                Case enuOperation_Type.UPDATE
                    Me._scalarOutput = Me.UpdateData
                    If Not Me._scalarOutput = "" Then
                        ' Success 
                        Return Me._scalarOutput
                    Else
                        ' Not Success 
                        Return ""
                    End If
                Case enuOperation_Type.DELETE
                    Me._scalarOutput = Me.Delete(enuDelete.DELETEALL)
                    If Not Me._scalarOutput = "" Then
                        ' Success 
                        Return Me._scalarOutput
                    Else
                        ' Not Success 
                        Return ""
                    End If
            End Select

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "ADD NEW ORDER"
    Private Function InsertData() As String
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            Dim objDBIndex As New Sy_AutoNumber
            Dim objDocumentNumber As New Sy_DocumentNumber
            If _objHeader.TransportManifest_No = "" Then
                _objHeader.TransportManifest_No = objDocumentNumber.Auto_DocumentType_Number(_objHeader.DocumentType_Index, "")
            End If

            _TransportManifest_No = _objHeader.TransportManifest_No 'For Return 

            ' ******************************************************************
            strSQL = " INSERT INTO tb_TransportManifest(TransportManifest_Index,TransportManifest_No,TransportManifest_Date,Trip_Sequence,Trip_Description,DocumentType_Index,TransportJobType_Index,IsInternalVehicle,IsContainerHaulage,IsMultiDrop,IsMilkRun,Has_Backhaul,Has_PickupReturnItem,Vehicle_Index,Driver_Index,TruckStaff_Index,VehicleType_Index,Vehicle_License_No,Tail_License_No,Customer_Index,Customer_Receive_Location_Index,Customer_Shipping_Index,Customer_Shipping_Location_Index,Department_Index,DistributionCenter_Index,Transport_From,Transport_To,Route_Index,SubRoute_Index,HandlingType_Index,Container_No1,Container_No2,Container_Size_Index1,Container_Size_Index2,ContainerSeal_No1,ContainerSeal_No2,Booking_No,Comment,Trip_Petro_Mile,Trip_Petro_Volume,Trip_Petro_UnitPrice,Trip_Petro_Amount,Trip_Gas_Mile,Trip_Gas_Volume,Trip_Gas_UnitPrice,Trip_Gas_Amount,Time_OutToPickup,Time_SourceInGate,Time_SourceLoadStart,Time_SourceLoadFinish,Time_SourceOutGate,Time_DestinationInGate,Time_DestinationUnloadStart,Time_DestinationUnloadFinish,Time_DestinationOutGate,Time_ReturnTruckInGate,Time_ReturnTruckUnloadStart,Time_ReturnTruckUnloadFinish,Time_ReturnTruckOutGate,Mile_OutToPickup,Mile_AtSource,Mile_AtDestination,Mile_Return,add_by,add_date,add_branch,update_by,update_date,update_branch,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Status,IsSubManifest,Worker,Carrier_Index,TotalTransportPaid,TotalTransportCharged,IsTransportCharged,IsTransportPaid,isSpecialCase,DriverPaidAmount,IsPack)" & _
             "       VALUES(@TransportManifest_Index,@TransportManifest_No,@TransportManifest_Date,@Trip_Sequence,@Trip_Description,@DocumentType_Index,@TransportJobType_Index,@IsInternalVehicle,@IsContainerHaulage,@IsMultiDrop,@IsMilkRun,@Has_Backhaul,@Has_PickupReturnItem,@Vehicle_Index,@Driver_Index,@TruckStaff_Index,@VehicleType_Index,@Vehicle_License_No,@Tail_License_No,@Customer_Index,@Customer_Receive_Location_Index,@Customer_Shipping_Index,@Customer_Shipping_Location_Index,@Department_Index,@DistributionCenter_Index,@Transport_From,@Transport_To,@Route_Index,@SubRoute_Index,@HandlingType_Index,@Container_No1,@Container_No2,@Container_Size_Index1,@Container_Size_Index2,@ContainerSeal_No1,@ContainerSeal_No2,@Booking_No,@Comment,@Trip_Petro_Mile,@Trip_Petro_Volume,@Trip_Petro_UnitPrice,@Trip_Petro_Amount,@Trip_Gas_Mile,@Trip_Gas_Volume,@Trip_Gas_UnitPrice,@Trip_Gas_Amount,@Time_OutToPickup,@Time_SourceInGate,@Time_SourceLoadStart,@Time_SourceLoadFinish,@Time_SourceOutGate,@Time_DestinationInGate,@Time_DestinationUnloadStart,@Time_DestinationUnloadFinish,@Time_DestinationOutGate,@Time_ReturnTruckInGate,@Time_ReturnTruckUnloadStart,@Time_ReturnTruckUnloadFinish,@Time_ReturnTruckOutGate,@Mile_OutToPickup,@Mile_AtSource,@Mile_AtDestination,@Mile_Return,@add_by,getdate(),@add_branch,@update_by,getdate(),@update_branch,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Status,@IsSubManifest,@Worker,@Carrier_Index,@TotalTransportPaid,@TotalTransportCharged,@IsTransportCharged,@IsTransportPaid,@isSpecialCase,@DriverPaidAmount,@IsPack)"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objHeader.TransportManifest_Index
                .Add("@TransportManifest_No", SqlDbType.VarChar, 50).Value = _objHeader.TransportManifest_No
                .Add("@TransportManifest_Date", SqlDbType.DateTime, 23).Value = _objHeader.TransportManifest_Date
                .Add("@Trip_Sequence", SqlDbType.Int, 10).Value = _objHeader.Trip_Sequence
                .Add("@Trip_Description", SqlDbType.VarChar, 100).Value = _objHeader.Trip_Description
                .Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Add("@TransportJobType_Index", SqlDbType.VarChar, 13).Value = _objHeader.TransportJobType_Index
                .Add("@IsInternalVehicle", SqlDbType.Bit, 1).Value = _objHeader.IsInternalVehicle
                .Add("@IsContainerHaulage", SqlDbType.Bit, 1).Value = _objHeader.IsContainerHaulage
                .Add("@IsMultiDrop", SqlDbType.Bit, 1).Value = _objHeader.IsMultiDrop
                .Add("@IsMilkRun", SqlDbType.Bit, 1).Value = _objHeader.IsMilkRun
                .Add("@Has_Backhaul", SqlDbType.Bit, 1).Value = _objHeader.Has_Backhaul
                .Add("@Has_PickupReturnItem", SqlDbType.Bit, 1).Value = _objHeader.Has_PickupReturnItem
                .Add("@Vehicle_Index", SqlDbType.VarChar, 13).Value = _objHeader.Vehicle_Index
                .Add("@Driver_Index", SqlDbType.VarChar, 13).Value = _objHeader.Driver_Index
                .Add("@TruckStaff_Index", SqlDbType.VarChar, 13).Value = _objHeader.TruckStaff_Index
                .Add("@VehicleType_Index", SqlDbType.VarChar, 13).Value = _objHeader.VehicleType_Index
                .Add("@Vehicle_License_No", SqlDbType.VarChar, 13).Value = _objHeader.Vehicle_License_No
                .Add("@Tail_License_No", SqlDbType.VarChar, 50).Value = _objHeader.Tail_License_No
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Receive_Location_Index
                .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Shipping_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Shipping_Location_Index
                .Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Add("@DistributionCenter_Index", SqlDbType.VarChar, 13).Value = _objHeader.DistributionCenter_Index
                .Add("@Transport_From", SqlDbType.VarChar, 500).Value = _objHeader.Transport_From
                .Add("@Transport_To", SqlDbType.VarChar, 500).Value = _objHeader.Transport_To
                .Add("@Route_Index", SqlDbType.VarChar, 13).Value = _objHeader.Route_Index
                .Add("@SubRoute_Index", SqlDbType.VarChar, 13).Value = _objHeader.SubRoute_Index
                .Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index
                .Add("@Container_No1", SqlDbType.VarChar, 50).Value = _objHeader.Container_No1
                .Add("@Container_No2", SqlDbType.VarChar, 50).Value = _objHeader.Container_No2
                .Add("@Container_Size_Index1", SqlDbType.VarChar, 13).Value = _objHeader.Container_Size_Index1
                .Add("@Container_Size_Index2", SqlDbType.VarChar, 13).Value = _objHeader.Container_Size_Index2
                .Add("@ContainerSeal_No1", SqlDbType.VarChar, 50).Value = _objHeader.ContainerSeal_No1
                .Add("@ContainerSeal_No2", SqlDbType.VarChar, 50).Value = _objHeader.ContainerSeal_No2
                .Add("@Booking_No", SqlDbType.VarChar, 50).Value = _objHeader.Booking_No
                .Add("@Comment", SqlDbType.VarChar, 255).Value = _objHeader.Comment
                .Add("@Trip_Petro_Mile", SqlDbType.Int, 10).Value = _objHeader.Trip_Petro_Mile
                .Add("@Trip_Petro_Volume", SqlDbType.Float, 15).Value = _objHeader.Trip_Petro_Volume
                .Add("@Trip_Petro_UnitPrice", SqlDbType.Float, 15).Value = _objHeader.Trip_Petro_UnitPrice
                .Add("@Trip_Petro_Amount", SqlDbType.Float, 15).Value = _objHeader.Trip_Petro_Amount
                .Add("@Trip_Gas_Mile", SqlDbType.Int, 10).Value = _objHeader.Trip_Gas_Mile
                .Add("@Trip_Gas_Volume", SqlDbType.Float, 15).Value = _objHeader.Trip_Gas_Volume
                .Add("@Trip_Gas_UnitPrice", SqlDbType.Float, 15).Value = _objHeader.Trip_Gas_UnitPrice
                .Add("@Trip_Gas_Amount", SqlDbType.Float, 15).Value = _objHeader.Trip_Gas_Amount

                If _objHeader.Time_OutToPickup = Nothing Then
                    .Add("@Time_OutToPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_OutToPickup", SqlDbType.DateTime, 23).Value = _objHeader.Time_OutToPickup
                End If
                If _objHeader.Time_SourceInGate = Nothing Then
                    .Add("@Time_SourceInGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_SourceInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceInGate
                End If
                If _objHeader.Time_SourceLoadStart = Nothing Then
                    .Add("@Time_SourceLoadStart", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_SourceLoadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceLoadStart
                End If
                If _objHeader.Time_SourceLoadFinish = Nothing Then
                    .Add("@Time_SourceLoadFinish", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_SourceLoadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceLoadFinish
                End If
                If _objHeader.Time_SourceOutGate = Nothing Then
                    .Add("@Time_SourceOutGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_SourceOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceOutGate
                End If

                If _objHeader.Time_DestinationInGate = Nothing Then
                    .Add("@Time_DestinationInGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_DestinationInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationInGate
                End If

                If _objHeader.Time_DestinationUnloadStart = Nothing Then
                    .Add("@Time_DestinationUnloadStart", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_DestinationUnloadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationUnloadStart
                End If

                If _objHeader.Time_DestinationUnloadFinish = Nothing Then
                    .Add("@Time_DestinationUnloadFinish", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_DestinationUnloadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationUnloadFinish
                End If
                If _objHeader.Time_DestinationOutGate = Nothing Then
                    .Add("@Time_DestinationOutGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_DestinationOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationOutGate
                End If
                If _objHeader.Time_ReturnTruckInGate = Nothing Then
                    .Add("@Time_ReturnTruckInGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_ReturnTruckInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckInGate
                End If

                If _objHeader.Time_ReturnTruckUnloadStart = Nothing Then
                    .Add("@Time_ReturnTruckUnloadStart", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_ReturnTruckUnloadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckUnloadStart
                End If
                If _objHeader.Time_ReturnTruckUnloadFinish = Nothing Then
                    .Add("@Time_ReturnTruckUnloadFinish", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_ReturnTruckUnloadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckUnloadFinish
                End If

                If _objHeader.Time_ReturnTruckOutGate = Nothing Then
                    .Add("@Time_ReturnTruckOutGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                Else
                    .Add("@Time_ReturnTruckOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckOutGate
                End If

                '    .Add("@Time_OutToPickup", SqlDbType.DateTime, 23).Value = _objHeader.Time_OutToPickup
                '    .Add("@Time_SourceInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceInGate
                '  .Add("@Time_SourceLoadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceLoadStart
                '.Add("@Time_SourceLoadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceLoadFinish
                '    .Add("@Time_SourceOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceOutGate
                '   .Add("@Time_DestinationInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationInGate
                '  .Add("@Time_DestinationUnloadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationUnloadStart
                '    .Add("@Time_DestinationUnloadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationUnloadFinish
                '  .Add("@Time_DestinationOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationOutGate
                '    .Add("@Time_ReturnTruckInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckInGate
                '   .Add("@Time_ReturnTruckUnloadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckUnloadStart
                '  .Add("@Time_ReturnTruckUnloadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckUnloadFinish
                '    .Add("@Time_ReturnTruckOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckOutGate


                .Add("@Mile_OutToPickup", SqlDbType.Int, 10).Value = _objHeader.Mile_OutToPickup
                .Add("@Mile_AtSource", SqlDbType.Int, 10).Value = _objHeader.Mile_AtSource
                .Add("@Mile_AtDestination", SqlDbType.Int, 10).Value = _objHeader.Mile_AtDestination
                .Add("@Mile_Return", SqlDbType.Int, 10).Value = _objHeader.Mile_Return
                .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                '.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = _add_date
                .Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                ''.Add("@update_date", SqlDbType.SmallDateTime, 16).Value = _update_date
                .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                '.Add("@cancel_by", SqlDbType.VarChar, 50).Value = _cancel_by
                '.Add("@cancel_date", SqlDbType.SmallDateTime, 16).Value = _cancel_date
                '.Add("@cancel_branch", SqlDbType.Int, 10).Value = _cancel_branch
                .Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objHeader.Str9
                .Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objHeader.Str10
                .Add("@Status", SqlDbType.Int, 10).Value = _objHeader.Status
                .Add("@IsSubManifest", SqlDbType.Int, 10).Value = _objHeader.IsSubManifest
                .Add("@Worker", SqlDbType.Int, 10).Value = _objHeader.Worker
                .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _objHeader.Carrier_Index

                .Add("@TotalTransportPaid", SqlDbType.Float, 15).Value = _objHeader.TotalTransportPaid
                .Add("@TotalTransportCharged", SqlDbType.Float, 15).Value = _objHeader.TotalTransportCharged

                .Add("@IsTransportCharged", SqlDbType.Bit, 10).Value = _objHeader.isTransportCharged
                .Add("@IsTransportPaid", SqlDbType.Bit, 10).Value = _objHeader.isTransportPaid
                .Add("@isSpecialCase", SqlDbType.Bit, 10).Value = _objHeader.isSpecialCase
                .Add("@DriverPaidAmount", SqlDbType.Float, 15).Value = _objHeader.DriverPaidAmount
                .Add("@IsPack", SqlDbType.Bit).Value = _objHeader.IsPack

            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            EXEC_Command()



            For Each _objItem In _objItemCollection

                strSQL = " INSERT INTO tb_TransportManifestItem(TransportManifestItem_Index,TransportManifest_Index,Process_Id,SalesOrder_Index,Withdraw_Index,Order_Index,Invoice_No,DO_No,Qty_Shipped,Weight_Shipped,Volume_Shipped,Pallet_Shipped,Value_Shipped,Qty_Delivered,Weight_Delivered,Volume_Delivered,Pallet_Delivered,Value_Delivered,Qty_Returned,Weight_Returned,Volume_Returned,Pallet_Returned,Value_Returned,Qty_Lost,Weight_Lost,Volume_Lost,Pallet_Lost,Value_Lost,Qty_Damaged,Weight_Damaged,Volume_Damaged,Pallet_Damaged,Value_Damaged,Qty_NeedReShipped,Weight_NeedReShipped,Volume_NeedReShipped,Pallet_NeedReShipped,Value_NeedReShipped,JobProblem_Index,JobProblem_Desc,ResponsibleParty_Index,JobSolution_Index,JobSolution_Desc,Comment,Customer_Index,Customer_Receive_Location_Index,Customer_Shipping_Index,Customer_Shipping_Location_Index,Department_Index,Appointment_Date,Appointment_Time,DocumentReturn_Method_Index,Time_ExpectedDocPickup,Time_ExpectedDeliveryToDestination,Time_ExpectedReturnPickup,Time_ExpectedDocReturnToOwner,Time_DocIssued,Time_DocPickup,Time_DocTripConfirmed,Time_DeliveryToDestination,Time_ReturnPickup,Time_DocReturnedToDC,Time_DocReturnedToSource,Time_DocReturnedToOwner,Time_DocConfirmedByOwner,Time_NextDeliveryToDestination,Time_NextReturnPickup,Mile_AtDestination,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,Flo1,Flo2,Flo3,Flo4,Flo5,Flo6,Flo7,Flo8,Flo9,Flo10,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_date,add_branch,update_by,update_date,update_branch,Status, IsTransportCharged,IsTransportPaid,IsSendOrPickup,IsPacking)" & _
             "       VALUES(@TransportManifestItem_Index,@TransportManifest_Index,@Process_Id,@SalesOrder_Index,@Withdraw_Index,@Order_Index,@Invoice_No,@DO_No,@Qty_Shipped,@Weight_Shipped,@Volume_Shipped,@Pallet_Shipped,@Value_Shipped,@Qty_Delivered,@Weight_Delivered,@Volume_Delivered,@Pallet_Delivered,@Value_Delivered,@Qty_Returned,@Weight_Returned,@Volume_Returned,@Pallet_Returned,@Value_Returned,@Qty_Lost,@Weight_Lost,@Volume_Lost,@Pallet_Lost,@Value_Lost,@Qty_Damaged,@Weight_Damaged,@Volume_Damaged,@Pallet_Damaged,@Value_Damaged,@Qty_NeedReShipped,@Weight_NeedReShipped,@Volume_NeedReShipped,@Pallet_NeedReShipped,@Value_NeedReShipped,@JobProblem_Index,@JobProblem_Desc,@ResponsibleParty_Index,@JobSolution_Index,@JobSolution_Desc,@Comment,@Customer_Index,@Customer_Receive_Location_Index,@Customer_Shipping_Index,@Customer_Shipping_Location_Index,@Department_Index,@Appointment_Date,@Appointment_Time,@DocumentReturn_Method_Index,@Time_ExpectedDocPickup,@Time_ExpectedDeliveryToDestination,@Time_ExpectedReturnPickup,@Time_ExpectedDocReturnToOwner,@Time_DocIssued,@Time_DocPickup,@Time_DocTripConfirmed,@Time_DeliveryToDestination,@Time_ReturnPickup,@Time_DocReturnedToDC,@Time_DocReturnedToSource,@Time_DocReturnedToOwner,@Time_DocConfirmedByOwner,@Time_NextDeliveryToDestination,@Time_NextReturnPickup,@Mile_AtDestination,@Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@Flo6,@Flo7,@Flo8,@Flo9,@Flo10,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@add_by,getdate(),@add_branch,@update_by,getdate(),@update_branch,@Status, @IsTransportCharged,@IsTransportPaid,@IsSendOrPickup,@IsPacking)"
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifestItem_Index
                    .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifest_Index
                    .Add("@Process_Id", SqlDbType.Int, 10).Value = _objItem.Process_Id
                    .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _objItem.SalesOrder_Index
                    .Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objItem.Withdraw_Index
                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objItem.Order_Index
                    .Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objItem.Invoice_No
                    .Add("@DO_No", SqlDbType.VarChar, 50).Value = _objItem.DO_No
                    .Add("@Qty_Shipped", SqlDbType.Float, 15).Value = _objItem.Qty_Shipped
                    .Add("@Weight_Shipped", SqlDbType.Float, 15).Value = _objItem.Weight_Shipped
                    .Add("@Volume_Shipped", SqlDbType.Float, 15).Value = _objItem.Volume_Shipped
                    .Add("@Pallet_Shipped", SqlDbType.Float, 15).Value = _objItem.Pallet_Shipped
                    .Add("@Value_Shipped", SqlDbType.Float, 15).Value = _objItem.Value_Shipped
                    .Add("@Qty_Delivered", SqlDbType.Float, 15).Value = _objItem.Qty_Delivered
                    .Add("@Weight_Delivered", SqlDbType.Float, 15).Value = _objItem.Weight_Delivered
                    .Add("@Volume_Delivered", SqlDbType.Float, 15).Value = _objItem.Volume_Delivered
                    .Add("@Pallet_Delivered", SqlDbType.Float, 15).Value = _objItem.Pallet_Delivered
                    .Add("@Value_Delivered", SqlDbType.Float, 15).Value = _objItem.Value_Delivered
                    .Add("@Qty_Returned", SqlDbType.Float, 15).Value = _objItem.Qty_Returned
                    .Add("@Weight_Returned", SqlDbType.Float, 15).Value = _objItem.Weight_Returned
                    .Add("@Volume_Returned", SqlDbType.Float, 15).Value = _objItem.Volume_Returned
                    .Add("@Pallet_Returned", SqlDbType.Float, 15).Value = _objItem.Pallet_Returned
                    .Add("@Value_Returned", SqlDbType.Float, 15).Value = _objItem.Value_Returned
                    .Add("@Qty_Lost", SqlDbType.Float, 15).Value = _objItem.Qty_Lost
                    .Add("@Weight_Lost", SqlDbType.Float, 15).Value = _objItem.Weight_Lost
                    .Add("@Volume_Lost", SqlDbType.Float, 15).Value = _objItem.Volume_Lost
                    .Add("@Pallet_Lost", SqlDbType.Float, 15).Value = _objItem.Pallet_Lost
                    .Add("@Value_Lost", SqlDbType.Float, 15).Value = _objItem.Value_Lost
                    .Add("@Qty_Damaged", SqlDbType.Float, 15).Value = _objItem.Qty_Damaged
                    .Add("@Weight_Damaged", SqlDbType.Float, 15).Value = _objItem.Weight_Damaged
                    .Add("@Volume_Damaged", SqlDbType.Float, 15).Value = _objItem.Volume_Damaged
                    .Add("@Pallet_Damaged", SqlDbType.Float, 15).Value = _objItem.Pallet_Damaged
                    .Add("@Value_Damaged", SqlDbType.Float, 15).Value = _objItem.Value_Damaged
                    .Add("@Qty_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Qty_NeedReShipped
                    .Add("@Weight_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Weight_NeedReShipped
                    .Add("@Volume_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Volume_NeedReShipped
                    .Add("@Pallet_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Pallet_NeedReShipped
                    .Add("@Value_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Value_NeedReShipped
                    .Add("@JobProblem_Index", SqlDbType.VarChar, 13).Value = _objItem.JobProblem_Index
                    .Add("@JobProblem_Desc", SqlDbType.VarChar, 500).Value = _objItem.JobProblem_Desc
                    .Add("@ResponsibleParty_Index", SqlDbType.VarChar, 13).Value = _objItem.ResponsibleParty_Index
                    .Add("@JobSolution_Index", SqlDbType.VarChar, 13).Value = _objItem.JobSolution_Index
                    .Add("@JobSolution_Desc", SqlDbType.VarChar, 500).Value = _objItem.JobSolution_Desc
                    .Add("@Comment", SqlDbType.VarChar, 500).Value = _objItem.Comment
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Index
                    .Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Receive_Location_Index
                    .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Shipping_Index
                    .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Shipping_Location_Index
                    .Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objItem.Department_Index
                    .Add("@Appointment_Date", SqlDbType.DateTime, 23).Value = _objItem.Appointment_Date
                    .Add("@Appointment_Time", SqlDbType.VarChar, 100).Value = _objItem.Appointment_Time
                    .Add("@DocumentReturn_Method_Index", SqlDbType.VarChar, 50).Value = _objItem.DocumentReturn_Method_Index


                    If _objItem.Time_ExpectedDocPickup = Nothing Then
                        .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocPickup
                    End If
                    If _objItem.Time_ExpectedDeliveryToDestination = Nothing Then
                        .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDeliveryToDestination
                    End If
                    If _objItem.Time_ExpectedReturnPickup = Nothing Then
                        .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedReturnPickup
                    End If
                    If _objItem.Time_ExpectedDocReturnToOwner = Nothing Then
                        .Add("@Time_ExpectedDocReturnToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedDocReturnToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocReturnToOwner
                    End If
                    If _objItem.Time_DocIssued = Nothing Then
                        .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = _objItem.Time_DocIssued
                    End If
                    If _objItem.Time_DocPickup = Nothing Then
                        .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_DocPickup
                    End If
                    If _objItem.Time_DocTripConfirmed = Nothing Then
                        .Add("@Time_DocTripConfirmed", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocTripConfirmed", SqlDbType.DateTime, 23).Value = _objItem.Time_DocTripConfirmed
                    End If
                    If _objItem.Time_DeliveryToDestination = Nothing Then
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    End If
                    If _objItem.Time_ReturnPickup = Nothing Then
                        .Add("@Time_ReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ReturnPickup
                    End If
                    If _objItem.Time_DocReturnedToDC = Nothing Then
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    End If

                    If _objItem.Time_DocReturnedToSource = Nothing Then
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    End If
                    If _objItem.Time_DocReturnedToOwner = Nothing Then
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    End If
                    If _objItem.Time_DocConfirmedByOwner = Nothing Then
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    End If
                    If _objItem.Time_NextDeliveryToDestination = Nothing Then
                        .Add("@Time_NextDeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_NextDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_NextDeliveryToDestination
                    End If
                    If _objItem.Time_NextReturnPickup = Nothing Then
                        .Add("@Time_NextReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_NextReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_NextReturnPickup
                    End If
                    '   .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocPickup
                    '.Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDeliveryToDestination
                    '.Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedReturnPickup
                    '.Add("@Time_ExpectedDocReturnToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocReturnToOwner
                    ' .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = _objItem.Time_DocIssued
                    '   .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_DocPickup
                    ' .Add("@Time_DocTripConfirmed", SqlDbType.DateTime, 23).Value = _objItem.Time_DocTripConfirmed
                    '.Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    ' .Add("@Time_ReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ReturnPickup
                    '  .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    '    .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    '  .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    '  .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    '   .Add("@Time_NextDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_NextDeliveryToDestination
                    '   .Add("@Time_NextReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_NextReturnPickup



                    .Add("@Mile_AtDestination", SqlDbType.Int, 10).Value = _objItem.Mile_AtDestination
                    .Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objItem.Ref_No1
                    .Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objItem.Ref_No2
                    .Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objItem.Ref_No3
                    .Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objItem.Ref_No4
                    .Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objItem.Ref_No5
                    .Add("@Flo1", SqlDbType.Float, 15).Value = _objItem.Flo1
                    .Add("@Flo2", SqlDbType.Float, 15).Value = _objItem.Flo2
                    .Add("@Flo3", SqlDbType.Float, 15).Value = _objItem.Flo3
                    .Add("@Flo4", SqlDbType.Float, 15).Value = _objItem.Flo4
                    .Add("@Flo5", SqlDbType.Float, 15).Value = _objItem.Flo5
                    .Add("@Flo6", SqlDbType.Float, 15).Value = _objItem.Flo6
                    .Add("@Flo7", SqlDbType.Float, 15).Value = _objItem.Flo7
                    .Add("@Flo8", SqlDbType.Float, 15).Value = _objItem.Flo8
                    .Add("@Flo9", SqlDbType.Float, 15).Value = _objItem.Flo9
                    .Add("@Flo10", SqlDbType.Float, 15).Value = _objItem.Flo10
                    .Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Add("@Str2", SqlDbType.NVarChar, 100).Value = _objItem.Str2
                    .Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Add("@Str9", SqlDbType.NVarChar, 100).Value = _objItem.Str9
                    .Add("@Str10", SqlDbType.NVarChar, 100).Value = _objItem.Str10
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    '.Add("@add_date", SqlDbType.smalldatetime, 16).Value = _objItem.add_date
                    .Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    '.Add("@update_date", SqlDbType.smalldatetime, 16).Value = _objItem.update_date
                    .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    '.Add("@cancel_by", SqlDbType.varchar, 50).Value = _objItem.cancel_by
                    '.Add("@cancel_date", SqlDbType.varchar, 50).Value = _objItem.cancel_date
                    '.Add("@cancel_branch", SqlDbType.int, 10).Value = _objItem.cancel_branch
                    .Add("@Status", SqlDbType.Int, 10).Value = _objItem.Status
                    .Add("@IsTransportCharged", SqlDbType.Bit, 10).Value = _objItem.IsTransportCharged
                    .Add("@IsTransportPaid", SqlDbType.Bit, 10).Value = _objItem.IsTransportPaid

                    .Add("@IsSendOrPickup", SqlDbType.Int, 4).Value = _objItem.IsSendOrPickup
                    .Add("@IsPacking", SqlDbType.Int, 4).Value = _objItem.IsPacking

                    SetSQLString = strSQL
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()


                    '     If _objHeader.IsSubManifest = 0 Then
                    strSQL = " UPDATE tb_SalesOrder" & _
                                           " SET Status_Manifest=@Status_Manifest" & _
                                           "           WHERE          SalesOrder_Index = @SalesOrder_Index"
                    .Clear()
                    .Add("@SalesOrder_Index", SqlDbType.NVarChar, 100).Value = _objItem.SalesOrder_Index
                    .Add("@Status_Manifest", SqlDbType.Int, 10).Value = _objHeader.Status

                    SetSQLString = strSQL
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()
                    '   End If

                    ' Autoi insert tb_SalesOrderTrip

                    Dim strSalesOrderTrip_No As Integer = 1
                    Dim strSalesOrderTrip_Index As String = ""


                    strSalesOrderTrip_Index = objDBIndex.getSys_Value("SalesOrderTrip_Index")
                    strSalesOrderTrip_No = (SelectMax_Trip_No(Connection, myTrans, "SalesOrderTrip_No", "tb_SalesOrderTrip", _objItem.SalesOrder_Index))


                    strSQL = "    INSERT INTO tb_SalesOrderTrip(TransportManifest_Index,TransportManifestItem_Index,SalesOrderTrip_Index,SalesOrderTrip_No,SalesOrder_Index"
                    strSQL &= "   ,Time_ExpectedDocPickup,Time_ExpectedReturnPickup,Time_DocIssued,Time_DocPickup,Time_ExpectedDeliveryToDestination,Time_ExpectedDeliveryToDestination_Remark,add_date,add_by,add_branch,Status"
                    strSQL &= "   ,IsTransportPaid,IsTransportCharged,Status_Manifest)"
                    'strSQL &= "                            VALUES(@TransportManifest_Index,@TransportManifestItem_Index,@SalesOrderTrip_Index,@SalesOrderTrip_No,@SalesOrder_Index,@Time_ExpectedDocPickup,@Time_ExpectedReturnPickup,@Time_DocIssued,@Time_DocPickup,@Time_ExpectedDeliveryToDestination,@Time_ExpectedDeliveryToDestination_Remark,getdate(),@add_by,@add_branch,1)"
                    strSQL &= "   (SELECT    @TransportManifest_Index,@TransportManifestItem_Index,@SalesOrderTrip_Index,@SalesOrderTrip_No,@SalesOrder_Index"
                    strSQL &= "             ,Time_ExpectedDocPickup,Expected_Delivery_Date,SalesOrder_Date,Time_DocPickup,Expected_Delivery_Date,@Time_ExpectedDeliveryToDestination_Remark,getdate(),@add_by,@add_branch,1"
                    strSQL &= "             ,@IsTransportPaid,@IsTransportCharged,@Status_Manifest"
                    strSQL &= "   FROM      tb_SalesOrder   "
                    strSQL &= "   WHERE     SalesOrder_Index=@SalesOrder_Index) "
                    .Clear()
                    .Add("@SalesOrderTrip_Index", SqlDbType.VarChar, 13).Value = strSalesOrderTrip_Index
                    .Add("@SalesOrderTrip_No", SqlDbType.VarChar, 13).Value = strSalesOrderTrip_No
                    .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifest_Index
                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifestItem_Index
                    .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _objItem.SalesOrder_Index
                    .Add("@Time_ExpectedDeliveryToDestination_Remark", SqlDbType.VarChar, 500).Value = _objItem.Time_ExpectedDeliveryToDestination_Remark
                    .Add("@IsTransportPaid", SqlDbType.VarChar, 13).Value = _objItem.IsTransportPaid
                    .Add("@IsTransportCharged", SqlDbType.VarChar, 13).Value = _objItem.IsTransportCharged
                    .Add("@Status_Manifest", SqlDbType.Int, 10).Value = _objHeader.Status
                    'If _objItem.Time_ExpectedDocPickup = Nothing Then
                    '    .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    'Else
                    '    .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocPickup
                    'End If
                    'If _objItem.Time_ExpectedReturnPickup = Nothing Then
                    '    .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    'Else
                    '    .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedReturnPickup
                    'End If
                    'If _objItem.Time_DocIssued = Nothing Then
                    '    .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = DBNull.Value
                    'Else
                    '    .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = _objItem.Time_DocIssued
                    'End If
                    'If _objItem.Time_DocPickup = Nothing Then
                    '    .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    'Else
                    '    .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_DocPickup
                    'End If
                    'If _objItem.Time_ExpectedDeliveryToDestination = Nothing Then
                    '    .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    'Else
                    '    .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDeliveryToDestination
                    'End If

                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID

                    SetSQLString = strSQL
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()


                    'Dong add new 2011-11-10
                    'strSQL = " UPDATE   tb_SalesOrderTrip   "
                    'strSQL &= " SET     Flo10 = (  SELECT     isnull(count(*),0)"
                    'strSQL &= "                     FROM        tb_SalesOrderTrip INNER JOIN"
                    'strSQL &= "                                 tb_TransportManifestItem ON tb_SalesOrderTrip.TransportManifestItem_Index = tb_TransportManifestItem.TransportManifestItem_Index INNER JOIN"
                    'strSQL &= "                                 tb_TransportManifest ON tb_SalesOrderTrip.TransportManifest_Index = tb_TransportManifest.TransportManifest_Index"
                    'strSQL &= "                     WHERE     (tb_TransportManifest.Status <> - 1)"
                    'strSQL &= "                      AND (tb_SalesOrderTrip.SalesOrder_Index =  @SalesOrder_Index))"
                    'strSQL &= " WHERE   SalesOrderTrip_Index = @SalesOrderTrip_Index"

                    'With SQLServerCommand.Parameters
                    '    .Clear()
                    '    .Add("@SalesOrderTrip_Index", SqlDbType.VarChar, 13).Value = strSalesOrderTrip_Index
                    '    .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _objItem.SalesOrder_Index
                    'End With

                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()


                    'dong add new 2013-09-24
                    strSQL = " UPDATE   tb_SalesOrder   "
                    strSQL &= " SET     TransportManifest_No = @TransportManifest_No"
                    strSQL &= " WHERE   SalesOrder_Index = @SalesOrder_Index"
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@TransportManifest_No", SqlDbType.VarChar, 50).Value = _objHeader.TransportManifest_No
                        .Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = _objItem.SalesOrder_Index
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' 2017-05-03
                    strSQL = " UPDATE   tb_SalesOrder   "
                    strSQL &= " SET     DistributionCenter_Index = @DistributionCenter_Index"
                    strSQL &= " WHERE   SalesOrder_Index = @SalesOrder_Index"
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@DistributionCenter_Index", SqlDbType.VarChar, 13).Value = _objHeader.DistributionCenter_Index
                        .Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = _objItem.SalesOrder_Index
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                End With

            Next
            'Dong add new : 2012/09/24
            SetSQLString = " DELETE svar_TransportManifestCharge WHERE   TransportManifest_Index = @TransportManifest_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = Me._objHeader.TransportManifest_Index
            End With
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            For Each objChargeItem As svar_TransportManifestCharge In _objItemTransportManifestCharge
                strSQL = " INSERT INTO svar_TransportManifestCharge( " _
                            & "TransportManifest_Index" _
                            & ",Customer_Index" _
                            & ",TransportRegion_Index" _
                            & ",Carrier_Index" _
                            & ",Description" _
                            & ",sumDrop" _
                            & ",Total_Qty" _
                            & ",Weight" _
                            & ",Volume" _
                            & ",Amount" _
                            & ",Amount_Charge" _
                            & ",TransportCharge_Type,Minimum_Rate)" _
                            & " VALUES(" _
                            & "@TransportManifest_Index" _
                            & ",@Customer_Index" _
                            & ",@TransportRegion_Index" _
                            & ",@Carrier_Index" _
                            & ",@Description" _
                            & ",@sumDrop" _
                            & ",@Total_Qty" _
                            & ",@Weight" _
                            & ",@Volume" _
                            & ",@Amount" _
                            & ",@Amount_Charge" _
                            & ",@TransportCharge_Type,@Minimum_Rate)"

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = objChargeItem.TransportManifest_Index
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = objChargeItem.Customer_Index
                    .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = objChargeItem.TransportRegion_Index
                    .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = objChargeItem.Carrier_Index
                    .Add("@Description", SqlDbType.VarChar, 255).Value = objChargeItem.Description
                    .Add("@sumDrop", SqlDbType.Float).Value = objChargeItem.sumDrop
                    .Add("@Total_Qty", SqlDbType.Float).Value = objChargeItem.Total_Qty
                    .Add("@Weight", SqlDbType.Float).Value = objChargeItem.Weight
                    .Add("@Volume", SqlDbType.Float).Value = objChargeItem.Volume
                    .Add("@Amount", SqlDbType.Float).Value = objChargeItem.Amount
                    .Add("@Amount_Charge", SqlDbType.Float).Value = objChargeItem.Amount_Charge
                    .Add("@TransportCharge_Type", SqlDbType.Int).Value = objChargeItem.TransportCharge_Type
                    .Add("@Minimum_Rate", SqlDbType.Float).Value = objChargeItem.Minimum_Rate
                    SetSQLString = strSQL
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()
                End With

            Next



            'Dong_Add New : 2011/10/26
            Me._TransportManifest_Index = objDBIndex.getSys_Value("TransportManifest_Index")
            objDBIndex = Nothing
            'Update New TransportManifest_Index
            strSQL = " UPDATE   tb_TransportManifest   "
            strSQL &= " SET     TransportManifest_Index = @NewTransportManifest_Index"
            strSQL &= " WHERE   TransportManifest_Index = @TransportManifest_Index"

            strSQL &= " UPDATE   tb_TransportManifestItem   "
            strSQL &= " SET     TransportManifest_Index = @NewTransportManifest_Index"
            strSQL &= " WHERE   TransportManifest_Index = @TransportManifest_Index"

            strSQL &= " UPDATE   tb_SalesOrderTrip   "
            strSQL &= " SET     TransportManifest_Index = @NewTransportManifest_Index"
            strSQL &= " WHERE   TransportManifest_Index = @TransportManifest_Index"

            strSQL &= " UPDATE   x_TransportManifest_Driver   "
            strSQL &= " SET     TransportManifest_Index = @NewTransportManifest_Index"
            strSQL &= " WHERE   TransportManifest_Index = @TransportManifest_Index"

            strSQL &= " UPDATE   svar_TransportManifestCharge   "
            strSQL &= " SET     TransportManifest_Index = @NewTransportManifest_Index"
            strSQL &= " WHERE   TransportManifest_Index = @TransportManifest_Index"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@NewTransportManifest_Index", SqlDbType.VarChar, 13).Value = Me._TransportManifest_Index
                .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = Me._objHeader.TransportManifest_Index
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '*** Commit transaction
            myTrans.Commit()
            Return Me._TransportManifest_Index
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex

        Finally
            disconnectDB()
        End Try



    End Function
#End Region

#Region "UPDATE  "
    Private Function UpdateData() As String
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Dim objReader As SqlClient.SqlDataReader = Nothing
        Try

            Dim objDBIndex As New Sy_AutoNumber
            Dim objDocumentNumber As New Sy_DocumentNumber
            If _objHeader.TransportManifest_No = "" Then
                _objHeader.TransportManifest_No = objDocumentNumber.Auto_DocumentType_Number(_objHeader.DocumentType_Index, "")
            End If

            _TransportManifest_No = _objHeader.TransportManifest_No 'For Return 

            strSQL = " UPDATE tb_TransportManifest"
            strSQL &= " SET TransportManifest_Index=@TransportManifest_Index,"
            strSQL &= "     TransportManifest_No=@TransportManifest_No,"
            strSQL &= "     TransportManifest_Date=@TransportManifest_Date,"
            strSQL &= "     Trip_Sequence=@Trip_Sequence,"
            strSQL &= "     Trip_Description=@Trip_Description,"
            strSQL &= "     DocumentType_Index=@DocumentType_Index,"
            strSQL &= "     TransportJobType_Index=@TransportJobType_Index,"
            strSQL &= "     IsInternalVehicle=@IsInternalVehicle,"
            strSQL &= "     IsContainerHaulage=@IsContainerHaulage,"
            strSQL &= "     IsMultiDrop=@IsMultiDrop,"
            strSQL &= "     IsMilkRun=@IsMilkRun,"
            strSQL &= "     Has_Backhaul=@Has_Backhaul,"
            strSQL &= "     Has_PickupReturnItem=@Has_PickupReturnItem,"
            strSQL &= "     Vehicle_Index=@Vehicle_Index,"
            strSQL &= "     Driver_Index=@Driver_Index,"
            strSQL &= "     TruckStaff_Index=@TruckStaff_Index,"
            strSQL &= "     VehicleType_Index=@VehicleType_Index,"
            strSQL &= "     Vehicle_License_No=@Vehicle_License_No,"
            strSQL &= "     Tail_License_No=@Tail_License_No,"
            strSQL &= "     Customer_Index=@Customer_Index,"
            strSQL &= "     Customer_Receive_Location_Index=@Customer_Receive_Location_Index,"
            strSQL &= "     Customer_Shipping_Index=@Customer_Shipping_Index,"
            strSQL &= "     Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index,"
            strSQL &= "     Department_Index=@Department_Index,"
            strSQL &= "     DistributionCenter_Index=@DistributionCenter_Index,"
            strSQL &= "     Transport_From=@Transport_From,"
            strSQL &= "     Transport_To=@Transport_To,"
            strSQL &= "     Route_Index=@Route_Index,"
            strSQL &= "     SubRoute_Index=@SubRoute_Index,"
            strSQL &= "     HandlingType_Index=@HandlingType_Index,"
            strSQL &= "     Container_No1=@Container_No1,"
            strSQL &= "     Container_No2=@Container_No2,"
            strSQL &= "     Container_Size_Index1=@Container_Size_Index1,"
            strSQL &= "     Container_Size_Index2=@Container_Size_Index2,"
            strSQL &= "     ContainerSeal_No1=@ContainerSeal_No1,"
            strSQL &= "     ContainerSeal_No2=@ContainerSeal_No2,"
            strSQL &= "     Booking_No=@Booking_No,"
            strSQL &= "     Comment=@Comment,"
            strSQL &= "     Trip_Petro_Mile=@Trip_Petro_Mile,"
            strSQL &= "     Trip_Petro_Volume=@Trip_Petro_Volume,"
            strSQL &= "     Trip_Petro_UnitPrice=@Trip_Petro_UnitPrice,"
            strSQL &= "     Trip_Petro_Amount=@Trip_Petro_Amount,"
            strSQL &= "     Trip_Gas_Mile=@Trip_Gas_Mile,"
            strSQL &= "     Trip_Gas_Volume=@Trip_Gas_Volume,"
            strSQL &= "     Trip_Gas_UnitPrice=@Trip_Gas_UnitPrice,"
            strSQL &= "     Trip_Gas_Amount=@Trip_Gas_Amount,"



            If (_objHeader.isTripTime1 = True) Or (_objHeader.isTripTime2 = True) Then
                strSQL &= "     Time_SourceOutGate=@Time_SourceOutGate,"
                strSQL &= "     Time_DestinationInGate=@Time_DestinationInGate,"
                strSQL &= "     Time_ReturnTruckInGate=@Time_ReturnTruckInGate,"
            End If
            If (_objHeader.isTripTime2 = True) Then
                strSQL &= "     Time_OutToPickup=@Time_OutToPickup,"
                strSQL &= "     Time_SourceInGate=@Time_SourceInGate,"
                strSQL &= "     Time_SourceLoadStart=@Time_SourceLoadStart,"
                strSQL &= "     Time_SourceLoadFinish=@Time_SourceLoadFinish,"
                strSQL &= "     Time_DestinationUnloadStart=@Time_DestinationUnloadStart,"
                strSQL &= "     Time_DestinationUnloadFinish=@Time_DestinationUnloadFinish,"
                strSQL &= "     Time_DestinationOutGate=@Time_DestinationOutGate,"
                strSQL &= "     Time_ReturnTruckUnloadStart=@Time_ReturnTruckUnloadStart,"
                strSQL &= "     Time_ReturnTruckUnloadFinish=@Time_ReturnTruckUnloadFinish,"
                strSQL &= "     Time_ReturnTruckOutGate=@Time_ReturnTruckOutGate,"
            End If

            strSQL &= "     Mile_OutToPickup=@Mile_OutToPickup,"
            strSQL &= "     Mile_AtSource=@Mile_AtSource,"
            strSQL &= "     Mile_AtDestination=@Mile_AtDestination,"
            strSQL &= "     Mile_Return=@Mile_Return,"
            strSQL &= "     update_by=@update_by,"
            strSQL &= "     update_date=getdate(),"
            strSQL &= "     update_branch=@update_branch,"
            strSQL &= "     Str1=@Str1,"
            strSQL &= "     Str2=@Str2,"
            strSQL &= "     Str3=@Str3,"
            strSQL &= "     Str4=@Str4,"
            strSQL &= "     Str5=@Str5,"
            strSQL &= "     Str6=@Str6,"
            strSQL &= "     Str7=@Str7,"
            strSQL &= "     Str8=@Str8,"
            strSQL &= "     Str9=@Str9,"
            strSQL &= "     Str10=@Str10,"
            strSQL &= "     Worker=@Worker,"
            strSQL &= "     Carrier_Index=@Carrier_Index,"
            strSQL &= "     TotalTransportPaid=@TotalTransportPaid,"
            strSQL &= "     TotalTransportCharged=@TotalTransportCharged,"
            strSQL &= "     IsTransportCharged=@IsTransportCharged,"
            strSQL &= "     IsTransportPaid=@IsTransportPaid,"
            strSQL &= "     isSpecialCase=@isSpecialCase,"
            strSQL &= "     DriverPaidAmount=@DriverPaidAmount,"
            strSQL &= "     IsPack=@IsPack"
            strSQL &= "           WHERE          TransportManifest_Index = @TransportManifest_Index"


            With SQLServerCommand.Parameters
                .Clear()
                .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objHeader.TransportManifest_Index
                .Add("@TransportManifest_No", SqlDbType.VarChar, 50).Value = _objHeader.TransportManifest_No
                .Add("@TransportManifest_Date", SqlDbType.DateTime, 23).Value = _objHeader.TransportManifest_Date
                .Add("@Trip_Sequence", SqlDbType.Int, 10).Value = _objHeader.Trip_Sequence
                .Add("@Trip_Description", SqlDbType.VarChar, 100).Value = _objHeader.Trip_Description
                .Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Add("@TransportJobType_Index", SqlDbType.VarChar, 13).Value = _objHeader.TransportJobType_Index
                .Add("@IsInternalVehicle", SqlDbType.Bit, 1).Value = _objHeader.IsInternalVehicle
                .Add("@IsContainerHaulage", SqlDbType.Bit, 1).Value = _objHeader.IsContainerHaulage
                .Add("@IsMultiDrop", SqlDbType.Bit, 1).Value = _objHeader.IsMultiDrop
                .Add("@IsMilkRun", SqlDbType.Bit, 1).Value = _objHeader.IsMilkRun
                .Add("@Has_Backhaul", SqlDbType.Bit, 1).Value = _objHeader.Has_Backhaul
                .Add("@Has_PickupReturnItem", SqlDbType.Bit, 1).Value = _objHeader.Has_PickupReturnItem
                .Add("@Vehicle_Index", SqlDbType.VarChar, 13).Value = _objHeader.Vehicle_Index
                .Add("@Driver_Index", SqlDbType.VarChar, 13).Value = _objHeader.Driver_Index
                .Add("@TruckStaff_Index", SqlDbType.VarChar, 13).Value = _objHeader.TruckStaff_Index
                .Add("@VehicleType_Index", SqlDbType.VarChar, 13).Value = _objHeader.VehicleType_Index
                .Add("@Vehicle_License_No", SqlDbType.VarChar, 13).Value = _objHeader.Vehicle_License_No
                .Add("@Tail_License_No", SqlDbType.VarChar, 50).Value = _objHeader.Tail_License_No
                .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Receive_Location_Index
                .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Shipping_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Shipping_Location_Index
                .Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Add("@DistributionCenter_Index", SqlDbType.VarChar, 13).Value = _objHeader.DistributionCenter_Index
                .Add("@Transport_From", SqlDbType.VarChar, 500).Value = _objHeader.Transport_From
                .Add("@Transport_To", SqlDbType.VarChar, 500).Value = _objHeader.Transport_To
                .Add("@Route_Index", SqlDbType.VarChar, 13).Value = _objHeader.Route_Index
                .Add("@SubRoute_Index", SqlDbType.VarChar, 13).Value = _objHeader.SubRoute_Index
                .Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index
                .Add("@Container_No1", SqlDbType.VarChar, 50).Value = _objHeader.Container_No1
                .Add("@Container_No2", SqlDbType.VarChar, 50).Value = _objHeader.Container_No2
                .Add("@Container_Size_Index1", SqlDbType.VarChar, 13).Value = _objHeader.Container_Size_Index1
                .Add("@Container_Size_Index2", SqlDbType.VarChar, 13).Value = _objHeader.Container_Size_Index2
                .Add("@ContainerSeal_No1", SqlDbType.VarChar, 50).Value = _objHeader.ContainerSeal_No1
                .Add("@ContainerSeal_No2", SqlDbType.VarChar, 50).Value = _objHeader.ContainerSeal_No2
                .Add("@Booking_No", SqlDbType.VarChar, 50).Value = _objHeader.Booking_No
                .Add("@Comment", SqlDbType.VarChar, 255).Value = _objHeader.Comment
                .Add("@Trip_Petro_Mile", SqlDbType.Int, 10).Value = _objHeader.Trip_Petro_Mile
                .Add("@Trip_Petro_Volume", SqlDbType.Float, 15).Value = _objHeader.Trip_Petro_Volume
                .Add("@Trip_Petro_UnitPrice", SqlDbType.Float, 15).Value = _objHeader.Trip_Petro_UnitPrice
                .Add("@Trip_Petro_Amount", SqlDbType.Float, 15).Value = _objHeader.Trip_Petro_Amount
                .Add("@Trip_Gas_Mile", SqlDbType.Int, 10).Value = _objHeader.Trip_Gas_Mile
                .Add("@Trip_Gas_Volume", SqlDbType.Float, 15).Value = _objHeader.Trip_Gas_Volume
                .Add("@Trip_Gas_UnitPrice", SqlDbType.Float, 15).Value = _objHeader.Trip_Gas_UnitPrice
                .Add("@Trip_Gas_Amount", SqlDbType.Float, 15).Value = _objHeader.Trip_Gas_Amount

                If (_objHeader.isTripTime1 = True) Or (_objHeader.isTripTime2 = True) Then
                    If _objHeader.Time_OutToPickup = Nothing Then
                        .Add("@Time_OutToPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_OutToPickup", SqlDbType.DateTime, 23).Value = _objHeader.Time_OutToPickup
                    End If
                    If _objHeader.Time_SourceInGate = Nothing Then
                        .Add("@Time_SourceInGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_SourceInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceInGate
                    End If
                    If _objHeader.Time_ReturnTruckInGate = Nothing Then
                        .Add("@Time_ReturnTruckInGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ReturnTruckInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckInGate
                    End If

                    'krit fix bug 02/02/2012
                    If _objHeader.Time_SourceOutGate = Nothing Then
                        .Add("@Time_SourceOutGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_SourceOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceOutGate
                    End If

                    If _objHeader.Time_DestinationInGate = Nothing Then
                        .Add("@Time_DestinationInGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DestinationInGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationInGate
                    End If
                End If

                If (_objHeader.isTripTime2 = True) Then
                    If _objHeader.Time_SourceLoadStart = Nothing Then
                        .Add("@Time_SourceLoadStart", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_SourceLoadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceLoadStart
                    End If
                    If _objHeader.Time_SourceLoadFinish = Nothing Then
                        .Add("@Time_SourceLoadFinish", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_SourceLoadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_SourceLoadFinish
                    End If

                    If _objHeader.Time_DestinationUnloadStart = Nothing Then
                        .Add("@Time_DestinationUnloadStart", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DestinationUnloadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationUnloadStart
                    End If

                    If _objHeader.Time_DestinationUnloadFinish = Nothing Then
                        .Add("@Time_DestinationUnloadFinish", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DestinationUnloadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationUnloadFinish
                    End If
                    If _objHeader.Time_DestinationOutGate = Nothing Then
                        .Add("@Time_DestinationOutGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DestinationOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_DestinationOutGate
                    End If


                    If _objHeader.Time_ReturnTruckUnloadStart = Nothing Then
                        .Add("@Time_ReturnTruckUnloadStart", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ReturnTruckUnloadStart", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckUnloadStart
                    End If
                    If _objHeader.Time_ReturnTruckUnloadFinish = Nothing Then
                        .Add("@Time_ReturnTruckUnloadFinish", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ReturnTruckUnloadFinish", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckUnloadFinish
                    End If

                    If _objHeader.Time_ReturnTruckOutGate = Nothing Then
                        .Add("@Time_ReturnTruckOutGate", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ReturnTruckOutGate", SqlDbType.DateTime, 23).Value = _objHeader.Time_ReturnTruckOutGate
                    End If
                End If


                .Add("@Mile_OutToPickup", SqlDbType.Int, 10).Value = _objHeader.Mile_OutToPickup
                .Add("@Mile_AtSource", SqlDbType.Int, 10).Value = _objHeader.Mile_AtSource
                .Add("@Mile_AtDestination", SqlDbType.Int, 10).Value = _objHeader.Mile_AtDestination
                .Add("@Mile_Return", SqlDbType.Int, 10).Value = _objHeader.Mile_Return
                '.Add("@add_by", SqlDbType.VarChar, 50).Value = _objHeader.add_by
                '.Add("@add_date", SqlDbType.SmallDateTime, 16).Value = _objHeader.add_date
                '.Add("@add_branch", SqlDbType.Int, 10).Value = _objHeader.add_branch
                .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                '.Add("@update_date", SqlDbType.SmallDateTime, 16).Value = _objHeader.update_date
                .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                '.Add("@cancel_by", SqlDbType.VarChar, 50).Value = _objHeader.cancel_by
                '.Add("@cancel_date", SqlDbType.SmallDateTime, 16).Value = _objHeader.cancel_date
                '.Add("@cancel_branch", SqlDbType.Int, 10).Value = _objHeader.cancel_branch
                .Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objHeader.Str9
                .Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objHeader.Str10
                .Add("@Worker", SqlDbType.Int, 10).Value = _objHeader.Worker
                '   .Add("@Status", SqlDbType.Int, 10).Value = _objHeader.Status
                .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _objHeader.Carrier_Index
                .Add("@TotalTransportCharged", SqlDbType.Float, 15).Value = _objHeader.TotalTransportCharged
                .Add("@TotalTransportPaid", SqlDbType.Float, 15).Value = _objHeader.TotalTransportPaid

                .Add("@IsTransportCharged", SqlDbType.Bit, 10).Value = _objHeader.isTransportCharged
                .Add("@IsTransportPaid", SqlDbType.Bit, 10).Value = _objHeader.isTransportPaid
                .Add("@isSpecialCase", SqlDbType.Bit, 10).Value = _objHeader.isSpecialCase
                .Add("@DriverPaidAmount", SqlDbType.Float, 15).Value = _objHeader.DriverPaidAmount
                .Add("@IsPack", SqlDbType.Bit).Value = _objHeader.IsPack
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            EXEC_Command()


            For Each _objItem In _objItemCollection
                If isExistID(_objItem.TransportManifestItem_Index, _objItem.SalesOrder_Index) = True Then
                    strSQL = " UPDATE tb_TransportManifestItem SET  "
                    strSQL &= "         Process_Id=@Process_Id"
                    strSQL &= "       ,SalesOrder_Index=@SalesOrder_Index"
                    strSQL &= "       ,Withdraw_Index=@Withdraw_Index"
                    strSQL &= "       ,Order_Index=@Order_Index"
                    strSQL &= "       ,Invoice_No=@Invoice_No"
                    strSQL &= "       ,DO_No=@DO_No"
                    strSQL &= "       ,Qty_Shipped=@Qty_Shipped"
                    strSQL &= "       ,Weight_Shipped=@Weight_Shipped"
                    strSQL &= "       ,Volume_Shipped=@Volume_Shipped"
                    strSQL &= "       ,Pallet_Shipped=@Pallet_Shipped"
                    strSQL &= "       ,Value_Shipped=@Value_Shipped"
                    strSQL &= "       ,Qty_Delivered=@Qty_Delivered"
                    strSQL &= "       ,Weight_Delivered=@Weight_Delivered"
                    strSQL &= "       ,Volume_Delivered=@Volume_Delivered"
                    strSQL &= "       ,Pallet_Delivered=@Pallet_Delivered"
                    strSQL &= "       ,Value_Delivered=@Value_Delivered"
                    strSQL &= "       ,Qty_Returned=@Qty_Returned"
                    strSQL &= "       ,Weight_Returned=@Weight_Returned"
                    strSQL &= "       ,Volume_Returned=@Volume_Returned"
                    strSQL &= "       ,Pallet_Returned=@Pallet_Returned"
                    strSQL &= "       ,Value_Returned=@Value_Returned"
                    strSQL &= "       ,Qty_Lost=@Qty_Lost"
                    strSQL &= "       ,Weight_Lost=@Weight_Lost"
                    strSQL &= "       ,Volume_Lost=@Volume_Lost"
                    strSQL &= "       ,Pallet_Lost=@Pallet_Lost"
                    strSQL &= "       ,Value_Lost=@Value_Lost"
                    strSQL &= "       ,Qty_Damaged=@Qty_Damaged"
                    strSQL &= "       ,Weight_Damaged=@Weight_Damaged"
                    strSQL &= "       ,Volume_Damaged=@Volume_Damaged"
                    strSQL &= "       ,Pallet_Damaged=@Pallet_Damaged"
                    strSQL &= "       ,Value_Damaged=@Value_Damaged"
                    strSQL &= "       ,Qty_NeedReShipped=@Qty_NeedReShipped"
                    strSQL &= "       ,Weight_NeedReShipped=@Weight_NeedReShipped"
                    strSQL &= "       ,Volume_NeedReShipped=@Volume_NeedReShipped"
                    strSQL &= "       ,Pallet_NeedReShipped=@Pallet_NeedReShipped"
                    strSQL &= "       ,Value_NeedReShipped=@Value_NeedReShipped"
                    strSQL &= "       ,JobProblem_Index=@JobProblem_Index"
                    strSQL &= "       ,JobProblem_Desc=@JobProblem_Desc"
                    strSQL &= "       ,ResponsibleParty_Index=@ResponsibleParty_Index"
                    strSQL &= "       ,JobSolution_Index=@JobSolution_Index"
                    strSQL &= "       ,JobSolution_Desc=@JobSolution_Desc"
                    strSQL &= "       ,Comment=@Comment"
                    strSQL &= "       ,Customer_Index=@Customer_Index"
                    strSQL &= "       ,Customer_Receive_Location_Index=@Customer_Receive_Location_Index"
                    strSQL &= "       ,Customer_Shipping_Index=@Customer_Shipping_Index"
                    strSQL &= "       ,Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index"
                    strSQL &= "       ,Department_Index=@Department_Index"
                    strSQL &= "       ,Appointment_Date=@Appointment_Date"
                    strSQL &= "       ,Appointment_Time=@Appointment_Time"
                    strSQL &= "       ,DocumentReturn_Method_Index=@DocumentReturn_Method_Index"

                    'dong comment
                    'If (_objHeader.isTripTime1 = True) Or (_objHeader.isTripTime2 = True) Then
                    'End If
                    'If (_objHeader.isTripTime2 = True) Then
                    'End If

                    'strSQL &= "       ,Time_ExpectedDocPickup=@Time_ExpectedDocPickup"
                    'strSQL &= "       ,Time_ExpectedDeliveryToDestination=@Time_ExpectedDeliveryToDestination"
                    'strSQL &= "       ,Time_ExpectedReturnPickup=@Time_ExpectedReturnPickup"
                    'strSQL &= "       ,Time_ExpectedDocReturnToOwner=@Time_ExpectedDocReturnToOwner"
                    'strSQL &= "       ,Time_DocIssued=@Time_DocIssued,Time_DocPickup=@Time_DocPickup"
                    'strSQL &= "       ,Time_DocTripConfirmed=@Time_DocTripConfirmed"
                    'strSQL &= "       ,Time_DeliveryToDestination=@Time_DeliveryToDestination"
                    'strSQL &= "       ,Time_ReturnPickup=@Time_ReturnPickup"
                    'strSQL &= "       ,Time_DocReturnedToDC=@Time_DocReturnedToDC"
                    'strSQL &= "       ,Time_DocReturnedToSource=@Time_DocReturnedToSource"
                    'strSQL &= "       ,Time_DocReturnedToOwner=@Time_DocReturnedToOwner"
                    'strSQL &= "       ,Time_DocConfirmedByOwner=@Time_DocConfirmedByOwner"
                    'strSQL &= "       ,Time_NextDeliveryToDestination=@Time_NextDeliveryToDestination"
                    'strSQL &= "       ,Time_NextReturnPickup=@Time_NextReturnPickup"

                    strSQL &= "       ,Mile_AtDestination=@Mile_AtDestination"
                    strSQL &= "       ,Ref_No1=@Ref_No1"
                    strSQL &= "       ,Ref_No2=@Ref_No2"
                    strSQL &= "       ,Ref_No3=@Ref_No3"
                    strSQL &= "       ,Ref_No4=@Ref_No4"
                    strSQL &= "       ,Ref_No5=@Ref_No5"
                    strSQL &= "       ,Flo1=@Flo1"
                    strSQL &= "       ,Flo2=@Flo2"
                    strSQL &= "       ,Flo3=@Flo3"
                    strSQL &= "       ,Flo4=@Flo4"
                    strSQL &= "       ,Flo5=@Flo5"
                    strSQL &= "       ,Flo6=@Flo6"
                    strSQL &= "       ,Flo7=@Flo7"
                    strSQL &= "       ,Flo8=@Flo8"
                    strSQL &= "       ,Flo9=@Flo9"
                    strSQL &= "       ,Flo10=@Flo10"
                    strSQL &= "       ,Str1=@Str1"
                    strSQL &= "       ,Str2=@Str2"
                    strSQL &= "       ,Str3=@Str3"
                    strSQL &= "       ,Str4=@Str4"
                    strSQL &= "       ,Str5=@Str5"
                    strSQL &= "       ,Str6=@Str6"
                    strSQL &= "       ,Str7=@Str7"
                    strSQL &= "       ,Str8=@Str8"
                    strSQL &= "       ,Str9=@Str9"
                    strSQL &= "       ,Str10=@Str10"
                    '   strSQL &= "       ,add_by=@add_by"
                    '   strSQL &= "       ,add_date=@add_date"
                    '  strSQL &= "       ,add_branch=@add_branch"
                    strSQL &= "       ,update_by=@update_by"
                    strSQL &= "       ,update_date=getdate()"
                    strSQL &= "       ,update_branch=@update_branch"
                    'strSQL &= "       ,Status=@Status"
                    strSQL &= "       ,IsTransportPaid=@IsTransportPaid"
                    strSQL &= "       ,IsTransportCharged=@IsTransportCharged"
                    strSQL &= "       ,IsSendOrPickup=@IsSendOrPickup"
                    strSQL &= "       ,IsPacking=@IsPacking"
                    strSQL &= "           WHERE          TransportManifestItem_Index = @TransportManifestItem_Index"

                    strSQL &= " UPDATE tb_SalesOrderTrip SET  "
                    strSQL &= "       IsTransportPaid=@IsTransportPaid"
                    strSQL &= "       ,IsTransportCharged=@IsTransportCharged"
                    strSQL &= "           WHERE          TransportManifestItem_Index = @TransportManifestItem_Index"
                Else
                    With SQLServerCommand.Parameters
                        Dim strSalesOrderTrip_No As Integer = 1
                        Dim strSalesOrderTrip_Index As String = ""

                        'Dim objDBIndex As New Sy_AutoNumber
                        strSalesOrderTrip_Index = objDBIndex.getSys_Value("SalesOrderTrip_Index")
                        strSalesOrderTrip_No = (SelectMax_Trip_No(Connection, myTrans, "SalesOrderTrip_No", "tb_SalesOrderTrip", _objItem.SalesOrder_Index))
                        'objDBIndex = Nothing


                        strSQL = "    INSERT INTO tb_SalesOrderTrip(TransportManifest_Index,TransportManifestItem_Index,SalesOrderTrip_Index,SalesOrderTrip_No,SalesOrder_Index"
                        strSQL &= "   ,Time_ExpectedDocPickup,Time_ExpectedReturnPickup,Time_DocIssued,Time_DocPickup,Time_ExpectedDeliveryToDestination,Time_ExpectedDeliveryToDestination_Remark,add_date,add_by,add_branch,Status"
                        strSQL &= "   ,IsTransportPaid,IsTransportCharged,Status_Manifest)  "
                        strSQL &= "   (SELECT    @TransportManifest_Index,@TransportManifestItem_Index,@SalesOrderTrip_Index,@SalesOrderTrip_No,@SalesOrder_Index"
                        strSQL &= "             ,Time_ExpectedDocPickup,Expected_Delivery_Date,SalesOrder_Date,Time_DocPickup,Expected_Delivery_Date,@Time_ExpectedDeliveryToDestination_Remark,getdate(),@add_by,@add_branch,1"
                        strSQL &= "   ,@IsTransportPaid,@IsTransportCharged,@Status_Manifest   "
                        strSQL &= "   FROM      tb_SalesOrder   "
                        strSQL &= "   WHERE     SalesOrder_Index=@SalesOrder_Index) "
                        .Clear()
                        .Add("@SalesOrderTrip_Index", SqlDbType.VarChar, 13).Value = strSalesOrderTrip_Index
                        .Add("@SalesOrderTrip_No", SqlDbType.VarChar, 13).Value = strSalesOrderTrip_No
                        .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifest_Index
                        .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifestItem_Index
                        .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _objItem.SalesOrder_Index
                        .Add("@Time_ExpectedDeliveryToDestination_Remark", SqlDbType.VarChar, 500).Value = _objItem.Time_ExpectedDeliveryToDestination_Remark
                        '@IsTransportPaid,@IsTransportCharged
                        .Add("@IsTransportPaid", SqlDbType.Bit, 1).Value = _objItem.IsTransportPaid
                        .Add("@IsTransportCharged", SqlDbType.Bit, 1).Value = _objItem.IsTransportCharged
                        .Add("@Status_Manifest", SqlDbType.Int, 10).Value = _objHeader.Status
                        'If _objItem.Time_ExpectedDocPickup = Nothing Then
                        '    .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                        'Else
                        '    .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocPickup
                        'End If
                        'If _objItem.Time_ExpectedReturnPickup = Nothing Then
                        '    .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                        'Else
                        '    .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedReturnPickup
                        'End If
                        'If _objItem.Time_DocIssued = Nothing Then
                        '    .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = DBNull.Value
                        'Else
                        '    .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = _objItem.Time_DocIssued
                        'End If
                        'If _objItem.Time_DocPickup = Nothing Then
                        '    .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                        'Else
                        '    .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_DocPickup
                        'End If
                        'If _objItem.Time_ExpectedDeliveryToDestination = Nothing Then
                        '    .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                        'Else
                        '    .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDeliveryToDestination
                        'End If

                        .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID

                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        EXEC_Command()


                        strSQL = " UPDATE tb_SalesOrder" & _
                                                                  " SET Status_Manifest=@Status_Manifest" & _
                                                                  "           WHERE          SalesOrder_Index = @SalesOrder_Index"
                        .Clear()
                        .Add("@SalesOrder_Index", SqlDbType.NVarChar, 100).Value = _objItem.SalesOrder_Index
                        .Add("@Status_Manifest", SqlDbType.Int, 10).Value = 2

                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        EXEC_Command()
                    End With
                    'End With

                    strSQL = " INSERT INTO tb_TransportManifestItem(TransportManifestItem_Index,TransportManifest_Index,Process_Id,SalesOrder_Index,Withdraw_Index,Order_Index,Invoice_No,DO_No,Qty_Shipped,Weight_Shipped,Volume_Shipped,Pallet_Shipped,Value_Shipped,Qty_Delivered,Weight_Delivered,Volume_Delivered,Pallet_Delivered,Value_Delivered,Qty_Returned,Weight_Returned,Volume_Returned,Pallet_Returned,Value_Returned,Qty_Lost,Weight_Lost,Volume_Lost,Pallet_Lost,Value_Lost,Qty_Damaged,Weight_Damaged,Volume_Damaged,Pallet_Damaged,Value_Damaged,Qty_NeedReShipped,Weight_NeedReShipped,Volume_NeedReShipped,Pallet_NeedReShipped,Value_NeedReShipped,JobProblem_Index,JobProblem_Desc,ResponsibleParty_Index,JobSolution_Index,JobSolution_Desc,Comment,Customer_Index,Customer_Receive_Location_Index,Customer_Shipping_Index,Customer_Shipping_Location_Index,Department_Index,Appointment_Date,Appointment_Time,DocumentReturn_Method_Index,Time_ExpectedDocPickup,Time_ExpectedDeliveryToDestination,Time_ExpectedReturnPickup,Time_ExpectedDocReturnToOwner,Time_DocIssued,Time_DocPickup,Time_DocTripConfirmed,Time_DeliveryToDestination,Time_ReturnPickup,Time_DocReturnedToDC,Time_DocReturnedToSource,Time_DocReturnedToOwner,Time_DocConfirmedByOwner,Time_NextDeliveryToDestination,Time_NextReturnPickup,Mile_AtDestination,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,Flo1,Flo2,Flo3,Flo4,Flo5,Flo6,Flo7,Flo8,Flo9,Flo10,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,add_by,add_date,add_branch,update_by,update_date,update_branch,Status, IsTransportCharged,IsTransportPaid,IsSendOrPickup)" & _
                          "       VALUES(@TransportManifestItem_Index,@TransportManifest_Index,@Process_Id,@SalesOrder_Index,@Withdraw_Index,@Order_Index,@Invoice_No,@DO_No,@Qty_Shipped,@Weight_Shipped,@Volume_Shipped,@Pallet_Shipped,@Value_Shipped,@Qty_Delivered,@Weight_Delivered,@Volume_Delivered,@Pallet_Delivered,@Value_Delivered,@Qty_Returned,@Weight_Returned,@Volume_Returned,@Pallet_Returned,@Value_Returned,@Qty_Lost,@Weight_Lost,@Volume_Lost,@Pallet_Lost,@Value_Lost,@Qty_Damaged,@Weight_Damaged,@Volume_Damaged,@Pallet_Damaged,@Value_Damaged,@Qty_NeedReShipped,@Weight_NeedReShipped,@Volume_NeedReShipped,@Pallet_NeedReShipped,@Value_NeedReShipped,@JobProblem_Index,@JobProblem_Desc,@ResponsibleParty_Index,@JobSolution_Index,@JobSolution_Desc,@Comment,@Customer_Index,@Customer_Receive_Location_Index,@Customer_Shipping_Index,@Customer_Shipping_Location_Index,@Department_Index,@Appointment_Date,@Appointment_Time,@DocumentReturn_Method_Index,@Time_ExpectedDocPickup,@Time_ExpectedDeliveryToDestination,@Time_ExpectedReturnPickup,@Time_ExpectedDocReturnToOwner,@Time_DocIssued,@Time_DocPickup,@Time_DocTripConfirmed,@Time_DeliveryToDestination,@Time_ReturnPickup,@Time_DocReturnedToDC,@Time_DocReturnedToSource,@Time_DocReturnedToOwner,@Time_DocConfirmedByOwner,@Time_NextDeliveryToDestination,@Time_NextReturnPickup,@Mile_AtDestination,@Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@Flo6,@Flo7,@Flo8,@Flo9,@Flo10,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@add_by,getdate(),@add_branch,@update_by,getdate(),@update_branch,1,@IsTransportCharged,@IsTransportPaid,@IsSendOrPickup)"

                End If


                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifestItem_Index
                    .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifest_Index
                    .Add("@Process_Id", SqlDbType.Int, 10).Value = _objItem.Process_Id
                    .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _objItem.SalesOrder_Index
                    .Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objItem.Withdraw_Index
                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objItem.Order_Index
                    .Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objItem.Invoice_No
                    .Add("@DO_No", SqlDbType.VarChar, 50).Value = _objItem.DO_No
                    .Add("@Qty_Shipped", SqlDbType.Float, 15).Value = _objItem.Qty_Shipped
                    .Add("@Weight_Shipped", SqlDbType.Float, 15).Value = _objItem.Weight_Shipped
                    .Add("@Volume_Shipped", SqlDbType.Float, 15).Value = _objItem.Volume_Shipped
                    .Add("@Pallet_Shipped", SqlDbType.Float, 15).Value = _objItem.Pallet_Shipped
                    .Add("@Value_Shipped", SqlDbType.Float, 15).Value = _objItem.Value_Shipped
                    .Add("@Qty_Delivered", SqlDbType.Float, 15).Value = _objItem.Qty_Delivered
                    .Add("@Weight_Delivered", SqlDbType.Float, 15).Value = _objItem.Weight_Delivered
                    .Add("@Volume_Delivered", SqlDbType.Float, 15).Value = _objItem.Volume_Delivered
                    .Add("@Pallet_Delivered", SqlDbType.Float, 15).Value = _objItem.Pallet_Delivered
                    .Add("@Value_Delivered", SqlDbType.Float, 15).Value = _objItem.Value_Delivered
                    .Add("@Qty_Returned", SqlDbType.Float, 15).Value = _objItem.Qty_Returned
                    .Add("@Weight_Returned", SqlDbType.Float, 15).Value = _objItem.Weight_Returned
                    .Add("@Volume_Returned", SqlDbType.Float, 15).Value = _objItem.Volume_Returned
                    .Add("@Pallet_Returned", SqlDbType.Float, 15).Value = _objItem.Pallet_Returned
                    .Add("@Value_Returned", SqlDbType.Float, 15).Value = _objItem.Value_Returned
                    .Add("@Qty_Lost", SqlDbType.Float, 15).Value = _objItem.Qty_Lost
                    .Add("@Weight_Lost", SqlDbType.Float, 15).Value = _objItem.Weight_Lost
                    .Add("@Volume_Lost", SqlDbType.Float, 15).Value = _objItem.Volume_Lost
                    .Add("@Pallet_Lost", SqlDbType.Float, 15).Value = _objItem.Pallet_Lost
                    .Add("@Value_Lost", SqlDbType.Float, 15).Value = _objItem.Value_Lost
                    .Add("@Qty_Damaged", SqlDbType.Float, 15).Value = _objItem.Qty_Damaged
                    .Add("@Weight_Damaged", SqlDbType.Float, 15).Value = _objItem.Weight_Damaged
                    .Add("@Volume_Damaged", SqlDbType.Float, 15).Value = _objItem.Volume_Damaged
                    .Add("@Pallet_Damaged", SqlDbType.Float, 15).Value = _objItem.Pallet_Damaged
                    .Add("@Value_Damaged", SqlDbType.Float, 15).Value = _objItem.Value_Damaged
                    .Add("@Qty_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Qty_NeedReShipped
                    .Add("@Weight_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Weight_NeedReShipped
                    .Add("@Volume_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Volume_NeedReShipped
                    .Add("@Pallet_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Pallet_NeedReShipped
                    .Add("@Value_NeedReShipped", SqlDbType.Float, 15).Value = _objItem.Value_NeedReShipped
                    .Add("@JobProblem_Index", SqlDbType.VarChar, 13).Value = _objItem.JobProblem_Index
                    .Add("@JobProblem_Desc", SqlDbType.VarChar, 500).Value = _objItem.JobProblem_Desc
                    .Add("@ResponsibleParty_Index", SqlDbType.VarChar, 13).Value = _objItem.ResponsibleParty_Index
                    .Add("@JobSolution_Index", SqlDbType.VarChar, 13).Value = _objItem.JobSolution_Index
                    .Add("@JobSolution_Desc", SqlDbType.VarChar, 500).Value = _objItem.JobSolution_Desc
                    .Add("@Comment", SqlDbType.VarChar, 500).Value = _objItem.Comment
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Index
                    .Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Receive_Location_Index
                    .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Shipping_Index
                    .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = _objItem.Customer_Shipping_Location_Index
                    .Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objItem.Department_Index
                    .Add("@Appointment_Date", SqlDbType.DateTime, 23).Value = _objItem.Appointment_Date
                    .Add("@Appointment_Time", SqlDbType.VarChar, 100).Value = _objItem.Appointment_Time
                    .Add("@DocumentReturn_Method_Index", SqlDbType.VarChar, 50).Value = _objItem.DocumentReturn_Method_Index

                    If _objItem.Time_ExpectedDocPickup = Nothing Then
                        .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedDocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocPickup
                    End If
                    If _objItem.Time_ExpectedDeliveryToDestination = Nothing Then
                        .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDeliveryToDestination
                    End If
                    If _objItem.Time_ExpectedReturnPickup = Nothing Then
                        .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedReturnPickup
                    End If
                    If _objItem.Time_ExpectedDocReturnToOwner = Nothing Then
                        .Add("@Time_ExpectedDocReturnToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ExpectedDocReturnToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_ExpectedDocReturnToOwner
                    End If
                    If _objItem.Time_DocIssued = Nothing Then
                        .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocIssued", SqlDbType.DateTime, 23).Value = _objItem.Time_DocIssued
                    End If
                    If _objItem.Time_DocPickup = Nothing Then
                        .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_DocPickup
                    End If
                    If _objItem.Time_DocTripConfirmed = Nothing Then
                        .Add("@Time_DocTripConfirmed", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocTripConfirmed", SqlDbType.DateTime, 23).Value = _objItem.Time_DocTripConfirmed
                    End If
                    If _objItem.Time_DeliveryToDestination = Nothing Then
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    End If
                    If _objItem.Time_ReturnPickup = Nothing Then
                        .Add("@Time_ReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_ReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_ReturnPickup
                    End If
                    If _objItem.Time_DocReturnedToDC = Nothing Then
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    End If

                    If _objItem.Time_DocReturnedToSource = Nothing Then
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    End If
                    If _objItem.Time_DocReturnedToOwner = Nothing Then
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    End If
                    If _objItem.Time_DocConfirmedByOwner = Nothing Then
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    End If
                    If _objItem.Time_NextDeliveryToDestination = Nothing Then
                        .Add("@Time_NextDeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_NextDeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_NextDeliveryToDestination
                    End If
                    If _objItem.Time_NextReturnPickup = Nothing Then
                        .Add("@Time_NextReturnPickup", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_NextReturnPickup", SqlDbType.DateTime, 23).Value = _objItem.Time_NextReturnPickup
                    End If

                    .Add("@Mile_AtDestination", SqlDbType.Int, 10).Value = _objItem.Mile_AtDestination
                    .Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objItem.Ref_No1
                    .Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objItem.Ref_No2
                    .Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objItem.Ref_No3
                    .Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objItem.Ref_No4
                    .Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objItem.Ref_No5
                    .Add("@Flo1", SqlDbType.Float, 15).Value = _objItem.Flo1
                    .Add("@Flo2", SqlDbType.Float, 15).Value = _objItem.Flo2
                    .Add("@Flo3", SqlDbType.Float, 15).Value = _objItem.Flo3
                    .Add("@Flo4", SqlDbType.Float, 15).Value = _objItem.Flo4
                    .Add("@Flo5", SqlDbType.Float, 15).Value = _objItem.Flo5
                    .Add("@Flo6", SqlDbType.Float, 15).Value = _objItem.Flo6
                    .Add("@Flo7", SqlDbType.Float, 15).Value = _objItem.Flo7
                    .Add("@Flo8", SqlDbType.Float, 15).Value = _objItem.Flo8
                    .Add("@Flo9", SqlDbType.Float, 15).Value = _objItem.Flo9
                    .Add("@Flo10", SqlDbType.Float, 15).Value = _objItem.Flo10
                    .Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Add("@Str2", SqlDbType.NVarChar, 100).Value = _objItem.Str2
                    .Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Add("@Str9", SqlDbType.NVarChar, 100).Value = _objItem.Str9
                    .Add("@Str10", SqlDbType.NVarChar, 100).Value = _objItem.Str10
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    '.Add("@add_date", SqlDbType.smalldatetime, 16).Value = _objItem.add_date
                    .Add("@add_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    '.Add("@update_date", SqlDbType.smalldatetime, 16).Value = _objItem.update_date
                    .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    '.Add("@cancel_by", SqlDbType.varchar, 50).Value = _objItem.cancel_by
                    '.Add("@cancel_date", SqlDbType.varchar, 50).Value = _objItem.cancel_date
                    '.Add("@cancel_branch", SqlDbType.int, 10).Value = _objItem.cancel_branch
                    '.Add("@Status", SqlDbType.Int, 10).Value = _objItem.Status
                    .Add("@IsTransportPaid", SqlDbType.Bit, 1).Value = _objItem.IsTransportPaid
                    .Add("@IsTransportCharged", SqlDbType.Bit, 1).Value = _objItem.IsTransportCharged
                    .Add("@IsSendOrPickup", SqlDbType.Int, 4).Value = _objItem.IsSendOrPickup
                    .Add("@IsPacking", SqlDbType.Int, 4).Value = _objItem.IsPacking
                    SetSQLString = strSQL
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()
                End With


                'dong add new 2013-09-24
                strSQL = " UPDATE   tb_SalesOrder   "
                strSQL &= " SET     TransportManifest_No = @TransportManifest_No"
                strSQL &= " WHERE   SalesOrder_Index = @SalesOrder_Index"
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@TransportManifest_No", SqlDbType.VarChar, 50).Value = _objHeader.TransportManifest_No
                    .Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = _objItem.SalesOrder_Index
                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                ' 2017-05-03
                strSQL = " UPDATE   tb_SalesOrder   "
                strSQL &= " SET     DistributionCenter_Index = @DistributionCenter_Index"
                strSQL &= " WHERE   SalesOrder_Index = @SalesOrder_Index"
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@DistributionCenter_Index", SqlDbType.VarChar, 13).Value = _objHeader.DistributionCenter_Index
                    .Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = _objItem.SalesOrder_Index
                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next


            'Dong add new : 2012/09/24
            SetSQLString = " DELETE svar_TransportManifestCharge WHERE   TransportManifest_Index = @TransportManifest_Index"
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = Me._objHeader.TransportManifest_Index
            End With
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            For Each objChargeItem As svar_TransportManifestCharge In _objItemTransportManifestCharge
                strSQL = " INSERT INTO svar_TransportManifestCharge( " _
                            & "TransportManifest_Index" _
                            & ",Customer_Index" _
                            & ",TransportRegion_Index" _
                            & ",Carrier_Index" _
                            & ",Description" _
                            & ",sumDrop" _
                            & ",Total_Qty" _
                            & ",Weight" _
                            & ",Volume" _
                            & ",Amount" _
                            & ",Amount_Charge" _
                            & ",TransportCharge_Type,Minimum_Rate)" _
                            & " VALUES(" _
                            & "@TransportManifest_Index" _
                            & ",@Customer_Index" _
                            & ",@TransportRegion_Index" _
                            & ",@Carrier_Index" _
                            & ",@Description" _
                            & ",@sumDrop" _
                            & ",@Total_Qty" _
                            & ",@Weight" _
                            & ",@Volume" _
                            & ",@Amount" _
                            & ",@Amount_Charge" _
                            & ",@TransportCharge_Type,@Minimum_Rate)"

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = objChargeItem.TransportManifest_Index
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = objChargeItem.Customer_Index
                    .Add("@TransportRegion_Index", SqlDbType.VarChar, 13).Value = objChargeItem.TransportRegion_Index
                    .Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = objChargeItem.Carrier_Index
                    .Add("@Description", SqlDbType.VarChar, 255).Value = objChargeItem.Description
                    .Add("@sumDrop", SqlDbType.Float).Value = objChargeItem.sumDrop
                    .Add("@Total_Qty", SqlDbType.Float).Value = objChargeItem.Total_Qty
                    .Add("@Weight", SqlDbType.Float).Value = objChargeItem.Weight
                    .Add("@Volume", SqlDbType.Float).Value = objChargeItem.Volume
                    .Add("@Amount", SqlDbType.Float).Value = objChargeItem.Amount
                    .Add("@Amount_Charge", SqlDbType.Float).Value = objChargeItem.Amount_Charge
                    .Add("@TransportCharge_Type", SqlDbType.Int).Value = objChargeItem.TransportCharge_Type
                    .Add("@Minimum_Rate", SqlDbType.Float).Value = objChargeItem.Minimum_Rate
                    SetSQLString = strSQL
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()
                End With
            Next



            '*** Commit transaction
            myTrans.Commit()
            Return _objHeader.TransportManifest_Index

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex

        Finally
            disconnectDB()
        End Try


    End Function

#End Region

#Region "DELETE"

    Public Function DeleteItem_CreateNewTMS(ByVal pstrTransportManifest_Index As String, ByVal pstrTransportManifestItem_Index As String) As String
        Dim sbSql As New System.Text.StringBuilder
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            sbSql.Append(" DELETE tb_TransportManifestItem")
            sbSql.Append(" WHERE TransportManifestItem_Index='" & pstrTransportManifestItem_Index & "'")
            sbSql.Append("      and TransportManifest_Index='" & pstrTransportManifest_Index & "'")
            SetSQLString = sbSql.ToString
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()
            Return "OK"


        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        End Try

    End Function

    Public Function Delete(ByVal penuDelete As enuDelete) As String
        Dim sbSql As New System.Text.StringBuilder
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            '1 = Delete All , 2 = Delete Item
            Select Case penuDelete
                Case enuDelete.DELETEITEM
                    sbSql.Remove(0, sbSql.Length())
                    sbSql.Append(" UPDATE tb_SalesOrder ")
                    sbSql.Append(" SET Status_Manifest = 1 ,TransportManifest_No = '' ")
                    sbSql.Append(" WHERE SalesOrder_Index in (select SalesOrder_Index from tb_TransportManifestItem where TransportManifestItem_Index='" & _TransportManifestItem_Index & "')")

                    DBExeNonQuery(sbSql.ToString, Connection, myTrans)


                    DBExeNonQuery(String.Format(" sp_delete_TransportManifestItem '{0}','{1}'", _TransportManifestItem_Index, WV_UserName), Connection, myTrans)

                    DBExeNonQuery(String.Format(" sp_delete_SalesOrderTrip '{0}','{1}'", _TransportManifestItem_Index, WV_UserName), Connection, myTrans)

                    'sbSql.Append(" DELETE tb_TransportManifestItem")
                    'sbSql.Append(" WHERE TransportManifestItem_Index='" & _TransportManifestItem_Index & "'")
                    'SetSQLString = sbSql.ToString
                    'SetCommandType = enuCommandType.Text
                    'SetEXEC_TYPE = EXEC.NonQuery
                    'EXEC_Command()


                    ' If isChkManifestItem(_TransportManifest_Index) = False Then
                    'sbSql.Remove(0, sbSql.Length())
                    'sbSql.Append(" UPDATE x_TransportManifest_Driver")
                    'sbSql.Append(" SET Status=-1 ")
                    'sbSql.Append(" WHERE TransportManifest_Index='" & _TransportManifest_Index & "'")

                    'sbSql.Append(" UPDATE tb_TransportManifest")
                    'sbSql.Append(" set status =-1")
                    'sbSql.Append(" WHERE TransportManifest_Index='" & _TransportManifest_Index & "'")
                    'SetSQLString = sbSql.ToString
                    'SetCommandType = enuCommandType.Text
                    'SetEXEC_TYPE = EXEC.NonQuery
                    'EXEC_Command()
                    ' End If
                    myTrans.Commit()
                    Return "OK"

                Case enuDelete.DELETEALL

                    sbSql.Remove(0, sbSql.Length())
                    sbSql.Append(" UPDATE tb_SalesOrder ")
                    sbSql.Append(" SET Status_Manifest = 1 ,TransportManifest_No = '' ")
                    sbSql.Append(" WHERE SalesOrder_Index in (select SalesOrder_Index from tb_TransportManifestItem where TransportManifest_Index='" & _TransportManifest_Index & "')")

                    sbSql.Append(" UPDATE x_TransportManifest_Driver")
                    sbSql.Append(" SET Status=-1 ")
                    sbSql.Append(" WHERE TransportManifest_Index='" & _TransportManifest_Index & "'")

                    sbSql.Append(" UPDATE tb_TransportManifestItem")
                    sbSql.Append(" SET Status=-1 ")
                    sbSql.Append(String.Format(" ,cancel_by='{0}' ", W_Module.WV_UserName))
                    sbSql.Append(" ,cancel_date=getdate() ")
                    sbSql.Append(String.Format(" ,cancel_branch='{0}' ", W_Module.WV_Branch_ID))
                    sbSql.Append(" WHERE TransportManifest_Index='" & _TransportManifest_Index & "'")

                    sbSql.Append(" UPDATE tb_TransportManifest ")
                    sbSql.Append(" SET Status=-1 ")
                    sbSql.Append(String.Format(" ,cancel_by='{0}' ", W_Module.WV_UserName))
                    sbSql.Append(" ,cancel_date=getdate() ")
                    sbSql.Append(String.Format(" ,cancel_branch='{0}' ", W_Module.WV_Branch_ID))
                    sbSql.Append(" WHERE TransportManifest_Index='" & _TransportManifest_Index & "'")

                    SetSQLString = sbSql.ToString
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()

                    myTrans.Commit()
                    Return "OK"


            End Select


            Return ""

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        End Try

    End Function


#End Region

#Region " Check isExist "
    Private Function isExistID(ByVal pTransportManifestItem_Index As String, ByVal pSalesOrder_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_TransportManifestItem "
            strSQL &= "  where SalesOrder_Index = @SalesOrder_Index   "
            strSQL &= "  and  TransportManifestItem_Index = @TransportManifestItem_Index   "
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = pSalesOrder_Index
            SQLServerCommand.Parameters.Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = pTransportManifestItem_Index
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                '' New Insert Item Manifest
                'strSQL = "    UPDATE tb_SalesOrder SET Status=8 "
                'strSQL &= "   WHERE          SalesOrder_Index = '" & pSalesOrder_Index & "'"
                'SetSQLString = strSQL
                'SetCommandType = enuCommandType.Text
                'SetEXEC_TYPE = EXEC.NonQuery
                'EXEC_Command()

                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
            '  Finally
            '      disconnectDB()
        End Try
    End Function

    Private Function isChkManifestItem(ByVal TransportManifest_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_TransportManifestItem where TransportManifest_Index = @TransportManifest_Index  AND Status <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = TransportManifest_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
            '  Finally
            '      disconnectDB()
        End Try
    End Function


    Public Function isChkManifestItem_No(ByVal TransportManifest_No As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_TransportManifest where TransportManifest_No = @TransportManifest_No  AND Status <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@TransportManifest_No", SqlDbType.VarChar, 13).Value = TransportManifest_No

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



    Private Function SelectMax_Trip_No(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal ColumnName As String, ByVal TableName As String, ByVal strSalesOrder_Index As String)

        Dim strSQL As String

        '    Dim objDr As SqlClient.SqlDataReader = Nothing


        Dim iDocumentValue As Integer = 1
        '   Dim NewDocumentValue As String
        Try

            'strSQL = " SELECT     Max(" & Trim(ColumnName).ToString & ")  as MaxDocumentNumber    " & _
            '         " FROM       " & TableName & "  Where SalesOrder_Index = '" & strSalesOrder_Index & "' "

            strSQL = " SELECT     isnull(count(*),0)  as MaxDocumentNumber "
            strSQL &= "                     FROM        tb_SalesOrderTrip INNER JOIN"
            strSQL &= "                                 tb_TransportManifestItem ON tb_SalesOrderTrip.TransportManifestItem_Index = tb_TransportManifestItem.TransportManifestItem_Index INNER JOIN"
            strSQL &= "                                 tb_TransportManifest ON tb_SalesOrderTrip.TransportManifest_Index = tb_TransportManifest.TransportManifest_Index"
            strSQL &= "                     WHERE     (tb_TransportManifest.Status <> - 1) and tb_TransportManifestItem.Status <> -1"
            strSQL &= "                      AND (tb_SalesOrderTrip.SalesOrder_Index = '" & strSalesOrder_Index & "' )"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans


            DataAdapter.Fill(DS, "Max")

            If DS.Tables("Max").Rows.Count > 0 Then
                Dim objDr As DataRow = DS.Tables("Max").Rows(0)

                iDocumentValue = Val(objDr("MaxDocumentNumber").ToString) + 1

                DS.Tables("Max").Clear()
                Return iDocumentValue
            Else
                DS.Tables("Max").Clear()
                Return iDocumentValue
            End If


        Catch ex As Exception
            Throw ex

        End Try
    End Function
#End Region


    Public Function UPDATE_RESERVLOADING(ByVal objStatus As enuOperation_Type, ByVal SalesOrderItem_Index As String, ByVal Qty_Reserv As Double, ByVal Weight_Reserv As Double, ByVal Volume_Reserv As Double, ByVal Price_Reserv As Double) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True
        Try

            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    strSQL &= " Update tb_SalesOrderItem "
                    strSQL &= " SET LoadTotal_Qty = LoadTotal_Qty + " & Qty_Reserv
                    strSQL &= " ,LoadWeight = LoadWeight + " & Weight_Reserv
                    strSQL &= " ,LoadVolume = LoadVolume + " & Volume_Reserv
                    strSQL &= " ,LoadAmount = LoadAmount + " & Price_Reserv
                    strSQL &= " where SalesOrderItem_Index='" & SalesOrderItem_Index & "'"
                Case enuOperation_Type.DELETE
                    strSQL &= " Update tb_SalesOrderItem "
                    strSQL &= " SET LoadTotal_Qty = LoadTotal_Qty - " & Qty_Reserv
                    strSQL &= " ,LoadWeight = LoadWeight - " & Weight_Reserv
                    strSQL &= " ,LoadVolume = LoadVolume - " & Volume_Reserv
                    strSQL &= " ,LoadAmount = LoadAmount - " & Price_Reserv
                    strSQL &= " where SalesOrderItem_Index='" & SalesOrderItem_Index & "'"
            End Select


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return True
        Catch ex As Exception
            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try
    End Function

#Region "ImportExcelServiceToTM"
    Public Function InsertDataEXCEL(ByVal DtData As DataTable) As String
        Dim strSQL As String = ""
        Dim obj As New config_CustomSetting
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            For iRow As Integer = 0 To DtData.Rows.Count - 1

                strSQL = " update tb_TransportManifest "
                strSQL &= " set TotalTransportPaid = TotalTransportPaid + @Service "
                strSQL &= " ,update_by = @Update_By "
                strSQL &= " ,update_date = getdate()"
                strSQL &= " where TransportManifest_Index in ( "
                strSQL &= " select TM.TransportManifest_Index from tb_TransportManifest TM "
                strSQL &= " inner join tb_TransportManifestItem TMI ON TM.TransportManifest_Index = TMI.TransportManifest_Index "
                strSQL &= " inner join tb_SalesOrder SO on TMI.SalesOrder_Index = SO.SalesOrder_Index "
                strSQL &= " where SO.Str1 = @Invioce ) "
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Service", SqlDbType.Float).Value = CDbl(DtData.Rows(iRow)("Service").ToString)
                    .Parameters.Add("@Invioce", SqlDbType.VarChar, 50).Value = DtData.Rows(iRow)("INV").ToString
                    .Parameters.Add("@Update_By", SqlDbType.VarChar, 50).Value = WV_UserFullName
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                strSQL = " update tb_TransportManifest "
                strSQL &= " set TotalTransportCharged = TotalTransportPaid * " & obj.getConfig_Key_DEFUALT("DEFAULT_PERCENT_SERVICE_TM")
                strSQL &= " where TransportManifest_Index in ( "
                strSQL &= " select TM.TransportManifest_Index from tb_TransportManifest TM "
                strSQL &= " inner join tb_TransportManifestItem TMI ON TM.TransportManifest_Index = TMI.TransportManifest_Index "
                strSQL &= " inner join tb_SalesOrder SO on TMI.SalesOrder_Index = SO.SalesOrder_Index "
                strSQL &= " where SO.Str1 = @Invioce ) "
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Invioce", SqlDbType.VarChar, 50).Value = DtData.Rows(iRow)("INV").ToString
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next

            strSQL = "INSERT INTO sy_Log_Import_TMS_Service"
            strSQL &= " (FileName"
            strSQL &= " ,User_Add"
            strSQL &= " ,Import_Date"
            strSQL &= "  ,Import_IP,Str1)"
            strSQL &= "    VALUES "
            strSQL &= "  (@FileName "
            strSQL &= "  ,@User_Add"
            strSQL &= "  ,getdate()"
            strSQL &= "  ,@Import_IP,@Str1)"
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@FileName", SqlDbType.VarChar, 100).Value = DtData.Rows(0)("Filename").ToString
                .Parameters.Add("@User_Add", SqlDbType.VarChar, 50).Value = WV_UserFullName
                .Parameters.Add("@Import_IP", SqlDbType.VarChar, 50).Value = WV_Host_Ip
                .Parameters.Add("@Str1", SqlDbType.VarChar, 50).Value = "RFE"
            End With
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery

            EXEC_Command()
            myTrans.Commit()
            Return "PASS"
        Catch ex As Exception
            myTrans.Rollback()
            Return ""
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function InsertDataEXCELKerry(ByVal DtData As DataTable, ByVal DtTM As DataTable, ByVal FileName As String) As String
        Dim strSQL As String = ""
        Dim obj As New config_CustomSetting
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            For iRow As Integer = 0 To DtData.Rows.Count - 1
                strSQL = " update tb_SalesOrder "
                strSQL &= " set TotalTransportPaid = TotalTransportPaid + @Service "
                strSQL &= " where str1 = @INV "
                ' strSQL &= " And DocumentType_Index in ( Select DocumentType_Index from ms_DocumentType where DocumentType_Id = @DocumentType_Id and process_Id = 10)  "
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Service", SqlDbType.Float).Value = CDbl(DtData.Rows(iRow)("total_rated_amount").ToString)
                    .Parameters.Add("@INV", SqlDbType.VarChar, 50).Value = DtData.Rows(iRow)("ref_no").ToString
                    '   .Parameters.Add("@DocumentType_Id", SqlDbType.VarChar, 250).Value = DtData.Rows(iRow)("Type").ToString
                    .Parameters.Add("@Update_By", SqlDbType.VarChar, 50).Value = WV_UserFullName
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next

            For iRow As Integer = 0 To DtTM.Rows.Count - 1
                strSQL = " update tb_TransportManifest"
                strSQL &= " set TotalTransportPaid = (select SUM(SO.TotalTransportPaid) as Total from tb_TransportManifest TM "
                strSQL &= "				 Inner Join tb_TransportManifestItem TMI ON TM.TransportManifest_Index = TMI.TransportManifest_Index "
                strSQL &= "		 Inner Join tb_SalesOrder SO ON TMI.SalesOrder_Index = SO.SalesOrder_Index "
                strSQL &= " where TM.TransportManifest_Index = @TransportManifest_Index) "
                strSQL &= " ,update_by = @update_by "
                strSQL &= " where TransportManifest_Index = @TransportManifest_Index "
                ' strSQL &= " And SO.DocumentType_Index in ( Select DocumentType_Index from ms_DocumentType where DocumentType_Id = @DocumentType_Id and process_Id = 10)  "

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = DtTM.Rows(iRow)("TransportManifest_Index").ToString
                    ' .Parameters.Add("@DocumentType_Id", SqlDbType.VarChar, 250).Value = DtData.Rows(iRow)("Type").ToString
                    .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserFullName
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                strSQL = " update tb_TransportManifest "
                strSQL &= " set TotalTransportCharged = TotalTransportPaid * " & obj.getConfig_Key_DEFUALT("DEFAULT_PERCENT_SERVICE_TM")
                strSQL &= " where TransportManifest_Index = @TransportManifest_Index"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = DtTM.Rows(iRow)("TransportManifest_Index").ToString
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


            Next


            strSQL = "INSERT INTO sy_Log_Import_TMS_Service"
            strSQL &= " (FileName"
            strSQL &= " ,User_Add"
            strSQL &= " ,Import_Date"
            strSQL &= "  ,Import_IP,Str1)"
            strSQL &= "    VALUES "
            strSQL &= "  (@FileName "
            strSQL &= "  ,@User_Add"
            strSQL &= "  ,getdate()"
            strSQL &= "  ,@Import_IP,@Str1)"
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@FileName", SqlDbType.VarChar, 100).Value = FileName
                .Parameters.Add("@User_Add", SqlDbType.VarChar, 50).Value = WV_UserFullName
                .Parameters.Add("@Import_IP", SqlDbType.VarChar, 50).Value = WV_Host_Ip
                .Parameters.Add("@Str1", SqlDbType.VarChar, 50).Value = "Kerry"
            End With
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()
            Return "PASS"
        Catch ex As Exception
            myTrans.Rollback()
            Return ""
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region "  GET IsSend_Recieve "
    Public Function GetIsSendOrRecieve_Value(ByVal Process_Id As Integer, ByVal DocumentType_Id As String) As Integer
        Try
            Dim strSQL As String = ""
            strSQL &= "  SELECT isnull(IsSend_OR_Recieve,1) as IsSend_OR_Recieve"
            strSQL &= "  FROM ms_DocumentType "
            strSQL &= "  WHERE DocumentType_Id = '" & DocumentType_Id & "'"
            strSQL &= "  AND Process_Id = " & Process_Id

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return CInt(_dataTable.Rows(0)("IsSend_OR_Recieve"))
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region


End Class
