Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports BarcodeNETWorkShop

Public Class ml_TNP : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

    '*** Property Readonly
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property

    Private barcodeFont As New FontDialog
    Private objBarcode As New BarcodeNETWorkShop.BarcodeNETWindows
    Public Sub GenBarcode(ByVal pstrBarcode As String)
        Try
            objBarcode.BarcodeText = pstrBarcode
            objBarcode.BarcodeType = BARCODE_TYPE.EAN128B     ' set barcode type
            objBarcode.BarcodeColor = Color.Black                 ' set barcode color
            objBarcode.TextColor = Color.Black                   ' set text color
            objBarcode.RotateAngle = ROTATE_ANGLE.R0            ' set barcode rotate
            objBarcode.TextFont = barcodeFont.Font              ' set barcode font
            objBarcode.ShowBarcodeText = False 'For Apex
            SaveBarcode(pstrBarcode)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub SaveBarcode(ByVal pstrBarcode As String)
        Try
            objBarcode.SaveToFile(Application.StartupPath & "\" & pstrBarcode & ".bmp", FILE_FORMAT.BMP)
            'objBarcode.SaveToFile(pstrBarcode + ".jpg", FILE_FORMAT.JPG)
            'objBarcode.SaveToFile(pstrBarcode + ".png", FILE_FORMAT.PNG)
            'objBarcode.SaveToFile(pstrBarcode + ".gif", FILE_FORMAT.GIF)
            'MessageBox.Show("Success save to current directory")
        Catch ex As Exception
            MessageBox.Show("Error saving files")
        End Try
    End Sub

    Public Sub getProcess_Reference(ByVal Main_Process_Id As String)
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM config_Process_Reference "
            strSQL &= " WHERE Main_Process_Id ='" & Main_Process_Id & "' "
            strSQL &= " AND IsUse = '1' "

            SetSQLString = strSQL & " Order by Seq "

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getQtyMaxRaio_Receive(ByVal Sku_Index As String, ByVal Total_Qty As Double)
        Try
            'dong 2015-04-10
            'Auto คำนวณหน่วยรับเข้ามากที่สุดในการเติมสินค้า
            Dim strSQLt As String = ""
            strSQLt &= " Declare @Total_Qty float"
            strSQLt &= " set @Total_Qty = " & Total_Qty

            strSQLt &= " select Top 1  X1.Package_Index,R.Ratio"
            strSQLt &= ",X1.Std_Package_Index,X1.UnitWeight_Index,X1.Unit_Volume"
            strSQLt &= " ,@Total_Qty as Total_Qty"
            strSQLt &= " ,(@Total_Qty/R.Ratio) as Normal"
            strSQLt &= " ,FLOOR((@Total_Qty/R.Ratio)) QFLOOR"
            strSQLt &= " ,CEILING((@Total_Qty/R.Ratio)) QCEILING"
            strSQLt &= " ,CEILING((@Total_Qty/R.Ratio)) * R.Ratio as QFullFill"

            strSQLt &= " from("
            strSQLt &= "        select OI.Sku_Index,S.Package_Index as Std_Package_Index,OI.Package_Index,S.UnitWeight_Index,S.Unit_Volume"
            strSQLt &= "        from tb_OrderItem OI "
            strSQLt &= "            inner join tb_Order O ON O.Order_Index = OI.Order_Index"
            strSQLt &= "            inner join ms_SKU S ON S.Sku_Index = OI.Sku_Index"
            strSQLt &= "        where O.Status in (2)"
            strSQLt &= "        Group by OI.Sku_Index,S.Package_Index,OI.Package_Index,S.UnitWeight_Index,S.Unit_Volume"
            strSQLt &= " )x1 left outer join ms_SKURatio R ON R.Package_Index = X1.Package_Index"
            strSQLt &= " WHERE X1.Sku_Index = '" & Sku_Index & "'"
            strSQLt &= " Order by R.Ratio desc"

            connectDB()
            SetSQLString = strSQLt
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub GetPOItem(ByVal PurchaseOrder_No As String, ByVal chkPM As Boolean)

        Dim strSQL As String = ""

        Try

            strSQL = " select P.PurchaseOrder_No as DocumentPlan_No,P.PurchaseOrder_Index as DocumentPlan_Index,POI.PurchaseOrderItem_Index as DocumentPlanItem_Index,9 as Process_Id"
            strSQL &= " 	,S.Sku_Index,S.Barcode1,S.Sku_Id,S.Str1 Product_Name_th,S.Str2 Product_Name_eng,PC.Description as Package,POI.Package_Index,ST.Ratio"
            strSQL &= " 	,POI.Qty - POI.Received_Qty as Qty_Remain,POI.Received_Qty-POI.Total_Received_Qty as Total_Qty_Remain"
            strSQL &= " 	,0.0 as Qty,'' as Plot,'' as ItemStatus_Index,'' as ItemStatus"
            strSQL &= " from tb_PurchaseOrder P"
            strSQL &= " 	inner join tb_PurchaseOrderItem POI ON P.PurchaseOrder_Index = POI.PurchaseOrder_Index"
            strSQL &= " 	inner join ms_SKU S ON S.Sku_Index = POI.Sku_Index"
            strSQL &= " 	inner join ms_Package PC ON PC.Package_Index = POI.Package_Index"
            strSQL &= " 	inner join ms_SKURatio ST ON ST.Package_Index = POI.Package_Index"
            strSQL &= " where P.PurchaseOrder_No = '" & PurchaseOrder_No & "'	"
            If chkPM = True Then
                strSQL &= " and POI.Str10 = 'PM' "
            Else
                strSQL &= " and POI.Str10 <> 'PM' "
            End If
            strSQL &= " and (POI.Qty - POI.Received_Qty) > 0 and P.Status = 2 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetPOACKINGItem(ByVal Packing_No As String)

        Dim strSQL As String = ""

        Try

            strSQL = "  select P.Packing_No as DocumentPlan_No,P.Packing_Index as DocumentPlan_Index,PI.PackingItem_Index as DocumentPlanItem_Index,7 as Process_Id 	"
            strSQL &= " 	,S.Sku_Index,S.Barcode1,S.Sku_Id,S.Str1 Product_Name_th,S.Str2 Product_Name_eng,PC.Description as Package,PI.Package_Index,ST.Ratio 	"
            strSQL &= " 	 ,PI.Qty - PI.Qty_Receive as Qty_Remain,0.0 as Qty,'' as Plot,'' as ItemStatus_Index,'' as ItemStatus "
            strSQL &= " 	from tb_Packing P 	"
            strSQL &= " 		inner join tb_PackingItem PI ON PI.Packing_Index = P.Packing_Index "
            strSQL &= " 		inner join ms_SKU S ON S.Sku_Index = PI.Sku_Index 	"
            strSQL &= " 		inner join ms_Package PC ON PC.Package_Index = PI.Package_Index 	"
            strSQL &= " 		inner join ms_SKURatio ST ON ST.Package_Index = PI.Package_Index "
            strSQL &= " 	where P.Packing_No =  '" & Packing_No & "' and (P.Status in (4,5)) and (PI.Qty - PI.Qty_Receive ) > 0 and PI.isFG = 1"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetPOACKING(ByVal Packing_No As String)

        Dim strSQL As String = ""

        Try

            strSQL = " select P.Packing_No as DocumentPlan_No,P.Packing_Index as DocumentPlan_Index,P.Packing_Index as DocumentPlanItem_Index,7 as Process_Id"
            strSQL &= " 	,S.Sku_Index,S.Barcode1,S.Sku_Id,S.Str1 Product_Name_th,S.Str2 Product_Name_eng,PC.Description as Package,P.Package_Index,ST.Ratio"
            strSQL &= " 	,P.Qty_Product - P.Qty_Receive as Qty_Remain"
            strSQL &= " 	,0.0 as Qty,'' as Plot,'' as ItemStatus_Index,'' as ItemStatus"
            strSQL &= " from tb_Packing P"
            strSQL &= " 	inner join ms_SKU S ON S.Sku_Index = P.Sku_Index"
            strSQL &= " 	inner join ms_Package PC ON PC.Package_Index = P.Package_Index"
            strSQL &= " 	inner join ms_SKURatio ST ON ST.Package_Index = P.Package_Index"
            strSQL &= " where P.Packing_No = '" & Packing_No & "'	"
            strSQL &= " and (P.Qty_Product - P.Qty_Receive ) > 0 and (P.Status in (4,5)) "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function fnFixbugQtyReceive_Po(ByVal pobjItemCollection As List(Of WMS_STD_INB_Receive_Datalayer.tb_OrderItem)) As String
        Dim strSQL As String = ""
        connectDB()
        Try
            For Each _objItem As WMS_STD_INB_Receive_Datalayer.tb_OrderItem In pobjItemCollection
                Select Case _objItem.Plan_Process
                    Case 9
                        strSQL = " UPDATE tb_PurchaseOrderItem SET "
                        strSQL &= " Received_Qty = Total_Received_Qty / (select top 1 R.Ratio from ms_Skuratio R where R.Package_Index = tb_PurchaseOrderItem.Package_Index)"
                        strSQL &= " WHERE PurchaseOrderItem_Index= @PurchaseOrderItem_Index"

                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                        End With
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                End Select
            Next


            Return "PASS"

        Catch ex As Exception
            Return ex.Message.ToString
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub UpdateSO_Cutoff(ByVal pstrSalesOrder_Index As String)
        Try

            Dim xx As String = ""
            Dim strSql As String


            '---item
            SetSQLString = " SELECT * FROM tb_SalesOrder WHERE SalesOrder_Index='" & pstrSalesOrder_Index & "'"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Select Case _dataTable.Rows(0).Item("Status")
                Case 6
                    '---item
                    'ให้จำนวนแจ้งเบิกสินค้าเท่ากับจำนวนเบิกสินค้าจริง
                    strSql = " UPDATE tb_salesorderitem SET "
                    strSql &= " Qty  =  Qty_Withdraw "
                    strSql &= " WHERE SalesOrder_Index='" & pstrSalesOrder_Index & "'"
                    SetSQLString = strSql
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                    'ลบรายการที่ไม่มีการเบิกสินค้า
                    strSql = " DELETE tb_salesorderitem "
                    strSql &= " WHERE SalesOrder_Index='" & pstrSalesOrder_Index & "' and Qty_Withdraw = 0 "
                    SetSQLString = strSql
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
            End Select

            '--- head
            strSql = " UPDATE tb_salesorder SET "
            strSql &= " Status = 5 ,update_date = getdate() ,update_by = '" & WV_UserName & "'"
            strSql &= " WHERE SalesOrder_Index='" & pstrSalesOrder_Index & "'"

            SetSQLString = strSql
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()



        Catch ex As Exception
            Throw ex
        Finally

            disconnectDB()
        End Try
    End Sub

    Public Sub getCheckPick_Pack(ByVal Withdraw_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = "     Select count(*) as cnt "
            strSQL &= "     from tb_WithdrawItem"
            strSQL &= "     where (Plan_Process = 10) AND Withdraw_Index = '" & Withdraw_Index & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            If _dataTable.Rows(0)(0) = 0 Then
                strSQL = "  select(0)as WQTY,(0)as PQTY"
            Else
                strSQL = "  select "
                strSQL &= " ("
                strSQL &= "     Select isnull(SUM(Total_Qty), 0)"
                strSQL &= "     from tb_WithdrawItem"
                strSQL &= "     where (Plan_Process = 10) AND Withdraw_Index = '" & Withdraw_Index & "'"
                strSQL &= "  )as WQTY,("
                strSQL &= "     select isnull(SUM(Qty_Pack),0)as Qty_Pack"
                strSQL &= "     from VIEW_SalesOrderPackingItem_TechnicPet P "
                strSQL &= "     where P.Status_Print <> -1 and P.SalesOrder_Index in (select DocumentPlan_Index from  tb_WithdrawItem where Plan_Process = 10 AND Withdraw_Index = '" & Withdraw_Index & "')"
                strSQL &= " )as PQTY"

            End If
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getCheckPick_Pack_SO(ByVal SalesOrder_No As String)
        Dim strSQL As String = ""
        Try
            'เพิ่ม Where tb_Withdraw.status <> -1 29/01/2558
            strSQL = "  select "
            strSQL &= " ("
            strSQL &= "     Select isnull(SUM(Total_Qty), 0)"
            strSQL &= "     from tb_WithdrawItem Inner Join tb_Withdraw On tb_WithdrawItem.Withdraw_Index = tb_Withdraw.Withdraw_Index "
            strSQL &= "     where (Plan_Process = 10) AND tb_Withdraw.status <> -1 AND DocumentPlan_No = '" & SalesOrder_No & "'"
            strSQL &= "  )as WQTY,("
            strSQL &= "     select isnull(SUM(Qty_Pack),0)as Qty_Pack"
            strSQL &= "     from VIEW_SalesOrderPackingItem_TechnicPet P "
            strSQL &= "     where P.Status_Print <> -1 and P.SalesOrder_No = '" & SalesOrder_No & "'"
            strSQL &= " )as PQTY"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub close_packing(ByVal Packing_Index As String)
        Try
            Dim strSQL As String = ""
            '--- head
            strSQL = " UPDATE tb_Packing SET "
            strSQL &= " Status = 3 ,update_date = getdate() ,update_by = '" & WV_UserName & "'"
            strSQL &= " WHERE Packing_Index='" & Packing_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub close_PurchaseOrder(ByVal PurchaseOrder_Index As String)
        Try
            Dim strSQL As String = ""
            '--- head
            strSQL = " UPDATE tb_PurchaseOrder SET "
            strSQL &= " Status = 3 ,update_date = getdate() ,update_by = '" & WV_UserName & "'"
            strSQL &= " WHERE PurchaseOrder_Index='" & PurchaseOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub



    Public Sub getLocationType(ByVal where As String)
        Dim strSQL As String = ""
        Try
            'เพิ่ม Where tb_Withdraw.status <> -1 29/01/2558
            strSQL = "  select *"
            strSQL &= " FROM ms_LocationType"
            SetSQLString = strSQL & where
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function ChkPickingItem(ByVal Withdraw_Index As String) As Boolean
        Dim strSQL As String
        Try
            Dim oDT As DataTable
            strSQL = "  SELECT  * "
            strSQL &= " FROM    VIEW_WithDrawAssetItemLocation "
            strSQL &= " WHERE   Withdraw_Index ='" & Withdraw_Index & "'"
            strSQL &= " and Status not in (-3) "
            oDT = DBExeQuery(strSQL)

            If oDT.Rows.Count > 0 Then
                Return False
            Else
                Return True
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Sub update_withdraw_Status(ByVal Withdraw_Index As String, ByVal Staus As Integer)
        Try
            Dim strSQL As String = ""
            '--- head
            strSQL = " UPDATE tb_Withdraw SET "
            strSQL &= " update_date = getdate() ,update_by = '" & WV_UserName & "',Status = " & Staus
            strSQL &= " WHERE Withdraw_Index='" & Withdraw_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


End Class
