Imports WMS_STD_Master.W_Language

Public Class frmPR_View

    Private Const Process_Id As Integer = 91

#Region " DGV HeaderCheckBox "

    Private WithEvents HeaderCheckBox1 As New CheckBox
    Private WithEvents CheckBoxHeaderCell1 As New DataGridViewCheckBoxHeaderCell

    Private Sub CheckBoxHeaderCell_CheckBoxClicked(ByVal sender As Object, ByVal e As DataGridViewCheckBoxHeaderCellEventArgs) Handles CheckBoxHeaderCell1.CheckBoxClicked
        sender.CheckUncheckEntireColumn(e.Checked)
    End Sub

    Private Sub dgvPR_CellContentClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPR.CellContentClick
        If e.ColumnIndex = CheckBoxHeaderCell1.ColumnIndex Then CheckBoxHeaderCell1.RefreshCheckState()
    End Sub

    Private Sub dgvPR_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPR.CellContentDoubleClick
        If e.ColumnIndex = CheckBoxHeaderCell1.ColumnIndex Then CheckBoxHeaderCell1.RefreshCheckState()
    End Sub

#End Region

#Region " Load "

    Private Sub frmPR_View_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")
            Me.defaultOnLoad()
            Me.search()
            Me.setDgvPRStyle(Me.dgvPR)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub defaultOnLoad()
        Try
            'Dim oFunction As New WMS_STD_Master.W_Language
            'oFunction.SwitchLanguage(Me, 91)
            'oFunction.SW_Language_Column(Me, Me.dgvPR, 91)
            'oFunction = Nothing

            ' Condition
            Me.dtpDate_S.Value = CDate(Now.ToShortDateString())
            Me.dtpDate_E.Value = CDate(Now.ToShortDateString())

            ' Filter
            Me.getCboStatus()
            Me.getCboDocumentType()
            Me.getCboProductType()

            ' Print
            Me.getCboPrint()

            ' Pager
            Me.cboRowPerPage.Enabled = (Me.cboRowPerPage.Items.Count > 0)
            If (Me.cboRowPerPage.Items.Count > 0) Then
                Me.cboRowPerPage.SelectedIndex = 0
            End If

            ' DGV
            If (Me.dgvPR.Columns.Contains("col_IsSelected")) Then
                Me.dgvPR.Columns("col_IsSelected").HeaderCell = Me.CheckBoxHeaderCell1
                Me.dgvPR.Columns("col_IsSelected").HeaderText = ""
            End If
            Me.dgvPR.AutoGenerateColumns = False

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

    'Private Sub frmPR_View_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
    '    Try
    '        If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
    '            Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
    '            If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
    '                Dim frm As New WMS_STD_CONFIGURATION.frmConfigPurchaseOrder
    '                frm.ShowDialog()
    '                Dim oFunction As New WMS_STD_Master.W_Language
    '                oFunction.SwitchLanguage(Me, 91)
    '                oFunction.SW_Language_Column(Me, Me.dgvPR, 91)
    '                oFunction = Nothing
    '            End If
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

#End Region

