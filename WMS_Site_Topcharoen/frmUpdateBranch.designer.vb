<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateBranch
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtBranchDB = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtWindowsVersion = New System.Windows.Forms.TextBox
        Me.txtMobileVersion = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtBranchName = New System.Windows.Forms.TextBox
        Me.Label7 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.Label9 = New System.Windows.Forms.Label
        Me.txtShortCutFile = New System.Windows.Forms.TextBox
        Me.txtUpdateFromPath = New System.Windows.Forms.TextBox
        Me.txtShortCutName = New System.Windows.Forms.TextBox
        Me.Button3 = New System.Windows.Forms.Button
        Me.txtSourceAppConfig = New System.Windows.Forms.TextBox
        Me.chbReadyToUpdate = New System.Windows.Forms.CheckBox
        Me.lblAppVersion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(20, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(65, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Branch DB :"
        '
        'txtBranchDB
        '
        Me.txtBranchDB.Location = New System.Drawing.Point(110, 103)
        Me.txtBranchDB.Multiline = True
        Me.txtBranchDB.Name = "txtBranchDB"
        Me.txtBranchDB.Size = New System.Drawing.Size(322, 51)
        Me.txtBranchDB.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(19, 164)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(92, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Windows Version:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(19, 189)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(79, 13)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Mobile Version:"
        '
        'txtWindowsVersion
        '
        Me.txtWindowsVersion.Location = New System.Drawing.Point(110, 161)
        Me.txtWindowsVersion.Name = "txtWindowsVersion"
        Me.txtWindowsVersion.Size = New System.Drawing.Size(100, 20)
        Me.txtWindowsVersion.TabIndex = 4
        '
        'txtMobileVersion
        '
        Me.txtMobileVersion.Location = New System.Drawing.Point(110, 186)
        Me.txtMobileVersion.Name = "txtMobileVersion"
        Me.txtMobileVersion.Size = New System.Drawing.Size(100, 20)
        Me.txtMobileVersion.TabIndex = 5
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(214, 160)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(44, 22)
        Me.Button1.TabIndex = 6
        Me.Button1.Text = "CV"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(276, 317)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 7
        Me.Button2.Text = "Update"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(20, 81)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(75, 13)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Branch Name:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(19, 25)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(66, 13)
        Me.Label5.TabIndex = 9
        Me.Label5.Text = "Source App:"
        '
        'txtBranchName
        '
        Me.txtBranchName.Location = New System.Drawing.Point(110, 78)
        Me.txtBranchName.Name = "txtBranchName"
        Me.txtBranchName.Size = New System.Drawing.Size(322, 20)
        Me.txtBranchName.TabIndex = 11
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(19, 222)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(90, 13)
        Me.Label7.TabIndex = 12
        Me.Label7.Text = "UpdateFormPath:"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(19, 246)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(79, 13)
        Me.Label8.TabIndex = 13
        Me.Label8.Text = "ShortCutName:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(19, 270)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(67, 13)
        Me.Label9.TabIndex = 14
        Me.Label9.Text = "ShortCutFile:"
        '
        'txtShortCutFile
        '
        Me.txtShortCutFile.Location = New System.Drawing.Point(110, 267)
        Me.txtShortCutFile.Name = "txtShortCutFile"
        Me.txtShortCutFile.Size = New System.Drawing.Size(322, 20)
        Me.txtShortCutFile.TabIndex = 15
        '
        'txtUpdateFromPath
        '
        Me.txtUpdateFromPath.Location = New System.Drawing.Point(110, 219)
        Me.txtUpdateFromPath.Name = "txtUpdateFromPath"
        Me.txtUpdateFromPath.Size = New System.Drawing.Size(322, 20)
        Me.txtUpdateFromPath.TabIndex = 15
        '
        'txtShortCutName
        '
        Me.txtShortCutName.Location = New System.Drawing.Point(110, 243)
        Me.txtShortCutName.Name = "txtShortCutName"
        Me.txtShortCutName.Size = New System.Drawing.Size(322, 20)
        Me.txtShortCutName.TabIndex = 15
        '
        'Button3
        '
        Me.Button3.Location = New System.Drawing.Point(357, 317)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(75, 23)
        Me.Button3.TabIndex = 16
        Me.Button3.Text = "Exit"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'txtSourceAppConfig
        '
        Me.txtSourceAppConfig.Location = New System.Drawing.Point(110, 25)
        Me.txtSourceAppConfig.Multiline = True
        Me.txtSourceAppConfig.Name = "txtSourceAppConfig"
        Me.txtSourceAppConfig.ReadOnly = True
        Me.txtSourceAppConfig.Size = New System.Drawing.Size(322, 51)
        Me.txtSourceAppConfig.TabIndex = 17
        '
        'chbReadyToUpdate
        '
        Me.chbReadyToUpdate.AutoSize = True
        Me.chbReadyToUpdate.Location = New System.Drawing.Point(110, 294)
        Me.chbReadyToUpdate.Name = "chbReadyToUpdate"
        Me.chbReadyToUpdate.Size = New System.Drawing.Size(105, 17)
        Me.chbReadyToUpdate.TabIndex = 18
        Me.chbReadyToUpdate.Text = "ReadyToUpdate"
        Me.chbReadyToUpdate.UseVisualStyleBackColor = True
        '
        'lblAppVersion
        '
        Me.lblAppVersion.AutoSize = True
        Me.lblAppVersion.Location = New System.Drawing.Point(264, 164)
        Me.lblAppVersion.Name = "lblAppVersion"
        Me.lblAppVersion.Size = New System.Drawing.Size(86, 13)
        Me.lblAppVersion.TabIndex = 19
        Me.lblAppVersion.Text = "WindowsVersion"
        '
        'frmUpdateBranch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(481, 352)
        Me.Controls.Add(Me.lblAppVersion)
        Me.Controls.Add(Me.chbReadyToUpdate)
        Me.Controls.Add(Me.txtSourceAppConfig)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.txtShortCutName)
        Me.Controls.Add(Me.txtUpdateFromPath)
        Me.Controls.Add(Me.txtShortCutFile)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtBranchName)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.txtMobileVersion)
        Me.Controls.Add(Me.txtWindowsVersion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtBranchDB)
        Me.Controls.Add(Me.Label1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmUpdateBranch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "admin"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtBranchDB As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtWindowsVersion As System.Windows.Forms.TextBox
    Friend WithEvents txtMobileVersion As System.Windows.Forms.TextBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtBranchName As System.Windows.Forms.TextBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents txtShortCutFile As System.Windows.Forms.TextBox
    Friend WithEvents txtUpdateFromPath As System.Windows.Forms.TextBox
    Friend WithEvents txtShortCutName As System.Windows.Forms.TextBox
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents txtSourceAppConfig As System.Windows.Forms.TextBox
    Friend WithEvents chbReadyToUpdate As System.Windows.Forms.CheckBox
    Friend WithEvents lblAppVersion As System.Windows.Forms.Label
End Class
