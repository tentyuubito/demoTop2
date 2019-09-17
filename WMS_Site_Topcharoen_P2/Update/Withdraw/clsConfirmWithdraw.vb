Imports WMS_STD_Formula
Imports System.Data

Public Class clsConfirmWithdraw : Inherits DBType_SQLServer

    Public Function calculateWeightScale(ByVal Withdraw_Index As String, ByVal _Connection As SqlClient.SqlConnection, ByVal _myTrans As SqlClient.SqlTransaction) As Boolean
        Dim _strQuery As System.Text.StringBuilder
        Try
           
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
