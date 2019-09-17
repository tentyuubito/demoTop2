'Dong_kk
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master.ValidateValueType
Imports System.Windows.Forms
Public Class frmProduct_Popup

    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        NULL
    End Enum

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal Customer_Index As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type
        _Customer_Index = Customer_Index
        '    mObjSearchCri = New uctSearchCri(mMasterType)

    End Sub

    Private _ConditionSql As String
    Public Property ConditionSql() As String
        Get
            Return _ConditionSql
        End Get
        Set(ByVal value As String)
            _ConditionSql = value
        End Set
    End Property

    Private _Picking_Bom_Filter As String = ""
    Public Property Picking_Bom_Filter() As String
        Get
            Return _Picking_Bom_Filter
        End Get
        Set(ByVal value As String)
            _Picking_Bom_Filter = value
        End Set
    End Property
    Private _Sku_Index As String = ""
    Public ReadOnly Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
    End Property

    Private _Sku_ID As String = ""
    Public ReadOnly Property Sku_ID() As String
        Get
            Return _Sku_ID
        End Get
    End Property

    Private _Sku_Des_th As String = ""
    Public ReadOnly Property Sku_Des_th() As String
        Get
            Return _Sku_Des_th
        End Get
    End Property

    Private _Sku_Des_eng As String = ""
    Public ReadOnly Property Sku_Des_eng() As String
        Get
            Return _Sku_Des_eng
        End Get
    End Property

    'Private _Qty_Per_Pallet As Double = 0.0
    Private _Qty_Per_Pallet As Double
    Public ReadOnly Property Qty_Per_Pallet() As Double
        Get
            Return _Qty_Per_Pallet
        End Get
    End Property

    Private _PalletType_Index As String = ""

    Property PalletType_Index() As String
        Get
            Return _PalletType_Index
        End Get
        Set(ByVal value As String)
            _PalletType_Index = value
        End Set
    End Property
   

    Private _Pallet_Name As String = ""
  
    Property Pallet_Name() As String
        Get
            Return _Pallet_Name
        End Get
        Set(ByVal value As String)
            _Pallet_Name = value
        End Set
    End Property

    Private _Order_Index As String = ""
    Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property

    Private _OrderItem_Index As String = ""
    Property OrderItem_Index() As String
        Get
            Return _OrderItem_Index
        End Get
        Set(ByVal value As String)
            _OrderItem_Index = value
        End Set
    End Property



    Private _Customer_Index As String = ""
    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property


    Private _Supplier_Index As String = ""
    Public Property Supplier_Index() As String
        Get
            Return _Supplier_Index
        End Get
        Set(ByVal value As String)
            _Supplier_Index = value
        End Set
    End Property


    Private _USE_PRODUCT_CUSTOMER As Boolean
    Private _USE_PRODUCT_CUSTOMERREFID As Boolean
    'Public Property Config() As Boolean
    '    Get
    '        Return _Config
    '    End Get
    '    Set(ByVal value As Boolean)
    '        _Config = value
    '    End Set
    'End Property

    Private _Package_Index As String
    Public Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
        Set(ByVal value As String)
            _Package_Index = value
        End Set
    End Property

    Private Sub getSku()
        Dim objClassDB As New ms_SKU_Update(ms_SKU_Update.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim AddWhereString As String = ""
        Dim strCusRefSku_Id As String = ""
        Try
            '02-02-2010 ja update Like Search
            cboCondition.BeginUpdate()
            'cboCondition.Items.Clear()
            If Not Trim(Me.txtCondition.Text) = "" Or Not IsDBNull(cbProductType.SelectedValue) Then
                Select Case Me.cboCondition.SelectedIndex
                    Case 0
                        ' รหัสลูกค้า
                        AddWhereString = " AND ms_Sku.Sku_Id Like '%" & Me.txtCondition.Text.Trim.Replace("'", "''") & "%'"
                    Case 1
                        ' รหัสอ้างอิงลูกค้า 
                        strCusRefSku_Id = Me.txtCondition.Text.Trim.Replace("'", "''")
                        Select Case _USE_PRODUCT_CUSTOMERREFID
                            Case False
                                AddWhereString = " AND ms_Sku.Str4 Like '%" & Me.txtCondition.Text.Trim.Replace("'", "''") & "%'"
                        End Select
                    Case 2
                        ' ชื่อลูกค้า 
                        AddWhereString = " AND ms_Sku.Str3 Like '%" & Me.txtCondition.Text.Trim.Replace("'", "''") & "%'"
                    Case 3
                        AddWhereString = " AND ms_Sku.Str2 Like '%" & Me.txtCondition.Text.Trim.Replace("'", "''") & "%'"
                End Select
            End If

            If Not String.IsNullOrEmpty(cbProductType.SelectedValue) Then AddWhereString &= " AND ms_ProductType.ProductType_Index = '" & cbProductType.SelectedValue & "' "

            'If Picking_Bom_Filter = "Filter" Then 'เอา เฉพาะ sku ที่เป็น BOM 
            '    AddWhereString &= " AND (PackingBom_Index <> '' ) " ' and ms_SKU.str10 = 4
            'ElseIf Picking_Bom_Filter = "NoneFilter" Then
            '    AddWhereString &= " AND (PackingBom_Index is  null)  "
            'End If

            If Picking_Bom_Filter = "Filter" Then 'เอา เฉพาะ sku ที่เป็น BOM 
                AddWhereString &= " AND ms_Sku.Sku_Index in ( select Sku_Index from tb_PackingBom where status_id not in (-1))" '" AND (PackingBom_Index <> '' ) " ' and ms_SKU.str10 = 4
            ElseIf Picking_Bom_Filter = "NoneFilter" Then
                AddWhereString &= " AND ms_Sku.Sku_Index not in ( select Sku_Index from tb_PackingBom where status_id not in (-1))"
                'AddWhereString &= " AND (PackingBom_Index is  null)  "
            End If

            Dim model As New Model_Condition_Popup
            model.Eye = txtEye.Text.ToString
            model.Add = txtAdd.Text.ToString
            model.Tilted = txtTilted.Text.ToString
            model.Color = txtColor.Text.ToString
            model.Degree = txtDegree.Text.ToString
            model.BC = txtBC.Text.ToString
            model.VMI = txtVMI.Text.ToString
            model.Generation = txtGeneration.Text.ToString
            model.Brand = txtBrand.Text.ToString

            AddWhereString &= _ConditionSql
            cboCondition.EndUpdate()

            If Not String.IsNullOrEmpty(Me.Order_Index) Then
                objClassDB.GetPopup_Search_ByOrder_Index(Me.Order_Index)
                objDT = objClassDB.DataTable
                Me.grdPopupList.Refresh()
                Me.grdPopupList.DataSource = objDT
                Me.grdPopupList.Update()
                Exit Sub
            End If

            '--- กรองลูกค้า

            If Not String.IsNullOrEmpty(Me._Supplier_Index) Then
                AddWhereString &= " AND ms_Supplier.Supplier_Index = '" & Me._Supplier_Index & "'"
            End If

            Select Case _USE_PRODUCT_CUSTOMERREFID ' ใช้ระบบรหัสอ้างอิงลูกค้า
                Case True
                    objClassDB.getPopup_Search_ByCustomerRefID(AddWhereString, Me._Customer_Index, model, strCusRefSku_Id)
                Case False
                    If Not String.IsNullOrEmpty(Me._Customer_Index) Then
                        Select Case _USE_PRODUCT_CUSTOMER
                            Case True
                                objClassDB.getPopup_Search_ByCustomer(AddWhereString, Me._Customer_Index, model)
                            Case False
                                objClassDB.getPopup_Search_ByCustomer(AddWhereString, "", model)
                        End Select
                    Else
                        objClassDB.getPopup_Search_ByCustomer(AddWhereString, "", model)
                    End If
            End Select


            objDT = objClassDB.DataTable
            Me.grdPopupList.Refresh()
            Me.grdPopupList.DataSource = objDT
            Me.grdPopupList.Update()


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Private Sub grdPopupList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPopupList.CellDoubleClick
        If e.RowIndex <= -1 Then Exit Sub
        Submit()
        Me.Visible = False
    End Sub

    Private Sub frmProduct_Popup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim objLang As New W_Language
            objLang.SwitchLanguage(Me, 2051)
            objLang.SW_Language_Column(Me, Me.grdPopupList, 2051)

            grdPopupList.AutoGenerateColumns = False
            Dim objSkuConfigDB As New config_CustomSetting
            _USE_PRODUCT_CUSTOMER = objSkuConfigDB.getConfig_Key_USE("USE_PRODUCT_CUSTOMER")
            _USE_PRODUCT_CUSTOMERREFID = objSkuConfigDB.getConfig_Key_USE("USE_PRODUCT_CUSTOMERREFID")
            Me.cboCondition.SelectedIndex = 0
            Me.getSku()
            Me.getProductType()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getSku()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            _Sku_Index = ""
            _Sku_ID = ""
            _Sku_Des_th = ""
            _Sku_Des_eng = ""
            _Qty_Per_Pallet = 0
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Try
            Submit()
            Me.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub Submit()
        Try
            With Me.grdPopupList
                If .Rows.Count > 0 Then
                    _Sku_Index = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("System_Index").Value, GetType(String))
                    _Sku_ID = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("ColSku_Id").Value, GetType(String))
                    _Sku_Des_th = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("Product_Name").Value, GetType(String))
                    _Sku_Des_eng = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("Product_Name_eng").Value, GetType(String))
                    _Package_Index = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("col_Package_Index").Value, GetType(String))
                    _Qty_Per_Pallet = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("Col_Qty_Per_Pallet").Value, GetType(Double))
                    _Pallet_Name = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("Col_Pallet_Name").Value, GetType(String))
                    _PalletType_Index = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("Col_PalletType_Index").Value, GetType(String))
                    _OrderItem_Index = IsNullOrEmptyOrDBNullOrNothingGetData(.CurrentRow.Cells("col_OrderItem_Index").Value, GetType(String))
                End If
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    'Function DataGridReturn_Data(ByVal DataGridRowData As Object) As String
    '    Dim DataReturn As String = ""
    '    Try
    '        If IsDBNull(DataGridRowData) Then
    '            DataReturn = DataGridRowData.ToString
    '        Else
    '            If Not String.IsNullOrEmpty(DataGridRowData) Then
    '                DataReturn = DataGridRowData.Value
    '            End If
    '        End If
    '        Return DataReturn
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function



    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmProductSKU_Des
            frm.ShowDialog()
            Me.getSku()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtCondition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCondition.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            Me.getSku()
        End If
    End Sub

    Private Sub frmProduct_Popup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigProduct_Popup
                    frm.ShowDialog()
                    Dim objLang As New W_Language
                    objLang.SwitchLanguage(Me, 2051)
                    objLang.SW_Language_Column(Me, Me.grdPopupList, 2051)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getProductType()
        Dim objClassDB As New ms_ProductType(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try

            'objClassDB.SearchData_Click("", "")
            'objDT = objClassDB.DataTable

            'Dim drNew As DataRow
            'drNew = objDT.NewRow
            'objDT.Rows.Add(drNew)

            'cbProductType.DisplayMember = "Description"
            'cbProductType.ValueMember = "ProductType_Index"
            'cbProductType.DataSource = objDT

            'cbProductType.SelectedIndex = objDT.Rows.Count - 1

            objDT.Columns.Add("Description")
            objDT.Columns.Add("ProductType_Index")
            Dim drNew As DataRow
            drNew = objDT.NewRow
            drNew("Description") = "แสดงทั้งหมด"
            drNew("ProductType_Index") = ""
            objDT.Rows.Add(drNew)

            objClassDB.SearchData_Click("", "")

            For Each item As DataRow In objClassDB.DataTable.Rows
                drNew = objDT.NewRow
                drNew("Description") = item.Item("Description").ToString
                drNew("ProductType_Index") = item.Item("ProductType_Index").ToString
                objDT.Rows.Add(drNew)
            Next

            cbProductType.DisplayMember = "Description"
            cbProductType.ValueMember = "ProductType_Index"
            cbProductType.DataSource = objDT

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

End Class

Public Class Model_Condition_Popup
    Private _Eye As String
    Public Property Eye() As String
        Get
            Return _Eye
        End Get
        Set(ByVal value As String)
            _Eye = value
        End Set
    End Property

    Private _Add As String
    Public Property Add() As String
        Get
            Return _Add
        End Get
        Set(ByVal value As String)
            _Add = value
        End Set
    End Property

    Private _Tilted As String
    Public Property Tilted() As String
        Get
            Return _Tilted
        End Get
        Set(ByVal value As String)
            _Tilted = value
        End Set
    End Property

    Private _Color As String
    Public Property Color() As String
        Get
            Return _Color
        End Get
        Set(ByVal value As String)
            _Color = value
        End Set
    End Property

    Private _Degree As String
    Public Property Degree() As String
        Get
            Return _Degree
        End Get
        Set(ByVal value As String)
            _Degree = value
        End Set
    End Property

    Private _BC As String
    Public Property BC() As String
        Get
            Return _BC
        End Get
        Set(ByVal value As String)
            _BC = value
        End Set
    End Property

    Private _VMI As String
    Public Property VMI() As String
        Get
            Return _VMI
        End Get
        Set(ByVal value As String)
            _VMI = value
        End Set
    End Property

    Private _Generation As String
    Public Property Generation() As String
        Get
            Return _Generation
        End Get
        Set(ByVal value As String)
            _Generation = value
        End Set
    End Property

    Private _Brand As String
    Public Property Brand() As String
        Get
            Return _Brand
        End Get
        Set(ByVal value As String)
            _Brand = value
        End Set
    End Property
End Class