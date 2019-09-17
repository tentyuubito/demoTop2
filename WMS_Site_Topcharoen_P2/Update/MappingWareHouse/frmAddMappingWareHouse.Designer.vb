<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddMappingWareHouse
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
        Me.btnSave = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.txtWareHouseID = New System.Windows.Forms.TextBox
        Me.txtWareHouseName = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboDtisributionCenter = New System.Windows.Forms.ComboBox
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(6, 159)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 38)
        Me.btnSave.TabIndex = 0
        Me.btnSave.Text = "      บันทึก"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(61, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "รหัสคลัง ST"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 61)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(55, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "ชื่อคลัง ST"
        '
        'txtWareHouseID
        '
        Me.txtWareHouseID.Location = New System.Drawing.Point(92, 31)
        Me.txtWareHouseID.Name = "txtWareHouseID"
        Me.txtWareHouseID.Size = New System.Drawing.Size(100, 20)
        Me.txtWareHouseID.TabIndex = 3
        '
        'txtWareHouseName
        '
        Me.txtWareHouseName.Location = New System.Drawing.Point(92, 54)
        Me.txtWareHouseName.Multiline = True
        Me.txtWareHouseName.Name = "txtWareHouseName"
        Me.txtWareHouseName.Size = New System.Drawing.Size(255, 54)
        Me.txtWareHouseName.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 120)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(96, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "ศุนย์กระจาย WMS"
        '
        'cboDtisributionCenter
        '
        Me.cboDtisributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDtisributionCenter.FormattingEnabled = True
        Me.cboDtisributionCenter.Location = New System.Drawing.Point(113, 116)
        Me.cboDtisributionCenter.Name = "cboDtisributionCenter"
        Me.cboDtisributionCenter.Size = New System.Drawing.Size(114, 21)
        Me.cboDtisributionCenter.TabIndex = 6
        '
        'btnClose
        '
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(281, 159)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 38)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "     ปิด"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(350, 141)
        Me.GroupBox1.TabIndex = 8
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "เพิ่มข้อมูล"
        '
        'frmAddMappingWareHouse
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 210)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.cboDtisributionCenter)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtWareHouseName)
        Me.Controls.Add(Me.txtWareHouseID)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "frmAddMappingWareHouse"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เพิ่ม"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtWareHouseID As System.Windows.Forms.TextBox
    Friend WithEvents txtWareHouseName As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboDtisributionCenter As System.Windows.Forms.ComboBox
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
End Class
