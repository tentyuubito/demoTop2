Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_Formula
Public Class ml_TAG : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

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

    Public Sub getView_Tag_Header(ByVal pstrWhere As String, ByVal intTop As Integer)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "select Top " & intTop.ToString & " '' as Row,convert(bit,0) as chkSelect,* from VIEW_TAG_Header_Reprint_V2 where 1=1 "

            SetSQLString = strSQL & pstrWhere & " ORDER BY TAG_No"


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
