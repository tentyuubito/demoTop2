Imports WMS_STD_Formula

Public Class clsSkuMinMaxByWH : Inherits DBType_SQLServer

    Public Function chkSKU_PACKAGE_USED(ByVal _pPackage_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL &= " DECLARE @Package_Index Varchar(13)"
            strSQL &= " SET @Package_Index = '" & _pPackage_Index & "'"
            strSQL &= " SELECT *"
            strSQL &= " FROM("
            strSQL &= " SELECT "
            '************************* -- รับ"
            strSQL &= " Receive = (select isnull(sum(OI.Total_Qty),0) AS Receive"
            strSQL &= " from tb_Order O inner join"
            strSQL &= " 	 tb_OrderItem OI ON O.Order_Index = OI.Order_Index
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND OI.Package_Index =@Package_Index)"
            '************************* -- เบิก"
            strSQL &= " ,WithDraw = (select isnull(sum(OI.Total_Qty),0) AS WithDraw"
            strSQL &= " from tb_WithDraw O inner join"
            strSQL &= " 	 tb_WithDrawItem OI ON O.WithDraw_Index = OI.WithDraw_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND OI.Package_Index =@Package_Index)"
            '************************* -- โอน"
            strSQL &= " ,Transfer = (select isnull(sum(OI.Total_Qty),0) AS Transfer"
            strSQL &= " from tb_TransferStatus O inner join"
            strSQL &= " 	 tb_TransferStatusLocation OI ON O.TransferStatus_Index = OI.TransferStatus_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND OI.Package_Index =@Package_Index)"
            '************************* -- ยืม"
            strSQL &= " ,Borrow = (select isnull(sum(OI.Total_Qty),0) AS Borrow"
            strSQL &= " from tb_Borrow O inner join"
            strSQL &= " 	 tb_BorrowLocation OI ON O.Borrow_Index = OI.Borrow_Index
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND OI.Package_Index =@Package_Index)"
            '************************* -- คืน"
            strSQL &= " ,BorrowReturn = (select isnull(sum(OI.Total_Qty),0) AS BorrowReturn"
            strSQL &= " from tb_BorrowReturn O inner join"
            strSQL &= " 	 tb_BorrowReturnLocation OI ON O.BorrowReturn_Index = OI.BorrowReturn_Index
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND OI.Package_Index =@Package_Index)"
            '************************* -- คงเหลือ"
            strSQL &= " ,Balance=(select isnull(sum(LB.Qty_Bal),0) as Qty_Bal"
            strSQL &= " from tb_LocationBalance LB inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = LB.Sku_Index inner join"
            strSQL &= " 	 tb_OrderItem OI ON LB.OrderItem_Index = OI.OrderItem_Index"
            strSQL &= "            Where (LB.Qty_Bal > 0)"
            strSQL &= " 	AND OI.Package_Index =@Package_Index)"
            strSQL &= " ) CHKSKU_USED"
            strSQL &= " WHERE (Receive > 0) or (WithDraw > 0) or (Transfer > 0) or (Borrow > 0) or (BorrowReturn > 0) or (Balance > 0)"

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = _pPackage_Index

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            '_dataTable = GetDataTable

            'If _dataTable.Rows.Count > 0 Then
            '    Return True
            'Else
            '    Return False
            'End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function Delete_Master(ByVal skuratio_Index As String) As Boolean
        ' *** Define value from parameter
        'Me._sku_Index = sku_Index

        Dim strSQL As String

        Try
            strSQL = " Delete  " & _
                     " From ms_SKURatio " & _
                     " WHERE skuratio_Index='" + skuratio_Index + "' "

            'strSQL = "update ms_SKURatio set status_id = -1"
            'strSQL &= " WHERE skuratio_Index='" + skuratio_Index + "' "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function getSkuMinMaxByWH(ByVal Sku_Index As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " select * from View_SkuMinMaxByWH where Sku_Index=@Sku_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("Sku_Index", SqlDbType.VarChar, 13).Value = Sku_Index
            End With
            strSQL &= " order by Seq1 asc; "

            Dim _dt As DataTable = DBExeQuery(strSQL)
            If (_dt.Columns.Contains("Min_Qty")) Then
                _dt.Columns("Min_Qty").ReadOnly = False
            End If
            If (_dt.Columns.Contains("Max_Qty")) Then
                _dt.Columns("Max_Qty").ReadOnly = False
            End If

            Return _dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function saveSkuMinMaxByWH(ByVal dt As DataTable) As Boolean
        If (dt.Rows.Count = 0) Then Return False
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim _SumMinSku_Qty As Decimal = 0
            Dim _SumMaxSku_Qty As Decimal = 0
            If (dt.Rows.Count() > 0) Then
                _SumMinSku_Qty = dt.Compute(" sum(Min_Qty) ", " Min_Qty>=0 ")
                _SumMaxSku_Qty = dt.Compute(" sum(Max_Qty) ", " Max_Qty>=0 ")
            End If
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("Sku_Index", SqlDbType.VarChar, 13).Value = dt.Rows(0).Item("Sku_Index").ToString()
            SQLServerCommand.Parameters.Add("Min_Qty", SqlDbType.Decimal).Value = _SumMinSku_Qty
            SQLServerCommand.Parameters.Add("Max_Qty", SqlDbType.Decimal).Value = _SumMaxSku_Qty
            SQLServerCommand.Parameters.Add("update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
            SQLServerCommand.Parameters.Add("update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            strSQL = "update ms_SKU set "
            strSQL &= "Min_Qty=@Min_Qty, "
            strSQL &= "Max_Qty=@Max_Qty, "
            strSQL &= "update_by=@update_by, "
            strSQL &= "update_date=getdate(), "
            strSQL &= "update_branch=@update_branch "
            strSQL &= "where Sku_Index=@Sku_Index "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Insert Or Update
            For iRow As Integer = 0 To dt.Rows.Count - 1
                Dim _dr As DataRow = dt.Rows(iRow)
                Dim _Running_Index As String = _dr("Running_Index").ToString()
                Dim _Sku_Index As String = _dr("Sku_Index").ToString()
                Dim _Warehouse_Index As String = _dr("Warehouse_Index").ToString()
                Dim _Min_Qty As Decimal = 0
                Decimal.TryParse(_dr("Min_Qty").ToString(), _Min_Qty)
                Decimal.TryParse(_Min_Qty.ToString("#,##0.######"), _Min_Qty)
                Dim _Max_Qty As Decimal = 0
                Decimal.TryParse(_dr("Max_Qty").ToString(), _Max_Qty)
                Decimal.TryParse(_Max_Qty.ToString("#,##0.######"), _Max_Qty)
                Dim _Min_Qty_Begin As Decimal = 0
                Decimal.TryParse(_dr("Min_Qty_Begin").ToString(), _Min_Qty_Begin)
                Decimal.TryParse(_Min_Qty_Begin.ToString("#,##0.######"), _Min_Qty_Begin)
                Dim _Max_Qty_Begin As Decimal = 0
                Decimal.TryParse(_dr("Max_Qty_Begin").ToString(), _Max_Qty_Begin)
                Decimal.TryParse(_Max_Qty_Begin.ToString("#,##0.######"), _Max_Qty_Begin)

                If (_Sku_Index = Nothing) Then
                    Throw New ApplicationException("ไม่พบสินค้า")
                End If
                If (_Warehouse_Index = Nothing) Then
                    Throw New ApplicationException("ไม่พบคลัง")
                End If

                ' Insert
                If (Not IsNumeric(_Running_Index)) Then
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("Sku_Index", SqlDbType.VarChar, 13).Value = _Sku_Index
                    SQLServerCommand.Parameters.Add("Warehouse_Index", SqlDbType.VarChar, 13).Value = _Warehouse_Index
                    SQLServerCommand.Parameters.Add("Min_Qty", SqlDbType.Decimal).Value = _Min_Qty
                    SQLServerCommand.Parameters.Add("Max_Qty", SqlDbType.Decimal).Value = _Max_Qty
                    SQLServerCommand.Parameters.Add("add_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                    SQLServerCommand.Parameters.Add("add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                    SQLServerCommand.Parameters.Add("status_id", SqlDbType.Int).Value = 1

                    strSQL = " insert into ms_SKU_WH_MinMax (Warehouse_Index,Sku_Index,Min_Qty,Max_Qty,add_by,add_date,add_branch,status_id) values(@Warehouse_Index,@Sku_Index,@Min_Qty,@Max_Qty,@add_by,getdate(),@add_branch,@status_id) "
                    DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    Continue For
                End If

                ' Update
                If (_Min_Qty <> _Min_Qty_Begin Or _Max_Qty <> _Max_Qty_Begin) Then
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("Running_Index", SqlDbType.Int).Value = CInt(_Running_Index)
                    SQLServerCommand.Parameters.Add("Min_Qty", SqlDbType.Decimal).Value = _Min_Qty
                    SQLServerCommand.Parameters.Add("Max_Qty", SqlDbType.Decimal).Value = _Max_Qty
                    SQLServerCommand.Parameters.Add("update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                    SQLServerCommand.Parameters.Add("update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                    strSQL = "update ms_SKU_WH_MinMax set "
                    strSQL &= "Min_Qty=@Min_Qty, "
                    strSQL &= "Max_Qty=@Max_Qty, "
                    strSQL &= "update_by=@update_by, "
                    strSQL &= "update_date=getdate(), "
                    strSQL &= "update_branch=@update_branch "
                    strSQL &= "where Running_Index=@Running_Index "
                    DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    Continue For
                End If
            Next

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

End Class
