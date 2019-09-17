Imports WMS_STD_Formula

Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms
Imports System.Configuration.ConfigurationSettings
Imports System.Drawing
Imports System.IO
Imports WMS_STD_Master


Public Class frmProductSKU_Des

    Public _strtxtMainPackage As String = ""
    Private _Package_Index As String
    Private _Product_Index As String
    Private _DimenstionRatio As Double = 1
    Private _DimenstionRatioPakcage As Double = 1
    Private _Digit As Integer = 4
    Private _DigitPackage As Integer = 4
    Private _isItem_Package As Integer = 0
    Private _isPic As Boolean = False

    Private _USE_Warehouse_NEW_VERSION As String = ""
    Public Property USE_Warehouse_NEW_VERSION() As String
        Get
            Return _USE_Warehouse_NEW_VERSION
        End Get
        Set(ByVal value As String)
            _USE_Warehouse_NEW_VERSION = value
        End Set
    End Property

    Public Property isItem_Package() As Integer
        Get
            Return _isItem_Package
        End Get
        Set(ByVal value As Integer)
            _isItem_Package = value
        End Set
    End Property


    Private _Item_Package_Index As String = ""
    Public Property Item_Package_Index() As String
        Get
            Return _Item_Package_Index
        End Get
        Set(ByVal value As String)
            _Item_Package_Index = value
        End Set
    End Property

    Public ReadOnly Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
    End Property

    Private _DEFAULT_ImagePath As String = ""

    Public Property DEFAULT_ImagePath() As String
        Get
            Return _DEFAULT_ImagePath
        End Get
        Set(ByVal value As String)
            _DEFAULT_ImagePath = value
        End Set
    End Property

    Private _Sku_Index As String

    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal value As String)
            _Sku_Index = value
        End Set
    End Property
    Public Property Product_Index() As String
        Get
            Return _Product_Index
        End Get
        Set(ByVal value As String)
            _Product_Index = value
        End Set
    End Property
    Private _Config As Boolean
    Public Property Config() As Boolean
        Get
            Return _Config
        End Get
        Set(ByVal value As Boolean)
            _Config = value
        End Set
    End Property
    Public SaveType As Integer = 0  ' 0 = Add / 1 = Edit
    Public ProductRatio_Type As String = "0"

    ' Variables for product images.
    Private gstrFileName As String = ""
    Private gstrLongFilePath As String = ""
    Private gstrAppPath As String = ""

    Private _Ration_Package As String = ""
    Private _NewPackage_Index As String = ""
    Private _Ratio_Index As String = ""

    ' Dong_kk Update 2010/06/21
    Private _OldSku_Id As String = ""

#Region "FORM LOAD"
    Private Sub frmProductEx_Des_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2001)
            oFunction.SW_Language_Column(Me, Me.grdSKU_Package, 2001)

            Me.txtCustomer_Name.Tag = ""

            Dim objSkuConfigDB As New config_CustomSetting
            If objSkuConfigDB.getConfig_Key_USE("USE_PRODUCT_CUSTOMER") Then
                Me.lblCustomer.Visible = True
                Me.txtCustomer_Name.Visible = True
                Me.btnSeachCustomer.Visible = True

            Else
                Me.lblCustomer.Visible = False
                Me.txtCustomer_Name.Visible = False
                Me.btnSeachCustomer.Visible = False
            End If
            If objSkuConfigDB.getConfig_Key_USE("USE_DEFAULT_LOCATION_SKU") Then
                Me.lblLocation.Visible = True
                Me.txtLocation.Visible = True
                Me.btnLocation.Visible = True
            Else
                Me.lblLocation.Visible = False
                Me.txtLocation.Visible = False
                Me.btnLocation.Visible = False
            End If



            Me.OpenFileDialog1.Filter = "Bmp files (*.bmp)|*.bmp|" + "Gif files (*.gif)|*.gif|" + "Jpeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Png files (*.png)|*.png|" + "All graphic files (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"

            ' ----- Set combobox values
            cbPickingType.SelectedIndex = 0
            'me._Sku_index = Me.Sku_Index
            AddcbColor()
            AddcbModel()
            AddcbBrand()
            AddcbType()
            ' AddcbMainPackage()
            Addcbsize()
            AddcbCurrency1()
            AddcbCurrency2()
            AddcbCurrency3()
            AddcbGroupSku()
            AddcbPalletType()
            ' Killz Add 
            AddcboDimensionType()
            AddcboDimensionType_Ratio()

            SetProductClass()
            SetProductSubClassByIndex()

            setSKU_ITEM_QTY()
            SetProductRatio() ' Check mode to auto add Product when new SKU is added?
            USE_DEFAULT_LOCATION_SKU()
            ' Check if this is Adding or Editing
            Select Case SaveType
                Case 0
                    'Dim objDBIndex As New Sy_AutoNumber
                    'Me.txtSKUID.Text = objDBIndex.getSys_ID("SKU_ID")
                    'objDBIndex = Nothing

                    ' txtMainPackage.Text = cbMainPackage.Text
                    Me._Sku_Index = ""
                    Me.picSKU.Tag = ""

                Case 1
                    LoadDataEdit()
                    ShowgrdSKU_Package(Me._Sku_Index)
                    'txtSKUID.ReadOnly = False   ' In case of Editing, we don't allow to modify SKU ID
                    txtProduct.ReadOnly = True
                    txtSKUID.BackColor = Color.Gainsboro
                    txtProduct.BackColor = Color.Gainsboro

            End Select

            Me.SetImage_Path()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try '
    End Sub

    Private Sub SetProductClass()
        Dim ProductClass As ms_Product_Class
        Try
            ProductClass = New ms_Product_Class(ms_Product_Class.enuOperation_Type.SEARCH)
            ProductClass.SearchData_Click("", "")

            Dim Row As DataRow = ProductClass.DataTable.NewRow

            With Row
                .Item("ProductClass_ID") = "--- ไม่ระบุ ---"
                .Item("ProductClass_Index") = String.Empty
                .Item("Customer_Index") = String.Empty
            End With

            With ProductClass
                .DataTable.Rows.InsertAt(Row, 0)
                '.DataTable.DefaultView.RowFilter = "customer_index = '" & Me.txtCustomer_Name.Tag.ToString & "'"
                '.DataTable = objDT.DefaultView.Table
                '.DataTable.AcceptChanges()
            End With

            With Me.cbProductClass
                .DisplayMember = "ProductClass_ID"
                .ValueMember = "ProductClass_Index"
                .DataSource = ProductClass.DataTable

                If .Items.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

        Catch Ex As Exception
            Throw Ex
        Finally
            ProductClass = Nothing
        End Try
    End Sub

    Private Sub SetProductSubClassByIndex(Optional ByVal pIndex As String = "")
        Dim ProductSubClass As ms_Product_SubClass
        Try
            ProductSubClass = New ms_Product_SubClass(ms_Product_SubClass.enuOperation_Type.SEARCH)
            ProductSubClass.SearchData_Index("", "", pIndex)

            Dim Row As DataRow = ProductSubClass.DataTable.NewRow

            With Row
                .Item("ProductSubClass_ID") = "--- ไม่ระบุ ---"
                .Item("ProductSubClass_Index") = String.Empty
            End With

            ProductSubClass.DataTable.Rows.InsertAt(Row, 0)

            With Me.cbProductSubClass
                .DisplayMember = "ProductSubClass_ID"
                .ValueMember = "ProductSubClass_Index"
                .DataSource = ProductSubClass.DataTable

                If .Items.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

        Catch Ex As Exception
            Throw Ex
        Finally
            ProductSubClass = Nothing
        End Try
    End Sub

    Private Sub setSKU_ITEM_QTY()
        Try
            Dim objCustomSetting As New config_CustomSetting
            Dim objDT As DataTable = New DataTable
            If objCustomSetting.getConfig_Key_USE("USE_SKU_ITEM_QTY") = False Then
                lblItem_Packge.Visible = False
                txtItem_Package.Visible = False
                btnItem_Packge.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub USE_DEFAULT_LOCATION_SKU()
        Try
            Dim objCustomSetting As New config_CustomSetting
            Dim objDT As DataTable = New DataTable
            If objCustomSetting.getConfig_Key_USE("USE_DEFAULT_LOCATION_SKU") = False Then
                lblLocation.Visible = False
                txtLocation.Visible = False
                btnLocation.Visible = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' Get setting variables from config_CustomSetting
    ''' This value will determine if the system will auto add ms_Product when adding ms_SKU?
    ''' Now, the default should be "1".
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub SetProductRatio()
        Try
            ' TODO: Should not use SQL command in the form.
            Dim objDB As New SQLCommands
            objDB.SQLComand("SELECT Config_Value FROM config_CustomSetting WHERE Config_Key = 'Product_SkuRatio'")

            If objDB.DataTable.Rows.Count > 0 Then
                ProductRatio_Type = objDB.DataTable.Rows(0).Item("Config_Value").ToString()
                Select Case ProductRatio_Type
                    Case "1"
                        'cbType.Visible = True
                        'lblType.Visible = True
                        btnGetProduct.Visible = False
                        txtProduct.Visible = False
                        lblProduct.Visible = False

                    Case "0"
                        btnGetProduct.Visible = True
                        txtProduct.Visible = True
                        lblProduct.Visible = True
                End Select
            End If

        Catch ex As Exception
            Throw ex
        End Try '
    End Sub

    Sub SetImage_Path()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Dim appSet As New Configuration.AppSettingsReader()
            Dim strUseLocalPath As String = appSet.GetValue("UseLocalPath", GetType(String))


            If strUseLocalPath = 0 Then
                Me._DEFAULT_ImagePath = appSet.GetValue("IMAGE_PATH_SKU", GetType(String)) ' AppSettings("IMAGE_PATH_SKU").ToString
            Else

                objCustomSetting.GetConfig_Value("DEFAULT_IMAGE_PATH_SKU", "")
                objDT = objCustomSetting.DataTable
                If objDT.Rows.Count > 0 Then
                    Me._DEFAULT_ImagePath = objDT.Rows(0).Item("Config_Value").ToString
                Else
                    Me._DEFAULT_ImagePath = Application.StartupPath
                End If


                If Not IO.Directory.Exists(Me._DEFAULT_ImagePath) Then
                    '(Directory.CreateDirectory(Me._DEFAULT_ImagePath))
                End If
            End If
            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
