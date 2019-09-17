<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBackup_Restore_DB
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
        Me.cmbDataBase1 = New System.Windows.Forms.ComboBox
        Me.cmbServerName1 = New System.Windows.Forms.ComboBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.cmbAuthe1 = New System.Windows.Forms.ComboBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.txtPassword1 = New System.Windows.Forms.TextBox
        Me.txtUserName1 = New System.Windows.Forms.TextBox
        Me.btnBackUp = New System.Windows.Forms.Button
        Me.btnRestore = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtPassword_HCB = New System.Windows.Forms.TextBox
        Me.txtUserName_HCB = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.cbServerName_HCB = New System.Windows.Forms.TextBox
        Me.cbAuthentication_HCB = New System.Windows.Forms.TextBox
        Me.cbDatabase_HCB = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'cmbDataBase1
        '
        Me.cmbDataBase1.FormattingEnabled = True
        Me.cmbDataBase1.Location = New System.Drawing.Point(470, 134)
        Me.cmbDataBase1.Name = "cmbDataBase1"
        Me.cmbDataBase1.Size = New System.Drawing.Size(197, 21)
        Me.cmbDataBase1.TabIndex = 13
        '
        'cmbServerName1
        '
        Me.cmbServerName1.FormattingEnabled = True
        Me.cmbServerName1.Location = New System.Drawing.Point(470, 28)
        Me.cmbServerName1.Name = "cmbServerName1"
        Me.cmbServerName1.Size = New System.Drawing.Size(197, 21)
        Me.cmbServerName1.TabIndex = 12
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(400, 137)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(60, 13)
        Me.Label10.TabIndex = 11
        Me.Label10.Text = "DataBase :"
        '
        'cmbAuthe1
        '
        Me.cmbAuthe1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbAuthe1.FormattingEnabled = True
        Me.cmbAuthe1.Items.AddRange(New Object() {"Windows Authentications", "Sql Server Authentications"})
        Me.cmbAuthe1.Location = New System.Drawing.Point(470, 55)
        Me.cmbAuthe1.Name = "cmbAuthe1"
        Me.cmbAuthe1.Size = New System.Drawing.Size(197, 21)
        Me.cmbAuthe1.TabIndex = 2
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(401, 111)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(59, 13)
        Me.Label12.TabIndex = 8
        Me.Label12.Text = "Password :"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Location = New System.Drawing.Point(394, 82)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 7
        Me.Label13.Text = "User Name :"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Location = New System.Drawing.Point(379, 55)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(81, 13)
        Me.Label14.TabIndex = 6
        Me.Label14.Text = "Authentication :"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Location = New System.Drawing.Point(385, 31)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(75, 13)
        Me.Label15.TabIndex = 5
        Me.Label15.Text = "Server Name :"
        '
        'txtPassword1
        '
        Me.txtPassword1.Location = New System.Drawing.Point(470, 108)
        Me.txtPassword1.Name = "txtPassword1"
        Me.txtPassword1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword1.Size = New System.Drawing.Size(196, 20)
        Me.txtPassword1.TabIndex = 4
        Me.txtPassword1.UseSystemPasswordChar = True
        '
        'txtUserName1
        '
        Me.txtUserName1.Location = New System.Drawing.Point(470, 81)
        Me.txtUserName1.Name = "txtUserName1"
        Me.txtUserName1.Size = New System.Drawing.Size(196, 20)
        Me.txtUserName1.TabIndex = 3
        '
        'btnBackUp
        '
        Me.btnBackUp.Image = Global.WMS_Site_KingStella.My.Resources.Resources.Save

        Me.btnBackUp.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnBackUp.Location = New System.Drawing.Point(33, 169)
        Me.btnBackUp.Name = "btnBackUp"
        Me.btnBackUp.Size = New System.Drawing.Size(75, 51)
        Me.btnBackUp.TabIndex = 18
        Me.btnBackUp.Text = "Back up"
        Me.btnBackUp.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBackUp.UseVisualStyleBackColor = True
        '
        'btnRestore
        '
        Me.btnRestore.Image = Global.WMS_Site_KingStella.My.Resources.Resources.ล้างหน้าจอ1
        Me.btnRestore.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnRestore.Location = New System.Drawing.Point(114, 169)
        Me.btnRestore.Name = "btnRestore"
        Me.btnRestore.Size = New System.Drawing.Size(75, 51)
        Me.btnRestore.TabIndex = 19
        Me.btnRestore.Text = "Restore"
        Me.btnRestore.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnRestore.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Image = Global.WMS_Site_KingStella.My.Resources.Resources.ลบรายการ
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.btnCancel.Location = New System.Drawing.Point(195, 169)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 51)
        Me.btnCancel.TabIndex = 20
        Me.btnCancel.Text = "Exit"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(43, 137)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(60, 13)
        Me.Label1.TabIndex = 11
        Me.Label1.Text = "DataBase :"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(44, 111)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 8
        Me.Label2.Text = "Password :"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(37, 82)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(66, 13)
        Me.Label3.TabIndex = 7
        Me.Label3.Text = "User Name :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(22, 55)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(81, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Authentication :"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(28, 31)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Server Name :"
        '
        'txtPassword_HCB
        '
        Me.txtPassword_HCB.BackColor = System.Drawing.Color.White
        Me.txtPassword_HCB.Location = New System.Drawing.Point(113, 108)
        Me.txtPassword_HCB.Name = "txtPassword_HCB"
        Me.txtPassword_HCB.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword_HCB.ReadOnly = True
        Me.txtPassword_HCB.Size = New System.Drawing.Size(196, 20)
        Me.txtPassword_HCB.TabIndex = 4
        Me.txtPassword_HCB.UseSystemPasswordChar = True
        '
        'txtUserName_HCB
        '
        Me.txtUserName_HCB.BackColor = System.Drawing.Color.White
        Me.txtUserName_HCB.Location = New System.Drawing.Point(113, 81)
        Me.txtUserName_HCB.Name = "txtUserName_HCB"
        Me.txtUserName_HCB.ReadOnly = True
        Me.txtUserName_HCB.Size = New System.Drawing.Size(196, 20)
        Me.txtUserName_HCB.TabIndex = 3
        '
        'Button1
        '
        Me.Button1.Image = Global.WMS_Site_KingStella.My.Resources.Resources.แก้ไขรายการ
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.TopCenter
        Me.Button1.Location = New System.Drawing.Point(276, 169)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(87, 50)
        Me.Button1.TabIndex = 22
        Me.Button1.Text = "EditServer  ->>"
        Me.Button1.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.Button1.UseVisualStyleBackColor = True
        '
        'cbServerName_HCB
        '
        Me.cbServerName_HCB.BackColor = System.Drawing.Color.White
        Me.cbServerName_HCB.Location = New System.Drawing.Point(113, 28)
        Me.cbServerName_HCB.Name = "cbServerName_HCB"
        Me.cbServerName_HCB.ReadOnly = True
        Me.cbServerName_HCB.Size = New System.Drawing.Size(196, 20)
        Me.cbServerName_HCB.TabIndex = 23
        '
        'cbAuthentication_HCB
        '
        Me.cbAuthentication_HCB.BackColor = System.Drawing.Color.White
        Me.cbAuthentication_HCB.Location = New System.Drawing.Point(113, 55)
        Me.cbAuthentication_HCB.Name = "cbAuthentication_HCB"
        Me.cbAuthentication_HCB.ReadOnly = True
        Me.cbAuthentication_HCB.Size = New System.Drawing.Size(196, 20)
        Me.cbAuthentication_HCB.TabIndex = 24
        Me.cbAuthentication_HCB.Text = "Sql Server Authentications"
        '
        'cbDatabase_HCB
        '
        Me.cbDatabase_HCB.BackColor = System.Drawing.Color.White
        Me.cbDatabase_HCB.Location = New System.Drawing.Point(113, 134)
        Me.cbDatabase_HCB.Name = "cbDatabase_HCB"
        Me.cbDatabase_HCB.ReadOnly = True
        Me.cbDatabase_HCB.Size = New System.Drawing.Size(196, 20)
        Me.cbDatabase_HCB.TabIndex = 25
        '
        'frmBackup_Restore_DB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(376, 241)
        Me.Controls.Add(Me.cbDatabase_HCB)
        Me.Controls.Add(Me.cbAuthentication_HCB)
        Me.Controls.Add(Me.cbServerName_HCB)
        Me.Controls.Add(Me.cmbDataBase1)
        Me.Controls.Add(Me.cmbServerName1)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.cmbAuthe1)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.btnRestore)
        Me.Controls.Add(Me.txtPassword1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUserName1)
        Me.Controls.Add(Me.btnBackUp)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtUserName_HCB)
        Me.Controls.Add(Me.txtPassword_HCB)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmBackup_Restore_DB"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BackUpAndRestoreSQL"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents cmbDataBase1 As System.Windows.Forms.ComboBox
    Friend WithEvents cmbServerName1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents cmbAuthe1 As System.Windows.Forms.ComboBox
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents txtPassword1 As System.Windows.Forms.TextBox
    Public WithEvents txtUserName1 As System.Windows.Forms.TextBox
    Friend WithEvents btnBackUp As System.Windows.Forms.Button
    Friend WithEvents btnRestore As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtPassword_HCB As System.Windows.Forms.TextBox
    Public WithEvents txtUserName_HCB As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Public WithEvents cbServerName_HCB As System.Windows.Forms.TextBox
    Public WithEvents cbAuthentication_HCB As System.Windows.Forms.TextBox
    Public WithEvents cbDatabase_HCB As System.Windows.Forms.TextBox

End Class
