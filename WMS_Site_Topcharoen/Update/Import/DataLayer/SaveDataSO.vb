Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data.SqlClient
Public Class SaveDataSO : Inherits DBType_SQLServer

#Region "Property"
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _DtTemp As New DataTable
    Private _FileName As String
    Private _GUid As String
    Private _Status As Integer
    Private _PurchaseOrder_Index As String
    Private _PurchaseOrderItem_Index As String = Nothing

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

    Public Property DtTemp() As DataTable
        Get
            Return _DtTemp
        End Get
        Set(ByVal value As DataTable)
            _DtTemp = value
        End Set
    End Property

    Public Property FileName() As String
        Get
            Return _FileName
        End Get
        Set(ByVal value As String)
            _FileName = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

    Public Property GUid() As String
        Get
            Return _GUid
        End Get
        Set(ByVal value As String)
            _GUid = value
        End Set
    End Property

    Property PurchaseOrder_Index() As String
        Get
            Return _PurchaseOrder_Index
        End Get
        Set(ByVal value As String)
            _PurchaseOrder_Index = value
        End Set
    End Property

#End Region

    Public Function Insert_PO(ByVal _dtPOHeader As DataTable, ByVal _dtPODetail As DataTable, Optional ByVal Status As Integer = 1) As Boolean
        Dim strSQL As String = " "
        Dim objDBIndex As New Sy_AutoNumber
        Dim DtDetail_New As New DataTable
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            For i As Integer = 0 To _dtPOHeader.Rows.Count - 1
                Me._PurchaseOrder_Index = objDBIndex.getSys_Value("PurchaseOrder_Index")
                strSQL = " INSERT INTO tb_PurchaseOrder(PurchaseOrder_Index,PurchaseOrder_No,PurchaseOrder_Date,Carrier_Index,Customer_Receive_Location_Index,Expected_Delivery_Date,Delivery_Date,Customer_Index,Supplier_Index,Department_Index,DocumentType_Index,Remark,Credit_Term,Currency_Index,Exchange_Rate,PaymentMethod_Index,Payment_Ref,FullPaid_Date,Amount,Discount_Percent,Discount_Amt,Deposit_Amt,Total_Amt,VAT_Percent,VAT,Net_Amt,Supplier_Address,Supplier_Tel,Supplier_Fax,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,Status)" & _
                           "       VALUES(@PurchaseOrder_Index,@PurchaseOrder_No,@PurchaseOrder_Date,@Carrier_Index,@Customer_Receive_Location_Index,@Expected_Delivery_Date,@Delivery_Date,@Customer_Index,@Supplier_Index,@Department_Index,@DocumentType_Index,@Remark,@Credit_Term,@Currency_Index,@Exchange_Rate,@PaymentMethod_Index,@Payment_Ref,@FullPaid_Date,@Amount,@Discount_Percent,@Discount_Amt,@Deposit_Amt,@Total_Amt,@VAT_Percent,@VAT,@Net_Amt,@Supplier_Address,@Supplier_Tel,@Supplier_Fax,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_date,@add_branch,@Status)"
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Index
                    .Parameters.Add("@PurchaseOrder_No", SqlDbType.VarChar, 50).Value = _dtPOHeader.Rows(i).Item("PurchaseOrder_No").ToString
                    .Parameters.Add("@PurchaseOrder_Date", SqlDbType.DateTime).Value = _dtPOHeader.Rows(i).Item("PurchaseOrder_Date")
                    .Parameters.Add("@Carrier_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("Carrier_Index").ToString
                    .Parameters.Add("@Customer_Receive_Location_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("Customer_Receive_Location_Index").ToString
                    .Parameters.Add("@Expected_Delivery_Date", SqlDbType.DateTime).Value = _dtPOHeader.Rows(i).Item("Expected_Delivery_Date")
                    .Parameters.Add("@Delivery_Date", SqlDbType.DateTime).Value = _dtPOHeader.Rows(i).Item("Delivery_Date")
                    .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("Customer_Index").ToString
                    .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("Supplier_Index").ToString
                    .Parameters.Add("@Department_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("Department_Index").ToString
                    .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("DocumentType_Index").ToString
                    .Parameters.Add("@Remark", SqlDbType.NVarChar, 255).Value = _dtPOHeader.Rows(i).Item("Remark").ToString
                    .Parameters.Add("@Credit_Term", SqlDbType.NVarChar, 10).Value = _dtPOHeader.Rows(i).Item("Credit_Term").ToString
                    .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = _dtPOHeader.Rows(i).Item("Currency_Index").ToString
                    .Parameters.Add("@Exchange_Rate", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("Exchange_Rate")
                    .Parameters.Add("@PaymentMethod_Index", SqlDbType.NVarChar, 13).Value = _dtPOHeader.Rows(i).Item("PaymentMethod_Index").ToString
                    .Parameters.Add("@Payment_Ref", SqlDbType.NVarChar, 50).Value = _dtPOHeader.Rows(i).Item("Payment_Ref").ToString
                    .Parameters.Add("@FullPaid_Date", SqlDbType.DateTime).Value = _dtPOHeader.Rows(i).Item("FullPaid_Date")
                    .Parameters.Add("@Amount", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("Amount")
                    .Parameters.Add("@Discount_Percent", SqlDbType.Int).Value = _dtPOHeader.Rows(i).Item("Discount_Percent")
                    .Parameters.Add("@Discount_Amt", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("Discount_Amt")
                    .Parameters.Add("@Deposit_Amt", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("Deposit_Amt")
                    .Parameters.Add("@Total_Amt", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("Total_Amt")
                    .Parameters.Add("@VAT_Percent", SqlDbType.Int).Value = _dtPOHeader.Rows(i).Item("VAT_Percent")
                    .Parameters.Add("@VAT", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("VAT")
                    .Parameters.Add("@Net_Amt", SqlDbType.Float).Value = _dtPOHeader.Rows(i).Item("Net_Amt")
                    .Parameters.Add("@Supplier_Address", SqlDbType.NVarChar, 1000).Value = _dtPOHeader.Rows(i).Item("Supplier_Address").ToString
                    .Parameters.Add("@Supplier_Tel", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Supplier_Tel").ToString
                    .Parameters.Add("@Supplier_Fax", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Supplier_Fax").ToString
                    .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str1").ToString
                    .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str2").ToString
                    .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str3").ToString
                    .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str4").ToString
                    .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str5").ToString
                    .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str6").ToString
                    .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str7").ToString
                    .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = _dtPOHeader.Rows(i).Item("Str8").ToString
                    .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = _dtPOHeader.Rows(i).Item("Str9").ToString
                    .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = _dtPOHeader.Rows(i).Item("Str10").ToString
                    .Parameters.Add("@Flo1", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Flo2", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Flo3", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Flo4", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Flo5", SqlDbType.Float).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_date", SqlDbType.SmallDateTime).Value = Now
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    '.Parameters.Add("@update_by", SqlDbType.VarChar, 50).Value = ""
                    '.Parameters.Add("@update_date", SqlDbType.SmallDateTime).Value = ""
                    ' .Parameters.Add("@update_branch", SqlDbType.Int).Value = ""
                    '.Parameters.Add("@cancel_by", SqlDbType.VarChar, 50).Value = ""
                    '.Parameters.Add("@cancel_date", SqlDbType.SmallDateTime).Value = ""
                    '.Parameters.Add("@cancel_branch", SqlDbType.Int).Value = ""
                    .Parameters.Add("@" & "Status", SqlDbType.Int).Value = Status
                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                EXEC_Command()

                Dim Dt_DetailInsert As New DataTable
                Dim drArr() As DataRow = _dtPODetail.Select("PurchaseOrder_No = '" & _dtPOHeader.Rows(i).Item("PurchaseOrder_No").ToString & "'")
                If drArr.Length > 0 Then
                    For j As Integer = 0 To drArr.Length - 1
                        If _PurchaseOrderItem_Index Is Nothing Then
                            Me._PurchaseOrderItem_Index = objDBIndex.getSys_Value("PurchaseOrderItem_Index")
                        End If

                        strSQL = " INSERT INTO tb_PurchaseOrderItem(PurchaseOrderItem_Index,Item_Seq,PurchaseOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Received_Qty,Received_Qty,Received_Weight,Received_Volume" ',Last_Received_Date
                        strSQL &= " ,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_branch,Percent_Over_Allow,Percent_Under_Allow)" & _
                           " VALUES(@PurchaseOrderItem_Index,@Item_Seq,@PurchaseOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Received_Qty,@Received_Qty,@Received_Weight,@Received_Volume" ',@Last_Received_Date
                        strSQL &= ",@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Remark,@Reason,@Ref_No1,@Ref_No2,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,@add_branch,@Percent_Over_Allow,@Percent_Under_Allow)"

                        With SQLServerCommand

                            .Parameters.Clear()
                            .Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrderItem_Index
                            .Parameters.Add("@Item_Seq", SqlDbType.Int, 4).Value = 0
                            .Parameters.Add("@PurchaseOrder_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrder_Index 'drArr(j)("PurchaseOrder_Index").ToString
                            .Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = drArr(j)("Sku_Index").ToString
                            .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = drArr(j)("Package_Index").ToString
                            .Parameters.Add("@Ratio", SqlDbType.Float, 8).Value = drArr(j)("Ratio")
                            .Parameters.Add("@Total_Qty", SqlDbType.Float, 8).Value = drArr(j)("Total_Qty")
                            .Parameters.Add("@Qty", SqlDbType.Float, 8).Value = drArr(j)("Qty")
                            .Parameters.Add("@Weight", SqlDbType.Float, 8).Value = drArr(j)("Weight").ToString
                            .Parameters.Add("@Volume", SqlDbType.Float, 8).Value = drArr(j)("Volume").ToString
                            .Parameters.Add("@Serial_No", SqlDbType.NVarChar, 100).Value = ""
                            .Parameters.Add("@Total_Received_Qty", SqlDbType.Float, 8).Value = drArr(j)("Total_Received_Qty")
                            .Parameters.Add("@Received_Qty", SqlDbType.Float, 8).Value = drArr(j)("Received_Qty")
                            .Parameters.Add("@Received_Weight", SqlDbType.Float, 8).Value = drArr(j)("Received_Weight")
                            .Parameters.Add("@Received_Volume", SqlDbType.Float, 8).Value = drArr(j)("Received_Volume")
                            ' .Parameters.Add("@Last_Received_Date", SqlDbType.DateTime, 23).Value = drArr("Last_Received_Date
                            .Parameters.Add("@UnitPrice", SqlDbType.Float, 8).Value = drArr(j)("UnitPrice")
                            .Parameters.Add("@Amount", SqlDbType.Float, 8).Value = drArr(j)("Amount")

                            .Parameters.Add("@Currency_Index", SqlDbType.VarChar, 13).Value = ""
                            .Parameters.Add("@Discount_Amt", SqlDbType.Float, 8).Value = drArr(j)("Discount_Amt").ToString
                            .Parameters.Add("@Total_Amt", SqlDbType.Float, 8).Value = drArr(j)("Total_Amt").ToString
                            .Parameters.Add("@Status", SqlDbType.Int, 4).Value = Status
                            .Parameters.Add("@Remark", SqlDbType.NVarChar, 1000).Value = drArr(j)("Remark").ToString
                            .Parameters.Add("@Reason", SqlDbType.NVarChar, 1000).Value = drArr(j)("Reason").ToString
                            .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = drArr(j)("Ref_No1").ToString
                            .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = drArr(j)("Ref_No2").ToString
                            .Parameters.Add("@Str1", SqlDbType.NVarChar, 100).Value = drArr(j)("Str1").ToString
                            .Parameters.Add("@Str2", SqlDbType.NVarChar, 100).Value = drArr(j)("Str2").ToString
                            .Parameters.Add("@Str3", SqlDbType.NVarChar, 100).Value = drArr(j)("Str3").ToString
                            .Parameters.Add("@Str4", SqlDbType.NVarChar, 100).Value = drArr(j)("Str4").ToString
                            .Parameters.Add("@Str5", SqlDbType.NVarChar, 100).Value = drArr(j)("Str5").ToString
                            .Parameters.Add("@Str6", SqlDbType.NVarChar, 100).Value = drArr(j)("Str6").ToString
                            .Parameters.Add("@Str7", SqlDbType.NVarChar, 100).Value = drArr(j)("Str7").ToString
                            .Parameters.Add("@Str8", SqlDbType.NVarChar, 100).Value = drArr(j)("Str8").ToString
                            .Parameters.Add("@Str9", SqlDbType.NVarChar, 2000).Value = drArr(j)("Str9").ToString
                            .Parameters.Add("@Str10", SqlDbType.NVarChar, 2000).Value = drArr(j)("Str10").ToString
                            .Parameters.Add("@Flo1", SqlDbType.Float, 8).Value = drArr(j)("Flo1")
                            .Parameters.Add("@Flo2", SqlDbType.Float, 8).Value = drArr(j)("Flo2")
                            .Parameters.Add("@Flo3", SqlDbType.Float, 8).Value = drArr(j)("Flo3")
                            .Parameters.Add("@Flo4", SqlDbType.Float, 8).Value = drArr(j)("Flo4")
                            .Parameters.Add("@Flo5", SqlDbType.Float, 8).Value = drArr(j)("Flo5")
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID
                            '''''add Percent
                            .Parameters.Add("@Percent_Over_Allow", SqlDbType.Float, 8).Value = drArr(j)("Percent_Over_Allow")
                            .Parameters.Add("@Percent_Under_Allow", SqlDbType.Float, 8).Value = drArr(j)("Percent_Under_Allow")
                            '''''
                        End With

                        SetSQLString = strSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                        _PurchaseOrderItem_Index = Nothing
                    Next
                    '--- Detail
                End If

            Next '--- Hearder

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            objDBIndex = Nothing
            disconnectDB()
        End Try
    End Function

    Public Function getStructure_PO() As DataSet
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Dim oDs As New DataSet
        Dim _dttmpPOHeader As New DataTable
        Dim _dttmpPODetail As New DataTable
        Dim _dtPOHeader As New DataTable
        Dim _dtPODetail As New DataTable
        Try


            strSQL = " SELECT top 1 PurchaseOrder_Index,PurchaseOrder_No,PurchaseOrder_Date,Carrier_Index,Customer_Receive_Location_Index,Expected_Delivery_Date,Delivery_Date,Customer_Index,Supplier_Index,Department_Index,DocumentType_Index,Remark,Credit_Term,Currency_Index,Exchange_Rate,PaymentMethod_Index,Payment_Ref,FullPaid_Date,Amount,Discount_Percent,Discount_Amt,Deposit_Amt,Total_Amt,VAT_Percent,VAT,Net_Amt,Supplier_Address,Supplier_Tel,Supplier_Fax,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,Status,StatusReceive"

            strSQL &= " FROM  tb_PurchaseOrder "

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dttmpPOHeader = GetDataTable
            _dtPOHeader = _dttmpPOHeader.Clone
            _dtPOHeader.TableName = "PO_Header"

            strSQL = " select top 1 PurchaseOrderItem_Index,Item_Seq,PurchaseOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Received_Qty,Received_Qty,Received_Weight,Received_Volume,Last_Received_Date,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,Charge_Status,Percent_Over_Allow,Percent_Under_Allow,'' as PurchaseOrder_No "
            strSQL &= " FROM  tb_PurchaseOrderItem "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dttmpPODetail = GetDataTable
            _dtPODetail = _dttmpPODetail.Clone
            _dtPODetail.TableName = "PO_Detail"

            oDs.Tables.Add(_dtPOHeader)
            oDs.Tables.Add(_dtPODetail)

            Return oDs
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function InsertTemp() As Boolean
        Dim strSQL As String = " "
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim CountDtTemp = _DtTemp.Columns.Count
            For iRows As Integer = 0 To _DtTemp.Rows.Count - 1
                strSQL = " INSERT INTO _Prepare_Imports"
                strSQL &= "  ("

                'Add Field
                For iCol As Integer = 0 To CountDtTemp - 1
                    If iCol = 0 Then
                        strSQL &= "[" & iCol & "]"
                    Else
                        strSQL &= ",[" & iCol & "]"
                    End If
                Next
                strSQL &= " ,Status"
                strSQL &= " ,FileName"
                strSQL &= " ,GUid"
                strSQL &= "  ) VALUES ( "

                'Add VALUES
                For iData As Integer = 0 To CountDtTemp - 1
                    If iData = 0 Then
                        If _DtTemp.Rows(iRows).Item(iData).ToString = "" Then
                            strSQL &= "''"
                        Else
                            strSQL &= "'" & _DtTemp.Rows(iRows).Item(iData).ToString & "'"
                        End If
                    Else
                        If _DtTemp.Rows(iRows).Item(iData).ToString = "" Then
                            strSQL &= ",''"
                        Else
                            strSQL &= " ,'" & _DtTemp.Rows(iRows).Item(iData).ToString & "'"
                        End If
                    End If
                Next
                strSQL &= ",1"
                strSQL &= ",'" & _FileName & "'"
                strSQL &= ",'" & _GUid & "'"
                strSQL &= "  ) "

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
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

    Public Function UpdateStatus() As Boolean
        Dim strSQL As String = " "
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = " Update _Prepare_Imports Set"
            strSQL &= " Status= @Status"
            strSQL &= " Where Status = 1"
            strSQL &= " AND   FileName = @FileName"
            strSQL &= " AND GUid = @GUid "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = Me.Status
                .Parameters.Add("@FileName", SqlDbType.NVarChar, 4000).Value = Me.FileName
                .Parameters.Add("@GUid", SqlDbType.NVarChar, 4000).Value = Me.GUid
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
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
    Public Function GetTmpDataUpdateStatus(ByVal strGUID As String, ByVal strFileName As String) As DataTable
        Dim strSQL As String = " "
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            strSQL = " Update _Prepare_Imports Set"
            strSQL &= " Status= @Status"
            strSQL &= " Where Status = 1"
            strSQL &= " AND   FileName = @FileName"
            strSQL &= " AND GUid = @GUid "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Status", SqlDbType.Int, 4).Value = 2
                .Parameters.Add("@FileName", SqlDbType.NVarChar, 4000).Value = strFileName
                .Parameters.Add("@GUid", SqlDbType.NVarChar, 4000).Value = strGUID
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            '*** Commit transaction 




            myTrans.Commit()

            strSQL = "Select * from  _Prepare_Imports  "

            strSQL &= " Where Status = 2"
            strSQL &= " AND   FileName = '" & strFileName & "'"
            strSQL &= " AND GUid =  '" & strGUID & "'"


            SetSQLString = strSQL
            'connectDB()
            EXEC_DataAdapter()

            Return GetDataTable
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function GetRatioPackageSKU(ByVal strSku_Index As String, ByVal strPackage_Index As String) As DataTable
        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT        dbo.ms_SKURatio.Sku_Index, dbo.ms_SKURatio.Package_Index, dbo.ms_SKURatio.Ratio, dbo.ms_Package.Package_Id, dbo.ms_SKU.Sku_Id, "
            strSQL &= "                         dbo.ms_Package.Description AS Package_Name, dbo.ms_SKURatio.SkuRatio_Index"
            strSQL &= " FROM            dbo.ms_SKURatio INNER JOIN"
            strSQL &= "                         dbo.ms_SKU ON dbo.ms_SKURatio.Sku_Index = dbo.ms_SKU.Sku_Index INNER JOIN"
            strSQL &= "                         dbo.ms_Package ON dbo.ms_SKURatio.Package_Index = dbo.ms_Package.Package_Index"
            strSQL &= " WHERE        (dbo.ms_SKU.status_id <> - 1) "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Return GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Private Function set_DtDetail_New(ByVal _odt As DataTable, ByVal PurchaseOrder_No As String) As DataTable
        Try
            Dim strwhere As String = ""
            Dim sPurchaseOrder_Index As String = ""
            strwhere = "PurchaseOrder_No = '" & PurchaseOrder_No & "'"
            Dim DtDetail_New As New DataTable
            Dim drArr() As DataRow = _odt.Select("PurchaseOrder_No = '" & PurchaseOrder_No & "'")
            If drArr.Length > 0 Then
                For i As Integer = 0 To drArr.Length - 1
                    _odt.Rows(i)("PurchaseOrder_Index") = _PurchaseOrder_Index
                Next
                ' sPurchaseOrder_Index = drArr(0).Item("PurchaseOrder_Index").ToString
            End If
            Return _odt
        Catch ex As Exception
            Throw ex
        End Try
    End Function


End Class
