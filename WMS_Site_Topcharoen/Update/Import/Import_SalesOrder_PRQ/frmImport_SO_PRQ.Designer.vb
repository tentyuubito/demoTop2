<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_SO_PRQ
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport_SO_PRQ))
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtShipping_Location_Name = New System.Windows.Forms.TextBox
        Me.lblReservedLocation = New System.Windows.Forms.Label
        Me.btnCustomer_Shipping_Location = New System.Windows.Forms.Button
        Me.txtShipping_Location_ID = New System.Windows.Forms.TextBox
        Me.lblConsignee = New System.Windows.Forms.Label
        Me.btnConsignee = New System.Windows.Forms.Button
        Me.txtConsignee_Name = New System.Windows.Forms.TextBox
        Me.txtConsignee_Id = New System.Windows.Forms.TextBox
        Me.cboWorkSheet = New System.Windows.Forms.ComboBox
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.btnCustomer = New System.Windows.Forms.Button
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.s = New System.Windows.Forms.Button
        Me.txtWorkSheet = New System.Windows.Forms.TextBox
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdPreviewData = New System.Windows.Forms.DataGridView
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.BntLoaddata = New System.Windows.Forms.Button
        Me.btnValidate = New System.Windows.Forms.Button
        Me.chkStock = New System.Windows.Forms.CheckBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(705, 559)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "   Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Enabled = False
        Me.btnImport.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(256, 556)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(128, 38)
        Me.btnImport.TabIndex = 21
        Me.btnImport.Text = "       Process  Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(554, 556)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "* Import Folder "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(554, 577)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 13)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "* Backup Import Folder"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtShipping_Location_Name)
        Me.GroupBox1.Controls.Add(Me.lblReservedLocation)
        Me.GroupBox1.Controls.Add(Me.btnCustomer_Shipping_Location)
        Me.GroupBox1.Controls.Add(Me.txtShipping_Location_ID)
        Me.GroupBox1.Controls.Add(Me.lblConsignee)
        Me.GroupBox1.Controls.Add(Me.btnConsignee)
        Me.GroupBox1.Controls.Add(Me.txtConsignee_Name)
        Me.GroupBox1.Controls.Add(Me.txtConsignee_Id)
        Me.GroupBox1.Controls.Add(Me.cboWorkSheet)
        Me.GroupBox1.Controls.Add(Me.cboDocumentType)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.lblCustomer)
        Me.GroupBox1.Controls.Add(Me.btnCustomer)
        Me.GroupBox1.Controls.Add(Me.txtCustomer_Id)
        Me.GroupBox1.Controls.Add(Me.s)
        Me.GroupBox1.Controls.Add(Me.txtWorkSheet)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblFilePath)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(808, 146)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select File "
        '
        'txtShipping_Location_Name
        '
        Me.txtShipping_Location_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtShipping_Location_Name.Location = New System.Drawing.Point(310, 98)
        Me.txtShipping_Location_Name.Name = "txtShipping_Location_Name"
        Me.txtShipping_Location_Name.ReadOnly = True
        Me.txtShipping_Location_Name.Size = New System.Drawing.Size(378, 20)
        Me.txtShipping_Location_Name.TabIndex = 393
        '
        'lblReservedLocation
        '
        Me.lblReservedLocation.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblReservedLocation.Location = New System.Drawing.Point(8, 103)
        Me.lblReservedLocation.Name = "lblReservedLocation"
        Me.lblReservedLocation.Size = New System.Drawing.Size(87, 13)
        Me.lblReservedLocation.TabIndex = 390
        Me.lblReservedLocation.Text = "สถานที่จัดส่ง"
        Me.lblReservedLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCustomer_Shipping_Location
        '
        Me.btnCustomer_Shipping_Location.Location = New System.Drawing.Point(276, 98)
        Me.btnCustomer_Shipping_Location.Name = "btnCustomer_Shipping_Location"
        Me.btnCustomer_Shipping_Location.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer_Shipping_Location.TabIndex = 392
        Me.btnCustomer_Shipping_Location.Text = "..."
        Me.btnCustomer_Shipping_Location.UseVisualStyleBackColor = True
        '
        'txtShipping_Location_ID
        '
        Me.txtShipping_Location_ID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtShipping_Location_ID.Location = New System.Drawing.Point(101, 98)
        Me.txtShipping_Location_ID.Name = "txtShipping_Location_ID"
        Me.txtShipping_Location_ID.ReadOnly = True
        Me.txtShipping_Location_ID.Size = New System.Drawing.Size(173, 20)
        Me.txtShipping_Location_ID.TabIndex = 391
        '
        'lblConsignee
        '
        Me.lblConsignee.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblConsignee.Location = New System.Drawing.Point(12, 76)
        Me.lblConsignee.Name = "lblConsignee"
        Me.lblConsignee.Size = New System.Drawing.Size(77, 13)
        Me.lblConsignee.TabIndex = 387
        Me.lblConsignee.Text = "ผู้รับสินค้า"
        Me.lblConsignee.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnConsignee
        '
        Me.btnConsignee.Location = New System.Drawing.Point(276, 70)
        Me.btnConsignee.Name = "btnConsignee"
        Me.btnConsignee.Size = New System.Drawing.Size(24, 23)
        Me.btnConsignee.TabIndex = 388
        Me.btnConsignee.TabStop = False
        Me.btnConsignee.Text = "..."
        Me.btnConsignee.UseVisualStyleBackColor = True
        '
        'txtConsignee_Name
        '
        Me.txtConsignee_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtConsignee_Name.Location = New System.Drawing.Point(310, 73)
        Me.txtConsignee_Name.Name = "txtConsignee_Name"
        Me.txtConsignee_Name.ReadOnly = True
        Me.txtConsignee_Name.Size = New System.Drawing.Size(378, 20)
        Me.txtConsignee_Name.TabIndex = 389
        Me.txtConsignee_Name.TabStop = False
        '
        'txtConsignee_Id
        '
        Me.txtConsignee_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtConsignee_Id.Location = New System.Drawing.Point(101, 72)
        Me.txtConsignee_Id.Name = "txtConsignee_Id"
        Me.txtConsignee_Id.Size = New System.Drawing.Size(173, 20)
        Me.txtConsignee_Id.TabIndex = 386
        '
        'cboWorkSheet
        '
        Me.cboWorkSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkSheet.FormattingEnabled = True
        Me.cboWorkSheet.Location = New System.Drawing.Point(377, 44)
        Me.cboWorkSheet.Name = "cboWorkSheet"
        Me.cboWorkSheet.Size = New System.Drawing.Size(310, 21)
        Me.cboWorkSheet.TabIndex = 289
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(101, 45)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(173, 21)
        Me.cboDocumentType.TabIndex = 287
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(6, 48)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(96, 13)
        Me.Label7.TabIndex = 288
        Me.Label7.Text = "Document Type"
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(6, 21)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(83, 13)
        Me.lblCustomer.TabIndex = 286
        Me.lblCustomer.Text = "Customer"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnCustomer
        '
        Me.btnCustomer.Location = New System.Drawing.Point(276, 16)
        Me.btnCustomer.Name = "btnCustomer"
        Me.btnCustomer.Size = New System.Drawing.Size(24, 23)
        Me.btnCustomer.TabIndex = 285
        Me.btnCustomer.Text = "..."
        Me.btnCustomer.UseVisualStyleBackColor = True
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(101, 18)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(173, 20)
        Me.txtCustomer_Id.TabIndex = 284
        Me.txtCustomer_Id.TabStop = False
        '
        's
        '
        Me.s.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.s.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.s.Location = New System.Drawing.Point(695, 16)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(107, 71)
        Me.s.TabIndex = 0
        Me.s.Text = "    Browse"
        Me.s.UseVisualStyleBackColor = True
        '
        'txtWorkSheet
        '
        Me.txtWorkSheet.Location = New System.Drawing.Point(567, 45)
        Me.txtWorkSheet.Name = "txtWorkSheet"
        Me.txtWorkSheet.Size = New System.Drawing.Size(116, 20)
        Me.txtWorkSheet.TabIndex = 1
        Me.txtWorkSheet.Visible = False
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFilePath.Location = New System.Drawing.Point(377, 18)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(311, 20)
        Me.txtFilePath.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(307, 47)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Work Sheet :"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(323, 21)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(54, 13)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "File Path :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdPreviewData)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 164)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(808, 385)
        Me.GroupBox2.TabIndex = 26
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
        Me.grdPreviewData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.grdPreviewData.Size = New System.Drawing.Size(802, 366)
        Me.grdPreviewData.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "*.xls"
        Me.OpenFileDialog1.Filter = "Excel File|*.xls"
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'BntLoaddata
        '
        Me.BntLoaddata.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ1
        Me.BntLoaddata.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BntLoaddata.Location = New System.Drawing.Point(12, 556)
        Me.BntLoaddata.Name = "BntLoaddata"
        Me.BntLoaddata.Size = New System.Drawing.Size(116, 38)
        Me.BntLoaddata.TabIndex = 372
        Me.BntLoaddata.Text = "    Load Data"
        Me.BntLoaddata.UseVisualStyleBackColor = True
        '
        'btnValidate
        '
        Me.btnValidate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnValidate.Location = New System.Drawing.Point(134, 556)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(116, 38)
        Me.btnValidate.TabIndex = 374
        Me.btnValidate.Text = "    Validate Data"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'chkStock
        '
        Me.chkStock.AutoSize = True
        Me.chkStock.Checked = True
        Me.chkStock.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.chkStock.Location = New System.Drawing.Point(390, 559)
        Me.chkStock.Name = "chkStock"
        Me.chkStock.Size = New System.Drawing.Size(149, 17)
        Me.chkStock.TabIndex = 375
        Me.chkStock.Text = "ตรวจสอบสินค้าคงเหลือ"
        Me.chkStock.UseVisualStyleBackColor = True
        Me.chkStock.Visible = False
        '
        'frmImport_SO_PRQ
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 603)
        Me.Controls.Add(Me.chkStock)
        Me.Controls.Add(Me.btnValidate)
        Me.Controls.Add(Me.BntLoaddata)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImport)
        Me.KeyPreview = True
        Me.Name = "frmImport_SO_PRQ"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Shipment Data [EXCEL]"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents s As System.Windows.Forms.Button
    Friend WithEvents txtWorkSheet As System.Windows.Forms.TextBox
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdPreviewData As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents btnCustomer As System.Windows.Forms.Button
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboWorkSheet As System.Windows.Forms.ComboBox
    Friend WithEvents BntLoaddata As System.Windows.Forms.Button
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents lblConsignee As System.Windows.Forms.Label
    Friend WithEvents btnConsignee As System.Windows.Forms.Button
    Friend WithEvents txtConsignee_Name As System.Windows.Forms.TextBox
    Friend WithEvents txtConsignee_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtShipping_Location_Name As System.Windows.Forms.TextBox
    Friend WithEvents lblReservedLocation As System.Windows.Forms.Label
    Friend WithEvents btnCustomer_Shipping_Location As System.Windows.Forms.Button
    Friend WithEvents txtShipping_Location_ID As System.Windows.Forms.TextBox
    Friend WithEvents chkStock As System.Windows.Forms.CheckBox
End Class
