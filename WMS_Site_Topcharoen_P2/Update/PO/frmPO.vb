Imports WMS_STD_INB_Receive
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports System.Windows.Forms
Imports WMS_STD_INB_PO_Datalayer
Imports WMS_STD_CONFIGURATION


Public Class frmPO

    Dim Due_Date As Date = Now


#Region " CLASS PROPERTY & VARIABLE DECLARATION "

    Public SaveType As Integer = 0
    Public statusFrm As LoadStatus
    Dim Counter As Integer = 0
    ' This variable is set to FALSE when this form is loaded in Edit mode
    ' to prevent CellValueChanged property to keep recalculate data when we assign
    ' data from DB to datagrid.
    Public gblnDataGridClick As Boolean = True

    ' TODO: Todd 3 Jan 2010 - Don't know what these public variables are for?
    ' Need to check
    'compare
    Public poNo As String = ""
    Public skuid As String = ""
    Public TypeProduct As String = ""
    Public NumPro As String = ""

    Private _odtDisCount As New DataTable
    Private _PurchaseOrder_Index As String = ""
    Private _Status As Integer = 0
    Private _USE_PRODUCT_SUPPLIER As String = ""
    Private _statusData As Boolean = True
    Private _USE_PRODUCT_CUSTOMER As Boolean = False
    Private _Customer_Index As String = ""

    Private _dtValidate_Item As DataTable
    Private _dtValidate_grdPOOther As DataTable
    Private _dtValidate_grdPORemain As DataTable
    Private _dtValidate_grdPOAlreadyReceipt As DataTable
    Enum LoadStatus
        Defalt
        Edit
        summit
    End Enum

    Property dtDiscount() As DataTable
        Get
            Return _odtDisCount
        End Get
        Set(ByVal value As DataTable)
            _odtDisCount = value
        End Set
    End Property

    Public Property PurchaseOrder_Index() As String
        Get
            Return _PurchaseOrder_Index
        End Get
        Set(ByVal value As String)
            _PurchaseOrder_Index = value
        End Set
    End Property

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

    Public Property USE_PRODUCT_SUPPLIER() As String
        Get
            Return _USE_PRODUCT_SUPPLIER
        End Get
        Set(ByVal value As String)
            _USE_PRODUCT_SUPPLIER = value
        End Set
    End Property

#End Region

#Region " OPERATION TYPE "
    Public objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        CANCEL
        NULL
    End Enum

#End Region

