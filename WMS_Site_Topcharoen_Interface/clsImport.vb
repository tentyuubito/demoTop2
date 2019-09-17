Imports System
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Xml
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration
Imports System.Threading
Imports WMS_STD_Formula

Public Class clsImport : Inherits DBType_SQLServer

    Public Function GetSoItemQty() As DataTable

        Dim strSQL As String = ""

        Try
            strSQL = " SELECT so.SalesOrder_No, so.SalesOrder_Index, soi.Sku_Index,SUM(ISNULL(soi.Total_Qty,0) - ISNULL(soi.Total_Qty_Withdraw,0)) AS Total_Qty,ISNULL(pc.ProductClass_Id,'') AS ProductClass_Id  "
            strSQL &= " FROM tb_SalesOrderItem soi "
            strSQL &= " INNER JOIN tb_SalesOrder so ON soi.SalesOrder_Index = so.SalesOrder_Index "
            strSQL &= " INNER JOIN ms_SKU sku ON soi.Sku_Index = sku.Sku_Index "
            strSQL &= " LEFT JOIN ms_Product_Class pc ON sku.ProductClass_Index = pc.ProductClass_Index "
            strSQL &= " WHERE so.Status <> -1 AND SO_type IS NULL "
            strSQL &= " GROUP BY so.SalesOrder_No, so.SalesOrder_Index,soi.SalesOrderItem_Index,soi.Sku_Index,pc.ProductClass_Id "

            Dim _dt As DataTable = DBExeQuery(strSQL)

            Return _dt
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetSKUBalance() As DataTable

        Dim strSQL As String = ""

        Try
            strSQL = " SELECT SKU.Sku_Index, ISNULL(BL.Total_Qty, 0) AS Balance_Total_Qty, ISNULL(SO.Total_Qty, 0) AS SO_Total_Qty , 0 AS Qty "
            strSQL &= " FROM ms_Sku SKU "
            strSQL &= " LEFT JOIN ( "
            strSQL &= " 	SELECT soi.Sku_Index  "
            strSQL &= " 	, SUM(ISNULL(soi.Total_Qty,0) - ISNULL(soi.Total_Qty_Withdraw,0)) AS Total_Qty "
            strSQL &= " 	FROM tb_SalesOrder so (NOLOCK) "
            strSQL &= " 	INNER JOIN tb_SalesOrderItem soi (NOLOCK) ON so.SalesOrder_Index = soi.SalesOrder_Index "
            strSQL &= " 	WHERE so.Status <> -1 AND SO_type IS NULL "
            strSQL &= " 	GROUP BY soi.Sku_Index "
            strSQL &= " ) SO "
            strSQL &= " ON SKU.Sku_Index = SO.Sku_Index "
            strSQL &= " LEFT JOIN ( "
            strSQL &= " 	SELECT Sku_Index , SUM(ISNULL(lb.Qty_Bal,0) - ISNULL(lb.ReserveQty,0)) AS Total_Qty "
            strSQL &= " 	FROM tb_LocationBalance lb "
            strSQL &= " 	GROUP BY lb.Sku_Index "
            strSQL &= " ) BL "
            strSQL &= " ON SKU.Sku_Index = BL.Sku_Index "
            strSQL &= " WHERE SO.Sku_Index IS NOT NULL OR BL.Sku_Index IS NOT NULL "

            Dim _dt As DataTable = DBExeQuery(strSQL)
            Return _dt
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function InsertDataPurchaseOrder(ByVal SalesOrder_Index As String) As Boolean
        DBconnect()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            Dim RowAffected As Integer
            Dim AutoIndex As New Sy_AutoNumber

            Dim Supplier_Index As String = "0010000000001" 'Fix
            Dim DocumentType_Index As String = "0010000000006" 'Fix
            Dim PurchaseOrder_Index As String = AutoIndex.getSys_Value("PurchaseOrder_Index")
            'Dim PurchaseOrder_No = New Sy_AutoyyyyMM().Auto_DocumentType_Number(Connection, myTrans, "0010000000006", Date.Now, "")

            'Insert PurchaseOrder
            Dim strSQL As String = " INSERT INTO [dbo].[tb_PurchaseOrder]  "
            strSQL &= " ( "
            strSQL &= " 	   [PurchaseOrder_Index],[PurchaseOrder_No],[PurchaseOrder_Date],[Carrier_Index] "
            strSQL &= "       ,[Customer_Receive_Location_Index],[Expected_Delivery_Date],[Delivery_Date] "
            strSQL &= "       ,[Customer_Index],[Supplier_Index],[Department_Index],[DocumentType_Index] "
            strSQL &= "       ,[Remark],[Credit_Term],[Currency_Index],[Exchange_Rate],[PaymentMethod_Index] "
            strSQL &= "       ,[Payment_Ref],[FullPaid_Date] "
            strSQL &= "       ,[Amount],[Discount_Percent],[Discount_Amt] "
            strSQL &= "       ,[Deposit_Amt],[Total_Amt] "
            strSQL &= "       ,[VAT_Percent],[VAT],[Net_Amt] "
            strSQL &= "       ,[Str1],[Str2],[Str3],[Str4],[Str5] "
            strSQL &= " 	  ,[Str6],[Str7],[Str8],[Str9],[Str10] "
            strSQL &= "       ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] "
            strSQL &= "       ,[add_by],[add_date],[add_branch] "
            strSQL &= "       ,[Status],[SalesUnit],[DistributionCenter_Index] "
            strSQL &= " )		 "
            strSQL &= " ( "
            strSQL &= " SELECT @PurchaseOrder_Index, [SalesOrder_No],[SalesOrder_Date],[Carrier_Index] "
            strSQL &= " 	  ,[Customer_Receive_Location_Index],[Expected_Delivery_Date],[Delivery_Date] "
            strSQL &= " 	  ,[Customer_Index], @Supplier_Index, [Department_Index], @DocumentType_Index "
            strSQL &= " 	  ,[Remark],[Credit_Term],[Currency_Index],[Exchange_Rate],[PaymentMethod_Index] "
            strSQL &= " 	  ,[Payment_Ref],[FullPaid_Date] "
            strSQL &= " 	  ,[Amount],[Discount_Percent],[Discount_Amt] "
            strSQL &= " 	  ,[Deposit_Amt],[Total_Amt] "
            strSQL &= " 	  ,[VAT_Percent],[VAT],[Net_Amt] "
            strSQL &= "       ,[Str1],[Str2],[Str3],[Str4],[Str5] "
            strSQL &= "       ,[Str6],[Str7],[Str8],[Str9],[SalesOrder_Index] "
            strSQL &= "       ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] "
            strSQL &= "       ,[add_by],GETDATE(),[add_branch] "
            strSQL &= "       ,1,[SalesUnit],[DistributionCenter_Index] "
            strSQL &= " FROM [dbo].[tb_SalesOrder] "
            strSQL &= " WHERE [SalesOrder_Index] = @SalesOrder_Index "

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@PurchaseOrder_Index", SqlDbType.VarChar).Value = PurchaseOrder_Index
                .Add("@Supplier_Index", SqlDbType.VarChar).Value = Supplier_Index
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = DocumentType_Index
                .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
            End With

            RowAffected = DBExeNonQuery(strSQL, myTrans.Connection, myTrans)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล PurchaseOrder")
            End If

            'Insert PurchaseOrderItem
            Dim strSQLPOItem As String = " INSERT INTO [dbo].[tb_PurchaseOrderItem]   "
            strSQLPOItem &= " (	   [PurchaseOrderItem_Index],[Item_Seq],[PurchaseOrder_Index] "
            strSQLPOItem &= "       ,[Sku_Index],[Package_Index],[Ratio] "
            strSQLPOItem &= "       ,[Total_Qty],[Qty] "
            strSQLPOItem &= "       ,[Qty_WithDraw],[Weight],[Volume],[Serial_No] "
            strSQLPOItem &= "       ,[Total_Received_Qty],[Received_Qty] "
            strSQLPOItem &= "       ,[Received_Weight],[Received_Volume] "
            strSQLPOItem &= "       ,[Last_Received_Date],[UnitPrice],[Amount] "
            strSQLPOItem &= "       ,[Currency_Index],[Discount_Amt],[Total_Amt] "
            strSQLPOItem &= "       ,[Status],[Remark],[Reason],[Ref_No1],[Ref_No2] "
            strSQLPOItem &= "       ,[Str1],[Str2],[Str3],[Str4],[Str5] "
            strSQLPOItem &= " 	    ,[Str6],[Str7],[Str8],[Str9],[Str10] "
            strSQLPOItem &= "       ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] "
            strSQLPOItem &= "       ,[add_by],[add_date],[add_branch] "
            strSQLPOItem &= "       ,[Charge_Status] "
            strSQLPOItem &= "       ,[Percent_Over_Allow],[Percent_Under_Allow],[PurchaseOrder_PR_Index] "
            strSQLPOItem &= " ) "
            strSQLPOItem &= " ( "
            strSQLPOItem &= " SELECT dbo.fn_IncSysValue((SELECT Sys_Value FROM Sy_AutoNumber WHERE Sys_Key = 'PurchaseOrderItem_Index'), ROW_NUMBER() OVER(ORDER BY Item_seq)),[Item_Seq],@PurchaseOrder_Index "
            strSQLPOItem &= " 	  ,[Sku_Index],[Package_Index],[Ratio] "
            strSQLPOItem &= "     ,[Total_Qty],[Qty] "
            strSQLPOItem &= " 	  ,0,[Weight],[Volume],[Serial_No] "
            strSQLPOItem &= " 	  ,0,0 "
            strSQLPOItem &= " 	  ,0,0 "
            strSQLPOItem &= " 	  ,NULL,[UnitPrice],[Amount] "
            strSQLPOItem &= "     ,[Currency_Index],[Discount_Amt],[Total_Amt] "
            strSQLPOItem &= "     ,[Status],[Remark],[Reason],[Ref_No1],[Ref_No2] "
            strSQLPOItem &= " 	  ,[Str1],[Str2],[Str3],[Str4],[Str5] "
            strSQLPOItem &= "     ,[Str6],[Str7],[Str8],ERP_Location,[Str10] "
            strSQLPOItem &= "     ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] "
            strSQLPOItem &= "     ,[add_by],GETDATE(),[add_branch] "
            strSQLPOItem &= "     ,[Charge_Status] "
            strSQLPOItem &= " 	  ,0,0,NULL "
            strSQLPOItem &= " FROM [dbo].[tb_SalesOrderItem] "
            strSQLPOItem &= " WHERE SalesOrder_Index = @SalesOrder_Index "
            strSQLPOItem &= " AND Status <> -1 "
            strSQLPOItem &= " )  "

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@PurchaseOrder_Index", SqlDbType.VarChar).Value = PurchaseOrder_Index
                .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
            End With

            RowAffected = DBExeNonQuery(strSQLPOItem, myTrans.Connection, myTrans)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล PurchaseOrderItem")
            End If

            Dim strSqlUpdateNum As String = " UPDATE Sy_AutoNumber WITH (ROWLOCK) "
            strSqlUpdateNum &= " SET Sys_Value = dbo.fn_IncSysValue((SELECT Sys_Value FROM Sy_AutoNumber WHERE Sys_Key = 'PurchaseOrderItem_Index'), " & RowAffected & ")"
            strSqlUpdateNum &= " WHERE Sys_Key = 'PurchaseOrderItem_Index'"

            RowAffected = DBExeNonQuery(strSqlUpdateNum, myTrans.Connection, myTrans)

            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล PurchaseOrderItem_Index")
            End If

            myTrans.Commit()
            Return True

        Catch Ex As Exception
            myTrans.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function UpdateSOType(ByVal SalesOrder_Index As String, ByVal SO_Type As String, ByVal So_No As String) As Boolean
        Dim strSQL As String = ""
        Try

            strSQL = " UPDATE tb_SalesOrder "
            strSQL &= " SET SO_Type = '" & SO_Type & "'"

            If SO_Type = "X" Then
                strSQL &= " , Status = 2 "
            End If

            strSQL &= " WHERE SalesOrder_Index ='" & SalesOrder_Index & "'"
            strSQL &= " AND SO_Type IS NULL "

            DBExeNonQuery(strSQL)

            If SO_Type.ToUpper() = "Y" Or SO_Type.ToUpper() = "Z" Then

                strSQL = ""
                strSQL = " UPDATE tb_SalesOrderItem "
                strSQL &= " SET Erp_Location = '" & So_No & "'"
                strSQL &= " WHERE SalesOrder_Index ='" & SalesOrder_Index & "'"
                '     strSQL &= " AND SO_Type IS NULL "

                DBExeNonQuery(strSQL)

            End If



        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub UpdateSOTypeWithAutoPurchaseOrder(ByVal SalesOrder_Index As String, ByVal SO_Type As String)
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()

        Try
            Dim RowAffected As Integer
            Dim SQL As System.Text.StringBuilder

            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE tb_SalesOrder ")
                .Append(" SET SO_Type = @SO_Type ")
                .Append("   , Status = (CASE WHEN @SO_Type = 'X' THEN '2' ELSE Status END) ")
                .Append(" WHERE SalesOrder_Index = @SalesOrder_Index ")
                .Append(" AND SO_Type IS NULL ")
                .Append(" AND Status NOT IN (-1, 3) ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@SO_Type", SqlDbType.VarChar).Value = SO_Type
                .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
            End With

            RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล SalesOrder Type")
            End If

            'Type Y = Auto PurchaseOrder
            If True Then ' SO_Type <> "Y" Then
                Transaction.Commit()
                Exit Sub
            End If

            Dim AutoIndex As New Sy_AutoNumber

            Dim Supplier_Index As String = "0010000000001" 'Fix
            Dim DocumentType_Index As String = "0010000000006" 'Fix
            Dim PurchaseOrder_Index As String = AutoIndex.getSys_Value("PurchaseOrder_Index")
            'Dim PurchaseOrder_No = New Sy_AutoyyyyMM().Auto_DocumentType_Number(Connection, Transaction, "0010000000006", Date.Now, "")

            'Insert PurchaseOrder
            SQL = New System.Text.StringBuilder
            With SQL
                .Append("  INSERT INTO [dbo].[tb_PurchaseOrder]  ")
                .Append(" ( ")
                .Append(" 	   [PurchaseOrder_Index],[PurchaseOrder_No],[PurchaseOrder_Date],[Carrier_Index] ")
                .Append("       ,[Customer_Receive_Location_Index],[Expected_Delivery_Date],[Delivery_Date] ")
                .Append("       ,[Customer_Index],[Supplier_Index],[Department_Index],[DocumentType_Index] ")
                .Append("       ,[Remark],[Credit_Term],[Currency_Index],[Exchange_Rate],[PaymentMethod_Index] ")
                .Append("       ,[Payment_Ref],[FullPaid_Date] ")
                .Append("       ,[Amount],[Discount_Percent],[Discount_Amt] ")
                .Append("       ,[Deposit_Amt],[Total_Amt] ")
                .Append("       ,[VAT_Percent],[VAT],[Net_Amt] ")
                .Append("       ,[Str1],[Str2],[Str3],[Str4],[Str5] ")
                .Append(" 	    ,[Str6],[Str7],[Str8],[Str9],[Str10] ")
                .Append("       ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] ")
                .Append("       ,[add_by],[add_date],[add_branch] ")
                .Append("       ,[Status],[SalesUnit],[DistributionCenter_Index] ")
                .Append(" )		 ")
                .Append(" ( ")
                .Append(" SELECT @PurchaseOrder_Index, [SalesOrder_No],[SalesOrder_Date],[Carrier_Index] ")
                .Append(" 	    ,[Customer_Receive_Location_Index],[Expected_Delivery_Date],[Delivery_Date] ")
                .Append(" 	    ,[Customer_Index], @Supplier_Index, [Department_Index], @DocumentType_Index ")
                .Append(" 	    ,[Remark],[Credit_Term],[Currency_Index],[Exchange_Rate],[PaymentMethod_Index] ")
                .Append(" 	    ,[Payment_Ref],[FullPaid_Date] ")
                .Append(" 	    ,[Amount],[Discount_Percent],[Discount_Amt] ")
                .Append(" 	    ,[Deposit_Amt],[Total_Amt] ")
                .Append(" 	    ,[VAT_Percent],[VAT],[Net_Amt] ")
                .Append("       ,[Str1],[Str2],[Str3],[Str4],[Str5] ")
                .Append("       ,[Str6],[Str7],[Str8],[Str9],[SalesOrder_Index] ")
                .Append("       ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] ")
                .Append("       ,[add_by],GETDATE(),[add_branch] ")
                .Append("       ,1,[SalesUnit],[DistributionCenter_Index] ")
                .Append(" FROM [dbo].[tb_SalesOrder] ")
                .Append(" WHERE [SalesOrder_Index] = @SalesOrder_Index ")
                .Append(" ) ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@PurchaseOrder_Index", SqlDbType.VarChar).Value = PurchaseOrder_Index
                .Add("@Supplier_Index", SqlDbType.VarChar).Value = Supplier_Index
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = DocumentType_Index
                .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
            End With

            RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล PurchaseOrder")
            End If

            'Insert PurchaseOrderItem
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" INSERT INTO [dbo].[tb_PurchaseOrderItem] ")
                .Append(" (	   [PurchaseOrderItem_Index],[Item_Seq],[PurchaseOrder_Index] ")
                .Append("       ,[Sku_Index],[Package_Index],[Ratio] ")
                .Append("       ,[Total_Qty],[Qty] ")
                .Append("       ,[Qty_WithDraw],[Weight],[Volume],[Serial_No] ")
                .Append("       ,[Total_Received_Qty],[Received_Qty] ")
                .Append("       ,[Received_Weight],[Received_Volume] ")
                .Append("       ,[Last_Received_Date],[UnitPrice],[Amount] ")
                .Append("       ,[Currency_Index],[Discount_Amt],[Total_Amt] ")
                .Append("       ,[Status],[Remark],[Reason],[Ref_No1],[Ref_No2] ")
                .Append("       ,[Str1],[Str2],[Str3],[Str4],[Str5] ")
                .Append(" 	    ,[Str6],[Str7],[Str8],[Str9],[Str10] ")
                .Append("       ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] ")
                .Append("       ,[add_by],[add_date],[add_branch] ")
                .Append("       ,[Charge_Status] ")
                .Append("       ,[Percent_Over_Allow],[Percent_Under_Allow],[PurchaseOrder_PR_Index] ")
                .Append(" ) ")
                .Append(" ( ")
                .Append(" SELECT dbo.fn_IncSysValue((SELECT Sys_Value FROM Sy_AutoNumber WHERE Sys_Key = 'PurchaseOrderItem_Index'), ROW_NUMBER() OVER(ORDER BY Item_seq)),[Item_Seq],@PurchaseOrder_Index ")
                .Append(" 	  ,[Sku_Index],[Package_Index],[Ratio] ")
                .Append("     ,[Total_Qty],[Qty] ")
                .Append(" 	  ,0,[Weight],[Volume],[Serial_No] ")
                .Append(" 	  ,0,0 ")
                .Append(" 	  ,0,0 ")
                .Append(" 	  ,NULL,[UnitPrice],[Amount] ")
                .Append("     ,[Currency_Index],[Discount_Amt],[Total_Amt] ")
                .Append("     ,[Status],[Remark],[Reason],[Ref_No1],[Ref_No2] ")
                .Append(" 	  ,[Str1],[Str2],[Str3],[Str4],[Str5] ")
                .Append("     ,[Str6],[Str7],[Str8],[Str9],[Str10] ")
                .Append("     ,[Flo1],[Flo2],[Flo3],[Flo4],[Flo5] ")
                .Append("     ,[add_by],GETDATE(),[add_branch] ")
                .Append("     ,[Charge_Status] ")
                .Append(" 	  ,0,0,NULL ")
                .Append(" FROM [dbo].[tb_SalesOrderItem] ")
                .Append(" WHERE SalesOrder_Index = @SalesOrder_Index ")
                .Append(" AND Status <> -1 ")
                .Append(" )  ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@PurchaseOrder_Index", SqlDbType.VarChar).Value = PurchaseOrder_Index
                .Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
            End With

            RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล PurchaseOrderItem")
            End If

            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE Sy_AutoNumber WITH (ROWLOCK) ")
                .Append(" SET Sys_Value = dbo.fn_IncSysValue((SELECT Sys_Value FROM Sy_AutoNumber WHERE Sys_Key = 'PurchaseOrderItem_Index'), @RowAffected) ")
                .Append(" WHERE Sys_Key = 'PurchaseOrderItem_Index' ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@RowAffected", SqlDbType.BigInt).Value = RowAffected
            End With

            RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล PurchaseOrderItem_Index")
            End If

            Transaction.Commit()

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Sub

    Public Sub UpdateCheckSOType()
        Try
            Dim _dtSOItem As DataTable = GetSoItemQty()
            Dim _dtBalance As DataTable = GetSKUBalance()
            For Each col As DataColumn In _dtBalance.Columns
                col.ReadOnly = False
            Next
            If _dtSOItem.Rows.Count = 0 Then
                Exit Sub
            End If
            Using SO As DataTable = _dtSOItem.DefaultView.ToTable(True, "SalesOrder_Index", "SalesOrder_No")
                For Each SO_Row As DataRow In SO.Rows
                    Dim SalesOrder_Index As String = SO_Row.Item("SalesOrder_Index").ToString
                    Dim SalesOrder_No As String = SO_Row.Item("SalesOrder_No").ToString
                    Dim Item As DataRow() = _dtSOItem.Select("SalesOrder_Index = '" & SalesOrder_Index & "'")
                    Dim isFlagType As String = "Y"
                    Dim SOTotalQty As Decimal = 0
                    If Item.Length = 0 Then
                        'ถ้าใน SO ไม่มี Item ให้ออกไปดู SO ใบใหม่
                        Exit For
                    End If
                    For Each Item_Row As DataRow In Item
                        Dim ItemSkuIndex As String = Item_Row("Sku_Index").ToString()
                        Dim ItemQty As String = Item_Row("Total_Qty").ToString()
                        Dim ItemProductClass As String = Item_Row("ProductClass_Id").ToString()

                        If ItemProductClass = "SL02" _
                            Or ItemProductClass = "SL03" _
                            Or ItemProductClass = "SL04" _
                            Or ItemProductClass = "SL05" Then
                            isFlagType = "Z"
                            Exit For
                        Else
                            Dim balanceRow As DataRow() = _dtBalance.Select("Sku_Index = '" & ItemSkuIndex & "'")
                            Dim BalanceTotalQty As Decimal
                            Dim SumSOQty As Decimal

                            For Each row As DataRow In balanceRow
                                BalanceTotalQty = row.Item("Balance_Total_Qty")
                                SumSOQty = row.Item("Qty") + ItemQty
                                row.Item("Qty") = SumSOQty
                            Next

                            If BalanceTotalQty < SumSOQty Then
                                isFlagType = "Y"
                                Exit For
                            Else
                                isFlagType = "X"
                            End If
                        End If
                    Next
                    'ของใน Balance ไม่พอ ให้ Update SO_type เป็น Y พอเป็น X

                    If isFlagType.Length > 0 Then
                        UpdateSOType(SalesOrder_Index, isFlagType, SalesOrder_No)

                        Dim Response As String
                        Using Service As New OMS.OMSApi
                            Response = "" 'Service.ResponseOrderList(SalesOrder_No, isFlagType)
                        End Using

                        If isFlagType = "Y" Then
                            InsertDataPurchaseOrder(SalesOrder_Index)
                        End If
                    End If
                Next
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function GetSalesOrder() As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT so.SalesOrder_No, so.SalesOrder_Index, soi.Sku_Index, soi.ItemStatus_Index, soi.Total_Qty ")
                .Append(" 	   , CASE WHEN ISNULL(pc.Str1, '') = 'Z' THEN 1 ELSE 0 END IsCaseZ ")
                .Append(" FROM tb_SalesOrder so  ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT SalesOrder_Index, Sku_Index, ItemStatus_Index ")
                .Append(" 		 , SUM(Total_Qty) - SUM(ISNULL(Total_Qty_Withdraw, 0)) AS Total_Qty ")
                .Append(" 	FROM tb_SalesOrderItem ")
                .Append(" 	WHERE Status <> -1 ")
                .Append(" 	AND Total_Qty - ISNULL(Total_Qty_Withdraw, 0) > 0 ")
                .Append(" 	GROUP BY SalesOrder_Index, Sku_Index, ItemStatus_Index ")
                .Append(" ) soi ")
                .Append(" ON so.SalesOrder_Index = soi.SalesOrder_Index ")
                .Append(" LEFT JOIN ms_SKU sku ")
                .Append(" ON soi.Sku_Index = sku.Sku_Index ")
                .Append(" LEFT JOIN ms_SKU sku2 ")
                .Append(" ON sku.Str6 = sku2.Sku_Id ")
                .Append(" LEFT JOIN ms_Product_Class pc ")
                .Append(" ON sku2.ProductClass_Index = pc.ProductClass_Index ")
                .Append(" WHERE so.Status <> -1 ")
                .Append(" AND so.SO_type IS NULL ")
            End With

            Return DBExeQuery(SQL.ToString)

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSalesOrderItemBalance() As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT BL.Sku_Index, BL.ItemStatus_Index ")
                .Append("      , BL.Total_Qty AS Full_Total_Qty, (BL.Total_Qty - ISNULL(SO_X.Total_Qty, 0)) AS Total_Qty ")
                .Append(" FROM ( ")
                .Append(" 	SELECT Sku_Index, ItemStatus_Index ")
                .Append(" 		 , SUM(ISNULL(Qty_Bal,0)) - SUM(ISNULL(ReserveQty,0)) AS Total_Qty ")
                .Append(" 	FROM tb_LocationBalance WITH(NOLOCK) ")
                .Append(" 	WHERE Qty_Bal > 0 ")
                .Append(" 	AND ReserveQty >= 0 ")
                .Append("	AND Qty_Bal - ReserveQty > 0 ")
                .Append("	GROUP BY Sku_Index, ItemStatus_Index ")
                .Append(" ) BL ")

                .Append(" INNER JOIN ( ")
                .Append("   SELECT soi.Sku_Index, soi.ItemStatus_Index ")
                .Append("   FROM tb_SalesOrder so ")
                .Append("   INNER JOIN tb_SalesOrderItem soi ")
                .Append("   ON so.SalesOrder_Index = soi.SalesOrder_Index ")
                .Append("   WHERE so.SO_type IS NULL ")
                .Append("   AND so.Status <> -1 ")
                .Append("   AND soi.Status <> -1 ")
                .Append("   GROUP BY soi.Sku_Index, soi.ItemStatus_Index ")
                .Append(" ) SO ")
                .Append(" ON BL.Sku_Index = SO.Sku_Index ")
                .Append(" AND BL.ItemStatus_Index = SO.ItemStatus_Index ")

                .Append(" LEFT JOIN ( ")
                .Append(" 	SELECT soi.Sku_Index, soi.ItemStatus_Index ")
                .Append(" 		 , SUM(ISNULL(soi.Total_Qty,0)) - SUM(ISNULL(soi.Total_Qty_Withdraw,0)) AS Total_Qty ")
                .Append(" 	FROM tb_SalesOrder so WITH(NOLOCK) ")
                .Append(" 	INNER JOIN tb_SalesOrderItem soi WITH(NOLOCK) ")
                .Append(" 	ON so.SalesOrder_Index = soi.SalesOrder_Index ")
                .Append(" 	WHERE so.Status NOT IN (-1, 3) ")
                .Append(" 	AND ISNULL(so.SO_Type, '') = 'X' ")
                .Append(" 	AND soi.Status <> -1 ")
                .Append(" 	GROUP BY soi.Sku_Index, soi.ItemStatus_Index ")
                .Append(" ) SO_X ")
                .Append(" ON BL.Sku_Index = SO_X.Sku_Index ")
                .Append(" AND BL.ItemStatus_Index = SO_X.ItemStatus_Index ")
                .Append(" WHERE BL.Total_Qty - ISNULL(SO_X.Total_Qty, 0) > 0 ")
            End With

            Return DBExeQuery(SQL.ToString)

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Sub UpdateSOType()
        Try
            Dim DataSalesOrder As DataTable = GetSalesOrder()
            Dim DataSalesOrderBalance As DataTable = GetSalesOrderItemBalance()

            If DataSalesOrder.Rows.Count = 0 Then
                Exit Sub
            End If

            Using SO As DataTable = DataSalesOrder.DefaultView.ToTable(True, "SalesOrder_Index", "SalesOrder_No")
                Dim SO_Type, SalesOrder_Index, SalesOrder_No, SkuIndex, ItemStatusIndex As String
                Dim SalesOrderItem, SalesOrderBalance As DataRow()
                Dim SO_Total_Qty As Decimal

                Dim Response As String
                Dim CountX, CountZ As Integer

                For Each SO_Row As DataRow In SO.Rows

                    SalesOrder_Index = SO_Row.Item("SalesOrder_Index").ToString
                    SalesOrder_No = SO_Row.Item("SalesOrder_No").ToString

                    SalesOrderItem = DataSalesOrder.Select(String.Format("SalesOrder_Index = '{0}'", SalesOrder_Index))

                    SO_Type = "Y" 'Default SO Type
                    CountX = 0
                    CountZ = 0

                    For Each Item_Row As DataRow In SalesOrderItem
                        SkuIndex = Item_Row("Sku_Index").ToString
                        ItemStatusIndex = Item_Row("ItemStatus_Index").ToString
                        SO_Total_Qty = Decimal.Parse(Item_Row("Total_Qty").ToString)

                        If DataSalesOrderBalance.Rows.Count > 0 Then
                            DataSalesOrderBalance.Columns.Item("Total_Qty").ReadOnly = False

                            SalesOrderBalance = DataSalesOrderBalance.Select(String.Format("Sku_Index = '{0}' AND ItemStatus_Index = '{1}' AND Total_Qty >= '{2}'", SkuIndex, ItemStatusIndex, SO_Total_Qty))
                            If SalesOrderBalance.Length > 0 Then
                                For Each Row As DataRow In SalesOrderBalance
                                    If SO_Total_Qty = 0 Then
                                        Exit For
                                    End If

                                    If SO_Total_Qty > Row.Item("Total_Qty") Then
                                        SO_Total_Qty -= Row.Item("Total_Qty")
                                        Row.Item("Total_Qty") = 0
                                    Else
                                        Row.Item("Total_Qty") -= SO_Total_Qty
                                        SO_Total_Qty = 0
                                    End If
                                Next

                                CountX += 1
                            End If
                        End If

                        If Item_Row("IsCaseZ").ToString = "1" Then
                            CountZ += 1
                        End If
                    Next

                    If CountX = SalesOrderItem.Length Then
                        SO_Type = "X"
                    Else
                        If CountZ > 0 Then
                            SO_Type = "Z"
                        End If
                    End If

                    If SO_Type = "X" Then
                        Using Service As New OMS.OMSApi
                            Response = Service.ResponseOrderList(SalesOrder_No, SO_Type, "", "", "", "")
                            Thread.Sleep(100)
                        End Using
                    End If

                    Try
                        UpdateSOTypeWithAutoPurchaseOrder(SalesOrder_Index, SO_Type)

                    Catch Ex As Exception
                        Continue For
                    End Try
                Next
            End Using
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
