Imports WMS_STD_Formula
Public Class ms_urgent : Inherits DBType_SQLServer

#Region " SELECT DATA "

    Public Function getUrgentALL() As DataTable
        Try

            Dim strSQL As String = ""

            strSQL = "SELECT * FROM Config_Priority "

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

#End Region

End Class