#Region " FORM LOAD "

    ''' <summary>
    ''' Form load 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Update By: TA 05 Jan 2010 - Get Default Customer
    ''' </remarks>


    Private Sub frmPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    

        Me.grdPORemain.AutoGenerateColumns = False
        Me.grdPOAlreadyReceipt.AutoGenerateColumns = False

        Try

            ' ========== Manage Language Functions Begin ==========
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 9)
            oFunction.SW_Language_Column(Me, Me.grdPOItem, 9)
            oFunction.SW_Language_Column(Me, Me.grdPOOther, 9)
            oFunction.SW_Language_Column(Me, Me.grdPORemain, 9)
            oFunction.SW_Language_Column(Me, Me.grdPOAlreadyReceipt, 9)



            ' ------ STEP 1: Get master data for combo boxes
            Me.getPaymentType()
            Me.getCurrency()
            Me.getDepartment()
            Me.SetDEFAULT_VAT()
            Me.getDocumentType(9) ' Process Status = 9 is Purchase Order
            Me.getReportName(9)
            Me.SetDEFAULT_CUSTOMER_INDEX()


            'ซ่อน Tab
            Me.tabHeader.TabPages.Remove(Me.tbpVisible)
            'Me.tabHeader.TabPages.Remove(Me.TabPage2)
            ' ------ STEP 2: Check which operation type it is loaded, Add or Edit.
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    PurchaseOrder_Index = objDBTempIndex.getSys_Value("PurchaseOrder_Index")
                    objDBTempIndex = Nothing

                    ' Default user to display
                    txtUser.Text = WV_UserName
                    ' Get config value to check if we filter SKU by supplier?
                    Me.getUSE_PRODUCT_SUPPLIER()


                Case enuOperation_Type.UPDATE
                    Me.getPurchaseOrderHeader(PurchaseOrder_Index)
                    Me.getPurchaseOrderDetail(PurchaseOrder_Index)
                    Me.getPurchaseOrderOther(PurchaseOrder_Index)
                    Call ManageButtonByDocStatus(Status)
                    Me.CalSubtotalAmount()
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    ''' <summary>
    ''' 2009-01-01 This sub is to enable/disable buttons during form load based on status of document.
    ''' It is called when this form is loaded.
    ''' </summary>
    ''' <param name="intDocStatus"></param>
    ''' <remarks>
    ''' Added by: Todd 1 Jan 2010
    ''' </remarks>
    Private Sub ManageButtonByDocStatus(ByVal intDocStatus As Integer)

        Select Case intDocStatus
            Case -1 ' Cancelled
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnDelete1.Enabled = False
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnClose_PO.Enabled = False
                Me.grdPOItem.ReadOnly = True
                Me.btnConfirm.Enabled = False
                Me.btnRecived.Enabled = False
                Me.BtnInsert.Enabled = False
                Me.btnCompare.Enabled = False
                Me.btnPO_PR_Import.Enabled = False

            Case 1 ' Pending. Waiting to be confirmed.
                Me.btnSave.Enabled = True
                Me.btnDelete.Enabled = True
                Me.btnDelete1.Enabled = True
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnClose_PO.Enabled = False
                Me.btnConfirm.Enabled = True
                Me.btnRecived.Enabled = False
                Me.btnCompare.Enabled = True
                Me.btnPO_PR_Import.Enabled = True


            Case 2 ' Confirmed, but some items are pending received.
                'Me.btnSave.Enabled = False
                'Me.btnDelete.Enabled = False
                'Me.btnDelete1.Enabled = False
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnDelete1.Enabled = False
                Me.BtnInsert.Enabled = False
                Me.cboPrint.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnClose_PO.Enabled = True
                Me.btnConfirm.Enabled = False
                Me.btnRecived.Enabled = True
                Me.btnCompare.Enabled = True
                Me.btnPO_PR_Import.Enabled = False

            Case 3 ' Completed (or Closed)
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.BtnInsert.Enabled = False
                Me.btnDelete1.Enabled = False
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnClose_PO.Enabled = False
                Me.grdPOItem.ReadOnly = True
                Me.btnConfirm.Enabled = False
                Me.btnRecived.Enabled = True
                Me.btnCompare.Enabled = False
                Me.btnPO_PR_Import.Enabled = False
            Case Else
                ' Other status are not normal, so we do not allow any activities.
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnDelete1.Enabled = False
                Me.BtnInsert.Enabled = False
                Me.cboPrint.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnClose_PO.Enabled = False
                Me.btnConfirm.Enabled = False
                Me.btnRecived.Enabled = False
                Me.btnCompare.Enabled = False
                Me.btnPO_PR_Import.Enabled = False
        End Select

    End Sub

#End Region

#Region " INITIALIZE CONTROL "

    ''' <summary>
    ''' Get config from Config_CustomSetting table.
    ''' "USE_PRODUCT_SUPPLIER" specifies if we are filtering products by supplier.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getUSE_PRODUCT_SUPPLIER()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_PRODUCT_SUPPLIER", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.USE_PRODUCT_SUPPLIER = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.USE_PRODUCT_SUPPLIER = 0
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Get document type from Process ID.
    ''' </summary>
    ''' <param name="Process_Id"></param>
    ''' <remarks></remarks>
    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New ms_DocumentType(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

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

    ''' <summary>
    ''' Get report print out from Process ID.
    ''' </summary>
    ''' <param name="Process_Id"></param>
    ''' <remarks></remarks>
    Private Sub getReportName(ByVal Process_Id As Integer)

        Dim objClassDB As New WMS_STD_Master.config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Get department master.
    ''' </summary>
    ''' <remarks></remarks>
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


    ''' <summary>
    ''' Get currency master.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCurrency()
        Dim objsvms_Currency As New svms_Currency(svms_Currency.enuOperation_Type.SEARCH)
        Dim objDTsvms_Currency As DataTable = New DataTable

        Try
            objsvms_Currency.SearchData_Click("", "")
            objDTsvms_Currency = objsvms_Currency.DataTable

            cboCurrency.DisplayMember = "Description"
            cboCurrency.ValueMember = "Currency_Index"
            cboCurrency.DataSource = objDTsvms_Currency

        Catch ex As Exception
            Throw ex
        Finally
            objsvms_Currency = Nothing
            objDTsvms_Currency = Nothing
        End Try

    End Sub

    Sub SetDEFAULT_VAT()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("DEFAULT_VAT", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                txtVAT_Percent.Text = objDT.Rows(0).Item("Config_Value").ToString
            Else
                txtVAT_Percent.Text = 0
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get payment type master.
    ''' </summary>
    ''' <remarks></remarks>
    Sub getPaymentType()
        'Dim objClassDB As New tb_Invoice_PaymentType(ms_Package.enuOperation_Type.SEARCH)
        Dim oml_PO_Invoice As New ml_PO_Invoice()
        Dim odtml_PO_Invoice As DataTable = New DataTable

        Try
            oml_PO_Invoice.GetAllAsDataTable()
            odtml_PO_Invoice = oml_PO_Invoice.DataTable
            cboConditionPay.BeginUpdate()

            With cboConditionPay
                .DisplayMember = "Description"
                .ValueMember = "PaymentType_Index"
                .DataSource = odtml_PO_Invoice
            End With
            cboConditionPay.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            oml_PO_Invoice = Nothing
            odtml_PO_Invoice = Nothing
        End Try
    End Sub

    Sub SET_DEFAULT_CUSTOMER_BYUSER()
        Try
            Dim tCustomer_Index As String
            Dim objconfig As New config_UserByCustomer(config_UserByCustomer.enuOperation_Type.SEARCH)
            objconfig.GetCustomerDefault(WV_User_Index)
            tCustomer_Index = objconfig.ScalarOutput
            If tCustomer_Index <> "" Then
                _Customer_Index = tCustomer_Index
                Me.getCustomer()
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Sub SetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Try

            Dim objSkuConfigDB As New config_CustomSetting
            _USE_PRODUCT_CUSTOMER = objSkuConfigDB.getConfig_Key_USE("USE_PRODUCT_CUSTOMER")

            Dim tCustomer_Index As String '= objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_CUSTOMER_INDEX")
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            tCustomer_Index = oUser.GetUserByCustomerDefault()

            If tCustomer_Index <> "" Then
                _Customer_Index = tCustomer_Index
                Me.getCustomer()
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

#Region " BUTTON EVENTS "

    ''' <summary>
    ''' Button for Supplier Popup. Use with private sub "getSupplier".
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSeachSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachSupplier.Click
        Try
            Dim frm As New frmSupplier_Popup
            frm.ShowDialog()
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
            ' *********************
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button for Carrier Popup. Use with private sub "getCarrier".
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCarrier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCarrier.Click
        Try

            Dim frm As New frmCarrier_PopUp
            frm.ShowDialog()
            Dim tmpCarrier_Index As String = ""
            tmpCarrier_Index = frm.Carrier_Index

            If tmpCarrier_Index = "" Then
                Exit Sub
            End If

            If Not tmpCarrier_Index = "" Then
                Me.txtCarrier_ID.Tag = tmpCarrier_Index
                Me.getCarrier()
            Else
                Me.txtCarrier_ID.Tag = ""
                Me.txtCarrier_ID.Text = ""
                Me.txtCarrier_Name.Text = ""
            End If
            ' *********************
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button for Customer Popup. Use with private sub "getCustomer".
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try

            If _USE_PRODUCT_CUSTOMER Then
                For i As Integer = 0 To grdPOItem.Rows.Count - 2
                    If grdPOItem.Rows(i).Cells("Col_SKU_ID").Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(grdPOItem.Rows(i).Cells("Col_SKU_ID").Tag.ToString) Then
                        W_MSG_Information(String.Format("กรุณาลบรายการก่อนเปลี่ยน{0} !!", Me.lblSupplier.Text.Trim))
                        Exit Sub
                    End If
                Next
            End If


            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            ' --- Receive value มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Index = "" Then
                _Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                _Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button for Customer Received Location Popup.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCustomer_Receive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Receive.Click
        ' ====== TODO: HARDCODE-MSG 
        Try
            If Me.txtCustomer_Id.Text = "" Then
                W_MSG_Information("กรุณาเลือก" & Me.lblCustomer.Text & "ก่อน")
                Exit Sub
            End If

            Dim frm As New frmCustomer_Receive_Location_PopUp
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()

            Dim tmpCustomer_Receive_Location_Index As String = ""
            tmpCustomer_Receive_Location_Index = frm.Customer_Receive_Location_Index

            If tmpCustomer_Receive_Location_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Receive_Location_Index = "" Then
                Me.txtShipping_Location_ID.Tag = tmpCustomer_Receive_Location_Index
                Me.getShipping_Location_Index()
            Else
                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
                Me.txtShip_Address.Text = ""
            End If
            If Me.txtShipping_Location_ID.Text = "" Or Me.txtShip_Address.Text = "" Then
                Me.txtShipping_Location_ID.Tag = ""
            End If

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button to save data in this screen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Dim strResult As String = ""
       
        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If

        ' TODO: HARDCODE-MSG
        '   --- STEP 1: Data Validation for required fields
        If Not ValidateDataBeforeSave() Then
            Exit Sub
        Else
            '   --- STEP 2: Do the actual data saving.
            strResult = DoSaveDocument()

            '   --- STEP 3: Manage result messages and buttons control.
            If strResult <> "" Then
                ' Manage buttons. Disable only if the saving is succeeded.

                ' Note that is the case of adding new document only.
                Me.getPurchaseOrderNo(strResult)

                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.btnDelete1.Enabled = False
                W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
            Else
                W_MSG_Information("ไม่สามารถบันทึกข้อมูลได้")
                Exit Sub
            End If


            ' Manage buttons again
            btnCompare.Enabled = False
            btnPrint.Enabled = True
            cboPrint.Enabled = True

            ' 2017-01-02
            Me.objStatus = enuOperation_Type.UPDATE
            Me.RefreshUpdate()
        End If

    End Sub

    ''' <summary>
    ''' Button to close PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd 3 Jan 2010
    ''' We allow PO to be closed only when there is no remaining balance to receive.
    ''' Therefore, to close PO, user is forced to adjust the quantities to match with received quantities. 
    '''</remarks>

    Private Sub btnClose_PO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose_PO.Click

        'KSL
        Try
            If grdPOItem.Rows.Count <= 0 Then Exit Sub

            Dim xSql As String = ""
            Dim xDB As New DBType_SQLServer
            Dim xdt As New DataTable

            xSql = String.Format("select * from tb_PurchaseOrder where PurchaseOrder_Index = '{0}'", Me._PurchaseOrder_Index)
            xdt = xDB.DBExeQuery(xSql)
            If xdt.Rows.Count > 0 Then
                '-3	รอส่งข้อมูล,-1	ยกเลิก,0 ไม่ระบุ(),1 รอยืนยัน(),2 ค้างรับ(),3 เสร็จสิ้น()
                Select Case xdt.Rows(0).Item("Status").ToString
                    Case "0", "1", "-1"
                        W_MSG_Information("กรุณาตรวจสอบสถานะเอกสารอีกครั้ง หรือใช้การยกเลิกใบสั่งซื้อแทน")
                    Case "3"
                        W_MSG_Information("เอกสารเสร็จสิ้นแล้ว")
                    Case "2"
                        If W_MSG_Confirm("คุณต้องการปิดรายการใบสั่งซื้อใช่หรือไม่") = Windows.Forms.DialogResult.No = True Then
                            Exit Sub
                        End If
                        xSql = "update tb_PurchaseOrder set status =3 "
                        xSql &= String.Format(", Close_by = '{0}', Close_date = getdate()", WV_UserName)
                        xSql &= String.Format("where PurchaseOrder_Index = '{0}'", Me._PurchaseOrder_Index)
                        xDB.DBExeNonQuery(xSql)
                        W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
                        Me.Close()
                End Select
            Else
                Exit Sub
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

        '' TODO: HARDCODE-MSG
        'Dim i As Integer
        'Dim dblQtyReceived As Double = 0
        'Dim dblQtyPOItemBal As Double = 0
        'Dim strPurchaseOrderItemIndex As String = ""
        'Dim blnClosePOAllow As Boolean = True

        'Dim intBackupStatus As Integer = 0
        'Dim strResult As String = ""

        'Try

        '    Dim oSession As New WMS_STD_Master.UserSession
        '    Dim oBolCheck As Boolean = oSession.CheckSession
        '    If oBolCheck = False Then
        '        Exit Sub
        '    End If


        '    ' We backup old status in case closing PO is not successful.
        '    ' In case of error, we will rollback status to previous status in Finally section.
        '    intBackupStatus = _Status

        '    ' ------- STEP 1: Check conditions before closing PO!
        '    ' ------- Condition 1) Can't close PO if none of the items in this PO has been received.
        '    ' ------- Condition 2) Can't close PO if there is any pending received PO items.
        '    ' -------              User must adjust the quantities to match with received quantities.
        '    ' ------- Condition 3) We can only close PO with status = 2 (ค้างรับ). 
        '    ' -------              Note that this case should be taken care of by the controlling
        '    ' -------              of CLOSE button during form load.

        '    If grdPOItem.Rows.Count <= 0 Then Exit Sub

        '    Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
        '    If Not objPOTransaction.IsExist_POItemAlreadyReceipt(_PurchaseOrder_Index) Then
        '        W_MSG_Information("ไม่สามารถปิดใบสั่งซื้อได้ เนื่องจากยังไม่ได้รับสินค้าที่อ้างอิงใบสั่งซื้อใบนี้ กรุณายกเลิกใบสั่งซื้อแทน")
        '        objPOTransaction = Nothing
        '        Exit Sub
        '    End If
        '    objPOTransaction = Nothing

        '    For i = 0 To grdPOItem.Rows.Count - 1
        '        strPurchaseOrderItemIndex = grdPOItem.Rows(i).Cells("Col_POItem_Index").Value
        '        'dblQtyReceived = getPOItem_ReceivedQty(strPurchaseOrderItemIndex)
        '        dblQtyReceived = grdPOItem.Rows(i).Cells("Col_Qty_ReceiptShow").Value
        '        dblQtyPOItemBal = grdPOItem.Rows(i).Cells("Col_Qty").Value


        '        If dblQtyPOItemBal <> dblQtyReceived Then
        '            blnClosePOAllow = False
        '            Exit For
        '        End If
        '    Next

        '    If Not blnClosePOAllow Then
        '        W_MSG_Information("ไม่สามารถปิดใบสั่งซื้อได้ เนื่องจากจำนวนที่สั่งซื้อไม่เท่ากับจำนวนที่รับไปแล้ว")
        '        Exit Sub
        '    End If

        '    ' ------- STEP 2: It seems all conditions are fine. 
        '    ' ------- Now we confirm if user really wants to close this PO.
        '    If W_MSG_Confirm("คุณต้องการปิดรายการใบสั่งซื้อใช่หรือไม่") = Windows.Forms.DialogResult.No = True Then
        '        Exit Sub
        '    End If

        '    ' ------- STEP 3: Now save information
        '    ' ------- and Close PO by update PO Status to "Completed"

        '    If Not ValidateDataBeforeSave() Then
        '        Exit Sub
        '    Else
        '        _Status = 3 ' Set status of PO to "Completed"
        '        strResult = DoSaveDocument()

        '        If strResult <> "" Then
        '            W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
        '        Else
        '            W_MSG_Information("ไม่สามารถบันทึกข้อมูลได้")
        '            Exit Sub
        '        End If

        '        ' Note that is the case of adding new document only.
        '        Me.getPurchaseOrderNo(strResult)

        '        Me.btnSave.Enabled = False
        '        Me.btnClose_PO.Enabled = False
        '        Me.btnDelete.Enabled = False
        '        Me.btnDelete1.Enabled = False
        '        Me.btnCompare.Enabled = False
        '        Me.btnPrint.Enabled = True
        '        Me.cboPrint.Enabled = True
        '    End If

        'Catch ex As Exception
        '    Throw ex
        'Finally
        '    If strResult = "" Then
        '        _Status = intBackupStatus
        '    End If

        'End Try

    End Sub

    ''' <summary>
    ''' Button to close this form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Button to print out document in crystal report format.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click_2(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim oconfig_Report As New WMS_STD_Master.config_Report
        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
        Try
            '' To DO : รอ form
            'Dim frm As New Object 'frmReport_PO()
            'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And PurchaseOrder_Index ='" & Me.PurchaseOrder_Index & "'")
            'frm.ShowDialog()
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, "And PurchaseOrder_Index ='" & Me.PurchaseOrder_Index & "'")
            frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            frm.ShowDialog()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
        End Try
    End Sub

    ''' <summary>
    ''' This function is to delete PO items.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ' ====== TODO: HARDCODE-MSG 
        Dim objDB As New POTransaction(POTransaction.enuOperation_Type.DELETE)
        Dim strPurchaseOrderItem_Index As String = ""

        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If grdPOItem.Rows.Count <= 0 Then Exit Sub

            If grdPOItem.CurrentRow.Cells("Col_POItem_Index").Value = "" Then
                ' ------ This should be the case of adding new PO.
                Me.grdPOItem.Rows.RemoveAt(grdPOItem.CurrentRow.Index)
                Exit Sub
            Else
                strPurchaseOrderItem_Index = grdPOItem.CurrentRow.Cells("Col_POItem_Index").Value
            End If

            If Status = 2 Then
                ' ------ Case PO Status = Partial Received.
                ' This case we need to check if this record had been received.
                ' If it was received, we do not allow user to delete this PO item record.

                If Me.getPOItem_ReceivedQty(strPurchaseOrderItem_Index) = 0 Then
                    ' ------ OK Here means we do not yet receive this item, so deletion is find.
                    If W_MSG_Confirm("คุณต้องการลบรายการใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then
                        Dim _PurchaseOrder_PR_Index As String = grdPOItem.CurrentRow.Cells("col_PurchaseOrder_PR_Index").Value
                        If (_PurchaseOrder_PR_Index = Nothing) Then
                            objDB.Delete_PurchaseOrderItem(strPurchaseOrderItem_Index)
                            Me.grdPOItem.Rows.RemoveAt(grdPOItem.CurrentRow.Index)
                        Else
                            Dim _result As Boolean = New clsPR().removePurchaseOrderItem(_PurchaseOrder_PR_Index, strPurchaseOrderItem_Index)
                            Me.getPurchaseOrderDetail(PurchaseOrder_Index)
                        End If
                    End If

                Else
                    ' ------ 
                    W_MSG_Information("ไม่สามารถลบรายการได้ เนื่องจากสินค้าที่ถูกรับเข้าแล้วจากรายการในใบสั่งซื้อนี้")
                End If

            ElseIf Status = 1 Then
                ' ------ Case PO Status = Waiting to confirm (รอยืนยัน). No problem, can delete.
                If W_MSG_Confirm("คุณต้องการลบรายการใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then
                    Dim _PurchaseOrder_PR_Index As String = grdPOItem.CurrentRow.Cells("col_PurchaseOrder_PR_Index").Value
                    If (_PurchaseOrder_PR_Index = Nothing) Then
                        objDB.Delete_PurchaseOrderItem(strPurchaseOrderItem_Index)
                        Me.grdPOItem.Rows.RemoveAt(grdPOItem.CurrentRow.Index)
                    Else
                        Dim _result As Boolean = New clsPR().removePurchaseOrderItem(_PurchaseOrder_PR_Index, strPurchaseOrderItem_Index)
                        Me.getPurchaseOrderDetail(PurchaseOrder_Index)
                    End If
                End If
            Else
                ' ------ Other than PO status = 1 or 2, 
                ' ------ we do not allow deletion of PO Item through UI.
                ' ------ In fact button control should already taking care of this, 
                ' ------ but we check here anyway just in case!
                Exit Sub
            End If

            Me.CalSubtotalAmount()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDB = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' This function is to delete PO Other item.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete1.Click
        Try
            If grdPOOther.Rows.Count <= 0 Then Exit Sub
            If grdPOOther.CurrentRow.Cells("Col_Index_Other").Value = "" Then
                ' ------ This should be the case of adding new PO.
                Me.grdPOOther.Rows.RemoveAt(grdPOItem.CurrentRow.Index)
                Exit Sub
            End If

            If (Status = 1) Or (Status = 2) Then
                ' ------ We allow deletion of PO status = 1 or 2 only.
                ' ------ In fact button control should already taking care of it, 
                ' ------ but check here, just in case!
                If Not Me.grdPOOther.CurrentRow.Cells("Col_Index_Other").Value = "" Then
                    Dim objDB As New POTransaction(POTransaction.enuOperation_Type.DELETE)

                    If W_MSG_Confirm("คุณต้องการลบรายการใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then
                        Dim strPurchaseOrderOther As String = grdPOOther.CurrentRow.Cells("Col_Index_Other").Value
                        objDB.Delete_PurchaseOrderOther(strPurchaseOrderOther)
                        Me.grdPOOther.Rows.RemoveAt(grdPOOther.CurrentRow.Index)
                        objDB = Nothing
                    End If
                End If
                Me.CalSubtotalAmount()

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region " TEXTBOX & CHECKBOX EVENTS "
    '-------------------------------------------------------------------
    '------ Most of the events are for "Net Amount Calculation". -------
    '-------------------------------------------------------------------
    Private Sub txtDiscount_Percent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If DataNumeric(txtDiscount_Percent.Text) = False Then
                txtDiscount_Percent.Text = "0.00"
            Else
                txtDiscount_Percent.Text = CDbl(txtDiscount_Percent.Text)
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_Percent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If DataNumeric(txtVAT_Percent.Text) = False Then
                txtVAT_Percent.Text = "0.00"
            Else
                txtVAT_Percent.Text = CDbl(txtVAT_Percent.Text).ToString("#,##0.00")
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDeposit_Amt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If DataNumeric(txtDeposit_Amt.Text) = False Then
                txtDeposit_Amt.Text = "0.00"
            Else
                txtDeposit_Amt.Text = CDbl(txtDeposit_Amt.Text).ToString("#,##0.00")
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Amt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ' TODO: HARDCODE-MSG
        Try
            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If DataNumeric(txtDiscount_Amt.Text) = False Then
                txtDiscount_Amt.Text = "0.00"
            Else
                txtDiscount_Amt.Text = CDbl(txtDiscount_Amt.Text).ToString("#,##0.00")
            End If

            CalNetAmount()

            Dim iDiscount_Amt As Integer = txtDiscount_Amt.Text
            Dim iSubtotal As Integer = txtSubtotal.Text

            If iDiscount_Amt > iSubtotal Then
                W_MSG_Information("กรุณาตรวจสอบจำนวนส่วนลด เนื่องจาก มีจำนวนมากกว่าจำนวนเงินรวม")
                Exit Sub
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkVAT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If chkVAT.Checked = True Then
                txtVAT_Percent.Enabled = True
                SetDEFAULT_VAT()
                CalNetAmount()

            Else
                txtVAT_Percent.Enabled = False
                txtVAT.Text = "0.00"
                txtVAT_Percent.Text = "0.00"
                CalNetAmount()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtDiscount_Percent.Enabled = chkDiscount.Checked
    End Sub

    Private Sub txtSubtotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtSubtotal, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Percent_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtDiscount_Percent, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Amt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtDiscount_Amt, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_Percent_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVAT_Percent, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVAT, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDeposit_Amt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtDeposit_Amt, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtNet_Amt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtNet_Amt, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    '------ Events for Exchange Rate calculation
    Private Sub cboCurrency_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCurrency.SelectedIndexChanged
        Dim objClassDB As New svms_Currency(svms_Currency.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllCurrency(cboCurrency.SelectedValue)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                txtExRate.Text = CDbl(objDT.Rows(0).Item("ExRate").ToString).ToString("#,##0.00")
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub txtExRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExRate.TextChanged
        txtExRate.Text = Format(CDbl(txtExRate.Text.ToString), "#,##0.00")
    End Sub

#End Region

#Region " DATAGRID & TAB EVENTS "

    ''' <summary>
    ''' Control the switch of tab page.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbcPO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcPO.Click
        Dim tbcSelectedIndex As String = tbcPO.SelectedIndex
        If tbcSelectedIndex = 2 Then
            ShowRemainPOItemQty(PurchaseOrder_Index)
        Else : tbcSelectedIndex = 3
            ShowPOItemAlreadyReceipt(PurchaseOrder_Index)
        End If
    End Sub

    ''' <summary>
    ''' Event handler for grdPOItem cell click.
    ''' This event is used for handle SKU popup when click the button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd - 1 Jan 2010 - Clean code / change column name
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : try Catch
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub grdPOItem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOItem.CellClick
        Try
            If e.ColumnIndex <= -1 Or e.RowIndex <= -1 Then
                Exit Sub
            End If

            If grdPOItem.RowCount < 0 Then Exit Sub

            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "Col_Btn_GetSKU"
                    If (Not grdPOItem.Rows(e.RowIndex).Cells("col_PurchaseOrder_PR_Index").Value = Nothing) Then
                        W_MSG_Information("ไม่สามารถแก้ไขได้, ดึงจาก PR")
                        Exit Sub
                    End If

                    If Me._USE_PRODUCT_CUSTOMER AndAlso Me._Customer_Index = "" Then
                        W_MSG_Information(String.Format("กรุณาระบุ{0} !!", Me.lblCustomer.Text.Trim))
                        Exit Sub
                    End If

                    If grdPOItem.Rows(e.RowIndex).Cells("Col_Btn_GetSKU").Tag <> "NULL" Then
                        Dim _Customer_Index As String = Me._Customer_Index
                        Dim frmPopup As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, _Customer_Index)

                        If _USE_PRODUCT_SUPPLIER.ToString = "1" Then
                            frmPopup.Supplier_Index = Me.txtSupplier_Id.Tag
                        End If

                        frmPopup.ShowDialog()
                        Me.grdPOItem.Rows(e.RowIndex).Cells("Col_SKU_ID").Value = frmPopup.Sku_ID
                        frmPopup.Close()
                    End If
            End Select



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Event handler for grdPOItem value change.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd - 1 Jan 2010 - Clean code and modify logic
    ''' </remarks>
    Private Sub grdPOItem_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOItem.CellValueChanged
        If grdPOItem.RowCount < 0 Then Exit Sub
        Dim strCurrentSKUID As String = ""
        Dim dblSum As Double = 0
        Dim dblQty As Double = 0
        Dim dblUnitPrice As Double = 0
        Dim dblDiscount As Double = 0
        If e.ColumnIndex <= -1 Or e.RowIndex <= -1 Then
            Exit Sub
        End If

        ' ------ First we check if this variable gblnDataGridClick is set.
        ' ------ Normally it is set to FALSE during form load to avoid unnecessary calculation.
        If Not gblnDataGridClick Then
            Exit Sub
        End If

        Try
            With grdPOItem
                Select Case .Columns(e.ColumnIndex).Name

                    ' ------ Case SKU ID value change, get SKU information to display in current row.
                    Case "Col_SKU_ID"

                        If Me._USE_PRODUCT_CUSTOMER AndAlso String.IsNullOrEmpty(Me._Customer_Index) Then
                            W_MSG_Information(String.Format("กรุณาระบุ {0} !!", Me.lblCustomer.Text.Trim))
                            .Rows(e.RowIndex).Cells("Col_SKU_ID").Value = ""
                            Exit Select
                        End If

                        strCurrentSKUID = .Rows(e.RowIndex).Cells("Col_SKU_ID").Value.ToString
                        getSKU_Detail(strCurrentSKUID, e.RowIndex)

                        ' ------ Case Qty, Unit Price or Discount change, then recalculate Amount
                    Case "Col_Qty", "Col_Unit_Price", "Col_Discount"
                        If IsNumeric(.Rows(e.RowIndex).Cells("Col_Qty").Value) Then
                            dblQty = .Rows(e.RowIndex).Cells("Col_Qty").Value
                        Else
                            Exit Select
                        End If
                        If IsNumeric(.Rows(e.RowIndex).Cells("Col_Unit_Price").Value) Then
                            dblUnitPrice = .Rows(e.RowIndex).Cells("Col_Unit_Price").Value
                        Else
                            Exit Select
                        End If
                        If IsNumeric(.Rows(e.RowIndex).Cells("Col_Discount").Value) Then
                            dblDiscount = .Rows(e.RowIndex).Cells("Col_Discount").Value
                        Else
                            dblDiscount = 0
                        End If
                        dblSum = (dblQty * dblUnitPrice) - dblDiscount
                        .Rows(e.RowIndex).Cells("Col_Amount").Value = Format(dblSum, "#,##0.00")

                        ' ------ Case Amount change, recalculate Subtotal.
                    Case "Col_Amount"
                        CalSubtotalAmount()

                End Select
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Event handler for grdPurchaseOrderOther value change.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd - 1 Jan 2010 - Clean code and modify logic
    ''' -------------------------------------------------
    ''' Update Date : 21/01/2010
    ''' Update By   : Dong_kk
    ''' Update For  : try Catch
    ''' -------------------------------------------------
    ''' </remarks>
    Private Sub grdPurchaseOrderOther_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOOther.CellValueChanged

        Dim dblSum As Double = 0
        Dim dblQty As Double = 0
        Dim dblUnitPrice As Double = 0
        Try
            If gblnDataGridClick Then

                With grdPOOther
                    Select Case .Columns(e.ColumnIndex).Name

                        ' ------ If Qty or Unit Price change, then recalculate amount again.
                        Case "Col_Qty_Other", "Col_Unit_Price_Other"
                            If .Rows.Count - 1 Then

                                '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                                If DataNumeric(.Rows(e.RowIndex).Cells("Col_Qty_Other").Value) = False Then
                                    .Rows(e.RowIndex).Cells("Col_Qty_Other").Value = "0.00"
                                End If

                                '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                                If DataNumeric(.Rows(e.RowIndex).Cells("Col_Unit_Price_Other").Value) = False Then
                                    .Rows(e.RowIndex).Cells("Col_Unit_Price_Other").Value = "0.00"
                                End If


                                If IsNumeric(.Rows(e.RowIndex).Cells("Col_Qty_Other").Value) Then
                                    dblQty = .Rows(e.RowIndex).Cells("Col_Qty_Other").Value
                                Else
                                    Exit Select
                                End If

                                If IsNumeric(.Rows(e.RowIndex).Cells("Col_Unit_Price_Other").Value) Then
                                    dblUnitPrice = .Rows(e.RowIndex).Cells("Col_Unit_Price_Other").Value
                                Else
                                    Exit Select
                                End If

                                dblSum = dblQty * dblUnitPrice

                                .Rows(e.RowIndex).Cells("Col_Qty_Other").Value = CDbl(.Rows(e.RowIndex).Cells("Col_Qty_Other").Value) '.ToString("#,##0.00")
                                .Rows(e.RowIndex).Cells("Col_Unit_Price_Other").Value = CDbl(.Rows(e.RowIndex).Cells("Col_Unit_Price_Other").Value) '.ToString("#,##0.00")
                                .Rows(e.RowIndex).Cells("Col_Amount_Other").Value = Format(dblSum, "#,##0.00")
                                'grdPOOther.Rows(e.RowIndex).Cells("Col_Seq_Other").Value = (e.RowIndex + 1).ToString
                            End If

                            ' ------ If Amount change, recalculate subtotal.
                        Case "Col_Amount_Other"
                            Me.CalSubtotalAmount()
                    End Select

                End With
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

#End Region

#Region " SELECT MASTER FUNCTIONS AND SUBS "
    ''' <summary>
    ''' SETUSE_CUSTOMSETTING 
    ''' </summary>
    ''' <remarks>เปิดปิด ปุ่ม Print Barcode 0=ปิด  1=เปิด</remarks>
    Sub SETUSE_CUSTOMSETTING()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            'Dim _USE_PO_BTN_PRINT_BARCODE As Integer = objCustomSetting.getConfig_Key_USE("USE_PO_BTN_PRINT_BARCODE")
            'If _USE_PO_BTN_PRINT_BARCODE = 0 Then
            '    btnPrintPOBar.Visible = False
            'ElseIf _USE_PO_BTN_PRINT_BARCODE = 1 Then
            '    btnPrintPOBar.Visible = True
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Get Supplier information returning from Popup
    ''' </summary>
    ''' <remarks></remarks> 
    Private Sub getSupplier()
        Dim objms_Supplier As New ms_Supplier(ms_Supplier.enuOperation_Type.SEARCH)
        Dim objDTms_Supplier As DataTable = New DataTable

        Try
            objms_Supplier.getPopup_Supplier("Supplier_Index", Me.txtSupplier_Id.Tag.ToString)
            objDTms_Supplier = objms_Supplier.DataTable
            If objDTms_Supplier.Rows.Count > 0 Then
                Me.txtSupplier_Id.Tag = objDTms_Supplier.Rows(0).Item("Supplier_Index").ToString
                Me.txtSupplier_Id.Text = objDTms_Supplier.Rows(0).Item("Supplier_Id").ToString
                Me.txtSupplier_Name.Text = objDTms_Supplier.Rows(0).Item("Supplier_Name").ToString
                Me.txtSupplier_Address.Text = objDTms_Supplier.Rows(0).Item("Address").ToString
                Me.txtSupplier_Phone.Text = objDTms_Supplier.Rows(0).Item("tel").ToString
                Me.txtSupplier_Fax.Text = objDTms_Supplier.Rows(0).Item("fax").ToString
                '   Me.txtTax_No.Text = objDTms_Supplier.Rows(0).Item("Str1").ToString
            Else
                Me.txtSupplier_Id.Tag = ""
                Me.txtSupplier_Id.Text = ""
                Me.txtSupplier_Name.Text = ""
                Me.txtSupplier_Address.Text = ""
                Me.txtSupplier_Phone.Text = ""
                Me.txtSupplier_Fax.Text = ""
                ' Me.txtTax_No.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Supplier = Nothing
            objDTms_Supplier = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get Carrier information returning from Popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCarrier()
        Dim objms_Carrier As New ms_Carrier(ms_Carrier.enuOperation_Type.SEARCH)
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

    ''' <summary>
    ''' Get Customer information returning from Popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            objms_Customer.getPopup_Customer("Customer_Index", _Customer_Index)
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                _Customer_Index = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString
            Else
                _Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get Shipping Location information returning from Popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getShipping_Location_Index()
        Dim objms_Shipping_Location As New ms_Customer_Receive_Location(ms_Customer_Receive_Location.enuOperation_Type.SEARCH)
        Dim objDTms_Shipping_Location As DataTable = New DataTable

        Try
            objms_Shipping_Location.getPopup_Customer("Customer_Receive_Location_Index", Me.txtShipping_Location_ID.Tag.ToString)
            objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
            If objDTms_Shipping_Location.Rows.Count > 0 Then
                Me.txtShipping_Location_ID.Tag = objDTms_Shipping_Location.Rows(0).Item("Customer_Receive_Location_Index").ToString
                Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Customer_Receive_Location_Id").ToString
                Me.txtShip_Address.Text = objDTms_Shipping_Location.Rows(0).Item("AddressShipping_Location").ToString 'objDTms_Shipping_Location.Rows(0).Item("Address").ToString
                Me.txtShip_Address1.Text = objDTms_Shipping_Location.Rows(0).Item("AddressShipping_Location").ToString 'objDTms_Shipping_Location.Rows(0).Item("Address").ToString

                Me.txtShip_Phone.Text = objDTms_Shipping_Location.Rows(0).Item("Tel").ToString
                Me.txtShip_Fax.Text = objDTms_Shipping_Location.Rows(0).Item("Fax").ToString
            Else
                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
                Me.txtShip_Address.Text = ""
                Me.txtShip_Address1.Text = ""

                Me.txtShip_Phone.Text = ""
                Me.txtShip_Fax.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Shipping_Location = Nothing
            objDTms_Shipping_Location = Nothing
        End Try
    End Sub

#End Region

#Region " CALCULATE NET AMOUNT & SUBTOTAL "

    ''' <summary>
    ''' This sub is to calculate the total amount of the entire PO.
    ''' The formula is Net Amount = Subtotal - Discount + VAT - Deposit
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Todd 1 Jan 2010 - Clean Code and Modift Logic
    ''' </remarks>
    Sub CalNetAmount()
        Try
            'Dim dblTotalAmount As Double = 0
            Dim dblDiscount As Double = 0
            Dim dblVat As Double = 0
            Dim dblDeposit As Double = 0
            Dim dblSubtotal As Double = 0
            Dim i As Integer = 0

            If Not IsNumeric(Me.txtSubtotal.Text) Then
                Me.txtSubtotal.Text = "0.00"
            End If

            dblSubtotal = Me.txtSubtotal.Text



            If chkDiscount.Checked = True Then
                If (Not txtDiscount_Percent.Text = "") And (Not txtDiscount_Percent.Text = "0") Then
                    dblDiscount = (Convert.ToDouble(txtDiscount_Percent.Text.ToString) * dblSubtotal) / 100
                    txtDiscount_Amt.Text = CDbl(dblDiscount).ToString("#,##0.00")
                End If
            Else
                If Not txtDiscount_Amt.Text = "" And Not txtDiscount_Amt.Text = "0" Then
                    dblDiscount = (Convert.ToDouble(txtDiscount_Amt.Text.ToString))
                End If
            End If

            'If Not txtDiscount_Percent.Text = "" And Not txtDiscount_Percent.Text = "0" Then
            '    dblDiscount = (Convert.ToDouble(txtDiscount_Percent.Text.ToString) * dblSubtotal) / 100
            'End If

            If (Not txtVAT_Percent.Text = "") And (Not txtVAT_Percent.Text = "0") Then
                dblVat = (CDbl(txtVAT_Percent.Text.ToString) * (dblSubtotal - dblDiscount)) / 100
            End If

            If (Not txtDeposit_Amt.Text = "") And (Not txtDeposit_Amt.Text = "0") Then
                dblDeposit = CDbl(txtDeposit_Amt.Text.ToString)
            End If

            txtVAT.Text = Format(dblVat, "#,##0.00")

            If DataNumeric(txtDiscount_Percent.Text) = False Then
                txtDiscount_Percent.Text = "0.00"
            Else
                txtDiscount_Percent.Text = Format(CDbl(txtDiscount_Percent.Text.ToString), "#,##0.00")
            End If

            If DataNumeric(txtDeposit_Amt.Text) = False Then
                txtDeposit_Amt.Text = "0.00"
            Else
                txtDeposit_Amt.Text = Format(CDbl(txtDeposit_Amt.Text.ToString), "#,##0.00")
            End If

            If DataNumeric(txtVAT_Percent.Text) = False Then
                txtVAT_Percent.Text = "0.00"
            Else
                txtVAT_Percent.Text = Format(CDbl(txtVAT_Percent.Text.ToString), "#,##0.00")
            End If

            txtNet_Amt.Text = Format(CDbl(dblSubtotal - dblDiscount + dblVat - dblDeposit), "#,##0.00")


        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' This sub is to calculate subtotal amount.
    ''' It takes sum values of both PO Item and PO Other datagrid and sum them together.
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Todd 1 Jan 2010 - Clean Code and Modify Logic
    ''' </remarks>
    Private Sub CalSubtotalAmount()

        Dim dblSum As Double = 0
        Dim dblTempQty As Double = 0
        Dim i As Integer

        For i = 0 To grdPOItem.Rows.Count - 1
            If IsNumeric(grdPOItem.Rows(i).Cells("Col_Amount").Value) Then
                dblTempQty = grdPOItem.Rows(i).Cells("Col_Amount").Value
                dblSum += CDbl(dblTempQty)
            End If
        Next

        For i = 0 To grdPOOther.Rows.Count - 1
            If IsNumeric(grdPOOther.Rows(i).Cells("Col_Amount_Other").Value) Then
                dblTempQty = grdPOOther.Rows(i).Cells("Col_Amount_Other").Value
                dblSum += CDbl(dblTempQty)
            End If
        Next

        txtSubtotal.Text = Format(dblSum, "#,##0.00")
        Me.CalNetAmount()

    End Sub

#End Region

#Region " GENERIC FUNCTIONS AND SUBS "

    ''' <summary>
    ''' Get PO Header to display in PO Edit mode.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Private Sub getPurchaseOrderHeader(ByVal PurchaseOrder_Index As String)

        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
        Dim objDTPOTransaction As DataTable = New DataTable

        Try
            objPOTransaction.getPurchaseOrderHeader(PurchaseOrder_Index)
            objDTPOTransaction = objPOTransaction.DataTable

            If objDTPOTransaction.Rows.Count > 0 Then
                txtPO_No.Text = objDTPOTransaction.Rows(0).Item("PurchaseOrder_No").ToString
                dtpPO_Date.Value = objDTPOTransaction.Rows(0).Item("PurchaseOrder_Date").ToShortDateString
                txtUser.Text = objDTPOTransaction.Rows(0).Item("add_by").ToString
                txtSupplier_Id.Text = objDTPOTransaction.Rows(0).Item("SupId").ToString
                txtSupplier_Id.Tag = objDTPOTransaction.Rows(0).Item("Supplier_Index").ToString
                txtSupplier_Name.Text = objDTPOTransaction.Rows(0).Item("SupName").ToString
                txtSupplier_Address.Text = objDTPOTransaction.Rows(0).Item("Supplier_Address").ToString
                txtSupplier_Phone.Text = objDTPOTransaction.Rows(0).Item("Supplier_Tel").ToString
                txtSupplier_Fax.Text = objDTPOTransaction.Rows(0).Item("Supplier_Fax").ToString
                txtRef1.Text = objDTPOTransaction.Rows(0).Item("Str1").ToString
                txtRef2.Text = objDTPOTransaction.Rows(0).Item("Str2").ToString
                txtTax_No.Text = objDTPOTransaction.Rows(0).Item("Str3").ToString
                txtCreditTerm.Text = objDTPOTransaction.Rows(0).Item("Credit_Term").ToString
                txtRemark.Text = objDTPOTransaction.Rows(0).Item("Remark").ToString
                Me.cboDocumentType.SelectedValue = objDTPOTransaction.Rows(0).Item("DocumentType_index").ToString

                If objDTPOTransaction.Rows(0).Item("Exchange_Rate").ToString = "" Then
                    txtExRate.Text = 0
                Else
                    txtExRate.Text = CDbl(objDTPOTransaction.Rows(0).Item("Exchange_Rate").ToString).ToString("#,##0.00")
                End If

                dtpDue_Date.Value = objDTPOTransaction.Rows(0).Item("Expected_Delivery_Date").ToString
                Due_Date = dtpDue_Date.Value
                cboCurrency.SelectedValue = objDTPOTransaction.Rows(0).Item("Currency_Index").ToString
                cboConditionPay.SelectedValue = objDTPOTransaction.Rows(0).Item("PaymentMethod_Index").ToString
                txtCarrier_ID.Tag = objDTPOTransaction.Rows(0).Item("Carrier_Index").ToString
                txtCarrier_ID.Text = objDTPOTransaction.Rows(0).Item("Carrier_Id").ToString
                txtCarrier_Name.Text = objDTPOTransaction.Rows(0).Item("DesCar").ToString
                _Customer_Index = objDTPOTransaction.Rows(0).Item("Customer_Index").ToString
                txtCustomer_Id.Text = objDTPOTransaction.Rows(0).Item("Customer_Id").ToString
                txtCustomer_Name.Text = objDTPOTransaction.Rows(0).Item("Customer_Name").ToString
                'cboDepartment.SelectedValue = objDTPOTransaction.Rows(0).Item("Department_Index").ToString
                txtDepartment_Id.Tag = objDTPOTransaction.Rows(0).Item("Department_Index").ToString
                getDepartment()
                txtShipping_Location_ID.Tag = objDTPOTransaction.Rows(0).Item("Customer_Receive_Location_Index").ToString
                txtShipping_Location_ID.Text = objDTPOTransaction.Rows(0).Item("Shipping_LocationId").ToString
                txtShip_Address1.Text = objDTPOTransaction.Rows(0).Item("Shipping_LocationAddress").ToString & objDTPOTransaction.Rows(0).Item("Customer_ReceiveDistrict").ToString & objDTPOTransaction.Rows(0).Item("Customer_ReceiveProvince").ToString
                'txtShip_Phone.Text = objDTPOTransaction.Rows(0).Item("Shipping_LocationTel").ToString
                'txtShip_Fax.Text = objDTPOTransaction.Rows(0).Item("Shipping_LocationFax").ToString
                txtShip_Address.Text = objDTPOTransaction.Rows(0).Item("Str9").ToString
                txtShip_Phone.Text = objDTPOTransaction.Rows(0).Item("Str4").ToString
                txtShip_Fax.Text = objDTPOTransaction.Rows(0).Item("Str5").ToString

                If objDTPOTransaction.Rows(0).Item("Amount").ToString = "" Then
                    txtSubtotal.Text = 0
                Else
                    txtSubtotal.Text = CDbl(objDTPOTransaction.Rows(0).Item("Amount").ToString).ToString("#,##0.00")
                End If

                If objDTPOTransaction.Rows(0).Item("Discount_Percent").ToString = "" Then
                    txtDiscount_Percent.Text = 0
                Else
                    txtDiscount_Percent.Text = CDbl(objDTPOTransaction.Rows(0).Item("Discount_Percent").ToString).ToString("#,##0.00")
                End If

                If objDTPOTransaction.Rows(0).Item("Discount_Amt").ToString = "" Then
                    txtDiscount_Amt.Text = 0
                Else
                    txtDiscount_Amt.Text = CDbl(objDTPOTransaction.Rows(0).Item("Discount_Amt").ToString).ToString("#,##0.00")
                End If
                If objDTPOTransaction.Rows(0).Item("VAT_Percent").ToString = "" Then
                    txtVAT_Percent.Text = 0
                Else
                    txtVAT_Percent.Text = CDbl(objDTPOTransaction.Rows(0).Item("VAT_Percent").ToString).ToString("#,##0.00")
                End If

                txtStatus.Text = objDTPOTransaction.Rows(0).Item("DesPro").ToString
                txtUser.Text = objDTPOTransaction.Rows(0).Item("add_by").ToString

                If objDTPOTransaction.Rows(0).Item("VAT_Percent").ToString = "" Or objDTPOTransaction.Rows(0).Item("VAT_Percent").ToString = "0" Then
                    chkVAT.Checked = False
                Else
                    chkVAT.Checked = True
                End If

                If objDTPOTransaction.Rows(0).Item("Discount_Percent").ToString = "" Or objDTPOTransaction.Rows(0).Item("Discount_Percent").ToString = "0" Then
                    chkDiscount.Checked = False
                Else
                    chkDiscount.Checked = True
                End If

                If objDTPOTransaction.Rows(0).Item("VAT").ToString = "" Then
                    txtVAT.Text = 0
                Else
                    txtVAT.Text = CDbl(objDTPOTransaction.Rows(0).Item("VAT").ToString).ToString("#,##0.00")
                End If
                If objDTPOTransaction.Rows(0).Item("Deposit_Amt").ToString = "" Then
                    txtDeposit_Amt.Text = 0
                Else
                    txtDeposit_Amt.Text = CDbl(objDTPOTransaction.Rows(0).Item("Deposit_Amt").ToString).ToString("#,##0.00")
                End If
                If objDTPOTransaction.Rows(0).Item("Net_Amt").ToString = "" Then
                    txtNet_Amt.Text = 0
                Else
                    txtNet_Amt.Text = CDbl(objDTPOTransaction.Rows(0).Item("Net_Amt").ToString).ToString("#,##0.00")
                End If
                If objDTPOTransaction.Rows(0).Item("Str3").ToString = "" Then
                    '  txtTax_No.Text = ""
                Else
                    ' txtTax_No.Text = objDTPOTransaction.Rows(0).Item("Str3").ToString
                End If
                Dim _Status As Integer = 0
                Integer.TryParse(objDTPOTransaction.Rows(0).Item("Status").ToString(), _Status)
                Me.Status = _Status
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objPOTransaction = Nothing
            objDTPOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get PO Items to display in PO Edit mode.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Private Sub getPurchaseOrderDetail(ByVal PurchaseOrder_Index As String)

        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
        Dim objDTPOTransaction As DataTable = New DataTable

        Try
            objPOTransaction.getPurchaseOrderDetail(PurchaseOrder_Index)
            objDTPOTransaction = objPOTransaction.DataTable

            Me.grdPOItem.Rows.Clear()
            gblnDataGridClick = False
            For i As Integer = 0 To objDTPOTransaction.Rows.Count - 1
                With Me.grdPOItem
                    Me.grdPOItem.Rows.Add()
                    .Rows(i).Cells("Col_No").Value = i + 1  'Add by : TOP 19/03/2013
                    .Rows(i).Cells("Col_POItem_Index").Value = objDTPOTransaction.Rows(i).Item("PurchaseOrderItem_Index").ToString
                    .Rows(i).Cells("Col_SKU_ID").Value = objDTPOTransaction.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("Col_SKU_ID").Tag = objDTPOTransaction.Rows(i).Item("Sku_Index").ToString
                    .Rows(i).Cells("Col_SKU_ID").ReadOnly = True
                    .Rows(i).Cells("Col_Product_Type").Value = objDTPOTransaction.Rows(i).Item("Producttype").ToString
                    .Rows(i).Cells("Col_Description").Value = objDTPOTransaction.Rows(i).Item("Sku_Des").ToString

                    If objDTPOTransaction.Rows(i).Item("Qty").ToString = "" Then
                        .Rows(i).Cells("Col_Qty").Value = 0
                    Else
                        .Rows(i).Cells("Col_Qty").Value = CDbl(objDTPOTransaction.Rows(i).Item("Qty").ToString)
                    End If

                    If objDTPOTransaction.Rows(i).Item("UnitPrice").ToString = "" Then
                        .Rows(i).Cells("Col_Unit_Price").Value = 0
                    Else
                        .Rows(i).Cells("Col_Unit_Price").Value = CDbl(objDTPOTransaction.Rows(i).Item("UnitPrice").ToString)
                    End If

                    If objDTPOTransaction.Rows(i).Item("Amount").ToString = "" Then
                        .Rows(i).Cells("Col_Amount").Value = 0
                    Else
                        .Rows(i).Cells("Col_Amount").Value = CDbl(objDTPOTransaction.Rows(i).Item("Amount").ToString)
                    End If

                    ' ------ Note that we do not use Weight & Volume at this point
                    ' ------ So we leave it as is for now.
                    If objDTPOTransaction.Rows(i).Item("Weight").ToString = "" Then
                        .Rows(i).Cells("Col_Weight").Value = 0
                    Else
                        .Rows(i).Cells("Col_Weight").Value = CDbl(objDTPOTransaction.Rows(i).Item("Flo3").ToString)
                    End If

                    If objDTPOTransaction.Rows(i).Item("Volume").ToString = "" Then
                        .Rows(i).Cells("Col_Volume").Value = 0
                    Else
                        .Rows(i).Cells("Col_Volume").Value = CDbl(objDTPOTransaction.Rows(i).Item("Flo3").ToString)
                    End If

                    If objDTPOTransaction.Rows(i).Item("Discount_Amt").ToString = "" Then
                        .Rows(i).Cells("Col_Discount").Value = 0
                    Else
                        .Rows(i).Cells("Col_Discount").Value = CDbl(objDTPOTransaction.Rows(i).Item("Discount_Amt"))
                    End If

                    If objDTPOTransaction.Rows(i).Item("Received_Qty").ToString = "" Then
                        .Rows(i).Cells("Col_Qty_ReceiptShow").Value = 0
                    Else
                        .Rows(i).Cells("Col_Qty_ReceiptShow").Value = CDbl(objDTPOTransaction.Rows(i).Item("Received_Qty").ToString)
                    End If

                    'If objDTPOTransaction.Rows(i).Item("Flo2").ToString = "" Then
                    '    .Rows(i).Cells("Col_Before_VAT").Value = 0
                    'Else
                    '    .Rows(i).Cells("Col_Before_VAT").Value = objDTPOTransaction.Rows(i).Item("Flo2").ToString
                    'End If

                    .Rows(i).Cells("Col_Remark").Value = objDTPOTransaction.Rows(i).Item("Remark").ToString
                    getSKU_Package(objDTPOTransaction.Rows(i).Item("Sku_Index").ToString, i)

                    .Rows(i).Cells("Col_UOM").Value = objDTPOTransaction.Rows(i).Item("Package_Index").ToString
                    .Rows(i).Cells("Col_UOM").Tag = objDTPOTransaction.Rows(i).Item("Package_Index").ToString
                    'แสดงตัวเปอร์เซนต์ที่เกิน
                    If objDTPOTransaction.Rows(i).Item("Percent_Over_Allow").ToString = "" Then
                        .Rows(i).Cells("Co_Percent_Over_Allow").Value = 0
                    Else
                        .Rows(i).Cells("Co_Percent_Over_Allow").Value = CDbl(objDTPOTransaction.Rows(i).Item("Percent_Over_Allow").ToString)
                    End If
                    'แสดงเปอร์เซ็นต์ที่ขาด
                    If objDTPOTransaction.Rows(i).Item("Percent_Under_Allow").ToString = "" Then
                        .Rows(i).Cells("Co_Percent_Under_Allow").Value = 0
                    Else
                        .Rows(i).Cells("Co_Percent_Under_Allow").Value = CDbl(objDTPOTransaction.Rows(i).Item("Percent_Under_Allow").ToString)
                    End If

                    .Rows(i).Cells("col_PurchaseOrder_PR_Index").Value = objDTPOTransaction.Rows(i).Item("PurchaseOrder_PR_Index").ToString()
                    If (Not .Rows(i).Cells("col_PurchaseOrder_PR_Index").Value = Nothing) Then
                        .Rows(i).Cells("Col_SKU_ID").ReadOnly = True
                        .Rows(i).Cells("Col_UOM").ReadOnly = True
                        .Rows(i).Cells("Col_Qty").ReadOnly = True
                    End If

                End With
            Next

            gblnDataGridClick = True
        Catch ex As Exception
            Throw ex
        Finally
            objPOTransaction = Nothing
            objDTPOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get PO Other to display in PO Edit mode.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Private Sub getPurchaseOrderOther(ByVal PurchaseOrder_Index As String)

        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
        Dim objDTPOTransaction As DataTable = New DataTable

        Try
            objPOTransaction.getPurchaseOrderOther(PurchaseOrder_Index)
            objDTPOTransaction = objPOTransaction.DataTable

            Me.grdPOOther.Rows.Clear()
            gblnDataGridClick = False

            For i As Integer = 0 To objDTPOTransaction.Rows.Count - 1
                With Me.grdPOOther

                    Me.grdPOOther.Rows.Add()
                    .Rows(i).Cells("Col_Seq_Other").Value = objDTPOTransaction.Rows(i).Item("Seq").ToString
                    .Rows(i).Cells("Col_Index_Other").Value = objDTPOTransaction.Rows(i).Item("PurchaseOrderOther_Index").ToString
                    .Rows(i).Cells("Col_Description_Other").Value = objDTPOTransaction.Rows(i).Item("Description").ToString
                    .Rows(i).Cells("Col_Unit_Price_Other").Value = CDbl(objDTPOTransaction.Rows(i).Item("Flo1").ToString)
                    .Rows(i).Cells("Col_Qty_Other").Value = CDbl(objDTPOTransaction.Rows(i).Item("Flo2").ToString)
                    .Rows(i).Cells("Col_Amount_Other").Value = CDbl(objDTPOTransaction.Rows(i).Item("Total_Amt").ToString)
                    .Rows(i).Cells("Col_Remark_Other").Value = objDTPOTransaction.Rows(i).Item("Str1").ToString
                End With
            Next

            gblnDataGridClick = True
        Catch ex As Exception
            Throw ex
        Finally
            objPOTransaction = Nothing
            objDTPOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Sub "getSKU_Package"
    ''' This sub gets SKU Index and row index from PO Item datagrid,
    ''' then populate packages into combobox.
    ''' This sub is called when changing SKU in datagrid "grdPOItem".
    ''' </summary>
    ''' <param name="Sku_Index"></param>
    ''' <param name="RowIndex"></param>
    ''' <remarks>
    ''' Added by: ?
    ''' Updated by: Todd - 29 Dec 2009
    ''' </remarks>
    ''' 
    Private Function getSKU_PackageCal(ByVal Sku_Index As String, ByVal RowIndex As Integer) As Boolean

        Try
            Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable

            objClassDB.getSKU_Package(Sku_Index)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                Dim dgvcob As New DataGridViewComboBoxCell
                dgvcob.DisplayMember = "Package"
                dgvcob.ValueMember = "Package_Index"
                dgvcob.DataSource = objDT

                grdPOItem.Rows(RowIndex).Cells("cbo_PackageCal") = dgvcob
                If objDT.Rows.Count = 1 Then
                    grdPOItem.Rows(RowIndex).Cells("cbo_PackageCal").Value = objDT.Rows(0).Item("Package_Index").ToString
                Else
                    grdPOItem.Rows(RowIndex).Cells("cbo_PackageCal").Value = objDT.Rows(1).Item("Package_Index").ToString
                End If


                Return 1
            Else
                Return -1
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            Return -1
        End Try

    End Function
    Private Sub getSKU_Package(ByVal Sku_Index As String, ByVal RowIndex As Integer)

        Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getSKU_Package(Sku_Index)
            objDT = objClassDB.DataTable
            Dim dgvcob As New DataGridViewComboBoxCell

            dgvcob.DisplayMember = "Package"
            dgvcob.ValueMember = "Package_Index"
            dgvcob.DataSource = objDT

            grdPOItem.Rows(RowIndex).Cells("Col_UOM") = dgvcob
            grdPOItem.Rows(RowIndex).Cells("Col_UOM").Value = objDT.Rows(0).Item("Package_Index").ToString
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' This function gets ratio for the pair of SKU and Package.
    ''' </summary>
    ''' <param name="Sku_Index"></param>
    ''' <param name="Package_Index"></param>
    ''' <returns>SKU Ratio</returns>
    ''' <remarks>
    ''' Added by: Todd - 28 Dec 2009
    ''' </remarks>
    Private Function getSKU_PackageRatio(ByVal Sku_Index As String, ByVal Package_Index As String) As Double

        Dim objClassDB As New ms_SKURatio(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim intRatio As Integer = 0

        Try
            objClassDB.SelectData_ByPackage(Sku_Index, Package_Index)
            objDT = objClassDB.DataTable

            If objDT.Rows.Count > 0 Then
                intRatio = objDT.Rows(0).Item("Ratio").ToString()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            getSKU_PackageRatio = intRatio
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Function

    ''' <summary>
    ''' This sub gets SKU details to display in grdPOItem.
    ''' </summary>
    ''' <param name="Sku_Id"></param>
    ''' <param name="RowIndex"></param>
    ''' <remarks>
    ''' Added by: ?
    ''' Updated by: Todd - 29 Dec 2009 - Fix bug in returning Package
    ''' </remarks>
    Private Sub getSKU_Detail(ByVal Sku_Id As String, ByVal RowIndex As Integer)
        Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strSku_Index As String = ""
        Try
            objClassDB.getSKU_Detail(Sku_Id)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then

                If Me._USE_PRODUCT_CUSTOMER = True Then
                    If objDT.Rows(0).Item("Customer_Index").ToString <> "" Then
                        If objDT.Rows(0).Item("Customer_Index").ToString <> Me._Customer_Index Then
                            W_MSG_Information(String.Format("สินค้ามี{0}ไม่ตรงกับที่ระบุ !!", Me.lblCustomer.Text.Trim))
                            grdPOItem.Rows(RowIndex).Cells("Col_Description").Value = ""
                            grdPOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = "0"
                            grdPOItem.Rows(RowIndex).Cells("Col_Product_Type").Value = "0"
                            grdPOItem.Rows(RowIndex).Cells("Col_UOM").Value = ""
                            grdPOItem.Rows(RowIndex).Cells("Col_UOM").Tag = ""
                            grdPOItem.Rows(RowIndex).Cells("Col_SKU_ID").Value = ""
                            Exit Sub

                        End If
                    End If
                    ' AndAlso objDT.Rows(0).Item("Customer_Index").ToString <> Me._Customer_Index Then

                End If

                grdPOItem.Rows(RowIndex).Cells("Col_SKU_ID").Tag = objDT.Rows(0).Item("Sku_Index").ToString
                grdPOItem.Rows(RowIndex).Cells("Col_Description").Value = objDT.Rows(0).Item("Str1").ToString
                grdPOItem.Rows(RowIndex).Cells("Col_UOM").Tag = objDT.Rows(0).Item("Package_Index").ToString
                grdPOItem.Rows(RowIndex).Cells("Col_UOM").Value = objDT.Rows(0).Item("Package").ToString
                grdPOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(objDT.Rows(0).Item("Price1"), GetType(Double))
                grdPOItem.Rows(RowIndex).Cells("Col_Product_Type").Value = objDT.Rows(0).Item("ProductType").ToString
                strSku_Index = grdPOItem.Rows(RowIndex).Cells("Col_SKU_ID").Tag
            Else
                grdPOItem.Rows(RowIndex).Cells("Col_Description").Value = ""
                grdPOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = "0"
                grdPOItem.Rows(RowIndex).Cells("Col_Product_Type").Value = "0"
                grdPOItem.Rows(RowIndex).Cells("Col_UOM").Value = ""
                grdPOItem.Rows(RowIndex).Cells("Col_UOM").Tag = ""
                Exit Sub
            End If

            If Not strSku_Index = "" Then
                ' Search SKU Package 
                getSKU_Package(strSku_Index, RowIndex)
                grdPOItem.Rows(RowIndex).Cells("Col_UOM").Tag = grdPOItem.Rows(RowIndex).Cells("Col_UOM").Value

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' This sub gets PO No. to display in textbox.
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks>
    ''' Added by: ?
    ''' Updated by: Todd 27 Dec 2009 - Rename sub name from "getPurchaseOrderIndex" to "getPurchaseOrderNo".
    ''' </remarks>
    Private Sub getPurchaseOrderNo(ByVal PurchaseOrder_Index As String)

        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
        Dim objDTPOTransaction As DataTable = New DataTable

        Try
            objPOTransaction.PurchaseOrderIndex(PurchaseOrder_Index)
            objDTPOTransaction = objPOTransaction.DataTable

            If objDTPOTransaction.Rows.Count > 0 Then
                Me.txtPO_No.Text = objDTPOTransaction.Rows(0).Item("PurchaseOrder_No").ToString
                Me.ManageButtonByDocStatus(objDTPOTransaction.Rows(0).Item("Status"))
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objPOTransaction = Nothing
            objDTPOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' This sub is to display PO items with Goods Receipt tab. (รายการที่รับเข้าแล้ว)
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks></remarks>
    Sub ShowPOItemAlreadyReceipt(ByVal PurchaseOrder_Index As String)

        Try
            Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
            Dim oDTPOTransaction As New DataTable

            objPOTransaction.getPurchaseOrder_AlreadyReceipt(PurchaseOrder_Index)
            oDTPOTransaction = objPOTransaction.DataTable

            Me.grdPOAlreadyReceipt.Rows.Clear()

            For i As Integer = 0 To oDTPOTransaction.Rows.Count - 1
                With Me.grdPOAlreadyReceipt
                    Me.grdPOAlreadyReceipt.Rows.Add()

                    .Rows(i).Cells("Col_SKU_ID_Receipt").Value = oDTPOTransaction.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("Col_Description_Receipt").Value = oDTPOTransaction.Rows(i).Item("Str1").ToString
                    .Rows(i).Cells("Col_Order_ID_Receipt").Value = oDTPOTransaction.Rows(i).Item("Order_No").ToString
                    .Rows(i).Cells("Col_Order_Date_Receipt").Value = oDTPOTransaction.Rows(i).Item("Order_Date").ToString
                    .Rows(i).Cells("Col_Qty_Receipt").Value = CDbl(oDTPOTransaction.Rows(i).Item("Qty").ToString)
                    .Rows(i).Cells("Col_Unit_Price_Receipt").Value = CDbl(oDTPOTransaction.Rows(i).Item("UnitPrice").ToString)
                    .Rows(i).Cells("Col_UOM_Receipt").Value = oDTPOTransaction.Rows(i).Item("Description").ToString
                    .Rows(i).Cells("Col_Total_Receipt").Value = CDbl(oDTPOTransaction.Rows(i).Item("Qty").ToString * oDTPOTransaction.Rows(i).Item("UnitPrice").ToString)

                End With
            Next

            objPOTransaction = Nothing
            oDTPOTransaction = Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' This sub is to display remaining PO items tab. (รายการที่ค้างรับ)
    ''' </summary>
    ''' <param name="PurchaseOrder_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd 3 Jan 2009 - Show only remaining items.
    ''' </remarks>
    Sub ShowRemainPOItemQty(ByVal PurchaseOrder_Index As String)
        Try

            Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
            Dim oDTPOTransaction As New DataTable

            objPOTransaction.getPurchaseOrderRemain(PurchaseOrder_Index)
            oDTPOTransaction = objPOTransaction.DataTable

            Me.grdPORemain.Rows.Clear()

            For i As Integer = 0 To oDTPOTransaction.Rows.Count - 1
                With Me.grdPORemain
                    Me.grdPORemain.Rows.Add()

                    .Rows(i).Cells("Col_POItem_Index_Pending").Value = oDTPOTransaction.Rows(i).Item("PurchaseOrderItem_Index").ToString
                    .Rows(i).Cells("Col_SKU_ID_Pending").Value = oDTPOTransaction.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("Col_SKU_ID_Pending").Tag = oDTPOTransaction.Rows(i).Item("Sku_Index").ToString
                    .Rows(i).Cells("Col_Description_Pending").Value = oDTPOTransaction.Rows(i).Item("Sku_Des").ToString
                    .Rows(i).Cells("Col_Product_Type_Pending").Value = oDTPOTransaction.Rows(i).Item("Producttype").ToString
                    .Rows(i).Cells("Col_Qty_Remain").Value = CDbl(oDTPOTransaction.Rows(i).Item("Qty").ToString - oDTPOTransaction.Rows(i).Item("Received_Qty").ToString)
                    .Rows(i).Cells("Col_Qty_Pending").Value = CDbl(oDTPOTransaction.Rows(i).Item("Qty").ToString)
                    .Rows(i).Cells("Col_UOM_Pending").Value = oDTPOTransaction.Rows(i).Item("PackDes").ToString
                    .Rows(i).Cells("Col_UOM_Pending").Tag = oDTPOTransaction.Rows(i).Item("Package_Index").ToString
                    .Rows(i).Cells("Col_Last_Received_Date").Value = oDTPOTransaction.Rows(i).Item("Last_Received_Date").ToString

                End With
            Next

            objPOTransaction = Nothing
            oDTPOTransaction = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Get PO Item Received Qty.
    ''' </summary>
    ''' <param name="PurchaseOrderItem_Index"></param>
    ''' <returns>"Received Qty" as Double</returns>
    ''' <remarks>
    ''' Updated by: Todd 27 Dec 2009 - Rename from "CheckQTYBal" to "getPOItem_ReceivedQty".
    ''' </remarks>
    Private Function getPOItem_ReceivedQty(ByVal PurchaseOrderItem_Index As String) As Double
        Dim QtyReceived As Double = 0

        Try
            Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
            Dim oDTPOTransaction As New DataTable

            objPOTransaction.getPurchaseOrderDetail_By_POItemIndex(PurchaseOrderItem_Index)
            oDTPOTransaction = objPOTransaction.DataTable
            'For i As Integer = 0 To oDTPOTransaction.Rows.Count - 1
            '    QtyReceived = oDTPOTransaction.Rows(i).Item("Received_Qty").ToString
            'Next
            If oDTPOTransaction.Rows.Count > 0 Then
                QtyReceived = oDTPOTransaction.Rows(0).Item("Received_Qty").ToString
            End If
            Return QtyReceived

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' This function performs data validation before saving.
    ''' This should includes required field validation.
    ''' </summary>
    ''' <returns>
    ''' True - If all validation is passed.
    ''' False - One of the validation failed.
    ''' </returns>
    ''' <remarks>
    ''' Added by: Todd - 28 Dec 2009 - To split code from button "Save".
    ''' </remarks>
    Private Function ValidateDataBeforeSave() As Boolean
        ' TODO: HARDCODE-MSG
        Dim i As Integer
        Dim strTempPOItemIndex As String
        Dim dblQty As Double = 0
        Dim dblReceivedQty As Double = 0
        Dim strTemp As String

        ' ------ STEP 1: Validate required fields

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                Dim objcon As New DBType_SQLServer
                Dim dt As New DataTable
                If objcon.DBExeQuery_Scalar(String.Format("select count(*) from tb_PurchaseOrder where PurchaseOrder_No = '{0}' and Status not in (-1) ", Me.txtPO_No.Text)) > 0 Then
                    W_MSG_Error("เลขที่เอกสารซ้ำ")
                    objcon = Nothing
                    Return False
                End If
        End Select

        Dim VaridateText As New W_SetValidate()
        Dim tmpMsg As String = ""
        tmpMsg = VaridateText.MessageTextValidate(Me, 9)
        If tmpMsg <> "S" Then
            W_MSG_Information(tmpMsg)
            Return False
        End If
        tmpMsg = ""

        tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdPOItem, 9)
        If tmpMsg <> "S" Then
            W_MSG_Information(tmpMsg)
            Return False
        End If
        tmpMsg = ""

        tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdPOAlreadyReceipt, 9)
        If tmpMsg <> "S" Then
            W_MSG_Information(tmpMsg)
            Return False
        End If
        tmpMsg = ""

        If String.IsNullOrEmpty(Me._Customer_Index) Then
            W_MSG_Information(String.Format("กรุณาระบุ{0} !!", Me.lblCustomer.Text.Trim))
            Return False
        End If

        'If txtNet_Amt.Text = 0 Then
        '    ValidateDataBeforeSave = False
        '    W_MSG_Information("ไม่สามารถบันทึกรายการได้ กรุณาตรวจสอบ ราคา/หน่วย และ จำนวน ของสินค้า")
        '    grdPOItem.Focus()
        '    Exit Function
        'End If

        ' ------ STEP 2: Item Quantity and Unit Price Comparison.
        For i = 0 To grdPOItem.Rows.Count - 2
            With grdPOItem

                If (.Rows(i).Cells("Col_Sku_Id").Tag = Nothing) Then
                    .Rows(i).Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบรหัสสินค้า, ลำดับ {0}", .Rows(i).Cells("col_no").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (.Rows(i).Cells("Col_UOM").Tag = Nothing) Then
                    .Rows(i).Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบหน่วยสินค้า, ลำดับ {0}", .Rows(i).Cells("col_no").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                ' Check if Qty is numeric value
                If Not IsNumeric(.Rows(i).Cells("Col_Qty").Value) Then
                    ValidateDataBeforeSave = False
                    .Rows(i).Selected = True
                    W_MSG_Information("ไม่สามารถทำรายการได้ จำนวนสั่งซื้อต้องเป็นตัวเลขเท่านั้น")
                    Exit Function
                End If

                ' Check if Qty is <= 0
                dblQty = .Rows(i).Cells("Col_Qty").Value
                If dblQty <= 0 Then
                    ValidateDataBeforeSave = False
                    .Rows(i).Selected = True
                    W_MSG_Information("ไม่สามารถทำรายการได้ จำนวนสั่งซื้อต้องมากกว่าศูนย์")
                    Exit Function
                Else
                    ' Check if Qty < Received Qty
                    strTempPOItemIndex = .Rows(i).Cells("Col_POItem_Index").Value
                    dblReceivedQty = getPOItem_ReceivedQty(strTempPOItemIndex)
                    If dblQty < dblReceivedQty Then
                        ValidateDataBeforeSave = False
                        .Rows(i).Selected = True
                        W_MSG_Information("ไม่สามารถทำรายการได้ เนื่องจากจำนวนที่สั่งซื้อน้อยกว่าจำนวนที่รับไปแล้ว")
                        Exit Function
                    End If
                End If

                ' Check if Unit Price is numeric value
                If Not IsNumeric(.Rows(i).Cells("Col_Unit_Price").Value) Then
                    ValidateDataBeforeSave = False
                    .Rows(i).Selected = True
                    W_MSG_Information("ไม่สามารถทำรายการได้ ราคาต่อหน่วยต้องเป็นตัวเลขเท่านั้น")
                    Exit Function
                End If

                ' Check if Discount is numeric value or ""
                If (Not IsNothing(.Rows(i).Cells("Col_Discount").Value)) Then
                    strTemp = .Rows(i).Cells("Col_Discount").Value.ToString.Trim
                    If (Not IsNumeric(strTemp)) And (strTemp <> "") Then
                        ValidateDataBeforeSave = False
                        .Rows(i).Selected = True
                        W_MSG_Information("ไม่สามารถทำรายการได้ ส่วนลดต้องเป็นตัวเลขเท่านั้น")
                        Exit Function
                    End If
                End If

                ' Check if Amount is numeric value
                If Not IsNumeric(.Rows(i).Cells("Col_Amount").Value) Then
                    ValidateDataBeforeSave = False
                    .Rows(i).Selected = True
                    W_MSG_Information("ไม่สามารถทำรายการได้ ราคารวมต้องเป็นตัวเลขเท่านั้น")
                    Exit Function
                End If

            End With
        Next

        ValidateDataBeforeSave = True

    End Function

    ''' <summary>
    ''' Actual code for saving data. This function prepares all required data from UI
    ''' and put them into Data Tables and Collections.
    ''' </summary>
    ''' <returns>
    ''' PurchaseOrder_Index if succeeded, 
    ''' "" if failed.
    ''' </returns>
    ''' <remarks>
    ''' Added by: Todd - 28 Dec 2009 - To split code from button "Save".
    ''' </remarks>
    Private Function DoSaveDocument() As String

        ' TODO: HARDCODE-MSG
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim strTempSKUIndex = ""
        Dim strTempPackageIndex = ""
        Dim dblSKURatio As Double = 1

        ' ------ STEP 1: Declare Data Table Objects
        Dim objtb_PurchaseOrder As New tb_PurchaseOrder
        Dim objtb_PurchaseOrderItem As New tb_PurchaseOrderItem
        Dim objtb_PurchaseOrderItemCollection As New List(Of tb_PurchaseOrderItem)
        Dim objtb_PurchaseOrder_Discount As New tb_PurchaseOrder_Discount
        Dim objtb_PurchaseOrder_DiscountItem As New List(Of tb_PurchaseOrder_Discount)
        Dim objtb_PurchaseOrderOtherCollection As New List(Of tb_PurchaseOrderOther)

        Try
            ' ------ STEP 2: Prepare values for PO Header


            objtb_PurchaseOrder.PurchaseOrder_Index = Me.PurchaseOrder_Index

            If Me.cboDocumentType.SelectedValue Is Nothing Then
                objtb_PurchaseOrder.DocumentType_Index = ""
            Else
                objtb_PurchaseOrder.DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
            End If

            ' ja 4 Jan 2010 - Update Run Document Number by DocumentType
            If txtPO_No.Text = "" Then
                Dim objDocumentNumber As New clsDocumentNumber()
                objtb_PurchaseOrder.PurchaseOrder_No = objDocumentNumber.Auto_DocumentType_Number(objtb_PurchaseOrder.DocumentType_Index, "", Me.dtpPO_Date.Value)
                txtPO_No.Text = objtb_PurchaseOrder.PurchaseOrder_No
                objDocumentNumber = Nothing

                'Dim objDocumentNumber2 As New Sy_AutoyyyyMM
                'objtb_PurchaseOrder.PurchaseOrder_No = objDocumentNumber2.Auto_DocumentType_Number(New SqlClient.SqlConnection(WV_ConnectionString), Nothing, objtb_PurchaseOrder.DocumentType_Index, Me.dtpPO_Date.Value, "")
                'txtPO_No.Text = objtb_PurchaseOrder.PurchaseOrder_No
                'objDocumentNumber2 = Nothing


            Else
                objtb_PurchaseOrder.PurchaseOrder_No = txtPO_No.Text
            End If

            'If txtPO_No.Text.Trim = "" Then
            '    If rdoPOTrue.Checked = True Then
            '        objtb_PurchaseOrder.PurchaseOrder_Prefix = "PO0"
            '    Else
            '        objtb_PurchaseOrder.PurchaseOrder_Prefix = "PN0"
            '    End If
            'End If

            objtb_PurchaseOrder.PurchaseOrder_Date = Me.dtpPO_Date.Value

            If Me.txtCarrier_ID.Tag = Nothing Then
                objtb_PurchaseOrder.Carrier_Index = ""
            Else
                objtb_PurchaseOrder.Carrier_Index = Me.txtCarrier_ID.Tag.ToString
            End If
            If Me.txtShipping_Location_ID.Tag = Nothing Then
                objtb_PurchaseOrder.Customer_Receive_Location_Index = ""
            Else
                objtb_PurchaseOrder.Customer_Receive_Location_Index = Me.txtShipping_Location_ID.Tag.ToString
            End If
            objtb_PurchaseOrder.Expected_Delivery_Date = Me.dtpDue_Date.Value
            objtb_PurchaseOrder.Delivery_Date = Date.Now
            If _Customer_Index Is Nothing AndAlso String.IsNullOrEmpty(_Customer_Index) Then
                objtb_PurchaseOrder.Customer_Index = ""
            Else
                objtb_PurchaseOrder.Customer_Index = _Customer_Index
            End If
            If Me.txtSupplier_Id.Tag = Nothing Then
                objtb_PurchaseOrder.Supplier_Index = ""
            Else
                objtb_PurchaseOrder.Supplier_Index = Me.txtSupplier_Id.Tag.ToString
            End If
            If Me.txtDepartment_Id.Tag = Nothing Then
                objtb_PurchaseOrder.Department_Index = ""
            Else
                objtb_PurchaseOrder.Department_Index = Me.txtDepartment_Id.Tag.ToString
            End If
            objtb_PurchaseOrder.Remark = Me.txtRemark.Text
            objtb_PurchaseOrder.Credit_Term = Me.txtCreditTerm.Text
            If Me.cboCurrency.SelectedValue Is Nothing Then
                objtb_PurchaseOrder.Currency_Index = ""
            Else
                objtb_PurchaseOrder.Currency_Index = Me.cboCurrency.SelectedValue.ToString
            End If
            objtb_PurchaseOrder.Exchange_Rate = Me.txtExRate.Text

            If Me.cboConditionPay.SelectedValue Is Nothing Then
                objtb_PurchaseOrder.PaymentMethod_Index = ""
            Else
                objtb_PurchaseOrder.PaymentMethod_Index = Me.cboConditionPay.SelectedValue.ToString
            End If
            'objtb_PurchaseOrder.Payment_Ref = ""
            'objtb_PurchaseOrder.FullPaid_Date = Date.Now
            objtb_PurchaseOrder.Amount = Me.txtSubtotal.Text
            objtb_PurchaseOrder.Discount_Percent = Me.txtDiscount_Percent.Text
            objtb_PurchaseOrder.Discount_Amt = Me.txtDiscount_Amt.Text
            objtb_PurchaseOrder.Deposit_Amt = Me.txtDeposit_Amt.Text
            objtb_PurchaseOrder.Total_Amt = Me.txtSubtotal.Text
            objtb_PurchaseOrder.VAT_Percent = Me.txtVAT_Percent.Text
            objtb_PurchaseOrder.VAT = Me.txtVAT.Text
            objtb_PurchaseOrder.Net_Amt = Me.txtNet_Amt.Text
            objtb_PurchaseOrder.Supplier_Address = Me.txtSupplier_Address.Text.ToString.Trim
            objtb_PurchaseOrder.Supplier_Tel = Me.txtSupplier_Phone.Text.ToString.Trim
            objtb_PurchaseOrder.Supplier_Fax = Me.txtSupplier_Fax.Text.ToString.Trim
            objtb_PurchaseOrder.Str1 = Me.txtRef1.Text.ToString.Trim
            objtb_PurchaseOrder.Str2 = Me.txtRef2.Text.ToString.Trim
            objtb_PurchaseOrder.Str3 = Me.txtTax_No.Text.ToString.Trim
            ' Note we use field Str9 to store Ship Address because the field length is 2000.
            objtb_PurchaseOrder.Str9 = Me.txtShip_Address.Text.ToString.Trim
            objtb_PurchaseOrder.Str4 = Me.txtShip_Phone.Text.ToString.Trim
            objtb_PurchaseOrder.Str5 = Me.txtShip_Fax.Text.ToString.Trim

            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    objtb_PurchaseOrder.add_by = WV_UserName ' Me.txtUser.Text
                Case enuOperation_Type.UPDATE
                    objtb_PurchaseOrder.update_by = WV_UserName ' Me.txtUser.Text
            End Select
            objtb_PurchaseOrder.Status = Status

            If Status = 2 Then
                objtb_PurchaseOrder.Status = 2
            Else
                objtb_PurchaseOrder.Status = 1
            End If

            ' ------ STEP 3: Prepare values for PO Items
            For i = 0 To grdPOItem.Rows.Count - 2

                With grdPOItem
                    objtb_PurchaseOrderItem = New tb_PurchaseOrderItem

                    If .Rows(i).Cells("Col_POItem_Index").Value = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        .Rows(i).Cells("Col_POItem_Index").Value = objDBIndex.getSys_Value("PurchaseOrderItem_Index")
                        objtb_PurchaseOrderItem.PurchaseOrderItem_Index = .Rows(i).Cells("Col_POItem_Index").Value
                        objDBIndex = Nothing
                    Else
                        objtb_PurchaseOrderItem.PurchaseOrderItem_Index = .Rows(i).Cells("Col_POItem_Index").Value.ToString
                    End If
                    objtb_PurchaseOrderItem.PurchaseOrder_Index = objtb_PurchaseOrder.PurchaseOrder_Index

                    If .Rows(i).Cells("Col_SKU_ID").Tag IsNot Nothing Then
                        ' We keep SKU Index to use for calculating SKU Ratio for Total Qty.
                        strTempSKUIndex = .Rows(i).Cells("Col_SKU_ID").Tag.ToString
                        objtb_PurchaseOrderItem.Sku_Index = .Rows(i).Cells("Col_SKU_ID").Tag.ToString

                    End If

                    Dim dgvcob As New DataGridViewComboBoxCell
                    ' ------ Here we cast out the combo box within datagrid to get 
                    ' ------ the selected Package Index value.
                    dgvcob = grdPOItem.Rows(i).Cells("Col_UOM")
                    strTempPackageIndex = dgvcob.Value.ToString
                    objtb_PurchaseOrderItem.Package_Index = strTempPackageIndex

                    ' ---------------------------------------
                    ' Modified by: Todd 28 Dec 2009
                    ' Fix bug to calculate ratio for Total_Qty.
                    dblSKURatio = getSKU_PackageRatio(strTempSKUIndex, strTempPackageIndex)
                    objtb_PurchaseOrderItem.Qty = CDbl(.Rows(i).Cells("Col_Qty").Value)

                    'If objtb_PurchaseOrderItem.Qty = 0 Then
                    '    MessageBox.Show("กรุณากรอกจำนวนสินค้า", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    '    Me.grdPOItem.Rows(i).Selected = True
                    '    DoSaveDocument = ""
                    '    Exit Function
                    'End If
                    objtb_PurchaseOrderItem.Ratio = dblSKURatio
                    objtb_PurchaseOrderItem.Total_Qty = dblSKURatio * .Rows(i).Cells("Col_Qty").Value.ToString

                    If .Rows(i).Cells("Col_Unit_Price").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.UnitPrice = CDbl(.Rows(i).Cells("Col_Unit_Price").Value.ToString)
                    Else
                        objtb_PurchaseOrderItem.UnitPrice = 0
                    End If

                    If .Rows(i).Cells("Col_Amount").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.Amount = CDbl(.Rows(i).Cells("Col_Amount").Value.ToString)
                    Else
                        objtb_PurchaseOrderItem.Amount = 0
                    End If

                    If .Rows(i).Cells("Col_Discount").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.Discount_Amt = CDbl(.Rows(i).Cells("Col_Discount").Value.ToString)
                    Else
                        objtb_PurchaseOrderItem.Discount_Amt = 0
                    End If
                    '''''''insert เปอร์เซ็นต์เกิน

                    If .Rows(i).Cells("Co_Percent_Over_Allow").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.Percent_Over_Allow = CDbl(.Rows(i).Cells("Co_Percent_Over_Allow").Value.ToString)
                    Else
                        objtb_PurchaseOrderItem.Percent_Over_Allow = 0
                    End If

                    If .Rows(i).Cells("Co_Percent_Under_Allow").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.Percent_Under_Allow = CDbl(.Rows(i).Cells("Co_Percent_Under_Allow").Value.ToString)
                    Else
                        objtb_PurchaseOrderItem.Percent_Under_Allow = 0
                    End If



                    ' ------ Todd 1 Jan 2010
                    ' ------ Calculate Total Amount
                    ' ------ Here the Total Amount equals to Amount - Discount
                    objtb_PurchaseOrderItem.Total_Amt = objtb_PurchaseOrderItem.Amount - objtb_PurchaseOrderItem.Discount_Amt

                    ' ------ Todd 1 Jan 2010
                    ' ------ Now we do not have the way to specify current for each item yet.
                    ' ------ We will save the same value of currency as it is selected in the PO header.
                    objtb_PurchaseOrderItem.Currency_Index = Me.cboCurrency.SelectedValue

                    ' ------ Todd 1 Jan 2010
                    ' ------ We do not use Weight & Volume yet, but prepare for future.
                    If .Rows(i).Cells("Col_Weight").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.Weight = .Rows(i).Cells("Col_Weight").Value.ToString
                    End If

                    If .Rows(i).Cells("Col_Volume").Value IsNot Nothing Then
                        objtb_PurchaseOrderItem.Volume = .Rows(i).Cells("Col_Volume").Value.ToString
                    End If

                    If .Rows(i).Cells("Col_Remark").Value Is Nothing Then
                        objtb_PurchaseOrderItem.Remark = ""
                    Else
                        objtb_PurchaseOrderItem.Remark = .Rows(i).Cells("Col_Remark").Value.ToString
                    End If

                    objtb_PurchaseOrderItem.Item_Seq = .Rows(i).Cells("col_no").Value.ToString
                End With

                objtb_PurchaseOrderItemCollection.Add(objtb_PurchaseOrderItem)

            Next

            ' ------ STEP 4: Prepare values for PO Multi-level discount
            For j = 0 To dtDiscount.Rows.Count - 1

                With dtDiscount
                    objtb_PurchaseOrder_Discount = New tb_PurchaseOrder_Discount

                    objtb_PurchaseOrder_Discount.PurchaseOrder_Index = objtb_PurchaseOrder.PurchaseOrder_Index

                    If dtDiscount.Rows(j).Item("Discount_Seq").ToString IsNot Nothing Then
                        objtb_PurchaseOrder_Discount.Discount_Seq = dtDiscount.Rows(j).Item("Discount_Seq").ToString
                    End If

                    If dtDiscount.Rows(j).Item("Discount_Type").ToString IsNot Nothing Then
                        objtb_PurchaseOrder_Discount.Discount_Type = dtDiscount.Rows(j).Item("Discount_Type").ToString
                    End If

                    If dtDiscount.Rows(j).Item("Discount_Amount1").ToString IsNot Nothing Then
                        objtb_PurchaseOrder_Discount.Discount_Amount1 = dtDiscount.Rows(j).Item("Discount_Amount1").ToString
                    End If

                    If dtDiscount.Rows(j).Item("Discount_Amount2").ToString IsNot Nothing Then
                        objtb_PurchaseOrder_Discount.Discount_Amount2 = dtDiscount.Rows(j).Item("Discount_Amount2").ToString
                    End If

                    If dtDiscount.Rows(j).Item("Discount_Total").ToString IsNot Nothing Then
                        objtb_PurchaseOrder_Discount.Discount_Total = dtDiscount.Rows(j).Item("Discount_Total").ToString
                    End If

                End With

                objtb_PurchaseOrder_DiscountItem.Add(objtb_PurchaseOrder_Discount)

            Next

            ' ------ STEP 5: Prepare values for tb_PurchaseOrderOther
            Dim objtb_PurchaseOrderOther As New tb_PurchaseOrderOther

            For k = 0 To grdPOOther.Rows.Count - 2

                With grdPOOther
                    objtb_PurchaseOrderOther = New tb_PurchaseOrderOther

                    If .Rows(k).Cells("Col_Index_Other").Value = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        .Rows(k).Cells("Col_Index_Other").Value = objDBIndex.getSys_Value("PurchaseOrderOther_Index")
                        objtb_PurchaseOrderOther.PurchaseOrderOther_Index = .Rows(k).Cells("Col_Index_Other").Value
                        objDBIndex = Nothing
                    Else
                        objtb_PurchaseOrderOther.PurchaseOrderOther_Index = .Rows(k).Cells("Col_Index_Other").Value.ToString
                    End If

                    objtb_PurchaseOrderOther.PurchaseOrder_Index = objtb_PurchaseOrder.PurchaseOrder_Index

                    If .Rows(k).Cells("Col_Seq_Other").Value IsNot Nothing Then
                        objtb_PurchaseOrderOther.Seq = .Rows(k).Cells("Col_Seq_Other").Value.ToString
                    End If

                    If .Rows(k).Cells("Col_Description_Other").Value IsNot Nothing Then
                        objtb_PurchaseOrderOther.Description = .Rows(k).Cells("Col_Description_Other").Value.ToString
                    End If

                    If .Rows(k).Cells("Col_Amount_Other").Value IsNot Nothing Then
                        ' Todd 28 Dec 2009
                        ' For this version we do not use Discount, Deposit & VAT 
                        ' in "Purchase Order Other" yet. So keep it this way for now.
                        objtb_PurchaseOrderOther.Amount = .Rows(k).Cells("Col_Amount_Other").Value.ToString
                        objtb_PurchaseOrderOther.Discount_Percent = 0
                        objtb_PurchaseOrderOther.Discount_Amt = 0
                        objtb_PurchaseOrderOther.Deposit_Amt = 0
                        objtb_PurchaseOrderOther.VAT_Percent = 0
                        objtb_PurchaseOrderOther.VAT = 0
                        objtb_PurchaseOrderOther.Total_Amt = objtb_PurchaseOrderOther.Amount
                        objtb_PurchaseOrderOther.Net_Amt = objtb_PurchaseOrderOther.Amount
                    End If

                    If .Rows(k).Cells("Col_Remark_Other").Value IsNot Nothing Then
                        objtb_PurchaseOrderOther.Str1 = .Rows(k).Cells("Col_Remark_Other").Value.ToString
                    End If

                    If .Rows(k).Cells("Col_Unit_Price_Other").Value IsNot Nothing Then
                        objtb_PurchaseOrderOther.Flo1 = .Rows(k).Cells("Col_Unit_Price_Other").Value.ToString
                    End If

                    If .Rows(k).Cells("Col_Qty_Other").Value IsNot Nothing Then
                        objtb_PurchaseOrderOther.Flo2 = .Rows(k).Cells("Col_Qty_Other").Value.ToString
                    End If

                End With
                objtb_PurchaseOrderOtherCollection.Add(objtb_PurchaseOrderOther)

            Next

            ' ------ STEP 6: Call the actual saving function in POTransaction class. 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDBPOTransaction As New POTransaction(POTransaction.enuOperation_Type.ADDNEW, objtb_PurchaseOrder, objtb_PurchaseOrderItemCollection, objtb_PurchaseOrder_DiscountItem, objtb_PurchaseOrderOtherCollection)
                    Me._PurchaseOrder_Index = objDBPOTransaction.SaveData
                    objDBPOTransaction = Nothing

                    'insert log
                    Dim obj_cls As New cls_syAditlog
                    obj_cls.Process_ID = 9
                    obj_cls.Description = "สร้างเอกสาร : " & Now.ToString("dd/MM/yyyy HH:mm:ssss")
                    obj_cls.Document_Index = Me._PurchaseOrder_Index
                    obj_cls.Document_No = Me.txtPO_No.Text
                    obj_cls.Log_Type_ID = 901 'Create
                    obj_cls.Insert_Master()

                Case enuOperation_Type.UPDATE
                    Dim objDBPOTransaction As New POTransaction(POTransaction.enuOperation_Type.UPDATE, objtb_PurchaseOrder, objtb_PurchaseOrderItemCollection, objtb_PurchaseOrder_DiscountItem, objtb_PurchaseOrderOtherCollection)
                    Me._PurchaseOrder_Index = objDBPOTransaction.SaveData
                    objDBPOTransaction = Nothing

                    'insert log
                    Dim obj_cls As New cls_syAditlog
                    obj_cls.Process_ID = 9
                    obj_cls.Description = "แก้ไขเอกสาร : " & Now.ToString("dd/MM/yyyy HH:mm:ssss")
                    obj_cls.Document_Index = Me._PurchaseOrder_Index
                    obj_cls.Document_No = Me.txtPO_No.Text
                    obj_cls.Log_Type_ID = 906 'แก้ไข PO
                    obj_cls.Insert_Master()
            End Select

            ' If save succeeded, _PurchaseOrder_Index will not be ""
            DoSaveDocument = Me._PurchaseOrder_Index

        Catch ex As Exception
            Throw ex
            DoSaveDocument = ""
            'objtb_PurchaseOrder.Dispose()
            'objtb_PurchaseOrderItemCollection = Nothing
        Finally
            objtb_PurchaseOrder.Dispose()
            objtb_PurchaseOrderItemCollection = Nothing
        End Try

    End Function

