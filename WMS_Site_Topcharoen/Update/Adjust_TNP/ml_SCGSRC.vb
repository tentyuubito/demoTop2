Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_Master_Datalayer

Public Class ml_SCGSRC : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

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

    Public Function UPDATE_ORDER_SAPSTATUS(ByVal pOrder_Index As String, ByVal EDI_Flag_Export As Integer) As String
        Dim strSQL As String = ""
        Try
            strSQL = " UPDATE tb_Order" & _
                      " SET EDI_Flag_Export =" & EDI_Flag_Export & _
                      " WHERE  Order_Index='" & pOrder_Index & "'"
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
            Return "PASS"

        Catch ex As Exception
            Return "FAIL"
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function


    Public Function UPDATE_PURCHASEORDER_STATUS(ByVal pPurchaseOrder_Index As String, ByVal iStatus As Integer) As String
        Dim strSQL As String = ""
        Try
            strSQL = " UPDATE tb_PurchaseOrder" & _
                      " SET Status =" & iStatus & _
                      " WHERE PurchaseOrder_Index='" & pPurchaseOrder_Index & "'"
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
            Return "PASS"

        Catch ex As Exception
            Return "FAIL"
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Function

    Public Function getReportSalsOrder_Req(Optional ByVal strWhere As String = "") As DataSet
        Dim strSQL As String
        Dim DS As New DataSet
        Try

            strSQL = " EXEC dbo.spReportSaleOrder_Req '" & strWhere & "'"
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            DS.Tables.Add(_dataTable)

            Return DS

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Sub fnUPDATE_CUSTOMER_SHIPPING_LOCATION(ByVal TransportManifestItem_Index As String, ByVal SalesOrder_Index As String, ByVal Customer_Shipping_Location_Index As String)
        Try
            connectDB()
            SetSQLString = " UPDATE tb_SalesOrder SET Customer_Shipping_Location_Index = '" & Customer_Shipping_Location_Index & "' WHERE SalesOrder_Index = '" & SalesOrder_Index & "'"
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            SetSQLString = " UPDATE tb_TransportManifestItem SET Customer_Shipping_Location_Index = '" & Customer_Shipping_Location_Index & "' WHERE TransportManifestItem_Index = '" & TransportManifestItem_Index & "'"
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getSOViewSearch(ByVal WhereString As String)
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            strSQL = "select * from VIEW_SOView "
            strSQL &= " WHERE 1=1 "

            SetSQLString = strSQL & WhereString & " Order by SalesOrder_No desc"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Function chkConfirmWithdraw(ByVal Withdraw_Index As String) As Boolean
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            strSQL = "select count(*) as ContW from tb_WithdrawItemLocation "
            strSQL &= " WHERE Status = -9 AND Withdraw_Index = '" & Withdraw_Index & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows(0)(0).ToString = "0" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function chkConfirmWithdraw_TransportManifest(ByVal Withdraw_Index As String) As Boolean
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            strSQL = "select count(*)"
            strSQL &= " from tb_WithdrawItem WI left outer join"
            strSQL &= " 	tb_SalesOrder SO ON SO.SalesOrder_Index = WI.DocumentPlan_Index"
            strSQL &= "             where (WI.Plan_Process = 10)"
            strSQL &= " 	and SO.TransportManifest_Index in (select TransportManifest_Index from tb_TransportManifest where Status <> 5)"
            strSQL &= " 	and WI.Withdraw_Index = '" & Withdraw_Index & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows(0)(0).ToString <> "0" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    'Public Sub getVIEW_RPT_DailyReceive(ByVal WhereString As String)
    '    Dim strSQL As String = ""
    '    'Dim strWhere As String = ""
    '    Try

    '        strSQL = "select * from VIEW_RPT_DailyReceive "
    '        strSQL &= " WHERE 1=1 "

    '        SetSQLString = strSQL & WhereString & " Order by DocumentPlan_No ,Sku_id"
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub

    'Public Sub getVIEW_PO_ReportStatus(ByVal WhereString As String)
    '    Dim strSQL As String = ""
    '    Try
    '        strSQL = " select * from VIEW_PO_ReportStatus "
    '        strSQL &= " WHERE 1=1 "

    '        SetSQLString = strSQL & WhereString & " Order by PoNO "
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub
    Public Sub Search_Lock()

        Dim strSQL As String = ""
        Try

            strSQL = " SELECT     lock"
            strSQL &= " FROM  ms_Location"
            strSQL &= " GROUP BY lock"
            SetSQLString = strSQL & " Order by lock "
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub Search_WithDraw_TM(ByVal Ref_No1 As String)

        Dim strSQL As String = ""
        Try

            strSQL = " SELECT     *"
            strSQL &= " FROM  tb_WithDraw"
            strSQL &= " WHERE  Ref_No1 = '" & Ref_No1 & "' AND Status not in (-1,2)"
            strSQL &= " Order by WithDraw_Index"
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


    Public Function isCheckSO_ERP(ByVal _ERP_Location As String, ByVal _Sku_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_LocationBalance where   ERP_Location = '" & _ERP_Location & "' AND Sku_Index='" & _Sku_Index & "'  AND (Qty_Bal - ReserveQty) > 0"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows(0)(0) = 0 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    'Public Function isCheckSO_ERP(ByVal _ERP_Location As String, ByVal _Sku_Index As String) As Boolean
    '    Dim strSQL As String
    '    Try
    '        strSQL = " select count(*) from tb_WithDraw where   ERP_Location = '" & _ERP_Location & "' AND Sku_Index='" & _Sku_Index & "'  AND (Qty_Bal - ReserveQty) > 0"
    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable

    '        If _dataTable.Rows(0)(0) = 0 Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Function


    Public Sub GetPOItem(ByVal PurchaseOrder_No As String)

        Dim strSQL As String = ""

        Try

            strSQL = " SELECT     tb_PurchaseOrder.PurchaseOrder_No, tb_PurchaseOrder.PurchaseOrder_Index, tb_PurchaseOrderItem.PurchaseOrderItem_Index, "
            strSQL &= "           ms_SKU.Sku_Id, ms_SKU.Sku_Index, ms_SKU.Str1, ms_Package.Package_Index, ms_Package.Description AS Package_DES, "
            strSQL &= "            (tb_PurchaseOrderItem.Qty - tb_PurchaseOrderItem.Received_Qty )AS Total_Qty , tb_PurchaseOrderItem.Weight, tb_PurchaseOrderItem.Volume, tb_PurchaseOrder.Status, "
            strSQL &= "           tb_PurchaseOrderItem.UnitPrice,tb_PurchaseOrderItem.Amount,"
            strSQL &= "           tb_PurchaseOrder.Supplier_Index"

            strSQL &= " FROM         ms_Package RIGHT OUTER JOIN "
            strSQL &= "           tb_PurchaseOrderItem ON ms_Package.Package_Index = tb_PurchaseOrderItem.Package_Index LEFT OUTER JOIN "
            strSQL &= "           ms_SKU ON tb_PurchaseOrderItem.Sku_Index = ms_SKU.Sku_Index RIGHT OUTER JOIN"
            strSQL &= "           tb_PurchaseOrder ON tb_PurchaseOrderItem.PurchaseOrder_Index = tb_PurchaseOrder.PurchaseOrder_Index "
            strSQL &= " WHERE     tb_PurchaseOrder.Status = 2  and tb_PurchaseOrder.StatusReceive <> - 2 and tb_PurchaseOrder.PurchaseOrder_No = '" & PurchaseOrder_No & "'"
            strSQL &= "        and tb_PurchaseOrderItem.Str8 <> 'X' and (tb_PurchaseOrderItem.Qty - tb_PurchaseOrderItem.Received_Qty ) > 0 "
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


    Public Function chkConfirmWithdraw_TM(ByVal Ref_No1 As String) As Boolean
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            strSQL = "select count(*) as ContW from tb_WithdrawItemLocation "
            strSQL &= " WHERE Status = -9 AND Withdraw_Index in (select Withdraw_Index from tb_Withdraw where Ref_No1 = '" & Ref_No1 & "') "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows(0)(0).ToString = "0" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Sub chkConfirmWithdrawInSTATE(ByVal Withdraw_Index As String)
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            'strSQL = " select *   FROM         dbo.tb_WithdrawItemLocation INNER JOIN "
            'strSQL &= " dbo.ms_Location ON dbo.tb_WithdrawItemLocation.Location_Index = dbo.ms_Location.Location_Index INNER JOIN"
            'strSQL &= " dbo.tb_Withdraw ON dbo.tb_WithdrawItemLocation.Withdraw_Index = dbo.tb_Withdraw.Withdraw_Index"
            'strSQL &= " WHERE 1=1 and  tb_Withdraw.Withdraw_Index = '" & Withdraw_Index & "' and ms_Location.Location_Alias <> 'STGI01' "

            strSQL = " select * from tb_TransferStatus"
            strSQL &= " where 1=1 and tb_TransferStatus.status  <> -1 and Ref_No2 in ("
            strSQL &= " select tb_Withdraw.Withdraw_No +'-'+convert(varchar(2), tb_WithdrawItemLocation.Seq_TimeStamp) FROM         dbo.tb_WithdrawItemLocation INNER JOIN "
            strSQL &= " dbo.ms_Location ON dbo.tb_WithdrawItemLocation.Location_Index = dbo.ms_Location.Location_Index INNER JOIN"
            strSQL &= " dbo.tb_Withdraw ON dbo.tb_WithdrawItemLocation.Withdraw_Index = dbo.tb_Withdraw.Withdraw_Index"
            strSQL &= " WHERE 1=1 and tb_Withdraw.status <> -1 and tb_Withdraw.Withdraw_Index = '" & Withdraw_Index & "' and ms_Location.Location_Alias <> 'STGI01' "
            strSQL &= " )"

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
    Public Sub chkTransferInWithdraw_No(ByVal strTransferStatus_Index As String)
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try


            'Load
            Dim objTran As New TransferStatusTransaction
            objTran.GetTransferAssetLocation(strTransferStatus_Index)
            Dim odtTransfer As New DataTable
            odtTransfer = objTran.DataTable

            connectDB()
            Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
            SQLServerCommand.Transaction = myTrans
            Try
                For Each drRowTransfer As DataRow In odtTransfer.Rows
                    ' *** Check ?? : Move All Qty of Tag or Move Some Qty from TAG ***

                    strSQL = "   SELECT  * "
                    strSQL &= "   FROM  tb_LocationBalance   "
                    strSQL &= "  WHERE  LocationBalance_Index ='" & drRowTransfer("From_LocationBalance_Index").ToString & "'"
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    If DS.Tables.Contains("LB") Then DS.Tables("LB").Clear()
                    DataAdapter.Fill(DS, "LB") 'tb_LocationBalance
                    'Need To Have Rows
                    Dim odrLocationBalance As DataRow = DS.Tables("LB").Rows(0)
                    'ถ้าเป็น Stgi01 แล้วไม่ต้อง Update
                    If odrLocationBalance("Location_index").ToString = "0010000003458" Then
                        Continue For
                    End If
                    Dim tmpPerWeight As Double = CDbl(odrLocationBalance("Weight_Bal_Begin")) / CDbl(odrLocationBalance("Qty_Bal_Begin"))
                    Dim tmpPerVolume As Double = CDbl(odrLocationBalance("Volume_Bal_Begin")) / CDbl(odrLocationBalance("Qty_Bal_Begin"))
                    Dim tmpPerItem_Qty As Double = CDbl(odrLocationBalance("Qty_Item_Begin")) / CDbl(odrLocationBalance("Qty_Bal_Begin"))
                    Dim tmpPerPrice As Double = CDbl(odrLocationBalance("OrderItem_Price_Begin")) / CDbl(odrLocationBalance("Qty_Bal_Begin"))

                    Dim dblQtyMove As Double = drRowTransfer("Total_Qty").ToString 'Me.Total_Qty
                    Dim dblWeightMove As Double = (dblQtyMove * tmpPerWeight)
                    Dim dblVolumeMove As Double = (dblQtyMove * tmpPerVolume)
                    Dim dblPriceMove As Double = (dblQtyMove * tmpPerPrice)
                    Dim dblItem_QtyMove As Double = (dblQtyMove * tmpPerItem_Qty)

                    '----------------------------------------------------------------------------------------
                    strSQL = " "
                    strSQL &= " Update tb_LocationBalance "
                    strSQL &= " SET ReserveQty = ReserveQty - " & dblQtyMove & ""
                    strSQL &= " ,ReserveWeight = ReserveWeight - " & dblWeightMove & ""
                    strSQL &= " ,ReserveVolume = ReserveVolume - " & dblVolumeMove & ""
                    strSQL &= " ,ReserveQty_Item = ReserveQty_Item - " & dblItem_QtyMove & ""
                    strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price - " & dblPriceMove & ""
                    strSQL &= " where LocationBalance_Index='" & drRowTransfer("From_LocationBalance_Index").ToString & "'"
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    Dim objCalBalance As New CalculateBalance
                    objCalBalance.setQty_Recieve_Package(Connection, myTrans, drRowTransfer("From_LocationBalance_Index").ToString)


                    strSQL = " "
                    strSQL &= " Update tb_LocationBalance "
                    strSQL &= " SET ReserveQty = ReserveQty + " & dblQtyMove & ""
                    strSQL &= " ,ReserveWeight = ReserveWeight + " & dblWeightMove & ""
                    strSQL &= " ,ReserveVolume = ReserveVolume + " & dblVolumeMove & ""
                    strSQL &= " ,ReserveQty_Item = ReserveQty_Item + " & dblItem_QtyMove & ""
                    strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price + " & dblPriceMove & ""
                    strSQL &= " where LocationBalance_Index='" & drRowTransfer("To_LocationBalance_Index").ToString & "'"
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    objCalBalance.setQty_Recieve_Package(Connection, myTrans, drRowTransfer("To_LocationBalance_Index").ToString)
                    objCalBalance = Nothing

                    '----------------------------------------------------------------------------------------

                    strSQL = " UPDATE ms_Location "
                    strSQL &= "  set ms_Location.Current_Qty = (select isnull(sum(LB.Qty_Bal),0)    from tb_LocationBalance LB where ms_Location.Location_Index = LB.Location_Index  and isnull((LB.Qty_Bal),0)   > 0)"
                    strSQL &= "      ,ms_Location.Current_Weight = (select isnull(sum(LB.Weight_Bal),0) from tb_LocationBalance LB where ms_Location.Location_Index = LB.Location_Index  and isnull((LB.Weight_Bal),0)   > 0 )"
                    strSQL &= "      ,ms_Location.Current_Volume = (select isnull(sum(LB.Volume_Bal),0) from tb_LocationBalance LB where ms_Location.Location_Index = LB.Location_Index  and isnull((LB.Volume_Bal),0)   > 0 )"
                    strSQL &= " WHERE  Location_Index ='" & drRowTransfer("From_Location_Index").ToString & "' "
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    strSQL = " UPDATE ms_Location "
                    strSQL &= "  set ms_Location.Current_Qty = (select isnull(sum(LB.Qty_Bal),0)    from tb_LocationBalance LB where ms_Location.Location_Index = LB.Location_Index  and isnull((LB.Qty_Bal),0)   > 0)"
                    strSQL &= "      ,ms_Location.Current_Weight = (select isnull(sum(LB.Weight_Bal),0) from tb_LocationBalance LB where ms_Location.Location_Index = LB.Location_Index  and isnull((LB.Weight_Bal),0)   > 0 )"
                    strSQL &= "      ,ms_Location.Current_Volume = (select isnull(sum(LB.Volume_Bal),0) from tb_LocationBalance LB where ms_Location.Location_Index = LB.Location_Index  and isnull((LB.Volume_Bal),0)   > 0 )"
                    strSQL &= " WHERE  Location_Index ='" & drRowTransfer("To_Location_Index").ToString & "' "
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    '----------------------------------------------------------------------------------------

                    'ถ้า Tag มีการเบิกแล้วให้ Update รายการเบิกนั้นที่ To_Location_Index,New_ItemStatus_Index
                    'ย้าย Withdrawitem
                    strSQL = " UPDATE tb_WithdrawItemLocation" & _
                            " SET " & _
                            "       LocationBalance_Index = @LocationBalance_Index," & _
                            "       Location_Index = @Location_Index," & _
                            "       Tag_No = (select top 1 Tag_No from tb_LocationBalance where LocationBalance_Index = @LocationBalance_Index )," & _
                            "       Tag_Index = (select top 1 TAG_Index from tb_LocationBalance where LocationBalance_Index = @LocationBalance_Index )," & _
                            "       update_by=@update_by," & _
                            "       update_date=getdate()," & _
                            "       update_branch=@update_branch " & _
                            " WHERE  WithdrawItemLocation_Index = @WithdrawItemLocation_Index "
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@WithdrawItemLocation_Index", SqlDbType.VarChar, 13).Value = drRowTransfer("Str1").ToString
                        .Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = drRowTransfer("To_LocationBalance_Index").ToString
                        '.Add("@Tag_Index", SqlDbType.VarChar, 50).Value = drRowTransfer("Tag_Index").ToString
                        '.Add("@Tag_No_New", SqlDbType.VarChar, 50).Value = drRowTransfer("Tag_No").ToString
                        .Add("@Location_Index", SqlDbType.VarChar, 50).Value = drRowTransfer("To_Location_Index").ToString
                        .Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Add("@update_branch", SqlDbType.Int, 10).Value = WV_Branch_ID
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    '----------------------------------------------------------------------------------------

                Next

                myTrans.Commit()
                myTrans.Dispose()
                myTrans = Nothing

            Catch ex As Exception
                myTrans.Rollback()
                Throw ex
            End Try

        Catch ex As Exception

            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Function chkConfirmWithdraw_TransportManifest_TM(ByVal Ref_No1 As String) As Boolean
        Dim strSQL As String = ""
        'Dim strWhere As String = ""
        Try

            strSQL = "select count(*)"
            strSQL &= " from tb_WithdrawItem WI left outer join"
            strSQL &= " 	tb_SalesOrder SO ON SO.SalesOrder_Index = WI.DocumentPlan_Index"
            strSQL &= "             where (WI.Plan_Process = 10)"
            strSQL &= " 	and SO.TransportManifest_Index in (select TransportManifest_Index from tb_TransportManifest where Status <> 5)"
            strSQL &= " 	and WI.Withdraw_Index  in (select Withdraw_Index from tb_Withdraw where Ref_No1 = '" & Ref_No1 & "') "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows(0)(0).ToString <> "0" Then
                Return True
            End If

            Return False
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
End Class
