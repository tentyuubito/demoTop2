Imports WMS_STD_Master_Datalayer
Imports WMS_STD_CONFIGURATION
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports System.Windows.Forms
Imports WMS_STD_INB_PO_Datalayer
Imports WMS_STD_INB_Receive
Public Class frmPOView
    Public SaveType As Integer = 0
    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 1
    Private _CountRow As Integer = 0
    Private _iNewRow As Integer = 0

    Private fLoad As Boolean = False

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
    ''' Form Load Sub.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub frmPOView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.grdPOView.AutoGenerateColumns = False
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 9)
            oFunction.SW_Language_Column(Me, Me.grdPOView, 9)
            oFunction = Nothing

            Me.getPayment()
            Me.getProcessStatus()
            Me.getReportName(9)
            Me.GetDocumentType(9)
            Me.configBtn()
            Me.getProductType()
            fLoad = True
            Me.getPOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region " INITIALIZE CONTROL "

    ''' <summary>
    ''' Get Process Status for PO.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getProcessStatus()
        Dim objtb_PurchaseOrder As New tb_PurchaseOrder
        Dim objDTtb_PurchaseOrder As DataTable = New DataTable

        Try
            objtb_PurchaseOrder.getProcessStatus()
            objDTtb_PurchaseOrder = objtb_PurchaseOrder.DataTable

            With cboDocumentStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDTtb_PurchaseOrder
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objtb_PurchaseOrder = Nothing
            objDTtb_PurchaseOrder = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Get print out report.
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

    Private Sub getPayment()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Dim dPayment As Boolean

        Try
            dPayment = objCustomSetting.getConfig_Key_USE("USE_BTNPayment")

            If dPayment = False Then
                btnPayment.Visible = False
            Else
                btnPayment.Visible = True
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

#Region " RADIO EVENTS "

    ' ----------------------------------------------------
    ' ------ These sub/functions are for managing search 
    ' ------ criteria textboxes and datetime picker.
    ' ----------------------------------------------------
    Private Sub radDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radDate.CheckedChanged
        Me.dtpBeginDate.Visible = True
        Me.dtpEndDate.Visible = True
        Me.txtKeySearch.Visible = False
        Me.txtKeySearch.Text = ""
        Me.lblto.Visible = True
    End Sub

    Private Sub radNo_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radNo.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        Me.lblto.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub radSupplier_Name_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radSupplier_Name.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        Me.lblto.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub radSupId_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles radSupId.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        Me.lblto.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub
    Private Sub rdDueDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdDueDate.CheckedChanged
        Me.dtpBeginDate.Visible = True
        Me.dtpEndDate.Visible = True
        Me.txtKeySearch.Visible = False
        Me.txtKeySearch.Text = ""
        Me.lblto.Visible = True
    End Sub
#End Region
    Sub configBtn()
        Dim objDB As New config_CustomSetting
        If objDB.getConfig_Key_USE("USE_NotShow_BtnReciveINfrmPO") = True Then
            btnRecived.Visible = False
        Else
            btnRecived.Visible = True
        End If
    End Sub

#Region " BUTTON EVENTS "

    ' ------ Todd's Note 3 Jan 2010: Main code are in btnCancel_Click & btnConfirm_Click
    ''' <summary>
    ''' Button to add new PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try

            Dim frm As New frmPO
            frm.Icon = Me.Icon
            frm.objStatus = frmPO.enuOperation_Type.ADDNEW
            frm.ShowDialog()
            Me.getPOViewSearch()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button to edit PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        If grdPOView.Rows.Count > 0 Then
            Dim frm As New frmPO
            ' For editing PO, you must set value of "PurchaseOrder_Index" and "Status"
            frm.objStatus = frmPO.enuOperation_Type.UPDATE
            frm.PurchaseOrder_Index = grdPOView.Rows(grdPOView.CurrentRow.Index).Cells("Col_PurchaseOrder_Index").Value.ToString
            frm.Status = grdPOView.Rows(grdPOView.CurrentRow.Index).Cells("Col_Status").Value.ToString

            frm.ShowDialog()

            getPOViewSearch()
        End If
    End Sub

    ''' <summary>
    ''' Button to refresh search criteria.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try

            Me.getPOViewSearch()
            btnSearch.Focus()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Button to Cancel PO (ยกเลิก)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ' ====== TODO: HARDCODE-MSG 
        ' ====== Todd: 26 Dec 2009 - Need to get rid of hardcode text message in Thai.

        If grdPOView.Rows.Count <= 0 Then Exit Sub

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        Dim objPOTransaction As New POTransaction(POTransaction.enuOperation_Type.DELETE)
        Dim oPo As New tb_PurchaseOrder
        Dim SalseUnit As String = ""
        objPOTransaction.newPurchaseOrder_Index = Me.grdPOView.Rows(grdPOView.CurrentRow.Index).Cells("Col_PurchaseOrder_Index").Value

        Try
            If MessageBox.Show("คุณต้องการยกเลิกรายการเลขที่เอกสาร " & Me.grdPOView.CurrentRow.Cells("Col_PurchaseOrder_No").Value & "   ใช่หรือไม่ ", "ยืนยันการยกเลิกรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                oPo.PurchaseOrder_Index = Me.grdPOView.CurrentRow.Cells("Col_PurchaseOrder_Index").Value.ToString
                oPo.PurchaseOrder_No = Me.grdPOView.CurrentRow.Cells("Col_PurchaseOrder_No").Value.ToString
                If Me.grdPOView.CurrentRow.Cells("Col_SalesUnit").Value Is Nothing Then
                    Me.grdPOView.CurrentRow.Cells("Col_SalesUnit").Value = ""
                End If
                SalseUnit = IIf(String.IsNullOrEmpty(Me.grdPOView.CurrentRow.Cells("Col_SalesUnit").Value.ToString), "", Me.grdPOView.CurrentRow.Cells("Col_SalesUnit").Value.ToString)
                If New clsPR().Cancel_PurchaseOrder(oPo) = True Then
                    MessageBox.Show("ยกเลิกรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    getPOViewSearch()
                Else
                    MessageBox.Show("ไม่สามารถยกเลิกรายการได้ ระบบทำงานผิดพลาด", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
                objPOTransaction = Nothing
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
            objPOTransaction = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Button to confirm PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click

        ' ====== TODO: HARDCODE-MSG   
        ' ====== Todd: 26 Dec 2009 - Need to get rid of hardcode text message in Thai.
        If grdPOView.Rows.Count <= 0 Then Exit Sub

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

        Dim checkError As String = ""

        For i As Integer = 0 To Me.grdPOView.Rows.Count - 1
            If Me.grdPOView.Rows(i).Cells("chkSelect").Value = True And Me.grdPOView.Rows(i).Cells("Col_Status_Desc").Value = "รอยืนยัน" Then
                objPOTransaction.newPurchaseOrder_Index = Me.grdPOView.Rows(i).Cells("Col_PurchaseOrder_Index").Value.ToString
                oPo.PurchaseOrder_Index = Me.grdPOView.Rows(i).Cells("Col_PurchaseOrder_Index").Value.ToString
                oPo.PurchaseOrder_No = Me.grdPOView.Rows(i).Cells("Col_PurchaseOrder_No").Value.ToString
                Try

                    If objPOTransaction.Confirm_PO(oPo) Then
                        checkError = "ยืนยันรายการเรียบร้อยแล้ว"
                        '   MessageBox.Show("ยืนยันรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        '     getPOViewSearch()
                    Else
                        checkError = "ไม่สามารถยืนยันรายการ : " & oPo.PurchaseOrder_No & " ได้ ระบบทำงานผิดพลาด"
                        Exit For
                        '  MessageBox.Show("ไม่สามารถยืนยันรายการได้ ระบบทำงานผิดพลาด", "ผลการยืนยัน", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    End If
                    '    objPOTransaction = Nothing

                Catch ex As Exception
                    W_MSG_Error(ex.Message)
                    objPOTransaction = Nothing
                End Try
            End If
        Next

        W_MSG_Information(checkError)
        getPOViewSearch()

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
    ''' Button to print document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

        Try

            If Report_Name = "PO_ScanPrintOut" Or Report_Name = "PO_ScanPrintOut2" Or Report_Name = "PO_Discrepancy" Then
                Dim po_index As String = ""
                For i As Integer = 0 To grdPOView.Rows.Count - 1
                    If grdPOView.Rows(i).Cells("chkSelect").Value = True Then
                        po_index = po_index & "'" & grdPOView.Rows(i).Cells("Col_PurchaseOrder_Index").Value & "',"
                    End If
                Next

                Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, "And PurchaseOrder_Index in ('" & po_index.Substring(1, po_index.Length - 2) & ")")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
            ElseIf Report_Name = "PO_TopCharoen" Then
                Dim po_index As String = ""
                For i As Integer = 0 To grdPOView.Rows.Count - 1
                    If grdPOView.Rows(i).Cells("chkSelect").Value = True Then
                        po_index = po_index & "'" & grdPOView.Rows(i).Cells("Col_PurchaseOrder_Index").Value & "',"
                    End If
                Next

                Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, "And PurchaseOrder_Index in ('" & po_index.Substring(1, po_index.Length - 2) & ")")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
            Else

                Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, "And PurchaseOrder_Index ='" & grdPOView.CurrentRow.Cells("Col_PurchaseOrder_Index").Value & "'")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
            End If

        Catch ex As Exception
            W_MSG_Error("เลือกรายการที่จะพิมพ์ก่อน")

        Finally

        End Try

    End Sub