#End Region

#Region " UNUSED & BACKUP FUNCTIONS(TEMPORARY) "
    '===================================================
    ' Todd - 27 Dec 2009
    ' The following functions are not yet tested. 
    ' We will use it in the next version.
    '===================================================

    Private Sub btnMultiLevelDiscount_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMultiLevelDiscount.Click

        Select Case objStatus
            Case enuOperation_Type.ADDNEW
                Dim frm As New WMS_STD_INB_PO.frmDisCount
                frm.tb_PO_Discount = Me.dtDiscount
                frm.Amount = CDbl(Me.txtSubtotal.Text)
                frm.ShowDialog()
                Me.dtDiscount = frm.tb_PO_Discount

                If frm.discount > 0 Then
                    chkDiscount.Checked = False
                    txtDiscount_Percent.Text = 0.0
                    txtDiscount_Amt.Text = Format(frm.discount, "#,##0.00")
                End If

            Case enuOperation_Type.UPDATE

                Dim frm As New WMS_STD_INB_PO.frmDisCount
                frm.objStatus = objStatus
                frm.PurchaseOrder_Index = Me.PurchaseOrder_Index
                frm.tb_PO_Discount = Me.dtDiscount
                frm.Amount = CDbl(Me.txtSubtotal.Text)
                frm.ShowDialog()
                Me.dtDiscount = frm.tb_PO_Discount
                If frm.discount > 0 Then
                    chkDiscount.Checked = False
                    txtDiscount_Percent.Text = 0.0
                    txtDiscount_Amt.Text = Format(frm.discount, "#,##0.00")
                End If
        End Select
    End Sub

    Private Sub btnCompare_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCompare.Click

        Dim i As Integer
        Try
            Dim frm As New WMS_STD_INB_PO.frmPoCompare
            'frm.dvtTemp = Me.dgvTmp
            frm.Icon = Me.Icon

            frm.dvtTemp.Rows.Clear()
            frm.dvtTemp.AllowUserToAddRows = False
            frm.dvtTemp.Columns.Add("Col_skuid", "Sku")
            frm.dvtTemp.Columns.Add("proType", "Productype")
            frm.dvtTemp.Columns.Add("proSum", "Qty")

            For i = 0 To grdPOItem.RowCount - 2
                frm.dvtTemp.Rows.Add()
                frm.dvtTemp.Rows(i).Cells(0).Value = grdPOItem.Rows(i).Cells("Col_SKU_ID").Value
                frm.dvtTemp.Rows(i).Cells(1).Value = grdPOItem.Rows(i).Cells("Col_Product_Type").Value
                frm.dvtTemp.Rows(i).Cells(2).Value = CDbl(grdPOItem.Rows(i).Cells("Col_Qty").Value)

            Next

            frm.ShowDialog()

            If frm.SaveType = 1 Then
                For i = 0 To frm.dvtTemp.RowCount - 1
                    If frm.dvtTemp.Rows(i).Cells("ColVolume").Value Is Nothing Then
                        Continue For
                    End If

                    grdPOItem.Rows(i).Cells("Col_Qty").Value = CDbl(frm.dvtTemp.Rows(i).Cells("ColVolume").Value)
                Next
            End If

            frm.dvtTemp.Rows.Clear()

        Catch ex As Exception
            W_MSG_Error(ex.Message)

        End Try
    End Sub



    'Sub UpdatePOStatus(ByVal PurchaseOrder_Index As String, ByVal DocStatus As Integer)
    '    'TODO: HARDCODE-MSG
    '    Try
    '        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
    '        'Dim oDTPOTransaction As New DataTable

    '        objPOTransaction.UpdatePOStatus(PurchaseOrder_Index, DocStatus)
    '        'oDTPOTransaction = objPOTransaction.DataTable

    '        'For i As Integer = 0 To oDTPOTransaction.Rows.Count - 1
    '        '    Qty_Rever = oDTPOTransaction.Rows(i).Item("Received_Qty").ToString
    '        'Next

    '        MessageBox.Show("บันทึกข้อมูลเรียบร้อยแล้ว", "ผลการบันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Sub

    '===================================================
    ' Todd - 27 Dec 2009
    ' The following are backup events which we commented out.
    ' These should be old junk code.
    '===================================================
    'Private Sub getPurchaseOrderDetailStatus(ByVal PurchaseOrder_Index As String)

    '    Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
    '    Dim objDTPOTransaction As DataTable = New DataTable

    '    Try
    '        objPOTransaction.getPurchaseOrderDetail(PurchaseOrder_Index)
    '        objDTPOTransaction = objPOTransaction.DataTable

    '        Me.grdPOItem.Rows.Clear()
    '        gblnDataGridClick = False

    '        For i As Integer = 0 To objDTPOTransaction.Rows.Count - 1
    '            With Me.grdPOItem
    '                Me.grdPOItem.Rows.Add()

    '                .Rows(i).Cells("Col_POItem_Index").Value = objDTPOTransaction.Rows(i).Item("PurchaseOrderItem_Index").ToString
    '                .Rows(i).Cells("Col_SKU_ID").Value = objDTPOTransaction.Rows(i).Item("Sku_Id").ToString
    '                .Rows(i).Cells("Col_SKU_ID").Tag = objDTPOTransaction.Rows(i).Item("Sku_Index").ToString
    '                .Rows(i).Cells("Col_SKU_ID").ReadOnly = True
    '                .Rows(i).Cells("Col_UOM").Value = objDTPOTransaction.Rows(i).Item("PackDes").ToString
    '                .Rows(i).Cells("Col_UOM").Tag = objDTPOTransaction.Rows(i).Item("Package_Index").ToString
    '                .Rows(i).Cells("Col_Product_Type").Value = objDTPOTransaction.Rows(i).Item("Producttype").ToString
    '                .Rows(i).Cells("Col_Description").Value = objDTPOTransaction.Rows(i).Item("Sku_Des").ToString
    '                .Rows(i).Cells("Col_Qty").Value = objDTPOTransaction.Rows(i).Item("Qty").ToString - objDTPOTransaction.Rows(i).Item("Received_Qty").ToString
    '                .Rows(i).Cells("Col_Unit_Price").Value = objDTPOTransaction.Rows(i).Item("UnitPrice").ToString
    '                .Rows(i).Cells("Col_Amount").Value = (objDTPOTransaction.Rows(i).Item("Qty").ToString - objDTPOTransaction.Rows(i).Item("Received_Qty").ToString) * objDTPOTransaction.Rows(i).Item("UnitPrice").ToString 'objDTPOTransaction.Rows(i).Item("Amount").ToString
    '                .Rows(i).Cells("Col_Weight").Value = objDTPOTransaction.Rows(i).Item("Flo3").ToString
    '                .Rows(i).Cells("Col_Remark").Value = objDTPOTransaction.Rows(i).Item("Remark").ToString
    '                .Rows(i).Cells("Col_VAT_Value").Value = objDTPOTransaction.Rows(i).Item("Flo1").ToString
    '                .Rows(i).Cells("Col_Before_VAT").Value = objDTPOTransaction.Rows(i).Item("Flo2").ToString

    '            End With
    '        Next

    '        gblnDataGridClick = True
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objPOTransaction = Nothing
    '        objDTPOTransaction = Nothing
    '    End Try
    'End Sub

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    ' hold  new for project report coustom
    '    Dim frm As New frmReport_PO
    '    frm.Report_Name = "rptPR"
    '    frm.Document_Index = PurchaseOrder_Index
    '    frm.Show()
    'End Sub

    'Private Sub btnPrint_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)

    '    Dim oconfig_Report As New config_Report

    '    Try
    '        Dim frm As New frmReport_PO
    '        frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo("PO_PrintOut", "And PurchaseOrder_Index ='" & Me.PurchaseOrder_Index & "'")
    '        frm.ShowDialog()

    '        '###################################
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '    End Try

    'End Sub

    ' ------ Todd 27 Dec 2009 
    ' ------ This function is redundant. Use function getPOItem_ReceivedQty instead.
    'Public Function CheckDeleteQTYBal(ByVal PurchaseOrderItem_Index As String) As Double
    '    Try
    '        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.SEARCH)
    '        Dim oDTPOTransaction As New DataTable

    '        Dim Received_Qty As Double = 0

    '        objPOTransaction.getPurchaseOrderItem(PurchaseOrderItem_Index)
    '        oDTPOTransaction = objPOTransaction.DataTable


    '        For i As Integer = 0 To oDTPOTransaction.Rows.Count - 1
    '            Received_Qty = oDTPOTransaction.Rows(i).Item("Received_Qty").ToString
    '        Next

    '        oDTPOTransaction = Nothing
    '        oDTPOTransaction = Nothing

    '        Return Received_Qty

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function
#End Region

