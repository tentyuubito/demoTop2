<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUserManagement_Encode
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmUserManagement_Encode))
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblDistributionCenter = New System.Windows.Forms.Label
        Me.txtUserID = New System.Windows.Forms.TextBox
        Me.btnResetPassword = New System.Windows.Forms.Button
        Me.cboDistributionCenter = New System.Windows.Forms.ComboBox
        Me.txtPasswdConfirm = New System.Windows.Forms.TextBox
        Me.lblPasswordConfirm = New System.Windows.Forms.Label
        Me.txtPasswd = New System.Windows.Forms.TextBox
        Me.lblPasswd = New System.Windows.Forms.Label
        Me.txtUserName = New System.Windows.Forms.TextBox
        Me.lblUserName = New System.Windows.Forms.Label
        Me.cboDepartMent = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.cbGroupIndex = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.txtFullName = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.TabPage1 = New System.Windows.Forms.TabPage
        Me.TabPage2 = New System.Windows.Forms.TabPage
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.grdConfigItem = New System.Windows.Forms.DataGridView
        Me.col_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Index = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_IsUse = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_IsDefault = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_Customer_ID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Co_customer = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnRemovePic = New System.Windows.Forms.Button
        Me.btnAddPic = New System.Windows.Forms.Button
        Me.picEmployee = New System.Windows.Forms.PictureBox
        Me.OpenFileDialog1 = New System.Windows.Forms.OpenFileDialog
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        CType(Me.grdConfigItem, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picEmployee, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnRemovePic)
        Me.GroupBox1.Controls.Add(Me.lblDistributionCenter)
        Me.GroupBox1.Controls.Add(Me.btnAddPic)
        Me.GroupBox1.Controls.Add(Me.txtUserID)
        Me.GroupBox1.Controls.Add(Me.picEmployee)
        Me.GroupBox1.Controls.Add(Me.btnResetPassword)
        Me.GroupBox1.Controls.Add(Me.cboDistributionCenter)
        Me.GroupBox1.Controls.Add(Me.txtPasswdConfirm)
        Me.GroupBox1.Controls.Add(Me.lblPasswordConfirm)
        Me.GroupBox1.Controls.Add(Me.txtPasswd)
        Me.GroupBox1.Controls.Add(Me.lblPasswd)
        Me.GroupBox1.Controls.Add(Me.txtUserName)
        Me.GroupBox1.Controls.Add(Me.lblUserName)
        Me.GroupBox1.Controls.Add(Me.cboDepartMent)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.cbGroupIndex)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Controls.Add(Me.txtFullName)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 6)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(539, 256)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "General"
        '
        'lblDistributionCenter
        '
        Me.lblDistributionCenter.AutoSize = True
        Me.lblDistributionCenter.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDistributionCenter.Location = New System.Drawing.Point(48, 101)
        Me.lblDistributionCenter.Name = "lblDistributionCenter"
        Me.lblDistributionCenter.Size = New System.Drawing.Size(66, 13)
        Me.lblDistributionCenter.TabIndex = 22
        Me.lblDistributionCenter.Text = "ศูนย์กระจาย"
        '
        'txtUserID
        '
        Me.txtUserID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtUserID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUserID.Location = New System.Drawing.Point(123, 17)
        Me.txtUserID.MaxLength = 50
        Me.txtUserID.Name = "txtUserID"
        Me.txtUserID.Size = New System.Drawing.Size(270, 20)
        Me.txtUserID.TabIndex = 20
        '
        'btnResetPassword
        '
        Me.btnResetPassword.Image = CType(resources.GetObject("btnResetPassword.Image"), System.Drawing.Image)
        Me.btnResetPassword.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnResetPassword.Location = New System.Drawing.Point(408, 194)
        Me.btnResetPassword.Name = "btnResetPassword"
        Me.btnResetPassword.Size = New System.Drawing.Size(125, 38)
        Me.btnResetPassword.TabIndex = 18
        Me.btnResetPassword.Text = "Reset Password"
        Me.btnResetPassword.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnResetPassword.UseVisualStyleBackColor = True
        '
        'cboDistributionCenter
        '
        Me.cboDistributionCenter.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboDistributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistributionCenter.FormattingEnabled = True
        Me.cboDistributionCenter.Location = New System.Drawing.Point(123, 98)
        Me.cboDistributionCenter.Name = "cboDistributionCenter"
        Me.cboDistributionCenter.Size = New System.Drawing.Size(270, 21)
        Me.cboDistributionCenter.TabIndex = 21
        '
        'txtPasswdConfirm
        '
        Me.txtPasswdConfirm.Location = New System.Drawing.Point(123, 212)
        Me.txtPasswdConfirm.Name = "txtPasswdConfirm"
        Me.txtPasswdConfirm.Size = New System.Drawing.Size(270, 20)
        Me.txtPasswdConfirm.TabIndex = 17
        Me.txtPasswdConfirm.Text = "123456"
        Me.txtPasswdConfirm.UseSystemPasswordChar = True
        '
        'lblPasswordConfirm
        '
        Me.lblPasswordConfirm.AutoSize = True
        Me.lblPasswordConfirm.Location = New System.Drawing.Point(20, 219)
        Me.lblPasswordConfirm.Name = "lblPasswordConfirm"
        Me.lblPasswordConfirm.Size = New System.Drawing.Size(94, 13)
        Me.lblPasswordConfirm.TabIndex = 16
        Me.lblPasswordConfirm.Text = "Confirm Password "
        '
        'txtPasswd
        '
        Me.txtPasswd.Location = New System.Drawing.Point(123, 185)
        Me.txtPasswd.Name = "txtPasswd"
        Me.txtPasswd.Size = New System.Drawing.Size(270, 20)
        Me.txtPasswd.TabIndex = 15
        Me.txtPasswd.Text = "123456"
        Me.txtPasswd.UseSystemPasswordChar = True
        '
        'lblPasswd
        '
        Me.lblPasswd.AutoSize = True
        Me.lblPasswd.Location = New System.Drawing.Point(61, 188)
        Me.lblPasswd.Name = "lblPasswd"
        Me.lblPasswd.Size = New System.Drawing.Size(53, 13)
        Me.lblPasswd.TabIndex = 14
        Me.lblPasswd.Text = "Password"
        '
        'txtUserName
        '
        Me.txtUserName.Location = New System.Drawing.Point(123, 156)
        Me.txtUserName.Name = "txtUserName"
        Me.txtUserName.Size = New System.Drawing.Size(270, 20)
        Me.txtUserName.TabIndex = 13
        '
        'lblUserName
        '
        Me.lblUserName.AutoSize = True
        Me.lblUserName.Location = New System.Drawing.Point(54, 159)
        Me.lblUserName.Name = "lblUserName"
        Me.lblUserName.Size = New System.Drawing.Size(60, 13)
        Me.lblUserName.TabIndex = 12
        Me.lblUserName.Text = "User Name"
        '
        'cboDepartMent
        '
        Me.cboDepartMent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDepartMent.FormattingEnabled = True
        Me.cboDepartMent.Location = New System.Drawing.Point(123, 71)
        Me.cboDepartMent.Name = "cboDepartMent"
        Me.cboDepartMent.Size = New System.Drawing.Size(270, 21)
        Me.cboDepartMent.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(60, 128)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(54, 13)
        Me.Label6.TabIndex = 10
        Me.Label6.Text = "Full Name"
        '
        'cbGroupIndex
        '
        Me.cbGroupIndex.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbGroupIndex.FormattingEnabled = True
        Me.cbGroupIndex.Location = New System.Drawing.Point(123, 43)
        Me.cbGroupIndex.Name = "cbGroupIndex"
        Me.cbGroupIndex.Size = New System.Drawing.Size(270, 21)
        Me.cbGroupIndex.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(64, 19)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(50, 13)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "User ID"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(82, 74)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(33, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Dept."
        '
        'txtFullName
        '
        Me.txtFullName.Location = New System.Drawing.Point(123, 125)
        Me.txtFullName.MaxLength = 50
        Me.txtFullName.Name = "txtFullName"
        Me.txtFullName.Size = New System.Drawing.Size(270, 20)
        Me.txtFullName.TabIndex = 11
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(55, 48)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(61, 13)
        Me.Label8.TabIndex = 2
        Me.Label8.Text = "User Group"
        '
        'btnExit
        '
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(464, 516)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "    Close"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(6, 428)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "    Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(559, 498)
        Me.TabControl1.TabIndex = 3
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.GroupBox1)
        Me.TabPage1.Controls.Add(Me.btnSave)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(551, 472)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "User Information"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.chkSelectAll)
        Me.TabPage2.Controls.Add(Me.Button1)
        Me.TabPage2.Controls.Add(Me.grdConfigItem)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(551, 472)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Customer"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(48, 10)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(15, 14)
        Me.chkSelectAll.TabIndex = 14
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'Button1
        '
        Me.Button1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(6, 428)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(106, 38)
        Me.Button1.TabIndex = 13
        Me.Button1.Text = "     Save"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'grdConfigItem
        '
        Me.grdConfigItem.AllowUserToAddRows = False
        Me.grdConfigItem.AllowUserToDeleteRows = False
        Me.grdConfigItem.AllowUserToResizeRows = False
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdConfigItem.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grdConfigItem.BackgroundColor = System.Drawing.Color.White
        Me.grdConfigItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdConfigItem.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Index, Me.col_Customer_Index, Me.Col_IsUse, Me.col_IsDefault, Me.col_Customer_ID, Me.Co_customer})
        Me.grdConfigItem.Location = New System.Drawing.Point(6, 6)
        Me.grdConfigItem.MultiSelect = False
        Me.grdConfigItem.Name = "grdConfigItem"
        Me.grdConfigItem.RowHeadersVisible = False
        Me.grdConfigItem.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdConfigItem.Size = New System.Drawing.Size(539, 416)
        Me.grdConfigItem.TabIndex = 5
        '
        'col_Index
        '
        Me.col_Index.DataPropertyName = "config_UserByCustomer_Index"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.col_Index.DefaultCellStyle = DataGridViewCellStyle6
        Me.col_Index.HeaderText = "config_UserByCustomer_Index"
        Me.col_Index.Name = "col_Index"
        Me.col_Index.ReadOnly = True
        Me.col_Index.Visible = False
        Me.col_Index.Width = 150
        '
        'col_Customer_Index
        '
        Me.col_Customer_Index.DataPropertyName = "Customer_Index"
        Me.col_Customer_Index.HeaderText = "Customer_Index"
        Me.col_Customer_Index.Name = "col_Customer_Index"
        Me.col_Customer_Index.ReadOnly = True
        Me.col_Customer_Index.Visible = False
        '
        'Col_IsUse
        '
        Me.Col_IsUse.DataPropertyName = "IsUse"
        Me.Col_IsUse.HeaderText = "Show"
        Me.Col_IsUse.Name = "Col_IsUse"
        Me.Col_IsUse.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.Col_IsUse.Width = 60
        '
        'col_IsDefault
        '
        Me.col_IsDefault.DataPropertyName = "IsDefault"
        Me.col_IsDefault.HeaderText = "Default"
        Me.col_IsDefault.Name = "col_IsDefault"
        Me.col_IsDefault.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.col_IsDefault.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.col_IsDefault.Width = 50
        '
        'col_Customer_ID
        '
        Me.col_Customer_ID.DataPropertyName = "Customer_ID"
        Me.col_Customer_ID.HeaderText = "Customer Code"
        Me.col_Customer_ID.Name = "col_Customer_ID"
        Me.col_Customer_ID.ReadOnly = True
        Me.col_Customer_ID.Width = 150
        '
        'Co_customer
        '
        Me.Co_customer.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Co_customer.DataPropertyName = "Customer_Name"
        Me.Co_customer.HeaderText = "Customer Name"
        Me.Co_customer.Name = "Co_customer"
        '
        'btnRemovePic
        '
        Me.btnRemovePic.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ลบรายการ
        Me.btnRemovePic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnRemovePic.Location = New System.Drawing.Point(471, 143)
        Me.btnRemovePic.Name = "btnRemovePic"
        Me.btnRemovePic.Size = New System.Drawing.Size(62, 33)
        Me.btnRemovePic.TabIndex = 58
        Me.btnRemovePic.Text = "ลบรูป"
        Me.btnRemovePic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnRemovePic.UseVisualStyleBackColor = True
        '
        'btnAddPic
        '
        Me.btnAddPic.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAddPic.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddPic.Location = New System.Drawing.Point(408, 143)
        Me.btnAddPic.Name = "btnAddPic"
        Me.btnAddPic.Size = New System.Drawing.Size(62, 33)
        Me.btnAddPic.TabIndex = 57
        Me.btnAddPic.Text = "เพิ่มรูป"
        Me.btnAddPic.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddPic.UseVisualStyleBackColor = True
        '
        'picEmployee
        '
        Me.picEmployee.BackColor = System.Drawing.Color.White
        Me.picEmployee.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.picEmployee.Location = New System.Drawing.Point(408, 17)
        Me.picEmployee.Name = "picEmployee"
        Me.picEmployee.Size = New System.Drawing.Size(125, 124)
        Me.picEmployee.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picEmployee.TabIndex = 59
        Me.picEmployee.TabStop = False
        '
        'OpenFileDialog1
        '
        Me.OpenFileDialog1.FileName = "OpenFileDialog1"
        '
        'frmUserManagement_Encode
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(577, 566)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnExit)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmUserManagement_Encode"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "User Management"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        CType(Me.grdConfigItem, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picEmployee, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cbGroupIndex As System.Windows.Forms.ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtFullName As System.Windows.Forms.TextBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cboDepartMent As System.Windows.Forms.ComboBox
    Friend WithEvents btnResetPassword As System.Windows.Forms.Button
    Friend WithEvents txtPasswdConfirm As System.Windows.Forms.TextBox
    Friend WithEvents lblPasswordConfirm As System.Windows.Forms.Label
    Friend WithEvents txtPasswd As System.Windows.Forms.TextBox
    Friend WithEvents lblPasswd As System.Windows.Forms.Label
    Friend WithEvents txtUserName As System.Windows.Forms.TextBox
    Friend WithEvents lblUserName As System.Windows.Forms.Label
    Friend WithEvents txtUserID As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents grdConfigItem As System.Windows.Forms.DataGridView
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents col_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Index As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_IsUse As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_IsDefault As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_Customer_ID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Co_customer As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblDistributionCenter As System.Windows.Forms.Label
    Friend WithEvents cboDistributionCenter As System.Windows.Forms.ComboBox
    Friend WithEvents btnRemovePic As System.Windows.Forms.Button
    Friend WithEvents btnAddPic As System.Windows.Forms.Button
    Friend WithEvents picEmployee As System.Windows.Forms.PictureBox
    Friend WithEvents OpenFileDialog1 As System.Windows.Forms.OpenFileDialog
End Class
