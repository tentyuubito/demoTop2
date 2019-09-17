Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_INB_PO_Datalayer

Public Class POTransaction : Inherits DBType_SQLServer

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

    Dim _objtb_PurchaseOrder As New tb_PurchaseOrder
    Dim _objtb_PurchaseOrderItem As New tb_PurchaseOrderItem
    Dim _objtb_PurchaseOrderItemCollection As New List(Of tb_PurchaseOrderItem)

    Dim _objtb_PurchaseOrder_Discount As New tb_PurchaseOrder_Discount
    Dim _objtb_PurchaseOrder_DiscountItem As New List(Of tb_PurchaseOrder_Discount)

    Private _newPurchaseOrder_Index As String

    Dim _objtb_PurchaseOrderOther As New tb_PurchaseOrderOther
    Dim _tb_PurchaseOrderOtherCollection As New List(Of tb_PurchaseOrderOther)

    Public Property newPurchaseOrder_Index() As String
        Get
            Return _newPurchaseOrder_Index
        End Get
        Set(ByVal value As String)
            _newPurchaseOrder_Index = value
        End Set
    End Property

    Private _DisCount_Index As String

    Public Property DisCount_Index() As String
        Get
            Return _DisCount_Index
        End Get
        Set(ByVal value As String)
            _DisCount_Index = value
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

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal objtb_PurchaseOrder As tb_PurchaseOrder, ByVal objtb_PurchaseOrderItemCollection As List(Of tb_PurchaseOrderItem), ByVal objtb_PurchaseOrder_DiscountItem As List(Of tb_PurchaseOrder_Discount), ByVal objtb_PurchaseOrderOtherCollection As List(Of tb_PurchaseOrderOther))
        MyBase.New()
        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
        _objtb_PurchaseOrder = objtb_PurchaseOrder
        _objtb_PurchaseOrderItemCollection = objtb_PurchaseOrderItemCollection

        'discount
        _objtb_PurchaseOrder_DiscountItem = objtb_PurchaseOrder_DiscountItem
        _tb_PurchaseOrderOtherCollection = objtb_PurchaseOrderOtherCollection

    End Sub

#End Region