#End Region

#Region "LOAD MASTER TO COMBO FUNCTIONS"

    Sub AddcboDimensionType()
        Try
            Dim objDB As New ms_DimensionType
            Dim objDT As New DataTable
            objDT = objDB.GetDimensionTypeData("")
            If objDT.Rows.Count > 0 Then
                With cboDimensionType
                    .DataSource = objDT
                    .DisplayMember = "DimensionType_Id"
                    .ValueMember = "DimensionType_Index"
                End With
                _DimenstionRatio = getRatioM3(cboDimensionType.SelectedValue)
                _Digit = getDigitM3(cboDimensionType.SelectedValue)
            Else
                _DimenstionRatio = 1
                _Digit = 4
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub AddcboDimensionType_Ratio()
        Try
            Dim objDB As New ms_DimensionType
            Dim objDT As New DataTable
            objDT = objDB.GetDimensionTypeData("")
            If objDT.Rows.Count > 0 Then
                With cboDimensionType_Ratio
                    .DataSource = objDT
                    .DisplayMember = "DimensionType_Id"
                    .ValueMember = "DimensionType_Index"
                End With
                _DimenstionRatioPakcage = getRatioM3(cboDimensionType_Ratio.SelectedValue)
                _DigitPackage = getDigitM3(cboDimensionType_Ratio.SelectedValue)
            Else
                _DimenstionRatio = 1
                _Digit = 4
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub AddcbPalletType()
        Try
            Dim objCurrencyPrice As New ms_PalletType(ms_PalletType.enuOperation_Type.SEARCH)
            objCurrencyPrice.getPalletType()
            With cbPalletType
                .DataSource = objCurrencyPrice.GetDataTable
                .DisplayMember = "Pallet_Name"
                .ValueMember = "PalletType_Index"
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub AddcbGroupSku()
        Dim objClassDB As New SQLCommands
        Dim objDT As DataTable = New DataTable

        ' TODO: Should not use SQL command in the form.
        Try
            cbGroup.Items.Clear()
            objClassDB.SQLComand("SELECT GroupSku_Index, GroupSku_Name FROM ms_GroupSku ORDER BY GroupSku_Index")
            objDT = objClassDB.DataTable

            cbGroup.DisplayMember = "GroupSku_Name"
            cbGroup.ValueMember = "GroupSku_Index"
            cbGroup.DataSource = objDT

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Sub AddcbCurrency1()

        Dim objCurrencyPrice As New svms_Currency()
        Try
            objCurrencyPrice.GetAllAsDataTable()
            With cbCurrency1
                .DataSource = objCurrencyPrice.GetDataTable
                .DisplayMember = "Description"
                .ValueMember = "Currency_Index"
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub AddcbCurrency2()

        Dim objCurrencyPrice As New svms_Currency()
        Try
            objCurrencyPrice.GetAllAsDataTable()
            With cbCurrency2
                .DataSource = objCurrencyPrice.GetDataTable
                .DisplayMember = "Description"
                .ValueMember = "Currency_Index"
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub AddcbCurrency3()

        Dim objCurrencyPrice As New svms_Currency()
        Try
            objCurrencyPrice.GetAllAsDataTable()
            With cbCurrency3
                .DataSource = objCurrencyPrice.GetDataTable
                .DisplayMember = "Description"
                .ValueMember = "Currency_Index"
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub AddcbColor()
        Dim objClassDB As New ms_color(ms_color.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            cbColor.Items.Clear()
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbColor.DisplayMember = "Description"
            cbColor.ValueMember = "Color_Index"
            cbColor.DataSource = objDT
            cbColor.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbModel()
        Dim objClassDB As New ms_Model(ms_Model.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            cbModel.Items.Clear()
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbModel.DisplayMember = "Description"
            cbModel.ValueMember = "Model_Index"
            cbModel.DataSource = objDT
            cbModel.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbBrand()
        Dim objClassDB As New ms_Brand(ms_Brand.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            cbBrand.Items.Clear()
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbBrand.DisplayMember = "Description"
            cbBrand.ValueMember = "Brand_Index"
            cbBrand.DataSource = objDT
            cbBrand.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbType()
        Dim objClassDB As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            '  cbType.Items.Clear()
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbType.DisplayMember = "Description"
            cbType.ValueMember = "ProductType_Index"
            cbType.DataSource = objDT
            cbType.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    'Sub AddcbMainPackage()
    '    Dim objClassDB As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable
    '    Try
    '        cbMainPackage.Items.Clear()
    '        objClassDB.SearchData_Click("", "")
    '        objDT = objClassDB.DataTable

    '        cbMainPackage.DisplayMember = "Description"
    '        cbMainPackage.ValueMember = "Package_Index"
    '        cbMainPackage.DataSource = objDT
    '        cbMainPackage.SelectedIndex = 0
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        objClassDB = Nothing
    '    End Try
    'End Sub
    Sub Addcbsize()
        Dim objClassDB As New ms_Size(ms_Size.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            cbSize.Items.Clear()
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbSize.DisplayMember = "Description"
            cbSize.ValueMember = "Size_Index"
            cbSize.DataSource = objDT
            cbSize.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

#End Region

#Region "MAIN FUNCTIONS AND SUBS"
    ''' <summary>
    ''' This functions load previously added SKU packages and ratios to Grid View.
    ''' </summary>
    ''' <param name="SKuIndex"></param>
    ''' <remarks>* Use in EDIT mode Only *</remarks>

    Sub ShowgrdSKU_Package(ByVal SKuIndex As String)
        Dim objClassDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        'Dim objDr As DataRow
        Dim i As Integer = 0

        Try
            'grdSKU_Package.Rows.Clear()
            objClassDB.SearchData2_Click(SKuIndex, "")
            objDT = objClassDB.DataTable

            Me.grdSKU_Package.AutoGenerateColumns = False
            Me.grdSKU_Package.DataSource = objDT
            Me.grdSKU_Package.Update()
            Me.grdSKU_Package.Refresh()
            'If objDT.Rows.Count > 0 Then

            '    For Each objDr In objDT.Rows
            '        ' If cbMainPackage.Items.Count <= 0 Then Exit Sub

            '        grdSKU_Package.Rows.Add()

            '        grdSKU_Package.Rows(i).Cells("Col_Qty").Value = 1
            '        grdSKU_Package.Rows(i).Cells("ColumnIndex").Value = objDr("SkuRatio_Index").ToString
            '        grdSKU_Package.Rows(i).Cells("ColumnPackageIndex").Value = objDr("Package_Index").ToString
            '        grdSKU_Package.Rows(i).Cells("ColumnRatio").Value = objDr("Ratio").ToString
            '        grdSKU_Package.Rows(i).Cells("ColumnPackage").Value = objDr("description").ToString
            '        grdSKU_Package.Rows(i).Cells("ColumnMainPackage").Value = Me.txtPackage.Text

            '        grdSKU_Package.Rows(i).Cells("col_Weight").Value = objDr("Weight").ToString
            '        grdSKU_Package.Rows(i).Cells("col_Dimension_Hi").Value = objDr("Dimension_Hi").ToString
            '        grdSKU_Package.Rows(i).Cells("col_Dimension_Wd").Value = objDr("Dimension_Wd").ToString
            '        grdSKU_Package.Rows(i).Cells("col_Dimension_Len").Value = objDr("Dimension_Len").ToString
            '        grdSKU_Package.Rows(i).Cells("Col_Barcode").Value = objDr("Barcode").ToString

            '        'killz Ratio M3
            '        grdSKU_Package.Rows(i).Cells("Col_DimensionType_Index").Value = objDr("DimensionType_Index").ToString
            '        grdSKU_Package.Rows(i).Cells("Col_DimensionType_Id").Value = objDr("DimensionType_Id").ToString

            '        i = i + 1
            '    Next

            'Else
            '    grdSKU_Package.Rows.Clear()
            '    grdSKU_Package.Refresh()
            'End If
        Catch ex As Exception
            'W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub

    ' Todd: 14 March 2009
    ' NOTE: Could not find it is used anywhere, so comment out!
    'Sub SaveSKU_Ratio()
    '    Select Case SaveType
    '        Case 0

    '        Case 1
    '            Dim objDB As New ms_SKURatio(ms_Size.enuOperation_Type.UPDATE)
    '            objDB.SaveData(_NewPackage_Index, me._Sku_index, cbPackage.SelectedValue.ToString, Val(txtQty.Text))
    '        Case Else
    '    End Select
    'End Sub

    ''' <summary>
    ''' Main functions to save SKU information. Use for both Adding and Editing.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SaveSKU()
        Try
            Dim bitPlot As Integer = 0
            Dim bitControlAsset As Integer = 0
            Dim bitPackRework As Integer = 0
            Dim bitBarcodeProcess As Integer = 0
            Dim bitPackingType As Integer = cbPickingType.SelectedIndex
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

            Dim strPallet_Type As String = ""

            Dim intPicking_Mode As Integer = 1

            Dim productClass As String = String.Empty
            Dim productSubClass As String = String.Empty

            If cb_isPlot.Checked = True Then bitPlot = 1
            If cb_IsControlAsset.Checked = True Then bitControlAsset = 1
            If cb_IsControlPack.Checked = True Then bitPackRework = 1
            If cb_PrintBarcode.Checked = True Then bitBarcodeProcess = 1

            ' Get Index from Tag / Combo
            If txtCustomer_Name.Tag <> "" Then
                strCustomerIndex = txtCustomer_Name.Tag
            Else
                strCustomerIndex = ""
            End If
            If txtSupplierName.Tag <> "" Then
                strSupplierIndex = txtSupplierName.Tag
            Else
                strSupplierIndex = ""
            End If

            If Not Me.cbSize.SelectedValue Is Nothing Then
                strSize = Me.cbSize.SelectedValue.ToString
            Else
                strSize = ""
            End If

            If Not Me.cbColor.SelectedValue Is Nothing Then
                strColor = Me.cbColor.SelectedValue.ToString
            Else
                strColor = ""
            End If

            If Not Me.cbModel.SelectedValue Is Nothing Then
                strModel = Me.cbModel.SelectedValue.ToString
            Else
                strModel = ""
            End If

            If Not Me.cbBrand.SelectedValue Is Nothing Then
                strBrand = Me.cbBrand.SelectedValue.ToString
            Else
                strBrand = ""
            End If

            strMainPackage = Me._Package_Index

            If Not cbCurrency1.SelectedValue Is Nothing Then
                strCurrency1 = cbCurrency1.SelectedValue.ToString
            Else
                strCurrency1 = ""
            End If

            If Not cbCurrency2.SelectedValue Is Nothing Then
                strCurrency2 = cbCurrency2.SelectedValue.ToString
            Else
                strCurrency2 = ""
            End If

            If Not cbCurrency3.SelectedValue Is Nothing Then
                strCurrency3 = cbCurrency3.SelectedValue.ToString
            Else
                strCurrency3 = ""
            End If

            If Not cbPalletType.SelectedValue Is Nothing Then
                strPallet_Type = cbPalletType.SelectedValue.ToString
            Else
                strPallet_Type = ""
            End If
            'If Not cbPickingType.SelectedValue Is Nothing Then
            '    intPicking_Mode = cbPickingType.SelectedIndex
            'Else
            '    intPicking_Mode = 1
            'End If

            If Me.cbProductClass.SelectedValue IsNot Nothing Then
                productClass = Me.cbProductClass.SelectedValue.ToString
            End If
            If Me.cbProductSubClass.SelectedValue IsNot Nothing Then
                productSubClass = Me.cbProductSubClass.SelectedValue.ToString
            End If

            Select Case SaveType
                ' ------ Case Add New SKU
                Case 0
                    ' Todd: 14 March 2009
                    ' NOTE: This function "AddDefaltSku_Package" is commented out.
                    ' We should not auto insert a default row into Package Grid View.
                    ' This logic is wrong.

                    ' AddDefaltSku_Package()
                    Dim objDB As New ms_SKU(ms_SKU.enuOperation_Type.ADDNEW)
                    Dim blnSaveResult As Boolean = False

                    Select Case ProductRatio_Type
                        Case "1"
                            objDB._ProductSku_Type = "1"
                            If cbType.SelectedValue Is Nothing Then
                                objDB._Product_Type = "0010000000001"
                            Else
                                objDB._Product_Type = cbType.SelectedValue.ToString
                            End If

                            objDB._ProductName = txtProduct.Text
                            ' ------ Remark: 
                            ' ------ Str10 stores GroupSKU value.
                            ' ------ Str4 and Str5 is Customer and Supplier Reference Code respectively.
                            objDB.Str10 = Me.cbGroup.SelectedValue.ToString
                            objDB.Str4 = Me.txtStr4_CustomerRefCode.Text.Trim
                            objDB.Str5 = Me.txtStr5_SupplierRefCode.Text.Trim
                            objDB.PalletType_Index = strPallet_Type
                            objDB.Pick_Method = intPicking_Mode
                            If Me._Item_Package_Index Is Nothing Then
                                objDB.Item_Package_Index = ""
                            Else
                                objDB.Item_Package_Index = Me._Item_Package_Index
                            End If

                            ' TODO: This is wrong.
                            Dim objDBIndex As New Sy_AutoNumber
                            Me._Sku_Index = objDBIndex.getSys_Value("SKU_Index")
                            objDBIndex = Nothing

                            If _isPic = True Then
                                gstrFileName = Me._Sku_Index.ToString & ".JPG"
                            Else
                                gstrFileName = ""
                            End If

                            blnSaveResult = objDB.SaveData(_Sku_Index, txtSKUID.Text, "", strSize, strMainPackage, txtUnitWeight.Text, strColor, strModel, strBrand, Me.txtBarCode1.Text, Me.txtBarCode2.Text, Val(Me.txtPrice1.Text), Val(Me.txtPrice2.Text), Val(Me.txtPrice3.Text), Val(Me.txtLifeTime_y.Text), Val(Me.txtLifeTime_m.Text), Val(Me.txtLifeTime_d.Text), Me.txtStr1_SKUNameThai.Text, Me.txtStr2_SKUNameEng.Text, Me.txtSKUDescription.Text, Val(Me.txtMin_Qty.Text), Val(Me.txtMin_Weight.Text), Val(Me.txtMin_Volume.Text), Val(Me.txtMax_Qty.Text), Val(Me.txtMax_Weight.Text), Val(Me.txtMin_Volume.Text), _
                                                bitPlot, bitControlAsset, bitPackRework, bitBarcodeProcess, bitPackingType, Val(txtQty_Per_Pallet.Text), Val(txtVolumeX.Text), Val(txtVolumeY.Text), Val(txtVolumeZ.Text), Val(txtVolume.Text), txtStr4_CustomerRefCode.Text, txtStr5_SupplierRefCode.Text, strCurrency1, strCurrency2, strCurrency3, strCustomerIndex, strSupplierIndex, gstrFileName, cbPickingType.SelectedIndex, Me._Item_Package_Index, Me.txtLocation.Tag, productClass, productSubClass, grdSKU_Package.DataSource)

                            'blnSaveResult = objDB.SaveData("", txtSKUID.Text, Me._Product_Index, strSize, strMainPackage, txtUnitWeight.Text, strColor, strModel, strBrand, Me.txtBarCode1.Text, Me.txtBarCode2.Text, Val(Me.txtPrice1.Text), Val(Me.txtPrice2.Text), Val(Me.txtPrice3.Text), Val(Me.txtLifeTime_y.Text), Val(Me.txtLifeTime_m.Text), Val(Me.txtLifeTime_d.Text), Me.txtStr1_SKUNameThai.Text, Me.txtStr2_SKUNameEng.Text, Me.txtSKUDescription.Text, Val(Me.txtMin_Qty.Text), Val(Me.txtMin_Weight.Text), Val(Me.txtMin_Volume.Text), Val(Me.txtMax_Qty.Text), Val(Me.txtMax_Weight.Text), Val(Me.txtMin_Volume.Text), bitPlot, Val(txtQty_Per_Pallet.Text), Val(txtVolumeX.Text), Val(txtVolumeY.Text), Val(txtVolumeZ.Text), Val(txtVolume.Text), txtStr4_CustomerRefCode.Text, txtStr5_SupplierRefCode.Text, strCurrency1, strCurrency2, strCurrency3, strCustomerIndex, strSupplierIndex, gstrFileName, cbPickingType.SelectedIndex, grdSKU_Package)
                            ' ------ Set new SKU Index to a label to be used later.


                            '*********** Dong_KK : Save Transaction SKU ***************

                            blnSaveResult = objDB.InsertSKU_Transation()
                            Me.txtSKUID.Text = objDB.Sku_Id

                            ' Return For Update 

                            '   Me._Sku_Index = objDB.Sku_Index.ToString
                            Me._Product_Index = objDB.Product_Index.ToString
                            cbType.SelectedValue = objDB._Product_Type

                        Case "0"
                            objDB.Str10 = Me.cbGroup.SelectedValue.ToString
                            blnSaveResult = objDB.SaveData("", txtSKUID.Text, Me._Product_Index, strSize, strMainPackage, txtUnitWeight.Text, strColor, strModel, strBrand, Me.txtBarCode1.Text, Me.txtBarCode2.Text, Val(Me.txtPrice1.Text), Val(Me.txtPrice2.Text), Val(Me.txtPrice3.Text), Val(Me.txtLifeTime_y.Text), Val(Me.txtLifeTime_m.Text), Val(Me.txtLifeTime_d.Text), Me.txtStr1_SKUNameThai.Text, Me.txtStr2_SKUNameEng.Text, Me.txtSKUDescription.Text, Val(Me.txtMin_Qty.Text), Val(Me.txtMin_Weight.Text), Val(Me.txtMin_Volume.Text), Val(Me.txtMax_Qty.Text), Val(Me.txtMax_Weight.Text), Val(Me.txtMin_Volume.Text), _
                                                            bitPlot, bitControlAsset, bitPackRework, bitBarcodeProcess, bitPackingType, Val(txtQty_Per_Pallet.Text), Val(txtVolumeX.Text), Val(txtVolumeY.Text), Val(txtVolumeZ.Text), Val(txtVolume.Text), txtStr4_CustomerRefCode.Text, txtStr5_SupplierRefCode.Text, strCurrency1, strCurrency2, strCurrency3, strCustomerIndex, strSupplierIndex, gstrFileName, cbPickingType.SelectedIndex, Me._Item_Package_Index, Me.txtLocation.Tag, productClass, productSubClass, grdSKU_Package.DataSource)

                        Case Else
                            objDB.Str10 = Me.cbGroup.SelectedValue.ToString
                            blnSaveResult = objDB.SaveData(Me._Sku_Index, txtSKUID.Text, Me._Product_Index, strSize, strMainPackage, txtUnitWeight.Text, strColor, strModel, strBrand, Me.txtBarCode1.Text, Me.txtBarCode2.Text, Val(Me.txtPrice1.Text), Val(Me.txtPrice2.Text), Val(Me.txtPrice3.Text), Val(Me.txtLifeTime_y.Text), Val(Me.txtLifeTime_m.Text), Val(Me.txtLifeTime_d.Text), Me.txtStr1_SKUNameThai.Text, Me.txtStr2_SKUNameEng.Text, Me.txtSKUDescription.Text, Val(Me.txtMin_Qty.Text), Val(Me.txtMin_Weight.Text), Val(Me.txtMin_Volume.Text), Val(Me.txtMax_Qty.Text), Val(Me.txtMax_Weight.Text), Val(Me.txtMin_Volume.Text), _
                                                            bitPlot, bitControlAsset, bitPackRework, bitBarcodeProcess, bitPackingType, Val(txtQty_Per_Pallet.Text), Val(txtVolumeX.Text), Val(txtVolumeY.Text), Val(txtVolumeZ.Text), Val(txtVolume.Text), txtStr4_CustomerRefCode.Text, txtStr5_SupplierRefCode.Text, strCurrency1, strCurrency2, strCurrency3, strCustomerIndex, strSupplierIndex, gstrFileName, cbPickingType.SelectedIndex, Me._Item_Package_Index, Me.txtLocation.Tag, productClass, productSubClass, grdSKU_Package.DataSource)

                    End Select


                    If blnSaveResult Then


                        gstrAppPath = _DEFAULT_ImagePath & Me._Sku_Index.ToString & ".JPG"

                        Saveimages()

                        W_MSG_Information_ByIndex(1)

                        ' Now since we did not close this form. We change SaveType mode to EDIT.
                        ' This way next time user click "Save", it will update insert of insert.

                    Else
                        ' TODO: Show appropriated messages instead of using the same messages.
                        W_MSG_Information_ByIndex(2)
                    End If

                    ' ------ Case Edit SKU data
                Case 1
                    Dim objDB As New ms_SKU(ms_SKU.enuOperation_Type.UPDATE)
                    objDB.Str10 = Me.cbGroup.SelectedValue.ToString
                    objDB._Product_Type = cbType.SelectedValue.ToString
                    objDB.Item_Package_Index = Me._Item_Package_Index
                    objDB.PalletType_Index = strPallet_Type
                    objDB.Pick_Method = intPicking_Mode
                    'gstrFileName
                    If _isPic = True Then
                        gstrFileName = Me._Sku_Index.ToString & ".JPG"
                    Else
                        gstrFileName = ""
                        If gstrAppPath <> "" Then
                            IO.File.Delete(gstrAppPath)
                            gstrAppPath = ""
                        End If
                    End If

                    Dim _Excute As New DBType_SQLServer
                 

                    Dim Check_Product As New DataTable
                    'Dim Product_Index_Update As String = ""
                    Check_Product = _Excute.DBExeQuery(String.Format("Select * from ms_Product where Product_index = '{0}'  and  Status_id <> -1", Me.Product_Index))
                    If Check_Product.Rows.Count = 0 Then
                        Dim str_Sql As String
                        Dim obj_syAuto As New Sy_AutoNumber
                        Dim Product_index As String = obj_syAuto.getSys_Value("Product_index")
                        str_Sql = "Insert ms_Product (Product_Index,Product_Id,Product_Name_th,Product_Name_en,ProductType_Index,Std_Package_Index,add_by,add_date,add_branch) "
                        str_Sql &= String.Format(" values('{0}','{1}','{2}','{3}','{4}','{5}','{6}',getdate(),'{7}')", Product_index, txtSKUID.Text, txtStr1_SKUNameThai.Text.Trim, _
                                                 txtStr2_SKUNameEng.Text.Trim, cbType.SelectedValue.ToString, strMainPackage, W_Module.WV_UserName, W_Module.WV_Branch_ID)
                        _Excute.DBExeNonQuery(str_Sql)
                        Me.Product_Index = Product_index
                    End If
                    Dim xdtPackage As New DataTable
                    xdtPackage = CType(grdSKU_Package.DataSource, DataTable)
                    xdtPackage.AcceptChanges()
                    Dim blnSaveResult As Boolean = objDB.SaveData(Me._Sku_Index, txtSKUID.Text, Me.Product_Index, strSize, strMainPackage, txtUnitWeight.Text, strColor, strModel, strBrand, Me.txtBarCode1.Text, Me.txtBarCode2.Text, Val(Me.txtPrice1.Text), Val(Me.txtPrice2.Text), Val(Me.txtPrice3.Text), Val(Me.txtLifeTime_y.Text), Val(Me.txtLifeTime_m.Text), Val(Me.txtLifeTime_d.Text), Me.txtStr1_SKUNameThai.Text, Me.txtStr2_SKUNameEng.Text, Me.txtSKUDescription.Text, Val(Me.txtMin_Qty.Text), Val(Me.txtMin_Weight.Text), Val(Me.txtMin_Volume.Text), Val(Me.txtMax_Qty.Text), Val(Me.txtMax_Weight.Text), Val(Me.txtMax_Volume.Text), _
                                                                                                   bitPlot, bitControlAsset, bitPackRework, bitBarcodeProcess, bitPackingType, Val(txtQty_Per_Pallet.Text), Val(txtVolumeX.Text), Val(txtVolumeY.Text), Val(txtVolumeZ.Text), Val(txtVolume.Text), txtStr4_CustomerRefCode.Text, txtStr5_SupplierRefCode.Text, strCurrency1, strCurrency2, strCurrency3, strCustomerIndex, strSupplierIndex, gstrFileName, cbPickingType.SelectedIndex, Me._Item_Package_Index, Me.txtLocation.Tag, productClass, productSubClass, xdtPackage)

                    '****************************
                    If blnSaveResult Then
                        'Dim objDBDelete As New ms_SKURatio(ms_SKURatio.enuOperation_Type.DELETE)

                        'Dim e As Integer = 0
                        'For e = 0 To grdSKU_Package.Rows.Count - 1
                        '    Dim PackageIndexE As String = grdSKU_Package.Rows(e).Cells("ColumnPackageIndex").Value.ToString()
                        '    Dim RatioE As String = grdSKU_Package.Rows(e).Cells("ColumnRatio").Value.ToString()
                        '    Dim objDBEdit As New ms_SKURatio(ms_SKURatio.enuOperation_Type.UPDATE)

                        '    objDBEdit.SaveData("", Me._Sku_index, PackageIndexE, Val(RatioE))
                        'Next
                        Saveimages()

                        W_MSG_Information_ByIndex(1)
                    Else
                        W_MSG_Information_ByIndex(2)
                    End If

                Case Else
            End Select
            'Fix Hard code interface flag
            Dim xobjDB As New DBType_SQLServer
            xobjDB.DBExeNonQuery(String.Format("update ms_SKU  set INT_U = Null where Sku_Index = '{0}'", Me._Sku_Index))

            '--- Update IsPrintBarcode & IsControlPack ---'
            Dim _chk As Boolean = False
            Dim objSetting As New config_CustomSetting
            _chk = objSetting.getConfig_Key_USE("USE_SAVE_IsControl_Pack_And_Barcode")
            If _chk = True Then

                Dim isCnPack As Integer = 0
                Dim isCnBarcode As Integer = 0

                If cb_IsControlPack.Checked = True Then isCnPack = 1
                If cb_PrintBarcode.Checked = True Then isCnBarcode = 1

                Dim objDB As New ms_SKU(ms_SKU.enuOperation_Type.UPDATE)
                If objDB.UpdateIscontrolPackAndBarcode(Me._Sku_Index, isCnPack, isCnBarcode) = False Then W_MSG_Error("ปัญหาจากการบันทึก IsPack ,Isbarcode")


            End If
            '---------------------------------------------'
            _OldSku_Id = Me.txtSKUID.Text
            SaveType = 1
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' Update By Hun
    ''' </summary>
    ''' <remarks></remarks>
    Sub SavePackage()
        Try

            Dim package_Id As String = ""
            Dim description As String = ""
            Dim dimension_Hi As Double = 0.0
            Dim dimension_Wd As Double = 0.0
            Dim dimension_Len As Double = 0.0
            Dim dimensionType_Index As String = ""
            Dim Weight As Double = 0.0
            Dim Barcode As String = ""


            If Me.txtPackage.Text.Trim <> "" Then
                package_Id = Me.txtPackage.Text
            Else
                package_Id = ""
            End If

            If Me.txtPackage.Text.Trim <> "" Then
                description = Me.txtPackage.Text
            Else
                description = ""
            End If

            If Me.txtVolumeZ.Text.Trim <> "" Then
                dimension_Hi = Me.txtVolumeZ.Text
            End If

            If Me.txtVolumeX.Text.Trim <> "" Then
                dimension_Wd = Me.txtVolumeX.Text
            End If

            If Me.txtVolumeY.Text.Trim <> "" Then
                dimension_Len = Me.txtVolumeY.Text
            End If

            'Weight
            If Me.txtUnitWeight.Text.Trim <> "" Then
                Weight = Me.txtUnitWeight.Text
            End If
            'Barcode
            If Me.txtBarCode1.Text.Trim <> "" Then
                Barcode = Me.txtBarCode1.Text
            End If
            ' Type To convert M3
            dimensionType_Index = cboDimensionType.SelectedValue.ToString

            Select Case SaveType
                Case 0
                    Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
                    Me._Package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, _isItem_Package, Weight, Barcode, dimensionType_Index) ', txtUnit_id.Text


                    '18-01-2010 KRIT Update : Save Item Packge
                    If Me.txtItem_Package.Text.Trim <> "" Then
                        Dim objms_ItemPackage As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)

                        _Item_Package_Index = objms_Package.SaveData("", Me.txtItem_Package.Text.Trim, Me.txtItem_Package.Text.Trim, 0, 0, 0, 1, 0, "", dimensionType_Index) ', txtUnit_id.Text
                    End If
                Case Else
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Add Date : 19/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Gen Ratio for Item Package
    ''' </remarks>
    Sub SaveSKURatio_ItemPackage(ByVal pSku_Index As String, ByVal pPackage_Index As String)
        Try

            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)

            Dim e As Integer = 0

            objDBSKURatio.SaveData("", pSku_Index, pPackage_Index, 1)


            '   W_MSG_Information_ByIndex(1)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' Update Date : 19/01/2010
    ''' 
    ''' <remarks></remarks>
    Sub UpdatePackage()
        Try

            Dim package_Id As String = ""
            Dim description As String = ""
            Dim dimension_Hi As Double = 0.0
            Dim dimension_Wd As Double = 0.0
            Dim dimension_Len As Double = 0.0
            Dim dimensionType_Index As String = ""
            Dim Weight As Double = 0.0
            Dim Barcode As String = ""
            If Me.txtPackage.Text.Trim <> "" Then
                package_Id = Me.txtPackage.Text
                description = Me.txtPackage.Text
            Else
                package_Id = ""
                description = ""
            End If

            If Me.txtVolumeZ.Text.Trim <> "" Then
                dimension_Hi = Me.txtVolumeZ.Text
            End If

            If Me.txtVolumeX.Text.Trim <> "" Then
                dimension_Wd = Me.txtVolumeX.Text
            End If

            If Me.txtVolumeY.Text.Trim <> "" Then
                dimension_Len = Me.txtVolumeY.Text
            End If

            If Me.txtUnitWeight.Text.Trim <> "" Then
                Weight = Me.txtUnitWeight.Text
            End If
            'Barcode
            If Me.txtBarCode1.Text.Trim <> "" Then
                Barcode = Me.txtBarCode1.Text
            End If

            'killz
            dimensionType_Index = cboDimensionType.SelectedValue.ToString

            Select Case SaveType

                Case 1
                    Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.UPDATE)
                    Me._Package_Index = objms_Package.SaveData(_Package_Index, package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, _isItem_Package, Weight, Barcode, dimensionType_Index) ', txtUnit_id.Text

                    '18-01-2010  KRIT Update เพิ่มเรื่อง Update data Item Package
                    If Me.txtItem_Package.Text.Trim <> "" And Me.txtItem_Package.Visible Then
                        If _Item_Package_Index = "" Then
                            Dim objItem_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
                            Me._Item_Package_Index = objItem_Package.SaveData("", Me.txtItem_Package.Text.Trim, Me.txtItem_Package.Text.Trim, 0, 0, 0, 1, 0, Barcode, dimensionType_Index) ', txtUnit_id.Text
                        Else
                            Dim objItem_Package As New ms_Package(ms_Package.enuOperation_Type.UPDATE)
                            Me._Item_Package_Index = objItem_Package.SaveData(_Item_Package_Index, Me.txtItem_Package.Text.Trim, Me.txtItem_Package.Text.Trim, 0, 0, 0, 1, 0, Barcode, dimensionType_Index) ', txtUnit_id.Text
                        End If

                    End If

            End Select

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' </summary>
    ''' <remarks>Date : 12/01/2010
    ''' Update By : Hun
    ''' -add parameter Barcode
    ''' </remarks>
    Sub SaveNewPackage()
        Try

            Dim package_Id As String = ""
            Dim description As String = ""
            Dim Des_Package As String = ""
            Dim dimension_Hi As Double = 0.0
            Dim dimension_Wd As Double = 0.0
            Dim dimension_Len As Double = 0.0
            Dim dimensionTypeRatio_Index As String = ""
            Dim Weight As Double = 0.0
            Dim Barcode As String = ""
            Barcode = Me.txtBarCode.Text

            If Not Me.txtPackageConvert.Text Is Nothing Then
                package_Id = Me.txtPackageConvert.Text
                description = Me.txtPackageConvert.Text
            Else

                package_Id = ""
                description = ""
            End If


            If Not Me.txtPackageHeight.Text Is Nothing Then
                dimension_Hi = Me.txtPackageHeight.Text
            End If

            If Not Me.txtPackageWidth.Text Is Nothing Then
                dimension_Wd = Me.txtPackageWidth.Text
            End If

            If Not Me.txtPackageLen.Text Is Nothing Then
                dimension_Len = Me.txtPackageLen.Text
            End If

            'Weight
            If Not Me.txtWeight.Text Is Nothing Then
                Weight = Me.txtWeight.Text
            End If
            ' Check barcode not null
            If Not Me.txtBarCode.Text Is Nothing Then
                Barcode = Me.txtBarCode.Text
            End If

            dimensionTypeRatio_Index = cboDimensionType_Ratio.SelectedValue.ToString

            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
            Me._NewPackage_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, _isItem_Package, Weight, Barcode, dimensionTypeRatio_Index) ', txtUnit_id.Text


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub SaveSKURatio()
        Try

            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)

            Dim e As Integer = 0
            For e = 0 To grdSKU_Package.Rows.Count

                Dim ratio As String = txtQty.Text 'grdSKU_Package.Rows(e).Cells("ColumnRatio").Value.ToString()
                objDBSKURatio.SaveData("", Me._Sku_Index, _NewPackage_Index, Val(ratio))
            Next

            W_MSG_Information_ByIndex(1)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Load all SKU data for Editing mode.
    ''' Update Date : 2010/06/21
    ''' Update By   : Dong_kk
    ''' Remark      : เพิ่ม _OldSku_Id
    ''' </summary>
    ''' <remarks></remarks>
    Sub LoadDataEdit()
        Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objClassDB.SelectData_For_Edit(Me._Sku_Index)
            objDT = objClassDB.DataTable

            Dim _chkControl As Boolean = False
            Dim objSetting As New config_CustomSetting
            _chkControl = objSetting.getConfig_Key_USE("USE_SAVE_IsControl_Pack_And_Barcode")

            If objDT.Rows.Count > 0 Then
                ' TODO: Why do we have to loop? Shouldn't it has only 1 record?
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    txtSKUID.Text = objDr("Sku_Id").ToString
                    _OldSku_Id = objDr("Sku_Id").ToString
                    Me._Product_Index = objDr("Product_Index").ToString
                    txtProduct.Text = objDr("Expr6").ToString + "-" + objDr("Expr7").ToString
                    txtStr1_SKUNameThai.Text = objDr("Str1").ToString
                    txtStr2_SKUNameEng.Text = objDr("Str2").ToString
                    txtSKUDescription.Text = objDr("Str3").ToString
                    txtStr4_CustomerRefCode.Text = objDr("Str4").ToString
                    txtStr5_SupplierRefCode.Text = objDr("Str5").ToString
                    txtBarCode1.Text = objDr("Barcode1").ToString
                    txtBarCode2.Text = objDr("Barcode2").ToString
                    '   txtBarcodePackage.Text = objDr("Barcode").ToString '
                    cbType.SelectedValue = objDr("ProductType_Index").ToString
                    ' cbMainPackage.SelectedValue = objDr("Package_Index").ToString
                    ' txtMainPackage.Text = cbMainPackage.Text
                    _Package_Index = objDr("Package_Index").ToString
                    _Item_Package_Index = objDr("Item_Package_Index").ToString
                    txtItem_Package.Text = objDr("Item_Package").ToString

                    txtPackage.Text = objDr("PackageDes").ToString

                    txtUnitWeight.Text = objDr("UnitWeight_Index").ToString

                    txtPrice1.Text = objDr("Price1").ToString
                    txtPrice2.Text = objDr("Price2").ToString
                    txtPrice3.Text = objDr("Price3").ToString

                    cbSize.SelectedValue = objDr("Size_Index").ToString
                    cbColor.SelectedValue = objDr("Color_Index").ToString
                    cbModel.SelectedValue = objDr("Model_Index").ToString
                    cbBrand.SelectedValue = objDr("Brand_Index").ToString

                    txtLifeTime_y.Text = objDr("ItemLife_y").ToString()
                    txtLifeTime_m.Text = objDr("ItemLife_m").ToString()
                    txtLifeTime_d.Text = objDr("ItemLife_d").ToString()

                    txtMin_Qty.Text = objDr("Min_Qty").ToString()
                    txtMin_Volume.Text = objDr("Min_Volume").ToString()
                    txtMin_Weight.Text = objDr("Min_Weight").ToString()

                    txtMax_Qty.Text = objDr("Max_Qty").ToString()
                    txtMax_Volume.Text = objDr("Max_Volume").ToString()
                    txtMax_Weight.Text = objDr("Max_Weight").ToString()

                    cb_isPlot.Checked = CType(objDr("isPlot").ToString(), Boolean)
                    'cb_IsControlAsset.Checked = CType(objDr("IsControlAsset").ToString(), Boolean)
                    cb_IsControlAsset.Checked = objDr("IsSerial").ToString
                    cbGroup.SelectedValue = objDr("str10").ToString
                    txtQty_Per_Pallet.Text = objDr("Qty_Per_Pallet").ToString


                    txtVolumeX.Text = objDr("Unit_Width").ToString
                    txtVolumeY.Text = objDr("Unit_Length").ToString
                    txtVolumeZ.Text = objDr("Unit_Height").ToString
                    txtVolume.Text = objDr("Unit_Volume").ToString

                    txtCustomer_Name.Text = objDr("Customer_Name").ToString
                    txtCustomer_Name.Tag = objDr("Customer_Index").ToString
                    txtSupplierName.Text = objDr("Supplier_Name").ToString
                    txtSupplierName.Tag = objDr("Supplier_Index").ToString
                    cbCurrency1.SelectedValue = objDr("Currency_Index1").ToString
                    cbCurrency2.SelectedValue = objDr("Currency_Index2").ToString
                    cbCurrency3.SelectedValue = objDr("Currency_Index3").ToString
                    cbPalletType.SelectedValue = objDr("PalletType_Index").ToString

                    cbProductClass.SelectedValue = objDr("ProductClass_Index").ToString

                    SetProductSubClassByIndex(objDr("ProductClass_Index").ToString)

                    cbProductSubClass.SelectedValue = objDr("ProductSubClass_Index").ToString

                    txtLocation.Text = objDr("Location_Alias").ToString
                    txtLocation.Tag = objDr("Location_Index").ToString

                    picSKU.Tag = ""

                    If IsDBNull(objDr("Image_Path")) Then
                        picSKU.ImageLocation = ""

                    ElseIf objDr("Image_Path") = "" Then
                        picSKU.ImageLocation = ""

                    Else
                        If Me._DEFAULT_ImagePath = "" Then
                            gstrAppPath = Application.StartupPath
                        Else
                            gstrAppPath = Me._DEFAULT_ImagePath
                        End If

                        gstrAppPath = gstrAppPath & objDr("Image_Path")

                     

                        If System.IO.File.Exists(gstrAppPath) Then
                            _isPic = True
                            picSKU.ImageLocation = gstrAppPath
                            picSKU.Tag = objDr("Image_Path")
                        End If

                    End If

                    If IsDBNull(objDr("Picking_Type")) Then
                        cbPickingType.SelectedIndex = 0
                    Else
                        cbPickingType.SelectedIndex = objDr("Picking_Type")
                    End If

                    'killz
                    cboDimensionType.SelectedValue = objDr("DimensionType_Index").ToString
                    If _chkControl = True Then
                        If objDr("IsControlPack").ToString = "1" Then
                            cb_IsControlPack.Checked = True
                        Else
                            cb_IsControlPack.Checked = False
                        End If
                        If objDr("IsPrintBarcode").ToString = "1" Then
                            cb_PrintBarcode.Checked = True
                        Else
                            cb_PrintBarcode.Checked = False
                        End If

                    End If


                    'PalletType_Index()
                    'Pick_Method()

                    i = i + 1
                Next

            Else

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
            objDr = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Write image file to disk
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Saveimages()
        Try

            If gstrLongFilePath = "" Then
                Exit Sub
            End If

            If picSKU.Image Is Nothing Then Exit Sub

            Me.picSKU.Image.Save(gstrAppPath, Imaging.ImageFormat.Jpeg)

            '  IO.File.Copy(gstrLongFilePath, gstrAppPath, True)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Check duplicated SKU ID.
    ''' </summary>
    ''' <param name="Sku_ID"></param>
    ''' <returns>True, if duplicated. False otherwise.</returns>
    ''' <remarks></remarks>
    Private Function Check_SkuID(ByVal Sku_ID As String) As Boolean
        Dim objSku As New SQLCommands
        'TODO: DO NOT use SQL command here!!!
        objSku.SQLComand("SELECT Count(*) As Count_ID FROM ms_SKU WHERE SKU_Id IN ('" & Sku_ID.Trim & "') AND status_id <> -1 ")

        If Val(objSku.DataTable.Rows(0).Item("Count_ID")) > 0 Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "EVENT CONTROL: BUTTONS"
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' This sub just adds new package and ratio to Grid View only. 
    ''' Adding to database will be done later.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' </remarks>
    Private Sub btnSave_Package_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave_Package.Click
        Try


            '2009-05-25
            Me._isItem_Package = 0

            If _Package_Index = "" Then
                W_MSG_Information("กรุณาทำการบันทึกข้อมูลสินค้าก่อน เพิ่มรายการแปลงหน่วยนับ")
                Exit Sub
            End If

            If txtPackageConvert.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนข้อมูลหน่วยนับที่ต้องการแปลงก่อน")
                Exit Sub
            End If

            ' Default for conversion ratio = 1
            If Me.txtQty.Text.Trim = "" Or Me.txtQty.Text.Trim <= 0 Then
                Me.txtQty.Text = "1"
            End If

            'Check Dup
            For iRow As Integer = 0 To grdSKU_Package.RowCount - 1
                If grdSKU_Package.Rows(iRow).Cells("ColumnPackage").Value = txtPackageConvert.Text.Trim Then
                    W_MSG_Information("หน่วยนับนี้ มีแล้วในฐานข้อมูล")
                    Exit Sub
                End If
            Next

            Me.SaveNewPackage()
            Me.SaveSKURatio()

            ShowgrdSKU_Package(Me._Sku_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
    ''' <summary>
    ''' This sub will be used in ProductRatio_Type = "0" only.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGetProduct_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetProduct.Click
        Dim frm As New frmGetProduct
        frm.ShowDialog()
        Me._Product_Index = frm.StrProductIndex
        txtProduct.Text = frm.StrProductName
        txtSKUID.Text = frm.strProductID

    End Sub

    ''' <summary>
    ''' This is the main button for saving SKU data.
    ''' Update By Hun
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Update Date : 19/01/2010
    ''' Update By   : Dong_kk
    ''' 
    ''' </remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click


        Try
            'Dim obj As New WMS_STD_Formula.W_SetValidate
            'Dim str As String
            'str = obj.ValidateControl(Me)
            'If str <> "" Then
            '    W_MSG_Information(str)
            '    Exit Sub
            'End If
            Dim sku_ID_Length As Double
            Dim obj_SKU As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            sku_ID_Length = obj_SKU.getSKU_Id_length()
            If txtSKUID.Text.Length >= sku_ID_Length Then
                'W_MSG_Information_ByIndex(300034)
                W_MSG_Information("รหัสสินค้าความยาวเกิน " & sku_ID_Length.ToString & " ที่ระบบกำหนดกำหนด")
                Exit Sub
            End If

            If cbType.SelectedIndex = -1 Then
                'W_MSG_Information_ByIndex(300034)
                W_MSG_Information("กรุณาเลือกประเภทสินค้าก่อน")
                Exit Sub
            End If

            If txtSKUID.Text.Length >= 250 Then
                'W_MSG_Information_ByIndex(300034)
                W_MSG_Information("รหัสสินค้าความยาวเกิน 250 ที่ระบบกำหนดกำหนด")
                Exit Sub
            End If


            '   2009-05-25
            If Me.txtPackage.Text = "" Then
                W_MSG_Information("กรุณาป้อนข้อมูลหน่วยสินค้า")
                Exit Sub
            End If

            If Me.txtLocation.Tag Is Nothing Then
                Me.txtLocation.Tag = ""
            End If

            '  18-01-2010 Krit Update Check การป้อนหน่วยสินค้าหน่วยภายใน 
            If Me.txtItem_Package.Visible = True And Me.txtItem_Package.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนข้อมูล" & lblItem_Packge.Text.Trim)
                Exit Sub
            End If



            '21-08-2014 Neung Check Ratio > 0
            With Me.grdSKU_Package
                For i As Integer = 0 To .Rows.Count - 1
                    If .Rows(i).Cells("ColumnPackage").Value Is Nothing Then
                        W_MSG_Information("กรุณาระบุหน่วยสินค้า")
                        Exit Sub
                    Else
                        If IsDBNull(.Rows(i).Cells("ColumnPackage").Value) Then
                            W_MSG_Information("กรุณาระบุหน่วยสินค้า")
                            Exit Sub
                        Else
                            If .Rows(i).Cells("ColumnPackage").Value.ToString.Trim = "" Then
                                W_MSG_Information("กรุณาระบุหน่วยสินค้า")
                                Exit Sub
                            End If
                        End If
                    End If

                    If .Rows(i).Cells("ColumnRatio").Value Is Nothing Then
                        W_MSG_Information("กรุณาระบุ Ratio มากกว่า 0")
                        Exit Sub
                    Else
                        If IsDBNull(.Rows(i).Cells("ColumnRatio").Value) Then
                            W_MSG_Information("กรุณาระบุ Ratio มากกว่า 0")
                            Exit Sub
                        Else
                            If CDbl(.Rows(i).Cells("ColumnRatio").Value) <= 0 Then
                                W_MSG_Information("กรุณาระบุ Ratio มากกว่า 0")
                                Exit Sub
                            End If
                        End If
                    End If

                Next
            End With



            Select Case ProductRatio_Type
                Case "1" ' Mode auto create Product when adding SKU. Product : SKU = 1:1
                    ' Check required fields.
                    If txtStr1_SKUNameThai.Text = "" Then
                        '   TODO: Use specific message. Do not use generic one.
                        'W_MSG_Information_ByIndex(6)
                        W_MSG_Information("กรุณาระบุ ชื่อรายการไทย")
                        Exit Sub
                    End If
                Case "0" ' Normal mode
                    '  Check required fields.
                    If txtProduct.Text = "" Or txtStr1_SKUNameThai.Text = "" Then
                        '  TODO: Use specific message. Do not use generic one.
                        W_MSG_Information_ByIndex(6)
                        Exit Sub
                    End If
            End Select

            Select Case SaveType
                Case 0
                    ' Check for duplicated SKU ID. Adding new SKU mode only!
                    If Check_SkuID(txtSKUID.Text.Trim) = False Then
                        W_MSG_Information_ByIndex(36)
                        Exit Sub
                    End If
                    SavePackage()
                    SaveSKU()
                    'SaveSKURatio_ItemPackage(Me._Sku_Index, _Item_Package_Index)

                    '----------LoadData From Load
                    LoadDataEdit()
                    ShowgrdSKU_Package(Me._Sku_Index)
                    'txtSKUID.ReadOnly = False   ' In case of Editing, we don't allow to modify SKU ID
                    txtProduct.ReadOnly = True
                    txtSKUID.BackColor = Color.Gainsboro
                    txtProduct.BackColor = Color.Gainsboro
                    '   -----------------
                Case 1
                    If txtSKUID.Text.Trim = "" And SaveType = 1 Then
                        W_MSG_Information("กรุณาป้อนรหัส SKU")
                        Exit Sub
                    End If
                    If _OldSku_Id <> txtSKUID.Text Then
                        If Check_SkuID(txtSKUID.Text.Trim) = False Then
                            W_MSG_Information_ByIndex(36)
                            Exit Sub
                        End If
                    End If

                    UpdatePackage()
                    SaveSKU()
                    '   SaveSKURatio_ItemPackage(Me._Sku_Index, _Item_Package_Index)
            End Select


            SaveType = 1

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Delete SKU Package from Grid View
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDel_Package_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel_Package.Click
        Try
            If grdSKU_Package.Rows.Count <= 0 Then
                Exit Sub
            End If

            If Me.grdSKU_Package.CurrentRow.Cells("ColumnPackageIndex").Value = Me.Package_Index Then
                W_MSG_Information("ไม่สามารถทำการลบรายการแปลงหน่วยหลักนี้ได้")
                Exit Sub
            End If

            If Not Me.grdSKU_Package.CurrentRow.Cells("ColumnIndex").Value = "" Then
                'Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.DELETE)
                Dim objDB As New clsSkuMinMaxByWH
                'dong Add 2010/01/05 ตรวจสอบว่ามีการใช้งานสินค้านี้อยู่ใหม
                If (objDB.chkSKU_PACKAGE_USED(Me.grdSKU_Package.CurrentRow.Cells("ColumnPackageIndex").Value)) Then
                    Dim dtChkSku As New DataTable
                    dtChkSku = objDB.GetDataTable
                    With dtChkSku.Rows(0)
                        MessageBox.Show("Receiving(กำลังรับ)       : " & .Item("Receive") & Environment.NewLine _
                                      & "Picking(กำลังเบิก)         : " & .Item("WithDraw") & Environment.NewLine _
                                      & "Transferring(กำลังโอน) : " & .Item("Transfer") & Environment.NewLine _
                                      & "Borrowing(กำลังยืม)      : " & .Item("Borrow") & Environment.NewLine _
                                      & "Returning(กำลังคืน)       : " & .Item("BorrowReturn") & Environment.NewLine _
                                      & "Balance(คงเหลือ)           : " & .Item("Balance") & Environment.NewLine _
                        , "สินค้ากำลังใช้งานไม่สามรถลบได้")
                    End With

                    Exit Sub
                End If

                If W_MSG_Confirm_ByIndex(41) = Windows.Forms.DialogResult.Yes Then
                    If grdSKU_Package.CurrentRow.Cells("ColumnIndex").Value IsNot Nothing Then
                        Dim index As String = grdSKU_Package.CurrentRow.Cells("ColumnIndex").Value.ToString
                        If objDB.Delete_Master(index) = True Then
                            W_MSG_Information_ByIndex(1)
                        End If
                    End If
                    Me.grdSKU_Package.Rows.RemoveAt(grdSKU_Package.CurrentRow.Index)
                End If
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Add new Package directly from SKU form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Package_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frmPck As New frmPackage
            frmPck.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Get customer name from Pop Up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()

            Dim tmpCustomer_Name As String = ""
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Name = frm.customerName
            tmpCustomer_Index = frm.Customer_Index
            If Not tmpCustomer_Name = "" Then
                Me.txtCustomer_Name.Text = tmpCustomer_Name
                Me.txtCustomer_Name.Tag = tmpCustomer_Index
            Else
                Me.txtCustomer_Name.Text = ""
                Me.txtCustomer_Name.Tag = ""
            End If
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' Get supplier name from Pop Up
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupplier.Click

        Try
            Dim frm As New frmSupplier_Popup
            frm.ShowDialog()
            Dim tmpSupplier_Name As String = ""
            Dim tmpSupplier_Index As String = ""
            tmpSupplier_Name = frm.SupplierName
            tmpSupplier_Index = frm.Supplier_Index
            If tmpSupplier_Name <> "" Then
                txtSupplierName.Text = tmpSupplier_Name
                txtSupplierName.Tag = tmpSupplier_Index
            Else
                txtSupplierName.Text = ""
            End If
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' Click to browse and add new picture
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddPic_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPic.Click
        Try

            ' Dim intFileNameLength As Integer = 0

            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                ' Display image
                picSKU.ImageLocation = OpenFileDialog1.FileName
                gstrLongFilePath = OpenFileDialog1.FileName
                Dim str() As String
                str = OpenFileDialog1.FileName.Split(".")
                'gstrFileName = txtSKUID.Text & "." & str(str.Length - 1) 'me._Sku_index.ToString & ".JPG"
                gstrFileName = Me._Sku_Index.ToString & ".JPG"
                picSKU.Tag = txtSKUID.Text & ".jpg"
                gstrAppPath = _DEFAULT_ImagePath & gstrFileName
                _isPic = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' Remove SKU picture
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnRemovePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePic.Click
        Try
            'IO.File.Delete(gstrAppPath)
            picSKU.Tag = ""
            picSKU.ImageLocation = ""
            gstrFileName = ""
            _isPic = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub






#End Region

#Region "EVENT CONTROL: CLICK AND KEY"
    Private Sub grdSKU_Package_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSKU_Package.CellClick
        _Ratio_Index = grdSKU_Package.Rows(grdSKU_Package.CurrentRow.Index).Cells("ColumnIndex").Value.ToString
        '   _Ration_Package = grdSKU_Package.Rows(grdSKU_Package.CurrentRow.Index).Cells("ColumnPackageIndex").Value.ToString
    End Sub

    Private Sub cbMainPackage_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs)
        '  txtMainPackage.Text = cbMainPackage.Text
    End Sub

    Private Sub txtVolumeX_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVolumeX.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVolumeX, e.KeyChar)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVolumeY_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVolumeY.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVolumeY, e.KeyChar)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVolumeZ_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVolumeZ.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVolumeZ, e.KeyChar)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVolume_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVolume.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVolume, e.KeyChar)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVolumeX_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolumeX.TextChanged
        Try
            'Dim Volume As Double = 0
            'Volume = Val(txtVolumeX.Text) * Val(txtVolumeY.Text) * Val(txtVolumeZ.Text)
            'txtVolume.Text = Volume.ToString

            CalculateToM3(_DimenstionRatio)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtVolumeY_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolumeY.TextChanged
        Try
            'Dim Volume As Double = 0
            'Volume = Val(txtVolumeX.Text) * Val(txtVolumeY.Text) * Val(txtVolumeZ.Text)
            'txtVolume.Text = Volume.ToString
            CalculateToM3(_DimenstionRatio)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVolumeZ_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolumeZ.TextChanged
        Try
            'Dim Volume As Double = 0
            'Volume = Val(txtVolumeX.Text) * Val(txtVolumeY.Text) * Val(txtVolumeZ.Text)
            'txtVolume.Text = Volume.ToString
            CalculateToM3(_DimenstionRatio)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtUnitWeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUnitWeight.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtUnitWeight, e.KeyChar)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtVolume_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolume.TextChanged
        'Try
        '    txtVolume.Text = ("0.0000")
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try
    End Sub

    '''' <summary>
    '''' Get values of H x W x L from ms_Package
    '''' </summary>
    '''' <param name="sender"></param>
    '''' <param name="e"></param>
    '''' <remarks></remarks>
    'Private Sub cbMainPackage_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbMainPackage.SelectedIndexChanged
    '    Try
    '        If cbMainPackage.SelectedValue IsNot Nothing Then
    '            Dim objPck As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
    '            Dim objDT As New DataTable
    '            Dim strIndex As String = cbMainPackage.SelectedValue.ToString
    '            Dim dbVolume As Double
    '            Dim dbVolumeX As Double
    '            Dim dbVolumeY As Double
    '            Dim dbVolumeZ As Double
    '            objPck.SelectData_ByIndex(strIndex)
    '            objDT = objPck.GetDataTable
    '            If objDT.Rows.Count > 0 Then
    '                With objDT.Rows(0)
    '                    If (.Item("Dimension_Wd").ToString = "") Or (.Item("Dimension_Len").ToString = "") Or (.Item("Dimension_Hi").ToString = "") Then
    '                        Me.txtVolumeX.Text = "0.0000"
    '                        Me.txtVolumeY.Text = "0.0000"
    '                        Me.txtVolumeZ.Text = "0.0000"
    '                        Me.txtVolume.Text = "0.0000"
    '                        Exit Sub
    '                    Else
    '                        dbVolumeX = .Item("Dimension_Wd").ToString
    '                        dbVolumeY = .Item("Dimension_Len").ToString
    '                        dbVolumeZ = .Item("Dimension_Hi").ToString
    '                        dbVolume = dbVolumeX * dbVolumeY * dbVolumeZ
    '                        Me.txtVolumeX.Text = dbVolumeX.ToString("0.0000")
    '                        Me.txtVolumeY.Text = dbVolumeY.ToString("0.0000")
    '                        Me.txtVolumeZ.Text = dbVolumeZ.ToString("0.0000")
    '                        Me.txtVolume.Text = dbVolume.ToString
    '                    End If

    '                End With
    '            End If
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
#End Region

    Private Sub btnPackage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPackage.Click
        Try
            Dim frm As New frmPackage_PopUp
            frm.isItem_Package = 0
            frm.ShowDialog()
            txtPackage.Text = frm.Description
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnPackageConvert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPackageConvert.Click
        Dim frm As New frmPackage_PopUp
        frm.isItem_Package = 0
        frm._DesName = _strtxtMainPackage
        frm.ShowDialog()
        txtPackageConvert.Text = frm.Description
    End Sub


    Private Sub tbProducts_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbProducts.SelectedIndexChanged

        txtMainPackage.Text = txtPackage.Text
        _strtxtMainPackage = txtPackage.Text

        'If _Package_Index <> "" Then
        '    Me.btnPackageConvert.Enabled = True
        '    Me.txtQty.Enabled = True
        '    Me.btnSave_Package.Enabled = True
        '    Me.btnDel_Package.Enabled = True
        'Else

        'End If
    End Sub


    Private Sub btnItem_Packge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnItem_Packge.Click
        Try

            '18-01-2010 KRIT Update : Comment การ Check ว่าจะเพิ่มหน่วยย่อยได้ต้องบันทึก SKU ก่อนเท่านั้น ออก
            'If Me._Sku_Index = "" Then
            '    W_MSG_Information_ByIndex("300001")
            '    Exit Sub
            'End If

            '18-01-2010 KRIT Update : เปลี่ยน form popup จาก frmItem_Package_PopUp เป็น frmPackage_PopUp (Work สุดๆ)
            ' Dim frm As New frmItem_Package_PopUp
            Dim frm As New frmPackage_PopUp
            frm.isItem_Package = 1
            ' frm.Sku_Index = Me._Sku_Index
            frm.ShowDialog()
            Me._Item_Package_Index = frm.Package_Index
            txtItem_Package.Text = frm.Description
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Sub SetWarehouse_NEW_VERSION()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_Warehouse_NEW_VERSION", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._USE_Warehouse_NEW_VERSION = objDT.Rows(0).Item("Config_Value").ToString

            Else
                Me._USE_Warehouse_NEW_VERSION = ""
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    Private Sub btnLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLocation.Click
        Try
            SetWarehouse_NEW_VERSION()
            'If _USE_Warehouse_NEW_VERSION = 0 Then
            'Dim frmVR As New frmVRWH_PutAway
            ''frmVR.DoubleCellClick = True
            ''frmVR.CheckMoseHover = False
            'frmVR.ShowDialog()
            'Me.txtLocation.Text = frmVR.Location_Alias
            'Me.txtLocation.Tag = frmVR.Location_Index
            'Else
            'Dim frmVR As New frmVRWH_PutAway
            'frmVR.ShowDialog()
            'Me.txtLocation.Text = frmVR.Location_Alias
            'Me.txtLocation.Tag = frmVR.Location_Index
            'End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Dim tmpLocation_Alias As String = ""
    Dim tmpLocation_Index As String = ""

    Private Sub txtLocation_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocation.Leave
        Try
            If txtLocation.Text = "" Then Exit Sub
            Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)

            Me.txtLocation.Tag = objClassDB.getLocation_Index(Me.txtLocation.Text)

            If txtLocation.Tag = "" Then
                W_MSG_Information_ByIndex("60")
                Me.txtLocation.Text = tmpLocation_Alias
                Me.txtLocation.Tag = tmpLocation_Index
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtLocation.Click
        Try
            If txtLocation.Text = "" Then Exit Sub
            tmpLocation_Alias = Me.txtLocation.Text
            tmpLocation_Index = Me.txtLocation.Tag.ToString
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub txtPackageWidth_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackageWidth.TextChanged
        Try
            'Dim Volume As Double = 0
            'Volume = Val(txtPackageWidth.Text) * Val(txtPackageLen.Text) * Val(txtPackageHeight.Text)
            'txtPackageVolumn.Text = Volume.ToString
            CalculateToM3Package(_DimenstionRatioPakcage)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtPackageLen_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackageLen.TextChanged
        Try
            'Dim Volume As Double = 0
            'Volume = Val(txtPackageWidth.Text) * Val(txtPackageLen.Text) * Val(txtPackageHeight.Text)
            'txtPackageVolumn.Text = Volume.ToString
            CalculateToM3Package(_DimenstionRatioPakcage)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPackageHeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPackageHeight.TextChanged
        Try
            'Dim Volume As Double = 0
            'Volume = Val(txtPackageWidth.Text) * Val(txtPackageLen.Text) * Val(txtPackageHeight.Text)
            'txtPackageVolumn.Text = Volume.ToString
            CalculateToM3Package(_DimenstionRatioPakcage)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPackageWidth_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPackageWidth.KeyPress, txtPackageHeight.KeyPress, txtPackageLen.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(sender, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(sender, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtWeight_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeight.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(sender, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtMin_Qty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty_Per_Pallet.KeyPress, txtMin_Weight.KeyPress, txtMin_Volume.KeyPress, txtMin_Qty.KeyPress, txtMax_Weight.KeyPress, txtMax_Volume.KeyPress, txtMax_Qty.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(sender, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPrice1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrice3.KeyPress, txtPrice2.KeyPress, txtPrice1.KeyPress, txtLifeTime_y.KeyPress, txtLifeTime_m.KeyPress, txtLifeTime_d.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(sender, e.KeyChar, False)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region "  Sub Cal M3 "
    Function getDigitM3(ByVal pstrDimensionType_Index As String) As Integer
        Dim objDB As New ms_DimensionType
        Try
            Return objDB.GetDigitDimensionTypeData(pstrDimensionType_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Function

    Function getRatioM3(ByVal pstrDimensionType_Index As String) As Double
        Dim objDB As New ms_DimensionType
        Dim _Ratio As Double = 1
        Try
            Return objDB.GetRatioDimensionTypeData(pstrDimensionType_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Function

    Sub CalculateToM3Package(ByVal Ratio As Double)

        Dim Volume As Double = 0

        Volume = (Val(txtPackageWidth.Text) * Val(txtPackageLen.Text) * Val(txtPackageHeight.Text)) / Ratio
        If Volume <> 0 Then
            txtPackageVolumn.Text = Math.Round(Volume, _DigitPackage).ToString
        End If


    End Sub
    Sub CalculateToM3(ByVal Ratio As Double)

        Dim Volume As Double = 0
        Volume = (Val(txtVolumeX.Text) * Val(txtVolumeY.Text) * Val(txtVolumeZ.Text)) / Ratio
        If Volume <> 0 Then
            txtVolume.Text = Math.Round(Volume, _Digit).ToString
        End If

    End Sub
#End Region
#Region "   DATAGRID CHK CURRENCY   "
    ''' <summary>
    ''' Add Date : 24/04/2010
    ''' Add By   : HUN
    ''' Remark   : For Check Update SkuRatio and Chk Currentcy
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>

    Private Sub grdSKU_Package_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSKU_Package.EditingControlShowing
        Try
            Dim strName As String = grdSKU_Package.Columns(grdSKU_Package.CurrentCell.ColumnIndex).Name
            Select Case strName.ToUpper
                Case Nothing
                    Exit Sub
                Case "COL_DIMENSION_HI", "COL_DIMENSION_WD", "COL_DIMENSION_LEN", "COL_WEIGHT", "COLUMNRATIO"
                    Dim txtEdit As TextBox = e.Control
                    RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                    AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdSKU_Package.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdSKU_Package.Columns("Col_Dimension_Hi").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSKU_Package.Columns("Col_Dimension_Wd").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSKU_Package.Columns("Col_Dimension_Len").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSKU_Package.Columns("col_Weight").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSKU_Package.Columns("COLUMNRATIO").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)

            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function Check_GrdKeyPress(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdSKU_Package.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdSKU_Package.CurrentRow.Cells(Column_Index).EditedFormattedValue
                                If strData.IndexOf(".") >= 0 Then Return True
                            Else
                                Return True
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return True
                    End If
                Case 1
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
                        Return False
                    Else
                        Return True
                    End If
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Function


#End Region




    Private Sub cboDimensionType_Ratio_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDimensionType_Ratio.SelectionChangeCommitted
        Try
            _DimenstionRatioPakcage = getRatioM3(cboDimensionType_Ratio.SelectedValue)
            _DigitPackage = getDigitM3(cboDimensionType_Ratio.SelectedValue)
            CalculateToM3Package(_DimenstionRatioPakcage)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboDimensionType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDimensionType.SelectionChangeCommitted
        Try
            _DimenstionRatio = getRatioM3(cboDimensionType.SelectedValue)
            _Digit = getDigitM3(cboDimensionType.SelectedValue)
            CalculateToM3(_DimenstionRatio)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cbProductClass_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbProductClass.SelectedValueChanged
        Try
            If Me.cbProductClass.SelectedValue IsNot Nothing Then
                SetProductSubClassByIndex(Me.cbProductClass.SelectedValue.ToString)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmProductSKU_Des_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigProduct_SKU
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2001)
                    oFunction.SW_Language_Column(Me, Me.grdSKU_Package, 2001)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cb_IsControlAsset_CheckStateChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cb_IsControlAsset.CheckStateChanged
        Try
            If cb_IsControlAsset.CheckState = CheckState.Checked Then
                If Not New ms_SKU(ms_SKU.enuOperation_Type.SEARCH).CheckQtyBalance(txtSKUID.Text) Then
                    cb_IsControlAsset.CheckState = CheckState.Unchecked
                    W_MSG_Error("มีสินค้าอยู่ในระบบไม่สามารถบันทึก Serial ได้")
                End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class