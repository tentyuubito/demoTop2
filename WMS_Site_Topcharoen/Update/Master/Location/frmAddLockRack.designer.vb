<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddLockRack
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.grbDocumentType = New System.Windows.Forms.GroupBox
        Me.cbLocationType = New System.Windows.Forms.ComboBox
        Me.cbZone = New System.Windows.Forms.ComboBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cbRoom = New System.Windows.Forms.ComboBox
        Me.cbWarehouse = New System.Windows.Forms.ComboBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblDes = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.btnGenLocation = New System.Windows.Forms.Button
        Me.btn_clear = New System.Windows.Forms.Button
        Me.txtMaxPallet = New System.Windows.Forms.TextBox
        Me.txtMaxVolume = New System.Windows.Forms.TextBox
        Me.txtMaxWeight = New System.Windows.Forms.TextBox
        Me.txtMaxQTY = New System.Windows.Forms.TextBox
        Me.Label16 = New System.Windows.Forms.Label
        Me.Label15 = New System.Windows.Forms.Label
        Me.Label14 = New System.Windows.Forms.Label
        Me.Label13 = New System.Windows.Forms.Label
        Me.txtPositionLevel = New System.Windows.Forms.TextBox
        Me.txtPositionDepth = New System.Windows.Forms.TextBox
        Me.txtPositionRow = New System.Windows.Forms.TextBox
        Me.Label12 = New System.Windows.Forms.Label
        Me.txtLevel2 = New System.Windows.Forms.TextBox
        Me.Label11 = New System.Windows.Forms.Label
        Me.txtDepth2 = New System.Windows.Forms.TextBox
        Me.Label10 = New System.Windows.Forms.Label
        Me.txtRow2 = New System.Windows.Forms.TextBox
        Me.txtLevel1 = New System.Windows.Forms.TextBox
        Me.Label8 = New System.Windows.Forms.Label
        Me.txtDepth1 = New System.Windows.Forms.TextBox
        Me.txtRow1 = New System.Windows.Forms.TextBox
        Me.txtLockRack = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtFormatLocation = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.grdLocationAlias = New System.Windows.Forms.DataGridView
        Me.Column6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColAlias = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColLock = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColWareHouse = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColRoom = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColZone = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColLocationType = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMaxQty = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMaxWeight = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ColMaxVolume = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnExit = New System.Windows.Forms.Button
        Me.grbDocumentType.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdLocationAlias, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'grbDocumentType
        '
        Me.grbDocumentType.Controls.Add(Me.cbLocationType)
        Me.grbDocumentType.Controls.Add(Me.cbZone)
        Me.grbDocumentType.Controls.Add(Me.Label3)
        Me.grbDocumentType.Controls.Add(Me.cbRoom)
        Me.grbDocumentType.Controls.Add(Me.cbWarehouse)
        Me.grbDocumentType.Controls.Add(Me.Label2)
        Me.grbDocumentType.Controls.Add(Me.Label1)
        Me.grbDocumentType.Controls.Add(Me.lblDes)
        Me.grbDocumentType.Location = New System.Drawing.Point(12, 6)
        Me.grbDocumentType.Name = "grbDocumentType"
        Me.grbDocumentType.Size = New System.Drawing.Size(570, 71)
        Me.grbDocumentType.TabIndex = 4
        Me.grbDocumentType.TabStop = False
        Me.grbDocumentType.Text = "คลังสินค้า"
        '
        'cbLocationType
        '
        Me.cbLocationType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbLocationType.FormattingEnabled = True
        Me.cbLocationType.Location = New System.Drawing.Point(375, 42)
        Me.cbLocationType.Name = "cbLocationType"
        Me.cbLocationType.Size = New System.Drawing.Size(162, 21)
        Me.cbLocationType.TabIndex = 3
        '
        'cbZone
        '
        Me.cbZone.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbZone.FormattingEnabled = True
        Me.cbZone.Location = New System.Drawing.Point(375, 15)
        Me.cbZone.Name = "cbZone"
        Me.cbZone.Size = New System.Drawing.Size(162, 21)
        Me.cbZone.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label3.Location = New System.Drawing.Point(34, 18)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(73, 13)
        Me.Label3.TabIndex = 17
        Me.Label3.Text = "WareHouse"
        '
        'cbRoom
        '
        Me.cbRoom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbRoom.FormattingEnabled = True
        Me.cbRoom.Location = New System.Drawing.Point(113, 42)
        Me.cbRoom.Name = "cbRoom"
        Me.cbRoom.Size = New System.Drawing.Size(162, 21)
        Me.cbRoom.TabIndex = 2
        '
        'cbWarehouse
        '
        Me.cbWarehouse.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbWarehouse.FormattingEnabled = True
        Me.cbWarehouse.Location = New System.Drawing.Point(113, 15)
        Me.cbWarehouse.Name = "cbWarehouse"
        Me.cbWarehouse.Size = New System.Drawing.Size(162, 21)
        Me.cbWarehouse.TabIndex = 0
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(281, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(88, 13)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Location Type"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(333, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(36, 13)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Zone"
        '
        'lblDes
        '
        Me.lblDes.AutoSize = True
        Me.lblDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblDes.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.lblDes.Location = New System.Drawing.Point(68, 45)
        Me.lblDes.Name = "lblDes"
        Me.lblDes.Size = New System.Drawing.Size(39, 13)
        Me.lblDes.TabIndex = 6
        Me.lblDes.Text = "Room"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnGenLocation)
        Me.GroupBox1.Controls.Add(Me.btn_clear)
        Me.GroupBox1.Controls.Add(Me.txtMaxPallet)
        Me.GroupBox1.Controls.Add(Me.txtMaxVolume)
        Me.GroupBox1.Controls.Add(Me.txtMaxWeight)
        Me.GroupBox1.Controls.Add(Me.txtMaxQTY)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Controls.Add(Me.Label15)
        Me.GroupBox1.Controls.Add(Me.Label14)
        Me.GroupBox1.Controls.Add(Me.Label13)
        Me.GroupBox1.Controls.Add(Me.txtPositionLevel)
        Me.GroupBox1.Controls.Add(Me.txtPositionDepth)
        Me.GroupBox1.Controls.Add(Me.txtPositionRow)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me.txtLevel2)
        Me.GroupBox1.Controls.Add(Me.Label11)
        Me.GroupBox1.Controls.Add(Me.txtDepth2)
        Me.GroupBox1.Controls.Add(Me.Label10)
        Me.GroupBox1.Controls.Add(Me.txtRow2)
        Me.GroupBox1.Controls.Add(Me.txtLevel1)
        Me.GroupBox1.Controls.Add(Me.Label8)
        Me.GroupBox1.Controls.Add(Me.txtDepth1)
        Me.GroupBox1.Controls.Add(Me.txtRow1)
        Me.GroupBox1.Controls.Add(Me.txtLockRack)
        Me.GroupBox1.Controls.Add(Me.Label6)
        Me.GroupBox1.Controls.Add(Me.Label5)
        Me.GroupBox1.Controls.Add(Me.txtFormatLocation)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Label7)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 83)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(570, 188)
        Me.GroupBox1.TabIndex = 21
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "คุณสมบัติ"
        '
        'btnGenLocation
        '
        Me.btnGenLocation.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnGenLocation.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGenLocation.Location = New System.Drawing.Point(158, 145)
        Me.btnGenLocation.Name = "btnGenLocation"
        Me.btnGenLocation.Size = New System.Drawing.Size(100, 38)
        Me.btnGenLocation.TabIndex = 21
        Me.btnGenLocation.Text = "ตกลง"
        Me.btnGenLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnGenLocation.UseVisualStyleBackColor = True
        '
        'btn_clear
        '
        Me.btn_clear.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ส่งข้อมูล
        Me.btn_clear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btn_clear.Location = New System.Drawing.Point(280, 145)
        Me.btn_clear.Name = "btn_clear"
        Me.btn_clear.Size = New System.Drawing.Size(100, 38)
        Me.btn_clear.TabIndex = 22
        Me.btn_clear.Text = "ล้าง"
        Me.btn_clear.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btn_clear.UseVisualStyleBackColor = True
        '
        'txtMaxPallet
        '
        Me.txtMaxPallet.Location = New System.Drawing.Point(447, 119)
        Me.txtMaxPallet.MaxLength = 10
        Me.txtMaxPallet.Name = "txtMaxPallet"
        Me.txtMaxPallet.Size = New System.Drawing.Size(92, 20)
        Me.txtMaxPallet.TabIndex = 20
        Me.txtMaxPallet.Text = "1000000"
        '
        'txtMaxVolume
        '
        Me.txtMaxVolume.Location = New System.Drawing.Point(447, 92)
        Me.txtMaxVolume.MaxLength = 10
        Me.txtMaxVolume.Name = "txtMaxVolume"
        Me.txtMaxVolume.Size = New System.Drawing.Size(92, 20)
        Me.txtMaxVolume.TabIndex = 16
        Me.txtMaxVolume.Text = "1000000"
        '
        'txtMaxWeight
        '
        Me.txtMaxWeight.Location = New System.Drawing.Point(447, 66)
        Me.txtMaxWeight.MaxLength = 10
        Me.txtMaxWeight.Name = "txtMaxWeight"
        Me.txtMaxWeight.Size = New System.Drawing.Size(92, 20)
        Me.txtMaxWeight.TabIndex = 12
        Me.txtMaxWeight.Text = "1000000"
        '
        'txtMaxQTY
        '
        Me.txtMaxQTY.Location = New System.Drawing.Point(447, 40)
        Me.txtMaxQTY.MaxLength = 10
        Me.txtMaxQTY.Name = "txtMaxQTY"
        Me.txtMaxQTY.Size = New System.Drawing.Size(92, 20)
        Me.txtMaxQTY.TabIndex = 8
        Me.txtMaxQTY.Text = "1000000"
        '
        'Label16
        '
        Me.Label16.AutoSize = True
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label16.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label16.Location = New System.Drawing.Point(384, 43)
        Me.Label16.Name = "Label16"
        Me.Label16.Size = New System.Drawing.Size(53, 13)
        Me.Label16.TabIndex = 44
        Me.Label16.Text = "Max Qty"
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label15.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label15.Location = New System.Drawing.Point(363, 69)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(74, 13)
        Me.Label15.TabIndex = 43
        Me.Label15.Text = "Max Weight"
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label14.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label14.Location = New System.Drawing.Point(363, 95)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(75, 13)
        Me.Label14.TabIndex = 42
        Me.Label14.Text = "Max Volume"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label13.Location = New System.Drawing.Point(372, 122)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(66, 13)
        Me.Label13.TabIndex = 41
        Me.Label13.Text = "Max Pallet"
        '
        'txtPositionLevel
        '
        Me.txtPositionLevel.Location = New System.Drawing.Point(264, 119)
        Me.txtPositionLevel.MaxLength = 5
        Me.txtPositionLevel.Name = "txtPositionLevel"
        Me.txtPositionLevel.Size = New System.Drawing.Size(42, 20)
        Me.txtPositionLevel.TabIndex = 19
        Me.txtPositionLevel.Text = "XX"
        '
        'txtPositionDepth
        '
        Me.txtPositionDepth.Location = New System.Drawing.Point(264, 92)
        Me.txtPositionDepth.MaxLength = 5
        Me.txtPositionDepth.Name = "txtPositionDepth"
        Me.txtPositionDepth.Size = New System.Drawing.Size(42, 20)
        Me.txtPositionDepth.TabIndex = 15
        Me.txtPositionDepth.Text = "XX"
        '
        'txtPositionRow
        '
        Me.txtPositionRow.Location = New System.Drawing.Point(264, 66)
        Me.txtPositionRow.MaxLength = 5
        Me.txtPositionRow.Name = "txtPositionRow"
        Me.txtPositionRow.Size = New System.Drawing.Size(42, 20)
        Me.txtPositionRow.TabIndex = 11
        Me.txtPositionRow.Text = "XX"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label12.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label12.Location = New System.Drawing.Point(179, 122)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(11, 13)
        Me.Label12.TabIndex = 36
        Me.Label12.Text = "-"
        '
        'txtLevel2
        '
        Me.txtLevel2.Location = New System.Drawing.Point(196, 119)
        Me.txtLevel2.MaxLength = 5
        Me.txtLevel2.Name = "txtLevel2"
        Me.txtLevel2.Size = New System.Drawing.Size(62, 20)
        Me.txtLevel2.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label11.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label11.Location = New System.Drawing.Point(179, 95)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(11, 13)
        Me.Label11.TabIndex = 34
        Me.Label11.Text = "-"
        '
        'txtDepth2
        '
        Me.txtDepth2.Location = New System.Drawing.Point(196, 92)
        Me.txtDepth2.MaxLength = 5
        Me.txtDepth2.Name = "txtDepth2"
        Me.txtDepth2.Size = New System.Drawing.Size(62, 20)
        Me.txtDepth2.TabIndex = 14
        Me.txtDepth2.Text = "1"
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label10.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label10.Location = New System.Drawing.Point(179, 69)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(11, 13)
        Me.Label10.TabIndex = 32
        Me.Label10.Text = "-"
        '
        'txtRow2
        '
        Me.txtRow2.Location = New System.Drawing.Point(196, 66)
        Me.txtRow2.MaxLength = 5
        Me.txtRow2.Name = "txtRow2"
        Me.txtRow2.Size = New System.Drawing.Size(62, 20)
        Me.txtRow2.TabIndex = 10
        '
        'txtLevel1
        '
        Me.txtLevel1.Location = New System.Drawing.Point(113, 119)
        Me.txtLevel1.MaxLength = 5
        Me.txtLevel1.Name = "txtLevel1"
        Me.txtLevel1.Size = New System.Drawing.Size(61, 20)
        Me.txtLevel1.TabIndex = 17
        Me.txtLevel1.Text = "1"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label8.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label8.Location = New System.Drawing.Point(69, 122)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(38, 13)
        Me.Label8.TabIndex = 27
        Me.Label8.Text = "Level"
        '
        'txtDepth1
        '
        Me.txtDepth1.Location = New System.Drawing.Point(113, 92)
        Me.txtDepth1.MaxLength = 5
        Me.txtDepth1.Name = "txtDepth1"
        Me.txtDepth1.Size = New System.Drawing.Size(61, 20)
        Me.txtDepth1.TabIndex = 13
        Me.txtDepth1.Text = "1"
        '
        'txtRow1
        '
        Me.txtRow1.Location = New System.Drawing.Point(113, 66)
        Me.txtRow1.MaxLength = 5
        Me.txtRow1.Name = "txtRow1"
        Me.txtRow1.Size = New System.Drawing.Size(61, 20)
        Me.txtRow1.TabIndex = 9
        Me.txtRow1.Text = "1"
        '
        'txtLockRack
        '
        Me.txtLockRack.Location = New System.Drawing.Point(113, 40)
        Me.txtLockRack.MaxLength = 20
        Me.txtLockRack.Name = "txtLockRack"
        Me.txtLockRack.Size = New System.Drawing.Size(193, 20)
        Me.txtLockRack.TabIndex = 5
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label6.Location = New System.Drawing.Point(66, 95)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(41, 13)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Depth"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(75, 66)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(32, 13)
        Me.Label5.TabIndex = 22
        Me.Label5.Text = "Row"
        '
        'txtFormatLocation
        '
        Me.txtFormatLocation.Location = New System.Drawing.Point(113, 15)
        Me.txtFormatLocation.MaxLength = 1024
        Me.txtFormatLocation.Name = "txtFormatLocation"
        Me.txtFormatLocation.Size = New System.Drawing.Size(267, 20)
        Me.txtFormatLocation.TabIndex = 4
        Me.txtFormatLocation.Text = "[Lock][Row][Level]"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Blue
        Me.Label4.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label4.Location = New System.Drawing.Point(9, 18)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(98, 13)
        Me.Label4.TabIndex = 17
        Me.Label4.Text = "Format Location"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(28, 43)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(79, 13)
        Me.Label7.TabIndex = 6
        Me.Label7.Text = "Lock / Rack"
        '
        'btnSave
        '
        Me.btnSave.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.Save
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(12, 635)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(100, 38)
        Me.btnSave.TabIndex = 24
        Me.btnSave.Text = "บันทึก"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grdLocationAlias
        '
        Me.grdLocationAlias.AllowUserToAddRows = False
        Me.grdLocationAlias.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdLocationAlias.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdLocationAlias.BackgroundColor = System.Drawing.Color.White
        Me.grdLocationAlias.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column6, Me.ColAlias, Me.ColLock, Me.Column7, Me.Column9, Me.Column8, Me.ColWareHouse, Me.ColRoom, Me.ColZone, Me.ColLocationType, Me.ColMaxQty, Me.ColMaxWeight, Me.ColMaxVolume, Me.Column1, Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4})
        Me.grdLocationAlias.Location = New System.Drawing.Point(12, 277)
        Me.grdLocationAlias.Name = "grdLocationAlias"
        Me.grdLocationAlias.ReadOnly = True
        Me.grdLocationAlias.Size = New System.Drawing.Size(570, 351)
        Me.grdLocationAlias.TabIndex = 45
        '
        'Column6
        '
        Me.Column6.DataPropertyName = "Location_Index"
        Me.Column6.HeaderText = "Column6"
        Me.Column6.Name = "Column6"
        Me.Column6.ReadOnly = True
        Me.Column6.Visible = False
        '
        'ColAlias
        '
        Me.ColAlias.DataPropertyName = "Location_Alias"
        Me.ColAlias.HeaderText = "ตำแหน่ง"
        Me.ColAlias.Name = "ColAlias"
        Me.ColAlias.ReadOnly = True
        Me.ColAlias.Width = 130
        '
        'ColLock
        '
        Me.ColLock.DataPropertyName = "Lock"
        Me.ColLock.HeaderText = "Lock"
        Me.ColLock.Name = "ColLock"
        Me.ColLock.ReadOnly = True
        '
        'Column7
        '
        Me.Column7.DataPropertyName = "Row"
        Me.Column7.HeaderText = "Row"
        Me.Column7.Name = "Column7"
        Me.Column7.ReadOnly = True
        '
        'Column9
        '
        Me.Column9.DataPropertyName = "Depth"
        Me.Column9.HeaderText = "Depth"
        Me.Column9.Name = "Column9"
        Me.Column9.ReadOnly = True
        '
        'Column8
        '
        Me.Column8.DataPropertyName = "Level"
        Me.Column8.HeaderText = "Level"
        Me.Column8.Name = "Column8"
        Me.Column8.ReadOnly = True
        '
        'ColWareHouse
        '
        Me.ColWareHouse.DataPropertyName = "warehose"
        Me.ColWareHouse.HeaderText = "คลังสินค้า"
        Me.ColWareHouse.Name = "ColWareHouse"
        Me.ColWareHouse.ReadOnly = True
        Me.ColWareHouse.Visible = False
        '
        'ColRoom
        '
        Me.ColRoom.DataPropertyName = "Room"
        Me.ColRoom.HeaderText = "ห้อง"
        Me.ColRoom.Name = "ColRoom"
        Me.ColRoom.ReadOnly = True
        Me.ColRoom.Visible = False
        '
        'ColZone
        '
        Me.ColZone.DataPropertyName = "Zone"
        Me.ColZone.HeaderText = "โซน"
        Me.ColZone.Name = "ColZone"
        Me.ColZone.ReadOnly = True
        Me.ColZone.Visible = False
        '
        'ColLocationType
        '
        Me.ColLocationType.DataPropertyName = "LocationType"
        Me.ColLocationType.HeaderText = "ประเภทตำแหน่ง"
        Me.ColLocationType.Name = "ColLocationType"
        Me.ColLocationType.ReadOnly = True
        Me.ColLocationType.Visible = False
        Me.ColLocationType.Width = 150
        '
        'ColMaxQty
        '
        Me.ColMaxQty.DataPropertyName = "Max_Qty"
        Me.ColMaxQty.HeaderText = "ปริมาณสูงสุด"
        Me.ColMaxQty.Name = "ColMaxQty"
        Me.ColMaxQty.ReadOnly = True
        Me.ColMaxQty.Visible = False
        '
        'ColMaxWeight
        '
        Me.ColMaxWeight.DataPropertyName = "Max_Weight"
        Me.ColMaxWeight.HeaderText = "น้ำหนักสูงสุด"
        Me.ColMaxWeight.Name = "ColMaxWeight"
        Me.ColMaxWeight.ReadOnly = True
        Me.ColMaxWeight.Visible = False
        '
        'ColMaxVolume
        '
        Me.ColMaxVolume.DataPropertyName = "Max_Volume"
        Me.ColMaxVolume.HeaderText = "ปริมาตรสูงสุด"
        Me.ColMaxVolume.Name = "ColMaxVolume"
        Me.ColMaxVolume.ReadOnly = True
        Me.ColMaxVolume.Visible = False
        '
        'Column1
        '
        Me.Column1.DataPropertyName = "Room_Id"
        Me.Column1.HeaderText = "Column1"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "Row"
        Me.DataGridViewTextBoxColumn1.HeaderText = "Column3"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Visible = False
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Location_Id"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Column2"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Visible = False
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "LocationType_Index"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Column4"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Visible = False
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Warehouse_No"
        Me.DataGridViewTextBoxColumn4.HeaderText = "Column5"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        Me.DataGridViewTextBoxColumn4.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ออกจากระบบ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnExit.Location = New System.Drawing.Point(482, 638)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(100, 33)
        Me.btnExit.TabIndex = 46
        Me.btnExit.Text = "ออก"
        Me.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'frmAddLockRack
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(596, 678)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grdLocationAlias)
        Me.Controls.Add(Me.btnSave)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grbDocumentType)
        Me.Name = "frmAddLockRack"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "เพิ่ม Lock / Rack"
        Me.grbDocumentType.ResumeLayout(False)
        Me.grbDocumentType.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdLocationAlias, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grbDocumentType As System.Windows.Forms.GroupBox
    Friend WithEvents cbRoom As System.Windows.Forms.ComboBox
    Friend WithEvents cbWarehouse As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblDes As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cbLocationType As System.Windows.Forms.ComboBox
    Friend WithEvents cbZone As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtFormatLocation As System.Windows.Forms.TextBox
    Friend WithEvents txtDepth1 As System.Windows.Forms.TextBox
    Friend WithEvents txtRow1 As System.Windows.Forms.TextBox
    Friend WithEvents txtLockRack As System.Windows.Forms.TextBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtLevel2 As System.Windows.Forms.TextBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents txtDepth2 As System.Windows.Forms.TextBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents txtRow2 As System.Windows.Forms.TextBox
    Friend WithEvents txtLevel1 As System.Windows.Forms.TextBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents txtPositionLevel As System.Windows.Forms.TextBox
    Friend WithEvents txtPositionDepth As System.Windows.Forms.TextBox
    Friend WithEvents txtPositionRow As System.Windows.Forms.TextBox
    Friend WithEvents Label16 As System.Windows.Forms.Label
    Friend WithEvents Label15 As System.Windows.Forms.Label
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents txtMaxPallet As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxVolume As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxWeight As System.Windows.Forms.TextBox
    Friend WithEvents txtMaxQTY As System.Windows.Forms.TextBox
    Friend WithEvents btn_clear As System.Windows.Forms.Button
    Friend WithEvents btnGenLocation As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents grdLocationAlias As System.Windows.Forms.DataGridView
    Friend WithEvents Column6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColAlias As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLock As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColWareHouse As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColRoom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColZone As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColLocationType As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaxQty As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaxWeight As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ColMaxVolume As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Column1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnExit As System.Windows.Forms.Button
End Class
