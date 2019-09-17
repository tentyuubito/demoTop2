Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_OUTB_SO_Datalayer
Imports System.Windows.Forms

Public Class frmSO
    Public Copy_Red As Boolean = False
    Public Copy_Red_NotStock As Boolean = False
    Public Copy_All As Boolean = False

    Private _Copy_SalesOrder_Index As String = ""

    'Public Document_Group_Name As String = "" 'KSL

    Private _USE_SO_PRICE_ALERT As Boolean = False
    Private _USE_PRODUCT_CUSTOMER As Boolean = False
    Private Consignee_Index As String = ""
    Private _Customer_Index As String = ""
    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
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


    '######################################################
    ' SO Module
    '######################################################
    ' Create By : ????
    ' Create Date : ???
    ' Remark : Old Moduel
    '######################################################
    ' Update By : Ta
    ' Update Date : 22/01/2010
    ' Remark : 
    '  Function Name : IsNumeric
    '  เพิ่ม Function การ Check data numberic ที User ป้อนมาเป็นช่องว่าง 
    '######################################################

#Region " CLASS PROPERTY & VARIABLE DECLARATION "

    Public SaveType As Integer = 0
    Public statusFrm As LoadStatus

    ' This variable is set to FALSE when this form is loaded in Edit mode
    ' to prevent CellValueChanged property to keep recalculate data when we assign
    ' data from DB to datagrid.
    Public gblnDataGridClick As Boolean = True


    Private _SalesOrder_Index As String = ""
    'Private _SalesOrderStatus As Boolean = False
    ' Todd 8 Jan 2010 - We removed _SalesOrderStatus and use _Status instead
    ' for better flexibility.
    Private _Status As Integer = 0
    Private _Status_Manifest As Integer = 0


    'compare
    'Public poNo As String = ""
    'Public skuid As String = ""
    'Public TypeProduct As String = ""
    'Public NumPro As String = ""

    Enum LoadStatus
        Defalt
        Edit
        summit
    End Enum

    Public Property Status() As Integer
        Get
            Return _Status
        End Get
        Set(ByVal value As Integer)
            _Status = value
        End Set
    End Property

    Public Property Status_Manifest() As Integer
        Get
            Return _Status_Manifest
        End Get
        Set(ByVal value As Integer)
            _Status_Manifest = value
        End Set
    End Property


    Public Property SalesOrder_Index() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal value As String)
            _SalesOrder_Index = value
        End Set
    End Property

#End Region

#Region " OERATION TYPE "
    Public objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        DELETE
        SEARCH
        CANCEL
        NULL
        COPY
    End Enum
#End Region

