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

Public Class bl_clsImport_TCR : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable

    Public Function GetPO_No(ByVal Purchaseorder_No As String) As DataTable
        Try

            Dim strSQL As String = "" 'SalesOrder_Index,SalesOrder_No
            strSQL = " select * from tb_PurchaseOrder where status <> -1 and Purchaseorder_No = '" & Purchaseorder_No & "'  "

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


    Public Function GetCustomer_Index(ByVal customer_Id As String) As String
        Try

            Dim strSQL As String = "" 'SalesOrder_Index,SalesOrder_No
            strSQL = " select customer_index from ms_customer where status_id <> -1 and customer_Id = '" & customer_Id & "'  "
            Return DBExeQuery_Scalar(strSQL)
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function GetSupplier_Index(ByVal supplier_id As String) As String
        Try

            Dim strSQL As String = "" 'SalesOrder_Index,SalesOrder_No
            strSQL = " select supplier_index from ms_supplier where status_id <> -1 and supplier_id = '" & supplier_id & "'  "
            Return DBExeQuery_Scalar(strSQL)
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetSKU_Index(ByVal SkuID As String) As String
        Try
            Dim strSQL As String = ""

            strSQL = " SELECT sku_index " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE Sku_Id = '" & SkuID & "' and Status_id <> -1"

            Return DBExeQuery_Scalar(strSQL)

        Catch Ex As Exception
            Throw Ex
        End Try

    End Function

    Public Function GetSKU_Detail(ByVal SkuID As String) As DataTable
        Try
            Dim strSQL As String = ""

            strSQL = " SELECT sku_index, Package_Index, UnitWeight_Index " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE Sku_Id = '" & SkuID & "' and Status_id <> -1"

            Return DBExeQuery(strSQL)

        Catch Ex As Exception
            Throw Ex
        End Try

    End Function


    Public Function GetSKU_Data_TCR(Optional ByVal Description As String = "", Optional ByVal Eye As String = "", Optional ByVal Add As String = "", Optional ByVal Tilted As String = "", Optional ByVal Color As String = "", Optional ByVal Degree As String = "", Optional ByVal BC As String = "", Optional ByVal VMI As String = "", Optional ByVal Generation As String = "", Optional ByVal Brand As String = "", Optional ByVal Group As String = "") As DataTable
        Try

            Dim sku_index_res As New DataTable
            Dim getProductType_Index As String = ""

            getProductType_Index = " SELECT * "
            getProductType_Index &= " FROM ms_ProductType where status_id <> -1 and Description = '" & Group & "' "
            getProductType_Index = DBExeQuery_Scalar(getProductType_Index)

            Dim getDatatableMapping As New DataTable

            If getProductType_Index <> "" Then
                Dim strSqlGetMapping As String = ""
                strSqlGetMapping = " SELECT * "
                strSqlGetMapping &= " FROM Config_Sku_Mapping where  productType_index = '" & getProductType_Index & "' "
                getDatatableMapping = DBExeQuery(strSqlGetMapping)

            End If

            Dim strSQlGetSKU As String = ""
            If getDatatableMapping.Rows.Count <= 0 Then

                strSQlGetSKU = "   SELECT * "
                strSQlGetSKU &= "  FROM ms_SKU "
                strSQlGetSKU &= "  WHERE status_id <> -1 "
                strSQlGetSKU &= "  AND Str3 = '" & Description & "'"
                sku_index_res = DBExeQuery(strSQlGetSKU)


            Else

                strSQlGetSKU = "   SELECT * "
                strSQlGetSKU &= "  FROM ms_SKU "
                strSQlGetSKU &= "  WHERE status_id <> -1 "
                strSQlGetSKU &= "  AND Str3 = '" & Description & "'"

                strSQlGetSKU &= " AND Sku_Index IN ( "
                strSQlGetSKU &= " Select  Sku_Index "
                strSQlGetSKU &= " FROM ms_SKU_Detail "
                strSQlGetSKU &= " WHERE "
                strSQlGetSKU &= "  1=1 "

                For Each dtrow As DataRow In getDatatableMapping.Rows
                    'If dtrow.Item("Description") = 1 Then
                    '    strSQlGetSKU &= " and Description = '" & Description & "'"
                    'End If

                    If dtrow.Item("Eye") = True Then
                        strSQlGetSKU &= " and Eye = '" & Eye & "'"
                    End If

                    If dtrow.Item("Add") = True Then
                        strSQlGetSKU &= " and [Add] = '" & Add & "'"
                    End If

                    If dtrow.Item("Tilted") = True Then
                        strSQlGetSKU &= " and Tilted = '" & Tilted & "'"
                    End If

                    If dtrow.Item("Color") = True Then
                        strSQlGetSKU &= " and Color = '" & Color & "'"
                    End If

                    If dtrow.Item("Degree") = True Then
                        strSQlGetSKU &= " and Degree = '" & Degree & "'"
                    End If

                    If dtrow.Item("BC") = True Then
                        strSQlGetSKU &= " and BC = '" & BC & "'"
                    End If

                    If dtrow.Item("VMI") = True Then
                        strSQlGetSKU &= " and VMI = '" & VMI & "'"
                    End If

                    If dtrow.Item("Generation") = True Then
                        strSQlGetSKU &= " and Generation = '" & Generation & "'"
                    End If


                    If dtrow.Item("Brand") = True Then
                        strSQlGetSKU &= " and Brand = '" & Brand & "'"
                    End If

                Next

                sku_index_res = DBExeQuery(strSQlGetSKU + ")")

            End If

            Return sku_index_res
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Function
    Public Function GetSKU_Index_TCR(Optional ByVal Description As String = "", Optional ByVal Eye As String = "", Optional ByVal Add As String = "", Optional ByVal Tilted As String = "", Optional ByVal Color As String = "", Optional ByVal Degree As String = "", Optional ByVal BC As String = "", Optional ByVal VMI As String = "", Optional ByVal Generation As String = "", Optional ByVal Brand As String = "", Optional ByVal Group As String = "") As String
        Try

            Dim sku_index_res As String = ""
            Dim getProductType_Index As String = ""

            getProductType_Index = " SELECT * "
            getProductType_Index &= " FROM ms_ProductType where status_id <> -1 and Description = '" & Group & "' "
            getProductType_Index = DBExeQuery_Scalar(getProductType_Index)

            Dim getDatatableMapping As New DataTable

            If getProductType_Index <> "" Then
                Dim strSqlGetMapping As String = ""
                strSqlGetMapping = " SELECT * "
                strSqlGetMapping &= " FROM Config_Sku_Mapping where  productType_index = '" & getProductType_Index & "' "
                getDatatableMapping = DBExeQuery(strSqlGetMapping)

            End If

            Dim strSQlGetSKU As String = ""
            If getDatatableMapping.Rows.Count <= 0 Then

                strSQlGetSKU = "   SELECT sku_index "
                strSQlGetSKU &= "  FROM ms_SKU "
                strSQlGetSKU &= "  WHERE status_id <> -1 "
                strSQlGetSKU &= "  AND Str3 = '" & Description & "'"
                sku_index_res = DBExeQuery_Scalar(strSQlGetSKU)


            Else

                strSQlGetSKU = "   SELECT sku_index "
                strSQlGetSKU &= "  FROM ms_SKU "
                strSQlGetSKU &= "  WHERE status_id <> -1 "
                strSQlGetSKU &= "  AND Str3 = '" & Description & "'"

                strSQlGetSKU &= " AND Sku_Index IN ( "
                strSQlGetSKU &= " Select  Sku_Index "
                strSQlGetSKU &= " FROM ms_SKU_Detail "
                strSQlGetSKU &= " WHERE "
                strSQlGetSKU &= "  1=1 "

                For Each dtrow As DataRow In getDatatableMapping.Rows
                    'If dtrow.Item("Description") = 1 Then
                    '    strSQlGetSKU &= " and Description = '" & Description & "'"
                    'End If

                    If dtrow.Item("Eye") = True Then
                        strSQlGetSKU &= " and Eye = '" & Eye & "'"
                    End If

                    If dtrow.Item("Add") = True Then
                        strSQlGetSKU &= " and [Add] = '" & Add & "'"
                    End If

                    If dtrow.Item("Tilted") = True Then
                        strSQlGetSKU &= " and Tilted = '" & Tilted & "'"
                    End If

                    If dtrow.Item("Color") = True Then
                        strSQlGetSKU &= " and Color = '" & Color & "'"
                    End If

                    If dtrow.Item("Degree") = True Then
                        strSQlGetSKU &= " and Degree = '" & Degree & "'"
                    End If

                    If dtrow.Item("BC") = True Then
                        strSQlGetSKU &= " and BC = '" & BC & "'"
                    End If

                    If dtrow.Item("VMI") = True Then
                        strSQlGetSKU &= " and VMI = '" & VMI & "'"
                    End If

                    If dtrow.Item("Generation") = True Then
                        strSQlGetSKU &= " and Generation = '" & Generation & "'"
                    End If


                    If dtrow.Item("Brand") = True Then
                        strSQlGetSKU &= " and Brand = '" & Brand & "'"
                    End If

                Next

                sku_index_res = DBExeQuery_Scalar(strSQlGetSKU + ")")

            End If

            Return sku_index_res
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
