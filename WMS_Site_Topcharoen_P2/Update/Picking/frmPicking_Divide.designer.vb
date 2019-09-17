<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPicking_Item
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
        Me.txtOrder_No = New System.Windows.Forms.TextBox
        Me.txtSku_Id = New System.Windows.Forms.TextBox
        Me.txtProductName = New System.Windows.Forms.TextBox
        Me.txtQtyBalance = New System.Windows.Forms.TextBox
        Me.txtQtyOut = New System.Windows.Forms.TextBox
        Me.txtWeightOut = New System.Windows.Forms.TextBox
        Me.lblOrder_No = New System.Windows.Forms.Label
        Me.lblSku_Id = New System.Windows.Forms.Label
        Me.lblProductName = New System.Windows.Forms.Label
        Me.lblQtyOut = New System.Windows.Forms.Label
        Me.lblWeightOut = New System.Windows.Forms.Label
        Me.txtVolumeOut = New System.Windows.Forms.TextBox
        Me.lblVolumeOut = New System.Windows.Forms.Label
        Me.btn_cancel = New System.Windows.Forms.Button
        Me.btn_select = New System.Windows.Forms.Button
        Me.txtWeight_Bal = New System.Windows.Forms.TextBox
        Me.txtVolume_Bal = New System.Windows.Forms.TextBox
        Me.lblQtyAllOut = New System.Windows.Forms.Label
        Me.txtQtyAllOut = New System.Windows.Forms.TextBox
        Me.txtQtyAll_Bal = New System.Windows.Forms.TextBox
        Me.lblPerQty = New System.Windows.Forms.Label
        Me.lblPerVolume = New System.Windows.Forms.Label
        Me.lblPerWeight = New System.Windows.Forms.Label
        Me.txtPerWeight = New System.Windows.Forms.TextBox
        Me.txtPerVolume = New System.Windows.Forms.TextBox
        Me.txtPerQty = New System.Windows.Forms.TextBox
        Me.grbWithDraw = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtPrice = New System.Windows.Forms.TextBox
        Me.txtQtyAll = New System.Windows.Forms.TextBox
        Me.txtVolume = New System.Windows.Forms.TextBox
        Me.txtWeight = New System.Windows.Forms.TextBox
        Me.txtQty = New System.Windows.Forms.TextBox
        Me.lblPriceOut = New System.Windows.Forms.Label
        Me.txtPriceOut = New System.Windows.Forms.TextBox
        Me.txtPriceBall = New System.Windows.Forms.TextBox
        Me.grbWithDraw.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtOrder_No
        '
        Me.txtOrder_No.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtOrder_No.Location = New System.Drawing.Point(100, 6)
        Me.txtOrder_No.Name = "txtOrder_No"
        Me.txtOrder_No.ReadOnly = True
        Me.txtOrder_No.Size = New System.Drawing.Size(138, 20)
        Me.txtOrder_No.TabIndex = 1
        '
        'txtSku_Id
        '
        Me.txtSku_Id.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtSku_Id.Location = New System.Drawing.Point(100, 28)
        Me.txtSku_Id.Name = "txtSku_Id"
        Me.txtSku_Id.ReadOnly = True
        Me.txtSku_Id.Size = New System.Drawing.Size(138, 20)
        Me.txtSku_Id.TabIndex = 3
        '
        'txtProductName
        '
        Me.txtProductName.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtProductName.Location = New System.Drawing.Point(100, 51)
        Me.txtProductName.Name = "txtProductName"
        Me.txtProductName.ReadOnly = True
        Me.txtProductName.Size = New System.Drawing.Size(213, 20)
        Me.txtProductName.TabIndex = 5
        '
        'txtQtyBalance
        '
        Me.txtQtyBalance.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtQtyBalance.Location = New System.Drawing.Point(321, 47)
        Me.txtQtyBalance.Multiline = True
        Me.txtQtyBalance.Name = "txtQtyBalance"
        Me.txtQtyBalance.ReadOnly = True
        Me.txtQtyBalance.Size = New System.Drawing.Size(101, 20)
        Me.txtQtyBalance.TabIndex = 9
        Me.txtQtyBalance.Text = "0"
        '
        'txtQtyOut
        '
        Me.txtQtyOut.Location = New System.Drawing.Point(223, 47)
        Me.txtQtyOut.Name = "txtQtyOut"
        Me.txtQtyOut.Size = New System.Drawing.Size(92, 20)
        Me.txtQtyOut.TabIndex = 1
        Me.txtQtyOut.Text = "0"
        '
        'txtWeightOut
        '
        Me.txtWeightOut.Location = New System.Drawing.Point(223, 70)
        Me.txtWeightOut.Name = "txtWeightOut"
        Me.txtWeightOut.Size = New System.Drawing.Size(92, 20)
        Me.txtWeightOut.TabIndex = 3
        Me.txtWeightOut.Text = "0"
        '
        'lblOrder_No
        '
        Me.lblOrder_No.AutoSize = True
        Me.lblOrder_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblOrder_No.Location = New System.Drawing.Point(15, 9)
        Me.lblOrder_No.Name = "lblOrder_No"
        Me.lblOrder_No.Size = New System.Drawing.Size(83, 13)
        Me.lblOrder_No.TabIndex = 0
        Me.lblOrder_No.Text = "เลขที่ใบรับสินค้า"
        '
        'lblSku_Id
        '
        Me.lblSku_Id.AutoSize = True
        Me.lblSku_Id.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSku_Id.Location = New System.Drawing.Point(46, 31)
        Me.lblSku_Id.Name = "lblSku_Id"
        Me.lblSku_Id.Size = New System.Drawing.Size(52, 13)
        Me.lblSku_Id.TabIndex = 2
        Me.lblSku_Id.Text = "รหัสสินค้า"
        '
        'lblProductName
        '
        Me.lblProductName.AutoSize = True
        Me.lblProductName.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblProductName.Location = New System.Drawing.Point(50, 54)
        Me.lblProductName.Name = "lblProductName"
        Me.lblProductName.Size = New System.Drawing.Size(46, 13)
        Me.lblProductName.TabIndex = 4
        Me.lblProductName.Text = "ชื่อสินค้า"
        '
        'lblQtyOut
        '
        Me.lblQtyOut.AutoSize = True
        Me.lblQtyOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblQtyOut.Location = New System.Drawing.Point(7, 54)
        Me.lblQtyOut.Name = "lblQtyOut"
        Me.lblQtyOut.Size = New System.Drawing.Size(40, 13)
        Me.lblQtyOut.TabIndex = 0
        Me.lblQtyOut.Text = "จำนวน"
        '
        'lblWeightOut
        '
        Me.lblWeightOut.AutoSize = True
        Me.lblWeightOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblWeightOut.Location = New System.Drawing.Point(7, 76)
        Me.lblWeightOut.Name = "lblWeightOut"
        Me.lblWeightOut.Size = New System.Drawing.Size(63, 13)
        Me.lblWeightOut.TabIndex = 2
        Me.lblWeightOut.Text = "น้ำหนัก (kg)"
        '
        'txtVolumeOut
        '
        Me.txtVolumeOut.Location = New System.Drawing.Point(223, 93)
        Me.txtVolumeOut.Name = "txtVolumeOut"
        Me.txtVolumeOut.Size = New System.Drawing.Size(92, 20)
        Me.txtVolumeOut.TabIndex = 5
        Me.txtVolumeOut.Text = "0"
        '
        'lblVolumeOut
        '
        Me.lblVolumeOut.AutoSize = True
        Me.lblVolumeOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblVolumeOut.Location = New System.Drawing.Point(7, 99)
        Me.lblVolumeOut.Name = "lblVolumeOut"
        Me.lblVolumeOut.Size = New System.Drawing.Size(68, 13)
        Me.lblVolumeOut.TabIndex = 4
        Me.lblVolumeOut.Text = "ปริมาตร (m3)"
        '
        'btn_cancel
        '
        Me.btn_cancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ยกเลิกรายการ
        Me.btn_cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_cancel.Location = New System.Drawing.Point(473, 265)
        Me.btn_cancel.Name = "btn_cancel"
        Me.btn_cancel.Size = New System.Drawing.Size(100, 38)
        Me.btn_cancel.TabIndex = 14
        Me.btn_cancel.Text = "ยกเลิก"
        Me.btn_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_cancel.UseVisualStyleBackColor = True
        '
        'btn_select
        '
        Me.btn_select.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.เลือกรายการ___เลือก
        Me.btn_select.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_select.Location = New System.Drawing.Point(8, 265)
        Me.btn_select.Name = "btn_select"
        Me.btn_select.Size = New System.Drawing.Size(100, 38)
        Me.btn_select.TabIndex = 13
        Me.btn_select.Text = "ตกลง"
        Me.btn_select.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_select.UseVisualStyleBackColor = True
        '
        'txtWeight_Bal
        '
        Me.txtWeight_Bal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtWeight_Bal.Location = New System.Drawing.Point(321, 70)
        Me.txtWeight_Bal.Multiline = True
        Me.txtWeight_Bal.Name = "txtWeight_Bal"
        Me.txtWeight_Bal.ReadOnly = True
        Me.txtWeight_Bal.Size = New System.Drawing.Size(101, 20)
        Me.txtWeight_Bal.TabIndex = 11
        Me.txtWeight_Bal.Text = "0"
        '
        'txtVolume_Bal
        '
        Me.txtVolume_Bal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtVolume_Bal.Location = New System.Drawing.Point(321, 93)
        Me.txtVolume_Bal.Multiline = True
        Me.txtVolume_Bal.Name = "txtVolume_Bal"
        Me.txtVolume_Bal.ReadOnly = True
        Me.txtVolume_Bal.Size = New System.Drawing.Size(101, 20)
        Me.txtVolume_Bal.TabIndex = 13
        Me.txtVolume_Bal.Text = "0"
        '
        'lblQtyAllOut
        '
        Me.lblQtyAllOut.AutoSize = True
        Me.lblQtyAllOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblQtyAllOut.Location = New System.Drawing.Point(7, 119)
        Me.lblQtyAllOut.Name = "lblQtyAllOut"
        Me.lblQtyAllOut.Size = New System.Drawing.Size(63, 13)
        Me.lblQtyAllOut.TabIndex = 6
        Me.lblQtyAllOut.Text = "รายการย่อย"
        '
        'txtQtyAllOut
        '
        Me.txtQtyAllOut.Location = New System.Drawing.Point(223, 116)
        Me.txtQtyAllOut.Name = "txtQtyAllOut"
        Me.txtQtyAllOut.Size = New System.Drawing.Size(92, 20)
        Me.txtQtyAllOut.TabIndex = 7
        Me.txtQtyAllOut.Text = "0"
        '
        'txtQtyAll_Bal
        '
        Me.txtQtyAll_Bal.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtQtyAll_Bal.Location = New System.Drawing.Point(321, 116)
        Me.txtQtyAll_Bal.Multiline = True
        Me.txtQtyAll_Bal.Name = "txtQtyAll_Bal"
        Me.txtQtyAll_Bal.ReadOnly = True
        Me.txtQtyAll_Bal.Size = New System.Drawing.Size(101, 20)
        Me.txtQtyAll_Bal.TabIndex = 15
        Me.txtQtyAll_Bal.Text = "0"
        '
        'lblPerQty
        '
        Me.lblPerQty.AutoSize = True
        Me.lblPerQty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPerQty.Location = New System.Drawing.Point(365, 54)
        Me.lblPerQty.Name = "lblPerQty"
        Me.lblPerQty.Size = New System.Drawing.Size(92, 13)
        Me.lblPerQty.TabIndex = 10
        Me.lblPerQty.Text = "จำนวนย่อย/หีบห่อ"
        '
        'lblPerVolume
        '
        Me.lblPerVolume.AutoSize = True
        Me.lblPerVolume.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPerVolume.Location = New System.Drawing.Point(323, 31)
        Me.lblPerVolume.Name = "lblPerVolume"
        Me.lblPerVolume.Size = New System.Drawing.Size(134, 13)
        Me.lblPerVolume.TabIndex = 8
        Me.lblPerVolume.Text = "ปริมาตร/หน่วย(หีบห่อ) (m3)"
        '
        'lblPerWeight
        '
        Me.lblPerWeight.AutoSize = True
        Me.lblPerWeight.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPerWeight.Location = New System.Drawing.Point(323, 9)
        Me.lblPerWeight.Name = "lblPerWeight"
        Me.lblPerWeight.Size = New System.Drawing.Size(132, 13)
        Me.lblPerWeight.TabIndex = 6
        Me.lblPerWeight.Text = "น้ำหนัก/หน่วย (หีบห่อ) (kg)"
        '
        'txtPerWeight
        '
        Me.txtPerWeight.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPerWeight.Location = New System.Drawing.Point(463, 6)
        Me.txtPerWeight.Name = "txtPerWeight"
        Me.txtPerWeight.ReadOnly = True
        Me.txtPerWeight.Size = New System.Drawing.Size(107, 20)
        Me.txtPerWeight.TabIndex = 7
        Me.txtPerWeight.Text = "0"
        '
        'txtPerVolume
        '
        Me.txtPerVolume.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPerVolume.Location = New System.Drawing.Point(463, 28)
        Me.txtPerVolume.Name = "txtPerVolume"
        Me.txtPerVolume.ReadOnly = True
        Me.txtPerVolume.Size = New System.Drawing.Size(107, 20)
        Me.txtPerVolume.TabIndex = 9
        Me.txtPerVolume.Text = "0"
        '
        'txtPerQty
        '
        Me.txtPerQty.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPerQty.Location = New System.Drawing.Point(463, 51)
        Me.txtPerQty.Name = "txtPerQty"
        Me.txtPerQty.ReadOnly = True
        Me.txtPerQty.Size = New System.Drawing.Size(107, 20)
        Me.txtPerQty.TabIndex = 11
        Me.txtPerQty.Text = "0"
        '
        'grbWithDraw
        '
        Me.grbWithDraw.Controls.Add(Me.Label3)
        Me.grbWithDraw.Controls.Add(Me.Label2)
        Me.grbWithDraw.Controls.Add(Me.Label1)
        Me.grbWithDraw.Controls.Add(Me.txtPrice)
        Me.grbWithDraw.Controls.Add(Me.txtQtyAll)
        Me.grbWithDraw.Controls.Add(Me.txtVolume)
        Me.grbWithDraw.Controls.Add(Me.txtWeight)
        Me.grbWithDraw.Controls.Add(Me.txtQty)
        Me.grbWithDraw.Controls.Add(Me.lblPriceOut)
        Me.grbWithDraw.Controls.Add(Me.txtPriceOut)
        Me.grbWithDraw.Controls.Add(Me.txtPriceBall)
        Me.grbWithDraw.Controls.Add(Me.lblQtyOut)
        Me.grbWithDraw.Controls.Add(Me.txtQtyOut)
        Me.grbWithDraw.Controls.Add(Me.lblQtyAllOut)
        Me.grbWithDraw.Controls.Add(Me.txtQtyAllOut)
        Me.grbWithDraw.Controls.Add(Me.txtWeightOut)
        Me.grbWithDraw.Controls.Add(Me.lblVolumeOut)
        Me.grbWithDraw.Controls.Add(Me.lblWeightOut)
        Me.grbWithDraw.Controls.Add(Me.txtVolumeOut)
        Me.grbWithDraw.Controls.Add(Me.txtQtyAll_Bal)
        Me.grbWithDraw.Controls.Add(Me.txtQtyBalance)
        Me.grbWithDraw.Controls.Add(Me.txtVolume_Bal)
        Me.grbWithDraw.Controls.Add(Me.txtWeight_Bal)
        Me.grbWithDraw.Location = New System.Drawing.Point(8, 77)
        Me.grbWithDraw.Name = "grbWithDraw"
        Me.grbWithDraw.Size = New System.Drawing.Size(565, 182)
        Me.grbWithDraw.TabIndex = 12
        Me.grbWithDraw.TabStop = False
        Me.grbWithDraw.Text = "รายการเบิก"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label3.Location = New System.Drawing.Point(344, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(77, 13)
        Me.Label3.TabIndex = 27
        Me.Label3.Text = "จำนวนหลังเบิก"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label2.Location = New System.Drawing.Point(248, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "จำนวนเบิก"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(142, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(80, 13)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "จำนวนก่อนเบิก"
        '
        'txtPrice
        '
        Me.txtPrice.Location = New System.Drawing.Point(122, 142)
        Me.txtPrice.Name = "txtPrice"
        Me.txtPrice.ReadOnly = True
        Me.txtPrice.Size = New System.Drawing.Size(92, 20)
        Me.txtPrice.TabIndex = 24
        Me.txtPrice.Text = "0"
        '
        'txtQtyAll
        '
        Me.txtQtyAll.Location = New System.Drawing.Point(122, 116)
        Me.txtQtyAll.Name = "txtQtyAll"
        Me.txtQtyAll.ReadOnly = True
        Me.txtQtyAll.Size = New System.Drawing.Size(92, 20)
        Me.txtQtyAll.TabIndex = 23
        Me.txtQtyAll.Text = "0"
        '
        'txtVolume
        '
        Me.txtVolume.Location = New System.Drawing.Point(122, 93)
        Me.txtVolume.Name = "txtVolume"
        Me.txtVolume.ReadOnly = True
        Me.txtVolume.Size = New System.Drawing.Size(92, 20)
        Me.txtVolume.TabIndex = 22
        Me.txtVolume.Text = "0"
        '
        'txtWeight
        '
        Me.txtWeight.Location = New System.Drawing.Point(122, 70)
        Me.txtWeight.Name = "txtWeight"
        Me.txtWeight.ReadOnly = True
        Me.txtWeight.Size = New System.Drawing.Size(92, 20)
        Me.txtWeight.TabIndex = 21
        Me.txtWeight.Text = "0"
        '
        'txtQty
        '
        Me.txtQty.Location = New System.Drawing.Point(122, 47)
        Me.txtQty.Name = "txtQty"
        Me.txtQty.ReadOnly = True
        Me.txtQty.Size = New System.Drawing.Size(92, 20)
        Me.txtQty.TabIndex = 20
        Me.txtQty.Text = "0"
        '
        'lblPriceOut
        '
        Me.lblPriceOut.AutoSize = True
        Me.lblPriceOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblPriceOut.Location = New System.Drawing.Point(7, 145)
        Me.lblPriceOut.Name = "lblPriceOut"
        Me.lblPriceOut.Size = New System.Drawing.Size(30, 13)
        Me.lblPriceOut.TabIndex = 16
        Me.lblPriceOut.Text = "ราคา"
        '
        'txtPriceOut
        '
        Me.txtPriceOut.Location = New System.Drawing.Point(223, 142)
        Me.txtPriceOut.Name = "txtPriceOut"
        Me.txtPriceOut.Size = New System.Drawing.Size(92, 20)
        Me.txtPriceOut.TabIndex = 17
        Me.txtPriceOut.Text = "0"
        '
        'txtPriceBall
        '
        Me.txtPriceBall.BackColor = System.Drawing.Color.WhiteSmoke
        Me.txtPriceBall.Location = New System.Drawing.Point(321, 142)
        Me.txtPriceBall.Multiline = True
        Me.txtPriceBall.Name = "txtPriceBall"
        Me.txtPriceBall.ReadOnly = True
        Me.txtPriceBall.Size = New System.Drawing.Size(101, 20)
        Me.txtPriceBall.TabIndex = 19
        Me.txtPriceBall.Text = "0"
        '
        'frmPicking_Item
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(582, 315)
        Me.Controls.Add(Me.grbWithDraw)
        Me.Controls.Add(Me.txtPerQty)
        Me.Controls.Add(Me.txtPerVolume)
        Me.Controls.Add(Me.txtPerWeight)
        Me.Controls.Add(Me.lblPerQty)
        Me.Controls.Add(Me.lblPerVolume)
        Me.Controls.Add(Me.lblPerWeight)
        Me.Controls.Add(Me.btn_cancel)
        Me.Controls.Add(Me.btn_select)
        Me.Controls.Add(Me.lblProductName)
        Me.Controls.Add(Me.lblSku_Id)
        Me.Controls.Add(Me.lblOrder_No)
        Me.Controls.Add(Me.txtProductName)
        Me.Controls.Add(Me.txtSku_Id)
        Me.Controls.Add(Me.txtOrder_No)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPicking_Item"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เบิกออกบางส่วน"
        Me.grbWithDraw.ResumeLayout(False)
        Me.grbWithDraw.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtOrder_No As System.Windows.Forms.TextBox
    Friend WithEvents txtSku_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtProductName As System.Windows.Forms.TextBox
    Friend WithEvents txtQtyBalance As System.Windows.Forms.TextBox
    Friend WithEvents txtQtyOut As System.Windows.Forms.TextBox
    Friend WithEvents txtWeightOut As System.Windows.Forms.TextBox
    Friend WithEvents lblOrder_No As System.Windows.Forms.Label
    Friend WithEvents lblSku_Id As System.Windows.Forms.Label
    Friend WithEvents lblProductName As System.Windows.Forms.Label
    Friend WithEvents lblQtyOut As System.Windows.Forms.Label
    Friend WithEvents lblWeightOut As System.Windows.Forms.Label
    Friend WithEvents txtVolumeOut As System.Windows.Forms.TextBox
    Friend WithEvents lblVolumeOut As System.Windows.Forms.Label
    Friend WithEvents btn_cancel As System.Windows.Forms.Button
    Friend WithEvents btn_select As System.Windows.Forms.Button
    Friend WithEvents txtWeight_Bal As System.Windows.Forms.TextBox
    Friend WithEvents txtVolume_Bal As System.Windows.Forms.TextBox
    Friend WithEvents lblQtyAllOut As System.Windows.Forms.Label
    Friend WithEvents txtQtyAllOut As System.Windows.Forms.TextBox
    Friend WithEvents txtQtyAll_Bal As System.Windows.Forms.TextBox
    Friend WithEvents lblPerQty As System.Windows.Forms.Label
    Friend WithEvents lblPerVolume As System.Windows.Forms.Label
    Friend WithEvents lblPerWeight As System.Windows.Forms.Label
    Friend WithEvents txtPerWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtPerVolume As System.Windows.Forms.TextBox
    Friend WithEvents txtPerQty As System.Windows.Forms.TextBox
    Friend WithEvents grbWithDraw As System.Windows.Forms.GroupBox
    Friend WithEvents lblPriceOut As System.Windows.Forms.Label
    Friend WithEvents txtPriceOut As System.Windows.Forms.TextBox
    Friend WithEvents txtPriceBall As System.Windows.Forms.TextBox
    Friend WithEvents txtPrice As System.Windows.Forms.TextBox
    Friend WithEvents txtQtyAll As System.Windows.Forms.TextBox
    Friend WithEvents txtVolume As System.Windows.Forms.TextBox
    Friend WithEvents txtWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtQty As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class
