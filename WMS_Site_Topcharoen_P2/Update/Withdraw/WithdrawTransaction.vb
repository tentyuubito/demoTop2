Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_Std_master.W_Language
Imports WMS_STD_Master
Imports WMS_STD_OUTB_Datalayer
Imports WMS_STD_OUTB_WithDraw_Datalayer

Public Class WithdrawTransaction : Inherits DBType_SQLServer
    ' Header : tb_Withdraw
    ' Item   : tb_WithdrawItem
    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _Packing_index As String = ""

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
    WriteOnly Property Packing_index() As String
        Set(ByVal value As String)
            _Packing_index = value
        End Set
    End Property
    Dim StatusWithdraw_Document As Withdraw_Document
    Private Enum Withdraw_Document
        SO = 10
        Packing = 7
        Reserve = 17
        Transport = 25
        Reservation = 30
    End Enum


#Region "   Operation Type   "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
        CHECK_DATA
        CANCEL
    End Enum
#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "

    Private _objHeader As New tb_Withdraw
    Private _objItemCollection As List(Of tb_WithdrawItem)
    Private _objItemCollectionWITL As List(Of tb_WithdrawItemLocation)

    Private _objItemWITL As New tb_WithdrawItemLocation
    Private _objItem As New tb_WithdrawItem
    Private _newWithdraw_Index As String

    Private _objPalletType As New tb_PalletType_History
    Private _objPalletTypeCollection As New List(Of tb_PalletType_History)

    Private Whitdraw_Index As String = ""

    Public Property NewWithdraw_Index() As String
        Get
            Return _newWithdraw_Index
        End Get
        Set(ByVal value As String)
            _newWithdraw_Index = value
        End Set
    End Property
    Property DeleteWhitdrawItem_Index() As String
        Get
            Return Whitdraw_Index
        End Get
        Set(ByVal value As String)
            Whitdraw_Index = value
        End Set
    End Property


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
    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objHeader As tb_Withdraw, ByVal objItemCollection As List(Of tb_WithdrawItem))
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
        _objHeader = objHeader
        _objItemCollection = objItemCollection


    End Sub


    '--- Old WithDraw
    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objHeader As tb_Withdraw, ByVal objItemCollection As List(Of tb_WithdrawItem), ByVal objPalletTypeCollection As List(Of tb_PalletType_History))
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
        _objHeader = objHeader
        _objItemCollection = objItemCollection
        _objPalletTypeCollection = objPalletTypeCollection

    End Sub


    '--- New WithDraw
    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objHeader As tb_Withdraw, ByVal objItemCollection As List(Of tb_WithdrawItem), ByVal objItemCollectionWITL As List(Of tb_WithdrawItemLocation), ByVal objPalletTypeCollection As List(Of tb_PalletType_History))
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
        _objHeader = objHeader
        _objItemCollection = objItemCollection
        _objItemCollectionWITL = objItemCollectionWITL
        _objPalletTypeCollection = objPalletTypeCollection

    End Sub

#End Region

