Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer

Public Class ml_TSS : Inherits DBType_SQLServer
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

    Dim StatusWithdraw_Document As Withdraw_Document
    Private Enum Withdraw_Document
        SO = 10
        Packing = 7
        Reserve = 17
        Transport = 25
        Reservation = 30
    End Enum
    Public Function UpdateStatus(ByVal pstrTransportManifest_Index As String, ByVal pstrSalesOrder_Index As String, ByVal Vehicle_License_No As String) As Boolean
        Try
            Dim strSQL As String = ""




            strSQL = "  UPDATE tb_SalesOrder SET "
            strSQL &= "  Status_Manifest =13"
            strSQL &= " WHERE SalesOrder_Index  ='" & pstrSalesOrder_Index & "'"

            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            strSQL = "  UPDATE tb_SalesOrder SET "
            strSQL &= "  Status =5"
            strSQL &= " WHERE SalesOrder_Index  ='" & pstrSalesOrder_Index & "'"

            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            Dim odt As New DataTable

            strSQL = " SELECT * FROM tb_SalesOrder "
            strSQL &= " WHERE SalesOrder_Index in (select SalesOrder_Index FROM tb_TransportmanifestItem WHERE TransportManifest_Index  ='" & pstrTransportManifest_Index & "')"
            strSQL &= " AND Status_Manifest <> 13 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            odt = GetDataTable


            If odt.Rows.Count = 0 Then
                strSQL = "  UPDATE tb_TransportManifest SET "
                strSQL &= "  Status =13" 'dong Edit
                If Vehicle_License_No <> "" Then
                    strSQL &= "  ,Vehicle_License_No ='" & Vehicle_License_No & "'"
                End If
                strSQL &= " WHERE TransportManifest_Index  ='" & pstrTransportManifest_Index & "'"

                SetSQLString = strSQL
                connectDB()
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            End If


            Return True
        Catch ex As Exception

            Throw ex
            Return False
        Finally
            disconnectDB()

        End Try
    End Function
    Public Function LoaddingPackDisplay(ByVal pstrTransportManifest_No As String, ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT TransportManifest_Index,SalesOrder_Index,SalesOrder_No,Invoice_No,BarcodePacking,sum(Total_Qty) as Total_Qty,sum(Qty_Pack) as Qty_Pack,sum(Loading) as Loading"
            strSQL &= " ,StatusLoad,TransportManifest_No,Vehicle_License_No,IsPacking"
            strSQL &= " FROM View_TSS_Loading "
            strSQL &= " group by TransportManifest_Index,SalesOrder_Index,SalesOrder_No,BarcodePacking,StatusLoad,TransportManifest_No,Vehicle_License_No,Invoice_No,IsPacking,update_date"
            strSQL &= " having TransportManifest_No = '" & pstrTransportManifest_No & "'" & strWhere

            SetSQLString = strSQL & " Order by update_date desc "
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function

  
    Public Sub getSalesOrderItem(ByVal SalesOrder_Index As String)
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM tb_SalesOrderItem "
            strSQL &= " WHERE SalesOrder_Index ='" & SalesOrder_Index & "'"
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

    Public Sub getDocumentType(ByVal Process_Id As Integer, Optional ByVal strWhere As String = "")
        Dim strSQL As String

        Try

            strSQL = " SELECT * "
            strSQL &= " FROM VIEW_TSS_DocumentType"
            strSQL &= " WHERE Process_Id = " & Process_Id.ToString & strWhere
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

    Public Sub Cancel_Withdraw_HandOver_Packed_TSS(ByVal Withdraw_Index As String)

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans


        Try
            Dim strSQL As String = String.Empty
            strSQL = "   SELECT    *"
            strSQL &= "   FROM   VIEW_WithDrawCancel"
            strSQL &= "   WHERE Withdraw_Index ='" & Withdraw_Index & "'"


            With SQLServerCommand

                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0

            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            DS = New DataSet
            DataAdapter.Fill(DS, "tbl")

            If DS.Tables("tbl").Rows.Count <> 0 Then
                For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1
                    Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString
                    Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                    Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString

                    Dim Total_Qty As Double = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                    Dim Qty As Double = DS.Tables("tbl").Rows(i).Item("Qty")
                    Dim Weight As Double = DS.Tables("tbl").Rows(i).Item("Weight")
                    Dim Volume As Double = DS.Tables("tbl").Rows(i).Item("Volume")
                    Dim ItemQty As Double = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                    Dim Price As Double = DS.Tables("tbl").Rows(i).Item("Price")

                    StatusWithdraw_Document = Plan_Process
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.SO, Withdraw_Document.Transport
                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET HandOver_Total_Qty = 0"
                            strSQL &= "     ,Total_Qty_Packed =0"
                            'strSQL &= "     ,Qty_WithDraw =Qty_WithDraw-@Qty "
                            'strSQL &= "     ,Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                            'strSQL &= "      Weight_Withdraw =Weight_Withdraw-@Weight "
                            'strSQL &= "     ,Volume_Withdraw =Volume_Withdraw-@Volume "
                            strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                    End Select
                    If strSQL <> "" Then
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty")
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                            .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                        End With
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        SetSQLString = " UPDATE tb_SalesOrder SET Status_Pack = 1,Status_Manifest = 8 WHERE SalesOrder_Index = '" & DocumentPlan_Index & "'"
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                    End If
                Next
            End If


            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Sub fnUPDATE_LOCATOR(ByVal TAG_Index As String, ByVal ERP_Location As String)
        Try
            connectDB()
            SetSQLString = " UPDATE tb_LocationBalance SET ERP_Location = '" & ERP_Location & "' WHERE TAG_Index = '" & TAG_Index & "'"
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            SetSQLString = " UPDATE tb_Transaction SET ERP_Location_From = '" & ERP_Location & "',ERP_Location_TO = '" & ERP_Location & "' WHERE TAG_Index = '" & TAG_Index & "'"
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Sub fnUPDATE_CUSTOMER_SHIPPING_LOCATION(ByVal TransportManifestItem_Index As String, ByVal SalesOrder_Index As String, ByVal Customer_Shipping_Location_Index As String)
        Try
            connectDB()

            Dim xQuery As String = ""
            xQuery = String.Format("Select * from ms_Customer_Shipping_Location where Customer_Shipping_Location_Index = '{0}' ", Customer_Shipping_Location_Index)
            Dim xTable As DataTable = DBExeQuery(xQuery)
            Dim drRow As DataRow = xTable.Rows(0)
            If xTable.Rows.Count > 0 Then
                'Update SalesOrder
                xQuery = " UPDATE tb_SalesOrder "
                xQuery &= " SET Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index "
                xQuery &= "     ,Route_Index = @Route_Index "
                xQuery &= "     ,SubRoute_Index = @SubRoute_Index "
                xQuery &= "     ,Str9 = @Str9 "
                xQuery &= "     ,Str4 = @Str4"
                xQuery &= "     ,Str5 = @Str5"
                xQuery &= " WHERE SalesOrder_Index = @SalesOrder_Index"
                'Update TransportManifest Order
                xQuery &= " UPDATE tb_TransportManifestItem "
                xQuery &= "  SET Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index "
                xQuery &= " WHERE TransportManifestItem_Index = @TransportManifestItem_Index"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@TransportManifestItem_Index", SqlDbType.VarChar).Value = TransportManifestItem_Index
                    .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = SalesOrder_Index
                    .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar).Value = drRow("Customer_Shipping_Location_Index").ToString
                    .Parameters.Add("@Route_Index", SqlDbType.VarChar).Value = drRow("Route_Index").ToString
                    .Parameters.Add("@SubRoute_Index", SqlDbType.VarChar).Value = drRow("SubRoute_Index").ToString
                    .Parameters.Add("@Str9", SqlDbType.VarChar).Value = drRow("Address").ToString
                    .Parameters.Add("@Str4", SqlDbType.VarChar).Value = drRow("Tel").ToString
                    .Parameters.Add("@Str5", SqlDbType.VarChar).Value = drRow("Fax").ToString
                End With
                SetSQLString = xQuery
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                connectDB()
                EXEC_Command()


            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


