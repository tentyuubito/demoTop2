Imports WMS_STD_Formula

Public Class cls_SP_Report : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
#Region "PROPERTY"
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
#End Region
#Region "SELECT"

    Public Function getConditionSP_Report(ByVal pOperationType As Integer) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL = " select *  "
            strSQL &= " from tb_condition "
            strSQL &= " where operation_type = " & pOperationType & " AND status  <> -1 "

            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    
    Public Function getReport()
        Dim strSQL As String = ""
        Try
            strSQL = " select *  "
            strSQL &= " from tb_sp_report "
            strSQL &= " where SP_Report_Status_ID  <> -1 "
            strSQL &= " Order By SP_Report_Seq "
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getView_SP_Report(ByVal Report_index As String)
        Dim strSQL As String = ""
        Try
            strSQL = " select 0 as chk_select , *  "
            strSQL &= " from View_SP_Report "
            strSQL &= " where SP_Report_Condition_Status_ID  <> -1 AND SP_Report_Index = '" & Report_index & "' "
            strSQL &= " ORDER BY SP_Report_Condition_Seq "
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function getData_Report(ByVal Condition As String)
        Dim strSQL As String = ""
        Try
            strSQL = Condition
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

End Class
