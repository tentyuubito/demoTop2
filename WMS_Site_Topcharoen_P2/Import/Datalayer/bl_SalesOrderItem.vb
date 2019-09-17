Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Public Class bl_SalesOrderItem : Inherits DBType_SQLServer
#Region " Private variables "
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _SalesOrderItem_Index As String = ""
    Private _Item_Seq As Integer = 0
    Private _SalesOrder_Index As String = ""
    Private _Sku_Index As String = ""
    Private _Package_Index As String = ""
    Private _Ratio As Double
    Private _Total_Qty As Double
    Private _Qty As Double
    Private _Weight As Double
    Private _Volume As Double
    Private _Serial_No As String = ""
    Private _Total_Qty_Withdraw As Double
    Private _Qty_Withdraw As Double
    Private _Weight_Withdraw As Double
    Private _Volume_Withdraw As Double
    Private _Last_Withdraw_Date As Date = Now
    Private _UnitPrice As Double
    Private _Amount As Double
    Private _Currency_Index As String = ""
    Private _Discount_Amt As Double
    Private _Total_Amt As Double
    Private _Status As Integer
    Private _Remark As String = ""
    Private _Reason As String = ""
    Private _Ref_No1 As String = ""
    Private _Ref_No2 As String = ""
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Str6 As String = ""
    Private _Str7 As String = ""
    Private _Str8 As String = ""
    Private _Str9 As String = ""
    Private _Str10 As String = ""
    Private _Flo1 As Double
    Private _Flo2 As Double
    Private _Flo3 As Double
    Private _Flo4 As Double
    Private _Flo5 As Double
    Private _add_by As String = ""
    Private _add_date As Date = Now
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date = Now
    Private _update_branch As Integer
    Private _cancel_by As String = ""
    Private _cancel_date As Date = Now
    Private _cancel_branch As Integer
    Private _Charge_Status As Integer
    Private _Itemstatus_index As String = ""
    Private _Plot As String = ""
#End Region

