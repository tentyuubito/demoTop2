Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master


Public Class OrderTransaction : Inherits DBType_SQLServer
    ' Header : tb_Order 
    ' Item   : tb_OrderItem
    '*** Fileds
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
    Dim StatusWithdraw_Document As Withdraw_Document
    Private Enum Withdraw_Document
        Shipment = 103
        Packing = 7
        PO = 9
        ASN = 16
    End Enum


#Region "Operation Type "
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

    Private _objHeader As New WMS_STD_INB_Receive_Datalayer.tb_Order
    Private _objItemCollection As List(Of tb_OrderItem)
    Private _objItem As New tb_OrderItem
    Private _newOrder_Index As String

    Private _objPalletType As New tb_PalletType_History
    Private _objPalletTypeCollection As New List(Of tb_PalletType_History)

    Private _objOrderItemsku_Collection As New List(Of WMS_STD_INB_Receive_Datalayer.tb_OrderItemSku)
    Private _objItemsku As New WMS_STD_INB_Receive_Datalayer.tb_OrderItemSku
    Private _Order_ItemIndex As String = ""
    Private _Order_no As String = ""

    Private Property NewOrder_Index() As String
        Get
            Return _newOrder_Index
        End Get
        Set(ByVal value As String)
            _newOrder_Index = value
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
    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objHeader As WMS_STD_INB_Receive_Datalayer.tb_Order, ByVal objItemCollection As List(Of tb_OrderItem), ByVal objPalletTypeCollection As List(Of tb_PalletType_History))
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

    Public Sub New(ByVal objItemCollection As List(Of WMS_STD_INB_Receive_Datalayer.tb_OrderItemSku))
        MyBase.New()
        _objOrderItemsku_Collection = objItemCollection
    End Sub
    Property Order_ItemIndex() As String
        Get
            Return _Order_ItemIndex
        End Get
        Set(ByVal value As String)
            _Order_ItemIndex = value
        End Set
    End Property

    Property Order_NO() As String
        Get
            Return _Order_no
        End Get
        Set(ByVal value As String)
            _Order_no = value
        End Set
    End Property

#End Region

#Region " SELECT ORDER VIEW "
    Public Sub getOrderItemAll(ByVal pstrOrderItem_Index As String)

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT  *  "
            strSQL &= " FROM     tb_OrderItem "
            strSQL &= " WHERE   OrderItem_Index='" & pstrOrderItem_Index & "' "

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

    Public Function getOrderviewSearch_Reader(ByVal WhereString As String, ByVal pintRowStart As Integer, ByVal pintRowEnd As Integer, ByVal CaseLoad As Boolean, ByVal pintTop As Integer, ByVal CaseLoadAll As Boolean) 'As Data.SqlClient.SqlDataReader
        Dim strSQL As String = ""
        'ton update 24/04/2015
        Try
            If CaseLoadAll = True Then

                strSQL = "  SELECT * "
                strSQL &= "  FROM VIEW_Orderview "

                If WhereString <> "" Then
                    strSQL &= " WHERE 1=1 " & WhereString
                End If
            Else
                If CaseLoad = True Then

                    strSQL = "  SELECT top " & pintTop & "  * "
                    strSQL &= "  FROM VIEW_Orderview "

                    If WhereString <> "" Then
                        strSQL &= " WHERE 1=1 " & WhereString
                    End If

                Else

                    strSQL = " SELECT * "
                    strSQL &= " FROM  "
                    strSQL &= " ("
                    strSQL &= "  SELECT ROW_NUMBER() OVER(ORDER BY Order_Index ASC) AS ROW_NO,  * "
                    strSQL &= "  FROM VIEW_Orderview "

                    If WhereString <> "" Then
                        strSQL &= " WHERE 1=1 " & WhereString
                    End If
                    strSQL &= " ) AS SO_View "

                    strSQL &= " WHERE ROW_NO >= " & pintRowStart & " AND  ROW_NO <= " & pintRowEnd
                End If
            End If

            'strSQL &= "  ORDER BY Str1 ASC,DocumentType"



            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            'EXEC_Command()


            Return Me.GetDataReader

        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
        End Try
    End Function

    Public Sub getOrderView(ByVal WhereString As String)

        Dim strSQL As String = ""

        Try
            strSQL = " SELECT     * "
            strSQL &= " FROM     VIEW_Orderview"
            strSQL &= " WHERE Process_Id =1  "


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

    Public Sub getOrderViewSearch_Count(ByVal WhereString As String) 'Update By ton 2015/04/24
        Dim strSQL As String = ""
        Try

            strSQL = "select count(*) Row_Total from VIEW_Orderview "
            strSQL &= " WHERE 1=1 "

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


    Public Sub getOrderView3PL(ByVal WhereString As String)

        Dim strSQL As String = ""

        Try

            strSQL = " SELECT     * "
            strSQL &= " FROM    VIEW_3PL_OrderView "
            strSQL &= " WHERE   VIEW_3PL_OrderView.Process_Id = 1 "

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

    Public Sub getOrderView_TFAC(ByVal WhereString As String)

        Dim strSQL As String = ""

        Try
            strSQL = " SELECT     tb_Order.Order_Index, tb_Order.Order_No, tb_Order.Order_Date, tb_Order.Order_Time, tb_Order.Ref_No1, tb_Order.Ref_No2, tb_Order.Ref_No3, tb_Order.Ref_No4, tb_Order.add_by , tb_Order.PO_No," & _
                  " tb_Order.Ref_No5, ms_Customer.Customer_Index, ms_Customer.Customer_Id, ms_Customer.Title, ms_Customer.Customer_Name, " & _
                  " ms_ProcessStatus.Status, ms_ProcessStatus.Description as StatusDescription, " & _
                  " ms_Department.Description as Department_Name , ms_Supplier.Supplier_Index, ms_Supplier.Supplier_Name,ms_DocumentType.Description as DocType,tb_Order.Ref_No1,ms_DocumentType.DocumentType_Index as DocumentType_Index ,tb_Order.Consignee_Index " & _
                  " FROM         tb_Order INNER JOIN " & _
                  " ms_Customer ON tb_Order.Customer_Index = ms_Customer.Customer_Index INNER JOIN " & _
                  " ms_ProcessStatus ON tb_Order.Status = ms_ProcessStatus.Status  LEFT JOIN " & _
                  " ms_Department ON tb_Order.Department_Index = ms_Department.Department_Index LEFT JOIN " & _
                  " ms_Supplier ON tb_Order.Supplier_Index =ms_Supplier.Supplier_Index  LEFT JOIN " & _
                  " ms_DocumentType ON  tb_Order.DocumentType_Index = ms_DocumentType.DocumentType_Index " & _
                  " WHERE ms_ProcessStatus.Process_Id =1  "


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

    Public Sub getProcessStatus()

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT * "
            strSQL &= " FROM     VIEW_ProcessStatus "
            strSQL &= " WHERE Process_Id= 1  AND Show=1  "
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

    Public Sub getDeliveryReceive(ByVal SoNO As String)

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT *  "
            strSQL &= " FROM     VIEW_Auto_DeleveryReceive "
            strSQL &= " WHERE 1=1  and SO_NO ='" & SoNO & "'"

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

    Public Sub OrderItemDetail(ByVal WhereString As String)

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT *  "
            strSQL &= " FROM     VIEW_TGI_Print_PalletSlip "
            strSQL &= " WHERE Status <> -1  "

            If WhereString <> "" Then
                SetSQLString = strSQL & WhereString
            Else
                SetSQLString = strSQL
            End If

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getEdit_Order(ByVal Order_Index As String)

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT *  "
            strSQL &= " FROM     VIEW_TGI_Edit_Order "
            strSQL &= " WHERE Status <> -1   and Order_Index ='" & Order_Index & "' "


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

