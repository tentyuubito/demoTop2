<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportSO_P2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImportSO_P2))
        Me.grbItem = New System.Windows.Forms.GroupBox
        Me.grdData = New System.Windows.Forms.DataGridView
        Me.btnOK = New System.Windows.Forms.Button
        Me.lblDocumentType = New System.Windows.Forms.Label
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.btnImport = New System.Windows.Forms.Button
        Me.cboSheet = New System.Windows.Forms.ComboBox
        Me.lblSum = New System.Windows.Forms.Label
        Me.txtFileName = New System.Windows.Forms.TextBox
        Me.btnSave = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnClear = New System.Windows.Forms.Button
        Me.openDialogImport = New System.Windows.Forms.OpenFileDialog
        Me.grbItem.SuspendLayout()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbItem
        '
        Me.grbItem.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbItem.Controls.Add(Me.grdData)
        Me.grbItem.Controls.Add(Me.btnOK)
        Me.grbItem.Controls.Add(Me.lblDocumentType)
        Me.grbItem.Controls.Add(Me.cboDocumentType)
        Me.grbItem.Controls.Add(Me.btnImport)
        Me.grbItem.Controls.Add(Me.cboSheet)
        Me.grbItem.Controls.Add(Me.lblSum)
        Me.grbItem.Controls.Add(Me.txtFileName)
        Me.grbItem.Controls.Add(Me.btnSave)
        Me.grbItem.Controls.Add(Me.btnExit)
        Me.grbItem.Controls.Add(Me.btnClear)
        Me.grbItem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.grbItem.Location = New System.Drawing.Point(16, 15)
        Me.grbItem.Margin = New System.Windows.Forms.Padding(4)
        Me.grbItem.Name = "grbItem"
        Me.grbItem.Padding = New System.Windows.Forms.Padding(4)
        Me.grbItem.Size = New System.Drawing.Size(945, 496)
        Me.grbItem.TabIndex = 0
        Me.grbItem.TabStop = False
        Me.grbItem.Text = "ข้อมูล"
        '
        'grdData
        '
        Me.grdData.AllowUserToAddRows = False
        Me.grdData.AllowUserToDeleteRows = False
        Me.grdData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grdData.BackgroundColor = System.Drawing.Color.White
        Me.grdData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdData.Location = New System.Drawing.Point(0, 65)
        Me.grdData.Margin = New System.Windows.Forms.Padding(4)
        Me.grdData.Name = "grdData"
        Me.grdData.RowHeadersVisible = False
        Me.grdData.RowTemplate.Height = 24
        Me.grdData.Size = New System.Drawing.Size(937, 319)
        Me.grdData.TabIndex = 12
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnOK.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ค้นหา
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.Location = New System.Drawing.Point(8, 428)
        Me.btnOK.Margin = New System.Windows.Forms.Padding(4)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(125, 54)
        Me.btnOK.TabIndex = 11
        Me.btnOK.Text = "เลือกไฟล"
        Me.btnOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'lblDocumentType
        '
        Me.lblDocumentType.AutoSize = True
        Me.lblDocumentType.Location = New System.Drawing.Point(60, 27)
        Me.lblDocumentType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDocumentType.Name = "lblDocumentType"
        Me.lblDocumentType.Size = New System.Drawing.Size(88, 17)
        Me.lblDocumentType.TabIndex = 0
        Me.lblDocumentType.Text = "ประเภทเอกสาร"
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(173, 23)
        Me.cboDocumentType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(284, 25)
        Me.cboDocumentType.TabIndex = 1
        '
        'btnImport
        '
        Me.btnImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnImport.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ดึงข้อมูล
        Me.btnImport.Location = New System.Drawing.Point(425, 428)
        Me.btnImport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnImport.Name = "btnImport"
        Me.btnImport.Size = New System.Drawing.Size(67, 54)
        Me.btnImport.TabIndex = 7
        Me.btnImport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnImport.UseVisualStyleBackColor = True
        '
        'cboSheet
        '
        Me.cboSheet.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cboSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSheet.FormattingEnabled = True
        Me.cboSheet.Location = New System.Drawing.Point(136, 455)
        Me.cboSheet.Margin = New System.Windows.Forms.Padding(4)
        Me.cboSheet.Name = "cboSheet"
        Me.cboSheet.Size = New System.Drawing.Size(284, 25)
        Me.cboSheet.TabIndex = 6
        '
        'lblSum
        '
        Me.lblSum.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSum.AutoSize = True
        Me.lblSum.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSum.ForeColor = System.Drawing.Color.Blue
        Me.lblSum.Location = New System.Drawing.Point(8, 391)
        Me.lblSum.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSum.Name = "lblSum"
        Me.lblSum.Size = New System.Drawing.Size(55, 25)
        Me.lblSum.TabIndex = 3
        Me.lblSum.Text = "รวม :"
        '
        'txtFileName
        '
        Me.txtFileName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtFileName.Location = New System.Drawing.Point(136, 428)
        Me.txtFileName.Margin = New System.Windows.Forms.Padding(4)
        Me.txtFileName.Name = "txtFileName"
        Me.txtFileName.ReadOnly = True
        Me.txtFileName.Size = New System.Drawing.Size(284, 23)
        Me.txtFileName.TabIndex = 5
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(496, 428)
        Me.btnSave.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(143, 54)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(795, 427)
        Me.btnExit.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(143, 54)
        Me.btnExit.TabIndex = 10
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnClear
        '
        Me.btnClear.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClear.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnClear.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ล้างหน้าจอ
        Me.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClear.Location = New System.Drawing.Point(647, 428)
        Me.btnClear.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(143, 54)
        Me.btnClear.TabIndex = 9
        Me.btnClear.Text = "ล้างข้อมูล"
        Me.btnClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'openDialogImport
        '
        Me.openDialogImport.DefaultExt = "*.xls"
        Me.openDialogImport.Filter = "Excel File|*.xls,.xlsx"
        Me.openDialogImport.RestoreDirectory = True
        '
        'frmImportSO_P2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(968, 528)
        Me.Controls.Add(Me.grbItem)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmImportSO_P2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "นำเข้าข้อมูล SO"
        Me.grbItem.ResumeLayout(False)
        Me.grbItem.PerformLayout()
        CType(Me.grdData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbItem As System.Windows.Forms.GroupBox
    Friend WithEvents btnClear As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents txtFileName As System.Windows.Forms.TextBox
    Friend WithEvents lblSum As System.Windows.Forms.Label
    Friend WithEvents btnImport As System.Windows.Forms.Button
    Friend WithEvents cboSheet As System.Windows.Forms.ComboBox
    Friend WithEvents openDialogImport As System.Windows.Forms.OpenFileDialog
    Friend WithEvents lblDocumentType As System.Windows.Forms.Label
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents grdData As System.Windows.Forms.DataGridView
End Class
