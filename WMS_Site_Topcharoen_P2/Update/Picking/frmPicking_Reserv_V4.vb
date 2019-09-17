Imports WMS_std_master_DataLayer
Imports WMS_std_Formula
Imports WMS_std_master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_master.W_Function
Imports WMS_STD_Master
Imports WMS_STD_OUTB_Datalayer

Public Class frmPicking_Reserv_V4

#Region " Properties "

    Dim StatusWithdraw_format As Withdraw_format
    Dim StatusWithdraw_Type As Withdraw_Type
    Private _BoolPickSome As Boolean = False

    Private Enum Withdraw_Type
        CUSTOM = 0
        AUTO = 1
    End Enum

    Private Enum Withdraw_format
        CUSTOM = 0
        FIFO = 1
        LIFO = 2
        FEFO = 3
        SERIAL = 4
        PICKFACE = 5
    End Enum
    Private dtSelect_Location As New DataTable
    Public Property DTtemp() As DataTable
        Get
            Return dtSelect_Location
        End Get
        Set(ByVal value As DataTable)
            dtSelect_Location = value
        End Set
    End Property
    Private _WithdrawType As Integer = 0
    Public Property WithdrawType() As Integer
        Get
            Return _WithdrawType
        End Get
        Set(ByVal value As Integer)
            _WithdrawType = value
        End Set
    End Property

    Public _DocumentType_Index As String

    Enum Operation
        Withdraw
        Transfer
        TransferOwner
    End Enum

    Private _pCaseOperation As Operation

    Public Sub New(ByVal DocumentType_Index As String, ByVal pDOC_Index As String, ByVal caseOperation As Operation)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _DocumentType_Index = DocumentType_Index
        Select Case caseOperation
            Case Operation.Withdraw
                _withdraw_index = pDOC_Index
                _pPlan_Process = -9
                _pCaseOperation = caseOperation
            Case Operation.Transfer
                _Transferstatus_Index = pDOC_Index
                _pPlan_Process = 5
                _pCaseOperation = caseOperation
            Case Operation.TransferOwner
                _TransferOwner_Index = pDOC_Index
                _pPlan_Process = -9
                _pCaseOperation = caseOperation
        End Select

    End Sub

    Public Sub New(ByVal DocumentType_Index As String, ByVal pDoc_Index As String, ByVal pDocumentPlan_No As String, ByVal pDocumentPlan_Index As String, ByVal pDocumentPlanItem_Index As String, ByVal pPlan_Process As String, ByVal caseOperation As Operation)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        _DocumentType_Index = DocumentType_Index
        Select Case caseOperation
            Case Operation.Withdraw
                _withdraw_index = pDoc_Index
            Case Operation.TransferOwner
                _TransferOwner_Index = pDoc_Index
            Case Else
                Throw New Exception("Not Support !!")
        End Select

        _pDocumentPlan_Index = pDocumentPlan_Index
        _pDocumentPlanItem_Index = pDocumentPlanItem_Index
        _pPlan_Process = pPlan_Process
        _pDocumentPlan_No = pDocumentPlan_No
        _pCaseOperation = caseOperation
    End Sub

    Private _Transferstatus_Index As String = ""
    Private _TransferOwner_Index As String = ""
    Private _pDocumentPlan_Index As String = ""
    Private _pDocumentPlanItem_Index As String = ""
    Private _pPlan_Process As Integer = -9
    Private _pDocumentPlan_No As String = ""

    Private Qty_Auto As Decimal = 0
    Private chkQty_Auto As Boolean = False
    Dim _boolReserv As Boolean = False
    Private _SKU_index As String = ""
    Private _withdraw_index As String = ""

    Private _objTmpWithDrawItem As DataTable



    Private _VisibleAutoPick As Boolean = True
    Public Property VisibleAutoPick() As Boolean
        Get
            Return _VisibleAutoPick
        End Get
        Set(ByVal value As Boolean)
            _VisibleAutoPick = value
        End Set
    End Property



    Private _AgeRemain As Integer
    Public Property AgeRemain() As Integer
        Get
            Return _AgeRemain
        End Get
        Set(ByVal value As Integer)
            _AgeRemain = value
        End Set
    End Property

    Private _UseAgeRemain As Boolean = False
    Public Property UseAgeRemain() As Boolean
        Get
            Return _UseAgeRemain
        End Get
        Set(ByVal value As Boolean)
            _UseAgeRemain = value
        End Set
    End Property

    Private _Exp_Date As String
    Public Property Exp_Date() As String
        Get
            Return _Exp_Date
        End Get
        Set(ByVal value As String)
            _Exp_Date = value
        End Set
    End Property



    Private _isExp_Date As Boolean
    Public Property isExp_Date() As Boolean
        Get
            Return _isExp_Date
        End Get
        Set(ByVal value As Boolean)
            _isExp_Date = value
        End Set
    End Property



    Public Property objTmpWithDrawItem() As DataTable
        Get
            Return _objTmpWithDrawItem
        End Get
        Set(ByVal value As DataTable)
            _objTmpWithDrawItem = value
        End Set
    End Property


    Public Property withdraw_index() As String
        Get
            Return _withdraw_index
        End Get
        Set(ByVal value As String)
            _withdraw_index = value
        End Set
    End Property

    Private _WithDraw_Date As Date = Now.Date
    Public Property WithDraw_Date() As Date
        Get
            Return _WithDraw_Date
        End Get
        Set(ByVal value As Date)
            _WithDraw_Date = value
        End Set
    End Property


    Private _Consignee_Index As String = ""
    Public Property Consignee_Index() As String
        Get
            Return _Consignee_Index
        End Get
        Set(ByVal value As String)
            _Consignee_Index = value
        End Set
    End Property


    Private _Consignee_Name As String = ""
    Public Property Consignee_Name() As String
        Get
            Return _Consignee_Name
        End Get
        Set(ByVal value As String)
            _Consignee_Name = value
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

    Private _Customer_Id As String = ""
    Public Property Customer_Id() As String
        Get
            Return _Customer_Id
        End Get
        Set(ByVal value As String)
            _Customer_Id = value
        End Set
    End Property

    Private _Customer_Name As String = ""
    Public Property Customer_Name() As String
        Get
            Return _Customer_Name
        End Get
        Set(ByVal value As String)
            _Customer_Name = value
        End Set
    End Property

    Public Property Sku_Index() As String
        Get
            Return _SKU_index
        End Get
        Set(ByVal value As String)
            _SKU_index = value
        End Set
    End Property

    Private _Sku_Id As String = ""
    Public Property Sku_Id() As String
        Get
            Return _Sku_Id
        End Get
        Set(ByVal value As String)
            _Sku_Id = value
        End Set
    End Property

    Private _Sku_Name As String = ""
    Public Property Sku_Name() As String
        Get
            Return _Sku_Name
        End Get
        Set(ByVal value As String)
            _Sku_Name = value
        End Set
    End Property

    Private _Max_Plan_Reserv As Decimal = 0
    Public Property Max_Plan_Reserv() As Decimal
        Get
            Return _Max_Plan_Reserv
        End Get
        Set(ByVal value As Decimal)
            _Max_Plan_Reserv = value
        End Set
    End Property

    Private _Qty_Reserv As Decimal = 0
    Public Property Qty_Reserv() As Decimal
        Get
            Return _Qty_Reserv
        End Get
        Set(ByVal value As Decimal)
            _Qty_Reserv = value
        End Set
    End Property

    Private _DocumentPlan_Process As Integer = -9
    Private _DocumentPlan_Index As String = ""

    Public Property DocumentPlan_Process() As Integer
        Get
            Return (Me._DocumentPlan_Process)
        End Get
        Set(ByVal value As Integer)
            Me._DocumentPlan_Process = value
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
    Private _ItemStatus_Index As String = ""
    Public Property ItemStatus_Index() As String
        Get
            Return _ItemStatus_Index
        End Get
        Set(ByVal value As String)
            _ItemStatus_Index = value
        End Set
    End Property

    Private _Plot As String = ""
    Public Property Plot() As String
        Get
            Return _Plot
        End Get
        Set(ByVal value As String)
            _Plot = value
        End Set
    End Property

    Private _ERP_location As String = ""
    Public Property ERP_location() As String
        Get
            Return _ERP_location
        End Get
        Set(ByVal value As String)
            _ERP_location = value
        End Set
    End Property

    Private _IS_TransferOwner As Boolean = False
    Public Property IS_TransferOwner() As Boolean
        Get
            Return _IS_TransferOwner
        End Get
        Set(ByVal value As Boolean)
            _IS_TransferOwner = value
        End Set
    End Property


    Private _Package_Index_Begin As String = ""
    Public Property Package_Index_Begin() As String
        Get
            Return _Package_Index_Begin
        End Get
        Set(ByVal value As String)
            _Package_Index_Begin = value
        End Set
    End Property



#End Region


#Region "FROM LOAD"
    Private Sub frmWithdraw_main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2)
            oFunction.SW_Language_Column(Me, Me.grdShowCustom, 2)
            oFunction.SW_Language_Column(Me, Me.grdWithdrawItemLocation, 2)


            grdShowCustom.AutoGenerateColumns = False
            grdWithdrawItemLocation.AutoGenerateColumns = False

            '--- SET SELECT COMBO
            cboAutoPicking.SelectedIndex = 0
            cboConditionExlife.SelectedIndex = 0
            cboDateExlife.SelectedIndex = 0
            cboConditionlife.SelectedIndex = 0
            cboDatelife.SelectedIndex = 0
            cboConditionWeight.SelectedIndex = 0

            ' StatusWithdraw_format = Withdraw_format.Custom
            '--- Load Combo Box
            Me.GetProductType()
            Me.GetItemStatus()
            Me.GetWarehouse()
            Me.GetRoom()
            Me.GetZone()
            Me.GetLocationType()
            Me.GetReference()


            Dim objgetconfigReWegiht As New config_CustomSetting
            If objgetconfigReWegiht.getConfig_Key_USE("USE_AUTO_REWEIGHTBALANCE") Then
                Dim ObjReWeight As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                ObjReWeight.ReWeightBalance()
            Else

            End If


            'LoadPackageToGrid()

            '--- Visible Close Column
            ' config_Withdraw_Reserv()
            config_Withdraw_ReservItem()


            USE_BLOCK_LOT()

            txtCustomer_Name.Text = Me._Customer_Name
            txtCustomer_Id.Text = Me._Customer_Id
            txtCustomer_Id.Tag = Me._Customer_Index
            StatusWithdraw_Type = 0

            txtConsignee_Name.Tag = _Consignee_Index
            txtConsignee_Name.Text = _Consignee_Name

            If _Consignee_Name <> "" Then
                chkConsigneeIndex.Checked = True
            End If



            Me.getcboHandlingType()
            Me.getcboHandlingType2()
            txtSku_Name.Text = Me._Sku_Name
            txtSKU_ID.Text = Me._Sku_Id
            If _Sku_Id <> "" Then
                Me.getPackage_Sku()
            End If


            txtQty_Reserv.Text = Me._Qty_Reserv

            If _ItemStatus_Index <> "" Then
                cboItemStatus.SelectedValue = _ItemStatus_Index
            End If
            If Not String.IsNullOrEmpty(_ItemStatus_Index) Then
                If cboItemStatus.SelectedValue Is Nothing Then
                    W_MSG_Information("WH Code ไม่ตรงกันกรุณาตรวจสอบ")
                    Me.Close()
                End If
            End If

            If _Plot <> "" Then
                chkLot.Checked = True
                txtLot.Text = _Plot
            End If
            If _ERP_location <> "" Then
                txt_ERP_location.Text = _ERP_location
                chk_ERP_location.Checked = True
            End If

            If String.IsNullOrEmpty(_Package_Index_Begin) = False Then
                cboPackage.SelectedValue = _Package_Index_Begin
            End If

            setLocationControl_By_Config()





            rdbAutoPicking.Visible = _VisibleAutoPick
            cboAutoPicking.Visible = _VisibleAutoPick
            'rdbCustom.Checked = Not _VisibleAutoPick


            If _UseAgeRemain And _AgeRemain >= 0 Then
                chkExLife.Checked = True
                txtConditionKeyExlife.Text = _AgeRemain
                cboDateExlife.SelectedIndex = 1
            End If

            If IsDate(_Exp_Date) Then
                If IsDate(_Exp_Date) Then
                    chkExDate.Checked = True
                    dtpFromDate_Ex.Value = CDate(_Exp_Date)
                    dtpToDate_Ex.Value = CDate(_Exp_Date)
                Else
                    chkExDate.Checked = True
                    dtpFromDate_Ex.Value = CDate(_Exp_Date)
                    dtpToDate_Ex.Value = CDate(_Exp_Date).AddYears(1)
                End If
            Else

            End If
 





        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Function USE_BLOCK_LOT() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_BLOCK_LOT", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim intBlock As Integer = 0
            If objDT.Rows.Count > 0 Then
                col_Block.Visible = True
                Return True
            Else
                Return False

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function

    Private Sub config_Withdraw_ReservItem()
        Dim objClassDB As New PICKING(PICKING.enmPicking_Type.CUSTOM)
        Dim objDT As DataTable = New DataTable

        Try
            ' Grid Custom
            objDT = objClassDB.getFieldConfigWithDrawAsset_Reserv(1)

            For iCol As Integer = 0 To grdShowCustom.Columns.Count - 1
                Dim odr() As DataRow
                odr = objDT.Select("Controls_Name = '" & grdShowCustom.Columns(iCol).Name & "'")
                If odr.Length > 0 Then
                    grdShowCustom.Columns(iCol).Visible = False
                End If
            Next

            ' Grid WithDraw
            objDT = objClassDB.getFieldConfigWithDrawAsset_Reserv(2)

            For iCol As Integer = 0 To grdWithdrawItemLocation.Columns.Count - 1
                Dim odr() As DataRow
                odr = objDT.Select("Controls_Name = '" & grdWithdrawItemLocation.Columns(iCol).Name & "'")
                If odr.Length > 0 Then
                    grdWithdrawItemLocation.Columns(iCol).Visible = False
                End If
            Next


        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub


#End Region

