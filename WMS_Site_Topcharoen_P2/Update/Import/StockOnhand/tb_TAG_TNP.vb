'*** Create Date :  17/01/2008
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data
Imports System.Data.SqlClient


Public Class tb_TAG_TNP : Inherits DBType_SQLServer

    'Private _objItemCollection As List(Of tb_TAG_TNP)
    'Private _objItem As New tb_TAG_TNP(enuOperation_Type.ADDNEW)

    Private _objItemCollection As List(Of tb_TAG_TNP)

    Public Property objItemCollection() As List(Of tb_TAG_TNP)
        Get
            Return _objItemCollection
        End Get
        Set(ByVal value As List(Of tb_TAG_TNP))
            _objItemCollection = value
        End Set
    End Property
    'Private _objItem As New tb_TAG_TNP(enuOperation_Type.ADDNEW)

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _TAG_No As String = ""
    Private _LinkOrderFlag As Boolean
    Private _Order_No As String = ""
    Private _Order_Index As String = ""
    Private _Order_Date As Date
    Private _Order_Time As String = ""
    Private _OrderItem_Index As String = ""
    Private _OrderItemLocation_Index As String = ""
    Private _Customer_Index As String = ""
    Private _Supplier_Index As String = ""
    Private _Sku_Index As String = ""
    Private _PLot As String = ""
    Private _ItemStatus_Index As String = ""
    Private _Package_Index As String = ""
    Private _Unit_Weight As Double
    Private _Size_Index As String = ""
    Private _Pallet_No As String = ""
    Private _Qty As Double
    Private _Weight As Double
    Private _Volume As Double
    Private _Qty_per_TAG As Double
    Private _Weight_per_TAG As Double
    Private _Volume_per_TAG As Double
    Private _TAG_Status As Integer
    Private _Ref_No1 As String = ""
    Private _Ref_No2 As String = ""
    Private _Ref_No3 As String = ""
    Private _Ref_No4 As String = ""
    Private _Ref_No5 As String = ""
    Private _tag_id As String = ""
    Private _description As String = ""
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer

    Private _TAG_Index As String
    Public Property TAG_Index() As String
        Get
            Return _TAG_Index
        End Get
        Set(ByVal value As String)
            _TAG_Index = value
        End Set
    End Property

    Private _suggest_Location_Index As String
    Public Property Suggest_Location_Index() As String
        Get
            Return _suggest_Location_Index
        End Get
        Set(ByVal value As String)
            _suggest_Location_Index = value
        End Set
    End Property



#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum
#End Region

