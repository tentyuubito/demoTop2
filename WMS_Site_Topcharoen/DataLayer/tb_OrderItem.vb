'*** Create Date :  17/01/2008
Imports WMS_STD_Formula

Public Class tb_OrderItem : Inherits DBType_SQLServer

    '*** Fileds         
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _orderitem_Index As String = ""
    Private _order_Index As String = ""
    Private _sku_Index As String
    Private _lot_No As String = ""
    Private _plot As String = ""
    Private _itemStatus_Index As String = ""
    Private _package_Index As String = ""
    Private _ratio As Decimal
    Private _qty As Decimal
    Private _total_qty As Decimal
    Private _plan_Qty As Decimal
    Private _palletType_Index As String = ""
    Private _pallet_Qty As Decimal
    Private _weight As Decimal
    Private _volume As Decimal
    Private _serial_No As String = ""
    Private _mfg_Date As Date = Now
    Private _exp_date As Date = Now
    Private _comment As String = ""
    Private _str1 As String = ""
    Private _str2 As String = ""
    Private _str3 As String = ""
    Private _str4 As String = ""
    Private _str5 As String = ""
    Private _str6 As String = ""
    Private _str7 As String = ""
    Private _str8 As String = ""
    Private _str9 As String = ""
    Private _str10 As String = ""
    Private _flo1 As Decimal
    Private _flo2 As Decimal
    Private _flo3 As Decimal
    Private _flo4 As Decimal
    Private _flo5 As Decimal
    Private _add_by As String = ""
    Private _add_date As Date = Now
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date = Now
    Private _update_branch As Integer
    Private _IsMfg_Date As Boolean
    Private _IsExp_Date As Boolean
    Private _status As Integer
    Private _itemDefinition_Index As String = ""


    Private _iRow_OrderItem_Index As Integer = 0
    Private _Weight_Per_Pck As Decimal = 0
    Private _Volume_Per_Pck As Decimal = 0
    Private _Price_Per_Pck As Decimal = 0
    Private _Qty_Per_Pck As Decimal = 0
    Private _Item_Qty As Decimal = 0
    Private _Item_Package_Index As String = ""
    Private _OrderItem_Price As Decimal = 0

    Private _PO_No As String = ""
    Private _ASN_No As String = ""
    Private _Invoice_No As String = ""
    Private _Is_SN As Boolean = 0


    Private _Declaration_No As String = ""
    Private _HandlingType_Index As String = ""

    Private _DocumentPlan_No As String = ""
    Private _DocumentPlan_Index As String = ""
    Private _DocumentPlanItem_Index As String = ""
    Private _Plan_Process As Integer = -9
    Private _OrderItem_RowIndex As Integer
    Private _Shipment_No As String = ""

    'include from master site
    Private _Tax1 As Decimal = 0
    Private _Tax2 As Decimal = 0
    Private _Tax3 As Decimal = 0
    Private _Tax4 As Decimal = 0
    Private _Tax5 As Decimal = 0

    Private _HS_Code As String = ""
    Private _ItemDescription As String = ""
    Private _Seq As Integer = 0
    Private _Consignee_Index As String = "" ' ja add new  12-01-2010


    Private _ERP_Location As String = ""
    Public Property ERP_Location() As String
        Get
            Return _ERP_Location
        End Get
        Set(ByVal value As String)
            _ERP_Location = value
        End Set
    End Property

    Private _WeightScale As Decimal
    Public Property WeightScale() As Decimal
        Get
            Return (Me._WeightScale)
        End Get
        Set(ByVal value As Decimal)
            Me._WeightScale = value
        End Set
    End Property


