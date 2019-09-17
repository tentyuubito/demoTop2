Imports WMS_STD_Master
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_OUTB_SO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master.CurrencyTextBox
Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_OUTB_WithDraw
Imports WMS_STD_OUTB_Transport

Public Class frmTransportBillLoad_Update

#Region "   1   FROM Load"

    Private _Route_Index As String = ""
    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal value As String)
            _Route_Index = value
        End Set
    End Property

    Private _SubRoute_Index As String = ""
    Public Property SubRoute_Index() As String
        Get
            Return _SubRoute_Index
        End Get
        Set(ByVal value As String)
            _SubRoute_Index = value
        End Set
    End Property
    Private _IsSubManifest As Integer = 0
    Private _IsUSE_TRANSPORT_DATETIME As Boolean = True
    Private _selectItem As Boolean = True
    Public Property IsSubManifest() As Integer
        Get
            Return _IsSubManifest
        End Get
        Set(ByVal value As Integer)
            _IsSubManifest = value
        End Set
    End Property
    Private _DataTable As New DataTable
    Public ReadOnly Property GetDataTable() As DataTable
        Get
            Return _DataTable
        End Get
    End Property

    Private objStatus As Manifest_Mode
    Public Enum Manifest_Mode
        ADD
        EDIT
    End Enum

    Public Sub New(ByVal Operation_Type As Manifest_Mode)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub

    Private Sub frmTransportBillLoad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ========== Manage Language Functions Begin ==========
            Dim oFunction As New WMS_STD_Master.W_Language
            'oFunction.Insert(Me)
            oFunction.SwitchLanguage(Me, 24)

            'Config config_DataGridColumn
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdItem_ToLoad)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdItem_OnTruck)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdManifest_Summary)


            oFunction.SW_Language_Column(Me, Me.grdItem_ToLoad, 24)
            oFunction.SW_Language_Column(Me, Me.grdItem_OnTruck, 24)
            oFunction.SW_Language_Column(Me, Me.grdManifest_Summary, 24)


            Application.CurrentCulture = New System.Globalization.CultureInfo("en-GB")
            dtpDate.Visible = True
            lb_to.Visible = True
            dateEnd.Visible = True
            txtKeySearch.Visible = False
            ' Me.cboRowPerPage.SelectedIndex = 0
            grdItem_ToLoad.AutoGenerateColumns = False
            grdItem_OnTruck.AutoGenerateColumns = False
            grdManifest_Summary.AutoGenerateColumns = False
            Me.cboDocTrip.SelectedIndex = 1
            Me.getDeleveryDay()
            'Dim oGetconfig As New WMS_SM_Formula.config_CustomSetting
            'If oGetconfig.getConfig_Key_USE("USE_VOLUME_TRANSPORT") = False Then
            '    lblVolume_ToLoad.Visible = False
            '    txtSumVolume_ToLoad.Visible = False
            '    lblSumVolume_OnTruck.Visible = False
            '    txtSumVolume_OnTruck.Visible = False
            '    lblSumVolume.Visible = False
            '    txtVolume.Visible = False

            '    col_Weight.Visible = False
            '    col_Weight_Shipped_OnTruck.Visible = False
            '    col_Weight_Sum.Visible = False
            'End If
            'If oGetconfig.getConfig_Key_USE("USE_WEIGHT_TRANSPORT") = False Then
            '    lblWeight_ToLoad.Visible = False
            '    txtSumWeight_ToLoad.Visible = False
            '    lblSumWeight_OnTruck.Visible = False
            '    txtSumWeight_OnTruck.Visible = False
            '    lbSumWt.Visible = False
            '    txtSumGrs_Wt.Visible = False

            '    col_Volume.Visible = False
            '    col_Volume_Shipped_OnTruck.Visible = False
            '    col_Volume_Sum.Visible = False
            'End If

            ' me.col_Expected_Delivery_Date.DefaultCellStyle.Format = new 
            config_Transport()
            SetMaskText()

            _selectItem = False
            LoadComboBox()
            'If (Me._Route_Index <> "") Or (Me._SubRoute_Index <> "") Then
            '    LoadDataGrid()
            'End If

            Select Case objStatus
                Case Manifest_Mode.ADD
                    Me.btnConfrim.Visible = False
                    Me.btnLoadToTruck.Visible = True
                Case Manifest_Mode.EDIT
                    Me.tabTrasport_Status.TabPages.Remove(Me.tbpItem_OnTruck)
                    Me.tabTrasport_Status.TabPages.Remove(Me.tbpManifest_Summary)
                    Me.btnConfrim.Location = New Point(7, 568)
                    Me.btnConfrim.Visible = True
                    Me.btnLoadToTruck.Visible = False
            End Select

            tbp_Search_SO.Dispose()
            tbpManifest_Summary.Dispose()


            'Me.LoadDataGrid()

            Me.pnlHide.Visible = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub SetMaskText()
        Try
            Dim strMaskText As String = "00/00/0000"
            If Not _IsUSE_TRANSPORT_DATETIME Then
                strMaskText = "00/00/0000"
                Me.col_Expected_Delivery_Date.DefaultCellStyle.Format = "d"
                Me.col_Time_ExpectedDocPickup.DefaultCellStyle.Format = "d"
                Me.col_SalesOrder_Date_OnTruck.DefaultCellStyle.Format = "d"
                Me.col_Expected_Delivery_Date_OnTruck.DefaultCellStyle.Format = "d"
                Me.col_TransportManifest_Date_OnTruck.DefaultCellStyle.Format = "d"

                Me.col_Expected_Delivery_Date.Width = 95
                Me.col_Time_ExpectedDocPickup.Width = 95
                Me.col_SalesOrder_Date_OnTruck.Width = 95
                Me.col_Expected_Delivery_Date_OnTruck.Width = 95
                Me.col_TransportManifest_Date_OnTruck.Width = 95
            Else
                strMaskText = "00/00/0000 90:00"
            End If

            'MaskText DataGridView
            Me.col_Expected_Delivery_Date.Mask = strMaskText
            Me.col_Time_ExpectedDocPickup.Mask = strMaskText



        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub config_Transport()
        Dim objClassDB As New tb_TransportManifest
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "USE_TRANSPORT_DATETIME"
                            _IsUSE_TRANSPORT_DATETIME = False

                        Case "USE_CUSTOMER_SHIPPING_LOCATION"
                            col_Shipping_Location_Name_OnTruck.Visible = False
                            col_CustomerShippingLocation.Visible = False
                            col_Shipping_Location_Name_Sum.Visible = False
                        Case "USE_VOLUME_TRANSPORT"
                            lblVolume_ToLoad.Visible = False
                            txtSumVolume_ToLoad.Visible = False
                            lblSumVolume_OnTruck.Visible = False
                            txtSumVolume_OnTruck.Visible = False
                            lblSumVolume.Visible = False
                            txtVolume.Visible = False

                            col_Volume.Visible = False
                            col_Volume_Shipped_OnTruck.Visible = False
                            col_Volume_Sum.Visible = False

                        Case "USE_WEIGHT_TRANSPORT"
                            lblWeight_ToLoad.Visible = False
                            txtSumWeight_ToLoad.Visible = False
                            lblSumWeight_OnTruck.Visible = False
                            txtSumWeight_OnTruck.Visible = False
                            lbSumWt.Visible = False
                            txtSumGrs_Wt.Visible = False

                            col_Weight.Visible = False
                            col_Weight_Shipped_OnTruck.Visible = False
                            col_Weight_Sum.Visible = False

                    End Select

                    i = i + 1
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub


    Private Sub LoadDataGrid()
        Try
            'tbpItem_ToLoad
            'tbpItem_OnTruck
            'tbpManifest_Summary
            'tbp_Search_SO

            Select Case Me.tabTrasport_Status.SelectedTab.Name
                Case tbpItem_ToLoad.Name
                    'รายการ SO ที่ยังไม่ได้จัดของ
                    LoadgrdItem_ToLoad(cboProcess_Id.SelectedValue.ToString.Trim)
                Case tbpItem_OnTruck.Name
                    'รายการ SO บนรถ
                    LoadgrdItem_OnTruck(cboProcess_Id.SelectedValue.ToString.Trim)
                Case tbpManifest_Summary.Name
                    'รายการสรุปของบนรถ
                    LoadgrdManifest_Summary()
                Case tbp_Search_SO.Name
                    LoadgrdSearch_Data()
            End Select

            SetnumRows()
            SetGridRowColor()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SetGridRowColor()
        Try
            Dim bgColor As Color
            'On Truck
            For i As Integer = 0 To grdItem_OnTruck.Rows.Count - 1
                '-----------------------------------------------------------------------------------------------------------------
                Select Case Me.grdItem_OnTruck.Rows(i).Cells("col_Process_Id").Value
                    Case Nothing
                        bgColor = Color.YellowGreen
                    Case "10"
                        bgColor = Color.YellowGreen
                    Case "25"
                        bgColor = Color.SkyBlue
                    Case "27"
                        bgColor = Color.Orange
                    Case Else
                        bgColor = Color.YellowGreen
                End Select
                Me.grdItem_OnTruck.Rows(i).Cells("col_SalesOrder_No").Style.BackColor = bgColor
                Me.grdItem_OnTruck.Rows(i).Cells("col_DocumentType_OnTruck").Style.BackColor = bgColor

            Next

            'Load
            For i As Integer = 0 To Me.grdItem_ToLoad.Rows.Count - 1
                Select Case Me.grdItem_ToLoad.Rows(i).Cells("col_Process_Id1").Value
                    Case Nothing
                        bgColor = Color.YellowGreen
                    Case "10"
                        bgColor = Color.YellowGreen
                    Case "25"
                        bgColor = Color.SkyBlue
                    Case "27"
                        bgColor = Color.Orange
                    Case Else
                        bgColor = Color.YellowGreen
                End Select
                Me.grdItem_ToLoad.Rows(i).Cells("col_SO_No").Style.BackColor = bgColor
                Me.grdItem_ToLoad.Rows(i).Cells("col_SODocumentType").Style.BackColor = bgColor

                Select Case Me.grdItem_ToLoad.Rows(i).Cells("RGB_Check").Value.ToString
                    Case 1 'พอเบิก
                        'grdItem_ToLoad.Rows(i).Cells("col_SO_No").Style.BackColor = Drawing.Color.LightGreen
                    Case 2 'ไม่พอเบิก
                        grdItem_ToLoad.Rows(i).DefaultCellStyle.BackColor = Drawing.Color.Red
                    Case Else
                End Select

            Next
            Dim a As String = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadComboBox()
        Try
            getComboRoute()
            getComboDistributionCenter()
            getComboTransportJobType()
            Me.getComboTransportRegion()
            getComboVehicleType()
            getComboProcess_Id()
            getComboUrgent_Id()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getComboUrgent_Id()
        Dim objClassDB As New ms_urgent()
        Dim objDT As DataTable = New DataTable
        Try
            objDT = objClassDB.getUrgentALL()
          
            Dim dr As DataRow = objDT.NewRow()

            dr("Priority_Name") = "ไม่ระบุ"
            dr("Priority_ID") = "-11"
            objDT.Rows.InsertAt(dr, 0)

            ddlUrgent.BeginUpdate()
            With ddlUrgent
                .DisplayMember = "Priority_Name"
                .ValueMember = "Priority_ID"
                .DataSource = objDT
            End With
            ddlUrgent.EndUpdate()
            If ddlUrgent.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    

    Private Sub getComboTransportRegion()
        Dim objClassDB As New ms_TransportRegion(ms_TransportRegion.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable

            Dim dr As DataRow = objDT.NewRow()
            dr("TransportRegion_Index") = "-11"
            dr("Description") = "ไม่ระบุ"
            objDT.Rows.InsertAt(dr, 0)

            cboTransportRegion.BeginUpdate()
            With cboTransportRegion
                .DisplayMember = "Description"
                .ValueMember = "TransportRegion_Index"
                .DataSource = objDT
            End With
            cboTransportRegion.EndUpdate()
            If cboTransportRegion.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getDeleveryDay()

        Dim objClassDB As New ms_Customer_Shipping_Location_Day '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllDataDay("")
            objDT = objClassDB.DataTable


            Dim cbItem As DataRow
            cbItem = objDT.NewRow
            cbItem("Day_index") = "-11"
            cbItem("Day_Id") = "-11"
            cbItem("Description") = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            With cbDeleveryDay
                .DisplayMember = "Description"
                .ValueMember = "Day_index"
                .DataSource = objDT
            End With

            ' *************************************
            cbDeleveryDay.SelectedIndex = cbDeleveryDay.Items.Count - 1



        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getComboProcess_Id()
        Dim objDT As DataTable = New DataTable
        Try
            'objClassDB.SearchData_Click("", "")
            'objDT = objClassDB.DataTable
            With objDT.Columns
                .Add("Description", GetType(String))
                .Add("Process_Id_Index", GetType(String))
            End With

            Dim cbItem(1) As String
            cbItem(0) = "ไม่ระบุ"
            cbItem(1) = ""
            objDT.Rows.Add(cbItem)

            cbItem(0) = "ใบขาย"
            cbItem(1) = "10"
            objDT.Rows.Add(cbItem)
            cbItem(0) = "ใบขนส่ง"
            cbItem(1) = "25"
            objDT.Rows.Add(cbItem)

            cboProcess_Id.BeginUpdate()
            With cboProcess_Id
                .DisplayMember = "Description"
                .ValueMember = "Process_Id_Index"
                .DataSource = objDT
            End With
            cboProcess_Id.EndUpdate()
            '  cboProcess_Id.SelectedIndex = 0

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub
    Private Sub getComboVehicleType()
        Dim objClassDB As New ms_VehicleType(ms_VehicleType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            Dim cbItem(3) As String
            cbItem(0) = "-11"
            cbItem(2) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboVehicleType.BeginUpdate()
            With cboVehicleType
                .DisplayMember = "Description"
                .ValueMember = "VehicleType_Index"
                .DataSource = objDT
            End With
            cboVehicleType.SelectedIndex = cboVehicleType.Items.Count - 1
            cboVehicleType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboTransportJobType()
        Dim objClassDB As New ms_TransportJobType(ms_TransportJobType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            Dim cbItem(2) As String
            cbItem(0) = "-11"
            cbItem(1) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboTransportType.BeginUpdate()
            With cboTransportType
                .DisplayMember = "Description"
                .ValueMember = "TransportJobType_Index"
                .DataSource = objDT
            End With
            cboTransportType.SelectedIndex = cboTransportType.Items.Count - 1
            cboTransportType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboDistributionCenter()
        Dim objClassDB As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable
            Dim cbItem(3) As String
            cbItem(0) = "-11"
            cbItem(2) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboDistributionCenter.BeginUpdate()
            With cboDistributionCenter
                .DisplayMember = "Description"
                .ValueMember = "DistributionCenter_Index"
                .DataSource = objDT
            End With
            cboDistributionCenter.SelectedIndex = cboDistributionCenter.Items.Count - 1
            cboDistributionCenter.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboRoute()
        Dim objClassDB As New ms_Route(ms_Route.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            Dim cbItem(3) As String
            cbItem(0) = "-11"
            cbItem(2) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            cboRoute.BeginUpdate()
            With cboRoute
                .DisplayMember = "Description"
                .ValueMember = "Route_Index"
                .DataSource = objDT
            End With

            cboRoute.EndUpdate()
            If objDT.Rows.Count > 0 Then
                cboRoute.SelectedValue = "-11"
            End If
            If _Route_Index <> "" Then
                cboRoute.SelectedValue = _Route_Index
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboSubRoute(ByVal vRoute_Index As String)
        Dim objClassDB As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            'If (cboRoute.SelectedIndex = (cboRoute.Items.Count - 1)) Then
            '    cboSubRoute.SelectedIndex = cboSubRoute.Items.Count - 1
            '    Exit Sub
            'End If


            objClassDB.GetAllAsDataTable(vRoute_Index)
            objDT = objClassDB.DataTable
            Dim cbItem(5) As String
            cbItem(0) = "-11"
            cbItem(3) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)

            cboSubRoute.BeginUpdate()
            With cboSubRoute
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = objDT
            End With

            cboSubRoute.EndUpdate()
            If objDT.Rows.Count > 0 Then
                cboSubRoute.SelectedValue = "-11"
            End If
            If _SubRoute_Index <> "" Then
                cboSubRoute.SelectedValue = _SubRoute_Index
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Add Date : 28/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Dong_kk
    ''' </remarks>

    Private Sub LoadgrdItem_ToLoad(ByVal Process_Id As String)
        Dim objtb_SalesOrder As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
        Dim objDT As New DataTable
        Dim sumWeight As Double = 0
        Dim sumVolume As Double = 0
        Dim sumQty As Double = 0
        Try
            objtb_SalesOrder.getSO_Totruck(sqlCondition(grdItem_ToLoad.Name), _IsSubManifest, Process_Id, _selectItem, txtTop.Text)
            Me.grdItem_ToLoad.DataSource = GetRowSelected(objtb_SalesOrder.DataTable)
            If grdItem_ToLoad.RowCount = 0 Then
                txtSumQty_ToLoad.Text = "0"
                txtSumWeight_ToLoad.Text = "0.0000"
                txtSumVolume_ToLoad.Text = "0.0000"
            Else
                txtSumQty_ToLoad.Text = Me.grdItem_ToLoad.DataSource.Compute("Sum(Qty)", "Qty >= 0")
                txtSumWeight_ToLoad.Text = Format(CDbl(Me.grdItem_ToLoad.DataSource.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
                txtSumVolume_ToLoad.Text = Format(CDbl(Me.grdItem_ToLoad.DataSource.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")
            End If


            'objtb_SalesOrder.getSO_Totruck(sqlCondition(grdItem_ToLoad.Name), _IsSubManifest, Process_Id, _selectItem, txtTop.Text)
            'objDT = GetRowSelected(objtb_SalesOrder.DataTable)
            'Me.grdItem_ToLoad.Rows.Clear()
            'Me.grdItem_ToLoad.Refresh()

            'For iRow As Integer = 0 To objDT.Rows.Count - 1
            '    With Me.grdItem_ToLoad
            '        Me.grdItem_ToLoad.Rows.Add()
            '        grdItem_ToLoad.Rows(iRow).Cells("chkSelect").Value = objDT.Rows(iRow)("chkSelect")
            '        .Rows(iRow).Cells("col_SubRoute").Value = objDT.Rows(iRow)("SubRoute")
            '        '.Rows(iRow).Cells("col_SO_No").Value = objDT.Rows(iRow)("SalesOrder_No")
            '        '.Rows(iRow).Cells("col_Invoice_No").Value = objDT.Rows(iRow)("str1")
            '        '.Rows(iRow).Cells("col_SODocumentType").Value = objDT.Rows(iRow)("DocumentType")
            '        '.Rows(iRow).Cells("col_SO_Date").Value = CDate(objDT.Rows(iRow)("SalesOrder_Date")).ToString("dd/MM/yyyy")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")
            '        '.Rows(iRow).Cells("").Value = objDT.Rows(iRow)("")


            '    End With
            'Next

            'Me.grdItem_ToLoad.Sort(col_Sort, System.ComponentModel.ListSortDirection.Descending)
            Me.grdItem_ToLoad.Sort(col_Sort, System.ComponentModel.ListSortDirection.Ascending)
            'Me.grdItem_ToLoad.Columns("col_SO_No").DefaultCellStyle.BackColor = Color.YellowGreen
        Catch ex As Exception
            Throw ex
        Finally
            objtb_SalesOrder = Nothing
        End Try
    End Sub

    'Private Sub LoadgrdItem_OnTruck(ByVal Process_Id As String)
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getTransportManifest_OnTruck(sqlCondition(grdItem_OnTruck.Name) & " and status_id in (1,2,3,6) ", _IsSubManifest, Process_Id)
    '        Me.grdItem_OnTruck.DataSource = objtbManifest.DataTable

    '        If grdItem_OnTruck.RowCount = 0 Then
    '            txtSumQty_OnTruck.Text = "0"
    '            txtSumWeight_OnTruck.Text = "0.0000"
    '            txtSumVolume_OnTruck.Text = "0.0000"
    '        Else
    '            txtSumQty_OnTruck.Text = objtbManifest.DataTable.Compute("Sum(Qty_Shipped)", "Qty_Shipped >= 0")
    '            txtSumWeight_OnTruck.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight_Shipped)", "Weight_Shipped >= 0")), "#,##0.0000")
    '            txtSumVolume_OnTruck.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume_Shipped)", "Volume_Shipped >= 0")), "#,##0.0000")
    '        End If

    '        CType(Me.grdItem_OnTruck.DataSource, DataTable).AcceptChanges()
    '        'Me.grdItem_ToLoad.Refresh()
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

    Private Sub LoadgrdItem_OnTruck(ByVal Process_Id As String)
        'best update 20-08-2012
        'data เป็น dataReader
        Dim objtbManifest As New tb_TransportManifest
        Try
            objtbManifest.getTransportManifest_OnTruck(sqlCondition(grdItem_OnTruck.Name) & " and status_id in (1,2,3,6) ", _IsSubManifest, Process_Id)
            Me.grdItem_OnTruck.DataSource = objtbManifest.DataTable

            If grdItem_OnTruck.RowCount = 0 Then
                txtSumQty_OnTruck.Text = "0"
                txtSumWeight_OnTruck.Text = "0.0000"
                txtSumVolume_OnTruck.Text = "0.0000"
            Else
                txtSumQty_OnTruck.Text = objtbManifest.DataTable.Compute("Sum(Qty_Shipped)", "Qty_Shipped >= 0")
                txtSumWeight_OnTruck.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight_Shipped)", "Weight_Shipped >= 0")), "#,##0.0000")
                txtSumVolume_OnTruck.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume_Shipped)", "Volume_Shipped >= 0")), "#,##0.0000")
            End If

            CType(Me.grdItem_OnTruck.DataSource, DataTable).AcceptChanges()
            'Me.grdItem_ToLoad.Refresh()
        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub

    'Private Sub LoadgrdManifest_Summary()
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getTransportManifest_Summary(sqlCondition(grdManifest_Summary.Name), "", _IsSubManifest)
    '        Me.grdManifest_Summary.DataSource = objtbManifest.DataTable
    '        If grdManifest_Summary.RowCount = 0 Then
    '            txtSumTruck.Text = "0"
    '            txtSumDoc.Text = "0"
    '            txtSumQty.Text = "0"
    '            txtSumGrs_Wt.Text = "0.0000"
    '            txtVolume.Text = "0.0000"
    '        Else
    '            txtSumTruck.Text = grdManifest_Summary.RowCount.ToString
    '            txtSumDoc.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
    '            txtSumQty.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
    '            txtSumGrs_Wt.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
    '            txtVolume.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

 
    Private Sub LoadgrdManifest_Summary()
        'best update 20-08-2012
        'data เป็น dataReader
        Dim objtbManifest As New tb_TransportManifest
        Try
            objtbManifest.getTransportManifest_Summary(sqlCondition(grdManifest_Summary.Name), "", _IsSubManifest)
            Me.grdManifest_Summary.DataSource = objtbManifest.DataTable
            If grdManifest_Summary.RowCount = 0 Then
                txtSumTruck.Text = "0"
                txtSumDoc.Text = "0"
                txtSumQty.Text = "0"
                txtSumGrs_Wt.Text = "0.0000"
                txtVolume.Text = "0.0000"
            Else
                txtSumTruck.Text = grdManifest_Summary.RowCount.ToString
                txtSumDoc.Text = objtbManifest.DataTable.Compute("Sum(Bill_Sum)", "Bill_Sum >= 0")
                txtSumQty.Text = objtbManifest.DataTable.Compute("Sum(Qty)", "Qty >= 0")
                txtSumGrs_Wt.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Weight)", "Weight >= 0")), "#,##0.0000")
                txtVolume.Text = Format(CDbl(objtbManifest.DataTable.Compute("Sum(Volume)", "Volume >= 0")), "#,##0.0000")
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub
    'Private Sub LoadgrdSearch_Data()
    '    Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
    '    Try
    '        objtbManifest.getSearch_SO(sqlCondition(grdSearch_Data.Name))
    '        Me.grdSearch_Data.DataSource = objtbManifest.DataTable

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objtbManifest = Nothing
    '    End Try
    'End Sub

    Private Sub LoadgrdSearch_Data()
        'best update 20-08-2012
        'data เป็น dataReader
        Dim objtbManifest As New tb_TransportManifest
        Try
            objtbManifest.getSearch_SO(sqlCondition(grdSearch_Data.Name))
            Me.grdSearch_Data.DataSource = objtbManifest.DataTable

        Catch ex As Exception
            Throw ex
        Finally
            objtbManifest = Nothing
        End Try
    End Sub

    Private Sub cboRoute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectedIndexChanged
        Try
            getComboSubRoute(cboRoute.SelectedValue.ToString)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

#Region "   2   FROM Exit"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExit_OnTruck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit_OnTruck.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#End Region

#Region "   3   check select All"
    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If Me.grdItem_ToLoad.RowCount = 0 Then Exit Sub
            Dim i As Integer

            For i = 0 To grdItem_ToLoad.Rows.Count - 1
                grdItem_ToLoad.Rows(i).Cells("chkSelect").Value = chkSelectAll.Checked
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Function chkSelect_OnTruck() As DataTable
        Try
            Dim _dataTable As New DataTable
            If Me.grdItem_ToLoad.DataSource Is Nothing Then Return _dataTable

            Me.grdItem_ToLoad.EndEdit()
            CType(Me.grdItem_ToLoad.DataSource, DataTable).AcceptChanges()
            Dim odtTemp As New DataTable
            Dim odrTemp() As DataRow
            odtTemp = Me.grdItem_ToLoad.DataSource
            odrTemp = odtTemp.Select("chkSelect = 1", "chkSelect")

            _dataTable = odtTemp.Clone
            For Each odrSelected As DataRow In odrTemp
                Select Case odrSelected("RGB_Check").ToString
                    Case 2
                        W_MSG_Information("รายการสินค้าไม่พอไม่สามารถจัดรถได้")
                        _dataTable = New DataTable
                        Return _dataTable
                End Select

                _dataTable.Rows.Add(odrSelected.ItemArray)
            Next
            SetGridRowColor()
            ' odtTemp.AcceptChanges()
            Return _dataTable

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function chkRow_notRoute(ByVal Datachk As DataTable) As List(Of String)
        Try
            Dim subRoute As New List(Of String)
            For irow As Integer = 0 To Datachk.Rows.Count - 1
                subRoute.Add(Datachk.Rows(irow)("Customer_Shipping_Id").ToString)
            Next
            Return subRoute
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub chkAll_OnTruck_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll_OnTruck.CheckedChanged
        Try
            If Me.grdItem_OnTruck.RowCount = 0 Then Exit Sub
            Dim i As Integer

            For i = 0 To grdItem_OnTruck.Rows.Count - 1
                grdItem_OnTruck.Rows(i).Cells("chkItemOnTruck").Value = chkAll_OnTruck.Checked
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "   4   LoadItem ToTruck"
    Private Sub btnLoadToTruck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoadToTruck.Click
        Try
            _selectItem = False          
            Dim Check_NotRoute_InMaster As Boolean = False
            Dim not_Route As New List(Of String)
            Dim dtTemp_NotRoute As New DataTable
            Dim ObjChk_AndInRoute As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
            Select Case objStatus
                Case Manifest_Mode.ADD
                    Dim dtTempLoad As New DataTable
                    dtTempLoad = chkSelect_OnTruck()
                    If dtTempLoad.Rows.Count = 0 Then
                        W_MSG_Information_ByIndex(300032)
                        Me.SetGridRowColor()
                        Exit Sub
                    End If
                    For iRows As Integer = 0 To dtTempLoad.Rows.Count - 1
                        ObjChk_AndInRoute.chkSalesOrder_Notroute_Inmaster(dtTempLoad.Rows(iRows)("SalesOrder_Index").ToString, dtTempLoad.Rows(iRows)("Customer_Shipping_Location_Index").ToString)
                        If iRows = 0 Then
                            dtTemp_NotRoute = ObjChk_AndInRoute.DataTable.Clone
                        End If
                        dtTemp_NotRoute.Merge(ObjChk_AndInRoute.DataTable)
                    Next
                    'not_Route = chkRow_notRoute(dtTemp_NotRoute)
                    If dtTemp_NotRoute.Rows.Count > 0 Then
                        If W_MSG_Confirm(GetMessage_Data("100049")) = Windows.Forms.DialogResult.Yes Then
                            Dim frmcus As New frmCustomer_Shipping_No_Route()
                            frmcus.SalesOrder_NotRoute = dtTemp_NotRoute
                            frmcus.ShowDialog()
                            If frmcus.chk_NotRoute = False Then
                                Exit Sub
                            End If
                        Else
                            Exit Sub
                        End If
                    End If

                    Dim frm As New frmTransportManifest_Update(frmTransportManifest_Update.Manifest_Mode.ADD)
                    frm.dataTable = dtTempLoad
                    frm.IsSubManifest = _IsSubManifest
                    frm.ShowDialog()
                    If frmTransportManifest.Manifest_Mode.ADD = 0 Then
                        Dim odtTemp As DataTable
                        odtTemp = CType(grdItem_ToLoad.DataSource, DataTable)
                        For Each odrSelected As DataRow In dtTempLoad.Rows
                            Dim drArrLoad() As DataRow = odtTemp.Select("SalesOrder_Index='" & odrSelected("SalesOrder_Index") & "'")
                            odtTemp.Rows.Remove(drArrLoad(0))
                        Next
                        odtTemp.AcceptChanges()
                        'LoadDataGrid()
                    End If

                    'Case Manifest_Mode.EDIT
                    '    _DataTable = chkSelect_OnTruck()
                    '    Me.Close()
            End Select

            '_selectItem = False
            'Select Case objStatus
            '    Case Manifest_Mode.ADD
            '        Dim dtTempLoad As New DataTable
            '        dtTempLoad = chkSelect_OnTruck()
            '        If dtTempLoad.Rows.Count = 0 Then
            '            W_MSG_Information_ByIndex(300032)
            '            Exit Sub
            '        End If
            '        Dim frm As New frmTransportLoadDoc
            '        frm.dataTable = dtTempLoad
            '        frm.IsSubManifest = _IsSubManifest
            '        frm.ShowDialog()
            '        If frm.SaveType = 1 Then
            '            Dim odtTemp As DataTable
            '            odtTemp = CType(grdItem_ToLoad.DataSource, DataTable)
            '            For Each odrSelected As DataRow In dtTempLoad.Rows
            '                Dim drArrLoad() As DataRow = odtTemp.Select("SalesOrder_Index='" & odrSelected("SalesOrder_Index") & "'")
            '                odtTemp.Rows.Remove(drArrLoad(0))
            '            Next
            '            odtTemp.AcceptChanges()
            '            LoadDataGrid()
            '        End If

            '        'Case Manifest_Mode.EDIT
            '        '    _DataTable = chkSelect_OnTruck()
            '        '    Me.Close()
            'End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCancel_OnTruck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel_OnTruck.Click
        Try
            If Me.grdItem_OnTruck.RowCount = 0 Then Exit Sub
            If W_MSG_Confirm_ByIndex(100004) = Windows.Forms.DialogResult.Yes = True Then
                Dim dtTemp As New DataTable
                dtTemp = grdItem_OnTruck.DataSource
                Dim drItem() As DataRow = dtTemp.Select("chkItemOnTruck=1", "chkItemOnTruck")
                Dim boolCheck As Boolean = False
                For Each dr As DataRow In drItem
                    Dim objDelectManifestItem As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.DELETE)
                    objDelectManifestItem.TransportManifestItem_Index = dr("TransportManifestItem_Index").ToString
                    objDelectManifestItem.TransportManifest_Index = dr("TransportManifest_Index").ToString
                    If objDelectManifestItem.Delete(ManifestTransaction_Update.enuDelete.DELETEITEM) <> "" Then
                        boolCheck = True
                    End If
                Next
                If boolCheck Then
                    W_MSG_Information_ByIndex(1)
                Else
                    W_MSG_Information_ByIndex(2)
                End If
                LoadDataGrid()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   5   Seach Condition"

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            _selectItem = False
            LoadDataGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Function sqlCondition(ByVal dataGridName As String) As String
        Try
            Dim strWhere As String = ""
            Dim intDay As Integer = Me.dtpDate.Value.Day
            Dim intMonth As Integer = Me.dtpDate.Value.Month
            Dim intYear As Integer = Me.dtpDate.Value.Year
            Dim StartDate As String = Format(dtpDate.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
            Dim EndtDate As String = Format(dateEnd.Value, "yyyy/MM/dd").ToString() + "  23:59:00"

            Select Case dataGridName.ToUpper
                Case "GRDMANIFEST_SUMMARY", "GRDITEM_ONTRUCK"
                    If cboDistributionCenter.SelectedValue <> "-11" Then
                        strWhere &= " and DistributionCenter_Index = '" & cboDistributionCenter.SelectedValue.ToString & "'"
                    End If
                    If cboRoute.SelectedValue <> "-11" Then
                        strWhere &= " and Route_Index = '" & cboRoute.SelectedValue.ToString & "'"
                    End If
                    If cboSubRoute.SelectedValue <> "-11" Then
                        strWhere &= " and SubRoute_Index = '" & cboSubRoute.SelectedValue.ToString & "'"
                    End If
                    If cboVehicleType.SelectedValue <> "-11" Then
                        strWhere &= " and VehicleType_Index = '" & cboVehicleType.SelectedValue.ToString & "'"
                    End If
                    If cboTransportType.SelectedValue <> "-11" Then
                        strWhere &= " and TransportJobType_Index = '" & cboTransportType.SelectedValue.ToString & "'"
                    End If


                    If rdbCustomer.Checked = True Then
                        If txtKeySearch.Text.Trim <> "" Then
                            strWhere &= " and Customer_Index like '" & txtKeySearch.Tag.ToString & "%'"
                        End If

                    End If

                    If rdoCustomerShipping.Checked = True Then
                        If txtKeySearch.Text.Trim <> "" Then
                            strWhere &= " and Customer_Shipping_Location_Id like '" & txtKeySearch.Text.ToString & "%'"

                        End If
                    End If


                Case "GRDITEM_TOLOAD"

                    'If Me.chkAddCondition.Checked = False Then


                    If cboDistributionCenter.SelectedValue <> "-11" Then
                        strWhere &= " and DistributionCenter_Index = '" & cboDistributionCenter.SelectedValue.ToString & "'"
                    End If
                    If cboRoute.SelectedValue <> "-11" Then
                        strWhere &= " and Route_Index = '" & cboRoute.SelectedValue.ToString & "'"
                    End If
                    If cboSubRoute.SelectedValue <> "-11" Then
                        strWhere &= " and SubRoute_Index = '" & cboSubRoute.SelectedValue.ToString & "'"
                    End If
                    If cboTransportRegion.SelectedValue <> "-11" Then
                        strWhere &= " and TransportRegion_Index = '" & cboTransportRegion.SelectedValue.ToString & "'"
                    End If
                    'Check Invoice Or SO
                    If rdbSO_Date.Checked = True Then
                        If chkShowInvoice.Checked = True Then
                            strWhere &= " and Str1<>'' "
                            'strWhere &= " and IV_Update_Date Between '" & StartDate & "' and '" & EndtDate & "'"
                            strWhere &= " and DateSalesOrder Between '" & StartDate & "' and '" & EndtDate & "'"
                        Else
                            strWhere &= " and DateSalesOrder Between '" & StartDate & "' and '" & EndtDate & "'"
                        End If
                    End If

                    If rdoExpectedDeliveryDate.Checked = True Then
                        strWhere &= " and Expected_Delivery_Date Between '" & StartDate & "' and '" & EndtDate & "'"
                    End If
                    If rdbSO_No.Checked = True Then
                        If txtKeySearch.Text.Trim = "" Then
                            strWhere &= " and SalesOrder_No = '" & txtKeySearch.Text & "'"
                        Else
                            strWhere &= " and SalesOrder_No like '" & txtKeySearch.Text & "%'"
                        End If
                    End If

                    'best up date 28-08-2012'''

                    ''''''''''''''''''
                    If rdoWithdraw_No.Checked = True Then
                        strWhere &= " and SalesOrder_Index "
                        strWhere &= " in (  SELECT	DocumentPlan_Index "
                        strWhere &= "       FROM	tb_WithDrawItem inner join"
                        strWhere &= "               tb_WithDraw On tb_WithDraw.WithDraw_Index = tb_WithDrawItem.WithDraw_Index"
                        strWhere &= "       WHERE	WithDraw_No like '" & txtKeySearch.Text & "%')"

                    End If
                    If rdoWithdraw_Date.Checked = True Then
                        strWhere &= " and SalesOrder_Index "
                        strWhere &= " in (  SELECT	DocumentPlan_Index "
                        strWhere &= "       FROM	tb_WithDrawItem inner join"
                        strWhere &= "               tb_WithDraw On tb_WithDraw.WithDraw_Index = tb_WithDrawItem.WithDraw_Index"
                        strWhere &= "       WHERE WithDraw_date Between '" & StartDate & "' and '" & EndtDate & "') "
                    End If

                    If rdoRefNo1.Checked = True Then
                        strWhere &= " and str1 like '" & txtKeySearch.Text & "%'"
                    End If


                    If rdbCustomer.Checked = True Then
                        If txtKeySearch.Text.Trim <> "" Then
                            strWhere &= " and Customer_Index like '" & txtKeySearch.Tag.ToString & "%'"
                        End If

                    End If

                    If rdoCustomerShipping.Checked = True Then
                        If txtKeySearch.Text.Trim <> "" Then
                            strWhere &= " and Customer_Shipping_Location_Id like '" & txtKeySearch.Text.ToString & "%'"

                        End If
                    End If

                    If txtRoute.Text.Trim <> "" Then
                        strWhere &= "  and Customer_Shipping_index in ( "
                        strWhere &= "       select Customer_Shipping_index "
                        strWhere &= "       from ms_Customer_Shipping "
                        strWhere &= "  where isnull(str2,'')    like '%" & txtRoute.Text.Trim & "%' )"
                    End If

                    If ddlUrgent.SelectedValue <> "-11" Then
                        strWhere &= "  and SalesOrder_index in ( "
                        strWhere &= "       select SalesOrder_index "
                        strWhere &= "       from tb_SalesOrder "
                        strWhere &= "  where isnull(Urgent_Id,'')  = '" & ddlUrgent.SelectedValue & "' "
                        strWhere &= " ) "
                    End If

                Case "GRDSEARCH_DATA"

                    If rdbSO_No.Checked = True Then
                        If txtKeySearch.Text.Trim = "" Then
                            strWhere &= " and SalesOrder_No = '" & txtKeySearch.Text & "'"
                        Else
                            strWhere &= " and SalesOrder_No like '" & txtKeySearch.Text & "%'"
                        End If
                    End If

                    If rdoExpectedDeliveryDate.Checked = True Then
                        strWhere &= " and Expected_Delivery_Date Between '" & StartDate & "' and '" & EndtDate & "'"
                    End If

                    If rdbCustomer.Checked = True Then
                        If txtKeySearch.Text.Trim <> "" Then
                            strWhere &= " and Customer_Index = '" & txtKeySearch.Tag.ToString & "'"
                        End If

                    End If

                    If rdoCustomerShipping.Checked = True Then
                        If txtKeySearch.Text.Trim <> "" Then
                            strWhere &= " and Company_Name like '%" & txtKeySearch.Text.ToString & "%'"
                        End If
                    End If

            End Select



            Select Case Me.cbDeleveryDay.SelectedValue
                Case "-11", Nothing
                    ' strWhere &= " "
                Case Else
                    strWhere &= " AND (DayIndex like '%" & Me.cbDeleveryDay.SelectedValue & "%')"
            End Select

            Return strWhere

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub closeAllCheckbox(ByVal rdoEnble As RadioButton)
        Try
            Dim radName As String = rdoEnble.Name.ToString
            btnPop_Search.Visible = False
            Select Case radName
                Case "rdbSO_Date", "rdoExpectedDeliveryDate", "rdoWithdraw_Date"
                    dtpDate.Visible = True
                    lb_to.Visible = True
                    dateEnd.Visible = True
                    txtKeySearch.Visible = False
                Case Else
                    dtpDate.Visible = False
                    lb_to.Visible = False
                    dateEnd.Visible = False
                    txtKeySearch.Visible = True
            End Select
            Me.txtKeySearch.Text = ""
            Me.txtKeySearch.Tag = ""
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub rdbSO_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSO_Date.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub rdoExpectedDeliveryDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoExpectedDeliveryDate.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbSO_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSO_No.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoCustomerShipping_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoCustomerShipping.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoRefNo1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoRefNo1.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoWithdraw_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoWithdraw_Date.CheckedChanged
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoWithdraw_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoWithdraw_No.CheckedChanged
        Try

            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub btnEditOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditOrder.Click
        'Try
        '    If grdItem_ToLoad.RowCount = 0 Then Exit Sub
        '    Dim frm As New frmTOItem
        '    frm.objStatus = frmTOItem.enuOperation_Type.UPDATE
        '    frm.Status = 3
        '    frm.SalesOrder_Index = grdItem_ToLoad.CurrentRow.Cells("col_System_Index").Value.ToString()
        '    frm.ShowDialog()
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try
        Try
            If grdItem_ToLoad.RowCount = 0 Then Exit Sub

            Select Case grdItem_ToLoad.CurrentRow.Cells("col_Process_Id1").Value
                Case Nothing
                    Dim frm As New frmSO
                    frm.objStatus = frmSO.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_ToLoad.CurrentRow.Cells("col_System_Index").Value.ToString()
                    frm.ShowDialog()
                Case "10"
                    Dim frm As New frmSO
                    frm.objStatus = frmSO.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_ToLoad.CurrentRow.Cells("col_System_Index").Value.ToString()
                    frm.ShowDialog()
                Case "25"
                    Dim frm As New frmTOItem
                    frm.objStatus = frmTOItem.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_ToLoad.CurrentRow.Cells("col_System_Index").Value.ToString()
                    frm.ShowDialog()
                Case "27"
                    Dim frm As New frmGROItem
                    frm.objStatus = frmGROItem.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_ToLoad.CurrentRow.Cells("col_System_Index").Value.ToString()
                    frm.ShowDialog()
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnView_OnTruck_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView_OnTruck.Click
        Try
            If grdItem_OnTruck.RowCount = 0 Then Exit Sub

            Select Case grdItem_OnTruck.CurrentRow.Cells("col_Process_Id").Value
                Case Nothing
                    Dim frm As New frmSO
                    frm.objStatus = frmSO.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_OnTruck.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
                Case "10"
                    Dim frm As New frmSO
                    frm.objStatus = frmSO.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_OnTruck.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
                Case "25"
                    Dim frm As New frmTOItem
                    frm.objStatus = frmTOItem.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_OnTruck.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
                Case "27"
                    Dim frm As New frmGROItem
                    frm.objStatus = frmGROItem.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdItem_OnTruck.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()

            End Select

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
    ''' Add Date : 29/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Dong_kk
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub btnPop_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPop_Search.Click
        Try
            If rdbCustomer.Checked = True Then
                Dim frm As New frmCustmer_Popup
                frm.ShowDialog()
                '    *** Recive value ****
                Dim tmpCustomer_Index As String = ""
                tmpCustomer_Index = frm.Customer_Index 'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                If Not tmpCustomer_Index = "" Then
                    Me.txtKeySearch.Text = frm.customerName
                    Me.txtKeySearch.Tag = frm.Customer_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
                ' *********************
                frm.Close()
            End If
            If rdoCustomerShipping.Checked = True Then
                Dim frm As New frmCus_Ship_Location_Popup
                frm.ShowDialog()

                Dim tmpCustomer_Shipping_Location_Index As String = ""
                tmpCustomer_Shipping_Location_Index = frm.Customer_Shipping_Location_Index

                If tmpCustomer_Shipping_Location_Index = "" Then
                    Exit Sub
                End If

                If Not tmpCustomer_Shipping_Location_Index = "" Then
                    Me.txtKeySearch.Tag = tmpCustomer_Shipping_Location_Index
                    Me.txtKeySearch.Text = frm.strCustomer_Shipping_Location_Name
                Else
                    Me.txtKeySearch.Tag = ""
                    Me.txtKeySearch.Text = ""

                End If

                frm.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub SetnumRows()
        ' ====== TODO: HARDCODE-MSG
        Try
            Dim numRows As Integer = 0

            numRows = grdItem_ToLoad.Rows.Count
            If numRows > 0 Then
                lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
            Else
                lbCountRows.Text = "ไม่พบรายการ"
            End If

            numRows = grdItem_OnTruck.Rows.Count
            If numRows > 0 Then
                lbCountRows2.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
            Else
                lbCountRows2.Text = "ไม่พบรายการ"
            End If
            numRows = grdManifest_Summary.Rows.Count
            If numRows > 0 Then
                lbCountRows3.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
            Else
                lbCountRows3.Text = "ไม่พบรายการ"
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnView_Manifest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnView_Manifest.Click
        Try
            If grdManifest_Summary.RowCount = 0 Then Exit Sub

            Dim frm As New frmTransportManifest(frmTransportManifest.Manifest_Mode.EDIT)
            frm.TransportManifest_Index = grdManifest_Summary.CurrentRow.Cells("col_TransportManifest_Index_Sum").Value.ToString
            frm.IsSubManifest = _IsSubManifest
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnExit_ManifsetSummary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit_ManifsetSummary.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub grdItem_ToLoad_CellMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdItem_ToLoad.CellMouseClick
    '    Try
    '        If grdItem_ToLoad.RowCount = 0 Then Exit Sub
    '        If e.Button = Windows.Forms.MouseButtons.Right Then
    '            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToUpper
    '                Case "COL_EXPECTED_DELIVERY_DATE"
    '                    If grdItem_ToLoad.CurrentRow.Cells("COL_EXPECTED_DELIVERY_DATE").Value.ToString <> "" Then
    '                        UcDateTime1.dtTime.Value = CDate(grdItem_ToLoad.CurrentRow.Cells("COL_EXPECTED_DELIVERY_DATE").Value.ToString)
    '                        UcDateTime1.tTime.Value = CDate(grdItem_ToLoad.CurrentRow.Cells("COL_EXPECTED_DELIVERY_DATE").Value.ToString)
    '                    End If
    '                    UcDateTime1.Location = Me.PointToClient(Cursor.Position) ' New Point(Mouse, e.Location.Y)
    '                    UcDateTime1.Visible = True
    '                    tmpRow_Index = e.RowIndex
    '                    'Dim dTimeTemp As DateTime = CDate(UcDateTime1.dtTime.Value.ToShortDateString & " " & UcDateTime1.tTime.Value.ToString("HH:MM:ss"))
    '                    'grdItem_ToLoad.CurrentRow.Cells("COL_EXPECTED_DELIVERY_DATE").Value = dTimeTemp

    '            End Select
    '        Else
    '            UcDateTime1.Visible = False
    '        End If

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
    'Dim tmpRow_Index As Integer = 0
    'Private Sub UcDateTime1_VisibleChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If grdItem_ToLoad.RowCount = 0 Then Exit Sub
    '        If UcDateTime1.Visible = False Then
    '            Dim dTimeTemp As DateTime = CDate(UcDateTime1.dtTime.Value.ToShortDateString & " " & UcDateTime1.tTime.Text)
    '            grdItem_ToLoad.Rows(tmpRow_Index).Cells("COL_EXPECTED_DELIVERY_DATE").Value = dTimeTemp
    '        End If

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub grdItem_ToLoad_CellBeginEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellCancelEventArgs) Handles grdItem_ToLoad.CellBeginEdit

    '    Try
    '        Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToUpper
    '            Case "COL_EXPECTED_DELIVERY_DATE", "COL_TIME_EXPECTEDDOCPICKUP"
    '                If grdItem_ToLoad.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString = "" Then
    '                    CType(grdItem_ToLoad.Rows(e.RowIndex).Cells(e.ColumnIndex), CalendarCell).Value = "11/11/2000"
    '                Else
    '                    If CType(grdItem_ToLoad.Rows(e.RowIndex).Cells(e.ColumnIndex), CalendarCell).Value = Date.MinValue Then
    '                        CType(grdItem_ToLoad.Rows(e.RowIndex).Cells(e.ColumnIndex), CalendarCell).Value = "11/11/2000"
    '                    End If
    '                End If

    '                If _IsUSE_TRANSPORT_DATETIME = True Then
    '                    Dim dgvcob As New DataGridViewCalendarColumn
    '                    CType(grdItem_ToLoad.Rows(e.RowIndex).Cells(e.ColumnIndex), CalendarCell).My_Format = "dd/MM/yyyy HH:mm:ss"
    '                End If

    '        End Select

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub grdItem_ToLoad_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem_ToLoad.CellEndEdit
        Try
            With Me.grdItem_ToLoad
                Select Case .Columns(e.ColumnIndex).Name.ToUpper
                    Case "COL_EXPECTED_DELIVERY_DATE", "COL_TIME_EXPECTEDDOCPICKUP"
                        Dim strDate As String = .Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString.Replace("/", "").Trim
                        If (strDate <> "") And _
                            (strDate <> ":") And _
                            (strDate <> "::") Then
                            If Not IsDate(.Rows(e.RowIndex).Cells(e.ColumnIndex).Value.ToString) Then
                                W_MSG_Information("กรุณาตรวจสอบ : " & .Columns(e.ColumnIndex).HeaderText)
                                '.Item(e.ColumnIndex, e.RowIndex).Value = ""
                                .CurrentCell = .Rows(e.RowIndex).Cells(e.ColumnIndex)
                                .BeginEdit(True)
                            End If
                        End If
                End Select
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private _CountRow As Integer = 0
    Private _iNewRow As Integer = 0

    Private Sub txtSaleOrder_NoSeach_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSaleOrder_NoSeach.KeyDown

        If Not (e.KeyCode = Keys.Enter) Or txtSaleOrder_NoSeach.Text.Trim = "" Then Exit Sub
        Dim objtb_SalesOrder As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
        Dim odtDTSelect As New DataTable
        Try
            Dim strSearch As String = " and SalesOrder_No like '%" & txtSaleOrder_NoSeach.Text & "%'"
            Select Case Me.cboDocTrip.SelectedIndex
                Case 0
                    strSearch = " and Str1 like '%" & Me.txtSaleOrder_NoSeach.Text.Trim & "%'"
                Case 1
                    strSearch = " and SalesOrder_No like  '%" & Me.txtSaleOrder_NoSeach.Text.Trim & "%'"
            End Select
            Me._selectItem = True
            objtb_SalesOrder.getSO_Totruck(strSearch, _IsSubManifest, cboProcess_Id.SelectedValue.ToString.Trim, _selectItem, txtTop.Text)
            'objtb_SalesOrder.getSO_Totruck(strSearch, _IsSubManifest, "", True)
            odtDTSelect = objtb_SalesOrder.DataTable
            _iNewRow = odtDTSelect.Rows.Count
            grdItem_ToLoad.DataSource = GetRowSelected(odtDTSelect)

            Me.grdItem_ToLoad.Sort(col_Sort, System.ComponentModel.ListSortDirection.Descending)
            'Clere Key So
            'CalculateManifest()

            '  Me.grdItem_ToLoad.Columns("col_SO_No").DefaultCellStyle.BackColor = Color.White

            SetGridRowColor()

            'For i As Integer = (_iNewRow - 1) To 0 Step -1
            '    Me.grdItem_ToLoad.Rows(i).Cells("col_SO_No").Style.BackColor = Color.Yellow
            'Next


            Me.txtSaleOrder_NoSeach.Text = ""
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Function GetRowSelected(ByVal podtData As DataTable) As DataTable
        'Keep Selected Product
        Try
            _iNewRow = podtData.Rows.Count

            Dim odtTemp As New DataTable

            '  If Me.grdItem_ToLoad.DataSource IsNot Nothing Then

            Dim odrTemNoSelected() As DataRow    '--- Array Datarow
            Dim odrDuplicate() As DataRow       '--- Array Datarow

            If grdItem_ToLoad.DataSource IsNot Nothing Then
                CType(grdItem_ToLoad.DataSource, DataTable).AcceptChanges()
                odtTemp = grdItem_ToLoad.DataSource
                odrTemNoSelected = odtTemp.Select("chkSelect=0")
                'Step 1: ลบตัวที่ไม่เลือก
                For Each drNoSelect As DataRow In odrTemNoSelected
                    odtTemp.Rows.Remove(drNoSelect)
                Next
                'Step 1: ตัวที่เลือก
                odrTemNoSelected = odtTemp.Select("chkSelect=1")
                For Each odrSelected As DataRow In odrTemNoSelected
                    odrDuplicate = podtData.Select("SalesOrder_Index = '" & odrSelected("SalesOrder_Index").ToString & "'")
                    If odrDuplicate.Length > 0 Then
                        'ลบตัวเก่าที่ซ้ำจากดาต้ากริด คีย์ใหม่
                        podtData.Rows.Remove(odrDuplicate(0))
                        _CountRow += 1
                        odrSelected("count") = _CountRow
                    End If
                Next

                'Step 3: ตัวที่เหลือจากการลบเป็นตัวใหม่ ให้แสดงบนสุด
                For Each odrSelected As DataRow In podtData.Rows
                    _CountRow += 1
                    odrSelected("count") = _CountRow
                Next


                ''Step 2: ลบตัวที่ซ้ำ
                'For Each odrSelected As DataRow In podtData.Rows
                '    odrDuplicate = odtTemp.Select("SalesOrder_Index = '" & odrSelected("SalesOrder_Index").ToString & "'")
                '    If odrDuplicate.Length > 0 Then
                '        'ลบตัวเก่าที่ซ้ำจากดาต้ากริด คีย์ใหม่
                '        odtTemp.Rows.Remove(odrDuplicate(0))
                '    End If
                '    _CountRow += 1
                '    odrSelected("count") = _CountRow
                'Next
            End If



            'Step 3: รวมกันตัวใหม่  กับ ตัวที่เดิมที่ลบตัวที่ซ้ำกับตัวใหม่และตัวเดิมที่ไม่ได้เลือก 
            odtTemp.Merge(podtData)

            Return odtTemp
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub cboProcess_Id_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProcess_Id.SelectionChangeCommitted
        Try
            _selectItem = False
            LoadDataGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnConfrim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnConfrim_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfrim.Click
        Try
            _selectItem = False
            Select Case objStatus
                'Case Manifest_Mode.ADD
                '    Dim dtTempLoad As New DataTable
                '    dtTempLoad = chkSelect_OnTruck()
                '    If dtTempLoad.Rows.Count = 0 Then
                '        W_MSG_Information_ByIndex(300032)
                '        Exit Sub
                '    End If
                '    Dim frm As New frmTransportLoadDoc
                '    frm.dataTable = dtTempLoad
                '    frm.IsSubManifest = _IsSubManifest
                '    frm.ShowDialog()
                '    If frm.SaveType = 1 Then
                '        Dim odtTemp As DataTable
                '        odtTemp = CType(grdItem_ToLoad.DataSource, DataTable)
                '        For Each odrSelected As DataRow In dtTempLoad.Rows
                '            Dim drArrLoad() As DataRow = odtTemp.Select("SalesOrder_Index='" & odrSelected("SalesOrder_Index") & "'")
                '            odtTemp.Rows.Remove(drArrLoad(0))
                '        Next
                '        odtTemp.AcceptChanges()
                '        LoadDataGrid()
                '    End If

                Case Manifest_Mode.EDIT
                    _DataTable = chkSelect_OnTruck()
                    Me.Close()
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'Private Sub frmTransportBillLoad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    Try
    '        If e.KeyCode = Keys.Escape Then
    '            Me.Close()
    '        End If

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub frmTransportBillLoad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransport
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 24)
                    oFunction.SW_Language_Column(Me, Me.grdItem_ToLoad, 24)
                    oFunction.SW_Language_Column(Me, Me.grdItem_OnTruck, 24)
                    oFunction.SW_Language_Column(Me, Me.grdManifest_Summary, 24)
                    oFunction = Nothing
                End If
            End If

            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdItem_ToLoad_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdItem_ToLoad.CellContentClick

    End Sub

    Private Sub frmTransportBillLoad_Update_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            Me.SetGridRowColor()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdoRoute_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            closeAllCheckbox(sender)
            btnPop_Search.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class