#Region " Properties "
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
    Public Property TAG_No() As String
        Get
            Return _TAG_No
        End Get
        Set(ByVal Value As String)
            _TAG_No = Value
        End Set
    End Property

    Public Property LinkOrderFlag() As Boolean
        Get
            Return _LinkOrderFlag
        End Get
        Set(ByVal Value As Boolean)
            _LinkOrderFlag = Value
        End Set
    End Property

    Public Property Order_No() As String
        Get
            Return _Order_No
        End Get
        Set(ByVal Value As String)
            _Order_No = Value
        End Set
    End Property

    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal Value As String)
            _Order_Index = Value
        End Set
    End Property

    Public Property Order_Date() As Date
        Get
            Return _Order_Date
        End Get
        Set(ByVal Value As Date)
            _Order_Date = Value
        End Set
    End Property

    Public Property Order_Time() As String
        Get
            Return _Order_Time
        End Get
        Set(ByVal Value As String)
            _Order_Time = Value
        End Set
    End Property

    Public Property OrderItem_Index() As String
        Get
            Return _OrderItem_Index
        End Get
        Set(ByVal Value As String)
            _OrderItem_Index = Value
        End Set
    End Property

    Public Property OrderItemLocation_Index() As String
        Get
            Return _OrderItemLocation_Index
        End Get
        Set(ByVal Value As String)
            _OrderItemLocation_Index = Value
        End Set
    End Property

    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Index = Value
        End Set
    End Property

    Public Property Supplier_Index() As String
        Get
            Return _Supplier_Index
        End Get
        Set(ByVal Value As String)
            _Supplier_Index = Value
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

    Public Property PLot() As String
        Get
            Return _PLot
        End Get
        Set(ByVal Value As String)
            _PLot = Value
        End Set
    End Property

    Public Property ItemStatus_Index() As String
        Get
            Return _ItemStatus_Index
        End Get
        Set(ByVal Value As String)
            _ItemStatus_Index = Value
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

    Public Property Unit_Weight() As Double
        Get
            Return _Unit_Weight
        End Get
        Set(ByVal Value As Double)
            _Unit_Weight = Value
        End Set
    End Property

    Public Property Size_Index() As String
        Get
            Return _Size_Index
        End Get
        Set(ByVal Value As String)
            _Size_Index = Value
        End Set
    End Property

    Public Property Pallet_No() As String
        Get
            Return _Pallet_No
        End Get
        Set(ByVal Value As String)
            _Pallet_No = Value
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

    Public Property Qty_per_TAG() As Double
        Get
            Return _Qty_per_TAG
        End Get
        Set(ByVal Value As Double)
            _Qty_per_TAG = Value
        End Set
    End Property

    Public Property Weight_per_TAG() As Double
        Get
            Return _Weight_per_TAG
        End Get
        Set(ByVal Value As Double)
            _Weight_per_TAG = Value
        End Set
    End Property

    Public Property Volume_per_TAG() As Double
        Get
            Return _Volume_per_TAG
        End Get
        Set(ByVal Value As Double)
            _Volume_per_TAG = Value
        End Set
    End Property

    Public Property TAG_Status() As Integer
        Get
            Return _TAG_Status
        End Get
        Set(ByVal Value As Integer)
            _TAG_Status = Value
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

    Public Property Ref_No3() As String
        Get
            Return _Ref_No3
        End Get
        Set(ByVal Value As String)
            _Ref_No3 = Value
        End Set
    End Property

    Public Property Ref_No4() As String
        Get
            Return _Ref_No4
        End Get
        Set(ByVal Value As String)
            _Ref_No4 = Value
        End Set
    End Property

    Public Property Ref_No5() As String
        Get
            Return _Ref_No5
        End Get
        Set(ByVal Value As String)
            _Ref_No5 = Value
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


#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()

        'This call is required by the Windows Form Designer.
        ' InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        '    mObjSearchCri = New uctSearchCri(mMasterType)
    End Sub

#End Region

    '*** Normal DB Method
