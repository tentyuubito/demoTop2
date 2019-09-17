Imports WMS_STD_Formula

Public Class clsSO : Inherits DBType_SQLServer

#Region " Rule Event SO "
    Public Function getReportSalsOrder_Req(Optional ByVal strWhere As String = "", Optional ByVal strSales As String = "") As DataSet
        Dim strSQL As String = ""
        Dim DS As New DataSet
        Try

            strSQL = " SELECT VSOH.DistributionCenter_Index,VSOH.WH,VSOH.WH_ID,VSOH.Sku_Index,VSOH.Sku_Id,VSOH.Sku_Name,VSOH.Package,VSOH.ItemStatus_Index"
            strSQL &= " 		,ISNULL(SUM(VSOH.Total_Qty),0) Total_Qty"
            strSQL &= " 		,SUM(ISNULL(vBAL.Qty_Bal,0)) as Qty_Bal_Real"
            strSQL &= " 		,SUM(ISNULL(vSOP.Total_Qty_Pending,0)) as Total_Qty_Pending"
            strSQL &= " 		,SUM(ISNULL(vPOP.Total_Qty_Pending_PO1,0)) as Total_Qty_Pending_PO1,SUM(ISNULL(vPOP.Total_Qty_Pending_PO2,0)) as Total_Qty_Pending_PO2"
            strSQL &= " 		,SUM(ISNULL(vPRP.Total_Qty_Pending_PR,0)) as Total_Qty_Pending_PR"
            strSQL &= " FROM (SELECT SO.DistributionCenter_Index,DC.Description as WH, DC.DistributionCenter_Id as WH_ID,  SKU.Sku_Index,SKU.Sku_Id,SKU.Str1 as Sku_Name,UNIT.Description as Package,SOI.ItemStatus_Index "
            strSQL &= " 		    ,SUM(ISNULL(SOI.Total_Qty,0)) - SUM(ISNULL(SOI.Total_Qty_Withdraw,0)) as Total_Qty"
            strSQL &= "         FROM	tb_SalesOrderItem SOI inner join"
            strSQL &= " 		        tb_SalesOrder SO ON SOI.SalesOrder_Index = SO.SalesOrder_Index  inner join"
            strSQL &= " 		        ms_SKU SKU ON SKU.Sku_Index = SOI.Sku_Index inner join"
            strSQL &= " 		        ms_Package UNIT ON UNIT.Package_Index = SKU.Package_Index inner join"
            strSQL &= " 		        ms_DocumentType DT ON DT.DocumentType_Index = SO.DocumentType_Index left outer join"
            strSQL &= " 		        ms_DistributionCenter DC ON DC.DistributionCenter_Index = SO.DistributionCenter_Index"
            strSQL &= "         WHERE   SO.Status NOT IN (-1,5)  " & strWhere
            strSQL &= " 		GROUP BY SO.DistributionCenter_Index ,DC.Description, DC.DistributionCenter_Id, SKU.Sku_Index,SKU.Sku_Id,SKU.Str1,UNIT.Description,SOI.ItemStatus_Index"
            strSQL &= " ) VSOH		"
            'SO
            strSQL &= "   left outer join"
            strSQL &= " 		(	SELECT  SUM(SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw,0)) AS Total_Qty_Pending"
            strSQL &= " 					,SOP.DistributionCenter_Index,SOIP.ItemStatus_Index,SOIP.Sku_Index"
            strSQL &= " 		    FROM    tb_salesOrderItem SOIP WITH (nolock)"
            strSQL &= " 		            inner join tb_salesOrder SOP  WITH (nolock) on SOP.SalesOrder_Index = SOIP.SalesOrder_Index "
            strSQL &= " 		            inner join ms_DocumentType DT  WITH (nolock) on DT.DocumentType_Index = SOP.DocumentType_Index "
            'strSQL &= " 		    WHERE   SOP.Status in (1,2,6)  "'ยอดขายค้าง (รอยืนยัน, รอเบิก, ค้างจ่าย)
            strSQL &= " 		    WHERE   SOP.Status in (2,6)  " 'ยอดขายค้าง (รอเบิก, ค้างจ่าย) 2018.05.23
            strSQL &= "                     AND (SOP.SalesOrder_Index NOT IN (" & strSales & "))"
            strSQL &= " 		            AND (SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw, 0)) > 0"
            'strSQL &= " 		            AND (DT.Document_Group_Name = '" & Document_Group_Name & "')"
            strSQL &= " 			GROUP BY SOP.DistributionCenter_Index,SOIP.ItemStatus_Index,SOIP.Sku_Index)as vSOP ON "
            strSQL &= " 			vSOP.DistributionCenter_Index = VSOH.DistributionCenter_Index "
            strSQL &= " 			and vSOP.Sku_Index = VSOH.Sku_Index"
            strSQL &= " 			and vSOP.ItemStatus_Index = VSOH.ItemStatus_Index"
            'BAL
            strSQL &= "   left outer join"
            strSQL &= " 		(	select isnull(SUM(LB.Qty_Bal)-SUM(LB.ReserveQty) ,0) as Qty_Bal"
            strSQL &= " 					,W.DistributionCenter_Index,LB.ItemStatus_Index,LB.Sku_Index"
            strSQL &= " 			from	tb_LocationBalance LB  inner join"
            strSQL &= " 					ms_Location L ON L.Location_Index = LB.Location_Index inner join"
            strSQL &= " 					ms_Warehouse W ON W.Warehouse_Index = L.Warehouse_Index"
            strSQL &= " 			WHERE	(LB.Qty_Bal > 0) AND (LB.Qty_Bal - LB.ReserveQty > 0)"
            strSQL &= " 			GROUP BY W.DistributionCenter_Index,LB.ItemStatus_Index,LB.Sku_Index)as vBAL ON"
            strSQL &= " 			vBAL.DistributionCenter_Index = VSOH.DistributionCenter_Index "
            strSQL &= " 			and vBAL.Sku_Index = VSOH.Sku_Index"
            strSQL &= " 			and vBAL.ItemStatus_Index = VSOH.ItemStatus_Index"
            'PO
            strSQL &= "   left outer join"
            strSQL &= " 		(	SELECT  SUM(ISNULL(Total_Qty_Pending_PO1,0)) as Total_Qty_Pending_PO1 , SUM(ISNULL(Total_Qty_Pending_PO2,0)) AS Total_Qty_Pending_PO2"
            strSQL &= " 					,DistributionCenter_Index,ItemStatus_Index,Sku_Index"
            strSQL &= " 			FROM (SELECT  CASE POP.Status WHEN 1 THEN POIP.Total_Qty ELSE 0 END as Total_Qty_Pending_PO1"
            strSQL &= " 							,CASE POP.Status WHEN 2 THEN (POIP.Total_Qty - ISNULL(POIP.Total_Received_Qty, 0)) ELSE 0 END as Total_Qty_Pending_PO2"
            strSQL &= " 							,se_User.DistributionCenter_Index,'0010000000001' as ItemStatus_Index,POIP.Sku_Index"
            strSQL &= " 					FROM    tb_PurchaseOrderItem POIP WITH (nolock)"
            strSQL &= " 							inner join tb_PurchaseOrder POP  WITH (nolock) on POIP.PurchaseOrder_Index = POP.PurchaseOrder_Index "
            strSQL &= " 							inner join se_User ON (se_User.user_id = POP.add_by or se_User.userName = POP.add_by)"
            strSQL &= " 					WHERE   POP.Status in (1,2)  "
            strSQL &= " 							AND (POIP.Total_Qty - ISNULL(POIP.Total_Received_Qty, 0)) > 0 )AS POIN GROUP BY DistributionCenter_Index,ItemStatus_Index,Sku_Index)as vPOP ON "
            strSQL &= " 			vPOP.DistributionCenter_Index = VSOH.DistributionCenter_Index "
            strSQL &= " 			and vPOP.Sku_Index = VSOH.Sku_Index"
            strSQL &= " 			and vPOP.ItemStatus_Index = VSOH.ItemStatus_Index "
            'PR
            strSQL &= "   left outer join"
            strSQL &= " 		(	SELECT  SUM(ISNULL(Total_Qty_Pending_PR,0)) as Total_Qty_Pending_PR , SUM(ISNULL(Total_Qty_Pending_PR2,0)) AS Total_Qty_Pending_PR2"
            strSQL &= " 				,DistributionCenter_Index,ItemStatus_Index,Sku_Index"
            strSQL &= " 		    FROM (SELECT  CASE PRP.Status WHEN 1 THEN PRIP.Total_Qty ELSE 0 END as Total_Qty_Pending_PR"
            strSQL &= " 						,CASE PRP.Status WHEN 2 THEN (PRIP.Total_Qty - ISNULL(PRIP.Total_Received_Qty, 0)) ELSE 0 END as Total_Qty_Pending_PR2"
            strSQL &= " 						,se_User.DistributionCenter_Index,'0010000000001' as ItemStatus_Index,PRIP.Sku_Index"
            strSQL &= " 				FROM    tb_PurchaseOrder_RequestItem PRIP WITH (nolock)"
            strSQL &= " 						inner join tb_PurchaseOrder_Request PRP  WITH (nolock) on PRIP.PurchaseOrder_Request_Index = PRP.PurchaseOrder_Request_Index "
            strSQL &= " 						inner join se_User ON (se_User.user_id = PRP.add_by or se_User.userName = PRP.add_by)"
            strSQL &= " 				WHERE   PRP.Status in (1,2)  "
            strSQL &= " 						AND (PRIP.Total_Qty - ISNULL(PRIP.Total_Received_Qty, 0)) > 0 )AS PRIN GROUP BY DistributionCenter_Index,ItemStatus_Index,Sku_Index  )as vPRP ON "
            strSQL &= " 			vPRP.DistributionCenter_Index = VSOH.DistributionCenter_Index "
            strSQL &= " 			and vPRP.Sku_Index = VSOH.Sku_Index"
            strSQL &= " 			and vPRP.ItemStatus_Index = VSOH.ItemStatus_Index  "

            strSQL &= " GROUP BY VSOH.DistributionCenter_Index,VSOH.WH,VSOH.WH_ID,VSOH.Sku_Index,VSOH.Sku_Id,VSOH.Sku_Name,VSOH.Package,VSOH.ItemStatus_Index"
            strSQL &= " HAVING (ISNULL(SUM(VSOH.Total_Qty),0) + SUM(ISNULL(vSOP.Total_Qty_Pending,0))) > SUM(ISNULL(vBAL.Qty_Bal,0))"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Dim _dt As DataTable
            _dt = GetDataTable



            'strSQL &= " SELECT (Qty_Bal_Real - Total_Qty_Pending) as  Qty_Bal,* "
            'strSQL &= " FROM("
            'strSQL &= " SELECT DC.Description as WH, DC.DistributionCenter_Id as WH_ID,  SKU.Sku_Index,SKU.Sku_Id,SKU.Str1 as Sku_Name,UNIT.Description as Package,SOI.ItemStatus_Index "
            'strSQL &= " 		,SUM(ISNULL(SOI.Total_Qty,0)) - SUM(ISNULL(SOI.Total_Qty_Withdraw,0)) as Total_Qty"
            'strSQL &= " 		,SUM(ISNULL(vBAL.Qty_Bal,0)) as Qty_Bal_Real"
            'strSQL &= " 		,SUM(ISNULL(vSOP.Total_Qty_Pending,0)) as Total_Qty_Pending"
            'strSQL &= " 		,SUM(ISNULL(vPOP.Total_Qty_Pending_PO1,0)) as Total_Qty_Pending_PO1,SUM(ISNULL(vPOP.Total_Qty_Pending_PO2,0)) as Total_Qty_Pending_PO2"
            'strSQL &= " 		,SUM(ISNULL(vPRP.Total_Qty_Pending_PR,0)) as Total_Qty_Pending_PR"
            'strSQL &= " from	tb_SalesOrderItem SOI inner join"
            'strSQL &= " 		tb_SalesOrder SO ON SOI.SalesOrder_Index = SO.SalesOrder_Index  inner join"
            'strSQL &= " 		ms_SKU SKU ON SKU.Sku_Index = SOI.Sku_Index inner join"
            'strSQL &= " 		ms_Package UNIT ON UNIT.Package_Index = SKU.Package_Index inner join"
            'strSQL &= " 		ms_DocumentType DT ON DT.DocumentType_Index = SO.DocumentType_Index left outer join"
            'strSQL &= " 		ms_DistributionCenter DC ON DC.DistributionCenter_Index = SO.DistributionCenter_Index left outer join"
            ''SO
            'strSQL &= " 		(	SELECT  SUM(SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw,0)) AS Total_Qty_Pending"
            'strSQL &= " 					,SOP.DistributionCenter_Index,SOIP.ItemStatus_Index,SOIP.Sku_Index"
            'strSQL &= " 		    FROM    tb_salesOrderItem SOIP WITH (nolock)"
            'strSQL &= " 		            inner join tb_salesOrder SOP  WITH (nolock) on SOP.SalesOrder_Index = SOIP.SalesOrder_Index "
            'strSQL &= " 		    WHERE   SOP.Status in (1,2,6)  "
            ''strSQL &= " 		    WHERE   SOP.Status in (2,6)  "
            'strSQL &= "                     AND (SOP.SalesOrder_Index NOT IN (" & strSales & "))"
            'strSQL &= " 		            AND (SOIP.Total_Qty - ISNULL(SOIP.Total_Qty_Withdraw, 0)) > 0"
            'strSQL &= " 			GROUP BY SOP.DistributionCenter_Index,SOIP.ItemStatus_Index,SOIP.Sku_Index)as vSOP ON "
            'strSQL &= " 			vSOP.DistributionCenter_Index = SO.DistributionCenter_Index "
            'strSQL &= " 			and vSOP.Sku_Index = SKU.Sku_Index"
            'strSQL &= " 			and vSOP.ItemStatus_Index = SOI.ItemStatus_Index  left outer join"
            ''BAL
            'strSQL &= " 		(	select isnull(SUM(LB.Qty_Bal)-SUM(LB.ReserveQty) ,0) as Qty_Bal"
            'strSQL &= " 					,W.DistributionCenter_Index,LB.ItemStatus_Index,LB.Sku_Index"
            'strSQL &= " 			from	tb_LocationBalance LB  inner join"
            'strSQL &= " 					ms_Location L ON L.Location_Index = LB.Location_Index inner join"
            'strSQL &= " 					ms_Warehouse W ON W.Warehouse_Index = L.Warehouse_Index"
            'strSQL &= " 			WHERE	(LB.Qty_Bal > 0) AND (LB.Qty_Bal - LB.ReserveQty > 0)"
            'strSQL &= " 			GROUP BY W.DistributionCenter_Index,LB.ItemStatus_Index,LB.Sku_Index)as vBAL ON"
            'strSQL &= " 			vBAL.DistributionCenter_Index = SO.DistributionCenter_Index "
            'strSQL &= " 			and vBAL.Sku_Index = SKU.Sku_Index"
            'strSQL &= " 			and vBAL.ItemStatus_Index = SOI.ItemStatus_Index  left outer join"
            ''PO
            'strSQL &= " 		(	SELECT  SUM(ISNULL(Total_Qty_Pending_PO1,0)) as Total_Qty_Pending_PO1 , SUM(ISNULL(Total_Qty_Pending_PO2,0)) AS Total_Qty_Pending_PO2"
            'strSQL &= " 					,DistributionCenter_Index,ItemStatus_Index,Sku_Index"
            'strSQL &= " 			FROM (SELECT  CASE POP.Status WHEN 1 THEN POIP.Total_Qty ELSE 0 END as Total_Qty_Pending_PO1"
            'strSQL &= " 							,CASE POP.Status WHEN 2 THEN (POIP.Total_Qty - ISNULL(POIP.Total_Received_Qty, 0)) ELSE 0 END as Total_Qty_Pending_PO2"
            'strSQL &= " 							,se_User.DistributionCenter_Index,'0010000000001' as ItemStatus_Index,POIP.Sku_Index"
            'strSQL &= " 					FROM    tb_PurchaseOrderItem POIP WITH (nolock)"
            'strSQL &= " 							inner join tb_PurchaseOrder POP  WITH (nolock) on POIP.PurchaseOrder_Index = POP.PurchaseOrder_Index "
            'strSQL &= " 							inner join se_User ON (se_User.user_id = POP.add_by or se_User.userName = POP.add_by)"
            'strSQL &= " 					WHERE   POP.Status in (1,2)  "
            'strSQL &= " 							AND (POIP.Total_Qty - ISNULL(POIP.Total_Received_Qty, 0)) > 0 )AS POIN GROUP BY DistributionCenter_Index,ItemStatus_Index,Sku_Index)as vPOP ON "
            'strSQL &= " 			vPOP.DistributionCenter_Index = SO.DistributionCenter_Index "
            'strSQL &= " 			and vPOP.Sku_Index = SKU.Sku_Index"
            'strSQL &= " 			and vPOP.ItemStatus_Index = SOI.ItemStatus_Index    left outer join"
            ''PR
            'strSQL &= " 		(	SELECT  SUM(ISNULL(Total_Qty_Pending_PR,0)) as Total_Qty_Pending_PR , SUM(ISNULL(Total_Qty_Pending_PR2,0)) AS Total_Qty_Pending_PR2"
            'strSQL &= " 				,DistributionCenter_Index,ItemStatus_Index,Sku_Index"
            'strSQL &= " 		    FROM (SELECT  CASE PRP.Status WHEN 1 THEN PRIP.Total_Qty ELSE 0 END as Total_Qty_Pending_PR"
            'strSQL &= " 						,CASE PRP.Status WHEN 2 THEN (PRIP.Total_Qty - ISNULL(PRIP.Total_Received_Qty, 0)) ELSE 0 END as Total_Qty_Pending_PR2"
            'strSQL &= " 						,se_User.DistributionCenter_Index,'0010000000001' as ItemStatus_Index,PRIP.Sku_Index"
            'strSQL &= " 				FROM    tb_PurchaseOrder_RequestItem PRIP WITH (nolock)"
            'strSQL &= " 						inner join tb_PurchaseOrder_Request PRP  WITH (nolock) on PRIP.PurchaseOrder_Request_Index = PRP.PurchaseOrder_Request_Index "
            'strSQL &= " 						inner join se_User ON (se_User.user_id = PRP.add_by or se_User.userName = PRP.add_by)"
            'strSQL &= " 				WHERE   PRP.Status in (1,2)  "
            'strSQL &= " 						AND (PRIP.Total_Qty - ISNULL(PRIP.Total_Received_Qty, 0)) > 0 )AS PRIN GROUP BY DistributionCenter_Index,ItemStatus_Index,Sku_Index  )as vPRP ON "
            'strSQL &= " 			vPRP.DistributionCenter_Index = SO.DistributionCenter_Index "
            'strSQL &= " 			and vPRP.Sku_Index = SKU.Sku_Index"
            'strSQL &= " 			and vPRP.ItemStatus_Index = SOI.ItemStatus_Index  "

            'strSQL &= " where   SO.Status NOT IN (-1,5)  " & strWhere
            'strSQL &= " Group by SKU.Sku_Index,SKU.Sku_id,SKU.Str1,UNIT.Description ,SOI.ItemStatus_Index"
            'strSQL &= " 	,DC.Description , DC.DistributionCenter_Id,SO.DistributionCenter_Index"
            'strSQL &= " )AS V WHERE (Qty_Bal_Real-Total_Qty_Pending) < Total_Qty"




            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()
            'Dim _dt As DataTable
            '_dt = GetDataTable

            DS.Tables.Add(_dt)

            Return DS

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getSODetail(ByVal SalesOrder_Index As String, Optional ByVal Copy_Red As Boolean = False, Optional ByVal Copy_All As Boolean = False) As DataTable
        '  
        Dim strSQL As String = ""

        Try

            strSQL = "SELECT * FROM VIEW_RPT_SODetail"
            strSQL &= " WHERE  SalesOrder_Index ='" & SalesOrder_Index & "' "
            'If Copy_Red = True And Copy_All = False Then
            '    strSQL &= " AND  RGB_Check = 2 "
            '    'ElseIf Copy_Red = True And Copy_All = True Then
            'End If
            strSQL = strSQL & " Order by Item_Seq"

            Dim _dt As DataTable
            _dt = DBExeQuery(strSQL)

            Return _dt

            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()
            '_dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function canCloseSO(ByVal SalesOrder_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            Dim _dt As DataTable
            strSQL = " select ms_ProcessStatus.Description as [Status_Desc] from tb_SalesOrder inner join ms_ProcessStatus on ms_ProcessStatus.Status=tb_SalesOrder.Status and ms_ProcessStatus.Process_Id=10 where tb_SalesOrder.SalesOrder_Index=@SalesOrder_Index and tb_SalesOrder.Status not in (2,6) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException(String.Format("ไม่สามารถปิดเอกสารได้, สถานะเอกสาร{0}", _dt.Rows(0).Item("Status_Desc").ToString()))
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function matchSO_Withdraw(ByVal SalesOrder_Index As String) As String
        Dim strSQL As String = ""
        Try
            Dim _dt As DataTable
            strSQL = " select isnull(sum(Total_Qty-Total_Qty_Withdraw),0) as [Total_Qty] from tb_SalesOrderItem where tb_SalesOrderItem.SalesOrder_Index=@SalesOrder_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                If (CDec(_dt.Rows(0).Item("Total_Qty")) > 0) Then
                    Return String.Format("พบสินค้าที่ยังไม่ได้ทำการเบิกจำนวน {0}", CDec(_dt.Rows(0).Item("Total_Qty")).ToString("#,##0.######"))
                End If
            Else
                Throw New ApplicationException(String.Format("ไม่พบเอกสาร"))
            End If
            Return ""
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function canConfirmSO(ByVal SalesOrder_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            Dim _dt As DataTable
            strSQL = " select tb_SalesOrder.SalesOrder_No,ms_ProcessStatus.Description as [Status_Desc] from tb_SalesOrder inner join ms_ProcessStatus on ms_ProcessStatus.Status=tb_SalesOrder.Status and ms_ProcessStatus.Process_Id=10 where tb_SalesOrder.SalesOrder_Index=@SalesOrder_Index and tb_SalesOrder.Status not in (1) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException(String.Format("ไม่สามารถยืนยันเอกสารเลขที่ {0} ได้, สถานะเอกสาร{1}", _dt.Rows(0).Item("SalesOrder_No").ToString(), _dt.Rows(0).Item("Status_Desc").ToString()))
            End If
            'strSQL = " select SalesOrder_No from tb_SalesOrder where SalesOrder_Index=@SalesOrder_Index and isnull(TransportRegion_Index,'0010000000000')='0010000000000' "
            'dhong Edit
            strSQL = " select SalesOrder_No from tb_SalesOrder where SalesOrder_Index=@SalesOrder_Index and isnull(DistributionCenter_Index,'0010000000000')='0010000000000' "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException(String.Format("ไม่สามารถยืนยันเอกสารเลขที่ {0} ได้, ไม่พบศูนย์กระจายสินค้า", _dt.Rows(0).Item("SalesOrder_No").ToString()))
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region " Event SO "

    Public Function closeSO(ByVal SalesOrder_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_SalesOrder set "
            strSQL &= "Status=3, "
            strSQL &= "update_by=@update_by, "
            strSQL &= "update_date=getdate(), "
            strSQL &= "update_branch=@update_branch "
            strSQL &= "where SalesOrder_Index=@SalesOrder_Index "
            strSQL &= "and Status in (2,6) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index
                .Add("@update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function closeSO(ByVal SalesOrder_Index As String, ByVal SalesOrder_No As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_SalesOrder set "
            strSQL &= "Status=3, "
            strSQL &= "update_by=@update_by, "
            strSQL &= "update_date=getdate(), "
            strSQL &= "update_branch=@update_branch "
            strSQL &= "where SalesOrder_Index=@SalesOrder_Index "
            strSQL &= "and Status in (2,6) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = SalesOrder_Index
                .Add("@update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            myTrans.Commit()


            'insert log
            Dim obj_cls As New cls_syAditlog
            obj_cls.Process_ID = 10
            obj_cls.Description = "ยืนยันเอกสาร"
            obj_cls.Document_Index = SalesOrder_Index
            obj_cls.Document_No = SalesOrder_No
            obj_cls.Log_Type_ID = 1003 '152 : Close SO
            obj_cls.Insert_Master()


            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function GetSO_Replenish(ByVal ListSalesOrderIndex() As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As DataTable
        Try
            Dim SalesOrderIndex As String = String.Join("', '", ListSalesOrderIndex)

            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" DECLARE @SO_DATA TABLE (Sku_Index CHAR(13), ItemStatus_Index CHAR(13), PLot NVARCHAR(100), ERP_Location NVARCHAR(100), Total_Qty DECIMAL(19, 6)) ")
                .Append(" INSERT INTO @SO_DATA ")
                .Append(" (Sku_Index, ItemStatus_Index, PLot, ERP_Location, Total_Qty) ")
                .Append(" SELECT Sku_Index, ItemStatus_Index, ISNULL(PLot, '') AS PLot ")
                .Append("      , ISNULL(ERP_Location, '') AS ERP_Location ")
                .Append(" 	 , SUM(Total_Qty) - SUM(ISNULL(Total_Qty_Withdraw, 0)) AS Total_Qty ")
                .Append(" FROM tb_SalesOrderItem (NOLOCK) ")
                .Append(" WHERE Status <> -1 ")
                .Append(" AND SalesOrder_Index IN ('" & SalesOrderIndex & "') ")
                .Append(" GROUP BY Sku_Index, ItemStatus_Index, ISNULL(PLot, ''), ISNULL(ERP_Location, '') ")
                .Append(" SELECT SO.Sku_Index, sku.Sku_Id ")
                .Append(" 	   , SO.ItemStatus_Index, ist.Description ")
                .Append("      , SO.PLot, SO.ERP_Location, SO.Total_Qty ")
                .Append("      , (ISNULL(LB.Total_Qty, 0) + ISNULL(TFS.Total_Qty_Transfer, 0)) AS Total_Qty_Balance ")
                .Append("      , (SO.Total_Qty - (ISNULL(LB.Total_Qty, 0) + ISNULL(TFS.Total_Qty_Transfer, 0))) AS Total_Qty_Require ")
                .Append("      , sku.Location_Index ")
                .Append(" FROM ( ")
                .Append(" 	SELECT Sku_Index, ItemStatus_Index, PLot, ERP_Location, Total_Qty ")
                .Append(" 	FROM @SO_DATA ")
                .Append(" ) SO ")
                .Append(" LEFT JOIN ( ")
                .Append(" 	SELECT lb.Sku_Index, lb.ItemStatus_Index, ISNULL(lb.PLot, '') AS PLot ")
                .Append(" 		 , ISNULL(lb.ERP_Location, '') AS ERP_Location ")
                .Append(" 	     , SUM(lb.Qty_Bal) AS Total_Qty ")
                .Append(" 	FROM tb_LocationBalance lb (NOLOCK) ")
                .Append(" 	INNER JOIN ms_Location lo ")
                .Append(" 	ON lb.Location_Index = lo.Location_Index ")
                .Append(" 	WHERE Sku_Index IN ( ")
                .Append(" 		SELECT Sku_Index ")
                .Append(" 		FROM @SO_DATA ")
                .Append(" 		GROUP BY Sku_Index ")
                .Append(" 	) ")
                .Append(" 	AND lo.LocationType_Index = 2 ")
                .Append(" 	GROUP BY lb.Sku_Index, lb.ItemStatus_Index, ISNULL(lb.PLot, '') ")
                .Append(" 		   , ISNULL(lb.ERP_Location, '') ")
                .Append(" ) LB ")
                .Append(" ON SO.Sku_Index = LB.Sku_Index ")
                .Append(" AND SO.ItemStatus_Index = LB.ItemStatus_Index ")
                .Append(" AND SO.PLot = LB.PLot ")
                .Append(" AND SO.ERP_Location = LB.ERP_Location ")
                .Append(" LEFT JOIN  ( ")
                .Append(" 	SELECT tsl.Sku_Index, tsl.New_ItemStatus_Index AS ItemStatus_Index, ISNULL(tsl.PLot, '') AS PLot ")
                .Append(" 		 , ISNULL(tsl.ERP_Location_TO, '') AS ERP_Location ")
                .Append(" 		 , SUM(Qty) AS Total_Qty_Transfer ")
                .Append(" 	FROM tb_TransferStatus ts ")
                .Append(" 	INNER JOIN tb_TransferStatusLocation tsl ")
                .Append(" 	ON ts.TransferStatus_Index = tsl.TransferStatus_Index ")
                .Append(" 	INNER JOIN ms_Location lo ")
                .Append(" 	ON tsl.To_Location_Index = lo.Location_Index ")
                .Append(" 	WHERE ts.Status <> -1 ")
                .Append(" 	AND tsl.Status <> -1 ")
                .Append(" 	AND tsl.Sku_Index IN ( ")
                .Append(" 		SELECT Sku_Index ")
                .Append(" 		FROM @SO_DATA ")
                .Append(" 		GROUP BY Sku_Index ")
                .Append(" 	) ")
                .Append(" 	AND lo.LocationType_Index = 2 ")
                .Append(" 	GROUP BY tsl.Sku_Index, tsl.New_ItemStatus_Index, ISNULL(tsl.PLot, ''), ISNULL(tsl.ERP_Location_TO, '') ")
                .Append(" ) TFS ")
                .Append(" ON SO.Sku_Index  = TFS.Sku_Index ")
                .Append(" AND SO.ItemStatus_Index = TFS.ItemStatus_Index ")
                .Append(" AND SO.PLot = TFS.PLot ")
                .Append(" AND SO.ERP_Location = TFS.ERP_Location ")
                .Append(" INNER JOIN ms_SKU sku (NOLOCK) ON SO.Sku_Index = sku.Sku_Index ")
                .Append(" INNER JOIN ms_ItemStatus ist (NOLOCK) ON SO.ItemStatus_Index = ist.ItemStatus_Index ")
                .Append(" WHERE SO.Total_Qty > (ISNULL(LB.Total_Qty, 0) + ISNULL(TFS.Total_Qty_Transfer, 0)) ")
            End With

            Dim SO_Data As DataTable

            If Transaction Is Nothing Then
                SO_Data = DBExeQuery(SQL.ToString)
            Else
                SO_Data = DBExeQuery(SQL.ToString, Transaction.Connection, Transaction)
            End If

            Return SO_Data

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Sub CheckSOGroupProductType(ByVal SalesOrderList() As String)
        Try
            Dim strSQL As String = ""
            Dim lstSalesOrder As String = ""
            For Each soIndex As String In SalesOrderList
                lstSalesOrder &= "'" & soIndex & "',"
            Next
            lstSalesOrder = IIf(lstSalesOrder.Length > 0, lstSalesOrder.Substring(0, lstSalesOrder.Length - 1), "")
            strSQL = " SELECT p.ProductType_Index "
            strSQL &= " FROM tb_SalesOrderItem soi (NOLOCK) "
            strSQL &= " INNER JOIN tb_SalesOrder so (NOLOCK) ON soi.SalesOrder_Index = so.SalesOrder_Index "
            strSQL &= " INNER JOIN ms_SKU sku (NOLOCK) ON soi.Sku_Index = sku.Sku_Index "
            strSQL &= " LEFT JOIN ms_Product p (NOLOCK) ON sku.Product_Index = p.Product_Index "
            strSQL &= " WHERE so.[Status] <> -1  "
            strSQL &= " AND so.SalesOrder_Index IN (" & lstSalesOrder & ")  "
            strSQL &= " GROUP BY p.ProductType_Index "

            Dim _dt As DataTable
            _dt = DBExeQuery(strSQL)

            If _dt.Rows.Count > 1 Then
                Throw New ApplicationException(String.Format("มีกลุ่มสินค้ามากกว่า 1 กลุ่ม"))
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function GetProductType() As DataTable

        Dim strSQL As String = ""

        Try
            strSQL = " SELECT ProductType_Index,ProductType_Id,[Description] FROM ms_ProductType (NOLOCK) "
            strSQL &= " WHERE(status_id <> -1) "

            Dim _dt As DataTable = DBExeQuery(strSQL)

            Return _dt
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function updateStatusAndSoType(ByVal so_type As String, ByVal strWhereSalesOrder As String) As Integer

        Dim strSQL As String = ""

        Try
            strSQL = "  update tb_salesOrder set so_type = '" & so_type & "' , status = 1 "
            strSQL &= " WHERE salesOrder_Index = '" & strWhereSalesOrder & "' and  status = 2 and So_Type = 'X' "

            Return DBExeNonQuery(strSQL)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


#End Region


    Public Function GetSalesOrder(ByVal SalesOrder_Index As String) As DataTable
        Try
            Dim SQL As New System.Text.StringBuilder
            With SQL
                .Append(" SELECT so.SalesOrder_No, so.SalesOrder_Index, soi.Sku_Index, soi.ItemStatus_Index, soi.Total_Qty ")
                .Append("        , CASE WHEN ISNULL(pc.Str1, '') = 'Z' THEN 1 ELSE 0 END IsCaseZ, sku.sku_id  ")
                .Append(" FROM tb_SalesOrder so  ")
                .Append(" INNER JOIN ( ")
                .Append("     SELECT SalesOrder_Index, Sku_Index, ItemStatus_Index ")
                .Append("          , SUM(Total_Qty) - SUM(ISNULL(Total_Qty_Withdraw, 0)) AS Total_Qty ")
                .Append("     FROM tb_SalesOrderItem ")
                .Append("     WHERE Status <> -1 ")
                .Append("     AND Total_Qty - ISNULL(Total_Qty_Withdraw, 0) > 0 ")
                .Append("     GROUP BY SalesOrder_Index, Sku_Index, ItemStatus_Index ")
                .Append(" ) soi ")
                .Append(" ON so.SalesOrder_Index = soi.SalesOrder_Index ")
                .Append(" LEFT JOIN ms_SKU sku ")
                .Append(" ON soi.Sku_Index = sku.Sku_Index ")
                .Append(" LEFT JOIN ms_SKU sku2 ")
                .Append(" ON sku.Str6 = sku2.Sku_Id ")
                .Append(" LEFT JOIN ms_Product_Class pc ")
                .Append(" ON sku2.ProductClass_Index = pc.ProductClass_Index ")
                .Append(" WHERE so.Status = 1 AND so.SalesOrder_Index = '" & SalesOrder_Index & "'")
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
                .Append("     SELECT Sku_Index, ItemStatus_Index ")
                .Append("          , SUM(ISNULL(Qty_Bal,0)) - SUM(ISNULL(ReserveQty,0)) AS Total_Qty ")
                .Append("     FROM tb_LocationBalance WITH(NOLOCK) ")
                .Append("     WHERE Qty_Bal > 0 ")
                .Append("     AND ReserveQty >= 0 ")
                .Append("    AND Qty_Bal - ReserveQty > 0 ")
                .Append("    GROUP BY Sku_Index, ItemStatus_Index ")
                .Append(" ) BL ")

                .Append(" LEFT JOIN ( ")
                .Append("     SELECT soi.Sku_Index, soi.ItemStatus_Index ")
                .Append("          , SUM(ISNULL(soi.Total_Qty,0)) - SUM(ISNULL(soi.Total_Qty_Withdraw,0)) AS Total_Qty ")
                .Append("     FROM tb_SalesOrder so WITH(NOLOCK) ")
                .Append("     INNER JOIN tb_SalesOrderItem soi WITH(NOLOCK) ")
                .Append("     ON so.SalesOrder_Index = soi.SalesOrder_Index ")
                .Append("     WHERE so.Status NOT IN (-1, 1, 3) ")
                .Append("     AND soi.Status <> -1 ")
                .Append("     GROUP BY soi.Sku_Index, soi.ItemStatus_Index ")
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

End Class
