Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Windows.Forms
Imports WMS_STD_Master_Datalayer

Imports System.Text
Imports System.IO
Imports System.Data.SqlClient
Imports System.Data.OleDb


Public Class Import_Master : Inherits DBType_SQLServer
    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

    Dim myReader As StreamReader
    Dim CurrentLine As String
    Dim strValue() As String
    Private _isItem_Package As Integer = 0
    Private ProductTypt_Index As String = ""
    Private _sku_Index As String
    Private _sku_Id As String
    Private _product_Index As String
    Private _size_Index As String
    Private _package_Index As String
    Private _unitWeight_Index As String
    Private _color_Index As String
    Private _model_Index As String
    Private _brand_Index As String
    Private _barcode1 As String
    Private _barcode2 As String
    Private _rFIDCode As String
    Private _price1 As Double
    Private _price2 As Double
    Private _price3 As Double
    Private _itemLife_y As Integer
    Private _itemLife_m As Integer
    Private _itemLife_d As Integer
    Private _str1 As String
    Private _str2 As String
    Private _str3 As String
    Private _str4 As String
    Private _str5 As String
    Private _str6 As String
    Private _str7 As String
    Private _str8 As String
    Private _str9 As String
    Private _str10 As String
    Private _flo1 As Double
    Private _flo2 As Double
    Private _flo3 As Double
    Private _flo4 As Double
    Private _flo5 As Double
    Private _min_Qty As Double
    Private _min_Weight As Double
    Private _min_Volume As Double
    Private _max_Qty As Double
    Private _max_Weight As Double
    Private _max_Volume As Double
    Private _add_by As String
    Private _add_date As Date
    Private _add_branch As Integer
    Private _update_by As String
    Private _update_date As Date
    Private _update_branch As Integer
    Private _Qty_Per_Pallet As Double

    Private _isPlot As Integer
    '---------------------------
    Public _ProductSku_Type As String = "0"
    Public _ProductName As String
    Public _Product_Type As String

    Private _Unit_Width As Double
    Private _Unit_Length As Double
    Private _Unit_Height As Double
    Private _Unit_Volume As Double
    Private _PalletType_Index As String
    Private _Pick_Method As Integer
    Private _Currency_Index1 As String
    Private _Currency_Index2 As String
    Private _Currency_Index3 As String
    Private _Customer_Index As String
    Private _Supplier_Index As String
    Private _Image_Path As String

    Private _Location_Index As String = ""
    Private _Item_Package_Index As String = ""
    Private _Picking_Type As Integer = 0

    Public grd As DataGridView


#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        IMPORT
        NULL
    End Enum
#End Region

