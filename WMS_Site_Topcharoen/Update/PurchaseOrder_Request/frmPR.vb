Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language

Public Class frmPR

#Region " Enum Operation Type "

    Public Enum enuOperation_Type
        [ADDNEW]
        [UPDATE]
    End Enum

#End Region

#Region " Property "

    Private Const Process_Id As Integer = 91
    Private Edit_Date_S As Date
    Private Default_Customer_Index As String = ""
    Private USE_PRODUCT_CUSTOMER As Boolean = False
    Private listRemove As List(Of String)

    Public OperationType As enuOperation_Type

    Private _IsProcess As Boolean = False
    Public ReadOnly Property IsProcess() As Boolean
        Get
            Return _IsProcess
        End Get
    End Property

    Private _PurchaseOrder_Request_Index As String = ""
    Public WriteOnly Property PurchaseOrder_Request_Index() As String
        Set(ByVal value As String)
            _PurchaseOrder_Request_Index = value
        End Set
    End Property

    Public Sub New(ByVal operationType As enuOperation_Type)
        InitializeComponent()
        Me.OperationType = operationType
    End Sub

#End Region

#Region " Load "

    Private Sub frmPR_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")
            Me.defaultOnLoad()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub defaultOnLoad()
        Try
            ''Dim oFunction As New WMS_STD_Master.W_Language
            ''oFunction.SwitchLanguage(Me, 91)
            ''oFunction.SW_Language_Column(Me, Me.dgvPR, 91)
            ''oFunction = Nothing

            ' Default Date
            Me.dtpPurchaseOrder_Request_Date.Value = Today
            Me.dtpDue_Date.Value = Today

            ' CheckBox Due_Date
            Me.dtpDue_Date.Checked = False

            ' ซ่อน(TAB)
            Me.tcoHeader.TabPages.Remove(Me.tpaHide)

            ' ComboBox
            Me.getCboDocumentType(Me._PurchaseOrder_Request_Index)
            Me.getCboPrint()

            ' DGV
            Me.dgvPRItem.AutoGenerateColumns = False
            Me.dgvPRItem_PO.AutoGenerateColumns = False

            ' Default_Customer_Index
            Me.Default_Customer_Index = New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH).GetUserByCustomerDefault()
            If (Not Me.Default_Customer_Index = Nothing) Then
                Me.getCustomer(Me.Default_Customer_Index)
            End If

            ' Config
            Me.USE_PRODUCT_CUSTOMER = New config_CustomSetting().getConfig_Key_USE("USE_PRODUCT_CUSTOMER")

            ' Get Data
            Dim _dt As DataTable = New clsPR().Query(" select getdate() as Edit_Date_S ")
            Me.Edit_Date_S = CDate(_dt.Rows(0).Item("Edit_Date_S"))
            Me.listRemove = New List(Of String)
            Me.getPurchaseOrder_Request_H(Me._PurchaseOrder_Request_Index)
            Me.getPurchaseOrder_Request_D(Me._PurchaseOrder_Request_Index)
            Me.getPOItem_PurchaseOrder_Request(Me._PurchaseOrder_Request_Index)
            Me.setItem_Seq(Me.dgvPRItem)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPopupCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopupCustomer.Click
        Try
            If (USE_PRODUCT_CUSTOMER) Then
                For i As Integer = 0 To Me.dgvPRItem.Rows.Count - 2
                    If (Me.dgvPRItem.Rows(i).Cells("col_Sku_Id").Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.dgvPRItem.Rows(i).Cells("col_Sku_Id").Tag.ToString())) Then
                        MessageBox.Show(String.Format("กรุณาลบรายการก่อนเปลี่ยน{0}", Me.lblCustomer.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                Next
            End If

            Dim _frm As New frmCustmer_Popup()
            _frm.ShowDialog()
            If (Not _frm.Customer_Index = Nothing) Then
                Me.getCustomer(_frm.Customer_Index)
            End If
            _frm.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " ComboBox "

    Private Sub getCboDocumentType(Optional ByVal PurchaseOrder_Request_Index As String = "")
        Try
            Dim _dt As DataTable
            If (Not PurchaseOrder_Request_Index = Nothing) Then
                _dt = New clsPR().Query(String.Format(" select DocumentType_Index,Description as DocumentType_Desc from ms_DocumentType where Process_Id={0} and (status_id<>-1 or DocumentType_Index in (select DocumentType_Index from tb_PurchaseOrder_Request where PurchaseOrder_Request_Index='{1}')) order by Description asc ", Process_Id, PurchaseOrder_Request_Index))
            Else
                _dt = New clsPR().Query(String.Format(" select DocumentType_Index,Description as DocumentType_Desc from ms_DocumentType where Process_Id={0} and status_id<>-1 order by Description asc ", Process_Id))
            End If
            With Me.cboDocumentType
                .DisplayMember = "DocumentType_Desc"
                .ValueMember = "DocumentType_Index"
                .DataSource = _dt
                .Enabled = (.Items.Count > 0)
                If (.Items.Count > 0) Then
                    .SelectedIndex = 0
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getCboPrint()
        Try
            With Me.cboPrint
                .DisplayMember = "Report_Desc"
                .ValueMember = "Report_Name"
                .DataSource = New clsPR().Query(String.Format(" select Report_Name,Description as Report_Desc from config_Report where Process_Id={0} and status_id<>-1 and IsVisible=1 order by Seq asc ", Process_Id))
                .Enabled = (.Items.Count > 0)
                If (.Items.Count > 0) Then
                    .SelectedIndex = 0
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " Tab PRItem "

    Private Sub setItem_Seq(ByVal dgv As DataGridView)
        Try
            For i As Integer = 0 To dgv.RowCount - 2
                dgv.Rows(i).Cells("col_Item_Seq").Value = i + 1
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dgvPRItem_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvPRItem.RowsAdded
        Try
            Me.setItem_Seq(dgvPRItem)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvPRItem_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvPRItem.RowsRemoved
        Try
            Me.setItem_Seq(dgvPRItem)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvPRItem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPRItem.CellClick
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (e.ColumnIndex <= -1 Or e.RowIndex <= -1) Then
                Exit Sub
            End If

            Dim _dgv As DataGridView = CType(sender, DataGridView)
            With _dgv
                If (.RowCount < 0) Then Exit Sub
                Select Case _dgv.CurrentCell.OwningColumn.Name
                    Case "col_btnPopupSku"
                        If (Me.dgvPRItem.ReadOnly) Then
                            Exit Sub
                        End If
                        If (Me.USE_PRODUCT_CUSTOMER AndAlso Me.txtCustomer_Id.Tag = Nothing) Then
                            MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.lblCustomer.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            Exit Sub
                        End If

                        If (.Rows(e.RowIndex).Cells("col_btnPopupSku").Tag <> "NULL") Then
                            Dim _frmPopup As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me.txtCustomer_Id.Tag)
                            _frmPopup.ShowDialog()
                            If (Not _frmPopup.Sku_ID = Nothing) Then
                                If (.CurrentCell.RowIndex = .NewRowIndex()) Then
                                    .Rows.Add()
                                End If
                                .Rows(e.RowIndex).Cells("col_Sku_Id").Value = _frmPopup.Sku_ID
                                .Rows(e.RowIndex).Cells("col_Due_Date").Value = Today
                                If (.Rows(e.RowIndex).Cells("col_Qty").Visible) Then
                                    .CurrentCell = .Rows(e.RowIndex).Cells("col_Qty")
                                Else
                                    .CurrentCell = .Rows(e.RowIndex).Cells("col_btnPopupSku")
                                End If
                            End If
                            _frmPopup.Close()
                        End If
                End Select
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvPRItem_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPRItem.CellValueChanged
        Try
            If (e.ColumnIndex <= -1 Or e.RowIndex <= -1) Then
                Exit Sub
            End If

            Dim _dgv As DataGridView = CType(sender, DataGridView)
            With _dgv
                If (.RowCount < 0) Then Exit Sub
                Select Case .Columns(e.ColumnIndex).Name
                    Case "col_Sku_Id"
                        Dim oSession As New WMS_STD_Master.UserSession
                        Dim oBolCheck As Boolean = oSession.CheckSession
                        If oBolCheck = False Then
                            Exit Sub
                        End If

                        If (Me.USE_PRODUCT_CUSTOMER AndAlso Me.txtCustomer_Id.Tag = Nothing) Then
                            MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.lblCustomer.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            .Rows(e.RowIndex).Cells("col_Sku_Id").Value = ""
                            Exit Sub
                        End If
                        Dim _Sku_Id As String = .Rows(e.RowIndex).Cells("col_Sku_Id").Value
                        Me.getSku(_Sku_Id, e.RowIndex)
                    Case "col_Qty", "col_Package"
                        Dim _dgvcbo As DataGridViewComboBoxCell
                        Try
                            _dgvcbo = .Rows(e.RowIndex).Cells("col_Package")
                        Catch ex As Exception
                            Exit Select
                        End Try
                        If (Not IsNumeric(.Rows(e.RowIndex).Cells("col_Qty").Value)) Then
                            .Rows(e.RowIndex).Cells("col_Qty").Value = "0"
                        End If

                        Dim _dtSku_Package As DataTable = DirectCast(_dgvcbo.DataSource, DataTable)
                        Dim _drArr() As DataRow = _dtSku_Package.Select(String.Format(" Package_Index='{0}' ", _dgvcbo.Value))
                        If (_drArr.Length = 0) Then
                            Exit Select
                        End If
                        Dim _ratio As Decimal = 0
                        Decimal.TryParse(_drArr(0).Item("Ratio").ToString(), _ratio)
                        Dim _qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Qty").Value, _qty)
                        Decimal.TryParse(_qty.ToString(Me.col_Qty.DefaultCellStyle.Format), _qty)
                        .Rows(e.RowIndex).Cells("col_Qty").Value = _qty
                        Dim _total_Qty As Decimal = _ratio * _qty
                        Dim _total_Received_Qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Total_Received_Qty").Value, _total_Received_Qty)
                        Dim _weight As Decimal = 0
                        Decimal.TryParse(_drArr(0).Item("Weight").ToString(), _weight)
                        Decimal.TryParse((_weight * _qty).ToString("0.######"), _weight)
                        Dim _volume As Decimal = 0
                        Decimal.TryParse(_drArr(0).Item("Volume").ToString(), _volume)
                        Decimal.TryParse((_volume * _qty).ToString("0.######"), _volume)
                        .Rows(e.RowIndex).Cells("col_Total_Qty").Value = _total_Qty
                        .Rows(e.RowIndex).Cells("col_Total_Received_Qty").Value = _total_Received_Qty
                        .Rows(e.RowIndex).Cells("col_Total_Pending_Qty").Value = _total_Qty - _total_Received_Qty
                        .Rows(e.RowIndex).Cells("col_Weight").Value = _weight
                        .Rows(e.RowIndex).Cells("col_Volume").Value = _volume

                End Select
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvPRItem_CellMouseEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPRItem.CellMouseEnter
        Try
            Me.dgvPRItem.ShowCellToolTips = (e.ColumnIndex <> Me.dgvPRItem.Columns("col_btnPopupSku").Index)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvPRItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvPRItem.EditingControlShowing
        Try
            If (Me.dgvPRItem.Columns(Me.dgvPRItem.CurrentCell.ColumnIndex).Name = "col_Qty") Then
                RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Qty_Keypress
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Qty_Keypress
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub col_Qty_Keypress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Try
            If Me.dgvPRItem.CurrentCell.ColumnIndex = Me.dgvPRItem.Columns("col_Qty").Index Then
                If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = "." Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = ChrW(Keys.Delete)) Then e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRemovePRItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePRItem.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (Me.dgvPRItem.CurrentRow Is Nothing) Then Exit Sub
            If (Me.dgvPRItem.CurrentCell.RowIndex = Me.dgvPRItem.NewRowIndex()) Then Exit Sub
            Dim _row_Index As Integer = Me.dgvPRItem.CurrentRow.Index

            If (Me.dgvPRItem.Rows(_row_Index).Cells("col_PurchaseOrder_RequestItem_Index").Value = Nothing) Then
                Me.dgvPRItem.Rows.RemoveAt(_row_Index)
                Exit Sub
            End If

            '' Dialog Confirm
            'Dim _result As Integer = MessageBox.Show(String.Format("คุณต้องการลบรายการลำดับ {0} หรือไม่?", Me.dgvPRItem.Rows(_row_Index).Cells("col_Item_Seq").Value), "ลบรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            'If (_result = DialogResult.No) Then Exit Sub
            Me.listRemove.Add(Me.dgvPRItem.Rows(_row_Index).Cells("col_PurchaseOrder_RequestItem_Index").Value)
            Me.dgvPRItem.Rows.RemoveAt(_row_Index)

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnInsertPRItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInsertPRItem.Click
        Try
            If (Me.dgvPRItem.CurrentRow Is Nothing) Then Exit Sub
            If (Me.dgvPRItem.CurrentCell.RowIndex = Me.dgvPRItem.NewRowIndex()) Then Exit Sub
            Me.dgvPRItem.Rows.Insert(Me.dgvPRItem.CurrentRow.Index)
            Me.setItem_Seq(Me.dgvPRItem)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Get Data "

    Private Sub getPurchaseOrder_Request_H(ByVal PurchaseOrder_Request_Index As String)
        Try
            If (PurchaseOrder_Request_Index = Nothing) Then Exit Sub
            Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request_View(String.Format(" and PurchaseOrder_Request_Index='{0}' ", PurchaseOrder_Request_Index))
            If (_dt.Rows.Count > 0) Then
                With _dt.Rows(0)
                    Me.txtPurchaseOrder_Request_No.Tag = .Item("PurchaseOrder_Request_Index").ToString()
                    Me.txtPurchaseOrder_Request_No.Text = .Item("PurchaseOrder_Request_No").ToString()
                    If (IsDate(.Item("PurchaseOrder_Request_Date"))) Then
                        Me.dtpPurchaseOrder_Request_Date.Value = CDate(.Item("PurchaseOrder_Request_Date"))
                    End If
                    Me.cboDocumentType.SelectedValue = .Item("DocumentType_Index").ToString()
                    Me.txtStatus_Desc.Tag = .Item("Status").ToString()
                    Me.txtStatus_Desc.Text = .Item("Status_Desc").ToString()
                    Me.getCustomer(.Item("Customer_Index").ToString())
                    Me.txtRef_No1.Text = .Item("Ref_No1").ToString()
                    Me.txtRef_No2.Text = .Item("Ref_No2").ToString()
                    Me.txtUser.Text = .Item("add_by").ToString()
                    If (IsDate(.Item("Due_Date"))) Then
                        Me.dtpDue_Date.Checked = True
                        Me.dtpDue_Date.Value = CDate(.Item("Due_Date"))
                    Else
                        Me.dtpDue_Date.Checked = False
                        Me.dtpDue_Date.Value = Today
                    End If
                    Me.txtRemark.Text = .Item("Remark").ToString()

                    Me.txtPurchaseOrder_Request_No.ReadOnly = True

                    Me.btnClose_PR.Enabled = Not CBool(.Item("IsClosed"))
                    Me.btnConfirm.Enabled = Not CBool(.Item("IsConfirm"))
                    Me.btnPrint.Enabled = True

                    Dim _IsEnableEditor As Boolean = Me.btnConfirm.Enabled
                    Me.btnSave.Enabled = _IsEnableEditor

                    ' tpaHeader
                    Me.dtpPurchaseOrder_Request_Date.Enabled = _IsEnableEditor
                    Me.btnPopupCustomer.Enabled = _IsEnableEditor
                    Me.txtRef_No1.Enabled = _IsEnableEditor
                    Me.txtRef_No2.Enabled = _IsEnableEditor
                    Me.dtpDue_Date.Enabled = _IsEnableEditor
                    Me.txtRemark.Enabled = _IsEnableEditor

                    ' tpaPRItem
                    Me.dgvPRItem.ReadOnly = Not _IsEnableEditor
                    Me.btnRemovePRItem.Enabled = _IsEnableEditor
                    Me.btnInsertPRItem.Enabled = _IsEnableEditor

                End With
            Else
                MessageBox.Show("ไม่พบใบคำขอสั่งซื้อ", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Me.Close()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getPurchaseOrder_Request_D(ByVal PurchaseOrder_Request_Index As String)
        Try
            If (PurchaseOrder_Request_Index = Nothing) Then Exit Sub
            Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request(String.Format(" and PurchaseOrder_Request_Index='{0}' ", PurchaseOrder_Request_Index))
            Me.dgvPRItem.Rows.Clear()
            For iRow As Integer = 0 To _dt.Rows.Count - 1
                Dim _dr As DataRow = _dt.Rows(iRow)
                With Me.dgvPRItem
                    .Rows.Add()
                    .Rows(iRow).Cells("col_PurchaseOrder_RequestItem_Index").Value = _dr("PurchaseOrder_RequestItem_Index")
                    .Rows(iRow).Cells("col_Item_Seq").Value = _dr("Item_Seq")
                    .Rows(iRow).Cells("col_Sku_Id").Value = _dr("Sku_Id")
                    .Rows(iRow).Cells("col_Sku_Id").Tag = _dr("Sku_Index")
                    .Rows(iRow).Cells("col_Sku_Desc").Value = _dr("Sku_Desc")
                    .Rows(iRow).Cells("col_ProductType_Desc").Value = _dr("ProductType_Desc")
                    .Rows(iRow).Cells("col_Qty").Value = _dr("Qty")
                    .Rows(iRow).Cells("col_Package").Value = _dr("Package_Index")
                    .Rows(iRow).Cells("col_Package").Tag = _dr("Package_Index")
                    .Rows(iRow).Cells("col_Weight").Value = _dr("Weight")
                    .Rows(iRow).Cells("col_Volume").Value = _dr("Volume")
                    .Rows(iRow).Cells("col_Total_Qty").Value = _dr("Total_Qty")
                    .Rows(iRow).Cells("col_Total_Received_Qty").Value = _dr("Total_Received_Qty")
                    .Rows(iRow).Cells("col_Total_Pending_Qty").Value = _dr("Total_Pending_Qty")
                    .Rows(iRow).Cells("col_Remark").Value = _dr("Remark")

                    If (IsDate(_dr("Due_Date"))) Then
                        .Rows(iRow).Cells("col_Due_Date").Value = _dr("Due_Date")
                    Else
                        .Rows(iRow).Cells("col_Due_Date").Value = Today
                    End If


                End With
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getPOItem_PurchaseOrder_Request(ByVal PurchaseOrder_Request_Index As String)
        Try
            If (PurchaseOrder_Request_Index = Nothing) Then Exit Sub
            Dim _dt As DataTable = New clsPR().getPurchaseOrder_RequestItem_PO(PurchaseOrder_Request_Index)
            Me.dgvPRItem_PO.DataSource = _dt
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getCustomer(ByVal Customer_Index As String)
        Dim oCustomer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim _dt As DataTable = New DataTable
        Try
            oCustomer.getPopup_Customer("Customer_Index", Customer_Index)
            _dt = oCustomer.DataTable
            If _dt.Rows.Count > 0 Then
                Me.txtCustomer_Id.Tag = _dt.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = _dt.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = _dt.Rows(0).Item("Customer_Name").ToString
            Else
                Me.txtCustomer_Id.Tag = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oCustomer = Nothing
            _dt = Nothing
        End Try
    End Sub

    Private Sub getSku(ByVal Sku_Id As String, ByVal RowIndex As Integer)
        Try
            Dim oSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            oSku.getSKU_Detail(Sku_Id)
            Dim _dt As DataTable = oSku.DataTable
            Dim _strSku_Index As String = ""
            If (_dt.Rows.Count > 0) Then
                With _dt.Rows(0)
                    If (Me.USE_PRODUCT_CUSTOMER) Then
                        If (Not .Item("Customer_Index").ToString() = Nothing) Then
                            'If (Not .Item("Customer_Index").ToString <> Me.txtCustomer_Id.Tag) Then
                            If (.Item("Customer_Index").ToString <> Me.txtCustomer_Id.Tag) Then
                                MessageBox.Show(String.Format("สินค้ามี{0}ไม่ตรงกับที่ระบุ", Me.lblCustomer.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Id").Tag = ""
                                Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Id").Value = ""
                                Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Desc").Value = ""
                                Me.dgvPRItem.Rows(RowIndex).Cells("col_ProductType_Desc").Value = ""
                                Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Tag = ""
                                Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Value = ""
                                Exit Sub
                            End If
                        End If
                    End If
                    Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Id").Tag = .Item("Sku_Index").ToString()
                    Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Desc").Value = .Item("Str1").ToString()
                    Me.dgvPRItem.Rows(RowIndex).Cells("col_ProductType_Desc").Value = .Item("ProductType").ToString()
                    Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Tag = .Item("Package_Index").ToString()
                    Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Value = .Item("Package_Index").ToString()
                    _strSku_Index = Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Id").Tag
                End With
            Else
                Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Id").Tag = ""
                Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Id").Value = ""
                Me.dgvPRItem.Rows(RowIndex).Cells("col_Sku_Desc").Value = ""
                Me.dgvPRItem.Rows(RowIndex).Cells("col_ProductType_Desc").Value = ""
                Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Tag = ""
                Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Value = ""
                Exit Sub
            End If

            If (Not _strSku_Index = Nothing) Then
                Me.getSku_Package(_strSku_Index, RowIndex)
                Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Tag = Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Value
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getSku_Package(ByVal Sku_Index As String, ByVal RowIndex As Integer)
        Try
            Dim _dt As DataTable = New clsPR().getSKU_Package(Sku_Index)
            Dim dgvcbo As New DataGridViewComboBoxCell()
            dgvcbo.DisplayMember = "Package"
            dgvcbo.ValueMember = "Package_Index"
            dgvcbo.DataSource = _dt
            Me.dgvPRItem.Rows(RowIndex).Cells("col_Package") = dgvcbo
            Me.dgvPRItem.Rows(RowIndex).Cells("col_Package").Value = _dt.Rows(0).Item("Package_Index").ToString()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function getSKU_PackageRatio(ByVal Sku_Index As String, ByVal Package_Index As String) As Decimal
        Dim objClassDB As New ms_SKURatio(ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim intRatio As Integer = 0
        Try
            objClassDB.SelectData_ByPackage(Sku_Index, Package_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Integer.TryParse(objDT.Rows(0).Item("Ratio").ToString(), intRatio)
            End If
        Catch ex As Exception
            Throw ex
        Finally
            getSKU_PackageRatio = intRatio
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Function

#End Region

#Region " Save "

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            ' Validate
            If (Not validateSave()) Then Exit Sub

            ' Save
            Dim Temp_Index As String = Me.save()
            If (Not Temp_Index = Nothing) Then
                Me.OperationType = enuOperation_Type.UPDATE
                Me._IsProcess = True
                Me._PurchaseOrder_Request_Index = Temp_Index
                MessageBox.Show(String.Format("บันทึกข้อมูลเรียบร้อย", Me.lblDocumentType.Text), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Try
                    Me.defaultOnLoad()
                Catch ex As Exception
                    MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Me.Close()
                End Try
            Else
                MessageBox.Show(String.Format("ไม่สามารถบันทึกข้อมูลได้, กรุณาลองอีกครั้ง", Me.lblDocumentType.Text), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function validateSave() As Boolean
        Try
            ' Header
            Select Case OperationType
                Case enuOperation_Type.ADDNEW
                    Dim objcon As New DBType_SQLServer
                    Dim dt As New DataTable
                    If objcon.DBExeQuery_Scalar(String.Format("select count(*) from tb_PurchaseOrder_Request where PurchaseOrder_Request_No = '{0}' and Status not in (-1) ", Me.txtPurchaseOrder_Request_No.Text)) > 0 Then
                        MessageBox.Show("เลขที่เอกสารซ้ำ", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        objcon = Nothing
                        Return False
                    End If
            End Select

            If (Not Me.txtPurchaseOrder_Request_No.Tag = Nothing) Then
                Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request_View(String.Format(" and PurchaseOrder_Request_Index='{0}' ", Me.txtPurchaseOrder_Request_No.Tag))
                If (_dt.Rows.Count > 0) Then
                    With _dt.Rows(0)
                        If (IsDate(.Item("Last_Modified_Date"))) Then
                            If (Edit_Date_S < CDate(.Item("Last_Modified_Date"))) Then
                                MessageBox.Show("ไม่สามารถแก้ไขได้, ข้อมูลมีการเปลี่ยนแปลงระหว่างแก้ไขข้อมูล", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                Try
                                    Me.defaultOnLoad()
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Me.Close()
                                End Try
                                Return False
                            End If
                        End If
                    End With
                End If
            End If
            If (Me.cboDocumentType.SelectedValue Is Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.lblDocumentType.Text), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return False
            End If
            If (Me.txtCustomer_Id.Tag = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.lblCustomer.Text), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return False
            End If

            ' Detail
            For iRow As Integer = 0 To Me.dgvPRItem.Rows.Count - 2
                Dim _dgvr As DataGridViewRow = Me.dgvPRItem.Rows(iRow)
                'For Each _dgvr As DataGridViewRow In Me.dgvPRItem.Rows
                If (_dgvr.Cells("col_Sku_Id").Tag = Nothing) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบรหัสสินค้า, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (_dgvr.Cells("col_Package").Tag = Nothing) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบหน่วยสินค้า, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (Not IsNumeric(_dgvr.Cells("col_Qty").Value)) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ จำนวนสั่งซื้อต้องเป็นตัวเลขเท่านั้น, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (CDec(_dgvr.Cells("col_Qty").Value) <= 0) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ จำนวนสั่งซื้อต้องมากกว่าศูนย์, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                Dim _dgvcbo As New DataGridViewComboBoxCell
                _dgvcbo = _dgvr.Cells("col_Package")
                Dim _dtSku_Package As DataTable = DirectCast(_dgvcbo.DataSource, DataTable)
                Dim _drArr() As DataRow = _dtSku_Package.Select(String.Format(" Package_Index='{0}' ", _dgvcbo.Value))
                If (_drArr.Length = 0) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบหน่วย, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                Dim _ratio As Decimal = 0
                If (Not Decimal.TryParse(_drArr(0).Item("Ratio").ToString(), _ratio)) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบ Ratio, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                'Dim _total_Received_Qty As Decimal = 0
                'Decimal.TryParse(_dgvr.Cells("col_Total_Received_Qty").Value, _total_Received_Qty)
                'If (_total_Received_Qty > (CDec(_dgvr.Cells("col_Qty").Value) * _ratio)) Then
                '    _dgvr.Selected = True
                '    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ เนื่องจากจำนวนที่สั่งซื้อน้อยกว่าจำนวนที่รับไปแล้ว, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                '    Return False
                'End If

                If (Not _dgvr.Cells("col_PurchaseOrder_RequestItem_Index").Value = Nothing) Then
                    Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request(String.Format(" and PurchaseOrder_RequestItem_Index='{0}' ", _dgvr.Cells("col_PurchaseOrder_RequestItem_Index").Value))
                    If (_dt.Rows.Count > 0) Then
                        Dim _total_Received_Qty As Decimal = _dt.Compute(" sum(Total_Received_Qty) ", "")
                        _dgvr.Cells("col_Total_Received_Qty").Value = _total_Received_Qty
                        _dgvr.Cells("col_Total_Pending_Qty").Value = CDec(_dgvr.Cells("col_Total_Qty").Value) - _total_Received_Qty
                        If (_total_Received_Qty > (CDec(_dgvr.Cells("col_Qty").Value) * _ratio)) Then
                            _dgvr.Selected = True
                            MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ เนื่องจากจำนวนที่สั่งซื้อน้อยกว่าจำนวนที่รับไปแล้ว, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            Return False
                        End If
                    End If
                End If

            Next

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function save() As String
        Dim Temp_Index As String = ""
        Try
            Dim _dtH As DataTable = New clsPR().getDtSchemaPurchaseOrder_Request()
            Dim _dtD As DataTable = New clsPR().getDtSchemaPurchaseOrder_RequestItem()

            '--------------------------------------------------------------------
            ' Header
            '--------------------------------------------------------------------
            Dim _drH As DataRow = _dtH.NewRow()
            _drH("PurchaseOrder_Request_Index") = Me.txtPurchaseOrder_Request_No.Tag
            _drH("PurchaseOrder_Request_No") = Me.txtPurchaseOrder_Request_No.Text.Trim()
            _drH("PurchaseOrder_Request_Date") = Me.dtpPurchaseOrder_Request_Date.Value

            If (Me.dtpDue_Date.Checked) Then
                _drH("Due_Date") = Me.dtpDue_Date.Value
            Else
                _drH("Due_Date") = DBNull.Value
            End If

            _drH("DocumentType_Index") = Me.cboDocumentType.SelectedValue
            _drH("Customer_Index") = Me.txtCustomer_Id.Tag
            _drH("Ref_No1") = Me.txtRef_No1.Text.Trim()
            _drH("Ref_No2") = Me.txtRef_No2.Text.Trim()
            '_drH("Ref_No3") = DBNull.Value
            '_drH("Ref_No4") = DBNull.Value
            '_drH("Ref_No5") = DBNull.Value
            _drH("Remark") = Me.txtRemark.Text.Trim()
            '_drH("IsClosed") = ""
            '_drH("Closed_By") = ""
            '_drH("Closed_Date") = ""
            '_drH("IsConfirm") = ""
            '_drH("Confirm_By") = ""
            '_drH("Confirm_Date") = ""
            '_drH("IsCanCancel") = ""
            '_drH("Status") = ""
            _dtH.Rows.Add(_drH)

            '--------------------------------------------------------------------
            ' Detail
            '--------------------------------------------------------------------
            For iRow As Integer = 0 To Me.dgvPRItem.Rows.Count - 2
                With Me.dgvPRItem.Rows(iRow)
                    Dim _drD As DataRow = _dtD.NewRow()
                    _drD("PurchaseOrder_RequestItem_Index") = .Cells("col_PurchaseOrder_RequestItem_Index").Value
                    _drD("PurchaseOrder_Request_Index") = Me.txtPurchaseOrder_Request_No.Tag
                    _drD("Item_Seq") = .Cells("col_Item_Seq").Value
                    _drD("Sku_Index") = .Cells("col_Sku_Id").Tag

                    Dim _dgvcbo As New DataGridViewComboBoxCell
                    _dgvcbo = .Cells("col_Package")
                    _drD("Package_Index") = _dgvcbo.Value.ToString()

                    '_drD("Serial_No") = DBNull.Value

                    Dim _qty As Decimal = 0
                    Decimal.TryParse(.Cells("col_Qty").Value, _qty)
                    Decimal.TryParse(_qty.ToString(Me.col_Qty.DefaultCellStyle.Format), _qty)
                    .Cells("col_Qty").Value = _qty
                    _drD("Qty") = _qty

                    Dim _ratio As Decimal = Me.getSKU_PackageRatio(_drD("Sku_Index").ToString(), _drD("Package_Index").ToString())
                    _drD("Ratio") = _ratio

                    _drD("Total_Qty") = _qty * _ratio
                    _drD("Weight") = .Cells("col_Weight").Value
                    _drD("Volume") = .Cells("col_Volume").Value
                    _drD("Total_Received_Qty") = .Cells("col_Total_Received_Qty").Value
                    '_drD("Ref_No1") = ""
                    '_drD("Ref_No2") = ""
                    '_drD("Ref_No3") = ""
                    '_drD("Ref_No4") = ""
                    '_drD("Ref_No5") = ""
                    _drD("Remark") = .Cells("col_Remark").Value
                    '_drD("Status") = ""
                    _drD("Due_Date") = .Cells("col_Due_Date").Value
                    _dtD.Rows.Add(_drD)
                End With
            Next

            '--------------------------------------------------------------------
            ' Save
            '--------------------------------------------------------------------
            Temp_Index = New clsPR().savePurchaseOrder_Request(_dtH, _dtD, Me.listRemove, Me.Edit_Date_S)

        Catch ex As Exception
            Throw ex
        End Try
        Return Temp_Index
    End Function

#End Region

#Region " Close PR "

    Private Sub btnClose_PR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose_PR.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (New clsPR().canClosePurchaseOrder_Request(Me.txtPurchaseOrder_Request_No.Tag)) Then
                Dim _result As Windows.Forms.DialogResult = MessageBox.Show(String.Format("คุณต้องการปิดเอกสารเลขที่ {0} หรือไม่?", Me.txtPurchaseOrder_Request_No.Text.Trim()), "ปิดเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
                If (New clsPR().canClosePurchaseOrder_Request(Me.txtPurchaseOrder_Request_No.Tag)) Then
                    If (New clsPR().closePurchaseOrder_Request(Me.txtPurchaseOrder_Request_No.Tag)) Then
                        Me._IsProcess = True
                        MessageBox.Show(String.Format("ปิดเอกสารเลขที่ {0} เรียบร้อย", Me.txtPurchaseOrder_Request_No.Text), "ปิดเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

#End Region

#Region " Confirm "

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (New clsPR().canConfirmPurchaseOrder_Request(Me.txtPurchaseOrder_Request_No.Tag)) Then
                Dim _result As Windows.Forms.DialogResult = MessageBox.Show(String.Format("คุณต้องการยืนยันเอกสารเลขที่ {0} หรือไม่?", Me.txtPurchaseOrder_Request_No.Text.Trim()), "ยืนยันเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
                If (New clsPR().canConfirmPurchaseOrder_Request(Me.txtPurchaseOrder_Request_No.Tag)) Then
                    If (New clsPR().confirmPurchaseOrder_Request(Me.txtPurchaseOrder_Request_No.Tag)) Then
                        Me._IsProcess = True
                        MessageBox.Show(String.Format("ยืนยันเอกสารเลขที่ {0} เรียบร้อย", Me.txtPurchaseOrder_Request_No.Text), "ยืนยันเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Me.Close()
                    Else
                        MessageBox.Show(String.Format("เกิดข้อผิดพลาดทางข้อมูล, กรุณาลองอีกครั้ง", Me.lblDocumentType.Text), "ยืนยันเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    End If
                End If
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If Me.txtPurchaseOrder_Request_No.Text.Trim = "" Then Exit Sub
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

            Try

                Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, "And PurchaseOrder_Request_Index ='" & Me._PurchaseOrder_Request_Index & "'")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
            Catch ex As Exception
                W_MSG_Error("เลือกรายการที่จะพิมพ์ก่อน")

            Finally

            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
