Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer

Public Class frmPO_PR_Import

#Region " Property "

    Private Const Process_Id As Integer = 91
    Private USE_PRODUCT_CUSTOMER As Boolean = False
    Private _PurchaseOrder_Index As String = ""
    Private _PurchaseOrder_PR_Index As String = ""

    Private _IsProcess As Boolean = False
    Public ReadOnly Property IsProcess() As Boolean
        Get
            Return _IsProcess
        End Get
    End Property

    Public Sub New(ByVal PurchaseOrder_Index As String)
        InitializeComponent()
        Me._PurchaseOrder_Index = PurchaseOrder_Index
    End Sub

#End Region

#Region " DGV HeaderCheckBox "

    Private WithEvents HeaderCheckBox1 As New CheckBox
    Private WithEvents CheckBoxHeaderCell1 As New DataGridViewCheckBoxHeaderCell

    Private Sub CheckBoxHeaderCell_CheckBoxClicked(ByVal sender As Object, ByVal e As DataGridViewCheckBoxHeaderCellEventArgs) Handles CheckBoxHeaderCell1.CheckBoxClicked
        sender.CheckUncheckEntireColumn(e.Checked)
    End Sub

    Private Sub dgvPR_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList_PR.CellContentClick
        If e.ColumnIndex = CheckBoxHeaderCell1.ColumnIndex Then
            CheckBoxHeaderCell1.RefreshCheckState()
            If (Not Me.dgvList_PR.Columns(e.ColumnIndex).Name = "col_IsSelected") Then Exit Sub
            If (TypeOf (Me.dgvList_PR.Rows(e.RowIndex).Cells(e.ColumnIndex)) Is DataGridViewCheckBoxCell) Then
                Dim dgvchk As DataGridViewCheckBoxCell = Me.dgvList_PR.Rows(e.RowIndex).Cells(e.ColumnIndex)
                If (dgvchk.EditedFormattedValue) Then
                    Me.dgvList_PR.CurrentCell = Me.dgvList_PR.CurrentRow.Cells("col_Receive_Qty")
                End If
            End If
        End If
    End Sub

    Private Sub dgvPR_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList_PR.CellContentDoubleClick
        If e.ColumnIndex = CheckBoxHeaderCell1.ColumnIndex Then CheckBoxHeaderCell1.RefreshCheckState()
    End Sub

#End Region