#Region " SELECT DATA  & SEARCH"
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??


    Public Sub getOrderDetail_Tag(ByVal pstrWhere As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = " select 0 as chkSelect,*  "
            strSQL &= " from VIEW_TAG_OrderItem  Where 1=1 "

            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub getLastTAG()

        Dim strSQL As String = ""

        Try
            strSQL = " select max(TAG_NO)as TAG_NO from tb_tag "

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
    Public Sub getView_Tag(ByVal pstrWhere As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "select * from view_tag where TAG_Status <> -1"

            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getView_Tag_Header(ByVal pstrWhere As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "select '' as Row,convert(bit,0) as chkSelect,* from VIEW_TAG_Header where TAG_Status <> -1 "

            SetSQLString = strSQL & pstrWhere & " ORDER BY TAG_No"


            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getConfig_View(ByVal pstrVIEW As String, ByVal pTagNo As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "SELECT * from " & pstrVIEW

            strSQL &= " WHERE Tag_No = '" & pTagNo & "'"

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

    Public Sub getVIEW_BAFCO_TAG_Header(ByVal pstrWhere As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "select * from VIEW_BAFCO_TAG_Header where TAG_Status <> -1"

            SetSQLString = strSQL & pstrWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub get_Tag(ByVal OrderItem_Index As String)
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "  select TAG_No from tb_tag "
            strSQL += "  where OrderItem_Index ='" & OrderItem_Index & "'  and TAG_Status <> -1"

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


    Public Function getRptView_Tag(ByVal pstrWhere As String) As DataSet
        '  
        Dim strSQL As String = ""

        Try
            strSQL = "select * from view_tag where TAG_Status <> -1 "
            strSQL &= pstrWhere
            With SQLServerCommand
                .Connection = Connection
                .CommandText = strSQL
                ' strSql = Nothing
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DS = New DataSet("dsTAG")
            DataAdapter.Fill(DS, "VIEW_TAG")

            Return DS
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    'Public Sub SearchData_ByTagNO(ByVal pstrTagNO As String)
    '    '  
    '    Dim strSQL As String = ""
    '    Dim strWhere As String = ""
    '    Try
    '        strSQL = " select * from view_tag where tag_no ='" & pstrTagNO & "' and tag_Status <> -1"
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


    Public Sub SearchData_Click(ByVal pstrColumnName As String, ByVal pstrFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            'If ColumnName = "" Then
            ' Select No Condition 
            strSQL = " SELECT  " & pstrColumnName & _
                     " FROM       tb_TAG where TAG_Status != -1"

            strWhere = pstrFilterValue
            'Else
            ' Sql for define ColumnName & Filter Value 

            'End If

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SelectData_For_Edit(ByVal pFilterValue As String)
        Dim strSQL As String
        Dim strWhere As String
        Try

            strSQL = " SELECT     *   " & _
                     " FROM       tb_tag "

            strWhere = ""

            strWhere += " WHERE  tb_tag.tag_id = '" & pFilterValue & "'"


            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchTAG(ByVal TAG_Index As String, ByVal pFilterValue As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = "SELECT     *    FROM       tb_tag where TAG_Index ='" & TAG_Index & "'"
            strWhere = pFilterValue

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getPutawayWithTAG(ByVal Order_Index As String, Optional ByVal ListTag_Index As List(Of String) = Nothing)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " select *,'' Suggest_Location_Index from VIEW_PutawayWithTAG "
            strWhere = "  WHERE Order_Index ='" & Order_Index & "' and Tag_Status =1 "

            If ListTag_Index Is Nothing Then

            Else
                strWhere &= " AND TAG_Index in ( 'xxx'"
                For Each StrList As String In ListTag_Index
                    strWhere &= ",'" & StrList & "'"
                Next
                strWhere &= ") "
            End If

            strSQL = strSQL & strWhere & " ORDER BY TAG_No"

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

        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT * "
            strSQL &= " FROM     VIEW_ProcessStatus "
            strSQL &= " WHERE Process_Id= 101  AND Show=1  "
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

    Public Sub getOrderItemNonTag(ByVal pstrOrder_index As String)

        Dim strSQL As String = ""
        Try
            strSQL = " Select  tb_Order.Order_No"
            strSQL &= ",tb_Order.Order_Index"
            strSQL &= ",tb_Order.Order_Date"
            strSQL &= ",tb_Order.Order_Time"
            strSQL &= ",tb_Order.Customer_Index"
            strSQL &= ",tb_Order.Supplier_Index"
            strSQL &= ",tb_OrderItem.OrderItem_Index"
            strSQL &= ",tb_OrderItem.SKU_Index"
            strSQL &= ",tb_OrderItem.Plot"
            strSQL &= ",tb_OrderItem.ItemStatus_Index"
            strSQL &= ",tb_OrderItem.Package_Index"
            'strSQL &= ",tb_OrderItem.Qty"
            strSQL &= ",tb_OrderItem.Weight"
            strSQL &= ",tb_OrderItem.Volume"
            strSQL &= ",tb_OrderItem.PalletType_Index"
            strSQL &= ",tb_OrderItem.Str1"
            strSQL &= ",tb_OrderItem.Str2"
            strSQL &= ",Qty = isnull((tb_OrderItem.Qty - isnull((select sum(tb_Tag.Qty_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1,2) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index ),0)),0)"
            strSQL &= " FROM  tb_Order INNER JOIN tb_OrderItem ON"
            strSQL &= " tb_Order.Order_Index = tb_OrderItem.Order_Index "
            strSQL &= " WHERE  tb_Order.Order_Index = '" & pstrOrder_index & "'"
            strSQL &= "        AND isnull((tb_OrderItem.Qty - isnull((select sum(tb_Tag.Qty_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index ),0)),0) > 0 "
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
#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??

    Public Sub Insert()
        Dim strSQL As String = " "
        Try
            strSQL = " INSERT INTO tb_Tag(TAG_Index,TAG_No,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,add_by,add_date,add_branch,Suggest_Location_Index)" & _
            "       VALUES(@TAG_Index,@TAG_No,@LinkOrderFlag,@Order_No,@Order_Index,@Order_Date,@Order_Time,@OrderItem_Index,@OrderItemLocation_Index,@Customer_Index,@Supplier_Index,@Sku_Index,@PLot,@ItemStatus_Index,@Package_Index,@Unit_Weight,@Size_Index,@Pallet_No,@Qty,@Weight,@Volume,@Qty_per_TAG,@Weight_per_TAG,@Volume_per_TAG,@TAG_Status,@Ref_No1,@Ref_No2,@Ref_No3,@Ref_No4,@Ref_No5,@add_by,getdate(),@add_branch,@Suggest_Location_Index)"
            With SQLServerCommand.Parameters
                .Clear()

                .Add(New SqlParameter("@TAG_Index", SqlDbType.NVarChar, 13)).Value = _TAG_Index
                .Add(New SqlParameter("@TAG_No", SqlDbType.VarChar, 50)).Value = _TAG_No
                .Add(New SqlParameter("@LinkOrderFlag", SqlDbType.Bit)).Value = _LinkOrderFlag
                .Add(New SqlParameter("@Order_No", SqlDbType.VarChar, 50)).Value = _Order_No
                .Add(New SqlParameter("@Order_Index", SqlDbType.VarChar, 13)).Value = _Order_Index
                .Add(New SqlParameter("@Order_Date", SqlDbType.SmallDateTime)).Value = _Order_Date.ToString("yyyy/MM/dd")
                .Add(New SqlParameter("@Order_Time", SqlDbType.VarChar, 50)).Value = _Order_Time
                .Add(New SqlParameter("@OrderItem_Index", SqlDbType.VarChar, 13)).Value = _OrderItem_Index
                .Add(New SqlParameter("@OrderItemLocation_Index", SqlDbType.VarChar, 13)).Value = _OrderItemLocation_Index
                .Add(New SqlParameter("@Customer_Index", SqlDbType.VarChar, 13)).Value = _Customer_Index
                .Add(New SqlParameter("@Supplier_Index", SqlDbType.VarChar, 13)).Value = _Supplier_Index
                .Add(New SqlParameter("@Sku_Index", SqlDbType.Char, 13)).Value = _Sku_Index
                .Add(New SqlParameter("@PLot", SqlDbType.NVarChar, 50)).Value = _PLot
                .Add(New SqlParameter("@ItemStatus_Index", SqlDbType.VarChar, 13)).Value = _ItemStatus_Index
                .Add(New SqlParameter("@Package_Index", SqlDbType.VarChar, 13)).Value = _Package_Index
                .Add(New SqlParameter("@Unit_Weight", SqlDbType.Float)).Value = _Unit_Weight
                .Add(New SqlParameter("@Size_Index", SqlDbType.VarChar, 13)).Value = _Size_Index
                .Add(New SqlParameter("@Pallet_No", SqlDbType.VarChar, 50)).Value = _Pallet_No
                .Add(New SqlParameter("@Qty", SqlDbType.Float)).Value = _Qty
                .Add(New SqlParameter("@Weight", SqlDbType.Float)).Value = _Weight
                .Add(New SqlParameter("@Volume", SqlDbType.Float)).Value = _Volume
                .Add(New SqlParameter("@Qty_per_TAG", SqlDbType.Float)).Value = _Qty_per_TAG
                .Add(New SqlParameter("@Weight_per_TAG", SqlDbType.Float)).Value = _Weight_per_TAG
                .Add(New SqlParameter("@Volume_per_TAG", SqlDbType.Float)).Value = _Volume_per_TAG
                .Add(New SqlParameter("@TAG_Status", SqlDbType.Int)).Value = 1
                .Add(New SqlParameter("@Ref_No1", SqlDbType.VarChar, 50)).Value = _Ref_No1
                .Add(New SqlParameter("@Ref_No2", SqlDbType.VarChar, 50)).Value = _Ref_No2
                .Add(New SqlParameter("@Ref_No3", SqlDbType.VarChar, 50)).Value = _Ref_No3
                .Add(New SqlParameter("@Ref_No4", SqlDbType.VarChar, 50)).Value = _Ref_No4
                .Add(New SqlParameter("@Ref_No5", SqlDbType.VarChar, 50)).Value = _Ref_No5
                .Add(New SqlParameter("@add_by", SqlDbType.VarChar, 50)).Value = _add_by
                '.Add(New SqlParameter("@add_date", SqlDbType.SmallDateTime, 16)).Value = _add_date
                .Add(New SqlParameter("@add_branch", SqlDbType.Int)).Value = _add_branch
                .Add(New SqlParameter("@Suggest_Location_Index", SqlDbType.VarChar, 13)).Value = Me._suggest_Location_Index
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
#End Region

#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Sub Update()
        Dim strSQL As String = " "
        Try
            strSQL = " UPDATE tb_Tag" & _
            " SET TAG_No=@TAG_No," & _
            "     LinkOrderFlag=@LinkOrderFlag," & _
            "     Order_No=@Order_No," & _
            "     Order_Index=@Order_Index," & _
            "     Order_Date=@Order_Date," & _
            "     OrderItem_Index=@OrderItem_Index," & _
            "     OrderItemLocation_Index=@OrderItemLocation_Index," & _
            "     Customer_Index=@Customer_Index," & _
            "     Supplier_Index=@Supplier_Index," & _
            "     Sku_Index=@Sku_Index," & _
            "     PLot=@PLot," & _
            "     ItemStatus_Index=@ItemStatus_Index," & _
            "     Unit_Weight=@Unit_Weight," & _
            "     Size_Index=@Size_Index," & _
            "     Pallet_No=@Pallet_No," & _
            "     Qty_per_TAG=@Qty_per_TAG," & _
            "     Weight_per_TAG=@Weight_per_TAG," & _
            "     Volume_per_TAG=@Volume_per_TAG," & _
            "     Ref_No1=@Ref_No1," & _
            "     Ref_No2=@Ref_No2," & _
            "     update_by=@update_by," & _
            "     update_date=(getdate())," & _
            "     update_branch=@update_branch " & _
            "           WHERE          TAG_Index = @TAG_Index"


            '   "     Order_Time=@Order_Time," & _
            '   "     Qty=@Qty," & _
            '   "     Weight=@Weight," & _
            '   "     Volume=@Volume," & _
            '   "     Package_Index=@Package_Index," & _
            With SQLServerCommand.Parameters
                .Clear()
                .Add(New SqlParameter("@TAG_Index", SqlDbType.VarChar, 13)).Value = _TAG_Index
                .Add(New SqlParameter("@TAG_No", SqlDbType.VarChar, 50)).Value = _TAG_No
                .Add(New SqlParameter("@LinkOrderFlag", SqlDbType.Bit)).Value = _LinkOrderFlag
                .Add(New SqlParameter("@Order_No", SqlDbType.VarChar, 50)).Value = _Order_No
                .Add(New SqlParameter("@Order_Index", SqlDbType.VarChar, 13)).Value = _Order_Index
                .Add(New SqlParameter("@Order_Date", SqlDbType.SmallDateTime)).Value = _Order_Date
                '.Add(New SqlParameter("@Order_Time", SqlDbType.VarChar, 50)).Value = _Order_Time
                .Add(New SqlParameter("@OrderItem_Index", SqlDbType.VarChar, 13)).Value = _OrderItem_Index
                .Add(New SqlParameter("@OrderItemLocation_Index", SqlDbType.VarChar, 13)).Value = _OrderItemLocation_Index
                .Add(New SqlParameter("@Customer_Index", SqlDbType.VarChar, 13)).Value = _Customer_Index
                .Add(New SqlParameter("@Supplier_Index", SqlDbType.VarChar, 13)).Value = _Supplier_Index
                .Add(New SqlParameter("@Sku_Index", SqlDbType.Char, 13)).Value = _Sku_Index
                .Add(New SqlParameter("@PLot", SqlDbType.NVarChar, 50)).Value = _PLot
                .Add(New SqlParameter("@ItemStatus_Index", SqlDbType.VarChar, 13)).Value = _ItemStatus_Index
                '.Add(New SqlParameter("@Package_Index", SqlDbType.VarChar, 13)).Value = _Package_Index
                .Add(New SqlParameter("@Unit_Weight", SqlDbType.Float)).Value = _Unit_Weight
                .Add(New SqlParameter("@Size_Index", SqlDbType.VarChar, 13)).Value = _Size_Index
                .Add(New SqlParameter("@Pallet_No", SqlDbType.VarChar, 50)).Value = _Pallet_No
                ' .Add(New SqlParameter("@Qty", SqlDbType.Float, 15)).Value = _Qty
                ' .Add(New SqlParameter("@Weight", SqlDbType.Float, 15)).Value = _Weight
                ' .Add(New SqlParameter("@Volume", SqlDbType.Float, 15)).Value = _Volume
                .Add(New SqlParameter("@Qty_per_TAG", SqlDbType.Float)).Value = _Qty_per_TAG
                .Add(New SqlParameter("@Weight_per_TAG", SqlDbType.Float)).Value = _Weight_per_TAG
                .Add(New SqlParameter("@Volume_per_TAG", SqlDbType.Float)).Value = _Volume_per_TAG
                '.Add(New SqlParameter("@TAG_Status", SqlDbType.Int, 10)).Value = _TAG_Status
                .Add(New SqlParameter("@Ref_No1", SqlDbType.VarChar, 50)).Value = _Ref_No1
                .Add(New SqlParameter("@Ref_No2", SqlDbType.VarChar, 50)).Value = _Ref_No2
                .Add(New SqlParameter("@update_by", SqlDbType.VarChar, 50)).Value = WV_UserName
                .Add(New SqlParameter("@update_branch", SqlDbType.Int)).Value = WV_Branch_ID
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

#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function Delete(ByVal TAG_Index As String) As Boolean
        ' *** Define value from parameter
        Me._TAG_Index = TAG_Index

        Dim strSQL As String

        Try
            'strSQL = " Delete  " & _
            '         " From tb_tag " & _
            '         " WHERE TAG_Index='" + Me._TAG_Index + "' "

            strSQL = "update tb_tag set TAG_status = -1"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",update_by= '" & WV_UserName & "' "
            strSQL &= " WHERE TAG_Index ='" + Me._TAG_Index + "' "

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

    ''' <summary>
    ''' Delete all tag for the specified Order Item
    ''' </summary>
    ''' <param name="pTAG_Index"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function DeleteAllTagForOrderItem(ByVal pTAG_Index As String) As Boolean
        ' *** Define value from parameter
        Me._TAG_Index = TAG_Index

        Dim strSQL As String

        Try
            strSQL = "UPDATE tb_TAG SET TAG_status = -1"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",update_by= '" & WV_UserName & "' "
            'strSQL &= " WHERE OrderItem_Index IN (SELECT OrderItem_Index FROM tb_TAG WHERE TAG_Index = '" & Me._TAG_Index & "') "
            strSQL &= " WHERE TAG_Index = '" & pTAG_Index & "'"
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            strSQL = "UPDATE tb_OrderItemLocation SET Status = -1"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",update_by= '" & WV_UserName & "' "
            strSQL &= " WHERE OrderItemLocation_Index IN (SELECT OrderItemLocation_Index FROM tb_TAG WHERE TAG_Index = '" & Me._TAG_Index & "') "

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

    Public Function UpdatePrintStatus(ByVal TAG_Index As String) As Boolean
        ' *** Define value from parameter
        Me._TAG_Index = TAG_Index

        Dim strSQL As String

        Try
            strSQL = "UPDATE tb_TAG SET "
            strSQL &= " Print_Status = 1 "
            strSQL &= ",update_branch= " & WV_Branch_ID & " "
            strSQL &= ",update_by= '" & WV_UserName & "' "
            strSQL &= " WHERE  TAG_Index = '" & Me._TAG_Index & "' "

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


    Public Function isChckID(ByVal _tag_id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_tag where tag_id = @tag_id and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@tag_id", SqlDbType.VarChar, 15).Value = _tag_id

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
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function isExistID(ByVal _tag_id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from tb_tag where tag_id = @tag_id  and status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@tag_id", SqlDbType.VarChar, 20).Value = _tag_id

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
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "
    Public Function InsertData() As String
        Dim strSQL As String = ""
        Dim _objItem As New tb_TAG_TNP(enuOperation_Type.ADDNEW)
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try

            For Each _objItem In _objItemCollection

                strSQL = " INSERT INTO tb_Tag(TAG_Index,TAG_No,LinkOrderFlag,Order_No,Order_Index,Order_Date,Order_Time,OrderItem_Index,OrderItemLocation_Index,Customer_Index,Supplier_Index,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,Ref_No1,Ref_No2,Ref_No3,add_by,add_branch,Suggest_Location_Index)" & _
                "    VALUES(@TAG_Index,@TAG_No,@LinkOrderFlag,@Order_No,@Order_Index,@Order_Date,@Order_Time,@OrderItem_Index,@OrderItemLocation_Index,@Customer_Index,@Supplier_Index,@Sku_Index,@PLot,@ItemStatus_Index,@Package_Index,@Unit_Weight,@Size_Index,@Pallet_No,@Qty,@Weight,@Volume,@Qty_per_TAG,@Weight_per_TAG,@Volume_per_TAG,@TAG_Status,@Ref_No1,@Ref_No2,@Ref_No3,@add_by,@add_branch,@Suggest_Location_Index)"

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@TAG_Index", SqlDbType.VarChar, 13).Value = _objItem._TAG_Index
                    .Parameters.Add("@TAG_No", SqlDbType.VarChar, 50).Value = _objItem._TAG_No
                    .Parameters.Add("@LinkOrderFlag", SqlDbType.Bit).Value = _objItem._LinkOrderFlag
                    .Parameters.Add("@Order_No", SqlDbType.VarChar, 50).Value = _objItem._Order_No
                    .Parameters.Add("@Order_Index", SqlDbType.VarChar, 13).Value = _objItem._Order_Index
                    .Parameters.Add("@Order_Date", SqlDbType.SmallDateTime).Value = _objItem._Order_Date
                    .Parameters.Add("@Order_Time", SqlDbType.VarChar, 50).Value = _objItem._Order_Time
                    .Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = _objItem._OrderItem_Index 'bug
                    .Parameters.Add("@OrderItemLocation_Index", SqlDbType.VarChar, 13).Value = _objItem._OrderItemLocation_Index 'bug
                    .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _objItem._Customer_Index
                    .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _objItem._Supplier_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.Char, 13).Value = _objItem._Sku_Index
                    .Parameters.Add("@PLot", SqlDbType.NVarChar, 50).Value = _objItem._PLot
                    .Parameters.Add("@ItemStatus_Index", SqlDbType.VarChar, 13).Value = _objItem._ItemStatus_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar, 13).Value = _objItem._Package_Index
                    .Parameters.Add("@Unit_Weight", SqlDbType.Float).Value = _objItem._Unit_Weight
                    .Parameters.Add("@Size_Index", SqlDbType.VarChar, 13).Value = _objItem._Size_Index
                    .Parameters.Add("@Pallet_No", SqlDbType.VarChar, 50).Value = _objItem._Pallet_No
                    .Parameters.Add("@Qty", SqlDbType.Float).Value = _objItem._Qty
                    .Parameters.Add("@Weight", SqlDbType.Float).Value = _objItem._Weight
                    .Parameters.Add("@Volume", SqlDbType.Float).Value = _objItem._Volume
                    .Parameters.Add("@Qty_per_TAG", SqlDbType.Float).Value = _objItem._Qty_per_TAG
                    .Parameters.Add("@Weight_per_TAG", SqlDbType.Float).Value = _objItem._Weight_per_TAG
                    .Parameters.Add("@Volume_per_TAG", SqlDbType.Float).Value = _objItem._Volume_per_TAG
                    .Parameters.Add("@TAG_Status", SqlDbType.Int).Value = 1 '_objItem._TAG_Status
                    .Parameters.Add("@Ref_No1", SqlDbType.VarChar, 50).Value = _objItem._Ref_No1
                    .Parameters.Add("@Ref_No2", SqlDbType.VarChar, 50).Value = _objItem._Ref_No2
                    .Parameters.Add("@Ref_No3", SqlDbType.VarChar, 50).Value = _objItem._Ref_No3
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@Suggest_Location_Index", SqlDbType.VarChar, 13).Value = _objItem._suggest_Location_Index
                End With

                SetSQLString = strSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            Next

            myTrans.Commit()
            Return _objItem._TAG_No
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally

            disconnectDB()
        End Try
    End Function

    'Public Sub InsertData_Only_SKU()
    '    Dim strSQL As String = ""
    '    'Dim _objItem As New tb_TAG_TNP(enuOperation_Type.ADDNEW)
    '    connectDB()
    '    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
    '    SQLServerCommand.Transaction = myTrans
    '    Try
    '        Dim i As Integer
    '        For i = 0 To _objItemCollection.Count - 1
    '            strSQL = " INSERT INTO tb_Tag(TAG_No,LinkOrderFlag,Sku_Index,PLot,ItemStatus_Index,Package_Index,Unit_Weight,Size_Index,Pallet_No,Qty,Weight,Volume,Qty_per_TAG,Weight_per_TAG,Volume_per_TAG,TAG_Status,add_by,add_branch)" & _
    '                            "      VALUES(@TAG_No,@LinkOrderFlag,@Sku_Index,@PLot,@ItemStatus_Index,@Package_Index,@Unit_Weight,@Size_Index,@Pallet_No,@Qty,@Weight,@Volume,@Qty_per_TAG,@Weight_per_TAG,@Volume_per_TAG,@TAG_Status,@add_by,@add_branch)"

    '            With SQLServerCommand.Parameters
    '                .Clear()
    '                .Add(New SqlParameter("@TAG_Index", SqlDbType.VarChar, 50)).Value = _objItemCollection(i)._TAG_Index
    '                .Add(New SqlParameter("@TAG_No", SqlDbType.VarChar, 50)).Value = _objItemCollection(i)._TAG_No
    '                .Add(New SqlParameter("@LinkOrderFlag", SqlDbType.Bit, 1)).Value = _objItemCollection(i)._LinkOrderFlag
    '                .Add(New SqlParameter("@Sku_Index", SqlDbType.Char, 13)).Value = _objItemCollection(i)._Sku_Index
    '                .Add(New SqlParameter("@PLot", SqlDbType.NVarChar, 50)).Value = _objItemCollection(i)._PLot
    '                .Add(New SqlParameter("@ItemStatus_Index", SqlDbType.VarChar, 13)).Value = _objItemCollection(i)._ItemStatus_Index
    '                .Add(New SqlParameter("@Package_Index", SqlDbType.VarChar, 13)).Value = _objItemCollection(i)._Package_Index '= "001"
    '                .Add(New SqlParameter("@Unit_Weight", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Unit_Weight
    '                .Add(New SqlParameter("@Size_Index", SqlDbType.VarChar, 13)).Value = _objItemCollection(i)._Size_Index
    '                .Add(New SqlParameter("@Pallet_No", SqlDbType.VarChar, 50)).Value = _objItemCollection(i)._Pallet_No
    '                .Add(New SqlParameter("@Qty", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Qty
    '                .Add(New SqlParameter("@Weight", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Weight
    '                .Add(New SqlParameter("@Volume", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Volume
    '                .Add(New SqlParameter("@Qty_per_TAG", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Qty_per_TAG
    '                .Add(New SqlParameter("@Weight_per_TAG", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Weight_per_TAG
    '                .Add(New SqlParameter("@Volume_per_TAG", SqlDbType.Float, 15)).Value = _objItemCollection(i)._Volume_per_TAG
    '                .Add(New SqlParameter("@TAG_Status", SqlDbType.Int, 10)).Value = _objItemCollection(i)._TAG_Status
    '                .Add(New SqlParameter("@add_by", SqlDbType.VarChar, 50)).Value = WV_UserName
    '                .Add(New SqlParameter("@add_branch", SqlDbType.Int, 10)).Value = WV_Branch_ID
    '            End With
    '            SetSQLString = strSQL
    '            SetCommandType = enuCommandType.Text
    '            SetEXEC_TYPE = EXEC.NonQuery
    '            connectDB()
    '            EXEC_Command()
    '        Next
    '        myTrans.Commit()
    '    Catch ex As Exception
    '        myTrans.Rollback()
    '        Throw ex
    '    Finally

    '        disconnectDB()
    '    End Try
    'End Sub
#End Region
End Class