#Region "GET COMBO BOX"
    Private Sub GetReference()
        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getReference_Order(2)
            objDT = objClassDB.DataTable

            Dim str(8) As String
            str(8) = "-11"

            ' Select Case W_Module.WV_Language.Current_Language
            '  Case W_Language.enmLanguage.Thai
            str(5) = "แสดงทุกรายการ"
            ' Case W_Language.enmLanguage.English
            str(6) = "Show All"
            '  End Select

            objDT.Rows.Add(str)


            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    cboReference.DisplayMember = "Description_th"
                Case enmLanguage.English
                    cboReference.DisplayMember = "Description_en"
            End Select
            cboReference.ValueMember = "ValueMember"
            cboReference.DataSource = objDT

            cboReference.SelectedIndex = cboReference.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetZone()
        Dim objClassDB As New ms_Zone(ms_Zone.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            Dim str(7) As String
            str(0) = "-11"
            str(2) = "แสดงทุกรายการ"
            objDT.Rows.Add(str)

            cboZone.DisplayMember = "Description"
            cboZone.ValueMember = "Zone_Index"
            cboZone.DataSource = objDT

            cboZone.SelectedIndex = cboZone.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetLocationType()
        Dim objClassDB As New ms_LocationType
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            Dim str(7) As String
            str(0) = "-11"
            str(2) = "แสดงทุกรายการ"
            objDT.Rows.Add(str)

            cboLocationType.DisplayMember = "Description"
            cboLocationType.ValueMember = "LocationType_Index"
            cboLocationType.DataSource = objDT

            cboLocationType.SelectedIndex = cboLocationType.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetRoom()
        Dim objClassDB As New ms_Room(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SelectByWareHouse(cboWareHouse.SelectedValue.ToString)
            objDT = objClassDB.DataTable

            Dim str(7) As String
            str(0) = "-11"
            str(5) = "แสดงทุกรายการ"
            objDT.Rows.Add(str)

            cboRoom.DisplayMember = "Description"
            cboRoom.ValueMember = "Room_Index"
            cboRoom.DataSource = objDT

            cboRoom.SelectedIndex = cboRoom.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetWarehouse()
        Dim objClassDB As New ms_Warehouse(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            Dim str(11) As String
            str(0) = "-11"
            str(1) = 0
            str(2) = 0
            str(3) = 0
            str(4) = "แสดงทุกรายการ"
            objDT.Rows.Add(str)

            cboWareHouse.DisplayMember = "Description"
            cboWareHouse.ValueMember = "Warehouse_Index"
            cboWareHouse.DataSource = objDT


            cboWareHouse.SelectedIndex = cboWareHouse.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetProductType()
        Dim objClassDB As New ms_ProductType(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            Dim str(6) As String
            str(4) = "-11"
            str(5) = " "
            str(6) = "แสดงทุกรายการ"
            objDT.Rows.Add(str)

            cboProductType.DisplayMember = "Description"
            cboProductType.ValueMember = "ProductType_Index"
            cboProductType.DataSource = objDT

            cboProductType.SelectedIndex = cboProductType.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetItemStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatusByDocumentType_Index(Me._DocumentType_Index)
            objDT = objClassDB.DataTable

            Dim str(1) As String
            str(0) = "-11"
            str(1) = "แสดงทุกรายการ"
            objDT.Rows.Add(str)

            With Me.cboItemStatus
                .DisplayMember = "ItemStatus"
                .ValueMember = "ItemStatus_Index"
                .DataSource = objDT
            End With

            cboItemStatus.SelectedIndex = cboItemStatus.Items.Count - 1


            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me.txtCustomer_Id.Tag.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me.txtCustomer_Id.Tag = objDT.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString
            Else
                Me.txtCustomer_Id.Tag = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
#End Region

#Region "SELECT PICKING"
    Private Sub rdbCustom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustom.CheckedChanged
        Try
            txtQty_Reserv.ReadOnly = True
            txtQty_Reserv.BackColor = Color.WhiteSmoke
            ' txtQty_Reserv.Text = 0
            'grdShowCustom.Rows.Clear()
            StatusWithdraw_format = Withdraw_format.CUSTOM


            Me.grbWithDrawItemLocation.Location = New System.Drawing.Point(9, 560)
            Me.grbWithDrawItemLocation.Size = New System.Drawing.Size(995, 192)

            StatusWithdraw_Type = Withdraw_Type.CUSTOM
            'setSortColumn()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub rdbAutoPicking_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAutoPicking.CheckedChanged
        Try
            txtQty_Reserv.ReadOnly = False
            txtQty_Reserv.BackColor = Color.White
            cboAutoPicking.Enabled = rdbAutoPicking.Checked
            'grdShowCustom.Rows.Clear()

            StatusWithdraw_Type = Withdraw_Type.AUTO
            cboAutoPicking.SelectedIndex = 0
            StatusWithdraw_format = Withdraw_format.FIFO

            Me.grbWithDrawItemLocation.Location = New System.Drawing.Point(7, 186)
            Me.grbWithDrawItemLocation.Size = New System.Drawing.Size(995, 445)


            'setSortColumn()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub setSortColumn()
        Try
            Dim column As New DataGridViewColumn
            Select Case StatusWithdraw_Type
                Case Withdraw_Type.AUTO
                    chk_WithDraw.Visible = False
                    For Each column In grdShowCustom.Columns
                        column.SortMode = DataGridViewColumnSortMode.NotSortable
                    Next
                Case Withdraw_Type.CUSTOM
                    chk_WithDraw.Visible = True
                    For Each column In grdShowCustom.Columns
                        column.SortMode = DataGridViewColumnSortMode.Automatic
                    Next
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rdbFIFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'grdShowCustom.Rows.Clear()
            StatusWithdraw_format = Withdraw_format.FIFO
            StatusWithdraw_Type = Withdraw_Type.AUTO
            'setSortColumn()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub rdbLIFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'grdShowCustom.Rows.Clear()
            StatusWithdraw_format = Withdraw_format.LIFO
            StatusWithdraw_Type = Withdraw_Type.AUTO
            'setSortColumn()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub rdbFEFO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'grdShowCustom.Rows.Clear()
            StatusWithdraw_format = Withdraw_format.FEFO
            StatusWithdraw_Type = Withdraw_Type.AUTO
            'setSortColumn()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

#Region "RESERV AND PICKING"
    Private Sub ClearReserv()

        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable) ' Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.ReadUncommitted)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0

        objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)

        Try

            If grdWithdrawItemLocation.RowCount = 0 Then
                myTrans.Rollback()
                Exit Sub
            End If


            Dim dtSaveWithDraw As New DataTable
            dtSaveWithDraw = CType(grdWithdrawItemLocation.DataSource, DataTable)
            dtSaveWithDraw.AcceptChanges()


            For Each drSaveWithDraw As DataRow In dtSaveWithDraw.Rows

                Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                Dim LocationBalance_Index As String = drSaveWithDraw("LocationBalance_Index").ToString
                Dim Total_Qty_Reserv As Decimal = CDec(drSaveWithDraw("Total_Qty").ToString)
                Dim Weight_Reserv As Decimal = CDec(drSaveWithDraw("WeightOut").ToString)
                Dim Volume_Reserv As Decimal = CDec(drSaveWithDraw("VolumeOut").ToString)
                Dim ItemQty_Reserv As Decimal = CDec(drSaveWithDraw("QtyItemOut").ToString)
                Dim Price_Reserv As Decimal = CDec(drSaveWithDraw("Price_Out").ToString)
                Dim Qty_Reserv As Decimal = CDec(drSaveWithDraw("Qty").ToString)

                objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "ออกจากหน้าหยิบ โดยไม่บันทึก", LocationBalance_Index, _
                               0, 0, 0, 0, 0, 0, _
                               Total_Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                Select Case _pCaseOperation
                    Case Operation.Withdraw
                        Dim strDocumentPlanItem_Index As String = drSaveWithDraw("DocumentPlanItem_Index").ToString
                        Dim strPlan_Process As String = drSaveWithDraw("Plan_Process").ToString
                        Dim strDocumentPlan_Index As String = drSaveWithDraw("DocumentPlan_Index").ToString

                        Select Case _pPlan_Process
                            Case 10
                                Dim oWithDrawItem As New Cl_WithdrawReserv
                                oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, Qty_Reserv, Total_Qty_Reserv, ItemQty_Reserv, Weight_Reserv, Volume_Reserv, strPlan_Process, objcon.Connection, myTrans)
                                oWithDrawItem.DeleteWithdrawItem(objcon.Connection, myTrans, drSaveWithDraw("WithdrawItem_Index").ToString)
                            Case -9
                                Dim oWithDrawItem As New Cl_WithdrawReserv
                                oWithDrawItem.DeleteWithdrawItem(objcon.Connection, myTrans, drSaveWithDraw("WithdrawItem_Index").ToString)
                            Case Else

                        End Select

                    Case Operation.Transfer
                        Dim oTransfer As New Cl_TransferReserv
                        oTransfer.DeleteTransferItem(objcon.Connection, myTrans, drSaveWithDraw("TransferStatusLocation_Index").ToString)

                    Case Operation.TransferOwner
                        Dim strDocumentPlanItem_Index As String = drSaveWithDraw("DocumentPlanItem_Index").ToString
                        Dim strPlan_Process As String = drSaveWithDraw("Plan_Process").ToString
                        Dim strDocumentPlan_Index As String = drSaveWithDraw("DocumentPlan_Index").ToString
                        Dim oTransfer As New Cl_TransferReserv
                        oTransfer.DeleteTransferItem_Owner(objcon.Connection, myTrans, drSaveWithDraw("TransferOwnerLocation_Index").ToString)

                        Select Case _pPlan_Process
                            Case 10
                                Dim oTransferDocRef As New Cl_WithdrawReserv
                                oTransferDocRef.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, Qty_Reserv, Total_Qty_Reserv, ItemQty_Reserv, Weight_Reserv, Volume_Reserv, strPlan_Process, objcon.Connection, myTrans)
                            Case Else

                        End Select
                End Select

            Next



            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            objcon.disconnectDB()
        End Try
    End Sub

    Private Sub btnDelete_temp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_temp.Click

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable) ' Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.ReadUncommitted)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0

        objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)

        Try
            If grdWithdrawItemLocation.RowCount = 0 Then
                myTrans.Commit()
                Exit Sub
            End If
            Dim i As Integer = 0
            i = grdWithdrawItemLocation.CurrentRow.Index

            Dim pWithdrawItem_Index As String
            If Not grdWithdrawItemLocation.Rows(i).Cells("Col_WithdrawItem_Index").Value Is Nothing Then
                pWithdrawItem_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_WithdrawItem_Index").Value.ToString
            Else
                pWithdrawItem_Index = ""
            End If

            Dim pTransferStatusLocation_Index As String
            If Not grdWithdrawItemLocation.Rows(i).Cells("Col_TransferStatusLocation_Index").Value Is Nothing Then
                pTransferStatusLocation_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_TransferStatusLocation_Index").Value.ToString
            Else
                pTransferStatusLocation_Index = ""
            End If

            Dim pTransferOwnerLocation_Index As String
            If Not grdWithdrawItemLocation.Rows(i).Cells("Col_TransferOwnerLocation_Index").Value Is Nothing Then
                pTransferOwnerLocation_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_TransferOwnerLocation_Index").Value.ToString
            Else
                pTransferOwnerLocation_Index = ""
            End If

            Dim tmpDataSoure As New DataTable
            tmpDataSoure = CType(grdWithdrawItemLocation.DataSource, DataTable)
            tmpDataSoure.AcceptChanges()
            Dim DrSelect() As DataRow
            Select Case _pCaseOperation
                Case Operation.Transfer
                    DrSelect = tmpDataSoure.Select(String.Format("TransferStatusLocation_Index = '{0}'", pTransferStatusLocation_Index))
                Case Operation.TransferOwner
                    DrSelect = tmpDataSoure.Select(String.Format("TransferOwnerLocation_Index = '{0}'", pTransferOwnerLocation_Index))
                Case Operation.Withdraw
                    DrSelect = tmpDataSoure.Select(String.Format("WithdrawItem_Index = '{0}'", pWithdrawItem_Index))
                Case Else
                    DrSelect = tmpDataSoure.Select(String.Format("1=2"))
            End Select


            For Each drSaveWithDraw As DataRow In DrSelect

                Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                Dim LocationBalance_Index As String = drSaveWithDraw("LocationBalance_Index").ToString
                Dim Total_Qty_Reserv As Decimal = CDec(drSaveWithDraw("Total_Qty").ToString)
                Dim Weight_Reserv As Decimal = CDec(drSaveWithDraw("WeightOut").ToString)
                Dim Volume_Reserv As Decimal = CDec(drSaveWithDraw("VolumeOut").ToString)
                Dim ItemQty_Reserv As Decimal = CDec(drSaveWithDraw("QtyItemOut").ToString)
                Dim Price_Reserv As Decimal = CDec(drSaveWithDraw("Price_Out").ToString)
                Dim Qty_Reserv As Decimal = CDec(drSaveWithDraw("Qty").ToString)
                objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "ลบจองจากหย้าหยิบ", LocationBalance_Index, _
                               0, 0, 0, 0, 0, 0, _
                               Total_Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                Select Case _pCaseOperation
                    Case Operation.Withdraw
                        Dim strDocumentPlanItem_Index As String = drSaveWithDraw("DocumentPlanItem_Index").ToString
                        Dim strPlan_Process As String = drSaveWithDraw("Plan_Process").ToString
                        Dim strDocumentPlan_Index As String = drSaveWithDraw("DocumentPlan_Index").ToString

                        Select Case _pPlan_Process
                            Case 10
                                Dim oWithDrawItem As New Cl_WithdrawReserv
                                oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, Qty_Reserv, Total_Qty_Reserv, ItemQty_Reserv, Weight_Reserv, Volume_Reserv, strPlan_Process, objcon.Connection, myTrans)
                                oWithDrawItem.DeleteWithdrawItem(objcon.Connection, myTrans, pWithdrawItem_Index)
                            Case -9
                                Dim oWithDrawItem As New Cl_WithdrawReserv
                                oWithDrawItem.DeleteWithdrawItem(objcon.Connection, myTrans, pWithdrawItem_Index)
                            Case Else

                        End Select
                    Case Operation.Transfer
                        Dim oTransfer As New Cl_TransferReserv
                        oTransfer.DeleteTransferItem(objcon.Connection, myTrans, pTransferStatusLocation_Index)

                    Case Operation.TransferOwner
                        Dim strDocumentPlanItem_Index As String = drSaveWithDraw("DocumentPlanItem_Index").ToString
                        Dim strPlan_Process As String = drSaveWithDraw("Plan_Process").ToString
                        Dim strDocumentPlan_Index As String = drSaveWithDraw("DocumentPlan_Index").ToString
                        Dim oTransfer As New Cl_TransferReserv
                        oTransfer.DeleteTransferItem_Owner(objcon.Connection, myTrans, pTransferOwnerLocation_Index)

                        Select Case _pPlan_Process
                            Case 10
                                Dim oTransferDocRef As New Cl_WithdrawReserv
                                oTransferDocRef.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, Qty_Reserv, Total_Qty_Reserv, ItemQty_Reserv, Weight_Reserv, Volume_Reserv, strPlan_Process, objcon.Connection, myTrans)
                            Case Else

                        End Select
                End Select
            Next

            grdWithdrawItemLocation.Rows.RemoveAt(i)

            myTrans.Commit()

        Catch ex As Exception
            myTrans.Rollback()
            W_MSG_Error(ex.Message)
        Finally
            Me.btnSearch_Click(sender, e)
        End Try

    End Sub

    Function Check_HeadWithdraw() As Boolean
        Try
            Select Case StatusWithdraw_Type
                Case Withdraw_Type.CUSTOM
                    'Notify
                Case Withdraw_Type.AUTO
                    If Me._SKU_index = "" Then
                        W_MSG_Information_ByIndex(30)
                        Return False

                    End If

            End Select
            If Me._Customer_Index = "" Then
                W_MSG_Information_ByIndex(8)
                Return False

            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#End Region



    'Function Show_grdCustom(ByVal dtData As DataTable) As Integer
    '    Dim status As Integer = 0
    '    Try
    '        If dtData Is Nothing Then Exit Function
    '        grdShowCustom.DataSource = Nothing
    '        For Each dr As DataRow In dtData.Rows
    '            dr("Qty_Recieve_Package") = FormatNumber(dr("Qty_Recieve_Package"), 4)
    '        Next
    '        grdShowCustom.DataSource = dtData
    '        Return status
    '    Catch ex As Exception
    '        Throw ex

    '    End Try

    'End Function
    Function Show_grdCustom(ByVal dtData As DataTable) As Integer
        Dim status As Integer = 0
        Try
            If dtData Is Nothing Then Exit Function
            Me.grdShowCustom.DataSource = Nothing
            Me.grdShowCustom.DataSource = dtData

            Dim objClassDB As New PICKING(PICKING.enmPicking_Type.CUSTOM)
            Dim objDT As DataTable = New DataTable
            Dim strSKU_Index As String = ""
            Dim strPackage_Index As String = ""

            For iRow As Integer = 0 To Me.grdShowCustom.Rows.Count - 1
                strSKU_Index = Me.grdShowCustom.Rows(iRow).Cells("col_Sku_Index").Value
                strPackage_Index = Me.grdShowCustom.Rows(iRow).Cells("col_Package_Index").Value

                'เบิกหน่วยใหญ่กว่าได้ ถ้าไม่อยากให้เบิกหน่วยใหญ่กว่าตอนรับเอา comment ใน Function getSKU_PackageReceive ออก
                objClassDB.getSKU_PackageReceive(strSKU_Index, strPackage_Index)
                objDT = objClassDB.DataTable
                Dim objGrdCombox As New DataGridViewComboBoxCell
                With objGrdCombox
                    .DisplayMember = "Package"
                    .ValueMember = "Package_Index"
                    .DataSource = objDT
                End With

                Me.grdShowCustom.Rows(iRow).Cells("cbo_Package_Withdraw") = objGrdCombox
                Me.cbo_Package_Withdraw.ReadOnly = False
            Next

            Return status
        Catch ex As Exception
            Throw ex

        End Try

    End Function



    Private Sub getcboHandlingType()

        Dim objDBType As New tb_HandlingType(tb_HandlingType.enuOperation_Type.SEARCH)
        Dim objDTType As DataTable = New DataTable

        Try

            objDBType.GetAllAsDataTable()
            objDTType = objDBType.DataTable
            With cbo_HandlingType
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTType
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing

        End Try
    End Sub
    Private Sub getcboHandlingType2()

        Dim objDBType As New tb_HandlingType(tb_HandlingType.enuOperation_Type.SEARCH)
        Dim objDTType As DataTable = New DataTable

        Try

            objDBType.GetAllAsDataTable()
            objDTType = objDBType.DataTable
            With cbo_HandlingType2
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTType
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing

        End Try
    End Sub

    Private Sub getgrdHandlingType(ByVal RowIndex As Integer)
        Dim a As Integer = 0
        Dim objDBType As New tb_HandlingType(tb_HandlingType.enuOperation_Type.SEARCH)
        Dim objDTType As DataTable = New DataTable

        Try
            Dim dgvcob As New DataGridViewComboBoxCell
            objDBType.GetAllAsDataTable()
            objDTType = objDBType.DataTable
            If objDTType.Rows.Count > 0 Then
                With dgvcob
                    .DisplayMember = "Description"
                    .ValueMember = "HandlingType_Index"
                    .DataSource = objDTType
                End With
                grdShowCustom.Rows(RowIndex).Cells("cbo_HandlingType") = dgvcob
                grdShowCustom.Rows(RowIndex).Cells("cbo_HandlingType").Value = objDTType.Rows(0).Item("HandlingType_Index").ToString
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing

        End Try
    End Sub

    '************ ไม่ใช้เพราะว่า จองต้องแต่เลือกสินค้า เลยไม่ต้องคำนวณเหมือนตัวก เก่า ซึ่งยากต่อการควบคุม
    'Sub CalculatorQty_grdShowCustom()
    '    Try
    '        If grdWithdrawItemLocation.RowCount <= 0 Then Exit Sub

    '        Dim intCount_Row As Integer = grdShowCustom.Rows.Count
    '        Dim strtmpWithDrawItem_Index As String = ""
    '        Dim intCount As Integer = 0
    '        Dim i As Integer = 0
    '        For i = 0 To intCount_Row - 1
    '            Dim strLocationBal_Index_Custom As String = grdShowCustom.Rows(i).Cells("Col_LocationBal_Index").Value.ToString

    '            'Dim intQty_Custom As Integer = grdShowCustom.Rows(i).Cells("Col_QtyPackage_Balance").Value
    '            'Dim ItemQty_sku As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_QtyItem_Balance").Value.ToString)
    '            'Dim Price_Custom As decimal = CDec(grdShowCustom.Rows(i).Cells("col_Price").Value)
    '            'Dim weight_Custom As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_Weight_Bal").Value)
    '            'Dim volume_Custom As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_Volume_Bal").Value)

    '            Dim intQty_Custom As Integer = grdShowCustom.Rows(i).Cells("Col_Qty_Begin").Value
    '            Dim ItemQty_sku As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_QtyItem_Begin").Value.ToString)
    '            Dim Price_Custom As decimal = CDec(grdShowCustom.Rows(i).Cells("col_Price_Begin").Value)
    '            Dim weight_Custom As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_Weight_Begin").Value)
    '            Dim volume_Custom As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_Volume_Begin").Value)


    '            Dim Reserve_Qty As decimal = CDec(grdShowCustom.Rows(i).Cells("col_Reserve_Qty").Value)

    '            'Dim strSku_index As String = grdShowCustom.Rows(i).Cells("col_Sku_Index").Value.ToString
    '            'Dim strPackage_index As String = grdShowCustom.Rows(i).Cells("col_Package_Index").Value.ToString
    '            '   Get_sku_ratio(strSku_index, strPackage_index) '

    '            Dim intRation As Integer = CDec(grdShowCustom.Rows(i).Cells("Col_Ratio_Receive").Value)


    '            Dim e As Integer = 0
    '            Dim Qty_WithDraw As decimal = CDec(grdShowCustom.Rows(i).Cells("Col_Qty_Withdraw").Value)


    '            Dim tmpintQty As decimal = 0
    '            Dim tmpintItemQty As decimal = 0
    '            Dim tmpPrice As decimal = 0
    '            Dim tmpweight As decimal = 0
    '            Dim tmpvolume As decimal = 0

    '            '------------------For GrdWithdrawItemLocation-----------------------------------
    '            For e = 0 To grdWithdrawItemLocation.Rows.Count - 1
    '                Dim strLocationBal_Index As String = grdWithdrawItemLocation.Rows(e).Cells("Col_WithdrawItemLocation_Index2").Value.ToString
    '                Dim intQty As Integer = grdWithdrawItemLocation.Rows(e).Cells("Col_Qty_sku2").Value
    '                Dim ItemQty As decimal = grdWithdrawItemLocation.Rows(e).Cells("col_ItemQty2").Value

    '                Dim weight As decimal = grdWithdrawItemLocation.Rows(e).Cells("Col_Weight2").Value
    '                Dim volume As decimal = grdWithdrawItemLocation.Rows(e).Cells("Col_Volume2").Value
    '                Dim intItemQty As decimal = grdWithdrawItemLocation.Rows(e).Cells("col_ItemQty2").Value
    '                Dim Price As decimal = grdWithdrawItemLocation.Rows(e).Cells("col_Price2").Value

    '                If grdWithdrawItemLocation.Rows(e).Cells("col_WithDrawItem_Index2").Value IsNot Nothing Then
    '                    strtmpWithDrawItem_Index = grdWithdrawItemLocation.Rows(e).Cells("col_WithDrawItem_Index2").Value.ToString
    '                Else
    '                    strtmpWithDrawItem_Index = ""
    '                End If


    '                Qty_WithDraw += CDec(grdWithdrawItemLocation.Rows(e).Cells("Col_Withdraw_Package_Qty2").Value)
    '                'ใช้ หน่วยย่อย ^^^

    '                '-----------------------------------------------------
    '                If strLocationBal_Index_Custom = strLocationBal_Index Then
    '                    'intCount = intCount + 1
    '                    If Reserve_Qty <> 0 Then
    '                        If strtmpWithDrawItem_Index = "" Then


    '                            intQty_Custom = intQty_Custom - ((tmpintQty + intQty) - (tmpintQty))
    '                            ItemQty_sku = ItemQty_sku - ((tmpintItemQty + ItemQty) - (tmpintItemQty))
    '                            Price_Custom = Price_Custom - ((tmpPrice + Price) - (tmpPrice))
    '                            weight_Custom = weight_Custom - ((tmpweight + weight) - (tmpweight))
    '                            volume_Custom = volume_Custom - ((tmpvolume + volume) - (tmpvolume))

    '                            Continue For
    '                        End If
    '                        tmpintQty += intQty
    '                        tmpintItemQty += ItemQty
    '                        tmpPrice += Price
    '                        tmpweight += weight
    '                        tmpvolume += volume
    '                        Continue For
    '                    Else
    '                        intQty_Custom = intQty_Custom - intQty
    '                        ItemQty_sku = ItemQty_sku - intItemQty
    '                        Price_Custom = Price_Custom - Price
    '                        weight_Custom = weight_Custom - weight
    '                        volume_Custom = volume_Custom - volume
    '                    End If

    '                    'intQtyItem_Custom = intQtyItem_Custom - intItemQty
    '                    'ได้จำนวน  คงเหลือที่เป็นหน่วยย่อย
    '                    'เอาค่าลง  หน่วยย่อย  กับ หน่วยที่รับ (ยังไม่ได้ทำ)
    '                    'สร้าง column ration ใน grdCustom ไว้ใช้ * กับคงเหลือหน่วยย่อย  จาได้ คงเหลือหน่วยรับ
    '                    'ถ้า - แล้วได้ 0 Delete ทิ่ง
    '                End If
    '                '-----------------------------------------------------

    '                'grdShowCustom.Rows(i).Cells("Col_Qty_Withdraw").Value = intQty_Custom

    '            Next
    '            'If intCount = 1 Then
    '            '    intCount = 0
    '            '    Continue For
    '            'End If
    '            '----------------------------------------------------

    '            grdShowCustom.Rows(i).Cells("Col_QtyPackage_Balance").Value = intQty_Custom         'หน่วยย่อย
    '            grdShowCustom.Rows(i).Cells("Col_Qty_Balance").Value = intQty_Custom / intRation   'รับ

    '            grdShowCustom.Rows(i).Cells("Col_QtyItem_Balance").Value = ItemQty_sku
    '            grdShowCustom.Rows(i).Cells("col_Price").Value = Price_Custom
    '            grdShowCustom.Rows(i).Cells("Col_Weight_Bal").Value = weight_Custom
    '            grdShowCustom.Rows(i).Cells("Col_Volume_Bal").Value = volume_Custom


    '        Next

    '        Clear_rows() 'clear rows 0 Ballance

    '    Catch ex As Exception
    '        Throw ex
    '    End Try


    'End Sub
    '********************************************
    Sub Clear_rows() 'ลบ rows ของ grdCustom ที่ qty เป็น 0
        Try
            Dim intCount_Row As Integer = grdShowCustom.Rows.Count
            Dim intCount_RowZero As Integer = 0

            '--------------count rows qty =0----------------
            Dim i As Integer = 0
            For i = 0 To intCount_Row - 1
                If grdShowCustom.Rows(i).Cells("Col_Qty_Balance").Value = 0 Then
                    intCount_RowZero += 1
                End If
            Next
            '------------------------------------------------


            '-----------Clear 0----------------------------
            Dim r As Integer = 0
            For r = 0 To intCount_RowZero - 1
                '######Secound for ######
                Dim t As Integer = 0
                For t = 0 To grdShowCustom.Rows.Count - 1
                    If grdShowCustom.Rows(t).Cells("Col_Qty_Balance").Value = 0 Then
                        grdShowCustom.Rows.RemoveAt(t)
                        Exit For
                    End If
                Next
                '#######################
            Next
            Select Case StatusWithdraw_Type
                Case Withdraw_Type.AUTO
                    If grdShowCustom.RowCount > 0 Then
                        grdShowCustom.Rows(0).Cells("Col_Qty_Withdraw").ReadOnly = False
                        grdShowCustom.Rows(0).Cells("Col_Qty_Withdraw").Style.BackColor = Color.White
                        grdShowCustom.Rows(0).Cells("chk_WithDraw").Value = True
                        grdShowCustom.Rows(0).Cells("chk_WithDraw").ReadOnly = True
                    End If
                Case Withdraw_Type.AUTO
            End Select
        Catch ex As Exception
            Throw ex
        End Try

        '------------------------------------------------




    End Sub

    Private Function Get_PackagewihtdrawColumn(ByVal psku_index As String, ByVal row_index As String) As Integer
        Try
            Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable

            objClassDB.getSKU_Package(psku_index)
            objDT = objClassDB.DataTable

            Dim objGrdCombox As New DataGridViewComboBoxCell
            If objDT.Rows.Count > 0 Then
                With objGrdCombox
                    .DisplayMember = "Package"
                    .ValueMember = "Package_Index"
                    .DataSource = objDT
                End With

                grdShowCustom.Rows(row_index).Cells("cbo_Package_Withdraw") = objGrdCombox
                grdShowCustom.Rows(row_index).Cells("cbo_Package_Withdraw").Value = objDT.Rows(0).Item("Package_Index").ToString()
                Return 1
            Else
                Return -1
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Sub Get_ItemStatus(ByVal row_index As String)
        Try
            Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable

            objClassDB.getItemStatus_WithDraw()
            objDT = objClassDB.DataTable


            If objDT.Rows.Count > 0 Then
                Dim objGrdCombo As New DataGridViewComboBoxCell


                objGrdCombo.DataSource = objDT
                objGrdCombo.DisplayMember = "ItemStatus"
                objGrdCombo.ValueMember = "ItemStatus_Index"

                If StatusWithdraw_format = Withdraw_format.CUSTOM Then

                End If

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSaveIn_temp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveIn_temp.Click
        Try
            If grdShowCustom.RowCount <= 0 Then Exit Sub
            If Check_HeadWithdraw() = False Then Exit Sub

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If



            _BoolPickSome = False
            Select Case StatusWithdraw_Type
                Case Withdraw_Type.CUSTOM
                    Picking_Manaul()

            End Select
            Get_SumData()
            SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub Picking_Manaul()


        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable) ' Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.ReadUncommitted)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0
        objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)

        Try
            Dim strTagError As String = ""
            Dim count_row As Integer = grdShowCustom.Rows.Count
            Dim intRowRemove_count As Integer = 0
            '------------------------------------------------------
            Dim e As Integer = 0
            Dim i As Integer = 0
            '---------------- BAL ---------------------------------
            '   Dim bCheck_Ok As Boolean
            Dim strLocationBalance_Index As String = ""
            Dim strSku_index As String = ""
            Dim intQty_sku As Decimal = 0
            Dim intItemQty_sku As Decimal = 0
            Dim intItem_QtyPerPck As Decimal = 0
            Dim PriceperPck As Decimal = 0
            Dim weightperPck As Decimal = 0
            Dim netweightperPck As Decimal = 0
            Dim volumeperPck As Decimal = 0
            Dim dtShowCustom As New DataTable

            Dim strPackage_Index As String = ""
            Dim strPackage_Desc As String = ""

            ' CType(grdShowCustom.DataSource, DataTable).AcceptChanges()
            dtShowCustom = CType(grdShowCustom.DataSource, DataTable)
            ' dtShowCustom.AcceptChanges()
            Dim drArrayLocationBalance() As DataRow = dtShowCustom.Select("chkSelect=1")


            If _WithdrawType = 1 Then
                If drArrayLocationBalance.Length > 0 Then
                    Dim Qty_With As Decimal = 0
                    Qty_With = dtShowCustom.Compute("Sum(Qty_Withdraw)", "chkSelect=1")
                    If Qty_With = 0 Then
                        Qty_With = dtShowCustom.Compute("Sum(Qty_Recieve_Package)", "chkSelect=1")
                    End If
                    Dim SumDiff As Decimal = CheckDiffQtyWithdraw()

                    If (Qty_With + SumDiff) > CDec(txtQty_Reserv.Text) Then
                        W_MSG_Information("ไม่สามารถเบิกเกินจำนวนใน SO ได้" & vbNewLine & "สามารถหยิบได้อีก " & (FormatNumber(SumDiff - CDec(txtQty_Reserv.Text), 2)).ToString.Replace("-", "") & " " & cboPackage.Text)
                        Exit Sub
                    End If
                End If
            End If





            For Each drLocationBalance As DataRow In drArrayLocationBalance
                strLocationBalance_Index = drLocationBalance("LocationBalance_Index").ToString
                strPackage_Index = drLocationBalance("Package_Index").ToString
                'strPackage_Index = drLocationBalance("Recieve_Package_Index").ToString

                Dim objdelPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                Dim dtrSetBalanceLocationBalance As New DataTable
                dtrSetBalanceLocationBalance = objdelPicking.SEARCHLOCATIONBALANCE_ALLBALANCE(" and  locationbalance_index = '" & strLocationBalance_Index & "'", objcon.Connection, myTrans, objcon.SQLServerCommand)
                If dtrSetBalanceLocationBalance.Rows.Count > 0 Then
                    Dim qty_WD As Decimal = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drLocationBalance("Qty_Withdraw"), GetType(Decimal))
                    Dim Bal As Decimal = dtrSetBalanceLocationBalance.Rows(0).Item("Qty_Bal")

                    Dim drSetBalanceLocationBalance As DataRow = dtrSetBalanceLocationBalance.Rows(0)
                    drLocationBalance("Qty_bal") = CDec(drSetBalanceLocationBalance("Qty_bal"))                        'intQty_sku ' จำนวนคงเหลือหน่วยย่อย
                    drLocationBalance("Qty_Recieve_Package") = CDec(drSetBalanceLocationBalance("Qty_Recieve_Package"))  ' (intQty_sku / CDec(drLocationBalance("Ratio").ToString)) 'คงเหลือ PCK
                    drLocationBalance("OrderItem_Price_Bal") = CDec(drSetBalanceLocationBalance("OrderItem_Price_Bal"))  'PriceperPck * CDec(drLocationBalance("Qty_Recieve_Package")) 'คงเหลือ ราคา
                    drLocationBalance("Qty_Item_Bal") = CDec(drSetBalanceLocationBalance("Qty_Item_Bal"))              'intItem_QtyPerPck * CDec(drLocationBalance("Qty_Recieve_Package")) 'คงเหลือ จำนวนชิ้นรวม
                    drLocationBalance("Weight_Bal") = CDec(drSetBalanceLocationBalance("Weight_Bal"))              ' weightperPck * CDec(drLocationBalance("Qty_Recieve_Package"))
                    drLocationBalance("Volume_Bal") = CDec(drSetBalanceLocationBalance("Volume_Bal"))              'volumeperPck * CDec(drLocationBalance("Qty_Recieve_Package"))
                    If qty_WD > Bal Or Bal = 0 Then
                        drLocationBalance("Qty_Withdraw") = 0 'Reset จำนวนเบิก=0
                        drLocationBalance("chkSelect") = False

                        strTagError &= drSetBalanceLocationBalance("Tag_No").ToString & vbNewLine
                        Continue For
                    End If
                End If

                strSku_index = drLocationBalance("Sku_Index").ToString
                intQty_sku = CDec(drLocationBalance("Qty_bal").ToString)
                intItemQty_sku = CDec(drLocationBalance("Qty_Item_Bal").ToString)

                intItem_QtyPerPck = intItemQty_sku / intQty_sku
                PriceperPck = CDec(drLocationBalance("OrderItem_Price_Bal").ToString) / CDec(drLocationBalance("Qty_bal").ToString)
                weightperPck = CDec(drLocationBalance("Weight_Bal").ToString) / CDec(drLocationBalance("Qty_bal").ToString)
                netweightperPck = CDec(drLocationBalance("Flo1").ToString) / CDec(drLocationBalance("Qty_bal").ToString)
                volumeperPck = CDec(drLocationBalance("Volume_Bal").ToString) / CDec(drLocationBalance("Qty_bal").ToString)

                Dim intQty As Decimal = 0
                Dim dblRatio As Decimal = Me.Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)

                If CDec(drLocationBalance("Qty_Withdraw")) <= 0 Then
                    'หยิบทั้งหมด
                    'intQty = CDec(drLocationBalance("Qty_bal").ToString) / Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                    'intQty = CDec(drLocationBalance("Qty_bal").ToString) / dblRatio
                    intQty = CDec(drLocationBalance("Qty_bal").ToString)
                Else
                    'หยิบตามหน่วยเลือก
                    intQty = CDec(drLocationBalance("Qty_Withdraw").ToString)
                    intQty = intQty * dblRatio
                End If

                'Get Package Index 
                'Dim oPackage As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
                'oPackage.SelectData_ByIndex(strPackage_Index)
                'strPackage_Index = oPackage.GetDataTable.Rows(0).Item("Package_Index").ToString
                'strPackage_Desc = oPackage.GetDataTable.Rows(0).Item("Description").ToString

                strPackage_Index = drLocationBalance("Sku_Package_Index").ToString
                strPackage_Desc = drLocationBalance("Sku_PackageDescription").ToString
                '---------------------------------------
                Dim xMSG As String = String.Format("จำนวนที่เบิกไม่ถูกต้อง {0} TAG No : {1} {2} System_Index : {3}", Chr(13), drLocationBalance("Tag_No").ToString, Chr(13), drLocationBalance("Locationbalance_Index").ToString)
                If (intQty <= 0) Then
                    'W_MSG_Information_ByIndex(300041)
                    W_MSG_Error(xMSG)
                    Exit For
                End If

                'intQty_sku = FormatNumber(intQty_sku, 6)
                'intQty = FormatNumber(intQty, 6)


                Select Case _BoolPickSome
                    Case False
                        intQty_sku = intQty_sku - (intQty * Get_sku_ratio(strSku_index, strPackage_Index))
                    Case True
                        intQty_sku = intQty_sku - intQty
                End Select

                'KSL : Edit 4 Digit
                'intQty_sku = FormatNumber(intQty_sku, 6)
                'intQty = FormatNumber(intQty, 6)

                '---------------------------------------
                If intQty_sku < 0 Or intQty = 0 Then
                    'W_MSG_Information_ByIndex(300041)
                    W_MSG_Error(xMSG)
                    Exit For
                Else
                    '--- STEP : DONG_KK จอง
                    '**************  STEP 1 : Insert WithDraw PICK ************** 
                    Dim iPick As Integer = 0
                    Dim row_num As Integer = 0
                    drLocationBalance("Qty") = intQty
                    'Begin : Calculate Per PCK.
                    drLocationBalance("Invoice_Out") = ""

                    Select Case _BoolPickSome
                        Case False
                            drLocationBalance("Total_Qty") = intQty * Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                            drLocationBalance("VolumeOut") = volumeperPck * intQty * Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                            drLocationBalance("WeightOut") = weightperPck * intQty * Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                            drLocationBalance("Flo1") = netweightperPck * intQty * Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                            drLocationBalance("QtyItemOut") = intItem_QtyPerPck * intQty * Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                            drLocationBalance("Price_Out") = PriceperPck * intQty * Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_Index)
                            'drLocationBalance("Description") = drLocationBalance("Package_Recieve")
                            drLocationBalance("Description") = strPackage_Desc 'drLocationBalance("Sku_PackageDescription")
                            drLocationBalance("Package_Index") = strPackage_Index 'drLocationBalance("Sku_Package_Index")
                        Case True
                            drLocationBalance("Total_Qty") = Qty
                            drLocationBalance("VolumeOut") = VolumeOut
                            drLocationBalance("WeightOut") = WeightOut
                            drLocationBalance("QtyItemOut") = QtyItemOut
                            drLocationBalance("Price_Out") = Price_Out
                            'drLocationBalance("Qty") = intQty / Get_sku_ratio(drLocationBalance("Sku_Index").ToString, strPackage_index)
                            drLocationBalance("Description") = drLocationBalance("Sku_PackageDescription")
                            drLocationBalance("Package_Index") = drLocationBalance("Sku_Package_Index")
                    End Select

                    'End : Calculate Per PCK.
                    drLocationBalance("NewItem") = 1

                    '--- Set DataSource DataGrid Pick
                    Dim dtShowLocationBalance As New DataTable
                    dtShowLocationBalance = dtShowCustom.Clone
                    dtShowLocationBalance.Rows.Add(drLocationBalance.ItemArray)
                    setDataSource_grdWithDrawItemLocation(dtShowLocationBalance, objcon.Connection, myTrans, objcon.SQLServerCommand)


                    Dim LocationBalance_Index As String = drLocationBalance("locationbalance_index")
                    Dim Qty_Reserv As Decimal = CDec(drLocationBalance("Total_Qty"))
                    Dim Weight_Reserv As Decimal = CDec(drLocationBalance("WeightOut"))
                    Dim Volume_Reserv As Decimal = CDec(drLocationBalance("VolumeOut"))
                    Dim ItemQty_Reserv As Decimal = CDec(drLocationBalance("QtyItemOut"))
                    Dim Price_Reserv As Decimal = CDec(drLocationBalance("Price_Out"))


                    Dim Package_Index_AC As String = strPackage_Index
                    Dim Qty_Recieve_Package_AC As Decimal = intQty
                    Dim ReserveQty_AC As Decimal = Qty_Reserv
                    Dim ReserveWeight_AC As Decimal = Weight_Reserv
                    Dim ReserveVolume_AC As Decimal = Volume_Reserv
                    Dim ReserveQty_Item_AC As Decimal = ItemQty_Reserv
                    Dim ReserveOrderItem_Price_AC As Decimal = Price_Reserv


                    objdelPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.ADDRESERVE, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "จองจากหย้าหยิบ", LocationBalance_Index, _
                              0, 0, 0, 0, 0, 0, _
                              Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)


                    '************** STEP 3 : CHECK True คำนวณ Balance Grid grdShowCustom **************
                    'ควรระวัง เพระข้อมูลจะแสดงที่ DataGrid ด้านบน
                    'Dim dtrSetBalanceLocationBalance As New DataTable
                    'dtrSetBalanceLocationBalance = objdelPicking.SEARCHLOCATIONBALANCE_ALLBALANCE(" and  locationbalance_index = '" & strLocationBalance_Index & "'")
                    dtrSetBalanceLocationBalance = objdelPicking.SEARCHLOCATIONBALANCE_ALLBALANCE(" and  locationbalance_index = '" & strLocationBalance_Index & "'", objcon.Connection, myTrans, objcon.SQLServerCommand)

                    If dtrSetBalanceLocationBalance.Rows.Count > 0 Then

                        Dim drSetBalanceLocationBalance As DataRow = dtrSetBalanceLocationBalance.Rows(0)
                        drLocationBalance("Description") = drSetBalanceLocationBalance("Description")
                        drLocationBalance("Package_Index") = drSetBalanceLocationBalance("Package_Index")
                        drLocationBalance("Qty_bal") = CDec(drSetBalanceLocationBalance("Qty_bal"))                        'intQty_sku ' จำนวนคงเหลือหน่วยย่อย
                        drLocationBalance("Qty_Recieve_Package") = CDec(drSetBalanceLocationBalance("Qty_Recieve_Package"))  ' (intQty_sku / CDec(drLocationBalance("Ratio").ToString)) 'คงเหลือ PCK
                        drLocationBalance("OrderItem_Price_Bal") = CDec(drSetBalanceLocationBalance("OrderItem_Price_Bal"))  'PriceperPck * CDec(drLocationBalance("Qty_Recieve_Package")) 'คงเหลือ ราคา
                        drLocationBalance("Qty_Item_Bal") = CDec(drSetBalanceLocationBalance("Qty_Item_Bal"))              'intItem_QtyPerPck * CDec(drLocationBalance("Qty_Recieve_Package")) 'คงเหลือ จำนวนชิ้นรวม
                        drLocationBalance("Weight_Bal") = CDec(drSetBalanceLocationBalance("Weight_Bal"))              ' weightperPck * CDec(drLocationBalance("Qty_Recieve_Package"))
                        drLocationBalance("Flo1") = CDec(drSetBalanceLocationBalance("Flo1"))
                        drLocationBalance("Volume_Bal") = CDec(drSetBalanceLocationBalance("Volume_Bal"))              'volumeperPck * CDec(drLocationBalance("Qty_Recieve_Package"))
                        drLocationBalance("Qty_Withdraw") = 0 'Reset จำนวนเบิก=0
                        drLocationBalance("chkSelect") = False
                    End If
                    intRowRemove_count += 1
                End If
            Next

            Clear_rows()
            If strTagError <> "" Then
                myTrans.Commit()
                Dim altstr As String = ""
                altstr = W_Language.GetMessage_Data(400015) & vbNewLine & "===========" & vbNewLine
                altstr &= strTagError
                altstr &= "===========" & vbNewLine & W_Language.GetMessage_Data(400016)
                W_MSG_Information(altstr)
            Else
                myTrans.Commit()
            End If

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            objcon.disconnectDB()
        End Try

    End Sub
    Private Function CheckDiffQtyWithdraw() As Decimal
        If grdWithdrawItemLocation.RowCount <= 0 Then
            Return 0
        Else
            Dim QtyDiff As Decimal = 0
            Dim dt As DataTable = CType(grdWithdrawItemLocation.DataSource, DataTable)
            QtyDiff = dt.Compute("Sum(Qty)", "")
            Return QtyDiff
        End If
    End Function

    'Function Show_grdWithdrawItemLocation_CusTom(ByVal strLocationBalance_index As String, ByVal intQty As decimal, ByVal strPackage_Index As String, ByVal Weight_Bal As decimal, ByVal Volume_Bal As decimal, ByVal QtyItem_Balance As decimal, ByVal Price_PerPCK As decimal) As Integer
    '    Try
    '        Dim objPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
    '        Dim dtLocationBalance As DataTable = objPick.SEARCHLOCATIONBALANCE_PICKING_CUSTOM(" and  locationbalance_index = '" & strLocationBalance_index & "'")

    '        If dtLocationBalance.Rows.Count > 0 Then
    '            Dim i As Integer = 0
    '            For i = 0 To dtLocationBalance.Rows.Count - 1
    '                Dim row_num As Integer = 0
    '                With grdWithdrawItemLocation
    '                    dtLocationBalance.Rows(i).Item("Qty") = intQty
    '                    Dim objDB_Package As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
    '                    objDB_Package.SelectData_ByIndex(strPackage_Index)

    '                    dtLocationBalance.Rows(i).Item("Description") = objDB_Package.DataTable.Rows(0).Item("Description").ToString()
    '                    dtLocationBalance.Rows(i).Item("Package_Index") = objDB_Package.DataTable.Rows(0).Item("Package_Index").ToString()

    '                    dtLocationBalance.Rows(i).Item("Invoice_Out") = ""
    '                    dtLocationBalance.Rows(i).Item("Total_Qty") = intQty * Get_sku_ratio(dtLocationBalance.Rows(i).Item("Sku_Index").ToString, strPackage_Index)
    '                    dtLocationBalance.Rows(i).Item("VolumeOut") = Volume_Bal * intQty
    '                    dtLocationBalance.Rows(i).Item("WeightOut") = Weight_Bal * intQty
    '                    dtLocationBalance.Rows(i).Item("QtyItemOut") = QtyItem_Balance * intQty
    '                    dtLocationBalance.Rows(i).Item("Price_Out") = Price_PerPCK * intQty

    '                    If WithDrawItem = 1 Then
    '                        dtLocationBalance.Rows(i).Item("VolumeOut") = VolumeOut
    '                        dtLocationBalance.Rows(i).Item("WeightOut") = WeightOut
    '                        dtLocationBalance.Rows(i).Item("QtyItemOut") = QtyItemOut
    '                        dtLocationBalance.Rows(i).Item("Price_Out") = Price_Out
    '                    End If
    '                    dtLocationBalance.Rows(i).Item("NewItem") = 1

    '                    '--- Set grid
    '                    setDataSource_grdWithDrawItemLocation(dtLocationBalance)


    '                End With

    '                '--- New Reserv No Insert tb_WithDrawItemLocation
    '                Dim LocationBalance_Index As String = dtLocationBalance.Rows(i).Item("locationbalance_index")
    '                Dim Qty_Reserv As decimal = CDec(dtLocationBalance.Rows(i).Item("Total_Qty"))
    '                Dim Weight_Reserv As decimal = CDec(dtLocationBalance.Rows(i).Item("WeightOut"))
    '                Dim Volume_Reserv As decimal = CDec(dtLocationBalance.Rows(i).Item("VolumeOut"))
    '                Dim ItemQty_Reserv As decimal = CDec(dtLocationBalance.Rows(i).Item("QtyItemOut"))
    '                Dim Price_Reserv As decimal = CDec(dtLocationBalance.Rows(i).Item("Price_Out"))

    '                Dim objdelPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM)
    '                objdelPicking.UPDATE_RESERVLOCATIONBALANCE(LocationBalance_Index, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

    '                'Insert LocationBalanceTransaction
    '                'Add by : Dong_kk
    '                'Add Date : 2011/06/03

    '                Dim DocumentPlan_Process As String = ""
    '                Dim DocumentPlan_No As String = ""
    '                Dim DocumentPlan_Index As String = ""
    '                Dim DocumentPlanItem_Index As String = ""
    '                Dim Package_Index_AC As String = strPackage_Index
    '                Dim Qty_Recieve_Package_AC As decimal = 0
    '                Dim ReserveQty_AC As decimal = Qty_Reserv
    '                Dim ReserveWeight_AC As decimal = Weight_Reserv
    '                Dim ReserveVolume_AC As decimal = Volume_Reserv
    '                Dim ReserveQty_Item_AC As decimal = ItemQty_Reserv
    '                Dim ReserveOrderItem_Price_AC As decimal = Price_Reserv

    '                objdelPicking.InsertLocationBalanceTransaction(PICKING.enmPicking_Action.ADDRESERVE _
    '                , LocationBalance_Index _
    '                , DocumentPlan_Process _
    '                , DocumentPlan_No _
    '                , DocumentPlan_Index _
    '                , DocumentPlanItem_Index _
    '                , Package_Index_AC _
    '                , Qty_Recieve_Package_AC _
    '                , ReserveQty_AC _
    '                , ReserveWeight_AC _
    '                , ReserveVolume_AC _
    '                , ReserveQty_Item_AC _
    '                , ReserveOrderItem_Price_AC)

    '            Next
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Dim tmpPrice As Decimal = 0
    Dim tmpPerPrice As Decimal = 0

    Private Sub getgrdHandlingType_Down(ByVal RowIndex As Integer)
        Dim a As Integer = 0
        Dim objDBType As New tb_HandlingType(tb_HandlingType.enuOperation_Type.SEARCH)
        Dim objDTType As DataTable = New DataTable

        Try
            Dim dgvcob As New DataGridViewComboBoxCell
            objDBType.GetAllAsDataTable()
            objDTType = objDBType.DataTable

            With dgvcob
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTType
            End With
            grdWithdrawItemLocation.Rows(RowIndex).Cells("cbo_HandlingType2") = dgvcob
            grdWithdrawItemLocation.Rows(RowIndex).Cells("cbo_HandlingType2").Value = objDTType.Rows(0).Item("HandlingType_Index").ToString()

        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing

        End Try
    End Sub

    Function Show_grdWithdrawItemLocation(ByVal strLocationBal_index As String, ByVal intQty As Decimal, ByVal strPackage_Index As String, ByVal WithDrawItem_Index As String) As Integer
        Try

            Dim objPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)

            Dim dtLocationBalance As DataTable = objPick.SEARCHLOCATIONBALANCE_PICKING_CUSTOM(" where locationbalance_index = '" & strLocationBal_index & "'")
            If dtLocationBalance.Rows.Count > 0 Then
                Dim i As Integer = 0
                For i = 0 To dtLocationBalance.Rows.Count - 1
                    Dim row_num As Integer = 0
                    With grdWithdrawItemLocation
                        .Rows.Add()
                        row_num = grdWithdrawItemLocation.Rows.Count - 1
                        '-------SET GRD_COLUMN-----------------------
                        .Rows(row_num).Cells("Col_WithdrawItemLocation_Index2").Value = dtLocationBalance.Rows(i).Item("locationbalance_index").ToString
                        .Rows(row_num).Cells("col_WithDrawItem_Index").Value = WithDrawItem_Index

                        .Rows(row_num).Cells("Col_Sku_Index2").Value = dtLocationBalance.Rows(i).Item("sku_index").ToString
                        .Rows(row_num).Cells("Col_Sku_Id2").Value = dtLocationBalance.Rows(i).Item("sku_id").ToString
                        .Rows(row_num).Cells("Col_SKU_Description2").Value = dtLocationBalance.Rows(i).Item("Sku_Description").ToString
                        .Rows(row_num).Cells("Col_Tag_No2").Value = dtLocationBalance.Rows(i).Item("Tag_No").ToString
                        .Rows(row_num).Cells("col_Location_Alias2").Value = dtLocationBalance.Rows(i).Item("Location_Alias").ToString
                        '.Rows(row_num).Cells("Col_PLot2").Value = dtLocationBalance.Rows(i).Item("PLot").ToString
                        .Rows(row_num).Cells("Col_PLot2").Value = dtLocationBalance.Rows(i).Item("PLot").ToString
                        .Rows(row_num).Cells("Col_Item_Status2").Value = dtLocationBalance.Rows(i).Item("ItemStatus_Description").ToString
                        .Rows(row_num).Cells("Col_ItemStatus_Index2").Value = dtLocationBalance.Rows(i).Item("ItemStatus_Index").ToString
                        .Rows(row_num).Cells("Col_Withdraw_Package_Qty2").Value = intQty

                        .Rows(row_num).Cells("Col_Mfg_Date2").Value = dtLocationBalance.Rows(i).Item("Mfg_Date").ToString
                        .Rows(row_num).Cells("Col_ExpDate2").Value = dtLocationBalance.Rows(i).Item("Exp_Date").ToString

                        Dim objDB_Package As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
                        objDB_Package.SelectData_ByIndex(strPackage_Index)
                        .Rows(row_num).Cells("Col_Withdraw_Package2").Value = objDB_Package.DataTable.Rows(0).Item("Description").ToString()
                        .Rows(row_num).Cells("Col_Package_IndexWithdraw2").Value = objDB_Package.DataTable.Rows(0).Item("Package_Index").ToString()

                        '------------------------------------------------------
                        .Rows(row_num).Cells("Col_Qty_sku2").Value = intQty * Get_sku_ratio(.Rows(row_num).Cells("Col_Sku_Index2").Value, strPackage_Index)
                        .Rows(row_num).Cells("Col_Package_sku2").Value = dtLocationBalance.Rows(i).Item("Sku_PackageDescription").ToString
                        '.Rows(row_num).Cells("Col_Package_Index_sku2").Value = dtLocationBalance.Rows(i).Item("Sku_Package_Index").ToString
                        .Rows(row_num).Cells("Col_Mix_Pallet2").Value = dtLocationBalance.Rows(i).Item("MixPallet").ToString

                        .Rows(row_num).Cells("Col_Pallet_Type2").Value = dtLocationBalance.Rows(i).Item("Pallet_Name").ToString
                        .Rows(row_num).Cells("Col_MaxPallet_Qty2").Value = dtLocationBalance.Rows(i).Item("Pallet_Qty").ToString
                        'If objStatus = enuOperation_Type.UPDATE Then
                        '    .Rows(row_num).Cells("ColPallet_Qty").Value = dtLocationBalance.Rows(i).Item("Pallet_Qty").ToString
                        'End If

                        .Rows(row_num).Cells("Col_OrderItem_WIL2").Value = dtLocationBalance.Rows(i).Item("OrderItem_Index").ToString

                        .Rows(row_num).Cells("Col_Weight2").Value = CDec(dtLocationBalance.Rows(i).Item("Weight_Per_Pck").ToString) * intQty
                        .Rows(row_num).Cells("Col_Volume2").Value = CDec(dtLocationBalance.Rows(i).Item("Volume_Per_Pck").ToString) * intQty
                        .Rows(row_num).Cells("col_ItemQty2").Value = CDec(dtLocationBalance.Rows(i).Item("Qty_Per_Pck").ToString) * intQty
                        .Rows(row_num).Cells("col_Price2").Value = (CDec(dtLocationBalance.Rows(i).Item("OrderItem_Price_Bal").ToString) / CDec(dtLocationBalance.Rows(i).Item("Qty_Bal").ToString)) * intQty
                        .Rows(row_num).Cells("col_Item_Package_Index2").Value = dtLocationBalance.Rows(i).Item("Item_Package_Index").ToString

                        ' --- Document
                        .Rows(row_num).Cells("Col_PLot2").Value = dtLocationBalance.Rows(i).Item("Plot").ToString
                        .Rows(row_num).Cells("Col_Pallet_No2").Value = dtLocationBalance.Rows(i).Item("Str5").ToString
                        .Rows(row_num).Cells("col_Invoice_In2").Value = dtLocationBalance.Rows(i).Item("Invoice_No").ToString
                        .Rows(row_num).Cells("Col_Serial_No2").Value = dtLocationBalance.Rows(i).Item("Serial_No").ToString
                        .Rows(row_num).Cells("col_Declaration_No2").Value = dtLocationBalance.Rows(i).Item("Declaration_No").ToString
                        .Rows(row_num).Cells("cbo_HandlingType2").Value = dtLocationBalance.Rows(i).Item("HandlingType").ToString

                        '12-01-2010 by ja add Consignee_Index
                        .Rows(row_num).Cells("col_Consignee_Index2").Value = dtLocationBalance.Rows(i).Item("ConsigneeItem_Index").ToString
                        .Rows(row_num).Cells("col_Consignee_Index2").Tag = dtLocationBalance.Rows(i).Item("Company_Name").ToString

                    End With
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Get_sku_ratio(ByVal strSku_index As String, ByVal strPackage_index As String) As Decimal
        Try
            Dim intRation As Decimal = 0
            Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.SEARCH)
            Dim objDT As New DataTable
            objDB.SelectData_ByPackage(strSku_index, strPackage_index)
            objDT = objDB.DataTable

            If objDT.Rows.Count > 0 Then
                intRation = objDT.Rows(0).Item("Ratio")
            End If

            Return intRation
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function GetDT_GrdWithdrawItemLocation(ByVal strWhere As String) As DataTable
        Try
            Dim objPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
            Dim dtLocationBalance As DataTable = objPick.SEARCHLOCATIONBALANCE_PICKING_CUSTOM(strWhere)

            Return dtLocationBalance
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Update_DT_WithdrawAuto(ByVal DT As DataTable) As DataTable
        Dim Status As Boolean = True

        If DT.Rows.Count <= 0 Then Status = False
        If grdWithdrawItemLocation.Rows.Count <= 0 Then Status = False

        If Status = True Then
            Dim i As Integer = 0
            For i = 0 To DT.Rows.Count - 1
                Dim LocationBal_IndexDT As String = DT.Rows(i).Item("LocationBalance_Index").ToString

                Dim ii As Integer = 0
                For ii = 0 To grdWithdrawItemLocation.Rows.Count - 1
                    Dim LocationBal_IndexGrd As String = grdWithdrawItemLocation.Rows(ii).Cells("Col_WithdrawItemLocation_Index2").Value.ToString

                    If LocationBal_IndexDT = LocationBal_IndexGrd Then
                        DT.Rows(i).Item("Qty_Bal") = CDec(DT.Rows(i).Item("Qty_Bal")) - CDec(grdWithdrawItemLocation.Rows(ii).Cells("Col_Qty_sku2").Value.ToString)
                    End If
                Next
            Next
        End If

        Return DT
    End Function

    Private Sub grdWithdrawItemLocation_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        Try
            If e.ColumnIndex = grdWithdrawItemLocation.Columns("clPallet_Qty").Index Then
                Dim MaxPallet_Qty As Integer = CDec(grdWithdrawItemLocation.Rows(e.RowIndex).Cells("Col_MaxPallet_Qty2").Value.ToString)
                Dim keyinPallet_Qty As Integer = CDec(grdWithdrawItemLocation.Rows(e.RowIndex).Cells("ColPallet_Qty").Value.ToString)

                If keyinPallet_Qty > MaxPallet_Qty Then
                    'clMsg.SettingMsg(16)
                    'MessageBox.Show(clMsg.Data, clMsg.Header, MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    W_MSG_Information_ByIndex(16)
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Function AutoCheck_Group(ByVal GrdRow_Index As String) As Boolean
        Try
            Dim Order_Index As String = ""
            Dim Group_ID As String = ""

            Dim i As Integer = 0

            'check Group y/n
            If grdShowCustom.Rows(GrdRow_Index).Cells("Col_Group_Id").Value Is Nothing _
            Or grdShowCustom.Rows(GrdRow_Index).Cells("Col_Group_Id").Value = "" Then
                Exit Function
            End If

            Order_Index = grdShowCustom.Rows(GrdRow_Index).Cells("Col_Order_Index").Value
            Group_ID = grdShowCustom.Rows(GrdRow_Index).Cells("Col_Group_Id").Value

            For i = 0 To grdShowCustom.Rows.Count - 1
                If Order_Index = grdShowCustom.Rows(i).Cells("Col_Order_Index").Value _
                And Group_ID = grdShowCustom.Rows(i).Cells("Col_Group_Id").Value Then
                    grdShowCustom.Rows(i).Cells("chk_WithDraw").Value = True
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Check_NumGroup() As Boolean
        Try
            Dim i As Integer = 0
            Dim Order_Index As String = ""
            Dim Group_ID As String = ""

            Dim drArrWithDraw() As DataRow = CType(grdShowCustom.DataSource, DataTable).Select("chkSelect=1")

            For Each drWithDraw As DataRow In drArrWithDraw
                If drWithDraw("str9").ToString.Trim = "" Then
                    Continue For
                End If

                Order_Index = drWithDraw("Order_Index").ToString
                Group_ID = drWithDraw("str9").ToString.Trim
                If CType(drWithDraw("chkSelect").ToString, Boolean) = True Then
                    If Check_Group(Group_ID, Order_Index) = False Then
                        W_MSG_Information_ByIndex(34)
                        Return False
                    End If
                End If
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Function Check_Group(ByVal Group_ID As String, ByVal Order_Index As String) As Boolean
        Try
            Dim i As Integer = 0
            Dim CountGroup As Integer = 0
            Dim CountItem_Group As Integer = 0

            Dim sqlDB As New SQLCommands
            sqlDB.SQLComand("select count(*) as CountGroup from tb_orderitem " & _
            "where Order_index ='" & Order_Index & "' and str9 ='" & Group_ID & "'")
            CountGroup = sqlDB.DataTable.Rows(0).Item("CountGroup").ToString()
            If CountGroup = 0 Then Return True

            'For i = 0 To grdShowCustom.Rows.Count - 1
            '    If grdShowCustom.Rows(i).Cells("Col_Group_Id").Value Is Nothing _
            '            Or grdShowCustom.Rows(i).Cells("Col_Group_Id").Value = "" Then
            '        Continue For
            '    End If

            '    If Order_Index = grdShowCustom.Rows(i).Cells("Col_Order_Index").Value _
            '    And Group_ID = grdShowCustom.Rows(i).Cells("Col_Group_Id").Value Then
            '        If CType(grdShowCustom.Rows(i).Cells("chk_WithDraw").Value, Boolean) = True Then
            '            CountItem_Group += 1
            '        End If
            '    End If
            'Next

            CountItem_Group = CType(grdShowCustom.DataSource, DataTable).Compute("count(str9)", _
            "Order_Index = '" & Order_Index & "' and str9='" & Group_ID & "' and chkSelect=1")

            If CountGroup = CountItem_Group Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Check_GroupAuto() As Boolean
        Try
            Dim i As Integer = 0
            Dim Sku_Index As String = ""

            For i = 0 To grdShowCustom.Rows.Count - 1
                If grdShowCustom.Rows(i).Cells("col_Sku_Index").Value Is Nothing Then Return True
                Sku_Index = grdShowCustom.Rows(i).Cells("col_Sku_Index").Value.ToString

                Dim sqlDB As New SQLCommands
                sqlDB.SQLComand(" select count(*) as CountSku from ms_sku " & _
                    " where str10 ='3' and sku_index ='" & Sku_Index & "'")
                If sqlDB.DataTable.Rows(0).Item("CountSku").ToString() <> "0" Then
                    W_MSG_Information_ByIndex(35)
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Function AddDummy_BTN(ByVal Sku_Index As String, ByVal Row_Index As Integer) As Integer
        Try
            Dim btnItem As New DataGridViewButtonCell
            Dim sqlDB As New SQLCommands

            btnItem.Value = "Item"

            sqlDB.SQLComand(" select count(*) as CountSku from ms_sku " & _
                "where str10 ='2' and sku_index ='" & Sku_Index & "'")
            If sqlDB.DataTable.Rows(0).Item("CountSku").ToString() <> "0" Then
                grdShowCustom.Rows(Row_Index).Cells("col_OrderItem_Sku") = btnItem
            End If
        Catch ex As Exception
            Throw ex
        End Try



    End Function

    'Function Item_Click(ByVal Row_Index As Integer) As Integer
    '    Try
    '        Dim obj_OrderItemSku As New frmOrderItemSku
    '        obj_OrderItemSku.Order_ItemIndex = grdShowCustom.Rows(Row_Index).Cells("ColOrderItem").Value
    '        obj_OrderItemSku.OrderItem_RowIndex = Row_Index
    '        obj_OrderItemSku.Sku_index = grdShowCustom.Rows(Row_Index).Cells("Col_Sku_Index").Value

    '        obj_OrderItemSku.Show()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Sub frmWithdraw_main_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles MyBase.KeyPress
        Try
            If e.KeyChar = ChrW(Keys.Enter) Then
                SendKeys.Send("{tab}")
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


