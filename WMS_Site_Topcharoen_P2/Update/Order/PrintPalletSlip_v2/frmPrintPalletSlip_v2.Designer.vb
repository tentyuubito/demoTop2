<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintPalletSlip_v2
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
        Me.gboData = New System.Windows.Forms.GroupBox
        Me.btnPO_Popup = New System.Windows.Forms.Button
        Me.cboItemStatus = New System.Windows.Forms.ComboBox
        Me.lblItemStatus = New System.Windows.Forms.Label
        Me.txtDocumentType = New System.Windows.Forms.TextBox
        Me.txtOrder_Date = New System.Windows.Forms.TextBox
        Me.btnSku = New System.Windows.Forms.Button
        Me.lblSku_Desc = New System.Windows.Forms.Label
        Me.txtSku_Desc = New System.Windows.Forms.TextBox
        Me.txtSku = New System.Windows.Forms.TextBox
        Me.lblSku = New System.Windows.Forms.Label
        Me.lblPallet = New System.Windows.Forms.Label
        Me.txtPalletQty = New System.Windows.Forms.TextBox
        Me.lblUnitWeight = New System.Windows.Forms.Label
        Me.txtUnitWeight = New System.Windows.Forms.TextBox
        Me.lblUnitVolume = New System.Windows.Forms.Label
        Me.txtUnitVolume = New System.Windows.Forms.TextBox
        Me.txtPlot = New System.Windows.Forms.TextBox
        Me.lblPlot = New System.Windows.Forms.Label
        Me.txtRefNo2 = New System.Windows.Forms.TextBox
        Me.lblRefNo2 = New System.Windows.Forms.Label
        Me.txtRefNo1 = New System.Windows.Forms.TextBox
        Me.lblOrder_Date = New System.Windows.Forms.Label
        Me.lblRefNo1 = New System.Windows.Forms.Label
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.dtpExpire_Date = New System.Windows.Forms.DateTimePicker
        Me.lblPosting_Date = New System.Windows.Forms.Label
        Me.dtpPosting_Date = New System.Windows.Forms.DateTimePicker
        Me.lblExpire_Date = New System.Windows.Forms.Label
        Me.cboPackageSku = New System.Windows.Forms.ComboBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.lblTag_No = New System.Windows.Forms.Label
        Me.txtQtyPerPallet = New System.Windows.Forms.TextBox
        Me.lblQtyPerPallet = New System.Windows.Forms.Label
        Me.txtTag_No = New System.Windows.Forms.TextBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.lblUnitMain = New System.Windows.Forms.Label
        Me.lblUnitMainQty = New System.Windows.Forms.Label
        Me.lblUnitMainPackage = New System.Windows.Forms.Label
        Me.chkPreview = New System.Windows.Forms.CheckBox
        Me.pdoPalletSlip = New System.Drawing.Printing.PrintDocument
        Me.lblPadding = New System.Windows.Forms.Label
        Me.lblPaddingPackage = New System.Windows.Forms.Label
        Me.lblPaddingQty = New System.Windows.Forms.Label
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.btnTag_Weight = New System.Windows.Forms.Button
        Me.lblSerialPort = New System.Windows.Forms.Label
        Me.cboSerialPort = New System.Windows.Forms.ComboBox
        Me.txtWeightScale = New System.Windows.Forms.TextBox
        Me.lblWeightScale = New System.Windows.Forms.Label
        Me.gboData.SuspendLayout()
        Me.SuspendLayout()
        '
        'gboData
        '
        Me.gboData.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboData.Controls.Add(Me.btnPO_Popup)
        Me.gboData.Controls.Add(Me.cboItemStatus)
        Me.gboData.Controls.Add(Me.lblItemStatus)
        Me.gboData.Controls.Add(Me.txtDocumentType)
        Me.gboData.Controls.Add(Me.txtOrder_Date)
        Me.gboData.Controls.Add(Me.btnSku)
        Me.gboData.Controls.Add(Me.lblSku_Desc)
        Me.gboData.Controls.Add(Me.txtSku_Desc)
        Me.gboData.Controls.Add(Me.txtSku)
        Me.gboData.Controls.Add(Me.lblSku)
        Me.gboData.Controls.Add(Me.lblPallet)
        Me.gboData.Controls.Add(Me.txtPalletQty)
        Me.gboData.Controls.Add(Me.lblUnitWeight)
        Me.gboData.Controls.Add(Me.txtUnitWeight)
        Me.gboData.Controls.Add(Me.lblUnitVolume)
        Me.gboData.Controls.Add(Me.txtUnitVolume)
        Me.gboData.Controls.Add(Me.txtPlot)
        Me.gboData.Controls.Add(Me.lblPlot)
        Me.gboData.Controls.Add(Me.txtRefNo2)
        Me.gboData.Controls.Add(Me.lblRefNo2)
        Me.gboData.Controls.Add(Me.txtRefNo1)
        Me.gboData.Controls.Add(Me.lblOrder_Date)
        Me.gboData.Controls.Add(Me.lblRefNo1)
        Me.gboData.Controls.Add(Me.lblDocumentType)
        Me.gboData.Controls.Add(Me.dtpExpire_Date)
        Me.gboData.Controls.Add(Me.lblPosting_Date)
        Me.gboData.Controls.Add(Me.dtpPosting_Date)
        Me.gboData.Controls.Add(Me.lblExpire_Date)
        Me.gboData.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.gboData.Location = New System.Drawing.Point(12, 12)
        Me.gboData.Name = "gboData"
        Me.gboData.Size = New System.Drawing.Size(700, 251)
        Me.gboData.TabIndex = 0
        Me.gboData.TabStop = False
        Me.gboData.Text = "ข้อมูล"
        '
        'btnPO_Popup
        '
        Me.btnPO_Popup.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btnPO_Popup.Location = New System.Drawing.Point(659, 25)
        Me.btnPO_Popup.Name = "btnPO_Popup"
        Me.btnPO_Popup.Size = New System.Drawing.Size(30, 26)
        Me.btnPO_Popup.TabIndex = 85
        Me.btnPO_Popup.Text = "..."
        Me.btnPO_Popup.UseVisualStyleBackColor = True
        '
        'cboItemStatus
        '
        Me.cboItemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.cboItemStatus.FormattingEnabled = True
        Me.cboItemStatus.Location = New System.Drawing.Point(470, 184)
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.Size = New System.Drawing.Size(183, 28)
        Me.cboItemStatus.TabIndex = 11
        '
        'lblItemStatus
        '
        Me.lblItemStatus.AutoSize = True
        Me.lblItemStatus.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblItemStatus.ForeColor = System.Drawing.Color.Black
        Me.lblItemStatus.Location = New System.Drawing.Point(407, 188)
        Me.lblItemStatus.Name = "lblItemStatus"
        Me.lblItemStatus.Size = New System.Drawing.Size(57, 20)
        Me.lblItemStatus.TabIndex = 84
        Me.lblItemStatus.Text = "สถานะ :"
        '
        'txtDocumentType
        '
        Me.txtDocumentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtDocumentType.Location = New System.Drawing.Point(130, 57)
        Me.txtDocumentType.Multiline = True
        Me.txtDocumentType.Name = "txtDocumentType"
        Me.txtDocumentType.ReadOnly = True
        Me.txtDocumentType.Size = New System.Drawing.Size(183, 26)
        Me.txtDocumentType.TabIndex = 2
        Me.txtDocumentType.TabStop = False
        '
        'txtOrder_Date
        '
        Me.txtOrder_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtOrder_Date.Location = New System.Drawing.Point(130, 25)
        Me.txtOrder_Date.Multiline = True
        Me.txtOrder_Date.Name = "txtOrder_Date"
        Me.txtOrder_Date.ReadOnly = True
        Me.txtOrder_Date.Size = New System.Drawing.Size(183, 26)
        Me.txtOrder_Date.TabIndex = 1
        Me.txtOrder_Date.TabStop = False
        '
        'btnSku
        '
        Me.btnSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.btnSku.Location = New System.Drawing.Point(319, 89)
        Me.btnSku.Name = "btnSku"
        Me.btnSku.Size = New System.Drawing.Size(30, 26)
        Me.btnSku.TabIndex = 6
        Me.btnSku.Text = "..."
        Me.btnSku.UseVisualStyleBackColor = True
        '
        'lblSku_Desc
        '
        Me.lblSku_Desc.AutoSize = True
        Me.lblSku_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku_Desc.ForeColor = System.Drawing.Color.Black
        Me.lblSku_Desc.Location = New System.Drawing.Point(397, 92)
        Me.lblSku_Desc.Name = "lblSku_Desc"
        Me.lblSku_Desc.Size = New System.Drawing.Size(67, 20)
        Me.lblSku_Desc.TabIndex = 83
        Me.lblSku_Desc.Text = "ชื่อสินค้า :"
        '
        'txtSku_Desc
        '
        Me.txtSku_Desc.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSku_Desc.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSku_Desc.ForeColor = System.Drawing.Color.Black
        Me.txtSku_Desc.Location = New System.Drawing.Point(470, 89)
        Me.txtSku_Desc.Multiline = True
        Me.txtSku_Desc.Name = "txtSku_Desc"
        Me.txtSku_Desc.ReadOnly = True
        Me.txtSku_Desc.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtSku_Desc.Size = New System.Drawing.Size(183, 55)
        Me.txtSku_Desc.TabIndex = 7
        Me.txtSku_Desc.TabStop = False
        '
        'txtSku
        '
        Me.txtSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtSku.Location = New System.Drawing.Point(130, 89)
        Me.txtSku.Multiline = True
        Me.txtSku.Name = "txtSku"
        Me.txtSku.ReadOnly = True
        Me.txtSku.Size = New System.Drawing.Size(183, 26)
        Me.txtSku.TabIndex = 5
        Me.txtSku.TabStop = False
        '
        'lblSku
        '
        Me.lblSku.AutoSize = True
        Me.lblSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku.ForeColor = System.Drawing.Color.Black
        Me.lblSku.Location = New System.Drawing.Point(50, 92)
        Me.lblSku.Name = "lblSku"
        Me.lblSku.Size = New System.Drawing.Size(74, 20)
        Me.lblSku.TabIndex = 81
        Me.lblSku.Text = "รหัสสินค้า :"
        '
        'lblPallet
        '
        Me.lblPallet.AutoSize = True
        Me.lblPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPallet.ForeColor = System.Drawing.Color.Black
        Me.lblPallet.Location = New System.Drawing.Point(22, 188)
        Me.lblPallet.Name = "lblPallet"
        Me.lblPallet.Size = New System.Drawing.Size(102, 20)
        Me.lblPallet.TabIndex = 79
        Me.lblPallet.Text = "จำนวน/พาเลท :"
        '
        'txtPalletQty
        '
        Me.txtPalletQty.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtPalletQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtPalletQty.ForeColor = System.Drawing.Color.Black
        Me.txtPalletQty.Location = New System.Drawing.Point(130, 185)
        Me.txtPalletQty.Name = "txtPalletQty"
        Me.txtPalletQty.ReadOnly = True
        Me.txtPalletQty.Size = New System.Drawing.Size(183, 26)
        Me.txtPalletQty.TabIndex = 12
        Me.txtPalletQty.TabStop = False
        Me.txtPalletQty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblUnitWeight
        '
        Me.lblUnitWeight.AutoSize = True
        Me.lblUnitWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUnitWeight.ForeColor = System.Drawing.Color.Black
        Me.lblUnitWeight.Location = New System.Drawing.Point(27, 220)
        Me.lblUnitWeight.Name = "lblUnitWeight"
        Me.lblUnitWeight.Size = New System.Drawing.Size(97, 20)
        Me.lblUnitWeight.TabIndex = 77
        Me.lblUnitWeight.Text = "นำหนัก/หน่วย :"
        '
        'txtUnitWeight
        '
        Me.txtUnitWeight.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtUnitWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtUnitWeight.ForeColor = System.Drawing.Color.Black
        Me.txtUnitWeight.Location = New System.Drawing.Point(130, 217)
        Me.txtUnitWeight.Name = "txtUnitWeight"
        Me.txtUnitWeight.ReadOnly = True
        Me.txtUnitWeight.Size = New System.Drawing.Size(183, 26)
        Me.txtUnitWeight.TabIndex = 14
        Me.txtUnitWeight.TabStop = False
        Me.txtUnitWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblUnitVolume
        '
        Me.lblUnitVolume.AutoSize = True
        Me.lblUnitVolume.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUnitVolume.ForeColor = System.Drawing.Color.Black
        Me.lblUnitVolume.Location = New System.Drawing.Point(361, 220)
        Me.lblUnitVolume.Name = "lblUnitVolume"
        Me.lblUnitVolume.Size = New System.Drawing.Size(103, 20)
        Me.lblUnitVolume.TabIndex = 75
        Me.lblUnitVolume.Text = "ปริมาตร/หน่วย :"
        '
        'txtUnitVolume
        '
        Me.txtUnitVolume.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtUnitVolume.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtUnitVolume.ForeColor = System.Drawing.Color.Black
        Me.txtUnitVolume.Location = New System.Drawing.Point(470, 217)
        Me.txtUnitVolume.Name = "txtUnitVolume"
        Me.txtUnitVolume.ReadOnly = True
        Me.txtUnitVolume.Size = New System.Drawing.Size(183, 26)
        Me.txtUnitVolume.TabIndex = 13
        Me.txtUnitVolume.TabStop = False
        Me.txtUnitVolume.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPlot
        '
        Me.txtPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!)
        Me.txtPlot.Location = New System.Drawing.Point(130, 121)
        Me.txtPlot.Name = "txtPlot"
        Me.txtPlot.Size = New System.Drawing.Size(183, 26)
        Me.txtPlot.TabIndex = 8
        '
        'lblPlot
        '
        Me.lblPlot.AutoSize = True
        Me.lblPlot.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPlot.ForeColor = System.Drawing.Color.Black
        Me.lblPlot.Location = New System.Drawing.Point(38, 124)
        Me.lblPlot.Name = "lblPlot"
        Me.lblPlot.Size = New System.Drawing.Size(86, 20)
        Me.lblPlot.TabIndex = 73
        Me.lblPlot.Text = "Lot/Batch :"
        '
        'txtRefNo2
        '
        Me.txtRefNo2.Location = New System.Drawing.Point(470, 57)
        Me.txtRefNo2.Multiline = True
        Me.txtRefNo2.Name = "txtRefNo2"
        Me.txtRefNo2.Size = New System.Drawing.Size(183, 26)
        Me.txtRefNo2.TabIndex = 4
        '
        'lblRefNo2
        '
        Me.lblRefNo2.AutoSize = True
        Me.lblRefNo2.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRefNo2.ForeColor = System.Drawing.Color.Black
        Me.lblRefNo2.Location = New System.Drawing.Point(352, 60)
        Me.lblRefNo2.Name = "lblRefNo2"
        Me.lblRefNo2.Size = New System.Drawing.Size(112, 20)
        Me.lblRefNo2.TabIndex = 71
        Me.lblRefNo2.Text = "เอกสารอ้างอิง 2 :"
        '
        'txtRefNo1
        '
        Me.txtRefNo1.Location = New System.Drawing.Point(470, 25)
        Me.txtRefNo1.Multiline = True
        Me.txtRefNo1.Name = "txtRefNo1"
        Me.txtRefNo1.Size = New System.Drawing.Size(183, 26)
        Me.txtRefNo1.TabIndex = 3
        '
        'lblOrder_Date
        '
        Me.lblOrder_Date.AutoSize = True
        Me.lblOrder_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblOrder_Date.ForeColor = System.Drawing.Color.Black
        Me.lblOrder_Date.Location = New System.Drawing.Point(65, 28)
        Me.lblOrder_Date.Name = "lblOrder_Date"
        Me.lblOrder_Date.Size = New System.Drawing.Size(59, 20)
        Me.lblOrder_Date.TabIndex = 69
        Me.lblOrder_Date.Text = "วันที่รับ :"
        '
        'lblRefNo1
        '
        Me.lblRefNo1.AutoSize = True
        Me.lblRefNo1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblRefNo1.ForeColor = System.Drawing.Color.Black
        Me.lblRefNo1.Location = New System.Drawing.Point(352, 28)
        Me.lblRefNo1.Name = "lblRefNo1"
        Me.lblRefNo1.Size = New System.Drawing.Size(112, 20)
        Me.lblRefNo1.TabIndex = 68
        Me.lblRefNo1.Text = "เอกสารอ้างอิง 1 :"
        '
        'lblDocumentType
        '
        Me.lblDocumentType.AutoSize = True
        Me.lblDocumentType.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblDocumentType.ForeColor = System.Drawing.Color.Black
        Me.lblDocumentType.Location = New System.Drawing.Point(14, 60)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(110, 20)
        Me.lblDocumentType.TabIndex = 59
        Me.lblDocumentType.Text = "ประเภทเอกสาร :"
        '
        'dtpExpire_Date
        '
        Me.dtpExpire_Date.Checked = False
        Me.dtpExpire_Date.CustomFormat = "dd/MM/yyy"
        Me.dtpExpire_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.dtpExpire_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpExpire_Date.Location = New System.Drawing.Point(469, 150)
        Me.dtpExpire_Date.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtpExpire_Date.Name = "dtpExpire_Date"
        Me.dtpExpire_Date.ShowCheckBox = True
        Me.dtpExpire_Date.Size = New System.Drawing.Size(183, 26)
        Me.dtpExpire_Date.TabIndex = 10
        '
        'lblPosting_Date
        '
        Me.lblPosting_Date.AutoSize = True
        Me.lblPosting_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPosting_Date.ForeColor = System.Drawing.Color.Black
        Me.lblPosting_Date.Location = New System.Drawing.Point(65, 156)
        Me.lblPosting_Date.Name = "lblPosting_Date"
        Me.lblPosting_Date.Size = New System.Drawing.Size(59, 20)
        Me.lblPosting_Date.TabIndex = 61
        Me.lblPosting_Date.Text = "วันผลิต :"
        '
        'dtpPosting_Date
        '
        Me.dtpPosting_Date.Checked = False
        Me.dtpPosting_Date.CustomFormat = "dd/MM/yyy"
        Me.dtpPosting_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.dtpPosting_Date.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpPosting_Date.Location = New System.Drawing.Point(130, 153)
        Me.dtpPosting_Date.MaxDate = New Date(2100, 12, 31, 0, 0, 0, 0)
        Me.dtpPosting_Date.Name = "dtpPosting_Date"
        Me.dtpPosting_Date.ShowCheckBox = True
        Me.dtpPosting_Date.Size = New System.Drawing.Size(183, 26)
        Me.dtpPosting_Date.TabIndex = 9
        '
        'lblExpire_Date
        '
        Me.lblExpire_Date.AutoSize = True
        Me.lblExpire_Date.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblExpire_Date.ForeColor = System.Drawing.Color.Black
        Me.lblExpire_Date.Location = New System.Drawing.Point(379, 153)
        Me.lblExpire_Date.Name = "lblExpire_Date"
        Me.lblExpire_Date.Size = New System.Drawing.Size(84, 20)
        Me.lblExpire_Date.TabIndex = 64
        Me.lblExpire_Date.Text = "วันหมดอายุ :"
        '
        'cboPackageSku
        '
        Me.cboPackageSku.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPackageSku.Enabled = False
        Me.cboPackageSku.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!)
        Me.cboPackageSku.FormattingEnabled = True
        Me.cboPackageSku.Location = New System.Drawing.Point(321, 329)
        Me.cboPackageSku.Name = "cboPackageSku"
        Me.cboPackageSku.Size = New System.Drawing.Size(154, 33)
        Me.cboPackageSku.TabIndex = 17
        '
        'btnPrint
        '
        Me.btnPrint.Font = New System.Drawing.Font("Microsoft Sans Serif", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(481, 322)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(209, 69)
        Me.btnPrint.TabIndex = 20
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'lblTag_No
        '
        Me.lblTag_No.AutoSize = True
        Me.lblTag_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTag_No.ForeColor = System.Drawing.Color.Black
        Me.lblTag_No.Location = New System.Drawing.Point(72, 281)
        Me.lblTag_No.Name = "lblTag_No"
        Me.lblTag_No.Size = New System.Drawing.Size(115, 29)
        Me.lblTag_No.TabIndex = 35
        Me.lblTag_No.Text = "Pallet ID :"
        '
        'txtQtyPerPallet
        '
        Me.txtQtyPerPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtQtyPerPallet.ForeColor = System.Drawing.Color.Black
        Me.txtQtyPerPallet.Location = New System.Drawing.Point(193, 329)
        Me.txtQtyPerPallet.MaxLength = 10
        Me.txtQtyPerPallet.Name = "txtQtyPerPallet"
        Me.txtQtyPerPallet.Size = New System.Drawing.Size(118, 47)
        Me.txtQtyPerPallet.TabIndex = 16
        Me.txtQtyPerPallet.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblQtyPerPallet
        '
        Me.lblQtyPerPallet.AutoSize = True
        Me.lblQtyPerPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblQtyPerPallet.ForeColor = System.Drawing.Color.Black
        Me.lblQtyPerPallet.Location = New System.Drawing.Point(29, 338)
        Me.lblQtyPerPallet.Name = "lblQtyPerPallet"
        Me.lblQtyPerPallet.Size = New System.Drawing.Size(158, 29)
        Me.lblQtyPerPallet.TabIndex = 37
        Me.lblQtyPerPallet.Text = "จำนวน/Pallet :"
        '
        'txtTag_No
        '
        Me.txtTag_No.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        Me.txtTag_No.Enabled = False
        Me.txtTag_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 26.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtTag_No.ForeColor = System.Drawing.Color.Black
        Me.txtTag_No.Location = New System.Drawing.Point(193, 269)
        Me.txtTag_No.Name = "txtTag_No"
        Me.txtTag_No.Size = New System.Drawing.Size(471, 47)
        Me.txtTag_No.TabIndex = 15
        '
        'btnClose
        '
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(588, 424)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(103, 38)
        Me.btnClose.TabIndex = 21
        Me.btnClose.Text = "ปิด"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'lblUnitMain
        '
        Me.lblUnitMain.AutoSize = True
        Me.lblUnitMain.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUnitMain.ForeColor = System.Drawing.Color.Black
        Me.lblUnitMain.Location = New System.Drawing.Point(71, 402)
        Me.lblUnitMain.Name = "lblUnitMain"
        Me.lblUnitMain.Size = New System.Drawing.Size(116, 20)
        Me.lblUnitMain.TabIndex = 81
        Me.lblUnitMain.Text = "จำนวนหน่วยหลัก :"
        Me.lblUnitMain.Visible = False
        '
        'lblUnitMainQty
        '
        Me.lblUnitMainQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUnitMainQty.ForeColor = System.Drawing.Color.Black
        Me.lblUnitMainQty.Location = New System.Drawing.Point(193, 402)
        Me.lblUnitMainQty.Name = "lblUnitMainQty"
        Me.lblUnitMainQty.Size = New System.Drawing.Size(118, 20)
        Me.lblUnitMainQty.TabIndex = 82
        Me.lblUnitMainQty.Text = "-"
        Me.lblUnitMainQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblUnitMainPackage
        '
        Me.lblUnitMainPackage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblUnitMainPackage.ForeColor = System.Drawing.Color.Black
        Me.lblUnitMainPackage.Location = New System.Drawing.Point(317, 402)
        Me.lblUnitMainPackage.Name = "lblUnitMainPackage"
        Me.lblUnitMainPackage.Size = New System.Drawing.Size(150, 20)
        Me.lblUnitMainPackage.TabIndex = 83
        '
        'chkPreview
        '
        Me.chkPreview.AutoSize = True
        Me.chkPreview.Checked = True
        Me.chkPreview.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPreview.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkPreview.Location = New System.Drawing.Point(393, 368)
        Me.chkPreview.Name = "chkPreview"
        Me.chkPreview.Size = New System.Drawing.Size(82, 24)
        Me.chkPreview.TabIndex = 18
        Me.chkPreview.Text = "Preview"
        Me.chkPreview.UseVisualStyleBackColor = True
        '
        'lblPadding
        '
        Me.lblPadding.AutoSize = True
        Me.lblPadding.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPadding.ForeColor = System.Drawing.Color.Black
        Me.lblPadding.Location = New System.Drawing.Point(71, 432)
        Me.lblPadding.Name = "lblPadding"
        Me.lblPadding.Size = New System.Drawing.Size(96, 20)
        Me.lblPadding.TabIndex = 84
        Me.lblPadding.Text = "จำนวนค้างรับ :"
        Me.lblPadding.Visible = False
        '
        'lblPaddingPackage
        '
        Me.lblPaddingPackage.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPaddingPackage.ForeColor = System.Drawing.Color.Black
        Me.lblPaddingPackage.Location = New System.Drawing.Point(317, 432)
        Me.lblPaddingPackage.Name = "lblPaddingPackage"
        Me.lblPaddingPackage.Size = New System.Drawing.Size(150, 20)
        Me.lblPaddingPackage.TabIndex = 86
        '
        'lblPaddingQty
        '
        Me.lblPaddingQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPaddingQty.ForeColor = System.Drawing.Color.Black
        Me.lblPaddingQty.Location = New System.Drawing.Point(193, 432)
        Me.lblPaddingQty.Name = "lblPaddingQty"
        Me.lblPaddingQty.Size = New System.Drawing.Size(118, 20)
        Me.lblPaddingQty.TabIndex = 85
        Me.lblPaddingQty.Text = "-"
        Me.lblPaddingQty.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboPrint
        '
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Location = New System.Drawing.Point(483, 397)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(208, 21)
        Me.cboPrint.TabIndex = 19
        '
        'btnTag_Weight
        '
        Me.btnTag_Weight.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnTag_Weight.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnTag_Weight.Location = New System.Drawing.Point(193, 376)
        Me.btnTag_Weight.Name = "btnTag_Weight"
        Me.btnTag_Weight.Size = New System.Drawing.Size(118, 20)
        Me.btnTag_Weight.TabIndex = 87
        Me.btnTag_Weight.Text = "Weight Scale"
        Me.btnTag_Weight.UseVisualStyleBackColor = True
        Me.btnTag_Weight.Visible = False
        '
        'lblSerialPort
        '
        Me.lblSerialPort.AutoSize = True
        Me.lblSerialPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSerialPort.Location = New System.Drawing.Point(59, 465)
        Me.lblSerialPort.Name = "lblSerialPort"
        Me.lblSerialPort.Size = New System.Drawing.Size(82, 20)
        Me.lblSerialPort.TabIndex = 88
        Me.lblSerialPort.Text = "Serial Port"
        '
        'cboSerialPort
        '
        Me.cboSerialPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSerialPort.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.cboSerialPort.FormattingEnabled = True
        Me.cboSerialPort.Location = New System.Drawing.Point(147, 461)
        Me.cboSerialPort.Name = "cboSerialPort"
        Me.cboSerialPort.Size = New System.Drawing.Size(202, 28)
        Me.cboSerialPort.TabIndex = 89
        '
        'txtWeightScale
        '
        Me.txtWeightScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtWeightScale.Location = New System.Drawing.Point(464, 462)
        Me.txtWeightScale.Name = "txtWeightScale"
        Me.txtWeightScale.ReadOnly = True
        Me.txtWeightScale.Size = New System.Drawing.Size(201, 26)
        Me.txtWeightScale.TabIndex = 91
        Me.txtWeightScale.TabStop = False
        '
        'lblWeightScale
        '
        Me.lblWeightScale.AutoSize = True
        Me.lblWeightScale.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblWeightScale.Location = New System.Drawing.Point(355, 465)
        Me.lblWeightScale.Name = "lblWeightScale"
        Me.lblWeightScale.Size = New System.Drawing.Size(103, 20)
        Me.lblWeightScale.TabIndex = 90
        Me.lblWeightScale.Text = "Weight Scale"
        '
        'frmPrintPalletSlip_v2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(724, 494)
        Me.Controls.Add(Me.lblSerialPort)
        Me.Controls.Add(Me.cboSerialPort)
        Me.Controls.Add(Me.txtWeightScale)
        Me.Controls.Add(Me.lblWeightScale)
        Me.Controls.Add(Me.btnTag_Weight)
        Me.Controls.Add(Me.cboPrint)
        Me.Controls.Add(Me.lblPaddingPackage)
        Me.Controls.Add(Me.lblPaddingQty)
        Me.Controls.Add(Me.lblPadding)
        Me.Controls.Add(Me.chkPreview)
        Me.Controls.Add(Me.lblUnitMainPackage)
        Me.Controls.Add(Me.lblUnitMainQty)
        Me.Controls.Add(Me.lblUnitMain)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.lblTag_No)
        Me.Controls.Add(Me.txtQtyPerPallet)
        Me.Controls.Add(Me.cboPackageSku)
        Me.Controls.Add(Me.lblQtyPerPallet)
        Me.Controls.Add(Me.txtTag_No)
        Me.Controls.Add(Me.gboData)
        Me.Name = "frmPrintPalletSlip_v2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "พิมพ์ PalletSlip"
        Me.gboData.ResumeLayout(False)
        Me.gboData.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gboData As System.Windows.Forms.GroupBox
    Private WithEvents lblRefNo1 As System.Windows.Forms.Label
    Private WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents dtpExpire_Date As System.Windows.Forms.DateTimePicker
    Private WithEvents lblPosting_Date As System.Windows.Forms.Label
    Friend WithEvents dtpPosting_Date As System.Windows.Forms.DateTimePicker
    Private WithEvents lblExpire_Date As System.Windows.Forms.Label
    Private WithEvents lblUnitWeight As System.Windows.Forms.Label
    Private WithEvents txtUnitWeight As System.Windows.Forms.TextBox
    Private WithEvents lblUnitVolume As System.Windows.Forms.Label
    Private WithEvents txtUnitVolume As System.Windows.Forms.TextBox
    Friend WithEvents txtPlot As System.Windows.Forms.TextBox
    Private WithEvents lblPlot As System.Windows.Forms.Label
    Friend WithEvents txtRefNo2 As System.Windows.Forms.TextBox
    Private WithEvents lblRefNo2 As System.Windows.Forms.Label
    Friend WithEvents txtRefNo1 As System.Windows.Forms.TextBox
    Private WithEvents lblOrder_Date As System.Windows.Forms.Label
    Private WithEvents lblPallet As System.Windows.Forms.Label
    Private WithEvents txtPalletQty As System.Windows.Forms.TextBox
    Friend WithEvents cboPackageSku As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Private WithEvents lblTag_No As System.Windows.Forms.Label
    Private WithEvents txtQtyPerPallet As System.Windows.Forms.TextBox
    Private WithEvents lblQtyPerPallet As System.Windows.Forms.Label
    Private WithEvents txtTag_No As System.Windows.Forms.TextBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Private WithEvents lblUnitMain As System.Windows.Forms.Label
    Private WithEvents lblUnitMainQty As System.Windows.Forms.Label
    Private WithEvents lblUnitMainPackage As System.Windows.Forms.Label
    Private WithEvents lblSku_Desc As System.Windows.Forms.Label
    Private WithEvents txtSku_Desc As System.Windows.Forms.TextBox
    Friend WithEvents txtSku As System.Windows.Forms.TextBox
    Private WithEvents lblSku As System.Windows.Forms.Label
    Friend WithEvents btnSku As System.Windows.Forms.Button
    Friend WithEvents txtDocumentType As System.Windows.Forms.TextBox
    Friend WithEvents txtOrder_Date As System.Windows.Forms.TextBox
    Friend WithEvents cboItemStatus As System.Windows.Forms.ComboBox
    Private WithEvents lblItemStatus As System.Windows.Forms.Label
    Friend WithEvents chkPreview As System.Windows.Forms.CheckBox
    Friend WithEvents pdoPalletSlip As System.Drawing.Printing.PrintDocument
    Friend WithEvents btnPO_Popup As System.Windows.Forms.Button
    Private WithEvents lblPadding As System.Windows.Forms.Label
    Private WithEvents lblPaddingPackage As System.Windows.Forms.Label
    Private WithEvents lblPaddingQty As System.Windows.Forms.Label
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents btnTag_Weight As System.Windows.Forms.Button
    Friend WithEvents lblSerialPort As System.Windows.Forms.Label
    Friend WithEvents cboSerialPort As System.Windows.Forms.ComboBox
    Friend WithEvents txtWeightScale As System.Windows.Forms.TextBox
    Friend WithEvents lblWeightScale As System.Windows.Forms.Label

End Class
