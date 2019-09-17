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

Public Class frmTransportManifest_Update

    Private _TransportJobType_Spacial_Case As String = "0010000000000"
    Private _TransportManifest_Index As String = ""
    Private _Status As Integer = 0

    Private _BoolLoad As Boolean = True

    Public ReadOnly Property Status() As Integer
        Get
            Return _Status
        End Get
    End Property

    Private _dataTable As New DataTable

 
    Public Property dataTable() As DataTable
        Get
            Return _dataTable
        End Get
        Set(ByVal value As DataTable)
            _dataTable = value
        End Set
    End Property
    Public Property TransportManifest_Index() As String
        Get
            Return _TransportManifest_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifest_Index = Value
        End Set
    End Property


    Private objStatus As Manifest_Mode

    Public Enum Manifest_Mode
        ADD
        EDIT
        COPY
        NULL
    End Enum

    Public Sub New(ByVal Operation_Type As Manifest_Mode)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub

    Private _IsSubManifest As Integer = 0
    Public Property IsSubManifest() As Integer
        Get
            Return _IsSubManifest
        End Get
        Set(ByVal value As Integer)
            _IsSubManifest = value
        End Set
    End Property

#Region "   1  FROM LOAD"
    Private Sub frmTransportManifest_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ========== Manage Language Functions Begin ==========
            Dim oFunction As New WMS_STD_Master.W_Language
            'oFunction.Insert(Me)
            oFunction.SwitchLanguage(Me, 24)
            oFunction.SW_Language_Column(Me, Me.grdTrasportManifestItem, 24)
            oFunction.SW_Language_Column(Me, Me.grdCustomer_Shipping, 24)
            oFunction.SW_Language_Column(Me, Me.grdTransportCharge, 24)
            oFunction.SW_Language_Column(Me, Me.grdTransportPayment, 24)
            oFunction.SW_Language_Column(Me, Me.grdCarrier, 24)

            oFunction = Nothing


            If Me.txtTransportManifest_No.Text = "" Then
                'Me.btnPrintBarcode.Enabled = False
            End If

            'Me.getComboRegion()
            Me.grdTrasportManifestItem.AutoGenerateColumns = False
            Me.grdCustomer_Shipping.AutoGenerateColumns = False
            Me.grdTransportCharge.AutoGenerateColumns = False
            Me.grdTransportPayment.AutoGenerateColumns = False
            Me.grdCarrier.AutoGenerateColumns = False
            Me.AddCurrencyTextBox()
            Me.config_Transport()
            Me.getComboBox()
            '-----------------------------------------
            ''ค่าขนส่งขาจ่าย
            'Me.getTransportPayment()
            'Me.CallculateTransportPayment()
            ''ค่าขนส่งขารับ
            'Me.getTransportCharge()
            'Me.CallculateTransportChage()

            '-----------------------------------------
            'ค่าขนส่งขาจ่าย
            Me.getTransportPayment_Data()
            'ค่าขนส่งขารับ
            Me.getTransportCharge_Data()

            '-----------------------------------------
            'Load DataSource Header Detail
            Me.getTransportManifest_Header()
            Me.getTransportManifest_Item()




            Select Case objStatus
                Case Manifest_Mode.ADD

                    'Dong Edit : Use Temp For Running Seq Index
                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    Me._TransportManifest_Index = objDBTempIndex.getSys_Value("TransportManifest_Index")
                    objDBTempIndex = Nothing

                    If Me._dataTable.Rows.Count > 0 Then
                        'STEP 1 : Load Header
                        Me.txtSumWeight.Text = Format(CDbl(_dataTable.Compute("SUM(Weight)", "Weight >= 0")), "#,##0.0000")
                        Me.txtSumVolume.Text = Format(CDbl(_dataTable.Compute("SUM(Volume)", "Volume >= 0")), "#,##0.0000")
                        Me.txtSumDoc.Text = Format(CDbl(_dataTable.Compute("count(SalesOrder_Index)", "SalesOrder_Index <> ''")), "#,##0")
                        If _dataTable.Rows(0).Item("Route_Index").ToString <> "" Then
                            Me.cboRoute.SelectedValue = _dataTable.Rows(0).Item("Route_Index").ToString
                        End If
                        If _dataTable.Rows(0).Item("SubRoute_Index").ToString <> "" Then
                            Me.cboSubRoute.SelectedValue = _dataTable.Rows(0).Item("SubRoute_Index").ToString
                        End If

                        If _dataTable.Rows(0).Item("DistributionCenter_Index").ToString <> "" Then
                            Me.cboDistributionCenter.SelectedValue = _dataTable.Rows(0).Item("DistributionCenter_Index").ToString
                        End If
                        'Defualt Customer
                        'ลูกค้า
                        Me.txtCustomer_Id.Tag = _dataTable.Rows(0).Item("Customer_Index").ToString
                        Me.txtCustomer_Id.Text = _dataTable.Rows(0).Item("Customer_Id").ToString
                        Me.txtCustomer_Name.Text = _dataTable.Rows(0).Item("Customer_Name").ToString
                        'ผู้รับ
                        Me.txtConsignee_Id.Tag = _dataTable.Rows(0).Item("Customer_Shipping_Index").ToString
                        Me.txtConsignee_Id.Text = _dataTable.Rows(0).Item("Customer_Shipping_Id").ToString
                        Me.txtConsignee_Name.Text = _dataTable.Rows(0).Item("Company_Name").ToString
                        'สถานที่จัดส่ง
                        Me.txtShipping_Location_ID.Tag = _dataTable.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                        Me.txtShipping_Location_ID.Text = _dataTable.Rows(0).Item("Customer_Shipping_Location_Id").ToString
                        Me.txtShipping_Location_Name.Text = _dataTable.Rows(0).Item("Shipping_Location_Name").ToString
                        'ผู้ส่ง
                        Me.txtCarrier_ID.Tag = _dataTable.Rows(0).Item("Carrier_Index").ToString
                        Me.txtCarrier_ID.Text = _dataTable.Rows(0).Item("Carrier_Id").ToString
                        Me.txtCarrier_Name.Text = _dataTable.Rows(0).Item("CarrierDES").ToString
                        If _dataTable.Rows(0).Item("worker").ToString <> "" Then
                            txtWorker.Text = _dataTable.Rows(0).Item("worker").ToString
                        Else
                            txtWorker.Text = "0"
                        End If
                        Me.txtTrasport_To.Text = Me.txtShipping_Location_Name.Text

                        chkMultidrop.Checked = True
                        Dim drGetDocGr() As DataRow = _dataTable.Select("Process_Id=27")
                        If drGetDocGr.Length > 0 Then
                            chkPickupReturnItem.Checked = True
                        End If
                        'STEP 1 : Load Detail
                        setDataSoucreManifestItem(Me._dataTable)
                        CType(grdTrasportManifestItem.DataSource, DataTable).AcceptChanges()
                        setRowColorAndCalcurate()
                        Me._Status = 0 'New
                        'Reload check box Packing 
                        Me.cboTransportJobType_SelectedIndexChanged(sender, e)
                    End If

                    Me.ManageButtom()

                Case Manifest_Mode.COPY
                    Dim strTransportManifest_No As String = Me.txtTransportManifest_No.Text
                    Dim strTemp As String = ""
                    If strTransportManifest_No.Contains("/") Then
                        Dim strSplit() As String = strTransportManifest_No.Split("/")

                        If strSplit.Length > 0 Then
                            strTemp = CInt(strSplit(1)) + 1
                        End If


                        Me.txtTransportManifest_No.Text = strSplit(0) & "/" & strTemp

                    Else
                        Me.txtTransportManifest_No.Text &= "/1"
                    End If




                    Me.txtVehicleID.Text = "" ' drManifest("Vehicle_Id").ToString
                    Me.txtVehicleID.Tag = "" 'drManifest("Vehicle_Index").ToString
                    Me.txtVehicleNo.Text = "" 'drManifest("Vehicle_License_No").ToString

                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    Me._TransportManifest_Index = objDBTempIndex.getSys_Value("TransportManifest_Index")
                    objDBTempIndex = Nothing
                    Me._Status = 0 'New
                    Me.ManageButtom()
                Case Manifest_Mode.EDIT
                    Me.ManageButtom()

            End Select

            Me.getReportName(23)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            _BoolLoad = False
        End Try
    End Sub


    Private Sub ManageButtom()
        Try
            '1:     รอจัดรถ
            '2:     Load(สินค้าแล้ว)
            '3	    รอ Load สินค้า,  กำลังจัดส่ง (LP)
            '4:     กำลังจัดส่ง
            '5:     เสร็จสิ้น
            '6:     รอปล่อยรถ
            '7	    รอส่งใหม่/ค้างส่ง
            '11	    ถึงผู้รับ/ถึงศูนย์กระจาย
            '12:    กระจายสินค้า
            '13:    รอคืนบิล
            '14:    คืนบิล(DC)
            '15:    คืนบิลต้นทาง

            'Btn Item
            Me.btnAddManifestItem.Enabled = False
            Me.btnDelManifestItem.Enabled = False
            Me.btnMoveManifestItem.Enabled = False
            Me.btnViewManifestItem.Enabled = False

            'Btn Process
            Me.btnSave.Enabled = False
            Me.btnEdit.Enabled = False
            Me.btnCancel.Enabled = False
            'Me.btnWithDraw.Enabled = False
            Me.btnPrint.Enabled = False


            If chkTransportCharged.Checked = True Then
                txtSumPrice_In.Enabled = True
            Else
                txtSumPrice_In.Enabled = False
            End If

            If chkTransportPaid.Checked = True Then
                txtSumPrice_Out.Enabled = True
            Else
                txtSumPrice_Out.Enabled = False
            End If


            Select Case Me._Status.ToString
                Case "0" 'New manifest
                    'Btn Item
                    Me.btnTransportCharge.Enabled = True
                    Me.btnClearTransportCharge.Enabled = False

                    Me.btnAddManifestItem.Enabled = True
                    Me.btnDelManifestItem.Enabled = True
                    Me.btnViewManifestItem.Enabled = True
                    'Btn Process
                    Me.btnSave.Enabled = True

                    'Me.cboTransportJobType.Enabled = True
                Case "5", "13"
                    EnableControl(False, True)
                    Me.btnPrint.Enabled = True
                    'Me.btnPrintBarcode = True
                    Me.btnClearTransportCharge.Enabled = False
                Case Else
                    'Btn Process
                    Me.btnTransportCharge.Enabled = False
                    Me.btnClearTransportCharge.Enabled = True

                    Me.btnEdit.Enabled = True
                    Me.btnViewManifestItem.Enabled = True
                    Me.btnCancel.Enabled = True
                    'Me.btnWithDraw.Enabled = True
                    Me.btnPrint.Enabled = True

                    'Me.cboTransportJobType.Enabled = False
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub EnableControl(ByVal EnabledType As Boolean, ByVal ReadOnlyType As Boolean)
        Try
            Dim btn As Button = Nothing
            Dim txt As TextBox = Nothing
            Dim cbo As ComboBox = Nothing
            Dim dtp As DateTimePicker = Nothing
            Dim chk As CheckBox = Nothing
            Dim grb As GroupBox = Nothing
            For Each Ctrl As Control In Me.tbpJobDetailMain.Controls
                If TypeOf Ctrl Is Button Then
                    btn = DirectCast(Ctrl, Button)
                    btn.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is TextBox Then
                    txt = DirectCast(Ctrl, TextBox)
                    txt.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is ComboBox Then
                    cbo = DirectCast(Ctrl, ComboBox)
                    cbo.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is DateTimePicker Then
                    dtp = DirectCast(Ctrl, DateTimePicker)
                    dtp.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is CheckBox Then
                    chk = DirectCast(Ctrl, CheckBox)
                    chk.Enabled = EnabledType
                End If
            Next
            For Each Ctrl As Control In Me.tbpContainerDetail.Controls
                If TypeOf Ctrl Is Button Then
                    btn = DirectCast(Ctrl, Button)
                    btn.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is TextBox Then
                    txt = DirectCast(Ctrl, TextBox)
                    txt.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is ComboBox Then
                    cbo = DirectCast(Ctrl, ComboBox)
                    cbo.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is DateTimePicker Then
                    dtp = DirectCast(Ctrl, DateTimePicker)
                    dtp.Enabled = EnabledType
                End If
            Next
            For Each Ctrl As Control In Me.gbTripMile.Controls
                If TypeOf Ctrl Is Button Then
                    btn = DirectCast(Ctrl, Button)
                    btn.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is TextBox Then
                    txt = DirectCast(Ctrl, TextBox)
                    txt.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is ComboBox Then
                    cbo = DirectCast(Ctrl, ComboBox)
                    cbo.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is DateTimePicker Then
                    dtp = DirectCast(Ctrl, DateTimePicker)
                    dtp.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is CheckBox Then
                    chk = DirectCast(Ctrl, CheckBox)
                    chk.Enabled = EnabledType
                End If
            Next
            For Each Ctrl As Control In Me.gbFuel.Controls
                If TypeOf Ctrl Is Button Then
                    btn = DirectCast(Ctrl, Button)
                    btn.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is TextBox Then
                    txt = DirectCast(Ctrl, TextBox)
                    txt.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is ComboBox Then
                    cbo = DirectCast(Ctrl, ComboBox)
                    cbo.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is DateTimePicker Then
                    dtp = DirectCast(Ctrl, DateTimePicker)
                    dtp.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is CheckBox Then
                    chk = DirectCast(Ctrl, CheckBox)
                    chk.Enabled = EnabledType
                End If
            Next
            For Each Ctrl As Control In Me.gbTaskInfo.Controls
                If TypeOf Ctrl Is Button Then
                    btn = DirectCast(Ctrl, Button)
                    btn.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is TextBox Then
                    txt = DirectCast(Ctrl, TextBox)
                    txt.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is ComboBox Then
                    cbo = DirectCast(Ctrl, ComboBox)
                    cbo.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is DateTimePicker Then
                    dtp = DirectCast(Ctrl, DateTimePicker)
                    dtp.Enabled = EnabledType
                End If
                If TypeOf Ctrl Is CheckBox Then
                    chk = DirectCast(Ctrl, CheckBox)
                    chk.Enabled = EnabledType
                End If
            Next
            grdTrasportManifestItem.ReadOnly = True
            grdTransportCharge.ReadOnly = True
            grdTransportPayment.ReadOnly = True
            grdCustomer_Shipping.ReadOnly = True
            grdCarrier.ReadOnly = True

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function ConfrimAdminPassword() As Boolean
        Try
            Select Case Me._Status.ToString
                Case "0" 'New manifest
                Case "1", "2", "3", "6", "7"
                Case Else
                    'Confirm Pass Admin
                    Dim frmpassword As New WMS_STD_Master.PopupEnterPassword
                    'frmpassword.Group = "0010000000001"
                    frmpassword.ShowDialog()
                    If frmpassword.passwordistrue = False Then
                        Return False
                    End If
            End Select

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try

            If Me.ConfrimAdminPassword() = False Then Exit Sub
            'Me.cboTransportJobType.Enabled = True
            Me.btnAddManifestItem.Enabled = True
            Me.btnDelManifestItem.Enabled = True
            Me.btnMoveManifestItem.Enabled = True
            Me.btnViewManifestItem.Enabled = True
            'Btn Process
            Me.btnEdit.Enabled = False
            Me.btnSave.Enabled = True
            SetRow_Number_grdTrasportManifestItem()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            'If Not Check_SaleOrders_Status() Then
            '    W_MSG_Information("ใบคุมนี้ไม่สามารถแก้ไข หรือยกเลิกได้ ")
            '    Exit Sub
            'End If

            If W_MSG_Confirm_ByIndex(100004) = Windows.Forms.DialogResult.No Then Exit Sub
            If Me.ConfrimAdminPassword() = False Then Exit Sub
            Dim objDelectManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.DELETE)
            objDelectManifest.TransportManifest_Index = Me._TransportManifest_Index
            objDelectManifest.Delete(ManifestTransaction_Update.enuDelete.DELETEALL)
            Me.btnSave.Enabled = False

            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub AddCurrencyTextBox()
        Try
            AddHandler Me.txtSumPrice_In.KeyPress, AddressOf Me.keypressed
            AddHandler Me.txtSumPrice_Out.KeyPress, AddressOf Me.keypressed
            AddHandler Me.txtDriverPaidAmount.KeyPress, AddressOf Me.keypressed
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
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
                    Select Case Trim(objDr("Field_Name")).ToString.ToUpper
                        Case "USE_TRANSPORT_DATETIME"
                            Me.col_SO_Date.DefaultCellStyle.Format = "d"
                            Me.col_Expected_Delivery_Date.DefaultCellStyle.Format = "d"
                            Me.col_SO_Date.Width = 95
                            Me.col_Expected_Delivery_Date.Width = 95

                        Case "TABMANIFEST_TIME1"
                            Me.tbcTransportManifestItem.TabPages.Remove(Me.tbpTripTime)
                        Case "TABMANIFEST_TIME2"
                            Me.tbcTransportManifestItem.TabPages.Remove(Me.tbpTripTime2)
                        Case "TABCONTAINERDETAIL"
                            Me.tbcTransportManifestHd.TabPages.Remove(Me.tbpContainerDetail)
                        Case "TABMILEANDGAS"
                            Me.tbcTransportManifestHd.TabPages.Remove(Me.tbpMileAndGas)
                        Case "USE_CUSTOMER_SHIPPING_LOCATION"
                            col_CustomerShippingLocation.Visible = False
                        Case "USE_VOLUME_TRANSPORT"
                            lblSumVolume.Visible = False
                            txtSumVolume.Visible = False
                            col_Volume_Shipped.Visible = False
                            txtVolume_Limit.Visible = False
                            txtVolume_Percent.Visible = False
                        Case "USE_WEIGHT_TRANSPORT"
                            lblSumWt.Visible = False
                            txtSumWeight.Visible = False
                            col_Weight_Shipped.Visible = False
                            txtWeight_Limit.Visible = False
                            txtWeight_Percent.Visible = False
                        Case "TABTRANSPORTCHARGE"
                            Me.tbcTransportManifestItem.TabPages.Remove(Me.tbpTripCharge)
                            Me.tbcTransportManifestItem.TabPages.Remove(Me.tbpDropandRegion)
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


    Private Sub getTransportManifest_Header()
        Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
        Try
            '**** Load Default setting ****

            Dim dtManifest As New DataTable
            Dim strWhere As String = " AND TransportManifest_Index='" & Me._TransportManifest_Index & "'"
            objtbManifest.getTransportManifest_Summary(strWhere, "", _IsSubManifest)
            dtManifest = objtbManifest.DataTable

            For Each drManifest As DataRow In dtManifest.Rows
                '*** รายละเอียดรถยนต์ ***
                Me.chkIsPack.Checked = drManifest("IsPack") 'IIf(drManifest("IsPack").ToString = "", False, True)
                txtTransportManifest_No.Text = drManifest("TransportManifest_No").ToString
                txtVehicleID.Text = drManifest("Vehicle_Id").ToString
                txtVehicleID.Tag = drManifest("Vehicle_Index").ToString
                txtVehicleNo.Text = drManifest("Vehicle_License_No").ToString

                cboDriver.SelectedValue = drManifest("Driver_Index").ToString
                cboTruckStaff.SelectedValue = drManifest("TruckStaff_Index").ToString
                cboTripSeq.Text = drManifest("Trip_Sequence").ToString
                txtRefNo1.Text = drManifest("Str1").ToString
                dtpTransportManifest_Date.Value = CDate(drManifest("TransportManifest_Date").ToString)
                '       txtVehicleType.Text = drManifest("VehicleType").ToString
                cboVehicleType.SelectedValue = drManifest("VehicleType_Index").ToString
                txtlblTailLicense_No.Text = drManifest("Tail_License_No").ToString
                cboRoute.SelectedValue = drManifest("Route_Index").ToString
                cboSubRoute.SelectedValue = drManifest("SubRoute_Index").ToString
                If drManifest("DistributionCenter_Index").ToString = "" Then
                    cboDistributionCenter.SelectedValue = "-11"
                Else
                    cboDistributionCenter.SelectedValue = drManifest("DistributionCenter_Index").ToString
                End If

                txtStatus.Text = drManifest("Status").ToString

                cboTransportJobType.SelectedValue = drManifest("TransportJobType_Index").ToString
                chkMultidrop.Checked = drManifest("IsMultiDrop").ToString
                chkPickupReturnItem.Checked = drManifest("Has_PickupReturnItem").ToString
                chkContainerHaulage.Checked = drManifest("IsContainerHaulage").ToString
                chkTransportToDC.Checked = drManifest("Has_Backhaul").ToString
                txtTrip_Description.Text = drManifest("Trip_Description").ToString

                '*** รายละเอียดรถตู้ ***
                txtContainer_No1.Text = drManifest("Container_No1").ToString
                cboContainer_Size1.SelectedValue = drManifest("Container_Size_Index1").ToString
                txtSeal_No1.Text = drManifest("ContainerSeal_No1").ToString
                txtContainer_No2.Text = drManifest("Container_No2").ToString
                cboContainer_Size2.SelectedValue = drManifest("Container_Size_Index2").ToString
                txtSeal_No2.Text = drManifest("ContainerSeal_No2").ToString
                txtComment.Text = drManifest("Comment").ToString


                'ลูกค้า
                Me.txtCustomer_Id.Tag = drManifest("Customer_Index").ToString
                Me.txtCustomer_Id.Text = drManifest("Customer_Id").ToString
                Me.txtCustomer_Name.Text = drManifest("Customer_Name").ToString

                'ผู้รับ
                Me.txtConsignee_Id.Tag = drManifest("Customer_Shipping_Index").ToString
                Me.txtConsignee_Id.Text = drManifest("Customer_Shipping_Id").ToString
                Me.txtConsignee_Name.Text = drManifest("Company_Name").ToString
                'สถานที่จัดส่ง
                Me.txtShipping_Location_ID.Tag = drManifest("Customer_Shipping_Location_Index").ToString
                Me.txtShipping_Location_ID.Text = drManifest("Customer_Shipping_Location_Id").ToString
                Me.txtShipping_Location_Name.Text = drManifest("Shipping_Location_Name").ToString
                'ผู้ส่ง
                Me.txtCarrier_ID.Tag = drManifest("Carrier_Index").ToString
                Me.txtCarrier_ID.Text = drManifest("Carrier_Id").ToString
                Me.txtCarrier_Name.Text = drManifest("CarrierDES").ToString

                If drManifest("worker").ToString <> "" Then
                    txtWorker.Text = drManifest("worker").ToString
                Else
                    txtWorker.Text = "0"
                End If

                txtTransport_From.Text = drManifest("Transport_From").ToString
                txtTrasport_To.Text = drManifest("Transport_To").ToString


                txtTruck_No.Text = drManifest("Booking_No").ToString
                cboHandlingType.SelectedValue = drManifest("HandlingType_Index").ToString

                '*** รายละเอียดระยะทาง/เชื้อเพลิง ***
                txtMile_AtSource.Text = drManifest("Mile_AtSource").ToString
                txtMile_AtDestination.Text = drManifest("Mile_AtDestination").ToString
                txtMile_Return.Text = drManifest("Mile_Return").ToString
                txtSumDistance.Text = Val(drManifest("Mile_AtSource").ToString) + Val(drManifest("Mile_AtDestination").ToString) + Val(drManifest("Mile_Return").ToString)
                txtMile_OutToPickup.Text = drManifest("Mile_OutToPickup").ToString

                chkTrip_Petro_Volume.Checked = False
                txtTrip_Petro_Volume.Text = drManifest("Trip_Petro_Volume").ToString
                If Val(drManifest("Trip_Petro_Volume").ToString) > 0 Then
                    chkTrip_Petro_Volume.Checked = True
                End If
                txtTrip_Gas_Volume.Text = drManifest("Trip_Gas_Volume").ToString
                chkTrip_Gas_Volume.Checked = False
                If Val(drManifest("Trip_Gas_Volume").ToString) > 0 Then
                    chkTrip_Petro_Volume.Checked = True
                End If

                txtTrip_Petro_UnitPrice.Text = drManifest("Trip_Petro_UnitPrice").ToString
                txtTrip_Petro_Amount.Text = drManifest("Trip_Petro_Amount").ToString
                txtTrip_Petro_Mile.Text = drManifest("Trip_Petro_Mile").ToString
                txtTrip_Gas_UnitPrice.Text = drManifest("Trip_Gas_UnitPrice").ToString
                txtTrip_Gas_Amount.Text = drManifest("Trip_Gas_Amount").ToString
                txtTrip_Gas_Mile.Text = drManifest("Trip_Gas_Mile").ToString
                If drManifest("Trip_Gas_Mile").ToString <> "" Then Me.txtSumPrice_In.Text = FormatNumber(drManifest("TotalTransportCharged"), 2)
                If drManifest("TotalTransportPaid").ToString <> "" Then Me.txtSumPrice_Out.Text = FormatNumber(drManifest("TotalTransportPaid"), 2)
                If drManifest("DriverPaidAmount").ToString <> "" Then Me.txtDriverPaidAmount.Text = FormatNumber(drManifest("DriverPaidAmount"), 2)
                If drManifest("isTransportCharged").ToString <> "" Then Me.chkTransportCharged.Checked = drManifest("isTransportCharged").ToString
                If drManifest("isTransportPaid").ToString <> "" Then Me.chkTransportPaid.Checked = drManifest("isTransportPaid").ToString
                If drManifest("isSpecialCase").ToString <> "" Then Me.chkSpecialCase.Checked = drManifest("isSpecialCase").ToString
                Me._Status = drManifest("Status_id")


                ''    *** บันทึกเวลาการจัดส่ง *** Tab tbpTripTime
                'เวลารถที่ต้นทาง
                'chkSourceSameDate.Checked = False
                'chkTime_SourceInGate.Checked = False

                If Me.tbcTransportManifestItem.Contains(tbpTripTime) Then
                    If drManifest("Time_SourceInGate").ToString = "" Then
                        dtTime_SourceInGate.Value = Now
                        tTime_SourceInGate.Value = Now
                    Else
                        chkTime_SourceInGate.Checked = True
                        dtTime_SourceInGate.Value = CDate(drManifest("Time_SourceInGate").ToString)
                        tTime_SourceInGate.Value = CDate(drManifest("Time_SourceInGate").ToString)
                    End If


                    'chkTime_SourceLoadStart.Checked = False
                    If drManifest("Time_SourceLoadStart").ToString = "" Then
                        dtTime_SourceLoadStart.Value = Now
                        tTime_SourceLoadStart.Value = Now
                    Else
                        chkTime_SourceLoadStart.Checked = True
                        dtTime_SourceLoadStart.Value = CDate(drManifest("Time_SourceLoadStart").ToString)
                        tTime_SourceLoadStart.Value = CDate(drManifest("Time_SourceLoadStart").ToString)
                    End If


                    'chkTime_SourceLoadFinish.Checked = False
                    If drManifest("Time_SourceLoadFinish").ToString = "" Then
                        dtTime_SourceLoadFinish.Value = Now
                        tTime_SourceLoadFinish.Value = Now
                    Else
                        chkTime_SourceLoadFinish.Checked = True
                        dtTime_SourceLoadFinish.Value = CDate(drManifest("Time_SourceLoadFinish").ToString)
                        tTime_SourceLoadFinish.Value = CDate(drManifest("Time_SourceLoadFinish").ToString)
                    End If


                    'chkTime_SourceOutGate.Checked = False
                    If drManifest("Time_SourceOutGate").ToString = "" Then
                        dtTime_SourceOutGatet.Value = Now
                        tTime_SourceOutGate.Value = Now
                    Else
                        chkTime_SourceOutGate.Checked = True
                        dtTime_SourceOutGatet.Value = CDate(drManifest("Time_SourceOutGate").ToString)
                        tTime_SourceOutGate.Value = CDate(drManifest("Time_SourceOutGate").ToString)
                    End If


                    'เวลารถถึงปลายทาง
                    'chkDestinationSameDate.Checked = False
                    'chkTime_DestinationInGate.Checked = False
                    If drManifest("Time_DestinationInGate").ToString = "" Then
                        dtTime_DestinationInGate.Value = Now
                        tTime_DestinationInGate.Value = Now
                    Else
                        chkTime_DestinationInGate.Checked = True
                        dtTime_DestinationInGate.Value = CDate(drManifest("Time_DestinationInGate").ToString)
                        tTime_DestinationInGate.Value = CDate(drManifest("Time_DestinationInGate").ToString)
                    End If


                    'chkTime_DestinationUnloadStart.Checked = False
                    If drManifest("Time_DestinationUnloadStart").ToString = "" Then
                        dtTime_DestinationUnloadStart.Value = Now
                        tTime_DestinationUnloadStart.Value = Now
                    Else
                        chkTime_DestinationUnloadStart.Checked = True
                        dtTime_DestinationUnloadStart.Value = CDate(drManifest("Time_DestinationUnloadStart").ToString)
                        tTime_DestinationUnloadStart.Value = CDate(drManifest("Time_DestinationUnloadStart").ToString)
                    End If


                    'chkTime_DestinationUnloadFinish.Checked = False
                    If drManifest("Time_DestinationUnloadFinish").ToString = "" Then
                        dtTime_DestinationUnloadFinish.Value = Now
                        tTime_DestinationUnloadFinish.Value = Now
                    Else
                        chkTime_DestinationUnloadFinish.Checked = True
                        dtTime_DestinationUnloadFinish.Value = CDate(drManifest("Time_DestinationUnloadFinish").ToString)
                        tTime_DestinationUnloadFinish.Value = CDate(drManifest("Time_DestinationUnloadFinish").ToString)
                    End If


                    'chkTime_DestinationOutGate.Checked = False
                    If drManifest("Time_DestinationOutGate").ToString = "" Then
                        dtTime_DestinationOutGate.Value = Now
                        tTime_DestinationOutGate.Value = Now
                    Else
                        chkTime_DestinationOutGate.Checked = True
                        dtTime_DestinationOutGate.Value = CDate(drManifest("Time_DestinationOutGate").ToString)
                        tTime_DestinationOutGate.Value = CDate(drManifest("Time_DestinationOutGate").ToString)
                    End If


                    'เวลารถกลับต้นทาง
                    'chkReturnSameDate.Checked = False
                    'chkTime_ReturnTruckInGate.Checked = False
                    If drManifest("Time_ReturnTruckInGate").ToString = "" Then
                        dtTime_ReturnTruckInGate.Value = Now
                        tTime_ReturnTruckInGate.Value = Now
                    Else
                        chkTime_ReturnTruckInGate.Checked = True
                        dtTime_ReturnTruckInGate.Value = CDate(drManifest("Time_ReturnTruckInGate").ToString)
                        tTime_ReturnTruckInGate.Value = CDate(drManifest("Time_ReturnTruckInGate").ToString)
                    End If

                    'chkTime_ReturnTruckUnloadStart.Checked = False

                    If drManifest("Time_ReturnTruckUnloadStart").ToString = "" Then
                        dtTime_ReturnTruckUnloadStart.Value = Now
                        tTime_ReturnTruckUnloadStart.Value = Now
                    Else
                        chkTime_ReturnTruckUnloadStart.Checked = True
                        dtTime_ReturnTruckUnloadStart.Value = CDate(drManifest("Time_ReturnTruckUnloadStart").ToString)
                        tTime_ReturnTruckUnloadStart.Value = CDate(drManifest("Time_ReturnTruckUnloadStart").ToString)
                    End If


                    'chkTime_ReturnTruckUnloadFinish.Checked = False
                    If drManifest("Time_ReturnTruckUnloadFinish").ToString = "" Then
                        dtTime_ReturnTruckUnloadFinish.Value = Now
                        tTime_ReturnTruckUnloadFinish.Value = Now
                    Else
                        chkTime_ReturnTruckUnloadFinish.Checked = True
                        dtTime_ReturnTruckUnloadFinish.Value = CDate(drManifest("Time_ReturnTruckUnloadFinish").ToString)
                        tTime_ReturnTruckUnloadFinish.Value = CDate(drManifest("Time_ReturnTruckUnloadFinish").ToString)
                    End If



                    'chkTime_ReturnTruckOutGate.Checked = False
                    If drManifest("Time_ReturnTruckOutGate").ToString = "" Then
                        dtTime_ReturnTruckOutGate.Value = Now
                        tTime_ReturnTruckOutGate.Value = Now
                    Else
                        chkTime_ReturnTruckOutGate.Checked = True
                        dtTime_ReturnTruckOutGate.Value = CDate(drManifest("Time_ReturnTruckOutGate").ToString)
                        tTime_ReturnTruckOutGate.Value = CDate(drManifest("Time_ReturnTruckOutGate").ToString)
                    End If


                End If

                If Me.tbcTransportManifestItem.Contains(tbpTripTime2) Then
                    If drManifest("Time_SourceOutGate").ToString = "" Then
                        dtTime_SourceOutGatet2.Value = Now
                        tTime_SourceOutGate2.Value = Now
                    Else
                        chkTime_SourceOutGate2.Checked = True
                        dtTime_SourceOutGatet2.Value = CDate(drManifest("Time_SourceOutGate").ToString)
                        tTime_SourceOutGate2.Value = CDate(drManifest("Time_SourceOutGate").ToString)
                    End If

                    If drManifest("Time_DestinationInGate").ToString = "" Then
                        dtTime_DestinationInGate2.Value = Now
                        tTime_DestinationInGate2.Value = Now
                    Else
                        chkTime_DestinationInGate2.Checked = True
                        dtTime_DestinationInGate2.Value = CDate(drManifest("Time_DestinationInGate").ToString)
                        tTime_DestinationInGate2.Value = CDate(drManifest("Time_DestinationInGate").ToString)
                    End If

                    If drManifest("Time_ReturnTruckInGate").ToString = "" Then
                        dtTime_ReturnTruckInGate2.Value = Now
                        tTime_ReturnTruckInGate2.Value = Now
                    Else
                        chkTime_ReturnTruckInGate2.Checked = True
                        dtTime_ReturnTruckInGate2.Value = CDate(drManifest("Time_ReturnTruckInGate").ToString)
                        tTime_ReturnTruckInGate2.Value = CDate(drManifest("Time_ReturnTruckInGate").ToString)
                    End If



                End If



            Next


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getTransportManifest_Item()
        Dim objtbManifest As New tb_TransportManifest_Update
        Try
            objtbManifest.getTransportManifest_Detail(Me._TransportManifest_Index, _IsSubManifest)
            Me.grdTrasportManifestItem.DataSource = objtbManifest.DataTable
            setRowColorAndCalcurate()
            SetRow_Number_grdTrasportManifestItem()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub getTransportCharge_Data()
        Dim objtbManifest As New svar_TransportManifestCharge
        Try
            objtbManifest.getTransportManifestCharge(1, Me._TransportManifest_Index)
            Me.grdTransportCharge.DataSource = objtbManifest.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getTransportPayment_Data()
        Dim objtbManifest As New svar_TransportManifestCharge
        Try
            objtbManifest.getTransportManifestCharge(2, Me._TransportManifest_Index)
            Me.grdTransportPayment.DataSource = objtbManifest.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




