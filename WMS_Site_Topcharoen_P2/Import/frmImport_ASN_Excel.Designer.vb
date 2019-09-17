<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_ASN_Excel
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport_ASN_Excel))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtCustomerShippingLocation_Name = New System.Windows.Forms.TextBox
        Me.btnSearchCustomerShippingLocation = New System.Windows.Forms.Button
        Me.lblChstomerShippingLocation = New System.Windows.Forms.Label
        Me.txtCustomerShippingLocation_ID = New System.Windows.Forms.TextBox
        Me.rd_None = New System.Windows.Forms.RadioButton
        Me.rd_Customer = New System.Windows.Forms.RadioButton
        Me.rd_Timco = New System.Windows.Forms.RadioButton
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboItemStatus = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.cboWorkSheet = New System.Windows.Forms.ComboBox
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.s = New System.Windows.Forms.Button
        Me.txtWorkSheet = New System.Windows.Forms.TextBox
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdPreviewData = New System.Windows.Forms.DataGridView
        Me.btnImport = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.BntLoaddata = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtCustomerShippingLocation_Name)
        Me.GroupBox1.Controls.Add(Me.btnSearchCustomerShippingLocation)
        Me.GroupBox1.Controls.Add(Me.lblChstomerShippingLocation)
        Me.GroupBox1.Controls.Add(Me.txtCustomerShippingLocation_ID)
        Me.GroupBox1.Controls.Add(Me.rd_None)
        Me.GroupBox1.Controls.Add(Me.rd_Customer)
        Me.GroupBox1.Controls.Add(Me.rd_Timco)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.cboItemStatus)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.cboWorkSheet)
        Me.GroupBox1.Controls.Add(Me.cboDocumentType)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblCustomer)
        Me.GroupBox1.Controls.Add(Me.btnCustomer)
        Me.GroupBox1.Controls.Add(Me.txtCustomer_Id)
        Me.GroupBox1.Controls.Add(Me.s)
        Me.GroupBox1.Controls.Add(Me.txtWorkSheet)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblFilePath)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 9)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(808, 124)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select File "
        '
        'txtCustomerShippingLocation_Name
        '
        Me.txtCustomerShippingLocation_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerShippingLocation_Name.Location = New System.Drawing.Point(613, 95)
        Me.txtCustomerShippingLocation_Name.Name = "txtCustomerShippingLocation_Name"
        Me.txtCustomerShippingLocation_Name.ReadOnly = True
        Me.txtCustomerShippingLocation_Name.Size = New System.Drawing.Size(186, 20)
        Me.txtCustomerShippingLocation_Name.TabIndex = 382
        Me.txtCustomerShippingLocation_Name.TabStop = False
        Me.txtCustomerShippingLocation_Name.Visible = False
        '
        'btnSearchCustomerShippingLocation
        '
        Me.btnSearchCustomerShippingLocation.Location = New System.Drawing.Point(583, 93)
        Me.btnSearchCustomerShippingLocation.Name = "btnSearchCustomerShippingLocation"
        Me.btnSearchCustomerShippingLocation.Size = New System.Drawing.Size(24, 23)
        Me.btnSearchCustomerShippingLocation.TabIndex = 381
        Me.btnSearchCustomerShippingLocation.TabStop = False
        Me.btnSearchCustomerShippingLocation.Text = "..."
        Me.btnSearchCustomerShippingLocation.UseVisualStyleBackColor = True
        Me.btnSearchCustomerShippingLocation.Visible = False
        '
        'lblChstomerShippingLocation
        '
        Me.lblChstomerShippingLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblChstomerShippingLocation.Location = New System.Drawing.Point(410, 98)
        Me.lblChstomerShippingLocation.Name = "lblChstomerShippingLocation"
        Me.lblChstomerShippingLocation.Size = New System.Drawing.Size(59, 13)
        Me.lblChstomerShippingLocation.TabIndex = 383
        Me.lblChstomerShippingLocation.Text = "Source"
        Me.lblChstomerShippingLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblChstomerShippingLocation.Visible = False
        '
        'txtCustomerShippingLocation_ID
        '
        Me.txtCustomerShippingLocation_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerShippingLocation_ID.Location = New System.Drawing.Point(475, 95)
        Me.txtCustomerShippingLocation_ID.Name = "txtCustomerShippingLocation_ID"
        Me.txtCustomerShippingLocation_ID.Size = New System.Drawing.Size(106, 20)
        Me.txtCustomerShippingLocation_ID.TabIndex = 380
        Me.txtCustomerShippingLocation_ID.Visible = False
        '
        'rd_None
        '
        Me.rd_None.AutoSize = True
        Me.rd_None.Location = New System.Drawing.Point(310, 100)
        Me.rd_None.Name = "rd_None"
        Me.rd_None.Size = New System.Drawing.Size(78, 17)
        Me.rd_None.TabIndex = 376
        Me.rd_None.TabStop = True
        Me.rd_None.Text = "SUB Truck"
        Me.rd_None.UseVisualStyleBackColor = True
        '
        'rd_Customer
        '
        Me.rd_Customer.AutoSize = True
        Me.rd_Customer.Location = New System.Drawing.Point(204, 100)
        Me.rd_Customer.Name = "rd_Customer"
        Me.rd_Customer.Size = New System.Drawing.Size(88, 17)
        Me.rd_Customer.TabIndex = 378
        Me.rd_Customer.TabStop = True
        Me.rd_Customer.Text = "CUST. Truck"
        Me.rd_Customer.UseVisualStyleBackColor = True
        '
        'rd_Timco
        '
        Me.rd_Timco.AutoSize = True
        Me.rd_Timco.Location = New System.Drawing.Point(99, 100)
        Me.rd_Timco.Name = "rd_Timco"
        Me.rd_Timco.Size = New System.Drawing.Size(90, 17)
        Me.rd_Timco.TabIndex = 377
        Me.rd_Timco.TabStop = True
        Me.rd_Timco.Text = "TIMCO Truck"
        Me.rd_Timco.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.Location = New System.Drawing.Point(14, 102)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(80, 13)
        Me.Label4.TabIndex = 379
        Me.Label4.Text = "Truck Owner"
        '
        'cboItemStatus
        '
        Me.cboItemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemStatus.FormattingEnabled = True
        Me.cboItemStatus.Location = New System.Drawing.Point(98, 71)
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.Size = New System.Drawing.Size(194, 21)
        Me.cboItemStatus.TabIndex = 374
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(17, 74)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(77, 13)
        Me.Label5.TabIndex = 375
        Me.Label5.Text = "Product Status"
        '
        'cboWorkSheet
        '
        Me.cboWorkSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkSheet.FormattingEnabled = True
        Me.cboWorkSheet.Location = New System.Drawing.Point(409, 44)
        Me.cboWorkSheet.Name = "cboWorkSheet"
        Me.cboWorkSheet.Size = New System.Drawing.Size(277, 21)
        Me.cboWorkSheet.TabIndex = 290
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(98, 45)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(194, 21)
        Me.cboDocumentType.TabIndex = 284
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(11, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(83, 13)
        Me.Label7.TabIndex = 285
        Me.Label7.Text = "Document Type"
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(17, 22)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(77, 13)
        Me.lblCustomer.TabIndex = 283
        Me.lblCustomer.Text = "Customer"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCustomer
        '
        Me.btnCustomer.Location = New System.Drawing.Point(298, 17)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer.TabIndex = 282
        Me.btnCustomer.Text = "..."
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(98, 19)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(194, 20)
        Me.txtCustomer_Id.TabIndex = 281
        Me.txtCustomer_Id.TabStop = False
        '
        's
        '
        Me.s.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.s.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.s.Location = New System.Drawing.Point(692, 20)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(107, 72)
        Me.s.TabIndex = 0
        Me.s.Text = "    Browse"
        Me.s.UseVisualStyleBackColor = True
        '
        'txtWorkSheet
        '
        Me.txtWorkSheet.Location = New System.Drawing.Point(409, 45)
        Me.txtWorkSheet.Name = "txtWorkSheet"
        Me.txtWorkSheet.Size = New System.Drawing.Size(276, 20)
        Me.txtWorkSheet.TabIndex = 1
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFilePath.Location = New System.Drawing.Point(409, 19)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(277, 20)
        Me.txtFilePath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(335, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(70, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Work Sheet :"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(351, 22)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(54, 13)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "File Path :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdPreviewData)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 138)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(808, 417)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Preview Data"
        '
        'grdPreviewData
        '
        Me.grdPreviewData.AllowUserToAddRows = False
        Me.grdPreviewData.AllowUserToDeleteRows = False
        Me.grdPreviewData.AllowUserToResizeRows = False
        Me.grdPreviewData.BackgroundColor = System.Drawing.Color.White
        Me.grdPreviewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPreviewData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdPreviewData.Location = New System.Drawing.Point(3, 16)
        Me.grdPreviewData.Name = "grdPreviewData"
        Me.grdPreviewData.ReadOnly = True
        Me.grdPreviewData.Size = New System.Drawing.Size(802, 398)
        Me.grdPreviewData.TabIndex = 0
        '
        'btnImport
        '
        Me.btnImport.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ดึงข้อมูล
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(137, 564)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(139, 38)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "     Process  Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(713, 563)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "   Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "*.xls"
        Me.OpenFileDialog1.Filter = "Excel File|*.xls"
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'BntLoaddata
        '
        Me.BntLoaddata.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ1
        Me.BntLoaddata.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BntLoaddata.Location = New System.Drawing.Point(15, 563)
        Me.BntLoaddata.Name = "BntLoaddata"
        Me.BntLoaddata.Size = New System.Drawing.Size(116, 38)
        Me.BntLoaddata.TabIndex = 373
        Me.BntLoaddata.Text = "    Load Data"
        Me.BntLoaddata.UseVisualStyleBackColor = True
        '
        'frmImport_ASN_Excel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(827, 613)
        Me.Controls.Add(Me.BntLoaddata)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImport_ASN_Excel"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Receiving Data [EXCEL]"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grdPreviewData As System.Windows.Forms.DataGridView
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtWorkSheet As System.Windows.Forms.TextBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents s As System.Windows.Forms.Button
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboWorkSheet As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemStatus As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents BntLoaddata As System.Windows.Forms.Button
    Friend WithEvents rd_None As System.Windows.Forms.RadioButton
    Friend WithEvents rd_Customer As System.Windows.Forms.RadioButton
    Friend WithEvents rd_Timco As System.Windows.Forms.RadioButton
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtCustomerShippingLocation_Name As System.Windows.Forms.TextBox
    Friend WithEvents btnSearchCustomerShippingLocation As System.Windows.Forms.Button
    Friend WithEvents lblChstomerShippingLocation As System.Windows.Forms.Label
    Friend WithEvents txtCustomerShippingLocation_ID As System.Windows.Forms.TextBox
End Class
