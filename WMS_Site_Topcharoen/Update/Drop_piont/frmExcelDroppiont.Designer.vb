<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExcelDroppiont
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExcelDroppiont))
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnImport = New System.Windows.Forms.Button
        Me.groupimport = New System.Windows.Forms.GroupBox
        Me.BntLoaddata = New System.Windows.Forms.Button
        Me.cboWorkSheet = New System.Windows.Forms.ComboBox
        Me.btnBrowse = New System.Windows.Forms.Button
        Me.txtFilePath = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblFilePath = New System.Windows.Forms.Label
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.pnlProgressBar = New System.Windows.Forms.Panel
        Me.lblProgress = New System.Windows.Forms.Label
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar
        Me.grdPreviewData = New System.Windows.Forms.DataGridView
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.btnDeleteFailAll = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        Me.lblCountRows = New System.Windows.Forms.Label
        Me.btnValidate = New System.Windows.Forms.Button
        Me.PanelOrderItemButton = New System.Windows.Forms.Panel
        Me.btnProvess = New System.Windows.Forms.Button
        Me.groupimport.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.pnlProgressBar.SuspendLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelOrderItemButton.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(705, 23)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 20
        Me.btnExit.Text = "   Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnImport
        '
        Me.btnImport.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ส่งข้อมูล
        Me.btnImport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnImport.Location = New System.Drawing.Point(138, 23)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(116, 38)
        Me.btnImport.TabIndex = 21
        Me.btnImport.Text = "       Process  Import"
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'groupimport
        '
        Me.groupimport.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.groupimport.Controls.Add(Me.BntLoaddata)
        Me.groupimport.Controls.Add(Me.cboWorkSheet)
        Me.groupimport.Controls.Add(Me.btnBrowse)
        Me.groupimport.Controls.Add(Me.txtFilePath)
        Me.groupimport.Controls.Add(Me.Label3)
        Me.groupimport.Controls.Add(Me.lblFilePath)
        Me.groupimport.Location = New System.Drawing.Point(7, 12)
        Me.groupimport.Name = "groupimport"
        Me.groupimport.Size = New System.Drawing.Size(808, 81)
        Me.groupimport.TabIndex = 25
        Me.groupimport.TabStop = False
        Me.groupimport.Text = "Select File "
        '
        'BntLoaddata
        '
        Me.BntLoaddata.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ล้างหน้าจอ
        Me.BntLoaddata.Location = New System.Drawing.Point(750, 19)
        Me.BntLoaddata.Name = "BntLoaddata"
        Me.BntLoaddata.Size = New System.Drawing.Size(50, 47)
        Me.BntLoaddata.TabIndex = 372
        Me.BntLoaddata.UseVisualStyleBackColor = True
        '
        'cboWorkSheet
        '
        Me.cboWorkSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkSheet.FormattingEnabled = True
        Me.cboWorkSheet.Location = New System.Drawing.Point(78, 45)
        Me.cboWorkSheet.Name = "cboWorkSheet"
        Me.cboWorkSheet.Size = New System.Drawing.Size(611, 21)
        Me.cboWorkSheet.TabIndex = 289
        '
        'btnBrowse
        '
        Me.btnBrowse.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnBrowse.Location = New System.Drawing.Point(695, 19)
        Me.btnBrowse.Name = "btnBrowse"
        Me.btnBrowse.Size = New System.Drawing.Size(51, 47)
        Me.btnBrowse.TabIndex = 0
        Me.btnBrowse.UseVisualStyleBackColor = True
        '
        'txtFilePath
        '
        Me.txtFilePath.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtFilePath.Location = New System.Drawing.Point(78, 19)
        Me.txtFilePath.Name = "txtFilePath"
        Me.txtFilePath.ReadOnly = True
        Me.txtFilePath.Size = New System.Drawing.Size(611, 20)
        Me.txtFilePath.TabIndex = 0
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(8, 48)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Work Sheet :"
        '
        'lblFilePath
        '
        Me.lblFilePath.AutoSize = True
        Me.lblFilePath.Location = New System.Drawing.Point(24, 22)
        Me.lblFilePath.Name = "lblFilePath"
        Me.lblFilePath.Size = New System.Drawing.Size(54, 13)
        Me.lblFilePath.TabIndex = 0
        Me.lblFilePath.Text = "File Path :"
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.pnlProgressBar)
        Me.GroupBox2.Controls.Add(Me.grdPreviewData)
        Me.GroupBox2.Location = New System.Drawing.Point(7, 99)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(808, 538)
        Me.GroupBox2.TabIndex = 26
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Preview Data"
        '
        'pnlProgressBar
        '
        Me.pnlProgressBar.BackColor = System.Drawing.Color.White
        Me.pnlProgressBar.Controls.Add(Me.lblProgress)
        Me.pnlProgressBar.Controls.Add(Me.ProgressBar1)
        Me.pnlProgressBar.Location = New System.Drawing.Point(193, 175)
        Me.pnlProgressBar.Name = "pnlProgressBar"
        Me.pnlProgressBar.Size = New System.Drawing.Size(422, 100)
        Me.pnlProgressBar.TabIndex = 81
        Me.pnlProgressBar.Visible = False
        '
        'lblProgress
        '
        Me.lblProgress.AutoSize = True
        Me.lblProgress.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblProgress.Location = New System.Drawing.Point(11, 13)
        Me.lblProgress.Name = "lblProgress"
        Me.lblProgress.Size = New System.Drawing.Size(136, 16)
        Me.lblProgress.TabIndex = 9
        Me.lblProgress.Tag = "ระบบกำลังประมวลผล..."
        Me.lblProgress.Text = "ระบบกำลังประมวลผล..."
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(14, 32)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(393, 48)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee
        Me.ProgressBar1.TabIndex = 8
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
        Me.grdPreviewData.Size = New System.Drawing.Size(802, 519)
        Me.grdPreviewData.TabIndex = 0
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.DefaultExt = "*.xls"
        Me.OpenFileDialog1.Filter = "Excel File|*.xls"
        Me.OpenFileDialog1.RestoreDirectory = True
        '
        'btnDeleteFailAll
        '
        Me.btnDeleteFailAll.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnDeleteFailAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDeleteFailAll.Location = New System.Drawing.Point(367, 23)
        Me.btnDeleteFailAll.Name = "btnDeleteFailAll"
        Me.btnDeleteFailAll.Size = New System.Drawing.Size(117, 38)
        Me.btnDeleteFailAll.TabIndex = 28
        Me.btnDeleteFailAll.Text = "ลบที่ผิดพลาด"
        Me.btnDeleteFailAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDeleteFailAll.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDelete.Location = New System.Drawing.Point(254, 23)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(113, 38)
        Me.btnDelete.TabIndex = 27
        Me.btnDelete.Text = "ลบที่เลือก"
        Me.btnDelete.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'lblCountRows
        '
        Me.lblCountRows.AutoSize = True
        Me.lblCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lblCountRows.Location = New System.Drawing.Point(17, 6)
        Me.lblCountRows.Name = "lblCountRows"
        Me.lblCountRows.Size = New System.Drawing.Size(71, 13)
        Me.lblCountRows.TabIndex = 29
        Me.lblCountRows.Text = "ไม่พบรายการ"
        '
        'btnValidate
        '
        Me.btnValidate.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btnValidate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnValidate.Location = New System.Drawing.Point(12, 23)
        Me.btnValidate.Name = "btnValidate"
        Me.btnValidate.Size = New System.Drawing.Size(126, 38)
        Me.btnValidate.TabIndex = 30
        Me.btnValidate.Text = "       Validate"
        Me.btnValidate.UseVisualStyleBackColor = True
        '
        'PanelOrderItemButton
        '
        Me.PanelOrderItemButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PanelOrderItemButton.Controls.Add(Me.btnProvess)
        Me.PanelOrderItemButton.Controls.Add(Me.btnValidate)
        Me.PanelOrderItemButton.Controls.Add(Me.btnImport)
        Me.PanelOrderItemButton.Controls.Add(Me.lblCountRows)
        Me.PanelOrderItemButton.Controls.Add(Me.btnExit)
        Me.PanelOrderItemButton.Controls.Add(Me.btnDeleteFailAll)
        Me.PanelOrderItemButton.Controls.Add(Me.btnDelete)
        Me.PanelOrderItemButton.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.PanelOrderItemButton.Location = New System.Drawing.Point(0, 643)
        Me.PanelOrderItemButton.Name = "PanelOrderItemButton"
        Me.PanelOrderItemButton.Size = New System.Drawing.Size(822, 72)
        Me.PanelOrderItemButton.TabIndex = 31
        '
        'btnProvess
        '
        Me.btnProvess.Enabled = False
        Me.btnProvess.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnProvess.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnProvess.Location = New System.Drawing.Point(490, 23)
        Me.btnProvess.Name = "btnProvess"
        Me.btnProvess.Size = New System.Drawing.Size(85, 38)
        Me.btnProvess.TabIndex = 31
        Me.btnProvess.Text = "ลบรายการที่ผ่านแล้ว"
        Me.btnProvess.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnProvess.UseVisualStyleBackColor = True
        '
        'frmExcelDroppiont
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(822, 715)
        Me.Controls.Add(Me.PanelOrderItemButton)
        Me.Controls.Add(Me.groupimport)
        Me.Controls.Add(Me.GroupBox2)
        Me.KeyPreview = True
        Me.Name = "frmExcelDroppiont"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import Shipment Data [EXCEL]"
        Me.groupimport.ResumeLayout(False)
        Me.groupimport.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.pnlProgressBar.ResumeLayout(False)
        Me.pnlProgressBar.PerformLayout()
        CType(Me.grdPreviewData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelOrderItemButton.ResumeLayout(False)
        Me.PanelOrderItemButton.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents groupimport As System.Windows.Forms.GroupBox
    Friend WithEvents btnBrowse As System.Windows.Forms.Button
    Friend WithEvents txtFilePath As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblFilePath As System.Windows.Forms.Label
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents grdPreviewData As System.Windows.Forms.DataGridView
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
    Friend WithEvents cboWorkSheet As System.Windows.Forms.ComboBox
    Friend WithEvents BntLoaddata As System.Windows.Forms.Button
    Friend WithEvents btnDeleteFailAll As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents pnlProgressBar As System.Windows.Forms.Panel
    Friend WithEvents lblProgress As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents lblCountRows As System.Windows.Forms.Label
    Friend WithEvents btnValidate As System.Windows.Forms.Button
    Friend WithEvents PanelOrderItemButton As System.Windows.Forms.Panel
    Friend WithEvents btnProvess As System.Windows.Forms.Button
End Class