#Region "   Admin TransportManifest   "



    Public Function LoaddingPack(ByVal pstrTransportManifest_No As String, ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM View_TSS_Loading "

            strSQL &= " WHERE TransportManifest_No = '" & pstrTransportManifest_No & "' " & strWhere

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function UpdateLoading(ByVal pstrSalesOrderPacking_Index As String, ByVal Loading_Qty As Integer) As Boolean
        Try
            Dim strSQL As String = ""
            strSQL = "  UPDATE tb_SalesOrderPackingItem SET "
            strSQL &= "  Loading_Qty =Qty_Pack  ,update_date = getdate()" '"
            strSQL &= " WHERE SalesOrderPacking_Index  ='" & pstrSalesOrderPacking_Index & "'"

            SetSQLString = strSQL
            connectDB()
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            Return True
        Catch ex As Exception

            Throw ex
            Return False
        Finally
            disconnectDB()

        End Try
    End Function
    Public Function GetCheckingBarcodeDupp(ByVal pSku_Barcode As String) As DataTable

        Try
            Dim strSQL As String = ""
            Dim oDT As New DataTable
            'strSQL = "  SELECT BC.Sku_Index "
            'strSQL &= " FROM tb_Barcode_Mapping BC INNER JOIN ms_Sku ON ms_Sku.Sku_Index = BC.Sku_Index "
            'strSQL &= " WHERE BC.Barcode =  '" & pSku_Barcode & "' AND ms_Sku.Status_Id <> -1 "

            strSQL &= "  SELECT * FROM VIEW_Barcode_Mapping WHERE Barcode =  '" & pSku_Barcode & "'"


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            oDT = GetDataTable
            Return oDT
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub getVIEW_TSS_Mobile_TransportmanifestItem(ByVal strwhere As String)
        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT  * "
            strSQL &= " FROM     VIEW_TSS_Mobile_TransportmanifestItem "
            strSQL &= " WHERE 1=1 "
            SetSQLString = strSQL & strwhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Function GetDetail_HandOver_Group(ByVal TransportManifest_No As String) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL &= " select * from View_TSS_Mobile_HandOver_Group "
            strSQL &= " where TransportManifest_No = '" & TransportManifest_No & "'"
            strSQL &= " Order by StatusHandOver "
            SetSQLString = strSQL
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

    Public Function UpdateDetail_HandOver(ByVal HandOver_TotalQty As Double, ByVal Sku_Index As String, ByVal Transportmanifest_Index As String, ByVal strBarcodeScan As String) As String
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim StrReturn As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            '-----------------------------------------------------------------------

            strSQL = " Update tb_SalesOrderItem Set HandOver_Total_Qty = Total_Qty "
            strSQL &= " WHERE Sku_Index  = '" & Sku_Index & "' AND SalesOrder_Index in (select SalesOrder_Index from tb_TransportmanifestItem where Transportmanifest_index = '" & Transportmanifest_Index & "')  "
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            '-----------------------------------------------------------------------

            strSQL = " select sum(Total_Qty) as 'Qty_WithDraw' ,sum(HandOver_Total_Qty) as 'Qty_SalesOrder' from View_TSS_Mobile_HandOver "
            strSQL &= " WHERE TransportManifest_Index = '" & Transportmanifest_Index & "'"
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            DS = New DataSet
            DataAdapter.Fill(DS, "tb_Check")


            '-----------------------------------------------------------------------


            strSQL = " select * from View_TSS_Mobile_HandOver "
            strSQL &= " where TransportManifest_Index = '" & Transportmanifest_Index & "' "
            strSQL &= " and Sku_Index  = '" & Sku_Index & "' "

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(DS, "View_TSS_Mobile_HandOver")

            Dim objDBTempIndex As New WMS_STD_Formula.Sy_AutoNumber
            For Each Dr As DataRow In DS.Tables("View_TSS_Mobile_HandOver").Rows
                If CBool(Dr("IsPacking")) = True Then
                    'If CDbl(DS.Tables("tb_Check").Rows(0)("Qty_SalesOrder")) >= CDbl(DS.Tables("tb_Check").Rows(0)("Qty_WithDraw")) Then
                    '    'ÃÍá¾ç¤ÊÔ¹¤éÒ
                    '    'strSQL = "Update tb_SalesOrder set Status_Manifest = 9 where SalesOrder_Index = '" & Dr("SalesOrder_Index").ToString & "'"
                    '    strSQL = "Update tb_SalesOrder set Status_Manifest = 9 where  SalesOrder_Index in (select SalesOrder_Index from tb_TransportmanifestItem where Transportmanifest_index = '" & Transportmanifest_Index & "' where ISNULL(IsPacking, 0) = 1)  "
                    '    SetSQLString = strSQL
                    '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    '    EXEC_Command()
                    'End If
                Else

                    Dim SalesOrderPacking_Index As String = objDBTempIndex.getSys_Value("SalesOrderPacking_Index")
                    Dim SalesOrderPackingItem_Index As String = objDBTempIndex.getSys_Value("SalesOrderPackingItem_Index")
                    ' --- STEP 1: Insert Header : tb_PurchaseOrder 
                    strSQL = " INSERT INTO tb_SalesOrderPacking"
                    strSQL &= "  ("
                    strSQL &= "SalesOrderPacking_Index"
                    strSQL &= " ,SalesOrder_Index"
                    strSQL &= " ,BarcodePacking"
                    strSQL &= " ,PackSize_Index"
                    strSQL &= " ,Status_Print"
                    strSQL &= " ,Count_Print"
                    strSQL &= " ,TransportManifestItem_Index"
                    strSQL &= " ,DateAdd"
                    strSQL &= "  ) VALUES ( "
                    strSQL &= "@SalesOrderPacking_Index"
                    strSQL &= ",@SalesOrder_Index"
                    strSQL &= ",@BarcodePacking"
                    strSQL &= ",@PackSize_Index"
                    strSQL &= ",@Status_Print"
                    strSQL &= ",@Count_Print"

                    strSQL &= ",@TransportManifestItem_Index"
                    strSQL &= ",getdate()"
                    strSQL &= "  ) "

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@SalesOrderPacking_Index", SqlDbType.VarChar, 50).Value = SalesOrderPacking_Index
                        .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = Dr("SalesOrder_Index")
                        .Parameters.Add("@BarcodePacking", SqlDbType.VarChar, 50).Value = strBarcodeScan 'Dr("Barcode1")
                        .Parameters.Add("@PackSize_Index", SqlDbType.VarChar, 13).Value = "0010000000003"
                        .Parameters.Add("@Status_Print", SqlDbType.VarChar, 13).Value = "0"
                        .Parameters.Add("@Count_Print", SqlDbType.Int, 4).Value = 0
                        .Parameters.Add("@TransportManifestItem_Index", SqlDbType.VarChar, 50).Value = Transportmanifest_Index

                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

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
                        .Parameters.Add("@SalesOrderPackingItem_Index", SqlDbType.VarChar, 50).Value = SalesOrderPackingItem_Index
                        .Parameters.Add("@SalesOrderPacking_Index", SqlDbType.VarChar, 50).Value = SalesOrderPacking_Index
                        .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar, 50).Value = Dr("SalesOrder_Index")
                        .Parameters.Add("@TransportManifestItem_Index", SqlDbType.VarChar, 50).Value = Dr("TransportManifestItem_Index")
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = Dr("Sku_Index")
                        .Parameters.Add("@Qty_Pack", SqlDbType.Float, 8).Value = Dr("Total_Qty")
                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = Dr("Total_Qty")
                        .Parameters.Add("@seq", SqlDbType.NChar, 20).Value = 1
                        .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar, 50).Value = Dr("SalesOrderItem_Index")
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    'Update tb_SalesOrderItem  At  Total_Qty_Packed
                    strSQL = " Update  TB_SALESORDERITEM "
                    strSQL &= " Set Total_Qty_Packed=@Qty_Pack"
                    strSQL &= " Where  SalesOrderItem_Index=@SalesOrderItem_Index"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Qty_Pack", SqlDbType.Float, 8).Value = Dr("Total_Qty")
                        .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar, 13).Value = Dr("SalesOrderItem_Index")
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                End If
            Next

            If CDbl(DS.Tables("tb_Check").Rows(0)("Qty_SalesOrder")) >= CDbl(DS.Tables("tb_Check").Rows(0)("Qty_WithDraw")) Then

                '-----------------------------------------------------------
                strSQL = " select SalesOrder_Index,IsPacking from View_TSS_Mobile_HandOver "
                strSQL &= " where TransportManifest_Index = '" & Transportmanifest_Index & "' "
                strSQL &= " Group by SalesOrder_Index,IsPacking "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DS, "PASSED")
                For Each DrPASSED As DataRow In DS.Tables("PASSED").Rows
                    If CBool(DrPASSED("IsPacking")) = True Then
                        'Pack
                        strSQL = "Update tb_SalesOrder set Status_Manifest = 9 where SalesOrder_Index = '" & DrPASSED("SalesOrder_Index").ToString & "'"
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Else
                        'Load
                        strSQL = "Update tb_SalesOrder set Status_Manifest = 16 where SalesOrder_Index = '" & DrPASSED("SalesOrder_Index").ToString & "'"
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If

                Next

                '-----------------------------------------------------------

                strSQL = " select Wi.Withdraw_Index from tb_withdrawitem Wi "
                strSQL &= " Inner Join tb_Withdraw W On W.WithDraw_Index = Wi.WithDraw_Index "
                strSQL &= " where Wi.DocumentPlan_Index IN  (select SalesOrder_Index from tb_TransportmanifestItem where Transportmanifest_index = '" & Transportmanifest_Index & "')  "
                strSQL &= "     AND W.Status <> -1 "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DS, "tb_WithDrawIndexConFrim")
                Dim DtConFrim As New DataTable
                DtConFrim = SelectDistinct(DS.Tables("tb_WithDrawIndexConFrim"), "Withdraw_Index")

                strSQL = " update tb_Withdraw"
                strSQL &= " Set Status = 1 "
                strSQL &= " Where WithDraw_Index In ('xxxxxxxxx'"

                For Each Dr As DataRow In DtConFrim.Rows
                    strSQL &= ",'" & Dr("WithDraw_Index").ToString & "'"
                Next

                strSQL &= ")"
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()




                myTrans.Commit()

                For Each dr As DataRow In DtConFrim.Rows
                    Dim objClassDB As New WMS_STD_OUTB_WithDraw_Datalayer.WithdrawTransaction(WMS_STD_OUTB_WithDraw_Datalayer.WithdrawTransaction.enuOperation_Type.CHECK_DATA)
                    Dim strchkWithDraw As String = objClassDB.Withdraw_Confirm(dr("Withdraw_Index").ToString)
                Next

                StrReturn = "FINISH"
            Else
                myTrans.Commit()
                StrReturn = "PASS"
            End If

        Catch e As Exception
            Try
                myTrans.Rollback()
                Return e.Message.ToString
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Return ex.Message.ToString
                End If
            End Try
        Finally
            disconnectDB()
        End Try
        Return StrReturn
    End Function


