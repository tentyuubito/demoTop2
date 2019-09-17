<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_Order
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport_Order))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtWorkSheet = New System.Windows.Forms.TextBox
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.cboItemStatus = New System.Windows.Forms.ComboBox
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.cboCustomer = New System.Windows.Forms.ComboBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.grdPreviewData = New System.Windows.Forms.DataGridView
        Me.btnImport = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GroupBox3.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnBrowse)
        Me.GroupBox1.Controls.Add(Me.txtWorkSheet)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.lblFilePath)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(593, 77)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เลือก File "
        '
        'btnBrowse
        '
        Me.btnBrowse.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnBrowse.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnBrowse.Location = New System.Drawing.Point(391, 19)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(107, 38)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.Text = "เลือก..."
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtWorkSheet
        '
        Me.txtWorkSheet.Location = New System.Drawing.Point(123, 45)
        Me.txtWorkSheet.Name = "txtWorkSheet"
        Me.txtWorkSheet.Size = New System.Drawing.Size(262, 20)
        Me.txtWorkSheet.TabIndex = 1
        Me.txtWorkSheet.Text = "Sheet1"
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFilePath.Location = New System.Drawing.Point(123, 19)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(262, 20)
        Me.txtFilePath.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(30, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(86, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "ชื่อ Work Sheet :"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(29, 22)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(87, 13)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "ระบุที่อยู่ของ File :"
        '
        'cboItemStatus
        '
        Me.cboItemStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboItemStatus.FormattingEnabled = True
        Me.cboItemStatus.Location = New System.Drawing.Point(123, 73)
        Me.cboItemStatus.Name = "cboItemStatus"
        Me.cboItemStatus.Size = New System.Drawing.Size(262, 21)
        Me.cboItemStatus.TabIndex = 2
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(123, 46)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(262, 21)
        Me.cboDocumentType.TabIndex = 1
        '
        'cboCustomer
        '
        Me.cboCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomer.FormattingEnabled = True
        Me.cboCustomer.Location = New System.Drawing.Point(123, 19)
        Me.cboCustomer.Name = "cboCustomer"
        Me.cboCustomer.Size = New System.Drawing.Size(262, 21)
        Me.cboCustomer.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdPreviewData)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 201)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(593, 295)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Preview Data"
        '
        'grdPreviewData
        '
        Me.grdPreviewData.AllowUserToAddRows = False
        Me.grdPreviewData.AllowUserToDeleteRows = False
        Me.grdPreviewData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdPreviewData.Location = New System.Drawing.Point(6, 19)
        Me.grdPreviewData.Name = "grdPreviewData"
        Me.grdPreviewData.Size = New System.Drawing.Size(581, 270)
        Me.grdPreviewData.TabIndex = 0
        '
        'btnImport
        '
        Me.btnImport.Image = CType(resources.GetObject("btnImport.Image"), System.Drawing.Image)
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(385, 502)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(107, 38)
        Me.btnImport.TabIndex = 3
        Me.btnImport.Text = "เริ่มนำเข้าข้อมูล"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(498, 502)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 4
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "*.xls"
        Me.OpenFileDialog1.Filter = "Excel File|*.xls"
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cboItemStatus)
        Me.GroupBox3.Controls.Add(Me.Label5)
        Me.GroupBox3.Controls.Add(Me.Label6)
        Me.GroupBox3.Controls.Add(Me.cboDocumentType)
        Me.GroupBox3.Controls.Add(Me.Label7)
        Me.GroupBox3.Controls.Add(Me.cboCustomer)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 95)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(593, 100)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "กำหนดค่าเริ่มต้น"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(46, 76)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(70, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "สถานะสินค้า :"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(78, 22)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 13)
        Me.Label6.TabIndex = 3
        Me.Label6.Text = "ลูกค้า :"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(31, 49)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(85, 13)
        Me.Label7.TabIndex = 5
        Me.Label7.Text = "ประเภทเอกสาร :"
        '
        'frmImport_Order
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(617, 550)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnImport)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImport_Order"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "นำเข้าข้อมูลรับสินค้า"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
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
    Friend WithEvents cboCustomer As System.Windows.Forms.ComboBox
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents cboItemStatus As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
End Class
