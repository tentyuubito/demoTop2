<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPO
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPO))
        Dim DataGridViewCellStyle49 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle50 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle51 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle52 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle53 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle54 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle55 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle56 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle57 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle58 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle59 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle60 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle61 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle62 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle63 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle64 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle65 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle66 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle67 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle68 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle69 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle70 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle71 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle72 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.tbcPO = New System.Windows.Forms.TabControl
        Me.tbpPO = New System.Windows.Forms.TabPage
        Me.btnPO_PR_Import = New System.Windows.Forms.Button
        Me.gbAmountSummary = New System.Windows.Forms.GroupBox
        Me.chkDiscount = New System.Windows.Forms.CheckBox
        Me.btnMultiLevelDiscount = New System.Windows.Forms.Button
        Me.lblPercent2 = New System.Windows.Forms.Label
        Me.chkVAT = New System.Windows.Forms.CheckBox
        Me.lblCurrency1 = New System.Windows.Forms.Label
        Me.txtSubtotal = New System.Windows.Forms.TextBox
        Me.lblCurrency5 = New System.Windows.Forms.Label
        Me.lblCurrency2 = New System.Windows.Forms.Label
        Me.lblCurrency4 = New System.Windows.Forms.Label
        Me.lblDeposit_Amt = New System.Windows.Forms.Label
        Me.lblCurrency3 = New System.Windows.Forms.Label
        Me.txtNet_Amt = New System.Windows.Forms.TextBox
        Me.txtDeposit_Amt = New System.Windows.Forms.TextBox
        Me.lblNet_Amt = New System.Windows.Forms.Label
        Me.txtVAT = New System.Windows.Forms.TextBox
        Me.lblPercent1 = New System.Windows.Forms.Label
        Me.txtVAT_Percent = New System.Windows.Forms.TextBox
        Me.lblSubtotal = New System.Windows.Forms.Label
        Me.txtDiscount_Amt = New System.Windows.Forms.TextBox
        Me.txtDiscount_Percent = New System.Windows.Forms.TextBox
        Me.BtnInsert = New System.Windows.Forms.Button
        Me.BtnSort = New System.Windows.Forms.Button
        Me.grdPOItem = New System.Windows.Forms.DataGridView
        Me.col_no = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SKU_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Btn_GetSKU = New System.Windows.Forms.DataGridViewButtonColumn
        Me.Col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Product_Type = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_UOM = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Unit_Price = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_ReceiptShow = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Discount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Amount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Currency = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Weight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Volume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_POItem_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Co_Percent_Over_Allow = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Co_Percent_Under_Allow = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Remark = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PurchaseOrder_PR_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnDelete = New System.Windows.Forms.Button
        Me.tbpOther = New System.Windows.Forms.TabPage
        Me.grdPOOther = New System.Windows.Forms.DataGridView
        Me.Col_Index_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Seq_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Unit_Price_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Amount_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Remark_Other = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnDelete1 = New System.Windows.Forms.Button
        Me.tbpRemain = New System.Windows.Forms.TabPage
        Me.grdPORemain = New System.Windows.Forms.DataGridView
        Me.Col_SKU_ID_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Product_Type_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_UOM_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_Remain = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Last_Received_Date = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_POItem_Index_Pending = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.tbpReceive = New System.Windows.Forms.TabPage
        Me.grdPOAlreadyReceipt = New System.Windows.Forms.DataGridView
        Me.Col_SKU_ID_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Order_ID_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Order_Date_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Qty_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Unit_Price_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_UOM_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Total_Receipt = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn37 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbPrintReport = New System.Windows.Forms.GroupBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.tabHeader = New System.Windows.Forms.TabControl
        Me.TabOrderReturn = New System.Windows.Forms.TabPage
        Me.txtDepartment_Id = New System.Windows.Forms.TextBox
        Me.txtDepartment_Name = New System.Windows.Forms.TextBox
        Me.btnSeachDepartment = New System.Windows.Forms.Button
        Me.txtStatus = New System.Windows.Forms.TextBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.btnCarrier = New System.Windows.Forms.Button
        Me.txtCarrier_Name = New System.Windows.Forms.TextBox
        Me.txtCarrier_ID = New System.Windows.Forms.TextBox
        Me.btnCustomer_Receive = New System.Windows.Forms.Button
        Me.txtShipping_Location_ID = New System.Windows.Forms.TextBox
        Me.txtTax_No = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.dtpDue_Date = New System.Windows.Forms.DateTimePicker
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.lblExRate = New System.Windows.Forms.Label
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.txtExRate = New System.Windows.Forms.TextBox
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.cboCurrency = New System.Windows.Forms.ComboBox
        Me.lblDay = New System.Windows.Forms.Label
        Me.lblCurrency = New System.Windows.Forms.Label
        Me.lblCreditTerm = New System.Windows.Forms.Label
        Me.cboConditionPay = New System.Windows.Forms.ComboBox
        Me.txtCreditTerm = New System.Windows.Forms.TextBox
        Me.lblPaymentType = New System.Windows.Forms.Label
        Me.lblDue_Date = New System.Windows.Forms.Label
        Me.txtShip_Address1 = New System.Windows.Forms.TextBox
        Me.lblRef2 = New System.Windows.Forms.Label
        Me.txtRef2 = New System.Windows.Forms.TextBox
        Me.lblRef1 = New System.Windows.Forms.Label
        Me.txtUser = New System.Windows.Forms.TextBox
        Me.txtRef1 = New System.Windows.Forms.TextBox
        Me.lblUser = New System.Windows.Forms.Label
        Me.txtSupplier_Id = New System.Windows.Forms.TextBox
        Me.txtSupplier_Name = New System.Windows.Forms.TextBox
        Me.btnSeachSupplier = New System.Windows.Forms.Button
        Me.lblPO_Date = New System.Windows.Forms.Label
        Me.dtpPO_Date = New System.Windows.Forms.DateTimePicker
        Me.txtPO_No = New System.Windows.Forms.TextBox
        Me.lblRemark = New System.Windows.Forms.Label
        Me.txtRemark = New System.Windows.Forms.TextBox
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.lblCarrier = New System.Windows.Forms.Label
        Me.lblReceivedLocation = New System.Windows.Forms.Label
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.lblDepartment = New System.Windows.Forms.Label
        Me.lblSupplier = New System.Windows.Forms.Label
        Me.lblPO_No = New System.Windows.Forms.Label
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtSupplier_Fax = New System.Windows.Forms.TextBox
        Me.txtSupplier_Phone = New System.Windows.Forms.TextBox
        Me.lblPO_Fax = New System.Windows.Forms.Label
        Me.lblPO_Phone = New System.Windows.Forms.Label
        Me.txtSupplier_Address = New System.Windows.Forms.TextBox
        Me.lblPO_Address = New System.Windows.Forms.Label
        Me.grbSuplier = New System.Windows.Forms.GroupBox
        Me.txtShip_Fax = New System.Windows.Forms.TextBox
        Me.txtShip_Phone = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtShip_Address = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.tbpVisible = New System.Windows.Forms.TabPage
        Me.btnRecived = New System.Windows.Forms.Button
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.btnClose_PO = New System.Windows.Forms.Button
        Me.btnCompare = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnEdit_Special = New System.Windows.Forms.Button
        Me.tbcPO.SuspendLayout()
        Me.tbpPO.SuspendLayout()
        Me.gbAmountSummary.SuspendLayout()
        CType(Me.grdPOItem, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpOther.SuspendLayout()
        CType(Me.grdPOOther, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpRemain.SuspendLayout()
        CType(Me.grdPORemain, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tbpReceive.SuspendLayout()
        CType(Me.grdPOAlreadyReceipt, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbPrintReport.SuspendLayout()
        Me.tabHeader.SuspendLayout()
        Me.TabOrderReturn.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.grbSuplier.SuspendLayout()
        Me.SuspendLayout()
        '
        'tbcPO
        '
        Me.tbcPO.Controls.Add(Me.tbpPO)
        Me.tbcPO.Controls.Add(Me.tbpOther)
        Me.tbcPO.Controls.Add(Me.tbpRemain)
        Me.tbcPO.Controls.Add(Me.tbpReceive)
        Me.tbcPO.Location = New System.Drawing.Point(3, 199)
        Me.tbcPO.Name = "tbcPO"
        Me.tbcPO.SelectedIndex = 0
        Me.tbcPO.Size = New System.Drawing.Size(993, 435)
        Me.tbcPO.TabIndex = 1
        '
        'tbpPO
        '
        Me.tbpPO.Controls.Add(Me.btnPO_PR_Import)
        Me.tbpPO.Controls.Add(Me.gbAmountSummary)
        Me.tbpPO.Controls.Add(Me.BtnInsert)
        Me.tbpPO.Controls.Add(Me.BtnSort)
        Me.tbpPO.Controls.Add(Me.grdPOItem)
        Me.tbpPO.Controls.Add(Me.btnDelete)
        Me.tbpPO.Location = New System.Drawing.Point(4, 22)
        Me.tbpPO.Name = "tbpPO"
        Me.tbpPO.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpPO.Size = New System.Drawing.Size(985, 409)
        Me.tbpPO.TabIndex = 0
        Me.tbpPO.Text = "รายการสั่งซื้อ"
        Me.tbpPO.UseVisualStyleBackColor = True
        '
        'btnPO_PR_Import
        '
        Me.btnPO_PR_Import.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnPO_PR_Import.Image = CType(resources.GetObject("btnPO_PR_Import.Image"), System.Drawing.Image)
        Me.btnPO_PR_Import.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPO_PR_Import.Location = New System.Drawing.Point(6, 361)
        Me.btnPO_PR_Import.Name = "btnPO_PR_Import"
        Me.btnPO_PR_Import.Size = New System.Drawing.Size(107, 38)
        Me.btnPO_PR_Import.TabIndex = 360
        Me.btnPO_PR_Import.Text = "ดึงจาก PR"
        Me.btnPO_PR_Import.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPO_PR_Import.UseVisualStyleBackColor = True
        '
        'gbAmountSummary
        '
        Me.gbAmountSummary.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbAmountSummary.Controls.Add(Me.chkDiscount)
        Me.gbAmountSummary.Controls.Add(Me.btnMultiLevelDiscount)
        Me.gbAmountSummary.Controls.Add(Me.lblPercent2)
        Me.gbAmountSummary.Controls.Add(Me.chkVAT)
        Me.gbAmountSummary.Controls.Add(Me.lblCurrency1)
        Me.gbAmountSummary.Controls.Add(Me.txtSubtotal)
        Me.gbAmountSummary.Controls.Add(Me.lblCurrency5)
        Me.gbAmountSummary.Controls.Add(Me.lblCurrency2)
        Me.gbAmountSummary.Controls.Add(Me.lblCurrency4)
        Me.gbAmountSummary.Controls.Add(Me.lblDeposit_Amt)
        Me.gbAmountSummary.Controls.Add(Me.lblCurrency3)
        Me.gbAmountSummary.Controls.Add(Me.txtNet_Amt)
        Me.gbAmountSummary.Controls.Add(Me.txtDeposit_Amt)
        Me.gbAmountSummary.Controls.Add(Me.lblNet_Amt)
        Me.gbAmountSummary.Controls.Add(Me.txtVAT)
        Me.gbAmountSummary.Controls.Add(Me.lblPercent1)
        Me.gbAmountSummary.Controls.Add(Me.txtVAT_Percent)
        Me.gbAmountSummary.Controls.Add(Me.lblSubtotal)
        Me.gbAmountSummary.Controls.Add(Me.txtDiscount_Amt)
        Me.gbAmountSummary.Controls.Add(Me.txtDiscount_Percent)
        Me.gbAmountSummary.Location = New System.Drawing.Point(406, 311)
        Me.gbAmountSummary.Name = "gbAmountSummary"
        Me.gbAmountSummary.Size = New System.Drawing.Size(567, 92)
        Me.gbAmountSummary.TabIndex = 359
        Me.gbAmountSummary.TabStop = False
        Me.gbAmountSummary.Text = "สรุปยอดเงิน"
        '
        'chkDiscount
        '
        Me.chkDiscount.AutoSize = True
        Me.chkDiscount.Location = New System.Drawing.Point(14, 42)
        Me.chkDiscount.Name = "chkDiscount"
        Me.chkDiscount.Size = New System.Drawing.Size(59, 17)
        Me.chkDiscount.TabIndex = 6
        Me.chkDiscount.Text = "ส่วนลด"
        Me.chkDiscount.UseVisualStyleBackColor = True
        '
        'btnMultiLevelDiscount
        '
        Me.btnMultiLevelDiscount.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnMultiLevelDiscount.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnMultiLevelDiscount.Location = New System.Drawing.Point(399, 9)
        Me.btnMultiLevelDiscount.Name = "btnMultiLevelDiscount"
        Me.btnMultiLevelDiscount.Size = New System.Drawing.Size(100, 25)
        Me.btnMultiLevelDiscount.TabIndex = 5
        Me.btnMultiLevelDiscount.Text = "ส่วนสดแบบขั้น"
        Me.btnMultiLevelDiscount.UseVisualStyleBackColor = True
        Me.btnMultiLevelDiscount.Visible = False
        '
        'lblPercent2
        '
        Me.lblPercent2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPercent2.Location = New System.Drawing.Point(138, 62)
        Me.lblPercent2.Name = "lblPercent2"
        Me.lblPercent2.Size = New System.Drawing.Size(21, 13)
        Me.lblPercent2.TabIndex = 13
        Me.lblPercent2.Text = "%"
        Me.lblPercent2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkVAT
        '
        Me.chkVAT.AutoSize = True
        Me.chkVAT.Checked = True
        Me.chkVAT.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkVAT.Location = New System.Drawing.Point(14, 63)
        Me.chkVAT.Name = "chkVAT"
        Me.chkVAT.Size = New System.Drawing.Size(46, 17)
        Me.chkVAT.TabIndex = 11
        Me.chkVAT.Text = "ภาษี"
        Me.chkVAT.UseVisualStyleBackColor = True
        '
        'lblCurrency1
        '
        Me.lblCurrency1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrency1.Location = New System.Drawing.Point(280, 16)
        Me.lblCurrency1.Name = "lblCurrency1"
        Me.lblCurrency1.Size = New System.Drawing.Size(40, 15)
        Me.lblCurrency1.TabIndex = 4
        Me.lblCurrency1.Text = "บาท"
        Me.lblCurrency1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtSubtotal
        '
        Me.txtSubtotal.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSubtotal.Location = New System.Drawing.Point(174, 13)
        Me.txtSubtotal.MaxLength = 20
        Me.txtSubtotal.Name = "txtSubtotal"
        Me.txtSubtotal.ReadOnly = True
        Me.txtSubtotal.Size = New System.Drawing.Size(100, 20)
        Me.txtSubtotal.TabIndex = 3
        Me.txtSubtotal.Text = "0.00"
        Me.txtSubtotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCurrency5
        '
        Me.lblCurrency5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrency5.Location = New System.Drawing.Point(505, 61)
        Me.lblCurrency5.Name = "lblCurrency5"
        Me.lblCurrency5.Size = New System.Drawing.Size(40, 13)
        Me.lblCurrency5.TabIndex = 21
        Me.lblCurrency5.Text = "บาท"
        Me.lblCurrency5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCurrency2
        '
        Me.lblCurrency2.AutoSize = True
        Me.lblCurrency2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrency2.Location = New System.Drawing.Point(280, 40)
        Me.lblCurrency2.Name = "lblCurrency2"
        Me.lblCurrency2.Size = New System.Drawing.Size(26, 13)
        Me.lblCurrency2.TabIndex = 10
        Me.lblCurrency2.Text = "บาท"
        Me.lblCurrency2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCurrency4
        '
        Me.lblCurrency4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrency4.Location = New System.Drawing.Point(505, 38)
        Me.lblCurrency4.Name = "lblCurrency4"
        Me.lblCurrency4.Size = New System.Drawing.Size(40, 17)
        Me.lblCurrency4.TabIndex = 18
        Me.lblCurrency4.Text = "บาท"
        Me.lblCurrency4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDeposit_Amt
        '
        Me.lblDeposit_Amt.AutoSize = True
        Me.lblDeposit_Amt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDeposit_Amt.Location = New System.Drawing.Point(311, 42)
        Me.lblDeposit_Amt.Name = "lblDeposit_Amt"
        Me.lblDeposit_Amt.Size = New System.Drawing.Size(85, 13)
        Me.lblDeposit_Amt.TabIndex = 16
        Me.lblDeposit_Amt.Text = "หักลดหนี้ / มัดจำ"
        Me.lblDeposit_Amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCurrency3
        '
        Me.lblCurrency3.AutoSize = True
        Me.lblCurrency3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrency3.Location = New System.Drawing.Point(280, 63)
        Me.lblCurrency3.Name = "lblCurrency3"
        Me.lblCurrency3.Size = New System.Drawing.Size(26, 13)
        Me.lblCurrency3.TabIndex = 15
        Me.lblCurrency3.Text = "บาท"
        Me.lblCurrency3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtNet_Amt
        '
        Me.txtNet_Amt.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtNet_Amt.Location = New System.Drawing.Point(399, 59)
        Me.txtNet_Amt.MaxLength = 20
        Me.txtNet_Amt.Name = "txtNet_Amt"
        Me.txtNet_Amt.ReadOnly = True
        Me.txtNet_Amt.Size = New System.Drawing.Size(100, 20)
        Me.txtNet_Amt.TabIndex = 20
        Me.txtNet_Amt.Text = "0.00"
        Me.txtNet_Amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDeposit_Amt
        '
        Me.txtDeposit_Amt.Location = New System.Drawing.Point(399, 37)
        Me.txtDeposit_Amt.MaxLength = 20
        Me.txtDeposit_Amt.Name = "txtDeposit_Amt"
        Me.txtDeposit_Amt.Size = New System.Drawing.Size(100, 20)
        Me.txtDeposit_Amt.TabIndex = 17
        Me.txtDeposit_Amt.Text = "0.00"
        Me.txtDeposit_Amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblNet_Amt
        '
        Me.lblNet_Amt.AutoSize = True
        Me.lblNet_Amt.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblNet_Amt.Location = New System.Drawing.Point(310, 63)
        Me.lblNet_Amt.Name = "lblNet_Amt"
        Me.lblNet_Amt.Size = New System.Drawing.Size(83, 13)
        Me.lblNet_Amt.TabIndex = 19
        Me.lblNet_Amt.Text = "รวมเป็นเงินสุทธิ"
        Me.lblNet_Amt.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVAT
        '
        Me.txtVAT.BackColor = System.Drawing.SystemColors.Control
        Me.txtVAT.Location = New System.Drawing.Point(174, 59)
        Me.txtVAT.MaxLength = 20
        Me.txtVAT.Name = "txtVAT"
        Me.txtVAT.ReadOnly = True
        Me.txtVAT.Size = New System.Drawing.Size(100, 20)
        Me.txtVAT.TabIndex = 14
        Me.txtVAT.Text = "0.00"
        Me.txtVAT.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPercent1
        '
        Me.lblPercent1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPercent1.Location = New System.Drawing.Point(142, 41)
        Me.lblPercent1.Name = "lblPercent1"
        Me.lblPercent1.Size = New System.Drawing.Size(17, 16)
        Me.lblPercent1.TabIndex = 8
        Me.lblPercent1.Text = "%"
        Me.lblPercent1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVAT_Percent
        '
        Me.txtVAT_Percent.Location = New System.Drawing.Point(77, 61)
        Me.txtVAT_Percent.MaxLength = 6
        Me.txtVAT_Percent.Name = "txtVAT_Percent"
        Me.txtVAT_Percent.Size = New System.Drawing.Size(61, 20)
        Me.txtVAT_Percent.TabIndex = 12
        Me.txtVAT_Percent.Text = "0.00"
        Me.txtVAT_Percent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSubtotal
        '
        Me.lblSubtotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSubtotal.Location = New System.Drawing.Point(79, 16)
        Me.lblSubtotal.Name = "lblSubtotal"
        Me.lblSubtotal.Size = New System.Drawing.Size(77, 17)
        Me.lblSubtotal.TabIndex = 2
        Me.lblSubtotal.Text = "รวมเป็นเงิน"
        Me.lblSubtotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDiscount_Amt
        '
        Me.txtDiscount_Amt.BackColor = System.Drawing.Color.White
        Me.txtDiscount_Amt.Location = New System.Drawing.Point(174, 35)
        Me.txtDiscount_Amt.MaxLength = 20
        Me.txtDiscount_Amt.Name = "txtDiscount_Amt"
        Me.txtDiscount_Amt.Size = New System.Drawing.Size(100, 20)
        Me.txtDiscount_Amt.TabIndex = 9
        Me.txtDiscount_Amt.Text = "0.00"
        Me.txtDiscount_Amt.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDiscount_Percent
        '
        Me.txtDiscount_Percent.Location = New System.Drawing.Point(77, 38)
        Me.txtDiscount_Percent.MaxLength = 6
        Me.txtDiscount_Percent.Name = "txtDiscount_Percent"
        Me.txtDiscount_Percent.Size = New System.Drawing.Size(61, 20)
        Me.txtDiscount_Percent.TabIndex = 7
        Me.txtDiscount_Percent.Text = "0.00"
        Me.txtDiscount_Percent.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnInsert
        '
        Me.BtnInsert.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnInsert.Image = CType(resources.GetObject("BtnInsert.Image"), System.Drawing.Image)
        Me.BtnInsert.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnInsert.Location = New System.Drawing.Point(113, 317)
        Me.BtnInsert.Name = "BtnInsert"
        Me.BtnInsert.Size = New System.Drawing.Size(108, 38)
        Me.BtnInsert.TabIndex = 356
        Me.BtnInsert.Text = "    แทรกรายการ"
        Me.BtnInsert.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.BtnInsert.UseVisualStyleBackColor = True
        '
        'BtnSort
        '
        Me.BtnSort.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BtnSort.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.BtnSort.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BtnSort.Location = New System.Drawing.Point(221, 317)
        Me.BtnSort.Name = "BtnSort"
        Me.BtnSort.Size = New System.Drawing.Size(45, 38)
        Me.BtnSort.TabIndex = 354
        Me.BtnSort.Text = "A ->Z"
        Me.BtnSort.UseVisualStyleBackColor = True
        '
        'grdPOItem
        '
        Me.grdPOItem.AllowUserToDeleteRows = False
        Me.grdPOItem.AllowUserToResizeRows = False
        Me.grdPOItem.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdPOItem.BackgroundColor = System.Drawing.Color.White
        Me.grdPOItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPOItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_no, Me.Col_SKU_ID, Me.Col_Btn_GetSKU, Me.Col_Description, Me.Col_Product_Type, Me.Col_UOM, Me.Col_Unit_Price, Me.Col_Qty, Me.Col_Qty_ReceiptShow, Me.Col_Discount, Me.Col_Amount, Me.Col_Currency, Me.Col_Weight, Me.Col_Volume, Me.Col_POItem_Index, Me.Co_Percent_Over_Allow, Me.Co_Percent_Under_Allow, Me.Col_Remark, Me.col_PurchaseOrder_PR_Index})
        Me.grdPOItem.Location = New System.Drawing.Point(1, 6)
        Me.grdPOItem.Name = "grdPOItem"
        Me.grdPOItem.RowHeadersVisible = False
        Me.grdPOItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdPOItem.Size = New System.Drawing.Size(972, 305)
        Me.grdPOItem.TabIndex = 0
        '
        'col_no
        '
        Me.col_no.HeaderText = "ลำดับ"
        Me.col_no.Name = "col_no"
        Me.col_no.Width = 50
        '
        'Col_SKU_ID
        '
        Me.Col_SKU_ID.HeaderText = "รหัส SKU"
        Me.Col_SKU_ID.Name = "Col_SKU_ID"
        '
        'Col_Btn_GetSKU
        '
        DataGridViewCellStyle49.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle49.NullValue = "..."
        Me.Col_Btn_GetSKU.DefaultCellStyle = DataGridViewCellStyle49
        Me.Col_Btn_GetSKU.HeaderText = ""
        Me.Col_Btn_GetSKU.Name = "Col_Btn_GetSKU"
        Me.Col_Btn_GetSKU.Width = 25
        '
        'Col_Description
        '
        Me.Col_Description.FillWeight = 150.0!
        Me.Col_Description.HeaderText = "รายละเอียด"
        Me.Col_Description.Name = "Col_Description"
        Me.Col_Description.ReadOnly = True
        Me.Col_Description.Width = 150
        '
        'Col_Product_Type
        '
        Me.Col_Product_Type.FillWeight = 150.0!
        Me.Col_Product_Type.HeaderText = "ประเภทสินค้า"
        Me.Col_Product_Type.Name = "Col_Product_Type"
        Me.Col_Product_Type.ReadOnly = True
        Me.Col_Product_Type.Width = 150
        '
        'Col_UOM
        '
        Me.Col_UOM.HeaderText = "หน่วย"
        Me.Col_UOM.Name = "Col_UOM"
        '
        'Col_Unit_Price
        '
        DataGridViewCellStyle50.NullValue = "0"
        Me.Col_Unit_Price.DefaultCellStyle = DataGridViewCellStyle50
        Me.Col_Unit_Price.HeaderText = "ราคา/หน่วย"
        Me.Col_Unit_Price.Name = "Col_Unit_Price"
        '
        'Col_Qty
        '
        DataGridViewCellStyle51.NullValue = "0"
        Me.Col_Qty.DefaultCellStyle = DataGridViewCellStyle51
        Me.Col_Qty.HeaderText = "จำนวนสั่ง"
        Me.Col_Qty.Name = "Col_Qty"
        '
        'Col_Qty_ReceiptShow
        '
        DataGridViewCellStyle52.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        DataGridViewCellStyle52.NullValue = "0"
        Me.Col_Qty_ReceiptShow.DefaultCellStyle = DataGridViewCellStyle52
        Me.Col_Qty_ReceiptShow.HeaderText = "จำนวนรับแล้ว"
        Me.Col_Qty_ReceiptShow.Name = "Col_Qty_ReceiptShow"
        Me.Col_Qty_ReceiptShow.ReadOnly = True
        '
        'Col_Discount
        '
        DataGridViewCellStyle53.NullValue = "0"
        Me.Col_Discount.DefaultCellStyle = DataGridViewCellStyle53
        Me.Col_Discount.FillWeight = 80.0!
        Me.Col_Discount.HeaderText = "ส่วนลด"
        Me.Col_Discount.Name = "Col_Discount"
        Me.Col_Discount.Width = 80
        '
        'Col_Amount
        '
        DataGridViewCellStyle54.NullValue = "0"
        Me.Col_Amount.DefaultCellStyle = DataGridViewCellStyle54
        Me.Col_Amount.HeaderText = "รวมเป็นเงิน"
        Me.Col_Amount.Name = "Col_Amount"
        '
        'Col_Currency
        '
        Me.Col_Currency.HeaderText = "สกุลเงิน"
        Me.Col_Currency.Name = "Col_Currency"
        Me.Col_Currency.Visible = False
        Me.Col_Currency.Width = 120
        '
        'Col_Weight
        '
        DataGridViewCellStyle55.NullValue = "0"
        Me.Col_Weight.DefaultCellStyle = DataGridViewCellStyle55
        Me.Col_Weight.HeaderText = "น้ำหนัก"
        Me.Col_Weight.Name = "Col_Weight"
        Me.Col_Weight.Visible = False
        Me.Col_Weight.Width = 105
        '
        'Col_Volume
        '
        DataGridViewCellStyle56.NullValue = "0"
        Me.Col_Volume.DefaultCellStyle = DataGridViewCellStyle56
        Me.Col_Volume.HeaderText = "ปริมาตร"
        Me.Col_Volume.Name = "Col_Volume"
        Me.Col_Volume.Visible = False
        '
        'Col_POItem_Index
        '
        Me.Col_POItem_Index.HeaderText = "Col_POItem_Index"
        Me.Col_POItem_Index.Name = "Col_POItem_Index"
        Me.Col_POItem_Index.Visible = False
        '
        'Co_Percent_Over_Allow
        '
        DataGridViewCellStyle57.NullValue = "0"
        Me.Co_Percent_Over_Allow.DefaultCellStyle = DataGridViewCellStyle57
        Me.Co_Percent_Over_Allow.HeaderText = "เปอร์เซ็นต์อนุญาติให้รับเกิน(%)"
        Me.Co_Percent_Over_Allow.Name = "Co_Percent_Over_Allow"
        Me.Co_Percent_Over_Allow.Width = 180
        '
        'Co_Percent_Under_Allow
        '
        DataGridViewCellStyle58.NullValue = "0"
        Me.Co_Percent_Under_Allow.DefaultCellStyle = DataGridViewCellStyle58
        Me.Co_Percent_Under_Allow.HeaderText = "เปอร์เซ็นต์อนุญาติให้รับขาด(%)"
        Me.Co_Percent_Under_Allow.Name = "Co_Percent_Under_Allow"
        Me.Co_Percent_Under_Allow.Width = 180
        '
        'Col_Remark
        '
        Me.Col_Remark.FillWeight = 150.0!
        Me.Col_Remark.HeaderText = "หมายเหตุ"
        Me.Col_Remark.Name = "Col_Remark"
        Me.Col_Remark.Width = 150
        '
        'col_PurchaseOrder_PR_Index
        '
        Me.col_PurchaseOrder_PR_Index.DataPropertyName = "PurchaseOrder_PR_Index"
        Me.col_PurchaseOrder_PR_Index.HeaderText = "PurchaseOrder_PR_Index"
        Me.col_PurchaseOrder_PR_Index.Name = "col_PurchaseOrder_PR_Index"
        Me.col_PurchaseOrder_PR_Index.ReadOnly = True
        Me.col_PurchaseOrder_PR_Index.Visible = False
        '
        'btnDelete
        '
        Me.btnDelete.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete.Image = CType(resources.GetObject("btnDelete.Image"), System.Drawing.Image)
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(6, 317)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(107, 38)
        Me.btnDelete.TabIndex = 2
        Me.btnDelete.Text = "       ลบรายการ"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'tbpOther
        '
        Me.tbpOther.Controls.Add(Me.grdPOOther)
        Me.tbpOther.Controls.Add(Me.btnDelete1)
        Me.tbpOther.Location = New System.Drawing.Point(4, 22)
        Me.tbpOther.Name = "tbpOther"
        Me.tbpOther.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpOther.Size = New System.Drawing.Size(985, 409)
        Me.tbpOther.TabIndex = 3
        Me.tbpOther.Text = "ค่าใช้จ่ายอื่นๆ"
        Me.tbpOther.UseVisualStyleBackColor = True
        '
        'grdPOOther
        '
        Me.grdPOOther.AllowUserToDeleteRows = False
        Me.grdPOOther.AllowUserToResizeRows = False
        DataGridViewCellStyle59.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdPOOther.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle59
        Me.grdPOOther.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdPOOther.BackgroundColor = System.Drawing.Color.White
        Me.grdPOOther.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPOOther.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Index_Other, Me.Col_Seq_Other, Me.Col_Description_Other, Me.Col_Unit_Price_Other, Me.Col_Qty_Other, Me.Col_Amount_Other, Me.Col_Remark_Other})
        Me.grdPOOther.Location = New System.Drawing.Point(6, 5)
        Me.grdPOOther.Name = "grdPOOther"
        Me.grdPOOther.RowHeadersVisible = False
        Me.grdPOOther.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.grdPOOther.Size = New System.Drawing.Size(972, 335)
        Me.grdPOOther.TabIndex = 0
        '
        'Col_Index_Other
        '
        Me.Col_Index_Other.HeaderText = "Index"
        Me.Col_Index_Other.Name = "Col_Index_Other"
        Me.Col_Index_Other.Visible = False
        '
        'Col_Seq_Other
        '
        Me.Col_Seq_Other.HeaderText = "ลำดับ"
        Me.Col_Seq_Other.Name = "Col_Seq_Other"
        Me.Col_Seq_Other.Width = 40
        '
        'Col_Description_Other
        '
        Me.Col_Description_Other.HeaderText = "ชื่อรายการ"
        Me.Col_Description_Other.Name = "Col_Description_Other"
        Me.Col_Description_Other.Width = 250
        '
        'Col_Unit_Price_Other
        '
        DataGridViewCellStyle60.NullValue = "0"
        Me.Col_Unit_Price_Other.DefaultCellStyle = DataGridViewCellStyle60
        Me.Col_Unit_Price_Other.HeaderText = "ราคา/หน่วย"
        Me.Col_Unit_Price_Other.Name = "Col_Unit_Price_Other"
        '
        'Col_Qty_Other
        '
        DataGridViewCellStyle61.NullValue = "0"
        Me.Col_Qty_Other.DefaultCellStyle = DataGridViewCellStyle61
        Me.Col_Qty_Other.HeaderText = "จำนวน"
        Me.Col_Qty_Other.Name = "Col_Qty_Other"
        '
        'Col_Amount_Other
        '
        DataGridViewCellStyle62.NullValue = "0"
        Me.Col_Amount_Other.DefaultCellStyle = DataGridViewCellStyle62
        Me.Col_Amount_Other.HeaderText = "รวมเป็นเงิน"
        Me.Col_Amount_Other.Name = "Col_Amount_Other"
        Me.Col_Amount_Other.Width = 150
        '
        'Col_Remark_Other
        '
        Me.Col_Remark_Other.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Remark_Other.HeaderText = "หมายเหตุ"
        Me.Col_Remark_Other.Name = "Col_Remark_Other"
        '
        'btnDelete1
        '
        Me.btnDelete1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnDelete1.Image = CType(resources.GetObject("btnDelete1.Image"), System.Drawing.Image)
        Me.btnDelete1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete1.Location = New System.Drawing.Point(6, 346)
        Me.btnDelete1.Name = "btnDelete1"
        Me.btnDelete1.Size = New System.Drawing.Size(107, 38)
        Me.btnDelete1.TabIndex = 4
        Me.btnDelete1.Text = "       ลบรายการ"
        Me.btnDelete1.UseVisualStyleBackColor = True
        '
        'tbpRemain
        '
        Me.tbpRemain.Controls.Add(Me.grdPORemain)
        Me.tbpRemain.Location = New System.Drawing.Point(4, 22)
        Me.tbpRemain.Name = "tbpRemain"
        Me.tbpRemain.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpRemain.Size = New System.Drawing.Size(985, 409)
        Me.tbpRemain.TabIndex = 1
        Me.tbpRemain.Text = "ยอดสินค้าค้างรับ"
        Me.tbpRemain.UseVisualStyleBackColor = True
        '
        'grdPORemain
        '
        Me.grdPORemain.AllowUserToAddRows = False
        Me.grdPORemain.AllowUserToDeleteRows = False
        Me.grdPORemain.AllowUserToResizeRows = False
        DataGridViewCellStyle63.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdPORemain.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle63
        Me.grdPORemain.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdPORemain.BackgroundColor = System.Drawing.Color.White
        Me.grdPORemain.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPORemain.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_SKU_ID_Pending, Me.Col_Description_Pending, Me.Col_Product_Type_Pending, Me.Col_Qty_Pending, Me.Col_UOM_Pending, Me.Col_Qty_Remain, Me.Col_Last_Received_Date, Me.Col_POItem_Index_Pending})
        Me.grdPORemain.Location = New System.Drawing.Point(14, 6)
        Me.grdPORemain.Name = "grdPORemain"
        Me.grdPORemain.ReadOnly = True
        Me.grdPORemain.RowHeadersVisible = False
        Me.grdPORemain.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPORemain.Size = New System.Drawing.Size(964, 378)
        Me.grdPORemain.TabIndex = 0
        '
        'Col_SKU_ID_Pending
        '
        Me.Col_SKU_ID_Pending.HeaderText = "รหัส SKU"
        Me.Col_SKU_ID_Pending.Name = "Col_SKU_ID_Pending"
        Me.Col_SKU_ID_Pending.ReadOnly = True
        '
        'Col_Description_Pending
        '
        Me.Col_Description_Pending.HeaderText = "รายละเอียดสินค้า"
        Me.Col_Description_Pending.Name = "Col_Description_Pending"
        Me.Col_Description_Pending.ReadOnly = True
        Me.Col_Description_Pending.Width = 180
        '
        'Col_Product_Type_Pending
        '
        Me.Col_Product_Type_Pending.HeaderText = "ประเภทสินค้า"
        Me.Col_Product_Type_Pending.Name = "Col_Product_Type_Pending"
        Me.Col_Product_Type_Pending.ReadOnly = True
        '
        'Col_Qty_Pending
        '
        Me.Col_Qty_Pending.HeaderText = "จำนวน"
        Me.Col_Qty_Pending.Name = "Col_Qty_Pending"
        Me.Col_Qty_Pending.ReadOnly = True
        Me.Col_Qty_Pending.Width = 80
        '
        'Col_UOM_Pending
        '
        Me.Col_UOM_Pending.HeaderText = "หน่วย"
        Me.Col_UOM_Pending.Name = "Col_UOM_Pending"
        Me.Col_UOM_Pending.ReadOnly = True
        Me.Col_UOM_Pending.Width = 80
        '
        'Col_Qty_Remain
        '
        Me.Col_Qty_Remain.HeaderText = "ค้างรับ"
        Me.Col_Qty_Remain.Name = "Col_Qty_Remain"
        Me.Col_Qty_Remain.ReadOnly = True
        '
        'Col_Last_Received_Date
        '
        Me.Col_Last_Received_Date.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Last_Received_Date.HeaderText = "วันที่รับล่าสุด"
        Me.Col_Last_Received_Date.Name = "Col_Last_Received_Date"
        Me.Col_Last_Received_Date.ReadOnly = True
        '
        'Col_POItem_Index_Pending
        '
        Me.Col_POItem_Index_Pending.HeaderText = "Col_POItem_Index_Pending"
        Me.Col_POItem_Index_Pending.Name = "Col_POItem_Index_Pending"
        Me.Col_POItem_Index_Pending.ReadOnly = True
        Me.Col_POItem_Index_Pending.Visible = False
        '
        'tbpReceive
        '
        Me.tbpReceive.Controls.Add(Me.grdPOAlreadyReceipt)
        Me.tbpReceive.Location = New System.Drawing.Point(4, 22)
        Me.tbpReceive.Name = "tbpReceive"
        Me.tbpReceive.Size = New System.Drawing.Size(985, 409)
        Me.tbpReceive.TabIndex = 2
        Me.tbpReceive.Text = "สินค้าที่รับแล้ว"
        Me.tbpReceive.UseVisualStyleBackColor = True
        '
        'grdPOAlreadyReceipt
        '
        Me.grdPOAlreadyReceipt.AllowUserToAddRows = False
        Me.grdPOAlreadyReceipt.AllowUserToDeleteRows = False
        Me.grdPOAlreadyReceipt.AllowUserToResizeRows = False
        DataGridViewCellStyle64.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdPOAlreadyReceipt.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle64
        Me.grdPOAlreadyReceipt.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grdPOAlreadyReceipt.BackgroundColor = System.Drawing.Color.White
        Me.grdPOAlreadyReceipt.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPOAlreadyReceipt.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_SKU_ID_Receipt, Me.Col_Description_Receipt, Me.Col_Order_ID_Receipt, Me.Col_Order_Date_Receipt, Me.Col_Qty_Receipt, Me.Col_Unit_Price_Receipt, Me.Col_UOM_Receipt, Me.Col_Total_Receipt})
        Me.grdPOAlreadyReceipt.Location = New System.Drawing.Point(14, 6)
        Me.grdPOAlreadyReceipt.Name = "grdPOAlreadyReceipt"
        Me.grdPOAlreadyReceipt.ReadOnly = True
        Me.grdPOAlreadyReceipt.RowHeadersVisible = False
        Me.grdPOAlreadyReceipt.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdPOAlreadyReceipt.Size = New System.Drawing.Size(964, 369)
        Me.grdPOAlreadyReceipt.TabIndex = 0
        '
        'Col_SKU_ID_Receipt
        '
        Me.Col_SKU_ID_Receipt.HeaderText = "รหัส SKU"
        Me.Col_SKU_ID_Receipt.Name = "Col_SKU_ID_Receipt"
        Me.Col_SKU_ID_Receipt.ReadOnly = True
        '
        'Col_Description_Receipt
        '
        Me.Col_Description_Receipt.FillWeight = 180.0!
        Me.Col_Description_Receipt.HeaderText = "รายละเอียดสินค้า"
        Me.Col_Description_Receipt.Name = "Col_Description_Receipt"
        Me.Col_Description_Receipt.ReadOnly = True
        Me.Col_Description_Receipt.Width = 180
        '
        'Col_Order_ID_Receipt
        '
        Me.Col_Order_ID_Receipt.FillWeight = 140.0!
        Me.Col_Order_ID_Receipt.HeaderText = "เอกสารรับเลขที่"
        Me.Col_Order_ID_Receipt.Name = "Col_Order_ID_Receipt"
        Me.Col_Order_ID_Receipt.ReadOnly = True
        Me.Col_Order_ID_Receipt.Width = 140
        '
        'Col_Order_Date_Receipt
        '
        Me.Col_Order_Date_Receipt.HeaderText = "วันที่รับ"
        Me.Col_Order_Date_Receipt.Name = "Col_Order_Date_Receipt"
        Me.Col_Order_Date_Receipt.ReadOnly = True
        '
        'Col_Qty_Receipt
        '
        Me.Col_Qty_Receipt.HeaderText = "จำนวน"
        Me.Col_Qty_Receipt.Name = "Col_Qty_Receipt"
        Me.Col_Qty_Receipt.ReadOnly = True
        Me.Col_Qty_Receipt.Width = 80
        '
        'Col_Unit_Price_Receipt
        '
        Me.Col_Unit_Price_Receipt.HeaderText = "ราคา/หน่วย"
        Me.Col_Unit_Price_Receipt.Name = "Col_Unit_Price_Receipt"
        Me.Col_Unit_Price_Receipt.ReadOnly = True
        Me.Col_Unit_Price_Receipt.Width = 90
        '
        'Col_UOM_Receipt
        '
        Me.Col_UOM_Receipt.HeaderText = "หน่วย"
        Me.Col_UOM_Receipt.Name = "Col_UOM_Receipt"
        Me.Col_UOM_Receipt.ReadOnly = True
        Me.Col_UOM_Receipt.Width = 80
        '
        'Col_Total_Receipt
        '
        Me.Col_Total_Receipt.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Total_Receipt.HeaderText = "รวมเป็นเงิน"
        Me.Col_Total_Receipt.Name = "Col_Total_Receipt"
        Me.Col_Total_Receipt.ReadOnly = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "OrderItem_Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 80
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "Sku_Index"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.Visible = False
        Me.DataGridViewTextBoxColumn2.Width = 120
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.Frozen = True
        Me.DataGridViewTextBoxColumn3.HeaderText = "Sku_Id"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.Frozen = True
        Me.DataGridViewTextBoxColumn4.HeaderText = "Product_Name_th"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Width = 70
        '
        'DataGridViewTextBoxColumn5
        '
        DataGridViewCellStyle65.NullValue = "0"
        Me.DataGridViewTextBoxColumn5.DefaultCellStyle = DataGridViewCellStyle65
        Me.DataGridViewTextBoxColumn5.Frozen = True
        Me.DataGridViewTextBoxColumn5.HeaderText = "Qty"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 70
        '
        'DataGridViewTextBoxColumn6
        '
        DataGridViewCellStyle66.NullValue = "0"
        Me.DataGridViewTextBoxColumn6.DefaultCellStyle = DataGridViewCellStyle66
        Me.DataGridViewTextBoxColumn6.Frozen = True
        Me.DataGridViewTextBoxColumn6.HeaderText = "Plan_Qty"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Width = 300
        '
        'DataGridViewTextBoxColumn7
        '
        DataGridViewCellStyle67.NullValue = "0"
        Me.DataGridViewTextBoxColumn7.DefaultCellStyle = DataGridViewCellStyle67
        Me.DataGridViewTextBoxColumn7.HeaderText = "Pallet_Quantity"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.Visible = False
        Me.DataGridViewTextBoxColumn7.Width = 300
        '
        'DataGridViewTextBoxColumn8
        '
        DataGridViewCellStyle68.NullValue = "0"
        Me.DataGridViewTextBoxColumn8.DefaultCellStyle = DataGridViewCellStyle68
        Me.DataGridViewTextBoxColumn8.HeaderText = "Weight"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 150
        '
        'DataGridViewTextBoxColumn9
        '
        DataGridViewCellStyle69.NullValue = "0"
        Me.DataGridViewTextBoxColumn9.DefaultCellStyle = DataGridViewCellStyle69
        Me.DataGridViewTextBoxColumn9.HeaderText = "Volume"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 120
        '
        'DataGridViewTextBoxColumn10
        '
        DataGridViewCellStyle70.NullValue = "0"
        Me.DataGridViewTextBoxColumn10.DefaultCellStyle = DataGridViewCellStyle70
        Me.DataGridViewTextBoxColumn10.HeaderText = "Serial_No"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 105
        '
        'DataGridViewTextBoxColumn11
        '
        DataGridViewCellStyle71.NullValue = "0"
        Me.DataGridViewTextBoxColumn11.DefaultCellStyle = DataGridViewCellStyle71
        Me.DataGridViewTextBoxColumn11.HeaderText = "Mfg_Date"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Visible = False
        Me.DataGridViewTextBoxColumn11.Width = 150
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn12.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn12.HeaderText = "Exp_date"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Visible = False
        '
        'DataGridViewTextBoxColumn13
        '
        DataGridViewCellStyle72.NullValue = "0"
        Me.DataGridViewTextBoxColumn13.DefaultCellStyle = DataGridViewCellStyle72
        Me.DataGridViewTextBoxColumn13.HeaderText = "PLot"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Visible = False
        Me.DataGridViewTextBoxColumn13.Width = 105
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn14.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn14.HeaderText = "Item_Status_Index"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Visible = False
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Total_Received_Qty"
        Me.DataGridViewTextBoxColumn15.FillWeight = 200.0!
        Me.DataGridViewTextBoxColumn15.HeaderText = "Package_Index"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Visible = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "PurchaseOrder_Index"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Item_Status_Index"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn17.HeaderText = "ประเภทสินค้า"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn18.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Visible = False
        Me.DataGridViewTextBoxColumn18.Width = 180
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "Qty"
        Me.DataGridViewTextBoxColumn19.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        Me.DataGridViewTextBoxColumn19.Visible = False
        Me.DataGridViewTextBoxColumn19.Width = 80
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "DesPack"
        Me.DataGridViewTextBoxColumn20.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        Me.DataGridViewTextBoxColumn20.Visible = False
        Me.DataGridViewTextBoxColumn20.Width = 80
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "PurQty"
        Me.DataGridViewTextBoxColumn21.HeaderText = "ค้างร้บ"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.ReadOnly = True
        Me.DataGridViewTextBoxColumn21.Visible = False
        Me.DataGridViewTextBoxColumn21.Width = 180
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "Last_Received_Date"
        Me.DataGridViewTextBoxColumn22.HeaderText = "วันที่รับ"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.ReadOnly = True
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "PurchaseOrderItem_Index"
        Me.DataGridViewTextBoxColumn23.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.ReadOnly = True
        Me.DataGridViewTextBoxColumn23.Visible = False
        Me.DataGridViewTextBoxColumn23.Width = 80
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "Sku_Id"
        Me.DataGridViewTextBoxColumn24.HeaderText = "รหัส SKU"
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Width = 180
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "Order_No"
        Me.DataGridViewTextBoxColumn25.HeaderText = "เอกสารรับเลขที่"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Str1"
        Me.DataGridViewTextBoxColumn27.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        Me.DataGridViewTextBoxColumn27.Visible = False
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "Qty"
        Me.DataGridViewTextBoxColumn28.HeaderText = "จำนวน"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        Me.DataGridViewTextBoxColumn28.Visible = False
        Me.DataGridViewTextBoxColumn28.Width = 80
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn29.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.Visible = False
        Me.DataGridViewTextBoxColumn29.Width = 80
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "UnitPrice"
        Me.DataGridViewTextBoxColumn30.HeaderText = "ราคา/หน่วย"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        Me.DataGridViewTextBoxColumn30.Width = 90
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn31.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.Visible = False
        Me.DataGridViewTextBoxColumn31.Width = 80
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn32.DataPropertyName = "totalQty"
        Me.DataGridViewTextBoxColumn32.HeaderText = "รวมเป็นเงิน"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn33.HeaderText = "หน่วย"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        Me.DataGridViewTextBoxColumn33.ReadOnly = True
        Me.DataGridViewTextBoxColumn33.Width = 80
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn34.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        Me.DataGridViewTextBoxColumn34.Visible = False
        Me.DataGridViewTextBoxColumn34.Width = 90
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn35.DataPropertyName = "totalQty"
        Me.DataGridViewTextBoxColumn35.HeaderText = "รวมเป็นเงิน"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.DataPropertyName = "PurchaseOrder_No"
        Me.DataGridViewTextBoxColumn36.HeaderText = "ColIndex"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        Me.DataGridViewTextBoxColumn36.ReadOnly = True
        Me.DataGridViewTextBoxColumn36.Visible = False
        '
        'DataGridViewTextBoxColumn37
        '
        Me.DataGridViewTextBoxColumn37.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn37.DataPropertyName = "totalQty"
        Me.DataGridViewTextBoxColumn37.HeaderText = "รวมเป็นเงิน"
        Me.DataGridViewTextBoxColumn37.Name = "DataGridViewTextBoxColumn37"
        Me.DataGridViewTextBoxColumn37.ReadOnly = True
        '
        'grbPrintReport
        '
        Me.grbPrintReport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.grbPrintReport.Controls.Add(Me.btnPrint)
        Me.grbPrintReport.Controls.Add(Me.cboPrint)
        Me.grbPrintReport.Location = New System.Drawing.Point(626, 641)
        Me.grbPrintReport.Name = "grbPrintReport"
        Me.grbPrintReport.Size = New System.Drawing.Size(244, 51)
        Me.grbPrintReport.TabIndex = 6
        Me.grbPrintReport.TabStop = False
        Me.grbPrintReport.Text = "พิมพ์เอกสาร"
        '
        'btnPrint
        '
        Me.btnPrint.Enabled = False
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(129, 10)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(107, 38)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "พิมพ์รายงาน"
        Me.btnPrint.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'cboPrint
        '
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.Enabled = False
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Items.AddRange(New Object() {"ใบสั่งงานนำสินค้าออก"})
        Me.cboPrint.Location = New System.Drawing.Point(8, 16)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(114, 21)
        Me.cboPrint.TabIndex = 2
        '
        'tabHeader
        '
        Me.tabHeader.Controls.Add(Me.TabOrderReturn)
        Me.tabHeader.Controls.Add(Me.TabPage2)
        Me.tabHeader.Controls.Add(Me.tbpVisible)
        Me.tabHeader.Location = New System.Drawing.Point(-1, 0)
        Me.tabHeader.Name = "tabHeader"
        Me.tabHeader.SelectedIndex = 0
        Me.tabHeader.Size = New System.Drawing.Size(1000, 199)
        Me.tabHeader.TabIndex = 33
        Me.tabHeader.TabStop = False
        '
        'TabOrderReturn
        '
        Me.TabOrderReturn.Controls.Add(Me.btnEdit_Special)
        Me.TabOrderReturn.Controls.Add(Me.txtDepartment_Id)
        Me.TabOrderReturn.Controls.Add(Me.txtDepartment_Name)
        Me.TabOrderReturn.Controls.Add(Me.btnSeachDepartment)
        Me.TabOrderReturn.Controls.Add(Me.txtStatus)
        Me.TabOrderReturn.Controls.Add(Me.lblStatus)
        Me.TabOrderReturn.Controls.Add(Me.btnCarrier)
        Me.TabOrderReturn.Controls.Add(Me.txtCarrier_Name)
        Me.TabOrderReturn.Controls.Add(Me.txtCarrier_ID)
        Me.TabOrderReturn.Controls.Add(Me.btnCustomer_Receive)
        Me.TabOrderReturn.Controls.Add(Me.txtShipping_Location_ID)
        Me.TabOrderReturn.Controls.Add(Me.txtTax_No)
        Me.TabOrderReturn.Controls.Add(Me.Label8)
        Me.TabOrderReturn.Controls.Add(Me.dtpDue_Date)
        Me.TabOrderReturn.Controls.Add(Me.txtCustomer_Id)
        Me.TabOrderReturn.Controls.Add(Me.lblExRate)
        Me.TabOrderReturn.Controls.Add(Me.txtCustomer_Name)
        Me.TabOrderReturn.Controls.Add(Me.txtExRate)
        Me.TabOrderReturn.Controls.Add(Me.btnCustomer)
        Me.TabOrderReturn.Controls.Add(Me.cboCurrency)
        Me.TabOrderReturn.Controls.Add(Me.lblDay)
        Me.TabOrderReturn.Controls.Add(Me.lblCurrency)
        Me.TabOrderReturn.Controls.Add(Me.lblCreditTerm)
        Me.TabOrderReturn.Controls.Add(Me.cboConditionPay)
        Me.TabOrderReturn.Controls.Add(Me.txtCreditTerm)
        Me.TabOrderReturn.Controls.Add(Me.lblPaymentType)
        Me.TabOrderReturn.Controls.Add(Me.lblDue_Date)
        Me.TabOrderReturn.Controls.Add(Me.txtShip_Address1)
        Me.TabOrderReturn.Controls.Add(Me.lblRef2)
        Me.TabOrderReturn.Controls.Add(Me.txtRef2)
        Me.TabOrderReturn.Controls.Add(Me.lblRef1)
        Me.TabOrderReturn.Controls.Add(Me.txtUser)
        Me.TabOrderReturn.Controls.Add(Me.txtRef1)
        Me.TabOrderReturn.Controls.Add(Me.lblUser)
        Me.TabOrderReturn.Controls.Add(Me.txtSupplier_Id)
        Me.TabOrderReturn.Controls.Add(Me.txtSupplier_Name)
        Me.TabOrderReturn.Controls.Add(Me.btnSeachSupplier)
        Me.TabOrderReturn.Controls.Add(Me.lblPO_Date)
        Me.TabOrderReturn.Controls.Add(Me.dtpPO_Date)
        Me.TabOrderReturn.Controls.Add(Me.txtPO_No)
        Me.TabOrderReturn.Controls.Add(Me.lblRemark)
        Me.TabOrderReturn.Controls.Add(Me.txtRemark)
        Me.TabOrderReturn.Controls.Add(Me.cboDocumentType)
        Me.TabOrderReturn.Controls.Add(Me.lblCarrier)
        Me.TabOrderReturn.Controls.Add(Me.lblReceivedLocation)
        Me.TabOrderReturn.Controls.Add(Me.lblCustomer)
        Me.TabOrderReturn.Controls.Add(Me.lblDepartment)
        Me.TabOrderReturn.Controls.Add(Me.lblSupplier)
        Me.TabOrderReturn.Controls.Add(Me.lblPO_No)
        Me.TabOrderReturn.Controls.Add(Me.lblDocumentType)
        Me.TabOrderReturn.Location = New System.Drawing.Point(4, 22)
        Me.TabOrderReturn.Name = "TabOrderReturn"
        Me.TabOrderReturn.Padding = New System.Windows.Forms.Padding(3)
        Me.TabOrderReturn.Size = New System.Drawing.Size(992, 173)
        Me.TabOrderReturn.TabIndex = 2
        Me.TabOrderReturn.Text = "ข้อมูลเอกสารการแจ้งรับ"
        Me.TabOrderReturn.UseVisualStyleBackColor = True
        '
        'txtDepartment_Id
        '
        Me.txtDepartment_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtDepartment_Id.Location = New System.Drawing.Point(104, 142)
        Me.txtDepartment_Id.MaxLength = 100
        Me.txtDepartment_Id.Name = "txtDepartment_Id"
        Me.txtDepartment_Id.ReadOnly = True
        Me.txtDepartment_Id.Size = New System.Drawing.Size(106, 20)
        Me.txtDepartment_Id.TabIndex = 8
        '
        'txtDepartment_Name
        '
        Me.txtDepartment_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtDepartment_Name.Location = New System.Drawing.Point(238, 142)
        Me.txtDepartment_Name.Name = "txtDepartment_Name"
        Me.txtDepartment_Name.ReadOnly = True
        Me.txtDepartment_Name.Size = New System.Drawing.Size(239, 20)
        Me.txtDepartment_Name.TabIndex = 370
        '
        'btnSeachDepartment
        '
        Me.btnSeachDepartment.Location = New System.Drawing.Point(213, 142)
        Me.btnSeachDepartment.Name = "btnSeachDepartment"
        Me.btnSeachDepartment.Size = New System.Drawing.Size(24, 23)
        Me.btnSeachDepartment.TabIndex = 369
        Me.btnSeachDepartment.Text = "..."
        Me.btnSeachDepartment.UseVisualStyleBackColor = True
        '
        'txtStatus
        '
        Me.txtStatus.Location = New System.Drawing.Point(344, 32)
        Me.txtStatus.MaxLength = 100
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(133, 20)
        Me.txtStatus.TabIndex = 3
        Me.txtStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblStatus.Location = New System.Drawing.Point(300, 33)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(42, 17)
        Me.lblStatus.TabIndex = 367
        Me.lblStatus.Text = "สถานะ"
        Me.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblStatus.Visible = False
        '
        'btnCarrier
        '
        Me.btnCarrier.Location = New System.Drawing.Point(213, 120)
        Me.btnCarrier.Name = "btnCarrier"
        Me.btnCarrier.Size = New System.Drawing.Size(24, 23)
        Me.btnCarrier.TabIndex = 363
        Me.btnCarrier.Text = "..."
        Me.btnCarrier.UseVisualStyleBackColor = True
        '
        'txtCarrier_Name
        '
        Me.txtCarrier_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCarrier_Name.Location = New System.Drawing.Point(238, 120)
        Me.txtCarrier_Name.MaxLength = 500
        Me.txtCarrier_Name.Name = "txtCarrier_Name"
        Me.txtCarrier_Name.ReadOnly = True
        Me.txtCarrier_Name.Size = New System.Drawing.Size(239, 20)
        Me.txtCarrier_Name.TabIndex = 365
        '
        'txtCarrier_ID
        '
        Me.txtCarrier_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCarrier_ID.Location = New System.Drawing.Point(104, 119)
        Me.txtCarrier_ID.MaxLength = 50
        Me.txtCarrier_ID.Name = "txtCarrier_ID"
        Me.txtCarrier_ID.ReadOnly = True
        Me.txtCarrier_ID.Size = New System.Drawing.Size(106, 20)
        Me.txtCarrier_ID.TabIndex = 7
        '
        'btnCustomer_Receive
        '
        Me.btnCustomer_Receive.Location = New System.Drawing.Point(213, 98)
        Me.btnCustomer_Receive.Name = "btnCustomer_Receive"
        Me.btnCustomer_Receive.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer_Receive.TabIndex = 359
        Me.btnCustomer_Receive.Text = "..."
        Me.btnCustomer_Receive.UseVisualStyleBackColor = True
        '
        'txtShipping_Location_ID
        '
        Me.txtShipping_Location_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtShipping_Location_ID.Location = New System.Drawing.Point(104, 97)
        Me.txtShipping_Location_ID.MaxLength = 50
        Me.txtShipping_Location_ID.Name = "txtShipping_Location_ID"
        Me.txtShipping_Location_ID.ReadOnly = True
        Me.txtShipping_Location_ID.Size = New System.Drawing.Size(106, 20)
        Me.txtShipping_Location_ID.TabIndex = 6
        '
        'txtTax_No
        '
        Me.txtTax_No.Location = New System.Drawing.Point(623, 7)
        Me.txtTax_No.MaxLength = 100
        Me.txtTax_No.Name = "txtTax_No"
        Me.txtTax_No.Size = New System.Drawing.Size(363, 20)
        Me.txtTax_No.TabIndex = 9
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label8.Location = New System.Drawing.Point(478, 6)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(139, 20)
        Me.Label8.TabIndex = 357
        Me.Label8.Text = "หมายเลขประจำตัวผู้เสียภาษี"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpDue_Date
        '
        Me.dtpDue_Date.CustomFormat = "dd/MM/yyyy"
        Me.dtpDue_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDue_Date.Location = New System.Drawing.Point(854, 99)
        Me.dtpDue_Date.Name = "dtpDue_Date"
        Me.dtpDue_Date.Size = New System.Drawing.Size(106, 20)
        Me.dtpDue_Date.TabIndex = 17
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(104, 75)
        Me.txtCustomer_Id.MaxLength = 50
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomer_Id.TabIndex = 5
        '
        'lblExRate
        '
        Me.lblExRate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblExRate.Location = New System.Drawing.Point(768, 76)
        Me.lblExRate.Name = "lblExRate"
        Me.lblExRate.Size = New System.Drawing.Size(86, 18)
        Me.lblExRate.TabIndex = 30
        Me.lblExRate.Text = "อัตราแลกเปลี่ยน"
        Me.lblExRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Name.Location = New System.Drawing.Point(238, 76)
        Me.txtCustomer_Name.MaxLength = 500
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(239, 20)
        Me.txtCustomer_Name.TabIndex = 355
        '
        'txtExRate
        '
        Me.txtExRate.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtExRate.Location = New System.Drawing.Point(854, 76)
        Me.txtExRate.Name = "txtExRate"
        Me.txtExRate.ReadOnly = True
        Me.txtExRate.Size = New System.Drawing.Size(133, 20)
        Me.txtExRate.TabIndex = 15
        Me.txtExRate.Text = "0"
        Me.txtExRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnCustomer
        '
        Me.btnCustomer.Location = New System.Drawing.Point(213, 76)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer.TabIndex = 353
        Me.btnCustomer.Text = "..."
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'cboCurrency
        '
        Me.cboCurrency.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCurrency.FormattingEnabled = True
        Me.cboCurrency.Location = New System.Drawing.Point(854, 53)
        Me.cboCurrency.Name = "cboCurrency"
        Me.cboCurrency.Size = New System.Drawing.Size(133, 21)
        Me.cboCurrency.TabIndex = 13
        '
        'lblDay
        '
        Me.lblDay.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDay.Location = New System.Drawing.Point(728, 100)
        Me.lblDay.Name = "lblDay"
        Me.lblDay.Size = New System.Drawing.Size(28, 17)
        Me.lblDay.TabIndex = 34
        Me.lblDay.Text = "วัน"
        Me.lblDay.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblCurrency
        '
        Me.lblCurrency.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCurrency.Location = New System.Drawing.Point(802, 55)
        Me.lblCurrency.Name = "lblCurrency"
        Me.lblCurrency.Size = New System.Drawing.Size(52, 15)
        Me.lblCurrency.TabIndex = 26
        Me.lblCurrency.Text = "สกุลเงิน"
        Me.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCreditTerm
        '
        Me.lblCreditTerm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCreditTerm.Location = New System.Drawing.Point(557, 102)
        Me.lblCreditTerm.Name = "lblCreditTerm"
        Me.lblCreditTerm.Size = New System.Drawing.Size(65, 14)
        Me.lblCreditTerm.TabIndex = 32
        Me.lblCreditTerm.Text = "เครดิต"
        Me.lblCreditTerm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboConditionPay
        '
        Me.cboConditionPay.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboConditionPay.FormattingEnabled = True
        Me.cboConditionPay.Location = New System.Drawing.Point(623, 76)
        Me.cboConditionPay.Name = "cboConditionPay"
        Me.cboConditionPay.Size = New System.Drawing.Size(133, 21)
        Me.cboConditionPay.TabIndex = 14
        '
        'txtCreditTerm
        '
        Me.txtCreditTerm.Location = New System.Drawing.Point(623, 99)
        Me.txtCreditTerm.MaxLength = 10
        Me.txtCreditTerm.Name = "txtCreditTerm"
        Me.txtCreditTerm.Size = New System.Drawing.Size(98, 20)
        Me.txtCreditTerm.TabIndex = 16
        Me.txtCreditTerm.Text = "0"
        Me.txtCreditTerm.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblPaymentType
        '
        Me.lblPaymentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPaymentType.Location = New System.Drawing.Point(532, 79)
        Me.lblPaymentType.Name = "lblPaymentType"
        Me.lblPaymentType.Size = New System.Drawing.Size(90, 13)
        Me.lblPaymentType.TabIndex = 28
        Me.lblPaymentType.Text = "ประเภทการชำระเงิน"
        Me.lblPaymentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDue_Date
        '
        Me.lblDue_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDue_Date.Location = New System.Drawing.Point(768, 102)
        Me.lblDue_Date.Name = "lblDue_Date"
        Me.lblDue_Date.Size = New System.Drawing.Size(86, 15)
        Me.lblDue_Date.TabIndex = 35
        Me.lblDue_Date.Text = "กำหนดวันที่รับ"
        Me.lblDue_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShip_Address1
        '
        Me.txtShip_Address1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtShip_Address1.Location = New System.Drawing.Point(238, 98)
        Me.txtShip_Address1.Name = "txtShip_Address1"
        Me.txtShip_Address1.ReadOnly = True
        Me.txtShip_Address1.Size = New System.Drawing.Size(239, 20)
        Me.txtShip_Address1.TabIndex = 16
        Me.txtShip_Address1.TabStop = False
        '
        'lblRef2
        '
        Me.lblRef2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRef2.Location = New System.Drawing.Point(538, 55)
        Me.lblRef2.Name = "lblRef2"
        Me.lblRef2.Size = New System.Drawing.Size(84, 15)
        Me.lblRef2.TabIndex = 24
        Me.lblRef2.Text = "เอกสารอ้างอิง 2"
        Me.lblRef2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRef2
        '
        Me.txtRef2.Location = New System.Drawing.Point(623, 53)
        Me.txtRef2.MaxLength = 100
        Me.txtRef2.Name = "txtRef2"
        Me.txtRef2.Size = New System.Drawing.Size(133, 20)
        Me.txtRef2.TabIndex = 12
        '
        'lblRef1
        '
        Me.lblRef1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRef1.Location = New System.Drawing.Point(538, 32)
        Me.lblRef1.Name = "lblRef1"
        Me.lblRef1.Size = New System.Drawing.Size(84, 15)
        Me.lblRef1.TabIndex = 20
        Me.lblRef1.Text = "เอกสารอ้างอิง 1"
        Me.lblRef1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUser
        '
        Me.txtUser.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtUser.Location = New System.Drawing.Point(854, 31)
        Me.txtUser.MaxLength = 100
        Me.txtUser.Name = "txtUser"
        Me.txtUser.ReadOnly = True
        Me.txtUser.Size = New System.Drawing.Size(133, 20)
        Me.txtUser.TabIndex = 11
        '
        'txtRef1
        '
        Me.txtRef1.Location = New System.Drawing.Point(623, 30)
        Me.txtRef1.MaxLength = 100
        Me.txtRef1.Name = "txtRef1"
        Me.txtRef1.Size = New System.Drawing.Size(133, 20)
        Me.txtRef1.TabIndex = 10
        '
        'lblUser
        '
        Me.lblUser.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUser.Location = New System.Drawing.Point(776, 33)
        Me.lblUser.Name = "lblUser"
        Me.lblUser.Size = New System.Drawing.Size(77, 15)
        Me.lblUser.TabIndex = 22
        Me.lblUser.Text = "ผู้ทำรายการ"
        Me.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSupplier_Id
        '
        Me.txtSupplier_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSupplier_Id.Location = New System.Drawing.Point(104, 53)
        Me.txtSupplier_Id.MaxLength = 50
        Me.txtSupplier_Id.Name = "txtSupplier_Id"
        Me.txtSupplier_Id.ReadOnly = True
        Me.txtSupplier_Id.Size = New System.Drawing.Size(106, 20)
        Me.txtSupplier_Id.TabIndex = 4
        '
        'txtSupplier_Name
        '
        Me.txtSupplier_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSupplier_Name.Location = New System.Drawing.Point(238, 54)
        Me.txtSupplier_Name.MaxLength = 500
        Me.txtSupplier_Name.Name = "txtSupplier_Name"
        Me.txtSupplier_Name.ReadOnly = True
        Me.txtSupplier_Name.Size = New System.Drawing.Size(239, 20)
        Me.txtSupplier_Name.TabIndex = 4
        '
        'btnSeachSupplier
        '
        Me.btnSeachSupplier.Location = New System.Drawing.Point(213, 52)
        Me.btnSeachSupplier.Name = "btnSeachSupplier"
        Me.btnSeachSupplier.Size = New System.Drawing.Size(24, 23)
        Me.btnSeachSupplier.TabIndex = 6
        Me.btnSeachSupplier.Text = "..."
        Me.btnSeachSupplier.UseVisualStyleBackColor = True
        '
        'lblPO_Date
        '
        Me.lblPO_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPO_Date.Location = New System.Drawing.Point(257, 6)
        Me.lblPO_Date.Name = "lblPO_Date"
        Me.lblPO_Date.Size = New System.Drawing.Size(79, 13)
        Me.lblPO_Date.TabIndex = 2
        Me.lblPO_Date.Text = "วันที่เอกสาร"
        Me.lblPO_Date.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'dtpPO_Date
        '
        Me.dtpPO_Date.CustomFormat = "dd/MM/yyyy"
        Me.dtpPO_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPO_Date.Location = New System.Drawing.Point(344, 4)
        Me.dtpPO_Date.Name = "dtpPO_Date"
        Me.dtpPO_Date.Size = New System.Drawing.Size(133, 20)
        Me.dtpPO_Date.TabIndex = 1
        Me.dtpPO_Date.TabStop = False
        '
        'txtPO_No
        '
        Me.txtPO_No.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtPO_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtPO_No.ForeColor = System.Drawing.Color.Black
        Me.txtPO_No.Location = New System.Drawing.Point(102, 6)
        Me.txtPO_No.Name = "txtPO_No"
        Me.txtPO_No.Size = New System.Drawing.Size(132, 20)
        Me.txtPO_No.TabIndex = 0
        Me.txtPO_No.TabStop = False
        '
        'lblRemark
        '
        Me.lblRemark.Location = New System.Drawing.Point(512, 124)
        Me.lblRemark.Name = "lblRemark"
        Me.lblRemark.Size = New System.Drawing.Size(104, 13)
        Me.lblRemark.TabIndex = 291
        Me.lblRemark.Text = "หมายเหตุ"
        Me.lblRemark.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRemark
        '
        Me.txtRemark.BackColor = System.Drawing.SystemColors.Window
        Me.txtRemark.Location = New System.Drawing.Point(622, 120)
        Me.txtRemark.Multiline = True
        Me.txtRemark.Name = "txtRemark"
        Me.txtRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtRemark.Size = New System.Drawing.Size(365, 42)
        Me.txtRemark.TabIndex = 18
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(103, 29)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(130, 21)
        Me.cboDocumentType.TabIndex = 2
        Me.cboDocumentType.TabStop = False
        '
        'lblCarrier
        '
        Me.lblCarrier.AutoSize = True
        Me.lblCarrier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCarrier.Location = New System.Drawing.Point(50, 124)
        Me.lblCarrier.Name = "lblCarrier"
        Me.lblCarrier.Size = New System.Drawing.Size(50, 13)
        Me.lblCarrier.TabIndex = 362
        Me.lblCarrier.Text = "จัดส่งโดย"
        Me.lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblReceivedLocation
        '
        Me.lblReceivedLocation.AutoSize = True
        Me.lblReceivedLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReceivedLocation.Location = New System.Drawing.Point(36, 100)
        Me.lblReceivedLocation.Name = "lblReceivedLocation"
        Me.lblReceivedLocation.Size = New System.Drawing.Size(64, 13)
        Me.lblReceivedLocation.TabIndex = 358
        Me.lblReceivedLocation.Text = "สถานที่จัดส่ง"
        Me.lblReceivedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCustomer
        '
        Me.lblCustomer.AutoSize = True
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(19, 79)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(81, 13)
        Me.lblCustomer.TabIndex = 352
        Me.lblCustomer.Text = "ลูกค้า/เจ้าของ"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDepartment
        '
        Me.lblDepartment.AutoSize = True
        Me.lblDepartment.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDepartment.Location = New System.Drawing.Point(63, 146)
        Me.lblDepartment.Name = "lblDepartment"
        Me.lblDepartment.Size = New System.Drawing.Size(37, 13)
        Me.lblDepartment.TabIndex = 312
        Me.lblDepartment.Text = "แผนก"
        Me.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblSupplier
        '
        Me.lblSupplier.AutoSize = True
        Me.lblSupplier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSupplier.Location = New System.Drawing.Point(64, 56)
        Me.lblSupplier.Name = "lblSupplier"
        Me.lblSupplier.Size = New System.Drawing.Size(36, 13)
        Me.lblSupplier.TabIndex = 4
        Me.lblSupplier.Text = "ผู้ขาย"
        Me.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPO_No
        '
        Me.lblPO_No.AutoSize = True
        Me.lblPO_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPO_No.Location = New System.Drawing.Point(22, 9)
        Me.lblPO_No.Name = "lblPO_No"
        Me.lblPO_No.Size = New System.Drawing.Size(78, 13)
        Me.lblPO_No.TabIndex = 0
        Me.lblPO_No.Text = "เลขที่ใบสั่งซื้อ"
        Me.lblPO_No.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblDocumentType
        '
        Me.lblDocumentType.AutoSize = True
        Me.lblDocumentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDocumentType.ForeColor = System.Drawing.Color.Black
        Me.lblDocumentType.Location = New System.Drawing.Point(9, 33)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(91, 13)
        Me.lblDocumentType.TabIndex = 301
        Me.lblDocumentType.Text = "ประเภทเอกสาร"
        Me.lblDocumentType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.GroupBox1)
        Me.TabPage2.Controls.Add(Me.grbSuplier)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(992, 173)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "ข้อมูลเอกสารเพิ่มเติม"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtSupplier_Fax)
        Me.GroupBox1.Controls.Add(Me.txtSupplier_Phone)
        Me.GroupBox1.Controls.Add(Me.lblPO_Fax)
        Me.GroupBox1.Controls.Add(Me.lblPO_Phone)
        Me.GroupBox1.Controls.Add(Me.txtSupplier_Address)
        Me.GroupBox1.Controls.Add(Me.lblPO_Address)
        Me.GroupBox1.Location = New System.Drawing.Point(482, 7)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(472, 160)
        Me.GroupBox1.TabIndex = 344
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เจ้าของสินค้าและผู้รับ"
        '
        'txtSupplier_Fax
        '
        Me.txtSupplier_Fax.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier_Fax.Location = New System.Drawing.Point(317, 95)
        Me.txtSupplier_Fax.MaxLength = 50
        Me.txtSupplier_Fax.Name = "txtSupplier_Fax"
        Me.txtSupplier_Fax.Size = New System.Drawing.Size(133, 20)
        Me.txtSupplier_Fax.TabIndex = 36
        '
        'txtSupplier_Phone
        '
        Me.txtSupplier_Phone.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier_Phone.Location = New System.Drawing.Point(100, 94)
        Me.txtSupplier_Phone.MaxLength = 50
        Me.txtSupplier_Phone.Name = "txtSupplier_Phone"
        Me.txtSupplier_Phone.Size = New System.Drawing.Size(133, 20)
        Me.txtSupplier_Phone.TabIndex = 35
        '
        'lblPO_Fax
        '
        Me.lblPO_Fax.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPO_Fax.Location = New System.Drawing.Point(256, 99)
        Me.lblPO_Fax.Name = "lblPO_Fax"
        Me.lblPO_Fax.Size = New System.Drawing.Size(59, 13)
        Me.lblPO_Fax.TabIndex = 40
        Me.lblPO_Fax.Text = "โทรสาร"
        Me.lblPO_Fax.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPO_Phone
        '
        Me.lblPO_Phone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPO_Phone.Location = New System.Drawing.Point(34, 98)
        Me.lblPO_Phone.Name = "lblPO_Phone"
        Me.lblPO_Phone.Size = New System.Drawing.Size(60, 13)
        Me.lblPO_Phone.TabIndex = 39
        Me.lblPO_Phone.Text = "โทรศัพท์"
        Me.lblPO_Phone.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSupplier_Address
        '
        Me.txtSupplier_Address.BackColor = System.Drawing.SystemColors.Window
        Me.txtSupplier_Address.Location = New System.Drawing.Point(100, 13)
        Me.txtSupplier_Address.MaxLength = 1000
        Me.txtSupplier_Address.Multiline = True
        Me.txtSupplier_Address.Name = "txtSupplier_Address"
        Me.txtSupplier_Address.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSupplier_Address.Size = New System.Drawing.Size(357, 76)
        Me.txtSupplier_Address.TabIndex = 34
        '
        'lblPO_Address
        '
        Me.lblPO_Address.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPO_Address.Location = New System.Drawing.Point(13, 15)
        Me.lblPO_Address.Name = "lblPO_Address"
        Me.lblPO_Address.Size = New System.Drawing.Size(81, 13)
        Me.lblPO_Address.TabIndex = 37
        Me.lblPO_Address.Text = "ที่อยู่"
        Me.lblPO_Address.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grbSuplier
        '
        Me.grbSuplier.Controls.Add(Me.txtShip_Fax)
        Me.grbSuplier.Controls.Add(Me.txtShip_Phone)
        Me.grbSuplier.Controls.Add(Me.Label3)
        Me.grbSuplier.Controls.Add(Me.Label4)
        Me.grbSuplier.Controls.Add(Me.txtShip_Address)
        Me.grbSuplier.Controls.Add(Me.Label5)
        Me.grbSuplier.Location = New System.Drawing.Point(6, 4)
        Me.grbSuplier.Name = "grbSuplier"
        Me.grbSuplier.Size = New System.Drawing.Size(470, 163)
        Me.grbSuplier.TabIndex = 343
        Me.grbSuplier.TabStop = False
        Me.grbSuplier.Text = "ข้อมูลผู้ขาย"
        '
        'txtShip_Fax
        '
        Me.txtShip_Fax.BackColor = System.Drawing.SystemColors.Window
        Me.txtShip_Fax.Location = New System.Drawing.Point(327, 101)
        Me.txtShip_Fax.MaxLength = 50
        Me.txtShip_Fax.Name = "txtShip_Fax"
        Me.txtShip_Fax.Size = New System.Drawing.Size(133, 20)
        Me.txtShip_Fax.TabIndex = 19
        '
        'txtShip_Phone
        '
        Me.txtShip_Phone.BackColor = System.Drawing.SystemColors.Window
        Me.txtShip_Phone.Location = New System.Drawing.Point(103, 100)
        Me.txtShip_Phone.MaxLength = 50
        Me.txtShip_Phone.Name = "txtShip_Phone"
        Me.txtShip_Phone.Size = New System.Drawing.Size(133, 20)
        Me.txtShip_Phone.TabIndex = 18
        '
        'Label3
        '
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(264, 104)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 13)
        Me.Label3.TabIndex = 22
        Me.Label3.Text = "โทรสาร"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.Location = New System.Drawing.Point(37, 103)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(60, 13)
        Me.Label4.TabIndex = 21
        Me.Label4.Text = "โทรศัพท์"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtShip_Address
        '
        Me.txtShip_Address.BackColor = System.Drawing.SystemColors.Window
        Me.txtShip_Address.Location = New System.Drawing.Point(103, 16)
        Me.txtShip_Address.MaxLength = 1000
        Me.txtShip_Address.Multiline = True
        Me.txtShip_Address.Name = "txtShip_Address"
        Me.txtShip_Address.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtShip_Address.Size = New System.Drawing.Size(357, 79)
        Me.txtShip_Address.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(16, 18)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 13)
        Me.Label5.TabIndex = 20
        Me.Label5.Text = "ที่อยู่"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'tbpVisible
        '
        Me.tbpVisible.Location = New System.Drawing.Point(4, 22)
        Me.tbpVisible.Name = "tbpVisible"
        Me.tbpVisible.Padding = New System.Windows.Forms.Padding(3)
        Me.tbpVisible.Size = New System.Drawing.Size(992, 173)
        Me.tbpVisible.TabIndex = 3
        Me.tbpVisible.Text = "ซ่อน"
        Me.tbpVisible.UseVisualStyleBackColor = True
        '
        'btnRecived
        '
        Me.btnRecived.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRecived.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnRecived.Image = CType(resources.GetObject("btnRecived.Image"), System.Drawing.Image)
        Me.btnRecived.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRecived.Location = New System.Drawing.Point(338, 654)
        Me.btnRecived.Name = "btnRecived"
        Me.btnRecived.Size = New System.Drawing.Size(107, 38)
        Me.btnRecived.TabIndex = 35
        Me.btnRecived.Text = "รับสินค้า"
        Me.btnRecived.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRecived.UseVisualStyleBackColor = True
        '
        'btnConfirm
        '
        Me.btnConfirm.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(225, 654)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(107, 38)
        Me.btnConfirm.TabIndex = 34
        Me.btnConfirm.Text = "    ยืนยัน"
        Me.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'btnClose_PO
        '
        Me.btnClose_PO.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnClose_PO.Enabled = False
        Me.btnClose_PO.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClose_PO.Image = CType(resources.GetObject("btnClose_PO.Image"), System.Drawing.Image)
        Me.btnClose_PO.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose_PO.Location = New System.Drawing.Point(112, 654)
        Me.btnClose_PO.Name = "btnClose_PO"
        Me.btnClose_PO.Size = New System.Drawing.Size(107, 38)
        Me.btnClose_PO.TabIndex = 30
        Me.btnClose_PO.Text = "ปิดใบสั่งซื้อ"
        Me.btnClose_PO.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose_PO.UseVisualStyleBackColor = True
        '
        'btnCompare
        '
        Me.btnCompare.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCompare.Image = CType(resources.GetObject("btnCompare.Image"), System.Drawing.Image)
        Me.btnCompare.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCompare.Location = New System.Drawing.Point(451, 654)
        Me.btnCompare.Name = "btnCompare"
        Me.btnCompare.Size = New System.Drawing.Size(107, 38)
        Me.btnCompare.TabIndex = 31
        Me.btnCompare.Text = "ตรวจสอบการสั่งซื้อ"
        Me.btnCompare.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCompare.UseVisualStyleBackColor = True
        Me.btnCompare.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(873, 651)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 32
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(-1, 654)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 29
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnEdit_Special
        '
        Me.btnEdit_Special.Location = New System.Drawing.Point(966, 99)
        Me.btnEdit_Special.Name = "btnEdit_Special"
        Me.btnEdit_Special.Size = New System.Drawing.Size(21, 20)
        Me.btnEdit_Special.TabIndex = 371
        Me.btnEdit_Special.Text = "S"
        Me.btnEdit_Special.UseVisualStyleBackColor = True
        '
        'frmPO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1004, 701)
        Me.Controls.Add(Me.btnRecived)
        Me.Controls.Add(Me.btnConfirm)
        Me.Controls.Add(Me.btnCompare)
        Me.Controls.Add(Me.tabHeader)
        Me.Controls.Add(Me.btnClose_PO)
        Me.Controls.Add(Me.grbPrintReport)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.tbcPO)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPO"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ใบสั่งซื้อ"
        Me.tbcPO.ResumeLayout(False)
        Me.tbpPO.ResumeLayout(False)
        Me.gbAmountSummary.ResumeLayout(False)
        Me.gbAmountSummary.PerformLayout()
        CType(Me.grdPOItem, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpOther.ResumeLayout(False)
        CType(Me.grdPOOther, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpRemain.ResumeLayout(False)
        CType(Me.grdPORemain, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tbpReceive.ResumeLayout(False)
        CType(Me.grdPOAlreadyReceipt, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbPrintReport.ResumeLayout(False)
        Me.tabHeader.ResumeLayout(False)
        Me.TabOrderReturn.ResumeLayout(False)
        Me.TabOrderReturn.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.grbSuplier.ResumeLayout(False)
        Me.grbSuplier.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbcPO As System.Windows.Forms.TabControl
    Friend WithEvents tbpPO As System.Windows.Forms.TabPage
    Friend WithEvents grdPOItem As System.Windows.Forms.DataGridView
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents tbpRemain As System.Windows.Forms.TabPage
    Friend WithEvents tbpReceive As System.Windows.Forms.TabPage
    Friend WithEvents grdPORemain As System.Windows.Forms.DataGridView
    Friend WithEvents grdPOAlreadyReceipt As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCompare As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents tbpOther As System.Windows.Forms.TabPage
    Friend WithEvents grdPOOther As System.Windows.Forms.DataGridView
    Friend WithEvents btnDelete1 As System.Windows.Forms.Button
    Friend WithEvents grbPrintReport As System.Windows.Forms.GroupBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose_PO As System.Windows.Forms.Button
    Friend WithEvents Col_SKU_ID_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Product_Type_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_UOM_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_Remain As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Last_Received_Date As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_POItem_Index_Pending As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Index_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Seq_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Unit_Price_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Amount_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Remark_Other As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SKU_ID_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Order_ID_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Order_Date_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Unit_Price_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_UOM_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Total_Receipt As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents BtnInsert As System.Windows.Forms.Button
    Friend WithEvents BtnSort As System.Windows.Forms.Button
    Friend WithEvents tabHeader As System.Windows.Forms.TabControl
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents TabOrderReturn As System.Windows.Forms.TabPage
    Friend WithEvents dtpDue_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents lblExRate As System.Windows.Forms.Label
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtExRate As System.Windows.Forms.TextBox
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents cboCurrency As System.Windows.Forms.ComboBox
    Friend WithEvents lblDay As System.Windows.Forms.Label
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents lblCurrency As System.Windows.Forms.Label
    Friend WithEvents lblCreditTerm As System.Windows.Forms.Label
    Friend WithEvents cboConditionPay As System.Windows.Forms.ComboBox
    Friend WithEvents txtCreditTerm As System.Windows.Forms.TextBox
    Friend WithEvents lblPaymentType As System.Windows.Forms.Label
    Friend WithEvents lblDue_Date As System.Windows.Forms.Label
    Friend WithEvents txtShip_Address1 As System.Windows.Forms.TextBox
    Friend WithEvents lblRef2 As System.Windows.Forms.Label
    Friend WithEvents lblDepartment As System.Windows.Forms.Label
    Friend WithEvents txtRef2 As System.Windows.Forms.TextBox
    Friend WithEvents lblRef1 As System.Windows.Forms.Label
    Friend WithEvents txtUser As System.Windows.Forms.TextBox
    Friend WithEvents txtRef1 As System.Windows.Forms.TextBox
    Friend WithEvents lblUser As System.Windows.Forms.Label
    Friend WithEvents txtSupplier_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplier_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnSeachSupplier As System.Windows.Forms.Button
    Friend WithEvents lblSupplier As System.Windows.Forms.Label
    Friend WithEvents lblPO_No As System.Windows.Forms.Label
    Friend WithEvents lblPO_Date As System.Windows.Forms.Label
    Friend WithEvents dtpPO_Date As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtPO_No As System.Windows.Forms.TextBox
    Friend WithEvents lblRemark As System.Windows.Forms.Label
    Friend WithEvents txtRemark As System.Windows.Forms.TextBox
    Friend WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents txtTax_No As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents lblReceivedLocation As System.Windows.Forms.Label
    Friend WithEvents btnCustomer_Receive As System.Windows.Forms.Button
    Friend WithEvents txtShipping_Location_ID As System.Windows.Forms.TextBox
    Friend WithEvents tbpVisible As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents grbSuplier As System.Windows.Forms.GroupBox
    Friend WithEvents txtShip_Fax As System.Windows.Forms.TextBox
    Friend WithEvents txtShip_Phone As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtShip_Address As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSupplier_Fax As System.Windows.Forms.TextBox
    Friend WithEvents txtSupplier_Phone As System.Windows.Forms.TextBox
    Friend WithEvents lblPO_Fax As System.Windows.Forms.Label
    Friend WithEvents lblPO_Phone As System.Windows.Forms.Label
    Friend WithEvents txtSupplier_Address As System.Windows.Forms.TextBox
    Friend WithEvents lblPO_Address As System.Windows.Forms.Label
    Friend WithEvents lblCarrier As System.Windows.Forms.Label
    Friend WithEvents btnCarrier As System.Windows.Forms.Button
    Friend WithEvents txtCarrier_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtCarrier_ID As System.Windows.Forms.TextBox
    Friend WithEvents gbAmountSummary As System.Windows.Forms.GroupBox
    Friend WithEvents chkDiscount As System.Windows.Forms.CheckBox
    Friend WithEvents btnMultiLevelDiscount As System.Windows.Forms.Button
    Friend WithEvents lblPercent2 As System.Windows.Forms.Label
    Friend WithEvents chkVAT As System.Windows.Forms.CheckBox
    Friend WithEvents lblCurrency1 As System.Windows.Forms.Label
    Friend WithEvents txtSubtotal As System.Windows.Forms.TextBox
    Friend WithEvents lblCurrency5 As System.Windows.Forms.Label
    Friend WithEvents lblCurrency2 As System.Windows.Forms.Label
    Friend WithEvents lblCurrency4 As System.Windows.Forms.Label
    Friend WithEvents lblDeposit_Amt As System.Windows.Forms.Label
    Friend WithEvents lblCurrency3 As System.Windows.Forms.Label
    Friend WithEvents txtNet_Amt As System.Windows.Forms.TextBox
    Friend WithEvents txtDeposit_Amt As System.Windows.Forms.TextBox
    Friend WithEvents lblNet_Amt As System.Windows.Forms.Label
    Friend WithEvents txtVAT As System.Windows.Forms.TextBox
    Friend WithEvents lblPercent1 As System.Windows.Forms.Label
    Friend WithEvents txtVAT_Percent As System.Windows.Forms.TextBox
    Friend WithEvents lblSubtotal As System.Windows.Forms.Label
    Friend WithEvents txtDiscount_Amt As System.Windows.Forms.TextBox
    Friend WithEvents txtDiscount_Percent As System.Windows.Forms.TextBox
    Friend WithEvents btnRecived As System.Windows.Forms.Button
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents txtDepartment_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtDepartment_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnSeachDepartment As System.Windows.Forms.Button
    Friend WithEvents btnPO_PR_Import As System.Windows.Forms.Button
    Friend WithEvents col_no As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SKU_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Btn_GetSKU As System.Windows.Forms.DataGridViewButtonColumn
    Friend WithEvents Col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Product_Type As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_UOM As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Unit_Price As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Qty_ReceiptShow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Discount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Amount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Currency As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Weight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Volume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_POItem_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Co_Percent_Over_Allow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Co_Percent_Under_Allow As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PurchaseOrder_PR_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnEdit_Special As System.Windows.Forms.Button
End Class