#Region " GroupBox Condition "

    Private Sub radCondition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radPurchaseOrder_Request_Date.CheckedChanged, radPurchaseOrder_Request_No.CheckedChanged, radDue_Date.CheckedChanged, radCustomer_Id.CheckedChanged
        Try
            Dim ShowPanel As Integer = 0
            If (sender Is Me.radPurchaseOrder_Request_Date) Then
                ShowPanel = 1
            ElseIf (sender Is Me.radPurchaseOrder_Request_No) Then
                ShowPanel = 2
            ElseIf (sender Is Me.radDue_Date) Then
                ShowPanel = 1
            ElseIf (sender Is Me.radCustomer_Id) Then
                ShowPanel = 2
            End If
            Select Case ShowPanel
                Case 1
                    Me.pnlCondition1.Visible = True
                    Me.pnlCondition2.Visible = False
                Case 2
                    Me.pnlCondition1.Visible = False
                    Me.pnlCondition2.Visible = True
                Case Else
                    Me.pnlCondition1.Visible = False
                    Me.pnlCondition2.Visible = False
            End Select
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dtpDate_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpDate_S.ValueChanged, dtpDate_E.ValueChanged
        Try
            If (sender Is Me.dtpDate_S) Then
                If Me.dtpDate_E.Value < Me.dtpDate_S.Value Then
                    Me.dtpDate_E.Value = Me.dtpDate_S.Value
                End If
            ElseIf (sender Is Me.dtpDate_E) Then
                If Me.dtpDate_S.Value > Me.dtpDate_E.Value Then
                    Me.dtpDate_S.Value = Me.dtpDate_E.Value
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtText_Condition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtText_Condition.KeyDown
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (e.KeyCode = Keys.Enter) Then
                Me.search()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

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
            If (Me.radPurchaseOrder_Request_Date.Checked) Then
                _strWhere = String.Format(" and (cast(PurchaseOrder_Request_Date as date)>='{0}' and cast(PurchaseOrder_Request_Date as date)<='{1}') ", Me.dtpDate_S.Value.ToString("yyyy-MM-dd"), Me.dtpDate_E.Value.ToString("yyyy-MM-dd"))
            ElseIf (Me.radPurchaseOrder_Request_No.Checked And Not Me.txtText_Condition.Text.Trim() = Nothing) Then
                _strWhere = String.Format(" and PurchaseOrder_Request_No like '%{0}%' ", Me.txtText_Condition.Text.Trim().Replace("'", "''"))
            ElseIf (Me.radDue_Date.Checked) Then
                _strWhere = String.Format(" and (cast(Due_Date as date)>='{0}' and cast(Due_Date as date)<='{1}') ", Me.dtpDate_S.Value.ToString("yyyy-MM-dd"), Me.dtpDate_E.Value.ToString("yyyy-MM-dd"))
            ElseIf (Me.radCustomer_Id.Checked And Not Me.txtText_Condition.Text.Trim() = Nothing) Then
                _strWhere = String.Format(" and Customer_Id like '%{0}%' ", Me.txtText_Condition.Text.Trim().Replace("'", "''"))
            End If

            ' Filter
            If (Not Me.cboStatus.SelectedValue = Nothing) Then
                _strWhere = String.Format(" and Status='{0}' ", Me.cboStatus.SelectedValue)
            End If
            If (Not Me.cboDocumentType.SelectedValue = Nothing) Then
                _strWhere = String.Format(" and DocumentType_Index='{0}' ", Me.cboDocumentType.SelectedValue)
            End If
            If (Not Me.cboProductType.SelectedValue = Nothing) Then
                _strWhere = String.Format(" and PurchaseOrder_Request_Index in (select tb_pri.PurchaseOrder_Request_Index from tb_PurchaseOrder_RequestItem as tb_pri where tb_pri.Status<>-1 and tb_pri.Sku_Index in (select ms_s.Sku_Index from ms_Sku as ms_s left join ms_Product as ms_pd on ms_pd.Product_Index=ms_s.Product_Index left join ms_ProductType as ms_pdt on ms_pdt.ProductType_Index=ms_pd.ProductType_Index where ms_pdt.ProductType_Index='{0}')) ", Me.cboProductType.SelectedValue)
            End If

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            _strWhere &= oUser.GetUserByCustomer()

            ' Pager
            Dim _pagerType As clsPR.enuPagerType = clsPR.enuPagerType.All
            Dim _intTop As Integer = 100
            Dim _intRowPerPage As Integer = 50
            Dim _intPage As Integer = 1
            If (Me.radAll.Checked) Then
                _pagerType = clsPR.enuPagerType.All
            ElseIf (Me.radTop.Checked) Then
                _pagerType = clsPR.enuPagerType.Top
                Integer.TryParse(Me.txtTop.Text.Trim(), _intTop)
                If (_intTop <= 0) Then
                    _intTop = 100
                    Me.txtTop.Text = _intTop
                End If
            ElseIf (Me.radRowPage.Checked) Then
                _pagerType = clsPR.enuPagerType.Page
                Integer.TryParse(Me.cboRowPerPage.Text.ToString(), _intRowPerPage)
                If (_intRowPerPage <= 0) Then
                    _intRowPerPage = 50
                    Me.cboRowPerPage.SelectedValue = _intRowPerPage
                End If
                Integer.TryParse(Me.txtPage.Text.Trim(), _intPage)
                If (_intPage <= 0) Then
                    _intPage = 1
                    Me.txtPage.Text = _intPage
                End If
            End If

            ' Query
            Dim _dt As DataTable = New clsPR().getPurchaseOrder_Request_View(_strWhere, _pagerType, _intTop, _intRowPerPage, _intPage)
            Dim _countView As Integer = New clsPR().getCountPurchaseOrder_Request_View(_strWhere)

            If (Not _dt.Columns.Contains("IsSelected")) Then
                _dt.Columns.Add("IsSelected", GetType(Boolean))
            End If

            ' Result
            Me.dgvPR.DataSource = _dt
            Me.CheckBoxHeaderCell1.Checked = False
            Me.Calculate_Paging()
            Me.txtRowsCount.Text = _countView.ToString("#,##0")
            Me.setDgvPRStyle(Me.dgvPR)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub setDgvPRStyle(ByVal dgv As DataGridView)
        Try
            For Each _dgvr As DataGridViewRow In dgv.Rows
                Dim _status As Integer = 0
                Dim _total_Qty As Decimal = 0
                Dim _total_Received_Qty As Decimal = 0
                Dim _total_Pending_Qty As Decimal = 0
                Integer.TryParse(_dgvr.Cells("col_Status").Value, _status)
                Decimal.TryParse(_dgvr.Cells("col_Total_Qty").Value, _total_Qty)
                Decimal.TryParse(_dgvr.Cells("col_Total_Received_Qty").Value, _total_Received_Qty)
                Decimal.TryParse(_dgvr.Cells("col_Total_Pending_Qty").Value, _total_Pending_Qty)
                Select Case _status
                    Case -1 '- ยกเลิก - ชมพู
                        _dgvr.Cells("col_PurchaseOrder_Request_No").Style.BackColor = Drawing.Color.Pink
                        _dgvr.Cells("col_Total_Received_Qty").Style.BackColor = Color.Pink
                        _dgvr.Cells("col_Total_Pending_Qty").Style.BackColor = Color.Pink
                    Case 2 '- ค้างรับ - เหลือง
                        _dgvr.Cells("col_PurchaseOrder_Request_No").Style.BackColor = Drawing.Color.LightYellow
                        If _dgvr.Cells("col_Total_Received_Qty").Value = 0 And _dgvr.Cells("col_Total_Pending_Qty").Value > 0 Then
                            _dgvr.Cells("col_Total_Received_Qty").Style.BackColor = Color.LightSalmon
                            _dgvr.Cells("col_Total_Pending_Qty").Style.BackColor = Color.LightSalmon
                        ElseIf _dgvr.Cells("col_Total_Received_Qty").Value > 0 And _dgvr.Cells("col_Total_Pending_Qty").Value > 0 Then
                            _dgvr.Cells("col_Total_Received_Qty").Style.BackColor = Color.LightYellow
                            _dgvr.Cells("col_Total_Pending_Qty").Style.BackColor = Color.LightYellow
                        ElseIf _dgvr.Cells("col_Total_Received_Qty").Value > 0 And _dgvr.Cells("col_Total_Pending_Qty").Value = 0 Then
                            _dgvr.Cells("col_Total_Received_Qty").Style.BackColor = Color.LightGreen
                            _dgvr.Cells("col_Total_Pending_Qty").Style.BackColor = Color.LightGreen
                        End If
                    Case 3 '- เสร็จสิ้น - เขียว
                        _dgvr.Cells("col_PurchaseOrder_Request_No").Style.BackColor = Drawing.Color.LightGreen
                        _dgvr.Cells("col_Total_Received_Qty").Style.BackColor = Color.LightGreen
                        _dgvr.Cells("col_Total_Pending_Qty").Style.BackColor = Color.LightGreen
                    Case Else
                End Select
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " GroupBox Filter "

    Private Sub getCboStatus()
        Try
            Dim _dt As DataTable = New clsPR().Query(String.Format(" select cast(Status as varchar) as Status,Description as Status_Desc from ms_ProcessStatus where Process_Id={0} and Show=1 order by Seq asc ", Process_Id))
            Dim _dr As DataRow = _dt.NewRow()
            _dr("Status") = ""
            _dr("Status_Desc") = "--- แสดงทุกรายการ ---"
            _dt.Rows.InsertAt(_dr, 0)
            With Me.cboStatus
                .DisplayMember = "Status_Desc"
                .ValueMember = "Status"
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

    Private Sub getCboDocumentType()
        Try
            Dim _dt As DataTable = New clsPR().Query(String.Format(" select DocumentType_Index,Description as DocumentType_Desc from ms_DocumentType where Process_Id={0} and status_id<>-1 order by Description asc ", Process_Id))
            Dim _dr As DataRow = _dt.NewRow()
            _dr("DocumentType_Index") = ""
            _dr("DocumentType_Desc") = "--- แสดงทุกรายการ ---"
            _dt.Rows.InsertAt(_dr, 0)
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

    Private Sub getCboProductType()
        Try
            Dim _dt As DataTable = New clsPR().Query(" select ProductType_Index,Description as ProductType_Desc from ms_ProductType where status_id<>-1 order by Description asc ")
            Dim _dr As DataRow = _dt.NewRow()
            _dr("ProductType_Index") = ""
            _dr("ProductType_Desc") = "--- แสดงทุกรายการ ---"
            _dt.Rows.InsertAt(_dr, 0)
            With Me.cboProductType
                .DisplayMember = "ProductType_Desc"
                .ValueMember = "ProductType_Index"
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

