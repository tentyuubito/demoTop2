<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfig_Picking_KSL
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.grbRoute = New System.Windows.Forms.GroupBox
        Me.txtMFG_Day_Count = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDistributionCenter = New System.Windows.Forms.Label
        Me.cboDistributionCenter = New System.Windows.Forms.ComboBox
        Me.cboCustomerType = New System.Windows.Forms.ComboBox
        Me.lblCustomerType = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtDes = New System.Windows.Forms.TextBox
        Me.lbID = New System.Windows.Forms.Label
        Me.lblDes = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.grd = New System.Windows.Forms.DataGridView
        Me.grbProducType = New System.Windows.Forms.GroupBox
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.chkSelect = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.Col_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.grbRoute.SuspendLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbProducType.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(543, 591)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 37)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grbRoute
        '
        Me.grbRoute.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbRoute.Controls.Add(Me.txtMFG_Day_Count)
        Me.grbRoute.Controls.Add(Me.Label1)
        Me.grbRoute.Controls.Add(Me.lblDistributionCenter)
        Me.grbRoute.Controls.Add(Me.cboDistributionCenter)
        Me.grbRoute.Controls.Add(Me.cboCustomerType)
        Me.grbRoute.Controls.Add(Me.lblCustomerType)
        Me.grbRoute.Controls.Add(Me.txtID)
        Me.grbRoute.Controls.Add(Me.txtDes)
        Me.grbRoute.Controls.Add(Me.lbID)
        Me.grbRoute.Controls.Add(Me.lblDes)
        Me.grbRoute.Location = New System.Drawing.Point(12, 12)
        Me.grbRoute.Name = "grbRoute"
        Me.grbRoute.Size = New System.Drawing.Size(631, 139)
        Me.grbRoute.TabIndex = 0
        Me.grbRoute.TabStop = False
        Me.grbRoute.Text = "เส้นทาง"
        '
        'txtMFG_Day_Count
        '
        Me.txtMFG_Day_Count.Location = New System.Drawing.Point(336, 19)
        Me.txtMFG_Day_Count.MaxLength = 500
        Me.txtMFG_Day_Count.Name = "txtMFG_Day_Count"
        Me.txtMFG_Day_Count.Size = New System.Drawing.Size(97, 20)
        Me.txtMFG_Day_Count.TabIndex = 51
        Me.txtMFG_Day_Count.Text = "0"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(200, 22)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(130, 13)
        Me.Label1.TabIndex = 50
        Me.Label1.Text = "นับจากวันที่ผลิตไม่เกิน"
        '
        'lblDistributionCenter
        '
        Me.lblDistributionCenter.AutoSize = True
        Me.lblDistributionCenter.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDistributionCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDistributionCenter.Location = New System.Drawing.Point(18, 99)
        Me.lblDistributionCenter.Name = "lblDistributionCenter"
        Me.lblDistributionCenter.Size = New System.Drawing.Size(75, 13)
        Me.lblDistributionCenter.TabIndex = 49
        Me.lblDistributionCenter.Text = "ศูนย์กระจาย"
        '
        'cboDistributionCenter
        '
        Me.cboDistributionCenter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDistributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistributionCenter.FormattingEnabled = True
        Me.cboDistributionCenter.Location = New System.Drawing.Point(100, 96)
        Me.cboDistributionCenter.Name = "cboDistributionCenter"
        Me.cboDistributionCenter.Size = New System.Drawing.Size(525, 21)
        Me.cboDistributionCenter.TabIndex = 48
        '
        'cboCustomerType
        '
        Me.cboCustomerType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboCustomerType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCustomerType.FormattingEnabled = True
        Me.cboCustomerType.Location = New System.Drawing.Point(100, 69)
        Me.cboCustomerType.Name = "cboCustomerType"
        Me.cboCustomerType.Size = New System.Drawing.Size(525, 21)
        Me.cboCustomerType.TabIndex = 47
        '
        'lblCustomerType
        '
        Me.lblCustomerType.AutoSize = True
        Me.lblCustomerType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomerType.Location = New System.Drawing.Point(15, 72)
        Me.lblCustomerType.Name = "lblCustomerType"
        Me.lblCustomerType.Size = New System.Drawing.Size(79, 13)
        Me.lblCustomerType.TabIndex = 46
        Me.lblCustomerType.Text = "ประเภทลูกค้า"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(100, 19)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.ReadOnly = True
        Me.txtID.Size = New System.Drawing.Size(65, 20)
        Me.txtID.TabIndex = 0
        '
        'txtDes
        '
        Me.txtDes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDes.Location = New System.Drawing.Point(100, 43)
        Me.txtDes.MaxLength = 500
        Me.txtDes.Name = "txtDes"
        Me.txtDes.Size = New System.Drawing.Size(525, 20)
        Me.txtDes.TabIndex = 1
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(65, 23)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(24, 46)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(70, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "รายละเอียด"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(17, 592)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 36)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grd
        '
        Me.grd.AllowUserToAddRows = False
        Me.grd.AllowUserToDeleteRows = False
        Me.grd.AllowUserToResizeRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grd.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grd.BackgroundColor = System.Drawing.Color.White
        Me.grd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grd.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Col_Index, Me.chkSelect, Me.Col_Id, Me.Col_Description})
        Me.grd.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grd.Location = New System.Drawing.Point(3, 16)
        Me.grd.Name = "grd"
        Me.grd.RowHeadersVisible = False
        Me.grd.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grd.Size = New System.Drawing.Size(623, 410)
        Me.grd.TabIndex = 3
        '
        'grbProducType
        '
        Me.grbProducType.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbProducType.Controls.Add(Me.chkSelectAll)
        Me.grbProducType.Controls.Add(Me.grd)
        Me.grbProducType.Location = New System.Drawing.Point(14, 157)
        Me.grbProducType.Name = "grbProducType"
        Me.grbProducType.Size = New System.Drawing.Size(629, 429)
        Me.grbProducType.TabIndex = 4
        Me.grbProducType.TabStop = False
        Me.grbProducType.Text = "ประเภทสินค้า"
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(10, 21)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 4
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "SubRoute_index"
        Me.DataGridViewTextBoxColumn1.Frozen = True
        Me.DataGridViewTextBoxColumn1.HeaderText = "Index"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "chkSelect"
        Me.DataGridViewCheckBoxColumn1.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.ReadOnly = True
        Me.DataGridViewCheckBoxColumn1.TrueValue = "1"
        Me.DataGridViewCheckBoxColumn1.Width = 25
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "SubRoute_No"
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "รหัส"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 200
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn3.HeaderText = "รายละเอียด"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        '
        'Col_Index
        '
        Me.Col_Index.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None
        Me.Col_Index.DataPropertyName = "ProductType_Index"
        Me.Col_Index.Frozen = True
        Me.Col_Index.HeaderText = "Index"
        Me.Col_Index.Name = "Col_Index"
        Me.Col_Index.ReadOnly = True
        Me.Col_Index.Visible = False
        '
        'chkSelect
        '
        Me.chkSelect.DataPropertyName = "chkSelect"
        Me.chkSelect.FalseValue = "False"
        Me.chkSelect.Frozen = True
        Me.chkSelect.HeaderText = ""
        Me.chkSelect.Name = "chkSelect"
        Me.chkSelect.TrueValue = "True"
        Me.chkSelect.Width = 25
        '
        'Col_Id
        '
        Me.Col_Id.DataPropertyName = "ProductType_Id"
        Me.Col_Id.Frozen = True
        Me.Col_Id.HeaderText = "รหัส"
        Me.Col_Id.Name = "Col_Id"
        Me.Col_Id.ReadOnly = True
        Me.Col_Id.Width = 80
        '
        'Col_Description
        '
        Me.Col_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Col_Description.DataPropertyName = "Description"
        Me.Col_Description.HeaderText = "รายละเอียด"
        Me.Col_Description.MinimumWidth = 70
        Me.Col_Description.Name = "Col_Description"
        Me.Col_Description.ReadOnly = True
        '
        'frmConfig_Picking_KSL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(655, 640)
        Me.Controls.Add(Me.grbProducType)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grbRoute)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MinimizeBox = False
        Me.Name = "frmConfig_Picking_KSL"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ตั้งค่าการหยิบสินค้า"
        Me.grbRoute.ResumeLayout(False)
        Me.grbRoute.PerformLayout()
        CType(Me.grd, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbProducType.ResumeLayout(False)
        Me.grbProducType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grbRoute As System.Windows.Forms.GroupBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtDes As System.Windows.Forms.TextBox
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grd As System.Windows.Forms.DataGridView
    Friend WithEvents grbProducType As System.Windows.Forms.GroupBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cboCustomerType As System.Windows.Forms.ComboBox
    Friend WithEvents lblCustomerType As System.Windows.Forms.Label
    Friend WithEvents lblDistributionCenter As System.Windows.Forms.Label
    Friend WithEvents cboDistributionCenter As System.Windows.Forms.ComboBox
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents txtMFG_Day_Count As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Col_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSelect As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents Col_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Description As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