#Region " Properties "
    Public Property Plot() As String
        Get
            Return _Plot
        End Get
        Set(ByVal value As String)
            _Plot = value
        End Set
    End Property
    Public Property Itemstatus_index() As String
        Get
            Return _Itemstatus_index
        End Get
        Set(ByVal value As String)
            _Itemstatus_index = value
        End Set
    End Property

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
    Public Property SalesOrderItem_Index() As String
        Get
            Return _SalesOrderItem_Index
        End Get
        Set(ByVal Value As String)
            _SalesOrderItem_Index = Value
        End Set
    End Property

    Public Property Item_Seq() As Integer
        Get
            Return _Item_Seq
        End Get
        Set(ByVal Value As Integer)
            _Item_Seq = Value
        End Set
    End Property

    Public Property SalesOrder_Index() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal Value As String)
            _SalesOrder_Index = Value
        End Set
    End Property

    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal Value As String)
            _Sku_Index = Value
        End Set
    End Property

    Public Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
        Set(ByVal Value As String)
            _Package_Index = Value
        End Set
    End Property

    Public Property Ratio() As Double
        Get
            Return _Ratio
        End Get
        Set(ByVal Value As Double)
            _Ratio = Value
        End Set
    End Property

    Public Property Total_Qty() As Double
        Get
            Return _Total_Qty
        End Get
        Set(ByVal Value As Double)
            _Total_Qty = Value
        End Set
    End Property

    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal Value As Double)
            _Qty = Value
        End Set
    End Property

    Public Property Weight() As Double
        Get
            Return _Weight
        End Get
        Set(ByVal Value As Double)
            _Weight = Value
        End Set
    End Property

    Public Property Volume() As Double
        Get
            Return _Volume
        End Get
        Set(ByVal Value As Double)
            _Volume = Value
        End Set
    End Property

    Public Property Serial_No() As String
        Get
            Return _Serial_No
        End Get
        Set(ByVal Value As String)
            _Serial_No = Value
        End Set
    End Property

    Public Property Total_Qty_Withdraw() As Double
        Get
            Return _Total_Qty_Withdraw
        End Get
        Set(ByVal Value As Double)
            _Total_Qty_Withdraw = Value
        End Set
    End Property

    Public Property Qty_Withdraw() As Double
        Get
            Return _Qty_Withdraw
        End Get
        Set(ByVal Value As Double)
            _Qty_Withdraw = Value
        End Set
    End Property

    Public Property Weight_Withdraw() As Double
        Get
            Return _Weight_Withdraw
        End Get
        Set(ByVal Value As Double)
            _Weight_Withdraw = Value
        End Set
    End Property

    Public Property Volume_Withdraw() As Double
        Get
            Return _Volume_Withdraw
        End Get
        Set(ByVal Value As Double)
            _Volume_Withdraw = Value
        End Set
    End Property

    Public Property Last_Withdraw_Date() As Date
        Get
            Return _Last_Withdraw_Date
        End Get
        Set(ByVal Value As Date)
            _Last_Withdraw_Date = Value
        End Set
    End Property

    Public Property UnitPrice() As Double
        Get
            Return _UnitPrice
        End Get
        Set(ByVal Value As Double)
            _UnitPrice = Value
        End Set
    End Property

    Public Property Amount() As Double
        Get
            Return _Amount
        End Get
        Set(ByVal Value As Double)
            _Amount = Value
        End Set
    End Property

    Public Property Currency_Index() As String
        Get
            Return _Currency_Index
        End Get
        Set(ByVal Value As String)
            _Currency_Index = Value
        End Set
    End Property

    Public Property Discount_Amt() As Double
        Get
            Return _Discount_Amt
        End Get
        Set(ByVal Value As Double)
            _Discount_Amt = Value
        End Set
    End Property

    Public Property Total_Amt() As Double
        Get
            Return _Total_Amt
        End Get
        Set(ByVal Value As Double)
            _Total_Amt = Value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal Value As Integer)
            _Status = Value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal Value As String)
            _Remark = Value
        End Set
    End Property

    Public Property Reason() As String
        Get
            Return _Reason
        End Get
        Set(ByVal Value As String)
            _Reason = Value
        End Set
    End Property

    Public Property Ref_No1() As String
        Get
            Return _Ref_No1
        End Get
        Set(ByVal Value As String)
            _Ref_No1 = Value
        End Set
    End Property

    Public Property Ref_No2() As String
        Get
            Return _Ref_No2
        End Get
        Set(ByVal Value As String)
            _Ref_No2 = Value
        End Set
    End Property

    Public Property Str1() As String
        Get
            Return _Str1
        End Get
        Set(ByVal Value As String)
            _Str1 = Value
        End Set
    End Property

    Public Property Str2() As String
        Get
            Return _Str2
        End Get
        Set(ByVal Value As String)
            _Str2 = Value
        End Set
    End Property

    Public Property Str3() As String
        Get
            Return _Str3
        End Get
        Set(ByVal Value As String)
            _Str3 = Value
        End Set
    End Property

    Public Property Str4() As String
        Get
            Return _Str4
        End Get
        Set(ByVal Value As String)
            _Str4 = Value
        End Set
    End Property

    Public Property Str5() As String
        Get
            Return _Str5
        End Get
        Set(ByVal Value As String)
            _Str5 = Value
        End Set
    End Property

    Public Property Str6() As String
        Get
            Return _Str6
        End Get
        Set(ByVal Value As String)
            _Str6 = Value
        End Set
    End Property

    Public Property Str7() As String
        Get
            Return _Str7
        End Get
        Set(ByVal Value As String)
            _Str7 = Value
        End Set
    End Property

    Public Property Str8() As String
        Get
            Return _Str8
        End Get
        Set(ByVal Value As String)
            _Str8 = Value
        End Set
    End Property

    Public Property Str9() As String
        Get
            Return _Str9
        End Get
        Set(ByVal Value As String)
            _Str9 = Value
        End Set
    End Property

    Public Property Str10() As String
        Get
            Return _Str10
        End Get
        Set(ByVal Value As String)
            _Str10 = Value
        End Set
    End Property

    Public Property Flo1() As Double
        Get
            Return _Flo1
        End Get
        Set(ByVal Value As Double)
            _Flo1 = Value
        End Set
    End Property

    Public Property Flo2() As Double
        Get
            Return _Flo2
        End Get
        Set(ByVal Value As Double)
            _Flo2 = Value
        End Set
    End Property

    Public Property Flo3() As Double
        Get
            Return _Flo3
        End Get
        Set(ByVal Value As Double)
            _Flo3 = Value
        End Set
    End Property

    Public Property Flo4() As Double
        Get
            Return _Flo4
        End Get
        Set(ByVal Value As Double)
            _Flo4 = Value
        End Set
    End Property

    Public Property Flo5() As Double
        Get
            Return _Flo5
        End Get
        Set(ByVal Value As Double)
            _Flo5 = Value
        End Set
    End Property

    Public Property add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property

    Public Property add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property

    Public Property add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property

    Public Property update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property

    Public Property update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property

    Public Property update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

    Public Property cancel_by() As String
        Get
            Return _cancel_by
        End Get
        Set(ByVal Value As String)
            _cancel_by = Value
        End Set
    End Property

    Public Property cancel_date() As Date
        Get
            Return _cancel_date
        End Get
        Set(ByVal Value As Date)
            _cancel_date = Value
        End Set
    End Property

    Public Property cancel_branch() As Integer
        Get
            Return _cancel_branch
        End Get
        Set(ByVal Value As Integer)
            _cancel_branch = Value
        End Set
    End Property

    Public Property Charge_Status() As Integer
        Get
            Return _Charge_Status
        End Get
        Set(ByVal Value As Integer)
            _Charge_Status = Value
        End Set
    End Property


