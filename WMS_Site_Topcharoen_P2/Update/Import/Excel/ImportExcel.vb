Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class ImportExcel
    Inherits DBType_SQLServer

    Private Const IMPORT_SO_PROCEDURE_NAME As String = "sp_Imports_SalesOrder_P2"
    Private Const IMPORT_SO_TABLE_NAME As String = "_Prepare_Imports_SalesOrder_P2"
    Private Const IMPORT_WITHDRAW_TABLE_NAME As String = "_Prepare_Imports_Withdraw"
    Private Const IMPORT_WITHDRAWORDER_TABLE_NAME As String = "_Prepare_Imports_WithdrawOrder"

    Public Enum eBalanceCondition
        Sku
        ItemStatus
        PLot
        LocationSku
        LocationItemStatus
        LocationPLot
    End Enum

    Public Function ImportSalesOrder(ByVal DataExcel As DataTable, ByVal DocumentTypeIndex As String, ByVal SAO_Type As String) As DataTable
        DBconnect()
        '      Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction

        Try
            Dim Data As DataTable = DataExcel.Copy
            Dim ImportGuID As String = Guid.NewGuid.ToString

            With Data.Columns
                With .Add("GuID", GetType(String))
                    .Expression = "'" & ImportGuID & "'"
                    .SetOrdinal(0)
                End With

                With .Add("Message", GetType(String))
                    .Expression = String.Empty
                    .SetOrdinal(1)
                End With

                With .Add("IsHeader", GetType(Boolean))
                    .Expression = False
                    .SetOrdinal(2)
                End With

                With .Add("Add_Date", GetType(String))
                    .Expression = "'" & Now.ToString("dd-MM-yyyy hh:mm:ss") & "'"
                    .SetOrdinal(3)
                End With

                With .Add("DocumentType_Index", GetType(String))
                    .Expression = "'" & DocumentTypeIndex & "'"
                    .SetOrdinal(3)
                End With

            End With

            Using MyBulkCopy As New SqlClient.SqlBulkCopy(Me.Connection)
                With MyBulkCopy
                    With .ColumnMappings
                        .Clear()
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("GuID", "GuID"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Message", "Message"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("IsHeader", "IsHeader"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Add_Date", "Add_Date"))


                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Seq", "Seq"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Job_Type", "Job_Type"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Order_No", "Order_No"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Order_Date", "Order_Date"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Expected_Date", "Expected_Date"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Customer_Shipping_ID", "Customer_Shipping_ID"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Customer_Shipping_Name", "Customer_Shipping_Name"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Sender_ID", "Sender_ID"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Owner", "Owner"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Customer_Shipping_Location_ID", "Customer_Shipping_Location_ID"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Customer_Shipping_Location_Name", "Customer_Shipping_Location_Name"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Route", "Route"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Tran_Group", "Tran_Group"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Tran_Type", "Tran_Type"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Ref_no", "Ref_no"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("SKU_ID", "SKU_ID"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Product_Description", "Product_Description"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Qty", "Qty"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("UOM", "UOM"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Itemcode", "Itemcode"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Remark", "Remark"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Lot", "Lot"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("ERP", "ERP"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("DocumentType_Index", "DocumentType_Index"))
                       

                    End With

                    .DestinationTableName = IMPORT_SO_TABLE_NAME
                    .WriteToServer(Data)
                    .Close()
                End With
            End Using

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@GuID", SqlDbType.VarChar).Value = ImportGuID
                .Add("@Add_By", SqlDbType.VarChar).Value = WV_UserName
            End With

            Dim QueryProcedure As String = String.Format(" EXEC {0} @GuID, @Add_By ", IMPORT_SO_PROCEDURE_NAME)
            Dim dtResult As New DataTable

            dtResult = DBExeQuery(QueryProcedure)
            If dtResult.Rows(0).Item(0) = "S" Then
                Return Nothing
            End If
            dtResult.Columns.RemoveAt(dtResult.Columns("DocumentType_Index").Ordinal)
            dtResult.Columns.RemoveAt(dtResult.Columns("Guid").Ordinal)
            dtResult.Columns.RemoveAt(dtResult.Columns("IsHeader").Ordinal)
            dtResult.Columns.RemoveAt(dtResult.Columns("Add_Date").Ordinal)

            Return dtResult

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function SaveWithdrawOrder(ByVal DataExcel As DataTable, ByVal WithdrawDocumentTypeIndex As String, ByVal OrderDocumentTypeIndex As String) As String
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction

        Try
            If DataExcel Is Nothing OrElse Not DataExcel.Rows.Count > 0 Then
                Throw New Exception("ไม่พบข้อมูลที่จะบันทึก")
            End If

            SQLServerCommand.Parameters.Clear()
            DBExeNonQuery(String.Format(" TRUNCATE TABLE {0} ", IMPORT_WITHDRAWORDER_TABLE_NAME), Transaction.Connection, Transaction)

            Dim SQL, SQL_Balance As System.Text.StringBuilder
            Dim RowAffected As Integer

            ' Insert Data to Table Import
            Using BulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(Transaction.Connection, SqlClient.SqlBulkCopyOptions.Default, Transaction)
                With BulkCopy
                    .BulkCopyTimeout = 0
                    .DestinationTableName = IMPORT_WITHDRAWORDER_TABLE_NAME

                    With .ColumnMappings
                        .Clear()

                        .Add(New SqlClient.SqlBulkCopyColumnMapping("TAG_No", "TAG_No"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Sku_Id", "Sku_Id"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("PLot", "PLot"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Total_Qty", "Total_Qty"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("ItemStatus", "ItemStatus"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Location_Alias", "Location_Alias"))
                    End With

                    .WriteToServer(DataExcel)
                End With
            End Using

            ' Get Index Master
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE Import ")
                .Append(" SET Sku_Index = Master_Sku_Index ")
                .Append("   , Package_Index = Master_Package_Index ")
                .Append("   , ItemStatus_Index = Master_ItemStatus_Index ")
                .Append("   , Location_Index = Master_Location_Index ")
                .Append("   , LocationBalance_Index = Transaction_LocationBalance_Index ")
                .Append("   , Customer_Index = Transaction_Customer_Index ")
                .Append(" FROM ( ")
                .Append("   SELECT PPW.LocationBalance_Index, tb_LocationBalance.LocationBalance_Index As Transaction_LocationBalance_Index ")
                .Append("        , PPW.Customer_Index, tb_Order.Customer_Index As Transaction_Customer_Index ")
                .Append("        , PPW.Sku_Index, tb_LocationBalance.Sku_Index As Master_Sku_Index ")
                .Append("        , PPW.Package_Index, ms_Sku.Package_Index As Master_Package_Index ")
                .Append("        , PPW.ItemStatus_Index, tb_LocationBalance.ItemStatus_Index As Master_ItemStatus_Index ")
                .Append("   	 , PPW.Location_Index, tb_LocationBalance.Location_Index As Master_Location_Index ")
                '.Append("        , PPW.Sku_Index, ms_SKU.Sku_Index As Master_Sku_Index ")
                '.Append("        , PPW.ItemStatus_Index, ms_ItemStatus.ItemStatus_Index As Master_ItemStatus_Index ")
                '.Append("   	 , PPW.Location_Index, ms_Location.Location_Index As Master_Location_Index ")
                .Append(String.Format("   FROM {0} PPW ", IMPORT_WITHDRAWORDER_TABLE_NAME))
                .Append("   INNER JOIN tb_LocationBalance ")
                .Append("   ON PPW.TAG_No = tb_LocationBalance.TAG_No ")
                .Append("   AND PPW.PLot = tb_LocationBalance.PLot ")
                .Append("   INNER JOIN tb_Order ")
                .Append("   ON tb_LocationBalance.Order_Index = tb_Order.Order_Index ")
                .Append("   INNER JOIN ms_Sku ")
                .Append("   ON tb_LocationBalance.Sku_Index = ms_Sku.Sku_Index ")
                '.Append("   LEFT JOIN ms_SKU ")
                '.Append("   ON PPW.Sku_Id = ms_SKU.Sku_Id ")
                '.Append("   AND ms_SKU.status_id <> -1 ")
                '.Append("   LEFT JOIN ms_ItemStatus ")
                '.Append("   ON PPW.ItemStatus = ms_ItemStatus.ItemStatus_Id ")
                '.Append("   AND ms_ItemStatus.status_id <> -1 ")
                '.Append("   LEFT JOIN ms_Location ")
                '.Append("   ON PPW.Location_Alias = ms_Location.Location_Alias ")
                '.Append("   AND ms_Location.status_id <> -1 ")
                .Append("   WHERE tb_LocationBalance.Qty_Bal > 0 ")
                .Append(" ) Import ")
            End With

            SQLServerCommand.Parameters.Clear()
            RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ข้อมูล TAG ไม่ถูกต้อง")
            End If

            Dim RowArray As DataRow()
            SQLServerCommand.Parameters.Clear()
            Dim DataWithdraw As DataTable = DBExeQuery(String.Format(" SELECT * FROM {0} ", IMPORT_WITHDRAWORDER_TABLE_NAME), Transaction.Connection, Transaction)

            'Validate
            RowArray = DataWithdraw.Select("LocationBalance_Index IS NULL")
            If RowArray.Length > 0 Then
                Throw New Exception(String.Format("ไม่พบหมายเลข TAG [ {0} ] ในระบบ", RowArray(0).Item("TAG_No").ToString))
            End If

            Dim CustomerIndex As String
            Using DataCustomer As DataTable = DataWithdraw.DefaultView.ToTable(True, "Customer_Index")
                If DataCustomer.Rows.Count > 1 Then
                    Throw New Exception("ข้อมูล TAG มีมากกว่าหนั่งเจ้าของสินค้า")
                Else
                    CustomerIndex = DataCustomer.Rows.Item(0).Item("Customer_Index").ToString
                End If
            End Using

            If RowArray.Length > 0 Then
                Throw New Exception(String.Format("ไม่พบหมายเลข TAG [ {0} ] ในระบบ", RowArray(0).Item("TAG_No").ToString))
            End If

            Dim AutoIndex As New Sy_AutoNumber
            Dim AutoDocument As New Sy_DocumentNumber

            'Insert tb_Withdraw
            Dim WithdrawDate As Date = Date.Now.ToString("yyyy-MM-dd")
            Dim WithdrawIndex As String = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Withdraw_Index")
            Dim WithdrawNo As String = AutoDocument.Auto_DocumentType_Number(WithdrawDocumentTypeIndex, , WithdrawDate)
            Dim SqlInsertWithdraw As String = GetSqlInsertWithdraw()

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Withdraw_Index", SqlDbType.VarChar).Value = WithdrawIndex
                .Add("@Withdraw_No", SqlDbType.VarChar).Value = WithdrawNo
                .Add("@Withdraw_Date", SqlDbType.Date).Value = WithdrawDate
                .Add("@Departure_Date", SqlDbType.Date).Value = WithdrawDate
                .Add("@Arrival_Date", SqlDbType.Date).Value = WithdrawDate
                .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                .Add("@Shipper_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Department_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No3", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No4", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No5", SqlDbType.VarChar).Value = String.Empty
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = WithdrawDocumentTypeIndex
                .Add("@Contact_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Comment", SqlDbType.VarChar).Value = "Auto เบิกสินค้าเพื่อรับเข้าด้วยยอดใหม่จาก Excel"
                .Add("@Str1", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str2", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str3", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str4", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str5", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str6", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str7", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str8", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str9", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str10", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Flo1", SqlDbType.Float).Value = 0
                .Add("@Flo2", SqlDbType.Float).Value = 0
                .Add("@Flo3", SqlDbType.Float).Value = 0
                .Add("@Flo4", SqlDbType.Float).Value = 0
                .Add("@Flo5", SqlDbType.Float).Value = 0

                .Add("@Status", SqlDbType.Int).Value = 2

                .Add("@Customer_Shipping_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Driver_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Round", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Leave_Time", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Factory_In", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Factory_Out", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Return_Time", SqlDbType.SmallDateTime).Value = WithdrawDate

                .Add("@Container_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@VehicleType_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_From", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_To", SqlDbType.VarChar).Value = String.Empty
                .Add("@Checker_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Driver_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@ApprovedBy_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Container_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_InGate", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_OutGate", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_StartLoad", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_FinistLoad", SqlDbType.VarChar).Value = String.Empty

                .Add("@HandlingType_Index", SqlDbType.Int).Value = 1
                .Add("@Vassel_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Flight_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Vehicle_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_by", SqlDbType.VarChar).Value = String.Empty
                .Add("@Origin_Port_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Origin_Country_Id", SqlDbType.VarChar).Value = "TH"
                .Add("@Destination_Port_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Destination_Country_Id", SqlDbType.VarChar).Value = "TH"
                .Add("@Terminal_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@SO_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Invoice_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@ASN_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Withdraw_Type", SqlDbType.Bit).Value = 0
                .Add("@Confirm_Flag", SqlDbType.Int).Value = 0
                .Add("@User_UseDoc", SqlDbType.Bit).Value = 0

                .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With

            RowAffected = DBExeNonQuery(SqlInsertWithdraw, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูลใบเบิกได้")
            End If

            'Get LocationBalance
            SQL_Balance = New System.Text.StringBuilder
            With SQL_Balance
                .Append(" SELECT lc.Location_Alias, tg.Customer_Index, tg.Order_Date, sku.Product_Index, sku.ProductType_Index, lb.* ")
                .Append(" FROM tb_LocationBalance lb ")
                .Append(" INNER JOIN tb_TAG tg ")
                .Append(" ON lb.TAG_Index = tg.TAG_Index ")
                .Append(" INNER JOIN ms_Location lc ")
                .Append(" ON lb.Location_Index = lc.Location_Index ")
                .Append(" LEFT JOIN ( ")
                .Append(" 	SELECT sk.Sku_Index, pd.Product_Index, pdt.ProductType_Index ")
                .Append(" 	FROM ms_SKU sk ")
                .Append(" 	INNER JOIN ms_Product pd  ")
                .Append(" 	ON sk.Product_Index = pd.Product_Index ")
                .Append(" 	INNER JOIN ms_ProductType pdt ")
                .Append(" 	ON pd.ProductType_Index = pdt.ProductType_Index ")
                .Append(" ) sku ")
                .Append(" ON lb.Sku_Index = sku.Sku_Index ")
                .Append(" WHERE lb.LocationBalance_Index = @LocationBalance_Index ")
            End With

            'Insert Item
            Dim WithdrawItemIndex, WithdrawItemLocationIndex As String
            Dim SqlInsertWithdrawItem As String = GetSqlInsertWithdrawItem()
            Dim SqlWithdrawItemLocation As String = GetSqlInsertWithdrawItemLocation()
            Dim SqlUpdateLocationBalance As String = GetSqlUpdateLocationBalance()
            Dim SqlInsertTransaction As String = GetSqlInsertTransaction()

            Dim PendingTotalQty, RemainQty, Ratio As Decimal
            Dim QtyBalance, QtyBalanceBegin, QtyReserve, QtyRemain As Decimal
            Dim QtyWithdraw, WeightWithdraw, VolumeWithdraw, QtyItemWithdraw, OrderItemPriceWithdraw As Decimal
            Dim SkuBalance, ItemStatusBalance, PLotBalance, LocationSkuBalance, LocationItemStatusBalance, LocationPLotBalance As DataTable

            Dim LocationBalance As DataTable

            Dim CurrentRow As Integer = 0

            For Each Row As DataRow In DataWithdraw.Rows
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Row.Item("LocationBalance_Index").ToString
                End With

                'Validate LocationBalance
                LocationBalance = DBExeQuery(SQL_Balance.ToString, Transaction.Connection, Transaction)
                If LocationBalance Is Nothing OrElse Not LocationBalance.Rows.Count > 0 Then
                    Throw New Exception("ไม่พบข้อมูลสินค้าในระบบ")
                End If

                PendingTotalQty = LocationBalance.Compute("SUM(Qty_Bal)", "1=1")
                If Not PendingTotalQty > 0 Then
                    Continue For
                End If

                RemainQty = LocationBalance.Compute("SUM(Qty_Bal) - SUM(ReserveQty)", "1=1")
                If PendingTotalQty <> RemainQty Then
                    Throw New Exception(String.Format("ไม่สามารถเบิกได้ TAG [ {0} ] เนื่องจากมีการจองสินค้าอยู่", Row.Item("TAG_No").ToString))
                End If

                For Each Balance As DataRow In LocationBalance.Rows
                    CurrentRow += 1

                    'Fig ค่า Ratio เป็น 1 เสมอ เพราะเบิกด้วยยอด Total_Qty
                    Ratio = 1

                    If Not PendingTotalQty > 0 Then
                        Exit For
                    End If

                    QtyBalance = Decimal.Parse(Balance.Item("Qty_Bal").ToString)
                    QtyReserve = Decimal.Parse(Balance.Item("ReserveQty").ToString)
                    QtyBalanceBegin = Decimal.Parse(Balance.Item("Qty_Bal_Begin").ToString)
                    QtyRemain = QtyBalance - QtyReserve

                    If Not QtyRemain > 0 Then
                        Continue For
                    End If

                    If PendingTotalQty > QtyRemain Then
                        QtyWithdraw = QtyRemain
                    Else
                        QtyWithdraw = PendingTotalQty
                    End If

                    PendingTotalQty -= QtyWithdraw

                    WeightWithdraw = Math.Round((Decimal.Parse(Balance.Item("Weight_Bal_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)
                    VolumeWithdraw = Math.Round((Decimal.Parse(Balance.Item("Volume_Bal_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)
                    QtyItemWithdraw = Math.Round((Decimal.Parse(Balance.Item("Qty_Item_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)
                    OrderItemPriceWithdraw = Math.Round((Decimal.Parse(Balance.Item("OrderItem_Price_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)

                    'Update tb_LocationBalance
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Balance.Item("LocationBalance_Index").ToString
                        .Add("@Qty_Bal", SqlDbType.Decimal).Value = QtyWithdraw
                        .Add("@Weight_Bal", SqlDbType.Decimal).Value = WeightWithdraw
                        .Add("@Volume_Bal", SqlDbType.Decimal).Value = VolumeWithdraw
                        .Add("@Qty_Item_Bal", SqlDbType.Decimal).Value = QtyItemWithdraw
                        .Add("@OrderItem_Price_Bal", SqlDbType.Decimal).Value = OrderItemPriceWithdraw
                    End With

                    RowAffected = DBExeNonQuery(SqlUpdateLocationBalance, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล LocationBalance")
                    End If

                    WithdrawItemIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "WithdrawItem_Index")

                    'tb_WithdrawItem
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@WithdrawItem_Index", SqlDbType.VarChar).Value = WithdrawItemIndex
                        .Add("@Withdraw_Index", SqlDbType.VarChar).Value = WithdrawIndex
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Balance.Item("Sku_Index").ToString
                        .Add("@Package_Index", SqlDbType.VarChar).Value = Balance.Item("Package_Index").ToString
                        .Add("@PLot", SqlDbType.VarChar).Value = String.Empty
                        .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString
                        .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Qty", SqlDbType.Float).Value = Math.Round(QtyWithdraw / Ratio, 6)
                        .Add("@Ratio", SqlDbType.Float).Value = Ratio
                        .Add("@Total_Qty", SqlDbType.Float).Value = QtyWithdraw
                        .Add("@Weight", SqlDbType.Float).Value = WeightWithdraw
                        .Add("@Volume", SqlDbType.Float).Value = VolumeWithdraw
                        .Add("@Plan_Qty", SqlDbType.Float).Value = 0
                        .Add("@Plan_Total_Qty", SqlDbType.Float).Value = 0
                        .Add("@Str1", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str2", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str3", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str4", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str5", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Flo1", SqlDbType.Float).Value = 0
                        .Add("@Flo2", SqlDbType.Float).Value = 0
                        .Add("@Flo3", SqlDbType.Float).Value = 0
                        .Add("@Flo4", SqlDbType.Float).Value = 0
                        .Add("@Flo5", SqlDbType.Float).Value = 0

                        .Add("@Status", SqlDbType.Int).Value = 1

                        .Add("@ItemDefinition_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Charge_Status", SqlDbType.Int).Value = 0
                        .Add("@Item_Qty", SqlDbType.Float).Value = QtyItemWithdraw
                        .Add("@Price", SqlDbType.Float).Value = OrderItemPriceWithdraw
                        .Add("@Item_Package_Index", SqlDbType.NVarChar).Value = String.Empty

                        .Add("@Invoice_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@SO_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@PACKING_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Declaration_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@HandlingType_Index", SqlDbType.VarChar).Value = String.Empty

                        .Add("@Plan_Process", SqlDbType.Int).Value = -9

                        .Add("@DocumentPlan_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@AssetLocationBalance_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Tax1", SqlDbType.Float).Value = 0
                        .Add("@Tax2", SqlDbType.Float).Value = 0
                        .Add("@Tax3", SqlDbType.Float).Value = 0
                        .Add("@Tax4", SqlDbType.Float).Value = 0
                        .Add("@Tax5", SqlDbType.Float).Value = 0
                        .Add("@HS_Code", SqlDbType.VarChar).Value = String.Empty
                        .Add("@ItemDescription", SqlDbType.VarChar).Value = String.Empty

                        .Add("@Seq", SqlDbType.Int).Value = CurrentRow

                        .Add("@Consignee_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str6", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str7", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str8", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str9", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str10", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@OrderItem_Index", SqlDbType.VarChar).Value = Balance.Item("OrderItem_Index").ToString
                        .Add("@NewItemFlag", SqlDbType.Int).Value = 2
                        .Add("@ERP_Location", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Status_PickToLight", SqlDbType.Int).Value = 0

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    End With

                    RowAffected = DBExeNonQuery(SqlInsertWithdrawItem, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล WithdrawItem")
                    End If

                    WithdrawItemLocationIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "WithdrawItemLocation_Index")

                    'tb_WithdrawItemLocation
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@WithdrawItemLocation_Index", SqlDbType.VarChar).Value = WithdrawItemLocationIndex
                        .Add("@JobWithdraw_Index", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@Withdraw_Index", SqlDbType.VarChar).Value = WithdrawIndex
                        .Add("@WithdrawItem_Index", SqlDbType.VarChar).Value = WithdrawItemIndex
                        .Add("@Order_Index", SqlDbType.VarChar).Value = Balance.Item("Order_Index").ToString
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Balance.Item("Sku_Index").ToString
                        .Add("@Lot_No", SqlDbType.VarChar).Value = Balance.Item("Lot_No").ToString
                        .Add("@Plot", SqlDbType.VarChar).Value = Balance.Item("Plot").ToString
                        .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString
                        .Add("@Tag_No", SqlDbType.VarChar).Value = Balance.Item("Tag_No").ToString
                        .Add("@Tag_Index", SqlDbType.VarChar).Value = Balance.Item("Tag_Index").ToString
                        .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Balance.Item("LocationBalance_Index").ToString
                        .Add("@Location_Index", SqlDbType.VarChar).Value = Balance.Item("Location_Index").ToString
                        .Add("@Serial_No", SqlDbType.VarChar).Value = Balance.Item("Serial_No").ToString
                        .Add("@Qty", SqlDbType.VarChar).Value = Math.Round(QtyWithdraw / Ratio, 6) 'Decimal.Parse(Row.Item("Qty").ToString)
                        .Add("@Package_Index", SqlDbType.VarChar).Value = Balance.Item("Package_Index").ToString
                        .Add("@Total_Qty", SqlDbType.Float).Value = QtyWithdraw
                        .Add("@Weight", SqlDbType.Float).Value = WeightWithdraw
                        .Add("@Volume", SqlDbType.Float).Value = VolumeWithdraw
                        .Add("@Pallet_Qty", SqlDbType.Int).Value = 0
                        .Add("@Status", SqlDbType.Int).Value = -9
                        .Add("@Item_Qty", SqlDbType.Float).Value = QtyItemWithdraw
                        .Add("@Price", SqlDbType.Float).Value = OrderItemPriceWithdraw
                        .Add("@TagOut_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@ERP_Location", SqlDbType.VarChar).Value = String.Empty
                        .Add("@picking_by", SqlDbType.VarChar).Value = String.Empty
                        .Add("@picking_date", SqlDbType.SmallDateTime).Value = DBNull.Value

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    End With

                    RowAffected = DBExeNonQuery(SqlWithdrawItemLocation, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล WithdrawItemLocation")
                    End If

                    'Withdraw tb_Transaction
                    SQLServerCommand.Parameters.Clear()

                    SkuBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.Sku, Balance.Item("Sku_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    ItemStatusBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.ItemStatus, Balance.Item("ItemStatus_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    PLotBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.PLot, Balance.Item("PLot").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    LocationSkuBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationSku, Balance.Item("Location_Index").ToString, Balance.Item("Sku_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    LocationItemStatusBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationItemStatus, Balance.Item("Location_Index").ToString, Balance.Item("ItemStatus_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    LocationPLotBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationPLot, Balance.Item("Location_Index").ToString, Balance.Item("PLot").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)

                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@Transaction_Index", SqlDbType.VarChar).Value = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Transaction_Index")

                        .Add("@TAG_Index", SqlDbType.VarChar).Value = Balance.Item("TAG_Index").ToString
                        .Add("@Tag_No", SqlDbType.VarChar).Value = Balance.Item("Tag_No").ToString

                        .Add("@TAG_IndexNew", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@Tag_NoNew", SqlDbType.VarChar).Value = DBNull.Value

                        .Add("@Qty_In", SqlDbType.Float).Value = 0
                        .Add("@Weight_In", SqlDbType.Float).Value = 0
                        .Add("@Volume_In", SqlDbType.Float).Value = 0
                        .Add("@Qty_Out", SqlDbType.Float).Value = QtyWithdraw
                        .Add("@Weight_Out", SqlDbType.Float).Value = WeightWithdraw
                        .Add("@Volume_Out", SqlDbType.Float).Value = VolumeWithdraw

                        .Add("@Qty_Item_In", SqlDbType.Float).Value = 0
                        .Add("@Qty_Item_Out", SqlDbType.Float).Value = QtyItemWithdraw
                        .Add("@Qty_Item_Bal", SqlDbType.Float).Value = Decimal.Parse(Balance.Item("Qty_Item_Bal").ToString) - QtyItemWithdraw
                        .Add("@OrderItem_Price_In", SqlDbType.Float).Value = 0
                        .Add("@OrderItem_Price_Out", SqlDbType.Float).Value = OrderItemPriceWithdraw
                        .Add("@OrderItem_Price_Bal", SqlDbType.Float).Value = Decimal.Parse(Balance.Item("OrderItem_Price_Bal").ToString) - OrderItemPriceWithdraw

                        .Add("@Transaction_Id", SqlDbType.VarChar).Value = WithdrawNo
                        .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                        .Add("@Order_Index", SqlDbType.VarChar).Value = Balance.Item("Order_Index").ToString
                        .Add("@Order_Date", SqlDbType.SmallDateTime).Value = Balance.Item("Order_Date").ToString
                        .Add("@Transation_Date", SqlDbType.Date).Value = WithdrawDate
                        .Add("@OrderItem_Index", SqlDbType.VarChar).Value = Balance.Item("OrderItem_Index").ToString
                        .Add("@Product_Index", SqlDbType.VarChar).Value = Balance.Item("Product_Index").ToString
                        .Add("@ProductType_Index", SqlDbType.VarChar).Value = Balance.Item("ProductType_Index").ToString
                        .Add("@DocumentType_Index", SqlDbType.VarChar).Value = WithdrawDocumentTypeIndex
                        .Add("@Description", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@ItemDefinition_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Balance.Item("Sku_Index").ToString
                        .Add("@Lot_No", SqlDbType.VarChar).Value = Balance.Item("Lot_No").ToString
                        .Add("@PLot", SqlDbType.VarChar).Value = Balance.Item("PLot").ToString
                        .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString

                        .Add("@New_ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString

                        .Add("@Process_Id", SqlDbType.Int).Value = 2
                        .Add("@From_Location_Index", SqlDbType.VarChar).Value = Balance.Item("Location_Index").ToString
                        .Add("@To_Location_Index", SqlDbType.VarChar).Value = Balance.Item("Location_Index").ToString
                        .Add("@Location_Alias_To", SqlDbType.VarChar).Value = Balance.Item("Location_Alias").ToString
                        .Add("@Location_Alias_From", SqlDbType.VarChar).Value = Balance.Item("Location_Alias").ToString

                        If SkuBalance.Rows.Count > 0 Then
                            .Add("@Qty_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Qty").ToString
                            .Add("@Weight_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Weight").ToString
                            .Add("@Volume_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Volume").ToString
                        Else
                            .Add("@Qty_Sku_Bal", SqlDbType.Float).Value = 0
                            .Add("@Weight_Sku_Bal", SqlDbType.Float).Value = 0
                            .Add("@Volume_Sku_Bal", SqlDbType.Float).Value = 0
                        End If

                        If PLotBalance.Rows.Count > 0 Then
                            .Add("@Qty_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Qty").ToString
                            .Add("@Weight_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Weight").ToString
                            .Add("@Volume_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Volume").ToString
                        Else
                            .Add("@Qty_PLot_Bal", SqlDbType.Float).Value = 0
                            .Add("@Weight_PLot_Bal", SqlDbType.Float).Value = 0
                            .Add("@Volume_PLot_Bal", SqlDbType.Float).Value = 0
                        End If

                        If ItemStatusBalance.Rows.Count > 0 Then
                            .Add("@Qty_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Qty").ToString
                            .Add("@Weight_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Weight").ToString
                            .Add("@Volume_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Volume").ToString
                        Else
                            .Add("@Qty_ItemStatus_Bal", SqlDbType.Float).Value = 0
                            .Add("@Weight_ItemStatus_Bal", SqlDbType.Float).Value = 0
                            .Add("@Volume_ItemStatus_Bal", SqlDbType.Float).Value = 0
                        End If

                        .Add("@Move_Qty", SqlDbType.Float).Value = DBNull.Value
                        .Add("@Qty_Variance", SqlDbType.Float).Value = 0
                        .Add("@Referent_1", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Referent_2", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Item_Package_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Invoice_In", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Invoice_Out", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Pallet_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Declaration_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@PO_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@SO_NO", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty

                        If LocationSkuBalance.Rows.Count > 0 Then
                            .Add("@Qty_Sku_Location_Bal", SqlDbType.Float).Value = LocationSkuBalance.Rows.Item(0).Item("Qty").ToString
                        Else
                            .Add("@Qty_Sku_Location_Bal", SqlDbType.Float).Value = 0
                        End If

                        If LocationItemStatusBalance.Rows.Count > 0 Then
                            .Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float).Value = LocationItemStatusBalance.Rows.Item(0).Item("Qty").ToString
                        Else
                            .Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float).Value = 0
                        End If

                        If LocationPLotBalance.Rows.Count > 0 Then
                            .Add("@Qty_PLot_Location_Bal", SqlDbType.Float).Value = LocationPLotBalance.Rows.Item(0).Item("Qty").ToString
                        Else
                            .Add("@Qty_PLot_Location_Bal", SqlDbType.Float).Value = 0
                        End If

                        .Add("@HandlingType_Index", SqlDbType.VarChar).Value = 1
                        .Add("@Tax1_In", SqlDbType.Float).Value = 0
                        .Add("@Tax2_In", SqlDbType.Float).Value = 0
                        .Add("@Tax3_In", SqlDbType.Float).Value = 0
                        .Add("@Tax4_In", SqlDbType.Float).Value = 0
                        .Add("@Tax5_In", SqlDbType.Float).Value = 0
                        .Add("@Tax1_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax2_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax3_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax4_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax5_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax1_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax2_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax3_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax4_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax5_Bal", SqlDbType.Float).Value = 0
                        .Add("@ERP_Location_From", SqlDbType.VarChar).Value = Balance.Item("ERP_Location").ToString
                        .Add("@ERP_Location_TO", SqlDbType.VarChar).Value = Balance.Item("ERP_Location").ToString
                        .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = WithdrawIndex
                        .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = WithdrawItemIndex

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    End With

                    RowAffected = DBExeNonQuery(SqlInsertTransaction, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล Transaction")
                    End If
                Next
            Next

            'Insert tb_Order
            Dim OrderDate As Date = WithdrawDate
            Dim OrderTime As String = Date.Now.ToString("HH:mm")
            Dim OrderIndex As String = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Order_Index")
            Dim OrderNo As String = AutoDocument.Auto_DocumentType_Number(OrderDocumentTypeIndex, , OrderDate)
            Dim SqlInsertOrder As String = GetSqlInsertOrder()

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Order_Index", SqlDbType.VarChar).Value = OrderIndex
                .Add("@Order_No", SqlDbType.VarChar).Value = OrderNo
                .Add("@Order_Date", SqlDbType.Date).Value = OrderDate
                .Add("@Order_Time", SqlDbType.VarChar).Value = OrderTime
                .Add("@Ref_No1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No3", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No4", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No5", SqlDbType.VarChar).Value = String.Empty
                .Add("@Lot_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                .Add("@Supplier_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Department_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = OrderDocumentTypeIndex

                .Add("@Status", SqlDbType.Int).Value = 2

                .Add("@Comment", SqlDbType.NVarChar).Value = "Auto รับสินค้าเข้าเพื่อปรับยอด จาก Excel" & vbCrLf & " อ้างอิงใบเบิกเลขที่ " & WithdrawNo
                .Add("@Str1", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str2", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str3", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str4", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str5", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str6", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str7", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str8", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str9", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str10", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Flo1", SqlDbType.Float).Value = 0
                .Add("@Flo2", SqlDbType.Float).Value = 0
                .Add("@Flo3", SqlDbType.Float).Value = 0
                .Add("@Flo4", SqlDbType.Float).Value = 0
                .Add("@Flo5", SqlDbType.Float).Value = 0
                .Add("@PO_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Invoice_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@ASN_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Checker_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@ApprovedBy_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@HandlingType_Index", SqlDbType.Int).Value = 1
                .Add("@Vassel_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Flight_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Vehicle_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_by", SqlDbType.VarChar).Value = String.Empty
                .Add("@Origin_Port_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Origin_Country_Id", SqlDbType.VarChar).Value = "TH"
                .Add("@Destination_Port_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Destination_Country_Id", SqlDbType.VarChar).Value = "TH"
                .Add("@Terminal_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Receive_Type", SqlDbType.Int).Value = 0
                .Add("@Consignee_Index", SqlDbType.VarChar).Value = String.Empty

                .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With

            RowAffected = DBExeNonQuery(SqlInsertOrder, Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูลใบรับได้")
            End If

            Dim DataOrderItem As DataTable
            Dim RowOrderItem As DataRow
            Dim SQL_OrderItem As New System.Text.StringBuilder
            With SQL_OrderItem
                .Append(" SELECT ms_Location.Location_Alias, ms_Product.Product_Index, ms_Product.ProductType_Index, tb_OrderItem.* ")
                .Append(" FROM tb_OrderItem ")
                .Append(" INNER JOIN tb_LocationBalance ")
                .Append(" ON tb_OrderItem.OrderItem_Index = tb_LocationBalance.OrderItem_Index ")
                .Append(" INNER JOIN ms_Sku ")
                .Append(" ON tb_LocationBalance.Sku_Index = ms_Sku.Sku_Index ")
                .Append(" INNER JOIN ms_Product ")
                .Append(" ON ms_Sku.Product_Index = ms_Product.Product_Index ")
                .Append(" INNER JOIN ms_Location ")
                .Append(" ON tb_LocationBalance.Location_Index = ms_Location.Location_Index ")
                .Append(" WHERE tb_LocationBalance.LocationBalance_Index = @LocationBalance_Index ")
            End With

            Dim OrderItemIndex, OrderItemLocationIndex, TagIndex, TagNo, LocationBalanceIndex, TransactionIndex As String
            Dim Total_Qty, Price, OrderItemPrice, Weight, Volume, DimensionHi, DimensionWd, DimensionLen As Decimal

            Dim SqlInsertOrderItem As String = GetSqlInsertOrderItem()
            Dim SqlInsertOrderItemLocation As String = GetSqlInsertOrderItemLocation()
            Dim SqlInsertTag As String = GetSqlInsertTAG()
            Dim SqlInsertLocationBalance As String = GetSqlInsertLocationBalance()
            SqlInsertTransaction = GetSqlInsertTransaction()

            CurrentRow = 0
            For Each Row As DataRow In DataWithdraw.Rows
                OrderItemIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "OrderItem_Index")
                OrderItemLocationIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "OrderItemLocation_Index")
                TagIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "TAG_Index")
                TagNo = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "TAG_No")
                LocationBalanceIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "LocationBalance_Index")
                TransactionIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Transaction_Index")

                Total_Qty = Decimal.Parse(Row.Item("Total_Qty").ToString)

                CurrentRow += 1

                'Fig ค่า Ratio เป็น 1 เสมอ เพราะรับด้วยยอด Total_Qty
                Ratio = 1

                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Row.Item("LocationBalance_Index").ToString
                End With

                DataOrderItem = DBExeQuery(SQL_OrderItem.ToString, Transaction.Connection, Transaction)
                RowOrderItem = DataOrderItem.Rows.Item(0)

                DimensionWd = Decimal.Parse(RowOrderItem.Item("Flo2").ToString)
                DimensionLen = Decimal.Parse(RowOrderItem.Item("Flo3").ToString)
                DimensionHi = Decimal.Parse(RowOrderItem.Item("Flo4").ToString)

                Weight = Math.Round(Decimal.Parse(RowOrderItem.Item("Weight_Per_Pck").ToString) * Total_Qty, 6)
                Volume = Math.Round(Decimal.Parse(RowOrderItem.Item("Volume_Per_Pck").ToString) * Total_Qty, 6)

                Price = Decimal.Parse(RowOrderItem.Item("Price_Per_Pck").ToString)
                OrderItemPrice = Decimal.Parse(RowOrderItem.Item("OrderItem_Price").ToString)

                'tb_OrderItem
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@OrderItem_Index", SqlDbType.VarChar).Value = OrderItemIndex
                    .Add("@Order_Index", SqlDbType.VarChar).Value = OrderIndex
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                    .Add("@Lot_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@PLot", SqlDbType.VarChar).Value = Row.Item("PLot").ToString
                    .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                    .Add("@Package_Index", SqlDbType.VarChar).Value = Row.Item("Package_Index").ToString
                    .Add("@Ratio", SqlDbType.Float).Value = Ratio
                    .Add("@Qty", SqlDbType.Float).Value = Total_Qty
                    .Add("@Total_Qty", SqlDbType.Float).Value = Total_Qty
                    .Add("@Plan_Qty", SqlDbType.Float).Value = 0
                    .Add("@PalletType_Index", SqlDbType.VarChar).Value = RowOrderItem.Item("PalletType_Index").ToString
                    .Add("@Pallet_Qty", SqlDbType.Float).Value = 0
                    .Add("@Weight", SqlDbType.Float).Value = Weight
                    .Add("@Volume", SqlDbType.Float).Value = Volume
                    .Add("@IsMfg_Date", SqlDbType.Bit).Value = RowOrderItem.Item("IsMfg_Date")
                    .Add("@Mfg_Date", SqlDbType.SmallDateTime).Value = RowOrderItem.Item("Mfg_Date").ToString
                    .Add("@IsExp_Date", SqlDbType.Bit).Value = RowOrderItem.Item("IsExp_Date")
                    .Add("@Exp_Date", SqlDbType.SmallDateTime).Value = RowOrderItem.Item("Exp_Date").ToString
                    .Add("@Status", SqlDbType.Int).Value = 1
                    .Add("@Comment", SqlDbType.NVarChar).Value = String.Empty
                    .Add("@Str1", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str1").ToString
                    .Add("@Str2", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str2").ToString
                    .Add("@Str3", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str3").ToString
                    .Add("@Str4", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str4").ToString
                    .Add("@Str5", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str5").ToString
                    .Add("@Str6", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str6").ToString
                    .Add("@Str7", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str7").ToString
                    .Add("@Str8", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str8").ToString
                    .Add("@Str9", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str9").ToString
                    .Add("@Str10", SqlDbType.NVarChar).Value = RowOrderItem.Item("Str10").ToString
                    .Add("@Flo1", SqlDbType.Float).Value = Weight
                    .Add("@Flo2", SqlDbType.Float).Value = DimensionWd
                    .Add("@Flo3", SqlDbType.Float).Value = DimensionLen
                    .Add("@Flo4", SqlDbType.Float).Value = DimensionHi
                    .Add("@Flo5", SqlDbType.Float).Value = 0
                    .Add("@Is_SN", SqlDbType.Bit).Value = 0
                    .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@PO_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Invoice_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@ASN_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Declaration_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Plan_Process", SqlDbType.VarChar).Value = -9
                    .Add("@DocumentPlan_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Item_Qty", SqlDbType.Float).Value = Total_Qty
                    .Add("@Qty_Per_Pck", SqlDbType.Float).Value = 1
                    .Add("@Weight_Per_Pck", SqlDbType.Float).Value = Weight
                    .Add("@Price_Per_Pck", SqlDbType.Float).Value = Price
                    .Add("@Volume_Per_Pck", SqlDbType.Float).Value = Volume
                    .Add("@OrderItem_Price", SqlDbType.Float).Value = OrderItemPrice
                    .Add("@Item_Package_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@HandlingType_Index", SqlDbType.Int).Value = 1
                    .Add("@Tax1", SqlDbType.Float).Value = 0
                    .Add("@Tax2", SqlDbType.Float).Value = 0
                    .Add("@Tax3", SqlDbType.Float).Value = 0
                    .Add("@Tax4", SqlDbType.Float).Value = 0
                    .Add("@Tax5", SqlDbType.Float).Value = 0
                    .Add("@HS_Code", SqlDbType.VarChar).Value = String.Empty
                    .Add("@ItemDescription", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Seq", SqlDbType.Int).Value = CurrentRow
                    .Add("@Consignee_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@ERP_Location", SqlDbType.VarChar).Value = RowOrderItem.Item("ERP_Location").ToString
                    .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                End With

                RowAffected = DBExeNonQuery(SqlInsertOrderItem, Connection, Transaction)
                If Not RowAffected > 0 Then
                    Throw New Exception("ไม่สามารถบันทึกข้อมูลใบรับได้")
                End If

                'tb_OrderItemLocation
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@OrderItemLocation_Index", SqlDbType.VarChar).Value = OrderItemLocationIndex
                    .Add("@OrderItem_Index", SqlDbType.VarChar).Value = OrderItemIndex
                    .Add("@TAG_Index", SqlDbType.VarChar).Value = TagIndex
                    .Add("@TAG_No", SqlDbType.VarChar).Value = TagNo
                    .Add("@Order_Index", SqlDbType.VarChar).Value = OrderIndex
                    .Add("@JobOrder_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                    .Add("@Package_Index", SqlDbType.VarChar).Value = Row.Item("Package_Index").ToString
                    .Add("@Lot_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@PLot", SqlDbType.VarChar).Value = Row.Item("PLot").ToString
                    .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                    .Add("@Location_Index", SqlDbType.VarChar).Value = Row.Item("Location_Index").ToString
                    .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Qty", SqlDbType.Float).Value = Total_Qty
                    .Add("@Ratio", SqlDbType.Float).Value = Ratio
                    .Add("@Total_Qty", SqlDbType.Float).Value = Total_Qty
                    .Add("@Weight", SqlDbType.Float).Value = Weight
                    .Add("@Volume", SqlDbType.Float).Value = Volume
                    .Add("@PalletType_Index", SqlDbType.VarChar).Value = RowOrderItem.Item("PalletType_Index").ToString
                    .Add("@Pallet_Qty", SqlDbType.Float).Value = 0
                    .Add("@MixPallet", SqlDbType.Bit).Value = 0
                    .Add("@Status", SqlDbType.Int).Value = 2
                    .Add("@Qty_Item", SqlDbType.Float).Value = Total_Qty
                    .Add("@OrderItem_Price", SqlDbType.Float).Value = OrderItemPrice
                    .Add("@ERP_Location", SqlDbType.VarChar).Value = RowOrderItem.Item("ERP_Location").ToString
                    .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                End With

                RowAffected = DBExeNonQuery(SqlInsertOrderItemLocation, Transaction.Connection, Transaction)
                If Not RowAffected > 0 Then
                    Throw New Exception("ไม่สามารถบันทึกข้อมูล OrderItemLocation")
                End If

                'tb_TAG
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@TAG_Index", SqlDbType.VarChar).Value = TagIndex
                    .Add("@TAG_No", SqlDbType.VarChar).Value = TagNo
                    .Add("@LinkOrderFlag", SqlDbType.Bit).Value = 0
                    .Add("@Order_No", SqlDbType.VarChar).Value = OrderNo
                    .Add("@Order_Index", SqlDbType.VarChar).Value = OrderIndex
                    .Add("@Order_Date", SqlDbType.Date).Value = OrderDate
                    .Add("@Order_Time", SqlDbType.VarChar).Value = OrderTime
                    .Add("@OrderItem_Index", SqlDbType.VarChar).Value = OrderItemIndex
                    .Add("@OrderItemLocation_Index", SqlDbType.VarChar).Value = OrderItemLocationIndex
                    .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                    .Add("@Supplier_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                    .Add("@PLot", SqlDbType.NVarChar).Value = Row.Item("PLot").ToString
                    .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                    .Add("@Package_Index", SqlDbType.VarChar).Value = Row.Item("Package_Index").ToString
                    .Add("@Unit_Weight", SqlDbType.Float).Value = Weight
                    .Add("@Size_Index", SqlDbType.VarChar).Value = -1
                    .Add("@Pallet_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Qty", SqlDbType.Float).Value = Total_Qty
                    .Add("@Weight", SqlDbType.Float).Value = Weight
                    .Add("@Volume", SqlDbType.Float).Value = Volume
                    .Add("@Qty_per_TAG", SqlDbType.Float).Value = Total_Qty
                    .Add("@Weight_per_TAG", SqlDbType.Float).Value = Weight
                    .Add("@Volume_per_TAG", SqlDbType.Float).Value = Volume
                    .Add("@TAG_Status", SqlDbType.Int).Value = 2
                    .Add("@Str1", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str2", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str3", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str4", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str5", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str6", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str7", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str8", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str9", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Str10", SqlDbType.NVarChar).Value = DBNull.Value
                    .Add("@Flo1", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Flo2", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Flo3", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Flo4", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Flo5", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Ref_No1", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@Ref_No2", SqlDbType.VarChar).Value = RowOrderItem.Item("Str2").ToString
                    .Add("@Ref_No3", SqlDbType.VarChar).Value = "1/1"
                    .Add("@Ref_No4", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@Ref_No5", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                End With

                RowAffected = DBExeNonQuery(SqlInsertTag, Transaction.Connection, Transaction)
                If Not RowAffected > 0 Then
                    Throw New Exception("ไม่สามารถบันทึกข้อมูล TAG")
                End If

                'tb_LocationBalance
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = LocationBalanceIndex
                    .Add("@TAG_Index", SqlDbType.VarChar).Value = TagIndex
                    .Add("@Location_Index", SqlDbType.VarChar).Value = Row.Item("Location_Index").ToString
                    .Add("@TAG_No", SqlDbType.VarChar).Value = TagNo
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                    .Add("@Order_Index", SqlDbType.VarChar).Value = OrderIndex
                    .Add("@OrderItem_Index", SqlDbType.VarChar).Value = OrderItemIndex
                    .Add("@Lot_No", SqlDbType.NVarChar).Value = String.Empty
                    .Add("@PLot", SqlDbType.NVarChar).Value = Row.Item("PLot").ToString
                    .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                    .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Ratio", SqlDbType.Float).Value = Ratio
                    .Add("@Package_Index", SqlDbType.VarChar).Value = Row.Item("Package_Index").ToString
                    .Add("@Qty_Recieve_Package", SqlDbType.Float).Value = Total_Qty
                    .Add("@Qty_Bal", SqlDbType.Float).Value = Total_Qty
                    .Add("@Weight_Bal", SqlDbType.Float).Value = Weight
                    .Add("@Volume_Bal", SqlDbType.Float).Value = Volume
                    .Add("@Qty_Item_Bal", SqlDbType.Float).Value = Total_Qty
                    .Add("@OrderItem_Price_Bal", SqlDbType.Float).Value = OrderItemPrice
                    .Add("@ReserveQty", SqlDbType.Float).Value = 0
                    .Add("@ReserveWeight", SqlDbType.Float).Value = 0
                    .Add("@ReserveVolume", SqlDbType.Float).Value = 0
                    .Add("@ReserveQty_Item", SqlDbType.Float).Value = 0
                    .Add("@ReserveOrderItem_Price", SqlDbType.Float).Value = 0
                    .Add("@Location_Index_Begin", SqlDbType.VarChar).Value = Row.Item("Location_Index").ToString
                    .Add("@Qty_Bal_Begin", SqlDbType.Float).Value = Total_Qty
                    .Add("@Weight_Bal_Begin", SqlDbType.Float).Value = Weight
                    .Add("@Volume_Bal_Begin", SqlDbType.Float).Value = Volume
                    .Add("@Qty_Item_Begin", SqlDbType.Float).Value = Total_Qty
                    .Add("@OrderItem_Price_Begin", SqlDbType.Float).Value = OrderItemPrice
                    .Add("@PalletType_Index", SqlDbType.VarChar).Value = RowOrderItem.Item("PalletType_Index").ToString
                    .Add("@Pallet_Qty", SqlDbType.Float).Value = 0
                    .Add("@MixPallet", SqlDbType.Bit).Value = 0
                    .Add("@IsMfg_Date", SqlDbType.Bit).Value = RowOrderItem.Item("IsMfg_Date").ToString
                    .Add("@Mfg_Date", SqlDbType.SmallDateTime).Value = RowOrderItem.Item("Mfg_Date").ToString
                    .Add("@IsExp_Date", SqlDbType.Bit).Value = RowOrderItem.Item("IsExp_Date").ToString
                    .Add("@Exp_Date", SqlDbType.SmallDateTime).Value = RowOrderItem.Item("Exp_Date").ToString
                    .Add("@Status", SqlDbType.Int).Value = DBNull.Value
                    .Add("@Item_Package_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@TagPack_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@System_Pallet_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@ERP_Location", SqlDbType.VarChar).Value = RowOrderItem.Item("ERP_Location").ToString

                    .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                End With

                RowAffected = DBExeNonQuery(SqlInsertLocationBalance, Transaction.Connection, Transaction)
                If Not RowAffected > 0 Then
                    Throw New Exception("ไม่สามารถบันทึกข้อมูล LocationBalance")
                End If

                'Order tb_Transaction
                SQLServerCommand.Parameters.Clear()

                SkuBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.Sku, Row.Item("Sku_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                ItemStatusBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.ItemStatus, Row.Item("ItemStatus_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                PLotBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.PLot, Row.Item("PLot").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                LocationSkuBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationSku, Row.Item("Location_Index").ToString, Row.Item("Sku_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                LocationItemStatusBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationItemStatus, Row.Item("Location_Index").ToString, Row.Item("ItemStatus_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                LocationPLotBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationPLot, Row.Item("Location_Index").ToString, Row.Item("PLot").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)

                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@Transaction_Index", SqlDbType.VarChar).Value = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Transaction_Index")

                    .Add("@TAG_Index", SqlDbType.VarChar).Value = TagIndex
                    .Add("@Tag_No", SqlDbType.VarChar).Value = TagNo

                    .Add("@TAG_IndexNew", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@Tag_NoNew", SqlDbType.VarChar).Value = DBNull.Value

                    .Add("@Qty_In", SqlDbType.Float).Value = Total_Qty
                    .Add("@Weight_In", SqlDbType.Float).Value = Weight
                    .Add("@Volume_In", SqlDbType.Float).Value = Volume
                    .Add("@Qty_Out", SqlDbType.Float).Value = 0
                    .Add("@Weight_Out", SqlDbType.Float).Value = 0
                    .Add("@Volume_Out", SqlDbType.Float).Value = 0

                    .Add("@Qty_Item_In", SqlDbType.Float).Value = Total_Qty
                    .Add("@Qty_Item_Out", SqlDbType.Float).Value = 0
                    .Add("@Qty_Item_Bal", SqlDbType.Float).Value = Total_Qty
                    .Add("@OrderItem_Price_In", SqlDbType.Float).Value = OrderItemPrice
                    .Add("@OrderItem_Price_Out", SqlDbType.Float).Value = 0
                    .Add("@OrderItem_Price_Bal", SqlDbType.Float).Value = OrderItemPrice

                    .Add("@Transaction_Id", SqlDbType.VarChar).Value = OrderNo
                    .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                    .Add("@Order_Index", SqlDbType.VarChar).Value = OrderIndex
                    .Add("@Order_Date", SqlDbType.SmallDateTime).Value = OrderDate
                    .Add("@Transation_Date", SqlDbType.Date).Value = OrderDate
                    .Add("@OrderItem_Index", SqlDbType.VarChar).Value = OrderItemIndex
                    .Add("@Product_Index", SqlDbType.VarChar).Value = RowOrderItem.Item("Product_Index").ToString
                    .Add("@ProductType_Index", SqlDbType.VarChar).Value = RowOrderItem.Item("ProductType_Index").ToString
                    .Add("@DocumentType_Index", SqlDbType.VarChar).Value = OrderDocumentTypeIndex
                    .Add("@Description", SqlDbType.VarChar).Value = DBNull.Value
                    .Add("@ItemDefinition_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                    .Add("@Lot_No", SqlDbType.VarChar).Value = RowOrderItem.Item("Lot_No").ToString
                    .Add("@PLot", SqlDbType.VarChar).Value = Row.Item("PLot").ToString
                    .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                    .Add("@New_ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString

                    .Add("@Process_Id", SqlDbType.Int).Value = 1
                    .Add("@From_Location_Index", SqlDbType.VarChar).Value = Row.Item("Location_Index").ToString
                    .Add("@To_Location_Index", SqlDbType.VarChar).Value = Row.Item("Location_Index").ToString
                    .Add("@Location_Alias_To", SqlDbType.VarChar).Value = RowOrderItem.Item("Location_Alias").ToString
                    .Add("@Location_Alias_From", SqlDbType.VarChar).Value = RowOrderItem.Item("Location_Alias").ToString

                    If SkuBalance.Rows.Count > 0 Then
                        .Add("@Qty_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Qty").ToString
                        .Add("@Weight_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Weight").ToString
                        .Add("@Volume_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Volume").ToString
                    Else
                        .Add("@Qty_Sku_Bal", SqlDbType.Float).Value = 0
                        .Add("@Weight_Sku_Bal", SqlDbType.Float).Value = 0
                        .Add("@Volume_Sku_Bal", SqlDbType.Float).Value = 0
                    End If

                    If PLotBalance.Rows.Count > 0 Then
                        .Add("@Qty_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Qty").ToString
                        .Add("@Weight_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Weight").ToString
                        .Add("@Volume_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Volume").ToString
                    Else
                        .Add("@Qty_PLot_Bal", SqlDbType.Float).Value = 0
                        .Add("@Weight_PLot_Bal", SqlDbType.Float).Value = 0
                        .Add("@Volume_PLot_Bal", SqlDbType.Float).Value = 0
                    End If

                    If ItemStatusBalance.Rows.Count > 0 Then
                        .Add("@Qty_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Qty").ToString
                        .Add("@Weight_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Weight").ToString
                        .Add("@Volume_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Volume").ToString
                    Else
                        .Add("@Qty_ItemStatus_Bal", SqlDbType.Float).Value = 0
                        .Add("@Weight_ItemStatus_Bal", SqlDbType.Float).Value = 0
                        .Add("@Volume_ItemStatus_Bal", SqlDbType.Float).Value = 0
                    End If

                    .Add("@Move_Qty", SqlDbType.Float).Value = DBNull.Value
                    .Add("@Qty_Variance", SqlDbType.Float).Value = 0
                    .Add("@Referent_1", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Referent_2", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Item_Package_Index", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Invoice_In", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Invoice_Out", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Pallet_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Declaration_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@PO_No", SqlDbType.VarChar).Value = String.Empty
                    .Add("@SO_NO", SqlDbType.VarChar).Value = String.Empty
                    .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty

                    If LocationSkuBalance.Rows.Count > 0 Then
                        .Add("@Qty_Sku_Location_Bal", SqlDbType.Float).Value = LocationSkuBalance.Rows.Item(0).Item("Qty").ToString
                    Else
                        .Add("@Qty_Sku_Location_Bal", SqlDbType.Float).Value = 0
                    End If

                    If LocationItemStatusBalance.Rows.Count > 0 Then
                        .Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float).Value = LocationItemStatusBalance.Rows.Item(0).Item("Qty").ToString
                    Else
                        .Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float).Value = 0
                    End If

                    If LocationPLotBalance.Rows.Count > 0 Then
                        .Add("@Qty_PLot_Location_Bal", SqlDbType.Float).Value = LocationPLotBalance.Rows.Item(0).Item("Qty").ToString
                    Else
                        .Add("@Qty_PLot_Location_Bal", SqlDbType.Float).Value = 0
                    End If

                    .Add("@HandlingType_Index", SqlDbType.VarChar).Value = 1
                    .Add("@Tax1_In", SqlDbType.Float).Value = 0
                    .Add("@Tax2_In", SqlDbType.Float).Value = 0
                    .Add("@Tax3_In", SqlDbType.Float).Value = 0
                    .Add("@Tax4_In", SqlDbType.Float).Value = 0
                    .Add("@Tax5_In", SqlDbType.Float).Value = 0
                    .Add("@Tax1_Out", SqlDbType.Float).Value = 0
                    .Add("@Tax2_Out", SqlDbType.Float).Value = 0
                    .Add("@Tax3_Out", SqlDbType.Float).Value = 0
                    .Add("@Tax4_Out", SqlDbType.Float).Value = 0
                    .Add("@Tax5_Out", SqlDbType.Float).Value = 0
                    .Add("@Tax1_Bal", SqlDbType.Float).Value = 0
                    .Add("@Tax2_Bal", SqlDbType.Float).Value = 0
                    .Add("@Tax3_Bal", SqlDbType.Float).Value = 0
                    .Add("@Tax4_Bal", SqlDbType.Float).Value = 0
                    .Add("@Tax5_Bal", SqlDbType.Float).Value = 0
                    .Add("@ERP_Location_From", SqlDbType.VarChar).Value = RowOrderItem.Item("ERP_Location").ToString
                    .Add("@ERP_Location_TO", SqlDbType.VarChar).Value = RowOrderItem.Item("ERP_Location").ToString
                    .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = OrderIndex
                    .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = OrderItemIndex

                    .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                End With

                RowAffected = DBExeNonQuery(SqlInsertTransaction, Transaction.Connection, Transaction)
                If Not RowAffected > 0 Then
                    Throw New Exception("ไม่สามารถบันทึกข้อมูล Transaction")
                End If
            Next

            Transaction.Commit()
            Return OrderNo

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function SaveWithdraw(ByVal DataExcel As DataTable, ByVal DocumentTypeIndex As String) As String
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction

        Try
            If DataExcel Is Nothing OrElse Not DataExcel.Rows.Count > 0 Then
                Throw New Exception("ไม่พบข้อมูลที่จะบันทึก")
            End If

            SQLServerCommand.Parameters.Clear()
            DBExeNonQuery(String.Format(" TRUNCATE TABLE {0} ", IMPORT_WITHDRAW_TABLE_NAME), Transaction.Connection, Transaction)

            Dim SQL, SQL_Balance As System.Text.StringBuilder
            Dim RowAffected As Integer

            ' Insert Data to Table Import
            Using BulkCopy As SqlClient.SqlBulkCopy = New SqlClient.SqlBulkCopy(Transaction.Connection, SqlClient.SqlBulkCopyOptions.Default, Transaction)
                With BulkCopy
                    .BulkCopyTimeout = 0
                    .DestinationTableName = IMPORT_WITHDRAW_TABLE_NAME

                    With .ColumnMappings
                        .Clear()

                        .Add(New SqlClient.SqlBulkCopyColumnMapping("TAG_No", "TAG_No"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Sku_Id", "Sku_Id"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("PLot", "PLot"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Total_Qty", "Total_Qty"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("ItemStatus", "ItemStatus"))
                        .Add(New SqlClient.SqlBulkCopyColumnMapping("Location_Alias", "Location_Alias"))
                    End With

                    .WriteToServer(DataExcel)
                End With
            End Using

            ' Get Index Master
            SQL = New System.Text.StringBuilder
            With SQL
                .Append(" UPDATE Import ")
                .Append(" SET Sku_Index = Master_Sku_Index ")
                .Append("   , ItemStatus_Index = Master_ItemStatus_Index ")
                .Append("   , Location_Index = Master_Location_Index ")
                .Append("   , LocationBalance_Index = Transaction_LocationBalance_Index ")
                .Append("   , Customer_Index = Transaction_Customer_Index ")
                .Append(" FROM ( ")
                .Append("   SELECT PPW.LocationBalance_Index, tb_LocationBalance.LocationBalance_Index As Transaction_LocationBalance_Index ")
                .Append("        , PPW.Customer_Index, tb_Order.Customer_Index As Transaction_Customer_Index ")
                .Append("        , PPW.Sku_Index, tb_LocationBalance.Sku_Index As Master_Sku_Index ")
                .Append("        , PPW.ItemStatus_Index, tb_LocationBalance.ItemStatus_Index As Master_ItemStatus_Index ")
                .Append("   	 , PPW.Location_Index, tb_LocationBalance.Location_Index As Master_Location_Index ")
                '.Append("        , PPW.Sku_Index, ms_SKU.Sku_Index As Master_Sku_Index ")
                '.Append("        , PPW.ItemStatus_Index, ms_ItemStatus.ItemStatus_Index As Master_ItemStatus_Index ")
                '.Append("   	 , PPW.Location_Index, ms_Location.Location_Index As Master_Location_Index ")
                .Append(String.Format("   FROM {0} PPW ", IMPORT_WITHDRAW_TABLE_NAME))
                .Append("   INNER JOIN tb_LocationBalance ")
                .Append("   ON PPW.TAG_No = tb_LocationBalance.TAG_No ")
                .Append("   AND PPW.PLot = tb_LocationBalance.PLot ")
                .Append("   INNER JOIN tb_Order ")
                .Append("   ON tb_LocationBalance.Order_Index = tb_Order.Order_Index ")
                '.Append("   LEFT JOIN ms_SKU ")
                '.Append("   ON PPW.Sku_Id = ms_SKU.Sku_Id ")
                '.Append("   AND ms_SKU.status_id <> -1 ")
                '.Append("   LEFT JOIN ms_ItemStatus ")
                '.Append("   ON PPW.ItemStatus = ms_ItemStatus.ItemStatus_Id ")
                '.Append("   AND ms_ItemStatus.status_id <> -1 ")
                '.Append("   LEFT JOIN ms_Location ")
                '.Append("   ON PPW.Location_Alias = ms_Location.Location_Alias ")
                '.Append("   AND ms_Location.status_id <> -1 ")
                .Append("   WHERE tb_LocationBalance.Qty_Bal > 0 ")
                .Append(" ) Import ")
            End With

            SQLServerCommand.Parameters.Clear()
            RowAffected = DBExeNonQuery(SQL.ToString, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ข้อมูล TAG ไม่ถูกต้อง")
            End If

            Dim RowArray As DataRow()
            SQLServerCommand.Parameters.Clear()
            Dim DataWithdraw As DataTable = DBExeQuery(String.Format(" SELECT * FROM {0} ", IMPORT_WITHDRAW_TABLE_NAME), Transaction.Connection, Transaction)

            'Validate
            RowArray = DataWithdraw.Select("LocationBalance_Index IS NULL")
            If RowArray.Length > 0 Then
                Throw New Exception(String.Format("ไม่พบหมายเลข TAG [ {0} ] ในระบบ", RowArray(0).Item("TAG_No").ToString))
            End If

            Dim CustomerIndex As String
            Using DataCustomer As DataTable = DataWithdraw.DefaultView.ToTable(True, "Customer_Index")
                If DataCustomer.Rows.Count > 1 Then
                    Throw New Exception("ข้อมูล TAG มีมากกว่าหนั่งเจ้าของสินค้า")
                Else
                    CustomerIndex = DataCustomer.Rows.Item(0).Item("Customer_Index").ToString
                End If
            End Using

            If RowArray.Length > 0 Then
                Throw New Exception(String.Format("ไม่พบหมายเลข TAG [ {0} ] ในระบบ", RowArray(0).Item("TAG_No").ToString))
            End If

            Dim AutoIndex As New Sy_AutoNumber
            Dim AutoDocument As New Sy_DocumentNumber

            'Insert tb_Withdraw
            Dim WithdrawDate As Date = Date.Now.ToString("yyyy-MM-dd")
            Dim WithdrawIndex As String = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Withdraw_Index")
            Dim WithdrawNo As String = AutoDocument.Auto_DocumentType_Number(DocumentTypeIndex, , WithdrawDate)
            Dim SqlInsertWithdraw As String = GetSqlInsertWithdraw()

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@Withdraw_Index", SqlDbType.VarChar).Value = WithdrawIndex
                .Add("@Withdraw_No", SqlDbType.VarChar).Value = WithdrawNo
                .Add("@Withdraw_Date", SqlDbType.Date).Value = WithdrawDate
                .Add("@Departure_Date", SqlDbType.Date).Value = WithdrawDate
                .Add("@Arrival_Date", SqlDbType.Date).Value = WithdrawDate
                .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                .Add("@Shipper_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Department_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No3", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No4", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No5", SqlDbType.VarChar).Value = String.Empty
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = DocumentTypeIndex
                .Add("@Contact_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Comment", SqlDbType.VarChar).Value = "Auto เบิกสินค้าจาก Excel"
                .Add("@Str1", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str2", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str3", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str4", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str5", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str6", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str7", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str8", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str9", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Str10", SqlDbType.NVarChar).Value = String.Empty
                .Add("@Flo1", SqlDbType.Float).Value = 0
                .Add("@Flo2", SqlDbType.Float).Value = 0
                .Add("@Flo3", SqlDbType.Float).Value = 0
                .Add("@Flo4", SqlDbType.Float).Value = 0
                .Add("@Flo5", SqlDbType.Float).Value = 0

                .Add("@Status", SqlDbType.Int).Value = 2

                .Add("@Customer_Shipping_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Driver_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Round", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Leave_Time", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Factory_In", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Factory_Out", SqlDbType.SmallDateTime).Value = WithdrawDate
                .Add("@Return_Time", SqlDbType.SmallDateTime).Value = WithdrawDate

                .Add("@Container_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@VehicleType_Index", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_From", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_To", SqlDbType.VarChar).Value = String.Empty
                .Add("@Checker_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Driver_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@ApprovedBy_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Container_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_InGate", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_OutGate", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_StartLoad", SqlDbType.VarChar).Value = String.Empty
                .Add("@Time_FinistLoad", SqlDbType.VarChar).Value = String.Empty

                .Add("@HandlingType_Index", SqlDbType.Int).Value = 1
                .Add("@Vassel_Name", SqlDbType.VarChar).Value = String.Empty
                .Add("@Flight_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Vehicle_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Transport_by", SqlDbType.VarChar).Value = String.Empty
                .Add("@Origin_Port_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Origin_Country_Id", SqlDbType.VarChar).Value = "TH"
                .Add("@Destination_Port_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@Destination_Country_Id", SqlDbType.VarChar).Value = "TH"
                .Add("@Terminal_Id", SqlDbType.VarChar).Value = String.Empty
                .Add("@SO_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Invoice_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@ASN_No", SqlDbType.VarChar).Value = String.Empty
                .Add("@Withdraw_Type", SqlDbType.Bit).Value = 0
                .Add("@Confirm_Flag", SqlDbType.Int).Value = 0
                .Add("@User_UseDoc", SqlDbType.Bit).Value = 0

                .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With

            RowAffected = DBExeNonQuery(SqlInsertWithdraw, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูลใบเบิกได้")
            End If

            'Get LocationBalance
            SQL_Balance = New System.Text.StringBuilder
            With SQL_Balance
                .Append(" SELECT lc.Location_Alias, tg.Customer_Index, tg.Order_Date, sku.Product_Index, sku.ProductType_Index, lb.* ")
                .Append(" FROM tb_LocationBalance lb ")
                .Append(" INNER JOIN tb_TAG tg ")
                .Append(" ON lb.TAG_Index = tg.TAG_Index ")
                .Append(" INNER JOIN ms_Location lc ")
                .Append(" ON lb.Location_Index = lc.Location_Index ")
                .Append(" LEFT JOIN ( ")
                .Append(" 	SELECT sk.Sku_Index, pd.Product_Index, pdt.ProductType_Index ")
                .Append(" 	FROM ms_SKU sk ")
                .Append(" 	INNER JOIN ms_Product pd  ")
                .Append(" 	ON sk.Product_Index = pd.Product_Index ")
                .Append(" 	INNER JOIN ms_ProductType pdt ")
                .Append(" 	ON pd.ProductType_Index = pdt.ProductType_Index ")
                .Append(" ) sku ")
                .Append(" ON lb.Sku_Index = sku.Sku_Index ")
                .Append(" WHERE lb.LocationBalance_Index = @LocationBalance_Index ")
            End With

            'Insert Item
            Dim WithdrawItemIndex, WithdrawItemLocationIndex As String
            Dim SqlInsertWithdrawItem As String = GetSqlInsertWithdrawItem()
            Dim SqlWithdrawItemLocation As String = GetSqlInsertWithdrawItemLocation()
            Dim SqlUpdateLocationBalance As String = GetSqlUpdateLocationBalance()
            Dim SqlInsertTransaction As String = GetSqlInsertTransaction()

            Dim PendingTotalQty, RemainQty, Ratio As Decimal
            Dim QtyBalance, QtyBalanceBegin, QtyReserve, QtyRemain As Decimal
            Dim QtyWithdraw, WeightWithdraw, VolumeWithdraw, QtyItemWithdraw, OrderItemPriceWithdraw As Decimal
            Dim SkuBalance, ItemStatusBalance, PLotBalance, LocationSkuBalance, LocationItemStatusBalance, LocationPLotBalance As DataTable

            Dim LocationBalance As DataTable

            Dim CurrentRow As Integer = 0

            For Each Row As DataRow In DataWithdraw.Rows
                PendingTotalQty = Decimal.Parse(Row.Item("Total_Qty").ToString)
                If Not PendingTotalQty > 0 Then
                    Continue For
                End If

                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Row.Item("LocationBalance_Index").ToString
                End With

                'Validate LocationBalance
                LocationBalance = DBExeQuery(SQL_Balance.ToString, Transaction.Connection, Transaction)
                If LocationBalance Is Nothing OrElse Not LocationBalance.Rows.Count > 0 Then
                    Throw New Exception("ไม่พบข้อมูลสินค้าในระบบ")
                End If

                RemainQty = LocationBalance.Compute("SUM(Qty_Bal) - SUM(ReserveQty)", "1=1")
                If PendingTotalQty > RemainQty Then
                    Throw New Exception(String.Format("จำนวนคงเหลือ TAG [ {0} ] ไม่พอที่จะเบิก", Row.Item("TAG_No").ToString))
                End If

                For Each Balance As DataRow In LocationBalance.Rows
                    CurrentRow += 1

                    'Fig ค่า Ration เป็น 1 เสมอ เพราะเบิกด้วยยอด Total_Qty
                    Ratio = 1

                    If Not PendingTotalQty > 0 Then
                        Exit For
                    End If

                    QtyBalance = Decimal.Parse(Balance.Item("Qty_Bal").ToString)
                    QtyReserve = Decimal.Parse(Balance.Item("ReserveQty").ToString)
                    QtyBalanceBegin = Decimal.Parse(Balance.Item("Qty_Bal_Begin").ToString)
                    QtyRemain = QtyBalance - QtyReserve

                    If Not QtyRemain > 0 Then
                        Continue For
                    End If

                    If PendingTotalQty > QtyRemain Then
                        QtyWithdraw = QtyRemain
                    Else
                        QtyWithdraw = PendingTotalQty
                    End If

                    PendingTotalQty -= QtyWithdraw

                    WeightWithdraw = Math.Round((Decimal.Parse(Balance.Item("Weight_Bal_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)
                    VolumeWithdraw = Math.Round((Decimal.Parse(Balance.Item("Volume_Bal_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)
                    QtyItemWithdraw = Math.Round((Decimal.Parse(Balance.Item("Qty_Item_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)
                    OrderItemPriceWithdraw = Math.Round((Decimal.Parse(Balance.Item("OrderItem_Price_Begin").ToString) / QtyBalanceBegin) * QtyWithdraw, 6)

                    'Update tb_LocationBalance
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Balance.Item("LocationBalance_Index").ToString
                        .Add("@Qty_Bal", SqlDbType.Decimal).Value = QtyWithdraw
                        .Add("@Weight_Bal", SqlDbType.Decimal).Value = WeightWithdraw
                        .Add("@Volume_Bal", SqlDbType.Decimal).Value = VolumeWithdraw
                        .Add("@Qty_Item_Bal", SqlDbType.Decimal).Value = QtyItemWithdraw
                        .Add("@OrderItem_Price_Bal", SqlDbType.Decimal).Value = OrderItemPriceWithdraw
                    End With

                    RowAffected = DBExeNonQuery(SqlUpdateLocationBalance, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล LocationBalance")
                    End If

                    WithdrawItemIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "WithdrawItem_Index")

                    'tb_WithdrawItem
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@WithdrawItem_Index", SqlDbType.VarChar).Value = WithdrawItemIndex
                        .Add("@Withdraw_Index", SqlDbType.VarChar).Value = WithdrawIndex
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Balance.Item("Sku_Index").ToString
                        .Add("@Package_Index", SqlDbType.VarChar).Value = Balance.Item("Package_Index").ToString
                        .Add("@PLot", SqlDbType.VarChar).Value = String.Empty
                        .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString
                        .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Qty", SqlDbType.Float).Value = Math.Round(QtyWithdraw / Ratio, 6)
                        .Add("@Ratio", SqlDbType.Float).Value = Ratio
                        .Add("@Total_Qty", SqlDbType.Float).Value = QtyWithdraw
                        .Add("@Weight", SqlDbType.Float).Value = WeightWithdraw
                        .Add("@Volume", SqlDbType.Float).Value = VolumeWithdraw
                        .Add("@Plan_Qty", SqlDbType.Float).Value = 0
                        .Add("@Plan_Total_Qty", SqlDbType.Float).Value = 0
                        .Add("@Str1", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str2", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str3", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str4", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str5", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Flo1", SqlDbType.Float).Value = 0
                        .Add("@Flo2", SqlDbType.Float).Value = 0
                        .Add("@Flo3", SqlDbType.Float).Value = 0
                        .Add("@Flo4", SqlDbType.Float).Value = 0
                        .Add("@Flo5", SqlDbType.Float).Value = 0

                        .Add("@Status", SqlDbType.Int).Value = 1

                        .Add("@ItemDefinition_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Charge_Status", SqlDbType.Int).Value = 0
                        .Add("@Item_Qty", SqlDbType.Float).Value = QtyItemWithdraw
                        .Add("@Price", SqlDbType.Float).Value = OrderItemPriceWithdraw
                        .Add("@Item_Package_Index", SqlDbType.NVarChar).Value = String.Empty

                        .Add("@Invoice_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@SO_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@PACKING_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Declaration_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@HandlingType_Index", SqlDbType.VarChar).Value = String.Empty

                        .Add("@Plan_Process", SqlDbType.Int).Value = -9

                        .Add("@DocumentPlan_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@AssetLocationBalance_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Tax1", SqlDbType.Float).Value = 0
                        .Add("@Tax2", SqlDbType.Float).Value = 0
                        .Add("@Tax3", SqlDbType.Float).Value = 0
                        .Add("@Tax4", SqlDbType.Float).Value = 0
                        .Add("@Tax5", SqlDbType.Float).Value = 0
                        .Add("@HS_Code", SqlDbType.VarChar).Value = String.Empty
                        .Add("@ItemDescription", SqlDbType.VarChar).Value = String.Empty

                        .Add("@Seq", SqlDbType.Int).Value = CurrentRow

                        .Add("@Consignee_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str6", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str7", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str8", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str9", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@Str10", SqlDbType.NVarChar).Value = String.Empty
                        .Add("@OrderItem_Index", SqlDbType.VarChar).Value = Balance.Item("OrderItem_Index").ToString
                        .Add("@NewItemFlag", SqlDbType.Int).Value = 2
                        .Add("@ERP_Location", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Status_PickToLight", SqlDbType.Int).Value = 0

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    End With

                    RowAffected = DBExeNonQuery(SqlInsertWithdrawItem, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล WithdrawItem")
                    End If

                    WithdrawItemLocationIndex = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "WithdrawItemLocation_Index")

                    'tb_WithdrawItemLocation
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@WithdrawItemLocation_Index", SqlDbType.VarChar).Value = WithdrawItemLocationIndex
                        .Add("@JobWithdraw_Index", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@Withdraw_Index", SqlDbType.VarChar).Value = WithdrawIndex
                        .Add("@WithdrawItem_Index", SqlDbType.VarChar).Value = WithdrawItemIndex
                        .Add("@Order_Index", SqlDbType.VarChar).Value = Balance.Item("Order_Index").ToString
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Balance.Item("Sku_Index").ToString
                        .Add("@Lot_No", SqlDbType.VarChar).Value = Balance.Item("Lot_No").ToString
                        .Add("@Plot", SqlDbType.VarChar).Value = Balance.Item("Plot").ToString
                        .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString
                        .Add("@Tag_No", SqlDbType.VarChar).Value = Balance.Item("Tag_No").ToString
                        .Add("@Tag_Index", SqlDbType.VarChar).Value = Balance.Item("Tag_Index").ToString
                        .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Balance.Item("LocationBalance_Index").ToString
                        .Add("@Location_Index", SqlDbType.VarChar).Value = Balance.Item("Location_Index").ToString
                        .Add("@Serial_No", SqlDbType.VarChar).Value = Balance.Item("Serial_No").ToString
                        .Add("@Qty", SqlDbType.VarChar).Value = Math.Round(QtyWithdraw / Ratio, 6) 'Decimal.Parse(Row.Item("Qty").ToString)
                        .Add("@Package_Index", SqlDbType.VarChar).Value = Balance.Item("Package_Index").ToString
                        .Add("@Total_Qty", SqlDbType.Float).Value = QtyWithdraw
                        .Add("@Weight", SqlDbType.Float).Value = WeightWithdraw
                        .Add("@Volume", SqlDbType.Float).Value = VolumeWithdraw
                        .Add("@Pallet_Qty", SqlDbType.Int).Value = 0
                        .Add("@Status", SqlDbType.Int).Value = -9
                        .Add("@Item_Qty", SqlDbType.Float).Value = QtyItemWithdraw
                        .Add("@Price", SqlDbType.Float).Value = OrderItemPriceWithdraw
                        .Add("@TagOut_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@ERP_Location", SqlDbType.VarChar).Value = String.Empty
                        .Add("@picking_by", SqlDbType.VarChar).Value = String.Empty
                        .Add("@picking_date", SqlDbType.SmallDateTime).Value = DBNull.Value

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    End With

                    RowAffected = DBExeNonQuery(SqlWithdrawItemLocation, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล WithdrawItemLocation")
                    End If

                    'tb_Transaction

                    SQLServerCommand.Parameters.Clear()

                    SkuBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.Sku, Balance.Item("Sku_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    ItemStatusBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.ItemStatus, Balance.Item("ItemStatus_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    PLotBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.PLot, Balance.Item("PLot").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    LocationSkuBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationSku, Balance.Item("Location_Index").ToString, Balance.Item("Sku_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    LocationItemStatusBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationItemStatus, Balance.Item("Location_Index").ToString, Balance.Item("ItemStatus_Index").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)
                    LocationPLotBalance = DBExeQuery(GetSqlQtyWeightVolumeBalance(eBalanceCondition.LocationPLot, Balance.Item("Location_Index").ToString, Balance.Item("PLot").ToString), Transaction.Connection, Transaction, , eData.DataAdapter)

                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@Transaction_Index", SqlDbType.VarChar).Value = AutoIndex.getSys_Value(Transaction.Connection, Transaction, "Transaction_Index")

                        .Add("@TAG_Index", SqlDbType.VarChar).Value = Balance.Item("TAG_Index").ToString
                        .Add("@Tag_No", SqlDbType.VarChar).Value = Balance.Item("Tag_No").ToString

                        .Add("@TAG_IndexNew", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@Tag_NoNew", SqlDbType.VarChar).Value = DBNull.Value

                        .Add("@Qty_In", SqlDbType.Float).Value = 0
                        .Add("@Weight_In", SqlDbType.Float).Value = 0
                        .Add("@Volume_In", SqlDbType.Float).Value = 0
                        .Add("@Qty_Out", SqlDbType.Float).Value = QtyWithdraw
                        .Add("@Weight_Out", SqlDbType.Float).Value = WeightWithdraw
                        .Add("@Volume_Out", SqlDbType.Float).Value = VolumeWithdraw

                        .Add("@Qty_Item_In", SqlDbType.Float).Value = 0
                        .Add("@Qty_Item_Out", SqlDbType.Float).Value = QtyItemWithdraw
                        .Add("@Qty_Item_Bal", SqlDbType.Float).Value = Decimal.Parse(Balance.Item("Qty_Item_Bal").ToString) - QtyItemWithdraw
                        .Add("@OrderItem_Price_In", SqlDbType.Float).Value = 0
                        .Add("@OrderItem_Price_Out", SqlDbType.Float).Value = OrderItemPriceWithdraw
                        .Add("@OrderItem_Price_Bal", SqlDbType.Float).Value = Decimal.Parse(Balance.Item("OrderItem_Price_Bal").ToString) - OrderItemPriceWithdraw

                        .Add("@Transaction_Id", SqlDbType.VarChar).Value = WithdrawNo
                        .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                        .Add("@Order_Index", SqlDbType.VarChar).Value = Balance.Item("Order_Index").ToString
                        .Add("@Order_Date", SqlDbType.SmallDateTime).Value = Balance.Item("Order_Date").ToString
                        .Add("@Transation_Date", SqlDbType.Date).Value = WithdrawDate
                        .Add("@OrderItem_Index", SqlDbType.VarChar).Value = Balance.Item("OrderItem_Index").ToString
                        .Add("@Product_Index", SqlDbType.VarChar).Value = Balance.Item("Product_Index").ToString
                        .Add("@ProductType_Index", SqlDbType.VarChar).Value = Balance.Item("ProductType_Index").ToString
                        .Add("@DocumentType_Index", SqlDbType.VarChar).Value = DocumentTypeIndex
                        .Add("@Description", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@ItemDefinition_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Balance.Item("Sku_Index").ToString
                        .Add("@Lot_No", SqlDbType.VarChar).Value = Balance.Item("Lot_No").ToString
                        .Add("@PLot", SqlDbType.VarChar).Value = Balance.Item("PLot").ToString
                        .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString

                        .Add("@New_ItemStatus_Index", SqlDbType.VarChar).Value = Balance.Item("ItemStatus_Index").ToString

                        .Add("@Process_Id", SqlDbType.Int).Value = 2
                        .Add("@From_Location_Index", SqlDbType.VarChar).Value = Balance.Item("Location_Index").ToString
                        .Add("@To_Location_Index", SqlDbType.VarChar).Value = Balance.Item("Location_Index").ToString
                        .Add("@Location_Alias_To", SqlDbType.VarChar).Value = Balance.Item("Location_Alias").ToString
                        .Add("@Location_Alias_From", SqlDbType.VarChar).Value = Balance.Item("Location_Alias").ToString

                        If SkuBalance.Rows.Count > 0 Then
                            .Add("@Qty_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Qty").ToString
                            .Add("@Weight_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Weight").ToString
                            .Add("@Volume_Sku_Bal", SqlDbType.Float).Value = SkuBalance.Rows.Item(0).Item("Volume").ToString
                        Else
                            .Add("@Qty_Sku_Bal", SqlDbType.Float).Value = 0
                            .Add("@Weight_Sku_Bal", SqlDbType.Float).Value = 0
                            .Add("@Volume_Sku_Bal", SqlDbType.Float).Value = 0
                        End If

                        If PLotBalance.Rows.Count > 0 Then
                            .Add("@Qty_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Qty").ToString
                            .Add("@Weight_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Weight").ToString
                            .Add("@Volume_PLot_Bal", SqlDbType.Float).Value = PLotBalance.Rows.Item(0).Item("Volume").ToString
                        Else
                            .Add("@Qty_PLot_Bal", SqlDbType.Float).Value = 0
                            .Add("@Weight_PLot_Bal", SqlDbType.Float).Value = 0
                            .Add("@Volume_PLot_Bal", SqlDbType.Float).Value = 0
                        End If

                        If ItemStatusBalance.Rows.Count > 0 Then
                            .Add("@Qty_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Qty").ToString
                            .Add("@Weight_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Weight").ToString
                            .Add("@Volume_ItemStatus_Bal", SqlDbType.Float).Value = ItemStatusBalance.Rows.Item(0).Item("Volume").ToString
                        Else
                            .Add("@Qty_ItemStatus_Bal", SqlDbType.Float).Value = 0
                            .Add("@Weight_ItemStatus_Bal", SqlDbType.Float).Value = 0
                            .Add("@Volume_ItemStatus_Bal", SqlDbType.Float).Value = 0
                        End If

                        .Add("@Move_Qty", SqlDbType.Float).Value = DBNull.Value
                        .Add("@Qty_Variance", SqlDbType.Float).Value = 0
                        .Add("@Referent_1", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Referent_2", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Item_Package_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Invoice_In", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Invoice_Out", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Pallet_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Declaration_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@PO_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@SO_NO", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty

                        If LocationSkuBalance.Rows.Count > 0 Then
                            .Add("@Qty_Sku_Location_Bal", SqlDbType.Float).Value = LocationSkuBalance.Rows.Item(0).Item("Qty").ToString
                        Else
                            .Add("@Qty_Sku_Location_Bal", SqlDbType.Float).Value = 0
                        End If

                        If LocationItemStatusBalance.Rows.Count > 0 Then
                            .Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float).Value = LocationItemStatusBalance.Rows.Item(0).Item("Qty").ToString
                        Else
                            .Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float).Value = 0
                        End If

                        If LocationPLotBalance.Rows.Count > 0 Then
                            .Add("@Qty_PLot_Location_Bal", SqlDbType.Float).Value = LocationPLotBalance.Rows.Item(0).Item("Qty").ToString
                        Else
                            .Add("@Qty_PLot_Location_Bal", SqlDbType.Float).Value = 0
                        End If

                        .Add("@HandlingType_Index", SqlDbType.VarChar).Value = 1
                        .Add("@Tax1_In", SqlDbType.Float).Value = 0
                        .Add("@Tax2_In", SqlDbType.Float).Value = 0
                        .Add("@Tax3_In", SqlDbType.Float).Value = 0
                        .Add("@Tax4_In", SqlDbType.Float).Value = 0
                        .Add("@Tax5_In", SqlDbType.Float).Value = 0
                        .Add("@Tax1_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax2_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax3_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax4_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax5_Out", SqlDbType.Float).Value = 0
                        .Add("@Tax1_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax2_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax3_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax4_Bal", SqlDbType.Float).Value = 0
                        .Add("@Tax5_Bal", SqlDbType.Float).Value = 0
                        .Add("@ERP_Location_From", SqlDbType.VarChar).Value = Balance.Item("ERP_Location").ToString
                        .Add("@ERP_Location_TO", SqlDbType.VarChar).Value = Balance.Item("ERP_Location").ToString
                        .Add("@DocumentPlan_Index", SqlDbType.VarChar).Value = WithdrawIndex
                        .Add("@DocumentPlanItem_Index", SqlDbType.VarChar).Value = WithdrawItemIndex

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    End With

                    RowAffected = DBExeNonQuery(SqlInsertTransaction, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล Transaction")
                    End If
                Next
            Next

            Transaction.Commit()
            Return WithdrawNo

        Catch Ex As Exception
            Transaction.Rollback()
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertOrder() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_Order ")
                .Append(" ( Order_Index ")
                .Append(" , Order_No ")
                .Append(" , Order_Date ")
                .Append(" , Order_Time ")
                .Append(" , Ref_No1, Ref_No2, Ref_No3, Ref_No4, Ref_No5 ")
                .Append(" , Lot_No ")
                .Append(" , Customer_Index ")
                .Append(" , Supplier_Index ")
                .Append(" , Department_Index ")
                .Append(" , DocumentType_Index ")
                .Append(" , Status ")
                .Append(" , Comment ")
                .Append(" , Str1, Str2, Str3, Str4, Str5, Str6, Str7, Str8, Str9, Str10 ")
                .Append(" , Flo1, Flo2, Flo3, Flo4, Flo5 ")
                .Append(" , PO_No ")
                .Append(" , Invoice_No ")
                .Append(" , ASN_No ")
                .Append(" , Checker_Name ")
                .Append(" , ApprovedBy_Name ")
                .Append(" , HandlingType_Index ")
                .Append(" , Vassel_Name ")
                .Append(" , Flight_No ")
                .Append(" , Vehicle_No ")
                .Append(" , Transport_by ")
                .Append(" , Origin_Port_Id ")
                .Append(" , Origin_Country_Id ")
                .Append(" , Destination_Port_Id ")
                .Append(" , Destination_Country_Id ")
                .Append(" , Terminal_Id ")
                .Append(" , Receive_Type ")
                .Append(" , Consignee_Index ")
                .Append(" , Departure_Date ")
                .Append(" , Arrival_Date ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch) ")
                .Append(" VALUES ")
                .Append(" ( @Order_Index ")
                .Append(" , @Order_No ")
                .Append(" , @Order_Date ")
                .Append(" , @Order_Time ")
                .Append(" , @Ref_No1, @Ref_No2, @Ref_No3, @Ref_No4, @Ref_No5 ")
                .Append(" , @Lot_No ")
                .Append(" , @Customer_Index ")
                .Append(" , @Supplier_Index ")
                .Append(" , @Department_Index ")
                .Append(" , @DocumentType_Index ")
                .Append(" , @Status ")
                .Append(" , @Comment ")
                .Append(" , @Str1, @Str2, @Str3, @Str4, @Str5, @Str6, @Str7, @Str8, @Str9, @Str10 ")
                .Append(" , @Flo1, @Flo2, @Flo3, @Flo4, @Flo5 ")
                .Append(" , @PO_No ")
                .Append(" , @Invoice_No ")
                .Append(" , @ASN_No ")
                .Append(" , @Checker_Name ")
                .Append(" , @ApprovedBy_Name ")
                .Append(" , @HandlingType_Index ")
                .Append(" , @Vassel_Name ")
                .Append(" , @Flight_No ")
                .Append(" , @Vehicle_No ")
                .Append(" , @Transport_by ")
                .Append(" , @Origin_Port_Id ")
                .Append(" , @Origin_Country_Id ")
                .Append(" , @Destination_Port_Id ")
                .Append(" , @Destination_Country_Id ")
                .Append(" , @Terminal_Id ")
                .Append(" , @Receive_Type ")
                .Append(" , @Consignee_Index ")
                .Append(" , GETDATE() ")
                .Append(" , GETDATE() ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertOrderItem() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_OrderItem ")
                .Append(" ( OrderItem_Index ")
                .Append(" , Order_Index ")
                .Append(" , Sku_Index ")
                .Append(" , Lot_No ")
                .Append(" , PLot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , Package_Index ")
                .Append(" , Ratio, Total_Qty, Qty, Plan_Qty, PalletType_Index, Pallet_Qty ")
                .Append(" , Weight ")
                .Append(" , Volume ")
                .Append(" , Serial_No ")
                .Append(" , IsMfg_Date, Mfg_Date ")
                .Append(" , IsExp_date, Exp_date ")
                .Append(" , Status ")
                .Append(" , Comment ")
                .Append(" , Str1, Str2, Str3, Str4, Str5, Str6, Str7, Str8, Str9, Str10 ")
                .Append(" , Flo1, Flo2, Flo3, Flo4, Flo5 ")
                .Append(" , Is_SN ")
                .Append(" , PO_No ")
                .Append(" , Invoice_No ")
                .Append(" , ASN_No ")
                .Append(" , Declaration_No ")
                .Append(" , Plan_Process ")
                .Append(" , DocumentPlan_No ")
                .Append(" , DocumentPlanItem_Index ")
                .Append(" , DocumentPlan_Index ")
                .Append(" , Item_Qty, Qty_Per_Pck ")
                .Append(" , Weight_Per_Pck, Price_Per_Pck, Volume_Per_Pck ")
                .Append(" , OrderItem_Price ")
                .Append(" , Item_Package_Index ")
                .Append(" , HandlingType_Index ")
                .Append(" , Tax1, Tax2, Tax3, Tax4, Tax5 ")
                .Append(" , HS_Code ")
                .Append(" , ItemDescription ")
                .Append(" , Seq ")
                .Append(" , Consignee_Index ")
                .Append(" , ERP_Location ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch) ")
                .Append(" VALUES ")
                .Append(" ( @OrderItem_Index ")
                .Append(" , @Order_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @Lot_No ")
                .Append(" , @PLot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @Package_Index ")
                .Append(" , @Ratio, @Total_Qty, @Qty, @Plan_Qty, @PalletType_Index, @Pallet_Qty ")
                .Append(" , @Weight ")
                .Append(" , @Volume ")
                .Append(" , @Serial_No ")
                .Append(" , @IsMfg_Date, @Mfg_Date ")
                .Append(" , @IsExp_Date, @Exp_date ")
                .Append(" , @Status ")
                .Append(" , @Comment ")
                .Append(" , @Str1, @Str2, @Str3, @Str4, @Str5, @Str6, @Str7, @Str8, @Str9, @Str10 ")
                .Append(" , @Flo1, @Flo2, @Flo3, @Flo4, @Flo5 ")
                .Append(" , @Is_SN ")
                .Append(" , @PO_No ")
                .Append(" , @Invoice_No ")
                .Append(" , @ASN_No ")
                .Append(" , @Declaration_No ")
                .Append(" , @Plan_Process ")
                .Append(" , @DocumentPlan_No ")
                .Append(" , @DocumentPlanItem_Index ")
                .Append(" , @DocumentPlan_Index ")
                .Append(" , @Item_Qty, @Qty_Per_Pck ")
                .Append(" , @Weight_Per_Pck, @Price_Per_Pck, @Volume_Per_Pck ")
                .Append(" , @OrderItem_Price ")
                .Append(" , @Item_Package_Index ")
                .Append(" , @HandlingType_Index ")
                .Append(" , @Tax1, @Tax2, @Tax3, @Tax4, @Tax5 ")
                .Append(" , @HS_Code ")
                .Append(" , @ItemDescription ")
                .Append(" , @Seq ")
                .Append(" , @Consignee_Index ")
                .Append(" , @ERP_Location ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertTAG() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_Tag ")
                .Append(" ( TAG_Index ")
                .Append(" , TAG_No ")
                .Append(" , LinkOrderFlag ")
                .Append(" , Order_No ")
                .Append(" , Order_Index ")
                .Append(" , Order_Date ")
                .Append(" , Order_Time ")
                .Append(" , OrderItem_Index ")
                .Append(" , OrderItemLocation_Index ")
                .Append(" , Customer_Index ")
                .Append(" , Supplier_Index ")
                .Append(" , Sku_Index ")
                .Append(" , PLot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , Package_Index ")
                .Append(" , Unit_Weight ")
                .Append(" , Size_Index ")
                .Append(" , Pallet_No ")
                .Append(" , Qty ")
                .Append(" , Weight, Volume ")
                .Append(" , Qty_per_TAG, Weight_per_TAG, Volume_per_TAG ")
                .Append(" , TAG_Status ")
                .Append(" , Str1, Str2, Str3, Str4, Str5, Str6, Str7, Str8, Str9, Str10 ")
                .Append(" , Flo1, Flo2, Flo3, Flo4, Flo5 ")
                .Append(" , Ref_No1, Ref_No2, Ref_No3, Ref_No4, Ref_No5 ")
                .Append(" , Putaway_By ")
                .Append(" , Putaway_Date ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ) ")
                .Append(" VALUES ")
                .Append(" ( @TAG_Index ")
                .Append(" , @TAG_No ")
                .Append(" , @LinkOrderFlag ")
                .Append(" , @Order_No ")
                .Append(" , @Order_Index ")
                .Append(" , @Order_Date ")
                .Append(" , @Order_Time ")
                .Append(" , @OrderItem_Index ")
                .Append(" , @OrderItemLocation_Index ")
                .Append(" , @Customer_Index ")
                .Append(" , @Supplier_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @PLot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @Package_Index ")
                .Append(" , @Unit_Weight ")
                .Append(" , @Size_Index ")
                .Append(" , @Pallet_No ")
                .Append(" , @Qty ")
                .Append(" , @Weight, @Volume ")
                .Append(" , @Qty_per_TAG, @Weight_per_TAG, @Volume_per_TAG ")
                .Append(" , @TAG_Status ")
                .Append(" , @Str1, @Str2, @Str3, @Str4, @Str5, @Str6, @Str7, @Str8, @Str9, @Str10 ")
                .Append(" , @Flo1, @Flo2, @Flo3, @Flo4, @Flo5 ")
                .Append(" , @Ref_No1, @Ref_No2, @Ref_No3, @Ref_No4, @Ref_No5 ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertOrderItemLocation() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_OrderItemLocation ")
                .Append(" ( OrderItemLocation_Index ")
                .Append(" , OrderItem_Index ")
                .Append(" , TAG_Index ")
                .Append(" , Tag_No ")
                .Append(" , Order_Index ")
                .Append(" , JobOrder_Index ")
                .Append(" , Sku_Index ")
                .Append(" , Package_Index ")
                .Append(" , Lot_No ")
                .Append(" , PLot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , Location_Index ")
                .Append(" , Serial_No ")
                .Append(" , Qty ")
                .Append(" , Ratio ")
                .Append(" , Total_Qty ")
                .Append(" , Weight ")
                .Append(" , Volume ")
                .Append(" , PalletType_Index ")
                .Append(" , Pallet_Qty ")
                .Append(" , MixPallet ")
                .Append(" , Status ")
                .Append(" , Qty_Item ")
                .Append(" , OrderItem_Price ")
                .Append(" , ERP_Location ")
                .Append(" , Putaway_By ")
                .Append(" , Putaway_Date ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ) ")
                .Append(" VALUES ")
                .Append(" ( @OrderItemLocation_Index ")
                .Append(" , @OrderItem_Index ")
                .Append(" , @TAG_Index ")
                .Append(" , @Tag_No ")
                .Append(" , @Order_Index ")
                .Append(" , @JobOrder_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @Package_Index ")
                .Append(" , @Lot_No ")
                .Append(" , @PLot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @Location_Index ")
                .Append(" , @Serial_No ")
                .Append(" , @Qty ")
                .Append(" , @Ratio ")
                .Append(" , @Total_Qty ")
                .Append(" , @Weight ")
                .Append(" , @Volume ")
                .Append(" , @PalletType_Index ")
                .Append(" , @Pallet_Qty ")
                .Append(" , @MixPallet ")
                .Append(" , @Status ")
                .Append(" , @Qty_Item ")
                .Append(" , @OrderItem_Price ")
                .Append(" , @ERP_Location ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertLocationBalance() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_LocationBalance ")
                .Append(" ( LocationBalance_Index ")
                .Append(" , TAG_Index ")
                .Append(" , Location_Index ")
                .Append(" , Tag_No ")
                .Append(" , Sku_Index ")
                .Append(" , Order_Index ")
                .Append(" , OrderItem_Index ")
                .Append(" , Lot_No ")
                .Append(" , PLot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , Serial_No ")
                .Append(" , Ratio ")
                .Append(" , Package_Index ")
                .Append(" , Qty_Recieve_Package ")
                .Append(" , Qty_Bal ")
                .Append(" , Weight_Bal ")
                .Append(" , Volume_Bal ")
                .Append(" , Qty_Item_Bal ")
                .Append(" , OrderItem_Price_Bal ")
                .Append(" , ReserveQty ")
                .Append(" , ReserveWeight ")
                .Append(" , ReserveVolume ")
                .Append(" , ReserveQty_Item ")
                .Append(" , ReserveOrderItem_Price ")
                .Append(" , Location_Index_Begin ")
                .Append(" , Qty_Bal_Begin ")
                .Append(" , Weight_Bal_Begin ")
                .Append(" , Volume_Bal_Begin ")
                .Append(" , Qty_Item_Begin ")
                .Append(" , OrderItem_Price_Begin ")
                .Append(" , PalletType_Index ")
                .Append(" , Pallet_Qty ")
                .Append(" , MixPallet ")
                .Append(" , IsMfg_Date ")
                .Append(" , IsExp_Date ")
                .Append(" , Mfg_Date ")
                .Append(" , Exp_Date ")
                .Append(" , Status ")
                .Append(" , Item_Package_Index ")
                .Append(" , TagPack_No ")
                .Append(" , System_Pallet_No ")
                .Append(" , ERP_Location ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ) ")
                .Append(" VALUES ")
                .Append(" ( @LocationBalance_Index ")
                .Append(" , @TAG_Index ")
                .Append(" , @Location_Index ")
                .Append(" , @Tag_No ")
                .Append(" , @Sku_Index ")
                .Append(" , @Order_Index ")
                .Append(" , @OrderItem_Index ")
                .Append(" , @Lot_No ")
                .Append(" , @PLot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @Serial_No ")
                .Append(" , @Ratio ")
                .Append(" , @Package_Index ")
                .Append(" , @Qty_Recieve_Package ")
                .Append(" , @Qty_Bal ")
                .Append(" , @Weight_Bal ")
                .Append(" , @Volume_Bal ")
                .Append(" , @Qty_Item_Bal ")
                .Append(" , @OrderItem_Price_Bal ")
                .Append(" , @ReserveQty ")
                .Append(" , @ReserveWeight ")
                .Append(" , @ReserveVolume ")
                .Append(" , @ReserveQty_Item ")
                .Append(" , @ReserveOrderItem_Price ")
                .Append(" , @Location_Index_Begin ")
                .Append(" , @Qty_Bal_Begin ")
                .Append(" , @Weight_Bal_Begin ")
                .Append(" , @Volume_Bal_Begin ")
                .Append(" , @Qty_Item_Begin ")
                .Append(" , @OrderItem_Price_Begin ")
                .Append(" , @PalletType_Index ")
                .Append(" , @Pallet_Qty ")
                .Append(" , @MixPallet ")
                .Append(" , @IsMfg_Date ")
                .Append(" , @IsExp_Date ")
                .Append(" , @Mfg_Date ")
                .Append(" , @Exp_Date ")
                .Append(" , @Status ")
                .Append(" , @Item_Package_Index ")
                .Append(" , @TagPack_No ")
                .Append(" , @System_Pallet_No ")
                .Append(" , @ERP_Location ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertTransaction() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_Transaction ")
                .Append(" ( Transaction_Index ")
                .Append(" , Transaction_Id ")
                .Append(" , Customer_Index ")
                .Append(" , Order_Index ")
                .Append(" , Order_date ")
                .Append(" , Transation_Date ")
                .Append(" , OrderItem_Index ")
                .Append(" , Product_Index ")
                .Append(" , ProductType_Index ")
                .Append(" , DocumentType_Index ")
                .Append(" , Description ")
                .Append(" , ItemDefinition_Index ")
                .Append(" , Sku_Index ")
                .Append(" , Lot_No ")
                .Append(" , PLot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , New_ItemStatus_Index ")
                .Append(" , Process_Id ")
                .Append(" , TAG_Index ")
                .Append(" , TAG_IndexNew ")
                .Append(" , Tag_No ")
                .Append(" , Tag_NoNew ")
                .Append(" , From_Location_Index ")
                .Append(" , To_Location_Index ")
                .Append(" , Location_Alias_To ")
                .Append(" , Location_Alias_From ")
                .Append(" , Qty_In ")
                .Append(" , Weight_In ")
                .Append(" , Volume_In ")
                .Append(" , Qty_Out ")
                .Append(" , Weight_Out ")
                .Append(" , Volume_Out ")
                .Append(" , Qty_Sku_Bal ")
                .Append(" , Weight_Sku_Bal ")
                .Append(" , Volume_Sku_Bal ")
                .Append(" , Qty_PLot_Bal ")
                .Append(" , Weight_PLot_Bal ")
                .Append(" , Volume_PLot_Bal ")
                .Append(" , Qty_ItemStatus_Bal ")
                .Append(" , Weight_ItemStatus_Bal ")
                .Append(" , Volume_ItemStatus_Bal ")
                .Append(" , Move_Qty ")
                .Append(" , Qty_Variance ")
                .Append(" , Referent_1 ")
                .Append(" , Referent_2 ")
                .Append(" , Qty_Item_In ")
                .Append(" , Qty_Item_Out ")
                .Append(" , Qty_Item_Bal ")
                .Append(" , OrderItem_Price_In ")
                .Append(" , OrderItem_Price_Out ")
                .Append(" , OrderItem_Price_Bal ")
                .Append(" , Item_Package_Index ")
                .Append(" , Invoice_In ")
                .Append(" , Invoice_Out ")
                .Append(" , Pallet_No ")
                .Append(" , Declaration_No ")
                .Append(" , PO_No ")
                .Append(" , SO_NO ")
                .Append(" , Serial_No ")
                .Append(" , Qty_Sku_Location_Bal ")
                .Append(" , Qty_ItemStatus_Location_Bal ")
                .Append(" , Qty_PLot_Location_Bal ")
                .Append(" , HandlingType_Index ")
                .Append(" , Tax1_In ")
                .Append(" , Tax2_In ")
                .Append(" , Tax3_In ")
                .Append(" , Tax4_In ")
                .Append(" , Tax5_In ")
                .Append(" , Tax1_Out ")
                .Append(" , Tax2_Out ")
                .Append(" , Tax3_Out ")
                .Append(" , Tax4_Out ")
                .Append(" , Tax5_Out ")
                .Append(" , Tax1_Bal ")
                .Append(" , Tax2_Bal ")
                .Append(" , Tax3_Bal ")
                .Append(" , Tax4_Bal ")
                .Append(" , Tax5_Bal ")
                .Append(" , ERP_Location_From ")
                .Append(" , ERP_Location_TO ")
                .Append(" , DocumentPlan_Index ")
                .Append(" , DocumentPlanItem_Index ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ) ")
                .Append(" VALUES ")
                .Append(" ( @Transaction_Index ")
                .Append(" , @Transaction_Id ")
                .Append(" , @Customer_Index ")
                .Append(" , @Order_Index ")
                .Append(" , @Order_date ")
                .Append(" , @Transation_Date ")
                .Append(" , @OrderItem_Index ")
                .Append(" , @Product_Index ")
                .Append(" , @ProductType_Index ")
                .Append(" , @DocumentType_Index ")
                .Append(" , @Description ")
                .Append(" , @ItemDefinition_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @Lot_No ")
                .Append(" , @PLot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @New_ItemStatus_Index ")
                .Append(" , @Process_Id ")
                .Append(" , @TAG_Index ")
                .Append(" , @TAG_IndexNew ")
                .Append(" , @Tag_No ")
                .Append(" , @Tag_NoNew ")
                .Append(" , @From_Location_Index ")
                .Append(" , @To_Location_Index ")
                .Append(" , @Location_Alias_To ")
                .Append(" , @Location_Alias_From ")
                .Append(" , @Qty_In ")
                .Append(" , @Weight_In ")
                .Append(" , @Volume_In ")
                .Append(" , @Qty_Out ")
                .Append(" , @Weight_Out ")
                .Append(" , @Volume_Out ")
                .Append(" , @Qty_Sku_Bal ")
                .Append(" , @Weight_Sku_Bal ")
                .Append(" , @Volume_Sku_Bal ")
                .Append(" , @Qty_PLot_Bal ")
                .Append(" , @Weight_PLot_Bal ")
                .Append(" , @Volume_PLot_Bal ")
                .Append(" , @Qty_ItemStatus_Bal ")
                .Append(" , @Weight_ItemStatus_Bal ")
                .Append(" , @Volume_ItemStatus_Bal ")
                .Append(" , @Move_Qty ")
                .Append(" , @Qty_Variance ")
                .Append(" , @Referent_1 ")
                .Append(" , @Referent_2 ")
                .Append(" , @Qty_Item_In ")
                .Append(" , @Qty_Item_Out ")
                .Append(" , @Qty_Item_Bal ")
                .Append(" , @OrderItem_Price_In ")
                .Append(" , @OrderItem_Price_Out ")
                .Append(" , @OrderItem_Price_Bal ")
                .Append(" , @Item_Package_Index ")
                .Append(" , @Invoice_In ")
                .Append(" , @Invoice_Out ")
                .Append(" , @Pallet_No ")
                .Append(" , @Declaration_No ")
                .Append(" , @PO_No ")
                .Append(" , @SO_NO ")
                .Append(" , @Serial_No ")
                .Append(" , @Qty_Sku_Location_Bal ")
                .Append(" , @Qty_ItemStatus_Location_Bal ")
                .Append(" , @Qty_PLot_Location_Bal ")
                .Append(" , @HandlingType_Index ")
                .Append(" , @Tax1_In ")
                .Append(" , @Tax2_In ")
                .Append(" , @Tax3_In ")
                .Append(" , @Tax4_In ")
                .Append(" , @Tax5_In ")
                .Append(" , @Tax1_Out ")
                .Append(" , @Tax2_Out ")
                .Append(" , @Tax3_Out ")
                .Append(" , @Tax4_Out ")
                .Append(" , @Tax5_Out ")
                .Append(" , @Tax1_Bal ")
                .Append(" , @Tax2_Bal ")
                .Append(" , @Tax3_Bal ")
                .Append(" , @Tax4_Bal ")
                .Append(" , @Tax5_Bal ")
                .Append(" , @ERP_Location_From ")
                .Append(" , @ERP_Location_TO ")
                .Append(" , @DocumentPlan_Index ")
                .Append(" , @DocumentPlanItem_Index ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertWithdraw() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_Withdraw ")
                .Append(" ( Withdraw_Index ")
                .Append(" , Withdraw_No ")
                .Append(" , Withdraw_Date ")
                .Append(" , Departure_Date ")
                .Append(" , Arrival_Date ")
                .Append(" , Customer_Index ")
                .Append(" , Shipper_Index ")
                .Append(" , Department_Index ")
                .Append(" , Ref_No1 ")
                .Append(" , Ref_No2 ")
                .Append(" , Ref_No3 ")
                .Append(" , Ref_No4 ")
                .Append(" , Ref_No5 ")
                .Append(" , DocumentType_Index ")
                .Append(" , Contact_Name ")
                .Append(" , Comment ")
                .Append(" , Str1 ")
                .Append(" , Str2 ")
                .Append(" , Str3 ")
                .Append(" , Str4 ")
                .Append(" , Str5 ")
                .Append(" , Str6 ")
                .Append(" , Str7 ")
                .Append(" , Str8 ")
                .Append(" , Str9 ")
                .Append(" , Str10 ")
                .Append(" , Flo1 ")
                .Append(" , Flo2 ")
                .Append(" , Flo3 ")
                .Append(" , Flo4 ")
                .Append(" , Flo5 ")
                .Append(" , Status ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ")
                .Append(" , Customer_Shipping_Index ")
                .Append(" , Driver_Index ")
                .Append(" , Round ")
                .Append(" , Leave_Time ")
                .Append(" , Factory_In ")
                .Append(" , Factory_Out ")
                .Append(" , Return_Time ")
                .Append(" , Container_Index ")
                .Append(" , VehicleType_Index ")
                .Append(" , Transport_From ")
                .Append(" , Transport_To ")
                .Append(" , Checker_Name ")
                .Append(" , Driver_Name ")
                .Append(" , ApprovedBy_Name ")
                .Append(" , Container_No ")
                .Append(" , Time_InGate ")
                .Append(" , Time_OutGate ")
                .Append(" , Time_StartLoad ")
                .Append(" , Time_FinistLoad ")
                .Append(" , HandlingType_Index ")
                .Append(" , Vassel_Name ")
                .Append(" , Flight_No ")
                .Append(" , Vehicle_No ")
                .Append(" , Transport_by ")
                .Append(" , Origin_Port_Id ")
                .Append(" , Origin_Country_Id ")
                .Append(" , Destination_Port_Id ")
                .Append(" , Destination_Country_Id ")
                .Append(" , Terminal_Id ")
                .Append(" , SO_No ")
                .Append(" , Invoice_No ")
                .Append(" , ASN_No ")
                .Append(" , Withdraw_Type ")
                .Append(" , Confirm_Flag ")
                .Append(" , User_UseDoc ) ")
                .Append(" VALUES ")
                .Append(" ( @Withdraw_Index ")
                .Append(" , @Withdraw_No ")
                .Append(" , @Withdraw_Date ")
                .Append(" , @Departure_Date ")
                .Append(" , @Arrival_Date ")
                .Append(" , @Customer_Index ")
                .Append(" , @Shipper_Index ")
                .Append(" , @Department_Index ")
                .Append(" , @Ref_No1 ")
                .Append(" , @Ref_No2 ")
                .Append(" , @Ref_No3 ")
                .Append(" , @Ref_No4 ")
                .Append(" , @Ref_No5 ")
                .Append(" , @DocumentType_Index ")
                .Append(" , @Contact_Name ")
                .Append(" , @Comment ")
                .Append(" , @Str1 ")
                .Append(" , @Str2 ")
                .Append(" , @Str3 ")
                .Append(" , @Str4 ")
                .Append(" , @Str5 ")
                .Append(" , @Str6 ")
                .Append(" , @Str7 ")
                .Append(" , @Str8 ")
                .Append(" , @Str9 ")
                .Append(" , @Str10 ")
                .Append(" , @Flo1 ")
                .Append(" , @Flo2 ")
                .Append(" , @Flo3 ")
                .Append(" , @Flo4 ")
                .Append(" , @Flo5 ")
                .Append(" , @Status ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ")
                .Append(" , @Customer_Shipping_Index ")
                .Append(" , @Driver_Index ")
                .Append(" , @Round ")
                .Append(" , @Leave_Time ")
                .Append(" , @Factory_In ")
                .Append(" , @Factory_Out ")
                .Append(" , @Return_Time ")
                .Append(" , @Container_Index ")
                .Append(" , @VehicleType_Index ")
                .Append(" , @Transport_From ")
                .Append(" , @Transport_To ")
                .Append(" , @Checker_Name ")
                .Append(" , @Driver_Name ")
                .Append(" , @ApprovedBy_Name ")
                .Append(" , @Container_No ")
                .Append(" , @Time_InGate ")
                .Append(" , @Time_OutGate ")
                .Append(" , @Time_StartLoad ")
                .Append(" , @Time_FinistLoad ")
                .Append(" , @HandlingType_Index ")
                .Append(" , @Vassel_Name ")
                .Append(" , @Flight_No ")
                .Append(" , @Vehicle_No ")
                .Append(" , @Transport_by ")
                .Append(" , @Origin_Port_Id ")
                .Append(" , @Origin_Country_Id ")
                .Append(" , @Destination_Port_Id ")
                .Append(" , @Destination_Country_Id ")
                .Append(" , @Terminal_Id ")
                .Append(" , @SO_No ")
                .Append(" , @Invoice_No ")
                .Append(" , @ASN_No ")
                .Append(" , @Withdraw_Type ")
                .Append(" , @Confirm_Flag ")
                .Append(" , @User_UseDoc ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertWithdrawItem() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_WithdrawItem ")
                .Append(" ( WithdrawItem_Index ")
                .Append(" , Withdraw_Index ")
                .Append(" , Sku_Index ")
                .Append(" , Package_Index ")
                .Append(" , PLot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , Serial_No ")
                .Append(" , Qty ")
                .Append(" , Ratio ")
                .Append(" , Total_Qty ")
                .Append(" , Weight ")
                .Append(" , Volume ")
                .Append(" , Plan_Qty ")
                .Append(" , Plan_Total_Qty ")
                .Append(" , Str1 ")
                .Append(" , Str2 ")
                .Append(" , Str3 ")
                .Append(" , Str4 ")
                .Append(" , Str5 ")
                .Append(" , Flo1 ")
                .Append(" , Flo2 ")
                .Append(" , Flo3 ")
                .Append(" , Flo4 ")
                .Append(" , Flo5 ")
                .Append(" , Status ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ")
                .Append(" , ItemDefinition_Index ")
                .Append(" , Charge_Status ")
                .Append(" , Item_Qty ")
                .Append(" , Price ")
                .Append(" , Item_Package_Index ")
                .Append(" , Invoice_No ")
                .Append(" , SO_No ")
                .Append(" , PACKING_No ")
                .Append(" , Declaration_No ")
                .Append(" , HandlingType_Index ")
                .Append(" , Plan_Process ")
                .Append(" , DocumentPlan_No ")
                .Append(" , DocumentPlan_Index ")
                .Append(" , DocumentPlanItem_Index ")
                .Append(" , AssetLocationBalance_Index ")
                .Append(" , Tax1 ")
                .Append(" , Tax2 ")
                .Append(" , Tax3 ")
                .Append(" , Tax4 ")
                .Append(" , Tax5 ")
                .Append(" , HS_Code ")
                .Append(" , ItemDescription ")
                .Append(" , Seq ")
                .Append(" , Consignee_Index ")
                .Append(" , Str6 ")
                .Append(" , Str7 ")
                .Append(" , Str8 ")
                .Append(" , Str9 ")
                .Append(" , Str10 ")
                .Append(" , OrderItem_Index ")
                .Append(" , NewItemFlag ")
                .Append(" , ERP_Location ")
                .Append(" , Status_PickToLight ) ")
                .Append(" VALUES ")
                .Append(" ( @WithdrawItem_Index ")
                .Append(" , @Withdraw_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @Package_Index ")
                .Append(" , @PLot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @Serial_No ")
                .Append(" , @Qty ")
                .Append(" , @Ratio ")
                .Append(" , @Total_Qty ")
                .Append(" , @Weight ")
                .Append(" , @Volume ")
                .Append(" , @Plan_Qty ")
                .Append(" , @Plan_Total_Qty ")
                .Append(" , @Str1 ")
                .Append(" , @Str2 ")
                .Append(" , @Str3 ")
                .Append(" , @Str4 ")
                .Append(" , @Str5 ")
                .Append(" , @Flo1 ")
                .Append(" , @Flo2 ")
                .Append(" , @Flo3 ")
                .Append(" , @Flo4 ")
                .Append(" , @Flo5 ")
                .Append(" , @Status ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ")
                .Append(" , @ItemDefinition_Index ")
                .Append(" , @Charge_Status ")
                .Append(" , @Item_Qty ")
                .Append(" , @Price ")
                .Append(" , @Item_Package_Index ")
                .Append(" , @Invoice_No ")
                .Append(" , @SO_No ")
                .Append(" , @PACKING_No ")
                .Append(" , @Declaration_No ")
                .Append(" , @HandlingType_Index ")
                .Append(" , @Plan_Process ")
                .Append(" , @DocumentPlan_No ")
                .Append(" , @DocumentPlan_Index ")
                .Append(" , @DocumentPlanItem_Index ")
                .Append(" , @AssetLocationBalance_Index ")
                .Append(" , @Tax1 ")
                .Append(" , @Tax2 ")
                .Append(" , @Tax3 ")
                .Append(" , @Tax4 ")
                .Append(" , @Tax5 ")
                .Append(" , @HS_Code ")
                .Append(" , @ItemDescription ")
                .Append(" , @Seq ")
                .Append(" , @Consignee_Index ")
                .Append(" , @Str6 ")
                .Append(" , @Str7 ")
                .Append(" , @Str8 ")
                .Append(" , @Str9 ")
                .Append(" , @Str10 ")
                .Append(" , @OrderItem_Index ")
                .Append(" , @NewItemFlag ")
                .Append(" , @ERP_Location ")
                .Append(" , @Status_PickToLight ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlInsertWithdrawItemLocation() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_WithdrawItemLocation ")
                .Append(" ( WithdrawItemLocation_Index ")
                .Append(" , JobWithdraw_Index ")
                .Append(" , Withdraw_Index ")
                .Append(" , WithdrawItem_Index ")
                .Append(" , Order_Index ")
                .Append(" , Sku_Index ")
                .Append(" , Lot_No ")
                .Append(" , Plot ")
                .Append(" , ItemStatus_Index ")
                .Append(" , Tag_No ")
                .Append(" , TAG_Index ")
                .Append(" , LocationBalance_Index ")
                .Append(" , Location_Index ")
                .Append(" , Serial_No ")
                .Append(" , Qty ")
                .Append(" , Package_Index ")
                .Append(" , Total_Qty ")
                .Append(" , Weight ")
                .Append(" , Volume ")
                .Append(" , Pallet_Qty ")
                .Append(" , Status ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ")
                .Append(" , Item_Qty ")
                .Append(" , Price ")
                .Append(" , TagOut_No ")
                .Append(" , ERP_Location ")
                .Append(" , picking_by ")
                .Append(" , picking_date ) ")
                .Append(" VALUES ")
                .Append(" ( @WithdrawItemLocation_Index ")
                .Append(" , @JobWithdraw_Index ")
                .Append(" , @Withdraw_Index ")
                .Append(" , @WithdrawItem_Index ")
                .Append(" , @Order_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @Lot_No ")
                .Append(" , @Plot ")
                .Append(" , @ItemStatus_Index ")
                .Append(" , @Tag_No ")
                .Append(" , @TAG_Index ")
                .Append(" , @LocationBalance_Index ")
                .Append(" , @Location_Index ")
                .Append(" , @Serial_No ")
                .Append(" , @Qty ")
                .Append(" , @Package_Index ")
                .Append(" , @Total_Qty ")
                .Append(" , @Weight ")
                .Append(" , @Volume ")
                .Append(" , @Pallet_Qty ")
                .Append(" , @Status ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ")
                .Append(" , @Item_Qty ")
                .Append(" , @Price ")
                .Append(" , @TagOut_No ")
                .Append(" , @ERP_Location ")
                .Append(" , @picking_by ")
                .Append(" , @picking_date ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlUpdateLocationBalance() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" UPDATE tb_LocationBalance ")

                'ReCalculate Qty_Recieve_Package
                .Append(" SET Qty_Recieve_Package = CONVERT(DECIMAL(18, 6), (Qty_Bal - @Qty_Bal) / Ratio) ")

                .Append("   , Qty_Bal = Qty_Bal - @Qty_Bal ")
                .Append("   , Weight_Bal = Weight_Bal - @Weight_Bal ")
                .Append("   , Volume_Bal = Volume_Bal - @Volume_Bal ")
                .Append("   , Qty_Item_Bal = Qty_Item_Bal - @Qty_Item_Bal ")
                .Append("   , OrderItem_Price_Bal = OrderItem_Price_Bal - @OrderItem_Price_Bal ")

                .Append(" WHERE LocationBalance_Index = @LocationBalance_Index ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlUpdateLocationBalanceReserve() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" UPDATE tb_LocationBalance ")
                .Append(" SET ReserveQty = ReserveQty + @Qty_Bal ")
                .Append("   , ReserveWeight = ReserveWeight + @Weight_Bal ")
                .Append("   , ReserveVolume = ReserveVolume + @Volume_Bal ")
                .Append("   , ReserveQty_Item = ReserveQty_Item + @Qty_Item_Bal ")
                .Append("   , ReserveOrderItem_Price = ReserveOrderItem_Price + @OrderItem_Price_Bal ")
                .Append(" WHERE LocationBalance_Index = @LocationBalance_Index ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function GetSqlQtyWeightVolumeBalance(ByVal pCondition As eBalanceCondition, ByVal ParamArray pConditionValue() As String) As String
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT SUM(Qty_Bal) AS Qty, SUM(Weight_Bal) AS [Weight], SUM(Volume_Bal) AS Volume ")
                .Append(" FROM tb_LocationBalance ")
                .Append(" WHERE 1 = 1 ")

                Select Case pCondition
                    Case eBalanceCondition.Sku
                        .Append(String.Format(" AND Sku_Index = '{0}' ", pConditionValue(0)))
                        .Append(" GROUP BY Sku_Index ")

                    Case eBalanceCondition.ItemStatus
                        .Append(String.Format(" AND ItemStatus_Index = '{0}' ", pConditionValue(0)))
                        .Append(" GROUP BY ItemStatus_Index ")

                    Case eBalanceCondition.PLot
                        .Append(String.Format(" AND PLot = '{0}' ", pConditionValue(0)))
                        .Append(" GROUP BY PLot ")

                    Case eBalanceCondition.LocationSku
                        .Append(String.Format(" AND Location_Index = '{0}' ", pConditionValue(0)))
                        .Append(String.Format(" AND Sku_Index = '{0}' ", pConditionValue(1)))
                        .Append(" GROUP BY Location_Index, Sku_Index ")

                    Case eBalanceCondition.LocationItemStatus
                        .Append(String.Format(" AND Location_Index = '{0}' ", pConditionValue(0)))
                        .Append(String.Format(" AND ItemStatus_Index = '{0}' ", pConditionValue(1)))
                        .Append(" GROUP BY Location_Index, ItemStatus_Index ")

                    Case eBalanceCondition.LocationPLot
                        .Append(String.Format(" AND Location_Index = '{0}' ", pConditionValue(0)))
                        .Append(String.Format(" AND PLot = '{0}' ", pConditionValue(1)))
                        .Append(" GROUP BY Location_Index, PLot ")

                    Case Else
                        Return String.Empty
                End Select
            End With

            Return SQL.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

End Class