#Region " Properties Section "
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
    Public Property Sku_Index() As String
        Get
            Return _sku_Index
        End Get
        Set(ByVal Value As String)

            _sku_Index = Value
        End Set
    End Property

    Public Property Sku_Id() As String
        Get
            Return _sku_Id
        End Get
        Set(ByVal Value As String)
            _sku_Id = Value
        End Set
    End Property
    Public Property Product_Index() As String
        Get
            Return _product_Index
        End Get
        Set(ByVal Value As String)
            _product_Index = Value
        End Set
    End Property

    Public Property Size_Index() As String
        Get
            Return _size_Index
        End Get
        Set(ByVal Value As String)
            _size_Index = Value
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
    Public Property UnitWeight_Index() As String
        Get
            Return _unitWeight_Index
        End Get
        Set(ByVal Value As String)
            _unitWeight_Index = Value
        End Set
    End Property
    Public Property Color_Index() As String
        Get
            Return _color_Index
        End Get
        Set(ByVal Value As String)
            _color_Index = Value
        End Set
    End Property
    Public Property Model_Index() As String
        Get
            Return _model_Index
        End Get
        Set(ByVal Value As String)
            _model_Index = Value
        End Set
    End Property
    Public Property Brand_Index() As String
        Get
            Return _brand_Index
        End Get
        Set(ByVal Value As String)
            _brand_Index = Value
        End Set
    End Property
    Public Property Barcode1() As String
        Get
            Return _barcode1
        End Get
        Set(ByVal Value As String)
            _barcode1 = Value
        End Set
    End Property
    Public Property Barcode2() As String
        Get
            Return _barcode2
        End Get
        Set(ByVal Value As String)
            _barcode2 = Value
        End Set
    End Property
    Public Property RFIDCode() As String
        Get
            Return _rFIDCode
        End Get
        Set(ByVal Value As String)
            _rFIDCode = Value
        End Set
    End Property
    Public Property Price1() As Double
        Get
            Return _price1
        End Get
        Set(ByVal Value As Double)
            _price1 = Value
        End Set
    End Property
    Public Property Price2() As Double
        Get
            Return _price2
        End Get
        Set(ByVal Value As Double)
            _price2 = Value
        End Set
    End Property
    Public Property Price3() As Double
        Get
            Return _price3
        End Get
        Set(ByVal Value As Double)
            _price3 = Value
        End Set
    End Property
    Public Property ItemLife_y() As Integer
        Get
            Return _itemLife_y
        End Get
        Set(ByVal Value As Integer)
            _itemLife_y = Value
        End Set
    End Property
    Public Property ItemLife_m() As Integer
        Get
            Return _itemLife_m
        End Get
        Set(ByVal Value As Integer)
            _itemLife_m = Value
        End Set
    End Property
    Public Property ItemLife_d() As Integer
        Get
            Return _itemLife_d
        End Get
        Set(ByVal Value As Integer)
            _itemLife_d = Value
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
    Public Property Flo1() As Double
        Get
            Return _flo1
        End Get
        Set(ByVal Value As Double)
            _flo1 = Value
        End Set
    End Property
    Public Property Flo2() As Double
        Get
            Return _flo2
        End Get
        Set(ByVal Value As Double)
            _flo2 = Value
        End Set
    End Property
    Public Property Flo3() As Double
        Get
            Return _flo3
        End Get
        Set(ByVal Value As Double)
            _flo3 = Value
        End Set
    End Property
    Public Property Flo4() As Double
        Get
            Return _flo4
        End Get
        Set(ByVal Value As Double)
            _flo4 = Value
        End Set
    End Property
    Public Property Flo5() As Double
        Get
            Return _flo5
        End Get
        Set(ByVal Value As Double)
            _flo5 = Value
        End Set
    End Property
    Public Property Min_Qty() As Double
        Get
            Return _min_Qty
        End Get
        Set(ByVal Value As Double)
            _min_Qty = Value
        End Set
    End Property
    Public Property Min_Weight() As Double
        Get
            Return _min_Weight
        End Get
        Set(ByVal Value As Double)
            _min_Weight = Value
        End Set
    End Property
    Public Property Min_Volume() As Double
        Get
            Return _min_Volume
        End Get
        Set(ByVal Value As Double)
            _min_Volume = Value
        End Set
    End Property
    Public Property Max_Qty() As Double
        Get
            Return _max_Qty
        End Get
        Set(ByVal Value As Double)
            _max_Qty = Value
        End Set
    End Property
    Public Property Max_Weight() As Double
        Get
            Return _max_Weight
        End Get
        Set(ByVal Value As Double)
            _max_Weight = Value
        End Set
    End Property
    Public Property Max_Volume() As Double
        Get
            Return _max_Volume
        End Get
        Set(ByVal Value As Double)
            _max_Volume = Value
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
    Public Property isPlot() As Integer
        Get
            Return _isPlot
        End Get
        Set(ByVal Value As Integer)
            _isPlot = Value
        End Set
    End Property


    Public Property Qty_Per_Pallet() As Double
        Get
            Return _Qty_Per_Pallet
        End Get
        Set(ByVal value As Double)
            _Qty_Per_Pallet = value
        End Set
    End Property

    Public Property Unit_Width() As Double
        Get
            Return _Unit_Width
        End Get
        Set(ByVal Value As Double)
            _Unit_Width = Value
        End Set
    End Property

    Public Property Unit_Length() As Double
        Get
            Return _Unit_Length
        End Get
        Set(ByVal Value As Double)
            _Unit_Length = Value
        End Set
    End Property

    Public Property Unit_Height() As Double
        Get
            Return _Unit_Height
        End Get
        Set(ByVal Value As Double)
            _Unit_Height = Value
        End Set
    End Property

    Public Property Unit_Volume() As Double
        Get
            Return _Unit_Volume
        End Get
        Set(ByVal Value As Double)
            _Unit_Volume = Value
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

    Public Property Pick_Method() As Integer
        Get
            Return _Pick_Method
        End Get
        Set(ByVal Value As Integer)
            _Pick_Method = Value
        End Set
    End Property
    Public Property Currency_Index1() As String
        Get
            Return _Currency_Index1
        End Get
        Set(ByVal Value As String)
            _Currency_Index1 = Value
        End Set
    End Property
    Public Property Currency_Index2() As String
        Get
            Return _Currency_Index2
        End Get
        Set(ByVal Value As String)
            _Currency_Index2 = Value
        End Set
    End Property
    Public Property Currency_Index3() As String
        Get
            Return _Currency_Index3
        End Get
        Set(ByVal Value As String)
            _Currency_Index3 = Value
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
    Public Property Image_Path() As String
        Get
            Return _Image_Path
        End Get
        Set(ByVal Value As String)
            _Image_Path = Value
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
    Public Property Location_Index() As String
        Get
            Return _Location_Index
        End Get
        Set(ByVal Value As String)
            _Location_Index = Value
        End Set
    End Property
    Public Property Picking_Type() As Integer
        Get
            Return _Picking_Type
        End Get
        Set(ByVal Value As Integer)
            _Picking_Type = Value
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

