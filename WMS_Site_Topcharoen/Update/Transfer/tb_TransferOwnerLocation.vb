Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Public Class tb_TransferOwnerLocation : Inherits DBType_SQLServer

#Region " Private variables "

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""
    Private _TransferOwnerLocation_Index As String = ""
    Private _TransferOwner_Index As String = ""
    Private _Old_Sku_Index As String = ""
    Private _New_Sku_Index As String = ""
    Private _Order_Index As String = ""
    Private _OrderItem_Index As String = ""
    Private _Lot_No As String = ""
    Private _Plot As String = ""
    Private _Old_ItemStatus_Index As String = ""
    Private _New_ItemStatus_Index As String = ""
    Private _Serial_No As String = ""
    Private _Tag_No As String = ""
    Private _Total_Qty As Decimal
    Private _Weight As Decimal
    Private _Volume As Decimal
    Private _From_Location_Index As String = ""
    Private _To_Location_Index As String = ""
    Private _PalletType_Index As String = ""
    Private _Pallet_Qty As Decimal
    Private _Str1 As String = ""
    Private _Str2 As String = ""
    Private _Str3 As String = ""
    Private _Str4 As String = ""
    Private _Str5 As String = ""
    Private _Flo1 As Decimal = 0
    Private _Flo2 As Decimal = 0
    Private _Flo3 As Decimal = 0
    Private _Flo4 As Decimal = 0
    Private _Flo5 As Decimal = 0
    Private _Status As Integer
    Private _add_by As String = ""
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String = ""
    Private _update_date As Date
    Private _update_branch As Integer

    Private _AssetLocationBalance_Index As String = ""
    Private _Mfg_date As Date
    Private _Exp_date As Date
    Private _Pallet_No As String = ""
    Private _Asset_No As String = ""

    '06/05/2009 
    Private _From_LocationBalance_Index As String = ""
    Private _To_LocationBalance_Index As String = ""

    Private _Item_Qty As Decimal = 0
    Private _Price As Decimal = 0
    Private _Package_Index As String = ""
    Private _Item_Package_Index As String = ""
    Private _New_Package_Index As String = ""


    Private _TAG_Index As String = ""

    '2013/10/04
    Private _ERP_LOCATION As String = ""


#End Region

