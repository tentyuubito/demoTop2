Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Function
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_OUTB_Report

Public Class frmPrintPalletSlip

#Region " Property "

    Private _frmDeposit_WMS As frmDeposit_WMS_V2
    Public Property frmDeposit_WMS() As frmDeposit_WMS_V2
        Get
            Return _frmDeposit_WMS
        End Get
        Set(ByVal value As frmDeposit_WMS_V2)
            _frmDeposit_WMS = value
        End Set
    End Property

    Private _Order_Index As String
    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property

    Private _DocumentType_Index As String
    Public Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Private _Customer_Index As String
    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Private _Order_Date As Date
    Public Property Order_Date() As Date
        Get
            Return _Order_Date
        End Get
        Set(ByVal value As Date)
            _Order_Date = value
        End Set
    End Property

    Private Dimension_Width As Decimal = 0
    Private Dimension_Length As Decimal = 0
    Private Dimension_Height As Decimal = 0

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPrintPalletSlip_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtOrder_Date.Text = Me.Order_Date.ToString("dd/MM/yyyy")
            Me.getCboItemStatus()
            Me.getDocumentType(Me.DocumentType_Index)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnSku_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me._Customer_Index)
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()
            If frm.Sku_Index = Nothing Then
                Exit Sub
            Else
                ' Clear Sku_Detail
                Me.txtSku.Tag = ""
                Me.txtSku.Text = ""
                Me.txtSku_Desc.Text = ""
                Me.dtpPosting_Date.Value = Now.ToString("yyyy-MM-dd")
                Me.dtpPosting_Date.Value = Now.ToString("yyyy-MM-dd")
                Me.dtpPosting_Date.Checked = False
                Me.dtpExpire_Date.Checked = False
                Me.txtPlot.Text = ""
                Me.txtPalletQty.Text = ""
                Me.Dimension_Width = 0
                Me.Dimension_Length = 0
                Me.Dimension_Height = 0
                Me.txtUnitVolume.Text = ""
                Me.txtUnitWeight.Text = ""
                Me.lblUnitMain.Visible = False
                Me.lblUnitMainQty.Text = ""
                Me.lblUnitMainPackage.Tag = "0"
                Me.lblUnitMainPackage.Text = ""
                Me.txtQtyPerPallet.Text = ""

                Me.getCboPackageSku(frm.Sku_Index)
                If (cboPackageSku.Items.Count > 0) Then
                    Me.txtSku.Tag = frm.Sku_Index
                    Me.txtSku.Text = frm.Sku_ID
                    Me.txtSku_Desc.Text = frm.Sku_Des_th
                    Me.getSku_Detail(Me.txtSku.Text, Me.Customer_Index)
                    If (cboPackageSku.Items.Count > 0) Then
                        Me.getSkuRatio(Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, Me.txtQtyPerPallet.Text)
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            ' Validate
            If (Me.txtSku.Tag = Nothing) Then
                MessageBox.Show("กรุณาระบุสินค้า", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.btnSku.Focus()
                Exit Sub
            End If
            If (Not IsNumeric(Me.txtQtyPerPallet.Text)) Then
                MessageBox.Show("จำนวนไม่ถูกต้อง", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtQtyPerPallet.Focus()
                Exit Sub
            End If
            If (Not CDec(Me.txtQtyPerPallet.Text) > 0) Then
                MessageBox.Show("จำนวนต้องมากกว่า 0", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtQtyPerPallet.Focus()
                Exit Sub
            End If
            If (Not IsNumeric(Me.lblUnitMainQty.Text)) Then
                MessageBox.Show("จำนวนหน่วยหลักไม่ถูกต้อง", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If (Not IsNumeric(Me.lblUnitMainPackage.Tag)) Then
                MessageBox.Show("Ratio ไม่ถูกต้อง", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If (Not CDec(Me.lblUnitMainPackage.Tag) > 0) Then
                MessageBox.Show("Ratio ต้องมากกว่า 0", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If (Not (Me.cboItemStatus.Items.Count) > 0) Then
                MessageBox.Show("ไม่พบสถานะสินค้า", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            If (New clsPrintPalletSlip().CheckSKUSerial(Me.txtSku.Tag)) Then
                MessageBox.Show("สินค้ามี Serial ไม่สามารถใช้งานฟังก์ชั่นนี้ได้", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.btnSku.Focus()
                Exit Sub
            End If
            If (CDec(Me.lblUnitMainQty.Text) > CDec(Me.txtPalletQty.Text)) Then
                If (MessageBox.Show("จำนวน/Pallet เกินกำหนดต้องการทำต่อหรือไม่", "ข้อความ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No) Then
                    Exit Sub
                End If
            End If

            ' Save OrderItem and Tag
            Dim Tag_Index As String = ""
            Dim Tag_No As String = ""
            Me.frmDeposit_WMS.SavePalletSlip(Tag_Index, Tag_No, Me.txtRefNo1.Text.Trim(), Me.txtRefNo2.Text.Trim(), Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, CDec(Me.txtQtyPerPallet.Text), CDec(Me.lblUnitMainPackage.Tag), CDec(Me.lblUnitMainQty.Text), Me.cboItemStatus.SelectedValue, Me.txtPlot.Text, IIf(Me.dtpPosting_Date.Checked, Me.dtpPosting_Date.Value.ToString("yyyy-MM-dd"), ""), IIf(Me.dtpExpire_Date.Checked, Me.dtpExpire_Date.Value.ToString("yyyy-MM-dd"), ""), CDec(Me.txtUnitWeight.Text), CDec(Me.txtUnitVolume.Text), CDec(Me.Dimension_Width), CDec(Me.Dimension_Length), CDec(Me.Dimension_Height))

            Me.txtTag_No.Text = Tag_No

            ' Print
            If (Not (Tag_Index = Nothing)) Then Me.PrintTag(Tag_Index, Tag_No, Me.chkPreview.Checked)

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub getDocumentType(ByVal DocumentType_Index As String)
        Dim objClass As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDt As DataTable = New DataTable
        Try
            objClass.getDocumentTypeByIndex(DocumentType_Index)
            objDt = objClass.DataTable
            If (objDt.Rows.Count() > 0) Then
                With objDt.Rows(0)
                    Me.txtDocumentType.Text = .Item("Description")
                End With
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClass = Nothing
            objDt = Nothing
        End Try
    End Sub

    Private Sub getCboPackageSku(ByVal Sku_Index As String)
        Try
            Dim objClass As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDt As DataTable = New DataTable
            Try
                objClass.getSKU_Package(Sku_Index)
                objDt = objClass.DataTable
                With cboPackageSku
                    .DisplayMember = "Package"
                    .ValueMember = "Package_Index"
                    .DataSource = objDt
                    If (.Items.Count > 0) Then
                        .Enabled = True
                    Else
                        .Enabled = False
                    End If
                End With
            Catch ex As Exception
                Throw ex
            Finally
                objClass = Nothing
                objDt = Nothing
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getCboItemStatus()
        Try
            Dim objClass As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
            Dim objDt As DataTable = New DataTable
            Try
                objClass.getItemStatus()
                objDt = objClass.DataTable
                With cboItemStatus
                    .DisplayMember = "ItemStatus"
                    .ValueMember = "ItemStatus_Index"
                    .DataSource = objDt
                    If (.Items.Count > 0) Then
                        .Enabled = True
                    Else
                        .Enabled = False
                    End If
                End With
            Catch ex As Exception
                Throw ex
            Finally
                objClass = Nothing
                objDt = Nothing
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getSku_Detail(ByVal Sku_Id As String, ByVal Customer_Index As String)
        Try
            Dim objClass As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDt As DataTable = New DataTable
            Try
                objClass.getSKU_Detail_Equal_ByCustomer(Sku_Id, Customer_Index)
                objDt = objClass.DataTable
                If (objDt.Rows.Count() > 0) Then
                    With objDt.Rows(0)
                        Me.txtPalletQty.Text = .Item("Qty_Per_Pallet")
                        Me.Dimension_Width = .Item("_W")
                        Me.Dimension_Length = .Item("_L")
                        Me.Dimension_Height = .Item("_H")
                        Me.txtUnitVolume.Text = .Item("Unit_Volume")
                        Me.txtUnitWeight.Text = .Item("UnitWeight")
                        Me.lblUnitMain.Visible = True
                        Me.lblUnitMainQty.Text = "-"
                        Me.lblUnitMainPackage.Tag = "0"
                        Me.lblUnitMainPackage.Text = .Item("Sku_PackageDescription")
                        If (IsNumeric(.Item("Qty_Per_Pallet"))) Then
                            Me.txtQtyPerPallet.Text = .Item("Qty_Per_Pallet").ToString().Trim("0").Trim(".")
                        End If
                    End With
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objClass = Nothing
                objDt = Nothing
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getSkuRatio(ByVal Sku_Index As String, ByVal Package_Index As String, ByVal Qty As String)
        Try
            If (Sku_Index = Nothing Or Package_Index = Nothing Or Not IsNumeric(Qty)) Then
                Me.lblUnitMainQty.Text = "-"
                Me.lblUnitMainPackage.Tag = "0"
                Exit Sub
            End If
            Dim objClass As New ms_SKURatio(ms_SKURatio.enuOperation_Type.SEARCH)
            Dim objDt As DataTable = New DataTable
            Try
                objClass.SelectData_ByPackage(Sku_Index, Package_Index)
                objDt = objClass.DataTable
                If (objDt.Rows.Count() > 0) Then
                    With objDt.Rows(0)
                        Me.lblUnitMainPackage.Tag = "0"
                        If (IsNumeric(.Item("Ratio"))) Then Me.lblUnitMainPackage.Tag = .Item("Ratio")
                        Me.lblUnitMainQty.Text = CDec(.Item("Ratio") * CDec(Qty)).ToString("#,##0")
                    End With
                End If
            Catch ex As Exception
                Throw ex
            Finally
                objClass = Nothing
                objDt = Nothing
            End Try
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboPackageSku_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPackageSku.SelectedValueChanged
        Try
            If (cboPackageSku.Items.Count = 0) Then Exit Sub
            Me.txtQtyPerPallet.Text = ""
            Me.getSkuRatio(Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, Me.txtQtyPerPallet.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtQtyPerPallet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyPerPallet.KeyPress
        e.Handled = ValidatedCurrency(Asc(e.KeyChar))
    End Sub

    Private Function ValidatedCurrency(ByVal Asc As Integer) As Boolean
        'Allowed ctrl-x
        If (Asc = 24) Then Return False
        'Allowed ctrl-c
        If (Asc = 3) Then Return False
        'Allowed ctrl-v
        If (Asc = 22) Then Return False
        'Allowed .
        If (Asc = 46) Then Return False
        'Allowed number
        If (Asc >= 48) And (Asc <= 57) Then Return False
        ' Allowed backspace
        If (Asc = 8) Then Return False
        ' Allowed delete
        If (Asc = 127) Then Return False
        Return True
    End Function

    Private Sub txtQtyPerPallet_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyPerPallet.TextChanged
        Try
            If (cboPackageSku.Items.Count = 0) Then Exit Sub
            Me.getSkuRatio(Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, Me.txtQtyPerPallet.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles btnPrint.KeyDown
        Try
            If (e.KeyCode = Keys.Enter) Then btnPrint_Click(sender, e)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintTag(ByVal Tag_Index As String, ByVal Tag_No As String, ByVal IsPreview As Boolean)
        Dim oReport As New clsReport()
        Dim Report_Name As String = "TagStickerPrintOut"
        Try
            Dim strWhere As String = String.Format(" and TAG_Index='{0}' ", Tag_Index)

            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
            rpt = oReport.GetReportInfo(Report_Name, strWhere)

            ' BGN Update PrintCount
            Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.NULL)
            objTag.UpdatePrintStatus(Tag_Index)
            ' END Update PrintCount

            If (IsPreview) Then
                frm.CrystalReportViewer1.ReportSource = rpt
                frm.ShowDialog()
            Else
                'Get the Copy times
                Dim nCopy As Integer = Me.pdoPalletSlip.PrinterSettings.Copies
                'Get the number of Start Page
                Dim sPage As Integer = Me.pdoPalletSlip.PrinterSettings.FromPage
                'Get the number of End Page
                Dim ePage As Integer = Me.pdoPalletSlip.PrinterSettings.ToPage
                'Get the printer name
                Dim PrinterName As String = Me.pdoPalletSlip.PrinterSettings.PrinterName

                rpt.PrintOptions.PrinterName = PrinterName
                rpt.PrintToPrinter(nCopy, False, sPage, ePage)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class
