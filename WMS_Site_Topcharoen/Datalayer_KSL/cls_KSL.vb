Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports System.Configuration.ConfigurationSettings
Imports Microsoft.Office.Interop
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master

Public Class cls_KSL : Inherits DBType_SQLServer
    Private _dataTable As System.Data.DataTable = New System.Data.DataTable
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
    Public Function Confirm_SO(ByVal SalesOrder_Index As String, Optional ByVal SalesOrder_No As String = "") As Boolean

        Dim strSQL As String = ""
        'Dim ochkTransport As New config_CustomSetting
        'Dim chkTransport As Boolean = ochkTransport.getConfig_Key_USE("USE_TRANSPORT")

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update status in tb_SaleOrderItem = 2 (Confirmed)
        ' --- STEP 2: Update status in tb_SaleOrder = 2 (Confirmed)
        ' --- STEP 3: Update status in Sy_Audit_Log

        ' Todd 8 Jan 2010: Out of Transaction. Why?
        ' --- STEP 4: Update status in tb_Reserve
        Try

            ' --- STEP 1: Update status in tb_SaleOrderItem = -1 (Cancelled)
            strSQL = "UPDATE tb_SalesOrderItem "
            strSQL &= " SET status =2 "
            strSQL &= " , Update_date =getdate() "
            strSQL &= " , Update_by = '" & WV_UserName & "' "
            strSQL &= " , Update_branch = '" & WV_Branch_ID & "' "
            strSQL &= " WHERE SalesOrderItem_Index ='" & SalesOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Update status in tb_SaleOrder = -1 (Cancelled)
            strSQL = "UPDATE tb_SalesOrder "
            strSQL &= " SET status =2 "
            strSQL &= " , Confirm_Date =getdate() "
            strSQL &= " , Confirm_By = '" & WV_UserName & "' "
            strSQL &= " WHERE SalesOrder_Index ='" & SalesOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 3: Update status in Sy_Audit_Log
            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = SalesOrder_Index 'Me._newSaleOrder_Index
            oAudit_Log.Document_No = SalesOrder_No '_objSaleOrder.SalesOrder_No
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Confirm_SO)


            'strSQL = "SELECT *"
            'strSQL &= " FROM tb_SalesOrder"
            'strSQL &= " WHERE SalesOrder_Index ='" & SalesOrder_Index & "'"
            'With SQLServerCommand
            '    .Connection = Connection
            '    .Transaction = myTrans
            '    .CommandText = strSQL
            '    .CommandTimeout = 0
            'End With
            'DataAdapter.SelectCommand = SQLServerCommand
            'DataAdapter.SelectCommand.Transaction = myTrans
            'DS = New DataSet
            'DataAdapter.Fill(DS, "tbSo")



            'If DS.Tables("tbSo").Rows.Count <> 0 Then
            '    Dim Reserve_Index = ""
            '    Reserve_Index = DS.Tables("tbSo").Rows(0).Item("Reserve_Index").ToString()
            '    ' --- STEP 4: Update status in tb_Reserve
            '    strSQL = " UPDATE tb_Reserve SET  "
            '    strSQL &= " Status= 3 "
            '    strSQL &= " WHERE Reserve_Index='" & Reserve_Index & "'"

            '    SetSQLString = strSQL
            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            '    EXEC_Command()
            'End If

            myTrans.Commit()

            Return True

        Catch e As Exception
            Try
                myTrans.Rollback()

                Return False

                '   Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getReportRemain(ByVal pwhere As String) As DataTable
        Try

            Return DBExeQuery(" select * from view_rpt_so_remain where 1=1  " & pwhere)

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function getCheckStock_Sales(ByVal SalesOrder_index As String, ByVal Customer_index As String, ByVal DistributionCenter_Index As String) As DataTable

        ' *** define value ***
        Dim strSQL As String = ""
        Try
            Dim _dtCheckStock_Sales As New DataTable

            strSQL = " 	SELECT VSOH.Sku_Index,VSOH.Sku_Id,VSOH.Package,VSOH.Std_Package_Index,VSOH.ItemStatus_Index"
            strSQL &= "             ,ISNULL(SUM(VSOH.Total_Qty),0) Total_Qty"
            strSQL &= "             ,ISNULL(SUM(VBAL.Qty_Bal),0) Qty_Bal"
            strSQL &= "             ,ISNULL(SUM(VSOP.Total_Qty_Pending),0) as Total_Qty_Pending"
            strSQL &= "             ,(ISNULL(SUM(VBAL.Qty_Bal),0) - ISNULL(SUM(VSOP.Total_Qty_Pending),0)) - ISNULL(SUM(VSOH.Total_Qty),0)  as Total_Qty_Request"
            strSQL &= " FROM ( SELECT tb_salesOrderItem.Sku_Index,ms_Sku.Sku_Id,ms_Package.Description as Package,ms_Sku.Package_Index as Std_Package_Index,tb_salesOrderItem.ItemStatus_Index"
            strSQL &= "             ,SUM(ISNULL(tb_salesOrderItem.Total_Qty,0))- SUM(ISNULL(tb_salesOrderItem.Total_Qty_Withdraw,0))  Total_Qty"

            'strSQL &= " 			,ISNULL(SUM(VSOH.Total_Qty),0)-ISNULL(SUM(VSOH.Total_Qty_Pending),0) Total_Qty" 'จำนวนที่สั่ง
            'strSQL &= " 			,ISNULL(SUM(VSOP.Total_Qty_Pending),0) + ISNULL(SUM(VSOH.Total_Qty_Pending),0) as Total_Qty_Pending" 'จำนวนสั่งขายค้าง
            'strSQL &= " 			,ISNULL(SUM(VBAL.Qty_Bal),0) - ISNULL(SUM(VSOH.Total_Qty_Pending),0) Qty_Bal" 'สินค้าคงคลัง
            'strSQL &= " 			,ISNULL(SUM(VBAL.Qty_Bal),0) - (ISNULL(SUM(VSOP.Total_Qty_Pending),0) + ISNULL(SUM(VSOH.Total_Qty_Pending),0))  as Total_Qty_Request" 'จำนวนคงเหลือ
            'strSQL &= " FROM ( "
            'strSQL &= "         SELECT tb_salesOrderItem.Sku_Index,ms_Sku.Sku_Id,ms_Package.Description as Package,ms_Sku.Package_Index as Std_Package_Index,tb_salesOrderItem.ItemStatus_Index,tb_salesOrder.Status"
            'strSQL &= "             ,SUM(ISNULL(tb_salesOrderItem.Total_Qty,0)) AS Total_Qty"
            'strSQL &= "             ,CASE WHEN tb_salesOrder.Status = 1 THEN 0 ELSE SUM(ISNULL(tb_salesOrderItem.Total_Qty,0))- SUM(ISNULL(tb_salesOrderItem.Total_Qty_Withdraw,0))  END AS Total_Qty_Pending"
            strSQL &= " 	    FROM tb_salesOrderItem "
            strSQL &= " 		    inner join tb_salesOrder on tb_salesOrder.SalesOrder_Index = tb_salesOrderItem.SalesOrder_Index     "
            strSQL &= " 		    inner join ms_Sku on ms_Sku.Sku_Index = tb_salesOrderItem.Sku_Index     "
            strSQL &= " 		    inner join ms_Package on ms_Package.Package_Index = ms_Sku.Package_Index     "
            strSQL &= " 	    WHERE tb_salesOrder.SalesOrder_Index in ('" & SalesOrder_index & "')"
            strSQL &= "         GROUP BY tb_salesOrderItem.sku_index,ms_Sku.Sku_Id,ms_Package.Description,ms_Sku.Package_Index,tb_salesOrderItem.ItemStatus_Index"
            'strSQL &= " 	    GROUP BY tb_salesOrderItem.sku_index,ms_Sku.Sku_Id,ms_Package.Description,ms_Sku.Package_Index,tb_salesOrderItem.ItemStatus_Index,tb_salesOrder.Status"
            strSQL &= " ) VSOH "

            strSQL &= " 		left outer join "
            strSQL &= " 		(	SELECT  LB.Sku_Index,LB.ItemStatus_Index"
            strSQL &= "                     ,SUM(LB.Qty_Bal - LB.ReserveQty) AS Qty_Bal"
            strSQL &= " 			FROM    tb_LocationBalance LB WITH (nolock)"
            strSQL &= "                     inner join tb_Order O  WITH (nolock) ON O.Order_Index = LB.Order_Index"
            strSQL &= "                     inner join ms_Location L  WITH (nolock) ON L.Location_Index = LB.Location_Index"
            strSQL &= "                     inner join ms_Warehouse W  WITH (nolock) ON W.Warehouse_Index = L.Warehouse_Index"
            strSQL &= "                     inner join ms_DistributionCenter DC  WITH (nolock) ON DC.DistributionCenter_Index = W.DistributionCenter_Index"
            strSQL &= " 			WHERE   (LB.Qty_Bal > 0)"
            strSQL &= "                     and	DC.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            strSQL &= " 			GROUP BY Sku_Index,LB.ItemStatus_Index"
            strSQL &= " 		)VBAL ON  VBAL.Sku_Index = VSOH.Sku_Index AND VBAL.ItemStatus_Index = VSOH.ItemStatus_Index "

            strSQL &= " 		left outer join "
            strSQL &= " 		(	SELECT  SOIP.Sku_Index,SOIP.ItemStatus_Index"
            strSQL &= "                     ,SUM(SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw,0)) AS Total_Qty_Pending"
            strSQL &= " 			FROM    tb_salesOrderItem SOIP WITH (nolock)"
            strSQL &= "                     inner join tb_salesOrder SOP  WITH (nolock) on SOP.SalesOrder_Index = SOIP.SalesOrder_Index "
            strSQL &= " 			WHERE   SOP.Status in (2,6) "
            strSQL &= "                     AND	SOP.SalesOrder_Index not in  ('" & SalesOrder_index & "')"
            strSQL &= "                     AND	SOP.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            strSQL &= "                     AND (SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw, 0)) > 0"
            strSQL &= " 			GROUP BY SOIP.Sku_Index,SOIP.ItemStatus_Index"
            strSQL &= " 		)VSOP ON  VSOP.Sku_Index = VSOH.Sku_Index AND VSOP.ItemStatus_Index = VSOH.ItemStatus_Index "

            strSQL &= " 	GROUP BY VSOH.Sku_Index,VSOH.Sku_Id,VSOH.Package,VSOH.Std_Package_Index,VSOH.ItemStatus_Index"




            'strSQL = " 	Select tb_salesOrderItem.Sku_Index,ms_Sku.Sku_Id,ms_Package.Description as Package,ms_Sku.Package_Index as Std_Package_Index,tb_salesOrderItem.ItemStatus_Index"
            'strSQL &= " 			,ISNULL(SUM(tb_salesOrderItem.Total_Qty),0)  Total_Qty"
            'strSQL &= " 			,ISNULL(SUM(VBAL.Qty_Bal),0) Qty_Bal"
            'strSQL &= " 			,ISNULL(SUM(VSOP.Total_Qty_Pending),0) as Total_Qty_Pending"
            'strSQL &= " 			,(ISNULL(SUM(VBAL.Qty_Bal),0) - ISNULL(SUM(VSOP.Total_Qty_Pending),0)) - ISNULL(SUM(tb_salesOrderItem.Total_Qty),0)  as Total_Qty_Request"

            'strSQL &= " 	from tb_salesOrderItem "
            'strSQL &= " 		inner join tb_salesOrder on tb_salesOrder.SalesOrder_Index = tb_salesOrderItem.SalesOrder_Index     "
            'strSQL &= " 		inner join ms_Sku on ms_Sku.Sku_Index = tb_salesOrderItem.Sku_Index     "
            'strSQL &= " 		inner join ms_Package on ms_Package.Package_Index = ms_Sku.Package_Index     "

            'strSQL &= " 		left outer join "
            'strSQL &= " 		(	SELECT  LB.Sku_Index,LB.ItemStatus_Index"
            'strSQL &= "                     ,SUM(LB.Qty_Bal - LB.ReserveQty) AS Qty_Bal"
            'strSQL &= " 			FROM    tb_LocationBalance LB WITH (nolock)"
            'strSQL &= "                     inner join tb_Order O  WITH (nolock) ON O.Order_Index = LB.Order_Index"
            'strSQL &= "                     inner join ms_Location L  WITH (nolock) ON L.Location_Index = LB.Location_Index"
            'strSQL &= "                     inner join ms_Warehouse W  WITH (nolock) ON W.Warehouse_Index = L.Warehouse_Index"
            'strSQL &= "                     inner join ms_DistributionCenter DC  WITH (nolock) ON DC.DistributionCenter_Index = W.DistributionCenter_Index"
            'strSQL &= " 			WHERE   (LB.Qty_Bal > 0)"
            'strSQL &= "                     and	DC.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            'strSQL &= " 			GROUP BY Sku_Index,LB.ItemStatus_Index"
            'strSQL &= " 		)VBAL ON  VBAL.Sku_Index = tb_salesOrderItem.Sku_Index AND VBAL.ItemStatus_Index = tb_salesOrderItem.ItemStatus_Index "

            'strSQL &= " 		left outer join "
            'strSQL &= " 		(	SELECT  SOIP.Sku_Index,SOIP.ItemStatus_Index"
            'strSQL &= "                     ,SUM(SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw,0)) AS Total_Qty_Pending"
            'strSQL &= " 			FROM    tb_salesOrderItem SOIP WITH (nolock)"
            'strSQL &= "                     inner join tb_salesOrder SOP  WITH (nolock) on SOP.SalesOrder_Index = SOIP.SalesOrder_Index "
            'strSQL &= " 			WHERE   SOP.Status in (2,6) "
            'strSQL &= "                     AND	SOP.SalesOrder_Index not in  ('" & SalesOrder_index & "')"
            'strSQL &= "                     AND	SOP.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            'strSQL &= "                     AND (SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw, 0)) > 0"
            'strSQL &= " 			GROUP BY SOIP.Sku_Index,SOIP.ItemStatus_Index"
            'strSQL &= " 		)VSOP ON  VSOP.Sku_Index = tb_salesOrderItem.Sku_Index AND VSOP.ItemStatus_Index = tb_salesOrderItem.ItemStatus_Index "


            'strSQL &= " 	where tb_salesOrder.SalesOrder_Index in ('" & SalesOrder_index & "')"
            'strSQL &= " 	GROUP BY tb_salesOrderItem.sku_index,ms_Sku.Sku_Id,ms_Package.Description,ms_Sku.Package_Index,tb_salesOrderItem.ItemStatus_Index"

            _dtCheckStock_Sales = DBExeQuery(strSQL)

            Return _dtCheckStock_Sales

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getCheckStock_Sales(ByVal SalesOrder_index As String, ByVal Customer_index As String, ByVal DistributionCenter_Index As String,Document_Group_Name as String) As DataTable

        ' *** define value ***
        Dim strSQL As String = ""
        Try
            Dim _dtCheckStock_Sales As New DataTable






            strSQL = " 	SELECT VSOH.Sku_Index,VSOH.Sku_Id,VSOH.Package,VSOH.Std_Package_Index,VSOH.ItemStatus_Index"
            strSQL &= "             ,ISNULL(SUM(VSOH.Total_Qty),0) Total_Qty"
            strSQL &= "             ,ISNULL(SUM(VBAL.Qty_Bal),0) Qty_Bal"
            strSQL &= "             ,ISNULL(SUM(VSOP.Total_Qty_Pending),0) as Total_Qty_Pending"
            strSQL &= "             ,(ISNULL(SUM(VBAL.Qty_Bal),0) - ISNULL(SUM(VSOP.Total_Qty_Pending),0)) - ISNULL(SUM(VSOH.Total_Qty),0)  as Total_Qty_Request"
            strSQL &= " FROM ( SELECT tb_salesOrderItem.Sku_Index,ms_Sku.Sku_Id,ms_Package.Description as Package,ms_Sku.Package_Index as Std_Package_Index,tb_salesOrderItem.ItemStatus_Index"
            strSQL &= "             ,SUM(ISNULL(tb_salesOrderItem.Total_Qty,0))- SUM(ISNULL(tb_salesOrderItem.Total_Qty_Withdraw,0))  Total_Qty"
            strSQL &= " 	    FROM tb_salesOrderItem "
            strSQL &= " 		    inner join tb_salesOrder on tb_salesOrder.SalesOrder_Index = tb_salesOrderItem.SalesOrder_Index     "
            strSQL &= " 		    inner join ms_Sku on ms_Sku.Sku_Index = tb_salesOrderItem.Sku_Index     "
            strSQL &= " 		    inner join ms_Package on ms_Package.Package_Index = ms_Sku.Package_Index     "
            strSQL &= " 	    WHERE tb_salesOrder.SalesOrder_Index in ('" & SalesOrder_index & "')"
            strSQL &= "         GROUP BY tb_salesOrderItem.sku_index,ms_Sku.Sku_Id,ms_Package.Description,ms_Sku.Package_Index,tb_salesOrderItem.ItemStatus_Index"
            strSQL &= " ) VSOH "

            strSQL &= " 		left outer join "
            strSQL &= " 		(	SELECT  LB.Sku_Index,LB.ItemStatus_Index"
            strSQL &= "                     ,SUM(LB.Qty_Bal - LB.ReserveQty) AS Qty_Bal"
            strSQL &= " 			FROM    tb_LocationBalance LB WITH (nolock)"
            strSQL &= "                     inner join tb_Order O  WITH (nolock) ON O.Order_Index = LB.Order_Index"
            strSQL &= "                     inner join ms_Location L  WITH (nolock) ON L.Location_Index = LB.Location_Index"
            strSQL &= "                     inner join ms_Warehouse W  WITH (nolock) ON W.Warehouse_Index = L.Warehouse_Index"
            strSQL &= "                     inner join ms_DistributionCenter DC  WITH (nolock) ON DC.DistributionCenter_Index = W.DistributionCenter_Index"
            strSQL &= " 			WHERE   (LB.Qty_Bal > 0)"
            strSQL &= "                     and	DC.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            strSQL &= " 			GROUP BY Sku_Index,LB.ItemStatus_Index"
            strSQL &= " 		)VBAL ON  VBAL.Sku_Index = VSOH.Sku_Index AND VBAL.ItemStatus_Index = VSOH.ItemStatus_Index "

            strSQL &= " 		left outer join "
            strSQL &= " 		(	SELECT  SOIP.Sku_Index,SOIP.ItemStatus_Index"
            strSQL &= "                     ,SUM(SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw,0)) AS Total_Qty_Pending"
            strSQL &= " 			FROM    tb_salesOrderItem SOIP WITH (nolock)"
            strSQL &= "                     inner join tb_salesOrder SOP  WITH (nolock) on SOP.SalesOrder_Index = SOIP.SalesOrder_Index "
            strSQL &= " 		            inner join ms_DocumentType DT  WITH (nolock) on DT.DocumentType_Index = SOP.DocumentType_Index "
            strSQL &= " 			WHERE   SOP.Status in (2,6) "
            strSQL &= "                     AND	SOP.SalesOrder_Index not in  ('" & SalesOrder_index & "')"
            strSQL &= "                     AND	SOP.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            strSQL &= " 		            AND (DT.Document_Group_Name = '" & Document_Group_Name & "')"
            strSQL &= "                     AND (SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw, 0)) > 0"
            strSQL &= " 			GROUP BY SOIP.Sku_Index,SOIP.ItemStatus_Index"
            strSQL &= " 		)VSOP ON  VSOP.Sku_Index = VSOH.Sku_Index AND VSOP.ItemStatus_Index = VSOH.ItemStatus_Index "

            strSQL &= " 	GROUP BY VSOH.Sku_Index,VSOH.Sku_Id,VSOH.Package,VSOH.Std_Package_Index,VSOH.ItemStatus_Index"




            'strSQL = " 	Select tb_salesOrderItem.Sku_Index,ms_Sku.Sku_Id,ms_Package.Description as Package,ms_Sku.Package_Index as Std_Package_Index,tb_salesOrderItem.ItemStatus_Index"
            'strSQL &= " 			,ISNULL(SUM(tb_salesOrderItem.Total_Qty),0)  Total_Qty"
            'strSQL &= " 			,ISNULL(SUM(VBAL.Qty_Bal),0) Qty_Bal"
            'strSQL &= " 			,ISNULL(SUM(VSOP.Total_Qty_Pending),0) as Total_Qty_Pending"
            'strSQL &= " 			,(ISNULL(SUM(VBAL.Qty_Bal),0) - ISNULL(SUM(VSOP.Total_Qty_Pending),0)) - ISNULL(SUM(tb_salesOrderItem.Total_Qty),0)  as Total_Qty_Request"

            'strSQL &= " 	from tb_salesOrderItem "
            'strSQL &= " 		inner join tb_salesOrder on tb_salesOrder.SalesOrder_Index = tb_salesOrderItem.SalesOrder_Index     "
            'strSQL &= " 		inner join ms_Sku on ms_Sku.Sku_Index = tb_salesOrderItem.Sku_Index     "
            'strSQL &= " 		inner join ms_Package on ms_Package.Package_Index = ms_Sku.Package_Index     "

            'strSQL &= " 		left outer join "
            'strSQL &= " 		(	SELECT  LB.Sku_Index,LB.ItemStatus_Index"
            'strSQL &= "                     ,SUM(LB.Qty_Bal - LB.ReserveQty) AS Qty_Bal"
            'strSQL &= " 			FROM    tb_LocationBalance LB WITH (nolock)"
            'strSQL &= "                     inner join tb_Order O  WITH (nolock) ON O.Order_Index = LB.Order_Index"
            'strSQL &= "                     inner join ms_Location L  WITH (nolock) ON L.Location_Index = LB.Location_Index"
            'strSQL &= "                     inner join ms_Warehouse W  WITH (nolock) ON W.Warehouse_Index = L.Warehouse_Index"
            'strSQL &= "                     inner join ms_DistributionCenter DC  WITH (nolock) ON DC.DistributionCenter_Index = W.DistributionCenter_Index"
            'strSQL &= " 			WHERE   (LB.Qty_Bal > 0)"
            'strSQL &= "                     and	DC.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            'strSQL &= " 			GROUP BY Sku_Index,LB.ItemStatus_Index"
            'strSQL &= " 		)VBAL ON  VBAL.Sku_Index = tb_salesOrderItem.Sku_Index AND VBAL.ItemStatus_Index = tb_salesOrderItem.ItemStatus_Index "

            'strSQL &= " 		left outer join "
            'strSQL &= " 		(	SELECT  SOIP.Sku_Index,SOIP.ItemStatus_Index"
            'strSQL &= "                     ,SUM(SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw,0)) AS Total_Qty_Pending"
            'strSQL &= " 			FROM    tb_salesOrderItem SOIP WITH (nolock)"
            'strSQL &= "                     inner join tb_salesOrder SOP  WITH (nolock) on SOP.SalesOrder_Index = SOIP.SalesOrder_Index "
            'strSQL &= " 			WHERE   SOP.Status in (2,6) "
            'strSQL &= "                     AND	SOP.SalesOrder_Index not in  ('" & SalesOrder_index & "')"
            'strSQL &= "                     AND	SOP.DistributionCenter_Index= '" & DistributionCenter_Index & "' "
            'strSQL &= "                     AND (SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw, 0)) > 0"
            'strSQL &= " 			GROUP BY SOIP.Sku_Index,SOIP.ItemStatus_Index"
            'strSQL &= " 		)VSOP ON  VSOP.Sku_Index = tb_salesOrderItem.Sku_Index AND VSOP.ItemStatus_Index = tb_salesOrderItem.ItemStatus_Index "


            'strSQL &= " 	where tb_salesOrder.SalesOrder_Index in ('" & SalesOrder_index & "')"
            'strSQL &= " 	GROUP BY tb_salesOrderItem.sku_index,ms_Sku.Sku_Id,ms_Package.Description,ms_Sku.Package_Index,tb_salesOrderItem.ItemStatus_Index"

            _dtCheckStock_Sales = DBExeQuery(strSQL)


            Return _dtCheckStock_Sales

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub ResetColor_Sales(ByVal dtCheckStock_Sales As DataTable, ByVal SalesOrder_index As String)
        Try
            Dim Result As Integer = 0
            For Each drColor As DataRow In dtCheckStock_Sales.Rows
                If (drColor("Total_Qty") + drColor("Total_Qty_Pending")) > drColor("Qty_Bal") Then
                    Result += 1
                    'แดง
                    DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 2 where SalesOrder_Index = '{0}' and Sku_Index = '{1}'", SalesOrder_index, drColor("Sku_Index").ToString))
                Else
                    'ไม่สี
                    DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 0 where SalesOrder_Index = '{0}' and Sku_Index = '{1}'", SalesOrder_index, drColor("Sku_Index").ToString))
                End If
            Next
            If Result = 0 Then
                'เขียว
                DBExeNonQuery("Update tb_salesorder set RGB_Check = '1' where SalesOrder_Index = '" & SalesOrder_index & "'")
            Else
                'แดง
                DBExeNonQuery("Update tb_salesorder set RGB_Check = '2' where SalesOrder_Index = '" & SalesOrder_index & "'")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Sub getSKU_Package(ByVal Sku_Index As String)

        ' *** define value ***
        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT      ms_SKURatio.Sku_Index,  ms_SKURatio.Package_Index, ms_Package.Description AS Package,ms_SKURatio.Ratio  "
            strSQL &= "             ,Isnull(ms_Package.Weight,0) as Unit_Weight "
            strSQL &= "             ,(Isnull(ms_Package.Dimension_Wd,0)*Isnull(ms_Package.Dimension_Len,0)*Isnull(ms_Package.Dimension_Hi,0))/Isnull(ms_DimensionType.Ratio,1) as Unit_Volume "
            strSQL &= " FROM        ms_SKURatio INNER JOIN  "
            strSQL &= "             ms_SKU ON ms_SKURatio.Sku_Index = ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "             ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index  LEFT JOIN "
            strSQL &= "             ms_DimensionType on ms_DimensionType.DimensionType_Index=ms_Package.DimensionType_Index "
            strSQL &= " WHERE       ms_SKURatio.Sku_Index ='" & Sku_Index & "'"
            strSQL &= "             AND ms_SKURatio.status_id <> -1 AND ms_SKU.Status_Id <> -1 AND  isNull(isItem_Package,0) = 0"

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

  
    Public Sub getZone_Warehouse(ByVal Warehouse_Index)
        Dim strSQL As String = " "
        Try
            strSQL = "  SELECT vml.Warehouse_Index,vml.Zone_Index vZone_Index,ms_Zone.*"
            strSQL &= "             FROM ms_Zone "
            strSQL &= "  	inner join  (   select Warehouse_Index,Zone_Index "
            strSQL &= "                     from ms_Location "
            strSQL &= "  					Group by Warehouse_Index,Zone_Index )vml on ms_Zone.Zone_Index = vml.Zone_Index"
            strSQL &= "  WHERE ms_Zone.status_id <> -1  and vml.Warehouse_Index = '" & Warehouse_Index & "'"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function getSearchData_ConfigPicking(ByVal _Condition As String) As DataTable

        Dim strSQL As String = ""

        Try
            strSQL = "   select *  "
            strSQL &= "  FROM VIEW_KSL_Picking_CustomerType_DistributionCenter_ProductGroup"
            strSQL &= "  where 1=1 " & _Condition

            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getSearchData_ConfigPicking_Group(ByVal _Condition As String) As DataTable

        Dim strSQL As String = ""

        Try
            strSQL = "   select Running_Id,CustomerType_Index, DistributionCenter_Index, Description,MFG_Day_Count"
            strSQL &= "  ,CustomerType,DistributionCenter"
            strSQL &= "  FROM VIEW_KSL_Picking_CustomerType_DistributionCenter_ProductGroup"
            strSQL &= "  where status_id <> -1 " & _Condition
            strSQL &= "  group by Running_Id,CustomerType_Index, DistributionCenter_Index, Description,MFG_Day_Count"
            strSQL &= "  ,CustomerType,DistributionCenter"
            strSQL &= "  Order by Running_Id,CustomerType_Index, DistributionCenter_Index,MFG_Day_Count"
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function



    Public Function getProductType(ByVal _Condition As String) As DataTable

        Dim strSQL As String = ""

        Try
            strSQL = "   select *  "
            strSQL &= "  FROM ms_ProductType"
            strSQL &= "  where Status_id <> -1  " & _Condition

            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function DeleteConfigPicking(ByVal Running_Id As String) As Integer

        Dim strSQL As String = ""

        Try
            strSQL = String.Format("DELETE config_Picking_CustomerType_DistributionCenter_ProductGroup WHERE Running_Id = '{0}'", Running_Id)
            Return DBExeNonQuery(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function MaxConfigPicking() As String
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT isnull(max(x.Running_Id),0) + 1 FROM config_Picking_CustomerType_DistributionCenter_ProductGroup x "
            Return DBExeQuery_Scalar(strSQL)
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function SaveConfigPicking(ByVal Running_Id As String _
           , ByVal CustomerType_Index As String _
           , ByVal DistributionCenter_Index As String _
           , ByVal ProductType_Index As String _
           , ByVal Description As String _
           , ByVal MFG_Day_Count As Integer) As Integer

        Dim strSQL As String = ""
        Try
            strSQL = "  INSERT INTO config_Picking_CustomerType_DistributionCenter_ProductGroup"
            strSQL &= "  ([Running_Id]"
            strSQL &= "  ,[CustomerType_Index]"
            strSQL &= "  ,[DistributionCenter_Index]"
            strSQL &= "  ,[ProductType_Index]"
            strSQL &= "  ,[Description]"
            strSQL &= "  ,[MFG_Day_Count]"
            strSQL &= "  ,[add_by]"
            strSQL &= "  ,[add_date]"
            strSQL &= "  ,[add_branch]"
            strSQL &= "  ,[update_by]"
            strSQL &= "  ,[update_date]"
            strSQL &= "  ,[update_branch]"
            strSQL &= "  ,[Cancel_by]"
            strSQL &= "  ,[Cancel_date]"
            strSQL &= "  ,[Cancel_branch]"
            strSQL &= "  ,[status_id])"
            strSQL &= "  VALUES("
            strSQL &= "  '" & Running_Id & "'"
            strSQL &= "  ,'" & CustomerType_Index & "'"
            strSQL &= "  ,'" & DistributionCenter_Index & "'"
            strSQL &= "  ,'" & ProductType_Index & "'"
            strSQL &= "  ,'" & Description & "'"
            strSQL &= "  ," & MFG_Day_Count
            strSQL &= "  ,'" & WV_Branch_ID & "'"
            strSQL &= "  ,getdate()"
            strSQL &= "  ,1"
            strSQL &= "  ,Null"
            strSQL &= "  ,Null"
            strSQL &= "  ,Null"
            strSQL &= "  ,Null"
            strSQL &= "  ,Null"
            strSQL &= "  ,Null"
            strSQL &= "  ,1)"

            Return DBExeNonQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
End Class


Public Class Export_Excel_KC

    Private Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer

    Public Sub export(ByVal ds As DataSet, ByVal FileName As String)
        Try
            Dim saveFileDialog As New SaveFileDialog
            saveFileDialog.Filter = "Excel File|*.xls"
            saveFileDialog.Title = "Save an Excel File"
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            saveFileDialog.FileName = FileName
            If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Me.exportExcel(saveFileDialog.FileName, ds)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function DataGridViewToDataTable(ByVal dtg As DataGridView, Optional ByVal DataTableName As String = "myDataTable") As System.Data.DataTable
        Try

            Dim dt As New System.Data.DataTable(DataTableName)
            Dim row As DataRow
            Dim TotalDatagridviewColumns As Integer = dtg.ColumnCount - 1
            Dim visibleList As New List(Of String)
            'Add Datacolumn
            For Each c As DataGridViewColumn In dtg.Columns
                If c.Visible Then
                    visibleList.Add(c.HeaderText)
                End If
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.ColumnName = c.HeaderText
                If (Not c.ValueType Is Nothing) Then
                    idColumn.DataType = c.ValueType
                End If
                dt.Columns.Add(idColumn)
            Next
            'Now Iterate thru Datagrid and create the data row
            For Each dr As DataGridViewRow In dtg.Rows
                'Iterate thru datagrid
                row = dt.NewRow 'Create new row
                'Iterate thru Column 1 up to the total number of columns
                For cn As Integer = 0 To TotalDatagridviewColumns
                    'If IsNumeric(IfNullObj(dr.Cells(cn).Value)) = False Then
                    '  If row.Table.Columns(cn).DataType.Name.Contains("Stirng") Then
                    'Dim obj = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dr.Cells(cn).Value, row.Table.Columns(cn).DataType)

                    ' End If

                    ' row.Item(cn) = IfNullObj(dr.Cells(cn).Value)
                    row.Item(cn) = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dr.Cells(cn).Value, row.Table.Columns(cn).DataType)
                    ' This Will handle error datagridviewcell on NULL Values
                Next
                'Now add the row to Datarow Collection
                dt.Rows.Add(row)
            Next
            For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
                If Not visibleList.Contains(dt.Columns(iCol).ColumnName) Then
                    dt.Columns.Remove(dt.Columns(iCol))
                End If
            Next
            'Now return the data table
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub exportExcel(ByVal strFileName As String, ByVal dsExport As DataSet)
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        'start the application
        'Dim xlApp As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim xlApp As Object = CreateObject("Excel.Application")

        'get the window handle
        Dim xlHWND As Integer = xlApp.Hwnd

        'this will have the process ID after call to GetWindowThreadProcessId
        Dim xlProcessId As Integer = 0

        'get the process ID
        GetWindowThreadProcessId(xlHWND, xlProcessId)

        'get the process
        Dim xlProcess As Process = Process.GetProcessById(xlProcessId)

        Dim isFileOpen As Boolean = False

        'Dim xlWorkBook As Workbook
        'Dim xlWorkSheet As Worksheet
        Dim xlWorkBook As New Object
        Dim xlWorkSheet As New Object
        Try
            If xlApp Is Nothing Then
                W_MSG_Error("Excel is not properly installed")
                Return
            End If
            If IsNumeric(xlApp.Version) AndAlso CDec(xlApp.Version) < 11 Then
                W_MSG_Error("Excel is lower than version 11 (2003)")
                Return
            End If

            Dim misValue As Object = System.Reflection.Missing.Value
            'Dim chartRange As Range
            Dim sheetIndex As Integer = 0

            xlWorkBook = xlApp.Workbooks.Add(misValue)

            ' Copy each DataTable as a new Sheet
            For Each dt As System.Data.DataTable In dsExport.Tables
                Dim Title_Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)
                For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
                    With dt.Columns(iCol)
                        If .ColumnName.Contains("title_") Then
                            Title_Dict.Add(.ColumnName, dt.Rows(0).Item(.ColumnName).ToString())
                            dt.Columns.Remove(dt.Columns(iCol))
                        End If
                    End With
                Next
                sheetIndex += 1

                ' Copy the DataTable to an object array
                Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

                ' Copy the column names to the first row of the object array
                For col As Integer = 0 To dt.Columns.Count - 1
                    rawData(0, col) = dt.Columns(col).ColumnName
                Next

                ' Copy the values to the object array
                For col As Integer = 0 To dt.Columns.Count - 1
                    For row As Integer = 0 To dt.Rows.Count - 1
                        rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
                    Next
                Next

                Dim finalColLetter As String = ColumnIndexToColumnLetter(dt.Columns.Count)

                ' Create a new Sheet
                xlWorkSheet = CType(xlWorkBook.Sheets.Add(xlWorkBook.Sheets(sheetIndex), Type.Missing, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet), Microsoft.Office.Interop.Excel.Worksheet)

                'xlWorkSheet = xlWorkBook.Sheets(dt.TableName)
                xlWorkSheet.Name = dt.TableName

                ' Format
                'xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).NumberFormat = "@"
                For iCol As Integer = 0 To dt.Columns.Count - 1
                    Select Case dt.Columns(iCol).DataType.Name
                        Case "Boolean", "String"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                        Case "DateTime", "TimeSpan"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "dd/MM/yyyy"
                        Case "Decimal", "Double"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0.000000"
                        Case "Int16", "Int32", "Int64"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0"
                        Case Else
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                    End Select
                Next
                'xlWorkSheet.Range("B:B").NumberFormat = "dd/MM/yyyy"
                'xlWorkSheet.Range("C:D").NumberFormat = "@"
                'xlWorkSheet.Range("E:F").NumberFormat = "#,##0"
                'xlWorkSheet.Range("G:H").NumberFormat = "@"
                'xlWorkSheet.Range("I:K").NumberFormat = "#,##0.00"
                'xlWorkSheet.Range("L:L").NumberFormat = "@"

                ' data export to Excel
                Dim iStartRows As Integer = 1
                Dim excelRange As String = String.Format("A{0}:{1}{2}", iStartRows, finalColLetter, iStartRows + dt.Rows.Count)
                xlWorkSheet.Range(excelRange, Type.Missing).Value2 = rawData

                'Dim headerList As New List(Of String)
                '

                'For iHeader As Integer = 1 To headerList.Count
                '    xlWorkSheet.Cells(1, iHeader) = headerList.Item(iHeader - 1)
                'Next

                'For iCols As Integer = 1 To 18
                '    ' Merge Cell
                '    xlWorkSheet.Range(String.Format("{0}1:{0}3", ColumnIndexToColumnLetter(iCols))).Merge()
                'Next
                'xlWorkSheet.Range(String.Format("{0}1:{1}1", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(24))).Merge()
                'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(22))).Merge()
                'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(23), ColumnIndexToColumnLetter(24))).Merge()

                ' Font Bold
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Bold = True

                ' Align
                xlWorkSheet.Range("A:AU").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("B:B").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("C:D").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("E:F").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("G:H").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("I:K").HorizontalAlignment = XlHAlign.xlHAlignRight
                'xlWorkSheet.Range("L:L").HorizontalAlignment = XlHAlign.xlHAlignLeft

                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

                ' Fill Color
                'xlWorkSheet.Range(String.Format("A1:{0}3", finalColLetter)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray)
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Interior.ColorIndex = 56

                ' Font Color
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

                ' Border Cell
                xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic)
                With xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                End With
                With xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                End With
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic)

                '' Read Data
                'Dim strCell As String = ""
                'Dim iCellTemp As Integer = 0
                'Dim strCellTemp As String = ""
                'Dim flag As Boolean = False
                'For iRows As Integer = iStartRows + 1 To iStartRows + dt.Rows.Count + 1
                '    strCell = xlWorkSheet.Cells(iRows, 1).value
                '    If iCellTemp = 0 Then
                '        iCellTemp = iRows
                '        strCellTemp = strCell
                '    End If
                '    If strCell <> strCellTemp Then
                '        If flag Then
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Transparent)
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 15
                '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
                '                .LineStyle = XlLineStyle.xlContinuous
                '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                '                .TintAndShade = 0
                '                .Weight = XlBorderWeight.xlThin
                '            End With
                '        Else
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver)
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 2
                '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
                '                .LineStyle = XlLineStyle.xlContinuous
                '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                '                .TintAndShade = 0
                '                .Weight = XlBorderWeight.xlThin
                '            End With
                '        End If
                '        flag = Not flag
                '        iCellTemp = iRows
                '        strCellTemp = strCell
                '    End If
                'Next

                ' Set Font
                xlWorkSheet.Cells.Font.Name = "Tahoma"
                xlWorkSheet.Cells.Font.Size = 10
                'xlWorkSheet.Rows.RowHeight = 18
                xlWorkSheet.Cells.WrapText = False

                ' Set Column Autosize
                xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).EntireColumn.AutoFit()
                '(xlWorkSheet.Range("F:F").EntireColumn.ColumnWidth = 50)
                'xlWorkSheet.Range("D:D").EntireColumn.ColumnWidth = 20
                'xlWorkSheet.Range("E:F").EntireColumn.ColumnWidth = 10
                'xlWorkSheet.Range("G:G").EntireColumn.ColumnWidth = 50
                'xlWorkSheet.Range("H:H").EntireColumn.ColumnWidth = 10
                'xlWorkSheet.Range("I:K").EntireColumn.ColumnWidth = 12
                'xlWorkSheet.Range("L:L").EntireColumn.ColumnWidth = 20

                xlWorkSheet = Nothing
            Next

            '--------------------------------------------------------

            'Dim strFileName As String = fileName
            'Dim isFileOpen As Boolean = False
            Try
                Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                fileTemp.Close()
            Catch ex As Exception
                isFileOpen = True
                MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            If Not isFileOpen Then
                If System.IO.File.Exists(strFileName) Then
                    System.IO.File.Delete(strFileName)
                End If
                xlWorkBook.SaveAs(strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            End If
            isFileOpen = True
            xlApp.Visible = True

        Catch ex As Exception
            If Not xlProcess.HasExited Then
                xlProcess.Kill()
            End If
            MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")
            If Not isFileOpen AndAlso Not xlProcess.HasExited Then
                xlProcess.Kill()
            End If
            ReleaseObject(xlApp)
            ReleaseObject(xlWorkBook)
            ReleaseObject(xlWorkSheet)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal grdExport As DataGridView)
        Try
            Dim i As Integer = 0
            Dim j As Integer = 2
            Dim ExcelApp As Excel.Application
            Dim ExcelBooks As Excel.Workbook
            Dim ExcelSheets As Excel.Worksheet
            ExcelApp = New Excel.Application

            Dim CurrentThread As System.Threading.Thread
            CurrentThread = System.Threading.Thread.CurrentThread
            'CurrentThread.CurrentCulture = New CultureInfo("en-US")
            CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")

            ExcelApp.Visible = True
            ExcelBooks = ExcelApp.Workbooks.Add()
            ExcelSheets = DirectCast(ExcelBooks.Worksheets(1), Excel.Worksheet)

            i = 0
            j = 2

            With ExcelSheets
                .Columns().ColumnWidth = 22


                .Range("D" & j.ToString()).Value = "รายงานสรุปสินค้า คงเหลือ"
                .Range("D" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("D" & j.ToString()).Font.Bold = True
                .Range("D" & j.ToString()).Font.Size = 14
                '.Range("A1").Interior.Color = RGB(224, 224, 224)

                j += 1

                '.Range("B" & j.ToString()).Value = chkCustomer.Text & " : " & txtCustomer_Name.Text.ToString
                .Range("B" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("B" & j.ToString()).Font.Bold = True
                .Range("B" & j.ToString()).Font.Size = 14
                '.Range("A2").Interior.Color = RGB(224, 224, 224)
                j += 1
                Dim Col As Integer = 0
                Dim strCol As String = "A"
                Dim iChar As Integer = 65
                Dim iTwoChar As Integer = 65
                For Col = 0 To grdExport.ColumnCount - 1
                    If iTwoChar > 90 Then 'เกิน Z
                        strCol = "A" & Chr(iChar)
                    Else
                        strCol = Chr(iChar)
                    End If

                    If iTwoChar = 90 And iChar = 90 Then
                        iChar = 65
                    End If
                    If grdExport.Columns(Col).Visible = False Then Continue For
                    .Range(strCol & j.ToString()).Value = grdExport.Columns(Col).HeaderText
                    .Range(strCol & j.ToString()).Font.Color = RGB(0, 0, 0)
                    .Range(strCol & j.ToString()).Font.Size = 9
                    .Range(strCol & j.ToString()).Font.Bold = True
                    .Range(strCol & j.ToString()).Interior.Color = RGB(192, 192, 192)
                    iChar += 1
                    iTwoChar += 1
                Next
                j += 1
                Dim dtgrdExport As New System.Data.DataTable
                dtgrdExport = grdExport.DataSource
                Dim Row As Integer = 0
                For Row = 0 To grdExport.RowCount - 1
                    strCol = "A"
                    iChar = 65
                    iTwoChar = 65
                    Col = 0
                    For Col = 0 To grdExport.ColumnCount - 1
                        If iTwoChar > 90 Then 'เกิน Z
                            strCol = "A" & Chr(iChar)
                        Else
                            strCol = Chr(iChar)
                        End If
                        If iTwoChar = 90 And iChar = 90 Then
                            iChar = 65
                        End If

                        If grdExport.Columns(Col).Visible = False Then Continue For
                        Dim strData As String = ""
                        If grdExport.Rows(Row).Cells(Col).Value IsNot Nothing Then
                            strData = grdExport.Rows(Row).Cells(Col).Value.ToString
                        Else
                            strData = ""
                        End If

                        Select Case strCol
                            Case "A", "B", "C", "D", "E", "H", "K"
                                .Range(strCol & j.ToString()).Value = "'" & strData
                                .Range(strCol & j.ToString()).Font.Size = 9
                            Case Else
                                .Range(strCol & j.ToString()).Value = strData
                                .Range(strCol & j.ToString()).Font.Size = 9
                        End Select
                        iChar += 1
                        iTwoChar += 1
                    Next
                    j += 1
                Next
            End With
            ExcelSheets = Nothing
            ExcelBooks = Nothing
            ExcelApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Function IfNullObj(ByVal o As Object, Optional ByVal DefaultValue As String = "") As String
        Dim ret As String = ""
        Try
            If o Is DBNull.Value Then
                ret = DefaultValue
            Else
                ret = o.ToString
            End If
            Return ret
        Catch ex As Exception
            Return ret
        End Try
    End Function
    Private Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = String.Empty
        Dim modnum As Integer = 0

        While div > 0
            modnum = (div - 1) Mod 26
            colLetter = Chr(65 + modnum) & colLetter
            div = CInt((div - modnum) \ 26)
        End While

        Return colLetter
    End Function

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

End Class
