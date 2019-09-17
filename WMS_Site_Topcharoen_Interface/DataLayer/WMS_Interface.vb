Imports Newtonsoft.Json
Imports WMS_STD_Formula

Public Class WMS_Interface

    Public Const IMPORT_TABLE_NAME As String = "_Prepare_Imports_Master"
    Public Const IMPORT_PROCEDURE_NAME As String = "sp_Imports_Master"
    Public Const IMPORT_SO_TABLE_NAME As String = "_Prepare_Imports_SalesOrder"
    Public Const IMPORT_SO_PROCEDURE_NAME As String = "sp_Imports_SalesOrder"
    Private xLOG As New Interface_Log_Service

    Private Enum eWMSMasterID
        None = 0
        CustomerShipping
        Sku
    End Enum

    Public Function InsertOMS_API_Log(ByVal Interface_Name As String, ByVal Result As Boolean, ByVal Message As String, ByVal Request As String) As String
        Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.LOG)
        Try
            Dim NewGuID As String = Guid.NewGuid.ToString

            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" INSERT INTO OMS_API ")
                .Append(" ( GuID, Add_Date, Interface_Name, Result, Message, Request ) ")
                .Append(" VALUES ")
                .Append(" ( @GuID, GETDATE(), @Interface_Name, @Result, @Message, @Request ) ")
            End With

            With MyTransaction.SqlParameter
                .Clear()

                .Add("@GuID", SqlDbType.VarChar).Value = NewGuID
                .Add("@Interface_Name", SqlDbType.VarChar).Value = Interface_Name
                .Add("@Result", SqlDbType.Bit).Value = Result
                .Add("@Message", SqlDbType.VarChar).Value = IIf(Message IsNot Nothing, Message.Trim, DBNull.Value)
                .Add("@Request", SqlDbType.VarChar).Value = Request
            End With

            MyTransaction.ExecuteNonQuery(SQL.ToString)
            MyTransaction.Commit()

            Return NewGuID

        Catch Ex As Exception
            MyTransaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function InsertOMS_API_Master_Log(ByVal Interface_Name As String, ByVal Result As Boolean, ByVal Message As String, ByVal DataCount As UInteger) As String
        Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.LOG)
        Try
            Dim NewGuID As String = Guid.NewGuid.ToString

            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" INSERT INTO OMS_API_Master ")
                .Append(" ( GuID, Add_Date, Interface_Name, Result, Message, DataCount ) ")
                .Append(" VALUES ")
                .Append(" ( @GuID, GETDATE(), @Interface_Name, @Result, @Message, @DataCount ) ")
            End With

            With MyTransaction.SqlParameter
                .Clear()

                .Add("@GuID", SqlDbType.VarChar).Value = NewGuID
                .Add("@Interface_Name", SqlDbType.VarChar).Value = Interface_Name
                .Add("@Result", SqlDbType.Bit).Value = Result
                .Add("@Message", SqlDbType.VarChar).Value = IIf(Message IsNot Nothing, Message.Trim, DBNull.Value)
                .Add("@DataCount", SqlDbType.Int).Value = DataCount
            End With

            MyTransaction.ExecuteNonQuery(SQL.ToString)
            MyTransaction.Commit()

            Return NewGuID

        Catch Ex As Exception
            MyTransaction.Rollback()
            Throw Ex
        End Try
    End Function

    'Public Function ConfirmGI(ByVal TransportManifestIndex As String, ByVal AddBy As String) As String
    '    Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.WMS)
    '    Try
    '        Dim SQL_HD As New System.Text.StringBuilder
    '        With SQL_HD
    '            '.Append(" SELECT sopi.Barcode_BOX AS tranf_id, sopi.Barcode_BOX AS tranf_code ")
    '            '.Append(" SELECT sopi.Barcode_GROUP AS tranf_id, sopi.Barcode_GROUP AS tranf_code ")
    '            .Append(" SELECT (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END) AS tranf_id ")
    '            .Append("      , (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END) AS tranf_code ")

    '            .Append(" 	 , 'I51' AS mvt_id, 'LAB' AS tranf_order_type, 'W' AS tranf_group, 'Branch' AS tranf_shipto_group ")
    '            .Append(" 	 , so.OMS_transport_type AS tranf_type, 'WH-HO' AS branch_code, 'สำนักงานใหญ่' AS branch_name ")
    '            .Append(" 	 , cs.Str1 AS shipto_code, cs.Company_Name AS shipto_name, so.OMS_tranf_remark AS tranf_remark ")
    '            .Append(" 	 , @Add_By AS emp_id, @Add_By AS cby, @Add_By AS mby, '' AS tranf_stamp ")
    '            .Append(" 	 , '' AS transport_type, 0 AS is_supplier_pack ")
    '            .Append(" FROM tb_SalesOrderPacking sop ")
    '            .Append(" INNER JOIN tb_SalesOrderPackingItem sopi ")
    '            .Append(" ON sop.SalesOrderPacking_Index = sopi.SalesOrderPacking_Index ")
    '            .Append(" INNER JOIN ( ")
    '            .Append("   SELECT SalesOrder_Index, Customer_Shipping_Index, OMS_tranf_remark, OMS_transport_type, Urgent_Id ")
    '            .Append("   FROM tb_SalesOrder ")
    '            .Append("   WHERE SalesOrder_Index IN ( ")
    '            .Append("       SELECT SalesOrder_Index FROM tb_TransportManifestItem WHERE Status <> -1 AND TransportManifest_Index = @TransportManifest_Index ")
    '            .Append("   ) ")
    '            .Append(" ) so ")
    '            .Append(" ON sopi.SalesOrder_Index = so.SalesOrder_Index ")
    '            .Append(" INNER JOIN ms_Customer_Shipping cs ")
    '            .Append(" ON so.Customer_Shipping_Index = cs.Customer_Shipping_Index ")
    '            .Append(" WHERE sop.Status <> -1 ")
    '            .Append(" GROUP BY (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END), cs.Str1, cs.Company_Name, so.OMS_transport_type, so.OMS_tranf_remark ")
    '        End With

    '        Dim SQL_ITEM As New System.Text.StringBuilder
    '        With SQL_ITEM
    '            '.Append(" SELECT sopi.Barcode_BOX AS tranf_id ")
    '            .Append(" SELECT (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END) AS tranf_id ")
    '            .Append(" 	 , ROW_NUMBER() OVER(PARTITION BY (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END) ORDER BY so.SalesOrder_No ASC) AS trani_item_no ")
    '            .Append(" 	 , sku.Str8 AS matc_id, pdc.ProductClass_Id AS matgp_id ")
    '            .Append(" 	 , sku.Sku_Id AS mat_id, sku.Str6 AS mat_g_id, sku.Str3 AS mat_name ")
    '            .Append(" 	 , SUM(sopi.Total_Qty) AS trani_req_qty, SUM(sopi.Total_Qty) AS trani_actual_qty ")
    '            .Append(" 	 , pk.Description AS trani_unit, '' AS trani_memo, '' AS item_code_01, '' AS item_code_02 ")
    '            .Append(" 	 , so.SalesOrder_No AS trani_ref_type, so.SalesOrder_No AS trani_ref_code ")
    '            .Append(" 	 , '' AS trani_traf_appcode, '' AS trani_approve_code ")
    '            .Append(" 	 , @Add_By AS cby, @Add_By AS mby ")
    '            .Append(" FROM tb_SalesOrderPacking sop ")
    '            .Append(" INNER JOIN tb_SalesOrderPackingItem sopi ")
    '            .Append(" ON sop.SalesOrderPacking_Index = sopi.SalesOrderPacking_Index ")
    '            .Append(" INNER JOIN ( ")
    '            .Append("   SELECT SalesOrder_Index, SalesOrder_No ")
    '            .Append("   FROM tb_SalesOrder ")
    '            .Append("   WHERE SalesOrder_Index IN ( ")
    '            .Append("       SELECT SalesOrder_Index FROM tb_TransportManifestItem WHERE Status <> -1 AND TransportManifest_Index = @TransportManifest_Index ")
    '            .Append("   ) ")
    '            .Append(" ) so ")
    '            .Append(" ON sopi.SalesOrder_Index = so.SalesOrder_Index ")
    '            .Append(" INNER JOIN ms_SKU sku ")
    '            .Append(" ON sopi.Sku_Index = sku.Sku_Index ")
    '            .Append(" INNER JOIN ms_Package pk ")
    '            .Append(" ON sku.Package_Index = pk.Package_Index ")
    '            .Append(" INNER JOIN ms_Product_Class pdc ")
    '            .Append(" ON sku.ProductClass_Index = pdc.ProductClass_Index ")
    '            .Append(" INNER JOIN ms_Product pd ")
    '            .Append(" ON sku.Product_Index = pd.Product_Index ")
    '            .Append(" INNER JOIN ms_ProductType pdt ")
    '            .Append(" ON pd.ProductType_Index = pdt.ProductType_Index ")
    '            .Append(" WHERE sop.Status <> -1 ")
    '            .Append(" GROUP BY (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END), so.SalesOrder_No, pdc.ProductClass_Id ")
    '            .Append("        , sku.Sku_Id, sku.Str3, sku.Str6, sku.Str8, pk.Description ")
    '        End With

    '        Dim SQL_PACK As New System.Text.StringBuilder
    '        With SQL_PACK
    '            '.Append(" SELECT (CASE WHEN ISNULL(sopi.Barcode_GROUP, '') <> '' THEN ISNULL(sopi.Barcode_GROUP, '') ELSE sopi.Barcode_BAG END) AS tranp_id, sopi.Barcode_BAG AS tranp_code ")
    '            .Append(" SELECT sopi.Barcode_BOX AS tranp_id, sopi.Barcode_BOX AS tranp_code ")
    '            .Append(" 	 , ROW_NUMBER() OVER(PARTITION BY ISNULL(sopi.Barcode_BOX, '') ORDER BY sopi.Barcode_BOX, csl.Customer_Shipping_Location_Id ASC) AS tranp_item_no ")
    '            .Append(" 	 , '' AS GTDOCNO, '' AS document_type, '' AS source_depart ")
    '            .Append(" 	 , 'WH-HO' AS branch_code, 'สำนักงานใหญ่' AS branch_name, '' AS add1 ")
    '            .Append(" 	 , csl.Customer_Shipping_Location_Id AS shipto_code, csl.Shipping_Location_Name AS shipto_name, csl.Address AS add2 ")
    '            .Append(" 	 , SUM(sopi.Total_Qty) AS tranp_total ")
    '            .Append(" 	 , '' AS employee_code, 'I51' AS transport_type, '' AS tranp_vendor, cs.Str2 AS tranp_ks, '' AS transport_group ")
    '            .Append(" 	 , csl.Customer_Shipping_Location_Id AS Dropoint, 0 AS is_supplier_pack ")
    '            .Append(" FROM tb_SalesOrderPacking sop ")
    '            .Append(" INNER JOIN tb_SalesOrderPackingItem sopi ")
    '            .Append(" ON sop.SalesOrderPacking_Index = sopi.SalesOrderPacking_Index ")
    '            .Append(" INNER JOIN ( ")
    '            .Append("   SELECT SalesOrder_Index, SalesOrder_No, Customer_Shipping_Index, Customer_Shipping_Location_Index ")
    '            .Append("   FROM tb_SalesOrder ")
    '            .Append("   WHERE SalesOrder_Index IN ( ")
    '            .Append("       SELECT SalesOrder_Index FROM tb_TransportManifestItem WHERE Status <> -1 AND TransportManifest_Index = @TransportManifest_Index ")
    '            .Append("   ) ")
    '            .Append(" ) so ")
    '            .Append(" ON sopi.SalesOrder_Index = so.SalesOrder_Index ")
    '            .Append(" INNER JOIN ms_Customer_Shipping cs ")
    '            .Append(" ON so.Customer_Shipping_Index = cs.Customer_Shipping_Index ")
    '            .Append(" INNER JOIN ms_Customer_Shipping_Location csl ")
    '            .Append(" ON so.Customer_Shipping_Location_Index = csl.Customer_Shipping_Location_Index ")
    '            .Append(" WHERE sop.Status <> -1 ")
    '            .Append(" GROUP BY sopi.Barcode_BOX, csl.Customer_Shipping_Location_Id ")
    '            .Append("        , csl.Shipping_Location_Name, csl.Address, cs.Str2 ")
    '        End With

    '        With MyTransaction.SqlParameter
    '            .Clear()

    '            .Add("@TransportManifest_Index", SqlDbType.VarChar).Value = TransportManifestIndex
    '            .Add("@Add_By", SqlDbType.VarChar).Value = AddBy
    '        End With

    '        Dim Data_HD As DataTable = MyTransaction.ExecuteQuery(SQL_HD.ToString)
    '        If Not WMS_Function.DataTableHasValue(Data_HD) Then
    '            Throw New Exception("Data HD not found")
    '        End If

    '        Dim ListTRANF_HD As List(Of OMS.TRANF_HD) = JsonConvert.DeserializeObject(Of List(Of OMS.TRANF_HD))(JsonConvert.SerializeObject(Data_HD))
    '        If Not ListTRANF_HD.Count > 0 Then
    '            Throw New Exception("Data HD not Mapped")
    '        End If

    '        Dim Data_ITEMS As DataTable = MyTransaction.ExecuteQuery(SQL_ITEM.ToString)
    '        If Not WMS_Function.DataTableHasValue(Data_ITEMS) Then
    '            Throw New Exception("Data ITEM not found")
    '        End If

    '        Dim ListTRANF_ITEM As List(Of OMS.TRANF_ITEMS) = JsonConvert.DeserializeObject(Of List(Of OMS.TRANF_ITEMS))(JsonConvert.SerializeObject(Data_ITEMS))
    '        If Not ListTRANF_ITEM.Count > 0 Then
    '            Throw New Exception("Data ITEM not Mapped")
    '        End If

    '        Dim Data_PACK As DataTable = MyTransaction.ExecuteQuery(SQL_PACK.ToString)
    '        If Not WMS_Function.DataTableHasValue(Data_PACK) Then
    '            Throw New Exception("Data PACK not found")
    '        End If

    '        Dim ListTRANF_PACK As List(Of OMS.TRANF_PACK) = JsonConvert.DeserializeObject(Of List(Of OMS.TRANF_PACK))(JsonConvert.SerializeObject(Data_PACK))
    '        If Not ListTRANF_PACK.Count > 0 Then
    '            Throw New Exception("Data PACK not Mapped")
    '        End If

    '        Dim ConfirmGI_Data As New ConfirmGI_Models
    '        With ConfirmGI_Data
    '            .TRANF_HD = ListTRANF_HD.Item(0)
    '            .TRANF_HD.ITEM = ListTRANF_ITEM.ToArray
    '            '.TRANF_HD.PACK = ListTRANF_PACK.ToArray
    '        End With

    '        Dim Response As OMS.ReturnValue
    '        Dim InterfaceName As String

    '        Using Service As New OMS.OMSApi
    '            Service.Timeout = 30000
    '            Response = New Object 'Service.Set_omsw_data(ConfirmGI_Data.TRANF_HD)
    '            InterfaceName = "Set_omsw_data"
    '        End Using

    '        Dim JSonRequest As String = JsonConvert.SerializeObject(ConfirmGI_Data)

    '        Try
    '            InsertOMS_API_Log(InterfaceName, Response.Result, Response.Message, JSonRequest)

    '        Catch Ex As Exception
    '            'Do Nothing when Exception
    '            'Just Logging
    '        End Try

    '        If Response.Result Then
    '            Dim SQL As New System.Text.StringBuilder
    '            With SQL
    '                .Append(" UPDATE tb_TransportManifest ")
    '                .Append(" SET Interface_Status = 1 ")
    '                .Append("   , Interface_By = @Add_By ")
    '                .Append("   , Interface_Date = GETDATE() ")
    '                .Append(" WHERE TransportManifest_Index = @TransportManifest_Index ")
    '            End With

    '            MyTransaction.ExecuteNonQuery(SQL.ToString)
    '        End If

    '        MyTransaction.Commit()

    '        If Response.Result Then
    '            Return String.Empty
    '        Else
    '            Return Response.Message
    '        End If

    '    Catch Ex As Exception
    '        MyTransaction.Rollback()
    '        Throw Ex
    '    End Try
    'End Function

    Public Function ConfirmGI(ByVal TransportManifestIndex As String, ByVal AddBy As String) As String
        Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.WMS)
        Try
            Dim SQL As String = "EXEC sp_GetOMS_TransferPacking @TransportManifest_Index, @Add_By"

            With MyTransaction.SqlParameter
                .Clear()

                .Add("@TransportManifest_Index", SqlDbType.VarChar).Value = TransportManifestIndex
                .Add("@Add_By", SqlDbType.VarChar).Value = AddBy
            End With

            Dim Data As DataSet = MyTransaction.ExecuteQueryDS(SQL)
            If Not WMS_Function.DataSetHasValue(Data) Then
                Throw New Exception("Data not found")
            End If

            Dim ListTRANF_HD As List(Of OMS.TRANF_HD) = JsonConvert.DeserializeObject(Of List(Of OMS.TRANF_HD))(JsonConvert.SerializeObject(Data.Tables(0)))
            If Not ListTRANF_HD.Count > 0 Then
                Throw New Exception("Data HD not Mapped")
            End If

            Dim TRANF_ITEM As DataTable = Data.Tables(1).Copy
            If Not WMS_Function.DataTableHasValue(TRANF_ITEM) Then
                Throw New Exception("Data ITEM not Mapped")
            End If

            Dim ListTRANF_PACK As List(Of OMS.TRANF_PACK) = JsonConvert.DeserializeObject(Of List(Of OMS.TRANF_PACK))(JsonConvert.SerializeObject(Data.Tables(2)))
            If Not ListTRANF_PACK.Count > 0 Then
                Throw New Exception("Data PACK not Mapped")
            End If

            Dim ListTRANF_ITEM As List(Of OMS.TRANF_ITEMS)
            Dim TranfITEM As DataTable = TRANF_ITEM.Clone
            For Each TranfHD As OMS.TRANF_HD In ListTRANF_HD
                TranfITEM.Rows.Clear()
                For Each Row As DataRow In TRANF_ITEM.Select(String.Format("tranf_id = '{0}'", TranfHD.tranf_id))
                    TranfITEM.Rows.Add(Row.ItemArray)
                Next
                If TranfITEM.Rows.Count > 0 Then
                    ListTRANF_ITEM = JsonConvert.DeserializeObject(Of List(Of OMS.TRANF_ITEMS))(JsonConvert.SerializeObject(TranfITEM))
                    TranfHD.ITEM = ListTRANF_ITEM.ToArray
                End If
            Next

            Dim ConfirmGI_Data As New ConfirmGI_Models
            With ConfirmGI_Data
                .TRANF_HD = ListTRANF_HD
                .TRANF_PACK = ListTRANF_PACK
            End With

            Dim Response As OMS.ReturnValue
            Dim InterfaceName, JSonRequest As String

            'Transfer Doc
            Using Service As New OMS.OMSApi
                Service.Timeout = 30000
                Response = Service.Set_Transfer_Doc(ConfirmGI_Data.TRANF_HD.ToArray)
                InterfaceName = "Set_Transfer_Doc"
            End Using

            JSonRequest = JsonConvert.SerializeObject(ConfirmGI_Data.TRANF_HD)
            Try
                InsertOMS_API_Log(InterfaceName, Response.Result, Response.Message, JSonRequest)

            Catch Ex As Exception
                'Do Nothing when Exception
                'Just Logging
            End Try

            If Not Response.Result Then
                'Transfer Doc Failed
                Throw New Exception("TRANF_HD Interface OMS [ Transfer Doc ] failed : " & Response.Message & "]")
            End If

            'Packing Doc
            Using Service As New OMS.OMSApi
                Service.Timeout = 30000
                Response = Service.Set_Packing_Doc(ConfirmGI_Data.TRANF_PACK.ToArray)
                InterfaceName = "Set_Packing_Doc"
            End Using

            JSonRequest = JsonConvert.SerializeObject(ConfirmGI_Data.TRANF_PACK)
            Try
                InsertOMS_API_Log(InterfaceName, Response.Result, Response.Message, JSonRequest)

            Catch Ex As Exception
                'Do Nothing when Exception
                'Just Logging
            End Try

            If Not Response.Result Then
                'Packing Doc Failed
                Throw New Exception("TRANF_PACK Interface OMS [ Packing Doc ] failed : " & Response.Message & "]")
            End If

            Dim SQL_Update As New System.Text.StringBuilder
            With SQL_Update
                .Append(" UPDATE tb_TransportManifest ")
                .Append(" SET Interface_Status = 1 ")
                .Append("   , Interface_By = @Add_By ")
                .Append("   , Interface_Date = GETDATE() ")
                .Append(" WHERE TransportManifest_Index = @TransportManifest_Index ")
            End With

            MyTransaction.ExecuteNonQuery(SQL_Update.ToString)

            MyTransaction.Commit()
            Return String.Empty

        Catch Ex As Exception
            MyTransaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function IsConfirmGI(ByVal TransportManifestIndex As String) As Boolean
        Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.WMS)
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT (CASE WHEN ISNULL(Interface_Status, 0) = 1 THEN 1 ELSE 0 END) AS Interface_Status ")
                .Append(" FROM tb_TransportManifest ")
                .Append(" WHERE TransportManifest_Index = @TransportManifest_Index ")
            End With

            With MyTransaction.SqlParameter
                .Clear()

                .Add("@TransportManifest_Index", SqlDbType.VarChar).Value = TransportManifestIndex
            End With

            Dim Status As String = MyTransaction.ExecuteScalar(SQL.ToString)

            MyTransaction.Commit()
            Return Status

        Catch Ex As Exception
            MyTransaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Sub ImportMaster(ByVal OMSMasterID As OMS_Interface.eOMSMasterID, ByVal LogGuID As String, ByVal Data As DataTable)
        Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.WMS, True)
        Try
            Dim WMSMasterID As eWMSMasterID
            Select Case OMSMasterID
                Case OMS_Interface.eOMSMasterID.Branch
                    WMSMasterID = eWMSMasterID.CustomerShipping

                Case OMS_Interface.eOMSMasterID.Product
                    WMSMasterID = eWMSMasterID.Sku

                Case Else
                    WMSMasterID = eWMSMasterID.None
            End Select

            Dim ImportGuID As String = Guid.NewGuid.ToString
            With Data.Columns
                With .Add("Kasco_GuID", GetType(String))
                    .Expression = "'" & ImportGuID & "'"
                    .SetOrdinal(0)
                End With

                With .Add("Kasco_GuID_Log", GetType(String))
                    .Expression = "'" & LogGuID & "'"
                    .SetOrdinal(1)
                End With

                With .Add("Kasco_MasterID", GetType(String))
                    .Expression = WMSMasterID
                    .SetOrdinal(2)
                End With

                With .Add("Kasco_Status", GetType(String))
                    .Expression = 0
                    .SetOrdinal(3)
                End With

                With .Add("Kasco_Message", GetType(String))
                    .Expression = String.Empty
                    .SetOrdinal(4)
                End With

                With .Add("IsInsert", GetType(Boolean))
                    .Expression = True
                    .SetOrdinal(5)
                End With
            End With

            'Bulk Data
            MyTransaction.BulkCopy(IMPORT_TABLE_NAME, Data)

            'Import Master
            If My.Settings.Import_RawData Then
                With MyTransaction.SqlParameter
                    .Clear()

                    .Add("@GuID", SqlDbType.Char).Value = ImportGuID
                End With

                Dim QueryProcedure As String = String.Format(" EXEC {0} @GuID ", IMPORT_PROCEDURE_NAME)
                Dim str As String = MyTransaction.ExecuteScalar(QueryProcedure)

                Dim xStatus As String = ""
                Dim xLogDescription As String = ""
                If str.Trim = "S" Then
                    xStatus = "S"
                    xLogDescription = "Success"
                Else
                    xStatus = "E"
                    xLogDescription = "Error"
                End If
                xLOG.KSL_SY_LOG_INTERFACE_ST("", "", "", xStatus, xLogDescription)

            End If

            'Delete Data
            If Not My.Settings.Preserve_RawData Then
                With MyTransaction.SqlParameter
                    .Clear()

                    .Add("@GuID", SqlDbType.Char).Value = ImportGuID
                End With

                MyTransaction.ExecuteNonQuery(String.Format(" DELETE {0} WHERE [GuID] = @GuID ", IMPORT_TABLE_NAME))
            End If

            MyTransaction.Commit()

        Catch Ex As Exception
            MyTransaction.Rollback()
            Throw Ex
        End Try
    End Sub

    Public Sub ImportSalesOrder(ByVal LogGuID As String, ByVal Data As DataTable, ByVal SO_Type As String)
        Dim MyTransaction As New Interface_Transaction(Interface_Transaction.eTransactionType.WMS)
        Try

            Dim ImportGuID As String = Guid.NewGuid.ToString
            With Data.Columns
                With .Add("Kasco_GuID", GetType(String))
                    .Expression = "'" & ImportGuID & "'"
                    .SetOrdinal(0)
                End With

                With .Add("Kasco_GuID_Log", GetType(String))
                    .Expression = "'" & LogGuID & "'"
                    .SetOrdinal(1)
                End With

                With .Add("Kasco_Status", GetType(String))
                    .Expression = 0
                    .SetOrdinal(2)
                End With

                With .Add("Kasco_Message", GetType(String))
                    .Expression = String.Empty
                    .SetOrdinal(3)
                End With

                With .Add("IsHeader", GetType(Boolean))
                    .Expression = False
                    .SetOrdinal(4)
                End With

                With .Add("Seq", GetType(Integer))
                    .Expression = 0
                    .SetOrdinal(5)
                End With

                With .Add("Add_Date", GetType(Date))
                    .SetOrdinal(6)
                End With
            End With

            'Bulk Data
            MyTransaction.BulkCopy(IMPORT_SO_TABLE_NAME, Data)

            'Import Master
            If My.Settings.Import_RawData Then
                With MyTransaction.SqlParameter
                    .Clear()

                    .Add("@GuID", SqlDbType.VarChar).Value = ImportGuID
                    .Add("@SO_Type", SqlDbType.VarChar).Value = SO_Type
                End With

                Dim QueryProcedure As String = String.Format(" EXEC {0} @GuID, @SO_Type ", IMPORT_SO_PROCEDURE_NAME)
                Dim str As String = MyTransaction.ExecuteScalar(QueryProcedure)

                Dim xStatus As String = ""
                Dim xLogDescription As String = ""
                If str.Trim = "S" Then
                    xStatus = "S"
                    xLogDescription = "Success"
                Else
                    xStatus = "E"
                    xLogDescription = "Error"
                End If
                xLOG.KSL_SY_LOG_INTERFACE_ST("", "", "", xStatus, xLogDescription)

            End If

            'Delete Data
            If Not My.Settings.Preserve_RawData Then
                With MyTransaction.SqlParameter
                    .Clear()

                    .Add("@GuID", SqlDbType.Char).Value = ImportGuID
                End With

                MyTransaction.ExecuteNonQuery(String.Format(" DELETE {0} WHERE [GuID] = @GuID ", IMPORT_SO_TABLE_NAME))
            End If

            MyTransaction.Commit()

        Catch Ex As Exception
            MyTransaction.Rollback()
            Throw Ex
        End Try
    End Sub

    Private Class ConfirmGI_Models
        Public TRANF_HD As New List(Of OMS.TRANF_HD)
        Public TRANF_PACK As New List(Of OMS.TRANF_PACK)
    End Class

    Public Function ResponseOrderList(ByVal SalesOrder_No As String, ByVal SO_Type As String, ByVal Transfer_No As String, ByVal ItemCode1 As String, ByVal ItemCode2 As String, ByVal RefJob_No As String, Optional ByVal TimeOut As Integer = 0) As String
        Using Service As New OMS.OMSApi
            Service.Timeout = TimeOut
            Return Service.ResponseOrderList(SalesOrder_No, SO_Type, Transfer_No, ItemCode1, ItemCode2, RefJob_No)
        End Using
    End Function

