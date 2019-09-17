Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data 
Imports System.Data.SqlClient 
Public class tb_SalesOrderPackingItem : Inherits DBType_SQLServer 
#Region " Private variables "
  Private _dataTable As DataTable = New DataTable 
  Private _scalarOutput As String 
Private _SalesOrderPackingItem_Index as string
Private _SalesOrderPacking_Index as string
Private _SalesOrder_Index as string
Private _TransportManifestItem_Index as string
Private _Sku_Index as string
Private _Qty_Pack as Double
Private _Total_Qty as Double
Private _seq as string
Private _SalesOrderItem_Index as string

# end Region 
#Region " Properties "
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
Public Property SalesOrderPackingItem_Index() As string
Get
return _SalesOrderPackingItem_Index
end Get
Set(ByVal Value As string)
_SalesOrderPackingItem_Index = Value
end set
end Property 

Public Property SalesOrderPacking_Index() As string
Get
return _SalesOrderPacking_Index
end Get
Set(ByVal Value As string)
_SalesOrderPacking_Index = Value
end set
end Property 

Public Property SalesOrder_Index() As string
Get
return _SalesOrder_Index
end Get
Set(ByVal Value As string)
_SalesOrder_Index = Value
end set
end Property 

Public Property TransportManifestItem_Index() As string
Get
return _TransportManifestItem_Index
end Get
Set(ByVal Value As string)
_TransportManifestItem_Index = Value
end set
end Property 

Public Property Sku_Index() As string
Get
return _Sku_Index
end Get
Set(ByVal Value As string)
_Sku_Index = Value
end set
end Property 

Public Property Qty_Pack() As Double
Get
return _Qty_Pack
end Get
Set(ByVal Value As Double)
_Qty_Pack = Value
end set
end Property 

Public Property Total_Qty() As Double
Get
return _Total_Qty
end Get
Set(ByVal Value As Double)
_Total_Qty = Value
end set
end Property 

Public Property seq() As string
Get
return _seq
end Get
Set(ByVal Value As string)
_seq = Value
end set
end Property 

Public Property SalesOrderItem_Index() As string
Get
return _SalesOrderItem_Index
end Get
Set(ByVal Value As string)
_SalesOrderItem_Index = Value
end set
end Property 