#Region " Load "

    Private Sub frmPO_PR_Import_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

            ' Default Condition
            Me.chkCustomer.Checked = False
            Me.chkSku.Checked = False
            Me.chkPurchaseOrder_Request_Date.Checked = False
            Me.chkDue_Date.Checked = False
            Me.chkDocumentType.Checked = False
            Me.chkPurchaseOrder_Request_No.Checked = False
            Me.chkRef_No1.Checked = False
            Me.chkRef_No2.Checked = False
            Me.btnPopupCustomer.Enabled = False
            Me.btnPopupSku.Enabled = False
            Me.dtpPurchaseOrder_Request_Date_S.Enabled = False
            Me.dtpPurchaseOrder_Request_Date_E.Enabled = False
            Me.dtpDue_Date_S.Enabled = False
            Me.dtpDue_Date_E.Enabled = False
            Me.cboDocumentType.Enabled = False
            Me.txtPurchaseOrder_Request_No.Enabled = False
            Me.txtRef_No1.Enabled = False
            Me.txtRef_No2.Enabled = False

            ' Default Date
            Me.dtpPurchaseOrder_Request_Date_S.Value = Today
            Me.dtpPurchaseOrder_Request_Date_E.Value = Today
            Me.dtpDue_Date_S.Value = Today
            Me.dtpDue_Date_E.Value = Today

            ' ComboBox
            Me.getCboDocumentType()
            Me.chkDocumentType.Enabled = (Me.cboDocumentType.Items.Count > 0)

            ' DGV
            If (Me.dgvList_PR.Columns.Contains("col_IsSelected")) Then
                Me.dgvList_PR.Columns("col_IsSelected").HeaderCell = Me.CheckBoxHeaderCell1
                Me.dgvList_PR.Columns("col_IsSelected").HeaderText = ""
            End If
            Me.dgvList_PR.AutoGenerateColumns = False
            Me.dgvPOItem.AutoGenerateColumns = False
            Me.dgvPRItem.AutoGenerateColumns = False

            ' Config
            Me.USE_PRODUCT_CUSTOMER = New config_CustomSetting().getConfig_Key_USE("USE_PRODUCT_CUSTOMER")

            ' Get Data
            Dim _dt As DataTable = New clsPR().Query(String.Format(" select Customer_Index from tb_PurchaseOrder where PurchaseOrder_Index='{0}' ", Me._PurchaseOrder_Index))
            If (_dt.Rows.Count = 0) Then
                Throw New ApplicationException("ไม่พบใบสั่งซื้อ")
            End If
            Me.txtCustomer.Tag = _dt.Rows(0).Item("Customer_Index")
            If (Me.txtCustomer.Tag = Nothing) Then
                Throw New ApplicationException("กรุณาระบุลูกค้า")
            End If
            Me.chkCustomer.Checked = True
            Me.chkCustomer.Enabled = False
            Me.btnPopupCustomer.Visible = False
            Me.getCustomer(Me.txtCustomer.Tag)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmPO_PR_Import_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            If (Me._PurchaseOrder_PR_Index = Nothing) Then Exit Sub
            If (Not _IsProcess) Then
                Dim _result As Boolean = New clsPR().renewPurchaseOrderItem_PR(Me._PurchaseOrder_PR_Index)
                Me._IsProcess = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " ComboBox "

    Private Sub getCboDocumentType()
        Try
            Dim _dt As DataTable
            _dt = New clsPR().Query(String.Format(" select DocumentType_Index,Description as DocumentType_Desc from ms_DocumentType where Process_Id={0} and status_id<>-1 order by Description asc ", Process_Id))
            With Me.cboDocumentType
                .DisplayMember = "DocumentType_Desc"
                .ValueMember = "DocumentType_Index"
                .DataSource = _dt
                '.Enabled = (.Items.Count > 0)
                If (.Items.Count > 0) Then
                    .SelectedIndex = 0
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " Get Data "

    Private Sub getCustomer(ByVal Customer_Index As String)
        Dim oCustomer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim _dt As DataTable = New DataTable
        Try
            oCustomer.getPopup_Customer("Customer_Index", Customer_Index)
            _dt = oCustomer.DataTable
            If _dt.Rows.Count > 0 Then
                Me.txtCustomer.Tag = _dt.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer.Text = _dt.Rows(0).Item("Customer_Id").ToString
            Else
                Me.txtCustomer.Tag = ""
                Me.txtCustomer.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            oCustomer = Nothing
            _dt = Nothing
        End Try
    End Sub

#End Region