End Class

Public Class Interface_Transaction

    Private _Connection As SqlClient.SqlConnection
    Private _Transaction As SqlClient.SqlTransaction
    Private _ServerCommand As SqlClient.SqlCommand

    Public ReadOnly Property SqlParameter() As SqlClient.SqlParameterCollection
        Get
            Return Me._ServerCommand.Parameters
        End Get
    End Property

    Public Enum eTransactionType
        LOG = 0
        WMS = 1
    End Enum

    Public Sub New(ByVal TransactionType As eTransactionType, Optional ByVal BeginTransaction As Boolean = False)
        Select Case TransactionType
            Case eTransactionType.LOG
                Me._Connection = New SqlClient.SqlConnection(My.Settings.LOG_ConnectionString)

            Case eTransactionType.WMS
                Me._Connection = New SqlClient.SqlConnection(My.Settings.WMS_ConnectionString)

            Case Else
                Throw New Exception("Invalid Type")
        End Select

        Me._Connection.Open()
        Me._ServerCommand = New SqlClient.SqlCommand
        Me._ServerCommand.Connection = Me._Connection

        If BeginTransaction Then
            Me._Transaction = Me._Connection.BeginTransaction
        Else
            Me._Transaction = Nothing
        End If

        Me._ServerCommand.Transaction = Me._Transaction
    End Sub

    Private Sub DisposeConnection()
        Me._Connection.Close()
        Me._Connection.Dispose()
    End Sub

    Public Sub Commit()
        If Me._Connection.State = ConnectionState.Open AndAlso Me._Transaction IsNot Nothing Then
            Me._Transaction.Commit()
        End If

        DisposeConnection()
    End Sub

    Public Sub Rollback()
        If Me._Connection.State = ConnectionState.Open AndAlso Me._Transaction IsNot Nothing Then
            Me._Transaction.Rollback()
        End If

        DisposeConnection()
    End Sub

    Public Function ExecuteNonQuery(ByVal SQL As String) As Integer
        Try
            Me._ServerCommand.CommandText = SQL
            Me._ServerCommand.CommandType = CommandType.Text
            Me._ServerCommand.CommandTimeout = 0
            Return Me._ServerCommand.ExecuteNonQuery()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function ExecuteQuery(ByVal SQL As String, Optional ByVal eCommandType As CommandType = CommandType.Text) As DataTable
        Try
            Me._ServerCommand.CommandText = SQL
            Me._ServerCommand.CommandType = eCommandType
            Me._ServerCommand.CommandTimeout = 0
            Dim DataReturn As New DataTable

            Using DataAdapter As New SqlClient.SqlDataAdapter(Me._ServerCommand)
                DataAdapter.Fill(DataReturn)
            End Using

            Return DataReturn

        Catch Ex As Exception
            Throw Ex
        End Try

    End Function

    Public Function ExecuteQueryDS(ByVal SQL As String, Optional ByVal eCommandType As CommandType = CommandType.Text) As DataSet
        Try
            Me._ServerCommand.CommandText = SQL
            Me._ServerCommand.CommandType = eCommandType
            Me._ServerCommand.CommandTimeout = 0
            Dim DataReturn As New DataSet

            Using DataAdapter As New SqlClient.SqlDataAdapter(Me._ServerCommand)
                DataAdapter.Fill(DataReturn)
            End Using

            Return DataReturn

        Catch Ex As Exception
            Throw Ex
        End Try

    End Function

    Public Function ExecuteScalar(ByVal SQL As String) As String
        Try
            Me._ServerCommand.CommandText = SQL
            Me._ServerCommand.CommandType = CommandType.Text
            Me._ServerCommand.CommandTimeout = 0

            Return Me._ServerCommand.ExecuteScalar

        Catch Ex As Exception
            Throw Ex
        End Try

    End Function

    Public Sub BulkCopy(ByVal TableName As String, ByVal Data As DataTable)
        Try
            Using MyBulkCopy As New SqlClient.SqlBulkCopy(Me._Connection, SqlClient.SqlBulkCopyOptions.Default, Me._Transaction)
                With MyBulkCopy
                    .ColumnMappings.Clear()
                    .BulkCopyTimeout = 0
                    .DestinationTableName = TableName
                    .WriteToServer(Data)
                    .Close()
                End With
            End Using

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

End Class