#Region " SAVE PURCHASE ORDER "

    ''' <summary>
    ''' Save Data: Main function to call insert or update PO.
    ''' </summary>
    ''' <returns>New Purchase Order Index, if success. "", if failed.</returns>
    ''' <remarks> In case of new document, we need to generate new document ID.</remarks>
    Public Function SaveData() As String

        Select Case objStatus
            Case enuOperation_Type.ADDNEW

                Me._newPurchaseOrder_Index = Me.InsertData()

                If Not Me._newPurchaseOrder_Index = "" Then
                    ' Success 
                    Return Me._newPurchaseOrder_Index
                Else
                    ' Not Success 
                    Return ""
                End If

            Case enuOperation_Type.UPDATE
                Me._newPurchaseOrder_Index = Me.UpdateData
                If Not Me._newPurchaseOrder_Index = "" Then
                    ' Success 
                    Return Me._newPurchaseOrder_Index
                Else
                    ' Not Success 
                    Return ""
                End If

            Case Else
                Return ""
        End Select


        'Return True
    End Function

#End Region

#Region " ADD NEW PURCHASE ORDER "

    ''' <summary>
    ''' Insert tb_PurchaseOrder and tb_PurchaseOrderItem data
    ''' </summary>
    ''' <returns>New PurchaseOrder_Index, if success. "", if failed.</returns>
    ''' <remarks>
    ''' Updated by: Ja 4 Jan 2010 - Comment out the old process of auto number generation.
    ''' </remarks>
    Private Function InsertData() As String
        Dim strSQL As String = ""
        Dim strSQL1 As String = ""
        Dim strSQL2 As String = ""
        Dim strSQLOrderOther As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Insert Header : tb_PurchaseOrder
        ' --- STEP 2: Insert Line Item: tb_PurchaseOrderItem
        ' --- STEP 2.1: Insert Line Item : tb_PurchaseOrder_Discount
        ' --- STEP 2.2: Insert Line Item : tb_PurchaseOrderOther
        ' --- STEP 3: Get New PurchaseOrder_Index
        ' --- STEP 4: Update New PurchaseOrder_Index with tb_PurchaseOrder
        ' --- STEP 5: Update New PurchaseOrder_Index with tb_PurchaseOrderItem
        ' --- STEP 5.1: Update New PurchaseOrder_Index to tb_PurchaseOrder_Discount
        ' --- STEP 5.2: Update New PurchaseOrder_Index to tb_PurchaseOrderOther
        ' --- STEP 6: Add Log to Sy_Audit_Log

        Try

            'If Trim(_objtb_PurchaseOrder.PurchaseOrder_No) = "" Then
            '    Dim objDocumentNumber As New Sy_DocumentNumber
            '    _objtb_PurchaseOrder.PurchaseOrder_No = (objDocumentNumber.getAuto_DocumentNumber("PO0", "PurchaseOrder_No", "tb_PurchaseOrder"))
            '    objDocumentNumber = Nothing
            'End If
            ''Dim strPO_Prefix As String = ""

            ''Select Case _objtb_PurchaseOrder.PurchaseOrder_Prefix.ToUpper
            ''    Case "PN0"
            ''        strPO_Prefix = "PN"
            ''    Case "PO0"
            ''        strPO_Prefix = "PO"
            ''    Case Else
            ''        strPO_Prefix = ""
            ''End Select

            ' '' --- Auto Running (User not defind PO No.)
            ''If strPO_Prefix <> "" Then
            ''    Dim objSy_AutoNumber As New Sy_AutoNumber
            ''    _objtb_PurchaseOrder.PurchaseOrder_No = strPO_Prefix & "0" & objSy_AutoNumber.SelectMax_Sys_Value("PurchaseOrder_" & strPO_Prefix, True)
            ''    objSy_AutoNumber = Nothing

            ''End If
            _objtb_PurchaseOrder.PurchaseOrder_Index = New Sy_AutoNumber().getSys_Value("PurchaseOrder_Index")

            ' --- STEP 1: Insert Header : tb_PurchaseOrder 
            strSQL = " INSERT INTO tb_PurchaseOrder(PurchaseOrder_Index,PurchaseOrder_No,PurchaseOrder_Date,Carrier_Index,Customer_Receive_Location_Index,Expected_Delivery_Date,Delivery_Date,Customer_Index,Supplier_Index,Department_Index,DocumentType_Index,Remark,Credit_Term,Currency_Index,Exchange_Rate,PaymentMethod_Index,Payment_Ref,FullPaid_Date,Amount,Discount_Percent,Discount_Amt,Deposit_Amt,Total_Amt,VAT_Percent,VAT,Net_Amt,Supplier_Address,Supplier_Tel,Supplier_Fax,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Status)" & _
            "       VALUES(@PurchaseOrder_Index,@PurchaseOrder_No,@PurchaseOrder_Date,@Carrier_Index,@Customer_Receive_Location_Index,@Expected_Delivery_Date,@Delivery_Date,@Customer_Index,@Supplier_Index,@Department_Index,@DocumentType_Index,@Remark,@Credit_Term,@Currency_Index,@Exchange_Rate,@PaymentMethod_Index,@Payment_Ref,@FullPaid_Date,@Amount,@Discount_Percent,@Discount_Amt,@Deposit_Amt,@Total_Amt,@VAT_Percent,@VAT,@Net_Amt,@Supplier_Address,@Supplier_Tel,@Supplier_Fax,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Status)"

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.PurchaseOrder_Index
                .Parameters.Add("@PurchaseOrder_No", SqlDbType.VarChar, 50).Value = _objtb_PurchaseOrder.PurchaseOrder_No
                .Parameters.Add("@PurchaseOrder_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.PurchaseOrder_Date.ToString("yyyy/MM/dd")
                .Parameters.Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Carrier_Index
                .Parameters.Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Customer_Receive_Location_Index
                .Parameters.Add("@Expected_Delivery_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.Expected_Delivery_Date
                .Parameters.Add("@Delivery_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.Delivery_Date
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Supplier_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.DocumentType_Index
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _objtb_PurchaseOrder.Remark
                .Parameters.Add("@Credit_Term", SqlDbType.NVarChar, 10).Value = _objtb_PurchaseOrder.Credit_Term
                .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Currency_Index
                .Parameters.Add("@Exchange_Rate", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Exchange_Rate
                .Parameters.Add("@PaymentMethod_Index", SqlDbType.NVarChar, 13).Value = _objtb_PurchaseOrder.PaymentMethod_Index
                .Parameters.Add("@Payment_Ref", SqlDbType.NVarChar, 50).Value = _objtb_PurchaseOrder.Payment_Ref
                .Parameters.Add("@FullPaid_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.FullPaid_Date
                .Parameters.Add("@Amount", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Amount
                .Parameters.Add("@Discount_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrder.Discount_Percent
                .Parameters.Add("@Discount_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Discount_Amt
                .Parameters.Add("@Deposit_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Deposit_Amt
                .Parameters.Add("@Total_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Total_Amt
                .Parameters.Add("@VAT_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrder.VAT_Percent
                .Parameters.Add("@VAT", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.VAT
                .Parameters.Add("@Net_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Net_Amt
                .Parameters.Add("@Supplier_Address", SqlDbType.NVarChar, 1000).Value = _objtb_PurchaseOrder.Supplier_Address
                .Parameters.Add("@Supplier_Tel", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Supplier_Tel
                .Parameters.Add("@Supplier_Fax", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Supplier_Fax
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrder.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrder.Str10
                .Parameters.Add("@Flo1", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = _objtb_PurchaseOrder.add_by
                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = _objtb_PurchaseOrder.Status
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Insert Line Item: PurchaseOrderItem 
            For Each _objtb_PurchaseOrderItem In _objtb_PurchaseOrderItemCollection

                strSQL1 = " INSERT INTO tb_PurchaseOrderItem(PurchaseOrderItem_Index,Item_Seq,PurchaseOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Received_Qty,Received_Qty,Received_Weight,Received_Volume" ',Last_Received_Date
                strSQL1 &= " ,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Percent_Over_Allow,Percent_Under_Allow)" & _
                   " VALUES(@PurchaseOrderItem_Index,@Item_Seq,@PurchaseOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Received_Qty,@Received_Qty,@Received_Weight,@Received_Volume" ',@Last_Received_Date
                strSQL1 &= ",@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Remark,@Reason,@Ref_No1,@Ref_No2,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Percent_Over_Allow,@Percent_Under_Allow)"

                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderItem.PurchaseOrderItem_Index
                    .Parameters.Add("@Item_Seq", SqlDbType.Int, 4).Value = _objtb_PurchaseOrderItem.Item_Seq
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.PurchaseOrder_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderItem.Sku_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderItem.Package_Index
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Ratio
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Total_Qty
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Volume
                    .Parameters.Add("@Serial_No", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Total_Received_Qty
                    .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Received_Qty
                    .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Received_Weight
                    .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Received_Volume
                    ' .Parameters.Add("@Last_Received_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrderItem.Last_Received_Date
                    .Parameters.Add("@UnitPrice", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.UnitPrice
                    .Parameters.Add("@Amount", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Amount
                   
                    .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Currency_Index
                    .Parameters.Add("@Discount_Amt", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Discount_Amt
                    .Parameters.Add("@Total_Amt", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Total_Amt
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = _objtb_PurchaseOrderItem.Status
                    .Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = _objtb_PurchaseOrderItem.Remark
                    .Parameters.Add("@Reason", SqlDbType.NVarChar, 1000).Value = "" '_objtb_PurchaseOrderItem.Reason
                    .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str5
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = ""
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrderItem.Str10
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    '''''add Percent
                    .Parameters.Add("@Percent_Over_Allow", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Percent_Over_Allow
                    .Parameters.Add("@Percent_Under_Allow", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Percent_Under_Allow
                    '''''
                End With

                SetSQLString = strSQL1
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next

            ' --- STEP 2.1: Insert Line Item: tb_PurchaseOrder_Discount 
            For Each _objtb_PurchaseOrder_Discount In _objtb_PurchaseOrder_DiscountItem

                Dim objSy_AutoNumber As New Sy_AutoNumber
                Me._DisCount_Index = objSy_AutoNumber.getSys_Value("DisCount_Index")
                objSy_AutoNumber = Nothing

                strSQL2 = " INSERT INTO tb_PurchaseOrder_Discount(DisCount_Index,PurchaseOrder_Index,Discount_Seq,Discount_Type,Discount_Amount1,Discount_Amount2,Discount_Total)" '
                strSQL2 &= " VALUES(@DisCount_Index,@PurchaseOrder_Index,@Discount_Seq,@Discount_Type,@Discount_Amount1,@Discount_Amount2,@Discount_Total)" '@PurchaseOrder_Index,
                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@DisCount_Index", SqlDbType.VarChar, 13).Value = _DisCount_Index
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder_Discount.PurchaseOrder_Index
                    .Parameters.Add("@Discount_Seq", SqlDbType.Int, 10).Value = _objtb_PurchaseOrder_Discount.Discount_Seq
                    .Parameters.Add("@Discount_Type", SqlDbType.SmallInt, 5).Value = _objtb_PurchaseOrder_Discount.Discount_Type
                    .Parameters.Add("@Discount_Amount1", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder_Discount.Discount_Amount1
                    .Parameters.Add("@Discount_Amount2", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder_Discount.Discount_Amount2
                    .Parameters.Add("@Discount_Total", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder_Discount.Discount_Total

                End With

                SetSQLString = strSQL2
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next

            ' --- STEP 2.2: Insert Line Item : tb_PurchaseOrderOther

            For Each _objtb_PurchaseOrderOther In _tb_PurchaseOrderOtherCollection

                strSQLOrderOther = " INSERT INTO tb_PurchaseOrderOther(PurchaseOrderOther_Index,PurchaseOrder_Index,Seq,Description,Amount,Discount_Percent,Discount_Amt,Deposit_Amt,Total_Amt,VAT_Percent,VAT,Net_Amt,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Status)"
                strSQLOrderOther &= " VALUES(@PurchaseOrderOther_Index,@PurchaseOrder_Index,@Seq,@Description,@Amount,@Discount_Percent,@Discount_Amt,@Deposit_Amt,@Total_Amt,@VAT_Percent,@VAT,@Net_Amt,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Status)"

                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@PurchaseOrderOther_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderOther.PurchaseOrderOther_Index
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderOther.PurchaseOrder_Index
                    .Parameters.Add("@Seq", SqlDbType.Int, 10).Value = _objtb_PurchaseOrderOther.Seq
                    .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = _objtb_PurchaseOrderOther.Description
                    .Parameters.Add("@Amount", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Amount
                    .Parameters.Add("@Discount_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrderOther.Discount_Percent
                    .Parameters.Add("@Discount_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Discount_Amt
                    .Parameters.Add("@Deposit_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Deposit_Amt
                    .Parameters.Add("@Total_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Total_Amt
                    .Parameters.Add("@VAT_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrderOther.VAT_Percent
                    .Parameters.Add("@VAT", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.VAT
                    .Parameters.Add("@Net_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Net_Amt
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = ""
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = ""
                    .Parameters.Add("@Flo1", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 15).Value = 0
                    .Parameters.Add("@Flo4", SqlDbType.Float, 15).Value = 0
                    .Parameters.Add("@Flo5", SqlDbType.Float, 15).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    .Parameters.Add("@Status", SqlDbType.Int, 10).Value = 1
                End With

                SetSQLString = strSQLOrderOther
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next

            ' --- STEP 3: Get New PurchaseOrder_Index 
            'Dim objDBIndex As New Sy_AutoNumber
            'Me._newPurchaseOrder_Index = objDBIndex.getSys_Value("PurchaseOrder_Index")
            'objDBIndex = Nothing

            ' --- STEP 4: Update New PurchaseOrder_Index to tb_PurchaseOrder
            ' --- and status = ÃÍÂ×¹ÂÑ¹
            'strSQL = " UPDATE tb_PurchaseOrder SET  "
            'strSQL &= " PurchaseOrder_Index='" & Me._newPurchaseOrder_Index & "' ,Status=1 "
            'strSQL &= " WHERE PurchaseOrder_Index='" & Me._objtb_PurchaseOrder.PurchaseOrder_Index & "'"

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()


            '' --- STEP 5: Update New PurchaseOrder_Index with tb_PurchaseOrderItem
            '' --- and status = ÃÍÂ×¹ÂÑ¹
            'strSQL = " UPDATE tb_PurchaseOrderItem SET  "
            'strSQL &= " PurchaseOrder_Index ='" & Me._newPurchaseOrder_Index & "' ,Status=1 "
            'strSQL &= " WHERE PurchaseOrder_Index='" & Me._objtb_PurchaseOrder.PurchaseOrder_Index & "'"

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()

            '' --- STEP 5.1: Update New PurchaseOrder_Index to tb_PurchaseOrder_Discount
            'strSQL = " UPDATE tb_PurchaseOrder_Discount SET  "
            'strSQL &= " PurchaseOrder_Index='" & Me._newPurchaseOrder_Index & "' "
            'strSQL &= " WHERE PurchaseOrder_Index='" & Me._objtb_PurchaseOrder_Discount.PurchaseOrder_Index & "' "

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()

            '' --- STEP 5.2: Update New PurchaseOrder_Index to tb_PurchaseOrderOther

            'strSQL = " UPDATE tb_PurchaseOrderOther SET  "
            'strSQL &= " PurchaseOrder_Index='" & Me._newPurchaseOrder_Index & "' "
            'strSQL &= " WHERE PurchaseOrder_Index='" & Me._objtb_PurchaseOrder.PurchaseOrder_Index & "'"

            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            'EXEC_Command()

            '' --- STEP 6: Add Log to Sy_Audit_Log
            'Dim oAudit_Log As New Sy_Audit_Log
            'oAudit_Log.Document_Index = _newPurchaseOrder_Index
            'oAudit_Log.Document_No = _objtb_PurchaseOrder.PurchaseOrder_No
            'oAudit_Log.Ref_No1 = _objtb_PurchaseOrder.Str1
            'oAudit_Log.Ref_No1 = _objtb_PurchaseOrder.Str2
            'oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Create_PO, Connection, myTrans)


            ' --- STEP 7: Update Sy_AutoNumber "PurchaseOrder_No"
            'If strPO_Prefix <> "" Then

            '    strSQL = " UPDATE Sy_AutoNumber SET  "
            '    strSQL &= " Sys_Value = '" & _objtb_PurchaseOrder.PurchaseOrder_No & "'"
            '    strSQL &= " WHERE Sys_Key = 'PurchaseOrder_" & strPO_Prefix & "' "

            '    SetSQLString = strSQL
            '    SetCommandType = DBType_SQLServer.enuCommandType.Text
            '    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            '    EXEC_Command()

            'End If

            ' --- Commit transaction
            Me._newPurchaseOrder_Index = Me._objtb_PurchaseOrder.PurchaseOrder_Index
            myTrans.Commit()
            Return Me._newPurchaseOrder_Index

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

#End Region

#Region " UPDATE PURCHASE ORDER "

    ''' <summary>
    ''' Update Purchase Order and Purchase Order Item Data
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>
    ''' Updated by: Todd - 3 Jan 2010
    ''' Note 1) In updating Purchase Order Item, we will skip the following fields.
    '''  *** Received_Qty, Total_Received_Qty, Received_Weight, Received_Volume, Last_Received_Date
    '''  This is because these fields should be updated from other module (Receiving process)
    ''' 
    ''' Note 2) This function returns NO VALUE. Must check!
    ''' 
    ''' Updated by: Todd - 4 Jan 2010
    ''' We found that the update_by fields never get updated. Make this change.
    ''' </remarks>    
    Private Function UpdateData() As String
        Dim strSQL As String = ""
        Dim strSQL1 As String = ""
        Dim strSQL2 As String = ""
        Dim strSQLOrderOther As String = ""
        Dim blnIsOldItem As Boolean = False

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update Header : tb_PurchaseOrder
        ' --- STEP 2: Loop to update (or insert if item exists) tb_PurchaseOrderItem
        ' --- STEP 3: Loop to update (or insert if item exists) tb_PurchaseOrder_Discount
        ' --- STEP 4: Loop to update (or insert if item exists) tb_PurchaseOrderOther
        Try

            ' --- STEP 1: Update Header : tb_PurchaseOrder
            strSQL = " Update tb_PurchaseOrder set PurchaseOrder_No=@PurchaseOrder_No "
            strSQL &= ",PurchaseOrder_Date=@PurchaseOrder_Date,Carrier_Index=@Carrier_Index,Customer_Receive_Location_Index=@Customer_Receive_Location_Index"
            strSQL &= ",Expected_Delivery_Date=@Expected_Delivery_Date,Delivery_Date=@Delivery_Date,Customer_Index=@Customer_Index,Supplier_Index=@Supplier_Index"
            strSQL &= ",Department_Index=@Department_Index,DocumentType_Index=@DocumentType_Index,Remark=@Remark,Credit_Term=@Credit_Term,Currency_Index=@Currency_Index,"
            strSQL &= " Exchange_Rate=@Exchange_Rate,PaymentMethod_Index=@PaymentMethod_Index,Payment_Ref=@Payment_Ref,FullPaid_Date=@FullPaid_Date,Amount=@Amount,Discount_Percent=@Discount_Percent"
            strSQL &= ",Discount_Amt=@Discount_Amt,Deposit_Amt=@Deposit_Amt,Total_Amt=@Total_Amt,VAT_Percent=@VAT_Percent,VAT=@VAT,Net_Amt=@Net_Amt,Supplier_Address=@Supplier_Address,Supplier_Tel=@Supplier_Tel"
            strSQL &= ",Supplier_Fax=@Supplier_Fax,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,Str6=@Str6,Str7=@Str7,Str8=@Str8,Str9=@Str9,Str10=@Str10,Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5"
            strSQL &= ",update_by=@update_by,update_date=@update_date,update_branch=@update_branch,Status=@Status"
            strSQL &= " WHERE PurchaseOrder_Index='" & _objtb_PurchaseOrder.PurchaseOrder_Index & "' "

            ' ------ Todd 4 Jan 2010 - Fix bug. When update, we have to add "update_by, update_branch, update_date
            'strSQL &= ",add_by=@add_by,add_branch=@add_branch,Status=@Status"
            'strSQL &= " WHERE PurchaseOrder_Index='" & _objtb_PurchaseOrder.PurchaseOrder_Index & "' "

            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.PurchaseOrder_Index
                .Parameters.Add("@PurchaseOrder_No", SqlDbType.VarChar, 50).Value = _objtb_PurchaseOrder.PurchaseOrder_No
                .Parameters.Add("@PurchaseOrder_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.PurchaseOrder_Date
                .Parameters.Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Carrier_Index
                .Parameters.Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Customer_Receive_Location_Index
                .Parameters.Add("@Expected_Delivery_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.Expected_Delivery_Date
                .Parameters.Add("@Delivery_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.Delivery_Date
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Supplier_Index
                .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Department_Index
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.DocumentType_Index
                .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _objtb_PurchaseOrder.Remark
                .Parameters.Add("@Credit_Term", SqlDbType.NVarChar, 10).Value = _objtb_PurchaseOrder.Credit_Term
                .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Currency_Index
                .Parameters.Add("@Exchange_Rate", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Exchange_Rate
                .Parameters.Add("@PaymentMethod_Index", SqlDbType.NVarChar, 13).Value = _objtb_PurchaseOrder.PaymentMethod_Index
                .Parameters.Add("@Payment_Ref", SqlDbType.NVarChar, 50).Value = _objtb_PurchaseOrder.Payment_Ref
                .Parameters.Add("@FullPaid_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrder.FullPaid_Date
                .Parameters.Add("@Amount", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Amount
                .Parameters.Add("@Discount_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrder.Discount_Percent
                .Parameters.Add("@Discount_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Discount_Amt
                .Parameters.Add("@Deposit_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Deposit_Amt
                .Parameters.Add("@Total_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Total_Amt
                .Parameters.Add("@VAT_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrder.VAT_Percent
                .Parameters.Add("@VAT", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.VAT
                .Parameters.Add("@Net_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Net_Amt
                .Parameters.Add("@Supplier_Address", SqlDbType.NVarChar, 1000).Value = _objtb_PurchaseOrder.Supplier_Address
                .Parameters.Add("@Supplier_Tel", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Supplier_Tel
                .Parameters.Add("@Supplier_Fax", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Supplier_Fax
                .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrder.Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrder.Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrder.Str10
                .Parameters.Add("@Flo1", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder.Flo5
                .Parameters.Add("@update_date", SqlDbType.DateTime, 23).Value = Now()
                .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = _objtb_PurchaseOrder.Status
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Loop to update (or insert if item exists) tb_PurchaseOrderItem
            For Each _objtb_PurchaseOrderItem In _objtb_PurchaseOrderItemCollection
                blnIsOldItem = IsExist_PurchaseOrderItem(_objtb_PurchaseOrderItem.PurchaseOrderItem_Index)
                If blnIsOldItem Then

                    ' --- Update tb_PurchaseOrderItem

                    ' --- Fix Bug: Todd 3 Jan 2010
                    ' --- We must skip the following fields in case of update, 
                    ' --- because these fields should be updated from other modules (Receiving)
                    ' --- ***** Received_Qty, Total_Received_Qty, Received_Weight, Received_Volume, Last_Received_Date

                    ' --- Fix Bug: Todd 4 Jan 2010 - When update, we have to add "update_by, update_branch, update_date

                    'strSQL1 = " UPDATE  tb_PurchaseOrderItem SET Item_Seq=@Item_Seq,PurchaseOrder_Index=@PurchaseOrder_Index,Sku_Index=@Sku_Index,Package_Index=@Package_Index,Ratio=@Ratio,Total_Qty=@Total_Qty,Qty=@Qty,Weight=@Weight,Volume=@Volume,Serial_No=@Serial_No,Total_Received_Qty=@Total_Received_Qty,Received_Qty=@Received_Qty,Received_Weight=@Received_Weight,Received_Volume=@Received_Volume" ',Last_Received_Date
                    'strSQL1 &= " ,UnitPrice=@UnitPrice,Amount=@Amount,Currency_Index=@Currency_Index,Discount_Amt=@Discount_Amt,Total_Amt=@Total_Amt,Status=@Status,Remark=@Remark,Reason=@Reason,Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,Str6=@Str6,Str7=@Str7,Str8=@Str8,Str9=@Str9,Str10=@Str10,Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,add_by=@add_by,add_branch=@add_branch"
                    'strSQL1 &= " WHERE PurchaseOrderItem_Index='" & _objtb_PurchaseOrderItem.PurchaseOrderItem_Index & "' "
                    strSQL1 = " UPDATE  tb_PurchaseOrderItem SET Item_Seq=@Item_Seq,PurchaseOrder_Index=@PurchaseOrder_Index,Sku_Index=@Sku_Index,Package_Index=@Package_Index,Ratio=@Ratio,Total_Qty=@Total_Qty,Qty=@Qty,Weight=@Weight,Volume=@Volume,Serial_No=@Serial_No" ',Total_Received_Qty=@Total_Received_Qty,Received_Qty=@Received_Qty,Received_Weight=@Received_Weight,Received_Volume=@Received_Volume" ',Last_Received_Date
                    strSQL1 &= " ,UnitPrice=@UnitPrice,Amount=@Amount,Currency_Index=@Currency_Index,Discount_Amt=@Discount_Amt,Total_Amt=@Total_Amt,Status=@Status,Remark=@Remark,Reason=@Reason,Ref_No1=@Ref_No1,Ref_No2=@Ref_No2,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,Str6=@Str6,Str7=@Str7,Str8=@Str8,Str9=@Str9,Str10=@Str10,Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5,update_by=@update_by,update_branch=@update_branch,update_date=@update_date,Percent_Over_Allow=@Percent_Over_Allow,Percent_Under_Allow=@Percent_Under_Allow"
                    strSQL1 &= " WHERE PurchaseOrderItem_Index='" & _objtb_PurchaseOrderItem.PurchaseOrderItem_Index & "' "

                Else
                    ' --- Insert New Item in tb_PurchaseOrderItem
                    strSQL1 = " INSERT INTO tb_PurchaseOrderItem(PurchaseOrderItem_Index,Item_Seq,PurchaseOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Received_Qty,Received_Qty,Received_Weight,Received_Volume" ',Last_Received_Date
                    strSQL1 &= " ,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Percent_Over_Allow,Percent_Under_Allow)" & _
                                " VALUES(@PurchaseOrderItem_Index,@Item_Seq,@PurchaseOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Received_Qty,@Received_Qty,@Received_Weight,@Received_Volume" ',@Last_Received_Date
                    strSQL1 &= ",@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Remark,@Reason,@Ref_No1,@Ref_No2,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Percent_Over_Allow,@Percent_Under_Allow)"
                End If
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderItem.PurchaseOrderItem_Index
                    .Parameters.Add("@Item_Seq", SqlDbType.Int, 4).Value = _objtb_PurchaseOrderItem.Item_Seq
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.PurchaseOrder_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderItem.Sku_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderItem.Package_Index
                    .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = 1
                    .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Total_Qty
                    .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Qty
                    .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Weight
                    .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Volume
                    .Parameters.Add("@Serial_No", SqlDbType.NVarChar, 100).Value = ""
                    .Parameters.Add("@Percent_Over_Allow", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Percent_Over_Allow
                    .Parameters.Add("@Percent_Under_Allow", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Percent_Under_Allow


                    If Not blnIsOldItem Then
                        ' We add these fields only in case of insertion.
                        .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Total_Received_Qty
                        .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Received_Qty
                        .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Received_Weight
                        .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Received_Volume
                        '.Parameters.Add("@Last_Received_Date", SqlDbType.DateTime, 23).Value = _objtb_PurchaseOrderItem.Last_Received_Date
                    End If
                    .Parameters.Add("@UnitPrice", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.UnitPrice
                    .Parameters.Add("@Amount", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Amount
                    .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder.Currency_Index
                    .Parameters.Add("@Discount_Amt", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Discount_Amt
                    .Parameters.Add("@Total_Amt", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Total_Amt
                    .Parameters.Add("@Status", SqlDbType.Int, 4).Value = _objtb_PurchaseOrderItem.Status
                    .Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = _objtb_PurchaseOrderItem.Remark
                    .Parameters.Add("@Reason", SqlDbType.NVarChar, 1000).Value = _objtb_PurchaseOrderItem.Reason
                    .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objtb_PurchaseOrderItem.Ref_No1
                    .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objtb_PurchaseOrderItem.Ref_No2
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str5
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderItem.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrderItem.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrderItem.Str10
                    .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = _objtb_PurchaseOrderItem.Flo5
                    If Not blnIsOldItem Then
                        ' Case insert
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    Else
                        ' Case update
                        .Parameters.Add("@update_date", SqlDbType.DateTime, 23).Value = Now()
                        .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    End If
                End With

                SetSQLString = strSQL1
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next

            ' --- STEP 3: Loop to update (or insert if item exists) tb_PurchaseOrder_Discount

            For Each _objtb_PurchaseOrder_Discount In _objtb_PurchaseOrder_DiscountItem

                If IsExist_PurchaseOrderDiscount(_objtb_PurchaseOrderItem.PurchaseOrder_Index) Then
                    ' --- Update New Item in tb_PurchaseOrder_Discount
                    strSQL2 = " UPDATE  tb_PurchaseOrder_Discount SET "
                    strSQL2 &= " DisCount_Index=@DisCount_Index,Discount_Seq=@Discount_Seq,Discount_Type=@Discount_Type,Discount_Amount1=@Discount_Amount1,Discount_Amount2=@Discount_Amount2,Discount_Total= @Discount_Total"
                    strSQL2 &= " WHERE PurchaseOrderItem_Index='" & _objtb_PurchaseOrder_Discount.PurchaseOrder_Index & "' "

                Else
                    ' --- Insert New Item in tb_PurchaseOrder_Discount
                    strSQL2 = " INSERT INTO tb_PurchaseOrder_Discount(DisCount_IndexPurchaseOrder_Index,Discount_Seq,Discount_Type,Discount_Amount1,Discount_Amount2,Discount_Total)"
                    strSQL2 &= " VALUES(@DisCount_Index,@PurchaseOrder_Index,@Discount_Seq,@Discount_Type,@Discount_Amount1,@Discount_Amount2,@Discount_Total)"
                End If

                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@DisCount_Index", SqlDbType.VarChar, 13).Value = _DisCount_Index
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrder_Discount.PurchaseOrder_Index
                    .Parameters.Add("@Discount_Seq", SqlDbType.Int, 10).Value = _objtb_PurchaseOrder_Discount.Discount_Seq
                    .Parameters.Add("@Discount_Type", SqlDbType.SmallInt, 5).Value = _objtb_PurchaseOrder_Discount.Discount_Type
                    .Parameters.Add("@Discount_Amount1", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder_Discount.Discount_Amount1
                    .Parameters.Add("@Discount_Amount2", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder_Discount.Discount_Amount2
                    .Parameters.Add("@Discount_Total", SqlDbType.Float, 15).Value = _objtb_PurchaseOrder_Discount.Discount_Total

                End With
                SetSQLString = strSQL2
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next

            ' --- STEP 4: Loop to update (or insert if item exists) tb_PurchaseOrderOther
            For Each _objtb_PurchaseOrderOther In _tb_PurchaseOrderOtherCollection
                blnIsOldItem = IsExist_PurchaseOrderOther(_objtb_PurchaseOrderOther.PurchaseOrderOther_Index)
                If blnIsOldItem Then

                    strSQLOrderOther = " UPDATE  tb_PurchaseOrderOther SET "
                    strSQLOrderOther &= "  PurchaseOrderOther_Index=@PurchaseOrderOther_Index,PurchaseOrder_Index=@PurchaseOrder_Index,Seq=@Seq"
                    strSQLOrderOther &= " ,Description=@Description,Amount=@Amount,Discount_Percent= @Discount_Percent"
                    strSQLOrderOther &= " ,Discount_Amt=@Discount_Amt,Deposit_Amt=@Deposit_Amt,Total_Amt=@Total_Amt "
                    strSQLOrderOther &= " ,VAT_Percent =@VAT_Percent,VAT=@VAT,Net_Amt=@Net_Amt"
                    strSQLOrderOther &= ",Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5,Str6 =@Str6,Str7 =@Str7,Str8 =@Str8,Str9 =@Str9,Str10 =@Str10"
                    strSQLOrderOther &= ",Flo1 = @Flo1,Flo2 = @Flo2,Flo3 = @Flo3,Flo4= @Flo4,Flo5= @Flo5,update_date =@update_date,update_by =@update_by,update_branch = @update_branch,Status =@Status"
                    strSQLOrderOther &= "  WHERE PurchaseOrderOther_Index='" & _objtb_PurchaseOrderOther.PurchaseOrderOther_Index & "' "

                Else
                    strSQLOrderOther = " INSERT INTO tb_PurchaseOrderOther(PurchaseOrderOther_Index,PurchaseOrder_Index,Seq,"
                    strSQLOrderOther &= " Description,Amount,Discount_Percent,Discount_Amt,Deposit_Amt,Total_Amt"
                    strSQLOrderOther &= " ,VAT_Percent,VAT,Net_Amt,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Status)"
                    strSQLOrderOther &= " VALUES(@PurchaseOrderOther_Index,@PurchaseOrder_Index,@Seq,@Description,@Amount,@Discount_Percent,@Discount_Amt,@Deposit_Amt,@Total_Amt,@VAT_Percent,@VAT,@Net_Amt,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Status)"

                End If

                With SQLServerCommand

                    .Parameters.Clear()
                    .Parameters.Add("@PurchaseOrderOther_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderOther.PurchaseOrderOther_Index
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _objtb_PurchaseOrderOther.PurchaseOrder_Index
                    .Parameters.Add("@Seq", SqlDbType.Int, 10).Value = _objtb_PurchaseOrderOther.Seq
                    .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = _objtb_PurchaseOrderOther.Description
                    .Parameters.Add("@Amount", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Amount
                    .Parameters.Add("@Discount_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrderOther.Discount_Percent
                    .Parameters.Add("@Discount_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Discount_Amt
                    .Parameters.Add("@Deposit_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Deposit_Amt
                    .Parameters.Add("@Total_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Total_Amt
                    .Parameters.Add("@VAT_Percent", SqlDbType.Int, 10).Value = _objtb_PurchaseOrderOther.VAT_Percent
                    .Parameters.Add("@VAT", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.VAT
                    .Parameters.Add("@Net_Amt", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Net_Amt
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str1
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str2
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str3
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str4
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str5
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str6
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str7
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _objtb_PurchaseOrderOther.Str8
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrderOther.Str9
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _objtb_PurchaseOrderOther.Str10
                    .Parameters.Add("@Flo1", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo1
                    .Parameters.Add("@Flo2", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo2
                    .Parameters.Add("@Flo3", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo3
                    .Parameters.Add("@Flo4", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo4
                    .Parameters.Add("@Flo5", SqlDbType.Float, 15).Value = _objtb_PurchaseOrderOther.Flo5
                    If Not blnIsOldItem Then
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    Else
                        .Parameters.Add("@update_date", SqlDbType.DateTime, 23).Value = Now()
                        .Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@update_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                    End If
                    .Parameters.Add("@Status", SqlDbType.Int, 10).Value = 1
                End With
                SetSQLString = strSQLOrderOther
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Next
   
            ' --- Commit transaction
            myTrans.Commit()
            Return _objtb_PurchaseOrder.PurchaseOrder_Index
        Catch ex As Exception
            Return ""
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
        'Return ""
    End Function

    ''' <summary>
    ''' Update Purchase Order Status to be "Confirmed".
    ''' </summary>
    ''' <returns>Ture is updated succeeded. False otherwise.</returns>
    ''' <remarks></remarks>
    Public Function Confirm_PO(ByVal pOPurchaseOrder As tb_PurchaseOrder) As Boolean

        Dim strSQL As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        ' --- TRANSACTION SUMMARY ---
        ' --- STEP 1: Update status in tb_PurchaseOrderItem = 2
        ' --- STEP 2: Update status in tb_PurchaseOrder = 2
        ' --- STEP 3: Update status in tb_PurchaseOrderOther = 2
        ' --- STEP 4: Update status in Sy_Audit_Log
        Try

            ' --- STEP 1: Update status in tb_PurchaseOrderItem = 2
            strSQL = "UPDATE tb_PurchaseOrderItem "
            strSQL &= " SET status = 2 "
            strSQL &= " , Update_date =getdate() "
            strSQL &= " , Update_by = '" & WV_UserName & "' "
            strSQL &= " , Update_branch = '" & WV_Branch_ID & "' "
            strSQL &= " WHERE PurchaseOrder_Index ='" & pOPurchaseOrder.PurchaseOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Update status in tb_PurchaseOrder =2
            strSQL = "UPDATE tb_PurchaseOrder "
            strSQL &= " SET status = 2  "
            strSQL &= " , Update_date =getdate() "
            strSQL &= " , Update_by = '" & WV_UserName & "' "
            strSQL &= " , Update_branch = '" & WV_Branch_ID & "' "
            strSQL &= " WHERE PurchaseOrder_Index ='" & pOPurchaseOrder.PurchaseOrder_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 3: Update status in tb_PurchaseOrderOther = 2
            strSQL = "UPDATE tb_PurchaseOrderOther "
            strSQL &= " SET Status = 2 "
            strSQL &= " , Update_date =getdate() "
            strSQL &= " , Update_by = '" & WV_UserName & "' "
            strSQL &= " , Update_branch = '" & WV_Branch_ID & "' "
            strSQL &= " WHERE PurchaseOrder_Index ='" & pOPurchaseOrder.PurchaseOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '' --- STEP 4: Update status in Sy_Audit_Log
            'Dim oAudit_Log As New Sy_Audit_Log
            'oAudit_Log.Document_Index = pOPurchaseOrder.PurchaseOrder_Index
            'oAudit_Log.Document_No = pOPurchaseOrder.PurchaseOrder_No
            'oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Confirm_PO, Connection, myTrans)

            myTrans.Commit()

            'insert log
            Dim obj_cls As New cls_syAditlog
            obj_cls.Process_ID = 9
            obj_cls.Description = "Â×¹ÂÑ¹ : " & Now.ToString("dd/MM/yyyy HH:mm:ssss")
            obj_cls.Document_Index = pOPurchaseOrder.PurchaseOrder_Index
            obj_cls.Document_No = pOPurchaseOrder.PurchaseOrder_No
            obj_cls.Log_Type_ID = 902 'Â×¹ÂÑ¹ PO
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

    ''' <summary>
    ''' Update Purchase Order Status to any status.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks>
    ''' Todd 27 Dec 2009 - Rename sub from "Update_PO" to "UpdatePOStatus".
    ''' </remarks>
    Public Sub UpdatePOStatus(ByVal PurchaseOrder_Index As String, ByVal DocStatus As Integer)

        Dim strSQL As String = ""
        Dim strSQL1 As String = ""
        Dim strSQLPurchaseOrderOther As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try

            ' --- STEP 1: Update status in tb_PurchaseOrderItem = DocStatus
            strSQL = "UPDATE tb_PurchaseOrderItem "
            strSQL += " SET status = " & DocStatus & " "
            strSQL += " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Update status in tb_PurchaseOrder = DocStatus
            strSQL1 = "UPDATE tb_PurchaseOrder "
            strSQL1 += " SET status = " & DocStatus & " "
            strSQL1 += " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "'"


            SetSQLString = strSQL1
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

#Region " DELETE DATA "

    ''' <summary>
    ''' Physical deletion of PO Items by passing "PurchaseOrderItem_Index"
    ''' </summary>
    ''' <param name="PurchaseOrderItem_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd 26 Dec 2009 - Change Sub Name to make more sense
    ''' from "Delete_Purchase" -> "Delete_PurchaseOrderItem"
    ''' </remarks>
    Public Sub Delete_PurchaseOrderItem(ByVal PurchaseOrderItem_Index As String, _
                                            Optional ByVal Connect As SqlClient.SqlConnection = Nothing, _
                                          Optional ByVal myTrans As SqlClient.SqlTransaction = Nothing)


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



            DBExeNonQuery(String.Format(" sp_Delete_PurchaseOrderItem '{0}','{1}' ", PurchaseOrderItem_Index, WV_UserName), Connect, myTrans)




            If isTran = False Then
                myTrans.Commit()
                disconnectDB()
            End If

        Catch ex As Exception
            If isTran = False Then
                myTrans.Rollback()
                disconnectDB()
            End If
            Throw ex
        End Try



    End Sub
    'Public Sub Delete_PurchaseOrderItem(ByVal PurchaseOrderItem_Index As String)
    '    Dim strSQL As String = ""
    '    Try
    '        strSQL = "DELETE FROM tb_PurchaseOrderItem "
    '        strSQL &= " WHERE PurchaseOrderItem_Index ='" & PurchaseOrderItem_Index & "' "

    '        connectDB()
    '        SetSQLString = strSQL
    '        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '        EXEC_Command()


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try

    'End Sub
    ' Public Function Delete_PurchaseOrderOther(ByVal PurchaseOrderOther_Index As String) As Boolean
    ''' <summary>
    ''' Physical deletion of PO Other items by passing "PurchaseOrderOther_Index"
    ''' </summary>
    ''' <param name="PurchaseOrderOther_Index"></param>
    ''' <remarks></remarks>
    Public Sub Delete_PurchaseOrderOther(ByVal PurchaseOrderOther_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = "DELETE FROM tb_PurchaseOrderOther "
            strSQL &= " WHERE PurchaseOrderOther_Index ='" & PurchaseOrderOther_Index & "' "

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

#End Region

#Region " SELECT DATA "

    ''' <summary>
    ''' Get Purchase Order data
    ''' through VIEW_PurchaseOrderHeader
    ''' passing PurchaseOrder_Index
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Public Sub getPurchaseOrderHeader(ByVal PurchaseOrder_Index As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "SELECT * FROM VIEW_PurchaseOrderHeader"
            strSQL &= " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "

            strSQL = strSQL

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
    ''' Get Purchase Order data
    ''' directly from tb_PurchaseOrder
    ''' passing PurchaseOrder_Index
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Public Sub PurchaseOrderIndex(ByVal PurchaseOrder_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM tb_PurchaseOrder"
            strSQL &= " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "'"

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
    ''' Get Purchase Order Item data
    ''' from tb_PurchaseOrderItem
    ''' passing PurchaseOrderItem_Index
    ''' </summary>
    ''' <param name="PurchaseOrderItem_Index"></param>
    ''' <remarks></remarks>
    Public Sub getPurchaseOrderItem(ByVal PurchaseOrderItem_Index As String)
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM tb_PurchaseOrderItem"
            strSQL &= " WHERE PurchaseOrderItem_Index ='" & PurchaseOrderItem_Index & "'"

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
    ''' Get fulll PO information
    ''' from VIEW_PurchaseOrderDetail
    ''' passing PurchaseOrder_Index
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Public Sub getPurchaseOrderDetail(ByVal PurchaseOrder_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_PurchaseOrderDetail"
            strSQL &= " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "

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
    ''' Get information of items not yet received.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Public Sub getPurchaseOrderRemain(ByVal PurchaseOrder_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_PurchaseOrderDetail"
            strSQL &= " WHERE (PurchaseOrder_Index ='" & PurchaseOrder_Index & "') "
            strSQL &= " AND (Qty > Received_Qty) "
            strSQL &= " ORDER BY PurchaseOrderItem_Index"

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


    Public Sub getPurchaseOrderRemain_V2(ByVal PurchaseOrder_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_PurchaseOrderDetail"
            strSQL &= " WHERE (PurchaseOrder_Index ='" & PurchaseOrder_Index & "') "
            strSQL &= " AND (Total_Qty > Total_Received_Qty) "
            strSQL &= " ORDER BY PurchaseOrderItem_Index ASC"

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


    ' Todd 26 Dec 2009
    ' Change sub name from "CheckReciveQty" to "getPurchaseOrderDetail_Using_POItemIndex"
    ' This sub is redundant. We do not use it anymore.
    'Public Sub CheckReciveQty(ByVal PurchaseOrderItem_Index As String)
    '    '  
    '    Dim strSQL As String = ""
    '    Dim strWhere As String = ""
    '    Try

    '        strSQL = " select * from VIEW_PurchaseOrderDetail"
    '        strWhere = " WHERE PurchaseOrderItem_Index ='" & PurchaseOrderItem_Index & "' "

    '        strSQL = strSQL & strWhere

    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub

    ''' <summary>
    ''' Get full PO information from VIEW_PurchaseOrderDetail
    ''' passing PurchaseOrderItem_Index (Item Index)
    ''' </summary>
    ''' <param name="PurchaseOrderItem_Index"></param>
    ''' <remarks></remarks>
    Public Sub getPurchaseOrderDetail_By_POItemIndex(ByVal PurchaseOrderItem_Index As String)

        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_PurchaseOrderDetail"
            strSQL &= " WHERE PurchaseOrderItem_Index ='" & PurchaseOrderItem_Index & "' "

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

    ' Todd 26 Dec 1009 - This sub is reduntdant.
    ' Replace with sub "getPurchaseOrderDetail"
    '
    'Public Sub ProductPlanOrderQty(ByVal PurchaseOrder_Index As String)
    '    '  
    '    Dim strSQL As String = ""
    '    Dim strWhere As String = ""
    '    Try

    '        strSQL = " select * from VIEW_PurchaseOrderDetail"
    '        strWhere = " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "

    '        strSQL = strSQL & strWhere

    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub

    ''' <summary>
    ''' Get PO Other information
    ''' passing PurchaseOrder_Index
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Public Sub getPurchaseOrderOther(ByVal PurchaseOrder_Index As String)
        Dim strSQL As String = ""

        Try

            strSQL = "SELECT * FROM tb_PurchaseOrderOther"
            strSQL &= " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "

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
    ''' Get PO Discount details
    ''' passing PurchaseOrder_Index
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd 26 Dec 1009 - Change sub name from "getDiscountDetail" to "getPurchaseOrder_DiscountDetail"
    ''' </remarks>
    Public Sub getPurchaseOrder_DiscountDetail(ByVal PurchaseOrder_Index As String)
        '  
        Dim strSQL As String = ""

        Try

            strSQL = "SELECT * FROM tb_PurchaseOrder_Discount"
            strSQL &= " WHERE  PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "

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
    ''' Get PO that already receipt 
    ''' through VIEW_PO_GetPOForOrder
    ''' passing PurchaseOrder_Index
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd 26 Dec 1009 - Change sub name from "ProductReciveOrder" to "getPurchaseOrder_AlreadyReceipt"
    ''' </remarks>
    Public Sub getPurchaseOrder_AlreadyReceipt(ByVal PurchaseOrder_Index As String)
        '  
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_PO_GetPOForOrder"
            strSQL &= " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "
            'strSQL &= " ORDER BY "

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
    ''' Check if PO Item exists.
    ''' </summary>
    ''' <param name="PurchaseOrderItem_Index"></param>
    ''' <returns>
    ''' True if exists. False otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Updated by: Todd 27 Dec 2009 - This function was renamed from "isExistID" to "IsExist_PurchaseOrderItem".
    ''' </remarks>
    Private Function IsExist_PurchaseOrderItem(ByVal PurchaseOrderItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_PurchaseOrderItem WHERE PurchaseOrderItem_Index = @PurchaseOrderItem_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = PurchaseOrderItem_Index

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
    ''' Check if PO Discount exists.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <returns>
    ''' True if exists. False otherwise.
    ''' </returns>
    ''' <remarks></remarks>
    Private Function IsExist_PurchaseOrderDiscount(ByVal PurchaseOrder_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_PurchaseOrder_Discount WHERE PurchaseOrder_Index = @PurchaseOrder_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Index

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
    ''' Check if PO Other exists.
    ''' </summary>
    ''' <param name="PurchaseOrderOther_Index"></param>
    ''' <returns>
    ''' True if exists. False otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Updated by: Todd 27 Dec 2009 - This function was renamed from "isExistID_PurchaseOrderOther" to "IsExist_PurchaseOrderOther".
    ''' </remarks>
    Private Function IsExist_PurchaseOrderOther(ByVal PurchaseOrderOther_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_PurchaseOrderOther WHERE PurchaseOrderOther_Index = @PurchaseOrderOther_Index  "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@PurchaseOrderOther_Index", SqlDbType.VarChar, 13).Value = PurchaseOrderOther_Index

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
    ''' Check if there is at least one item had been received for this PO.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <returns>
    ''' True - if at least one qty of any items in this PO had been received.
    ''' False - Otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Added by: Todd 3 Jan 2010 - This function is created for checking in closing PO process.
    '''           However, it can be used for other purposes later.
    ''' </remarks>
    Public Function IsExist_POItemAlreadyReceipt(ByVal PurchaseOrder_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "SELECT count(*) FROM tb_PurchaseOrder PO, tb_PurchaseOrderItem POItem "
            strSQL &= " WHERE (PO.PurchaseOrder_Index = POItem.PurchaseOrder_Index) "
            strSQL &= " AND (PO.Status = 2) "
            strSQL &= " AND (POItem.Received_Qty > 0) "
            strSQL &= " AND (PO.PurchaseOrder_Index = @PurchaseOrder_Index)"

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = PurchaseOrder_Index

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
        Finally
            disconnectDB()
        End Try
    End Function

#End Region

#Region " CANCEL PO "

    ''' <summary>
    ''' Cancel PO Transaction.
    ''' </summary>
    ''' <param name="oPurchaseOrder"></param>
    ''' <returns>
    ''' True if process succeeded.
    ''' False otherwise.
    ''' </returns>
    ''' <remarks>
    ''' Updated by: Todd 4 Jan 2010 - Fix bug in cancel_by, cancel_date, cancel_branch.
    ''' These fields need to be updated.
    ''' </remarks>
    Public Function Cancel_PurchaseOrder(ByVal oPurchaseOrder As tb_PurchaseOrder) As Boolean

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
            strSQL &= " cancel_date=getdate(), cancel_by='" & WV_UserName & "', "
            strSQL &= " cancel_branch='" & WV_Branch_ID & "' "
            strSQL &= " WHERE PurchaseOrder_Index ='" & oPurchaseOrder.PurchaseOrder_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 2: Update status in tb_PurchaseOrder = -1 (Cancelled)
            strSQL1 = "UPDATE tb_PurchaseOrder "
            strSQL1 &= " SET status =-1, "
            strSQL1 &= " cancel_date=getdate(), cancel_by='" & WV_UserName & "', "
            strSQL1 &= " cancel_branch='" & WV_Branch_ID & "' "
            strSQL1 &= " WHERE PurchaseOrder_Index ='" & oPurchaseOrder.PurchaseOrder_Index & "'"


            SetSQLString = strSQL1
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 3: Update status in tb_PurchaseOrderOther = -1 (Cancelled)
            strSQL1 = "UPDATE tb_PurchaseOrderOther "
            strSQL1 &= " SET status =-1, "
            strSQL1 &= " cancel_date=getdate(), cancel_by='" & WV_UserName & "', "
            strSQL1 &= " cancel_branch='" & WV_Branch_ID & "' "
            strSQL1 &= " WHERE PurchaseOrder_Index ='" & oPurchaseOrder.PurchaseOrder_Index & "'"

            SetSQLString = strSQL1
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            ' --- STEP 4: Add new log in Sy_Audit_Log
            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = oPurchaseOrder.PurchaseOrder_Index
            oAudit_Log.Document_No = oPurchaseOrder.PurchaseOrder_No
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_PO, Connection, myTrans)

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

#End Region

#Region " UNUSED & BACKUP FUNCTIONS(TEMPORARY) "
    ' ------ Todd 26 Dec 2009 - Think this sub is not used. 
    ' Should be bad code beacuse passing File_Name as parameter make no sense.
    ' str10 is not used.
    Public Sub Delete_PO(ByVal File_Name As String)

        Dim PurchaseOrder As String = ""
        Dim PurchaseOrderItem As String = ""
        Dim strSQL As String = ""


        Dim odtTransferStatusLocation As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction
        SQLServerCommand.Transaction = myTrans

        Try

            PurchaseOrder = "DELETE FROM tb_PurchaseOrder  "
            PurchaseOrder &= " WHERE str10 ='" & File_Name & "' "

            connectDB()
            SetSQLString = PurchaseOrder
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            PurchaseOrderItem = "DELETE FROM tb_PurchaseOrderItem  "
            PurchaseOrderItem &= " WHERE str10 ='" & File_Name & "' "

            connectDB()
            SetSQLString = PurchaseOrderItem
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

    '   Public Function Delete_PurchaseOrder(ByVal PurchaseOrder_Index As String) As Boolean
    Public Sub Delete_PurchaseOrder(ByVal PurchaseOrder_Index As String)
        Dim strSQL As String = ""
        Try
            strSQL = "DELETE FROM tb_PurchaseOrderItem "
            strSQL &= " WHERE PurchaseOrder_Index ='" & PurchaseOrder_Index & "' "

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


    'Public Sub Delete_Purchase(ByVal PurchaseOrderItem_Index As String)
    '    Dim strSQL As String = ""
    '    Try
    '        strSQL = "DELETE FROM tb_PurchaseOrderItem "
    '        strSQL &= " WHERE PurchaseOrderItem_Index ='" & PurchaseOrderItem_Index & "' "

    '        connectDB()
    '        SetSQLString = strSQL
    '        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
    '        EXEC_Command()


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    '
    'End Sub
#End Region

End Class
