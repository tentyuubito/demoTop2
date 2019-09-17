'*** Create Date :  17/01/2008
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer

'Imports System.Windows.Forms

Public Class ms_SKU_Update : Inherits DBType_SQLServer

    '*** Fileds
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _sku_Index As String
    Private _sku_Id As String = ""
    Private _product_Index As String

    Private _productClass As String
    Private _productSubClass As String

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
    Private _isControlAsset As Integer
    Private _isControlPack As Integer
    Private _PrintBarcode As Integer
    Private _PackingType As Integer
    '---------------------------
    Public _ProductSku_Type As String = "0"
    Public _ProductName As String
    Public _Product_Type As String

    Private _Unit_Width As Double
    Private _Unit_Length As Double
    Private _Unit_Height As Double
    Private _Unit_Volume As Double
    Private _PalletType_Index As String = ""
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

    Public dtDataSoruce_Sku As New DataTable 'DataGridView

    Private _MessageError As String = ""


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

    Public ReadOnly Property MessageError() As String
        Get
            Return _MessageError
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

    Public Property ProductClass() As String
        Get
            Return _productClass
        End Get
        Set(ByVal Value As String)

            _productClass = Value
        End Set
    End Property
    Public Property ProductSubClass() As String
        Get
            Return _productSubClass
        End Get
        Set(ByVal Value As String)

            _productSubClass = Value
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
    Public Property isControlAsset() As Integer
        Get
            Return _isControlAsset
        End Get
        Set(ByVal Value As Integer)
            _isControlAsset = Value
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

    '*** Normal DB Method
