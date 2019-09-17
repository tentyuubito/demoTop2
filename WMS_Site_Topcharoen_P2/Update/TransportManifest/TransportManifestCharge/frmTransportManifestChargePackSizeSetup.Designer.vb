<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransportManifestChargePackSizeSetup
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grbHeader = New System.Windows.Forms.GroupBox
        Me.lblReservedLocation = New System.Windows.Forms.Label
        Me.btnCustomer_Shipping_Location = New System.Windows.Forms.Button
        Me.txtShipping_Location_ID = New System.Windows.Forms.TextBox
        Me.txtShipping_Location_Name = New System.Windows.Forms.TextBox
        Me.txtCarrier_Id = New System.Windows.Forms.TextBox
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.txtCarrier_Name = New System.Windows.Forms.TextBox
        Me.lblCarrier = New System.Windows.Forms.Label
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.lblTransportJobType = New System.Windows.Forms.Label
        Me.cboTransportJobType = New System.Windows.Forms.ComboBox
        Me.grbTransportPaymentPerDrop = New System.Windows.Forms.GroupBox
        Me.grdTransportPaymentPerDrop = New System.Windows.Forms.DataGridView
        Me.col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Rate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PackSize_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
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
        Me.grbHeader.SuspendLayout()
        Me.grbTransportPaymentPerDrop.SuspendLayout()
        CType(Me.grdTransportPaymentPerDrop, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbHeader
        '
        Me.grbHeader.Controls.Add(Me.lblReservedLocation)
        Me.grbHeader.Controls.Add(Me.btnCustomer_Shipping_Location)
        Me.grbHeader.Controls.Add(Me.txtShipping_Location_ID)
        Me.grbHeader.Controls.Add(Me.txtShipping_Location_Name)
        Me.grbHeader.Controls.Add(Me.txtCarrier_Id)
        Me.grbHeader.Controls.Add(Me.lblCustomer)
        Me.grbHeader.Controls.Add(Me.txtCarrier_Name)
        Me.grbHeader.Controls.Add(Me.lblCarrier)
        Me.grbHeader.Controls.Add(Me.txtCustomer_Id)
        Me.grbHeader.Controls.Add(Me.txtCustomer_Name)
        Me.grbHeader.Controls.Add(Me.lblTransportJobType)
        Me.grbHeader.Controls.Add(Me.cboTransportJobType)
        Me.grbHeader.Location = New System.Drawing.Point(12, 12)
        Me.grbHeader.Name = "grbHeader"
        Me.grbHeader.Size = New System.Drawing.Size(733, 120)
        Me.grbHeader.TabIndex = 12
        Me.grbHeader.TabStop = False
        Me.grbHeader.Text = "ข้อกำหนดการคิดเงิน"
        '
        'lblReservedLocation
        '
        Me.lblReservedLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblReservedLocation.Location = New System.Drawing.Point(59, 70)
        Me.lblReservedLocation.Name = "lblReservedLocation"
        Me.lblReservedLocation.Size = New System.Drawing.Size(88, 13)
        Me.lblReservedLocation.TabIndex = 313
        Me.lblReservedLocation.Text = "สถานที่ส่งสินค้า"
        Me.lblReservedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCustomer_Shipping_Location
        '
        Me.btnCustomer_Shipping_Location.Location = New System.Drawing.Point(315, 67)
        Me.btnCustomer_Shipping_Location.Name = "btnCustomer_Shipping_Location"
        Me.btnCustomer_Shipping_Location.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer_Shipping_Location.TabIndex = 315
        Me.btnCustomer_Shipping_Location.Text = "..."
        Me.btnCustomer_Shipping_Location.UseVisualStyleBackColor = True
        '
        'txtShipping_Location_ID
        '
        Me.txtShipping_Location_ID.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtShipping_Location_ID.Location = New System.Drawing.Point(153, 69)
        Me.txtShipping_Location_ID.Name = "txtShipping_Location_ID"
        Me.txtShipping_Location_ID.ReadOnly = True
        Me.txtShipping_Location_ID.Size = New System.Drawing.Size(157, 20)
        Me.txtShipping_Location_ID.TabIndex = 314
        '
        'txtShipping_Location_Name
        '
        Me.txtShipping_Location_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtShipping_Location_Name.Location = New System.Drawing.Point(345, 70)
        Me.txtShipping_Location_Name.Multiline = True
        Me.txtShipping_Location_Name.Name = "txtShipping_Location_Name"
        Me.txtShipping_Location_Name.Size = New System.Drawing.Size(265, 19)
        Me.txtShipping_Location_Name.TabIndex = 316
        '
        'txtCarrier_Id
        '
        Me.txtCarrier_Id.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCarrier_Id.Location = New System.Drawing.Point(153, 44)
        Me.txtCarrier_Id.Name = "txtCarrier_Id"
        Me.txtCarrier_Id.ReadOnly = True
        Me.txtCarrier_Id.Size = New System.Drawing.Size(186, 20)
        Me.txtCarrier_Id.TabIndex = 19
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(42, 19)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(105, 23)
        Me.lblCustomer.TabIndex = 18
        Me.lblCustomer.Text = "ลูกค้า/เจ้าของงาน"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCarrier_Name
        '
        Me.txtCarrier_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCarrier_Name.Location = New System.Drawing.Point(345, 45)
        Me.txtCarrier_Name.Name = "txtCarrier_Name"
        Me.txtCarrier_Name.ReadOnly = True
        Me.txtCarrier_Name.Size = New System.Drawing.Size(265, 20)
        Me.txtCarrier_Name.TabIndex = 20
        '
        'lblCarrier
        '
        Me.lblCarrier.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCarrier.Location = New System.Drawing.Point(42, 41)
        Me.lblCarrier.Name = "lblCarrier"
        Me.lblCarrier.Size = New System.Drawing.Size(105, 23)
        Me.lblCarrier.TabIndex = 17
        Me.lblCarrier.Text = "ชื่อบริษัทขนส่ง"
        Me.lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Id.Location = New System.Drawing.Point(153, 19)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(186, 20)
        Me.txtCustomer_Id.TabIndex = 1
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtCustomer_Name.Location = New System.Drawing.Point(345, 20)
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(265, 20)
        Me.txtCustomer_Name.TabIndex = 3
        '
        'lblTransportJobType
        '
        Me.lblTransportJobType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTransportJobType.Location = New System.Drawing.Point(24, 91)
        Me.lblTransportJobType.Name = "lblTransportJobType"
        Me.lblTransportJobType.Size = New System.Drawing.Size(118, 23)
        Me.lblTransportJobType.TabIndex = 4
        Me.lblTransportJobType.Text = "ประเภทงานขนส่ง"
        Me.lblTransportJobType.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTransportJobType
        '
        Me.cboTransportJobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransportJobType.Enabled = False
        Me.cboTransportJobType.FormattingEnabled = True
        Me.cboTransportJobType.Location = New System.Drawing.Point(153, 92)
        Me.cboTransportJobType.Name = "cboTransportJobType"
        Me.cboTransportJobType.Size = New System.Drawing.Size(187, 21)
        Me.cboTransportJobType.TabIndex = 5
        '
        'grbTransportPaymentPerDrop
        '
        Me.grbTransportPaymentPerDrop.Controls.Add(Me.grdTransportPaymentPerDrop)
        Me.grbTransportPaymentPerDrop.Location = New System.Drawing.Point(12, 138)
        Me.grbTransportPaymentPerDrop.Name = "grbTransportPaymentPerDrop"
        Me.grbTransportPaymentPerDrop.Size = New System.Drawing.Size(735, 415)
        Me.grbTransportPaymentPerDrop.TabIndex = 3
        Me.grbTransportPaymentPerDrop.TabStop = False
        Me.grbTransportPaymentPerDrop.Text = "รายละเอียดการคิดเงินต่อ กล่อง"
        '
        'grdTransportPaymentPerDrop
        '
        Me.grdTransportPaymentPerDrop.AllowUserToAddRows = False
        Me.grdTransportPaymentPerDrop.AllowUserToDeleteRows = False
        Me.grdTransportPaymentPerDrop.AllowUserToResizeRows = False
        Me.grdTransportPaymentPerDrop.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTransportPaymentPerDrop.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.grdTransportPaymentPerDrop.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdTransportPaymentPerDrop.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Description, Me.col_Rate, Me.PackSize_Index})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdTransportPaymentPerDrop.DefaultCellStyle = DataGridViewCellStyle2
        Me.grdTransportPaymentPerDrop.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdTransportPaymentPerDrop.Location = New System.Drawing.Point(3, 16)
        Me.grdTransportPaymentPerDrop.Name = "grdTransportPaymentPerDrop"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdTransportPaymentPerDrop.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.grdTransportPaymentPerDrop.RowHeadersVisible = False
        Me.grdTransportPaymentPerDrop.Size = New System.Drawing.Size(729, 396)
        Me.grdTransportPaymentPerDrop.TabIndex = 289
        '
        'col_Description
        '
        Me.col_Description.DataPropertyName = "Description"
        Me.col_Description.HeaderText = "ขนาดกล่อง"
        Me.col_Description.Name = "col_Description"
        Me.col_Description.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.col_Description.Width = 500
        '
        'col_Rate
        '
        Me.col_Rate.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.col_Rate.DataPropertyName = "Rate"
        Me.col_Rate.HeaderText = "อัตรา/กล่อง"
        Me.col_Rate.Name = "col_Rate"
        Me.col_Rate.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'PackSize_Index
        '
        Me.PackSize_Index.DataPropertyName = "PackSize_Index"
        Me.PackSize_Index.HeaderText = "PackSize_Index"
        Me.PackSize_Index.Name = "PackSize_Index"
        Me.PackSize_Index.Visible = False
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(18, 559)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(627, 559)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 16
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ชื่อลูกค้า"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn1.Width = 150
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "TransportJobType_desc"
        Me.DataGridViewTextBoxColumn2.HeaderText = "ประเภทงานขนส่ง"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn2.Width = 150
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TransportRegion_desc"
        Me.DataGridViewTextBoxColumn3.HeaderText = "เขตพื้นที่บริการ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Currency_desc"
        Me.DataGridViewTextBoxColumn4.HeaderText = "สกุลเงิน"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn4.Width = 122
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn5.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "TransportJobType_Index"
        Me.DataGridViewTextBoxColumn6.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.ReadOnly = True
        Me.DataGridViewTextBoxColumn6.Visible = False
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "TransportRegion_Index"
        Me.DataGridViewTextBoxColumn7.HeaderText = "จำนวน Drop (เริ่ม)"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        Me.DataGridViewTextBoxColumn7.ReadOnly = True
        Me.DataGridViewTextBoxColumn7.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn7.Visible = False
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Customer_Index"
        Me.DataGridViewTextBoxColumn8.HeaderText = "จำนวน Drop (ถึง)"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.ReadOnly = True
        Me.DataGridViewTextBoxColumn8.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn8.Visible = False
        Me.DataGridViewTextBoxColumn8.Width = 120
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn9.HeaderText = "อัตรา Drop ละ"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.ReadOnly = True
        Me.DataGridViewTextBoxColumn9.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn9.Visible = False
        Me.DataGridViewTextBoxColumn9.Width = 130
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Currency_Index"
        Me.DataGridViewTextBoxColumn10.HeaderText = "ปริมาณเกิน"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.ReadOnly = True
        Me.DataGridViewTextBoxColumn10.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn10.Visible = False
        Me.DataGridViewTextBoxColumn10.Width = 130
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn11.HeaderText = "หน่วยส่วนเกิน"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.ReadOnly = True
        Me.DataGridViewTextBoxColumn11.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn11.Visible = False
        Me.DataGridViewTextBoxColumn11.Width = 120
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "QtyDropStart"
        Me.DataGridViewTextBoxColumn12.HeaderText = "อัตราส่วนเกิน"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.ReadOnly = True
        Me.DataGridViewTextBoxColumn12.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "QtyDropEnd"
        Me.DataGridViewTextBoxColumn13.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.ReadOnly = True
        Me.DataGridViewTextBoxColumn13.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn13.Visible = False
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "RateTransportPerDrop"
        Me.DataGridViewTextBoxColumn14.HeaderText = "หน่วยส่วนเกิน"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.ReadOnly = True
        Me.DataGridViewTextBoxColumn14.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn14.Width = 130
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "OverflowPerDrop"
        Me.DataGridViewTextBoxColumn15.HeaderText = "อัตราส่วนเกิน"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.ReadOnly = True
        Me.DataGridViewTextBoxColumn15.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "OverflowPerDropUnit_desc"
        Me.DataGridViewTextBoxColumn16.HeaderText = "Column1"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable
        Me.DataGridViewTextBoxColumn16.Visible = False
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "OverflowPerDropUnit"
        Me.DataGridViewTextBoxColumn17.HeaderText = "OverflowPerDropUnit"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Visible = False
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "OverflowRate"
        Me.DataGridViewTextBoxColumn18.HeaderText = "อัตราส่วนเกิน"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        '
        'frmTransportManifestChargePackSizeSetup
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(759, 611)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.grbTransportPaymentPerDrop)
        Me.Controls.Add(Me.grbHeader)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTransportManifestChargePackSizeSetup"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "กำหนดค่าบริการงานขนส่ง/กล่อง"
        Me.grbHeader.ResumeLayout(False)
        Me.grbHeader.PerformLayout()
        Me.grbTransportPaymentPerDrop.ResumeLayout(False)
        CType(Me.grdTransportPaymentPerDrop, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbHeader As System.Windows.Forms.GroupBox
    Friend WithEvents lblTransportJobType As System.Windows.Forms.Label
    Friend WithEvents cboTransportJobType As System.Windows.Forms.ComboBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grbTransportPaymentPerDrop As System.Windows.Forms.GroupBox
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grdTransportPaymentPerDrop As System.Windows.Forms.DataGridView
    Friend WithEvents lblCarrier As System.Windows.Forms.Label
    Friend WithEvents txtCarrier_Id As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents txtCarrier_Name As System.Windows.Forms.TextBox
    Friend WithEvents col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Rate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents PackSize_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblReservedLocation As System.Windows.Forms.Label
    Friend WithEvents btnCustomer_Shipping_Location As System.Windows.Forms.Button
    Friend WithEvents txtShipping_Location_ID As System.Windows.Forms.TextBox
    Friend WithEvents txtShipping_Location_Name As System.Windows.Forms.TextBox
End Class
