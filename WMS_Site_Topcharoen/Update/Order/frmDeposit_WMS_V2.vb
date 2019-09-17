Imports WMS_STD_Master
Imports WMS_STD_Formula
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_Master.W_Language
Imports System.Configuration.ConfigurationSettings
'Imports WMS_INH_SO
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_INB_Report
Imports WMS_STD_CONFIGURATION

Public Class frmDeposit_WMS_V2
    Public DataGridClick As Boolean = False
    Private USE_REPORTPRINTOUT_BOND As Boolean = False
    'Public Plan_Process, DocumentPlanItem_Index, DocumentPlan_Index, Qty As String
    Dim Counter As Integer = 0
    Private _Customer_Index As String = ""
    Private _strPathName As String = ""
    Private _strFileName As String = ""
    Private _strLongFilePath As String = ""
    Private _SalesOrderIndex As String = ""

    Private _strDocumentType As String = ""
    Public _ReceiveType As Integer = 0
    Dim _tmpLocation_Alias As String = ""
    Dim _tmpTotalQty_PutAway As Double = 0
    Dim _tmpQty_PutAway As Double = 0

    Public odtOrderImage As DataTable
    Private _USE_PRODUCT_CUSTOMER As Boolean = False
    Private _odtValidate_Control As DataTable
    Private _odtValidate_grdOrderItem As DataTable
    Private _odtValidate_grdPallet As DataTable
    Private _odtValidate_grdSerial As DataTable
    Private _odtValidate_grdOrderImage As DataTable
    Private _odtValidate_grdOrderTruckIn As DataTable

    Enum enmGenTag_Type
        NotGen = 0
        NormalGen = 1
        GenPerQty = 2
    End Enum

#Region "   Property   "

    Public Receive_Process As Receive_Process_Enum = Receive_Process_Enum.None
    Enum Receive_Process_Enum
        None = -9
        PO = 9
        Packing = 7
        ASN = 16
    End Enum

    Private _DocumentPlan_No As String = ""
    Private _DocumentPlan_Index As String = ""

    Private _USE_PUTAWAY_BY_TAG As Boolean = False
    Public Property USE_PUTAWAY_BY_TAG() As Boolean
        Get
            Return _USE_PUTAWAY_BY_TAG
        End Get
        Set(ByVal value As Boolean)
            _USE_PUTAWAY_BY_TAG = value
        End Set
    End Property
    Public Property Plan_Process() As Receive_Process_Enum
        Get
            Return Receive_Process
        End Get
        Set(ByVal value As Receive_Process_Enum)
            Receive_Process = value
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


    Private _WithdrawStatus As Boolean = False

    Public Property WithdrawStatus() As Boolean
        Get
            Return _WithdrawStatus
        End Get
        Set(ByVal value As Boolean)
            _WithdrawStatus = value
        End Set
    End Property


    Property CustomerIndex() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    'Property SalesOrderIndex() As String
    '    Get
    '        Return _SalesOrderIndex
    '    End Get
    '    Set(ByVal value As String)
    '        _SalesOrderIndex = value
    '    End Set
    'End Property

    Private _Location_Alias As String
    Public Property Location_Alias() As String
        Get
            Return _Location_Alias
        End Get
        Set(ByVal value As String)
            _Location_Alias = value
        End Set
    End Property

    Private _CustomerID As String = ""
    Property CustomerID() As String
        Get
            Return _CustomerID
        End Get
        Set(ByVal value As String)
            _CustomerID = value
        End Set
    End Property


    Private _CustomerName As String = ""
    Property CustomerName() As String
        Get
            Return _CustomerName
        End Get
        Set(ByVal value As String)
            _CustomerName = value
        End Set
    End Property



    Private _status As String = 0

    Public Property status() As String
        Get
            Return _status
        End Get
        Set(ByVal value As String)
            _status = value
        End Set
    End Property

#End Region

#Region "   USE SYSTEM   "
    Private _USE_Gen_SystemLOT As Boolean = False

    Public Property USE_Gen_SystemLOT() As Boolean
        Get
            Return _USE_Gen_SystemLOT
        End Get
        Set(ByVal value As Boolean)
            _USE_Gen_SystemLOT = value
        End Set
    End Property


    Private _USE_PACKING_NEW_PRODUCTION As Boolean = False
    Public Property USE_PACKING_NEW_PRODUCTION() As Boolean
        Get
            Return _USE_PACKING_NEW_PRODUCTION
        End Get
        Set(ByVal value As Boolean)
            _USE_PACKING_NEW_PRODUCTION = value
        End Set
    End Property

    Private _USE_RECEIVED_QTY As Boolean = False

    Public Property USE_RECEIVED_QTY() As Boolean
        Get
            Return _USE_RECEIVED_QTY
        End Get
        Set(ByVal value As Boolean)
            _USE_RECEIVED_QTY = value
        End Set
    End Property


    Private _USE_BLOCK_LOT As Boolean = False

    Public Property USE_BLOCK_LOT() As Boolean
        Get
            Return _USE_BLOCK_LOT
        End Get
        Set(ByVal value As Boolean)
            _USE_BLOCK_LOT = value
        End Set
    End Property

    Private _USE_PUTAWAY_BTNCONFIRM As Boolean = False
    Public Property USE_PUTAWAY_BTNCONFIRM() As Boolean
        Get
            Return _USE_PUTAWAY_BTNCONFIRM
        End Get
        Set(ByVal value As Boolean)
            _USE_PUTAWAY_BTNCONFIRM = value
        End Set
    End Property

    Private _USE_BRANCH_ID As Boolean = False
    Public Property USE_BRANCH_ID() As Boolean
        Get
            Return _USE_BRANCH_ID
        End Get
        Set(ByVal value As Boolean)
            _USE_BRANCH_ID = value
        End Set
    End Property


#End Region

#Region "   DEFAULT SYSTEM   "
    Private _Auto_Fill_Ref1_To As String = ""
    Private _Auto_Fill_Ref2_To As String = ""


    Private _DEFAULT_QTY As Double
    Public Property DEFAULT_QTY() As Double
        Get
            Return _DEFAULT_QTY
        End Get
        Set(ByVal value As Double)
            _DEFAULT_QTY = value
        End Set
    End Property

    Private _DEFAULT_ITEMSTATUS As String
    Public Property DEFAULT_ITEMSTATUS() As String
        Get
            Return _DEFAULT_ITEMSTATUS
        End Get
        Set(ByVal value As String)
            _DEFAULT_ITEMSTATUS = value
        End Set
    End Property



    Private _DEFAULT_AUTO_TAG As enmGenTag_Type
    Public Property DEFAULT_AUTO_TAG() As enmGenTag_Type
        Get
            Return _DEFAULT_AUTO_TAG
        End Get
        Set(ByVal value As enmGenTag_Type)
            _DEFAULT_AUTO_TAG = value
        End Set
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

#End Region

#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        CANCEL
        NULL
    End Enum
#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "


    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub

    Public Sub New(ByVal Operation_Type As enuOperation_Type, ByVal Order_Index As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

        Select Case objStatus

            Case enuOperation_Type.UPDATE
                Me._Order_Index = Order_Index
                Me._Order_Index = Me._Order_Index

        End Select


    End Sub

#End Region

#Region "Variable & Property "
    Private _Order_Index As String

    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property

    Private _OrderItem_Id As String




    Public Property OrderItem_Id() As String
        Get
            Return _OrderItem_Id
        End Get
        Set(ByVal value As String)
            _OrderItem_Id = value
        End Set
    End Property
    Private _LocationID As String


    Public Property LocationID() As String
        Get
            Return _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property

#End Region
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Update Date : 19/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : เพิ่ม Config จำนวน Package
    ''' </remarks>
    Private Sub Config_Btn()
        Me.status = _status
        Me.btnDelete.Enabled = False
        Me.btnCopy.Enabled = False
        Me.btnPalletSlip.Enabled = False
        Me.btnAddPic.Enabled = False
        Me.btnRemovePic.Enabled = False
        Me.btnAdd_TruckIn.Enabled = False
        Me.btnEdit_TruckIn.Enabled = False
        Me.btnDelete_TruckIn.Enabled = False

        Select Case _status
            Case 1, 5, 10, 4
                btnTag.Enabled = True
                btnSave.Enabled = False
                btnEdit.Enabled = True
                btnSerial.Enabled = True
                btnPrintReport.Enabled = True
                cboPrint.Enabled = True
            Case -1
                btnSave.Enabled = False
                btnEdit.Enabled = False
                btnSerial.Enabled = False
                btnPrintReport.Enabled = False
                cboPrint.Enabled = False
            Case 2
                btnSave.Enabled = False
                btnEdit.Enabled = False
                btnSerial.Enabled = False
                btnPrintReport.Enabled = True
                cboPrint.Enabled = True
                Me.btnTag.Enabled = True
        End Select


    End Sub
#Region "Load Config"
    Private Sub Config_Order()
        Dim objClassDB As New tb_Order
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            txtTime.Text = DateTime.Now.ToString("HH:mm")
            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "Control_Pallet"
                            Me.tbcOrder.TabPages.Remove(Me.tbpPallet)
                        Case "Control_Asset"
                            Me.tbcOrder.TabPages.Remove(Me.tbpAsset)
                        Case "Control_Delivery"
                            Me.tbcOrder.TabPages.Remove(Me.tbpDelivery)
                        Case "Control_Image"
                            Me.tbcOrder.TabPages.Remove(Me.tbpImage)
                        Case "Control_ImportExport"
                            Me.tabHeader.TabPages.Remove(Me.tabImportExport)
                    End Select
                    i = i + 1
                Next

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub
    Private Sub Config_Order_Enable()
        Dim objClassDB As New tb_Order
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            txtTime.Text = DateTime.Now.ToString("HH:mm")
            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "Control_Pallet"
                            Me.tbcOrder.TabPages.Remove(Me.tbpPallet)
                        Case "Control_Asset"
                            Me.tbcOrder.TabPages.Remove(Me.tbpAsset)
                        Case "Control_Delivery"
                            Me.tbcOrder.TabPages.Remove(Me.tbpDelivery)
                        Case "Control_Image"
                            Me.tbcOrder.TabPages.Remove(Me.tbpImage)
                        Case "Control_ImportExport"
                            Me.tabHeader.TabPages.Remove(Me.tabImportExport)
                    End Select
                    i = i + 1
                Next

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub
    'Sub config_txtField() ' sub config การเปลี่ยนแปลงชื่อ ของ filed ของหน้าจอต่างๆผ่าน database  และการสลับหน้าจอการทำงานเปง ไทย อิง
    '    Dim Config As New config_txtfile
    '    Dim language As Integer = enmLanguage_frm
    '    Config.txtFile_Click(Me.Name) ' txtFile_Click ค่าที่เราสร้างไว้ใน config_txtfile
    '    Dim dtFiled As New DataTable
    '    dtFiled = Config.DataTable

    '    Dim i As Integer = 0
    '    For i = 0 To dtFiled.Rows.Count - 1 ' นับค่า ข้อมูลในแถว -1 เพื่อเริ่มจากตำแหน่งที่ 0
    '        Dim strControl_name As String = ""
    '        Select Case language
    '            Case 0 ' แบบไทย
    '                strControl_name = dtFiled.Rows(i).Item("control_name_th").ToString()
    '            Case 1 ' แบบอิง
    '                strControl_name = dtFiled.Rows(i).Item("control_name_eng").ToString()

    '        End Select
    '        Dim strtxt_Control As String = dtFiled.Rows(i).Item("control_txt").ToString() 'โดยควบคุมตาม control_txt
    '        'Me.Controls(strtxt_Control).Text = strControl_name
    '        Me.tabWithdraw.TabPages(0).Controls(strtxt_Control).Text = strControl_name
    '    Next
    'End Sub
    Private Sub Config_OrderItem()
        Dim objClassDB As New tb_OrderItem
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            '--- Visible
            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                For Each objDr In objDT.Rows
                    grdOrderItem.Columns(objDr("Controls_Name").ToString).Visible = False
                Next
            End If



        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub
#End Region

#Region "GET DATA TO HEADER"
    Private Sub getDocumentType(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_DocumentType(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
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

    Private Sub getProcess_Ref(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_DocumentType(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcess_Reference(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cboPlanReceive
                Select Case W_Module.WV_Language
                    Case enmLanguage.Thai
                        .DisplayMember = "Description_th"
                    Case enmLanguage.English
                        .DisplayMember = "Description_en"
                End Select

                .ValueMember = "Ref_Process_Id"
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

    Private Sub getCustomerContact(ByVal Cuntomer_Index As String)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
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
                End If

            End If

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

    Private Sub getSupplier()
        If Me.txtSupplier_Id.Tag Is Nothing Then Exit Sub
        Dim objClassDB As New ms_Supplier(ms_Supplier.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try

            objClassDB.getPopup_Supplier("Supplier_Index", Me.txtSupplier_Id.Tag.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me.txtSupplier_Id.Tag = objDT.Rows(0).Item("Supplier_Index").ToString
                Me.txtSupplier_Id.Text = objDT.Rows(0).Item("Supplier_Id").ToString
                Me.txtSupplier_Name.Text = objDT.Rows(0).Item("Supplier_Name").ToString
            Else
                Me.txtSupplier_Id.Tag = ""
                Me.txtSupplier_Id.Text = ""
                Me.txtSupplier_Name.Text = ""
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getDepartment()
        If Me.txtDepartment_Id.Tag Is Nothing Then Exit Sub
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

    Private Sub getOrderHeader(ByVal Order_Index As String)

        ' *** Lock Customer :  Cannot Edit ***
        'btnSeachCustomer.Enabled = False
        txtCustomer_Id.ReadOnly = True
        txtCustomer_Name.ReadOnly = True
        ' ************************************

        Dim objClassDB As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getOrderHeader(Me._Order_Index)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                txtOrder_No.Text = objDT.Rows(0).Item("Order_No").ToString
                dtOrder.Value = objDT.Rows(0).Item("Order_Date").ToShortDateString
                cboDocumentType.SelectedValue = objDT.Rows(0).Item("DocumentType_Index").ToString
                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString

                txtConsignee_ID.Tag = objDT.Rows(0).Item("Consignee_Index").ToString
                txtConsignee_ID.Text = objDT.Rows(0).Item("Consignee_Id").ToString
                txtConsignee_Name.Text = objDT.Rows(0).Item("Company_Name").ToString

                getCustomerContact(Me._Customer_Index) 'Set ค่าผู้ติดต่อตาม ลูกค้า

                txtSupplier_Id.Tag = objDT.Rows(0).Item("Supplier_Index").ToString
                txtDepartment_Id.Tag = objDT.Rows(0).Item("Department_Index").ToString
                txtLot_No.Text = objDT.Rows(0).Item("Lot_No").ToString
                txtTime.Text = objDT.Rows(0).Item("Order_Time").ToString
                txtRef_No1.Text = objDT.Rows(0).Item("Ref_No1").ToString
                txtRef_No2.Text = objDT.Rows(0).Item("Ref_No2").ToString
                txtRef_No3.Text = objDT.Rows(0).Item("Ref_No3").ToString
                txtRef_No4.Text = objDT.Rows(0).Item("Ref_No4").ToString
                txtRef_No5.Text = objDT.Rows(0).Item("Ref_No5").ToString
                txtComment.Text = objDT.Rows(0).Item("Comment").ToString
                txtStr1.Text = objDT.Rows(0).Item("Str1").ToString
                'txtStr2.Text = objDT.Rows(0).Item("Str2").ToString
                txtStr3.Text = objDT.Rows(0).Item("Str3").ToString
                txtStr4.Text = objDT.Rows(0).Item("Str4").ToString
                txtStr5.Text = objDT.Rows(0).Item("Str5").ToString
                cboContact_Name.ValueMember = objDT.Rows(0).Item("Str8").ToString
                cboContact_Name.Text = objDT.Rows(0).Item("Str8").ToString
                txtPlanReceive.Text = objDT.Rows(0).Item("PO_No").ToString
                txtInvoice_No.Text = objDT.Rows(0).Item("Invoice_No").ToString
                txtASN_No.Text = objDT.Rows(0).Item("ASN_No").ToString
                txtChecker_Name.Text = objDT.Rows(0).Item("Checker_Name").ToString
                txtApprovede_By.Text = objDT.Rows(0).Item("ApprovedBy_Name").ToString
                txtQtyPck.Text = objDT.Rows(0).Item("Flo1").ToString


                txtStr2.SelectedValue = objDT.Rows(0).Item("Str2").ToString
                txtStr9.SelectedValue = objDT.Rows(0).Item("Str9").ToString

                '--- tab 2

                '--- Transport
                txtVessel_Name.Text = objDT.Rows(0).Item("Vassel_Name").ToString
                txtFlight_No.Text = objDT.Rows(0).Item("Flight_No").ToString
                txtTransport_by.Text = objDT.Rows(0).Item("Transport_by").ToString
                txtOrigin_Port.Text = objDT.Rows(0).Item("Origin_Port_Id").ToString
                txtDestination_Port.Text = objDT.Rows(0).Item("Destination_Port_Id").ToString
                txtOrigin_Country.Text = objDT.Rows(0).Item("Origin_Country_Id").ToString
                txtDestination_Country.Text = objDT.Rows(0).Item("Destination_Country_Id").ToString
                txtTerminal.Text = objDT.Rows(0).Item("Terminal_Id").ToString


                If objDT.Rows(0).Item("Departure_Date").ToString <> "" Then
                    dtpDeparture_Date.Value = objDT.Rows(0).Item("Departure_Date").ToShortDateString
                End If
                If objDT.Rows(0).Item("Arrival_Date").ToString <> "" Then
                    dtpArrival_Date.Value = objDT.Rows(0).Item("Arrival_Date").ToShortDateString
                End If

                'txtConsignee_Name
                txtChecker_Name.Text = objDT.Rows(0).Item("Checker_Name").ToString
                txtApprovede_By.Text = objDT.Rows(0).Item("ApprovedBy_Name").ToString

                If objDT.Rows(0).Item("HandlingType_Index").ToString = "" Then
                    cboType.SelectedIndex = 0
                Else
                    cboType.SelectedValue = objDT.Rows(0).Item("HandlingType_Index").ToString
                End If

                Me._status = objDT.Rows(0).Item("Status").ToString

            End If

            ' ***** Load Value From other Master *****
            Me.getCustomer()
            Me.getSupplier()
            Me.getDepartment()
            ' ****************************************
            Me.Config_Btn()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

#End Region

#Region "Check Input Data"
    Private Function CheckInputItem() As String

        ' *** Loop to Check Unique Item Line In datagrid **
        Dim j As Integer = 0
        For i As Integer = 0 To grdOrderItem.Rows.Count - 2

            j = i + 1
            If j > grdOrderItem.Rows.Count - 2 Then
                Return ""

            End If

            Dim iItemUnique As String = ""
            Dim iSku_Index As String = ""
            Dim iPlot As String = ""
            Dim iItemStatus_Index As String = ""

            Dim jItemUnique As String = ""
            Dim jSku_Index As String = ""
            Dim jPlot As String = ""
            Dim jItemStatus_Index As String = ""

            With Me.grdOrderItem

                ' *** i value ***

                If .Rows(i).Cells("cbo_ItemStatus").Value IsNot Nothing Then
                    iItemStatus_Index = .Rows(i).Cells("cbo_ItemStatus").Value.ToString
                End If

                If .Rows(i).Cells("col_SKU_Index").Value IsNot Nothing Then
                    iSku_Index = .Rows(i).Cells("col_SKU_Index").Value.ToString
                End If

                If .Rows(i).Cells("col_Plot").Value IsNot Nothing Then
                    iPlot = .Rows(i).Cells("col_Plot").Value.ToString
                End If

                ' *** j value ***
                If .Rows(j).Cells("cbo_ItemStatus").Value IsNot Nothing Then
                    jItemStatus_Index = .Rows(j).Cells("cbo_ItemStatus").Value.ToString
                End If

                If .Rows(j).Cells("col_SKU_Index").Value IsNot Nothing Then
                    jSku_Index = .Rows(j).Cells("col_SKU_Index").Value.ToString
                End If

                If .Rows(j).Cells("col_Plot").Value IsNot Nothing Then
                    jPlot = .Rows(i).Cells("col_Plot").Value.ToString
                End If


            End With
            iItemUnique = iSku_Index + iPlot + iItemStatus_Index
            jItemUnique = jSku_Index + jPlot + jItemStatus_Index
            If iItemUnique = jItemUnique Then
                MessageBox.Show("รายการสินค้าที่รับเข้าซ้ำกัน กรุณาตรวจสอบข้อมูลอีกครั้ง ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return jItemUnique

            End If

        Next

        Return ""
    End Function
#End Region

#Region "Manage DatatimePicker In Gridview "
    Private Sub CalcualateDate_In_Gridview(ByVal iRow As Integer)
        Dim objCalDate As New CalculateDate
        Dim IsMfg_Date As Boolean
        Dim IsExp_Date As Boolean
        Dim strIsMfg_Date As String
        Dim strIsExp_Date As String
        Dim Mfg_Date As DateTime
        Dim Exp_Date As DateTime
        Dim Sku_Index As String
        Dim Output_Date As DateTime
        Try

            With Me.grdOrderItem
                If Me.grdOrderItem.CurrentRow.Index <= -1 Then Exit Sub
                Sku_Index = .Rows(iRow).Cells("col_SKU_Index").Value
                If Sku_Index = "" Then
                    Exit Sub
                End If

                strIsMfg_Date = .Rows(iRow).Cells("chk_Mfg_Date").Value.ToString()
                If strIsMfg_Date.ToUpper = CStr("True").ToUpper Then
                    IsMfg_Date = System.Convert.ToBoolean(strIsMfg_Date)
                ElseIf strIsMfg_Date.ToUpper = CStr("False").ToUpper Then
                    IsMfg_Date = System.Convert.ToBoolean(strIsMfg_Date)
                ElseIf strIsMfg_Date = 1 Then
                    IsMfg_Date = True
                Else
                    IsMfg_Date = False
                End If

                'chkExp_date
                strIsExp_Date = .Rows(iRow).Cells("chkExp_Date").Value.ToString
                If strIsExp_Date.ToUpper = CStr("True").ToUpper Then
                    IsExp_Date = System.Convert.ToBoolean(strIsExp_Date)
                ElseIf strIsExp_Date.ToUpper = CStr("False").ToUpper Then
                    IsExp_Date = System.Convert.ToBoolean(strIsExp_Date)

                ElseIf strIsExp_Date = 1 Then
                    IsExp_Date = True
                Else
                    IsExp_Date = False

                End If

                'IsExp_Date = System.Convert.ToBoolean(strIsExp_Date)

                If Me.grdOrderItem.Rows(iRow).Cells("col_Mfg_Date").Value IsNot Nothing Then
                    Dim strMfg_Date As String = .Rows(iRow).Cells("col_Mfg_Date").Value.ToString
                    Mfg_Date = System.Convert.ToDateTime(strMfg_Date)
                End If

                ' Mfg_Date = CDate(strMfg_Date)

                '    .Rows(i).Cells("col_Mfg_Date").Value = Format(CDate(objDT.Rows(i).Item("Mfg_Date")), "dd/MM/yyyy").ToString
                '    .Rows(i).Cells("Exp_Date").Value = Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString

                If IsMfg_Date = True And IsExp_Date = False Then
                    ' *** Know Mfg_date : Using Mfg_date for Calculate Exp_Date ***  
                    Output_Date = objCalDate.getExpDate_With_MfgDate(Sku_Index, Mfg_Date)
                    .Rows(iRow).Cells("col_Exp_date").Value = Format(Output_Date, "dd/MM/yyyy").ToString
                    Exit Sub
                End If
                If .Rows(iRow).Cells("col_Exp_Date").Value IsNot Nothing Then
                    Dim strExp_Date As String = .Rows(iRow).Cells("col_Exp_Date").Value.ToString
                    Exp_Date = System.Convert.ToDateTime(strExp_Date)
                End If


                If IsMfg_Date = False And IsExp_Date = True Then
                    ' *** Know Exp_date : Using Exp_Date for Calculate Mfg_Date ***
                    Output_Date = objCalDate.getMfgDate_With_ExpDate(Sku_Index, Exp_Date)
                    .Rows(iRow).Cells("col_Mfg_Date").Value = Format(Output_Date, "dd/MM/yyyy").ToString
                    Exit Sub
                End If

                If IsMfg_Date = False And IsExp_Date = False Then
                    ' *** Don't Know Mfg_date and Exp_Date: Using Order_Date for Calculate Exp_Date
                    .Rows(iRow).Cells("col_Mfg_Date").Value = Format(Me.dtOrder.Value, "dd/MM/yyyy").ToString
                    Output_Date = objCalDate.getExpDate_With_OrderDate(Sku_Index, Me.dtOrder.Value)
                    .Rows(iRow).Cells("col_Exp_Date").Value = Format(Output_Date, "dd/MM/yyyy").ToString
                    Exit Sub
                End If


            End With

        Catch ex As Exception
            Throw ex
        Finally
            objCalDate = Nothing
        End Try


    End Sub
#End Region

#Region "GET DATA TO DATAGRIDVIEW"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Sku_Index"></param>
    ''' <param name="RowIndex"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Date 14/01/2010
    ''' Update By : Dong_kk 
    ''' เพิ่มการรับหน่วยที่รับได้ต้องเป็นหน่วยที่เล็กกว่าเท่านั้น
    ''' </remarks>
    Private Function getSKU_Package(ByVal Sku_Index As String, ByVal Package_Index As String, ByVal RowIndex As Integer) As Integer
        Try
            Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable


            If Package_Index = "" Then
                objClassDB.getSKU_Package(Sku_Index)
                objDT = objClassDB.DataTable
            Else
                objClassDB.getSKU_PackageReceive(Sku_Index, Package_Index)
                objDT = objClassDB.DataTable
            End If


            If objDT.Rows.Count > 0 Then
                Dim dgvcob As New DataGridViewComboBoxCell
                dgvcob.DisplayMember = "Package"
                dgvcob.ValueMember = "Package_Index"
                dgvcob.DataSource = objDT
                grdOrderItem.Rows(RowIndex).Cells("cbo_Receive_Package") = dgvcob

                Return 1
            Else
                Return -1
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            Return -1
        End Try

    End Function


    Private Function USE_DEFAULT_LOCATION_SKU() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_DEFAULT_LOCATION_SKU", " AND Config_Value = 1 ")
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Sku_Id"></param>
    ''' <param name="RowIndex"></param>
    ''' <remarks>
    ''' Date : 12/01/2010
    ''' Update By : ja Update 
    ''' เมื่อ เลือก Consignee ตรงหัว ให้มา Auto ค่าเริ่มที่ Item
    ''' -------------------------------------------------- 
    ''' Date 14/01/2010
    ''' Update By : Dong_kk
    ''' check Ratio ของหน่วยที่รับต้องน้อย กว่าหน่อยของ ตัวตั้งต้น
    ''' </remarks>

    Private Sub getSKU_Detail(ByVal Sku_Id As String, ByVal RowIndex As Integer)
        Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strSku_Index As String = ""
        Dim strItem_Package_Index As String = ""
        Dim strPackage_Index As String = ""
        Try
            objClassDB.getSKU_Detail(Sku_Id)
            objDT = objClassDB.DataTable

            'Not Found SKU ID
            If objDT.Rows.Count = 0 Then
                If Sku_Id = "" Then Exit Sub
                W_MSG_Information("ไม่พบรหัส SKU นี้ ในฐานข้อมูล กรุณาป้อนรหัส SKU ใหม่อีกครั้ง")

                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Id").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Description").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_ProductName_th").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag = ""
                grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value = "0"
                'include from master site
                grdOrderItem.Rows(RowIndex).Cells("col_Customer_SKU_ID").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_Supplier_SKU_ID").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_Brand").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_Model").Value = ""

                grdOrderItem.Rows(RowIndex).Cells("col_Tax1").Value = "0"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax2").Value = "0"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax3").Value = "0"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax4").Value = "0"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax5").Value = "0"
                Exit Sub
            End If

            If objDT.Rows.Count > 0 Then

                If Me._USE_PRODUCT_CUSTOMER = True Then
                    'AndAlso objDT.Rows(0).Item("Customer_Index").ToString <> Me.CustomerIndex Then
                    If objDT.Rows(0).Item("Customer_Index").ToString <> "" Then
                        If objDT.Rows(0).Item("Customer_Index").ToString <> Me.CustomerIndex Then
                            W_MSG_Information(String.Format("สินค้ามี{0}ไม่ตรงกับที่ระบุ !!", Me.lblCustomer.Text.Trim))
                            grdOrderItem.Rows(RowIndex).Cells("col_SKU_Id").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_SKU_Description").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_ProductName_th").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value = "0"
                            'include from master site
                            grdOrderItem.Rows(RowIndex).Cells("col_Customer_SKU_ID").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_Supplier_SKU_ID").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_Brand").Value = ""
                            grdOrderItem.Rows(RowIndex).Cells("col_Model").Value = ""

                            grdOrderItem.Rows(RowIndex).Cells("col_Tax1").Value = "0"
                            grdOrderItem.Rows(RowIndex).Cells("col_Tax2").Value = "0"
                            grdOrderItem.Rows(RowIndex).Cells("col_Tax3").Value = "0"
                            grdOrderItem.Rows(RowIndex).Cells("col_Tax4").Value = "0"
                            grdOrderItem.Rows(RowIndex).Cells("col_Tax5").Value = "0"
                            Exit Sub
                        End If

                    End If

                End If

                '====================
                If objDT.Rows(0).Item("status_id").ToString = "-1" Then
                    W_MSG_Information("SKU Is Cancel.")

                    grdOrderItem.Rows(RowIndex).Cells("col_SKU_Id").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_SKU_Description").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_ProductName_th").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value = "0"
                    'include from master site
                    grdOrderItem.Rows(RowIndex).Cells("col_Customer_SKU_ID").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_Supplier_SKU_ID").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_Brand").Value = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_Model").Value = ""

                    grdOrderItem.Rows(RowIndex).Cells("col_Tax1").Value = "0"
                    grdOrderItem.Rows(RowIndex).Cells("col_Tax2").Value = "0"
                    grdOrderItem.Rows(RowIndex).Cells("col_Tax3").Value = "0"
                    grdOrderItem.Rows(RowIndex).Cells("col_Tax4").Value = "0"
                    grdOrderItem.Rows(RowIndex).Cells("col_Tax5").Value = "0"
                    Exit Sub
                End If
                '==============
            End If
            ' *** set default of checkbox ****
            grdOrderItem.Rows(RowIndex).Cells("chk_Mfg_Date").Value = False
            grdOrderItem.Rows(RowIndex).Cells("chkExp_Date").Value = False

            If USE_DEFAULT_LOCATION_SKU() Then
                If objDT.Rows.Count > 0 Then
                    grdOrderItem.Rows(RowIndex).Cells("Col_location").Value = objDT.Rows(0).Item("Location_Alias").ToString
                End If

            End If

            If Me._Location_Alias <> "" Then
                grdOrderItem.Rows(RowIndex).Cells("Col_location").Value = Me._Location_Alias
            End If

            If Col_location.Visible = False Then
                grdOrderItem.Rows(RowIndex).Cells("Col_location").Value = ""
            End If

            If Me._DEFAULT_QTY > 0 Then
                grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value = Me._DEFAULT_QTY
            End If

            ' 12/01/2010 By ja Auto Consignee
            If txtConsignee_ID.Tag <> "" Then
                grdOrderItem.Rows(RowIndex).Cells("Col_Consignee").Tag = txtConsignee_ID.Tag
                grdOrderItem.Rows(RowIndex).Cells("Col_Consignee").Value = txtConsignee_Name.Text
            End If


            If _Auto_Fill_Ref1_To <> "" Then
                If Me.grdOrderItem.Columns.Contains(_Auto_Fill_Ref1_To) Then
                    grdOrderItem.Rows(RowIndex).Cells(_Auto_Fill_Ref1_To).Value = txtRef_No1.Text
                End If

            End If

            If _Auto_Fill_Ref2_To <> "" Then
                If Me.grdOrderItem.Columns.Contains(_Auto_Fill_Ref2_To) Then
                    grdOrderItem.Rows(RowIndex).Cells(_Auto_Fill_Ref2_To).Value = txtRef_No2.Text
                End If

            End If

            If _USE_Gen_SystemLOT Then
                If cboDocumentType.SelectedValue = GetDocumentType_NotGenSystemLot() Then
                    grdOrderItem.Rows(RowIndex).Cells("col_Reference2").Value = ""
                Else
                    Dim StrDate As String = Date.Now.ToString("yyyyMMdd")
                    grdOrderItem.Rows(RowIndex).Cells("col_Reference2").Value = StrDate
                End If
            End If


            If objDT.Rows.Count > 0 Then
                'grdOrderItem.Rows(RowIndex).Cells("col_Seq").Value = RowIndex + 1
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value = objDT.Rows(0).Item("Sku_Index").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Id").Value = objDT.Rows(0).Item("Sku_Id").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Description").Value = objDT.Rows(0).Item("Str1").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_ProductName_th").Value = objDT.Rows(0).Item("Product_Name_th").ToString

                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag = objDT.Rows(0).Item("Package_Index").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Value = objDT.Rows(0).Item("Package").ToString


                '----------------------------------------------------------
                'killz 30-04-2012
                If objDT.Rows(0).Item("DimensionRatio").ToString <> "" Then
                    grdOrderItem.Rows(RowIndex).Cells("col_DimensionRatio").Value = objDT.Rows(0).Item("DimensionRatio").ToString
                End If
                If objDT.Rows(0).Item("DimensionType_Id").ToString <> "" Then
                    grdOrderItem.Rows(RowIndex).Cells("Col_DimensionType_Id").Value = objDT.Rows(0).Item("DimensionType_Id").ToString
                Else
                    grdOrderItem.Rows(RowIndex).Cells("Col_DimensionType_Id").Value = "ไม่ระบุ"
                End If
                '------------------------------------------------------------

                'Reset when data Fail
                If Not IsNumeric(objDT.Rows(0).Item("Unit_Width")) Then objDT.Rows(0).Item("Unit_Width") = 0
                If Not IsNumeric(objDT.Rows(0).Item("Unit_Length")) Then objDT.Rows(0).Item("Unit_Length") = 0
                If Not IsNumeric(objDT.Rows(0).Item("Unit_Height")) Then objDT.Rows(0).Item("Unit_Height") = 0
                If Not IsNumeric(objDT.Rows(0).Item("Unit_Volume")) Then objDT.Rows(0).Item("Unit_Volume") = 0
                If Not IsNumeric(objDT.Rows(0).Item("UnitWeight")) Then objDT.Rows(0).Item("UnitWeight") = 0
                If Not IsNumeric(objDT.Rows(0).Item("Price1")) Then objDT.Rows(0).Item("Price1") = 0


                grdOrderItem.Rows(RowIndex).Cells("col_Width_PerPackage").Value = FormatNumber(objDT.Rows(0).Item("Unit_Width"), 4)
                grdOrderItem.Rows(RowIndex).Cells("col_Long_PerPackage").Value = FormatNumber(objDT.Rows(0).Item("Unit_Length"), 4)
                grdOrderItem.Rows(RowIndex).Cells("col_Height_PerPackage").Value = FormatNumber(objDT.Rows(0).Item("Unit_Height"), 4)
                grdOrderItem.Rows(RowIndex).Cells("col_Volume_PerPackage").Value = FormatNumber(objDT.Rows(0).Item("Unit_Volume"), 4)
                grdOrderItem.Rows(RowIndex).Cells("col_Weight_PerPackage").Value = FormatNumber(objDT.Rows(0).Item("UnitWeight"), 4)
                grdOrderItem.Rows(RowIndex).Cells("col_ItemPrice_PerPackage").Value = FormatNumber(objDT.Rows(0).Item("Price1"), 4)

                If IsNumeric(grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value) Then
                    grdOrderItem.Rows(RowIndex).Cells("col_Volume").Value = FormatNumber(CDbl(objDT.Rows(0).Item("Unit_Volume").ToString) * CDbl(grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value), 4)
                    grdOrderItem.Rows(RowIndex).Cells("col_Weight").Value = FormatNumber(CDbl(objDT.Rows(0).Item("UnitWeight").ToString) * CDbl(grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value), 4)
                    grdOrderItem.Rows(RowIndex).Cells("col_ItemPrice").Value = FormatNumber(CDbl(objDT.Rows(0).Item("Price1").ToString) * CDbl(grdOrderItem.Rows(RowIndex).Cells("col_Qty").Value), 4)
                Else
                    grdOrderItem.Rows(RowIndex).Cells("col_Volume").Value = FormatNumber(objDT.Rows(0).Item("Unit_Volume"), 4)
                    grdOrderItem.Rows(RowIndex).Cells("col_Weight").Value = FormatNumber(objDT.Rows(0).Item("UnitWeight"), 4)
                    grdOrderItem.Rows(RowIndex).Cells("col_ItemPrice").Value = FormatNumber(objDT.Rows(0).Item("Price1"), 4)
                End If

                ' *** Using Value of strSku_Index for Search Package
                If objDT.Rows(0).Item("Item_Package_Index") IsNot Nothing Then
                    strItem_Package_Index = objDT.Rows(0).Item("Item_Package_Index").ToString
                End If

                ' *** Using Value of strSku_Index for Search Package
                strSku_Index = grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value

                ' include from master site
                grdOrderItem.Rows(RowIndex).Cells("col_Customer_SKU_ID").Value = objDT.Rows(0).Item("str4").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_Supplier_SKU_ID").Value = objDT.Rows(0).Item("str5").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_Brand").Value = objDT.Rows(0).Item("Brand").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_Model").Value = objDT.Rows(0).Item("Model").ToString

                grdOrderItem.Rows(RowIndex).Cells("col_Tax1").Value = "0.00"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax2").Value = "0.00"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax3").Value = "0.00"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax4").Value = "0.00"
                grdOrderItem.Rows(RowIndex).Cells("col_Tax5").Value = "0.00"

                Me.CalcualateDate_In_Gridview(RowIndex)
            Else
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Description").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_ProductName_th").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Value = ""
                grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag = ""

                ' *** Using Value of strSku_Index for Search Package
                strSku_Index = grdOrderItem.Rows(RowIndex).Cells("col_SKU_Index").Value
                strPackage_Index = ""
            End If

            'ถ้าเป็นการรับ จากการตั้งต้น
            If grdOrderItem.Rows(RowIndex).Cells("col_Plan_Package").Tag IsNot Nothing Then
                strPackage_Index = grdOrderItem.Rows(RowIndex).Cells("col_Plan_Package").Tag.ToString
            Else
                strPackage_Index = ""
            End If


            If Not strSku_Index = "" Then
                ' Search SKU Package 
                If getSKU_Package(strSku_Index, strPackage_Index, RowIndex) < 0 Then
                    W_MSG_Information_ByIndex(500004)
                    Exit Sub
                End If
                'add Item package
                If grdOrderItem.Rows(RowIndex).Cells("cbo_Package_ItemQty").Visible = True Then
                    If Me.get_Package(RowIndex, strSku_Index) = False Then
                        grdOrderItem.Rows(RowIndex).Cells("cbo_Package_ItemQty").Value = ""
                        W_MSG_Information_ByIndex(500008)
                        Exit Sub
                    End If
                End If

                Me.getgrdHandlingType(RowIndex) ' add HandlingType

                ' Set Default Item Package
                If strItem_Package_Index <> "" Then
                    grdOrderItem.Rows(RowIndex).Cells("cbo_Package_ItemQty").Value = strItem_Package_Index
                End If


                grdOrderItem.Rows(RowIndex).Cells("cbo_Receive_Package").Value = grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag
                grdOrderItem.Rows(RowIndex).Cells("cbo_Receive_Package").Tag = grdOrderItem.Rows(RowIndex).Cells("col_SKU_Package").Tag


                Me.getDocumentType_Itemstatus(RowIndex)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Function get_Package(ByVal RowIndex As Integer, ByVal Sku_index As String) As Boolean
        Dim objClassDB As New SQLCommands
        Dim objDT As DataTable = New DataTable
        Try
            If Sku_index = "" Then
                objClassDB.SQLComand("select package_index,Description from ms_package where package_index not in ('-1') and isItem_Package=1")
                objDT = objClassDB.DataTable
            Else
                Dim oMs_sku As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
                oMs_sku.GetItemPackage_Search(Sku_index, 1, "")
                objDT = oMs_sku.GetDataTable
            End If

            Dim dgvcob As New DataGridViewComboBoxCell

            dgvcob.DisplayMember = "Description"
            dgvcob.ValueMember = "Package_Index"
            dgvcob.DataSource = objDT

            If objDT.Rows.Count > 0 Then
                grdOrderItem.Rows(RowIndex).Cells("cbo_Package_ItemQty") = dgvcob
                grdOrderItem.Rows(RowIndex).Cells("cbo_Package_ItemQty").Value = objDT.Rows(0).Item("Package_Index").ToString
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Function
    Private Sub getgrdHandlingType(ByVal RowIndex As Integer)
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
            grdOrderItem.Rows(RowIndex).Cells("cbo_HandlingType") = dgvcob
            grdOrderItem.Rows(RowIndex).Cells("cbo_HandlingType").Value = objDTType.Rows(0).Item("HandlingType_Index").ToString()

        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing
        End Try
    End Sub

    Function GetDocumentType_NotGenSystemLot()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Dim _DocumentTypeIndex As String = ""
        Try
            objCustomSetting.GetConfig_Value("DocumentType_Index_NotGenSystemLot")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                _DocumentTypeIndex = objDT.Rows(0).Item("Config_Value").ToString

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

        Return _DocumentTypeIndex
    End Function

    Private Sub getItemStatus_To_LineItem(ByVal ItemStatus_Id As String, ByVal RowIndex As Integer)
        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strItemStatus_Index As String = ""
        Try
            objClassDB.getItemStatus(ItemStatus_Id)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then

                grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Tag = objDT.Rows(0).Item("ItemStatus_Index").ToString
                grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = objDT.Rows(0).Item("ItemStatus").ToString
                'strItemStatus_Index = grdOrderItem.Rows(RowIndex).Cells("ItemStatus_Index_Hiden").Value
                strItemStatus_Index = grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Tag
            Else
                grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Tag = ""
                grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = ""

            End If

            If Not strItemStatus_Index = "" Then
                If Not grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Tag = "" Then
                    ' set value Recieve Package from database 
                    grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Tag = strItemStatus_Index
                    grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = objDT.Rows(0).Item("ItemStatus").ToString
                Else
                    ' set default 
                    grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Tag = ""
                    grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = ""
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getItemDefinition_To_LineItem(ByVal ItemDefinition_Id As String, ByVal RowIndex As Integer)
        Dim objClassDB As New ms_ItemDefinition(ms_ItemDefinition.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strItemDefinition_Index As String = ""
        Try
            objClassDB.getItemDefinition(ItemDefinition_Id)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Tag = objDT.Rows(0).Item("ItemDefinition_Index").ToString
                grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Value = objDT.Rows(0).Item("ItemDefinition_Name").ToString
                strItemDefinition_Index = grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Tag
            Else
                grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Tag = ""
                grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Value = ""
            End If

            If Not strItemDefinition_Index = "" Then
                If Not grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Tag = "" Then
                    ' set value Recieve Package from database
                    grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Tag = strItemDefinition_Index
                    grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Value = objDT.Rows(0).Item("ItemDefinition_Name_th").ToString
                Else
                    ' set default 
                    grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Tag = ""
                    grdOrderItem.Rows(RowIndex).Cells("col_ItemDefinition").Value = ""
                End If
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getPalletType()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New ms_PalletType(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPalletType()
            objDT = objClassDB.DataTable
            With cbo_PalletType
                .DisplayMember = "Pallet_Name"
                .ValueMember = "PalletType_Index"
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

    Private Sub getPalletTypeToGRD()
        Dim objClassDB As New tb_PalletType_History
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPalletType()
            objDT = objClassDB.DataTable
            Me.grdPallet.DataSource = Nothing
            Me.grdPallet.Rows.Clear()

            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdPallet
                    Me.grdPallet.Rows.Add()
                    .Rows(i).Cells("col_Palletindex").Value = objDT.Rows(i).Item("PalletType_Index").ToString
                    .Rows(i).Cells("col_PalletType").Value = objDT.Rows(i).Item("Pallet_Name").ToString
                    .Rows(i).Cells("col_Palletqty").Value = objDT.Rows(i).Item("PalletQTY").ToString

                End With
            Next

            grdPallet.Rows(0).Cells("col_UsePallet").Value = "0"

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getItemStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_ItemStatus(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatus()
            objDT = objClassDB.DataTable

            With cbo_ItemStatus
                .DisplayMember = "ItemStatus"
                .ValueMember = "ItemStatus_Index"
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

    Private Sub getItemDefinition()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_ItemDefinition(ms_ItemDefinition.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemDefinition()
            objDT = objClassDB.DataTable

            With col_ItemDefinition
                .DisplayMember = "ItemDefinition_Name"
                .ValueMember = "ItemDefinition_Index"
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

    Private Sub getOrderItemDetail(ByVal Order_Index As String)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getOrderItem(Order_Index)
            objDT = objClassDB.DataTable
            Me.grdOrderItem.DataSource = Nothing
            Me.grdOrderItem.Rows.Clear()
            DataGridClick = False
            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdOrderItem
                    Me.grdOrderItem.Rows.Add()
                    'begin dong_kk 16/08/2011 วันที่หมดอายุต้อง Assinge ก่อนสินค้า
                    ' *** set Value of CheckBox *** 
                    Counter = -9
                    ' *** set Format date ***
                    .Rows(i).Cells("col_Mfg_Date").Value = Format(CDate(objDT.Rows(i).Item("Mfg_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("col_Exp_Date").Value = Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString
                    Counter = 0
                    'end dong_kk 16/08/2011 วันที่หมดอายุต้อง Assinge ก่อนสินค้า

                    .Rows(i).Cells("col_OrderItem_Index").Value = objDT.Rows(i).Item("OrderItem_Index").ToString
                    '  .Rows(i).Cells("Order_Date").Value = Format(CDate(objDT.Rows(i).Item("Order_Date")), "dd/MM/yyyy").ToString '  ' Format(Today, "dd/MM/yyyy").ToString 
                    .Rows(i).Cells("col_SKU_Index").Value = objDT.Rows(i).Item("Sku_Index").ToString
                    .Rows(i).Cells("col_SKU_Index").Tag = objDT.Rows(i).Item("Package_Index").ToString

                    .Rows(i).Cells("col_SKU_Id").Value = objDT.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("col_SKU_Description").Value = objDT.Rows(i).Item("SKU_Description").ToString
                    ' *** set ReadOnly *** 
                    .Rows(i).Cells("col_SKU_Id").ReadOnly = True
                    'get ComboBox Datagrid no load value Change
                    Me.getSKU_Package(objDT.Rows(i).Item("Sku_Index").ToString, objDT.Rows(i).Item("Package_Index").ToString, i)
                    Me.get_Package(i, objDT.Rows(i).Item("Sku_Index").ToString)
                    Me.getgrdHandlingType(i)
                    Me.getDocumentType_Itemstatus(i)



                    ' *** set value of PalletType_Index to combo ***
                    .Rows(i).Cells("cbo_PalletType").Tag = Trim(objDT.Rows(i).Item("PalletType_Index").ToString)
                    .Rows(i).Cells("cbo_PalletType").Value = Trim(.Rows(i).Cells("cbo_PalletType").Tag).ToString
                    .Rows(i).Cells("col_Pallet_No").Value = Trim(objDT.Rows(i).Item("str5").ToString)

                    .Rows(i).Cells("cbo_ItemStatus").Value = objDT.Rows(i).Item("ItemStatus_Index").ToString
                    '  .Rows(i).Cells("col_status").Value = objDT.Rows(i).Item("Status").ToString

                    '------------------------------
                    'แก้ไขใช้งาน ชั่วคราว
                    .Rows(i).Cells("col_PO_Number").Value = objDT.Rows(i).Item("str1").ToString 'เลขที่ PO


                    .Rows(i).Cells("ColumnPO_Item").Value = objDT.Rows(i).Item("str7").ToString
                    '.Rows(i).Cells("ColumnReference").Value = objDT.Rows(i).Item("str2").ToString
                    '.Rows(i).Cells("ColumnItemRef").Value = objDT.Rows(i).Item("str3").ToString
                    '.Rows(i).Cells("ColumnINVOICE").Value = objDT.Rows(i).Item("str4").ToString
                    .Rows(i).Cells("col_PO_Number").Value = objDT.Rows(i).Item("PO_No").ToString
                    .Rows(i).Cells("col_Invoice").Value = objDT.Rows(i).Item("Invoice_No").ToString
                    .Rows(i).Cells("col_Coment").Value = objDT.Rows(i).Item("Comment").ToString
                    If Not IsDBNull(objDT.Rows(i).Item("Is_SN")) Then
                        Select Case objDT.Rows(i).Item("Is_SN")
                            Case True
                                ' Me.grdOrderItem.Rows(i).Cells("col_Qty").ReadOnly = True
                                ' Me.grdOrderItem.Rows(i).Cells("col_Qty").Style.BackColor = Color.Gainsboro
                                ' Me.grdOrderItem.Rows(i).Cells("col_SKU_Id").Style.BackColor = Color.Pink

                        End Select
                    End If


                    .Rows(i).Cells("col_Order_Index").Value = objDT.Rows(i).Item("Order_Index").ToString


                    .Rows(i).Cells("col_Width_PerPackage").Value = FormatNumber(objDT.Rows(i).Item("flo2"), 4)
                    .Rows(i).Cells("col_Long_PerPackage").Value = FormatNumber(objDT.Rows(i).Item("flo3"), 4)
                    .Rows(i).Cells("col_Height_PerPackage").Value = FormatNumber(objDT.Rows(i).Item("flo4"), 4)
                    .Rows(i).Cells("col_Qty_PerPackage").Value = objDT.Rows(i).Item("Qty_Per_Pck") 'FormatNumber(objDT.Rows(i).Item("Qty_Per_Pck"), 2)
                    .Rows(i).Cells("col_Weight_PerPackage").Value = FormatNumber(objDT.Rows(i).Item("Weight_Per_Pck"), 4)
                    .Rows(i).Cells("col_Volume_PerPackage").Value = FormatNumber(objDT.Rows(i).Item("Volume_Per_Pck"), 4)
                    .Rows(i).Cells("col_ItemPrice_PerPackage").Value = objDT.Rows(i).Item("Price_Per_Pck") 'FormatNumber(objDT.Rows(i).Item("Price_Per_Pck"), 2)

                    .Rows(i).Cells("col_ItemPrice").Value = FormatNumber(objDT.Rows(i).Item("OrderItem_Price"), 4)
                    .Rows(i).Cells("col_Item_Qty").Value = objDT.Rows(i).Item("Item_Qty") 'FormatNumber(objDT.Rows(i).Item("Item_Qty"), 2)
                    .Rows(i).Cells("col_Qty").Value = objDT.Rows(i).Item("Qty") 'FormatNumber(objDT.Rows(i).Item("Qty"), 2)
                    .Rows(i).Cells("col_Plan_Qty").Value = objDT.Rows(i).Item("Plan_Qty") 'FormatNumber(objDT.Rows(i).Item("Plan_Qty"), 2)
                    .Rows(i).Cells("col_Volume").Value = FormatNumber(objDT.Rows(i).Item("Volume"), 4)
                    .Rows(i).Cells("col_Net_Weight").Value = FormatNumber(objDT.Rows(i).Item("Flo1"), 4)
                    .Rows(i).Cells("col_Pallet_Qty").Value = objDT.Rows(i).Item("Pallet_Qty") 'FormatNumber(objDT.Rows(i).Item("Pallet_Qty"), 2)
                    '
                    .Rows(i).Cells("col_Weight").Value = FormatNumber(objDT.Rows(i).Item("Weight"), 4)
                    '
                    .Rows(i).Cells("col_Declaration_No").Value = objDT.Rows(i).Item("Declaration_No").ToString
                    .Rows(i).Cells("col_Invoice").Value = objDT.Rows(i).Item("Invoice_No").ToString
                    .Rows(i).Cells("col_PLot").Value = objDT.Rows(i).Item("PLot").ToString
                    .Rows(i).Cells("col_Serial_No").Value = objDT.Rows(i).Item("Serial_No").ToString

                    'Me.get_Package(i, objDT.Rows(i).Item("Sku_Index").ToString)

                    'Me.getgrdHandlingType(i)

                    .Rows(i).Cells("cbo_HandlingType").Value = objDT.Rows(i).Item("HandlingType_Index").ToString
                    .Rows(i).Cells("cbo_Package_ItemQty").Value = objDT.Rows(i).Item("Item_Package_Index").ToString
                    .Rows(i).Cells("cbo_Receive_Package").Value = objDT.Rows(i).Item("Package_Index").ToString

                    'Dong add for error split lot
                    .Rows(i).Cells("col_SKU_Package").Tag = objDT.Rows(i).Item("Package_Index").ToString
                    .Rows(i).Cells("col_SKU_Package").Value = objDT.Rows(i).Item("Package_Id").ToString


                    .Rows(i).Cells("col_Reference").Value = objDT.Rows(i).Item("str1").ToString
                    .Rows(i).Cells("col_Reference2").Value = objDT.Rows(i).Item("str2").ToString
                    .Rows(i).Cells("col_ItemDefinition").Tag = Trim(objDT.Rows(i).Item("ItemDefinition_Index").ToString)
                    .Rows(i).Cells("col_ItemDefinition").Value = objDT.Rows(i).Item("ItemDefinition_Name_th").ToString

                    'include from master site

                    .Rows(i).Cells("col_Str6").Value = objDT.Rows(i).Item("str6").ToString
                    .Rows(i).Cells("col_Tax1").Value = objDT.Rows(i).Item("Tax1").ToString
                    .Rows(i).Cells("col_Tax2").Value = objDT.Rows(i).Item("Tax2").ToString
                    .Rows(i).Cells("col_Tax3").Value = objDT.Rows(i).Item("Tax3").ToString
                    .Rows(i).Cells("col_Tax4").Value = objDT.Rows(i).Item("Tax4").ToString
                    .Rows(i).Cells("col_Tax5").Value = objDT.Rows(i).Item("Tax5").ToString


                    .Rows(i).Cells("col_HS_Code").Value = objDT.Rows(i).Item("HS_Code").ToString
                    .Rows(i).Cells("col_ItemDescription").Value = objDT.Rows(i).Item("ItemDescription").ToString

                    'add new 11-01-2010 Consignee_Index
                    .Rows(i).Cells("Col_Consignee").Tag = objDT.Rows(i).Item("Customer_Shipping_Index").ToString
                    .Rows(i).Cells("Col_Consignee").Value = objDT.Rows(i).Item("Company_Name").ToString

                    .Rows(i).Cells("col_Plan_Process").Value = objDT.Rows(i).Item("Plan_Process").ToString
                    .Rows(i).Cells("col_DocumentPlan_Index").Value = objDT.Rows(i).Item("DocumentPlan_Index").ToString
                    .Rows(i).Cells("col_DocumentPlan_No").Value = objDT.Rows(i).Item("DocumentPlan_No").ToString
                    .Rows(i).Cells("col_DocumentPlanItem_Index").Value = objDT.Rows(i).Item("DocumentPlanItem_Index").ToString


                    If objDT.Rows(i).Item("Seq").ToString = "" Then
                        .Rows(i).Cells("col_Seq").Value = i + 1
                    Else
                        .Rows(i).Cells("col_Seq").Value = objDT.Rows(i).Item("Seq")
                        '.Rows(i).Cells("col_Seq").Value = objDT.Rows(i).Item("Item_Seq").ToString
                    End If

                    If Me._USE_PUTAWAY_BTNCONFIRM Then
                        'temp Location
                        If objDT.Rows(i).Item("Str4").ToString <> "" Then
                            .Rows(i).Cells("Col_location").Value = objDT.Rows(i).Item("Str4").ToString
                        End If
                        Col_location.ReadOnly = True
                    End If

                    .Rows(i).Cells("col_Group_No").Value = objDT.Rows(i).Item("str9").ToString


                    ' *** set Format date ***
                    .Rows(i).Cells("col_Mfg_Date").Value = Format(CDate(objDT.Rows(i).Item("Mfg_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("col_Exp_Date").Value = Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("chk_Mfg_Date").Value = objDT.Rows(i).Item("IsMfg_Date").ToString
                    .Rows(i).Cells("chkExp_Date").Value = objDT.Rows(i).Item("IsExp_Date").ToString

                    .Rows(i).Cells("Col_ERP_Location").Value = objDT.Rows(i).Item("ERP_Location").ToString

                End With

            Next
            DataGridClick = True
            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getPalletDetail(ByVal Order_Index As String)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Try
            Dim objClassDB As New tb_PalletType_History
            Dim objDT As DataTable = New DataTable

            Try
                objClassDB.getpalletDetail(Order_Index)
                objDT = objClassDB.DataTable
                Me.grdPallet.DataSource = Nothing
                Me.grdPallet.Rows.Clear()

                For i As Integer = 0 To objDT.Rows.Count - 1
                    With Me.grdPallet
                        Me.grdPallet.Rows.Add()
                        .Rows(i).Cells("col_PalletType_History_Index").Value = objDT.Rows(i).Item("PalletType_History_Index").ToString
                        .Rows(i).Cells("col_Palletindex").Value = objDT.Rows(i).Item("PalletType_Index").ToString
                        .Rows(i).Cells("col_PalletType").Value = objDT.Rows(i).Item("Pallet_Name").ToString
                        .Rows(i).Cells("col_Palletqty").Value = objDT.Rows(i).Item("PalletQTY").ToString - objDT.Rows(i).Item("Qty_In").ToString 'objDT.Rows(i).Item("PalletQTY").ToString
                        '   .Rows(i).Cells("col_UsePallet").Value = objDT.Rows(i).Item("Qty_In").ToString
                        If objDT.Rows(i).Item("Qty_In").ToString = "" Then
                            .Rows(i).Cells("col_UsePallet").Value = "0"
                        Else
                            .Rows(i).Cells("col_UsePallet").Value = objDT.Rows(i).Item("Qty_In").ToString
                        End If


                    End With
                Next
            Catch ex As Exception
                Throw ex
            Finally
                objClassDB = Nothing
                objDT = Nothing
            End Try

        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

#End Region

#Region "Function For Control Combobox in Datagridview"
    Private Sub Select_Package(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("col_SKU_Index").Value Is Nothing Then
            Exit Sub
        End If
        If CType(sender, ComboBox).SelectedValue IsNot Nothing Then
            '  Dim s As String = CType(sender, ComboBox).SelectedValue
            Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("cbo_Receive_Package").Tag = CType(sender, ComboBox).SelectedValue
        End If
    End Sub
    Private Sub Select_PalletType(ByVal sender As Object, ByVal e As System.EventArgs)


        If Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("col_SKU_Index").Value Is Nothing Then
            Exit Sub
        End If
        If CType(sender, ComboBox).SelectedValue IsNot Nothing Then
            Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("cbo_PalletType").Tag = CType(sender, ComboBox).SelectedValue
        End If

    End Sub
    Private Sub Select_ItemStatus(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("col_SKU_Index").Value Is Nothing Then
            Exit Sub
        End If

        If CType(sender, ComboBox).SelectedValue IsNot Nothing Then
            Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("cbo_ItemStatus").Tag = CType(sender, ComboBox).SelectedValue
        End If
    End Sub
    Private Sub Select_ItemDefinition(ByVal sender As Object, ByVal e As System.EventArgs)
        If Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("col_SKU_Index").Value Is Nothing Then
            Exit Sub
        End If

        If CType(sender, ComboBox).SelectedValue IsNot Nothing Then
            Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("col_ItemDefinition").Tag = CType(sender, ComboBox).SelectedValue
        End If
    End Sub
    Private Sub Select_Exp_Date(ByVal sender As Object, ByVal e As System.EventArgs)
        'If Me.grdList.Rows(Me.grdList.CurrentCellAddress.Y).Cells("chkExp_Date").Value Is Nothing Then
        '    Exit Sub
        'End If
        Dim i As String = Me.grdOrderItem.CurrentRow.Cells("chkExp_Date").Value
        MessageBox.Show(i)
        If CType(sender, CheckBox).Checked = True Then
            '   Me.grdList.Rows(Me.grdList.CurrentCellAddress.Y).Cells("ItemStatus_Index").Tag = CType(sender, ComboBox).SelectedValue
            Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("chkExp_Date").Value = "True"
        Else
            Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentCellAddress.Y).Cells("chkExp_Date").Value = "False"
        End If
    End Sub
#End Region

    Private Sub frmDeposit_WMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ' ------ Set Language Begin ------
            Dim objLang As New W_Language
            objLang.SwitchLanguage(Me, 1)
            Me._odtValidate_grdOrderItem = objLang.SW_Language_Column(Me, Me.grdOrderItem, 1)
            Me._odtValidate_grdOrderTruckIn = objLang.SW_Language_Column(Me, Me.grdOrderTruckIn, 1)

            'set Enable
            Me.grdPallet.AutoGenerateColumns = False
            Me.grdSerial.AutoGenerateColumns = False
            Me.btnTag.Enabled = False

            ' Set Use and Default
            Me.OpenFileDialog1.Filter = "Bmp files (*.bmp)|*.bmp|" + "Gif files (*.gif)|*.gif|" + "Jpeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Png files (*.png)|*.png|" + "All graphic files (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"
            Me.SET_USESYSTEM()
            Me.SET_DEFAULTSYSTEM()
            Me.SETCUTOFF_TIME_OFFSET()
            Me.SETDEFAULT_CUSTOMER_INDEX()
            Me.DataGridClick = True

            Dim objCustomSetting As New config_CustomSetting
            If objCustomSetting.getConfig_Key_USE("USE_Enable_Control_Order") = True Then
                Me.Config_Order_Enable()
            Else
                Me.Config_Order()
            End If


            ' ****** Loading Config ******
            Dim objOrder As New tb_Order
            'Load Combobox
            Me.getDocumentType(1)
            Me.getReportName(1)
            Me.getCboHandlingType()
            Me.getProcess_Ref(1)
            Me.getPalletType()
            Me.getDocumentType_Itemstatus(0)
            Me.getCboCtnrSize()
            Me.getCboTypeTruck()

            If Me._USE_PUTAWAY_BY_TAG Then
                Me.btnTag.Visible = False ' ปิดปุ่ม TAG 2018/10/09
                Me.btnStoreIn.Visible = False
            Else
                Me.btnTag.Visible = False
                Me.btnStoreIn.Visible = True
            End If


            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    '-------------- เอกสารตั้งต้น ---------------------
                    Select Case Me.Receive_Process
                        Case Receive_Process_Enum.PO
                            If Me._DocumentPlan_Index <> "" Then
                                Me.txtPlanReceive.Text = Me._DocumentPlan_No
                                Me.GetPOItem()
                                cboPlanReceive.SelectedValue = 9
                            End If
                        Case Receive_Process_Enum.ASN
                            If Me._DocumentPlan_Index <> "" Then
                                Me.txtPlanReceive.Text = Me._DocumentPlan_No
                                Me.GetASNItem()
                                cboPlanReceive.SelectedValue = 16
                            End If
                        Case Else
                    End Select
                    '-------------- เอกสารตั้งต้น ---------------------
                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    Me._Order_Index = objDBTempIndex.getSys_Value("Order_Index")
                    objDBTempIndex = Nothing
                    Me.btnEdit.Enabled = False
                    Me.btnSerial.Enabled = False
                    'Me.btnCancel.Enabled = False
                    'Me.btnMemo.Enabled = False
                    Me.getPalletTypeToGRD()
                    'Me.getRef_DocumentType()
                    Me.grdOrderItem.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2

                Case enuOperation_Type.UPDATE
                    Me.txtOrder_No.ReadOnly = True
                    Me.getOrderHeader(_Order_Index)
                    ' **** Load Data Old Product Item ****
                    Me.getOrderItemDetail(_Order_Index)
                    Me.getOrderTruckInDetail(_Order_Index)
                    Me.grdOrderItem.EditMode = DataGridViewEditMode.EditProgrammatically
                    Me.grdOrderItem.ReadOnly = True
            End Select

            Me.setLocationControl_By_Config()
            'Add BY TOP: 
            'Add Date : 16/03/2013
            Me.Update_RefreshDataGrid()
            Call ManageBottonSerial()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub Update_RefreshDataGrid()
        'Add BY TOP: 
        'Add Date : 16/03/2013

        grdOrderImage.RefreshEdit()
        grdOrderItem.RefreshEdit()
        grdPallet.RefreshEdit()
        grdSerial.RefreshEdit()
    End Sub

    Private Sub setSortColumn()
        Try
            Dim column As New DataGridViewColumn
            'Case Withdraw_Type.AUTO
            For Each column In grdOrderItem.Columns
                column.SortMode = DataGridViewColumnSortMode.Programmatic
            Next

            grdOrderItem.Columns("col_Seq").ValueType = GetType(System.Int32)

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

    Sub SET_USESYSTEM()
        Try
            Dim objCustomSetting As New config_CustomSetting

            Me._USE_PUTAWAY_BY_TAG = objCustomSetting.getConfig_Key_USE("USE_PUTAWAY_BY_TAG")
            Me._USE_PUTAWAY_BTNCONFIRM = objCustomSetting.getConfig_Key_USE("USE_PUTAWAY_BTNCONFIRM")

            Me._USE_Gen_SystemLOT = objCustomSetting.getConfig_Key_USE("USE_Gen_SystemLOT")
            Me._USE_PACKING_NEW_PRODUCTION = objCustomSetting.getConfig_Key_USE("USE_PACKING_NEW_PRODUCTION")
            Me._USE_RECEIVED_QTY = objCustomSetting.getConfig_Key_USE("USE_RECEIVED_QTY")
            Me._USE_BLOCK_LOT = objCustomSetting.getConfig_Key_USE("USE_BLOCK_LOT")
            Me._USE_BRANCH_ID = objCustomSetting.getConfig_Key_USE("USE_BRANCH_ID")
            Me.USE_REPORTPRINTOUT_BOND = objCustomSetting.getConfig_Key_USE("USE_REPORTPRINTOUT_BOND")

            If objCustomSetting.getConfig_Key_USE("USE_SERIAL") Then
                btn_Serial_No.Visible = True
            Else
                btn_Serial_No.Visible = False
                Me.tbcOrder.TabPages.Remove(Me.tbpAsset)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SET_DEFAULTSYSTEM()
        Try
            SetDEFAULT_QTY()
            SetImage_Path()
            SetAUTO_GENTAG()
            SetAUTO_ORDER_REF1_TO()
            SetAUTO_ORDER_REF2_TO()


        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub SetAUTO_ORDER_REF1_TO()
        Dim objCustomSetting As New config_CustomSetting

        If objCustomSetting.getConfig_Key_USE("USE_AUTO_ORDER_REF1") = True Then
            _Auto_Fill_Ref1_To = objCustomSetting.GetValue_Config("AUTO_ORDER_REF1_TO")
        End If
    End Sub

    Sub SetAUTO_ORDER_REF2_TO()
        Dim objCustomSetting As New config_CustomSetting
        If objCustomSetting.getConfig_Key_USE("USE_AUTO_ORDER_REF2") = True Then
            _Auto_Fill_Ref2_To = objCustomSetting.GetValue_Config("AUTO_ORDER_REF2_TO")
        End If

    End Sub

    Sub ViewOnly()
        Try
            btnPrintReport.Enabled = True
            grbPrintReport.Enabled = True
            cboPrint.Enabled = True
            btnSave.Enabled = False
            btnDelete.Enabled = False
            btnCopy.Enabled = False
            Me.btnPalletSlip.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub SETCUTOFF_TIME_OFFSET()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Dim dblOffsetMinute As Double = 0

            Dim strOffsetMinute As String = objCustomSetting.GetValue_Config("CUTOFF_TIME_OFFSET", "")
            If strOffsetMinute <> "" Then
                dblOffsetMinute = strOffsetMinute
                dtOrder.Value = Now.AddMinutes(dblOffsetMinute * -1)
            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub

    Sub SetAUTO_GENTAG()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("AUTO_GENTAG", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._DEFAULT_AUTO_TAG = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me._DEFAULT_AUTO_TAG = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub SetDEFAULT_QTY()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_QTY")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.DEFAULT_QTY = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DEFAULT_QTY = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub SETDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Dim objSkuConfigDB As New config_CustomSetting
            _USE_PRODUCT_CUSTOMER = objSkuConfigDB.getConfig_Key_USE("USE_PRODUCT_CUSTOMER")

            'Me._Customer_Index = objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_CUSTOMER_INDEX")
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            Me._Customer_Index = oUser.GetUserByCustomerDefault()

            If Me._Customer_Index <> "" Then Me.getCustomer()

        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

    'Set Default Item Status
    Sub SetDEFAULT_ITEMSTATUS()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_ITEMSTATUS")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.DEFAULT_ITEMSTATUS = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DEFAULT_ITEMSTATUS = 1
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Private Sub GetAsset(ByVal pOrder_Index As String)
        'Dim objAsset As New tb_AssetLocationBalance
        'Try

        '    objAsset.GetAsset(" AND Order_Index = '" & pOrder_Index & "'")
        '    Dim odtAsset As DataTable = objAsset.GetDataTable
        '    With grdSerial
        '        .DataSource = odtAsset
        '    End With
        'Catch ex As Exception
        '    Throw ex
        'End Try

    End Sub

    Sub SetImage_Path()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Dim appSet As New Configuration.AppSettingsReader()
        Try

            'Dim strUseLocalPath As String = AppSettings("UseLocalPath").ToString
            Dim strUseLocalPath As String = appSet.GetValue("UseLocalPath", GetType(String))
            If strUseLocalPath = 0 Then

                ' Me._DEFAULT_ImagePath = AppSettings("IMAGE_PATH_ORDER").ToString
                Me._DEFAULT_ImagePath = appSet.GetValue("IMAGE_PATH_ORDER", GetType(String))

            Else
                objCustomSetting.GetConfig_Value("DEFAULT_IMAGE_PATH_ORDER", "")
                objDT = objCustomSetting.DataTable
                If objDT.Rows.Count > 0 Then
                    Me._DEFAULT_ImagePath = objDT.Rows(0).Item("Config_Value").ToString
                Else
                    Me._DEFAULT_ImagePath = ""
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
    '''' <summary>
    '''' 
    '''' </summary>
    '''' <remarks>
    '''' Update Date : 19/01/2010
    '''' Update By   : Dong_kk
    '''' Update For  : Return 1 or 0
    '''' </remarks>
    'Sub SetUSE_Gen_SystemLOT()
    '    Dim objCustomSetting As New config_CustomSetting
    '    Dim objDT As DataTable = New DataTable

    '    Try

    '        If objCustomSetting.getConfig_Key_USE("USE_Gen_SystemLOT") Then
    '            Me._USE_Gen_SystemLOT = 1
    '        Else
    '            Me._USE_Gen_SystemLOT = 0
    '        End If

    '        '###################################
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDT = Nothing
    '        objCustomSetting = Nothing
    '    End Try
    'End Sub


    Sub SetDate_Time()
        Dim dtDateTime_di As DateTime = Date.Now
        If Date.Now.Hour >= 0 And Date.Now.Hour <= 5 Then
            dtDateTime_di = DateAdd(DateInterval.Day, -1, dtDateTime_di)
        End If
        dtOrder.Value = dtDateTime_di
    End Sub

    Private Sub getCboHandlingType()
        Dim a As Integer = 0
        Dim objDBType1 As New tb_HandlingType(tb_HandlingType.enuOperation_Type.SEARCH)
        Dim objDTType1 As DataTable = New DataTable

        Try
            '--- Unload
            objDBType1.GetAllAsDataTable()
            objDTType1 = objDBType1.DataTable
            cboType.BeginUpdate()
            With cboType
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTType1
            End With
            cboType.EndUpdate()

        Catch ex As Exception
            Throw ex
        Finally
            objDBType1 = Nothing
            objDTType1 = Nothing

        End Try

    End Sub
    Function Setting_PoBySku(ByVal sku_index As String, ByVal grdRow As Integer, ByVal PO_No As String) As Integer
        Try
            Dim frmPo As New WMS_STD_INB_Receive.frmPo_popup(sku_index, PO_No)
            frmPo.ShowDialog()

            grdOrderItem.Rows(grdRow).Cells("col_PO_Number").Value = frmPo.strPo_No
            grdOrderItem.Rows(grdRow).Cells("col_Plan_Qty").Value = frmPo.dbTotalQty
            ' grdOrderItem.Rows(grdRow).Cells("col_Plan_Package").Value = frmPo.dbTotalQty
            ' grdOrderItem.Rows(grdRow).Cells("col_Plan_Qty").Value = frmPo.dbTotalQty
            grdOrderItem.Rows(grdRow).Cells("col_Qty").Value = frmPo.dbTotalQty 'frmPo.dblQty
            grdOrderItem.Rows(grdRow).Cells("col_Weight").Value = frmPo.dblWeight
            grdOrderItem.Rows(grdRow).Cells("col_Volume").Value = frmPo.dblVolume
            grdOrderItem.Rows(grdRow).Cells("ColumnPO_Item").Value = frmPo.strPo_Index

            'Col_Location
            frmPo.Close()

            Return 1
        Catch ex As Exception
            Return -10
        End Try
    End Function
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Date : 12/01/2010
    ''' Update By : ja Update 
    ''' update Consignee_Popup   , add new  Function btnSplit_Qty
    ''' </remarks>
    ''' 
    Private Sub grdList_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderItem.CellClick
        '  Dim objAsset As New tb_AssetLocationBalance
        If e.RowIndex <= -1 Then
            Exit Sub
        End If
        If e.ColumnIndex <= -1 Then
            Exit Sub
        End If

        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "btn_PO_Popup"
                    Dim strSku_index As String = ""
                    If grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value Is Nothing Then Exit Select
                    strSku_index = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value

                    Dim strPO_NO As String = grdOrderItem.Rows(e.RowIndex).Cells("col_PO_Number").Value



                    Setting_PoBySku(strSku_index, e.RowIndex, strPO_NO)

                Case "btn_ItemDefinition"
                    ' *********************************************************************
                    '   grdOrderItem.Rows(e.RowIndex).Cells("ItemDefinition_Index_Hiden").Value = ""
                    ' *********************************************************************
                    If grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value Is Nothing Then
                        MessageBox.Show(" กรุณาระบุ SKU!!!   ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    Dim frmPopup As New frmDefinition_Popup
                    frmPopup.Proces_id = "'1','4'"

                    frmPopup.ShowDialog()
                    Me.grdOrderItem.Rows(e.RowIndex).Cells("col_ItemDefinition").Tag = frmPopup.ItemDefinition_Index
                    Me.grdOrderItem.Rows(e.RowIndex).Cells("col_ItemDefinition").Value = frmPopup.ItemDefinition_Description
                    frmPopup.Close()

                    '12-01-2010 by ja Consignee_Popup
                Case "btn_Consignee_Popup"
                    If grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value Is Nothing Then
                        MessageBox.Show(" กรุณาระบุ SKU!!!   ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Exit Sub
                    End If

                    Dim frmConsignee_Popup As New frmConsignee_Popup
                    frmConsignee_Popup.Customer_Index = Me._Customer_Index
                    frmConsignee_Popup.ShowDialog()
                    Me.grdOrderItem.Rows(e.RowIndex).Cells("Col_Consignee").Tag = frmConsignee_Popup.Consignee_Index
                    Me.grdOrderItem.Rows(e.RowIndex).Cells("Col_Consignee").Value = frmConsignee_Popup.Consignee_Name

                    frmConsignee_Popup.Close()

                Case "btn_SKU_Popup"

                    If Me._USE_PRODUCT_CUSTOMER AndAlso String.IsNullOrEmpty(Me._Customer_Index) Then
                        W_MSG_Information(String.Format("กรุณาระบุ {0} !!", Me.lblCustomer.Text.Trim))
                        Exit Select
                    End If

                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("btn_SKU_Popup").ReadOnly = True Then
                        Exit Sub
                    End If

                    If txtCustomer_Id.Text = "" Then
                        W_MSG_Information_ByIndex(8)
                        Exit Sub
                    End If

                    If grdOrderItem.Rows(e.RowIndex).Cells("btn_SKU_Popup").Tag <> "NULL" Then
                        If grdOrderItem.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value <> Nothing Then
                            If grdOrderItem.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value.ToString.Trim <> "" Then Exit Sub
                        End If
                        Dim frmPopup As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me._Customer_Index)

                        frmPopup.Customer_Index = Me._Customer_Index
                        frmPopup.ShowDialog()
                        '   frmPopup.Close()
                        If frmPopup.Sku_ID = "" Then
                            Exit Sub
                        End If
                        ' เลือก SKU 
                        If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value IsNot Nothing Then
                            'กรณีมีการเปลี่ยน SKU
                            If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value <> frmPopup.Sku_Index Then

                                Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
                                'จะเปลี่ยนได้เฉพาะกรณีที่ยังไม่จัดเก็บเท่านั้น
                                If Not Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value = "" Then
                                    'กรณีจัดเก็บแล้ว
                                    If objDB.isItemLocation(Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value) = True Then
                                        MessageBox.Show("ไม่สามารถเปลี่ยนข้อมูล SKU ได้ เนื่องจากสินค้ามีการจัดเก็บแล้วกรุณาตรวจสอบสินค้าในคลัง", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                        objDB = Nothing
                                        Exit Sub
                                    End If
                                End If
                            End If
                            Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value = frmPopup.Sku_ID
                        Else
                            ' Dim iRow As Integer
                            ' iRow =  grdOrderItem.Rows.Add()
                            Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value = frmPopup.Sku_ID
                        End If
                    End If


                Case "btn_Serial_No"
                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value Is Nothing Then
                        W_MSG_Information("กรุณาเลือกข้อมูล SKU")
                        Exit Sub
                    End If

                    If grdOrderItem.Rows(e.RowIndex).Cells("btn_Serial_No").Tag <> "NULL" Then
                        Dim frmPopup As New WMS_STD_INB_Receive.frmSerialNo_PopUp
                        frmPopup.intKey = e.RowIndex
                        frmPopup.Order_Index = _Order_Index
                        frmPopup.OrderItem_Index = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_OrderItem_Index").Value
                        frmPopup.SKU_Id = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value
                        frmPopup.SKU_Index = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                        frmPopup.SKU_Name = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Description").Value
                        frmPopup.SKU_Package = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Package").Value
                        frmPopup.ShowDialog()
                        Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = frmPopup.intQty
                        Me.grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value = frmPopup.intQty
                        If grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value > 0 Then
                            Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").ReadOnly = True
                            Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Style.BackColor = Color.Gainsboro
                            Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Style.BackColor = Color.Pink '  Me.grdOrderItem.Rows(e.RowIndex).Cells("btn_Serial_No").Style.BackColor = Color.Pink

                        End If
                        Me.GetAsset(_Order_Index)
                        frmPopup.Close()

                    End If

                Case "btnSplit"

                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("btnSplit").ReadOnly = True Then
                        Exit Sub
                    End If

                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value Is Nothing Then
                        W_MSG_Information("กรุณาเลือกข้อมูล SKU")
                        Exit Sub
                    End If

                    If grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value > 0 Then
                        Dim frmSplitLot_Popup As New WMS_STD_INB_Receive.frmSplitLot_Popup
                        If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value <= 1 Then
                            W_MSG_Information("จำนวนสินค้าไม่สามารถแยก Lot ได้")
                            Exit Sub
                        End If
                        frmSplitLot_Popup.DocumentType_Index = Me.cboDocumentType.SelectedValue
                        frmSplitLot_Popup.SKU_Id = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value
                        frmSplitLot_Popup.SKU_Index = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                        frmSplitLot_Popup.SKU_Name = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Description").Value
                        frmSplitLot_Popup.SKU_Package = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Package").Value
                        frmSplitLot_Popup.intQty = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value

                        frmSplitLot_Popup.ShowDialog()
                        If frmSplitLot_Popup.isClose = 1 Then
                            If frmSplitLot_Popup.intQty > 0 Then
                                Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = frmSplitLot_Popup.intQty

                                Dim intCurrentRows As Integer = grdOrderItem.Rows.Count - 1

                                With Me.grdOrderItem
                                    Me.grdOrderItem.Rows.Add()
                                    Me.grdOrderItem.Rows(intCurrentRows).Cells("col_SKU_Id").Value = frmSplitLot_Popup.SKU_Id
                                    grdOrderItem.Rows(intCurrentRows).Cells("col_SKU_Id").Style.BackColor = Color.Violet
                                    Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Qty").Value = frmSplitLot_Popup.LOTSplit
                                    Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Plot").Value = frmSplitLot_Popup.PLOT
                                    Me.grdOrderItem.Rows(intCurrentRows).Cells("cbo_ItemStatus").Value = frmSplitLot_Popup.strItemStatus
                                    Me.grdOrderItem.Rows(intCurrentRows).Cells("cbo_Receive_Package").Value = Me.grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value
                                    Calculate_Pck(intCurrentRows)
                                    Calculate_Pck()
                                    Me.grdOrderItem.Update()
                                End With

                            End If
                        End If
                        ' frmSplitLot_Popup.Close()
                    Else
                        W_MSG_Information("กรุณากรอกจำนวนสินค้า")
                        Exit Sub
                    End If


                    '19-01-2010 ja update Function Split_Qty
                Case "btnSplit_Qty"

                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("btnSplit_Qty").ReadOnly = True Then
                        Exit Sub
                    End If

                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value Is Nothing Then
                        W_MSG_Information("กรุณาเลือกข้อมูล SKU")
                        Exit Sub
                    End If

                    If grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value <> 0 Then
                        Dim frmSplitQty_Popup As New WMS_STD_INB_Receive.frmSplitQty_Popup
                        frmSplitQty_Popup.SKU_Id = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Id").Value
                        frmSplitQty_Popup.SKU_Index = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                        frmSplitQty_Popup.SKU_Name = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Description").Value
                        frmSplitQty_Popup.SKU_Package = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Package").Value
                        frmSplitQty_Popup.intQty = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value

                        frmSplitQty_Popup.strPlot = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Plot").Value
                        frmSplitQty_Popup.intWeight = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Weight").Value
                        frmSplitQty_Popup.intVolume = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Volume").Value

                        frmSplitQty_Popup.ShowDialog()

                        'grdOrderItem.Rows.Insert(e.RowIndex, "", "", "")
                        If frmSplitQty_Popup.intSplit_Qty <> 0 Then

                            Me.RowCopyGrd(grdOrderItem, e.RowIndex)
                            'Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = frmSplitQty_Popup.intQty

                            Dim intCurrentRows As Integer = e.RowIndex + 1

                            With Me.grdOrderItem

                                'grdOrderItem.Rows.Insert(e.RowIndex + 1, e.RowIndex, frmSplitQty_Popup.SKU_Id.ToString, "", "", "", "", "", "", frmSplitQty_Popup.SKU_Name.ToString, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", frmSplitQty_Popup.intSplit_Qty.ToString, "", "", "")

                                ' getSKU_Detail(frmSplitQty_Popup.SKU_Id.ToString, e.RowIndex + 1)

                                grdOrderItem.Rows(intCurrentRows).Cells("col_Seq").Value = grdOrderItem.Rows(e.RowIndex).Cells("col_Seq").Value
                                Me.grdOrderItem.Rows(intCurrentRows).Cells("col_SKU_Id").Value = frmSplitQty_Popup.SKU_Id
                                grdOrderItem.Rows(intCurrentRows).Cells("col_SKU_Id").Style.BackColor = Color.Violet
                                Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Qty").Value = frmSplitQty_Popup.intSplit_Qty
                                Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Weight").Value = frmSplitQty_Popup.intSplit_Weight
                                Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Volume").Value = frmSplitQty_Popup.intSplit_Volume
                                Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Plot").Value = frmSplitQty_Popup.strPlot
                                Me.grdOrderItem.Rows(intCurrentRows).Cells("cbo_ItemStatus").Value = frmSplitQty_Popup.strItemStatus

                                Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = frmSplitQty_Popup.intQty
                                Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Weight").Value = frmSplitQty_Popup.intWeight
                                Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Volume").Value = frmSplitQty_Popup.intVolume

                            End With

                        End If
                    Else
                        W_MSG_Information("กรุณากรอกจำนวนสินค้า")
                        Exit Sub
                    End If


                Case "col_SkuItem"
                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value = "" Then Exit Select
                    Dim sqlDB As New SQLCommands
                    sqlDB.SQLComand("Select count(*) as Count from ms_sku where sku_index = '" & Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value & "' and str10='2'")
                    If sqlDB.DataTable.Rows(0).Item("Count").ToString = 0 Then Exit Select
                    sqlDB = Nothing

                    Dim obj_OrderItemSku As New WMS_STD_INB_Receive.frmOrderItemSku
                    obj_OrderItemSku.Order_ItemIndex = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_OrderItem_Index").Value
                    obj_OrderItemSku.OrderItem_RowIndex = e.RowIndex
                    obj_OrderItemSku.Sku_index = Me.grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Volume").Value Is Nothing Then
                        obj_OrderItemSku.Sku_Volume = 0
                    Else
                        obj_OrderItemSku.Sku_Volume = CDbl(Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Volume").Value.ToString)
                    End If
                    If Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Weight").Value Is Nothing Then
                        obj_OrderItemSku.Sku_Weight = 0
                    Else
                        obj_OrderItemSku.Sku_Weight = CDbl(Me.grdOrderItem.Rows(e.RowIndex).Cells("col_Weight").Value.ToString)
                    End If


                    If _WithdrawStatus = True Then
                        obj_OrderItemSku.btnSave.Enabled = False
                        obj_OrderItemSku.grd_OrderItemSku.ReadOnly = True
                    End If

                    obj_OrderItemSku.Show()

                Case "Col_location"
                    If (grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value Is Nothing) Or (grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value Is Nothing) Or (grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value Is Nothing) Or (grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value Is Nothing) Then
                        Exit Sub
                    End If
                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString = "" Then Exit Sub
                    Dim vPackage_Index As String = ""
                    Dim vSku_Index As String = ""
                    Dim vdblRatio As Double = 1
                    Dim vdblQty As Double = 1
                    vPackage_Index = grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value.ToString
                    vSku_Index = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value.ToString
                    _tmpLocation_Alias = grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString
                    vdblQty = CDbl(grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value.ToString)
                    ' *** Get Retio ***
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    vdblRatio = objRatio.getRatio(vSku_Index, vPackage_Index)
                    objRatio = Nothing

                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    _tmpTotalQty_PutAway = vdblQty * vdblRatio
                    _tmpQty_PutAway = vdblQty

                Case "col_Qty"
                    If _USE_RECEIVED_QTY Then

                        If grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value IsNot Nothing And grdOrderItem.Rows(e.RowIndex).Cells("col_Plan_Qty").Value IsNot Nothing Then
                            Dim strQty As String = grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value
                            Dim strPlan_Qty As String = grdOrderItem.Rows(e.RowIndex).Cells("col_Plan_Qty").Value

                            If strPlan_Qty > strQty Then
                                W_MSG_Information("กรุณากรอกสาเหตุของการรับสินค้าขาด")

                            ElseIf strQty > strPlan_Qty Then
                                W_MSG_Information("กรุณากรอกสาเหตุของการรับสินค้ามากเกิน")
                            End If
                        End If

                    End If


                    If (grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value Is Nothing) Or (grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value Is Nothing) Or (grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value Is Nothing) Or (grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value Is Nothing) Then
                        Exit Sub
                    End If

                    If (grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value.ToString = "") Or (grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value.ToString = "") Or (grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value.ToString = "") Then
                        Exit Sub
                    End If
                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString = "" Then Exit Sub

                    Dim vPackage_Index As String = ""
                    Dim vSku_Index As String = ""
                    Dim vdblRatio As Double = 1
                    Dim vdblQty As Double = 1
                    vPackage_Index = grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value.ToString
                    vSku_Index = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value.ToString

                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value IsNot Nothing Then
                        _tmpLocation_Alias = grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString
                    End If

                    vdblQty = CDbl(grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value.ToString)
                    ' *** Get Retio ***
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    vdblRatio = objRatio.getRatio(vSku_Index, vPackage_Index)
                    objRatio = Nothing

                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    _tmpTotalQty_PutAway = vdblQty * vdblRatio
                    _tmpQty_PutAway = vdblQty


            End Select




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            Call ManageBottonSerial()
        End Try

    End Sub

    Private Sub grdList_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderItem.CellValueChanged

        Try
            If e.RowIndex <= -1 Then Exit Sub
            'If DataGridClick = False Then Exit Sub
            If Counter = -9 Then Exit Sub

            Dim dgv As DataGridView = CType(sender, DataGridView)
            Dim strColumnName As String = dgv.Columns(e.ColumnIndex).Name.ToUpper

            Select Case strColumnName
                Case "COL_SKU_ID"

                    If grdOrderItem.Rows(e.RowIndex).Cells("btn_SKU_Popup").Tag <> "NULL" Then
                        If grdOrderItem.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value <> Nothing Then

                            If grdOrderItem.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value.ToString.Trim <> "" Then Exit Select
                        End If
                    End If

                    If Me._USE_PRODUCT_CUSTOMER AndAlso String.IsNullOrEmpty(Me._Customer_Index) Then
                        W_MSG_Information(String.Format("กรุณาระบุ {0} !!", Me.lblCustomer.Text.Trim))
                        grdOrderItem.Rows(e.RowIndex).Cells("COL_SKU_ID").Value = ""
                        Exit Select
                    End If

                    Dim Sku_id As String = ""
                    Sku_id = grdOrderItem.Rows(e.RowIndex).Cells("COL_SKU_ID").Value.ToString
                    ' *** Display SKU detail ***
                    getSKU_Detail(Sku_id, e.RowIndex)
                    Check_GroupNo(Sku_id, e.RowIndex)
                    Check_DummyNo(Sku_id, e.RowIndex)

                    'Case "CHKEXP_DATE"
                    '        If grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value IsNot Nothing Then
                    '            Me.CalcualateDate_In_Gridview(e.RowIndex)
                    '        End If
                Case "COL_TAX1", _
                     "COL_TAX2", _
                     "COL_TAX3", _
                     "COL_TAX4", _
                     "COL_TAX5"

                        If IsNumeric(grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value) Then
                            grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value = FormatNumber(grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value, 2)
                        Else
                            grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value = FormatNumber(0, 2)
                        End If

                        'Change Formatnumber
                Case "COL_VOLUME_PERPACKAGE", _
                     "COL_VOLUME", _
                     "COL_WEIGHT_PERPACKAGE", _
                     "COL_WEIGHT", _
                     "COL_WIDTH_PERPACKAGE", _
                     "COL_LONG_PERPACKAGE", _
                     "COL_HEIGHT_PERPACKAGE", _
                     "COL_ITEMPRICE_PERPACKAGE", _
                     "COL_ITEMPRICE", _
                     "COL_ITEMPRICE_PERPACKAGE", _
                     "COL_QTY"

                        'If IsNumeric(grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value) Then
                        '    grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value = FormatNumber(grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value, 6)
                        'Else
                        '    grdOrderItem.Rows(e.RowIndex).Cells(strColumnName).Value = FormatNumber(0, 6)
                        'End If

            End Select




            Get_SumData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Function Check_GroupNo(ByVal Sku_id As String, ByVal Grdrow As Integer) As Integer

        Try
            If Sku_id = "" Then Exit Function

            Dim sqlDB As New SQLCommands
            Dim GroupNo As Integer = 1
            Dim i As Integer
            sqlDB.SQLComand("select count(*) as Count from ms_sku where sku_id='" & Sku_id & "' and str10 = '3' ")
            If sqlDB.DataTable.Rows(0).Item("Count").ToString = "0" Then
                grdOrderItem.Rows(Grdrow).Cells("col_Group_No").Value = Nothing
                grdOrderItem.Rows(Grdrow).Cells("col_Group_No").ReadOnly = True
                grdOrderItem.Rows(Grdrow).Cells("col_Group_No").Style.BackColor = Color.WhiteSmoke
                Return 0
            End If

            grdOrderItem.Rows(Grdrow).Cells("col_Group_No").ReadOnly = False
            grdOrderItem.Rows(Grdrow).Cells("col_Group_No").Style.BackColor = Color.White

            If Grdrow = 0 Then
                grdOrderItem.Rows(Grdrow).Cells("col_Group_No").Value = GroupNo.ToString
                Return 1
            End If

            For i = 0 To grdOrderItem.Rows.Count - 1
                If i = 0 Then Continue For
                If grdOrderItem.Rows(i - 1).Cells("col_Group_No").Value Is Nothing Or grdOrderItem.Rows(i - 1).Cells("col_Group_No").Value = "" Then
                    Continue For
                End If
                GroupNo = CInt(grdOrderItem.Rows(i - 1).Cells("col_Group_No").Value.ToString())

            Next
            'GroupNo += 1

            'For i = 0 To grdOrderItem.Rows.Count - 2
            '    If Sku_id = grdOrderItem.Rows(i).Cells("col_SKU_Id").Value.ToString Then
            '        GroupNo = CInt(grdOrderItem.Rows(i).Cells("col_Group_No").Value.ToString)
            '        Exit For
            '    End If
            'Next

            grdOrderItem.Rows(Grdrow).Cells("col_Group_No").Value = GroupNo.ToString

            Return 1
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Check_DummyNo(ByVal Sku_id As String, ByVal Grdrow As Integer) As Integer
        Try
            If Sku_id = "" Then Exit Function

            Dim sqlDB As New SQLCommands
            Dim GroupNo As Integer = 1

            sqlDB.SQLComand("select count(*) as Count from ms_sku where sku_id='" & Sku_id & "' and str10 = '2' ")
            If sqlDB.DataTable.Rows(0).Item("Count").ToString = "0" Then
                Dim txtViewItem As New DataGridViewTextBoxCell
                txtViewItem.Value = " "
                grdOrderItem.Rows(Grdrow).Cells("col_SkuItem") = txtViewItem
                txtViewItem.Dispose()
                Return 0
            End If

            Dim btnViewItem As New DataGridViewButtonCell
            btnViewItem.Value = grdOrderItem.Columns("col_SkuItem").DefaultCellStyle.NullValue
            grdOrderItem.Rows(Grdrow).Cells("col_SkuItem") = btnViewItem
            btnViewItem.Dispose()


            Return 1
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try

            If _USE_PRODUCT_CUSTOMER Then
                For i As Integer = 0 To grdOrderItem.Rows.Count - 2
                    If grdOrderItem.Rows(i).Cells("col_SKU_Index").Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(grdOrderItem.Rows(i).Cells("col_SKU_Index").Value.ToString) Then
                        W_MSG_Information(String.Format("กรุณาลบรายการก่อนเปลี่ยน{0} !!", Me.lblCustomer.Text.Trim))
                        Exit Sub
                    End If
                Next
            End If



            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value ****
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            getCustomerContact(tmpCustomer_Index) 'get ค่าลงใน cbContrat

            If Not tmpCustomer_Index = "" Then
                Me._Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
            ' *********************
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdList_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdOrderItem.EditingControlShowing
        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
                Case "cbo_Receive_Package"
                    Dim cboPackage As ComboBox = CType(e.Control, ComboBox)
                    '---add an event handler to the ComboBox control---
                    AddHandler cboPackage.SelectionChangeCommitted, AddressOf Select_Package

                Case "cbo_PalletType"
                    If TypeOf e.Control Is ComboBox Then
                        Dim comboBoxColumn11 As DataGridViewComboBoxColumn = grdOrderItem.Columns("cbo_PalletType")
                        If (grdOrderItem.CurrentCellAddress.X = comboBoxColumn11.DisplayIndex) Then
                            Dim cboPalletType As ComboBox = CType(e.Control, ComboBox)
                            If cboPalletType IsNot Nothing Then
                                AddHandler cboPalletType.SelectionChangeCommitted, New EventHandler(AddressOf Me.Select_PalletType)
                            End If
                        End If
                    End If
            End Select

            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            Dim strName As String = grdOrderItem.Columns(grdOrderItem.CurrentCell.ColumnIndex).Name
            If (strName <> "cbo_Package_ItemQty") And (strName <> "cbo_Receive_Package") And (strName <> "cbo_PalletType") And (strName <> "cbo_ItemStatus") And (strName <> "cbo_HandlingType") And (strName <> "Col_Mfg_Date") And (strName <> "col_Exp_Date") Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnSeachDepartment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachDepartment.Click
        Try
            Dim frm As New frmDepartment_Popup
            frm.ShowDialog()
            '    *** Recive value ****
            Dim tmpDepartment_Index As String = ""
            tmpDepartment_Index = frm.Department_index

            If tmpDepartment_Index = "" Then
                Exit Sub
            End If

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

    Private Function Convert_CMD(ByVal value As String) As String
        Try
            Select Case value
                Case "CMD"
                    Return "XXX"
                Case Else
                    Return value
            End Select
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub btnSeachSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachSupplier.Click
        Try
            Dim frm As New frmSupplier_Popup
            frm.ShowDialog()
            '    *** Recive value **** มาแสดงในตัวแปล 
            Dim tmpSupplier_Index As String = ""
            tmpSupplier_Index = frm.Supplier_Index

            If tmpSupplier_Index = "" Then
                Exit Sub
            End If

            If Not tmpSupplier_Index = "" Then
                Me.txtSupplier_Id.Tag = tmpSupplier_Index
                Me.getSupplier()
            Else
                Me.txtSupplier_Id.Tag = ""
                Me.txtSupplier_Id.Text = ""
                Me.txtSupplier_Name.Text = ""
            End If
            ' *************

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdList_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles grdOrderItem.KeyDown
        Try
            If e.KeyCode = Keys.Delete Then
                If Me.grdOrderItem.SelectedRows.Count > 0 AndAlso Not Me.grdOrderItem.SelectedRows(0).Index = Me.grdOrderItem.Rows.Count - 1 Then

                    If Not Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value = "" Then
                        If MessageBox.Show("คุณต้องการลบรายการใช่หรือไม่ ", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.Yes Then
                            Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
                            objDB.Delete_OrderItem(Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value)
                            objDB = Nothing
                            Me.grdOrderItem.Rows.RemoveAt(Me.grdOrderItem.SelectedRows(0).Index)
                        End If

                    Else
                        Me.grdOrderItem.Rows.RemoveAt(Me.grdOrderItem.SelectedRows(0).Index)

                    End If

                End If
            End If

            If e.KeyCode = Keys.Insert Then
                RowCopyGrd(grdOrderItem)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    '''  <remarks>
    ''' Date : 12/01/2010
    ''' Update By : ja Update 
    ''' update save Consignee_Index to  tb_OrderItem 
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  :  Me.txtOrder_No.Text = objHeader.Order_No เมื่อ Save แล้ว เพราะไม่งั้น tb_tag จะไม่มี Order_No
    ''' -------------------------------------------------
    ''' </remarks>
    ''' 
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click



        ' **************************
        ' *** Loop to Check Unique Item Line In datagrid **
        ' *** option for check unique product ***
        ' *************************************************

        Dim objHeader As New tb_Order
        Dim objItem As New tb_OrderItem
        Dim objItemCollection As New List(Of tb_OrderItem)
        Dim objms_ItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim ItemLife_Total_Day As Integer = 0

        ' PalletType_History
        Dim objPalletType As New tb_PalletType_History
        Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

        Try


            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If


            '************* Begin Comment When Use Function Validate ***************** 
            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderItem, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdPallet, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdSerial, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderImage, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderTruckIn, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""






            '*** Check Important Data ***
            If Me._Customer_Index = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If
            '************* End Comment When Use Function Validate ***************** 

            ' *** Check Product Item ***
            If Me.grdOrderItem.Rows.Count = 1 Then
                W_MSG_Information("กรุณากรอกรายการสินค้า")
                Exit Sub
            End If

            ' *** BEGIN Checking Putaway all item
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim intCountLocation As Integer = 0
                For i As Integer = 0 To grdOrderItem.Rows.Count - 2

                    '************* Begin Comment When Use Function Validate ***************** 
                    'Checking SKU 
                    If grdOrderItem.Rows(i).Cells("col_SKU_Index").Value Is Nothing Then
                        W_MSG_Information("กรุณากรอกรายการสินค้าให้ครบ")
                        Exit Sub
                    End If

                    If grdOrderItem.Rows(i).Cells("col_SKU_Index").Value.ToString.Trim = "" Then
                        W_MSG_Information("กรุณากรอกรายการสินค้าให้ครบ")
                        Exit Sub
                    End If
                    '************* End Comment When Use Function Validate ***************** 

                    Me._LocationID = grdOrderItem.Rows(i).Cells("Col_location").Value
                    If (Me._LocationID IsNot Nothing) And (Not _LocationID = "") Then
                        intCountLocation += 1

                        'Check Location
                        Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                        Dim odtLocation As New DataTable
                        oms_Location.SearchData_Click("*", " AND Location_Alias = '" & Me._LocationID.Replace("'", "''").Trim & "'")
                        odtLocation = oms_Location.GetDataTable

                        If odtLocation.Rows.Count > 0 Then
                            Dim dblQty, dblWeight, dblVolume As Double

                            dblQty = odtLocation.Rows(0)("Current_Qty").ToString
                            dblWeight = odtLocation.Rows(0)("Current_Weight").ToString
                            dblVolume = odtLocation.Rows(0)("Current_Volume").ToString

                            If dblQty + CDbl(grdOrderItem.Rows(i).Cells("col_Qty").Value) > odtLocation.Rows(0)("Max_Qty") Then
                                W_MSG_Information("จำนวนในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                                Exit Sub
                            ElseIf dblQty + CDbl(grdOrderItem.Rows(i).Cells("col_Net_Weight").Value) > odtLocation.Rows(0)("Max_Weight") Then
                                W_MSG_Information("น้ำหนักในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                                Exit Sub
                            ElseIf dblQty + CDbl(grdOrderItem.Rows(i).Cells("col_Volume").Value) > odtLocation.Rows(0)("Max_Volume") Then
                                W_MSG_Information("ปริมาตรในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                                Exit Sub

                            End If
                        Else
                            W_MSG_Information("ไม่พบตำแหน่งจัดเก็บ " & Me._LocationID & " !" & vbNewLine & "กรุณาป้อนตำแหน่งใหม่")
                        End If
                    End If
                Next
                If intCountLocation > 0 Then
                    If intCountLocation <> grdOrderItem.Rows.Count - 1 Then
                        W_MSG_Information("กรุณาป้อนตำแหน่งจัดเก็บให้ครบทุกรายการ")
                        Exit Sub
                    End If
                End If

            End If


            objHeader.Order_Index = Me._Order_Index
            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_No = Me.txtOrder_No.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            If txtOrder_No.Text = "" Then

                If _USE_BRANCH_ID Then
                    Dim strWhere As String = " Branch_ID ='" & WV_Branch_ID & "'"
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing

                Else
                    Dim strWhere As String = ""
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing

                End If

            End If

            objHeader.Lot_No = Me.txtLot_No.Text

            ' xxxxxxx Using TAG Properties xxxxxxxxxxx
            objHeader.Customer_Index = Me._Customer_Index.ToString
            If Me.txtSupplier_Id.Tag = Nothing Then
                objHeader.Supplier_Index = ""
            Else
                objHeader.Supplier_Index = Me.txtSupplier_Id.Tag.ToString
            End If

            If Me.txtDepartment_Id.Tag = Nothing Then
                objHeader.Department_Index = ""
            Else
                objHeader.Department_Index = Me.txtDepartment_Id.Tag.ToString
            End If
            If Me.txtConsignee_ID.Tag = Nothing Then
                objHeader.Consignee_Index = ""
            Else
                objHeader.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
            End If
            ' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_Time = Me.txtTime.Text
            objHeader.Ref_No1 = Me.txtRef_No1.Text
            objHeader.Ref_No2 = Me.txtRef_No2.Text
            objHeader.Ref_No3 = Me.txtRef_No3.Text
            objHeader.Ref_No4 = Me.txtRef_No4.Text
            objHeader.Ref_No5 = Me.txtRef_No5.Text
            objHeader.Str1 = Me.txtStr1.Text
            'objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str3 = Me.txtStr3.Text
            objHeader.Str4 = Me.txtStr4.Text
            objHeader.Str5 = Me.txtStr5.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            objHeader.Comment = Me.txtComment.Text
            objHeader.Str8 = Me.cboContact_Name.Text
            objHeader.PO_No = Me.txtPlanReceive.Text
            objHeader.Invoice_No = Me.txtInvoice_No.Text
            objHeader.ASN_No = Me.txtASN_No.Text
            'objHeader.Receive_Type = Me.chkReceiveType.Checked

            If cboType.SelectedValue IsNot Nothing Then
                objHeader.HandlingType_Index = cboType.SelectedValue
            Else
                objHeader.HandlingType_Index = ""
            End If

            'End If
            'killz 02-06-2011 objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str2 = IIf(Me.txtStr2.SelectedValue Is Nothing, "", Me.txtStr2.SelectedValue)
            'killz 02-06-2011 เก็บประเภทรถ ไว้คิดตัง
            objHeader.Str9 = IIf(Me.txtStr9.SelectedValue Is Nothing, "", Me.txtStr9.SelectedValue)
            ' *************************************************tap 2

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


            If txtQtyPck.Text = "" Then
                objHeader.Flo1 = 0
            Else
                objHeader.Flo1 = txtQtyPck.Text

            End If
            For i As Integer = 0 To grdOrderItem.Rows.Count - 2

                With grdOrderItem

                    ' *** New Object *********
                    objItem = New tb_OrderItem


                    If .Rows(i).Cells("col_OrderItem_Index").Value = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        .Rows(i).Cells("col_OrderItem_Index").Value = objDBIndex.getSys_Value("OrderItem_Index")
                        objItem.OrderItem_Index = .Rows(i).Cells("col_OrderItem_Index").Value
                        Me.OrderItem_Id = objItem.OrderItem_Index
                        objDBIndex = Nothing
                    Else
                        objItem.OrderItem_Index = .Rows(i).Cells("col_OrderItem_Index").Value.ToString
                    End If
                    objItem.Order_Index = objHeader.Order_Index

                    'Plan_Qty
                    If .Rows(i).Cells("col_Plan_Qty").Value IsNot Nothing Then
                        objItem.Plan_Qty = CDbl(.Rows(i).Cells("col_Plan_Qty").Value.ToString)
                    End If

                    'Sku_Index
                    If .Rows(i).Cells("col_SKU_Index").Value IsNot Nothing Then
                        objItem.Sku_Index = .Rows(i).Cells("col_SKU_Index").Value.ToString
                    End If

                    '12-01-2010 By ja saveConsignee_Index ตามฟิวใหม่ที่แอดลงใน tb_orderItem 
                    If .Rows(i).Cells("Col_Consignee").Tag IsNot Nothing Then
                        objItem.Consignee_Index = .Rows(i).Cells("Col_Consignee").Tag.ToString
                    End If

                    'comment 
                    If .Rows(i).Cells("col_Coment").Value IsNot Nothing Then
                        objItem.Comment = .Rows(i).Cells("col_Coment").Value.ToString
                    End If

                    'Qty
                    objItem.Qty = .Rows(i).Cells("col_Qty").Value
                    If objItem.Qty = 0 Then
                        MessageBox.Show("กรุณากรอกจำนวนสินค้า", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        Me.grdOrderItem.Rows(i).Selected = True
                        Exit Sub
                    End If

                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.UPDATE)

                    If objDB.isItemTAG(.Rows(i).Cells("col_OrderItem_Index").Value) Then
                        Dim QtyInTAG As Decimal = objDB.GetQtyTagInTAG(.Rows(i).Cells("col_OrderItem_Index").Value)
                        If QtyInTAG > 0 And objItem.Qty < QtyInTAG Then
                            MessageBox.Show(String.Format("ไม่สามารถระบุจำนวนน้อยกว่าจำนวนที่สร้าง TAG ไปแล้วได้  สร้าง TAG ไปแล้ว : {0} ", QtyInTAG), "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.grdOrderItem.Rows(i).Selected = True
                            Exit Sub
                        End If
                    End If


                    If .Rows(i).Cells("cbo_Receive_Package").Value IsNot Nothing Then
                        ' ** .Tag
                        objItem.Package_Index = .Rows(i).Cells("cbo_Receive_Package").Value
                    End If
                    ' *** Get Retio ***
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
                    objRatio = Nothing

                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    objItem.Total_Qty = objItem.Qty * objItem.Ratio
                    ' ***************************

                    'Weight
                    If .Rows(i).Cells("col_Weight").Value IsNot Nothing Then
                        objItem.Weight = CDbl(.Rows(i).Cells("col_Weight").Value)
                    End If

                    'col_Net_Weight
                    If .Rows(i).Cells("col_Net_Weight").Value IsNot Nothing Then
                        objItem.Flo1 = CDbl(.Rows(i).Cells("col_Net_Weight").Value)
                    End If

                    'Volume
                    If .Rows(i).Cells("col_Volume").Value IsNot Nothing Then
                        objItem.Volume = CDbl(.Rows(i).Cells("col_Volume").Value)
                    End If

                    'PalletType_Index   
                    If .Rows(i).Cells("col_Pallet_No").Tag IsNot Nothing Then
                        objItem.PalletType_Index = .Rows(i).Cells("col_Pallet_No").Tag.ToString
                    End If

                    'Pallet_Qty
                    If .Rows(i).Cells("col_Pallet_Qty").Value IsNot Nothing Then
                        objItem.Pallet_Qty = CDbl(.Rows(i).Cells("col_Pallet_Qty").Value)
                    End If

                    If grdOrderItem.Rows(i).Cells("Col_location").Value Is Nothing Then

                    Else

                        Me._LocationID = .Rows(i).Cells("Col_location").Value
                        'Temp Location
                        objItem.Str4 = .Rows(i).Cells("Col_location").Value.ToString
                    End If

                    objItem.ItemStatus_Index = .Rows(i).Cells("cbo_ItemStatus").Value


                    ' **** Calculate Exp_Date ***
                    'Me.CalcualateDate_In_Gridview(i)
                    ' ***************************

                    ' *** value for date in gridview ***
                    Dim strIsMfg_Date As String = .Rows(i).Cells("chk_Mfg_Date").Value.ToString()
                    objItem.IsMfg_Date = System.Convert.ToBoolean(strIsMfg_Date)
                    ''chkExp_date
                    Dim strIsExp_Date As String = .Rows(i).Cells("chkExp_Date").Value.ToString
                    objItem.IsExp_Date = System.Convert.ToBoolean(strIsExp_Date)

                    Dim strMfg_Date As String = .Rows(i).Cells("Col_Mfg_Date").Value.ToString
                    objItem.Mfg_Date = System.Convert.ToDateTime(strMfg_Date)

                    Dim strExp_Date As String = .Rows(i).Cells("col_Exp_Date").Value.ToString
                    objItem.Exp_date = System.Convert.ToDateTime(strExp_Date)

                    Select Case .Rows(i).Cells("col_Qty").ReadOnly
                        Case False
                            objItem.Is_SN = False 'Not SN 
                        Case True
                            objItem.Is_SN = True 'IS SN
                        Case Else
                            objItem.Is_SN = False 'Not SN 
                    End Select


                    ' Add new By Dong_kk

                    'W
                    If .Rows(i).Cells("col_Width_PerPackage").Value IsNot Nothing Then
                        objItem.Flo2 = .Rows(i).Cells("col_Width_PerPackage").Value.ToString
                    End If

                    'L
                    If .Rows(i).Cells("col_Long_PerPackage").Value IsNot Nothing Then
                        objItem.Flo3 = .Rows(i).Cells("col_Long_PerPackage").Value.ToString
                    End If

                    'H
                    If .Rows(i).Cells("col_Height_PerPackage").Value IsNot Nothing Then
                        objItem.Flo4 = .Rows(i).Cells("col_Height_PerPackage").Value.ToString
                    End If


                    'Qty_Per_Pck
                    If .Rows(i).Cells("col_Qty_PerPackage").Value IsNot Nothing Then
                        objItem.Qty_Per_Pck = CDbl(.Rows(i).Cells("col_Qty_PerPackage").Value)
                    End If


                    'Item_Qty
                    If .Rows(i).Cells("col_Item_Qty").Value IsNot Nothing Then
                        objItem.Item_Qty = CDbl(.Rows(i).Cells("col_Item_Qty").Value)
                    End If


                    'Weight_Per_Pck
                    If .Rows(i).Cells("col_Weight_PerPackage").Value IsNot Nothing Then
                        objItem.Weight_Per_Pck = CDbl(.Rows(i).Cells("col_Weight_PerPackage").Value)

                    End If

                    'Volume_Per_Pck
                    If .Rows(i).Cells("col_Volume_PerPackage").Value IsNot Nothing Then
                        objItem.Volume_Per_Pck = CDbl(.Rows(i).Cells("col_Volume_PerPackage").Value)

                    End If

                    'OderItem_Price
                    If .Rows(i).Cells("col_ItemPrice").Value IsNot Nothing Then
                        objItem.OrderItem_Price = .Rows(i).Cells("col_ItemPrice").Value.ToString
                    End If

                    'Price_Per_Pck
                    If .Rows(i).Cells("col_ItemPrice_PerPackage").Value IsNot Nothing Then
                        objItem.Price_Per_Pck = .Rows(i).Cells("col_ItemPrice_PerPackage").Value.ToString

                    End If

                    'PACKAGE ITEM
                    If .Rows(i).Cells("cbo_Package_ItemQty").Value IsNot Nothing Then
                        objItem.Item_Package_Index = .Rows(i).Cells("cbo_Package_ItemQty").Value.ToString
                    End If


                    '********* DOCUMENT TYPE *********
                    '--- Lot_No From Header
                    objItem.Lot_No = objHeader.Lot_No



                    Select Case cboPlanReceive.SelectedValue
                        Case 9
                            '--- PO_No
                            If .Rows(i).Cells("col_PO_Number").Value IsNot Nothing Then
                                objItem.PO_No = .Rows(i).Cells("col_PO_Number").Value.ToString
                            End If
                        Case 7

                            If .Rows(i).Cells("col_PO_Number").Value IsNot Nothing Then
                                objItem.PO_No = .Rows(i).Cells("col_PO_Number").Value.ToString
                            End If

                        Case 16
                            '--- ASN_No,Shipment_No
                            If .Rows(i).Cells("col_PO_Number").Value IsNot Nothing Then
                                objItem.ASN_No = .Rows(i).Cells("col_PO_Number").Value.ToString
                            End If
                        Case 103

                    End Select
                    'PLOT
                    If .Rows(i).Cells("col_Plot").Value IsNot Nothing Then
                        objItem.Plot = .Rows(i).Cells("col_Plot").Value
                    End If
                    'INVOICE
                    If .Rows(i).Cells("col_Invoice").Value IsNot Nothing Then
                        objItem.Invoice_No = .Rows(i).Cells("col_Invoice").Value.ToString
                    End If

                    'Pallet No.
                    If .Rows(i).Cells("col_Pallet_No").Value IsNot Nothing Then
                        objItem.Str5 = .Rows(i).Cells("col_Pallet_No").Value.ToString
                    End If

                    'PO  ******แก้จากเก็บ index เป็น เก็บ name [ชั่วคราว]
                    If .Rows(i).Cells("ColumnPO_Item").Value IsNot Nothing Then
                        objItem.Str7 = .Rows(i).Cells("ColumnPO_Item").Value.ToString
                    End If


                    'Group NO
                    If .Rows(i).Cells("col_Group_No").Value IsNot Nothing Then
                        objItem.Str9 = .Rows(i).Cells("col_Group_No").Value.ToString
                    End If

                    'POINDEX
                    If .Rows(i).Cells("ColumnPO_Item").Value IsNot Nothing Then
                        objItem.Str10 = .Rows(i).Cells("ColumnPO_Item").Value.ToString
                    End If

                    'Decralation
                    If .Rows(i).Cells("col_Declaration_No").Value IsNot Nothing Then
                        objItem.Declaration_No = .Rows(i).Cells("col_Declaration_No").Value.ToString
                    End If



                    If SetAUTO_REFERENCE() = 1 Then
                        If .Rows(i).Cells("Col_Reference").Value = "" Then
                            MessageBox.Show("Reference", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.grdOrderItem.Rows(i).Selected = True
                            Exit Sub
                        Else
                            objItem.Str1 = .Rows(i).Cells("Col_Reference").Value
                        End If
                        '--- Referecne 2
                        If .Rows(i).Cells("col_Reference2").Value = "" Then
                            MessageBox.Show("Reference2", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.grdOrderItem.Rows(i).Selected = True
                            Exit Sub
                        Else
                            objItem.Str2 = .Rows(i).Cells("col_Reference2").Value
                        End If

                    Else

                        If .Rows(i).Cells("col_Reference").Value IsNot Nothing Then
                            objItem.Str1 = .Rows(i).Cells("col_Reference").Value
                        Else
                            objItem.Str1 = ""
                        End If
                        '--- Referecne 2
                        If .Rows(i).Cells("col_Reference2").Value IsNot Nothing Then
                            objItem.Str2 = .Rows(i).Cells("col_Reference2").Value
                        Else
                            objItem.Str2 = ""
                        End If
                        '--- ItemDefinition_Index
                        If .Rows(i).Cells("col_ItemDefinition").Tag IsNot Nothing Then
                            objItem.ItemDefinition_Index = .Rows(i).Cells("col_ItemDefinition").Tag.ToString
                        Else
                            objItem.ItemDefinition_Index = ""
                        End If
                    End If

                    objItem.OrderItem_RowIndex = i

                    If .Rows(i).Cells("col_Serial_No").Value IsNot Nothing Then
                        objItem.Serial_No = .Rows(i).Cells("col_Serial_No").Value
                    End If


                    'Lot/BATCTH
                    ' **** Check isPlot ****
                    ' *** You need to check that need Lot to used ***
                    Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    If objSku.isPlot_Value(objItem.Sku_Index) = True Then
                        ' *** Need to Input PLot in Order Item *** 
                        If Trim(objItem.Plot).ToString = "" Then
                            MessageBox.Show("กรุณากรอก Lot ผลิตด้วย", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.grdOrderItem.Rows(i).Selected = True
                            Exit Sub
                        End If
                    End If





                    ' HandlingType
                    If .Rows(i).Cells("cbo_HandlingType").Value IsNot Nothing Then
                        objItem.HandlingType_Index = .Rows(i).Cells("cbo_HandlingType").Value.ToString
                    End If

                    ' Plan_Process

                    If .Rows(i).Cells("col_DocumentPlan_Index").Value IsNot Nothing Then
                        objItem.DocumentPlan_Index = .Rows(i).Cells("col_DocumentPlan_Index").Value.ToString
                    Else
                        objItem.DocumentPlan_Index = ""
                    End If
                    If .Rows(i).Cells("col_DocumentPlanItem_Index").Value IsNot Nothing Then
                        objItem.DocumentPlanItem_Index = .Rows(i).Cells("col_DocumentPlanItem_Index").Value.ToString
                    Else
                        objItem.DocumentPlanItem_Index = ""
                    End If
                    If .Rows(i).Cells("col_DocumentPlan_No").Value IsNot Nothing Then
                        objItem.DocumentPlan_No = .Rows(i).Cells("col_DocumentPlan_No").Value.ToString
                    Else
                        objItem.DocumentPlan_No = ""
                    End If

                    If .Rows(i).Cells("col_Plan_Process").Value IsNot Nothing Then
                        objItem.Plan_Process = .Rows(i).Cells("col_Plan_Process").Value.ToString
                    Else
                        objItem.Plan_Process = -9
                    End If

                    'include from master site
                    If .Rows(i).Cells("col_str6").Value IsNot Nothing Then
                        objItem.Str6 = .Rows(i).Cells("col_str6").Value.ToString
                    End If

                    If .Rows(i).Cells("col_tax1").Value.ToString <> "" Then
                        objItem.Tax1 = .Rows(i).Cells("col_tax1").Value
                    End If

                    If .Rows(i).Cells("col_tax2").Value.ToString <> "" Then
                        objItem.Tax2 = .Rows(i).Cells("col_tax2").Value
                    End If

                    If .Rows(i).Cells("col_tax3").Value.ToString <> "" Then
                        objItem.Tax3 = .Rows(i).Cells("col_tax3").Value
                    End If

                    If .Rows(i).Cells("col_tax4").Value.ToString <> "" Then
                        objItem.Tax4 = .Rows(i).Cells("col_tax4").Value
                    End If

                    If .Rows(i).Cells("col_tax5").Value.ToString <> "" Then
                        objItem.Tax5 = .Rows(i).Cells("col_tax5").Value
                    End If

                    'add new 14/10/2009
                    If .Rows(i).Cells("col_HS_Code").Value IsNot Nothing Then
                        objItem.HS_Code = .Rows(i).Cells("col_HS_Code").Value
                    End If

                    If .Rows(i).Cells("col_ItemDescription").Value IsNot Nothing Then
                        objItem.ItemDescription = .Rows(i).Cells("col_ItemDescription").Value
                    End If


                    If .Columns.Contains("col_Seq") = True Then
                        If IsNumeric(.Rows(i).Cells("col_Seq").Value) = True Then
                            objItem.Seq = .Rows(i).Cells("col_Seq").Value
                        End If
                    End If


                    If .Columns.Contains("Col_ERP_Location") = True Then
                        If String.IsNullOrEmpty(.Rows(i).Cells("Col_ERP_Location").Value) = False Then
                            objItem.ERP_Location = .Rows(i).Cells("Col_ERP_Location").Value
                        End If
                    End If


                    'Begin : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 
                    Dim dblQty_Receive As Double = 0
                    Dim dblTotal_Qty_Receive As Double = 0
                    Dim dtCheckPlanReceive As New DataTable
                    With dtCheckPlanReceive.Columns
                        .Add("DocumentPlanItem_Index", GetType(String))
                        .Add("Plan_Process", GetType(Double))
                        .Add("PlanQty", GetType(Double))
                        .Add("Qty", GetType(Double))
                    End With

                    If Not String.IsNullOrEmpty(objItem.DocumentPlanItem_Index) Then

                        'dong edit 2014-05-25
                        Select Case objItem.Plan_Process
                            Case 9
                                Dim oPOI As New ml_Receive_PO
                                Dim dtPO As New DataTable
                                oPOI.getPo_byPoi_Index(objItem.DocumentPlanItem_Index)
                                dtPO = oPOI.GetDataTable
                                If dtPO.Rows.Count > 0 Then
                                    'Local Receive
                                    If dtCheckPlanReceive.Rows.Count > 0 Then
                                        dblQty_Receive = dtCheckPlanReceive.Compute("SUM(Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                                        dblTotal_Qty_Receive = dtCheckPlanReceive.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                                    End If
                                    'Real Oi Receive
                                    Dim oOI As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
                                    Dim dtOI As New DataTable
                                    oOI.getOrderItemAll(objItem.OrderItem_Index)
                                    dtOI = oOI.GetDataTable
                                    Dim dblQtyOI As Double = 0
                                    Dim dblTotal_Qty As Double = 0
                                    If dtOI.Rows.Count > 0 Then
                                        dblQtyOI = IIf(IsNumeric(dtOI.Rows(0)("Qty")), dtOI.Rows(0)("Qty"), 0)
                                        dblTotal_Qty = IIf(IsNumeric(dtOI.Rows(0)("Total_Qty")), dtOI.Rows(0)("Total_Qty"), 0)
                                    End If

                                    'Online Receive
                                    dtPO.Rows(0)("Qty_Bal") = IIf(IsNumeric(dtPO.Rows(0)("Qty_Bal")), dtPO.Rows(0)("Qty_Bal"), 0)
                                    dtPO.Rows(0)("Qty") = IIf(IsNumeric(dtPO.Rows(0)("Qty")), dtPO.Rows(0)("Qty"), 0)
                                    dtPO.Rows(0)("Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Received_Qty")), dtPO.Rows(0)("Received_Qty"), 0)
                                    dtPO.Rows(0)("Total_Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Total_Received_Qty")), dtPO.Rows(0)("Total_Received_Qty"), 0)

                                    Dim Qty_Receive_Real As Double = (dtPO.Rows(0)("Received_Qty") - dblQty_Receive - dblQtyOI) + objItem.Total_Qty

                                    Dim Total_Qty_Receive_Real As Double = 0

                                    Total_Qty_Receive_Real = (dtPO.Rows(0)("Total_Received_Qty") - dblTotal_Qty_Receive - dblTotal_Qty) + objItem.Total_Qty

                                    If dtPO.Rows(0)("Total_Qty") < Total_Qty_Receive_Real Then
                                        W_MSG_Information("บรรทัดที่ " & (i + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "" & "")
                                        'W_MSG_Information("บรรทัดที่ " & (i + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "จำนวนเกิน " & "")
                                        Exit Sub

                                    End If

                                End If

                                Dim drNewRow As DataRow = dtCheckPlanReceive.NewRow
                                drNewRow("Qty") = objItem.Qty
                                drNewRow("PlanQty") = objItem.Plan_Qty
                                drNewRow("Plan_Process") = objItem.Plan_Process
                                drNewRow("DocumentPlanItem_Index") = objItem.DocumentPlanItem_Index
                                dtCheckPlanReceive.Rows.Add(drNewRow)

                        End Select
                    End If

                    'End : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 




                    objItem.iRow_OrderItem_Index = i
                End With
                ' *** Add value ***
                objItemCollection.Add(objItem)

            Next

            ' PALLETTYPE
            Dim J As Integer = 0

            For J = 0 To grdPallet.Rows.Count - 1
                If grdPallet.Rows(J).Cells("col_UsePallet").Value <> 0 Then

                    With grdPallet

                        ' *** New Object *********
                        objPalletType = New tb_PalletType_History
                        ' ************************

                        If .Rows(J).Cells("col_PalletType_History_Index").Value = "" Then
                            Dim objDBIndex As New Sy_AutoNumber
                            .Rows(J).Cells("col_PalletType_History_Index").Value = objDBIndex.getSys_Value("PalletType_History_Index ")
                            objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value
                            objDBIndex = Nothing
                        Else
                            objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value.ToString
                        End If

                        'PalletType_Index
                        If .Rows(J).Cells("col_Palletindex").Value IsNot Nothing Then
                            objPalletType.PalletType_Index = .Rows(J).Cells("col_Palletindex").Value
                        End If

                        'Qty_In
                        If .Rows(J).Cells("col_UsePallet").Value IsNot Nothing Then
                            objPalletType.Qty_In = .Rows(J).Cells("col_UsePallet").Value
                        End If

                        'Qty_Bal
                        If .Rows(J).Cells("col_Palletqty").Value IsNot Nothing Then
                            objPalletType.Qty_Bal = .Rows(J).Cells("col_Palletqty").Value
                        End If

                    End With
                    objPalletTypeCollection.Add(objPalletType)
                End If
            Next

            ' *** Call Class for Manage Data ***
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.UPDATE, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing

            End Select


            '====== BEGIN Putaway  by OrderItem =====
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim bAutoPutaway As Boolean = False
                For i As Integer = 0 To grdOrderItem.Rows.Count - 2
                    Me._LocationID = grdOrderItem.Rows(i).Cells("Col_location").Value
                    Me.OrderItem_Id = objItemCollection(i).OrderItem_Index

                    If (_LocationID IsNot Nothing) And (Not _LocationID = "") Then
                        SetLocation_fromitemAll(i)
                        bAutoPutaway = True
                    End If
                Next

                If bAutoPutaway Then
                    Dim objClassDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
                    objClassDB.SaveData(Me._Order_Index)
                End If
            End If

            '====== END Putaway  by OrderItem =====


            If Not Me._Order_Index = "" Then
                _Order_Index = Me._Order_Index
            End If

            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnCopy.Enabled = False
            Me.btnPalletSlip.Enabled = False

            '------ Update Comment Image
            Dim odjOrderImage As New tb_Order_Image

            For iRow As Integer = 0 To grdOrderImage.RowCount - 1
                With odjOrderImage
                    .Order_Index = _Order_Index
                    .Order_Image_Index = grdOrderImage.Rows(iRow).Cells("Col_Order_Image_Index").Value.ToString
                    .Comment = grdOrderImage.Rows(iRow).Cells("Col_Comment").Value.ToString
                    .Update()
                End With
            Next


            'include from master site
            If Not Me._Order_Index = "" Then
                'W_MSG_Information_ByIndex(1)
                W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
                _Order_Index = Me._Order_Index
                cboPrint.Enabled = True
                btnPrintReport.Enabled = True
                objStatus = enuOperation_Type.UPDATE
                Me.getOrderHeader(_Order_Index)
                Me.grdOrderItem.ReadOnly = True
            Else
                W_MSG_Information_ByIndex(2)
                Exit Sub
            End If




        Catch ex As Exception
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
            W_MSG_Error(ex.Message.ToString)
        Finally
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
        End Try


    End Sub


#Region "Save Auto OrderItemLocation AND Tag"

    Sub SaveTag(ByVal pRowIndex As Integer)

        Try
            Dim objItemCollection As New List(Of tb_TAG)
            Dim objItem As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

            Dim objDBTempIndex As New Sy_AutoNumber
            Dim objAutoNumber As New Sy_AutoNumber

            SetTagItem(objItem, pRowIndex)
            Select Case Me._DEFAULT_AUTO_TAG
                Case enmGenTag_Type.NormalGen
                    objItem.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                    objItem.TAG_No = objAutoNumber.getSys_Value("TAG_NO")

                    objItemCollection.Add(objItem)

                Case enmGenTag_Type.GenPerQty

                    ' objItemPerQty = objItem

                    For i As Integer = 1 To grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value
                        Dim objItemPerQty As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                        'Set Value 
                        SetTagItem(objItemPerQty, pRowIndex)

                        'Set Value Per Tag
                        With objItemPerQty
                            .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                            .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                            .Qty_per_TAG = 1
                            .Weight_per_TAG = .Weight / .Qty
                            .Volume_per_TAG = .Volume / .Qty
                            .Ref_No3 = i + 1 & "/" & .Qty
                        End With

                        objItemCollection.Add(objItemPerQty)
                    Next
            End Select

            objDBTempIndex = Nothing
            objAutoNumber = Nothing

            Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

            objItemA.objItemCollection = objItemCollection
            objItemA.InsertData()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function SetTagItem(ByVal poTagItem As tb_TAG, ByVal pRowIndex As Integer) As tb_TAG
        Try

            poTagItem.Order_No = Me.txtOrder_No.Text
            poTagItem.Order_Index = _Order_Index
            poTagItem.Order_Date = Me.dtOrder.Value
            poTagItem.Order_Time = ""
            poTagItem.Customer_Index = Me._Customer_Index

            If txtSupplier_Id.Tag IsNot Nothing Then
                poTagItem.Supplier_Index = txtSupplier_Id.Tag
            Else
                poTagItem.Supplier_Index = ""
            End If

            If grdOrderItem.Rows(pRowIndex).Cells("col_OrderItem_Index").Value.ToString IsNot Nothing Then
                poTagItem.OrderItem_Index = grdOrderItem.Rows(pRowIndex).Cells("col_OrderItem_Index").Value.ToString
            Else
                poTagItem.OrderItem_Index = ""
            End If

            poTagItem.OrderItemLocation_Index = ""
            If grdOrderItem.Rows(pRowIndex).Cells("col_SKU_Index").Value.ToString IsNot Nothing Then
                poTagItem.Sku_Index = grdOrderItem.Rows(pRowIndex).Cells("col_SKU_Index").Value.ToString
            Else
                poTagItem.Sku_Index = ""
            End If

            If grdOrderItem.Rows(pRowIndex).Cells("col_Plot").Value IsNot Nothing Then
                poTagItem.PLot = grdOrderItem.Rows(pRowIndex).Cells("col_Plot").Value
            Else
                poTagItem.PLot = ""
            End If

            If grdOrderItem.Rows(pRowIndex).Cells("cbo_ItemStatus").Value IsNot Nothing Then
                poTagItem.ItemStatus_Index = grdOrderItem.Rows(pRowIndex).Cells("cbo_ItemStatus").Value
            Else
                poTagItem.ItemStatus_Index = ""
            End If

            If grdOrderItem.Rows(pRowIndex).Cells("cbo_Receive_Package").Value IsNot Nothing Then
                poTagItem.Package_Index = grdOrderItem.Rows(pRowIndex).Cells("cbo_Receive_Package").Value
            Else
                poTagItem.Package_Index = ""
            End If

            poTagItem.Unit_Weight = 0
            poTagItem.Size_Index = -1


            If grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_No").Value IsNot Nothing Then
                poTagItem.Pallet_No = grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_No").Value
            Else
                poTagItem.Pallet_No = ""
            End If


            poTagItem.TAG_Status = 1

            If grdOrderItem.Rows(pRowIndex).Cells("col_Reference").Value IsNot Nothing Then
                poTagItem.Ref_No1 = grdOrderItem.Rows(pRowIndex).Cells("col_Reference").Value
            Else
                poTagItem.Ref_No1 = ""
            End If

            If grdOrderItem.Rows(pRowIndex).Cells("col_Reference2").Value IsNot Nothing Then
                poTagItem.Ref_No2 = grdOrderItem.Rows(pRowIndex).Cells("col_Reference2").Value
            Else
                poTagItem.Ref_No2 = ""
            End If


            If grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value IsNot Nothing Then
                poTagItem.Qty = grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value
            Else
                poTagItem.Qty = 0
            End If


            If grdOrderItem.Rows(pRowIndex).Cells("col_Weight").Value IsNot Nothing Then
                poTagItem.Weight = grdOrderItem.Rows(pRowIndex).Cells("col_Weight").Value
            Else
                poTagItem.Weight = 0
            End If


            If grdOrderItem.Rows(pRowIndex).Cells("col_Volume").Value IsNot Nothing Then
                poTagItem.Volume = grdOrderItem.Rows(pRowIndex).Cells("col_Volume").Value
            Else
                poTagItem.Volume = 0
            End If

            poTagItem.Qty_per_TAG = poTagItem.Qty
            poTagItem.Weight_per_TAG = poTagItem.Weight
            poTagItem.Volume_per_TAG = poTagItem.Volume

            poTagItem.Ref_No3 = "1/1"
            Return poTagItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub SetLocation_fromitemAll(ByVal pRowIndex As Integer)
        Try
            Dim objOrderItem As New tb_OrderItem
            Dim clsJobOrder As New tb_JobOrder
            Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)

            Dim objOrderItemLocation As New tb_OrderItemLocation
            Dim objCollection As New List(Of tb_OrderItemLocation)

            Dim Recieve_Qty As Double
            Dim Recieve_Total_Qty As Double

            Dim sumQty As Double = 0
            Dim sumTotal_Qty As Double = 0

            clsJobOrder = setObjJobOrder()
            objOrderItem = GetOrderItem(Me.OrderItem_Id)

            objCollection.Clear()

            Recieve_Qty = objOrderItem.Qty  'Val(Me.grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value)
            Recieve_Total_Qty = objOrderItem.Total_Qty 'Val(Me.grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value)


            ''*** Check Location ***
            'With Me.grdOrderItem.Rows(pRowIndex)
            '    '###############################

            '    If .Cells("COL_Tag").Value Is Nothing Then
            '        ' *** Generate Tag Number ***
            '        Dim objDBIndex As New Sy_AutoNumber
            '        Dim New_Tag_No As String = objDBIndex.getSys_Value("Tag_Index")
            '        objDBIndex = Nothing

            '        .Cells("COL_Tag").Value = New_Tag_No
            '    Else
            '        If objClassDB.isExistTag_Number(.Cells("COL_Tag").Value) = True Then
            '            ' *** Invalid Location ***
            '            MessageBox.Show("กรุณากรอก Tag No. ใหม่อีกครั้ง(Tag No ซ้ำกัน)  " & .Cells("COL_Tag").Value & "", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '            Exit Sub
            '        End If
            '    End If

            'End With

            ' *** insert data to tb_OrderItemlocation ***
            objOrderItemLocation = New tb_OrderItemLocation

            Dim objDBTempIndex As New Sy_AutoNumber
            Dim strOrderItemLocation_Index As String = ""

            With objOrderItemLocation

                .Order_Index = objOrderItem.Order_Index
                .OrderItem_Index = objOrderItem.OrderItem_Index

                .Sku_Index = objOrderItem.Sku_Index
                .Package_Index = objOrderItem.Package_Index
                .Lot_No = objOrderItem.Lot_No
                .PLot = objOrderItem.Plot

                .ItemStatus_Index = objOrderItem.ItemStatus_Index
                .Serial_No = objOrderItem.Serial_No

                ' *** Define value from datagridview ***
                .Tag_No = Me.grdOrderItem.Rows(pRowIndex).Cells("COL_Tag").Value
                ' *** Location Index  ***
                .Location_Index = objClassDB.getLocation_Index(Me._LocationID).ToString
                .PalletType_Index = Me.grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_No").Value
                .Pallet_Qty = Me.grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_Qty").Value

                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_No").Value IsNot Nothing Then
                    .PalletType_Index = Me.grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_No").Value.ToString
                Else
                    .PalletType_Index = ""
                End If



                .Tag_No = Me.grdOrderItem.Rows(pRowIndex).Cells("COL_Tag").Value
                'Pallet_Qty
                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_Qty").Value IsNot Nothing Then
                    .Pallet_Qty = CDbl(Me.grdOrderItem.Rows(pRowIndex).Cells("col_Pallet_Qty").Value)
                Else
                    .Pallet_Qty = 0
                End If

                .Qty = objOrderItem.Qty
                .Total_Qty = objOrderItem.Total_Qty

                .Weight = objOrderItem.Weight
                .Volume = objOrderItem.Volume

                ' *** Reference by Tag property ***
                .Ratio = objOrderItem.Ratio

                'New 23/7/2008 11.56
                .MixPallet = 0


                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_Item_Qty").Value IsNot Nothing Then
                    .Qty_Item = Me.grdOrderItem.Rows(pRowIndex).Cells("col_Item_Qty").Value.ToString
                Else
                    .Qty_Item = 0
                End If
                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_ItemPrice").Value IsNot Nothing Then
                    .OrderItem_Price = CDbl(Me.grdOrderItem.Rows(pRowIndex).Cells("col_ItemPrice").Value.ToString)
                Else
                    .OrderItem_Price = 0
                End If

                Dim Qty_PerPackage As Double = 0
                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_Qty_PerPackage").Value IsNot Nothing Then
                    Qty_PerPackage = CDbl(Me.grdOrderItem.Rows(pRowIndex).Cells("col_Qty_PerPackage").Value.ToString)
                Else
                    Qty_PerPackage = 0
                End If
                Dim Price_PerPackage As Double = 0
                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_ItemPrice_PerPackage").Value IsNot Nothing Then
                    Price_PerPackage = CDbl(Me.grdOrderItem.Rows(pRowIndex).Cells("col_ItemPrice_PerPackage").Value.ToString)
                Else
                    Price_PerPackage = 0
                End If


                If Me.grdOrderItem.Rows(pRowIndex).Cells("col_ItemPrice").Value IsNot Nothing Then
                    .OrderItem_Price = CDbl(Me.grdOrderItem.Rows(pRowIndex).Cells("col_ItemPrice").Value.ToString)
                Else
                    .OrderItem_Price = 0
                End If
                ' *** Sum Balance ***
                sumQty = sumQty + .Qty
                sumTotal_Qty = sumTotal_Qty + .Total_Qty



                '--------------- Dong_kk add New Tag  ---------------------

                Dim objItemCollection As New List(Of tb_TAG)
                Dim objItem As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

                Dim objAutoNumber As New Sy_AutoNumber

                SetTagItem(objItem, pRowIndex)
                Select Case Me._DEFAULT_AUTO_TAG
                    Case enmGenTag_Type.NormalGen
                        objItem.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                        objItem.TAG_No = objAutoNumber.getSys_Value("TAG_NO")

                        .Tag_No = objItem.TAG_No
                        strOrderItemLocation_Index = objDBTempIndex.getSys_Value("OrderItemLocation_Index")
                        .OrderItemLocation_Index = strOrderItemLocation_Index
                        objCollection.Add(objOrderItemLocation)

                        objItem.OrderItemLocation_Index = strOrderItemLocation_Index
                        objItemCollection.Add(objItem)

                    Case enmGenTag_Type.GenPerQty

                        ' objItemPerQty = objItem
                        For i As Integer = 1 To grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value
                            Dim objItemPerQty As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                            'Set Value 
                            SetTagItem(objItemPerQty, pRowIndex)

                            'Set Value Per Tag
                            With objItemPerQty
                                .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                .Qty_per_TAG = 1
                                .Weight_per_TAG = .Weight / .Qty
                                .Volume_per_TAG = .Volume / .Qty
                                .Ref_No3 = (i - 1) + 1 & "/" & .Qty
                            End With



                            ' set new OrderItemLocation

                            Dim newobjOrderItemLocation As New tb_OrderItemLocation
                            newobjOrderItemLocation.Order_Index = .Order_Index
                            newobjOrderItemLocation.OrderItem_Index = .OrderItem_Index

                            newobjOrderItemLocation.Sku_Index = .Sku_Index
                            newobjOrderItemLocation.Package_Index = .Package_Index
                            newobjOrderItemLocation.Lot_No = .Lot_No
                            newobjOrderItemLocation.PLot = .PLot

                            newobjOrderItemLocation.ItemStatus_Index = .ItemStatus_Index
                            newobjOrderItemLocation.Serial_No = .Serial_No

                            ' *** Define value from datagridview ***
                            ' *** Location Index  ***
                            newobjOrderItemLocation.Location_Index = .Location_Index
                            newobjOrderItemLocation.PalletType_Index = .PalletType_Index

                            newobjOrderItemLocation.Tag_No = objItemPerQty.TAG_No



                            newobjOrderItemLocation.Ratio = objOrderItem.Ratio

                            If newobjOrderItemLocation.Pallet_Qty <> 0 Then
                                newobjOrderItemLocation.Pallet_Qty = 0
                            Else
                                newobjOrderItemLocation.Pallet_Qty = 0
                            End If

                            newobjOrderItemLocation.Qty = 1
                            newobjOrderItemLocation.Total_Qty = newobjOrderItemLocation.Qty * .Ratio
                            newobjOrderItemLocation.Weight = objItemPerQty.Weight
                            newobjOrderItemLocation.Volume = objItemPerQty.Volume

                            If .MixPallet <> 0 Then
                                newobjOrderItemLocation.MixPallet = 0
                            Else
                                newobjOrderItemLocation.MixPallet = 0
                            End If
                            If .Qty_Item <> 0 Then
                                newobjOrderItemLocation.Qty_Item = Qty_PerPackage
                            Else
                                newobjOrderItemLocation.Qty_Item = 0
                            End If
                            If .OrderItem_Price <> 0 Then
                                newobjOrderItemLocation.OrderItem_Price = Price_PerPackage
                            Else
                                newobjOrderItemLocation.OrderItem_Price = 0
                            End If

                            strOrderItemLocation_Index = objDBTempIndex.getSys_Value("OrderItemLocation_Index")
                            newobjOrderItemLocation.OrderItemLocation_Index = strOrderItemLocation_Index
                            objCollection.Add(newobjOrderItemLocation)


                            objItemPerQty.OrderItemLocation_Index = strOrderItemLocation_Index
                            objItemCollection.Add(objItemPerQty)
                        Next
                End Select

                objDBTempIndex = Nothing
                objAutoNumber = Nothing

                Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

                objItemA.objItemCollection = objItemCollection

                Select Case Me._DEFAULT_AUTO_TAG
                    Case enmGenTag_Type.NotGen
                    Case Else
                        objItemA.InsertData()
                End Select

                ' ''------------------------------------------------------
                ''strOrderItemLocation_Index = objDBTempIndex.getSys_Value("OrderItemLocation_Index")
                ''.OrderItemLocation_Index = strOrderItemLocation_Index
                ''objCollection.Add(objOrderItemLocation)
            End With




            ' *** Check Balance ***
            If Not Recieve_Qty = sumQty Then
                MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' *** Save Data ***
            Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
            objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
            objJobLocation = Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Function setObjJobOrder() As Object
        Try
            Dim objJobOrder As New tb_JobOrder
            objJobOrder.JobOrder_Index = Me._Order_Index
            objJobOrder.JobOrder_Date = Me.dtOrder.Value
            objJobOrder.JobOrder_No = Me.txtOrder_No.Text

            ' *************************************
            ' Current System Use tb_Order with tb_JobOrder by 1:1  
            '  Value of in  tb_JobOrder.JobOrder_Index field  >> tb_JobOrder.JobOrder_Index =tb_Order.Order_Index 
            objJobOrder.Order_Index = Me._Order_Index
            ' *************************************

            objJobOrder.Str1 = Me.txtRef_No1.Text
            objJobOrder.Str2 = Me.txtRef_No2.Text
            objJobOrder.Str3 = Me.txtRef_No3.Text
            objJobOrder.Str4 = Me.txtRef_No4.Text
            objJobOrder.Str5 = Me.txtRef_No5.Text

            Return objJobOrder
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Function GetOrderItem(ByVal pOrderItem_Index As String) As tb_OrderItem

        Dim objOrderItem As New tb_OrderItem

        Dim objClassDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProductSelection(pOrderItem_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then

                With objOrderItem
                    '  *** Define value to medel *** 
                    .Sku_Index = objDT.Rows(0).Item("SKU_Index").ToString
                    .OrderItem_Index = objDT.Rows(0).Item("OrderItem_Index").ToString
                    .Order_Index = objDT.Rows(0).Item("Order_Index").ToString
                    .Plot = objDT.Rows(0).Item("Plot").ToString
                    .Lot_No = objDT.Rows(0).Item("Lot_No").ToString
                    .Serial_No = objDT.Rows(0).Item("Serial_No").ToString
                    .ItemStatus_Index = objDT.Rows(0).Item("ItemStatus_Index").ToString
                    .Package_Index = objDT.Rows(0).Item("Package_Index").ToString

                    .Qty = objDT.Rows(0).Item("Qty").ToString
                    .Total_Qty = objDT.Rows(0).Item("Total_Qty").ToString
                    .Ratio = objDT.Rows(0).Item("Ratio").ToString

                    .Weight = objDT.Rows(0).Item("Weight").ToString
                    .Volume = objDT.Rows(0).Item("Volume").ToString
                    .Item_Qty = objDT.Rows(0).Item("Item_Qty").ToString
                    .OrderItem_Price = objDT.Rows(0).Item("OrderItem_Price").ToString
                End With

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

        Return objOrderItem

    End Function

#End Region


    ''' <summary>
    ''' Delete Order Item from datagrid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd - 2010-05-15 Fix bug on deleting of asset. The S/N should be deleted only on the items selected.
    ''' </remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If

        If grdOrderItem.Rows.Count <= 1 Then
            Exit Sub
        End If

        Try
            Me.DeleteOrderItem(Me.grdOrderItem.CurrentRow.Index, True)
            AutoSort(grdOrderItem)
            Get_SumData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub grdOrderItem_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdOrderItem.RowsAdded
        Try
            SetRunningNo(grdOrderItem)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdOrderItem_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdOrderItem.RowsRemoved
        Try
            SetRunningNo(grdOrderItem)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub DeleteOrderItem(ByVal intRow As Integer, ByVal isRemoveRow As Boolean)
        Try
            If Me.grdOrderItem.Rows(intRow).Cells("col_Qty").ReadOnly = True Then Exit Sub
            Dim odelete As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
            Dim oOrderItem As New tb_OrderItem
            oOrderItem.Plan_Process = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdOrderItem.Rows(intRow).Cells("col_Plan_Process").Value, GetType(Integer))
            If oOrderItem.Plan_Process = 0 Then oOrderItem.Plan_Process = -9
            oOrderItem.DocumentPlan_No = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdOrderItem.Rows(intRow).Cells("col_DocumentPlan_No").Value, GetType(String))
            oOrderItem.DocumentPlan_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdOrderItem.Rows(intRow).Cells("col_DocumentPlan_Index").Value, GetType(String))
            oOrderItem.DocumentPlanItem_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdOrderItem.Rows(intRow).Cells("col_DocumentPlanItem_Index").Value, GetType(String))
            odelete.UPDATE_STATUS_PLANRECEIVE(oOrderItem, Nothing)

            Dim _OrderItem_Index As String = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdOrderItem.Rows(intRow).Cells("col_OrderItem_Index").Value, GetType(String))
            If Not _OrderItem_Index = "" Then
                Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
                If objDB.isItemLocation(Me.grdOrderItem.Rows(intRow).Cells("col_OrderItem_Index").Value) = True Then
                    MessageBox.Show("ไม่สามารถลบรายการได้ เนื่องจากสินค้ามีการจัดเก็บแล้วกรุณาตรวจสอบสินค้าในคลัง", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    objDB = Nothing
                    Exit Sub
                End If



                Dim pSku_Index As String
                Dim OBJ As New tb_OrderItemSerial
                If grdOrderItem.CurrentRow.Cells("col_SKU_Index").Value Is Nothing Then
                    objDB = Nothing
                    Exit Sub
                End If
                pSku_Index = grdOrderItem.CurrentRow.Cells("col_SKU_Index").Value.ToString
                If OBJ.CheckIsSerial(pSku_Index) = True Then
                    If objDB.isItemSerial(Me.grdOrderItem.Rows(intRow).Cells("col_OrderItem_Index").Value) = True Then
                        MessageBox.Show("ไม่สามารถลบรายการได้ เนื่องจากมีการสร้าง Serial แล้ว", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                        objDB = Nothing
                        Exit Sub
                    End If
                End If



                If objDB.isItemTAG(Me.grdOrderItem.Rows(intRow).Cells("col_OrderItem_Index").Value) = True Then
                    MessageBox.Show("ไม่สามารถลบรายการได้ เนื่องจากมีการสร้าง TAG แล้ว", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    objDB = Nothing
                    Exit Sub
                End If


                If isRemoveRow Then
                    If MessageBox.Show("คุณต้องการลบรายการใช่หรือไม่ ", "ยืนยันการลบข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.No Then Exit Sub
                End If


                'add by dong 2011/08/11 getCurrent
                Dim drOrderItemData As DataRow
                objDB.getOrderItemAll(Me.grdOrderItem.Rows(intRow).Cells("col_OrderItem_Index").Value)
                drOrderItemData = objDB.GetDataTable.Rows(0)
                'Delete OrderItem
                If objDB.Delete_OrderItem(Me.grdOrderItem.Rows(intRow).Cells("col_OrderItem_Index").Value) = True Then
                    oOrderItem.Qty = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drOrderItemData("Qty"), GetType(Double))
                    oOrderItem.Weight = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drOrderItemData("Weight"), GetType(Double))
                    oOrderItem.Volume = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drOrderItemData("Volume"), GetType(Double))
                    'calculate ratio
                    oOrderItem.Sku_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drOrderItemData("Sku_Index"), GetType(String))
                    oOrderItem.Package_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drOrderItemData("Package_Index"), GetType(String))
                    oOrderItem.Total_Qty = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(drOrderItemData("Total_Qty"), GetType(Double))

                    odelete.UPDATE_DELETE_PLANRECEIVE(oOrderItem, Nothing)
                    If isRemoveRow Then
                        Me.grdOrderItem.Rows.RemoveAt(grdOrderItem.Rows(intRow).Index)
                        'AutoSort(grdOrderItem)
                    End If

                End If
                objDB = Nothing

            Else
                If isRemoveRow Then
                    Me.grdOrderItem.Rows.RemoveAt(grdOrderItem.Rows(intRow).Index)
                    'AutoSort(grdOrderItem)
                End If
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub AutoSort(ByVal grd As DataGridView)
        Try

            grd.CommitEdit(DataGridViewDataErrorContexts.Commit)
            grd.Columns("col_Seq").ValueType = GetType(System.Int32)
            'grd.CommitEdit(DataGridViewDataErrorContexts.Commit)

            grd.Sort(grd.Columns("col_Seq"), System.ComponentModel.ListSortDirection.Ascending)
            'SetRunningNo(grd)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetRunningNo(ByVal grd As DataGridView)
        For i As Integer = 0 To grd.RowCount - 2
            grd.Rows(i).Cells("col_Seq").Value = i + 1
        Next
    End Sub
#Region "Comment OLD CODE "
    '' *************** OLD CODE ******************
    ''chkMfg_Date
    'Dim strIsMfg_Date As String = .Rows(i).Cells("chkMfg_Date").Value.ToString()
    '                objItem.IsMfg_Date = System.Convert.ToBoolean(strIsMfg_Date)
    ''chkExp_date
    'Dim strIsExp_Date As String = .Rows(i).Cells("chkExp_Date").Value.ToString
    '                objItem.IsExp_Date = System.Convert.ToBoolean(strIsExp_Date)

    ''Mfg_Date
    '                If .Rows(i).Cells("Mfg_Date").Value IsNot Nothing Then
    'Dim strMfg_Date As String = .Rows(i).Cells("Mfg_Date").Value.ToString
    '                    objItem.Mfg_Date = System.Convert.ToDateTime(strMfg_Date)
    '                End If

    ''Exp_date
    '                If .Rows(i).Cells("Exp_Date").Value IsNot Nothing Then
    '' *** Calculate Expire date ***
    '                    If objItem.IsMfg_Date = True And objItem.IsExp_Date = True Then
    'Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
    '                        ItemLife_Total_Day = objClassDB.getItemLife_TotalDay(grdOrderItem.Rows(i).Cells("Sku_Index").Value, objItem.Mfg_Date)
    '' Dim Exp_Date As DateTime
    ''  Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString
    ''  Exp_Date = objClassDB.setExp_Date(grdList.Rows(i).Cells("Sku_Index").Value, objItem.Mfg_Date)
    ''  grdList.Rows(i).Cells("Exp_Date").Value = Format(CDate(Exp_Date), "dd/MM/yyyy").ToString
    '                        objClassDB = Nothing

    'Dim strExp_Date As String = .Rows(i).Cells("Exp_Date").Value.ToString
    '                        objItem.Exp_date = System.Convert.ToDateTime(strExp_Date)
    '                        objItem.Exp_date = objItem.Exp_date.AddDays(ItemLife_Total_Day)

    '                    Else
    'Dim strExp_Date As String = .Rows(i).Cells("Exp_Date").Value.ToString
    '                        objItem.Exp_date = System.Convert.ToDateTime(strExp_Date)
    '                    End If

    '                End If
    '' *************** END OLD CODE ******************
#End Region



    Function CheckPO_use() As Boolean
        Try
            Dim RowsCount As Integer = grdOrderItem.Rows.Count
            Dim StatusPO As Boolean = False
            Dim i As Integer = 0
            For i = 0 To RowsCount - 2
                Dim grdRows As Integer = i

                StatusPO = False
                If grdOrderItem.Rows(grdRows).Cells("ColumnPO_Remain").Value = "" Then
                    StatusPO = True
                    Continue For
                End If

                Dim strOrderNo As String = grdOrderItem.Rows(grdRows).Cells("ColumnPO_Remain").Value.ToString
                Dim strSku_ID As String = grdOrderItem.Rows(grdRows).Cells("col_SKU_Id").Value.ToString

                Dim oml_Receive_PO As New ml_Receive_PO
                oml_Receive_PO.Select_OrderID(strOrderNo, strSku_ID)
                Dim dtPurChaseOrder As DataTable = oml_Receive_PO.DataTable
                If dtPurChaseOrder.Rows.Count = 0 Then
                    MessageBox.Show(" PO ไม่ถูกต้อง!!!   ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Function
                End If
                Dim Remain_Qty As Integer = CDbl(grdOrderItem.Rows(grdRows).Cells("col_Plan_Qty").Value.ToString)
                If grdOrderItem.Rows(grdRows).Cells("col_Qty").Value Is Nothing Then
                    MessageBox.Show(" จำนวนไม่ถูกต้อง!!!   ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Function
                End If
                Dim Receive_Qty As Integer = CDbl(grdOrderItem.Rows(grdRows).Cells("col_Qty").Value.ToString)
                If Remain_Qty < Receive_Qty Then
                    MessageBox.Show(" จำนวนไม่ถูกต้อง!!!   ", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Function
                End If

            Next
            StatusPO = True
            Return StatusPO
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' -------------------------------------------------
    ''' Update Date : 23/04/2010
    ''' Update By   : Dong_kk
    ''' Update For  : แยกการ Copy ธรรมดา ออกจากเรื่อง Spitlot และ Split Qty
    ''' -------------------------------------------------
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try
            If Me.grdOrderItem.Rows.Count <= 1 Then Exit Sub
            Me.RowCopy_OrderItem(grdOrderItem)
            'AutoSort
            Me.AutoSort(grdOrderItem)
            Me.Get_SumData()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="grd"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Date 14/01/2010
    ''' Update By : Dong_kk 
    ''' 1.เพิ่มการรับหน่วยที่รับได้ต้องเป็นหน่วยที่เล็กกว่าเท่านั้น
    ''' 2.ก็อปปี้ HandlingType
    ''' -------------------------------------------------
    ''' Update Date : 22/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Copy Item Package
    ''' -------------------------------------------------
    ''' </remarks>
    Function RowCopyGrd(ByVal grd As DataGridView, Optional ByVal InsertRowIndex As Integer = -1) As Integer
        Try
            Dim num As Integer = grd.CurrentRow.Index
            Dim intNewRowIndex As Integer = 0
            Dim i As Integer = 0
            Counter = -9
            If InsertRowIndex = 0 Then
                intNewRowIndex = grd.Rows.Add()
            Else
                grd.Rows.Insert(InsertRowIndex, "")
                intNewRowIndex = InsertRowIndex + 1
            End If



            Dim strSku_index As String = grd.Rows(num).Cells("col_SKU_Index").Value
            Dim strPackage_index As String = ""
            If grd.Rows(num).Cells("col_Plan_Package").Tag IsNot Nothing Then
                strPackage_index = grd.Rows(num).Cells("col_Plan_Package").Tag.ToString
            Else
                strPackage_index = ""
            End If

            getSKU_Package(strSku_index, strPackage_index, intNewRowIndex)
            Me.get_Package(intNewRowIndex, strSku_index)
            For i = 0 To grd.Columns.Count - 1
                grd.Rows(intNewRowIndex).Cells(i).Value = grd.Rows(num).Cells(i).Value
            Next

            For i = 0 To grd.Columns.Count - 1
                grd.Rows(intNewRowIndex).Cells(i).Tag = grd.Rows(num).Cells(i).Tag
            Next

            grd.Rows(intNewRowIndex).Cells("cbo_Receive_Package").Tag = grd.Rows(num).Cells("cbo_Receive_Package").Tag
            grd.Rows(intNewRowIndex).Cells("col_OrderItem_Index").Value = ""

            Me.getgrdHandlingType(intNewRowIndex)
            Counter = 0
            Return 1
        Catch ex As Exception
            Counter = 0
            Throw ex

        End Try
    End Function
    ''' <summary>
    ''' -------------------------------------------------
    ''' add Date : 23/04/2010
    ''' add By   : Dong_kk
    ''' add For  : เนื่องจากเอาการ copy ของเก่าไปทำเรื่อง Split lot แล้วทำให้การ copy ธรรมดา Error / จึงแยกออกจากกัน
    ''' -------------------------------------------------
    ''' </summary>
    ''' <param name="grd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function RowCopy_OrderItem(ByVal grd As DataGridView) As Integer
        Try
            Dim num As Integer = grd.CurrentRow.Index
            Dim intNewRowIndex As Integer = 0
            Dim i As Integer = 0
            Counter = -9

            If grd.Rows(num).Cells("col_SKU_Index").Value = Nothing Then
                Exit Function
            End If

            If grd.Rows(num).Cells("col_SKU_Index").Value.ToString = "" Then
                Exit Function
            End If

            grd.Rows.Add()

            Dim strSku_index As String = grd.Rows(num).Cells("col_SKU_Index").Value
            Dim strPackage_index As String = ""
            If grd.Rows(num).Cells("col_Plan_Package").Tag IsNot Nothing Then
                strPackage_index = grd.Rows(num).Cells("col_Plan_Package").Tag.ToString
            Else
                strPackage_index = ""
            End If

            getSKU_Package(strSku_index, strPackage_index, grd.Rows.Count - 2)
            Me.get_Package(grd.Rows.Count - 2, strSku_index)
            For i = 0 To grd.Columns.Count - 1
                'Select Case grd.Rows(grd.Rows.Count - 2).Cells(i).ColumnIndex
                '    Case Is = grd.Columns("col_Seq").Index
                '        Continue For
                'End Select
                grd.Rows(grd.Rows.Count - 2).Cells(i).Value = grd.Rows(num).Cells(i).Value

                Select Case grd.Rows(grd.Rows.Count - 2).Cells(i).ColumnIndex
                    Case Is = grd.Columns("col_SKU_Id").Index
                        If grd.Rows(num).Cells(i).ReadOnly = True Then grd.Rows(grd.Rows.Count - 2).Cells(i).ReadOnly = True
                End Select

            Next

            For i = 0 To grd.Columns.Count - 1
                grd.Rows(grd.Rows.Count - 2).Cells(i).Tag = grd.Rows(num).Cells(i).Tag
            Next

            grd.Rows(grd.Rows.Count - 2).Cells("cbo_Receive_Package").Tag = grd.Rows(num).Cells("cbo_Receive_Package").Tag
            grd.Rows(grd.Rows.Count - 2).Cells("col_OrderItem_Index").Value = ""

            Me.getgrdHandlingType(grd.Rows.Count - 2)
            Counter = 0

            ''AutoSort
            'AutoSort(grdOrderItem)  'กระทบกับ Function Spilt Lot

            Return 1
        Catch ex As Exception
            Counter = 0
            Throw ex

        End Try
    End Function

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

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            SetDEFAULT_QTY()

            SETDEFAULT_CUSTOMER_INDEX()



            Me.getDocumentType(1)

            grdOrderItem.Rows.Clear()
            btnDelete.Enabled = True
            btnSave.Enabled = True
            Me.btnPalletSlip.Enabled = True
            txtOrder_No.Text = ""

            ' ****** Loading Config ******
            Config_Order()
            Config_OrderItem()
            ' ****************************

            ' **** Loading Data from table master to datagridview ****
            Me.getPalletType()

            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    _Order_Index = objDBTempIndex.getSys_Value("Order_Index")
                    objDBTempIndex = Nothing

                Case enuOperation_Type.UPDATE
                    Me.txtOrder_No.ReadOnly = True
                    Me.getOrderHeader(_Order_Index)
                    Me.getOrderItemDetail(_Order_Index)

            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub getCboCtnrSize()

        Dim objDBContainer As New ms_Container(ms_Container.enuOperation_Type.SEARCH)
        Dim objDTContainer As DataTable = New DataTable

        Try

            '--- UnStaffing
            txtStr2.DataSource = Nothing
            objDBContainer.GetAllAsDataTable()
            objDTContainer = objDBContainer.DataTable


            txtStr2.DisplayMember = "Description"
            txtStr2.ValueMember = "Container_Index"
            txtStr2.DataSource = objDTContainer

        Catch ex As Exception
            Throw ex
        Finally
            objDBContainer = Nothing
            objDTContainer = Nothing
        End Try

    End Sub

    Private Sub getCboTypeTruck()

        Dim objDBVehicle As New ms_VehicleType(ms_VehicleType.enuOperation_Type.SEARCH)
        Dim objDTVehicle As DataTable = New DataTable

        Try

            '--- UnStaffing
            txtStr9.DataSource = Nothing
            objDBVehicle.SearchData_Click("", "")
            objDTVehicle = objDBVehicle.DataTable

            txtStr9.DisplayMember = "Description"
            txtStr9.ValueMember = "VehicleType_Index"
            txtStr9.DataSource = objDTVehicle


        Catch ex As Exception
            Throw ex
        Finally
            objDBVehicle = Nothing
            objDTVehicle = Nothing
        End Try

    End Sub



    Sub getDocumentType_Itemstatus(ByVal RowIndex As Integer)
        Dim objDocumentType_Itemstatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)

        Try
            'Load Item Status By DocumentType_Index
            'If ReceiveType = 2 Then
            '    objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocOrderReturn.SelectedValue)
            'Else
            objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocumentType.SelectedValue)
            'End If

            ' objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocumentType.SelectedValue)
            Dim odtDocumentType_Itemstatus As New DataTable
            odtDocumentType_Itemstatus = objDocumentType_Itemstatus.DataTable

            If odtDocumentType_Itemstatus.Rows.Count = 0 Then
                ' TODO : Wait Assigne Msg
                W_MSG_Information("ไม่มีการกำหนดสถานะสินค้าในการรับ ประเภทการรับนี้ ")
                Exit Sub
            End If

            With cbo_ItemStatus
                .DisplayMember = "ItemStatusDes"
                .ValueMember = "ItemStatus_Index"
                .DataSource = odtDocumentType_Itemstatus
            End With

            'Load Default Item Status By DocumentType_Index
            objDocumentType.SearchDocumentType(cboDocumentType.SelectedValue)
            '   objDocumentType.SearchDocumentType(cboDocumentType.SelectedValue)
            Dim odtDocumentType As New DataTable
            odtDocumentType = objDocumentType.DataTable

            If odtDocumentType.Rows.Count > 0 Then
                If odtDocumentType.Rows(0).Item("ItemStatus_Index").ToString <> "" Then
                    grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = odtDocumentType.Rows(0).Item("ItemStatus_Index").ToString
                Else
                    grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = odtDocumentType_Itemstatus.Rows(0).Item("ItemStatus_Index").ToString
                End If
            Else
                grdOrderItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = odtDocumentType_Itemstatus.Rows(0).Item("ItemStatus_Index").ToString
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDocumentType_Itemstatus = Nothing
        End Try

    End Sub

    Private Sub grdPallet_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPallet.CellValueChanged
        Dim j As Integer = 0
        Try
            If grdPallet.Rows.Count > 0 Then
                If grdPallet.Rows(j).Cells("col_UsePallet").Value > grdPallet.Rows(j).Cells("col_Palletqty").ToString Then
                    MessageBox.Show("ขออภัยค่ะ จำนวนที่ใช้มีจำนวนมากกว่าจำนวนพาเลทคงเหลือ", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    grdPallet.Rows(j).Cells("col_UsePallet").Value = ""
                End If
            Else
                Exit Sub
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub cboDocumentType_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectionChangeCommitted
        Try
            For iRow As Integer = 0 To grdOrderItem.Rows.Count - 2
                getDocumentType_Itemstatus(iRow)
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
        'Load New data Combobox


    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub frmDeposit_WMS_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        'Dim objAsset As New tb_AssetLocationBalance
        Try
            ' Chcek : ถ้าไม่ save ใบ order ให้ลบ ข้อมูล S/N ใน tb_asset

            If grdOrderItem.Rows(0).Cells("col_OrderItem_Index").Value Is Nothing Then
                '  objAsset.DeleteAsset(" WHERE Order_Index = '" & _order_index & "'")
            End If

            '--- Check PO
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim oml_Receive_PO As New ml_Receive_PO
                    Dim oml_ASN As New ml_Receive_ASN
                    Dim oml_Shipment As New ml_Receive_Shipment
                    Dim objDT As DataTable = New DataTable

                    For i As Integer = 0 To grdOrderItem.Rows.Count - 1
                        Dim strDocumentPlan_No As String = ""
                        If (grdOrderItem.Rows(i).Cells("col_DocumentPlan_No").Value) IsNot Nothing Then
                            strDocumentPlan_No = grdOrderItem.Rows(i).Cells("col_DocumentPlan_No").Value
                        End If

                        Select Case grdOrderItem.Rows(i).Cells("col_Plan_Process").Value
                            Case 9
                                oml_Receive_PO.UpdatePOItem(strDocumentPlan_No, 2)
                            Case 7
                                'Not Real Time
                            Case 16
                                oml_ASN.UpdateStatusReceive(strDocumentPlan_No, 2)
                            Case 103
                                oml_Shipment.UpdateStatusReceive(strDocumentPlan_No, 2)

                        End Select
                    Next
            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        Finally
            e.Cancel = False
        End Try

    End Sub
    Sub Get_SumData()
        Try
            Dim SumQty As Double = 0
            Dim SumWeight As Double = 0
            Dim SumVolume As Double = 0
            Dim SumItem_Qty As Double = 0
            Dim SumItemPrice As Double = 0
            Dim SumNet_Weight As Double = 0

            Dim i As Integer = 0

            For i = 0 To grdOrderItem.Rows.Count - 1
                With grdOrderItem.Rows(i)
                    If .Cells("col_Qty").Value IsNot Nothing Then SumQty += CDbl(.Cells("col_Qty").Value)
                    If .Cells("col_Weight").Value IsNot Nothing Then SumWeight += CDbl(.Cells("col_Weight").Value)
                    If .Cells("col_Volume").Value IsNot Nothing Then SumVolume += CDbl(.Cells("col_Volume").Value)
                    If .Cells("col_Item_Qty").Value IsNot Nothing Then SumItem_Qty += CDbl(.Cells("col_Item_Qty").Value)
                    If .Cells("col_ItemPrice").Value IsNot Nothing Then SumItemPrice += CDbl(.Cells("col_ItemPrice").Value)
                    If .Cells("col_Net_Weight").Value IsNot Nothing Then SumNet_Weight += CDbl(.Cells("col_Net_Weight").Value)
                End With
            Next

            txtSumQty.Text = Format(SumQty) 'Format(SumQty, "0")
            txtSumNet_Wt.Text = Format(SumWeight) 'Format(SumWeight, "#,##0.0000")
            txtSumVolume.Text = Format(SumVolume) 'Format(SumVolume, "#,##0.0000")
            txtPackage.Text = Format(SumItem_Qty) 'Format(SumItem_Qty, "#,##0.00")
            txtRate.Text = Format(SumItemPrice) 'Format(SumItemPrice, "#,##0.00")
            txtSumGrs_Wt.Text = Format(SumNet_Weight) 'Format(SumNet_Weight, "#,##0.0000")

            'txtSumQty.Text = Format(CDbl(Cal_Sum("col_Qty").ToString), "0")
            'txtSumNet_Wt.Text = Format(CDbl(Cal_Sum("col_Weight").ToString), "#,##0.0000")
            'txtSumVolume.Text = Format(CDbl(Cal_Sum("col_Volume").ToString), "#,##0.0000")
            'txtPackage.Text = Format(CDbl(Cal_Sum("col_Item_Qty").ToString), "#,##0.00")
            'txtRate.Text = Format(CDbl(Cal_Sum("col_ItemPrice").ToString), "#,##0.00")
            'txtSumGrs_Wt.Text = Format(CDbl(Cal_Sum("col_Net_Weight").ToString), "#,##0.0000")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub grdOrderItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderItem.CellEndEdit

        Try
            Dim Column_Index As String = grdOrderItem.CurrentCell.ColumnIndex
            'txtEdit_Keypress(sender, grdOrderItem.CurrentCell.ColumnIndex)
            Select Case Column_Index

                '--- CBM
                Case Is = grdOrderItem.Columns("col_Volume_PerPackage").Index
                    grdOrderItem.CurrentRow.Cells("col_Volume_PerPackage").Value = Calculate_CBM().ToString
                    grdOrderItem.CurrentRow.Cells("col_Volume").Value = Calculate_CBM_Total().ToString
                    Calculate_Pck()

                Case Is = grdOrderItem.Columns("col_Weight_PerPackage").Index
                    grdOrderItem.CurrentRow.Cells("col_Weight").Value = Calculate_Net_Pck().ToString

                Case Is = grdOrderItem.Columns("col_Long_PerPackage").Index
                    grdOrderItem.CurrentRow.Cells("col_Volume_PerPackage").Value = Calculate_CBM().ToString
                    grdOrderItem.CurrentRow.Cells("col_Volume").Value = Calculate_CBM_Total().ToString

                Case Is = grdOrderItem.Columns("col_Height_PerPackage").Index
                    grdOrderItem.CurrentRow.Cells("col_Volume_PerPackage").Value = Calculate_CBM().ToString
                    grdOrderItem.CurrentRow.Cells("col_Volume").Value = Calculate_CBM_Total().ToString
                    '25/03/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                Case Is = grdOrderItem.Columns("col_ItemPrice_PerPackage").Index

                    If DataNumeric(grdOrderItem.CurrentRow.Cells("col_ItemPrice_PerPackage").Value) = False Then
                        grdOrderItem.CurrentRow.Cells("col_ItemPrice_PerPackage").Value = "0.00"
                    End If

                    grdOrderItem.CurrentRow.Cells("col_ItemPrice").Value = Calculate_PRICE_TOTAL().ToString

                Case Is = grdOrderItem.Columns("col_ItemPrice").Index
                    '25/03/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง
                    If DataNumeric(grdOrderItem.CurrentRow.Cells("col_ItemPrice").Value) = False Then
                        grdOrderItem.CurrentRow.Cells("col_ItemPrice").Value = "0.00"
                    End If
                    'pla add 05112012----------------
                    If IsNumeric(grdOrderItem.Rows(e.RowIndex).Cells("col_ItemPrice_PerPackage").Value) Then
                        If Val(grdOrderItem.Rows(e.RowIndex).Cells("col_ItemPrice_PerPackage").Value) > 0 Then
                            W_MSG_Information("Product Code : " & grdOrderItem.Rows(e.RowIndex).Cells("col_Sku_Id").Value & " : Weight ไม่ตรงกับค่ามาตรฐานที่กำหนด")
                        End If
                    End If
                    '-----------------
                    grdOrderItem.CurrentRow.Cells("col_ItemPrice_PerPackage").Value = Calculate_PRICE().ToString

                Case Is = grdOrderItem.Columns("col_Width_PerPackage").Index
                    grdOrderItem.CurrentRow.Cells("col_Volume_PerPackage").Value = Calculate_CBM().ToString
                    grdOrderItem.CurrentRow.Cells("col_Volume").Value = Calculate_CBM_Total().ToString


                Case Is = grdOrderItem.Columns("col_Weight").Index
                    grdOrderItem.CurrentRow.Cells("col_Weight_PerPackage").Value = Calculate_CBM_PerPck().ToString

                Case Is = grdOrderItem.Columns("col_Item_Qty").Index
                    grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value = Calculate_Qty_PerPck().ToString

                    'Calculate_Pck()
                    grdOrderItem.CurrentRow.Cells("col_Volume").Value = Calculate_CBM_Total().ToString
                    grdOrderItem.CurrentRow.Cells("col_Weight").Value = Calculate_Net_Pck().ToString
                Case Is = grdOrderItem.Columns("col_Qty_PerPackage").Index
                    grdOrderItem.CurrentRow.Cells("col_Qty").Value = Calculate_Qty().ToString
                    grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value = Calculate_Item_Qty().ToString
                    'Calculate_Pck()
                    grdOrderItem.CurrentRow.Cells("col_Volume").Value = Calculate_CBM_Total().ToString
                    grdOrderItem.CurrentRow.Cells("col_Weight").Value = Calculate_Net_Pck().ToString

                Case Is = grdOrderItem.Columns("col_SKU_Index").Index

                    Dim strSku_index As String = ""
                    strSku_index = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                    Dim strPO_NO As String = grdOrderItem.Rows(e.RowIndex).Cells("col_PO_Number").Value
                    Setting_PoBySku(strSku_index, e.RowIndex, strPO_NO)

                Case Is = grdOrderItem.Columns("Col_location").Index
                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value Is Nothing Then Exit Sub
                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString = "" Then Exit Sub
                    Dim oChk_LocationAlias As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    If oChk_LocationAlias.Chk_Alias(grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString) = False Then
                        W_MSG_Information_ByIndex("300019")
                        grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value = _tmpLocation_Alias
                        Exit Sub
                    Else
                        If oChk_LocationAlias.ChkQty_Alias(grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString, _tmpTotalQty_PutAway) = False Then
                            W_MSG_Information_ByIndex("300020")
                            grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value = _tmpLocation_Alias
                        End If
                    End If

                Case Is = grdOrderItem.Columns("col_Qty").Index
                    If grdOrderItem.CurrentRow.Cells("col_Qty").Value Is Nothing Then
                        grdOrderItem.CurrentRow.Cells("col_Qty").Value = 0
                    ElseIf grdOrderItem.CurrentRow.Cells("col_Qty").Value.ToString = "" Then
                        grdOrderItem.CurrentRow.Cells("col_Qty").Value = 0
                    End If

                    grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value = grdOrderItem.CurrentRow.Cells("col_Qty").Value
                    grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value = Calculate_Qty_PerPck().ToString
                    Calculate_Pck()


                    If grdOrderItem.Rows(e.RowIndex).Cells("col_Plan_Process").Value IsNot Nothing AndAlso grdOrderItem.Rows(e.RowIndex).Cells("col_Plan_Process").Value.ToString = "9" Then
                        Dim pDocumentPlanItem_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value.ToString
                        Dim pSku_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value.ToString
                        Dim pPackage_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("cbo_Receive_Package").Value.ToString
                        Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                        Dim pRatio = objRatio.getRatio(pSku_Index, pPackage_Index)
                        Dim pQty As Decimal = 0
                        Dim objCheck As New tb_OrderItem()

                        If IsNumeric(grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value) Then
                            pQty = CDec(grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value)
                            Dim pTotalQty As Decimal = pQty * pRatio

                            If grdOrderItem.Rows(e.RowIndex).Cells("col_OrderItem_Index").Value IsNot Nothing AndAlso grdOrderItem.Rows(e.RowIndex).Cells("col_OrderItem_Index").Value.ToString <> "" AndAlso objStatus = enuOperation_Type.UPDATE Then
                                Dim strReturn As String = objCheck.CheckTotalQtyInPO(pTotalQty, pDocumentPlanItem_Index, grdOrderItem.Rows(e.RowIndex).Cells("col_OrderItem_Index").Value.ToString)
                                If Not String.IsNullOrEmpty(strReturn) Then
                                    W_MSG_Information(strReturn)
                                    grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = "0"
                                    Exit Sub
                                End If
                            Else
                                Dim strReturn As String = objCheck.CheckTotalQtyInPO(pTotalQty, pDocumentPlanItem_Index)
                                If Not String.IsNullOrEmpty(strReturn) Then
                                    W_MSG_Information(strReturn)
                                    grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = "0"
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If


                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value Is Nothing Then Exit Sub
                    If grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString = "" Then Exit Sub
                    Dim oChk_LocationAlias As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    If oChk_LocationAlias.Chk_Alias(grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString) = False Then
                        W_MSG_Information_ByIndex("300019")
                        grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value = _tmpLocation_Alias
                        Exit Sub
                    Else
                        If oChk_LocationAlias.ChkQty_Alias(grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value.ToString, _tmpTotalQty_PutAway) = False Then
                            W_MSG_Information_ByIndex("300020")
                            grdOrderItem.Rows(e.RowIndex).Cells("Col_location").Value = _tmpLocation_Alias
                            grdOrderItem.Rows(e.RowIndex).Cells("col_Qty").Value = _tmpTotalQty_PutAway
                        End If
                    End If

                    'pla add 05112012----------------
                Case Is = grdOrderItem.Columns("col_Volume").Index
                    'If IsNumeric(grdOrderItem.Rows(e.RowIndex).Cells("col_Volume_PerPackage").Value) Then
                    '    If Val(grdOrderItem.Rows(e.RowIndex).Cells("col_Volume_PerPackage").Value) > 0 Then
                    '        W_MSG_Information("Product Code : " & grdOrderItem.Rows(e.RowIndex).Cells("col_Sku_Id").Value & " : M3 ไม่ตรงกับค่ามาตรฐานที่กำหนด")
                    '    End If
                    'End If

                Case Is = grdOrderItem.Columns("CBO_RECEIVE_PACKAGE").Index
                    Dim strSku_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                    If strSku_Index IsNot Nothing Then
                        '  Dim obj As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                        Dim obj As New tb_Order
                        Dim dtDatat As New DataTable
                        Dim strPackage_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("CBO_RECEIVE_PACKAGE").Value
                        ' obj.getSKU_Ratio(strSku_Index, strPackage_Index)
                        obj.getSKU_Detail(strSku_Index, strPackage_Index)
                        dtDatat = obj.DataTable


                        If dtDatat.Rows.Count > 0 Then
                            grdOrderItem.Rows(e.RowIndex).Cells("col_Weight_PerPackage").Value = IIf(IsNumeric(dtDatat.Rows(0).Item("Weight")), dtDatat.Rows(0).Item("Weight"), 0)
                            grdOrderItem.Rows(e.RowIndex).Cells("col_Width_PerPackage").Value = IIf(IsNumeric(dtDatat.Rows(0).Item("Dimension_Wd")), dtDatat.Rows(0).Item("Dimension_Wd"), 0)
                            grdOrderItem.Rows(e.RowIndex).Cells("col_Long_PerPackage").Value = IIf(IsNumeric(dtDatat.Rows(0).Item("Dimension_Len")), dtDatat.Rows(0).Item("Dimension_Len"), 0)
                            grdOrderItem.Rows(e.RowIndex).Cells("col_Height_PerPackage").Value = IIf(IsNumeric(dtDatat.Rows(0).Item("Dimension_Hi")), dtDatat.Rows(0).Item("Dimension_Hi"), 0)
                            grdOrderItem.Rows(e.RowIndex).Cells("col_Volume_PerPackage").Value = IIf(IsNumeric(dtDatat.Rows(0).Item("Vol")), dtDatat.Rows(0).Item("Vol"), 0)
                            grdOrderItem.Rows(e.RowIndex).Cells("Col_DimensionType_Id").Value = dtDatat.Rows(0).Item("DimensionType_Id").ToString
                            grdOrderItem.Rows(e.RowIndex).Cells("Col_DimensionRatio").Value = IIf(IsNumeric(dtDatat.Rows(0).Item("Ratio_Dimension")), dtDatat.Rows(0).Item("Ratio_Dimension"), 0)

                            Calculate_Pck()
                        End If

                    End If
                Case Is = grdOrderItem.Columns("COL_MFG_DATE").Index
                    If grdOrderItem.Rows(e.RowIndex).Cells("chk_Mfg_Date").Value = True Then
                        Dim objCalDate As New CalculateDate
                        Dim Output_Date As DateTime
                        If Me.grdOrderItem.CurrentRow.Index <= -1 Then Exit Sub
                        Dim Sku_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                        If Sku_Index = "" Then
                            Exit Sub
                        End If
                        Output_Date = objCalDate.getExpDate_With_MfgDate(Sku_Index, grdOrderItem.Rows(e.RowIndex).Cells("col_Mfg_Date").Value)
                        grdOrderItem.Rows(e.RowIndex).Cells("col_Exp_Date").Value = Output_Date
                    End If
                Case Is = grdOrderItem.Columns("COL_EXP_DATE").Index
                    If grdOrderItem.Rows(e.RowIndex).Cells("chkExp_date").Value = True Then
                        Dim objCalDate As New CalculateDate
                        Dim Output_Date As DateTime
                        If Me.grdOrderItem.CurrentRow.Index <= -1 Then Exit Sub
                        Dim Sku_Index As String = grdOrderItem.Rows(e.RowIndex).Cells("col_SKU_Index").Value
                        If Sku_Index = "" Then
                            Exit Sub
                        End If
                        Output_Date = objCalDate.getMfgDate_With_ExpDate(Sku_Index, grdOrderItem.Rows(e.RowIndex).Cells("col_Exp_Date").Value)
                        grdOrderItem.Rows(e.RowIndex).Cells("col_Mfg_Date").Value = Output_Date
                    End If


            End Select

            Get_SumData()  'คำนวน sum ทั้งหมด

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    ''' <summary>
    ''' Check null data is numberic data
    ''' </summary>
    ''' <param name="pstrValue">Numberic data</param>
    ''' <returns>ture is numberic; false is not numberic</returns>
    ''' <remarks> Date 25/03/2010
    '''  Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง
    ''' </remarks>

    Private Function DataNumeric(ByVal pstrValue As String) As Boolean
        Try
            If pstrValue = "" Then
                Return False
            ElseIf CDbl(pstrValue) = "0" Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Function

    Function Calculate_Qty() As Double
        Try
            'Dim Qty_Pck_cal As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value ' จำนวนหีบห่อ
            Dim Item_Qty As Double = grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value
            Dim Qty_Per_Pck_cal As Double = grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value
            Dim Cal As Double = (Item_Qty / Qty_Per_Pck_cal)
            If Qty_Per_Pck_cal = 0 Then
                Cal = grdOrderItem.CurrentRow.Cells("col_Qty").Value
            End If

            Return Cal

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Calculate_Qty_PerPck() As Double
        Try
            Dim Qty_Pck_cal As Double = 0 ' จำนวนหีบห่อ
            Dim Item_Qty As Double = 0

            If grdOrderItem.CurrentRow.Cells("col_Qty").Value IsNot Nothing Then

                If grdOrderItem.CurrentRow.Cells("col_Qty").Value.ToString.Trim <> "" Then
                    Qty_Pck_cal = grdOrderItem.CurrentRow.Cells("col_Qty").Value
                End If
            End If

            If CDbl(grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value) = 0 Then
                grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value = Qty_Pck_cal
            End If
            Item_Qty = CDbl(grdOrderItem.CurrentRow.Cells("col_Item_Qty").Value)


            Dim Cal As Double = Item_Qty / Qty_Pck_cal
            If Qty_Pck_cal = 0 Then
                Cal = grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value
            End If

            Return Cal

        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function Calculate_Item_Qty() As Double
        Try
            Dim Total As Double

            Dim Qty_Pck_cal As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value

            If grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value = 0 Then
                grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value = 1
            End If

            Dim Qty_Per_Pck_cal As Double = grdOrderItem.CurrentRow.Cells("col_Qty_PerPackage").Value

            Total = Qty_Per_Pck_cal * Qty_Pck_cal

            Return Total

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Calculate_CBM_Total() As Double
        Try
            Dim W_cal As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdOrderItem.CurrentRow.Cells("col_Width_PerPackage").Value, GetType(Double)) 'grdOrderItem.CurrentRow.Cells("col_Width_PerPackage").Value
            Dim L_cal As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdOrderItem.CurrentRow.Cells("col_Long_PerPackage").Value, GetType(Double)) 'grdOrderItem.CurrentRow.Cells("col_Long_PerPackage").Value
            Dim H_cal As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdOrderItem.CurrentRow.Cells("col_Height_PerPackage").Value, GetType(Double)) 'grdOrderItem.CurrentRow.Cells("col_Height_PerPackage").Value
            'killz 30-04-2012
            Dim DimenstionRatio As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdOrderItem.CurrentRow.Cells("col_DimensionRatio").Value, GetType(Double))
            If DimenstionRatio = 0 Then DimenstionRatio = 1
            Dim CBM_PerPck As Double = ((W_cal * L_cal * H_cal) / DimenstionRatio) '((W_cal / DimenstionRatio) * (L_cal / DimenstionRatio) * (H_cal / DimenstionRatio))
            ' Dim CBM_PerPck As Double = (W_cal * L_cal * H_cal)

            Dim ItemNum As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdOrderItem.CurrentRow.Cells("col_Qty").Value, GetType(Double)) 'grdOrderItem.CurrentRow.Cells("col_Qty").Value

            'killz 30-04-2012
            Return Math.Round(CBM_PerPck * ItemNum, 4)
            'Dim Ratio As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(grdOrderItem.CurrentRow.Cells("col_Ratio").Value, GetType(Double)) 'grdOrderItem.CurrentRow.Cells("col_Height_PerPackage").Value
            'Return CBM_PerPck * ItemNum * Ratio
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Calculate_CBM_Total_Loop(ByVal iRow As Integer) As Double
        Try
            'top 13/03/2013
            Dim W_cal As Double = IIf(IsNumeric(grdOrderItem.Rows(iRow).Cells("col_Width_PerPackage").Value), grdOrderItem.Rows(iRow).Cells("col_Width_PerPackage").Value, 0)
            Dim L_cal As Double = IIf(IsNumeric(grdOrderItem.Rows(iRow).Cells("col_Long_PerPackage").Value), grdOrderItem.Rows(iRow).Cells("col_Long_PerPackage").Value, 0)
            Dim H_cal As Double = IIf(IsNumeric(grdOrderItem.Rows(iRow).Cells("col_Height_PerPackage").Value), grdOrderItem.Rows(iRow).Cells("col_Height_PerPackage").Value, 0)
            'killz 30-04-2012
            Dim DimenstionRatio As Double = grdOrderItem.Rows(iRow).Cells("col_DimensionRatio").Value
            If DimenstionRatio = 0 Then DimenstionRatio = 1
            Dim CBM_PerPck As Double = ((W_cal * L_cal * H_cal) / DimenstionRatio)

            Dim ItemNum As Double = grdOrderItem.Rows(iRow).Cells("col_Qty").Value
            'killz 30-04-2012
            Return Math.Round(CBM_PerPck * ItemNum, 4)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function Calculate_CBM_Loop(ByVal iRow As Integer) As Double
        Try
            Dim W_cal As Double = grdOrderItem.Rows(iRow).Cells("col_Width_PerPackage").Value
            Dim L_cal As Double = grdOrderItem.Rows(iRow).Cells("col_Long_PerPackage").Value
            Dim H_cal As Double = grdOrderItem.Rows(iRow).Cells("col_Height_PerPackage").Value
            'killz 30-04-2012
            Dim DimenstionRatio As Double = grdOrderItem.Rows(iRow).Cells("col_DimensionRatio").Value
            If DimenstionRatio = 0 Then DimenstionRatio = 1
            Dim CBM_PerPck As Double = ((W_cal / DimenstionRatio) * (L_cal / DimenstionRatio) * (H_cal / DimenstionRatio))


            Return Math.Round(CBM_PerPck, 4) '* ItemNum
        Catch ex As Exception
            Throw ex
        End Try

    End Function
    Function Calculate_Net_Pck() As Double
        Try
            Dim Qty As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value
            Dim Net As Double = grdOrderItem.CurrentRow.Cells("col_Weight_PerPackage").Value

            Return Net * Qty
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function Calculate_PRICE_TOTAL() As Double
        Try

            Dim Qty As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value
            Dim PriceItem As Double = grdOrderItem.CurrentRow.Cells("col_ItemPrice_PerPackage").Value

            Dim dblPRICE As Double = (Qty * PriceItem)

            Return dblPRICE '* ItemNum
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Calculate_PRICE() As Double
        Try

            Dim Qty As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value
            Dim PriceItem As Double = grdOrderItem.CurrentRow.Cells("col_ItemPrice").Value

            Dim dblPRICE As Double = PriceItem / Qty

            If Qty = 0 Then
                Return 0
            End If

            Return dblPRICE '* ItemNum
        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Function Calculate_CBM() As Decimal
        Try
            Dim W_cal As Double = grdOrderItem.CurrentRow.Cells("col_Width_PerPackage").Value
            Dim L_cal As Double = grdOrderItem.CurrentRow.Cells("col_Long_PerPackage").Value
            Dim H_cal As Double = grdOrderItem.CurrentRow.Cells("col_Height_PerPackage").Value

            'killz 30-04-2012
            Dim DimenstionRatio As Decimal = grdOrderItem.CurrentRow.Cells("col_DimensionRatio").Value
            If DimenstionRatio = 0 Then DimenstionRatio = 1
            Dim CBM_PerPck As Double = ((W_cal * L_cal * H_cal) / DimenstionRatio) '((W_cal / DimenstionRatio) * (L_cal / DimenstionRatio) * (H_cal / DimenstionRatio))
            ' Dim CBM_PerPck As Double = (W_cal * L_cal * H_cal)

            ' Dim ItemNum As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value

            Return CBM_PerPck 'Math.Round(CBM_PerPck, 4) '* ItemNum
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function Calculate_CBM_PerPck() As Double
        Try
            Dim Qty As Double = grdOrderItem.CurrentRow.Cells("col_Qty").Value
            Dim Weight As Double = grdOrderItem.CurrentRow.Cells("col_Weight").Value
            Dim CBM_PerPck As Double = Weight / Qty

            If Qty = 0 Then
                Return 0
            End If
            Return CBM_PerPck
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub Calculate_Pck()
        Try
            Dim Qty_Pck_cal As Decimal = 0
            Dim Vol_Per_Pck_cal As Decimal = 0
            Dim Net_Per_Pck_cal As Decimal = 0
            Dim Price_Per_Pck_cal As Decimal = 0
            Dim Vol_Total As Decimal = 0
            Dim DimensionRatio As Decimal = 0

            If grdOrderItem.CurrentRow.Cells("col_Volume").Value IsNot Nothing Then
                Vol_Total = grdOrderItem.CurrentRow.Cells("col_Volume").Value
            End If

            If grdOrderItem.CurrentRow.Cells("col_Qty").Value IsNot Nothing Then

                If Val(grdOrderItem.CurrentRow.Cells("col_Qty").Value) <> 0 Or grdOrderItem.CurrentRow.Cells("col_Qty").Value.ToString.Trim <> "" Then
                    Qty_Pck_cal = grdOrderItem.CurrentRow.Cells("col_Qty").Value ' จำนวนหีบห่อ
                End If

            End If

            If grdOrderItem.CurrentRow.Cells("col_Volume_PerPackage").Value IsNot Nothing Then
                Vol_Per_Pck_cal = grdOrderItem.CurrentRow.Cells("col_Volume_PerPackage").Value
            End If

            If grdOrderItem.CurrentRow.Cells("col_Weight_PerPackage").Value Then
                Net_Per_Pck_cal = grdOrderItem.CurrentRow.Cells("col_Weight_PerPackage").Value
            End If

            If grdOrderItem.CurrentRow.Cells("col_ItemPrice_PerPackage").Value Then
                Price_Per_Pck_cal = grdOrderItem.CurrentRow.Cells("col_ItemPrice_PerPackage").Value
            End If

            DimensionRatio = grdOrderItem.CurrentRow.Cells("Col_DimensionRatio").Value

            grdOrderItem.CurrentRow.Cells("col_Volume").Value = (IIf((Vol_Per_Pck_cal * Qty_Pck_cal) = 0, 0, (Vol_Per_Pck_cal * Qty_Pck_cal))).ToString
            grdOrderItem.CurrentRow.Cells("col_Weight").Value = ((Net_Per_Pck_cal * Qty_Pck_cal)).ToString
            grdOrderItem.CurrentRow.Cells("col_ItemPrice").Value = ((Price_Per_Pck_cal * Qty_Pck_cal)).ToString
            grdOrderItem.CurrentRow.Cells("col_Net_Weight").Value = ((Net_Per_Pck_cal * Qty_Pck_cal)).ToString

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Calculate_Pck(ByVal Row_Index As Integer)
        Try
            Dim Qty_Pck_cal As Double = 0
            Dim Vol_Per_Pck_cal As Double = 0
            Dim Net_Per_Pck_cal As Double = 0
            Dim Price_Per_Pck_cal As Double = 0
            Dim Vol_Total As Double = 0


            If grdOrderItem.Rows(Row_Index).Cells("col_Volume").Value IsNot Nothing Then
                Vol_Total = grdOrderItem.Rows(Row_Index).Cells("col_Volume").Value
            End If

            If grdOrderItem.Rows(Row_Index).Cells("col_Qty").Value IsNot Nothing Then

                If Val(grdOrderItem.Rows(Row_Index).Cells("col_Qty").Value) <> 0 Or grdOrderItem.Rows(Row_Index).Cells("col_Qty").Value.ToString.Trim <> "" Then
                    Qty_Pck_cal = grdOrderItem.Rows(Row_Index).Cells("col_Qty").Value ' จำนวนหีบห่อ
                End If

            End If

            If grdOrderItem.Rows(Row_Index).Cells("col_Volume_PerPackage").Value IsNot Nothing Then
                Vol_Per_Pck_cal = grdOrderItem.Rows(Row_Index).Cells("col_Volume_PerPackage").Value
            End If

            If grdOrderItem.Rows(Row_Index).Cells("col_Weight_PerPackage").Value Then
                Net_Per_Pck_cal = grdOrderItem.Rows(Row_Index).Cells("col_Weight_PerPackage").Value
            End If

            If grdOrderItem.Rows(Row_Index).Cells("col_ItemPrice_PerPackage").Value Then
                Price_Per_Pck_cal = grdOrderItem.Rows(Row_Index).Cells("col_ItemPrice_PerPackage").Value
            End If

            grdOrderItem.Rows(Row_Index).Cells("col_Volume").Value = (IIf((Vol_Per_Pck_cal * Qty_Pck_cal) = 0, Vol_Total, (Vol_Per_Pck_cal * Qty_Pck_cal))).ToString
            grdOrderItem.Rows(Row_Index).Cells("col_Weight").Value = ((Net_Per_Pck_cal * Qty_Pck_cal)).ToString
            grdOrderItem.Rows(Row_Index).Cells("col_ItemPrice").Value = ((Price_Per_Pck_cal * Qty_Pck_cal)).ToString
            grdOrderItem.Rows(Row_Index).Cells("col_Net_Weight").Value = ((Net_Per_Pck_cal * Qty_Pck_cal)).ToString

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub GetOrderImage(ByVal pstrOrder_Index As String)
        Dim odjOrderImage As New tb_Order_Image
        Try
            grdOrderImage.AutoGenerateColumns = False
            odjOrderImage.GetAllDataTable(" WHERE Order_Index = '" & pstrOrder_Index & "'")
            odtOrderImage = odjOrderImage.GetDataTable

            With grdOrderImage
                .DataSource = odtOrderImage
            End With
            If odtOrderImage.Rows.Count > 0 Then
                imgPreview.ImageLocation = odtOrderImage.Rows(0).Item("Image_Path").ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnAddPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPic.Click

        Try
            grdOrderImage.AutoGenerateColumns = False
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                imgPreview.ImageLocation = OpenFileDialog1.FileName
                _strLongFilePath = OpenFileDialog1.FileName

                Dim objDBIndex As New Sy_AutoNumber
                Dim StrOrder_Image_Index As String = ""
                StrOrder_Image_Index = objDBIndex.getSys_Value("Order_Image_Index")

                Dim str() As String
                str = OpenFileDialog1.FileName.Split(".")

                _strFileName = StrOrder_Image_Index & "." & str(str.Length - 1)

                '  _strPathName = Application.StartupPath
                _strPathName = _DEFAULT_ImagePath & _strFileName

                With odtOrderImage.Rows.Add
                    .Item("Order_Image_Index") = StrOrder_Image_Index
                    .Item("Image_Path") = _strPathName
                End With

                With grdOrderImage
                    .DataSource = odtOrderImage
                End With

                ' Add Image  To  Folder
                If _strLongFilePath <> "" Then
                    IO.File.Copy(_strLongFilePath, _strPathName, True)
                End If
                'Insert To tb_Order_Image
                InsertOrderImage(StrOrder_Image_Index, _strPathName)

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub InsertOrderImage(ByVal pstrOrder_Image_Index As String, ByVal pstrPathName As String)
        Dim odjOrderImage As New tb_Order_Image
        Try
            With odjOrderImage
                .Order_Image_Index = pstrOrder_Image_Index
                .Order_Index = _Order_Index
                .Image_Path = pstrPathName
                .Comment = ""
                .InsertOrderImage()
            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub btnRemovePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePic.Click

        Dim odjOrderImage As New tb_Order_Image
        Try
            If odtOrderImage.Rows.Count = 0 Then
                Exit Sub
            End If
            If Not Me.grdOrderImage.CurrentRow.Cells("Col_Order_Image_Index").Value = "" Then
                odjOrderImage.DeleteOrderImage(" WHERE Order_Image_Index = '" & Me.grdOrderImage.CurrentRow.Cells("Col_Order_Image_Index").Value & "'")
                IO.File.Delete(Me.grdOrderImage.CurrentRow.Cells("Col_Image_Name").Value)
                imgPreview.ImageLocation = ""
                Me.grdOrderImage.Rows.RemoveAt(grdOrderImage.CurrentRow.Index)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnRotatetion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRotatePic.Click
        Try
            If grdOrderImage.Rows.Count = 0 Then Exit Sub
            imgPreview.Image.RotateFlip(RotateFlipType.Rotate90FlipXY)
            imgPreview.Refresh()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdOrderImage_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderImage.CellClick
        Try
            If e.RowIndex <= -1 Then Exit Sub
            If Not Me.grdOrderImage.Rows(e.RowIndex).Cells("Col_Image_Path").Value Is Nothing Then
                imgPreview.ImageLocation = grdOrderImage.Rows(e.RowIndex).Cells("Col_Image_Path").Value
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPo_Item.Click
        Try
            If txtPlanReceive.Text = "" Then
                Exit Sub
            End If
            Select Case cboPlanReceive.SelectedValue
                Case 9
                    Me.GetPOItem()
                Case 7
                    Me.GetPACKINGItem()
                Case 16
                    Me.GetASNItem()
                Case 103
                    Me.GetShipmentItem()
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub GetPACKINGItem()
        Try
            If USE_PACKING_NEW_PRODUCTION = False Then

                ' 05-05-2010 OLD VERSION PACKING
                Dim oml_tb_Packing As New ml_tb_Packing
                Dim objDT As DataTable = New DataTable

                If Me._DocumentPlan_Index = "" Then
                    Exit Sub
                End If

                oml_tb_Packing.getView_Packing_Header(Me.txtPlanReceive.Tag, "")
                objDT = oml_tb_Packing.DataTable

                If objDT.Rows.Count < 1 Then
                    W_MSG_Information_ByIndex(300044)
                End If

                If objDT.Rows.Count > 0 Then
                    With grdOrderItem
                        For Each odr As DataRow In objDT.Rows
                            Dim i As Integer = grdOrderItem.Rows.Add
                            .Rows(i).Cells("col_Plan_Package").Tag = odr("Package_Index").ToString
                            .Rows(i).Cells("col_SKU_Id").Value = odr("Sku_Id").ToString
                            .Rows(i).Cells("ColumnPO_Item").Value = odr("Packing_Index").ToString
                            .Rows(i).Cells("col_PO_Number").Value = odr("Packing_No").ToString
                            .Rows(i).Cells("cbo_ItemStatus").Value = odr("ItemStatus_Index").ToString
                            .Rows(i).Cells("col_mfg_Date").Value = CDate(odr("mfg_Date").ToString).ToString("dd/MM/yyyy")
                            .Rows(i).Cells("col_Exp_Date").Value = CDate(odr("Exp_Date").ToString).ToString("dd/MM/yyyy")
                            .Rows(i).Cells("ColumnPO_Item").Value = odr("Packing_Index").ToString
                            .Rows(i).Cells("col_Plot").Value = odr("Plot").ToString

                            If odr("VolumeX").ToString <> "" Then
                                .Rows(i).Cells("col_Width_PerPackage").Value = odr("VolumeX").ToString
                            End If
                            If odr("VolumeY").ToString <> "" Then
                                .Rows(i).Cells("col_Long_PerPackage").Value = odr("VolumeY").ToString
                            End If
                            If odr("VolumeZ").ToString <> "" Then
                                .Rows(i).Cells("col_Height_PerPackage").Value = odr("VolumeZ").ToString
                            End If
                            If odr("Volume").ToString <> "" Then
                                .Rows(i).Cells("col_Volume_PerPackage").Value = CDbl(odr("VolumeX").ToString) * CDbl(odr("VolumeY").ToString) * CDbl(odr("VolumeZ").ToString)
                            End If
                            If odr("Weight").ToString <> "" Then
                                .Rows(i).Cells("col_Weight_PerPackage").Value = odr("Weight").ToString
                            End If

                            '.Rows(i).Cells("col_Plan_Qty").Value = odr("Qty_Product").ToString
                            .Rows(i).Cells("col_Qty").Value = odr("Qty_Product").ToString
                            .Rows(i).Cells("col_Qty_PerPackage").Value = 1
                            .Rows(i).Cells("col_Item_Qty").Value = odr("Qty_Product").ToString
                            .Rows(i).Cells("col_Volume").Value = odr("Volume").ToString
                            .Rows(i).Cells("col_Weight").Value = odr("Weight_Sum").ToString


                            .Rows(i).Cells("col_Plan_Process").Value = 7
                            .Rows(i).Cells("col_DocumentPlan_Index").Value = odr("Packing_Index").ToString
                            .Rows(i).Cells("col_DocumentPlan_No").Value = odr("Packing_No").ToString
                            .Rows(i).Cells("col_DocumentPlanItem_Index").Value = ""
                            If odr("Package_Index").ToString <> "" Then
                                .Rows(i).Cells("cbo_Receive_Package").Value = odr("Package_Index").ToString
                            End If


                            'grdOrderItem.Rows(i).Cells("col_Qty").Value = odr("Qty_Bal").ToString
                            grdOrderItem.Rows(i).Cells("col_Qty").Value = odr("Qty_Product").ToString
                            Me.txtPlanReceive.Text = ""


                        Next
                    End With

                End If


            Else

                ' 05-05-2010 New  VERSION PACKING
                Dim oml_tb_Packing As New ml_tb_Packing
                Dim objDT As DataTable = New DataTable

                If Me._DocumentPlan_Index = "" Then
                    Exit Sub
                End If
                oml_tb_Packing.getVIEW_Packing_Order(Me.txtPlanReceive.Tag, "")
                objDT = oml_tb_Packing.DataTable

                If objDT.Rows.Count < 1 Then
                    W_MSG_Information_ByIndex(300044)
                End If

                If objDT.Rows.Count > 0 Then
                    With grdOrderItem
                        For Each odr As DataRow In objDT.Rows
                            Dim i As Integer = grdOrderItem.Rows.Add
                            .Rows(i).Cells("col_Plan_Package").Tag = odr("Package_Index").ToString
                            .Rows(i).Cells("col_SKU_Id").Value = odr("Sku_Id").ToString
                            .Rows(i).Cells("ColumnPO_Item").Value = odr("Packing_Index").ToString
                            .Rows(i).Cells("col_PO_Number").Value = odr("Packing_No").ToString
                            .Rows(i).Cells("cbo_ItemStatus").Value = odr("ItemStatus_Index").ToString
                            .Rows(i).Cells("col_mfg_Date").Value = CDate(odr("mfg_Date").ToString).ToString("dd/MM/yyyy")
                            .Rows(i).Cells("col_Exp_Date").Value = CDate(odr("Exp_Date").ToString).ToString("dd/MM/yyyy")
                            .Rows(i).Cells("ColumnPO_Item").Value = odr("Packing_Index").ToString
                            .Rows(i).Cells("col_Plot").Value = odr("Plot").ToString

                            'If odr("VolumeX").ToString <> "" Then
                            '    .Rows(i).Cells("col_Width_PerPackage").Value = odr("VolumeX").ToString
                            'End If
                            'If odr("VolumeY").ToString <> "" Then
                            '    .Rows(i).Cells("col_Long_PerPackage").Value = odr("VolumeY").ToString
                            'End If
                            'If odr("VolumeZ").ToString <> "" Then
                            '    .Rows(i).Cells("col_Height_PerPackage").Value = odr("VolumeZ").ToString
                            'End If
                            'If odr("Volume").ToString <> "" Then
                            '    .Rows(i).Cells("col_Volume_PerPackage").Value = Val(odr("VolumeX").ToString) * Val(odr("VolumeY").ToString) * Val(odr("VolumeZ").ToString)
                            'End If
                            'If odr("Weight").ToString <> "" Then
                            '    .Rows(i).Cells("col_Weight_PerPackage").Value = odr("Weight").ToString
                            'End If

                            '.Rows(i).Cells("col_Plan_Qty").Value = odr("Qty_Product").ToString
                            .Rows(i).Cells("col_Qty").Value = odr("Qty_Product").ToString
                            .Rows(i).Cells("col_Qty_PerPackage").Value = 1
                            .Rows(i).Cells("col_Item_Qty").Value = odr("Qty_Product").ToString
                            '.Rows(i).Cells("col_Volume").Value = odr("Volume").ToString
                            '.Rows(i).Cells("col_Weight").Value = odr("Weight_Sum").ToString


                            .Rows(i).Cells("col_Plan_Process").Value = 7
                            .Rows(i).Cells("col_DocumentPlan_Index").Value = odr("Packing_Index").ToString
                            .Rows(i).Cells("col_DocumentPlan_No").Value = odr("Packing_No").ToString
                            .Rows(i).Cells("col_DocumentPlanItem_Index").Value = ""
                            .Rows(i).Cells("cbo_Receive_Package").Value = odr("Package_Index").ToString

                            '  grdOrderItem.Rows(i).Cells("col_Qty").Value = odr("Total_Qty").ToString
                            Me.txtPlanReceive.Text = ""


                        Next
                    End With

                End If

            End If
        Catch ex As Exception
            Throw ex
        Finally

        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks> 
    ''' Updated by: Dong_kk - 9 Jan 2010 - Update Code / Add New column name (col_Plan_Package) and add new column VIEW_ASN_Received  (Package_Des)
    ''' -------------------------------------------------
    ''' Update Date : 20/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Ducument Process / col_Plan_Process,col_DocumentPlan_Index,col_DocumentPlanItem_Index
    ''' </remarks>
    ''' 
    Sub GetASNItem()
        Dim oml_ASN As New ml_Receive_ASN
        Dim objDT As DataTable = New DataTable

        Try
            If oml_ASN.CheckGetASNReceive(Me._DocumentPlan_Index) Then
                Exit Sub
            End If
            oml_ASN.getAdvanceShipNotice_No(Me._DocumentPlan_Index)
            objDT = oml_ASN.DataTable


            If objDT.Rows.Count > 0 Then
                'GetHader
                Me.GetASNHeader(Me._DocumentPlan_Index)
                'Get Detail
                For Each odr As DataRow In objDT.Rows
                    Dim i As Integer = grdOrderItem.Rows.Add
                    With grdOrderItem.Rows(i)
                        .Cells("col_mfg_Date").Value = Now.ToString("dd/MM/yyyy")
                        .Cells("col_Exp_Date").Value = Now.ToString("dd/MM/yyyy")
                        .Cells("col_SKU_Id").Value = odr("Sku_Id").ToString
                        ' *** set ReadOnly *** 
                        .Cells("col_SKU_Id").ReadOnly = True

                        'Document
                        .Cells("col_Plan_Process").Value = 16
                        .Cells("col_DocumentPlan_Index").Value = odr("AdvanceShipNotice_Index").ToString
                        .Cells("col_DocumentPlan_No").Value = odr("AdvanceShipNotice_No").ToString
                        .Cells("col_DocumentPlanItem_Index").Value = odr("AdvanceShipNoticeItem_Index").ToString
                        .Cells("ColumnPO_Item").Value = odr("AdvanceShipNotice_Index").ToString
                        .Cells("col_PO_Number").Value = odr("AdvanceShipNotice_No").ToString
                        .Cells("col_Plan_Process").Value = 16
                        .Cells("col_Declaration_No").Value = odr("str7").ToString 'Notifi
                        .Cells("col_Invoice").Value = odr("str8").ToString
                        .Cells("col_Reference").Value = odr("Ref_NO1").ToString
                        .Cells("col_Reference2").Value = odr("Ref_NO2").ToString
                        .Cells("col_Coment").Value = odr("Remark").ToString
                        'Item
                        .Cells("cbo_Receive_Package").Value = odr("Package_Index").ToString
                        .Cells("col_Plan_Package").Tag = odr("Package_Index").ToString
                        .Cells("col_Plan_Package").Value = odr("Package_Des").ToString
                        .Cells("Col_location").Value = odr("Location_Alias").ToString
                        .Cells("col_ItemPrice_PerPackage").Value = FormatNumber(odr("UnitPrice"), 2)
                        .Cells("col_Plot").Value = odr("Plot").ToString
                        .Cells("col_HS_Code").Value = odr("Ref_No2").ToString
                        .Cells("col_Pallet_No").Value = odr("Pallet_No").ToString
                        'If odr("Picking_Type").ToString = 3 Then
                        '    .Cells("chkExp_date").Value = 1
                        'Else
                        '    .Cells("chkExp_date").Value = 0
                        'End If
                        .Cells("col_Seq").Value = odr("Item_Seq")


                        'Qty
                        .Cells("col_Item_Qty").Value = odr("Qty_Bal") 'FormatNumber(odr("Qty_Bal"), 2)
                        .Cells("col_Qty").Value = odr("Qty") 'FormatNumber(odr("Qty_Bal"), 2) for timco
                        .Cells("col_Plan_Qty").Value = odr("Qty_Bal") 'FormatNumber(odr("Qty_Bal"), 2)
                        'Value
                        .Cells("col_Qty_PerPackage").Value = 1
                        .Cells("col_Weight_PerPackage").Value = FormatNumber(odr("Weight_Bal") / odr("Qty_Bal"), 4) 'น้ำหนัก
                        .Cells("col_Volume_PerPackage").Value = FormatNumber(odr("Volume_Bal") / odr("Qty_Bal"), 4) 'ปริมาตร
                        .Cells("col_ItemPrice_PerPackage").Value = FormatNumber(odr("Net_weight_Bal") / odr("Qty_Bal"), 4) 'Net Weight

                        .Cells("col_Weight").Value = FormatNumber(odr("Weight_Bal"), 4)
                        .Cells("Col_Volume").Value = FormatNumber(odr("Volume_Bal"), 4)
                        .Cells("col_ItemPrice").Value = FormatNumber(odr("Net_weight_Bal"), 4) 'FormatNumber(odr("Amount"), 2)

                        .Cells("col_Net_Weight").Value = FormatNumber(odr("Net_weight_Bal"), 4)


                        'Calculate_Pck()
                    End With
                Next

                oml_ASN.UpdateStatusReceive(Me._DocumentPlan_Index, -2) 'Update Status

            End If


        Catch ex As Exception
            Throw ex
        Finally
            oml_ASN = Nothing
            objDT = Nothing
        End Try
    End Sub

    Sub GetASNHeader(ByVal pstrAdvanceShipNotice_Index As String)
        Try
            Dim oml_ASN As New ml_Receive_ASN
            Dim AddWhereString As String = " AND AdvanceShipNotice_Index = '" & pstrAdvanceShipNotice_Index & "'"
            oml_ASN.getVIEW_ASN_ReceivedHeader(AddWhereString)
            Dim odt As New DataTable
            odt = oml_ASN.DataTable
            If odt.Rows.Count = 0 Then Exit Sub
            Dim odr As DataRow = odt.Rows(0)

            '************************* BEGIN : Get Popup Person *************************
            Me.txtPlanReceive.Tag = odr("AdvanceShipNotice_No").ToString
            Me.txtPlanReceive.Text = odr("AdvanceShipNotice_No").ToString

            'Customer
            Me._Customer_Index = odr("Customer_Index").ToString
            If (Not odr("Customer_Index").ToString = "") Then getCustomer()
            If (Not odr("Customer_Index").ToString = "") Then getCustomerContact(Me._Customer_Index)
            'Carrier_ID
            'Me.txtCarrier_ID.Tag = odr("Carrier_Index").ToString
            'If (Not odr("Carrier_Index").ToString = "") Then Me.getCarrier()
            'Customer_Receive_Location
            'Me.txtSupplier_Id.Tag = odr("Customer_Receive_Location_Index").ToString
            'If (Not odr("Customer_Receive_Location_Index").ToString = "") Then Me.getCus_Shipping_Location_Index()
            'Departmen
            Me.txtDepartment_Id.Tag = odr("Department_Index").ToString
            If (Not odr("Department_Index").ToString = "") Then Me.getDepartment()
            'Customer_Shipping
            Me.txtConsignee_ID.Tag = odr("Customer_Shipping_Index").ToString
            Me.txtConsignee_ID.Text = odr("Customer_Shipping_ID").ToString
            Me.txtConsignee_Name.Text = odr("Company_Name").ToString
            'Checker
            Me.txtChecker_Name.Text = odr("Checker_Name").ToString
            '************************* END : Get Popup Person *************************
            'Header
            Me.cboPlanReceive.SelectedValue = 16
            'Me.dtOrder.Value = CDate(odr("AdvanceShipNotice_Date").ToString)
            Me.dtOrder.Value = CDate(Now()).ToString("dd/MM/yyyy")
            If IsDate(odr("Departure_Date").ToString) Then
                dtpDeparture_Date.Text = CDate(odr("Departure_Date").ToString).ToString("dd/MM/yyyy")
            End If
            If IsDate(odr("Arrival_Date").ToString) Then
                dtpArrival_Date.Text = CDate(odr("Arrival_Date").ToString).ToString("dd/MM/yyyy")
            End If
            'Me.Use_car = odr("Use_car").ToString
            Me.cboDocumentType.Text = odr("DocumentType_Desc").ToString
            If Me.cboDocumentType.SelectedValue Is Nothing Then Me.cboDocumentType.SelectedIndex = 0

            Me.txtASN_No.Text = odr("AdvanceShipNotice_No").ToString
            Me.txtPlanReceive.Text = odr("AdvanceShipNotice_No").ToString
            Me.txtComment.Text = odr("Comment").ToString
            Me.txtRef_No1.Text = odr("Str1").ToString
            Me.txtRef_No2.Text = odr("Str2").ToString
            Me.txtRef_No3.Text = odr("Str3").ToString
            Me.txtRef_No4.Text = odr("Str4").ToString
            Me.txtRef_No5.Text = odr("Str5").ToString
            Me.txtStr5.Text = odr("Str6").ToString
            Me.txtLot_No.Text = odr("Str9").ToString 'Str9 ของ ASN  บอกว่ามาจาก EDI
            'Port and Location

            Me.txtFlight_No.Text = odr("Flight_No").ToString
            Me.txtTransport_by.Text = odr("Transport_by").ToString
            Me.txtOrigin_Port.Text = odr("Origin_Port_Id").ToString
            Me.txtDestination_Port.Text = odr("Destination_Port_Id").ToString
            Me.txtOrigin_Country.Text = odr("Origin_Country_Id").ToString
            Me.txtDestination_Country.Text = odr("Destination_Country_Id").ToString
            Me.txtTerminal.Text = odr("Terminal_Id").ToString
            'If odr("Mobile").ToString = "1" And odr("Str9").ToString = "EDI" Then
            '    If odr("EDI_Not_Gen_XP_Req").ToString = "0" Then 'And odr("EDI_Gen_XP_Req").ToString = "1"
            '        ChkGXP.Checked = True
            '    ElseIf odr("EDI_Not_Gen_XP_Req").ToString = "1" And odr("EDI_Gen_XP_Req").ToString <> "1" Then
            '        Chk945.Checked = True
            '    End If
            'End If
            Me.txtVessel_Name.Text = odr("Vassel_Name").ToString
            'Me.cbProductGroup.SelectedValue = odr("Epson_ProductGroup_Index").ToString 'Vassel_Name ใช้เก็บ product_group_index ของ import EDI
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub GetShipmentItem()
        Dim oml_Shipment As New ml_Receive_Shipment
        Dim objDT As DataTable = New DataTable

        Try
            oml_Shipment.getShipment_No(Me.txtPlanReceive.Text)
            objDT = oml_Shipment.DataTable

            oml_Shipment.UpdateStatusReceive(Me.txtPlanReceive.Text, -2)

            If objDT.Rows.Count < 1 Then
                W_MSG_Information_ByIndex(300043)
            End If

            If objDT.Rows.Count > 0 Then
                For Each odr As DataRow In objDT.Rows
                    Dim i As Integer = grdOrderItem.Rows.Add
                    grdOrderItem.Rows(i).Cells("ColumnPO_Item").Value = odr("Shipment_Index").ToString
                    grdOrderItem.Rows(i).Cells("col_PO_Number").Value = odr("Shipment_No").ToString
                    grdOrderItem.Rows(i).Cells("col_Plan_Package").Tag = odr("Package_Index").ToString
                    grdOrderItem.Rows(i).Cells("col_SKU_Id").Value = odr("Sku_Id").ToString
                    grdOrderItem.Rows(i).Cells("col_Plan_Qty").Value = odr("Total_Qty").ToString
                    grdOrderItem.Rows(i).Cells("col_mfg_Date").Value = Now.ToString("dd/MM/yyyy")
                    grdOrderItem.Rows(i).Cells("col_Exp_Date").Value = Now.ToString("dd/MM/yyyy")

                    grdOrderItem.Rows(i).Cells("col_Qty_PerPackage").Value = 1
                    grdOrderItem.Rows(i).Cells("col_Item_Qty").Value = odr("Total_Qty").ToString

                    grdOrderItem.Rows(i).Cells("col_Qty").Value = odr("Total_Qty").ToString
                    grdOrderItem.Rows(i).Cells("cbo_Receive_Package").Value = odr("Package_Index").ToString

                    grdOrderItem.Rows(i).Cells("col_Plan_Process").Value = 103
                    grdOrderItem.Rows(i).Cells("col_DocumentPlan_Index").Value = odr("Shipment_Index").ToString
                    grdOrderItem.Rows(i).Cells("col_DocumentPlan_No").Value = odr("Shipment_No").ToString
                    grdOrderItem.Rows(i).Cells("col_DocumentPlanItem_Index").Value = odr("ShipmentReceived_Index").ToString
                Next

            End If


        Catch ex As Exception
            Throw ex
        Finally
            oml_Shipment = Nothing
            objDT = Nothing
        End Try
    End Sub

#Region " getPOItem "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Dong_kk - 9 Jan 2010 - Update Code / Add New column name (col_Plan_Package) 
    ''' --------------------------
    ''' Update Date : 20/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Ducument Process / col_Plan_Process,col_DocumentPlan_Index,col_DocumentPlanItem_Index
    ''' </remarks>
    Sub GetPOItem()
        Dim oml_Receive_PO As New ml_Receive_PO
        Dim objDT As DataTable = New DataTable

        Try
            oml_Receive_PO.GetPOItem_V2(Me.txtPlanReceive.Text)
            objDT = oml_Receive_PO.DataTable

            oml_Receive_PO.UpdatePOItem(Me.txtPlanReceive.Text, -2)

            If objDT.Rows.Count < 1 Then
                W_MSG_Information_ByIndex(300043)
            End If

            If objDT.Rows.Count > 0 Then

                If Not objDT.Rows(0).Item("Supplier_Index").ToString = "" Then
                    Me.txtSupplier_Id.Tag = objDT.Rows(0).Item("Supplier_Index").ToString
                    Me.getSupplier()
                End If

                If Not objDT.Rows(0).Item("Customer_Index").ToString = "" Then
                    Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                    Me.getCustomer()
                End If
                Me.txtRef_No1.Text = objDT.Rows(0).Item("PurchaseOrder_No").ToString

                For Each odr As DataRow In objDT.Rows
                    Dim i As Integer = grdOrderItem.Rows.Add

                    grdOrderItem.Rows(i).Cells("ColumnPO_Item").Value = odr("PurchaseOrder_Index").ToString
                    grdOrderItem.Rows(i).Cells("col_PO_Number").Value = odr("PurchaseOrder_No").ToString

                    grdOrderItem.Rows(i).Cells("col_Plan_Qty").Value = odr("Total_Qty") / odr("Ratio")
                    grdOrderItem.Rows(i).Cells("col_Qty_PerPackage").Value = 1 ' odr("Qty").ToString
                    grdOrderItem.Rows(i).Cells("col_Item_Qty").Value = odr("Total_Qty") / odr("Ratio")
                    grdOrderItem.Rows(i).Cells("col_Qty").Value = odr("Total_Qty") / odr("Ratio")
                    grdOrderItem.Rows(i).Cells("col_ItemPrice_PerPackage").Value = odr("UnitPrice").ToString
                    grdOrderItem.Rows(i).Cells("col_ItemPrice").Value = odr("Amount").ToString

                    grdOrderItem.Rows(i).Cells("col_SKU_Id").Value = odr("Sku_Id").ToString
                    grdOrderItem.Rows(i).Cells("col_Plan_Package").Tag = odr("Package_Index").ToString
                    grdOrderItem.Rows(i).Cells("cbo_Receive_Package").Value = odr("Package_Index").ToString
                    grdOrderItem.Rows(i).Cells("col_Plan_Package").Value = odr("Package_DES").ToString
                    grdOrderItem.Rows(i).Cells("col_mfg_Date").Value = Now.ToString("dd/MM/yyyy")
                    grdOrderItem.Rows(i).Cells("col_Exp_Date").Value = Now.ToString("dd/MM/yyyy")

                    grdOrderItem.Rows(i).Cells("col_Plan_Process").Value = 9
                    grdOrderItem.Rows(i).Cells("col_DocumentPlan_Index").Value = odr("PurchaseOrder_Index").ToString
                    grdOrderItem.Rows(i).Cells("col_DocumentPlan_No").Value = odr("PurchaseOrder_No").ToString
                    grdOrderItem.Rows(i).Cells("col_DocumentPlanItem_Index").Value = odr("PurchaseOrderItem_Index").ToString

                    CalcualateDate_In_Gridview(i)

                    Me.txtPlanReceive.Text = ""
                Next
            End If


        Catch ex As Exception
            Throw ex
        Finally
            oml_Receive_PO = Nothing
            objDT = Nothing
        End Try
    End Sub
#End Region

    Private Sub txtPo_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlanReceive.KeyPress


    End Sub

    Private Sub btnPopUp_PO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopUp_PO.Click

        Try
            If Me._Customer_Index = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If

            Select Case cboPlanReceive.SelectedValue
                Case 2
                    If String.IsNullOrEmpty(Me._Order_Index) Then Throw New Exception("Order index has not found")
                    Dim frm As New frmWithdrawSelected
                    frm.Customer_Index = Me.CustomerIndex
                    frm.ShowDialog()
                    If frm.isSelect Then
                        Me.setDataWithdraw(frm.drReturn)
                    End If
                    frm.Dispose()

                Case 9
                    Dim frm As New WMS_STD_INB_Receive.frmPopUp_PO
                    frm.Customer_Index = Me._Customer_Index.ToString
                    frm.ShowDialog()
                    '    *** Recive value ****
                    Dim tmpOrder_No As String = ""
                    tmpOrder_No = frm.Po_No 'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                    If Not tmpOrder_No = "" Then
                        Me.txtPlanReceive.Text = tmpOrder_No
                        Me.txtPlanReceive.Tag = frm.Po_Index

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag

                        Me.getPO_PopUp()
                    Else
                        Me.txtPlanReceive.Text = ""
                        Me.txtPlanReceive.Tag = ""

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag

                    End If
                    ' *********************
                    frm.Close()
                Case 7
                    Dim frm As New WMS_STD_INB_Receive.frmPackingBom_Popup
                    frm.USE_PACKING_NEW_PRODUCTION = Me._USE_PACKING_NEW_PRODUCTION
                    'frm.Customer_Index = Me._Customer_Index.ToString
                    frm.ShowDialog()
                    If frm.Packing_Index <> "" Then
                        txtPlanReceive.Tag = frm.Packing_Index
                        txtPlanReceive.Text = frm.Packing_ID

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag
                    Else
                        txtPlanReceive.Tag = ""
                        txtPlanReceive.Text = ""

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag
                    End If

                    frm.Close()

                Case 16
                    Dim frm As New WMS_STD_INB_Receive.frmPopUp_ASN
                    frm.Customer_Index = Me._Customer_Index.ToString
                    frm.ShowDialog()
                    If frm.ASN_Index <> "" Then
                        txtPlanReceive.Tag = frm.ASN_Index
                        txtPlanReceive.Text = frm.ASN_No

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag


                        txtCustomer_Id.Text = frm.Customer_ID
                        Me._Customer_Index = frm.Customer_Index
                        txtCustomer_Name.Text = frm.Customer_Name
                        Me._Customer_Index = frm.Customer_Index
                        getCustomer()
                        Me.txtSupplier_Id.Tag = frm.Supplier_Index
                        Me.getSupplier()
                    Else
                        txtPlanReceive.Tag = ""
                        txtPlanReceive.Text = ""

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag
                    End If

                    frm.Close()
                Case 103
                    Dim frm As New WMS_STD_INB_Receive.frmPopup_Shipment
                    frm.Customer_Index = Me._Customer_Index.ToString
                    frm.ShowDialog()
                    If frm.Shipment_Index <> "" Then
                        txtPlanReceive.Tag = frm.Shipment_Index
                        txtPlanReceive.Text = frm.Shipment_No

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag

                        txtCustomer_Id.Text = frm.Customer_ID
                        Me._Customer_Index = frm.Customer_Index
                        txtCustomer_Name.Text = frm.Customer_Name
                        Me._Customer_Index = frm.Customer_Index
                        getCustomer()
                        Me.txtSupplier_Id.Tag = frm.Supplier_Index
                        Me.getSupplier()
                    Else

                        txtPlanReceive.Tag = ""
                        txtPlanReceive.Text = ""

                        Me._DocumentPlan_No = Me.txtPlanReceive.Text
                        Me._DocumentPlan_Index = Me.txtPlanReceive.Tag
                    End If
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub setDataWithdraw(ByVal drr() As DataRow)
        Try
            For Each dr As DataRow In drr
                With Me.grdOrderItem
                    Dim Row_Index As Integer = .Rows.Add()
                    .Rows(Row_Index).Cells("col_Qty").Value = dr("Qty")
                    .Rows(Row_Index).Cells("col_SKU_Id").Value = dr("Sku_Id").ToString()
                    .Rows(Row_Index).Cells("col_Plot").Value = dr("PLot").ToString()
                    .Rows(Row_Index).Cells("col_Plan_Process").Value = 2
                    .Rows(Row_Index).Cells("col_DocumentPlan_No").Value = dr("Withdraw_No").ToString()
                    .Rows(Row_Index).Cells("col_DocumentPlan_Index").Value = dr("Withdraw_Index").ToString()
                    .Rows(Row_Index).Cells("col_DocumentPlanItem_Index").Value = dr("WithdrawItem_Index").ToString()
                    .Rows(Row_Index).Cells("cbo_Receive_Package").Value = dr("Package_Index").ToString()
                    .Rows(Row_Index).Cells("col_Pallet_No").Value = dr("Tag_No").ToString()
                    .Rows(Row_Index).Cells("chk_Mfg_Date").Value = dr("IsMfg_Date")
                    .Rows(Row_Index).Cells("Col_Mfg_Date").Value = Format(CDate(dr("Mfg_Date").ToString()), "dd/MM/yyyy").ToString
                    .Rows(Row_Index).Cells("chkExp_date").Value = dr("IsExp_Date")
                    .Rows(Row_Index).Cells("col_Exp_Date").Value = Format(CDate(dr("Exp_Date").ToString()), "dd/MM/yyyy").ToString
                End With
            Next
        Catch ex As Exception
            W_Language.W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getPO_PopUp()
        Dim oml_Receive_PO As New ml_Receive_PO
        Dim DTtb_Order As DataTable = New DataTable

        Try

            oml_Receive_PO.getPurchaseOrder_No(txtPlanReceive.Text)
            DTtb_Order = oml_Receive_PO.DataTable
            If DTtb_Order.Rows.Count > 0 Then
                Me.txtPlanReceive.Text = DTtb_Order.Rows(0).Item("PurchaseOrder_No").ToString
            Else
                Me.txtPlanReceive.Text = ""

            End If

        Catch ex As Exception
            Throw ex
        Finally
            oml_Receive_PO = Nothing
            DTtb_Order = Nothing
        End Try
    End Sub

    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click

        Dim oconfig_Report As New WMS_STD_Master.config_Report 'config_Report_INH
        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
        Try

            Dim frm As New frmReportViewerMain
            Dim oReport As New Loading_Report(Report_Name, "And Order_Index ='" & _Order_Index & "'")
            frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
        End Try

    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : Key In Tax
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdOrderItem.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdOrderItem.Columns("col_Item_Qty").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Qty_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Qty").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Weight").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Net_Weight").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Weight_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Width_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Height_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Long_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Volume_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_Volume").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_ItemPrice").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("col_ItemPrice_PerPackage").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)

                Case Is = grdOrderItem.Columns("Col_Tax1").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("Col_Tax2").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("Col_Tax3").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("Col_Tax4").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdOrderItem.Columns("Col_Tax5").Index
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
                            If grdOrderItem.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdOrderItem.CurrentRow.Cells(Column_Index).EditedFormattedValue
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

    Private Sub btnAdd_TruckIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_TruckIn.Click
        Try
            Dim frm As New WMS_STD_INB_Receive.frmOrderTruckIn(WMS_STD_INB_Receive.frmOrderTruckIn.enuOperation_Type.ADDNEW)
            frm.Order_index = Me._Order_Index
            frm.ShowDialog()
            getOrderTruckInDetail(Order_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getOrderTruckInDetail(ByVal Order_Index As String)
        Try
            Dim objOrderTruckIn As New tb_OrderTruckIn(tb_OrderTruckIn.enuOperation_Type.SEARCH)
            Dim objDTOrderTruckIn As New DataTable
            objOrderTruckIn.getOrderTruckInDetail(Order_Index)
            objDTOrderTruckIn = objOrderTruckIn.DataTable

            grdOrderTruckIn.Rows.Clear()
            For i As Integer = 0 To objDTOrderTruckIn.Rows.Count - 1
                With Me.grdOrderTruckIn
                    .Rows.Add()
                    .Rows(i).Cells("ColOrderTruckIn_Index").Value = objDTOrderTruckIn.Rows(i).Item("OrderTruckIn_Index").ToString
                    .Rows(i).Cells("ColContainer_Size").Value = objDTOrderTruckIn.Rows(i).Item("Container_Size").ToString
                    .Rows(i).Cells("ColContainer_No").Value = objDTOrderTruckIn.Rows(i).Item("Container_No").ToString
                    .Rows(i).Cells("ColContainer_No").Tag = objDTOrderTruckIn.Rows(i).Item("Container_Index").ToString
                    .Rows(i).Cells("ColDriver_Name").Value = objDTOrderTruckIn.Rows(i).Item("Driver_Name").ToString
                    .Rows(i).Cells("ColVehicle_No").Value = objDTOrderTruckIn.Rows(i).Item("Vehicle_No").ToString
                    .Rows(i).Cells("ColVehicle_Type").Value = objDTOrderTruckIn.Rows(i).Item("VehicleType").ToString
                    .Rows(i).Cells("ColVehicle_Type").Tag = objDTOrderTruckIn.Rows(i).Item("VehicleType_Index").ToString
                    .Rows(i).Cells("ColTime_Finish").Value = objDTOrderTruckIn.Rows(i).Item("Time_FinistLoad").ToString
                    .Rows(i).Cells("ColTime_Ingate").Value = objDTOrderTruckIn.Rows(i).Item("Time_Ingate").ToString
                    .Rows(i).Cells("ColTime_Outgate").Value = objDTOrderTruckIn.Rows(i).Item("Time_Outgate").ToString
                    .Rows(i).Cells("ColTime_Start").Value = objDTOrderTruckIn.Rows(i).Item("Time_StartLoad").ToString
                    .Rows(i).Cells("ColTransport_From").Value = objDTOrderTruckIn.Rows(i).Item("Transport_From").ToString
                    .Rows(i).Cells("ColTransport_To").Value = objDTOrderTruckIn.Rows(i).Item("Transport_To").ToString

                End With
            Next
            grdOrderTruckIn.Update()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnEdit_TruckIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_TruckIn.Click
        Try
            Dim frm As New WMS_STD_INB_Receive.frmOrderTruckIn(WMS_STD_INB_Receive.frmOrderTruckIn.enuOperation_Type.UPDATE)
            If grdOrderTruckIn.RowCount = 0 Then
                Exit Sub
            End If
            Dim strOrderTruckIn_Index As String = grdOrderTruckIn.CurrentRow.Cells("ColOrderTruckIn_Index").Value.ToString
            frm.Order_index = Me._Order_Index
            frm.OrderTruckIn_Index = strOrderTruckIn_Index
            frm.ShowDialog()
            getOrderTruckInDetail(_Order_Index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_TruckIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete_TruckIn.Click
        Try
            If grdOrderTruckIn.RowCount <= 0 Then
                Exit Sub
            End If
            Dim a As Integer = grdOrderTruckIn.CurrentRow.Index
            If W_MSG_Confirm_ByIndex(100004) = Windows.Forms.DialogResult.Yes Then
                Dim objSaveOrderTruckIn As New tb_OrderTruckIn(tb_OrderTruckIn.enuOperation_Type.DELETE)
                objSaveOrderTruckIn.OrderTruckIn_Index = grdOrderTruckIn.CurrentRow.Cells("ColOrderTruckIn_Index").Value.ToString
                objSaveOrderTruckIn = New tb_OrderTruckIn(tb_OrderTruckIn.enuOperation_Type.DELETE, objSaveOrderTruckIn)
                objSaveOrderTruckIn.SaveData()
                getOrderTruckInDetail(_Order_Index)
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
            If USE_PRODUCT_CUSTOMER() = True Then
                If Me._Customer_Index = "" Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If

            End If

            Dim frm As New WMS_STD_INB_Receive.frmOrderBarcode
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()
            Dim odtOrderItem_Barcode As New DataTable
            If (frm.DataTable Is Nothing) Then Exit Sub
            odtOrderItem_Barcode = frm.DataTable
            Dim intCurrentRows As Integer = grdOrderItem.Rows.Count - 1
            For Each drOrder_Barcode As DataRow In odtOrderItem_Barcode.Rows
                With Me.grdOrderItem
                    '
                    Me.grdOrderItem.Rows.Add()
                    Me.grdOrderItem.Rows(grdOrderItem.Rows.Count - 2).Cells("col_Qty").Value = drOrder_Barcode("Qty").ToString  ' Me.grdOrderItem.Rows(intCurrentRows).Cells("col_Qty").Value = drOrder_Barcode("Qty").ToString

                    Me.grdOrderItem.Rows(grdOrderItem.Rows.Count - 2).Cells("col_Plot").Value = drOrder_Barcode("Plot").ToString
                    Me.grdOrderItem.Rows(grdOrderItem.Rows.Count - 2).Cells("cbo_ItemStatus").Value = drOrder_Barcode("Status").ToString

                    Me.grdOrderItem.Rows(grdOrderItem.Rows.Count - 2).Cells("col_SKU_Id").Value = drOrder_Barcode("Sku_Id").ToString
                    Me.grdOrderItem.Update()

                    '  intCurrentRows += 1
                End With

            Next

            Get_SumData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                W_MSG_Information_ByIndex(8)

                Exit Sub
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
            End If

            frm.Close()
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
            For i As Integer = 0 To pGridName.Rows.Count - 2
                For Each odrTemp As DataRow In odr

                    strColumnName = odrTemp("Column_Name").ToString

                    Dim strDefaultValue As String = odrTemp.Item("Description").ToString
                    Dim chkDefaultValue As Boolean = IsNumeric(strDefaultValue)
                    Select Case chkDefaultValue
                        Case False
                            If pGridName.Rows(i).Cells(strColumnName).Value = strDefaultValue Then
                                Select Case W_Module.WV_Language
                                    Case enmLanguage.Thai
                                        W_MSG_Information(odrTemp("Msg_Th").ToString)
                                        Return False
                                    Case enmLanguage.English
                                        W_MSG_Information(odrTemp("Msg_En").ToString)
                                        Return False
                                End Select
                            End If

                        Case True
                            Dim dblValue As Double = pGridName.Rows(i).Cells(strColumnName).Value
                            If dblValue = CDbl(strDefaultValue) Then
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


    Function SetValidate_Control(ByVal podtValidate As DataTable, ByVal pstrTypeCheck As String) As Boolean
        Try

            If podtValidate Is Nothing Then Exit Function
            For i As Integer = 0 To podtValidate.Rows.Count - 1
                Select Case pstrTypeCheck
                    Case "SetFont"
                        Select Case podtValidate.Rows(i).Item("Field_Name").ToString

                            Case "USE_QtyPck"
                                lblQtyPck.Font = Me.lblOrder_No.Font
                            Case "Ref_No1"

                                lblRef_No1.Font = Me.lblOrder_No.Font

                            Case "Ref_No2"

                                lblRef_No2.Font = Me.lblOrder_No.Font

                            Case "Ref_No3"

                                lblRef_No3.Font = Me.lblOrder_No.Font

                            Case "Ref_No4"

                                lblRef_No4.Font = Me.lblOrder_No.Font

                            Case "Ref_No5"

                                lblRef_No5.Font = Me.lblOrder_No.Font

                            Case "Str1"

                                lblStr1.Font = Me.lblOrder_No.Font

                            Case "Str2"

                                lblStr2.Font = Me.lblOrder_No.Font

                            Case "Str3"

                                lblStr3.Font = Me.lblOrder_No.Font

                            Case "Str4"
                                lblStr4.Font = Me.lblOrder_No.Font
                            Case "Str5"

                                lblStr5.Font = Me.lblOrder_No.Font

                            Case "ASN_No"

                                lblASN_No.Font = Me.lblOrder_No.Font

                            Case "Invoice_No"
                                lblInvoice_No.Font = Me.lblOrder_No.Font


                            Case "Handling_Type"
                                lblType.Font = Me.lblOrder_No.Font

                            Case "ApprovedBy_Name"

                                lblApprovede_By.Font = Me.lblOrder_No.Font
                            Case "Checker_Name"

                                lblChecker_Name.Font = Me.lblOrder_No.Font
                            Case "Supplier_Index"
                                lblSupplier_Index.Font = Me.lblOrder_No.Font

                            Case "Department_Index"
                                lblDepartment.Font = Me.lblOrder_No.Font

                            Case "ReceiveType"

                            Case "Control_PlanReceive"

                            Case "btnBarcode"


                            Case "Consignee_Index"
                                lbConsignee.Font = Me.lblOrder_No.Font
                        End Select

                    Case "SetValidate"

                        Select Case podtValidate.Rows(i).Item("Field_Name").ToString

                            Case "USE_QtyPck"

                                If txtQtyPck.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblQtyPck.Text)
                                    Return False
                                End If

                            Case "Ref_No1"

                                If txtRef_No1.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblRef_No1.Text)
                                    Return False
                                End If


                            Case "Ref_No2"

                                If txtRef_No2.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblRef_No2.Text)
                                    Exit Function
                                End If
                            Case "Ref_No3"
                                If txtRef_No3.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblRef_No3.Text)
                                    Return False
                                End If
                            Case "Ref_No4"

                                If txtRef_No4.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblRef_No4.Text)
                                    Return False
                                End If

                            Case "Ref_No5"

                                If txtRef_No5.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblRef_No5.Text)
                                    Return False
                                End If

                            Case "Str1"
                                If txtStr1.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblStr1.Text)
                                    Return False
                                End If
                            Case "Str2"
                                If txtStr2.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblStr2.Text)
                                    Return False
                                End If

                            Case "Str3"
                                If txtStr3.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblStr3.Text)
                                    Return False
                                End If

                            Case "Str4"
                                If txtStr4.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblStr4.Text)
                                    Return False
                                End If
                            Case "Str5"
                                If txtStr5.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblStr5.Text)
                                    Return False
                                End If
                            Case "ASN_No"
                                If txtASN_No.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblASN_No.Text)
                                    Return False
                                End If
                            Case "Invoice_No"
                                If txtInvoice_No.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblInvoice_No.Text)
                                    Return False
                                End If

                            Case "Handling_Type"

                                If cboType.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblType.Text)
                                    Return False
                                End If
                            Case "ApprovedBy_Name"

                                If txtApprovede_By.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblApprovede_By.Text)
                                    Return False
                                End If

                            Case "Checker_Name"
                                If txtChecker_Name.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblChecker_Name.Text)
                                    Return False
                                End If

                            Case "Supplier_Index"

                                If txtSupplier_Id.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblSupplier_Index.Text)
                                    Return False
                                End If

                            Case "Department_Index"
                                If txtDepartment_Id.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lblDepartment.Text)
                                    Return False
                                End If

                            Case "ReceiveType"

                            Case "Control_PlanReceive"

                            Case "btnBarcode"


                            Case "Consignee_Index"

                                If txtConsignee_ID.Text = "" Then
                                    W_MSG_Information("กรุณาป้อน " & lbConsignee.Text)
                                    Return False
                                End If
                        End Select

                End Select




            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region



    Private Sub btn_swap_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_swap.Click
        Try
            'Dim frm As New viewimg
            ''frm.imgPreview = Me.imgPreview
            'frm._str_img_path = Me.imgPreview.ImageLocation
            'frm.ShowDialog()

            imgPreview.Size = New System.Drawing.Size(627, 264)
            If imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize Then
                Panel1.AutoScroll = False
                imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage

            ElseIf imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage Then
                Panel1.AutoScroll = True
                imgPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub frmDeposit_WMS_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New frmConfigReceive
                    frm.ShowDialog()
                    Dim objLang As New W_Language
                    objLang.SwitchLanguage(Me, 1)
                    Me._odtValidate_grdOrderItem = objLang.SW_Language_Column(Me, Me.grdOrderItem, 1)
                    Me._odtValidate_grdOrderTruckIn = objLang.SW_Language_Column(Me, Me.grdOrderTruckIn, 1)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            btnSave.Enabled = True

            cboPrint.Enabled = False
            btnPrintReport.Enabled = False
            btnTag.Enabled = False
            btnEdit.Enabled = False
            btnSerial.Enabled = False

            Me.btnDelete.Enabled = True
            Me.btnCopy.Enabled = True
            Me.btnPalletSlip.Enabled = True
            Me.btnAddPic.Enabled = True
            Me.btnRemovePic.Enabled = True
            Me.btnAdd_TruckIn.Enabled = True
            Me.btnEdit_TruckIn.Enabled = True
            Me.btnDelete_TruckIn.Enabled = True
            Me.grdOrderItem.ReadOnly = False

            Me.grdOrderItem.EditMode = DataGridViewEditMode.EditOnKeystrokeOrF2

            Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
            For Irow As Integer = 0 To grdOrderItem.Rows.Count - 2
                If objDB.isItemTAG(Me.grdOrderItem.Rows(Irow).Cells("col_OrderItem_Index").Value) Or objDB.isItemSerial(Me.grdOrderItem.Rows(Irow).Cells("col_OrderItem_Index").Value) Then
                    Me.grdOrderItem.Rows(Irow).Cells("col_Qty").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("btnSplit").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("btnSplit_Qty").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("btn_SKU_Popup").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("col_SKU_Id").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("cbo_Receive_Package").ReadOnly = True

                    Me.grdOrderItem.Rows(Irow).Cells("col_Plot").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("chk_Mfg_Date").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("chkExp_date").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("Col_Mfg_Date").ReadOnly = True
                    Me.grdOrderItem.Rows(Irow).Cells("col_Exp_Date").ReadOnly = True

                    'Me.grdOrderItem.Rows(Irow).Cells("col_Qty").Style.BackColor = Color.Gainsboro

                    Me.grdOrderItem.Rows(Irow).Cells("col_SKU_Id").Style.BackColor = Color.Pink
                Else
                    Me.grdOrderItem.Rows(Irow).Cells("col_Qty").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("btnSplit").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("btnSplit_Qty").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("btn_SKU_Popup").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("col_SKU_Id").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("cbo_Receive_Package").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("col_Qty").Style.BackColor = Color.White
                    Me.grdOrderItem.Rows(Irow).Cells("col_SKU_Id").Style.BackColor = Color.White


                    Me.grdOrderItem.Rows(Irow).Cells("col_Plot").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("chk_Mfg_Date").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("chkExp_date").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("Col_Mfg_Date").ReadOnly = False
                    Me.grdOrderItem.Rows(Irow).Cells("col_Exp_Date").ReadOnly = False
                End If

                If grdOrderItem.Rows(Irow).Cells("col_DocumentPlanItem_Index").Value <> Nothing Then
                    If grdOrderItem.Rows(Irow).Cells("col_DocumentPlanItem_Index").Value.ToString.Trim <> "" Then
                        Me.grdOrderItem.Rows(Irow).Cells("btn_SKU_Popup").ReadOnly = True
                        Me.grdOrderItem.Rows(Irow).Cells("col_SKU_Id").ReadOnly = True
                    End If
                End If

            Next


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTag.Click, btnStoreIn.Click
        Try


            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If


            Select Case _USE_PUTAWAY_BY_TAG
                Case 0
                    Dim frm As New WMS_STD_INB_Receive.frmJobOrderProductList(WMS_STD_INB_Receive.frmJobOrderProductList.enuOperation_Type.SEARCH, Me._Order_Index)
                    frm.ShowDialog()
                Case 1
                    Dim frm As New frmTag_Main_V4 'frmTag_Main_V3 'frmTag_Main
                    frm.Icon = Me.Icon
                    frm.Order_Index = Me._Order_Index
                    frm.ShowDialog()
                    Select Case objStatus
                        Case enuOperation_Type.UPDATE
                            Me.txtOrder_No.ReadOnly = True
                            Me.getOrderHeader(_Order_Index)
                    End Select
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)

        End Try
    End Sub


    Private Sub btnSerial_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSerial.Click
        Try
            If Me.grdOrderItem.RowCount <= 0 Then Exit Sub
            If Me.grdOrderItem.CurrentRow.Index < 0 Then Exit Sub
            If Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value Is Nothing Then Exit Sub
            Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)

            If objDB.isItemTAG(Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value) = True Then
                Dim frm As New WMS_STD_INB_Receive.frmOrderItemSerial(WMS_STD_INB_Receive.frmOrderItemSerial.eMode.ViewOnly)
                frm.OrderItem_Index = Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value
                frm.ShowDialog()
            Else
                Dim frm As New WMS_STD_INB_Receive.frmOrderItemSerial(WMS_STD_INB_Receive.frmOrderItemSerial.eMode.Operation)
                frm.OrderItem_Index = Me.grdOrderItem.CurrentRow.Cells("col_OrderItem_Index").Value
                frm.ShowDialog()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdOrderItem_RowHeaderMouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles grdOrderItem.RowHeaderMouseClick
        Try
            Call ManageBottonSerial()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ManageBottonSerial()
        Try
            If Me.grdOrderItem.CurrentRow Is Nothing OrElse grdOrderItem.CurrentRow.Cells("col_SKU_Index").Value Is Nothing Then
                Exit Sub
            End If
            Select Case _status
                Case 1, 4, 5, 10
                    Dim pSku_Index As String
                    Dim OBJ As New tb_OrderItemSerial
                    pSku_Index = grdOrderItem.CurrentRow.Cells("col_SKU_Index").Value.ToString
                    Me.btnSerial.Enabled = OBJ.CheckIsSerial(pSku_Index)
                Case Else
                    Me.btnSerial.Enabled = False
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPlanReceive_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPlanReceive.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                If txtPlanReceive.Text <> "" Then
                    Me.GetPOItem()
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPalletSlip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPalletSlip.Click
        Try
            If Me.cboDocumentType.Items.Count = 0 Then
                W_MSG_Information("ไม่พบประเภทเอกสาร")
                Exit Sub
            End If
            'Dim frm As New frmPrintPalletSlip_v2
            Dim frm As New frmPrintPalletSlip_v3
            frm.frmDeposit_WMS = Me
            frm.Order_Index = Me._Order_Index
            frm.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            frm.Customer_Index = Me._Customer_Index
            frm.Order_Date = Me.dtOrder.Value
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Public Sub SavePalletSlip(ByRef Tag_Index As String, ByRef Tag_No As String, ByVal Ref_No1 As String, ByVal Ref_No2 As String, ByVal Sku_Index As String, ByVal Package_Index As String, ByVal Qty As String, ByVal Ratio As String, ByVal Total_Qty As String, ByVal ItemStatus_Index As String, ByVal Plot As String, ByVal Mfg_Date As String, ByVal Exp_Date As String, ByVal UnitWeight As Decimal, ByVal UnitVolume As Decimal, ByVal UnitWidth As Decimal, ByVal UnitLength As Decimal, ByVal UnitHeight As Decimal)

        ' **************************
        ' *** Loop to Check Unique Item Line In datagrid **
        ' *** option for check unique product ***
        ' *************************************************

        Dim objHeader As New tb_Order
        Dim objItem As New tb_OrderItem
        Dim objItemCollection As New List(Of tb_OrderItem)
        Dim objms_ItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim ItemLife_Total_Day As Integer = 0

        ' PalletType_History
        Dim objPalletType As New tb_PalletType_History
        Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

        Dim Max_Seq As Integer = New clsPrintPalletSlip().getOrderItemCount(Me.Order_Index)
        Dim OrderItem_Index As String = ""
        Dim Consignee_Index As String = ""
        Dim Comment As String = ""
        Dim Location_Alias As String = ""
        Dim Weight As Decimal = CDec(Qty) * CDec(UnitWeight)
        Dim NetWeight As Decimal = CDec(Qty) * CDec(UnitWeight)
        Dim Volume As Decimal = CDec(Qty) * CDec(UnitVolume)
        Dim NetVolume As Decimal = CDec(Qty) * CDec(UnitVolume)
        Dim PalletType_Index As String = ""
        Dim Pallet_Qty As Decimal = 0
        Dim OrderItem_Price As Decimal = 0
        Dim Price_Per_Pck As Decimal = 0
        Dim Item_Package_Index As String = ""
        Dim PO_No As String = ""
        Dim ASN_No As String = ""
        Dim Invoice_No As String = ""
        Dim Pallet_No As String = ""
        Dim PO_Item As String = ""
        Dim Group_No As String = ""
        Dim Declaration_No As String = ""
        Dim Reference As String = Ref_No1
        Dim Reference2 As String = Ref_No2
        Dim ItemDefinition As String = ""
        Dim OrderItem_RowIndex As String = Max_Seq
        Dim Serial_No As String = ""
        Dim HandlingType_Index As String = ""
        Dim DocumentPlan_Index As String = ""
        Dim DocumentPlanItem_Index As String = ""
        Dim DocumentPlan_No As String = ""
        Dim Plan_Process As String = ""
        Dim Str6 As String = ""
        Dim Tax1 As Decimal = 0
        Dim Tax2 As Decimal = 0
        Dim Tax3 As Decimal = 0
        Dim Tax4 As Decimal = 0
        Dim Tax5 As Decimal = 0
        Dim HS_Code As String = ""
        Dim ItemDescription As String = ""
        Dim Seq As String = Max_Seq + 1
        Dim ERP_Location As String = ""

        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            '************* Begin Comment When Use Function Validate ***************** 
            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderItem, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdPallet, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdSerial, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderImage, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderTruckIn, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            '*** Check Important Data ***
            If Me._Customer_Index = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If
            '************* End Comment When Use Function Validate ***************** 

            ' *** Check Product Item ***
            If (Sku_Index = Nothing Or Package_Index = Nothing) Then
                W_MSG_Information("กรุณากรอกรายการสินค้า")
                Exit Sub
            End If

            ' *** BEGIN Checking Putaway all item
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim intCountLocation As Integer = 0

                '************* Begin Comment When Use Function Validate ***************** 
                'Checking SKU 
                If (Sku_Index = Nothing) Then
                    W_MSG_Information("กรุณากรอกรายการสินค้าให้ครบ")
                    Exit Sub
                End If

                '************* End Comment When Use Function Validate ***************** 

                Me._LocationID = Location_Alias
                If (Me._LocationID IsNot Nothing) And (Not _LocationID = "") Then
                    intCountLocation += 1

                    'Check Location
                    Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    Dim odtLocation As New DataTable
                    oms_Location.SearchData_Click("*", " AND Location_Alias = '" & Me._LocationID.Replace("'", "''").Trim & "'")
                    odtLocation = oms_Location.GetDataTable

                    If odtLocation.Rows.Count > 0 Then
                        Dim dblQty, dblWeight, dblVolume As Double

                        dblQty = odtLocation.Rows(0)("Current_Qty").ToString
                        dblWeight = odtLocation.Rows(0)("Current_Weight").ToString
                        dblVolume = odtLocation.Rows(0)("Current_Volume").ToString

                        If dblQty + CDbl(Qty) > odtLocation.Rows(0)("Max_Qty") Then
                            W_MSG_Information("จำนวนในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub
                        ElseIf dblQty + NetWeight > odtLocation.Rows(0)("Max_Weight") Then
                            W_MSG_Information("น้ำหนักในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub
                        ElseIf dblQty + NetVolume > odtLocation.Rows(0)("Max_Volume") Then
                            W_MSG_Information("ปริมาตรในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub

                        End If
                    Else
                        W_MSG_Information("ไม่พบตำแหน่งจัดเก็บ " & Me._LocationID & " !" & vbNewLine & "กรุณาป้อนตำแหน่งใหม่")
                    End If
                End If

                'If intCountLocation > 0 Then
                '    If intCountLocation <> grdOrderItem.Rows.Count - 1 Then
                '        W_MSG_Information("กรุณาป้อนตำแหน่งจัดเก็บให้ครบทุกรายการ")
                '        Exit Sub
                '    End If
                'End If

            End If

            objHeader.Order_Index = Me._Order_Index
            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_No = Me.txtOrder_No.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            If txtOrder_No.Text = "" Then

                If _USE_BRANCH_ID Then
                    Dim strWhere As String = " Branch_ID ='" & WV_Branch_ID & "'"
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing
                Else
                    Dim strWhere As String = ""
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing

                End If

            End If

            objHeader.Lot_No = Me.txtLot_No.Text

            ' xxxxxxx Using TAG Properties xxxxxxxxxxx
            objHeader.Customer_Index = Me._Customer_Index.ToString
            If Me.txtSupplier_Id.Tag = Nothing Then
                objHeader.Supplier_Index = ""
            Else
                objHeader.Supplier_Index = Me.txtSupplier_Id.Tag.ToString
            End If

            If Me.txtDepartment_Id.Tag = Nothing Then
                objHeader.Department_Index = ""
            Else
                objHeader.Department_Index = Me.txtDepartment_Id.Tag.ToString
            End If
            If Me.txtConsignee_ID.Tag = Nothing Then
                objHeader.Consignee_Index = ""
            Else
                objHeader.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
            End If
            ' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_Time = Me.txtTime.Text
            objHeader.Ref_No1 = Me.txtRef_No1.Text
            objHeader.Ref_No2 = Me.txtRef_No2.Text
            objHeader.Ref_No3 = Me.txtRef_No3.Text
            objHeader.Ref_No4 = Me.txtRef_No4.Text
            objHeader.Ref_No5 = Me.txtRef_No5.Text
            objHeader.Str1 = Me.txtStr1.Text
            'objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str3 = Me.txtStr3.Text
            objHeader.Str4 = Me.txtStr4.Text
            objHeader.Str5 = Me.txtStr5.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            objHeader.Comment = Me.txtComment.Text
            objHeader.Str8 = Me.cboContact_Name.Text
            objHeader.PO_No = Me.txtPlanReceive.Text
            objHeader.Invoice_No = Me.txtInvoice_No.Text
            objHeader.ASN_No = Me.txtASN_No.Text
            'objHeader.Receive_Type = Me.chkReceiveType.Checked

            If cboType.SelectedValue IsNot Nothing Then
                objHeader.HandlingType_Index = cboType.SelectedValue
            Else
                objHeader.HandlingType_Index = ""
            End If

            'End If
            'killz 02-06-2011 objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str2 = IIf(Me.txtStr2.SelectedValue Is Nothing, "", Me.txtStr2.SelectedValue)
            'killz 02-06-2011 เก็บประเภทรถ ไว้คิดตัง
            objHeader.Str9 = IIf(Me.txtStr9.SelectedValue Is Nothing, "", Me.txtStr9.SelectedValue)
            ' *************************************************tap 2

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


            If txtQtyPck.Text = "" Then
                objHeader.Flo1 = 0
            Else
                objHeader.Flo1 = txtQtyPck.Text

            End If
            'For i As Integer = 0 To grdOrderItem.Rows.Count - 2

            'With grdOrderItem

            ' *** New Object *********
            objItem = New tb_OrderItem


            If (OrderItem_Index = Nothing) Then
                Dim objDBIndex As New Sy_AutoNumber
                OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                objItem.OrderItem_Index = OrderItem_Index
                Me.OrderItem_Id = objItem.OrderItem_Index
                objDBIndex = Nothing
            Else
                objItem.OrderItem_Index = OrderItem_Index
            End If
            objItem.Order_Index = objHeader.Order_Index

            'Plan_Qty
            If (IsNumeric(Qty)) Then
                objItem.Plan_Qty = CDbl(Qty)
            End If

            'Sku_Index
            If (Not (Sku_Index = Nothing)) Then
                objItem.Sku_Index = Sku_Index
            End If

            '12-01-2010 By ja saveConsignee_Index ตามฟิวใหม่ที่แอดลงใน tb_orderItem 
            If (Not (Consignee_Index = Nothing)) Then
                objItem.Consignee_Index = Consignee_Index
            End If

            'comment 
            If (Not (Comment = Nothing)) Then
                objItem.Comment = Comment
            End If

            'Qty
            objItem.Qty = CDec(Qty)
            If objItem.Qty = 0 Then
                MessageBox.Show("กรุณากรอกจำนวนสินค้า", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If (Not (Package_Index = Nothing)) Then
                ' ** .Tag
                objItem.Package_Index = Package_Index
            End If
            ' *** Get Retio ***
            Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
            objRatio = Nothing

            ' *****************
            ' *** Calculate Tatal Qty *** 
            objItem.Total_Qty = objItem.Qty * objItem.Ratio
            ' ***************************

            'Weight
            If (IsNumeric(Weight)) Then
                objItem.Weight = Weight
            End If

            'col_Net_Weight
            If (IsNumeric(NetWeight)) Then
                objItem.Flo1 = NetWeight
            End If

            'Volume
            If (IsNumeric(Volume)) Then
                objItem.Volume = Volume
            End If

            'PalletType_Index   
            If (Not (PalletType_Index = Nothing)) Then
                objItem.PalletType_Index = PalletType_Index
            End If

            'Pallet_Qty
            If (IsNumeric(Pallet_Qty)) Then
                objItem.Pallet_Qty = Pallet_Qty
            End If

            If (Not (Location_Alias = Nothing)) Then
                Me._LocationID = Location_Alias
                'Temp Location
                objItem.Str4 = Location_Alias
            End If

            objItem.ItemStatus_Index = ItemStatus_Index


            ' **** Calculate Exp_Date ***
            'Me.CalcualateDate_In_Gridview(i)
            ' ***************************

            ' *** value for date in gridview ***
            objItem.IsMfg_Date = IIf(IsDate(Mfg_Date), True, False)
            ''chkExp_date
            objItem.IsExp_Date = IIf(IsDate(Exp_Date), True, False)

            If (IsDate(Mfg_Date)) Then
                objItem.Mfg_Date = CDate(Mfg_Date)
            End If
            If (IsDate(Exp_Date)) Then
                objItem.Exp_date = CDate(Exp_Date)
            End If

            ' *** set default of IsMfg_Date and IsExp_Date For EFFEM *** 
            'objItem.IsMfg_Date = True
            'objItem.IsExp_Date = True
            ' **********************************************************

            'Is_SN
            objItem.Is_SN = False
            'Select Case .Rows(i).Cells("col_Qty").ReadOnly
            '    Case False
            '        objItem.Is_SN = False 'Not SN 
            '    Case True
            '        objItem.Is_SN = True 'IS SN
            '    Case Else
            '        objItem.Is_SN = False 'Not SN 
            'End Select


            ' Add new By Dong_kk

            'W
            If (IsNumeric(UnitWidth)) Then
                objItem.Flo2 = UnitWidth
            End If

            'L
            If (IsNumeric(UnitLength)) Then
                objItem.Flo3 = UnitLength
            End If

            'H
            If (IsNumeric(UnitHeight)) Then
                objItem.Flo4 = UnitHeight
            End If

            'Qty_Per_Pck
            objItem.Qty_Per_Pck = 1

            'Item_Qty
            objItem.Item_Qty = CDbl(Total_Qty)

            'Weight_Per_Pck
            If (IsNumeric(UnitWeight)) Then
                objItem.Weight_Per_Pck = CDec(UnitWeight)

            End If

            'Volume_Per_Pck
            If (IsNumeric(UnitVolume)) Then
                objItem.Volume_Per_Pck = UnitVolume

            End If

            'OderItem_Price
            If (IsNumeric(OrderItem_Price)) Then
                objItem.OrderItem_Price = OrderItem_Price
            End If

            'Price_Per_Pck
            If (IsNumeric(Price_Per_Pck)) Then
                objItem.Price_Per_Pck = Price_Per_Pck
            End If

            'PACKAGE ITEM
            If (Not (Item_Package_Index = Nothing)) Then
                objItem.Item_Package_Index = Item_Package_Index
            End If


            '********* DOCUMENT TYPE *********
            '--- Lot_No From Header
            objItem.Lot_No = objHeader.Lot_No



            Select Case cboPlanReceive.SelectedValue
                Case 9
                    '--- PO_No
                    If (Not (PO_No = Nothing)) Then
                        objItem.PO_No = PO_No
                    End If
                Case 7

                    If (Not (PO_No = Nothing)) Then
                        objItem.PO_No = PO_No
                    End If

                Case 16
                    '--- ASN_No,Shipment_No
                    If (Not (ASN_No = Nothing)) Then
                        objItem.ASN_No = ASN_No
                    End If
                Case 103

            End Select
            'PLOT
            If (Not (Plot = Nothing)) Then
                objItem.Plot = Plot
            End If
            'INVOICE
            If (Not (Invoice_No = Nothing)) Then
                objItem.Invoice_No = Invoice_No
            End If

            'Pallet No.
            If (Not (Pallet_No = Nothing)) Then
                objItem.Str5 = Pallet_No
            End If

            'PO  ******แก้จากเก็บ index เป็น เก็บ name [ชั่วคราว]
            If (Not (PO_Item = Nothing)) Then
                objItem.Str7 = PO_Item
            End If


            'Group NO
            If (Not (Group_No = Nothing)) Then
                objItem.Str9 = Group_No
            End If

            'POINDEX
            If (Not (PO_Item = Nothing)) Then
                objItem.Str10 = PO_Item
            End If

            'Decralation
            If (Not (Declaration_No = Nothing)) Then
                objItem.Declaration_No = Declaration_No
            End If



            If SetAUTO_REFERENCE() = 1 Then
                If (Reference = Nothing) Then
                    MessageBox.Show("Reference", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    objItem.Str1 = Reference
                End If
                '--- Referecne 2
                If (Reference2 = Nothing) Then
                    MessageBox.Show("Reference2", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    objItem.Str2 = Reference2
                End If

            Else

                If (Not (Reference = Nothing)) Then
                    objItem.Str1 = Reference
                Else
                    objItem.Str1 = ""
                End If
                '--- Referecne 2
                If (Not (Reference2 = Nothing)) Then
                    objItem.Str2 = Reference2
                Else
                    objItem.Str2 = ""
                End If
                '--- ItemDefinition_Index
                If (Not (ItemDefinition = Nothing)) Then
                    objItem.ItemDefinition_Index = ItemDefinition
                Else
                    objItem.ItemDefinition_Index = ""
                End If
            End If

            If (IsNumeric(OrderItem_RowIndex)) Then
                objItem.OrderItem_RowIndex = OrderItem_RowIndex
            End If

            If (Not (Serial_No = Nothing)) Then
                objItem.Serial_No = Serial_No
            End If


            'Lot/BATCTH
            ' **** Check isPlot ****
            ' *** You need to check that need Lot to used ***
            Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            If objSku.isPlot_Value(objItem.Sku_Index) = True Then
                ' *** Need to Input PLot in Order Item *** 
                If Trim(objItem.Plot).ToString = "" Then
                    MessageBox.Show("กรุณากรอก Lot ผลิตด้วย", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If





            ' HandlingType
            If (Not (HandlingType_Index = Nothing)) Then
                objItem.HandlingType_Index = HandlingType_Index
            End If

            ' Plan_Process

            If (Not (DocumentPlan_Index = Nothing)) Then
                objItem.DocumentPlan_Index = DocumentPlan_Index
            Else
                objItem.DocumentPlan_Index = ""
            End If
            If (Not (DocumentPlanItem_Index = Nothing)) Then
                objItem.DocumentPlanItem_Index = DocumentPlanItem_Index
            Else
                objItem.DocumentPlanItem_Index = ""
            End If
            If (Not (DocumentPlan_No = Nothing)) Then
                objItem.DocumentPlan_No = DocumentPlan_No
            Else
                objItem.DocumentPlan_No = ""
            End If

            If (Not (Plan_Process = Nothing)) Then
                objItem.Plan_Process = CInt(Plan_Process)
            Else
                objItem.Plan_Process = -9
            End If

            'include from master site
            If (Not (Str6 = Nothing)) Then
                objItem.Str6 = Str6
            End If

            If (IsNumeric(Tax1)) Then
                objItem.Tax1 = Tax1
            End If

            If (IsNumeric(Tax2)) Then
                objItem.Tax2 = Tax2
            End If

            If (IsNumeric(Tax3)) Then
                objItem.Tax3 = Tax3
            End If

            If (IsNumeric(Tax4)) Then
                objItem.Tax4 = Tax4
            End If

            If (IsNumeric(Tax5)) Then
                objItem.Tax5 = Tax5
            End If

            'add new 14/10/2009
            If (Not (HS_Code = Nothing)) Then
                objItem.HS_Code = HS_Code
            End If

            If (Not (ItemDescription = Nothing)) Then
                objItem.ItemDescription = ItemDescription
            End If


            If (IsNumeric(Seq)) Then
                objItem.Seq = Seq
            End If


            If (Not (ERP_Location = Nothing)) Then
                objItem.ERP_Location = ERP_Location
            End If


            'Begin : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 
            Dim dblQty_Receive As Double = 0
            Dim dblTotal_Qty_Receive As Double = 0
            Dim dtCheckPlanReceive As New DataTable
            With dtCheckPlanReceive.Columns
                .Add("DocumentPlanItem_Index", GetType(String))
                .Add("Plan_Process", GetType(Double))
                .Add("PlanQty", GetType(Double))
                .Add("Qty", GetType(Double))
            End With

            If Not String.IsNullOrEmpty(objItem.DocumentPlanItem_Index) Then

                'dong edit 2014-05-25
                Select Case objItem.Plan_Process
                    Case 9
                        Dim oPOI As New ml_Receive_PO
                        Dim dtPO As New DataTable
                        oPOI.getPo_byPoi_Index(objItem.DocumentPlanItem_Index)
                        dtPO = oPOI.GetDataTable
                        If dtPO.Rows.Count > 0 Then
                            'Local Receive
                            If dtCheckPlanReceive.Rows.Count > 0 Then
                                dblQty_Receive = dtCheckPlanReceive.Compute("SUM(Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                                dblTotal_Qty_Receive = dtCheckPlanReceive.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                            End If
                            'Real Oi Receive
                            Dim oOI As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
                            Dim dtOI As New DataTable
                            oOI.getOrderItemAll(objItem.OrderItem_Index)
                            dtOI = oOI.GetDataTable
                            Dim dblQtyOI As Double = 0
                            Dim dblTotal_Qty As Double = 0
                            If dtOI.Rows.Count > 0 Then
                                dblQtyOI = IIf(IsNumeric(dtOI.Rows(0)("Qty")), dtOI.Rows(0)("Qty"), 0)
                                dblTotal_Qty = IIf(IsNumeric(dtOI.Rows(0)("Total_Qty")), dtOI.Rows(0)("Total_Qty"), 0)
                            End If

                            'Online Receive
                            dtPO.Rows(0)("Qty_Bal") = IIf(IsNumeric(dtPO.Rows(0)("Qty_Bal")), dtPO.Rows(0)("Qty_Bal"), 0)
                            dtPO.Rows(0)("Qty") = IIf(IsNumeric(dtPO.Rows(0)("Qty")), dtPO.Rows(0)("Qty"), 0)
                            dtPO.Rows(0)("Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Received_Qty")), dtPO.Rows(0)("Received_Qty"), 0)
                            dtPO.Rows(0)("Total_Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Total_Received_Qty")), dtPO.Rows(0)("Total_Received_Qty"), 0)

                            Dim Qty_Receive_Real As Double = (dtPO.Rows(0)("Received_Qty") - dblQty_Receive - dblQtyOI) + objItem.Total_Qty

                            Dim Total_Qty_Receive_Real As Double = 0

                            Total_Qty_Receive_Real = (dtPO.Rows(0)("Total_Received_Qty") - dblTotal_Qty_Receive - dblTotal_Qty) + objItem.Total_Qty

                            If dtPO.Rows(0)("Total_Qty") < Total_Qty_Receive_Real Then
                                W_MSG_Information("บรรทัดที่ " & (0 + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "" & "")
                                'W_MSG_Information("บรรทัดที่ " & (i + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "จำนวนเกิน " & "")
                                Exit Sub

                            End If

                        End If

                        Dim drNewRow As DataRow = dtCheckPlanReceive.NewRow
                        drNewRow("Qty") = objItem.Qty
                        drNewRow("PlanQty") = objItem.Plan_Qty
                        drNewRow("Plan_Process") = objItem.Plan_Process
                        drNewRow("DocumentPlanItem_Index") = objItem.DocumentPlanItem_Index
                        dtCheckPlanReceive.Rows.Add(drNewRow)

                End Select
            End If

            'End : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 



            If (IsNumeric(OrderItem_RowIndex)) Then
                objItem.iRow_OrderItem_Index = OrderItem_RowIndex
            End If
            'End With
            ' *** Add value ***
            objItemCollection.Add(objItem)

            'Next

            '' PALLETTYPE
            'Dim J As Integer = 0

            'For J = 0 To grdPallet.Rows.Count - 1
            '    If grdPallet.Rows(J).Cells("col_UsePallet").Value <> 0 Then

            '        With grdPallet

            '            ' *** New Object *********
            '            objPalletType = New tb_PalletType_History
            '            ' ************************

            '            If .Rows(J).Cells("col_PalletType_History_Index").Value = "" Then
            '                Dim objDBIndex As New Sy_AutoNumber
            '                .Rows(J).Cells("col_PalletType_History_Index").Value = objDBIndex.getSys_Value("PalletType_History_Index ")
            '                objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value
            '                objDBIndex = Nothing
            '            Else
            '                objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value.ToString
            '            End If

            '            'PalletType_Index
            '            If .Rows(J).Cells("col_Palletindex").Value IsNot Nothing Then
            '                objPalletType.PalletType_Index = .Rows(J).Cells("col_Palletindex").Value
            '            End If

            '            'Qty_In
            '            If .Rows(J).Cells("col_UsePallet").Value IsNot Nothing Then
            '                objPalletType.Qty_In = .Rows(J).Cells("col_UsePallet").Value
            '            End If

            '            'Qty_Bal
            '            If .Rows(J).Cells("col_Palletqty").Value IsNot Nothing Then
            '                objPalletType.Qty_Bal = .Rows(J).Cells("col_Palletqty").Value
            '            End If

            '        End With
            '        objPalletTypeCollection.Add(objPalletType)
            '    End If
            'Next

            ' *** Call Class for Manage Data ***
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.UPDATE, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing

            End Select


            '====== BEGIN Putaway  by OrderItem =====
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim bAutoPutaway As Boolean = False
                For i As Integer = 0 To objItemCollection.Count - 1
                    Me._LocationID = Location_Alias
                    Me.OrderItem_Id = objItemCollection(i).OrderItem_Index

                    If (_LocationID IsNot Nothing) And (Not _LocationID = "") Then
                        SetLocation_fromitemAll(i)
                        bAutoPutaway = True
                    End If
                Next

                If bAutoPutaway Then
                    Dim objClassDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
                    objClassDB.SaveData(Me._Order_Index)
                End If
            End If

            '====== END Putaway  by OrderItem =====

            ' BGN Manage Tag
            For i As Integer = 0 To objItemCollection.Count - 1
                Me.SaveTag_No(objItemCollection(i).OrderItem_Index)
                Dim ObjClass As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
                ObjClass.getView_Tag_Header(String.Format(" and Tag_Process_Id = 1 and OrderItem_Index='{0}' ", Replace(OrderItem_Index, "'", "''")))
                Dim dtTag As DataTable = ObjClass.GetDataTable()
                If (dtTag.Rows.Count > 0) Then
                    Tag_Index = dtTag.Rows(0).Item("Tag_Index").ToString()
                    Tag_No = dtTag.Rows(0).Item("TAG_No").ToString()
                Else
                    ' Delete OrderItem
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
                    objDB.Delete_OrderItem(objItemCollection(i).OrderItem_Index)
                End If
            Next
            ' END Manage Tag

            If Not Me._Order_Index = "" Then
                _Order_Index = Me._Order_Index
            End If

            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnCopy.Enabled = False
            Me.btnPalletSlip.Enabled = False

            '------ Update Comment Image
            'Dim odjOrderImage As New tb_Order_Image

            'For iRow As Integer = 0 To grdOrderImage.RowCount - 1
            '    With odjOrderImage
            '        .Order_Index = _Order_Index
            '        .Order_Image_Index = grdOrderImage.Rows(iRow).Cells("Col_Order_Image_Index").Value.ToString
            '        .Comment = grdOrderImage.Rows(iRow).Cells("Col_Comment").Value.ToString
            '        .Update()
            '    End With
            'Next


            'include from master site
            If Not Me._Order_Index = "" Then
                'W_MSG_Information_ByIndex(1)
                Me.getOrderItemDetail(Me._Order_Index)
                'W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
                _Order_Index = Me._Order_Index
                cboPrint.Enabled = True
                btnPrintReport.Enabled = True
                objStatus = enuOperation_Type.UPDATE
                Me.getOrderHeader(_Order_Index)
                Me.grdOrderItem.ReadOnly = True
            Else
                W_MSG_Information_ByIndex(2)
                Exit Sub
            End If

            If (Tag_Index = Nothing) Then
                W_MSG_Information("บันทึกข้อมูลไม่สำเร็จ, กรุณาลองใหม่")
            End If
        Catch ex As Exception
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
            W_MSG_Error(ex.Message)
        Finally
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
        End Try
    End Sub

    Private Sub SaveTag_No(ByVal OrderItem_Index As String)
        Try
            ' 1 : 1
            If (OrderItem_Index = Nothing) Then Exit Sub

            Dim ObjClass As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
            ObjClass.getOrderDetail_Tag(String.Format(" and OrderItem_Index='{0}' ", Replace(OrderItem_Index, "'", "''")))
            Dim dtOrderItem As DataTable = ObjClass.GetDataTable()
            If (dtOrderItem.Rows.Count > 0) Then
                With dtOrderItem.Rows(0)
                    Dim objDBTempIndex As New Sy_AutoNumber
                    Dim objAutoNumber As New Sy_AutoNumber
                    Dim objItemCollection As New List(Of tb_TAG)
                    Dim otb_TAG As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                    otb_TAG.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                    otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                    otb_TAG.Order_No = .Item("Order_No").ToString
                    otb_TAG.Order_Index = Me._Order_Index
                    otb_TAG.Order_Date = .Item("Order_Date").ToString
                    otb_TAG.Order_Time = .Item("Order_Time").ToString
                    otb_TAG.Customer_Index = .Item("Customer_Index").ToString
                    If .Item("Supplier_Index").ToString IsNot Nothing Then
                        otb_TAG.Supplier_Index = .Item("Supplier_Index").ToString
                    Else
                        otb_TAG.Supplier_Index = ""
                    End If
                    If .Item("OrderItem_Index").ToString IsNot Nothing Then
                        otb_TAG.OrderItem_Index = .Item("OrderItem_Index").ToString
                    Else
                        otb_TAG.OrderItem_Index = ""
                    End If
                    otb_TAG.OrderItemLocation_Index = ""
                    If .Item("Sku_Index").ToString IsNot Nothing Then
                        otb_TAG.Sku_Index = .Item("Sku_Index").ToString
                    Else
                        otb_TAG.Sku_Index = ""
                    End If
                    If .Item("PLot").ToString IsNot Nothing Then
                        otb_TAG.PLot = .Item("PLot").ToString
                    Else
                        otb_TAG.PLot = ""
                    End If
                    If .Item("ItemStatus_Index").ToString IsNot Nothing Then
                        otb_TAG.ItemStatus_Index = .Item("ItemStatus_Index").ToString
                    Else
                        otb_TAG.ItemStatus_Index = ""
                    End If
                    If .Item("Package_Index").ToString IsNot Nothing Then
                        otb_TAG.Package_Index = .Item("Package_Index").ToString
                    Else
                        otb_TAG.Package_Index = ""
                    End If
                    otb_TAG.Unit_Weight = 0
                    otb_TAG.Size_Index = -1
                    If .Item("PalletType_Index").ToString IsNot Nothing Then
                        otb_TAG.Pallet_No = .Item("PalletType_Index").ToString
                    Else
                        otb_TAG.Pallet_No = ""
                    End If
                    otb_TAG.TAG_Status = 0
                    If .Item("str1").ToString IsNot Nothing Then
                        otb_TAG.Ref_No1 = .Item("str1").ToString
                    Else
                        otb_TAG.Ref_No1 = ""
                    End If
                    If .Item("str2").ToString IsNot Nothing Then
                        otb_TAG.Ref_No2 = .Item("str2").ToString
                    Else
                        otb_TAG.Ref_No2 = ""
                    End If
                    If .Item("Qty").ToString IsNot Nothing Then
                        otb_TAG.Qty = .Item("Qty").ToString
                    Else
                        otb_TAG.Qty = 0
                    End If
                    If .Item("Weight").ToString IsNot Nothing Then
                        otb_TAG.Weight = .Item("Weight").ToString
                    Else
                        otb_TAG.Weight = 0
                    End If
                    If .Item("Volume").ToString IsNot Nothing Then
                        otb_TAG.Volume = .Item("Volume").ToString
                    Else
                        otb_TAG.Volume = 0
                    End If
                    If String.IsNullOrEmpty(.Item("ERP_Location").ToString) = False Then
                        otb_TAG.ERP_Location = .Item("ERP_Location").ToString
                    Else
                        otb_TAG.ERP_Location = ""
                    End If
                    otb_TAG.Qty_per_TAG = otb_TAG.Qty
                    otb_TAG.Weight_per_TAG = otb_TAG.Weight
                    otb_TAG.Volume_per_TAG = otb_TAG.Volume
                    otb_TAG.Ref_No3 = "1/1"

                    objItemCollection.Add(otb_TAG)
                    If objItemCollection.Count > 0 Then
                        Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                        objItemA.objItemCollection = objItemCollection
                        objItemA.InsertData()
                        objDBTempIndex = Nothing
                        objAutoNumber = Nothing
                    End If

                End With
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub SavePalletSlip_v2(ByRef Tag_Index As String, ByRef Tag_No As String, ByVal Ref_No1 As String, ByVal Ref_No2 As String, ByVal Sku_Index As String, ByVal Package_Index As String, ByVal Qty As String, ByVal Ratio As String, ByVal Total_Qty As String, ByVal ItemStatus_Index As String, ByVal Plot As String, ByVal Mfg_Date As String, ByVal Exp_Date As String, ByVal UnitWeight As Decimal, ByVal UnitVolume As Decimal, ByVal UnitWidth As Decimal, ByVal UnitLength As Decimal, ByVal UnitHeight As Decimal, ByVal PurchaseOrderItem_Index As String, ByVal PurchaseOrder_Index As String, ByVal PurchaseOrder_No As String, Optional ByVal WeightScale As Decimal = 0)

        ' **************************
        ' *** Loop to Check Unique Item Line In datagrid **
        ' *** option for check unique product ***
        ' *************************************************

        Dim objHeader As New tb_Order
        Dim objItem As New tb_OrderItem
        Dim objItemCollection As New List(Of tb_OrderItem)
        Dim objms_ItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim ItemLife_Total_Day As Integer = 0

        ' PalletType_History
        Dim objPalletType As New tb_PalletType_History
        Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

        Dim Max_Seq As Integer = New clsPrintPalletSlip().getOrderItemCount(Me.Order_Index)
        Dim OrderItem_Index As String = ""
        Dim Consignee_Index As String = ""
        Dim Comment As String = ""
        Dim Location_Alias As String = ""
        Dim Weight As Decimal = CDec(Qty) * CDec(UnitWeight)
        Dim NetWeight As Decimal = CDec(Qty) * CDec(UnitWeight)
        Dim Volume As Decimal = CDec(Qty) * CDec(UnitVolume)
        Dim NetVolume As Decimal = CDec(Qty) * CDec(UnitVolume)
        Dim PalletType_Index As String = ""
        Dim Pallet_Qty As Decimal = 0
        Dim OrderItem_Price As Decimal = 0
        Dim Price_Per_Pck As Decimal = 0
        Dim Item_Package_Index As String = ""
        Dim PO_No As String = ""
        Dim ASN_No As String = ""
        Dim Invoice_No As String = ""
        Dim Pallet_No As String = ""
        Dim PO_Item As String = ""
        Dim Group_No As String = ""
        Dim Declaration_No As String = ""
        Dim Reference As String = Ref_No1
        Dim Reference2 As String = Ref_No2
        Dim ItemDefinition As String = ""
        Dim OrderItem_RowIndex As String = Max_Seq
        Dim Serial_No As String = ""
        Dim HandlingType_Index As String = ""
        Dim DocumentPlan_Index As String = PurchaseOrder_Index
        Dim DocumentPlanItem_Index As String = PurchaseOrderItem_Index
        Dim DocumentPlan_No As String = PurchaseOrder_No
        Dim Plan_Process As String = IIf(PurchaseOrderItem_Index = Nothing, "", "9")
        Dim Str6 As String = ""
        Dim Tax1 As Decimal = 0
        Dim Tax2 As Decimal = 0
        Dim Tax3 As Decimal = 0
        Dim Tax4 As Decimal = 0
        Dim Tax5 As Decimal = 0
        Dim HS_Code As String = ""
        Dim ItemDescription As String = ""
        Dim Seq As String = Max_Seq + 1
        Dim ERP_Location As String = ""

        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            '************* Begin Comment When Use Function Validate ***************** 
            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderItem, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdPallet, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdSerial, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderImage, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderTruckIn, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            '*** Check Important Data ***
            If Me._Customer_Index = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If
            '************* End Comment When Use Function Validate ***************** 

            ' *** Check Product Item ***
            If (Sku_Index = Nothing Or Package_Index = Nothing) Then
                W_MSG_Information("กรุณากรอกรายการสินค้า")
                Exit Sub
            End If

            ' *** BEGIN Checking Putaway all item
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim intCountLocation As Integer = 0

                '************* Begin Comment When Use Function Validate ***************** 
                'Checking SKU 
                If (Sku_Index = Nothing) Then
                    W_MSG_Information("กรุณากรอกรายการสินค้าให้ครบ")
                    Exit Sub
                End If

                '************* End Comment When Use Function Validate ***************** 

                Me._LocationID = Location_Alias
                If (Me._LocationID IsNot Nothing) And (Not _LocationID = "") Then
                    intCountLocation += 1

                    'Check Location
                    Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    Dim odtLocation As New DataTable
                    oms_Location.SearchData_Click("*", " AND Location_Alias = '" & Me._LocationID.Replace("'", "''").Trim & "'")
                    odtLocation = oms_Location.GetDataTable

                    If odtLocation.Rows.Count > 0 Then
                        Dim dblQty, dblWeight, dblVolume As Double

                        dblQty = odtLocation.Rows(0)("Current_Qty").ToString
                        dblWeight = odtLocation.Rows(0)("Current_Weight").ToString
                        dblVolume = odtLocation.Rows(0)("Current_Volume").ToString

                        If dblQty + CDbl(Qty) > odtLocation.Rows(0)("Max_Qty") Then
                            W_MSG_Information("จำนวนในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub
                        ElseIf dblQty + NetWeight > odtLocation.Rows(0)("Max_Weight") Then
                            W_MSG_Information("น้ำหนักในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub
                        ElseIf dblQty + NetVolume > odtLocation.Rows(0)("Max_Volume") Then
                            W_MSG_Information("ปริมาตรในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub

                        End If
                    Else
                        W_MSG_Information("ไม่พบตำแหน่งจัดเก็บ " & Me._LocationID & " !" & vbNewLine & "กรุณาป้อนตำแหน่งใหม่")
                    End If
                End If

                'If intCountLocation > 0 Then
                '    If intCountLocation <> grdOrderItem.Rows.Count - 1 Then
                '        W_MSG_Information("กรุณาป้อนตำแหน่งจัดเก็บให้ครบทุกรายการ")
                '        Exit Sub
                '    End If
                'End If

            End If

            objHeader.Order_Index = Me._Order_Index
            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_No = Me.txtOrder_No.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            If txtOrder_No.Text = "" Then

                If _USE_BRANCH_ID Then
                    Dim strWhere As String = " Branch_ID ='" & WV_Branch_ID & "'"
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing
                Else
                    Dim strWhere As String = ""
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing

                End If

            End If

            objHeader.Lot_No = Me.txtLot_No.Text

            ' xxxxxxx Using TAG Properties xxxxxxxxxxx
            objHeader.Customer_Index = Me._Customer_Index.ToString
            If Me.txtSupplier_Id.Tag = Nothing Then
                objHeader.Supplier_Index = ""
            Else
                objHeader.Supplier_Index = Me.txtSupplier_Id.Tag.ToString
            End If

            If Me.txtDepartment_Id.Tag = Nothing Then
                objHeader.Department_Index = ""
            Else
                objHeader.Department_Index = Me.txtDepartment_Id.Tag.ToString
            End If
            If Me.txtConsignee_ID.Tag = Nothing Then
                objHeader.Consignee_Index = ""
            Else
                objHeader.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
            End If
            ' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_Time = Me.txtTime.Text
            objHeader.Ref_No1 = Me.txtRef_No1.Text
            objHeader.Ref_No2 = Me.txtRef_No2.Text
            objHeader.Ref_No3 = Me.txtRef_No3.Text
            objHeader.Ref_No4 = Me.txtRef_No4.Text
            objHeader.Ref_No5 = Me.txtRef_No5.Text
            objHeader.Str1 = Me.txtStr1.Text
            'objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str3 = Me.txtStr3.Text
            objHeader.Str4 = Me.txtStr4.Text
            objHeader.Str5 = Me.txtStr5.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            objHeader.Comment = Me.txtComment.Text
            objHeader.Str8 = Me.cboContact_Name.Text
            objHeader.PO_No = Me.txtPlanReceive.Text
            objHeader.Invoice_No = Me.txtInvoice_No.Text
            objHeader.ASN_No = Me.txtASN_No.Text
            'objHeader.Receive_Type = Me.chkReceiveType.Checked

            If cboType.SelectedValue IsNot Nothing Then
                objHeader.HandlingType_Index = cboType.SelectedValue
            Else
                objHeader.HandlingType_Index = ""
            End If

            'End If
            'killz 02-06-2011 objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str2 = IIf(Me.txtStr2.SelectedValue Is Nothing, "", Me.txtStr2.SelectedValue)
            'killz 02-06-2011 เก็บประเภทรถ ไว้คิดตัง
            objHeader.Str9 = IIf(Me.txtStr9.SelectedValue Is Nothing, "", Me.txtStr9.SelectedValue)
            ' *************************************************tap 2

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


            If txtQtyPck.Text = "" Then
                objHeader.Flo1 = 0
            Else
                objHeader.Flo1 = txtQtyPck.Text

            End If
            'For i As Integer = 0 To grdOrderItem.Rows.Count - 2

            'With grdOrderItem

            ' *** New Object *********
            objItem = New tb_OrderItem


            If (OrderItem_Index = Nothing) Then
                Dim objDBIndex As New Sy_AutoNumber
                OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                objItem.OrderItem_Index = OrderItem_Index
                Me.OrderItem_Id = objItem.OrderItem_Index
                objDBIndex = Nothing
            Else
                objItem.OrderItem_Index = OrderItem_Index
            End If
            objItem.Order_Index = objHeader.Order_Index

            'Plan_Qty
            If (IsNumeric(Qty)) Then
                objItem.Plan_Qty = CDbl(Qty)
            End If

            'Sku_Index
            If (Not (Sku_Index = Nothing)) Then
                objItem.Sku_Index = Sku_Index
            End If

            '12-01-2010 By ja saveConsignee_Index ตามฟิวใหม่ที่แอดลงใน tb_orderItem 
            If (Not (Consignee_Index = Nothing)) Then
                objItem.Consignee_Index = Consignee_Index
            End If

            'comment 
            If (Not (Comment = Nothing)) Then
                objItem.Comment = Comment
            End If

            'Qty
            objItem.Qty = CDec(Qty)
            If objItem.Qty = 0 Then
                MessageBox.Show("กรุณากรอกจำนวนสินค้า", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If (Not (Package_Index = Nothing)) Then
                ' ** .Tag
                objItem.Package_Index = Package_Index
            End If
            ' *** Get Retio ***
            Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
            objRatio = Nothing

            ' *****************
            ' *** Calculate Tatal Qty *** 
            objItem.Total_Qty = objItem.Qty * objItem.Ratio
            ' ***************************

            'Weight
            If (IsNumeric(Weight)) Then
                objItem.Weight = Weight
            End If

            'col_Net_Weight
            If (IsNumeric(NetWeight)) Then
                objItem.Flo1 = NetWeight
            End If

            'Volume
            If (IsNumeric(Volume)) Then
                objItem.Volume = Volume
            End If

            'PalletType_Index   
            If (Not (PalletType_Index = Nothing)) Then
                objItem.PalletType_Index = PalletType_Index
            End If

            'Pallet_Qty
            If (IsNumeric(Pallet_Qty)) Then
                objItem.Pallet_Qty = Pallet_Qty
            End If

            If (Not (Location_Alias = Nothing)) Then
                Me._LocationID = Location_Alias
                'Temp Location
                objItem.Str4 = Location_Alias
            End If

            objItem.ItemStatus_Index = ItemStatus_Index


            ' **** Calculate Exp_Date ***
            'Me.CalcualateDate_In_Gridview(i)
            ' ***************************

            ' *** value for date in gridview ***
            objItem.IsMfg_Date = IIf(IsDate(Mfg_Date), True, False)
            ''chkExp_date
            objItem.IsExp_Date = IIf(IsDate(Exp_Date), True, False)

            If (IsDate(Mfg_Date)) Then
                objItem.Mfg_Date = CDate(Mfg_Date)
            End If
            If (IsDate(Exp_Date)) Then
                objItem.Exp_date = CDate(Exp_Date)
            End If

            ' *** set default of IsMfg_Date and IsExp_Date For EFFEM *** 
            'objItem.IsMfg_Date = True
            'objItem.IsExp_Date = True
            ' **********************************************************

            'Is_SN
            objItem.Is_SN = False
            'Select Case .Rows(i).Cells("col_Qty").ReadOnly
            '    Case False
            '        objItem.Is_SN = False 'Not SN 
            '    Case True
            '        objItem.Is_SN = True 'IS SN
            '    Case Else
            '        objItem.Is_SN = False 'Not SN 
            'End Select


            ' Add new By Dong_kk

            'W
            If (IsNumeric(UnitWidth)) Then
                objItem.Flo2 = UnitWidth
            End If

            'L
            If (IsNumeric(UnitLength)) Then
                objItem.Flo3 = UnitLength
            End If

            'H
            If (IsNumeric(UnitHeight)) Then
                objItem.Flo4 = UnitHeight
            End If

            'Qty_Per_Pck
            objItem.Qty_Per_Pck = 1

            'Item_Qty
            objItem.Item_Qty = CDbl(Total_Qty)

            'Weight_Per_Pck
            If (IsNumeric(UnitWeight)) Then
                objItem.Weight_Per_Pck = CDec(UnitWeight)

            End If

            'Volume_Per_Pck
            If (IsNumeric(UnitVolume)) Then
                objItem.Volume_Per_Pck = UnitVolume

            End If

            'OderItem_Price
            If (IsNumeric(OrderItem_Price)) Then
                objItem.OrderItem_Price = OrderItem_Price
            End If

            'Price_Per_Pck
            If (IsNumeric(Price_Per_Pck)) Then
                objItem.Price_Per_Pck = Price_Per_Pck
            End If

            'PACKAGE ITEM
            If (Not (Item_Package_Index = Nothing)) Then
                objItem.Item_Package_Index = Item_Package_Index
            End If


            '********* DOCUMENT TYPE *********
            '--- Lot_No From Header
            objItem.Lot_No = objHeader.Lot_No



            Select Case cboPlanReceive.SelectedValue
                Case 9
                    '--- PO_No
                    If (Not (PO_No = Nothing)) Then
                        objItem.PO_No = PO_No
                    End If
                Case 7

                    If (Not (PO_No = Nothing)) Then
                        objItem.PO_No = PO_No
                    End If

                Case 16
                    '--- ASN_No,Shipment_No
                    If (Not (ASN_No = Nothing)) Then
                        objItem.ASN_No = ASN_No
                    End If
                Case 103

            End Select
            'PLOT
            If (Not (Plot = Nothing)) Then
                objItem.Plot = Plot
            End If
            'INVOICE
            If (Not (Invoice_No = Nothing)) Then
                objItem.Invoice_No = Invoice_No
            End If

            'Pallet No.
            If (Not (Pallet_No = Nothing)) Then
                objItem.Str5 = Pallet_No
            End If

            'PO  ******แก้จากเก็บ index เป็น เก็บ name [ชั่วคราว]
            If (Not (PO_Item = Nothing)) Then
                objItem.Str7 = PO_Item
            End If


            'Group NO
            If (Not (Group_No = Nothing)) Then
                objItem.Str9 = Group_No
            End If

            'POINDEX
            If (Not (PO_Item = Nothing)) Then
                objItem.Str10 = PO_Item
            End If

            'Decralation
            If (Not (Declaration_No = Nothing)) Then
                objItem.Declaration_No = Declaration_No
            End If



            If SetAUTO_REFERENCE() = 1 Then
                If (Reference = Nothing) Then
                    MessageBox.Show("Reference", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    objItem.Str1 = Reference
                End If
                '--- Referecne 2
                If (Reference2 = Nothing) Then
                    MessageBox.Show("Reference2", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    objItem.Str2 = Reference2
                End If

            Else

                If (Not (Reference = Nothing)) Then
                    objItem.Str1 = Reference
                Else
                    objItem.Str1 = ""
                End If
                '--- Referecne 2
                If (Not (Reference2 = Nothing)) Then
                    objItem.Str2 = Reference2
                Else
                    objItem.Str2 = ""
                End If
                '--- ItemDefinition_Index
                If (Not (ItemDefinition = Nothing)) Then
                    objItem.ItemDefinition_Index = ItemDefinition
                Else
                    objItem.ItemDefinition_Index = ""
                End If
            End If

            If (IsNumeric(OrderItem_RowIndex)) Then
                objItem.OrderItem_RowIndex = OrderItem_RowIndex
            End If

            If (Not (Serial_No = Nothing)) Then
                objItem.Serial_No = Serial_No
            End If


            'Lot/BATCTH
            ' **** Check isPlot ****
            ' *** You need to check that need Lot to used ***
            Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            If objSku.isPlot_Value(objItem.Sku_Index) = True Then
                ' *** Need to Input PLot in Order Item *** 
                If Trim(objItem.Plot).ToString = "" Then
                    MessageBox.Show("กรุณากรอก Lot ผลิตด้วย", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If





            ' HandlingType
            If (Not (HandlingType_Index = Nothing)) Then
                objItem.HandlingType_Index = HandlingType_Index
            End If

            ' Plan_Process

            If (Not (DocumentPlan_Index = Nothing)) Then
                objItem.DocumentPlan_Index = DocumentPlan_Index
            Else
                objItem.DocumentPlan_Index = ""
            End If
            If (Not (DocumentPlanItem_Index = Nothing)) Then
                objItem.DocumentPlanItem_Index = DocumentPlanItem_Index
            Else
                objItem.DocumentPlanItem_Index = ""
            End If
            If (Not (DocumentPlan_No = Nothing)) Then
                objItem.DocumentPlan_No = DocumentPlan_No
            Else
                objItem.DocumentPlan_No = ""
            End If

            If (Not (Plan_Process = Nothing)) Then
                objItem.Plan_Process = CInt(Plan_Process)
            Else
                objItem.Plan_Process = -9
            End If

            'include from master site
            If (Not (Str6 = Nothing)) Then
                objItem.Str6 = Str6
            End If

            If (IsNumeric(Tax1)) Then
                objItem.Tax1 = Tax1
            End If

            If (IsNumeric(Tax2)) Then
                objItem.Tax2 = Tax2
            End If

            If (IsNumeric(Tax3)) Then
                objItem.Tax3 = Tax3
            End If

            If (IsNumeric(Tax4)) Then
                objItem.Tax4 = Tax4
            End If

            If (IsNumeric(Tax5)) Then
                objItem.Tax5 = Tax5
            End If

            'add new 14/10/2009
            If (Not (HS_Code = Nothing)) Then
                objItem.HS_Code = HS_Code
            End If

            If (Not (ItemDescription = Nothing)) Then
                objItem.ItemDescription = ItemDescription
            End If


            If (IsNumeric(Seq)) Then
                objItem.Seq = Seq
            End If


            If (Not (ERP_Location = Nothing)) Then
                objItem.ERP_Location = ERP_Location
            End If


            'Begin : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 
            Dim dblQty_Receive As Double = 0
            Dim dblTotal_Qty_Receive As Double = 0
            Dim dtCheckPlanReceive As New DataTable
            With dtCheckPlanReceive.Columns
                .Add("DocumentPlanItem_Index", GetType(String))
                .Add("Plan_Process", GetType(Double))
                .Add("PlanQty", GetType(Double))
                .Add("Qty", GetType(Double))
            End With

            If Not String.IsNullOrEmpty(objItem.DocumentPlanItem_Index) Then

                'dong edit 2014-05-25
                Select Case objItem.Plan_Process
                    Case 9
                        Dim oPOI As New ml_Receive_PO
                        Dim dtPO As New DataTable
                        oPOI.getPo_byPoi_Index(objItem.DocumentPlanItem_Index)
                        dtPO = oPOI.GetDataTable
                        If dtPO.Rows.Count > 0 Then
                            'Local Receive
                            If dtCheckPlanReceive.Rows.Count > 0 Then
                                dblQty_Receive = dtCheckPlanReceive.Compute("SUM(Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                                dblTotal_Qty_Receive = dtCheckPlanReceive.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                            End If
                            'Real Oi Receive
                            Dim oOI As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
                            Dim dtOI As New DataTable
                            oOI.getOrderItemAll(objItem.OrderItem_Index)
                            dtOI = oOI.GetDataTable
                            Dim dblQtyOI As Double = 0
                            Dim dblTotal_Qty As Double = 0
                            If dtOI.Rows.Count > 0 Then
                                dblQtyOI = IIf(IsNumeric(dtOI.Rows(0)("Qty")), dtOI.Rows(0)("Qty"), 0)
                                dblTotal_Qty = IIf(IsNumeric(dtOI.Rows(0)("Total_Qty")), dtOI.Rows(0)("Total_Qty"), 0)
                            End If

                            'Online Receive
                            dtPO.Rows(0)("Qty_Bal") = IIf(IsNumeric(dtPO.Rows(0)("Qty_Bal")), dtPO.Rows(0)("Qty_Bal"), 0)
                            dtPO.Rows(0)("Qty") = IIf(IsNumeric(dtPO.Rows(0)("Qty")), dtPO.Rows(0)("Qty"), 0)
                            dtPO.Rows(0)("Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Received_Qty")), dtPO.Rows(0)("Received_Qty"), 0)
                            dtPO.Rows(0)("Total_Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Total_Received_Qty")), dtPO.Rows(0)("Total_Received_Qty"), 0)

                            Dim Qty_Receive_Real As Double = (dtPO.Rows(0)("Received_Qty") - dblQty_Receive - dblQtyOI) + objItem.Total_Qty

                            Dim Total_Qty_Receive_Real As Double = 0

                            Total_Qty_Receive_Real = (dtPO.Rows(0)("Total_Received_Qty") - dblTotal_Qty_Receive - dblQtyOI) + objItem.Total_Qty

                            If dtPO.Rows(0)("Total_Qty") < Total_Qty_Receive_Real Then
                                W_MSG_Information("บรรทัดที่ " & (0 + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "" & "")
                                'W_MSG_Information("บรรทัดที่ " & (i + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "จำนวนเกิน " & "")
                                Exit Sub

                            End If

                        End If

                        Dim drNewRow As DataRow = dtCheckPlanReceive.NewRow
                        drNewRow("Qty") = objItem.Qty
                        drNewRow("PlanQty") = objItem.Plan_Qty
                        drNewRow("Plan_Process") = objItem.Plan_Process
                        drNewRow("DocumentPlanItem_Index") = objItem.DocumentPlanItem_Index
                        dtCheckPlanReceive.Rows.Add(drNewRow)

                End Select
            End If

            'End : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 



            If (IsNumeric(OrderItem_RowIndex)) Then
                objItem.iRow_OrderItem_Index = OrderItem_RowIndex
            End If

            objItem.WeightScale = WeightScale
            'End With
            ' *** Add value ***
            objItemCollection.Add(objItem)

            'Next

            '' PALLETTYPE
            'Dim J As Integer = 0

            'For J = 0 To grdPallet.Rows.Count - 1
            '    If grdPallet.Rows(J).Cells("col_UsePallet").Value <> 0 Then

            '        With grdPallet

            '            ' *** New Object *********
            '            objPalletType = New tb_PalletType_History
            '            ' ************************

            '            If .Rows(J).Cells("col_PalletType_History_Index").Value = "" Then
            '                Dim objDBIndex As New Sy_AutoNumber
            '                .Rows(J).Cells("col_PalletType_History_Index").Value = objDBIndex.getSys_Value("PalletType_History_Index ")
            '                objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value
            '                objDBIndex = Nothing
            '            Else
            '                objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value.ToString
            '            End If

            '            'PalletType_Index
            '            If .Rows(J).Cells("col_Palletindex").Value IsNot Nothing Then
            '                objPalletType.PalletType_Index = .Rows(J).Cells("col_Palletindex").Value
            '            End If

            '            'Qty_In
            '            If .Rows(J).Cells("col_UsePallet").Value IsNot Nothing Then
            '                objPalletType.Qty_In = .Rows(J).Cells("col_UsePallet").Value
            '            End If

            '            'Qty_Bal
            '            If .Rows(J).Cells("col_Palletqty").Value IsNot Nothing Then
            '                objPalletType.Qty_Bal = .Rows(J).Cells("col_Palletqty").Value
            '            End If

            '        End With
            '        objPalletTypeCollection.Add(objPalletType)
            '    End If
            'Next

            ' *** Call Class for Manage Data ***
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.UPDATE, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing

            End Select


            '====== BEGIN Putaway  by OrderItem =====
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim bAutoPutaway As Boolean = False
                For i As Integer = 0 To objItemCollection.Count - 1
                    Me._LocationID = Location_Alias
                    Me.OrderItem_Id = objItemCollection(i).OrderItem_Index

                    If (_LocationID IsNot Nothing) And (Not _LocationID = "") Then
                        SetLocation_fromitemAll(i)
                        bAutoPutaway = True
                    End If
                Next

                If bAutoPutaway Then
                    Dim objClassDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
                    objClassDB.SaveData(Me._Order_Index)
                End If
            End If

            '====== END Putaway  by OrderItem =====

            ' BGN Manage Tag
            For i As Integer = 0 To objItemCollection.Count - 1
                Me.SaveTag_No(objItemCollection(i).OrderItem_Index)
                Dim ObjClass As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
                ObjClass.getView_Tag_Header(String.Format(" and Tag_Process_Id = 1 and OrderItem_Index='{0}' ", Replace(OrderItem_Index, "'", "''")))
                Dim dtTag As DataTable = ObjClass.GetDataTable()
                If (dtTag.Rows.Count > 0) Then
                    Tag_Index = dtTag.Rows(0).Item("Tag_Index").ToString()
                    Tag_No = dtTag.Rows(0).Item("TAG_No").ToString()
                Else
                    ' Delete OrderItem
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
                    objDB.Delete_OrderItem(objItemCollection(i).OrderItem_Index)
                End If
            Next
            ' END Manage Tag

            If Not Me._Order_Index = "" Then
                _Order_Index = Me._Order_Index
            End If

            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnCopy.Enabled = False
            Me.btnPalletSlip.Enabled = False

            '------ Update Comment Image
            'Dim odjOrderImage As New tb_Order_Image

            'For iRow As Integer = 0 To grdOrderImage.RowCount - 1
            '    With odjOrderImage
            '        .Order_Index = _Order_Index
            '        .Order_Image_Index = grdOrderImage.Rows(iRow).Cells("Col_Order_Image_Index").Value.ToString
            '        .Comment = grdOrderImage.Rows(iRow).Cells("Col_Comment").Value.ToString
            '        .Update()
            '    End With
            'Next


            'include from master site
            If Not Me._Order_Index = "" Then
                'W_MSG_Information_ByIndex(1)
                Me.getOrderItemDetail(Me._Order_Index)
                'W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
                _Order_Index = Me._Order_Index
                cboPrint.Enabled = True
                btnPrintReport.Enabled = True
                objStatus = enuOperation_Type.UPDATE
                Me.getOrderHeader(_Order_Index)
                Me.grdOrderItem.ReadOnly = True
            Else
                W_MSG_Information_ByIndex(2)
                Exit Sub
            End If

            If (Tag_Index = Nothing) Then
                W_MSG_Information("บันทึกข้อมูลไม่สำเร็จ, กรุณาลองใหม่")
            End If
        Catch ex As Exception
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
            W_MSG_Error(ex.Message)
        Finally
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
        End Try
    End Sub

    Public Sub SavePalletSlip_v3(ByRef Tag_Index As String, ByRef Tag_No As String, ByVal Ref_No1 As String, ByVal Ref_No2 As String, ByVal Sku_Index As String, ByVal Package_Index As String, ByVal Qty As String, ByVal Ratio As String, ByVal Total_Qty As String, ByVal ItemStatus_Index As String, ByVal Plot As String, ByVal Mfg_Date As String, ByVal Exp_Date As String, ByVal UnitWeight As Decimal, ByVal UnitVolume As Decimal, ByVal UnitWidth As Decimal, ByVal UnitLength As Decimal, ByVal UnitHeight As Decimal, ByVal PurchaseOrderItem_Index As String, ByVal PurchaseOrder_Index As String, ByVal PurchaseOrder_No As String, Optional ByVal WeightScale As Decimal = 0, Optional ByVal Tank_Weight As Decimal = 0)

        ' **************************
        ' *** Loop to Check Unique Item Line In datagrid **
        ' *** option for check unique product ***
        ' *************************************************

        Dim objHeader As New tb_Order
        Dim objItem As New tb_OrderItem
        Dim objItemCollection As New List(Of tb_OrderItem)
        Dim objms_ItemStatus As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim ItemLife_Total_Day As Integer = 0

        ' PalletType_History
        Dim objPalletType As New tb_PalletType_History
        Dim objPalletTypeCollection As New List(Of tb_PalletType_History)

        Dim Max_Seq As Integer = New clsPrintPalletSlip().getOrderItemCount(Me.Order_Index)
        Dim OrderItem_Index As String = ""
        Dim Consignee_Index As String = ""
        Dim Comment As String = ""
        Dim Location_Alias As String = ""
        Dim Weight As Decimal = CDec(Qty) * CDec(UnitWeight)
        Dim NetWeight As Decimal = CDec(Qty) * CDec(UnitWeight)
        Dim Volume As Decimal = CDec(Qty) * CDec(UnitVolume)
        Dim NetVolume As Decimal = CDec(Qty) * CDec(UnitVolume)
        Dim PalletType_Index As String = ""
        Dim Pallet_Qty As Decimal = Tank_Weight
        Dim OrderItem_Price As Decimal = 0
        Dim Price_Per_Pck As Decimal = 0
        Dim Item_Package_Index As String = ""
        Dim PO_No As String = ""
        Dim ASN_No As String = ""
        Dim Invoice_No As String = ""
        Dim Pallet_No As String = ""
        Dim PO_Item As String = ""
        Dim Group_No As String = ""
        Dim Declaration_No As String = ""
        Dim Reference As String = Ref_No1
        Dim Reference2 As String = Ref_No2
        Dim ItemDefinition As String = ""
        Dim OrderItem_RowIndex As String = Max_Seq
        Dim Serial_No As String = ""
        Dim HandlingType_Index As String = ""
        Dim DocumentPlan_Index As String = PurchaseOrder_Index
        Dim DocumentPlanItem_Index As String = PurchaseOrderItem_Index
        Dim DocumentPlan_No As String = PurchaseOrder_No
        Dim Plan_Process As String = IIf(PurchaseOrderItem_Index = Nothing, "", "9")
        Dim Str6 As String = ""
        Dim Tax1 As Decimal = 0
        Dim Tax2 As Decimal = 0
        Dim Tax3 As Decimal = 0
        Dim Tax4 As Decimal = 0
        Dim Tax5 As Decimal = 0
        Dim HS_Code As String = ""
        Dim ItemDescription As String = ""
        Dim Seq As String = Max_Seq + 1
        Dim ERP_Location As String = ""

        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            '************* Begin Comment When Use Function Validate ***************** 
            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderItem, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdPallet, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdSerial, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderImage, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdOrderTruckIn, 1)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            '*** Check Important Data ***
            If Me._Customer_Index = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If
            '************* End Comment When Use Function Validate ***************** 

            ' *** Check Product Item ***
            If (Sku_Index = Nothing Or Package_Index = Nothing) Then
                W_MSG_Information("กรุณากรอกรายการสินค้า")
                Exit Sub
            End If

            ' *** BEGIN Checking Putaway all item
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim intCountLocation As Integer = 0

                '************* Begin Comment When Use Function Validate ***************** 
                'Checking SKU 
                If (Sku_Index = Nothing) Then
                    W_MSG_Information("กรุณากรอกรายการสินค้าให้ครบ")
                    Exit Sub
                End If

                '************* End Comment When Use Function Validate ***************** 

                Me._LocationID = Location_Alias
                If (Me._LocationID IsNot Nothing) And (Not _LocationID = "") Then
                    intCountLocation += 1

                    'Check Location
                    Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    Dim odtLocation As New DataTable
                    oms_Location.SearchData_Click("*", " AND Location_Alias = '" & Me._LocationID.Replace("'", "''").Trim & "'")
                    odtLocation = oms_Location.GetDataTable

                    If odtLocation.Rows.Count > 0 Then
                        Dim dblQty, dblWeight, dblVolume As Double

                        dblQty = odtLocation.Rows(0)("Current_Qty").ToString
                        dblWeight = odtLocation.Rows(0)("Current_Weight").ToString
                        dblVolume = odtLocation.Rows(0)("Current_Volume").ToString

                        If dblQty + CDbl(Qty) > odtLocation.Rows(0)("Max_Qty") Then
                            W_MSG_Information("จำนวนในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub
                        ElseIf dblQty + NetWeight > odtLocation.Rows(0)("Max_Weight") Then
                            W_MSG_Information("น้ำหนักในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub
                        ElseIf dblQty + NetVolume > odtLocation.Rows(0)("Max_Volume") Then
                            W_MSG_Information("ปริมาตรในตำแหน่ง " & Me._LocationID & " มีไม่เพียงพอ")
                            Exit Sub

                        End If
                    Else
                        W_MSG_Information("ไม่พบตำแหน่งจัดเก็บ " & Me._LocationID & " !" & vbNewLine & "กรุณาป้อนตำแหน่งใหม่")
                    End If
                End If

                'If intCountLocation > 0 Then
                '    If intCountLocation <> grdOrderItem.Rows.Count - 1 Then
                '        W_MSG_Information("กรุณาป้อนตำแหน่งจัดเก็บให้ครบทุกรายการ")
                '        Exit Sub
                '    End If
                'End If

            End If

            objHeader.Order_Index = Me._Order_Index
            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_No = Me.txtOrder_No.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            If txtOrder_No.Text = "" Then

                If _USE_BRANCH_ID Then
                    Dim strWhere As String = " Branch_ID ='" & WV_Branch_ID & "'"
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing
                Else
                    Dim strWhere As String = ""
                    Dim objDocumentNumber As New clsDocumentNumber()
                    objHeader.Order_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, objHeader.Order_Date) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                    txtOrder_No.Text = objHeader.Order_No
                    objDocumentNumber = Nothing

                End If

            End If

            objHeader.Lot_No = Me.txtLot_No.Text

            ' xxxxxxx Using TAG Properties xxxxxxxxxxx
            objHeader.Customer_Index = Me._Customer_Index.ToString
            If Me.txtSupplier_Id.Tag = Nothing Then
                objHeader.Supplier_Index = ""
            Else
                objHeader.Supplier_Index = Me.txtSupplier_Id.Tag.ToString
            End If

            If Me.txtDepartment_Id.Tag = Nothing Then
                objHeader.Department_Index = ""
            Else
                objHeader.Department_Index = Me.txtDepartment_Id.Tag.ToString
            End If
            If Me.txtConsignee_ID.Tag = Nothing Then
                objHeader.Consignee_Index = ""
            Else
                objHeader.Consignee_Index = Me.txtConsignee_ID.Tag.ToString
            End If
            ' xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

            objHeader.Order_Date = Me.dtOrder.Value
            objHeader.Order_Time = Me.txtTime.Text
            objHeader.Ref_No1 = Me.txtRef_No1.Text
            objHeader.Ref_No2 = Me.txtRef_No2.Text
            objHeader.Ref_No3 = Me.txtRef_No3.Text
            objHeader.Ref_No4 = Me.txtRef_No4.Text
            objHeader.Ref_No5 = Me.txtRef_No5.Text
            objHeader.Str1 = Me.txtStr1.Text
            'objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str3 = Me.txtStr3.Text
            objHeader.Str4 = Me.txtStr4.Text
            objHeader.Str5 = Me.txtStr5.Text
            objHeader.DocumentType_Index = Me.cboDocumentType.SelectedValue()
            objHeader.Comment = Me.txtComment.Text
            objHeader.Str8 = Me.cboContact_Name.Text
            objHeader.PO_No = Me.txtPlanReceive.Text
            objHeader.Invoice_No = Me.txtInvoice_No.Text
            objHeader.ASN_No = Me.txtASN_No.Text
            'objHeader.Receive_Type = Me.chkReceiveType.Checked

            If cboType.SelectedValue IsNot Nothing Then
                objHeader.HandlingType_Index = cboType.SelectedValue
            Else
                objHeader.HandlingType_Index = ""
            End If

            'End If
            'killz 02-06-2011 objHeader.Str2 = Me.txtStr2.Text
            objHeader.Str2 = IIf(Me.txtStr2.SelectedValue Is Nothing, "", Me.txtStr2.SelectedValue)
            'killz 02-06-2011 เก็บประเภทรถ ไว้คิดตัง
            objHeader.Str9 = IIf(Me.txtStr9.SelectedValue Is Nothing, "", Me.txtStr9.SelectedValue)
            ' *************************************************tap 2

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


            If txtQtyPck.Text = "" Then
                objHeader.Flo1 = 0
            Else
                objHeader.Flo1 = txtQtyPck.Text

            End If
            'For i As Integer = 0 To grdOrderItem.Rows.Count - 2

            'With grdOrderItem

            ' *** New Object *********
            objItem = New tb_OrderItem


            If (OrderItem_Index = Nothing) Then
                Dim objDBIndex As New Sy_AutoNumber
                OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                objItem.OrderItem_Index = OrderItem_Index
                Me.OrderItem_Id = objItem.OrderItem_Index
                objDBIndex = Nothing
            Else
                objItem.OrderItem_Index = OrderItem_Index
            End If
            objItem.Order_Index = objHeader.Order_Index

            'Plan_Qty
            If (IsNumeric(Qty)) Then
                objItem.Plan_Qty = CDbl(Qty)
            End If

            'Sku_Index
            If (Not (Sku_Index = Nothing)) Then
                objItem.Sku_Index = Sku_Index
            End If

            '12-01-2010 By ja saveConsignee_Index ตามฟิวใหม่ที่แอดลงใน tb_orderItem 
            If (Not (Consignee_Index = Nothing)) Then
                objItem.Consignee_Index = Consignee_Index
            End If

            'comment 
            If (Not (Comment = Nothing)) Then
                objItem.Comment = Comment
            End If

            'Qty
            objItem.Qty = CDec(Qty)
            If objItem.Qty = 0 Then
                MessageBox.Show("กรุณากรอกจำนวนสินค้า", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            If (Not (Package_Index = Nothing)) Then
                ' ** .Tag
                objItem.Package_Index = Package_Index
            End If
            ' *** Get Retio ***
            Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            objItem.Ratio = objRatio.getRatio(objItem.Sku_Index, objItem.Package_Index)
            objRatio = Nothing

            ' *****************
            ' *** Calculate Tatal Qty *** 
            objItem.Total_Qty = objItem.Qty * objItem.Ratio
            ' ***************************

            'Weight
            If (IsNumeric(Weight)) Then
                objItem.Weight = Weight
            End If

            'col_Net_Weight
            If (IsNumeric(NetWeight)) Then
                objItem.Flo1 = NetWeight
            End If

            'Volume
            If (IsNumeric(Volume)) Then
                objItem.Volume = Volume
            End If

            'PalletType_Index   
            If (Not (PalletType_Index = Nothing)) Then
                objItem.PalletType_Index = PalletType_Index
            End If

            'Pallet_Qty
            If (IsNumeric(Pallet_Qty)) Then
                objItem.Pallet_Qty = Pallet_Qty * objItem.Ratio ' Total
            End If

            If (Not (Location_Alias = Nothing)) Then
                Me._LocationID = Location_Alias
                'Temp Location
                objItem.Str4 = Location_Alias
            End If

            objItem.ItemStatus_Index = ItemStatus_Index


            ' **** Calculate Exp_Date ***
            'Me.CalcualateDate_In_Gridview(i)
            ' ***************************

            ' *** value for date in gridview ***
            objItem.IsMfg_Date = IIf(IsDate(Mfg_Date), True, False)
            ''chkExp_date
            objItem.IsExp_Date = IIf(IsDate(Exp_Date), True, False)

            If (IsDate(Mfg_Date)) Then
                objItem.Mfg_Date = CDate(Mfg_Date)
            End If
            If (IsDate(Exp_Date)) Then
                objItem.Exp_date = CDate(Exp_Date)
            End If

            ' *** set default of IsMfg_Date and IsExp_Date For EFFEM *** 
            'objItem.IsMfg_Date = True
            'objItem.IsExp_Date = True
            ' **********************************************************

            'Is_SN
            objItem.Is_SN = False
            'Select Case .Rows(i).Cells("col_Qty").ReadOnly
            '    Case False
            '        objItem.Is_SN = False 'Not SN 
            '    Case True
            '        objItem.Is_SN = True 'IS SN
            '    Case Else
            '        objItem.Is_SN = False 'Not SN 
            'End Select


            ' Add new By Dong_kk

            'W
            If (IsNumeric(UnitWidth)) Then
                objItem.Flo2 = UnitWidth
            End If

            'L
            If (IsNumeric(UnitLength)) Then
                objItem.Flo3 = UnitLength
            End If

            'H
            If (IsNumeric(UnitHeight)) Then
                objItem.Flo4 = UnitHeight
            End If

            'Qty_Per_Pck
            objItem.Qty_Per_Pck = 1

            'Item_Qty
            objItem.Item_Qty = CDbl(Total_Qty)

            'Weight_Per_Pck
            If (IsNumeric(UnitWeight)) Then
                objItem.Weight_Per_Pck = CDec(UnitWeight)

            End If

            'Volume_Per_Pck
            If (IsNumeric(UnitVolume)) Then
                objItem.Volume_Per_Pck = UnitVolume

            End If

            'OderItem_Price
            If (IsNumeric(OrderItem_Price)) Then
                objItem.OrderItem_Price = OrderItem_Price
            End If

            'Price_Per_Pck
            If (IsNumeric(Price_Per_Pck)) Then
                objItem.Price_Per_Pck = Price_Per_Pck
            End If

            'PACKAGE ITEM
            If (Not (Item_Package_Index = Nothing)) Then
                objItem.Item_Package_Index = Item_Package_Index
            End If


            '********* DOCUMENT TYPE *********
            '--- Lot_No From Header
            objItem.Lot_No = objHeader.Lot_No



            Select Case cboPlanReceive.SelectedValue
                Case 9
                    '--- PO_No
                    If (Not (PO_No = Nothing)) Then
                        objItem.PO_No = PO_No
                    End If
                Case 7

                    If (Not (PO_No = Nothing)) Then
                        objItem.PO_No = PO_No
                    End If

                Case 16
                    '--- ASN_No,Shipment_No
                    If (Not (ASN_No = Nothing)) Then
                        objItem.ASN_No = ASN_No
                    End If
                Case 103

            End Select
            'PLOT
            If (Not (Plot = Nothing)) Then
                objItem.Plot = Plot
            End If
            'INVOICE
            If (Not (Invoice_No = Nothing)) Then
                objItem.Invoice_No = Invoice_No
            End If

            'Pallet No.
            If (Not (Pallet_No = Nothing)) Then
                objItem.Str5 = Pallet_No
            End If

            'PO  ******แก้จากเก็บ index เป็น เก็บ name [ชั่วคราว]
            If (Not (PO_Item = Nothing)) Then
                objItem.Str7 = PO_Item
            End If


            'Group NO
            If (Not (Group_No = Nothing)) Then
                objItem.Str9 = Group_No
            End If

            'POINDEX
            If (Not (PO_Item = Nothing)) Then
                objItem.Str10 = PO_Item
            End If

            'Decralation
            If (Not (Declaration_No = Nothing)) Then
                objItem.Declaration_No = Declaration_No
            End If



            If SetAUTO_REFERENCE() = 1 Then
                If (Reference = Nothing) Then
                    MessageBox.Show("Reference", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    objItem.Str1 = Reference
                End If
                '--- Referecne 2
                If (Reference2 = Nothing) Then
                    MessageBox.Show("Reference2", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                Else
                    objItem.Str2 = Reference2
                End If

            Else

                If (Not (Reference = Nothing)) Then
                    objItem.Str1 = Reference
                Else
                    objItem.Str1 = ""
                End If
                '--- Referecne 2
                If (Not (Reference2 = Nothing)) Then
                    objItem.Str2 = Reference2
                Else
                    objItem.Str2 = ""
                End If
                '--- ItemDefinition_Index
                If (Not (ItemDefinition = Nothing)) Then
                    objItem.ItemDefinition_Index = ItemDefinition
                Else
                    objItem.ItemDefinition_Index = ""
                End If
            End If

            If (IsNumeric(OrderItem_RowIndex)) Then
                objItem.OrderItem_RowIndex = OrderItem_RowIndex
            End If

            If (Not (Serial_No = Nothing)) Then
                objItem.Serial_No = Serial_No
            End If


            'Lot/BATCTH
            ' **** Check isPlot ****
            ' *** You need to check that need Lot to used ***
            Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            If objSku.isPlot_Value(objItem.Sku_Index) = True Then
                ' *** Need to Input PLot in Order Item *** 
                If Trim(objItem.Plot).ToString = "" Then
                    MessageBox.Show("กรุณากรอก Lot ผลิตด้วย", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    Exit Sub
                End If
            End If





            ' HandlingType
            If (Not (HandlingType_Index = Nothing)) Then
                objItem.HandlingType_Index = HandlingType_Index
            End If

            ' Plan_Process

            If (Not (DocumentPlan_Index = Nothing)) Then
                objItem.DocumentPlan_Index = DocumentPlan_Index
            Else
                objItem.DocumentPlan_Index = ""
            End If
            If (Not (DocumentPlanItem_Index = Nothing)) Then
                objItem.DocumentPlanItem_Index = DocumentPlanItem_Index
            Else
                objItem.DocumentPlanItem_Index = ""
            End If
            If (Not (DocumentPlan_No = Nothing)) Then
                objItem.DocumentPlan_No = DocumentPlan_No
            Else
                objItem.DocumentPlan_No = ""
            End If

            If (Not (Plan_Process = Nothing)) Then
                objItem.Plan_Process = CInt(Plan_Process)
            Else
                objItem.Plan_Process = -9
            End If

            'include from master site
            If (Not (Str6 = Nothing)) Then
                objItem.Str6 = Str6
            End If

            If (IsNumeric(Tax1)) Then
                objItem.Tax1 = Tax1
            End If

            If (IsNumeric(Tax2)) Then
                objItem.Tax2 = Tax2
            End If

            If (IsNumeric(Tax3)) Then
                objItem.Tax3 = Tax3
            End If

            If (IsNumeric(Tax4)) Then
                objItem.Tax4 = Tax4
            End If

            If (IsNumeric(Tax5)) Then
                objItem.Tax5 = Tax5
            End If

            'add new 14/10/2009
            If (Not (HS_Code = Nothing)) Then
                objItem.HS_Code = HS_Code
            End If

            If (Not (ItemDescription = Nothing)) Then
                objItem.ItemDescription = ItemDescription
            End If


            If (IsNumeric(Seq)) Then
                objItem.Seq = Seq
            End If


            If (Not (ERP_Location = Nothing)) Then
                objItem.ERP_Location = ERP_Location
            End If


            'Begin : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 
            Dim dblQty_Receive As Double = 0
            Dim dblTotal_Qty_Receive As Double = 0
            Dim dtCheckPlanReceive As New DataTable
            With dtCheckPlanReceive.Columns
                .Add("DocumentPlanItem_Index", GetType(String))
                .Add("Plan_Process", GetType(Double))
                .Add("PlanQty", GetType(Double))
                .Add("Qty", GetType(Double))
            End With

            If Not String.IsNullOrEmpty(objItem.DocumentPlanItem_Index) Then

                'dong edit 2014-05-25
                Select Case objItem.Plan_Process
                    Case 9
                        Dim oPOI As New ml_Receive_PO
                        Dim dtPO As New DataTable
                        oPOI.getPo_byPoi_Index(objItem.DocumentPlanItem_Index)
                        dtPO = oPOI.GetDataTable
                        If dtPO.Rows.Count > 0 Then
                            'Local Receive
                            If dtCheckPlanReceive.Rows.Count > 0 Then
                                dblQty_Receive = dtCheckPlanReceive.Compute("SUM(Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                                dblTotal_Qty_Receive = dtCheckPlanReceive.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index='" & objItem.DocumentPlanItem_Index & "' and Plan_Process = " & objItem.Plan_Process)
                            End If
                            'Real Oi Receive
                            Dim oOI As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
                            Dim dtOI As New DataTable
                            oOI.getOrderItemAll(objItem.OrderItem_Index)
                            dtOI = oOI.GetDataTable
                            Dim dblQtyOI As Double = 0
                            Dim dblTotal_Qty As Double = 0
                            If dtOI.Rows.Count > 0 Then
                                dblQtyOI = IIf(IsNumeric(dtOI.Rows(0)("Qty")), dtOI.Rows(0)("Qty"), 0)
                                dblTotal_Qty = IIf(IsNumeric(dtOI.Rows(0)("Total_Qty")), dtOI.Rows(0)("Total_Qty"), 0)
                            End If

                            'Online Receive
                            dtPO.Rows(0)("Qty_Bal") = IIf(IsNumeric(dtPO.Rows(0)("Qty_Bal")), dtPO.Rows(0)("Qty_Bal"), 0)
                            dtPO.Rows(0)("Qty") = IIf(IsNumeric(dtPO.Rows(0)("Qty")), dtPO.Rows(0)("Qty"), 0)
                            dtPO.Rows(0)("Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Received_Qty")), dtPO.Rows(0)("Received_Qty"), 0)
                            dtPO.Rows(0)("Total_Received_Qty") = IIf(IsNumeric(dtPO.Rows(0)("Total_Received_Qty")), dtPO.Rows(0)("Total_Received_Qty"), 0)

                            Dim Qty_Receive_Real As Double = (dtPO.Rows(0)("Received_Qty") - dblQty_Receive - dblQtyOI) + objItem.Total_Qty

                            Dim Total_Qty_Receive_Real As Double = 0

                            Total_Qty_Receive_Real = (dtPO.Rows(0)("Total_Received_Qty") - dblTotal_Qty_Receive - dblQtyOI) + objItem.Total_Qty
                            'No_Sure #Commet รับเข้าที่รับครบแล้ว
                            'If dtPO.Rows(0)("Total_Qty") < Total_Qty_Receive_Real Then
                            '    W_MSG_Information("บรรทัดที่ " & (0 + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "" & "")
                            '    'W_MSG_Information("บรรทัดที่ " & (i + 1).ToString & " ไม่สามารถรับสินค้าเกินจำนวนค้างรับได้" & Chr(13) & "จำนวนเกิน " & "")
                            '    Exit Sub

                            'End If

                        End If

                        Dim drNewRow As DataRow = dtCheckPlanReceive.NewRow
                        drNewRow("Qty") = objItem.Qty
                        drNewRow("PlanQty") = objItem.Plan_Qty
                        drNewRow("Plan_Process") = objItem.Plan_Process
                        drNewRow("DocumentPlanItem_Index") = objItem.DocumentPlanItem_Index
                        dtCheckPlanReceive.Rows.Add(drNewRow)

                End Select
            End If

            'End : TOP Add Date : 14/03/13  : Validate value Recieve not more PO 



            If (IsNumeric(OrderItem_RowIndex)) Then
                objItem.iRow_OrderItem_Index = OrderItem_RowIndex
            End If

            objItem.WeightScale = WeightScale
            'End With
            ' *** Add value ***
            objItemCollection.Add(objItem)

            'Next

            '' PALLETTYPE
            'Dim J As Integer = 0

            'For J = 0 To grdPallet.Rows.Count - 1
            '    If grdPallet.Rows(J).Cells("col_UsePallet").Value <> 0 Then

            '        With grdPallet

            '            ' *** New Object *********
            '            objPalletType = New tb_PalletType_History
            '            ' ************************

            '            If .Rows(J).Cells("col_PalletType_History_Index").Value = "" Then
            '                Dim objDBIndex As New Sy_AutoNumber
            '                .Rows(J).Cells("col_PalletType_History_Index").Value = objDBIndex.getSys_Value("PalletType_History_Index ")
            '                objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value
            '                objDBIndex = Nothing
            '            Else
            '                objPalletType.PalletType_History_Index = .Rows(J).Cells("col_PalletType_History_Index").Value.ToString
            '            End If

            '            'PalletType_Index
            '            If .Rows(J).Cells("col_Palletindex").Value IsNot Nothing Then
            '                objPalletType.PalletType_Index = .Rows(J).Cells("col_Palletindex").Value
            '            End If

            '            'Qty_In
            '            If .Rows(J).Cells("col_UsePallet").Value IsNot Nothing Then
            '                objPalletType.Qty_In = .Rows(J).Cells("col_UsePallet").Value
            '            End If

            '            'Qty_Bal
            '            If .Rows(J).Cells("col_Palletqty").Value IsNot Nothing Then
            '                objPalletType.Qty_Bal = .Rows(J).Cells("col_Palletqty").Value
            '            End If

            '        End With
            '        objPalletTypeCollection.Add(objPalletType)
            '    End If
            'Next

            ' *** Call Class for Manage Data ***
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.UPDATE, objHeader, objItemCollection, objPalletTypeCollection)
                    Me._Order_Index = objDB.SaveData
                    Me.txtOrder_No.Text = objHeader.Order_No
                    objDB = Nothing

            End Select


            '====== BEGIN Putaway  by OrderItem =====
            If Me._USE_PUTAWAY_BTNCONFIRM = False Then
                Dim bAutoPutaway As Boolean = False
                For i As Integer = 0 To objItemCollection.Count - 1
                    Me._LocationID = Location_Alias
                    Me.OrderItem_Id = objItemCollection(i).OrderItem_Index

                    If (_LocationID IsNot Nothing) And (Not _LocationID = "") Then
                        SetLocation_fromitemAll(i)
                        bAutoPutaway = True
                    End If
                Next

                If bAutoPutaway Then
                    Dim objClassDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
                    objClassDB.SaveData(Me._Order_Index)
                End If
            End If

            '====== END Putaway  by OrderItem =====

            ' BGN Manage Tag
            For i As Integer = 0 To objItemCollection.Count - 1
                Me.SaveTag_No(objItemCollection(i).OrderItem_Index)
                Dim ObjClass As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
                ObjClass.getView_Tag_Header(String.Format(" and Tag_Process_Id = 1 and OrderItem_Index='{0}' ", Replace(OrderItem_Index, "'", "''")))
                Dim dtTag As DataTable = ObjClass.GetDataTable()
                If (dtTag.Rows.Count > 0) Then
                    Tag_Index = dtTag.Rows(0).Item("Tag_Index").ToString()
                    Tag_No = dtTag.Rows(0).Item("TAG_No").ToString()
                Else
                    ' Delete OrderItem
                    Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
                    objDB.Delete_OrderItem(objItemCollection(i).OrderItem_Index)
                End If
            Next
            ' END Manage Tag

            If Not Me._Order_Index = "" Then
                _Order_Index = Me._Order_Index
            End If

            Me.btnSave.Enabled = False
            Me.btnDelete.Enabled = False
            Me.btnCopy.Enabled = False
            Me.btnPalletSlip.Enabled = False

            '------ Update Comment Image
            'Dim odjOrderImage As New tb_Order_Image

            'For iRow As Integer = 0 To grdOrderImage.RowCount - 1
            '    With odjOrderImage
            '        .Order_Index = _Order_Index
            '        .Order_Image_Index = grdOrderImage.Rows(iRow).Cells("Col_Order_Image_Index").Value.ToString
            '        .Comment = grdOrderImage.Rows(iRow).Cells("Col_Comment").Value.ToString
            '        .Update()
            '    End With
            'Next


            'include from master site
            If Not Me._Order_Index = "" Then
                'W_MSG_Information_ByIndex(1)
                Me.getOrderItemDetail(Me._Order_Index)
                'W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
                _Order_Index = Me._Order_Index
                cboPrint.Enabled = True
                btnPrintReport.Enabled = True
                objStatus = enuOperation_Type.UPDATE
                Me.getOrderHeader(_Order_Index)
                Me.grdOrderItem.ReadOnly = True
            Else
                W_MSG_Information_ByIndex(2)
                Exit Sub
            End If

            If (Tag_Index = Nothing) Then
                W_MSG_Information("บันทึกข้อมูลไม่สำเร็จ, กรุณาลองใหม่")
            End If
        Catch ex As Exception
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
            W_MSG_Error(ex.Message)
        Finally
            objHeader.Dispose()
            objItem.Dispose()
            objItemCollection = Nothing
        End Try
    End Sub

End Class