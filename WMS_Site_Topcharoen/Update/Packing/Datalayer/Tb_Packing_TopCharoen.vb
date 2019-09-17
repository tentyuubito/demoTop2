Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class Tb_Packing_TopCharoen : Inherits DBType_SQLServer

    Public Function ListCustomerShippingPacking(Optional ByVal RouteDesc As String = "") As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT cs.Customer_Shipping_Index, cs.Str1 + ' : ' + cs.Company_Name AS Shipping_Name ")
                .Append("      , ISNULL(tr.TransportRegion_ID, '') AS Route_ID ")
                .Append(" 	   , SalesOrder.Total_Qty_SO AS Qty_SO, SalesOrder.Total_Qty AS Qty_SL ")
                .Append(" FROM ms_Customer_Shipping cs ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT so.Customer_Shipping_Index, COUNT(so.SalesOrder_Index) AS Total_Qty_SO, SUM(withdraw.Total_Qty) AS Total_Qty ")
                .Append(" 	FROM tb_SalesOrder so ")
                .Append(" 	INNER JOIN ( ")
                .Append("       SELECT sp.SalesOrder_Index, SUM(sp.Total_Qty) AS Total_Qty ")
                .Append("       FROM tb_SalesOrderPackingItem sp ")
                .Append("       WHERE ISNULL(sp.Barcode_Group, '') = '' ")
                .Append("       AND sp.SalesOrderPacking_Index IN ( ")
                .Append(" 	        SELECT SalesOrderPacking_Index ")
                .Append(" 	        FROM tb_SalesOrderPacking ")
                .Append(" 	        WHERE Status <> -1 ")
                .Append(" 	        AND ISNULL(IsSL, 0) = 1 ")
                .Append("       ) ")
                .Append("       GROUP BY sp.SalesOrder_Index ")
                .Append(" 	) Withdraw ")
                .Append(" 	ON so.SalesOrder_Index = withdraw.SalesOrder_Index ")
                .Append(" 	WHERE so.Status NOT IN (-1, -1) ")
                .Append(" 	AND ISNULL(so.Customer_Shipping_Index, '') <> '' ")
                .Append(" 	GROUP BY so.Customer_Shipping_Index ")
                .Append(" ) SalesOrder ")
                .Append(" ON cs.Customer_Shipping_Index = SalesOrder.Customer_Shipping_Index ")
                .Append(" LEFT JOIN ms_TransportRegion tr ")
                .Append(" ON cs.TransportRegion_Index = tr.TransportRegion_Index ")
                .Append(" WHERE 1 = 1 ")

                If Not String.IsNullOrEmpty(RouteDesc) Then
                    .Append(" AND ISNULL(tr.TransportRegion_ID, '') = @TransportRegion_ID ")
                End If

                .Append(" ORDER BY cs.Str1 ASC ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                If Not String.IsNullOrEmpty(RouteDesc) Then
                    .Add("@TransportRegion_ID", SqlDbType.VarChar).Value = RouteDesc
                End If
            End With

            Dim Data As DataTable = DBExeQuery(SQL.ToString)
            'If Data Is Nothing OrElse Not Data.Rows.Count > 0 Then
            '    Throw New Exception("Data not found")
            'End If

            Return Data

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function ListCustomerShippingLocationPacking(Optional ByVal Route As String = "") As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT cs.Str2 AS Route_Id, csl.Customer_Shipping_Location_Index, csl.Customer_Shipping_Location_Id + ' : ' + csl.Shipping_Location_Name AS Shipping_Location_Name ")
                .Append(" 	   , cs.Customer_Shipping_Index ")
                .Append("      , ISNULL(cs.Str1,'') + ' : ' + ISNULL(cs.Company_Name,'') AS Customer_Shipping ")
                .Append(" 	   , SalesOrder.Total_Qty_SO AS Qty_SO, SalesOrder.Total_Qty AS Qty_CL ")
                '.Append("      , dp.Customer_Shipping_Location_Id + ' : ' + dp.Shipping_Location_Name  AS droppoint ")
                .Append(" FROM ms_Customer_Shipping cs ")
                .Append(" INNER JOIN ms_Customer_Shipping_Location csl ")
                .Append(" ON cs.Customer_Shipping_Index = csl.Customer_Shipping_Index ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT so.Customer_Shipping_Location_Index, COUNT(so.SalesOrder_Index) AS Total_Qty_SO, SUM(withdraw.Total_Qty) AS Total_Qty ")
                '.Append(" 	,so.OMS_Droppoint_Index")
                .Append(" 	FROM tb_SalesOrder so ")
                .Append(" 	INNER JOIN ( ")
                .Append(" 		SELECT wdi.DocumentPlan_Index AS SalesOrder_Index, SUM(wdil.Total_Qty) AS Total_Qty ")
                .Append(" 		FROM tb_Withdraw wd ")
                .Append(" 		INNER JOIN tb_WithdrawItem wdi ")
                .Append(" 		ON wd.Withdraw_Index = wdi.Withdraw_Index ")
                .Append(" 		INNER JOIN tb_WithdrawItemLocation wdil ")
                .Append(" 		ON wdi.WithdrawItem_Index = wdil.WithdrawItem_Index ")
                .Append(" 		WHERE wd.Status NOT IN (-1) ")
                .Append(" 		AND ISNULL(wdil.Barcode_Tray, '') <> '' ")
                .Append(" 		AND wdi.Status <> -1 ")
                .Append(" 		AND wdi.Plan_Process = 10 ")
                .Append(" 		AND ISNULL(wdil.Barcode_CL, '') <> '' ")
                .Append(" 		AND ISNULL(wdil.IsPack, 0) = 0 ")
                .Append(" 		GROUP BY wdi.DocumentPlan_Index ")
                .Append(" 	) withdraw ")
                .Append(" 	ON so.SalesOrder_Index = withdraw.SalesOrder_Index ")
                .Append(" 	WHERE so.Status NOT IN (-1, -1) ")
                .Append(" 	AND ISNULL(so.Customer_Shipping_Location_Index, '') <> '' ")
                .Append(" 	GROUP BY so.Customer_Shipping_Location_Index")
                '.Append(" 	,so.OMS_Droppoint_Index")
                .Append(" ) SalesOrder ")
                .Append(" ON csl.Customer_Shipping_Location_Index = SalesOrder.Customer_Shipping_Location_Index ")
                '.Append(" LEFT OUTER JOIN ms_Customer_Shipping_Location dp on dp.Customer_Shipping_Location_Index = SalesOrder.OMS_Droppoint_Index")
                .Append(" WHERE 1 = 1 ")

                If Not String.IsNullOrEmpty(Route) Then
                    .Append(" AND cs.Str2 = @Route ")
                End If

                .Append(" ORDER BY cs.Str1 ASC ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                If Not String.IsNullOrEmpty(Route) Then
                    .Add("@Route", SqlDbType.VarChar).Value = Route
                End If
            End With

            Dim Data As DataTable = DBExeQuery(SQL.ToString)
            'If Data Is Nothing OrElse Not Data.Rows.Count > 0 Then
            '    Throw New Exception("Data not found")
            'End If

            Return Data

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function ListTray(ByVal CustomerShippingLocationIndex As String) As DataTable
        If String.IsNullOrEmpty(CustomerShippingLocationIndex) Then
            Return Nothing
        End If

        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT 0 AS Checked, wdi.DocumentPlan_No AS SalesOrder_No ")
                .Append(" 	   , wdi.DocumentPlan_Index AS SalesOrder_Index, wdi.DocumentPlanItem_Index AS SalesOrderItem_Index ")
                .Append(" 	   , wdil.Barcode_CL, wdil.Barcode_Tray, sk.Sku_Index, sk.Sku_Id, sk.Str1 AS Sku_Name, SUM(wdil.Total_Qty) AS Total_Qty, 1 AS IsScanBarcode ")
                .Append(" 	   , sk.ItemCode_1,sk.ItemCode_2,so.Str10")

                .Append(" 	   , ISNULL(SO.OMS_Droppoint_Index,'') as OMS_Droppoint_Index, ISNULL(SO.OMS_Droppoint_Desc,'') as OMS_Droppoint_Desc ")
                .Append(" 	   , ISNULL(SO.Urgent_Id,'') as Urgent_Id ")
                .Append(" FROM tb_Withdraw wd  ")
                .Append(" INNER JOIN tb_WithdrawItem wdi  ")
                .Append(" ON wd.Withdraw_Index = wdi.Withdraw_Index  ")
                .Append(" INNER JOIN tb_WithdrawItemLocation wdil  ")
                .Append(" ON wdi.WithdrawItem_Index = wdil.WithdrawItem_Index  ")
                '.Append(" INNER JOIN (  ")
                '.Append(" 	SELECT SalesOrder_Index, Process_Id  ")
                '.Append(" 	FROM tb_SalesOrder  ")
                '.Append(" 	WHERE Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index  ")
                '.Append(" ) so  ")
                '.Append(" ON wdi.DocumentPlan_Index = so.SalesOrder_Index  ")
                .Append(" INNER JOIN (  ")
                .Append(" 	SELECT S.SalesOrder_Index, S.Process_Id,SI.SalesOrderItem_Index,SI.Str10, S.Urgent_Id,S.OMS_Droppoint_Index, CS.Str1 AS OMS_Droppoint_Desc ")
                .Append(" 	FROM tb_SalesOrder S ")
                .Append(" 	INNER JOIN tb_SalesOrderItem SI ON SI.SalesOrder_Index = S.SalesOrder_Index ")
                .Append(" 	LEFT JOIN ms_Customer_Shipping cs ON S.OMS_Droppoint_Index = cs.Customer_Shipping_Index ")
                .Append(" 	WHERE S.Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index  ")
                .Append(" ) so  ")
                .Append(" ON wdi.DocumentPlan_Index = so.SalesOrder_Index  AND so.SalesOrderItem_Index =  wdi.DocumentPlanItem_Index ")

                .Append(" AND wdi.Plan_Process = so.Process_Id  ")
                .Append(" INNER JOIN ms_SKU sk ")
                .Append(" ON wdi.Sku_Index = sk.Sku_Index ")
                .Append(" WHERE wd.Status NOT IN (-1, -1) ")
                .Append(" AND wdi.Status <> -1  ")
                .Append(" AND ISNULL(wdil.Barcode_Tray, '') <> '' ")
                .Append(" AND ISNULL(wdil.Barcode_CL, '') <> '' ")
                .Append(" AND ISNULL(wdil.IsPack, 0) = 0 ")
                .Append(" GROUP BY wdi.DocumentPlan_No , wdi.DocumentPlan_Index, wdi.DocumentPlanItem_Index ")
                .Append(" 		 , wdil.Barcode_CL, wdil.Barcode_Tray, sk.Sku_Index, sk.Sku_Id, sk.Str1 ")
                .Append(" 	     , sk.ItemCode_1,sk.ItemCode_2,so.Str10")
                .Append(" 	     , ISNULL(SO.OMS_Droppoint_Index,''), ISNULL(SO.OMS_Droppoint_Desc,''), ISNULL(SO.Urgent_Id,'') ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar).Value = CustomerShippingLocationIndex
            End With

            Dim Data As DataTable = DBExeQuery(SQL.ToString)
            'If Data Is Nothing OrElse Not Data.Rows.Count > 0 Then
            '    Throw New Exception("Data not found")
            'End If

            Data.Columns.Item("Checked").ReadOnly = False
            Return Data

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function ListTraySL(ByVal CustomerShippingIndex As String) As DataTable
        If String.IsNullOrEmpty(CustomerShippingIndex) Then
            Return Nothing
        End If

        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT 0 AS Checked, so.SalesOrder_No, sp.SalesOrder_Index, sp.SalesOrderItem_Index ")
                .Append("      , sp.SalesOrderPacking_Index, sp.Barcode_BAG AS Barcode_SL, sp.Barcode_Tray ")
                .Append("      , sk.Sku_Index, sk.Sku_Id, sk.Str1 AS Sku_Name, sp.Total_Qty,  Droppoint_Desc,Urgent_ID  ")
                .Append(" FROM tb_SalesOrderPackingItem sp ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT SalesOrder_Index, SalesOrder_No, mcs.Customer_Shipping_Location_Id as Droppoint_Desc, Urgent_ID")
                .Append(" 	FROM tb_SalesOrder ")
                .Append(" 	LEFT JOIN  ms_customer_shipping_Location mcs ")
                .Append(" 	on mcs.Customer_Shipping_Location_Index = tb_SalesOrder.oms_droppoint_index ")
                .Append(" 	WHERE tb_SalesOrder.Customer_Shipping_Index = @Customer_Shipping_Index ")
                .Append(" 	AND tb_SalesOrder.Status NOT IN (-1, -1) ")
                .Append(" ) so ")
                .Append(" ON sp.SalesOrder_Index = so.SalesOrder_Index ")
                .Append(" INNER JOIN ms_SKU sk ")
                .Append(" ON sp.Sku_Index = sk.Sku_Index ")
                .Append(" WHERE ISNULL(sp.Barcode_Group, '') = '' ")
                .Append(" AND sp.SalesOrderPacking_Index IN ( ")
                .Append(" 	SELECT SalesOrderPacking_Index ")
                .Append(" 	FROM tb_SalesOrderPacking ")
                .Append(" 	WHERE Status <> -1 ")
                .Append(" 	AND ISNULL(IsSL, 0) = 1 ")
                .Append(" ) ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Customer_Shipping_Index", SqlDbType.VarChar).Value = CustomerShippingIndex
            End With

            Dim Data As DataTable = DBExeQuery(SQL.ToString)
            'If Data Is Nothing OrElse Not Data.Rows.Count > 0 Then
            '    Throw New Exception("Data not found")
            'End If

            Data.Columns.Item("Checked").ReadOnly = False
            Return Data

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function SavePackingGroupSL(ByVal DataSave As DataTable) As String
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()
        Try
            Dim RowAffected As Integer
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_SalesOrderPackingItem ")
                .Append(" SET Barcode_Group = @Barcode_Group ")
                .Append("   , group_Date = GETDATE() ")
                .Append(" WHERE SalesOrderPacking_Index = @SalesOrderPacking_Index ")
            End With

            '"GR-" & Now.ToString("yyyyMMddhhmmss")
            Dim AutoRunning As New Sy_Running
            Dim BarcodeGroup As String = AutoRunning.GetNextRunningBAG

            Using Data As DataTable = DataSave.DefaultView.ToTable(True, "SalesOrderPacking_Index")
                For Each Row As DataRow In Data.Rows
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@SalesOrderPacking_Index", SqlDbType.VarChar).Value = Row.Item("SalesOrderPacking_Index").ToString
                        .Add("@Barcode_Group", SqlDbType.VarChar).Value = BarcodeGroup
                    End With

                    RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
                Next
            End Using

            Transaction.Commit()
            Return BarcodeGroup

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function SavePackingBag(ByVal CustomerShippingLocationIndex As String, ByVal DataSave As DataTable, ByVal DataBarcodeB1 As DataTable, Optional ByVal ByPass As Boolean = False, Optional ByVal IsSL As Boolean = False) As String
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()
        Try
            Dim SQL As System.Text.StringBuilder

            'Header
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" INSERT INTO tb_SalesOrderPacking ")
                .Append(" ( SalesOrderPacking_Index, SalesOrder_Index, BarcodePacking, PackSize_Index, Status_Print")
                .Append(" , Count_Print, TransportManifestItem_Index, [DateAdd], seq, RFID, Status, addby_user_Index, Add_By ,IsSL ) ")
                .Append(" VALUES ")
                .Append(" ( @SalesOrderPacking_Index, @SalesOrder_Index, @BarcodePacking, @PackSize_Index, @Status_Print ")
                .Append(" , @Count_Print, @TransportManifestItem_Index, GETDATE(), @Seq, @RFID, @Status, @addby_user_Index, @Add_By ,@IsSL) ")
            End With

            Dim AutoRunning As New Sy_Running
            Dim BarcodeBag_SL As String = "SL-" & Date.Now.ToString("yyyyMMddhhmmss")
            Dim BarcodeBag As String = AutoRunning.GetNextRunningBAG
            Dim Autonumber As New Sy_AutoNumber
            Dim SalesOrderPackingIndex As String = Autonumber.getSys_Value("SalesOrderPacking_Index")

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@SalesOrderPacking_Index", SqlDbType.VarChar).Value = SalesOrderPackingIndex

                'ByPass Packing
                If ByPass = False Then
                    .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@BarcodePacking", SqlDbType.VarChar).Value = BarcodeBag
                Else
                    If IsSL Then
                        .Add("@BarcodePacking", SqlDbType.VarChar).Value = BarcodeBag_SL
                    Else
                        .Add("@BarcodePacking", SqlDbType.VarChar).Value = BarcodeBag
                    End If

                    .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = DataSave.Rows(0)("SalesOrder_Index").ToString
                End If


                .Add("@PackSize_Index", SqlDbType.VarChar).Value = DBNull.Value
                .Add("@Status_Print", SqlDbType.Int).Value = 0
                .Add("@Count_Print", SqlDbType.Int).Value = 1
                .Add("@TransportManifestItem_Index", SqlDbType.NVarChar).Value = DBNull.Value

                .Add("@Seq", SqlDbType.Int).Value = DBNull.Value
                .Add("@RFID", SqlDbType.NVarChar).Value = DBNull.Value
                .Add("@Status", SqlDbType.Int, 13).Value = 1

                .Add("@addby_user_Index", SqlDbType.NVarChar).Value = WV_User_Index
                .Add("@Add_By", SqlDbType.NVarChar).Value = WV_UserName

                .Add("@IsSL", SqlDbType.Bit, 13).Value = IsSL
            End With

            DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)

            'Item
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" INSERT INTO tb_SalesOrderPackingItem ")
                .Append(" ( SalesOrderPackingItem_Index, SalesOrderPacking_Index, SalesOrder_Index, TransportManifestItem_Index ")
                .Append(" , Sku_Index, Qty_Pack, Total_Qty, seq, SalesOrderItem_Index, Loading_Qty, update_date, Barcode_BAG, Barcode_Tray,Barcode_GROUP, group_date ) ")
                .Append(" VALUES ")
                .Append(" ( @SalesOrderPackingItem_Index, @SalesOrderPacking_Index, @SalesOrder_Index, @TransportManifestItem_Index ")
                .Append(" , @Sku_Index, @Qty_Pack, @Total_Qty, @seq, @SalesOrderItem_Index, @Loading_Qty, @update_date, @Barcode_BAG, @Barcode_Tray,@Barcode_GROUP, (CASE WHEN @IsSL = 1 THEN GETDATE() ELSE NULL END) ) ")
            End With

            Dim SQL_UpdateFlag As New System.Text.StringBuilder
            With SQL_UpdateFlag
                .Append(" UPDATE wdil ")
                .Append(" SET wdil.IsPack = 1 ")
                .Append(" OUTPUT @SalesOrderPacking_Index AS SalesOrderPacking_Index, INSERTED.WithdrawItemLocation_Index, INSERTED.Total_Qty ")
                .Append(" FROM tb_Withdraw wd ")
                .Append(" INNER JOIN tb_WithdrawItem wdi ")
                .Append(" ON wd.Withdraw_Index = wdi.Withdraw_Index ")
                .Append(" INNER JOIN tb_WithdrawItemLocation wdil ")
                .Append(" ON wdi.WithdrawItem_Index = wdil.WithdrawItem_Index ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT SalesOrder_Index, Process_Id, Customer_Shipping_Location_Index ")
                .Append(" 	FROM tb_SalesOrder ")
                .Append(" 	WHERE Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index ")
                .Append(" ) so ")
                .Append(" ON wdi.DocumentPlan_Index = so.SalesOrder_Index ")
                .Append(" AND wdi.Plan_Process = so.Process_Id ")
                .Append(" WHERE wd.Status NOT IN (-1) ")
                .Append(" AND wdi.Status <> -1 ")
                .Append(" AND wdi.DocumentPlan_Index = @DocumentPlan_Index ")
                .Append(" AND wdi.DocumentPlanItem_Index = @DocumentPlanItem_Index ")
                .Append(" AND ISNULL(wdil.Barcode_Tray, '') = @Barcode_Tray ")
                .Append(" AND ISNULL(wdil.Barcode_CL, '') = @Barcode_CL ")
                .Append(" AND ISNULL(wdil.IsPack, 0) = 0 ")
            End With

            For Each Row As DataRow In DataSave.Rows
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@SalesOrderPacking_Index", SqlDbType.VarChar).Value = SalesOrderPackingIndex

                    .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar).Value = CustomerShippingLocationIndex
                    .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = Row.Item("SalesOrder_Index").ToString
                    .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = Row.Item("SalesOrderItem_Index").ToString

                    .Add("@Barcode_Tray", SqlDbType.VarChar).Value = Row.Item("Barcode_Tray").ToString
                    .Add("@Barcode_CL", SqlDbType.VarChar).Value = Row.Item("Barcode_CL").ToString
                End With


                'ByPass Packing
                If ByPass = False Then

                    Dim DataUpdated As DataTable = DBExeQuery(SQL_UpdateFlag.ToString, Transaction.Connection, Transaction)
                    If DataUpdated IsNot Nothing AndAlso DataUpdated.Rows.Count > 0 Then
                        Dim SumTotalQtyUpdated As Decimal = DataUpdated.Compute("SUM(Total_Qty)", "")
                        If SumTotalQtyUpdated <> Row.Item("Total_Qty").ToString Then
                            Throw New Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากจำนวน Packing ไม่ถูกต้อง")
                        End If
                    Else
                        Throw New Exception("ไม่สามารถบันทึกข้อมูลได้ เนื่องจากมีการ Packing สินค้าที่จะบันทึกแล้ว")
                    End If



                    Using BulkCopy As New SqlClient.SqlBulkCopy(Transaction.Connection, SqlClient.SqlBulkCopyOptions.Default, Transaction)
                        With BulkCopy
                            With .ColumnMappings
                                .Clear()

                                .Add(New SqlClient.SqlBulkCopyColumnMapping("SalesOrderPacking_Index", "SalesOrderPacking_Index"))
                                .Add(New SqlClient.SqlBulkCopyColumnMapping("WithdrawItemLocation_Index", "WithdrawItemLocation_Index"))
                            End With

                            .DestinationTableName = "tb_SalesOrderPacking_Withdraw"
                            .WriteToServer(DataUpdated)
                            .Close()
                        End With
                    End Using

                End If 'End ByPass Packing

                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@SalesOrderPackingItem_Index", SqlDbType.VarChar).Value = Autonumber.getSys_Value("SalesOrderPackingItem_Index")
                    .Add("@SalesOrderPacking_Index", SqlDbType.VarChar).Value = SalesOrderPackingIndex

                    .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = Row.Item("SalesOrder_Index").ToString
                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                    .Add("@Qty_Pack", SqlDbType.Decimal).Value = Row.Item("Total_Qty").ToString
                    .Add("@Total_Qty", SqlDbType.Decimal).Value = Row.Item("Total_Qty").ToString
                    .Add("@seq", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@SalesOrderItem_Index", SqlDbType.VarChar).Value = Row.Item("SalesOrderItem_Index").ToString
                    .Add("@Loading_Qty", SqlDbType.Int).Value = 0
                    .Add("@update_date", SqlDbType.SmallDateTime).Value = DBNull.Value

                    .Add("@Barcode_Tray", SqlDbType.VarChar).Value = Row.Item("Barcode_Tray").ToString
                    .Add("@IsSL", SqlDbType.Bit).Value = IsSL

                    If IsSL Then
                        .Add("@Barcode_BAG", SqlDbType.VarChar).Value = BarcodeBag_SL
                        .Add("@Barcode_GROUP", SqlDbType.VarChar).Value = BarcodeBag
                    Else
                        .Add("@Barcode_BAG", SqlDbType.VarChar).Value = BarcodeBag
                        .Add("@Barcode_GROUP", SqlDbType.VarChar).Value = DBNull.Value
                    End If

                End With

                DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            Next

            'Barcode B1
            If DataBarcodeB1 IsNot Nothing AndAlso DataBarcodeB1.Rows.Count > 0 Then
                Using DataBarcodeB1_Copy As DataTable = DataBarcodeB1.Copy
                    Dim UserName As String = WV_UserName
                    DataBarcodeB1_Copy.Columns.Add("Add_By", GetType(String))
                    DataBarcodeB1_Copy.Columns.Add("SalesOrderPacking_Index", GetType(String))

                    For Each Row As DataRow In DataBarcodeB1_Copy.Rows
                        Row.Item("SalesOrderPacking_Index") = SalesOrderPackingIndex
                        Row.Item("Add_By") = UserName
                    Next

                    Using BulkCopy As New SqlClient.SqlBulkCopy(Transaction.Connection, SqlClient.SqlBulkCopyOptions.Default, Transaction)
                        With BulkCopy
                            With .ColumnMappings
                                .Clear()

                                .Add(New SqlClient.SqlBulkCopyColumnMapping("SalesOrderPacking_Index", "SalesOrderPacking_Index"))
                                .Add(New SqlClient.SqlBulkCopyColumnMapping("SalesOrder_Index", "SalesOrder_Index"))
                                .Add(New SqlClient.SqlBulkCopyColumnMapping("SalesOrderItem_Index", "SalesOrderItem_Index"))
                                .Add(New SqlClient.SqlBulkCopyColumnMapping("Barcode", "Barcode"))
                                .Add(New SqlClient.SqlBulkCopyColumnMapping("Add_By", "Add_By"))
                            End With

                            .DestinationTableName = "tb_SalesOrderPacking_Barcode"
                            .WriteToServer(DataBarcodeB1_Copy)
                            .Close()
                        End With
                    End Using
                End Using
            End If

            Transaction.Commit()
            Return SalesOrderPackingIndex

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function ListPackingBag(Optional ByVal Route As String = "", Optional ByVal ShippingIndex As String = "", Optional ByVal ShippingLocationIndex As String = "") As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .AppendLine(" SELECT 0 AS Checked, SalesOrder.Barcode_BAG,SalesOrder.SO_Type, SalesOrder.SAO_Type AS ProductType_Desc, SalesOrder.Total_Qty ")
                .AppendLine(" 	   , cs.Str2 AS [Route], cs.Str1 AS Shipping, cs.Company_Name, SalesOrder.Urgent_Id ")
                .AppendLine(" 	   , csl.Customer_Shipping_Location_Id AS Shipping_Location, csl.Shipping_Location_Name AS Shipping_Desc ")
                .AppendLine("      , csl.Customer_Shipping_Index, csl.Customer_Shipping_Location_Index ")
                '.AppendLine(" 	   , (dp.Customer_Shipping_Location_Id + ' : ' + dp.Shipping_Location_Name) AS droppoint ")
                .AppendLine(" 	   , dp.Customer_Shipping_Location_Id AS droppoint,Convert(date, SalesOrder.Group_Date , 103)  as Group_Date   ")
                .AppendLine(" FROM ( ")
                .AppendLine(" 	SELECT sopi.Barcode_BAG, 'CL' AS SAO_Type,so.SO_Type, so.Customer_Shipping_Location_Index ")
                .AppendLine(" 		 , CAST(SUM(sopi.Total_Qty) AS INT) AS Total_Qty, so.Urgent_Id,so.OMS_Droppoint_Index , Convert(date, sop.DateAdd , 103)  as Group_Date ")
                .AppendLine(" 	FROM tb_SalesOrderPacking sop ")
                .AppendLine(" 	INNER JOIN tb_SalesOrderPackingItem sopi ")
                .AppendLine(" 	ON sop.SalesOrderPacking_Index = sopi.SalesOrderPacking_Index ")
                .AppendLine(" 	INNER JOIN tb_SalesOrder so ")
                .AppendLine(" 	ON sopi.SalesOrder_Index = so.SalesOrder_Index ")
                .AppendLine(" 	WHERE sop.Status = 1 ")
                .AppendLine(" 	AND ISNULL(sop.IsSL, 0) = 0 ")
                .AppendLine(" 	AND ISNULL(sopi.Barcode_BAG, '') <> '' ")
                .AppendLine(" 	AND ISNULL(sopi.Barcode_Box, '') = '' ")
                .AppendLine(" 	GROUP BY sopi.Barcode_BAG, so.Customer_Shipping_Location_Index, so.Urgent_Id ,so.SO_Type,so.OMS_Droppoint_Index , Convert(date, sop.DateAdd , 103)  ")
                .AppendLine(" 	UNION ALL ")
                .AppendLine(" 	SELECT ISNULL(sopi.Barcode_GROUP, ''), 'SL' AS SAO_Type,so.SO_Type, so.Customer_Shipping_Location_Index ")
                .AppendLine(" 		 , CAST(SUM(sopi.Total_Qty) AS INT) AS Total_Qty, so.Urgent_Id,so.OMS_Droppoint_Index, Convert(date, sopi.Group_Date , 103) ")
                .AppendLine(" 	FROM tb_SalesOrderPacking sop ")
                .AppendLine(" 	INNER JOIN tb_SalesOrderPackingItem sopi ")
                .AppendLine(" 	ON sop.SalesOrderPacking_Index = sopi.SalesOrderPacking_Index ")
                .AppendLine(" 	INNER JOIN tb_SalesOrder so ")
                .AppendLine(" 	ON sopi.SalesOrder_Index = so.SalesOrder_Index ")
                .AppendLine(" 	WHERE sop.Status = 1 ")
                .AppendLine(" 	AND ISNULL(sop.IsSL, 0) = 1 ")
                .AppendLine(" 	AND ISNULL(sopi.Barcode_GROUP, '') <> '' ")
                .AppendLine(" 	AND ISNULL(sopi.Barcode_Box, '') = '' ")
                .AppendLine(" 	GROUP BY ISNULL(sopi.Barcode_GROUP, ''), so.Customer_Shipping_Location_Index, so.Urgent_Id,so.SO_Type,so.OMS_Droppoint_Index  , Convert(date, sopi.Group_Date , 103)")
                .AppendLine(" ) SalesOrder ")
                .AppendLine(" INNER JOIN ms_Customer_Shipping_Location csl ")
                .AppendLine(" ON SalesOrder.Customer_Shipping_Location_Index = csl.Customer_Shipping_Location_Index ")
                .AppendLine(" left outer join ms_Customer_Shipping_Location dp on  SalesOrder.OMS_Droppoint_Index = dp.Customer_Shipping_Location_Index ")
                .AppendLine(" INNER JOIN ms_Customer_Shipping cs ")
                .AppendLine(" ON csl.Customer_Shipping_Index = cs.Customer_Shipping_Index ")
                .AppendLine(" WHERE 1 = 1 ")

                'If chkY = False Then
                '    .AppendLine(" AND SalesOrder.SO_Type not in ('Y') ")
                'End If

                'If chkZ = False Then
                '    .AppendLine(" AND SalesOrder.SO_Type not in ('Z') ")
                'End If


                If Not String.IsNullOrEmpty(Route) Then
                    .AppendLine(" AND cs.Str2 = @Route ")
                End If

                If Not String.IsNullOrEmpty(ShippingIndex) Then
                    .AppendLine(" AND cs.Customer_Shipping_Index = @Shipping ")
                End If

                If Not String.IsNullOrEmpty(ShippingLocationIndex) Then
                    .AppendLine(" AND csl.Customer_Shipping_Location_Index = @ShippingLocation ")
                End If

                .AppendLine(" ORDER BY cs.Str1 ASC ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                If Not String.IsNullOrEmpty(Route) Then
                    .Add("@Route", SqlDbType.VarChar).Value = Route
                End If

                If Not String.IsNullOrEmpty(ShippingIndex) Then
                    .Add("@Shipping", SqlDbType.VarChar).Value = ShippingIndex
                End If

                If Not String.IsNullOrEmpty(ShippingLocationIndex) Then
                    .Add("@ShippingLocation", SqlDbType.VarChar).Value = ShippingLocationIndex
                End If
            End With

            Dim Data As DataTable = DBExeQuery(SQL.ToString)
            'If Data Is Nothing OrElse Not Data.Rows.Count > 0 Then
            '    Throw New Exception("Data not found")
            'End If

            Data.Columns.Item("Checked").ReadOnly = False
            Return Data

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function SavePackingBox(ByVal DataSave As DataTable, Optional ByVal PackSize_Index As String = "") As String
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()
        Try
            Dim SQL, SQL2 As System.Text.StringBuilder

            'Update Barcode Box
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_SalesOrderPackingItem ")
                .Append(" SET Barcode_Box = @Barcode_Box ")
                .Append("   , Update_By = @Update_By ")
                .Append("   , Update_Date = GETDATE() ")
                .Append(" OUTPUT DELETED.SalesOrderPacking_Index ")
                .Append(" WHERE (CASE WHEN @Type = 'SL' THEN Barcode_GROUP ELSE Barcode_BAG END) = @Barcode_Bag ")
                .Append(" AND ISNULL(Barcode_Box, '') = '' ")
            End With

            'Update Status Header
            SQL2 = New System.Text.StringBuilder
            With SQL2
                .Append(" UPDATE tb_SalesOrderPacking ")
                .Append(" SET Status = 2, PackSize_Index = @PackSize_Index")
                .Append(" OUTPUT DELETED.Status ")
                .Append(" WHERE SalesOrderPacking_Index = @SalesOrderPacking_Index ")
            End With

            'Urgent ********************************************************************************************************************************
            Dim strUrgent As String = ""
            Dim dtUrgent As New DataTable
            Dim sqlUrgent As New System.Text.StringBuilder
            With sqlUrgent
                .Append(" SELECT * ")
                .Append(" FROM Config_Priority ")
                .Append(" WHERE ISNULL(Prefix,'') <> '' AND Priority_ID = @Priority_ID")
            End With
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Priority_ID", SqlDbType.VarChar).Value = DataSave.Rows.Item(0).Item("Urgent_Id").ToString
            End With
            dtUrgent = DBExeQuery(sqlUrgent.ToString, Transaction.Connection, Transaction)
            If dtUrgent.Rows.Count > 0 Then
                strUrgent = dtUrgent.Rows(0).Item("Prefix").ToString
            Else
                strUrgent = "ND"
            End If
            Dim AutoRunning As New Sy_Running
            'Dim BarcodeBox As String = AutoRunning.GetNextRunningBOX(strUrgent, DataSave.Rows.Item(0).Item("Shipping").ToString)
            Dim BarcodeBox As String = AutoRunning.GetNextRunningBOX(strUrgent, DataSave.Rows.Item(0).Item("droppoint").ToString)
            '********************************************************************************************************************************

            Dim PackingData As DataTable
            Dim DeletedStatus As String

            For Each Row As DataRow In DataSave.DefaultView.ToTable(True, "Barcode_BAG", "ProductType_Desc").Rows
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@Type", SqlDbType.VarChar).Value = Row.Item("ProductType_Desc").ToString
                    .Add("@Barcode_Bag", SqlDbType.VarChar).Value = Row.Item("Barcode_BAG").ToString
                    .Add("@Barcode_Box", SqlDbType.VarChar).Value = BarcodeBox
                    .Add("@Update_By", SqlDbType.VarChar).Value = WV_UserName
                End With

                PackingData = DBExeQuery(SQL.ToString, Transaction.Connection, Transaction)
                If Not PackingData.Rows.Count > 0 Then
                    Throw New Exception(String.Format("ไม่สามารถปิดกล่องได้ กรุณาตรวจสอบข้อมูลถุง [ {0} ]", Row.Item("Barcode_BAG").ToString))
                End If

                For Each PackingRow As DataRow In PackingData.DefaultView.ToTable(True, "SalesOrderPacking_Index").Rows
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@SalesOrderPacking_Index", SqlDbType.VarChar).Value = PackingRow.Item("SalesOrderPacking_Index").ToString
                        .Add("@PackSize_Index", SqlDbType.VarChar).Value = PackSize_Index
                    End With

                    DeletedStatus = DBExeQuery_Scalar(SQL2.ToString, Transaction.Connection, Transaction)

                    Select Case DeletedStatus
                        Case "-1"
                            Throw New Exception("ไม่สามารถยกปิดกล่องได้ สถานะถุง ได้ถูก ยกเลิก ไปเรียบร้อยแล้ว")

                        Case "2"
                            Throw New Exception("ไม่สามารถยกปิดกล่องได้ สถานะถุง ได้ถูก แพคกล่อง ไปเรียบร้อยแล้ว")
                    End Select
                Next
            Next

            Transaction.Commit()
            Return BarcodeBox

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

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

    Public Function GetReportData(ByVal pstrSQL As String, ByVal pstrWhere As String) As DataTable
        Dim strSQL As String = ""


        Try
            strSQL = " SELECT * "
            strSQL &= " FROM  " & pstrSQL
            strSQL &= " WHERE    1=1 " & pstrWhere

            SetSQLString = strSQL

            connectDB()

            EXEC_DataAdapter()

            Return GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function GetDataReport(ByVal pstrReportName As String) As DataTable
        Dim strSQL As String = ""

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM  config_Report "
            strSQL &= " WHERE     Report_Name = '" & pstrReportName & "'"
            strSQL &= " Order By Seq ASC"



            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getSalesOrderPacking(ByVal customer_shipping_index As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try
            strSQL &= " select tb_so.SalesOrder_Index ,tb_so.SalesOrder_No , ms_route.Description as route_Desc, ms_cs.Company_Name,ms_cs_l.Shipping_Location_Name   "
            strSQL &= "  , tb_SalesOrderPackingitem.Barcode_BAG  "
            strSQL &= "  , tb_SalesOrderPackingitem.total_qty "
            strSQL &= "  from (  "
            strSQL &= "           select SalesOrder_Index , Barcode_BAG , sum(Total_Qty) as total_qty  "
            strSQL &= "           from tb_SalesOrderPackingitem  "
            strSQL &= "           where Barcode_Box Is null  "
            strSQL &= "           group by SalesOrder_Index , Barcode_BAG "
            strSQL &= "       ) as tb_SalesOrderPackingitem  "
            strSQL &= "  inner join tb_salesOrder tb_so on tb_so.salesOrder_index = tb_SalesOrderPackingitem.salesOrder_index "
            strSQL &= "  inner join ms_customer_Shipping  ms_cs on tb_so.customer_shipping_index = ms_cs.customer_shipping_index "
            strSQL &= "  inner join ms_customer_Shipping_location ms_cs_l on tb_so.Customer_Shipping_Location_Index = ms_cs_l.Customer_Shipping_Location_Index "
            strSQL &= "  left join ms_route on tb_so.route_index = ms_route.route_index "


            'strSQL &= " select tb_salesOrder.SalesOrder_No,  tb_salesOrder.SalesOrder_index,  ms_Customer_Shipping_location.Shipping_Location_Name,  tb_salesOrder.Customer_Shipping_Index, Company_Name as customer_shipping_location_name  , isnull(tb_SalesOrderPackingitem.barcode_bag,'') as CodeBAG "
            'strSQL &= " ,(case when isnull(tb_SalesOrderPackingitem.barcode_bag,'') = '' then 'Wait' else 'Pass' end) as ConfirmBAG  "
            'strSQL &= " from tb_salesOrder "
            'strSQL &= " inner join tb_SalesOrderPackingitem on tb_SalesOrderPackingitem.SalesOrder_Index = tb_salesOrder.SalesOrder_Index "
            'strSQL &= " inner join ms_Customer_Shipping  on ms_Customer_Shipping.Customer_Shipping_Index = tb_salesOrder.Customer_Shipping_Index "
            'strSQL &= " inner join ms_Customer_Shipping_location  on ms_Customer_Shipping_location.Customer_Shipping_Location_Index = tb_salesOrder.Customer_Shipping_Location_Index "

            If Not String.IsNullOrEmpty(customer_shipping_index) Then
                strSQL &= " WHERE tb_so.Customer_Shipping_Index = '" & customer_shipping_index & "' "
            End If

            'strSQL &= " group by   tb_salesOrder.SalesOrder_No,"
            'strSQL &= "  tb_salesOrder.Customer_Shipping_Index, Company_Name"
            'strSQL &= " ,tb_SalesOrderPackingitem.Barcode_BAG , tb_salesOrder.SalesOrder_index, Shipping_Location_Name "

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function
    Public Function getSalesOrder(ByVal customer_shipping_location_index As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try

            strSQL &= " select   isnull(pick.Pick_Total_Qty, 0) as PickQty, tb_SalesOrderItem.Total_Qty, tb_SalesOrderItem.Total_Qty as Barcode_Qty, 0 as Barcode_Qty_Confirm, 0 as CheckHeader,  0 AS Checked,tb_SalesOrderitem.SalesOrderItem_Index,  tb_SalesOrder.SalesOrder_Index , tb_SalesOrderItem.Sku_Index , tb_SalesOrder.SalesOrder_No, ms_route.[Description] as Route_Name,  ms_Customer_Shipping.Company_Name , "
            strSQL &= " ms_ProductType.ProductType_Index as ProductType_Index , ms_Customer_Shipping_location.Shipping_Location_Name, "
            strSQL &= " ms_ProductType.ProductType_Id as ProductType_Id , "
            strSQL &= " ms_ProductType.ProductType_Id as [Group], "
            strSQL &= " ms_SKU_Detail.Eye as [Eye], ms_SKU_Detail.[Add]  "
            strSQL &= " ,ms_SKU_Detail.[Tilted]  , ms_SKU_Detail.[Color] ,"
            strSQL &= " ms_SKU_Detail.[Degree] ,  ms_SKU_Detail.[BC],"
            strSQL &= " ms_SKU_Detail.[VMI], ms_SKU_Detail.Generation,"
            strSQL &= "    ms_SKU_Detail.Brand, tb_SalesOrderItem.Total_Qty "
            strSQL &= "  from tb_SalesOrder"
            strSQL &= " inner join tb_SalesOrderItem on tb_SalesOrderItem.SalesOrder_Index = tb_SalesOrder.SalesOrder_Index  "
            strSQL &= " inner join ms_sku on ms_sku.sku_index = tb_SalesOrderItem.sku_index  "
            strSQL &= " inner join ms_product on ms_product.Product_Index = ms_sku.Product_Index  "
            strSQL &= " inner join ms_ProductType on ms_ProductType.ProductType_Index = ms_product.ProductType_Index  "
            strSQL &= " left join ms_SKU_Detail on ms_SKU_Detail.Sku_Index = ms_sku.Sku_Index  "
            strSQL &= " left  join ms_route on ms_route.Route_Index = tb_salesorder.Route_Index"
            strSQL &= " inner join ms_Customer_Shipping  on ms_Customer_Shipping.Customer_Shipping_Index = tb_SalesOrder.Customer_Shipping_Index"
            strSQL &= " inner join ms_Customer_Shipping_location on ms_Customer_Shipping_location.Customer_Shipping_Location_Index = tb_SalesOrder.Customer_Shipping_Location_Index "
            strSQL &= "  Left JOIN ("
            strSQL &= "SELECT wdi.DocumentPlan_Index AS SalesOrder_Index, wdi.DocumentPlanItem_Index AS SalesOrderItem_Index"
            strSQL &= " , SUM(wdi.Total_Qty) AS Total_Qty"
            strSQL &= "	 , SUM(CASE WHEN wdi.Status = -9 THEN wdi.Total_Qty ELSE 0 END) AS Pick_Total_Qty"
            strSQL &= "	FROM tb_WithdrawItem wdi"
            strSQL &= "	INNER JOIN tb_WithdrawItemLocation wdil"
            strSQL &= "	ON wdi.WithdrawItem_Index = wdil.WithdrawItem_Index"
            strSQL &= "        WHERE(wdi.Status <> -1)"
            strSQL &= "	AND wdi.Plan_Process = 10"
            strSQL &= "	GROUP BY wdi.DocumentPlan_Index, wdi.DocumentPlanItem_Index "
            strSQL &= " ) pick  on tb_SalesOrderItem.salesOrder_index = pick.salesOrder_Index    "
            strSQL &= " AND tb_SalesOrderItem.salesOrderItem_index = pick.salesOrderItem_index    "
            'strSQL = " select tb_SalesOrder.SalesOrder_Index , ms_route.[Description] as Route_Name,  ms_Customer_Shipping.Company_Name"
            'strSQL &= " , ms_Customer_Shipping_location.Shipping_Location_Name "
            'strSQL &= "  from tb_salesorder "
            'strSQL &= "  left  join ms_route on ms_route.Route_Index = tb_salesorder.Route_Index "
            'strSQL &= "  inner join ms_Customer_Shipping  on ms_Customer_Shipping.Customer_Shipping_Index = tb_SalesOrder.Customer_Shipping_Index "
            'strSQL &= "  inner join ms_Customer_Shipping_location on ms_Customer_Shipping_location.Customer_Shipping_Location_Index = tb_SalesOrder.Customer_Shipping_Location_Index "
            strSQL &= " WHERE 1 = 1 "
            If Not String.IsNullOrEmpty(customer_shipping_location_index) Then
                strSQL &= " AND tb_salesorder.Customer_Shipping_Location_Index = '" & customer_shipping_location_index & "' "
            End If

            strSQL &= " and ms_ProductType.ProductType_Id <> 'SL' and tb_salesorderItem.salesOrderItem_Index not in ( select salesOrderItem_Index from tb_SalesOrderPackingItem) "

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function

    Public Function getSalesOrderGroupItem(ByVal salesOrder_Index As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try

            strSQL = " select ms_ProductType.ProductType_Index as ProductType_Index , ms_ProductType.ProductType_Id as ProductType_Id ,  ms_ProductType.ProductType_Id as [Group], "
            strSQL &= " ms_SKU_Detail.Eye as [Eye], ms_SKU_Detail.[Add]   , ms_SKU_Detail.[Tilted]  , ms_SKU_Detail.[Color] , "
            strSQL &= " ms_SKU_Detail.[Degree] , "
            strSQL &= " ms_SKU_Detail.[BC], ms_SKU_Detail.[VMI], ms_SKU_Detail.Generation, ms_SKU_Detail.Brand, sum(tb_SalesOrderItem.Total_Qty) "
            strSQL &= " from tb_SalesOrder "
            strSQL &= " inner join tb_SalesOrderItem on tb_SalesOrderItem.SalesOrder_Index = tb_SalesOrder.SalesOrder_Index "
            strSQL &= " inner join ms_sku on ms_sku.sku_index = tb_SalesOrderItem.sku_index "
            strSQL &= " inner join ms_product on ms_product.Product_Index = ms_sku.Product_Index "
            strSQL &= " inner join ms_ProductType on ms_ProductType.ProductType_Index = ms_product.ProductType_Index "
            strSQL &= " inner join ms_SKU_Detail on ms_SKU_Detail.Sku_Index = ms_sku.Sku_Index "

            If Not String.IsNullOrEmpty(salesOrder_Index) Then
                strSQL &= " WHERE tb_salesorder.SalesOrder_Index in (" & salesOrder_Index & ") "
            End If

            strSQL &= " group by ms_ProductType.ProductType_Id , ms_ProductType.ProductType_Index,  "
            strSQL &= " ms_SKU_Detail.Eye , ms_SKU_Detail.[Add]   , ms_SKU_Detail.[Tilted]  , ms_SKU_Detail.[Color] , "
            strSQL &= " ms_SKU_Detail.[Degree] ,"
            strSQL &= " ms_SKU_Detail.[BC], ms_SKU_Detail.[VMI], ms_SKU_Detail.Generation, ms_SKU_Detail.Brand "
            strSQL &= "  ,tb_SalesOrderItem.Total_Qty "

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function


    Public Function getSalesOrderItem(ByVal salesOrder_Index As String, ByVal productType_Index As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try

            strSQL = " select tb_SalesOrderitem.sku_index , tb_SalesOrder.salesOrder_index,  tb_SalesOrderItem.salesOrderItem_Index, ms_ProductType.ProductType_Index as ProductType_Index , ms_ProductType.ProductType_Id as ProductType_Id ,  ms_ProductType.ProductType_Id as [Group], "
            strSQL &= "  ms_SKU_Detail.Eye as [Eye], ms_SKU_Detail.[Add]   , ms_SKU_Detail.[Tilted]  , ms_SKU_Detail.[Color] , "
            strSQL &= " ms_SKU_Detail.[Degree] , "
            strSQL &= " ms_SKU_Detail.[BC], ms_SKU_Detail.[VMI], ms_SKU_Detail.Generation, ms_SKU_Detail.Brand, tb_SalesOrderItem.Total_Qty "
            strSQL &= " from tb_SalesOrder "
            strSQL &= " inner join tb_SalesOrderItem on tb_SalesOrderItem.SalesOrder_Index = tb_SalesOrder.SalesOrder_Index "
            strSQL &= " inner join ms_sku on ms_sku.sku_index = tb_SalesOrderItem.sku_index "
            strSQL &= " inner join ms_product on ms_product.Product_Index = ms_sku.Product_Index "
            strSQL &= " inner join ms_ProductType on ms_ProductType.ProductType_Index = ms_product.ProductType_Index "
            strSQL &= " inner join ms_SKU_Detail on ms_SKU_Detail.Sku_Index = ms_sku.Sku_Index  where 1=1 "

            If Not String.IsNullOrEmpty(salesOrder_Index) Then
                strSQL &= " and  tb_salesorder.SalesOrder_Index in (" & salesOrder_Index & ") "
            End If

            If Not String.IsNullOrEmpty(salesOrder_Index) Then
                strSQL &= " and  ms_ProductType.ProductType_Index in (" & productType_Index & ") "
            End If


            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function



    Public Function SavePackingBox(ByVal StrSalesOrderItem As String, ByVal BarcodeBag As String, ByVal Barcode_Box As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try


            strSQL &= " Update tb_SalesOrderPackingitem  set Barcode_BOX = '" & Barcode_Box & "' "
            strSQL &= " where salesOrder_index in (" & StrSalesOrderItem & ") and Barcode_BAG in (" & BarcodeBag & ")  "

            DBExeNonQuery(strSQL, myTrans.Connection, myTrans)

            myTrans.Commit()

            Return True

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function


    Public Function SavePacking(ByVal dtrowSalesOrderItem As DataRow(), ByVal DataBarcode As DataRow()) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            strSQL &= "   INSERT INTO tb_SalesOrderPacking (SalesOrderPacking_Index , SalesOrder_Index , BarcodePacking , PackSize_Index , Status_Print, Count_Print , TransportManifestItem_Index , [DateAdd] , seq, RFID, Status , addby_user_Index)"
            strSQL &= "  values (@SalesOrderPacking_Index , @SalesOrder_Index , @BarcodePacking , @PackSize_Index , @Status_Print, @Count_Print , @TransportManifestItem_Index , @DateAdd , @seq, @RFID, @Status , @addby_user_Index)"

            Dim BarcodeBag As String = Now.ToString("yyyyMMddhhmmss")
            Dim clsAutonumber As New Sy_AutoNumber
            Dim getSalesOrderPacking_Index As String = clsAutonumber.getSys_Value("SalesOrderPacking_Index")
            With SQLServerCommand.Parameters

                .Clear()
                .Add("@SalesOrderPacking_Index", SqlDbType.VarChar, 13).Value = getSalesOrderPacking_Index
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 15).Value = dtrowSalesOrderItem(0).Item("SalesOrder_Index")
                .Add("@BarcodePacking", SqlDbType.VarChar).Value = BarcodeBag
                .Add("@PackSize_Index", SqlDbType.VarChar, 100).Value = ""
                .Add("@Status_Print", SqlDbType.Int, 50).Value = 0
                .Add("@Count_Print", SqlDbType.Int).Value = 1
                .Add("@TransportManifestItem_Index", SqlDbType.NVarChar, 50).Value = DBNull.Value
                .Add("@DateAdd", SqlDbType.SmallDateTime, 50).Value = Now
                .Add("@seq", SqlDbType.Int, 50).Value = DBNull.Value
                .Add("@RFID", SqlDbType.NVarChar, 50).Value = DBNull.Value
                .Add("@Status", SqlDbType.Int, 13).Value = 1
                .Add("@addby_user_Index", SqlDbType.NVarChar, 13).Value = WV_User_Index

            End With

            DBExeNonQuery(strSQL, myTrans.Connection, myTrans)

            For Each dtrow As DataRow In dtrowSalesOrderItem

                strSQL = ""
                strSQL &= "  INSERT INTO tb_SalesOrderPackingItem (SalesOrderPackingItem_Index,SalesOrderPacking_Index,SalesOrder_Index,TransportManifestItem_Index,Sku_Index,Qty_Pack,Total_Qty,seq,SalesOrderItem_Index,Loading_Qty,update_date, Barcode_BAG) "
                strSQL &= "  values (@SalesOrderPackingItem_Index,@SalesOrderPacking_Index,@SalesOrder_Index,@TransportManifestItem_Index,@Sku_Index,@Qty_Pack,@Total_Qty,@seq,@SalesOrderItem_Index,@Loading_Qty, @update_date, @Barcode_BAG) "

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@SalesOrderPackingItem_Index", SqlDbType.VarChar, 13).Value = clsAutonumber.getSys_Value("SalesOrderPackingItem_Index")
                    .Add("@SalesOrderPacking_Index", SqlDbType.VarChar, 15).Value = getSalesOrderPacking_Index
                    .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = dtrow.Item("SalesOrder_Index")
                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 100).Value = DBNull.Value
                    .Add("@Sku_Index", SqlDbType.VarChar, 50).Value = dtrow.Item("Sku_Index")
                    .Add("@Qty_Pack", SqlDbType.Int).Value = dtrow.Item("Total_Qty")
                    .Add("@Total_Qty", SqlDbType.NVarChar, 50).Value = dtrow.Item("Total_Qty")
                    .Add("@seq", SqlDbType.NVarChar, 50).Value = DBNull.Value
                    .Add("@SalesOrderItem_Index", SqlDbType.NVarChar, 50).Value = dtrow.Item("SalesOrderItem_Index")
                    .Add("@Loading_Qty", SqlDbType.NVarChar, 50).Value = 0
                    .Add("@update_date", SqlDbType.SmallDateTime, 13).Value = DBNull.Value
                    .Add("@Barcode_BAG", SqlDbType.VarChar).Value = BarcodeBag
                End With

                DBExeNonQuery(strSQL, myTrans.Connection, myTrans)

            Next

            Dim Barcode As DataTable
            Dim SQL As String = " INSERT INTO tb_SalesOrderPacking_Barcode (SalesOrderPacking_index, Barcode) VALUES (@SalesOrderPacking_index, @Barcode) "
            For Each Row As DataRow In DataBarcode
                If Row.Item("Barcode") Is DBNull.Value Then
                    Continue For
                End If

                Barcode = CType(Row.Item("Barcode"), DataTable)
                For Each Item As DataRow In Barcode.Rows
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@SalesOrderPacking_index", SqlDbType.VarChar).Value = getSalesOrderPacking_Index '  Row.Item("SalesOrder_Index").ToString
                        .Add("@Barcode", SqlDbType.VarChar).Value = Item.Item("Barcode").ToString
                    End With

                    DBExeNonQuery(SQL, myTrans.Connection, myTrans)
                Next
            Next

            myTrans.Commit()

            Return True

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function

    Public Function getSalesOrderMainPacking(ByVal conditionSearch As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try
            strSQL &= " select tb_salesOrderpacking.SalesOrderPacking_Index,tb_salesOrderpacking.BarcodePacking as Barcode ,  tb_salesOrderpacking.[DateAdd] "
            strSQL &= " ,sum(tb_SalesOrderPackingItem.Total_Qty) as Qty   from tb_salesOrderpacking "
            strSQL &= " inner join tb_SalesOrderPackingItem on tb_salesOrderpacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index "
            strSQL &= " where tb_salesOrderpacking.status <> -1 "
            strSQL &= " AND ISNULL(tb_salesOrderpacking.IsSL, 0) = 0 "

            If Not String.IsNullOrEmpty(conditionSearch) Then
                strSQL &= conditionSearch
            End If
            strSQL &= " group by tb_salesOrderpacking.SalesOrderPacking_Index,tb_salesOrderpacking.BarcodePacking,  tb_salesOrderpacking.[DateAdd] "

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function

    Public Function getSalesOrderMainPackingSL(ByVal conditionSearch As String, ByVal pstrWhere As String) As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT '' AS SalesOrderPacking_Index, ISNULL(tb_SalesOrderPackingItem.Barcode_GROUP, '') AS Barcode, MAX(tb_SalesOrderPackingItem.group_date) AS [DateAdd] ")
                .Append("      , SUM(tb_SalesOrderPackingItem.Total_Qty) as Qty ")
                .Append(" FROM tb_salesOrderpacking ")
                .Append(" INNER JOIN tb_SalesOrderPackingItem ")
                .Append(" ON tb_salesOrderpacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index ")
                .Append(" WHERE tb_salesOrderpacking.status <> -1 ")
                .Append(" AND ISNULL(tb_salesOrderpacking.IsSL, 0) = 1 ")
                .Append(" AND ISNULL(tb_SalesOrderPackingItem.Barcode_GROUP, '') <> '' ")


                If Not String.IsNullOrEmpty(conditionSearch) Then
                    .Append(conditionSearch)
                End If

                .Append(" GROUP BY ISNULL(tb_SalesOrderPackingItem.Barcode_GROUP, '') ")
            End With


            Return DBExeQuery(SQL.ToString)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function getSalesOrderMainPackingBox(ByVal conditionSearch As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try
            strSQL &= " SELECT tb_SalesOrderPackingItem.Barcode_Box AS Barcode, MAX(tb_SalesOrderPackingItem.Update_Date) AS DateAdd "
            strSQL &= "      , SUM(tb_SalesOrderPackingItem.Total_Qty) AS Qty, tb_TransportManifest.TransportManifest_No  "
            strSQL &= " FROM tb_SalesOrderPackingItem "
            strSQL &= " INNER JOIN tb_SalesOrderPacking on tb_SalesOrderPacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index "
            strSQL &= " LEFT JOIN tb_TransportManifestItem on tb_SalesOrderPackingItem.SalesOrder_Index = tb_TransportManifestItem.SalesOrder_Index AND tb_TransportManifestItem.Status <> -1 "
            strSQL &= " LEFT JOIN tb_TransportManifest on tb_TransportManifestItem.TransportManifest_Index = tb_TransportManifest.TransportManifest_Index "
            strSQL &= " WHERE tb_SalesOrderPackingItem.Barcode_Box IS NOT NULL "

            If Not String.IsNullOrEmpty(conditionSearch) Then
                strSQL &= conditionSearch
            End If

            strSQL &= " GROUP BY tb_SalesOrderPackingItem.Barcode_Box, tb_TransportManifest.TransportManifest_No  "


            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function

    Public Function CancelSalesOrderPackingBag(ByVal SalesOrderPackingIndex As String) As Boolean
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()
        Try
            Dim SQL As System.Text.StringBuilder

            'Cancel Bag
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_SalesOrderPacking ")
                .Append(" SET Status = -1 ")
                .Append("   , Cancel_By = @Cancel_By ")
                .Append("   , Cancel_Date = GETDATE() ")
                .Append(" OUTPUT DELETED.Status ")
                .Append(" WHERE SalesOrderPacking_Index = @SalesOrderPacking_Index ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@SalesOrderPacking_index", SqlDbType.VarChar).Value = SalesOrderPackingIndex
                .Add("@Cancel_By", SqlDbType.VarChar).Value = WV_UserName
            End With

            Dim DeletedStatus As String = DBExeQuery_Scalar(SQL.ToString, Transaction.Connection, Transaction)
            Select Case DeletedStatus
                Case "-1"
                    Throw New Exception("ไม่สามารถยกเลิกถุงนี้ได้เนื่องจาก สถานะ ได้ถูก ยกเลิก ไปเรียบร้อยแล้ว")

                Case "2"
                    Throw New Exception("ไม่สามารถยกเลิกถุงนี้ได้เนื่องจาก สถานะ ได้ถูก แพคกล่อง ไปเรียบร้อยแล้ว")

                Case ""
                    Throw New Exception("ไม่สามารถยกเลิกถุงนี้ได้เนื่องจาก Index not found")
            End Select

            'Set IsPack
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_WithdrawItemLocation ")
                .Append(" SET IsPack = 0 ")
                .Append(" WHERE WithdrawItemLocation_Index IN (SELECT WithdrawItemLocation_Index FROM tb_SalesOrderPacking_Withdraw WHERE SalesOrderPacking_Index = @SalesOrderPacking_Index) ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@SalesOrderPacking_index", SqlDbType.VarChar).Value = SalesOrderPackingIndex
            End With

            DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)

            Transaction.Commit()
            Return True

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function CancelSalesOrderPackingBagSL(ByVal BarcodeGroup As String) As Boolean
        Try
            Dim SQL As System.Text.StringBuilder

            'Cancel Bag
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_SalesOrderPackingItem ")
                .Append(" SET Barcode_GROUP = NULL ")
                .Append("   , group_Date = NULL ")
                .Append(" WHERE Barcode_GROUP = @Barcode_GROUP ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Barcode_GROUP", SqlDbType.VarChar).Value = BarcodeGroup
            End With

            DBExeNonQuery(SQL.ToString)
            Return True

        Catch Ex As Exception

            Throw Ex
        End Try
    End Function

    Public Function CancelSalesOrderPackingBox(ByVal BarcodeBox As String) As Boolean
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()
        Try
            Dim SQL As System.Text.StringBuilder

            'Cancel Box
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_SalesOrderPackingItem ")
                .Append(" SET Barcode_Box = NULL ")
                .Append("   , Update_By = NULL ")
                .Append("   , Update_Date = NULL ")
                .Append(" OUTPUT INSERTED.SalesOrderPacking_Index ")
                .Append(" WHERE Barcode_Box = @Barcode_Box ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Barcode_Box", SqlDbType.VarChar).Value = BarcodeBox
            End With

            Dim UpdatedIndex As DataTable = DBExeQuery(SQL.ToString, Transaction.Connection, Transaction)
            If UpdatedIndex.Rows.Count > 0 Then
                SQL = New System.Text.StringBuilder
                With SQL
                    .Append(" UPDATE tb_SalesOrderPacking ")
                    .Append(" SET Status = 1 ")
                    .Append(" WHERE SalesOrderPacking_Index = @SalesOrderPacking_Index ")
                    .Append(" AND Status = 2 ")
                End With

                Using DataGroup As DataTable = UpdatedIndex.DefaultView.ToTable(True, "SalesOrderPacking_Index")
                    For Each Row As DataRow In DataGroup.Rows
                        With SQLServerCommand.Parameters
                            .Clear()

                            .Add("@SalesOrderPacking_Index", SqlDbType.VarChar).Value = Row.Item("SalesOrderPacking_Index").ToString
                        End With

                        DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
                    Next
                End Using
            Else
                Throw New Exception("ไม่สามารถยกเลิกรายการกล่องนี้ได้")
            End If

            Transaction.Commit()
            Return True

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function getSalesOrderMainPackingItem(ByVal index As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try
            strSQL &= " select tb_salesOrder.SalesOrder_No,  tb_salesOrderpacking.SalesOrderPacking_Index,tb_salesOrderpacking.BarcodePacking as BarcodeItem ,  tb_salesOrderpacking.[DateAdd] "
            strSQL &= " ,tb_SalesOrderPackingItem.Total_Qty  , ms_sku.str1 as Sku_Name   from tb_salesOrderpacking "
            strSQL &= " inner join tb_SalesOrderPackingItem on tb_salesOrderpacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index "

            strSQL &= " inner join ms_sku on ms_sku.sku_index = tb_SalesOrderPackingItem.sku_index "
            strSQL &= " inner join tb_salesOrder on tb_salesOrder.salesOrder_Index = tb_SalesOrderPackingItem.salesOrder_Index "

            strSQL &= " where tb_salesOrderpacking.status <> -1 "

            If Not String.IsNullOrEmpty(index) Then
                strSQL &= " and tb_salesOrderpacking.SalesOrderPacking_Index  =   '" & index & "'"
            End If

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function

    Public Function getSalesOrderMainPackingItemSL(ByVal BarcodeGroup As String) As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT tb_salesOrder.SalesOrder_No,  tb_salesOrderpacking.SalesOrderPacking_Index ")
                .Append(" 	 , tb_salesOrderpacking.BarcodePacking as BarcodeItem ")
                .Append(" 	 , tb_SalesOrderPackingItem.group_date AS [DateAdd] ")
                .Append(" 	 , tb_SalesOrderPackingItem.Total_Qty  ")
                .Append(" 	 , ms_sku.str1 as Sku_Name ")
                .Append(" FROM tb_salesOrderpacking ")
                .Append(" INNER JOIN tb_SalesOrderPackingItem ")
                .Append(" ON tb_salesOrderpacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index ")
                .Append(" INNER JOIN ms_sku on ms_sku.sku_index = tb_SalesOrderPackingItem.sku_index ")
                .Append(" INNER JOIN tb_salesOrder on tb_salesOrder.salesOrder_Index = tb_SalesOrderPackingItem.salesOrder_Index ")
                .Append(" WHERE tb_salesOrderpacking.status <> -1 ")
                .Append(" AND ISNULL(Barcode_GROUP, '') = @Barcode_GROUP ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Barcode_GROUP", SqlDbType.VarChar).Value = BarcodeGroup
            End With

            Return DBExeQuery(SQL.ToString)

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function getSalesOrderMainPackingItemBox(ByVal BarcodePacking As String, ByVal pstrWhere As String) As DataTable
        Dim strDocument_No As String = ""
        Dim strSQL As String = ""
        connectDB()

        Try
            strSQL &= " select SalesOrder_No , tb_SalesOrderPackingItem.SalesOrderPacking_Index,tb_SalesOrderPacking.[DateAdd] "
            strSQL &= " , Total_Qty ,  ms_sku.str1 as Sku_Name, (CASE WHEN ISNULL(tb_SalesOrderPacking.IsSL, 0) = 1 THEN tb_SalesOrderPackingItem.Barcode_Group ELSE tb_SalesOrderPackingItem.Barcode_BAG END) BarcodeItem    "
            strSQL &= " from tb_SalesOrderPackingItem "
            strSQL &= " inner join tb_SalesOrderPacking on tb_SalesOrderPacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index "
            strSQL &= " inner join tb_SalesOrder on tb_SalesOrder.SalesOrder_Index = tb_SalesOrderPackingItem.SalesOrder_Index "
            strSQL &= " inner join ms_sku on ms_sku.sku_index = tb_SalesOrderPackingItem.sku_index "
            strSQL &= " where Barcode_Box Is Not null  and  tb_SalesOrderPacking.status <>-1 "

            If Not String.IsNullOrEmpty(BarcodePacking) Then
                strSQL &= " and tb_SalesOrderPackingItem.Barcode_Box  =   '" & BarcodePacking & "'"
            End If

            Return DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            strSQL = Nothing
        End Try
    End Function
    Public Function GetReportInfo(ByVal pstrReportName As String, ByVal pstrWhere As String) As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim ods As New DataSet
        Dim clsPacking As New Tb_Packing_TopCharoen
        odt = clsPacking.GetDataReport(pstrReportName)

        For i As Integer = 0 To odt.Rows.Count - 1

            If odt.Rows(i)("IsVisible") = 1 Then
                oCrystal.Load(WV_Report_Path & odt.Rows(i)("Report_Path").ToString)
            End If

            ods.Tables.Add(clsPacking.GetReportData(odt.Rows(i)("View_Name").ToString, pstrWhere))

            ods.DataSetName = odt.Rows(i)("DataSet_Name").ToString
            ods.Tables(i).TableName = odt.Rows(i)("DataTable_Name").ToString

            oCrystal.SetDataSource(ods)
        Next

        Return oCrystal

    End Function


    Public Function GetReportInfoST_G(ByVal pstrReportName As String, ByVal pstrWhere As String) As CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim odt As New DataTable
        Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Dim ods As New DataSet
        Dim clsPacking As New Tb_Packing_TopCharoen

        oCrystal.Load(WV_Report_Path & "\Report\TopCharoen\rptSendProductST_G.rpt")

        Dim frem As DataTable
        frem = clsPacking.GetReportData("View_Rpt_ST_G", "")
        'For i As Integer = 1 To 5
        Dim dtrow As DataRow
        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(1).Item("Document_No") = "Test01"
        frem.Rows(1).Item("Sku_Description") = "SKU20"


        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(2).Item("Document_No") = "Test01"
        frem.Rows(2).Item("Sku_Description") = "SKU21"

        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(3).Item("Document_No") = "Test02"
        frem.Rows(3).Item("Sku_Description") = "SKU22"


        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(4).Item("Document_No") = "Test02"
        frem.Rows(4).Item("Sku_Description") = "SKU23"


        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(5).Item("Document_No") = "Test02"
        frem.Rows(5).Item("Sku_Description") = "SKU24"


        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(6).Item("Document_No") = "Test02"
        frem.Rows(6).Item("Sku_Description") = "SKU25"


        dtrow = frem.NewRow()
        frem.Rows.Add(dtrow)
        frem.Rows(7).Item("Document_No") = "Test03"
        frem.Rows(7).Item("Sku_Description") = "SKU26"
        '    Next


        '   ods.Tables.Add(clsPacking.GetReportData("View_Rpt_ST_G", ""))
        ods.Tables.Add(frem)
        ods.Tables.Add(clsPacking.GetReportData("View_Rpt_Sub_ST_G", ""))

        ods.DataSetName = "ds_SendProductST_G"
        ods.Tables(0).TableName = "View_Rpt_ST_G"
        ods.Tables(1).TableName = "View_Rpt_Sub_ST_G"

        oCrystal.SetDataSource(ods)

        Return oCrystal

    End Function

End Class
