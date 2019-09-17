<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditSOItem
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.lblOrg_Total_Qty = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTotal_Qty = New System.Windows.Forms.TextBox
        Me.txtTotal_Qty_Withdraw = New System.Windows.Forms.TextBox
        Me.txtTotal_Qty_Plan = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(12, 125)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(102, 33)
        Me.btnSave.TabIndex = 15
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(182, 125)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(102, 33)
        Me.btnExit.TabIndex = 16
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.lblOrg_Total_Qty)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.txtTotal_Qty)
        Me.GroupBox1.Controls.Add(Me.txtTotal_Qty_Withdraw)
        Me.GroupBox1.Controls.Add(Me.txtTotal_Qty_Plan)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(272, 117)
        Me.GroupBox1.TabIndex = 17
        Me.GroupBox1.TabStop = False
        '
        'lblOrg_Total_Qty
        '
        Me.lblOrg_Total_Qty.AutoSize = True
        Me.lblOrg_Total_Qty.Location = New System.Drawing.Point(166, 65)
        Me.lblOrg_Total_Qty.Name = "lblOrg_Total_Qty"
        Me.lblOrg_Total_Qty.Size = New System.Drawing.Size(37, 13)
        Me.lblOrg_Total_Qty.TabIndex = 7
        Me.lblOrg_Total_Qty.Text = "00000"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(101, 64)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(65, 13)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Orginal Qty :"
        '
        'txtTotal_Qty
        '
        Me.txtTotal_Qty.Location = New System.Drawing.Point(93, 80)
        Me.txtTotal_Qty.Name = "txtTotal_Qty"
        Me.txtTotal_Qty.Size = New System.Drawing.Size(125, 20)
        Me.txtTotal_Qty.TabIndex = 5
        Me.txtTotal_Qty.Text = "0"
        '
        'txtTotal_Qty_Withdraw
        '
        Me.txtTotal_Qty_Withdraw.Location = New System.Drawing.Point(93, 41)
        Me.txtTotal_Qty_Withdraw.Name = "txtTotal_Qty_Withdraw"
        Me.txtTotal_Qty_Withdraw.ReadOnly = True
        Me.txtTotal_Qty_Withdraw.Size = New System.Drawing.Size(125, 20)
        Me.txtTotal_Qty_Withdraw.TabIndex = 4
        Me.txtTotal_Qty_Withdraw.Text = "0"
        '
        'txtTotal_Qty_Plan
        '
        Me.txtTotal_Qty_Plan.Location = New System.Drawing.Point(93, 18)
        Me.txtTotal_Qty_Plan.Name = "txtTotal_Qty_Plan"
        Me.txtTotal_Qty_Plan.ReadOnly = True
        Me.txtTotal_Qty_Plan.Size = New System.Drawing.Size(125, 20)
        Me.txtTotal_Qty_Plan.TabIndex = 3
        Me.txtTotal_Qty_Plan.Text = "0"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(53, 83)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(34, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "แก้ไข"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(41, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(46, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "เบิกแล้ว"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(47, 21)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(40, 13)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "จำนวน"
        '
        'frmEditSOItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(296, 170)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(302, 170)
        Me.Name = "frmEditSOItem"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "EditSOItem"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents txtTotal_Qty As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal_Qty_Withdraw As System.Windows.Forms.TextBox
    Friend WithEvents txtTotal_Qty_Plan As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblOrg_Total_Qty As System.Windows.Forms.Label
End Class
