Imports System.Data.Common
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_master.ValidateValueType
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_master.W_Function
Imports WMS_STD_Master
Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_OUTB_Reserv_Datalayer
Imports WMS_STD_OUTB_Datalayer
Imports WMS_STD_OUTB
Imports WMS_STD_OUTB_Report


Public Class frmWithdrawAsset_V4
    Private _Status As Integer = 0

    Dim objCustomSetting As New config_CustomSetting
    Dim StatusWithdraw_format As Withdraw_format
    Dim StatusWithdraw_Type As Withdraw_Type
    Private dtpTempWithDraw_Date As Date = Now
    Public IsAdmin As Boolean = False
    Public _ConfigPrint As String = ""
    Private _OldWithdraw_No As String = ""

    Private Enum Withdraw_Type
        CUSTOM = 0
        AUTO = 1
    End Enum

    Private Enum Withdraw_format
        Custom = 0
        FIFO = 1
        LIFO = 2
        FEFO = 3
        SERIAL = 4
        Pickface = 5
    End Enum

    Private _strPathName As String = ""
    Private _strFileName As String = ""
    Private _strLongFilePath As String = ""

    Public odtWithdrawImage As DataTable

    Private _DEFAULT_ImagePath As String = ""
    Private DEFAULT_USE_REPORTPRINTOUT_BOND As String = ""
    Public _withdrawType As Integer = 0

    Private _USE_PACKING_NEW_PRODUCTION As Boolean = False

    Public Property USE_PACKING_NEW_PRODUCTION() As Boolean
        Get
            Return _USE_PACKING_NEW_PRODUCTION
        End Get
        Set(ByVal value As Boolean)
            _USE_PACKING_NEW_PRODUCTION = value
        End Set
    End Property


    Public Property withdrawType() As Integer
        Get
            Return _withdrawType
        End Get
        Set(ByVal value As Integer)
            _withdrawType = value
        End Set
    End Property

    Public Property DEFAULT_ImagePath() As String
        Get
            Return _DEFAULT_ImagePath
        End Get
        Set(ByVal value As String)
            _DEFAULT_ImagePath = value
        End Set
    End Property


    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        SEARCH
        NULL
    End Enum

    Dim StatusWithdraw_Document As Withdraw_Document
    Private Enum Withdraw_Document
        SO = 10
        Packing = 7
        Reserve = 17
        Transport = 25
        Reservation = 30
    End Enum


    Private _Withdraw_index As String = ""
    Private _Withdraw_no As String = ""
    Private Updatewithdraw_index As String = ""
    Private _Customer_Index As String = ""
    Private _CustomerID As String = ""
    Private _CustomerName As String = ""

    Private _Plan_Process As Integer = -9
    Private _DocumentPlanItem_Index As String = ""
    Private _DocumentPlan_Index As String = ""

    Private _DocumentPlan_No As String = ""
    Private _Reference As String = ""
    Private _Reference2 As String = ""

    Private _TConsinee_Index As String = ""
    Private _TConsinee_Name As String = ""

    Private _AssetLocationBalance_Index As String = ""
    Private _Asset_No As String = ""
    Private _AssetSerial_No As String = ""
    Private _isSerail As Boolean = False
    Private _NewItem As Integer = 1

    Private _DEFAULT_SerialNO As Decimal
    Private _Remark As String = ""

    Private _odtValidate_Control As DataTable
    Private _odtValidate_grdWithdrawItemLocation As DataTable
    Private _odtValidate_grdWithDrawPlant As DataTable
    Private _odtValidate_grdWithDrawTruckOut As DataTable
    Private _odtTempItemLocation As New DataTable


    'Plan withdraw
    Public Property Plan_Process() As String
        Get
            Return _Plan_Process
        End Get
        Set(ByVal value As String)
            _Plan_Process = value
        End Set
    End Property
    Public Property DocumentPlan_No() As String
        Get
            Return _DocumentPlan_No
        End Get
        Set(ByVal value As String)
            _DocumentPlan_No = value
        End Set
    End Property
    Public Property DocumentPlan_Index() As String
        Get
            Return _DocumentPlan_Index
        End Get
        Set(ByVal value As String)
            _DocumentPlan_Index = value
        End Set
    End Property
    Public Property DocumentPlanItem_Index() As String
        Get
            Return _DocumentPlanItem_Index
        End Get
        Set(ByVal value As String)
            _DocumentPlanItem_Index = value
        End Set
    End Property


    'Begin AutoSo
    Private _SO_Index As String = ""
    Public Property SO_Index() As String
        Get
            Return _SO_Index
        End Get
        Set(ByVal value As String)
            _SO_Index = value
        End Set
    End Property

    Private _ArrSalesOrder_Index As List(Of String)
    Public Property ArrSalesOrder_Index() As List(Of String)
        Get
            Return _ArrSalesOrder_Index
        End Get
        Set(ByVal value As List(Of String))
            _ArrSalesOrder_Index = value
        End Set
    End Property


    'End AutoSo


    Public Property DEFAULT_SerialNO() As Decimal
        Get
            Return _DEFAULT_SerialNO
        End Get
        Set(ByVal value As Decimal)
            _DEFAULT_SerialNO = value
        End Set
    End Property

    Private _WithdrawStatus As Boolean
    Public Property WithdrawStatus() As Boolean
        Get
            Return _WithdrawStatus
        End Get
        Set(ByVal value As Boolean)
            _WithdrawStatus = value
        End Set
    End Property


    Private CurrentRowPlanWithDraw_Index As Integer = 0

    Public Property Withdraw_Index() As String
        Get
            Return _Withdraw_index
        End Get
        Set(ByVal value As String)
            _Withdraw_index = value
        End Set
    End Property

    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property

    Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property


    '15-01-2010 ja update AutoReserver
    Private _Reserve_Index As String = ""
    Public Property Reserve_Index() As String
        Get
            Return _Reserve_Index
        End Get
        Set(ByVal value As String)
            _Reserve_Index = value
        End Set
    End Property



    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub
    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal withdraw_index As String)

        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

        Select Case objStatus

            Case enuOperation_Type.UPDATE
                Me._Withdraw_index = withdraw_index
        End Select

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' 15-01-2010
    ''' ja
    ''' load plan reserver to withdraw
    ''' <remarks></remarks>
    Private Sub frmWithdrawAsset_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            _odtValidate_Control = oFunction.SwitchLanguage(Me, 2)
            _odtValidate_grdWithdrawItemLocation = oFunction.SW_Language_Column(Me, Me.grdWithdrawItemLocation, 2)
            _odtValidate_grdWithDrawPlant = oFunction.SW_Language_Column(Me, Me.grdWithDrawPlan, 2)
            _odtValidate_grdWithDrawTruckOut = oFunction.SW_Language_Column(Me, Me.grdWithDrawTruckOut, 2)

            If objCustomSetting.getConfig_Key_USE("USE_WITHDRAW_ASSIGNJOB_MULTI_USER") Then
                btnAssignJob.Visible = True
            Else
                btnAssignJob.Visible = False
            End If

            grdWithDrawPlan.AutoGenerateColumns = False
            grdWithdrawItemLocation.AutoGenerateColumns = False

            Me.SetDEFAULT_CUSTOMER_INDEX()

            Me.SetUSE_PACKING_NEW_PRODUCTION()

            Me.OpenFileDialog1.Filter = "Bmp files (*.bmp)|*.bmp|" + "Gif files (*.gif)|*.gif|" + "Jpeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Png files (*.png)|*.png|" + "All graphic files (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"
            SetImage_Path()

            Me.getDocumentType(2)
            Me.getReportName(2)
            getCboHandlingType()
            getcboHandlingType2()

            btnPrint.Enabled = False

            '--- SET DATE BEGIN
            SETCUTOFF_TIME_OFFSET()
            SetDEFAULT_Serial_No()

            'config_txtField()
            If objCustomSetting.getConfig_Key_USE("USE_Enable_Control_WTH") = True Then
                config_Withdraw_Enable()
            Else
                config_Withdraw()
            End If

            '--- SET SELECT COMBO
            cboAutoPicking.SelectedIndex = 0
            Me.btnSerial.Enabled = False


            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                    'Insert Header
                    Dim Strtmp As String = ""
                    Strtmp = InsertTmpHaeder()

                    If Strtmp.ToUpper <> "S" Then
                        W_MSG_Information(Strtmp)
                        Me.Close()
                    End If

                    'Dong_kk : Edit เก็บไว้เพราะที่อื่นอาจใช้อยู่ 
                    'Load So : One To One
                    If Me.SO_Index <> "" Then
                        Me.getSOHeader(Me.SO_Index)
                        LoadPlanWithdraw(Me.SO_Index, Withdraw_Document.SO)
                        ' Load_WhitdrawLocation_Edit(Me.SO_Index, Withdraw_Document.SO)
                        tbcWithDrawDetail.SelectedTab = Me.tbpPlanWithDraw
                    End If

                    'Load So : One To Many
                    If Me._ArrSalesOrder_Index IsNot Nothing Then
                        Me.getSOHeader(_ArrSalesOrder_Index(0))
                        For Each strSalesOrder_Index As String In _ArrSalesOrder_Index
                            LoadPlanWithdraw(strSalesOrder_Index, Withdraw_Document.SO)
                        Next
                        tbcWithDrawDetail.SelectedTab = Me.tbpPlanWithDraw
                    End If

                    If Me.Reserve_Index <> "" Then '15-01-2010 ja update load Reserve
                        Me.getReserveHeader(Me.Reserve_Index)
                        _DocumentPlan_Index = Me.Reserve_Index
                        _Plan_Process = Withdraw_Document.Reserve

                        LoadPlanWithdraw(Me.Reserve_Index, Withdraw_Document.Reserve)
                        LoadWithdrawItemLocation(Me.Reserve_Index)

                        Me.btnAddItemAsset.Visible = False
                        Me.btnAddItemAsset_Barcode.Visible = False
                        Me.btnDelItemAsset.Visible = False
                        Me.btn_Reserv.Visible = False
                    End If

                    'Me.btnSwitch_Sku.Visible = False

                Case enuOperation_Type.UPDATE
                    _NewItem = 0
                    Load_frmEdit(_Withdraw_index)
                    Load_WhitdrawLocation_Edit(_Withdraw_index, 2)
                    getWithDrawTruckOutDetail(_Withdraw_index)
                    Get_SumData()

                    If _WithdrawStatus = True Then
                        ViewOnly()
                    Else
                        Dim objReserve As New Cl_WithdrawReserv
                        objReserve.UpdateUseDoc(Cl_WithdrawReserv.CaseReserve.Reserve, _Withdraw_index)
                    End If

            End Select

            Me.OpenFileDialog1.Filter = "Bmp files (*.bmp)|*.bmp|" + "Gif files (*.gif)|*.gif|" + "Jpeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Png files (*.png)|*.png|" + "All graphic files (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"

            GetWithdrawImage(_Withdraw_index)
            If IsAdmin Then
                Me.txtWithdraw_No.Enabled = True
                Me.txtWithdraw_No.ReadOnly = False
                Me.cboDocumentType.Enabled = True
                Me.btnSave.Enabled = True
                Me.btnSeachDepartment.Enabled = True
                _OldWithdraw_No = txtWithdraw_No.Text
            End If
            setLocationControl_By_Config()

            'Add new
            Me.setStatusPicking()

            'KSL
            Me.btnClear.Visible = False
            Me.Panel3.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
            Me.Close()
            'Finally
            '    objStatus = enuOperation_Type.UPDATE
        End Try
    End Sub

    Private Function InsertTmpHaeder() As String
        Try
            Dim objHaeder As New tb_Withdraw
            Dim objSy_AutoNumber As New Sy_AutoNumber

            objHaeder.Withdraw_Index = objSy_AutoNumber.getSys_Value("Withdraw_Index")
            objHaeder.Withdraw_No = objHaeder.Withdraw_Index
            objHaeder.Customer_Index = Me.Customer_Index
            objHaeder.DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
            _Withdraw_index = objHaeder.InsertTempHesder()

            Return "S"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try
    End Function

    Private Sub ManageButton()
        Try
            'รายการนำของออกรายชิ้น
            Me.btnAddItemAsset.Enabled = False
            Me.btnAddItemAsset_Barcode.Enabled = False
            Me.btnDelItemAsset.Enabled = False
            'รายการสินค้าที่ต้องการออก
            Me.btnAddPlan.Enabled = False
            Me.btnDelPlan.Enabled = False
            Me.btnBarCode.Enabled = False
            Me.btnWithDraw.Enabled = False
            'ข้อมูลการจัดส่ง
            Me.btnAdd_TruckOut.Enabled = False
            Me.btnEdit_TruckOut.Enabled = False
            Me.btnDelete_TruckOut.Enabled = False
            'รูปภาพ
            Me.btnImageAdd.Enabled = False
            Me.btnImageDelete.Enabled = False
            'ปุ่มหลัก
            Me.btnSave.Enabled = False
            Me.btnClear.Enabled = False
            'Me.btnEditOrder.Enabled = False
            'Me.btnConfirm.Enabled = False
            'Me.btnCancel.Enabled = False
            'Me.btnMemo.Enabled = False
            Me.grbPrintReport.Enabled = False

            Select Case Me._Status
                Case 0 'new
                    'รายการนำของออกรายชิ้น
                    Me.btnAddItemAsset.Enabled = True
                    Me.btnAddItemAsset_Barcode.Enabled = True
                    Me.btnDelItemAsset.Enabled = True
                    'รายการสินค้าที่ต้องการออก
                    Me.btnAddPlan.Enabled = True
                    Me.btnDelPlan.Enabled = True
                    Me.btnBarCode.Enabled = True
                    Me.btnWithDraw.Enabled = True
                    'ข้อมูลการจัดส่ง
                    Me.btnAdd_TruckOut.Enabled = True
                    Me.btnEdit_TruckOut.Enabled = True
                    Me.btnDelete_TruckOut.Enabled = True
                    'รูปภาพ
                    Me.btnImageAdd.Enabled = True
                    Me.btnImageDelete.Enabled = True
                    'ปุ่มหลัก
                    Me.btnSave.Enabled = True
                    Me.btnClear.Enabled = True
                Case 1 'Edit
                    'Me.btnEditOrder.Enabled = True
                    'ปุ่มหลัก
                    'Me.btnSave.Enabled = False
                    'Me.btnCancel.Enabled = True
                    'Me.btnConfirm.Enabled = True
                    'Me.btnMemo.Enabled = True
                Case 2 'Complete
                    'ปุ่มหลัก
                    'Me.btnCancel.Enabled = True
                    'print
                    Me.grbPrintReport.Enabled = True
                    Me.btnPrint.Enabled = True
                    Me.cboPrint.Enabled = True
                    'Me.btnMemo.Enabled = True
                Case -1 'Cancel
                    'False All
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub setLocationControl_By_Config()
        Try

            Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
            If Not oconfig.getConfig_Key_USE("USE_SKU_ITEM_QTY") Then
                lblSumPck.Location = New Point(lblSumPck.Location.X + 91, lblSumPck.Location.Y)
                txtSumQty.Location = New Point(txtSumQty.Location.X + 91, txtSumQty.Location.Y)
                lblSumAll.Location = New Point(lblSumAll.Location.X + 91, lblSumAll.Location.Y)

                txtPackage.Visible = False
                lbSumQty.Visible = False
            End If

        Catch ex As Exception
            Throw ex
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
                Return True
            Else
                Return False

            End If

            Return intBlock

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function


    Sub SetImage_Path()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Dim appSet As New Configuration.AppSettingsReader()
        Try

            'Dim strUseLocalPath As String = AppSettings("UseLocalPath").ToString
            Dim strUseLocalPath As String = appSet.GetValue("UseLocalPath", GetType(String))
            If strUseLocalPath = 0 Then
                ' Me._DEFAULT_ImagePath = AppSettings("IMAGE_PATH_WITHDRAW").ToString
                Me._DEFAULT_ImagePath = appSet.GetValue("IMAGE_PATH_WITHDRAW", GetType(String))
            Else
                objCustomSetting.GetConfig_Value("DEFAULT_IMAGE_PATH_WITHDRAW", "")
                objDT = objCustomSetting.DataTable
                If objDT.Rows.Count > 0 Then
                    Me._DEFAULT_ImagePath = objDT.Rows(0).Item("Config_Value").ToString
                Else
                    Me._DEFAULT_ImagePath = Application.StartupPath
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


#Region "  Auto So "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="SO_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd - 03 March 2010 - Add SubRoute
    ''' </remarks>
    Private Sub getSOHeader(ByVal SO_Index As String)
        Dim objWhitdraw As New tb_Withdraw
        ' Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.SEARCH)
        Dim DTWhitdraw As DataTable = New DataTable

        Try
            objWhitdraw.getSO_Auto_WithdrawHeader(SO_Index)
            DTWhitdraw = objWhitdraw.DataTable

            If DTWhitdraw.Rows.Count > 0 Then
                Me.txtSo_No.Text = DTWhitdraw.Rows(0).Item("SalesOrder_No").ToString
                Me.txtCustomer_Id.Text = DTWhitdraw.Rows(0).Item("Customer_Id").ToString
                Me._Customer_Index = DTWhitdraw.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Name.Text = DTWhitdraw.Rows(0).Item("Customer_Name").ToString
                Me.txtRef_No1.Text = DTWhitdraw.Rows(0).Item("Ref1").ToString
                Me.txtRef_No2.Text = DTWhitdraw.Rows(0).Item("Ref2").ToString
                Me.txtConsignee_ID.Tag = DTWhitdraw.Rows(0).Item("Customer_Shipping_Index").ToString
                Me.txtConsignee_Name.Text = DTWhitdraw.Rows(0).Item("Customer_ShippingName").ToString
                Me.txtConsignee_ID.Text = DTWhitdraw.Rows(0).Item("Customer_ShippingID").ToString

                ' Todd - 03 March 2010 - Add SubRoute
                If Not DTWhitdraw.Columns.Contains("SubRoute") Then
                    Me.txtNote.Text = ""
                Else
                    Me.txtNote.Text = DTWhitdraw.Rows(0).Item("SubRoute").ToString
                End If


            End If
        Catch ex As Exception
            Throw ex
        Finally
            objWhitdraw = Nothing
            DTWhitdraw = Nothing
        End Try
    End Sub