#Region " Condition "

    Private Sub chkCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomer.CheckedChanged
        If (Me.chkCustomer.Checked) Then
            Me.btnPopupCustomer.Enabled = True
            Me.btnPopupCustomer.Focus()
        Else
            Me.btnPopupCustomer.Enabled = False
        End If
    End Sub

    Private Sub chkSku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku.CheckedChanged
        If (Me.chkSku.Checked) Then
            Me.btnPopupSku.Enabled = True
            Me.btnPopupSku.Focus()
        Else
            Me.btnPopupSku.Enabled = False
        End If
    End Sub

    Private Sub chkPurchaseOrder_Request_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPurchaseOrder_Request_Date.CheckedChanged
        If (Me.chkPurchaseOrder_Request_Date.Checked) Then
            Me.dtpPurchaseOrder_Request_Date_S.Enabled = True
            Me.dtpPurchaseOrder_Request_Date_E.Enabled = True
            Me.dtpPurchaseOrder_Request_Date_S.Focus()
        Else
            Me.dtpPurchaseOrder_Request_Date_S.Enabled = False
            Me.dtpPurchaseOrder_Request_Date_E.Enabled = False
        End If
    End Sub

    Private Sub chkDue_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDue_Date.CheckedChanged
        If (Me.chkDue_Date.Checked) Then
            Me.dtpDue_Date_S.Enabled = True
            Me.dtpDue_Date_E.Enabled = True
            Me.chkDue_Date.Focus()
        Else
            Me.dtpDue_Date_S.Enabled = False
            Me.dtpDue_Date_E.Enabled = False
        End If
    End Sub

    Private Sub chkDocumentType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkDocumentType.CheckedChanged
        If (Me.chkDocumentType.Checked) Then
            Me.cboDocumentType.Enabled = True
            Me.cboDocumentType.Focus()
        Else
            Me.cboDocumentType.Enabled = False
        End If
    End Sub

    Private Sub chkPurchaseOrder_Request_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPurchaseOrder_Request_No.CheckedChanged
        If (Me.chkPurchaseOrder_Request_No.Checked) Then
            Me.txtPurchaseOrder_Request_No.Enabled = True
            Me.txtPurchaseOrder_Request_No.SelectAll()
            Me.txtPurchaseOrder_Request_No.Focus()
        Else
            Me.txtPurchaseOrder_Request_No.Enabled = False
        End If
    End Sub

    Private Sub chkRef_No1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRef_No1.CheckedChanged
        If (Me.chkRef_No1.Checked) Then
            Me.txtRef_No1.Enabled = True
            Me.txtRef_No1.SelectAll()
            Me.txtRef_No1.Focus()
        Else
            Me.txtRef_No1.Enabled = False
        End If
    End Sub

    Private Sub chkRef_No2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRef_No2.CheckedChanged
        If (Me.chkRef_No2.Checked) Then
            Me.txtRef_No2.Enabled = True
            Me.txtRef_No2.SelectAll()
            Me.txtRef_No2.Focus()
        Else
            Me.txtRef_No2.Enabled = False
        End If
    End Sub

    Private Sub dtpPurchaseOrder_Request_Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPurchaseOrder_Request_Date_S.ValueChanged, dtpPurchaseOrder_Request_Date_E.ValueChanged
        Try
            If (sender Is Me.dtpPurchaseOrder_Request_Date_S) Then
                If Me.dtpPurchaseOrder_Request_Date_E.Value < Me.dtpPurchaseOrder_Request_Date_S.Value Then
                    Me.dtpPurchaseOrder_Request_Date_E.Value = Me.dtpPurchaseOrder_Request_Date_S.Value
                End If
            ElseIf (sender Is Me.dtpPurchaseOrder_Request_Date_E) Then
                If Me.dtpPurchaseOrder_Request_Date_S.Value > Me.dtpPurchaseOrder_Request_Date_E.Value Then
                    Me.dtpPurchaseOrder_Request_Date_S.Value = Me.dtpPurchaseOrder_Request_Date_E.Value
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtpDue_Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDue_Date_S.ValueChanged, dtpDue_Date_E.ValueChanged
        Try
            If (sender Is Me.dtpDue_Date_S) Then
                If Me.dtpDue_Date_E.Value < Me.dtpDue_Date_S.Value Then
                    Me.dtpDue_Date_E.Value = Me.dtpDue_Date_S.Value
                End If
            ElseIf (sender Is Me.dtpDue_Date_E) Then
                If Me.dtpDue_Date_S.Value > Me.dtpDue_Date_E.Value Then
                    Me.dtpDue_Date_S.Value = Me.dtpDue_Date_E.Value
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPopupCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopupCustomer.Click
        Try
            If (USE_PRODUCT_CUSTOMER) Then
                For i As Integer = 0 To Me.dgvPRItem.Rows.Count - 2
                    If (Me.dgvPRItem.Rows(i).Cells("col_Sku_Id").Tag IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.dgvPRItem.Rows(i).Cells("col_Sku_Id").Tag.ToString())) Then
                        MessageBox.Show(String.Format("กรุณาลบรายการก่อนเปลี่ยน{0}", Me.chkCustomer.Text.Trim()), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
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

    Private Sub btnPopupSku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopupSku.Click
        Try
            If (Me.USE_PRODUCT_CUSTOMER AndAlso Me.txtCustomer.Tag = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkCustomer.Text.Trim()), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If

            Dim _frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me.txtCustomer.Tag)
            _frm.ShowDialog()
            If (Not _frm.Sku_Index = Nothing) Then
                Me.txtSku.Tag = _frm.Sku_Index
                Me.txtSku.Text = _frm.Sku_ID
            End If
            _frm.Close()

        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Search Condition "

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            Me.search()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub search()
        Try
            Dim _strWhere As String = ""

            ' Condition
            If (Me.chkCustomer.Checked AndAlso Me.txtCustomer.Tag = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkCustomer.Text.Trim()), "ค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (Me.chkCustomer.Checked) Then
                _strWhere = String.Format(" and Customer_Index='{0}' ", Me.txtCustomer.Tag)
            End If

            If (Me.chkSku.Checked AndAlso Me.txtSku.Tag = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkSku.Text.Trim()), "ค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (Me.chkSku.Checked) Then
                _strWhere = String.Format(" and Sku_Index='{0}' ", Me.txtSku.Tag)
            End If

            If (Me.chkPurchaseOrder_Request_Date.Checked) Then
                _strWhere = String.Format(" and (PurchaseOrder_Request_Date>='{0}' and PurchaseOrder_Request_Date <='{1}') ", Me.dtpPurchaseOrder_Request_Date_S.Value.ToString("yyyy-MM-dd"), Me.dtpPurchaseOrder_Request_Date_E.Value.ToString("yyyy-MM-dd"))
            End If

            If (Me.chkDue_Date.Checked) Then
                _strWhere = String.Format(" and (Due_Date>='{0}' and Due_Date <='{1}') ", Me.dtpDue_Date_S.Value.ToString("yyyy-MM-dd"), Me.dtpDue_Date_E.Value.ToString("yyyy-MM-dd"))
            End If

            If (Me.chkDocumentType.Checked AndAlso Me.cboDocumentType.SelectedValue = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkSku.Text.Trim()), "ค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (Me.chkDocumentType.Checked) Then
                _strWhere = String.Format(" and DocumentType_Index='{0}' ", Me.cboDocumentType.SelectedValue)
            End If

            If (Me.chkPurchaseOrder_Request_No.Checked AndAlso Me.txtPurchaseOrder_Request_No.Text.Trim() = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkPurchaseOrder_Request_No.Text.Trim()), "ค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (Me.chkPurchaseOrder_Request_No.Checked) Then
                Dim arrStr() As String = txtPurchaseOrder_Request_No.Text.Split(vbNewLine)
                Dim strIN As String = ""
                Dim cnt As Integer = 0
                For Each Str As String In arrStr
                    If Str.Trim = "" Then Continue For
                    strIN &= IIf(strIN.Length = 0, Str.Trim.Replace("'", "''"), "','" & Str.Trim.Replace("'", "''"))
                    cnt += 1
                Next
                If cnt > 1 Then
                    _strWhere &= String.Format(" and PurchaseOrder_Request_No in ('{0}') ", strIN)
                Else
                    _strWhere &= String.Format(" and PurchaseOrder_Request_No like '%{0}' ", strIN)
                End If
            End If

            If (Me.chkRef_No1.Checked AndAlso Me.txtRef_No1.Text.Trim() = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkRef_No1.Text.Trim()), "ค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (Me.chkRef_No1.Checked) Then
                _strWhere = String.Format(" and Ref_No1='{0}' ", Me.txtRef_No1.Text.Trim().Replace("'", "''"))
            End If

            If (Me.chkRef_No2.Checked AndAlso Me.txtRef_No2.Text.Trim() = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.chkRef_No2.Text.Trim()), "ค้นหา", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Exit Sub
            End If
            If (Me.chkRef_No2.Checked) Then
                _strWhere = String.Format(" and Ref_No2='{0}' ", Me.txtRef_No2.Text.Trim().Replace("'", "''"))
            End If

            ' Query
            Dim _dt As DataTable = New clsPR().getList_PurchaseOrder_Request(_strWhere)

            If (Not _dt.Columns.Contains("IsSelected")) Then
                _dt.Columns.Add("IsSelected", GetType(Boolean))
            End If
            If (_dt.Columns.Contains("Receive_Qty")) Then
                _dt.Columns("Receive_Qty").ReadOnly = False
            End If
            If (_dt.Columns.Contains("Total_Receive_Qty")) Then
                _dt.Columns("Total_Receive_Qty").ReadOnly = False
            End If

            ' Result
            '_dt.DefaultView.RowFilter = " isnull(Total_Receive_Qty,0)>0 "
            'Me.dgvList_PR.DataSource = _dt.DefaultView
            Me.dgvList_PR.DataSource = _dt
            Me.col_Receive_Qty.ReadOnly = False
            Me.CheckBoxHeaderCell1.Checked = False

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " DGV List PR "

    Private Sub dgvList_PR_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvList_PR.CellValueChanged
        Try
            If (e.ColumnIndex <= -1 Or e.RowIndex <= -1) Then
                Exit Sub
            End If

            Dim _dgv As DataGridView = CType(sender, DataGridView)
            With _dgv
                If (.RowCount < 0) Then Exit Sub
                Select Case .Columns(e.ColumnIndex).Name
                    Case "col_Receive_Qty"
                        If (Not IsNumeric(.Rows(e.RowIndex).Cells("col_Receive_Qty").Value)) Then
                            .Rows(e.RowIndex).Cells("col_Receive_Qty").Value = "0"
                        End If
                        If (CDec(.Rows(e.RowIndex).Cells("col_Receive_Qty").Value) < 0) Then
                            .Rows(e.RowIndex).Cells("col_Receive_Qty").Value = "0"
                        End If

                        Dim _ratio As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Ratio").Value, _ratio)
                        Dim _receive_Qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Receive_Qty").Value, _receive_Qty)
                        Decimal.TryParse(_receive_Qty.ToString(Me.col_Receive_Qty.DefaultCellStyle.Format), _receive_Qty)
                        Dim _total_Receive_Qty As Decimal = _ratio * _receive_Qty
                        Dim _total_Pending_Qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Total_Pending_Qty").Value, _total_Pending_Qty)
                        If (_total_Receive_Qty > _total_Pending_Qty) Then
                            _receive_Qty = _total_Pending_Qty / _ratio
                            _total_Receive_Qty = _ratio * _receive_Qty
                        End If
                        .Rows(e.RowIndex).Cells("col_Receive_Qty").Value = _receive_Qty
                        .Rows(e.RowIndex).Cells("col_Total_Receive_Qty").Value = _total_Receive_Qty

                End Select
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvList_PR_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvList_PR.EditingControlShowing
        Try
            If (Me.dgvList_PR.Columns(Me.dgvList_PR.CurrentCell.ColumnIndex).Name = "col_Receive_Qty") Then
                RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Receive_Qty_Keypress
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Receive_Qty_Keypress
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub col_Receive_Qty_Keypress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Try
            If Me.dgvList_PR.CurrentCell.ColumnIndex = Me.dgvList_PR.Columns("col_Receive_Qty").Index Then
                If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = "." Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = ChrW(Keys.Delete)) Then e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvList_PR_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvList_PR.CellValidating
        Try
            If (Not Me.dgvList_PR.Columns(e.ColumnIndex).Name = "col_Receive_Qty") Then Exit Sub
            If (Not IsNumeric(e.FormattedValue)) Then
                e.Cancel = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Private Sub btnReceive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReceive.Click
        Try
            Dim _dtReceive As DataTable = DirectCast(Me.dgvList_PR.DataSource, DataTable)
            Dim _drArrReceive() As DataRow = _dtReceive.Select(" IsSelected='true' and Total_Receive_Qty>0 ")
            If (_drArrReceive.Length = 0) Then Exit Sub
            If (Me._PurchaseOrder_PR_Index = Nothing) Then
                Me._PurchaseOrder_PR_Index = New Sy_AutoNumber().getSys_Value("PurchaseOrder_PR_Index")
            End If
            ' set data
            Dim _dtInsert As DataTable = New clsPR().getDtSchemaPurchaseOrderItem_PR()
            For Each _drReceive As DataRow In _drArrReceive
                Dim _drInsert As DataRow = _dtInsert.NewRow()
                _drInsert("PurchaseOrderItem_PR_Index") = DBNull.Value
                _drInsert("PurchaseOrder_PR_Index") = Me._PurchaseOrder_PR_Index
                _drInsert("PurchaseOrderItem_Index") = DBNull.Value
                _drInsert("PurchaseOrder_Index") = Me._PurchaseOrder_Index
                _drInsert("PurchaseOrder_RequestItem_Index") = _drReceive("PurchaseOrder_RequestItem_Index")
                _drInsert("PurchaseOrder_Request_Index") = _drReceive("PurchaseOrder_Request_Index")
                _drInsert("Sku_Index") = _drReceive("Sku_Index")
                _drInsert("Package_Index") = _drReceive("Package_Index")
                _drInsert("Qty") = _drReceive("Receive_Qty")
                _drInsert("Ratio") = _drReceive("Ratio")
                _drInsert("Total_Qty") = _drReceive("Total_Receive_Qty")
                _drInsert("Weight") = DBNull.Value
                _drInsert("Volume") = DBNull.Value
                _drInsert("Status") = 1
                _dtInsert.Rows.Add(_drInsert)
            Next
            ' insert
            Dim _result As Boolean = New clsPR().receivePurchaseOrderItem_PR(_dtInsert)
            Me.dgvList_PR.DataSource = Nothing
            Me.col_Receive_Qty.ReadOnly = False
            Me.CheckBoxHeaderCell1.Checked = False

            Me.dgvPRItem.DataSource = New clsPR().getConsolidate_PurchaseOrder_RequestItem(Me._PurchaseOrder_PR_Index)
            Me.dgvPOItem.DataSource = New clsPR().getConsolidate_PurchaseOrderItem(Me._PurchaseOrder_PR_Index)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        Try
            If (Me._PurchaseOrder_PR_Index = Nothing) Then Exit Sub
            If (Me.tcoConsolidate_PR.SelectedTab Is Me.tpaPOItem) Then
                If (Me.dgvPOItem.CurrentRow Is Nothing) Then Exit Sub
                Dim _PurchaseOrderItem_Index As String = Me.dgvPOItem.CurrentRow.Cells("col_G_PurchaseOrderItem_Index").Value
                ' remove
                Dim _result As Boolean = New clsPR().removePurchaseOrderItem(Me._PurchaseOrder_PR_Index, _PurchaseOrderItem_Index)
                Me.dgvList_PR.DataSource = Nothing
                Me.col_Receive_Qty.ReadOnly = False
                Me.CheckBoxHeaderCell1.Checked = False

                Me.dgvPRItem.DataSource = New clsPR().getConsolidate_PurchaseOrder_RequestItem(Me._PurchaseOrder_PR_Index)
                Me.dgvPOItem.DataSource = New clsPR().getConsolidate_PurchaseOrderItem(Me._PurchaseOrder_PR_Index)
            ElseIf (Me.tcoConsolidate_PR.SelectedTab Is Me.tpaPRItem) Then
                If (Me.dgvPRItem.CurrentRow Is Nothing) Then Exit Sub
                Dim _PurchaseOrderItem_Index As String = Me.dgvPRItem.CurrentRow.Cells("col_I_PurchaseOrderItem_Index").Value
                Dim _PurchaseOrderItem_PR_Index As String = Me.dgvPRItem.CurrentRow.Cells("col_I_PurchaseOrderItem_PR_Index").Value
                ' remove
                Dim _result As Boolean = New clsPR().removePurchaseOrderItem_PR(Me._PurchaseOrder_PR_Index, _PurchaseOrderItem_Index, _PurchaseOrderItem_PR_Index)
                Me.dgvList_PR.DataSource = Nothing
                Me.col_Receive_Qty.ReadOnly = False
                Me.CheckBoxHeaderCell1.Checked = False

                Me.dgvPRItem.DataSource = New clsPR().getConsolidate_PurchaseOrder_RequestItem(Me._PurchaseOrder_PR_Index)
                Me.dgvPOItem.DataSource = New clsPR().getConsolidate_PurchaseOrderItem(Me._PurchaseOrder_PR_Index)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnRenew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRenew.Click
        Try
            If (Me._PurchaseOrder_PR_Index = Nothing) Then Exit Sub
            Dim _result As Boolean = New clsPR().renewPurchaseOrderItem_PR(Me._PurchaseOrder_PR_Index)
            Me.dgvPRItem.DataSource = New clsPR().getConsolidate_PurchaseOrder_RequestItem(Me._PurchaseOrder_PR_Index)
            Me.dgvPOItem.DataSource = New clsPR().getConsolidate_PurchaseOrderItem(Me._PurchaseOrder_PR_Index)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            If (Me._PurchaseOrder_PR_Index = Nothing) Then
                Me.Close()
                Exit Sub
            End If
            Dim _result As Boolean = New clsPR().confirmPurchaseOrderItem_PR(Me._PurchaseOrder_PR_Index)
            Me._IsProcess = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If (Me._PurchaseOrder_PR_Index = Nothing) Then
                Me.Close()
                Exit Sub
            End If
            Dim _result As Boolean = New clsPR().renewPurchaseOrderItem_PR(Me._PurchaseOrder_PR_Index)
            Me._IsProcess = True
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class