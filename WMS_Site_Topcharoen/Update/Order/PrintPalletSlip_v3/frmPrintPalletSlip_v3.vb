Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Function
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_OUTB_Report

Public Class frmPrintPalletSlip_v3

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
    Private PurchaseOrderItem_Index As String = ""
    Private PurchaseOrder_Index As String = ""
    Private PurchaseOrder_No As String = ""
    Private ItemLife_y As Integer = 0
    Private ItemLife_m As Integer = 0
    Private ItemLife_d As Integer = 0
    Private isPlot As Boolean = False
    Private ProductType_Index As String = ""
    Dim dblPrice3 As Double = 0

#End Region

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub frmPrintPalletSlip_v3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.dtpExpire_Date.Value = Now
            Me.dtpPosting_Date.Value = Now
            'If (W_Module.WV_UserName.ToUpper = "ADMIN") Then
            '    Me.btnTag_Weight.Visible = True
            'End If
            Me.btnTag_Weight.Visible = True
            Me.getCboSerialPort(cboSerialPort)

            Me.Clear_Detail()
            Me.getReportName(101)
            txtOrder_Date.Text = Me.Order_Date.ToString("dd/MM/yyyy")
            Me.getDocumentType(Me.DocumentType_Index)
            Me.getCboItemStatus()

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
                Me.Clear_Detail()
                Me.getCboPackageSku(frm.Sku_Index)
                If (cboPackageSku.Items.Count > 0) Then
                    Me.txtSku.Tag = frm.Sku_Index
                    Me.txtSku.Text = frm.Sku_ID
                    Me.txtSku_Desc.Text = frm.Sku_Des_th
                    Me.getSku_Detail(Me.txtSku.Text, Me.Customer_Index)
                    If (cboPackageSku.Items.Count > 0) Then
                        Me.getSkuRatio(Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, Me.txtQtyPerPallet.Text)

                        ' DEFAULT_PALLETSLIP_PACKAGE_KG_BY_PRODUCTTYPE_IN
                        Dim strConfigValue As String = New config_CustomSetting().getConfig_Key_DEFUALT("DEFAULT_PALLETSLIP_PACKAGE_KG_BY_PRODUCTTYPE_IN")
                        If (Not strConfigValue = Nothing) Then
                            Dim arrProductType_Index() As String = strConfigValue.Split(",")
                            For Each ProductType_Index As String In arrProductType_Index
                                If (ProductType_Index.Replace("'", "") = Me.ProductType_Index) Then
                                    If (Me.cboPackageSku.FindStringExact("กิโลกรัม") = 1) Then
                                        Me.cboPackageSku.SelectedIndex = Me.cboPackageSku.FindString("กิโลกรัม")
                                    End If
                                    Exit For
                                End If
                            Next
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            'KSL : Add on fix bug
            Dim odbo As New DBType_SQLServer
            If odbo.DBExeQuery_Scalar(String.Format("select count(*) from tb_Order where status in (-1,2) and Order_Index = '{0}'", Me._Order_Index)) > 0 Then
                W_Language.W_MSG_Error("ใบรับจัดเก็บเสร็จสิ้น ไม่สามารถสร้างพาเลทเพิ่มได้")
                Exit Sub
            End If
            ' Validate
            If (Me.isPlot = True) And (Me.txtPlot.Text.Trim.Length = 0) Then
                MessageBox.Show("กรุณา Lot/Batch", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.txtPlot.Focus()
                Exit Sub
            End If

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
            If (New clsPrintPalletSlip_v2().CheckSKUSerial(Me.txtSku.Tag)) Then
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
            Dim WeightScale As Decimal = 0
            If (IsNumeric(Me.txtWeightScale.Text)) Then
                WeightScale = CDec(Me.txtWeightScale.Text)
            End If
            Dim Pallet_Qty As Decimal = 0
            If (IsNumeric(Me.txtPallet_Qty.Text)) Then
                Pallet_Qty = CDec(Me.txtPallet_Qty.Text)
            End If

            'If IsNumeric(Me.txtPallet_Qty.Text) Then
            '    If CDbl(Me.txtPallet_Qty.Text) > 0 Then
            '        If Me.cboPackageSku.SelectedValue IsNot Nothing Then
            '            Dim xdrRatio() As DataRow = CType(Me.cboPackageSku.DataSource, DataTable).Select(String.Format("Package_Index = {0}", Me.cboPackageSku.SelectedValue))
            '            Pallet_Qty = FormatNumber((Me.txtPallet_Qty.Text * CDec(xdrRatio(0)("Ratio"))), 6)
            '        End If
            '    End If
            'End If


            Me.frmDeposit_WMS.SavePalletSlip_v3(Tag_Index, Tag_No, Me.txtRefNo1.Text.Trim(), Me.txtRefNo2.Text.Trim(), Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, CDec(Me.txtQtyPerPallet.Text), CDec(Me.lblUnitMainPackage.Tag), CDec(Me.lblUnitMainQty.Text), Me.cboItemStatus.SelectedValue, Me.txtPlot.Text, IIf(Me.dtpPosting_Date.Checked, Me.dtpPosting_Date.Value.ToString("yyyy-MM-dd"), ""), IIf(Me.dtpExpire_Date.Checked, Me.dtpExpire_Date.Value.ToString("yyyy-MM-dd"), ""), CDec(Me.txtUnitWeight.Text), CDec(Me.txtUnitVolume.Text), CDec(Me.Dimension_Width), CDec(Me.Dimension_Length), CDec(Me.Dimension_Height), Me.PurchaseOrderItem_Index, Me.PurchaseOrder_Index, Me.PurchaseOrder_No, WeightScale, Pallet_Qty)
            Me._Order_Index = Me.frmDeposit_WMS.Order_Index
            Me.txtTag_No.Text = Tag_No

            ' Print
            If (Not (Tag_Index = Nothing)) Then Me.PrintTag(Tag_Index, Tag_No, Me.chkPreview.Checked)

            ' Refresh
            If (Not Me.PurchaseOrderItem_Index = Nothing) Then
                Dim dt As DataTable = New clsPO_Popup().getPO_PopupList(String.Format(" where PurchaseOrderItem_Index='{0}' ", Me.PurchaseOrderItem_Index))
                If (dt.Rows.Count() > 0) Then
                    With dt.Rows(0)
                        If (IsNumeric(.Item("Padding_Qty"))) Then
                            Me.lblPaddingQty.Text = CDec(.Item("Padding_Qty")).ToString("#,##0.00")
                        End If
                        Me.lblPaddingPackage.Tag = .Item("Ratio")
                        Me.lblPaddingPackage.Text = .Item("Package_Desc")
                    End With
                Else
                    Me.lblPaddingQty.Text = "-"
                    Me.lblPaddingPackage.Tag = "0"
                    Me.lblPaddingPackage.Text = ""
                End If
            End If

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
            'Dim objClass As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objClass As New cls_KSL
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
            'Dim objClass As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
            'Dim objDt As DataTable = New DataTable
            Dim objDocumentType_Itemstatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
            Dim odtDocumentType_Itemstatus As New DataTable
            Try
                objDocumentType_Itemstatus.SearchDocumentType("", "", Me.DocumentType_Index)
                odtDocumentType_Itemstatus = objDocumentType_Itemstatus.DataTable

                With cboItemStatus
                    .DisplayMember = "ItemStatusDes"
                    .ValueMember = "ItemStatus_Index"
                    .DataSource = odtDocumentType_Itemstatus
                End With
                If odtDocumentType_Itemstatus.Rows.Count > 0 Then
                    Dim objDocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
                    objDocumentType.SearchDocumentType(Me.DocumentType_Index)
                    Dim odtDocumentType As New DataTable
                    odtDocumentType = objDocumentType.DataTable
                    If odtDocumentType.Rows.Count > 0 Then
                        If Not odtDocumentType.Rows(0)("ItemStatus_Index").ToString = "" Then cboItemStatus.SelectedValue = odtDocumentType.Rows(0)("ItemStatus_Index").ToString
                    End If
                End If
                'objClass.getItemStatus()
                'objDt = objClass.DataTable
                'With cboItemStatus
                '    .DisplayMember = "ItemStatus"
                '    .ValueMember = "ItemStatus_Index"
                '    .DataSource = objDt
                '    If (.Items.Count > 0) Then
                '        .Enabled = True
                '    Else
                '        .Enabled = False
                '    End If
                'End With
            Catch ex As Exception
                Throw ex
            Finally
                'objClass = Nothing
                'objDt = Nothing

                objDocumentType_Itemstatus = Nothing
                odtDocumentType_Itemstatus = Nothing
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
                If (objDt.Rows.Count() = 0) Then
                    objClass.getSKU_Detail_Equal_ByCustomer(Sku_Id, "")
                    objDt = objClass.DataTable
                End If
                If (objDt.Rows.Count() > 0) Then
                    With objDt.Rows(0)
                        Me.txtPalletQty.Text = .Item("Qty_Per_Pallet")
                        Me.Dimension_Width = .Item("_W")
                        Me.Dimension_Length = .Item("_L")
                        Me.Dimension_Height = .Item("_H")
                        Me.txtUnitVolume.Text = .Item("Unit_Volume")
                        Me.txtUnitWeight.Text = .Item("UnitWeight")
                        Me.ItemLife_y = .Item("ItemLife_y")
                        Me.ItemLife_m = .Item("ItemLife_m")
                        Me.ItemLife_d = .Item("ItemLife_d")
                        Me.lblUnitMain.Visible = True
                        Me.lblUnitMainQty.Text = "-"
                        Me.lblUnitMainPackage.Tag = "0"
                        Me.lblUnitMainPackage.Text = .Item("Sku_PackageDescription")
                        If (IsNumeric(.Item("Qty_Per_Pallet"))) Then
                            Me.txtQtyPerPallet.Text = .Item("Qty_Per_Pallet").ToString().Trim("0").Trim(".")
                        End If
                        Me.ProductType_Index = .Item("ProductType_Index")

                        'KSL : Fix code
                        If IsNumeric(.Item("Price3")) Then Me.dblPrice3 = FormatNumber(.Item("Price3"), 2)
                        Me.isPlot = .Item("isPlot")
                        If (Me.ItemLife_y > 0) Or (Me.ItemLife_m > 0) Or (Me.ItemLife_d > 0) Then
                            Me.dtpPosting_Date.Checked = True
                            Me.dtpExpire_Date.Value = Me.dtpPosting_Date.Value.AddYears(Me.ItemLife_y).AddMonths(Me.ItemLife_m).AddDays(Me.ItemLife_d)
                            Me.dtpExpire_Date.Checked = True
                        Else
                            Me.dtpPosting_Date.Value = Now
                            Me.dtpExpire_Date.Value = Now
                            Me.dtpPosting_Date.Checked = False
                            Me.dtpExpire_Date.Checked = False
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


            'KSL : Fix recal
            'KSL : Fix recal
            If IsNumeric(Me.dblPrice3) Then
                If CDbl(Me.dblPrice3) > 0 Then
                    If Me.cboPackageSku.SelectedValue IsNot Nothing Then
                        Dim xdrRatio() As DataRow = CType(Me.cboPackageSku.DataSource, DataTable).Select(String.Format("Package_Index = {0}", Me.cboPackageSku.SelectedValue))
                        Me.txtPallet_Qty.Text = FormatNumber((Me.dblPrice3 / CDec(xdrRatio(0)("Ratio"))), 2)
                    End If
                End If
            End If


        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtQtyPerPallet_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyPerPallet.KeyPress
        e.Handled = ValidatedCurrency(Asc(e.KeyChar))

        If e.KeyChar = Convert.ToChar(Keys.Enter) Then
            Me.txtPallet_Qty.Focus()
            Me.txtPallet_Qty.SelectAll()
        End If

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
            Me.CalTotalNet()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Sub CalTotalNet()
        Try
            If IsNumeric(Me.txtPallet_Qty.Text) And IsNumeric(Me.txtQtyPerPallet.Text) Then
                Dim xNet As Double = CDbl(Me.txtQtyPerPallet.Text) - CDbl(Me.txtPallet_Qty.Text)
                If Me.cboPackageSku.SelectedValue IsNot Nothing Then
                    Dim xdrRatio() As DataRow = CType(Me.cboPackageSku.DataSource, DataTable).Select(String.Format("Package_Index = {0}", Me.cboPackageSku.SelectedValue))
                    Me.txtTotalNet.Text = xNet * xdrRatio(0)("Ratio")
                End If

            End If
        Catch ex As Exception
            Throw ex
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
        Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
        Try
            Select Case Report_Name.ToUpper
                Case "TAGSTICKERPRINTOUT", "TAGSTICKERPRINTOUT_MINI", "TAGSTICKERCHEMICALPRINTOUT_MINI", "TAGSTICKERSPBRPRINTOUT_MINI"
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
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Clear_Detail()
        Try
            ' Clear Sku_Detail
            Me.txtRefNo1.Clear()
            Me.txtRefNo2.Clear()
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
            Me.cboPackageSku.DataSource = Nothing
            Me.cboPackageSku.Enabled = False
            Me.lblPaddingQty.Text = ""
            Me.lblPaddingPackage.Tag = "0"
            Me.lblPaddingPackage.Text = ""
            Me.PurchaseOrderItem_Index = ""
            Me.PurchaseOrder_Index = ""
            Me.PurchaseOrder_No = ""
            Me.ItemLife_y = 0
            Me.ItemLife_m = 0
            Me.ItemLife_d = 0
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPO_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPO_Popup.Click
        Try
            If (Me.Customer_Index = Nothing) Then
                MessageBox.Show("กรุณาระบุเจ้าของงานก่อน", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            Dim frm As New frmPO_Popup()
            frm.Customer_Index = Me.Customer_Index
            frm.ShowDialog()
            If frm.IsProcess Then
                Me.getPurchaseOrderItem(frm.PurchaseOrderItem_Index)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub getPurchaseOrderItem(ByVal PurchaseOrderItem_Index As String)
        Try
            Me.Clear_Detail()
            If (PurchaseOrderItem_Index = Nothing) Then
                Me.txtRefNo1.ReadOnly = False
                Me.btnSku.Enabled = True
                Me.lblPadding.Visible = False
                Me.lblPaddingQty.Visible = False
                Me.lblPaddingPackage.Visible = False
                Exit Sub
            End If
            Dim dt As DataTable = New clsPO_Popup().getPO_PopupList(String.Format(" where PurchaseOrderItem_Index='{0}' ", PurchaseOrderItem_Index))
            If (dt.Rows.Count() > 0) Then
                With dt.Rows(0)
                    Me.txtRefNo1.ReadOnly = True
                    Me.btnSku.Enabled = False
                    Me.lblPadding.Visible = True
                    Me.lblPaddingQty.Visible = True
                    Me.lblPaddingPackage.Visible = True
                    Me.PurchaseOrderItem_Index = .Item("PurchaseOrderItem_Index")
                    Me.PurchaseOrder_Index = .Item("PurchaseOrder_Index")
                    Me.PurchaseOrder_No = .Item("PurchaseOrder_No")
                    Me.txtRefNo1.Text = Me.PurchaseOrder_No
                    If (IsNumeric(.Item("Total_Padding_Qty"))) Then
                        Me.lblPaddingQty.Text = CDec(.Item("Total_Padding_Qty")).ToString("#,##0.00")
                    End If
                    Me.lblPaddingPackage.Tag = 1
                    Me.lblPaddingPackage.Text = .Item("PackageSku_Desc")
                    Me.txtSku.Tag = .Item("Sku_Index")
                    Me.txtSku.Text = .Item("Sku_Id")
                    If (Me.txtSku.Tag = Nothing) Then
                        Exit Sub
                    Else
                        Me.getCboPackageSku(Me.txtSku.Tag)
                        If (cboPackageSku.Items.Count > 0) Then
                            Me.txtSku.Tag = Me.txtSku.Tag
                            Me.txtSku.Text = Me.txtSku.Text
                            Me.txtSku_Desc.Text = .Item("Sku_Name")
                            Me.getSku_Detail(Me.txtSku.Text, Me.Customer_Index)
                            If (cboPackageSku.Items.Count > 0) Then
                                Me.getSkuRatio(Me.txtSku.Tag, Me.cboPackageSku.SelectedValue, Me.txtQtyPerPallet.Text)
                            End If
                        End If
                    End If
                End With
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub dtpPosting_Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPosting_Date.ValueChanged
        Try
            Me.dtpExpire_Date.Value = Me.dtpPosting_Date.Value.AddYears(Me.ItemLife_y).AddMonths(Me.ItemLife_m).AddDays(Me.ItemLife_d)
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub getReportName(ByVal Process_Id As Integer)
        Dim objClassDB As New WMS_STD_Master.config_Report()
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

    Private Sub btnTag_Weight_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTag_Weight.Click
        Try
            'Dim _frm As New frmTag_Weight()
            '_frm.ShowDialog()
            If (cboSerialPort.Items.Count = 0) Then
                MessageBox.Show("ไม่พบ Serial Port บนคอมพิวเตอร์", "ข้อความ", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Exit Sub
            End If
            RemoveHandler txtQtyPerPallet.TextChanged, AddressOf txtWeight_TextChanged
            Me.txtWeightScale.Text = New Azano.VC50().getWeight(cboSerialPort.SelectedValue).ToString("0.00")
            Me.txtQtyPerPallet.Text = Me.txtWeightScale.Text
            AddHandler txtQtyPerPallet.TextChanged, AddressOf txtWeight_TextChanged
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub txtWeight_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyPerPallet.TextChanged
        Try
            Me.txtWeightScale.Text = "-"
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub getCboSerialPort(ByVal cbo As ComboBox)
        Dim _ports As String() = System.IO.Ports.SerialPort.GetPortNames()
        Dim _oDt As DataTable = New DataTable
        Try
            If (Not (_oDt.Columns.Contains("DisplayPort"))) Then
                _oDt.Columns.Add("DisplayPort", GetType(System.String))
            End If
            If (Not (_oDt.Columns.Contains("ValuePort"))) Then
                _oDt.Columns.Add("ValuePort", GetType(System.String))
            End If
            For Each _port As String In _ports
                _oDt.Rows.Add(_port, _port)
            Next
            With cbo
                .DisplayMember = "DisplayPort"
                .ValueMember = "ValuePort"
                .DataSource = _oDt
                .Enabled = .Items.Count > 0
            End With
        Catch ex As Exception
            Throw ex
        Finally
            _oDt = Nothing
        End Try

    End Sub

    Private Sub txtPallet_Qty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPallet_Qty.KeyPress
        Try
            e.Handled = ValidatedCurrency(Asc(e.KeyChar))
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

    End Sub

    Private Sub txtPallet_Qty_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPallet_Qty.TextChanged
        Try
            Me.CalTotalNet()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString().ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
End Class
