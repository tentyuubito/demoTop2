Imports WMS_STD_Formula

Public Class clsMasterPriority : Inherits DBType_SQLServer

    Public Function getPriority() As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " SELECT *,Priority_ID + ' : ' + Priority_Name as display FROM Config_Priority"

            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
        End Try
    End Function



End Class