#End Region

#Region "   2   GET COMBOBOX"
    Private Sub getComboBox()
        Try
            cboTripSeq.SelectedIndex = 0
            Me.getDocumentType(24)
            Me.getComboRoute()
            Me.getComboDistributionCenter()
            Me.getComboTransportJobType()
            Me.getComboContainer1()
            Me.getComboContainer2()
            Me.getComboDriver()
            Me.getComboSubDriver()
            Me.getComBoHandlingType()
            Me.getComBoVihecleType()
            'Fix Code SendOrPickup
            Me.getisSendOrPickup()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub getisSendOrPickup()

        Try
            Dim objDT As DataTable = New DataTable
            objDT.Columns.Add("Description", GetType(String))
            objDT.Columns.Add("Column_Index", GetType(Integer))
            Dim drNew As DataRow
            '--------------------------------- tab 1 ----------------------------------
            '"ส่ง"
            drNew = objDT.NewRow
            drNew("Description") = "ส่ง"
            drNew("Column_Index") = 1
            objDT.Rows.Add(drNew)
            '"รับ"
            drNew = objDT.NewRow
            drNew("Description") = "รับ"
            drNew("Column_Index") = 2
            objDT.Rows.Add(drNew)

            With col_SendOrPickup
                .DisplayMember = "Description"
                .ValueMember = "Column_Index"
                .DataSource = objDT
            End With
            '--------------------------------- tab 3 ----------------------------------
            Dim objDT2 As New DataTable
            objDT2 = objDT.Clone
            '"ส่ง"
            drNew = objDT2.NewRow
            drNew("Description") = "ส่ง"
            drNew("Column_Index") = 1
            objDT2.Rows.Add(drNew)
            '"รับ"
            drNew = objDT2.NewRow
            drNew("Description") = "รับ"
            drNew("Column_Index") = 2
            objDT2.Rows.Add(drNew)

            With col_SendPickup
                .DisplayMember = "Description"
                .ValueMember = "Column_Index"
                .DataSource = objDT2
            End With
            '--------------------------------- tab 3 ----------------------------------
            Dim objDT3 As New DataTable
            objDT3 = objDT.Clone
            '"ส่ง"
            drNew = objDT3.NewRow
            drNew("Description") = "ส่ง"
            drNew("Column_Index") = 1
            objDT3.Rows.Add(drNew)
            '"รับ"
            drNew = objDT3.NewRow
            drNew("Description") = "รับ"
            drNew("Column_Index") = 2
            objDT3.Rows.Add(drNew)

            With col_IsSendOrPickup2
                .DisplayMember = "Description"
                .ValueMember = "Column_Index"
                .DataSource = objDT3
            End With
        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getComBoVihecleType()
        Dim objClassDB As New ms_VehicleType(ms_VehicleType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboVehicleType.BeginUpdate()
            With cboVehicleType
                .DisplayMember = "Description"
                .ValueMember = "VehicleType_Index"
                .DataSource = objDT
            End With

            cboVehicleType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComBoHandlingType()
        Dim objClassDB As New tb_HandlingType(tb_HandlingType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            cboHandlingType.BeginUpdate()
            With cboHandlingType
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDT
            End With

            cboHandlingType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboDriver()
        Dim objClassDB As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getTransportManifest_Driver(Me._TransportManifest_Index, 0)
            objDT = objClassDB.DataTable

            cboDriver.BeginUpdate()
            With cboDriver
                .DisplayMember = "Driver_Name"
                .ValueMember = "Driver_Index"
                .DataSource = objDT
            End With

            cboDriver.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboSubDriver()
        Dim objClassDB As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getTransportManifest_Driver(Me._TransportManifest_Index, 1)
            objDT = objClassDB.DataTable

            cboTruckStaff.BeginUpdate()
            With cboTruckStaff
                .DisplayMember = "Driver_Name"
                .ValueMember = "Driver_Index"
                .DataSource = objDT
            End With

            cboTruckStaff.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboContainer2()
        Dim objClassDB As New ms_Container(ms_Container.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            cboContainer_Size2.BeginUpdate()
            With cboContainer_Size2
                .DisplayMember = "Container_No"
                .ValueMember = "Container_Index"
                .DataSource = objDT
            End With

            cboContainer_Size2.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getComboContainer1()
        Dim objClassDB As New ms_Container(ms_Container.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            cboContainer_Size1.BeginUpdate()
            With cboContainer_Size1
                .DisplayMember = "Container_No"
                .ValueMember = "Container_Index"
                .DataSource = objDT
            End With

            cboContainer_Size1.EndUpdate()
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

            cboTransportJobType.BeginUpdate()
            With cboTransportJobType
                .DisplayMember = "Description"
                .ValueMember = "TransportJobType_Index"
                .DataSource = objDT
            End With

            cboTransportJobType.EndUpdate()
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

            cboRoute.BeginUpdate()
            With cboRoute
                .DisplayMember = "Description"
                .ValueMember = "Route_Index"
                .DataSource = objDT
            End With

            cboRoute.EndUpdate()
            If cboRoute.Items.Count = 0 Then Exit Sub

            getComboSubRoute(cboRoute.SelectedValue.ToString)
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
            objClassDB.GetAllAsDataTable(vRoute_Index)
            objDT = objClassDB.DataTable

            cboSubRoute.BeginUpdate()
            With cboSubRoute
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = objDT
            End With

            cboSubRoute.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnSearchDriver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchDriver.Click
        Try

            Dim frm As New WMS_STD_OUTB_Transport.frmDriver_Popup
            frm.isDriverManifest = True
            frm.TransportManifest_Index = Me._TransportManifest_Index
            frm.ShowDialog()
            If frm.Driver_Index <> "" Then
                Dim objDriver As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.ADDNEW)
                objDriver.TransportManifest_Index = Me._TransportManifest_Index
                objDriver.Driver_Index = frm.Driver_Index
                objDriver.isSubDriver = False
                objDriver.Seq = cboDriver.Items.Count
                objDriver.Insert()
                getComboDriver()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelDriver_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelDriver.Click
        Try
            If cboDriver.SelectedValue Is Nothing Then Exit Sub
            If W_MSG_Confirm_ByIndex(100004) = Windows.Forms.DialogResult.Yes = True Then
                Dim objDelManifest As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.DELETE)
                objDelManifest.TransportManifest_Index = Me._TransportManifest_Index
                objDelManifest.Driver_Index = cboDriver.SelectedValue.ToString
                objDelManifest.DeleteDriver()
                getComboDriver()
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSearchTruckStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchTruckStaff.Click
        Try
            Dim frm As New WMS_STD_OUTB_Transport.frmDriver_Popup
            frm.TransportManifest_Index = Me._TransportManifest_Index
            frm.isDriverManifest = True
            frm.ShowDialog()
            If frm.Driver_Index <> "" Then
                Dim objDriver As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.ADDNEW)
                objDriver.TransportManifest_Index = Me._TransportManifest_Index
                objDriver.Driver_Index = frm.Driver_Index
                objDriver.isSubDriver = True
                objDriver.Seq = cboDriver.Items.Count
                objDriver.Insert()
                getComboSubDriver()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelTruckStaff_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelTruckStaff.Click
        Try
            If cboTruckStaff.SelectedValue Is Nothing Then Exit Sub
            If W_MSG_Confirm_ByIndex(100004) = Windows.Forms.DialogResult.Yes = True Then
                Dim objDelManifest As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.DELETE)
                objDelManifest.TransportManifest_Index = Me._TransportManifest_Index
                objDelManifest.Driver_Index = Me.cboTruckStaff.SelectedValue.ToString
                objDelManifest.DeleteDriver()
                getComboSubDriver()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.cboRoute.SelectedValue Is Nothing Then
                W_MSG_Information("กรุณาเลือกเส้นทางหลัก")
                Exit Sub
            End If
            If Me.cboSubRoute.SelectedValue Is Nothing Then
                W_MSG_Information("กรุณาเลือกเส้นทางย่อย")
                Exit Sub
            End If
            If cboDistributionCenter.SelectedValue Is Nothing Then
                W_MSG_Information("กรุณาเลือกส่งศูนย์กระจาย")
                Exit Sub
            End If


            '************************* HEADER ***********************
            'If txtVehicleID.Text = "" Then
            '    W_MSG_Information_ByIndex(300053)
            '    Exit Sub
            'End If

            'If txtTransportManifestNo.Text.Trim = "" Then
            '    Dim objDocumentNumber As New Sy_DocumentNumber
            '    Me.txtTransportManifestNo.Text = objDocumentNumber.Auto_DocumentType_Number(Me.cboDocumentType.SelectedValue, "")
            '    objDocumentNumber = Nothing
            'End If


            'Select Case objStatus
            '    Case Manifest_Mode.ADD
            '        Dim objDBIndex As New Sy_AutoNumber
            '        Me._TransportManifest_Index = objDBIndex.getSys_Value("TransportManifest_Index")
            '        objDBIndex = Nothing

            '    Case Manifest_Mode.COPY
            '        Dim objDBIndex As New Sy_AutoNumber
            '        Me._TransportManifest_Index = objDBIndex.getSys_Value("TransportManifest_Index")
            '        objDBIndex = Nothing

            '    Case Manifest_Mode.EDIT


            'End Select

            'If  Then

            'End If

            'If Me.txtVehicleID.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาเลือกหมายเลขรถ")
            '    Exit Sub
            'End If

            'If Me.txtRefNo1.Text.Trim = "" Then
            '    W_MSG_Information("กรุณา : " & Me.lblRefNo1.Text)
            '    Me.txtRefNo1.Focus()
            '    Exit Sub
            'End If

            'If Me.cboRoute.SelectedValue.ToString = "0010000000000" Then
            '    W_MSG_Information("กรุณาเลือกเส้นทางหลัก")
            '    Exit Sub
            'End If


            'If Me.cboSubRoute.SelectedValue.ToString = "0010000000000" Then
            '    W_MSG_Information("กรุณาเลือกเส้นทางย่อย")
            '    Exit Sub
            'End If

            'If cboDriver.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาเลือกพนักงานขับรถ")
            '    Exit Sub
            'End If


            If cboDistributionCenter.Text.Trim = "" Then
                W_MSG_Information("กรุณาเลือกส่งศูนย์กระจาย")
                Exit Sub
            End If

            ' BGN Pong 2017-05-03 
            If (Me.cboDistributionCenter.SelectedValue IsNot Nothing) Then
                'If (Me.cboDistributionCenter.SelectedValue = "-11" Or Me.cboDistributionCenter.SelectedValue = "0010000000000") Then
                '    W_MSG_Information("ไม่สามารถเลือกส่งศูนย์กระจายนี้ได้")
                '    Exit Sub
                'End If
            Else
                W_MSG_Information("กรุณาเลือกส่งศูนย์กระจาย")
                Exit Sub
            End If
            ' END Pong 2017-05-03 

            If txtSumPrice_In.Text.Trim = "" Then
                W_MSG_Information("กรุณากรอกคิดเงินรับ")
                Exit Sub
            End If

            If txtSumPrice_Out.Text.Trim = "" Then
                W_MSG_Information("กรุณากรอกคิดเงินจ่าย")
                Exit Sub
            End If


            Dim objTransportManifest As New tb_TransportManifest_Update 'tb_TransportManifest(tb_TransportManifest.enuOperation_Type.ADDNEW)


            If Me.txtTransportManifest_No.Text = "" Then
                Dim strWhere As String = ""
                Dim objDocumentNumber As New WMS_STD_OUTB_Transport.Sy_DocumentNumber
                objTransportManifest.TransportManifest_No = objDocumentNumber.Auto_DocumentType_Number(Me.cboDocumentType.SelectedValue, strWhere, Me.dtpTransportManifest_Date.Value)
                Me.txtTransportManifest_No.Text = objTransportManifest.TransportManifest_No
                objDocumentNumber = Nothing
            End If


            objTransportManifest.TransportManifest_Index = Me._TransportManifest_Index
            objTransportManifest.TransportManifest_No = txtTransportManifest_No.Text
            objTransportManifest.TransportManifest_Date = dtpTransportManifest_Date.Value.ToString("yyyy/MM/dd HH:mm:ss")
            objTransportManifest.Trip_Sequence = cboTripSeq.Text
            objTransportManifest.Trip_Description = txtTrip_Description.Text
            objTransportManifest.IsInternalVehicle = True
            objTransportManifest.IsContainerHaulage = chkContainerHaulage.Checked
            objTransportManifest.IsMultiDrop = chkMultidrop.Checked
            objTransportManifest.IsMilkRun = False
            objTransportManifest.Has_Backhaul = chkTransportToDC.Checked
            objTransportManifest.Has_PickupReturnItem = chkPickupReturnItem.Checked
            objTransportManifest.Vehicle_License_No = txtVehicleNo.Text
            objTransportManifest.Tail_License_No = txtlblTailLicense_No.Text
            objTransportManifest.Transport_From = txtTransport_From.Text
            objTransportManifest.Transport_To = txtTrasport_To.Text
            objTransportManifest.Container_No1 = txtContainer_No1.Text
            objTransportManifest.Container_No2 = txtContainer_No2.Text
            objTransportManifest.ContainerSeal_No1 = txtSeal_No1.Text
            objTransportManifest.ContainerSeal_No2 = txtSeal_No2.Text
            objTransportManifest.Booking_No = txtTruck_No.Text
            objTransportManifest.Comment = txtComment.Text
            objTransportManifest.IsPack = Me.chkIsPack.Checked

            '---- Check Tag Keep Index Noting -------------

            If cboHandlingType.SelectedValue IsNot Nothing Then
                objTransportManifest.HandlingType_Index = cboHandlingType.SelectedValue.ToString
            Else
                objTransportManifest.HandlingType_Index = ""
            End If


            If cboDriver.SelectedValue IsNot Nothing Then
                objTransportManifest.Driver_Index = cboDriver.SelectedValue.ToString
            Else
                objTransportManifest.Driver_Index = ""
            End If

            If cboTruckStaff.SelectedValue IsNot Nothing Then
                objTransportManifest.TruckStaff_Index = cboTruckStaff.SelectedValue.ToString
            Else
                objTransportManifest.TruckStaff_Index = ""
            End If

            If cboHandlingType.SelectedValue IsNot Nothing Then
                objTransportManifest.HandlingType_Index = cboHandlingType.SelectedValue.ToString
            Else
                objTransportManifest.HandlingType_Index = ""
            End If


            If txtVehicleID.Tag IsNot Nothing Then
                objTransportManifest.Vehicle_Index = txtVehicleID.Tag.ToString
            Else
                objTransportManifest.VehicleType_Index = ""
            End If

            If cboVehicleType.SelectedValue IsNot Nothing Then
                objTransportManifest.VehicleType_Index = cboVehicleType.SelectedValue.ToString
            Else
                objTransportManifest.VehicleType_Index = ""
            End If


            '---- Check ComboBox When Noting -------------
            If cboTransportJobType.SelectedValue IsNot Nothing Then
                objTransportManifest.DocumentType_Index = Me.cboDocumentType.SelectedValue
            Else
                objTransportManifest.DocumentType_Index = ""
            End If

            If cboTransportJobType.SelectedValue IsNot Nothing Then
                objTransportManifest.TransportJobType_Index = cboTransportJobType.SelectedValue.ToString
            Else
                objTransportManifest.TransportJobType_Index = ""
            End If
            If cboContainer_Size1.SelectedValue IsNot Nothing Then
                objTransportManifest.Container_Size_Index1 = cboContainer_Size1.SelectedValue.ToString
            Else
                objTransportManifest.Container_Size_Index1 = ""
            End If
            If cboContainer_Size2.SelectedValue IsNot Nothing Then
                objTransportManifest.Container_Size_Index2 = cboContainer_Size2.SelectedValue.ToString
            Else
                objTransportManifest.Container_Size_Index2 = ""
            End If

            If cboDistributionCenter.SelectedValue IsNot Nothing Then
                If cboDistributionCenter.SelectedValue = "-11" Then
                    objTransportManifest.DistributionCenter_Index = ""
                Else
                    objTransportManifest.DistributionCenter_Index = cboDistributionCenter.SelectedValue.ToString
                End If
            Else
                objTransportManifest.DistributionCenter_Index = ""
            End If

            If cboRoute.SelectedValue IsNot Nothing Then
                objTransportManifest.Route_Index = cboRoute.SelectedValue.ToString
            Else
                objTransportManifest.Route_Index = ""
            End If

            'เส้นทางย่อย
            If cboSubRoute.SelectedValue IsNot Nothing Then
                objTransportManifest.SubRoute_Index = cboSubRoute.SelectedValue.ToString
            Else
                objTransportManifest.SubRoute_Index = ""
            End If

            objTransportManifest.Customer_Receive_Location_Index = ""
            objTransportManifest.Customer_Shipping_Index = ""
            objTransportManifest.Customer_Shipping_Location_Index = ""
            objTransportManifest.Department_Index = ""

            objTransportManifest.Customer_Receive_Location_Index = ""
            objTransportManifest.Customer_Shipping_Index = ""
            objTransportManifest.Customer_Shipping_Location_Index = ""
            objTransportManifest.Department_Index = ""
            objTransportManifest.Carrier_Index = ""


            If txtCustomer_Id.Tag IsNot Nothing Then
                objTransportManifest.Customer_Index = txtCustomer_Id.Tag.ToString
            Else
                objTransportManifest.Customer_Index = ""
            End If
            If Me.txtConsignee_Id.Tag IsNot Nothing Then
                objTransportManifest.Customer_Shipping_Index = Me.txtConsignee_Id.Tag
            End If
            If Me.txtShipping_Location_ID.Tag IsNot Nothing Then
                objTransportManifest.Customer_Shipping_Location_Index = Me.txtShipping_Location_ID.Tag
            End If
            If txtCarrier_ID.Tag IsNot Nothing Then
                objTransportManifest.Carrier_Index = txtCarrier_ID.Tag
            End If
            objTransportManifest.Worker = CInt(txtWorker.Text)


            If IsNumeric(txtTrip_Petro_Mile.Text) Then
                objTransportManifest.Trip_Petro_Mile = txtTrip_Petro_Mile.Text
            End If
            If IsNumeric(txtTrip_Petro_Volume.Text) Then
                objTransportManifest.Trip_Petro_Volume = txtTrip_Petro_Volume.Text
            End If
            If IsNumeric(txtTrip_Petro_UnitPrice.Text) Then
                objTransportManifest.Trip_Petro_UnitPrice = txtTrip_Petro_UnitPrice.Text
            End If
            If IsNumeric(txtTrip_Petro_Amount.Text) Then
                objTransportManifest.Trip_Petro_Amount = txtTrip_Petro_Amount.Text
            End If
            If IsNumeric(txtTrip_Petro_Amount.Text) Then
                objTransportManifest.Trip_Petro_Amount = txtTrip_Petro_Amount.Text
            End If
            If IsNumeric(txtTrip_Gas_Mile.Text) Then
                objTransportManifest.Trip_Gas_Mile = txtTrip_Gas_Mile.Text
            End If
            If IsNumeric(txtTrip_Gas_Volume.Text) Then
                objTransportManifest.Trip_Gas_Volume = txtTrip_Gas_Volume.Text
            End If
            If IsNumeric(txtTrip_Gas_UnitPrice.Text) Then
                objTransportManifest.Trip_Gas_UnitPrice = txtTrip_Gas_UnitPrice.Text
            End If
            If IsNumeric(txtTrip_Gas_Amount.Text) Then
                objTransportManifest.Trip_Gas_Amount = txtTrip_Gas_Amount.Text
            End If
            If IsNumeric(txtMile_OutToPickup.Text) Then
                objTransportManifest.Mile_OutToPickup = txtMile_OutToPickup.Text
            End If
            If IsNumeric(txtMile_AtSource.Text) Then
                objTransportManifest.Mile_AtSource = txtMile_AtSource.Text
            End If
            If IsNumeric(txtMile_AtDestination.Text) Then
                objTransportManifest.Mile_AtDestination = txtMile_AtDestination.Text
            End If
            If IsNumeric(txtMile_Return.Text) Then
                objTransportManifest.Mile_Return = txtMile_Return.Text
            End If



            '*****  บันทึกเวลาการจัดส่ง   *****
            If Me.tbcTransportManifestItem.Contains(tbpTripTime) Then

                objTransportManifest.isTripTime1 = True

                objTransportManifest.Time_OutToPickup = dtpTransportManifest_Date.Value

                If chkTime_SourceInGate.Checked Then
                    objTransportManifest.Time_SourceInGate = CDate(dtTime_SourceInGate.Value.ToShortDateString & " " & tTime_SourceInGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_SourceInGate = Nothing
                End If

                If chkTime_SourceLoadStart.Checked Then
                    objTransportManifest.Time_SourceLoadStart = CDate(dtTime_SourceLoadStart.Value.ToShortDateString & " " & tTime_SourceLoadStart.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_SourceLoadStart = Nothing
                End If

                If chkTime_SourceLoadFinish.Checked Then
                    objTransportManifest.Time_SourceLoadFinish = CDate(dtTime_SourceLoadFinish.Value.ToShortDateString & " " & tTime_SourceLoadFinish.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_SourceLoadFinish = Nothing
                End If

                If chkTime_SourceOutGate.Checked Then
                    objTransportManifest.Time_SourceOutGate = CDate(dtTime_SourceOutGatet.Value.ToShortDateString & " " & tTime_SourceOutGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_SourceOutGate = Nothing
                End If

                If chkTime_DestinationInGate.Checked Then
                    objTransportManifest.Time_DestinationInGate = CDate(dtTime_DestinationInGate.Value.ToShortDateString & " " & tTime_DestinationInGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_DestinationInGate = Nothing
                End If
                If chkTime_DestinationUnloadStart.Checked Then
                    objTransportManifest.Time_DestinationUnloadStart = CDate(dtTime_DestinationUnloadStart.Value.ToShortDateString & " " & tTime_DestinationUnloadStart.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_DestinationUnloadStart = Nothing
                End If

                If chkTime_DestinationUnloadFinish.Checked Then
                    objTransportManifest.Time_DestinationUnloadFinish = CDate(dtTime_DestinationUnloadFinish.Value.ToShortDateString & " " & tTime_DestinationUnloadFinish.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_DestinationUnloadFinish = Nothing
                End If

                If chkTime_DestinationOutGate.Checked Then
                    objTransportManifest.Time_DestinationOutGate = CDate(dtTime_DestinationOutGate.Value.ToShortDateString & " " & tTime_DestinationOutGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_DestinationOutGate = Nothing
                End If

                If chkTime_ReturnTruckInGate.Checked Then
                    objTransportManifest.Time_ReturnTruckInGate = CDate(dtTime_ReturnTruckInGate.Value.ToShortDateString & " " & tTime_ReturnTruckInGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_ReturnTruckInGate = Nothing
                End If


                If chkTime_ReturnTruckUnloadStart.Checked Then
                    objTransportManifest.Time_ReturnTruckUnloadStart = CDate(dtTime_ReturnTruckUnloadStart.Value.ToShortDateString & " " & tTime_ReturnTruckUnloadStart.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_ReturnTruckUnloadStart = Nothing
                End If

                If chkTime_ReturnTruckUnloadFinish.Checked Then
                    objTransportManifest.Time_ReturnTruckUnloadFinish = CDate(dtTime_ReturnTruckUnloadFinish.Value.ToShortDateString & " " & tTime_ReturnTruckUnloadFinish.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_ReturnTruckUnloadFinish = Nothing
                End If

                If chkTime_ReturnTruckOutGate.Checked Then
                    objTransportManifest.Time_ReturnTruckOutGate = CDate(dtTime_ReturnTruckOutGate.Value.ToShortDateString & " " & tTime_ReturnTruckOutGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_ReturnTruckOutGate = Nothing
                End If



            End If

            If Me.tbcTransportManifestItem.Contains(tbpTripTime2) Then
                objTransportManifest.isTripTime2 = True

                If chkTime_SourceOutGate2.Checked Then
                    objTransportManifest.Time_SourceOutGate = CDate(dtTime_SourceOutGatet2.Value.ToShortDateString & " " & tTime_SourceOutGate2.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_SourceOutGate = Nothing
                End If
                If chkTime_DestinationInGate2.Checked Then
                    objTransportManifest.Time_DestinationInGate = CDate(dtTime_DestinationInGate2.Value.ToShortDateString & " " & tTime_DestinationInGate2.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_DestinationInGate = Nothing
                End If
                If chkTime_ReturnTruckInGate.Checked Then
                    objTransportManifest.Time_ReturnTruckInGate = CDate(dtTime_ReturnTruckInGate2.Value.ToShortDateString & " " & tTime_ReturnTruckInGate2.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                Else
                    objTransportManifest.Time_ReturnTruckInGate = Nothing
                End If
            End If

            objTransportManifest.add_by = ""
            objTransportManifest.add_date = Now
            objTransportManifest.add_branch = 0
            objTransportManifest.update_by = ""
            objTransportManifest.update_date = Now
            objTransportManifest.update_branch = 0
            objTransportManifest.cancel_by = ""
            objTransportManifest.cancel_date = Now
            objTransportManifest.cancel_branch = 0
            objTransportManifest.Str1 = txtRefNo1.Text
            objTransportManifest.Str2 = ""
            objTransportManifest.Str3 = ""
            objTransportManifest.Str4 = ""
            objTransportManifest.Str5 = ""
            objTransportManifest.Str6 = ""
            objTransportManifest.Str7 = ""
            objTransportManifest.Str8 = ""
            objTransportManifest.Str9 = ""
            objTransportManifest.Str10 = ""
            objTransportManifest.IsSubManifest = _IsSubManifest

            If IsNumeric(txtSumPrice_In.Text) Then objTransportManifest.TotalTransportCharged = CDbl(txtSumPrice_In.Text)
            If IsNumeric(txtSumPrice_Out.Text) Then objTransportManifest.TotalTransportPaid = CDbl(txtSumPrice_Out.Text)

            If IsNumeric(txtDriverPaidAmount.Text) Then objTransportManifest.DriverPaidAmount = CDbl(txtDriverPaidAmount.Text)
            objTransportManifest.isTransportCharged = chkTransportCharged.Checked
            objTransportManifest.isTransportPaid = chkTransportPaid.Checked
            objTransportManifest.isSpecialCase = chkSpecialCase.Checked

            If (objTransportManifest.TotalTransportCharged < objTransportManifest.TotalTransportPaid) Then
                If W_MSG_Confirm("Trip นี้ขาดทุน " & (objTransportManifest.TotalTransportPaid - objTransportManifest.TotalTransportCharged) & Chr(13) & " บาท ท่านต้องการยืนยันข้อมูลใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If

            End If

            Dim oGetconfig As New config_CustomSetting
            If oGetconfig.getConfig_Key_USE("USE_TRANSPORT_LOADING_CONFIRM") Then
                objTransportManifest.Status = 3
            Else
                objTransportManifest.Status = 6
            End If

            'ถ้าไม่มีรายการ SO ในใบคุมให้รอปล่อยรถ
            'If _dataTable.Rows.Count > 0 Then
            '    Dim drArrCheckProcess() As DataRow = _dataTable.Select("Process_Id=10")
            '    If drArrCheckProcess.Length = 0 Then
            '        objTransportManifest.Status = 6
            '    End If
            'End If


            ''************************* BEGIN DETAIL ***********************
            ' Assinge Item
            Dim cllTransportManifestItem As New List(Of tb_TransportManifestItem)
            '   Dim cllSalesOrderTrip As New List(Of tb_SalesOrderTrip)
            Select Case objStatus
                Case Manifest_Mode.COPY
                    '************************* DETAIL ***********************
                    'objTransportManifest.Status = 1
                    'Dim oGetconfig As New config_CustomSetting
                    'If oGetconfig.getConfig_Key_USE("USE_TRANSPORT_LOADING_CONFIRM") Then
                    '    objTransportManifest.Status = 3
                    'Else
                    '    objTransportManifest.Status = 6
                    'End If

                    Dim cllSalesOrderTrip As New List(Of tb_SalesOrderTrip)
                    Dim ogetcopy As New tb_TransportManifestItem(tb_TransportManifestItem.enuOperation_Type.SEARCH)
                    ogetcopy.GetAllAsDataTable_ByIndex(Me._TransportManifest_Index)
                    Dim _dataTable As New DataTable
                    _dataTable = ogetcopy.GetDataTable
                    For Each drSo As DataRow In _dataTable.Rows
                        Dim objTransportManifestItem As New tb_TransportManifestItem(tb_TransportManifestItem.enuOperation_Type.ADDNEW)
                        Dim objSalesOrderTrip As New tb_SalesOrderTrip(tb_SalesOrderTrip.enuOperation_Type.ADDNEW)

                        objTransportManifestItem.TransportManifest_Index = Me._TransportManifest_Index
                        'Dim objDBIndex As New Sy_AutoNumber
                        'objTransportManifestItem.TransportManifestItem_Index = objDBIndex.getSys_Value("TransportManifestItem_Index")
                        'objDBIndex = Nothing

                        objTransportManifestItem.Process_Id = drSo("Process_Id").ToString
                        objTransportManifestItem.SalesOrder_Index = drSo("SalesOrder_Index").ToString
                        objTransportManifestItem.Withdraw_Index = drSo("Withdraw_Index").ToString
                        objTransportManifestItem.Order_Index = drSo("Order_Index").ToString
                        objTransportManifestItem.Invoice_No = drSo("Invoice_No").ToString
                        objTransportManifestItem.DO_No = drSo("DO_No").ToString
                        objTransportManifestItem.Qty_Shipped = CDbl(drSo("Qty_Shipped").ToString)
                        objTransportManifestItem.Weight_Shipped = CDbl(drSo("Weight_Shipped").ToString)
                        objTransportManifestItem.Volume_Shipped = CDbl(drSo("Volume_Shipped").ToString)
                        objTransportManifestItem.Pallet_Shipped = CDbl(drSo("Pallet_Shipped").ToString)
                        objTransportManifestItem.Value_Shipped = CDbl(drSo("Value_Shipped").ToString)
                        objTransportManifestItem.Qty_Delivered = 0
                        objTransportManifestItem.Weight_Delivered = 0
                        objTransportManifestItem.Volume_Delivered = 0
                        objTransportManifestItem.Pallet_Delivered = 0
                        objTransportManifestItem.Value_Delivered = 0
                        objTransportManifestItem.Qty_Returned = 0
                        objTransportManifestItem.Weight_Returned = 0
                        objTransportManifestItem.Volume_Returned = 0
                        objTransportManifestItem.Pallet_Returned = 0
                        objTransportManifestItem.Value_Returned = 0
                        objTransportManifestItem.Qty_Lost = 0
                        objTransportManifestItem.Weight_Lost = 0
                        objTransportManifestItem.Volume_Lost = 0
                        objTransportManifestItem.Pallet_Lost = 0
                        objTransportManifestItem.Value_Lost = 0
                        objTransportManifestItem.Qty_Damaged = 0
                        objTransportManifestItem.Weight_Damaged = 0
                        objTransportManifestItem.Volume_Damaged = 0
                        objTransportManifestItem.Pallet_Damaged = 0
                        objTransportManifestItem.Value_Damaged = 0
                        objTransportManifestItem.Qty_NeedReShipped = 0
                        objTransportManifestItem.Weight_NeedReShipped = 0
                        objTransportManifestItem.Volume_NeedReShipped = 0
                        objTransportManifestItem.Pallet_NeedReShipped = 0
                        objTransportManifestItem.Value_NeedReShipped = 0
                        objTransportManifestItem.JobProblem_Index = ""
                        objTransportManifestItem.JobProblem_Desc = ""
                        objTransportManifestItem.ResponsibleParty_Index = ""
                        objTransportManifestItem.JobSolution_Index = ""
                        objTransportManifestItem.JobSolution_Desc = ""
                        objTransportManifestItem.Comment = drSo("Comment").ToString
                        objTransportManifestItem.Customer_Index = drSo("Customer_Index").ToString
                        objTransportManifestItem.Customer_Receive_Location_Index = drSo("Customer_Receive_Location_Index").ToString
                        objTransportManifestItem.Customer_Shipping_Index = drSo("Customer_Shipping_Index").ToString
                        objTransportManifestItem.Customer_Shipping_Location_Index = drSo("Customer_Shipping_Location_Index").ToString
                        objTransportManifestItem.Department_Index = drSo("Department_Index").ToString
                        objTransportManifestItem.Appointment_Date = Now
                        objTransportManifestItem.Appointment_Time = ""
                        objTransportManifestItem.DocumentReturn_Method_Index = ""

                        If drSo("Time_ExpectedDocPickup").ToString <> "" Then
                            objTransportManifestItem.Time_ExpectedDocPickup = drSo("Time_ExpectedDocPickup")
                        Else
                            objTransportManifestItem.Time_ExpectedDocPickup = Nothing
                        End If
                        If drSo("Time_ExpectedDeliveryToDestination").ToString <> "" Then
                            objTransportManifestItem.Time_ExpectedDeliveryToDestination = drSo("Time_ExpectedDeliveryToDestination")
                        Else
                            objTransportManifestItem.Time_ExpectedDeliveryToDestination = Nothing
                        End If
                        If drSo("Time_ExpectedReturnPickup").ToString <> "" Then
                            objTransportManifestItem.Time_ExpectedReturnPickup = drSo("Time_ExpectedReturnPickup")
                        Else
                            objTransportManifestItem.Time_ExpectedReturnPickup = Nothing
                        End If
                        If drSo("Time_DocIssued").ToString <> "" Then
                            objTransportManifestItem.Time_DocIssued = drSo("Time_DocIssued")
                        Else
                            objTransportManifestItem.Time_DocIssued = Nothing
                        End If
                        If drSo("Time_DocPickup").ToString <> "" Then
                            objTransportManifestItem.Time_DocPickup = drSo("Time_DocPickup")
                        Else
                            objTransportManifestItem.Time_DocPickup = Nothing
                        End If

                        objTransportManifestItem.Time_ExpectedDocReturnToOwner = Nothing
                        objTransportManifestItem.Time_DocTripConfirmed = Nothing
                        objTransportManifestItem.Time_DeliveryToDestination = Nothing
                        objTransportManifestItem.Time_ReturnPickup = Nothing
                        objTransportManifestItem.Time_DocReturnedToDC = Nothing
                        objTransportManifestItem.Time_DocReturnedToSource = Nothing
                        objTransportManifestItem.Time_DocReturnedToOwner = Nothing
                        objTransportManifestItem.Time_DocConfirmedByOwner = Nothing
                        objTransportManifestItem.Time_NextDeliveryToDestination = Nothing
                        objTransportManifestItem.Time_NextReturnPickup = Nothing


                        objTransportManifestItem.Mile_AtDestination = 0
                        objTransportManifestItem.Ref_No1 = ""
                        objTransportManifestItem.Ref_No2 = ""
                        objTransportManifestItem.Ref_No3 = ""
                        objTransportManifestItem.Ref_No4 = ""
                        objTransportManifestItem.Ref_No5 = ""
                        objTransportManifestItem.Flo1 = 0
                        objTransportManifestItem.Flo2 = 0
                        objTransportManifestItem.Flo3 = 0
                        objTransportManifestItem.Flo4 = 0
                        objTransportManifestItem.Flo5 = 0
                        objTransportManifestItem.Flo6 = 0
                        objTransportManifestItem.Flo7 = 0
                        objTransportManifestItem.Flo8 = 0
                        objTransportManifestItem.Flo9 = 0
                        objTransportManifestItem.Flo10 = 0
                        objTransportManifestItem.Str1 = ""
                        objTransportManifestItem.Str2 = ""
                        objTransportManifestItem.Str3 = ""
                        objTransportManifestItem.Str4 = ""
                        objTransportManifestItem.Str5 = ""
                        objTransportManifestItem.Str6 = ""
                        objTransportManifestItem.Str7 = ""
                        objTransportManifestItem.Str8 = ""
                        objTransportManifestItem.Str9 = ""
                        objTransportManifestItem.Str10 = ""
                        objTransportManifestItem.add_by = ""
                        objTransportManifestItem.add_date = Now
                        objTransportManifestItem.add_branch = 0
                        objTransportManifestItem.update_by = ""
                        objTransportManifestItem.update_date = Now
                        objTransportManifestItem.update_branch = 0
                        objTransportManifestItem.cancel_by = ""
                        objTransportManifestItem.cancel_date = ""
                        objTransportManifestItem.cancel_branch = 0
                        objTransportManifestItem.Status = 1
                        objTransportManifestItem.Time_ExpectedDeliveryToDestination_Remark = "" ' drSo("Time_ExpectedDeliveryToDestination_Remark").ToString
                        objTransportManifestItem.IsTransportPaid = drSo("IsTransportPaid").ToString
                        objTransportManifestItem.IsTransportCharged = drSo("IsTransportCharged").ToString

                        objTransportManifestItem.IsSendOrPickup = drSo("IsSendOrPickup").ToString
                        objTransportManifestItem.IsPacking = drSo("IsPacking").ToString
                        'Select Case objTransportManifestItem.Process_Id
                        '    Case 10
                        '        objTransportManifestItem.IsSendOrPickup = 1
                        '    Case Else
                        '        objTransportManifestItem.IsSendOrPickup = 2
                        'End Select


                        cllTransportManifestItem.Add(objTransportManifestItem)


                        ''    strSQL &= "                 ,Time_ExpectedDeliveryToDestination"
                        ''    strSQL &= "                 ,Time_ExpectedReturnPickup"
                        ''    strSQL &= "                 ,Time_DocIssued"
                        ''    strSQL &= "                 ,Time_DocPickup"
                        ''    strSQL &= "                 ,Time_DocTripConfirmed"

                        'Dim objDBIndex2 As New Sy_AutoNumber
                        'objSalesOrderTrip.SalesOrderTrip_Index = objDBIndex2.getSys_Value("SalesOrderTrip_Index")
                        'objSalesOrderTrip.TransportManifest_Index = objTransportManifest.TransportManifest_Index

                        'objSalesOrderTrip.SalesOrder_Index = drSo("SalesOrder_Index")
                        'objSalesOrderTrip.Time_ExpectedDocPickup = drSo("Time_ExpectedDocPickup")
                        'objSalesOrderTrip.Time_ExpectedDeliveryToDestination = drSo("Expected_Delivery_Date")
                        'objSalesOrderTrip.Time_ExpectedReturnPickup = drSo("Expected_Delivery_Date")
                        'objSalesOrderTrip.Time_DocIssued = drSo("SalesOrder_Date")
                        'objSalesOrderTrip.Time_DocPickup = drSo("Time_DocPickup")
                        'objSalesOrderTrip.Time_DocTripConfirmed = Nothing
                        'cllSalesOrderTrip.Add(objSalesOrderTrip)

                    Next
                Case Else

                    For Each drSo As DataRow In CType(grdTrasportManifestItem.DataSource, DataTable).Rows
                        Dim objTransportManifestItem As New tb_TransportManifestItem(tb_TransportManifestItem.enuOperation_Type.ADDNEW)
                        Dim objSalesOrderTrip As New tb_SalesOrderTrip(tb_SalesOrderTrip.enuOperation_Type.ADDNEW)

                        objTransportManifestItem.TransportManifest_Index = Me._TransportManifest_Index

                        If drSo("TransportManifestItem_Index").ToString = "" Then
                            Dim objDBIndex As New Sy_AutoNumber
                            objTransportManifestItem.TransportManifestItem_Index = objDBIndex.getSys_Value("TransportManifestItem_Index")
                            objDBIndex = Nothing
                        Else
                            objTransportManifestItem.TransportManifestItem_Index = drSo("TransportManifestItem_Index").ToString
                        End If

                        objTransportManifestItem.SalesOrder_Index = drSo("SalesOrder_Index").ToString
                        objTransportManifestItem.Withdraw_Index = ""
                        objTransportManifestItem.Order_Index = ""
                        objTransportManifestItem.Invoice_No = drSo("Invoice_No").ToString
                        objTransportManifestItem.DO_No = drSo("SalesOrder_No").ToString
                        objTransportManifestItem.Qty_Shipped = CDbl(drSo("Qty_Shipped").ToString)
                        objTransportManifestItem.Weight_Shipped = CDbl(drSo("Weight_Shipped").ToString)
                        objTransportManifestItem.Volume_Shipped = CDbl(drSo("Volume_Shipped").ToString)
                        objTransportManifestItem.Pallet_Shipped = 0
                        objTransportManifestItem.Value_Shipped = CDbl(drSo("Value_Shipped").ToString)
                        objTransportManifestItem.Qty_Delivered = 0
                        objTransportManifestItem.Weight_Delivered = 0
                        objTransportManifestItem.Volume_Delivered = 0
                        objTransportManifestItem.Pallet_Delivered = 0
                        objTransportManifestItem.Value_Delivered = 0
                        objTransportManifestItem.Qty_Returned = 0
                        objTransportManifestItem.Weight_Returned = 0
                        objTransportManifestItem.Volume_Returned = 0
                        objTransportManifestItem.Pallet_Returned = 0
                        objTransportManifestItem.Value_Returned = 0
                        objTransportManifestItem.Qty_Lost = 0
                        objTransportManifestItem.Weight_Lost = 0
                        objTransportManifestItem.Volume_Lost = 0
                        objTransportManifestItem.Pallet_Lost = 0
                        objTransportManifestItem.Value_Lost = 0
                        objTransportManifestItem.Qty_Damaged = 0
                        objTransportManifestItem.Weight_Damaged = 0
                        objTransportManifestItem.Volume_Damaged = 0
                        objTransportManifestItem.Pallet_Damaged = 0
                        objTransportManifestItem.Value_Damaged = 0
                        objTransportManifestItem.Qty_NeedReShipped = 0
                        objTransportManifestItem.Weight_NeedReShipped = 0
                        objTransportManifestItem.Volume_NeedReShipped = 0
                        objTransportManifestItem.Pallet_NeedReShipped = 0
                        objTransportManifestItem.Value_NeedReShipped = 0
                        objTransportManifestItem.JobProblem_Index = ""
                        objTransportManifestItem.JobProblem_Desc = ""
                        objTransportManifestItem.ResponsibleParty_Index = ""
                        objTransportManifestItem.JobSolution_Index = ""
                        objTransportManifestItem.JobSolution_Desc = ""
                        objTransportManifestItem.Comment = drSo("Remark").ToString
                        objTransportManifestItem.Customer_Index = drSo("Customer_Index").ToString
                        objTransportManifestItem.Customer_Receive_Location_Index = drSo("Customer_Receive_Location_Index").ToString
                        objTransportManifestItem.Customer_Shipping_Index = drSo("Customer_Shipping_Index").ToString
                        objTransportManifestItem.Customer_Shipping_Location_Index = drSo("Customer_Shipping_Location_Index").ToString
                        objTransportManifestItem.Department_Index = drSo("Department_Index").ToString
                        objTransportManifestItem.Appointment_Date = Now
                        objTransportManifestItem.Appointment_Time = ""
                        objTransportManifestItem.DocumentReturn_Method_Index = ""

                        If drSo("Time_ExpectedDocPickup").ToString <> "" Then
                            objTransportManifestItem.Time_ExpectedDocPickup = drSo("Time_ExpectedDocPickup")
                        Else
                            objTransportManifestItem.Time_ExpectedDocPickup = Nothing
                        End If
                        If drSo("Expected_Delivery_Date").ToString <> "" Then
                            objTransportManifestItem.Time_ExpectedDeliveryToDestination = drSo("Expected_Delivery_Date")
                        Else
                            objTransportManifestItem.Time_ExpectedDeliveryToDestination = Nothing
                        End If
                        If drSo("Expected_Delivery_Date").ToString <> "" Then
                            objTransportManifestItem.Time_ExpectedReturnPickup = drSo("Expected_Delivery_Date")
                        Else
                            objTransportManifestItem.Time_ExpectedReturnPickup = Nothing
                        End If
                        If drSo("SalesOrder_Date").ToString <> "" Then
                            objTransportManifestItem.Time_DocIssued = drSo("SalesOrder_Date")
                        Else
                            objTransportManifestItem.Time_DocIssued = Nothing
                        End If
                        If drSo("Time_DocPickup").ToString <> "" Then
                            objTransportManifestItem.Time_DocPickup = drSo("Time_DocPickup")
                        Else
                            objTransportManifestItem.Time_DocPickup = Nothing
                        End If

                        objTransportManifestItem.Time_ExpectedDocReturnToOwner = Nothing
                        objTransportManifestItem.Time_DocTripConfirmed = Nothing
                        objTransportManifestItem.Time_DeliveryToDestination = Nothing
                        objTransportManifestItem.Time_ReturnPickup = Nothing
                        objTransportManifestItem.Time_DocReturnedToDC = Nothing
                        objTransportManifestItem.Time_DocReturnedToSource = Nothing
                        objTransportManifestItem.Time_DocReturnedToOwner = Nothing
                        objTransportManifestItem.Time_DocConfirmedByOwner = Nothing
                        objTransportManifestItem.Time_NextDeliveryToDestination = Nothing
                        objTransportManifestItem.Time_NextReturnPickup = Nothing


                        objTransportManifestItem.Mile_AtDestination = 0
                        objTransportManifestItem.Ref_No1 = ""
                        objTransportManifestItem.Ref_No2 = ""
                        objTransportManifestItem.Ref_No3 = ""
                        objTransportManifestItem.Ref_No4 = ""
                        objTransportManifestItem.Ref_No5 = ""
                        objTransportManifestItem.Flo1 = 0
                        objTransportManifestItem.Flo2 = 0
                        objTransportManifestItem.Flo3 = 0
                        objTransportManifestItem.Flo4 = 0
                        objTransportManifestItem.Flo5 = 0
                        objTransportManifestItem.Flo6 = 0
                        objTransportManifestItem.Flo7 = 0
                        objTransportManifestItem.Flo8 = 0
                        objTransportManifestItem.Flo9 = 0
                        objTransportManifestItem.Flo10 = 0
                        objTransportManifestItem.Str1 = ""
                        objTransportManifestItem.Str2 = ""
                        objTransportManifestItem.Str3 = ""
                        objTransportManifestItem.Str4 = ""
                        objTransportManifestItem.Str5 = ""
                        objTransportManifestItem.Str6 = ""
                        objTransportManifestItem.Str7 = ""
                        objTransportManifestItem.Str8 = ""
                        objTransportManifestItem.Str9 = ""
                        objTransportManifestItem.Str10 = ""
                        objTransportManifestItem.add_by = ""
                        objTransportManifestItem.add_date = Now
                        objTransportManifestItem.add_branch = 0
                        objTransportManifestItem.update_by = ""
                        objTransportManifestItem.update_date = Now
                        objTransportManifestItem.update_branch = 0
                        objTransportManifestItem.cancel_by = ""
                        objTransportManifestItem.cancel_date = ""
                        objTransportManifestItem.cancel_branch = 0
                        objTransportManifestItem.Status = 1
                        objTransportManifestItem.Time_ExpectedDeliveryToDestination_Remark = drSo("Time_ExpectedDeliveryToDestination_Remark").ToString
                        objTransportManifestItem.Process_Id = drSo("Process_Id").ToString
                        objTransportManifestItem.IsTransportPaid = drSo("IsTransportPaid").ToString
                        objTransportManifestItem.IsTransportCharged = drSo("IsTransportCharged").ToString

                        objTransportManifestItem.IsSendOrPickup = drSo("IsSendOrPickup").ToString
                        objTransportManifestItem.IsPacking = drSo("IsPacking").ToString

                        Select Case objStatus
                            Case Manifest_Mode.ADD
                                Select Case objTransportManifestItem.Process_Id
                                    Case 10
                                        objTransportManifestItem.IsSendOrPickup = 1
                                    Case Else
                                        objTransportManifestItem.IsSendOrPickup = 2
                                End Select
                        End Select


                        cllTransportManifestItem.Add(objTransportManifestItem)


                    Next
            End Select


            ''************************* END DETAIL ***********************
            ''************************* BEGIN TransportManifestCharge DETAIL ***********************
            Dim objTransportManifestChargeCollection As New List(Of svar_TransportManifestCharge)
            If Me.chkTransportCharged.Checked = True Then
                If Me.grdTransportCharge.RowCount > 0 Then
                    CType(Me.grdTransportCharge.DataSource, DataTable).AcceptChanges()
                    Dim dtTransportCharge As New DataTable
                    dtTransportCharge = Me.grdTransportCharge.DataSource
                    For Each drTransportCharge As DataRow In dtTransportCharge.Rows
                        Dim objTransportManifestCharge As New svar_TransportManifestCharge
                        objTransportManifestCharge.TransportManifest_Index = Me._TransportManifest_Index
                        objTransportManifestCharge.Customer_Index = drTransportCharge("Customer_Index").ToString
                        objTransportManifestCharge.TransportRegion_Index = drTransportCharge("TransportRegion_Index").ToString
                        objTransportManifestCharge.Carrier_Index = "" 'drTransportCharge("Carrier_Index").ToString
                        objTransportManifestCharge.Description = drTransportCharge("Description").ToString
                        objTransportManifestCharge.sumDrop = drTransportCharge("sumDrop")
                        objTransportManifestCharge.Total_Qty = drTransportCharge("Total_Qty")
                        objTransportManifestCharge.Weight = drTransportCharge("Weight")
                        objTransportManifestCharge.Volume = drTransportCharge("Volume")
                        objTransportManifestCharge.Amount = drTransportCharge("Amount")
                        objTransportManifestCharge.Amount_Charge = drTransportCharge("Amount_Charge")
                        objTransportManifestCharge.TransportCharge_Type = 1 'คิดเงินขารับ
                        objTransportManifestCharge.Minimum_Rate = drTransportCharge("Minimum_Rate")
                        objTransportManifestChargeCollection.Add(objTransportManifestCharge)
                    Next
                End If
            End If


            If Me.chkTransportPaid.Checked = True Then
                If Me.grdTransportPayment.RowCount > 0 Then
                    CType(Me.grdTransportPayment.DataSource, DataTable).AcceptChanges()
                    Dim dtTransportPayment As New DataTable
                    dtTransportPayment = Me.grdTransportPayment.DataSource
                    For Each drTransportPayment As DataRow In dtTransportPayment.Rows
                        Dim objTransportManifestCharge As New svar_TransportManifestCharge
                        objTransportManifestCharge.TransportManifest_Index = Me._TransportManifest_Index
                        objTransportManifestCharge.Customer_Index = drTransportPayment("Customer_Index").ToString
                        objTransportManifestCharge.TransportRegion_Index = drTransportPayment("TransportRegion_Index").ToString
                        objTransportManifestCharge.Carrier_Index = drTransportPayment("Carrier_Index").ToString
                        objTransportManifestCharge.Description = drTransportPayment("Description").ToString
                        objTransportManifestCharge.sumDrop = drTransportPayment("sumDrop")
                        objTransportManifestCharge.Total_Qty = drTransportPayment("Total_Qty")
                        objTransportManifestCharge.Weight = drTransportPayment("Weight")
                        objTransportManifestCharge.Volume = drTransportPayment("Volume")
                        objTransportManifestCharge.Amount = drTransportPayment("Amount")
                        objTransportManifestCharge.Amount_Charge = drTransportPayment("Amount_Charge")
                        objTransportManifestCharge.TransportCharge_Type = 2 'คิดเงินขาจ่าย
                        objTransportManifestCharge.Minimum_Rate = drTransportPayment("Minimum_Rate")
                        objTransportManifestChargeCollection.Add(objTransportManifestCharge)
                    Next
                End If

            End If


            ''************************* END TransportManifestCharge DETAIL ***********************
            Select Case objStatus
                Case Manifest_Mode.ADD
                    Dim objSaveManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.ADDNEW, objTransportManifest, cllTransportManifestItem, objTransportManifestChargeCollection)
                    Me._TransportManifest_Index = objSaveManifest.SaveData()
                    Me.txtTransportManifest_No.Text = objSaveManifest.TransportManifest_No
                Case Manifest_Mode.EDIT
                    Dim objSaveManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.UPDATE, objTransportManifest, cllTransportManifestItem, objTransportManifestChargeCollection)
                    Me._TransportManifest_Index = objSaveManifest.SaveData()
                    Me.txtTransportManifest_No.Text = objSaveManifest.TransportManifest_No
                Case Manifest_Mode.COPY
                    Dim objSaveManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.COPY, objTransportManifest, cllTransportManifestItem, objTransportManifestChargeCollection)
                    Me._TransportManifest_Index = objSaveManifest.SaveData()
                    Me.txtTransportManifest_No.Text = objSaveManifest.TransportManifest_No
            End Select

            If Me._TransportManifest_Index <> "" Then
                W_MSG_Information_ByIndex(1)
                Me.getTransportManifest_Header()
                Me.getTransportManifest_Item()

                ''ค่าขนส่งขาจ่าย
                'Me.getTransportPayment()
                'Me.CallculateTransportPayment()
                ''ค่าขนส่งขารับ
                'Me.getTransportCharge()
                'Me.CallculateTransportChage()
                Me.getTransportCharge_Data()
                Me.getTransportPayment_Data()


                objStatus = Manifest_Mode.EDIT
                Me.ManageButtom()

            Else
                W_MSG_Information_ByIndex(2)
            End If
            SetRow_Number_grdTrasportManifestItem()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnViewDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewManifestItem.Click
        Try
            'If grdTrasportManifestItem.RowCount = 0 Then Exit Sub
            'Dim frm As New frmTOItem
            'frm.objStatus = frmTOItem.enuOperation_Type.UPDATE
            'frm.Status = 3
            'frm.SalesOrder_Index = grdTrasportManifestItem.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
            'frm.ShowDialog()

            If grdTrasportManifestItem.RowCount = 0 Then Exit Sub

            Select Case grdTrasportManifestItem.CurrentRow.Cells("col_Process_Id").Value
                Case Nothing
                    Dim frm As New frmSO
                    frm.objStatus = frmSO.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdTrasportManifestItem.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
                Case "10"
                    Dim frm As New frmSO
                    frm.objStatus = frmSO.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdTrasportManifestItem.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
                Case "25"
                    Dim frm As New frmTOItem
                    frm.objStatus = frmTOItem.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdTrasportManifestItem.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
                Case "27"
                    Dim frm As New frmGROItem
                    frm.objStatus = frmGROItem.enuOperation_Type.UPDATE
                    frm.Status = 3
                    frm.SalesOrder_Index = grdTrasportManifestItem.CurrentRow.Cells("col_SalesOrder_Index").Value.ToString()
                    frm.ShowDialog()
            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Function getVehicle_OnTruck(ByVal pstrVehicle_Index As String) As Boolean
        Try
            'รถ
            Dim dblWeight_Max As Double = 0
            Dim dblVolume_Max As Double = 0
            Dim boolLimit As Boolean = True

            If Not String.IsNullOrEmpty(pstrVehicle_Index) Then
                Dim oManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
                Dim dtManifest As New DataTable
                oManifest.getTructManifest_Summary(pstrVehicle_Index, "")
                dtManifest = oManifest.DataTable

                'Get Carrier
                If dtManifest.Rows(0).Item("Carrier_Index").ToString <> "" Then
                    Me.txtCarrier_ID.Tag = dtManifest.Rows(0).Item("Carrier_Index").ToString
                    Me.getCarrier()
                Else
                    Dim Msg As String = Me.lblVehicleID.Text & " " & Me.txtVehicleID.Text & Chr(13) & "ยังไม่ระบุ " & Me.lblCarrier.Text
                    W_MSG_Information(Msg)
                    Me.txtCarrier_ID.Tag = ""
                    Me.txtCarrier_ID.Text = ""
                    Me.txtCarrier_Name.Text = ""
                    boolLimit = False
                End If


                Me.txtVehicleID.Text = dtManifest.Rows(0)("Vehicle_Id").ToString
                Me.txtVehicleID.Tag = dtManifest.Rows(0)("Vehicle_Index").ToString
                Me.txtVehicleNo.Text = dtManifest.Rows(0)("Vehicle_No").ToString
                Me.cboVehicleType.SelectedValue = dtManifest.Rows(0)("VehicleType_Index").ToString

                If IsNumeric(dtManifest.Rows(0)("Weight_Vehicle")) Then
                    dblWeight_Max = dtManifest.Rows(0)("Weight_Vehicle")
                End If
                If IsNumeric(dtManifest.Rows(0)("Volume_Vehicle")) Then
                    dblVolume_Max = dtManifest.Rows(0)("Volume_Vehicle")
                End If


            End If



            'ประเภทรถ

            Dim oVehicleType As New ms_VehicleType(ms_VehicleType.enuOperation_Type.SEARCH)
            oVehicleType.SearchVehicleType("", "", cboVehicleType.SelectedValue.ToString)
            For Each drVehicleType As DataRow In oVehicleType.GetDataTable.Rows
                If dblWeight_Max = 0 Then
                    If IsNumeric(drVehicleType("Weight")) Then
                        dblWeight_Max = drVehicleType("Weight")
                    End If
                End If
                If dblVolume_Max = 0 Then
                    If IsNumeric(drVehicleType("Volume")) Then
                        dblVolume_Max = drVehicleType("Volume")
                    End If
                End If
            Next

            If Not _BoolLoad Then
                If IsNumeric(Me.txtSumDoc.Text) Then
                    If (CDbl(Me.txtSumWeight.Text)) > CDbl(dblWeight_Max) Then
                        If W_MSG_Confirm(Me.lblSumWt.Text & "เกินพิกัด" & Chr(13) & "ต้องการดำเนินการต่อใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                            boolLimit = False
                        End If
                    End If
                    If (CDbl(Me.txtSumVolume.Text)) > CDbl(dblVolume_Max) Then
                        If W_MSG_Confirm(Me.lblSumVolume.Text & "เกินพิกัด" & Chr(13) & "ต้องการดำเนินการต่อใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                            boolLimit = False
                        End If
                    End If

                End If
            End If
            If boolLimit Then
                Me.txtWeight_Limit.Text = Format(dblWeight_Max, "#,##0.0000")
                Me.txtVolume_Limit.Text = Format(dblVolume_Max, "#,##0.0000")
            Else
                Me.txtWeight_Limit.Text = Format(0, "#,##0.0000")
                Me.txtVolume_Limit.Text = Format(0, "#,##0.0000")
            End If


            Return boolLimit

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSeachSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachSupplier.Click
        Try
            'Dim frm As New frmMainTransport
            Dim frm As New WMS_STD_OUTB_Transport.frmVehicle_Popup
            frm.ShowDialog()

            If frm.Vehicle_Index <> "" Then
                Dim objDriver As New x_TransportManifest_Driver(x_TransportManifest_Driver.enuOperation_Type.ADDNEW)
                objDriver.TransportManifest_Index = Me._TransportManifest_Index

                If Me.getVehicle_OnTruck(frm.Vehicle_Index) = False Then
                    Me.txtVehicleID.Text = ""
                    Me.txtVehicleID.Tag = ""
                    txtVehicleNo.Text = ""
                    objDriver.DeleteDriver_Manifest()
                    Me.getComboDriver()
                    Exit Sub
                End If
                'Insert New Vehicle
                objDriver.TransportManifest_Index = Me._TransportManifest_Index
                objDriver.DeleteDriver_Manifest()

                Dim objchkVehicle As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
                Dim oVehicle As New WMS_STD_OUTB_Transport.ms_Vehicle(WMS_STD_OUTB_Transport.ms_Vehicle.enuOperation_Type.SEARCH)
                oVehicle.SearchData_Click("", " and Vehicle_Index = '" & frm.Vehicle_Index & "' ")
                objDriver.TransportManifest_Index = Me._TransportManifest_Index
                objDriver.Driver_Index = oVehicle.GetDataTable.Rows(0).Item("Driver_Index").ToString
                objDriver.isSubDriver = False
                objDriver.Seq = cboDriver.Items.Count
                objDriver.Insert()
                Me.getComboDriver()

            Else
                Me.txtVehicleID.Text = ""
                Me.txtVehicleID.Tag = ""
                Me.txtVehicleNo.Text = ""
                Me.txtWeight_Limit.Text = "0.0000"
                Me.txtVolume_Limit.Text = "0.0000"
            End If

            frm.Close()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub cboRoute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectedIndexChanged
        Try
            getComboSubRoute(cboRoute.SelectedValue.ToString)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            'Dim dr As DataRow = objDT.NewRow
            'dr("Description") = "รายงานคิดเงิน/กล่อง"
            'dr("Report_Name") = "rptSumBox"
            'objDT.Rows.Add(dr)

            ' ***** Using add comboboxcolumn *****
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
                .DataSource = objDT
            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            'Dim oconfig_Report As New config_Report
            'Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Try
                Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
                Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And TransportManifest_Index ='" & Me._TransportManifest_Index & "'")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()


                'If Report_Name = "rptSumBox" Then
                '    Dim obj As New frmReportViewerMain
                '    obj.TransportManifest_No = txtTransportManifest_No.Text.Trim
                '    obj.Report_Name = Report_Name
                '    obj.ShowDialog()
                'Else
                '    Dim frm As New frmReportMainTransportManifest_Update
                '    frm.Report_Name = Report_Name
                '    frm.Document_No = txtTransportManifest_No.Text.Trim
                '    frm.Document_Index = _TransportManifest_Index
                '    'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And TransportManifest_Index ='" & _TransportManifest_Index & "'")
                '    frm.ShowDialog()

                'End If


                '###################################
            Catch ex As Exception
                W_MSG_Error(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
    '    Try

    '        Dim oconfig_Report As New config_Report
    '        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

    '        Dim SubDriver_Name1 As String = ""
    '        Dim SubDriver_Name2 As String = ""
    '        Dim SubDriver_Name3 As String = ""

    '        Try

    '            Dim frm As New frmReportMainTransportManifest
    '            frm.Report_Name = Report_Name
    '            frm.Document_No = Me.txtTransportManifest_No.Text
    '            frm.Document_Index = Me._TransportManifest_Index
    '            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument


    '            cry = oconfig_Report.GetReportInfo(Report_Name, "And TransportManifest_Index ='" & _TransportManifest_Index & "'")

    '            Dim objManifest_Driver As New x_TransportManifest_Driver
    '            Dim odtManifest_Driver As DataTable

    '            objManifest_Driver.getTransportManifest_Driver(_TransportManifest_Index, 1)
    '            odtManifest_Driver = objManifest_Driver.GetDataTable

    '            If odtManifest_Driver.Rows.Count > 0 Then
    '                Select Case odtManifest_Driver.Rows.Count
    '                    Case 1
    '                        SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
    '                    Case 2
    '                        SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
    '                        SubDriver_Name2 = odtManifest_Driver.Rows(1).Item("Driver_name")

    '                    Case 3
    '                        SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
    '                        SubDriver_Name2 = odtManifest_Driver.Rows(1).Item("Driver_name")
    '                        '  SubDriver_Name3 = odtManifest_Driver.Rows(2).Item("Driver_name")

    '                    Case Else
    '                        SubDriver_Name1 = odtManifest_Driver.Rows(0).Item("Driver_name")
    '                        SubDriver_Name2 = odtManifest_Driver.Rows(1).Item("Driver_name")
    '                        ' SubDriver_Name3 = odtManifest_Driver.Rows(2).Item("Driver_name")

    '                End Select
    '            End If

    '            cry.SetParameterValue("SubDriver_Name1", SubDriver_Name1.ToString)
    '            cry.SetParameterValue("SubDriver_Name2", SubDriver_Name2.ToString)
    '            ' cry.SetParameterValue("SubDriver_Name3", SubDriver_Name3.ToString)

    '            'frm.CrystalReportViewer1.ReportSource = cry
    '            frm.ShowDialog()


    '        Catch ex As Exception
    '            W_MSG_Error(ex.Message)
    '        Finally
    '        End Try

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnMoveItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMoveManifestItem.Click
        Try
            If grdTrasportManifestItem.RowCount = 0 Then Exit Sub
            Dim dtpValue As New DataTable
            dtpValue = CType(grdTrasportManifestItem.DataSource, DataTable).Clone
            Dim drJobLoading() As DataRow
            Dim odrValue As DataRow
            drJobLoading = CType(grdTrasportManifestItem.DataSource, DataTable).Select("chkItemOnTruck=1", "chkItemOnTruck")
            If drJobLoading.Length = 0 Then
                W_MSG_Information("กรุณาเลือกรายการที่สามารถย้ายได้")
                Exit Sub
            End If
            ' If W_MSG_Confirm("คุณต้องการย้ายรายการใบคุม ใช่หรือไม่") = Windows.Forms.DialogResult.Yes = True Then
            For Each odr As DataRow In drJobLoading
                odrValue = dtpValue.NewRow
                odrValue.ItemArray = odr.ItemArray.Clone
                dtpValue.Rows.Add(odrValue)
            Next

            Dim frm As New frmTransportManifest_MoveItemSelect
            frm.IsSubManifest = Me._IsSubManifest
            frm.TransportManifest_Index = Me._TransportManifest_Index
            frm.dtJobLoading = dtpValue
            frm.ShowDialog()
            If frm.boolSelect Then
                Me.getTransportManifest_Item()
                ''ค่าขนส่งขาจ่าย
                'Me.getTransportPayment()
                'Me.CallculateTransportPayment()
                ''ค่าขนส่งขารับ
                'Me.getTransportCharge()
                'Me.CallculateTransportChage()
                Me.getTransportCharge_Data()
                Me.getTransportPayment_Data()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub btnAddManifestItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddManifestItem.Click
        Try
            Dim frm As New frmTransportBillLoad_Update(frmTransportBillLoad_Update.Manifest_Mode.EDIT)
            frm.IsSubManifest = _IsSubManifest
            frm.ShowDialog()
            setDataSoucreManifestItem(frm.GetDataTable)
            CType(grdTrasportManifestItem.DataSource, DataTable).AcceptChanges()
            setRowColorAndCalcurate()
            SetRow_Number_grdTrasportManifestItem()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub setRowColorAndCalcurate()
        Try
            Dim iSumDoc As Integer = 0
            Dim dblSumQty As Double = 0.0
            Dim dblSumWeight As Double = 0.0
            Dim dblSumVolume As Double = 0.0

            If grdTrasportManifestItem.RowCount = 0 Then Exit Sub

            For iRow As Integer = 0 To grdTrasportManifestItem.RowCount - 1
                If grdTrasportManifestItem.RowCount = 0 Then Exit Sub
                Select Case grdTrasportManifestItem.Rows(iRow).Cells("col_Process_Id").Value
                    Case Nothing
                        grdTrasportManifestItem.Rows(iRow).Cells("col_SO_No").Style.BackColor = Color.YellowGreen
                        grdTrasportManifestItem.Rows(iRow).Cells("col_DocumentType_Des_Item").Style.BackColor = Color.YellowGreen
                    Case "10"
                        grdTrasportManifestItem.Rows(iRow).Cells("col_SO_No").Style.BackColor = Color.YellowGreen
                        grdTrasportManifestItem.Rows(iRow).Cells("col_DocumentType_Des_Item").Style.BackColor = Color.YellowGreen
                    Case "25"
                        grdTrasportManifestItem.Rows(iRow).Cells("col_SO_No").Style.BackColor = Color.SkyBlue
                        grdTrasportManifestItem.Rows(iRow).Cells("col_DocumentType_Des_Item").Style.BackColor = Color.SkyBlue
                    Case "27"
                        grdTrasportManifestItem.Rows(iRow).Cells("col_SO_No").Style.BackColor = Color.Orange
                        grdTrasportManifestItem.Rows(iRow).Cells("col_DocumentType_Des_Item").Style.BackColor = Color.Orange
                End Select
                iSumDoc = grdTrasportManifestItem.RowCount
                dblSumQty += CDbl(grdTrasportManifestItem.Rows(iRow).Cells("col_Qty_Shipped").Value)
                dblSumWeight += CDbl(grdTrasportManifestItem.Rows(iRow).Cells("col_Weight_Shipped").Value)
                dblSumVolume += CDbl(grdTrasportManifestItem.Rows(iRow).Cells("col_Volume_Shipped").Value)
            Next

            txtSumDoc.Text = Format(iSumDoc, "#,##0")
            txtSumQty.Text = Format(dblSumQty, "#,##0.00")
            txtSumWeight.Text = Format(dblSumWeight, "#,##0.0000")
            txtSumVolume.Text = Format(dblSumVolume, "#,##0.0000")

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub setDataSoucreManifestItem(ByVal dtManifestItem As DataTable)
        Try
            If dtManifestItem Is Nothing Then Exit Sub
            With dtManifestItem.Columns
                If Not .Contains("Qty_Shipped") Then
                    dtManifestItem.Columns.Add(New DataColumn("Qty_Shipped", GetType(Double)))
                End If
                If Not .Contains("Weight_Shipped") Then
                    dtManifestItem.Columns.Add(New DataColumn("Weight_Shipped", GetType(Double)))
                End If
                If Not .Contains("Volume_Shipped") Then
                    dtManifestItem.Columns.Add(New DataColumn("Volume_Shipped", GetType(Double)))
                End If
                If Not .Contains("Value_Shipped") Then
                    dtManifestItem.Columns.Add(New DataColumn("Value_Shipped", GetType(Double)))
                End If
                If Not .Contains("SaleOrders_StatusDesc") Then
                    dtManifestItem.Columns.Add(New DataColumn("SaleOrders_StatusDesc", GetType(String)))
                End If
                If Not .Contains("Status_Manifest_Desc") Then
                    dtManifestItem.Columns.Add(New DataColumn("Status_Manifest_Desc", GetType(String)))
                End If
                If Not .Contains("TransportManifestItem_Index") Then
                    dtManifestItem.Columns.Add(New DataColumn("TransportManifestItem_Index", GetType(String)))
                End If
                If Not .Contains("IsSendOrPickup") Then
                    dtManifestItem.Columns.Add(New DataColumn("IsSendOrPickup", GetType(Integer)))
                End If
            End With

            If Me.grdTrasportManifestItem.DataSource Is Nothing Then
                For Each odr As DataRow In dtManifestItem.Rows
                    odr("TransportManifestItem_Index") = ""
                    odr("Qty_Shipped") = odr("Qty")
                    odr("Weight_Shipped") = odr("Weight")
                    odr("Volume_Shipped") = odr("Volume")
                    odr("Value_Shipped") = odr("Amount")
                    odr("SaleOrders_StatusDesc") = odr("Description")
                    odr("Status_Manifest_Desc") = odr("Status_Manifest_Desc")

                    Select Case odr("Process_Id")
                        Case 10
                            odr("IsSendOrPickup") = 1
                        Case Else
                            odr("IsSendOrPickup") = 2
                    End Select

                Next
                Me.grdTrasportManifestItem.DataSource = dtManifestItem
            Else
                Dim odtTempItemLocation As New DataTable
                odtTempItemLocation = Me.grdTrasportManifestItem.DataSource
                For Each odrTemp As DataRow In dtManifestItem.Rows
                    odrTemp("TransportManifestItem_Index") = ""
                    odrTemp("Qty_Shipped") = odrTemp("Qty")
                    odrTemp("Weight_Shipped") = odrTemp("Weight")
                    odrTemp("Volume_Shipped") = odrTemp("Volume")
                    odrTemp("Value_Shipped") = odrTemp("Amount")
                    odrTemp("SaleOrders_StatusDesc") = odrTemp("Description")
                    odrTemp("Status_Manifest_Desc") = odrTemp("Status_Manifest_Desc")

                    'Select Case odrTemp("Process_Id")
                    '    Case 10
                    '        odrTemp("IsSendOrPickup") = 1
                    '    Case Else
                    '        odrTemp("IsSendOrPickup") = 2
                    'End Select

                    ' Update Art 05-09-2012
                    ' Detail : เรื่องการเลือกว่าไปรับไปส่งตามประเภทเอกสาร
                    '    Dim objIsSendOrPickup As New ManifestTransaction_Update_Update(ManifestTransaction_Update_Update.enuOperation_Type.SEARCH)
                    Dim objIsSendOrPickup As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)

                    odrTemp("IsSendOrPickup") = objIsSendOrPickup.GetIsSendOrRecieve_Value(odrTemp("Process_Id"), odrTemp("DocumentType_Id"))

                Next

                dtManifestItem.Merge(odtTempItemLocation)
                Me.grdTrasportManifestItem.DataSource = dtManifestItem

            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function GetIsSendORRecieve() As Integer
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Private Sub btnDelManifestItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelManifestItem.Click
        Try
            CType(grdTrasportManifestItem.DataSource, DataTable).AcceptChanges()
            Dim drArrDelete() As DataRow = CType(grdTrasportManifestItem.DataSource, DataTable).Select("chkItemOnTruck=1")
            If drArrDelete.Length = 0 Then Exit Sub

            If Not Check_SaleOrders_Status() Then
                W_MSG_Information("ใบคุมนี้ไม่สามารถแก้ไข หรือยกเลิกได้ ")
                Exit Sub
            End If

            If W_MSG_Confirm("คุณต้องการลบรายการนี้ ใช่หรือไม่") = Windows.Forms.DialogResult.Yes = True Then
                Dim oManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.DELETE)

                For Each drDelete As DataRow In drArrDelete
                    oManifest.TransportManifestItem_Index = drDelete("TransportManifestItem_Index").ToString
                    oManifest.TransportManifest_Index = Me.TransportManifest_Index
                    oManifest.Delete(ManifestTransaction_Update.enuDelete.DELETEITEM)
                Next
                Me.getTransportManifest_Item()
                Me.setRowColorAndCalcurate()
                ''ค่าขนส่งขาจ่าย
                'Me.getTransportPayment()
                'Me.CallculateTransportPayment()
                ''ค่าขนส่งขารับ
                'Me.getTransportCharge()
                'Me.CallculateTransportChage()
                SetRow_Number_grdTrasportManifestItem()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub txtWorker_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWorker.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(sender, e.KeyChar)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()

            If frm.Customer_Index <> "" Then
                txtCustomer_Id.Text = frm.strCustomer_Name_Id
                txtCustomer_Name.Text = frm.customerName
                txtCustomer_Id.Tag = frm.Customer_Index
            Else
                txtCustomer_Id.Text = ""
                txtCustomer_Name.Text = ""
                txtCustomer_Id.Tag = ""
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnCarrier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarrier.Click
        Try
            Dim frm As New frmCarrier_PopUp
            frm.ShowDialog()
            Dim tmpCarrier_Index As String = ""
            tmpCarrier_Index = frm.Carrier_Index

            'If tmpCarrier_Index = "" Then
            '    Exit Sub
            'End If

            If Not tmpCarrier_Index = "" Then
                Me.txtCarrier_ID.Tag = tmpCarrier_Index
                Me.getCarrier()

            Else
                Me.txtCarrier_ID.Tag = ""
                Me.txtCarrier_ID.Text = ""
                Me.txtCarrier_Name.Text = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getCarrier()
        Dim objms_Carrier As New WMS_STD_OUTB_Transport.ms_Carrier(WMS_STD_OUTB_Transport.ms_Carrier.enuOperation_Type.SEARCH)
        Dim objDTms_Carrier As DataTable = New DataTable

        Try
            objms_Carrier.getPopup_Carrier("Carrier_Index", Me.txtCarrier_ID.Tag.ToString)
            objDTms_Carrier = objms_Carrier.DataTable
            If objDTms_Carrier.Rows.Count > 0 Then
                Me.txtCarrier_ID.Tag = objDTms_Carrier.Rows(0).Item("Carrier_Index").ToString
                Me.txtCarrier_ID.Text = objDTms_Carrier.Rows(0).Item("Carrier_Id").ToString
                Me.txtCarrier_Name.Text = objDTms_Carrier.Rows(0).Item("Description").ToString
            Else
                Me.txtCarrier_ID.Tag = ""
                Me.txtCarrier_ID.Text = ""
                Me.txtCarrier_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Carrier = Nothing
            objDTms_Carrier = Nothing
        End Try
    End Sub

    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                If Me.txtCustomer_Id.Tag Is Nothing Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If
                'Else
                '    Me._Customer_Index = ""
            End If

            Dim frm As New frmConsignee_Popup
            frm.Customer_Index = Me.txtCustomer_Id.Tag
            frm.ShowDialog()

            Dim tmp_Index As String = ""
            tmp_Index = frm.Consignee_Index

            If Not tmp_Index = "" Then
                Me.txtConsignee_Id.Tag = tmp_Index
                txtConsignee_Id.Text = frm.Consignee_ID
                txtConsignee_Name.Text = frm.Consignee_Name

            Else
                Me.txtConsignee_Id.Tag = ""
                txtConsignee_Id.Text = ""
                txtConsignee_Name.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomer_Shipping_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Shipping_Location.Click
        'TODO: HARDCODE-MSG
        Try
            Dim strWhere As String = ""
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION") Then
                If Me.txtConsignee_Id.Text = "" Then
                    W_MSG_Information("กรุณาเลือก" & lblConsignee.Text & "ก่อน")
                    Exit Sub
                End If
                strWhere = " and  Str1 = '" & txtConsignee_Id.Text & "'"
            Else
                strWhere = ""
            End If

            Dim frm As New frmCus_Ship_Location_Popup
            frm.strAddStrWhere = strWhere
            frm.ShowDialog()

            Dim tmpCustomer_Shipping_Location_Index As String = ""
            tmpCustomer_Shipping_Location_Index = frm.Customer_Shipping_Location_Index

            If tmpCustomer_Shipping_Location_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Shipping_Location_Index = "" Then
                Me.txtShipping_Location_ID.Tag = tmpCustomer_Shipping_Location_Index
                Me.getCus_Shipping_Location_Index()
            Else
                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
            End If
            If Me.txtShipping_Location_ID.Text = "" Then
                Me.txtShipping_Location_ID.Tag = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getCus_Shipping_Location_Index()
        Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDTms_Shipping_Location As DataTable = New DataTable
        Dim _Postcode As String = ""
        Try
            objms_Shipping_Location.getCus_Ship_Locartion_Search("Customer_Shipping_Location_Index", Me.txtShipping_Location_ID.Tag.ToString)
            objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
            If objDTms_Shipping_Location.Rows.Count > 0 Then

                Me.txtShipping_Location_ID.Tag = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                'Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Company_Name").ToString
                'Me.txtShip_Address.Text = objDTms_Shipping_Location.Rows(0).Item("Address").ToString
                Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Shipping_Location_Name").ToString
                'Me.txtShip_Address.Text = objDTms_Shipping_Location.Rows(0).Item("AddressShipping_Location").ToString

                'Me.txtShip_Phone.Text = objDTms_Shipping_Location.Rows(0).Item("Tel").ToString
                'Me.txtShip_Fax.Text = objDTms_Shipping_Location.Rows(0).Item("Fax").ToString
                _Postcode = objDTms_Shipping_Location.Rows(0).Item("Postcode").ToString
                'If _Postcode <> "" Then
                '    Dim oSalesOrder As New tb_SalesOrder
                '    Dim dtSalesOrder As New DataTable
                '    oSalesOrder.GetSubRouteByPostcode(_Postcode)
                '    dtSalesOrder = oSalesOrder.GetDataTable
                '    If dtSalesOrder.Rows.Count > 0 Then
                '        'dong_kk update
                '        cboRoute.SelectedValue = dtSalesOrder.Rows(0).Item("Route_Index").ToString
                '        cboSubRoute.SelectedValue = dtSalesOrder.Rows(0).Item("SubRoute_Index").ToString
                '        'cboSubRoute.SelectedValue = oSalesOrder.GetSubRouteByPostcode(_Postcode)
                '    End If
                '    oSalesOrder = Nothing
                'End If
            Else
                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
                'Me.txtShip_Address.Text = ""
                'Me.txtShip_Phone.Text = ""
                'Me.txtShip_Fax.Text = ""
                _Postcode = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Shipping_Location = Nothing
            objDTms_Shipping_Location = Nothing
        End Try
    End Sub
    '#Region "Key Number In Datagrid"
    '    ''' <summary>
    '    ''' 
    '    ''' </summary>
    '    ''' <param name="sender"></param>
    '    ''' <param name="e"></param>
    '    ''' <remarks>
    '    ''' Add Date : 20/10/2011
    '    ''' Add By   : Dong_kk
    '    ''' Add For  : Key In Double 
    '    ''' </remarks>
    '    Private Sub grdTransportRegion_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTransportCharge.EditingControlShowing
    '        Try
    '            ' Dong_kk 
    '            '***************เปิดใช้ keyPress ของ grdcell*****************
    '            Dim strName As String = grdTransportCharge.Columns(grdTransportCharge.CurrentCell.ColumnIndex).Name
    '            If (strName = "col_Amount") Then
    '                '---add an event handler to the TextBox control--- 
    '                Dim tb As TextBox = CType(e.Control, TextBox)
    '                AddHandler tb.KeyPress, AddressOf TextBox_KeyPress
    '                AddHandler tb.TextChanged, AddressOf Textbox_TextChanged
    '            End If
    '        Catch ex As Exception
    '            W_MSG_Error(ex.Message)
    '        End Try
    '    End Sub

    '    Private Sub TextBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
    '        If Char.IsDigit(e.KeyChar) Or Asc(e.KeyChar) = 8 Or Asc(e.KeyChar) = 46 Then
    '            If Asc(e.KeyChar) = 46 Then
    '                If CType(sender, TextBox).Text.Contains(Chr(46)) Then
    '                    e.Handled = True
    '                Else
    '                    e.Handled = False
    '                End If
    '            Else
    '                e.Handled = False
    '            End If
    '        Else
    '            e.Handled = True
    '        End If
    '    End Sub
    '    Private Sub Textbox_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)

    '        'If (Me.grdTransportRegion.Columns("col_Amount").HeaderText = "Percentage") Or (Me.grdTransportRegion.Columns("col_Amount").HeaderText = "Value") Then
    '        Dim Column_Index As String = grdTransportCharge.CurrentCell.ColumnIndex
    '        Select Case Column_Index
    '            Case Is = grdTransportCharge.Columns("col_Amount").Index
    '                If CDec(CType(sender, TextBox).Text.Trim = ".") Then
    '                    W_MSG_Error(". has to appear after an interger")
    '                    CType(sender, TextBox).Text = "0"
    '                    CType(sender, TextBox).Focus()
    '                End If
    '                If Not CType(sender, TextBox).Text.Trim.Length = 0 Then
    '                    If CDec(CType(sender, TextBox).Text >= 100) Then
    '                        W_MSG_Error("Percentage cannot exceeds 100")
    '                        CType(sender, TextBox).Text = "0"
    '                    End If
    '                End If
    '        End Select


    '    End Sub


    '#End Region

    Private Sub btnTransportCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransportCharge.Click
        Try
            'ค่าขนส่งขาจ่าย
            Me.getTransportPayment()
            Me.CallculateTransportPayment()
            'ค่าขนส่งขารับ
            Me.getTransportCharge()
            Me.CallculateTransportChage()

            'Get Region minimum Rate.
            Me.getComboRegion_Charge()


            'Me.txtSumPrice_In.Text = txtSumPrice_In.Tag

            'Me.txtSumPrice_Out.Text = txtSumPrice_Out.Tag

            If IsNumeric(Me.txtSumPrice_In.Text) Then
                If CDbl(Me.txtSumPrice_In.Text) > 0 Then
                    Me.chkTransportCharged.Checked = True
                Else
                    Me.chkTransportCharged.Checked = False
                End If
            End If
            If IsNumeric(Me.txtSumPrice_Out.Text) Then
                If CDbl(Me.txtSumPrice_Out.Text) > 0 Then
                    Me.chkTransportPaid.Checked = True
                Else
                    Me.chkTransportPaid.Checked = False
                End If
            End If

            Me.btnTransportCharge.Enabled = False
            Me.btnClearTransportCharge.Enabled = True
            SetRow_Number_grdTrasportManifestItem()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub getComboRegion_Charge()
        Dim objDT As DataTable = New DataTable
        Try
            objDT.Columns.Add("Description", GetType(String))
            objDT.Columns.Add("TransportRegion_Index", GetType(String))

            If Me.grdTransportCharge.Rows.Count > 0 Then
                For Each drCharge As DataRow In CType(Me.grdTransportCharge.DataSource, DataTable).Rows
                    Dim drNew As DataRow
                    drNew = objDT.NewRow
                    drNew("Description") = drCharge("TransportRegion_desc")
                    drNew("TransportRegion_Index") = drCharge("TransportRegion_Index")
                    objDT.Rows.Add(drNew)
                Next
            End If

            Dim drSelect() As DataRow
            drSelect = Nothing
            If Me.grdTransportPayment.Rows.Count > 0 Then
                For Each drCharge As DataRow In CType(Me.grdTransportPayment.DataSource, DataTable).Rows
                    If objDT.Rows.Count > 0 Then drSelect = objDT.Select("TransportRegion_Index='" & drCharge("TransportRegion_Index") & "'")
                    If drSelect.Length = 0 Then
                        Dim drNew As DataRow
                        drNew = objDT.NewRow
                        drNew("Description") = drCharge("TransportRegion_desc")
                        drNew("TransportRegion_Index") = drCharge("TransportRegion_Index")
                        objDT.Rows.Add(drNew)
                    End If
                Next
            End If

            cboRegion.BeginUpdate()
            With cboRegion
                .DisplayMember = "Description"
                .ValueMember = "TransportRegion_Index"
                .DataSource = objDT
            End With

            cboRegion.EndUpdate()
            If cboRegion.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            'objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub


#Region "   TransportCharge   "

    Private Sub getTransportCharge()
        Try
            Dim oManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
            Dim dtManifest As New DataTable
            'oManifest.getCustomer_ShippingDrop_Region(Me._TransportManifest_Index)
            'dtManifest = oManifest.GetDataTable
            'Me.grdCustomer_Shipping.DataSource = dtManifest

            'Select Case objStatus
            '    Case Manifest_Mode.ADD
            '        Dim pstrSalesOrder_Indexs As String = ""
            '        For Each drSales As DataRow In Me._dataTable.Rows
            '            pstrSalesOrder_Indexs &= " '" & drSales("SalesOrder_Index").ToString & "'"
            '            pstrSalesOrder_Indexs &= ","
            '        Next
            '        pstrSalesOrder_Indexs = pstrSalesOrder_Indexs.Remove(pstrSalesOrder_Indexs.Length - 1, 1)

            '        oManifest.getCustomer_ShippingDrop_Region_SalesOrder(pstrSalesOrder_Indexs)
            '        dtManifest = oManifest.GetDataTable
            '        Me.grdCustomer_Shipping.DataSource = dtManifest
            '        'Mapping ประเภทการจัดส่ง,เจ้าจัดส่ง
            '        'For Each drMapping As DataRow In dtManifest.Rows
            '        '    drMapping("Carrier_Index") = Me.txtCarrier_ID.Tag
            '        '    drMapping("Carrier_Id") = Me.txtCarrier_ID.Text
            '        '    drMapping("Carrier_Name") = Me.txtCarrier_Name.Text
            '        'Next
            '        'If Me.txtCarrier_ID.Tag.ToString = "" Then
            '        '    W_MSG_Information(Me.chkTransportPaid.Text & " ต้องระบุ " & Me.lblCarrier.Text)
            '        '    Me.txtCarrier_Name.Focus()
            '        'End If

            '    Case Else
            Dim pstrSalesOrder_Indexs As String = ""
            Dim dtManifestItem As New DataTable
            CType(Me.grdTrasportManifestItem.DataSource, DataTable).AcceptChanges()
            dtManifestItem = Me.grdTrasportManifestItem.DataSource
            For Each drSales As DataRow In dtManifestItem.Rows
                pstrSalesOrder_Indexs &= " '" & drSales("SalesOrder_Index").ToString & "'"
                pstrSalesOrder_Indexs &= ","
            Next
            pstrSalesOrder_Indexs = pstrSalesOrder_Indexs.Remove(pstrSalesOrder_Indexs.Length - 1, 1)

            oManifest.getCustomer_ShippingDrop_Region_SalesOrder(pstrSalesOrder_Indexs)
            dtManifest = oManifest.GetDataTable
            Me.grdCustomer_Shipping.DataSource = dtManifest


            'oManifest.getCustomer_ShippingDrop_Region(Me._TransportManifest_Index)
            'dtManifest = oManifest.GetDataTable
            'Me.grdCustomer_Shipping.DataSource = dtManifest

            'End Select

            If dtManifest.Rows.Count > 0 Then
                Dim dtDrop As New DataTable
                Dim drArrDrop() As DataRow
                dtDrop.Columns.Add("Customer_Index", GetType(String))
                'dtDrop.Columns.Add("Customer_Shipping_Index", GetType(String))
                dtDrop.Columns.Add("Customer_Shipping_Location_Index", GetType(String))
                dtDrop.Columns.Add("TransportRegion_Index", GetType(String))
                dtDrop.Columns.Add("IsSendOrPickup", GetType(Integer))

                Dim dtRegion As New DataTable
                Dim drArrRegion() As DataRow
                dtRegion.Columns.Add("Customer_Index", GetType(String))
                dtRegion.Columns.Add("Customer_Id", GetType(String))
                dtRegion.Columns.Add("Customer_Name", GetType(String))
                dtRegion.Columns.Add("TransportRegion_Index", GetType(String))
                dtRegion.Columns.Add("TransportRegion_desc", GetType(String))
                dtRegion.Columns.Add("Description", GetType(String))
                dtRegion.Columns.Add("sumDrop", GetType(Double))
                dtRegion.Columns.Add("Total_Qty", GetType(Double))
                dtRegion.Columns.Add("Weight", GetType(Double))
                dtRegion.Columns.Add("Volume", GetType(Double))
                dtRegion.Columns.Add("Amount", GetType(Double))
                'dtRegion.Columns.Add("IsSendOrPickup", GetType(Integer))
                dtRegion.Columns.Add("Amount_Charge", GetType(Double))
                dtRegion.Columns.Add("Minimum_Rate", GetType(Double))

                Dim keys(1) As DataColumn
                keys(0) = dtRegion.Columns("Customer_Index")
                keys(1) = dtRegion.Columns("TransportRegion_Index")
                'keys(2) = dtRegion.Columns("IsSendOrPickup")
                dtRegion.PrimaryKey = keys

                Dim drNewRow As DataRow
                Dim strCondition As String = ""

                For Each drManifest As DataRow In dtManifest.Rows
                    'BEGIN : TransportCharge
                    drNewRow = dtRegion.NewRow
                    drNewRow("Customer_Index") = drManifest("Customer_Index").ToString
                    drNewRow("Customer_Id") = drManifest("Customer_Id").ToString
                    drNewRow("Customer_Name") = drManifest("Customer_Name").ToString
                    drNewRow("TransportRegion_Index") = drManifest("TransportRegion_Index").ToString
                    drNewRow("TransportRegion_desc") = drManifest("TransportRegion_desc").ToString
                    drNewRow("Description") = drManifest("Description").ToString
                    drNewRow("sumDrop") = 0
                    drNewRow("Total_Qty") = FormatNumber(0, 2)
                    drNewRow("Weight") = FormatNumber(0, 4)
                    drNewRow("Volume") = FormatNumber(0, 4)
                    drNewRow("Amount") = FormatNumber(0, 2)
                    drNewRow("Amount_Charge") = FormatNumber(0, 2)
                    drNewRow("Minimum_Rate") = FormatNumber(0, 2)
                    'Dim strCondition2 As String = "TransportRegion_Index='" & drManifest("TransportRegion_Index").ToString & "' AND Customer_Index='" & drManifest("Customer_Index").ToString & "' AND IsSendOrPickup='" & drManifest("IsSendOrPickup").ToString & "'"
                    strCondition = "TransportRegion_Index='" & drManifest("TransportRegion_Index").ToString & "' AND Customer_Index='" & drManifest("Customer_Index").ToString & "'"

                    drArrRegion = dtRegion.Select(strCondition)
                    If drArrRegion.Length = 0 Then
                        dtRegion.Rows.Add(drNewRow)
                        Dim drCurrent As DataRow
                        Dim key(1) As Object
                        key(0) = drManifest("Customer_Index").ToString
                        key(1) = drManifest("TransportRegion_Index").ToString
                        'key(2) = drManifest("IsSendOrPickup").ToString
                        drCurrent = dtRegion.Rows.Find(key)

                        drCurrent.BeginEdit()
                        drCurrent("Total_Qty") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("sum(Total_Qty)", strCondition), GetType(Double))
                        drCurrent("Weight") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("sum(Weight)", strCondition), GetType(Double))
                        drCurrent("Volume") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("sum(Volume)", strCondition), GetType(Double))
                        'Callculate Drop
                        'Dim drArrDrop_Ship() As DataRow
                        'CType(Me.grdTrasportManifestItem.DataSource, DataTable).AcceptChanges()
                        'drArrDrop_Ship = CType(Me.grdTrasportManifestItem.DataSource, DataTable).Select(strCondition & " AND IsSendOrPickup=1")
                        'If drArrDrop_Ship.Length > 0 Then
                        '    drCurrent("sumDrop") = 0
                        '    drCurrent("sumDrop2") = 0
                        'End If
                        drCurrent("sumDrop") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("count(Customer_Shipping_Location_Index)", strCondition), GetType(Double))

                        drCurrent.EndEdit()

                    End If

                    'Drop
                    drNewRow = dtDrop.NewRow
                    drNewRow("Customer_Index") = drManifest("Customer_Index")
                    drNewRow("TransportRegion_Index") = drManifest("TransportRegion_Index")
                    drNewRow("Customer_Shipping_Location_Index") = drManifest("Customer_Shipping_Location_Index")
                    'drNewRow("IsSendOrPickup") = drManifest("IsSendOrPickup")
                    drArrDrop = dtDrop.Select(strCondition & " AND Customer_Shipping_Location_Index='" & drManifest("Customer_Shipping_Location_Index") & "'")
                    If drArrDrop.Length = 0 Then
                        dtDrop.Rows.Add(drNewRow)
                    End If

                    'END : TransportCharge

                Next


                'Me.lblTransportChargeSumDrop.Text = dtDrop.Rows.Count.ToString & Me.lblTransportChargeSumDrop.Tag
                'Me.lblTransportChargeSumTranportRegion.Text = dtRegion.Rows.Count.ToString & Me.lblTransportChargeSumTranportRegion.Tag
                Me.grdTransportCharge.DataSource = dtRegion

            Else

                'Me.lblTransportChargeSumDrop.Text = "0" & Me.lblTransportChargeSumDrop.Tag
                'Me.lblTransportChargeSumTranportRegion.Text = "0" & Me.lblTransportChargeSumTranportRegion.Tag

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CallculateTransportChage(Optional ByVal dblMinimum_Rate As Double = 0)
        Try
            'ขารับ
            If Me.grdTransportCharge.RowCount = 0 Then Exit Sub
            Dim dtDrop As New DataTable
            Dim dblSumIn As Double = 0
            CType(Me.grdTransportCharge.DataSource, DataTable).AcceptChanges()
            dtDrop = CType(Me.grdTransportCharge.DataSource, DataTable)
            For Each drDrop As DataRow In dtDrop.Rows
                Dim ReturnObject(2) As Object
                ReturnObject = Me.CalculateTransportCharge(drDrop("Customer_Index").ToString _
                                                                    , Me.cboTransportJobType.SelectedValue _
                                                                    , drDrop("TransportRegion_Index").ToString _
                                                                    , drDrop("sumDrop") _
                                                                    , drDrop("Total_Qty") _
                                                                    , drDrop("Weight") _
                                                                    , drDrop("Volume"))
                If dblMinimum_Rate > 0 Then
                    If dblMinimum_Rate > ReturnObject(0) Then ReturnObject(0) = dblMinimum_Rate
                End If
                drDrop("Amount") = ReturnObject(0)
                drDrop("Amount_Charge") = ReturnObject(0)
                drDrop("Description") = ReturnObject(1)
                drDrop("Minimum_Rate") = ReturnObject(2)
                dblSumIn += drDrop("Amount")
            Next


            'If CDbl(Me.txtSumPrice_In.Text) = 0 Then
            Me.txtSumPrice_In.Text = FormatNumber(dblSumIn, 2)
            'End If

            'Me.txtSumPrice_In.Tag = FormatNumber(dblSumIn, 2)

            'ขาจ่าย
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CalculateTransportCharge(ByVal pstrCustomer_Index As String, ByVal pstrTransportJobType_Index As String, ByVal pstrTransportRegion_Index As String _
    , ByVal intDrop As Integer _
    , ByVal pdblTotal_Qty As Double _
    , ByVal pdblWeight As Double _
    , ByVal pdblVolume As Double) As Object
        Try
            Dim dblAmount As Double = 0
            Dim osvarDropItem As New svar_TransportChargePerDropItem
            Dim dtDropItem As New DataTable
            Dim ReturnObject(2) As Object
            ReturnObject(0) = 0
            ReturnObject(1) = ""
            ReturnObject(2) = 0
            osvarDropItem.GetTransportChargePerItem(pstrCustomer_Index, pstrTransportJobType_Index, pstrTransportRegion_Index)
            dtDropItem = osvarDropItem.GetDataTable
            If dtDropItem.Rows.Count = 0 Then Return ReturnObject

            Dim dblValue As Double = 0
            Dim dblRatePerDrop As Double = 0
            'Dim oManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
            'Dim dtManifest As New DataTable
            'oManifest.getCustomer_ShippingDrop_Region(Me._TransportManifest_Index)
            'dtManifest = oManifest.GetDataTable
            'Me.grdCustomer_Shipping.DataSource = dtManifest
            CType(Me.grdCustomer_Shipping.DataSource, DataTable).AcceptChanges()
            Dim drArrManifest() As DataRow
            drArrManifest = CType(Me.grdCustomer_Shipping.DataSource, DataTable).Select("TransportRegion_Index='" & pstrTransportRegion_Index & "'")

            Dim boolCharge As Boolean = False
            For Each drCal As DataRow In dtDropItem.Rows
                Select Case drCal("TransportChargeGroup_Id")
                    Case 1 'คิดตามจำนวน Drop
                        If drCal("QtyDropEnd") = 0 Then
                            boolCharge = True
                        End If
                        If (intDrop >= drCal("QtyDropStart")) And (intDrop <= drCal("QtyDropEnd")) Then
                            boolCharge = True
                        End If
                        If Not boolCharge Then
                            Continue For
                        End If

                        For Each drManifest As DataRow In drArrManifest
                            drManifest("OverflowPerDrop") = CDbl(drCal("OverflowPerDrop"))
                            drManifest("OverflowRate") = CDbl(drCal("OverflowRate"))
                            drManifest("RateTransportPerDrop") = CDbl(drCal("RateTransportPerDrop"))
                            drManifest("Description") = drCal("Description").ToString
                            ReturnObject(2) = drCal("Minimum_Rate")
                            'Have Overflow
                            If (intDrop <= drCal("Minimum_Drop")) Then
                                drManifest("Rate") = drCal("RateTransportPerDrop")
                                drManifest("Description") = "Minimum Charge"
                                drCal("Description") = drManifest("Description")
                                dblAmount = drCal("Minimum_Rate")
                            Else
                                If drCal("isOverflow") Then
                                    Select Case drCal("OverflowPerDropUnit")
                                        Case 0 'Volume
                                            dblValue = CDbl(drManifest("Volume"))
                                        Case 1 'Kg
                                            dblValue = CDbl(drManifest("Weight"))
                                    End Select

                                    If dblValue >= CDbl(drCal("OverflowPerDrop")) Then
                                        dblRatePerDrop = dblValue * CDbl(drCal("OverflowRate"))
                                        drManifest("Description") &= " (Overflow)"
                                    Else
                                        dblRatePerDrop = CDbl(drCal("RateTransportPerDrop"))
                                    End If

                                    drManifest("Rate") = dblRatePerDrop
                                    dblAmount += dblRatePerDrop
                                Else
                                    'Don,t Have Overflow Normal Caculate (Drop*Rate)
                                    drManifest("Rate") = drCal("RateTransportPerDrop")
                                    dblAmount = intDrop * drCal("RateTransportPerDrop")
                                End If

                            End If

                        Next

                        'End If

                        ReturnObject(0) = dblAmount
                        ReturnObject(1) = drCal("Description").ToString
                        Return ReturnObject

                    Case 2 'คิดเหมาต่อเที่ยว
                        For Each drManifest As DataRow In drArrManifest
                            drManifest("OverflowPerDrop") = 0
                            drManifest("OverflowRate") = 0
                            drManifest("RateTransportPerDrop") = 0
                            drManifest("Description") = drCal("Description").ToString
                        Next
                        dblAmount = drCal("RateTransportPerTrip")
                        ReturnObject(0) = dblAmount
                        ReturnObject(1) = drCal("Description").ToString
                        Return ReturnObject
                    Case 3 'คิดจากต้นทุนการจ้างรถ
                        For Each drManifest As DataRow In drArrManifest
                            drManifest("OverflowPerDrop") = 0
                            drManifest("OverflowRate") = 0
                            drManifest("RateTransportPerDrop") = 0
                            drManifest("Description") = drCal("Description").ToString
                        Next
                        dblAmount = 0
                        If IsNumeric(Me.txtSumPrice_Out.Text) Then
                            dblAmount = ((CDbl(Me.txtSumPrice_Out.Text) * drCal("Percent_TransportChargeTopUp")) / 100) + CDbl(Me.txtSumPrice_Out.Text)
                        End If
                        ReturnObject(0) = dblAmount
                        ReturnObject(1) = drCal("Description").ToString
                        Return ReturnObject
                End Select

            Next

            Return ReturnObject

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

#Region "   TransportPayment   "
    Private Sub getTransportPayment()
        Try
            Dim oManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
            Dim dtManifest As New DataTable

            Dim pstrSalesOrder_Indexs As String = ""
            Dim dtManifestItem As New DataTable
            CType(Me.grdTrasportManifestItem.DataSource, DataTable).AcceptChanges()
            dtManifestItem = Me.grdTrasportManifestItem.DataSource

            'Select Case objStatus
            '    Case Manifest_Mode.ADD
            '        'Dim pstrSalesOrder_Indexs As String = ""
            '        For Each drSales As DataRow In Me._dataTable.Rows
            '            pstrSalesOrder_Indexs &= " '" & drSales("SalesOrder_Index").ToString & "'"
            '            pstrSalesOrder_Indexs &= ","
            '        Next
            '        pstrSalesOrder_Indexs = pstrSalesOrder_Indexs.Remove(pstrSalesOrder_Indexs.Length - 1, 1)
            '        oManifest.getCustomer_Carrier_Region_SalesOrder(pstrSalesOrder_Indexs)
            '        dtManifest = oManifest.GetDataTable
            '        Me.grdCarrier.DataSource = dtManifest
            '        'Mapping ประเภทการจัดส่ง,เจ้าจัดส่ง
            '        For Each drMapping As DataRow In dtManifest.Rows
            '            drMapping("Carrier_Index") = Me.txtCarrier_ID.Tag
            '            drMapping("Carrier_Id") = Me.txtCarrier_ID.Text
            '            drMapping("Carrier_Name") = Me.txtCarrier_Name.Text
            '        Next
            '        If Me.txtCarrier_ID.Tag.ToString = "" Then
            '            W_MSG_Information(Me.chkTransportPaid.Text & " ต้องระบุ " & Me.lblCarrier.Text)
            '            Me.txtCarrier_Name.Focus()
            '        End If
            '    Case Else

            For Each drSales As DataRow In dtManifestItem.Rows
                pstrSalesOrder_Indexs &= " '" & drSales("SalesOrder_Index").ToString & "'"
                pstrSalesOrder_Indexs &= ","
            Next
            pstrSalesOrder_Indexs = pstrSalesOrder_Indexs.Remove(pstrSalesOrder_Indexs.Length - 1, 1)
            oManifest.getCustomer_Carrier_Region_SalesOrder(pstrSalesOrder_Indexs)
            dtManifest = oManifest.GetDataTable
            Me.grdCarrier.DataSource = dtManifest
            'Mapping ประเภทการจัดส่ง,เจ้าจัดส่ง
            For Each drMapping As DataRow In dtManifest.Rows
                drMapping("Carrier_Index") = Me.txtCarrier_ID.Tag
                drMapping("Carrier_Id") = Me.txtCarrier_ID.Text
                drMapping("Carrier_Name") = Me.txtCarrier_Name.Text
            Next
            If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.txtCarrier_ID.Tag, GetType(String)) = "" Then
                W_MSG_Information(Me.chkTransportPaid.Text & " ต้องระบุ " & Me.lblCarrier.Text)
                Me.txtCarrier_Name.Focus()
            End If


            'oManifest.getCustomer_Carrier_Region(Me._TransportManifest_Index)
            'dtManifest = oManifest.GetDataTable
            'Me.grdCarrier.DataSource = dtManifest
            'End Select


            If dtManifest.Rows.Count > 0 Then
                Dim dtDrop As New DataTable
                Dim drArrDrop() As DataRow
                dtDrop.Columns.Add("Customer_Index", GetType(String))
                dtDrop.Columns.Add("Carrier_Index", GetType(String))
                'dtDrop.Columns.Add("Customer_Shipping_Index", GetType(String))
                dtDrop.Columns.Add("Customer_Shipping_Location_Index", GetType(String))
                dtDrop.Columns.Add("TransportRegion_Index", GetType(String))

                Dim dtRegion As New DataTable
                Dim drArrRegion() As DataRow
                dtRegion.Columns.Add("Customer_Index", GetType(String))
                dtRegion.Columns.Add("Customer_Id", GetType(String))
                dtRegion.Columns.Add("Customer_Name", GetType(String))
                dtRegion.Columns.Add("Carrier_Index", GetType(String))
                dtRegion.Columns.Add("Carrier_Id", GetType(String))
                dtRegion.Columns.Add("Carrier_Name", GetType(String))

                dtRegion.Columns.Add("TransportRegion_Index", GetType(String))
                dtRegion.Columns.Add("TransportRegion_desc", GetType(String))
                dtRegion.Columns.Add("Description", GetType(String))
                dtRegion.Columns.Add("sumDrop", GetType(Double))
                dtRegion.Columns.Add("Total_Qty", GetType(Double))
                dtRegion.Columns.Add("Weight", GetType(Double))
                dtRegion.Columns.Add("Volume", GetType(Double))
                dtRegion.Columns.Add("Amount", GetType(Double))
                dtRegion.Columns.Add("Amount_Charge", GetType(Double))
                dtRegion.Columns.Add("Minimum_Rate", GetType(Double))

                Dim keys(2) As DataColumn
                keys(0) = dtRegion.Columns("Customer_Index")
                keys(1) = dtRegion.Columns("Carrier_Index")
                keys(2) = dtRegion.Columns("TransportRegion_Index")
                dtRegion.PrimaryKey = keys

                Dim drNewRow As DataRow
                Dim strCondition As String = ""

                For Each drManifest As DataRow In dtManifest.Rows
                    'Region
                    drNewRow = dtRegion.NewRow
                    drNewRow("Customer_Index") = drManifest("Customer_Index").ToString
                    drNewRow("Customer_Id") = drManifest("Customer_Id").ToString
                    drNewRow("Customer_Name") = drManifest("Customer_Name").ToString

                    drNewRow("Carrier_Index") = drManifest("Carrier_Index").ToString
                    drNewRow("Carrier_Id") = drManifest("Carrier_Id").ToString
                    drNewRow("Carrier_Name") = drManifest("Carrier_Name").ToString

                    drNewRow("TransportRegion_Index") = drManifest("TransportRegion_Index").ToString
                    drNewRow("TransportRegion_desc") = drManifest("TransportRegion_desc").ToString
                    drNewRow("Description") = drManifest("Description")
                    drNewRow("sumDrop") = 0
                    drNewRow("Total_Qty") = FormatNumber(0, 2)
                    drNewRow("Weight") = FormatNumber(0, 4)
                    drNewRow("Volume") = FormatNumber(0, 4)
                    drNewRow("Amount") = FormatNumber(0, 2)
                    drNewRow("Amount_Charge") = FormatNumber(0, 2)
                    drNewRow("Minimum_Rate") = FormatNumber(0, 2)

                    strCondition = "TransportRegion_Index='" & drManifest("TransportRegion_Index") & "' AND Customer_Index='" & drManifest("Customer_Index") & "' AND Carrier_Index='" & drManifest("Carrier_Index") & "'"
                    drArrRegion = dtRegion.Select(strCondition)
                    If drArrRegion.Length = 0 Then
                        dtRegion.Rows.Add(drNewRow)
                        Dim drCurrent As DataRow
                        Dim key(2) As Object
                        key(0) = drManifest("Customer_Index").ToString
                        key(1) = drManifest("Carrier_Index").ToString
                        key(2) = drManifest("TransportRegion_Index").ToString
                        drCurrent = dtRegion.Rows.Find(key)

                        drCurrent.BeginEdit()
                        'drCurrent("Total_Qty") = dtManifest.Compute("sum(Total_Qty)", strCondition)
                        'drCurrent("Weight") = dtManifest.Compute("sum(Weight)", strCondition)
                        'drCurrent("Volume") = dtManifest.Compute("sum(Volume)", strCondition)
                        drCurrent("Total_Qty") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("sum(Total_Qty)", strCondition), GetType(Double))
                        drCurrent("Weight") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("sum(Weight)", strCondition), GetType(Double))
                        drCurrent("Volume") = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtManifest.Compute("sum(Volume)", strCondition), GetType(Double))
                        drCurrent("sumDrop") = dtManifest.Compute("count(Customer_Shipping_Location_Index)", strCondition)
                        drCurrent.EndEdit()

                    End If
                    'Drop
                    drNewRow = dtDrop.NewRow
                    drNewRow("Customer_Index") = drManifest("Customer_Index")
                    drNewRow("Carrier_Index") = drManifest("Carrier_Index")
                    'drNewRow("Customer_Shipping_Index") = drManifest("Customer_Shipping_Index")
                    drNewRow("Customer_Shipping_Location_Index") = drManifest("Customer_Shipping_Location_Index")

                    drNewRow("TransportRegion_Index") = drManifest("TransportRegion_Index")
                    'drArrDrop = dtDrop.Select(strCondition & " AND Customer_Shipping_Index='" & drManifest("Customer_Shipping_Index") & "'")
                    drArrDrop = dtDrop.Select(strCondition & " AND Customer_Shipping_Location_Index='" & drManifest("Customer_Shipping_Location_Index") & "'")

                    If drArrDrop.Length = 0 Then
                        dtDrop.Rows.Add(drNewRow)
                    End If


                Next

                Me.grdTransportPayment.DataSource = dtRegion

            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub CallculateTransportPayment(Optional ByVal dblMinimum_Rate As Double = 0)
        Try
            'ขารับ
            If Me.grdTransportPayment.RowCount = 0 Then Exit Sub
            Dim dtDrop As New DataTable
            Dim dblSumIn As Double = 0
            CType(Me.grdTransportPayment.DataSource, DataTable).AcceptChanges()
            dtDrop = CType(Me.grdTransportPayment.DataSource, DataTable)

            For Each drDrop As DataRow In dtDrop.Rows
                Dim ReturnObject(2) As Object
                ReturnObject = Me.CalculateTransportPayment(drDrop("Customer_Index").ToString _
                                                                    , drDrop("Carrier_Index").ToString _
                                                                    , Me.cboTransportJobType.SelectedValue _
                                                                    , drDrop("TransportRegion_Index").ToString _
                                                                    , drDrop("sumDrop") _
                                                                    , drDrop("Total_Qty") _
                                                                    , drDrop("Weight") _
                                                                    , drDrop("Volume"))
                If dblMinimum_Rate > 0 Then
                    If dblMinimum_Rate > ReturnObject(0) Then ReturnObject(0) = dblMinimum_Rate
                End If
                drDrop("Amount") = ReturnObject(0)
                drDrop("Amount_Charge") = ReturnObject(0)
                drDrop("Description") = ReturnObject(1)
                drDrop("Minimum_Rate") = ReturnObject(2)
                dblSumIn += drDrop("Amount")
            Next

            'If CDbl(Me.txtSumPrice_Out.Text) = 0 Then
            Me.txtSumPrice_Out.Text = FormatNumber(dblSumIn, 2)
            'End If

            'Me.txtSumPrice_Out.Tag = FormatNumber(dblSumIn, 2)

            'ขาจ่าย
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function CalculateTransportPayment(ByVal pstrCustomer_Index As String, ByVal pstrCarrier_Index As String, ByVal pstrTransportJobType_Index As String, ByVal pstrTransportRegion_Index As String _
    , ByVal intDrop As Integer _
    , ByVal pdblTotal_Qty As Double _
    , ByVal pdblWeight As Double _
    , ByVal pdblVolume As Double) As Object
        Try
            Dim dblAmount As Double = 0
            Dim osvarDropItem As New svar_TransportPaymentPerDropItem
            Dim dtDropItem As New DataTable
            Dim ReturnObject(2) As Object
            ReturnObject(0) = 0
            ReturnObject(1) = ""
            ReturnObject(2) = 0
            'dong ขี้เกียจแก้ class Make Index
            If pstrCustomer_Index = "" Then pstrCustomer_Index = "-11"
            If pstrCarrier_Index = "" Then pstrCarrier_Index = "-11"
            If pstrTransportJobType_Index = "" Then pstrTransportJobType_Index = "-11"
            If pstrTransportRegion_Index = "" Then pstrTransportRegion_Index = "-11"

            osvarDropItem.GetTransportPaymentPerDropItem(pstrCustomer_Index, pstrCarrier_Index, pstrTransportJobType_Index, pstrTransportRegion_Index)
            dtDropItem = osvarDropItem.GetDataTable
            If dtDropItem.Rows.Count = 0 Then Return ReturnObject

            'Dim oManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
            'Dim dtManifest As New DataTable
            'oManifest.getCustomer_Carrier_Region(Me._TransportManifest_Index)
            'dtManifest = oManifest.GetDataTable
            'Me.grdCarrier.DataSource = dtManifest

            Dim dblValue As Double = 0
            Dim dblRatePerDrop As Double = 0
            CType(Me.grdCarrier.DataSource, DataTable).AcceptChanges()
            Dim drArrManifest() As DataRow
            drArrManifest = CType(Me.grdCarrier.DataSource, DataTable).Select("TransportRegion_Index='" & pstrTransportRegion_Index & "'")



            Dim boolCharge As Boolean = False
            For Each drCal As DataRow In dtDropItem.Rows
                Select Case drCal("TransportPaymentGroup_Id")
                    Case 1 'คิดตามจำนวน Drop
                        If drCal("QtyDropEnd") = 0 Then
                            boolCharge = True
                        End If
                        If (intDrop >= drCal("QtyDropStart")) And (intDrop <= drCal("QtyDropEnd")) Then
                            boolCharge = True
                        End If
                        If Not boolCharge Then
                            Continue For
                        End If

                        For Each drManifest As DataRow In drArrManifest
                            drManifest("OverflowPerDrop") = CDbl(drCal("OverflowPerDrop"))
                            drManifest("OverflowRate") = CDbl(drCal("OverflowRate"))
                            drManifest("RateTransportPerDrop") = CDbl(drCal("RateTransportPerDrop"))
                            drManifest("Description") = drCal("Description").ToString
                            ReturnObject(2) = drCal("Minimum_Rate")
                            'Have Overflow
                            If (intDrop <= drCal("Minimum_Drop")) Then
                                drManifest("Rate") = drCal("RateTransportPerDrop")
                                drManifest("Description") = "Minimum Charge"
                                drCal("Description") = drManifest("Description")
                                dblAmount = drCal("Minimum_Rate")
                            Else
                                If drCal("isOverflow") Then
                                    Select Case drCal("OverflowPerDropUnit")
                                        Case 0 'Volume
                                            dblValue = CDbl(drManifest("Volume"))
                                        Case 1 'Kg
                                            dblValue = CDbl(drManifest("Weight"))
                                    End Select

                                    If dblValue >= CDbl(drCal("OverflowPerDrop")) Then
                                        dblRatePerDrop = dblValue * CDbl(drCal("OverflowRate"))
                                        drManifest("Description") &= " (Overflow)"
                                    Else
                                        dblRatePerDrop = CDbl(drCal("RateTransportPerDrop"))
                                    End If

                                    drManifest("Rate") = dblRatePerDrop
                                    dblAmount += dblRatePerDrop
                                Else
                                    'Don,t Have Overflow Normal Caculate (Drop*Rate)
                                    drManifest("Rate") = drCal("RateTransportPerDrop")
                                    dblAmount = intDrop * drCal("RateTransportPerDrop")
                                End If

                            End If

                        Next


                        ReturnObject(0) = dblAmount
                        ReturnObject(1) = drCal("Description").ToString
                        Return ReturnObject

                    Case 2 'คิดเหมาต่อเที่ยว
                        For Each drManifest As DataRow In drArrManifest
                            drManifest("OverflowPerDrop") = 0
                            drManifest("OverflowRate") = 0
                            drManifest("RateTransportPerDrop") = 0
                            drManifest("Description") = drCal("Description").ToString
                        Next

                        dblAmount = drCal("RateTransportPerTrip")
                        ReturnObject(0) = dblAmount
                        ReturnObject(1) = drCal("Description").ToString
                        Return ReturnObject

                    Case 3 'คิดจากต้นทุนการจ้างรถ
                        For Each drManifest As DataRow In drArrManifest
                            drManifest("OverflowPerDrop") = 0
                            drManifest("OverflowRate") = 0
                            drManifest("RateTransportPerDrop") = 0
                            drManifest("Description") = drCal("Description").ToString
                        Next

                        dblAmount = 0
                        If IsNumeric(Me.txtSumPrice_Out.Text) Then
                            dblAmount = ((CDbl(Me.txtSumPrice_Out.Text) * drCal("Percent_TransportChargeTopUp")) / 100) + CDbl(Me.txtSumPrice_Out.Text)
                        End If
                        ReturnObject(0) = dblAmount
                        ReturnObject(1) = drCal("Description").ToString
                        Return ReturnObject
                End Select

            Next

            Return ReturnObject

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    Private Sub cboTransportJobType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTransportJobType.SelectionChangeCommitted
        Try
            'Me.CallculateTransportChage()
            'Me.CallculateTransportPayment()
            Select Case Me.cboTransportJobType.SelectedValue
                Case Me._TransportJobType_Spacial_Case
                    Me.grdTransportCharge.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
                    Me.grdTransportPayment.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2
                    Me.chkSpecialCase.Checked = True
                Case Else
                    Me.chkSpecialCase.Checked = False
                    Me.grdTransportCharge.EditMode = DataGridViewEditMode.EditProgrammatically
                    Me.grdTransportPayment.EditMode = DataGridViewEditMode.EditProgrammatically
                    'If Me.grdTrasportManifestItem.Rows.Count > 0 Then
                    '    Me.btnTransportCharge_Click(sender, e)
                    'End If

            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboTransportJobType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboTransportJobType.SelectedIndexChanged
        Try

            If Me.cboTransportJobType.SelectedValue IsNot Nothing Then
                Dim drArr() As DataRow
                drArr = CType(Me.cboTransportJobType.DataSource, DataTable).Select("TransportJobType_Index ='" & Me.cboTransportJobType.SelectedValue & "'")
                If drArr.Length > 0 Then
                    Me.chkIsPack.Checked = IIf(drArr(0)("IsPack").ToString = "", False, drArr(0)("IsPack"))

                    For i As Integer = 0 To Me.grdTrasportManifestItem.RowCount - 1
                        Me.grdTrasportManifestItem.Rows(i).Cells("chkPacking").Value = IIf(drArr(0)("IsPack").ToString = "", False, drArr(0)("IsPack"))
                    Next

                End If

            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboVehicleType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboVehicleType.SelectedIndexChanged
        Try
            If cboVehicleType.SelectedValue Is Nothing Then Exit Sub
            'Me.getVehicle_OnTruck(txtVehicleID.Tag)


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'Private Function checkTruckLiimit() As Boolean
    '    Try
    '        Dim boolLimit As Boolean = False
    '        If (CDbl(Me.txtSumWeight.Text)) > CDbl(Me.txtWeight_Limit.Text) Then
    '            boolLimit = True
    '        End If
    '        If (CDbl(Me.txtSumVolume.Text)) > CDbl(Me.txtVolume_Limit.Text) Then
    '            boolLimit = True
    '        End If
    '        If W_MSG_Confirm_ByIndex("100034") = Windows.Forms.DialogResult.Yes Then
    '            Return True
    '        Else
    '            Return False
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Private Sub btnWithDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'STEP 1 : Load ManifestItem
            Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
            objtbManifest.getTransportManifest_Detail(Me._TransportManifest_Index, _IsSubManifest)
            Dim dtTrasportManifestItem As New DataTable
            dtTrasportManifestItem = objtbManifest.DataTable

            Dim drArr() As DataRow = dtTrasportManifestItem.Select("SaleOrders_Status not in (2,6) and Process_Id = 10 ")
            For Each drso As DataRow In drArr
                W_MSG_Information(GetMessage_Data("400030") & " " & drso("SalesOrder_No") & Chr(13) & GetMessage_Data("400059"))
                Exit Sub
            Next

            drArr = dtTrasportManifestItem.Select("SaleOrders_Status in (2,6) and Process_Id = 10")
            Dim ArrSalesOrder_Index As New List(Of String)
            For Each drso As DataRow In drArr
                ArrSalesOrder_Index.Add(drso("SalesOrder_Index").ToString)
            Next


            If ArrSalesOrder_Index.Count > 0 Then
                Dim frm As New frmWithdrawAsset(frmWithdrawAsset.enuOperation_Type.ADDNEW)
                frm.ArrSalesOrder_Index = ArrSalesOrder_Index
                frm.txtRef_No5.Text = Me.txtTransportManifest_No.Text
                frm.txtRef_No5.ReadOnly = True
                frm.ShowDialog()
                Me.getTransportManifest_Item()
            Else
                W_MSG_Information_ByIndex("400060")
            End If
            SetRow_Number_grdTrasportManifestItem()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub SetRow_Number_grdTrasportManifestItem()
        Try
            If Me.grdTrasportManifestItem.Rows.Count > 0 Then
                For i As Integer = 0 To Me.grdTrasportManifestItem.Rows.Count - 1
                    Me.grdTrasportManifestItem.Rows(i).Cells("col_Row_No").Value = i + 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Private Sub chkTransportCharged_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTransportCharged.CheckedChanged
        Try
            If chkTransportCharged.Checked = True Then
                txtSumPrice_In.Enabled = True
            Else
                txtSumPrice_In.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkTransportPaid_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkTransportPaid.CheckedChanged
        Try
            If chkTransportPaid.Checked = True Then
                txtSumPrice_Out.Enabled = True
            Else
                txtSumPrice_Out.Enabled = False
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSumPrice_In_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSumPrice_In.TextChanged
        Try
            If txtSumPrice_In.Text = "" Then
                txtSumPrice_In.Text = "0"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSumPrice_Out_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtSumPrice_Out.TextChanged
        Try
            If txtSumPrice_Out.Text = "" Then
                txtSumPrice_Out.Text = "0"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



#Region " Datagrid Controll Event grdTransportCharge"
    Private Sub grdTransportCharge_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTransportCharge.CellEndEdit
        Try
            With Me.grdTransportCharge
                Dim Column_Index As String = .CurrentCell.ColumnIndex
                Select Case Column_Index
                    Case Is = .Columns("Amount_Charge").Index
                        If IsNumeric(.CurrentRow.Cells("Amount_Charge").Value) Then
                            Dim dblAmount As Double = 0
                            For i As Integer = 0 To .RowCount - 1
                                If IsNumeric(.Rows(i).Cells("Amount_Charge").Value) Then
                                    dblAmount += .Rows(i).Cells("Amount_Charge").Value
                                End If
                            Next
                            Me.txtSumPrice_In.Text = FormatNumber(dblAmount, 2)
                        End If
                End Select
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdTransportCharge_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTransportCharge.EditingControlShowing
        Try
            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            Dim strName As String = grdTransportCharge.Columns(grdTransportCharge.CurrentCell.ColumnIndex).Name
            If (strName = "Amount_Charge") Then
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

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdTransportCharge.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdTransportCharge.Columns("Amount_Charge").Index
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
                            If grdTransportCharge.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdTransportCharge.CurrentRow.Cells(Column_Index).EditedFormattedValue
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

#Region " Datagrid Controll Event grdTransportPayment"
    Private Sub grdTransportPayment_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTransportPayment.CellEndEdit
        Try
            With Me.grdTransportPayment
                Dim Column_Index As String = .CurrentCell.ColumnIndex
                Select Case Column_Index
                    Case Is = .Columns("Amount_Charge2").Index
                        If IsNumeric(.CurrentRow.Cells("Amount_Charge2").Value) Then
                            Dim dblAmount As Double = 0
                            For i As Integer = 0 To .RowCount - 1
                                If IsNumeric(.Rows(i).Cells("Amount_Charge2").Value) Then
                                    dblAmount += .Rows(i).Cells("Amount_Charge2").Value
                                End If
                            Next
                            Me.txtSumPrice_Out.Text = FormatNumber(dblAmount, 2)
                        End If
                End Select
            End With

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdTransportPayment_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTransportPayment.EditingControlShowing
        Try
            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            Dim strName As String = grdTransportPayment.Columns(grdTransportPayment.CurrentCell.ColumnIndex).Name
            If (strName = "Amount_Charge2") Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress2
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress2
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtEdit_Keypress2(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdTransportPayment.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdTransportPayment.Columns("Amount_Charge2").Index
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function Check_GrdKeyPress2(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdTransportPayment.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdTransportPayment.CurrentRow.Cells(Column_Index).EditedFormattedValue
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




    Private Sub btnClearTransportCharge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearTransportCharge.Click
        Try
            If W_MSG_Confirm("คุณต้องการล้างข้อมูลการคิดเงินค่าขนส่ง ใช่หรือไม่") = Windows.Forms.DialogResult.No Then Exit Sub
            Me.txtSumPrice_In.Text = FormatNumber(0, 2)
            Me.txtSumPrice_Out.Text = FormatNumber(0, 2)

            Me.grdTransportCharge.DataSource = Nothing
            Me.grdTransportPayment.DataSource = Nothing

            Me.grdCustomer_Shipping.DataSource = Nothing
            Me.grdCarrier.DataSource = Nothing

            Me.btnClearTransportCharge.Enabled = False
            Me.btnTransportCharge.Enabled = True

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub tbpSOList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbpSOList.Click

    End Sub

    Private Sub txtKeySearch_Inv_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch_Inv.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                SearchInv_IN_Datagrid()
            End If

        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

    Private Sub SearchInv_IN_Datagrid()
        Try
            If Me.txtKeySearch_Inv.Text = "" Then Exit Sub

            Dim strCol As String = ""
            If Me.rdoInv_No.Checked = True Then
                strCol = "col_Invoice_No"
            End If

            If Me.rdoSO_No.Checked = True Then
                strCol = "col_SO_No"
            End If

            For Row As Integer = 0 To grdTrasportManifestItem.Rows.Count - 1
                If grdTrasportManifestItem.Rows(Row).Cells(strCol).Value.ToString.Substring(0, Me.txtKeySearch_Inv.Text.Length) = txtKeySearch_Inv.Text Then
                    grdTrasportManifestItem.Rows(Row).Selected = True
                    Exit For

                End If
            Next



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboRegion_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRegion.SelectionChangeCommitted
        Try
            If Me.cboRegion.SelectedValue Is Nothing Then Exit Sub

            If Me.grdTransportCharge.RowCount > 0 Then
                CType(Me.grdTransportCharge.DataSource, DataTable).AcceptChanges()
                Dim dtTransportCharge As New DataTable
                dtTransportCharge = Me.grdTransportCharge.DataSource
                Dim drArrSelect() As DataRow
                drArrSelect = dtTransportCharge.Select("TransportRegion_Index='" & Me.cboRegion.SelectedValue & "'")
                Dim dblMinimum_Rate As Double = 0
                dblMinimum_Rate = IIf(drArrSelect.Length = 0, 0, IIf(IsNumeric(drArrSelect(0)("Minimum_Rate")), drArrSelect(0)("Minimum_Rate"), 0))

                'If dblMinimum_Rate > 0 Then Me.CallculateTransportChage(dblMinimum_Rate)
                If dblMinimum_Rate > 0 Then txtSumPrice_In.Text = FormatNumber(dblMinimum_Rate, 2)

            End If


            If Me.grdTransportPayment.RowCount > 0 Then
                CType(Me.grdTransportPayment.DataSource, DataTable).AcceptChanges()
                Dim dtTransportPayment As New DataTable
                dtTransportPayment = Me.grdTransportPayment.DataSource
                Dim drArrSelect() As DataRow
                drArrSelect = dtTransportPayment.Select("TransportRegion_Index='" & Me.cboRegion.SelectedValue & "'")
                Dim dblMinimum_Rate As Double = 0
                dblMinimum_Rate = IIf(drArrSelect.Length = 0, 0, IIf(IsNumeric(drArrSelect(0)("Minimum_Rate")), drArrSelect(0)("Minimum_Rate"), 0))


                'If dblMinimum_Rate > 0 Then Me.CallculateTransportPayment(dblMinimum_Rate)
                If dblMinimum_Rate > 0 Then txtSumPrice_Out.Text = FormatNumber(dblMinimum_Rate, 2)

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
    Private Sub frmTransportManifest_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransport
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 24)
                    oFunction.SW_Language_Column(Me, Me.grdTrasportManifestItem, 24)
                    oFunction.SW_Language_Column(Me, Me.grdCustomer_Shipping, 24)
                    oFunction.SW_Language_Column(Me, Me.grdTransportCharge, 24)
                    oFunction.SW_Language_Column(Me, Me.grdTransportPayment, 24)
                    oFunction.SW_Language_Column(Me, Me.grdCarrier, 24)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintBarcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frm As New frmPrintBarcode_Transport
            frm.Transport_Manifest_Index = Me.txtTransportManifest_No.Text.Trim()
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub grdTrasportManifestItem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTrasportManifestItem.CellClick
        If e.RowIndex <= -1 Then
            Exit Sub
        End If
        If e.ColumnIndex <= -1 Then
            Exit Sub
        End If

        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToUpper
                Case "COL_BTNCUSTOMERSHIPPINGLOCATION"
                    Dim TransportManifestItem_Index As String = grdTrasportManifestItem.Rows(e.RowIndex).Cells("col_System_Index").Value
                    Dim SalesOrder_Index As String = grdTrasportManifestItem.Rows(e.RowIndex).Cells("col_SalesOrder_Index").Value

                    Dim frm As New frmCus_Ship_Location_Popup
                    frm.strAddStrWhere = ""
                    frm.ShowDialog()
                    If frm.Customer_Shipping_Location_Index = "" Then
                        Exit Sub
                    End If

                    '1.Update CUSTOMER_SHIPPING_LOCATION
                    Dim oTSS As New ml_TSS
                    oTSS.fnUPDATE_CUSTOMER_SHIPPING_LOCATION(TransportManifestItem_Index, SalesOrder_Index, frm.Customer_Shipping_Location_Index)
                    W_MSG_Information_ByIndex(1)
                    '2.Load Data
                    Me.getTransportManifest_Item()

            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnAdminConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdminConfirm.Click
        Try
            'Dim frmpassword As New WMS_STD_Master.PopupEnterPassword
            'frmpassword.ShowDialog()
            'If frmpassword.passwordistrue = False Then
            '    Exit Sub
            'End If

            'Dim frm As New frmTransportManifest_AdminConfirm
            'frm.TransportManifest_Index = Me._TransportManifest_Index
            'frm.ShowDialog()

            If String.IsNullOrEmpty(Me._TransportManifest_Index) Then
                Exit Sub
            End If

            Dim TransportManifestIndex As String = Me._TransportManifest_Index

            Dim Service As New WMS_Site_Topcharoen_Interface.WMS_Interface
            If Service.IsConfirmGI(TransportManifestIndex) Then
                If W_MSG_Confirm("ใบคุมรถนี้ ส่ง Interface OMS ไปแล้ว" & vbCrLf & "ต้องการส่ง Interface อีกครั้ง ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            Else
                If W_MSG_Confirm("ยืนยันการส่ง Interface ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                    Exit Sub
                End If
            End If

INTERFACE_OMS:
            Dim Result As String = Service.ConfirmGI(TransportManifestIndex, W_Module.WV_UserName)
            If Not String.IsNullOrEmpty(Result) Then
                If W_MSG_Confirm("Interface OMS ผิดพลาด" & vbCrLf & Result & vbCrLf & "ต้องการส่ง Interface OMS อีกครั้งใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
                    GoTo INTERFACE_OMS
                End If
            End If

            W_MSG_Information("ส่ง Interface OMS เสร็จสิ้น")

            Me.getTransportManifest_Header()
            Me.getTransportManifest_Item()
            Me.getTransportCharge_Data()
            Me.getTransportPayment_Data()

            objStatus = Manifest_Mode.EDIT
            Me.ManageButtom()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnAddTransportChargePackSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTransportChargePackSize.Click
        Try
            'If String.IsNullOrEmpty(Me.txtCarrier_ID.Tag) Then
            '    W_MSG_Information("กรุณาระบุ " & Me.lblCarrier.Text)
            '    Exit Sub
            'End If

            'Dim frm As New frmMainTransportManifestChargePackSize
            'frm.Carrier_Index = Me.txtCarrier_ID.Tag
            'frm.ShowDialog()

            Dim Service As New tb_TransportManifest_Update
            Dim Data As DataTable = Service.GetBarcodeB1(Me._TransportManifest_Index)

            Data.Columns.Remove("TransportManifest_Index")
            If Data IsNot Nothing AndAlso Data.Rows.Count > 0 Then
                Dim frmExport As New frmPopup_for_ExportExcel(Data)
                frmExport.ShowDialog()
            Else
                Throw New Exception("ไม่พบข้อมูล Barcode B1")
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Function Check_SaleOrders_Status()
        Try
            'Check Status SalesOrder
            Dim oManifest As New ManifestTransaction_Update(ManifestTransaction_Update.enuOperation_Type.SEARCH)
            For Each checkDelete As DataRow In CType(grdTrasportManifestItem.DataSource, DataTable).Rows
                Dim CheckTransportManifestItem_Index As String = checkDelete("TransportManifestItem_Index").ToString()

                If Not oManifest.Validate_Edit_TM(CheckTransportManifestItem_Index) Then
                    Return False
                End If
            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class