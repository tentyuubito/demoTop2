'Imports WMS_STD_Formula
Public Class ML_TransportBarcode : Inherits DBType_SQLServer
#Region " SELECT DATA "

    Public Function getTransBarcode(ByVal txtSaleOrder_NoSeach As String) As DataTable
        Try

            Dim strSQL As String = ""

            strSQL &= " SELECT   tb_so.SalesOrder_Index from tb_WithdrawBarcode tb_wb "
            strSQL &= " inner join tb_withdrawItem tb_wi on tb_wi.Withdraw_Index = tb_wb.Withdraw_Index "
            strSQL &= " INNER JOIN tb_Salesorder tb_so on tb_so.SalesOrder_index = tb_wi.documentPlan_index "
            strSQL &= " Where tb_wb.Barcode_Bag = " & "'" & txtSaleOrder_NoSeach & "'"
            strSQL &= " group by  tb_so.SalesOrder_Index "
            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

#End Region

End Class
