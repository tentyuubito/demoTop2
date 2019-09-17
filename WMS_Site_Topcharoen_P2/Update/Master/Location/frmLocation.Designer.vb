<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLocation
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
        Me.btnExit = New System.Windows.Forms.Button
        Me.grbDocumentType = New System.Windows.Forms.GroupBox
        Me.lblScore = New System.Windows.Forms.Label
        Me.lblMaxPallet = New System.Windows.Forms.Label
        Me.txtScore = New System.Windows.Forms.TextBox
        Me.txtMaxPallet = New System.Windows.Forms.TextBox
        Me.cboAction = New System.Windows.Forms.ComboBox
        Me.lblAction = New System.Windows.Forms.Label
        Me.chkAllow_Sugguest_Pick = New System.Windows.Forms.CheckBox
        Me.chkAllow_Sugguest_Putaway = New System.Windows.Forms.CheckBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtMaxQty = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtDepth = New System.Windows.Forms.TextBox
        Me.txtLevel = New System.Windows.Forms.TextBox
        Me.txtRow = New System.Windows.Forms.TextBox
        Me.Label9 = New System.Windows.Forms.Label
        Me.Label8 = New System.Windows.Forms.Label
        Me.cbZone = New System.Windows.Forms.ComboBox
        Me.lblZone = New System.Windows.Forms.Label
        Me.cbLocationType = New System.Windows.Forms.ComboBox
        Me.cbRoom = New System.Windows.Forms.ComboBox
        Me.cbWarehouse = New System.Windows.Forms.ComboBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.lblLock = New System.Windows.Forms.Label
        Me.lblRoom = New System.Windows.Forms.Label
        Me.txtMax_Volume = New System.Windows.Forms.TextBox
        Me.txtMax_Weight = New System.Windows.Forms.TextBox
        Me.txtLock = New System.Windows.Forms.TextBox
        Me.lblWareHouse = New System.Windows.Forms.Label
        Me.txtID = New System.Windows.Forms.TextBox
        Me.txtLocation = New System.Windows.Forms.TextBox
        Me.lbID = New System.Windows.Forms.Label
        Me.lblDes = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.grbDocumentType.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(718, 303)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 33)
        Me.btnExit.TabIndex = 14
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'grbDocumentType
        '
        Me.grbDocumentType.Controls.Add(Me.lblScore)
        Me.grbDocumentType.Controls.Add(Me.lblMaxPallet)
        Me.grbDocumentType.Controls.Add(Me.txtScore)
        Me.grbDocumentType.Controls.Add(Me.txtMaxPallet)
        Me.grbDocumentType.Controls.Add(Me.cboAction)
        Me.grbDocumentType.Controls.Add(Me.lblAction)
        Me.grbDocumentType.Controls.Add(Me.chkAllow_Sugguest_Pick)
        Me.grbDocumentType.Controls.Add(Me.chkAllow_Sugguest_Putaway)
        Me.grbDocumentType.Controls.Add(Me.Label11)
        Me.grbDocumentType.Controls.Add(Me.txtMaxQty)
        Me.grbDocumentType.Controls.Add(Me.Label10)
        Me.grbDocumentType.Controls.Add(Me.txtDepth)
        Me.grbDocumentType.Controls.Add(Me.txtLevel)
        Me.grbDocumentType.Controls.Add(Me.txtRow)
        Me.grbDocumentType.Controls.Add(Me.Label9)
        Me.grbDocumentType.Controls.Add(Me.Label8)
        Me.grbDocumentType.Controls.Add(Me.cbZone)
        Me.grbDocumentType.Controls.Add(Me.lblZone)
        Me.grbDocumentType.Controls.Add(Me.cbLocationType)
        Me.grbDocumentType.Controls.Add(Me.cbRoom)
        Me.grbDocumentType.Controls.Add(Me.cbWarehouse)
        Me.grbDocumentType.Controls.Add(Me.Label6)
        Me.grbDocumentType.Controls.Add(Me.Label5)
        Me.grbDocumentType.Controls.Add(Me.Label4)
        Me.grbDocumentType.Controls.Add(Me.lblLock)
        Me.grbDocumentType.Controls.Add(Me.lblRoom)
        Me.grbDocumentType.Controls.Add(Me.txtMax_Volume)
        Me.grbDocumentType.Controls.Add(Me.txtMax_Weight)
        Me.grbDocumentType.Controls.Add(Me.txtLock)
        Me.grbDocumentType.Controls.Add(Me.lblWareHouse)
        Me.grbDocumentType.Controls.Add(Me.txtID)
        Me.grbDocumentType.Controls.Add(Me.txtLocation)
        Me.grbDocumentType.Controls.Add(Me.lbID)
        Me.grbDocumentType.Controls.Add(Me.lblDes)
        Me.grbDocumentType.Location = New System.Drawing.Point(12, 12)
        Me.grbDocumentType.Name = "grbDocumentType"
        Me.grbDocumentType.Size = New System.Drawing.Size(806, 272)
        Me.grbDocumentType.TabIndex = 3
        Me.grbDocumentType.TabStop = False
        Me.grbDocumentType.Text = "ตำแหน่งจัดเก็บ"
        '
        'lblScore
        '
        Me.lblScore.AutoSize = True
        Me.lblScore.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblScore.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblScore.Location = New System.Drawing.Point(384, 101)
        Me.lblScore.Name = "lblScore"
        Me.lblScore.Size = New System.Drawing.Size(101, 13)
        Me.lblScore.TabIndex = 40
        Me.lblScore.Text = "ลำดับจัดเก็บ/เบิก"
        '
        'lblMaxPallet
        '
        Me.lblMaxPallet.AutoSize = True
        Me.lblMaxPallet.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblMaxPallet.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblMaxPallet.Location = New System.Drawing.Point(414, 240)
        Me.lblMaxPallet.Name = "lblMaxPallet"
        Me.lblMaxPallet.Size = New System.Drawing.Size(71, 13)
        Me.lblMaxPallet.TabIndex = 39
        Me.lblMaxPallet.Text = "พาเลทสูงสุด"
        '
        'txtScore
        '
        Me.txtScore.Location = New System.Drawing.Point(492, 98)
        Me.txtScore.MaxLength = 100
        Me.txtScore.Name = "txtScore"
        Me.txtScore.Size = New System.Drawing.Size(248, 20)
        Me.txtScore.TabIndex = 9
        '
        'txtMaxPallet
        '
        Me.txtMaxPallet.Location = New System.Drawing.Point(492, 237)
        Me.txtMaxPallet.MaxLength = 100
        Me.txtMaxPallet.Name = "txtMaxPallet"
        Me.txtMaxPallet.Size = New System.Drawing.Size(248, 20)
        Me.txtMaxPallet.TabIndex = 14
        '
        'cboAction
        '
        Me.cboAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboAction.FormattingEnabled = True
        Me.cboAction.Location = New System.Drawing.Point(126, 207)
        Me.cboAction.Name = "cboAction"
        Me.cboAction.Size = New System.Drawing.Size(248, 21)
        Me.cboAction.TabIndex = 36
        '
        'lblAction
        '
        Me.lblAction.AutoSize = True
        Me.lblAction.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblAction.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblAction.Location = New System.Drawing.Point(76, 210)
        Me.lblAction.Name = "lblAction"
        Me.lblAction.Size = New System.Drawing.Size(43, 13)
        Me.lblAction.TabIndex = 37
        Me.lblAction.Text = "สถานะ"
        '
        'chkAllow_Sugguest_Pick
        '
        Me.chkAllow_Sugguest_Pick.AutoSize = True
        Me.chkAllow_Sugguest_Pick.Location = New System.Drawing.Point(236, 234)
        Me.chkAllow_Sugguest_Pick.Name = "chkAllow_Sugguest_Pick"
        Me.chkAllow_Sugguest_Pick.Size = New System.Drawing.Size(113, 17)
        Me.chkAllow_Sugguest_Pick.TabIndex = 35
        Me.chkAllow_Sugguest_Pick.Text = "ให้แนะนำหยิบเบิก"
        Me.chkAllow_Sugguest_Pick.UseVisualStyleBackColor = True
        '
        'chkAllow_Sugguest_Putaway
        '
        Me.chkAllow_Sugguest_Putaway.AutoSize = True
        Me.chkAllow_Sugguest_Putaway.Location = New System.Drawing.Point(125, 234)
        Me.chkAllow_Sugguest_Putaway.Name = "chkAllow_Sugguest_Putaway"
        Me.chkAllow_Sugguest_Putaway.Size = New System.Drawing.Size(105, 17)
        Me.chkAllow_Sugguest_Putaway.TabIndex = 34
        Me.chkAllow_Sugguest_Putaway.Text = "ให้แนะนำจัดเก็บ"
        Me.chkAllow_Sugguest_Putaway.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(411, 157)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(74, 13)
        Me.Label11.TabIndex = 33
        Me.Label11.Text = "ปริมาณสูงสุด"
        '
        'txtMaxQty
        '
        Me.txtMaxQty.Location = New System.Drawing.Point(492, 154)
        Me.txtMaxQty.MaxLength = 100
        Me.txtMaxQty.Name = "txtMaxQty"
        Me.txtMaxQty.Size = New System.Drawing.Size(248, 20)
        Me.txtMaxQty.TabIndex = 11
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(453, 48)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(32, 13)
        Me.Label10.TabIndex = 31
        Me.Label10.Text = "ชั้นที่"
        '
        'txtDepth
        '
        Me.txtDepth.Location = New System.Drawing.Point(492, 71)
        Me.txtDepth.MaxLength = 100
        Me.txtDepth.Name = "txtDepth"
        Me.txtDepth.Size = New System.Drawing.Size(248, 20)
        Me.txtDepth.TabIndex = 8
        '
        'txtLevel
        '
        Me.txtLevel.Location = New System.Drawing.Point(492, 45)
        Me.txtLevel.MaxLength = 100
        Me.txtLevel.Name = "txtLevel"
        Me.txtLevel.Size = New System.Drawing.Size(248, 20)
        Me.txtLevel.TabIndex = 7
        '
        'txtRow
        '
        Me.txtRow.Location = New System.Drawing.Point(126, 181)
        Me.txtRow.MaxLength = 100
        Me.txtRow.Name = "txtRow"
        Me.txtRow.Size = New System.Drawing.Size(248, 20)
        Me.txtRow.TabIndex = 6
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label9.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label9.Location = New System.Drawing.Point(463, 74)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(22, 13)
        Me.Label9.TabIndex = 27
        Me.Label9.Text = "ลึก"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(80, 184)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(39, 13)
        Me.Label8.TabIndex = 26
        Me.Label8.Text = "แถวที่"
        '
        'cbZone
        '
        Me.cbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbZone.FormattingEnabled = True
        Me.cbZone.Location = New System.Drawing.Point(126, 127)
        Me.cbZone.Name = "cbZone"
        Me.cbZone.Size = New System.Drawing.Size(248, 21)
        Me.cbZone.TabIndex = 4
        '
        'lblZone
        '
        Me.lblZone.AutoSize = True
        Me.lblZone.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblZone.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblZone.Location = New System.Drawing.Point(89, 130)
        Me.lblZone.Name = "lblZone"
        Me.lblZone.Size = New System.Drawing.Size(30, 13)
        Me.lblZone.TabIndex = 24
        Me.lblZone.Text = "โซน"
        '
        'cbLocationType
        '
        Me.cbLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLocationType.FormattingEnabled = True
        Me.cbLocationType.Location = New System.Drawing.Point(492, 127)
        Me.cbLocationType.Name = "cbLocationType"
        Me.cbLocationType.Size = New System.Drawing.Size(248, 21)
        Me.cbLocationType.TabIndex = 10
        '
        'cbRoom
        '
        Me.cbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRoom.FormattingEnabled = True
        Me.cbRoom.Location = New System.Drawing.Point(126, 98)
        Me.cbRoom.Name = "cbRoom"
        Me.cbRoom.Size = New System.Drawing.Size(248, 21)
        Me.cbRoom.TabIndex = 3
        '
        'cbWarehouse
        '
        Me.cbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWarehouse.FormattingEnabled = True
        Me.cbWarehouse.Location = New System.Drawing.Point(126, 71)
        Me.cbWarehouse.Name = "cbWarehouse"
        Me.cbWarehouse.Size = New System.Drawing.Size(248, 21)
        Me.cbWarehouse.TabIndex = 2
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(406, 212)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(79, 13)
        Me.Label6.TabIndex = 20
        Me.Label6.Text = "ปริมาตรสูงสุด"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(410, 186)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(75, 13)
        Me.Label5.TabIndex = 19
        Me.Label5.Text = "น้ำหนักสูงสุด"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(383, 130)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(102, 13)
        Me.Label4.TabIndex = 18
        Me.Label4.Text = "ประเภทตำาแหน่ง"
        '
        'lblLock
        '
        Me.lblLock.AutoSize = True
        Me.lblLock.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblLock.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblLock.Location = New System.Drawing.Point(90, 157)
        Me.lblLock.Name = "lblLock"
        Me.lblLock.Size = New System.Drawing.Size(29, 13)
        Me.lblLock.TabIndex = 17
        Me.lblLock.Text = "ล๊อค"
        '
        'lblRoom
        '
        Me.lblRoom.AutoSize = True
        Me.lblRoom.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblRoom.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblRoom.Location = New System.Drawing.Point(91, 101)
        Me.lblRoom.Name = "lblRoom"
        Me.lblRoom.Size = New System.Drawing.Size(28, 13)
        Me.lblRoom.TabIndex = 16
        Me.lblRoom.Text = "ห้อง"
        '
        'txtMax_Volume
        '
        Me.txtMax_Volume.Location = New System.Drawing.Point(492, 209)
        Me.txtMax_Volume.MaxLength = 100
        Me.txtMax_Volume.Name = "txtMax_Volume"
        Me.txtMax_Volume.Size = New System.Drawing.Size(248, 20)
        Me.txtMax_Volume.TabIndex = 13
        '
        'txtMax_Weight
        '
        Me.txtMax_Weight.Location = New System.Drawing.Point(492, 183)
        Me.txtMax_Weight.MaxLength = 100
        Me.txtMax_Weight.Name = "txtMax_Weight"
        Me.txtMax_Weight.Size = New System.Drawing.Size(248, 20)
        Me.txtMax_Weight.TabIndex = 12
        '
        'txtLock
        '
        Me.txtLock.Location = New System.Drawing.Point(126, 154)
        Me.txtLock.MaxLength = 100
        Me.txtLock.Name = "txtLock"
        Me.txtLock.Size = New System.Drawing.Size(248, 20)
        Me.txtLock.TabIndex = 5
        '
        'lblWareHouse
        '
        Me.lblWareHouse.AutoSize = True
        Me.lblWareHouse.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblWareHouse.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblWareHouse.Location = New System.Drawing.Point(61, 75)
        Me.lblWareHouse.Name = "lblWareHouse"
        Me.lblWareHouse.Size = New System.Drawing.Size(58, 13)
        Me.lblWareHouse.TabIndex = 8
        Me.lblWareHouse.Text = "คลังสินค้า"
        '
        'txtID
        '
        Me.txtID.BackColor = System.Drawing.Color.LightSteelBlue
        Me.txtID.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtID.Location = New System.Drawing.Point(126, 21)
        Me.txtID.MaxLength = 50
        Me.txtID.Name = "txtID"
        Me.txtID.Size = New System.Drawing.Size(151, 20)
        Me.txtID.TabIndex = 0
        Me.txtID.Visible = False
        '
        'txtLocation
        '
        Me.txtLocation.Location = New System.Drawing.Point(126, 45)
        Me.txtLocation.MaxLength = 100
        Me.txtLocation.Name = "txtLocation"
        Me.txtLocation.Size = New System.Drawing.Size(248, 20)
        Me.txtLocation.TabIndex = 1
        '
        'lbID
        '
        Me.lbID.AutoSize = True
        Me.lbID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lbID.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lbID.Location = New System.Drawing.Point(90, 25)
        Me.lbID.Name = "lbID"
        Me.lbID.Size = New System.Drawing.Size(29, 13)
        Me.lbID.TabIndex = 7
        Me.lbID.Text = "รหัส"
        Me.lbID.Visible = False
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(66, 48)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(53, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "ตำแหน่ง"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnSave.Location = New System.Drawing.Point(12, 303)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 33)
        Me.btnSave.TabIndex = 13
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'frmLocation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(840, 352)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grbDocumentType)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLocation"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ตำแหน่งจัดเก็บ"
        Me.grbDocumentType.ResumeLayout(False)
        Me.grbDocumentType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grbDocumentType As System.Windows.Forms.GroupBox
    Friend WithEvents txtID As System.Windows.Forms.TextBox
    Friend WithEvents txtLocation As System.Windows.Forms.TextBox
    Friend WithEvents lbID As System.Windows.Forms.Label
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents lblLock As System.Windows.Forms.Label
    Friend WithEvents lblRoom As System.Windows.Forms.Label
    Friend WithEvents txtMax_Volume As System.Windows.Forms.TextBox
    Friend WithEvents txtMax_Weight As System.Windows.Forms.TextBox
    Friend WithEvents txtLock As System.Windows.Forms.TextBox
    Friend WithEvents lblWareHouse As System.Windows.Forms.Label
    Friend WithEvents cbWarehouse As System.Windows.Forms.ComboBox
    Friend WithEvents cbRoom As System.Windows.Forms.ComboBox
    Friend WithEvents cbLocationType As System.Windows.Forms.ComboBox
    Friend WithEvents cbZone As System.Windows.Forms.ComboBox
    Friend WithEvents lblZone As System.Windows.Forms.Label
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtDepth As System.Windows.Forms.TextBox
    Friend WithEvents txtLevel As System.Windows.Forms.TextBox
    Friend WithEvents txtRow As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtMaxQty As System.Windows.Forms.TextBox
    Friend WithEvents chkAllow_Sugguest_Pick As System.Windows.Forms.CheckBox
    Friend WithEvents chkAllow_Sugguest_Putaway As System.Windows.Forms.CheckBox
    Friend WithEvents cboAction As System.Windows.Forms.ComboBox
    Friend WithEvents lblAction As System.Windows.Forms.Label
    Friend WithEvents lblMaxPallet As System.Windows.Forms.Label
    Friend WithEvents txtMaxPallet As System.Windows.Forms.TextBox
    Friend WithEvents lblScore As System.Windows.Forms.Label
    Friend WithEvents txtScore As System.Windows.Forms.TextBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
End Class