#Region " SELECT "

    Public Function IsExitData(ByVal pstrTableName As String, ByVal pstrFieldName As String, ByVal pstrFieldValue As String) As Boolean
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrFieldName & " FROM " & pstrTableName & " WHERE " & pstrFieldName & " = '" & pstrFieldValue.Trim & "'"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetPackage_Index(ByVal pstrSKU_Index As String, ByVal pstrPackage_ID As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT P.Package_Index "
            strSQL &= " FROM ms_Package P INNER JOIN ms_SKU S ON P.Package_Index = S.Package_Index"
            strSQL &= " 	INNER JOIN ms_SKURatio SR ON SR.SKU_index = S.SKU_Index AND P.Package_Index = SR.Package_Index"
            strSQL &= " WHERE S.SKU_index = '" & pstrSKU_Index & "' AND  P.Description = '" & pstrPackage_ID.Replace("'", "''") & "'"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)("Package_Index").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = '" & pstrField_ID_Value.Trim & "'"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)(pstrField_Index).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region " Insert "

    Public Function InsertSKU_fromImport() As Boolean
        Dim strSQL As String

        Try

            Dim objms_Product As New ms_Product(ms_Product.enuOperation_Type.ADDNEW)
            objms_Product.SaveData("", _sku_Id, _str1, _str2, _Product_Type, _package_Index, "0")
            _product_Index = objms_Product.objProduct_index
            _Product_Type = objms_Product.ProductType_Index

            strSQL = " INSERT INTO ms_SKU (SKU_Index,SKU_Id,product_Index,size_Index,package_Index,unitWeight_Index,color_Index,model_Index,brand_Index,barcode1,barcode2,price1,price2,price3,itemLife_y,itemLife_m,itemLife_d,str1,str2,str3,str4,str5,str10,min_Qty,min_Weight,min_Volume,max_Qty,max_Weight,max_Volume,Qty_Per_Pallet,add_by,add_date,add_branch,isPlot,Unit_Width,Unit_Length,Unit_Height,Unit_Volume,PalletType_Index,Pick_Method,Currency_Index1,Currency_Index2,Currency_Index3,Customer_Index,Supplier_Index,Image_Path,Picking_Type,Item_Package_Index,Location_Index) "
            strSQL &= " VALUES (@SKU_Index,@SKU_Id ,@product_Index,@size_Index,@package_Index,@unitWeight_Index,@color_Index,@model_Index,@brand_Index,@barcode1,@barcode2,@price1,@price2,@price3,@itemLife_y,@itemLife_m,@itemLife_d,@str1,@str2,@str3,@str4,@str5,@str10,@min_Qty,@min_Weight,@min_Volume,@max_Qty,@max_Weight,@max_Volume,@Qty_Per_Pallet,@add_by,getdate(),@add_branch,@isPlot,@Unit_Width,@Unit_Length,@Unit_Height,@Unit_Volume,@PalletType_Index,@Pick_Method,@Currency_Index1,@Currency_Index2,@Currency_Index3,@Customer_Index,@Supplier_Index,@Image_Path,@Picking_Type,@Item_Package_Index,@Location_Index)"
            strSQL = strSQL
            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@SKU_Index", SqlDbType.VarChar, 30).Value = Me._sku_Index
                .Parameters.Add("@SKU_Id", SqlDbType.VarChar, 50).Value = Me._sku_Id
                .Parameters.Add("@product_Index", SqlDbType.VarChar, 30).Value = _product_Index
                .Parameters.Add("@size_Index", SqlDbType.VarChar, 30).Value = Me._size_Index
                .Parameters.Add("@package_Index", SqlDbType.VarChar, 30).Value = Me._package_Index
                .Parameters.Add("@unitWeight_Index", SqlDbType.VarChar, 30).Value = Me._unitWeight_Index
                .Parameters.Add("@color_Index", SqlDbType.VarChar, 30).Value = Me._color_Index
                .Parameters.Add("@model_Index", SqlDbType.VarChar, 30).Value = Me._model_Index
                .Parameters.Add("@brand_Index", SqlDbType.VarChar, 30).Value = Me._brand_Index
                .Parameters.Add("@barcode1", SqlDbType.VarChar, 30).Value = Me._barcode1
                .Parameters.Add("@barcode2", SqlDbType.VarChar, 30).Value = Me._barcode2

                .Parameters.Add("@price1", SqlDbType.Float, 8).Value = _price1
                .Parameters.Add("@price2", SqlDbType.Float, 8).Value = _price2
                .Parameters.Add("@price3", SqlDbType.Float, 8).Value = _price3
                .Parameters.Add("@itemLife_y", SqlDbType.Int, 4).Value = _itemLife_y
                .Parameters.Add("@itemLife_m", SqlDbType.Int, 4).Value = _itemLife_m
                .Parameters.Add("@itemLife_d", SqlDbType.Int, 4).Value = _itemLife_d

                .Parameters.Add("@str1", SqlDbType.VarChar, 255).Value = _str1
                .Parameters.Add("@str2", SqlDbType.VarChar, 255).Value = _str2
                .Parameters.Add("@str3", SqlDbType.VarChar, 100).Value = _str3
                .Parameters.Add("@str4", SqlDbType.VarChar, 100).Value = _str4
                .Parameters.Add("@str5", SqlDbType.VarChar, 100).Value = _str5
                .Parameters.Add("@str10", SqlDbType.VarChar, 100).Value = _str10

                .Parameters.Add("@min_Qty", SqlDbType.Float, 8).Value = _min_Qty
                .Parameters.Add("@min_Weight", SqlDbType.Float, 8).Value = _min_Weight
                .Parameters.Add("@min_Volume", SqlDbType.Float, 8).Value = _min_Volume
                .Parameters.Add("@max_Qty", SqlDbType.Float, 8).Value = _max_Qty
                .Parameters.Add("@max_Weight", SqlDbType.Float, 8).Value = _max_Weight
                .Parameters.Add("@max_Volume", SqlDbType.Float, 8).Value = _max_Volume

                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float, 8).Value = Me._Qty_Per_Pallet
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int, 4).Value = WV_Branch_ID

                .Parameters.Add("@isPlot", SqlDbType.Bit, 4).Value = isPlot

                .Parameters.Add("@Currency_Index1", SqlDbType.VarChar, 13).Value = _Currency_Index1
                .Parameters.Add("@Currency_Index2", SqlDbType.VarChar, 13).Value = _Currency_Index2
                .Parameters.Add("@Currency_Index3", SqlDbType.VarChar, 13).Value = _Currency_Index3
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _Supplier_Index
                .Parameters.Add("@Image_Path", SqlDbType.VarChar, 13).Value = _Image_Path
                .Parameters.Add("@Picking_Type", SqlDbType.Int, 4).Value = Picking_Type

                .Parameters.Add("@Unit_Width", SqlDbType.Float, 15).Value = _Unit_Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float, 15).Value = _Unit_Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float, 15).Value = _Unit_Height
                .Parameters.Add("@Unit_Volume", SqlDbType.Float, 15).Value = _Unit_Volume
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _PalletType_Index
                .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _Item_Package_Index
                .Parameters.Add("@Pick_Method", SqlDbType.Int, 10).Value = _Pick_Method
                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = _Location_Index


            End With


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()



            Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
            objDB.SaveData("", _sku_Index, Me._package_Index, 1)

            Return True

        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function

