Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_OUTB_Transport

Public Class PackingTransaction : Inherits DBType_SQLServer

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





#Region " OPERATION TYPE "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
        CANCEL
    End Enum
#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "

    Dim _objtb_SalesOrderPacking As New tb_SalesOrderPacking
    Dim _objtb_SalesOrderPackingItem As New tb_SalesOrderPackingItem
    Dim _objtb_SalesOrderPackingItemCollection As New List(Of tb_SalesOrderPackingItem)


    Private _newSOP_Index As String


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal Value As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)

    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objtb_SalesOrderPacking As tb_SalesOrderPacking, ByVal objtb_SalesOrderPackingItemCollection As List(Of tb_SalesOrderPackingItem))
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
        _objtb_SalesOrderPacking = objtb_SalesOrderPacking
        _objtb_SalesOrderPackingItemCollection = objtb_SalesOrderPackingItemCollection




    End Sub


#Region " SAVE PACKING "

    ''' <summary>
    ''' Save Data: Main function to call insert or update PO.
    ''' </summary>
    ''' <returns>New Purchase Order Index, if success. "", if failed.</returns>
    ''' <remarks> In case of new document, we need to generate new document ID.</remarks>
    Public Function SaveData() As String

        Select Case objStatus
            Case enuOperation_Type.ADDNEW

                Me._newSOP_Index = Me.InsertData()

                If Not Me._newSOP_Index = "" Then
                    ' Success 
                    Return Me._newSOP_Index
                Else
                    ' Not Success 
                    Return ""
                End If

                'Case enuOperation_Type.UPDATE
                '    Me._newPurchaseOrder_Index = Me.UpdateData
                '    If Not Me._newPurchaseOrder_Index = "" Then
                '        ' Success 
                '        Return Me._newPurchaseOrder_Index
                '    Else
                '        ' Not Success 
                '        Return ""
                '    End If

                'Case Else
                '    Return ""
        End Select


        Return ""
    End Function

#End Region
#End Region

#Region " ADD NEW SOP "
    Private Function InsertData() As String
        Dim strSQL As String = ""
        Dim strSQL1 As String = ""
        Dim strSQL2 As String = ""
        Dim strSQLOrderOther As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans



        Try



            ' --- STEP 1: Insert Header : tb_PurchaseOrder 
            strSQL = " INSERT INTO tb_SalesOrderPacking"
            strSQL &= "  ("
            strSQL &= "SalesOrderPacking_Index"
            strSQL &= " ,SalesOrder_Index"
            strSQL &= " ,BarcodePacking"
            strSQL &= " ,PackSize_Index"
            strSQL &= " ,Status_Print"
            strSQL &= " ,Count_Print"
            strSQL &= " ,seq"
            strSQL &= " ,TransportManifestItem_Index"
            strSQL &= " ,DateAdd"
            strSQL &= "  ) VALUES ( "
            strSQL &= "@SalesOrderPacking_Index"
            strSQL &= ",@SalesOrder_Index"
            strSQL &= ",@BarcodePacking"
            strSQL &= ",@PackSize_Index"
            strSQL &= ",@Status_Print"
            strSQL &= ",@Count_Print"
            strSQL &= ",(select isnull(max(seq),0) + 1 from tb_SalesOrderPacking WHERE  SalesOrder_Index = @SalesOrder_Index and Status_Print  <> -1 )  "
            strSQL &= ",@TransportManifestItem_Index"
            strSQL &= ",getdate()"
            strSQL &= "  ) "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SalesOrderPacking_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPacking.SalesOrderPacking_Index
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPacking.SalesOrder_Index
                .Parameters.Add("@BarcodePacking", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPacking.BarcodePacking
                .Parameters.Add("@PackSize_Index", SqlDbType.VarChar, 13).Value = _objtb_SalesOrderPacking.PackSize_Index
                .Parameters.Add("@Status_Print", SqlDbType.VarChar, 13).Value = _objtb_SalesOrderPacking.Status_Print
                .Parameters.Add("@Count_Print", SqlDbType.Int, 4).Value = _objtb_SalesOrderPacking.Count_Print
                '.Parameters.Add("@seq", SqlDbType.Int, 4).Value = _objtb_SalesOrderPacking.Seq
                .Parameters.Add("@TransportManifestItem_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPacking.TransportManifestItem_Index
                '.Parameters.Add("@DateAdd", SqlDbType.DateTime, 16).Value = _objtb_SalesOrderPacking.DateAdd
            End With
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Insert Line Item: PurchaseOrderItem 
            For Each _objtb_SalesOrderPackingItem In _objtb_SalesOrderPackingItemCollection

                strSQL = " INSERT INTO tb_SalesOrderPackingItem"
                strSQL &= "  ("
                strSQL &= "SalesOrderPackingItem_Index"
                strSQL &= " ,SalesOrderPacking_Index"
                strSQL &= " ,SalesOrder_Index"
                strSQL &= " ,TransportManifestItem_Index"
                strSQL &= " ,Sku_Index"
                strSQL &= " ,Qty_Pack"
                strSQL &= " ,Total_Qty"
                strSQL &= " ,seq"
                strSQL &= " ,SalesOrderItem_Index"
                strSQL &= "  ) VALUES ( "
                strSQL &= "@SalesOrderPackingItem_Index"
                strSQL &= ",@SalesOrderPacking_Index"
                strSQL &= ",@SalesOrder_Index"
                strSQL &= ",@TransportManifestItem_Index"
                strSQL &= ",@Sku_Index"
                strSQL &= ",@Qty_Pack"
                strSQL &= ",@Total_Qty"
                strSQL &= ",@seq"
                strSQL &= ",@SalesOrderItem_Index"
                strSQL &= "  ) "

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@SalesOrderPackingItem_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPackingItem.SalesOrderPackingItem_Index
                    .Parameters.Add("@SalesOrderPacking_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPackingItem.SalesOrderPacking_Index
                    .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPackingItem.SalesOrder_Index
                    .Parameters.Add("@TransportManifestItem_Index", SqlDbType.VarChar, 50).Value = "" '_objtb_SalesOrderPackingItem.TransportManifestItem_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objtb_SalesOrderPackingItem.Sku_Index
                    .Parameters.Add("@Qty_Pack", SqlDbType.Float, 8).Value = _objtb_SalesOrderPackingItem.Qty_Pack
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objtb_SalesOrderPackingItem.Total_Qty
                    .Parameters.Add("@seq", SqlDbType.NChar, 20).Value = _objtb_SalesOrderPackingItem.seq
                    .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar, 50).Value = _objtb_SalesOrderPackingItem.SalesOrderItem_Index
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                strSQL = " Update  TB_SALESORDERITEM "
                strSQL &= " Set Total_Qty_Packed+=@Qty_Pack"
                strSQL &= " Where  SalesOrderItem_Index=@SalesOrderItem_Index"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Qty_Pack", SqlDbType.Float, 8).Value = _objtb_SalesOrderPackingItem.Qty_Pack
                    .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar, 13).Value = _objtb_SalesOrderPackingItem.SalesOrderItem_Index
                End With
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next
            ' --- Commit transaction
            myTrans.Commit()
            Return Me._objtb_SalesOrderPacking.SalesOrderPacking_Index

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

#End Region

    Public Function GetPackingHeader(ByVal strWhere As String) As DataTable
        Dim strSQLDetail As String = " "
        Dim strSQLHeader As String = " "
        Try


            strSQLDetail = "(Select SalesOrder_Index  from VIEW_Packing Where 1 = 1 "
            strSQLDetail = strSQLDetail & strWhere & " )"
            strSQLHeader = "Select * from VIEW_Header_Packing Where SalesOrder_Index in (" & strSQLDetail & ") Order By Status_Pack,SalesOrder_Index "

            SetSQLString = strSQLHeader
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function GetPackingDetail(ByVal strWhere As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  Select * "
            strSQL &= " from VIEW_Packing "
            strSQL &= " Where 1 = 1 and (Status in (3,6,2)) " 'and Status_manifest  in ('9','10')"
            SetSQLString = strSQL & strWhere & " Order By Invoice_No "
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function




    Public Function UpdatePrint(ByVal pOPurchaseOrder As tb_SalesOrderPacking) As Boolean

        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try

            ' --- STEP 1: Update status in tb_SalesOrderPacking status_print =1 แสดงว่า พิมพ์แล้ว
            strSQL = "UPDATE tb_SalesOrderPacking "
            strSQL &= " SET Status_Print =@StatusPrint"
            strSQL &= " , Count_Print =@countPrint "
            strSQL &= " WHERE BarcodePacking ='" & pOPurchaseOrder.BarcodePacking & "'"

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@StatusPrint", SqlDbType.Int, 4).Value = pOPurchaseOrder.Status_Print
                .Parameters.Add("@countPrint", SqlDbType.VarChar, 13).Value = pOPurchaseOrder.Count_Print
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Update status in tb_SalesOrder status_pack = 1 ยังไม่ pack
            strSQL = "UPDATE tb_SalesOrder"
            strSQL &= " SET Status_Pack =@Status_Pack "
            'strSQL &= "   , update_by = @update_by "
            'strSQL &= "   , update_date = getdate() "
            'strSQL &= "   , update_branch = @update_branch "
            strSQL &= " WHERE SalesOrder_Index = @SalesOrder_Index "
            With SQLServerCommand
                .Parameters.Clear()

                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = pOPurchaseOrder.SalesOrder_Index
                .Parameters.Add("@Status_Pack", SqlDbType.Int).Value = 1
                '.Parameters.Add("@update_by", SqlDbType.VarChar).Value = W_Module.WV_UserName
                '.Parameters.Add("@update_branch", SqlDbType.Int).Value = W_Module.WV_Branch_ID
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            Dim dtTmp2 As New DataTable
            strSQL = " SELECT (isnull(Sum(Total_Qty),0) - isnull(Sum(Qty_Pack),0)) as Qty_Total "
            strSQL &= " FROM VIEW_Packing "
            strSQL &= " WHERE SalesOrder_Index ='" & pOPurchaseOrder.SalesOrder_Index & "'"
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(dtTmp2)

            If dtTmp2.Rows(0)("Qty_Total") <= 0 Then
                strSQL = " UPDATE tb_SalesOrder SET Status_Manifest = 16 "
                strSQL &= " WHERE SalesOrder_Index = '" & pOPurchaseOrder.SalesOrder_Index & "' "
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                strSQL = " select TM.TransportManifest_Index,SO.Status_Manifest"
                strSQL &= " from tb_TransportManifest TM "
                strSQL &= " 	inner join tb_TransportManifestItem TMI ON TM.TransportManifest_Index = TMI.TransportManifest_Index"
                strSQL &= " 	inner join tb_SalesOrder SO ON SO.SalesOrder_Index = TMI.SalesOrder_Index"
                strSQL &= " WHERE TMI.SalesOrder_Index = '" & pOPurchaseOrder.SalesOrder_Index & "' and SO.Status_Manifest = 16 "
                'strSQL &= " and TM.Status in (-1,1,4,3,6)"
                Dim xdt As New DataTable
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(xdt)
                If xdt.Rows.Count > 0 Then
                    Dim drCheck() As DataRow = xdt.Select("Status_Manifest <> 16")
                    If drCheck.Length = 0 Then
                        strSQL = " UPDATE tb_TransportManifest SET Status = 16 "
                        strSQL &= " WHERE TransportManifest_Index = '" & xdt.Rows(0).Item("TransportManifest_Index").ToString & "' "
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If

                End If
            End If

            '---------------------------------------------------------------------------------------------------
            'Update Withdraw
            dtTmp2 = New DataTable
            strSQL = " SELECT WI.Withdraw_Index,W.Withdraw_No" ',SUM(ISNULL(WIL.HandOver_Total_Qty,0)) HandOver_Total_Qty  "
            strSQL &= " ,SUM(ISNULL(CASE WHEN isnull(WI.TransportManifest_Index , '') = '' THEN WIL.Total_Qty ELSE WIL.HandOver_Total_Qty END,0)) HandOver_Total_Qty  "
            strSQL &= " FROM	tb_WithdrawItemLocation WIL "
            strSQL &= " 			inner join tb_WithdrawItem WI ON WIL.WithdrawItem_Index = WI.WithdrawItem_Index"
            strSQL &= " 			inner join tb_Withdraw W ON W.Withdraw_Index = WI.Withdraw_Index"
            strSQL &= " WHERE WI.Withdraw_Index in ( SELECT Withdraw_Index FROM tb_WithdrawItem WHERE Plan_Process = 10 AND  DocumentPlan_Index = '" & pOPurchaseOrder.SalesOrder_Index & "' )"
            strSQL &= " 	AND W.Status = 5"
            strSQL &= " GROUP BY  WI.Withdraw_Index,W.Withdraw_No"
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(dtTmp2)

            For Each drWD As DataRow In dtTmp2.Rows
                strSQL = " SELECT SUM(ISNULL(tb_SalesOrderPackingItem.Total_Qty,0)) Qty_Pack"
                strSQL &= " FROM tb_SalesOrderPackingItem INNER JOIN tb_SalesOrderPacking ON tb_SalesOrderPacking.SalesOrderPacking_Index = tb_SalesOrderPackingItem.SalesOrderPacking_Index "
                strSQL &= " WHERE tb_SalesOrderPacking.Status_Print <> -1 AND tb_SalesOrderPacking.SalesOrder_Index in "
                strSQL &= " ("
                strSQL &= " 	SELECT WI.DocumentPlan_Index"
                strSQL &= " 	FROM	tb_WithdrawItemLocation WIL "
                strSQL &= " 				inner join tb_WithdrawItem WI ON WIL.WithdrawItem_Index = WI.WithdrawItem_Index"
                strSQL &= " 				inner join tb_Withdraw W ON W.Withdraw_Index = WI.Withdraw_Index"
                strSQL &= " 	WHERE  W.Status = 5"
                strSQL &= " 		AND W.Withdraw_Index = '" & drWD("Withdraw_Index").ToString & "'"
                strSQL &= " )"
                Dim xdtt As New DataTable
                xdtt = DBExeQuery(strSQL, Connection, myTrans)
                Dim dblPACK As Double = 0
                If xdtt.Rows(0).Item(0).ToString = "" Then
                    dblPACK = 0
                Else
                    dblPACK = xdtt.Rows(0).Item(0)
                End If
                'Dim dblPACK As Double = DBExeQuery_Scalar(strSQL, Connection, myTrans)
                Dim dblPick As Double = dtTmp2.Compute("SUM(HandOver_Total_Qty)", "")
                If dblPACK = dblPick Then
                    xdtt = DBExeQuery(String.Format("select * from tb_Withdrawitem where Withdraw_Index = '{0}'", drWD("Withdraw_Index").ToString), Connection, myTrans)
                    If xdtt.Rows(0).Item("TransportManifest_Index").ToString = "" Then
                        strSQL = " Update tb_Withdraw "
                        strSQL &= " Set Activity_Id = 6,Activity = 'รอตัด Stock'"
                        strSQL &= " WHERE  Withdraw_Index = '" & drWD("Withdraw_Index").ToString & "' "
                        DBExeNonQuery(strSQL, Connection, myTrans)
                    Else
                        strSQL = " Update tb_Withdraw "
                        strSQL &= " Set Activity_Id = 5,Activity = 'รอโหลดสินค้า'"
                        strSQL &= " WHERE  Withdraw_Index = '" & drWD("Withdraw_Index").ToString & "' "
                        DBExeNonQuery(strSQL, Connection, myTrans)
                    End If
             
                Else
                    strSQL = " Update tb_Withdraw "
                    strSQL &= " Set Activity_Id = 4,Activity = 'รอแพ็คสินค้า'"
                    strSQL &= " WHERE  Withdraw_Index = '" & drWD("Withdraw_Index").ToString & "' "
                    DBExeNonQuery(strSQL, Connection, myTrans)
                End If
            Next
            '---------------------------------------------------------------------------------------------------


            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = pOPurchaseOrder.SalesOrder_Index
            oAudit_Log.Document_No = pOPurchaseOrder.BarcodePacking
            oAudit_Log.Str1 = pOPurchaseOrder.Count_Print
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_Packing, Connection, myTrans)
            myTrans.Commit()



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

    Public Function UpdateStatus(ByVal pstrSalesOrder_Index As String) As Boolean

        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try

            ' --- STEP 1: Update status in tb_SalesOrderPacking status_print =1 แสดงว่า พิมพ์แล้ว
            strSQL = "  UPDATE tb_SalesOrder SET "
            strSQL &= "  Status_Manifest =16"
            strSQL &= "  ,Status_Pack='2'"
            strSQL &= " WHERE SalesOrder_Index  =@pstrSalesOrder_Index"


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@pstrSalesOrder_Index", SqlDbType.VarChar, 13).Value = pstrSalesOrder_Index


            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()

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

    

    Public Function GetAllDataSOP(ByVal strWhere As String) As DataTable
        Dim strSQL As String = " "
        Try
            strSQL = "  Select TOP 500 * "
            strSQL &= " from VIEW_SalesOrderPacking"
            strSQL &= " Where 1 = 1 and status_print <> -1 "
            SetSQLString = strSQL & strWhere & "  order by seq"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

End Class