#Region "FORMATTING NUMBER"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Formatting 
    ''' </remarks>

    Private Sub grdPOItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOItem.CellEndEdit
        Try
            Select Case e.ColumnIndex
                Case grdPOItem.CurrentRow.Cells("Col_Unit_Price").ColumnIndex
                    If grdPOItem.CurrentRow.Cells("Col_Unit_Price").Value Is Nothing Then
                        grdPOItem.CurrentRow.Cells("Col_Unit_Price").Value = "0.00"
                    Else
                        '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                        If DataNumeric(grdPOItem.CurrentRow.Cells("Col_Unit_Price").Value) = False Then
                            grdPOItem.CurrentRow.Cells("Col_Unit_Price").Value = "0.00"
                            Exit Sub
                        End If
                        grdPOItem.CurrentRow.Cells("Col_Unit_Price").Value = CDbl(grdPOItem.CurrentRow.Cells("Col_Unit_Price").Value)
                    End If

                Case grdPOItem.CurrentRow.Cells("Col_Qty").ColumnIndex
                    If grdPOItem.CurrentRow.Cells("Col_Qty").Value Is Nothing Then
                        grdPOItem.CurrentRow.Cells("Col_Qty").Value = "0.00"
                    Else
                        '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                        If DataNumeric(grdPOItem.CurrentRow.Cells("Col_Qty").Value) = False Then
                            grdPOItem.CurrentRow.Cells("Col_Qty").Value = "0.00"
                            Exit Sub
                        End If
                        grdPOItem.CurrentRow.Cells("Col_Qty").Value = CDbl(grdPOItem.CurrentRow.Cells("Col_Qty").Value)
                    End If

                Case grdPOItem.CurrentRow.Cells("Col_Discount").ColumnIndex
                    If grdPOItem.CurrentRow.Cells("Col_Discount").Value Is Nothing Then
                        grdPOItem.CurrentRow.Cells("Col_Discount").Value = "0.00"
                    Else
                        '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                        If DataNumeric(grdPOItem.CurrentRow.Cells("Col_Discount").Value) = False Then
                            grdPOItem.CurrentRow.Cells("Col_Discount").Value = "0.00"
                            Exit Sub
                        End If
                        grdPOItem.CurrentRow.Cells("Col_Discount").Value = CDbl(grdPOItem.CurrentRow.Cells("Col_Discount").Value)
                    End If
                Case grdPOItem.CurrentRow.Cells("Col_Amount").ColumnIndex
                    If grdPOItem.CurrentRow.Cells("Col_Amount").Value Is Nothing Then
                        grdPOItem.CurrentRow.Cells("Col_Amount").Value = "0.00"
                    Else
                        '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                        If DataNumeric(grdPOItem.CurrentRow.Cells("Col_Amount").Value) = False Then
                            grdPOItem.CurrentRow.Cells("Col_Amount").Value = "0.00"
                            Exit Sub
                        End If

                        grdPOItem.CurrentRow.Cells("Col_Amount").Value = CDbl(grdPOItem.CurrentRow.Cells("Col_Amount").Value)
                    End If


            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
        If grdPOItem.RowCount < 0 Then
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' Check null data is numberic data
    ''' </summary>
    ''' <param name="pstrValue">Numberic data</param>
    ''' <returns>ture is numberic; false is not numberic</returns>
    ''' <remarks>เป็นการ check data ที่ user ป้อนเข้ามาจาก UI</remarks>
    ''' 
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
#End Region

