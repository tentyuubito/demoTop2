<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport_SO_New_V2
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.ddlFormat_Import = New System.Windows.Forms.ComboBox
        Me.btnValidate = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.lblError = New System.Windows.Forms.Label
        Me.lblFile = New System.Windows.Forms.Label
        Me.lblOpenFileDialog = New System.Windows.Forms.Label
        Me.btnOpenFileDialog = New System.Windows.Forms.Button
        Me.btnConfigCSV = New System.Windows.Forms.Button
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.grdImportPO = New System.Windows.Forms.DataGridView
        Me.col_No = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Validate = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SOLD_TO_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_SO_NO = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SOLD_TO_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DOC_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SOLD_TO_ADD1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SALE_NAME = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SOLD_TO_ADD2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SOLD_TO_TEL = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_EXPECT_DELIVERY_DATE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SEQ = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SKU_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_QTY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_PACKAGE = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Remark = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ValidateRamark = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ORDER_BY = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdImportPO, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.ddlFormat_Import)
        Me.GroupBox1.Controls.Add(Me.btnValidate)
        Me.GroupBox1.Controls.Add(Me.btnSave)
        Me.GroupBox1.Controls.Add(Me.lblError)
        Me.GroupBox1.Controls.Add(Me.lblFile)
        Me.GroupBox1.Controls.Add(Me.lblOpenFileDialog)
        Me.GroupBox1.Controls.Add(Me.btnOpenFileDialog)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(743, 60)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "นำเข้า CSV"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(368, 38)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(71, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Format Import"
        '
        'ddlFormat_Import
        '
        Me.ddlFormat_Import.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.ddlFormat_Import.FormattingEnabled = True
        Me.ddlFormat_Import.Location = New System.Drawing.Point(371, 14)
        Me.ddlFormat_Import.Name = "ddlFormat_Import"
        Me.ddlFormat_Import.Size = New System.Drawing.Size(153, 21)
        Me.ddlFormat_Import.TabIndex = 6
        '
        'btnValidate
        '
        Me.btnValidate.BackgroundImage = Global.WMS_Site_KingStella.My.Resources.Resources.ล้างหน้าจอ
        Me.btnValidate.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnValidate.Location = New System.Drawing.Point(542, 13)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(91, 37)
        Me.btnValidate.TabIndex = 1
        Me.btnValidate.Text = "ตรวจสอบ"
        Me.btnValidate.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.BackgroundImage = Global.WMS_Site_KingStella.My.Resources.Resources.Save
        Me.btnSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnSave.Location = New System.Drawing.Point(639, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(91, 37)
        Me.btnSave.TabIndex = 2
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'lblError
        '
        Me.lblError.AutoSize = True
        Me.lblError.ForeColor = System.Drawing.Color.Red
        Me.lblError.Location = New System.Drawing.Point(254, 34)
        Me.lblError.Name = "lblError"
        Me.lblError.Size = New System.Drawing.Size(98, 13)
        Me.lblError.TabIndex = 4
        Me.lblError.Text = "จำนวนผิดพลาด    0"
        '
        'lblFile
        '
        Me.lblFile.AutoSize = True
        Me.lblFile.Location = New System.Drawing.Point(254, 17)
        Me.lblFile.Name = "lblFile"
        Me.lblFile.Size = New System.Drawing.Size(97, 13)
        Me.lblFile.TabIndex = 3
        Me.lblFile.Text = "จำนวนรายการ     0"
        '
        'lblOpenFileDialog
        '
        Me.lblOpenFileDialog.AutoSize = True
        Me.lblOpenFileDialog.Location = New System.Drawing.Point(15, 25)
        Me.lblOpenFileDialog.Name = "lblOpenFileDialog"
        Me.lblOpenFileDialog.Size = New System.Drawing.Size(110, 13)
        Me.lblOpenFileDialog.TabIndex = 1
        Me.lblOpenFileDialog.Text = "จำนวนไฟล์นำเข้า     0"
        '
        'btnOpenFileDialog
        '
        Me.btnOpenFileDialog.BackgroundImage = Global.WMS_Site_KingStella.My.Resources.Resources.เพิ่มรายการ
        Me.btnOpenFileDialog.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnOpenFileDialog.Location = New System.Drawing.Point(143, 13)
        Me.btnOpenFileDialog.Name = "btnOpenFileDialog"
        Me.btnOpenFileDialog.Size = New System.Drawing.Size(91, 37)
        Me.btnOpenFileDialog.TabIndex = 0
        Me.btnOpenFileDialog.Text = "เพิ่มไฟล์"
        Me.btnOpenFileDialog.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOpenFileDialog.UseVisualStyleBackColor = True
        '
        'btnConfigCSV
        '
        Me.btnConfigCSV.BackgroundImage = Global.WMS_Site_KingStella.My.Resources.Resources.รายงาน_แสดงรายงาน
        Me.btnConfigCSV.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnConfigCSV.Location = New System.Drawing.Point(804, 22)
        Me.btnConfigCSV.Name = "btnConfigCSV"
        Me.btnConfigCSV.Size = New System.Drawing.Size(127, 37)
        Me.btnConfigCSV.TabIndex = 5
        Me.btnConfigCSV.Text = "Config การนำเข้า"
        Me.btnConfigCSV.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfigCSV.UseVisualStyleBackColor = True
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'grdImportPO
        '
        Me.grdImportPO.BackgroundColor = System.Drawing.Color.White
        Me.grdImportPO.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdImportPO.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_No, Me.Col_Validate, Me.Col_SOLD_TO_ID, Me.Col_SO_NO, Me.col_SOLD_TO_NAME, Me.col_DOC_DATE, Me.col_SOLD_TO_ADD1, Me.col_SALE_NAME, Me.col_SOLD_TO_ADD2, Me.col_SOLD_TO_TEL, Me.col_EXPECT_DELIVERY_DATE, Me.col_SEQ, Me.col_SKU_ID, Me.col_QTY, Me.col_PACKAGE, Me.col_Remark, Me.col_ValidateRamark, Me.col_ORDER_BY})
        Me.grdImportPO.Location = New System.Drawing.Point(12, 78)
        Me.grdImportPO.Name = "grdImportPO"
        Me.grdImportPO.Size = New System.Drawing.Size(955, 468)
        Me.grdImportPO.TabIndex = 1
        '
        'col_No
        '
        Me.col_No.DataPropertyName = "No"
        Me.col_No.HeaderText = "ลำดับ"
        Me.col_No.Name = "col_No"
        Me.col_No.ReadOnly = True
        '
        'Col_Validate
        '
        Me.Col_Validate.DataPropertyName = "Validate"
        Me.Col_Validate.HeaderText = "ตรวจสอบ"
        Me.Col_Validate.Name = "Col_Validate"
        Me.Col_Validate.ReadOnly = True
        '
        'Col_SOLD_TO_ID
        '
        Me.Col_SOLD_TO_ID.DataPropertyName = "SOLD_TO_ID"
        Me.Col_SOLD_TO_ID.HeaderText = "รหัสลูกค้า"
        Me.Col_SOLD_TO_ID.Name = "Col_SOLD_TO_ID"
        Me.Col_SOLD_TO_ID.ReadOnly = True
        '
        'Col_SO_NO
        '
        Me.Col_SO_NO.DataPropertyName = "SO_NO"
        Me.Col_SO_NO.HeaderText = "เลขที่เอกสาร"
        Me.Col_SO_NO.Name = "Col_SO_NO"
        Me.Col_SO_NO.ReadOnly = True
        '
        'col_SOLD_TO_NAME
        '
        Me.col_SOLD_TO_NAME.DataPropertyName = "SOLD_TO_NAME"
        Me.col_SOLD_TO_NAME.HeaderText = "ชื่อ"
        Me.col_SOLD_TO_NAME.Name = "col_SOLD_TO_NAME"
        Me.col_SOLD_TO_NAME.ReadOnly = True
        '
        'col_DOC_DATE
        '
        Me.col_DOC_DATE.DataPropertyName = "DOC_DATE"
        Me.col_DOC_DATE.HeaderText = "วันที่เอกสาร"
        Me.col_DOC_DATE.Name = "col_DOC_DATE"
        Me.col_DOC_DATE.ReadOnly = True
        '
        'col_SOLD_TO_ADD1
        '
        Me.col_SOLD_TO_ADD1.DataPropertyName = "SOLD_TO_ADD1"
        Me.col_SOLD_TO_ADD1.HeaderText = "ที่อยู่"
        Me.col_SOLD_TO_ADD1.Name = "col_SOLD_TO_ADD1"
        Me.col_SOLD_TO_ADD1.ReadOnly = True
        '
        'col_SALE_NAME
        '
        Me.col_SALE_NAME.DataPropertyName = "SALE_NAME"
        Me.col_SALE_NAME.HeaderText = "พนักงานขาย"
        Me.col_SALE_NAME.Name = "col_SALE_NAME"
        Me.col_SALE_NAME.ReadOnly = True
        '
        'col_SOLD_TO_ADD2
        '
        Me.col_SOLD_TO_ADD2.DataPropertyName = "SOLD_TO_ADD2"
        Me.col_SOLD_TO_ADD2.HeaderText = "ที่อยู่"
        Me.col_SOLD_TO_ADD2.Name = "col_SOLD_TO_ADD2"
        Me.col_SOLD_TO_ADD2.ReadOnly = True
        '
        'col_SOLD_TO_TEL
        '
        Me.col_SOLD_TO_TEL.DataPropertyName = "SOLD_TO_TEL"
        Me.col_SOLD_TO_TEL.HeaderText = "ผู้ติดต่อ"
        Me.col_SOLD_TO_TEL.Name = "col_SOLD_TO_TEL"
        Me.col_SOLD_TO_TEL.ReadOnly = True
        '
        'col_EXPECT_DELIVERY_DATE
        '
        Me.col_EXPECT_DELIVERY_DATE.DataPropertyName = "EXPECT_DELIVERY_DATE"
        Me.col_EXPECT_DELIVERY_DATE.HeaderText = "วันครบกำหนดส่ง"
        Me.col_EXPECT_DELIVERY_DATE.Name = "col_EXPECT_DELIVERY_DATE"
        Me.col_EXPECT_DELIVERY_DATE.ReadOnly = True
        Me.col_EXPECT_DELIVERY_DATE.Width = 120
        '
        'col_SEQ
        '
        Me.col_SEQ.DataPropertyName = "SEQ"
        Me.col_SEQ.HeaderText = "ตน.เก็บ"
        Me.col_SEQ.Name = "col_SEQ"
        Me.col_SEQ.ReadOnly = True
        '
        'col_SKU_ID
        '
        Me.col_SKU_ID.DataPropertyName = "SKU_ID"
        Me.col_SKU_ID.HeaderText = "SKU_ID"
        Me.col_SKU_ID.Name = "col_SKU_ID"
        Me.col_SKU_ID.ReadOnly = True
        '
        'col_QTY
        '
        Me.col_QTY.DataPropertyName = "QTY"
        Me.col_QTY.HeaderText = "จำนวน"
        Me.col_QTY.Name = "col_QTY"
        Me.col_QTY.ReadOnly = True
        '
        'col_PACKAGE
        '
        Me.col_PACKAGE.DataPropertyName = "PACKAGE"
        Me.col_PACKAGE.HeaderText = "PACKAGE"
        Me.col_PACKAGE.Name = "col_PACKAGE"
        Me.col_PACKAGE.ReadOnly = True
        '
        'col_Remark
        '
        Me.col_Remark.DataPropertyName = "Remark"
        Me.col_Remark.HeaderText = "หมายเหตุ"
        Me.col_Remark.Name = "col_Remark"
        Me.col_Remark.ReadOnly = True
        '
        'col_ValidateRamark
        '
        Me.col_ValidateRamark.HeaderText = "ValidateRamark"
        Me.col_ValidateRamark.Name = "col_ValidateRamark"
        Me.col_ValidateRamark.ReadOnly = True
        '
        'col_ORDER_BY
        '
        Me.col_ORDER_BY.DataPropertyName = "ORDER_BY"
        Me.col_ORDER_BY.HeaderText = "ORDER_BY"
        Me.col_ORDER_BY.Name = "col_ORDER_BY"
        Me.col_ORDER_BY.Visible = False
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(129, 188)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(738, 73)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 6
        Me.ProgressBar1.Visible = False
        '
        'Timer1
        '
        '
        'frmImport_SO_New_V2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(979, 558)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.grdImportPO)
        Me.Controls.Add(Me.btnConfigCSV)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmImport_SO_New_V2"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "นำเข้า SO"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdImportPO, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnOpenFileDialog As System.Windows.Forms.Button
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents lblOpenFileDialog As System.Windows.Forms.Label
    Friend WithEvents lblError As System.Windows.Forms.Label
    Friend WithEvents lblFile As System.Windows.Forms.Label
    Friend WithEvents grdImportPO As System.Windows.Forms.DataGridView
    Friend WithEvents btnConfigCSV As System.Windows.Forms.Button
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents col_No As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Validate As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SOLD_TO_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_SO_NO As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SOLD_TO_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DOC_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SOLD_TO_ADD1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SALE_NAME As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SOLD_TO_ADD2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SOLD_TO_TEL As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_EXPECT_DELIVERY_DATE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SEQ As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SKU_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_QTY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_PACKAGE As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Remark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ValidateRamark As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ORDER_BY As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ddlFormat_Import As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