#Region " GroupBox Print "

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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

            Try
                Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, "And PurchaseOrder_Request_Index ='" & Me.dgvPR.CurrentRow.Cells("Col_PurchaseOrder_Request_Index").Value & "'")
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

    'Private Sub print(Optional ByVal IsPreview As Boolean = False)
    '    Dim oReport As New clsReport()
    '    Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
    '    Try
    '        Dim PurchaseOrder_Request_Index_IN As String = ""
    '        Dim drArr() As DataRow = DirectCast(Me.dgvPR.DataSource, System.Data.DataTable).Select(" IsSelected=1 and Status<>-1 ")
    '        If drArr.Length > 0 Then
    '            For Each dr As DataRow In drArr
    '                PurchaseOrder_Request_Index_IN &= "'" & dr("PurchaseOrder_Request_Index_Index").ToString() & "',"
    '            Next
    '        Else
    '            MessageBox.Show("ไม่พบประเภทเอกสาร", "พิมพ์เอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
    '            Exit Sub
    '        End If
    '        PurchaseOrder_Request_Index_IN = PurchaseOrder_Request_Index_IN.Substring(0, PurchaseOrder_Request_Index_IN.Trim.Length - 1)
    '        Dim strWhere As String = " and PurchaseOrder_Request_Index in(" & PurchaseOrder_Request_Index_IN & ")"

    '        Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
    '        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
    '        rpt = oReport.GetReportInfo(Report_Name, strWhere)

    '        If (IsPreview) Then
    '            frm.CrystalReportViewer1.ReportSource = rpt
    '            frm.ShowDialog()
    '        Else
    '            Dim pdoPrint As New Printing.PrintDocument()
    '            'Get the Copy times
    '            Dim nCopy As Integer = pdoPrint.PrinterSettings.Copies
    '            'Get the number of Start Page
    '            Dim sPage As Integer = pdoPrint.PrinterSettings.FromPage
    '            'Get the number of End Page
    '            Dim ePage As Integer = pdoPrint.PrinterSettings.ToPage
    '            'Get the printer name
    '            Dim PrinterName As String = pdoPrint.PrinterSettings.PrinterName

    '            rpt.PrintOptions.PrinterName = PrinterName
    '            rpt.PrintToPrinter(nCopy, False, sPage, ePage)
    '        End If

    '        rpt.Close()
    '        rpt.Dispose()
    '        GC.Collect()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

