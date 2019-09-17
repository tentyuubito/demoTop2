Imports WMS_STD_Formula

Public Class clsPR : Inherits DBType_SQLServer

#Region " Enum Pager Type "

    Public Enum enuPagerType
        [All]
        [Top]
        [Page]
    End Enum

#End Region

#Region " Enum Save Type "

    Public Enum enuSaveType
        [Insert]
        [Update]
    End Enum

#End Region

#Region " Rule Event PR "

    Public Function canCancelPurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            Dim _dt As DataTable
            strSQL = " select Status_Desc from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and Status in (-1) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException(String.Format("ไม่สามารถยกเลิกเอกสารได้, สถานะเอกสาร{0}", _dt.Rows(0).Item("Status_Desc").ToString()))
            End If
            strSQL = " select PurchaseOrder_Request_Index from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and IsCanCancel=0 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException("เอกสารนี้ไม่ยินยอมให้ยกเลิก")
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function canConfirmPurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            Dim _dt As DataTable
            strSQL = " select Status_Desc from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and Status not in (1) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException(String.Format("ไม่สามารถยืนยันเอกสารได้, สถานะเอกสาร{0}", _dt.Rows(0).Item("Status_Desc").ToString()))
            End If
            strSQL = " select Confirm_By,Confirm_Date from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and IsConfirm=1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                If (IsDate(_dt.Rows(0).Item("Confirm_Date"))) Then
                    Throw New ApplicationException(String.Format("ไม่สามารถยืนยันเอกสารได้, เอกสารถูกยืนยันแล้วเมื่อ {1} โดย {0}", _dt.Rows(0).Item("Confirm_By").ToString(), CDate(_dt.Rows(0).Item("Confirm_Date")).ToString("dd/MM/yyyy HH:mm")))
                Else
                    Throw New ApplicationException(String.Format("ไม่สามารถยืนยันเอกสารได้, เอกสารถูกยืนยันแล้วโดย {0}", _dt.Rows(0).Item("Confirm_By").ToString()))
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function canClosePurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            Dim _dt As DataTable
            strSQL = " select Status_Desc from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and Status not in (1,2) "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                Throw New ApplicationException(String.Format("ไม่สามารถปิดเอกสารได้, สถานะเอกสาร{0}", _dt.Rows(0).Item("Status_Desc").ToString()))
            End If
            strSQL = " select Closed_By,Closed_Date from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and IsClosed=1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            _dt = DBExeQuery(strSQL)
            If (_dt.Rows.Count > 0) Then
                If (IsDate(_dt.Rows(0).Item("Closed_Date"))) Then
                    Throw New ApplicationException(String.Format("ไม่สามารถปิดเอกสารได้, เอกสารถูกปิดแล้วเมื่อ {1} โดย {0}", _dt.Rows(0).Item("Closed_By").ToString(), CDate(_dt.Rows(0).Item("Closed_Date")).ToString("dd/MM/yyyy HH:mm")))
                Else
                    Throw New ApplicationException(String.Format("ไม่สามารถปิดเอกสารได้, เอกสารถูกปิดแล้วโดย {0}", _dt.Rows(0).Item("Closed_By").ToString()))
                End If
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region " Event PR "

    Public Function savePurchaseOrder_Request(ByVal dtH As DataTable, ByVal dtD As DataTable, ByVal listRemove As List(Of String), ByVal Edit_Date_S As Date) As String
        If (Not dtH.Rows.Count = 1) Then
            Throw New ApplicationException("ไม่พบข้อมูลใบคำขอสั่งซื้อ")
        End If
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim saveType As enuSaveType = enuSaveType.Update
            '--------------------------------------------------------------------
            ' Header
            '--------------------------------------------------------------------
            Dim _drH As DataRow = dtH.Rows(0)
            Dim _PurchaseOrder_Request_Index As String = _drH("PurchaseOrder_Request_Index").ToString()
            Dim _PurchaseOrder_Request_No As String = _drH("PurchaseOrder_Request_No").ToString()
            Dim _DocumentType_Index As String = _drH("DocumentType_Index").ToString()
            If (_DocumentType_Index = Nothing) Then
                Throw New ApplicationException("ไม่พบประเภทเอกสาร")
            End If
            If (_PurchaseOrder_Request_Index = Nothing) Then
                saveType = enuSaveType.Insert
                '----- GET INDEX -----'
                _PurchaseOrder_Request_Index = New Sy_AutoNumber().getSys_Value("PurchaseOrder_Request_Index")
            End If
            If (_PurchaseOrder_Request_Index = Nothing) Then
                Throw New ApplicationException("ไม่พบเลขที่ใบคำขอสั่งซื้อ")
            End If
            If (_PurchaseOrder_Request_No = Nothing) Then
                '----- GET NO -----'
                Dim _ServerDate As Date = CDate(DBExeQuery_Scalar(String.Format(" select getdate() "), Connection, myTrans, eCommandType.Text))
                _PurchaseOrder_Request_No = New Sy_AutoyyyyMM().Auto_DocumentType_Number(Connection, myTrans, _DocumentType_Index, _ServerDate, "")
            End If
            If (_PurchaseOrder_Request_No = Nothing) Then
                Throw New ApplicationException("ไม่พบเลขที่ใบคำขอสั่งซื้อ")
            End If

            ' Check Validate
            strSQL = " select Last_Modified_Date from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Request_Index
            End With
            Dim _dtValid_1 As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
            If (_dtValid_1.Rows.Count > 0) Then
                If (IsDate(_dtValid_1.Rows(0).Item("Last_Modified_Date"))) Then
                    If (Edit_Date_S < CDate(_dtValid_1.Rows(0).Item("Last_Modified_Date"))) Then
                        Throw New ApplicationException("ไม่สามารถแก้ไขได้, ข้อมูลมีการเปลี่ยนแปลงระหว่างแก้ไขข้อมูล")
                    End If
                End If
            End If
            strSQL = " select PurchaseOrder_Request_Index from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and Status=-1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Request_Index
            End With
            Dim _dtValid_2 As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
            If (_dtValid_2.Rows.Count > 0) Then
                Throw New ApplicationException("ไม่สามารถแก้ไขได้, มีการยกเลิกใบคำขอสั่งซื้อนี้แล้ว")
            End If
            strSQL = " select PurchaseOrder_Request_Index from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index and IsConfirm=1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Request_Index
            End With
            Dim _dtValid_3 As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
            If (_dtValid_3.Rows.Count > 0) Then
                Throw New ApplicationException("ไม่สามารถแก้ไขได้, มีการยืนยันใบคำขอสั่งซื้อนี้แล้ว")
            End If

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Request_Index
            SQLServerCommand.Parameters.Add("PurchaseOrder_Request_No", SqlDbType.VarChar, 50).Value = _PurchaseOrder_Request_No
            SQLServerCommand.Parameters.Add("PurchaseOrder_Request_Date", SqlDbType.DateTime).Value = _drH("PurchaseOrder_Request_Date")
            SQLServerCommand.Parameters.Add("Due_Date", SqlDbType.DateTime).Value = _drH("Due_Date")
            SQLServerCommand.Parameters.Add("DocumentType_Index", SqlDbType.VarChar, 13).Value = _DocumentType_Index
            SQLServerCommand.Parameters.Add("Customer_Index", SqlDbType.VarChar, 13).Value = _drH("Customer_Index")
            SQLServerCommand.Parameters.Add("Ref_No1", SqlDbType.VarChar, 100).Value = _drH("Ref_No1")
            SQLServerCommand.Parameters.Add("Ref_No2", SqlDbType.VarChar, 100).Value = _drH("Ref_No2")
            'SQLServerCommand.Parameters.Add("Ref_No3", SqlDbType.VarChar, 100).Value = _drH("Ref_No3")
            'SQLServerCommand.Parameters.Add("Ref_No4", SqlDbType.VarChar, 100).Value = _drH("Ref_No4")
            'SQLServerCommand.Parameters.Add("Ref_No5", SqlDbType.VarChar, 100).Value = _drH("Ref_No5")
            SQLServerCommand.Parameters.Add("Remark", SqlDbType.NVarChar, 255).Value = _drH("Remark")
            'SQLServerCommand.Parameters.Add("IsClosed", SqlDbType.Bit).Value = _drH("IsClosed")
            'SQLServerCommand.Parameters.Add("Closed_By", SqlDbType.VarChar, 50).Value = _drH("Closed_By")
            'SQLServerCommand.Parameters.Add("Closed_Date", SqlDbType.SmallDateTime).Value = _drH("Closed_Date")
            'SQLServerCommand.Parameters.Add("IsConfirm", SqlDbType.Bit).Value = _drH("IsConfirm")
            'SQLServerCommand.Parameters.Add("Confirm_By", SqlDbType.VarChar, 50).Value = _drH("Confirm_By")
            'SQLServerCommand.Parameters.Add("Confirm_Date", SqlDbType.SmallDateTime).Value = _drH("Confirm_Date")
            'SQLServerCommand.Parameters.Add("IsCanCancel", SqlDbType.Bit).Value = _drH("IsCanCancel")
            'SQLServerCommand.Parameters.Add("Status", SqlDbType.Int).Value = _drH("Status")
            If (saveType = enuSaveType.Insert) Then
                SQLServerCommand.Parameters.Add("Status", SqlDbType.Int).Value = 1
                SQLServerCommand.Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                SQLServerCommand.Parameters.Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                strSQL = " insert into tb_PurchaseOrder_Request (PurchaseOrder_Request_Index,PurchaseOrder_Request_No,PurchaseOrder_Request_Date,Due_Date,DocumentType_Index,Customer_Index,Ref_No1,Ref_No2,Remark,add_by,add_date,add_branch,Status) values(@PurchaseOrder_Request_Index,@PurchaseOrder_Request_No,@PurchaseOrder_Request_Date,@Due_Date,@DocumentType_Index,@Customer_Index,@Ref_No1,@Ref_No2,@Remark,@add_by,getdate(),@add_branch,@Status) "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            ElseIf (saveType = enuSaveType.Update) Then
                SQLServerCommand.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                SQLServerCommand.Parameters.Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                strSQL = "update tb_PurchaseOrder_Request set "
                strSQL &= "PurchaseOrder_Request_No=@PurchaseOrder_Request_No, "
                strSQL &= "PurchaseOrder_Request_Date=@PurchaseOrder_Request_Date, "
                strSQL &= "Due_Date=@Due_Date, "
                strSQL &= "DocumentType_Index=@DocumentType_Index, "
                strSQL &= "Customer_Index=@Customer_Index, "
                strSQL &= "Ref_No1=@Ref_No1, "
                strSQL &= "Ref_No2=@Ref_No2, "
                strSQL &= "Remark=@Remark, "
                strSQL &= "update_by=@update_by, "
                strSQL &= "update_date=getdate(), "
                strSQL &= "update_branch=@update_branch "
                strSQL &= "where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            End If

            '--------------------------------------------------------------------
            ' Detail
            '--------------------------------------------------------------------
            ' Remove In List
            For Each _PurchaseOrder_RequestItem_Index As String In listRemove
                SQLServerCommand.Parameters.Clear()
                SQLServerCommand.Parameters.Add("PurchaseOrder_RequestItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_RequestItem_Index
                SQLServerCommand.Parameters.Add("@cancel_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                SQLServerCommand.Parameters.Add("@cancel_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                strSQL = "update tb_PurchaseOrder_RequestItem set "
                strSQL &= "Status=-1, "
                strSQL &= "cancel_by=@cancel_by, "
                strSQL &= "cancel_date=getdate(), "
                strSQL &= "cancel_branch=@cancel_branch "
                strSQL &= "where PurchaseOrder_RequestItem_Index=@PurchaseOrder_RequestItem_Index "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            Next
            ' Insert Or Update
            For iRow As Integer = 0 To dtD.Rows.Count - 1
                Dim _drD As DataRow = dtD.Rows(iRow)
                Dim _PurchaseOrder_RequestItem_Index As String = _drD("PurchaseOrder_RequestItem_Index").ToString()
                If (_drD("Sku_Index") = Nothing) Then
                    Throw New ApplicationException("ไม่พบรหัสสินค้า")
                End If
                If (_drD("Package_Index") = Nothing) Then
                    Throw New ApplicationException("ไม่พบหน่วยสินค้า")
                End If

                ' Insert
                If (_PurchaseOrder_RequestItem_Index = Nothing) Then
                    '----- GET INDEX -----'
                    _PurchaseOrder_RequestItem_Index = New Sy_AutoNumber().getSys_Value("PurchaseOrder_RequestItem_Index")
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("PurchaseOrder_RequestItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_RequestItem_Index
                    SQLServerCommand.Parameters.Add("PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Request_Index
                    SQLServerCommand.Parameters.Add("Item_Seq", SqlDbType.Int).Value = _drD("Item_Seq")
                    SQLServerCommand.Parameters.Add("Sku_Index", SqlDbType.VarChar, 13).Value = _drD("Sku_Index")
                    SQLServerCommand.Parameters.Add("Package_Index", SqlDbType.VarChar, 13).Value = _drD("Package_Index")
                    'SQLServerCommand.Parameters.Add("Serial_No", SqlDbType.NVarChar, 100).Value = DBNull.Value
                    SQLServerCommand.Parameters.Add("Qty", SqlDbType.Decimal).Value = _drD("Qty")
                    SQLServerCommand.Parameters.Add("Ratio", SqlDbType.Decimal).Value = _drD("Ratio")
                    SQLServerCommand.Parameters.Add("Total_Qty", SqlDbType.Decimal).Value = _drD("Total_Qty")
                    SQLServerCommand.Parameters.Add("Weight", SqlDbType.Decimal).Value = _drD("Weight")
                    SQLServerCommand.Parameters.Add("Volume", SqlDbType.Decimal).Value = _drD("Volume")
                    SQLServerCommand.Parameters.Add("Total_Received_Qty", SqlDbType.Decimal).Value = _drD("Total_Received_Qty")
                    'SQLServerCommand.Parameters.Add("Ref_No1", SqlDbType.NVarChar, 100).Value = DBNull.Value
                    'SQLServerCommand.Parameters.Add("Ref_No2", SqlDbType.NVarChar, 100).Value = DBNull.Value
                    'SQLServerCommand.Parameters.Add("Ref_No3", SqlDbType.NVarChar, 100).Value = DBNull.Value
                    'SQLServerCommand.Parameters.Add("Ref_No4", SqlDbType.NVarChar, 100).Value = DBNull.Value
                    SQLServerCommand.Parameters.Add("Ref_No5", SqlDbType.NVarChar, 100).Value = _drD("Ref_No5")
                    SQLServerCommand.Parameters.Add("Remark", SqlDbType.NVarChar, 255).Value = _drD("Remark")
                    SQLServerCommand.Parameters.Add("Status", SqlDbType.Int).Value = 1
                    SQLServerCommand.Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                    SQLServerCommand.Parameters.Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                    SQLServerCommand.Parameters.Add("Due_Date", SqlDbType.DateTime).Value = _drD("Due_Date")

                    strSQL = " insert into tb_PurchaseOrder_RequestItem (PurchaseOrder_RequestItem_Index,PurchaseOrder_Request_Index,Item_Seq,Sku_Index,Package_Index,Qty,Ratio,Total_Qty,Weight,Volume,Total_Received_Qty,Ref_No5,Remark,add_by,add_date,add_branch,Status,Due_Date) values(@PurchaseOrder_RequestItem_Index,@PurchaseOrder_Request_Index,@Item_Seq,@Sku_Index,@Package_Index,@Qty,@Ratio,@Total_Qty,@Weight,@Volume,@Total_Received_Qty,@Ref_No5,@Remark,@add_by,getdate(),@add_branch,@Status,@Due_Date) "
                    DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    Continue For
                End If

                ' Update
                SQLServerCommand.Parameters.Clear()
                SQLServerCommand.Parameters.Add("PurchaseOrder_RequestItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_RequestItem_Index
                'SQLServerCommand.Parameters.Add("PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                SQLServerCommand.Parameters.Add("Item_Seq", SqlDbType.Int).Value = _drD("Item_Seq")
                SQLServerCommand.Parameters.Add("Sku_Index", SqlDbType.VarChar, 13).Value = _drD("Sku_Index")
                SQLServerCommand.Parameters.Add("Package_Index", SqlDbType.VarChar, 13).Value = _drD("Package_Index")
                'SQLServerCommand.Parameters.Add("Serial_No", SqlDbType.NVarChar, 100).Value = DBNull.Value
                SQLServerCommand.Parameters.Add("Qty", SqlDbType.Decimal).Value = _drD("Qty")
                SQLServerCommand.Parameters.Add("Ratio", SqlDbType.Decimal).Value = _drD("Ratio")
                SQLServerCommand.Parameters.Add("Total_Qty", SqlDbType.Decimal).Value = _drD("Total_Qty")
                SQLServerCommand.Parameters.Add("Weight", SqlDbType.Decimal).Value = _drD("Weight")
                SQLServerCommand.Parameters.Add("Volume", SqlDbType.Decimal).Value = _drD("Volume")
                SQLServerCommand.Parameters.Add("Total_Received_Qty", SqlDbType.Decimal).Value = _drD("Total_Received_Qty")
                'SQLServerCommand.Parameters.Add("Ref_No1", SqlDbType.NVarChar, 100).Value = DBNull.Value
                'SQLServerCommand.Parameters.Add("Ref_No2", SqlDbType.NVarChar, 100).Value = DBNull.Value
                'SQLServerCommand.Parameters.Add("Ref_No3", SqlDbType.NVarChar, 100).Value = DBNull.Value
                'SQLServerCommand.Parameters.Add("Ref_No4", SqlDbType.NVarChar, 100).Value = DBNull.Value
                'SQLServerCommand.Parameters.Add("Ref_No5", SqlDbType.NVarChar, 100).Value = DBNull.Value
                SQLServerCommand.Parameters.Add("Remark", SqlDbType.NVarChar, 255).Value = DBNull.Value
                'SQLServerCommand.Parameters.Add("Status", SqlDbType.Int).Value = DBNull.Value
                SQLServerCommand.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                SQLServerCommand.Parameters.Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                SQLServerCommand.Parameters.Add("Due_Date", SqlDbType.DateTime).Value = _drD("Due_Date")

                strSQL = "update tb_PurchaseOrder_RequestItem set "
                strSQL &= "Item_Seq=@Item_Seq, "
                strSQL &= "Sku_Index=@Sku_Index, "
                strSQL &= "Package_Index=@Package_Index, "
                strSQL &= "Qty=@Qty, "
                strSQL &= "Ratio=@Ratio, "
                strSQL &= "Total_Qty=@Total_Qty, "
                strSQL &= "Weight=@Weight, "
                strSQL &= "Volume=@Volume, "
                strSQL &= "Total_Received_Qty=@Total_Received_Qty, "
                strSQL &= "Remark=@Remark, "
                strSQL &= "update_by=@update_by, "
                strSQL &= "update_date=getdate(), "
                strSQL &= "Due_Date=@Due_Date, "
                strSQL &= "update_branch=@update_branch "
                strSQL &= "where PurchaseOrder_RequestItem_Index=@PurchaseOrder_RequestItem_Index "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            Next


            myTrans.Commit()
            Return _PurchaseOrder_Request_Index
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function cancelPurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrder_Request set "
            strSQL &= "Status=-1, "
            strSQL &= "IsClosed=1, "
            strSQL &= "IsConfirm=1, "
            strSQL &= "cancel_by=@cancel_by, "
            strSQL &= "cancel_date=getdate(), "
            strSQL &= "cancel_branch=@cancel_branch "
            strSQL &= "where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index "
            strSQL &= "and Status<>-1 "
            strSQL &= "and IsCanCancel=1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
                .Add("@cancel_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@cancel_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
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

    Public Function confirmPurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrder_Request set "
            strSQL &= "Status=2, "
            strSQL &= "IsConfirm=1, "
            strSQL &= "Confirm_By=@Confirm_By, "
            strSQL &= "Confirm_Date=getdate() "
            strSQL &= "where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index "
            strSQL &= "and Status=1 "
            strSQL &= "and IsConfirm=0 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
                .Add("@Confirm_By", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
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

    Public Function closePurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrder_Request set "
            strSQL &= "Status=3, "
            strSQL &= "IsClosed=1, "
            strSQL &= "Closed_By=@Closed_By, "
            strSQL &= "Closed_Date=getdate(), "
            strSQL &= "IsConfirm=1, "
            strSQL &= "IsCanCancel=0 "
            strSQL &= "where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index "
            strSQL &= "and Status in (1,2) "
            strSQL &= "and IsClosed=0 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
                .Add("@Closed_By", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
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

#End Region

#Region " Event PO_PR "

    Public Function receivePurchaseOrderItem_PR(ByVal dtInsert As DataTable) As Boolean
        If dtInsert.Rows.Count = 0 Then Return False
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim _PurchaseOrder_Index As String = dtInsert.Rows(0).Item("PurchaseOrder_Index").ToString()
            Dim _PurchaseOrder_PR_Index As String = dtInsert.Rows(0).Item("PurchaseOrder_PR_Index").ToString()
            ' Insert
            For iRow As Integer = 0 To dtInsert.Rows.Count - 1
                Dim _drInsert As DataRow = dtInsert.Rows(iRow)
                Dim _PurchaseOrder_RequestItem_Index As String = _drInsert("PurchaseOrder_RequestItem_Index").ToString()
                Dim _Qty As Decimal = 0
                Decimal.TryParse(_drInsert("Qty").ToString(), _Qty)
                Dim _Total_Qty As Decimal = 0
                Decimal.TryParse(_drInsert("Total_Qty").ToString(), _Total_Qty)

                If (_Total_Qty <= 0) Then
                    Throw New ApplicationException("จำนวนไม่ถูกต้อง")
                End If

                ' Check Validate
                strSQL = " select * from View_List_PurchaseOrder_Request where PurchaseOrder_RequestItem_Index=@PurchaseOrder_RequestItem_Index and Total_Pending_Qty>=@Total_Qty "
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@PurchaseOrder_RequestItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_RequestItem_Index
                    .Add("@Total_Qty", SqlDbType.Decimal).Value = _Total_Qty
                End With
                Dim _dtList_PR As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
                If (_dtList_PR.Rows.Count = 0) Then
                    Throw New ApplicationException("ไม่สามารถดึงข้อมูลได้, จำนวนรับเกินที่ค้างรับ")
                End If

                '----- GET INDEX -----'
                Dim _PurchaseOrderItem_PR_Index As String = New Sy_AutoNumber().getSys_Value("PurchaseOrderItem_PR_Index")


                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@PurchaseOrderItem_PR_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrderItem_PR_Index
                    .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_PR_Index
                    '.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                    .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Index
                    .Add("@PurchaseOrder_RequestItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_RequestItem_Index
                    .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = _dtList_PR.Rows(0).Item("PurchaseOrder_Request_Index")
                    .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _dtList_PR.Rows(0).Item("Sku_Index")
                    .Add("@Package_Index", SqlDbType.VarChar, 13).Value = _dtList_PR.Rows(0).Item("Package_Index")
                    .Add("@Serial_No", SqlDbType.NVarChar, 100).Value = _dtList_PR.Rows(0).Item("Serial_No")
                    .Add("@Qty", SqlDbType.Decimal).Value = _Qty
                    .Add("@Ratio", SqlDbType.Decimal).Value = _dtList_PR.Rows(0).Item("Ratio")
                    .Add("@Total_Qty", SqlDbType.Decimal).Value = _Total_Qty
                    Dim _weight As Decimal = CDec(_dtList_PR.Rows(0).Item("Unit_Weight").ToString()) * _Total_Qty
                    Decimal.TryParse(_weight.ToString("0.######"), _weight)
                    Dim _volume As Decimal = CDec(_dtList_PR.Rows(0).Item("Unit_Volume").ToString()) * _Total_Qty
                    Decimal.TryParse(_volume.ToString("0.######"), _volume)
                    .Add("@Weight", SqlDbType.Decimal).Value = _weight
                    .Add("@Volume", SqlDbType.Decimal).Value = _volume
                    .Add("@Status", SqlDbType.Int).Value = 1
                    .Add("@add_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
                End With

                strSQL = " insert into tb_PurchaseOrderItem_PR (PurchaseOrderItem_PR_Index,PurchaseOrder_PR_Index,PurchaseOrder_Index,PurchaseOrder_RequestItem_Index,PurchaseOrder_Request_Index,Sku_Index,Package_Index,Qty,Ratio,Total_Qty,Weight,Volume,add_by,add_date,add_branch,Status) values(@PurchaseOrderItem_PR_Index,@PurchaseOrder_PR_Index,@PurchaseOrder_Index,@PurchaseOrder_RequestItem_Index,@PurchaseOrder_Request_Index,@Sku_Index,@Package_Index,@Qty,@Ratio,@Total_Qty,@Weight,@Volume,@add_by,getdate(),@add_branch,@Status) "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
                Continue For
            Next

            ' Consolidate
            Me.consolidatPurchaseOrderItem_PR(Connection, myTrans, _PurchaseOrder_PR_Index)

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function removePurchaseOrderItem(ByVal PurchaseOrder_PR_Index As String, ByVal PurchaseOrderItem_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrderItem_PR set "
            strSQL &= "Status=-1, "
            strSQL &= "cancel_by=@cancel_by, "
            strSQL &= "cancel_date=getdate(), "
            strSQL &= "cancel_branch=@cancel_branch "
            strSQL &= "where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            strSQL &= "and PurchaseOrderItem_Index=@PurchaseOrderItem_Index "
            strSQL &= "and Status<>-1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
                .Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = PurchaseOrderItem_Index
                .Add("@cancel_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@cancel_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Consolidate
            Me.consolidatPurchaseOrderItem_PR(Connection, myTrans, PurchaseOrder_PR_Index, PurchaseOrderItem_Index)

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function removePurchaseOrderItem_PR(ByVal PurchaseOrder_PR_Index As String, ByVal PurchaseOrderItem_Index As String, ByVal PurchaseOrderItem_PR_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrderItem_PR set "
            strSQL &= "Status=-1, "
            strSQL &= "cancel_by=@cancel_by, "
            strSQL &= "cancel_date=getdate(), "
            strSQL &= "cancel_branch=@cancel_branch "
            strSQL &= "where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            strSQL &= "and PurchaseOrderItem_PR_Index=@PurchaseOrderItem_PR_Index "
            strSQL &= "and Status<>-1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
                .Add("@PurchaseOrderItem_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrderItem_PR_Index
                .Add("@cancel_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@cancel_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Consolidate
            Me.consolidatPurchaseOrderItem_PR(Connection, myTrans, PurchaseOrder_PR_Index, PurchaseOrderItem_Index)

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function renewPurchaseOrderItem_PR(ByVal PurchaseOrder_PR_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrderItem_PR set "
            strSQL &= "Status=-1, "
            strSQL &= "cancel_by=@cancel_by, "
            strSQL &= "cancel_date=getdate(), "
            strSQL &= "cancel_branch=@cancel_branch "
            strSQL &= "where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            strSQL &= "and Status<>-1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
                .Add("@cancel_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                .Add("@cancel_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Consolidate
            Me.consolidatPurchaseOrderItem_PR(Connection, myTrans, PurchaseOrder_PR_Index)

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function consolidatPurchaseOrderItem_PR(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal PurchaseOrder_PR_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " select PurchaseOrderItem_Index from tb_PurchaseOrderItem where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
            End With
            Dim _dtPOI As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
            For Each _drPOI As DataRow In _dtPOI.Rows
                DBExeNonQuery(String.Format(" sp_Delete_PurchaseOrderItem '{0}','{1}' ", _drPOI("PurchaseOrderItem_Index").ToString(), W_Module.WV_UserName), Connection, myTrans)
            Next

            strSQL = " select * from View_Consolidate_PurchaseOrderItem_Group where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
            End With
            Dim _dtPOI_PR As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)

            Dim _Item_Seq As Integer = 0
            Dim _Currency_Index As String = ""

            For Each _drPOI_PR As DataRow In _dtPOI_PR.Rows
                If (_Item_Seq = 0) Then
                    strSQL = " select max(Item_Seq) as [Item_Seq] from tb_PurchaseOrderItem where PurchaseOrder_Index=@PurchaseOrder_Index "
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_Index")
                    End With
                    Dim _dtPOI_Seq As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    If (Not _dtPOI_Seq.Rows.Count = 0) Then
                        If (IsNumeric(_dtPOI_Seq.Rows(0).Item("Item_Seq"))) Then
                            _Item_Seq = _dtPOI_Seq.Rows(0).Item("Item_Seq")
                        End If
                    End If

                    strSQL = " select Currency_Index from tb_PurchaseOrder where PurchaseOrder_Index=@PurchaseOrder_Index "
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_Index")
                    End With
                    Dim _dtPO_Currency As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    If (Not _dtPO_Currency.Rows.Count = 0) Then
                        _Currency_Index = _dtPO_Currency.Rows(0).Item("Currency_Index")
                    End If
                End If

                '----- GET INDEX -----'
                Dim _PurchaseOrderItem_Index As String = New Sy_AutoNumber().getSys_Value("PurchaseOrderItem_Index")

                _Item_Seq += 1

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrderItem_Index
                    .Add("@Item_Seq", SqlDbType.Int).Value = _Item_Seq
                    .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_Index")
                    .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("Sku_Index")
                    .Add("@Package_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("Package_Index")
                    .Add("@Ratio", SqlDbType.Decimal).Value = _drPOI_PR("Ratio")
                    .Add("@Total_Qty", SqlDbType.Decimal).Value = _drPOI_PR("Total_Qty")
                    .Add("@Qty", SqlDbType.Decimal).Value = _drPOI_PR("Qty")
                    .Add("@Weight", SqlDbType.Decimal).Value = _drPOI_PR("Weight")
                    .Add("@Volume", SqlDbType.Decimal).Value = _drPOI_PR("Volume")
                    .Add("@Serial_No", SqlDbType.NVarChar, 100).Value = _drPOI_PR("Serial_No")

                    .Add("@Total_Received_Qty", SqlDbType.Decimal).Value = 0
                    .Add("@Received_Qty", SqlDbType.Decimal).Value = 0
                    .Add("@Received_Weight", SqlDbType.Decimal).Value = 0
                    .Add("@Received_Volume", SqlDbType.Decimal).Value = 0

                    .Add("@UnitPrice", SqlDbType.Decimal).Value = 0
                    .Add("@Amount", SqlDbType.Decimal).Value = 0
                    .Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _Currency_Index
                    .Add("@Discount_Amt", SqlDbType.Decimal).Value = 0
                    .Add("@Total_Amt", SqlDbType.Decimal).Value = 0
                    .Add("@Status", SqlDbType.Int).Value = 1

                    .Add("@Flo1", SqlDbType.Decimal).Value = 0
                    .Add("@Flo2", SqlDbType.Decimal).Value = 0
                    .Add("@Flo3", SqlDbType.Decimal).Value = 0
                    .Add("@Flo4", SqlDbType.Decimal).Value = 0
                    .Add("@Flo5", SqlDbType.Decimal).Value = 0

                    .Add("@add_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                    .Add("@Percent_Over_Allow", SqlDbType.Decimal).Value = 0
                    .Add("@Percent_Under_Allow", SqlDbType.Decimal).Value = 0

                    .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_PR_Index")
                End With

                strSQL = " insert into tb_PurchaseOrderItem (PurchaseOrderItem_Index,Item_Seq,PurchaseOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Received_Qty,Received_Qty,Received_Weight,Received_Volume,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,Percent_Over_Allow,Percent_Under_Allow,PurchaseOrder_PR_Index) values(@PurchaseOrderItem_Index,@Item_Seq,@PurchaseOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Received_Qty,@Received_Qty,@Received_Weight,@Received_Volume,@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,getdate(),@add_branch,@Percent_Over_Allow,@Percent_Under_Allow,@PurchaseOrder_PR_Index) "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

                strSQL = "update tb_PurchaseOrderItem_PR set "
                strSQL &= "PurchaseOrderItem_Index=@PurchaseOrderItem_Index "
                strSQL &= "where Status<>-1 "
                strSQL &= "and PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
                strSQL &= "and PurchaseOrder_Index=@PurchaseOrder_Index "
                strSQL &= "and Sku_Index=@Sku_Index "
                strSQL &= "and Package_Index=@Package_Index "
                strSQL &= "and Ratio=@Ratio "
                strSQL &= "and (Serial_No=@Serial_No or (Serial_No is null and @Serial_No is null)) "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            Next

            ' Update tb_PurchaseOrder_RequestItem[Total_Received_Qty]
            strSQL = "update tb_pri set "
            strSQL &= "tb_pri.Total_Received_Qty=(select isnull(sum(Total_Qty),0) from tb_PurchaseOrderItem_PR inner join tb_PurchaseOrder on tb_PurchaseOrder.PurchaseOrder_Index=tb_PurchaseOrderItem_PR.PurchaseOrder_Index and tb_PurchaseOrderItem_PR.Status<>-1 and tb_PurchaseOrder.Status<>-1 where PurchaseOrder_RequestItem_Index=tb_pri.PurchaseOrder_RequestItem_Index) "
            strSQL &= "from tb_PurchaseOrder_RequestItem as tb_pri "
            strSQL &= "where tb_pri.PurchaseOrder_RequestItem_Index in (select PurchaseOrder_RequestItem_Index from tb_PurchaseOrderItem_PR where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index group by PurchaseOrder_RequestItem_Index) "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Update tb_PurchaseOrder_Request[Status]
            strSQL = "update tb_pr set "
            strSQL &= "tb_pr.Status=(select case when isnull(sum(Total_Qty),0)=isnull(sum(Total_Received_Qty),0) then 3 else 2 end from tb_PurchaseOrder_RequestItem where Status<>-1 and PurchaseOrder_Request_Index=tb_pr.PurchaseOrder_Request_Index) "
            strSQL &= "from tb_PurchaseOrder_Request as tb_pr "
            strSQL &= "where tb_pr.PurchaseOrder_Request_Index in (select PurchaseOrder_Request_Index from tb_PurchaseOrderItem_PR where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index group by PurchaseOrder_Request_Index) "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function consolidatPurchaseOrderItem_PR(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal PurchaseOrder_PR_Index As String, ByVal PurchaseOrderItem_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
                .Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = PurchaseOrderItem_Index
            End With
            strSQL = " select PurchaseOrderItem_Index from tb_PurchaseOrderItem where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index and PurchaseOrderItem_Index=@PurchaseOrderItem_Index "
            Dim _dtPOI As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
            For Each _drPOI As DataRow In _dtPOI.Rows
                DBExeNonQuery(String.Format(" sp_Delete_PurchaseOrderItem '{0}','{1}' ", _drPOI("PurchaseOrderItem_Index").ToString(), W_Module.WV_UserName), Connection, myTrans)
            Next

            strSQL = " select tb_poi_pr.PurchaseOrder_Index,tb_poi_pr.Sku_Index,tb_poi_pr.Package_Index,tb_poi_pr.Ratio,isnull(sum(tb_poi_pr.Total_Qty),0) as [Total_Qty],isnull(sum(tb_poi_pr.Qty),0) as [Qty],isnull(sum(tb_poi_pr.Weight),0) as [Weight],isnull(sum(tb_poi_pr.Volume),0) as [Volume],tb_poi_pr.Serial_No,tb_poi_pr.PurchaseOrder_PR_Index,tb_poi_pr.PurchaseOrderItem_Index from tb_PurchaseOrderItem_PR as tb_poi_pr where tb_poi_pr.Status<>-1 and PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index and PurchaseOrderItem_Index=@PurchaseOrderItem_Index group by tb_poi_pr.PurchaseOrder_Index,tb_poi_pr.Sku_Index,tb_poi_pr.Package_Index,tb_poi_pr.Ratio,tb_poi_pr.Serial_No,tb_poi_pr.PurchaseOrder_PR_Index,tb_poi_pr.PurchaseOrderItem_Index "
            Dim _dtPOI_PR As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)

            Dim _Item_Seq As Integer = 0
            Dim _Currency_Index As String = ""

            For Each _drPOI_PR As DataRow In _dtPOI_PR.Rows
                If (_Item_Seq = 0) Then
                    strSQL = " select max(Item_Seq) as [Item_Seq] from tb_PurchaseOrderItem where PurchaseOrder_Index=@PurchaseOrder_Index "
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_Index")
                    End With
                    Dim _dtPOI_Seq As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    If (Not _dtPOI_Seq.Rows.Count = 0) Then
                        If (IsNumeric(_dtPOI_Seq.Rows(0).Item("Item_Seq"))) Then
                            _Item_Seq = _dtPOI_Seq.Rows(0).Item("Item_Seq")
                        End If
                    End If

                    strSQL = " select Currency_Index from tb_PurchaseOrder where PurchaseOrder_Index=@PurchaseOrder_Index "
                    With SQLServerCommand.Parameters
                        .Clear()
                        .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_Index")
                    End With
                    Dim _dtPO_Currency As DataTable = DBExeQuery(strSQL, Connection, myTrans, eCommandType.Text)
                    If (Not _dtPO_Currency.Rows.Count = 0) Then
                        _Currency_Index = _dtPO_Currency.Rows(0).Item("Currency_Index")
                    End If
                End If

                '----- GET INDEX -----'
                Dim _PurchaseOrderItem_Index As String = New Sy_AutoNumber().getSys_Value("PurchaseOrderItem_Index")

                _Item_Seq += 1

                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrderItem_Index
                    .Add("@Item_Seq", SqlDbType.Int).Value = _Item_Seq
                    .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_Index")
                    .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("Sku_Index")
                    .Add("@Package_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("Package_Index")
                    .Add("@Ratio", SqlDbType.Decimal).Value = _drPOI_PR("Ratio")
                    .Add("@Total_Qty", SqlDbType.Decimal).Value = _drPOI_PR("Total_Qty")
                    .Add("@Qty", SqlDbType.Decimal).Value = _drPOI_PR("Qty")
                    .Add("@Weight", SqlDbType.Decimal).Value = _drPOI_PR("Weight")
                    .Add("@Volume", SqlDbType.Decimal).Value = _drPOI_PR("Volume")
                    .Add("@Serial_No", SqlDbType.NVarChar, 100).Value = _drPOI_PR("Serial_No")

                    .Add("@Total_Received_Qty", SqlDbType.Decimal).Value = 0
                    .Add("@Received_Qty", SqlDbType.Decimal).Value = 0
                    .Add("@Received_Weight", SqlDbType.Decimal).Value = 0
                    .Add("@Received_Volume", SqlDbType.Decimal).Value = 0

                    .Add("@UnitPrice", SqlDbType.Decimal).Value = 0
                    .Add("@Amount", SqlDbType.Decimal).Value = 0
                    .Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _Currency_Index
                    .Add("@Discount_Amt", SqlDbType.Decimal).Value = 0
                    .Add("@Total_Amt", SqlDbType.Decimal).Value = 0
                    .Add("@Status", SqlDbType.Int).Value = 1

                    .Add("@Flo1", SqlDbType.Decimal).Value = 0
                    .Add("@Flo2", SqlDbType.Decimal).Value = 0
                    .Add("@Flo3", SqlDbType.Decimal).Value = 0
                    .Add("@Flo4", SqlDbType.Decimal).Value = 0
                    .Add("@Flo5", SqlDbType.Decimal).Value = 0

                    .Add("@add_by", SqlDbType.VarChar, 50).Value = W_Module.WV_UserName
                    .Add("@add_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID

                    .Add("@Percent_Over_Allow", SqlDbType.Decimal).Value = 0
                    .Add("@Percent_Under_Allow", SqlDbType.Decimal).Value = 0

                    .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = _drPOI_PR("PurchaseOrder_PR_Index")
                End With

                strSQL = " insert into tb_PurchaseOrderItem (PurchaseOrderItem_Index,Item_Seq,PurchaseOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Received_Qty,Received_Qty,Received_Weight,Received_Volume,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,Percent_Over_Allow,Percent_Under_Allow,PurchaseOrder_PR_Index) values(@PurchaseOrderItem_Index,@Item_Seq,@PurchaseOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Received_Qty,@Received_Qty,@Received_Weight,@Received_Volume,@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,getdate(),@add_branch,@Percent_Over_Allow,@Percent_Under_Allow,@PurchaseOrder_PR_Index) "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

                strSQL = "update tb_PurchaseOrderItem_PR set "
                strSQL &= "PurchaseOrderItem_Index=@PurchaseOrderItem_Index "
                strSQL &= "where Status<>-1 "
                strSQL &= "and PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
                strSQL &= "and PurchaseOrder_Index=@PurchaseOrder_Index "
                strSQL &= "and Sku_Index=@Sku_Index "
                strSQL &= "and Package_Index=@Package_Index "
                strSQL &= "and Ratio=@Ratio "
                strSQL &= "and (Serial_No=@Serial_No or (Serial_No is null and @Serial_No is null)) "
                DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)
            Next

            ' Update tb_PurchaseOrder_RequestItem[Total_Received_Qty]
            strSQL = "update tb_pri set "
            strSQL &= "tb_pri.Total_Received_Qty=(select isnull(sum(Total_Qty),0) from tb_PurchaseOrderItem_PR inner join tb_PurchaseOrder on tb_PurchaseOrder.PurchaseOrder_Index=tb_PurchaseOrderItem_PR.PurchaseOrder_Index and tb_PurchaseOrderItem_PR.Status<>-1 and tb_PurchaseOrder.Status<>-1 where PurchaseOrder_RequestItem_Index=tb_pri.PurchaseOrder_RequestItem_Index) "
            strSQL &= "from tb_PurchaseOrder_RequestItem as tb_pri "
            strSQL &= "where tb_pri.PurchaseOrder_RequestItem_Index in (select PurchaseOrder_RequestItem_Index from tb_PurchaseOrderItem_PR where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index and PurchaseOrderItem_Index=@PurchaseOrderItem_Index group by PurchaseOrder_RequestItem_Index) "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Update tb_PurchaseOrder_Request[Status]
            strSQL = "update tb_pr set "
            strSQL &= "tb_pr.Status=(select case when isnull(sum(Total_Qty),0)=isnull(sum(Total_Received_Qty),0) then 3 else 2 end from tb_PurchaseOrder_RequestItem where Status<>-1 and PurchaseOrder_Request_Index=tb_pr.PurchaseOrder_Request_Index) "
            strSQL &= "from tb_PurchaseOrder_Request as tb_pr "
            strSQL &= "where tb_pr.PurchaseOrder_Request_Index in (select PurchaseOrder_Request_Index from tb_PurchaseOrderItem_PR where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index and PurchaseOrderItem_Index=@PurchaseOrderItem_Index group by PurchaseOrder_Request_Index) "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function confirmPurchaseOrderItem_PR(ByVal PurchaseOrder_PR_Index As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = "update tb_PurchaseOrderItem_PR set "
            strSQL &= "Status=2 "
            strSQL &= "where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            strSQL &= "and Status<>-1 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
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

#End Region

    Public Function Query(ByVal strQuery As String) As DataTable
        Dim _dt As New DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= strQuery
            _dt = DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
        Return _dt
    End Function

    Public Function getPurchaseOrder_Request_View(ByVal strWhere As String, Optional ByVal PagerType As enuPagerType = enuPagerType.All, Optional ByVal intTop As Integer = 100, Optional ByVal intRowPerPage As Integer = 50, Optional ByVal intPage As Integer = 1) As DataTable
        Dim strSQL As String = ""
        Try
            Select Case PagerType
                Case enuPagerType.All
                    strSQL &= String.Format(" select * from View_PurchaseOrder_Request_H where 1=1 {0} ", strWhere)
                Case enuPagerType.Top
                    strSQL &= String.Format(" select top {1} * from View_PurchaseOrder_Request_H where 1=1 {0} ", strWhere, intTop)
                Case enuPagerType.Page
                    strSQL &= String.Format(" declare @Sort as varchar(13); ")
                    strSQL &= String.Format(" set rowcount @StartRow; ")
                    strSQL &= String.Format(" select @Sort=PurchaseOrder_Request_Index from View_PurchaseOrder_Request_H order by PurchaseOrder_Request_Index asc; ")
                    strSQL &= String.Format(" set rowcount @PageSize; ")
                    strSQL &= String.Format(" select * from View_PurchaseOrder_Request_H where PurchaseOrder_Request_Index>=@Sort {0} ", strWhere)
                    With SQLServerCommand.Parameters
                        .Clear()
                        Dim intStartRow As Integer = intPage + ((intPage - 1) * intRowPerPage)
                        .Add("@StartRow", SqlDbType.Int).Value = intPage
                        .Add("@PageSize", SqlDbType.Int).Value = intRowPerPage
                    End With
            End Select
            strSQL &= " order by PurchaseOrder_Request_No asc,PurchaseOrder_Request_Index asc; "
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getCountPurchaseOrder_Request_View(ByVal strWhere As String) As Integer
        Dim strSQL As String = ""
        Try
            strSQL &= String.Format(" select count(*) from View_PurchaseOrder_Request_H where 1=1 {0} ", strWhere)
            Return CInt(DBExeQuery_Scalar(strSQL))
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getDtSchemaPurchaseOrder_Request() As DataTable
        Try
            Dim _dt As New DataTable
            _dt.Columns.Add("PurchaseOrder_Request_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrder_Request_No", GetType(String))
            _dt.Columns.Add("PurchaseOrder_Request_Date", GetType(Date))
            _dt.Columns.Add("Due_Date", GetType(Date))
            _dt.Columns.Add("DocumentType_Index", GetType(String))
            _dt.Columns.Add("Customer_Index", GetType(String))
            _dt.Columns.Add("Ref_No1", GetType(String))
            _dt.Columns.Add("Ref_No2", GetType(String))
            _dt.Columns.Add("Ref_No3", GetType(String))
            _dt.Columns.Add("Ref_No4", GetType(String))
            _dt.Columns.Add("Ref_No5", GetType(String))
            _dt.Columns.Add("Remark", GetType(String))
            _dt.Columns.Add("IsClosed", GetType(Boolean))
            _dt.Columns.Add("Closed_By", GetType(String))
            _dt.Columns.Add("Closed_Date", GetType(Date))
            _dt.Columns.Add("IsConfirm", GetType(Boolean))
            _dt.Columns.Add("Confirm_By", GetType(String))
            _dt.Columns.Add("Confirm_Date", GetType(Date))
            _dt.Columns.Add("IsCanCancel", GetType(Boolean))
            _dt.Columns.Add("Status", GetType(Integer))
            Return _dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getDtSchemaPurchaseOrder_RequestItem() As DataTable
        Try
            Dim _dt As New DataTable
            _dt.Columns.Add("PurchaseOrder_RequestItem_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrder_Request_Index", GetType(String))
            _dt.Columns.Add("Item_Seq", GetType(Integer))
            _dt.Columns.Add("Sku_Index", GetType(String))
            _dt.Columns.Add("Package_Index", GetType(String))
            _dt.Columns.Add("Serial_No", GetType(String))
            _dt.Columns.Add("Qty", GetType(Decimal))
            _dt.Columns.Add("Ratio", GetType(Decimal))
            _dt.Columns.Add("Total_Qty", GetType(Decimal))
            _dt.Columns.Add("Weight", GetType(Decimal))
            _dt.Columns.Add("Volume", GetType(Decimal))
            _dt.Columns.Add("Total_Received_Qty", GetType(Decimal))
            _dt.Columns.Add("Ref_No1", GetType(String))
            _dt.Columns.Add("Ref_No2", GetType(String))
            _dt.Columns.Add("Ref_No3", GetType(String))
            _dt.Columns.Add("Ref_No4", GetType(String))
            _dt.Columns.Add("Ref_No5", GetType(String))
            _dt.Columns.Add("Remark", GetType(String))
            _dt.Columns.Add("Status", GetType(Integer))

            _dt.Columns.Add("Due_Date", GetType(Date))

            Return _dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getDtSchemaPurchaseOrderItem_PR() As DataTable
        Try
            Dim _dt As New DataTable
            _dt.Columns.Add("PurchaseOrderItem_PR_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrder_PR_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrderItem_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrder_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrder_RequestItem_Index", GetType(String))
            _dt.Columns.Add("PurchaseOrder_Request_Index", GetType(String))
            _dt.Columns.Add("Sku_Index", GetType(String))
            _dt.Columns.Add("Package_Index", GetType(String))
            _dt.Columns.Add("Qty", GetType(Decimal))
            _dt.Columns.Add("Ratio", GetType(Decimal))
            _dt.Columns.Add("Total_Qty", GetType(Decimal))
            _dt.Columns.Add("Weight", GetType(Decimal))
            _dt.Columns.Add("Volume", GetType(Decimal))
            _dt.Columns.Add("Status", GetType(Integer))
            Return _dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getPurchaseOrder_Request(ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= String.Format(" select * from View_PurchaseOrder_Request_D where 1=1 {0} order by Item_Seq asc ", strWhere)
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getSKU_Package(ByVal Sku_Index As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " select ms_sr.Sku_Index,ms_sr.Package_Index,ms_pk.Description as [Package],ms_sr.Ratio,ms_pk.Weight,cast(((ms_pk.Dimension_Wd * ms_pk.Dimension_Len * ms_pk.Dimension_Hi) / ms_dmt.Ratio) as decimal(19,6)) as [Volume] from ms_SKURatio as ms_sr inner join ms_SKU as ms_s on ms_sr.Sku_Index=ms_s.Sku_Index inner join ms_Package as ms_pk on ms_sr.Package_Index=ms_pk.Package_Index inner join ms_DimensionType as ms_dmt on ms_pk.DimensionType_Index=ms_dmt.DimensionType_Index where ms_sr.Sku_Index=@Sku_Index and ms_sr.status_id<>-1 and ms_s.status_id<>-1 and isnull(ms_pk.isItem_Package,0)=0 "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = Sku_Index
            End With
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getPurchaseOrder_RequestItem_PO(ByVal PurchaseOrder_Request_Index As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " select * from View_PurchaseOrder_Request_POItem where PurchaseOrder_Request_Index=@PurchaseOrder_Request_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Request_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Request_Index
            End With
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getList_PurchaseOrder_Request(ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= String.Format(" select * from View_List_PurchaseOrder_Request where 1=1 {0} order by PurchaseOrder_Request_No asc,PurchaseOrder_Request_Index asc,Item_Seq asc ", strWhere)
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getConsolidate_PurchaseOrder_RequestItem(ByVal PurchaseOrder_PR_Index As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " select * from View_Consolidate_PurchaseOrder_RequestItem where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
            End With
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function getConsolidate_PurchaseOrderItem(ByVal PurchaseOrder_PR_Index As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " select * from View_Consolidate_PurchaseOrderItem where PurchaseOrder_PR_Index=@PurchaseOrder_PR_Index "
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_PR_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_PR_Index
            End With
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#Region " POTransaction.vb "

    Public Function Cancel_PurchaseOrder(ByVal oPurchaseOrder As WMS_STD_INB_PO_Datalayer.tb_PurchaseOrder) As Boolean

        Dim strSQL As String = ""
        Dim strSQL1 As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update status in tb_PurchaseOrderItem = -1 (Cancelled)
        ' --- STEP 2: Update status in tb_PurchaseOrder = -1 (Cancelled)
        ' --- STEP 3: Update status in tb_PurchaseOrderOther = -1 (Cancelled)
        ' --- STEP 4: Add log to Sy_Audit_Log
        Try

            ' --- STEP 1: Update status in tb_PurchaseOrderItem = -1 (Cancelled)
            strSQL = "UPDATE tb_PurchaseOrderItem "
            strSQL &= " SET status =-1, "
            strSQL &= " cancel_date=getdate(), cancel_by='" & W_Module.WV_UserName & "', "
            strSQL &= " cancel_branch='" & W_Module.WV_Branch_ID & "' "
            strSQL &= " WHERE PurchaseOrder_Index ='" & oPurchaseOrder.PurchaseOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Update status in tb_PurchaseOrder = -1 (Cancelled)
            strSQL1 = "UPDATE tb_PurchaseOrder "
            strSQL1 &= " SET status =-1, "
            strSQL1 &= " cancel_date=getdate(), cancel_by='" & W_Module.WV_UserName & "', "
            strSQL1 &= " cancel_branch='" & W_Module.WV_Branch_ID & "' "
            strSQL1 &= " WHERE PurchaseOrder_Index ='" & oPurchaseOrder.PurchaseOrder_Index & "'"


            SetSQLString = strSQL1
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 3: Update status in tb_PurchaseOrderOther = -1 (Cancelled)
            strSQL1 = "UPDATE tb_PurchaseOrderOther "
            strSQL1 &= " SET status =-1, "
            strSQL1 &= " cancel_date=getdate(), cancel_by='" & W_Module.WV_UserName & "', "
            strSQL1 &= " cancel_branch='" & W_Module.WV_Branch_ID & "' "
            strSQL1 &= " WHERE PurchaseOrder_Index ='" & oPurchaseOrder.PurchaseOrder_Index & "'"

            SetSQLString = strSQL1
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            With SQLServerCommand.Parameters
                .Clear()
                .Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = oPurchaseOrder.PurchaseOrder_Index
            End With
            ' Update tb_PurchaseOrder_RequestItem[Total_Received_Qty]
            strSQL = "update tb_pri set "
            strSQL &= "tb_pri.Total_Received_Qty=(select isnull(sum(Total_Qty),0) from tb_PurchaseOrderItem_PR inner join tb_PurchaseOrder on tb_PurchaseOrder.PurchaseOrder_Index=tb_PurchaseOrderItem_PR.PurchaseOrder_Index and tb_PurchaseOrderItem_PR.Status<>-1 and tb_PurchaseOrder.Status<>-1 where PurchaseOrder_RequestItem_Index=tb_pri.PurchaseOrder_RequestItem_Index) "
            strSQL &= "from tb_PurchaseOrder_RequestItem as tb_pri "
            strSQL &= "where tb_pri.PurchaseOrder_RequestItem_Index in (select PurchaseOrder_RequestItem_Index from tb_PurchaseOrderItem_PR where PurchaseOrder_Index=@PurchaseOrder_Index group by PurchaseOrder_RequestItem_Index) "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            ' Update tb_PurchaseOrder_Request[Status]
            strSQL = "update tb_pr set "
            strSQL &= "tb_pr.Status=(select case when isnull(sum(Total_Qty),0)=isnull(sum(Total_Received_Qty),0) then 3 else 2 end from tb_PurchaseOrder_RequestItem where Status<>-1 and PurchaseOrder_Request_Index=tb_pr.PurchaseOrder_Request_Index) "
            strSQL &= "from tb_PurchaseOrder_Request as tb_pr "
            strSQL &= "where tb_pr.PurchaseOrder_Request_Index in (select PurchaseOrder_Request_Index from tb_PurchaseOrderItem_PR where PurchaseOrder_Index=@PurchaseOrder_Index group by PurchaseOrder_Request_Index) "
            DBExeNonQuery(strSQL, Connection, myTrans, eCommandType.Text)

            '' --- STEP 4: Add new log in Sy_Audit_Log
            'Dim oAudit_Log As New Sy_Audit_Log
            'oAudit_Log.Document_Index = oPurchaseOrder.PurchaseOrder_Index
            'oAudit_Log.Document_No = oPurchaseOrder.PurchaseOrder_No
            'oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_PO, Connection, myTrans)

            myTrans.Commit()

            'insert log
            Dim obj_cls As New cls_syAditlog
            obj_cls.Process_ID = 9
            obj_cls.Description = "ยกเลิก : " & Now.ToString("dd/MM/yyyy HH:mm:ssss")
            obj_cls.Document_Index = oPurchaseOrder.PurchaseOrder_Index
            obj_cls.Document_No = oPurchaseOrder.PurchaseOrder_No
            obj_cls.Log_Type_ID = 904 'Cancel PO
            obj_cls.Insert_Master()


            Return True


        Catch e As Exception
            Try
                myTrans.Rollback()

                Return False

                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
    End Function

#End Region

#Region " Auto PR "

    Public Function getPurchaseOrder_Request_Auto(ByVal Customer_Index As String, ByVal ItemStatus_Index As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " exec sp_PurchaseOrder_Request_Auto @Customer_Index,@ItemStatus_Index "
            With SQLServerCommand.Parameters
                .Clear()
                If (Customer_Index = Nothing) Then
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                Else
                    .Add("@Customer_Index", SqlDbType.VarChar, 13).Value = Customer_Index
                End If
                If (ItemStatus_Index = Nothing) Then
                    .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DBNull.Value
                Else
                    .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = ItemStatus_Index
                End If
            End With
            Return DBExeQuery(strSQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

End Class
