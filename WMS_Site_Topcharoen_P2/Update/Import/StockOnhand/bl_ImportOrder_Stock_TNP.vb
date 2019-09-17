Imports System.IO
Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Data.OleDb
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class bl_ImportOrder_Stock_TNP : Inherits DBType_SQLServer

    Public Sub Delete_OrderTransactionAuto(ByVal pstrOrder_Index As String)
        Dim strSQL As String = ""
        Dim odtTransferStatusLocation As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try


            strSQL = "DELETE FROM tb_Order  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            strSQL &= "DELETE FROM tb_OrderItem  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            strSQL &= "DELETE FROM tb_OrderItemLocation  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            strSQL &= "DELETE FROM tb_Tag  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            strSQL &= "DELETE FROM tb_LocationBalance  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            strSQL &= "DELETE FROM tb_Transaction  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()

        Catch e As Exception
            Try
                myTrans.Rollback()

                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub Delete_OrderTransactionImport(ByVal pstrOrder_Index As String)
        Dim strSQL As String = ""
        Dim odtTransferStatusLocation As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try


            strSQL = "DELETE FROM tb_Order  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "

            strSQL &= "DELETE FROM tb_OrderItem  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "


            strSQL &= "DELETE FROM tb_OrderItemLocation  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "


            strSQL &= "DELETE FROM tb_Tag  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "


            strSQL &= "DELETE FROM tb_LocationBalance  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "


            strSQL &= "DELETE FROM tb_Transaction  "
            strSQL &= " WHERE Order_Index ='" & pstrOrder_Index & "' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            strSQL = " update Sy_AutoNumber set Sys_Value=(select Max(Order_Index) from  tb_Order ) WHERE Sys_Key = 'Order_Index' "
            strSQL &= " update Sy_AutoNumber set Sys_Value=(select Max(OrderItem_Index) from  tb_OrderItem ) WHERE Sys_Key = 'OrderItem_Index' "
            strSQL &= " update Sy_AutoNumber set Sys_Value=(select Max(OrderItemLocation_Index) from  tb_OrderItemLocation ) WHERE Sys_Key = 'OrderItemLocation_Index' "
            strSQL &= " update Sy_AutoNumber set Sys_Value=(select Max(Tag_Index) from  tb_Tag ) WHERE Sys_Key = 'Tag_Index' "
            strSQL &= " update Sy_AutoNumber set Sys_Value=(select Max(Tag_No) from  tb_Tag ) WHERE Sys_Key = 'Tag_No' "
            strSQL &= " update Sy_AutoNumber set Sys_Value=(select Max(LocationBalance_Index) from  tb_LocationBalance ) WHERE Sys_Key = 'LocationBalance_Index' "
            strSQL &= " update Sy_AutoNumber set Sys_Value=(select Max(Transaction_Index) from  tb_Transaction ) WHERE Sys_Key = 'Transaction_Index' "


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery

            EXEC_Command()

            myTrans.Commit()

        Catch e As Exception
            Try
                myTrans.Rollback()

                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub UpdateSys_Value_MaxIndexTable(ByVal Table_Name As String, ByVal Sys_Key As String)
        Dim strSQL As String
        Try

            strSQL = " update Sy_AutoNumber set Sys_Value=(select Max(" & Sys_Key & ") from  " & Table_Name & " ) "
            strSQL &= " WHERE Sys_Key = '" & Sys_Key & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
            strSQL = Nothing
        End Try
    End Sub

    Public Sub UpdateSys_Suggest_Location(ByVal Suggest_Location_Index As String, ByVal TAG_Index As String)
        Dim strSQL As String
        Try

            strSQL = " update tb_TAG SET Suggest_Location_Index = '" & Suggest_Location_Index & "'"
            strSQL &= " WHERE TAG_Index = '" & TAG_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
            strSQL = Nothing
        End Try
    End Sub


    Public Function fncUpdateERPLocation(ByVal Order_Index As String) As Boolean
        Try
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
            End With
            Dim strSQL$ = ""
            strSQL = " update l SET l.ERP_Location = oi.str1 FROM tb_LocationBalance l "
            strSQL &= " INNER JOIN tb_OrderItem oi ON l.OrderItem_Index = oi.OrderItem_Index "
            strSQL &= " where oi.Order_Index = @Order_Index "
            DBExeNonQuery(strSQL)
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Public Function LoadWorkSheet(ByVal pstrFileName As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDbConnection(strConnString)
            Dim odaSource As New OleDbDataAdapter

            '=============
            oConnSource.Open()

            Dim Objdt As DataTable = oConnSource.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

            'Dim Objds As New DataSet

            ' Me.DataSource = Objdt

            Return Objdt

        Catch ex As Exception
            Throw ex
        End Try

    End Function


End Class