#End Region

#Region " GroupBox Pager "

    Private Sub Calculate_Paging()
        ' Calculate Paging 
        Try
            Dim intRowCount As Integer
            Dim intRowPerPage As Integer

            intRowCount = CInt(Me.txtRowsCount.Text)
            intRowPerPage = CInt(Me.cboRowPerPage.Text)

            ' row count = 0 ; page = 1 : total page = 1
            If intRowCount = 0 Or (intRowCount <= intRowPerPage) Then
                Me.txtTotalPage.Text = 1
                Me.txtPage.Text = 1
            Else
                Me.txtTotalPage.Text = CInt(intRowCount / intRowPerPage)

                If CInt(Me.txtTotalPage.Text) * intRowPerPage < intRowCount Then
                    Me.txtTotalPage.Text = CInt(Me.txtTotalPage.Text) + 1
                End If
            End If

            Me.txtPage.Text = IIf(IsNumeric(txtPage.Text), Me.txtPage.Text, 1)

            'Enable Button
            Dim gintRowStart As Integer = 0
            Dim gintRowEnd As Integer = 0
            If CInt(Me.txtPage.Text) = 1 Then
                gintRowStart = 1
                gintRowEnd = intRowPerPage

                Me.btnPagePrev.Enabled = False
                Me.btnPageFirst.Enabled = False
            Else
                gintRowEnd = CInt(Me.txtPage.Text) * intRowPerPage
                gintRowStart = gintRowEnd - intRowPerPage + 1

                Me.btnPagePrev.Enabled = True
                Me.btnPageFirst.Enabled = True
            End If

            If CInt(Me.txtPage.Text) = CInt(Me.txtTotalPage.Text) Then
                Me.btnPageNext.Enabled = False
                Me.btnPageLast.Enabled = False
            Else
                Me.btnPageNext.Enabled = True
                Me.btnPageLast.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnPageFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageFirst.Click
        Try
            Me.txtPage.Text = 1
            Me.search()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            If (IsNumeric(Me.txtPage.Text)) Then
                Me.txtPage.Text -= 1
                If (CInt(Me.txtPage.Text) < 1) Then
                    Me.txtPage.Text = 1
                End If
            Else
                Me.txtPage.Text = 1
            End If
            Me.search()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            If (IsNumeric(Me.txtPage.Text)) Then
                Me.txtPage.Text += 1
                If (CInt(Me.txtPage.Text) < 1) Then
                    Me.txtPage.Text = 1
                End If
            Else
                Me.txtPage.Text = 1
            End If
            Me.search()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            Me.txtPage.Text = Me.txtTotalPage.Text
            Me.search()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtPage_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPage.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If IsNumeric(Me.txtPage.Text) Then
                    If CInt(Me.txtPage.Text) > CInt(Me.txtTotalPage.Text) Then
                        Me.txtPage.Text = Me.txtTotalPage.Text
                    End If
                    Me.txtPage.Text = Trim(Me.txtPage.Text)
                    '  Calculate_Paging()
                    Me.search()
                Else
                    Me.txtPage.Text = 1
                    ' Calculate_Paging()
                    Me.search()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " DGV PR "

    Private Sub dgvPR_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvPR.CellDoubleClick
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (Me.dgvPR.CurrentRow Is Nothing) Then Exit Sub
            Dim _PurchaseOrder_Request_Index As String = Me.dgvPR.CurrentRow.Cells("col_PurchaseOrder_Request_Index").Value
            If (Not _PurchaseOrder_Request_Index = Nothing) Then
                Me.edit(_PurchaseOrder_Request_Index)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvPR_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvPR.Sorted
        Try
            Me.setDgvPRStyle(Me.dgvPR)
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub edit(ByVal PurchaseOrder_Request_Index As String)
        Try
            Dim _frm As New frmPR(frmPR.enuOperation_Type.UPDATE)
            _frm.PurchaseOrder_Request_Index = PurchaseOrder_Request_Index
            _frm.ShowDialog()
            If (_frm.IsProcess) Then
                Me.search()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub add()
        Try
            Dim _frm As New frmPR(frmPR.enuOperation_Type.ADDNEW)
            _frm.ShowDialog()
            If (_frm.IsProcess) Then
                Me.search()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region " PanelMainButton "

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            Me.add()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (Me.dgvPR.CurrentRow Is Nothing) Then Exit Sub
            Dim _PurchaseOrder_Request_Index As String = Me.dgvPR.CurrentRow.Cells("col_PurchaseOrder_Request_Index").Value
            If (Not _PurchaseOrder_Request_Index = Nothing) Then
                Me.edit(_PurchaseOrder_Request_Index)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (Me.dgvPR.CurrentRow Is Nothing) Then Exit Sub
            Dim _PurchaseOrder_Request_Index As String = Me.dgvPR.CurrentRow.Cells("col_PurchaseOrder_Request_Index").Value
            Dim _PurchaseOrder_Request_No As String = Me.dgvPR.CurrentRow.Cells("col_PurchaseOrder_Request_No").Value
            'Dim _IsCanCancel As Boolean = Me.dgvPR.CurrentRow.Cells("col_IsCanCancel").Value
            'Dim _Status As String = Me.dgvPR.CurrentRow.Cells("col_Status").Value
            If (Not _PurchaseOrder_Request_Index = Nothing) Then

                'If (_Status = "-1") Then
                '    MessageBox.Show(String.Format("ไม่สามารถยกเลิกเอกสารได้, เอกสารได้รับการยกเลิกแล้ว"), "ยกเลิกเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                '    Exit Sub
                'End If
                'If (Not _IsCanCancel) Then
                '    MessageBox.Show(String.Format("เอกสารนี้ไม่ยินยอมให้ยกเลิก"), "ยกเลิกเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                '    Exit Sub
                'End If

                If (New clsPR().canCancelPurchaseOrder_Request(_PurchaseOrder_Request_Index)) Then
                    Dim _result As Windows.Forms.DialogResult = MessageBox.Show(String.Format("คุณต้องการยกเลิกเอกสารเลขที่ {0} หรือไม่?", _PurchaseOrder_Request_No), "ยกเลิกเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
                    If (New clsPR().canCancelPurchaseOrder_Request(_PurchaseOrder_Request_Index)) Then
                        If (New clsPR().cancelPurchaseOrder_Request(_PurchaseOrder_Request_Index)) Then
                            Me.search()
                            MessageBox.Show(String.Format("ยกเลิกเอกสารเลขที่ {0} เรียบร้อย", _PurchaseOrder_Request_No), "ยกเลิกเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show(String.Format("เกิดข้อผิดพลาดทางข้อมูล, กรุณาลองอีกครั้ง", Me.lblDocumentType.Text), "ยกเลิกเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If (Me.dgvPR.CurrentRow Is Nothing) Then Exit Sub
            Dim _PurchaseOrder_Request_Index As String = Me.dgvPR.CurrentRow.Cells("col_PurchaseOrder_Request_Index").Value
            Dim _PurchaseOrder_Request_No As String = Me.dgvPR.CurrentRow.Cells("col_PurchaseOrder_Request_No").Value
            'Dim _IsConfirm As Boolean = Me.dgvPR.CurrentRow.Cells("col_IsConfirm").Value
            'Dim _Status As String = Me.dgvPR.CurrentRow.Cells("col_Status").Value
            If (Not _PurchaseOrder_Request_Index = Nothing) Then

                'If (_Status <> "1") Then
                '    MessageBox.Show(String.Format("ไม่สามารถยืนยันเอกสารได้, สถานะเอกสารไม่ถูกต้อง"), "ยืนยันเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                '    Exit Sub
                'End If
                'If (_IsConfirm) Then
                '    MessageBox.Show(String.Format("ไม่สามารถยืนยันเอกสารได้, เอกสารได้รับการยืนยันแล้ว"), "ยืนยันเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                '    Exit Sub
                'End If

                If (New clsPR().canConfirmPurchaseOrder_Request(_PurchaseOrder_Request_Index)) Then
                    Dim _result As Windows.Forms.DialogResult = MessageBox.Show(String.Format("คุณต้องการยืนยันเอกสารเลขที่ {0} หรือไม่?", _PurchaseOrder_Request_No), "ยืนยันเอกสาร", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If (_result = Windows.Forms.DialogResult.No) Then Exit Sub
                    If (New clsPR().canConfirmPurchaseOrder_Request(_PurchaseOrder_Request_Index)) Then
                        If (New clsPR().confirmPurchaseOrder_Request(_PurchaseOrder_Request_Index)) Then
                            Me.search()
                            MessageBox.Show(String.Format("ยืนยันเอกสารเลขที่ {0} เรียบร้อย", _PurchaseOrder_Request_No), "ยืนยันเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Else
                            MessageBox.Show(String.Format("เกิดข้อผิดพลาดทางข้อมูล, กรุณาลองอีกครั้ง", Me.lblDocumentType.Text), "ยืนยันเอกสาร", MessageBoxButtons.OK, MessageBoxIcon.Stop)
                        End If
                    End If
                End If

            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region " Auto PR "

    Private Sub btnAutoPR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoPR.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            Dim _frm As New frmPR_Auto(frmPR_Auto.enuOperation_Type.ADDNEW)
            _frm.ShowDialog()
            If (_frm.IsProcess) Then
                Me.search()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
