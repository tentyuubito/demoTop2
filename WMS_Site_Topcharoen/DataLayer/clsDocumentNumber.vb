Imports WMS_STD_Formula

Public Class clsDocumentNumber : Inherits DBType_SQLServer

    Public Function Auto_DocumentType_Number(ByVal pstrDocumentType_index As String, ByVal pstrWhere As String, ByVal pdateDocument_Date As Date) As String
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strDocument_No = New Sy_AutoyyyyMM().Auto_DocumentType_Number(Connection, myTrans, pstrDocumentType_index, pdateDocument_Date, pstrWhere)
            myTrans.Commit()
            Return strDocument_No
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

End Class