#Region " SAVE ORDER "
    ''' <summary>
    ''' Save Data: Main function to call insert or update order
    ''' </summary>
    ''' <returns>New Order Index, if success. "", if failed.</returns>
    ''' <remarks> In case of new document, we need to generate new document ID.</remarks>
    Public Function SaveData() As String

        Select Case objStatus
            Case enuOperation_Type.ADDNEW

                Me._newOrder_Index = Me.InsertData()

                If Not Me._newOrder_Index = "" Then
                    ' Success 
                    Return Me._newOrder_Index
                    Return Me._objHeader.Order_No
                Else
                    ' Not Success 
                    Return ""
                End If

            Case enuOperation_Type.UPDATE
                Me._newOrder_Index = Me.UpdateData
                If Not Me._newOrder_Index = "" Then
                    ' Success 
                    Return Me._newOrder_Index
                Else
                    ' Not Success 
                    Return ""
                End If


        End Select

        'For irow As Integer = 0 To _objItemCollection.Count - 1
        '    Dim Order_Index As String = _objItemCollection.Item(irow).Order_Index
        'Next

        ' TOFIX: Why return true here?
        Return True
    End Function

    Public Function SaveData_Update() As String

        Select Case objStatus
            Case enuOperation_Type.ADDNEW

                Me._newOrder_Index = Me.InsertData()

                If Not Me._newOrder_Index = "" Then
                    ' Success 
                    _Order_no = _objHeader.Order_No
                    Return Me._newOrder_Index

                    Return Me._objHeader.Order_No
                Else
                    ' Not Success 
                    Return ""
                End If

            Case enuOperation_Type.UPDATE
                Me._newOrder_Index = Me.UpdateData
                If Not Me._newOrder_Index = "" Then
                    ' Success 
                    _Order_no = _objHeader.Order_No
                    Return Me._newOrder_Index


                Else
                    ' Not Success 
                    Return ""
                End If


        End Select

        'For irow As Integer = 0 To _objItemCollection.Count - 1
        '    Dim Order_Index As String = _objItemCollection.Item(irow).Order_Index
        'Next

        ' TOFIX: Why return true here?
        Return True
    End Function
#End Region

#Region " ADD NEW ORDER "

    ''' <summary>
    ''' Insert Order and Order Item data
    ''' </summary>
    ''' <returns>New Order Index, if success. "", if failed.</returns>
    ''' <remarks>
    ''' Date 2009-01-19
    ''' BY TaTa
    ''' Fix bug  Receive  Shipment
    ''' -----------------------------------------------
    ''' Update Date : 20/01/2010
    ''' Update by   : Dong_kk
    ''' Update For  : comment 'Return Me._objHeader.Order_No /Update Last_Receive_Date
    '''             : QC Receive With Plan Document
    ''' ------------------------------------------------
    ''' </remarks>
    ''' 


    Private Function InsertData() As String
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Insert Header : tb_order
        ' --- STEP 2: Insert Line Item: tb_OrderItem
        ' --- STEP 3: Get New Order Index
        ' --- STEP 4: Update New Order_Index with tb_Order
        ' --- STEP 5: Update New Order_Index with tb_OrderItem
        Try

            If Trim(_objHeader.Order_No) = "" Then
                Dim objDocumentNumber As New Sy_DocumentNumber
                _objHeader.Order_No = (objDocumentNumber.getAuto_DocumentNumber("ORD", "Order_No", "tb_Order"))
                _Order_no = _objHeader.Order_No
                objDocumentNumber = Nothing
            End If

            ' --- STEP 1: Insert Header : tb_order 
            strSQL = " INSERT INTO tb_Order ( Order_Index,Order_No,Order_Date,Order_Time,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5, "
            strSQL &= " Lot_No,Customer_Index,Supplier_Index,Department_Index,DocumentType_Index,Status,Comment,"
            strSQL &= " Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,  "
            strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,PO_No,Invoice_No,ASN_No,Checker_Name,ApprovedBy_Name,"
            strSQL &= " HandlingType_Index,Vassel_Name,Flight_No,Vehicle_No,Transport_by,Origin_Port_Id,Origin_Country_Id,Destination_Port_Id,Destination_Country_Id,Terminal_Id,Receive_Type,Consignee_Index"
            strSQL &= " ,Departure_Date,Arrival_Date) "
            strSQL &= " Values "
            strSQL &= "  ( @Order_Index,@Order_No,@Order_Date,@Order_Time,@Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5, "
            strSQL &= " @Lot_No,@Customer_Index,@Supplier_Index,@Department_Index,@DocumentType_Index,@Status,@Comment,"
            strSQL &= " @Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10, "
            strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@PO_No,@Invoice_No,@ASN_No,@Checker_Name,@ApprovedBy_Name,"
            strSQL &= " @HandlingType_Index,@Vassel_Name,@Flight_No,@Vehicle_No,@Transport_by,@Origin_Port_Id,@Origin_Country_Id,@Destination_Port_Id,@Destination_Country_Id,@Terminal_Id,@Receive_Type,@Consignee_Index"
            'include from master site
            strSQL &= "  ,@Departure_Date,@Arrival_Date)"
            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objHeader.Order_Index
                .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = _objHeader.Order_No
                .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Order_Date.ToString("yyyy/MM/dd")
                .Parameters.Add("@Order_Time", SqlDbType.VarChar, 50).Value = _objHeader.Order_Time
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No2
                .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No3
                .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No4
                .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No5
                .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = _objHeader.Lot_No
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _objHeader.Supplier_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objHeader.Comment
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objHeader.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objHeader.Str10
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objHeader.Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = _objHeader.PO_No
                .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objHeader.Invoice_No
                .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objHeader.ASN_No
                .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 50).Value = _objHeader.Checker_Name
                .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 50).Value = _objHeader.ApprovedBy_Name

                'include from master site
                .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Departure_Date
                .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Arrival_Date
                .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index
                .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = _objHeader.Vassel_Name
                .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = _objHeader.Flight_No
                .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = _objHeader.Vehicle_No
                .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = _objHeader.Transport_by
                .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Port_Id
                .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Country_Id
                .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Port_Id
                .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Country_Id
                .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = _objHeader.Terminal_Id
                .Parameters.Add("@Receive_Type", SqlDbType.Int, 4).Value = _objHeader.Receive_Type

                .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objHeader.Consignee_Index

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Insert Line Item: tb_OrderItem 
            For Each _objItem In _objItemCollection

                strSQL = " INSERT INTO tb_OrderItem ( OrderItem_Index,Order_Index,Sku_Index, "
                strSQL &= " ItemStatus_Index,Package_Index,Ratio,Qty,Total_Qty,Plan_Qty,PalletType_Index,"
                strSQL &= " Pallet_Qty,Weight,Volume,IsMfg_Date,Mfg_Date,IsExp_date,Exp_date, "
                strSQL &= " Status,Comment,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,ItemDefinition_Index,"
                strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Is_SN,"
                strSQL &= " Lot_No,Plot,Serial_No,PO_No,Invoice_No,ASN_No,Declaration_No,"
                strSQL &= " Plan_Process, DocumentPlan_No, DocumentPlanItem_Index, DocumentPlan_Index,"
                strSQL &= " Item_Qty,Qty_Per_Pck,Weight_Per_Pck,Price_Per_Pck,Volume_Per_Pck,OrderItem_Price,Item_Package_Index,HandlingType_Index,Tax1,Tax2,Tax3,Tax4,Tax5"
                strSQL &= " ,HS_Code,ItemDescription,Seq,Consignee_Index,ERP_Location,WeightScale "
                strSQL &= " ) "
                strSQL &= " Values "
                strSQL &= "( @OrderItem_Index,@Order_Index,@Sku_Index, "
                strSQL &= " @ItemStatus_Index,@Package_Index,@Ratio,@Qty,@Total_Qty,@Plan_Qty,@PalletType_Index,"
                strSQL &= " @Pallet_Qty,@Weight,@Volume,@IsMfg_Date,@Mfg_Date,@IsExp_date,@Exp_date, "
                strSQL &= " @Status,@Comment,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@ItemDefinition_Index, "
                strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Is_SN,"
                strSQL &= " @Lot_No,@Plot,@Serial_No,@PO_No,@Invoice_No,@ASN_No,@Declaration_No,"
                'strSQL &= " @Item_Qty,@Qty_Per_Pck,@Weight_Per_Pck,@Price_Per_Pck,@Volume_Per_Pck,@OrderItem_Price,@Item_Package_Index,@HandlingType_Index"
                'include from master site
                strSQL &= " @Plan_Process, @DocumentPlan_No, @DocumentPlanItem_Index, @DocumentPlan_Index,"
                strSQL &= " @Item_Qty,@Qty_Per_Pck,@Weight_Per_Pck,@Price_Per_Pck,@Volume_Per_Pck,@OrderItem_Price,@Item_Package_Index,@HandlingType_Index,@Tax1,@Tax2,@Tax3,@Tax4,@Tax5"
                strSQL &= " ,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@ERP_Location,@WeightScale "
                strSQL &= " ) "


                With SQLServerCommand

                    .Parameters.Clear()

                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.OrderItem_Index
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objItem.Order_Index
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemStatus_Index
                    .Parameters.Add("@IsMfg_Date", SqlDbType.Bit, 1).Value = _objItem.IsMfg_Date
                    .Parameters.Add("@Mfg_Date", SqlDbType.SmallDateTime, 4).Value = _objItem.Mfg_Date
                    .Parameters.Add("@IsExp_Date", SqlDbType.Bit, 1).Value = _objItem.IsExp_Date
                    .Parameters.Add("@Exp_Date", SqlDbType.SmallDateTime, 4).Value = _objItem.Exp_date
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objItem.Comment
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 4000).Value = _objItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objItem.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objItem.Str10
                    .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = _objItem.ItemDefinition_Index
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objItem.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objItem.Flo5
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    .Parameters.Add("@Is_SN", SqlDbType.Bit, 1).Value = _objItem.Is_SN

                    '--- PCK
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objItem.Sku_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = _objItem.Package_Index
                    .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Qty
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _objItem.PalletType_Index
                    .Parameters.Add("@Pallet_Qty", SqlDbType.Decimal).Value = _objItem.Pallet_Qty
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = _objItem.Ratio
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = Format(CDbl(_objItem.Qty.ToString), "###0.0000")
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = Format(CDbl(_objItem.Total_Qty.ToString), "###0.0000")
                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 15).Value = _objItem.Item_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume
                    .Parameters.Add("@OrderItem_Price", SqlDbType.Float, 8).Value = _objItem.OrderItem_Price
                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Item_Package_Index
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingType_Index

                    '--- Document 
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = _objItem.Lot_No
                    .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = _objItem.Plot
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = _objItem.Serial_No
                    .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = _objItem.PO_No
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objItem.Invoice_No
                    .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objItem.ASN_No
                    .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 50).Value = _objItem.Declaration_No

                    '--- Per_PCK 
                    .Parameters.Add("@Qty_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Qty_Per_Pck
                    .Parameters.Add("@Weight_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Weight_Per_Pck
                    .Parameters.Add("@Volume_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Volume_Per_Pck
                    .Parameters.Add("@Price_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Price_Per_Pck

                    'tax include from master site
                    .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = _objItem.Tax1
                    .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = _objItem.Tax2
                    .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = _objItem.Tax3
                    .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = _objItem.Tax4
                    .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = _objItem.Tax5

                    ' include from master site
                    .Parameters.Add("@HS_Code", SqlDbType.VarChar, 50).Value = _objItem.HS_Code
                    .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 200).Value = _objItem.ItemDescription
                    .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = _objItem.Seq

                    'add new 11-01-2010 Consignee_Index 
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objItem.Consignee_Index

                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = _objItem.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = _objItem.DocumentPlan_No
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index

                    .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = _objItem.ERP_Location
                    .Parameters.Add("@WeightScale", SqlDbType.Decimal).Value = _objItem.WeightScale



                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                '***************** Warning !!! Please Check Data Make Sure / Before In Process ***************************

                If _objItem.Plan_Process <> -9 Then
                    StatusWithdraw_Document = _objItem.Plan_Process
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.Packing
                            strSQL = "  UPDATE tb_Packing"
                            strSQL &= " SET Qty_Receive =Qty_Receive+" & _objItem.Total_Qty
                            strSQL &= " , Weight_Sum =Weight_Sum+" & _objItem.Weight
                            strSQL &= " , Volume =Volume+" & _objItem.Volume

                            strSQL &= " WHERE Packing_Index ='" & _objItem.DocumentPlan_Index & "'"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            '--- Update Status_Packing
                            Update_Packing(_objItem.DocumentPlan_Index)

                        Case Withdraw_Document.PO
                            Me.UPDATE_ADD_PLANRECEIVE(_objItem, myTrans)

                         
                        Case Withdraw_Document.ASN
                            Me.UPDATE_ADD_PLANRECEIVE(_objItem, myTrans)
                           
                        Case Withdraw_Document.Shipment
                            'Shipment
                            strSQL = " UPDATE shtb_ShipmentReceived SET "
                            strSQL &= " Received_Qty = Received_Qty + " & _objItem.Qty
                            strSQL &= " ,Total_Received_Qty = Total_Received_Qty + " & _objItem.Total_Qty
                            strSQL &= " ,Received_Weight = Received_Weight + " & _objItem.Weight
                            '--- Last_Received_Date
                            If Check_LastReceive_Date("shtb_ShipmentReceived", "shtb_ShipmentReceived_Index", _objItem.DocumentPlanItem_Index, "Last_Received_Date", _objHeader.Order_Date.ToString("yyyy/MM/dd")) Then
                                strSQL &= " ,Last_Received_Date='" & _objHeader.Order_Date.ToString("yyyy/MM/dd") & "'"
                            End If
                            strSQL &= " WHERE Sku_Index ='" & _objItem.Sku_Index & "'"
                            strSQL &= " AND Shipment_Index='" & _objItem.DocumentPlanItem_Index & "'"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()


                            strSQL = " UPDATE shtb_Shipment SET "
                            strSQL &= "  StatusReceive = 0"
                            strSQL &= " WHERE Shipment_Index='" & _objItem.DocumentPlan_Index & "'"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' 2010-01-19 TaTa 
                            ' Fix bug  Receive  Shipment

                            If Check_Shipment(_objItem.Str7) = True Then

                                strSQL = " UPDATE shtb_Shipment SET "
                                strSQL &= " Status_Id = 3 , StatusReceive = 0"
                                strSQL &= " WHERE Shipment_Index='" & _objItem.DocumentPlan_Index & "'"

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()
                            End If

                            If Check_OverReceiveShipment(_objItem.Str7) = True Then

                                strSQL = " UPDATE shtb_Shipment SET "
                                strSQL &= " Status_Id = 3 , StatusReceive = 0"
                                strSQL &= " WHERE Shipment_Index='" & _objItem.DocumentPlan_Index & "'"

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            End If


                    End Select
                End If






                '************************************************************************


                '************************************************************************
                ' updade OrderItem_Index To tb_AssetLocationBalance,tb_AssetTransaction
                strSQL = " UPDATE tb_AssetLocationBalance SET  "
                strSQL &= "OrderItem_Index = '" & _objItem.OrderItem_Index & "'"
                strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "' AND OrderItem_Index = '" & _objItem.iRow_OrderItem_Index & "'"


                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                strSQL = " UPDATE tb_AssetTransaction SET  "
                strSQL &= " OrderItem_Index = '" & _objItem.OrderItem_Index & "'"
                strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "' AND OrderItem_Index = '" & _objItem.iRow_OrderItem_Index & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()



                '************************************************************************


                strSQL = " select Qty,Total_Qty,Weight,Volume,add_date"
                strSQL &= " FROM tb_OrderItem "
                strSQL &= " WHERE Sku_Index ='" & _objItem.Sku_Index & "'"
                strSQL &= " AND Str7='" & _objItem.Str7 & "'"
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
                    _objItem.Total_Qty = Val(DS.Tables("tbl").Rows(0).Item("Total_Qty").ToString)
                    _objItem.Qty = Val(DS.Tables("tbl").Rows(0).Item("Qty").ToString)
                    _objItem.Weight = Val(DS.Tables("tbl").Rows(0).Item("Weight").ToString)
                    _objItem.Volume = Val(DS.Tables("tbl").Rows(0).Item("Volume").ToString)
                End If



                'Update orderItem ของ OrderItemSku
                Dim row As Integer = _objItem.OrderItem_RowIndex
                strSQL = "Update tb_OrderItemSku set "
                strSQL &= " OrderItem_index='" & _objItem.OrderItem_Index & "'"
                strSQL &= " where Left(OrderItem_index,3)='TEM' and substring(OrderItem_index,4,len(OrderItem_index)+1) = '" & row & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


            Next


            'INSERT TB_PalletType_History
            If _objPalletTypeCollection IsNot Nothing Then

                For Each _objPalletType In _objPalletTypeCollection

                    If _objPalletType.PalletType_History_Index <> "" Then
                        strSQL = "INSERT INTO tb_PalletType_History "
                        strSQL &= " (PalletType_History_Index,Order_Index,PalletType_Index,Process_Id,Qty_In,Qty_Bal)"
                        strSQL &= " Values "
                        strSQL &= " (@PalletType_History_Index,@Order_Index,@PalletType_Index,@Process_Id,@Qty_In,@Qty_Bal)"

                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = _objPalletType.PalletType_History_Index
                            .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _objPalletType.PalletType_Index
                            .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1
                            .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objHeader.Order_Index
                            .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = _objPalletType.Qty_In
                            .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = _objPalletType.Qty_Bal
                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If
                    '----------------------------

                Next

            End If

            ' --- STEP 3: Get New Order Index
            Dim objDBIndex As New Sy_AutoNumber
            Me._newOrder_Index = objDBIndex.getSys_Value("Order_Index")
            'objDBIndex = Nothing

            ' --- STEP 4: Update New Order_Index with tb_Order
            strSQL = " UPDATE tb_Order SET  "
            strSQL &= " Order_Index='" & Me._newOrder_Index & "' ,Status=1 "
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            strSQL = " UPDATE tb_AssetLocationBalance SET  "
            strSQL &= " Order_Index='" & Me._newOrder_Index & "'" ' ,Status=1"
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            strSQL = " UPDATE tb_AssetTransaction SET  "
            strSQL &= " Order_Index='" & Me._newOrder_Index & "'"
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            strSQL = " UPDATE tb_Order_Image SET  "
            strSQL &= " Order_Index='" & Me._newOrder_Index & "'"
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()



            strSQL = " UPDATE tb_OrderTruckIn SET  "
            strSQL &= " Order_Index='" & Me._newOrder_Index & "' ,Status_Id =1 "
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()



            ' Update Get New Order Index (tb_AssetLocationBalancet,tb_AssetLocationBalancetTransfer)


            '----------------------------
            If _objPalletType.PalletType_History_Index IsNot Nothing Then
                strSQL = " UPDATE tb_PalletType_History SET  "
                strSQL &= " Order_Index='" & Me._newOrder_Index & "' "
                strSQL &= " WHERE PalletType_History_Index='" & Me._objPalletType.PalletType_History_Index & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            End If
            ' --- STEP 5: Update New Order_Index with tb_OrderItem
            strSQL = " UPDATE tb_OrderItem SET  "
            strSQL &= " Order_Index='" & Me._newOrder_Index & "' ,Status=1 "
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = Me._newOrder_Index
            oAudit_Log.Document_No = _objHeader.Order_No
            oAudit_Log.Ref_No1 = _objHeader.Ref_No1
            oAudit_Log.Ref_No1 = _objHeader.Ref_No2
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_Receiving_Order)


            'Dong 2011/01/20 Default Assigne User
            Dim Use_Skip_AssignJob_Order As Boolean = False
            Dim config_Custom_Setting As New config_CustomSetting
            Use_Skip_AssignJob_Order = config_Custom_Setting.getConfig_Key_USE("USE_Skip_AssignJob_Order")
            If Use_Skip_AssignJob_Order = False Then
                Dim oAssign As New WMS_STD_OAW_Adjust_Datalayer.tb_AssignJob
                With oAssign
                    .User_Index = "0010000000001"
                    .Assign_Date = Now
                    .DocumentPlan_No = _objHeader.Order_No
                    .DocumentPlan_Index = Me._newOrder_Index
                    .Plan_Process = 1
                    .Priority = 3
                    .AssignJob_Index = objDBIndex.getSys_Value("AssignJob_Index")
                    .InsertData()
                End With
            End If


            objDBIndex = Nothing


            ' --- Commit transaction
            myTrans.Commit()
            Return Me._newOrder_Index
            'Return Me._objHeader.Order_No

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally

            disconnectDB()
        End Try

    End Function
#End Region

#Region " UPDATE ORDER "

    ''' <summary>
    ''' Update Order and Order Item Data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateData() As String
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update Header : tb_order
        ' --- STEP 2: Loop to update (or insert if item exists) tb_OrderItem
        Try

            ' --- STEP 1: Update Header : tb_order
            strSQL = " UPDATE tb_Order "
            strSQL &= " SET "
            strSQL &= " Order_No=@Order_No,Order_Date=@Order_Date,Order_Time=@Order_Time,Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Ref_No3=@Ref_No3,Ref_No4=@Ref_No4,Ref_No5=@Ref_No5, "
            strSQL &= " Lot_No=@Lot_No,Customer_Index=@Customer_Index,Supplier_Index=@Supplier_Index,Department_Index=@Department_Index,DocumentType_Index=@DocumentType_Index,Comment=@Comment,"
            strSQL &= " Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,Str6=@Str6,Str7=@Str7,Str8=@Str8,Str9=@Str9,Str10=@Str10, "
            strSQL &= " Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,update_date=getdate(),update_by=@update_by,update_branch=@update_branch,PO_No=@PO_No,Invoice_No=@Invoice_No,ASN_No=@ASN_No,Checker_Name=@Checker_Name,ApprovedBy_Name=@ApprovedBy_Name,"
            'strSQL &= " HandlingType_Index=@HandlingType_Index,Vassel_Name=@Vassel_Name,Flight_No=@Flight_No,Vehicle_No=@Vehicle_No,Transport_by=@Transport_by,Origin_Port_Id=@Origin_Port_Id,Origin_Country_Id=@Origin_Country_Id,Destination_Port_Id=@Destination_Port_Id,Destination_Country_Id=@Destination_Country_Id,Terminal_Id=@Terminal_Id,Receive_Type=@Receive_Type,Consignee_Index=@Consignee_Index"
            'include from master site
            strSQL &= " HandlingType_Index=@HandlingType_Index,Vassel_Name=@Vassel_Name,Flight_No=@Flight_No,Vehicle_No=@Vehicle_No,Transport_by=@Transport_by,Origin_Port_Id=@Origin_Port_Id,Origin_Country_Id=@Origin_Country_Id,Destination_Port_Id=@Destination_Port_Id,Destination_Country_Id=@Destination_Country_Id,Terminal_Id=@Terminal_Id,Receive_Type=@Receive_Type,Consignee_Index=@Consignee_Index,Departure_Date=@Departure_Date,Arrival_Date=@Arrival_Date"
            strSQL &= " WHERE Order_Index=@Order_Index "

            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objHeader.Order_Index
                .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = _objHeader.Order_No
                .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Order_Date
                .Parameters.Add("@Order_Time", SqlDbType.VarChar, 50).Value = _objHeader.Order_Time
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No2
                .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No3
                .Parameters.Add("@Ref_No4", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No4
                .Parameters.Add("@Ref_No5", SqlDbType.VarChar, 50).Value = _objHeader.Ref_No5
                .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = _objHeader.Lot_No
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objHeader.Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _objHeader.Supplier_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objHeader.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objHeader.DocumentType_Index
                .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objHeader.Comment
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objHeader.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objHeader.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objHeader.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objHeader.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objHeader.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objHeader.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objHeader.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objHeader.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objHeader.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objHeader.Str10
                .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objHeader.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objHeader.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objHeader.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objHeader.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objHeader.Flo5
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = _objHeader.PO_No
                .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objHeader.Invoice_No
                .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objHeader.ASN_No
                .Parameters.Add("@Checker_Name", SqlDbType.VarChar, 50).Value = _objHeader.Checker_Name
                .Parameters.Add("@ApprovedBy_Name", SqlDbType.VarChar, 50).Value = _objHeader.ApprovedBy_Name

                'include from master site
                .Parameters.Add("@Departure_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Departure_Date
                .Parameters.Add("@Arrival_Date", SqlDbType.SmallDateTime, 4).Value = _objHeader.Arrival_Date
                .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objHeader.HandlingType_Index
                .Parameters.Add("@Vassel_Name", SqlDbType.VarChar, 50).Value = _objHeader.Vassel_Name
                .Parameters.Add("@Flight_No", SqlDbType.VarChar, 50).Value = _objHeader.Flight_No
                .Parameters.Add("@Vehicle_No", SqlDbType.VarChar, 50).Value = _objHeader.Vehicle_No
                .Parameters.Add("@Transport_by", SqlDbType.VarChar, 50).Value = _objHeader.Transport_by
                .Parameters.Add("@Origin_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Port_Id
                .Parameters.Add("@Origin_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Origin_Country_Id
                .Parameters.Add("@Destination_Port_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Port_Id
                .Parameters.Add("@Destination_Country_Id", SqlDbType.VarChar, 50).Value = _objHeader.Destination_Country_Id
                .Parameters.Add("@Terminal_Id", SqlDbType.VarChar, 50).Value = _objHeader.Terminal_Id
                .Parameters.Add("@Receive_Type", SqlDbType.Bit, 1).Value = _objHeader.Receive_Type

                .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objHeader.Consignee_Index



            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Loop to update (or insert if item exists) tb_OrderItem
            For Each _objItem In _objItemCollection

                ' --- Check if the item exist?

                If isExistID(_objItem.OrderItem_Index) = True Then
                    'Begin TOP   update หรือคืนค่าให้กับใบตั้งรับ add date : 18/03/2013



                    strSQL = " SELECT * FROM tb_OrderItem WHERE  OrderItem_Index= @OrderItem_Index"
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.OrderItem_Index
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    DS = New DataSet
                    DataAdapter.Fill(DS, "OLDOI")
                    Dim oOldOrderItem As New tb_OrderItem
                    oOldOrderItem.DocumentPlan_Index = _objItem.DocumentPlan_Index
                    oOldOrderItem.DocumentPlanItem_Index = _objItem.DocumentPlanItem_Index
                    oOldOrderItem.Plan_Process = _objItem.Plan_Process
                    oOldOrderItem.Total_Qty = CDbl(DS.Tables("OLDOI").Rows(0).Item("Total_Qty"))
                    oOldOrderItem.Qty = CDbl(DS.Tables("OLDOI").Rows(0).Item("Qty"))
                    oOldOrderItem.Weight = CDbl(DS.Tables("OLDOI").Rows(0).Item("Weight"))
                    oOldOrderItem.Volume = CDbl(DS.Tables("OLDOI").Rows(0).Item("Volume"))
                    StatusWithdraw_Document = oOldOrderItem.Plan_Process

                    UPDATE_DELETE_PLANRECEIVE(oOldOrderItem, myTrans)

                    ' --- Update New Item in tb_OrderItem

                    strSQL = " UPDATE tb_OrderItem "
                    strSQL &= " SET "
                    strSQL &= " Order_Index=@Order_Index,Sku_Index=@Sku_Index, "
                    strSQL &= " ItemStatus_Index=@ItemStatus_Index,Package_Index=@Package_Index,Ratio=@Ratio,Qty=@Qty,Total_Qty=@Total_Qty,Plan_Qty=@Plan_Qty,PalletType_Index=@PalletType_Index,"
                    strSQL &= " Pallet_Qty=@Pallet_Qty,Weight=@Weight,Volume=@Volume,IsMfg_Date=@IsMfg_Date,Mfg_Date=@Mfg_Date,IsExp_date=@IsExp_date,Exp_date=@Exp_date, "
                    strSQL &= " Status=@Status,Comment=@Comment,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,Str6=@Str6,Str7=@Str7,Str8=@Str8,Str9=@Str9,Str10=@Str10,ItemDefinition_Index=@ItemDefinition_Index, "
                    strSQL &= " Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,add_by=@add_by,add_branch=@add_branch,Is_SN=@Is_SN,"
                    strSQL &= " Lot_No=@Lot_No,Plot=@Plot,Serial_No=@Serial_No,PO_No=@PO_No,Invoice_No=@Invoice_No,ASN_No=@ASN_No,Declaration_No=@Declaration_No,"
                    'strSQL &= " Item_Qty=@Item_Qty,Qty_Per_Pck=@Qty_Per_Pck,Weight_Per_Pck=@Weight_Per_Pck,Price_Per_Pck=@Price_Per_Pck,Volume_Per_Pck=@Volume_Per_Pck,OrderItem_Price=@OrderItem_Price,Item_Package_Index=@Item_Package_Index,HandlingType_Index=@HandlingType_Index"
                    'include from master site
                    strSQL &= " Plan_Process=@Plan_Process,DocumentPlan_No=@DocumentPlan_No,DocumentPlanItem_Index=@DocumentPlanItem_Index,DocumentPlan_Index=@DocumentPlan_Index,"
                    strSQL &= " Item_Qty=@Item_Qty,Qty_Per_Pck=@Qty_Per_Pck,Weight_Per_Pck=@Weight_Per_Pck,Price_Per_Pck=@Price_Per_Pck,Volume_Per_Pck=@Volume_Per_Pck,OrderItem_Price=@OrderItem_Price,Item_Package_Index=@Item_Package_Index,HandlingType_Index=@HandlingType_Index ,Tax1=@Tax1,Tax2=@Tax2,Tax3=@Tax3,Tax4=@Tax4,Tax5=@Tax5,HS_Code=@HS_Code,ItemDescription=@ItemDescription,Seq=@Seq,Consignee_Index=@Consignee_Index,ERP_Location=@ERP_Location"
                    strSQL &= " WHERE OrderItem_Index=@OrderItem_Index "

                Else
                    ' --- Insert New Item in tb_OrderItem
                    strSQL = " INSERT INTO tb_OrderItem ( OrderItem_Index,Order_Index,Sku_Index, "
                    strSQL &= " ItemStatus_Index,Package_Index,Ratio,Qty,Total_Qty,Plan_Qty,PalletType_Index,"
                    strSQL &= " Pallet_Qty,Weight,Volume,IsMfg_Date,Mfg_Date,IsExp_date,Exp_date, "
                    strSQL &= " Status,Comment,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,ItemDefinition_Index,"
                    strSQL &= " Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Is_SN,"
                    strSQL &= " Lot_No,Plot,Serial_No,PO_No,Invoice_No,ASN_No,Declaration_No,"
                    'strSQL &= " Item_Qty,Qty_Per_Pck,Weight_Per_Pck,Price_Per_Pck,Volume_Per_Pck,OrderItem_Price,Item_Package_Index,HandlingType_Index"
                    'include from master site
                    strSQL &= " Plan_Process,DocumentPlan_No,DocumentPlanItem_Index,DocumentPlan_Index,"
                    strSQL &= " Item_Qty,Qty_Per_Pck,Weight_Per_Pck,Price_Per_Pck,Volume_Per_Pck,OrderItem_Price,Item_Package_Index,HandlingType_Index,Tax1,Tax2,Tax3,Tax4,Tax5,HS_Code,ItemDescription,Seq,Consignee_Index,ERP_Location"
                    strSQL &= " ) "
                    strSQL &= " Values "
                    strSQL &= "( @OrderItem_Index,@Order_Index,@Sku_Index, "
                    strSQL &= " @ItemStatus_Index,@Package_Index,@Ratio,@Qty,@Total_Qty,@Plan_Qty,@PalletType_Index,"
                    strSQL &= " @Pallet_Qty,@Weight,@Volume,@IsMfg_Date,@Mfg_Date,@IsExp_date,@Exp_date, "
                    strSQL &= " @Status,@Comment,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@ItemDefinition_Index, "
                    strSQL &= " @Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Is_SN,"
                    strSQL &= " @Lot_No,@Plot,@Serial_No,@PO_No,@Invoice_No,@ASN_No,@Declaration_No,"
                    'strSQL &= " @Item_Qty,@Qty_Per_Pck,@Weight_Per_Pck,@Price_Per_Pck,@Volume_Per_Pck,@OrderItem_Price,@Item_Package_Index,@HandlingType_Index"
                    'include from master site
                    strSQL &= " @Plan_Process,@DocumentPlan_No,@DocumentPlanItem_Index,@DocumentPlan_Index,"
                    strSQL &= " @Item_Qty,@Qty_Per_Pck,@Weight_Per_Pck,@Price_Per_Pck,@Volume_Per_Pck,@OrderItem_Price,@Item_Package_Index,@HandlingType_Index,@Tax1,@Tax2,@Tax3,@Tax4,@Tax5,@HS_Code,@ItemDescription,@Seq,@Consignee_Index,@ERP_Location"
                    strSQL &= " ) "

                End If

                With SQLServerCommand
                    .Parameters.Clear()

                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem.OrderItem_Index
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objItem.Order_Index
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 50).Value = _objItem.ItemStatus_Index
                    .Parameters.Add("@IsMfg_Date", SqlDbType.Bit, 1).Value = _objItem.IsMfg_Date
                    .Parameters.Add("@Mfg_Date", SqlDbType.SmallDateTime, 4).Value = _objItem.Mfg_Date
                    .Parameters.Add("@IsExp_Date", SqlDbType.Bit, 1).Value = _objItem.IsExp_Date
                    .Parameters.Add("@Exp_Date", SqlDbType.SmallDateTime, 4).Value = _objItem.Exp_date
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                    .Parameters.Add("@Comment", SqlDbType.NVarChar, 255).Value = _objItem.Comment
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 4000).Value = _objItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objItem.Str5
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objItem.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objItem.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objItem.Str10
                    .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 13).Value = _objItem.ItemDefinition_Index
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objItem.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objItem.Flo5
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    .Parameters.Add("@Is_SN", SqlDbType.Bit, 1).Value = _objItem.Is_SN

                    '--- PCK
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objItem.Sku_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 50).Value = _objItem.Package_Index
                    .Parameters.Add("@Plan_Qty", SqlDbType.Float, 8).Value = _objItem.Plan_Qty
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _objItem.PalletType_Index
                    .Parameters.Add("@Pallet_Qty", SqlDbType.Decimal).Value = _objItem.Pallet_Qty
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = _objItem.Ratio
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = Format(CDbl(_objItem.Qty.ToString), "###0.0000")
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = Format(CDbl(_objItem.Total_Qty.ToString), "###0.0000")
                    .Parameters.Add("@Item_Qty", SqlDbType.Float, 15).Value = _objItem.Item_Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objItem.Volume
                    .Parameters.Add("@OrderItem_Price", SqlDbType.Float, 8).Value = _objItem.OrderItem_Price
                    .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _objItem.Item_Package_Index
                    .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = _objItem.HandlingType_Index

                    '--- Document 
                    .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = _objItem.Lot_No
                    .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = _objItem.Plot
                    .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = _objItem.Serial_No
                    .Parameters.Add("@PO_No", SqlDbType.VarChar, 50).Value = _objItem.PO_No
                    .Parameters.Add("@Invoice_No", SqlDbType.VarChar, 50).Value = _objItem.Invoice_No
                    .Parameters.Add("@ASN_No", SqlDbType.VarChar, 50).Value = _objItem.ASN_No
                    .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 50).Value = _objItem.Declaration_No

                    '--- Per_PCK 
                    .Parameters.Add("@Qty_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Qty_Per_Pck
                    .Parameters.Add("@Weight_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Weight_Per_Pck
                    .Parameters.Add("@Volume_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Volume_Per_Pck
                    .Parameters.Add("@Price_Per_Pck", SqlDbType.Float, 8).Value = _objItem.Price_Per_Pck

                    'tax include from master site
                    .Parameters.Add("@Tax1", SqlDbType.Float, 8).Value = _objItem.Tax1
                    .Parameters.Add("@Tax2", SqlDbType.Float, 8).Value = _objItem.Tax2
                    .Parameters.Add("@Tax3", SqlDbType.Float, 8).Value = _objItem.Tax3
                    .Parameters.Add("@Tax4", SqlDbType.Float, 8).Value = _objItem.Tax4
                    .Parameters.Add("@Tax5", SqlDbType.Float, 8).Value = _objItem.Tax5


                    .Parameters.Add("@HS_Code", SqlDbType.VarChar, 50).Value = _objItem.HS_Code
                    .Parameters.Add("@ItemDescription", SqlDbType.VarChar, 200).Value = _objItem.ItemDescription
                    .Parameters.Add("@Seq", SqlDbType.Int, 4).Value = _objItem.Seq
                    'add new 11-01-2010 Consignee_Index
                    .Parameters.Add("@Consignee_Index", SqlDbType.VarChar, 13).Value = _objItem.Consignee_Index
                    .Parameters.Add("@Plan_Process", SqlDbType.Int, 4).Value = _objItem.Plan_Process
                    .Parameters.Add("@DocumentPlan_No", SqlDbType.VarChar, 50).Value = _objItem.DocumentPlan_No
                    .Parameters.Add("@DocumentPlan_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index
                    .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlanItem_Index
                    .Parameters.Add("@ERP_Location", SqlDbType.NVarChar, 100).Value = _objItem.ERP_Location

                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
                ' updade OrderItem_Index To tb_AssetLocationBalance,tb_AssetTransaction
                strSQL = " UPDATE tb_AssetLocationBalance SET  "
                strSQL &= "OrderItem_Index = '" & _objItem.OrderItem_Index & "'"
                strSQL &= " WHERE Order_Index='" & _objItem.Order_Index & "' AND OrderItem_Index = '" & _objItem.iRow_OrderItem_Index & "'"


                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                strSQL = " UPDATE tb_AssetTransaction SET  "
                strSQL &= " OrderItem_Index = '" & _objItem.OrderItem_Index & "'"
                strSQL &= " WHERE Order_Index='" & _objItem.Order_Index & "' AND OrderItem_Index = '" & _objItem.iRow_OrderItem_Index & "'"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                If _objItem.Plan_Process <> -9 Then
                    StatusWithdraw_Document = _objItem.Plan_Process
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.Packing
                            strSQL = "  UPDATE tb_Packing"
                            strSQL &= " SET Qty_Receive =Qty_Receive+" & _objItem.Total_Qty
                            strSQL &= " , Weight_Sum =Weight_Sum+" & _objItem.Weight
                            strSQL &= " , Volume =Volume+" & _objItem.Volume

                            strSQL &= " WHERE Packing_Index ='" & _objItem.DocumentPlan_Index & "'"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            '--- Update Status_Packing
                            Update_Packing(_objItem.DocumentPlan_Index)

                        Case Withdraw_Document.PO
                            ' PO
                            'Add By : top  Add Date : 16/03/2013
                            UPDATE_ADD_PLANRECEIVE(_objItem, myTrans)
                        Case Withdraw_Document.ASN

                            UPDATE_ADD_PLANRECEIVE(_objItem, myTrans)
                            If Check_ASN(_objItem.Str7) = True Then

                                strSQL = " UPDATE tb_AdvanceShipNotice SET "
                                strSQL &= " Status = 3 , StatusReceive = 0"
                                strSQL &= " WHERE AdvanceShipNotice_Index='" & _objItem.DocumentPlan_Index & "'"

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()
                            End If

                            If Check_OverReceiveASN(_objItem.Str7) = True Then

                                strSQL = " UPDATE tb_AdvanceShipNotice SET "
                                strSQL &= " Status = 3 , StatusReceive = 0"
                                strSQL &= " WHERE AdvanceShipNotice_Index='" & _objItem.DocumentPlan_Index & "'"

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            End If

                        Case Withdraw_Document.Shipment
                            'Shipment
                            strSQL = " UPDATE shtb_ShipmentReceived SET "
                            strSQL &= " Received_Qty = Received_Qty + " & _objItem.Qty
                            strSQL &= " ,Total_Received_Qty = Total_Received_Qty + " & _objItem.Total_Qty
                            strSQL &= " ,Received_Weight = Received_Weight + " & _objItem.Weight
                            '--- Last_Received_Date
                            If Check_LastReceive_Date("shtb_ShipmentReceived", "shtb_ShipmentReceived_Index", _objItem.DocumentPlanItem_Index, "Last_Received_Date", _objHeader.Order_Date.ToString("yyyy/MM/dd")) Then
                                strSQL &= " ,Last_Received_Date='" & _objHeader.Order_Date.ToString("yyyy/MM/dd") & "'"
                            End If
                            strSQL &= " WHERE Sku_Index ='" & _objItem.Sku_Index & "'"
                            strSQL &= " AND Shipment_Index='" & _objItem.DocumentPlanItem_Index & "'"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()


                            strSQL = " UPDATE shtb_Shipment SET "
                            strSQL &= "  StatusReceive = 0"
                            strSQL &= " WHERE Shipment_Index='" & _objItem.DocumentPlan_Index & "'"

                            SetSQLString = strSQL
                            SetCommandType = DBType_SQLServer.enuCommandType.Text
                            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                            EXEC_Command()

                            ' 2010-01-19 TaTa 
                            ' Fix bug  Receive  Shipment

                            If Check_Shipment(_objItem.Str7) = True Then

                                strSQL = " UPDATE shtb_Shipment SET "
                                strSQL &= " Status_Id = 3 , StatusReceive = 0"
                                strSQL &= " WHERE Shipment_Index='" & _objItem.DocumentPlan_Index & "'"

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()
                            End If

                            If Check_OverReceiveShipment(_objItem.Str7) = True Then

                                strSQL = " UPDATE shtb_Shipment SET "
                                strSQL &= " Status_Id = 3 , StatusReceive = 0"
                                strSQL &= " WHERE Shipment_Index='" & _objItem.DocumentPlan_Index & "'"

                                SetSQLString = strSQL
                                SetCommandType = DBType_SQLServer.enuCommandType.Text
                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                EXEC_Command()

                            End If


                    End Select
                End If




            Next


            For Each _objPalletType In _objPalletTypeCollection

                If isExistIDForPalletType(_objPalletType.PalletType_History_Index) = True Then

                    strSQL = " UPDATE tb_PalletType_History "
                    strSQL &= " SET "
                    strSQL &= "PalletType_Index=@PalletType_Index,Process_Id=@Process_Id,"
                    strSQL &= " Order_Index=@Order_Index,Qty_In=@Qty_In,"
                    strSQL &= " Qty_Bal=@Qty_Bal"
                    strSQL &= " WHERE PalletType_History_Index=@PalletType_History_Index "

                Else
                    strSQL = "INSERT INTO tb_PalletType_History "
                    strSQL &= " (PalletType_History_Index,Order_Index,PalletType_Index,Process_Id,Qty_In,Qty_Bal)"
                    strSQL &= " Values "
                    strSQL &= " (@PalletType_History_Index,@Order_Index,@PalletType_Index,@Process_Id,@Qty_In,@Qty_Bal)"
                End If
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = _objPalletType.PalletType_History_Index
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _objPalletType.PalletType_Index
                    .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = _objPalletType.Process_Id
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objHeader.Order_Index
                    .Parameters.Add("@Qty_In", SqlDbType.VarChar, 50).Value = _objPalletType.Qty_In
                    .Parameters.Add("@Qty_Bal", SqlDbType.VarChar, 50).Value = _objPalletType.Qty_Bal

                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next

            strSQL = " UPDATE tb_OrderTruckIn SET  "
            strSQL &= " Order_Index='" & Me._objHeader.Order_Index & "' ,Status_Id =1 "
            strSQL &= " WHERE Order_Index='" & Me._objHeader.Order_Index & "'"
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            strSQL = " UPDATE tb_tag "
            strSQL &= " SET "
            strSQL &= " order_no=@Order_No "
            strSQL &= " WHERE Order_Index=@Order_Index "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objHeader.Order_Index
                .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = _objHeader.Order_No

            End With
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- Commit transaction
            myTrans.Commit()
            Return _objHeader.Order_Index
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try


    End Function


    Public Function InsertOrderItemSku() As Integer
        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try

            strSQL = "Delete from tb_OrderItemSku where OrderItem_index ='" & _Order_ItemIndex & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            For Each _objItemsku In _objOrderItemsku_Collection

                strSQL = " insert into tb_OrderItemSku (OrderItemSku_index,Sku_index,SkuItem_index,OrderItem_index,Package_index,Qty,Weight) "
                strSQL &= " values ('" & _objItemsku.OrderItemSku_index & "'"
                strSQL &= ",'" & _objItemsku.Sku_index & "'"
                strSQL &= ",'" & _objItemsku.SkuItem_index & "'"
                strSQL &= ",'" & _objItemsku.OrderItem_index & "'"
                strSQL &= ",'" & _objItemsku.Package_index & "'"
                strSQL &= "," & _objItemsku.Qty & ""
                strSQL &= "," & _objItemsku.Weight & ""
                strSQL &= ")"

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next


            ' --- Commit transaction
            myTrans.Commit()
            Return 1
        Catch e As Exception
            Try
                myTrans.Rollback()
                Throw e
                Return -1
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try

        Return ""

    End Function

    'BEGIN Add By Top: 16/03/2013
    Sub UPDATE_DELETE_PLANRECEIVE(ByVal poOrderItem As tb_OrderItem, ByVal myTrans As SqlClient.SqlTransaction)

        Dim strSQL As String = ""
        Dim boolNoTran As Boolean = False
        If myTrans Is Nothing Then
            boolNoTran = True
            connectDB()
            myTrans = Connection.BeginTransaction()
            SQLServerCommand.Transaction = myTrans
        End If

        Try
            StatusWithdraw_Document = poOrderItem.Plan_Process
            Select Case StatusWithdraw_Document
                Case Withdraw_Document.PO
                    ' UPDATE QTY PO
                    strSQL = " UPDATE tb_PurchaseOrderItem SET "
                    '  strSQL &= " Received_Qty = Received_Qty - @Received_Qty"
                    strSQL &= " Total_Received_Qty = Total_Received_Qty - @Total_Received_Qty "
                    strSQL &= " ,Received_Qty = convert(decimal(19,6), Total_Received_Qty / ratio) "

                    strSQL &= " ,Received_Weight = Received_Weight - @Received_Weight"
                    strSQL &= " ,Received_Volume = Received_Volume - @Received_Volume"
                    strSQL &= " WHERE PurchaseOrderItem_Index= @PurchaseOrderItem_Index"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlanItem_Index
                        ' .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Qty
                        .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Total_Qty
                        .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = poOrderItem.Weight
                        .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = poOrderItem.Volume
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()



                Case Withdraw_Document.Packing
                    strSQL = "  UPDATE tb_Packing"
                    strSQL &= " SET Qty_Receive =Qty_Receive-@Qty_Receive"
                    strSQL &= " , Weight_Receive =Weight_Receive-@Weight_Receive"
                    strSQL &= " , Volume_Receive =Volume_Receive-@Volume_Receive"
                    strSQL &= " WHERE Packing_Index =@Packing_Index"
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Packing_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                        .Parameters.Add("@Qty_Receive", SqlDbType.Float, 8).Value = poOrderItem.Total_Qty
                        .Parameters.Add("@Weight_Receive", SqlDbType.Float, 8).Value = poOrderItem.Weight
                        .Parameters.Add("@Volume_Receive", SqlDbType.Float, 8).Value = poOrderItem.Volume
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                Case Withdraw_Document.ASN
                    ' UPDATE QTY ASN
                    strSQL = " UPDATE tb_AdvanceShipNoticeItem SET "
                    strSQL &= " Received_Qty = Received_Qty - @Received_Qty"
                    strSQL &= " ,Total_Received_Qty = Total_Received_Qty - @Total_Received_Qty"
                    strSQL &= " ,Received_Weight = Received_Weight - @Received_Weight"
                    strSQL &= " ,Received_Net_Weight = Received_Net_Weight - @Received_Net_Weight"
                    strSQL &= " ,Received_Volume = Received_Volume - @Received_Volume"
                    strSQL &= " WHERE AdvanceShipNoticeItem_Index= @AdvanceShipNoticeItem_Index"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@AdvanceShipNoticeItem_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlanItem_Index
                        .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Qty
                        .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Total_Qty
                        .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = poOrderItem.Weight
                        .Parameters.Add("@Received_Net_Weight", SqlDbType.Float, 8).Value = poOrderItem.Flo1
                        .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = poOrderItem.Volume
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()



            End Select

            UPDATE_STATUS_PLANRECEIVE(poOrderItem, myTrans)

            If boolNoTran Then
                myTrans.Commit()
            End If

        Catch ex As Exception
            If boolNoTran Then
                myTrans.Rollback()
            End If
            Throw ex
        End Try
    End Sub

    Sub UPDATE_ADD_PLANRECEIVE(ByVal poOrderItem As tb_OrderItem, ByVal myTrans As SqlClient.SqlTransaction)

        Dim strSQL As String = ""
        Dim boolNoTran As Boolean = False
        If myTrans Is Nothing Then
            boolNoTran = True
            connectDB()
            myTrans = Connection.BeginTransaction()
            SQLServerCommand.Transaction = myTrans
        End If
        Try
            StatusWithdraw_Document = poOrderItem.Plan_Process
            Select Case StatusWithdraw_Document
                Case Withdraw_Document.ASN
                    ' UPDATE QTY ASN
                    strSQL = " UPDATE tb_AdvanceShipNoticeItem SET "
                    strSQL &= " Received_Qty = Received_Qty + @Received_Qty"
                    strSQL &= " ,Total_Received_Qty = Total_Received_Qty + @Total_Received_Qty"
                    strSQL &= " ,Received_Weight = Received_Weight + @Received_Weight"
                    strSQL &= " ,Received_Net_Weight = Received_Net_Weight + @Received_Net_Weight"
                    strSQL &= " ,Received_Volume = Received_Volume + @Received_Volume"
                    strSQL &= " ,Last_Received_Date = @Last_Received_Date"
                    strSQL &= " WHERE AdvanceShipNoticeItem_Index= @AdvanceShipNoticeItem_Index"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@AdvanceShipNoticeItem_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlanItem_Index
                        .Parameters.Add("@Last_Received_Date", SqlDbType.SmallDateTime, 4).Value = _objItem.Add_date.ToString("yyyy/MM/dd")
                        .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Qty
                        .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Total_Qty
                        .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = poOrderItem.Weight
                        .Parameters.Add("@Received_Net_Weight", SqlDbType.Float, 8).Value = poOrderItem.Flo1
                        .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = poOrderItem.Volume
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                Case Withdraw_Document.PO
                    ' UPDATE QTY PO

                    strSQL = " UPDATE tb_PurchaseOrderItem SET "
                    strSQL &= " Received_Qty = Received_Qty + @Received_Qty"
                    strSQL &= " ,Total_Received_Qty = Total_Received_Qty + @Total_Received_Qty"
                    strSQL &= " ,Received_Weight = Received_Weight + @Received_Weight"
                    strSQL &= " ,Received_Volume = Received_Volume + @Received_Volume"
                    strSQL &= " ,Last_Received_Date = @Last_Received_Date"
                    strSQL &= " WHERE PurchaseOrderItem_Index= @PurchaseOrderItem_Index"

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlanItem_Index
                        .Parameters.Add("@Last_Received_Date", SqlDbType.SmallDateTime, 4).Value = _objItem.Add_date.ToString("yyyy/MM/dd")
                        .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Qty
                        .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = poOrderItem.Total_Qty
                        .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = poOrderItem.Weight
                        .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = poOrderItem.Volume
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    strSQL = " UPDATE tb_PurchaseOrder SET "
                    strSQL &= " StatusReceive = 0"
                    strSQL &= " WHERE PurchaseOrder_Index='" & _objItem.DocumentPlan_Index & "'"

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()

                Case Withdraw_Document.Packing
                    strSQL = "  UPDATE tb_Packing"
                    strSQL &= " SET Qty_Receive =Qty_Receive+@Qty_Receive"
                    strSQL &= " , Weight_Receive =Weight_Receive+@Weight_Receive"
                    strSQL &= " , Volume_Receive =Volume_Receive+@Volume_Receive"
                    strSQL &= " WHERE Packing_Index =@Packing_Index"
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Packing_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                        .Parameters.Add("@Qty_Receive", SqlDbType.Float, 8).Value = poOrderItem.Total_Qty
                        .Parameters.Add("@Weight_Receive", SqlDbType.Float, 8).Value = poOrderItem.Weight
                        .Parameters.Add("@Volume_Receive", SqlDbType.Float, 8).Value = poOrderItem.Volume
                    End With
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
            End Select

            UPDATE_STATUS_PLANRECEIVE(poOrderItem, myTrans)

            If boolNoTran Then
                myTrans.Commit()
            End If

        Catch ex As Exception
            If boolNoTran Then
                myTrans.Rollback()
            End If
            Throw ex
        End Try
    End Sub

    Sub UPDATE_STATUS_PLANRECEIVE(ByVal poOrderItem As tb_OrderItem, ByVal myTrans As SqlClient.SqlTransaction)

        Dim strSQL As String = ""
        Dim boolNoTran As Boolean = False
        If myTrans Is Nothing Then
            boolNoTran = True
            connectDB()
            myTrans = Connection.BeginTransaction()
            SQLServerCommand.Transaction = myTrans
        End If




        Try
            StatusWithdraw_Document = poOrderItem.Plan_Process
            Select Case StatusWithdraw_Document
                Case Withdraw_Document.PO

                    'HARD UPDATE STATUS
                    strSQL = " SELECT SUM(isnull(Total_Received_Qty,0)) as Total_Received_Qty ,SUM(isnull(Total_Qty,0)) as Total_Qty"
                    strSQL &= " FROM tb_PurchaseOrderItem  "
                    strSQL &= " WHERE PurchaseOrder_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "POSTATUS")

                    Dim dblTotal_Received_Qty As Double = 0
                    Dim dblTotal_Qty As Double = 0
                    dblTotal_Received_Qty = Val(DSSO.Tables("POSTATUS").Rows(0).Item("Total_Received_Qty").ToString)
                    dblTotal_Qty = Val(DSSO.Tables("POSTATUS").Rows(0).Item("Total_Qty").ToString)
                    If dblTotal_Received_Qty >= dblTotal_Qty Then 'เสร็จสิ้น
                        strSQL = " UPDATE tb_PurchaseOrder SET "
                        strSQL &= " Status = 3 , StatusReceive = 0"
                        strSQL &= " WHERE PurchaseOrder_Index=@PlanDocument_Index"
                        SQLServerCommand.Parameters.Clear()
                        SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Else 'ค้างรับ
                        strSQL = " UPDATE tb_PurchaseOrder SET "
                        strSQL &= " Status = 2 , StatusReceive = 0"
                        strSQL &= " WHERE PurchaseOrder_Index=@PlanDocument_Index"
                        SQLServerCommand.Parameters.Clear()
                        SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If

                Case Withdraw_Document.Packing
                    '--- Update Status_Packing
                    strSQL = "SELECT isnull(Qty_Product,0) as Total_Qty ,isnull(Qty_Receive,0) as Received_Qty FROM tb_Packing WHERE Packing_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSPACK As New DataSet
                    DataAdapter.Fill(DSPACK, "PACKSTATUS")

                    Dim dblReceived_Qty As Double = 0
                    Dim dblTotal_Qty As Double = 0
                    dblReceived_Qty = CDbl(DSPACK.Tables("PACKSTATUS").Rows(0).Item("Received_Qty").ToString)
                    dblTotal_Qty = CDbl(DSPACK.Tables("PACKSTATUS").Rows(0).Item("Total_Qty").ToString)

                    If dblReceived_Qty = 0 Then 'รอรับสินค้า
                        strSQL = "  UPDATE tb_Packing"
                        strSQL &= " SET Status =5"
                        strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    ElseIf dblReceived_Qty >= dblTotal_Qty Then 'รับสินค้าแล้ว
                        strSQL = "  UPDATE tb_Packing"
                        strSQL &= " SET Status =3"
                        strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    ElseIf dblReceived_Qty <= dblTotal_Qty Then 'รับสินค้าบางส่วน
                        strSQL = "  UPDATE tb_Packing"
                        strSQL &= " SET Status =4"
                        strSQL &= " WHERE Packing_Index =@PlanDocument_Index"
                    End If
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                Case Withdraw_Document.ASN
                    'HARD UPDATE STATUS
                    strSQL = " SELECT SUM(isnull(Received_Qty,0)) as Received_Qty ,SUM(isnull(Total_Qty,0)) as Total_Qty"
                    strSQL &= " FROM tb_AdvanceShipNoticeItem  "
                    strSQL &= " WHERE AdvanceShipNotice_Index =@PlanDocument_Index"
                    SQLServerCommand.Parameters.Clear()
                    SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = poOrderItem.DocumentPlan_Index
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = strSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    Dim DSSO As New DataSet
                    DataAdapter.Fill(DSSO, "ASNSTATUS")

                    Dim dblReceived_Qty As Double = 0
                    Dim dblTotal_Qty As Double = 0
                    dblReceived_Qty = Val(DSSO.Tables("ASNSTATUS").Rows(0).Item("Received_Qty").ToString)
                    dblTotal_Qty = Val(DSSO.Tables("ASNSTATUS").Rows(0).Item("Total_Qty").ToString)
                    If dblReceived_Qty >= dblTotal_Qty Then 'เสร็จสิ้น
                        strSQL = " UPDATE tb_AdvanceShipNotice SET "
                        strSQL &= " Status = 3 , StatusReceive = 0"
                        strSQL &= " WHERE AdvanceShipNotice_Index=@PlanDocument_Index"
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Else 'ค้างรับ
                        strSQL = " UPDATE tb_AdvanceShipNotice SET "
                        strSQL &= " Status = 2 , StatusReceive = 0"
                        strSQL &= " WHERE AdvanceShipNotice_Index=@PlanDocument_Index"
                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    End If
            End Select

            If boolNoTran Then
                myTrans.Commit()
            End If

        Catch ex As Exception
            If boolNoTran Then
                myTrans.Rollback()
            End If
            Throw ex
        End Try
    End Sub
    'END  Add By Top: 16/03/2013
    Sub Update_Packing(ByVal Packing_Index As String)
        Dim strSQL As String = ""
        Try
            If checkPackingQty(Packing_Index) <= 0 Then
                strSQL = "  UPDATE tb_Packing"
                strSQL &= " SET Status =3"
                strSQL &= " WHERE Packing_Index ='" & Packing_Index & "'"
            Else
                strSQL = "  UPDATE tb_Packing"
                strSQL &= " SET Status =4"
                strSQL &= " WHERE Packing_Index ='" & Packing_Index & "'"
            End If
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Sub


    Function UpdateRemark(ByVal OrderIndex As String, ByVal OrderItemIndex As String, ByVal Remark As String)
        Dim strSQL As String = ""
        Dim StrOrder As String = OrderIndex
        Try
            connectDB()
            Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
            SQLServerCommand.Transaction = myTrans


            strSQL = " UPDATE tb_OrderItem "
            strSQL &= " SET  Str3= '" & Remark & "' "
            strSQL &= " WHERE Order_Index = '" & OrderIndex & "' And OrderItem_Index='" & OrderItemIndex & "' "

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()
            Return StrOrder
        Catch ex As Exception
            Throw ex
        Finally

        End Try
    End Function

#End Region

#Region " DELETE "
    Public Function Delete_OrderItem_Ref_PO_After_Save(ByVal pstrOrderItem_Index As String, ByVal intRows As Integer, Optional ByVal pstrOrder_index As String = "") As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim odtOrderItem As New DataTable

            strSQL = " select Qty,Total_Qty,Order_Index,Plan_Process,DocumentPlan_No,DocumentPlan_Index,DocumentPlanItem_Index "
            strSQL &= " from tb_OrderItem "
            strSQL &= " WHERE OrderItem_Index ='" & pstrOrderItem_Index & "'"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            strSQL = Nothing

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            DS = New DataSet
            DataAdapter.Fill(DS, "tbDel")

            For i As Integer = 0 To DS.Tables("tbDel").Rows.Count - 1
                _objItem.Qty = DS.Tables("tbDel").Rows(i).Item("Qty").ToString
                _objItem.Total_Qty = DS.Tables("tbDel").Rows(i).Item("Total_Qty").ToString
                _objItem.Order_Index = DS.Tables("tbDel").Rows(i).Item("Order_Index").ToString
                _objItem.Plan_Process = DS.Tables("tbDel").Rows(i).Item("Plan_Process").ToString
                _objItem.DocumentPlan_No = DS.Tables("tbDel").Rows(i).Item("DocumentPlan_No").ToString
                _objItem.DocumentPlan_Index = DS.Tables("tbDel").Rows(i).Item("DocumentPlan_Index").ToString
                _objItem.DocumentPlanItem_Index = DS.Tables("tbDel").Rows(i).Item("DocumentPlanItem_Index").ToString
            Next

            If intRows <> 1 Then


                strSQL = "  DELETE FROM tb_OrderItem "
                strSQL &= " WHERE OrderItem_Index ='" & pstrOrderItem_Index & "' "

                connectDB()
                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                'คืนค่า PO


                If _objItem.Plan_Process <> -9 Then
                    StatusWithdraw_Document = _objItem.Plan_Process
                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.PO
                            strSQL = "   SELECT  * "
                            strSQL &= "   FROM  tb_PurchaseOrderItem   "
                            strSQL &= " WHERE  PurchaseOrderItem_Index='" & _objItem.DocumentPlanItem_Index & "'"

                            With SQLServerCommand
                                .Connection = Connection
                                .Transaction = myTrans
                                .CommandText = strSQL
                                .CommandTimeout = 0
                            End With

                            strSQL = Nothing

                            DataAdapter.SelectCommand = SQLServerCommand
                            DataAdapter.SelectCommand.Transaction = myTrans

                            DataAdapter.Fill(DS, "POI")
                            For i As Integer = 0 To DS.Tables("tbDel").Rows.Count - 1
                                If DS.Tables("POI").Rows.Count <> 0 Then
                                    strSQL = " UPDATE tb_PurchaseOrderItem SET "
                                    strSQL &= "Total_Received_Qty = " & Val(DS.Tables("POI").Rows(i).Item("Total_Received_Qty").ToString) & " - " & _objItem.Total_Qty
                                    strSQL &= ",Received_Qty= " & Val(DS.Tables("POI").Rows(i).Item("Received_Qty").ToString) & " - " & _objItem.Qty
                                    strSQL &= " WHERE PurchaseOrderItem_Index = '" & DS.Tables("POI").Rows(i).Item("PurchaseOrderItem_Index").ToString & "'"



                                    SetSQLString = strSQL
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                    strSQL = Nothing

                                    Dim str_Order As String = ""
                                    str_Order = " UPDATE tb_PurchaseOrder SET "
                                    str_Order &= "Status = 2 , StatusReceive = -2"
                                    str_Order &= " WHERE PurchaseOrder_Index = '" & DS.Tables("POI").Rows(i).Item("PurchaseOrder_Index").ToString & "'"

                                    SetSQLString = str_Order
                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                                    EXEC_Command()
                                    str_Order = Nothing
                                End If
                            Next

                    End Select
                End If


            Else
                Cancel_Order(pstrOrder_index)
            End If


            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
    Public Function Delete_QTY_PO(ByVal Plan_Process As String, ByVal DocumentPlanItem_Index As String, ByVal DocumentPlan_Index As String, ByVal Qty As String) As Boolean
        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try



            Select Case Plan_Process
                Case "9"
                    strSQL = ""
                    strSQL += "update tb_PurchaseOrderitem "
                    strSQL += " SET  Received_Qty = Received_Qty - " & Qty
                    strSQL += " where PurchaseOrderItem_Index = '" & DocumentPlanItem_Index & "'"
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    strSQL = ""
                    strSQL += "update tb_PurchaseOrder "
                    strSQL += " SET  Status = 2"
                    strSQL += " where PurchaseOrder_Index = '" & DocumentPlan_Index & "'"
                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                Case "16"
                    strSQL = ""
            End Select

            myTrans.Commit()
            Return True

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function
    Public Function Delete_OrderItem(ByVal OrderItem_Index As String, _
                                            Optional ByVal Connect As SqlClient.SqlConnection = Nothing, _
                                          Optional ByVal myTrans As SqlClient.SqlTransaction = Nothing) As Boolean

        Dim strSQL As String = ""
        Dim isTran As Boolean = True
        Try


            If myTrans Is Nothing Then
                connectDB()
                Connect = Connection
                myTrans = Connection.BeginTransaction
                SQLServerCommand.Transaction = myTrans
                isTran = False
            End If



            DBExeNonQuery(String.Format(" sp_Delete_OrderItem '{0}','{1}' ", OrderItem_Index, WV_UserName), Connect, myTrans)




            If isTran = False Then
                myTrans.Commit()
                disconnectDB()
            End If

            Return True
        Catch ex As Exception
            If isTran = False Then
                myTrans.Rollback()
                disconnectDB()
            End If
            Throw ex
        End Try


    End Function
    'Public Function Delete_OrderItem(ByVal OrderItem_Index As String) As Boolean
    '    Dim strSQL As String = ""
    '    Try
    '        strSQL = "DELETE FROM tb_OrderItem "
    '        strSQL &= " WHERE OrderItem_Index ='" & OrderItem_Index & "' "

    '        connectDB()
    '        SetSQLString = strSQL
    '        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '        EXEC_Command()

    '        Return True
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try

    'End Function

    Public Sub Delete_OrderTransaction(ByVal File_Name As String)

        Dim Order As String = ""
        Dim OrderItem As String = ""
        Dim strSQL As String = ""


        Dim odtTransferStatusLocation As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try

            Order = "DELETE FROM tb_Order  "
            Order &= " WHERE str10 ='" & File_Name & "' "

            connectDB()
            SetSQLString = Order
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            OrderItem = "DELETE FROM tb_OrderItem  "
            OrderItem &= " WHERE str10 ='" & File_Name & "' "

            connectDB()
            SetSQLString = OrderItem
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            myTrans.Commit()

        Catch e As Exception
            Try
                myTrans.Rollback()

                Throw e
            Catch ex As SqlClient.SqlException
                If Not myTrans.Connection Is Nothing Then
                    Throw ex
                End If
            End Try
        Finally
            disconnectDB()
        End Try
    End Sub

#End Region

#Region " GET ORDER HEADER & GET ORDER ITEM "
    ''' <summary>
    ''' Get Order from Order Index
    ''' </summary>
    ''' <param name="Order_Index"></param>
    ''' <remarks></remarks>
    Public Sub getOrderHeader(ByVal Order_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'ISNULL(Title, '')
            strSQL = " SELECT     * "
            strSQL &= " FROM    VIEW_OrderHeder"
            strWhere = " WHERE Order_Index ='" & Order_Index & "' "

            strSQL = strSQL + strWhere

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


    Public Sub getOrderHeaderReturn(ByVal Order_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'ISNULL(Title, '')
            strSQL = " SELECT     * "
            strSQL &= " FROM    VIEW_OrderHederReturn"
            strWhere = " WHERE Order_Index ='" & Order_Index & "' "

            strSQL = strSQL + strWhere

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


    Public Function GetDocumentType_Ref(ByVal No As String) As DataTable
        Try
            Dim strSQL As String = ""
            'strSQL = "  SELECT * FROM ms_DocumentType WHERE DocumentType_Index IN ("
            'strSQL += "  SELECT Ref_DocumentType_Index FROM ms_DocumentType WHERE DocumentType_Index = '" & No & "') "

            strSQL = "SELECT * FROM ms_DocumentType WHERE DocumentType_Index IN ( SELECT t.Ref_DocumentType_Index FROM  tb_PurchaseOrder p "
            strSQL += " INNER JOIN ms_DocumentType t ON t.DocumentType_Index = p.DocumentType_Index "
            strSQL += " WHERE p.PurchaseOrder_No = '" & No & "')"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            disconnectDB()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Sub getOrderReturn(ByVal PLOT As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'ISNULL(Title, '')
            strSQL = " SELECT     * "
            strSQL &= " FROM    VIEW_TFAC_OrderReturn"
            strWhere = " WHERE PLOT ='" & PLOT & "' "

            strSQL = strSQL + strWhere

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

    ''' <summary>
    ''' Get Data Table for Order Item from Order Index
    ''' </summary>
    ''' <param name="Order_Index"></param>
    ''' <remarks></remarks>
    Public Sub getOrderItemDetail(ByVal Order_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = "SELECT * "
            strSQL &= "  FROM VIEW_OrderDetail"

            strWhere = " WHERE Order_Index ='" & Order_Index & "' AND Status <> -1"
            strWhere &= " order by OrderItem_Index asc"

            strSQL = strSQL + strWhere

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

    Public Sub getOrderItem(ByVal Order_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = "SELECT * "
            strSQL &= "  FROM VIEW_OrderDetail"

            strWhere = " WHERE Order_Index ='" & Order_Index & "' "
            strWhere &= " order by Seq ,OrderItem_Index asc"

            strSQL = strSQL + strWhere

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

    Public Sub getOrderDetail_ConfrimPutAway(ByVal Order_Index As String)
        '  
        Dim strSQL As String = ""

        Try
            'strSQL = "SELECT    tb_Order.Order_No,tb_Order.Order_Date,tb_Order.Order_Time,tb_Order.Departure_Date,tb_Order.Arrival_Date,tb_Order.Ref_No1, "
            'strSQL &= "         tb_Order.Ref_No2,tb_Order.Ref_No3,tb_Order.Ref_No4,tb_Order.Ref_No5,tb_Order.Customer_Index, "
            'strSQL &= "         tb_Order.Consignee_Index AS Consignee_Index_Header,tb_Order.Supplier_Index,tb_Order.Department_Index,tb_Order.DocumentType_Index,tb_OrderItem.*"
            'strSQL &= " FROM    tb_Order INNER JOIN"
            'strSQL &= "         tb_OrderItem ON tb_Order.Order_Index =tb_OrderItem.Order_Index "
            'If Order_Index <> "" Then
            '    strSQL &= " where tb_Order.Order_Index ='" & Order_Index & "' "
            'End If

            ' เปลี่ยน Query ให้หาเฉพาะ orderitem ที่ยังไม่ได้ สร้าง orderitemLocation
            strSQL = "SELECT    tb_Order.Order_No,tb_Order.Order_Date,tb_Order.Order_Time,tb_Order.Departure_Date,tb_Order.Arrival_Date,tb_Order.Ref_No1, "
            strSQL &= "         tb_Order.Ref_No2,tb_Order.Ref_No3,tb_Order.Ref_No4,tb_Order.Ref_No5,tb_Order.Customer_Index, "
            strSQL &= "         tb_Order.Consignee_Index AS Consignee_Index_Header,tb_Order.Supplier_Index,tb_Order.Department_Index,tb_Order.DocumentType_Index,tb_OrderItem.*"
            strSQL &= "         ,isnull(SOIL_QTY,0) SOIL_QTY,isnull(SOIL_TotalQTY,0) SOIL_TotalQTY "
            strSQL &= " FROM    tb_Order INNER JOIN"
            strSQL &= "         tb_OrderItem ON tb_Order.Order_Index =tb_OrderItem.Order_Index "
            strSQL &= "          left outer join (select sum(qty) SOIL_QTY,sum(total_qty)SOIL_TotalQTY,orderitem_index from tb_orderitemlocation where Status <> -1 group by orderitem_index) as SOIL      "
            strSQL &= "           	on SOIL.orderitem_index=tb_OrderItem.orderitem_index "
            strSQL &= "  WHERE   total_qty-isnull(SOIL_TotalQTY,0)>0  "
            If Order_Index <> "" Then
                strSQL &= " AND tb_Order.Order_Index ='" & Order_Index & "' "
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
#End Region

#Region " CHECK DATA "
    ''' <summary>
    ''' Check if the selected order item exists.
    ''' </summary>
    ''' <param name="Packing_Index"></param>
    ''' <returns>True of False</returns>
    ''' <remarks></remarks>
    ''' 

    Private Function checkPackingQty(ByVal Packing_Index As String) As Double
        Dim strSQL As String
        Try
            strSQL = "SELECT (Qty_Product-Qty_Receive) as Qty_Bal FROM tb_Packing WHERE Packing_Index = '" & Packing_Index & "'  "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return _scalarOutput
            Else
                Return _scalarOutput
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    ''' <summary>
    ''' Check if the selected order item exists.
    ''' </summary>
    ''' <param name="OrderItem_Index"></param>
    ''' <returns>True of False</returns>
    ''' <remarks></remarks>
    ''' 

    Private Function isExistID(ByVal OrderItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_OrderItem WHERE OrderItem_Index = @OrderItem_Index  "
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index

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

    Private Function Check_PO(ByVal PurchaseOrder_Index As String) As Boolean
        Dim strSQL As String
        Try

            ' strSQL = "SELECT sum(Total_Qty) = sum(Total_Received_Qty) FROM tb_PurchaseOrderItem WHERE PurchaseOrder_Index = @PurchaseOrder_Index  "
            strSQL = " SELECT  count(*) as gg"
            strSQL &= " FROm tb_PurchaseOrderItem "
            strSQL &= " group by PurchaseOrder_Index"
            strSQL &= " Having PurchaseOrder_Index = '" & PurchaseOrder_Index & "' and (sum(Total_Qty) - sum(Total_Received_Qty)) = 0 "

            'SQLServerCommand.Parameters.Clear()
            'SQLServerCommand.Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            ' connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput = Nothing Then
                _scalarOutput = ""
            End If

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Function Check_OverReceive(ByVal PurchaseOrder_Index As String) As Boolean
        Dim strSQL As String
        Try


            strSQL = " SELECT  count(*) as gg"
            strSQL &= " FROm tb_PurchaseOrderItem "
            strSQL &= " group by PurchaseOrder_Index"
            strSQL &= " Having PurchaseOrder_Index = '" & PurchaseOrder_Index & "' and ( sum(Qty)-sum(Received_Qty) ) < = 0"
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput = Nothing Then
                _scalarOutput = ""
            End If

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Private Function Check_ASN(ByVal AdvanceShipNotice_Index As String) As Boolean
        Dim strSQL As String
        Try

            ' strSQL = "SELECT sum(Total_Qty) = sum(Total_Received_Qty) FROM tb_PurchaseOrderItem WHERE PurchaseOrder_Index = @PurchaseOrder_Index  "
            strSQL = " SELECT  count(*) as gg"
            strSQL &= " FROM tb_AdvanceShipNoticeItem "
            strSQL &= " group by AdvanceShipNotice_Index"
            strSQL &= " Having AdvanceShipNotice_Index = '" & AdvanceShipNotice_Index & "' and (sum(Total_Qty) - sum(Total_Received_Qty)) = 0 "

            'SQLServerCommand.Parameters.Clear()
            'SQLServerCommand.Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput = Nothing Then
                _scalarOutput = ""
            End If

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function



    Private Function Check_OverReceiveASN(ByVal AdvanceShipNotice_Index As String) As Boolean
        Dim strSQL As String
        Try


            strSQL = " SELECT  count(*) as gg"
            strSQL &= " FROM tb_AdvanceShipNoticeItem "
            strSQL &= " group by AdvanceShipNotice_Index"
            strSQL &= " Having AdvanceShipNotice_Index = '" & AdvanceShipNotice_Index & "' and ( sum(Qty)-sum(Received_Qty) ) < = 0"
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput = Nothing Then
                _scalarOutput = ""
            End If

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function Check_Shipment(ByVal Shipment_Index As String) As Boolean
        Dim strSQL As String
        Try

            ' strSQL = "SELECT sum(Total_Qty) = sum(Total_Received_Qty) FROM tb_PurchaseOrderItem WHERE PurchaseOrder_Index = @PurchaseOrder_Index  "
            strSQL = " SELECT  count(*) as gg"
            strSQL &= " FROM shtb_ShipmentReceived "
            strSQL &= " group by Shipment_Index"
            strSQL &= " Having Shipment_Index = '" & Shipment_Index & "' and (sum(Total_Qty) - sum(Total_Received_Qty)) = 0 "

            'SQLServerCommand.Parameters.Clear()
            'SQLServerCommand.Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            '  connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput = Nothing Then
                _scalarOutput = ""
            End If

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function Check_OverReceiveShipment(ByVal Shipment_Index As String) As Boolean
        Dim strSQL As String
        Try


            strSQL = " SELECT  count(*) as gg"
            strSQL &= " FROM shtb_ShipmentReceived "
            strSQL &= " group by Shipment_Index"
            strSQL &= " Having Shipment_Index = '" & Shipment_Index & "' and ( sum(Qty)-sum(Received_Qty) ) < = 0"
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput = Nothing Then
                _scalarOutput = ""
            End If

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

    ''' <summary>
    ''' Check if the selected order item location exists.
    ''' </summary>
    ''' <param name="OrderItem_Index"></param>
    ''' <returns>True of False</returns>
    ''' <remarks></remarks>
    Public Function isItemLocation(ByVal OrderItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_OrderItemLocation WHERE OrderItem_Index = @OrderItem_Index "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
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

    Public Function isItemTAG(ByVal OrderItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_Tag WHERE OrderItem_Index = @OrderItem_Index AND TAG_Status <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
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

    Public Function GetQtyTagInTAG(ByVal OrderItem_Index As String) As Decimal
        Dim strSQL As String
        Try

            strSQL = "SELECT IsNull(Sum(Qty_per_TAG),0) FROM tb_TAG WHERE OrderItem_Index = @OrderItem_Index AND TAG_Status <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index

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

    Public Function CloseDocument(ByVal Order_Index As String) As String
        Try
            Dim StrSQL As New System.Text.StringBuilder
            StrSQL.Append(String.Format(" SELECT Top 1 Status FROM tb_Order Where Order_Index = '{0}' ", Order_Index))
            Dim Current_Status As String = DBExeQuery_Scalar(StrSQL.ToString, eCommandType.Text)
            If Current_Status <> "4" Then
                Return "สามารถทำได้เฉพาะสถานะ จัดเก็บบางส่วนเท่านั้น !!"
            End If

            'Dim objTAG As New WMS_STD_INB_Receive_Datalayer.tb_TAG(WMS_STD_INB_Receive_Datalayer.tb_TAG.enuOperation_Type.SEARCH)
            'objTAG.getOrderDetail_Tag(" AND (Qty > 0) AND Order_Index = '" & Order_Index & "' ")

            'If objTAG.DataTable.Rows.Count > 0 Then
            '    Return "ไม่สามารถปิดเอกสารได้ เนื่องจากยังสร้าง TAG ไม่หมด  !!"
            'End If

            'StrSQL = New System.Text.StringBuilder
            'StrSQL.Append(String.Format(" SELECT COUNT(TAG_Index) FROM tb_Tag WHERE Order_Index = '{0}' AND TAG_Status = 1 ", Order_Index))
            'Dim CountTagNotReceive As Decimal = DBExeQuery_Scalar(StrSQL.ToString, eCommandType.Text)
            'If CountTagNotReceive > 0 Then
            '    Return "ไม่สามารถปิดเอกสารได้ เนื่องจากยังมี TAG ที่ยังไม่ได้จัดเก็บ  !!"
            'End If

            StrSQL = New System.Text.StringBuilder
            StrSQL.Append(String.Format("Update tb_Order Set Status = 2 where Order_Index = '{0}' AND Status = 4 ", Order_Index))
            StrSQL.Append(String.Format("Update tb_TAG Set TAG_Status = -1 where Order_Index = '{0}' AND TAG_Status = 1 ", Order_Index))
            DBExeNonQuery(StrSQL.ToString, eCommandType.Text)

            Return ""
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function isItemSerial(ByVal OrderItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_OrderItemSerial WHERE OrderItem_Index = @OrderItem_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
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
    Private Function Check_LastReceive_Date(ByVal pTableName As String, ByVal pColumn_Index As String, ByVal pStr_Index As String, ByVal pColumn_Date As String, ByVal pCheck_Date As String) As Boolean
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

#End Region
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Order_Index"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Update Dong_kk 10/01-2010 - / check การยกเลิกจำนวน Qty ,Total_Qty ใน ASN และ PO
    ''' -------------------------------------------------
    ''' Update Date : 01/02/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Add Status
    ''' -------------------------------------------------
    ''' </remarks>
    ''' 

#Region " CANCEL ORDER "
    Public Function Cancel_Order(ByVal Order_Index As String) As Boolean
        Dim strSQL As String = ""
        Dim Qty_Sku_Bal As Double = 0
        Dim Weight_Sku_Bal As Double = 0
        Dim Volume_Sku_Bal As Double = 0
        Dim Qty_PLot_Bal As Double = 0
        Dim Weight_PLot_Bal As Double = 0
        Dim Volume_PLot_Bal As Double = 0
        Dim Qty_ItemStatus_Bal As Double = 0
        Dim Weight_ItemStatus_Bal As Double = 0
        Dim Volume_ItemStatus_Bal As Double = 0

        Dim WV_UserName_By_Cacel As String = WV_UserName + "-" + Order_Index
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            'PlanReceie Cancel Dong_kk
            strSQL = " select tb_OrderItem.*,tb_Order.Order_No "
            strSQL &= " from tb_OrderItem inner join "
            strSQL &= "      tb_Order  ON tb_Order.Order_Index = tb_OrderItem.Order_Index "
            strSQL &= " WHERE tb_OrderItem.Order_Index ='" & Order_Index & "'"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With
            strSQL = Nothing
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            If DS.Tables.Contains("tbl") Then
                DS.Tables("tbl").Rows.Clear()
            End If
            DataAdapter.Fill(DS, "tbl")
            Dim strOrder_No As String = "" 'Need For audit_log
            If DS.Tables("tbl").Rows.Count <> 0 Then
                strOrder_No = DS.Tables("tbl").Rows(0).Item("Order_No").ToString

                For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1

                    _objItem.Qty = DS.Tables("tbl").Rows(i).Item("Qty").ToString
                    _objItem.Total_Qty = DS.Tables("tbl").Rows(i).Item("Total_Qty").ToString
                    _objItem.Weight = DS.Tables("tbl").Rows(i).Item("Weight").ToString
                    _objItem.Volume = DS.Tables("tbl").Rows(i).Item("Volume").ToString
                    _objItem.Flo1 = DS.Tables("tbl").Rows(i).Item("Flo1").ToString
                    _objItem.OrderItem_Price = DS.Tables("tbl").Rows(i).Item("OrderItem_Price").ToString

                    _objItem.Str10 = DS.Tables("tbl").Rows(i).Item("str10").ToString
                    _objItem.Sku_Index = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
                    _objItem.Plan_Process = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString
                    _objItem.DocumentPlan_No = DS.Tables("tbl").Rows(i).Item("DocumentPlan_No").ToString
                    _objItem.DocumentPlan_Index = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
                    _objItem.DocumentPlanItem_Index = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString


                    If _objItem.Plan_Process <> -9 Then
                        StatusWithdraw_Document = _objItem.Plan_Process
                        Select Case StatusWithdraw_Document
                            Case Withdraw_Document.PO
                                Me.UPDATE_DELETE_PLANRECEIVE(_objItem, myTrans)
                            Case Withdraw_Document.Packing
                                Me.UPDATE_DELETE_PLANRECEIVE(_objItem, myTrans)
                            Case Withdraw_Document.ASN
                                Me.UPDATE_DELETE_PLANRECEIVE(_objItem, myTrans)
                        End Select
                    End If
                Next
            End If

            ' ***********************************************

            ' *** Important WHERE  tb_Order.Status in (2,3)  ONLY  2 : สินค้าในคลัง  , 3: เสร็จสิ้น เพื่อใช้ในการ Insert ข้อมูลใน tb_Transaction
            'Dong_kk add Status Cancel Status in (2,3,4)

            strSQL = " SELECT    * "
            strSQL &= " FROM VIEW_OrderCancelTransaction "
            strSQL &= "  WHERE      VIEW_OrderCancelTransaction.Order_Index='" & Order_Index & "' AND VIEW_OrderCancelTransaction.Status in (2,3,4)  "
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans

            If DS.Tables.Contains("tbl_order") Then
                DS.Tables("tbl_order").Rows.Clear()
            End If
            DataAdapter.Fill(DS, "tbl_order")

            If DS.Tables("tbl_order").Rows.Count <> 0 Then
                For i As Integer = 0 To DS.Tables("tbl_order").Rows.Count - 1
                    Dim Customer_Index As String = DS.Tables("tbl_order").Rows(i).Item("Customer_Index").ToString
                    Dim Plot As String = DS.Tables("tbl_order").Rows(i).Item("PLot").ToString
                    Dim ItemStatus_Index As String = DS.Tables("tbl_order").Rows(i).Item("ItemStatus_Index").ToString
                    Dim Sku_Index As String = DS.Tables("tbl_order").Rows(i).Item("Sku_Index").ToString
                    Dim Tag_No As String = DS.Tables("tbl_order").Rows(i).Item("Tag_No").ToString
                    Dim LocationBalance_Index As String = DS.Tables("tbl_order").Rows(i).Item("LocationBalance_Index").ToString
                    Dim Order_No As String = DS.Tables("tbl_order").Rows(i).Item("Order_No").ToString ''
                    Dim ERP_Location As String = DS.Tables("tbl_order").Rows(i).Item("ERP_Location").ToString
                    ' *** Manage ms_location ***
                    strSQL = " UPDATE ms_Location "
                    strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl_order").Rows(i).Item("Qty_Bal") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl_order").Rows(i).Item("Weight_Bal") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl_order").Rows(i).Item("Volume_Bal") & " "
                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString & "' "
                    ' *** If Area of Location Emtry ***
                    strSQL &= " UPDATE ms_Location "
                    strSQL &= " SET Space_Used =0 "
                    strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString & "' "
                    ' ****************************************
                    strSQL &= "  UPDATE tb_LocationBalance "
                    strSQL &= " SET ReserveQty= 0,"
                    strSQL &= "ReserveWeight= 0,"
                    strSQL &= "ReserveVolume= 0,"
                    strSQL &= "ReserveQty_Item= 0,"
                    strSQL &= "ReserveOrderItem_Price= 0,"
                    strSQL &= "Qty_Bal= 0,"
                    strSQL &= "Qty_Recieve_Package= 0,"
                    strSQL &= "Weight_Bal= 0,"
                    strSQL &= "Volume_Bal= 0,"
                    strSQL &= "Qty_Item_Bal= 0,"
                    strSQL &= "OrderItem_Price_Bal= 0  "
                    strSQL &= " WHERE LocationBalance_Index ='" & LocationBalance_Index & "' "

                    With SQLServerCommand
                        .CommandText = strSQL
                        .ExecuteNonQuery()
                    End With

                    ' ****************************************

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

                    objBal = Nothing

                    ' *********************************

                    ' *** Insert tb_Transaction ***

                    strSQL = " INSERT INTO tb_Transaction    "
                    strSQL &= " (Customer_Index,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_In,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_In,OrderItem_Price_Bal,       Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_In,Weight_In,Volume_In,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,SO_No,ItemDefinition_Index,DocumentType_Index,Serial_No,HandlingType_Index ,Tax1_In,Tax2_In,Tax3_In,Tax4_In,Tax5_In,TAG_Index,DocumentPlan_Index,DocumentPlanItem_Index,ERP_Location_From,ERP_Location_TO) VALUES "
                    strSQL &= " (@Customer_Index,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,-@Qty_Item_In,0,0,-@OrderItem_Price_In,0,       @Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_In,@Weight_In,@Volume_In,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@SO_No,@ItemDefinition_Index,@DocumentType_Index,@Serial_No,@HandlingType_Index,@Tax1_In,@Tax2_In,@Tax3_In,@Tax4_In,@Tax5_In,@TAG_Index,@DocumentPlan_Index,@DocumentPlanItem_Index,@ERP_Location_From,@ERP_Location_TO)"


                    ' **** Manage Balance ***

                    With SQLServerCommand

                        .Parameters.Clear()

                        '  **** Generate OrderItemLocation_Index  ***
                        Dim objDBIndex As New Sy_AutoNumber
                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
                        objDBIndex = Nothing
                        ' *******************************************

                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString


                        .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("TAG_Index").ToString
                        .Parameters.Add("@DocumentPlan_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Order_Index").ToString
                        .Parameters.Add("@DocumentPlanItem_Index", SqlDbType.NVarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("OrderItem_Index").ToString


                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Customer_Index").ToString
                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Order_Index").ToString
                        .Parameters.Add("@Order_Date", SqlDbType.DateTime, 20).Value = DS.Tables("tbl_order").Rows(i).Item("Order_Date").ToString
                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("OrderItem_Index").ToString
                        .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Product_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("ProductType_Index").ToString
                        .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = DS.Tables("tbl_order").Rows(i).Item("Qty_Item_Bal").ToString
                        .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl_order").Rows(i).Item("Qty_Item_Bal").ToString


                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("Order_No").ToString 'ja test
                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl_order").Rows(i).Item("Order_Date").ToString).ToString("yyyy/MM/dd")
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Sku_Index").ToString
                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 500).Value = DS.Tables("tbl_order").Rows(i).Item("Lot_No").ToString
                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString
                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("Tag_No").ToString
                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("PLot").ToString
                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("ItemStatus_Index").ToString
                        .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl_order").Rows(i).Item("Qty_Bal").ToString)
                        .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl_order").Rows(i).Item("Weight_Bal").ToString)
                        .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl_order").Rows(i).Item("Volume_Bal").ToString)
                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("Serial_No").ToString
                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                        ' Using Getdate() 
                        '.Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Date")
                        ' Process_id 
                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 1

                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

                        .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = DS.Tables("tbl_order").Rows(i).Item("OrderItem_Price_Bal").ToString

                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Item_Package_Index").ToString
                        'Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No
                        .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Invoice_In").ToString
                        .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = ""
                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("PO_No").ToString
                        .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = ""
                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Pallet_No").ToString
                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Declaration_No").ToString

                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "" 'DS.Tables("tbl_order").Rows(i).Item("ItemDefinition_Index").ToString
                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("DocumentType_Index").ToString

                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("HandlingType_Index").ToString

                        'include from master site
                        ',@Tax1_In,@Tax2_In,@Tax3_In,@Tax4_In,@Tax5_In,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out,@Tax1_Bal,@Tax2_Bal,@Tax3_Bal,@Tax4_Bal,@Tax5_Bal)"
                        .Parameters.Add("@Tax1_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax1").ToString
                        .Parameters.Add("@Tax2_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax2").ToString
                        .Parameters.Add("@Tax3_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax3").ToString
                        .Parameters.Add("@Tax4_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax4").ToString
                        .Parameters.Add("@Tax5_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax5").ToString
                        .Parameters.Add("@ERP_Location_From", SqlDbType.NVarChar, 100).Value = ERP_Location
                        .Parameters.Add("@ERP_Location_TO", SqlDbType.NVarChar, 100).Value = ERP_Location
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    connectDB()
                    EXEC_Command()

                Next

            End If

            ' ********** Cancel tb_Order **********
            strSQL = "  UPDATE tb_Order "
            strSQL &= " SET status =-1  ,cancel_date=getdate(),cancel_by='" & WV_UserName & "',cancel_branch='" & WV_Branch_ID & "' "
            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "
            'UPDATE tb_OrderItem
            strSQL &= "  UPDATE tb_OrderItem "
            strSQL &= " SET status =-1  "
            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "
            'UPDATE tb_OrderItemLocation
            strSQL &= "UPDATE tb_OrderItemLocation "
            strSQL &= " SET status =-1 "
            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "

            ' Update tb_AssetLocationBalance*********************************
            strSQL &= "  UPDATE tb_AssetLocationBalance "
            strSQL &= " SET status =-1  "
            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "
            'UPDATE tb_JobOrder
            strSQL &= "  UPDATE tb_JobOrder "
            strSQL &= " SET status =-1  "
            strSQL &= " WHERE JobOrder_Index ='" & Order_Index & "' "
            'UPDATE tb_OrderItemLocation
            strSQL &= "  UPDATE tb_OrderItemLocation "
            strSQL &= " SET status =-1  "
            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "
            'UPDATE tb_TAG
            strSQL &= "  UPDATE tb_TAG "
            strSQL &= " SET TAG_Status =-1  "
            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "

            strSQL &= " Update tb_OrderItemSerial Set Status = -1 WHERE OrderItem_Index In ( Select OrderItem_Index from tb_OrderItem WHERE Order_Index = '" & Order_Index & "' )"

            With SQLServerCommand
                .CommandText = strSQL
                .ExecuteNonQuery()
            End With
            ' ********************************************

            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = Order_Index
            oAudit_Log.Document_No = strOrder_No
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_Receiving)


            '*** Commit transaction
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
    '#Region " CANCEL ORDER "
    '    Public Function Cancel_Order(ByVal Order_Index As String) As Boolean
    '        Dim strSQL As String = ""
    '        Dim Qty_Sku_Bal As Double = 0
    '        Dim Weight_Sku_Bal As Double = 0
    '        Dim Volume_Sku_Bal As Double = 0
    '        Dim Qty_PLot_Bal As Double = 0
    '        Dim Weight_PLot_Bal As Double = 0
    '        Dim Volume_PLot_Bal As Double = 0
    '        Dim Qty_ItemStatus_Bal As Double = 0
    '        Dim Weight_ItemStatus_Bal As Double = 0
    '        Dim Volume_ItemStatus_Bal As Double = 0

    '        Dim Qty_Sku_Location_Bal As Double = 0
    '        Dim Qty_ItemStatus_Location_Bal As Double = 0
    '        Dim Qty_PLot_Location_Bal As Double = 0


    '        Dim WV_UserName_By_Cacel As String = WV_UserName + "-" + Order_Index
    '        connectDB()
    '        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
    '        SQLServerCommand.Transaction = myTrans

    '        Try



    '            'Search Item Withdraw
    '            'strSQL = " SELECT    tb_WithdrawItemLocation.Tag_No,tb_WithdrawItemLocation.Location_Index,tb_WithdrawItemLocation.LocationBalance_Index,tb_WithdrawItemLocation.Serial_No,tb_WithdrawItemLocation.WithdrawItemLocation_Index,tb_WithdrawItemLocation.JobWithdraw_Index,tb_WithdrawItemLocation.Withdraw_Index,tb_WithdrawItemLocation.WithdrawItem_Index,tb_WithdrawItemLocation.Order_Index, tb_WithdrawItemLocation.Sku_Index,tb_WithdrawItemLocation.Lot_No, tb_WithdrawItemLocation.PLot, "
    '            'strSQL &= "          tb_WithdrawItemLocation.ItemStatus_Index, tb_WithdrawItemLocation.Qty,  tb_WithdrawItemLocation.Total_Qty,"
    '            'strSQL &= "          tb_WithdrawItemLocation.Weight, tb_WithdrawItemLocation.Volume, tb_WithdrawItemLocation.Serial_No,  "
    '            'strSQL &= "          tb_WithdrawItemLocation.Package_Index,tb_WithdrawItemLocation.LocationBalance_Index,tb_WithdrawItemLocation.Withdraw_Index,tb_Withdraw.Customer_Index ,tb_Withdraw.Withdraw_No "
    '            'strSQL &= "     FROM tb_WithdrawItemLocation INNER JOIN  "
    '            'strSQL &= "          tb_Withdraw   ON  tb_WithdrawItemLocation.Withdraw_Index = tb_Withdraw.Withdraw_Index  INNER JOIN "
    '            'strSQL &= "           ms_Customer   ON tb_Withdraw.Customer_Index =ms_Customer.Customer_Index "
    '            'strSQL &= "     WHERE tb_WithdrawItemLocation.Order_Index ='" & Order_Index & "'  AND tb_Withdraw.Status=-1 "

    '            '-- Dong_kk 

    '            strSQL = " SELECT    *"
    '            strSQL &= "     FROM VIEW_OrderCancelWithDraw "
    '            strSQL &= "     WHERE VIEW_OrderCancelWithDraw.Order_Index ='" & Order_Index & "'  AND VIEW_OrderCancelWithDraw.Status=2 "
    '            With SQLServerCommand

    '                .Connection = Connection
    '                .Transaction = myTrans
    '                .CommandText = strSQL
    '                .CommandTimeout = 0

    '            End With

    '            DataAdapter.SelectCommand = SQLServerCommand
    '            DataAdapter.SelectCommand.Transaction = myTrans

    '            DS = New DataSet
    '            DataAdapter.Fill(DS, "tbl")

    '            If DS.Tables("tbl").Rows.Count <> 0 Then

    '                For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1

    '                    Dim Customer_Index As String = DS.Tables("tbl").Rows(i).Item("Customer_Index").ToString
    '                    Dim Plot As String = DS.Tables("tbl").Rows(i).Item("PLot").ToString
    '                    Dim ItemStatus_Index As String = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
    '                    Dim Sku_Index As String = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
    '                    Dim Tag_No As String = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
    '                    Dim LocationBalance_Index As String = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString
    '                    Dim Withdraw_Index As String = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
    '                    Dim Withdraw_No As String = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString ' add 

    '                    ' ***  update quantity ***
    '                    strSQL = "  UPDATE tb_LocationBalance    "
    '                    strSQL &= " SET Qty_Bal=Qty_Bal+@Total_Qty,Weight_Bal=Weight_Bal+@Weight,Volume_Bal=Volume_Bal+@Volume "
    '                    strSQL &= " WHERE LocationBalance_Index=@LocationBalance_Index "

    '                    With SQLServerCommand

    '                        .Parameters.Clear()
    '                        .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("LocationBalance_Index").ToString
    '                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
    '                        .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Total_Qty")
    '                        .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Weight")
    '                        .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Volume")
    '                        ' *** If Check Qty_Recieve_Package *** 
    '                        .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = DS.Tables("tbl").Rows(i).Item("Qty")

    '                    End With


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,PalletType_Index,Pallet_Qty " _
    '                                                   & " FROM  tb_LocationBalance  " _
    '                                                   & " where LocationBalance_Index ='" & LocationBalance_Index & "'  "

    '                    With SQLServerCommand
    '                        .Connection = Connection
    '                        .Transaction = myTrans
    '                        .CommandText = strSQL
    '                        .CommandTimeout = 0
    '                    End With

    '                    DataAdapter.SelectCommand = SQLServerCommand
    '                    DataAdapter.SelectCommand.Transaction = myTrans

    '                    '   DS = New DataSet
    '                    DataAdapter.Fill(DS, "tbl0")

    '                    If DS.Tables("tbl0").Rows.Count <> 0 Then
    '                        ' ***********************
    '                        Dim objCalBalance As New CalculateBalance
    '                        objCalBalance.setQty_Recieve_Package(Connection, myTrans, DS.Tables("tbl0").Rows(0).Item("LocationBalance_Index").ToString)
    '                        objCalBalance = Nothing
    '                        ' ***********************


    '                        ' *** Set Zero  with Cancel Item with Order ***

    '                        strSQL = "  UPDATE tb_LocationBalance "
    '                        strSQL &= " SET status =-1,Qty_Recieve_Package=0,Qty_Bal=0,Weight_Bal=0,Volume_Bal=0 "
    '                        strSQL &= " WHERE LocationBalance_Index ='" & LocationBalance_Index & "' "


    '                        With SQLServerCommand
    '                            .CommandText = strSQL
    '                            .ExecuteNonQuery()
    '                        End With

    '                        ' *********************************************

    '                    End If
    '                    DS.Tables("tbl0").Clear()

    '                    ' ********************* Manage PalletType  ******************************

    '                    ' *** Need to Qty_Bal = 0 ***
    '                    strSQL = " SELECT   LocationBalance_Index,Order_Index, Qty_Bal ,PalletType_Index,Pallet_Qty " _
    '                                                     & " FROM  tb_LocationBalance  " _
    '                                                     & " where LocationBalance_Index ='" & LocationBalance_Index & "'  AND Qty_Bal=0 "

    '                    With SQLServerCommand
    '                        .Connection = Connection
    '                        .Transaction = myTrans
    '                        .CommandText = strSQL
    '                        .CommandTimeout = 0
    '                    End With

    '                    DataAdapter.SelectCommand = SQLServerCommand
    '                    DataAdapter.SelectCommand.Transaction = myTrans
    '                    '   DS = New DataSet
    '                    DataAdapter.Fill(DS, "tbl1")

    '                    If DS.Tables("tbl1").Rows.Count <> 0 Then

    '                        If Val(DS.Tables("tbl1").Rows(0).Item("Qty_Bal").ToString) <= 0 Then
    '                            ' *** update ms_PalletType *** 

    '                            strSQL = "UPDATE ms_PalletType "
    '                            strSQL &= " SET Pallet_Remain=Pallet_Remain+" & Val(DS.Tables("tbl1").Rows(0).Item("Pallet_Qty").ToString) & ""
    '                            strSQL &= " WHERE PalletType_Index ='" & DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString & "' "


    '                            With SQLServerCommand
    '                                .CommandText = strSQL
    '                                .ExecuteNonQuery()
    '                            End With


    '                            ' *** Insert Record in tb_PalletType_History ***

    '                            strSQL = " INSERT INTO tb_PalletType_History  "
    '                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_In,Qty_Bal,add_by,add_branch) Values  "
    '                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_In,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,@Qty_In,0),@add_by,@add_branch) "


    '                            ' Generate PalletType_History_Index 

    '                            Dim objDBPalletTypeIndex2 As New Sy_AutoNumber
    '                            Dim PalletType_History_Index2 As String = objDBPalletTypeIndex2.getSys_Value("PalletType_History_Index")
    '                            objDBPalletTypeIndex2 = Nothing


    '                            ' *** Call Function Get Balance ***

    '                            'Dim objPalletTypeBal2 As New CalculateBalance
    '                            Dim Qty_PalletType_Bal2 As Double = 0
    '                            '' *** Qty ***
    '                            'Qty_PalletType_Bal2 = objPalletTypeBal2.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
    '                            'objPalletTypeBal2 = Nothing

    '                            ' *********************************

    '                            With SQLServerCommand

    '                                .Parameters.Clear()

    '                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index2
    '                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
    '                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

    '                                ' *** Important Fix Code for Pallettype Location Default ***
    '                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
    '                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
    '                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"

    '                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
    '                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
    '                                .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
    '                                '     .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal2
    '                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
    '                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

    '                            End With


    '                            With SQLServerCommand
    '                                .CommandText = strSQL
    '                                .ExecuteNonQuery()
    '                            End With

    '                            ' **********************************************

    '                            ' *** Insert Record in tb_PalletType_History ***

    '                            strSQL = " INSERT INTO tb_PalletType_History  "
    '                            strSQL &= " (PalletType_History_Index,PalletType_Index,Process_Id,From_PalletType_Location_Index,To_PalletType_Location_Index,Destination_PalletType_Location_Index,Tag_No,Order_Index,Qty_Out,Qty_Bal,add_by,add_branch) Values  "
    '                            strSQL &= " (@PalletType_History_Index,@PalletType_Index,@Process_Id,@From_PalletType_Location_Index,@To_PalletType_Location_Index,@Destination_PalletType_Location_Index,@Tag_No,@Order_Index,@Qty_Out,dbo.udf_PalletType_Location_Balance(@PalletType_Index,@Destination_PalletType_Location_Index,0,@Qty_Out),@add_by,@add_branch) "


    '                            ' Generate PalletType_History_Index 

    '                            Dim objDBPalletTypeIndex As New Sy_AutoNumber
    '                            Dim PalletType_History_Index As String = objDBPalletTypeIndex.getSys_Value("PalletType_History_Index")
    '                            objDBPalletTypeIndex = Nothing


    '                            ' *** Call Function Get Balance ***

    '                            'Dim objPalletTypeBal As New CalculateBalance
    '                            Dim Qty_PalletType_Bal As Double = 0
    '                            '' *** Qty ***
    '                            'Qty_PalletType_Bal = objPalletTypeBal.getQty_PalletType_Bal(Connection, myTrans, DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString)
    '                            'objPalletTypeBal = Nothing

    '                            ' *********************************

    '                            With SQLServerCommand

    '                                .Parameters.Clear()

    '                                .Parameters.Add("@PalletType_History_Index", SqlDbType.VarChar, 13).Value = PalletType_History_Index
    '                                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("PalletType_Index").ToString
    '                                .Parameters.Add("@Process_Id", SqlDbType.Int, 4).Value = 1

    '                                ' *** Important Fix Code for Pallettype Location Default ***
    '                                .Parameters.Add("@From_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"
    '                                .Parameters.Add("@To_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "0"
    '                                .Parameters.Add("@Destination_PalletType_Location_Index", SqlDbType.VarChar, 13).Value = "1"

    '                                .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = Tag_No
    '                                .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl1").Rows(0).Item("Order_Index").ToString
    '                                .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = DS.Tables("tbl1").Rows(0).Item("Pallet_Qty")
    '                                '    .Parameters.Add("@Qty_Bal", SqlDbType.Float, 8).Value = Qty_PalletType_Bal
    '                                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
    '                                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

    '                            End With


    '                            With SQLServerCommand
    '                                .CommandText = strSQL
    '                                .ExecuteNonQuery()
    '                            End With

    '                            ' **********************************************


    '                        End If

    '                    End If

    '                    ' xxxxxxxxxxxxxxxxxxxx
    '                    DS.Tables("tbl1").Clear()
    '                    ' xxxxxxxxxxxxxxxxxxxx


    '                    ' *********************  End Manage PalletType  ******************************

    '                    ' *** Call Function Get Balance ***
    '                    Dim objBal As New CalculateBalance
    '                    ' *** Qty ***
    '                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
    '                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
    '                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
    '                    ' *** Weight ***
    '                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
    '                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
    '                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
    '                    ' *** Volume ***
    '                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
    '                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
    '                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)




    '                    ' *********************************
    '                    ' *** Insert tb_Transaction ***

    '                    strSQL = " INSERT INTO tb_Transaction    "
    '                    strSQL &= " (Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_Out,Weight_Out,Volume_Out,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch ,Qty_Sku_Location_Bal,Qty_ItemStatus_Location_Bal,Qty_PLot_Location_Bal ) VALUES "
    '                    strSQL &= " (@Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_Out,@Weight_Out,@Volume_Out,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch ,@Qty_Sku_Location_Bal,@Qty_ItemStatus_Location_Bal,@Qty_PLot_Location_Bal )"


    '                    ' **** Manage Balance ***

    '                    With SQLServerCommand

    '                        .Parameters.Clear()

    '                        '  **** Generate OrderItemLocation_Index  ***
    '                        Dim objDBIndex As New Sy_AutoNumber
    '                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
    '                        objDBIndex = Nothing
    '                        ' *******************************************

    '                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString
    '                        '.Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Index").ToString
    '                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_No").ToString
    '                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
    '                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Lot_No").ToString
    '                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
    '                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("Location_Index").ToString
    '                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Tag_No").ToString
    '                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("PLot").ToString
    '                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("ItemStatus_Index").ToString
    '                        .Parameters.Add("@Qty_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Total_Qty").ToString)
    '                        .Parameters.Add("@Weight_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Weight").ToString)
    '                        .Parameters.Add("@Volume_Out", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl").Rows(i).Item("Volume").ToString)
    '                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl").Rows(i).Item("Serial_No").ToString
    '                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
    '                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
    '                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
    '                        ' Using Getdate() 
    '                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Date")
    '                        ' Process_id 
    '                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 2

    '                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
    '                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
    '                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

    '                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
    '                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
    '                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

    '                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
    '                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
    '                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

    '                        '07/05/2009 
    '                        .Parameters.Add("@Qty_Sku_Location_Bal", SqlDbType.Float, 8).Value = objBal.getQty_Sku_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, DS.Tables("tbl").Rows(i).Item("Location_Index").ToString)
    '                        .Parameters.Add("@Qty_ItemStatus_Location_Bal", SqlDbType.Float, 8).Value = objBal.getQty_ItemStatus_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index, DS.Tables("tbl").Rows(i).Item("Location_Index").ToString)
    '                        .Parameters.Add("@Qty_PLot_Location_Bal", SqlDbType.Float, 8).Value = objBal.getQty_PLot_Location_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, DS.Tables("tbl").Rows(i).Item("Location_Index").ToString)

    '                        objBal = Nothing
    '                    End With

    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    ' *** Manage ms_location ***

    '                    strSQL = "UPDATE ms_Location "
    '                    strSQL &= " SET Current_Qty =Current_Qty+" & DS.Tables("tbl").Rows(i).Item("Total_Qty") & ",Current_Weight=Current_Weight+" & DS.Tables("tbl").Rows(i).Item("Weight") & " ,Current_Volume=Current_Volume+" & DS.Tables("tbl").Rows(i).Item("Volume") & " "
    '                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    ' *** If Area of Location Emtry ***
    '                    strSQL = "UPDATE ms_Location "
    '                    strSQL &= " SET Space_Used =1 "
    '                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl").Rows(i).Item("Location_Index").ToString & "' "


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    ' *********************************



    '                    strSQL = "UPDATE tb_Withdraw "
    '                    strSQL &= " SET status =-1 ,cancel_date=getdate(),cancel_by='" & WV_UserName_By_Cacel & "'"
    '                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    strSQL = "UPDATE tb_WithdrawItem "
    '                    strSQL &= " SET status =-1 "
    '                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    strSQL = "UPDATE tb_JobWithdraw "
    '                    strSQL &= " SET status =-1 "
    '                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With
    '                    strSQL = "UPDATE tb_WithdrawItemLocation "
    '                    strSQL &= " SET status =-1 "
    '                    strSQL &= " WHERE Withdraw_Index ='" & Withdraw_Index & "' "


    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With


    '                Next

    '            End If




    '            'PO Cancel
    '            strSQL = " select sku_index,Qty,Total_Qty,Order_Index,str7,str10,Plan_Process,DocumentPlan_No,DocumentPlan_Index,DocumentPlanItem_Index "
    '            strSQL &= " from tb_OrderItem "
    '            strSQL &= " WHERE Order_Index ='" & Order_Index & "'"
    '            '  strSQL &= " AND Str7='" & _objItem.Str7 & "'"
    '            With SQLServerCommand
    '                .Connection = Connection
    '                .Transaction = myTrans
    '                .CommandText = strSQL
    '                .CommandTimeout = 0
    '            End With

    '            strSQL = Nothing

    '            DataAdapter.SelectCommand = SQLServerCommand
    '            DataAdapter.SelectCommand.Transaction = myTrans

    '            DS = New DataSet
    '            DataAdapter.Fill(DS, "tbl")

    '            If DS.Tables("tbl").Rows.Count <> 0 Then

    '                For i As Integer = 0 To DS.Tables("tbl").Rows.Count - 1

    '                    _objItem.Qty = DS.Tables("tbl").Rows(i).Item("Qty").ToString
    '                    _objItem.Total_Qty = DS.Tables("tbl").Rows(i).Item("Total_Qty").ToString
    '                    _objItem.Str10 = DS.Tables("tbl").Rows(i).Item("str10").ToString
    '                    _objItem.Sku_Index = DS.Tables("tbl").Rows(i).Item("Sku_Index").ToString
    '                    _objItem.Plan_Process = DS.Tables("tbl").Rows(i).Item("Plan_Process").ToString
    '                    _objItem.DocumentPlan_No = DS.Tables("tbl").Rows(i).Item("DocumentPlan_No").ToString
    '                    _objItem.DocumentPlan_Index = DS.Tables("tbl").Rows(i).Item("DocumentPlan_Index").ToString
    '                    _objItem.DocumentPlanItem_Index = DS.Tables("tbl").Rows(i).Item("DocumentPlanItem_Index").ToString


    '                    If _objItem.Plan_Process <> -9 Then
    '                        StatusWithdraw_Document = _objItem.Plan_Process
    '                        Select Case StatusWithdraw_Document
    '                            Case Withdraw_Document.PO
    '                                strSQL = "   SELECT  * "
    '                                strSQL &= "   FROM  tb_PurchaseOrderItem   "
    '                                strSQL &= " WHERE  PurchaseOrderItem_Index='" & _objItem.DocumentPlanItem_Index & "'" 'AND Sku_Index ='" & _objItem.Sku_Index & "' "

    '                                With SQLServerCommand
    '                                    .Connection = Connection
    '                                    .Transaction = myTrans
    '                                    .CommandText = strSQL
    '                                    .CommandTimeout = 0
    '                                End With

    '                                strSQL = Nothing

    '                                DataAdapter.SelectCommand = SQLServerCommand
    '                                DataAdapter.SelectCommand.Transaction = myTrans

    '                                DataAdapter.Fill(DS, "POI")

    '                                If DS.Tables("POI").Rows.Count <> 0 Then
    '                                    strSQL = " UPDATE tb_PurchaseOrderItem SET "
    '                                    strSQL &= "Total_Received_Qty = " & Val(DS.Tables("POI").Rows(i).Item("Total_Received_Qty").ToString) & " - " & _objItem.Total_Qty
    '                                    strSQL &= ",Received_Qty= " & Val(DS.Tables("POI").Rows(i).Item("Received_Qty").ToString) & " - " & _objItem.Qty
    '                                    strSQL &= " WHERE PurchaseOrderItem_Index = '" & DS.Tables("POI").Rows(i).Item("PurchaseOrderItem_Index").ToString & "'"
    '                                    '  strSQL &= " AND Sku_Index='" & _objItem.Sku_Index & "'"


    '                                    SetSQLString = strSQL
    '                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                    EXEC_Command()
    '                                    strSQL = Nothing

    '                                    Dim str_Order As String = ""
    '                                    str_Order = " UPDATE tb_PurchaseOrder SET "
    '                                    str_Order &= "Status = 2 , StatusReceive = 0"
    '                                    str_Order &= " WHERE PurchaseOrder_Index = '" & DS.Tables("POI").Rows(i).Item("PurchaseOrder_Index").ToString & "'"

    '                                    SetSQLString = str_Order
    '                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                    EXEC_Command()
    '                                    str_Order = Nothing
    '                                End If

    '                            Case Withdraw_Document.ASN
    '                                strSQL = "   SELECT  * "
    '                                strSQL &= "   FROM  tb_AdvanceShipNoticeItem   "
    '                                strSQL &= " WHERE  AdvanceShipNoticeItem_Index='" & _objItem.DocumentPlanItem_Index & "'" 'AND Sku_Index ='" & _objItem.Sku_Index & "' "

    '                                With SQLServerCommand
    '                                    .Connection = Connection
    '                                    .Transaction = myTrans
    '                                    .CommandText = strSQL
    '                                    .CommandTimeout = 0
    '                                End With

    '                                strSQL = Nothing

    '                                DataAdapter.SelectCommand = SQLServerCommand
    '                                DataAdapter.SelectCommand.Transaction = myTrans

    '                                DataAdapter.Fill(DS, "ASN")

    '                                'AdvanceShipNotice

    '                                If DS.Tables("ASN").Rows.Count <> 0 Then


    '                                    strSQL = " UPDATE tb_AdvanceShipNoticeItem SET "
    '                                    strSQL &= "Total_Received_Qty = " & Val(DS.Tables("ASN").Rows(i).Item("Total_Received_Qty").ToString) & " - " & _objItem.Total_Qty
    '                                    strSQL &= ",Received_Qty = " & Val(DS.Tables("ASN").Rows(i).Item("Received_Qty").ToString) & " - " & _objItem.Qty
    '                                    strSQL &= " WHERE AdvanceShipNoticeItem_Index = '" & DS.Tables("ASN").Rows(i).Item("AdvanceShipNoticeItem_Index").ToString & "'"


    '                                    SetSQLString = strSQL
    '                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                    EXEC_Command()
    '                                    strSQL = Nothing

    '                                    Dim str_Order As String = ""
    '                                    str_Order = " UPDATE tb_AdvanceShipNotice SET "
    '                                    str_Order &= "Status = 2 , StatusReceive = 0"
    '                                    str_Order &= " WHERE AdvanceShipNotice_Index = '" & DS.Tables("ASN").Rows(i).Item("AdvanceShipNotice_Index").ToString & "'"

    '                                    SetSQLString = str_Order
    '                                    SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                    EXEC_Command()
    '                                    str_Order = Nothing


    '                                    '   DS = Nothing


    '                                End If

    '                            Case Withdraw_Document.Packing
    '                                strSQL = " UPDATE tb_Packing SET "
    '                                strSQL &= "Qty_Receive = Qty_Receive - " & _objItem.Total_Qty
    '                                strSQL &= " WHERE Packing_Index = '" & _objItem.DocumentPlan_Index & "'"
    '                                SetSQLString = strSQL
    '                                SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                EXEC_Command()

    '                                strSQL = "   SELECT isnull(Qty_Product,0) as Qty_Product, isnull(Qty_Receive,0) as Qty_Receive FROM tb_Packing WHERE Packing_Index = @PlanDocument_Index  "
    '                                SQLServerCommand.Parameters.Clear()
    '                                SQLServerCommand.Parameters.Add("@PlanDocument_Index", SqlDbType.VarChar, 13).Value = _objItem.DocumentPlan_Index

    '                                With SQLServerCommand
    '                                    .Connection = Connection
    '                                    .Transaction = myTrans
    '                                    .CommandText = strSQL
    '                                    .CommandTimeout = 0
    '                                End With

    '                                DataAdapter.SelectCommand = SQLServerCommand
    '                                DataAdapter.SelectCommand.Transaction = myTrans
    '                                '   DS = New DataSet
    '                                DataAdapter.Fill(DS, "SO")

    '                                If DS.Tables("SO").Rows.Count > 0 Then
    '                                    If CDbl(DS.Tables("SO").Rows(0)("Qty_Receive").ToString) = 0 Then
    '                                        strSQL = "UPDATE tb_Packing "
    '                                        strSQL &= " SET status =5 "
    '                                        strSQL &= " WHERE Packing_Index ='" & _objItem.DocumentPlan_Index & "'"
    '                                        SetSQLString = strSQL
    '                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                        EXEC_Command()
    '                                    ElseIf CDbl(DS.Tables("SO").Rows(0)("Qty_Receive").ToString) >= CDbl(DS.Tables("SO").Rows(0)("Qty_Product").ToString) Then
    '                                        ' --- STEP 2: Update status in tb_SaleOrder = 3
    '                                        '****************  Case ผลิต    ****************  
    '                                        strSQL = "UPDATE tb_Packing "
    '                                        strSQL &= " SET status =3 "
    '                                        strSQL &= " WHERE Packing_Index ='" & _objItem.DocumentPlan_Index & "'"

    '                                        SetSQLString = strSQL
    '                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                        EXEC_Command()
    '                                    Else
    '                                        strSQL = "UPDATE tb_Packing "
    '                                        strSQL &= " SET status =4 "
    '                                        strSQL &= " WHERE Packing_Index ='" & _objItem.DocumentPlan_Index & "'"

    '                                        SetSQLString = strSQL
    '                                        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                                        EXEC_Command()
    '                                    End If

    '                                End If
    '                        End Select
    '                    End If




    '                    '   DS = Nothing






    '                Next
    '            End If

    '            ' ***********************************************

    '            ' *** Important WHERE  tb_Order.Status in (2,3)  ONLY  2 : สินค้าในคลัง  , 3: เสร็จสิ้น เพื่อใช้ในการ Insert ข้อมูลใน tb_Transaction
    '            'Dong_kk add Status Cancel Status in (2,3,4)

    '            strSQL = " SELECT    * "
    '            strSQL &= " FROM VIEW_OrderCancelTransaction "
    '            strSQL &= "  WHERE      VIEW_OrderCancelTransaction.Order_Index='" & Order_Index & "' AND VIEW_OrderCancelTransaction.Status in (2,3,4)  "

    '            With SQLServerCommand

    '                .Connection = Connection
    '                .Transaction = myTrans
    '                .CommandText = strSQL
    '                .CommandTimeout = 0

    '            End With

    '            DataAdapter.SelectCommand = SQLServerCommand
    '            DataAdapter.SelectCommand.Transaction = myTrans

    '            DS = New DataSet
    '            DataAdapter.Fill(DS, "tbl_order")

    '            If DS.Tables("tbl_order").Rows.Count <> 0 Then

    '                For i As Integer = 0 To DS.Tables("tbl_order").Rows.Count - 1

    '                    Dim Customer_Index As String = DS.Tables("tbl_order").Rows(i).Item("Customer_Index").ToString
    '                    Dim Plot As String = DS.Tables("tbl_order").Rows(i).Item("PLot").ToString
    '                    Dim ItemStatus_Index As String = DS.Tables("tbl_order").Rows(i).Item("ItemStatus_Index").ToString
    '                    Dim Sku_Index As String = DS.Tables("tbl_order").Rows(i).Item("Sku_Index").ToString
    '                    Dim Tag_No As String = DS.Tables("tbl_order").Rows(i).Item("Tag_No").ToString
    '                    Dim LocationBalance_Index As String = DS.Tables("tbl_order").Rows(i).Item("LocationBalance_Index").ToString
    '                    Dim Order_No As String = DS.Tables("tbl_order").Rows(i).Item("Order_No").ToString ''





    '                    ' *** Manage ms_location ***

    '                    strSQL = "UPDATE ms_Location "
    '                    strSQL &= " SET Current_Qty =Current_Qty-" & DS.Tables("tbl_order").Rows(i).Item("Qty_Bal") & ",Current_Weight=Current_Weight-" & DS.Tables("tbl_order").Rows(i).Item("Weight_Bal") & " ,Current_Volume=Current_Volume-" & DS.Tables("tbl_order").Rows(i).Item("Volume_Bal") & " "
    '                    strSQL &= " WHERE   Location_Index ='" & DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString & "' "

    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    ' *** If Area of Location Emtry ***
    '                    strSQL = "UPDATE ms_Location "
    '                    strSQL &= " SET Space_Used =0 "
    '                    strSQL &= " WHERE  Current_Qty <=0 AND Location_Index ='" & DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString & "' "

    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With
    '                    ' *********************************



    '                    ' ****************************************
    '                    strSQL = "  UPDATE tb_LocationBalance "
    '                    strSQL &= " SET ReserveQty= 0,"
    '                    strSQL &= "ReserveWeight= 0,"
    '                    strSQL &= "ReserveVolume= 0,"
    '                    strSQL &= "ReserveQty_Item= 0,"
    '                    strSQL &= "ReserveOrderItem_Price= 0,"
    '                    strSQL &= "Qty_Bal= 0,"
    '                    strSQL &= "Qty_Recieve_Package= 0,"
    '                    strSQL &= "Weight_Bal= 0,"
    '                    strSQL &= "Volume_Bal= 0,"
    '                    strSQL &= "Qty_Item_Bal= 0,"
    '                    strSQL &= "OrderItem_Price_Bal= 0  "
    '                    strSQL &= " WHERE LocationBalance_Index ='" & LocationBalance_Index & "' "

    '                    With SQLServerCommand
    '                        .CommandText = strSQL
    '                        .ExecuteNonQuery()
    '                    End With

    '                    ' ****************************************

    '                    ' *** Call Function Get Balance ***
    '                    Dim objBal As New CalculateBalance
    '                    ' *** Qty ***
    '                    Qty_Sku_Bal = objBal.getQty_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
    '                    Qty_PLot_Bal = objBal.getQty_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
    '                    Qty_ItemStatus_Bal = objBal.getQty_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
    '                    ' *** Weight ***
    '                    Weight_Sku_Bal = objBal.getWeight_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
    '                    Weight_PLot_Bal = objBal.getWeight_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
    '                    Weight_ItemStatus_Bal = objBal.getWeight_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)
    '                    ' *** Volume ***
    '                    Volume_Sku_Bal = objBal.getVolume_Sku_Bal(Connection, myTrans, Customer_Index, Sku_Index)
    '                    Volume_PLot_Bal = objBal.getVolume_PLot_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot)
    '                    Volume_ItemStatus_Bal = objBal.getVolume_ItemStatus_Bal(Connection, myTrans, Customer_Index, Sku_Index, Plot, ItemStatus_Index)

    '                    objBal = Nothing

    '                    ' *********************************

    '                    ' *** Insert tb_Transaction ***

    '                    strSQL = " INSERT INTO tb_Transaction    "
    '                    'strSQL &= " (Customer_Index,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_In,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_In,OrderItem_Price_Bal,       Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_In,Weight_In,Volume_In,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,SO_No,ItemDefinition_Index,DocumentType_Index,Serial_No,HandlingType_Index) VALUES "
    '                    'strSQL &= " (@Customer_Index,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,-@Qty_Item_In,0,0,-@OrderItem_Price_In,0,       @Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_In,@Weight_In,@Volume_In,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@SO_No,@ItemDefinition_Index,@DocumentType_Index,@Serial_No,@HandlingType_Index)"

    '                    'include from master site
    '                    strSQL &= " (Customer_Index,Order_Index,Order_Date,OrderItem_Index,Product_Index,ProductType_Index,Qty_Item_In,Qty_Item_Out,Qty_Item_Bal,OrderItem_Price_In,OrderItem_Price_Bal,       Transaction_Index,Transaction_Id,Sku_Index,Lot_No,PLot,ItemStatus_Index,Process_Id,Transation_Date,Tag_No,From_Location_Index,To_Location_Index,Qty_In,Weight_In,Volume_In,Qty_Sku_Bal,Qty_PLot_Bal,Qty_ItemStatus_Bal,Weight_Sku_Bal,Weight_PLot_Bal,Weight_ItemStatus_Bal,Volume_Sku_Bal,Volume_PLot_Bal,Volume_ItemStatus_Bal,add_by,add_branch,Item_Package_Index,Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No,SO_No,ItemDefinition_Index,DocumentType_Index,Serial_No,HandlingType_Index ,Tax1_In,Tax2_In,Tax3_In,Tax4_In,Tax5_In) VALUES "
    '                    strSQL &= " (@Customer_Index,@Order_Index,@Order_Date,@OrderItem_Index,@Product_Index,@ProductType_Index,-@Qty_Item_In,0,0,-@OrderItem_Price_In,0,       @Transaction_Index,@Transaction_Id,@Sku_Index,@Lot_No,@PLot,@ItemStatus_Index,@Process_Id,@Transation_Date,@Tag_No,@From_Location_Index,@To_Location_Index,@Qty_In,@Weight_In,@Volume_In,@Qty_Sku_Bal,@Qty_PLot_Bal,@Qty_ItemStatus_Bal,@Weight_Sku_Bal,@Weight_PLot_Bal,@Weight_ItemStatus_Bal,@Volume_Sku_Bal,@Volume_PLot_Bal,@Volume_ItemStatus_Bal,@add_by,@add_branch,@Item_Package_Index,@Invoice_In,@Invoice_Out,@PO_No,@Pallet_No,@Declaration_No,@SO_No,@ItemDefinition_Index,@DocumentType_Index,@Serial_No,@HandlingType_Index,@Tax1_In,@Tax2_In,@Tax3_In,@Tax4_In,@Tax5_In)"


    '                    ' **** Manage Balance ***

    '                    With SQLServerCommand

    '                        .Parameters.Clear()

    '                        '  **** Generate OrderItemLocation_Index  ***
    '                        Dim objDBIndex As New Sy_AutoNumber
    '                        .Parameters.Add("@Transaction_Index", SqlDbType.VarChar, 13).Value = objDBIndex.getSys_Value("Transaction_Index")
    '                        objDBIndex = Nothing
    '                        ' *******************************************

    '                        '     .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl").Rows(i).Item("OrderItem_Index").ToString

    '                        .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Customer_Index").ToString
    '                        .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Order_Index").ToString
    '                        .Parameters.Add("@Order_Date", SqlDbType.DateTime, 20).Value = DS.Tables("tbl_order").Rows(i).Item("Order_Date").ToString
    '                        .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("OrderItem_Index").ToString
    '                        .Parameters.Add("@Product_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Product_Index").ToString
    '                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("ProductType_Index").ToString
    '                        .Parameters.Add("@Qty_Item_In", SqlDbType.Float, 8).Value = DS.Tables("tbl_order").Rows(i).Item("Qty_Item_Bal").ToString
    '                        .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float, 8).Value = DS.Tables("tbl_order").Rows(i).Item("Qty_Item_Bal").ToString


    '                        .Parameters.Add("@Transaction_Id", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Order_No").ToString 'ja test
    '                        .Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = CDate(DS.Tables("tbl_order").Rows(i).Item("Order_Date").ToString).ToString("yyyy/MM/dd")
    '                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Sku_Index").ToString
    '                        .Parameters.Add("@Lot_No", SqlDbType.VarChar, 500).Value = DS.Tables("tbl_order").Rows(i).Item("Lot_No").ToString
    '                        .Parameters.Add("@From_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString
    '                        .Parameters.Add("@To_Location_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Location_Index").ToString
    '                        .Parameters.Add("@Tag_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("Tag_No").ToString
    '                        .Parameters.Add("@PLot", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("PLot").ToString
    '                        .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("ItemStatus_Index").ToString
    '                        .Parameters.Add("@Qty_In", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl_order").Rows(i).Item("Qty_Bal").ToString)
    '                        .Parameters.Add("@Weight_In", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl_order").Rows(i).Item("Weight_Bal").ToString)
    '                        .Parameters.Add("@Volume_In", SqlDbType.Float, 8).Value = -Val(DS.Tables("tbl_order").Rows(i).Item("Volume_Bal").ToString)
    '                        .Parameters.Add("@Serial_No", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("Serial_No").ToString
    '                        .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 1
    '                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
    '                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
    '                        ' Using Getdate() 
    '                        '.Parameters.Add("@Transation_Date", SqlDbType.SmallDateTime, 4).Value = DS.Tables("tbl").Rows(i).Item("Withdraw_Date")
    '                        ' Process_id 
    '                        .Parameters.Add("@Process_id", SqlDbType.Float, 8).Value = 1

    '                        .Parameters.Add("@Qty_Sku_Bal", SqlDbType.Float, 8).Value = Qty_Sku_Bal
    '                        .Parameters.Add("@Qty_PLot_Bal", SqlDbType.Float, 8).Value = Qty_PLot_Bal
    '                        .Parameters.Add("@Qty_ItemStatus_Bal", SqlDbType.Float, 8).Value = Qty_ItemStatus_Bal

    '                        .Parameters.Add("@Weight_Sku_Bal", SqlDbType.Float, 8).Value = Weight_Sku_Bal
    '                        .Parameters.Add("@Weight_PLot_Bal", SqlDbType.Float, 8).Value = Weight_PLot_Bal
    '                        .Parameters.Add("@Weight_ItemStatus_Bal", SqlDbType.Float, 8).Value = Weight_ItemStatus_Bal

    '                        .Parameters.Add("@Volume_Sku_Bal", SqlDbType.Float, 8).Value = Volume_Sku_Bal
    '                        .Parameters.Add("@Volume_PLot_Bal", SqlDbType.Float, 8).Value = Volume_PLot_Bal
    '                        .Parameters.Add("@Volume_ItemStatus_Bal", SqlDbType.Float, 8).Value = Volume_ItemStatus_Bal

    '                        .Parameters.Add("@OrderItem_Price_In", SqlDbType.Float, 8).Value = DS.Tables("tbl_order").Rows(i).Item("OrderItem_Price_Bal").ToString

    '                        .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("Item_Package_Index").ToString
    '                        'Invoice_In,Invoice_Out,PO_No,Pallet_No,Declaration_No
    '                        .Parameters.Add("@Invoice_In", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Invoice_In").ToString
    '                        .Parameters.Add("@Invoice_Out", SqlDbType.VarChar, 100).Value = ""
    '                        .Parameters.Add("@PO_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("PO_No").ToString
    '                        .Parameters.Add("@SO_No", SqlDbType.VarChar, 100).Value = ""
    '                        .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Pallet_No").ToString
    '                        .Parameters.Add("@Declaration_No", SqlDbType.VarChar, 100).Value = DS.Tables("tbl_order").Rows(i).Item("Declaration_No").ToString

    '                        .Parameters.Add("@ItemDefinition_Index", SqlDbType.VarChar, 50).Value = "" 'DS.Tables("tbl_order").Rows(i).Item("ItemDefinition_Index").ToString
    '                        .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 50).Value = DS.Tables("tbl_order").Rows(i).Item("DocumentType_Index").ToString

    '                        .Parameters.Add("@HandlingType_Index", SqlDbType.VarChar, 13).Value = DS.Tables("tbl_order").Rows(i).Item("HandlingType_Index").ToString

    '                        'include from master site
    '                        ',@Tax1_In,@Tax2_In,@Tax3_In,@Tax4_In,@Tax5_In,@Tax1_Out,@Tax2_Out,@Tax3_Out,@Tax4_Out,@Tax5_Out,@Tax1_Bal,@Tax2_Bal,@Tax3_Bal,@Tax4_Bal,@Tax5_Bal)"
    '                        .Parameters.Add("@Tax1_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax1").ToString
    '                        .Parameters.Add("@Tax2_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax2").ToString
    '                        .Parameters.Add("@Tax3_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax3").ToString
    '                        .Parameters.Add("@Tax4_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax4").ToString
    '                        .Parameters.Add("@Tax5_In", SqlDbType.Float, 8).Value = -DS.Tables("tbl_order").Rows(i).Item("Tax5").ToString
    '                    End With

    '                    SetSQLString = strSQL
    '                    SetCommandType = DBType_SQLServer.enuCommandType.Text
    '                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '                    connectDB()
    '                    EXEC_Command()

    '                Next

    '            End If
    '            ' ****************************
    '            DS.Tables("tbl_order").Clear()
    '            ' ****************************

    '            ' ***********************************************

    '            ' ********************************************
    '            strSQL = "  UPDATE tb_Order "
    '            strSQL &= " SET status =-1  ,cancel_date=getdate(),cancel_by='" & WV_UserName & "',cancel_branch='" & WV_Branch_ID & "' "
    '            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "

    '            With SQLServerCommand
    '                .CommandText = strSQL
    '                .ExecuteNonQuery()
    '            End With


    '            strSQL = " SELECT    *"
    '            strSQL &= "     FROM tb_Order "
    '            strSQL &= "     WHERE Order_Index ='" & Order_Index & "' "
    '            With SQLServerCommand

    '                .Connection = Connection
    '                .Transaction = myTrans
    '                .CommandText = strSQL
    '                .CommandTimeout = 0

    '            End With

    '            DataAdapter.SelectCommand = SQLServerCommand
    '            DataAdapter.SelectCommand.Transaction = myTrans

    '            DS = New DataSet
    '            DataAdapter.Fill(DS, "tbl_order")

    '            Dim oAudit_Log As New Sy_Audit_Log
    '            oAudit_Log.Document_Index = Order_Index
    '            oAudit_Log.Document_No = DS.Tables("tbl_order").Rows(0).Item("Order_No").ToString
    '            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_Receiving)
    '            DS.Tables("tbl_order").Clear()


    '            strSQL = "  UPDATE tb_OrderItem "
    '            strSQL &= " SET status =-1  "
    '            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "


    '            With SQLServerCommand
    '                .CommandText = strSQL
    '                .ExecuteNonQuery()
    '            End With

    '            strSQL = "UPDATE tb_OrderItemLocation "
    '            strSQL &= " SET status =-1 "
    '            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "

    '            SetSQLString = strSQL
    '            SetCommandType = DBType_SQLServer.enuCommandType.Text
    '            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '            EXEC_Command()


    '            ' Update tb_AssetLocationBalance*********************************
    '            strSQL = "  UPDATE tb_AssetLocationBalance "
    '            strSQL &= " SET status =-1  "
    '            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "


    '            With SQLServerCommand
    '                .CommandText = strSQL
    '                .ExecuteNonQuery()
    '            End With

    '            ' *************************************
    '            ' Current System Use tb_Order with tb_JobOrder by 1:1  
    '            '  Value of in  tb_JobOrder.JobOrder_Index field  >> tb_JobOrder.JobOrder_Index =tb_Order.Order_Index 
    '            strSQL = "  UPDATE tb_JobOrder "
    '            strSQL &= " SET status =-1  "
    '            strSQL &= " WHERE JobOrder_Index ='" & Order_Index & "' "

    '            With SQLServerCommand
    '                .CommandText = strSQL
    '                .ExecuteNonQuery()
    '            End With
    '            ' ************************************

    '            strSQL = "  UPDATE tb_OrderItemLocation "
    '            strSQL &= " SET status =-1  "
    '            strSQL &= " WHERE Order_Index ='" & Order_Index & "' "

    '            With SQLServerCommand
    '                .CommandText = strSQL
    '                .ExecuteNonQuery()
    '            End With
    '            ' ********************************************


    '            '*** Commit transaction
    '            myTrans.Commit()

    '            Return True

    '        Catch ex As Exception
    '            myTrans.Rollback()
    '            Throw ex

    '        Finally
    '            disconnectDB()
    '        End Try
    '    End Function

    '#End Region

End Class
