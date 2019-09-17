Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master

Public Class clsPO_Popup : Inherits DBType_SQLServer

    Public Function getPO_PopupList(ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= String.Format(" select * from View_PurchaseOrder_Popup {0} ", strWhere)
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