#Region "Distinct_DataTable"
    Public Shared Function SelectDistinct(ByVal SourceTable As DataTable, ByVal ParamArray FieldNames() As String) As DataTable
        Dim lastValues() As Object
        Dim newTable As DataTable

        If FieldNames Is Nothing OrElse FieldNames.Length = 0 Then
            Throw New ArgumentNullException("FieldNames")
        End If

        lastValues = New Object(FieldNames.Length - 1) {}
        newTable = New DataTable

        For Each field As String In FieldNames
            newTable.Columns.Add(field, SourceTable.Columns(field).DataType)
        Next

        For Each Row As DataRow In SourceTable.Select("", String.Join(", ", FieldNames))
            If Not fieldValuesAreEqual(lastValues, Row, FieldNames) Then
                newTable.Rows.Add(createRowClone(Row, newTable.NewRow(), FieldNames))

                setLastValues(lastValues, Row, FieldNames)
            End If
        Next

        Return newTable
    End Function

    Private Shared Function fieldValuesAreEqual(ByVal lastValues() As Object, ByVal currentRow As DataRow, ByVal fieldNames() As String) As Boolean
        Dim areEqual As Boolean = True

        For i As Integer = 0 To fieldNames.Length - 1
            If lastValues(i) Is Nothing OrElse Not lastValues(i).Equals(currentRow(fieldNames(i))) Then
                areEqual = False
                Exit For
            End If
        Next

        Return areEqual
    End Function

    Private Shared Function createRowClone(ByVal sourceRow As DataRow, ByVal newRow As DataRow, ByVal fieldNames() As String) As DataRow
        For Each field As String In fieldNames
            newRow(field) = sourceRow(field)
        Next

        Return newRow
    End Function

    Private Shared Sub setLastValues(ByVal lastValues() As Object, ByVal sourceRow As DataRow, ByVal fieldNames() As String)
        For i As Integer = 0 To fieldNames.Length - 1
            lastValues(i) = sourceRow(fieldNames(i))
        Next
    End Sub