#Region " FORM LOAD "

    ''' <summary>
    ''' Form load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd 17 Jan 2010 - Add language switching code.
    ''' </remarks>
    Private Sub frmSO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try
            ' ========== Manage Language Functions Begin ==========
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 10)
            oFunction.SW_Language_Column(Me, Me.grdSOItem, 10)
            oFunction.SW_Language_Column(Me, Me.grdSORemain, 10)
            ' ========== Manage Language Functions End ==========

            Dim oConfig As New config_CustomSetting
            Me._USE_SO_PRICE_ALERT = oConfig.getConfig_Key_USE("USE_SO_PRICE_ALERT")

            ' ------ STEP 1: Get master data for combo boxes
            Me.getCurrency()
            Me.getDocumentType(10)
            Me.getReportName(10)
            Me.getPaymentType()
            Me.getComboRoute()
            Me.getComboRegion()
            Me.SetDEFAULT_VAT()

            If checkHaveRoute() = True Then
                Me.getComboSubRoute(cboRoute.SelectedValue.ToString)
            End If



            Me.getDistributionCenter()
            Call SetDEFAULT_CUSTOMER_INDEX()

            txtCustomer_Id.Tag = Me.Customer_Index
            txtCustomer_Id.Text = Me.CustomerID
            txtCustomer_Name.Text = Me.CustomerName
            Me.getCustomer()

            'Dim obj As New clsSO
            'Dim res As Boolean
            'Dim SalesOrderList() As String = {"0010000000052", "0010000000053", "0010000000058", "0010000000062", "0010000000063"}
            'res = obj.isOverLocationBalance(SalesOrderList)

            cboReserve_Doc.SelectedIndex = 0
            Me.btnClose_SO.Enabled = False

            ' ------ STEP 2: Check which operation type it is loaded, Add or Edit.
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    SalesOrder_Index = objDBTempIndex.getSys_Value("SalesOrder_Index")
                    objDBTempIndex = Nothing
                    ' Set default customer
                    SetDEFAULT_CUSTOMER_INDEX()

                    ' Default user to display
                    txtUser.Text = WV_UserName

                Case enuOperation_Type.UPDATE
                    Me._SalesOrder_Index = SalesOrder_Index
                    Me.getSOHeader(SalesOrder_Index)
                    Me.getDocumentType_Itemstatus(0)
                    Me.getSODetail(SalesOrder_Index)

                    Call ManageButtonByDocStatus(Status)

                Case enuOperation_Type.COPY
                    Me._Copy_SalesOrder_Index = Me.SalesOrder_Index
                    Me._SalesOrder_Index = SalesOrder_Index
                    Me.getSOHeader(SalesOrder_Index)
                    Me.getDocumentType_Itemstatus(0)
                    Me.getSODetail(SalesOrder_Index)

                    Dim objDBTempIndex As New Sy_Temp_AutoNumber
                    SalesOrder_Index = objDBTempIndex.getSys_Value("SalesOrder_Index")
                    objDBTempIndex = Nothing
                    Me.Status = 0

                    Dim xcontdoc As Integer = 0
                    If Me.txtInterface_No.Text.Trim <> "" Then
                        Dim xdoc As New DBType_SQLServer
                        xcontdoc = xdoc.DBExeQuery_Scalar(String.Format("select count(*) cnt from tb_SalesOrder where Interface_No = '{0}' AND Status NOT IN (-1)", Me.txtInterface_No.Text.Trim))
                        'xcontdoc += 1
                    Else
                        xcontdoc += 1
                    End If
                    'Me.txtSO_No.Text &= "-1"

                    Me.txtSO_No.Text = Me.txtInterface_No.Text.Trim & "-" & xcontdoc.ToString
                    For irow As Integer = 0 To Me.grdSOItem.RowCount - 1
                        Me.grdSOItem.Rows(irow).Cells("Col_SOItem_Index").Value = ""
                    Next

            End Select

            Me.tbcSO.TabPages.Remove(Me.TabPage_Visible)

            'Select Case Me.Document_Group_Name
            '    Case "BOM"
            '        Me.btnFG.Visible = True
            '    Case Else
            '        Me.btnFG.Visible = False
            'End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function checkHaveRoute() As Boolean
        Try
            Dim odt As New DataTable
            odt = cboRoute.DataSource
            If odt.Rows.Count < 1 Then
                Return False
            Else
                Return True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub getComboRegion()
        Dim objClassDB As New ms_TransportRegion(ms_TransportRegion.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable

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
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' This sub is to enable/disable buttons during form load based on status of document.
    ''' It is called when this form is loaded.
    ''' </summary>
    ''' <param name="intDocStatus"></param>
    ''' <remarks>
    ''' Added by: Todd 8 Jan 2010
    ''' </remarks>
    Private Sub ManageButtonByDocStatus(ByVal intDocStatus As Integer)
        Me.btnEdit.Enabled = False
        Select Case intDocStatus
            Case -1 ' ยกเลิก
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.grdSOItem.ReadOnly = True
                Me.btnClose_SO.Enabled = False

            Case 1 ' รอยืนยัน
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = True
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnClose_SO.Enabled = False

                Me.btnEdit.Enabled = True

            Case 6 ' ค้างจ่าย
                ' 2016-12-26
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.grdSOItem.ReadOnly = True
                'Me.btnSave.Enabled = True
                'Me.btnDelete.Enabled = True
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnClose_SO.Enabled = True

                Me.btnEdit.Enabled = True

                Me.gbShippingAddress.Enabled = False
            Case 2 ' รอเบิก
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.grdSOItem.ReadOnly = True
                Me.btnClose_SO.Enabled = True

                Me.btnEdit.Enabled = True

            Case 3, 4, 5 ' รอส่ง / กำลังจัดส่ง / เสร็จสิ้น
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.cboPrint.Enabled = True
                Me.btnPrint.Enabled = True
                Me.grdSOItem.ReadOnly = True
                Me.btnClose_SO.Enabled = False

                Me.gbShippingAddress.Enabled = False
            Case Else
                ' Other status are not normal, 
                ' so we do not allow any activities.
                Me.btnSave.Enabled = False
                Me.btnDelete.Enabled = False
                Me.cboPrint.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnClose_SO.Enabled = False

        End Select

        Select Case _Status_Manifest
            Case 0, 1
            Case Else
                Me.gbShippingAddress.Enabled = False
        End Select

    End Sub

#End Region

#Region " INITIALIZE CONTROL "

    ''' <summary>
    ''' Get document type from Process ID.
    ''' </summary>
    ''' <param name="Process_Id"></param>
    ''' <remarks></remarks>
    Private Sub getDocumentType(ByVal Process_Id As Integer)

        'Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        'Dim objDT As DataTable = New DataTable

        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        Dim xSQL As String = ""
        Try

            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                Case Else
                    If Not String.IsNullOrEmpty(Me.SalesOrder_Index) Then
                        xSQL = " SELECT ISNULL(DCT.Document_Group_Name,'') Document_Group_Name "
                        xSQL &= " FROM tb_SalesOrder"
                        xSQL &= "   inner join ms_DocumentType DCT ON DCT.DocumentType_Index = tb_SalesOrder.DocumentType_Index"
                        xSQL &= " WHERE  SalesOrder_Index = '" & Me.SalesOrder_Index & "'"
                        Dim xScalar As String = objClassDB.DBExeQuery_Scalar(xSQL)
                        'If Not String.IsNullOrEmpty(xScalar) Then
                        '    Me.Document_Group_Name = xScalar
                        'End If
                    End If
            End Select
            'objClassDB.getDocumentType(10)
            'objDT = objClassDB.DataTable
            xSQL = " SELECT  DocumentType_Index,  Description FROM     ms_DocumentType"
            xSQL &= " WHERE Process_Id= 10 and status_id not in (-1)"
            'xSQL &= " AND Document_Group_Name = '" & Me.Document_Group_Name & "'"
            objDT = objClassDB.DBExeQuery(xSQL)

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

    ''' <summary>
    ''' Get report print out from Process ID.
    ''' </summary>
    ''' <param name="Process_Id"></param>
    ''' <remarks></remarks>
    Private Sub getReportName(ByVal Process_Id As Integer)

        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
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

    ''' <summary>
    ''' Get payment type master.
    ''' </summary>
    ''' <remarks></remarks>
    Sub getPaymentType()
        'Dim objClassDB As New tb_Invoice_PaymentType(ms_Package.enuOperation_Type.SEARCH)
        Dim oml_SO_Invoice As New ml_SO_Invoice()
        Dim odtml_SO_Invoice As DataTable = New DataTable

        Try
            oml_SO_Invoice.GetAllAsDataTable()
            odtml_SO_Invoice = oml_SO_Invoice.DataTable
            cboConditionPay.BeginUpdate()

            With cboConditionPay
                .DisplayMember = "Description"
                .ValueMember = "PaymentType_Index"
                .DataSource = odtml_SO_Invoice
            End With
            cboConditionPay.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            oml_SO_Invoice = Nothing
            odtml_SO_Invoice = Nothing
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



            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub SetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try

            _USE_PRODUCT_CUSTOMER = objCustomSetting.getConfig_Key_USE("USE_PRODUCT_CUSTOMER")
            'Me._Customer_Index = objCustomSetting.getConfig_Key_DEFUALT("DEFAULT_CUSTOMER_INDEX")

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            Me._Customer_Index = oUser.GetUserByCustomerDefault()
            If Me._Customer_Index <> "" Then Me.getCustomer()

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
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
    Private Sub getDistributionCenter()
        Dim objms_DistributionCenter As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
        Dim objDTms_DistributionCenter As DataTable = New DataTable

        Try
            objms_DistributionCenter.GetAllAsDataTable("")
            objDTms_DistributionCenter = objms_DistributionCenter.DataTable

            cboDistributionCenter.DisplayMember = "Description"
            cboDistributionCenter.ValueMember = "DistributionCenter_Index"
            cboDistributionCenter.DataSource = objDTms_DistributionCenter

        Catch ex As Exception
            Throw ex
        Finally
            objms_DistributionCenter = Nothing
            objDTms_DistributionCenter = Nothing
        End Try

    End Sub
#End Region

#Region " BUTTON EVENTS "

    ''' <summary>
    ''' Button for Customer Popup. Use with private sub "getCustomer".
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearchCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCustomer.Click
        Try


            If _USE_PRODUCT_CUSTOMER Then
                For i As Integer = 0 To grdSOItem.Rows.Count - 2
                    If grdSOItem.Rows(i).Cells("Col_SKU_ID").Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(grdSOItem.Rows(i).Cells("Col_SKU_ID").Tag.ToString) Then
                        W_MSG_Information(String.Format("กรุณาลบรายการก่อนเปลี่ยน{0} !!", Me.lblSupplier.Text.Trim))
                        Exit Sub
                    End If
                Next
            End If

            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()

            Dim tmpCustomer_Index As String = ""

            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Index = "" Then
                Me._Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If

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
            'TODO: รอฟอร์ม
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

            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button for Consignee (Or Customer Shipping) Popup.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            Dim frm As New frmConsignee_Popup
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If
                frm.Customer_Index = Me._Customer_Index
            Else
                frm.Customer_Index = ""
            End If
            frm.ShowDialog()

            Dim tmp_Index As String = ""
            Consignee_Index = frm.Consignee_Index
            tmp_Index = frm.Consignee_Index

            If Not tmp_Index = "" Then
                Me.txtConsignee_Id.Tag = tmp_Index
                txtConsignee_Id.Text = frm.Consignee_ID
                txtConsignee_Name.Text = frm.Consignee_Name

                Dim objCusShip As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
                objCusShip.SelectByIndex(tmp_Index)
                Dim dtCusShip As New DataTable
                dtCusShip = objCusShip.GetDataTable

                If dtCusShip.Rows.Count > 0 Then

                    Me.txtTax_No.Text = dtCusShip.Rows(0).Item("Tax_No").ToString

                    If dtCusShip.Rows(0)("Route_Index").ToString() <> "" Then
                        If Not cboRoute.DataSource Is Nothing Then
                            Dim drr() As DataRow
                            Dim dt As New DataTable
                            dt = cboRoute.DataSource
                            drr = dt.Select(String.Format(" Route_Index ='{0}'", dtCusShip.Rows(0)("Route_Index").ToString()))
                            If drr.Length > 0 Then
                                cboRoute.SelectedValue = dtCusShip.Rows(0)("Route_Index").ToString()
                            End If


                        End If


                    End If
                    If dtCusShip.Rows(0)("SubRoute_Index").ToString() <> "" Then
                        If Not cboSubRoute.DataSource Is Nothing Then
                            Dim drr() As DataRow
                            Dim dt As New DataTable
                            dt = cboSubRoute.DataSource
                            drr = dt.Select(String.Format(" SubRoute_Index ='{0}'", dtCusShip.Rows(0)("SubRoute_Index").ToString()))
                            If drr.Length > 0 Then
                                cboSubRoute.SelectedValue = dtCusShip.Rows(0)("SubRoute_Index").ToString()
                            End If
                        End If


                    End If
                    'If dtCusShip.Rows(0)("TransportRegion_Index").ToString() <> "" Then
                    '    cboRegion.SelectedValue = dtCusShip.Rows(0)("TransportRegion_Index").ToString()
                    'End If
                End If

                Me.Get_CUSTOMERSHIPPINGLOCATION()

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

    ''' <summary>
    ''' Button for Customer Shipping Location Popup.
    ''' Use with private sub "getCus_Shipping_Location_Index"
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCustomer_Shipping_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Shipping_Location.Click
        'TODO: HARDCODE-MSG
        Try
            Dim strWhere As String = ""
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION") Then
                If Me.txtConsignee_Id.Text.Trim.Length = 0 Then
                    W_MSG_Information(GetMessage_Data("400041") & lblConsignee.Text & GetMessage_Data("400042"))
                    Exit Sub
                    strWhere = " AND Customer_Shipping_Index  = '" & Consignee_Index & "' " ' Str1 = '" & txtConsignee_Id.Text & "'"
                End If
                strWhere = " AND Customer_Shipping_Index  = '" & Consignee_Index & "' " ' Str1 = '" & txtConsignee_Id.Text & "'"
            Else
                strWhere = ""
            End If

            Dim frm As New frmCus_Ship_Location_Popup
            frm.strAddStrWhere = strWhere
            'frm.Customer_Shipping_Index = Consignee_Index
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
                Me.txtShip_Address.Text = ""
                Me.txtShip_Phone.Text = ""
                Me.txtShip_Fax.Text = ""
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
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim oconfig_Report As New config_Report
        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

        Try
            If Report_Name = "RPTSALEORDER_REQ" Then

                Dim oSalesReq As New clsSO
                Dim dsSalesReq As New DataSet
                Dim strWhere As String = ""

                strWhere &= " and SO.SalesOrder_Index IN (" & Me.SalesOrder_Index & ")"

                'Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
                'strWhere &= oUser.GetUserByCustomer()
                'strWhere = strWhere.Replace("Customer_Index", "SO.Customer_Index")

                strWhere &= New clsUserByDC().GetDistributionCenterByUser()
                strWhere = strWhere.Replace("DistributionCenter_Index", "SO.DistributionCenter_Index")

                strWhere &= " AND SOI.RGB_Check = 2"
                'strWhere &= " AND DT.Document_Group_Name = '" & Me.Document_Group_Name & "'"
                dsSalesReq = oSalesReq.getReportSalsOrder_Req(strWhere, Me.SalesOrder_Index)


                If dsSalesReq.Tables(0).Rows.Count = 0 Then
                    W_MSG_Information("ไม่พบรายการสินค้าไม่พอ")
                    Exit Sub
                End If
                Dim oCrystal As New rptSaleOrder_Req
                Dim frmrpt As New frmReportViewerMain
                frmrpt.Report_Name = "RPTSALEORDER_REQ"
                dsSalesReq.DataSetName = "dsSaleOrder_Req"
                dsSalesReq.Tables(0).TableName = "SaleOrder_Req"
                oCrystal.SetDataSource(dsSalesReq)
                oCrystal.SetParameterValue("User", W_Module.WV_UserName)
                oCrystal.SetParameterValue("SalesOrder_No", Me.txtSO_No.Text)
                frmrpt.CrystalReportViewer1.ReportSource = oCrystal
                frmrpt.ShowDialog()
            Else
                Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And SalesOrder_Index ='" & Me.SalesOrder_Index & "'")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally

        End Try
    End Sub

    ''' <summary>
    ''' Button to save data in this screen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim strResult As String = ""

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            ' TODO: HARDCODE-MSG
            ' --- STEP 1: Data Validation for required fields
            'Recalculator
            Dim objDB As New DBType_SQLServer

            If Me._Copy_SalesOrder_Index <> "" Then
                If Me.Copy_Red Then
                    If W_MSG_Confirm("ระบบจะทำการลบรายการสินค้าไม่พอ ออกจากเอกสารเดิมยืนยันใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If

            End If

            If Not ValidateDataBeforeSave() Then
                Exit Sub
            Else
                '   --- STEP 2: Do the actual data saving.
                strResult = Me.DoSaveDocument()

                If Me.txtInterface_No.Text.Trim = "" Then
                    objDB.DBExeNonQuery(String.Format("update tb_SalesOrder set Interface_No = '{0}' WHERE SalesOrder_Index = '{1}'", Me.txtSO_No.Text, strResult))
                End If
                ' --- STEP 3: Manage result messages and buttons control.
                If strResult <> "" Then
                    'Dim dtHDelete As New DataTable
                    'Dim dtDDelete As New DataTable
                    'If Me._Copy_SalesOrder_Index <> "" And Me.Copy_Red = True And Me.Copy_All = False Then
                    If Me._Copy_SalesOrder_Index <> "" Then
                        'update value header ,new order, no red
                        'Header
                        '********************************************************************************************************************************************
                        Dim xsql As String = ""
                        Dim dtHDOld As New DataTable
                        dtHDOld = objDB.DBExeQuery(String.Format(" select * from tb_SalesOrder where SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index))
                        For Each drDel As DataRow In dtHDOld.Rows
                            xsql = " update tb_SalesOrder "
                            xsql &= String.Format(" set SaleCode = '{0}' ", drDel("SaleCode").ToString)
                            xsql &= String.Format("     , SaleName = '{0}' ", drDel("SaleName").ToString)
                            xsql &= String.Format("     , Interface_No = '{0}' ", drDel("Interface_No").ToString)
                            xsql &= String.Format("     , SaleType_Id  = '{0}' ", drDel("SaleType_Id").ToString)
                            xsql &= String.Format("     , SaleType = '{0}' ", drDel("SaleType").ToString)
                            xsql &= String.Format("     , SalesAreaCode  = '{0}' ", drDel("SalesAreaCode").ToString)
                            xsql &= String.Format("     , SalesUnit  = '{0}' ", drDel("SalesUnit").ToString)
                            xsql &= String.Format("     , Shipby_Id  = '{0}' ", drDel("Shipby_Id").ToString)
                            xsql &= String.Format("     , PO_NO  = '{0}' ", drDel("PO_NO").ToString)
                            'xsql &= String.Format("     , Discount_Percent  = '{0}' ", drDel("Discount_Percent").ToString)
                            'xsql &= String.Format("     , Deposit_Amt  = '{0}' ", drDel("Deposit_Amt").ToString)
                            'xsql &= String.Format("     , Net_Amt  = '{0}' ", drDel("Net_Amt").ToString)
                            xsql &= String.Format("     , RGB_Check = '{0}' ", 0)
                            xsql &= String.Format(" where SalesOrder_Index = '{0}' ", Me._SalesOrder_Index)
                            objDB.DBExeNonQuery(xsql)
                        Next
                        'update item
                        xsql = String.Format(" select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                        xsql &= String.Format(" and Item_Seq in (select Item_Seq from tb_SalesOrderItem where SalesOrder_Index = '{0}' )", Me._SalesOrder_Index)
                        Dim dtDDOld As New DataTable
                        dtDDOld = objDB.DBExeQuery(xsql)
                        For Each drDel As DataRow In dtDDOld.Rows
                            xsql = " update tb_SalesOrderItem "
                            xsql &= String.Format(" set Discount_Rate = '{0}' ", drDel("Discount_Rate").ToString)
                            xsql &= String.Format("     , WOI_ID = '{0}' ", drDel("WOI_ID").ToString)
                            xsql &= String.Format(" where Item_Seq = '{0}' ", drDel("Item_Seq").ToString)
                            xsql &= String.Format("     and SalesOrder_Index = '{0}' ", Me._SalesOrder_Index)
                            objDB.DBExeNonQuery(xsql)
                            'End If
                        Next
                        '********************************************************************************************************************************************

                        'Recal old order
                        Dim dtDDNew As New DataTable
                        dtDDNew = objDB.DBExeQuery(String.Format(" select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' ", Me._SalesOrder_Index))
                        For Each drNew As DataRow In dtDDNew.Rows
                            'If Me.Copy_Red_NotStock = True Then
                            Dim xdrArrOld() As DataRow
                            xdrArrOld = dtDDOld.Select(String.Format("Item_Seq = '{0}'", drNew("Item_Seq").ToString))
                            If xdrArrOld.Length > 0 Then
                                xsql = " update tb_SalesOrderItem "
                                If (xdrArrOld(0)("Qty") > drNew("Qty")) Then
                                    Dim xQty As Decimal = xdrArrOld(0)("Qty") - drNew("Qty")
                                    Dim xTotal_Qty As Decimal = xQty * xdrArrOld(0)("Ratio")
                                    Dim xAmount As Decimal = (xdrArrOld(0)("Amount") / xdrArrOld(0)("Qty")) * xQty

                                    xsql &= String.Format(" SET Qty = {0} ", xQty)
                                    xsql &= String.Format("     , Total_Qty = {0} ", xTotal_Qty)
                                    xsql &= String.Format("     , Amount = {0}", xAmount)
                                Else
                                    xsql &= String.Format(" SET Qty = {0} ", 0)
                                    xsql &= String.Format("     , Total_Qty = {0} ", 0)
                                End If
                                xsql &= String.Format(" where Item_Seq = '{0}' ", drNew("Item_Seq").ToString)
                                xsql &= String.Format("     and SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                                objDB.DBExeNonQuery(xsql)
                            End If
                            'Else
                            'xsql = " update tb_SalesOrderItem "
                            'xsql &= String.Format(" SET Qty = {0} ", 0)
                            'xsql &= String.Format(" where Item_Seq = '{0}' ", drNew("Item_Seq").ToString)
                            'xsql &= String.Format("     and SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                            'objDB.DBExeNonQuery(xsql)
                            'End If


                        Next

                        'ลบรายการเก่า
                        'Loop delete from old order
                        xsql = String.Format(" select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' and Qty = 0", Me._Copy_SalesOrder_Index)
                        dtDDOld = objDB.DBExeQuery(xsql)
                        For Each drDel As DataRow In dtDDOld.Rows
                            objDB.DBExeNonQuery(String.Format(" sp_Delete_SalesOrderItem '{0}','{1}' ", drDel("SalesOrderItem_Index").ToString, WV_UserName))
                        Next

                        'คำนวณเงินใหม่
                        xsql = String.Format(" select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                        dtDDOld = objDB.DBExeQuery(xsql)
                        If dtDDOld.Rows.Count > 0 Then
                            Dim xTotal_Amt As Double = dtDDOld.Compute("SUM(Amount)", "")
                            Dim xDiscount_Amt As Double = (dtHDOld.Rows(0)("Discount_Amt") * xTotal_Amt) / dtHDOld.Rows(0)("Total_Amt")
                            If Double.IsNaN(xDiscount_Amt) Then xDiscount_Amt = 0
                            Dim xAmount As Double = xTotal_Amt - xDiscount_Amt
                            Dim xVAT As Double = xAmount * (7 / 100)
                            Dim xNet_Amt As Double = xAmount - xVAT

                            xsql = " update tb_SalesOrder "
                            xsql &= String.Format(" set Total_Amt = '{0}' ", xTotal_Amt)
                            xsql &= String.Format("     , Discount_Amt = '{0}' ", xDiscount_Amt)
                            xsql &= String.Format("     , Amount = '{0}' ", xAmount)
                            'xsql &= String.Format("     , Amount = '{0}' ", xTotal_Amt)
                            xsql &= String.Format("     , VAT = '{0}' ", xVAT)
                            xsql &= String.Format("     , Net_Amt = '{0}' ", xNet_Amt)
                            xsql &= String.Format(" where SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                            objDB.DBExeNonQuery(xsql)
                        End If



                        'Reset color old
                        Dim objSOTransaction As New cls_KSL
                        Dim dtStock As New DataTable
                        dtStock = objSOTransaction.getCheckStock_Sales(Me._Copy_SalesOrder_Index, Me._Customer_Index, Me.cboDistributionCenter.SelectedValue.ToString)
                        If dtStock.Rows.Count > 0 Then
                            objSOTransaction.ResetColor_Sales(dtStock, Me._Copy_SalesOrder_Index)
                        End If
                    End If






                    Me.btnSave.Enabled = False
                    Me.btnDelete.Enabled = False
                    W_MSG_Information_ByIndex("1")
                Else
                    W_MSG_Information_ByIndex("2")
                    Exit Sub
                End If

                ' Note that is the case of adding new document only.
                Me.getSalesOrderNoFromIndex(strResult)

                ' Manage buttons again
                btnPrint.Enabled = True
                cboPrint.Enabled = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    ''' <summary>
    ''' This function is to delete SO item.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        ' ====== TODO: HARDCODE-MSG 
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If grdSOItem.Rows.Count <= 0 Then Exit Sub

            If Not Me.grdSOItem.CurrentRow.Cells("Col_SOItem_Index").Value = "" Then
                Select Case Status

                    Case 1, 2, 6
                        ' 1 = รอยืนยัน, 2 = รอเบิก, 6 = ค้างจ่าย
                        Dim objDB As New SOTransaction(SOTransaction.enuOperation_Type.DELETE)

                        If W_MSG_Confirm(GetMessage_Data("5")) = Windows.Forms.DialogResult.Yes Then
                            Dim xSalesOrderItem_Index As String = Me.grdSOItem.CurrentRow.Cells("Col_SOItem_Index").Value
                            'KSL : Fix bug reset staus
                            Dim objcon As New DBType_SQLServer
                            Dim dt As New DataTable
                            dt = objcon.DBExeQuery(String.Format("select * from tb_SalesOrderItem where SalesOrderItem_Index= '{0}'", xSalesOrderItem_Index))
                            objcon = Nothing
                            If dt.Rows(0)("Qty_Withdraw") > 0 Then
                                W_MSG_Error("มีการเบิกสินค้าอยู่ไม่สามารถลบได้")
                                Exit Sub
                            End If

                            If objDB.Delete_SalesOrderItem(Me.grdSOItem.CurrentRow.Cells("Col_SOItem_Index").Value) = True Then
                                Me.grdSOItem.Rows.RemoveAt(grdSOItem.CurrentRow.Index)
                            End If
                            objDB = Nothing
                        End If
                    Case Else
                        ' Do nothing.
                        ' Button control should already taking care of this, 
                        ' but we check here anyway just in case!
                End Select

            Else
                ' ------ Index = "". This should be the case of adding new SO.
                Me.grdSOItem.Rows.RemoveAt(grdSOItem.CurrentRow.Index)
            End If

            Me.CalSubtotalAmount()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

#Region " TEXTBOX & CHECKBOX EVENTS "

    '-------------------------------------------------------------------
    '------ Most of the events are for "Net Amount Calculation". -------
    '-------------------------------------------------------------------
    Private Sub txtDiscount_Percent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscount_Percent.TextChanged
        Try

            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If IsNumeric(txtDiscount_Percent.Text) = False Then
                txtDiscount_Percent.Text = "0.00"
            Else
                'txtDiscount_Percent.Text = CDbl(txtDiscount_Percent.Text).ToString("#,##0.00")
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_Percent_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVAT_Percent.TextChanged
        Try

            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If IsNumeric(txtVAT_Percent.Text) = False Then
                txtVAT_Percent.Text = "0.00"
            Else
                'txtVAT_Percent.Text = CDbl(txtVAT_Percent.Text).ToString("#,##0.00")
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDeposit_Amt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDeposit_Amt.TextChanged
        Try

            '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
            If IsNumeric(txtDeposit_Amt.Text) = False Then
                txtDeposit_Amt.Text = "0.00"
            Else
                'txtDeposit_Amt.Text = CDbl(txtDeposit_Amt.Text).ToString("#,##0.00")
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkVAT_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkVAT.CheckedChanged
        Try
            If chkVAT.Checked = True Then
                txtVAT_Percent.Enabled = True
                SetDEFAULT_VAT()
            Else
                txtVAT_Percent.Enabled = False
                txtVAT.Text = "0.00"
                txtVAT_Percent.Text = "0.00"
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDiscount_Amt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDiscount_Amt.TextChanged
        Try
            If IsNumeric(Me.txtDiscount_Amt.Text) = False Then
                txtDiscount_Amt.Text = "0.00"
            Else
                'txtDiscount_Amt.Text = CDbl(txtDiscount_Amt.Text).ToString("#,##0.00")
            End If

            CalNetAmount()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkDiscount_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDiscount.CheckedChanged

        Try
            If chkDiscount.Checked = True Then
                txtDiscount_Percent.Enabled = True
            Else
                txtDiscount_Percent.Enabled = False
                txtDiscount_Percent.Text = "0.00"
                txtDiscount_Amt.Text = "0.00"
            End If

            CalNetAmount()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtSubtotal_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSubtotal.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtSubtotal, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtDiscount_Percent_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscount_Percent.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtDiscount_Percent, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtDiscount_Amt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDiscount_Amt.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtDiscount_Amt, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_Percent_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVAT_Percent.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVAT_Percent, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVAT_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVAT.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVAT, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtDeposit_Amt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDeposit_Amt.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtDeposit_Amt, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtNet_Amt_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNet_Amt.KeyPress
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
                txtExRate.Text = objDT.Rows(0).Item("ExRate").ToString
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub txtExRate_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtExRate.TextChanged
        txtExRate.Text = Format(CDbl(txtExRate.Text.ToString), "#,##0.00")
    End Sub

#End Region

#Region " SELECT MASTER FUNCTIONS AND SUBS "

    ''' <summary>
    ''' Get Customer information returning from Popup
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            objms_Customer.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me._Customer_Index = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString
                txtCustomer_Address.Text = objDTms_Customer.Rows(0).Item("Address").ToString
                txtCustomer_Phone.Text = objDTms_Customer.Rows(0).Item("Tel").ToString
                txtCustomer_Fax.Text = objDTms_Customer.Rows(0).Item("Fax").ToString

            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
                txtCustomer_Address.Text = ""
                txtCustomer_Phone.Text = ""
                txtCustomer_Fax.Text = ""

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
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
            objms_Supplier.getPopup_Supplier("Supplier_Index", Me._Customer_Index.ToString)
            objDTms_Supplier = objms_Supplier.DataTable
            If objDTms_Supplier.Rows.Count > 0 Then
                Me._Customer_Index = objDTms_Supplier.Rows(0).Item("Supplier_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Supplier.Rows(0).Item("Supplier_Id").ToString
                Me.txtCustomer_Name.Text = objDTms_Supplier.Rows(0).Item("Supplier_Name").ToString
                Me.txtCustomer_Address.Text = objDTms_Supplier.Rows(0).Item("Address").ToString
                Me.txtCustomer_Phone.Text = objDTms_Supplier.Rows(0).Item("tel").ToString
                Me.txtCustomer_Fax.Text = objDTms_Supplier.Rows(0).Item("fax").ToString
                Me.txtTax_No.Text = objDTms_Supplier.Rows(0).Item("Str1").ToString
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
                Me.txtCustomer_Address.Text = ""
                Me.txtCustomer_Phone.Text = ""
                Me.txtCustomer_Fax.Text = ""
                Me.txtTax_No.Text = ""
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


    Private Sub getCustomer_Shipping()
        Dim objms_Customer As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            objms_Customer.getPopup_CustomerShip("Customer_Shipping_Index", Me.txtConsignee_Id.Tag.ToString)
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me.txtConsignee_Id.Tag = objDTms_Customer.Rows(0).Item("Customer_Shipping_Index").ToString
                Me.txtConsignee_Id.Text = objDTms_Customer.Rows(0).Item("Str1").ToString
                Me.txtConsignee_Name.Text = objDTms_Customer.Rows(0).Item("Company_Name").ToString
            Else
                Me.txtConsignee_Id.Tag = ""
                Me.txtConsignee_Id.Text = ""
                Me.txtConsignee_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get Customer Shipping Location information returning from Popup
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Todd: 16 Jan 2010 - Fix bug in Customer Shipping Location.
    ''' </remarks>
    Private Sub getCus_Shipping_Location_Index()
        Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDTms_Shipping_Location As DataTable = New DataTable
        Dim _Postcode As String = ""
        Try
            objms_Shipping_Location.getCus_Ship_Locartion_Search("Customer_Shipping_Location_Index", Me.txtShipping_Location_ID.Tag.ToString)
            objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
            If objDTms_Shipping_Location.Rows.Count > 0 Then

                Me.txtShipping_Location_ID.Tag = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Customer_Shipping_Location_Id").ToString
                Me.txtShipping_Location_Name.Text = objDTms_Shipping_Location.Rows(0).Item("Shipping_Location_Name").ToString
                'Me.txtShip_Address.Text = objDTms_Shipping_Location.Rows(0).Item("Address").ToString
                '(Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Shipping_Location_Name").ToString)
                Me.txtShip_Address.Text = objDTms_Shipping_Location.Rows(0).Item("xAddressShipping_Location").ToString ' objDTms_Shipping_Location.Rows(0).Item("AddressShipping_Location").ToString
                Me.txtShip_Phone.Text = objDTms_Shipping_Location.Rows(0).Item("Tel").ToString
                Me.txtShip_Fax.Text = objDTms_Shipping_Location.Rows(0).Item("Fax").ToString
                '_Postcode = objDTms_Shipping_Location.Rows(0).Item("Postcode").ToString
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

                'cboRoute.SelectedValue = objDTms_Shipping_Location.Rows(0).Item("Route_Index").ToString
                'cboSubRoute.SelectedValue = objDTms_Shipping_Location.Rows(0).Item("SubRoute_Index").ToString

                'dong : add for KSL

                With objDTms_Shipping_Location.Rows(0)
                    cboRoute.SelectedValue = IIf(.Item("Route_Index").ToString = "", "0010000000000", .Item("Route_Index").ToString)
                    cboSubRoute.SelectedValue = IIf(.Item("SubRoute_Index").ToString = "", "0010000000000", .Item("SubRoute_Index").ToString)
                    cboDistributionCenter.SelectedValue = IIf(.Item("DistributionCenter_Index").ToString = "", "0010000000000", objDTms_Shipping_Location.Rows(0).Item("DistributionCenter_Index").ToString)
                    cboRegion.SelectedValue = IIf(.Item("TransportRegion_Index").ToString = "", "0010000000000", .Item("TransportRegion_Index").ToString)

                End With


            Else

                Me.txtShipping_Location_ID.Tag = ""
                Me.txtShipping_Location_ID.Text = ""
                Me.txtShip_Address.Text = ""
                Me.txtShip_Phone.Text = ""
                Me.txtShip_Fax.Text = ""
                _Postcode = ""
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
                    dblDiscount = (CDbl(txtDiscount_Percent.Text.ToString) * dblSubtotal) / 100
                    txtDiscount_Amt.Text = dblDiscount.ToString("#,##0.00")
                End If
            Else
                If Not txtDiscount_Amt.Text = "" And Not txtDiscount_Amt.Text = "0" Then
                    dblDiscount = (CDbl(txtDiscount_Amt.Text.ToString))
                End If
            End If

            If (Not txtVAT_Percent.Text = "") And (Not txtVAT_Percent.Text = "0") Then
                dblVat = (CDbl(txtVAT_Percent.Text.ToString) * (dblSubtotal - dblDiscount)) / 100
            End If

            If (Not txtDeposit_Amt.Text = "") And (Not txtDeposit_Amt.Text = "0") Then
                dblDeposit = CDbl(txtDeposit_Amt.Text.ToString)
            End If

            txtVAT.Text = Format(dblVat, "#,##0.00")

            If IsNumeric(txtDiscount_Percent.Text) = False Then
                txtDiscount_Percent.Text = "0.00"
            Else
                txtDiscount_Percent.Text = Format(CDbl(txtDiscount_Percent.Text.ToString), "#,##0.00")
            End If

            If IsNumeric(txtDeposit_Amt.Text) = False Then
                txtDeposit_Amt.Text = "0.00"
            Else
                txtDeposit_Amt.Text = Format(CDbl(txtDeposit_Amt.Text.ToString), "#,##0.00")
            End If

            If IsNumeric(txtVAT_Percent.Text) = False Then
                txtVAT_Percent.Text = "0.00"
            Else
                txtVAT_Percent.Text = Format(CDbl(txtVAT_Percent.Text.ToString), "#,##0.00")
            End If

            'txtNet_Amt.Text = Format(CDbl(dblSubtotal - dblDiscount - dblVat - dblDeposit), "#,##0.00")
            Me.txtTotalAmount.Text = Format(CDbl(dblSubtotal - dblDiscount), "#,##0.00")
            Me.txtNet_Amt.Text = Format(CDbl(dblSubtotal - dblDiscount + dblVat - dblDeposit), "#,##0.00")

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' This sub is to calculate subtotal amount.
    ''' It takes sum values of both PO Item and PO Other datagrid and sum them together.
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Todd 16 Jan 2010 - Clean Code and Modify Logic
    ''' </remarks>
    Private Sub CalSubtotalAmount()
        Try
            Dim dblSum As Double = 0
            Dim dblTempQty As Double = 0
            Dim i As Integer
            Dim dblSum_Discount As Double = 0
            For i = 0 To grdSOItem.Rows.Count - 1
                If IsNumeric(grdSOItem.Rows(i).Cells("Col_Amount").Value) Then
                    dblTempQty = grdSOItem.Rows(i).Cells("Col_Amount").Value
                    dblSum += CDbl(dblTempQty)
                End If
                If IsNumeric(grdSOItem.Rows(i).Cells("Col_Discount").Value) Then
                    dblTempQty = grdSOItem.Rows(i).Cells("Col_Discount").Value
                    dblSum_Discount += CDbl(dblTempQty)
                End If
            Next
            If Me._Copy_SalesOrder_Index <> "" And Me.Copy_Red = True Then
                Dim xobjDB As New DBType_SQLServer
                Dim xdtOld As New DataTable
                Dim xsql As String = ""
                xsql &= String.Format(" select * from tb_SalesOrder where SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                xdtOld = xobjDB.DBExeQuery(xsql)
                If xdtOld.Rows.Count > 0 Then
                    Dim xdr As DataRow = xdtOld.Rows(0)
                    dblSum_Discount = (xdr("Discount_Amt") * dblSum) / xdr("Amount")
                End If

            End If



            Me.txtDiscount_Amt.Text = Format(dblSum_Discount, "#,##0.00")
            Me.txtSubtotal.Text = Format(dblSum, "#,##0.00")

            Me.CalNetAmount()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " DATAGRID & TAB EVENTS "

    ''' <summary>
    ''' Control the switch of tab page.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub tbcSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tbcSO.Click
        Dim tbcSelectedIndex As String = tbcSO.SelectedIndex
        If tbcSelectedIndex = 1 Then
            ShowRemainSOItemQty(SalesOrder_Index)
        End If
    End Sub

    ''' <summary>
    ''' Event handler for grdSOItem cell click.
    ''' This event is used for handle SKU popup when click the button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd - 8 Jan 2010 - Clean code / change column name
    ''' </remarks>
    Private Sub grdSOItem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOItem.CellClick
        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "Col_Btn_GetSKU"

                    If Me._USE_PRODUCT_CUSTOMER AndAlso String.IsNullOrEmpty(Me._Customer_Index) Then
                        W_MSG_Information(String.Format("กรุณาระบุ {0} !!", Me.lblSupplier.Text.Trim))
                        Exit Select
                    End If


                    If grdSOItem.Rows(e.RowIndex).Cells("Col_Btn_GetSKU").Tag <> "NULL" Then
                        Dim frmPopup As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me._Customer_Index)


                        frmPopup.ShowDialog()
                        If frmPopup.Sku_ID <> "" Then
                            Me.grdSOItem.Rows(e.RowIndex).Cells("Col_SKU_ID").Value = frmPopup.Sku_ID
                        End If
                        frmPopup.Close()
                    End If
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Event handler for grdSOItem value change.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd - 8 Jan 2010 - Clean code and modify logic
    ''' </remarks>
    Private Sub grdSOItem_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOItem.CellValueChanged
        If e.RowIndex <= -1 Then Exit Sub
        Dim strCurrentSKUID As String = ""
        Dim dblSum As Decimal = 0
        Dim dblQty As Decimal = 0
        Dim dblUnitPrice As Decimal = 0
        Dim dblDiscount As Decimal = 0

        ' ------ First we check if this variable gblnDataGridClick is set.
        ' ------ Normally it is set to FALSE during form load to avoid unnecessary calculation.
        If Not gblnDataGridClick Then
            Exit Sub
        End If

        Try
            With grdSOItem
                Select Case .Columns(e.ColumnIndex).Name

                    ' ------ Case SKU ID value change, get SKU information to display in current row.
                    Case "Col_SKU_ID"

                        If Me._USE_PRODUCT_CUSTOMER AndAlso String.IsNullOrEmpty(Me._Customer_Index) Then
                            W_MSG_Information(String.Format("กรุณาระบุ {0} !!", Me.lblSupplier.Text.Trim))
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
                            dblDiscount = Format(0, "#,##0.000000")
                        End If

                        If Me._Copy_SalesOrder_Index <> "" And Me.Copy_Red = True Then
                            Dim xobjDB As New DBType_SQLServer
                            Dim xdtOld As New DataTable
                            Dim xsql As String = ""
                            xsql &= String.Format(" select * from tb_SalesOrderItem where SalesOrder_Index = '{0}' ", Me._Copy_SalesOrder_Index)
                            xsql &= String.Format(" and Item_Seq = '{0}' ", .Rows(e.RowIndex).Cells("Col_Seq").Value)
                            xdtOld = xobjDB.DBExeQuery(xsql)
                            If xdtOld.Rows.Count > 0 Then
                                Dim xdr As DataRow = xdtOld.Rows(0)
                                '.Rows(e.RowIndex).Cells("Col_Unit_Price").Value = xdr("Amount") / xdr("Qty")
                                .Rows(e.RowIndex).Cells("Col_Amount").Value = (xdr("Amount") / xdr("Qty")) * dblQty
                            End If

                        Else
                            dblSum = (dblQty * dblUnitPrice) - dblDiscount
                            .Rows(e.RowIndex).Cells("Col_Amount").Value = Format(dblSum, "#,##0.000000")
                        End If


                        ' ------ Case Amount change, recalculate Subtotal.
                    Case "Col_Amount"
                        CalSubtotalAmount()


                End Select
                If (.Columns(e.ColumnIndex).Name = "Col_Qty") Then
                    Me.setWeightAndVolume(e.RowIndex)
                End If
            End With
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub setWeightAndVolume(ByVal rowIndex As Integer)
        Try
            With grdSOItem
                Dim _dgvcob As DataGridViewComboBoxCell
                _dgvcob = .Rows(rowIndex).Cells("Col_UOM")
                .Rows(rowIndex).Cells("Col_UOM").Tag = .Rows(rowIndex).Cells("Col_UOM").Value
                Dim _dt As DataTable = DirectCast(_dgvcob.DataSource, DataTable)
                Dim _dr() As DataRow = _dt.Select(String.Format(" Sku_Index='{0}' and Package_Index='{1}' ", .Rows(rowIndex).Cells("Col_SKU_ID").Tag, .Rows(rowIndex).Cells("Col_UOM").Tag))
                If (_dr.Length > 0) Then
                    Dim _dblQty As Decimal = 0
                    If IsNumeric(.Rows(rowIndex).Cells("Col_Qty").Value) Then
                        _dblQty = .Rows(rowIndex).Cells("Col_Qty").Value
                    End If
                    Dim _unit_Weight As Decimal = 0
                    If IsNumeric(_dr(0).Item("Unit_Weight")) Then
                        _unit_Weight = _dr(0).Item("Unit_Weight")
                    End If
                    Dim _unit_Volume As Decimal = 0
                    If IsNumeric(_dr(0).Item("Unit_Volume")) Then
                        _unit_Volume = _dr(0).Item("Unit_Volume")
                    End If
                    .Rows(rowIndex).Cells("Col_Weight").Value = _dblQty * _unit_Weight
                    .Rows(rowIndex).Cells("Col_Volume").Value = _dblQty * _unit_Volume
                Else
                    .Rows(rowIndex).Cells("Col_Weight").Value = 0
                    .Rows(rowIndex).Cells("Col_Volume").Value = 0
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOItem_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOItem.CellEndEdit
        Try
            If grdSOItem.RowCount < 0 Then
                Exit Sub
            End If

            Select Case e.ColumnIndex
                Case grdSOItem.CurrentRow.Cells("Col_Unit_Price").ColumnIndex
                    'If grdSOItem.CurrentRow.Cells("Col_Unit_Price").Value Is Nothing Then
                    '    grdSOItem.CurrentRow.Cells("Col_Unit_Price").Value = "0.00"
                    'Else
                    '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                    If IsNumeric(grdSOItem.CurrentRow.Cells("Col_Unit_Price").Value) = False Then
                        grdSOItem.CurrentRow.Cells("Col_Unit_Price").Value = "0.000000"
                        Exit Sub
                    End If
                    grdSOItem.CurrentRow.Cells("Col_Unit_Price").Value = CDbl(grdSOItem.CurrentRow.Cells("Col_Unit_Price").Value)
                    'End If

                Case grdSOItem.CurrentRow.Cells("Col_Qty").ColumnIndex
                    'If grdSOItem.CurrentRow.Cells("Col_Qty").Value.ToString Is Nothing Then
                    '    grdSOItem.CurrentRow.Cells("Col_Qty").Value = "0.00"
                    'Else
                    '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                    If IsNumeric(grdSOItem.CurrentRow.Cells("Col_Qty").Value) = False Then
                        grdSOItem.CurrentRow.Cells("Col_Qty").Value = "0.000000"
                        Exit Sub
                    End If
                    grdSOItem.CurrentRow.Cells("Col_Qty").Value = CDbl(grdSOItem.CurrentRow.Cells("Col_Qty").Value)
                    'End If
                Case grdSOItem.CurrentRow.Cells("Col_Amount").ColumnIndex
                    'If grdSOItem.CurrentRow.Cells("Col_Amount").Value.ToString Is Nothing Then
                    '    grdSOItem.CurrentRow.Cells("Col_Amount").Value = "0.00"
                    'Else
                    '22/01/2010 TaTa  เพิ่ม Function การ Check ข้อมูลตัวเลขที่เป็นช่องว่าง 
                    If IsNumeric(grdSOItem.CurrentRow.Cells("Col_Amount").Value) = False Then
                        grdSOItem.CurrentRow.Cells("Col_Amount").Value = "0.000000"
                        Exit Sub
                    End If
                    grdSOItem.CurrentRow.Cells("Col_Amount").Value = CDbl(grdSOItem.CurrentRow.Cells("Col_Amount").Value)
                    'End If
                Case grdSOItem.CurrentRow.Cells("Col_Discount").ColumnIndex
                    If IsNumeric(grdSOItem.CurrentRow.Cells("Col_Discount").Value) = False Then
                        grdSOItem.CurrentRow.Cells("Col_Discount").Value = "0.000000"
                        Exit Sub
                    End If
                    grdSOItem.CurrentRow.Cells("Col_Discount").Value = CDbl(grdSOItem.CurrentRow.Cells("Col_Discount").Value)
                Case grdSOItem.CurrentRow.Cells("Col_UOM").ColumnIndex
                    Me.setWeightAndVolume(e.RowIndex)
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

#Region " GENERIC FUNCTIONS AND SUBS "



    Private Sub getSOHeader(ByVal SalesOrder_Index As String)

        Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.SEARCH)
        Dim objDTSOTransaction As DataTable = New DataTable

        Try
            objSOTransaction.getSOHeader(SalesOrder_Index)
            objDTSOTransaction = objSOTransaction.DataTable

            If objDTSOTransaction.Rows.Count > 0 Then
                txtSO_No.Text = objDTSOTransaction.Rows(0).Item("SalesOrder_No").ToString
                dtpSO_Date.Value = objDTSOTransaction.Rows(0).Item("SalesOrder_Date")
                'dtpSO_Date.Value = objDTSOTransaction.Rows(0).Item("SalesOrder_Date").ToShortDateString

                txtUser.Text = objDTSOTransaction.Rows(0).Item("add_by").ToString

                txtCustomer_Id.Text = objDTSOTransaction.Rows(0).Item("Customer_Id").ToString
                txtCustomer_Id.Tag = objDTSOTransaction.Rows(0).Item("Customer_Index").ToString
                Me._Customer_Index = objDTSOTransaction.Rows(0).Item("Customer_Index").ToString
                txtCustomer_Name.Text = objDTSOTransaction.Rows(0).Item("Customer_Name").ToString

                ' Todd: 16 Jan 2010 - There is no actual fields to store 
                ' customer addr., tel, fax in "tb_SalesOrder". So we store customer addr. in Str fields.
                ' Now old database before we make this change, Str10, Str6, Str7 may not have values,
                ' so to solve this problem, we will load data from customer master table 
                'if these str fields are empty.

                'txtCustomer_Address.Text = objDTSOTransaction.Rows(0).Item("Address").ToString
                'txtCustomer_Phone.Text = objDTSOTransaction.Rows(0).Item("Tel").ToString
                'txtCustomer_Fax.Text = objDTSOTransaction.Rows(0).Item("Fax").ToString
                If IsNothing(objDTSOTransaction.Rows(0).Item("Str10")) Then
                    txtCustomer_Address.Text = objDTSOTransaction.Rows(0).Item("Address").ToString
                ElseIf objDTSOTransaction.Rows(0).Item("Str10").ToString = "" Then
                    txtCustomer_Address.Text = objDTSOTransaction.Rows(0).Item("Address").ToString
                Else
                    txtCustomer_Address.Text = objDTSOTransaction.Rows(0).Item("Str10").ToString
                End If
                If IsNothing(objDTSOTransaction.Rows(0).Item("Str6")) Then
                    txtCustomer_Phone.Text = objDTSOTransaction.Rows(0).Item("Tel").ToString
                ElseIf objDTSOTransaction.Rows(0).Item("Str6").ToString = "" Then
                    txtCustomer_Phone.Text = objDTSOTransaction.Rows(0).Item("Tel").ToString
                Else
                    txtCustomer_Phone.Text = objDTSOTransaction.Rows(0).Item("Str6").ToString
                End If
                If IsNothing(objDTSOTransaction.Rows(0).Item("Str7")) Then
                    txtCustomer_Fax.Text = objDTSOTransaction.Rows(0).Item("Fax").ToString
                ElseIf objDTSOTransaction.Rows(0).Item("Str7").ToString = "" Then
                    txtCustomer_Fax.Text = objDTSOTransaction.Rows(0).Item("Fax").ToString
                Else
                    txtCustomer_Fax.Text = objDTSOTransaction.Rows(0).Item("Str7").ToString
                End If

                txtRef1.Text = objDTSOTransaction.Rows(0).Item("Str1").ToString
                txtRef2.Text = objDTSOTransaction.Rows(0).Item("Str2").ToString
                txtCreditTerm.Text = objDTSOTransaction.Rows(0).Item("Credit_Term").ToString
                txtRemark.Text = objDTSOTransaction.Rows(0).Item("Remark").ToString
                txtExRate.Text = objDTSOTransaction.Rows(0).Item("Exchange_Rate").ToString
                If Not String.IsNullOrEmpty(objDTSOTransaction.Rows(0).Item("Expected_Delivery_Date").ToString) Then
                    dtpDue_Date.Value = objDTSOTransaction.Rows(0).Item("Expected_Delivery_Date").ToString
                End If

                cboCurrency.SelectedValue = objDTSOTransaction.Rows(0).Item("Currency_Index").ToString
                cboConditionPay.SelectedValue = objDTSOTransaction.Rows(0).Item("PaymentMethod_Index").ToString
                txtCarrier_ID.Tag = objDTSOTransaction.Rows(0).Item("Carrier_Index").ToString
                txtCarrier_ID.Text = objDTSOTransaction.Rows(0).Item("Carrier_Id").ToString
                txtCarrier_Name.Text = objDTSOTransaction.Rows(0).Item("DesCar").ToString

                txtConsignee_Id.Tag = objDTSOTransaction.Rows(0).Item("Customer_Shipping_Index").ToString
                txtConsignee_Id.Text = objDTSOTransaction.Rows(0).Item("Company_ID").ToString
                txtConsignee_Name.Text = objDTSOTransaction.Rows(0).Item("Company_Name").ToString

                txtShipping_Location_ID.Tag = objDTSOTransaction.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                'txtShipping_Location_ID.Text = objDTSOTransaction.Rows(0).Item("Company_Name").ToString ' Wrong
                'txtShip_Address.Text = objDTSOTransaction.Rows(0).Item("Address_Shipping_Location").ToString
                'txtShip_Phone.Text = objDTSOTransaction.Rows(0).Item("Tel_Shipping_Location").ToString
                'txtShip_Fax.Text = objDTSOTransaction.Rows(0).Item("Fax_Shipping_Location").ToString
                txtShipping_Location_ID.Text = objDTSOTransaction.Rows(0).Item("Customer_Shipping_Location_Id").ToString
                txtShipping_Location_Name.Text = objDTSOTransaction.Rows(0).Item("Shipping_Location_Name").ToString 'objDTSOTransaction.Rows(0).Item("Company_Name").ToString
                txtShip_Address.Text = objDTSOTransaction.Rows(0).Item("Str9").ToString
                txtShip_Phone.Text = objDTSOTransaction.Rows(0).Item("Str4").ToString
                txtShip_Fax.Text = objDTSOTransaction.Rows(0).Item("Str5").ToString

                'If objDTSOTransaction.Rows(0).Item("Amount").ToString = "" Then
                '    txtSubtotal.Text = 0
                'Else
                '    txtSubtotal.Text = CDbl(objDTSOTransaction.Rows(0).Item("Amount").ToString).ToString("#,##0.00")
                'End If

                If objDTSOTransaction.Rows(0).Item("Total_Amt").ToString = "" Then
                    txtSubtotal.Text = 0
                Else
                    txtSubtotal.Text = CDbl(objDTSOTransaction.Rows(0).Item("Total_Amt").ToString).ToString("#,##0.00")
                End If


                If objDTSOTransaction.Rows(0).Item("Discount_Percent").ToString = "" Then
                    txtDiscount_Percent.Text = 0
                Else
                    txtDiscount_Percent.Text = objDTSOTransaction.Rows(0).Item("Discount_Percent").ToString
                End If

                If objDTSOTransaction.Rows(0).Item("Discount_Amt").ToString = "" Then
                    txtDiscount_Amt.Text = 0
                Else
                    txtDiscount_Amt.Text = CDbl(objDTSOTransaction.Rows(0).Item("Discount_Amt").ToString).ToString("#,##0.00")
                End If

                If objDTSOTransaction.Rows(0).Item("VAT_Percent").ToString = "" Then
                    txtVAT_Percent.Text = 0
                Else
                    txtVAT_Percent.Text = objDTSOTransaction.Rows(0).Item("VAT_Percent").ToString
                End If

                txtStatus.Text = objDTSOTransaction.Rows(0).Item("DesPro").ToString
                txtUser.Text = objDTSOTransaction.Rows(0).Item("add_by").ToString

                If objDTSOTransaction.Rows(0).Item("VAT_Percent").ToString = "" Or objDTSOTransaction.Rows(0).Item("VAT_Percent").ToString = "0" Then
                    chkVAT.Checked = False
                Else
                    chkVAT.Checked = True
                End If

                If objDTSOTransaction.Rows(0).Item("Discount_Percent").ToString = "" Or objDTSOTransaction.Rows(0).Item("Discount_Percent").ToString = "0" Then
                    chkDiscount.Checked = False
                Else
                    chkDiscount.Checked = True
                End If

                If objDTSOTransaction.Rows(0).Item("VAT").ToString = "" Then
                    txtVAT.Text = 0
                Else
                    txtVAT.Text = CDbl(objDTSOTransaction.Rows(0).Item("VAT").ToString).ToString("#,##0.00")
                End If

                If objDTSOTransaction.Rows(0).Item("Deposit_Amt").ToString = "" Then
                    txtDeposit_Amt.Text = 0
                Else
                    txtDeposit_Amt.Text = CDbl(objDTSOTransaction.Rows(0).Item("Deposit_Amt").ToString).ToString("#,##0.00")
                End If

                If objDTSOTransaction.Rows(0).Item("Net_Amt").ToString = "" Then
                    txtNet_Amt.Text = 0
                Else
                    txtNet_Amt.Text = CDbl(objDTSOTransaction.Rows(0).Item("Net_Amt").ToString).ToString("#,##0.00")
                End If
                txtTax_No.Text = objDTSOTransaction.Rows(0).Item("Str3").ToString

                ' Todd 3 Mar 2010 - Add Distribution Center & Sub Route
                If objDTSOTransaction.Rows(0).Item("Route_Index").ToString <> "" Then
                    cboRoute.SelectedValue = objDTSOTransaction.Rows(0).Item("Route_Index").ToString
                End If
                If objDTSOTransaction.Rows(0).Item("SubRoute_Index").ToString <> "" Then
                    cboSubRoute.SelectedValue = objDTSOTransaction.Rows(0).Item("SubRoute_Index").ToString
                End If
                If objDTSOTransaction.Rows(0).Item("TransportRegion_Index").ToString <> "" Then
                    cboRegion.SelectedValue = objDTSOTransaction.Rows(0).Item("TransportRegion_Index").ToString
                End If
                cboDistributionCenter.SelectedValue = objDTSOTransaction.Rows(0).Item("DistributionCenter_Index").ToString
                cboDocumentType.SelectedValue = objDTSOTransaction.Rows(0).Item("DocumentType_Index").ToString

                '-- Dong_kk Add New
                'If objDTSOTransaction.Rows(0).Item("Time_ExpectedDocPickup").ToString <> "" Then
                '    dtpTime_ExpectedDocPickup.Value = objDTSOTransaction.Rows(0).Item("Time_ExpectedDocPickup").ToString
                'End If
                If Not String.IsNullOrEmpty(objDTSOTransaction.Rows(0).Item("Time_DocPickup").ToString) Then
                    dtpTime_DocPickup.Value = objDTSOTransaction.Rows(0).Item("Time_DocPickup").ToString
                End If
                'If objDTSOTransaction.Rows(0).Item("Time_DocIssued").ToString <> "" Then
                '    dtpTime_DocIssued.Value = objDTSOTransaction.Rows(0).Item("Time_DocIssued").ToString
                'End If
                _Status_Manifest = objDTSOTransaction.Rows(0).Item("Status_Manifest").ToString

                Me.txtInvoice_No.Text = objDTSOTransaction.Rows(0).Item("Invoice_No").ToString
                Me.txtInterface_No.Text = objDTSOTransaction.Rows(0).Item("Interface_No").ToString

                If Me.txtInterface_No.Text.Trim = "" Then
                    Me.txtInterface_No.Text = objDTSOTransaction.Rows(0).Item("SalesOrder_No").ToString
                End If

                Me.txtPO_No.Text = objDTSOTransaction.Rows(0).Item("PO_No").ToString

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objSOTransaction = Nothing
            objDTSOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get SO Items to display in SO Edit mode.
    ''' </summary>
    ''' <param name="SalesOrder_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd - 16 Jan 2010 - Clean code and fix bugs.
    ''' </remarks>
    Private Sub getSODetail(ByVal SalesOrder_Index As String)

        'Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.SEARCH)
        Dim objSOTransaction As New clsSO 'SOTransaction(SOTransaction.enuOperation_Type.SEARCH)

        Dim objDTSOTransaction As DataTable = New DataTable

        Try
            'objSOTransaction.getSODetail(SalesOrder_Index)
            'objDTSOTransaction = objSOTransaction.DataTable


            objDTSOTransaction = objSOTransaction.getSODetail(SalesOrder_Index, Copy_Red, Copy_All)

            '====== Comment 20181024_1730 
            'If Copy_Red And Copy_All = False And Copy_Red_NotStock = True Then
            '    Dim dtCopy_redstock As New DataTable
            '    dtCopy_redstock = objDTSOTransaction.Clone
            '    Dim objCopy As New cls_KSL
            '    Dim dtCopy As New DataTable
            '    dtCopy = objCopy.getCheckStock_Sales(Me._SalesOrder_Index, Me._Customer_Index, Me.cboDistributionCenter.SelectedValue.ToString)
            '    Dim dblReq As Decimal = 0

            '    Dim drArrCopy() As DataRow
            '    drArrCopy = dtCopy.Select("Total_Qty_Request <= 0")

            '    For Each drCopy As DataRow In drArrCopy
            '        Dim drArrCopy2() As DataRow
            '        Dim drRowcopy2 As DataRow
            '        drArrCopy2 = objDTSOTransaction.Select("Sku_Index = '" & drCopy("Sku_Index").ToString & "'")
            '        drRowcopy2 = drArrCopy2(0)
            '        'dblReq = drCopy("Qty_Bal") - (drCopy("Total_Qty_Pending") + drCopy("Total_Qty")) 'bal
            '        'dblReq = (drCopy("Total_Qty_Pending") + drCopy("Total_Qty"))
            '        'If drCopy("Qty_Bal") > 0 Then
            '        dblReq = drCopy("Total_Qty") - (drCopy("Qty_Bal") - drCopy("Total_Qty_Pending"))
            '        If dblReq > 0 Then
            '            If drRowcopy2("Sku_Index").ToString = drCopy("Sku_Index").ToString Then
            '                drRowcopy2("Qty") = Format(CDbl(dblReq / drRowcopy2("Ratio")), "#.00")
            '            End If
            '            If drRowcopy2("Qty") > 0 Then
            '                Dim drRow As DataRow
            '                drRow = drRowcopy2
            '                dtCopy_redstock.Rows.Add(drRow.ItemArray)
            '            End If
            '        End If
            '        'End If

            '        'For iRows As Integer = 0 To objDTSOTransaction.Rows.Count - 1
            '        '    dblReq = drCopy("Qty_Bal") - (drCopy("Total_Qty_Pending") + drCopy("Total_Qty")) 'bal
            '        '    If dblReq < 0 Then
            '        '        dblReq = drCopy("Total_Qty") - (drCopy("Qty_Bal") - drCopy("Total_Qty_Pending"))
            '        '        If dblReq > 0 Then
            '        '            If objDTSOTransaction.Rows(iRows).Item("Sku_Index").ToString = drCopy("Sku_Index").ToString Then
            '        '                objDTSOTransaction.Rows(iRows).Item("Qty") = Format(CDbl(dblReq / objDTSOTransaction.Rows(iRows).Item("Ratio")), "#.00")
            '        '            End If
            '        '            If objDTSOTransaction.Rows(iRows).Item("Qty") > 0 Then
            '        '                Dim drRow As DataRow
            '        '                drRow = objDTSOTransaction.Rows(iRows)
            '        '                dtCopy_redstock.Rows.Add(drRow.ItemArray)
            '        '            End If
            '        '        End If
            '        '    End If
            '        'Next
            '    Next

            '    If dtCopy_redstock.Rows.Count > 0 Then
            '        objDTSOTransaction = dtCopy_redstock
            '    Else
            '        W_MSG_Information("ไม่พบรากการที่สามารถคัดลอกได้")
            '        Me.Close()
            '    End If

            'End If
            '======== End Comment 20181024_1730 


            Me.grdSOItem.Rows.Clear()
            gblnDataGridClick = False

            For i As Integer = 0 To objDTSOTransaction.Rows.Count - 1
                With Me.grdSOItem
                    Me.grdSOItem.Rows.Add()
                    .Rows(i).Cells("Col_Seq").Value = objDTSOTransaction.Rows(i).Item("Item_Seq").ToString
                    .Rows(i).Cells("Col_SOItem_Index").Value = objDTSOTransaction.Rows(i).Item("SalesOrderItem_Index").ToString
                    .Rows(i).Cells("Col_SKU_ID").Value = objDTSOTransaction.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("LensSide").Value = objDTSOTransaction.Rows(i).Item("LensSide").ToString
                    .Rows(i).Cells("Col_SKU_ID").Tag = objDTSOTransaction.Rows(i).Item("Sku_Index").ToString
                    .Rows(i).Cells("Col_SKU_ID").ReadOnly = True
                    If Me.getSKU_Package(objDTSOTransaction.Rows(i).Item("Sku_Index").ToString, i) = False Then
                        W_MSG_Information_ByIndex(500008)
                        Exit Sub
                    End If
                    .Rows(i).Cells("Col_UOM").Value = objDTSOTransaction.Rows(i).Item("Package_Index").ToString
                    .Rows(i).Cells("Col_UOM").Tag = objDTSOTransaction.Rows(i).Item("Package_Index").ToString
                    .Rows(i).Cells("Col_Product_Type").Value = objDTSOTransaction.Rows(i).Item("Producttype").ToString
                    .Rows(i).Cells("Col_Description").Value = objDTSOTransaction.Rows(i).Item("Sku_Des").ToString
                    If objDTSOTransaction.Rows(i).Item("Qty").ToString = "" Then
                        .Rows(i).Cells("Col_Qty").Value = CDbl(0)
                    Else
                        .Rows(i).Cells("Col_Qty").Value = CDbl(objDTSOTransaction.Rows(i).Item("Qty").ToString)
                    End If

                    If objDTSOTransaction.Rows(i).Item("UnitPrice").ToString = "" Then
                        .Rows(i).Cells("Col_Unit_Price").Value = CDbl(0)
                    Else
                        .Rows(i).Cells("Col_Unit_Price").Value = CDbl(objDTSOTransaction.Rows(i).Item("UnitPrice").ToString)
                    End If
                    If IsNumeric(objDTSOTransaction.Rows(i).Item("Discount_Amt")) = False Then
                        .Rows(i).Cells("Col_Discount").Value = CDbl(0)
                    Else
                        .Rows(i).Cells("Col_Discount").Value = CDbl(objDTSOTransaction.Rows(i).Item("Discount_Amt").ToString)
                    End If
                    If objDTSOTransaction.Rows(i).Item("Amount").ToString = "" Then
                        .Rows(i).Cells("Col_Amount").Value = CDbl(0)
                    Else
                        .Rows(i).Cells("Col_Amount").Value = CDbl(objDTSOTransaction.Rows(i).Item("Amount").ToString)
                    End If
                    If objDTSOTransaction.Rows(i).Item("Weight").ToString = "" Then
                        .Rows(i).Cells("Col_Weight").Value = CDbl(0)
                    Else
                        .Rows(i).Cells("Col_Weight").Value = CDbl(objDTSOTransaction.Rows(i).Item("Weight").ToString)
                    End If
                    If objDTSOTransaction.Rows(i).Item("Volume").ToString = "" Then
                        .Rows(i).Cells("Col_Volume").Value = CDbl(0)
                    Else
                        .Rows(i).Cells("Col_Volume").Value = CDbl(objDTSOTransaction.Rows(i).Item("Volume").ToString)
                    End If
                    .Rows(i).Cells("Col_Remark").Value = objDTSOTransaction.Rows(i).Item("Remark").ToString
                    .Rows(i).Cells("col_PLot").Value = objDTSOTransaction.Rows(i).Item("PLot").ToString
                    .Rows(i).Cells("cbo_ItemStatus").Value = objDTSOTransaction.Rows(i).Item("ItemStatus_index").ToString
                    .Rows(i).Cells("col_ERP_Location").Value = objDTSOTransaction.Rows(i).Item("ERP_Location").ToString
                    .Rows(i).Cells("col_CopySalesOrderItem_Index").Value = ""
                    If Me._Copy_SalesOrder_Index <> "" Then
                        .Rows(i).Cells("col_CopySalesOrderItem_Index").Value = objDTSOTransaction.Rows(i).Item("SalesOrderItem_Index").ToString
                    End If
                    If objDTSOTransaction.Rows(i).Item("RGB_Check").ToString = "2" Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.Red
                    End If
                    If Me.Copy_Red Then
                        '.Rows(i).ReadOnly = True    
                        gblnDataGridClick = True
                        .Rows(i).Cells("Col_Qty").Value = CDbl(0)
                        .Rows(i).Cells("Col_Qty").Value = CDbl(objDTSOTransaction.Rows(i).Item("Qty").ToString)


                    End If

                End With
            Next

            gblnDataGridClick = True
        Catch ex As Exception
            Throw ex
        Finally
            objSOTransaction = Nothing
            objDTSOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' This sub gets SKU details to display in grdSOItem.
    ''' </summary>
    ''' <param name="Sku_Id"></param>
    ''' <param name="RowIndex"></param>
    ''' <remarks>
    ''' Added by: ?
    ''' Updated by: Todd - 16 Jan 2010 - Code Cleaning
    ''' </remarks>
    Private Sub getSKU_Detail(ByVal Sku_Id As String, ByVal RowIndex As Integer)
        Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strSku_Index As String = ""
        Try
            objClassDB.getSKU_Detail(Sku_Id)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then

                If Me._USE_PRODUCT_CUSTOMER AndAlso objDT.Rows(0).Item("Customer_Index").ToString <> Me._Customer_Index AndAlso Not objDT.Rows(0).Item("Customer_Index").ToString = Nothing Then
                    W_MSG_Information(String.Format("สินค้ามี{0}ไม่ตรงกับที่ระบุ !!", Me.lblSupplier.Text.Trim))
                    grdSOItem.Rows(RowIndex).Cells("Col_Description").Value = ""
                    grdSOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = ""
                    grdSOItem.Rows(RowIndex).Cells("Col_Product_Type").Value = ""
                    grdSOItem.Rows(RowIndex).Cells("Col_UOM").Value = ""
                    grdSOItem.Rows(RowIndex).Cells("Col_UOM").Tag = ""
                    grdSOItem.Rows(RowIndex).Cells("Col_SKU_ID").Value = ""
                    Exit Sub
                End If

                grdSOItem.Rows(RowIndex).Cells("Col_SKU_ID").Tag = objDT.Rows(0).Item("Sku_Index").ToString
                grdSOItem.Rows(RowIndex).Cells("Col_Description").Value = objDT.Rows(0).Item("Str1").ToString
                grdSOItem.Rows(RowIndex).Cells("Col_UOM").Tag = objDT.Rows(0).Item("Package_Index").ToString
                grdSOItem.Rows(RowIndex).Cells("Col_UOM").Value = objDT.Rows(0).Item("Package").ToString
                If objDT.Rows(0).Item("Price2").ToString <> "" Then
                    grdSOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = CDbl(objDT.Rows(0).Item("Price2").ToString)
                Else
                    grdSOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = 0
                End If


                grdSOItem.Rows(RowIndex).Cells("Col_Product_Type").Value = objDT.Rows(0).Item("ProductType").ToString

                strSku_Index = grdSOItem.Rows(RowIndex).Cells("Col_SKU_ID").Tag
            Else

                grdSOItem.Rows(RowIndex).Cells("Col_Description").Value = ""
                grdSOItem.Rows(RowIndex).Cells("Col_Unit_Price").Value = ""
                grdSOItem.Rows(RowIndex).Cells("Col_Product_Type").Value = ""
                grdSOItem.Rows(RowIndex).Cells("Col_UOM").Value = ""
                grdSOItem.Rows(RowIndex).Cells("Col_UOM").Tag = ""

                strSku_Index = grdSOItem.Rows(RowIndex).Cells("Col_SKU_ID").Value
            End If

            If Not strSku_Index = "" Then
                ' Search SKU Package 
                getSKU_Package(strSku_Index, RowIndex)
                grdSOItem.Rows(RowIndex).Cells("Col_UOM").Tag = grdSOItem.Rows(RowIndex).Cells("Col_UOM").Value
                Me.setWeightAndVolume(RowIndex)
            End If
            getDocumentType_Itemstatus(RowIndex)
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Get SO No. from SO Index.
    ''' </summary>
    ''' <param name="SalesOrder_Index"></param>
    ''' <remarks>
    ''' Updated by: Todd 16 Jan 2010
    '''    - Rename sub from "getSO_Index" to "getSalesOrderNoFromIndex"
    ''' </remarks>
    Private Sub getSalesOrderNoFromIndex(ByVal SalesOrder_Index As String)
        Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.SEARCH)
        Dim objDTSOTransaction As DataTable = New DataTable

        Try
            objSOTransaction.getSalesOrderNoFromIndex(SalesOrder_Index)
            objDTSOTransaction = objSOTransaction.DataTable

            If objDTSOTransaction.Rows.Count > 0 Then
                Me.txtSO_No.Text = objDTSOTransaction.Rows(0).Item("SalesOrder_No").ToString

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objSOTransaction = Nothing
            objDTSOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' This sub is to display remaining SO items tab. (รายการที่ค้างเบิก)
    ''' </summary>
    ''' <param name="SalesOrder_Index"></param>
    ''' <remarks></remarks>
    Private Sub ShowRemainSOItemQty(ByVal SalesOrder_Index As String)
        Try

            Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.SEARCH)
            Dim oDTSOTransaction As New DataTable

            objSOTransaction.getSOPendingWithdraw(SalesOrder_Index)
            oDTSOTransaction = objSOTransaction.DataTable

            Me.grdSORemain.Rows.Clear()
            'gblnDataGridClick = False

            For i As Integer = 0 To oDTSOTransaction.Rows.Count - 1
                With Me.grdSORemain
                    Me.grdSORemain.Rows.Add()
                    .Rows(i).Cells("Col_SalesOrderItem_Pending").Value = oDTSOTransaction.Rows(i).Item("SalesOrderItem_Index").ToString
                    .Rows(i).Cells("Col_SKU_ID_Pending").Value = oDTSOTransaction.Rows(i).Item("Sku_Id").ToString
                    .Rows(i).Cells("Col_SKU_ID_Pending").Tag = oDTSOTransaction.Rows(i).Item("Sku_Index").ToString
                    .Rows(i).Cells("Col_Description_Pending").Value = oDTSOTransaction.Rows(i).Item("Sku_Des").ToString
                    .Rows(i).Cells("Col_Qty_Remain").Value = CDbl(oDTSOTransaction.Rows(i).Item("Qty").ToString - oDTSOTransaction.Rows(i).Item("Qty_Withdraw").ToString)
                    .Rows(i).Cells("Col_Qty_Pending").Value = CDbl(oDTSOTransaction.Rows(i).Item("Qty").ToString)
                    .Rows(i).Cells("Col_UOM_Pending").Value = oDTSOTransaction.Rows(i).Item("PackDes").ToString
                    .Rows(i).Cells("Col_UOM_Pending").Tag = oDTSOTransaction.Rows(i).Item("Package_Index").ToString
                    .Rows(i).Cells("Col_Last_Withdraw_Date").Value = oDTSOTransaction.Rows(i).Item("Last_Withdraw_Date").ToString


                    .Rows(i).Cells("Col_Product_Type_Pending").Value = oDTSOTransaction.Rows(i).Item("ProductType").ToString
                End With
            Next

            'gblnDataGridClick = True

            objSOTransaction = Nothing
            oDTSOTransaction = Nothing
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    ''' <summary>
    ''' Sub "getSKU_Package"
    ''' This sub gets SKU Index and row index from SO Item datagrid,
    ''' then populate packages into combobox.
    ''' This sub is called when changing SKU in datagrid "grdSOItem".
    ''' </summary>
    ''' <param name="Sku_Index"></param>
    ''' <param name="RowIndex"></param>
    ''' <remarks>
    ''' Added by: ?
    ''' Updated by: Todd - 29 Dec 2009
    ''' </remarks>
    Private Function getSKU_Package(ByVal Sku_Index As String, ByVal RowIndex As Integer) As Boolean
        Try
            'Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            'Dim objDT As DataTable = New DataTable

            'objClassDB.getSKU_Package(Sku_Index)
            'objDT = objClassDB.DataTable

            Dim objDT As DataTable
            objDT = New cls_KSL_SINO().getSKU_Package(Sku_Index)


            If objDT.Rows.Count > 0 Then
                Dim dgvcob As New DataGridViewComboBoxCell
                dgvcob.DisplayMember = "Package"
                dgvcob.ValueMember = "Package_Index"
                dgvcob.DataSource = objDT

                grdSOItem.Rows(RowIndex).Cells("Col_UOM") = dgvcob
                grdSOItem.Rows(RowIndex).Cells("Col_UOM").Value = objDT.Rows(0).Item("Package_Index").ToString

                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
            Return False

        End Try

    End Function

    ''' <summary>
    ''' This function gets ratio for the pair of SKU and Package.
    ''' </summary>
    ''' <param name="Sku_Index"></param>
    ''' <param name="Package_Index"></param>
    ''' <returns>SKU Ratio</returns>
    ''' <remarks>
    ''' Added by: Todd - 16  Jan 2010
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
    ''' This function performs data validation before saving.
    ''' This should includes required field validation.
    ''' </summary>
    ''' <returns>
    ''' True - If all validation is passed.
    ''' False - One of the validation failed.
    ''' </returns>
    ''' <remarks>
    ''' Added by: Todd - 16 Jan 2010 - To split code from button "Save".
    ''' </remarks>
    Private Function ValidateDataBeforeSave() As Boolean
        Try
            ' TODO: HARDCODE-MSG
            Dim i As Integer
            Dim dblQty As Double = 0
            Dim dblReceivedQty As Double = 0

            ' ------ STEP 1: Validate required fields
            Select Case objStatus
                Case enuOperation_Type.ADDNEW, enuOperation_Type.COPY
                    Dim objcon As New DBType_SQLServer
                    Dim dt As New DataTable
                    If objcon.DBExeQuery_Scalar(String.Format("select count(*) from tb_SalesOrder where SalesOrder_No = '{0}' and Status not in (-1) ", Me.txtSO_No.Text)) > 0 Then
                        W_MSG_Error("เลขที่เอกสารซ้ำ")
                        objcon = Nothing
                        Return False
                    End If
            End Select




            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 10)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Return False
            End If
            tmpMsg = ""


            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdSOItem, 10)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Return False
            End If
            tmpMsg = ""


            If txtCustomer_Id.Text = "" Then
                ValidateDataBeforeSave = False
                W_MSG_Information_ByIndex(8)
                Exit Function
            End If

            If Me.cboDistributionCenter.SelectedValue = "0010000000000" Then
                ValidateDataBeforeSave = False
                W_MSG_Information("กรุณาระบุ : " & Me.lblDistributionCenter.Text)
                Exit Function
            End If

            If txtConsignee_Id.Text = "" Then
                ValidateDataBeforeSave = False
                W_MSG_Information_ByIndex(9)
                Exit Function
            End If

            If Me._USE_SO_PRICE_ALERT Then
                If txtNet_Amt.Text = 0 Then
                    ValidateDataBeforeSave = False
                    W_MSG_Information(GetMessage_Data("400043"))
                    'MessageBox.Show("ไม่สามารถบันทึกรายการได้ กรุณาตรวจสอบ ราคา/หน่วย และ จำนวน ของสินค้า", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                    grdSOItem.Focus()
                    Exit Function
                End If
            End If



            ' ------ STEP 2: Item Quantity and Unit Price Comparison.
            For i = 0 To grdSOItem.Rows.Count - 2
                With grdSOItem
                    ' Check if Qty is numeric value
                    If Not IsNumeric(.Rows(i).Cells("Col_Qty").Value) Then
                        ValidateDataBeforeSave = False
                        .Rows(i).Selected = True

                        W_MSG_Information_ByIndex("400044")
                        Exit Function
                    End If

                    ' Check if Qty is <= 0
                    dblQty = .Rows(i).Cells("Col_Qty").Value
                    If dblQty <= 0 Then
                        ValidateDataBeforeSave = False
                        .Rows(i).Selected = True
                        W_MSG_Information_ByIndex("400045")
                        Exit Function

                    End If

                    'KSL : Fix bug reset staus
                    If .Rows(i).Cells("Col_SOItem_Index").Value IsNot Nothing Then
                        Dim xSalesOrderItem_Index As String = .Rows(i).Cells("Col_SOItem_Index").Value
                        If xSalesOrderItem_Index.Trim <> "" Then
                            Dim objcon As New DBType_SQLServer
                            Dim dt As New DataTable
                            dt = objcon.DBExeQuery(String.Format("select ISNULL(Total_Qty_Withdraw,0) Total_Qty_Withdraw,* from tb_SalesOrderItem where SalesOrderItem_Index= '{0}'", xSalesOrderItem_Index))
                            objcon = Nothing

                            If (dblQty * dt.Rows(0)("Ratio")) < dt.Rows(0)("Total_Qty_Withdraw") Then
                                W_MSG_Error("แก้ไขจำนวนน้อยกว่าเบิกสินค้า ตรวจสอบจำนวนรายการที่ : " & (i + 1).ToString)
                                Exit Function
                            End If
                            If dt.Rows(0)("Qty_Withdraw") > 0 Then
                                If .Rows(i).Cells("Col_SKU_ID").Tag.ToString <> dt.Rows(0)("Sku_Index").ToString Then
                                    W_MSG_Error("มีการเบิกสินค้าไม่สามารถแก้ไขสินค้าได้ รายการที่ : " & (i + 1).ToString)
                                    Exit Function
                                End If
                            End If
                        End If
                    End If

                    If Me._USE_SO_PRICE_ALERT Then
                        ' Check if Unit Price is numeric value
                        If Not IsNumeric(.Rows(i).Cells("Col_Unit_Price").Value) Then
                            ValidateDataBeforeSave = False
                            .Rows(i).Selected = True
                            W_MSG_Information_ByIndex("400046")
                            Exit Function
                        End If

                        ' Check if Amount is numeric value
                        If Not IsNumeric(.Rows(i).Cells("Col_Amount").Value) Then
                            ValidateDataBeforeSave = False
                            .Rows(i).Selected = True
                            W_MSG_Information_ByIndex("400047")
                            Exit Function
                        End If
                    End If

                End With
            Next

            ValidateDataBeforeSave = True
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' Actual code for saving data. This function prepares all required data from UI
    ''' and put them into Data Tables and Collections.
    ''' </summary>
    ''' <returns>
    ''' SalesOrder_Index if succeeded, 
    ''' "" if failed.
    ''' </returns>

    Private Function DoSaveDocument() As String

        Dim i As Integer = 0
        Dim strTempSKUIndex = ""
        Dim strTempPackageIndex = ""
        Dim dblSKURatio As Decimal = 1

        ' ------ STEP 1: Declare Data Table Objects
        Dim objSaleOrder As New tb_SalesOrder
        Dim objSaleOrderItem As New tb_SalesOrderItem
        Dim objSaleOrderItemCollection As New List(Of tb_SalesOrderItem)

        Try

            ' ------ STEP 2: Prepare values for SO Header
            objSaleOrder.SalesOrder_Index = Me._SalesOrder_Index

            If Me.cboDocumentType.SelectedValue Is Nothing Then
                objSaleOrder.DocumentType_Index = ""
            Else
                objSaleOrder.DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
            End If

            'ja Update Run DocumentNumber by DocumentType

            If txtSO_No.Text = "" Then
                Dim objDocumentNumber As New Sy_DocumentNumber
                objSaleOrder.SalesOrder_No = objDocumentNumber.Auto_DocumentType_Number(objSaleOrder.DocumentType_Index, "", Me.dtpSO_Date.Value)
                Me.txtSO_No.Text = objSaleOrder.SalesOrder_No
                objDocumentNumber = Nothing
            Else
                objSaleOrder.SalesOrder_No = Me.txtSO_No.Text

            End If

            objSaleOrder.SalesOrder_Date = Me.dtpSO_Date.Value

            If Me.txtCarrier_ID.Tag = Nothing Then
                objSaleOrder.Carrier_Index = ""
            Else
                objSaleOrder.Carrier_Index = Me.txtCarrier_ID.Tag.ToString

            End If
            If Me.txtShipping_Location_ID.Tag = Nothing Then
                objSaleOrder.Customer_Shipping_Location_Index = ""
            Else
                objSaleOrder.Customer_Shipping_Location_Index = Me.txtShipping_Location_ID.Tag.ToString
            End If


            If Me.txtConsignee_Id.Tag = Nothing Then
                objSaleOrder.Customer_Shipping_Index = ""
            Else
                objSaleOrder.Customer_Shipping_Index = Me.txtConsignee_Id.Tag.ToString
            End If

            If Me._Customer_Index = Nothing Then
                objSaleOrder.Customer_Index = ""
            Else
                objSaleOrder.Customer_Index = Me._Customer_Index.ToString
            End If

            objSaleOrder.Remark = Me.txtRemark.Text
            objSaleOrder.Credit_Term = Me.txtCreditTerm.Text

            If Me.cboCurrency.SelectedValue Is Nothing Then
                objSaleOrder.Currency_Index = ""
            Else
                objSaleOrder.Currency_Index = Me.cboCurrency.SelectedValue.ToString
            End If
            objSaleOrder.Exchange_Rate = Me.txtExRate.Text

            If Me.cboConditionPay.SelectedValue Is Nothing Then
                objSaleOrder.PaymentMethod_Index = ""
            Else
                objSaleOrder.PaymentMethod_Index = Me.cboConditionPay.SelectedValue.ToString
            End If
            ' --- Todd: 16 Jan 2010 - Payment_Ref & FullPaid_Date are not used for now.
            ' May use again in the future, so keep it this way.
            'objSaleOrder.Payment_Ref = ""
            'objSaleOrder.FullPaid_Date = Me.dtpDue_Date.Value
            objSaleOrder.Discount_Percent = Me.txtDiscount_Percent.Text
            objSaleOrder.Discount_Amt = Me.txtDiscount_Amt.Text
            objSaleOrder.Deposit_Amt = Me.txtDeposit_Amt.Text
            objSaleOrder.Total_Amt = Me.txtSubtotal.Text
            objSaleOrder.VAT_Percent = Me.txtVAT_Percent.Text
            objSaleOrder.VAT = Me.txtVAT.Text
            objSaleOrder.Net_Amt = Me.txtNet_Amt.Text

            If Double.IsNaN(objSaleOrder.Discount_Percent) = True Then objSaleOrder.Discount_Percent = 0
            If Double.IsNaN(objSaleOrder.Discount_Amt) = True Then objSaleOrder.Discount_Amt = 0
            If Double.IsNaN(objSaleOrder.Deposit_Amt) = True Then objSaleOrder.Deposit_Amt = 0
            If Double.IsNaN(objSaleOrder.Total_Amt) = True Then objSaleOrder.Total_Amt = 0
            If Double.IsNaN(objSaleOrder.VAT_Percent) = True Then objSaleOrder.VAT_Percent = 0
            If Double.IsNaN(objSaleOrder.VAT) = True Then objSaleOrder.VAT = 0
            If Double.IsNaN(objSaleOrder.Net_Amt) = True Then objSaleOrder.Net_Amt = 0

            objSaleOrder.Amount = (objSaleOrder.Total_Amt - objSaleOrder.Discount_Amt)

            ' --- Todd: 16 Jan 2010 - These fields in tb_SalesOrder do not have fields in 
            ' database 'tb_SalesOder. We will use Str10, Str6, Str7 to store customer address.
            ' We allow flexibility to make changes to customer address, tel, fax for each SO,
            ' but these values will NOT be saved to customer master table.

            'objSaleOrder.Supplier_Address = Me.txtCustomer_Address.Text
            'objSaleOrder.Supplier_Tel = Me.txtCustomer_Phone.Text
            'objSaleOrder.Supplier_Fax = Me.txtCustomer_Fax.Text
            objSaleOrder.Str10 = Me.txtCustomer_Address.Text
            objSaleOrder.Str6 = Me.txtCustomer_Phone.Text
            objSaleOrder.Str7 = Me.txtCustomer_Fax.Text
            objSaleOrder.Str1 = Me.txtRef1.Text
            objSaleOrder.Str2 = Me.txtRef2.Text
            objSaleOrder.Str3 = Me.txtTax_No.Text
            ' --- Note we use field Str9 to store Ship Address because the field length is 2000.
            objSaleOrder.Str9 = Me.txtShip_Address.Text.ToString.Trim
            objSaleOrder.Str4 = Me.txtShip_Phone.Text.ToString.Trim
            objSaleOrder.Str5 = Me.txtShip_Fax.Text.ToString.Trim

            ' --- Todd: 03 March 2010 - Add Distribution Center & Sub Route 
            If Me.cboDistributionCenter.SelectedValue Is Nothing Then
                objSaleOrder.DistributionCenter_Index = ""
            Else
                objSaleOrder.DistributionCenter_Index = Me.cboDistributionCenter.SelectedValue.ToString
            End If
            If Me.cboSubRoute.SelectedValue Is Nothing Then
                objSaleOrder.SubRoute_Index = ""
            Else
                objSaleOrder.SubRoute_Index = Me.cboSubRoute.SelectedValue.ToString
            End If
            If Me.cboRoute.SelectedValue Is Nothing Then
                objSaleOrder.Route_Index = ""
            Else
                objSaleOrder.Route_Index = Me.cboRoute.SelectedValue.ToString
            End If
            If Me.cboRegion.SelectedValue Is Nothing Then
                objSaleOrder.TransportRegion_Index = ""
            Else
                objSaleOrder.TransportRegion_Index = Me.cboRegion.SelectedValue.ToString
            End If
            '-- dong_kk Add New
            If Not dtpTime_DocPickup.Checked Then
                objSaleOrder.chkTime_DocPickup = False
            End If
            objSaleOrder.Time_ExpectedDocPickup = dtpTime_DocPickup.Value
            objSaleOrder.Time_DocPickup = dtpTime_DocPickup.Value

            If Not dtpDue_Date.Checked Then
                objSaleOrder.chkExpected_Delivery_Date = False
            End If
            objSaleOrder.Expected_Delivery_Date = Me.dtpDue_Date.Value
            objSaleOrder.Process_Id = 10
            '    objSaleOrder.Time_DocIssued = dtpSO_Date.Value

            'objSaleOrder.add_by = Me.txtUser.Text
            Select Case objStatus
                Case enuOperation_Type.ADDNEW
                    objSaleOrder.add_by = WV_UserName ' Me.txtUser.Text
                Case enuOperation_Type.UPDATE
                    objSaleOrder.update_by = WV_UserName ' Me.txtUser.Text
            End Select
            objSaleOrder.Status = Status

            If Me.txtReserve_NO.Tag Is Nothing Then
                objSaleOrder.Reserve_index = ""
            Else
                objSaleOrder.Reserve_index = Me.txtReserve_NO.Tag
            End If


            ' NEW Field 2010/10/14
            objSaleOrder.chkTime_DocPickup = dtpTime_DocPickup.Checked
            objSaleOrder.chkExpected_Delivery_Date = dtpDue_Date.Checked
            objSaleOrder.District_Index = ""
            objSaleOrder.Province_Index = ""
            objSaleOrder.VehicleType_Index = ""
            objSaleOrder.PODRemark1 = ""
            objSaleOrder.PODDoc_Copy1 = ""
            objSaleOrder.PODDoc_Copy2 = ""
            objSaleOrder.PODDoc_Copy3 = ""
            objSaleOrder.PODDoc_Copy4 = ""
            objSaleOrder.PODDoc_Copy5 = ""
            objSaleOrder.GRRemark1 = ""
            objSaleOrder.GRDoc_Copy1 = ""
            objSaleOrder.GRDoc_Copy2 = ""
            objSaleOrder.GRDoc_Copy3 = ""
            objSaleOrder.GRDoc_Copy4 = ""
            objSaleOrder.GRDoc_Copy5 = ""


            ' Temp Qty for Save Header Flo4,Flo5
            Dim tQty As Decimal = 0.0
            Dim tTotalQty As Decimal = 0.0

            ' ------ STEP 3: Prepare values for SO Items
            For i = 0 To grdSOItem.Rows.Count - 2

                With grdSOItem
                    objSaleOrderItem = New tb_SalesOrderItem

                    If .Rows(i).Cells("Col_SOItem_Index").Value = "" Then
                        Dim objDBIndex As New Sy_AutoNumber
                        .Rows(i).Cells("Col_SOItem_Index").Value = objDBIndex.getSys_Value("SalesOrderItem_Index")
                        objSaleOrderItem.SalesOrderItem_Index = .Rows(i).Cells("Col_SOItem_Index").Value
                        objDBIndex = Nothing
                    Else
                        objSaleOrderItem.SalesOrderItem_Index = .Rows(i).Cells("Col_SOItem_Index").Value.ToString
                    End If
                    objSaleOrderItem.Item_Seq = .Rows(i).Cells("Col_Seq").Value.ToString

                    objSaleOrderItem.SalesOrder_Index = objSaleOrder.SalesOrder_Index

                    ' Todd 16 Jan 2010 - This is bad code. Not the same field.
                    'objSaleOrderItem.Last_Withdraw_Date = Me.dtpDue_Date.Value

                    If .Rows(i).Cells("Col_SKU_ID").Tag IsNot Nothing Then
                        ' We keep SKU Index to use for calculating SKU Ratio for Total Qty.
                        strTempSKUIndex = .Rows(i).Cells("Col_SKU_ID").Tag.ToString
                        objSaleOrderItem.Sku_Index = strTempSKUIndex
                    End If


                    Dim dgvcob As New DataGridViewComboBoxCell
                    ' ------ Here we cast out the combo box within datagrid to get 
                    ' ------ the selected Package Index value.
                    dgvcob = grdSOItem.Rows(i).Cells("Col_UOM")
                    If dgvcob.Value IsNot Nothing Then
                        strTempPackageIndex = dgvcob.Value.ToString
                        objSaleOrderItem.Package_Index = strTempPackageIndex
                    End If

                    ' ---------------------------------------
                    ' Modified by: Todd 16 Jan 2010
                    ' Fix bug to calculate ratio for Total_Qty.
                    dblSKURatio = getSKU_PackageRatio(strTempSKUIndex, strTempPackageIndex)
                    objSaleOrderItem.Qty = CDbl(.Rows(i).Cells("Col_Qty").Value)

                    If IsNumeric(.Rows(i).Cells("Col_Qty").Value) Then
                        objSaleOrderItem.Total_Qty = dblSKURatio * CDbl(.Rows(i).Cells("Col_Qty").Value)

                        ' For Save So Header
                        tQty = tQty + objSaleOrderItem.Qty
                        tTotalQty = tTotalQty + objSaleOrderItem.Total_Qty
                    Else
                        ' Error
                        ' This should be taken care of since data validation,
                        ' so we do not check again here!
                    End If



                    If .Rows(i).Cells("Col_Unit_Price").Value IsNot Nothing Then
                        objSaleOrderItem.UnitPrice = .Rows(i).Cells("Col_Unit_Price").Value.ToString
                    Else
                        objSaleOrderItem.UnitPrice = 0
                    End If

                    If .Rows(i).Cells("Col_Amount").Value IsNot Nothing Then
                        objSaleOrderItem.Amount = .Rows(i).Cells("Col_Amount").Value.ToString ' chang today
                    Else
                        objSaleOrderItem.Amount = 0
                    End If
                    ' ------ Todd 16 Jan 2010 - To use in-line discount field in SO, just enable "Col_Discount" field in datagrid.
                    If .Rows(i).Cells("Col_Discount").Value IsNot Nothing Then
                        objSaleOrderItem.Discount_Amt = .Rows(i).Cells("Col_Discount").Value.ToString
                    Else
                        objSaleOrderItem.Discount_Amt = 0
                    End If
                    ' ------ Todd 16 Jan 2010
                    ' ------ Calculate Total Amount
                    ' ------ In general the Total Amount equals to Amount - Discount.
                    objSaleOrderItem.Total_Amt = objSaleOrderItem.Amount - objSaleOrderItem.Discount_Amt

                    ' ------ Todd 16 Jan 2010
                    ' ------ We do not use Weight & Volume yet, but prepare for future.
                    If .Rows(i).Cells("Col_Weight").Value IsNot Nothing Then
                        objSaleOrderItem.Weight = .Rows(i).Cells("Col_Weight").Value.ToString
                    End If

                    If .Rows(i).Cells("Col_Volume").Value IsNot Nothing Then
                        objSaleOrderItem.Volume = .Rows(i).Cells("Col_Volume").Value.ToString
                    End If

                    If .Rows(i).Cells("Col_Remark").Value Is Nothing Then
                        objSaleOrderItem.Remark = ""
                    Else
                        objSaleOrderItem.Remark = .Rows(i).Cells("Col_Remark").Value.ToString
                    End If

                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    objSaleOrderItem.Ratio = objRatio.getRatio(objSaleOrderItem.Sku_Index, objSaleOrderItem.Package_Index)
                    objRatio = Nothing

                    If .Rows(i).Cells("cbo_ItemStatus").Value Is Nothing Then
                        objSaleOrderItem.Itemstatus_index = ""
                    Else
                        objSaleOrderItem.Itemstatus_index = .Rows(i).Cells("cbo_ItemStatus").Value
                    End If

                    If .Rows(i).Cells("col_Plot").Value Is Nothing Then
                        objSaleOrderItem.Plot = ""
                    Else
                        objSaleOrderItem.Plot = .Rows(i).Cells("col_Plot").Value
                    End If

                    If .Rows(i).Cells("col_ERP_Location").Value Is Nothing Then
                        objSaleOrderItem.ERP_location = ""
                    Else
                        objSaleOrderItem.ERP_location = .Rows(i).Cells("col_ERP_Location").Value
                    End If



                End With

                objSaleOrderItemCollection.Add(objSaleOrderItem)
            Next

            ' For Save Qty In Header
            objSaleOrder.Flo4 = tQty
            objSaleOrder.Flo5 = tTotalQty

            ' ------ STEP 6: Call the actual saving function in SOTransaction class. 
            Select Case objStatus
                Case enuOperation_Type.ADDNEW, enuOperation_Type.COPY
                    Dim objDBPOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.ADDNEW, objSaleOrder, objSaleOrderItemCollection)
                    Me._SalesOrder_Index = objDBPOTransaction.SaveData
                    objDBPOTransaction = Nothing
                Case enuOperation_Type.UPDATE
                    Dim objDBPOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.UPDATE, objSaleOrder, objSaleOrderItemCollection)
                    Me._SalesOrder_Index = objDBPOTransaction.SaveData
                    objDBPOTransaction = Nothing
            End Select



            ' If save succeeded, _SalesOrder_Index will not be ""
            DoSaveDocument = Me._SalesOrder_Index

        Catch ex As Exception
            Throw ex

        Finally
            objSaleOrderItemCollection = Nothing
        End Try

    End Function

#End Region

    '#Region " RESERVE FUNCTIONS & EVENTS "

    '    Private Sub btnBorrow_Item_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSO_Pull_Item.Click

    '        Try
    '            If txtCustomer_Id.Text = "" Then
    '                W_MSG_Information_ByIndex(8)
    '                Exit Sub
    '            End If
    '            If txtReserve_NO.Text = "" Then
    '                W_MSG_Information_ByIndex(300056)
    '                Exit Sub
    '            End If
    '            If getReserveHeader() Then
    '                getReserveItem()
    '            End If

    '        Catch ex As Exception
    '            W_MSG_Error(ex.Message)
    '        End Try

    '    End Sub

    '    Private Sub getReserveItem()

    '        Try

    '            Dim objReserveItemLocation As New tb_ReserveLocation
    '            Dim dtReserveItemLocation As DataTable

    '            '---------------------------------------------------------------------
    '            objReserveItemLocation.SearchReserveItemLocation(" Reserve_NO= '" & Me.txtReserve_NO.Text & "'  ")
    '            dtReserveItemLocation = objReserveItemLocation.GetDataTable
    '            grdSOItem.Rows.Clear()


    '            Dim odtReserve As DataTable = objReserveItemLocation.GetDataTable

    '            For i As Integer = 0 To dtReserveItemLocation.Rows.Count - 1
    '                With Me.grdSOItem
    '                    Me.grdSOItem.Rows.Add()

    '                    .Rows(i).Cells("Col_SOItem_Index").Value = ""
    '                    .Rows(i).Cells("Col_SKU_ID").Value = dtReserveItemLocation.Rows(i).Item("Sku_Id").ToString
    '                    .Rows(i).Cells("Col_SKU_ID").Tag = dtReserveItemLocation.Rows(i).Item("Sku_Index").ToString

    '                    getSKU_Package(dtReserveItemLocation.Rows(i).Item("Sku_Index").ToString, i)
    '                    .Rows(i).Cells("Col_UOM").Value = dtReserveItemLocation.Rows(i).Item("Col_UOM").ToString
    '                    .Rows(i).Cells("Col_UOM").Tag = dtReserveItemLocation.Rows(i).Item("Col_UOM").ToString

    '                    .Rows(i).Cells("Col_Product_Type").Value = dtReserveItemLocation.Rows(i).Item("Producttype").ToString
    '                    .Rows(i).Cells("Col_Description").Value = dtReserveItemLocation.Rows(i).Item("Sku_Description").ToString

    '                    If dtReserveItemLocation.Rows(i).Item("Qty").ToString = "" Then
    '                        .Rows(i).Cells("Col_Qty").Value = 0
    '                    Else
    '                        .Rows(i).Cells("Col_Qty").Value = dtReserveItemLocation.Rows(i).Item("Qty").ToString
    '                    End If

    '                    'If dtReserveItemLocation.Rows(i).Item("UnitPrice").ToString = "" Then
    '                    '    .Rows(i).Cells("Col_Unit_Price").Value = 0
    '                    'Else
    '                    '    .Rows(i).Cells("Col_Unit_Price").Value = dtReserveItemLocation.Rows(i).Item("UnitPrice").ToString
    '                    'End If

    '                    'If dtReserveItemLocation.Rows(i).Item("Amount").ToString = "" Then
    '                    '    .Rows(i).Cells("Col_Amount").Value = 0
    '                    'Else
    '                    '    .Rows(i).Cells("Col_Amount").Value = Val(dtReserveItemLocation.Rows(0).Item("Amount").ToString).ToString("#,##0.000")

    '                    'End If

    '                    If dtReserveItemLocation.Rows(i).Item("WeightOut").ToString = "" Then
    '                        .Rows(i).Cells("ColWeight").Value = 0
    '                    Else
    '                        .Rows(i).Cells("ColWeight").Value = dtReserveItemLocation.Rows(i).Item("WeightOut").ToString
    '                    End If

    '                    If dtReserveItemLocation.Rows(i).Item("VAT").ToString = "" Then
    '                        .Rows(i).Cells("Col_Vet").Value = 0
    '                    Else
    '                        .Rows(i).Cells("Col_Vet").Value = dtReserveItemLocation.Rows(i).Item("VAT").ToString
    '                    End If

    '                    'If dtReserveItemLocation.Rows(i).Item("Flo2").ToString = "" Then
    '                    '    .Rows(i).Cells("Col_B_For_Vet").Value = 0
    '                    'Else
    '                    '    .Rows(i).Cells("Col_B_For_Vet").Value = dtReserveItemLocation.Rows(i).Item("Flo2").ToString
    '                    'End If


    '                    .Rows(i).Cells("Col_Remark").Value = dtReserveItemLocation.Rows(i).Item("Remark").ToString

    '                    grdSOItem.Rows(i).Cells("Col_SKU_ID").ReadOnly = True
    '                    grdSOItem.Rows(i).Cells("Col_Btn_GetSKU").ReadOnly = True

    '                    'Dim dgvbtn As New DataGridViewButtonCell
    '                    'dgvSOItem.Rows(i).Cells("Col_Btn_GetSKU") = dgvbtn
    '                    'dgvbtn.ReadOnly = True

    '                End With
    '            Next
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Sub

    '    Private Function getReserveHeader() As Boolean
    '        Dim objReserve As New tb_Reserve
    '        Try
    '            objReserve.ReserveView(" AND Reserve_NO= '" & Me.txtReserve_NO.Text & "' AND Customer_Index = '" & Me._Customer_Index & "'")
    '            Dim odtReserve As DataTable = objReserve.GetDataTable
    '            If odtReserve.Rows.Count = 0 Then
    '                W_MSG_Information_ByIndex(13)
    '                Return False

    '            End If

    '            With odtReserve.Rows(0)
    '                txtCarrier_ID.Tag = .Item("Carrier_Index").ToString
    '                txtCarrier_ID.Text = .Item("Carrier_Id").ToString
    '                txtCarrier_Name.Text = .Item("DesCar").ToString

    '                txtConsignee_Id.Text = .Item("Customer_Shipping_Index").ToString
    '                txtConsignee_Id.Tag = .Item("Company_ID").ToString
    '                txtConsignee_Name.Text = .Item("Company_Name").ToString
    '                txtShipping_Location_ID.Tag = .Item("Customer_Shipping_Location_Index").ToString
    '                txtShipping_Location_ID.Text = .Item("Company_Name").ToString
    '                txtShip_Address.Text = .Item("Address_Shipping_Location").ToString
    '                txtShip_Phone.Text = .Item("Tel_Shipping_Location").ToString
    '                txtShip_Fax.Text = .Item("Fax_Shipping_Location").ToString

    '                If .Item("Amount").ToString = "" Then
    '                    txtSubtotal.Text = 0
    '                Else
    '                    txtSubtotal.Text = .Item("Amount").ToString
    '                End If

    '                If .Item("Discount_Percent").ToString = "" Then
    '                    txtDiscount_Percent.Text = 0
    '                Else
    '                    txtDiscount_Percent.Text = .Item("Discount_Percent").ToString
    '                End If

    '                If .Item("Discount_Amt").ToString = "" Then
    '                    txtDiscount_Amt.Text = 0
    '                Else
    '                    txtDiscount_Amt.Text = .Item("Discount_Amt").ToString
    '                End If

    '                If .Item("VAT_Percent").ToString = "" Then
    '                    txtVAT_Percent.Text = 0
    '                Else
    '                    txtVAT_Percent.Text = .Item("VAT_Percent").ToString
    '                End If


    '                If .Item("VAT_Percent").ToString = "" Or .Item("VAT_Percent").ToString = "0" Then
    '                    chkVAT.Checked = False
    '                Else
    '                    chkVAT.Checked = True
    '                End If

    '                If .Item("Discount_Percent").ToString = "" Or .Item("Discount_Percent").ToString = "0" Then
    '                    chkDiscount.Checked = False
    '                Else
    '                    chkDiscount.Checked = True
    '                End If

    '                If .Item("VAT").ToString = "" Then
    '                    txtVAT.Text = 0
    '                Else
    '                    txtVAT.Text = .Item("VAT").ToString
    '                End If

    '                If .Item("Deposit_Amt").ToString = "" Then
    '                    txtDeposit_Amt.Text = 0
    '                Else
    '                    txtDeposit_Amt.Text = .Item("Deposit_Amt").ToString
    '                End If

    '                If .Item("Net_Amt").ToString = "" Then
    '                    txtNet_Amt.Text = 0
    '                Else
    '                    txtNet_Amt.Text = .Item("Net_Amt").ToString
    '                End If
    '                txtTax_No.Text = .Item("Str3").ToString
    '            End With

    '            Return True
    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Function

    '    Private Sub txtReserve_NO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtReserve_NO.KeyDown
    '        Try

    '            If e.KeyCode = Keys.Enter Then
    '                ' Validation 
    '                If txtCustomer_Id.Text = "" Then
    '                    W_MSG_Information_ByIndex(8)
    '                    Exit Sub
    '                End If
    '                If txtReserve_NO.Text = "" Then
    '                    W_MSG_Information_ByIndex(300056)
    '                    Exit Sub
    '                End If

    '                If getReserveHeader() Then
    '                    getReserveItem()
    '                End If

    '            End If

    '        Catch ex As Exception
    '            W_MSG_Error(ex.Message)
    '        End Try
    '    End Sub

    '#End Region
    Private Sub cboRoute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectedIndexChanged
        Try
            getComboSubRoute(cboRoute.SelectedValue.ToString)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region " UNUSED & BACKUP FUNCTIONS(TEMPORARY) "

    '''' <summary>
    '''' Disable buttons in case we do not allow user to save.
    '''' </summary>
    '''' <remarks></remarks>
    'Sub ViewOnly()
    '    Try
    '        btnPrint.Enabled = True
    '        grbPrintReport.Enabled = True
    '        cboPrint.Enabled = True

    '        btnSave.Enabled = False
    '        btnDelete.Enabled = False
    '        grdSOItem.ReadOnly = True

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

#End Region


    'Private Sub btnReserve_PopUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReserve_PopUp.Click
    '    Try
    '        If txtCustomer_Id.Text = "" Then
    '            W_MSG_Information_ByIndex(8)
    '            Exit Sub
    '        End If
    '        'If txtReserve_NO.Text = "" Then
    '        '    W_MSG_Information_ByIndex(300056)
    '        '    Exit Sub
    '        'End If
    '        If getReserveHeader() Then
    '            getReserveItem()
    '        End If

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

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
    Private Sub grdSOItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdSOItem.EditingControlShowing
        Try
            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            Dim strName As String = grdSOItem.Columns(grdSOItem.CurrentCell.ColumnIndex).Name
            If (strName <> "Col_Btn_GetSKU") And (strName <> "Col_UOM") And (strName <> "cbo_ItemStatus") Then
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
            Dim Column_Index As String = grdSOItem.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdSOItem.Columns("Col_Unit_Price").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSOItem.Columns("Col_Qty").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSOItem.Columns("Col_Discount").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdSOItem.Columns("Col_Amount").Index
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
                            If grdSOItem.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdSOItem.CurrentRow.Cells(Column_Index).EditedFormattedValue
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


    '''' <summary>
    '''' Check null data is numberic data
    '''' </summary>
    '''' <param name="pstrValue">Numberic data</param>
    '''' <returns>ture is numberic; false is not numberic</returns>
    '''' <remarks>เป็นการ check data ที่ user ป้อนเข้ามาจาก UI</remarks>
    '''' 
    'Private Function IsNumeric(ByVal pstrValue As String) As Boolean
    '    Try
    '        If pstrValue = "" Then
    '            Return False
    '        ElseIf CDbl(pstrValue) = "0" Then
    '            Return False
    '        Else
    '            Return True
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Function

#End Region


    Private Sub Get_CUSTOMERSHIPPINGLOCATION()
        Try
            Dim objCust_Ship_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
            Dim odtCus_ShipLocation As DataTable

            objCust_Ship_Location.getCus_Ship_Locartion_SearchPopUp(" WHERE  Customer_Shipping_Location_Id Like '" & Me.txtConsignee_Id.Text & "' ")
            ' objCustomer_Shipping_Location.getPopup_Search(" Where Str1 = '" & txtConsignee_Id.Text & "'")
            odtCus_ShipLocation = objCust_Ship_Location.GetDataTable

            If odtCus_ShipLocation.Rows.Count > 0 Then
                Me.txtShipping_Location_ID.Tag = odtCus_ShipLocation.Rows(0).Item("Customer_Shipping_Location_Index").ToString
                getCus_Shipping_Location_Index()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub getDocumentType_Itemstatus(ByVal RowIndex As Integer)
        Dim objDocumentType_Itemstatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDocumentType As New WMS_STD_Master_Datalayer.ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)

        Try

            objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocumentType.SelectedValue)

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


            grdSOItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = odtDocumentType.Rows(0).Item("ItemStatus_Index").ToString

            grdSOItem.Rows(RowIndex).Cells("cbo_ItemStatus").Value = odtDocumentType_Itemstatus.Rows(0).Item("ItemStatus_Index").ToString


        Catch ex As Exception
            Throw ex
        Finally
            objDocumentType_Itemstatus = Nothing
        End Try

    End Sub

    Private Sub cboDocumentType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectionChangeCommitted
        Try
            For iRow As Integer = 0 To grdSOItem.Rows.Count - 2
                getDocumentType_Itemstatus(iRow)
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmSO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigSalesOrder
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 10)
                    oFunction.SW_Language_Column(Me, Me.grdSOItem, 10)
                    oFunction.SW_Language_Column(Me, Me.grdSORemain, 10)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdSOItem_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdSOItem.RowsAdded
        Try
            SetRunningNo(Me.grdSOItem)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub SetRunningNo(ByVal grd As DataGridView)
        For i As Integer = 0 To grd.RowCount - 2
            If IsNumeric(grd.Rows(i).Cells("Col_Seq").Value) Then
                If grd.Rows(i).Cells("Col_Seq").Value = 0 Then
                    grd.Rows(i).Cells("Col_Seq").Value = i + 1
                End If
            Else
                grd.Rows(i).Cells("Col_Seq").Value = i + 1
            End If
        Next
    End Sub

    Private Sub btnClose_SO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose_SO.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (Me.SalesOrder_Index = Nothing) Then Exit Sub
            If (Me.Status = 0) Then Exit Sub

            If (New clsSO().canCloseSO(Me.SalesOrder_Index)) Then
                Dim _strMatchSO As String = New clsSO().matchSO_Withdraw(Me.SalesOrder_Index)
                Dim _result As Integer
                If (Not _strMatchSO = Nothing) Then
                    _result = MessageBox.Show(String.Format("{1}{2}คุณต้องการปิดเอกสารเลขที่ {0} หรือไม่?", Me.txtSO_No.Text.Trim(), _strMatchSO, vbCrLf), "ปิดเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                Else
                    _result = MessageBox.Show(String.Format("คุณต้องการปิดเอกสารเลขที่ {0} หรือไม่?", Me.txtSO_No.Text.Trim()), "ปิดเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                End If
                If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
                If (New clsSO().canCloseSO(Me.SalesOrder_Index)) Then
                    If (New clsSO().closeSO(Me.SalesOrder_Index)) Then
                        MessageBox.Show(String.Format("ปิดเอกสารเลขที่ {0} เรียบร้อย", Me.txtSO_No.Text.Trim()), "ปิดเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    Else
                        MessageBox.Show(String.Format("เกิดข้อผิดพลาดทางข้อมูล, กรุณาลองอีกครั้ง", Me.lblDocumentType.Text), "ปิดเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Select Case Status
                Case 0 ' 
                    objStatus = enuOperation_Type.UPDATE
                Case -1 ' ยกเลิก
                Case 1 ' รอยืนยัน
                    objStatus = enuOperation_Type.UPDATE
                Case 2, 3, 4, 5, 6 ' รอเบิก  รอส่ง / กำลังจัดส่ง / เสร็จสิ้น ค้างจ่าย
                    If W_MSG_Confirm("สินค้ามีการทำงานไปแล้วคุณต้องการแก้ไข ใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                Case Else
            End Select



            Me.btnDelete.Enabled = True
            Me.btnSave.Enabled = True
            Me.grdSOItem.ReadOnly = False
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnFG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFG.Click
        Try
            Dim frm As New frmSO_PRQ_KSL
            frm.SalesOrder_Index = Me.SalesOrder_Index
            frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            If Me.txtShipping_Location_ID.Tag = "" Then
                Exit Sub
            End If

            Dim objfrm As New frmCustomer_Shipping_Location_V3
            objfrm.SaveType = 1
            objfrm.Customer_Shipping_Index = ""
            objfrm.Customer_Shipping_Location_Index = Me.txtShipping_Location_ID.Tag.ToString
            objfrm.ShowDialog()

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCreditTrem.Click
        Try

            If W_MSG_Confirm("ต้องการแก้ไขข้อมูล ใช่ หรือ ไม่") = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            If txtCreditTerm.Text.Length = 0 Then
                txtCreditTerm.Text = 0
            End If
            Select Case objStatus
                Case enuOperation_Type.UPDATE
                    Dim _excSQL As New DBType_SQLServer
                    _excSQL.DBExeNonQuery(String.Format("Update tb_SalesOrder set Credit_Term = {0},Invoice_No='{2}',PO_No='{3}' where SalesOrder_Index = '{1}'", CInt(Me.txtCreditTerm.Text), Me.SalesOrder_Index, Me.txtInvoice_No.Text, Me.txtPO_No.Text))
                    Dim obj_cls As New cls_syAditlog
                    obj_cls.Process_ID = 10
                    obj_cls.Description = "update Credit_Term,Invoice_No,PO_No = " & Me.txtCreditTerm.Text.ToString & "," & Me.SalesOrder_Index & "," & Me.txtInvoice_No.Text & "," & Me.txtPO_No.Text
                    obj_cls.Document_Index = Me.SalesOrder_Index
                    obj_cls.Document_No = Me.txtSO_No.Text
                    obj_cls.Log_Type_ID = "1005"
                    If obj_cls.Insert_Master() Then
                        W_MSG_Information("บันทึกเรียบร้อย")
                    End If
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_MouseHover(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCreditTrem.MouseHover
        Try
            If lblCreditTerm.BackColor = Color.Transparent Then
                lblCreditTerm.BackColor = Color.Red
                Me.lblInvoice_No.BackColor = Color.Red
                Me.lblPO_No.BackColor = Color.Red
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_MouseLeave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateCreditTrem.MouseLeave
        Try
            lblCreditTerm.BackColor = Color.Transparent
            Me.lblInvoice_No.BackColor = Color.Transparent
            Me.lblPO_No.BackColor = Color.Transparent
        Catch ex As Exception
            W_MSG_Error(ex.Message())
        End Try

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Me.Status = 1 Then
                W_MSG_Information("กรุณายืนยันเอกสาร")
                Exit Sub
            End If
            If Me._SalesOrder_Index <> "" Then
                Dim objSOTransaction As New cls_KSL
                Dim dtStock As New DataTable
                dtStock = objSOTransaction.getCheckStock_Sales(Me._SalesOrder_Index, Me._Customer_Index, Me.cboDistributionCenter.SelectedValue.ToString)
                If dtStock.Rows.Count > 0 Then
                    objSOTransaction.ResetColor_Sales(dtStock, Me._SalesOrder_Index)

                    Dim frm As New frmSO_CheckStock
                    frm.SalesOrder_No = Me.txtSO_No.Text
                    frm.SalesOrder_Index = Me.SalesOrder_Index
                    frm.Status = Me.Status
                    frm.DataGridView1.AutoGenerateColumns = False
                    frm.DataGridView1.DataSource = dtStock
                    frm.ShowDialog()
                    Me.Status = frm.Status

                    'Dim frm As New frmSO_CheckStock
                    'frm.SalesOrder_No = Me.txtSO_No.Text
                    'frm.DataGridView1.AutoGenerateColumns = False
                    'frm.DataGridView1.DataSource = dtStock
                    'frm.ShowDialog()
                End If

                Me.getSODetail(SalesOrder_Index)
                W_MSG_Information("ปรับสีเอกสาร เสร็จสิ้น")
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message())
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim objSOTransaction As New cls_KSL
            Dim dtStock As New DataTable
            dtStock = objSOTransaction.getCheckStock_Sales(Me._SalesOrder_Index, Me._Customer_Index, Me.cboDistributionCenter.SelectedValue.ToString)
            If dtStock.Rows.Count > 0 Then
                'objSOTransaction.ResetColor_Sales(dtStock, Me._SalesOrder_Index)
                Dim frm As New frmSO_CheckStock
                frm.SalesOrder_No = Me.txtSO_No.Text
                frm.SalesOrder_Index = Me.SalesOrder_Index
                frm.Status = Me.Status
                frm.DataGridView1.AutoGenerateColumns = False
                frm.DataGridView1.DataSource = dtStock
                frm.ShowDialog()
                If Me.Status <> frm.Status Then
                    Me.getSODetail(SalesOrder_Index)
                End If
                Me.Status = frm.Status

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message())
        End Try
    End Sub
End Class
