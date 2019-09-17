Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer

Public Class frmPR_Auto

#Region " Enum Operation Type "

    Public Enum enuOperation_Type
        [ADDNEW]
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

    Private Sub frmPR_Auto_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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
            Me.cboDocumentType.SelectedValue = New config_CustomSetting().getConfig_Key_DEFUALT("DEFAULT_DOCUMENT_TYPE_PR_AUTO")
            Me.cboDocumentType.Enabled = False
            If (Me.cboDocumentType.SelectedValue = Nothing And Me.cboDocumentType.Items.Count > 0) Then
                Me.cboDocumentType.SelectedIndex = 0
                Me.cboDocumentType.Enabled = True
            End If

            ' DGV
            Me.dgvPRItem.AutoGenerateColumns = False

            ' Default_Customer_Index
            Me.Default_Customer_Index = New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH).GetUserByCustomerDefault()
            If (Not Me.Default_Customer_Index = Nothing) Then
                Me.getCustomer(Me.Default_Customer_Index)
                Me.getPurchaseOrder_Request_Auto(Me.txtCustomer_Id.Tag, "0010000000001")
                Me.setItem_Seq(Me.dgvPRItem)
            End If

            ' Config
            Me.USE_PRODUCT_CUSTOMER = New config_CustomSetting().getConfig_Key_USE("USE_PRODUCT_CUSTOMER")

            ' Get Data
            Dim _dt As DataTable = New clsPR().Query(" select getdate() as Edit_Date_S ")
            Me.Edit_Date_S = CDate(_dt.Rows(0).Item("Edit_Date_S"))
            Me.listRemove = New List(Of String)

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
            'If (USE_PRODUCT_CUSTOMER) Then
            '    For i As Integer = 0 To Me.dgvPRItem.Rows.Count - 1
            '        If (Me.dgvPRItem.Rows(i).Cells("col_Sku_Index").Value IsNot Nothing AndAlso Not String.IsNullOrEmpty(Me.dgvPRItem.Rows(i).Cells("col_Sku_Index").Value)) Then
            '            MessageBox.Show(String.Format("กรุณาลบรายการก่อนเปลี่ยน{0}", Me.lblCustomer.Text.Trim()), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '            Exit Sub
            '        End If
            '    Next
            'End If

            Dim _frm As New frmCustmer_Popup()
            _frm.ShowDialog()
            If (Not _frm.Customer_Index = Nothing) Then
                Me.getCustomer(_frm.Customer_Index)
                Me.getPurchaseOrder_Request_Auto(Me.txtCustomer_Id.Tag, "0010000000001")
                Me.setItem_Seq(Me.dgvPRItem)
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

#End Region

#Region " Tab PRItem "

    Private Sub setItem_Seq(ByVal dgv As DataGridView)
        Try
            For i As Integer = 0 To dgv.RowCount - 1
                dgv.Rows(i).Cells("col_Item_Seq").Value = i + 1
            Next
        Catch ex As Exception
            Throw ex
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
                    Case "col_Qty"
                        If (Not IsNumeric(.Rows(e.RowIndex).Cells("col_Qty").Value)) Then
                            .Rows(e.RowIndex).Cells("col_Qty").Value = "0"
                        End If

                        Dim _ratio As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Ratio").Value, _ratio)
                        Dim _qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Qty").Value, _qty)
                        Decimal.TryParse(_qty.ToString(Me.col_Qty.DefaultCellStyle.Format), _qty)
                        .Rows(e.RowIndex).Cells("col_Qty").Value = _qty
                        Dim _total_Qty As Decimal = _ratio * _qty
                        Dim _total_Received_Qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Total_Received_Qty").Value, _total_Received_Qty)
                        Dim _weight As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Unit_Weight").Value, _weight)
                        Decimal.TryParse((_weight * _qty).ToString("0.######"), _weight)
                        Dim _volume As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells("col_Unit_Volume").Value, _volume)
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

#End Region