#End Region
#End Region

    Public Function getZone_Sku(ByVal Sku_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT * "
            strSQL &= " FROM tb_Zone_SKU "
            strSQL &= " WHERE Sku_Index = '" & Sku_Index & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return True
            End If
            Return False

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub UPDATE_QTY_PER_TAG(ByVal OrderItem_Index As String, ByVal QTY_PER_TAG As String)
        Try
            connectDB()
            SetSQLString = " UPDATE tb_OrderItem SET Flo5 = '" & QTY_PER_TAG & "' WHERE OrderItem_Index = '" & OrderItem_Index & "'"
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub



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

    Public Sub UPDATE_STATUS_SO(ByVal SalesOrder_Index As String, ByVal Status As Integer)
        Try
            connectDB()
            SetSQLString = " UPDATE tb_SalesOrder SET Status = '" & Status & "' , update_by ='" & WMS_STD_Formula.W_Module.WV_UserName & "', update_date = getdate() WHERE SalesOrder_Index = '" & SalesOrder_Index & "'"
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function getTSS_TRANSFER_RESERVE_PACKINGLIST(Optional ByVal strWhere As String = "") As DataSet
        Dim strSQL As String
        Dim DS As New DataSet
        Try

            strSQL = " EXEC dbo.sp_TSS_TRANSFER_RESERVE_PACKINGLIST '" & strWhere & "'"
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If Not _dataTable.Columns.Contains("Comment") Then
                _dataTable.Columns.Add("Comment", GetType(String))
            End If

            strSQL = " SELECT Comment "
            strSQL &= " FROM tb_TransferStatus TR "
            strSQL &= " WHERE isnull(Comment,'') <> '' " & strWhere.Replace("''", "'")
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Dim _dataTable2 As New DataTable
            _dataTable2 = GetDataTable
            Dim strdrComment As String = ""
            For Each drComment As DataRow In _dataTable2.Rows
                strdrComment &= drComment("Comment") & IIf(_dataTable2.Rows.Count > 1, ",", "")
            Next

            If strdrComment.Trim <> "" Then
                For Each drComment As DataRow In _dataTable.Rows
                    drComment("Comment") = strdrComment
                Next
            End If


            DS.Tables.Add(_dataTable)

            Return DS

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


End Class
