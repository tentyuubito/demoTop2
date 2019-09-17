<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_PO_TCR
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport_PO_TCR))
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboWorkSheet = New System.Windows.Forms.ComboBox
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.Label7 = New System.Windows.Forms.Label
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
        Me.btnExit.Location = New System.Drawing.Point(940, 688)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(143, 47)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "   Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Enabled = False
        Me.btnImport.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(341, 684)
        Me.btnImport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(171, 47)
        Me.btnImport.TabIndex = 21
        Me.btnImport.Text = "       Process  Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.ForeColor = System.Drawing.Color.Blue
        Me.Label1.Location = New System.Drawing.Point(739, 684)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(104, 17)
        Me.Label1.TabIndex = 22
        Me.Label1.Text = "* Import Folder "
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.ForeColor = System.Drawing.Color.Blue
        Me.Label2.Location = New System.Drawing.Point(739, 710)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(151, 17)
        Me.Label2.TabIndex = 22
        Me.Label2.Text = "* Backup Import Folder"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.cboWorkSheet)
        Me.GroupBox1.Controls.Add(Me.cboDocumentType)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.s)
        Me.GroupBox1.Controls.Add(Me.txtWorkSheet)
        Me.GroupBox1.Controls.Add(Me.txtFilePath)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.lblFilePath)
        Me.GroupBox1.Location = New System.Drawing.Point(9, 15)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(1077, 120)
        Me.GroupBox1.TabIndex = 25
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Select File "
        '
        'cboWorkSheet
        '
        Me.cboWorkSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkSheet.FormattingEnabled = True
        Me.cboWorkSheet.Location = New System.Drawing.Point(506, 60)
        Me.cboWorkSheet.Margin = New System.Windows.Forms.Padding(4)
        Me.cboWorkSheet.Name = "cboWorkSheet"
        Me.cboWorkSheet.Size = New System.Drawing.Size(412, 24)
        Me.cboWorkSheet.TabIndex = 289
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(154, 26)
        Me.cboDocumentType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(229, 24)
        Me.cboDocumentType.TabIndex = 287
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.Location = New System.Drawing.Point(11, 25)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 17)
        Me.Label7.TabIndex = 288
        Me.Label7.Text = "Document Type"
        '
        's
        '
        Me.s.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ค้นหา
        Me.s.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.s.Location = New System.Drawing.Point(926, 23)
        Me.s.Margin = New System.Windows.Forms.Padding(4)
        Me.s.Name = "s"
        Me.s.Size = New System.Drawing.Size(143, 87)
        Me.s.TabIndex = 0
        Me.s.Text = "    Browse"
        Me.s.UseVisualStyleBackColor = True
        '
        'txtWorkSheet
        '
        Me.txtWorkSheet.Location = New System.Drawing.Point(756, 60)
        Me.txtWorkSheet.Margin = New System.Windows.Forms.Padding(4)
        Me.txtWorkSheet.Name = "txtWorkSheet"
        Me.txtWorkSheet.Size = New System.Drawing.Size(153, 22)
        Me.txtWorkSheet.TabIndex = 1
        Me.txtWorkSheet.Visible = False
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFilePath.Location = New System.Drawing.Point(506, 26)
        Me.txtFilePath.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(413, 22)
        Me.txtFilePath.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(409, 60)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(90, 17)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Work Sheet :"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(428, 26)
        Me.lblFilePath.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(71, 17)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "File Path :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.grdPreviewData)
        Me.GroupBox2.Location = New System.Drawing.Point(9, 143)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(1077, 533)
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
        Me.grdPreviewData.Location = New System.Drawing.Point(4, 19)
        Me.grdPreviewData.Margin = New System.Windows.Forms.Padding(4)
        Me.grdPreviewData.Name = "grdPreviewData"
        Me.grdPreviewData.ReadOnly = True
        Me.grdPreviewData.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders
        Me.grdPreviewData.RowTemplate.Height = 24
        Me.grdPreviewData.Size = New System.Drawing.Size(1069, 510)
        Me.grdPreviewData.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "*.xls;*.xlsx;*.xlsm"""
        Me.OpenFileDialog1.Filter = "Excel Files|*.xls;*.xlsx;*.xlsm"""
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'BntLoaddata
        '
        Me.BntLoaddata.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ1
        Me.BntLoaddata.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.BntLoaddata.Location = New System.Drawing.Point(16, 684)
        Me.BntLoaddata.Margin = New System.Windows.Forms.Padding(4)
        Me.BntLoaddata.Name = "BntLoaddata"
        Me.BntLoaddata.Size = New System.Drawing.Size(155, 47)
        Me.BntLoaddata.TabIndex = 372
        Me.BntLoaddata.Text = "    Load Data"
        Me.BntLoaddata.UseVisualStyleBackColor = True
        '
        'btnValidate
        '
        Me.btnValidate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnValidate.Location = New System.Drawing.Point(179, 684)
        Me.btnValidate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(155, 47)
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
        Me.chkStock.Location = New System.Drawing.Point(520, 688)
        Me.chkStock.Margin = New System.Windows.Forms.Padding(4)
        Me.chkStock.Name = "chkStock"
        Me.chkStock.Size = New System.Drawing.Size(169, 21)
        Me.chkStock.TabIndex = 375
        Me.chkStock.Text = "ตรวจสอบสินค้าคงเหลือ"
        Me.chkStock.UseVisualStyleBackColor = True
        Me.chkStock.Visible = False
        '
        'frmImport_PO_TCR
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1096, 742)
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
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmImport_PO_TCR"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import ASN Data [EXCEL]"
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
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents cboWorkSheet As System.Windows.Forms.ComboBox
    Friend WithEvents BntLoaddata As System.Windows.Forms.Button
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents chkStock As System.Windows.Forms.CheckBox
End Class
