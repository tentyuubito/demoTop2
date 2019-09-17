Imports WMS_STD_Formula

Public Class clsTransfer : Inherits DBType_SQLServer
#Region "Transfer"

    Public Function Auto_TransferQty() As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim RowAffected As Integer
            Dim SQL_GetBalance As System.Text.StringBuilder

            'Get LocationBalance LocationType_Index = 1 only
            SQL_GetBalance = New System.Text.StringBuilder
            With SQL_GetBalance
                .Append(" SELECT tg.Customer_Index, tg.Order_Date, sku.Product_Index, sku.ProductType_Index, lc.Location_Alias, lb.* ")
                .Append(" FROM tb_LocationBalance lb ")
                .Append(" INNER JOIN tb_TAG tg ")
                .Append(" ON lb.TAG_Index = tg.TAG_Index ")
                .Append(" INNER JOIN ms_Location lc ")
                .Append(" ON lb.Location_Index = lc.Location_Index ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT sk.Sku_Index, pd.Product_Index, pdt.ProductType_Index ")
                .Append(" 	FROM ms_SKU sk ")
                .Append(" 	INNER JOIN ms_Product pd  ")
                .Append(" 	ON sk.Product_Index = pd.Product_Index ")
                .Append(" 	INNER JOIN ms_ProductType pdt ")
                .Append(" 	ON pd.ProductType_Index = pdt.ProductType_Index ")
                .Append(" ) sku ")
                .Append(" ON lb.Sku_Index = sku.Sku_Index ")
                .Append(" INNER JOIN ms_LocationType lt ON lc.LocationType_Index = lt.LocationType_Index  ")
                .Append(" WHERE lt.LocationType_Index = 1 ")
                .Append(" AND lc.Action_Id NOT IN (3) ")
                .Append(" AND lb.Sku_Index = @Sku_Index ")
                .Append(" ORDER BY lb.Mfg_Date ,lb.Exp_Date ,lb.Plot ,tg.Order_Date ,tg.Tag_No ,lb.Qty_Bal,lc.Room,lc.Lock,lc.Row,lc.Level,lc.Depth ")
            End With

            'Insert Transfer Header

            Dim TransferStatusDocumentTypeIndex As String = "0010000000007" 'Fix Value ?
            Dim _Customer_Index As String = "0010000000001" 'Fix Value ?

            Dim AutoIndex As New Sy_AutoNumber
            Dim AutoDocumentNumber As New Sy_DocumentNumber
            Dim CurrentDate As Date = Date.Now

            'จะดึงรายการที่ MinQty มากค่ามากกว่า Total_Pickface และต้อง Total_Storage มีค่ามากกว่า Total_Pickface
            Dim DataTransfer As DataTable = DBExeQuery(GetSqlcheckMinQty, myTrans.Connection, myTrans, , eData.DataAdapter)

            If DataTransfer.Rows.Count = 0 Then
                Throw New Exception("ไม่พบข้อมูลที่ต้องโอนย้ายสินค้าอัตโนมัติ")
            End If

            Dim TransferStatusIndex As String = AutoIndex.getSys_Value(myTrans.Connection, myTrans, "TransferStatus_Index")
            Dim TransferStatusNo As String = AutoDocumentNumber.Auto_DocumentType_Number(TransferStatusDocumentTypeIndex, , CurrentDate)

            Dim QtyBalance, QtyBalanceBegin, QtyReserve, QtyRemain, Ratio, New_ReserveQty As Decimal
            Dim QtyTransfer, WeightTransfer, VolumeTransfer, QtyItemTransfer, OrderItemPriceTransfer As Decimal

            Dim Sku_Index, TransferStatusLocationIndex, ToLocationIndex As String

            Dim SqlInsertTransfer As String = GetSqlInsertTransferStatus()
            Dim SqlInsertTransferLocation As String = GetSqlInsertTransferStatusLocation()
            Dim SqlUpdateLocationBalanceReserve As String = GetSqlUpdateLocationBalanceReserve()

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@TransferStatus_Index", SqlDbType.VarChar).Value = TransferStatusIndex
                .Add("@TransferStatus_No", SqlDbType.VarChar).Value = TransferStatusNo
                .Add("@TransferStatus_Date", SqlDbType.Date).Value = CurrentDate
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = TransferStatusDocumentTypeIndex
                .Add("@Customer_Index", SqlDbType.VarChar).Value = _Customer_Index
                .Add("@Times", SqlDbType.VarChar).Value = CurrentDate.ToString("HH:mm")
                .Add("@Ref_No1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Status", SqlDbType.Int).Value = 1
                .Add("@Str1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str3", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str4", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str5", SqlDbType.VarChar).Value = String.Empty
                .Add("@Flo1", SqlDbType.Float).Value = 0
                .Add("@Flo2", SqlDbType.Float).Value = 0
                .Add("@Flo3", SqlDbType.Float).Value = 0
                .Add("@Flo4", SqlDbType.Float).Value = 0
                .Add("@Flo5", SqlDbType.Float).Value = 0
                .Add("@User_UseDoc", SqlDbType.VarChar).Value = 0
                .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With

            RowAffected = DBExeNonQuery(SqlInsertTransfer, myTrans.Connection, myTrans)

            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล TransferStatus")
            End If

            'วนจาก DataTransfer
            For Each TransferRow As DataRow In DataTransfer.Rows
                Sku_Index = TransferRow.Item("Sku_Index").ToString
                ToLocationIndex = TransferRow.Item("Location_Index").ToString
                ' จองจำนวนที่ต้องการ ได้จากการคำนวณ MinQty - (TotalLocaionBalance + Total_Qty_Transfer) ที่เป็น Pickface
                New_ReserveQty = Decimal.Parse(TransferRow.Item("Min_Qty").ToString) - (Decimal.Parse(TransferRow.Item("Total_Qty_Pickface").ToString + Decimal.Parse(TransferRow.Item("Total_Qty_Transfer").ToString)))

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Sku_Index
                End With
                Dim LocationBalance As DataTable = DBExeQuery(SQL_GetBalance.ToString, myTrans.Connection, myTrans, , eData.DataAdapter)
                'วนจาก LocationBalance ได้ข้อมูลมาจาก tb_LocationBalance ที่เป็น Storage
                'Validate LocationBalance
                If LocationBalance.Rows.Count = 0 Then
                    Throw New Exception("ไม่พบข้อมูลจาก Location Balance ที่เป็น type storage")
                End If

                For Each row As DataRow In LocationBalance.Rows

                    Ratio = Decimal.Parse(row.Item("Ratio").ToString)
                    QtyBalance = Decimal.Parse(row.Item("Qty_Bal").ToString)
                    QtyReserve = Decimal.Parse(row.Item("ReserveQty").ToString)
                    QtyBalanceBegin = Decimal.Parse(row.Item("Qty_Bal_Begin").ToString)
                    QtyRemain = IIf(New_ReserveQty < QtyBalance - QtyReserve, New_ReserveQty, QtyBalance - QtyReserve)

                    If Not New_ReserveQty > 0 Then
                        Exit For
                    End If

                    If QtyBalance < (QtyReserve) Then
                        Throw New Exception("สินค้าในระบบ มีการเปลี่ยนแปลงค่าจอง ทำให้ จำนวนไม่พอที่จะเติม")
                    End If

                    QtyTransfer = QtyRemain

                    WeightTransfer = Math.Round((Decimal.Parse(row.Item("Weight_Bal_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)
                    VolumeTransfer = Math.Round((Decimal.Parse(row.Item("Volume_Bal_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)
                    QtyItemTransfer = Math.Round((Decimal.Parse(row.Item("Qty_Item_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)
                    OrderItemPriceTransfer = Math.Round((Decimal.Parse(row.Item("OrderItem_Price_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)

                    TransferStatusLocationIndex = AutoIndex.getSys_Value(myTrans.Connection, myTrans, "TransferStatusLocation_Index")

                    'Insert tb_TransferStatusLocation
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@TransferStatusLocation_Index", SqlDbType.VarChar).Value = TransferStatusLocationIndex
                        .Add("@TransferStatus_Index", SqlDbType.VarChar).Value = TransferStatusIndex
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = row.Item("Sku_Index").ToString
                        .Add("@Order_Index", SqlDbType.VarChar).Value = row.Item("Order_Index").ToString
                        .Add("@OrderItem_Index", SqlDbType.VarChar).Value = row.Item("OrderItem_Index").ToString
                        .Add("@Lot_No", SqlDbType.VarChar).Value = row.Item("Lot_No").ToString
                        .Add("@Plot", SqlDbType.VarChar).Value = row.Item("Plot").ToString
                        .Add("@Old_ItemStatus_Index", SqlDbType.VarChar).Value = row.Item("ItemStatus_Index").ToString
                        .Add("@New_ItemStatus_Index", SqlDbType.VarChar).Value = row.Item("ItemStatus_Index").ToString
                        .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Tag_No", SqlDbType.VarChar).Value = row.Item("Tag_No").ToString
                        .Add("@Tag_NoNew", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@TAG_Index", SqlDbType.VarChar).Value = row.Item("TAG_Index").ToString
                        .Add("@Ratio", SqlDbType.Decimal).Value = Ratio
                        .Add("@Qty", SqlDbType.Decimal).Value = Math.Round(QtyTransfer / Ratio, 6)
                        .Add("@Total_Qty", SqlDbType.Decimal).Value = QtyTransfer
                        .Add("@Weight", SqlDbType.Decimal).Value = WeightTransfer
                        .Add("@Volume", SqlDbType.Decimal).Value = VolumeTransfer
                        .Add("@Item_Qty", SqlDbType.Decimal).Value = QtyItemTransfer
                        .Add("@Price", SqlDbType.Decimal).Value = OrderItemPriceTransfer
                        .Add("@From_Location_Index", SqlDbType.VarChar).Value = row.Item("Location_Index").ToString
                        .Add("@To_Location_Index", SqlDbType.VarChar).Value = ToLocationIndex
                        .Add("@PalletType_Index", SqlDbType.VarChar).Value = row.Item("PalletType_Index").ToString
                        .Add("@Pallet_Qty", SqlDbType.Decimal).Value = row.Item("Pallet_Qty").ToString
                        .Add("@From_LocationBalance_Index", SqlDbType.VarChar).Value = row.Item("LocationBalance_Index").ToString
                        .Add("@To_LocationBalance_Index", SqlDbType.VarChar).Value = row.Item("LocationBalance_Index").ToString 'LocationBalanceIndexNew
                        .Add("@AssetLocationBalance_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@MfgDate", SqlDbType.SmallDateTime).Value = row.Item("Mfg_Date").ToString
                        .Add("@ExpDate", SqlDbType.SmallDateTime).Value = row.Item("Exp_Date").ToString
                        .Add("@PallNo", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Asset_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Status", SqlDbType.Int).Value = 1
                        .Add("@Str1", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str2", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str3", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str4", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str5", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Flo1", SqlDbType.Float).Value = 0
                        .Add("@Flo2", SqlDbType.Float).Value = 0
                        .Add("@Flo3", SqlDbType.Float).Value = 0
                        .Add("@Flo4", SqlDbType.Float).Value = 0
                        .Add("@Flo5", SqlDbType.Float).Value = 0
                        .Add("@Package_Index", SqlDbType.VarChar).Value = row.Item("Package_Index").ToString
                        .Add("@Item_Package_Index", SqlDbType.VarChar).Value = row.Item("Item_Package_Index").ToString
                        .Add("@ERP_Location_TO", SqlDbType.VarChar).Value = row.Item("ERP_Location").ToString
                        .Add("@ERP_Location_From", SqlDbType.VarChar).Value = row.Item("ERP_Location").ToString
                        .Add("@NewItemFlag", SqlDbType.Int).Value = 2

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                        .Add("@MobileUser", SqlDbType.VarChar).Value = DBNull.Value ' W_Module.WV_UserName
                    End With

                    RowAffected = DBExeNonQuery(SqlInsertTransferLocation, myTrans.Connection, myTrans)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล TransferStatusLocation")
                    End If

                    'Update tb_LocationBalance
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = row.Item("LocationBalance_Index").ToString
                        .Add("@Qty_Bal", SqlDbType.Decimal).Value = QtyTransfer
                        .Add("@Weight_Bal", SqlDbType.Decimal).Value = WeightTransfer
                        .Add("@Volume_Bal", SqlDbType.Decimal).Value = VolumeTransfer
                        .Add("@Qty_Item_Bal", SqlDbType.Decimal).Value = QtyItemTransfer
                        .Add("@OrderItem_Price_Bal", SqlDbType.Decimal).Value = OrderItemPriceTransfer
                    End With

                    RowAffected = DBExeNonQuery(SqlUpdateLocationBalanceReserve, myTrans.Connection, myTrans)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล LocationBalance")
                    End If

                    New_ReserveQty = New_ReserveQty - QtyRemain
                Next

            Next


            myTrans.Commit()
            Return True
        Catch Ex As Exception
            myTrans.Rollback()
            Throw New Exception("บันทึกข้อมูลผิดพลาด ไม่สามารถ โอนย้ายเติมสินค้าได้" & vbCrLf & Ex.Message)
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function Auto_Replenish_SO(ByVal DataTransfer As DataTable) As String
        DBconnect()
        Dim Transaction As SqlClient.SqlTransaction = Connection.BeginTransaction()

        Try
            If DataTransfer Is Nothing OrElse DataTransfer.Rows.Count = 0 Then
                Throw New Exception("ไม่พบข้อมูลที่ต้องโอนย้ายสินค้าอัตโนมัติ")
            End If

            Dim RowAffected As Integer
            Dim SQL_GetBalance As System.Text.StringBuilder

            'Get LocationBalance LocationType_Index = 1 only
            SQL_GetBalance = New System.Text.StringBuilder
            With SQL_GetBalance
                .Append(" SELECT tg.Customer_Index, tg.Order_Date, sku.Product_Index, sku.ProductType_Index, lc.Location_Alias, lb.* ")
                .Append(" FROM tb_LocationBalance lb ")
                .Append(" INNER JOIN tb_TAG tg ")
                .Append(" ON lb.TAG_Index = tg.TAG_Index ")
                .Append(" INNER JOIN ms_Location lc ")
                .Append(" ON lb.Location_Index = lc.Location_Index ")
                .Append(" INNER JOIN ( ")
                .Append(" 	SELECT sk.Sku_Index, pd.Product_Index, pdt.ProductType_Index ")
                .Append(" 	FROM ms_SKU sk ")
                .Append(" 	INNER JOIN ms_Product pd  ")
                .Append(" 	ON sk.Product_Index = pd.Product_Index ")
                .Append(" 	INNER JOIN ms_ProductType pdt ")
                .Append(" 	ON pd.ProductType_Index = pdt.ProductType_Index ")
                .Append(" ) sku ")
                .Append(" ON lb.Sku_Index = sku.Sku_Index ")
                .Append(" WHERE lc.LocationType_Index = 1 ")
                .Append(" AND lc.Action_Id NOT IN (3) ")
                .Append(" AND lb.Sku_Index = @Sku_Index ")
                .Append(" AND lb.PLot = @PLot ")
                .Append(" AND lb.ItemStatus_Index = @ItemStatus_Index ")
                .Append(" AND ISNULL(lb.ERP_Location, '') = @ERP_Location ")
                .Append(" AND lb.Qty_Bal > 0 ")
                .Append(" AND lb.ReserveQty >= 0 ")
                .Append(" AND (lb.Qty_Bal - lb.ReserveQty) > 0 ")
                .Append(" ORDER BY lb.Mfg_Date, lb.Exp_Date, lb.Plot, tg.Order_Date, tg.Tag_No, lb.Qty_Bal, lc.Room, lc.Lock, lc.Row, lc.Level, lc.Depth ")
            End With

            'Insert Transfer Header

            Dim TransferStatusDocumentTypeIndex As String = "0010000000007" 'Fix Value ?
            Dim CustomerIndex As String = "0010000000001" 'Fix Value ?

            Dim AutoIndex As New Sy_AutoNumber
            Dim AutoDocumentNumber As New Sy_DocumentNumber
            Dim CurrentDate As Date = Date.Now

            Dim TransferStatusIndex As String = AutoIndex.getSys_Value("TransferStatus_Index")
            Dim TransferStatusNo As String = AutoDocumentNumber.Auto_DocumentType_Number(TransferStatusDocumentTypeIndex, , CurrentDate)

            Dim QtyBalance, QtyBalanceBegin, QtyReserve, QtyRemain, Ratio, Qty_Require As Decimal
            Dim QtyTransfer, WeightTransfer, VolumeTransfer, QtyItemTransfer, OrderItemPriceTransfer As Decimal

            Dim Sku_Id, Sku_Index, ItemStatus_Index, PLot, ERP_Location, TransferStatusLocationIndex, ToLocationIndex As String

            Dim SqlInsertTransfer As String = GetSqlInsertTransferStatus()
            Dim SqlInsertTransferLocation As String = GetSqlInsertTransferStatusLocation()
            Dim SqlUpdateLocationBalanceReserve As String = GetSqlUpdateLocationBalanceReserve()

            With SQLServerCommand.Parameters
                .Clear()

                .Add("@TransferStatus_Index", SqlDbType.VarChar).Value = TransferStatusIndex
                .Add("@TransferStatus_No", SqlDbType.VarChar).Value = TransferStatusNo
                .Add("@TransferStatus_Date", SqlDbType.Date).Value = CurrentDate
                .Add("@DocumentType_Index", SqlDbType.VarChar).Value = TransferStatusDocumentTypeIndex
                .Add("@Customer_Index", SqlDbType.VarChar).Value = CustomerIndex
                .Add("@Times", SqlDbType.VarChar).Value = CurrentDate.ToString("HH:mm")
                .Add("@Ref_No1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Ref_No2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Status", SqlDbType.Int).Value = 1
                .Add("@Str1", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str2", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str3", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str4", SqlDbType.VarChar).Value = String.Empty
                .Add("@Str5", SqlDbType.VarChar).Value = String.Empty
                .Add("@Flo1", SqlDbType.Float).Value = 0
                .Add("@Flo2", SqlDbType.Float).Value = 0
                .Add("@Flo3", SqlDbType.Float).Value = 0
                .Add("@Flo4", SqlDbType.Float).Value = 0
                .Add("@Flo5", SqlDbType.Float).Value = 0
                .Add("@User_UseDoc", SqlDbType.VarChar).Value = 0
                .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With

            RowAffected = DBExeNonQuery(SqlInsertTransfer, Transaction.Connection, Transaction)
            If Not RowAffected > 0 Then
                Throw New Exception("ไม่สามารถบันทึกข้อมูล TransferStatus")
            End If

            For Each TransferRow As DataRow In DataTransfer.Rows
                Sku_Id = TransferRow.Item("Sku_Id").ToString
                Sku_Index = TransferRow.Item("Sku_Index").ToString
                ItemStatus_Index = TransferRow.Item("ItemStatus_Index").ToString
                PLot = TransferRow.Item("PLot").ToString
                ERP_Location = TransferRow.Item("ERP_Location").ToString
                ToLocationIndex = TransferRow.Item("Location_Index").ToString
                Qty_Require = Decimal.Parse(TransferRow.Item("Total_Qty_Require").ToString)

                If String.IsNullOrEmpty(ToLocationIndex) Then
                    Throw New Exception(String.Format("ไม่สามารถเติมสินค้า [ {0} ] เนื่องจากไม่พบข้อมูล Config Pickface Location", Sku_Id))
                End If

                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@Sku_Index", SqlDbType.VarChar).Value = Sku_Index
                    .Add("@ItemStatus_Index", SqlDbType.VarChar).Value = ItemStatus_Index
                    .Add("@PLot", SqlDbType.VarChar).Value = PLot
                    .Add("@ERP_Location", SqlDbType.VarChar).Value = ERP_Location
                End With

                Dim LocationBalance As DataTable = DBExeQuery(SQL_GetBalance.ToString, Transaction.Connection, Transaction, , eData.DataAdapter)
                If LocationBalance Is Nothing OrElse LocationBalance.Rows.Count = 0 Then
                    Throw New Exception(String.Format("ไม่สามารถเติมสินค้า [ {0} ] เนื่องจากไม่พบข้อมูลสินค้านี้ที่ Storage", Sku_Id))
                End If

                QtyRemain = LocationBalance.Compute("SUM(Qty_Bal) - SUM(ReserveQty)", "")
                If Qty_Require > QtyRemain Then
                    Throw New Exception(String.Format("ไม่สามารถเติมสินค้า [ {0} ] เนื่องจากจำนวนที่ Storage ไม่เพียงพอ", Sku_Id))
                End If

                For Each Row As DataRow In LocationBalance.Rows
                    If Not Qty_Require > 0 Then
                        Exit For
                    End If

                    Ratio = Decimal.Parse(Row.Item("Ratio").ToString)
                    QtyBalance = Decimal.Parse(Row.Item("Qty_Bal").ToString)
                    QtyReserve = Decimal.Parse(Row.Item("ReserveQty").ToString)
                    QtyBalanceBegin = Decimal.Parse(Row.Item("Qty_Bal_Begin").ToString)
                    QtyRemain = QtyBalance - QtyReserve

                    If Not QtyRemain > 0 Then
                        Continue For
                    End If

                    If Qty_Require > QtyRemain Then
                        QtyTransfer = QtyRemain
                    Else
                        QtyTransfer = Qty_Require
                    End If

                    Qty_Require -= QtyTransfer

                    WeightTransfer = Math.Round((Decimal.Parse(Row.Item("Weight_Bal_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)
                    VolumeTransfer = Math.Round((Decimal.Parse(Row.Item("Volume_Bal_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)
                    QtyItemTransfer = Math.Round((Decimal.Parse(Row.Item("Qty_Item_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)
                    OrderItemPriceTransfer = Math.Round((Decimal.Parse(Row.Item("OrderItem_Price_Begin").ToString) / QtyBalanceBegin) * QtyTransfer, 6)

                    TransferStatusLocationIndex = AutoIndex.getSys_Value("TransferStatusLocation_Index")

                    'Insert tb_TransferStatusLocation
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@TransferStatusLocation_Index", SqlDbType.VarChar).Value = TransferStatusLocationIndex
                        .Add("@TransferStatus_Index", SqlDbType.VarChar).Value = TransferStatusIndex
                        .Add("@Sku_Index", SqlDbType.VarChar).Value = Row.Item("Sku_Index").ToString
                        .Add("@Order_Index", SqlDbType.VarChar).Value = Row.Item("Order_Index").ToString
                        .Add("@OrderItem_Index", SqlDbType.VarChar).Value = Row.Item("OrderItem_Index").ToString
                        .Add("@Lot_No", SqlDbType.VarChar).Value = Row.Item("Lot_No").ToString
                        .Add("@Plot", SqlDbType.VarChar).Value = Row.Item("Plot").ToString
                        .Add("@Old_ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                        .Add("@New_ItemStatus_Index", SqlDbType.VarChar).Value = Row.Item("ItemStatus_Index").ToString
                        .Add("@Serial_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Tag_No", SqlDbType.VarChar).Value = Row.Item("Tag_No").ToString
                        .Add("@Tag_NoNew", SqlDbType.VarChar).Value = DBNull.Value
                        .Add("@TAG_Index", SqlDbType.VarChar).Value = Row.Item("TAG_Index").ToString
                        .Add("@Ratio", SqlDbType.Decimal).Value = Ratio
                        .Add("@Qty", SqlDbType.Decimal).Value = Math.Round(QtyTransfer / Ratio, 6)
                        .Add("@Total_Qty", SqlDbType.Decimal).Value = QtyTransfer
                        .Add("@Weight", SqlDbType.Decimal).Value = WeightTransfer
                        .Add("@Volume", SqlDbType.Decimal).Value = VolumeTransfer
                        .Add("@Item_Qty", SqlDbType.Decimal).Value = QtyItemTransfer
                        .Add("@Price", SqlDbType.Decimal).Value = OrderItemPriceTransfer
                        .Add("@From_Location_Index", SqlDbType.VarChar).Value = Row.Item("Location_Index").ToString
                        .Add("@To_Location_Index", SqlDbType.VarChar).Value = ToLocationIndex
                        .Add("@PalletType_Index", SqlDbType.VarChar).Value = Row.Item("PalletType_Index").ToString
                        .Add("@Pallet_Qty", SqlDbType.Decimal).Value = Row.Item("Pallet_Qty").ToString
                        .Add("@From_LocationBalance_Index", SqlDbType.VarChar).Value = Row.Item("LocationBalance_Index").ToString
                        .Add("@To_LocationBalance_Index", SqlDbType.VarChar).Value = Row.Item("LocationBalance_Index").ToString 'LocationBalanceIndexNew
                        .Add("@AssetLocationBalance_Index", SqlDbType.VarChar).Value = String.Empty
                        .Add("@MfgDate", SqlDbType.SmallDateTime).Value = Row.Item("Mfg_Date").ToString
                        .Add("@ExpDate", SqlDbType.SmallDateTime).Value = Row.Item("Exp_Date").ToString
                        .Add("@PallNo", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Asset_No", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Status", SqlDbType.Int).Value = 1
                        .Add("@Str1", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str2", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str3", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str4", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Str5", SqlDbType.VarChar).Value = String.Empty
                        .Add("@Flo1", SqlDbType.Float).Value = 0
                        .Add("@Flo2", SqlDbType.Float).Value = 0
                        .Add("@Flo3", SqlDbType.Float).Value = 0
                        .Add("@Flo4", SqlDbType.Float).Value = 0
                        .Add("@Flo5", SqlDbType.Float).Value = 0
                        .Add("@Package_Index", SqlDbType.VarChar).Value = Row.Item("Package_Index").ToString
                        .Add("@Item_Package_Index", SqlDbType.VarChar).Value = Row.Item("Item_Package_Index").ToString
                        .Add("@ERP_Location_TO", SqlDbType.VarChar).Value = Row.Item("ERP_Location").ToString
                        .Add("@ERP_Location_From", SqlDbType.VarChar).Value = Row.Item("ERP_Location").ToString
                        .Add("@NewItemFlag", SqlDbType.Int).Value = 2

                        .Add("@add_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                        .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                        .Add("@MobileUser", SqlDbType.VarChar).Value = DBNull.Value ' W_Module.WV_UserName
                    End With

                    RowAffected = DBExeNonQuery(SqlInsertTransferLocation, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล TransferStatusLocation")
                    End If

                    'Update tb_LocationBalance
                    With SQLServerCommand.Parameters
                        .Clear()

                        .Add("@LocationBalance_Index", SqlDbType.VarChar).Value = Row.Item("LocationBalance_Index").ToString
                        .Add("@Qty_Bal", SqlDbType.Decimal).Value = QtyTransfer
                        .Add("@Weight_Bal", SqlDbType.Decimal).Value = WeightTransfer
                        .Add("@Volume_Bal", SqlDbType.Decimal).Value = VolumeTransfer
                        .Add("@Qty_Item_Bal", SqlDbType.Decimal).Value = QtyItemTransfer
                        .Add("@OrderItem_Price_Bal", SqlDbType.Decimal).Value = OrderItemPriceTransfer
                    End With

                    RowAffected = DBExeNonQuery(SqlUpdateLocationBalanceReserve, Transaction.Connection, Transaction)
                    If Not RowAffected > 0 Then
                        Throw New Exception("ไม่สามารถบันทึกข้อมูล LocationBalance")
                    End If
                Next
            Next

            Transaction.Commit()
            Return TransferStatusNo

        Catch Ex As Exception
            Transaction.Rollback()
            Throw New Exception("บันทึกข้อมูลผิดพลาด" & vbCrLf & Ex.Message)
        End Try
    End Function

    Public Function GetSqlcheckMinQty() As String

        Dim strSQL As String = ""
        Try

            strSQL = " SELECT SKU.Sku_Index, SKU.Sku_Id , SKU.Location_Index,  SKU.Location_Id ,SKU.Min_Qty "
            strSQL &= " 	 , ISNULL(LB_Pick.TotalQty,0) AS Total_Qty_Pickface "
            strSQL &= " 	 , ISNULL(LB_Store.TotalQty,0) AS Total_Qty_Storage "
            strSQL &= " 	 , ISNULL(TFS.Total_Qty_Transfer,0) AS Total_Qty_Transfer "
            strSQL &= " FROM ( "
            strSQL &= " 		SELECT sku.Sku_Index, sku.Sku_Id , ISNULL(sku.Location_Index,'') AS Location_Index, lo.Location_Id, sku.Min_Qty "
            strSQL &= " 		FROM ms_SKU sku (NOLOCK) "
            strSQL &= " 		INNER JOIN ms_Location lo ON sku.Location_Index = lo.Location_Index "
            strSQL &= " 		WHERE ISNULL(sku.Location_Index,'') <> '' AND sku.Min_Qty > 0  "
            strSQL &= " 		AND sku.status_id <> -1 "
            strSQL &= " ) SKU "
            strSQL &= " LEFT JOIN  ( "
            strSQL &= " 		SELECT lb.Sku_Index, lb.Location_Index, SUM(lb.Qty_Bal) AS TotalQty "
            strSQL &= " 		FROM tb_LocationBalance lb (NOLOCK) "
            strSQL &= " 		INNER JOIN ms_Location lo ON lb.Location_Index = lo.Location_Index "
            strSQL &= " 		WHERE lb.Qty_Bal > 0 AND lo.LocationType_Index = 2 "
            strSQL &= " 		AND lo.status_id <> -1 "
            strSQL &= " 		GROUP BY lb.Sku_Index,lb.Location_Index,lo.status_id "
            strSQL &= " ) LB_Pick "
            strSQL &= " ON LB_Pick.Sku_Index = SKU.Sku_Index AND LB_Pick.Location_Index = SKU.Location_Index "
            strSQL &= " LEFT JOIN  ( "
            strSQL &= " 		SELECT lb.Sku_Index, SUM(lb.Qty_Bal - lb.ReserveQty) AS TotalQty "
            strSQL &= " 		FROM tb_LocationBalance lb (NOLOCK) "
            strSQL &= " 		INNER JOIN ms_Location lo ON lb.Location_Index = lo.Location_Index "
            strSQL &= " 		WHERE lb.Qty_Bal > 0 AND lo.LocationType_Index = 1 "
            strSQL &= " 		AND lo.status_id <> -1 "
            strSQL &= " 		GROUP BY lb.Sku_Index,lo.status_id "
            strSQL &= " ) LB_Store "
            strSQL &= " ON LB_Store.Sku_Index = SKU.Sku_Index  "
            strSQL &= " LEFT JOIN  ( "
            strSQL &= " 		SELECT tsl.Sku_Index, tsl.From_Location_Index , tsl.To_Location_Index, tsl.Status,SUM(Qty) AS Total_Qty_Transfer "
            strSQL &= " 		FROM tb_TransferStatus ts "
            strSQL &= " 		INNER JOIN tb_TransferStatusLocation tsl ON ts.TransferStatus_Index = tsl.TransferStatus_Index "
            strSQL &= " 		WHERE ts.Status <> -1 AND tsl.Status <> -1 "
            strSQL &= " 		GROUP BY tsl.Sku_Index, tsl.From_Location_Index , tsl.To_Location_Index ,tsl.Status "
            strSQL &= " ) TFS "
            strSQL &= " ON TFS.Sku_Index = SKU.Sku_Index  "
            strSQL &= " AND TFS.To_Location_Index = SKU.Location_Index "
            strSQL &= " WHERE ISNULL(LB_Pick.TotalQty,0) < ISNULL(SKU.Min_Qty,0) "
            strSQL &= " AND ISNULL(LB_Store.TotalQty,0) > 0 "
            strSQL &= " AND (ISNULL(LB_Pick.TotalQty,0) + ISNULL(TFS.Total_Qty_Transfer,0)) < ISNULL(SKU.Min_Qty,0) "

            Return strSQL.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetSqlcheckMinQtyBySO(ByVal SalesOrderList() As String) As String

        Dim strSQL As String = ""
        Dim lstSO As String = ""
        For Each soIndex As String In SalesOrderList
            lstSO &= "'" & soIndex & "',"
        Next
        lstSO = IIf(lstSO.Length > 0, lstSO.Substring(0, lstSO.Length - 1), "")

        Try

            strSQL = " SELECT SKU.Sku_Index, SKU.Sku_Id , SKU.Location_Index,  SKU.Location_Id ,SKU.Min_Qty "
            strSQL &= " 	 , ISNULL(So.TotalQty,0) AS Total_Qty_SO "
            strSQL &= " 	 , ISNULL(LB_Pick.TotalQty,0) AS Total_Qty_Pickface "
            strSQL &= " 	 , ISNULL(LB_Store.TotalQty,0) AS Total_Qty_Storage "
            strSQL &= " 	 , ISNULL(TFS.Total_Qty_Transfer,0) AS Total_Qty_Transfer "
            strSQL &= " FROM ( "
            strSQL &= " 		SELECT sku.Sku_Index, sku.Sku_Id , ISNULL(sku.Location_Index,'') AS Location_Index, ISNULL(lo.Location_Id,'') AS Location_Id, ISNULL(sku.Min_Qty,0) as Min_Qty "
            strSQL &= " 		FROM ms_SKU sku (NOLOCK) "
            strSQL &= " 		INNER JOIN ms_Location lo ON sku.Location_Index = lo.Location_Index "
            'WHERE ISNULL(sku.Location_Index,'') <> '' AND sku.Min_Qty > 0 
            strSQL &= " 		WHERE ISNULL(sku.Location_Index,'') <> ''  "
            strSQL &= " 		AND sku.status_id <> -1 "
            strSQL &= " ) SKU "
            strSQL &= " INNER JOIN ( "
            strSQL &= " 		SELECT so.Sku_Index, so.ItemStatus_Index, ISNULL(so.PLot, '') AS PLot, ISNULL(so.ERP_Location, '') AS ERP_Location  "
            strSQL &= " 		 , SUM(so.Total_Qty) AS TotalQty ,sku.Location_Index "
            strSQL &= " 		FROM tb_SalesOrderItem so (NOLOCK)  "
            strSQL &= " 		INNER JOIN ms_SKU sku ON so.Sku_Index = sku.Sku_Index "
            strSQL &= " 		WHERE Status <> -1   "
            strSQL &= " 		AND SalesOrder_Index IN (" & lstSO & ")  "
            strSQL &= " 		GROUP BY so.Sku_Index, so.ItemStatus_Index, ISNULL(so.PLot, ''), ISNULL(so.ERP_Location, '') ,sku.Location_Index "
            strSQL &= " ) SO "
            strSQL &= " ON SO.Sku_Index = SKU.Sku_Index  "
            strSQL &= " AND SO.Location_Index = SKU.Location_Index "
            strSQL &= " LEFT JOIN  ( "
            strSQL &= " 		SELECT lb.Sku_Index, lb.Location_Index,ISNULL(lb.PLot,'') AS PLot, SUM(lb.Qty_Bal) AS TotalQty "
            strSQL &= " 		FROM tb_LocationBalance lb (NOLOCK) "
            strSQL &= " 		INNER JOIN ms_Location lo ON lb.Location_Index = lo.Location_Index "
            strSQL &= " 		WHERE lb.Qty_Bal > 0 AND lo.LocationType_Index = 2 "
            strSQL &= "         AND Sku_Index IN (  "
            strSQL &= " 	        SELECT Sku_Index   "
            strSQL &= " 	        FROM tb_SalesOrderItem (NOLOCK)  "
            strSQL &= " 	        WHERE Status <> -1  "
            strSQL &= " 	        AND SalesOrder_Index IN (" & lstSO & ")  "
            strSQL &= " 	        GROUP BY Sku_Index  "
            strSQL &= "         )  "
            strSQL &= " 		AND lo.status_id <> -1 "
            strSQL &= " 		GROUP BY lb.Sku_Index,lb.Location_Index,ISNULL(lb.PLot,''),lo.status_id "
            strSQL &= " ) LB_Pick "
            strSQL &= " ON LB_Pick.Sku_Index = SKU.Sku_Index  "
            strSQL &= " AND LB_Pick.Location_Index = SKU.Location_Index "
            strSQL &= " AND LB_Pick.PLot = SO.PLot "
            strSQL &= " LEFT JOIN  ( "
            strSQL &= " 		SELECT lb.Sku_Index, SUM(lb.Qty_Bal - lb.ReserveQty) AS TotalQty "
            strSQL &= " 		FROM tb_LocationBalance lb (NOLOCK) "
            strSQL &= " 		INNER JOIN ms_Location lo ON lb.Location_Index = lo.Location_Index "
            strSQL &= " 		WHERE lb.Qty_Bal > 0 AND lo.LocationType_Index = 1 "
            strSQL &= "         AND Sku_Index IN (  "
            strSQL &= " 	        SELECT Sku_Index   "
            strSQL &= " 	        FROM tb_SalesOrderItem (NOLOCK)  "
            strSQL &= " 	        WHERE Status <> -1  "
            strSQL &= " 	        AND SalesOrder_Index IN (" & lstSO & ")  "
            strSQL &= " 	        GROUP BY Sku_Index  "
            strSQL &= "         )  "
            strSQL &= " 		AND lo.status_id <> -1 "
            strSQL &= " 		GROUP BY lb.Sku_Index,lo.status_id "
            strSQL &= " ) LB_Store "
            strSQL &= " ON LB_Store.Sku_Index = SKU.Sku_Index  "
            strSQL &= " LEFT JOIN  ( "
            strSQL &= " 		SELECT tsl.Sku_Index, tsl.From_Location_Index , tsl.To_Location_Index, tsl.Status,SUM(tsl.Qty) AS Total_Qty_Transfer "
            strSQL &= " 		FROM tb_TransferStatus ts "
            strSQL &= " 		INNER JOIN tb_TransferStatusLocation tsl ON ts.TransferStatus_Index = tsl.TransferStatus_Index "
            strSQL &= " 		WHERE ts.Status <> -1 AND tsl.Status <> -1 "
            strSQL &= " 		GROUP BY tsl.Sku_Index, tsl.From_Location_Index , tsl.To_Location_Index ,tsl.Status "
            strSQL &= " ) TFS "
            strSQL &= " ON TFS.Sku_Index = SKU.Sku_Index  "
            strSQL &= " AND TFS.To_Location_Index = SKU.Location_Index "
            'WHERE ISNULL(LB_Pick.TotalQty,0) < ISNULL(SKU.Min_Qty,0) AND ISNULL(LB_Store.TotalQty,0) > 0
            strSQL &= " WHERE  ISNULL(LB_Store.TotalQty,0) > 0 "
            strSQL &= " AND ISNULL(LB_Store.TotalQty,0) > (ISNULL(LB_Pick.TotalQty,0) + ISNULL(TFS.Total_Qty_Transfer,0))  "
            strSQL &= " AND ISNULL(SO.TotalQty,0) > (ISNULL(LB_Pick.TotalQty,0) + ISNULL(TFS.Total_Qty_Transfer,0)) "

            Return strSQL.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function GetSqlInsertTransferStatus() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_TransferStatus ")
                .Append(" ( TransferStatus_Index ")
                .Append(" , TransferStatus_No ")
                .Append(" , TransferStatus_Date ")
                .Append(" , DocumentType_Index ")
                .Append(" , Customer_Index ")
                .Append(" , Times ")
                .Append(" , Ref_No1 ")
                .Append(" , Ref_No2 ")
                .Append(" , Status ")
                .Append(" , Str1, Str2, Str3, Str4, Str5 ")
                .Append(" , Flo1, Flo2, Flo3, Flo4, Flo5 ")
                .Append(" , User_UseDoc ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ) ")
                .Append(" VALUES ")
                .Append(" ( @TransferStatus_Index ")
                .Append(" , @TransferStatus_No ")
                .Append(" , @TransferStatus_Date ")
                .Append(" , @DocumentType_Index ")
                .Append(" , @Customer_Index ")
                .Append(" , @Times ")
                .Append(" , @Ref_No1 ")
                .Append(" , @Ref_No2 ")
                .Append(" , @Status ")
                .Append(" , @Str1, @Str2, @Str3, @Str4, @Str5 ")
                .Append(" , @Flo1, @Flo2, @Flo3, @Flo4, @Flo5 ")
                .Append(" , @User_UseDoc ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Private Function GetSqlInsertTransferStatusLocation() As String
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" INSERT INTO tb_TransferStatusLocation ")
                .Append(" ( TransferStatusLocation_Index ")
                .Append(" , TransferStatus_Index ")
                .Append(" , Sku_Index ")
                .Append(" , Order_Index ")
                .Append(" , OrderItem_Index ")
                .Append(" , Lot_No ")
                .Append(" , Plot ")
                .Append(" , Old_ItemStatus_Index ")
                .Append(" , New_ItemStatus_Index ")
                .Append(" , Serial_No ")
                .Append(" , Tag_No ")
                .Append(" , Tag_NoNew ")
                .Append(" , Tag_Index ")
                .Append(" , Ratio ")
                .Append(" , Qty ")
                .Append(" , Total_Qty ")
                .Append(" , Weight ")
                .Append(" , Volume ")
                .Append(" , Item_Qty ")
                .Append(" , Price ")
                .Append(" , From_Location_Index ")
                .Append(" , To_Location_Index ")
                .Append(" , PalletType_Index ")
                .Append(" , Pallet_Qty ")
                .Append(" , From_LocationBalance_Index ")
                .Append(" , To_LocationBalance_Index ")
                .Append(" , AssetLocationBalance_Index ")
                .Append(" , MfgDate ")
                .Append(" , ExpDate ")
                .Append(" , PallNo ")
                .Append(" , Asset_No ")
                .Append(" , Status ")
                .Append(" , Str1, Str2, Str3, Str4, Str5 ")
                .Append(" , Flo1, Flo2, Flo3, Flo4, Flo5 ")
                .Append(" , Package_Index ")
                .Append(" , Item_Package_Index ")
                .Append(" , ERP_Location_TO ")
                .Append(" , ERP_Location_From ")
                .Append(" , NewItemFlag ")
                .Append(" , Transfer_By ")
                .Append(" , Transfer_Date ")
                .Append(" , add_by ")
                .Append(" , add_date ")
                .Append(" , add_branch ) ")
                .Append(" VALUES ")
                .Append(" ( @TransferStatusLocation_Index ")
                .Append(" , @TransferStatus_Index ")
                .Append(" , @Sku_Index ")
                .Append(" , @Order_Index ")
                .Append(" , @OrderItem_Index ")
                .Append(" , @Lot_No ")
                .Append(" , @Plot ")
                .Append(" , @Old_ItemStatus_Index ")
                .Append(" , @New_ItemStatus_Index ")
                .Append(" , @Serial_No ")
                .Append(" , @Tag_No ")
                .Append(" , @Tag_NoNew ")
                .Append(" , @Tag_Index ")
                .Append(" , @Ratio ")
                .Append(" , @Qty ")
                .Append(" , @Total_Qty ")
                .Append(" , @Weight ")
                .Append(" , @Volume ")
                .Append(" , @Item_Qty ")
                .Append(" , @Price ")
                .Append(" , @From_Location_Index ")
                .Append(" , @To_Location_Index ")
                .Append(" , @PalletType_Index ")
                .Append(" , @Pallet_Qty ")
                .Append(" , @From_LocationBalance_Index ")
                .Append(" , @To_LocationBalance_Index ")
                .Append(" , @AssetLocationBalance_Index ")
                .Append(" , @MfgDate ")
                .Append(" , @ExpDate ")
                .Append(" , @PallNo ")
                .Append(" , @Asset_No ")
                .Append(" , @Status ")
                .Append(" , @Str1, @Str2, @Str3, @Str4, @Str5 ")
                .Append(" , @Flo1, @Flo2, @Flo3, @Flo4, @Flo5 ")
                .Append(" , @Package_Index ")
                .Append(" , @Item_Package_Index ")
                .Append(" , @ERP_Location_TO ")
                .Append(" , @ERP_Location_From ")
                .Append(" , @NewItemFlag ")
                .Append(" , NULL ")
                .Append(" , NULL ")
                .Append(" , @add_by ")
                .Append(" , GETDATE() ")
                .Append(" , @add_branch ) ")
            End With

            Return Sql.ToString

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Private Function GetSqlUpdateLocationBalanceReserve() As String
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

#End Region

End Class
