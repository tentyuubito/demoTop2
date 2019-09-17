Imports WMS_STD_Formula
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_Master_Datalayer
Public Class GrouppingConditionPicking : Inherits DBType_SQLServer

    Private builder As New System.Text.StringBuilder
    ''' <summary>
    ''' FIFO : แบบมีตำแหน่ง Pick face กับ Storage เท่านั้น 
    ''' มีการเติมจาก Storage ไป pick face 
    ''' </summary>
    ''' <param name="Withdraw_Index"></param>
    ''' <param name="DocumentType_Index"></param>
    ''' <param name="dtSalesOrderPlan"></param>
    ''' <param name="dtSalesOrderGroup"></param>
    ''' <param name="pconfigFullFill"></param>
    ''' <param name="_USE_PICKFACE_RACK_TAG"></param>
    ''' <param name="_USE_PICKFACE_BUSKET_TAG"></param>
    ''' <param name="_Connection"></param>
    ''' <param name="_myTrans"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function CreateAutoPickingWithdraw_v4(ByVal Withdraw_Index As String, ByVal DocumentType_Index As String, ByVal dtSalesOrderPlan As DataTable, ByVal dtSalesOrderGroup As DataTable, Optional ByVal pconfigFullFill As Boolean = True, Optional ByVal _USE_PICKFACE_RACK_TAG As Boolean = False, Optional ByVal _USE_PICKFACE_BUSKET_TAG As Boolean = False, _
Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim irow As Integer = 0
        Try
            'หมายเหตุ : ถ้าต้องการให้ Rack เป็นตะกร้าและมี TAG ต้องแก้ไขไม่ให้ TAG เป็นตำแหน่งตอนเติมเต็มสินค้า
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True

            End If
            If Withdraw_Index = "" Then
                Throw New Exception("Withdraw not found!!!")
            End If
            If dtSalesOrderPlan.Rows.Count = 0 Then
                Throw New Exception("SalesOrder not found!!!")
            End If
            If dtSalesOrderGroup.Rows.Count = 0 Then
                Throw New Exception("Group SalesOrder not found!!!")
            End If

            Dim TypePicking As New WMS_Site_TopCharoen.PICKING.enmPicking_Type
            Dim Pick_Result As New DataTable
            Dim Pick_ResultAll As New DataTable
            Dim objPicking As New WMS_Site_TopCharoen.PICKING(WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO)

            Dim boolFullFill As Boolean = False

            Dim xdrSelect() As DataRow
            Dim xQuery As String = ""
            For Each drAutoPicking As DataRow In dtSalesOrderGroup.Rows
                'If irow = 182 Then
                '    Dim xStop As String = "STOP"
                'End If

                irow += 1
                If CDec(drAutoPicking("Total_Qty").ToString) = 0 Then Continue For
                Dim TransSAVE As String = Now.ToString("yyyyddMMHHmmssfff")
                _myTrans.Save(TransSAVE)
                Dim strWhere As String = ""
                Dim xWhere As New System.Text.StringBuilder
                Dim dtLocationBalance As New DataTable
                Dim Result As New DataTable
                With drAutoPicking
                    Dim SKU_Id As String = .Item("SKU_Id").ToString
                    Dim Total_Qty As Decimal = CDec(.Item("Total_Qty").ToString)
                    Dim Qty_Per_Pallet As Decimal = IIf(CDec(.Item("Qty_Per_Pallet").ToString) <= 0, 1, CDec(.Item("Qty_Per_Pallet").ToString))
                    Dim xLocationType_Index As String = ""
                    Dim Picking_Qty As Decimal = 0 'จำนวนสั่งหยิบ
                    Dim Pick_Floor As Decimal = 0 'จำนวนเต็มพาเลทสเปค
                    Dim Pick_Mod As Decimal = 0 'จำนวนเศษ
                    Pick_Floor = Math.Floor(Total_Qty / Qty_Per_Pallet) 'จำนวนเต็มพาเลทสเปค
                    Pick_Mod = (Total_Qty Mod Qty_Per_Pallet) 'จำนวนเศษ

                    'xWhere = xWhere.Append(StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, _
                    '            .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT"), .Item("LastExp_date").ToString, .Item("MFG_Day_Count").ToString, _
                    '           _Connection, _myTrans))

                    xWhere = xWhere.Append(StrWherePicking_Row("FIFO", drAutoPicking, _Connection, _myTrans))

                    If .Item("Sku_Index").ToString = "0010000000670" Then
                        Dim a As String = "aaa"
                    End If
                    '1	001	Storage (Rack)
                    '2	002	Pickface (Rack)
                    '3	003	Pickface (Busket)
                    While (0 < Total_Qty)
                        '--------------------------------------------------------------------------------------------------
                        'New version ทำทั้งคลังทีเดียวทีละ SKU
                        'STEP 1.2 : Grouping tag. ก่อนหยิบทั้งพาเลท
                        TypePicking = WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO
                        xLocationType_Index = ""
                        Picking_Qty = 0
                        strWhere = xWhere.ToString
                        objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                        Pick_ResultAll = New DataTable
                        If .Item("FlagMix_Lot").ToString = "" Then
                            .Item("FlagMix_Lot") = False
                        End If
                        If .Item("FlagMix_Lot") Then
                            'Search Group by tag lot
                            Pick_ResultAll = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_TopCharoen.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, .Item("FlagMix_Lot"), _Connection, _myTrans, SQLServerCommand)
                            If Pick_ResultAll.Rows.Count = 0 Then
                                Continue For
                            Else
                                'Mix lot
                                Pick_ResultAll.Columns.Add("X", GetType(String))
                                Dim primaryKey(1) As DataColumn
                                primaryKey(0) = Pick_ResultAll.Columns("TAG_No")
                                primaryKey(1) = Pick_ResultAll.Columns("Mfg_Date")
                                Pick_ResultAll.PrimaryKey = primaryKey
                                Dim xLoop As Boolean = True
                                Dim xTotalQty As Double = Total_Qty
                                Dim dtMixLot As New DataTable
                                dtMixLot.Columns.Add("TAG_No", GetType(String))
                                dtMixLot.Columns.Add("Mfg_Date", GetType(String))
                                dtMixLot.Columns.Add("Qty_Bal", GetType(Decimal))
                                While xLoop
                                    Dim xxArrdr() As DataRow = Pick_ResultAll.Select("ISNULL(X,'') <> 'X'", "RowNum")
                                    If xxArrdr.Length > 0 Then
                                        'Update
                                        If dtMixLot.Rows.Count > 0 Then dtMixLot.Rows.Clear() 'Reset
                                        Dim oKey(1) As Object
                                        oKey(0) = xxArrdr(0)("TAG_No").ToString
                                        oKey(1) = xxArrdr(0)("Mfg_Date")

                                        'Dim xxArrdr2() As DataRow = Pick_ResultAll.Select(String.Format("TAG_No <> '{0}' and Mfg_Date <> '{1}'", xxArrdr(0)("TAG_No").ToString, xxArrdr(0)("Mfg_Date").ToString), "RowNum")
                                        Dim xxArrdr2() As DataRow = Pick_ResultAll.Select("ISNULL(X,'') <> 'X'", "RowNum")
                                        Dim drNewrow As DataRow
                                        drNewrow = Pick_ResultAll.Rows.Find(oKey) 'Key_No1 = "001",'Key_No2 = "002"
                                        drNewrow.BeginEdit()
                                        drNewrow("X") = "X"
                                        drNewrow.EndEdit()
                                        If xxArrdr2.Length > 0 Then
                                            'เพิ่มตัวเอง
                                            Dim xdrNewrow As DataRow = dtMixLot.NewRow
                                            xdrNewrow("TAG_No") = xxArrdr(0)("TAG_No")
                                            xdrNewrow("Mfg_Date") = xxArrdr(0)("Mfg_Date")
                                            xdrNewrow("Qty_Bal") = xxArrdr(0)("Qty_Bal")
                                            dtMixLot.Rows.Add(xdrNewrow)
                                            For Each yRow As DataRow In xxArrdr2
                                                If (dtMixLot.Compute("SUM(Qty_Bal)", "") + yRow("Qty_Bal") < xTotalQty) And (.Item("NumFlagMix_Lot") = (dtMixLot.Rows.Count + 1)) Then
                                                    Continue For
                                                End If

                                                xdrNewrow = dtMixLot.NewRow
                                                xdrNewrow("TAG_No") = yRow("TAG_No")
                                                xdrNewrow("Mfg_Date") = yRow("Mfg_Date")
                                                xdrNewrow("Qty_Bal") = yRow("Qty_Bal")
                                                dtMixLot.Rows.Add(xdrNewrow)
                                                'End If
                                                If .Item("NumFlagMix_Lot") = dtMixLot.Rows.Count Then Exit For
                                                If dtMixLot.Compute("SUM(Qty_Bal)", "") >= xTotalQty Then
                                                    Exit For
                                                End If
                                            Next
                                            If dtMixLot.Compute("SUM(Qty_Bal)", "") >= xTotalQty Then
                                                xLoop = False
                                                Exit While
                                            Else
                                                Continue While
                                            End If
                                        End If
                                    Else
                                        xLoop = False
                                        Exit While
                                    End If
                                End While

                                If dtMixLot.Rows.Count > 0 Then
                                    If dtMixLot.Compute("SUM(Qty_Bal)", "") < xTotalQty Then
                                        'Exit While 'Close
                                        Continue For
                                    End If

                                    'TO DO Loop add condition
                                    Dim xSQLMix_TAG As String = " AND TAG_No IN ("
                                    Dim xSQLMix_Lot As String = " AND CAST(Mfg_Date as DATE) IN ("
                                    For xi As Integer = 0 To dtMixLot.Rows.Count - 1
                                        xSQLMix_TAG &= "'" & dtMixLot.Rows(xi)("TAG_No").ToString & "'"
                                        'xSQLMix_Lot &= "'" & dtMixLot.Rows(xi)("Mfg_Date").ToString & "'"
                                        xSQLMix_Lot &= "'" & CDate(dtMixLot.Rows(xi)("Mfg_Date")).ToString("yyyy-MM-dd") & "'"
                                        If xi = dtMixLot.Rows.Count - 1 Then
                                            xSQLMix_TAG &= ")"
                                            xSQLMix_Lot &= ")"
                                        Else
                                            xSQLMix_TAG &= ","
                                            xSQLMix_Lot &= ","
                                        End If
                                    Next
                                    xWhere = xWhere.Append(xSQLMix_Lot)
                                    xWhere = xWhere.Append(xSQLMix_TAG)
                                Else
                                    Continue For
                                End If

                            End If

                        End If
                        'Search Group by tag
                        Picking_Qty = 0
                        strWhere = xWhere.ToString
                        objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                        Pick_ResultAll = New DataTable
                        Pick_ResultAll = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_TopCharoen.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, False, _Connection, _myTrans, SQLServerCommand)
                        Pick_ResultAll.PrimaryKey = New DataColumn() {Pick_ResultAll.Columns("TAG_No")}
                        If Pick_ResultAll.Rows.Count = 0 Then
                            Continue For
                        End If
                        '--------------------------------------------------------------------------------------------------
                        'STEP 1 : FIFO Storage Full Pallet Spec.
                        xLocationType_Index = "1"
                        Picking_Qty = Qty_Per_Pallet
                        xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
                        xdrSelect = Pick_ResultAll.Select(xQuery, "RowNum")
                        If xdrSelect.Length > 0 Then
                            While (Pick_Floor > 0)
                                Picking_Qty = Qty_Per_Pallet
                                For Each drFIFO As DataRow In xdrSelect
                                    Picking_Qty = IIf(Total_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
                                    If Picking_Qty = 0 Then Continue For
                                    'BEGIN : FIFO
                                    TypePicking = WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO
                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No").ToString & "' "
                                    strWhere = xWhere.ToString
                                    strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
                                    objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 2, Withdraw_Index, Addition)
                                    'END : FIFO
                                    If Result IsNot Nothing Then
                                        If Result.Rows.Count > 0 Then
                                            dtLocationBalance.Merge(Result)
                                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
                                            Picking_Qty = Total_Qty
                                            Pick_Floor -= 1
                                            'Update data all tag
                                            Dim drNewrow As DataRow
                                            drNewrow = Pick_ResultAll.NewRow
                                            drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
                                            drNewrow.BeginEdit()
                                            drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                            drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                            drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                            drNewrow.EndEdit()
                                        End If
                                        If Total_Qty <= 0 Then Exit While
                                    End If
                                Next
                                'Set End loop
                                Pick_Floor = 0
                            End While
                        End If

                        If Total_Qty <= 0 Then Exit While
                        '--------------------------------------------------------------------------------------------------
                        'STEP 3 : FIFO Pickface
                        Picking_Qty = Total_Qty
                        'xLocationType_Index = "3"'STEP 3 : FIFO Pickface ตระกล้า
                        xLocationType_Index = "2"
                        xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal >= {1}", xLocationType_Index, 0)
                        xdrSelect = Pick_ResultAll.Select(xQuery, "RowNum")
                        If xdrSelect.Length > 0 Then
                            For Each drFIFO As DataRow In xdrSelect
                                'BEGIN : FIFO
                                Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), Picking_Qty)
                                If Picking_Qty = 0 Then Continue For
                                TypePicking = WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO
                                Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
                                strWhere = xWhere.ToString
                                strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
                                objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 2, Withdraw_Index, Addition)
                                'END : FIFO
                                If Result IsNot Nothing Then
                                    If Result.Rows.Count > 0 Then
                                        dtLocationBalance.Merge(Result)
                                        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
                                        Picking_Qty = Total_Qty
                                        Pick_Floor -= 1
                                        'Update data all tag
                                        Dim drNewrow As DataRow
                                        drNewrow = Pick_ResultAll.NewRow
                                        drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
                                        drNewrow.BeginEdit()
                                        drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                        drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                        drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                        drNewrow.EndEdit()

                                    End If
                                End If
                                If Total_Qty <= 0 Then Exit While
                            Next
                        End If




                        If pconfigFullFill Then 'มีการเติมเต็มหรือไม่ค่าส่งมาจากหน้าจอ
                            '--------------------------------------------------------------------------------------------------
                            '**************************************************************************************************
                            '*******************************                มีการเติมเต็มสินค้า                                 **********************
                            '**************************************************************************************************
                            '--------------------------------------------------------------------------------------------------
                            'STEP 4 : FIFO ทั้งคลัง,แบบมีเติมเต็มสินค้า
                            'STEP 4.1 : FIFO ทั้งคลัง,ที่ตำแหน่ง Pick face rack
                            While Total_Qty > 0
                                Picking_Qty = Total_Qty
                                'xLocationType_Index = "2"
                                xQuery = String.Format("LocationType_Index in ('1') and Qty_Bal >= {0}", 0)
                                xdrSelect = Pick_ResultAll.Select(xQuery, "LocationType_Index desc,RowNum")
                                If xdrSelect.Length > 0 Then
                                    For Each drFIFO As DataRow In xdrSelect
                                        'เป็นเศษจริง สั่งเติมสินค้า จองสินค้าเต็ม TAG
                                        Picking_Qty = drFIFO("Qty_Bal")
                                        If Picking_Qty = 0 Then Continue For
                                        'ค้นหาตำแหน่งที่ต้องเติมเศษ
                                        'ต้องหาตำแหน่งที่สามารถเติมสินค้าได้
                                        '-------------------------------------------------------------------------------------
                                        Dim xTo_Location_Index As String = ""
                                        Dim xTo_TAG_No As String = ""
                                        Dim xTotal_Qty As Decimal = Picking_Qty
                                        Dim xWeightOut As Decimal = Picking_Qty * drAutoPicking("Weight_STD")
                                        Dim xVolumeOut As Decimal = Picking_Qty * drAutoPicking("Volume_STD")
                                        Dim xAdditional As String = ""
                                        xLocationType_Index = "2"
                                        xAdditional = String.Format(" ' And ml.LocationType_Index in ({0},{1}) '", "2", "3")
                                        'End Select
                                        xQuery = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
                                                                                    drAutoPicking("Sku_Index").ToString, _
                                                                                    drAutoPicking("ProductType_Index").ToString, _
                                                                                    drAutoPicking("ItemStatus_Index").ToString, _
                                                                                    "", _
                                                                                    drAutoPicking("Customer_Index").ToString, _
                                                                                    xTotal_Qty, xWeightOut, xVolumeOut, _
                                                                                    xAdditional)
                                        Dim SuggestLocation As DataTable = DBExeQuery(xQuery, _Connection, _myTrans)
                                        If SuggestLocation.Rows.Count > 0 Then
                                            xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
                                            xTo_TAG_No = drFIFO("TAG_No")

                                            If _USE_PICKFACE_RACK_TAG Then
                                                xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString 'ตะกร้า TAG NO = Location
                                            End If

                                            'Alert
                                            If boolFullFill = False Then
                                                If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
                                                    _myTrans.Rollback(TransSAVE)
                                                    Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
                                                End If
                                                boolFullFill = True
                                            End If
                                            'FIFO TAG ย้ายทั้ง TAG
                                            TypePicking = WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO
                                            Dim xAddition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
                                            strWhere = xWhere.ToString
                                            strWhere &= String.Format(" AND LocationType_Index = '{0}' ", drFIFO("LocationType_Index").ToString)
                                            objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 2, Withdraw_Index, xAddition)
                                            If Result IsNot Nothing Then
                                                'สร้างใบโอนย้ายสินค้า Auto
                                                Dim TransferStatus_Index As String = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drFIFO("TAG_No"), _Connection, _myTrans)
                                                ''FIFO หยิบสินค้า
                                                Picking_Qty = Total_Qty
                                                xLocationType_Index = ""
                                                strWhere = xWhere.ToString
                                                xAddition = " AND TAG_No = '" & xTo_TAG_No & "' " 'picking tag no , location pick face.
                                                strWhere &= String.Format(" AND LocationType_Index <> '{0}' ", drFIFO("LocationType_Index").ToString)
                                                objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 2, Withdraw_Index, xAddition)
                                                'Keep Transfer.
                                                If Not Result.Columns.Contains("TransferStatus_Index") Then
                                                    Result.Columns.Add("TransferStatus_Index", GetType(String))
                                                End If
                                                For Each drRow As DataRow In Result.Rows
                                                    drRow("TransferStatus_Index") = TransferStatus_Index
                                                Next
                                                'Add new tag move ; TAG_No,Sku_Index,LocationType_Index,Qty_Bal,RowNum
                                                Dim drAddNewrow As DataRow
                                                drAddNewrow = Pick_ResultAll.NewRow
                                                drAddNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
                                                If drAddNewrow Is Nothing Then
                                                    drAddNewrow = Pick_ResultAll.NewRow
                                                    drAddNewrow.BeginEdit()
                                                    drAddNewrow("TAG_No") = xTo_TAG_No
                                                    drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
                                                    drAddNewrow("LocationType_Index") = "3" ' drFIFO("LocationType_Index")
                                                    drAddNewrow("Qty_Bal") = FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                                    drAddNewrow("Weight_Bal") = FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                                    drAddNewrow("Volume_Bal") = FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                                    drAddNewrow("RowNum") = drFIFO("RowNum")
                                                    drAddNewrow.EndEdit()
                                                    Pick_ResultAll.Rows.Add(drAddNewrow)
                                                Else
                                                    drAddNewrow = Pick_ResultAll.NewRow
                                                    drAddNewrow.BeginEdit()
                                                    drAddNewrow("TAG_No") = xTo_TAG_No
                                                    drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
                                                    drAddNewrow("LocationType_Index") = "3" ' drFIFO("LocationType_Index")
                                                    drAddNewrow("Qty_Bal") += FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                                    drAddNewrow("Weight_Bal") += FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                                    drAddNewrow("Volume_Bal") += FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                                    drAddNewrow("RowNum") = drFIFO("RowNum")
                                                    drAddNewrow.EndEdit()
                                                End If


                                                dtLocationBalance.Merge(Result)
                                                Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
                                                Picking_Qty = Total_Qty
                                                Pick_Floor -= 1
                                                'Update data all tag
                                                Dim drNewrow As DataRow
                                                drNewrow = Pick_ResultAll.NewRow
                                                drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
                                                drNewrow.BeginEdit()
                                                drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                                drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                                drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                                drNewrow.EndEdit()

                                                If Total_Qty <= 0 Then Exit While
                                                'End :   If Result IsNot Nothing Then
                                                'FIFO แล้วไม่พบสินค้าที่ Pick face rack
                                            End If
                                        Else
                                            'หาตำแหน่งไม่ได้ให้หยิบเลย
                                            'BEGIN : FIFO
                                            Picking_Qty = Total_Qty
                                            Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), Picking_Qty)
                                            If Picking_Qty = 0 Then Continue For
                                            TypePicking = WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO
                                            Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
                                            strWhere = xWhere.ToString
                                            objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 2, Withdraw_Index, Addition)
                                            If Result IsNot Nothing Then
                                                If Result.Rows.Count > 0 Then
                                                    dtLocationBalance.Merge(Result)
                                                    Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
                                                    Picking_Qty = Total_Qty
                                                    Pick_Floor -= 1
                                                    'Update data all tag
                                                    Dim drNewrow As DataRow
                                                    drNewrow = Pick_ResultAll.NewRow
                                                    drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
                                                    drNewrow.BeginEdit()
                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                                    drNewrow.EndEdit()
                                                End If
                                            End If
                                            If Total_Qty <= 0 Then Exit While
                                            'End : If SuggestLocation.Rows.Count > 0 Then
                                            'ถ้าแนะนำตำแหน่งไม่ได้ตะกร้าให้หยิบแบบ Ratio
                                        End If
                                        'Else
                                        '    Exit While
                                        '    'end : If drPick.Length = 0 Then
                                        '    'ถ้าหยิบ Ratio กล่อง/หีบ ไม่ได้
                                        'End If
                                    Next
                                    Exit While
                                    'End : If dtRatioPick.Rows.Count > 0 Then
                                    'ไม่มี Ratio กล่อง/หีบ
                                    'End If
                                Else
                                    Exit While
                                    'End :  If xdrSelect.Length > 0 Then
                                    'ไม่พบสินค้าในตำแหน่ง Pick face rack จากข้อมูลตอนแรก
                                End If
                            End While

                            If Total_Qty <= 0 Then Exit While

                        End If


                        'ทั้งคลัง
                        While Total_Qty > 0
                            Picking_Qty = Total_Qty
                            'xLocationType_Index = "3"
                            xQuery = String.Format("Qty_Bal > {0}", 0)
                            xdrSelect = Pick_ResultAll.Select(xQuery, "LocationType_Index asc,RowNum")
                            If xdrSelect.Length > 0 Then
                                For Each drFIFO As DataRow In xdrSelect
                                    ''BEGIN : FIFO
                                    'Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
                                    'If Picking_Qty = 0 Then Continue For
                                    TypePicking = WMS_Site_TopCharoen.PICKING.enmPicking_Type.FIFO
                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
                                    strWhere = xWhere.ToString
                                    'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
                                    objPicking = New WMS_Site_TopCharoen.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, 2, Withdraw_Index, Addition)
                                    'END : FIFO
                                    If Result IsNot Nothing Then
                                        If Result.Rows.Count > 0 Then
                                            dtLocationBalance.Merge(Result)
                                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
                                            Picking_Qty = Total_Qty
                                            Pick_Floor -= 1
                                            'Update data all tag
                                            Dim drNewrow As DataRow
                                            drNewrow = Pick_ResultAll.NewRow
                                            drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
                                            drNewrow.BeginEdit()
                                            drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
                                            drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
                                            drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
                                            drNewrow.EndEdit()
                                        End If
                                    End If
                                    If Total_Qty <= 0 Then Exit While
                                Next
                            Else
                                Total_Qty = 0 'Set for Exit loop
                                Exit While
                            End If
                        End While


                    End While

                    Dim Total_Qty_LocationBalance As Decimal = 0
                    If dtLocationBalance Is Nothing Then dtLocationBalance = New DataTable
                    If dtLocationBalance.Rows.Count > 0 Then
                        Total_Qty_LocationBalance = dtLocationBalance.Compute("SUM(Total_Qty)", "")
                    End If
                    'ครบสร้างรายการใบเบิกสินค้า
                    If CDbl(drAutoPicking.Item("Total_Qty")) = Total_Qty_LocationBalance Then
                        '7.	บังคับห้ามหยิบสินค้าเกิน 2 Lot ต่อ Item ต่อ Sale Order by Customer
                        'KSL : คำนวณด้านบน
                        'If CInt(.Item("FlagMix_Lot")) Then
                        '    'If utilDatatable.GroupDataTable(dtLocationBalance, New String() {"Exp_Date"}).Rows.Count > 2 Then
                        '    If utilDatatable.GroupDataTable(dtLocationBalance, New String() {"Mfg_Date"}).Rows.Count > CInt(.Item("NumFlagMix_Lot")) Then 'KSL Use. Mfg
                        '        .Item("StatusPicking") = "Lot มากกว่า " & CInt(.Item("NumFlagMix_Lot").ToString & " !!!")
                        '        _myTrans.Rollback(TransSAVE)
                        '        Continue For
                        '    End If
                        'End If

                        'If irow = 183 Then
                        '    Dim xStop As String = "STOP"
                        'End If
                        MappingSOAndSaveWTHI(Withdraw_Index, dtSalesOrderPlan, drAutoPicking, dtLocationBalance, _Connection, _myTrans)
                    Else
                        ' .Item("StatusPicking") = String.Format("สินค้าคงเหลือ {0}", Total_Qty_LocationBalance)
                        _myTrans.Rollback(TransSAVE)
                        'Exit For
                        Continue For
                        'If IsNotPassTransaction Then _myTrans.Rollback()
                    End If
                End With

            Next
            If IsNotPassTransaction Then _myTrans.Commit()
            Return ""
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            'Throw New Exception(ex.Message.ToString & " Row : " & irow.ToString)
            Return ex.Message.ToString & " Row : " & irow.ToString
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()

        End Try
    End Function

    Public Function StrWherePicking_Row(ByVal PickingType As String, ByVal xdrRow As DataRow, Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Try
            Dim _strPicking As New System.Text.StringBuilder
            'Default
            '0. เบิกสินค้ามากกว่าวันปัจจุบัน
            _strPicking.Append(" AND CAST(Order_Date as DATE) <= CAST(getDate() as DATE) ")
            '1.	เจ้าของสินค้า
            _strPicking.Append(String.Format(" AND Customer_Index = '{0}' ", xdrRow("Customer_Index").ToString))
            '2.	สถานะสินค้า  'KSL ตาม Config ประเภทการจ่ายสินค้า
            '3.	Lot สินค้า ตามที่ระบุใน Sale order (ถ้าไม่มี ระบบจะแนะนำตาม FEFO เสมอ)
            If xdrRow("PLot").ToString.Trim.Length > 0 Then
                _strPicking.Append(String.Format(" AND PLot = '{0}' ", xdrRow("PLot").ToString))
            End If
            '4.	อายุสินค้า by สินค้า ตาม Master ใน WMS 
            If xdrRow("Shelf_Life") >= 0 Then
                _strPicking.Append(String.Format(" AND AgeRemain >= {0} ", xdrRow("Shelf_Life")))
            End If
            If xdrRow("Day_picking") >= 0 Then
                _strPicking.Append(String.Format(" AND AgeRemain >= {0} ", xdrRow("Day_picking")))
            End If
            If xdrRow("MinAgeRemain") >= 0 Then
                _strPicking.Append(String.Format(" AND AgeRemain >= {0} ", xdrRow("MinAgeRemain")))
            End If
            '5.KSL : CustomerType + Distribution + ProductType
            If xdrRow("MFG_Day_Count") >= 0 Then
                _strPicking.Append(String.Format(" AND AgeCountFromMfg <= {0} ", xdrRow("MFG_Day_Count")))
            End If
            '6.	ไม่หยิบย้อน Lot by Customer ตาม Master ใน WMS  , KSL : ใช้ Mfg_Date
            If xdrRow("FlagDont_Reverse_LOT") = True And xdrRow("LastMfg_date").Trim.Length > 0 Then
                'Last EXP Date
                If IsDate(xdrRow("LastMfg_date")) Then
                    _strPicking.Append(String.Format(" AND CAST(Mfg_Date as DATE) >= CAST('{0}' AS DATE) ", CDate(xdrRow("LastMfg_date")).ToString("yyyy-MM-dd")))
                End If
            End If

            'Fix fo KSL
            '7. Special KSL
            'ผู้ใช้งานที่อยู่ในศูนย์กระจาย และคลังสินค้าผูกกับศูนย์กระจาย
            _strPicking.Append(" AND Warehouse_Index in ( Select W.Warehouse_Index")
            _strPicking.Append(" from se_User U inner join ms_Warehouse W on W.DistributionCenter_Index = U.DistributionCenter_Index")
            _strPicking.Append(String.Format(" where U.user_index = '{0}' ) ", W_Module.WV_User_Index))



            Return _strPicking.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function StrWherePicking(ByVal PickingType As String, ByVal Customer_Index As String, ByVal ItemStatus_Index As String, ByVal Sku_Index As String, ByVal PLot As String, _
     ByVal Shelf_Life As Integer, ByVal Day_picking As Integer, ByVal MinAgeRemain As Integer, ByVal FlagDont_Reverse_LOT As Boolean, ByVal LastExp_date As String, ByVal MFG_Day_Count As Integer, _
     Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Try

            'Dim strOtherWhere As String = New config_CustomSetting_New().GetOther_Where(PickingType, "", _Connection, _myTrans) 'FEFO
            Dim _strPicking As New System.Text.StringBuilder
            'Default
            '_strPicking.Append(" AND Qty_Bal > 0 ")
            'If Total_Qty > 0 Then
            '    _strPicking.Append(String.Format(" AND Qty_Bal = {0} ", Total_Qty))
            'End If

            _strPicking.Append(" AND CAST(Order_Date as DATE) <= CAST(getDate() as DATE) ")
            '1.	เจ้าของสินค้า เช่น SINO, Abbot ตามที่ระบุใน Sale Order
            _strPicking.Append(String.Format(" AND Customer_Index = '{0}' ", Customer_Index))
            '2.	สถานะสินค้า (WH Code) ตามที่ระบุใน Sale order
            '_strPicking.Append(String.Format(" AND ItemStatus_Index = '{0}' ", ItemStatus_Index)) 'KSL ตาม Config ประเภทการจ่ายสินค้า
            '3.	Lot สินค้า ตามที่ระบุใน Sale order (ถ้าไม่มี ระบบจะแนะนำตาม FEFO เสมอ)
            If PLot.Trim.Length > 0 Then
                _strPicking.Append(String.Format(" AND PLot = '{0}' ", PLot))
            End If
            '4.	อายุสินค้า by สินค้า ตาม Master ใน WMS 
            '5.	อายุสินค้า by Customer ตามที่ระบุใน Sale Order 
            If Shelf_Life >= 0 Then
                _strPicking.Append(String.Format(" AND AgeRemain >= {0} ", Shelf_Life))
            End If
            If Day_picking >= 0 Then
                _strPicking.Append(String.Format(" AND AgeRemain >= {0} ", Day_picking))
            End If
            If MinAgeRemain >= 0 Then
                _strPicking.Append(String.Format(" AND AgeRemain >= {0} ", MinAgeRemain))
            End If
            'KSL : CustomerType + Distribution + ProductType
            If MFG_Day_Count >= 0 Then
                _strPicking.Append(String.Format(" AND AgeCountFromMfg >= {0} ", MFG_Day_Count))
            End If
            '6.	ไม่หยิบย้อน Lot by Customer ตาม Master ใน WMS 
            'เพิ่มเงื่อนไข SO Exp Date
            'If SO_Exp_Date.Trim.Length > 0 Then
            '    _strPicking.Append(String.Format(" AND CAST(Exp_Date as DATE)=CAST('{0}' AS Date) ", SO_Exp_Date))
            'Else
            If FlagDont_Reverse_LOT = True And LastExp_date.Trim.Length > 0 Then
                'Last EXP Date
                If IsDate(LastExp_date) Then
                    _strPicking.Append(String.Format(" AND CAST(Exp_Date as DATE) >= CAST('{0}' AS DATE) ", CDate(LastExp_date).ToString("yyyy-MM-dd")))
                End If
            End If
            'End If

            'Fix fo KSL
            '7. Special KSL
            'ผู้ใช้งานที่อยู่ในศูนย์กระจาย และคลังสินค้าผูกกับศูนย์กระจาย
            _strPicking.Append(" AND Warehouse_Index in ( Select W.Warehouse_Index")
            _strPicking.Append(" from se_User U inner join ms_Warehouse W on W.DistributionCenter_Index = U.DistributionCenter_Index")
            _strPicking.Append(String.Format(" where U.user_index = '{0}' ) ", W_Module.WV_User_Index))



            Return _strPicking.ToString
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    '    ''' <summary>
    '    ''' FIFO : แบบมีตำแหน่ง Pick face rack และมี Pick face ตะกร้าแยกกัน 
    '    ''' มีการเติมจาก Storage ไป pick face rack 
    '    ''' มีการเติมจาก Pick face rack ไป Busket
    '    ''' </summary>
    '    ''' <param name="Withdraw_Index"></param>
    '    ''' <param name="DocumentType_Index"></param>
    '    ''' <param name="dtSalesOrderPlan"></param>
    '    ''' <param name="dtSalesOrderGroup"></param>
    '    ''' <param name="pconfigFullFill"></param>
    '    ''' <param name="_USE_PICKFACE_RACK_TAG"></param>
    '    ''' <param name="_USE_PICKFACE_BUSKET_TAG"></param>
    '    ''' <param name="_Connection"></param>
    '    ''' <param name="_myTrans"></param>
    '    ''' <returns></returns>
    '    ''' <remarks></remarks>
    '    Public Function CreateAutoPickingWithdraw_v3(ByVal Withdraw_Index As String, ByVal DocumentType_Index As String, ByVal dtSalesOrderPlan As DataTable, ByVal dtSalesOrderGroup As DataTable, Optional ByVal pconfigFullFill As Boolean = True, Optional ByVal _USE_PICKFACE_RACK_TAG As Boolean = False, Optional ByVal _USE_PICKFACE_BUSKET_TAG As Boolean = False, _
    'Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
    '        Dim IsNotPassTransaction As Boolean = False
    '        Try
    '            'หมายเหตุ : ถ้าต้องการให้ Rack เป็นตะกร้าและมี TAG ต้องแก้ไขไม่ให้ TAG เป็นตำแหน่งตอนเติมเต็มสินค้า
    '            If _Connection Is Nothing Then
    '                _Connection = Connection
    '                With SQLServerCommand
    '                    .Connection = _Connection
    '                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
    '                    .Connection.Open()
    '                    .Transaction = _myTrans
    '                End With
    '                _myTrans = Connection.BeginTransaction()
    '                IsNotPassTransaction = True


    '                'Connection = New SqlClient.SqlConnection(WV_ConnectionString)
    '                ''connectDB()
    '                'Connection.Open()
    '                'myTrans = Connection.BeginTransaction(IsolationLevel.Serializable)
    '                'SQLServerCommand.Transaction = myTrans
    '                'SQLServerCommand.CommandTimeout = 0

    '            End If
    '            If Withdraw_Index = "" Then
    '                Throw New Exception("Withdraw not found!!!")
    '            End If
    '            If dtSalesOrderPlan.Rows.Count = 0 Then
    '                Throw New Exception("SalesOrder not found!!!")
    '            End If
    '            If dtSalesOrderGroup.Rows.Count = 0 Then
    '                Throw New Exception("Group SalesOrder not found!!!")
    '            End If

    '            Dim TypePicking As New WMS_Site_KingStella.PICKING.enmPicking_Type
    '            Dim Pick_Result As New DataTable
    '            Dim Pick_ResultAll As New DataTable
    '            Dim objPicking As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '            Dim boolFullFill As Boolean = False
    '            Dim irow As Integer = 0
    '            Dim xdrSelect() As DataRow
    '            Dim xQuery As String = ""
    '            For Each drAutoPicking As DataRow In dtSalesOrderGroup.Rows
    '                irow += 1
    '                If CDec(drAutoPicking("Total_Qty").ToString) = 0 Then Continue For
    '                Dim TransSAVE As String = Now.ToString("yyyyddMMHHmmssfff")
    '                _myTrans.Save(TransSAVE)
    '                Dim strWhere As String = ""
    '                Dim xWhere As New System.Text.StringBuilder
    '                Dim dtLocationBalance As New DataTable
    '                Dim Result As New DataTable
    '                With drAutoPicking
    '                    Dim SKU_Id As String = .Item("SKU_Id").ToString
    '                    Dim Total_Qty As Decimal = CDec(.Item("Total_Qty").ToString)
    '                    Dim Qty_Per_Pallet As Decimal = IIf(CDec(.Item("Qty_Per_Pallet").ToString) <= 0, 1, CDec(.Item("Qty_Per_Pallet").ToString))
    '                    Dim xLocationType_Index As String = ""
    '                    Dim Picking_Qty As Decimal = 0 'จำนวนสั่งหยิบ
    '                    Dim Pick_Floor As Decimal = 0 'จำนวนเต็มพาเลทสเปค
    '                    Dim Pick_Mod As Decimal = 0 'จำนวนเศษ
    '                    Pick_Floor = Math.Floor(Total_Qty / Qty_Per_Pallet) 'จำนวนเต็มพาเลทสเปค
    '                    Pick_Mod = (Total_Qty Mod Qty_Per_Pallet) 'จำนวนเศษ

    '                    xWhere = xWhere.Append(StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, .Item("MFG_Day_Count").ToString, _
    '                               _Connection, _myTrans))
    '                    '1	001	Storage (Rack)
    '                    '2	002	Pickface (Rack)
    '                    '3	003	Pickface (Busket)
    '                    While (0 < Total_Qty)
    '                        '--------------------------------------------------------------------------------------------------
    '                        'New version ทำทั้งคลังทีเดียวทีละ SKU
    '                        'STEP 1.2 : Grouping tag. ก่อนหยิบทั้งพาเลท
    '                        TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                        xLocationType_Index = ""
    '                        Picking_Qty = 0
    '                        strWhere = xWhere.ToString
    '                        objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                        Pick_ResultAll = New DataTable
    '                        Pick_ResultAll = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                        Pick_ResultAll.PrimaryKey = New DataColumn() {Pick_ResultAll.Columns("TAG_No")}
    '                        If Pick_ResultAll.Rows.Count = 0 Then
    '                            Exit While
    '                        End If
    '                        '--------------------------------------------------------------------------------------------------
    '                        'STEP 1 : FIFO Storage Full Pallet Spec.
    '                        xLocationType_Index = "1"
    '                        Picking_Qty = Qty_Per_Pallet
    '                        xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
    '                        xdrSelect = Pick_ResultAll.Select(xQuery, "RowNum")
    '                        If xdrSelect.Length > 0 Then
    '                            While (Pick_Floor > 0)
    '                                Picking_Qty = Qty_Per_Pallet
    '                                For Each drFIFO As DataRow In xdrSelect
    '                                    Picking_Qty = IIf(Total_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
    '                                    If Picking_Qty = 0 Then Continue For
    '                                    'BEGIN : FIFO
    '                                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No").ToString & "' "
    '                                    strWhere = xWhere.ToString
    '                                    strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                    'END : FIFO
    '                                    If Result IsNot Nothing Then
    '                                        If Result.Rows.Count > 0 Then
    '                                            dtLocationBalance.Merge(Result)
    '                                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                            Picking_Qty = Total_Qty
    '                                            Pick_Floor -= 1
    '                                            'Update data all tag
    '                                            Dim drNewrow As DataRow
    '                                            drNewrow = Pick_ResultAll.NewRow
    '                                            drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                            drNewrow.BeginEdit()
    '                                            drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                            drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                            drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                            drNewrow.EndEdit()
    '                                        End If
    '                                        If Total_Qty <= 0 Then Exit While
    '                                    End If
    '                                Next
    '                                'Set End loop
    '                                Pick_Floor = 0
    '                            End While
    '                        End If
    '                        '--------------------------------------------------------------------------------------------------
    '                        'STEP 2 : FIFO Pickface Rack
    '                        While Total_Qty > 0
    '                            Picking_Qty = Total_Qty
    '                            xLocationType_Index = "2"
    '                            xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
    '                            xdrSelect = Pick_ResultAll.Select(xQuery, "RowNum")
    '                            If xdrSelect.Length > 0 Then
    '                                'ค้นหา Ratio
    '                                Dim dtRatioPick As New DataTable
    '                                Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                                dtRatioPick = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Total_Qty, _Connection, _myTrans, SQLServerCommand)

    '                                If dtRatioPick.Rows.Count = 0 Then Exit While
    '                                For Each drPick As DataRow In dtRatioPick.Rows
    '                                    If Total_Qty < dtRatioPick.Compute("Min(Ratio)", "") Then Exit While
    '                                    Picking_Qty = drPick("Ratio")
    '                                    'จองสินค้า FIFO
    '                                    Dim drPick_Full() As DataRow
    '                                    xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
    '                                    drPick_Full = Pick_ResultAll.Select(xQuery, "")
    '                                    If drPick_Full.Length > 0 Then
    '                                        For Each drFIFO As DataRow In drPick_Full
    '                                            Picking_Qty = Math.Floor(drFIFO("Qty_Bal") / drPick("Ratio")) * drPick("Ratio") 'คำนวณหยิบเต็มกล่อง
    '                                            Pick_Floor = Math.Floor(Total_Qty / drPick("Ratio")) * drPick("Ratio")
    '                                            Picking_Qty = IIf((Picking_Qty > Pick_Floor), Pick_Floor, Picking_Qty)
    '                                            If Picking_Qty = 0 Then Continue For
    '                                            If Total_Qty = drFIFO("Qty_Bal") Then Picking_Qty = drFIFO("Qty_Bal")
    '                                            'BEGIN : FIFO
    '                                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                            Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                            strWhere = xWhere.ToString
    '                                            strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                            'END : FIFO
    '                                            If Result IsNot Nothing Then
    '                                                If Result.Rows.Count > 0 Then
    '                                                    dtLocationBalance.Merge(Result)
    '                                                    Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                    Pick_Floor -= 1
    '                                                    'Update data all tag
    '                                                    Dim drNewrow As DataRow
    '                                                    drNewrow = Pick_ResultAll.NewRow
    '                                                    drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                                    drNewrow.BeginEdit()
    '                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drNewrow.EndEdit()
    '                                                End If
    '                                            End If
    '                                            If Total_Qty <= 0 Then Exit While
    '                                        Next
    '                                        'Else
    '                                        '    Exit While
    '                                    End If
    '                                Next
    '                            Else
    '                                Exit While
    '                            End If
    '                        End While
    '                        If Total_Qty <= 0 Then Exit While
    '                        '--------------------------------------------------------------------------------------------------
    '                        'STEP 3 : FIFO Pickface ตระกล้า
    '                        Picking_Qty = Total_Qty
    '                        xLocationType_Index = "3"
    '                        xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal >= {1}", xLocationType_Index, 0)
    '                        xdrSelect = Pick_ResultAll.Select(xQuery, "RowNum")
    '                        If xdrSelect.Length > 0 Then
    '                            For Each drFIFO As DataRow In xdrSelect
    '                                'BEGIN : FIFO
    '                                Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), Picking_Qty)
    '                                If Picking_Qty = 0 Then Continue For
    '                                TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                strWhere = xWhere.ToString
    '                                strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                'END : FIFO
    '                                If Result IsNot Nothing Then
    '                                    If Result.Rows.Count > 0 Then
    '                                        dtLocationBalance.Merge(Result)
    '                                        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                        Pick_Floor -= 1
    '                                        'Update data all tag
    '                                        Dim drNewrow As DataRow
    '                                        drNewrow = Pick_ResultAll.NewRow
    '                                        drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                        drNewrow.BeginEdit()
    '                                        drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                        drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                        drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                        drNewrow.EndEdit()
    '                                    End If
    '                                End If
    '                                If Total_Qty <= 0 Then Exit While
    '                            Next
    '                        End If




    '                        If pconfigFullFill Then 'มีการเติมเต็มหรือไม่ค่าส่งมาจากหน้าจอ
    '                            '--------------------------------------------------------------------------------------------------
    '                            '**************************************************************************************************
    '                            '*******************************                มีการเติมเต็มสินค้า                                 **********************
    '                            '**************************************************************************************************
    '                            '--------------------------------------------------------------------------------------------------
    '                            'STEP 4 : FIFO ทั้งคลัง,แบบมีเติมเต็มสินค้า
    '                            'STEP 4.1 : FIFO ทั้งคลัง,ที่ตำแหน่ง Pick face rack
    '                            While Total_Qty > 0
    '                                Picking_Qty = Total_Qty
    '                                'xLocationType_Index = "2"
    '                                'xQuery = String.Format("LocationType_Index = '{0}' and Qty_Bal > {1}", xLocationType_Index, 0)
    '                                xQuery = String.Format("LocationType_Index in ('1','2') and Qty_Bal >= {0}", 0)
    '                                xdrSelect = Pick_ResultAll.Select(xQuery, "LocationType_Index desc,RowNum")
    '                                If xdrSelect.Length > 0 Then
    '                                    For Each drFIFO As DataRow In xdrSelect
    '                                        'เป็นเศษจริง สั่งเติมสินค้า จองสินค้าเต็ม TAG
    '                                        Picking_Qty = drFIFO("Qty_Bal")
    '                                        If Picking_Qty = 0 Then Continue For
    '                                        'ค้นหาตำแหน่งที่ต้องเติมเศษ
    '                                        'ต้องหาตำแหน่งที่สามารถเติมสินค้าได้
    '                                        '-------------------------------------------------------------------------------------
    '                                        Dim xTo_Location_Index As String = ""
    '                                        Dim xTo_TAG_No As String = ""
    '                                        Dim xTotal_Qty As Decimal = Picking_Qty
    '                                        Dim xWeightOut As Decimal = Picking_Qty * drAutoPicking("Weight_STD")
    '                                        Dim xVolumeOut As Decimal = Picking_Qty * drAutoPicking("Volume_STD")
    '                                        Dim xAdditional As String = ""
    '                                        Select Case drFIFO("LocationType_Index").ToString
    '                                            Case "2"
    '                                                xLocationType_Index = "3" 'เติมตำแหน่งตะกร้า
    '                                                xAdditional = String.Format(" ' And ml.LocationType_Index = {0} '", xLocationType_Index)
    '                                            Case "1"
    '                                                xLocationType_Index = "2"
    '                                                xAdditional = String.Format(" ' And ml.LocationType_Index in ({0},{1}) '", "2", "3")
    '                                        End Select
    '                                        xQuery = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
    '                                                                                    drAutoPicking("Sku_Index").ToString, _
    '                                                                                    drAutoPicking("ProductType_Index").ToString, _
    '                                                                                    drAutoPicking("ItemStatus_Index").ToString, _
    '                                                                                    "", _
    '                                                                                    drAutoPicking("Customer_Index").ToString, _
    '                                                                                    xTotal_Qty, xWeightOut, xVolumeOut, _
    '                                                                                    xAdditional)
    '                                        Dim SuggestLocation As DataTable = DBExeQuery(xQuery, _Connection, _myTrans)
    '                                        If SuggestLocation.Rows.Count > 0 Then
    '                                            xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '                                            xTo_TAG_No = drFIFO("TAG_No")
    '                                            Select Case SuggestLocation.Rows.Item(0).Item("LocationType_Index").ToString
    '                                                Case "3"
    '                                                    If _USE_PICKFACE_BUSKET_TAG Then
    '                                                        xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString 'ตะกร้า TAG NO = Location
    '                                                    End If
    '                                                Case "2"
    '                                                    If _USE_PICKFACE_RACK_TAG Then
    '                                                        xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString 'ตะกร้า TAG NO = Location
    '                                                    End If
    '                                            End Select
    '                                            If boolFullFill = False Then
    '                                                If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
    '                                                    _myTrans.Rollback(TransSAVE)
    '                                                    Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
    '                                                End If
    '                                                boolFullFill = True
    '                                            End If
    '                                            'FIFO TAG ย้ายทั้ง TAG
    '                                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                            Dim xAddition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                            strWhere = xWhere.ToString
    '                                            strWhere &= String.Format(" AND LocationType_Index = '{0}' ", drFIFO("LocationType_Index").ToString)
    '                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, xAddition)
    '                                            If Result IsNot Nothing Then
    '                                                'สร้างใบโอนย้ายสินค้า Auto
    '                                                Dim TransferStatus_Index As String = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drFIFO("TAG_No"), _Connection, _myTrans)
    '                                                ''FIFO หยิบสินค้า
    '                                                Picking_Qty = Total_Qty
    '                                                xLocationType_Index = ""
    '                                                strWhere = xWhere.ToString
    '                                                xAddition = " AND TAG_No = '" & xTo_TAG_No & "' " 'picking tag no , location pick face.
    '                                                strWhere &= String.Format(" AND LocationType_Index <> '{0}' ", drFIFO("LocationType_Index").ToString)
    '                                                objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, xAddition)
    '                                                'Keep Transfer.
    '                                                If Not Result.Columns.Contains("TransferStatus_Index") Then
    '                                                    Result.Columns.Add("TransferStatus_Index", GetType(String))
    '                                                End If
    '                                                For Each drRow As DataRow In Result.Rows
    '                                                    drRow("TransferStatus_Index") = TransferStatus_Index
    '                                                Next
    '                                                'Add new tag move ; TAG_No,Sku_Index,LocationType_Index,Qty_Bal,RowNum
    '                                                Dim drAddNewrow As DataRow
    '                                                drAddNewrow = Pick_ResultAll.NewRow
    '                                                drAddNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
    '                                                If drAddNewrow Is Nothing Then
    '                                                    drAddNewrow = Pick_ResultAll.NewRow
    '                                                    drAddNewrow.BeginEdit()
    '                                                    drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                    drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                    drAddNewrow("LocationType_Index") = "3" ' drFIFO("LocationType_Index")
    '                                                    drAddNewrow("Qty_Bal") = FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drAddNewrow("Weight_Bal") = FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drAddNewrow("Volume_Bal") = FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                    drAddNewrow.EndEdit()
    '                                                    Pick_ResultAll.Rows.Add(drAddNewrow)
    '                                                Else
    '                                                    drAddNewrow = Pick_ResultAll.NewRow
    '                                                    drAddNewrow.BeginEdit()
    '                                                    drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                    drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                    drAddNewrow("LocationType_Index") = "3" ' drFIFO("LocationType_Index")
    '                                                    drAddNewrow("Qty_Bal") += FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drAddNewrow("Weight_Bal") += FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drAddNewrow("Volume_Bal") += FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                    drAddNewrow.EndEdit()
    '                                                End If


    '                                                dtLocationBalance.Merge(Result)
    '                                                Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                Picking_Qty = Total_Qty
    '                                                Pick_Floor -= 1
    '                                                'Update data all tag
    '                                                Dim drNewrow As DataRow
    '                                                drNewrow = Pick_ResultAll.NewRow
    '                                                drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                                drNewrow.BeginEdit()
    '                                                drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                drNewrow.EndEdit()

    '                                                '-------------------------------------------------------------------------------------------
    '                                                'เติมตะกร้า
    '                                                'ถ้าย้ายไป Pick face rack ต้องเต็มกล่อง
    '                                                If drNewrow("Qty_Bal") > 0 And SuggestLocation.Rows.Item(0).Item("LocationType_Index").ToString = "2" Then
    '                                                    Picking_Qty = drNewrow("Qty_Bal") 'ของที่เหลือจากการหยิบ Pick face
    '                                                    Dim dtRatioPick As New DataTable
    '                                                    Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                                                    dtRatioPick = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Picking_Qty, _Connection, _myTrans, SQLServerCommand, True)
    '                                                    Dim drPick() As DataRow = dtRatioPick.Select("Ratio > 1 and xModulo > 0", "")
    '                                                    If drPick.Length > 0 Then
    '                                                        Picking_Qty = drPick(0)("xModulo") 'หาเศษของกล่อง
    '                                                        xTo_Location_Index = ""
    '                                                        xTo_TAG_No = "" 'มีค่าเท่ากันกับ TAG > "K1HG0101"
    '                                                        xTotal_Qty = Picking_Qty
    '                                                        xVolumeOut = (drAddNewrow("Weight_Bal") / drAddNewrow("Qty_Bal")) * Picking_Qty
    '                                                        xWeightOut = (drAddNewrow("Volume_Bal") / drAddNewrow("Qty_Bal")) * Picking_Qty
    '                                                        xAdditional = String.Format(" ' And ml.LocationType_Index in (''{0}'') '", "3")
    '                                                        xQuery = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
    '                                                                                    drAutoPicking("Sku_Index").ToString, _
    '                                                                                    drAutoPicking("ProductType_Index").ToString, _
    '                                                                                    drAutoPicking("ItemStatus_Index").ToString, _
    '                                                                                    "", _
    '                                                                                    drAutoPicking("Customer_Index").ToString, _
    '                                                                                    xTotal_Qty, xWeightOut, xVolumeOut, _
    '                                                                                    xAdditional)
    '                                                        SuggestLocation = DBExeQuery(xQuery, _Connection, _myTrans)

    '                                                        If SuggestLocation.Rows.Count > 0 Then
    '                                                            xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '                                                            xTo_TAG_No = drFIFO("TAG_No")
    '                                                            If _USE_PICKFACE_BUSKET_TAG Then
    '                                                                xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString
    '                                                            End If

    '                                                            If boolFullFill = False Then
    '                                                                If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
    '                                                                    _myTrans.Rollback(TransSAVE)
    '                                                                    Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
    '                                                                End If
    '                                                                boolFullFill = True
    '                                                            End If
    '                                                            'FIFO
    '                                                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                                            xAddition = " AND TAG_No = '" & drAddNewrow("TAG_No") & "'"
    '                                                            strWhere = xWhere.ToString
    '                                                            xLocationType_Index = ""
    '                                                            strWhere &= String.Format(" AND LocationType_Index='{0}' ", "2")
    '                                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, xAddition)
    '                                                            If Result IsNot Nothing Then
    '                                                                'Dim drNewrow As DataRow
    '                                                                drNewrow = Pick_ResultAll.NewRow
    '                                                                drNewrow = Pick_ResultAll.Rows.Find(drAddNewrow("TAG_No").ToString)
    '                                                                If drNewrow IsNot Nothing Then
    '                                                                    drNewrow.BeginEdit()
    '                                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                                    drNewrow.EndEdit()
    '                                                                End If

    '                                                                'สร้างใบโอนย้ายสินค้า Auto ย้ายแบบไม่หยิบ
    '                                                                TransferStatus_Index = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drAddNewrow("TAG_No"), _Connection, _myTrans)

    '                                                                drAddNewrow = Pick_ResultAll.NewRow
    '                                                                drAddNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
    '                                                                If drAddNewrow Is Nothing Then
    '                                                                    drAddNewrow = Pick_ResultAll.NewRow
    '                                                                    drAddNewrow.BeginEdit()
    '                                                                    drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                                    drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                                    drAddNewrow("LocationType_Index") = "3" 'drFIFO("LocationType_Index")
    '                                                                    drAddNewrow("Qty_Bal") = FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                                    drAddNewrow("Weight_Bal") = FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                                    drAddNewrow("Volume_Bal") = FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                                    drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                                    drAddNewrow.EndEdit()
    '                                                                    Pick_ResultAll.Rows.Add(drAddNewrow)
    '                                                                End If
    '                                                            End If

    '                                                        End If
    '                                                    End If
    '                                                End If
    '                                                '-------------------------------------------------------------------------------------------


    '                                                If Total_Qty <= 0 Then Exit While
    '                                                'End :   If Result IsNot Nothing Then
    '                                                'FIFO แล้วไม่พบสินค้าที่ Pick face rack
    '                                            End If
    '                                        Else
    '                                            'หาตำแหน่งได้ให้หยิบเลย
    '                                            'BEGIN : FIFO
    '                                            Picking_Qty = Total_Qty
    '                                            Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), Picking_Qty)
    '                                            If Picking_Qty = 0 Then Continue For
    '                                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                            Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                            strWhere = xWhere.ToString
    '                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                            If Result IsNot Nothing Then
    '                                                If Result.Rows.Count > 0 Then
    '                                                    dtLocationBalance.Merge(Result)
    '                                                    Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                    Pick_Floor -= 1
    '                                                    'Update data all tag
    '                                                    Dim drNewrow As DataRow
    '                                                    drNewrow = Pick_ResultAll.NewRow
    '                                                    drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                                    drNewrow.BeginEdit()
    '                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drNewrow.EndEdit()
    '                                                End If
    '                                            End If
    '                                            If Total_Qty <= 0 Then Exit While
    '                                            'End : If SuggestLocation.Rows.Count > 0 Then
    '                                            'ถ้าแนะนำตำแหน่งไม่ได้ตะกร้าให้หยิบแบบ Ratio
    '                                        End If
    '                                        'Else
    '                                        '    Exit While
    '                                        '    'end : If drPick.Length = 0 Then
    '                                        '    'ถ้าหยิบ Ratio กล่อง/หีบ ไม่ได้
    '                                        'End If
    '                                    Next
    '                                    Exit While
    '                                    'End : If dtRatioPick.Rows.Count > 0 Then
    '                                    'ไม่มี Ratio กล่อง/หีบ
    '                                    'End If
    '                                Else
    '                                    Exit While
    '                                    'End :  If xdrSelect.Length > 0 Then
    '                                    'ไม่พบสินค้าในตำแหน่ง Pick face rack จากข้อมูลตอนแรก
    '                                End If
    '                            End While

    '                            If Total_Qty <= 0 Then Exit While

    '                        End If


    '                        'ทั้งคลัง
    '                        While Total_Qty > 0
    '                            Picking_Qty = Total_Qty
    '                            'xLocationType_Index = "3"
    '                            xQuery = String.Format("Qty_Bal > {0}", 0)
    '                            xdrSelect = Pick_ResultAll.Select(xQuery, "LocationType_Index asc,RowNum")
    '                            If xdrSelect.Length > 0 Then
    '                                For Each drFIFO As DataRow In xdrSelect
    '                                    ''BEGIN : FIFO
    '                                    'Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
    '                                    'If Picking_Qty = 0 Then Continue For
    '                                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                    strWhere = xWhere.ToString
    '                                    'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                    'END : FIFO
    '                                    If Result IsNot Nothing Then
    '                                        If Result.Rows.Count > 0 Then
    '                                            dtLocationBalance.Merge(Result)
    '                                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                            Pick_Floor -= 1
    '                                            'Update data all tag
    '                                            Dim drNewrow As DataRow
    '                                            drNewrow = Pick_ResultAll.NewRow
    '                                            drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                            drNewrow.BeginEdit()
    '                                            drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                            drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                            drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                            drNewrow.EndEdit()
    '                                        End If
    '                                    End If
    '                                    If Total_Qty <= 0 Then Exit While
    '                                Next
    '                            Else
    '                                Total_Qty = 0 'Set for Exit loop
    '                                Exit While
    '                            End If
    '                        End While


    '                    End While

    '                    Dim Total_Qty_LocationBalance As Decimal = 0
    '                    If dtLocationBalance Is Nothing Then dtLocationBalance = New DataTable
    '                    If dtLocationBalance.Rows.Count > 0 Then
    '                        Total_Qty_LocationBalance = dtLocationBalance.Compute("SUM(Total_Qty)", "")
    '                    End If
    '                    'ครบสร้างรายการใบเบิกสินค้า
    '                    If CDbl(.Item("Total_Qty")) = Total_Qty_LocationBalance Then
    '                        ''7.	บังคับห้ามหยิบสินค้าเกิน 2 Lot ต่อ Item ต่อ Sale Order by Customer
    '                        'If CInt(.Item("FlagMix_Lot")) = 1 Then
    '                        '    If utilDatatable.GroupDataTable(dtLocationBalance, New String() {"Exp_Date"}).Rows.Count > 2 Then
    '                        '        ' .Item("StatusPicking") = "Lot มากกว่า 2!!!"
    '                        '        _myTrans.Rollback(TransSAVE)
    '                        '        Continue For
    '                        '    End If
    '                        'End If
    '                        MappingSOAndSaveWTHI(Withdraw_Index, dtSalesOrderPlan, drAutoPicking, dtLocationBalance, _Connection, _myTrans)
    '                    Else
    '                        ' .Item("StatusPicking") = String.Format("สินค้าคงเหลือ {0}", Total_Qty_LocationBalance)
    '                        _myTrans.Rollback(TransSAVE)
    '                        Continue For
    '                    End If
    '                End With

    '            Next
    '            If IsNotPassTransaction Then _myTrans.Commit()
    '            Return ""
    '        Catch ex As Exception
    '            If IsNotPassTransaction Then _myTrans.Rollback()
    '            Throw ex
    '        Finally
    '            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
    '        End Try
    '    End Function

    '    Public Function CreateAutoPickingWithdraw_v2(ByVal Withdraw_Index As String, ByVal DocumentType_Index As String, ByVal dtSalesOrderPlan As DataTable, ByVal dtSalesOrderGroup As DataTable, Optional ByVal pconfigFullFill As Boolean = True, _
    'Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
    '        Dim IsNotPassTransaction As Boolean = False
    '        Try

    '            If _Connection Is Nothing Then
    '                _Connection = Connection
    '                With SQLServerCommand
    '                    .Connection = _Connection
    '                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
    '                    .Connection.Open()
    '                    .Transaction = _myTrans
    '                End With
    '                _myTrans = Connection.BeginTransaction()
    '                IsNotPassTransaction = True


    '                'Connection = New SqlClient.SqlConnection(WV_ConnectionString)
    '                ''connectDB()
    '                'Connection.Open()
    '                'myTrans = Connection.BeginTransaction(IsolationLevel.Serializable)
    '                'SQLServerCommand.Transaction = myTrans
    '                'SQLServerCommand.CommandTimeout = 0

    '            End If
    '            If Withdraw_Index = "" Then
    '                Throw New Exception("Withdraw not found!!!")
    '            End If
    '            If dtSalesOrderPlan.Rows.Count = 0 Then
    '                Throw New Exception("SalesOrder not found!!!")
    '            End If
    '            If dtSalesOrderGroup.Rows.Count = 0 Then
    '                Throw New Exception("Group SalesOrder not found!!!")
    '            End If

    '            Dim TypePicking As New WMS_Site_KingStella.PICKING.enmPicking_Type
    '            Dim Pick_Result As New DataTable
    '            Dim Pick_ResultAll As New DataTable
    '            Dim objPicking As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '            Dim boolFullFill As Boolean = False
    '            Dim irow As Integer = 0
    '            Dim xdrSelect() As DataRow
    '            Dim xQuery As String = ""
    '            For Each drAutoPicking As DataRow In dtSalesOrderGroup.Rows
    '                irow += 1
    '                If CDec(drAutoPicking("Total_Qty").ToString) = 0 Then Continue For
    '                Dim TransSAVE As String = Now.ToString("yyyyddMMHHmmssfff")
    '                _myTrans.Save(TransSAVE)
    '                Dim strWhere As String = ""
    '                Dim xWhere As New System.Text.StringBuilder
    '                Dim dtLocationBalance As New DataTable
    '                Dim Result As New DataTable
    '                With drAutoPicking
    '                    Dim SKU_Id As String = .Item("SKU_Id").ToString
    '                    Dim Total_Qty As Decimal = CDec(.Item("Total_Qty").ToString)
    '                    Dim Qty_Per_Pallet As Decimal = IIf(CDec(.Item("Qty_Per_Pallet").ToString) <= 0, 1, CDec(.Item("Qty_Per_Pallet").ToString))
    '                    Dim xLocationType_Index As String = ""
    '                    Dim Picking_Qty As Decimal = 0 'จำนวนสั่งหยิบ
    '                    Dim Pick_Floor As Decimal = 0 'จำนวนเต็มพาเลทสเปค
    '                    Dim Pick_Mod As Decimal = 0 'จำนวนเศษ
    '                    Pick_Floor = Math.Floor(Total_Qty / Qty_Per_Pallet) 'จำนวนเต็มพาเลทสเปค
    '                    Pick_Mod = (Total_Qty Mod Qty_Per_Pallet) 'จำนวนเศษ

    '                    xWhere = xWhere.Append(StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, .Item("MFG_Day_Count").ToString, _
    '                               _Connection, _myTrans))
    '                    '1	001	Storage (Rack)
    '                    '2	002	Pickface (Rack)
    '                    '3	003	Pickface (Busket)
    '                    While (0 < Total_Qty)
    '                        '--------------------------------------------------------------------------------------------------
    '                        'New version ทำทั้งคลังทีเดียวทีละ SKU
    '                        'STEP 1.2 : Grouping tag. ก่อนหยิบทั้งพาเลท
    '                        TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                        xLocationType_Index = ""
    '                        Picking_Qty = Qty_Per_Pallet '0
    '                        strWhere = xWhere.ToString
    '                        objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                        Pick_ResultAll = New DataTable
    '                        Pick_ResultAll = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                        Pick_ResultAll.PrimaryKey = New DataColumn() {Pick_ResultAll.Columns("TAG_No")}
    '                        '--------------------------------------------------------------------------------------------------
    '                        'STEP 1 : FIFO Storage Full Pallet Spec.
    '                        xLocationType_Index = "1"
    '                        xQuery = String.Format("LocationType_Index = {0} and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
    '                        xdrSelect = Pick_ResultAll.Select(xQuery, "")
    '                        If xdrSelect.Length > 0 Then
    '                            While (Pick_Floor > 0)
    '                                Picking_Qty = Qty_Per_Pallet
    '                                For Each drFIFO As DataRow In xdrSelect
    '                                    Picking_Qty = IIf(Total_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
    '                                    If Picking_Qty = 0 Then Continue For
    '                                    'BEGIN : FIFO
    '                                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No").ToString & "' "
    '                                    strWhere = xWhere.ToString
    '                                    'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                    'END : FIFO
    '                                    If Result IsNot Nothing Then
    '                                        If Result.Rows.Count > 0 Then
    '                                            dtLocationBalance.Merge(Result)
    '                                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                            Picking_Qty = Total_Qty
    '                                            Pick_Floor -= 1
    '                                            'Update data all tag
    '                                            Dim drNewrow As DataRow
    '                                            drNewrow = Pick_ResultAll.NewRow
    '                                            drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                            drNewrow.BeginEdit()
    '                                            drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                            drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                            drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                            drNewrow.EndEdit()
    '                                        End If
    '                                        If Total_Qty <= 0 Then Exit While
    '                                    End If
    '                                Next
    '                                'Set End loop
    '                                Pick_Floor = 0
    '                            End While
    '                        End If
    '                        '--------------------------------------------------------------------------------------------------
    '                        'STEP 2 : FIFO Pickface Rack
    '                        While Total_Qty > 0
    '                            xLocationType_Index = "2"
    '                            xQuery = String.Format("LocationType_Index = {0} and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
    '                            xdrSelect = Pick_ResultAll.Select(xQuery, "")
    '                            If xdrSelect.Length > 0 Then
    '                                'ค้นหา Ratio
    '                                Dim dtRatioPick As New DataTable
    '                                Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                                dtRatioPick = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Total_Qty, _Connection, _myTrans, SQLServerCommand)

    '                                If dtRatioPick.Rows.Count = 0 Then Exit While
    '                                For Each drPick As DataRow In dtRatioPick.Rows
    '                                    If Total_Qty < dtRatioPick.Compute("Min(Ratio)", "") Then Exit While
    '                                    Picking_Qty = drPick("Ratio")
    '                                    'จองสินค้า FIFO
    '                                    Dim drPick_Full() As DataRow
    '                                    xQuery = String.Format("LocationType_Index = {0} and Qty_Bal >= {1}", xLocationType_Index, Picking_Qty)
    '                                    drPick_Full = Pick_ResultAll.Select(xQuery, "")
    '                                    If drPick_Full.Length > 0 Then
    '                                        For Each drFIFO As DataRow In drPick_Full
    '                                            Picking_Qty = Math.Floor(drFIFO("Qty_Bal") / drPick("Ratio")) * drPick("Ratio") 'คำนวณหยิบเต็มกล่อง
    '                                            Pick_Floor = Math.Floor(Total_Qty / drPick("Ratio")) * drPick("Ratio")
    '                                            Picking_Qty = IIf((Picking_Qty > Pick_Floor), Pick_Floor, Picking_Qty)
    '                                            If Picking_Qty = 0 Then Continue For
    '                                            'BEGIN : FIFO
    '                                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                            Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                            strWhere = xWhere.ToString
    '                                            'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                            'END : FIFO
    '                                            If Result IsNot Nothing Then
    '                                                If Result.Rows.Count > 0 Then
    '                                                    dtLocationBalance.Merge(Result)
    '                                                    Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                    Pick_Floor -= 1
    '                                                    'Update data all tag
    '                                                    Dim drNewrow As DataRow
    '                                                    drNewrow = Pick_ResultAll.NewRow
    '                                                    drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                                    drNewrow.BeginEdit()
    '                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drNewrow.EndEdit()
    '                                                End If
    '                                            End If
    '                                            If Total_Qty <= 0 Then Exit While
    '                                        Next
    '                                        'Else
    '                                        '    Exit While
    '                                    End If
    '                                Next
    '                            Else
    '                                Exit While
    '                            End If
    '                        End While
    '                        '--------------------------------------------------------------------------------------------------
    '                        'STEP 3 : FIFO Pickface ตระกล้า
    '                        Picking_Qty = Total_Qty
    '                        xLocationType_Index = "3"
    '                        xQuery = String.Format("LocationType_Index = {0} and Qty_Bal > {1}", xLocationType_Index, 0)
    '                        xdrSelect = Pick_ResultAll.Select(xQuery, "")
    '                        If xdrSelect.Length > 0 Then
    '                            For Each drFIFO As DataRow In xdrSelect
    '                                'BEGIN : FIFO
    '                                Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), Picking_Qty)
    '                                If Picking_Qty = 0 Then Continue For
    '                                TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                strWhere = xWhere.ToString
    '                                'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                'END : FIFO
    '                                If Result IsNot Nothing Then
    '                                    If Result.Rows.Count > 0 Then
    '                                        dtLocationBalance.Merge(Result)
    '                                        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                        Pick_Floor -= 1
    '                                        'Update data all tag
    '                                        Dim drNewrow As DataRow
    '                                        drNewrow = Pick_ResultAll.NewRow
    '                                        drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                        drNewrow.BeginEdit()
    '                                        drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                        drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                        drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                        drNewrow.EndEdit()
    '                                    End If
    '                                End If
    '                                If Total_Qty <= 0 Then Exit While
    '                            Next
    '                        End If




    '                        If Not pconfigFullFill Then 'มีการเติมเต็มหรือไม่ค่าส่งมาจากหน้าจอ
    '                            While Total_Qty > 0
    '                                Picking_Qty = Total_Qty
    '                                'xLocationType_Index = "3"
    '                                xQuery = String.Format("Qty_Bal > {0}", 0)
    '                                xdrSelect = Pick_ResultAll.Select(xQuery, "LocationType_Index asc")
    '                                If xdrSelect.Length > 0 Then
    '                                    For Each drFIFO As DataRow In xdrSelect
    '                                        ''BEGIN : FIFO
    '                                        'Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
    '                                        'If Picking_Qty = 0 Then Continue For
    '                                        TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                        Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                        strWhere = xWhere.ToString
    '                                        'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                        objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                        Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                        'END : FIFO
    '                                        If Result IsNot Nothing Then
    '                                            If Result.Rows.Count > 0 Then
    '                                                dtLocationBalance.Merge(Result)
    '                                                Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                Pick_Floor -= 1
    '                                                'Update data all tag
    '                                                Dim drNewrow As DataRow
    '                                                drNewrow = Pick_ResultAll.NewRow
    '                                                drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                                drNewrow.BeginEdit()
    '                                                drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                drNewrow.EndEdit()
    '                                            End If
    '                                        End If
    '                                        If Total_Qty <= 0 Then Exit While
    '                                    Next
    '                                Else
    '                                    Total_Qty = 0 'Set for Exit loop
    '                                    Exit While
    '                                End If
    '                            End While

    '                        Else

    '                            '--------------------------------------------------------------------------------------------------
    '                            '**************************************************************************************************
    '                            '*******************************                มีการเติมเต็มสินค้า                                 **********************
    '                            '**************************************************************************************************
    '                            '--------------------------------------------------------------------------------------------------

    '                            'STEP 4 : FIFO ทั้งคลัง,แบบมีเติมเต็มสินค้า
    '                            'STEP 4.1 : FIFO ทั้งคลัง,ที่ตำแหน่ง Pick face rack
    '                            While Total_Qty > 0
    '                                Picking_Qty = Total_Qty
    '                                xLocationType_Index = "2"
    '                                xQuery = String.Format("LocationType_Index = {0} and Qty_Bal > {1}", xLocationType_Index, 0)
    '                                xdrSelect = Pick_ResultAll.Select(xQuery, "")
    '                                If xdrSelect.Length > 0 Then
    '                                    Dim dtRatioPick As New DataTable
    '                                    Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                                    dtRatioPick = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Picking_Qty, _Connection, _myTrans, SQLServerCommand, True)
    '                                    If dtRatioPick.Rows.Count > 0 Then
    '                                        For Each drFIFO As DataRow In xdrSelect
    '                                            Dim drPick() As DataRow = dtRatioPick.Select("Ratio > 1 and xFloor > 0", "")
    '                                            If drPick.Length = 0 Then
    '                                                'เป็นเศษจริง สั่งเติมสินค้า จองสินค้าเต็ม TAG
    '                                                Picking_Qty = drFIFO("Qty_Bal")
    '                                                If Picking_Qty = 0 Then Continue For
    '                                                TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                                Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                                strWhere = xWhere.ToString
    '                                                'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                                objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                                If Result IsNot Nothing Then
    '                                                    If Total_Qty < drFIFO("Qty_Bal") Then
    '                                                        'ค้นหาตำแหน่งที่ต้องเติมเศษ
    '                                                        'ต้องหาตำแหน่งที่สามารถเติมสินค้าได้
    '                                                        '-------------------------------------------------------------------------------------
    '                                                        Dim xTo_Location_Index As String = "3"
    '                                                        Dim xTo_TAG_No As String = "" 'มีค่าเท่ากันกับ TAG > "K1HG0101"
    '                                                        Dim xTotal_Qty As Decimal = Result.Compute("SUM(Total_Qty)", "")
    '                                                        Dim xVolumeOut As Decimal = Result.Compute("SUM(VolumeOut)", "")
    '                                                        Dim xWeightOut As Decimal = Result.Compute("SUM(WeightOut)", "")
    '                                                        xLocationType_Index = "3" 'เติมตำแหน่งตะกร้า
    '                                                        Dim xAdditional As String = String.Format(" ' And ml.LocationType_Index = {0} '", xLocationType_Index)
    '                                                        xQuery = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
    '                                                                                                    Result.Rows(0)("Sku_Index").ToString, _
    '                                                                                                    Result.Rows(0)("ProductType_Index").ToString, _
    '                                                                                                    Result.Rows(0)("ItemStatus_Index").ToString, _
    '                                                                                                    drFIFO("DocumentType_Index").ToString, _
    '                                                                                                    Result.Rows(0)("Customer_Index").ToString, _
    '                                                                                                    xTotal_Qty, xWeightOut, xVolumeOut, _
    '                                                                                                    xAdditional)
    '                                                        Dim SuggestLocation As DataTable = DBExeQuery(xQuery, _Connection, _myTrans)
    '                                                        '-------------------------------------------------------------------------------------
    '                                                        'มีตำแหน่งให้เติมเต็มสินค้า
    '                                                        If SuggestLocation.Rows.Count > 0 Then
    '                                                            xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '                                                            xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString

    '                                                            If boolFullFill = False Then
    '                                                                If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
    '                                                                    _myTrans.Rollback(TransSAVE)
    '                                                                    Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
    '                                                                End If
    '                                                                boolFullFill = True
    '                                                            End If

    '                                                            'สร้างใบโอนย้ายสินค้า Auto
    '                                                            Dim TransferStatus_Index As String = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drFIFO("TAG_No"), _Connection, _myTrans)
    '                                                            ''FIFO หยิบสินค้า
    '                                                            Picking_Qty = Total_Qty
    '                                                            xLocationType_Index = ""
    '                                                            strWhere = xWhere.ToString
    '                                                            'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                                            Addition = " AND TAG_No = '" & xTo_TAG_No & "' " 'picking tag no , location pick face.
    '                                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                                            'Keep Transfer.
    '                                                            If Not Result.Columns.Contains("TransferStatus_Index") Then
    '                                                                Result.Columns.Add("TransferStatus_Index", GetType(String))
    '                                                            End If
    '                                                            For Each drRow As DataRow In Result.Rows
    '                                                                drRow("TransferStatus_Index") = TransferStatus_Index
    '                                                            Next
    '                                                            'Add new tag move ; TAG_No,Sku_Index,LocationType_Index,Qty_Bal,RowNum
    '                                                            Dim drAddNewrow As DataRow
    '                                                            drAddNewrow = Pick_ResultAll.NewRow
    '                                                            drAddNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
    '                                                            If drAddNewrow Is Nothing Then
    '                                                                drAddNewrow = Pick_ResultAll.NewRow
    '                                                                drAddNewrow.BeginEdit()
    '                                                                drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                                drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                                drAddNewrow("LocationType_Index") = "3" ' drFIFO("LocationType_Index")
    '                                                                drAddNewrow("Qty_Bal") = FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                                drAddNewrow("Weight_Bal") = FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                                drAddNewrow("Volume_Bal") = FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                                drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                                drAddNewrow.EndEdit()
    '                                                                Pick_ResultAll.Rows.Add(drAddNewrow)
    '                                                            Else
    '                                                                drAddNewrow = Pick_ResultAll.NewRow
    '                                                                drAddNewrow.BeginEdit()
    '                                                                drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                                drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                                drAddNewrow("LocationType_Index") = "3" ' drFIFO("LocationType_Index")
    '                                                                drAddNewrow("Qty_Bal") += FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                                drAddNewrow("Weight_Bal") += FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                                drAddNewrow("Volume_Bal") += FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                                drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                                drAddNewrow.EndEdit()
    '                                                            End If
    '                                                        End If
    '                                                        '-------------------------------------------------------------------------------------
    '                                                    End If

    '                                                    dtLocationBalance.Merge(Result)
    '                                                    Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                    Picking_Qty = Total_Qty
    '                                                    Pick_Floor -= 1
    '                                                    'Update data all tag
    '                                                    Dim drNewrow As DataRow
    '                                                    drNewrow = Pick_ResultAll.NewRow
    '                                                    drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                                    drNewrow.BeginEdit()
    '                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drNewrow.EndEdit()

    '                                                    If Total_Qty <= 0 Then Exit While
    '                                                End If
    '                                            Else
    '                                                Exit While
    '                                            End If

    '                                        Next

    '                                    End If
    '                                Else
    '                                    Exit While
    '                                End If

    '                            End While

    '                            '--------------------------------------------------------------------------------------------------
    '                            'STEP 5 : FIFO ทั้งคลัง ,แบบทั้ง TAG และแบบมีเติมเต็มสินค้า
    '                            If Total_Qty <= 0 Then Exit While
    '                            Picking_Qty = Total_Qty
    '                            xLocationType_Index = ""
    '                            xQuery = String.Format("Qty_Bal > {0}", 0)
    '                            xdrSelect = Pick_ResultAll.Select(xQuery, "")
    '                            If xdrSelect.Length > 0 Then
    '                                For Each drFIFO As DataRow In xdrSelect
    '                                    Picking_Qty = drFIFO("Qty_Bal")
    '                                    Dim Qty_Bal_Begin As Decimal = drFIFO("Qty_Bal")
    '                                    'If Total_Qty <= Picking_Qty Then
    '                                    '    Picking_Qty = Total_Qty
    '                                    'End If
    '                                    If Picking_Qty = 0 Then Continue For
    '                                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                    strWhere = xWhere.ToString
    '                                    'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                    xLocationType_Index = ""
    '                                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                    'If Result IsNot Nothing Then
    '                                    If Result.Rows.Count > 0 Then
    '                                        'จอง tag เต็ม
    '                                        Dim drNewrow As DataRow
    '                                        drNewrow = Pick_ResultAll.NewRow
    '                                        drNewrow = Pick_ResultAll.Rows.Find(drFIFO("TAG_No").ToString)
    '                                        drNewrow.BeginEdit()
    '                                        drNewrow("Qty_Bal") -= Result.Compute("SUM(Total_Qty)", "")
    '                                        drNewrow("Weight_Bal") -= Result.Compute("SUM(WeightOut)", "")
    '                                        drNewrow("Volume_Bal") -= Result.Compute("SUM(VolumeOut)", "")
    '                                        drNewrow.EndEdit()

    '                                        If Total_Qty < Qty_Bal_Begin Then
    '                                            'มีตำแหน่งให้เติมเต็มสินค้า
    '                                            Dim xTo_Location_Index As String = ""
    '                                            Dim xTo_TAG_No As String = "" 'มีค่าเท่ากันกับ TAG > "K1HG0101"
    '                                            Dim xTotal_Qty As Decimal = Result.Compute("SUM(Total_Qty)", "")
    '                                            Dim xVolumeOut As Decimal = Result.Compute("SUM(VolumeOut)", "")
    '                                            Dim xWeightOut As Decimal = Result.Compute("SUM(WeightOut)", "")
    '                                            Dim xAdditional As String = "''"
    '                                            'SuggestLocation = New DataTable
    '                                            Select Case drFIFO("LocationType_Index").ToString
    '                                                Case "1" 'Storage ต้องย้าย,'Pick face และ ตำแหน่งตะกร้า 
    '                                                    'ถ้าย้ายไป Pick face rack ต้องเต็มกล่อง
    '                                                    Dim dtRatioPick As New DataTable
    '                                                    Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                                                    dtRatioPick = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Picking_Qty, _Connection, _myTrans, SQLServerCommand, True)
    '                                                    Dim drPick() As DataRow = dtRatioPick.Select("Ratio > 1 and xFloor > 0", "")

    '                                                    If drPick.Length > 0 Then
    '                                                        'Pick face rack
    '                                                        xAdditional = String.Format(" ' And ml.LocationType_Index in (''{0}'') '", "2")
    '                                                        xLocationType_Index = "2"
    '                                                    Else
    '                                                        'Pick face ตำแหน่งตะกร้า 
    '                                                        xAdditional = String.Format(" ' And ml.LocationType_Index in (''{0}'') '", "3")
    '                                                        xLocationType_Index = "3"
    '                                                    End If
    '                                                Case "2" 'Pick face rack ต้องย้าย
    '                                                    xAdditional = String.Format(" ' And ml.LocationType_Index in (''{0}'') '", "3")
    '                                                    xLocationType_Index = "3"
    '                                                Case "3" 'Pick face ตะกร้า
    '                                            End Select

    '                                            xQuery = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
    '                                                                                .Item("Sku_Index").ToString, _
    '                                                                                .Item("ProductType_Index").ToString, _
    '                                                                                .Item("ItemStatus_Index").ToString, _
    '                                                                                 drFIFO("DocumentType_Index").ToString, _
    '                                                                                .Item("Customer_Index").ToString, _
    '                                                                                xTotal_Qty, xWeightOut, xVolumeOut, _
    '                                                                                xAdditional)
    '                                            Dim SuggestLocation As DataTable = DBExeQuery(xQuery, _Connection, _myTrans)

    '                                            If SuggestLocation.Rows.Count > 0 Then
    '                                                xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '                                                Select Case xLocationType_Index
    '                                                    Case "3"
    '                                                        xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString
    '                                                    Case Else
    '                                                        xTo_TAG_No = drFIFO("TAG_No").ToString 'SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString
    '                                                End Select


    '                                                If boolFullFill = False Then
    '                                                    If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
    '                                                        _myTrans.Rollback(TransSAVE)
    '                                                        Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
    '                                                    End If
    '                                                    boolFullFill = True
    '                                                End If

    '                                                'สร้างใบโอนย้ายสินค้า Auto
    '                                                Dim TransferStatus_Index As String = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drFIFO("TAG_No"), _Connection, _myTrans)

    '                                                'Add new tag move ; TAG_No,Sku_Index,LocationType_Index,Qty_Bal,RowNum
    '                                                Dim drAddNewrow As DataRow
    '                                                drAddNewrow = Pick_ResultAll.NewRow
    '                                                drAddNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
    '                                                If drAddNewrow Is Nothing Then
    '                                                    drAddNewrow = Pick_ResultAll.NewRow
    '                                                    drAddNewrow.BeginEdit()
    '                                                    drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                    drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                    drAddNewrow("LocationType_Index") = xLocationType_Index 'drFIFO("LocationType_Index")
    '                                                    drAddNewrow("Qty_Bal") = FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drAddNewrow("Weight_Bal") = FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drAddNewrow("Volume_Bal") = FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                    drAddNewrow.EndEdit()
    '                                                    Pick_ResultAll.Rows.Add(drAddNewrow)


    '                                                    ''FIFO หยิบสินค้า
    '                                                    Picking_Qty = Total_Qty
    '                                                    'xLocationType_Index = ""
    '                                                    strWhere = xWhere.ToString
    '                                                    'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                                    Addition = " AND TAG_No = '" & xTo_TAG_No & "' " 'picking tag no , location pick face.
    '                                                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                                    'Keep Transfer.
    '                                                    If Not Result.Columns.Contains("TransferStatus_Index") Then
    '                                                        Result.Columns.Add("TransferStatus_Index", GetType(String))
    '                                                    End If
    '                                                    For Each drRow As DataRow In Result.Rows
    '                                                        drRow("TransferStatus_Index") = TransferStatus_Index
    '                                                    Next
    '                                                    'update ค่าเบิกสินค้า
    '                                                    dtLocationBalance.Merge(Result)
    '                                                    Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                    Picking_Qty = Total_Qty
    '                                                    Pick_Floor -= 1
    '                                                    'Dim drNewrow As DataRow
    '                                                    drNewrow = Pick_ResultAll.NewRow
    '                                                    drNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
    '                                                    drNewrow.BeginEdit()
    '                                                    drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                    drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                    drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                    drNewrow.EndEdit()
    '                                                    'ตรวจสอบเติมสินค้าเศษที่ตะกร้า
    '                                                    Picking_Qty = drNewrow("Qty_Bal")
    '                                                    If Picking_Qty > 0 Then
    '                                                        Select Case xLocationType_Index
    '                                                            Case "1" 'Storage ต้องย้าย,'Pick face และ ตำแหน่งตะกร้า 
    '                                                            Case "2" 'Pick face rack ต้องย้าย
    '                                                                'ถ้าย้ายไป Pick face rack ต้องเต็มกล่อง
    '                                                                Dim dtRatioPick As New DataTable
    '                                                                Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                                                                dtRatioPick = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Picking_Qty, _Connection, _myTrans, SQLServerCommand, True)
    '                                                                Dim drPick() As DataRow = dtRatioPick.Select("Ratio > 1 and xModulo > 0", "")
    '                                                                If drPick.Length > 0 Then
    '                                                                    Picking_Qty = drPick(0)("xModulo") 'หาเศษของกล่อง
    '                                                                    xTo_Location_Index = ""
    '                                                                    xTo_TAG_No = "" 'มีค่าเท่ากันกับ TAG > "K1HG0101"
    '                                                                    xTotal_Qty = Picking_Qty
    '                                                                    xVolumeOut = (drAddNewrow("Weight_Bal") / drAddNewrow("Qty_Bal")) * Picking_Qty
    '                                                                    xWeightOut = (drAddNewrow("Volume_Bal") / drAddNewrow("Qty_Bal")) * Picking_Qty
    '                                                                    xAdditional = String.Format(" ' And ml.LocationType_Index in (''{0}'') '", "3")
    '                                                                    xQuery = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
    '                                                                                .Item("Sku_Index").ToString, _
    '                                                                                .Item("ProductType_Index").ToString, _
    '                                                                                .Item("ItemStatus_Index").ToString, _
    '                                                                                 drFIFO("DocumentType_Index").ToString, _
    '                                                                                .Item("Customer_Index").ToString, _
    '                                                                                xTotal_Qty, xWeightOut, xVolumeOut, _
    '                                                                                xAdditional)
    '                                                                    SuggestLocation = DBExeQuery(xQuery, _Connection, _myTrans)
    '                                                                    If SuggestLocation.Rows.Count > 0 Then
    '                                                                        xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '                                                                        xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString
    '                                                                        If boolFullFill = False Then
    '                                                                            If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
    '                                                                                _myTrans.Rollback(TransSAVE)
    '                                                                                Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
    '                                                                            End If
    '                                                                            boolFullFill = True
    '                                                                        End If
    '                                                                        'FIFO
    '                                                                        TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                                                                        Addition = " AND TAG_No = '" & drAddNewrow("TAG_No") & "'"
    '                                                                        strWhere = xWhere.ToString
    '                                                                        'strWhere &= String.Format(" AND LocationType_Index='{0}' ", xLocationType_Index)
    '                                                                        xLocationType_Index = ""
    '                                                                        objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere.ToString, DocumentType_Index)
    '                                                                        Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                                                        If Result IsNot Nothing Then
    '                                                                            'Dim drNewrow As DataRow
    '                                                                            drNewrow = Pick_ResultAll.NewRow
    '                                                                            drNewrow = Pick_ResultAll.Rows.Find(drAddNewrow("TAG_No").ToString)
    '                                                                            If drNewrow IsNot Nothing Then
    '                                                                                drNewrow.BeginEdit()
    '                                                                                drNewrow("Qty_Bal") -= FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                                                drNewrow("Weight_Bal") -= FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                                                drNewrow("Volume_Bal") -= FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                                                drNewrow.EndEdit()
    '                                                                            End If

    '                                                                            'สร้างใบโอนย้ายสินค้า Auto ย้ายแบบไม่หยิบ
    '                                                                            TransferStatus_Index = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drAddNewrow("TAG_No"), _Connection, _myTrans)

    '                                                                            drAddNewrow = Pick_ResultAll.NewRow
    '                                                                            drAddNewrow = Pick_ResultAll.Rows.Find(xTo_TAG_No)
    '                                                                            If drAddNewrow Is Nothing Then
    '                                                                                drAddNewrow = Pick_ResultAll.NewRow
    '                                                                                drAddNewrow.BeginEdit()
    '                                                                                drAddNewrow("TAG_No") = xTo_TAG_No
    '                                                                                drAddNewrow("Sku_Index") = drFIFO("Sku_Index")
    '                                                                                drAddNewrow("LocationType_Index") = "3" 'drFIFO("LocationType_Index")
    '                                                                                drAddNewrow("Qty_Bal") = FormatNumber(Result.Compute("SUM(Total_Qty)", ""), 6)
    '                                                                                drAddNewrow("Weight_Bal") = FormatNumber(Result.Compute("SUM(WeightOut)", ""), 6)
    '                                                                                drAddNewrow("Volume_Bal") = FormatNumber(Result.Compute("SUM(VolumeOut)", ""), 6)
    '                                                                                drAddNewrow("RowNum") = drFIFO("RowNum")
    '                                                                                drAddNewrow.EndEdit()
    '                                                                                Pick_ResultAll.Rows.Add(drAddNewrow)
    '                                                                            End If
    '                                                                        End If

    '                                                                    End If
    '                                                                End If

    '                                                            Case "3" 'Pick face ตะกร้า
    '                                                        End Select
    '                                                    End If

    '                                                End If
    '                                            End If
    '                                        Else
    '                                            'หยิบบางส่วนไม่มีการเติม
    '                                            dtLocationBalance.Merge(Result)
    '                                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                            Picking_Qty = Total_Qty
    '                                            Pick_Floor -= 1
    '                                        End If
    '                                    Else
    '                                        'update ค่าเบิกสินค้า
    '                                        dtLocationBalance.Merge(Result)
    '                                        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                        Picking_Qty = Total_Qty
    '                                        Pick_Floor -= 1
    '                                    End If

    '                                    If Total_Qty <= 0 Then Exit While


    '                                Next
    '                            Else
    '                                Exit While
    '                            End If

    '                            '**************************************************************************************************
    '                            '--------------------------------------------------------------------------------------------------
    '                        End If


    '                    End While

    '                    Dim Total_Qty_LocationBalance As Decimal = 0
    '                    If dtLocationBalance Is Nothing Then dtLocationBalance = New DataTable
    '                    If dtLocationBalance.Rows.Count > 0 Then
    '                        Total_Qty_LocationBalance = dtLocationBalance.Compute("SUM(Total_Qty)", "")
    '                    End If
    '                    'ครบสร้างรายการใบเบิกสินค้า
    '                    If CDbl(.Item("Total_Qty")) = Total_Qty_LocationBalance Then
    '                        ''7.	บังคับห้ามหยิบสินค้าเกิน 2 Lot ต่อ Item ต่อ Sale Order by Customer
    '                        'If CInt(.Item("FlagMix_Lot")) = 1 Then
    '                        '    If utilDatatable.GroupDataTable(dtLocationBalance, New String() {"Exp_Date"}).Rows.Count > 2 Then
    '                        '        ' .Item("StatusPicking") = "Lot มากกว่า 2!!!"
    '                        '        _myTrans.Rollback(TransSAVE)
    '                        '        Continue For
    '                        '    End If
    '                        'End If
    '                        MappingSOAndSaveWTHI(Withdraw_Index, dtSalesOrderPlan, drAutoPicking, dtLocationBalance, _Connection, _myTrans)
    '                    Else
    '                        ' .Item("StatusPicking") = String.Format("สินค้าคงเหลือ {0}", Total_Qty_LocationBalance)
    '                        _myTrans.Rollback(TransSAVE)
    '                        Continue For
    '                    End If
    '                End With

    '            Next
    '            If IsNotPassTransaction Then _myTrans.Commit()
    '            Return ""
    '        Catch ex As Exception
    '            If IsNotPassTransaction Then _myTrans.Rollback()
    '            Throw ex
    '        Finally
    '            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
    '        End Try
    '    End Function


    Public Function GetSalesOrderPlan(ByVal arrSalesOrder_Index() As String, ByVal strWhere As String, _
       Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Try

            Dim Result As DataTable
            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

            builder = New System.Text.StringBuilder
            SQLServerCommand.Parameters.Clear()

            'builder.Append(" SELECT * FROM VIEW_SalesOrderPlan_SINO WHERE 1=1 ")
            builder.Append(" SELECT * FROM VIEW_SalesOrderPlan_KSL WHERE 1=1 ")

            builder.Append(strWhere)

            Dim builderArr As New System.Text.StringBuilder
            If arrSalesOrder_Index.Length > 0 Then
                For i As Integer = 0 To arrSalesOrder_Index.Length - 1
                    Dim parmname As String = String.Format("@SalesOrder_Index{0}", i)
                    builderArr.Append(parmname & ",")
                    SQLServerCommand.Parameters.Add(parmname, SqlDbType.VarChar, 13).Value = arrSalesOrder_Index(i)
                Next

                builder.Append(String.Format(" AND SalesOrder_Index IN ({0}) ", builderArr.ToString.Remove(builderArr.ToString.LastIndexOf(","), 1)))
            End If


            Result = DBExeQuery(builder.ToString, _Connection, _myTrans)

            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function GroupDatatableSalesOrderGroup(ByVal dtSalesOrderPlan As DataTable) As DataTable
        Try
            Dim Result As New DataTable

            Dim colGroup As New List(Of String)
            Dim colSum As New List(Of String)

            For Each col As DataColumn In dtSalesOrderPlan.Columns
                Select Case col.ColumnName
                    Case "SalesOrderItem_Index", "SalesOrder_Index", "SalesOrder_No", "Status", "Pre_Manifest_No", "Branch_Id"
                    Case "Qty_Plan", "Total_Qty_Plan", "Qty_Withdraw", "Qty", "QtyCARTON", "QtyPC"
                    Case "Customer_Shipping_Index", "Customer_Shipping_Id", "Company_Name"
                    Case "Customer_Shipping_Location_Index", "Shipping_Location_Name"
                    Case "Total_Qty", "Total_Qty_Withdraw"
                        colSum.Add(col.ColumnName)
                    Case Else
                        colGroup.Add(col.ColumnName)
                End Select
            Next


            Result = utilDatatable.GroupDataTable(dtSalesOrderPlan, colGroup.ToArray, colSum.ToArray, " FlagMix_Lot=0 ")

            If Result.Columns.Contains("SalesOrder_Index") = False Then Result.Columns.Add("SalesOrder_Index", GetType(String))
            'If dtAutoPicking.Columns.Contains("SalesOrderItem_Index") = False Then dtAutoPicking.Columns.Add("SalesOrderItem_Index", GetType(String))
            If Result.Columns.Contains("SalesOrder_No") = False Then Result.Columns.Add("SalesOrder_No", GetType(String))

            '***เพิ่ม Field Group อย่าลืมเพิ่ม UnGroup

            'colGroup.Add("SalesOrderItem_Index")
            colGroup.Add("SalesOrder_Index")
            colGroup.Add("SalesOrder_No")

            ' colGroup.Add("Customer_Shipping_Index")
            'colGroup.Add("Customer_Shipping_Id")
            'colGroup.Add("Company_Name")


            'colGroup.Add("Customer_Type_Index")
            'colGroup.Add("Customer_Type_Desc")

            Result.Merge(utilDatatable.GroupDataTable(dtSalesOrderPlan, colGroup.ToArray, colSum.ToArray, " FlagMix_Lot=1 "))



            Return Result
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function CreateAutoPickingWithdraw_v1(ByVal Withdraw_Index As String, ByVal DocumentType_Index As String, ByVal dtSalesOrderPlan As DataTable, ByVal dtSalesOrderGroup As DataTable, _
    'Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
    '    Dim IsNotPassTransaction As Boolean = False
    '    Try

    '        If _Connection Is Nothing Then
    '            _Connection = Connection
    '            With SQLServerCommand
    '                .Connection = _Connection
    '                If .Connection.State = ConnectionState.Open Then .Connection.Close()
    '                .Connection.Open()
    '                .Transaction = _myTrans
    '            End With
    '            _myTrans = Connection.BeginTransaction()
    '            IsNotPassTransaction = True
    '        End If
    '        If Withdraw_Index = "" Then
    '            Throw New Exception("Withdraw not found!!!")
    '        End If
    '        If dtSalesOrderPlan.Rows.Count = 0 Then
    '            Throw New Exception("SalesOrder not found!!!")
    '        End If
    '        If dtSalesOrderGroup.Rows.Count = 0 Then
    '            Throw New Exception("Group SalesOrder not found!!!")
    '        End If

    '        Dim TypePicking As New WMS_Site_KingStella.PICKING.enmPicking_Type
    '        Dim Pick_Result As New DataTable
    '        Dim objPicking As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '        Dim boolFullFill As Boolean = False
    '        Dim irow As Integer = 0
    '        For Each drAutoPicking As DataRow In dtSalesOrderGroup.Rows
    '            irow += 1
    '            If CDec(drAutoPicking("Total_Qty").ToString) = 0 Then Continue For
    '            Dim TransSAVE As String = Now.ToString("yyyyddMMHHmmssfff")
    '            _myTrans.Save(TransSAVE)
    '            Dim strWhere As String = ""
    '            Dim dtLocationBalance As New DataTable
    '            Dim Result As New DataTable
    '            With drAutoPicking
    '                Dim SKU_Id As String = .Item("SKU_Id").ToString
    '                Dim Total_Qty As Decimal = CDec(.Item("Total_Qty").ToString)
    '                Dim Qty_Per_Pallet As Decimal = IIf(CDec(.Item("Qty_Per_Pallet").ToString) <= 0, 1, CDec(.Item("Qty_Per_Pallet").ToString))
    '                Dim _LocationType_Index As String = ""
    '                Dim Picking_Qty As Decimal = 0 'จำนวนสั่งหยิบ
    '                Dim Pick_Floor As Decimal = 0 'จำนวนเต็มพาเลทสเปค
    '                Dim Pick_Mod As Decimal = 0 'จำนวนเศษ
    '                Pick_Floor = Math.Floor(Total_Qty / Qty_Per_Pallet) 'จำนวนเต็มพาเลทสเปค
    '                Pick_Mod = (Total_Qty Mod Qty_Per_Pallet) 'จำนวนเศษ
    '                '1	001	Storage (Rack)
    '                '2	002	Pickface (Rack)
    '                '3	003	Pickface (Busket)
    '                While (0 < Total_Qty)
    '                    '--------------------------------------------------------------------------------------------------
    '                    'STEP 1 : FIFO Storage Full Pallet Spec.
    '                    'ตำแหน่ง Storage เก็บสินค้าเต็มพาเลทชั้น 3 ขึ้นไป
    '                    'STEP 1.1 : เงื่อนไข Fix หยิบเท่ากับจำนวนเท่านั้น แต่ยังไม่รองรับหยิบทั้งพาเลทเพราะไม่ Group TAG
    '                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                    If Total_Qty >= Qty_Per_Pallet Then
    '                        _LocationType_Index = "1"
    '                        Picking_Qty = Qty_Per_Pallet
    '                        'จองสินค้า FIFO
    '                        'STEP 1.2 : Grouping tag. ก่อนหยิบทั้งพาเลท
    '                        strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                    .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                    _LocationType_Index, _
    '                                   _Connection, _myTrans)

    '                        objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                        Pick_Result = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                        'STEP 1.3 : FIFO Storage Pick pallet spec.
    '                        '(หยิบสินค้าเต็มพาเลทเท่ากับพาเลทสเปค ที่ตำแหน่งเก็บ)
    '                        While (Pick_Floor > 0)
    '                            For Each drFIFO As DataRow In Pick_Result.Rows
    '                                Picking_Qty = IIf(Total_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
    '                                If Picking_Qty = 0 Then Continue For
    '                                Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "' "
    '                                objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                                Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                If Result IsNot Nothing Then
    '                                    If Result.Rows.Count > 0 Then
    '                                        dtLocationBalance.Merge(Result)
    '                                        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                        Picking_Qty = Total_Qty
    '                                        Pick_Floor -= 1
    '                                    End If
    '                                    If Total_Qty <= 0 Then Exit While
    '                                End If
    '                            Next
    '                            'Set End loop
    '                            Pick_Floor = 0
    '                        End While
    '                    End If
    '                    ''STEP 1.4 : FIFO Storage Full tag.
    '                    ''TODO : ถ้าต้องการหยิบหมดพาเลทที่ Storage แต่ของไม่เต็ม Full ค่อยเปิดใช้งาน
    '                    ''(หยิบสินค้าเต็มพาเลทของพอดี ที่ตำแหน่งเก็บ)
    '                    ''Reset search
    '                    'Picking_Qty = Total_Qty
    '                    'While Picking_Qty > 0
    '                    '    Picking_Qty = Total_Qty
    '                    '    'Exit Function
    '                    '    _LocationType_Index = "1"
    '                    '    strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                    '                .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                    '                _LocationType_Index, _
    '                    '               _Connection, _myTrans)

    '                    '    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                    '    Pick_Result = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.LESS_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                    '    If Pick_Result IsNot Nothing Then
    '                    '        Dim drPick_Full() As DataRow
    '                    '        drPick_Full = Pick_Result.Select(String.Format("Qty_Bal <= {0}", Picking_Qty), "")
    '                    '        If drPick_Full.Length > 0 Then
    '                    '            For Each drFIFO As DataRow In Pick_Result.Rows
    '                    '                Picking_Qty = IIf(Picking_Qty >= drFIFO("Qty_Bal"), drFIFO("Qty_Bal"), 0)
    '                    '                If Picking_Qty <= 0 Then Exit For
    '                    '                drPick_Full = Pick_Result.Select(String.Format("Qty_Bal <= {0} AND TAG_No = '{1}'", Picking_Qty, drFIFO("TAG_No").ToString), "")
    '                    '                If drPick_Full.Length > 0 Then
    '                    '                    Dim Addition As String = String.Format(" AND TAG_No = '{0}'", drFIFO("TAG_No").ToString)
    '                    '                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                    '                    If Result IsNot Nothing Then
    '                    '                        If Result.Rows.Count > 0 Then
    '                    '                            dtLocationBalance.Merge(Result)
    '                    '                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                    '                            Picking_Qty = Total_Qty
    '                    '                            Pick_Floor -= 1
    '                    '                        End If
    '                    '                    End If
    '                    '                    If Total_Qty <= 0 Then Exit While
    '                    '                End If
    '                    '            Next
    '                    '        Else
    '                    '            Exit While
    '                    '        End If
    '                    '    End If
    '                    'End While

    '                    '--------------------------------------------------------------------------------------------------
    '                    'STEP 2 : FIFO Pickface Rack
    '                    'ตำแหน่ง Pickface Rack เก็บสินค้าเศษแต่เป็นสินค้าเศษกล่องเต็มและหีบ ชั้น 1 และ 2
    '                    If Total_Qty <= 0 Then Exit While
    '                    _LocationType_Index = "2"
    '                    Dim dtPickface_Rack As New DataTable
    '                    Dim RatioPick As New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                    dtPickface_Rack = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Total_Qty, _Connection, _myTrans, SQLServerCommand)
    '                    While Total_Qty > 0
    '                        If dtPickface_Rack.Rows.Count = 0 Then Exit While
    '                        For Each drPick As DataRow In dtPickface_Rack.Rows
    '                            If Total_Qty < dtPickface_Rack.Compute("Min(Ratio)", "") Then Exit While
    '                            Picking_Qty = drPick("Ratio")
    '                            'Pick_Floor = drPick("xFloor") 'จำนวนกล่อง/หีบ
    '                            'Pick_Mod = 0 'จำนวนเศษ
    '                            'Picking_Qty = drPick("xFloor_full") 'จำนวนในกล่อง

    '                            strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                                                        .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                                                        _LocationType_Index, _
    '                                                                       _Connection, _myTrans)
    '                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO

    '                            'จองสินค้า FIFO
    '                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                            Pick_Result = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                            If Pick_Result IsNot Nothing Then
    '                                Dim drPick_Full() As DataRow
    '                                'Picking_Qty = (Total_Qty / drPick("Ratio"))
    '                                drPick_Full = Pick_Result.Select(String.Format("Qty_Bal >= {0}", Picking_Qty), "")
    '                                If drPick_Full.Length > 0 Then
    '                                    For Each drFIFO As DataRow In Pick_Result.Rows
    '                                        Picking_Qty = Math.Floor(drFIFO("Qty_Bal") / drPick("Ratio")) * drPick("Ratio") 'คำนวณหยิบเต็มกล่อง
    '                                        Pick_Floor = Math.Floor(Total_Qty / drPick("Ratio")) * drPick("Ratio")
    '                                        Picking_Qty = IIf((Picking_Qty > Pick_Floor), Pick_Floor, Picking_Qty)
    '                                        If Picking_Qty = 0 Then Continue For
    '                                        'Set Qty Picking FIFO
    '                                        objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                                        Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "'"
    '                                        Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                        If Result IsNot Nothing Then
    '                                            If Result.Rows.Count > 0 Then
    '                                                dtLocationBalance.Merge(Result)
    '                                                Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                                Pick_Floor -= 1
    '                                            End If
    '                                        End If
    '                                        If Total_Qty <= 0 Then Continue For
    '                                    Next
    '                                Else
    '                                    Exit While
    '                                End If
    '                            End If

    '                        Next
    '                    End While

    '                    '--------------------------------------------------------------------------------------------------
    '                    'STEP 3 : FIFO Pickface ตระกล้า
    '                    'ตำแหน่ง Pickface ตะกล้า เก็บสินค้าเป็นชิ้นเท่านั้น เป็นโซนวางตระกล้า
    '                    Picking_Qty = Total_Qty
    '                    _LocationType_Index = "3"
    '                    strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                                                .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                                                _LocationType_Index, _
    '                                                               _Connection, _myTrans)
    '                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                    'Set Qty Picking FIFO
    '                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, "")
    '                    If Result IsNot Nothing Then
    '                        If Result.Rows.Count > 0 Then
    '                            dtLocationBalance.Merge(Result)
    '                            Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                            Pick_Floor -= 1
    '                        End If
    '                    End If
    '                    If Total_Qty <= 0 Then Exit While

    '                    '--------------------------------------------------------------------------------------------------
    '                    '**************************************************************************************************
    '                    '*******************************                มีการเติมเต็มสินค้า                                 **********************
    '                    '**************************************************************************************************
    '                    '--------------------------------------------------------------------------------------------------
    '                    'STEP 4 : FIFO ทั้งคลัง,แบบมีเติมเต็มสินค้า
    '                    'สินค้าเหลือและเกิดกรณีเติมเต็มสินค้า
    '                    'ถ้าสินค้าไม่พอมาถึง step นี้แสดงว่าสินค้าที่ตระกร้าก็ไม่พอแล้ว

    '                    'STEP 4.1 : FIFO ทั้งคลัง,ที่ตำแหน่ง Pick face rack
    '                    Picking_Qty = Total_Qty
    '                    _LocationType_Index = "2"
    '                    While Total_Qty > 0
    '                        dtPickface_Rack = New DataTable
    '                        RatioPick = New WMS_Site_KingStella.PICKING(WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO)
    '                        dtPickface_Rack = RatioPick.SEARCH_SKU_RATIO(.Item("Sku_Index").ToString, Picking_Qty, _Connection, _myTrans, SQLServerCommand, True)
    '                        If dtPickface_Rack.Rows.Count > 0 Then
    '                            'Dim drPick() As DataRow = dtPickface_Rack.Select(String.Format("Ratio > 1 and Ratio <= {0}", Picking_Qty), "")
    '                            'If drPick.Length = 0 Then Exit While 'ถ้าจำนวนในกล่องไม่พอหรือไม่มีให้ออกไปหยิบที่ตะกร้า
    '                            strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                        .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                        _LocationType_Index, _
    '                                       _Connection, _myTrans)
    '                            TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                            Pick_Result = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                            If Pick_Result.Rows.Count = 0 Then Exit While
    '                            For Each drFIFO As DataRow In Pick_Result.Rows
    '                                'drPick = dtPickface_Rack.Select("Ratio > 1 and xFloor > 0", "")
    '                                Dim drPick() As DataRow = dtPickface_Rack.Select("Ratio > 1 and xFloor > 0", "")
    '                                If drPick.Length = 0 Then
    '                                    'เป็นเศษจริง สั่งเติมสินค้า จองสินค้าเต็ม TAG
    '                                    Picking_Qty = drFIFO("Qty_Bal")
    '                                    If Picking_Qty = 0 Then Continue For
    '                                    Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "' "
    '                                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                                    Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)
    '                                    If Result IsNot Nothing Then
    '                                        'ค้นหาตำแหน่งที่ต้องเติมเศษ
    '                                        'ต้องหาตำแหน่งที่สามารถเติมสินค้าได้
    '                                        '-------------------------------------------------------------------------------------
    '                                        Dim xTo_Location_Index As String = ""
    '                                        Dim xTo_TAG_No As String = "" 'มีค่าเท่ากันกับ TAG > "K1HG0101"
    '                                        Dim xTotal_Qty As Decimal = Result.Compute("SUM(Total_Qty)", "")
    '                                        Dim xVolumeOut As Decimal = Result.Compute("SUM(VolumeOut)", "")
    '                                        Dim xWeightOut As Decimal = Result.Compute("SUM(WeightOut)", "")
    '                                        _LocationType_Index = "3" 'เติมตำแหน่งตะกร้า
    '                                        Dim xAdditional As String = String.Format(" ' And ml.LocationType_Index = {0} '", _LocationType_Index)
    '                                        Dim xQuery As String = String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", _
    '                                                                                    Result.Rows(0)("Sku_Index").ToString, _
    '                                                                                    Result.Rows(0)("ProductType_Index").ToString, _
    '                                                                                    Result.Rows(0)("ItemStatus_Index").ToString, _
    '                                                                                    "", _
    '                                                                                    Result.Rows(0)("Customer_Index").ToString, _
    '                                                                                    xTotal_Qty, xWeightOut, xVolumeOut, _
    '                                                                                    xAdditional)
    '                                        Dim SuggestLocation As DataTable = DBExeQuery(xQuery, _Connection, _myTrans)
    '                                        '-------------------------------------------------------------------------------------
    '                                        'มีตำแหน่งให้เติมเต็มสินค้า
    '                                        If SuggestLocation.Rows.Count > 0 Then
    '                                            xTo_Location_Index = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '                                            xTo_TAG_No = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString

    '                                            If boolFullFill = False Then
    '                                                If WMS_STD_Master.W_Language.W_MSG_Confirm("ระบบเกิดการย้ายสินค้าเพื่อเติมเต็ม คุณต้องการให้เกิดการย้ายหรือไม่ ?") = DialogResult.No Then
    '                                                    _myTrans.Rollback(TransSAVE)
    '                                                    Return New Exception("ระบบยกเลิกการ FIFO").Message.ToString
    '                                                End If
    '                                                boolFullFill = True
    '                                            End If

    '                                            'สร้างใบโอนย้ายสินค้า Auto
    '                                            Dim TransferStatus_Index As String = Me.AutoTransfer_KSL(Result, .Item("Customer_Index").ToString, Withdraw_Index, xTo_Location_Index, xTo_TAG_No, drFIFO("TAG_No"), _Connection, _myTrans)

    '                                            ''FIFO หยิบสินค้า
    '                                            Picking_Qty = Total_Qty
    '                                            _LocationType_Index = ""
    '                                            strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                                        .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                                        _LocationType_Index, _
    '                                                       _Connection, _myTrans)
    '                                            Addition = " AND TAG_No = '" & xTo_TAG_No & "' " 'picking tag no , location pick face.
    '                                            objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                                            Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)

    '                                            'Keep Transfer.
    '                                            If Not Result.Columns.Contains("TransferStatus_Index") Then
    '                                                Result.Columns.Add("TransferStatus_Index", GetType(String))
    '                                            End If
    '                                            For Each drRow As DataRow In Result.Rows
    '                                                drRow("TransferStatus_Index") = TransferStatus_Index
    '                                            Next

    '                                        End If

    '                                        '-------------------------------------------------------------------------------------

    '                                        dtLocationBalance.Merge(Result)
    '                                        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                        Picking_Qty = Total_Qty
    '                                        Pick_Floor -= 1

    '                                        If Total_Qty <= 0 Then Continue For

    '                                    End If
    '                                End If
    '                            Next

    '                        End If

    '                    End While

    '                    If Total_Qty <= 0 Then Exit While

    '                    '--------------------------------------------------------------------------------------------------
    '                    'STEP 5 : FIFO ทั้งคลัง ,แบบทั้ง TAG และแบบมีเติมเต็มสินค้า
    '                    Picking_Qty = Total_Qty
    '                    _LocationType_Index = ""
    '                    strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                                                .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                                                _LocationType_Index, _
    '                                                               _Connection, _myTrans)
    '                    TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                    'Set Qty Picking FIFO
    '                    objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                    Pick_Result = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(WMS_Site_KingStella.PICKING.enmMode_Pick.OVER_EQUALS, Picking_Qty, _Connection, _myTrans, SQLServerCommand)
    '                    For Each drFIFO As DataRow In Pick_Result.Rows
    '                        'Picking_Qty = drFIFO("Qty_Bal")
    '                        'Dim Addition As String = " AND TAG_No = '" & drFIFO("TAG_No") & "' "
    '                        'objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                        'Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, Addition)

    '                        'ตรวจสอบ Location Type ว่าควรเติมไหม
    '                        Dim xQuery As String = String.Format("select top 1 LocationType_Index from tb_LocationBalance LB " & _
    '                        " inner join ms_Location ML ON ML.Location_Index = LB.Location_Index WHERE LB.Tag_No = '{0}'", drFIFO("TAG_No").ToString)
    '                        Dim xLocationType_Index As String = DBExeQuery(xQuery, _Connection, _myTrans).Rows(0)(0)
    '                        Select Case xLocationType_Index
    '                            Case "1" 'Storage ต้องย้าย
    '                            Case "2" 'Pick face rack ต้องย้าย
    '                            Case "3" 'Pick face ตะกร้า

    '                        End Select
    '                        If Result IsNot Nothing Then
    '                            If Result.Rows.Count > 0 Then
    '                                dtLocationBalance.Merge(Result)
    '                                Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                                Picking_Qty = Total_Qty
    '                                Pick_Floor -= 1
    '                            End If
    '                            If Total_Qty <= 0 Then Exit While
    '                        End If
    '                    Next

    '                    ''--------------------------------------------------------------------------------------------------
    '                    ''STEP 6 : FIFO ทั้งคลัง แบบไม่มีเติมเต็มสินค้า, ไม่เปิดใช้เพราะต้องทำเรื่องเติมเต็มสินค้า
    '                    'Picking_Qty = Total_Qty
    '                    '_LocationType_Index = ""
    '                    'strWhere = StrWherePicking("FIFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                    '                                            .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                    '                                            _LocationType_Index, _
    '                    '                                           _Connection, _myTrans)
    '                    'TypePicking = WMS_Site_KingStella.PICKING.enmPicking_Type.FIFO
    '                    ''Set Qty Picking FIFO
    '                    'objPicking = New WMS_Site_KingStella.PICKING(TypePicking, .Item("Sku_Index").ToString, .Item("Package_Index").ToString, Picking_Qty, strWhere, DocumentType_Index)
    '                    'Result = objPicking.fnPICKING_KSL(_Connection, _myTrans, SQLServerCommand, "")
    '                    'If Result IsNot Nothing Then
    '                    '    If Result.Rows.Count > 0 Then
    '                    '        dtLocationBalance.Merge(Result)
    '                    '        Total_Qty -= Result.Compute("SUM(Total_Qty)", "")
    '                    '        Pick_Floor -= 1
    '                    '    End If
    '                    'End If
    '                    'If Total_Qty <= 0 Then Exit While
    '                    '--------------------------------------------------------------------------------------------------


    '                    Total_Qty = 0
    '                    If Total_Qty <= 0 Then Exit While 'Fix Exit loop
    '                End While


    '                Dim Total_Qty_LocationBalance As Decimal = 0
    '                If dtLocationBalance Is Nothing Then dtLocationBalance = New DataTable
    '                If dtLocationBalance.Rows.Count > 0 Then
    '                    Total_Qty_LocationBalance = dtLocationBalance.Compute("SUM(Total_Qty)", "")
    '                End If
    '                'ครบสร้างรายการใบเบิกสินค้า
    '                If CDbl(.Item("Total_Qty")) = Total_Qty_LocationBalance Then
    '                    '7.	บังคับห้ามหยิบสินค้าเกิน 2 Lot ต่อ Item ต่อ Sale Order by Customer
    '                    If CInt(.Item("FlagMix_Lot")) = 1 Then
    '                        If utilDatatable.GroupDataTable(dtLocationBalance, New String() {"Exp_Date"}).Rows.Count > 2 Then
    '                            ' .Item("StatusPicking") = "Lot มากกว่า 2!!!"
    '                            _myTrans.Rollback(TransSAVE)
    '                            Continue For
    '                        End If
    '                    End If
    '                    MappingSOAndSaveWTHI(Withdraw_Index, dtSalesOrderPlan, drAutoPicking, dtLocationBalance, _Connection, _myTrans)
    '                Else
    '                    ' .Item("StatusPicking") = String.Format("สินค้าคงเหลือ {0}", Total_Qty_LocationBalance)
    '                    _myTrans.Rollback(TransSAVE)
    '                    Continue For
    '                End If
    '            End With

    '        Next
    '        If IsNotPassTransaction Then _myTrans.Commit()
    '        Return ""
    '    Catch ex As Exception
    '        If IsNotPassTransaction Then _myTrans.Rollback()
    '        Throw ex
    '    Finally
    '        If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
    '    End Try
    'End Function

    Public Function MappingSOAndSaveWTHI(ByVal Withdraw_Index As String, ByVal dtSalesOrderPlan As DataTable, ByVal drAutoPicking As DataRow, ByVal dtLocationBalance As DataTable, _
      Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If



            'TODO BUG Duplicate Item
            Dim dtSalesOrderPlanForSave As New DataTable
            'dtSalesOrderPlanForSave = utilDatatable.UnGroupDataTable(dtSalesOrderPlan, drAutoPicking, New String() {"Qty", "Total_Qty", "StatusPicking", "Qty_Plan", "Total_Qty_Plan", "QtyCARTON", "QtyPC"}, New String() {"SalesOrderItem_Index", "SalesOrder_Index", "SalesOrder_No", "Customer_Shipping_Index", "Customer_Shipping_Id", "Company_Name", "Shipping_Location_Name", "CustomerType_Index", "DistributionCenter_Index", "DistributionCenter"})
            'dtSalesOrderPlanForSave = utilDatatable.UnGroupDataTable(dtSalesOrderPlan, drAutoPicking, New String() {"Qty", "Total_Qty", "StatusPicking", "Qty_Plan", "Total_Qty_Plan", "QtyCARTON", "QtyPC", "FlagMix_Lot", "FlagDont_Reverse_LOT"}, New String() {"SalesOrderItem_Index", "SalesOrder_Index", "SalesOrder_No", "Customer_Shipping_Index", "Customer_Shipping_Id", "Company_Name", "Shipping_Location_Name", "CustomerType_Index", "DistributionCenter_Index", "DistributionCenter"})
            dtSalesOrderPlanForSave = utilDatatable.UnGroupDataTable(dtSalesOrderPlan, drAutoPicking, New String() {"Sku_Name", "Qty", "Total_Qty", "StatusPicking", "Qty_Plan", "Total_Qty_Plan", "QtyCARTON", "QtyPC", "FlagMix_Lot", "FlagDont_Reverse_LOT", "Total_Qty_Withdraw"}, New String() {"SalesOrderItem_Index", "SalesOrder_Index", "SalesOrder_No", "Customer_Shipping_Index", "Customer_Shipping_Id", "Company_Name", "Shipping_Location_Name", "CustomerType_Index", "DistributionCenter_Index", "DistributionCenter"})

            If dtSalesOrderPlanForSave.Rows.Count = 0 Then
                Throw New Exception("Error For Save")
            End If

            Dim Total_Qty_SalesOrder As Decimal = 0
            Dim dtLocationBalanceForSave As New DataTable
            dtLocationBalanceForSave = dtLocationBalance.Clone


            For Each drSalesOrderPlanForSave As DataRow In dtSalesOrderPlanForSave.Rows
                Total_Qty_SalesOrder = CDec(drSalesOrderPlanForSave("Total_Qty").ToString)
                dtLocationBalanceForSave.Clear()

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@SalesOrderItem_Index", SqlDbType.VarChar).Value = drSalesOrderPlanForSave("SalesOrderItem_Index")
                End With

                Dim QtyPlan As Decimal = DBExeQuery_Scalar(" select  isnull(Total_Qty,0) - isnull(Total_Qty_Withdraw,0) from tb_SalesOrderitem  Where SalesOrderItem_Index=@SalesOrderItem_Index  ", _Connection, _myTrans)

                If QtyPlan <= 0 Then Continue For
                For Each drLocationBalance As DataRow In dtLocationBalance.Rows
                    If Total_Qty_SalesOrder = 0 Then Exit For

                    If CDec(drLocationBalance("Total_Qty").ToString) > 0 And CDec(drLocationBalance("Total_Qty").ToString) >= Total_Qty_SalesOrder Then
                        dtLocationBalanceForSave.Rows.Add(drLocationBalance.ItemArray)
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Total_Qty") = Total_Qty_SalesOrder
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Qty") = Total_Qty_SalesOrder

                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("QtyItemOut") = (drLocationBalance("QtyItemOut") / drLocationBalance("Total_Qty")) * Total_Qty_SalesOrder
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("WeightOut") = (drLocationBalance("WeightOut") / drLocationBalance("Total_Qty")) * Total_Qty_SalesOrder
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("VolumeOut") = (drLocationBalance("VolumeOut") / drLocationBalance("Total_Qty")) * Total_Qty_SalesOrder
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Price_Out") = (drLocationBalance("Price_Out") / drLocationBalance("Total_Qty")) * Total_Qty_SalesOrder


                        drLocationBalance("Total_Qty") = CDec(drLocationBalance("Total_Qty")) - Total_Qty_SalesOrder
                        drLocationBalance("QtyItemOut") = CDec(drLocationBalance("QtyItemOut")) - CDec(dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("QtyItemOut"))
                        drLocationBalance("WeightOut") = CDec(drLocationBalance("WeightOut")) - CDec(dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("WeightOut"))
                        drLocationBalance("VolumeOut") = CDec(drLocationBalance("VolumeOut")) - CDec(dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("VolumeOut"))
                        drLocationBalance("Price_Out") = CDec(drLocationBalance("Price_Out")) - CDec(dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Price_Out"))

                        Total_Qty_SalesOrder = 0
                        dtLocationBalance.AcceptChanges()
                        Exit For
                    ElseIf CDec(drLocationBalance("Total_Qty").ToString) > 0 And CDec(drLocationBalance("Total_Qty").ToString) < Total_Qty_SalesOrder Then
                        dtLocationBalanceForSave.Rows.Add(drLocationBalance.ItemArray)
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Total_Qty") = CDec(drLocationBalance("Total_Qty").ToString)
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Qty") = CDec(drLocationBalance("Total_Qty").ToString)

                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("QtyItemOut") = (drLocationBalance("QtyItemOut") / CDec(drLocationBalance("Total_Qty").ToString)) * CDec(drLocationBalance("Total_Qty").ToString)
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("WeightOut") = (drLocationBalance("WeightOut") / CDec(drLocationBalance("Total_Qty").ToString)) * CDec(drLocationBalance("Total_Qty").ToString)
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("VolumeOut") = (drLocationBalance("VolumeOut") / CDec(drLocationBalance("Total_Qty").ToString)) * CDec(drLocationBalance("Total_Qty").ToString)
                        dtLocationBalanceForSave.Rows(dtLocationBalanceForSave.Rows.Count - 1)("Price_Out") = (drLocationBalance("Price_Out") / CDec(drLocationBalance("Total_Qty").ToString)) * CDec(drLocationBalance("Total_Qty").ToString)


                        Total_Qty_SalesOrder = Total_Qty_SalesOrder - CDec(drLocationBalance("Total_Qty").ToString)


                        drLocationBalance("Total_Qty") = 0
                        drLocationBalance("QtyItemOut") = 0
                        drLocationBalance("WeightOut") = 0
                        drLocationBalance("VolumeOut") = 0
                        drLocationBalance("Price_Out") = 0




                        dtLocationBalance.AcceptChanges()
                    End If

                Next


                CreateWTHIL(Withdraw_Index, drSalesOrderPlanForSave.Item("SalesOrder_No").ToString, drSalesOrderPlanForSave.Item("SalesOrderItem_Index").ToString, drSalesOrderPlanForSave.Item("SalesOrder_Index").ToString, 10, dtLocationBalanceForSave, _Connection, _myTrans)

            Next




            If IsNotPassTransaction Then _myTrans.Commit()
            Return ""
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    'Public Function Picking(ByVal Sku_Index As String, ByVal Package_Index As String, ByVal DocumentType_Index As String, ByVal QtyPicking As Decimal, ByVal StrWherePicking As String, ByVal Type As WMS_Site_KingStella.PICKING.enmPicking_Type, _
    '    Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
    '    Dim IsNotPassTransaction As Boolean = False
    '    Dim Result As New DataTable
    '    Try

    '        If _Connection Is Nothing Then
    '            _Connection = Connection
    '            With SQLServerCommand
    '                .Connection = _Connection
    '                If .Connection.State = ConnectionState.Open Then .Connection.Close()
    '                .Connection.Open()
    '                .Transaction = _myTrans
    '            End With
    '            _myTrans = Connection.BeginTransaction()
    '            IsNotPassTransaction = True
    '        End If

    '        ' Grouping tag. ก่อนหยิบทั้งพาเลท
    '        'Dim dtPickTAG As New DataTable
    '        'Dim objPicking As New WMS_Site_KingStella.PICKING(Type, Sku_Index, Package_Index, QtyPicking, StrWherePicking, DocumentType_Index)
    '        'objPicking.strOrderForV3 = ""
    '        'dtPickTAG = objPicking.FIFO_FULL_TAG_SEARCH_PICKING(_Connection, _myTrans, SQLServerCommand)

    '        Dim objPicking As New WMS_Site_KingStella.PICKING(Type, Sku_Index, Package_Index, QtyPicking, StrWherePicking, DocumentType_Index)
    '        objPicking.strOrderForV3 = ""
    '        Result = objPicking.fnPICKING(_Connection, _myTrans, SQLServerCommand)

    '        If IsNotPassTransaction Then _myTrans.Commit()
    '        Return Result
    '    Catch ex As Exception
    '        If IsNotPassTransaction Then _myTrans.Rollback()
    '        Throw ex
    '    Finally
    '        If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
    '    End Try
    'End Function


    Public Function GetDataLastIssueOption(ByVal pSku_Index As String, ByVal pCustomer_Shipping_Index As String, _
    Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim Result As DataTable
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = pSku_Index
                .Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = pCustomer_Shipping_Index
            End With

            Result = DBExeQuery(" SELECT * FROM VIEW_LastIssueOption Where Sku_Index=@Sku_Index AND Customer_Shipping_Index=@Customer_Shipping_Index  ", _Connection, _myTrans)




            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function CreateWTH(ByVal DocumentType_Index As String, ByVal Customer_Index As String, _
     Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim DateNowServer As Date = CDate(DBExeQuery_Scalar("SELECT getDate() as DateNowServer", _Connection, _myTrans))

            Dim objHeader As New WMS_STD_OUTB_WithDraw_Datalayer.tb_Withdraw
            objHeader.Withdraw_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "Withdraw_Index") 'Me._Withdraw_index
            objHeader.Withdraw_Date = DateNowServer.ToString("yyyy/MM/dd") 'CDate(Me.dtpWithdraw_Date.Value.ToString("yyyy/MM/dd"))
            '--- ประเภทการเบิก
            objHeader.DocumentType_Index = DocumentType_Index '"0010000000007" 'Me.cboDocumentType.SelectedValue
            objHeader.Withdraw_No = "TMP" & DateNowServer.ToString("yyyyddMMHHmmssfff")  'New Sy_DocumentNumber_New().Auto_DocumentType_Number(objHeader.DocumentType_Index, "", objHeader.Withdraw_Date, _Connection, _myTrans)  'Me.txtWithdraw_No.Text
            '--- ลูกค้า Need 
            objHeader.Customer_Index = Customer_Index '"0010000000001" 'Me._Customer_Index.ToString
            '--- ผู้ติดต่อ
            objHeader.Contact_Name = "" 'Me.cboContact_Name.Text
            '--- แผนก
            objHeader.Department_Index = "" 'Me.txtDepartment_Id.Tag.ToString
            '--- ผู้รับ
            objHeader.Customer_Shipping_Index = "" 'txtConsignee_ID.Tag.ToString
            '--- ผู้ขาย
            objHeader.Shipper_Index = "" 'Me.txtShipper_ID.Tag.ToString
            '--- Referrent
            objHeader.Ref_No1 = "" 'Me.txtRef_No1.Text                              '3PL M. B/L, MAWB               INH เอกสารอ้างอิง1
            objHeader.Ref_No2 = "" 'Me.txtRef_No2.Text                              '3PL H. B/L, HAWB               INH เอกสารอ้างอิง2
            objHeader.Ref_No3 = "" 'Me.txtRef_No3.Text                              '3PL วันขึ้นรถ ย้ายไป Truck out จ้า        INH เอกสารอ้างอิง3
            objHeader.Ref_No4 = "" 'Me.txtRef_No4.Text                              '3PL Declaration No.            INH เอกสารอ้างอิง4
            objHeader.Ref_No5 = "" 'Me.txtRef_No5.Text                              '3PL To Terminal                INH เอกสารอ้างอิง5
            '--- Text For Assign You Can Edit For Site
            objHeader.Str1 = ""
            objHeader.Str2 = ""
            objHeader.Str3 = ""
            objHeader.Str4 = "" 'Me.txtNote.Text                                    'Use For Deimos
            objHeader.Str5 = ""
            objHeader.Str6 = ""                               'ประเภทพาหนะ
            objHeader.Str7 = ""
            objHeader.Str8 = ""
            objHeader.Str9 = ""
            objHeader.Str10 = ""
            '--- Float For Assigne You Can Edit
            objHeader.Flo1 = 0
            objHeader.Flo2 = 0
            objHeader.Flo3 = 0
            objHeader.Flo4 = 0
            objHeader.Flo5 = 0
            '--- Document Import Data       
            objHeader.SO_No = "" 'txtSo_No.Text                               'Use For Deimos
            'objHeader.ASN_No = txtASN_No.Text                                   'Use For Deimos เลขที่ Seal
            objHeader.Invoice_No = "" 'txtInvoice_No.Text
            objHeader.Comment = "" 'txtComment.Text
            objHeader.Withdraw_Type = 0 'chkWithdrawType.Checked
            '--- รายละเอียดการนำออก Use For 3PL นะ ส่วนมาก
            objHeader.Vassel_Name = "" 'Me.txtVessel_Name.Text                      'ชื่อเรือ (Vessel Name)
            objHeader.Flight_No = "" 'Me.txtFlight_No.Text                          'เที่ยวบิน (Flight No)
            objHeader.Vehicle_No = ""                                           ' Change In Truck Out
            objHeader.Transport_by = "" 'Me.txtTransport_by.Text                    ' ประเภทพาหนะ
            objHeader.Origin_Port_Id = "" 'Me.txtOrigin_Port.Text                   'Port ต้นทาง
            objHeader.Destination_Port_Id = "" 'Me.txtDestination_Port.Text         'Port ปลายทาง
            objHeader.Origin_Country_Id = "" 'Me.txtOrigin_Country.Text             'ประเทศต้นทาง
            objHeader.Destination_Country_Id = "" ' Me.txtDestination_Country.Text   'ประเทศปลายทาง
            objHeader.Terminal_Id = "" 'Me.txtTerminal.Text                         'ส่ง Terminal/WA
            objHeader.Departure_Date = DateNowServer.ToString("yyyy/MM/dd") 'Me.dtpDeparture_Date.Value.Date          'Departure_Date
            objHeader.Arrival_Date = DateNowServer.ToString("yyyy/MM/dd") 'Me.dtpArrival_Date.Value.Date              'Arrival_Date
            objHeader.Checker_Name = "" 'txtChecker_Name.Text.ToString              'Checker_Name
            objHeader.ApprovedBy_Name = "" 'txtApprovede_By.Text.ToString           'Approvede_By
            '---- Delivery Information Don't Use น่าจาลบ เพราะ เห็นว่าย้ายไปที่ ย้ายไปที่ tb_WithDrawTruckOut แล้วนิ จ๊ะ
            objHeader.Driver_Index = "" ' cbDriver.SelectedValue.ToString
            objHeader.Round = DateNowServer.ToString("yyyy/MM/dd")
            objHeader.Leave_Time = DateNowServer.ToString("yyyy/MM/dd")
            objHeader.Factory_In = DateNowServer.ToString("yyyy/MM/dd")
            objHeader.Factory_Out = DateNowServer.ToString("yyyy/MM/dd")
            objHeader.Return_Time = DateNowServer.ToString("yyyy/MM/dd")



            Result = New GI_Logic().InsertWTH(objHeader, _Connection, _myTrans)


            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function CreateWTHIL(ByVal Withdraw_Index As String, ByVal DocumentPlan_No As String, ByVal DocumentPlanItem_Index As String, ByVal DocumentPlan_Index As String, ByVal Plan_Process As Integer, ByVal dtLocationbalance As DataTable, _
       Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim DateNowServer As Date = CDate(DBExeQuery_Scalar("SELECT getDate() as DateNowServer", _Connection, _myTrans))

            Dim objItem As New tb_WithdrawItem
            Dim objDBItemWithdrawItemLocation As New tb_WithdrawItemLocation
            Dim objItemCollection As New List(Of tb_WithdrawItem)
            Dim objItemCollectionWITL As New List(Of tb_WithdrawItemLocation)


            Dim objPalletType As New tb_PalletType_History
            Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

            If Not dtLocationbalance.Columns.Contains("TransferStatus_Index") Then
                dtLocationbalance.Columns.Add("TransferStatus_Index", GetType(String))
            End If


            'Detail
            For Each drDetail As DataRow In dtLocationbalance.Rows
                objItem = New WMS_STD_OUTB_WithDraw_Datalayer.tb_WithdrawItem
                '--- Withdraw_Index
                objItem.Withdraw_Index = Withdraw_Index
                '--- Create WithdrawItem_Index
                objItem.WithdrawItem_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "WithdrawItem_Index")
                '--- LocationBalane_index
                objItem.LocationBalane_index = drDetail("LocationBalance_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_LocationBalance_Index2").Value.ToString, GetType(String))
                '--- SKU_Index
                objItem.Sku_Index = drDetail("Sku_Index").ToString ' ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Sku_Index2").Value, GetType(String))
                '--- ItemStatus_Index
                objItem.ItemStatus_Index = drDetail("ItemStatus_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemStatus_Index2").Value, GetType(String))
                '--- QTY จำนวนที่เบิก
                objItem.Qty = CDec(drDetail("Qty").ToString) 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Withdraw_Package_Qty2").Value, GetType(String))
                '--- Package_Index หน่วยที่เบิก
                objItem.Package_Index = drDetail("Package_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Package_IndexWithdraw2").Value, GetType(String))
                '--- RATIO 

                objItem.Ratio = New GI_Logic().getRatio(objItem.Sku_Index, objItem.Package_Index, _Connection, _myTrans)


                objItem.Total_Qty = CDec(drDetail("Total_Qty").ToString)  'Total_Qty

                ' *** set Total_Qty = 0 ***
                '--- Plan_Qty จำนวนคาดว่าจะบิก ถูกกำหนดจากการ Import เช่น SO PACKING
                '--- Set Plan_Qty = Total_Qty เพราะว่า จำนวนที่คาดว่า น่าจะเท่ากับ จำนวนที่เบิก
                objItem.Plan_Qty = objItem.Qty
                objItem.Plan_Total_Qty = objItem.Total_Qty

                '   --- ITEM_QTY
                objItem.ItemQty = CDec(drDetail("QtyItemOut").ToString) 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemQty2").Value, GetType(String))

                '--- WEIGHT
                objItem.Weight = CDec(drDetail("WeightOut").ToString) 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Weight2").Value, GetType(String))

                '--- VOLUME
                objItem.Volume = CDec(drDetail("VolumeOut").ToString) 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Volume2").Value, GetType(String))

                '--- ILOT/BATH
                objItem.PLot = drDetail("PLot").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Plot2").Value, GetType(String))

                '--- CERTIFICATE
                objItem.Serial_No = drDetail("Serial_No").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Serial_No2").Value, GetType(String))

                '--- ADDNEW NEED
                objItem.NewItem = 0 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_NewItem2").Value, GetType(Integer))

                '--- PRICE
                objItem.Price = CDec(drDetail("Price_Out").ToString)  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Price2").Value, GetType(String))

                '  -------------------------- DOCUMENT -----------------------------
                objItem.Item_Package_Index = drDetail("Item_Package_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Item_Package_Index2").Value, GetType(String))

                '--- DECLARATION
                objItem.Declaration_No = "" 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Declaration_No_Out").Value, GetType(String))

                '--- HANDLING TYPE
                objItem.HandlingType_Index = drDetail("HandlingType_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("cbo_HandlingType2").Value, GetType(String))

                '--- ADDNEW NEED
                objItem.Invoice_No = drDetail("Invoice_Out").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Invoice_Out2").Value, GetType(String))
                '--- String 

                '***********************    REFERECNE   *************************
                objItem.Str1 = "" 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Reference").Value, GetType(String))

                'add on ksl
                objItem.Str1 = drDetail("TransferStatus_Index").ToString
                objItem.Str2 = "" 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Reference2").Value, GetType(String))
                objItem.ItemDefinition_Index = drDetail("ItemDefinition_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemDefinition_Index").Value, GetType(String))
                '************************************************************************

                '--- INVOICE
                objItem.Str3 = drDetail("Invoice_No").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Invoice_In2").Value, GetType(String))

                '--- PALLET NO
                objItem.Str4 = drDetail("Pallet_No").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Pallet_No2").Value, GetType(String))

                '--- PALLET NO
                objItem.Str5 = "" 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Comment").Value, GetType(String))

                '--- Plan WithDraw 
                '--- Type
                objItem.Plan_Process = Plan_Process 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Plan_Process2").Value, GetType(Integer))
                '--- Index
                objItem.DocumentPlan_No = DocumentPlan_No 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_DocumentPlan_No2").Value.ToString, GetType(String))
                '--- ItemIndex
                objItem.DocumentPlanItem_Index = DocumentPlanItem_Index 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_DocumentPlanItem_Index2").Value.ToString, GetType(String))

                objItem.DocumentPlan_Index = DocumentPlan_Index 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_DocumentPlan_Index2").Value.ToString, GetType(String))

                '--- AssetLocationBalance_Index
                objItem.AssetLocationBalance_Index = "" 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_AssetLocationBalance_Index").Value.ToString, GetType(String))

                objItem.Seq = objItemCollection.Count + 1  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Seq").Value, GetType(Integer))

                '--- Empty Float
                '--- WEIGHT
                objItem.Flo1 = CDec(drDetail("Flo1").ToString) 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_2Flo1").Value.ToString, GetType(Decimal))

                '   objItem.Flo1 = 0
                objItem.Flo2 = 0
                objItem.Flo3 = 0
                objItem.Flo4 = 0
                objItem.Flo5 = 0

                'tax
                objItem.Tax1 = CDec(drDetail("Tax1").ToString) 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax21").Value, GetType(Decimal))

                objItem.Tax2 = CDec(drDetail("Tax2").ToString)  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax22").Value, GetType(Decimal))

                objItem.Tax3 = CDec(drDetail("Tax3").ToString)  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax23").Value, GetType(Decimal))

                objItem.Tax4 = CDec(drDetail("Tax4").ToString)  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax24").Value, GetType(Decimal))

                objItem.Tax5 = CDec(drDetail("Tax5").ToString)  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax25").Value, GetType(Decimal))


                '11-02-2010 ja Update str6
                objItem.Str6 = drDetail("Str6").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_2Str6").Value.ToString, GetType(String))


                objItem.HS_Code = drDetail("HS_Code").ToString  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_HS_Code2").Value.ToString, GetType(String))

                objItem.ItemDescription = drDetail("ItemDescription").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemDescription2").Value.ToString, GetType(String))


                objItem.Consignee_Index = drDetail("ConsigneeItem_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ConsigneeItem_Index2").Value.ToString, GetType(String))


                '16-02-2010 ja OrderItem_Index
                objItem.OrderItem_Index = drDetail("OrderItem_Index").ToString 'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Orderitem_WIL2").Value.ToString, GetType(String))

                objItem.ERP_Location = drDetail("ERP_Location").ToString


                '--- ADD For WithDrawItemLocation
                objDBItemWithdrawItemLocation = New tb_WithdrawItemLocation

                objDBItemWithdrawItemLocation.WithdrawItemLocation_Index = New Sy_AutoNumber().getSys_Value(_Connection, _myTrans, "WithdrawItemLocation_Index")

                '''''''Killz  GEN TAGOUT NO FOR TB_WithDrawItemLocation   

                'tagout_no = GENTAGOUT_NO(_Withdraw_no, objDBItemWithdrawItemLocation.WithdrawItemLocation_Index)
                'Dim tagout_no As String = ""
                'If Config_PrintPalletSlip() = 1 Then
                '    Dim objGEN_NO As New Sy_AutoyyyyMM_WithDrawTag
                '    tagout_no = objGEN_NO.Auto_DocumentType_Number_NT(cboDocumentType.SelectedValue, dtpWithdraw_Date.Value, "")
                'End If

                objDBItemWithdrawItemLocation.TAG_Index = drDetail("TAG_Index").ToString 'grdWithdrawItemLocation.Rows(i).Cells("Col_TAG_Index").Value.ToString
                objDBItemWithdrawItemLocation.Withdraw_Index = Withdraw_Index 'Me._Withdraw_index
                objDBItemWithdrawItemLocation.WithdrawItem_Index = objItem.WithdrawItem_Index 'grdWithdrawItemLocation.Rows(i).Cells("col_WithDrawItem_Index2").Value.ToString
                objDBItemWithdrawItemLocation.Order_Index = drDetail("Order_Index").ToString 'grdWithdrawItemLocation.Rows(i).Cells("col_Order_Index2").Value.ToString
                objDBItemWithdrawItemLocation.Sku_Index = objItem.Sku_Index 'grdWithdrawItemLocation.Rows(i).Cells("Col_Sku_Index2").Value.ToString
                objDBItemWithdrawItemLocation.Lot_No = drDetail("Lot_No").ToString
                objDBItemWithdrawItemLocation.Plot = drDetail("Plot").ToString 'grdWithdrawItemLocation.Rows(i).Cells("Col_PLot2").Value.ToString
                objDBItemWithdrawItemLocation.ItemStatus_Index = drDetail("ItemStatus_Index").ToString 'grdWithdrawItemLocation.Rows(i).Cells("Col_ItemStatus_Index2").Value.ToString
                objDBItemWithdrawItemLocation.Tag_No = drDetail("Tag_No").ToString 'grdWithdrawItemLocation.Rows(i).Cells("Col_Tag_No2").Value.ToString
                objDBItemWithdrawItemLocation.LocationBalance_Index = drDetail("LocationBalance_Index").ToString 'grdWithdrawItemLocation.Rows(i).Cells("Col_LocationBalance_Index2").Value.ToString
                objDBItemWithdrawItemLocation.Location_Index = drDetail("Location_Index").ToString  'grdWithdrawItemLocation.Rows(i).Cells("col_Location_Index2").Value.ToString
                objDBItemWithdrawItemLocation.Serial_No = drDetail("Serial_No").ToString  'grdWithdrawItemLocation.Rows(i).Cells("Col_Serial_No2").Value.ToString
                objDBItemWithdrawItemLocation.Qty = objItem.Qty ' CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Withdraw_Package_Qty2").Value.ToString)
                objDBItemWithdrawItemLocation.Package_Index = objItem.Package_Index 'grdWithdrawItemLocation.Rows(i).Cells("Col_Package_IndexWithdraw2").Value.ToString
                objDBItemWithdrawItemLocation.Total_Qty = objItem.Total_Qty ' CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Qty_sku2").Value.ToString)
                objDBItemWithdrawItemLocation.Weight = objItem.Weight 'CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Weight2").Value.ToString)
                objDBItemWithdrawItemLocation.Volume = objItem.Volume 'CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Volume2").Value.ToString)
                objDBItemWithdrawItemLocation.Pallet_Qty = CDec(drDetail("Pallet_Qty").ToString) 'CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Pallet_Qty2").Value.ToString)
                objDBItemWithdrawItemLocation.Item_Qty = CDec(drDetail("QtyItemOut").ToString)  'CDec(grdWithdrawItemLocation.Rows(i).Cells("col_ItemQty2").Value.ToString)
                objDBItemWithdrawItemLocation.Price = CDec(drDetail("Price_Out").ToString) ' CDec(grdWithdrawItemLocation.Rows(i).Cells("col_Price2").Value.ToString)
                objDBItemWithdrawItemLocation.Status = drDetail("Status").ToString 'grdWithdrawItemLocation.Rows(i).Cells("Col_Status_WIL").Value.ToString
                objDBItemWithdrawItemLocation.TagOut_No = ""
                objDBItemWithdrawItemLocation.ERP_Location = objItem.ERP_Location

                objItemCollectionWITL.Add(objDBItemWithdrawItemLocation)
                objItemCollection.Add(objItem)

            Next






            Result = New GI_Logic().InsertWTHI(DateNowServer.ToString("yyyy/MM/dd"), objItemCollection, objItemCollectionWITL, objPalletTypeCollection, _Connection, _myTrans)

            'Add on
            DBExeNonQuery(" update tb_Withdrawitem set TransferStatus_Index = Str1,Str1 = '' where Withdraw_Index = '" & Withdraw_Index & "' ", _Connection, _myTrans)

            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function DeleteWithdraw(ByVal Withdraw_index As String, _
        Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim objDeleteWithdraw As New WMS_STD_OUTB_Datalayer.Cl_WithdrawReserv
            objDeleteWithdraw.DeleteWithdraw(_Connection, _myTrans, Withdraw_index)




            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getCountItemWithdraw(ByVal Withdraw_index As String, _
           Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Integer
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As Integer = 0
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_index", SqlDbType.VarChar, 13).Value = Withdraw_index



            Result = CInt(DBExeQuery_Scalar("SELECT count(WithdrawItem_Index) as CountItem FROM tb_Withdraw tw inner join tb_WithdrawItem twi on twi.Withdraw_Index = tw.Withdraw_Index WHERE tw.Withdraw_Index=@Withdraw_Index ", _Connection, _myTrans))



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function UpdateWithdrawNo(ByVal Withdraw_index As String, ByVal pComment As String, _
           Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = "OK"
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_index", SqlDbType.VarChar, 13).Value = Withdraw_index

            Dim dtWithdraw As DataTable = DBExeQuery(" SELECT TOP 1 * FROM tb_withdraw where Withdraw_index=@Withdraw_index", _Connection, _myTrans)

            Dim dtWithdrawItem As DataTable = DBExeQuery(" SELECT * FROM tb_withdrawItem where Withdraw_index=@Withdraw_index", _Connection, _myTrans)


            If dtWithdraw.Rows.Count > 0 And dtWithdrawItem.Rows.Count > 0 Then

                Result = New Sy_DocumentNumber_New().Auto_DocumentType_Number(dtWithdraw.Rows(0)("DocumentType_Index").ToString, "", CDate(dtWithdraw.Rows(0)("Withdraw_Date").ToString), _Connection, _myTrans)

                SQLServerCommand.Parameters.Clear()
                SQLServerCommand.Parameters.Add("@Withdraw_index", SqlDbType.VarChar, 13).Value = Withdraw_index
                SQLServerCommand.Parameters.Add("@Withdraw_No", SqlDbType.VarChar, 50).Value = Result
                SQLServerCommand.Parameters.Add("@pComment", SqlDbType.NVarChar, 255).Value = pComment


                DBExeNonQuery(" UPDATE tb_withdraw set Withdraw_No =@Withdraw_No, Comment=@pComment Where Withdraw_index=@Withdraw_index", _Connection, _myTrans)
                DBExeNonQuery(" UPDATE tb_withdrawitem set NewItemFlag =0 Where Withdraw_index=@Withdraw_index", _Connection, _myTrans)
                DBExeNonQuery(" UPDATE tb_transferstatus set Ref_No1 =@Withdraw_No Where Withdraw_index=@Withdraw_index", _Connection, _myTrans)

            End If






            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function IsCreateWithdraw_Set(ByVal SalesOrder_Index As String, _
               Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If



            builder = New System.Text.StringBuilder
            builder.Append(" UPDATE tb_SalesOrder ")
            builder.Append(" SET IsCreateWithdraw = 1 ")
            builder.Append(" WHERE SalesOrder_Index = @SalesOrder_Index AND IsCreateWithdraw = 0 ")

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index


            If DBExeNonQuery(builder.ToString, _Connection, _myTrans) > 0 Then
                Return True
            Else
                Return False
            End If



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function IsCreateWithdraw_Clear(ByVal SalesOrder_Index As String, _
             Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If



            builder = New System.Text.StringBuilder
            builder.Append(" UPDATE tb_SalesOrder ")
            builder.Append(" SET IsCreateWithdraw = 0 ")
            builder.Append(" WHERE SalesOrder_Index = @SalesOrder_Index  ")

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index


            If DBExeNonQuery(builder.ToString, _Connection, _myTrans) > 0 Then
                Return True
            Else
                Return False
            End If



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getLocationSKU_Pickface(ByVal Sku_Index As String, ByVal ItemStatus_Index As String, _
             Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = Sku_Index
            SQLServerCommand.Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = ItemStatus_Index


            Result = DBExeQuery("Select Location_Index from ms_SKU_Pickface where Sku_Index = @Sku_Index and ItemStatus_Index = @ItemStatus_Index AND isnull(Location_Index,'') <> '' ", _Connection, _myTrans)



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getWithdrawNo(ByVal Withdraw_Index As String, _
               Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = Withdraw_Index


            Result = DBExeQuery_Scalar(" select Withdraw_No from tb_Withdraw Where Withdraw_Index=@Withdraw_Index ", _Connection, _myTrans)



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getTransferStatus_IndexByWithdraw_Index(ByVal Withdraw_Index As String, _
               Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = Withdraw_Index


            Result = DBExeQuery(" select TransferStatus_Index from tb_TransferStatus Where Withdraw_Index=@Withdraw_Index and isnull(TransferStatus_Index,'') <> '' ", _Connection, _myTrans)



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function UpdateTransferStatus_IndexByWithdraw_Index(ByVal TransferStatus_Index As String, ByVal Withdraw_Index As String, _
               Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = Withdraw_Index
            SQLServerCommand.Parameters.Add("@TransferStatus_Index", SqlDbType.VarChar, 13).Value = TransferStatus_Index

            Result = DBExeNonQuery(" Update tb_Withdraw SET TransferStatus_Index=@TransferStatus_Index Where Withdraw_Index=@Withdraw_Index ", _Connection, _myTrans)



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function CreateTFH(ByVal Withdraw_Index As String, ByVal DocumentType_Index As String, ByVal Customer_Index As String, ByVal Comment As String, _
      Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim DateNowServer As Date = CDate(DBExeQuery_Scalar("SELECT getDate() as DateNowServer", _Connection, _myTrans))

            Dim objHeader As New WMS_STD_TMM_Transfer_Datalayer.tb_TransferStatus
            With objHeader
                .TransferStatus_Index = ""
                .TransferStatus_No = "" 'Me.txtTransferStatus_No.Text
                .TransferStatus_Date = DateNowServer.ToString("yyyy/MM/dd")
                .Customer_Index = Customer_Index
                .Ref_No1 = "" 'Me.txtRef_No1.Text
                .Ref_No2 = "" 'Me.txtRef_No2.Text
                .Str2 = ""
                .Times = DateNowServer.ToString("HH:mm") 'Me.txtTimes.Text
                .Comment = Comment
                .DocumentType_Index = DocumentType_Index
                .Str1 = "" 'Me.txtCustomer_Ship_Name.Tag

            End With


            Result = New TF_Logic().InsertTF(objHeader, _Connection, _myTrans)

            If Withdraw_Index <> "" Then

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = Withdraw_Index
                    .Add("@TransferStatus_Index", SqlDbType.VarChar, 13).Value = Result
                End With

                DBExeNonQuery(" UPDATE tb_TransferStatus set Withdraw_Index=@Withdraw_Index Where TransferStatus_Index=@TransferStatus_Index ", _Connection, _myTrans)
            End If

            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function CreateTFL(ByVal TransferStatus_Index As String, ByVal To_Location_Index As String, ByVal dtLocationBalance As DataTable, _
     Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim DateNowServer As Date = CDate(DBExeQuery_Scalar("SELECT getDate() as DateNowServer", _Connection, _myTrans))

            Dim objDetail As New tb_TransferStatusLocation 'tb_TransferStatusLocation_Update
            Dim objDetailL As New List(Of tb_TransferStatusLocation)

            Dim i As Integer = 0
            For Each dr As DataRow In dtLocationBalance.Rows
                objDetail = New tb_TransferStatusLocation
                i += 1
                With objDetail
                    .TransferStatusLocation_Index = ""
                    .TransferStatus_Index = TransferStatus_Index
                    .From_LocationBalance_Index = dr("LocationBalance_Index").ToString
                    .To_LocationBalance_Index = .From_LocationBalance_Index
                    .Sku_Index = dr("SKU_Index").ToString
                    .Package_Index = dr("Package_Index").ToString
                    .Order_Index = dr("Order_Index").ToString
                    .OrderItem_Index = dr("OrderItem_Index").ToString
                    .Lot_No = dr("Lot_No").ToString
                    .Plot = dr("Plot").ToString
                    .TAG_Index = dr("TAG_Index").ToString
                    .Tag_No = dr("Tag_No").ToString
                    .Qty = dr("Qty").ToString
                    .Total_Qty = dr("Total_Qty").ToString
                    .Ratio = dr("Ratio").ToString
                    .Weight = dr("WeightOut").ToString
                    .Volume = dr("VolumeOut").ToString
                    .Item_Package_Index = dr("Item_Package_Index").ToString
                    .Item_Qty = dr("QtyItemOut").ToString
                    .Price = dr("Price_Out").ToString
                    .Pallet_Qty = dr("Pallet_Qty").ToString
                    .Serial_No = dr("Serial_No").ToString
                    .Asset_No = ""
                    .MfgDate = dr("Mfg_Date").ToString
                    .ExpDate = dr("Exp_Date").ToString
                    .PallNo = dr("Pallet_No").ToString
                    .ERP_Location_From = dr("ERP_Location").ToString
                    .Status = 1
                    .Str1 = ""
                    .Str2 = ""
                    .Str3 = ""
                    .Str4 = ""
                    .Str5 = ""
                    .AssetLocationBalance_Index = ""
                    .Flo1 = 0
                    .Flo2 = 0
                    .Flo3 = 0
                    .Flo4 = 0
                    .Flo5 = 0
                    '.ItemSeq = i + 1

                    'KSL : Manual Assign
                    If To_Location_Index.ToString.Trim.Length > 0 Then
                        .To_Location_Index = To_Location_Index
                    Else
                        If dtLocationBalance.Columns.Contains("To_Location_Index") Then
                            .To_Location_Index = dr("To_Location_Index").ToString
                        End If
                    End If
                    If .ERP_Location_TO.ToString.Trim.Length > 0 Then
                        .ERP_Location_TO = .ERP_Location_From
                    Else
                        If dtLocationBalance.Columns.Contains("ERP_Location_TO") Then
                            .ERP_Location_TO = dr("ERP_Location_TO").ToString
                        End If
                    End If
                    If .PalletType_Index.ToString.Trim.Length > 0 Then
                        .PalletType_Index = .PalletType_Index
                    Else
                        If dtLocationBalance.Columns.Contains("PalletType_Index") Then
                            .PalletType_Index = dr("PalletType_Index").ToString
                        End If
                    End If
                    If .Old_ItemStatus_Index.ToString.Trim.Length > 0 Then
                        .Old_ItemStatus_Index = .Old_ItemStatus_Index
                    Else
                        If dtLocationBalance.Columns.Contains("Old_ItemStatus_Index") Then
                            .Old_ItemStatus_Index = dr("Old_ItemStatus_Index").ToString
                        Else
                            .Old_ItemStatus_Index = dr("ItemStatus_Index").ToString
                        End If
                    End If
                    If .New_ItemStatus_Index.ToString.Trim.Length > 0 Then
                        .New_ItemStatus_Index = .New_ItemStatus_Index
                    Else
                        If dtLocationBalance.Columns.Contains("New_ItemStatus_Index") Then
                            .New_ItemStatus_Index = dr("New_ItemStatus_Index").ToString
                        Else
                            .New_ItemStatus_Index = dr("ItemStatus_Index").ToString
                        End If
                    End If
                    If .From_Location_Index.ToString.Trim.Length > 0 Then
                        .From_Location_Index = .From_Location_Index
                    Else
                        If dtLocationBalance.Columns.Contains("From_Location_Index") Then
                            .From_Location_Index = dr("From_Location_Index").ToString
                        Else
                            .From_Location_Index = dr("Location_Index").ToString
                        End If
                    End If

                End With
                objDetailL.Add(objDetail)
            Next


            Result = New TF_Logic().InsertTFL(objDetailL.ToArray, _Connection, _myTrans)


            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    'Public Function AutoTransfer(ByVal drAutoPicking As DataRow, ByVal drWithdrawLocationType As DataRow, ByVal Customer_Shipping_Index As String, ByVal Withdraw_Index As String, ByVal To_Location_Index As String, ByVal TypePicking As WMS_Site_KingStella.PICKING.enmPicking_Type, ByVal Total_Qty As Decimal, ByVal MINExp_Date As String, _
    '      Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
    '    Dim IsNotPassTransaction As Boolean = False
    '    Dim Result As String = ""
    '    Try

    '        If _Connection Is Nothing Then
    '            _Connection = Connection
    '            With SQLServerCommand
    '                .Connection = _Connection
    '                If .Connection.State = ConnectionState.Open Then .Connection.Close()
    '                .Connection.Open()
    '                .Transaction = _myTrans
    '            End With
    '            _myTrans = Connection.BeginTransaction()
    '            IsNotPassTransaction = True
    '        End If

    '        With drAutoPicking


    '            Dim TF_DocumentType_Index As String = "0010000000058" 'TODO SINO Config TF_DocumentType_Index
    '            Dim TransTFSAVE As String = Now.ToString("TFyyyyddMMHHmmssfff")
    '            Dim dtlbCheck As New DataTable
    '            Dim dtlbTF As New DataTable

    '            _myTrans.Save(TransTFSAVE)
    '            Dim strWhereTF As String = StrWherePicking("FEFO", .Item("Customer_Index").ToString, .Item("ItemStatus_Index").ToString, .Item("Sku_Index").ToString, .Item("PLot").ToString, .Item("SO_Exp_Date").ToString, _
    '                                                        .Item("Shelf_Life").ToString, .Item("Day_picking").ToString, .Item("MinAgeRemain").ToString, .Item("FlagDont_Reverse_LOT").ToString, .Item("LastExp_date").ToString, _
    '                                                        "1", _
    '                                                       _Connection, _myTrans)

    '            If MINExp_Date <> "" Then
    '                strWhereTF &= String.Format(" AND CAST(Exp_Date as DATE) < CAST('{0}' as DATE) ", MINExp_Date)
    '            End If

    '            dtlbCheck = Picking(.Item("Sku_Index").ToString, .Item("Package_Index").ToString, TF_DocumentType_Index, drWithdrawLocationType("Total_Qty"), strWhereTF, TypePicking, _Connection, _myTrans)
    '            _myTrans.Rollback(TransTFSAVE)


    '            If dtlbCheck Is Nothing Then dtlbCheck = New DataTable
    '            For Each drlbCheck As DataRow In dtlbCheck.Rows
    '                If dtlbTF.Rows.Count = 0 Then
    '                    dtlbTF = getVIEW_WithdrawReserveLocationByLocationBalance_Index(drlbCheck("LocationBalance_Index"), _Connection, _myTrans)
    '                Else
    '                    dtlbTF.Merge(getVIEW_WithdrawReserveLocationByLocationBalance_Index(drlbCheck("LocationBalance_Index"), _Connection, _myTrans))
    '                End If
    '            Next




    '            Dim dtLocationBalanceTF As New DataTable

    '            If dtlbCheck Is Nothing OrElse dtlbCheck.Columns.Count = 0 Then
    '            Else

    '                For Each drlbTF As DataRow In dtlbTF.Rows
    '                    If dtLocationBalanceTF Is Nothing OrElse dtLocationBalanceTF.Columns.Count = 0 Then
    '                        dtLocationBalanceTF = Picking(drlbTF("Sku_Index").ToString, drlbTF("Package_Index").ToString, TF_DocumentType_Index, drlbTF("Qty_Bal_Begin"), String.Format(" AND LocationBalance_Index ='{0}'", drlbTF("LocationBalance_Index")), TypePicking, _Connection, _myTrans)
    '                    Else
    '                        Dim dtTmpTF As New DataTable
    '                        dtTmpTF = Picking(drlbTF("Sku_Index").ToString, drlbTF("Package_Index").ToString, TF_DocumentType_Index, drlbTF("Qty_Bal_Begin"), String.Format(" AND LocationBalance_Index ='{0}'", drlbTF("LocationBalance_Index")), TypePicking, _Connection, _myTrans)
    '                        If dtTmpTF IsNot Nothing Then dtLocationBalanceTF.Merge(dtTmpTF)
    '                    End If

    '                Next

    '                ' dtLocationBalanceTF = dtlbCheck

    '                If dtLocationBalanceTF Is Nothing OrElse dtLocationBalanceTF.Columns.Count = 0 Then
    '                Else
    '                    Dim TransferStatus_Index As String = "" 'getTransferStatus_IndexByWithdraw_Index(Withdraw_Index, _Connection, _myTrans)
    '                    If TransferStatus_Index = "" Then
    '                        TransferStatus_Index = CreateTFH(Withdraw_Index, TF_DocumentType_Index, .Item("Customer_Index").ToString, "Auto เติมสินค้า", _Connection, _myTrans)
    '                    End If
    '                    If TransferStatus_Index = "" Then Throw New Exception(" Create TFH Error!!!")

    '                    CreateTFL(TransferStatus_Index, To_Location_Index, dtLocationBalanceTF, _Connection, _myTrans)

    '                    Call New TF_Logic().ConfirmTF(TransferStatus_Index, _Connection, _myTrans)
    '                End If


    '            End If



    '        End With



    '        If IsNotPassTransaction Then _myTrans.Commit()
    '        Return Result
    '    Catch ex As Exception
    '        If IsNotPassTransaction Then _myTrans.Rollback()
    '        Throw ex
    '    Finally
    '        If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
    '    End Try
    'End Function

    Private TF_DocumentType_Index As String = "0010000000500" 'Fix document type

    Public Function AutoTransfer_KSL(ByVal dtWithdrawLocation As DataTable, ByVal Customer_Index As String, ByVal Withdraw_Index As String, ByVal To_Location_Index As String, ByVal To_Location_Alias As String, ByVal TAG_No As String, Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As String
        Dim IsNotPassTransaction As Boolean = False
        Dim TransferStatus_Index As String = ""
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If

            'ต้องสอบถาม User ว่าต้องการให้เกิดใบโอนกี่ใบ
            '1 ใบโอนต่อ 1 SKU ,หรือ 1 ใบเบิกต่อหนึ่ง SKU
            'DBExeQuery(" select TransferStatus_Index from tb_TransferStatus Where Withdraw_Index=@Withdraw_Index and isnull(TransferStatus_Index,'') <> '' ", _Connection, _myTrans)
            'Dim TransferStatus_Index As String = "" 'getTransferStatus_IndexByWithdraw_Index(Withdraw_Index, _Connection, _myTrans)
            'Dim TF_DocumentType_Index As String = "0010000000500" 'Fix document type
            If TransferStatus_Index = "" Then
                TransferStatus_Index = CreateTFH(Withdraw_Index, TF_DocumentType_Index, Customer_Index, "Auto เติมสินค้า", _Connection, _myTrans)
            End If
            If TransferStatus_Index = "" Then Throw New Exception(" Create TFH Error!!!")
            CreateTFL(TransferStatus_Index, To_Location_Index, dtWithdrawLocation, _Connection, _myTrans)
            Call New TF_Logic().ConfirmTF(TransferStatus_Index, _Connection, _myTrans)
            'update tag no to Location pickface
            'TransferStatusLocation status = 1 : รอโอนเติมสินค้า()
            Dim tSql As String = ""
            If TAG_No.ToString.Trim.Length > 0 Then
                tSql = String.Format(" update tb_TransferStatusLocation" & _
                                     " set TAG_No ='{0}' ,Status_Fullfill = 1" & _
                                     " where TransferStatus_Index = '{1}' and Tag_No = '{2}'", To_Location_Alias, TransferStatus_Index, TAG_No)
                DBExeNonQuery(tSql, _Connection, _myTrans)

            End If

            'Set Status and Ref no.
            'TransferStatus status = 5 : รอโอนเติมสินค้า()
            tSql = String.Format(" update tb_TransferStatus " & _
                                 " set Status_Fullfill = 1,Ref_No1 =(select top 1 withdraw_no from tb_withdraw where Withdraw_Index ='{0}')" & _
                                 " where TransferStatus_Index = '{1}'", Withdraw_Index, TransferStatus_Index)
            DBExeNonQuery(tSql, _Connection, _myTrans)
            tSql = String.Format(" update tb_LocationBalance " & _
                                    " set TAG_No = (select top 1 tb_TransferStatusLocation.Tag_No from tb_TransferStatusLocation where tb_TransferStatusLocation.To_LocationBalance_Index = tb_LocationBalance.LocationBalance_Index  and  tb_TransferStatusLocation.TransferStatus_Index = '{0}')" & _
                                    " WHERE LocationBalance_Index = (select top 1 tb_TransferStatusLocation.To_LocationBalance_Index from tb_TransferStatusLocation where tb_TransferStatusLocation.To_LocationBalance_Index = tb_LocationBalance.LocationBalance_Index  and  tb_TransferStatusLocation.TransferStatus_Index = '{0}')", TransferStatus_Index)
            DBExeNonQuery(tSql, _Connection, _myTrans)

            If IsNotPassTransaction Then _myTrans.Commit()
            Return TransferStatus_Index

        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function


    Public Function getVIEW_WithdrawReserveLocationByLocation_Index(ByVal Location_Index As String, _
            Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = Location_Index


            Result = DBExeQuery(" select * from VIEW_WithdrawReserveLocation Where Location_Index=@Location_Index AND Qty_Bal > 0  ", _Connection, _myTrans)


            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getVIEW_WithdrawReserveLocationByLocationBalance_Index(ByVal LocationBalance_Index As String, _
                Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = LocationBalance_Index


            Result = DBExeQuery(" select * from VIEW_WithdrawReserveLocation Where LocationBalance_Index=@LocationBalance_Index AND Qty_Bal > 0  ", _Connection, _myTrans)


            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getEditSOItem(ByVal SalesOrderItem_Index As String, _
            Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As DataTable
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As New DataTable
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar).Value = SalesOrderItem_Index


            Result = DBExeQuery(" select Total_Qty,isnull(Total_Qty_Withdraw,0) as Total_Qty_Withdraw,isnull(Org_Total_Qty,0) as Org_Total_Qty from tb_salesOrderitem WHERE SalesOrderItem_Index=@SalesOrderItem_Index  ", _Connection, _myTrans)


            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function UpdateEditSOItem(ByVal SalesOrderItem_Index As String, ByVal Total_Qty As Decimal, _
               Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Integer
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As Integer = 0
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrderItem_Index", SqlDbType.VarChar).Value = SalesOrderItem_Index
                .Add("@Total_Qty", SqlDbType.Decimal).Value = Total_Qty
            End With


            Result = DBExeNonQuery(" Update tb_SalesOrderItem SET  Total_Qty=@Total_Qty  WHERE SalesOrderItem_Index=@SalesOrderItem_Index AND  @Total_Qty >= isnull(Total_Qty_Withdraw,0) AND (@Total_Qty <= isnull(Org_Total_Qty,0) or isnull(Org_Total_Qty,0) =0  ) ", _Connection, _myTrans)




            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function

    Public Function getWithdrawItemCount(ByVal Withdraw_Index As String, _
                Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Integer
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As Integer = 0
        Try

            If _Connection Is Nothing Then
                _Connection = Connection
                With SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = Withdraw_Index


            Result = DBExeQuery_Scalar(" select Count(WithdrawItem_Index) from tb_Withdrawitem Where Withdraw_Index=@Withdraw_Index ", _Connection, _myTrans)



            If IsNotPassTransaction Then _myTrans.Commit()
            Return Result
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then SQLServerCommand.Connection.Close()
        End Try
    End Function


    'Public Function GetSuggestLocationTAG(ByVal pTagNo As String, ByVal pSkuIndex As String, ByVal pProductTypeIndex As String, ByVal pItemStatusIndex As String, ByVal pDocumentTypeIndex As String, ByVal pCustomerIndex As String, ByVal pTotalQty As Decimal, ByVal pTotalWeight As Decimal, ByVal pTotalVolume As Decimal, Optional ByVal pAddition_Condition As String = "") As String
    '    Try
    '        Dim SuggestLocation As DataTable = DBExeQuery(String.Format("exec spSuggest_Location '{0}','{1}','{2}','{3}','{4}',{5},{6},{7},{8}", pSkuIndex, pProductTypeIndex, pItemStatusIndex, pDocumentTypeIndex, pCustomerIndex, pTotalQty, pTotalWeight, pTotalVolume, pAddition_Condition), eCommandType.Text)
    '        Dim SuggestLocationIndex As String = String.Empty
    '        Dim SuggestLocationAlias As String = String.Empty

    '        If SuggestLocation IsNot Nothing AndAlso SuggestLocation.Rows.Count > 0 Then
    '            SuggestLocationIndex = SuggestLocation.Rows.Item(0).Item("Location_Index").ToString
    '            SuggestLocationAlias = SuggestLocation.Rows.Item(0).Item("Location_Alias").ToString
    '        End If

    '        Dim Sql As New System.Text.StringBuilder
    '        With Sql
    '            .Append(" UPDATE tb_TAG ")
    '            .Append(" SET Suggest_Location_Index = @Suggest_Location_Index ")
    '            .Append(" WHERE TAG_Status = 1 ")
    '            .Append(" AND TAG_No = @TAG_No ")
    '        End With

    '        With SQLServerCommand.Parameters
    '            .Clear()

    '            .Add("@Suggest_Location_Index", SqlDbType.VarChar).Value = SuggestLocationIndex
    '            .Add("@TAG_No", SqlDbType.VarChar).Value = pTagNo
    '        End With

    '        DBExeNonQuery(Sql.ToString)

    '        Return SuggestLocationAlias

    '    Catch Ex As Exception
    '        Throw Ex
    '    End Try
    'End Function

End Class