#Region " Properties Section "


	'include from master site
    Public Property Tax1() As Decimal
        Get
            Return _Tax1
        End Get
        Set(ByVal Value As Decimal)
            _Tax1 = Value
        End Set
    End Property
    Public Property Tax2() As Decimal
        Get
            Return _Tax2
        End Get
        Set(ByVal Value As Decimal)
            _Tax2 = Value
        End Set
    End Property
    Public Property Tax3() As Decimal
        Get
            Return _Tax3
        End Get
        Set(ByVal Value As Decimal)
            _Tax3 = Value
        End Set
    End Property
    Public Property Tax4() As Decimal
        Get
            Return _Tax4
        End Get
        Set(ByVal Value As Decimal)
            _Tax4 = Value
        End Set
    End Property
    Public Property Tax5() As Decimal
        Get
            Return _Tax5
        End Get
        Set(ByVal Value As Decimal)
            _Tax5 = Value
        End Set
    End Property

    Public Property HS_Code() As String
        Get
            Return (Me._HS_Code)
        End Get
        Set(ByVal value As String)
            Me._HS_Code = value
        End Set
    End Property

    Public Property ItemDescription() As String
        Get
            Return (Me._ItemDescription)
        End Get
        Set(ByVal value As String)
            Me._ItemDescription = value
        End Set
    End Property
    Public Property Seq() As Integer
        Get
            Return (Me._Seq)
        End Get
        Set(ByVal value As Integer)
            Me._Seq = value
        End Set
    End Property

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

    '*** Property Writeonly

    '*** Property Read and Write
    Public Property Plan_Process() As Integer
        Get
            Return (Me._Plan_Process)
        End Get
        Set(ByVal value As Integer)
            Me._Plan_Process = value
        End Set
    End Property

    Public Property Consignee_Index() As String
        Get
            Return _Consignee_Index
        End Get
        Set(ByVal Value As String)
            _Consignee_Index = Value
        End Set
    End Property


    Public Property DocumentPlan_No() As String
        Get
            Return (Me._DocumentPlan_No)
        End Get
        Set(ByVal value As String)
            Me._DocumentPlan_No = value
        End Set
    End Property
    Public Property DocumentPlan_Index() As String
        Get
            Return (Me._DocumentPlan_Index)
        End Get
        Set(ByVal value As String)
            Me._DocumentPlan_Index = value
        End Set
    End Property


    Public Property DocumentPlanItem_Index() As String
        Get
            Return (Me._DocumentPlanItem_Index)
        End Get
        Set(ByVal value As String)
            Me._DocumentPlanItem_Index = value
        End Set
    End Property

    Public Property OrderItem_RowIndex() As Integer
        Get
            Return _OrderItem_RowIndex
        End Get
        Set(ByVal Value As Integer)
            _OrderItem_RowIndex = Value
        End Set
    End Property

    Public Property OrderItem_Index() As String
        Get
            Return _orderitem_Index
        End Get
        Set(ByVal value As String)
            _orderitem_Index = value
        End Set
    End Property
    Public Property Order_Index() As String
        Get
            Return _order_Index
        End Get
        Set(ByVal Value As String)
            _order_Index = Value
        End Set
    End Property
    Public Property Sku_Index() As String
        Get
            Return _sku_Index
        End Get
        Set(ByVal Value As String)
            _sku_Index = Value
        End Set
    End Property
    Public Property Lot_No() As String
        Get
            Return _lot_No
        End Get
        Set(ByVal Value As String)
            _lot_No = Value
        End Set
    End Property
    Public Property Plot() As String
        Get
            Return _plot
        End Get
        Set(ByVal Value As String)
            _plot = Value
        End Set
    End Property
    Public Property ItemStatus_Index() As String
        Get
            Return _itemStatus_Index
        End Get
        Set(ByVal Value As String)
            _itemStatus_Index = Value
        End Set
    End Property
    Public Property Package_Index() As String
        Get
            Return _package_Index
        End Get
        Set(ByVal Value As String)
            _package_Index = Value
        End Set
    End Property
    Public Property Ratio() As Decimal
        Get
            Return _ratio
        End Get
        Set(ByVal Value As Decimal)
            _ratio = Value
        End Set
    End Property
    Public Property Qty() As Decimal
        Get
            Return _qty
        End Get
        Set(ByVal Value As Decimal)
            _qty = Value
        End Set
    End Property
    Public Property Total_Qty() As Decimal
        Get
            Return _total_qty
        End Get
        Set(ByVal Value As Decimal)
            _total_qty = Value
        End Set
    End Property
    Public Property Plan_Qty() As Decimal
        Get
            Return _plan_Qty
        End Get
        Set(ByVal Value As Decimal)
            _plan_Qty = Value
        End Set
    End Property
    Public Property PalletType_Index() As String
        Get
            Return _palletType_Index
        End Get
        Set(ByVal Value As String)
            _palletType_Index = Value
        End Set
    End Property
    Public Property Pallet_Qty() As Decimal
        Get
            Return _pallet_Qty
        End Get
        Set(ByVal Value As Decimal)
            _pallet_Qty = Value
        End Set
    End Property
    Public Property Weight() As Decimal
        Get
            Return _weight
        End Get
        Set(ByVal Value As Decimal)
            _weight = Value
        End Set
    End Property
    Public Property Volume() As Decimal
        Get
            Return _volume
        End Get
        Set(ByVal Value As Decimal)
            _volume = Value
        End Set
    End Property
    Public Property Serial_No() As String
        Get
            Return _serial_No
        End Get
        Set(ByVal Value As String)
            _serial_No = Value
        End Set
    End Property
    Public Property Mfg_Date() As Date
        Get
            Return _mfg_Date
        End Get
        Set(ByVal Value As Date)
            _mfg_Date = Value
        End Set
    End Property
    Public Property Exp_date() As Date
        Get
            Return _exp_date
        End Get
        Set(ByVal Value As Date)
            _exp_date = Value
        End Set
    End Property
    Public Property Comment() As String
        Get
            Return _comment
        End Get
        Set(ByVal Value As String)
            _comment = Value
        End Set
    End Property
    Public Property Str1() As String
        Get
            Return _str1
        End Get
        Set(ByVal Value As String)
            _str1 = Value
        End Set
    End Property
    Public Property Str2() As String
        Get
            Return _str2
        End Get
        Set(ByVal Value As String)
            _str2 = Value
        End Set
    End Property
    Public Property Str3() As String
        Get
            Return _str3
        End Get
        Set(ByVal Value As String)
            _str3 = Value
        End Set
    End Property
    Public Property Str4() As String
        Get
            Return _str4
        End Get
        Set(ByVal Value As String)
            _str4 = Value
        End Set
    End Property
    Public Property Str5() As String
        Get
            Return _str5
        End Get
        Set(ByVal Value As String)
            _str5 = Value
        End Set
    End Property
    Public Property Str6() As String
        Get
            Return _str6
        End Get
        Set(ByVal Value As String)
            _str6 = Value
        End Set
    End Property
    Public Property Str7() As String
        Get
            Return _str7
        End Get
        Set(ByVal Value As String)
            _str7 = Value
        End Set
    End Property
    Public Property Str8() As String
        Get
            Return _str8
        End Get
        Set(ByVal Value As String)
            _str8 = Value
        End Set
    End Property
    Public Property Str9() As String
        Get
            Return _str9
        End Get
        Set(ByVal Value As String)
            _str9 = Value
        End Set
    End Property
    Public Property Str10() As String
        Get
            Return _str10
        End Get
        Set(ByVal Value As String)
            _str10 = Value
        End Set
    End Property
    Public Property Flo1() As Decimal
        Get
            Return _flo1
        End Get
        Set(ByVal Value As Decimal)
            _flo1 = Value
        End Set
    End Property
    Public Property Flo2() As Decimal
        Get
            Return _flo2
        End Get
        Set(ByVal Value As Decimal)
            _flo2 = Value
        End Set
    End Property
    Public Property Flo3() As Decimal
        Get
            Return _flo3
        End Get
        Set(ByVal Value As Decimal)
            _flo3 = Value
        End Set
    End Property
    Public Property Flo4() As Decimal
        Get
            Return _flo4
        End Get
        Set(ByVal Value As Decimal)
            _flo4 = Value
        End Set
    End Property
    Public Property Flo5() As Decimal
        Get
            Return _flo5
        End Get
        Set(ByVal Value As Decimal)
            _flo5 = Value
        End Set
    End Property
    Public Property Add_by() As String
        Get
            Return _add_by
        End Get
        Set(ByVal Value As String)
            _add_by = Value
        End Set
    End Property
    Public Property Add_date() As Date
        Get
            Return _add_date
        End Get
        Set(ByVal Value As Date)
            _add_date = Value
        End Set
    End Property
    Public Property Add_branch() As Integer
        Get
            Return _add_branch
        End Get
        Set(ByVal Value As Integer)
            _add_branch = Value
        End Set
    End Property
    Public Property Update_by() As String
        Get
            Return _update_by
        End Get
        Set(ByVal Value As String)
            _update_by = Value
        End Set
    End Property
    Public Property Update_date() As Date
        Get
            Return _update_date
        End Get
        Set(ByVal Value As Date)
            _update_date = Value
        End Set
    End Property
    Public Property Update_branch() As Integer
        Get
            Return _update_branch
        End Get
        Set(ByVal Value As Integer)
            _update_branch = Value
        End Set
    End Property

    Public Property IsMfg_Date() As Boolean
        Get
            Return _IsMfg_Date
        End Get
        Set(ByVal value As Boolean)
            _IsMfg_Date = value
        End Set
    End Property

    Public Property IsExp_Date() As Boolean
        Get
            Return _IsExp_Date
        End Get
        Set(ByVal value As Boolean)
            _IsExp_Date = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _status
        End Get
        Set(ByVal value As Integer)
            _status = value
        End Set
    End Property

    Public Property ItemDefinition_Index() As String
        Get
            Return (Me._itemDefinition_Index)
        End Get
        Set(ByVal value As String)
            Me._itemDefinition_Index = value
        End Set
    End Property

    Public Property Invoice_No() As String
        Get
            Return _Invoice_No
        End Get
        Set(ByVal Value As String)
            _Invoice_No = Value
        End Set
    End Property

    Public Property ASN_No() As String
        Get
            Return _ASN_No
        End Get
        Set(ByVal Value As String)
            _ASN_No = Value
        End Set
    End Property

    Public Property PO_No() As String
        Get
            Return _PO_No
        End Get
        Set(ByVal Value As String)
            _PO_No = Value
        End Set
    End Property
    Public Property Is_SN() As Boolean
        Get
            Return _Is_SN
        End Get
        Set(ByVal value As Boolean)
            _Is_SN = value
        End Set
    End Property
    Public Property iRow_OrderItem_Index() As Integer
        Get
            Return _iRow_OrderItem_Index
        End Get
        Set(ByVal value As Integer)
            _iRow_OrderItem_Index = value
        End Set
    End Property



    Public Property Weight_Per_Pck() As Decimal
        Get
            Return (Me._Weight_Per_Pck)
        End Get
        Set(ByVal value As Decimal)
            Me._Weight_Per_Pck = value
        End Set
    End Property

    Public Property Volume_Per_Pck() As Decimal
        Get
            Return (Me._Volume_Per_Pck)
        End Get
        Set(ByVal value As Decimal)
            Me._Volume_Per_Pck = value
        End Set
    End Property

    Public Property Price_Per_Pck() As Decimal
        Get
            Return (Me._Price_Per_Pck)
        End Get
        Set(ByVal value As Decimal)
            Me._Price_Per_Pck = value
        End Set
    End Property

    Public Property Qty_Per_Pck() As Decimal
        Get
            Return (Me._Qty_Per_Pck)
        End Get
        Set(ByVal value As Decimal)
            Me._Qty_Per_Pck = value
        End Set
    End Property
    Public Property Item_Qty() As Decimal
        Get
            Return (Me._Item_Qty)
        End Get
        Set(ByVal value As Decimal)
            Me._Item_Qty = value
        End Set
    End Property

    Public Property Item_Package_Index() As String
        Get
            Return (Me._Item_Package_Index)
        End Get
        Set(ByVal value As String)
            Me._Item_Package_Index = value
        End Set
    End Property

    Public Property OrderItem_Price() As Decimal
        Get
            Return _OrderItem_Price
        End Get
        Set(ByVal Value As Decimal)
            _OrderItem_Price = Value
        End Set
    End Property
    Public Property Declaration_No() As String
        Get
            Return (Me._Declaration_No)
        End Get
        Set(ByVal value As String)
            Me._Declaration_No = value
        End Set
    End Property

    Public Property HandlingType_Index() As String
        Get
            Return (Me._HandlingType_Index)
        End Get
        Set(ByVal value As String)
            Me._HandlingType_Index = value
        End Set
    End Property
    Public Property Shipment_No() As String
        Get
            Return (Me._Shipment_No)
        End Get
        Set(ByVal value As String)
            Me._Shipment_No = value
        End Set
    End Property

