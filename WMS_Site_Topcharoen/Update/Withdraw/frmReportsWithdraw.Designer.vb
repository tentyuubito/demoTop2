<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportsWithdraw
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportsWithdraw))
        Me.gboCondition = New System.Windows.Forms.GroupBox
        Me.txtRef_No = New System.Windows.Forms.TextBox
        Me.txtWithdraw_No = New System.Windows.Forms.TextBox
        Me.chkRef_No = New System.Windows.Forms.CheckBox
        Me.chkWithdraw_No = New System.Windows.Forms.CheckBox
        Me.dtpWithdraw_Date_E = New System.Windows.Forms.DateTimePicker
        Me.chkWithdraw_Date = New System.Windows.Forms.CheckBox
        Me.lblWithdraw_Date = New System.Windows.Forms.Label
        Me.dtpWithdraw_Date_S = New System.Windows.Forms.DateTimePicker
        Me.btnSearch = New System.Windows.Forms.Button
        Me.gboData = New System.Windows.Forms.GroupBox
        Me.dgvData = New System.Windows.Forms.DataGridView
        Me.btnExport = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.gboCondition.SuspendLayout()
        Me.gboData.SuspendLayout()
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'gboCondition
        '
        Me.gboCondition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboCondition.Controls.Add(Me.txtRef_No)
        Me.gboCondition.Controls.Add(Me.txtWithdraw_No)
        Me.gboCondition.Controls.Add(Me.chkRef_No)
        Me.gboCondition.Controls.Add(Me.chkWithdraw_No)
        Me.gboCondition.Controls.Add(Me.dtpWithdraw_Date_E)
        Me.gboCondition.Controls.Add(Me.chkWithdraw_Date)
        Me.gboCondition.Controls.Add(Me.lblWithdraw_Date)
        Me.gboCondition.Controls.Add(Me.dtpWithdraw_Date_S)
        Me.gboCondition.Controls.Add(Me.btnSearch)
        Me.gboCondition.Location = New System.Drawing.Point(12, 12)
        Me.gboCondition.Name = "gboCondition"
        Me.gboCondition.Size = New System.Drawing.Size(520, 101)
        Me.gboCondition.TabIndex = 0
        Me.gboCondition.TabStop = False
        Me.gboCondition.Text = "เงื่อนไข"
        '
        'txtRef_No
        '
        Me.txtRef_No.Location = New System.Drawing.Point(132, 73)
        Me.txtRef_No.MaxLength = 50
        Me.txtRef_No.Name = "txtRef_No"
        Me.txtRef_No.Size = New System.Drawing.Size(249, 20)
        Me.txtRef_No.TabIndex = 15
        '
        'txtWithdraw_No
        '
        Me.txtWithdraw_No.Location = New System.Drawing.Point(132, 47)
        Me.txtWithdraw_No.MaxLength = 50
        Me.txtWithdraw_No.Name = "txtWithdraw_No"
        Me.txtWithdraw_No.Size = New System.Drawing.Size(249, 20)
        Me.txtWithdraw_No.TabIndex = 14
        '
        'chkRef_No
        '
        Me.chkRef_No.Location = New System.Drawing.Point(6, 73)
        Me.chkRef_No.Name = "chkRef_No"
        Me.chkRef_No.Size = New System.Drawing.Size(120, 20)
        Me.chkRef_No.TabIndex = 13
        Me.chkRef_No.Text = "เลขที่เอกสารอ้างอิง"
        Me.chkRef_No.UseVisualStyleBackColor = True
        '
        'chkWithdraw_No
        '
        Me.chkWithdraw_No.Location = New System.Drawing.Point(6, 47)
        Me.chkWithdraw_No.Name = "chkWithdraw_No"
        Me.chkWithdraw_No.Size = New System.Drawing.Size(120, 20)
        Me.chkWithdraw_No.TabIndex = 12
        Me.chkWithdraw_No.Text = "เลขที่ใบเบิก"
        Me.chkWithdraw_No.UseVisualStyleBackColor = True
        '
        'dtpWithdraw_Date_E
        '
        Me.dtpWithdraw_Date_E.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpWithdraw_Date_E.Location = New System.Drawing.Point(272, 21)
        Me.dtpWithdraw_Date_E.Name = "dtpWithdraw_Date_E"
        Me.dtpWithdraw_Date_E.Size = New System.Drawing.Size(109, 20)
        Me.dtpWithdraw_Date_E.TabIndex = 11
        '
        'chkWithdraw_Date
        '
        Me.chkWithdraw_Date.Location = New System.Drawing.Point(6, 21)
        Me.chkWithdraw_Date.Name = "chkWithdraw_Date"
        Me.chkWithdraw_Date.Size = New System.Drawing.Size(120, 20)
        Me.chkWithdraw_Date.TabIndex = 10
        Me.chkWithdraw_Date.Text = "วันที่เบิก"
        Me.chkWithdraw_Date.UseVisualStyleBackColor = True
        '
        'lblWithdraw_Date
        '
        Me.lblWithdraw_Date.AutoSize = True
        Me.lblWithdraw_Date.Location = New System.Drawing.Point(247, 24)
        Me.lblWithdraw_Date.Name = "lblWithdraw_Date"
        Me.lblWithdraw_Date.Size = New System.Drawing.Size(19, 13)
        Me.lblWithdraw_Date.TabIndex = 9
        Me.lblWithdraw_Date.Text = "ถึง"
        '
        'dtpWithdraw_Date_S
        '
        Me.dtpWithdraw_Date_S.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpWithdraw_Date_S.Location = New System.Drawing.Point(132, 21)
        Me.dtpWithdraw_Date_S.Name = "dtpWithdraw_Date_S"
        Me.dtpWithdraw_Date_S.Size = New System.Drawing.Size(109, 20)
        Me.dtpWithdraw_Date_S.TabIndex = 7
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(400, 19)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(100, 38)
        Me.btnSearch.TabIndex = 5
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'gboData
        '
        Me.gboData.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.gboData.Controls.Add(Me.dgvData)
        Me.gboData.Location = New System.Drawing.Point(12, 118)
        Me.gboData.Name = "gboData"
        Me.gboData.Size = New System.Drawing.Size(520, 162)
        Me.gboData.TabIndex = 1
        Me.gboData.TabStop = False
        Me.gboData.Text = "ข้อมูล"
        '
        'dgvData
        '
        Me.dgvData.AllowUserToAddRows = False
        Me.dgvData.AllowUserToDeleteRows = False
        Me.dgvData.AllowUserToResizeRows = False
        Me.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvData.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvData.Location = New System.Drawing.Point(3, 16)
        Me.dgvData.Name = "dgvData"
        Me.dgvData.ReadOnly = True
        Me.dgvData.RowHeadersVisible = False
        Me.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect
        Me.dgvData.Size = New System.Drawing.Size(514, 143)
        Me.dgvData.TabIndex = 0
        '
        'btnExport
        '
        Me.btnExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExport.Image = CType(resources.GetObject("btnExport.Image"), System.Drawing.Image)
        Me.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExport.Location = New System.Drawing.Point(12, 286)
        Me.btnExport.Name = "btnExport"
        Me.btnExport.Size = New System.Drawing.Size(100, 38)
        Me.btnExport.TabIndex = 2
        Me.btnExport.Text = "Export"
        Me.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExport.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Image = CType(resources.GetObject("btnClose.Image"), System.Drawing.Image)
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(432, 286)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(100, 38)
        Me.btnClose.TabIndex = 4
        Me.btnClose.Text = "ปิด"
        Me.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'frmReportsWithdraw
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(544, 336)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExport)
        Me.Controls.Add(Me.gboData)
        Me.Controls.Add(Me.gboCondition)
        Me.MinimumSize = New System.Drawing.Size(550, 365)
        Me.Name = "frmReportsWithdraw"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Export to Excel"
        Me.gboCondition.ResumeLayout(False)
        Me.gboCondition.PerformLayout()
        Me.gboData.ResumeLayout(False)
        CType(Me.dgvData, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gboCondition As System.Windows.Forms.GroupBox
    Friend WithEvents txtRef_No As System.Windows.Forms.TextBox
    Friend WithEvents txtWithdraw_No As System.Windows.Forms.TextBox
    Friend WithEvents chkRef_No As System.Windows.Forms.CheckBox
    Friend WithEvents chkWithdraw_No As System.Windows.Forms.CheckBox
    Friend WithEvents dtpWithdraw_Date_E As System.Windows.Forms.DateTimePicker
    Friend WithEvents chkWithdraw_Date As System.Windows.Forms.CheckBox
    Friend WithEvents lblWithdraw_Date As System.Windows.Forms.Label
    Friend WithEvents dtpWithdraw_Date_S As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents gboData As System.Windows.Forms.GroupBox
    Friend WithEvents btnExport As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dgvData As System.Windows.Forms.DataGridView
End Class