#Region " Properties "
    Public Property TAG_Index() As String
        Get
            Return _TAG_Index
        End Get
        Set(ByVal value As String)
            _TAG_Index = value
        End Set
    End Property
    Private _new_Plot As String = ""
    Public Property new_Plot() As String
        Get
            Return _new_Plot
        End Get
        Set(ByVal value As String)
            _new_Plot = value
        End Set
    End Property

    Private _DocumentPlan_Index As String = ""
    Public Property DocumentPlan_Index() As String
        Get
            Return _DocumentPlan_Index
        End Get
        Set(ByVal value As String)
            _DocumentPlan_Index = value
        End Set
    End Property


    Private _DocumentPlanItem_Index As String = ""
    Public Property DocumentPlanItem_Index() As String
        Get
            Return _DocumentPlanItem_Index
        End Get
        Set(ByVal value As String)
            _DocumentPlanItem_Index = value
        End Set
    End Property

    Private _DocumentPlan_No As String = ""
    Public Property DocumentPlan_No() As String
        Get
            Return _DocumentPlan_No
        End Get
        Set(ByVal value As String)
            _DocumentPlan_No = value
        End Set
    End Property

    Private _Plan_Process As Integer = -9
    Public Property Plan_Process() As Integer
        Get
            Return _Plan_Process
        End Get
        Set(ByVal value As Integer)
            _Plan_Process = value
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
    Public Property TransferOwnerLocation_Index() As String
        Get
            Return _TransferOwnerLocation_Index
        End Get
        Set(ByVal Value As String)
            _TransferOwnerLocation_Index = Value
        End Set
    End Property

    Public Property TransferOwner_Index() As String
        Get
            Return _TransferOwner_Index
        End Get
        Set(ByVal Value As String)
            _TransferOwner_Index = Value
        End Set
    End Property

    Public Property Old_Sku_Index() As String
        Get
            Return _Old_Sku_Index
        End Get
        Set(ByVal Value As String)
            _Old_Sku_Index = Value
        End Set
    End Property

    Public Property New_Sku_Index() As String
        Get
            Return _New_Sku_Index
        End Get
        Set(ByVal Value As String)
            _New_Sku_Index = Value
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

    Public Property OrderItem_Index() As String
        Get
            Return _OrderItem_Index
        End Get
        Set(ByVal Value As String)
            _OrderItem_Index = Value
        End Set
    End Property

    Public Property Lot_No() As String
        Get
            Return _Lot_No
        End Get
        Set(ByVal Value As String)
            _Lot_No = Value
        End Set
    End Property

    Public Property Plot() As String
        Get
            Return _Plot
        End Get
        Set(ByVal Value As String)
            _Plot = Value
        End Set
    End Property

    Public Property Old_ItemStatus_Index() As String
        Get
            Return _Old_ItemStatus_Index
        End Get
        Set(ByVal Value As String)
            _Old_ItemStatus_Index = Value
        End Set
    End Property

    Public Property New_ItemStatus_Index() As String
        Get
            Return _New_ItemStatus_Index
        End Get
        Set(ByVal Value As String)
            _New_ItemStatus_Index = Value
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

    Public Property Tag_No() As String
        Get
            Return _Tag_No
        End Get
        Set(ByVal Value As String)
            _Tag_No = Value
        End Set
    End Property

    Public Property Total_Qty() As Decimal
        Get
            Return _Total_Qty
        End Get
        Set(ByVal Value As Decimal)
            _Total_Qty = Value
        End Set
    End Property

    Public Property Weight() As Decimal
        Get
            Return _Weight
        End Get
        Set(ByVal Value As Decimal)
            _Weight = Value
        End Set
    End Property

    Public Property Volume() As Decimal
        Get
            Return _Volume
        End Get
        Set(ByVal Value As Decimal)
            _Volume = Value
        End Set
    End Property

    Public Property From_Location_Index() As String
        Get
            Return _From_Location_Index
        End Get
        Set(ByVal Value As String)
            _From_Location_Index = Value
        End Set
    End Property

    Public Property To_Location_Index() As String
        Get
            Return _To_Location_Index
        End Get
        Set(ByVal Value As String)
            _To_Location_Index = Value
        End Set
    End Property

    Public Property PalletType_Index() As String
        Get
            Return _PalletType_Index
        End Get
        Set(ByVal Value As String)
            _PalletType_Index = Value
        End Set
    End Property

    Public Property Pallet_Qty() As Decimal
        Get
            Return _Pallet_Qty
        End Get
        Set(ByVal Value As Decimal)
            _Pallet_Qty = Value
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

    Public Property Flo1() As Decimal
        Get
            Return _Flo1
        End Get
        Set(ByVal Value As Decimal)
            _Flo1 = Value
        End Set
    End Property

    Public Property Flo2() As Decimal
        Get
            Return _Flo2
        End Get
        Set(ByVal Value As Decimal)
            _Flo2 = Value
        End Set
    End Property

    Public Property Flo3() As Decimal
        Get
            Return _Flo3
        End Get
        Set(ByVal Value As Decimal)
            _Flo3 = Value
        End Set
    End Property

    Public Property Flo4() As Decimal
        Get
            Return _Flo4
        End Get
        Set(ByVal Value As Decimal)
            _Flo4 = Value
        End Set
    End Property

    Public Property Flo5() As Decimal
        Get
            Return _Flo5
        End Get
        Set(ByVal Value As Decimal)
            _Flo5 = Value
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


    Public Property AssetLocationBalance_Index() As String
        Get
            Return _AssetLocationBalance_Index
        End Get
        Set(ByVal value As String)
            _AssetLocationBalance_Index = value
        End Set
    End Property

    Public Property MfgDate() As Date
        Get
            Return _Mfg_date
        End Get
        Set(ByVal Value As Date)
            _Mfg_date = Value
        End Set
    End Property

    Public Property ExpDate() As Date
        Get
            Return _Exp_date
        End Get
        Set(ByVal Value As Date)
            _Exp_date = Value
        End Set
    End Property

    Public Property Pallet_No() As String
        Get
            Return _Pallet_No
        End Get
        Set(ByVal value As String)
            _Pallet_No = value
        End Set
    End Property

    Public Property Asset_No() As String
        Get
            Return _Asset_No
        End Get
        Set(ByVal value As String)
            _Asset_No = value
        End Set
    End Property

    '06/05/2009
    Public Property From_LocationBalance_Index() As String
        Get
            Return _From_LocationBalance_Index
        End Get
        Set(ByVal value As String)
            _From_LocationBalance_Index = value
        End Set
    End Property

    Public Property To_LocationBalance_Index() As String
        Get
            Return _To_LocationBalance_Index
        End Get
        Set(ByVal value As String)
            _To_LocationBalance_Index = value
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

    Public Property Item_Package_Index() As String
        Get
            Return _Item_Package_Index
        End Get
        Set(ByVal Value As String)
            _Item_Package_Index = Value
        End Set
    End Property
    Public Property Item_Qty() As Decimal
        Get
            Return _Item_Qty
        End Get
        Set(ByVal Value As Decimal)
            _Item_Qty = Value
        End Set
    End Property
    Public Property Price() As Decimal
        Get
            Return _Price
        End Get
        Set(ByVal Value As Decimal)
            _Price = Value
        End Set
    End Property
    Public Property New_Package_Index() As String
        Get
            Return _New_Package_Index
        End Get
        Set(ByVal Value As String)
            _New_Package_Index = Value
        End Set
    End Property

    '2013/10/04

    Public Property ERP_LOCATION() As String
        Get
            Return _ERP_LOCATION
        End Get
        Set(ByVal Value As String)
            _ERP_LOCATION = Value
        End Set
    End Property


    Private _IsMfg_Date As Boolean
    Public Property IsMfg_Date() As Boolean
        Get
            Return _IsMfg_Date
        End Get
        Set(ByVal value As Boolean)
            _IsMfg_Date = value
        End Set
    End Property
    Private _IsExp_Date As Boolean
    Public Property IsExp_Date() As Boolean
        Get
            Return _IsExp_Date
        End Get
        Set(ByVal value As Boolean)
            _IsExp_Date = value
        End Set
    End Property
    Private _Ratio As Decimal = 1

    Public Property Ratio() As Decimal
        Get
            Return _Ratio
        End Get
        Set(ByVal value As Decimal)
            _Ratio = value
        End Set
    End Property

    Private _Qty As Decimal = 0
    Public Property Qty() As Decimal
        Get
            Return _Qty
        End Get
        Set(ByVal value As Decimal)
            _Qty = value
        End Set
    End Property


    '
#End Region

#Region " SELECT DATA "

#End Region

#Region " INSERT DATA "

#End Region

#Region " UPDATE DATA "

#End Region

#Region " DELETE DATA "

#End Region

End Class