#End Region

    ''' <summary>
    ''' 
    ''' </summary>
    ''' 15-01-2010
    ''' ja
    '''Update Auto Reserve
    ''' <remarks></remarks>
#Region "  Auto Reserve "
    Private Sub getReserveHeader(ByVal Reserve_Index As String)
        Dim objWhitdraw As New tb_Withdraw
        Dim DTWhitdraw As DataTable = New DataTable

        Try
            objWhitdraw.getReserve_Auto_WithdrawHeader(Reserve_Index)
            DTWhitdraw = objWhitdraw.DataTable

            If DTWhitdraw.Rows.Count > 0 Then
                Me.txtSo_No.Text = DTWhitdraw.Rows(0).Item("Reserve_No").ToString
                Me.dtpWithdraw_Date.Value = DTWhitdraw.Rows(0).Item("Reserve_Date").ToString
                dtpTempWithDraw_Date = Me.dtpWithdraw_Date.Value
                Me.txtCustomer_Id.Text = DTWhitdraw.Rows(0).Item("Customer_Id").ToString
                Me._Customer_Index = DTWhitdraw.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Name.Text = DTWhitdraw.Rows(0).Item("Customer_Name").ToString
                Me.txtConsignee_ID.Tag = DTWhitdraw.Rows(0).Item("Customer_Shipping_Index").ToString
                Me.txtConsignee_Name.Text = DTWhitdraw.Rows(0).Item("Customer_ShippingName").ToString
                Me.txtConsignee_ID.Text = DTWhitdraw.Rows(0).Item("Customer_ShippingID").ToString
                Me.txtShipper_ID.Tag = DTWhitdraw.Rows(0).Item("Supplier_Index").ToString
                Me.txtShipper_ID.Text = DTWhitdraw.Rows(0).Item("Supplier_Id").ToString
                Me.txtShipper_Name.Text = DTWhitdraw.Rows(0).Item("Supplier_Name").ToString
                Me.txtRef_No1.Text = DTWhitdraw.Rows(0).Item("Str1").ToString 'เลขที่ใบขนฯ
                Me.txtRef_No2.Text = DTWhitdraw.Rows(0).Item("Str2").ToString ' MAWB / M.BL
                Me.txtRef_No3.Text = DTWhitdraw.Rows(0).Item("Str3").ToString 'HAWB / H.BL
                Me.txtRef_No4.Text = DTWhitdraw.Rows(0).Item("Str4").ToString 'Shipping No.
                Me.txtRef_No5.Text = DTWhitdraw.Rows(0).Item("Str5").ToString 'เลขที่ DO

                Me.txtInvoice_No.Text = DTWhitdraw.Rows(0).Item("Invoice_No").ToString 'Invoice_No
                Me.txtNote.Text = DTWhitdraw.Rows(0).Item("Note").ToString 'Note
                Me.txtComment.Text = DTWhitdraw.Rows(0).Item("Remark").ToString 'Remark





            End If
        Catch ex As Exception
            Throw ex
        Finally
            objWhitdraw = Nothing
            DTWhitdraw = Nothing
        End Try
    End Sub

#End Region
    Private Sub GetWithdrawImage(ByVal pstWithdraw_Index As String)
        Dim odjWithdrawImage As New tb_Withdraw_Image
        Try
            grdWithdrawImage.AutoGenerateColumns = False
            odjWithdrawImage.GetAllDataTable(" WHERE Withdraw_Index = '" & pstWithdraw_Index & "'")
            odtWithdrawImage = odjWithdrawImage.GetDataTable

            With grdWithdrawImage
                .DataSource = odtWithdrawImage
            End With
            If odtWithdrawImage.Rows.Count > 0 Then
                imgPreview.ImageLocation = odtWithdrawImage.Rows(0).Item("Image_Path").ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub




    Sub SetDEFAULT_Serial_No()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("Use_Serial", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me._DEFAULT_SerialNO = objDT.Rows(0).Item("Config_Value").ToString
            End If

            If _DEFAULT_SerialNO = 1 Then
                Me.grdWithdrawItemLocation.Location = New System.Drawing.Point(2, 35)
                Me.grdWithdrawItemLocation.Size = New System.Drawing.Size(981, 309)
            Else
                Me.grdWithdrawItemLocation.Location = New System.Drawing.Point(2, 3)
                Me.grdWithdrawItemLocation.Size = New System.Drawing.Size(981, 341)
            End If
            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub


    Sub SetDEFAULT_CUSTOMER_INDEX()
        Try
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            Me._Customer_Index = oUser.GetUserByCustomerDefault()

            If Not String.IsNullOrEmpty(Me._Customer_Index) Then
                Me.getCustomer()
            End If

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try

    End Sub

    Sub SetUSE_PACKING_NEW_PRODUCTION()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Me._USE_PACKING_NEW_PRODUCTION = objCustomSetting.getConfig_Key_USE("USE_PACKING_NEW_PRODUCTION")


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub ViewOnly()
        Try
            btnAddItemAsset.Enabled = False
            btnDelItemAsset.Enabled = False
            btnSave.Enabled = False
            btnAdd_TruckOut.Enabled = False
            btnEdit_TruckOut.Enabled = False
            btnDelete_TruckOut.Enabled = False
            btnAssignJob.Enabled = False
            btnClear.Enabled = False
            grdWithDrawPlan.ReadOnly = True
            grbPrintReport.Enabled = True
            btnPrint.Enabled = True
            cboPrint.Enabled = True

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New WMS_STD_Master.config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

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

    Private Sub config_Withdraw()
        Dim objClassDB As New tb_Withdraw
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "Ref_No1"
                            lblRef_No1.Visible = False
                            txtRef_No1.Visible = False

                        Case "Ref_No2"
                            lblRef_No2.Visible = False
                            txtRef_No2.Visible = False

                        Case "Ref_No3"
                            lblRef_No3.Visible = False
                            txtRef_No3.Visible = False

                        Case "Ref_No4"
                            lblRef_No4.Visible = False
                            txtRef_No4.Visible = False
                            txtRef_No1.Width = 355

                        Case "Ref_No5"
                            lblRef_No5.Visible = False
                            txtRef_No5.Visible = False
                            txtRef_No2.Width = 355

                        Case "Approved_By"
                            lblApprovede_By.Visible = False
                            txtApprovede_By.Visible = False

                        Case "Checker"
                            lblChecker_Name.Visible = False
                            txtChecker_Name.Visible = False

                        Case "Comment"
                            lblComment.Visible = False
                            txtComment.Visible = False

                            'Case "ASN_No"
                            '    lblASN_No.Visible = False
                            '    txtASN_No.Visible = False

                        Case "Handling_Type"
                            lblHandling_Type.Visible = False
                            cboType.Visible = False
                            txtRef_No3.Width = 355

                        Case "SO_No"
                            lblSo_No.Visible = False
                            txtSo_No.Visible = False

                        Case "Invoice_No"
                            lblInvoice_No.Visible = False
                            txtInvoice_No.Visible = False
                            txtSo_No.Width = 355

                        Case "Note"
                            lblNote.Visible = False
                            txtNote.Visible = False
                            'txtASN_No.Width = 355

                        Case "tbpHeader1"
                            Me.tbpHeader1.Dispose()
                        Case "tbpHeader2"
                            Me.tabImportExport.Dispose()
                        Case "Control_Image"
                            Me.tbpImage.Dispose()
                        Case "tbpDelivery"
                            Me.tbpDelivery.Dispose()

                        Case "tbpPlanWithDraw"
                            Me.tbpPlanWithDraw.Dispose()
                        Case "tbpHeader2"
                            Me.tbcWithdraw.TabPages.Remove(Me.tabImportExport)
                        Case "Supplier_Index"
                            lblShipper.Visible = False
                            txtShipper_ID.Visible = False
                            btnShipper.Visible = False
                            txtShipper_Name.Visible = False
                        Case "Department_Index"
                            lblDepartment.Visible = False
                            txtDepartment_Id.Visible = False
                            btnSeachDepartment.Visible = False
                            txtDepartment_Name.Visible = False
                        Case "Consignee_Index"
                            lblConsignee.Visible = False
                            txtConsignee_ID.Visible = False
                            btnConsignee.Visible = False
                            txtConsignee_Name.Visible = False

                            'Case "WithdrawType"
                            '    chkWithdrawType.Visible = False
                        Case "Print_Tag"
                            btnPrint_TagWithDraw.Visible = False
                        Case "BARCODE_PICK"
                            btnAddItemAsset_Barcode.Visible = False
                            btnBarCode.Visible = False
                    End Select
                    '   grdMaster.Rows(i).Cells("ColumnID").Value = objDr("Size_Id").ToString
                    '   grdMaster.Rows(i).Cells("ColumnDescription").Value = objDr("Description").ToString
                    '   grdMaster.Rows(i).Cells("ColumnIndex").Value = objDr("Size_Index").ToString
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
    Private Sub config_Withdraw_Enable()
        Dim objClassDB As New tb_Withdraw
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "tbpHeader1"
                            Me.tbpHeader1.Dispose()
                        Case "tbpHeader2"
                            Me.tabImportExport.Dispose()
                        Case "Control_Image"
                            Me.tbpImage.Dispose()
                        Case "tbpDelivery"
                            Me.tbpDelivery.Dispose()
                        Case "tbpPlanWithDraw"
                            Me.tbpPlanWithDraw.Dispose()
                        Case "tbpHeader2"
                            Me.tbcWithdraw.TabPages.Remove(Me.tabImportExport)
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

    Sub SETCUTOFF_TIME_OFFSET()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Dim dblOffsetMinute As Decimal = 0

            Dim strOffsetMinute As String = objCustomSetting.GetValue_Config("CUTOFF_TIME_OFFSET", "")
            If strOffsetMinute <> "" Then
                dblOffsetMinute = strOffsetMinute
                dtpWithdraw_Date.Value = Now.AddMinutes(dblOffsetMinute * -1)
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    Private Sub getCboHandlingType()
        Dim objDBType As New tb_HandlingType
        Dim objDTType As DataTable = New DataTable

        Try
            '--- Unload
            objDBType.GetAllAsDataTable()
            objDTType = objDBType.DataTable
            cboType.BeginUpdate()
            With cboType
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTType
            End With
            cboType.EndUpdate()

        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing

        End Try

    End Sub

    'Sub config_txtField() ' sub config การเปลี่ยนแปลงชื่อ ของ filed ของหน้าจอต่างๆผ่าน database  และการสลับหน้าจอการทำงานเปง ไทย อิง
    '    Try
    '        Dim Config As New config_txtfile
    '        Config.LoadTextField(Me.Name) ' txtFile_Click ค่าที่เราสร้างไว้ใน config_txtfile
    '        Dim dtFiled As New DataTable
    '        dtFiled = Config.DataTable

    '        Dim i As Integer = 0
    '        For i = 0 To dtFiled.Rows.Count - 1 ' นับค่า ข้อมูลในแถว -1 เพื่อเริ่มจากตำแหน่งที่ 0
    '            Dim strControl_name As String = ""
    '            Select Case WV_Language.Current_Language
    '                Case enmLanguage.Thai
    '                    strControl_name = dtFiled.Rows(i).Item("control_name_th").ToString()
    '                Case enmLanguage.English
    '                    strControl_name = dtFiled.Rows(i).Item("control_name_eng").ToString()

    '            End Select

    '            Dim strtxt_Control As String = dtFiled.Rows(i).Item("control_txt").ToString() 'โดยควบคุมตาม control_txt

    '            If tabHeader1.Controls.ContainsKey(strtxt_Control) Then
    '                tabHeader1.Controls(strtxt_Control).Text = strControl_name
    '            End If

    '            If tabHeader2.Controls.ContainsKey(strtxt_Control) Then
    '                tabHeader2.Controls(strtxt_Control).Text = strControl_name
    '            End If

    '            If Me.Controls.ContainsKey(strtxt_Control) Then
    '                Me.Controls(strtxt_Control).Text = strControl_name
    '            End If
    '        Next
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    Private Sub config_Withdraw_Reserv()
        Dim objClassDB As New tb_WithdrawItem
        Dim objDT As DataTable = New DataTable
        Try

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

    Sub Load_frmEdit(ByVal _strWithdraw_index As String)
        Try
            Dim Whithdraw_index As String = Withdraw_Index
            Dim obj_tb_Whitdraw As New tb_Withdraw
            Dim dt_tb_Whitdraw As DataTable

            '---------------------------------------------------------------------
            obj_tb_Whitdraw.SearchData_WithDrawAssetHeader(" Withdraw_Index = '" & Whithdraw_index & "'")
            dt_tb_Whitdraw = obj_tb_Whitdraw.DataTable
            'lblWithdraw_Index.Text = Whithdraw_index
            If dt_tb_Whitdraw.Rows.Count = 0 Then Exit Sub
            With dt_tb_Whitdraw.Rows(0)

                _Withdraw_no = .Item("Withdraw_No").ToString()
                If objStatus = enuOperation_Type.UPDATE Then
                    txtWithdraw_No.Text = _Withdraw_no
                End If


                txtCustomer_Id.Text = .Item("Customer_Id").ToString()
                Me._Customer_Index = .Item("Customer_Index").ToString()
                txtCustomer_Name.Text = .Item("Title").ToString() + " " + .Item("Customer_Name").ToString()

                txtShipper_ID.Tag = .Item("Shipper_Index").ToString()
                txtShipper_ID.Text = .Item("Supplier_Id").ToString()
                txtShipper_Name.Text = .Item("Supplier_Name").ToString()

                txtConsignee_ID.Tag = .Item("Customer_Shipping_Index").ToString()
                txtConsignee_ID.Text = .Item("Customer_Shipping_Id").ToString()
                txtConsignee_Name.Text = .Item("Company_Name").ToString()


                txtDepartment_Id.Tag = .Item("Department_Index").ToString()
                txtDepartment_Id.Text = .Item("Department_Id").ToString()
                txtDepartment_Name.Text = .Item("Department").ToString()

                getCustomerContact(Me._Customer_Index)



                dtpWithdraw_Date.Text = .Item("Withdraw_Date").ToString
                cboDocumentType.SelectedValue = .Item("DocumentType_Index").ToString()

                txtRef_No1.Text = .Item("Ref_No1").ToString
                txtRef_No2.Text = .Item("Ref_No2").ToString
                txtRef_No3.Text = .Item("Ref_No3").ToString
                txtRef_No4.Text = .Item("Ref_No4").ToString
                txtRef_No5.Text = .Item("Ref_No5").ToString

                txtSo_No.Text = .Item("SO_No").ToString
                'txtASN_No.Text = .Item("ASN_No").ToString
                txtInvoice_No.Text = .Item("Invoice_No").ToString
                txtNote.Text = .Item("str4").ToString
                txtComment.Text = .Item("Comment").ToString

                cboContact_Name.Text = .Item("Contact_Name").ToString

                txtVessel_Name.Text = .Item("Vassel_Name").ToString
                txtFlight_No.Text = .Item("Flight_No").ToString
                ' = .Item("Vehicle_No").ToString
                txtTransport_by.Text = .Item("Transport_by").ToString
                txtOrigin_Port.Text = .Item("Origin_Port_Id").ToString
                txtDestination_Port.Text = .Item("Destination_Port_Id").ToString
                txtOrigin_Country.Text = .Item("Origin_Country_Id").ToString
                txtDestination_Country.Text = .Item("Destination_Country_Id").ToString


                txtTerminal.Text = .Item("Terminal_Id").ToString
                txtTransport_by.Text = .Item("Transport_by").ToString


                If .Item("Departure_Date").ToString <> "" Then
                    dtpDeparture_Date.Value = .Item("Departure_Date").ToShortDateString
                End If
                If .Item("Arrival_Date").ToString <> "" Then
                    dtpArrival_Date.Value = .Item("Arrival_Date").ToShortDateString
                End If


                'txtConsignee_Name
                txtChecker_Name.Text = .Item("Checker_Name").ToString
                txtApprovede_By.Text = .Item("ApprovedBy_Name").ToString

                If .Item("HandlingType_Index").ToString = "" Then
                    cboType.SelectedIndex = 0
                Else
                    cboType.SelectedValue = .Item("HandlingType_Index").ToString
                End If

                Me._Status = .Item("Status")

            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub Load_WhitdrawLocation_Edit(ByVal strDocument_index As String, ByVal Process_Id As Integer)
        Try

            Dim objWithdrawItemLocation As New tb_WithdrawItem
            Dim dtWithdrawItemLocation As DataTable
            dtWithdrawItemLocation = Nothing
            Dim i As Integer = 0

            '---------------------------------------------------------------------
            Select Case Process_Id
                Case 2
                    objWithdrawItemLocation.SearchWithDrawAssetItemLocation("  Withdraw_Index = '" & strDocument_index & "'")
                    dtWithdrawItemLocation = objWithdrawItemLocation.DataTable

                Case 10
                    objWithdrawItemLocation.SearchWithDrawAssetItemLocation("  DocumentPlan_Index = '" & strDocument_index & "' and Plan_Process = 10")
                    dtWithdrawItemLocation = objWithdrawItemLocation.DataTable

                Case 7
                    objWithdrawItemLocation.SearchWithDrawAssetItemLocation("  DocumentPlan_Index = '" & strDocument_index & "' and Plan_Process = 7")
                    dtWithdrawItemLocation = objWithdrawItemLocation.DataTable

            End Select

            If dtWithdrawItemLocation.Rows.Count <= 0 Then Exit Sub
            objStatus = enuOperation_Type.UPDATE
            '--- Add For Show Serial Detail
            dtWithdrawItemLocation.Columns.Add(New DataColumn("Asset_No"))
            dtWithdrawItemLocation.Columns.Add(New DataColumn("AssetSerial_No"))

            For Each odr As DataRow In dtWithdrawItemLocation.Rows
                If odr("AssetLocationBalance_Index").ToString <> "" Then
                    Dim objPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    Dim odt As New DataTable
                    odt = objPick.SEARCHASSETLOCATIONBALANCE_PICKING(" AND AssetLocationBalance_Index ='" & odr("AssetLocationBalance_Index").ToString & "'")
                    odr("Asset_No") = odt.Rows(0).Item("Asset_No").ToString
                    odr("AssetSerial_No") = odt.Rows(0).Item("AssetSerial_No").ToStrin
                    objPick = Nothing
                    odt = Nothing
                End If
                odr("NewItem") = 0
            Next

            grdWithdrawItemLocation.DataSource = dtWithdrawItemLocation
            'setDataSoucreWithdrawItemLocation(dtWithdrawItemLocation)

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Function Get_sku_ratio(ByVal strSku_index As String, ByVal strPackage_index As String) As Integer
        Try
            Dim intRation As Integer = 0
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

    Private Sub getPalletDetail(ByVal _withdraw_index As String)
        ''xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        'Try
        '    Dim objClassDB As New tb_PalletType_History
        '    Dim objDT As DataTable = New DataTable

        '    Try
        '        objClassDB.getpalletWithdraw_Index(Withdraw_Index)
        '        objDT = objClassDB.DataTable
        '        Me.grdPallet.DataSource = Nothing
        '        Me.grdPallet.Rows.Clear()

        '        For i As Integer = 0 To objDT.Rows.Count - 1
        '            With Me.grdPallet
        '                Me.grdPallet.Rows.Add()
        '                .Rows(i).Cells("col_PalletType_History_Index").Value = objDT.Rows(i).Item("PalletType_History_Index").ToString
        '                .Rows(i).Cells("col_Palletindex").Value = objDT.Rows(i).Item("PalletType_Index").ToString
        '                .Rows(i).Cells("col_PalletType").Value = objDT.Rows(i).Item("Pallet_Name").ToString
        '                '  .Rows(i).Cells("col_Palletqty").Value = objDT.Rows(i).Item("PalletQTY").ToString
        '                .Rows(i).Cells("col_Palletqty").Value = objDT.Rows(i).Item("Pallet_Remain").ToString 'objDT.Rows(i).Item("PalletQTY").ToString

        '                If objDT.Rows(i).Item("Qty_Out").ToString = "" Then
        '                    .Rows(i).Cells("col_UsePallet").Value = 0
        '                Else
        '                    .Rows(i).Cells("col_UsePallet").Value = objDT.Rows(i).Item("Qty_Out").ToString
        '                End If
        '                ' .Rows(i).Cells("col_UsePallet").Value = objDT.Rows(i).Item("Qty_Out").ToString

        '            End With
        '        Next
        '    Catch ex As Exception
        '        Throw ex
        '    Finally
        '        objClassDB = Nothing
        '        objDT = Nothing
        '    End Try

        'Catch ex As Exception
        '    Throw ex
        'Finally

        'End Try

    End Sub

    Private Sub getPalletTypeToGRD()
        'Dim objClassDB As New tb_PalletType_History
        'Dim objDT As DataTable = New DataTable

        'Try
        '    objClassDB.getPalletType()
        '    objDT = objClassDB.DataTable
        '    Me.grdPallet.DataSource = Nothing
        '    Me.grdPallet.Rows.Clear()

        '    For i As Integer = 0 To objDT.Rows.Count - 1
        '        With Me.grdPallet
        '            Me.grdPallet.Rows.Add()
        '            .Rows(i).Cells("col_Palletindex").Value = objDT.Rows(i).Item("PalletType_Index").ToString
        '            .Rows(i).Cells("col_PalletType").Value = objDT.Rows(i).Item("Pallet_Name").ToString
        '            .Rows(i).Cells("col_Palletqty").Value = objDT.Rows(i).Item("PalletQTY").ToString
        '        End With
        '    Next
        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    objClassDB = Nothing
        '    objDT = Nothing
        'End Try

    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            'Dim str(1) As String
            'str(0) = "-1"
            'str(1) = "Packing"
            'objDT.Rows.Add(str)

            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
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

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try

            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value ****
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            'New 5-8-2008 10:45
            getCustomerContact(tmpCustomer_Index)
            'SetCustomerShipping(tmpCustomer_Index)

            '------------------------------
            If Not tmpCustomer_Index = "" Then
                Me._Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me._Customer_Index = ""
                Me.txtCustomer_Name.Text = ""
            End If
            ' *********************
            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString
            Else
                Me._Customer_Index = ""
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
    Private Sub btnSeachDepartment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachDepartment.Click
        Try
            Dim frm As New frmDepartment_Popup
            frm.ShowDialog()
            '    *** Recive value ****
            Dim tmpDepartment_Index As String = ""
            tmpDepartment_Index = frm.Department_index
            If Not tmpDepartment_Index = "" Then
                Me.txtDepartment_Id.Tag = tmpDepartment_Index
                Me.getDepartment()
            Else
                Me.txtDepartment_Id.Tag = ""
                Me.txtDepartment_Id.Text = ""
                Me.txtDepartment_Name.Text = ""
            End If
            ' *********************
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub getDepartment()
        Dim objClassDB As New ms_Department(ms_Department.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Department("Department_Index", Me.txtDepartment_Id.Tag.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me.txtDepartment_Id.Tag = objDT.Rows(0).Item("Department_Index").ToString
                Me.txtDepartment_Id.Text = objDT.Rows(0).Item("Department_Id").ToString
                Me.txtDepartment_Name.Text = objDT.Rows(0).Item("Description").ToString
            Else
                Me.txtDepartment_Id.Tag = ""
                Me.txtDepartment_Id.Text = ""
                Me.txtDepartment_Name.Text = ""
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try

            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If

            End If
            Dim frm As New frmConsignee_Popup
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()

            Dim tmp_Index As String = ""
            tmp_Index = frm.Consignee_Index

            If Not tmp_Index = "" Then
                Me.txtConsignee_ID.Tag = tmp_Index
                txtConsignee_Name.Text = frm.Consignee_Name
                txtConsignee_ID.Text = frm.Consignee_ID
            Else
                Me.txtConsignee_ID.Tag = ""
                txtConsignee_Name.Text = ""
                txtConsignee_ID.Text = ""

            End If

            frm.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnShipper_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShipper.Click
        Try
            Dim frm As New frmShipper_Popup
            frm.ShowDialog()

            Dim tmp_Index As String = ""
            tmp_Index = frm.Shipper_Index

            If Not tmp_Index = "" Then
                Me.txtShipper_ID.Tag = tmp_Index
                Me.txtShipper_ID.Text = frm.Shipper_ID
                Me.txtShipper_Name.Text = frm.Shipper_Name
            Else
                Me.txtShipper_ID.Tag = ""
                Me.txtShipper_ID.Text = ""
                Me.txtShipper_Name.Text = ""
            End If

            frm.Close()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getCustomerContact(ByVal Cuntomer_Index As String)

        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getCustomerContact(Cuntomer_Index)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                cboContact_Name.Items.Clear()
                If objDT.Rows(0).Item("Contact_Person").ToString <> "" Then
                    cboContact_Name.Items.Add(objDT.Rows(0).Item("Contact_Person").ToString)
                End If
                If objDT.Rows(0).Item("Contact_Person2").ToString <> "" Then
                    cboContact_Name.Items.Add(objDT.Rows(0).Item("Contact_Person2").ToString)
                End If
                If objDT.Rows(0).Item("Contact_Person3").ToString <> "" Then
                    cboContact_Name.Items.Add(objDT.Rows(0).Item("Contact_Person3").ToString)
                End If
                If cboContact_Name.Items.Count > 0 Then
                    cboContact_Name.SelectedIndex = 0
                Else
                    cboContact_Name.Text = ""
                End If
            Else
                cboContact_Name.Items.Clear()
                cboContact_Name.Text = ""
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub cboContact_Name_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboContact_Name.SelectedIndexChanged
        Try
            If cboContact_Name.Items.Count = 0 Then
                If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnAddItemLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItemAsset.Click
        Try
            If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                W_MSG_Information_ByIndex(8)
                Exit Sub
            End If
            _Remark = ""
            Dim frm As New frmPicking_Reserv_V4(Me.cboDocumentType.SelectedValue.ToString, Me._Withdraw_index, frmPicking_Reserv_V4.Operation.Withdraw)
            frm.withdraw_index = Me._Withdraw_index
            frm.Customer_Id = txtCustomer_Id.Text
            frm.Customer_Index = Me._Customer_Index
            frm.Customer_Name = txtCustomer_Name.Text
            frm.WithDraw_Date = Me.dtpWithdraw_Date.Value
            frm.DocumentPlan_Process = 2
            frm.DocumentPlan_Index = Me._Withdraw_index

            If Me.txtConsignee_ID.Tag IsNot Nothing Then
                frm.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
            End If

            frm.ShowDialog()

            Dim dtLocationBalance As DataTable = frm.objTmpWithDrawItem

            If dtLocationBalance Is Nothing Then
                Exit Sub
            End If

            dtLocationBalance.AcceptChanges()

            _Plan_Process = -9
            _DocumentPlan_No = ""
            _DocumentPlanItem_Index = ""
            _DocumentPlan_Index = ""
            _Reference = ""
            _Reference2 = ""

            _AssetLocationBalance_Index = ""
            _Asset_No = ""
            _AssetSerial_No = ""
            _NewItem = 1

            If SetAUTO_REFERENCE() = 1 Then
                If txtRef_No1.Text <> "" Then
                    _Reference = txtRef_No1.Text
                    _Reference2 = txtRef_No2.Text
                End If
            End If

            setDataSoucreWithdrawItemLocation(dtLocationBalance)
            frm.Close()

            Get_SumData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub setDataSoucreWithdrawItemLocation(ByVal dtLocationBalance As DataTable, ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub
            Dim i As Integer = 0
            With dtLocationBalance.Columns
                If Not .Contains("Reference") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Reference", GetType(String)))
                End If
                If Not .Contains("Reference2") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Reference2", GetType(String)))
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
                If Not .Contains("AssetLocationBalance_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetLocationBalance_Index", GetType(String)))
                End If
                If Not .Contains("Asset_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Asset_No", GetType(String)))
                End If
                If Not .Contains("AssetSerial_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetSerial_No", GetType(String)))
                End If
                If Not .Contains("Seq") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Seq", GetType(Integer)))
                End If
                If Not .Contains("comment") Then
                    dtLocationBalance.Columns.Add(New DataColumn("comment", GetType(String)))
                End If
                If Not .Contains("WithdrawItemLocation_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("WithdrawItemLocation_Index", GetType(String)))
                End If
                If Not .Contains("Status") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Status", GetType(Integer)))
                End If
                If Not .Contains("NewItem") Then
                    dtLocationBalance.Columns.Add(New DataColumn("NewItem", GetType(Integer)))
                End If
                If Not .Contains("Declaration_No_Out") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Declaration_No_Out", GetType(String)))
                End If
                'Consinee_Index
                If Not .Contains("ConsigneeItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("ConsigneeItem_Index", GetType(String)))
                End If
                'Consinee_Name
                If Not .Contains("Company_Name") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Company_Name", GetType(String)))
                End If
            End With

            Dim Seq As Integer = _odtTempItemLocation.Rows.Count + 1
            Dim objInsertItem As New Cl_WithdrawReserv

            For Each odrTemp As DataRow In dtLocationBalance.Rows
                odrTemp("Plan_Process") = _Plan_Process
                odrTemp("DocumentPlan_No") = _DocumentPlan_No
                odrTemp("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                odrTemp("DocumentPlan_Index") = _DocumentPlan_Index
                odrTemp("Reference") = _Reference
                odrTemp("Reference2") = _Reference2
                odrTemp("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                odrTemp("Asset_No") = _Asset_No
                odrTemp("AssetSerial_No") = _AssetSerial_No
                odrTemp("Seq") = Seq
                odrTemp("WithdrawItemLocation_Index") = ""
                odrTemp("comment") = _Remark
                odrTemp("Status") = -9
                odrTemp("NewItem") = _NewItem
                '**************เลขที่ใบขนขาออก************
                odrTemp("Declaration_No_Out") = txtRef_No1.Text
                '**************เลขที่ใบขนขาออก*************
                '**************ผู้รับสินค้า*************
                odrTemp("ConsigneeItem_Index") = _TConsinee_Index
                odrTemp("Company_Name") = _TConsinee_Name
                Seq += 1

                objInsertItem.SaveWithdrawItem_V4(odrTemp, _Withdraw_index, Connection, myTrans, SQLServerCommand)
            Next

            _odtTempItemLocation.Merge(dtLocationBalance)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub setDataSoucreWithdrawItemLocation(ByVal dtLocationBalance As DataTable)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub

            Dim i As Integer = 0
            With dtLocationBalance.Columns
                If Not .Contains("Reference") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Reference", GetType(String)))
                End If
                If Not .Contains("Reference2") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Reference2", GetType(String)))
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
                If Not .Contains("AssetLocationBalance_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetLocationBalance_Index", GetType(String)))
                End If
                If Not .Contains("Asset_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Asset_No", GetType(String)))

                End If
                If Not .Contains("AssetSerial_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetSerial_No", GetType(String)))
                End If
                If Not .Contains("Seq") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Seq", GetType(Integer)))
                End If
                If Not .Contains("comment") Then
                    dtLocationBalance.Columns.Add(New DataColumn("comment", GetType(String)))
                End If
                If Not .Contains("WithdrawItemLocation_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("WithdrawItemLocation_Index", GetType(String)))
                End If
                If Not .Contains("Status") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Status", GetType(Integer)))
                End If
                If Not .Contains("NewItem") Then
                    dtLocationBalance.Columns.Add(New DataColumn("NewItem", GetType(Integer)))
                End If
                If Not .Contains("Declaration_No_Out") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Declaration_No_Out", GetType(String)))
                End If

                'Consinee_Index
                If Not .Contains("ConsigneeItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("ConsigneeItem_Index", GetType(String)))
                End If

                'Consinee_Name
                If Not .Contains("Company_Name") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Company_Name", GetType(String)))
                End If
            End With


            Dim Seq As Integer = grdWithdrawItemLocation.RowCount + 1
            If Me.grdWithdrawItemLocation.RowCount = 0 Then
                For Each odr As DataRow In dtLocationBalance.Rows
                    odr("Plan_Process") = _Plan_Process
                    odr("DocumentPlan_No") = _DocumentPlan_No
                    odr("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                    odr("DocumentPlan_Index") = _DocumentPlan_Index
                    odr("Reference") = _Reference
                    odr("Reference2") = _Reference2

                    odr("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                    odr("Asset_No") = _Asset_No
                    odr("AssetSerial_No") = _AssetSerial_No
                    odr("Seq") = Seq
                    odr("WithdrawItemLocation_Index") = ""
                    odr("comment") = _Remark
                    odr("Status") = -9
                    odr("NewItem") = _NewItem

                    odr("Declaration_No_Out") = txtRef_No1.Text

                    '**************ผู้รับสินค้า*************
                    odr("ConsigneeItem_Index") = _TConsinee_Index
                    odr("Company_Name") = _TConsinee_Name

                    Seq += 1
                Next
                Me.grdWithdrawItemLocation.DataSource = dtLocationBalance
            Else
                Dim odtTempItemLocation As New DataTable
                odtTempItemLocation = Me.grdWithdrawItemLocation.DataSource
                For Each odrTemp As DataRow In dtLocationBalance.Rows
                    odrTemp("Plan_Process") = _Plan_Process
                    odrTemp("DocumentPlan_No") = _DocumentPlan_No
                    odrTemp("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                    odrTemp("DocumentPlan_Index") = _DocumentPlan_Index
                    odrTemp("Reference") = _Reference
                    odrTemp("Reference2") = _Reference2
                    odrTemp("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                    odrTemp("Asset_No") = _Asset_No
                    odrTemp("AssetSerial_No") = _AssetSerial_No
                    odrTemp("Seq") = Seq
                    odrTemp("WithdrawItemLocation_Index") = ""
                    odrTemp("comment") = _Remark
                    odrTemp("Status") = -9
                    odrTemp("NewItem") = _NewItem

                    '**************เลขที่ใบขนขาออก************
                    odrTemp("Declaration_No_Out") = txtRef_No1.Text

                    '**************เลขที่ใบขนขาออก*************



                    '**************ผู้รับสินค้า*************
                    odrTemp("ConsigneeItem_Index") = _TConsinee_Index
                    odrTemp("Company_Name") = _TConsinee_Name


                    Seq += 1
                Next
                'odtTempItemLocation.Merge(dtLocationBalance)


            End If

            Load_WhitdrawLocation_Edit(_Withdraw_index, 2)

        Catch ex As Exception
            Throw ex
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

    Private Sub grdWithdrawItemLocation_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdWithdrawItemLocation.RowsAdded
        Try
            'If e.RowIndex <= -1 Then Exit Sub

            'If SetAUTO_REFERENCE() = 1 Then
            '    If txtRef_No1.Text <> "" Then
            '        grdWithdrawItemLocation.Rows(e.RowIndex).Cells("Col_reference").Value = txtRef_No1.Text
            '        grdWithdrawItemLocation.Rows(e.RowIndex).Cells("Col_reference2").Value = txtRef_No2.Text
            '    End If
            'End If

            ''--- เก็บประเภทของเอกสารการเบิก
            'If grdWithDrawPlan.RowCount <= 0 Then Exit Sub

            'Me.grdWithdrawItemLocation.Rows(e.RowIndex).Cells("col_Plan_Process2").Value = grdWithDrawPlan.Rows(CurrentRowPlanWithDraw_Index).Cells("col_Plan_Process").Value.ToString
            'Me.grdWithdrawItemLocation.Rows(e.RowIndex).Cells("col_Row_WithDrawPlan2").Value = CurrentRowPlanWithDraw_Index
            'Me.grdWithdrawItemLocation.Rows(e.RowIndex).Cells("col_DocumentPlan_No2").Value = grdWithDrawPlan.Rows(CurrentRowPlanWithDraw_Index).Cells("col_DocumentPlan_No").Value.ToString
            'Me.grdWithdrawItemLocation.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index2").Value = grdWithDrawPlan.Rows(CurrentRowPlanWithDraw_Index).Cells("col_DocumentPlanItem_Index").Value.ToString



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Function SetAUTO_REFERENCE() As Integer
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_AUTO_REFERENCE", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim intBlock As Integer = 0
            If objDT.Rows.Count > 0 Then
                intBlock = 1
            Else
                intBlock = 0
            End If

            Return intBlock

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Date 26/03/2010
    ''' By TaTa
    '''   ลบ Item
    '''      - Old Item = Update Total_Qty_Withdraw,Qty_Withdraw,Weight_Withdraw,Volume_Withdraw To tb_SalesOrderItem
    '''      - New Item = คืนค่าจำนวนที่ต้องการเบิก ที่ grdWithDrawPlan
    ''' </remarks>
    Private Sub btnDelItemAsset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelItemAsset.Click
        Try
            If grdWithdrawItemLocation.RowCount = 0 Then
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex("5") = Windows.Forms.DialogResult.Yes Then
                Dim iRow As Integer = grdWithdrawItemLocation.CurrentRow.Index
                DELETE_GRIDWITHDRAWITEM(iRow, True)


                'grdWithdrawItemLocation.DataSource
                Get_SumDataPlan()
                Get_SumData()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub DELETE_GRIDWITHDRAWITEM(ByVal iRow As Integer, ByVal isDeleteWI As Boolean)

        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable) ' Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.ReadUncommitted)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0


        Try

            objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)
            objcon.DBExeNonQuery(" update tb_SalesOrderItem set SalesOrderItem_Index = '0010000000001' where SalesOrderItem_Index = '0010000000001' ", objcon.Connection, myTrans)





            If grdWithdrawItemLocation.Rows(iRow).Cells("Col_LocationBalance_Index2").Value IsNot Nothing Then
                Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                Dim LocationBalance_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("Col_LocationBalance_Index2").Value.ToString
                Dim Qty_Reserv As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Qty_sku2").Value.ToString)
                Dim Weight_Reserv As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Weight2").Value.ToString)
                Dim Volume_Reserv As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Volume2").Value.ToString)
                Dim ItemQty_Reserv As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("col_ItemQty2").Value.ToString)
                Dim Price_Reserv As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("col_Price2").Value)

                'Dong_kk Add Date : 2011/06/7
                Dim Package_Index_AC As String = grdWithdrawItemLocation.Rows(iRow).Cells("Col_Package_IndexWithdraw2").Value.ToString
                Dim Qty_Recieve_Package_AC As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Withdraw_Package_Qty2").Value)
                Dim ReserveQty_AC As Decimal = Qty_Reserv
                Dim ReserveWeight_AC As Decimal = Weight_Reserv
                Dim ReserveVolume_AC As Decimal = Volume_Reserv
                Dim ReserveQty_Item_AC As Decimal = ItemQty_Reserv
                Dim ReserveOrderItem_Price_AC As Decimal = Price_Reserv


                objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 2, Me._Withdraw_index, "ลบจองจากหน้า WithdrawAsset", LocationBalance_Index, 0, 0, 0, 0, 0, 0, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                If grdWithdrawItemLocation.Rows(iRow).Cells("col_AssetLocationBalance_Index").Value IsNot Nothing Then
                    If grdWithdrawItemLocation.Rows(iRow).Cells("col_AssetLocationBalance_Index").Value <> "" Then
                        Dim _AssetLocationBalance_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("col_AssetLocationBalance_Index").Value.ToString
                        objdelPick.DELETE_RESERV_ASSETLOCATIONBALANCE(_AssetLocationBalance_Index, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)
                    End If
                End If
            End If
            If isDeleteWI Then
                If grdWithdrawItemLocation.Rows(iRow).Cells("col_WithDrawItem_Index2").Value IsNot Nothing Then

                    Dim objSerial As New tb_WithdrawItemSerial
                    Dim objWithDraw As New tb_WithdrawItemLocation

                    If objSerial.CheckIsSerial(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Sku_Index2").Value, objcon.Connection, myTrans) = True Then
                        If objWithDraw.isItemSerial(grdWithdrawItemLocation.Rows(iRow).Cells("col_WithDrawItem_Index2").Value.ToString, objcon.Connection, myTrans) = True Then
                            MessageBox.Show("ไม่สามารถลบรายการได้ เนื่องจากมีการสร้าง Serial แล้ว", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            objWithDraw = Nothing
                            Exit Sub
                        End If
                    End If

                    Dim strWithDrawItem_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("col_WithDrawItem_Index2").Value.ToString

                    objWithDraw.delete_WithDrawItem(strWithDrawItem_Index, objcon.Connection, myTrans)

                End If
            End If


            '--- CHECK DELETE PLAN

            With grdWithdrawItemLocation

                Dim strDocumentPlanItem_Index As String = .Rows(iRow).Cells("col_DocumentPlanItem_Index2").Value.ToString
                Dim strPlan_Process As String = .Rows(iRow).Cells("col_Plan_Process2").Value.ToString
                Dim strDocumentPlan_Index As String = .Rows(iRow).Cells("col_DocumentPlan_Index2").Value.ToString
                Dim intRowPlan As Integer

                If strPlan_Process <> -9 Then
                    Dim oWithDrawItem As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.DELETE)
                    Dim dblWithDrawWeight As Decimal = CDec(.Rows(iRow).Cells("Col_Weight2").Value.ToString)
                    Dim dblWithDrawVolume As Decimal = CDec(.Rows(iRow).Cells("Col_Volume2").Value.ToString)
                    Dim dblWithDrawQty As Decimal = CDec(.Rows(iRow).Cells("Col_Withdraw_Package_Qty2").Value.ToString)
                    Dim dblWItemQty As Decimal = CDec(.Rows(iRow).Cells("col_ItemQty2").Value.ToString)
                    Dim dblWTotalQty As Decimal = CDec(.Rows(iRow).Cells("Col_Qty_sku2").Value.ToString)
                    If grdWithDrawPlan.RowCount > 0 Then
                        If .Rows(iRow).Cells("col_DocumentPlanItem_Index2").Value IsNot Nothing Then
                            For intRowPlan = 0 To grdWithDrawPlan.Rows.Count - 1
                                If (strDocumentPlanItem_Index = grdWithDrawPlan.Rows(intRowPlan).Cells("col_DocumentPlanItem_Index").Value.ToString) And (strPlan_Process = grdWithDrawPlan.Rows(intRowPlan).Cells("col_Plan_Process").Value.ToString) Then
                                    '        '--- จำนวนที่ต้องเบิก ก่อนลบจาก WithDraw
                                    Dim dblPlanQty As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value.ToString)
                                    Dim dblPlanItemQty As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Item_Qty").Value.ToString)
                                    Dim dblQtyNeed As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_Plan").Value.ToString)
                                    Dim dblPlanTotalQty As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Total_Qty_Paln").Value.ToString)
                                    Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value = dblPlanQty - dblWithDrawQty
                                    Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Item_Qty").Value = dblPlanItemQty - dblWItemQty
                                    Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Total_Qty_Paln").Value = dblPlanTotalQty - dblWTotalQty
                                    If grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value < dblQtyNeed Then
                                        grdWithDrawPlan.Rows(intRowPlan).DefaultCellStyle.BackColor = Color.Gainsboro
                                        grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Style.BackColor = Color.WhiteSmoke
                                    End If
                                End If
                            Next
                        End If
                    End If
                    oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, dblWithDrawQty, dblWTotalQty, dblWItemQty, dblWithDrawWeight, dblWithDrawVolume, strPlan_Process, objcon.Connection, myTrans)
                End If
            End With
            grdWithdrawItemLocation.Rows.RemoveAt(iRow)
            myTrans.Commit()

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            objcon.disconnectDB()
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' 15-01-201
    ''' ja
    ''' คืนค่า reserver
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmWithdrawAsset_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0

        Try
            Dim objDeleteWithdraw As New Cl_WithdrawReserv

            If grdWithdrawItemLocation.Rows.Count > 0 Then

                Dim dtSaveWithDraw As New DataTable
                dtSaveWithDraw = CType(grdWithdrawItemLocation.DataSource, DataTable)
                dtSaveWithDraw.AcceptChanges()
                Dim drArrSaveWithDraw() As DataRow = dtSaveWithDraw.Select("NewItemFlag='1'")


                For Each drSaveWithDraw As DataRow In drArrSaveWithDraw

                    objcon.DBExeNonQuery(String.Format("update tb_LocationBalance set LocationBalance_Index = LocationBalance_Index where LocationBalance_Index = '{0}'", drSaveWithDraw("LocationBalance_Index").ToString), objcon.Connection, myTrans)


                    Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    Dim LocationBalance_Index As String = drSaveWithDraw("LocationBalance_Index").ToString
                    Dim Total_Qty_Reserv As Decimal = CDec(drSaveWithDraw("Total_Qty").ToString)
                    Dim Weight_Reserv As Decimal = CDec(drSaveWithDraw("WeightOut").ToString)
                    Dim Volume_Reserv As Decimal = CDec(drSaveWithDraw("VolumeOut").ToString)
                    Dim ItemQty_Reserv As Decimal = CDec(drSaveWithDraw("QtyItemOut").ToString)
                    Dim Price_Reserv As Decimal = CDec(drSaveWithDraw("Price_Out").ToString)
                    Dim Qty_Reserv As Decimal = CDec(drSaveWithDraw("Qty").ToString)


                    objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 2, Me._Withdraw_index, "ออกโดยไม่บันทึก WithdrawAsset", LocationBalance_Index, _
                                   0, 0, 0, 0, 0, 0, _
                                   Total_Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)




                    If String.IsNullOrEmpty(drSaveWithDraw("WithDrawItem_Index")) = False Then
                        objDeleteWithdraw.DeleteWithdrawItem(objcon.Connection, myTrans, drSaveWithDraw("WithDrawItem_Index"))
                    End If

                    Dim strDocumentPlanItem_Index As String = drSaveWithDraw("DocumentPlanItem_Index").ToString
                    Dim strPlan_Process As String = drSaveWithDraw("Plan_Process").ToString
                    Dim strDocumentPlan_Index As String = drSaveWithDraw("DocumentPlan_Index").ToString

                    Select Case strPlan_Process
                        Case "10"
                            Dim oWithDrawItem As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.DELETE)
                            oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, Qty_Reserv, Total_Qty_Reserv, ItemQty_Reserv, Weight_Reserv, Volume_Reserv, strPlan_Process, objcon.Connection, myTrans)
                        Case "7"

                        Case Else

                    End Select
                Next


            End If

            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    objDeleteWithdraw.DeleteWithdraw(objcon.Connection, myTrans, _Withdraw_index)

                Case enuOperation_Type.UPDATE
                    objDeleteWithdraw.ChkDelete(objcon.Connection, myTrans, _Withdraw_index)
            End Select

            If Me._ArrSalesOrder_Index IsNot Nothing Then
                Dim objSalesOrder As New WMS_STD_OUTB_SO_Datalayer.tb_SalesOrder
                For Each Str As String In _ArrSalesOrder_Index
                    objSalesOrder.IsCreateWithdraw_Clear(objcon.Connection, myTrans, Str)
                Next
            End If

            objDeleteWithdraw.UpdateUseDoc(objcon.Connection, myTrans, Cl_WithdrawReserv.CaseReserve.ClearReserve, _Withdraw_index)

            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' 14-01-2010
    ''' ja
    ''' update save str6 - str 10 
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Key In Tax
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try

            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 2)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdWithdrawItemLocation, 2)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdWithDrawPlan, 2)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdWithDrawTruckOut, 2)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""



            If grdWithdrawItemLocation.RowCount = 0 Then
                If W_MSG_Confirm(GetMessage_Data(100045)) = Windows.Forms.DialogResult.No Then
                    Exit Sub
                End If
            End If

            If grdWithdrawItemLocation.RowCount <= 0 Then Exit Sub
            Dim dtpMinOrder_Date As Date
            Dim dtTemp As New DataTable
            dtTemp = CType(grdWithdrawItemLocation.DataSource, DataTable)
            dtpMinOrder_Date = CDate(dtTemp.Compute("max(Order_Date)", "Sku_Index <> ''"))

            If CDate(dtpWithdraw_Date.Value.ToString("dd/MM/yyyy")) < CDate(dtpMinOrder_Date.ToString("dd/MM/yyyy")) Then
                dtpWithdraw_Date.Value = dtpTempWithDraw_Date
                W_MSG_Information(GetMessage_Data("400020") & CDate(dtpMinOrder_Date).ToString("dd/MM/yyyy"))
                Exit Sub
            End If



            Dim strReturn As String = ""
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    strReturn = Save_WhitdrawItem()

                Case enuOperation_Type.UPDATE
                    Dim dtSerial As New DataTable
                    Dim objGetSerial As New tb_WithdrawItemSerial
                    objGetSerial.getWithdrawSerial(_Withdraw_index)
                    dtSerial = objGetSerial.DataTable
                    If dtSerial.Rows.Count > 0 Then
                        dtpMinOrder_Date = CDate(dtSerial.Compute("max(Order_Date)", "Sku_Index <> ''"))
                        If CDate(dtpWithdraw_Date.Value.ToString("dd/MM/yyyy")) < CDate(dtpMinOrder_Date.ToString("dd/MM/yyyy")) Then
                            W_MSG_Information("มี Serial เบิกที่ วันที่รับมากกว่าวันเบิก กรุณาเลือกวันที่ใหม่ที่มากกว่าวันที่ :" & CDate(dtpMinOrder_Date).ToString("dd/MM/yyyy"))
                            Exit Sub
                        End If
                    End If

                    Dim Whithdraw_index As String = _Withdraw_index
                    Updatewithdraw_index = Whithdraw_index
                    strReturn = Save_WhitdrawItem()

                Case enuOperation_Type.NULL
                    If IsAdmin Then
                        If _OldWithdraw_No <> txtWithdraw_No.Text Then
                            Dim obj_withdrawTra As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.UPDATE)
                            obj_withdrawTra.Updatewithdraw_Transaction(Me._OldWithdraw_No, Me.Withdraw_Index)
                        End If
                    End If
            End Select

            'จำเป็นมากเพราะต้อง update รายการที่บันทึกแล้วไม่งั้นการคืน Reserve จะผิด (ถ้าไม่มีการ Load data หลัง Save เรียบร้อยแล้วให้เปิด comment)
            If strReturn = "PASS" Then
                For iRow As Integer = 0 To Me.grdWithdrawItemLocation.RowCount - 1
                    Me.grdWithdrawItemLocation.Rows(iRow).Cells("col_NewItem2").Value = 0
                Next
            End If



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' -------------------------------------------------
    ''' Update Date : 11/02/2010
    ''' Update By   : ja
    ''' Update For  : เพิ่ม Column withdrawitem Str6/forsmart depot
    ''' -------------------------------------------------
    ''' </summary>
    ''' <remarks></remarks>
    Function Save_WhitdrawItem() As String


        Dim objHeader As New tb_Withdraw
        Dim objItem As New tb_WithdrawItem
        Dim objDBItemWithdrawItemLocation As New tb_WithdrawItemLocation

        Dim objPalletType As New tb_PalletType_History
        Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

        Dim objItemCollection As New List(Of tb_WithdrawItem)
        Dim objItemCollectionWITL As New List(Of tb_WithdrawItemLocation)

        '--- ไม่ Group ละ ไม่ชอบ
        'group_Whithdraw_item(grdWithdrawItemLocation) 'Group WhiteDraw_Item

        Try
            ' *********** Define Value for Header ***********
            objHeader.Withdraw_Index = Me._Withdraw_index
            objHeader.Withdraw_Date = CDate(Me.dtpWithdraw_Date.Value.ToString("yyyy/MM/dd"))
            objHeader.Withdraw_No = Me.txtWithdraw_No.Text

            '--- ประเภทการเบิก
            If cboDocumentType.SelectedValue Is Nothing Then
                objHeader.DocumentType_Index = ""
            Else
                objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue
            End If

            'ja 04-03-2010 add Set เลขที่เอกวารใบเบิกตาม formatลูกค้า
            If txtWithdraw_No.Text = "" Then
                Dim strWhere As String = ""
                Dim objDocumentNumber As New clsDocumentNumber()
                objHeader.Withdraw_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, Me.dtpWithdraw_Date.Value) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                txtWithdraw_No.Text = objHeader.Withdraw_No
                _Withdraw_no = objHeader.Withdraw_No
                objDocumentNumber = Nothing
            End If

            '--- ลูกค้า Need 
            objHeader.Customer_Index = Me._Customer_Index.ToString
            '--- ผู้ติดต่อ
            If cboContact_Name IsNot Nothing Then
                objHeader.Contact_Name = Me.cboContact_Name.Text
            End If
            '--- แผนก
            If Me.txtDepartment_Id.Tag Is Nothing Then
                objHeader.Department_Index = ""
            Else
                objHeader.Department_Index = Me.txtDepartment_Id.Tag.ToString
            End If
            '--- ผู้รับ
            If txtConsignee_ID.Tag Is Nothing Then
                objHeader.Customer_Shipping_Index = ""
            Else
                objHeader.Customer_Shipping_Index = txtConsignee_ID.Tag.ToString
            End If
            '--- ผู้ขาย
            If Me.txtShipper_ID.Tag Is Nothing Then
                objHeader.Shipper_Index = ""
            Else
                objHeader.Shipper_Index = Me.txtShipper_ID.Tag.ToString
            End If


            '--- Referrent
            objHeader.Ref_No1 = Me.txtRef_No1.Text                              '3PL M. B/L, MAWB               INH เอกสารอ้างอิง1
            objHeader.Ref_No2 = Me.txtRef_No2.Text                              '3PL H. B/L, HAWB               INH เอกสารอ้างอิง2
            objHeader.Ref_No3 = Me.txtRef_No3.Text                              '3PL วันขึ้นรถ ย้ายไป Truck out จ้า        INH เอกสารอ้างอิง3
            objHeader.Ref_No4 = Me.txtRef_No4.Text                              '3PL Declaration No.            INH เอกสารอ้างอิง4
            objHeader.Ref_No5 = Me.txtRef_No5.Text                              '3PL To Terminal                INH เอกสารอ้างอิง5

            '--- Text For Assign You Can Edit For Site
            objHeader.Str1 = ""
            objHeader.Str2 = ""
            objHeader.Str3 = ""
            objHeader.Str4 = Me.txtNote.Text                                    'Use For Deimos
            objHeader.Str5 = ""
            objHeader.Str6 = ""                               'ประเภทพาหนะ
            objHeader.Str7 = ""
            objHeader.Str8 = ""
            objHeader.Str9 = ""
            objHeader.Str10 = ""

            '--- Float For Assigne You Can Edit
            objHeader.Flo1 = 0
            objHeader.Flo2 = 0
            objHeader.Flo3 = 0
            objHeader.Flo4 = 0
            objHeader.Flo5 = 0


            '--- Document Import Data       
            objHeader.SO_No = txtSo_No.Text                               'Use For Deimos
            'objHeader.ASN_No = txtASN_No.Text                                   'Use For Deimos เลขที่ Seal
            objHeader.Invoice_No = txtInvoice_No.Text
            objHeader.Comment = txtComment.Text
            objHeader.Withdraw_Type = 0 'chkWithdrawType.Checked

            '--- รายละเอียดการนำออก Use For 3PL นะ ส่วนมาก

            objHeader.Vassel_Name = Me.txtVessel_Name.Text                      'ชื่อเรือ (Vessel Name)
            objHeader.Flight_No = Me.txtFlight_No.Text                          'เที่ยวบิน (Flight No)
            objHeader.Vehicle_No = ""                                           ' Change In Truck Out
            objHeader.Transport_by = Me.txtTransport_by.Text                    ' ประเภทพาหนะ
            objHeader.Origin_Port_Id = Me.txtOrigin_Port.Text                   'Port ต้นทาง
            objHeader.Destination_Port_Id = Me.txtDestination_Port.Text         'Port ปลายทาง
            objHeader.Origin_Country_Id = Me.txtOrigin_Country.Text             'ประเทศต้นทาง
            objHeader.Destination_Country_Id = Me.txtDestination_Country.Text   'ประเทศปลายทาง
            objHeader.Terminal_Id = Me.txtTerminal.Text                         'ส่ง Terminal/WA
            objHeader.Departure_Date = Me.dtpDeparture_Date.Value.Date          'Departure_Date
            objHeader.Arrival_Date = Me.dtpArrival_Date.Value.Date              'Arrival_Date
            objHeader.Checker_Name = txtChecker_Name.Text.ToString              'Checker_Name
            objHeader.ApprovedBy_Name = txtApprovede_By.Text.ToString           'Approvede_By


            '---- Delivery Information Don't Use น่าจาลบ เพราะ เห็นว่าย้ายไปที่ ย้ายไปที่ tb_WithDrawTruckOut แล้วนิ จ๊ะ
            objHeader.Driver_Index = "" ' cbDriver.SelectedValue.ToString
            objHeader.Round = Date.Now
            objHeader.Leave_Time = Date.Now
            objHeader.Factory_In = Date.Now
            objHeader.Factory_Out = Date.Now
            objHeader.Return_Time = Date.Now





            '######  End Header #####

            '*************************************************
            Dim i As Integer = 0
            For i = 0 To grdWithdrawItemLocation.Rows.Count - 1

                With grdWithdrawItemLocation
                    objItem = New tb_WithdrawItem
                    '--- Withdraw_Index
                    objItem.Withdraw_Index = objHeader.Withdraw_Index
                    '--- Create WithdrawItem_Index
                    If .Rows(i).Cells("col_WithDrawItem_Index2").Value = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        .Rows(i).Cells("col_WithDrawItem_Index2").Value = objDBIndex.getSys_Value("WithdrawItem_Index")
                        objItem.WithdrawItem_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_WithDrawItem_Index2").Value, GetType(String))
                        objDBIndex = Nothing
                    Else
                        objItem.WithdrawItem_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_WithDrawItem_Index2").Value, GetType(String))
                    End If
                    '--- ReTrun For Update
                    .Rows(i).Cells("col_WithDrawItem_Index2").Value = objItem.WithdrawItem_Index
                    '--- LocationBalane_index
                    objItem.LocationBalane_index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_LocationBalance_Index2").Value.ToString, GetType(String))

                    '--- SKU_Index
                    If .Rows(i).Cells("col_Sku_Index2").Value IsNot Nothing Then
                        objItem.Sku_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Sku_Index2").Value, GetType(String))
                    End If
                    '--- ItemStatus_Index
                    If .Rows(i).Cells("col_ItemStatus_Index2").Value IsNot Nothing Then
                        objItem.ItemStatus_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemStatus_Index2").Value, GetType(String))
                    End If
                    '--- QTY จำนวนที่เบิก
                    objItem.Qty = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Withdraw_Package_Qty2").Value, GetType(String))

                    If objItem.Qty = 0 Then
                        Return "Qty is Zero"
                    End If

                    '--- Package_Index หน่วยที่เบิก
                    objItem.Package_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Package_IndexWithdraw2").Value, GetType(String))
                    '--- RATIO 
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
                    objRatio = Nothing
                    ' *****************

                    ' *** Calculate Tatal Qty *** 
                    '--- Total_Qty จำนวนของหน่วยหลัก 
                    'objItem.Total_Qty = objItem.Qty * objItem.Ratio
                    Dim Total_Qty As Decimal = 0
                    Total_Qty = CType(grdWithdrawItemLocation.DataSource, DataTable).Rows(i).Item("Total_Qty")
                    objItem.Total_Qty = Total_Qty

                    ' *** set Total_Qty = 0 ***
                    '--- Plan_Qty จำนวนคาดว่าจะบิก ถูกกำหนดจากการ Import เช่น SO PACKING
                    '--- Set Plan_Qty = Total_Qty เพราะว่า จำนวนที่คาดว่า น่าจะเท่ากับ จำนวนที่เบิก
                    objItem.Plan_Qty = objItem.Qty
                    objItem.Plan_Total_Qty = objItem.Total_Qty

                    '   --- ITEM_QTY
                    objItem.ItemQty = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemQty2").Value, GetType(String))

                    '--- WEIGHT
                    objItem.Weight = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Weight2").Value, GetType(String))

                    '--- VOLUME
                    objItem.Volume = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Volume2").Value, GetType(String))

                    '--- ILOT/BATH
                    objItem.PLot = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Plot2").Value, GetType(String))

                    '--- CERTIFICATE
                    objItem.Serial_No = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Serial_No2").Value, GetType(String))

                    '--- ADDNEW NEED
                    objItem.NewItem = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_NewItem2").Value, GetType(Integer))

                    '--- PRICE
                    objItem.Price = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Price2").Value, GetType(String))

                    '  -------------------------- DOCUMENT -----------------------------
                    objItem.Item_Package_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Item_Package_Index2").Value, GetType(String))

                    '--- DECLARATION
                    objItem.Declaration_No = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Declaration_No_Out").Value, GetType(String))

                    '--- HANDLING TYPE
                    objItem.HandlingType_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("cbo_HandlingType2").Value, GetType(String))

                    '--- ADDNEW NEED
                    objItem.Invoice_No = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Invoice_Out2").Value, GetType(String))
                    '--- String 

                    '***********************    REFERECNE   *************************

                    If SetAUTO_REFERENCE() = 1 Then
                        If .Rows(i).Cells("Col_Reference").Value = "" Then
                            W_MSG_Error_ByIndex(400019)
                            Me.grdWithdrawItemLocation.Rows(i).Selected = True
                            Return "Reference1 is no data"
                        Else
                            objItem.Str1 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Reference").Value, GetType(String))
                        End If
                        '--- Referecne 2
                        If .Rows(i).Cells("col_Reference2").Value = "" Then
                            W_MSG_Error(GetMessage_Data(400019) & "2")
                            Me.grdWithdrawItemLocation.Rows(i).Selected = True
                            Return "Reference2 is no data"
                        Else
                            objItem.Str2 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Reference2").Value, GetType(String))
                        End If
                        '--- ItemDefinition_Index
                        If .Rows(i).Cells("col_ItemDefinition_Index").Value Is Nothing Then

                            W_MSG_Error_ByIndex(400021)
                            Me.grdWithdrawItemLocation.Rows(i).Selected = True
                            Return "ItemDefinition is no data"
                        Else
                            objItem.ItemDefinition_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemDefinition_Index").Value, GetType(String))
                        End If
                    Else

                        objItem.Str1 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Reference").Value, GetType(String))

                        '--- Referecne 2
                        objItem.Str2 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Reference2").Value, GetType(String))

                        '--- ItemDefinition_Index
                        objItem.ItemDefinition_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemDefinition_Index").Value, GetType(String))

                    End If

                    '************************************************************************

                    '--- INVOICE
                    objItem.Str3 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Invoice_In2").Value, GetType(String))

                    '--- PALLET NO
                    objItem.Str4 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Pallet_No2").Value, GetType(String))

                    '--- PALLET NO
                    objItem.Str5 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Comment").Value, GetType(String))

                    '--- Plan WithDraw 
                    '--- Type
                    objItem.Plan_Process = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Plan_Process2").Value, GetType(Integer))
                    If objItem.Plan_Process = 0 Then
                        objItem.Plan_Process = -9
                    End If
                    '--- Index
                    objItem.DocumentPlan_No = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_DocumentPlan_No2").Value.ToString, GetType(String))

                    '--- ItemIndex
                    objItem.DocumentPlanItem_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_DocumentPlanItem_Index2").Value.ToString, GetType(String))

                    objItem.DocumentPlan_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_DocumentPlan_Index2").Value.ToString, GetType(String))

                    '--- AssetLocationBalance_Index
                    objItem.AssetLocationBalance_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_AssetLocationBalance_Index").Value.ToString, GetType(String))

                    objItem.Seq = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_Seq").Value, GetType(Integer))
                    If objItem.Seq = 0 Then
                        objItem.Seq = i + 1
                    End If
                    '--- Empty Float
                    '--- WEIGHT
                    objItem.Flo1 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_2Flo1").Value.ToString, GetType(Decimal))

                    '   objItem.Flo1 = 0
                    objItem.Flo2 = 0
                    objItem.Flo3 = 0
                    objItem.Flo4 = 0
                    objItem.Flo5 = 0

                    'tax
                    objItem.Tax1 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax21").Value, GetType(Decimal))

                    objItem.Tax2 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax22").Value, GetType(Decimal))

                    objItem.Tax3 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax23").Value, GetType(Decimal))

                    objItem.Tax4 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax24").Value, GetType(Decimal))

                    objItem.Tax5 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Tax25").Value, GetType(Decimal))


                    '11-02-2010 ja Update str6
                    objItem.Str6 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_2Str6").Value.ToString, GetType(String))


                    objItem.HS_Code = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_HS_Code2").Value.ToString, GetType(String))

                    objItem.ItemDescription = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ItemDescription2").Value.ToString, GetType(String))


                    objItem.Consignee_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("col_ConsigneeItem_Index2").Value.ToString, GetType(String))
                    If txtConsignee_ID.Tag <> "" Then
                        If objItem.Consignee_Index = "" Then
                            objItem.Consignee_Index = txtConsignee_ID.Tag
                        End If
                    End If



                    '16-02-2010 ja OrderItem_Index
                    objItem.OrderItem_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Col_Orderitem_WIL2").Value.ToString, GetType(String))


                    If String.IsNullOrEmpty(.Rows(i).Cells("Col_ERP_Location").Value.ToString) = False Then
                        objItem.ERP_Location = .Rows(i).Cells("Col_ERP_Location").Value.ToString
                    Else
                        objItem.ERP_Location = ""
                    End If


                    '--- ADD For WithDrawItemLocation
                    objDBItemWithdrawItemLocation = New tb_WithdrawItemLocation

                    If .Rows(i).Cells("col_WithDrawItemLocation_Index2").Value.ToString = "" Then
                        Dim objDBWIndex As New Sy_AutoNumber
                        objDBItemWithdrawItemLocation.WithdrawItemLocation_Index = objDBWIndex.getSys_Value("WithdrawItemLocation_Index")
                        .Rows(i).Cells("col_WithDrawItemLocation_Index2").Value = objDBItemWithdrawItemLocation.WithdrawItemLocation_Index
                        objDBWIndex = Nothing
                    Else
                        objDBItemWithdrawItemLocation.WithdrawItemLocation_Index = .Rows(i).Cells("col_WithDrawItemLocation_Index2").Value
                    End If



                    '''''''Killz  GEN TAGOUT NO FOR TB_WithDrawItemLocation   

                    'tagout_no = GENTAGOUT_NO(_Withdraw_no, objDBItemWithdrawItemLocation.WithdrawItemLocation_Index)
                    Dim tagout_no As String = ""
                    If Config_PrintPalletSlip() = 1 Then
                        Dim objGEN_NO As New Sy_AutoyyyyMM_WithDrawTag
                        tagout_no = objGEN_NO.Auto_DocumentType_Number_NT(cboDocumentType.SelectedValue, dtpWithdraw_Date.Value, "")
                    End If

                    objDBItemWithdrawItemLocation.TAG_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_TAG_Index").Value.ToString
                    objDBItemWithdrawItemLocation.Withdraw_Index = Me._Withdraw_index
                    objDBItemWithdrawItemLocation.WithdrawItem_Index = grdWithdrawItemLocation.Rows(i).Cells("col_WithDrawItem_Index2").Value.ToString
                    objDBItemWithdrawItemLocation.Order_Index = grdWithdrawItemLocation.Rows(i).Cells("col_Order_Index2").Value.ToString
                    objDBItemWithdrawItemLocation.Sku_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_Sku_Index2").Value.ToString
                    objDBItemWithdrawItemLocation.Lot_No = ""
                    objDBItemWithdrawItemLocation.Plot = grdWithdrawItemLocation.Rows(i).Cells("Col_PLot2").Value.ToString
                    objDBItemWithdrawItemLocation.ItemStatus_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_ItemStatus_Index2").Value.ToString
                    objDBItemWithdrawItemLocation.Tag_No = grdWithdrawItemLocation.Rows(i).Cells("Col_Tag_No2").Value.ToString
                    objDBItemWithdrawItemLocation.LocationBalance_Index = grdWithdrawItemLocation.Rows(i).Cells("Col_LocationBalance_Index2").Value.ToString
                    objDBItemWithdrawItemLocation.Location_Index = grdWithdrawItemLocation.Rows(i).Cells("col_Location_Index2").Value.ToString
                    objDBItemWithdrawItemLocation.Serial_No = grdWithdrawItemLocation.Rows(i).Cells("Col_Serial_No2").Value.ToString
                    objDBItemWithdrawItemLocation.Qty = objItem.Qty ' CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Withdraw_Package_Qty2").Value.ToString)
                    objDBItemWithdrawItemLocation.Package_Index = objItem.Package_Index 'grdWithdrawItemLocation.Rows(i).Cells("Col_Package_IndexWithdraw2").Value.ToString
                    objDBItemWithdrawItemLocation.Total_Qty = objItem.Total_Qty ' CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Qty_sku2").Value.ToString)
                    objDBItemWithdrawItemLocation.Weight = CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Weight2").Value.ToString)
                    objDBItemWithdrawItemLocation.Volume = CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Volume2").Value.ToString)
                    objDBItemWithdrawItemLocation.Pallet_Qty = CDec(grdWithdrawItemLocation.Rows(i).Cells("Col_Pallet_Qty2").Value.ToString)
                    objDBItemWithdrawItemLocation.Item_Qty = CDec(grdWithdrawItemLocation.Rows(i).Cells("col_ItemQty2").Value.ToString)
                    objDBItemWithdrawItemLocation.Price = CDec(grdWithdrawItemLocation.Rows(i).Cells("col_Price2").Value.ToString)
                    objDBItemWithdrawItemLocation.Status = grdWithdrawItemLocation.Rows(i).Cells("Col_Status_WIL").Value.ToString
                    objDBItemWithdrawItemLocation.TagOut_No = tagout_no

                End With

                ' *** Add value ***
                objItemCollectionWITL.Add(objDBItemWithdrawItemLocation)
                objItemCollection.Add(objItem)

            Next

            ' *** Call Class for Manage Data ***
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDB As New WithdrawTransaction(enuOperation_Type.UPDATE, objHeader, objItemCollection, objItemCollectionWITL, objPalletTypeCollection)
                    Me._Withdraw_index = objDB.SaveData
                    objDB = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDB As New WithdrawTransaction(enuOperation_Type.UPDATE, objHeader, objItemCollection, objItemCollectionWITL, objPalletTypeCollection)
                    objDB.DeleteWhitdrawItem_Index = Updatewithdraw_index
                    Me._Withdraw_index = objDB.SaveData
                    objDB = Nothing
            End Select

            '-2:	    กำลังสร้าง
            '-1:	    ยกเลิก
            '0:          ไม่ระบุ()
            '1:          รอยืนยัน()
            '2:          เสร็จสิ้น()
            '3:          เบิกบางส่่วน()
            '4:          PICKING(บางส่วน)
            '5:          PICKING(เสร็จสิ้น)

            Select Case Me._Status
                Case 0, -2
                    Dim objcon As New DBType_SQLServer
                    objcon.DBExeNonQuery(String.Format("update tb_Withdraw set Status = {0} where withdraw_index= '{1}'", 1, Me.Withdraw_Index))
                Case Else
                    'KSL : Fix bug reset staus
                    Dim objcon As New DBType_SQLServer
                    objcon.DBExeNonQuery(String.Format("update tb_Withdraw set Status = {0} where withdraw_index= '{1}'", Me._Status, Me.Withdraw_Index))

                    Dim xSql As String = ""
                    xSql &= " Update tb_WithdrawItem "
                    xSql &= " set TransportManifest_Index = (select top 1  tb_TransportManifest.TransportManifest_Index"
                    xSql &= " 								from tb_SalesOrder SO "
                    xSql &= " 									inner join tb_TransportManifest on tb_TransportManifest.TransportManifest_No = SO.TransportManifest_No"
                    xSql &= " 								WHERE SO.SalesOrder_Index =tb_WithdrawItem.DocumentPlan_Index and  tb_WithdrawItem.Plan_Process = 10)"
                    xSql &= " "
                    xSql &= " where Withdraw_Index = '" & Me.Withdraw_Index & "'"
                    objcon.DBExeNonQuery(xSql)
                    objcon = Nothing
                    '' **********************************
            End Select

            '------ Update Comment Image
            Dim odjWithdrawImage As New tb_Withdraw_Image

            For iRow As Integer = 0 To grdWithdrawImage.RowCount - 1
                With odjWithdrawImage
                    .Withdraw_Index = _Withdraw_index
                    .Withdraw_Image_Index = grdWithdrawImage.Rows(iRow).Cells("Col_Withdraw_ImageIndex").Value.ToString
                    .Comment = grdWithdrawImage.Rows(iRow).Cells("ColComment").Value.ToString
                    .Update()
                End With
            Next

            If Not Me._Withdraw_index = "" Then
                W_MSG_Information_ByIndex(1)
                Me.btnSave.Enabled = False
                'Truck
                btnAdd_TruckOut.Enabled = False
                btnEdit_TruckOut.Enabled = False
                btnDelete_TruckOut.Enabled = False
                'Asset
                btnAddItemAsset_Barcode.Enabled = False
                btnAddItemAsset.Enabled = False
                btnDelItemAsset.Enabled = False
                'Plan
                btnDelPlan.Enabled = False
                btnAddPlan.Enabled = False

                btnAssignJob.Enabled = False

                btnPrint.Enabled = True
                cboPrint.Enabled = True
                btnPrint_TagWithDraw.Enabled = True
                btnClear.Enabled = False

                objStatus = enuOperation_Type.UPDATE
                Load_WhitdrawLocation_Edit(_Withdraw_index, 2)
            Else
                ' Save Not Success
                W_MSG_Information_ByIndex(2)

                Return "Save Fail"

            End If

            Return "PASS"

        Catch ex As Exception
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
            Throw ex
        Finally
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
        End Try
    End Function

    Function Config_PrintPalletSlip() As String
        Dim objCustomSetting As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
        Dim COnfig As String

        Try
            objCustomSetting.GetConfig_Key("USE_GENTAGWithdraw")
            COnfig = objCustomSetting.ScalarOutput
            Return COnfig
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Return ""
        Finally
            objCustomSetting = Nothing
        End Try

    End Function


    Sub Get_SumData()
        Try
            If grdWithdrawItemLocation.RowCount > 0 Then
                Me.btnSeachCustomer.Enabled = False
                Me.dtpWithdraw_Date.Enabled = False
                Me.cboDocumentType.Enabled = False
            Else
                Me.btnSeachCustomer.Enabled = True
                Me.dtpWithdraw_Date.Enabled = True
                Me.cboDocumentType.Enabled = True
            End If
            Dim dtSum As New DataTable
            dtSum = grdWithdrawItemLocation.DataSource
            If Not dtSum Is Nothing Then
                dtSum.AcceptChanges()
            End If
            Dim SumQty_sku2 As Decimal = 0
            Dim SumWeight2 As Decimal = 0
            Dim Sum2Flo1 As Decimal = 0
            Dim SumVolume2 As Decimal = 0
            Dim SumPrice2 As Decimal = 0
            Dim SumItemQty2 As Decimal = 0
            Try
                SumQty_sku2 = dtSum.Compute("Sum(Total_Qty)", "")
            Catch ex As Exception
            End Try
            Try
                SumWeight2 = dtSum.Compute("Sum(WeightOut)", "")
            Catch ex As Exception

            End Try
            Try
                Sum2Flo1 = dtSum.Compute("Sum(Flo1)", "")
            Catch ex As Exception
            End Try
            Try
                SumVolume2 = dtSum.Compute("Sum(VolumeOut)", "")
            Catch ex As Exception
            End Try
            Try
                SumPrice2 = dtSum.Compute("Sum(flo3)", "")
            Catch ex As Exception
            End Try
            Try
                SumItemQty2 = dtSum.Compute("Sum(QtyItemOut)", "")
            Catch ex As Exception
            End Try
            txtSumQty.Text = Format(SumQty_sku2, "0")
            txtSumNet_Wt.Text = Format(SumWeight2, "#,##0.0000")
            txtSumGrs_Wt.Text = Format(Sum2Flo1, "#,##0.0000")
            txtVolume.Text = Format(SumVolume2, "#,##0.0000")
            txtRate.Text = Format(SumPrice2, "#,##0.00")
            txtPackage.Text = Format(SumItemQty2, "#,##0.00")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub Get_SumDataPlan()
        Try
            Dim SumQty_Plan As Decimal = 0
            Dim SumQty_WithDraw As Decimal = 0
            Dim i As Integer = 0
            For i = 0 To grdWithDrawPlan.Rows.Count - 1
                With grdWithDrawPlan.Rows(i)
                    If .Cells("col_Qty_Plan").Value IsNot Nothing Then SumQty_Plan += CDec(.Cells("col_Qty_Plan").Value)
                    If .Cells("col_Qty_WithDraw").Value IsNot Nothing Then SumQty_WithDraw += CDec(.Cells("col_Qty_WithDraw").Value)
                End With
            Next

            txtSumPlanPck.Text = Format(SumQty_Plan, "0")
            txtWithDrawPlan.Text = Format(SumQty_WithDraw, "0")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Function Cal_SumPlan(ByVal grdColumn As String) As decimal
    '    Try
    '        Dim SumData As decimal = 0
    '        Dim i As Integer = 0
    '        For i = 0 To grdWithDrawPlan.Rows.Count - 1
    '            SumData += CDec(grdWithDrawPlan.Rows(i).Cells(grdColumn).Value)
    '        Next
    '        Return SumData
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    'Function Cal_Sum(ByVal grdColumn As String) As decimal
    '    Try
    '        Dim SumData As decimal = 0
    '        Dim i As Integer = 0
    '        For i = 0 To grdWithdrawItemLocation.Rows.Count - 1
    '            SumData += CDec(grdWithdrawItemLocation.Rows(i).Cells(grdColumn).Value)
    '        Next
    '        Return SumData
    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Private Sub grdWithdrawItemLocation_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithdrawItemLocation.CellEndEdit
        Try
            Get_SumData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnNewWithDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_TruckOut.Click
        Try
            Dim frm As New WMS_STD_OUTB_WithDraw.frmWithdrawTruckOut(WMS_STD_OUTB_WithDraw.frmWithdrawTruckOut.enuOperation_Type.ADDNEW)
            frm.WithDraw_Index = Me._Withdraw_index
            frm.ShowDialog()
            getWithDrawTruckOutDetail(_Withdraw_index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getWithDrawTruckOutDetail(ByVal WithDraw_Index As String)
        Try
            Dim objWithDrawTruckOut As New tb_WithDrawTruckOut(tb_WithDrawTruckOut.enuOperation_Type.SEARCH)
            Dim objDTWithDrawTruckOut As New DataTable
            objWithDrawTruckOut.getWithDrawTruckOutDetail(WithDraw_Index)
            objDTWithDrawTruckOut = objWithDrawTruckOut.DataTable

            grdWithDrawTruckOut.Rows.Clear()
            For i As Integer = 0 To objDTWithDrawTruckOut.Rows.Count - 1
                With Me.grdWithDrawTruckOut
                    .Rows.Add()
                    .Rows(i).Cells("ColWithDrawTruckOut_Index").Value = objDTWithDrawTruckOut.Rows(i).Item("WithDrawTruckOut_Index").ToString
                    .Rows(i).Cells("ColContainer_Size").Value = objDTWithDrawTruckOut.Rows(i).Item("Container_Size").ToString
                    .Rows(i).Cells("ColContainer_No").Value = objDTWithDrawTruckOut.Rows(i).Item("Container_No").ToString
                    .Rows(i).Cells("ColContainer_No").Tag = objDTWithDrawTruckOut.Rows(i).Item("Container_Index").ToString
                    .Rows(i).Cells("ColDriver_Name").Value = objDTWithDrawTruckOut.Rows(i).Item("Driver_Name").ToString
                    .Rows(i).Cells("ColVehicle_No").Value = objDTWithDrawTruckOut.Rows(i).Item("Vehicle_No").ToString
                    .Rows(i).Cells("ColVehicle_Type").Value = objDTWithDrawTruckOut.Rows(i).Item("VehicleType").ToString
                    .Rows(i).Cells("ColVehicle_Type").Tag = objDTWithDrawTruckOut.Rows(i).Item("VehicleType_Index").ToString
                    .Rows(i).Cells("ColTime_Finish").Value = objDTWithDrawTruckOut.Rows(i).Item("Time_FinistLoad").ToString
                    .Rows(i).Cells("ColTime_Ingate").Value = objDTWithDrawTruckOut.Rows(i).Item("Time_Ingate").ToString
                    .Rows(i).Cells("ColTime_Outgate").Value = objDTWithDrawTruckOut.Rows(i).Item("Time_Outgate").ToString
                    .Rows(i).Cells("ColTime_Start").Value = objDTWithDrawTruckOut.Rows(i).Item("Time_StartLoad").ToString
                    .Rows(i).Cells("ColTransport_From").Value = objDTWithDrawTruckOut.Rows(i).Item("Transport_From").ToString
                    .Rows(i).Cells("ColTransport_To").Value = objDTWithDrawTruckOut.Rows(i).Item("Transport_To").ToString

                End With
            Next
            grdWithDrawTruckOut.Update()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnEditWithDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_TruckOut.Click
        Try
            Dim frm As New WMS_STD_OUTB_WithDraw.frmWithdrawTruckOut(WMS_STD_OUTB_WithDraw.frmWithdrawTruckOut.enuOperation_Type.UPDATE)
            If grdWithDrawTruckOut.RowCount = 0 Then
                Exit Sub
            End If
            Dim strWithDrawTruckOut_Index As String = grdWithDrawTruckOut.CurrentRow.Cells("ColWithDrawTruckOut_Index").Value.ToString
            frm.WithDraw_Index = Me._Withdraw_index
            frm.WithDrawTruckOut_Index = strWithDrawTruckOut_Index
            frm.ShowDialog()
            getWithDrawTruckOutDetail(_Withdraw_index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim oconfig_Report As New WMS_STD_Master.config_Report
        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
        Try
            Select Case Report_Name.ToUpper
                Case "WTH_PRINTOUT_V1"
                    Dim oReport As New clsReport()

                    Dim strWhere As String = String.Format(" and Withdraw_Index='{0}' ", Me._Withdraw_index)

                    Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                    Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                    rpt = oReport.GetReportInfo(Report_Name, strWhere)

                    frm.CrystalReportViewer1.ReportSource = rpt
                    frm.ShowDialog()
                Case Else
                    Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                    Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And Withdraw_index ='" & Me._Withdraw_index & "'")
                    frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                    frm.ShowDialog()
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
        End Try
    End Sub

    Sub SetDEFAULT_USE_REPORTPRINTOUT_BOND()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_REPORTPRINTOUT_BOND", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.DEFAULT_USE_REPORTPRINTOUT_BOND = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DEFAULT_USE_REPORTPRINTOUT_BOND = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_TruckOut.Click
        Try
            If grdWithDrawTruckOut.RowCount <= 0 Then
                Exit Sub
            End If
            Dim a As Integer = grdWithDrawTruckOut.CurrentRow.Index
            If W_MSG_Confirm_ByIndex("5") = Windows.Forms.DialogResult.Yes Then
                Dim objSaveWithDrawTruckOut As New tb_WithDrawTruckOut(tb_WithDrawTruckOut.enuOperation_Type.DELETE)
                objSaveWithDrawTruckOut.WithDrawTruckOut_Index = grdWithDrawTruckOut.CurrentRow.Cells("ColWithDrawTruckOut_Index").Value.ToString
                objSaveWithDrawTruckOut = New tb_WithDrawTruckOut(tb_WithDrawTruckOut.enuOperation_Type.DELETE, objSaveWithDrawTruckOut)
                objSaveWithDrawTruckOut.SaveData()
                getWithDrawTruckOutDetail(_Withdraw_index)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Terminal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Terminal.Click
        Try
            Dim frm As New frmTerminal_Popup
            frm.ShowDialog()
            txtTerminal.Text = frm.TerminalName
            txtTerminal.Tag = frm.TerminalIndex
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)

        End Try
    End Sub

    Private Sub btnSearch_Origin_Port_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Origin_Port.Click
        Try
            Dim frm As New frmPort_Popup
            frm.ShowDialog()
            txtOrigin_Port.Text = frm.PortId
            txtOrigin_Port.Tag = frm.PortIndex

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Destination_Port_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Destination_Port.Click
        Try
            Dim frm As New frmPort_Popup
            frm.ShowDialog()
            txtDestination_Port.Text = frm.PortId
            txtDestination_Port.Tag = frm.PortIndex

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Origin_Country_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Origin_Country.Click
        Try
            Dim frm As New frmCountry_Popup
            frm.ShowDialog()
            txtOrigin_Country.Text = frm.CountryId
            txtOrigin_Country.Tag = frm.CountryIndex

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Destination_Country_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch_Destination_Country.Click
        Try
            Dim frm As New frmCountry_Popup
            frm.ShowDialog()
            txtDestination_Country.Text = frm.CountryId
            txtDestination_Country.Tag = frm.CountryIndex
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdWithdrawItemLocation_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdWithdrawItemLocation.EditingControlShowing
        Try
            Dim strName As String = grdWithdrawItemLocation.Columns(grdWithdrawItemLocation.CurrentCell.ColumnIndex).Name
            If (strName <> "cbo_HandlingType2") And (strName <> "btn_ItemDefinition") Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
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
    ''' Update Date : 20/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Key In Tax
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdWithdrawItemLocation.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 decimal
            Dim Column_Index As String = grdWithdrawItemLocation.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdWithdrawItemLocation.Columns("Col_Weight2").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Volume2").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("col_ItemQty2").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("col_Price2").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)

                Case Is = grdWithdrawItemLocation.Columns("Col_Tax21").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax22").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax23").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax24").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdWithdrawItemLocation.Columns("Col_Tax25").Index
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


    Sub Show_grdWithDrawPlan(ByVal dtPlanWithDraw As DataTable)
        Try

            If Me.grdWithDrawPlan.DataSource Is Nothing Then

                Me.grdWithDrawPlan.DataSource = dtPlanWithDraw
            Else
                'Dim i As Integer
                'For i = 0 To dtPlanWithDraw.Rows.Count - 1
                '    Dim odtTempItemLocation As New DataTable
                '    Dim odrTemp As DataRow
                '    odtTempItemLocation = Me.grdWithDrawPlan.DataSource
                '    odrTemp = odtTempItemLocation.NewRow
                '    odrTemp.ItemArray = dtPlanWithDraw.Rows(i).ItemArray.Clone
                '    odtTempItemLocation.Rows.Add(odrTemp)
                'Next

                Dim odtTempItemLocation As New DataTable
                odtTempItemLocation = Me.grdWithDrawPlan.DataSource
                'For Each odrTemp As DataRow In dtPlanWithDraw.Rows
                '    odrTemp("Plan_Process") = _Plan_Process
                'Next
                odtTempItemLocation.Merge(dtPlanWithDraw)


            End If
            Get_SumDataPlan()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub



    Private Sub grdWithDrawPlan_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithDrawPlan.CellClick
        Try
            If e.RowIndex <= -1 Then Exit Sub
            If grdWithDrawPlan.CurrentRow.Index < 0 Then Exit Sub

            If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                W_MSG_Information_ByIndex(8)
                Exit Sub
            End If

            _Remark = ""

            CurrentRowPlanWithDraw_Index = grdWithDrawPlan.CurrentRow.Index

            Dim frm As New frmPicking_Reserv_V4(Me.cboDocumentType.SelectedValue.ToString, Me._Withdraw_index, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_No").Value, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_Index").Value, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Process").Value, frmPicking_Reserv_V4.Operation.Withdraw)

            If Me.txtConsignee_ID.Tag IsNot Nothing Then
                frm.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
            End If



            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
                Case "btn_Reserv"
                    frm.DocumentPlan_Process = 2
                    frm.DocumentPlan_Index = Me._Withdraw_index
                    frm.withdraw_index = Me._Withdraw_index
                    frm.Customer_Id = txtCustomer_Id.Text
                    frm.Customer_Index = Me._Customer_Index
                    frm.Customer_Name = txtCustomer_Name.Text
                    frm.WithDraw_Date = Me.dtpWithdraw_Date.Value
                    If Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_ID_Plan").Value Is Nothing Then Exit Sub

                    frm.Sku_Id = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_ID_Plan").Value
                    frm.Sku_Index = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_Index_Plan").Value
                    frm.Sku_Name = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_Des_Plan").Value


                    frm.Qty_Reserv = (CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value)) - (CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value))
                    'frm.Qty_Reserv = ((CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value)) - (CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value))) * (CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value))
                    'frm.Qty_Reserv = CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan_Sku").Value)

                    frm.Package_Index_Begin = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Index_Plan").Value
                    'frm.Package_Index_Begin = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Index_Plan_Sku").Value

                    frm.Max_Plan_Reserv = CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value)
                    'frm.Max_Plan_Reserv = CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value) * (CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value))
                    frm.txtQty_Reserv.ReadOnly = False
                    frm.txtQty_Reserv.BackColor = Color.White
                    frm.ItemStatus_Index = ""


                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ItemStatus_Index_Plan").Value, GetType(String)) <> "" Then
                        frm.ItemStatus_Index = grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ItemStatus_Index_Plan").Value.ToString
                    End If
                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Status_Plan").Value, GetType(String)) <> "" Then
                        Dim objClassDB1 As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
                        Dim objDt1 As New DataTable
                        objClassDB1.SelectData_For_Edit(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Status_Plan").Value)
                        objDt1 = objClassDB1.DataTable
                        If objDt1.Rows.Count > 0 Then
                            frm.ItemStatus_Index = objDt1.Rows(0).Item("ItemStatus_Index")
                        End If
                    End If


                    _Remark = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("Col_Remark").Value


                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Lot_Plan").Value, GetType(String)) <> "" Then
                        frm.Plot = grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Lot_Plan").Value.ToString
                    Else
                        frm.Plot = ""
                    End If

                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ERP_Location_Plan").Value, GetType(String)) <> "" Then
                        frm.ERP_location = grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ERP_Location_Plan").Value.ToString
                    Else
                        frm.ERP_location = ""
                    End If
                    ' Fixed SKU
                    frm.btnSeachSku.Enabled = False
                    frm.rdbAutoPicking.Checked = False
                    frm.rdbCustom.Checked = True
                    'frm.VisibleAutoPick = True
                    frm.ShowDialog()
                    If frm.objTmpWithDrawItem IsNot Nothing Then
                        frm.objTmpWithDrawItem.AcceptChanges()
                    End If

                    '--- Color 
                    Dim dtLocationBalance As DataTable = frm.objTmpWithDrawItem
                    Dim i As Integer = 0
                    If dtLocationBalance Is Nothing Then
                        Exit Sub
                    End If
                    If dtLocationBalance.Rows.Count > 0 Then

                        ''Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = CDec(dtLocationBalance.Compute("sum(Qty)", "Qty >= 0"))
                        'Bo Update 25/06/2011 
                        Dim odtPicked As New DataTable
                        Dim dblTotal_Qty_Picking As Decimal = 0

                        If dtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty >= 0") IsNot Nothing Then
                            dblTotal_Qty_Picking = CDec(dtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty >= 0"))
                        End If

                        odtPicked = Me.grdWithdrawItemLocation.DataSource

                        If odtPicked IsNot Nothing Then
                            Dim dblTotal_Qty As Decimal = 0
                            Dim drSelectSum() As DataRow = odtPicked.Select("DocumentPlanItem_Index = '" & Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value & "'")
                            If drSelectSum.Length > 0 Then
                                If IsDBNull(odtPicked.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index = '" & Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value & "' AND NewItemFlag = '1' ")) Then
                                    dblTotal_Qty = 0
                                Else
                                    dblTotal_Qty = odtPicked.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index = '" & Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value & "' AND NewItemFlag = '1' ")
                                End If
                                Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = Math.Round((dblTotal_Qty / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6) + Math.Round((dblTotal_Qty_Picking / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6)
                            Else
                                Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = Math.Round((dblTotal_Qty_Picking / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6)
                            End If
                        Else
                            Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = Math.Round((dblTotal_Qty_Picking / IIf(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value = Nothing, 0, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value)), 6)
                        End If
                        '********************

                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Item_Qty").Value = CDec(dtLocationBalance.Compute("sum(QtyItemOut)", "QtyItemOut >= 0"))
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Total_Qty_Paln").Value = CDec(dtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty  >= 0"))

                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Qty").Value = dtLocationBalance.Rows(0)("Sku_PackageDescription").ToString
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Item").Value = dtLocationBalance.Rows(0)("Item_Package").ToString
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Pakage_Qty").Value = dtLocationBalance.Rows(0)("Description").ToString
                    End If



                    If CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value.ToString) >= CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value) Then
                        Me.grdWithDrawPlan.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.YellowGreen
                    Else
                        Me.grdWithDrawPlan.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Gainsboro
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Style.BackColor = Color.WhiteSmoke
                    End If

                    Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_No").Style.BackColor = Color.Yellow





                    StatusWithdraw_Document = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Process").Value.ToString
                    Dim strDocumentPlan_No As String = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_No").Value.ToString
                    Dim strDocumentPlanItem_Index As String = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value.ToString
                    Dim strDocumentPlan_Index As String = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_Index").Value.ToString

                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.Packing
                            _Plan_Process = Withdraw_Document.Packing
                            _DocumentPlan_No = strDocumentPlan_No
                            _DocumentPlanItem_Index = strDocumentPlanItem_Index
                            _DocumentPlan_Index = strDocumentPlan_Index
                        Case Withdraw_Document.SO
                            _Plan_Process = Withdraw_Document.SO
                            _DocumentPlan_No = strDocumentPlan_No
                            _DocumentPlanItem_Index = strDocumentPlanItem_Index
                            _DocumentPlan_Index = strDocumentPlan_Index
                    End Select

                    _Reference = ""
                    _Reference2 = ""

                    _AssetLocationBalance_Index = ""
                    _Asset_No = ""
                    _AssetSerial_No = ""
                    _NewItem = 1
                    If SetAUTO_REFERENCE() = 1 Then
                        If txtRef_No1.Text <> "" Then
                            _Reference = txtRef_No1.Text
                            _Reference2 = txtRef_No2.Text
                        End If
                    End If

                    setDataSoucreWithdrawItemLocation(dtLocationBalance)


                    'If cdbl(frm.txtSumQty.Text) < cdbl(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value) Then
                    '    W_MSG_Information_ByIndex(300045)
                    '    Exit Sub
                    'End If

                    frm.Close()

                    Load_WhitdrawLocation_Edit(_Withdraw_index, 2)
            End Select

            Get_SumDataPlan()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdWithdrawItemLocation_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithdrawItemLocation.CellClick
        If e.RowIndex <= -1 Then
            Exit Sub
        End If
        If e.ColumnIndex <= -1 Then
            Exit Sub
        End If

        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToUpper
                Case "BTN_ITEMDEFINITION"
                    ' *********************************************************************
                    '   grdOrderItem.Rows(e.RowIndex).Cells("ItemDefinition_Index_Hiden").Value = ""
                    ' *********************************************************************
                    If grdWithdrawItemLocation.Rows(e.RowIndex).Cells("Col_Sku_Id2").Value Is Nothing Then
                        W_MSG_Confirm_ByIndex(300008)
                        Exit Sub
                    End If

                    Dim frmPopup As New frmDefinition_Popup
                    frmPopup.Proces_id = "'2','4'"
                    frmPopup.ShowDialog()
                    Me.grdWithdrawItemLocation.Rows(e.RowIndex).Cells("col_ItemDefinition_Index").Value = frmPopup.ItemDefinition_Index
                    Me.grdWithdrawItemLocation.Rows(e.RowIndex).Cells("col_ItemDefinition2").Value = frmPopup.ItemDefinition_Description
                    frmPopup.Close()
                Case "BTNSWITCH_SKU"
                    If W_MSG_Confirm(GetMessage_Data(100046)) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                    If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                        W_MSG_Information_ByIndex(8)
                        Exit Sub
                    End If
                    _Remark = ""
                    '******* Begin Set frm Picking *********
                    Dim iRow As Integer = grdWithdrawItemLocation.CurrentRow.Index
                    Dim Oty_Reserv As Decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Qty_sku2").Value.ToString)
                    'Dim Weight_Reserv As decimal = cdbl(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Weight2").Value.ToString)
                    'Dim Volume_Reserv As decimal = cdbl(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Volume2").Value.ToString)
                    'Dim ItemQty_Reserv As decimal = cdbl(grdWithdrawItemLocation.Rows(iRow).Cells("col_ItemQty2").Value.ToString)
                    'Dim Price_Reserv As decimal = cdbl(grdWithdrawItemLocation.Rows(iRow).Cells("col_Price2").Value)
                    'Dim LocationBal_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("Col_LocationBalance_Index2").Value.ToString
                    '********* BEGIN BLOCK LOCATION ********
                    Dim oLocation As New ml_Location(ml_Location.enuOperation_Type.UPDATE)
                    Dim strLocation_Index As String = grdWithdrawItemLocation.CurrentRow.Cells("col_Location_Index2").Value.ToString
                    oLocation.Update_Action_Id(strLocation_Index, ml_Location.enuOperation_Active.BLOCK)
                    '********* END BLOCK LOCATION ********


                    Dim frm As New frmPicking_Reserv(Me.cboDocumentType.SelectedValue.ToString)
                    frm.withdraw_index = Me._Withdraw_index
                    frm.Customer_Id = txtCustomer_Id.Text
                    frm.Customer_Index = Me._Customer_Index
                    frm.Customer_Name = txtCustomer_Name.Text
                    frm.WithDraw_Date = Me.dtpWithdraw_Date.Value
                    If Me.txtConsignee_ID.Tag IsNot Nothing Then
                        frm.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
                    End If
                    'set Auto Pick

                    frm.Sku_Id = grdWithdrawItemLocation.Rows(iRow).Cells("Col_SKU_Id2").Value.ToString
                    frm.Sku_Index = grdWithdrawItemLocation.Rows(iRow).Cells("Col_Sku_Index2").Value.ToString
                    frm.Sku_Name = grdWithdrawItemLocation.Rows(iRow).Cells("Col_SKU_Description2").Value.ToString
                    frm.Qty_Reserv = Oty_Reserv

                    frm.txtQty_Reserv.ReadOnly = True
                    frm.rdbAutoPicking.Checked = True
                    ' Fixed SKU
                    frm.btnSeachSku.Enabled = False
                    frm.ShowDialog()

                    '******* END Set frm Picking *********

                    Dim dtLocationBalance As DataTable = frm.objTmpWithDrawItem

                    If dtLocationBalance Is Nothing Then
                        oLocation.Update_Action_Id(strLocation_Index, ml_Location.enuOperation_Active.ACTIVE)
                        Exit Sub
                    End If

                    dtLocationBalance.AcceptChanges()

                    _Plan_Process = -9
                    _DocumentPlan_No = ""
                    _DocumentPlanItem_Index = ""
                    _DocumentPlan_Index = ""
                    _Reference = ""
                    _Reference2 = ""

                    _AssetLocationBalance_Index = ""
                    _Asset_No = ""
                    _AssetSerial_No = ""
                    _NewItem = 1


                    If SetAUTO_REFERENCE() = 1 Then
                        If txtRef_No1.Text <> "" Then
                            _Reference = txtRef_No1.Text
                            _Reference2 = txtRef_No2.Text
                        End If
                    End If

                    '******* begin set datagrid withdraw ********

                    Select Case dtLocationBalance.Rows.Count
                        Case 0
                            'ACTIVE WHEN NO ACTION
                            oLocation.Update_Action_Id(strLocation_Index, ml_Location.enuOperation_Active.ACTIVE)
                        Case Else
                            Dim strWithDrawItem_Index As String = grdWithdrawItemLocation.CurrentRow.Cells("col_WithDrawItem_Index2").Value.ToString
                            Dim dtDatasource As New DataTable
                            Dim dtTempDatasource As New DataTable
                            dtDatasource = grdWithdrawItemLocation.DataSource

                            'Assinge TempTable
                            Dim dtTemp As New DataTable
                            Dim dtInsert As New DataTable
                            dtTemp = dtLocationBalance.Clone
                            dtDatasource.Merge(dtTemp)
                            dtTempDatasource = dtDatasource.Clone
                            dtInsert = dtDatasource.Clone


                            'ถ่ายค่าจาก การหยิบลงใน Datable เปล่า ๆ To Datasource ที่ถูกต้อง

                            For Each drLocationBalance As DataRow In dtLocationBalance.Rows
                                Dim odrNewData As DataRow
                                odrNewData = dtInsert.NewRow
                                For Each dc As DataColumn In dtInsert.Columns
                                    Dim cName As String = dc.ColumnName
                                    If dtLocationBalance.Columns.Contains(cName) Then
                                        odrNewData(cName) = drLocationBalance(cName)
                                    End If
                                Next
                                dtInsert.Rows.Add(odrNewData)
                            Next

                            Dim Seq As Integer = 1
                            ' loop DataSource Grid
                            For Each odrEdit As DataRow In dtDatasource.Rows
                                Dim odrNewData As DataRow
                                odrNewData = dtInsert.NewRow
                                odrNewData.ItemArray = odrEdit.ItemArray.Clone



                                If odrEdit("WithDrawItem_Index").ToString = strWithDrawItem_Index And dtInsert.Rows.Count > 0 Then
                                    Dim odrNewTempData As DataRow
                                    odrNewTempData = dtInsert.NewRow
                                    odrNewTempData.ItemArray = dtInsert.Rows(0).ItemArray.Clone

                                    _Plan_Process = odrNewData("Plan_Process").ToString
                                    _DocumentPlan_No = odrNewData("DocumentPlan_No").ToString
                                    _DocumentPlanItem_Index = odrNewData("DocumentPlanItem_Index").ToString
                                    _DocumentPlan_Index = odrNewData("DocumentPlan_Index").ToString
                                    _Reference = odrNewData("Reference").ToString
                                    _Reference2 = odrNewData("Reference2").ToString
                                    _AssetLocationBalance_Index = odrNewData("AssetLocationBalance_Index").ToString
                                    _Asset_No = odrNewData("Asset_No").ToString

                                    odrNewTempData("Plan_Process") = _Plan_Process
                                    odrNewTempData("DocumentPlan_No") = _DocumentPlan_No
                                    odrNewTempData("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                                    odrNewTempData("DocumentPlan_Index") = _DocumentPlan_Index
                                    odrNewTempData("Reference") = _Reference
                                    odrNewTempData("Reference2") = _Reference2
                                    odrNewTempData("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                                    odrNewTempData("Asset_No") = _Asset_No
                                    odrNewTempData("AssetSerial_No") = _AssetSerial_No
                                    odrNewTempData("Seq") = odrNewData("Seq")
                                    odrNewTempData("comment") = odrNewData("comment").ToString
                                    odrNewTempData("WithdrawItemLocation_Index") = odrNewData("WithdrawItemLocation_Index").ToString
                                    odrNewTempData("WithdrawItem_Index") = odrNewData("WithdrawItem_Index").ToString
                                    odrNewTempData("Withdraw_Index") = odrNewData("Withdraw_Index").ToString
                                    odrNewTempData("Status") = odrNewData("Status").ToString

                                    odrNewTempData("NewItem") = 1


                                    odrNewData.ItemArray = odrNewTempData.ItemArray.Clone
                                    dtInsert.Rows.RemoveAt(0)

                                End If
                                odrNewData("Seq") = Seq

                                dtTempDatasource.Rows.Add(odrNewData.ItemArray)
                                Seq += 1

                            Next


                            '**************   BEGIN Old WithDrawItem   **************

                            DELETE_GRIDWITHDRAWITEM(iRow, True)
                            dtTempDatasource.AcceptChanges()
                            Me.grdWithdrawItemLocation.DataSource = Nothing
                            Me.grdWithdrawItemLocation.DataSource = dtTempDatasource
                            Me.grdWithdrawItemLocation.Update()
                            '**************   END Old WithDrawItem   **************

                            setDataSoucreWithdrawItemLocation(dtInsert)

                    End Select

                    '******* end set datagrid withdraw ********
                    '   frm.Close()
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            'CheckIsSerial
            Try

                Dim pSku_Index As String
                Dim OBJ As New tb_WithdrawItemSerial
                pSku_Index = grdWithdrawItemLocation.Rows(e.RowIndex).Cells("Col_Sku_Index2").Value.ToString
                Me.btnSerial.Enabled = OBJ.CheckIsSerial(pSku_Index)

            Catch ex As Exception
                W_MSG_Error(ex.Message)
            End Try

        End Try
    End Sub

    Private Sub btnAddPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPlan.Click
        Try
            If grdWithDrawPlan.RowCount > 0 Then
                If W_MSG_Confirm_ByIndex(100038) = Windows.Forms.DialogResult.Yes Then
                    For irowDel As Integer = grdWithDrawPlan.RowCount - 1 To 0 Step -1

                        btnDelSO(irowDel)
                    Next
                Else
                    Exit Sub
                End If
            End If
            If Me._Customer_Index = "" Then
                W_MSG_Information_ByIndex(8)
                Exit Sub
            End If

            Dim frm As New frmWithDraw_Plan
            frm.USE_PACKING_NEW_PRODUCTION = Me._USE_PACKING_NEW_PRODUCTION
            frm.Customer_index = _Customer_Index
            '  frm.StrSONO = SO_Index
            frm.ShowDialog()


            Dim odrPlan As DataRow
            Dim odtPlan As New DataTable

            odtPlan = frm.DataTable
            If odtPlan Is Nothing Then
                Exit Sub
            End If


            For Each odrPlan In odtPlan.Rows
                Dim dblQty_Bal As Decimal = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(odrPlan("Qty_Bal").ToString, GetType(Decimal))
                LoadPlanWithdraw(odrPlan("Document_Index").ToString, odrPlan("Process_Id").ToString, dblQty_Bal)
            Next
            cboAutoPicking.Enabled = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' 15-01-2010
    ''' ja
    ''' add Withdraw_Document.Reserve ใน  LoadPlanWithdraw
    ''' <param name="pintProcess_ID"></param>
    ''' <remarks></remarks>

    Private Sub LoadPlanWithdraw(ByVal pstrDocumnet_Index As String, ByVal pintProcess_ID As Integer, Optional ByVal dblQty_Bal As Decimal = 0)
        Try
            Dim strDocumentPlan_Index As String
            Dim dtDataPlan As New DataTable
            strDocumentPlan_Index = "DocumentPlan_Index = '" & pstrDocumnet_Index & "'"
            Dim objDB As New View_LocationBalance

            Select Case pintProcess_ID
                Case Withdraw_Document.Packing
                    If Me._USE_PACKING_NEW_PRODUCTION = False Then
                        objDB.SearchWithDraw_PackingItem(strDocumentPlan_Index)
                        dtDataPlan = objDB.DataTable
                        If dblQty_Bal > 0 Then
                            For Each drcalQty As DataRow In dtDataPlan.Rows
                                drcalQty("Qty_Plan") = drcalQty("Qty_Per_Pack") * dblQty_Bal
                            Next
                        End If
                    Else
                        objDB.SearchWithDraw_PackingHeader(strDocumentPlan_Index)
                        dtDataPlan = objDB.DataTable
                    End If
                Case Withdraw_Document.SO, Withdraw_Document.Reservation
                    objDB.SearchWithDraw_SO(strDocumentPlan_Index)
                    dtDataPlan = objDB.DataTable
                Case Withdraw_Document.Reserve '15-01-2010 ja add auto reserve
                    objDB.SearchWithDraw_Reserve(strDocumentPlan_Index)
                    dtDataPlan = objDB.DataTable
                Case Withdraw_Document.Transport
                    objDB.SearchWithDraw_SO(strDocumentPlan_Index)
                    dtDataPlan = objDB.DataTable
            End Select

            Show_grdWithDrawPlan(dtDataPlan)
        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub LoadWithdrawItemLocation(ByVal Reserve_Index As String)
        Try
            '15-01-2010 ja  new LoadWithdrawItemLocation
            Dim objDB As New View_LocationBalance
            objDB.SearchWithdrawItemLocation_ReserveWithdrawItemLocation(Reserve_Index)
            _NewItem = 0
            Me.grdWithdrawItemLocation.DataSource = objDB.DataTable


        Catch ex As Exception
            Throw ex
        End Try


    End Sub

    Private Sub btnDelPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelPlan.Click
        Try
            If grdWithDrawPlan.RowCount <= 0 Then
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex("5") = Windows.Forms.DialogResult.Yes Then
                Me.btnDelSO(grdWithDrawPlan.CurrentRow.Index)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelSO(ByVal irowSO As Integer)
        Try
            If grdWithDrawPlan.RowCount <= 0 Then
                Exit Sub
            End If
            Dim strDocumentPlanItem_Index As String = grdWithDrawPlan.Rows(irowSO).Cells("col_DocumentPlanItem_Index").Value.ToString
            Dim strPlan_Process As String = grdWithDrawPlan.Rows(irowSO).Cells("col_Plan_Process").Value.ToString
            Dim Total_Qty_Withdraw As Decimal = 0
            If (IsNumeric(grdWithDrawPlan.Rows(irowSO).Cells("col_Qty_WithDraw").Value.ToString()) And IsNumeric(grdWithDrawPlan.Rows(irowSO).Cells("col_Plan_Ratio").Value.ToString())) Then
                Total_Qty_Withdraw = CDec(grdWithDrawPlan.Rows(irowSO).Cells("col_Qty_WithDraw").Value.ToString()) * CDec(grdWithDrawPlan.Rows(irowSO).Cells("col_Plan_Ratio").Value.ToString())
            End If
            Dim intRowPlan As Integer = grdWithDrawPlan.Rows(irowSO).Index

            If grdWithdrawItemLocation.Rows.Count = 0 Then
                grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.Rows(irowSO).Index)
            Else
                If strPlan_Process <> -9 Then
                    For intRow As Integer = (grdWithdrawItemLocation.Rows.Count - 1) To 0 Step -1
                        If (strDocumentPlanItem_Index = grdWithdrawItemLocation.Rows(intRow).Cells("col_DocumentPlanItem_Index2").Value.ToString) And (strPlan_Process = grdWithdrawItemLocation.Rows(intRow).Cells("col_Plan_Process2").Value.ToString) Then
                            If (Total_Qty_Withdraw = 0) Then Exit For
                            If (IsNumeric(Me.grdWithdrawItemLocation.Rows(intRow).Cells("Col_Qty_sku2").Value.ToString())) Then
                                Total_Qty_Withdraw -= CDec(Me.grdWithdrawItemLocation.Rows(intRow).Cells("Col_Qty_sku2").Value.ToString())
                            End If
                            DELETE_GRIDWITHDRAWITEM(intRow, True)
                        End If
                    Next
                End If
                grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.Rows(irowSO).Index)
            End If

            Me.Get_SumDataPlan()
            Me.Get_SumData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub btnDelPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelPlan.Click
    '    Try
    '        If grdWithDrawPlan.RowCount <= 0 Then
    '            Exit Sub
    '        End If
    '        If W_MSG_Confirm_ByIndex("5") = Windows.Forms.DialogResult.Yes Then
    '            'If grdWithdrawItemLocation.RowCount = 0 Then
    '            '    Exit Sub
    '            'End If
    '            'If W_MSG_Confirm_ByIndex("5") = Windows.Forms.DialogResult.Yes Then
    '            '    Dim iRow As Integer = grdWithdrawItemLocation.CurrentRow.Index
    '            '    DELETE_GRIDWITHDRAWITEM(iRow, True)
    '            '    grdWithdrawItemLocation.Rows.RemoveAt(iRow)
    '            '    Get_SumData()
    '            'End If

    '            Dim strDocumentPlanItem_Index As String = grdWithDrawPlan.CurrentRow.Cells("col_DocumentPlanItem_Index").Value.ToString
    '            Dim strPlan_Process As String = grdWithDrawPlan.CurrentRow.Cells("col_Plan_Process").Value.ToString
    '            Dim intRowPlan As Integer = grdWithDrawPlan.CurrentRow.Index

    '            If grdWithdrawItemLocation.Rows.Count = 0 Then
    '                grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.CurrentRow.Index)
    '            Else
    '                If strPlan_Process <> -9 Then
    '                    For intRow As Integer = (grdWithdrawItemLocation.Rows.Count - 1) To 0 Step -1
    '                        If (strDocumentPlanItem_Index = grdWithdrawItemLocation.Rows(intRow).Cells("col_DocumentPlanItem_Index2").Value.ToString) And (strPlan_Process = grdWithdrawItemLocation.Rows(intRow).Cells("col_Plan_Process2").Value.ToString) Then
    '                            DELETE_GRIDWITHDRAWITEM(intRow, True)
    '                            grdWithdrawItemLocation.Rows.RemoveAt(intRow)
    '                        End If
    '                    Next
    '                End If

    '                grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.CurrentRow.Index)

    '            End If

    '            Get_SumDataPlan()
    '            Get_SumData()
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnDelSO(ByVal irowSO As Integer)
    '    Try
    '        If grdWithDrawPlan.RowCount <= 0 Then
    '            Exit Sub
    '        End If
    '        Dim strDocumentPlanItem_Index As String = grdWithDrawPlan.Rows(irowSO).Cells("col_DocumentPlanItem_Index").Value.ToString
    '        Dim strPlan_Process As String = grdWithDrawPlan.Rows(irowSO).Cells("col_Plan_Process").Value.ToString
    '        Dim intRowPlan As Integer = grdWithDrawPlan.Rows(irowSO).Index

    '        If grdWithdrawItemLocation.Rows.Count = 0 Then
    '            grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.CurrentRow.Index)
    '        Else
    '            If strPlan_Process <> -9 Then
    '                For intRow As Integer = (grdWithdrawItemLocation.Rows.Count - 1) To 0 Step -1
    '                    If (strDocumentPlanItem_Index = grdWithdrawItemLocation.Rows(intRow).Cells("col_DocumentPlanItem_Index2").Value.ToString) And (strPlan_Process = grdWithdrawItemLocation.Rows(intRow).Cells("col_Plan_Process2").Value.ToString) Then
    '                        DELETE_GRIDWITHDRAWITEM(intRow, True)
    '                        'grdWithdrawItemLocation.Rows.RemoveAt(intRow)
    '                    End If
    '                Next
    '            End If

    '            grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.CurrentRow.Index)

    '        End If


    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Function PickByAsset_Serial(ByVal pstrSQLWhere As String) As Boolean
    '    Try
    '        Dim objPicking As New PICKING(PICKING.enmPicking_Type.SERIAL)
    '        ' Dim Serial_No As String = 

    '        Dim strSqlCondition As String = pstrSQLWhere
    '        Dim odtPickAsset As New DataTable
    '        odtPickAsset = objPicking.SEARCHASSETLOCATIONBALANCE_PICKING(strSqlCondition)

    '        'If Not (odtPickAsset Is Nothing) Then
    '        '    If odtPickAsset.Rows.Count <= 0 Then
    '        '        W_MSG_Information_ByIndex(400004)
    '        '        Exit Sub
    '        '    End If

    '        If odtPickAsset.Rows.Count > 0 Then
    '            '************************** BEGIN PICKING **************************
    '            '--- LocationBalance Assign Condition For Pick LocationBalance
    '            '--- AssetLocationBalance Assign Property AssetLocationBalance For Pick AssetLocationBalance

    '            Dim strSku_Index As String = odtPickAsset.Rows(0).Item("Sku_Index").ToString
    '            Dim strPackage_Index As String = odtPickAsset.Rows(0).Item("Package_Index").ToString
    '            Dim strLocationBalance_Index As String = odtPickAsset.Rows(0).Item("LocationBalance_Index").ToString
    '            _AssetLocationBalance_Index = odtPickAsset.Rows(0).Item("AssetLocationBalance_Index").ToString
    '            _Asset_No = odtPickAsset.Rows(0).Item("Asset_No").ToString
    '            _AssetSerial_No = odtPickAsset.Rows(0).Item("AssetSerial_No").ToString


    '            objPicking = New PICKING(PICKING.enmPicking_Type.SERIAL, _AssetLocationBalance_Index, strLocationBalance_Index, strSku_Index, strPackage_Index, 1, "") '--- PICK 1 FOR SERIAL
    '            If objPicking.CHEK_QTY_BALANCE_Asset() = False Then Exit Function

    '            setDataSoucreWithdrawItemLocation(objPicking.fnPICKING())

    '            Return True
    '        Else
    '            Select Case _isSerail
    '                Case True
    '                    ' objPicking.CHECKSERIALASSET_STATTUS_PICKING(True, " AND AssetSerial_No = '" & txtSerial_No.Text.Trim.Replace("'", "''").ToString & "'")

    '                    Exit Function
    '                Case False
    '                    '  objPicking.CHECKSERIALASSET_STATTUS_PICKING(False, " AND Asset_No = '" & txtAsset_No.Text.Trim.Replace("'", "''").ToString & "'")
    '                    Exit Function
    '            End Select

    '            Return False
    '        End If



    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    'Private Sub txtAsset_No_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If e.KeyCode = Keys.Enter Then
    '            _isSerail = False
    '            If txtAsset_No.Text.Trim = "" Then
    '                W_MSG_Information_ByIndex(400007)
    '                Exit Sub
    '            End If

    '            If PickByAsset_Serial(" AND Asset_No = '" & txtAsset_No.Text.Trim.Replace("'", "''").ToString & "' AND Qty_Bal > 0 ") = True Then
    '                Me.txtAsset_No.Text = ""
    '                Me.txtSerial_No.Text = ""
    '            End If

    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try
    'End Sub

    'Private Sub txtSerial_No_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
    '    Try
    '        If e.KeyCode = Keys.Enter Then
    '            _isSerail = True
    '            If txtSerial_No.Text.Trim = "" Then
    '                W_MSG_Information_ByIndex(400006)
    '                Exit Sub
    '            End If

    '            If PickByAsset_Serial(" AND AssetSerial_No = '" & txtSerial_No.Text.Trim.Replace("'", "''").ToString & "' AND Qty_Bal > 0  ") = True Then
    '                Me.txtAsset_No.Text = ""
    '                Me.txtSerial_No.Text = ""
    '            End If
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try
    'End Sub

    Private Sub btnPrint_TagWithDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint_TagWithDraw.Click
        Try
            Dim frm As New WMS_STD_OUTB_WithDraw.frmWithDrawItemGroup_Sku
            frm.Withdraw_Index = Me._Withdraw_index
            frm._ConfigPrint = Me._ConfigPrint
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Function USE_PRODUCT_CUSTOMER() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_PRODUCT_CUSTOMER", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim intBlock As Integer = 0
            If objDT.Rows.Count > 0 Then
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

    Private Sub btnBarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarCode.Click
        Try
            'If USE_PRODUCT_CUSTOMER() = True Then
            '    If Me._Customer_Index = "" Then
            '        W_MSG_Information_ByIndex(8)
            '        Exit Sub
            '    End If

            'End If

            'Dim dtBarcode As New DataTable
            'Dim frm As New frmWithDraw_Barcode(frmWithDraw_Barcode.WithDraw_Mode.PLAN)
            'frm.Customer_Index = Me._Customer_Index
            'frm.ShowDialog()

            'dtBarcode = frm.DataTable
            'If dtBarcode Is Nothing Then Exit Sub
            '_NewItem = 1
            'Show_grdWithDrawPlanBarcode(dtBarcode)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub Show_grdWithDrawPlanBarcode(ByVal dtBarcode As DataTable)
        Try
            '--- Create DataSource 
            dtBarcode.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Sku_ID_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Sku_Index_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Sku_Des_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Package_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Package_Index_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Qty_Plan", GetType(Decimal)))
            dtBarcode.Columns.Add(New DataColumn("Qty_WithDraw", GetType(Decimal)))
            dtBarcode.Columns.Add(New DataColumn("Status_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Total_Qty_Plan", GetType(Decimal)))
            dtBarcode.Columns.Add(New DataColumn("Lot_Plan", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
            dtBarcode.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))



            Dim i As Integer = 0
            For i = 0 To dtBarcode.Rows.Count - 1
                dtBarcode.Rows(i).Item("DocumentPlan_No") = dtBarcode.Rows(i).Item("Barcode1")
                dtBarcode.Rows(i).Item("Sku_ID_Plan") = dtBarcode.Rows(i).Item("Sku_ID")
                dtBarcode.Rows(i).Item("Sku_Index_Plan") = dtBarcode.Rows(i).Item("Sku_Index")
                dtBarcode.Rows(i).Item("Sku_Des_Plan") = dtBarcode.Rows(i).Item("Product_Name_th")
                dtBarcode.Rows(i).Item("Package_Plan") = dtBarcode.Rows(i).Item("Sku_PackageDescription")
                dtBarcode.Rows(i).Item("Package_Index_Plan") = dtBarcode.Rows(i).Item("Package_Index")
                dtBarcode.Rows(i).Item("Qty_Plan") = dtBarcode.Rows(i).Item("Qty")
                dtBarcode.Rows(i).Item("Qty_WithDraw") = 0
                dtBarcode.Rows(i).Item("Status_Plan") = dtBarcode.Rows(i).Item("ItemStatus_Description")

                Dim Ratio As Decimal = 1
                Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                Ratio = objRatio.getRatio(dtBarcode.Rows(i).Item("Sku_Index"), dtBarcode.Rows(i).Item("Package_Index"))
                objRatio = Nothing

                dtBarcode.Rows(i).Item("Total_Qty_Plan") = CDec(dtBarcode.Rows(i).Item("Qty")) * Ratio

                dtBarcode.Rows(i).Item("Lot_Plan") = dtBarcode.Rows(i).Item("Plot")
                dtBarcode.Rows(i).Item("DocumentPlanItem_Index") = -9
                dtBarcode.Rows(i).Item("Plan_Process") = -9
            Next
            If Me.grdWithDrawPlan.RowCount = 0 Then
                Me.grdWithDrawPlan.DataSource = dtBarcode
            Else
                For i = 0 To dtBarcode.Rows.Count - 1
                    Dim odtTempItemLocation As New DataTable
                    Dim odrTemp As DataRow
                    odtTempItemLocation = Me.grdWithDrawPlan.DataSource
                    odrTemp = odtTempItemLocation.NewRow
                    odrTemp.ItemArray = dtBarcode.Rows(i).ItemArray.Clone
                    odtTempItemLocation.Rows.Add(odrTemp)
                Next
            End If

            Get_SumDataPlan()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAddItemAsset_Barcode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItemAsset_Barcode.Click
        Try
            If USE_PRODUCT_CUSTOMER() = True Then
                If Me._Customer_Index = "" Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If

            End If

            Dim frm As New WMS_STD_OUTB_WithDraw.frmWithDraw_Barcode_V2(Me.cboDocumentType.SelectedValue.ToString, Me.dtpWithdraw_Date.Value, Me._Customer_Index, Me._Withdraw_index)
            frm.ShowDialog()


            Dim dtLocationBalance As New DataTable


            _Plan_Process = -9
            _DocumentPlan_No = ""
            _DocumentPlanItem_Index = ""
            _Reference = ""
            _Reference2 = ""

            _AssetLocationBalance_Index = ""
            _Asset_No = ""
            _AssetSerial_No = ""
            _NewItem = 1

            setDataSoucreWithdrawItemLocation(dtLocationBalance)

            Call Get_SumData()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnImageAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImageAdd.Click
        Try
            grdOrderImage.AutoGenerateColumns = False
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                imgPreview.ImageLocation = OpenFileDialog1.FileName
                _strLongFilePath = OpenFileDialog1.FileName

                Dim objDBIndex As New Sy_AutoNumber
                Dim StrWithdraw_Image_Index As String = ""
                StrWithdraw_Image_Index = objDBIndex.getSys_Value("Withdraw_Image_Index")

                Dim str() As String
                str = OpenFileDialog1.FileName.Split(".")

                _strFileName = StrWithdraw_Image_Index & "." & str(str.Length - 1)
                '  _strPathName = Application.StartupPath
                _strPathName = _DEFAULT_ImagePath & _strFileName

                With odtWithdrawImage.Rows.Add
                    .Item("Withdraw_Image_Index") = StrWithdraw_Image_Index
                    .Item("Image_Path") = _strPathName
                End With

                With grdOrderImage
                    .DataSource = odtWithdrawImage
                End With

                ' Add Image  To  Folder
                If _strLongFilePath <> "" Then
                    IO.File.Copy(_strLongFilePath, _strPathName, True)
                End If
                'Insert To tb_Order_Image
                InsertWithdrawImage(StrWithdraw_Image_Index, _strPathName)

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub InsertWithdrawImage(ByVal pstrWithdraw_Image_Index As String, ByVal pstrPathName As String)
        Dim odjWithdrawImage As New tb_Withdraw_Image
        Try
            With odjWithdrawImage
                .Withdraw_Image_Index = pstrWithdraw_Image_Index
                .Withdraw_Index = _Withdraw_index
                .Image_Path = pstrPathName
                .Comment = ""
                .InsertWithdrawImage()
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnImageDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImageDelete.Click
        Dim odjWithdraw_Image As New tb_Withdraw_Image
        Try
            If odtWithdrawImage.Rows.Count = 0 Then
                Exit Sub
            End If
            If Not Me.grdWithdrawImage.CurrentRow.Cells("Col_Withdraw_ImageIndex").Value = "" Then
                odjWithdraw_Image.DeleteWithdrawImage(" WHERE Withdraw_Image_Index = '" & Me.grdWithdrawImage.CurrentRow.Cells("Col_Withdraw_ImageIndex").Value & "'")
                IO.File.Delete(Me.grdWithdrawImage.CurrentRow.Cells("ColImageName").Value)
                imgPreview.ImageLocation = ""
                Me.grdWithdrawImage.Rows.RemoveAt(grdWithdrawImage.CurrentRow.Index)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRotatePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotatePic.Click
        Try
            If grdWithdrawImage.Rows.Count = 0 Then Exit Sub
            imgPreview.Image.RotateFlip(RotateFlipType.Rotate90FlipXY)
            imgPreview.Refresh()
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
    ''' Add Date : 19/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : No Delete Reserv
    ''' </remarks>
    Private Sub grdWithdrawItemLocation_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdWithdrawItemLocation.SelectionChanged
        Try
            If grdWithdrawItemLocation.RowCount = 0 Then Exit Sub

            Select Case grdWithdrawItemLocation.CurrentRow.Cells("col_Plan_Process2").Value.ToString
                Case Nothing
                    '    btnDelItemAsset.Enabled = True
                    'Case ""
                    '    btnDelItemAsset.Enabled = True
                    'Case "17"
                    '    btnDelItemAsset.Enabled = False
                    'Case Else
                    '    btnDelItemAsset.Enabled = True
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
    '''  Data 25/03/2010
    '''  By TaTa
    '''     เบิกสินค้า
    ''' </remarks>
    Private Sub btnWithDraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWithDraw.Click

        Dim odtLocationBalance As New DataTable


        If grdWithDrawPlan.RowCount = 0 Then
            W_MSG_Information_ByIndex(300003)
            Exit Sub
        End If

        If W_MSG_Confirm(GetMessage_Data(400023) & cboAutoPicking.Text & GetMessage_Data(400022)) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If

        Try

            Dim _strOrderForV3 As String = ""
            Dim intRemainingAge_Value As Integer = 0
            Dim intRemainingAge_Unit As Integer = 0
            Dim booSkuRemainCheckValue As Boolean = False
            Dim booSkuRemainCheckUnit As Boolean = False
            Dim strLastIssue_Option As String = ""
            Dim strCustomer_Shipping_Location_Index As String = ""

            For i As Integer = 0 To grdWithDrawPlan.RowCount - 1

                Dim objcon As New DBType_SQLServer
                objcon.connectDB()
                Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable)
                objcon.SQLServerCommand.Transaction = myTrans
                objcon.SQLServerCommand.CommandTimeout = 0

                Try
                    objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)

                    _strOrderForV3 = ""
                    Dim _SKU_index As String = grdWithDrawPlan.Rows(i).Cells("col_Sku_Index_Plan").Value.ToString
                    Dim _Package_index As String = grdWithDrawPlan.Rows(i).Cells("col_Package_Index_Plan").Value.ToString
                    'Dim _Package_index As String = grdWithDrawPlan.Rows(i).Cells("col_Package_Index_Plan_Sku").Value.ToString
                    Dim dblQty_Reserv As Decimal = 0

                    Dim dblQty_Plan As Decimal = 0
                    Dim dblQty_WithDraw As Decimal = 0

                    If grdWithDrawPlan.Rows(i).Cells("col_Qty_Plan").Value IsNot Nothing Then
                        dblQty_Plan = grdWithDrawPlan.Rows(i).Cells("col_Qty_Plan").Value
                        'dblQty_Plan = grdWithDrawPlan.Rows(i).Cells("col_Qty_Plan").Value * grdWithDrawPlan.Rows(i).Cells("col_Plan_Ratio").Value
                    End If
                    If grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value IsNot Nothing Then
                        dblQty_WithDraw = grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value
                        'dblQty_WithDraw = grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value * grdWithDrawPlan.Rows(i).Cells("col_Plan_Ratio").Value
                    End If

                    dblQty_Reserv = dblQty_Plan - dblQty_WithDraw
                    If dblQty_Reserv <= 0 Then
                        myTrans.Rollback()
                        Continue For
                    End If


                    Dim _strPicking As String = " AND Qty_Bal > 0 "
                    _strPicking &= " and Order_Date <= '" & Me.dtpWithdraw_Date.Value.ToString("yyyy/MM/dd HH:mm:ss") & "'"


                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_ItemStatus_Index_Plan").Value, GetType(String)) <> "" Then
                        _strPicking &= " AND ItemStatus_Index = '" & grdWithDrawPlan.Rows(i).Cells("col_ItemStatus_Index_Plan").Value.ToString & "'"
                    End If


                    If Not String.IsNullOrEmpty(Me._Customer_Index) Then
                        _strPicking &= " AND Customer_Index = '" & Me._Customer_Index & "'"
                    End If



                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_Lot_Plan").Value, GetType(String)) <> "" Then
                        _strPicking &= " AND Plot = '" & grdWithDrawPlan.Rows(i).Cells("col_Lot_Plan").Value.ToString & "'"
                    End If

                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_ERP_Location_Plan").Value, GetType(String)) <> "" Then
                        _strPicking &= " AND ERP_Location = '" & grdWithDrawPlan.Rows(i).Cells("col_ERP_Location_Plan").Value.ToString & "'"
                    End If

                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_Serial_No_Plan").Value, GetType(String)) <> "" Then
                        _strPicking &= " AND Serial_No = '" & grdWithDrawPlan.Rows(i).Cells("col_Serial_No_Plan").Value.ToString & "'"
                    End If

                    _strPicking &= " and ItemStatus_Index in ( SELECT ItemStatus_Index FROM ms_DocumentType_ItemStatus WHERE    DocumentType_Index = '" & Me.cboDocumentType.SelectedValue & "' AND status_id <> -1) "




                    '--ADD V3--
                    If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_Customer_Shipping_Location_Index").Value, GetType(String)) Is Nothing Then
                        strCustomer_Shipping_Location_Index = Me.grdWithDrawPlan.Rows(i).Cells("col_Customer_Shipping_Location_Index").Value
                    End If

                    If strCustomer_Shipping_Location_Index <> "" Then



                        '>>a.เงื่อนไขคลังสินค้าหลัก และข้อกำหนด “จ่ายจากคลังหลักเท่านั้น”
                        If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_PrimaryWH").Value, GetType(Boolean)) Is Nothing Then
                            If IsDBNull(Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_PrimaryWH").Value) = False Then
                                If Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_PrimaryWH").Value Then
                                    If IsDBNull(Me.grdWithDrawPlan.Rows(i).Cells("col_Warehouse_Index").Value) = False Then
                                        If Me.grdWithDrawPlan.Rows(i).Cells("col_Warehouse_Index").Value <> "" Then
                                            Dim Warehouse_Index As String = Me.grdWithDrawPlan.Rows(i).Cells("col_Warehouse_Index").Value
                                            If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_PrimaryWH").Value, GetType(Boolean)) Is Nothing Then
                                                If Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_PrimaryWHOnly").Value Then
                                                    _strPicking &= String.Format(" AND  Warehouse_Index = '{0}' ", Warehouse_Index)
                                                Else
                                                    _strOrderForV3 &= String.Format(" CASE Warehouse_Index WHEN '{0}' THEN 1 ELSE 2 END ", Warehouse_Index)
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        End If

                        '>>b.เงื่อนไขอายุสินค้าคงเหลือ ทั้งแบบวันและ % ถ้ามีการกำหนดต่อ SKU ใช้ข้อกำหนดของ SKU ไม่เช่นนั้นใช้ข้อกำหนดรวม
                        booSkuRemainCheckValue = False
                        booSkuRemainCheckUnit = False
                        intRemainingAge_Value = 0
                        intRemainingAge_Unit = 0
                        If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_isSKURemainingAge_Value").Value, GetType(Boolean)) Is Nothing Then
                            If Me.grdWithDrawPlan.Rows(i).Cells("col_isSKURemainingAge_Value").Value Then
                                booSkuRemainCheckValue = True
                                If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Value").Value, GetType(Integer)) Is Nothing Then
                                    intRemainingAge_Value = Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Value").Value
                                End If
                            Else
                            End If
                        End If

                        If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_isSKURemainingAge_Unit").Value, GetType(Boolean)) Is Nothing Then
                            If Me.grdWithDrawPlan.Rows(i).Cells("col_isSKURemainingAge_Unit").Value Then
                                booSkuRemainCheckUnit = True
                                If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Unit").Value, GetType(Integer)) Is Nothing Then
                                    intRemainingAge_Unit = Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Unit").Value
                                End If
                            End If
                        End If


                        If booSkuRemainCheckValue = False Then
                            If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_RemainingAge").Value, GetType(Boolean)) Is Nothing Then
                                If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Value").Value, GetType(Integer)) Is Nothing Then
                                    intRemainingAge_Value = Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Value").Value
                                End If
                            End If
                        End If

                        If booSkuRemainCheckUnit = False Then
                            If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Unit").Value, GetType(Integer)) Is Nothing Then
                                intRemainingAge_Unit = Me.grdWithDrawPlan.Rows(i).Cells("col_RemainingAge_Unit").Value
                            End If
                        End If



                        If intRemainingAge_Value > 0 Then
                            _strPicking &= String.Format(" AND  AgeRemain >= {0}", intRemainingAge_Value)
                        End If

                        If intRemainingAge_Unit > 0 Then
                            _strPicking &= String.Format(" AND AgeRemainPercent >= {0} ", intRemainingAge_Unit)
                        End If

                        '>>c.เงื่อนไข “ห้ามจ่ายสินค้าที่เก่ากว่าของเดิม” คือ ไป Select Max(Plot) หรือ Select Max(Mfg_Date) หรือ Select Max(Exp_Date) มาจากใบเบิกที่ส่งผู้รับปลายทางนั้นๆ / สินค้าตัวนั้น (ms_SKU) / ลูกค้าคนนั้น (ms_Customer) ขึ้นมา แล้วให้กรอง Location Balance ที่น้อยกว่าออกไป
                        strLastIssue_Option = ""
                        If Me.grdWithDrawPlan.Rows(i).Cells("col_Is_GI_NotOlderThanLastIssue").Value Then
                            If Not ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("col_LastIssue_Option").Value, GetType(String)) Is Nothing Then
                                strLastIssue_Option = Me.grdWithDrawPlan.Rows(i).Cells("col_LastIssue_Option").Value
                                Dim objPickingV3 As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                                Dim objDT As New DataTable
                                If strCustomer_Shipping_Location_Index <> "" Then
                                    objDT = objPickingV3.GetDataLastIssueOption(_SKU_index, strCustomer_Shipping_Location_Index, objcon.Connection, myTrans)
                                    objPickingV3 = Nothing
                                    If objDT.Rows.Count > 0 Then
                                        Select Case strLastIssue_Option.ToUpper
                                            Case "E" 'Last EXP Date
                                                If objDT.Rows(0).Item("LastExp_date").ToString <> "" Then
                                                    _strPicking &= String.Format(" AND Exp_Date >= '{0}' ", CDate(objDT.Rows(0).Item("LastExp_date").ToString).ToString("yyyy-MM-dd 00:00:00"))
                                                End If
                                            Case "M" 'Last MFG Date
                                                If objDT.Rows(0).Item("LastMfg_date").ToString <> "" Then
                                                    _strPicking &= String.Format(" AND Mfg_Date >= '{0}' ", CDate(objDT.Rows(0).Item("LastMfg_date").ToString).ToString("yyyy-MM-dd 00:00:00"))
                                                End If
                                            Case "L" 'Last Lot/Batch
                                                If objDT.Rows(0).Item("LastPlot").ToString <> "" Then
                                                    _strPicking &= String.Format(" AND PLot >= '{0}' ", objDT.Rows(0).Item("LastPlot").ToString.Trim)
                                                End If
                                            Case Else
                                        End Select
                                    End If
                                End If
                            End If
                        End If

                        '>>e.เงื่อนไข “สินค้าห้ามจ่าย” คือ ไปเพิ่มเงื่อนไขใน Query Location Balance ให้กรองรายการ SKU นี้ที่มี Lot หรือ Exp. Date หรือ Mfg. Date ที่ระบุไว้ออก



                        If strCustomer_Shipping_Location_Index <> "" Then
                            Dim objPickingV3 As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                            Dim objDT As New DataTable
                            objDT = objPickingV3.GetDataCustomer_Shipping_Location_SKU_Block(_SKU_index, strCustomer_Shipping_Location_Index, objcon.Connection, myTrans)
                            If objDT.Rows.Count > 0 Then
                                For Each dr As DataRow In objDT.Rows
                                    Select Case dr("Block_Type").ToString
                                        Case "0" 'ห้ามจ่ายตาม Lot / Batch 
                                            _strPicking &= String.Format(" AND Plot <> '{0}' ", dr("PLot").ToString.Trim)
                                        Case "1" 'ห้ามจ่ายตามวันหมดอายุ
                                            _strPicking &= String.Format(" AND Exp_Date <> '{0}' ", CDate(dr("Exp_Date")).ToString("yyyy-MM-dd 00:00:00"))
                                        Case "2" 'ห้ามจ่ายตามวันที่ผลิต
                                            _strPicking &= String.Format(" AND Mfg_Date <> '{0}' ", CDate(dr("Mfg_Date")).ToString("yyyy-MM-dd 00:00:00"))
                                        Case Else

                                    End Select


                                Next
                            End If

                        End If



                    End If
                    '--END V3--

                    Select Case StatusWithdraw_format

                        Case Withdraw_format.FIFO
                            Dim objWhere As New config_CustomSetting
                            Dim strWhere As String = objWhere.GetOther_Where("FIFO", "")
                            _strPicking &= strWhere

                            Dim objPicking As New PICKING(PICKING.enmPicking_Type.FIFO, _SKU_index, _Package_index, dblQty_Reserv, _strPicking, cboDocumentType.SelectedValue.ToString)

                            If objPicking.CHEK_QTY_BALANCE(objcon.Connection, myTrans, objcon.SQLServerCommand) = False Then

                                If W_MSG_Confirm(GetMessage_Data(400024) & grdWithDrawPlan.Rows(i).Cells("col_Sku_ID_Plan").Value.ToString & GetMessage_Data(400025) & objPicking.DBLQTY_BALANCE & GetMessage_Data(400026)) = Windows.Forms.DialogResult.Yes Then

                                Else
                                    myTrans.Commit()
                                    Exit Sub
                                End If
                            End If
                            objPicking.strOrderForV3 = _strOrderForV3
                            odtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, 2, Me._Withdraw_index, "")

                        Case Withdraw_format.LIFO
                            Dim objWhere As New config_CustomSetting
                            Dim strWhere As String = objWhere.GetOther_Where("LIFO", "")
                            _strPicking &= strWhere
                            Dim objPicking As New PICKING(PICKING.enmPicking_Type.LIFO, _SKU_index, _Package_index, dblQty_Reserv, _strPicking, cboDocumentType.SelectedValue.ToString)

                            If objPicking.CHEK_QTY_BALANCE(objcon.Connection, myTrans, objcon.SQLServerCommand) = False Then
                                If W_MSG_Confirm(GetMessage_Data(400024) & grdWithDrawPlan.Rows(i).Cells("col_Sku_ID_Plan").Value.ToString & GetMessage_Data(400025) & objPicking.DBLQTY_BALANCE & GetMessage_Data(400026)) = Windows.Forms.DialogResult.Yes Then

                                Else
                                    myTrans.Commit()
                                    Exit Sub
                                End If
                            End If

                            objPicking.strOrderForV3 = _strOrderForV3
                            odtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, 2, Me._Withdraw_index, "")

                        Case Withdraw_format.FEFO
                            Dim objWhere As New config_CustomSetting
                            Dim strWhere As String = objWhere.GetOther_Where("FEFO", "")
                            _strPicking &= strWhere

                            Dim objPicking As New PICKING(PICKING.enmPicking_Type.FEFO, _SKU_index, _Package_index, dblQty_Reserv, _strPicking, cboDocumentType.SelectedValue.ToString)

                            If objPicking.CHEK_QTY_BALANCE(objcon.Connection, myTrans, objcon.SQLServerCommand) = False Then

                                If W_MSG_Confirm(GetMessage_Data(400024) & grdWithDrawPlan.Rows(i).Cells("col_Sku_ID_Plan").Value.ToString & GetMessage_Data(400025) & objPicking.DBLQTY_BALANCE & GetMessage_Data(400026)) = Windows.Forms.DialogResult.Yes Then

                                Else
                                    myTrans.Commit()
                                    Exit Sub
                                End If
                            End If

                            objPicking.strOrderForV3 = _strOrderForV3
                            odtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, 2, Me._Withdraw_index, "")

                        Case Withdraw_format.Pickface
                            Dim _dblQty_Per_Pallet As Decimal = 0.0
                            Dim oGetSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                            Dim dtGetSku As New DataTable
                            oGetSku.SearchData_ByIndex(_SKU_index)
                            dtGetSku = oGetSku.DataTable
                            If dtGetSku.Rows.Count > 0 Then
                                If dtGetSku.Rows(0)("Qty_Per_Pallet").ToString = "" Then
                                    _dblQty_Per_Pallet = 0
                                Else
                                    _dblQty_Per_Pallet = CDec(dtGetSku.Rows(0)("Qty_Per_Pallet").ToString)
                                End If

                                If _dblQty_Per_Pallet <= 0 Then
                                    myTrans.Commit()
                                    W_MSG_Information(GetMessage_Data(400024) & "  : " & grdWithDrawPlan.Rows(i).Cells("col_Sku_ID_Plan").Value.ToString & GetMessage_Data(400027))
                                    Exit Sub
                                End If
                            End If
                            Dim objPicking As New PICKING(PICKING.enmPicking_Type.PICKFACE, _SKU_index, _Package_index, dblQty_Reserv, _strPicking, cboDocumentType.SelectedValue.ToString)
                            If objPicking.CHEK_QTY_BALANCE() = False Then
                                If W_MSG_Confirm(GetMessage_Data(400024) & grdWithDrawPlan.Rows(i).Cells("col_Sku_ID_Plan").Value.ToString & GetMessage_Data(400025) & objPicking.DBLQTY_BALANCE & GetMessage_Data(400026)) = Windows.Forms.DialogResult.Yes Then
                                Else
                                    myTrans.Commit()
                                    Exit Sub
                                End If
                            End If
                            objPicking.strOrderForV3 = _strOrderForV3
                            odtLocationBalance = objPicking.fnPICKING_KSL(objcon.Connection, myTrans, objcon.SQLServerCommand, 2, Me._Withdraw_index, "")
                    End Select

                    If odtLocationBalance Is Nothing Then
                        myTrans.Commit()
                        Continue For
                    End If


                    If odtLocationBalance.Rows.Count = 0 Then
                        myTrans.Commit()
                        Continue For
                    End If





                    Me.grdWithDrawPlan.Rows(i).Cells("col_Package_Qty").Value = odtLocationBalance.Rows(0)("Sku_PackageDescription").ToString
                    Me.grdWithDrawPlan.Rows(i).Cells("col_Package_Item").Value = odtLocationBalance.Rows(0)("Item_Package").ToString
                    Me.grdWithDrawPlan.Rows(i).Cells("col_Pakage_Qty").Value = odtLocationBalance.Rows(0)("Description").ToString

                    StatusWithdraw_Document = Me.grdWithDrawPlan.Rows(i).Cells("col_Plan_Process").Value.ToString
                    Dim strDocumentPlan_No As String = Me.grdWithDrawPlan.Rows(i).Cells("col_DocumentPlan_No").Value.ToString
                    Dim strDocumentPlanItem_Index As String = Me.grdWithDrawPlan.Rows(i).Cells("col_DocumentPlanItem_Index").Value.ToString
                    Dim strDocumentPlan_Index As String = Me.grdWithDrawPlan.Rows(i).Cells("col_DocumentPlan_Index").Value.ToString


                    'killz add Consinee 28-07-2011
                    Dim strConsinee_Index As String = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("Col_Consinee_Index3").Value, GetType(String))
                    Dim strConsinee_Name As String = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(i).Cells("Col_ConsineeName3").Value, GetType(String))

                    Select Case StatusWithdraw_Document
                        Case Withdraw_Document.Packing
                            _Plan_Process = Withdraw_Document.Packing
                            _DocumentPlan_No = strDocumentPlan_No
                            _DocumentPlanItem_Index = strDocumentPlanItem_Index
                            _DocumentPlan_Index = strDocumentPlan_Index
                        Case Withdraw_Document.SO
                            _Plan_Process = Withdraw_Document.SO
                            _DocumentPlan_No = strDocumentPlan_No
                            _DocumentPlanItem_Index = strDocumentPlanItem_Index
                            _DocumentPlan_Index = strDocumentPlan_Index
                            _TConsinee_Index = strConsinee_Index
                            _TConsinee_Name = strConsinee_Name
                    End Select


                    setDataSoucreWithdrawItemLocation(odtLocationBalance, objcon.Connection, myTrans, objcon.SQLServerCommand)

                    _Reference = ""
                    _Reference2 = ""

                    _AssetLocationBalance_Index = ""
                    _Asset_No = ""
                    _AssetSerial_No = ""
                    _NewItem = 1

                    If SetAUTO_REFERENCE() = 1 Then
                        If txtRef_No1.Text <> "" Then
                            _Reference = txtRef_No1.Text
                            _Reference2 = txtRef_No2.Text
                        End If
                    End If


                    'If String.IsNullOrEmpty(odtLocationBalance.Compute("sum(Qty)", "Qty >= 0")) = True Or String.IsNullOrEmpty(Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value) = True Then
                    '    Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value = 0
                    'Else
                    '    Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value = CDec(odtLocationBalance.Compute("sum(Qty)", "Qty >= 0")) + CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value)
                    'End If

                    If String.IsNullOrEmpty(odtLocationBalance.Compute("sum(QtyItemOut)", "QtyItemOut >= 0")) = True Or String.IsNullOrEmpty(Me.grdWithDrawPlan.Rows(i).Cells("col_Item_Qty").Value) = True Then
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Item_Qty").Value = 0
                    Else
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Item_Qty").Value = CDec(odtLocationBalance.Compute("sum(QtyItemOut)", "QtyItemOut >= 0")) + CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Item_Qty").Value)

                    End If
                    'If String.IsNullOrEmpty(odtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty  >= 0")) = True Or String.IsNullOrEmpty(Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value) = True Then
                    '    Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value = 0
                    'Else
                    '    Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value = CDec(odtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty  >= 0")) + CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value)
                    'End If
                    If String.IsNullOrEmpty(odtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty  >= 0")) = True Or String.IsNullOrEmpty(Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value) = True Then
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value = 0
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value = 0
                    Else
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value = (CDec(odtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty >= 0")) / Me.grdWithDrawPlan.Rows(i).Cells("col_Plan_Ratio").Value) + CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value)
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value = CDec(odtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty  >= 0")) + CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Total_Qty_Paln").Value)
                    End If


                    If CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Value.ToString) >= CDec(Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_Plan").Value) Then
                        Me.grdWithDrawPlan.Rows(i).DefaultCellStyle.BackColor = Color.YellowGreen
                    Else
                        Me.grdWithDrawPlan.Rows(i).DefaultCellStyle.BackColor = Color.Gainsboro
                        Me.grdWithDrawPlan.Rows(i).Cells("col_Qty_WithDraw").Style.BackColor = Color.WhiteSmoke
                    End If

                    Me.grdWithDrawPlan.Rows(i).Cells("col_DocumentPlan_No").Style.BackColor = Color.Yellow



                    myTrans.Commit()
                Catch ex As Exception
                    myTrans.Rollback()
                    Throw ex
                End Try

            Next



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            Load_WhitdrawLocation_Edit(_Withdraw_index, 2)
            Get_SumData()
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
                    StatusWithdraw_format = Withdraw_format.Pickface
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


#Region "   Validate Data "
    ''' <summary></summary>
    ''' <param name="podtValidate"></param>
    ''' <param name="pGridName"></param>
    ''' <remarks>
    '''   Create BY : TaTa
    '''   Create Date : 29/07/2010
    '''             SetValidate DataGridColumn  
    ''' </remarks>
    Function SetValidate_DataGridColumn(ByVal podtValidate As DataTable, ByVal pGridName As DataGridView) As Boolean
        Try

            '  If podtValidate Is Nothing Then Exit Function
            Dim strColumnName As String = ""
            Dim odr() As DataRow
            odr = podtValidate.Select("IsRequired = 1 ")

            If odr.Length = 0 Then
                Return True
            End If
            For i As Integer = 0 To pGridName.Rows.Count - 1
                For Each odrTemp As DataRow In odr

                    strColumnName = odrTemp("Column_Name").ToString

                    Dim strDefaultValue As String = odrTemp.Item("Description").ToString
                    Dim chkDefaultValue As Boolean = IsNumeric(strDefaultValue)
                    Select Case chkDefaultValue
                        Case False
                            If pGridName.Rows(i).Cells(strColumnName).Value = strDefaultValue Then
                                Select Case WV_Language
                                    Case enmLanguage.Thai
                                        W_MSG_Information(odrTemp("Msg_Th").ToString)
                                        Return False
                                    Case enmLanguage.English
                                        W_MSG_Information(odrTemp("Msg_En").ToString)
                                        Return False
                                End Select
                            End If

                        Case True
                            Dim dblValue As Decimal = pGridName.Rows(i).Cells(strColumnName).Value
                            If dblValue = CDec(strDefaultValue) Then
                                Select Case W_Module.WV_Language
                                    Case enmLanguage.Thai
                                        W_MSG_Information(odrTemp("Msg_Th").ToString)
                                        Return False
                                    Case enmLanguage.English
                                        W_MSG_Information(odrTemp("Msg_En").ToString)
                                        Return False
                                End Select
                            End If

                    End Select
                Next
            Next
            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    'Function SetValidate_Control(ByVal podtValidate As DataTable, ByVal pstrTypeCheck As String) As Boolean
    '    Try

    '        If podtValidate Is Nothing Then Exit Function

    '        For i As Integer = 0 To podtValidate.Rows.Count - 1
    '            Select Case pstrTypeCheck
    '                Case "SetFont"
    '                    Select Case podtValidate.Rows(i).Item("Field_Name").ToString
    '                        Case "Ref_No1"
    '                            lblRef_No1.Font = Me.lblWithDraw_no.Font

    '                        Case "Ref_No2"
    '                            lblRef_No2.Font = Me.lblWithDraw_no.Font

    '                        Case "Ref_No3"
    '                            lblRef_No3.Font = Me.lblWithDraw_no.Font

    '                        Case "Ref_No4"
    '                            lblRef_No4.Font = Me.lblWithDraw_no.Font

    '                        Case "Ref_No5"
    '                            lblRef_No5.Font = Me.lblWithDraw_no.Font

    '                        Case "Approved_By"
    '                            lblApprovede_By.Font = Me.lblWithDraw_no.Font

    '                        Case "Checker"
    '                            lblChecker_Name.Font = Me.lblWithDraw_no.Font

    '                        Case "Comment"
    '                            lblComment.Font = Me.lblWithDraw_no.Font

    '                        Case "Handling_Type"
    '                            lblHandling_Type.Font = Me.lblWithDraw_no.Font

    '                        Case "SO_No"
    '                            lblSo_No.Font = Me.lblWithDraw_no.Font

    '                        Case "Invoice_No"
    '                            lblInvoice_No.Font = Me.lblWithDraw_no.Font

    '                        Case "Note"
    '                            lblNote.Font = Me.lblWithDraw_no.Font

    '                        Case "Supplier_Index"
    '                            lblShipper.Font = Me.lblWithDraw_no.Font

    '                        Case "Department_Index"
    '                            lblDepartment.Font = Me.lblWithDraw_no.Font

    '                        Case "Consignee_Index"
    '                            lblConsignee.Font = Me.lblWithDraw_no.Font
    '                    End Select

    '                Case "SetValidate"

    '                    Select Case podtValidate.Rows(i).Item("Field_Name").ToString

    '                        Case "Ref_No1"
    '                            If txtRef_No1.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblRef_No1.Text)
    '                                Return False
    '                            End If
    '                        Case "Ref_No2"
    '                            If txtRef_No2.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblRef_No2.Text)
    '                                Return False
    '                            End If

    '                        Case "Ref_No3"
    '                            If txtRef_No3.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblRef_No3.Text)
    '                                Return False
    '                            End If

    '                        Case "Ref_No4"
    '                            If txtRef_No4.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblRef_No4.Text)
    '                                Return False
    '                            End If

    '                        Case "Ref_No5"
    '                            If txtRef_No5.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblRef_No5.Text)
    '                                Return False
    '                            End If

    '                        Case "Approved_By"
    '                            If txtApprovede_By.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblApprovede_By.Text)
    '                                Return False
    '                            End If
    '                        Case "Checker"
    '                            If txtChecker_Name.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblChecker_Name.Text)
    '                                Return False
    '                            End If

    '                        Case "Comment"
    '                            If txtComment.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblComment.Text)
    '                                Return False
    '                            End If

    '                        Case "Handling_Type"
    '                            If cboType.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblHandling_Type.Text)
    '                                Return False
    '                            End If
    '                        Case "SO_No"
    '                            If txtSo_No.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblSo_No.Text)
    '                                Return False
    '                            End If

    '                        Case "Invoice_No"
    '                            If txtInvoice_No.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblInvoice_No.Text)
    '                                Return False
    '                            End If

    '                        Case "Note"
    '                            If txtNote.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblNote.Text)
    '                                Return False
    '                            End If

    '                        Case "tbpHeader1"


    '                        Case "tbpHeader2"


    '                        Case "Control_Image"


    '                        Case "tbpDelivery"


    '                        Case "tbpPlanWithDraw"


    '                        Case "tbpHeader2"


    '                        Case "Supplier_Index"
    '                            If txtShipper_ID.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblShipper.Text)
    '                                Return False
    '                            End If

    '                        Case "Department_Index"
    '                            If txtDepartment_Id.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblDepartment.Text)
    '                                Return False
    '                            End If

    '                        Case "Consignee_Index"

    '                            If txtConsignee_ID.Text = "" Then
    '                                W_MSG_Information("กรุณาป้อน " & lblConsignee.Text)
    '                                Return False
    '                            End If

    '                    End Select

    '            End Select

    '        Next

    '        Return True

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function
#End Region

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If grdWithdrawItemLocation.RowCount = 0 Then
                Exit Sub
            End If
            If W_MSG_Confirm(GetMessage_Data(100047)) = Windows.Forms.DialogResult.Yes Then
                For iRow As Integer = 0 To grdWithdrawItemLocation.Rows.Count - 1
                    DELETE_GRIDWITHDRAWITEM(iRow, True)
                Next
                'grdWithdrawItemLocation.Rows.Clear()
                grdWithdrawItemLocation.DataSource = Nothing
                Get_SumDataPlan()
                Get_SumData()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
        'Try
        '    Dim dtwithdrawitem As New DataTable
        '    dtwithdrawitem = grdWithdrawItemLocation.DataSource
        '    Dim arrdrwithdrawitem() As DataRow
        '    arrdrwithdrawitem = dtwithdrawitem.Select("distinct DocumentPlanItem_Index")
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try

    End Sub

    Private Sub txtRef_No1_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtRef_No1.Leave
        Try
            With grdWithdrawItemLocation
                For i As Integer = 0 To .RowCount - 1
                    .Rows(i).Cells("col_Declaration_No_Out").Value = txtRef_No1.Text
                Next
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAssignJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignJob.Click
        Try
            Dim frm As New WMS_STD_OUTB_WithDraw.frmPopup_AssignJob
            frm.Process_ID = 2
            frm.DocumentPlan_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(DocumentPlan_Index, GetType(String))
            frm.DocumentPlan_No = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(DocumentPlan_No, GetType(String))
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function GENTAGOUT_NO(ByVal pstrWithdraw_No As String, ByVal pstrWithdrawItemLocation_Index As String) As String

        Dim objDBClass As New GenTagOut
        Dim objDT As New DataTable
        Dim running_no As String
        Dim tagout_no As String = ""
        Try
            objDT = objDBClass.GetNoTagOut(pstrWithdrawItemLocation_Index)

            If objDT.Rows.Count = 0 Then '''''


                running_no = objDBClass.GENTAG_OUT("TAGOUT", pstrWithdraw_No, "")

                If txtWithdraw_No.Text = "" Then
                    tagout_no = _Withdraw_no & Val(running_no).ToString("00")
                Else
                    tagout_no = txtWithdraw_No.Text & Val(running_no).ToString("00")
                End If

                Return tagout_no
            Else

                tagout_no = objDT.Rows(0).Item("TagOut_No")

                Return tagout_no
            End If

            Return tagout_no
        Catch ex As Exception
            W_MSG_Error(ex.Message)
            Return ""
        End Try
    End Function

    Private Sub frmWithdrawAsset_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigWithdraw
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2)
                    _odtValidate_grdWithdrawItemLocation = oFunction.SW_Language_Column(Me, Me.grdWithdrawItemLocation, 2)
                    _odtValidate_grdWithDrawPlant = oFunction.SW_Language_Column(Me, Me.grdWithDrawPlan, 2)
                    _odtValidate_grdWithDrawTruckOut = oFunction.SW_Language_Column(Me, Me.grdWithDrawTruckOut, 2)
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub



    Private Sub btnSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerial.Click
        Try
            Select Case objStatus
                Case enuOperation_Type.ADDNEW

                Case enuOperation_Type.UPDATE
                    If Me.grdWithdrawItemLocation.RowCount <= 0 Then Exit Sub
                    If Me.grdWithdrawItemLocation.CurrentRow.Index < 0 Then Exit Sub
                    If Me.grdWithdrawItemLocation.CurrentRow.Cells("col_WithDrawItem_Index2").Value Is Nothing Then Exit Sub
                    Dim frm As New WMS_STD_OUTB_WithDraw.frmWithDrawSerial(Me.dtpWithdraw_Date.Value.Date, _Withdraw_index, Me.grdWithdrawItemLocation.CurrentRow.Cells("col_WithDrawItem_Index2").Value, Me.grdWithdrawItemLocation.CurrentRow.Cells("Col_Orderitem_WIL2").Value, Me.grdWithdrawItemLocation.CurrentRow.Cells("Col_Sku_Index2").Value.ToString)
                    frm.ShowDialog()
            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnViewSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewSO.Click
        Try
            If Me.grdWithDrawPlan.Rows.Count <= 0 Then Exit Sub
            If Me.grdWithDrawPlan.CurrentRow Is Nothing Then Exit Sub
            Dim frm As New frmSO
            frm.objStatus = frmSO.enuOperation_Type.UPDATE
            frm.Status = 3
            frm.SalesOrder_Index = grdWithDrawPlan.CurrentRow.Cells("col_DocumentPlan_Index").Value.ToString()
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Load_WhitdrawLocation_Edit(_Withdraw_index, 2)
            Me.setStatusPicking()
        Catch ex As Exception
            W_Language.W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub setStatusPicking()
        Try
            If Not grdWithdrawItemLocation.Columns.Contains("col_Mobile_Pick") Then
                Exit Sub
            End If
            If Not grdWithdrawItemLocation.Columns.Contains("col_Mobile_Pick_By") Then
                Exit Sub
            End If

            'VIEW_WithDrawAssetItemLocation
            'Mobile_Pick : CASE WHEN tb_WithdrawItem.Status = - 9 THEN 'Picked' ELSE '' END
            'Mobile_Pick_By : CASE WHEN tb_WithdrawItem.Status = - 9 THEN tb_WithdrawItem.update_by ELSE '' END

            Me.col_Mobile_Pick.Visible = False
            Me.col_Mobile_Pick_By.Visible = False

            Dim isPick As Boolean = False

            For a As Integer = 0 To grdWithdrawItemLocation.RowCount - 1
                With grdWithdrawItemLocation.Rows(a)
                    If Not String.IsNullOrEmpty(.Cells("col_Mobile_Pick").Value) Then
                        .Cells("col_Mobile_Pick").Style.BackColor = Color.Yellow
                        .Cells("col_Mobile_Pick_By").Style.BackColor = Color.Yellow
                        isPick = True
                    End If
                    If grdWithdrawItemLocation.Columns.Contains("col_FullFill") Then
                        If Not IsDBNull((.Cells("col_FullFill").Value)) AndAlso Not String.IsNullOrEmpty(.Cells("col_FullFill").Value) Then
                            .Cells("Col_Tag_No2").Style.BackColor = Color.Pink
                        End If
                    End If
                End With
            Next



            If isPick = True Then
                Me.col_Mobile_Pick.Visible = True
                Me.col_Mobile_Pick_By.Visible = True

                Me.col_Mobile_Pick_By.DisplayIndex = 0
                Me.col_Mobile_Pick.DisplayIndex = 0
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
End Class

