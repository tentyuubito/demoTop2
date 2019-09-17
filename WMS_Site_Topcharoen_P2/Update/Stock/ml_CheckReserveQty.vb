Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module


Public Class ml_CheckReserveQty : Inherits DBType_SQLServer

    Private _dataTable As DataTable
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

#Region "Select Data"
    Public Sub getReserveAll(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL &= " SELECT *"
            'strSQL &= " , ReserveQty - (isnull(Total_QtyW,0) + isnull(Total_QtyTr,0)   "
            strSQL &= " ,  (isnull(Total_QtyW,0) + isnull(Total_QtyTr,0)   "
            strSQL &= " 	+ isnull(Total_QtyTo,0)  + isnull(Total_QtyTc,0)   "
            strSQL &= " 	+ isnull(Total_QtyTb,0) + isnull(Total_QtyTbr,0))   as ReserveQtyAll"
            strSQL &= " FROM ("
            strSQL &= " SELECT tlb.Tag_No,tlb.LocationBalance_Index,ml.Location_Alias,ms.Sku_Id,tlb.Qty_Bal,tlb.ReserveQty"
            strSQL &= " ,(SELECT SUM(twil.Total_Qty	) FROM dbo.tb_WithdrawItemLocation twil"
            strSQL &= " 	INNER JOIN dbo.tb_Withdraw tw	ON tw.Withdraw_Index = twil.Withdraw_Index	"
            strSQL &= " 	AND twil.LocationBalance_Index	= tlb.LocationBalance_Index AND tw.Status	NOT IN  (-1,2)) AS Total_QtyW"
            strSQL &= " ,(SELECT SUM(ttsl.Total_Qty	) FROM dbo.tb_TransferStatusLocation ttsl	"
            strSQL &= " 	INNER JOIN dbo.tb_TransferStatus tts		ON tts.TransferStatus_Index = ttsl.TransferStatus_Index		"
            strSQL &= " 	AND ttsl.from_LocationBalance_Index	= tlb.LocationBalance_Index AND tts.Status	NOT IN  (-1,2)) AS Total_QtyTr"
            strSQL &= " ,(SELECT SUM(ttsl.Total_Qty	) FROM dbo.tb_TransferOwnerLocation ttsl	"
            strSQL &= " 	INNER JOIN dbo.tb_TransferOwner tts		ON tts.TransferOwner_Index = ttsl.TransferOwner_Index 	"
            strSQL &= " 	AND ttsl.from_LocationBalance_Index	= tlb.LocationBalance_Index AND tts.Status	NOT IN  (-1,2)) AS Total_QtyTo"
            strSQL &= " ,(SELECT SUM(ttsl.Total_Qty	) FROM dbo.tb_TransferCodeLocation ttsl	"
            strSQL &= " 	INNER JOIN dbo.tb_TransferCode tts		ON tts.TransferCode_Index = ttsl.TransferCode_Index 	"
            strSQL &= " 	AND ttsl.from_LocationBalance_Index	= tlb.LocationBalance_Index AND tts.Status	NOT IN  (-1,2)) AS Total_QtyTc"
            strSQL &= " ,(SELECT SUM(ttsl.Total_Qty	) FROM dbo.tb_BorrowLocation ttsl	"
            strSQL &= " 	INNER JOIN dbo.tb_Borrow tts		ON tts.Borrow_Index = ttsl.Borrow_Index 	"
            strSQL &= " 	AND ttsl.from_LocationBalance_Index	= tlb.LocationBalance_Index AND tts.Status	NOT IN  (-1,3)) AS Total_QtyTb"
            strSQL &= " ,(SELECT SUM(ttsl.Total_Qty	) FROM dbo.tb_BorrowReturnLocation	 ttsl	"
            strSQL &= " 	INNER JOIN dbo.tb_BorrowReturn tts		ON tts.BorrowReturn_Index = ttsl.BorrowReturn_Index 	"
            strSQL &= " 	AND ttsl.from_LocationBalance_Index	= tlb.LocationBalance_Index AND tts.Status	NOT IN  (-1,2)) AS Total_QtyTbr"

            strSQL &= " FROM dbo.tb_LocationBalance tlb	"
            strSQL &= " 	LEFT OUTER JOIN	dbo.ms_SKU ms	ON ms.Sku_Index = tlb.Sku_Index"
            strSQL &= " 	LEFT OUTER	JOIN dbo.ms_Location ml	ON ml.Location_Index = tlb.Location_Index"
            strSQL &= " WHERE tlb.SKU_Index	 = '" & SKU_Index & "'"
            strSQL &= " ) AS LB WHERE "

            strSQL &= "  ReserveQty <> (isnull(Total_QtyW,0) + isnull(Total_QtyTr,0)   "
            strSQL &= " 	+ isnull(Total_QtyTo,0)  + isnull(Total_QtyTc,0)   "
            strSQL &= " 	+ isnull(Total_QtyTb,0) + isnull(Total_QtyTbr,0))"

            'strSQL &= "  (ReserveQty - (isnull(Total_QtyW,0) + isnull(Total_QtyTr,0)   "
            'strSQL &= " 	+ isnull(Total_QtyTo,0)  + isnull(Total_QtyTc,0)   "
            'strSQL &= " 	+ isnull(Total_QtyTb,0) + isnull(Total_QtyTbr,0)))  > 0"


            strSQL = strSQL & Strwhere
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

    Public Sub UPDATE_RESERVE_LOCATIONBALANCE(ByVal LocationBalace_Idex As String, ByVal Total_Qty As Double)
        Try
            Dim strSQL As String = ""
            strSQL &= " DECLARE	@LocationBalace_Idex varchar(13)"
            strSQL &= " DECLARE @Total_Qty float	"

            strSQL &= " SET @LocationBalace_Idex = '" & LocationBalace_Idex & "'"
            strSQL &= " SET @Total_Qty = " & Total_Qty

            strSQL &= " UPDATE dbo.tb_LocationBalance"
            strSQL &= " SET"
            strSQL &= "    dbo.tb_LocationBalance.ReserveQty = @Total_Qty, "
            strSQL &= "     dbo.tb_LocationBalance.ReserveWeight = ((dbo.tb_LocationBalance.Weight_Bal_Begin	/ dbo.tb_LocationBalance.Qty_Bal_Begin)	 * @Total_Qty), "
            strSQL &= "     dbo.tb_LocationBalance.ReserveVolume = ((dbo.tb_LocationBalance.Volume_Bal_Begin	/ dbo.tb_LocationBalance.Qty_Bal_Begin)	 * @Total_Qty), "
            strSQL &= "     dbo.tb_LocationBalance.ReserveQty_Item = ((dbo.tb_LocationBalance.Qty_Item_Begin	/ dbo.tb_LocationBalance.Qty_Bal_Begin)	 * @Total_Qty), "
            strSQL &= "     dbo.tb_LocationBalance.ReserveOrderItem_Price =  ((dbo.tb_LocationBalance.OrderItem_Price_Begin	/ dbo.tb_LocationBalance.Qty_Bal_Begin)	 * @Total_Qty) "
            strSQL &= String.Format("     ,ClearReserve_date = getdate(),ClearReserve_by = '{0}'", WV_UserName)
            strSQL &= " WHERE dbo.tb_LocationBalance.LocationBalance_Index = @LocationBalace_Idex "

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

    Public Sub getTotal_ReserveQty_System(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = " select IsNull(sum(ReserveQty),0)  as Total_ReserveQty from tb_locationbalance "
            strSQL &= " where sku_index = '" & SKU_Index & "'"

            strSQL = strSQL & Strwhere
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub getWithDraw(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = " select	W.WithDraw_Index,Doc.Description as DocumentType_WithDraw,W.WithDraw_No,W.WithDraw_Date,SUM(WIL.Total_Qty)   AS Total_Qty "
            strSQL &= " from	tb_WithdrawItemLocation WIL inner join"
            strSQL &= " tb_Withdraw W ON W.Withdraw_index = WIL.Withdraw_index"
            strSQL &= " Left join ms_DocumentType Doc on W.DocumentType_Index = Doc.DocumentType_Index"
            strSQL &= " where	WIL.LocationBalance_index in (select LocationBalance_index from tb_locationbalance  "
            strSQL &= " where   sku_index = '" & SKU_Index & "')"
            strSQL &= " and W.Status NOT IN(-1,2) "
            strSQL &= " group by W.WithDraw_Index,Doc.Description ,W.WithDraw_No,W.WithDraw_Date "

            strSQL = strSQL & Strwhere
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
    Public Sub getTransferStatus(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = "  select	T.TransferStatus_Index,Doc.Description as DocumentType_TransferStatus,T.TransferStatus_No,T.TransferStatus_Date, TSL.Total_Qty "
            strSQL &= " from	tb_TransferStatusLocation TSL inner join"
            strSQL &= " tb_TransferStatus T ON T.TransferStatus_Index = TSL.TransferStatus_Index"
            strSQL &= "	Left join ms_DocumentType Doc on T.DocumentType_Index = Doc.DocumentType_Index"
            strSQL &= " where	TSL.From_LocationBalance_Index in (select LocationBalance_index from tb_locationbalance  "
            strSQL &= " where   sku_index = '" & SKU_Index & "')"
            strSQL &= " and T.Status NOT IN(-1,2) "

            strSQL = strSQL & Strwhere
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
    Public Sub getTransferOwner(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = " select	T.TransferOwner_Index,Doc.Description as DocumentType_TransferOwner,T.TransferOwner_No,T.TransferOwner_Date,TOL.Total_Qty  "
            strSQL &= " from    tb_TransferOwnerLocation TOL"
            strSQL &= "         inner join tb_TransferOwner T on TOL.TransferOwner_Index = T.TransferOwner_Index"
            strSQL &= "         Left join ms_DocumentType Doc on T.DocumentType_Index = Doc.DocumentType_Index"
            strSQL &= " where	TOL.From_LocationBalance_Index in (select LocationBalance_index from tb_locationbalance  "
            strSQL &= " where   sku_index = '" & SKU_Index & "')"
            strSQL &= "         and T.Status = 1"

            strSQL = strSQL & Strwhere
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
    Public Sub getTransferCode(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try


            strSQL = " select	T.TransferCode_Index,Doc.Description as DocumentType_TransferCode,T.TransferCode_No,T.TransferCode_Date,TCL.Total_Qty "
            strSQL &= " from    tb_TransferCodeLocation TCL"
            strSQL &= "         inner join tb_TransferCode T on TCL.TransferCode_Index = T.TransferCode_Index"
            strSQL &= "         Left join ms_DocumentType Doc on T.DocumentType_Index = Doc.DocumentType_Index"
            strSQL &= " where	TCL.From_LocationBalance_Index in (select LocationBalance_index from tb_locationbalance  "
            strSQL &= " where   sku_index = '" & SKU_Index & "')"
            strSQL &= " and T.Status = 1		"

            strSQL = strSQL & Strwhere
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
    Public Sub getBorrow(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = " select	B.Borrow_Index,Doc.Description as DocumentType_Borrow,B.Borrow_No,B.Borrow_Date,Bl.Total_Qty  "
            strSQL &= " from	tb_BorrowLocation Bl"
            strSQL &= "         inner join tb_Borrow B on Bl.Borrow_Index = B.Borrow_Index"
            strSQL &= "         Left join ms_DocumentType Doc on B.DocumentType_Index = Doc.DocumentType_Index"
            strSQL &= " where	Bl.From_LocationBalance_Index  in (select LocationBalance_index from tb_locationbalance  "
            strSQL &= " where   sku_index = '" & SKU_Index & "')"
            strSQL &= " and B.Status = 1		"


            strSQL = strSQL & Strwhere
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
    Public Sub getBorrowReturn(ByVal SKU_Index As String, Optional ByVal Strwhere As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = " select	Br.BorrowReturn_Index,Doc.Description as DocumentType_BorrowReturn,Br.BorrowReturn_No,Br.BorrowReturn_Date,Brl.Total_Qty  "
            strSQL &= " from	    tb_BorrowReturnLocation Brl"
            strSQL &= "          inner join tb_BorrowReturn Br on Brl.BorrowReturn_Index = Br.BorrowReturn_Index"
            strSQL &= "          Left join ms_DocumentType Doc on Br.DocumentType_Index = Doc.DocumentType_Index"
            strSQL &= " where	Brl.From_LocationBalance_Index  in (select LocationBalance_index from tb_locationbalance  "
            strSQL &= " where   sku_index = '" & SKU_Index & "')"
            strSQL &= " and Br.Status = 1"

            strSQL = strSQL & Strwhere
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
#End Region




End Class
