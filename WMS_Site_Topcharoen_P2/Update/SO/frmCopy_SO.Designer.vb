<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCopy_SO
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCopy_SO))
        Me.radCopy_Red = New System.Windows.Forms.RadioButton
        Me.radCopy_AllMaster = New System.Windows.Forms.RadioButton
        Me.btn_select = New System.Windows.Forms.Button
        Me.btn_cancel = New System.Windows.Forms.Button
        Me.radCopy_All = New System.Windows.Forms.RadioButton
        Me.radCopy_RedNonStock = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'radCopy_Red
        '
        Me.radCopy_Red.AutoSize = True
        Me.radCopy_Red.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.radCopy_Red.Location = New System.Drawing.Point(35, 44)
        Me.radCopy_Red.Name = "radCopy_Red"
        Me.radCopy_Red.Size = New System.Drawing.Size(313, 17)
        Me.radCopy_Red.TabIndex = 0
        Me.radCopy_Red.Text = "รายการแดงทั้งหมด ลบรายการเก่าและคำนวณต้นฉบับ"
        Me.radCopy_Red.UseVisualStyleBackColor = True
        '
        'radCopy_AllMaster
        '
        Me.radCopy_AllMaster.AutoSize = True
        Me.radCopy_AllMaster.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.radCopy_AllMaster.Location = New System.Drawing.Point(35, 69)
        Me.radCopy_AllMaster.Name = "radCopy_AllMaster"
        Me.radCopy_AllMaster.Size = New System.Drawing.Size(283, 17)
        Me.radCopy_AllMaster.TabIndex = 1
        Me.radCopy_AllMaster.Text = "ทุกรายการ ไม่ลบรายการเก่าและคำนวณต้นฉบับ"
        Me.radCopy_AllMaster.UseVisualStyleBackColor = True
        '
        'btn_select
        '
        Me.btn_select.Image = CType(resources.GetObject("btn_select.Image"), System.Drawing.Image)
        Me.btn_select.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_select.Location = New System.Drawing.Point(15, 142)
        Me.btn_select.Name = "btn_select"
        Me.btn_select.Size = New System.Drawing.Size(100, 38)
        Me.btn_select.TabIndex = 7
        Me.btn_select.Text = "เลือก"
        Me.btn_select.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_select.UseVisualStyleBackColor = True
        '
        'btn_cancel
        '
        Me.btn_cancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_cancel.Location = New System.Drawing.Point(310, 142)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(100, 38)
        Me.btn_cancel.TabIndex = 8
        Me.btn_cancel.Text = "ออก"
        Me.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'radCopy_All
        '
        Me.radCopy_All.AutoSize = True
        Me.radCopy_All.Location = New System.Drawing.Point(35, 92)
        Me.radCopy_All.Name = "radCopy_All"
        Me.radCopy_All.Size = New System.Drawing.Size(75, 17)
        Me.radCopy_All.TabIndex = 9
        Me.radCopy_All.Text = "ทุกรายการ"
        Me.radCopy_All.UseVisualStyleBackColor = True
        '
        'radCopy_RedNonStock
        '
        Me.radCopy_RedNonStock.AutoSize = True
        Me.radCopy_RedNonStock.Checked = True
        Me.radCopy_RedNonStock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.radCopy_RedNonStock.Location = New System.Drawing.Point(35, 17)
        Me.radCopy_RedNonStock.Name = "radCopy_RedNonStock"
        Me.radCopy_RedNonStock.Size = New System.Drawing.Size(375, 17)
        Me.radCopy_RedNonStock.TabIndex = 10
        Me.radCopy_RedNonStock.TabStop = True
        Me.radCopy_RedNonStock.Text = "รายการแดงทั้งหมดยอดที่ไม่พอ ลบรายการเก่าและคำนวณต้นฉบับ"
        Me.radCopy_RedNonStock.UseVisualStyleBackColor = True
        '
        'frmCopy_SO
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 192)
        Me.Controls.Add(Me.radCopy_RedNonStock)
        Me.Controls.Add(Me.radCopy_All)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_select)
        Me.Controls.Add(Me.radCopy_AllMaster)
        Me.Controls.Add(Me.radCopy_Red)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmCopy_SO"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy document"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_select As System.Windows.Forms.Button
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Public WithEvents radCopy_Red As System.Windows.Forms.RadioButton
    Public WithEvents radCopy_AllMaster As System.Windows.Forms.RadioButton
    Public WithEvents radCopy_All As System.Windows.Forms.RadioButton
    Public WithEvents radCopy_RedNonStock As System.Windows.Forms.RadioButton
End Class