#Region " SELECT DATA  & SEARCH"

    ''' <summary>
    ''' get package by barcode1 Or barcode package
    ''' 
    ''' if TypeBarcode = true (barcode package)
    ''' </summary>
    ''' <param name="pstrBarcode"></param>
    ''' <param name="TypeBarcode"></param>
    ''' <remarks>
    ''' Create By Hun
    ''' Date : 2010 03 22
    '''</remarks>
    Public Sub getPackage_ByBarcode(ByVal pstrBarcode As String, ByVal TypeBarcode As Boolean)

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT  * "
            strSQL &= " FROM ms_SKURatio INNER JOIN ms_Package ON "
            strSQL &= " ms_SKURatio.Package_Index = ms_Package.Package_Index "
            strSQL &= " INNER JOIN ms_Sku ON ms_Sku.Sku_Index = ms_SkuRatio.Sku_Index "

            If TypeBarcode = True Then

                strSQL &= " WHERE ms_Package.Barcode='" & pstrBarcode & "'"

            Else

                strSQL &= " WHERE ms_Sku.Barcode1='" & pstrBarcode & "'"

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

    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function getSKU_Index(ByVal Sku_Id As String, Optional ByVal strWhere As String = "") As String

        Dim strSQL As String
        Try
            strSQL = " select Sku_Index from ms_SKU where Sku_Id = @Sku_Id  "

            If strWhere <> "" Then
                strSQL &= strWhere
            End If


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Sku_Id", SqlDbType.NVarChar, 100).Value = Sku_Id

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput Is Nothing Then 'Or _scalarOutput.Trim = "" Then
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
    Public Function getSKU_Id_length() As String

        Dim strSQL As String
        Try
            strSQL = " select character_maximum_length "
            strSQL &= "  from information_schema.columns  "
            strSQL &= "  where table_name = 'ms_sku' and column_name='sku_id'  "

            SQLServerCommand.Parameters.Clear()
            'SQLServerCommand.Parameters.Add("@Sku_Id", SqlDbType.NVarChar, 100).Value = Sku_Id

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput Is Nothing Then 'Or _scalarOutput.Trim = "" Then
                Return "0"
            Else
                Return _scalarOutput
            End If


        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Sub SearchData_ByIndex(ByVal SkuIndex As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            strSQL = "SELECT  * from ms_sku where sku_index = '" & SkuIndex & "'"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getSKU_Package1(ByVal psku_Id As String)

        ' *** define value ***
        Me._sku_Id = Sku_Index


        Dim strSQL As String = ""
        Try


            strSQL = " SELECT     dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_SKU.Package_Index, dbo.ms_SKU.Str1, dbo.ms_Package.Description as Package,dbo.ms_SKU.UnitWeight_Index   FROM    dbo.ms_Package INNER JOIN dbo.ms_SKU ON dbo.ms_Package.Package_Index = dbo.ms_SKU.Package_Index "
            strSQL &= "  WHERE dbo.ms_SKU.Sku_Id ='" & psku_Id & "' And ms_SKU.status_id  not in (-1)  "

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

    Public Sub getSKU(ByVal Sku_Index As String)

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT  Sku_Id,Sku_Index"
            strSQL &= "  FROM       ms_SKU"

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
    Public Sub GetSku_Config(ByVal ZoneIndex As String, Optional ByVal strCondition As String = "")
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT Sku_Index,Sku_Id,Str1 "
            strSQL &= " FROM ms_Sku "
            strSQL &= " WHERE status_id != -1 "
            If ZoneIndex <> "" Then
                strSQL &= " AND Sku_Index not in (select Sku_Index from tb_Zone_SKU WHERE Zone_Index = '" & ZoneIndex & "')"
            Else
                strSQL &= " AND Sku_Index not in (select Sku_Index from tb_Zone_SKU)"
            End If

            SetSQLString = strSQL & strCondition & " Order by Sku_Id"

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getAllSKUDetail()

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT  *"
            strSQL &= "  FROM       ms_SKU WHERE status_id <> -1 "

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

    Public Sub getSKU_PackageItem(ByVal Sku_Index As String)

        ' *** define value ***
        Me._sku_Index = Sku_Index


        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT   Package_Index, Description AS Package "
            strSQL &= " FROM     ms_Package"
            strSQL &= " WHERE status_id <> -1"

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

    Public Sub getSKU_Description(ByVal Sku_Id As String)

        ' *** define value ***
        Me._sku_Id = Sku_Id

        Dim strSQL As String = ""
        Try

            strSQL = " SELECT     dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_SKU.Package_Index, dbo.ms_SKU.Str1, dbo.ms_Package.Description as Package ,dbo.ms_SKU.UnitWeight_Index FROM  dbo.ms_Package INNER JOIN dbo.ms_SKU ON dbo.ms_Package.Package_Index = dbo.ms_SKU.Package_Index "
            strSQL &= "  WHERE dbo.ms_SKU.Sku_Id ='" & Me._sku_Id & "' And ms_SKU.status_id  not in (-1)  "

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

    Public Sub SearchColume_ByIndex(ByVal SkuIndex As String, ByVal Colume As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            strSQL = "SELECT  " & Colume & " from ms_sku where sku_index = '" & SkuIndex & "'"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchData_Click(ByVal ColumnName As String, ByVal pFilterValue As String, Optional ByVal intTop As Integer = 0)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then

                If intTop = 0 Then
                    strSQL = "SELECT  "
                Else
                    strSQL = String.Format("SELECT  top {0} ", intTop)
                End If

                strSQL &= " *, ms_SKU.*, ms_Color.Description AS Expr1, ms_Model.Description AS Expr2, ms_Size.Description AS Expr3, ms_Brand.Description AS Expr4, "
                strSQL &= "            ms_Package.Description AS Expr5, ms_Product.Product_Name_th AS Expr6, ms_Product.Product_Name_en AS Expr7,ms_Product.Product_Id AS Product_id "
                strSQL &= "FROM         ms_SKU Left JOIN "
                strSQL &= "ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index Left JOIN "
                strSQL &= "ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index Left JOIN "
                strSQL &= "ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index Left JOIN "
                strSQL &= "ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index Left JOIN "
                strSQL &= "ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index Left JOIN "
                strSQL &= "ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index"
                strSQL &= " where ms_SKU.status_id != -1 "

                If pFilterValue <> "" Then
                    strSQL &= pFilterValue
                End If

            Else
                ' Sql for define ColumnName & Filter Value 

            End If

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchData_Row(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" Then

                strSQL = "select count(*) as row from ms_sku"

                If pFilterValue <> "" Then
                    strSQL &= pFilterValue
                End If

            Else
            End If

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchData_Sku(ByVal ColumnName As String, ByVal pFilterValue As String, ByVal RowStart As Integer, ByVal rowEnd As Integer)
        Dim strSQL As String = ""
        Dim strWhere As String = ""

        Try

            If ColumnName = "" Then

                strSQL = " declare @Product_index NVARCHAR(50);"
                strSQL &= " declare @SKU_Index NVARCHAR(50);"
                strSQL &= " declare @Product_ID NVARCHAR(50); "
                strSQL &= " declare @ROW_START int; "
                strSQL &= " declare @ROW_END int; "
                strSQL &= " set @ROW_START = '" & RowStart & "' "
                strSQL &= " set @ROW_END =  '" & rowEnd & "'"
                strSQL &= " set rowcount @ROW_START "

                strSQL &= "   SELECT   @SKU_Index = ms_SKU.SKU_index   "
                strSQL &= "       FROM        ms_SKU Left JOIN"
                strSQL &= "    ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index Left JOIN"
                strSQL &= "    ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index Left JOIN"
                strSQL &= "    ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index Left JOIN"
                strSQL &= "    ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index Left JOIN"
                strSQL &= "    ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index Left JOIN"
                strSQL &= "    ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index LEFT JOIN"
                strSQL &= "    tb_PackingBom ON ms_SKU.Sku_Index = tb_PackingBom.Sku_Index  "
                strSQL &= "   where ms_SKU.status_id != -1 "

                If pFilterValue <> "" Then
                    strSQL &= pFilterValue
                End If
                strSQL &= "   order by ms_SKU.SKU_index ASC"


                strSQL &= " set @ROW_END = 1 + @ROW_END - @ROW_START "
                strSQL &= " set rowcount @ROW_END "

                strSQL &= "  SELECT   *, ms_SKU.*, ms_Color.Description AS Expr1, ms_Model.Description AS Expr2, ms_Size.Description AS Expr3, ms_Brand.Description AS Expr4,"
                strSQL &= "  ms_Package.Description AS Expr5, ms_Product.Product_Name_th AS Expr6, ms_Product.Product_Name_en AS Expr7,ms_Product.Product_Id AS Product_id"
                strSQL &= " ,tb_PackingBom.PackingBom_Index "
                strSQL &= "  FROM        ms_SKU Left JOIN"
                strSQL &= "  ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT JOIN"
                strSQL &= "  ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index Left JOIN"
                strSQL &= "  ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index LEFT JOIN"
                strSQL &= "  ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index LEFT JOIN"
                strSQL &= "  ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index LEFT JOIN"
                strSQL &= "  ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index LEFT JOIN"
                strSQL &= "    tb_PackingBom ON ms_SKU.Sku_Index = tb_PackingBom.Sku_Index  "

                strSQL &= "  where ms_SKU.status_id != -1 "
                strSQL &= "  AND  ms_SKU.sku_index >= @sku_index "

                If pFilterValue <> "" Then
                    strSQL &= pFilterValue
                End If

                strSQL &= "  order by ms_SKU.SKU_index ASC"
                strSQL &= "  set rowcount 0 "



            Else
                ' TODO:
                ' Sql for define ColumnName & Filter Value 

            End If

            SetSQLString = strSQL + strWhere
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

            strSQL = "SELECT TOP 1 ms_SKU.*, ms_Color.Description AS Expr1, ms_Model.Description AS Expr2, ms_Size.Description AS Expr3, ms_Brand.Description AS Expr4,ms_Location.Location_Alias "
            strSQL &= "           , ms_Package.Description AS PackageDes, ms_Product.Product_Name_th AS Expr6, ms_Product.Product_Name_en AS Expr7,ms_Product.Product_Id AS Product_id ,ms_Customer.Customer_Name as Customer_Name,ms_Supplier.Supplier_Name as Supplier_Name, ms_Package_1.Description AS Item_Package,ms_Product.ProductType_Index"
            strSQL &= "           , ms_Package.DimensionType_Index "

            strSQL &= " FROM  ms_SKU Left JOIN"
            strSQL &= " ms_Package ms_Package_1 ON ms_SKU.Item_Package_Index = ms_Package_1.Package_Index LEFT OUTER JOIN"
            strSQL &= " ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index Left JOIN"
            strSQL &= " ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index Left JOIN "
            strSQL &= " ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index Left JOIN"
            strSQL &= " ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index Left JOIN"
            strSQL &= " ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index Left JOIN"
            strSQL &= " ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index Left JOIN"
            strSQL &= " ms_Customer ON ms_SKU.Customer_Index = ms_Customer.Customer_Index Left JOIN"
            strSQL &= " ms_Location ON ms_SKU.Location_Index = ms_Location.Location_Index Left JOIN"
            strSQL &= " ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index"


            strWhere = ""

            strWhere += " WHERE  ms_SKU.Sku_Index = '" & pFilterValue & "' and ms_SKU.status_id != -1"


            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getSKU_Package(ByVal Sku_Index As String)

        ' *** define value ***
        Me._sku_Index = Sku_Index

        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT      ms_SKURatio.Sku_Index,  ms_SKURatio.Package_Index, ms_Package.Description AS Package,ms_SKURatio.Ratio  "
            strSQL &= "             ,Isnull(ms_Package.Weight,0) as Unit_Weight "
            strSQL &= "             ,(Isnull(ms_Package.Dimension_Wd,0)*Isnull(ms_Package.Dimension_Len,0)*Isnull(ms_Package.Dimension_Hi,0))/Isnull(ms_DimensionType.Ratio,1) as Unit_Volume "
            strSQL &= " FROM        ms_SKURatio INNER JOIN  "
            strSQL &= "             ms_SKU ON ms_SKURatio.Sku_Index = ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "             ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index  LEFT JOIN "
            strSQL &= "             ms_DimensionType on ms_DimensionType.DimensionType_Index=ms_Package.DimensionType_Index "
            strSQL &= " WHERE       ms_SKURatio.Sku_Index ='" & Me._sku_Index & "'"
            strSQL &= "             AND ms_SKURatio.status_id <> -1 AND ms_SKU.Status_Id <> -1 AND  isNull(isItem_Package,0) = 0"

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

    Public Sub getProductByProductType_Index(ByVal pProductType_Index As String)
        Try
            Dim strSQL As String = ""

            strSQL = String.Format(" SELECT 0 as  isCheck,* FROM ms_SKU inner join ms_Product on ms_SKU.Product_Index =ms_Product.Product_Index WHERE ProductType_Index = '{0}' ", pProductType_Index)

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Sku_Index"></param>
    ''' <param name="Package_Index"></param>
    ''' <remarks>
    ''' Date 10/01/2010
    ''' Add By : Dong_kk 
    ''' เพื่อคำนวณ Ratio รับเฉพาะหน่วยที่ เล็กกว่า ตัวตั้งต้นการรับ
    ''' </remarks>

    Public Sub getSKU_PackageReceive(ByVal Sku_Index As String, ByVal Package_Index As String)

        ' *** define value ***


        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT      ms_SKURatio.Sku_Index,  ms_SKURatio.Package_Index, ms_Package.Description AS Package ,  ms_SKURatio.Ratio, ms_SKURatio.Package_Index as Recieve_Package_Index"
            strSQL &= "             FROM        ms_SKURatio INNER JOIN  "
            strSQL &= "                         ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "                         ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= "             WHERE       ms_SKURatio.Sku_Index ='" & Sku_Index & "'"
            strSQL &= "                         AND ms_SKURatio.status_id <> -1 AND ms_SKU.Status_Id <> -1 AND  isNull(isItem_Package,0) = 0"
            strSQL &= "              and        ms_SKURatio.Ratio <= (select aa.ratio from ms_SKURatio aa where aa.Package_Index='" & Package_Index & "')"
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




    Public Sub getSKU_Detail(ByVal Sku_Id As String)

        Me._sku_Id = Sku_Id

        Dim strSQL As String = ""
        Try
            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE Sku_Id ='" & Me._sku_Id & "' AND status_id  not in (-1) "

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

    Public Function getSku_ByBarCode_Package(ByVal pstrBarCodePackage As String, Optional ByVal Customer_Index As String = "") As DataTable
        Dim strSQL As New System.Text.StringBuilder
        Try

            strSQL.Append(" select  ms_Package.Barcode,ms_SKU.Sku_Index,ms_SKU.Sku_Id,ms_SKU.Str1 as Sku_Name_Th,ms_SKU.Str2 as Sku_Name_En,ms_Package.Description as Package_Des,ms_Package.Package_Index  from ms_SKU ")
            strSQL.Append(" Inner Join ms_SKURatio On ms_SKU.Sku_Index = ms_SKURatio.Sku_Index ")
            strSQL.Append(" Inner Join ms_Package On ms_SKURatio.Package_Index = ms_Package.Package_Index ")
            strSQL.Append(String.Format(" where ms_Package.Barcode = '{0}' ", pstrBarCodePackage))


            If Not String.IsNullOrEmpty(Customer_Index) Then
                strSQL.Append(String.Format(" AND ms_SKU.Customer_Index = '{0}' ", Customer_Index))
            End If

            Return DBExeQuery(strSQL.ToString, eCommandType.Text)

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Sub getSKU_Detail_ByCode_Package(ByVal pstrBarCodePackage As String)
        '--- Dong_kk 23-06-09
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE  status_id  not in (-1) "
            strSQL &= " AND Barcode='" & pstrBarCodePackage & "'"

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


    Public Sub getSKU_Detail_BySKU_ID(ByVal pstrSKU_ID As String)
        '--- Dong_kk 23-06-09
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE  status_id  not in (-1) "
            strSQL &= " AND SKU_ID='" & pstrSKU_ID & "'"

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

    Public Sub getSKU_Detail_ByCode(ByVal Barcode1 As String, ByVal Barcode2 As String, ByVal RFIDCode As String, ByVal strAndCondition As String)
        '--- Dong_kk 23-06-09
        Dim strSQL As String = ""
        Try

            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE  status_id  not in (-1) "
            If Barcode1 <> "" Then
                strSQL &= " AND Barcode1='" & Barcode1 & "'"
            End If
            If Barcode2 <> "" Then
                strSQL &= " AND Barcode2='" & Barcode2 & "'"
            End If
            If RFIDCode <> "" Then
                strSQL &= " AND RFIDCode='" & RFIDCode & "'"
            End If
            If strAndCondition <> "" Then
                strSQL &= strAndCondition
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

    Public Sub getSKU_LocationBal(ByVal StrAnd As String)

        Me._sku_Id = Sku_Id

        Dim strSQL As String = ""
        Try
            strSQL = "select * from VIEW_LocationBalance"
            strSQL &= " WHERE  status_id  not in (-1) "

            SetSQLString = strSQL & StrAnd
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function getRatio(ByVal Sku_Index As String, ByVal Package_Index As String) As Double
        ' *** define value ***
        Me._sku_Index = Sku_Index
        Me._package_Index = Package_Index

        Dim strSQL As String = ""

        Try

            strSQL = "  SELECT  ms_SKURatio.Ratio "
            strSQL &= " FROM     ms_SKURatio INNER JOIN "
            strSQL &= " ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "     ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= " WHERE ms_SKURatio.Sku_Index ='" & Me._sku_Index & "' AND ms_SKURatio.Package_Index='" & Me._package_Index & "' and ms_SKU.status_id != -1"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput

            If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
                Return 0
            Else
                Return _scalarOutput
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getSKU_Ratio(ByVal Sku_Index As String, ByVal Package_Index As String) As Double

        ' *** define value ***
        Me._sku_Index = Sku_Index
        Me._package_Index = Package_Index

        Dim strSQL As String = ""
        Try


            strSQL = "  SELECT   ms_SKURatio.Sku_Index,  ms_SKURatio.Package_Index, ms_Package.Description AS Package,ms_Package.Weight "
            strSQL &= " FROM     ms_SKURatio INNER JOIN "
            strSQL &= "     ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= " WHERE ms_SKURatio.Sku_Index ='" & Me._sku_Index & "' AND ms_SKURatio.Package_Index='" & Me._package_Index & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return 0

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function getItemLife_TotalDay(ByVal Sku_Index As String, ByVal Mfg_Date As Date) As Integer

        Dim strSQL As String
        Dim strWhere As String
        Dim TotalDay As Integer = 0
        Try

            strSQL = " SELECT     Sku_Index,ItemLife_y,ItemLife_m,ItemLife_d   " & _
                     " FROM       ms_SKU "

            strWhere = ""

            strWhere += " WHERE  ms_SKU.SKU_Index = '" & Sku_Index & "' and ms_SKU.status_id != -1"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                TotalDay = (Val(_dataTable.Rows(0).Item("ItemLife_y").ToString) * 365) + (Val(_dataTable.Rows(0).Item("ItemLife_m").ToString) * 30) + Val(_dataTable.Rows(0).Item("ItemLife_d").ToString)
                Return TotalDay
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Sub getSKU_MasterDetail(ByVal ColumnName As String, ByVal FilterByvalue As String)

        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            If ColumnName = "" Then
                strSQL = "SELECT     ms_SKU.Sku_Index,ms_SKU.Package_Index, ms_SKU.Sku_Id, ms_SKU.Str1 as SKU_Description, ms_Product.Product_Name_th, ms_Package.Description AS Package, "
                strSQL &= "  ms_Size.Description AS Size, ms_UnitWeight.Description AS UnitWeight, ms_Model.Description AS Model, ms_Color.Description AS Color, "
                strSQL &= "  ms_Brand.Description AS Brand"
                strSQL &= " FROM  ms_SKU INNER JOIN"
                strSQL &= "     ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index INNER JOIN"
                strSQL &= "     ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index LEFT OUTER JOIN"
                strSQL &= "     ms_UnitWeight ON ms_SKU.UnitWeight_Index = ms_UnitWeight.UnitWeight_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index "

            Else
                strSQL = "SELECT     ms_SKU.Sku_Index,ms_SKU.Package_Index, ms_SKU.Sku_Id, ms_SKU.Str1 as SKU_Description, ms_Product.Product_Name_th, ms_Package.Description AS Package, "
                strSQL &= "  ms_Size.Description AS Size, ms_UnitWeight.Description AS UnitWeight, ms_Model.Description AS Model, ms_Color.Description AS Color, "
                strSQL &= "  ms_Brand.Description AS Brand"
                strSQL &= " FROM  ms_SKU INNER JOIN"
                strSQL &= "     ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index INNER JOIN"
                strSQL &= "     ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index LEFT OUTER JOIN"
                strSQL &= "     ms_UnitWeight ON ms_SKU.UnitWeight_Index = ms_UnitWeight.UnitWeight_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index LEFT OUTER JOIN"
                strSQL &= "     ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index "
                Select Case ColumnName
                    Case ""
                        strWhere = ""
                End Select

            End If


            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getPopup_Product(ByVal Sku_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strOrder As String = ""
        Try

            strSQL = "  SELECT   ms_SKU.Sku_Id,ms_SKU.Sku_Index,ms_Product.Product_Name_th "
            strSQL &= " FROM         ms_SKU INNER JOIN"
            strSQL &= " ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "
            strSQL &= "  where   Sku_Index ='" & Sku_Index & "' "


            strOrder &= " ORDER BY ms_SKU.Sku_Id"

            SetSQLString = strSQL & strOrder
            connectDB()
            EXEC_DataAdapter()

            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getPopup_Search(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strOrder As String = ""

        Try
            'strSQL = " SELECT    TOP 500 ms_SKU.Sku_Index,ms_SKU.Sku_Id,ms_SKU.Qty_Per_Pallet, ms_SKU.Str1,ms_SKU.Str2,ms_SKU.Str4,ms_SKU.Str5,ms_Brand.Description as Brand_Des,ms_Model.Description  as Model_Des, ms_SKU.Package_Index"
            'strSQL &= " FROM    ms_SKU INNER JOIN"
            'strSQL &= "         ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "
            'strSQL &= "         LEFT JOIN ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index "
            'strSQL &= "         LEFT JOIN ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index "

            strSQL = "  SELECT     TOP 500 dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_SKU.Qty_Per_Pallet, dbo.ms_SKU.Str1, dbo.ms_SKU.Str2, dbo.ms_SKU.Str4,dbo.ms_SKU.Price1, "
            strSQL &= "          dbo.ms_SKU.Str5, dbo.ms_Brand.Description AS Brand_Des, dbo.ms_Model.Description AS Model_Des, dbo.ms_SKU.Package_Index, "
            strSQL &= "  dbo.ms_PalletType.PalletType_Id, dbo.ms_PalletType.Pallet_Name,dbo.ms_PalletType.PalletType_Index,dbo.ms_Package.Description "
            strSQL &= "  FROM         dbo.ms_SKU INNER JOIN"
            strSQL &= "               dbo.ms_Product ON dbo.ms_SKU.Product_Index = dbo.ms_Product.Product_Index LEFT OUTER JOIN"

            strSQL &= "   dbo.ms_Package ON dbo.ms_SKU.Package_Index = dbo.ms_Package.Package_Index LEFT OUTER JOIN"
            strSQL &= "     dbo.ms_PalletType ON dbo.ms_SKU.PalletType_Index = dbo.ms_PalletType.PalletType_Index LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_Brand ON dbo.ms_SKU.Brand_Index = dbo.ms_Brand.Brand_Index LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_Model ON dbo.ms_SKU.Model_Index = dbo.ms_Model.Model_Index"

            strSQL &= " WHERE   ms_SKU.status_id <> -1  and ms_SKU.str10 <> 4  "

            strOrder &= " ORDER BY ms_SKU.Sku_Id"

            SetSQLString = strSQL & WhereString & strOrder


            connectDB()
            EXEC_DataAdapter()

            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub getSKUItem_TGI(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""

        Try
            'strSQL = " SELECT    TOP 500 ms_SKU.Sku_Index,ms_SKU.Sku_Id,ms_SKU.Qty_Per_Pallet, ms_SKU.Str1,ms_SKU.Str2,ms_SKU.Str4,ms_SKU.Str5,ms_Brand.Description as Brand_Des,ms_Model.Description  as Model_Des, ms_SKU.Package_Index"
            'strSQL &= " FROM    ms_SKU INNER JOIN"
            'strSQL &= "         ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "
            'strSQL &= "         LEFT JOIN ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index "
            'strSQL &= "         LEFT JOIN ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index "

            strSQL = "  SELECT     TOP 500 dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_SKU.Qty_Per_Pallet, dbo.ms_SKU.Str1, dbo.ms_SKU.Str2, dbo.ms_SKU.Str4, "
            strSQL &= "          dbo.ms_SKU.Str5, dbo.ms_Brand.Description AS Brand_Des, dbo.ms_Model.Description AS Model_Des, dbo.ms_SKU.Package_Index, "
            strSQL &= "  dbo.ms_PalletType.PalletType_Id, dbo.ms_PalletType.Pallet_Name,dbo.ms_PalletType.PalletType_Index,dbo.ms_Package.Description "
            strSQL &= "  FROM         dbo.ms_SKU INNER JOIN"
            strSQL &= "               dbo.ms_Product ON dbo.ms_SKU.Product_Index = dbo.ms_Product.Product_Index LEFT OUTER JOIN"

            strSQL &= "   dbo.ms_Package ON dbo.ms_SKU.Package_Index = dbo.ms_Package.Package_Index LEFT OUTER JOIN"
            strSQL &= "     dbo.ms_PalletType ON dbo.ms_SKU.PalletType_Index = dbo.ms_PalletType.PalletType_Index LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_Brand ON dbo.ms_SKU.Brand_Index = dbo.ms_Brand.Brand_Index LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_Model ON dbo.ms_SKU.Model_Index = dbo.ms_Model.Model_Index"

            strSQL &= " WHERE   ms_SKU.status_id <> -1 and str10  = 4"
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

    Public Sub getSKU_TGI(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""

        Try
            'strSQL = " SELECT    TOP 500 ms_SKU.Sku_Index,ms_SKU.Sku_Id,ms_SKU.Qty_Per_Pallet, ms_SKU.Str1,ms_SKU.Str2,ms_SKU.Str4,ms_SKU.Str5,ms_Brand.Description as Brand_Des,ms_Model.Description  as Model_Des, ms_SKU.Package_Index"
            'strSQL &= " FROM    ms_SKU INNER JOIN"
            'strSQL &= "         ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "
            'strSQL &= "         LEFT JOIN ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index "
            'strSQL &= "         LEFT JOIN ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index "

            strSQL = "  SELECT     TOP 500 dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_SKU.Qty_Per_Pallet, dbo.ms_SKU.Str1, dbo.ms_SKU.Str2, dbo.ms_SKU.Str4, "
            strSQL &= "          dbo.ms_SKU.Str5, dbo.ms_Brand.Description AS Brand_Des, dbo.ms_Model.Description AS Model_Des, dbo.ms_SKU.Package_Index, "
            strSQL &= "  dbo.ms_PalletType.PalletType_Id, dbo.ms_PalletType.Pallet_Name,dbo.ms_PalletType.PalletType_Index,dbo.ms_Package.Description "
            strSQL &= "  FROM         dbo.ms_SKU INNER JOIN"
            strSQL &= "               dbo.ms_Product ON dbo.ms_SKU.Product_Index = dbo.ms_Product.Product_Index LEFT OUTER JOIN"

            strSQL &= "   dbo.ms_Package ON dbo.ms_SKU.Package_Index = dbo.ms_Package.Package_Index LEFT OUTER JOIN"
            strSQL &= "     dbo.ms_PalletType ON dbo.ms_SKU.PalletType_Index = dbo.ms_PalletType.PalletType_Index LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_Brand ON dbo.ms_SKU.Brand_Index = dbo.ms_Brand.Brand_Index LEFT OUTER JOIN"
            strSQL &= "   dbo.ms_Model ON dbo.ms_SKU.Model_Index = dbo.ms_Model.Model_Index"

            strSQL &= " WHERE   ms_SKU.status_id <> -1 and str10  <> 4"
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

    Public Function isChckID(ByVal _Sku_Id As String, ByVal _PurchaseOrderItem_Index As String) As Boolean
        Dim strSQL As String
        Try
            'strSQL = " select count(*) from ms_sku where Sku_Id = @Sku_Id  "
            strSQL = " SELECT     dbo.tb_PurchaseOrderItem.PurchaseOrderItem_Index, dbo.tb_PurchaseOrderItem.Sku_Index, dbo.ms_SKU.Sku_Id"
            strSQL &= " FROM         dbo.ms_SKU RIGHT OUTER JOIN"
            strSQL &= " dbo.tb_PurchaseOrderItem ON dbo.ms_SKU.Sku_Index = dbo.tb_PurchaseOrderItem.Sku_Index"
            strSQL &= "  where PurchaseOrderItem_Index=@PurchaseOrderItem_Index and Sku_Id = @Sku_Id"

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@PurchaseOrderItem_Index", SqlDbType.VarChar, 13).Value = _PurchaseOrderItem_Index
            SQLServerCommand.Parameters.Add("@Sku_Id", SqlDbType.VarChar, 13).Value = _Sku_Id

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


    'Krit add 18/11/2008
    Public Sub SearchData_Count(ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""

        Try

            strSQL = "SELECT COUNT(*) AS Row_Total "
            strSQL &= "FROM         ms_SKU Left JOIN "
            strSQL &= "ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT JOIN "
            strSQL &= "ms_Size ON ms_SKU.Size_Index = ms_Size.Size_Index LEFT JOIN "
            strSQL &= "ms_Package ON ms_SKU.Package_Index = ms_Package.Package_Index LEFT JOIN "
            strSQL &= "ms_Color ON ms_SKU.Color_Index = ms_Color.Color_Index LEFT JOIN "
            strSQL &= "ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index LEFT JOIN "
            strSQL &= "ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index LEFT JOIN"
            strSQL &= "    tb_PackingBom ON ms_SKU.Sku_Index = tb_PackingBom.Sku_Index"

            strSQL &= " WHERE ms_SKU.status_id != -1 "

            If pFilterValue <> "" Then
                strSQL &= pFilterValue
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

    Public Sub getSKU_ID(ByVal Sku_Id As String)

        Dim strSQL As String = ""
        Try
            strSQL = "select sku_index from ms_sku"
            strSQL &= " WHERE Sku_Id ='" & Sku_Id & "' and status_Id <> -1 "

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



    Public Sub GetPopup_Search_ByOrder_Index(ByVal pstrOrder_Index As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            strSQL = " SELECT  tb_OrderItem.OrderItem_Index, ms_SKU.Sku_Index,ms_SKU.Sku_Id, ms_Product.Product_Name_th, ms_SKU.Str1,ms_SKU.Str2 ,ms_SKU.Qty_Per_Pallet,ms_SKU.Str4 ,ms_SKU.Str5,ms_Brand.Description as Brand_Des, ms_model.Description as model_des,ms_SKU.Package_Index,ms_SKU.Price1"
            strSQL &= " FROM  tb_OrderItem LEFT JOIN ms_SKU ON "
            strSQL &= " tb_OrderItem.Sku_Index = ms_SKU.Sku_Index Left JOIN "
            strSQL &= " ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "
            strSQL &= " LEFT JOIN ms_Brand ON ms_SKU.Brand_Index = ms_Brand.Brand_Index "
            strSQL &= " LEFT JOIN ms_Model ON ms_SKU.Model_Index = ms_Model.Model_Index "


            strSQL &= " WHERE   ms_SKU.status_id <> -1  and ms_SKU.str10 <> 4  AND tb_OrderItem.Order_Index = '" & pstrOrder_Index & "'"


            SetSQLString = strSQL + strWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub GetPopup_Search_By_Sku_Index(ByVal pstrSkuIndex As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            strSQL = " SELECT  tb_OrderItem.OrderItem_Index, ms_SKU.Sku_Index,ms_SKU.Sku_Id, ms_Product.Product_Name_th   "
            strSQL &= " FROM  tb_OrderItem LEFT JOIN ms_SKU ON "
            strSQL &= " tb_OrderItem.Sku_Index = ms_SKU.Sku_Index Left JOIN "
            strSQL &= " ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "

            strSQL &= " WHERE   ms_SKU.status_id <> -1 AND tb_OrderItem.sku_index = '" & pstrSkuIndex & "'"


            SetSQLString = strSQL + strWhere

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getPopup_Search_ByCustomerRefID(ByVal WhereString As String, ByVal pstrCustomer_Index As String, Optional ByVal strCusRefSku_Id As String = "")
        Dim strSQL As String = ""
        Try
            If Not String.IsNullOrEmpty(pstrCustomer_Index) Then
                strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Sku_Id, ms_Product.Product_Name_th, ms_SKU.Price1"
                strSQL &= "           , Customer_Index = '" & pstrCustomer_Index & "'"
                strSQL &= "           , Customer_Name = (select top 1 Customer_Name from ms_Customer where Customer_Index = '" & pstrCustomer_Index & "') "
                strSQL &= "           , ms_Supplier.Supplier_Id, ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index,ms_SKU.Package_Index"
                strSQL &= "           , Str4 = isnull((select top 1 rf.RefId "
                strSQL &= "			            From ms_Customer_SKU_RefId rf "
                strSQL &= "                     where  rf.Sku_Index = ms_SKU.Sku_Index "
                strSQL &= "				                and rf.Customer_Index = '" & pstrCustomer_Index & "'),ms_Sku.Sku_Id)"
                strSQL &= " FROM         ms_SKU INNER JOIN"
                strSQL &= "             ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT OUTER JOIN"
                strSQL &= "             ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index LEFT OUTER JOIN "
                strSQL &= "    tb_PackingBom ON ms_SKU.Sku_Index = tb_PackingBom.Sku_Index  "
                strSQL &= "    WHERE(ms_SKU.status_id <> -1)"
                If Not String.IsNullOrEmpty(strCusRefSku_Id) Then
                    strSQL &= "  and  isnull((select rf.RefId "
                    strSQL &= "			            From ms_Customer_SKU_RefId rf "
                    strSQL &= "                     where  rf.Sku_Index = ms_SKU.Sku_Index "
                    strSQL &= "				                and rf.Customer_Index = '" & pstrCustomer_Index & "'),ms_Sku.Sku_Id) like '" & strCusRefSku_Id & "%'"
                End If
            Else
                strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Sku_Id, "
                strSQL &= "             ISNULL(ms_Customer_SKU_RefId.RefId, ms_SKU.Sku_Id) AS Str4, ms_Product.Product_Name_th, ms_SKU.Price1, isnull(ms_Customer.Customer_Index,ms_SKU.Customer_Index) as Customer_Index ,  "
                strSQL &= "  isnull(ms_Customer.Customer_Name,(select top 1 Customer_Name from ms_Customer where Customer_Index =ms_SKU.Customer_Index ) ) as Customer_Name, "
                strSQL &= "             ms_Customer.Title, ms_Supplier.Supplier_Id, ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index, ms_SKU.Package_Index"
                strSQL &= " FROM         ms_SKU INNER JOIN"
                strSQL &= "          ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index  LEFT OUTER JOIN"
                strSQL &= "           ms_Customer_SKU_RefId ON ms_SKU.Sku_Index = ms_Customer_SKU_RefId.Sku_Index  LEFT OUTER JOIN"
                strSQL &= "          ms_Customer ON ms_Customer_SKU_RefId.Customer_Index = ms_Customer.Customer_Index LEFT OUTER JOIN"
                strSQL &= "           ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index"
                strSQL &= " WHERE     (ms_SKU.status_id <> - 1)"
                If Not String.IsNullOrEmpty(strCusRefSku_Id) Then
                    strSQL &= "  and  ISNULL(ms_Customer_SKU_RefId.RefId, ms_SKU.Sku_Id) like '" & strCusRefSku_Id & "%'"
                End If
            End If

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

    Public Sub getPopup_Search_ByCustomerRefID(ByVal WhereString As String, ByVal pstrCustomer_Index As String, ByVal model As Model_Condition_Popup, Optional ByVal strCusRefSku_Id As String = "")
        Dim strSQL As String = ""
        Dim strSQLSKU_Detail As String = ""
        Try

            If Not String.IsNullOrEmpty(pstrCustomer_Index) Then
                strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Str3, ms_SKU.Sku_Id, ms_Product.Product_Name_th, ms_SKU.Price1"
                strSQL &= "           , Customer_Index = '" & pstrCustomer_Index & "'"
                strSQL &= "           , Customer_Name = (select top 1 Customer_Name from ms_Customer where Customer_Index = '" & pstrCustomer_Index & "') "
                strSQL &= "           , ms_Supplier.Supplier_Id, ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index,ms_SKU.Package_Index"
                strSQL &= "           , Str4 = isnull((select top 1 rf.RefId "
                strSQL &= "			            From ms_Customer_SKU_RefId rf "
                strSQL &= "                     where  rf.Sku_Index = ms_SKU.Sku_Index "
                strSQL &= "				                and rf.Customer_Index = '" & pstrCustomer_Index & "'),ms_Sku.Sku_Id)"
                strSQL &= " FROM         ms_SKU INNER JOIN"
                strSQL &= "    ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT OUTER JOIN"
                strSQL &= "    ms_ProductType ON ms_Product.ProductType_Index = ms_ProductType.ProductType_Index LEFT OUTER JOIN"
                strSQL &= "    ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index LEFT OUTER JOIN "
                strSQL &= "    tb_PackingBom ON ms_SKU.Sku_Index = tb_PackingBom.Sku_Index  "
                strSQL &= "    WHERE(ms_SKU.status_id <> -1)"
                If Not String.IsNullOrEmpty(strCusRefSku_Id) Then
                    strSQL &= "  and  isnull((select rf.RefId "
                    strSQL &= "			            From ms_Customer_SKU_RefId rf "
                    strSQL &= "                     where  rf.Sku_Index = ms_SKU.Sku_Index "
                    strSQL &= "				                and rf.Customer_Index = '" & pstrCustomer_Index & "'),ms_Sku.Sku_Id) like '" & strCusRefSku_Id & "%'"
                End If
            Else
                strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Str3, ms_SKU.Sku_Id, "
                strSQL &= "             ISNULL(ms_Customer_SKU_RefId.RefId, ms_SKU.Sku_Id) AS Str4, ms_Product.Product_Name_th, ms_SKU.Price1, isnull(ms_Customer.Customer_Index,ms_SKU.Customer_Index) as Customer_Index ,  "
                strSQL &= "  isnull(ms_Customer.Customer_Name,(select top 1 Customer_Name from ms_Customer where Customer_Index =ms_SKU.Customer_Index ) ) as Customer_Name, "
                strSQL &= "             ms_Customer.Title, ms_Supplier.Supplier_Id, ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index, ms_SKU.Package_Index"
                strSQL &= " FROM         ms_SKU INNER JOIN"
                strSQL &= "          ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index  LEFT OUTER JOIN"
                strSQL &= "          ms_ProductType ON ms_Product.ProductType_Index = ms_ProductType.ProductType_Index LEFT OUTER JOIN"
                strSQL &= "          ms_Customer_SKU_RefId ON ms_SKU.Sku_Index = ms_Customer_SKU_RefId.Sku_Index  LEFT OUTER JOIN"
                strSQL &= "          ms_Customer ON ms_Customer_SKU_RefId.Customer_Index = ms_Customer.Customer_Index LEFT OUTER JOIN"
                strSQL &= "          ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index"
                strSQL &= " WHERE     (ms_SKU.status_id <> - 1)"
                If Not String.IsNullOrEmpty(strCusRefSku_Id) Then
                    strSQL &= "  and  ISNULL(ms_Customer_SKU_RefId.RefId, ms_SKU.Sku_Id) like '" & strCusRefSku_Id & "%'"
                End If
            End If

            With model
                If .Eye <> "" Or .Add <> "" Or .Tilted <> "" Or .Color <> "" Or .Degree <> "" Or .BC <> "" Or .VMI <> "" Or .Generation <> "" Or .Brand <> "" Then
                    strSQLSKU_Detail = " AND ms_SKU.Sku_Index IN ( "
                    strSQLSKU_Detail &= " SELECT Sku_Index FROM ms_SKU_Detail WHERE 1 = 1 "
                    If model.Eye <> "" Then strSQLSKU_Detail &= " And [Eye] = N'" & model.Eye & "'"
                    If model.Add <> "" Then strSQLSKU_Detail &= " And [Add] = N'" & model.Add & "'"
                    If model.Tilted <> "" Then strSQLSKU_Detail &= " And [Tilted] = N'" & model.Tilted & "'"
                    If model.Color <> "" Then strSQLSKU_Detail &= " And [Color] = N'" & model.Color & "'"
                    If model.Degree <> "" Then strSQLSKU_Detail &= " And [Degree] = N'" & model.Degree & "'"
                    If model.BC <> "" Then strSQLSKU_Detail &= " And [BC] = N'" & model.BC & "'"
                    If model.VMI <> "" Then strSQLSKU_Detail &= " And [VMI] = N'" & model.VMI & "'"
                    If model.Generation <> "" Then strSQLSKU_Detail &= " And [Generation] = N'" & model.Generation & "'"
                    If model.Brand <> "" Then strSQLSKU_Detail &= " And [Brand] = N'" & model.Brand & "'"
                    strSQLSKU_Detail &= " )"
                End If
            End With

            SetSQLString = strSQL & WhereString & strSQLSKU_Detail
            connectDB()
            EXEC_DataAdapter()

            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getPopup_Search_ByCustomer(ByVal WhereString As String, ByVal pstrCustomer_Index As String)
        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Str3, ms_SKU.Sku_Id, ms_Product.Product_Name_th, ms_SKU.Price1"
            strSQL &= "           , ms_Customer.Customer_Index, ms_Customer.Customer_Name, ms_Customer.Title, ms_Supplier.Supplier_Id"
            strSQL &= "           , ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index,ms_SKU.Package_Index"
            strSQL &= "           , Str4 = isnull(ms_SKU.Str4,ms_Sku.Sku_Id) "
            strSQL &= " FROM         ms_SKU INNER JOIN"
            strSQL &= "             ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT OUTER JOIN"
            strSQL &= "             ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index LEFT OUTER JOIN"
            strSQL &= "             ms_Customer ON ms_SKU.Customer_Index = ms_Customer.Customer_Index"
            strSQL &= "    WHERE(ms_SKU.status_id <> -1)"

            If Not String.IsNullOrEmpty(pstrCustomer_Index) Then
                strSQL &= "  and ( ms_SKU.Customer_Index ='" & pstrCustomer_Index & "'  Or  ms_SKU.Customer_Index ='' )"
            End If

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

    Public Sub getPopup_Search_ByCustomer(ByVal WhereString As String, ByVal pstrCustomer_Index As String, ByVal model As Model_Condition_Popup)
        Dim strSQL As String = ""
        Dim strSQLSKU_Detail As String = ""
        Try

            strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Str3, ms_SKU.Sku_Id,ISNULL(ms_ProductType.ProductType_Index,'') AS ProductType_Index, ms_Product.Product_Name_th, ms_SKU.Price1"
            strSQL &= "           , ms_Customer.Customer_Index, ms_Customer.Customer_Name, ms_Customer.Title, ms_Supplier.Supplier_Id"
            strSQL &= "           , ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index,ms_SKU.Package_Index"
            strSQL &= "           , Str4 = isnull(ms_SKU.Str4,ms_Sku.Sku_Id) "
            strSQL &= " FROM         ms_SKU "
            strSQL &= " INNER JOIN  ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index "
            strSQL &= " LEFT JOIN   ms_ProductType ON ms_Product.ProductType_Index = ms_ProductType.ProductType_Index "
            strSQL &= " LEFT JOIN   ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index "
            strSQL &= " LEFT JOIN   ms_Customer ON ms_SKU.Customer_Index = ms_Customer.Customer_Index "
            strSQL &= " WHERE(ms_SKU.status_id <> -1)"

            If Not String.IsNullOrEmpty(pstrCustomer_Index) Then
                strSQL &= "  and ( ms_SKU.Customer_Index ='" & pstrCustomer_Index & "'  Or  ISNULL(ms_SKU.Customer_Index,'') ='' )"
            End If

            With model
                If .Eye <> "" Or .Add <> "" Or .Tilted <> "" Or .Color <> "" Or .Degree <> "" Or .BC <> "" Or .VMI <> "" Or .Generation <> "" Or .Brand <> "" Then
                    strSQLSKU_Detail = " AND ms_Sku.Sku_Index IN ( "
                    strSQLSKU_Detail &= " SELECT Sku_Index FROM ms_SKU_Detail WHERE 1 = 1 "
                    If model.Eye <> "" Then strSQLSKU_Detail &= " And [Eye] = N'" & model.Eye & "'"
                    If model.Add <> "" Then strSQLSKU_Detail &= " And [Add] = N'" & model.Add & "'"
                    If model.Tilted <> "" Then strSQLSKU_Detail &= " And [Tilted] = N'" & model.Tilted & "'"
                    If model.Color <> "" Then strSQLSKU_Detail &= " And [Color] = N'" & model.Color & "'"
                    If model.Degree <> "" Then strSQLSKU_Detail &= " And [Degree] = N'" & model.Degree & "'"
                    If model.BC <> "" Then strSQLSKU_Detail &= " And [BC] = N'" & model.BC & "'"
                    If model.VMI <> "" Then strSQLSKU_Detail &= " And [VMI] = N'" & model.VMI & "'"
                    If model.Generation <> "" Then strSQLSKU_Detail &= " And [Generation] = N'" & model.Generation & "'"
                    If model.Brand <> "" Then strSQLSKU_Detail &= " And [Brand] = N'" & model.Brand & "'"
                    strSQLSKU_Detail &= " )"
                End If
            End With

            SetSQLString = strSQL & WhereString & strSQLSKU_Detail
            connectDB()
            EXEC_DataAdapter()

            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub getPopup_Search_ByCustomer(ByVal WhereString As String)
        '  
        Dim strSQL As String = ""

        Try
            'strSQL = " SELECT           TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2,ms_SKU.Str4, ms_SKU.Sku_Id, ms_Product.Product_Name_th, ms_Customer.Customer_Index, "
            'strSQL &= "                 ms_Customer.Customer_Name, ms_Customer.Title"
            'strSQL &= " FROM            ms_SKU INNER JOIN"
            'strSQL &= "                 ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT OUTER JOIN"
            'strSQL &= "                 ms_Customer ON ms_SKU.Customer_Index = ms_Customer.Customer_Index"
            'strSQL &= " WHERE           ms_SKU.status_id <> - 1"
            strSQL = "  SELECT     TOP 500 ms_SKU.Sku_Index, ms_SKU.Str1, ms_SKU.Str2, ms_SKU.Str4, ms_SKU.Sku_Id, ms_Product.Product_Name_th, ms_SKU.Price1,"
            strSQL &= "            ms_Customer.Customer_Index, ms_Customer.Customer_Name, ms_Customer.Title, ms_Supplier.Supplier_Id, "
            strSQL &= "   ms_Supplier.Supplier_Name, ms_Supplier.Supplier_Index"
            strSQL &= " FROM         ms_SKU INNER JOIN"
            strSQL &= "             ms_Product ON ms_SKU.Product_Index = ms_Product.Product_Index LEFT OUTER JOIN"
            strSQL &= "        ms_Supplier ON ms_SKU.Supplier_Index = ms_Supplier.Supplier_Index LEFT OUTER JOIN"
            strSQL &= "    ms_Customer ON ms_SKU.Customer_Index = ms_Customer.Customer_Index"
            strSQL &= "    WHERE(ms_SKU.status_id <> -1)"

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

    Public Sub getPopup_SKU(ByVal ColumnName As String, ByVal pFilterValue As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            If ColumnName = "" And pFilterValue = "" Then

                strSQL = " select *  from ms_SKU where status_id  not in (-1)   "
                strWhere = ""
            Else

                strSQL = " select *  from ms_SKU   "
                strWhere = "WHERE  " & ColumnName & " ='" & pFilterValue & "'and status_id not in (-1) "
            End If

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub



    Public Sub getSKU_Detail_ByCustomer(ByVal pstrSku_Id As String, ByVal pstrCustomer_Index As String)

        Me._sku_Id = Sku_Id

        Dim strSQL As String = ""
        Try
            If pstrSku_Id.Trim = "" Then Exit Sub

            _sku_Id = _sku_Id.Replace("'", "''").ToString
            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE Sku_Id Like '" & pstrSku_Id & "%' AND status_id  not in (-1) "   'FOR TIMCO
            'strSQL &= "WHERE Sku_Id = '" & pstrSku_Id & "' AND status_id  not in (-1) "
            If Not pstrCustomer_Index = "" Then
                strSQL &= " AND Customer_Index ='" & pstrCustomer_Index & "'"
            End If

            strSQL &= " Order By Sku_Id "

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

    Public Sub getSKU_Detail_Equal_ByCustomer(ByVal pstrSku_Id As String, ByVal pstrCustomer_Index As String)

        'Me._sku_Id = Sku_Id

        Dim strSQL As String = ""
        Try
            If pstrSku_Id.Trim = "" Then Exit Sub
            pstrSku_Id = pstrSku_Id.Replace("'", "''").ToString

            strSQL = "SELECT * FROM VIEW_MS_SKU_Detail "
            strSQL &= "WHERE Sku_Id = '" & pstrSku_Id & "' AND status_id  not in (-1) " 'FOR TIMCO
            If Not pstrCustomer_Index = "" Then
                strSQL &= " AND (Customer_Index ='" & pstrCustomer_Index & "' Or Customer_Index ='' )"
            End If

            strSQL &= " Order By Sku_Id "


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

    Public Function UpdateSupplierIndex(ByVal Sku_Index As String, ByVal Supplier_Index As String, ByVal Supplier_No As String) As Boolean
        Try
            Dim Sql As New System.Text.StringBuilder
            With Sql
                .Append(" UPDATE ms_Sku ")

                If String.IsNullOrEmpty(Supplier_Index) Then
                    .Append(" SET Supplier_Index = (SELECT TOP 1 Supplier_Index FROM ms_Supplier WHERE Status_id <> -1 AND Supplier_Id = @Supplier) ")
                Else
                    .Append(" SET Supplier_Index = @Supplier ")
                End If

                .Append(" WHERE Sku_Index = @Sku_Index ")
            End With

            With SQLServerCommand.Parameters
                .Clear()

                .Add("Sku_Index", SqlDbType.VarChar).Value = Sku_Index

                If String.IsNullOrEmpty(Supplier_Index) Then
                    .Add("Supplier", SqlDbType.VarChar).Value = Supplier_No
                Else
                    .Add("Supplier", SqlDbType.VarChar).Value = Supplier_Index
                End If
            End With

            Return DBExeNonQuery(Sql.ToString) > 0

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Public Function AutoInsertSKU(ByVal Sku_Id As String, ByVal Sku_Name_Thai As String, ByVal Package_Name As String, Optional ByVal Ratio As Integer = 1, Optional ByVal ProductType_ID As String = "", Optional ByVal ProductType_Name As String = "", Optional ByVal Sku_Name_Eng As String = "", Optional ByVal Unit_Weight As Double = 0, Optional ByVal DimensionType_Id As String = "", Optional ByVal Width As Double = 0, Optional ByVal Length As Double = 0, Optional ByVal Height As Double = 0, Optional ByVal Barcode1 As String = "", Optional ByVal Customer_Index As String = "") As Boolean
        Dim strSQL As String = ""
        Dim strSku_Index As String = ""
        Dim strPackage_Index As String = ""
        Dim strSkuRatio_Index As String = ""
        Dim strProduct_Index As String = ""
        Dim objCustomer_Setting As New config_CustomSetting
        Dim strProductType_Index As String = ""

        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Dim objSy_AutoNumber As New Sy_AutoNumber

        Try
            If ProductType_ID <> "" Then
                strSQL = "  SELECT  * "
                strSQL &= "  FROM   ms_ProductType  "
                strSQL &= "   WHERE  ProductType_Id = '" & ProductType_ID & "'  AND Status_Id <> -1 "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DS, "PD")

                If DS.Tables("PD").Rows.Count <= 0 Then 'AutoInsertProductType
                    strSQL = "  INSERT INTO ms_ProductType(                                                             "
                    strSQL &= "                              ProductType_Index, ProductType_ID, Description, add_by     "
                    strSQL &= "	                            ,add_date,add_branch,status_id,ItemLife_y,ItemLife_m        "
                    strSQL &= "		                        ,ItemLife_d                                                 "
                    strSQL &= "	                           )   VALUES                                                   "
                    strSQL &= "                         (   @ProductType_Index,@ProductType_Id,@Description             "
                    strSQL &= "                             ,@add_by,getdate(),@add_branch,@status_id                   "
                    strSQL &= "                             ,@ItemLife_y,@ItemLife_m,@ItemLife_d                        "
                    strSQL &= "                         )                                                               "

                    strProductType_Index = objSy_AutoNumber.getSys_Value("ProductType_Index")

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = strProductType_Index
                        .Parameters.Add("@ProductType_Id", SqlDbType.VarChar, 50).Value = ProductType_ID
                        If ProductType_Name = "" Then
                            .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ProductType_ID
                        Else
                            .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = ProductType_Name
                        End If
                        .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                        .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                        .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                        .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                        .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                    End With

                    SetSQLString = strSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                Else
                    strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
                End If
            Else
                strSQL = "  SELECT  top 1  * "
                strSQL &= "  FROM   ms_ProductType Where Status_Id <> -1 "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DS, "PD")
                strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
            End If




            Dim objDiman As New ms_DimensionType
            Dim dtDimensionTypeData As New DataTable
            Dim DimensionType_Index As String = ""
            If DimensionType_Id <> "" Then
                dtDimensionTypeData = objDiman.GetDimensionTypeData(" AND DimensionType_Id = '" & DimensionType_Id & "'")
                DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
            Else
                dtDimensionTypeData = objDiman.GetDimensionTypeData("")
                DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
            End If








            strPackage_Index = objSy_AutoNumber.getSys_Value("Package_Index")
            strSQL = "   INSERT INTO ms_Package (                                                           "
            strSQL &= "                         Package_Index,Package_Id,Description,                       "
            strSQL &= "                         Dimension_Hi,Dimension_Wd,Dimension_Len,Dimension_Unit_Id , "
            strSQL &= "                         add_by,add_date,add_branch,status_id, Barcode,              "
            strSQL &= "                         Pkg_Description,isItem_Package,Weight,DimensionType_Index   "
            strSQL &= "                         )                                                           "
            strSQL &= "  VALUES (                                                                           "
            strSQL &= "             @Package_Index, @Package_Id,@Description ,                              "
            strSQL &= "             @Dimension_Hi,@Dimension_Wd,@Dimension_Len,@Dimension_Unit_Id ,         "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id,@Barcode,                      "
            strSQL &= "             @Pkg_Description,@isItem_Package,@Weight,@DimensionType_Index           "
            strSQL &= "          )                                                                          "


            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Package_Name
                .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Package_Name

                .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = Height
                .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = Width
                .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = Length
                .Parameters.Add("@Dimension_Unit_Id", SqlDbType.VarChar).Value = "0"

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

                .Parameters.Add("@Pkg_Description", SqlDbType.NVarChar).Value = ""
                .Parameters.Add("@isItem_Package", SqlDbType.Int).Value = 0
                .Parameters.Add("@Weight", SqlDbType.Float).Value = Unit_Weight
                .Parameters.Add("@DimensionType_Index", SqlDbType.VarChar, 13).Value = DimensionType_Index

                If Barcode1 = "" Then
                    .Parameters.Add("@Barcode", SqlDbType.VarChar).Value = Sku_Id
                Else
                    .Parameters.Add("@Barcode", SqlDbType.VarChar).Value = Barcode1
                End If
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            'Insert ms_Product
            strProduct_Index = objSy_AutoNumber.getSys_Value("Product_Index")

            strSQL = "   INSERT INTO ms_Product (                                                           "

            strSQL &= "                         Product_Index,Product_Id,Product_Name_th,Product_Name_en ,  "
            strSQL &= "                         ProductType_Index,Std_Unit_Cost,Default_Exp_Day ,                             "
            strSQL &= "                         add_by,add_date,add_branch,status_id    "

            strSQL &= "                         )                                                           "


            strSQL &= "  VALUES (                                                                           "

            strSQL &= "             @Product_Index, @Product_Id,@Product_Name_th,@Product_Name_en ,         "
            strSQL &= "             @ProductType_Index,@Std_Unit_Cost,@Default_Exp_Day ,                                       "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id            "

            strSQL &= "          )                                                                          "



            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Product_Index", SqlDbType.VarChar).Value = strProduct_Index
                .Parameters.Add("@Product_Id", SqlDbType.NVarChar).Value = Sku_Id
                .Parameters.Add("@Product_Name_th", SqlDbType.NVarChar).Value = Sku_Id
                .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Sku_Id
                .Parameters.Add("@ProductType_Index", SqlDbType.VarChar).Value = strProductType_Index
                .Parameters.Add("@Std_Unit_Cost", SqlDbType.Float).Value = 0
                .Parameters.Add("@Default_Exp_Day", SqlDbType.Float).Value = 0

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            'Insert ms_SKU
            strSku_Index = objSy_AutoNumber.getSys_Value("Sku_Index")

            ' strReturn = strSku_Index

            strSQL = "  INSERT INTO ms_SKU (Sku_Index,Sku_Id,Product_Index,Size_Index,Package_Index,UnitWeight_Index,Unit_Volume, "
            strSQL &= " Color_Index,Model_Index,Brand_Index,Barcode1,Price1,Price2,Price3,ItemLife_y,ItemLife_m,ItemLife_d, "
            strSQL &= " Str1,Str2,Str10,Min_Qty,Min_Weight,Min_Volume,Max_Qty,Max_Weight,Max_Volume,Qty_Per_Pallet, Customer_Index, "
            strSQL &= " add_by,add_date,add_branch,isPLot,status_id,PalletType_Index,Pick_Method,Picking_Type,Unit_Width,Unit_Length,Unit_Height   "
            strSQL &= " )    "
            strSQL &= " VALUES (                                                                            "
            strSQL &= "         @Sku_Index, @Sku_Id  ,@Product_Index                                                      "
            strSQL &= "         ,@Size_Index,@Package_Index,@UnitWeight_Index, @Unit_Volume,@Color_Index,@Model_Index ,@Brand_Index ,@Barcode1 "
            strSQL &= "         ,@Price1,@Price2,@Price3                                                  "
            strSQL &= "         ,@ItemLife_y ,@ItemLife_m ,@ItemLife_d                                      "
            strSQL &= "         ,@Str1  ,@Str2  ,@Str10                                                     "
            strSQL &= "         ,@Min_Qty ,@Min_Weight ,@Min_Volume                                         "
            strSQL &= "         ,@Max_Qty ,@Max_Weight ,@Max_Volume                                         "
            strSQL &= "         ,@Qty_Per_Pallet, @Customer_Index                                           "
            strSQL &= "         ,@add_by,getdate(),@add_branch                                              "
            strSQL &= "         ,@isPLot                                                                    "
            strSQL &= "         ,@status_id                                                                 "
            strSQL &= "         ,@PalletType_Index                                                          "
            strSQL &= "         ,@Pick_Method                                                               "
            strSQL &= "         ,@Picking_Type                                                            "
            strSQL &= "         ,@Unit_Width,@Unit_Length,@Unit_Height  ) "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                .Parameters.Add("@Sku_Id", SqlDbType.NVarChar).Value = Sku_Id
                .Parameters.Add("@Product_Index", SqlDbType.NVarChar).Value = strProduct_Index
                .Parameters.Add("@Size_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@UnitWeight_Index", SqlDbType.Float).Value = Unit_Weight
                .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = (Width * Length * Height)
                .Parameters.Add("@Color_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Model_Index", SqlDbType.VarChar).Value = ""
                .Parameters.Add("@Brand_Index", SqlDbType.VarChar).Value = ""

                If Barcode1 = "" Then
                    .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Sku_Id
                Else
                    .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Barcode1
                End If


                .Parameters.Add("@Price1", SqlDbType.Float).Value = 0
                .Parameters.Add("@Price2", SqlDbType.Float).Value = 0
                .Parameters.Add("@Price3", SqlDbType.Float).Value = 0
                .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = Sku_Name_Thai

                If Sku_Name_Eng = "" Then
                    .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Sku_Name_Thai
                Else
                    .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Sku_Name_Eng
                End If

                If String.IsNullOrEmpty(Customer_Index) Then
                    .Parameters.Add("@Customer_Index", SqlDbType.NVarChar).Value = ""
                Else
                    .Parameters.Add("@Customer_Index", SqlDbType.NVarChar).Value = Customer_Index
                End If

                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = "1"
                .Parameters.Add("@Min_Qty", SqlDbType.Float).Value = 0
                .Parameters.Add("@Min_Weight", SqlDbType.Float).Value = 0
                .Parameters.Add("@Min_Volume", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = 0
                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = 1
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@isPLot", SqlDbType.Bit).Value = 0
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar).Value = "1"
                .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = 1
                .Parameters.Add("@Picking_Type", SqlDbType.Int).Value = 0


                .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = Height


            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()



            strSkuRatio_Index = objSy_AutoNumber.getSys_Value("SkuRatio_Index")
            objSy_AutoNumber = Nothing

            strSQL = "   INSERT INTO ms_SkuRatio (                                                          "
            strSQL &= "                         SkuRatio_Index,Sku_Index,Package_Index,Ratio,               "
            strSQL &= "                         add_by,add_date,add_branch,status_id                        "
            strSQL &= "                         )                                                           "
            strSQL &= "  VALUES (                                                                           "
            strSQL &= "             @SkuRatio_Index, @Sku_Index,@Package_Index,@Ratio,                      "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id                                "
            strSQL &= "          )                                                                          "

            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@SkuRatio_Index", SqlDbType.VarChar).Value = strSkuRatio_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@Ratio", SqlDbType.Float).Value = Ratio

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0


            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            _MessageError = ex.ToString
            Return False
        Finally

            disconnectDB()
        End Try
    End Function

    Private Function InsertSKU_Master() As Boolean
        Dim strSQL As String

        Try

            strSQL = " insert into ms_SKU (SKU_Index,SKU_Id,product_Index,size_Index,package_Index,unitWeight_Index,color_Index,model_Index,brand_Index,barcode1,barcode2,price1,price2,price3,itemLife_y,itemLife_m,itemLife_d,str1,str2,str3,str10,min_Qty,min_Weight,min_Volume,max_Qty,max_Weight,max_Volume,Qty_Per_Pallet,add_by,add_date,add_branch,isPlot,isControlAsset,Unit_Width,Unit_Length,Unit_Height,Unit_Volume,PalletType_Index,Pick_Method) "
            strSQL &= " values (@SKU_Index,@SKU_Id ,@product_Index,@size_Index,@package_Index,@unitWeight_Index,@color_Index,@model_Index,@brand_Index,@barcode1,@barcode2,@price1,@price2,@price3,@itemLife_y,@itemLife_m,@itemLife_d,@str1,@str2,@str3,@str10,@min_Qty,@min_Weight,@min_Volume,@max_Qty,@max_Weight,@max_Volume,@Qty_Per_Pallet,@add_by,getdate(),@add_branch,@isPlot,@isControlAsset,@Unit_Width,@Unit_Length,@Unit_Height,@Unit_Volume,@PalletType_Index,@Pick_Method)"


            strSQL = strSQL
            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@SKU_Index", SqlDbType.VarChar, 13).Value = Me._sku_Index
                .Parameters.Add("@SKU_Id", SqlDbType.VarChar, 50).Value = Me._sku_Id
                .Parameters.Add("@product_Index", SqlDbType.VarChar, 13).Value = _product_Index
                .Parameters.Add("@size_Index", SqlDbType.VarChar, 13).Value = Me._size_Index
                .Parameters.Add("@package_Index", SqlDbType.VarChar, 13).Value = Me._package_Index
                .Parameters.Add("@unitWeight_Index", SqlDbType.Float).Value = Me._unitWeight_Index
                .Parameters.Add("@color_Index", SqlDbType.VarChar, 13).Value = Me._color_Index
                .Parameters.Add("@model_Index", SqlDbType.VarChar, 13).Value = Me._model_Index
                .Parameters.Add("@brand_Index", SqlDbType.VarChar, 13).Value = Me._brand_Index
                .Parameters.Add("@barcode1", SqlDbType.VarChar, 50).Value = Me._barcode1
                .Parameters.Add("@barcode2", SqlDbType.VarChar, 50).Value = Me._barcode2

                .Parameters.Add("@price1", SqlDbType.Float).Value = _price1
                .Parameters.Add("@price2", SqlDbType.Float).Value = _price2
                .Parameters.Add("@price3", SqlDbType.Float).Value = _price3
                .Parameters.Add("@itemLife_y", SqlDbType.Int).Value = _itemLife_y
                .Parameters.Add("@itemLife_m", SqlDbType.Int).Value = _itemLife_m
                .Parameters.Add("@itemLife_d", SqlDbType.Int).Value = _itemLife_d

                .Parameters.Add("@str1", SqlDbType.NVarChar, 255).Value = _str1
                .Parameters.Add("@str2", SqlDbType.NVarChar, 255).Value = _str2
                .Parameters.Add("@str3", SqlDbType.NVarChar, 100).Value = _str3

                .Parameters.Add("@str10", SqlDbType.NVarChar, 100).Value = _str10


                .Parameters.Add("@min_Qty", SqlDbType.Float).Value = _min_Qty
                .Parameters.Add("@min_Weight", SqlDbType.Float).Value = _min_Weight
                .Parameters.Add("@min_Volume", SqlDbType.Float).Value = _min_Volume
                .Parameters.Add("@max_Qty", SqlDbType.Float).Value = _max_Qty
                .Parameters.Add("@max_Weight", SqlDbType.Float).Value = _max_Weight
                .Parameters.Add("@max_Volume", SqlDbType.Float).Value = _max_Volume

                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = Me._Qty_Per_Pallet
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID

                .Parameters.Add("@isPlot", SqlDbType.Bit).Value = isPlot
                .Parameters.Add("@isControlAsset", SqlDbType.Bit).Value = isControlAsset


                .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = _Unit_Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = _Unit_Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = _Unit_Height
                .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = _Unit_Volume
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _PalletType_Index
                .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = _Pick_Method

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            '***************
            Dim i As Integer = 0
            For i = 0 To dtDataSoruce_Sku.Rows.Count - 1
                'Dim PackageIndex As String = grd.Rows(i).Cells("ColumnPackageIndex").Value.ToString()
                'Dim Ratio As String = grd.Rows(i).Cells("ColumnRatio").Value.ToString()
                Dim PackageIndex As String = dtDataSoruce_Sku.Rows(i)("Package_Index").ToString()
                Dim Ratio As String = dtDataSoruce_Sku.Rows(i)("Ratio").ToString

                Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
                objDB.SaveData("", _sku_Index, PackageIndex, Val(Ratio))
            Next
            '***************



            Return True

        Catch ex As Exception
            Return False
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function

    Public Function InsertSKU_Transation() As Boolean
        Dim strSQL As String

        Try
            Dim IsNewCustomer As Boolean = _Customer_Index <> ""
            If IsNewCustomer Then
                If isExistID(Me._sku_Id, _Customer_Index) Then
                    Return False
                End If
            Else
                If isExistID(Me._sku_Id) Then
                    Return False
                End If
            End If


            If Trim(_sku_Id) = "" Then
                Dim objDocumentNumber As New Sy_AutoNumber
                While _sku_Id = ""
                    Dim temp_sku_id As String = objDocumentNumber.getSys_ID("sku_Id")
                    strSQL = " SELECT Count(*) As Count_ID FROM ms_SKU WHERE SKU_Id IN ('" & temp_sku_id.Trim & "') AND status_id <> -1 "
                    If DBExeQuery_Scalar(strSQL) = "0" Then
                        _sku_Id = temp_sku_id
                    End If
                End While
                objDocumentNumber = Nothing
            End If


            Dim objms_Product As New ms_Product_Update(ms_Product_Update.enuOperation_Type.ADDNEW)
            objms_Product.SaveData("", _sku_Id, _str1, _str2, _Product_Type, _package_Index, "0", IsNewCustomer)
            _product_Index = objms_Product.objProduct_index
            _Product_Type = objms_Product.ProductType_Index

            strSQL = " INSERT INTO ms_SKU (SKU_Index,SKU_Id,product_Index,size_Index,package_Index,unitWeight_Index,color_Index,model_Index,brand_Index,barcode1,barcode2,price1,price2,price3,itemLife_y,itemLife_m,itemLife_d,str1,str2,str3,str4,str5,str10,min_Qty,min_Weight,min_Volume,max_Qty,max_Weight,max_Volume,Qty_Per_Pallet,add_by,add_date,add_branch,isPlot,IsControlAsset,IsSerial,Unit_Width,Unit_Length,Unit_Height,Unit_Volume,PalletType_Index,Pick_Method,Currency_Index1,Currency_Index2,Currency_Index3,Customer_Index,Supplier_Index,Image_Path,Picking_Type,Item_Package_Index,Location_Index,ProductClass_Index,ProductSubClass_Index) "
            strSQL &= " VALUES (@SKU_Index,@SKU_Id ,@product_Index,@size_Index,@package_Index,@unitWeight_Index,@color_Index,@model_Index,@brand_Index,@barcode1,@barcode2,@price1,@price2,@price3,@itemLife_y,@itemLife_m,@itemLife_d,@str1,@str2,@str3,@str4,@str5,@str10,@min_Qty,@min_Weight,@min_Volume,@max_Qty,@max_Weight,@max_Volume,@Qty_Per_Pallet,@add_by,getdate(),@add_branch,@isPlot,@IsControlAsset,@IsSerial,@Unit_Width,@Unit_Length,@Unit_Height,@Unit_Volume,@PalletType_Index,@Pick_Method,@Currency_Index1,@Currency_Index2,@Currency_Index3,@Customer_Index,@Supplier_Index,@Image_Path,@Picking_Type,@Item_Package_Index,@Location_Index,@ProductClass_Index,@ProductSubClass_Index)"

            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@SKU_Index", SqlDbType.VarChar, 13).Value = Me._sku_Index
                .Parameters.Add("@SKU_Id", SqlDbType.VarChar, 50).Value = Me._sku_Id
                .Parameters.Add("@product_Index", SqlDbType.VarChar, 13).Value = _product_Index
                .Parameters.Add("@size_Index", SqlDbType.VarChar, 13).Value = Me._size_Index
                .Parameters.Add("@package_Index", SqlDbType.VarChar, 13).Value = Me._package_Index
                .Parameters.Add("@unitWeight_Index", SqlDbType.Float).Value = Me._unitWeight_Index
                .Parameters.Add("@color_Index", SqlDbType.VarChar, 13).Value = Me._color_Index
                .Parameters.Add("@model_Index", SqlDbType.VarChar, 13).Value = Me._model_Index
                .Parameters.Add("@brand_Index", SqlDbType.VarChar, 13).Value = Me._brand_Index
                .Parameters.Add("@barcode1", SqlDbType.VarChar, 50).Value = Me._barcode1
                .Parameters.Add("@barcode2", SqlDbType.VarChar, 50).Value = Me._barcode2

                .Parameters.Add("@price1", SqlDbType.Float).Value = _price1
                .Parameters.Add("@price2", SqlDbType.Float).Value = _price2
                .Parameters.Add("@price3", SqlDbType.Float).Value = _price3
                .Parameters.Add("@itemLife_y", SqlDbType.Int).Value = _itemLife_y
                .Parameters.Add("@itemLife_m", SqlDbType.Int).Value = _itemLife_m
                .Parameters.Add("@itemLife_d", SqlDbType.Int).Value = _itemLife_d

                .Parameters.Add("@str1", SqlDbType.VarChar, 500).Value = _str1
                .Parameters.Add("@str2", SqlDbType.VarChar, 500).Value = _str2
                .Parameters.Add("@str3", SqlDbType.VarChar, 500).Value = _str3
                .Parameters.Add("@str4", SqlDbType.VarChar, 100).Value = _str4
                .Parameters.Add("@str5", SqlDbType.VarChar, 100).Value = _str5
                .Parameters.Add("@str10", SqlDbType.VarChar, 100).Value = _str10

                .Parameters.Add("@min_Qty", SqlDbType.Float).Value = _min_Qty
                .Parameters.Add("@min_Weight", SqlDbType.Float).Value = _min_Weight
                .Parameters.Add("@min_Volume", SqlDbType.Float).Value = _min_Volume
                .Parameters.Add("@max_Qty", SqlDbType.Float).Value = _max_Qty
                .Parameters.Add("@max_Weight", SqlDbType.Float).Value = _max_Weight
                .Parameters.Add("@max_Volume", SqlDbType.Float).Value = _max_Volume

                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = Me._Qty_Per_Pallet
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName

                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID

                .Parameters.Add("@isPlot", SqlDbType.Bit).Value = isPlot
                .Parameters.Add("@isControlAsset", SqlDbType.Bit).Value = isControlAsset
                .Parameters.Add("@IsSerial", SqlDbType.Bit).Value = isControlAsset

                .Parameters.Add("@Currency_Index1", SqlDbType.VarChar, 13).Value = _Currency_Index1
                .Parameters.Add("@Currency_Index2", SqlDbType.VarChar, 13).Value = _Currency_Index2
                .Parameters.Add("@Currency_Index3", SqlDbType.VarChar, 13).Value = _Currency_Index3
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _Supplier_Index
                .Parameters.Add("@Image_Path", SqlDbType.VarChar, 500).Value = _Image_Path
                .Parameters.Add("@Picking_Type", SqlDbType.Int).Value = Picking_Type

                .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = _Unit_Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = _Unit_Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = _Unit_Height
                .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = _Unit_Volume
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _PalletType_Index
                .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _Item_Package_Index
                .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = _Pick_Method
                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = _Location_Index

                .Parameters.Add("@ProductClass_Index", SqlDbType.VarChar, 13).Value = _productClass
                .Parameters.Add("@ProductSubClass_Index", SqlDbType.VarChar, 13).Value = _productSubClass
            End With


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            '***************
            'Dim i As Integer = 0
            'For i = 0 To grd.Rows.Count - 1
            '    Dim PackageIndex As String = grd.Rows(i).Cells("ColumnPackageIndex").Value.ToString()
            '    Dim Ratio As String = grd.Rows(i).Cells("ColumnRatio").Value.ToString()

            '    Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
            '    objDB.SaveData("", _sku_Index, PackageIndex, Val(Ratio))
            'Next
            '***************

            Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
            objDB.SaveData("", _sku_Index, Me._package_Index, 1)

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
            strSQL = Nothing

        End Try
    End Function
#End Region

#Region " UPDATE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Private Function UpdateSKU_Master() As Boolean
        Dim strSQL As String = ""
        Try

            'strSQL = "  Update ms_SKURatio set Package_Index='" & Me._package_Index & "'"
            'strSQL &= "  where Sku_Index ='" & Me._sku_Index & "' and Package_Index in (select Package_Index from ms_SKU where Sku_Index ='" & Me._sku_Index & "')"

            strSQL &= " update ms_SKU set SKU_Id=@SKU_Id"
            strSQL &= ",product_Index = @product_Index"
            strSQL &= ",size_Index  =@size_Index"
            strSQL &= ",package_Index  = @package_Index"
            strSQL &= ",unitWeight_Index = @unitWeight_Index"
            strSQL &= ",color_Index = @color_Index"
            strSQL &= ",model_Index =@model_Index"
            strSQL &= ",brand_Index  = @brand_Index"
            strSQL &= ",barcode1  = @barcode1"
            strSQL &= ",barcode2  =@barcode2"
            strSQL &= ",price1  =@price1"
            strSQL &= ",price2  =@price2"
            strSQL &= ",price3  = @price3"
            strSQL &= ",itemLife_y  =@itemLife_y"
            strSQL &= ",itemLife_m  =@itemLife_m"
            strSQL &= ",itemLife_d  =@itemLife_d"
            strSQL &= ",str1  = @str1"
            strSQL &= ",str2  =@str2"
            strSQL &= ",str3  = @str3"
            strSQL &= ",str4  =@str4"
            strSQL &= ",str5 =@str5"
            strSQL &= ",str10  =@str10"
            strSQL &= ",min_Qty  =@min_Qty"
            strSQL &= ",min_Weight  =@min_Weight"
            strSQL &= ",min_Volume  =@min_Volume"
            strSQL &= ",max_Qty  =@max_Qty"
            strSQL &= ",max_Weight  =@max_Weight"
            strSQL &= ",max_Volume  = @max_Volume"
            strSQL &= ",Qty_Per_Pallet  =@Qty_Per_Pallet"
            strSQL &= ",isPlot  = @isPlot"
            strSQL &= ",isControlAsset  = @isControlAsset"
            strSQL &= ",IsSerial = @IsSerial"
            strSQL &= ",IsPick_PackRework = @IsPick_PackRework"
            strSQL &= ",IsBarcode_Process = @IsBarcode_Process"
            strSQL &= ",Picking_Type = @Picking_Type"
            strSQL &= "     ,Unit_Width=@Unit_Width"
            strSQL &= "     ,Unit_Length=@Unit_Length"
            strSQL &= "     ,Unit_Height=@Unit_Height"
            strSQL &= "     ,Unit_Volume=@Unit_Volume"
            strSQL &= "     ,PalletType_Index=@PalletType_Index"
            strSQL &= "     ,Pick_Method=@Pick_Method"
            strSQL &= ",Update_by =@Update_by"
            strSQL &= ",update_date=getdate() "
            strSQL &= ",update_branch= @update_branch"
            strSQL &= ",Currency_Index1= @Currency_Index1"
            strSQL &= ",Currency_Index2=@Currency_Index2"
            strSQL &= ",Currency_Index3=@Currency_Index3"
            strSQL &= ",Customer_Index=@Customer_Index"
            strSQL &= ",Supplier_Index= @Supplier_Index"
            strSQL &= ",Image_Path= @Image_Path"
            strSQL &= ",Item_Package_Index= @Item_Package_Index"
            strSQL &= ",Location_Index=@Location_Index"
            strSQL &= ",ProductClass_Index=@ProductClass_Index"
            strSQL &= ",ProductSubClass_Index=@ProductSubClass_Index"
            strSQL &= " WHERE SKU_Index=@SKU_Index"
            With SQLServerCommand

                .Parameters.Clear()
                .Parameters.Add("@SKU_Index", SqlDbType.VarChar, 13).Value = Me._sku_Index
                .Parameters.Add("@SKU_Id", SqlDbType.VarChar, 50).Value = Me._sku_Id
                .Parameters.Add("@product_Index", SqlDbType.VarChar, 13).Value = _product_Index
                .Parameters.Add("@size_Index", SqlDbType.VarChar, 13).Value = Me._size_Index
                .Parameters.Add("@package_Index", SqlDbType.VarChar, 13).Value = Me._package_Index
                .Parameters.Add("@unitWeight_Index", SqlDbType.VarChar, 13).Value = Me._unitWeight_Index
                .Parameters.Add("@color_Index", SqlDbType.VarChar, 13).Value = Me._color_Index
                .Parameters.Add("@model_Index", SqlDbType.VarChar, 13).Value = Me._model_Index
                .Parameters.Add("@brand_Index", SqlDbType.VarChar, 13).Value = Me._brand_Index
                .Parameters.Add("@barcode1", SqlDbType.VarChar, 50).Value = Me._barcode1
                .Parameters.Add("@barcode2", SqlDbType.VarChar, 50).Value = Me._barcode2

                .Parameters.Add("@price1", SqlDbType.Float).Value = _price1
                .Parameters.Add("@price2", SqlDbType.Float).Value = _price2
                .Parameters.Add("@price3", SqlDbType.Float).Value = _price3
                .Parameters.Add("@itemLife_y", SqlDbType.Int).Value = _itemLife_y
                .Parameters.Add("@itemLife_m", SqlDbType.Int).Value = _itemLife_m
                .Parameters.Add("@itemLife_d", SqlDbType.Int).Value = _itemLife_d
                .Parameters.Add("@str1", SqlDbType.VarChar, 255).Value = _str1
                .Parameters.Add("@str2", SqlDbType.VarChar, 255).Value = _str2
                .Parameters.Add("@str3", SqlDbType.VarChar, 100).Value = _str3
                .Parameters.Add("@str4", SqlDbType.VarChar, 2000).Value = _str4
                .Parameters.Add("@str5", SqlDbType.VarChar, 2000).Value = _str5
                .Parameters.Add("@str10", SqlDbType.VarChar, 100).Value = _str10
                .Parameters.Add("@min_Qty", SqlDbType.Float).Value = _min_Qty
                .Parameters.Add("@min_Weight", SqlDbType.Float).Value = _min_Weight
                .Parameters.Add("@min_Volume", SqlDbType.Float).Value = _min_Volume
                .Parameters.Add("@max_Qty", SqlDbType.Float).Value = _max_Qty
                .Parameters.Add("@max_Weight", SqlDbType.Float).Value = _max_Weight
                .Parameters.Add("@max_Volume", SqlDbType.Float).Value = _max_Volume
                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = Me._Qty_Per_Pallet
                .Parameters.Add("@Update_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@Update_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@isPlot", SqlDbType.Bit).Value = isPlot
                .Parameters.Add("@isControlAsset", SqlDbType.Bit).Value = _isControlAsset
                .Parameters.Add("@IsSerial", SqlDbType.Bit).Value = _isControlAsset

                .Parameters.Add("@IsPick_PackRework", SqlDbType.Bit).Value = _isControlPack
                .Parameters.Add("@IsBarcode_Process", SqlDbType.Bit).Value = _PrintBarcode
                '.Parameters.Add("@IsBarcode_Process", SqlDbType.Bit).Value = _PackingType

                .Parameters.Add("@Currency_Index1", SqlDbType.VarChar, 13).Value = _Currency_Index1
                .Parameters.Add("@Currency_Index2", SqlDbType.VarChar, 13).Value = _Currency_Index2
                .Parameters.Add("@Currency_Index3", SqlDbType.VarChar, 13).Value = _Currency_Index3
                .Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                .Parameters.Add("@Supplier_Index", SqlDbType.VarChar, 13).Value = _Supplier_Index
                .Parameters.Add("@Image_Path", SqlDbType.VarChar, 500).Value = _Image_Path
                .Parameters.Add("@Picking_Type", SqlDbType.Int).Value = _PackingType
                .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = _Unit_Width
                .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = _Unit_Length
                .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = _Unit_Height
                .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = _Unit_Volume
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar, 13).Value = _PalletType_Index
                .Parameters.Add("@Item_Package_Index", SqlDbType.VarChar, 13).Value = _Item_Package_Index
                .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = _Pick_Method
                .Parameters.Add("@Location_Index", SqlDbType.VarChar, 13).Value = _Location_Index
                .Parameters.Add("@ProductClass_Index", SqlDbType.VarChar, 13).Value = _productClass
                .Parameters.Add("@ProductSubClass_Index", SqlDbType.VarChar, 13).Value = _productSubClass
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()
            'update Product dong ADD

            Dim objms_Product As New ms_Product(ms_Product.enuOperation_Type.UPDATE)
            objms_Product.SaveData(Me._product_Index, Me._sku_Id, Me._str1, Me._str2, Me._Product_Type, Me._package_Index, "0")




            '*****************************
            'Dim objDBDelete As New ms_SKURatio(ms_SKURatio.enuOperation_Type.DELETE)
            'If objDBDelete.Delete_AllRatio(_sku_Index) Then

            '    Dim e As Integer = 0
            '    For e = 0 To grd.Rows.Count - 1
            '        Dim PackageIndexE As String = grd.Rows(e).Cells("ColumnPackageIndex")..ToString()
            '        Dim RatioE As String = grd.Rows(e).Cells("ColumnRatio")..ToString()

            '        Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
            '        objDB.SaveData("", _sku_Index, PackageIndexE, Val(RatioE))
            '    Next

            'End If
            '*****************************

            '***************
            '---Update  grdRatio ----'
            Dim i As Integer = 0
            For i = 0 To dtDataSoruce_Sku.Rows.Count - 1
                With dtDataSoruce_Sku.Rows(i)
                    Dim PackageIndex As String = .Item("Package_Index").ToString()
                    Dim Ratio As Double = CDbl(.Item("Ratio"))
                    Dim strPackage_ID As String = .Item("Description").ToString()
                    Dim strDescription As String = .Item("Description").ToString()
                    Dim dblDimension_Hi As Double
                    If .Item("Dimension_Hi").ToString() = "" Then
                        dblDimension_Hi = 0
                    Else
                        dblDimension_Hi = .Item("Dimension_Hi").ToString()
                    End If
                    Dim dblDimension_Wd As Double
                    If .Item("Dimension_Wd").ToString() = "" Then
                        dblDimension_Wd = 0
                    Else
                        dblDimension_Wd = .Item("Dimension_Wd").ToString()
                    End If
                    Dim dblDimension_Len As Double
                    If .Item("Dimension_Len").ToString() = "" Then
                        dblDimension_Len = 0
                    Else
                        dblDimension_Len = .Item("Dimension_Len").ToString()
                    End If

                    Dim dblWeight As Double
                    If .Item("Weight").ToString() = "" Then
                        dblWeight = 0
                    Else
                        dblWeight = .Item("Weight").ToString()
                    End If
                    Dim Barcode As String = .Item("Barcode").ToString()
                    Dim DimensionType_Index As String = .Item("DimensionType_Index").ToString()

                    'Package 
                    Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.UPDATE)
                    objms_Package.SaveData(PackageIndex, strPackage_ID, strDescription, dblDimension_Hi, dblDimension_Wd, dblDimension_Len, 0, dblWeight, Barcode, DimensionType_Index) ', txtUnit_id.Text

                    Dim SKURatio_Index As String = .Item("SkuRatio_Index").ToString()
                    'Ratio
                    Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.UPDATE)
                    objDB.SaveData(SKURatio_Index, _sku_Index, PackageIndex, Ratio)
                End With

            Next
            '***************


            Return True
        Catch ex As Exception

            Throw ex
            Return False
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function


    Public Function UpdateIscontrolPackAndBarcode(ByVal pstrSku_index As String, ByVal iIsControlPack As Integer, ByVal iIsPrintBarcode As Integer) As Boolean
        Dim strSQL As String = ""

        Try
            strSQL = " Update ms_SKU set"
            strSQL &= " IsControlPack = " & iIsControlPack
            strSQL &= " ,IsPrintBarcode =  " & iIsPrintBarcode
            strSQL &= " where Sku_Index = '" & pstrSku_index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

            Return True

        Catch ex As Exception
            Throw ex
            Return False
        Finally
            disconnectDB()
            strSQL = Nothing
        End Try
    End Function
#End Region
#Region " DELETE DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
    Public Function DeleteSKU_Master(ByVal SKU_Index As String) As Boolean
        ' *** Define value from parameter
        Me._sku_Index = SKU_Index

        Dim strSQL As String

        Try
            'strSQL = " Delete  " & _
            '         " From ms_SKU " & _
            '         " WHERE SKU_Index='" + Me._sku_Index + "' "

            strSQL = "UPDATE ms_SKU SET status_id = -1"
            strSQL &= ", update_by = '" + WV_UserName + "'"
            strSQL &= ", update_date=getdate() "
            strSQL &= ", update_branch= " & WV_Branch_ID & " "
            strSQL &= " WHERE SKU_Index='" + Me._sku_Index + "' "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            connectDB()
            EXEC_Command()

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
#Region " CHECK DATA "
    '***
    '*** Create By  : Paponshet ; [09/01/2008] ; ??s
    '*** Return : ??
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

    Private Function isExistID(ByVal _SKU_Id As String, ByVal _Customer_Index As String) As Boolean
        Dim strSQL As String
        Try

            strSQL = "select count(*) from ms_SKU where SKU_Id = @SKU_Id AND Customer_Index = @Customer_Index AND status_id <> -1"
            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@SKU_Id", SqlDbType.NVarChar, 50).Value = _SKU_Id
            SQLServerCommand.Parameters.Add("@Customer_Index", SqlDbType.NVarChar, 13).Value = _Customer_Index

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

    Public Function isPlot_Value(ByVal Sku_Index As String) As Boolean
        ' *** Function for check isPlot *** 
        Dim strSQL As String
        Dim strWhere As String
        Dim TotalDay As Integer = 0
        Try

            strSQL = " SELECT     isPlot   " & _
                     " FROM       ms_SKU   "

            strWhere = ""

            strWhere += " WHERE  ms_SKU.SKU_Index = '" & Sku_Index & "' and ms_SKU.status_id != -1"

            SetSQLString = strSQL + strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return DataTable.Rows(0).Item("isPlot")
            Else
                Return False
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function


    Public Function chkSKU_USED(ByVal _pSKU_Index As String) As Boolean
        Dim strSQL As String = ""
        Try
            strSQL &= " DECLARE @Sku_Index Varchar(13)"
            strSQL &= " SET @Sku_Index = '" & _pSKU_Index & "'"
            strSQL &= " SELECT *"
            strSQL &= " FROM("
            strSQL &= " SELECT "
            '************************* -- รับ"
            strSQL &= " Receive = (select isnull(sum(OI.Total_Qty),0) AS Receive"
            strSQL &= " from tb_Order O inner join"
            strSQL &= " 	 tb_OrderItem OI ON O.Order_Index = OI.Order_Index inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = OI.Sku_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND SKU.Sku_Index =@Sku_Index)"
            '************************* -- เบิก"
            strSQL &= " ,WithDraw = (select isnull(sum(OI.Total_Qty),0) AS WithDraw"
            strSQL &= " from tb_WithDraw O inner join"
            strSQL &= " 	 tb_WithDrawItem OI ON O.WithDraw_Index = OI.WithDraw_Index inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = OI.Sku_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND SKU.Sku_Index =@Sku_Index)"
            '************************* -- โอน"
            strSQL &= " ,Transfer = (select isnull(sum(OI.Total_Qty),0) AS Transfer"
            strSQL &= " from tb_TransferStatus O inner join"
            strSQL &= " 	 tb_TransferStatusLocation OI ON O.TransferStatus_Index = OI.TransferStatus_Index inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = OI.Sku_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND SKU.Sku_Index =@Sku_Index)"
            '************************* -- ยืม"
            strSQL &= " ,Borrow = (select isnull(sum(OI.Total_Qty),0) AS Borrow"
            strSQL &= " from tb_Borrow O inner join"
            strSQL &= " 	 tb_BorrowLocation OI ON O.Borrow_Index = OI.Borrow_Index inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = OI.Sku_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND SKU.Sku_Index =@Sku_Index)"
            '************************* -- คืน"
            strSQL &= " ,BorrowReturn = (select isnull(sum(OI.Total_Qty),0) AS BorrowReturn"
            strSQL &= " from tb_BorrowReturn O inner join"
            strSQL &= " 	 tb_BorrowReturnLocation OI ON O.BorrowReturn_Index = OI.BorrowReturn_Index inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = OI.Sku_Index"
            strSQL &= " Where O.Status not in (-1,2)"
            strSQL &= " 	AND SKU.Sku_Index =@Sku_Index)"
            '************************* -- คงเหลือ"
            strSQL &= " ,Balance=(select isnull(sum(LB.Qty_Bal),0) as Qty_Bal"
            strSQL &= " from tb_LocationBalance LB inner join"
            strSQL &= " 	 ms_Sku SKU ON SKU.Sku_Index = LB.Sku_Index"
            strSQL &= "            Where(LB.Qty_Bal > 0)"
            strSQL &= " 	AND SKU.Sku_Index =@Sku_Index)"
            strSQL &= " ) CHKSKU_USED"
            strSQL &= " WHERE (Receive > 0) or (WithDraw > 0) or (Transfer > 0) or (Borrow > 0) or (BorrowReturn > 0) or (Balance > 0)"

            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Sku_Index", SqlDbType.VarChar, 50).Value = _pSKU_Index

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
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
    Public Function SaveData(ByVal _SKU_Index As String, ByVal _SKU_Id As String, ByVal _product_Index As String, ByVal _size_Index As String, ByVal _package_Index As String, ByVal _unitWeight_Index As String, ByVal _color_Index As String, ByVal _model_Index As String, ByVal _brand_Index As String, ByVal _barcode1 As String, ByVal _barcode2 As String, ByVal _price1 As Double, ByVal _price2 As Double, ByVal _price3 As Double, ByVal _itemLife_y As Integer, ByVal _itemLife_m As Integer, ByVal _itemLife_d As Integer, ByVal name_thai As String, ByVal name_eng As String, ByVal sku_des As String, ByVal _min_Qty As Double, ByVal _min_Weight As Double, ByVal _min_Volume As Double, ByVal _max_Qty As Double, ByVal _max_Weight As Double, ByVal _max_Volume As Double, _
                                ByVal _isPlot As Integer, ByVal _isControlAsset As Integer, ByVal _IsControlPack As Integer, ByVal _PrintBarcode As Integer, ByVal _PackingType As Integer, ByVal Qty_Per_Pallet As Double, ByVal _VolumeX As String, ByVal _VolumeY As String, ByVal _VolumeZ As String, ByVal _Volume As String, ByVal _CustomerRefCode As String, ByVal _SupplierRefCode As String, ByVal CurrencyIndex1 As String, ByVal CurrencyIndex2 As String, ByVal CurrencyIndex3 As String, ByVal CustomerIndex As String, ByVal SupplierIndex As String, ByVal ImagePath As String, ByVal Picking_Type As Integer, ByVal Item_Package_Index As String, ByVal Location_Index As String, ByVal Product_Class As String, ByVal Product_SubClass As String, Optional ByRef dtDataSoruce_Sku As DataTable = Nothing _
                                ) As Boolean

        ' ***  define value to field ***
        Me._sku_Index = _SKU_Index

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
        Me.dtDataSoruce_Sku = dtDataSoruce_Sku

        Me._Unit_Width = _VolumeX
        Me._Unit_Length = _VolumeY
        Me._Unit_Height = _VolumeZ
        Me._Unit_Volume = _Volume
        '  Me._PalletType_Index = 1
        ' Me._Pick_Method = 1

        Me._isPlot = _isPlot
        Me._isControlAsset = _isControlAsset
        Me._isControlPack = _IsControlPack
        Me._PrintBarcode = _PrintBarcode
        Me._PackingType = _PackingType
        Me._Currency_Index1 = CurrencyIndex1
        Me._Currency_Index2 = CurrencyIndex2
        Me._Currency_Index3 = CurrencyIndex3
        Me._Customer_Index = CustomerIndex
        Me._Supplier_Index = SupplierIndex
        Me._Image_Path = ImagePath
        Me._Picking_Type = Picking_Type
        Me._Location_Index = Location_Index

        Me._productClass = Product_Class
        Me._productSubClass = Product_SubClass

        Me._Item_Package_Index = Item_Package_Index

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                ' get New Sys_Value 
                'Dim objDBIndex As New Sy_AutoNumber
                'Me._sku_Index = objDBIndex.getSys_Value("SKU_Index")
                'objDBIndex = Nothing

            Case enuOperation_Type.UPDATE
                ' Assign value from parameter
                'Me._SKU_Id = _SKU_Index
                Me._sku_Index = _SKU_Index
        End Select

        '****************Pui*************
        'If _ProductSku_Type = "1" Then
        '    Exit Function
        'End If

        ' *******************************
        Try

            ' *** check  Operation 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    '  *** check duplicate id 
                    'แก้ตรงนี้ไป
                    If isExistID(_SKU_Id, _Customer_Index) = True Then
                        ' Cannot Save data
                        '    MessageBox.Show("รหัสซ้ำ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Return False

                        '  Exit Function
                    Else

                        ' ไม่ใช้ เพราะสร้าง ฟังชั่นใหม่แล้ว 
                        '
                        '    ' Can Save data
                        '    If Me.InsertSKU_Master = True Then
                        '        '      MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '        Return True
                        '        Exit Function
                        '    Else
                        '        '   MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '        Return False
                        '        Exit Function
                        '    End If
                    End If


                Case enuOperation_Type.UPDATE
                    ' **** update value 
                    If Me.UpdateSKU_Master = True Then
                        '  MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return True
                        '    Exit Function
                    Else
                        'MessageBox.Show("ไม่สามารถบันทึกข้อมูล", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Return False
                        Exit Function
                    End If

            End Select

        Catch ex As Exception
            Return False
            Throw ex
        End Try


    End Function
#End Region
    Public Function isExistID_CusRefId(ByVal _Customer_Index As String, ByVal _Sku_Index As String) As Boolean
        Dim strSQL As String
        Try
            strSQL = "  SELECT  count(*) "
            strSQL &= " FROM    ms_Customer_SKU_RefId "
            strSQL &= " WHERE   Customer_Index='" & _Customer_Index & "'"
            strSQL &= "         AND  Sku_Index='" & _Sku_Index & "'"


            SQLServerCommand.Parameters.Clear()
            SQLServerCommand.Parameters.Add("@Customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
            SQLServerCommand.Parameters.Add("@Sku_Index", SqlDbType.VarChar, 13).Value = _Sku_Index
            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            ' connectDB()
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
    Public Function InsertSku_CusRefId(ByVal pSku_Index As String _
          , ByVal pCustomer_Index As String _
          , ByVal pRefId As String _
          , ByVal pStr1 As String, ByVal pStr2 As String, ByVal pStr3 As String, ByVal pStr4 As String, ByVal pStr5 As String _
          , ByVal pFlo1 As Double, ByVal pFlo2 As Double, ByVal pFlo3 As Double, ByVal pFlo4 As Double, ByVal pFlo5 As Double) As Boolean

        Dim strSQL As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Try
            If isExistID_CusRefId(pCustomer_Index, pSku_Index) Then

                strSQL = " UPDATE ms_Customer_SKU_RefId "
                strSQL &= " SET  Customer_Index=@Customer_Index,RefId=@RefId "
                strSQL &= " ,Str1=@Str1,Str2=@Str2,Str3=@Str3,Str4=@Str4,Str5=@Str5"
                strSQL &= " ,Flo1=@Flo1,Flo2=@Flo2,Flo3=@Flo3,Flo4=@Flo4,Flo5=@Flo5"
                strSQL &= " ,status_id=@status_id"
                strSQL &= " WHERE Sku_Index=@Sku_Index "
                strSQL &= " AND  Customer_Index=@Customer_Index "

            Else
                strSQL = " insert into ms_Customer_SKU_RefId (Sku_Index,Customer_Index,RefId "
                strSQL &= " ,Str1,Str2,Str3,Str4,Str5,Flo1,Flo2,Flo3,Flo4,Flo5"
                strSQL &= " ,status_id)"
                strSQL &= " values (@Sku_Index,@Customer_Index,@RefId"
                strSQL &= " ,@Str1,@Str2,@Str3,@Str4,@Str5,@Flo1,@Flo2,@Flo3,@Flo4,@Flo5"
                strSQL &= " ,@status_id)"

                If pRefId = "" Then Exit Function

            End If


            With SQLServerCommand.Parameters
                'gSB_GetDBServerDateTime()
                .Clear()
                .Add("@Sku_Index", SqlDbType.VarChar, 30).Value = pSku_Index
                .Add("@Customer_Index", SqlDbType.VarChar, 30).Value = pCustomer_Index

                If pRefId = "" Then
                    .Add("@RefId", SqlDbType.NVarChar, 100).Value = DBNull.Value
                Else
                    .Add("@RefId", SqlDbType.NVarChar, 100).Value = pRefId
                End If

                .Add("@Str1", SqlDbType.NVarChar, 100).Value = pStr1
                .Add("@Str2", SqlDbType.NVarChar, 100).Value = pStr2
                .Add("@Str3", SqlDbType.NVarChar, 100).Value = pStr3
                .Add("@Str4", SqlDbType.NVarChar, 100).Value = pStr4
                .Add("@Str5", SqlDbType.NVarChar, 100).Value = pStr5
                .Add("@Flo1", SqlDbType.Float, 15).Value = pFlo1
                .Add("@Flo2", SqlDbType.Float, 15).Value = pFlo2
                .Add("@Flo3", SqlDbType.Float, 15).Value = pFlo3
                .Add("@Flo4", SqlDbType.Float, 15).Value = pFlo4
                .Add("@Flo5", SqlDbType.Float, 15).Value = pFlo5
                .Add("@status_id", SqlDbType.Int, 4).Value = 1

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            '   connectDB()
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
    Public Sub getCustomer_SKU_RefId(ByVal pCustomer_Index As String, ByVal pSku_Index As String, ByVal WhereString As String)
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            ' Select No Condition 
            strSQL = "  DECLARE @CusRef TABLE ("
            strSQL &= "             Customer_SKU_RefId  varchar(50)"
            strSQL &= " 			,Sku_Index	varchar(13)"
            strSQL &= " 			,Sku_Name		varchar(500)"
            strSQL &= " 			,Sku_Id		varchar(50)"
            strSQL &= " 			,RefID		varchar(50)"
            strSQL &= " 			,RefID2		varchar(50)"
            strSQL &= " 			,Customer_Id		varchar(50)"
            strSQL &= " 			,Customer_Name		varchar(500)"
            strSQL &= " 			,Customer_Index		varchar(13))"

            strSQL &= " DECLARE @Customer_SKU_RefId	varchar(50)"
            strSQL &= " DECLARE @Sku_Index	varchar(13)"
            strSQL &= " DECLARE @Sku_Name		varchar(500)"
            strSQL &= " DECLARE @Sku_Id		varchar(50)"
            strSQL &= " DECLARE @RefID						varchar(50)"
            strSQL &= " DECLARE @RefID2						varchar(50)"
            strSQL &= " DECLARE @Customer_Id				varchar(50)"
            strSQL &= " DECLARE @Customer_Name				varchar(500)"
            strSQL &= " DECLARE @Customer_Index				varchar(13)"
            strSQL &= " DECLARE @chkValue				varchar(500)"

            If pSku_Index <> "" Then strSQL &= " SET @Sku_Index = '" & pSku_Index & "'"

            If pCustomer_Index <> "" Then strSQL &= " SET @Customer_Index = '" & pCustomer_Index & "'"



            strSQL &= " 	DECLARE CurSku CURSOR FOR"
            strSQL &= " 	SELECT	Sku_Index,Str1 as Sku_Name,Sku_Id as Sku_Id"
            strSQL &= " 	FROM	ms_Sku "
            strSQL &= " 	WHERE	Status_Id <> -1"

            If pSku_Index <> "" Then strSQL &= " 	AND	Sku_Index = @Sku_Index "

            strSQL &= " 				OPEN CurSku"
            strSQL &= " 			    FETCH NEXT FROM CurSku"
            strSQL &= " 				INTO @Sku_Index,@Sku_Name,@Sku_Id"
            strSQL &= " 				WHILE (@@FETCH_STATUS = 0) "
            strSQL &= " 				Begin"
            '---- BEGIN LOOP CUTOMER
            strSQL &= " 						DECLARE CurCustomer CURSOR FOR"
            strSQL &= " 						SELECT	Customer_Index,Customer_Id,Customer_Name"
            strSQL &= " 						FROM	ms_Customer"
            strSQL &= " 						WHERE	Status_Id <> -1"

            If pCustomer_Index <> "" Then strSQL &= " 	AND	Customer_Index = @Customer_Index "


            strSQL &= " 						OPEN CurCustomer"
            strSQL &= " 						FETCH NEXT FROM CurCustomer"
            strSQL &= " 						INTO @Customer_Index,@Customer_Id,@Customer_Name"
            strSQL &= " 						WHILE (@@FETCH_STATUS = 0) "
            strSQL &= " 						Begin"
            strSQL &= " 							SET @chkValue = (select	top 1 Customer_SKU_RefId "
            strSQL &= " 																			from	ms_Customer_SKU_RefId "
            strSQL &= " 																			where	Sku_Index=@Sku_Index"
            strSQL &= " 																			And		Customer_Index=@Customer_Index)"
            strSQL &= " 							SET @RefID = (select top 1 	RefID"
            strSQL &= " 																			from	ms_Customer_SKU_RefId "
            strSQL &= " 																			where	Sku_Index=@Sku_Index"
            strSQL &= " 																			And		Customer_Index=@Customer_Index)"
            strSQL &= " 							Insert Into @CusRef"
            strSQL &= " 							SELECT			Customer_SKU_RefId = @chkValue"
            strSQL &= " 											,@Sku_Index"
            strSQL &= " 											,@Sku_Name"
            strSQL &= " 											,@Sku_Id"
            strSQL &= " 											,RefID =  isnull(@RefID,'')"
            strSQL &= " 											,RefID2 =  isnull(@RefID,@Sku_Id)"
            strSQL &= " 											,Customer_Id =  @Customer_Id"
            strSQL &= " 											,Customer_Name = @Customer_Name"
            strSQL &= " 											,Customer_Index = @Customer_Index"
            strSQL &= " 							FROM       ms_Sku"
            strSQL &= " 							WHERE	Status_Id <> -1"
            strSQL &= " 									AND  Sku_Index=@Sku_Index"
            strSQL &= " 							FETCH NEXT FROM CurCustomer"
            strSQL &= " 							INTO  @Customer_Index,@Customer_Id,@Customer_Name"
            strSQL &= "                                 End"
            strSQL &= " 						CLOSE CurCustomer"
            strSQL &= " 						DEALLOCATE CurCustomer "
            '---- END LOOP CUTOMER"
            strSQL &= " 					FETCH NEXT FROM CurSku"
            strSQL &= " 					INTO @Sku_Index,@Sku_Name,@Sku_Id"
            strSQL &= "                                End"

            strSQL &= " 				CLOSE CurSku"
            strSQL &= " 				DEALLOCATE CurSku "


            strSQL &= " SELECT * FROM @CusRef ORDER BY Isnull(Customer_SKU_RefId,999999999)"




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

    Public Function CheckQtyBalance(ByVal sku_id As String) As Boolean
        Dim strSQL As String = ""
        strSQL &= "SELECT SUM(Qty_Bal) As Qty_Bal,ms_SKU.IsSerial FROM tb_LocationBalance"
        strSQL &= " INNER JOIN ms_SKU ON tb_LocationBalance.Sku_Index = ms_SKU.Sku_Index"
        strSQL &= " WHERE tb_LocationBalance.Sku_Index = (SELECT Sku_Index FROM ms_SKU "
        strSQL &= " WHERE Sku_Id = '" & sku_id & "' AND status_id <> -1)"
        strSQL &= " GROUP By tb_LocationBalance.Sku_Index,ms_SKU.IsSerial"
        Dim dt As DataTable = DBExeQuery(strSQL)
        If dt.Rows.Count > 0 Then
            If dt.Rows(0).Item("IsSerial") = 0 Then
                If dt.Rows(0).Item("Qty_Bal") > 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return True
            End If
        Else
            Return True
        End If
    End Function
End Class