#Region "WithdrawView"
    Public Sub getWithdrawView(ByVal WhereString As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     tb_Withdraw.Withdraw_Index, tb_Withdraw.Withdraw_No, tb_Withdraw.Withdraw_Date, tb_Withdraw.Ref_No1, tb_Withdraw.Ref_No2, tb_Withdraw.Ref_No3, tb_Withdraw.Ref_No4, tb_Withdraw.add_by ," & _
                  "               tb_Withdraw.Ref_No5, ms_Customer.Customer_Index, ms_Customer.Customer_Id, ms_Customer.Title, ms_Customer.Customer_Name, " & _
                  "               ms_ProcessStatus.Status, ms_ProcessStatus.Description as StatusDescription ,ms_DocumentType.Description as DocumentType,tb_Withdraw.Ref_No1" & _
                  "    FROM       tb_Withdraw INNER JOIN " & _
                  "               ms_Customer ON tb_Withdraw.Customer_Index = ms_Customer.Customer_Index INNER JOIN " & _
                  "               ms_ProcessStatus ON tb_Withdraw.Status = ms_ProcessStatus.Status LEFT OUTER JOIN " & _
                  "               ms_DocumentType ON tb_Withdraw.DocumentType_Index = ms_DocumentType.DocumentType_Index LEFT OUTER JOIN " & _
                  "               ms_Department ON tb_Withdraw.Department_Index = ms_Department.Department_Index " & _
"    WHERE ms_ProcessStatus.Process_Id =2   "



            SetSQLString = strSQL & WhereString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getWithdrawView3PL(ByVal WhereString As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "      SELECT  * "
            strSQL &= "     FROM    VIEW_3PL_WithDrawView"
            strSQL &= "     WHERE   VIEW_3PL_WithDrawView.Process_Id =2   "



            SetSQLString = strSQL + WhereString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="WhereString"></param>
    ''' <remarks>
    ''' Update Date : 02/04/2010
    ''' Update By   : TaTa
    '''     เพิ่ม UserName,AssignJob_Index (AssignJob)
    ''' </remarks>
    Public Sub getViewWithdrawAsset(ByVal WhereString As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'strSQL = "      SELECT  * "
            'strSQL &= "     FROM    VIEW_WithDrawView"
            'strSQL &= "     WHERE   VIEW_WithDrawView.Process_Id =2   "

            strSQL = "      SELECT  * "
            strSQL &= "     ,(select top 1 se_user.userFullName from tb_AssignJob inner join se_user on  tb_AssignJob.user_Index = se_user.user_Index where VIEW_WithDrawView.WithDraw_Index=tb_AssignJob.DocumentPlan_Index AND Plan_Process=2 )userFullName "
            strSQL &= "                        ,(select top 1 tb_AssignJob.AssignJob_Index from tb_AssignJob inner join se_user on  tb_AssignJob.user_Index = se_user.user_Index where VIEW_WithDrawView.WithDraw_Index=tb_AssignJob.DocumentPlan_Index AND Plan_Process=2 )AssignJob_Index "
            strSQL &= "     FROM    VIEW_WithDrawView  "
            'strSQL &= "     Left Join (select * from tb_AssignJob where Plan_Process=2) AssignJob ON "
            'strSQL &= "     AssignJob.DocumentPlan_Index = WithDraw_Index Left Join se_user ON"
            'strSQL &= "     AssignJob.user_Index = se_user.user_Index"
            strSQL &= "     WHERE   VIEW_WithDrawView.Process_Id =2      "

            SetSQLString = strSQL & WhereString & " ORDER BY VIEW_WithDrawView.Withdraw_No,VIEW_WithDrawView.Withdraw_Date"
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    'Add By Pong 28/04/2015
    Public Sub getViewWithdrawAsset(ByVal WhereString As String, ByVal pintRowStart As Integer, ByVal pintRowEnd As Integer)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'strSQL = "      SELECT  * "
            'strSQL &= "     FROM    VIEW_WithDrawView"
            'strSQL &= "     WHERE   VIEW_WithDrawView.Process_Id =2   "
            strSQL &= "SELECT * FROM ("
            strSQL &= " SELECT ROW_NUMBER() OVER(ORDER BY VIEW_WithDrawView.Withdraw_No ASC,VIEW_WithDrawView.Withdraw_Date ASC) AS ROW_NO, Count(*) Over() AS ROW_COUNT"
            strSQL &= " ,* "
            'strSQL &= "     ,(select top 1 se_user.userFullName from tb_AssignJob inner join se_user on  tb_AssignJob.user_Index = se_user.user_Index where VIEW_WithDrawView.WithDraw_Index=tb_AssignJob.DocumentPlan_Index AND Plan_Process=2 )userFullName "
            'strSQL &= "                        ,(select top 1 tb_AssignJob.AssignJob_Index from tb_AssignJob inner join se_user on  tb_AssignJob.user_Index = se_user.user_Index where VIEW_WithDrawView.WithDraw_Index=tb_AssignJob.DocumentPlan_Index AND Plan_Process=2 )AssignJob_Index "
            strSQL &= "     FROM    VIEW_WithDrawView  "
            'strSQL &= "     Left Join (select * from tb_AssignJob where Plan_Process=2) AssignJob ON "
            'strSQL &= "     AssignJob.DocumentPlan_Index = WithDraw_Index Left Join se_user ON"
            'strSQL &= "     AssignJob.user_Index = se_user.user_Index"
            strSQL &= "     WHERE   VIEW_WithDrawView.Process_Id =2      "

            If WhereString <> "" Then
                strSQL &= WhereString
            End If
            'strSQL &= " ORDER BY VIEW_WithDrawView.Withdraw_No,VIEW_WithDrawView.Withdraw_Date"

            strSQL &= " ) AS SO_View "

            If (pintRowStart > 0) Then
                strSQL &= " WHERE ROW_NO BETWEEN " & pintRowStart & " AND " & pintRowEnd
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

    Public Sub getProcessStatus()

        ' *** define value  ***

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT * "
            strSQL &= " FROM     VIEW_ProcessStatus "
            strSQL &= " WHERE Process_Id= 2  AND Show = 1  And status_id <> -1  "
            strSQL &= " ORDER BY Seq ASC "


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

    Public Sub getWithdrawList_For_JobWithdraw(ByVal WhereString As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " SELECT     tb_Withdraw.Withdraw_Index, tb_Withdraw.Withdraw_No, tb_Withdraw.Withdraw_Date, tb_Withdraw.Ref_No1, tb_Withdraw.Ref_No2, tb_Withdraw.Ref_No3, tb_Withdraw.Ref_No4, tb_Withdraw.add_by ," & _
                  "               tb_Withdraw.Ref_No5, ms_Customer.Customer_Index, ms_Customer.Customer_Id, ms_Customer.Title, ms_Customer.Customer_Name, " & _
                  "               ms_ProcessStatus.Status, ms_ProcessStatus.Description as StatusDescription " & _
                  "    FROM       tb_Withdraw INNER JOIN " & _
                  "               ms_Customer ON tb_Withdraw.Customer_Index = ms_Customer.Customer_Index INNER JOIN " & _
                  "               ms_ProcessStatus ON tb_Withdraw.Status = ms_ProcessStatus.Status " & _
                  "    WHERE ms_ProcessStatus.Process_Id =2  AND tb_Withdraw.Status not in ( 2,-1 )   "



            SetSQLString = strSQL & WhereString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getWithdrawAutpReceive(ByVal Withdraw_Index As String)

        Dim strSQL As String = ""
       Try

            strSQL = "      SELECT  * "
            strSQL &= "     FROM    VIEW_USE_AUTO_RECEIVE"
            strSQL &= "     WHERE   Withdraw_Index ='" & Withdraw_Index & "' "


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

    Public Sub getAutoReceiveHeader(ByVal Withdraw_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "      SELECT  * "
            strSQL &= "     FROM    tb_Withdraw"
            strSQL &= "     WHERE   Withdraw_Index ='" & Withdraw_Index & "' "


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

    Public Sub getAutoReceiveDetail(ByVal Withdraw_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "      SELECT  * "
            strSQL &= "     FROM    tb_WithdrawItem"
            strSQL &= "     WHERE   Withdraw_Index ='" & Withdraw_Index & "' "


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

#Region "SAVE Withdraw "
    'Public Overridable Sub SaveWithdrawConfirm(ByVal Withdraw_Index As String)
    Public Function SaveWithdrawConfirm(ByVal Withdraw_Index As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As String

        Dim strSQL As String = ""

        Dim Qty_Sku_Bal As Decimal = 0
        Dim Weight_Sku_Bal As Decimal = 0
        Dim Volume_Sku_Bal As Decimal = 0
        Dim Qty_PLot_Bal As Decimal = 0
        Dim Weight_PLot_Bal As Decimal = 0
        Dim Volume_PLot_Bal As Decimal = 0
        Dim Qty_ItemStatus_Bal As Decimal = 0
        Dim Weight_ItemStatus_Bal As Decimal = 0
        Dim Volume_ItemStatus_Bal As Decimal = 0

        Dim Qty_Sku_Location_Bal As Decimal = 0
        Dim Qty_ItemStatus_Location_Bal As Decimal = 0
        Dim Qty_PLot_Location_Bal As Decimal = 0

        Dim myTrans As SqlClient.SqlTransaction = Nothing

        Dim IsMyTransaction As Boolean = Transaction Is Nothing
        If IsMyTransaction Then
            connectDB()
            myTrans = Connection.BeginTransaction(IsolationLevel.Serializable)
        Else
            Connection = Transaction.Connection
            myTrans = Transaction
        End If

        SQLServerCommand.Connection = Connection
        SQLServerCommand.Transaction = myTrans
        SQLServerCommand.CommandTimeout = 0
        Try

            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", Connection, myTrans)


            ' *** 0. insert tb_jobWithdraw ***
            ' *** 1.  update status of Withdraw ***
            ' *** 2.  update tb_LocationBalance  ***
            ' *** 3.  insert tb_transaction --- bal 
            ' *** 4.  update tb_withdraw  ***


            ' *** Get data of Product ***

            'strSQL = "   SELECT  WIL.WithdrawItemLocation_Index,WIL.Withdraw_Index,WIL.Sku_Index,WIL.Lot_No,WIL.Location_Index,WIL.Tag_No,WIL.PLot,WIL.ItemStatus_Index,WIL.Total_Qty,WIL.Weight,WIL.Volume,WIL.Serial_No ,"
            'strSQL &= "   W.Withdraw_Date, W.DocumentType_Index, W.Customer_Index,WIL.Qty,WIL.Package_Index,W.Withdraw_No,WI.ItemDefinition_Index  "
            'strSQL &= " ,WI.str1 as Reference1,WI.str2 as Reference2" 'New 6-8-2008
            'strSQL &= "   FROM  tb_WithdrawItemLocation WIL INNER JOIN  "
            'strSQL &= "   tb_Withdraw W ON WIL.Withdraw_Index=W.Withdraw_Index INNER JOIN "
            'strSQL &= "   tb_WithdrawItem WI ON WIL.WithdrawItem_Index=WI.WithdrawItem_Index  "
            'strSQL &= "   WHERE WIL.Withdraw_Index ='" & Withdraw_Index & "' "

            ' Dong_Edit
            strSQL = " SELECT       * "
            strSQL &= " FROM        VIEW_WithDrawSave "
            strSQL &= " WHERE       Withdraw_Index ='" & Withdraw_Index & "' "

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

                    Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                    Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                    Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                    Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                    Dim Tag_No As String = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                    Dim TAG_Index As String = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                    Dim Withdraw_No As String = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                    Dim Location_Index As String = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                    Dim LocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString

                    Dim Total_Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                    Dim Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Qty")
                    Dim Weight As Decimal = DS.Tables("tbl").Rows(i).Item("Weight")
                    Dim Volume As Decimal = DS.Tables("tbl").Rows(i).Item("Volume")
                    Dim ItemQty As Decimal = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                    Dim Price As Decimal = DS.Tables("tbl").Rows(i).Item("Price")

                    Dim ERP_Location As String = DS.Tables("tbl").Rows(i).Item("ERP_Location")

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(Connection, myTrans, PICKING.enmPicking_Action.DELBALANCE_RESERVE, 2, Withdraw_Index, "ยืนยันรายการเบิก", LocationBalance_Index, Qty, Total_Qty, Weight, Volume, ItemQty, Price, _
                    Total_Qty, Weight, Volume, ItemQty, Price)
                    objPicking = Nothing


                    strSQL = "UPDATE tb_WithdrawItemLocation "
                    strSQL &= " SET status =-1 "
                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .ExecuteNonQuery()
                    End With



                    ' ********************* Manage PalletType  ******************************


                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,Ratio,PalletType_Index,Pallet_Qty " _
                                                     & " FROM  tb_LocationBalance  " _
                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "' "  '& " where Tag_No ='" & Tag_No & "' "


                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans

                    '   DS = New DataSet
                    DataAdapter.Fill(DS, "tbl1")

                    If DS.Tables("tbl1").Rows.Count <> 0 Then
                        ' ***********************
                        Dim objCalBalance As New CalculateBalance
                        objCalBalance.setQty_Recieve_Package(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("LocationBalance_Index").ToString)
                        objCalBalance = Nothing
                        ' ***********************

                        If CDec(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
                            ' *** update ms_PalletType *** 

                            strSQL = "UPDATE ms_PalletType "
                            strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
                            strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex As New Sy_AutoNumber
                            Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value(Connection, myTrans, "PalletType_History_Index")
                            objDBPalletTypeIndex = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal As New CalculateBalance
                            Dim Qty_PalletType_Bal As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal = objPalletTypeBal.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '    .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                            Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value(Connection, myTrans, "PalletType_History_Index")
                            objDBPalletTypeIndex2 = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal2 As New CalculateBalance
                            Dim Qty_PalletType_Bal2 As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal2 = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal2 = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal2
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************
                        End If

                    End If

                    ' xxxxxxxxxxxxxxxxxxxx
                    DS.Tables("tbl1").Clear()
                    ' xxxxxxxxxxxxxxxxxxxx


                    ' *********************  End Manage PalletType  ******************************

                    ' *** Call Function Get Balance ***
                    Dim objBal As New CalculateBalance
                    ' *** Qty ***
                    Qty_Sku_Bal = 0 ' objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Qty_PLot_Bal = 0 ' objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Qty_ItemStatus_Bal = 0 ' objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Weight ***
                    Weight_Sku_Bal = 0 ' objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Weight_PLot_Bal = 0 ' objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Weight_ItemStatus_Bal = 0 'objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Volume ***
                    Volume_Sku_Bal = 0 ' objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Volume_PLot_Bal = 0 ' objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Volume_ItemStatus_Bal = 0 'objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

                    Qty_Sku_Location_Bal = 0 ' objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Location_Index)
                    Qty_PLot_Location_Bal = 0 'objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, Location_Index)
                    Qty_ItemStatus_Location_Bal = 0 'objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, Location_Index)


                    objBal = Nothing

                    ' *********************************
                    ' *** Insert tb_Transaction ***
                    Dim Product_Index As String = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString
                    Dim ProductType_Index As String = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                    Dim Order_Index As String = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                    Dim Order_Date As Date = CDate(DS.Tables("tbl").Rows(i).Item("Order_Date").ToString)
                    Dim OrderItem_Index As String = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                    Dim strSQL_Qty_Item_Bal As String = 0
                    Dim strSQL_OrderItem_Price_Bal As String = 0

                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_index,DocumentType_Index,ItemDefinition_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,Serial_No,HandlingType_Index"
                    strSQL &= " ,Tax1_Out,Tax2_Out,Tax3_Out,Tax4_Out,Tax5_Out "
                    strSQL &= " ,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index,ERP_Location_From,ERP_Location_TO )"
                    strSQL &= "  VALUES (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_index,@DocumentType_Index,@ItemDefinition_Index,@Reference1,@Reference2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_Out," & strSQL_Qty_Item_Bal & "-0,@OrderItem_Price_Out," & strSQL_OrderItem_Price_Bal & "-0,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@Serial_No,@HandlingType_Index"
                    strSQL &= " ,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out "
                    strSQL &= " ,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@ERP_Location_From,@ERP_Location_TO )"
                    '
                    strSQL_Qty_Item_Bal += " select sum(Qty_Item_In-Qty_Item_Out) as Qty from tb_Transaction"
                    strSQL_Qty_Item_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    strSQL_OrderItem_Price_Bal += " select sum(OrderItem_Price_In-OrderItem_Price_Out) as Price from tb_Transaction"
                    strSQL_OrderItem_Price_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("WithdrawItem_Index").ToString

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 2

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString

                        'New 6-8-2008
                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                        ' Dong_Edit
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
                        .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = Product_Index
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = ProductType_Index
                        .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = Order_Date.ToString("yyyy/MM/dd")

                        .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                        .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString

                        .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_In").ToString
                        .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_Out").ToString
                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("PO_No").ToString
                        .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("SO_No").ToString
                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Pallet_No").ToString
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Declaration_No").ToString
                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("HandlingType_Index").ToString

                        'add new 2011-03-03
                        .Parameters.Add("@Tax1_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax1")
                        .Parameters.Add("@Tax2_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax2")
                        .Parameters.Add("@Tax3_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax3")
                        .Parameters.Add("@Tax4_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax4")
                        .Parameters.Add("@Tax5_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax5")


                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal

                        .Parameters.Add("@ERP_Location_From", SqlDbType.NVarChar, 100).Value = ERP_Location
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.NVarChar, 100).Value = ERP_Location

                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                    strSQL = Nothing


                    '***************************** ASSET TRANSACTION ********************************

                    Dim _AssetLocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                    Dim _AssetTransaction_Index As String = ""
                    If _AssetLocationBalance_Index <> "" Then

                        strSQL = " SELECT       * "
                        strSQL &= " FROM       tb_AssetLocationBalance"
                        strSQL &= " WHERE       AssetLocationBalance_Index ='" & _AssetLocationBalance_Index & "' "

                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        connectDB()
                        EXEC_Command()

                        Dim DSAS As New DataSet
                        DataAdapter.Fill(DSAS, "tblas")

                        If DSAS.Tables("tblas").Rows.Count <> 0 Then

                            For iAs As Integer = 0 To DSAS.Tables("tblas").Rows.Count - 1
                                Dim objDBAsIndex As New Sy_AutoNumber
                                _AssetTransaction_Index = objDBAsIndex.getSys_Value(Connection, myTrans, "AssetTransaction_Index")
                                objDBAsIndex = Nothing
                                'Insert tb_AssetTransaction
                                strSQL = "  INSERT INTO tb_AssetTransaction("
                                strSQL &= " AssetTransaction_Index, AssetLocationBalance_Index, Asset_No, Serial_No, Sku_Index, Order_Index, OrderItem_Index, Description, add_by, add_date, add_branch"
                                strSQL &= " ,AssetTransaction_Id, Lot_No, From_Location_Index, To_Location_Index, Tag_No, Plot, ItemStatus_Index, Transation_Date, Process_id"
                                strSQL &= " ,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal, Qty_PLot_Bal, Qty_ItemStatus_Bal, Weight_Sku_Bal, Weight_PLot_Bal, Weight_ItemStatus_Bal, Volume_Sku_Bal, Volume_PLot_Bal, Volume_ItemStatus_Bal,Qty_Item_Out, OrderItem_Price_Out"
                                strSQL &= " ,ItemDefinition_Index, Customer_Index, DocumentType_Index, Referent_1, Referent_2, Order_Date, ProductType_Index, Product_Index)"
                                strSQL &= "       VALUES("
                                strSQL &= " @AssetTransaction_Index,@AssetLocationBalance_Index,@Asset_No,@Serial_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Description,@add_by,getdate(),@add_branch"
                                strSQL &= " ,@AssetTransaction_Id,@Lot_No,@From_Location_Index,@To_Location_Index,@Tag_No,@Plot,@ItemStatus_Index,@Transation_Date,@Process_id"
                                strSQL &= " ,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@Qty_Item_Out,@OrderItem_Price_Out"
                                strSQL &= " ,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent_1,@Referent_2,@Order_Date,@ProductType_Index,@Product_Index)"

                                With SQLServerCommand.Parameters
                                    .Clear()
                                    '-- from AssetLocation
                                    .Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = _AssetTransaction_Index
                                    .Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _AssetLocationBalance_Index
                                    .Add("@Asset_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Asset_No") ' _Asset_No
                                    .Add("@Serial_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Serial_No") '  _Serial_No
                                    .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("SKU_Index") '  _SKU_Index
                                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("Order_Index") '  _Order_Index
                                    .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("OrderItem_Index") '  _OrderItem_Index
                                    .Add("@Description", SqlDbType.VarChar, 200).Value = ""

                                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                    .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                    '--- From WithDrawItem


                                    .Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                                    .Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                                    .Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                                    .Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                                    .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                                    '.Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                    Dim _Order_date As DateTime = Format(DS.Tables("tbl").Rows(i).Item("Order_date"), "dd/MM/yyyy").ToString()

                                    .Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                                    ' Process_id 
                                    .Add("@Process_id", SqlDbType.Float, 8).Value = 2

                                    .Add("@Qty_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Qty")) ' _Qty_In
                                    .Add("@Weight_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Weight"))
                                    .Add("@Volume_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Volume"))

                                    .Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                                    .Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                                    .Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                                    .Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                                    .Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                                    .Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                                    .Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                                    .Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                                    .Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal
                                    .Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                                    .Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")

                                    .Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString
                                    .Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                                    .Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                                    .Add("@Referent_1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                                    .Add("@Referent_2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                                    .Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = _Order_date
                                    .Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                                    .Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString




                                End With
                                SetSQLString = strSQL
                                SetCommandType = enuCommandType.Text
                                SetEXEC_TYPE = EXEC.NonQuery
                                connectDB()
                                EXEC_Command()


                                strSQL = "  UPDATE tb_AssetLocationBalance    "
                                strSQL &= "  SET Qty_Bal=Qty_Bal-@Total_Qty "
                                strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                                strSQL &= " ,Status = -2 "
                                strSQL &= "  WHERE AssetLocationBalance_Index=@AssetLocationBalance_Index "
                                With SQLServerCommand
                                    .Parameters.Clear()
                                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                End With

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            Next
                        End If

                    End If

                    ' *********************************




                    ' *** Manage ms_location ***

                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' *** If Area of Location Emtry ***
                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Space_Used =0 "
                    strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                    ' *********************************


                    ' *** End Manage ms_location ***

                    ' *** Update tb_withdraw & tb_jobWithdraw ***
                    strSQL = "UPDATE tb_Withdraw "
                    strSQL &= " SET status =2  "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    strSQL = "UPDATE tb_JobWithdraw "
                    strSQL &= " SET status =2 "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' *** Update tb_SalesOrder & tb_SalesOrderItem ***

                    Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString

                    If Plan_Process <> -9 Then
                        Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                        Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString
                        StatusWithdraw_Document = Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                ' '' '' ''krit update 28/12/2009 for mm
                                '' '' ''strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                                '' '' ''strSQL &= " FROM tb_SalesOrderItem  "
                                '' '' ''strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index and Status <> -1"
                                ' '' '' ''  case นี้ กรณี สองใบเบิก 1 SO ไม่รอง รับ   และ ไม่รองรับ Ratio ด้วย         <------ เบส 29 /10 /2013               

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=10 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                If DS.Tables.Contains("SO") Then
                                    DS.Tables("SO").Clear()
                                End If
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If

                            Case Withdraw_Document.Packing
                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=7 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Qty) as Total_Qty "
                                strSQL &= " FROM tb_PackingItem WHERE Packing_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        '****************  Case ผลิต    ****************  
                                        strSQL = "UPDATE tb_Packing "
                                        strSQL &= " SET status =5 "
                                        strSQL &= " WHERE Packing_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If


                                '15-01-2010 ja update Status Reserve 
                            Case Withdraw_Document.Reserve
                                strSQL = " select Reserve_Index from tb_Reserve where Reserve_Index = @Reserve_Index  "
                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@Reserve_Index", SqlDbType.NVarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With
                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "Reserve")

                                If DS.Tables("Reserve").Rows.Count > 0 Then
                                    ' --- STEP 2: Update status in tb_Reserve = 3
                                    strSQL = "UPDATE tb_Reserve "
                                    strSQL &= " SET status =3 "
                                    strSQL &= " WHERE Reserve_Index ='" & DS.Tables("Reserve").Rows(0).Item("Reserve_Index").ToString & "'"

                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                End If


                            Case Withdraw_Document.Transport 'killz update 08-08-2011

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=25 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If
                        End Select


                        'Update Status All Document  WithDraw
                        'UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, StatusWithdraw_Document, myTrans)

                    End If

                    Dim oAudit_Log As New Sy_Audit_Log
                    oAudit_Log.Document_Index = Withdraw_Index  'Me._newSaleOrder_Index
                    oAudit_Log.Document_No = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                    oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Confirm_Picking, Connection, myTrans)

                Next

                DBExeNonQuery(String.Format("UPDATE tb_WithdrawItemSerial SET Status = 2 where Withdraw_Index = '{0}'", Withdraw_Index), Connection, myTrans, eCommandType.Text)

            Else
                Return "รายการนี้ยังไม่สามารถยืนยันรายการได้"
            End If
            '*** Commit transaction

            If IsMyTransaction Then
                myTrans.Commit()
            End If

            Return "PASS"
        Catch ex As Exception
            If IsMyTransaction Then
                myTrans.Rollback()
            End If

            Throw ex
        End Try
    End Function

    Public Function SaveWithdrawConfirm(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand, ByVal Withdraw_Index As String) As String

        Dim strSQL As String = ""

        Dim Qty_Sku_Bal As Decimal = 0
        Dim Weight_Sku_Bal As Decimal = 0
        Dim Volume_Sku_Bal As Decimal = 0
        Dim Qty_PLot_Bal As Decimal = 0
        Dim Weight_PLot_Bal As Decimal = 0
        Dim Volume_PLot_Bal As Decimal = 0
        Dim Qty_ItemStatus_Bal As Decimal = 0
        Dim Weight_ItemStatus_Bal As Decimal = 0
        Dim Volume_ItemStatus_Bal As Decimal = 0

        Dim Qty_Sku_Location_Bal As Decimal = 0
        Dim Qty_ItemStatus_Location_Bal As Decimal = 0
        Dim Qty_PLot_Location_Bal As Decimal = 0

        SQLServerCommand.CommandTimeout = 0

        Try

            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", Connection, myTrans)


            ' *** 0. insert tb_jobWithdraw ***
            ' *** 1.  update status of Withdraw ***
            ' *** 2.  update tb_LocationBalance  ***
            ' *** 3.  insert tb_transaction --- bal 
            ' *** 4.  update tb_withdraw  ***


            ' *** Get data of Product ***

            ' Dong_Edit
            strSQL = " SELECT       * "
            strSQL &= " FROM        VIEW_WithDrawSave "
            strSQL &= " WHERE       Withdraw_Index ='" & Withdraw_Index & "' "

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

                    Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                    Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                    Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                    Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                    Dim Tag_No As String = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                    Dim TAG_Index As String = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                    Dim Withdraw_No As String = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                    Dim Location_Index As String = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                    Dim LocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString

                    Dim Total_Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                    Dim Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Qty")
                    Dim Weight As Decimal = DS.Tables("tbl").Rows(i).Item("Weight")
                    Dim Volume As Decimal = DS.Tables("tbl").Rows(i).Item("Volume")
                    Dim ItemQty As Decimal = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                    Dim Price As Decimal = DS.Tables("tbl").Rows(i).Item("Price")

                    Dim ERP_Location As String = DS.Tables("tbl").Rows(i).Item("ERP_Location")

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(Connection, myTrans, PICKING.enmPicking_Action.DELBALANCE_RESERVE, 2, Withdraw_Index, "ยืนยันรายการเบิก", LocationBalance_Index, Qty, Total_Qty, Weight, Volume, ItemQty, Price, _
                    Total_Qty, Weight, Volume, ItemQty, Price)
                    objPicking = Nothing


                    strSQL = "UPDATE tb_WithdrawItemLocation "
                    strSQL &= " SET status =-1 "
                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .ExecuteNonQuery()
                    End With



                    ' ********************* Manage PalletType  ******************************


                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,Ratio,PalletType_Index,Pallet_Qty " _
                                                     & " FROM  tb_LocationBalance  " _
                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "' "  '& " where Tag_No ='" & Tag_No & "' "


                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans

                    '   DS = New DataSet
                    DataAdapter.Fill(DS, "tbl1")

                    If DS.Tables("tbl1").Rows.Count <> 0 Then
                        ' ***********************
                        Dim objCalBalance As New CalculateBalance
                        objCalBalance.setQty_Recieve_Package(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("LocationBalance_Index").ToString)
                        objCalBalance = Nothing
                        ' ***********************

                        If CDec(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
                            ' *** update ms_PalletType *** 

                            strSQL = "UPDATE ms_PalletType "
                            strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
                            strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex As New Sy_AutoNumber
                            Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value("PalletType_History_Index")
                            objDBPalletTypeIndex = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal As New CalculateBalance
                            Dim Qty_PalletType_Bal As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal = objPalletTypeBal.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '    .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                            Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value("PalletType_History_Index")
                            objDBPalletTypeIndex2 = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal2 As New CalculateBalance
                            Dim Qty_PalletType_Bal2 As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal2 = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal2 = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal2
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************
                        End If

                    End If

                    ' xxxxxxxxxxxxxxxxxxxx
                    DS.Tables("tbl1").Clear()
                    ' xxxxxxxxxxxxxxxxxxxx


                    ' *********************  End Manage PalletType  ******************************

                    ' *** Call Function Get Balance ***
                    Dim objBal As New CalculateBalance
                    ' *** Qty ***
                    Qty_Sku_Bal = 0 ' objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Qty_PLot_Bal = 0 ' objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Qty_ItemStatus_Bal = 0 ' objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Weight ***
                    Weight_Sku_Bal = 0 ' objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Weight_PLot_Bal = 0 ' objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Weight_ItemStatus_Bal = 0 'objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Volume ***
                    Volume_Sku_Bal = 0 ' objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Volume_PLot_Bal = 0 ' objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Volume_ItemStatus_Bal = 0 'objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

                    Qty_Sku_Location_Bal = 0 ' objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Location_Index)
                    Qty_PLot_Location_Bal = 0 'objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, Location_Index)
                    Qty_ItemStatus_Location_Bal = 0 'objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, Location_Index)


                    objBal = Nothing

                    ' *********************************
                    ' *** Insert tb_Transaction ***
                    Dim Product_Index As String = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString
                    Dim ProductType_Index As String = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                    Dim Order_Index As String = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                    Dim Order_Date As Date = CDate(DS.Tables("tbl").Rows(i).Item("Order_Date").ToString)
                    Dim OrderItem_Index As String = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                    Dim strSQL_Qty_Item_Bal As String = 0
                    Dim strSQL_OrderItem_Price_Bal As String = 0

                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_index,DocumentType_Index,ItemDefinition_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,Serial_No,HandlingType_Index"
                    strSQL &= " ,Tax1_Out,Tax2_Out,Tax3_Out,Tax4_Out,Tax5_Out "
                    strSQL &= " ,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index,ERP_Location_From,ERP_Location_TO )"
                    strSQL &= "  VALUES (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_index,@DocumentType_Index,@ItemDefinition_Index,@Reference1,@Reference2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_Out," & strSQL_Qty_Item_Bal & "-0,@OrderItem_Price_Out," & strSQL_OrderItem_Price_Bal & "-0,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@Serial_No,@HandlingType_Index"
                    strSQL &= " ,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out "
                    strSQL &= " ,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@ERP_Location_From,@ERP_Location_TO )"
                    '
                    strSQL_Qty_Item_Bal += " select sum(Qty_Item_In-Qty_Item_Out) as Qty from tb_Transaction"
                    strSQL_Qty_Item_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    strSQL_OrderItem_Price_Bal += " select sum(OrderItem_Price_In-OrderItem_Price_Out) as Price from tb_Transaction"
                    strSQL_OrderItem_Price_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("WithdrawItem_Index").ToString

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 2

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString

                        'New 6-8-2008
                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                        ' Dong_Edit
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
                        .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = Product_Index
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = ProductType_Index
                        .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = Order_Date.ToString("yyyy/MM/dd")

                        .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                        .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString

                        .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_In").ToString
                        .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_Out").ToString
                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("PO_No").ToString
                        .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("SO_No").ToString
                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Pallet_No").ToString
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Declaration_No").ToString
                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("HandlingType_Index").ToString

                        'add new 2011-03-03
                        .Parameters.Add("@Tax1_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax1")
                        .Parameters.Add("@Tax2_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax2")
                        .Parameters.Add("@Tax3_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax3")
                        .Parameters.Add("@Tax4_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax4")
                        .Parameters.Add("@Tax5_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax5")


                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal

                        .Parameters.Add("@ERP_Location_From", SqlDbType.NVarChar, 100).Value = ERP_Location
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.NVarChar, 100).Value = ERP_Location

                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                    strSQL = Nothing


                    '***************************** ASSET TRANSACTION ********************************

                    Dim _AssetLocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                    Dim _AssetTransaction_Index As String = ""
                    If _AssetLocationBalance_Index <> "" Then

                        strSQL = " SELECT       * "
                        strSQL &= " FROM       tb_AssetLocationBalance"
                        strSQL &= " WHERE       AssetLocationBalance_Index ='" & _AssetLocationBalance_Index & "' "

                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        connectDB()
                        EXEC_Command()

                        Dim DSAS As New DataSet
                        DataAdapter.Fill(DSAS, "tblas")

                        If DSAS.Tables("tblas").Rows.Count <> 0 Then

                            For iAs As Integer = 0 To DSAS.Tables("tblas").Rows.Count - 1
                                Dim objDBAsIndex As New Sy_AutoNumber
                                _AssetTransaction_Index = objDBAsIndex.getSys_Value("AssetTransaction_Index")
                                objDBAsIndex = Nothing
                                'Insert tb_AssetTransaction
                                strSQL = "  INSERT INTO tb_AssetTransaction("
                                strSQL &= " AssetTransaction_Index, AssetLocationBalance_Index, Asset_No, Serial_No, Sku_Index, Order_Index, OrderItem_Index, Description, add_by, add_date, add_branch"
                                strSQL &= " ,AssetTransaction_Id, Lot_No, From_Location_Index, To_Location_Index, Tag_No, Plot, ItemStatus_Index, Transation_Date, Process_id"
                                strSQL &= " ,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal, Qty_PLot_Bal, Qty_ItemStatus_Bal, Weight_Sku_Bal, Weight_PLot_Bal, Weight_ItemStatus_Bal, Volume_Sku_Bal, Volume_PLot_Bal, Volume_ItemStatus_Bal,Qty_Item_Out, OrderItem_Price_Out"
                                strSQL &= " ,ItemDefinition_Index, Customer_Index, DocumentType_Index, Referent_1, Referent_2, Order_Date, ProductType_Index, Product_Index)"
                                strSQL &= "       VALUES("
                                strSQL &= " @AssetTransaction_Index,@AssetLocationBalance_Index,@Asset_No,@Serial_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Description,@add_by,getdate(),@add_branch"
                                strSQL &= " ,@AssetTransaction_Id,@Lot_No,@From_Location_Index,@To_Location_Index,@Tag_No,@Plot,@ItemStatus_Index,@Transation_Date,@Process_id"
                                strSQL &= " ,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@Qty_Item_Out,@OrderItem_Price_Out"
                                strSQL &= " ,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent_1,@Referent_2,@Order_Date,@ProductType_Index,@Product_Index)"

                                With SQLServerCommand.Parameters
                                    .Clear()
                                    '-- from AssetLocation
                                    .Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = _AssetTransaction_Index
                                    .Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _AssetLocationBalance_Index
                                    .Add("@Asset_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Asset_No") ' _Asset_No
                                    .Add("@Serial_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Serial_No") '  _Serial_No
                                    .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("SKU_Index") '  _SKU_Index
                                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("Order_Index") '  _Order_Index
                                    .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("OrderItem_Index") '  _OrderItem_Index
                                    .Add("@Description", SqlDbType.VarChar, 200).Value = ""

                                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                    .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                    '--- From WithDrawItem


                                    .Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                                    .Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                                    .Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                                    .Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                                    .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                                    '.Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                    Dim _Order_date As DateTime = Format(DS.Tables("tbl").Rows(i).Item("Order_date"), "dd/MM/yyyy").ToString()

                                    .Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                                    ' Process_id 
                                    .Add("@Process_id", SqlDbType.Float, 8).Value = 2

                                    .Add("@Qty_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Qty")) ' _Qty_In
                                    .Add("@Weight_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Weight"))
                                    .Add("@Volume_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Volume"))

                                    .Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                                    .Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                                    .Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                                    .Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                                    .Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                                    .Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                                    .Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                                    .Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                                    .Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal
                                    .Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                                    .Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")

                                    .Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString
                                    .Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                                    .Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                                    .Add("@Referent_1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                                    .Add("@Referent_2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                                    .Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = _Order_date
                                    .Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                                    .Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString




                                End With
                                SetSQLString = strSQL
                                SetCommandType = enuCommandType.Text
                                SetEXEC_TYPE = EXEC.NonQuery
                                connectDB()
                                EXEC_Command()


                                strSQL = "  UPDATE tb_AssetLocationBalance    "
                                strSQL &= "  SET Qty_Bal=Qty_Bal-@Total_Qty "
                                strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                                strSQL &= " ,Status = -2 "
                                strSQL &= "  WHERE AssetLocationBalance_Index=@AssetLocationBalance_Index "
                                With SQLServerCommand
                                    .Parameters.Clear()
                                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                End With

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            Next
                        End If

                    End If

                    ' *********************************




                    ' *** Manage ms_location ***

                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' *** If Area of Location Emtry ***
                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Space_Used =0 "
                    strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                    ' *********************************


                    ' *** End Manage ms_location ***

                    ' *** Update tb_withdraw & tb_jobWithdraw ***
                    strSQL = "UPDATE tb_Withdraw "
                    strSQL &= " SET status =2  "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    strSQL = "UPDATE tb_JobWithdraw "
                    strSQL &= " SET status =2 "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' *** Update tb_SalesOrder & tb_SalesOrderItem ***

                    Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString

                    If Plan_Process <> -9 Then
                        Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                        Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString
                        StatusWithdraw_Document = Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                ' '' '' ''krit update 28/12/2009 for mm
                                '' '' ''strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                                '' '' ''strSQL &= " FROM tb_SalesOrderItem  "
                                '' '' ''strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index and Status <> -1"
                                ' '' '' ''  case นี้ กรณี สองใบเบิก 1 SO ไม่รอง รับ   และ ไม่รองรับ Ratio ด้วย         <------ เบส 29 /10 /2013               

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=10 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                If DS.Tables.Contains("SO") Then
                                    DS.Tables("SO").Clear()
                                End If
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If

                            Case Withdraw_Document.Packing
                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=7 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Qty) as Total_Qty "
                                strSQL &= " FROM tb_PackingItem WHERE Packing_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        '****************  Case ผลิต    ****************  
                                        strSQL = "UPDATE tb_Packing "
                                        strSQL &= " SET status =5 "
                                        strSQL &= " WHERE Packing_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If


                                '15-01-2010 ja update Status Reserve 
                            Case Withdraw_Document.Reserve
                                strSQL = " select Reserve_Index from tb_Reserve where Reserve_Index = @Reserve_Index  "
                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@Reserve_Index", SqlDbType.NVarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With
                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "Reserve")

                                If DS.Tables("Reserve").Rows.Count > 0 Then
                                    ' --- STEP 2: Update status in tb_Reserve = 3
                                    strSQL = "UPDATE tb_Reserve "
                                    strSQL &= " SET status =3 "
                                    strSQL &= " WHERE Reserve_Index ='" & DS.Tables("Reserve").Rows(0).Item("Reserve_Index").ToString & "'"

                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                End If


                            Case Withdraw_Document.Transport 'killz update 08-08-2011

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=25 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If
                        End Select


                        'Update Status All Document  WithDraw
                        'UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, StatusWithdraw_Document, myTrans)

                    End If

                    Dim oAudit_Log As New Sy_Audit_Log
                    oAudit_Log.Document_Index = Withdraw_Index  'Me._newSaleOrder_Index
                    oAudit_Log.Document_No = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                    oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Confirm_Picking)


                Next

                DBExeNonQuery(String.Format("UPDATE tb_WithdrawItemSerial SET Status = 2 where Withdraw_Index = '{0}'", Withdraw_Index), Connection, myTrans, eCommandType.Text)

            Else
                Return "รายการนี้ยังไม่สามารถยืนยันรายการได้"
            End If
            '*** Commit transaction

            myTrans.Commit()
            Return "PASS"
        Catch ex As Exception

            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function



    Public Function SaveWithdrawConfirm_Flag(ByVal Withdraw_Index As String) As String

        Dim strSQL As String = ""

        Dim Qty_Sku_Bal As Decimal = 0
        Dim Weight_Sku_Bal As Decimal = 0
        Dim Volume_Sku_Bal As Decimal = 0
        Dim Qty_PLot_Bal As Decimal = 0
        Dim Weight_PLot_Bal As Decimal = 0
        Dim Volume_PLot_Bal As Decimal = 0
        Dim Qty_ItemStatus_Bal As Decimal = 0
        Dim Weight_ItemStatus_Bal As Decimal = 0
        Dim Volume_ItemStatus_Bal As Decimal = 0

        Dim Qty_Sku_Location_Bal As Decimal = 0
        Dim Qty_ItemStatus_Location_Bal As Decimal = 0
        Dim Qty_PLot_Location_Bal As Decimal = 0

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            ' *** 0. insert tb_jobWithdraw ***
            ' *** 1.  update status of Withdraw ***
            ' *** 2.  update tb_LocationBalance  ***
            ' *** 3.  insert tb_transaction --- bal 
            ' *** 4.  update tb_withdraw  ***


            ' *** Get data of Product ***

            'strSQL = "   SELECT  WIL.WithdrawItemLocation_Index,WIL.Withdraw_Index,WIL.Sku_Index,WIL.Lot_No,WIL.Location_Index,WIL.Tag_No,WIL.PLot,WIL.ItemStatus_Index,WIL.Total_Qty,WIL.Weight,WIL.Volume,WIL.Serial_No ,"
            'strSQL &= "   W.Withdraw_Date, W.DocumentType_Index, W.Customer_Index,WIL.Qty,WIL.Package_Index,W.Withdraw_No,WI.ItemDefinition_Index  "
            'strSQL &= " ,WI.str1 as Reference1,WI.str2 as Reference2" 'New 6-8-2008
            'strSQL &= "   FROM  tb_WithdrawItemLocation WIL INNER JOIN  "
            'strSQL &= "   tb_Withdraw W ON WIL.Withdraw_Index=W.Withdraw_Index INNER JOIN "
            'strSQL &= "   tb_WithdrawItem WI ON WIL.WithdrawItem_Index=WI.WithdrawItem_Index  "
            'strSQL &= "   WHERE WIL.Withdraw_Index ='" & Withdraw_Index & "' "

            ' Dong_Edit
            strSQL = " SELECT       *"
            strSQL &= " FROM        VIEW_WithDrawSave"
            strSQL &= " WHERE       Withdraw_Index ='" & Withdraw_Index & "' "

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

                    Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                    Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                    Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                    Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                    Dim Tag_No As String = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                    Dim TAG_Index As String = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                    Dim Withdraw_No As String = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                    Dim Location_Index As String = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                    Dim LocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString

                    Dim Total_Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                    Dim Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Qty")
                    Dim Weight As Decimal = DS.Tables("tbl").Rows(i).Item("Weight")
                    Dim Volume As Decimal = DS.Tables("tbl").Rows(i).Item("Volume")
                    Dim ItemQty As Decimal = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                    Dim Price As Decimal = DS.Tables("tbl").Rows(i).Item("Price")

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(Connection, myTrans, PICKING.enmPicking_Action.DELBALANCE_RESERVE, 2, Withdraw_Index, "ยืนยันรายการ", LocationBalance_Index, Qty, Total_Qty, Weight, Volume, ItemQty, Price, _
                    Total_Qty, Weight, Volume, ItemQty, Price)
                    objPicking = Nothing



                    '' ***  update quantity ***
                    'strSQL = "  UPDATE tb_LocationBalance    "
                    'strSQL &= " SET "
                    'strSQL &= " Qty_Recieve_Package=Qty_Recieve_Package-@Qty"
                    'strSQL &= " ,Qty_Bal=Qty_Bal-@Total_Qty"
                    'strSQL &= " ,Weight_Bal=Weight_Bal-@Weight"
                    'strSQL &= " ,Volume_Bal=Volume_Bal-@Volume "
                    'strSQL &= " ,Qty_Item_Bal = Qty_Item_Bal-@ItemQty"
                    'strSQL &= " ,OrderItem_Price_Bal=OrderItem_Price_Bal-@Price"

                    'strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                    'strSQL &= " ,ReserveWeight = ReserveWeight-@Weight"
                    'strSQL &= " ,ReserveVolume = ReserveVolume-@Volume"
                    'strSQL &= " ,ReserveQty_Item = ReserveQty_Item-@ItemQty"
                    'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price-@Price"
                    'strSQL &= " WHERE  LocationBalance_Index=@LocationBalance_Index "
                    'With SQLServerCommand

                    '    .Parameters.Clear()
                    '    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                    '    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                    '    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                    '    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                    '    ' *** If Check Qty_Recieve_Package *** 
                    '    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty")
                    '    .Parameters.Add("@ItemQty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                    '    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")
                    '    .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString
                    'End With


                    'SetSQLString = strSQL
                    'SetCommandType = DBType_SQLServer.enuCommandType.Text
                    'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    'EXEC_Command()




                    strSQL = "UPDATE tb_WithdrawItemLocation "
                    strSQL &= " SET status =-1 "
                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .ExecuteNonQuery()
                    End With



                    ' ********************* Manage PalletType  ******************************


                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,Ratio,PalletType_Index,Pallet_Qty " _
                                                     & " FROM  tb_LocationBalance  " _
                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "' "  '& " where Tag_No ='" & Tag_No & "' "


                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans

                    '   DS = New DataSet
                    DataAdapter.Fill(DS, "tbl1")

                    If DS.Tables("tbl1").Rows.Count <> 0 Then
                        ' ***********************
                        Dim objCalBalance As New CalculateBalance
                        objCalBalance.setQty_Recieve_Package(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("LocationBalance_Index").ToString)
                        objCalBalance = Nothing
                        ' ***********************

                        If CDec(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
                            ' *** update ms_PalletType *** 

                            strSQL = "UPDATE ms_PalletType "
                            strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
                            strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex As New Sy_AutoNumber
                            Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value("PalletType_History_Index")
                            objDBPalletTypeIndex = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal As New CalculateBalance
                            Dim Qty_PalletType_Bal As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal = objPalletTypeBal.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '    .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************



                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                            Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value("PalletType_History_Index")
                            objDBPalletTypeIndex2 = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal2 As New CalculateBalance
                            Dim Qty_PalletType_Bal2 As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal2 = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal2 = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal2
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************
                        End If

                    End If

                    ' xxxxxxxxxxxxxxxxxxxx
                    DS.Tables("tbl1").Clear()
                    ' xxxxxxxxxxxxxxxxxxxx


                    ' *********************  End Manage PalletType  ******************************

                    ' *** Call Function Get Balance ***
                    Dim objBal As New CalculateBalance
                    ' *** Qty ***
                    Qty_Sku_Bal = 0 ' objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Qty_PLot_Bal = 0 ' objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Qty_ItemStatus_Bal = 0 ' objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Weight ***
                    Weight_Sku_Bal = 0 ' objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Weight_PLot_Bal = 0 ' objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Weight_ItemStatus_Bal = 0 'objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Volume ***
                    Volume_Sku_Bal = 0 ' objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Volume_PLot_Bal = 0 ' objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Volume_ItemStatus_Bal = 0 'objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

                    Qty_Sku_Location_Bal = 0 ' objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Location_Index)
                    Qty_PLot_Location_Bal = 0 'objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, Location_Index)
                    Qty_ItemStatus_Location_Bal = 0 'objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, Location_Index)


                    objBal = Nothing

                    ' *********************************
                    ' *** Insert tb_Transaction ***
                    Dim Product_Index As String = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString
                    Dim ProductType_Index As String = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                    Dim Order_Index As String = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                    Dim Order_Date As Date = CDate(DS.Tables("tbl").Rows(i).Item("Order_Date").ToString)
                    Dim OrderItem_Index As String = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                    Dim strSQL_Qty_Item_Bal As String = 0
                    Dim strSQL_OrderItem_Price_Bal As String = 0

                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Customer_index,DocumentType_Index,ItemDefinition_Index,Referent_1,Referent_2,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_Out,OrderItem_Price_Bal,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,Serial_No,HandlingType_Index"
                    strSQL &= " ,Tax1_Out,Tax2_Out,Tax3_Out,Tax4_Out,Tax5_Out "
                    strSQL &= " ,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index )"
                    strSQL &= "  VALUES (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Customer_index,@DocumentType_Index,@ItemDefinition_Index,@Reference1,@Reference2,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,@Qty_Item_Out," & strSQL_Qty_Item_Bal & "-0,@OrderItem_Price_Out," & strSQL_OrderItem_Price_Bal & "-0,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@Serial_No,@HandlingType_Index"
                    strSQL &= " ,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out "
                    strSQL &= " ,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index )"
                    '
                    strSQL_Qty_Item_Bal += " select sum(Qty_Item_In-Qty_Item_Out) as Qty from tb_Transaction"
                    strSQL_Qty_Item_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    strSQL_OrderItem_Price_Bal += " select sum(OrderItem_Price_In-OrderItem_Price_Out) as Price from tb_Transaction"
                    strSQL_OrderItem_Price_Bal += " where OrderItem_Index ='" & OrderItem_Index & "'"

                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("WithdrawItem_Index").ToString

                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 2

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString

                        'New 6-8-2008
                        .Parameters.Add("@Reference1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                        .Parameters.Add("@Reference2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                        ' Dong_Edit
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = Order_Index
                        .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = Product_Index
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = ProductType_Index
                        .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = Order_Date.ToString("yyyy/MM/dd")

                        .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                        .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString

                        .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_In").ToString
                        .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_Out").ToString
                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("PO_No").ToString
                        .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("SO_No").ToString
                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Pallet_No").ToString
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Declaration_No").ToString
                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("HandlingType_Index").ToString

                        'add new 2011-03-03
                        .Parameters.Add("@Tax1_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax1")
                        .Parameters.Add("@Tax2_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax2")
                        .Parameters.Add("@Tax3_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax3")
                        .Parameters.Add("@Tax4_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax4")
                        .Parameters.Add("@Tax5_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Tax5")


                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal


                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()
                    strSQL = Nothing


                    '***************************** ASSET TRANSACTION ********************************

                    Dim _AssetLocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                    Dim _AssetTransaction_Index As String = ""
                    If _AssetLocationBalance_Index <> "" Then

                        strSQL = " SELECT       * "
                        strSQL &= " FROM       tb_AssetLocationBalance"
                        strSQL &= " WHERE       AssetLocationBalance_Index ='" & _AssetLocationBalance_Index & "' "

                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        connectDB()
                        EXEC_Command()

                        Dim DSAS As New DataSet
                        DataAdapter.Fill(DSAS, "tblas")

                        If DSAS.Tables("tblas").Rows.Count <> 0 Then

                            For iAs As Integer = 0 To DSAS.Tables("tblas").Rows.Count - 1
                                Dim objDBAsIndex As New Sy_AutoNumber
                                _AssetTransaction_Index = objDBAsIndex.getSys_Value("AssetTransaction_Index")
                                objDBAsIndex = Nothing
                                'Insert tb_AssetTransaction
                                strSQL = "  INSERT INTO tb_AssetTransaction("
                                strSQL &= " AssetTransaction_Index, AssetLocationBalance_Index, Asset_No, Serial_No, Sku_Index, Order_Index, OrderItem_Index, Description, add_by, add_date, add_branch"
                                strSQL &= " ,AssetTransaction_Id, Lot_No, From_Location_Index, To_Location_Index, Tag_No, Plot, ItemStatus_Index, Transation_Date, Process_id"
                                strSQL &= " ,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal, Qty_PLot_Bal, Qty_ItemStatus_Bal, Weight_Sku_Bal, Weight_PLot_Bal, Weight_ItemStatus_Bal, Volume_Sku_Bal, Volume_PLot_Bal, Volume_ItemStatus_Bal,Qty_Item_Out, OrderItem_Price_Out"
                                strSQL &= " ,ItemDefinition_Index, Customer_Index, DocumentType_Index, Referent_1, Referent_2, Order_Date, ProductType_Index, Product_Index)"
                                strSQL &= "       VALUES("
                                strSQL &= " @AssetTransaction_Index,@AssetLocationBalance_Index,@Asset_No,@Serial_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Description,@add_by,getdate(),@add_branch"
                                strSQL &= " ,@AssetTransaction_Id,@Lot_No,@From_Location_Index,@To_Location_Index,@Tag_No,@Plot,@ItemStatus_Index,@Transation_Date,@Process_id"
                                strSQL &= " ,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@Qty_Item_Out,@OrderItem_Price_Out"
                                strSQL &= " ,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent_1,@Referent_2,@Order_Date,@ProductType_Index,@Product_Index)"

                                With SQLServerCommand.Parameters
                                    .Clear()
                                    '-- from AssetLocation
                                    .Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = _AssetTransaction_Index
                                    .Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _AssetLocationBalance_Index
                                    .Add("@Asset_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Asset_No") ' _Asset_No
                                    .Add("@Serial_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Serial_No") '  _Serial_No
                                    .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("SKU_Index") '  _SKU_Index
                                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("Order_Index") '  _Order_Index
                                    .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("OrderItem_Index") '  _OrderItem_Index
                                    .Add("@Description", SqlDbType.VarChar, 200).Value = ""

                                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                    .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                    '--- From WithDrawItem


                                    .Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                                    .Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                                    .Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                                    .Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                                    .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                                    '.Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                    Dim _Order_date As DateTime = Format(DS.Tables("tbl").Rows(i).Item("Order_date"), "dd/MM/yyyy").ToString()

                                    .Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("Withdraw_Date")).ToString("yyyy/MM/dd")
                                    ' Process_id 
                                    .Add("@Process_id", SqlDbType.Float, 8).Value = 2

                                    .Add("@Qty_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Qty")) ' _Qty_In
                                    .Add("@Weight_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Weight"))
                                    .Add("@Volume_Out", SqlDbType.Float, 8).Value = Val(DS.Tables("tbl").Rows(i).Item("Volume"))

                                    .Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                                    .Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                                    .Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                                    .Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                                    .Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                                    .Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                                    .Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                                    .Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                                    .Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal
                                    .Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                                    .Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Price")

                                    .Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString
                                    .Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                                    .Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                                    .Add("@Referent_1", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                                    .Add("@Referent_2", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                                    .Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = _Order_date
                                    .Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                                    .Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString




                                End With
                                SetSQLString = strSQL
                                SetCommandType = enuCommandType.Text
                                SetEXEC_TYPE = EXEC.NonQuery
                                connectDB()
                                EXEC_Command()


                                strSQL = "  UPDATE tb_AssetLocationBalance    "
                                strSQL &= "  SET Qty_Bal=Qty_Bal-@Total_Qty "
                                strSQL &= " ,ReserveQty = ReserveQty-@Total_Qty"
                                strSQL &= " ,Status = -2 "
                                strSQL &= "  WHERE AssetLocationBalance_Index=@AssetLocationBalance_Index "
                                With SQLServerCommand
                                    .Parameters.Clear()
                                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                End With

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            Next
                        End If

                    End If

                    ' *********************************




                    ' *** Manage ms_location ***

                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' *** If Area of Location Emtry ***
                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Space_Used =0 "
                    strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                    ' *********************************


                    ' *** End Manage ms_location ***

                    ' *** Update tb_withdraw & tb_jobWithdraw ***
                    strSQL = "UPDATE tb_Withdraw "
                    strSQL &= " SET status =2 , Confirm_Flag = 2 "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    strSQL = "UPDATE tb_JobWithdraw "
                    strSQL &= " SET status =2 "
                    strSQL &= " WHERE Withdraw_Index ='" & DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString & "' "

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' *** Update tb_SalesOrder & tb_SalesOrderItem ***

                    Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString

                    If Plan_Process <> -9 Then
                        Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                        Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString
                        StatusWithdraw_Document = Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                ' '' '' ''krit update 28/12/2009 for mm
                                '' '' ''strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                                '' '' ''strSQL &= " FROM tb_SalesOrderItem  "
                                '' '' ''strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index and Status <> -1"
                                ' '' '' ''  case นี้ กรณี สองใบเบิก 1 SO ไม่รอง รับ   และ ไม่รองรับ Ratio ด้วย         <------ เบส 29 /10 /2013               

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=10 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                If DS.Tables.Contains("SO") Then
                                    DS.Tables("SO").Clear()
                                End If
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If

                            Case Withdraw_Document.Packing
                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=7 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Qty) as Total_Qty "
                                strSQL &= " FROM tb_PackingItem WHERE Packing_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        '****************  Case ผลิต    ****************  
                                        strSQL = "UPDATE tb_Packing "
                                        strSQL &= " SET status =5 "
                                        strSQL &= " WHERE Packing_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If


                                '15-01-2010 ja update Status Reserve 
                            Case Withdraw_Document.Reserve
                                strSQL = " select Reserve_Index from tb_Reserve where Reserve_Index = @Reserve_Index  "
                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@Reserve_Index", SqlDbType.NVarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With
                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "Reserve")

                                If DS.Tables("Reserve").Rows.Count > 0 Then
                                    ' --- STEP 2: Update status in tb_Reserve = 3
                                    strSQL = "UPDATE tb_Reserve "
                                    strSQL &= " SET status =3 "
                                    strSQL &= " WHERE Reserve_Index ='" & DS.Tables("Reserve").Rows(0).Item("Reserve_Index").ToString & "'"

                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                End If


                            Case Withdraw_Document.Transport 'killz update 08-08-2011

                                strSQL = " SELECT (select sum(total_QTY) from tb_withdrawitem WI inner join tb_withdraw W "
                                strSQL &= " on wi.withdraw_index=w.withdraw_index "
                                strSQL &= " where Plan_Process=25 and DocumentPlan_Index=@PlanDocument_Index and w.status=2 ) as Qty_Withdraw "
                                strSQL &= " ,SUM(Total_Qty) as Total_Qty "
                                strSQL &= " FROM tb_SalesOrderItem WHERE SalesOrder_Index =@PlanDocument_Index "



                                SQLServerCommand.Parameters.Clear()
                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = DocumentPlan_Index

                                With SQLServerCommand
                                    .Connection = Connection
                                    .Transaction = myTrans
                                    .CommandText = strSQL
                                    .CommandTimeout = 0
                                End With

                                DataAdapter.SelectCommand = SQLServerCommand
                                DataAdapter.SelectCommand.Transaction = myTrans
                                '   DS = New DataSet
                                DataAdapter.Fill(DS, "SO")

                                If DS.Tables("SO").Rows.Count > 0 Then

                                    If CDec(DS.Tables("SO").Rows(0)("Qty_Withdraw").ToString) >= CDec(DS.Tables("SO").Rows(0)("Total_Qty").ToString) Then
                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
                                        strSQL = "UPDATE tb_SalesOrder "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                        ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                                        strSQL = "UPDATE tb_SalesOrderItem "
                                        strSQL &= " SET status =3 "
                                        strSQL &= " WHERE SalesOrder_Index ='" & DocumentPlan_Index & "'"

                                        SetSQLString = strSQL
                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                        EXEC_Command()
                                    End If

                                End If
                        End Select


                        'Update Status All Document  WithDraw
                        'UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, StatusWithdraw_Document, myTrans)

                    End If



                Next

            End If










            '*** Commit transaction
            myTrans.Commit()

            Return "PASS"
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

        Return "PASS"

    End Function




    Public Function getSalesOrder(ByVal SalesOrder_No As String) As String

        Dim strSQL As String
        Try
            strSQL = " select SalesOrder_Index from tb_SalesOrder where SalesOrder_No = @SalesOrder_No  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SalesOrder_No", SqlDbType.VarChar, 50).Value = SalesOrder_No

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()

            If GetScalarOutput Is Nothing Then
                _scalarOutput = ""
            Else
                _scalarOutput = GetScalarOutput
            End If


            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return ""
            Else
                Return _scalarOutput
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    ' case new document : you will get generate document id
    Public Function SaveData(Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As String

        Select Case objStatus
            Case enuOperation_Type.ADDNEW

                Me._newWithdraw_Index = Me.InsertData(Transaction)

                If Not Me._newWithdraw_Index = "" Then
                    ' Success 
                    Return Me._newWithdraw_Index
                Else
                    ' Not Success 
                    Return ""
                End If

            Case enuOperation_Type.UPDATE
                Me._newWithdraw_Index = Me.UpdateData(Transaction)
                If Not Me._newWithdraw_Index = "" Then
                    ' Success 
                    Return Me._newWithdraw_Index
                Else
                    ' Not Success 
                    Return ""
                End If

        End Select

        Return True
    End Function

    Public Function SaveWithdrawItem_V4(ByVal dtStandard As DataTable, ByVal pWithdraw_Index As String, ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As String
        Try

            Dim StrSQL As String = ""
            Dim objSysAuto As New Sy_AutoNumber

            Dim objWithdrawHeader As New DataTable
            StrSQL = String.Format("select * from tb_Withdraw where Withdraw_Index = '{0}'", pWithdraw_Index)
            objWithdrawHeader = DBExeQuery(StrSQL, Connection, myTrans, eCommandType.Text)


            For Each drRow As DataRow In dtStandard.Rows
                Dim tmpRatio As Decimal = 1
                Dim tmpScalar As String = ""
                StrSQL = "  SELECT  ms_SKURatio.Ratio "
                StrSQL &= " FROM     ms_SKURatio INNER JOIN "
                StrSQL &= " ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
                StrSQL &= "     ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
                StrSQL &= " WHERE ms_SKURatio.Sku_Index ='{0}' AND ms_SKURatio.Package_Index='{1}' and ms_SKU.status_id != -1"
                StrSQL = String.Format(StrSQL, drRow("Sku_Index"), drRow("Package_Index"))

                With SQLServerCommand
                    .CommandType = CommandType.Text
                    .CommandText = StrSQL
                    .Transaction = myTrans
                    .Connection = Connection
                    tmpScalar = .ExecuteScalar()
                End With

                If IsDBNull(tmpScalar) = False Then
                    tmpRatio = tmpScalar
                End If

                Dim tmpWithdrawItem_Index As String = objSysAuto.getSys_Value(Connection, myTrans, SQLServerCommand, "WithdrawItem_Index")
                ''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
                StrSQL = " INSERT INTO tb_WithdrawItem (WithdrawItem_Index,Withdraw_Index,Sku_Index,Package_Index,PLot,ItemStatus_Index ,Serial_No"
                StrSQL &= " ,Qty,Total_Qty,Plan_Qty,Ratio,Plan_Total_Qty,Weight,Volume,Str1,Str2,Str3,Str4,Str5"
                StrSQL &= " ,Flo1,Flo2,Flo3,Flo4,Flo5,Status,add_by,add_branch"
                StrSQL &= " ,Item_Qty,Price,Item_Package_Index,Declaration_No,Invoice_No,HandlingType_Index,ItemDefinition_Index,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,AssetLocationBalance_Index,DocumentPlan_Index"
                StrSQL &= ",Tax1,Tax2,Tax3,Tax4,Tax5 ,HS_Code,ItemDescription,Seq,Consignee_Index,Str6,Str7,Str8,Str9,Str10,OrderItem_Index)"
                StrSQL &= " Values "
                StrSQL &= "( @WithdrawItem_Index,@Withdraw_Index,@Sku_Index,@Package_Index,@PLot,@ItemStatus_Index ,@Serial_No"
                StrSQL &= " ,@Qty,@Total_Qty,@Plan_Qty,@Ratio,@Plan_Total_Qty,@Weight,@Volume,@Str1,@Str2,@Str3,@Str4,@Str5"
                StrSQL &= " ,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@Status,@add_by,@add_branch,@Item_Qty,@Price,@Item_Package_Index,@Declaration_No,@Invoice_No,@HandlingType_Index,@ItemDefinition_Index,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@AssetLocationBalance_Index,@DocumentPlan_Index"
                StrSQL &= ",@Tax1,@Tax2,@Tax3,@Tax4,@Tax5,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@Str6,@Str7,@Str8,@Str9,@Str10,@OrderItem_Index)"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = tmpWithdrawItem_Index
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = pWithdraw_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drRow("Sku_Index")
                    .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = drRow("PLot")
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = drRow("ItemStatus_Index")
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = drRow("Package_Index")
                    .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = CDec(drRow("Qty"))
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = CDec(drRow("Qty")) * tmpRatio
                    .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = CDec(drRow("Qty"))
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = tmpRatio
                    .Parameters.Add("@Plan_Total_Qty", SqlDbType.Float, 8).Value = CDec(drRow("Qty")) * tmpRatio
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = CDec(drRow("WeightOut"))
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = CDec(drRow("VolumeOut"))
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = drRow("Reference")
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = drRow("Reference2")
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = drRow("Invoice_No")
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = drRow("Pallet_No")
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = drRow("comment")
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = CDec(drRow("Flo1"))
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = drRow("Serial_No")
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = drRow("QtyItemOut")
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = drRow("Price_Out")
                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = drRow("Item_Package_Index")
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 100).Value = drRow("Invoice_Out")
                    .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = drRow("Declaration_No_Out")
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = drRow("HandlingType_Index")
                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = drRow("Plan_Process")
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = drRow("DocumentPlan_No")
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = drRow("DocumentPlanItem_Index")
                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = drRow("AssetLocationBalance_Index")
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = drRow("DocumentPlan_Index")
                    .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = drRow("Tax1")
                    .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = drRow("Tax2")
                    .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = drRow("Tax3")
                    .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = drRow("Tax4")
                    .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = drRow("Tax5")
                    .Parameters.Add("@HS_Code", SqlDbType.VarChar, 100).Value = drRow("HS_Code")
                    .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 400).Value = drRow("ItemDescription")
                    .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = drRow("Seq")
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = drRow("ConsigneeItem_Index")
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = drRow("Str6")
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = drRow("OrderItem_Index")

                    With SQLServerCommand
                        .CommandType = CommandType.Text
                        .CommandText = StrSQL
                        .Transaction = myTrans
                        .Connection = Connection
                        .ExecuteNonQuery()
                    End With

                    '****************************************************************
                    '--- PlanWithDraw
                    If drRow("Plan_Process") <> -9 Then
                        StatusWithdraw_Document = drRow("Plan_Process")
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation

                                StrSQL = "  UPDATE tb_SalesOrderItem "
                                StrSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+@Qty "
                                StrSQL &= " , Total_Qty_Withdraw =isnull(Total_Qty_Withdraw,0)+@Total_Qty "
                                StrSQL &= " , Weight_Withdraw =isnull(Weight_Withdraw,0)+@Weight "
                                StrSQL &= " , Volume_Withdraw =isnull(Volume_Withdraw,0)+@Volume "

                                If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", drRow("DocumentPlanItem_Index"), "Isnull(Last_Withdraw_Date,'')", CDate(objWithdrawHeader.Rows(0)("Withdraw_Date")).ToString("yyyy/MM/dd"), Connection, myTrans, SQLServerCommand) Then
                                    StrSQL &= " ,Last_Withdraw_Date='" & CDate(objWithdrawHeader.Rows(0)("Withdraw_Date")).ToString("yyyy/MM/dd") & "'"
                                End If

                                StrSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                            Case Withdraw_Document.Packing
                                StrSQL = "  UPDATE tb_PackingItem"
                                StrSQL &= " SET Qty_WithDraw =Qty_WithDraw+@Qty "
                                StrSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"
                            Case Withdraw_Document.Reserve
                                StrSQL = " UPDATE tb_Reserve SET  "
                                StrSQL &= " Status=4 "
                                StrSQL &= " WHERE Reserve_Index =@DocumentPlan_Index"
                            Case Withdraw_Document.Transport
                                StrSQL = "  UPDATE tb_SalesOrderItem "

                                StrSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+(@Total_Qty / Ratio) "
                                StrSQL &= " , Total_Qty_Withdraw =isnull(Total_Qty_Withdraw,0)+@Total_Qty "
                                StrSQL &= " , Weight_Withdraw =isnull(Weight_Withdraw,0)+@Weight "
                                StrSQL &= " , Volume_Withdraw =isnull(Volume_Withdraw,0)+@Volume "

                                If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", drRow("DocumentPlanItem_Index"), "Isnull(Last_Withdraw_Date,'')", CDate(objWithdrawHeader.Rows(0)("Withdraw_Date")).ToString("yyyy/MM/dd"), Connection, myTrans, SQLServerCommand) Then
                                    StrSQL &= " ,Last_Withdraw_Date='" & CDate(objWithdrawHeader.Rows(0)("Withdraw_Date")).ToString("yyyy/MM/dd") & "'"
                                End If
                                StrSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                        End Select
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = drRow("DocumentPlanItem_Index")
                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = drRow("DocumentPlan_Index")
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = CDec(drRow("Qty"))
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = CDec(drRow("Qty")) * tmpRatio
                            .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = drRow("QtyItemOut")
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = CDec(drRow("WeightOut"))
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = CDec(drRow("VolumeOut"))
                        End With

                        With SQLServerCommand
                            .CommandType = CommandType.Text
                            .CommandText = StrSQL
                            .Connection = Connection
                            .ExecuteNonQuery()
                        End With

                        UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, _objItem.Plan_Process, Connection, myTrans)

                    End If
                End With

                StrSQL = "Insert into tb_WithdrawItemLocation "
                StrSQL &= "( WithdrawItemLocation_Index,Withdraw_Index,WithdrawItem_Index,Order_Index,Sku_Index,Lot_No,Plot,ItemStatus_Index,Tag_No,LocationBalance_Index,Location_Index,Serial_No,Qty,Package_Index,Total_Qty,Weight,Volume,Status,add_by,add_date,add_branch,Pallet_Qty,Item_Qty,Price,tagout_no,TAG_Index ) "
                StrSQL &= "  Values ( "
                StrSQL &= "@WithdrawItemLocation_Index,"
                StrSQL &= "@Withdraw_Index,"
                StrSQL &= "@WithdrawItem_Index,"
                StrSQL &= "@Order_Index,"
                StrSQL &= "@Sku_Index,"
                StrSQL &= "@Lot_No,"
                StrSQL &= "@Plot,"
                StrSQL &= "@ItemStatus_Index,"
                StrSQL &= "@Tag_No,"
                StrSQL &= "@LocationBalance_Index,"
                StrSQL &= "@Location_Index ,"
                StrSQL &= "@Serial_No,"
                StrSQL &= "@Qty,"
                StrSQL &= "@Package_Index,"
                StrSQL &= "@Total_Qty,"
                StrSQL &= "@Weight,"
                StrSQL &= "@Volume,"
                StrSQL &= "'-9',"
                StrSQL &= "@add_by,"
                StrSQL &= "getdate(),"
                StrSQL &= "@add_branch,"
                StrSQL &= "@Pallet_Qty,"
                StrSQL &= "@Item_Qty,"
                StrSQL &= "@Price,"
                StrSQL &= "@TagOut_No,"
                StrSQL &= "@TAG_Index)"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@WithdrawItemLocation_Index", SqlDbType.VarChar, 13).Value = objSysAuto.getSys_Value(Connection, myTrans, "WithdrawItemLocation_Index")
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = pWithdraw_Index
                    .Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = tmpWithdrawItem_Index
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = drRow("Order_Index")
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drRow("Sku_Index")
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = drRow("PLot")
                    .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = drRow("PLot")
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = drRow("ItemStatus_Index")
                    .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = drRow("Tag_No")
                    .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = drRow("LocationBalance_Index")
                    .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = drRow("Location_Index")
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = drRow("Serial_No")
                    .Parameters.Add("@Qty", SqlDbType.Float).Value = CDec(drRow("Qty"))
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = drRow("Package_Index")
                    .Parameters.Add("@Total_Qty", SqlDbType.Float).Value = CDec(drRow("Qty")) * tmpRatio
                    .Parameters.Add("@Weight", SqlDbType.Float).Value = CDec(drRow("WeightOut"))
                    .Parameters.Add("@Volume", SqlDbType.Float).Value = CDec(drRow("VolumeOut"))
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@Pallet_Qty", SqlDbType.Int).Value = drRow("Pallet_Qty")
                    .Parameters.Add("@Item_Qty", SqlDbType.Float).Value = drRow("QtyItemOut")
                    .Parameters.Add("@Price", SqlDbType.Float).Value = drRow("Price_Out")
                    .Parameters.Add("@TagOut_No", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = drRow("TAG_Index")
                End With

                With SQLServerCommand
                    .CommandType = CommandType.Text
                    .CommandText = StrSQL
                    .Connection = Connection
                    .ExecuteNonQuery()
                End With

                '''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''''
            Next

            Return "S"
        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region

#Region "ADD NEW Withdraw"
    ''' <summary>
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Tax,Seq,Consignee
    ''' -------------------------------------------------
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertData(Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As String
        Dim strSQL As String = ""
        Dim myTrans As SqlClient.SqlTransaction = Nothing

        Dim IsMyTransaction As Boolean = Transaction Is Nothing
        If IsMyTransaction Then
            connectDB()
            myTrans = Connection.BeginTransaction(IsolationLevel.Serializable)
        Else
            Connection = Transaction.Connection
            myTrans = Transaction
        End If

        SQLServerCommand.Connection = Connection
        SQLServerCommand.Transaction = myTrans
        SQLServerCommand.CommandTimeout = 0
        '   Dim objReader As SqlClient.SqlDataReader = Nothing
        Try


            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_SalesOrderItem set SalesOrderItem_Index = '0010000000001' where SalesOrderItem_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update ms_Location set Location_Index = '0010000000001' where Location_Index = '0010000000001' ", Connection, myTrans)




            ' ***** Step 1 Insert Header : tb_withdraw ***
            ' ******************************************************************
            strSQL = " INSERT INTO tb_Withdraw ( Withdraw_Index,Withdraw_No,Withdraw_Date,Customer_Index,Department_Index,DocumentType_Index, "
            strSQL &= " Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,"
            strSQL &= " Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Contact_Name,Comment, "
            strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_date,add_by,add_branch,"
            strSQL &= " Customer_Shipping_Index,Driver_Index,Round,Leave_Time,Factory_In,Factory_Out,Return_Time,SO_No,Invoice_No,ASN_No,Departure_Date,Arrival_Date,"
            strSQL &= "  Vassel_Name, Flight_No, Vehicle_No, Transport_by, Origin_Port_Id, Origin_Country_Id, Destination_Port_Id, Destination_Country_Id, Terminal_Id,HandlingType_Index,ApprovedBy_Name,Checker_Name,Shipper_Index,Withdraw_Type"
            strSQL &= " ) "
            strSQL &= " Values "
            strSQL &= "  ( @Withdraw_Index,@Withdraw_No,@Withdraw_Date,@Customer_Index,@Department_Index,@DocumentType_Index,"
            strSQL &= " @Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5,"
            strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10, @Contact_Name,@Comment,"
            strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,getdate(),@add_by,@add_branch,"
            strSQL &= " @Customer_Shipping_Index,@Driver_Index,@Round,@Leave_Time,@Factory_In,@Factory_Out,@Return_Time,@SO_No,@Invoice_No,@ASN_No,@Departure_Date,@Arrival_Date,"
            strSQL &= " @Vassel_Name, @Flight_No, @Vehicle_No, @Transport_by, @Origin_Port_Id, @Origin_Country_Id, @Destination_Port_Id, @Destination_Country_Id, @Terminal_Id,@HandlingType_Index,@ApprovedBy_Name,@Checker_Name,@Shipper_Index,@Withdraw_Type"
            strSQL &= " ) "

            If Trim(_objHeader.Withdraw_No) = "" Then
                Dim objDocumentNumber As New Sy_DocumentNumber
                _objHeader.Withdraw_No = (objDocumentNumber.getAuto_DocumentNumber(Connection, myTrans, "WTH", "Withdraw_No", "tb_Withdraw"))
                objDocumentNumber = Nothing
            End If


            With SQLServerCommand
                '        gSB_GetDBServerDateTime()
                .Parameters.Clear()
                .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objHeader.Withdraw_Index
                .Parameters.Add("@Withdraw_No", SqlDbType.VarChar, 50).Value = _objHeader.Withdraw_No
                _objHeader.Withdraw_Date = CDate(_objHeader.Withdraw_Date.ToString("yyyy/MM/dd"))
                .Parameters.Add("@Withdraw_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Withdraw_Date
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Parameters.Add("@Contact_Name", SqlDbType.NVarChar, 50).Value = _objHeader.Contact_Name
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objHeader.Comment
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No2
                .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No3
                .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No4
                .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No5
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = _objHeader.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = _objHeader.Str10
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objHeader.Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID


                'Add New 19/6/2008
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.NVarChar, 13).Value = _objHeader.Customer_Shipping_Index
                .Parameters.Add("@Driver_Index", SqlDbType.NVarChar, 13).Value = _objHeader.Driver_Index
                .Parameters.Add("@Round", SqlDbType.SmallDateTime, 4).Value = _objHeader.Round
                .Parameters.Add("@Leave_Time", SqlDbType.SmallDateTime, 4).Value = _objHeader.Leave_Time
                .Parameters.Add("@Factory_In", SqlDbType.SmallDateTime, 4).Value = _objHeader.Factory_In
                .Parameters.Add("@Factory_Out", SqlDbType.SmallDateTime, 4).Value = _objHeader.Factory_Out
                .Parameters.Add("@Return_Time", SqlDbType.SmallDateTime, 4).Value = _objHeader.Return_Time


                '--- AddNew 19/05/2009

                .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = _objHeader.Vassel_Name
                .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = _objHeader.Flight_No
                .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = _objHeader.Vehicle_No
                .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = _objHeader.Transport_by
                .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Port_Id
                .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Country_Id
                .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Port_Id
                .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Country_Id
                .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = _objHeader.Terminal_Id
                .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index

                .Parameters.Add("@So_No", SqlDbType.VarChar, 50).Value = _objHeader.SO_No
                .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objHeader.Invoice_No
                .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objHeader.ASN_No

                .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Departure_Date
                .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Arrival_Date

                .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 50).Value = _objHeader.ApprovedBy_Name
                .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 50).Value = _objHeader.Checker_Name
                .Parameters.Add("@Shipper_Index", SqlDbType.NVarChar, 13).Value = _objHeader.Shipper_Index
                .Parameters.Add("@Withdraw_Type", SqlDbType.Bit, 1).Value = _objHeader.Withdraw_Type
            End With

            ' *****************************************************************


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()



            ' **** Step 2 Insert ProductItem  : tb_WithdrawItem 


            For Each _objItem In _objItemCollection

                strSQL = " INSERT INTO tb_WithdrawItem (WithdrawItem_Index,Withdraw_Index,Sku_Index,Package_Index,PLot,ItemStatus_Index ,Serial_No"
                strSQL &= " ,Qty,Total_Qty,Plan_Qty,Ratio,Plan_Total_Qty,Weight,Volume,Str1,Str2,Str3,Str4,Str5"
                strSQL &= " ,Flo1,Flo2,Flo3,Flo4,Flo5,Status,add_by, add_date,add_branch"
                strSQL &= " ,Item_Qty,Price,Item_Package_Index,Declaration_No,Invoice_No,HandlingType_Index,ItemDefinition_Index,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,AssetLocationBalance_Index,DocumentPlan_Index"
                strSQL &= ",Tax1,Tax2,Tax3,Tax4,Tax5 ,HS_Code,ItemDescription,Seq,Consignee_Index,Str6,Str7,Str8,Str9,Str10,OrderItem_Index, ERP_Location, NewItemFlag)"
                strSQL &= " Values "
                strSQL &= "( @WithdrawItem_Index,@Withdraw_Index,@Sku_Index,@Package_Index,@PLot,@ItemStatus_Index ,@Serial_No"
                strSQL &= " ,@Qty,@Total_Qty,@Plan_Qty,@Ratio,@Plan_Total_Qty,@Weight,@Volume,@Str1,@Str2,@Str3,@Str4,@Str5"
                strSQL &= " ,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@Status,@add_by, GETDATE(),@add_branch,@Item_Qty,@Price,@Item_Package_Index,@Declaration_No,@Invoice_No,@HandlingType_Index,@ItemDefinition_Index,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@AssetLocationBalance_Index,@DocumentPlan_Index"
                strSQL &= ",@Tax1,@Tax2,@Tax3,@Tax4,@Tax5,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@Str6,@Str7,@Str8,@Str9,@Str10,@OrderItem_Index, @ERP_Location, @NewItemFlag)"

                With SQLServerCommand
                    '        gSB_GetDBServerDateTime()
                    .Parameters.Clear()

                    .Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = _objItem.WithdrawItem_Index
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objItem.Withdraw_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objItem.Sku_Index
                    .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = _objItem.PLot
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemStatus_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Package_Index
                    .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemDefinition_Index


                    ' *** Need to set Qty=0  and Total_Qty =0 
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objItem.Qty
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objItem.Total_Qty
                    ' ***************************************

                    .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Qty
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = _objItem.Ratio
                    .Parameters.Add("@Plan_Total_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Total_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objItem.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objItem.Flo5
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = _objItem.Serial_No
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = _objItem.ItemQty
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = _objItem.Price

                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Item_Package_Index
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 100).Value = _objItem.Invoice_No
                    .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = _objItem.Declaration_No
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingType_Index

                    '--- Plan WithDraw 
                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = _objItem.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = _objItem.DocumentPlan_No
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _objItem.AssetLocationBalance_Index
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index

                    'Dong_kk Tax,Seq,Consignee

                    .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = _objItem.Tax1
                    .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = _objItem.Tax2
                    .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = _objItem.Tax3
                    .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = _objItem.Tax4
                    .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = _objItem.Tax5
                    .Parameters.Add("@HS_Code", SqlDbType.VarChar, 100).Value = _objItem.HS_Code
                    .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 400).Value = _objItem.ItemDescription
                    .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = _objItem.Seq
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objItem.Consignee_Index

                    '11-02-2010 ja add Str6-Str10
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = _objItem.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = _objItem.Str10

                    '16-02-2010 ja
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.OrderItem_Index


                    .Parameters.Add("@ERP_Location", SqlDbType.VarChar).Value = _objItem.ERP_Location
                    .Parameters.Add("@NewItemFlag", SqlDbType.VarChar).Value = _objItem.NewItem

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' ****************************************************************
                    '--- PlanWithDraw

                    If _objItem.Plan_Process <> -9 Then
                        StatusWithdraw_Document = _objItem.Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                strSQL = "  UPDATE tb_SalesOrderItem "
                                strSQL &= " SET Qty_WithDraw =ISNULL(Qty_WithDraw,0)+(@Total_Qty / Ratio) "
                                strSQL &= " , Total_Qty_Withdraw =ISNULL(Total_Qty_Withdraw,0)+@Total_Qty "
                                strSQL &= " , Weight_Withdraw =ISNULL(Weight_Withdraw,0)+@Weight "
                                strSQL &= " , Volume_Withdraw =ISNULL(Volume_Withdraw,0)+@Volume "
                                'strSQL &= " , Status =3 "

                                If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", _objItem.DocumentPlanItem_Index, "Last_Withdraw_Date", _objHeader.Withdraw_Date.ToString("yyyy/MM/dd")) Then
                                    strSQL &= " ,Last_Withdraw_Date='" & _objHeader.Withdraw_Date.ToString("yyyy/MM/dd") & "'"
                                End If
                                strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "


                            Case Withdraw_Document.Packing
                                strSQL = "  UPDATE tb_PackingItem"
                                strSQL &= " SET Qty_WithDraw =Qty_WithDraw+@Qty "
                                strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"

                            Case Withdraw_Document.Reserve
                                strSQL = " UPDATE tb_Reserve SET  "
                                strSQL &= " Status=4 "
                                strSQL &= " WHERE Reserve_Index =@DocumentPlan_Index"

                            Case Withdraw_Document.Transport 'killz update 08-08-2011

                                strSQL = "  UPDATE tb_SalesOrderItem "
                                'strSQL &= " SET Qty_WithDraw =isnull(Qty_WithDraw,0)+@Qty "
                                strSQL &= " SET Qty_WithDraw =ISNULL(Qty_WithDraw,0)+(@Total_Qty / Ratio) "
                                strSQL &= " , Total_Qty_Withdraw =ISNULL(Total_Qty_Withdraw,0)+@Total_Qty "
                                strSQL &= " , Weight_Withdraw =ISNULL(Weight_Withdraw,0)+@Weight "
                                strSQL &= " , Volume_Withdraw =ISNULL(Volume_Withdraw,0)+@Volume "
                                'strSQL &= " , Status =3 "

                                If Check_LastWithdraw_Date("tb_SalesOrderItem", "SalesOrderItem_Index", _objItem.DocumentPlanItem_Index, "Last_Withdraw_Date", _objHeader.Withdraw_Date.ToString("yyyy/MM/dd")) Then
                                    strSQL &= " ,Last_Withdraw_Date='" & _objHeader.Withdraw_Date.ToString("yyyy/MM/dd") & "'"
                                End If
                                strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "


                        End Select
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objItem.Qty
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objItem.Total_Qty
                            .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = _objItem.ItemQty
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume

                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        'Update Status All Document  WithDraw
                        UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, _objItem.Plan_Process, Connection, myTrans)

                    End If


                End With

            Next


            ' --- ADD NEW By Dong_KK Need InSert To Transaction
            If _objItemCollectionWITL IsNot Nothing Then
                For Each _objItemWITL In _objItemCollectionWITL


                    strSQL = "Insert into tb_WithdrawItemLocation "
                    strSQL &= " (WithdrawItemLocation_Index,Withdraw_Index,WithdrawItem_Index,Order_Index,Sku_Index,Lot_No,Plot,ItemStatus_Index,Tag_No,LocationBalance_Index,Location_Index,Serial_No,Qty,Package_Index,Total_Qty,Weight,Volume,Status,add_by,add_date,add_branch,Pallet_Qty,Item_Qty,Price,tagout_no,TAG_Index, ERP_Location) "
                    strSQL &= "  values("
                    strSQL &= "'" & _objItemWITL.WithdrawItemLocation_Index & "',"
                    strSQL &= "'" & _objItemWITL.Withdraw_Index & "',"
                    strSQL &= "'" & _objItemWITL.WithdrawItem_Index & "',"
                    strSQL &= "'" & _objItemWITL.Order_Index & "',"
                    strSQL &= "'" & _objItemWITL.Sku_Index & "',"
                    strSQL &= "'" & _objItemWITL.Lot_No & "',"
                    strSQL &= "'" & _objItemWITL.Plot & "',"
                    strSQL &= "'" & _objItemWITL.ItemStatus_Index & "',"
                    strSQL &= "'" & _objItemWITL.Tag_No & "',"
                    strSQL &= "'" & _objItemWITL.LocationBalance_Index & "',"
                    strSQL &= "'" & _objItemWITL.Location_Index & "' ,"
                    strSQL &= "'" & _objItemWITL.Serial_No & "',"
                    strSQL &= "'" & _objItemWITL.Qty & "',"
                    strSQL &= "'" & _objItemWITL.Package_Index & "',"
                    strSQL &= "'" & _objItemWITL.Total_Qty & "',"
                    strSQL &= "'" & _objItemWITL.Weight & "',"
                    strSQL &= "'" & _objItemWITL.Volume & "',"
                    strSQL &= "'-9',"
                    strSQL &= "'" & WV_UserName & "',"
                    strSQL &= "getdate(),"
                    strSQL &= "'" & WV_Branch_ID & "',"
                    strSQL &= "'" & _objItemWITL.Pallet_Qty & "',"
                    strSQL &= "'" & _objItemWITL.Item_Qty & "',"
                    strSQL &= "'" & _objItemWITL.Price & "',"
                    strSQL &= "'" & _objItemWITL.TagOut_No & "',"
                    strSQL &= "'" & _objItemWITL.TAG_Index & "',"
                    strSQL &= "'" & _objItemWITL.ERP_Location & "')"

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                Next
            End If


            ' ***  Step 3 get New Sys_Value  ***
            Dim objDBIndex As New Sy_AutoNumber
            Me._newWithdraw_Index = objDBIndex.getSys_Value("Withdraw_Index")
            'objDBIndex = Nothing

            ' *** Step 4 update New Withdraw_Index with tb_Withdraw ***
            strSQL = " UPDATE tb_Withdraw SET  "
            strSQL &= " Withdraw_Index='" & Me._newWithdraw_Index & "' ,Status=1 "
            strSQL &= " WHERE Withdraw_Index='" & Me._objHeader.Withdraw_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            strSQL = " UPDATE tb_PalletType_History SET  "
            strSQL &= " Withdraw_Index='" & Me._newWithdraw_Index & "' "
            strSQL &= " WHERE Withdraw_Index='" & Me._objHeader.Withdraw_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' *** Step 5 update New Order_Index with tb_WithdrawItem ***
            strSQL = " UPDATE tb_WithdrawItem SET  "
            strSQL &= " Withdraw_Index='" & Me._newWithdraw_Index & "' ,Status=1 "
            strSQL &= " WHERE Withdraw_Index='" & Me._objHeader.Withdraw_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            If _Packing_index <> "" Then
                'strSQL = "update tb_Packing set withdraw_index = '" & Me._objHeader.Withdraw_Index & "'"
                'strSQL &= " where Packing_Index = '" & Me._PackingBom_index & "'"
                strSQL = " insert into tb_PackingWithdraw(Packing_Index,Withdraw_Index)"
                strSQL &= " values ('" & Me._Packing_index & "','" & Me._newWithdraw_Index & "')"
            End If
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' *** Step 5 update New Order_Index with tb_WithdrawItem ***
            strSQL = " UPDATE tb_WithdrawItemLocation SET  "
            strSQL &= " Withdraw_Index='" & Me._newWithdraw_Index & "'"
            strSQL &= " WHERE Withdraw_Index='" & Me._objHeader.Withdraw_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            If _Packing_index <> "" Then
                'strSQL = "update tb_Packing set withdraw_index = '" & Me._objHeader.Withdraw_Index & "'"
                'strSQL &= " where Packing_Index = '" & Me._PackingBom_index & "'"
                strSQL = " insert into tb_PackingWithdraw(Packing_Index,Withdraw_Index)"
                strSQL &= " values ('" & Me._Packing_index & "','" & Me._newWithdraw_Index & "')"
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            strSQL = " UPDATE tb_WithDrawTruckOut SET  "
            strSQL &= " Withdraw_Index='" & Me._newWithdraw_Index & "' ,Status_Id =1 "
            strSQL &= " WHERE Withdraw_Index='" & Me._objHeader.Withdraw_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            strSQL = " UPDATE tb_WithDrawSN SET  "
            strSQL &= " Withdraw_Index='" & Me._newWithdraw_Index & "' ,Status_Id =1 "
            strSQL &= " WHERE Withdraw_Index='" & Me._objHeader.Withdraw_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            'Dong 2011/01/20 Default Assigne User
            Dim oAssign As New tb_AssignJob
            With oAssign
                .User_Index = "0010000000001"
                .Assign_Date = Now
                .DocumentPlan_No = _objHeader.Withdraw_No
                .DocumentPlan_Index = _newWithdraw_Index
                .Plan_Process = 2
                .Priority = 3
                .AssignJob_Index = objDBIndex.getSys_Value("AssignJob_Index")
                .InsertData()
            End With

            ' --- STEP 6: Add Log to Sy_Audit_Log
            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = _newWithdraw_Index
            oAudit_Log.Document_No = _objHeader.Withdraw_No
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_Picking_Order, Connection, myTrans)
            objDBIndex = Nothing


            '*** Commit transaction
            If IsMyTransaction Then
                myTrans.Commit()
            End If

            Return Me._newWithdraw_Index
        Catch ex As Exception
            If IsMyTransaction Then
                myTrans.Rollback()
            End If

            Throw ex
        End Try



    End Function
#End Region

#Region "UPDATE Withdraw "
    ''' <summary>
    ''' </summary>
    ''' <remarks>
    ''' 14-01-2010
    ''' JA
    ''' UPDATE STR6-STR10
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Tax,Seq,Consignee
    ''' -------------------------------------------------
    ''' </remarks>

    Private Function UpdateData(Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As String
        Dim strSQL As String = ""

        Dim myTrans As SqlClient.SqlTransaction = Nothing

        Dim IsMyTransaction As Boolean = Transaction Is Nothing
        If IsMyTransaction Then
            connectDB()
            myTrans = Connection.BeginTransaction(IsolationLevel.Serializable)
        Else
            Connection = Transaction.Connection
            myTrans = Transaction
        End If

        SQLServerCommand.Connection = Connection
        SQLServerCommand.Transaction = myTrans
        SQLServerCommand.CommandTimeout = 0

        'Dim objReader As SqlClient.SqlDataReader = Nothing
        Try

            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_SalesOrderItem set SalesOrderItem_Index = '0010000000001' where SalesOrderItem_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update ms_Location set Location_Index = '0010000000001' where Location_Index = '0010000000001' ", Connection, myTrans)


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objHeader.Withdraw_Index
            End With

            Dim CurrentStatus As String = DBExeQuery_Scalar("Select Status from tb_Withdraw Where Withdraw_Index = @Withdraw_Index", Connection, myTrans)


            'If CurrentStatus = "-2" OrElse CurrentStatus = "1" Then

            'Else
            '    Throw New Exception(String.Format("ไม่สามารถบันทึกรายการได้  !! (CurrentStatus = {0})", CurrentStatus))
            'End If


            Select Case CurrentStatus
                Case "-2", "1", "4"
                Case Else
                    Throw New Exception(String.Format("ไม่สามารถบันทึกรายการได้  !! (CurrentStatus = {0})", CurrentStatus))
            End Select
            '***** Step 1 Update Header : tb_Withdraw ***

            strSQL = " UPDATE tb_Withdraw "
            strSQL &= " SET "
            strSQL &= " Withdraw_No=@Withdraw_No,Withdraw_Date=@Withdraw_Date,Customer_Index=@Customer_Index,Department_Index=@Department_Index,DocumentType_Index=@DocumentType_Index,"
            strSQL &= " Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Ref_No3=@Ref_No3,Ref_No4=@Ref_No4,Ref_No5=@Ref_No5, "
            strSQL &= " Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,"
            strSQL &= " Str6=@Str6,Str7=@Str7,Str8=@Str8,Str9=@Str9,Str10=@Str10,"
            strSQL &= " Contact_Name=@Contact_Name,Comment=@Comment,ASN_No=@ASN_No,Departure_Date=@Departure_Date,Arrival_Date=@Arrival_Date,"
            strSQL &= " Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,update_date=getdate(),update_by=@update_by,update_branch=@update_branch, "
            strSQL &= " Customer_Shipping_Index=@Customer_Shipping_Index,Driver_Index=@Driver_Index,Round=@Round,Leave_Time=@Leave_Time,Factory_In=@Factory_In,Factory_Out=@Factory_Out,Return_Time=@Return_Time,SO_No=@SO_No,Invoice_No=@Invoice_No,"
            strSQL &= " Vassel_Name=@Vassel_Name, Flight_No=@Flight_No, Vehicle_No=@Vehicle_No, Transport_by=@Transport_by, Origin_Port_Id=@Origin_Port_Id, Origin_Country_Id=@Origin_Country_Id, Destination_Port_Id=@Destination_Port_Id, "
            strSQL &= " Destination_Country_Id=@Destination_Country_Id, Terminal_Id=@Terminal_Id,HandlingType_Index=@HandlingType_Index, ApprovedBy_Name=@ApprovedBy_Name,Checker_Name=@Checker_Name,Shipper_Index=@Shipper_Index,Withdraw_Type=@Withdraw_Type"
            'strSQL &= " ,Status = 1 "
            strSQL &= " WHERE Withdraw_Index=@Withdraw_Index "


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objHeader.Withdraw_Index
                .Parameters.Add("@Withdraw_No", SqlDbType.VarChar, 50).Value = _objHeader.Withdraw_No
                _objHeader.Withdraw_Date = CDate(_objHeader.Withdraw_Date.ToString("yyyy/MM/dd"))
                .Parameters.Add("@Withdraw_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Withdraw_Date
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Parameters.Add("@Contact_Name", SqlDbType.NVarChar, 100).Value = _objHeader.Contact_Name
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objHeader.Comment
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No2
                .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No3
                .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No4
                .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No5
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = _objHeader.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = _objHeader.Str10
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objHeader.Flo5
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                ''Add New 19/6/2008
                .Parameters.Add("@Customer_Shipping_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Shipping_Index
                .Parameters.Add("@Driver_Index", SqlDbType.VarChar, 13).Value = _objHeader.Driver_Index
                .Parameters.Add("@Round", SqlDbType.SmallDateTime, 4).Value = _objHeader.Round
                .Parameters.Add("@Leave_Time", SqlDbType.SmallDateTime, 4).Value = _objHeader.Leave_Time
                .Parameters.Add("@Factory_In", SqlDbType.SmallDateTime, 4).Value = _objHeader.Factory_In
                .Parameters.Add("@Factory_Out", SqlDbType.SmallDateTime, 4).Value = _objHeader.Factory_Out
                .Parameters.Add("@Return_Time", SqlDbType.SmallDateTime, 4).Value = _objHeader.Return_Time

                'Add New 19/05/2009

                .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = _objHeader.Vassel_Name
                .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = _objHeader.Flight_No
                .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = _objHeader.Vehicle_No
                .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = _objHeader.Transport_by
                .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Port_Id
                .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Country_Id
                .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Port_Id
                .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Country_Id
                .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = _objHeader.Terminal_Id
                .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index

                .Parameters.Add("@SO_No", SqlDbType.VarChar, 50).Value = _objHeader.SO_No
                .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objHeader.Invoice_No
                .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objHeader.ASN_No

                .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Departure_Date
                .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Arrival_Date

                .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 50).Value = _objHeader.ApprovedBy_Name
                .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 50).Value = _objHeader.Checker_Name
                .Parameters.Add("@Shipper_Index", SqlDbType.VarChar, 13).Value = _objHeader.Shipper_Index
                .Parameters.Add("@Withdraw_Type", SqlDbType.Bit, 1).Value = _objHeader.Withdraw_Type

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()




            ' ************ BEGIN UPDATE WITHDRAW ITEM ******************
            Dim iWIL As Integer = 0
            For Each _objItem In _objItemCollection
                If isExistID(_objItem.WithdrawItem_Index) = True Then
                    strSQL = " UPDATE tb_WithdrawItem" & _
                    " SET WithdrawItem_Index=@WithdrawItem_Index," & _
                    "     Withdraw_Index=@Withdraw_Index," & _
                    "     Sku_Index=@Sku_Index," & _
                    "     Package_Index=@Package_Index," & _
                    "     PLot=@PLot," & _
                    "     ItemStatus_Index=@ItemStatus_Index," & _
                    "     Serial_No=@Serial_No," & _
                    "     Qty=@Qty," & _
                    "     Ratio=@Ratio," & _
                    "     Total_Qty=@Total_Qty," & _
                    "     Weight=@Weight," & _
                    "     Volume=@Volume," & _
                    "     Plan_Qty=@Plan_Qty," & _
                    "     Plan_Total_Qty=@Plan_Total_Qty," & _
                    "     Str1=@Str1," & _
                    "     Str2=@Str2," & _
                    "     Str3=@Str3," & _
                    "     Str4=@Str4," & _
                    "     Str5=@Str5," & _
                    "     Flo1=@Flo1," & _
                    "     Flo2=@Flo2," & _
                    "     Flo3=@Flo3," & _
                    "     Flo4=@Flo4," & _
                    "     Flo5=@Flo5," & _
                    "     ItemDefinition_Index=@ItemDefinition_Index," & _
                    "     Item_Qty=@Item_Qty," & _
                    "     Price=@Price," & _
                    "     Item_Package_Index=@Item_Package_Index," & _
                    "     Invoice_No=@Invoice_No," & _
                    "     Declaration_No=@Declaration_No," & _
                    "     HandlingType_Index=@HandlingType_Index," & _
                    "     Plan_Process=@Plan_Process," & _
                    "     DocumentPlan_No=@DocumentPlan_No," & _
                    "     DocumentPlan_Index=@DocumentPlan_Index," & _
                    "     DocumentPlanItem_Index=@DocumentPlanItem_Index," & _
                    "     AssetLocationBalance_Index=@AssetLocationBalance_Index," & _
                    "     Tax1=@Tax1," & _
                    "     Tax2=@Tax2," & _
                    "     Tax3=@Tax3," & _
                    "     Tax4=@Tax4," & _
                    "     Tax5=@Tax5," & _
                    "     HS_Code=@HS_Code," & _
                    "     ItemDescription=@ItemDescription," & _
                    "     Seq=@Seq," & _
                    "     Consignee_Index=@Consignee_Index," & _
                    "     Str6=@Str6," & _
                    "     Str7=@Str7," & _
                    "     Str8=@Str8," & _
                    "     Str9=@Str9," & _
                    "     Str10=@Str10," & _
                    "     OrderItem_Index=@OrderItem_Index ,NewItemFlag = 2," & _
                    "     ERP_Location=@ERP_Location " & _
                    "           WHERE          WithdrawItem_Index = @WithdrawItem_Index"

                Else


                    strSQL = " INSERT INTO tb_WithdrawItem (WithdrawItem_Index,Withdraw_Index,Sku_Index,Package_Index,PLot,ItemStatus_Index ,Serial_No"
                    strSQL &= " ,Qty,Total_Qty,Plan_Qty,Ratio,Plan_Total_Qty,Weight,Volume,Str1,Str2,Str3,Str4,Str5"
                    strSQL &= " ,Flo1,Flo2,Flo3,Flo4,Flo5,Status,add_by,add_branch"
                    strSQL &= " ,Item_Qty,Price,Item_Package_Index,Declaration_No,Invoice_No,HandlingType_Index,ItemDefinition_Index,Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,AssetLocationBalance_Index,DocumentPlan_Index"
                    strSQL &= ",Tax1,Tax2,Tax3,Tax4,Tax5 ,HS_Code,ItemDescription,Seq,Consignee_Index,Str6,Str7,Str8,Str9,Str10,OrderItem_Index,ERP_Location)"
                    strSQL &= " Values "
                    strSQL &= "( @WithdrawItem_Index,@Withdraw_Index,@Sku_Index,@Package_Index,@PLot,@ItemStatus_Index ,@Serial_No"
                    strSQL &= " ,@Qty,@Total_Qty,@Plan_Qty,@Ratio,@Plan_Total_Qty,@Weight,@Volume,@Str1,@Str2,@Str3,@Str4,@Str5"
                    strSQL &= " ,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,1,@add_by,@add_branch,@Item_Qty,@Price,@Item_Package_Index,@Declaration_No,@Invoice_No,@HandlingType_Index,@ItemDefinition_Index,@Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@AssetLocationBalance_Index,@DocumentPlan_Index"
                    strSQL &= ",@Tax1,@Tax2,@Tax3,@Tax4,@Tax5,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@Str6,@Str7,@Str8,@Str9,@Str10,@OrderItem_Index,@ERP_Location) "

                End If


                With SQLServerCommand
                    '        gSB_GetDBServerDateTime()
                    .Parameters.Clear()

                    .Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = _objItem.WithdrawItem_Index
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objItem.Withdraw_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objItem.Sku_Index
                    .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = _objItem.PLot
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemStatus_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = _objItem.Package_Index
                    .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemDefinition_Index


                    ' *** Need to set Qty=0  and Total_Qty =0 
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objItem.Qty
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objItem.Total_Qty
                    ' ***************************************

                    .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Qty
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = _objItem.Ratio
                    .Parameters.Add("@Plan_Total_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Total_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objItem.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objItem.Flo5
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = _objItem.Serial_No
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    '.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    '.Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = _objItem.ItemQty
                    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = _objItem.Price

                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Item_Package_Index
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 100).Value = _objItem.Invoice_No
                    .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = _objItem.Declaration_No
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingType_Index

                    '--- Plan WithDraw 
                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = _objItem.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = _objItem.DocumentPlan_No
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _objItem.AssetLocationBalance_Index
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index
                    'Dong_kk Tax,Seq,Consignee

                    .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = _objItem.Tax1
                    .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = _objItem.Tax2
                    .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = _objItem.Tax3
                    .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = _objItem.Tax4
                    .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = _objItem.Tax5
                    .Parameters.Add("@HS_Code", SqlDbType.VarChar, 100).Value = _objItem.HS_Code
                    .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 200).Value = _objItem.ItemDescription
                    .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = _objItem.Seq
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objItem.Consignee_Index

                    '11-02-2010 ja add Str6-Str10
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 100).Value = _objItem.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 100).Value = _objItem.Str10

                    '16-02-2010 ja
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.OrderItem_Index

                    .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = _objItem.ERP_Location



                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                    ' ************ END UPDATE WITHDRAW ITEM ******************

                    ' ************ BEGIN UPDATE WITHDRAW ITEM LOCATION ******************

                    _objItemWITL = _objItemCollectionWITL.Item(iWIL)

                    If isExistID_WIL(_objItemWITL.WithdrawItemLocation_Index) = True Then
                        strSQL = " UPDATE tb_WithdrawItemLocation" & _
                                      " SET Lot_No=@Lot_No," & _
                                      "     Plot=@Plot," & _
                                      "     Serial_No=@Serial_No" & _
                                      "           WHERE          WithdrawItemLocation_Index = @WithdrawItemLocation_Index"
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@WithdrawItemLocation_Index", SqlDbType.VarChar, 13).Value = _objItemWITL.WithdrawItemLocation_Index
                            .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = _objItemWITL.Lot_No
                            .Parameters.Add("@Plot", SqlDbType.VarChar, 50).Value = _objItemWITL.Plot
                            .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = _objItemWITL.Serial_No
                            '.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            '.Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        End With
                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        EXEC_Command()


                    Else
                        strSQL = "Insert into tb_WithdrawItemLocation "
                        strSQL &= " (WithdrawItemLocation_Index,Withdraw_Index,WithdrawItem_Index,Order_Index,Sku_Index,Lot_No,Plot,ItemStatus_Index,Tag_No,LocationBalance_Index,Location_Index,Serial_No,Qty,Package_Index,Total_Qty,Weight,Volume,Status,add_by,add_date,add_branch,Pallet_Qty,Item_Qty,Price,TagOut_No,TAG_Index, ERP_Location) "
                        strSQL &= "  values("
                        strSQL &= "'" & _objItemWITL.WithdrawItemLocation_Index & "',"
                        strSQL &= "'" & _objItemWITL.Withdraw_Index & "',"
                        strSQL &= "'" & _objItemWITL.WithdrawItem_Index & "',"
                        strSQL &= "'" & _objItemWITL.Order_Index & "',"
                        strSQL &= "'" & _objItemWITL.Sku_Index & "',"
                        strSQL &= "'" & _objItemWITL.Lot_No & "',"
                        strSQL &= "'" & _objItemWITL.Plot & "',"
                        strSQL &= "'" & _objItemWITL.ItemStatus_Index & "',"
                        strSQL &= "'" & _objItemWITL.Tag_No & "',"
                        strSQL &= "'" & _objItemWITL.LocationBalance_Index & "',"
                        strSQL &= "'" & _objItemWITL.Location_Index & "' ,"
                        strSQL &= "'" & _objItemWITL.Serial_No & "',"
                        strSQL &= "'" & _objItemWITL.Qty & "',"
                        strSQL &= "'" & _objItemWITL.Package_Index & "',"
                        strSQL &= "'" & _objItemWITL.Total_Qty & "',"
                        strSQL &= "'" & _objItemWITL.Weight & "',"
                        strSQL &= "'" & _objItemWITL.Volume & "',"
                        strSQL &= "'-9',"
                        strSQL &= "'" & WV_UserName & "',"
                        strSQL &= "getdate(),"
                        strSQL &= "'" & WV_Branch_ID & "',"
                        strSQL &= "'" & _objItemWITL.Pallet_Qty & "',"
                        strSQL &= "'" & _objItemWITL.Item_Qty & "',"
                        strSQL &= "'" & _objItemWITL.Price & "',"
                        strSQL &= "'" & _objItemWITL.TagOut_No & "',"
                        strSQL &= "'" & _objItemWITL.TAG_Index & "',"
                        strSQL &= "'" & _objItemWITL.ERP_Location & "')"

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()

                        '--- PlanWithDraw FOR NEW ITEM ONLY

                        If _objItem.Plan_Process <> -9 Then
                            StatusWithdraw_Document = _objItem.Plan_Process
                            Select Case StatusWithdraw_Document
                                Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                    strSQL = "  UPDATE tb_SalesOrderItem "
                                    strSQL &= " SET Qty_WithDraw = ISNULL(Qty_WithDraw, 0) + (@Total_Qty / Ratio) "
                                    strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw+@Total_Qty "
                                    strSQL &= " , Weight_Withdraw =Weight_Withdraw+@Weight "
                                    strSQL &= " , Volume_Withdraw =Volume_Withdraw+@Volume "
                                    strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                                Case Withdraw_Document.Packing
                                    strSQL = "  UPDATE tb_PackingItem"
                                    strSQL &= " SET Qty_WithDraw =ISNULL(Qty_WithDraw, 0) + @Qty "
                                    strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"

                                Case Withdraw_Document.Transport 'killz update 08-08-2011
                                    strSQL = "  UPDATE tb_SalesOrderItem "
                                    strSQL &= " SET Qty_WithDraw =ISNULL(Qty_WithDraw, 0) + (@Total_Qty / Ratio) "
                                    strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw+@Total_Qty "
                                    strSQL &= " , Weight_Withdraw =Weight_Withdraw+@Weight "
                                    strSQL &= " , Volume_Withdraw =Volume_Withdraw+@Volume "
                                    strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                            End Select
                            With SQLServerCommand
                                .Parameters.Clear()
                                .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objItem.Qty
                                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objItem.Total_Qty
                                .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = _objItem.ItemQty
                                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume
                            End With
                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()
                            'Update Status All Document  WithDraw
                            UpdatePlanDocument_Status(_objItem.DocumentPlan_Index, StatusWithdraw_Document, Connection, myTrans)
                        End If


                    End If

                    'Loop WitdrawItemLocation
                    iWIL += 1


                    ' ************ END UPDATE WITHDRAW ITEM LOCATION ******************



                End With

            Next

            ' Update tb_palletHistory

            For Each _objPalletType In _objPalletTypeCollection

                If isExistIDForPalletType(_objPalletType.PalletType_History_Index) = True Then

                    strSQL = " UPDATE tb_PalletType_History "
                    strSQL &= " SET "
                    strSQL &= "PalletType_Index=@PalletType_Index,Process_Id=@Process_Id,"
                    strSQL &= " Withdraw_Index=@Withdraw_Index,Qty_Out=@Qty_Out,"
                    strSQL &= " Qty_Bal=@Qty_Bal"
                    strSQL &= " WHERE PalletType_History_Index=@PalletType_History_Index "

                Else
                    strSQL = "INSERT INTO tb_PalletType_History "
                    strSQL &= " (PalletType_History_Index,Withdraw_Index,PalletType_Index,Process_Id,Qty_Out,Qty_Bal)"
                    strSQL &= " Values "
                    strSQL &= " (@PalletType_History_Index,@Withdraw_Index,@PalletType_Index,@Process_Id,@Qty_Out,@Qty_Bal)"


                End If
                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = _objPalletType.PalletType_History_Index
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _objPalletType.PalletType_Index
                    .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = _objPalletType.Process_Id
                    .Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = _objHeader.Withdraw_Index
                    .Parameters.Add("@Qty_Out", SqlDbType.VarChar, 50).Value = _objPalletType.Qty_Out
                    .Parameters.Add("@Qty_Bal", SqlDbType.VarChar, 50).Value = _objPalletType.Qty_Bal

                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next

            If _Packing_index <> "" Then
                strSQL = " update tb_PackingWithdraw set Packing_Index = '" & Me._Packing_index & "'"
                strSQL &= " where Withdraw_Index = '" & Whitdraw_Index & "'"
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            End If
            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = _objHeader.Withdraw_Index
            oAudit_Log.Document_No = _objHeader.Withdraw_No
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_Picking_Order, Connection, myTrans)

            '
            '*** Commit transaction
            If IsMyTransaction Then
                myTrans.Commit()
            End If

            Return _objHeader.Withdraw_Index
        Catch ex As Exception
            If IsMyTransaction Then
                myTrans.Rollback()
            End If

            Throw ex
        End Try

    End Function
    Public Function Updatewithdraw_Transaction(ByVal _OldWithdraw_No As String, ByVal pstrWithdraw_index As String)
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        'Dim objReader As SqlClient.SqlDataReader = Nothing
        Try



            strSQL = " update tb_transaction set Transaction_Id = (select top 1 withdraw_no  from tb_withdraw     where withdraw_index='" & pstrWithdraw_index & "') "
            strSQL &= " ,Transation_Date= (select top 1 withdraw_date from tb_withdraw     where withdraw_index='" & pstrWithdraw_index & "') "
            strSQL &= " where Transaction_Id = '" & _OldWithdraw_No & "' and process_id=2 "
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            'Dim oAudit_Log As New Sy_Audit_Log_Update
            'oAudit_Log.Document_Index = _objHeader.Withdraw_Index
            'oAudit_Log.Document_No = _objHeader.Withdraw_No
            ''oAudit_Log.Insert(Sy_Audit_Log_Update.Log_Type.Edit_WithDraw_Transaction)

            '
            '*** Commit transaction
            myTrans.Commit()
            Return _objHeader.Withdraw_Index
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region " DELETE "
    Public Sub Delete_OrderItem(ByVal OrderItem_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = "DELETE FROM tb_OrderItem "
            strSQL &= " WHERE OrderItem_Index ='" & OrderItem_Index & "' "

            connectDB()
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Sub

    Public Sub DeleteWithdraw(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal Withdraw_Index As String)
        Try
            Dim StrSql As String = ""
            StrSql = String.Format(" Delete tb_Withdraw where Withdraw_Index = '{0}' Delete tb_WithdrawItem where Withdraw_Index = '{0}' Delete tb_WithdrawItemLocation where Withdraw_Index = '{0}' ", Withdraw_Index)
            DBExeNonQuery(StrSql, Connection, myTrans, eCommandType.Text)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

#Region "Confirm Wihtdraw "
    Public Function Withdraw_Confirm(ByVal Withdraw_Index As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As String
        Try

            ' *** Check Balance : Item Of tb_WithdrawItem = Item Of tb_WithdrawItemLocation  ??? 
            Dim strchkWithDraw As String = Me.IsWithdrawItem_Bal(Withdraw_Index, Transaction)
            If strchkWithDraw = "PASS" Then
                ' *** Item not balance ***
                Return Me.SaveWithdrawConfirm(Withdraw_Index, Transaction)

            Else
                Return strchkWithDraw
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function Withdraw_Confirm(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand, ByVal Withdraw_Index As String) As String
        Try
            ' *** Check Balance : Item Of tb_WithdrawItem = Item Of tb_WithdrawItemLocation  ??? 
            Dim strchkWithDraw As String = Me.IsWithdrawItem_Bal(Withdraw_Index)
            If strchkWithDraw = "PASS" Then
                ' *** Item not balance ***
                Return Me.SaveWithdrawConfirm(Connection, myTrans, SQLServerCommand, Withdraw_Index)

            Else
                Return strchkWithDraw
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "GET ORDER HEADER & GET PRODUCT ITEM "
    Public Sub getOrderHeader(ByVal Order_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'ISNULL(Title, '')
            strSQL = " SELECT tb_Order.Order_Index, tb_Order.Order_No, tb_Order.Order_Date, tb_Order.Order_Time, tb_Order.Ref_No1, tb_Order.Ref_No2, tb_Order.Ref_No3, "
            strSQL &= "       tb_Order.Ref_No5, tb_Order.Ref_No4, tb_Order.Lot_No, tb_Order.Customer_Index, tb_Order.Supplier_Index, tb_Order.Department_Index, "
            strSQL &= "       tb_Order.DocumentType_Index, tb_Order.Status, tb_Order.Str1, tb_Order.Comment, tb_Order.Str2, tb_Order.Str3, tb_Order.Str4, tb_Order.Str5, "
            strSQL &= "       tb_Order.Str6, tb_Order.Str7, tb_Order.Str8, tb_Order.Str9, tb_Order.Str10, tb_Order.Flo1, tb_Order.Flo2, tb_Order.Flo3, tb_Order.Flo4, tb_Order.Flo5, "
            strSQL &= "       tb_Order.add_by, ms_Customer.Title, ms_Customer.Customer_Name, ms_DocumentType.Description AS DocumentType "
            strSQL &= " FROM  tb_Order INNER JOIN "
            strSQL &= "       ms_Customer ON tb_Order.Customer_Index = ms_Customer.Customer_Index INNER JOIN "
            strSQL &= "       ms_DocumentType ON tb_Order.DocumentType_Index = ms_DocumentType.DocumentType_Index LEFT OUTER JOIN "
            strSQL &= "       ms_Supplier ON tb_Order.Supplier_Index = ms_Supplier.Supplier_Index LEFT OUTER JOIN "
            strSQL &= "       ms_Department ON tb_Order.Department_Index = ms_Department.Department_Index "

            strWhere = " WHERE tb_Order.Order_Index ='" & Order_Index & "' "

            strSQL = strSQL & strWhere

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

    Public Sub getWithDrawIndex_Noconfrim()
        Dim strSQL As String = ""
        Try
            strSQL = " Select withDraw_Index from tb_withDraw where Status = 1"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception

        End Try
    End Sub
    Public Sub getWithdrawItemDetail(ByVal Withdraw_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "   SELECT   tb_WithdrawItem.WithdrawItem_Index, tb_WithdrawItem.Sku_Index,ms_SKU.Sku_Id , tb_WithdrawItem.PLot, "
            strSQL &= "          tb_WithdrawItem.ItemStatus_Index, tb_WithdrawItem.Package_Index, tb_WithdrawItem.Ratio,tb_WithdrawItem.Plan_Qty,tb_WithdrawItem.Plan_Total_Qty, tb_WithdrawItem.Qty , tb_WithdrawItem.Total_Qty,"
            strSQL &= "          tb_WithdrawItem.Weight, tb_WithdrawItem.Volume, tb_WithdrawItem.Serial_No,  "
            strSQL &= "          tb_WithdrawItem.Str1, tb_WithdrawItem.Str2, tb_WithdrawItem.Str3, tb_WithdrawItem.Str4,   "
            strSQL &= "          tb_WithdrawItem.Flo1, tb_WithdrawItem.Flo2, tb_WithdrawItem.Flo3, tb_WithdrawItem.Flo4, tb_WithdrawItem.Flo5,  "
            strSQL &= "          ms_Product.Product_Name_th,ms_SKU.Str1 AS SKU_Description, ms_SKURatio.Ratio AS msRatio,ms_ItemStatus.Description  as ItemStatus_Description, "
            strSQL &= "          ms_Package.Description AS PackageSKU, dbo.udf_Show_PackageItem(tb_WithdrawItem.Sku_Index, tb_WithdrawItem.Package_Index) AS PackageWithdraw ,"
            strSQL &= "          tb_WithdrawItem.Package_Index as Withdraw_Package_Index, ms_SKU.Package_Index as Sku_Package_Index "
            strSQL &= "  FROM    tb_WithdrawItem INNER JOIN"
            strSQL &= "          ms_SKU ON tb_WithdrawItem.Sku_Index = ms_SKU.Sku_Index INNER JOIN"
            strSQL &= "          ms_SKURatio ON ms_SKU.Sku_Index = ms_SKURatio.Sku_Index AND tb_WithdrawItem.Package_Index = ms_SKURatio.Package_Index INNER JOIN "
            strSQL &= "          ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index  INNER JOIN  "
            strSQL &= "          ms_Package ON ms_SKU.Package_Index=ms_Package.Package_Index LEFT JOIN "
            strSQL &= "          ms_ItemStatus ON  tb_WithdrawItem.ItemStatus_Index = ms_ItemStatus.ItemStatus_Index "

            strWhere = " WHERE tb_WithdrawItem.Withdraw_Index ='" & Withdraw_Index & "' "
            strSQL = strSQL & strWhere

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

    Public Sub getWithdrawItem_By_WithdrawItem_Index(ByVal WithdrawItem_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "   SELECT   tb_Withdraw.Customer_Index ,tb_WithdrawItem.WithdrawItem_Index, tb_WithdrawItem.Sku_Index,ms_SKU.Sku_Id , tb_WithdrawItem.PLot, "
            strSQL &= "          tb_WithdrawItem.ItemStatus_Index, tb_WithdrawItem.Package_Index, tb_WithdrawItem.Ratio,tb_WithdrawItem.Plan_Qty,tb_WithdrawItem.Plan_Total_Qty, tb_WithdrawItem.Qty , tb_WithdrawItem.Total_Qty,"
            strSQL &= "          tb_WithdrawItem.Weight, tb_WithdrawItem.Volume, tb_WithdrawItem.Serial_No,  "
            strSQL &= "          tb_WithdrawItem.Str1, tb_WithdrawItem.Str2, tb_WithdrawItem.Str3, tb_WithdrawItem.Str4,   "
            strSQL &= "          tb_WithdrawItem.Flo1, tb_WithdrawItem.Flo2, tb_WithdrawItem.Flo3, tb_WithdrawItem.Flo4, tb_WithdrawItem.Flo5,  "
            strSQL &= "          ms_Product.Product_Name_th,ms_SKU.Str1 AS SKU_Description, ms_SKURatio.Ratio AS msRatio,ms_ItemStatus.Description  as ItemStatus_Description, "
            strSQL &= "          ms_Package.Description AS PackageSKU, dbo.udf_Show_PackageItem(tb_WithdrawItem.Sku_Index, tb_WithdrawItem.Package_Index) AS PackageWithdraw ,"
            strSQL &= "          tb_WithdrawItem.Package_Index as Withdraw_Package_Index, ms_SKU.Package_Index as Sku_Package_Index "
            strSQL &= "  FROM    tb_WithdrawItem INNER JOIN "
            strSQL &= "           tb_Withdraw ON tb_WithdrawItem.Withdraw_Index = tb_Withdraw.Withdraw_Index  INNER JOIN "
            strSQL &= "          ms_SKU ON tb_WithdrawItem.Sku_Index = ms_SKU.Sku_Index INNER JOIN"
            strSQL &= "          ms_SKURatio ON ms_SKU.Sku_Index = ms_SKURatio.Sku_Index AND tb_WithdrawItem.Package_Index = ms_SKURatio.Package_Index INNER JOIN "
            strSQL &= "          ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index  INNER JOIN  "
            strSQL &= "          ms_Package ON ms_SKU.Package_Index=ms_Package.Package_Index LEFT JOIN "
            strSQL &= "          ms_ItemStatus ON  tb_WithdrawItem.ItemStatus_Index = ms_ItemStatus.ItemStatus_Index "

            strWhere = " WHERE tb_WithdrawItem.WithdrawItem_Index ='" & WithdrawItem_Index & "' "
            strSQL = strSQL & strWhere

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

    Public Function getQty_By_WithdrawItem_Index(ByVal Withdraw_Index As String) As Decimal

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim Total_Qty As Decimal = 0
        Dim DT1 As New DataTable

        Try


            strSQL = "SELECT Sum(Qty) as SumQty, WithdrawItem_Index "
            strSQL &= "  FROM tb_WithdrawItemLocation "
            strSQL &= " WHERE WithdrawItem_Index ='" & Withdraw_Index & "' "
            strSQL &= " GROUP BY WithdrawItem_Index "


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()

            ' *** Manage Value ***
            DT1.Clear()
            DT1 = GetDataTable
            If DT1.Rows.Count > 0 Then
                Return Val(DT1.Rows(0).Item("SumQty").ToString)
            Else
                Return 0
            End If
            ' ********************

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Function


    Public Sub getWithdrawItem_Unique(ByVal Withdraw_Index As String)
        '  *** Query for Jobwithdraw  to ComboBox Screen  ***
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            strSQL = "  SELECT    Distinct tb_WithdrawItem.WithdrawItem_Index, "
            strSQL &= "       ms_SKU.Str1 + '  '+"
            strSQL &= "  (CASE ISNULL(tb_WithdrawItem.PLot,'') "
            strSQL &= "   WHEN '' THEN ''  "
            strSQL &= " 	  ELSE  'Lot ผลิต :' +  tb_WithdrawItem.PLot END )+' ' + "

            strSQL &= " 	 (CASE ISNULL(ms_ItemStatus.Description,'')   "
            strSQL &= "   WHEN '' THEN '' "
            strSQL &= "   ELSE 'สถานะสินค้า  : ' + ms_ItemStatus.Description END) as Unique_Item ,"
            strSQL &= "          dbo.udf_Show_PackageItem(tb_WithdrawItem.Sku_Index, tb_WithdrawItem.Package_Index) AS PackageWithdraw"
            strSQL &= "         FROM         tb_WithdrawItem INNER JOIN"
            strSQL &= "        ms_SKU ON tb_WithdrawItem.Sku_Index = ms_SKU.Sku_Index INNER JOIN"
            strSQL &= "        ms_SKURatio ON ms_SKU.Sku_Index = ms_SKURatio.Sku_Index AND tb_WithdrawItem.Package_Index = ms_SKURatio.Package_Index INNER JOIN"
            strSQL &= "        ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index INNER JOIN"
            strSQL &= "        ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index LEFT OUTER JOIN"
            strSQL &= "          ms_ItemStatus ON tb_WithdrawItem.ItemStatus_Index = ms_ItemStatus.ItemStatus_Index"

            strWhere = " WHERE tb_WithdrawItem.Withdraw_Index ='" & Withdraw_Index & "' "
            strSQL = strSQL & strWhere

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

#Region "CANCEL"
#Region "Cancel Wihtdraw "
    Public Function Withdraw_Cancel(ByVal Withdraw_Index As String) As String
        Try



            Dim strchkWithDraw As String = Me.IsWithdrawItem_BalCancel(Withdraw_Index)
            If strchkWithDraw = "PASS" Then

                Me.Cancel_Withdraw(Withdraw_Index)

            Else
                Return strchkWithDraw
            End If

            Return "PASS"

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


    Public Function Cancel_Withdraw(ByVal Withdraw_Index As String) As String

        Dim strSQL As String = ""

        Dim Qty_Sku_Bal As Decimal = 0
        Dim Weight_Sku_Bal As Decimal = 0
        Dim Volume_Sku_Bal As Decimal = 0
        Dim Qty_PLot_Bal As Decimal = 0
        Dim Weight_PLot_Bal As Decimal = 0
        Dim Volume_PLot_Bal As Decimal = 0
        Dim Qty_ItemStatus_Bal As Decimal = 0
        Dim Weight_ItemStatus_Bal As Decimal = 0
        Dim Volume_ItemStatus_Bal As Decimal = 0


        Dim Qty_Sku_Location_Bal As Decimal = 0
        Dim Qty_ItemStatus_Location_Bal As Decimal = 0
        Dim Qty_PLot_Location_Bal As Decimal = 0

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction(IsolationLevel.Serializable)
        SQLServerCommand.Transaction = myTrans
        SQLServerCommand.CommandTimeout = 0
        Try
            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdraw set Withdraw_Index = '0010000000001' where Withdraw_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_Withdrawitem set WithdrawItem_Index = '0010000000001' where WithdrawItem_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update tb_SalesOrderItem set SalesOrderItem_Index = '0010000000001' where SalesOrderItem_Index = '0010000000001' ", Connection, myTrans)
            DBExeNonQuery(" update ms_Location set Location_Index = '0010000000001' where Location_Index = '0010000000001' ", Connection, myTrans)




            'ถ้าใบเบิกนั้น Complete แล้ว
            'Step 1 : UPDATE tb_LocationBalance คืน Balance
            'Step 2 : UPDATE tb_SalesOrderItem หรือ อื่น ๆ เพื่อคืนค่าเอกสารตั้งต้นเบิก
            'Step 3 : INSERT tb_Transaction เพื่อลงข้อมูล - 
            'Step 4 : UPDATE Ms_Location เพื่อคืนค่า Current
            strSQL = "   SELECT    *"
            strSQL &= "   FROM   VIEW_WithDrawCancel"
            strSQL &= "   WHERE Withdraw_Index ='" & Withdraw_Index & "'  AND Status=2 "


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

                    Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                    Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                    Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                    Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                    Dim Tag_No As String = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                    Dim TAG_Index As String = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                    Dim LocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString
                    Dim Withdraw_No As String = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                    Dim Reserve_No As String = DS.Tables("tbl").Rows(i).Item("SO_No").ToString
                    Dim Location_Index As String = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString





                    Dim Total_Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                    Dim Qty As Decimal = DS.Tables("tbl").Rows(i).Item("Qty")
                    Dim Weight As Decimal = DS.Tables("tbl").Rows(i).Item("Weight")
                    Dim Volume As Decimal = DS.Tables("tbl").Rows(i).Item("Volume")
                    Dim ItemQty As Decimal = DS.Tables("tbl").Rows(i).Item("Item_Qty")
                    Dim Price As Decimal = DS.Tables("tbl").Rows(i).Item("Price")

                    Dim ERP_Location As String = DS.Tables("tbl").Rows(i).Item("ERP_Location")

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(Connection, myTrans, PICKING.enmPicking_Action.ADDBALANCE, 2, Withdraw_Index, "ยกเลิกรายการที่ยืนยันแล้ว", LocationBalance_Index, Qty, Total_Qty, Weight, Volume, ItemQty, Price, _
                    Total_Qty, Weight, Volume, ItemQty, Price)
                    objPicking = Nothing




                  


                    ' 03-05-2010 case Cancel withdraw 
                    If Reserve_No <> "" Then

                        ' *** Set Status of Reserve ***
                        strSQL = "UPDATE tb_Reserve "
                        strSQL &= " SET Status =-1 "
                        strSQL &= " WHERE   Reserve_No ='" & Reserve_No & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                        ' *********************************

                    End If

                    '************* Plan_Process

                    Dim Plan_Process As String = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString
                    Dim DocumentPlan_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                    Dim DocumentPlanItem_Index As String = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString


                    If Plan_Process <> -9 Then


                        strSQL = ""
                        StatusWithdraw_Document = Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.SO, Withdraw_Document.Reservation
                                strSQL = "  UPDATE tb_SalesOrderItem "
                                strSQL &= " SET Qty_WithDraw = (Isnull(Total_Qty_Withdraw,0) / Isnull(Ratio,1)) - (@Total_Qty / Isnull(Ratio,1))  "
                                strSQL &= " , Total_Qty_Withdraw = Total_Qty_Withdraw - @Total_Qty "
                                strSQL &= " , Weight_Withdraw = Weight_Withdraw - @Weight "
                                strSQL &= " , Volume_Withdraw = Volume_Withdraw - @Volume "
                                strSQL &= " WHERE SalesOrderItem_Index= @DocumentPlanItem_Index "
                            Case Withdraw_Document.Packing
                                strSQL = "  UPDATE tb_PackingItem"
                                strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                                strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"
                            Case Withdraw_Document.Transport 'killz update 08-08-2011
                                strSQL = "  UPDATE tb_SalesOrderItem "
                                strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                                strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                                strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                                strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
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

                        End If

                        ' Update Status
                        UpdatePlanDocument_Status(DocumentPlan_Index, StatusWithdraw_Document, Connection, myTrans)
                    End If

                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,PalletType_Index,Pallet_Qty " _
                                                   & " FROM  tb_LocationBalance  " _
                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "'  "

                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans



                    '   DS = New DataSet
                    DataAdapter.Fill(DS, "tbl0")

                    If DS.Tables("tbl0").Rows.Count <> 0 Then
                        ' ***********************
                        Dim objCalBalance As New CalculateBalance
                        objCalBalance.setQty_Recieve_Package(Connection, myTrans, DS.Tables("tbl0").Rows(0).Item("LocationBalance_Index").ToString)
                        objCalBalance = Nothing
                        ' ***********************
                    End If
                    DS.Tables("tbl0").Clear()

                    ' ********************* Manage PalletType  ******************************

                    ' *** Need to Qty_Bal = 0 ***
                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,PalletType_Index,Pallet_Qty " _
                                                     & " FROM  tb_LocationBalance  " _
                                                     & " where LocationBalance_Index ='" & LocationBalance_Index & "'  AND Qty_Bal=0 "

                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans

                    '   DS = New DataSet
                    DataAdapter.Fill(DS, "tbl1")

                    If DS.Tables("tbl1").Rows.Count <> 0 Then

                        ' *** Set Status of Order ***
                        strSQL = "UPDATE tb_Order "
                        strSQL &= " SET Status =2 "
                        strSQL &= " WHERE   Order_Index ='" & DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString & "' "

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                        ' *********************************


                        If Val(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
                            ' *** update ms_PalletType *** 

                            strSQL = "UPDATE ms_PalletType "
                            strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
                            strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()




                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
                            Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value("PalletType_History_Index")
                            objDBPalletTypeIndex2 = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal2 As New CalculateBalance
                            Dim Qty_PalletType_Bal2 As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal2 = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal2 = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal2
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************

                            ' *** Insert Record in tb_PalletType_History ***

                            strSQL = " INSERT INTO tb_PalletType_History  "
                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


                            ' Generate PalletType_History_Index 

                            Dim objDBPalletTypeIndex As New Sy_AutoNumber
                            Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value("PalletType_History_Index")
                            objDBPalletTypeIndex = Nothing


                            ' *** Call Function Get Balance ***

                            'Dim objPalletTypeBal As New CalculateBalance
                            Dim Qty_PalletType_Bal As Decimal = 0
                            '' *** Qty ***
                            'Qty_PalletType_Bal = objPalletTypeBal.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
                            'objPalletTypeBal = Nothing

                            ' *********************************

                            With SQLServerCommand

                                .Parameters.Clear()

                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

                                ' *** Important Fix Code for Pallettype Location Default ***
                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
                                .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
                                '    .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                            End With

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' **********************************************


                        End If

                    End If

                    ' xxxxxxxxxxxxxxxxxxxx
                    DS.Tables("tbl1").Clear()
                    ' xxxxxxxxxxxxxxxxxxxx


                    ' *********************  End Manage PalletType  ******************************

                    ' *** Call Function Get Balance ***
                    Dim objBal As New CalculateBalance
                    ' *** Qty ***
                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Weight ***
                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
                    ' *** Volume ***
                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

                    Qty_Sku_Location_Bal = objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Location_Index)
                    Qty_PLot_Location_Bal = objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, Location_Index)
                    Qty_ItemStatus_Location_Bal = objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, Location_Index)


                    objBal = Nothing


                    ' *********************************
                    Dim strSQL_Qty_Item_Bal As String = ""
                    Dim strSQL_OrderItem_Price_Bal As String = ""
                    ' *** Insert tb_Transaction ***

                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Transaction_Index,Transaction_Id,Customer_Index,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Order_Index,Order_date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_Out,OrderItem_Price_Bal,ItemDefinition_Index,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,SO_No,DocumentType_Index,Serial_No,HandlingType_Index"
                    strSQL &= " ,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index,ERP_Location_From,ERP_Location_TO ) "
                    strSQL &= " VALUES "
                    strSQL &= " (@Transaction_Index,@Transaction_Id,@Customer_Index,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Order_Index,@Order_date,@OrderItem_Index,@Product_Index,@ProductType_Index,-@Qty_Item_Out," & strSQL_Qty_Item_Bal & "+@Qty_Item_Out,-@OrderItem_Price_Out," & strSQL_OrderItem_Price_Bal & "+@OrderItem_Price_Out,@ItemDefinition_Index,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@SO_No,@DocumentType_Index,@Serial_No,@HandlingType_Index "
                    strSQL &= "  ,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@ERP_Location_From,@ERP_Location_TO  ) "

                    strSQL_Qty_Item_Bal += " select sum(Qty_Item_In-Qty_Item_Out) as Qty from tb_Transaction"
                    strSQL_Qty_Item_Bal += " where OrderItem_Index = @OrderItem_Index"

                    strSQL_OrderItem_Price_Bal += " select sum(OrderItem_Price_In-OrderItem_Price_Out) as Price from tb_Transaction"
                    strSQL_OrderItem_Price_Bal += " where OrderItem_Index = @OrderItem_Index"



                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("TAG_Index").ToString
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("WithdrawItem_Index").ToString


                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                        '.Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = Now.Date.ToString("yyyy/MM/dd")
                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl").Rows(i).Item("WithDraw_Date").ToString).ToString("yyyy/MM/dd")
                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Total_Qty").ToString)
                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Weight").ToString)
                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Volume").ToString)
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Order_Index").ToString
                        .Parameters.Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Order_date").ToString
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString
                        .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString

                        ' Using Getdate() 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 2

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal
                        '####
                        .Parameters.Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = -DS.Tables("tbl").Rows(i).Item("Item_Qty")
                        .Parameters.Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = -DS.Tables("tbl").Rows(i).Item("Price")

                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = ""
                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Item_Package_Index").ToString

                        .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_In").ToString
                        .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Invoice_Out").ToString
                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("PO_No").ToString
                        .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("SO_No").ToString
                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Pallet_No").ToString
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl").Rows(i).Item("Declaration_No").ToString
                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("HandlingType_Index").ToString

                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Location_Bal
                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Location_Bal
                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Location_Bal


                        .Parameters.Add("@ERP_Location_From", SqlDbType.NVarChar, 100).Value = ERP_Location
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.NVarChar, 100).Value = ERP_Location


                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()



                    '********************** ASSET *************************
                    Dim _AssetLocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                    Dim _AssetTransaction_Index As String = ""
                    If _AssetLocationBalance_Index <> "" Then

                        strSQL = " SELECT       * "
                        strSQL &= " FROM       tb_AssetLocationBalance"
                        strSQL &= " WHERE       AssetLocationBalance_Index ='" & _AssetLocationBalance_Index & "' "

                        SetSQLString = strSQL
                        SetCommandType = enuCommandType.Text
                        SetEXEC_TYPE = EXEC.NonQuery
                        connectDB()
                        EXEC_Command()

                        Dim DSAS As New DataSet
                        DataAdapter.Fill(DSAS, "tblas")

                        If DSAS.Tables("tblas").Rows.Count <> 0 Then

                            For iAs As Integer = 0 To DSAS.Tables("tblas").Rows.Count - 1


                                Dim objDBAsIndex As New Sy_AutoNumber
                                _AssetTransaction_Index = objDBAsIndex.getSys_Value("AssetTransaction_Index")
                                objDBAsIndex = Nothing
                                'Insert tb_AssetTransaction
                                strSQL = "  INSERT INTO tb_AssetTransaction("
                                strSQL &= " AssetTransaction_Index, AssetLocationBalance_Index, Asset_No, Serial_No, Sku_Index, Order_Index, OrderItem_Index, Description, add_by, add_date, add_branch"
                                strSQL &= " ,AssetTransaction_Id, Lot_No, From_Location_Index, To_Location_Index, Tag_No, Plot, ItemStatus_Index, Transation_Date, Process_id"
                                strSQL &= " ,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal, Qty_PLot_Bal, Qty_ItemStatus_Bal, Weight_Sku_Bal, Weight_PLot_Bal, Weight_ItemStatus_Bal, Volume_Sku_Bal, Volume_PLot_Bal, Volume_ItemStatus_Bal,Qty_Item_Out,OrderItem_Price_Out"
                                strSQL &= " ,ItemDefinition_Index, Customer_Index, DocumentType_Index, Referent_1, Referent_2, Order_Date, ProductType_Index, Product_Index)"
                                strSQL &= "       VALUES("
                                strSQL &= " @AssetTransaction_Index,@AssetLocationBalance_Index,@Asset_No,@Serial_No,@Sku_Index,@Order_Index,@OrderItem_Index,@Description,@add_by,getdate(),@add_branch"
                                strSQL &= " ,@AssetTransaction_Id,@Lot_No,@From_Location_Index,@To_Location_Index,@Tag_No,@Plot,@ItemStatus_Index,@Transation_Date,@Process_id"
                                strSQL &= " ,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@Qty_Item_Out,@OrderItem_Price_Out"
                                strSQL &= " ,@ItemDefinition_Index,@Customer_Index,@DocumentType_Index,@Referent_1,@Referent_2,@Order_Date,@ProductType_Index,@Product_Index)"

                                With SQLServerCommand.Parameters
                                    .Clear()
                                    '-- from AssetLocation
                                    .Add("@AssetTransaction_Index", SqlDbType.VarChar, 13).Value = _AssetTransaction_Index
                                    .Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = _AssetLocationBalance_Index
                                    .Add("@Asset_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Asset_No") ' _Asset_No
                                    .Add("@Serial_No", SqlDbType.VarChar, 50).Value = DSAS.Tables("tblas").Rows(iAs).Item("Serial_No") '  _Serial_No
                                    .Add("@SKU_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("SKU_Index") '  _SKU_Index
                                    .Add("@Order_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("Order_Index") '  _Order_Index
                                    .Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DSAS.Tables("tblas").Rows(iAs).Item("OrderItem_Index") '  _OrderItem_Index
                                    .Add("@Description", SqlDbType.VarChar, 255).Value = ""

                                    .Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                                    .Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                                    '--- From WithDrawItem


                                    .Add("@AssetTransaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
                                    .Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
                                    .Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
                                    .Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
                                    .Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
                                    .Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
                                    '.Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")

                                    Dim _Order_date As DateTime = Format(DS.Tables("tbl").Rows(i).Item("Order_date"), "dd/MM/yyyy").ToString()

                                    .Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = Now.Date.ToString("yyyy/MM/dd")
                                    ' Process_id 
                                    .Add("@Process_id", SqlDbType.Float, 8).Value = 2

                                    .Add("@Qty_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Qty")) ' _Qty_In
                                    .Add("@Weight_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Weight"))
                                    .Add("@Volume_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Volume"))
                                    .Add("@Qty_Item_Out", SqlDbType.Float, 8).Value = -DS.Tables("tbl").Rows(i).Item("Item_Qty")
                                    .Add("@OrderItem_Price_Out", SqlDbType.Float, 8).Value = -DS.Tables("tbl").Rows(i).Item("Price")

                                    .Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = -Qty_Sku_Bal
                                    .Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = -Qty_PLot_Bal
                                    .Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = -Qty_ItemStatus_Bal

                                    .Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = -Weight_Sku_Bal
                                    .Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = -Weight_PLot_Bal
                                    .Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = -Weight_ItemStatus_Bal

                                    .Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = -Volume_Sku_Bal
                                    .Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = -Volume_PLot_Bal
                                    .Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = -Volume_ItemStatus_Bal

                                    .Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("ItemDefinition_Index").ToString
                                    .Add("@Customer_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
                                    .Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("DocumentType_Index").ToString
                                    .Add("@Referent_1", SqlDbType.VarChar, 100).Value = "" 'DS.Tables("tbl").Rows(i).Item("Reference1").ToString
                                    .Add("@Referent_2", SqlDbType.VarChar, 100).Value = "" 'DS.Tables("tbl").Rows(i).Item("Reference2").ToString

                                    .Add("@Order_date", SqlDbType.SmallDateTime, 4).Value = _Order_date
                                    .Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ProductType_Index").ToString
                                    .Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Product_Index").ToString




                                End With
                                SetSQLString = strSQL
                                SetCommandType = enuCommandType.Text
                                SetEXEC_TYPE = EXEC.NonQuery
                                connectDB()
                                EXEC_Command()



                                strSQL = "  UPDATE tb_AssetLocationBalance    "
                                strSQL &= "  SET Qty_Bal=Qty_Bal+@Total_Qty "
                                strSQL &= " ,Status = 1 "
                                strSQL &= "  WHERE AssetLocationBalance_Index=@AssetLocationBalance_Index "
                                With SQLServerCommand
                                    .Parameters.Clear()
                                    .Parameters.Add("@AssetLocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("AssetLocationBalance_Index").ToString
                                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
                                End With

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            Next
                        End If


                    End If


                    ' *** Manage ms_location ***

                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Current_Qty =Current_Qty+" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight+" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume+" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .ExecuteNonQuery()
                    End With

                    ' *** If Area of Location Emtry ***
                    strSQL = "UPDATE ms_Location "
                    strSQL &= " SET Space_Used =1 "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .ExecuteNonQuery()
                    End With
                    ' *********************************


                    ' *** End Manage ms_location ***

                Next

            End If
            'Update By Dong_kk  20/01/2011 ยกเลิกบางส่วนไม่คืนค่า Reserve
            'ถ้าใบเบิกนั้น Not Complete แล้ว
            'Step 1 : UPDATE tb_LocationBalance คืน Balance
            'Step 2 : UPDATE tb_SalesOrderItem หรือ อื่น ๆ เพื่อคืนค่าเอกสารตั้งต้นเบิก

            '************ Update By Dong_kk  21/01/2010 Case หยิบด้วย Mobile หยิบบางส่วน  *****************
            strSQL = " SELECT    tb_WithdrawItemLocation.*,tb_WithdrawItem.Plan_Process AS Plan_Process,tb_WithdrawItem.DocumentPlan_No AS DocumentPlan_No, "
            strSQL &= "          tb_WithdrawItem.DocumentPlan_Index AS DocumentPlan_Index,tb_WithdrawItem.DocumentPlanItem_Index AS DocumentPlanItem_Index,tb_WithdrawItem.AssetLocationBalance_Index AS AssetLocationBalance_Index"
            strSQL &= " FROM        tb_WithdrawItemLocation INNER JOIN"
            strSQL &= "          tb_WithdrawItem ON tb_WithdrawItemLocation.WithdrawItem_Index =tb_WithdrawItem.WithdrawItem_Index Inner join "
            strSQL &= "          tb_Withdraw ON tb_Withdraw.Withdraw_Index =tb_WithdrawItem.Withdraw_Index "
            strSQL &= " where tb_withdrawitemLocation.withdraw_Index ='" & Withdraw_Index & "'  AND tb_withdrawitemLocation.status not in (-1) AND tb_Withdraw.Status Not in (-1,2) "

            'strSQL &= " where tb_withdrawitemLocation.withdraw_Index ='" & Withdraw_Index & "'   AND tb_Withdraw.Status Not in (-1,2) "
            ''strSQL &= " where tb_withdrawitemLocation.withdraw_Index ='" & Withdraw_Index & "'  AND tb_withdrawitemLocation.status = -9 "

            SQLServerCommand.Connection = Connection
            SQLServerCommand.Transaction = myTrans
            SQLServerCommand.CommandText = strSQL
            SQLServerCommand.CommandTimeout = 0

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            DS = New DataSet
            DataAdapter.Fill(DS, "twr")
            If DS.Tables("twr").Rows.Count <> 0 Then
                Dim ii As Integer = 0
                For ii = 0 To DS.Tables("twr").Rows.Count - 1
                    Dim Plan_Process As String = DS.Tables("twr").Rows(ii).Item("Plan_Process").ToString
                    Dim DocumentPlanItem_Index As String = DS.Tables("twr").Rows(ii).Item("DocumentPlanItem_Index").ToString
                    StatusWithdraw_Document = Plan_Process
                    strSQL = ""

                    '*************** ReNew Qty In ITEM ***************
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.SO, Withdraw_Document.Reservation
                            'strSQL = "  UPDATE tb_SalesOrderItem "
                            'strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                            'strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                            'strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                            'strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET Qty_WithDraw = (Isnull(Total_Qty_Withdraw,0) / Isnull(Ratio,1)) - (@Total_Qty / Isnull(Ratio,1))  "
                            strSQL &= " , Total_Qty_Withdraw = Total_Qty_Withdraw - @Total_Qty "
                            strSQL &= " , Weight_Withdraw = Weight_Withdraw - @Weight "
                            strSQL &= " , Volume_Withdraw = Volume_Withdraw - @Volume "
                            strSQL &= " WHERE SalesOrderItem_Index= @DocumentPlanItem_Index "

                        Case Withdraw_Document.Packing
                            strSQL = "  UPDATE tb_PackingItem"
                            strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                            strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"
                        Case Withdraw_Document.Reserve
                            'ไม่มีการ Renew Qty เพราะไม่ได้มีการเบิกบางส่วน

                        Case Withdraw_Document.Transport
                            strSQL = "  UPDATE tb_SalesOrderItem "
                            strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                            strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                            strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                            strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "

                            strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "
                    End Select

                    If strSQL <> "" Then
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("twr").Rows(ii).Item("DocumentPlanItem_Index").ToString
                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = DS.Tables("twr").Rows(ii).Item("DocumentPlan_Index").ToString
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Qty")
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Total_Qty")
                            .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Item_Qty")
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Weight")
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Volume")

                        End With
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If

                    '*************** Update Status Header check Renew WithDraw ***************
                    strSQL = ""

                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.Reserve
                            strSQL = " UPDATE tb_Reserve"
                            strSQL &= " SET status =2 "
                            strSQL &= " WHERE Reserve_Index =@DocumentPlan_Index"

                        Case Withdraw_Document.SO, Withdraw_Document.Reservation
                            strSQL = "  UPDATE tb_SalesOrder "
                            strSQL &= " SET Status =2 "
                            strSQL &= " WHERE SalesOrder_Index=@DocumentPlan_Index "
                            strSQL &= " AND  (select sum(Qty_WithDraw) from tb_SalesOrderItem where SalesOrder_Index=@DocumentPlan_Index )=0"

                            Dim objSalesOrder As New WMS_STD_OUTB_SO_Datalayer.tb_SalesOrder
                            objSalesOrder.IsCreateWithdraw_Clear(Connection, myTrans, DS.Tables("twr").Rows(ii).Item("DocumentPlan_Index").ToString)

                        Case Withdraw_Document.Packing
                            strSQL = "  UPDATE tb_Packing"
                            strSQL &= " SET  Status =2 "
                            strSQL &= " WHERE Packing_Index =@DocumentPlan_Index"
                            strSQL &= " AND  (select sum(Qty_WithDraw) from tb_PackingItem where Packing_Index=@DocumentPlan_Index )=0"
                        Case Withdraw_Document.Transport ' killz Update 08-08-2011
                            strSQL = "  UPDATE tb_SalesOrder "
                            strSQL &= " SET Status =2 "
                            strSQL &= " WHERE SalesOrder_Index=@DocumentPlan_Index "
                            strSQL &= " AND  (select sum(Qty_WithDraw) from tb_SalesOrderItem where SalesOrder_Index=@DocumentPlan_Index )=0"
                    End Select
                    If strSQL <> "" Then
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = DS.Tables("twr").Rows(ii).Item("DocumentPlan_Index").ToString
                        End With
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If

                    '*************** Renew Balance Reserve ***************
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.Reserve
                            ' ไม่คืนการจองสินค้า เพระโดนจองโดย Reserve อยู่แล้ว ที่ต้องทำคือคืน Status ให้ Reserve Doc.
                        Case Else

                            Dim Total_Qty As Decimal = DS.Tables("twr").Rows(ii).Item("Total_Qty")
                            Dim Qty As Decimal = DS.Tables("twr").Rows(ii).Item("Weight")
                            Dim Weight As Decimal = DS.Tables("twr").Rows(ii).Item("Weight")
                            Dim Volume As Decimal = DS.Tables("twr").Rows(ii).Item("Volume")
                            Dim ItemQty As Decimal = DS.Tables("twr").Rows(ii).Item("Item_Qty")
                            Dim Price As Decimal = DS.Tables("twr").Rows(ii).Item("Price")
                            Dim LocationBalance_Index As String = DS.Tables("twr").Rows(ii).Item("LocationBalance_Index").ToString

                            Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                            objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 2, Withdraw_Index, "ยกเลิกรายการที่ไม่ได้ยืนยัน", LocationBalance_Index, 0, 0, 0, 0, 0, 0, _
                            Total_Qty, Weight, Volume, ItemQty, Price)
                            objPicking = Nothing


                            'With SQLServerCommand
                            '    .Parameters.Clear()
                            '    .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 50).Value = DS.Tables("twr").Rows(ii).Item("LocationBalance_Index").ToString
                            '    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Total_Qty")
                            '    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Weight")
                            '    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Volume")
                            '    .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Item_Qty")
                            '    .Parameters.Add("@Price", SqlDbType.Float, 8).Value = DS.Tables("twr").Rows(ii).Item("Price")
                            'End With


                            'strSQL = "  UPDATE tb_LocationBalance    "
                            'strSQL &= " SET ReserveQty= ReserveQty -@Total_Qty   "
                            'strSQL &= " ,ReserveWeight = ReserveWeight - @Weight "
                            'strSQL &= " ,ReserveVolume = ReserveVolume - @Volume  "
                            'strSQL &= " ,ReserveQty_Item = ReserveQty_Item - @Item_Qty  "
                            'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price - @Price "
                            'strSQL &= " WHERE LocationBalance_Index=@LocationBalance_Index "

                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()


                    End Select



                Next
            End If


            '************ END Update By Dong_kk  21/01/2010 *****************


            '#############  WitdrawReserve ############# 28/10/2008

            strSQL = "    UPDATE OrderItemSerial   " & _
                     "    Set OrderItemSerial.Status = 1  " & _
                     "    From tb_OrderItemSerial as OrderItemSerial " & _
                     "    Inner join tb_WithdrawItemSerial On (OrderItemSerial.SerialNumber = tb_WithdrawItemSerial.SerialNumber)  " & _
                     "    And (OrderItemSerial.Sku_Index = tb_WithdrawItemSerial.Sku_Index) " & _
                     "    And (OrderItemSerial.OrderItem_Index = tb_WithdrawItemSerial.OrderItem_Index) " & _
                     "    where tb_WithdrawItemSerial.Withdraw_Index = '" & Withdraw_Index & "' "

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With

            strSQL = " Update tb_WithdrawItemSerial set Status = -1 where Withdraw_Index = '" & Withdraw_Index & "' "
            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With


            ' *** Update tb_withdraw & tb_jobWithdraw ***
            strSQL = "UPDATE tb_Withdraw "
            strSQL &= " SET status =-1  ,cancel_date=getdate(),cancel_by='" & WV_UserName & "',cancel_branch='" & WV_Branch_ID & "' ,User_UseDoc = 0 "
            strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With
            strSQL = "     SELECT * "
            strSQL &= "     FROM tb_WithDraw "
            strSQL &= "     WHERE Withdraw_Index ='" & Withdraw_Index & "' "
            With SQLServerCommand

                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0

            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            DS = New DataSet
            DataAdapter.Fill(DS, "tbl_WithDraw")

            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = Withdraw_Index
            oAudit_Log.Document_No = DS.Tables("tbl_WithDraw").Rows(0).Item("WithDraw_No").ToString
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_Picking)
            DS.Tables("tbl_WithDraw").Clear()


            strSQL = "UPDATE tb_WithdrawItem "
            strSQL &= " SET status =-1 "
            strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With

            strSQL = "UPDATE tb_JobWithdraw "
            strSQL &= " SET status =-1 "
            strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With

            strSQL = "UPDATE tb_WithdrawItemLocation "
            strSQL &= " SET status =-1 "
            strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With

            ' *** Update tb_withdraw & tb_jobWithdraw ***


            '*** Commit transaction
            myTrans.Commit()

            'Return True
            Return "PASS"

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
            Return ex.Message.ToString
        Finally
            disconnectDB()
        End Try

    End Function
#End Region
    Private Function IsWithdrawItem_BalCancel(ByVal Withdraw_Index As String) As String
        Dim strSQL As String = ""

        'connectDB()

        Try
            'เช็คว่าใบเบิกนั้นสามารถทำการยกเลิกได้หรือไม่
            If isNotCancelWithDraw(Withdraw_Index) Then
                Return GetMessage_Data("500014") '"ใบเบิกนี้อยู่ในสถานะไม่สามารถยกเลิกได้"
            End If





            Return "PASS"
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try



    End Function
    Private Function isTotalQty_NotCancelWithDraw(ByVal pstrWithdraw_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "  declare @Withdraw_Index Varchar(50)"
            strSQL &= "  SET  @Withdraw_Index  = '" & pstrWithdraw_Index & "'"
            strSQL &= "  SELECT isnull(SUM(WI.Total_Qty),0) AS WI_Total_Qty , isnull(SUM(WIL.Total_Qty),0) AS WIL_Total_Qty"
            strSQL &= " FROM tb_WithdrawItem WI inner join"
            strSQL &= "         tb_WithDrawItemLocation WIL ON WI.WithDrawItem_Index =  WIL.WithDrawItem_Index "
            strSQL &= " WHERE WI.Withdraw_Index=@Withdraw_Index"
            'SQLServerCommand.Parameters.Clear()
            'SQLServerCommand.Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = pstrWithdraw_Index
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            If _dataTable.Rows.Count = 0 Then Return True
            If CDec(_dataTable.Rows(0).Item("WI_Total_Qty")) = (_dataTable.Rows(0).Item("WIL_Total_Qty")) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function isNotCancelWithDraw(ByVal pstrWithdraw_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "  SELECT  count(*) "
            strSQL &= " FROM    tb_Withdraw "
            strSQL &= " WHERE   status in (-1,0)"
            strSQL &= "     AND  Withdraw_Index=@Withdraw_Index"
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = pstrWithdraw_Index
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case 0
                    Return False
                Case Else
                    Return True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Private Function isExistID(ByVal WithdrawItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_WithdrawItem where WithdrawItem_Index = @WithdrawItem_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@WithdrawItem_Index", SqlDbType.VarChar, 13).Value = WithdrawItem_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Private Function isExistID_WIL(ByVal pWithdrawItemLocation_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_WithdrawItemLocation where WithdrawItemLocation_Index = @WithdrawItemLocation_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@WithdrawItemLocation_Index", SqlDbType.VarChar, 13).Value = pWithdrawItemLocation_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Function
    Private Function isExistIDForPalletType(ByVal pstrPalletType_History_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_PalletType_History WHERE PalletType_History_Index ='" & pstrPalletType_History_Index & "'" '@Order_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = pstrPalletType_History_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function isNotConfrimWithDraw(ByVal pstrWithdraw_Index As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As Boolean
        Dim SQL As String
        Try
            SQL = "  SELECT  count(*) "
            SQL &= " FROM    tb_Withdraw "
            SQL &= " WHERE   status in (-1,0,2)"
            SQL &= " AND  Withdraw_Index = @Withdraw_Index"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Withdraw_Index", SqlDbType.VarChar).Value = pstrWithdraw_Index
            End With
            
            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            'connectDB()
            'EXEC_Command()
            If Transaction Is Nothing Then
                _scalarOutput = DBExeQuery_Scalar(SQL)
            Else
                _scalarOutput = DBExeQuery_Scalar(SQL, Transaction.Connection, Transaction)
            End If


            Select Case _scalarOutput
                Case Nothing
                    Return False
                Case 0
                    Return False
                Case Else
                    Return True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function isTotalQty_NotConfrimWithDraw(ByVal pstrWithdraw_Index As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As Boolean
        Dim SQL As String
        Try
            SQL = " SELECT isnull(SUM(WI.Total_Qty),0) AS WI_Total_Qty , isnull(SUM(WIL.Total_Qty),0) AS WIL_Total_Qty"
            SQL &= " FROM tb_WithdrawItem WI inner join"
            SQL &= "         tb_WithDrawItemLocation WIL ON WI.WithDrawItem_Index =  WIL.WithDrawItem_Index "
            SQL &= " WHERE WI.Withdraw_Index = @Withdraw_Index"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Withdraw_Index", SqlDbType.VarChar).Value = pstrWithdraw_Index
            End With

            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()
            '_dataTable = GetDataTable

            If Transaction Is Nothing Then
                _dataTable = DBExeQuery(SQL)
            Else
                _dataTable = DBExeQuery(SQL, Transaction.Connection, Transaction)
            End If

            If _dataTable.Rows.Count = 0 Then Return True
            If CDec(_dataTable.Rows(0).Item("WI_Total_Qty")) = (_dataTable.Rows(0).Item("WIL_Total_Qty")) Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function isNotRowConfrimWithDraw_Item(ByVal pstrWithdraw_Index As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As Boolean
        Dim SQL As String
        Try
            SQL = "  SELECT  count(*) "
            SQL &= " FROM    tb_WithdrawItem "
            SQL &= " WHERE  Withdraw_Index = @Withdraw_Index"

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Withdraw_Index", SqlDbType.VarChar, 13).Value = pstrWithdraw_Index
            End With
            
            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            'connectDB()
            'EXEC_Command()
            '_scalarOutput = GetScalarOutput

            If Transaction Is Nothing Then
                _scalarOutput = DBExeQuery_Scalar(SQL)
            Else
                _scalarOutput = DBExeQuery_Scalar(SQL, Transaction.Connection, Transaction)
            End If

            Select Case _scalarOutput
                Case Nothing
                    Return True
                Case 0
                    Return True
                Case Else
                    Return False
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function IsWithdrawItem_Bal(ByVal Withdraw_Index As String, Optional ByVal Transaction As SqlClient.SqlTransaction = Nothing) As String
        Dim strSQL As String = ""

        'connectDB()

        Try
            'เช็คว่าใบเบิกนั้นสามารถทำการเบิกได้หรือไม่
            If isNotConfrimWithDraw(Withdraw_Index, Transaction) Then
                Return GetMessage_Data("500009") '"ใบเบิกนี้อยู่ในสถานะไม่สามารถยืนยันได้"
            End If
            'เช็คว่าจำนวนที่เบิกกับจำนวนหยิบเท่ากันหรือไม่
            If isTotalQty_NotConfrimWithDraw(Withdraw_Index, Transaction) Then
                Return GetMessage_Data("500011")
            End If
            'เช็คว่าในใบเบิกนั้นมีรายการสินค้าที่ทำการเบิกหรือไม่
            If isNotRowConfrimWithDraw_Item(Withdraw_Index, Transaction) Then
                Return GetMessage_Data("500015")
            End If


            'strSQL = "  SELECT  * "
            'strSQL &= " FROM  tb_WithdrawItem "
            'strSQL &= " WHERE Withdraw_Index='" & Withdraw_Index & "'"

            'With SQLServerCommand
            '    .Connection = Connection
            '    .CommandText = strSQL
            'End With

            'DataAdapter.SelectCommand = SQLServerCommand

            'DataAdapter.Fill(DS, "tb_WithdrawItem")

            'If DS.Tables("tb_WithdrawItem").Rows.Count <> 0 Then
            '    Dim iWithdrawItem_Index As String = DS.Tables("tb_WithdrawItem").Rows(0).Item("WithdrawItem_Index").ToString
            '    Dim iWithdraw_Index As String = DS.Tables("tb_WithdrawItem").Rows(0).Item("Withdraw_Index").ToString
            '    Dim iSku_Index As String = DS.Tables("tb_WithdrawItem").Rows(0).Item("Sku_Index").ToString
            '    Dim iPLot As String = DS.Tables("tb_WithdrawItem").Rows(0).Item("PLot").ToString
            '    Dim iItemStatus_Index As String = DS.Tables("tb_WithdrawItem").Rows(0).Item("ItemStatus_Index").ToString
            '    Dim iTotal_Qty As decimal = Val(DS.Tables("tb_WithdrawItem").Rows(0).Item("Total_Qty").ToString)
            '    Dim iWeight As decimal = Val(DS.Tables("tb_WithdrawItem").Rows(0).Item("Weight").ToString)
            '    Dim iVolume As decimal = Val(DS.Tables("tb_WithdrawItem").Rows(0).Item("Volume").ToString)

            '    Dim iItemQty As decimal = Val(DS.Tables("tb_WithdrawItem").Rows(0).Item("Item_Qty").ToString)
            '    Dim iPlanQty As decimal = Val(DS.Tables("tb_WithdrawItem").Rows(0).Item("Plan_Qty").ToString)



            '    For i As Integer = 0 To DS.Tables("tb_WithdrawItem").Rows.Count - 1
            '        ' *** check Total_Qty ***
            '        ' *** check Weithd ***
            '        ' *** check Volume ***
            '        If Me.isBalanceOfWithdrawItem(iWithdrawItem_Index, iTotal_Qty, iWeight, iVolume) = False Then
            '            ' *** WithdrawItem not Balance ***
            '            Return False

            '        End If
            '    Next

            '    ' *** Can Withdraw product *** 
            '    Return True
            '    ' ****************************
            'Else
            '    MessageBox.Show("ไม่มีรายการเบิกสินค้า", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Return False

            'End If

            Return "PASS"
        Catch ex As Exception
            Throw ex
        End Try



    End Function
    Public Function isCanWithdraw(ByVal Customer_Index As String, ByVal Sku_Index As String, ByVal PLot As String, ByVal ItemStatus_Index As String, ByVal Total_Qty As Decimal, ByVal Weight As Decimal, ByVal Volume As Decimal) As Boolean
        ' *** Check  Current Quantity of Withdraw with tb_LocationBalance + Pre Quantity + WithDraw_Number *** 
        Dim strSQL As String = ""
        Dim sqlWhere As String = ""
        Dim sumQty_Bal As Decimal = 0
        Dim sumWeight_Bal As Decimal = 0
        Dim sumVolume_Bal As Decimal = 0

        Try
            strSQL = "   SELECT  Sum(LB.Qty_Bal)  as sumQty_Bal ,Sum(LB.Weight_Bal)  as sumWeight_Bal ,Sum(LB.Volume_Bal)  as sumVolume_Bal "
            strSQL &= "  FROM   tb_LocationBalance LB  INNER JOIN   "
            strSQL &= "         tb_Order O ON  LB.Order_Index=O.Order_Index "
            sqlWhere = " WHERE  O.Customer_Index ='" & Customer_Index & "' AND LB.Sku_Index='" & Sku_Index & "' AND LB.Plot='" & PLot & " ' AND LB.ItemStatus_Index ='" & ItemStatus_Index & "'"

            strSQL = strSQL & sqlWhere
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            If _dataTable.Rows.Count > 0 Then
                sumQty_Bal = Val(_dataTable.Rows(0).Item("sumQty_Bal").ToString)
                sumWeight_Bal = Val(_dataTable.Rows(0).Item("sumWeight_Bal").ToString)
                sumVolume_Bal = Val(_dataTable.Rows(0).Item("sumVolume_Bal").ToString)

                If sumQty_Bal <= 0 Then
                    'MessageBox.Show("ไม่มีสินค้าในคลังตามรายการเบิก", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False

                End If

                If Total_Qty <= sumQty_Bal Then
                    Return True
                Else
                    'MessageBox.Show("จำนวนสินค้าที่เบิกมีมากกว่าสินค้าในคลัง", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False

                End If

                'If Weight <= sumWeight_Bal Then
                '    Return True
                'Else
                '    MessageBox.Show("น้ำหนักสินค้าที่เบิกมีมากกว่าสินค้าในคลัง", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return False
                '    Exit Function
                'End If

                'If Volume = sumVolume_Bal Then
                '    Return True
                'Else
                '    MessageBox.Show("ปริมาตรสินค้าที่เบิกมีมากกว่าสินค้าในคลัง", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return False
                '    Exit Function
                'End If
            Else
                'MessageBox.Show("ไม่มีสินค้าในคลังตามรายการเบิก", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False
                'Exit Function
            End If

        Catch ex As Exception
            Throw ex
        End Try
        'Return False
    End Function
    Public Function isItemLocation(ByVal OrderItem_Index As String) As Boolean
        'Dim strSQL As String
        'Try
        '    strSQL = " select count(*) from tb_OrderItemLocation where OrderItem_Index = @OrderItem_Index  "

        '    SQLServerCommand.Parameters.Clear()
        '    SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index

        '    SetSQLString = strSQL
        '    SetCommandType = DBType_SQLServer.enuCommandType.Text
        '    SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
        '    connectDB()
        '    EXEC_Command()
        '    _scalarOutput = GetScalarOutput

        '    If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
        '        Return False
        '    Else
        '        Return True
        '    End If
        'Catch ex As Exception
        '    Throw ex
        '    '  Finally
        '    '      disconnectDB()
        'End Try
    End Function
    Private Function isBalanceOfWithdrawItem(ByVal WithdrawItem_Index As String, ByVal Total_Qty As Decimal, ByVal Weight As Decimal, ByVal Volume As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim sumTotal_Qty As Decimal = 0
        Dim sumWeight As Decimal = 0
        Dim sumVolume As Decimal = 0
        Try
            strSQL = " SELECT SUM(Total_Qty) as sumTotal_Qty ,SUM(Weight) as sumWeight,SUM(Volume) as sumVolume ,WithdrawItem_Index "
            strSQL &= " FROM tb_WithdrawItemLocation  "
            strSQL &= " WHERE WithdrawItem_Index ='" & WithdrawItem_Index & "'"
            strSQL &= " GROUP BY WithdrawItem_Index "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            If _dataTable.Rows.Count > 0 Then
                sumTotal_Qty = Val(_dataTable.Rows(0).Item("sumTotal_Qty").ToString)
                sumWeight = Val(_dataTable.Rows(0).Item("sumWeight").ToString)
                sumVolume = Val(_dataTable.Rows(0).Item("sumVolume").ToString)

                If Total_Qty = sumTotal_Qty Then
                    Return True
                Else
                    ' MessageBox.Show("จำนวนสินค้าในใบเบิกไม่เท่ากับใบสั่งงานนำสินค้าออก", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Return False

                End If

                'If Weight = sumWeight Then
                '    Return True
                'Else
                '    ' MessageBox.Show("น้ำหนักสินค้าในใบเบิกไม่เท่ากับใบสั่งงานนำสินค้าออก", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return False
                '    Exit Function
                'End If

                'If Volume = sumVolume Then
                '    Return True
                'Else
                '    ' MessageBox.Show("ปริมาตรสินค้าในใบเบิกไม่เท่ากับใบสั่งงานนำสินค้าออก", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Return False
                '    Exit Function
                'End If
            Else
                ' MessageBox.Show("ยังไม่มีการนำสินค้าออกจากคลัง", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return False

            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function



    Public Function GetConfig_Key(ByVal pConfig_Key As String) As String
        Dim strSQL As String = ""

        Try
            strSQL = "SELECT Config_Value FROM config_PrintPalletSlip WHERE Config_Key = '" & pConfig_Key & "' "
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput


            Return _scalarOutput
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub UpdatePlanDocument_Status(ByVal PlanDocument_Index As String, ByVal Process_Id As Withdraw_Document, ByVal myTrans As SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        Try
            Select Case Process_Id
                Case Withdraw_Document.SO, Withdraw_Document.Reservation
                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, Connection, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, Connection, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                            'สเสร็จสิ้น Fix ตอนยืนยันเอา
                            'ElseIf dblQty_WithDraw >= dblTotal_Qty Then
                            '    strSQL = "UPDATE tb_SalesOrder "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                            '    ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                            '    strSQL = "UPDATE tb_SalesOrderItem "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()

                            'No Action รอส่งเมื่อ Confrim เท่านั้น

                        End If
                    End If
                Case Withdraw_Document.Packing
                    strSQL = " SELECT isnull(SUM(Qty_Withdraw),0) as Qty_Withdraw ,isnull(SUM(Qty),0) as Total_Qty"
                    strSQL &= " FROM tb_PackingItem  "
                    strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSPACK As New DataSet
                    DataAdapter.Fill(DSPACK, "tbpack")
                    If DSPACK.Tables("tbpack").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSPACK.Tables("tbpack").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSPACK.Tables("tbpack").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, Connection, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                        ElseIf dblQty_WithDraw < dblTotal_Qty Then
                            '--- สถานะ เบิกบางส่วน
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, Connection, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                            'ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '    '--- สถานะ รอรับสินค้า
                            '    strSQL = "UPDATE tb_SalesOrder "
                            '    strSQL &= " SET status =5 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                            '    ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                            '    strSQL = "UPDATE tb_SalesOrderItem "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                        End If

                    End If

                Case Withdraw_Document.Transport 'killz update 08-08-2011

                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, Connection, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, Connection, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                        End If

                    End If
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub UpdatePlanDocument_Status(ByVal PlanDocument_Index As String, ByVal Process_Id As Withdraw_Document, ByVal pConnect As SqlClient.SqlConnection, ByVal myTrans As System.Data.SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        Try
            Select Case Process_Id
                Case Withdraw_Document.SO, Withdraw_Document.Reservation
                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = pConnect
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                            'สเสร็จสิ้น Fix ตอนยืนยันเอา
                            'ElseIf dblQty_WithDraw >= dblTotal_Qty Then
                            '    strSQL = "UPDATE tb_SalesOrder "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                            '    ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                            '    strSQL = "UPDATE tb_SalesOrderItem "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()

                            'No Action รอส่งเมื่อ Confrim เท่านั้น

                        End If
                    End If
                Case Withdraw_Document.Packing
                    strSQL = " SELECT isnull(SUM(Qty_Withdraw),0) as Qty_Withdraw ,isnull(SUM(Qty),0) as Total_Qty"
                    strSQL &= " FROM tb_PackingItem  "
                    strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = pConnect
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSPACK As New DataSet
                    DataAdapter.Fill(DSPACK, "tbpack")
                    If DSPACK.Tables("tbpack").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSPACK.Tables("tbpack").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSPACK.Tables("tbpack").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                        ElseIf dblQty_WithDraw < dblTotal_Qty Then
                            '--- สถานะ เบิกบางส่วน
                            strSQL = " UPDATE  tb_Packing"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE Packing_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()
                            'ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '    '--- สถานะ รอรับสินค้า
                            '    strSQL = "UPDATE tb_SalesOrder "
                            '    strSQL &= " SET status =5 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                            '    ' --- STEP 2: Update status in tb_SaleOrderItem = 3
                            '    strSQL = "UPDATE tb_SalesOrderItem "
                            '    strSQL &= " SET status =3 "
                            '    strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"

                            '    SetSQLString = strSQL
                            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
                            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            '    EXEC_Command()
                        End If

                    End If

                Case Withdraw_Document.Transport 'killz update 08-08-2011

                    strSQL = " SELECT SUM(Qty_Withdraw) as Qty_Withdraw ,SUM(Total_Qty) as Total_Qty"
                    strSQL &= " FROM tb_SalesOrderItem  "
                    strSQL &= " WHERE SalesOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = PlanDocument_Index
                    With SQLServerCommand
                        .Connection = pConnect
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With

                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "tbso")

                    If DSSO.Tables("tbso").Rows.Count <> 0 Then
                        dblQty_WithDraw = Val(DSSO.Tables("tbso").Rows(0).Item("Qty_Withdraw").ToString)
                        dblTotal_Qty = Val(DSSO.Tables("tbso").Rows(0).Item("Total_Qty").ToString)
                        If dblQty_WithDraw = 0 Then
                            '--- สถานะ รอเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 2  "
                            strSQL &= " WHERE SalesOrder_Index ='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                        ElseIf dblQty_WithDraw <= dblTotal_Qty Then
                            '--- สถานะ ค้างเบิก
                            strSQL = " UPDATE  tb_SalesOrder"
                            strSQL &= " SET Status = 6  "
                            strSQL &= " WHERE SalesOrder_Index='" & PlanDocument_Index & "'"
                            DBExeNonQuery(strSQL, pConnect, myTrans)
                            'SetSQLString = strSQL
                            'SetCommandType = DBType_SQLServer.enuCommandType.Text
                            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            'EXEC_Command()

                        End If

                    End If
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub UpdatePlanDocument_Reserve(ByVal DocumentPlan_Index As String, ByVal DocumentPlanItem_Index As String, ByVal Qty As Decimal, ByVal Total_Qty As Decimal, ByVal ItemQty As Decimal, ByVal Weight As Decimal, ByVal Volume As Decimal, ByVal Process_Id As Integer)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try


            Select Case Process_Id
                Case Withdraw_Document.SO, Withdraw_Document.Reservation
                    strSQL = "  UPDATE tb_SalesOrderItem "
                    strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                    strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                    strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                    strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                    strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "

                Case Withdraw_Document.Packing
                    strSQL = "  UPDATE tb_PackingItem"
                    strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                    strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"

                Case Withdraw_Document.Transport 'killz upadte 08-08-2011
                    strSQL = "  UPDATE tb_SalesOrderItem "
                    strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                    strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                    strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                    strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                    strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "


            End Select
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = DocumentPlanItem_Index
                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = Qty
                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = Total_Qty
                .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = ItemQty
                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = Weight
                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = Volume

            End With

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()

            DBExeNonQuery(strSQL)

            ' Update Status
            UpdatePlanDocument_Status(DocumentPlan_Index, Process_Id, myTrans)

            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub UpdatePlanDocument_Reserve(ByVal DocumentPlan_Index As String, ByVal DocumentPlanItem_Index As String, ByVal Qty As Decimal, ByVal Total_Qty As Decimal, ByVal ItemQty As Decimal, ByVal Weight As Decimal, ByVal Volume As Decimal, ByVal Process_Id As Integer, ByVal pConnection As SqlClient.SqlConnection, ByVal pMytrans As SqlClient.SqlTransaction)
        Dim strSQL As String = ""
        Dim dblQty_WithDraw As Decimal = 0
        Dim dblTotal_Qty As Decimal = 0
        'connectDB()
        'Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        'SQLServerCommand.Transaction = myTrans

        Try


            Select Case Process_Id
                Case Withdraw_Document.SO, Withdraw_Document.Reservation
                    strSQL = "  UPDATE tb_SalesOrderItem "
                    strSQL &= " SET Qty_WithDraw = (Isnull(Total_Qty_Withdraw,0) / Isnull(Ratio,1)) - (@Total_Qty / Isnull(Ratio,1)) " '"Qty_WithDraw - (@Total_Qty / Ratio) "
                    strSQL &= " , Total_Qty_Withdraw =Total_Qty_Withdraw-@Total_Qty "
                    strSQL &= " , Weight_Withdraw =Weight_Withdraw-@Weight "
                    strSQL &= " , Volume_Withdraw =Volume_Withdraw-@Volume "
                    strSQL &= " WHERE SalesOrderItem_Index=@DocumentPlanItem_Index "

                Case Withdraw_Document.Packing
                    strSQL = "  UPDATE tb_PackingItem"
                    strSQL &= " SET Qty_WithDraw =Qty_WithDraw-@Qty "
                    strSQL &= " WHERE PackingItem_Index =@DocumentPlanItem_Index"

                Case Withdraw_Document.Transport 'killz upadte 08-08-2011
                    strSQL = "  UPDATE tb_SalesOrderItem "
                    strSQL &= " SET Qty_WithDraw = (Total_Qty_Withdraw / Ratio) - (@Total_Qty / Ratio) "
                    strSQL &= " , Total_Qty_Withdraw = Total_Qty_Withdraw - @Total_Qty "
                    strSQL &= " , Weight_Withdraw = Weight_Withdraw - @Weight "
                    strSQL &= " , Volume_Withdraw = Volume_Withdraw - @Volume "
                    strSQL &= " WHERE SalesOrderItem_Index= @DocumentPlanItem_Index "


            End Select
            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = DocumentPlanItem_Index
                .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = Qty
                .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = Total_Qty
                .Parameters.Add("@Item_Qty", SqlDbType.Float, 8).Value = ItemQty
                .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = Weight
                .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = Volume

            End With

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()

            DBExeNonQuery(strSQL, pConnection, pMytrans)

            ' Update Status
            UpdatePlanDocument_Status(DocumentPlan_Index, Process_Id, pConnection, pMytrans)

            'myTrans.Commit()
        Catch ex As Exception
            ' myTrans.Rollback()
            Throw ex
        Finally
            ' disconnectDB()
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pTableName"></param>
    ''' <param name="pColumn_Index"></param>
    ''' <param name="pColumn_Date"></param>
    ''' <param name="pCheck_Date"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Add Date : 19/01/2010
    ''' Add By   : Dong_kk
    ''' </remarks>
    Private Function Check_LastWithdraw_Date(ByVal pTableName As String, ByVal pColumn_Index As String, ByVal pStr_Index As String, ByVal pColumn_Date As String, ByVal pCheck_Date As String) As Boolean
        Dim strSQL As String
        Try

            '--- Check Before

            strSQL = " SELECT  " & pColumn_Date
            strSQL &= " FROM " & pTableName
            strSQL &= " Where " & pColumn_Index & "='" & pStr_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            Select Case _scalarOutput
                Case Nothing
                    Return True
                Case ""
                    Return True
                Case Else

                    strSQL = " SELECT  count(*) as gg"
                    strSQL &= " FROM " & pTableName
                    strSQL &= " Where " & pColumn_Index & "='" & pStr_Index & "'"
                    strSQL &= " AND " & pColumn_Date & "<'" & pCheck_Date & "'"

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
                    EXEC_Command()
                    _scalarOutput = GetScalarOutput

                    Select Case _scalarOutput
                        Case Nothing
                            Return False
                        Case ""
                            Return False
                        Case Else
                            Return True
                    End Select

            End Select




        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function Check_LastWithdraw_Date(ByVal pTableName As String, ByVal pColumn_Index As String, ByVal pStr_Index As String, ByVal pColumn_Date As String, ByVal pCheck_Date As String, ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As Boolean
        Dim strSQL As String
        Try

            '--- Check Before
            Dim tmpScalar As String

            strSQL = " SELECT  " & pColumn_Date
            strSQL &= " FROM " & pTableName
            strSQL &= " Where " & pColumn_Index & "='" & pStr_Index & "'"

            tmpScalar = DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text)

            Select Case tmpScalar
                Case Nothing
                    Return True
                Case ""
                    Return True
                Case Else

                    strSQL = " SELECT  count(*) as gg"
                    strSQL &= " FROM " & pTableName
                    strSQL &= " Where " & pColumn_Index & "='" & pStr_Index & "'"
                    strSQL &= " AND " & pColumn_Date & "<'" & pCheck_Date & "'"

                    tmpScalar = DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text)


                    Select Case tmpScalar
                        Case Nothing
                            Return False
                        Case ""
                            Return False
                        Case Else
                            Return True
                    End Select
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Function

#End Region

#Region "   Reserv  "
    'Function delete_ForUpdateItem(ByVal WithdrawItem_Index As String) As Boolean
    '    Dim strSQL As String = ""
    '    connectDB()
    '    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
    '    SQLServerCommand.Transaction = myTrans

    '    Try


    '        '#############  WitdrawReserve ############# 28/10/2008
    '        strSQL = " select * "
    '        strSQL &= " from tb_withdrawitemLocation"
    '        strSQL &= " where WithdrawItem_Index ='" & WithdrawItem_Index & "'  AND status = -9"

    '        SQLServerCommand.Connection = Connection
    '        SQLServerCommand.Transaction = myTrans
    '        SQLServerCommand.CommandText = strSQL
    '        SQLServerCommand.CommandTimeout = 0

    '        DataAdapter.SelectCommand = SQLServerCommand
    '        DataAdapter.SelectCommand.Transaction = myTrans
    '        Dim Total_Qty As decimal
    '        Dim Weight As decimal
    '        Dim Volume As decimal
    '        'Dim Item_Qty As decimal
    '        'Dim Price As decimal
    '        Dim LocationBalance_Index As String

    '        DS = New DataSet
    '        DataAdapter.Fill(DS, "twr")
    '        If DS.Tables("twr").Rows.Count <> 0 Then
    '            Dim ii As Integer = 0
    '            For ii = 0 To DS.Tables("twr").Rows.Count - 1
    '                LocationBalance_Index = DS.Tables("twr").Rows(ii).Item("LocationBalance_Index").ToString
    '                Total_Qty = DS.Tables("twr").Rows(ii).Item("Total_Qty")
    '                Weight = DS.Tables("twr").Rows(ii).Item("Weight")
    '                Volume = DS.Tables("twr").Rows(ii).Item("Volume")


    '                strSQL = "  UPDATE tb_LocationBalance    "
    '                strSQL &= " SET ReserveQty= ReserveQty -" & Total_Qty
    '                strSQL &= " ,ReserveWeight = ReserveWeight -  " & Weight
    '                strSQL &= " ,ReserveVolume = ReserveVolume -   " & Volume
    '                strSQL &= " WHERE LocationBalance_Index= '" & LocationBalance_Index & "'"

    '                SetSQLString = strSQL
    '                SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                EXEC_Command()
    '            Next
    '        End If

    '        strSQL = " delete  "
    '        strSQL &= " FROM  tb_WithdrawItem "
    '        strSQL &= " where WithdrawItem_Index = '" & WithdrawItem_Index & "'"

    '        SetSQLString = strSQL
    '        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '        EXEC_Command()

    '        '-------------------------------------------------------------------------
    '        ' ***** Step 2 Delete tb_WithdrawItemLocation***
    '        strSQL = " delete  "
    '        strSQL &= " FROM  tb_WithdrawItemLocation "
    '        strSQL &= " where WithdrawItem_Index = '" & WithdrawItem_Index & "'"

    '        SetSQLString = strSQL
    '        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '        EXEC_Command()

    '        '*** Commit transaction
    '        myTrans.Commit()

    '        Return True
    '    Catch ex As Exception
    '        myTrans.Rollback()

    '        Throw ex
    '    End Try

    'End Function
#End Region

#Region "   Check WithBarcode   "
    Public Sub chkWithDraw_Barcode(ByVal WithDraw_Index As String)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     * FROM VIEW_WithDrawBarcode "
            strSQL &= " WHERE WithDraw_Index = '" & WithDraw_Index & "'"
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

#Region " SELECT DATA "
    ''' <summary>
    ''' 05-02-2010
    ''' ja
    ''' getWithdrawAlert for sonic
    ''' </summary>
    ''' <remarks></remarks>

    Public Sub getWithdrawAlert()

        Dim strSQL As String = ""

        Try
            strSQL = " SELECT * "
            strSQL &= " FROM  VIEW_WithdrawAlert "


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




    Public Sub UpdateConfirmFlagWithdraw(ByVal pLWithdraw_Index As List(Of String))
        Try


            If pLWithdraw_Index Is Nothing Then Exit Sub
            If pLWithdraw_Index.Count = 0 Then Exit Sub
            For Each str As String In pLWithdraw_Index
                DBExeNonQuery(String.Format(" UPDATE  tb_Withdraw set Confirm_Flag = 1 WHERE Withdraw_Index = '{0}'", str))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub UpdateUnConfirmFlagWithdraw(ByVal pLWithdraw_Index As List(Of String))
        Try


            If pLWithdraw_Index Is Nothing Then Exit Sub
            If pLWithdraw_Index.Count = 0 Then Exit Sub
            For Each str As String In pLWithdraw_Index
                DBExeNonQuery(String.Format(" UPDATE  tb_Withdraw set Confirm_Flag = 0 WHERE Withdraw_Index = '{0}'", str))
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Public Function GetConfirmFlagWithdraw() As DataTable
        Try
            Return DBExeQuery(" SELECT Withdraw_Index FROM VIEW_WithdrawConfirmFlag where Confirm_Flag = 1 and Status in (1,3) ")
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CheckStatusWithDraw(ByVal pWithdraw_Index As String) As Boolean
        Try

            Dim Result As String = DBExeQuery_Scalar(String.Format(" SELECT Withdraw_Index FROM tb_withdraw where withdraw_Index = '{0}' and Status in (1,3) ", pWithdraw_Index))
            If Result Is Nothing Then Return False
            If IsDBNull(Result) Then Return False
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function



End Class