#Region "Key Number In Datagrid"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In Double 
    ''' </remarks>
    Private Sub grdPOItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdPOItem.EditingControlShowing
        Try
            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            Dim strName As String = grdPOItem.Columns(grdPOItem.CurrentCell.ColumnIndex).Name
            If (strName <> "Col_Btn_GetSKU") And (strName <> "Col_UOM") Then
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
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In Double 
    ''' </remarks>
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdPOItem.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdPOItem.Columns("Col_Unit_Price").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdPOItem.Columns("Col_Qty").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdPOItem.Columns("Col_Discount").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdPOItem.Columns("Col_Amount").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
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
    ''' Add Date : 21/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In Double 
    ''' </remarks>
    Function Check_GrdKeyPress(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdPOItem.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdPOItem.CurrentRow.Cells(Column_Index).EditedFormattedValue
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

    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If grdPOItem.Rows.Count <= 1 Then Exit Sub
            RowCopy_PO(grdPOItem)

            'ManageRowItem_Seq()
        Catch ex As Exception

            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Add By TOP:
    'Add Date: 19/03/2013 
    'btnCopy Rows
    Function RowCopy_PO(ByVal grd As DataGridView) As Integer
        Try
            Dim num As Integer = grd.CurrentRow.Index
            Dim intNewRowIndex As Integer = 0
            Dim i As Integer = 0
            Counter = -9

            If grd.Rows(num).Cells("Col_SKU_ID").Value = Nothing Then
                Exit Function
            End If


            grd.Rows.Add()


            Dim strSku_ID As String = grd.Rows(num).Cells("Col_SKU_ID").Value

            Me.getPurchaseOrderDetail(strSku_ID)
            '(Me.getSKU_Package(strSku_ID, grd.Rows.Count - 2))
            'Me.getSKU_PackageCal(strSku_index, grd.Rows.Count - 2)


            'Me.get_Package(grd.Rows.Count - 2, strSku_index)
            For i = 0 To grd.Columns.Count - 1
                'Select Case grd.Rows(grd.Rows.Count - 2).Cells(i).ColumnIndex
                '    Case Is = grd.Columns("col_No").Index
                '        Continue For
                'End Select

                Select Case grd.Rows(grd.Rows.Count - 2).Cells(i).ColumnIndex
                    Case Is = grd.Columns("col_No").Index
                        Continue For
                End Select

                grd.Rows(grd.Rows.Count - 2).Cells(i).Value = grd.Rows(num).Cells(i).Value
            Next

            For i = 0 To grd.Columns.Count - 1
                grd.Rows(grd.Rows.Count - 2).Cells(i).Tag = grd.Rows(num).Cells(i).Tag
            Next


            grd.Rows(grd.Rows.Count - 2).Cells("ColUnitPrice").Value = 1
            grd.Rows(grd.Rows.Count - 2).Cells("ColIndex").Value = ""
            Counter = 0

            'AutoSort
            AutoSort(grdPOItem)
            Return 1
        Catch ex As Exception
            Counter = 0
            Throw ex

        End Try
    End Function
    Private Sub AutoSort(ByVal grd As DataGridView)
        Try

            grd.CommitEdit(DataGridViewDataErrorContexts.Commit)

            ' grd.Columns("col_No").ValueType = GetType(System.Int32)

            ' grd.Sort(grd.Columns("col_No"), System.ComponentModel.ListSortDirection.Ascending)

            ' SetRunningNo(grd)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub SetRunningNo(ByVal grd As DataGridView)
        For i As Integer = 0 To grd.RowCount - 2
            grd.Rows(i).Cells("col_No").Value = i + 1
        Next
    End Sub
    'END btnCopy Rows
    'BEGIN btnInsertRow
    Private Sub BtnInsert_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnInsert.Click
        Try
            If grdPOItem.Rows.Count <= 1 Then Exit Sub
            RowInsert_ASN(grdPOItem)

            'ManageRowItem_Seq()
        Catch ex As Exception

            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Function RowInsert_ASN(ByVal grd As DataGridView) As Integer
        Try
            Dim num As Integer = grd.CurrentRow.Index
            grd.Rows.Insert(num)
            SetNewSeqNo(grdPOItem)
            SetRunningNo(grdPOItem)
            Return 1

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub SetNewSeqNo(ByVal grd As DataGridView)
        For i As Integer = 0 To grd.RowCount - 2
            grd.Rows(i).Cells("col_No").Value = (i + 1) * 10
        Next
    End Sub
    'END btnInsertRow

    Private Sub BtnSort_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnSort.Click
        AutoSort(grdPOItem)
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        ' ====== TODO: HARDCODE-MSG   
        ' ====== Todd: 26 Dec 2009 - Need to get rid of hardcode text message in Thai.
        If grdPOItem.Rows.Count <= 0 Then Exit Sub

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        If MessageBox.Show("คุณต้องการยืนยันรายการใบสั่งซื้อใช่หรือไม่", "ยืนยันรายการใบสั่งซื้อ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No = True Then
            Exit Sub
        End If

        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.UPDATE)
        Dim oPo As New tb_PurchaseOrder
        objPOTransaction.newPurchaseOrder_Index = Me.PurchaseOrder_Index
        oPo.PurchaseOrder_Index = Me.PurchaseOrder_Index
        oPo.PurchaseOrder_No = Me.txtPO_No.Text

        Try

            If objPOTransaction.Confirm_PO(oPo) Then
                MessageBox.Show("ยืนยันรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Dim dt As New DataTable
                oPo.getPo_Status(Me.PurchaseOrder_Index)
                dt = oPo.GetDataTable
                Me.Status = dt.Rows(0).Item("Status").ToString
                Call ManageButtonByDocStatus(Me.Status)


            Else
                MessageBox.Show("ไม่สามารถยืนยันรายการได้ ระบบทำงานผิดพลาด", "ผลการยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            objPOTransaction = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            objPOTransaction = Nothing
        End Try
    End Sub

    Private Sub btnRecived_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecived.Click

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        If Me.Status = 2 Then

            Dim frm As New frmDeposit_WMS_V2(frmDeposit_WMS_V2.enuOperation_Type.ADDNEW)
            frm.Receive_Process = frmDeposit_WMS_V2.Receive_Process_Enum.PO
            frm.DocumentPlan_Index = Me.PurchaseOrder_Index
            frm.DocumentPlan_No = txtPO_No.Text
            frm.ShowDialog()
            '            getPOViewSearch()
        ElseIf Me.Status = 3 Then
            btnRecived.Enabled = False
            W_MSG_Information("สถานะของใบสั่งซื้อนี้เสร็จสิ้นแล้ว")
        Else

            W_MSG_Information("สถานะของใบสั่งซื้อไม่ถูกต้อง")
        End If
    End Sub

    Private Sub grdPOItem_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdPOItem.RowsAdded
        Try
            SetRunningNo(grdPOItem)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdPOItem_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdPOItem.RowsRemoved
        Try
            SetRunningNo(grdPOItem)
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

    Private Sub frmPO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New frmConfigPurchaseOrder
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 9)
                    oFunction.SW_Language_Column(Me, Me.grdPOItem, 9)
                    oFunction.SW_Language_Column(Me, Me.grdPOOther, 9)
                    oFunction.SW_Language_Column(Me, Me.grdPORemain, 9)
                    oFunction.SW_Language_Column(Me, Me.grdPOAlreadyReceipt, 9)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtDiscount_Percent_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscount_Percent.KeyPress
        Try
            e.Handled = Check_GrdKeyPress(txtDiscount_Percent, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_Percent_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVAT_Percent.KeyPress
        Try
            e.Handled = Check_GrdKeyPress(txtVAT_Percent, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Amt_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscount_Amt.KeyPress
        Try
            e.Handled = Check_GrdKeyPress(txtDiscount_Amt, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDeposit_Amt_KeyPress_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDeposit_Amt.KeyPress
        Try
            e.Handled = Check_GrdKeyPress(txtDeposit_Amt, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function Check_GrdKeyPress(ByVal txtBox As System.Windows.Forms.TextBox, ByVal e As System.Windows.Forms.KeyPressEventArgs) As Boolean
        Try

            If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                If e.KeyChar = "." Then
                    If txtBox.Text <> "" Then
                        Dim strData As String = txtBox.Text
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

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub txtVAT_Percent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVAT_Percent.Leave
        Try
            CalSubtotalAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Percent_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscount_Percent.Leave
        Try
            CalSubtotalAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub CalPercenAmount()
        Try
            If chkDiscount.Checked = False Then Exit Sub
            If IsNumeric(txtDiscount_Amt.Text) = True Then
                If CDec(txtDiscount_Amt.Text) > 0 Then
                    Dim DisPercen As Decimal = 0.0
                    Dim Price As Decimal = 0.0
                    Dim DisPrice As Decimal = CDec(txtDiscount_Amt.Text)

                    If IsNumeric(txtSubtotal.Text) = True Then
                        Price = txtSubtotal.Text
                        If Price = 0 Then Price = 1
                    End If

                    DisPercen = (DisPrice * 100) / Price
                    txtDiscount_Percent.Text = Format(DisPercen, "#,##0.00")
                Else
                    txtDiscount_Percent.Text = "0.00"
                    txtDiscount_Amt.Text = "0.00"
                End If
            Else
                txtDiscount_Amt.Text = "0.00"
                txtDiscount_Percent.Text = "0.00"
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtDiscount_Amt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscount_Amt.Leave
        Try
            CalPercenAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDeposit_Amt_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDeposit_Amt.Leave
        Try
            CalSubtotalAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPO_PR_Import_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPO_PR_Import.Click
        Try
            If (Not (Me.Status = 0 Or Me.Status = 1)) Then Exit Sub
            If (Not Me.objStatus = enuOperation_Type.UPDATE) Then
                Dim _result As Windows.Forms.DialogResult = MessageBox.Show(String.Format("การดึงข้อมูลจากใบคำขอสั่งซื้อ(PR)ต้องสร้างใบสั่งซื้อก่อน{0}คุณต้องการสร้างเอกสารหรือไม่?", vbCrLf), "สร้างเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
            Else
                Dim _result As Windows.Forms.DialogResult = MessageBox.Show(String.Format("การดึงข้อมูลจากใบคำขอสั่งซื้อ(PR)ต้องบันทึกข้อมูลก่อน{0}คุณต้องการบันทึกข้อมูลหรือไม่?", vbCrLf), "บันทึกข้อมูล", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
            End If
            Me.btnSave_Click(Nothing, Nothing)
            If (Not Me.objStatus = enuOperation_Type.UPDATE) Then Exit Sub

            Dim _frm As New frmPO_PR_Import(Me.PurchaseOrder_Index)
            _frm.ShowDialog()
            If (_frm.IsProcess) Then
                Me.RefreshUpdate()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub RefreshUpdate()
        Try
            If (Not Me.objStatus = enuOperation_Type.UPDATE) Then Exit Sub
            Me.getPurchaseOrderHeader(PurchaseOrder_Index)
            Me.getPurchaseOrderDetail(PurchaseOrder_Index)
            Me.getPurchaseOrderOther(PurchaseOrder_Index)
            Call ManageButtonByDocStatus(Status)
            Me.CalSubtotalAmount()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnEdit_Special_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_Special.MouseHover
        Try
            If lblDue_Date.BackColor = Color.Transparent Then
                lblDue_Date.BackColor = Color.Red
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnEdit_Special_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_Special.MouseLeave
        Try
            lblDue_Date.BackColor = Color.Transparent
        Catch ex As Exception
            W_MSG_Error(ex.Message())
        End Try

    End Sub

    Private Sub btnEdit_Special_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_Special.Click
        Try
            If W_MSG_Confirm("ต้องการแก้ไข" & lblDue_Date.Text & " ใช่ หรือ ไม่") = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            Dim _excSQL As New DBType_SQLServer
            _excSQL.DBExeNonQuery("Update tb_PurchaseOrder set Expected_Delivery_Date = '" & Me.dtpDue_Date.Value.ToString("yyyy/MM/dd HH:mm:ssss") & "' where PurchaseOrder_index = '" & PurchaseOrder_Index & "'")
            Dim obj_cls As New cls_syAditlog
            obj_cls.Process_ID = 9
            obj_cls.Description = "แก้ไข " & lblDue_Date.Text & " จากวันที่ " & Me.Due_Date.ToString("dd/MM/yyyy") & " เป็น " & Me.dtpDue_Date.Value.ToString("dd/MM/yyyy")
            obj_cls.Document_Index = Me.PurchaseOrder_Index
            obj_cls.Document_No = Me.txtPO_No.Text
            obj_cls.Log_Type_ID = 905 'แก้ไข PO
            If obj_cls.Insert_Master() Then
                W_MSG_Information("บันทึกเรียบร้อย")
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class
