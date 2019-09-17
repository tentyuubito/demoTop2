Imports WMS_STD_Formula
Public Class ms_RouteTCP : Inherits DBType_SQLServer

#Region " SELECT DATA "

    Public Function getUrgentALL() As DataTable
        Try

            Dim strSQL As String = ""

            strSQL = "  "

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

#End Region

End Class