#End Region

#Region " DATAGRID EVENTS "

    ''' <summary>
    ''' Double click to Edit PO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdPOView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOView.CellDoubleClick
        Try
            btnEdit_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' We just refresh number of rows when a row is removed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdPO_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdPOView.RowsRemoved
        SetnumRows()
    End Sub

    ''' <summary>
    ''' We just refresh number of rows when a row is added.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdPO_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdPOView.RowsAdded
        SetnumRows()
    End Sub

    ''' <summary>
    ''' This sub manages button controls when we change row selection in datagrid.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdPOView_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPOView.SelectionChanged
        If grdPOView.CurrentRow Is Nothing Then Exit Sub
        rowstate()
    End Sub
    Sub rowstate()
        Try
            If grdPOView.CurrentRow.Selected Then
                Me.btnClose_PO.Enabled = False
                Select Case grdPOView.CurrentRow.Cells("Col_Status").Value

                    Case "-1"
                        ' ====== Case of "CANCELLED" Document (ยกเลิก)
                        Me.btnCancel.Enabled = False
                        Me.btnEdit.Enabled = False
                        Me.btnConfirm.Enabled = False
                        Me.btnRecived.Enabled = False

                    Case "1"
                        ' ====== Case of "TO BE CONFIRMED" Document (รอยืนยัน)
                        Me.btnCancel.Enabled = True
                        Me.btnEdit.Enabled = True
                        Me.btnConfirm.Enabled = True
                        Me.btnRecived.Enabled = False

                    Case "2"
                        ' ====== Case of "PENDING RECEIVED" Document (ค้างรับ)
                        Me.btnCancel.Enabled = True
                        Me.btnEdit.Enabled = True
                        Me.btnConfirm.Enabled = False
                        Me.btnRecived.Enabled = True
                        Me.btnClose_PO.Enabled = True
                    Case "3"
                        ' ====== Case of "COMPLETED" Document (เสร็จสิ้น)
                        Me.btnCancel.Enabled = False
                        Me.btnEdit.Enabled = True
                        Me.btnConfirm.Enabled = False
                        Me.btnRecived.Enabled = False

                    Case Else
                        Me.btnCancel.Enabled = False
                        Me.btnEdit.Enabled = False
                        Me.btnConfirm.Enabled = False
                        Me.btnRecived.Enabled = False

                End Select
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " GENERIC FUNCTIONS AND SUBS "

    ''' <summary>
    ''' Count display rows and show result.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetnumRows()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = grdPOView.Rows.Count
        If numRows > 0 Then
            lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows.Text = "ไม่พบรายการ"
        End If
    End Sub
    Private Sub GetDocumentType(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_DocumentType(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            Dim cbItem As DataRow
            cbItem = objDT.NewRow
            cbItem("DocumentType_Index") = "-11"
            cbItem("Description") = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)

            ' ***** Using add comboboxcolumn *****
            With cbPOType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With


            ' *************************************
            cbPOType.SelectedIndex = cbPOType.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' This is the main function to show all data in data grid view.
    ''' SQL statement is builded in this sub.
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Todd 26 Dec 2009
    ''' 
    ''' ====== TODO: REMOVE SQL from presentation layer.
    ''' ====== Remark by Todd: We want to add more search criteria to PO, 
    ''' ====== but we need to modify VIEW_PO_View, so just leave to next version.
    ''' </remarks>
    Private Sub getPOViewSearch()
        If fLoad = False Then Exit Sub
        Dim objtb_PurchaseOrder As New tb_PurchaseOrder
        Dim objDT As DataTable = New DataTable
        'Dim objDTtb_PurchaseOrder As DataTable = New DataTable
        Dim strWhere As String = ""
        'Add By ton 24/04/2015_________________
        Dim caseloadAll As Boolean
        caseloadAll = rdAll.Checked
        Dim CaseLoad As Boolean = False
        CaseLoad = rdTop.Checked
        If rdRowPage.Checked = True Then
            If cboRowPerPage.Text = "" Then
                cboRowPerPage.Text = 50
            End If

        End If
        '______________________________________

        Try


            If Me.radDate.Checked = True Then
                Dim intDay As Integer = Me.dtpBeginDate.Value.Day
                Dim intMonth As Integer = Me.dtpBeginDate.Value.Month
                Dim intYear As Integer = Me.dtpBeginDate.Value.Year
                If intYear > 2500 Then
                    intYear = intYear - 543
                End If

                ' ------ Filter by Date Range
                Dim StartDate As String = Format(dtpBeginDate.Value, "MM/dd/yyyy").ToString() + " 00:00:00"
                Dim EndtDate As String = Format(dtpEndDate.Value.AddDays(1), "MM/dd/yyyy").ToString() + " 00:00:00"
                'strWhere &= " AND (PurchaseOrder_Date between '" & StartDate & "' AND  '" & EndtDate & "'"
                strWhere = " AND ((PurchaseOrder_Date >= '" & StartDate & "') AND (PurchaseOrder_Date < '" & EndtDate & "'))"
            End If

            If Me.rdDueDate.Checked = True Then
                Dim intDay As Integer = Me.dtpBeginDate.Value.Day
                Dim intMonth As Integer = Me.dtpBeginDate.Value.Month
                Dim intYear As Integer = Me.dtpBeginDate.Value.Year
                If intYear > 2500 Then
                    intYear = intYear - 543
                End If

                ' ------ Filter by Date Range
                Dim StartDate As String = Format(dtpBeginDate.Value, "MM/dd/yyyy").ToString() + " 00:00:00"
                Dim EndtDate As String = Format(dtpEndDate.Value.AddDays(1), "MM/dd/yyyy").ToString() + " 00:00:00"
                'strWhere &= " AND (PurchaseOrder_Date between '" & StartDate & "' AND  '" & EndtDate & "'"
                strWhere = " AND ((Expected_Delivery_Date >= '" & StartDate & "') AND (Expected_Delivery_Date < '" & EndtDate & "'))"
            End If



            If Me.radNo.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere = " AND (PurchaseOrder_No like '%" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%') "
                End If
            End If

            If Me.radSupplier_Name.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere = " AND (Supplier_Name LIKE '" & Me.txtKeySearch.Text & "%') "
                End If
            End If
            If Me.radSupId.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere = " AND (Supplier_ID Like '" & Me.txtKeySearch.Text & "%') "
                End If
            End If

            If Me.rboCustomer.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere = " AND (Customer_Id Like '%" & Me.txtKeySearch.Text & "%') "
                End If
            End If

            Select Case Me.cbPOType.SelectedValue
                Case "-11", Nothing
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (DocType_Index='" & Me.cbPOType.SelectedValue & "') "
            End Select


            Select Case Me.cboDocumentStatus.SelectedValue
                Case 0
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (Status = " & Me.cboDocumentStatus.SelectedValue & ") "
            End Select

            Select Case Me.cboProductType.SelectedValue
                Case ""
                    strWhere &= " "
                Case Else
                    strWhere &= String.Format(" and PurchaseOrder_Index in (select tb_poi.PurchaseOrder_Index from tb_PurchaseOrderItem as tb_poi where tb_poi.Status<>-1 and tb_poi.Sku_Index in (select ms_s.Sku_Index from ms_Sku as ms_s left join ms_Product as ms_pd on ms_pd.Product_Index=ms_s.Product_Index left join ms_ProductType as ms_pdt on ms_pdt.ProductType_Index=ms_pd.ProductType_Index where ms_pdt.ProductType_Index='{0}')) ", Me.cboProductType.SelectedValue)
            End Select


            'Dim arrSku() As String = txtSku.Text.Split(vbNewLine)
            'Dim skuList As New List(Of String)
            'For Each str As String In arrSku
            '    If (Not str.Trim() = Nothing) Then
            '        skuList.Add(str.Trim())
            '    End If
            'Next
            'If (skuList.Count > 0) Then
            '    Dim str As String = String.Join("','", skuList.ToArray())
            '    strWhere &= String.Format(" and PurchaseOrder_Index in (select tb_poi.PurchaseOrder_Index from tb_PurchaseOrderItem as tb_poi where tb_poi.Status<>-1 and tb_poi.Sku_Index in (select ms_s.Sku_Index from ms_Sku as ms_s where ms_s.Sku_Id in ('{0}'))) ", str)
            'End If

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()

            Me.cboRowPerPage.SelectedIndex = IIf(Me.cboRowPerPage.SelectedIndex > 0, Me.cboRowPerPage.SelectedIndex, 0)

            'Add By ton 20/04/2015_____
            If caseloadAll = False Then
                If CaseLoad = False Then ' True : Top100 || False : Paging All Update By ton 2015/04/24
                    Dim odtSO_Count As New DataTable
                    objtb_PurchaseOrder.getPOViewSearch_Count(strWhere)
                    odtSO_Count = objtb_PurchaseOrder.DataTable

                    ' Get total records of the current search
                    If odtSO_Count.Rows.Count > 0 Then
                        Me.txtRowCount.Text = odtSO_Count.Rows(0)("Row_Total").ToString
                    Else
                        Me.txtRowCount.Text = 0
                    End If

                End If

            End If

            ' Calculate Paging
            Call Calculate_Paging()

            Dim oReader As New DataTable 'Data.SqlClient.SqlDataReader
            objtb_PurchaseOrder.getPOSearch_Reader(strWhere, gintRowStart, gintRowEnd, CaseLoad, txtTop.Text, caseloadAll)
            oReader = objtb_PurchaseOrder.DataTable
            'objtb_SalesOrder.getSOViewSearch(strWhere)
            objDT = objtb_PurchaseOrder.DataTable()
            'oReader.Close()
            'objDT = objtb_SalesOrder.DataTable

            'Update By Art 13-06-2012 : ค้นหาต่อท้ายของเก่า
            With objDT.Columns
                If Not .Contains("chkSelect") Then
                    .Add("chkSelect", GetType(Boolean))
                End If
            End With
            With objDT.Columns
                If Not .Contains("Count") Then
                    .Add("Count", GetType(Integer))
                End If
            End With

            '__________________________

            'objtb_PurchaseOrder.PO_SearchMain(strWhere)  'Comment By ton 20/04/2015_____
            'objDT = objtb_PurchaseOrder.DataTable

            Me.grdPOView.Rows.Clear()
            Me.grdPOView.Refresh()


            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdPOView
                    Me.grdPOView.Rows.Add()
                    .Rows(i).Cells("Col_PurchaseOrder_No").Value = objDT.Rows(i).Item("PurchaseOrder_No").ToString
                    .Rows(i).Cells("Col_PurchaseOrder_Date").Value = Format(CDate(objDT.Rows(i).Item("PurchaseOrder_Date")), "dd/MM/yyyy").ToString
                    '  *** Reference By Tag property  to check Enable Button ***
                    .Rows(i).Cells("Col_Supplier_Name").Value = objDT.Rows(i).Item("Supplier_Name").ToString
                    .Rows(i).Cells("Col_PurchaseOrder_Index").Value = objDT.Rows(i).Item("PurchaseOrder_Index").ToString
                    .Rows(i).Cells("Col_Process_Id").Value = objDT.Rows(i).Item("Process_Id").ToString
                    .Rows(i).Cells("Col_Status").Value = objDT.Rows(i).Item("Status").ToString
                    .Rows(i).Cells("Col_Status_Desc").Value = objDT.Rows(i).Item("Description").ToString
                    .Rows(i).Cells("Col_Qty").Value = objDT.Rows(i).Item("Qty").ToString
                    .Rows(i).Cells("Col_Weight").Value = objDT.Rows(i).Item("Weight").ToString
                    .Rows(i).Cells("Col_Net_Amt").Value = objDT.Rows(i).Item("Net_Amt").ToString
                    .Rows(i).Cells("Col_Remark").Value = objDT.Rows(i).Item("Remark").ToString
                    .Rows(i).Cells("Col_Addby").Value = objDT.Rows(i).Item("add_by").ToString
                    .Rows(i).Cells("Col_DocType_No").Value = objDT.Rows(i).Item("DocType_No").ToString
                    .Rows(i).Cells("Col_DocType_Index").Value = objDT.Rows(i).Item("DocType_Index").ToString
                    'Pending_Qty
                    'Expected_Delivery_Date
                    'Received_Qty
                    'DueDate
                    .Rows(i).Cells("Col_Expected_Delivery_Date").Value = Format(CDate(objDT.Rows(i).Item("Expected_Delivery_Date")), "dd/MM/yyyy").ToString
                    .Rows(i).Cells("Col_Total_Received_Qty").Value = objDT.Rows(i).Item("Total_Received_Qty").ToString
                    .Rows(i).Cells("Col_Total_Pending_Qty").Value = objDT.Rows(i).Item("Total_Pending_Qty").ToString
                    If .Rows(i).Cells("Col_Status").Value = "-1" Then
                        .Rows(i).Cells("Col_Total_Received_Qty").Style.BackColor = Color.Pink
                        .Rows(i).Cells("Col_Total_Pending_Qty").Style.BackColor = Color.Pink
                    ElseIf .Rows(i).Cells("Col_Total_Received_Qty").Value = 0 And .Rows(i).Cells("Col_Total_Pending_Qty").Value > 0 Then
                        .Rows(i).Cells("Col_Total_Received_Qty").Style.BackColor = Color.LightSalmon
                        .Rows(i).Cells("Col_Total_Pending_Qty").Style.BackColor = Color.LightSalmon
                    ElseIf .Rows(i).Cells("Col_Total_Received_Qty").Value > 0 And .Rows(i).Cells("Col_Total_Pending_Qty").Value > 0 Then
                        .Rows(i).Cells("Col_Total_Received_Qty").Style.BackColor = Color.LightYellow
                        .Rows(i).Cells("Col_Total_Pending_Qty").Style.BackColor = Color.LightYellow
                    ElseIf .Rows(i).Cells("Col_Total_Received_Qty").Value > 0 And .Rows(i).Cells("Col_Total_Pending_Qty").Value = 0 Then
                        .Rows(i).Cells("Col_Total_Received_Qty").Style.BackColor = Color.LightGreen
                        .Rows(i).Cells("Col_Total_Pending_Qty").Style.BackColor = Color.LightGreen
                    End If

                    'Add Show Owner
                    .Rows(i).Cells("Col_Customer_Name").Value = objDT.Rows(i).Item("Customer_Name").ToString
                    .Rows(i).Cells("Col_Customer_Id").Value = objDT.Rows(i).Item("Customer_Id").ToString

                    'DocType_No
                    Dim intStatus As Integer = objDT.Rows(i).Item("Status")
                    Select Case intStatus
                        Case -1 '- ยกเลิก - ชมพู
                            .Rows(i).Cells("Col_PurchaseOrder_No").Style.BackColor = Drawing.Color.Pink
                        Case 2 '- ค้างรับ - เหลือง
                            .Rows(i).Cells("Col_PurchaseOrder_No").Style.BackColor = Drawing.Color.LightYellow
                        Case 3 '- เสร็จสิ้น - เขียว
                            .Rows(i).Cells("Col_PurchaseOrder_No").Style.BackColor = Drawing.Color.LightGreen

                        Case Else

                    End Select
                End With
            Next
            btnAdd.Focus()
            Me.SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_PurchaseOrder = Nothing
            'objDTtb_PurchaseOrder = Nothing
        End Try
    End Sub

#End Region

#Region " UNUSED & BACKUP FUNCTIONS(TEMPORARY) "

    ''' <summary>
    ''' Remark: 
    ''' Todd 26 Dec 2009 - Not sure where this sub is used. 
    ''' It was not called anywhere within this page.
    ''' </summary>
    ''' <param name="pstrStatus"></param>
    ''' <remarks></remarks>
    Sub getPOview(Optional ByVal pstrStatus As String = "")
        Dim objtb_PurchaseOrder As New tb_PurchaseOrder
        Try

            objtb_PurchaseOrder.PO_ShowMain("" & pstrStatus)
            grdPOView.DataSource = objtb_PurchaseOrder.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objtb_PurchaseOrder = Nothing
        End Try
    End Sub
#End Region

    Private Sub grdPOView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOView.CellClick
        rowstate()
    End Sub

    Private Sub txtKeySearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then

                Me.getPOViewSearch()


            End If
        Catch ex As Exception
            Throw ex

        End Try
    End Sub


    Public Sub getPoType()
        Try

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboDocumentStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentStatus.SelectedIndexChanged
        Try
            getPOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cbPOType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbPOType.SelectedIndexChanged
        Try
            getPOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnRecived_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRecived.Click
        ' Check Status 

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If



        If grdPOView.CurrentRow.Cells("Col_Status").Value = 2 Then

            Dim frm As New frmDeposit_WMS_V2(frmDeposit_WMS_V2.enuOperation_Type.ADDNEW)
            frm.Receive_Process = frmDeposit_WMS_V2.Receive_Process_Enum.PO
            frm.DocumentPlan_Index = grdPOView.CurrentRow.Cells("Col_PurchaseOrder_Index").Value
            frm.DocumentPlan_No = grdPOView.CurrentRow.Cells("Col_PurchaseOrder_No").Value
            frm.strDocumentType = grdPOView.CurrentRow.Cells("Col_DocType_Index").Value
            frm.ShowDialog()
            getPOViewSearch()
        Else
            W_MSG_Information("สถานะของใบสั่งซื้อไม่ถูกต้อง")
        End If


        'Dim objCustomSetting As New config_CustomSetting
        'Dim checkUpdateORD As Boolean = objCustomSetting.getConfig_Key_USE("frmPOcheckUpdatefrmORD")
        'If checkUpdateORD = False Then
        '    If grdPOView.CurrentRow.Cells("Col_Status").Value = 2 Then
        '        Dim frm As New frmDeposit_WMS(frmDeposit_WMS.enuOperation_Type.ADDNEW)
        '        frm.strPlanReceive = grdPOView.CurrentRow.Cells("Col_PurchaseOrder_No").Value
        '        frm.AutoPlanByProcess_id = grdPOView.CurrentRow.Cells("Col_Process_Id").Value
        '        frm.ReceiveType = 0
        '        frm.ShowDialog()
        '        getPOViewSearch()
        '    Else
        '        W_MSG_Information("สถานะของใบสั่งซื้อไม่ถูกต้อง")
        '    End If
        'Else
        '    Dim frmUpdate As New Object()

        '    If grdPOView.CurrentRow.Cells("Col_Status").Value = 2 Then
        '        frmUpdate = New Form()
        '        frmUpdate = "frmDeposit_WMS_Update(frmDeposit_WMS_Update.enuOperation_Type.ADDNEW)"
        '        'Dim frm As New frmUpdate 'frmDeposit_WMS_Update(frmDeposit_WMS_Update.enuOperation_Type.ADDNEW)
        '        'frmUpdate = New FormCollection()
        '        'frmUpdate = "frmDeposit_WMS_Update(frmDeposit_WMS_Update.enuOperation_Type.ADDNEW)"
        '        frmUpdate.strPlanReceive = ""
        '        frmUpdate.strPlanReceive = grdPOView.CurrentRow.Cells("Col_PurchaseOrder_No").Value
        '        frmUpdate.AutoPlanByProcess_id = grdPOView.CurrentRow.Cells("Col_Process_Id").Value
        '        frmUpdate.ReceiveType = 0
        '        frmUpdate.ShowDialog()
        '        getPOViewSearch()
        '    Else
        '        W_MSG_Information("สถานะของใบสั่งซื้อไม่ถูกต้อง")
        '    End If
        'End If

    End Sub

    Private Sub grdPOView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPOView.CellContentClick

    End Sub

    Private Sub frmPOView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New frmConfigPurchaseOrder
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 9)
                    oFunction.SW_Language_Column(Me, Me.grdPOView, 9)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub Calculate_Paging()
        ' Calculate Paging 
        Try
            Dim intRowCount As Integer
            Dim intRowPerPage As Integer

            intRowCount = CInt(Me.txtRowCount.Text)
            intRowPerPage = CInt(Me.cboRowPerPage.Text)

            ' row count = 0 ; page = 1 : total page = 1
            If intRowCount = 0 Or (intRowCount <= intRowPerPage) Then
                Me.txtTotalPage.Text = 1
                Me.txtPageIndex.Text = 1
            Else
                Me.txtTotalPage.Text = CInt(intRowCount / intRowPerPage)

                If CInt(Me.txtTotalPage.Text) * intRowPerPage < intRowCount Then
                    Me.txtTotalPage.Text = CInt(Me.txtTotalPage.Text) + 1
                End If
            End If

            Me.txtPageIndex.Text = IIf(IsNumeric(txtPageIndex.Text), Me.txtPageIndex.Text, 1)

            'Enable Button
            If CInt(Me.txtPageIndex.Text) = 1 Then
                gintRowStart = 1
                gintRowEnd = intRowPerPage

                Me.btnPagePrev.Enabled = False
                Me.btnPageFirst.Enabled = False
            Else
                gintRowEnd = CInt(Me.txtPageIndex.Text) * intRowPerPage
                gintRowStart = gintRowEnd - intRowPerPage + 1

                Me.btnPagePrev.Enabled = True
                Me.btnPageFirst.Enabled = True
            End If

            If CInt(Me.txtPageIndex.Text) = CInt(Me.txtTotalPage.Text) Then
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
            Me.txtPageIndex.Text = 1
            Me.getPOViewSearch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            Me.txtPageIndex.Text -= 1
            Me.getPOViewSearch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            Me.txtPageIndex.Text += 1
            Me.getPOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            Me.txtPageIndex.Text = Me.txtTotalPage.Text
            Me.getPOViewSearch()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPageIndex_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPageIndex.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If IsNumeric(Me.txtPageIndex.Text) Then
                    If CInt(Me.txtPageIndex.Text) > CInt(Me.txtTotalPage.Text) Then
                        Me.txtPageIndex.Text = Me.txtTotalPage.Text
                    End If
                    Me.txtPageIndex.Text = Trim(Me.txtPageIndex.Text)
                    '  Calculate_Paging()
                    Me.getPOViewSearch()
                Else
                    Me.txtPageIndex.Text = 1
                    ' Calculate_Paging()
                    Me.getPOViewSearch()
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPayment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPayment.Click

    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rboCustomer.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        Me.lblto.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub getProductType()

        Dim objClassDB As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProductType()
            objDT = objClassDB.DataTable

            Dim dr As DataRow = objDT.NewRow()
            dr("Description") = "-- แสดงทุกรายการ --"
            dr("ProductType_Index") = ""
            objDT.Rows.InsertAt(dr, 0)

            With Me.cboProductType
                .DisplayMember = "Description"
                .ValueMember = "ProductType_Index"
                .DataSource = objDT
            End With
            cboProductType.SelectedIndex = 0

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            Dim frm As New frmReportsPO()
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
     
    End Sub

    Private Sub btnClose_PO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose_PO.Click
        'KSL
        Try
            If grdPOView.Rows.Count <= 0 Then Exit Sub

            Dim xSql As String = ""
            Dim xDB As New DBType_SQLServer
            Dim xdt As New DataTable

            Dim xPurchaseOrder_Index As String = Me.grdPOView.Rows(grdPOView.CurrentRow.Index).Cells("Col_PurchaseOrder_Index").Value

            xSql = String.Format("select * from tb_PurchaseOrder where PurchaseOrder_Index = '{0}'", xPurchaseOrder_Index)
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
                        xSql &= String.Format(", Close_by = '{0}', Close_date = getdate()", W_Module.WV_UserName)
                        xSql &= String.Format("where PurchaseOrder_Index = '{0}'", xPurchaseOrder_Index)
                        xDB.DBExeNonQuery(xSql)

                        'insert log
                        Dim obj_cls As New cls_syAditlog
                        obj_cls.Process_ID = 9
                        obj_cls.Description = "ยืนยัน : " & Now.ToString("dd/MM/yyyy HH:mm:ssss")
                        obj_cls.Document_Index = xPurchaseOrder_Index
                        obj_cls.Document_No = xdt.Rows(0).Item("PurchaseOrder_No").ToString
                        obj_cls.Log_Type_ID = 903 'ปิด PO
                        obj_cls.Insert_Master()


                        W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")
                        Me.getPOViewSearch()
                        'Me.Close()
                End Select
            Else
                Exit Sub
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImportExcelPo.Click
        Try
            Dim ofrm As New frmImport_PO_TCR
            ofrm.Icon = Me.Icon
            ofrm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkAllSelect_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllSelect.CheckedChanged
        Try

            For i As Integer = 0 To Me.grdPOView.Rows.Count - 1
                Me.grdPOView.Rows(i).Cells("chkSelect").Value = chkAllSelect.Checked
            Next

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
End Class