#End Region
#Region " INSERT DATA "
    Public Sub Insert()
        Dim strSQL As String = " "
        Try
            strSQL = " INSERT INTO tb_SalesOrderItem(SalesOrderItem_Index,Item_Seq,SalesOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Qty_Withdraw,Qty_Withdraw,Weight_Withdraw,Volume_Withdraw,Last_Withdraw_Date,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,Charge_Status)" & _
            "       VALUES(@SalesOrderItem_Index,@Item_Seq,@SalesOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Qty_Withdraw,@Qty_Withdraw,@Weight_Withdraw,@Volume_Withdraw,@Last_Withdraw_Date,@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Remark,@Reason,@Ref_No1,@Ref_No2,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,getdate(),@add_branch,@Charge_Status)"

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar).Value = _SalesOrderItem_Index
                .Parameters.Add("@Item_Seq", SqlDbType.Int).Value = _Item_Seq
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = _SalesOrder_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = _Sku_Index
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = _Package_Index
                .Parameters.Add("@Ratio", SqlDbType.Float).Value = _Ratio
                .Parameters.Add("@Total_Qty", SqlDbType.Float).Value = _Total_Qty
                .Parameters.Add("@Qty", SqlDbType.Float).Value = _Qty
                .Parameters.Add("@Weight", SqlDbType.Float).Value = _Weight
                .Parameters.Add("@Volume", SqlDbType.Float).Value = _Volume
                .Parameters.Add("@Serial_No", SqlDbType.NVarChar).Value = _Serial_No
                .Parameters.Add("@Total_Qty_Withdraw", SqlDbType.Float).Value = _Total_Qty_Withdraw
                .Parameters.Add("@Qty_Withdraw", SqlDbType.Float).Value = _Qty_Withdraw
                .Parameters.Add("@Weight_Withdraw", SqlDbType.Float).Value = _Weight_Withdraw
                .Parameters.Add("@Volume_Withdraw", SqlDbType.Float).Value = _Volume_Withdraw
                .Parameters.Add("@Last_Withdraw_Date", SqlDbType.DateTime).Value = _Last_Withdraw_Date
                .Parameters.Add("@UnitPrice", SqlDbType.Float).Value = _UnitPrice
                .Parameters.Add("@Amount", SqlDbType.Float).Value = _Amount
                .Parameters.Add("@Currency_Index", SqlDbType.VarChar).Value = _Currency_Index
                .Parameters.Add("@Discount_Amt", SqlDbType.Float).Value = _Discount_Amt
                .Parameters.Add("@Total_Amt", SqlDbType.Float).Value = _Total_Amt
                .Parameters.Add("@Status", SqlDbType.Int).Value = _Status
                .Parameters.Add("@Remark", SqlDbType.NVarChar).Value = _Remark
                .Parameters.Add("@Reason", SqlDbType.NVarChar).Value = _Reason
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar).Value = _Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar).Value = _Ref_No2
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = _Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = _Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar).Value = _Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar).Value = _Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar).Value = _Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar).Value = _Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar).Value = _Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar).Value = _Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar).Value = _Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = _Str10
                .Parameters.Add("@Flo1", SqlDbType.Float).Value = _Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float).Value = _Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float).Value = _Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float).Value = _Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float).Value = _Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                '.Parameters.Add("@update_by", SqlDbType.VarChar).Value = _update_by
                '.Parameters.Add("@update_date", SqlDbType.SmallDateTime).Value = _update_date
                '.Parameters.Add("@update_branch", SqlDbType.Int).Value = _update_branch
                '.Parameters.Add("@cancel_by", SqlDbType.VarChar).Value = _cancel_by
                '.Parameters.Add("@cancel_date", SqlDbType.SmallDateTime).Value = _cancel_date
                '.Parameters.Add("@cancel_branch", SqlDbType.Int).Value = _cancel_branch
                .Parameters.Add("@Charge_Status", SqlDbType.Int).Value = _Charge_Status
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            _dataTable = Nothing
            disconnectDB()
        End Try
    End Sub

    Public Sub Insert_Import()
        Dim strSQL As String = " "

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            strSQL = " INSERT INTO tb_SalesOrderItem(SalesOrderItem_Index,Item_Seq,SalesOrder_Index,Sku_Index,Package_Index,Ratio,Total_Qty,Qty,Weight,Volume,Serial_No,Total_Qty_Withdraw,Qty_Withdraw,Weight_Withdraw,Volume_Withdraw,Last_Withdraw_Date,UnitPrice,Amount,Currency_Index,Discount_Amt,Total_Amt,Status,Remark,Reason,Ref_No1,Ref_No2,Str1,Str2,Str3,Str4,Str5,Str6,Str7,Str8,Str9,Str10,Flo1,Flo2,Flo3,Flo4,Flo5,add_by,add_date,add_branch,Charge_Status,ItemStatus_Index,Plot)" & _
            "       VALUES(@SalesOrderItem_Index,@Item_Seq,@SalesOrder_Index,@Sku_Index,@Package_Index,@Ratio,@Total_Qty,@Qty,@Weight,@Volume,@Serial_No,@Total_Qty_Withdraw,@Qty_Withdraw,@Weight_Withdraw,@Volume_Withdraw,@Last_Withdraw_Date,@UnitPrice,@Amount,@Currency_Index,@Discount_Amt,@Total_Amt,@Status,@Remark,@Reason,@Ref_No1,@Ref_No2,@Str1,@Str2,@Str3,@Str4,@Str5,@Str6,@Str7,@Str8,@Str9,@Str10,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5,@add_by,getdate(),@add_branch,@Charge_Status,@ItemStatus_Index,@Plot)"

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SalesOrderItem_Index", SqlDbType.VarChar).Value = _SalesOrderItem_Index
                .Parameters.Add("@Item_Seq", SqlDbType.Int).Value = _Item_Seq
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = _SalesOrder_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = _Sku_Index
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = _Package_Index
                .Parameters.Add("@Ratio", SqlDbType.Float).Value = _Ratio
                .Parameters.Add("@Total_Qty", SqlDbType.Float).Value = _Total_Qty
                .Parameters.Add("@Qty", SqlDbType.Float).Value = _Qty
                .Parameters.Add("@Weight", SqlDbType.Float).Value = _Weight
                .Parameters.Add("@Volume", SqlDbType.Float).Value = _Volume
                .Parameters.Add("@Serial_No", SqlDbType.NVarChar).Value = _Serial_No
                .Parameters.Add("@Total_Qty_Withdraw", SqlDbType.Float).Value = _Total_Qty_Withdraw
                .Parameters.Add("@Qty_Withdraw", SqlDbType.Float).Value = _Qty_Withdraw
                .Parameters.Add("@Weight_Withdraw", SqlDbType.Float).Value = _Weight_Withdraw
                .Parameters.Add("@Volume_Withdraw", SqlDbType.Float).Value = _Volume_Withdraw
                .Parameters.Add("@Last_Withdraw_Date", SqlDbType.DateTime).Value = _Last_Withdraw_Date
                .Parameters.Add("@UnitPrice", SqlDbType.Float).Value = _UnitPrice
                .Parameters.Add("@Amount", SqlDbType.Float).Value = _Amount
                .Parameters.Add("@Currency_Index", SqlDbType.VarChar).Value = _Currency_Index
                .Parameters.Add("@Discount_Amt", SqlDbType.Float).Value = _Discount_Amt
                .Parameters.Add("@Total_Amt", SqlDbType.Float).Value = _Total_Amt
                .Parameters.Add("@Status", SqlDbType.Int).Value = _Status
                .Parameters.Add("@Remark", SqlDbType.NVarChar).Value = _Remark
                .Parameters.Add("@Reason", SqlDbType.NVarChar).Value = _Reason
                .Parameters.Add("@Ref_No1", SqlDbType.VarChar).Value = _Ref_No1
                .Parameters.Add("@Ref_No2", SqlDbType.VarChar).Value = _Ref_No2
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = _Str1
                .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = _Str2
                .Parameters.Add("@Str3", SqlDbType.NVarChar).Value = _Str3
                .Parameters.Add("@Str4", SqlDbType.NVarChar).Value = _Str4
                .Parameters.Add("@Str5", SqlDbType.NVarChar).Value = _Str5
                .Parameters.Add("@Str6", SqlDbType.NVarChar).Value = _Str6
                .Parameters.Add("@Str7", SqlDbType.NVarChar).Value = _Str7
                .Parameters.Add("@Str8", SqlDbType.NVarChar).Value = _Str8
                .Parameters.Add("@Str9", SqlDbType.NVarChar).Value = _Str9
                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = _Str10
                .Parameters.Add("@Flo1", SqlDbType.Float).Value = _Flo1
                .Parameters.Add("@Flo2", SqlDbType.Float).Value = _Flo2
                .Parameters.Add("@Flo3", SqlDbType.Float).Value = _Flo3
                .Parameters.Add("@Flo4", SqlDbType.Float).Value = _Flo4
                .Parameters.Add("@Flo5", SqlDbType.Float).Value = _Flo5
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                '.Parameters.Add("@update_by", SqlDbType.VarChar).Value = _update_by
                '.Parameters.Add("@update_date", SqlDbType.SmallDateTime).Value = _update_date
                '.Parameters.Add("@update_branch", SqlDbType.Int).Value = _update_branch
                '.Parameters.Add("@cancel_by", SqlDbType.VarChar).Value = _cancel_by
                '.Parameters.Add("@cancel_date", SqlDbType.SmallDateTime).Value = _cancel_date
                '.Parameters.Add("@cancel_branch", SqlDbType.Int).Value = _cancel_branch
                .Parameters.Add("@Charge_Status", SqlDbType.Int).Value = _Charge_Status
                .Parameters.Add("@Itemstatus_index", SqlDbType.NVarChar).Value = _Itemstatus_index
                .Parameters.Add("@Plot", SqlDbType.NVarChar).Value = _Plot
            End With
            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()

            ' --- Commit transaction
            myTrans.Commit()
            _dataTable = Nothing

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()

        End Try
    End Sub
#End Region
End Class