#Region " Get Data "

    Private Sub getPurchaseOrder_Request_Auto(ByVal Customer_Index As String, ByVal ItemStatus_Index As String)
        Try
            Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request_Auto(Customer_Index, ItemStatus_Index)

            If (_dt.Columns.Contains("Request")) Then
                _dt.Columns("Request").ReadOnly = False
            End If
            If (_dt.Columns.Contains("Total_Request_Qty")) Then
                _dt.Columns("Total_Request_Qty").ReadOnly = False
            End If
            If (_dt.Columns.Contains("Total_Pending_Qty")) Then
                _dt.Columns("Total_Pending_Qty").ReadOnly = False
            End If
            If (_dt.Columns.Contains("Weight")) Then
                _dt.Columns("Weight").ReadOnly = False
            End If
            If (_dt.Columns.Contains("Volume")) Then
                _dt.Columns("Volume").ReadOnly = False
            End If

            Me.dgvPRItem.DataSource = _dt
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
                Me._IsProcess = True
                MessageBox.Show(String.Format("บันทึกข้อมูลเรียบร้อย", Me.lblDocumentType.Text), "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
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
            If (Me.cboDocumentType.SelectedValue Is Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.lblDocumentType.Text), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return False
            End If
            If (Me.txtCustomer_Id.Tag = Nothing) Then
                MessageBox.Show(String.Format("กรุณาระบุ{0}", Me.lblCustomer.Text), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return False
            End If

            ' Detail
            For iRow As Integer = 0 To Me.dgvPRItem.Rows.Count - 1
                Dim _dgvr As DataGridViewRow = Me.dgvPRItem.Rows(iRow)
                'For Each _dgvr As DataGridViewRow In Me.dgvPRItem.Rows
                If (_dgvr.Cells("col_Sku_Index").Value = Nothing) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบรหัสสินค้า, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (_dgvr.Cells("col_Package_Index").Value = Nothing) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบหน่วยสินค้า, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (Not IsNumeric(_dgvr.Cells("col_Qty").Value)) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ จำนวนสั่งซื้อต้องเป็นตัวเลขเท่านั้น, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (CDec(_dgvr.Cells("col_Qty").Value) < 0) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ จำนวนสั่งซื้อต้องมากกว่าหรือเท่ากับศูนย์, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                    Return False
                End If

                If (Not IsNumeric(_dgvr.Cells("col_Ratio").Value)) Then
                    _dgvr.Selected = True
                    MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ ไม่พบอัตราส่วนหน่วยสินค้า, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                End If

                If (Not _dgvr.Cells("col_PurchaseOrder_RequestItem_Index").Value = Nothing) Then
                    Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request(String.Format(" and PurchaseOrder_RequestItem_Index='{0}' ", _dgvr.Cells("col_PurchaseOrder_RequestItem_Index").Value))
                    If (_dt.Rows.Count > 0) Then
                        Dim _total_Received_Qty As Decimal = _dt.Compute(" sum(Total_Received_Qty) ", "")
                        _dgvr.Cells("col_Total_Received_Qty").Value = _total_Received_Qty
                        _dgvr.Cells("col_Total_Pending_Qty").Value = CDec(_dgvr.Cells("col_Total_Qty").Value) - _total_Received_Qty
                        If (_total_Received_Qty > (CDec(_dgvr.Cells("col_Qty").Value) * CDec(_dgvr.Cells("col_Ratio").Value))) Then
                            _dgvr.Selected = True
                            MessageBox.Show(String.Format("ไม่สามารถทำรายการได้ เนื่องจากจำนวนที่สั่งซื้อน้อยกว่าจำนวนที่รับไปแล้ว, ลำดับ {0}", _dgvr.Cells("col_Item_Seq").Value), "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                            Return False
                        End If
                    End If
                End If

            Next

            Dim _dtD As DataTable = DirectCast(Me.dgvPRItem.DataSource, DataTable)
            If (Not (_dtD.Select(" Request>0 ").Length > 0)) Then
                MessageBox.Show("ไม่พบรายการคำขอสั่งซื้อ", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                Return False
            End If

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
            For iRow As Integer = 0 To Me.dgvPRItem.Rows.Count - 1
                With Me.dgvPRItem.Rows(iRow)
                    Dim _qty As Decimal = 0
                    Decimal.TryParse(.Cells("col_Qty").Value, _qty)
                    Decimal.TryParse(_qty.ToString(Me.col_Qty.DefaultCellStyle.Format), _qty)
                    If (_qty <= 0) Then
                        Continue For
                    End If

                    Dim _drD As DataRow = _dtD.NewRow()
                    _drD("PurchaseOrder_RequestItem_Index") = .Cells("col_PurchaseOrder_RequestItem_Index").Value
                    _drD("PurchaseOrder_Request_Index") = Me.txtPurchaseOrder_Request_No.Tag
                    _drD("Item_Seq") = .Cells("col_Item_Seq").Value
                    _drD("Sku_Index") = .Cells("col_Sku_Index").Value

                    _drD("Package_Index") = .Cells("col_Package_Index").Value

                    '_drD("Serial_No") = DBNull.Value

                    .Cells("col_Qty").Value = _qty
                    _drD("Qty") = _qty

                    Dim _ratio As Decimal = 0
                    Decimal.TryParse(.Cells("col_Ratio").Value, _ratio)
                    Decimal.TryParse(_ratio.ToString(Me.col_Ratio.DefaultCellStyle.Format), _ratio)

                    _drD("Ratio") = _ratio

                    _drD("Total_Qty") = _qty * _ratio
                    _drD("Weight") = .Cells("col_Weight").Value
                    _drD("Volume") = .Cells("col_Volume").Value
                    '_drD("Total_Received_Qty") = .Cells("col_Total_Received_Qty").Value
                    _drD("Total_Received_Qty") = 0
                    '_drD("Ref_No1") = ""
                    '_drD("Ref_No2") = ""
                    '_drD("Ref_No3") = ""
                    '_drD("Ref_No4") = ""
                    _drD("Ref_No5") = "Auto PR"
                    _drD("Remark") = .Cells("col_Remark").Value
                    '_drD("Status") = ""
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

#Region " Refresh "

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            Me.getPurchaseOrder_Request_Auto(Me.txtCustomer_Id.Tag, "0010000000001")
            Me.setItem_Seq(Me.dgvPRItem)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
