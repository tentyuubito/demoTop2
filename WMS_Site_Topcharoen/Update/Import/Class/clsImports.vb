Imports System
Imports System.Data
Imports WMS_STD_Formula
Public Class clsImports : Inherits DBType_SQLServer

    Public Function updateCustomer_Shipping_Location(ByVal Customer_Shipping_Location_Index As String, ByVal Customer_Shipping_Location_Name As String, ByVal Customer_Shipping_Location_Address As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = " update ms_Customer_Shipping_Location set "
            strSQL &= " Shipping_Location_Name=@Shipping_Location_Name, "
            strSQL &= " Address=@Address, "
            strSQL &= " update_by=@update_by, "
            strSQL &= " update_date=getdate(), "
            strSQL &= " update_branch=@update_branch "
            strSQL &= " where Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index "
            strSQL &= " and status_id<>-1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = Customer_Shipping_Location_Index
                .Add("@Shipping_Location_Name", SqlDbType.NVarChar, 400).Value = Customer_Shipping_Location_Name
                .Add("@Address", SqlDbType.NVarChar, 510).Value = Customer_Shipping_Location_Address
                .Add("@update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

End Class