#Region "WITHDRAW ITEM PICKING"
    'Dim WithDrawItem As Integer = 0
    '--- Out
    Dim VolumeOut As Decimal = 0
    Dim WeightOut As Decimal = 0
    Dim QtyItemOut As Decimal = 0
    Dim Price_Out As Decimal = 0
    Dim Qty As Decimal = 0

    '--- Out
    'Dim QtyPackage_Balance As decimal = 0
    'Dim Qty_Balance As decimal = 0
    'Dim Price_Bal As decimal = 0
    'Dim QtyItem_Balance As decimal = 0
    'Dim Weight_Bal As decimal = 0
    'Dim Volume_Bal As decimal = 0

    Private Sub btnWithDrawItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            _BoolPickSome = True
            If grdShowCustom.RowCount <= 0 Then
                Exit Sub
            End If

            Select Case StatusWithdraw_Type
                Case Withdraw_Type.AUTO
                    If grdShowCustom.CurrentRow.Index <> 0 Then
                        Exit Sub
                    End If
            End Select

            Dim bCheck_Ok As Boolean = grdShowCustom.CurrentRow.Cells("chk_WithDraw").Value
            If bCheck_Ok = False Then
                W_MSG_Information_ByIndex(300032)
                Exit Sub
            End If
            'frm.Order_No = Me.grdShowCustom.CurrentRow.Cells("Col_Order_No").Value.ToString
            'frm.Sku_Id = Me.grdShowCustom.CurrentRow.Cells("Col_Sku_Id").Value.ToString
            'frm.Product_Name = Me.grdShowCustom.CurrentRow.Cells("Col_Sku_Description").Value.ToString


            ''-- Bal คงเหลือ    PCK
            'frm.QtyBalance = Me.grdShowCustom.CurrentRow.Cells("Col_Qty_Balance").Value.ToString
            'frm.weightBalance = Me.grdShowCustom.CurrentRow.Cells("Col_Weight_Bal").Value.ToString
            'frm.VolumeBalance = Me.grdShowCustom.CurrentRow.Cells("Col_Volume_Bal").Value.ToString


            ''-- All ออก
            'frm.QtyOut = Me.grdShowCustom.CurrentRow.Cells("Col_Qty_Balance").Value.ToString

            ''frm.txtVolumeOut.Text = Me.grdShowCustom.CurrentRow.Cells("Col_Volume_Bal").Value.ToString
            ''frm.txtWeightOut.Text = Me.grdShowCustom.CurrentRow.Cells("Col_Weight_Bal").Value.ToString
            ''frm.txtQtyAllOut.Text = Me.grdShowCustom.CurrentRow.Cells("Col_QtyItem_Balance").Value.ToString

            ''--- All Bal Col_QtyItem_Balance
            'frm.QtyAllBal = Me.grdShowCustom.CurrentRow.Cells("Col_QtyItem_Balance").Value.ToString
            'frm.WeightAllBal = Me.grdShowCustom.CurrentRow.Cells("Col_Weight_Bal").Value.ToString
            'frm.VolumeAllBal = Me.grdShowCustom.CurrentRow.Cells("Col_Volume_Bal").Value.ToString

            'frm.PriceOut = Me.grdShowCustom.CurrentRow.Cells("col_Price").Value.ToString
            'frm.PriceBal = 0

            ''--- ต่อ PCK
            'frm.Qty_Per_Pck = Me.grdShowCustom.CurrentRow.Cells("Col_Qty_PerPackage").Value.ToString
            'frm.weight_Per_Pck = Me.grdShowCustom.CurrentRow.Cells("col_Weight_PerPackage").Value.ToString
            'frm.Volume_Per_Pck = Me.grdShowCustom.CurrentRow.Cells("Col_Volume_PerPackage").Value.ToString

            'If CDec(Me.grdShowCustom.CurrentRow.Cells("Col_Qty_Withdraw").Value) = 0 Then
            '    'frm.QtyAllBal += frm.QtyOut
            '    frm.QtyOut = 0
            'End If
            '''''''''''''''''''''''''''''''''''''''''''''''''''

            Dim strLoctionBalance_Index As String = Me.grdShowCustom.CurrentRow.Cells("col_LocationBal_Index").Value
            Dim drArrSelect() As DataRow
            drArrSelect = CType(Me.grdShowCustom.DataSource, DataTable).Select("LocationBalance_Index='" & strLoctionBalance_Index & "'")

            Dim frm As New frmPicking_Item
            frm.drArrSelect = drArrSelect

            '''''''''''''''''''''''''''''''''''''''''''''''''''
            Dim ror As Integer = Me.grdShowCustom.CurrentRow.Index
            Dim dblRation As Decimal = CDec(grdShowCustom.CurrentRow.Cells("Col_Ratio_Receive").Value)
            '  tmpPrice = CDec(grdShowCustom.CurrentRow.Cells("col_Price").Value) / CDec(grdShowCustom.CurrentRow.Cells("Col_Qty_Balance").Value)

            frm.ShowDialog()
            '-- update Bal 
            If frm.Edit = 1 Then

                'WithDrawItem = 1

                '--- Out
                If CDec(frm.txtWeightOut.Text) < 0 Then
                    W_MSG_Information_ByIndex("400061")
                    Exit Sub
                End If
                If CDec(frm.txtVolumeOut.Text) < 0 Then
                    W_MSG_Information_ByIndex("400062")
                    Exit Sub
                End If
                VolumeOut = CDec(frm.txtVolumeOut.Text)
                WeightOut = CDec(frm.txtWeightOut.Text)
                QtyItemOut = CDec(frm.txtQtyAllOut.Text)

                '--- Balance
                'Price_Bal = CDec(frm.txtPriceBall.Text)
                Price_Out = CDec(frm.txtPriceOut.Text)

                'QtyPackage_Balance = CDec(frm.txtQtyBalance.Text) * dblRation
                'Qty_Balance = CDec(frm.txtQtyBalance.Text)
                'QtyItem_Balance = CDec(frm.txtQtyAll_Bal.Text)
                'Weight_Bal = CDec(frm.txtWeight_Bal.Text)
                'Volume_Bal = CDec(frm.txtVolume_Bal.Text)

                Qty = CDec(frm.txtQtyOut.Text)

                grdShowCustom.Rows(ror).Cells("Col_Qty_Withdraw").Value = CDec(frm.txtQtyOut.Text)



                Picking_Manaul()
                Get_SumData()

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region


    Private Sub grdWithdrawItemLocation_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithdrawItemLocation.CellClick
        Try
            If e.RowIndex <= -1 Then
                Exit Sub
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdShowCustom_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdShowCustom.CellClick
        Try
            If e.RowIndex <= -1 Then
                Exit Sub
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSeachSku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachSku.Click
        Try

            Dim _Cus_Index As String = Me._Customer_Index
            If _IS_TransferOwner = True Then
                Me._Customer_Index = ""
            End If

            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me._Customer_Index)
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") Or (Not frm.Sku_Index Is Nothing) Then
                Me._SKU_index = frm.Sku_Index
                txtSKU_ID.Text = frm.Sku_ID
                txtSku_Name.Text = frm.Sku_Des_eng

                getPackage_Sku()

            Else
                Me._SKU_index = ""
                txtSKU_ID.Text = ""
                txtSku_Name.Text = ""
            End If
            Me._Customer_Index = _Cus_Index
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub cboWareHouse_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWareHouse.SelectedIndexChanged
        Try
            Me.GetRoom()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0

        Try
            chkSelectAll.Checked = False
            If Check_HeadWithdraw() <> True Then
                myTrans.Commit()
                Exit Sub
            End If


            ''grdShowCustom.Rows.Clear()
            Dim strPicking As String = ""
            Dim objPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
            If rdbCustom.Checked Then
                StatusWithdraw_format = Withdraw_format.CUSTOM
            End If

            grdShowCustom.DataSource = Nothing

            Select Case StatusWithdraw_format
                Case Withdraw_format.CUSTOM
                    strPicking = Setsql_Condition()

                    Dim dtLocationBalance As DataTable
                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.CUSTOM, strPicking)
                    If objPicking.CHEK_QTY_BALANCE() = False Then
                        W_MSG_Information(GetMessage_Data("400065") & objPicking.DBLQTY_BALANCE)
                        myTrans.Commit()
                        Exit Sub
                    End If


                    dtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "")

                    If dtLocationBalance.Rows.Count <= 0 Then
                        W_MSG_Information_ByIndex(300040)
                        myTrans.Commit()
                        Exit Sub
                    End If

                    '--- ShowData From Location By Condition Picking
                    Show_grdCustom(dtLocationBalance)
                    '--- Calculate Balance Beetwenn Grid And LocationBalance
                    '    CalculatorQty_grdShowCustom()

                Case Withdraw_format.FIFO
                    If Qty_Reserv <= 0 Then
                        W_MSG_Information_ByIndex(300011)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    strPicking = Setsql_Condition()
                    If Check_GroupAuto() = False Then
                        myTrans.Commit()
                        Exit Sub
                    End If





                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.FIFO, _SKU_index, cboPackage.SelectedValue.ToString, Qty_Reserv, strPicking, _DocumentType_Index)

                    If objPicking.CHEK_QTY_BALANCE(objcon.Connection, myTrans, objcon.SQLServerCommand) = False Then
                        W_MSG_Information(GetMessage_Data("400065") & objPicking.DBLQTY_BALANCE)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    '--- BEGIN PICKING
                    Dim dtLocationBalance As DataTable
                    dtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "")

                    If dtLocationBalance Is Nothing Then
                        W_MSG_Information_ByIndex(300040)
                        myTrans.Commit()
                        Exit Sub
                    End If

                    setDataSource_grdWithDrawItemLocation(dtLocationBalance, objcon.Connection, myTrans, objcon.SQLServerCommand)




                Case Withdraw_format.LIFO
                    If Qty_Reserv <= 0 Then
                        W_MSG_Information_ByIndex(300011)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    strPicking = Setsql_Condition()
                    If Check_GroupAuto() = False Then
                        myTrans.Commit()
                        Exit Sub
                    End If

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.LIFO, _SKU_index, cboPackage.SelectedValue.ToString, Qty_Reserv, strPicking, _DocumentType_Index)
                    If objPicking.CHEK_QTY_BALANCE() = False Then
                        W_MSG_Information(GetMessage_Data("400065") & objPicking.DBLQTY_BALANCE)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    '--- BEGIN PICKING
                    Dim dtLocationBalance As DataTable
                    dtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "")

                    If dtLocationBalance Is Nothing Then
                        W_MSG_Information_ByIndex(300040)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    setDataSource_grdWithDrawItemLocation(dtLocationBalance, objcon.Connection, myTrans, objcon.SQLServerCommand)

                Case Withdraw_format.FEFO
                    If Qty_Reserv <= 0 Then
                        W_MSG_Information_ByIndex(300011)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    strPicking = Setsql_Condition()
                    If Check_GroupAuto() = False Then
                        myTrans.Commit()
                        Exit Sub
                    End If

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.FEFO, _SKU_index, cboPackage.SelectedValue.ToString, Qty_Reserv, strPicking, _DocumentType_Index)
                    If objPicking.CHEK_QTY_BALANCE() = False Then
                        W_MSG_Information(GetMessage_Data("400065") & objPicking.DBLQTY_BALANCE)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    '--- BEGIN PICKING
                    Dim dtLocationBalance As DataTable
                    dtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "")

                    If dtLocationBalance Is Nothing Then
                        W_MSG_Information_ByIndex(300040)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    setDataSource_grdWithDrawItemLocation(dtLocationBalance, objcon.Connection, myTrans, objcon.SQLServerCommand)

                Case Withdraw_format.PICKFACE
                    If Qty_Reserv <= 0 Then
                        W_MSG_Information_ByIndex(300011)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    Dim objconfigPicking As New config_CustomSetting

                    Dim boolgetConfig_Key As Boolean = False
                    boolgetConfig_Key = objconfigPicking.getConfig_Key_USE("USE_PICKFACE_NO_QTY_PER_PALLET")
                    If Not boolgetConfig_Key Then 'ใช้ Qty_Per_Pallet Pickface
                        '----------- Begin : Chk sku Qty_Per_Pck -----------
                        Dim _dblQty_Per_Pallet As Decimal = 0.0
                        Dim oGetSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                        Dim dtGetSku As New DataTable
                        oGetSku.SearchData_ByIndex(_SKU_index)
                        dtGetSku = oGetSku.GetDataTable
                        If dtGetSku.Rows.Count > 0 Then
                            If dtGetSku.Rows(0)("Qty_Per_Pallet").ToString = "" Then
                                _dblQty_Per_Pallet = 0
                            Else
                                _dblQty_Per_Pallet = CDec(dtGetSku.Rows(0)("Qty_Per_Pallet").ToString)
                            End If

                            If _dblQty_Per_Pallet <= 0 Then
                                W_MSG_Information(lblSku.Text & " : " & txtSKU_ID.Text & GetMessage_Data("400066"))
                                myTrans.Commit()
                                Exit Sub
                            End If
                        End If
                        '----------- End : Chk sku Qty_Per_Pck -----------

                    End If

                    strPicking = Setsql_Condition()
                    If Check_GroupAuto() = False Then
                        myTrans.Commit()
                        Exit Sub
                    End If

                    Dim objPicking As New PICKING(PICKING.enmPicking_Type.PICKFACE, _SKU_index, cboPackage.SelectedValue.ToString, Qty_Reserv, strPicking, _DocumentType_Index)
                    If objPicking.CHEK_QTY_BALANCE() = False Then
                        W_MSG_Information(GetMessage_Data("400065") & objPicking.DBLQTY_BALANCE)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    '--- BEGIN PICKING
                    Dim dtLocationBalance As DataTable
                    dtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, Me._DocumentPlan_Process, Me._DocumentPlan_Index, "")

                    If dtLocationBalance Is Nothing Then
                        W_MSG_Information_ByIndex(300040)
                        myTrans.Commit()
                        Exit Sub
                    End If
                    setDataSource_grdWithDrawItemLocation(dtLocationBalance, objcon.Connection, myTrans, objcon.SQLServerCommand)
            End Select

            Get_SumData()
            SetnumRows()

            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        End Try

    End Sub
    Sub SetnumRows()
        Try
            Dim numRows As Integer = 0

            numRows = grdShowCustom.Rows.Count
            If numRows > 0 Then
                lbCountRows.Text = GetMessage_Data("400067") & numRows & GetMessage_Data("400030")
            Else
                lbCountRows.Text = GetMessage_Data("400031")
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Sub setDataSource_grdWithDrawItemLocation(ByVal dtLocationBalance As DataTable, ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub

            If _pCaseOperation = Operation.Withdraw Then
                With dtLocationBalance.Columns
                    If Not .Contains("Plan_Process") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))
                    End If
                    If Not .Contains("DocumentPlan_No") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
                    End If
                    If Not .Contains("DocumentPlan_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_Index", GetType(String)))
                    End If
                    If Not .Contains("DocumentPlanItem_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
                    End If
                    If Not .Contains("WithdrawItem_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("WithdrawItem_Index", GetType(String)))
                    End If
                    If Not .Contains("TransferStatusLocation_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("TransferStatusLocation_Index", GetType(String)))
                    End If
                End With

                Dim objInsertItem As New Cl_WithdrawReserv
                For Each Dr As DataRow In dtLocationBalance.Rows
                    Select Case _pPlan_Process
                        Case 10, 7, 17, 25
                            Dr("Plan_Process") = _pPlan_Process
                            Dr("DocumentPlan_Index") = _pDocumentPlan_Index
                            Dr("DocumentPlanItem_Index") = _pDocumentPlanItem_Index
                            Dr("DocumentPlan_No") = _pDocumentPlan_No
                            Dr("WithdrawItem_Index") = objInsertItem.SaveWithdrawItem_V4(Dr, _withdraw_index, Connection, myTrans, SQLServerCommand)
                        Case -9
                            Dr("Plan_Process") = _pPlan_Process
                            Dr("WithdrawItem_Index") = objInsertItem.SaveWithdrawItem_V4(Dr, _withdraw_index, Connection, myTrans, SQLServerCommand)
                    End Select
                Next

            ElseIf _pCaseOperation = Operation.Transfer Then

                With dtLocationBalance.Columns
                    If Not .Contains("Plan_Process") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))
                    End If
                    If Not .Contains("DocumentPlan_No") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
                    End If
                    If Not .Contains("DocumentPlan_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_Index", GetType(String)))
                    End If
                    If Not .Contains("DocumentPlanItem_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
                    End If
                    If Not .Contains("TransferStatusLocation_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("TransferStatusLocation_Index", GetType(String)))
                    End If
                    If Not .Contains("Old_ItemStatus_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Index", GetType(String)))
                    End If
                    If Not .Contains("Old_ItemStatus_Description") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Description", GetType(String)))
                    End If
                    If Not .Contains("New_ItemStatus_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("New_ItemStatus_Index", GetType(String)))
                    End If
                    If Not .Contains("From_Location_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("From_Location_Index", GetType(String)))
                    End If
                    If Not .Contains("From_Location") Then
                        dtLocationBalance.Columns.Add(New DataColumn("From_Location", GetType(String)))
                    End If
                    If Not .Contains("To_Location") Then
                        dtLocationBalance.Columns.Add(New DataColumn("To_Location", GetType(String)))
                    End If
                    If Not .Contains("To_Location_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("To_Location_Index", GetType(String)))
                    End If
                End With

                Dim objInsertItem As New Cl_TransferReserv
                For Each odr As DataRow In dtLocationBalance.Rows
                    odr("Old_ItemStatus_Index") = odr("ItemStatus_Index").ToString
                    odr("Old_ItemStatus_Description") = odr("ItemStatus_Description").ToString
                    odr("New_ItemStatus_Index") = odr("ItemStatus_Index").ToString
                    odr("From_Location_Index") = odr("Location_Index").ToString
                    odr("From_Location") = odr("Location_Alias").ToString
                    odr("To_Location_Index") = odr("Location_Index").ToString
                    odr("To_Location") = odr("Location_Alias").ToString
                    odr("str1") = ""
                    odr("str2") = ""
                    odr("TransferStatusLocation_Index") = objInsertItem.SaveTransferItem(odr, _Transferstatus_Index, Connection, myTrans, SQLServerCommand)
                Next

            ElseIf _pCaseOperation = Operation.TransferOwner Then

                With dtLocationBalance.Columns
                    If Not .Contains("TransferOwnerLocation_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("TransferOwnerLocation_Index", GetType(String)))
                    End If
                    If Not .Contains("Old_ItemStatus_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Index", GetType(String)))
                    End If
                    If Not .Contains("Old_ItemStatus_Description") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Description", GetType(String)))
                    End If
                    If Not .Contains("New_ItemStatus_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("New_ItemStatus_Index", GetType(String)))
                    End If
                    If Not .Contains("From_Location_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("From_Location_Index", GetType(String)))
                    End If
                    If Not .Contains("From_Location") Then
                        dtLocationBalance.Columns.Add(New DataColumn("From_Location", GetType(String)))
                    End If
                    If Not .Contains("To_Location") Then
                        dtLocationBalance.Columns.Add(New DataColumn("To_Location", GetType(String)))
                    End If
                    If Not .Contains("To_Location_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("To_Location_Index", GetType(String)))
                    End If
                    If Not .Contains("Plan_Process") Then
                        dtLocationBalance.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))
                    End If
                    If Not .Contains("DocumentPlan_No") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
                    End If
                    If Not .Contains("DocumentPlan_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_Index", GetType(String)))
                    End If
                    If Not .Contains("DocumentPlanItem_Index") Then
                        dtLocationBalance.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
                    End If
                    If Not .Contains("new_Plot") Then
                        dtLocationBalance.Columns.Add(New DataColumn("new_Plot", GetType(String)))
                    End If
                End With

                Dim objInsertItem As New Cl_TransferReserv
                For Each odrTemp As DataRow In dtLocationBalance.Rows
                    Select Case _pPlan_Process
                        Case 10
                            odrTemp("Plan_Process") = _pPlan_Process
                            odrTemp("DocumentPlan_No") = _pDocumentPlan_No
                            odrTemp("DocumentPlan_Index") = _pDocumentPlan_Index
                            odrTemp("DocumentPlanItem_Index") = _pDocumentPlanItem_Index
                        Case Else
                            odrTemp("Plan_Process") = _pPlan_Process
                    End Select
                    odrTemp("new_Plot") = odrTemp("Plot")
                    odrTemp("Old_ItemStatus_Index") = odrTemp("ItemStatus_Index").ToString
                    odrTemp("New_ItemStatus_Index") = odrTemp("ItemStatus_Index").ToString
                    odrTemp("From_Location_Index") = odrTemp("Location_Index").ToString
                    odrTemp("From_Location") = odrTemp("Location_Alias").ToString
                    odrTemp("To_Location_Index") = odrTemp("Location_Index").ToString
                    odrTemp("To_Location") = odrTemp("Location_Alias").ToString
                    odrTemp("TransferOwnerLocation_Index") = objInsertItem.SaveTransferItem_Owner(odrTemp, _TransferOwner_Index, Connection, myTrans, SQLServerCommand)
                Next
            End If


            If Me.grdWithdrawItemLocation.DataSource Is Nothing Then
                Me.grdWithdrawItemLocation.DataSource = dtLocationBalance
            Else
                Dim i As Integer
                For i = 0 To dtLocationBalance.Rows.Count - 1
                    Dim odtTempItemLocation As New DataTable
                    Dim odtTempLocationBalance As New DataTable
                    odtTempItemLocation = DirectCast(Me.grdWithdrawItemLocation.DataSource, DataTable)
                    odtTempLocationBalance = dtLocationBalance
                    odtTempItemLocation.Merge(dtLocationBalance.Clone())
                    odtTempLocationBalance.Merge(DirectCast(Me.grdWithdrawItemLocation.DataSource, DataTable).Clone())
                    Dim odrTemp As DataRow
                    odrTemp = odtTempItemLocation.NewRow()
                    odrTemp.ItemArray = odtTempLocationBalance.Rows(i).ItemArray.Clone
                    odtTempItemLocation.Rows.Add(odrTemp)
                    'Dim odtTempItemLocation As New DataTable
                    'Dim odrTemp As DataRow
                    'odtTempItemLocation = Me.grdWithdrawItemLocation.DataSource
                    'odrTemp = odtTempItemLocation.NewRow
                    'odrTemp.ItemArray = dtLocationBalance.Rows(i).ItemArray.Clone
                    'odtTempItemLocation.Rows.Add(odrTemp)
                Next
            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub setDataSource_grdWithDrawItemLocation(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal dtLocationBalance As DataTable)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub


            With dtLocationBalance.Columns

                If Not .Contains("Plan_Process") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))
                End If

                If Not .Contains("DocumentPlan_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
                End If

                If Not .Contains("DocumentPlan_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_Index", GetType(String)))

                End If

                If Not .Contains("DocumentPlanItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
                End If

                If Not .Contains("WithdrawItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("WithdrawItem_Index", GetType(String)))
                End If

            End With




            If Me.grdWithdrawItemLocation.DataSource Is Nothing Then
                Me.grdWithdrawItemLocation.DataSource = dtLocationBalance
            Else
                Dim i As Integer
                For i = 0 To dtLocationBalance.Rows.Count - 1
                    Dim odtTempItemLocation As New DataTable
                    Dim odrTemp As DataRow
                    odtTempItemLocation = Me.grdWithdrawItemLocation.DataSource
                    odrTemp = odtTempItemLocation.NewRow
                    odrTemp.ItemArray = dtLocationBalance.Rows(i).ItemArray.Clone
                    odtTempItemLocation.Rows.Add(odrTemp)
                Next
            End If


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Date : 12/01/2010
    ''' Update By : ja Update 
    ''' search Consignee_Index
    ''' </remarks>

    Function Setsql_Condition() As String
        Try
            Dim strWhere As String = ""
            Dim StartDate As String = ""
            Dim EndtDate As String = ""

            '*************    MAIN   ******************
            '--------- DocumentType ------------
            strWhere &= " and ItemStatus_Index in ( SELECT ItemStatus_Index FROM ms_DocumentType_ItemStatus WHERE     DocumentType_Index = '" & Me._DocumentType_Index & "' AND status_id <> -1) "

            '--------- Customer ------------
            strWhere &= " and Customer_Index = '" & Me._Customer_Index & "'"

            '--------- WithDarw_DATE ------------
            strWhere &= " and Order_Date <= '" & Me._WithDraw_Date.ToString("yyyy/MM/dd HH:mm:ss") & "'"

            '--------- ประเภทสินค้า ------------
            If cboProductType.SelectedValue <> "-11" Then
                strWhere &= " and  ProductType_Index = '" & cboProductType.SelectedValue.ToString & "'"
            End If

            '--------- สถานะสินค้า ------------
            If cboItemStatus.SelectedValue <> "-11" Then
                strWhere &= " and  ItemStatus_Index = '" & cboItemStatus.SelectedValue.ToString & "'"
            End If


            '   --------- รหัสสินค้า ------------
            If Me._SKU_index <> "" Then
                strWhere &= " and  SKU_Index = '" & Me._SKU_index & "'"
                'strWhere &= " and  Package_Index = '" & Me.cboPackage.SelectedValue & "'"
            End If

            '*************    DATETIME   ******************
            '--------- MFG_DATE ------------
            If chkMdate.Checked = True Then
                StartDate = Format(dtpFromDate_M.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                EndtDate = Format(dtpToDate_M.Value, "yyyy/MM/dd").ToString() + "  23:59:00"
                strWhere &= " And Mfg_Date between '" & StartDate & "' and '" & EndtDate & "'"
            End If

            '--------- EXP_DATE ------------
            If chkExDate.Checked = True Then
                StartDate = Format(dtpFromDate_Ex.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                EndtDate = Format(dtpToDate_Ex.Value, "yyyy/MM/dd").ToString() + "  23:59:00"
                strWhere &= " And Exp_Date between '" & StartDate & "' and '" & EndtDate & "'"
            End If

            '--------- Order_DATE ------------
            If chkExOrder.Checked = True Then
                StartDate = Format(dtpFromDate_Or.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                EndtDate = Format(dtpToDate_Or.Value, "yyyy/MM/dd").ToString() + "  23:59:00"
                strWhere &= " And Order_Date between '" & StartDate & "' and '" & EndtDate & "'"
            End If

            '--------- TAG NO ------------
            If ChkTagNo.Checked = True Then
                strWhere &= " And Tag_No = '" & Me.txtTagNo.Text.Trim & "'"
            End If


            '--------- ExLife_DATE ------------
            If chkExLife.Checked = True Then
                Select Case cboDateExlife.SelectedIndex
                    Case 0
                        Select Case cboConditionExlife.SelectedIndex
                            Case 0
                                strWhere &= " AND ( AgeRemain_Month > " & Me.txtConditionKeyExlife.Text.Trim & ")"
                            Case 1
                                strWhere &= " AND ( AgeRemain_Month < " & Me.txtConditionKeyExlife.Text.Trim & ")"
                        End Select
                    Case 1
                        Select Case cboConditionExlife.SelectedIndex
                            Case 0
                                strWhere &= " AND ( AgeRemain > " & Me.txtConditionKeyExlife.Text.Trim & ")"
                            Case 1
                                strWhere &= " AND ( AgeRemain < " & Me.txtConditionKeyExlife.Text.Trim & ")"
                        End Select
                End Select
            End If

            '--------- ItemLife_DATE ------------
            If chkItemLife.Checked = True Then
                Select Case cboDatelife.SelectedIndex
                    Case 0
                        Select Case cboConditionlife.SelectedIndex
                            Case 0
                                strWhere &= " AND ( AgeCountFromMfg_Month > " & Me.txtLife.Text.Trim & ")"
                            Case 1
                                strWhere &= " AND ( AgeCountFromMfg_Month < " & Me.txtLife.Text.Trim & ")"
                        End Select
                    Case 1
                        Select Case cboConditionlife.SelectedIndex
                            Case 0
                                strWhere &= " AND ( AgeCountFromMfg > " & Me.txtLife.Text.Trim & ")"
                            Case 1
                                strWhere &= " AND ( AgeCountFromMfg < " & Me.txtLife.Text.Trim & ")"
                        End Select
                End Select
            End If

            '--------- ItemWeight ------------
            If chkWeight.Checked = True Then
                Select Case cboConditionWeight.SelectedIndex
                    Case 0
                        strWhere &= " AND ( Weight_Bal > " & Me.txtWeight.Text.Trim & ")"
                    Case 1
                        strWhere &= " AND ( Weight_Bal < " & Me.txtWeight.Text.Trim & ")"
                End Select
            End If

            '*************    DOCUMENT   ******************
            '--------- Order_NO ------------
            If chkOrder_No.Checked = True Then
                strWhere &= " and Order_No = '" & txtOrder_No.Text.Trim & "'"
            End If

            '--------- Lot No ------------
            If chkLot.Checked = True Then
                strWhere &= " and PLot = '" & txtLot.Text.Trim & "'"
            End If

            '--------- Pallet_No ------------
            If ChkPallet.Checked = True Then
                strWhere &= " And Str5 like '" & Me.txtPallet.Text.Trim.Replace("'", " ") & "%' "
            End If

            '--------- PO_No ------------
            If chkPo_No.Checked = True Then
                strWhere &= " And Po_No like '" & Me.txtPo_No.Text.Trim.Replace("'", " ") & "%' "
            End If

            '--------- Declaration_No ------------
            If chkDeclaration_No.Checked = True Then
                strWhere &= " And Declaration_No like '" & Me.txtDeclaration_No.Text.Trim.Replace("'", " ") & "%' "
            End If

            '--------- Invoice_No ------------
            If ChkInvoice_In.Checked = True Then
                strWhere &= " And Invoice_No like '" & Me.txtInvoice_In.Text.Trim.Replace("'", " ") & "%' "
            End If

            '--------- Certificate_No ------------
            If chkCertificate.Checked = True Then
                strWhere &= " And Serial_No like '" & Me.txtCertificate.Text.Trim.Replace("'", " ") & "%' "
            End If

            '--------- ASN_No ------------
            If chkASN_No.Checked = True Then
                strWhere &= " And ASN_No like '" & Me.txtASN_No.Text.Trim.Replace("'", " ") & "%' "
            End If

            '--------- Reference ------------
            If chkReference.Checked = True Then
                If cboReference.SelectedValue <> "-11" Then

                    strWhere &= " And " & cboReference.SelectedValue.ToString & " like '" & Me.txtReference.Text.Trim.Replace("'", " ") & "%' "

                End If
            End If

            '12-01-2010 by ja search Consignee_Index ส่วนของ ระดับ Item
            If chkConsigneeIndex.Checked = True Then
                If txtConsignee_Name.Text <> "" Then
                    strWhere &= " and  ConsigneeItem_Index = '" & txtConsignee_Name.Tag & "'"
                End If
            End If

            '*************    LOCATION   ******************
            '--------- WAREHOUSE ------------
            If Me.chkWareHouse.Checked AndAlso cboWareHouse.SelectedValue <> "-11" Then
                strWhere &= "  and Warehouse_Index = '" & cboWareHouse.SelectedValue.ToString & "'"
            End If

            If Me.chkRoom.Checked AndAlso Me.cboRoom.SelectedValue <> "-11" Then
                strWhere &= "  and Room = '" & cboRoom.SelectedValue.ToString & "'"
            End If

            '--------- ZONE ------------
            If cboZone.SelectedValue <> "-11" Then
                strWhere &= " and Zone_Index = '" & cboZone.SelectedValue.ToString & "'"
            End If

            '--------- LOCATIONTYPE ------------
            If cboLocationType.SelectedValue <> "-11" Then
                strWhere &= " and LocationType_Index = '" & cboLocationType.SelectedValue.ToString & "'"
            End If

            '--------- ALIAS ------------
            If chkAlias.Checked = True Then
                strWhere &= " and Location_Alias = '" & txtAlias.Text.Trim & "'"
            End If

            If USE_BLOCK_LOT() = True Then
                strWhere &= " and Consignee_Index = '" & Me.Consignee_Index & "'"
            End If

            If chk_ERP_location.Checked = True Then
                strWhere &= " AND ERP_Location like '%" & txt_ERP_location.Text & "%'  "
            End If

            strWhere &= " AND Qty_Bal > 0  "

            '--------- Details ------------
            Dim IsCheckDetails As Boolean = False

            If (chkEye.Checked And txtEye.Text.ToString <> "") Or (chkAdd.Checked And txtAdd.Text.ToString <> "") _
            Or (chkTilted.Checked And txtTilted.Text.ToString <> "") Or (chkColor.Checked And txtColor.Text.ToString <> "") _
            Or (chkDegree.Checked And txtDegree.Text.ToString <> "") Or (chkBC.Checked And txtBC.Text.ToString <> "") _
            Or (chkVMI.Checked And txtVMI.Text.ToString <> "") Or (chkGeneration.Checked And txtGeneration.Text.ToString <> "") _
            Or (chkBrand.Checked And txtBrand.Text.ToString <> "") Then
                IsCheckDetails = True
            End If

            If IsCheckDetails Then
                strWhere &= " AND Sku_Index IN ( "
                strWhere &= " SELECT Sku_Index FROM ms_SKU_Detail WHERE 1 = 1 "
                If chkEye.Checked And txtEye.Text.ToString <> "" Then strWhere &= " And [Eye] = N'" & txtEye.Text.ToString & "'"
                If chkAdd.Checked And txtAdd.Text.ToString <> "" Then strWhere &= " And [Add] = N'" & txtAdd.Text.ToString & "'"
                If chkTilted.Checked And txtTilted.Text.ToString <> "" Then strWhere &= " And [Tilted] = N'" & txtTilted.Text.ToString & "'"
                If chkColor.Checked And txtColor.Text.ToString <> "" Then strWhere &= " And [Color] = N'" & txtColor.Text.ToString & "'"
                If chkDegree.Checked And txtDegree.Text.ToString <> "" Then strWhere &= " And [Degree] = N'" & txtDegree.Text.ToString & "'"
                If chkBC.Checked And txtBC.Text.ToString <> "" Then strWhere &= " And [BC] = N'" & txtBC.Text.ToString & "'"
                If chkVMI.Checked And txtVMI.Text.ToString <> "" Then strWhere &= " And [VMI] = N'" & txtVMI.Text.ToString & "'"
                If chkGeneration.Checked And txtGeneration.Text.ToString <> "" Then strWhere &= " And [Generation] = N'" & txtGeneration.Text.ToString & "'"
                If chkBrand.Checked And txtBrand.Text.ToString <> "" Then strWhere &= " And [Brand] = N'" & txtBrand.Text.ToString & "'"
                strWhere &= " ) "
            End If

            strWhere &= New clsUserByDC().GetWarehouseByUser()

            Return strWhere

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            _boolReserv = False
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If _Qty_Reserv > 0 Then
                If CDec(txtSumQty.Text) > _Qty_Reserv Then
                    W_MSG_Information_ByIndex(300042)
                    Exit Sub
                    'Else
                    '_Qty_Reserv = CDec(txtSumQty.Text)
                End If
            End If
            _boolReserv = True
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmWithdraw_Reserv_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If _boolReserv = False Then
                ClearReserv()
                _Qty_Reserv = 0
            Else
                _objTmpWithDrawItem = grdWithdrawItemLocation.DataSource

                _objTmpWithDrawItem.AcceptChanges()

                If _objTmpWithDrawItem.Columns.Contains("NewItemFlag") = False Then
                    _objTmpWithDrawItem.Columns.Add("NewItemFlag", GetType(Integer))
                    For Each Dr As DataRow In _objTmpWithDrawItem.Rows
                        Dr("NewItemFlag") = 1
                    Next
                End If

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub Get_SumData()
        Try
            Dim SumData As Decimal = 0
            Dim SumItem As Decimal = 0
            Dim SumNet As Decimal = 0
            Dim SumGrs As Decimal = 0
            Dim SumVolume As Decimal = 0
            Dim SumRate As Decimal = 0

            Dim i As Integer = 0

            For i = 0 To grdWithdrawItemLocation.Rows.Count - 1
                With Me.grdWithdrawItemLocation.Rows(i)
                    SumData += .Cells("Col_Qty_sku2").Value
                    SumItem += .Cells("col_ItemQty2").Value
                    SumNet += .Cells("col_2Flo1").Value
                    SumGrs += .Cells("col_Weight2").Value
                    SumVolume += .Cells("col_Volume2").Value
                    SumRate += .Cells("col_Price2").Value
                End With
            Next


            txtSumQty.Text = Format(SumData, "0")
            txtPackage.Text = Format(SumItem, "#,##0.00")
            txtSumNet_Wt.Text = Format(SumNet, "#,##0.0000")
            txtSumGrs_Wt.Text = Format(SumGrs, "#,##0.0000")
            txtVolume.Text = Format(SumVolume, "#,##0.0000")
            txtRate.Text = Format(SumRate, "#,##0.00")

            'txtSumQty.Text = Format(CDec(Cal_Sum("Col_Qty_sku2").ToString), "0")
            'txtSumNet_Wt.Text = Format(CDec(Cal_Sum("col_Weight2").ToString), "#,##0.0000")
            'txtSumGrs_Wt.Text = Format(CDec(Cal_Sum("col_2Flo1").ToString), "#,##0.0000")
            'txtVolume.Text = Format(CDec(Cal_Sum("col_Volume2").ToString), "#,##0.0000")
            'txtRate.Text = Format(CDec(Cal_Sum("col_Price2").ToString), "#,##0.00")
            'txtPackage.Text = Format(CDec(Cal_Sum("col_ItemQty2").ToString), "#,##0.00")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function Cal_Sum(ByVal grdColumn As String) As Decimal
        Try
            Dim SumData As Decimal = 0
            Dim i As Integer = 0
            For i = 0 To grdWithdrawItemLocation.Rows.Count - 1
                SumData += CDec(grdWithdrawItemLocation.Rows(i).Cells(grdColumn).Value)
            Next
            Return SumData
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub grdShowCustom_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdShowCustom.EditingControlShowing
        Try
            Dim strName As String = grdShowCustom.Columns(grdShowCustom.CurrentCell.ColumnIndex).Name
            If (strName <> "cbo_Package_Withdraw") And (strName <> "cbo_HandlingType") Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 decimal
            Dim Column_Index As String = grdShowCustom.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdShowCustom.Columns("Col_Qty_Withdraw").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Function Check_GrdKeyPress(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdShowCustom.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdShowCustom.CurrentRow.Cells(Column_Index).EditedFormattedValue
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
            Throw ex
        End Try
    End Function

    Private Sub txtQty_Reserv_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty_Reserv.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtQty_Reserv, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_Reserv_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQty_Reserv.TextChanged
        Try
            If txtQty_Reserv.Text <> "" Then
                If _Max_Plan_Reserv < CDec(txtQty_Reserv.Text) And _Max_Plan_Reserv <> 0 Then
                    W_MSG_Information_ByIndex(300060)
                    txtQty_Reserv.Text = _Max_Plan_Reserv
                End If
                _Qty_Reserv = CDec(txtQty_Reserv.Text)
                'Dim dblRatio As decimal = 1
                'dblRatio = Get_sku_ratio(_SKU_index, cboPackage.SelectedValue.ToString)
                'Qty_Auto = CDec(txtQty_Reserv.Text) * dblRatio
                Qty_Auto = CDec(txtQty_Reserv.Text)

            Else
                _Qty_Reserv = 0
                Qty_Auto = 0
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub cboAutoPicking_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboAutoPicking.SelectedIndexChanged
        Try
            Select Case cboAutoPicking.SelectedIndex
                Case 0
                    StatusWithdraw_format = Withdraw_format.FIFO
                Case 1
                    StatusWithdraw_format = Withdraw_format.LIFO
                Case 2
                    StatusWithdraw_format = Withdraw_format.FEFO
                Case 3
                    StatusWithdraw_format = Withdraw_format.PICKFACE

            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub txtConditionKeyExlife_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtConditionKeyExlife.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtConditionKeyExlife, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtLife_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLife.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtLife, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtSKU_ID_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSKU_ID.Leave
        Try
            getPackage_Sku()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub getPackage_Sku()
        Try
            Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable
            Dim strSku_Index As String = ""

            Sku_Id = Me.txtSKU_ID.Text
            objClassDB.getSKU_Detail(Sku_Id)
            objDT = objClassDB.DataTable



            If objDT.Rows.Count > 0 Then
                Dim objDTSku As DataTable = New DataTable

                Sku_Index = objDT.Rows(0).Item("Sku_index").ToString
                objClassDB.getSKU_Package(Sku_Index)
                objDTSku = objClassDB.DataTable

                cboPackage.DisplayMember = "Package"
                cboPackage.ValueMember = "Package_Index"
                cboPackage.DataSource = objDTSku


                txtSKU_ID.Text = objDT.Rows(0)("Sku_Id").ToString

                Select Case W_Module.WV_Language
                    Case enmLanguage.Thai
                        txtSku_Name.Text = objDT.Rows(0)("Product_Name_th").ToString
                    Case enmLanguage.English
                        txtSku_Name.Text = objDT.Rows(0)("Product_Name_Eng").ToString
                End Select


            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub grdShowCustom_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdShowCustom.RowsAdded
        'Try
        '    'If e.RowIndex <= -1 Then Exit Sub

        '    'Dim strSku_Index As String = grdShowCustom.Rows(e.RowIndex).Cells("col_sku_index").Value.ToString

        '    '' Search SKU Package 
        '    'If Get_PackagewihtdrawColumn(strSku_Index, e.RowIndex) < 0 Then
        '    '    W_MSG_Information_ByIndex(500004)
        '    '    Exit Sub
        '    'End If


        '    'AddDummy_BTN(strSku_Index, e.RowIndex)

        '    'Me.getgrdHandlingType(e.RowIndex)

        '    'With grdShowCustom
        '    '    Select Case StatusWithdraw_Type
        '    '        Case Withdraw_Type.CUSTOM
        '    '            .Rows(e.RowIndex).Cells("Col_Qty_Withdraw").ReadOnly = False
        '    '            .Rows(e.RowIndex).Cells("Col_Qty_Withdraw").Style.BackColor = Color.White

        '    '            .Columns("chk_WithDraw").Visible = True
        '    '            .Rows(e.RowIndex).Cells("chk_WithDraw").Value = False
        '    '            .Rows(e.RowIndex).Cells("chk_WithDraw").ReadOnly = False
        '    '        Case Withdraw_Type.AUTO
        '    '            '--- ไม่สามารถเลือกรายการเบิกได้
        '    '            .Columns("chk_WithDraw").Visible = False
        '    '            '--- ไม่สามารถเบิก รายการที่สองได้ต้องเบิก จาก รายการแรกหมดก่อน

        '    '            If e.RowIndex <> 0 Then
        '    '                .Rows(e.RowIndex).Cells("Col_Qty_Withdraw").ReadOnly = True
        '    '                .Rows(e.RowIndex).Cells("Col_Qty_Withdraw").Style.BackColor = Color.SkyBlue

        '    '                .Rows(e.RowIndex).Cells("chk_WithDraw").Value = False
        '    '                .Rows(e.RowIndex).Cells("chk_WithDraw").ReadOnly = False
        '    '            Else
        '    '                .Rows(e.RowIndex).Cells("Col_Qty_Withdraw").ReadOnly = False
        '    '                .Rows(e.RowIndex).Cells("Col_Qty_Withdraw").Style.BackColor = Color.White

        '    '                .Rows(e.RowIndex).Cells("chk_WithDraw").Value = True
        '    '                .Rows(e.RowIndex).Cells("chk_WithDraw").ReadOnly = True

        '    '            End If
        '    '    End Select
        '    'End With

        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try
    End Sub


    'Private Sub LoadPackageToGrid()

    '    Dim oms_Package As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
    '    Dim odtPackage As DataTable = New DataTable
    '    Try
    '        oms_Package.SearchData_Click("", "")
    '        odtPackage = oms_Package.DataTable

    '        With cbo_Package_Withdraw
    '            .DisplayMember = "Description"
    '            .ValueMember = "Package_Index"
    '            .DataSource = odtPackage
    '        End With

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        oms_Package = Nothing
    '        odtPackage = Nothing
    '    End Try

    'End Sub

    Private Sub grdShowCustom_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdShowCustom.CellBeginEdit
        Try
            Select Case Me.grdShowCustom.Columns(e.ColumnIndex).Name.ToUpper
                Case "CBO_PACKAGE_WITHDRAW"
                    Dim strSKU_Index As String = Me.grdShowCustom.Rows(e.RowIndex).Cells("col_Sku_Index").Value
                    Dim strPackage_Index As String = Me.grdShowCustom.Rows(e.RowIndex).Cells("col_Package_Index").Value
                    Dim objClassDB As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    'Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    Dim objDT As DataTable = New DataTable

                    objClassDB.getSKU_PackageReceive(strSKU_Index, strPackage_Index)
                    objDT = objClassDB.DataTable


                    Dim objGrdCombox As New DataGridViewComboBoxCell
                    With objGrdCombox

                        .DisplayMember = "Package"
                        .ValueMember = "Package_Index"
                        .DataSource = objDT
                    End With

                    grdShowCustom.Rows(e.RowIndex).Cells("cbo_Package_Withdraw") = objGrdCombox
            End Select
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            ClearReserv()
            'grdShowCustom.Rows.Clear()
            btnSearch_Click(Nothing, Nothing)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            Dim i As Integer = 0

            For i = 0 To grdShowCustom.Rows.Count - 1
                grdShowCustom.Rows(i).Cells("chk_WithDraw").Value = chkSelectAll.Checked
            Next
            grdShowCustom.Focus()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtSKU_ID_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSKU_ID.Enter
        SkuDes()
    End Sub

    Sub SkuDes()
        Try
            Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable
            Dim strSku_Index As String = ""

            Sku_Id = Me.txtSKU_ID.Text
            objClassDB.getSKU_Detail(Sku_Id)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                txtSku_Name.Text = objDT.Rows(0).Item("str1").ToString
                txtSKU_ID.Tag = objDT.Rows(0).Item("Sku_index").ToString
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Date : 12/01/2010
    ''' Update By : ja Update Consignee
    ''' add pop up 
    ''' </remarks>
    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try

            Dim frmConsignee_Popup As New frmConsignee_Popup
            frmConsignee_Popup.Customer_Index = Me.txtCustomer_Id.Tag
            frmConsignee_Popup.ShowDialog()

            Dim tmp_Index As String = ""
            tmp_Index = frmConsignee_Popup.Consignee_Index

            If Not tmp_Index = "" Then
                txtConsignee_Name.Tag = tmp_Index
                txtConsignee_Name.Text = frmConsignee_Popup.Consignee_Name
            End If

            frmConsignee_Popup.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' -------------------------------------------------
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub grdWithdrawItemLocation_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdWithdrawItemLocation.EditingControlShowing
        Try
            Dim strName As String = grdWithdrawItemLocation.Columns(grdWithdrawItemLocation.CurrentCell.ColumnIndex).Name
            If (strName <> "cbo_HandlingType2") And (strName <> "Col_Mfg_Date2") And (strName <> "col_Exp_Date2") Then
                Dim txtEdit2 As TextBox = e.Control
                RemoveHandler txtEdit2.KeyPress, AddressOf txtEdit_Keypress2
                AddHandler txtEdit2.KeyPress, AddressOf txtEdit_Keypress2
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' -------------------------------------------------
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub txtEdit_Keypress2(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 decimal
            Dim Column_Index As String = grdWithdrawItemLocation.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax21").Index
                    e.Handled = Check_GrdKeyPress2(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax22").Index
                    e.Handled = Check_GrdKeyPress2(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax23").Index
                    e.Handled = Check_GrdKeyPress2(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax24").Index
                    e.Handled = Check_GrdKeyPress2(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax25").Index
                    e.Handled = Check_GrdKeyPress2(0, e, Column_Index)


            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="format"></param>
    ''' <param name="e"></param>
    ''' <param name="Column_Index"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' -------------------------------------------------
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In
    ''' -------------------------------------------------
    ''' </remarks>
    Function Check_GrdKeyPress2(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdWithdrawItemLocation.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdWithdrawItemLocation.CurrentRow.Cells(Column_Index).EditedFormattedValue
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
            Throw ex
        End Try
    End Function

    Sub setLocationControl_By_Config()
        Try
            Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
            If Not oconfig.getConfig_Key_USE("USE_SKU_ITEM_QTY") Then
                lblSumPck.Location = New Point(416, 6)
                txtSumQty.Location = New Point(416, 20)
                lblSumAll.Location = New Point(380, 24)

                lbSumQty.Visible = False
                txtPackage.Visible = False

                'txtSumQty.Location = New Point(txtSumQty.Location.X + 91, txtSumQty.Location.Y)
                'lblSumPck.Location = New Point(lblSumPck.Location.X + 91, lblSumPck.Location.Y)
                'lblSumAll.Location = New Point(lblSumAll.Location.X + 91, lblSumAll.Location.Y)


                txtPackage.Visible = False
                lbSumQty.Visible = False


            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub grdShowCustom_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdShowCustom.CellEndEdit
        Try
            With Me.grdShowCustom.Rows(e.RowIndex)
                If .Cells("Col_Qty_Withdraw").ColumnIndex = e.ColumnIndex Then
                    If .Cells("Col_Qty_Withdraw").Value Is Nothing Then Exit Sub

                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Col_Qty_Withdraw").Value, GetType(Integer)) > 0 Then
                        .Cells("chk_WithDraw").Value = 1
                    Else
                        .Cells("chk_WithDraw").Value = 0
                    End If
                End If
            End With

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmPicking_Reserv_V4_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigWithdraw
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2)
                    oFunction.SW_Language_Column(Me, Me.grdShowCustom, 2)
                    oFunction.SW_Language_Column(Me, Me.grdWithdrawItemLocation, 2)
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class