# end Region 
#Region " Select Data "
 Public Function GetAllData(ByVal strWhere As String) As DataTable
   Dim strSQL As String = " 
    Try 
       strSQL = "  Select * "
        strSQL &= " from tb_SalesOrderPackingItem"
        strSQL &= " Where 1 = 1 "
        SetSQLString = strSQL & strwhere 
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

    Public Function GetAllDataSOP_CountPack(ByVal strWhere As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  select SUM(Pack) Pack,SUM(NoPAck) NoPAck"
            strSQL &= " FROM("

            strSQL &= " Select count(*) as Pack,0 NoPAck"
            strSQL &= " from VIEW_SalesOrderPacking "
            strSQL &= " Where 1 = 1 and status_print <> -1  "
            strSQL &= " and isnull(TransportManifestItem_Index,'') = ''" & strWhere

            strSQL &= " UNION"

            strSQL &= " Select 0 as Pack, count(*)  NoPAck"
            strSQL &= " from VIEW_SalesOrderPacking "
            strSQL &= " Where 1 = 1 and status_print <> -1  "
            strSQL &= " and isnull(TransportManifestItem_Index,'') <> ''" & strWhere

            strSQL &= " ) xxx"

            connectDB()
            SetSQLString = strSQL
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function GetAllDataSOP(ByVal strWhere As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  Select * "
            strSQL &= " from VIEW_SalesOrderPacking"
            strSQL &= " Where 1 = 1 and status_print <> -1 "
            SetSQLString = strSQL & strWhere & "  order by SalesOrderPacking_Index desc"
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
    Public Function GetIndexBySoNumber(ByVal strNumber As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  Select SalesOrder_Index "
            strSQL &= " from tb_salesorder"
            strSQL &= " Where  SalesOrder_No='" & strNumber & "'"
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
    Public Function GetAllDataSOPDetail(ByVal strWhere As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  Select * "
            strSQL &= " from VIEW_SalesOrderPackingItem"
            strSQL &= " Where 1 = 1 and status_print <> -1 "
            SetSQLString = strSQL & strWhere & " Order by Sku_id"
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


    Public Function GetAllDataSOP_Item(ByVal strWhere As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  Select * "
            strSQL &= " from VIEW_SalesOrderPackingItem"
            strSQL &= " Where 1 = 1 "
            SetSQLString = strSQL & strWhere
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
# end Region 
#Region " Insert Data "
Public Function InsertData() As Boolean 
    Dim strSQL As String = " 
    connectDB() 
    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction() 
    SQLServerCommand.Transaction = myTrans 
    Try  
        strSQL = " INSERT INTO tb_SalesOrderPackingItem
strSQL &= "  ("
 strSQL &= "SalesOrderPackingItem_Index"
 strSQL &= " ,SalesOrderPacking_Index"
 strSQL &= " ,SalesOrder_Index"
 strSQL &= " ,TransportManifestItem_Index"
 strSQL &= " ,Sku_Index"
 strSQL &= " ,Qty_Pack"
 strSQL &= " ,Total_Qty"
            'strSQL &= " ,seq"
 strSQL &= " ,SalesOrderItem_Index"
strSQL &= "  ) VALUES ( "
strSQL &= "@SalesOrderPackingItem_Index"
strSQL &= ",@SalesOrderPacking_Index"
strSQL &= ",@SalesOrder_Index"
strSQL &= ",@TransportManifestItem_Index"
strSQL &= ",@Sku_Index"
strSQL &= ",@Qty_Pack"
strSQL &= ",@Total_Qty"
            'strSQL &= ",@seq"
strSQL &= ",@SalesOrderItem_Index"
 strSQL &="  ) "

   With SQLServerCommand 
  .Parameters.Clear() 
 .Parameters.Add("@SalesOrderPackingItem_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrderPackingItem_Index
 .Parameters.Add("@SalesOrderPacking_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrderPacking_Index
 .Parameters.Add("@SalesOrder_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrder_Index
 .Parameters.Add("@TransportManifestItem_Index", SqlDbType.varchar,50 ).Value = Me.TransportManifestItem_Index
 .Parameters.Add("@Sku_Index", SqlDbType.varchar,13 ).Value = Me.Sku_Index
 .Parameters.Add("@Qty_Pack", SqlDbType.float,8 ).Value = Me.Qty_Pack
 .Parameters.Add("@Total_Qty", SqlDbType.float,8 ).Value = Me.Total_Qty
                '.Parameters.Add("@seq", SqlDbType.nchar,20 ).Value = Me.seq
 .Parameters.Add("@SalesOrderItem_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrderItem_Index
   End With  
     SetSQLString = strSQL 
     SetCommandType = DBType_SQLServer.enuCommandType.Text 
     SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery 
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
# end Region 
#Region " Update Data "
  Public Function UpdateData() As Boolean 
    Dim strSQL As String = " 
    connectDB() 
    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction() 
    SQLServerCommand.Transaction = myTrans 
    Try  
        strSQL = " Update tb_SalesOrderPackingItem Set "
 strSQL &= "SalesOrderPackingItem_Index= @SalesOrderPackingItem_Index"
 strSQL &= " ,SalesOrderPacking_Index= @SalesOrderPacking_Index"
 strSQL &= " ,SalesOrder_Index= @SalesOrder_Index"
 strSQL &= " ,TransportManifestItem_Index= @TransportManifestItem_Index"
 strSQL &= " ,Sku_Index= @Sku_Index"
 strSQL &= " ,Qty_Pack= @Qty_Pack"
 strSQL &= " ,Total_Qty= @Total_Qty"
 strSQL &= " ,seq= @seq"
 strSQL &= " ,SalesOrderItem_Index= @SalesOrderItem_Index"
   With SQLServerCommand 
  .Parameters.Clear() 
 .Parameters.Add("@SalesOrderPackingItem_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrderPackingItem_Index
 .Parameters.Add("@SalesOrderPacking_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrderPacking_Index
 .Parameters.Add("@SalesOrder_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrder_Index
 .Parameters.Add("@TransportManifestItem_Index", SqlDbType.varchar,50 ).Value = Me.TransportManifestItem_Index
 .Parameters.Add("@Sku_Index", SqlDbType.varchar,13 ).Value = Me.Sku_Index
 .Parameters.Add("@Qty_Pack", SqlDbType.float,8 ).Value = Me.Qty_Pack
 .Parameters.Add("@Total_Qty", SqlDbType.float,8 ).Value = Me.Total_Qty
 .Parameters.Add("@seq", SqlDbType.nchar,20 ).Value = Me.seq
 .Parameters.Add("@SalesOrderItem_Index", SqlDbType.varchar,13 ).Value = Me.SalesOrderItem_Index
   End With  
     SetSQLString = strSQL 
     SetCommandType = DBType_SQLServer.enuCommandType.Text 
     SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery 
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
# end Region 

end class