#End Region

#Region " TRANSACTION "

    Public Function SaveData(ByVal _SKU_Index As String, ByVal _SKU_Id As String, ByVal _product_Index As String, ByVal _size_Index As String, ByVal _package_Index As String, ByVal _unitWeight_Index As String, ByVal _color_Index As String, ByVal _model_Index As String, ByVal _brand_Index As String, ByVal _barcode1 As String, ByVal _barcode2 As String, ByVal _price1 As Double, ByVal _price2 As Double, ByVal _price3 As Double, ByVal _itemLife_y As Integer, ByVal _itemLife_m As Integer, ByVal _itemLife_d As Integer, ByVal name_thai As String, ByVal name_eng As String, ByVal sku_des As String, ByVal _min_Qty As Double, ByVal _min_Weight As Double, ByVal _min_Volume As Double, ByVal _max_Qty As Double, ByVal _max_Weight As Double, ByVal _max_Volume As Double, ByVal _isPlot As Integer, ByVal Qty_Per_Pallet As Double, ByVal _VolumeX As String, ByVal _VolumeY As String, ByVal _VolumeZ As String, ByVal _Volume As String, ByVal _CustomerRefCode As String, ByVal _SupplierRefCode As String, ByVal CurrencyIndex1 As String, ByVal CurrencyIndex2 As String, ByVal CurrencyIndex3 As String, ByVal CustomerIndex As String, ByVal SupplierIndex As String, ByVal ImagePath As String, ByVal Item_Package_Index As String, ByVal Location_Index As String) As Boolean
        ' ***  define value to field ***
        '  Me._sku_Index = _SKU_Index

        Me._sku_Id = _SKU_Id
        Me._size_Index = _size_Index
        Me._package_Index = _package_Index
        Me._unitWeight_Index = _unitWeight_Index
        Me._color_Index = _color_Index
        Me._model_Index = _model_Index
        Me._brand_Index = _brand_Index
        Me._barcode1 = _barcode1
        Me._barcode2 = _barcode2
        Me._product_Index = _product_Index
        Me._price1 = _price1
        Me._price2 = _price2
        Me._price3 = _price3
        Me._itemLife_y = _itemLife_y
        Me._itemLife_m = _itemLife_m
        Me._itemLife_d = _itemLife_d
        Me._str1 = name_thai
        Me._str2 = name_eng
        Me._str3 = sku_des
        Me._str4 = _CustomerRefCode
        Me._str5 = _SupplierRefCode
        Me._min_Qty = _min_Qty
        Me._min_Weight = _min_Weight
        Me._min_Volume = _min_Volume
        Me._max_Qty = _max_Qty
        Me._max_Weight = _max_Weight
        Me._max_Volume = _max_Volume
        Me._Qty_Per_Pallet = Qty_Per_Pallet
        '   Me.grd = Gridview

        Me._Unit_Width = _VolumeX
        Me._Unit_Length = _VolumeY
        Me._Unit_Height = _VolumeZ
        Me._Unit_Volume = _Volume
        Me._PalletType_Index = 1
        Me._Pick_Method = 1

        Me._isPlot = _isPlot
        Me._Currency_Index1 = CurrencyIndex1
        Me._Currency_Index2 = CurrencyIndex2
        Me._Currency_Index3 = CurrencyIndex3
        Me._Customer_Index = CustomerIndex
        Me._Supplier_Index = SupplierIndex
        Me._Image_Path = ImagePath
        ' Me._Picking_Type = Picking_Type
        Me._Location_Index = Location_Index

        Me._Item_Package_Index = Item_Package_Index

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                Dim objDBIndex As New Sy_AutoNumber
                Me._sku_Index = objDBIndex.getSys_Value("SKU_Index")
                objDBIndex = Nothing

        End Select

        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    If isExistID(_SKU_Id) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        Exit Function

                    End If
                Case enuOperation_Type.IMPORT


            End Select

        Catch ex As Exception
            Return False
            Throw ex
        End Try


    End Function