#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Protected Overrides Sub Finalize()
        Dispose()
        MyBase.Finalize()
    End Sub
    Public Sub Dispose()
        'Class variable
        _dataTable = Nothing
        _scalarOutput = Nothing

        'Fields variable
    End Sub
    Public Sub Clear()
        _dataTable.Clear()
        _scalarOutput = ""

        'Fields variable 
    End Sub
#End Region


    '*** Normal DB Method
#Region " SELECT DATA "
    '***
    '*** Create By  : Paponshet ; [17/01/2008] ; ??s
    '*** Return : ??

    Public Sub searchByIndex(ByVal pstrIndex As String)
        Dim strSQL As String
        'strSQL = "SELECT     tb_OrderItem.*, ms_SKU.UnitWeight_Index AS UnitWeight"
        'strSQL &= " FROM         tb_OrderItem LEFT OUTER JOIN"
        'strSQL &= "            ms_SKU ON tb_OrderItem.Sku_Index = ms_SKU.Sku_Index"
        'strSQL &= " where     tb_OrderItem.sku_index ='" & pstrIndex & "'"

        strSQL = "SELECT     dbo.tb_OrderItem.*, dbo.ms_SKU.UnitWeight_Index AS UnitWeight, dbo.ms_Product.Product_Name_th AS Product_Name"
        strSQL &= "        FROM         dbo.ms_Product LEFT OUTER JOIN"
        strSQL &= "                    dbo.ms_SKU ON dbo.ms_Product.Product_Index = dbo.ms_SKU.Product_Index RIGHT OUTER JOIN"
        strSQL &= "                    dbo.tb_OrderItem ON dbo.ms_SKU.Sku_Index = dbo.tb_OrderItem.Sku_Index"
        strSQL &= " where     tb_OrderItem.sku_index ='" & pstrIndex & "'"
        Try
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



    Public Sub searchByOrderItem_Index(ByVal pstrIndex As String)
        Dim strSQL As String
        'strSQL = "SELECT     tb_OrderItem.*, ms_SKU.UnitWeight_Index AS UnitWeight"
        'strSQL &= " FROM         tb_OrderItem LEFT OUTER JOIN"
        'strSQL &= "            ms_SKU ON tb_OrderItem.Sku_Index = ms_SKU.Sku_Index"
        'strSQL &= " where     tb_OrderItem.sku_index ='" & pstrIndex & "'"

        strSQL = "SELECT     dbo.tb_OrderItem.*, dbo.ms_SKU.UnitWeight_Index AS UnitWeight, dbo.ms_Product.Product_Name_th AS Product_Name"
        strSQL &= "   		,Qty_Bal = isnull((tb_OrderItem.Qty - isnull((select sum(tb_Tag.Qty_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index ),0)),0)"
        strSQL &= "   		,Weight_Bal = isnull((tb_OrderItem.Weight - isnull((select sum(tb_Tag.Weight_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index ),0)),0)"
        strSQL &= "   		,Volume_Bal = isnull((tb_OrderItem.Volume - isnull((select sum(tb_Tag.Volume_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index ),0)),0)"
        strSQL &= "        FROM         dbo.ms_Product LEFT OUTER JOIN"
        strSQL &= "                    dbo.ms_SKU ON dbo.ms_Product.Product_Index = dbo.ms_SKU.Product_Index RIGHT OUTER JOIN"
        strSQL &= "                    dbo.tb_OrderItem ON dbo.ms_SKU.Sku_Index = dbo.tb_OrderItem.Sku_Index"
        strSQL &= " where     tb_OrderItem.OrderItem_Index ='" & pstrIndex & "'"
        Try
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

    Public Sub SearchData_Click(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then
                ' Select No Condition 
                strSQL = " SELECT     *   " & _
                         " FROM       tb_OrderItem "

                strWhere = pFilterValue
            Else
                ' Sql for define ColumnName & Filter Value 

                strSQL = " SELECT     " & ColumnName & _
                         " FROM       tb_OrderItem "

                strWhere = pFilterValue
            End If

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

    Public Sub GetOrderReadyCreteTag(ByVal pstrOrder_index As String)
        Dim strSQL As String = ""
        'strSQL &= "   SELECT  dbo.tb_OrderItem.*,dbo.ms_SKU.Sku_ID, dbo.ms_SKU.UnitWeight_Index AS UnitWeight, dbo.ms_Product.Product_Name_th AS Product_Name"
        'strSQL &= "   FROM dbo.tb_order left join  dbo.tb_OrderItem  on  dbo.tb_order.order_index = dbo.tb_OrderItem.order_index"
        'strSQL &= "  LEFT OUTER JOIN  dbo.ms_SKU  ON dbo.ms_SKU.Sku_Index = dbo.tb_OrderItem.Sku_Index"
        'strSQL &= "  LEFT OUTER JOIN  dbo.ms_Product ON dbo.ms_Product.Product_Index = dbo.ms_SKU.Product_Index "
        'strSQL &= "  where dbo.tb_OrderItem.orderItem_index not in (select orderItem_index from tb_tag where tag_status <> -1)"
        'strSQL &= "  and dbo.tb_OrderItem.orderItem_index not in (select orderItem_index from tb_locationBalance )"
        'strSQL &= " AND dbo.tb_OrderItem.Order_Index = '" & pstrOrderItem_index & "'"
        strSQL &= "   SELECT  dbo.tb_OrderItem.*,dbo.ms_SKU.Sku_ID, dbo.ms_SKU.UnitWeight_Index AS UnitWeight, dbo.ms_Product.Product_Name_th AS Product_Name"
        strSQL &= "   		,Qty_Bal =      isnull((tb_OrderItem.Qty - isnull((select sum(tb_Tag.Qty_Per_Tag)       FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index  and tb_Tag.Process_Id = 1 ),0)),0)"
        strSQL &= "   		,Weight_Bal =   isnull((tb_OrderItem.Weight - isnull((select sum(tb_Tag.Weight_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index  and tb_Tag.Process_Id = 1 ),0)),0)"
        strSQL &= "   		,Volume_Bal =   isnull((tb_OrderItem.Volume - isnull((select sum(tb_Tag.Volume_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index  and tb_Tag.Process_Id = 1 ),0)),0)"

        strSQL &= "   FROM	dbo.tb_order left join  "
        strSQL &= "   		dbo.tb_OrderItem  on  dbo.tb_order.order_index = dbo.tb_OrderItem.order_index LEFT OUTER JOIN  "
        strSQL &= "   		dbo.ms_SKU  ON dbo.ms_SKU.Sku_Index = dbo.tb_OrderItem.Sku_Index LEFT OUTER JOIN  "
        strSQL &= "   		dbo.ms_Product ON dbo.ms_Product.Product_Index = dbo.ms_SKU.Product_Index "
        strSQL &= "   WHERE     dbo.tb_OrderItem.Order_Index = '" & pstrOrder_index & "'"
        strSQL &= "   		AND isnull((tb_OrderItem.Qty - isnull((select sum(tb_Tag.Qty_Per_Tag) FROM tb_Tag WHERE tb_Tag.Tag_Status  not in (-1) AND tb_Tag.OrderItem_Index = tb_OrderItem.OrderItem_Index   and tb_Tag.Process_Id = 1 ),0)),0) > 0"

        Try
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

    Public Sub GetOrderReadyCreteTag_PopUp_Consignee(ByVal pstrOrderItem_index As String)
        Dim strSQL As String = ""
        'strSQL &= "   SELECT  dbo.tb_OrderItem.*,dbo.ms_SKU.Sku_ID, dbo.ms_SKU.UnitWeight_Index AS UnitWeight, dbo.ms_Product.Product_Name_th AS Product_Name"
        'strSQL &= "   FROM dbo.tb_order left join  dbo.tb_OrderItem  on  dbo.tb_order.order_index = dbo.tb_OrderItem.order_index"
        'strSQL &= "  LEFT OUTER JOIN  dbo.ms_SKU  ON dbo.ms_SKU.Sku_Index = dbo.tb_OrderItem.Sku_Index"
        'strSQL &= "  LEFT OUTER JOIN  dbo.ms_Product ON dbo.ms_Product.Product_Index = dbo.ms_SKU.Product_Index "
        'strSQL &= "  where dbo.tb_OrderItem.orderItem_index not in (select orderItem_index from tb_tag where tag_status <> -1)"
        'strSQL &= "  and dbo.tb_OrderItem.orderItem_index not in (select orderItem_index from tb_locationBalance )"
        'strSQL &= " AND dbo.tb_OrderItem.Order_Index = '" & pstrOrderItem_index & "'"
        strSQL &= " SELECT     dbo.tb_OrderItem.*, dbo.ms_SKU.Sku_Id AS Sku_Id, dbo.ms_SKU.UnitWeight_Index AS UnitWeight, "
        strSQL &= "    dbo.ms_Product.Product_Name_th AS Product_Name, dbo.ms_Customer_Shipping.Company_Name AS Company_Name, "
        strSQL &= " dbo.tb_OrderItem.Consignee_Index AS Consignee_Index"
        strSQL &= "  FROM         dbo.ms_SKU RIGHT OUTER JOIN"
        strSQL &= "          dbo.tb_OrderItem LEFT OUTER JOIN"
        strSQL &= "      dbo.ms_Customer_Shipping ON dbo.tb_OrderItem.Consignee_Index = dbo.ms_Customer_Shipping.Customer_Shipping_Index RIGHT OUTER JOIN"
        strSQL &= "      dbo.tb_Order ON dbo.tb_OrderItem.Order_Index = dbo.tb_Order.Order_Index ON "
        strSQL &= "    dbo.ms_SKU.Sku_Index = dbo.tb_OrderItem.Sku_Index LEFT OUTER JOIN"
        strSQL &= "     dbo.ms_Product ON dbo.ms_SKU.Product_Index = dbo.ms_Product.Product_Index"
        strSQL &= "  WHERE     (dbo.tb_OrderItem.OrderItem_Index NOT IN"
        strSQL &= "                   (SELECT     orderItem_index "
        strSQL &= " FROM tb_TAG "
        strSQL &= "              WHERE      tag_status <> - 1)) AND (dbo.tb_OrderItem.OrderItem_Index NOT IN"
        strSQL &= "            (SELECT     orderItem_index"
        strSQL &= "         FROM          tb_locationBalance))"
        strSQL &= " AND dbo.tb_OrderItem.Order_Index = '" & pstrOrderItem_index & "'"

        Try
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

    Public Function getForOrderItemSerial(ByVal OrderItem_Index As String) As Boolean
        Dim strSQL As New System.Text.StringBuilder
        Try
            strSQL.Append(" select tb_OrderItem.Sku_index,tb_OrderItem.OrderItem_index, ")
            strSQL.Append(" ms_sku.Sku_id,ms_sku.str1,tb_OrderItem.Total_Qty,tb_OrderItem.Status ")
            strSQL.Append(" from tb_OrderItem inner join ms_sku on tb_OrderItem.Sku_Index = ms_sku.Sku_Index ")
            strSQL.Append(" where OrderItem_index=@OrderItem_index ")

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
            _dataTable = DBExeQuery(strSQL.ToString)
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function getTotal_Qty(ByVal OrderItem_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL &= "select Total_Qty from tb_OrderItem where OrderItem_index=@OrderItem_index"
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@OrderItem_Index", SqlDbType.VarChar, 13).Value = OrderItem_Index
            _dataTable = DBExeQuery(strSQL)
            Return True
        Catch ex As Exception
            Throw ex
            Return False
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function

    Public Function CheckTotalQtyInPO(ByVal pTotalQty As Decimal, ByVal PurchaseOrderItem_Index As String) As String
        Dim StrSQL As New System.Text.StringBuilder
        Try

            StrSQL.Append(" SELECT Isnull(Total_Received_Qty,0) as Total_Received_Qty,Total_Qty FROM tb_PurchaseOrderItem ")
            StrSQL.Append(String.Format(" WHERE PurchaseOrderItem_Index = '{0}' ", PurchaseOrderItem_Index))
            Dim dtData As DataTable = DBExeQuery(StrSQL.ToString, eCommandType.Text)

            If dtData.Rows.Count <= 0 Then
                Return "PurchaseOrderItem_Index Data Not Found !!"
            End If

            If Not IsNumeric(dtData.Rows(0).Item("Total_Received_Qty")) Then
                Return "Total_Received_Qty Is Not Numeric !!"
            End If


            If (CDec(dtData.Rows(0).Item("Total_Received_Qty")) + pTotalQty) > (CDec(dtData.Rows(0).Item("Total_Qty"))) Then
                Return "ไม่สามารถรับสินค้า เกินจำนวนที่สั่งได้ !!"
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CheckTotalQtyInPO(ByVal pTotalQty As Decimal, ByVal PurchaseOrderItem_Index As String, ByVal OrderItem_Index As String) As String
        Dim StrSQL As New System.Text.StringBuilder
        Try

            StrSQL.Append(" SELECT Isnull(Total_Received_Qty,0) as Total_Received_Qty,Total_Qty FROM tb_PurchaseOrderItem ")
            StrSQL.Append(String.Format(" WHERE PurchaseOrderItem_Index = '{0}' ", PurchaseOrderItem_Index))
            Dim dtDataPurchaseOrder As DataTable = DBExeQuery(StrSQL.ToString, eCommandType.Text)

            StrSQL = New System.Text.StringBuilder

            StrSQL.Append(" SELECT Total_Qty FROM tb_OrderItem  ")
            StrSQL.Append(String.Format(" WHERE OrderItem_Index = '{0}' ", OrderItem_Index))
            Dim dtDataOrderItem As DataTable = DBExeQuery(StrSQL.ToString, eCommandType.Text)

            If dtDataPurchaseOrder.Rows.Count <= 0 Then
                Return "PurchaseOrderItem_Index Data Not Found !!"
            End If

            If dtDataOrderItem.Rows.Count <= 0 Then
                Return "OrderItem_Index Data Not Found !!"
            End If

            Dim TotalQty_R As Decimal = 0
            TotalQty_R = CDec(dtDataPurchaseOrder.Rows(0).Item("Total_Received_Qty")) - CDec(dtDataOrderItem.Rows(0).Item("Total_Qty"))


            If (TotalQty_R + pTotalQty) > CDec(dtDataPurchaseOrder.Rows(0).Item("Total_Qty")) Then
                Return "ไม่สามารถรับสินค้า เกินจำนวนที่สั่งได้ !!"
            Else
                Return ""
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function


#End Region
#Region " INSERT DATA "
    '***
    '*** Create By  : Paponshet ; [17/01/2008] ; ??s
    '*** Return : ??
#End Region
#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [17/01/2008] ; ??s
    '*** Return : ??
    
#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [17/01/2008] ; ??s
    '*** Return : ??
#End Region
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [17/01/2008] ; ??s
    '*** Return : ??
#End Region
#Region " REPORT DATA "
    '***
    '*** Create By  : Paponshet ; [17/01/2008] ; ??s
    '*** Return : ??
#End Region

    '*** Transaction DB Method
#Region " TRANSACTION "
#End Region
#Region " CONFIG FIELD "
    Public Function getFieldConfig() As DataTable
        Dim strSQL As String
        Try

            strSQL = " SELECT *   " & _
                     " FROM   config_OrderItem " & _
                     " WHERE IsUse=0 "

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

    Public Function getLanguageConfig() As DataTable
        Dim strSQL As String
        Try

            strSQL = " SELECT *   " & _
                     " FROM   config_OrderItem " & _
                     " WHERE IsUse=1 "

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

#End Region
End Class