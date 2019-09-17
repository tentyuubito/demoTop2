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

Public Class bl_SO_PRQ_KSL : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable

    Public Function GetSKU_ById(ByVal Sku_Id As String, ByVal pstrCustomer As String) As String
        Try

            Dim strSQL As String = ""
            Dim strReturn As String

            strSQL &= " SELECT * " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE Sku_Id = '" & Sku_Id & "' and Status_id <> -1"
            If pstrCustomer.Trim.Length > 0 Then
                strSQL &= "  and Customer_Index = '" & pstrCustomer & "' "
            End If
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("SKU_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function GetSaleOrder_No(ByVal Salesorder_No As String) As DataTable
        Try

            Dim strSQL As String = "" 'SalesOrder_Index,SalesOrder_No
            strSQL = " select * from tb_Salesorder where status <> -1 and Salesorder_No = '" & Salesorder_No & "'  "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GetSKU_Name(ByVal pstrModel As String) As DataTable
        Try

            Dim strSQL As String = ""

            strSQL = " SELECT * " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE (str4 = '" & pstrModel & "' or sku_id = '" & pstrModel & "') and Status_id <> -1"

            _dataTable = DBExeQuery(strSQL)

            Return _dataTable
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
    Public Function GetWorkWOI_ID(ByVal WOI_ID As String) As DataTable
        Try

            Dim strSQL As String = "" 'SalesOrder_Index,SalesOrder_No
            strSQL = "select * from tb_SalesOrderItem "
            ' strSQL &= "inner join  tb_SalesOrderItem on tb_SalesOrderItem.SalesOrder_Index = tb_SalesOrder_Production_KSL.SalesOrder_Index  "
            '  strSQL &= "inner join  tb_SalesOrder on tb_SalesOrder.SalesOrder_Index = tb_SalesOrder_Production_KSL.SalesOrder_Index  "
            strSQL &= "Where Status <> -1  and WOI_ID = '" & WOI_ID & "'  "

            Return DBExeQuery(strSQL)
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GetSKU_Index(ByVal pstrModel As String, ByVal pstrCustomer As String) As String
        Try

            Dim strSQL As String = ""
            Dim strReturn As String

            strSQL &= " SELECT * " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE str4 = '" & pstrModel & "' and Status_id <> -1"
            If pstrCustomer.Trim.Length > 0 Then
                strSQL &= "  and Customer_Index = '" & pstrCustomer & "' "
            End If
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                strReturn = _dataTable.Rows(0)("SKU_Index").ToString
            Else
                strReturn = ""
            End If
            Return strReturn
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function


    Public Function checkPackage(ByVal pPackage_Id As String, ByVal pSku_Index As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try

            strSQL = "  SELECT        dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_Package.Package_Id, dbo.ms_Package.Description, dbo.ms_SKURatio.Ratio"
            strSQL &= " FROM            dbo.ms_SKU INNER JOIN"
            strSQL &= "                   dbo.ms_SKURatio ON dbo.ms_SKU.Sku_Index = dbo.ms_SKURatio.Sku_Index INNER JOIN"
            strSQL &= "                  dbo.ms_Package ON dbo.ms_SKURatio.Package_Index = dbo.ms_Package.Package_Index"
            strSQL &= "     WHERE (dbo.ms_SKU.status_id <> -1)"
            strSQL &= " And   ms_SKU.Sku_Index = '" & pSku_Index & "'      "
            strSQL &= " And   ms_Package.Description = '" & pPackage_Id & "'      "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
End Class