#End Region

#Region " CHECK DATA "
    Private Function isExistID(ByVal _SKU_Id As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = " select count(*) from ms_SKU where SKU_Id = @SKU_Id AND status_id <> -1 "

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SKU_Id", SqlDbType.VarChar, 50).Value = _SKU_Id

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

#Region " Import Data"
    Public Sub ImportCumtomer(ByVal DEFAULT_IMPORT_PATH As String)

        Dim objCustomer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim odtCustomer As DataTable
        Try
            'set Pate  = Fix Code
            ' Path File & File name S2R = C:\Interface\Customer.txt

            Dim pstrFileName As String = ""
            pstrFileName = DEFAULT_IMPORT_PATH & "Customer.txt"

            myReader = New StreamReader(pstrFileName, System.Text.UnicodeEncoding.Default)

            While myReader.Peek <> -1
                CurrentLine = myReader.ReadLine
                strValue = CurrentLine.Split("|")
                'ReadLine = (No,name1,namr2,Address,Contact,Phone no.,Fax)

                'Check is Duplicate Customer no (Base)

                If strValue(0) = "" Then 'รหัส
                    Continue While
                End If

                Dim CusName As String = ""
                If strValue(1) <> "" Then
                    CusName = strValue(1).Trim
                Else
                    CusName = strValue(2).Trim
                End If

                objCustomer.getPopup_Search(" AND Customer_Name = '" & CusName & "'")
                odtCustomer = objCustomer.GetDataTable
                If odtCustomer.Rows.Count = 0 Then
                    '  New Customer
                    Dim objAutoNumber As New Sy_AutoNumber
                    With objCustomer
                        .Customer_Index = objAutoNumber.getSys_Value("Customer_Index")
                        .Customer_Id = strValue(0)
                        .Customer_Name = CusName
                        .Address = strValue(3)
                        .Contact_Person = strValue(4)
                        .Tel = strValue(5)
                        .Fax = strValue(6)
                        .Insert_Customer()
                    End With
                End If
            End While

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Public Sub ImportVender(ByVal DEFAULT_IMPORT_PATH As String)

    '    Dim objSupplier As New ms_Supplier(ms_Supplier.enuOperation_Type.SEARCH)
    '    Dim odtSupplier As DataTable
    '    Try


    '        Dim objAutoNumber As New Sy_AutoNumber
    '        Dim i As Integer = 0
    '        For i = 0 To grdve.Rows.Count - 1

    '            With objSupplier
    '                .Supplier_Index = objAutoNumber.getSys_Value("Supplier_Index")
    '                .Supplier_Id = objAutoNumber.getSys_Value("Supplier_ID")
    '            .Supplier_Name = 
    '                .Address = strValue(3)
    '                .Contact_Person = strValue(4)
    '                .Tel = strValue(5)
    '                .Fax = strValue(6)
    '                .Status = 1
    '                .Insert_Master()
    '            End With
    '        Next



    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Public Sub ImportSKU(ByVal DEFAULT_IMPORT_PATH As String)

        Try
            Dim objSKU As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim package_Id As String = ""
            Dim description As String = ""
            Dim dimension_Hi As Double = 0.0
            Dim dimension_Wd As Double = 0.0
            Dim dimension_Len As Double = 0.0

            Dim bitPlot As Integer = 0

            Dim strCustomerIndex As String = ""
            Dim strSupplierIndex As String = ""
            Dim strSize As String = ""
            Dim strColor As String = ""
            Dim strModel As String = ""
            Dim strBrand As String = ""
            Dim strMainPackage As String = ""

            Dim strCurrency1 As String = ""
            Dim strCurrency2 As String = ""
            Dim strCurrency3 As String = ""

            strCustomerIndex = ""
            strSupplierIndex = ""
            strSize = ""
            strColor = ""
            strModel = ""
            strBrand = ""
            strMainPackage = Me._package_Index
            strCurrency1 = ""
            strCurrency2 = ""
            strCurrency3 = ""
            Dim pstrFileName As String = ""

            pstrFileName = DEFAULT_IMPORT_PATH & "Item.txt"

            myReader = New StreamReader(pstrFileName, System.Text.UnicodeEncoding.Default)
            'หมายเหตุ,รหัสสินค้า,รหัสสินค้า 2 .ชื่อสินค้า,ชื่อสินค้า 2,จำนวนหน่วยเก็บ,หน่วยจัดเก็บ,จำนวนหน่วยซื้อ,หน่วยที่สั่งซื้อ,ประเภทสินค้า,กลุ่มสินค้า,ผู้จำหน่ายที่ดูแลสินค้า,จำนวนสั่งซื้อขั้นต่ำ,รหัส Barcode Vendor,price1
            While myReader.Peek <> -1
                CurrentLine = myReader.ReadLine
                strValue = CurrentLine.Split("|")
                If strValue(1).Trim = "" Then
                    Continue While
                End If
                strValue = CurrentLine.Split("|")
                Dim objProductType As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)

                objProductType.getProductType(" AND ProductType_Id = '" & strValue(9).Trim & "'")
                Dim odtProductTypt As DataTable = objProductType.GetDataTable

                If odtProductTypt.Rows.Count = 0 Then
                    'new ProductType
                    Dim objAutoNumber As New Sy_AutoNumber
                    Dim objProductTypeSave As New ms_ProductType(ms_ProductType.enuOperation_Type.ADDNEW)
                    With objProductTypeSave
                        ProductTypt_Index = objAutoNumber.getSys_Value("ProductType_Index")
                        .ProductType_Index = ProductTypt_Index
                        .ProductType_Id = strValue(9).Trim
                        .Description = strValue(9).Trim
                        .SaveData(.ProductType_Index, .ProductType_Id, .Description, 0, 0, 0)
                    End With
                Else
                    Continue While
                    ProductTypt_Index = odtProductTypt.Rows(0).Item("ProductType_Index")
                End If

                ' Insert Package

                package_Id = strValue(6).Trim
                description = strValue(6).Trim
                Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)


                Me._package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, _isItem_Package, 0, "") ', txtUnit_id.Text
                strMainPackage = Me._package_Index
                Item_Package_Index = Me._package_Index

                ' Save SKU

                Dim objImportData As New Import_Master(Import_Master.enuOperation_Type.ADDNEW)
                Dim blnSaveResult As Boolean = False
                With objImportData
                    ._ProductSku_Type = "1"
                    ._Product_Type = ProductTypt_Index
                    ._ProductName = ""

                    .Str10 = "" 'Group
                    .Str4 = "" 'Customer
                    .Str5 = "" '_Supplier

                    .Item_Package_Index = strMainPackage
                    .Min_Qty = 0


                    ' .ItemLife_y = 10
                    blnSaveResult = .SaveData(Me._sku_Index, strValue(1).Trim, _product_Index, strSize, strMainPackage, 0, strColor, strModel, strBrand, "", "", 0, 5, 0, 0, 0, 0, strValue(3).Trim, strValue(4).Trim, strValue(0).Trim, .Min_Qty, 0, 0, 0, 0, 0, bitPlot, 0, 0, 0, 0, 0, "", "", strCurrency1, strCurrency2, strCurrency3, strCustomerIndex, strSupplierIndex, "", Item_Package_Index, "")

                    blnSaveResult = .InsertSKU_fromImport()
                    _sku_Index = objImportData.Sku_Index


                End With
            End While


            'For i As Integer = 0 To grdPreviewData.Rows.Count - 1




            'Next
            'W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

End Class
