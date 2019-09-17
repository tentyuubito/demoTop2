<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransportProblem
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransportProblem))
        Me.txtSO_No = New System.Windows.Forms.TextBox
        Me.txtCustomer_Id = New System.Windows.Forms.TextBox
        Me.txtCustomer_Name = New System.Windows.Forms.TextBox
        Me.lblSO_No = New System.Windows.Forms.Label
        Me.lblCustomer = New System.Windows.Forms.Label
        Me.txtTransportManifestNo = New System.Windows.Forms.TextBox
        Me.lblTransportManifestNo = New System.Windows.Forms.Label
        Me.txtVehicleID = New System.Windows.Forms.TextBox
        Me.lblVehicleID = New System.Windows.Forms.Label
        Me.grbProblemInfo = New System.Windows.Forms.GroupBox
        Me.btnJobSolution = New System.Windows.Forms.Button
        Me.btnJobProblem = New System.Windows.Forms.Button
        Me.lblCustomerShipping = New System.Windows.Forms.Label
        Me.txtCustomerShippingName = New System.Windows.Forms.TextBox
        Me.txtCustomerShippingID = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.lblJobSolutionDes = New System.Windows.Forms.Label
        Me.txtJobSolutionDes = New System.Windows.Forms.TextBox
        Me.lblJobProblemDes = New System.Windows.Forms.Label
        Me.txtJobProblemDes = New System.Windows.Forms.TextBox
        Me.cboJobSolution = New System.Windows.Forms.ComboBox
        Me.lblJobSolution = New System.Windows.Forms.Label
        Me.cboJobProblem = New System.Windows.Forms.ComboBox
        Me.lblJobProblem = New System.Windows.Forms.Label
        Me.cboResponsibleParty = New System.Windows.Forms.ComboBox
        Me.lblResponsibleParty = New System.Windows.Forms.Label
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnSave = New System.Windows.Forms.Button
        Me.grbPaneltyFeeInfo = New System.Windows.Forms.GroupBox
        Me.chkComplaint_Delivered = New System.Windows.Forms.CheckBox
        Me.chkDamage_Delivered = New System.Windows.Forms.CheckBox
        Me.chkTransportPaid = New System.Windows.Forms.CheckBox
        Me.chkTransportCharged = New System.Windows.Forms.CheckBox
        Me.cboStatusManifest = New System.Windows.Forms.ComboBox
        Me.lblSOTransportStatus = New System.Windows.Forms.Label
        Me.grbProblemInfo.SuspendLayout()
        Me.grbPaneltyFeeInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtSO_No
        '
        Me.txtSO_No.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtSO_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSO_No.ForeColor = System.Drawing.Color.Black
        Me.txtSO_No.Location = New System.Drawing.Point(293, 21)
        Me.txtSO_No.Name = "txtSO_No"
        Me.txtSO_No.ReadOnly = True
        Me.txtSO_No.Size = New System.Drawing.Size(153, 20)
        Me.txtSO_No.TabIndex = 5
        '
        'txtCustomer_Id
        '
        Me.txtCustomer_Id.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Id.Location = New System.Drawing.Point(99, 67)
        Me.txtCustomer_Id.Name = "txtCustomer_Id"
        Me.txtCustomer_Id.ReadOnly = True
        Me.txtCustomer_Id.Size = New System.Drawing.Size(119, 20)
        Me.txtCustomer_Id.TabIndex = 7
        '
        'txtCustomer_Name
        '
        Me.txtCustomer_Name.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomer_Name.Location = New System.Drawing.Point(224, 67)
        Me.txtCustomer_Name.Name = "txtCustomer_Name"
        Me.txtCustomer_Name.ReadOnly = True
        Me.txtCustomer_Name.Size = New System.Drawing.Size(222, 20)
        Me.txtCustomer_Name.TabIndex = 8
        '
        'lblSO_No
        '
        Me.lblSO_No.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSO_No.Location = New System.Drawing.Point(237, 22)
        Me.lblSO_No.Name = "lblSO_No"
        Me.lblSO_No.Size = New System.Drawing.Size(50, 20)
        Me.lblSO_No.TabIndex = 4
        Me.lblSO_No.Text = "เลขที่บิล"
        Me.lblSO_No.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblCustomer
        '
        Me.lblCustomer.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomer.Location = New System.Drawing.Point(34, 66)
        Me.lblCustomer.Name = "lblCustomer"
        Me.lblCustomer.Size = New System.Drawing.Size(62, 17)
        Me.lblCustomer.TabIndex = 6
        Me.lblCustomer.Text = "เจ้าของงาน"
        Me.lblCustomer.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTransportManifestNo
        '
        Me.txtTransportManifestNo.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtTransportManifestNo.Location = New System.Drawing.Point(99, 22)
        Me.txtTransportManifestNo.Name = "txtTransportManifestNo"
        Me.txtTransportManifestNo.ReadOnly = True
        Me.txtTransportManifestNo.Size = New System.Drawing.Size(119, 20)
        Me.txtTransportManifestNo.TabIndex = 1
        '
        'lblTransportManifestNo
        '
        Me.lblTransportManifestNo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblTransportManifestNo.Location = New System.Drawing.Point(25, 21)
        Me.lblTransportManifestNo.Name = "lblTransportManifestNo"
        Me.lblTransportManifestNo.Size = New System.Drawing.Size(71, 20)
        Me.lblTransportManifestNo.TabIndex = 0
        Me.lblTransportManifestNo.Text = "เลขที่ใบคุมรถ"
        Me.lblTransportManifestNo.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVehicleID
        '
        Me.txtVehicleID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtVehicleID.Location = New System.Drawing.Point(99, 45)
        Me.txtVehicleID.Name = "txtVehicleID"
        Me.txtVehicleID.ReadOnly = True
        Me.txtVehicleID.Size = New System.Drawing.Size(119, 20)
        Me.txtVehicleID.TabIndex = 3
        '
        'lblVehicleID
        '
        Me.lblVehicleID.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblVehicleID.Location = New System.Drawing.Point(28, 42)
        Me.lblVehicleID.Name = "lblVehicleID"
        Me.lblVehicleID.Size = New System.Drawing.Size(68, 20)
        Me.lblVehicleID.TabIndex = 2
        Me.lblVehicleID.Text = "หมายเลขรถ"
        Me.lblVehicleID.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grbProblemInfo
        '
        Me.grbProblemInfo.Controls.Add(Me.btnJobSolution)
        Me.grbProblemInfo.Controls.Add(Me.btnJobProblem)
        Me.grbProblemInfo.Controls.Add(Me.lblCustomerShipping)
        Me.grbProblemInfo.Controls.Add(Me.txtCustomerShippingName)
        Me.grbProblemInfo.Controls.Add(Me.txtCustomerShippingID)
        Me.grbProblemInfo.Controls.Add(Me.Label1)
        Me.grbProblemInfo.Controls.Add(Me.TextBox1)
        Me.grbProblemInfo.Controls.Add(Me.lblJobSolutionDes)
        Me.grbProblemInfo.Controls.Add(Me.txtJobSolutionDes)
        Me.grbProblemInfo.Controls.Add(Me.lblJobProblemDes)
        Me.grbProblemInfo.Controls.Add(Me.txtJobProblemDes)
        Me.grbProblemInfo.Controls.Add(Me.cboJobSolution)
        Me.grbProblemInfo.Controls.Add(Me.lblJobSolution)
        Me.grbProblemInfo.Controls.Add(Me.cboJobProblem)
        Me.grbProblemInfo.Controls.Add(Me.lblJobProblem)
        Me.grbProblemInfo.Controls.Add(Me.lblTransportManifestNo)
        Me.grbProblemInfo.Controls.Add(Me.txtVehicleID)
        Me.grbProblemInfo.Controls.Add(Me.lblCustomer)
        Me.grbProblemInfo.Controls.Add(Me.lblVehicleID)
        Me.grbProblemInfo.Controls.Add(Me.lblSO_No)
        Me.grbProblemInfo.Controls.Add(Me.txtTransportManifestNo)
        Me.grbProblemInfo.Controls.Add(Me.txtCustomer_Name)
        Me.grbProblemInfo.Controls.Add(Me.txtCustomer_Id)
        Me.grbProblemInfo.Controls.Add(Me.txtSO_No)
        Me.grbProblemInfo.Location = New System.Drawing.Point(14, 3)
        Me.grbProblemInfo.Name = "grbProblemInfo"
        Me.grbProblemInfo.Size = New System.Drawing.Size(462, 334)
        Me.grbProblemInfo.TabIndex = 0
        Me.grbProblemInfo.TabStop = False
        Me.grbProblemInfo.Text = "รายละเอียดปัญหา"
        '
        'btnJobSolution
        '
        Me.btnJobSolution.Location = New System.Drawing.Point(310, 219)
        Me.btnJobSolution.Name = "btnJobSolution"
        Me.btnJobSolution.Size = New System.Drawing.Size(24, 23)
        Me.btnJobSolution.TabIndex = 321
        Me.btnJobSolution.Text = "..."
        Me.btnJobSolution.UseVisualStyleBackColor = True
        '
        'btnJobProblem
        '
        Me.btnJobProblem.Location = New System.Drawing.Point(310, 112)
        Me.btnJobProblem.Name = "btnJobProblem"
        Me.btnJobProblem.Size = New System.Drawing.Size(24, 23)
        Me.btnJobProblem.TabIndex = 320
        Me.btnJobProblem.Text = "..."
        Me.btnJobProblem.UseVisualStyleBackColor = True
        '
        'lblCustomerShipping
        '
        Me.lblCustomerShipping.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblCustomerShipping.Location = New System.Drawing.Point(28, 89)
        Me.lblCustomerShipping.Name = "lblCustomerShipping"
        Me.lblCustomerShipping.Size = New System.Drawing.Size(68, 17)
        Me.lblCustomerShipping.TabIndex = 21
        Me.lblCustomerShipping.Text = "ผู้รับ/ร้านค้า"
        Me.lblCustomerShipping.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCustomerShippingName
        '
        Me.txtCustomerShippingName.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerShippingName.Location = New System.Drawing.Point(224, 89)
        Me.txtCustomerShippingName.Name = "txtCustomerShippingName"
        Me.txtCustomerShippingName.ReadOnly = True
        Me.txtCustomerShippingName.Size = New System.Drawing.Size(222, 20)
        Me.txtCustomerShippingName.TabIndex = 23
        '
        'txtCustomerShippingID
        '
        Me.txtCustomerShippingID.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.txtCustomerShippingID.Location = New System.Drawing.Point(99, 89)
        Me.txtCustomerShippingID.Name = "txtCustomerShippingID"
        Me.txtCustomerShippingID.ReadOnly = True
        Me.txtCustomerShippingID.Size = New System.Drawing.Size(119, 20)
        Me.txtCustomerShippingID.TabIndex = 22
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label1.Location = New System.Drawing.Point(237, 42)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 20)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "คนขับ"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.TextBox1.Location = New System.Drawing.Point(293, 44)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(153, 20)
        Me.TextBox1.TabIndex = 19
        '
        'lblJobSolutionDes
        '
        Me.lblJobSolutionDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblJobSolutionDes.Location = New System.Drawing.Point(28, 248)
        Me.lblJobSolutionDes.Name = "lblJobSolutionDes"
        Me.lblJobSolutionDes.Size = New System.Drawing.Size(65, 20)
        Me.lblJobSolutionDes.TabIndex = 17
        Me.lblJobSolutionDes.Text = "รายละเอียด"
        Me.lblJobSolutionDes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtJobSolutionDes
        '
        Me.txtJobSolutionDes.Location = New System.Drawing.Point(99, 248)
        Me.txtJobSolutionDes.Multiline = True
        Me.txtJobSolutionDes.Name = "txtJobSolutionDes"
        Me.txtJobSolutionDes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtJobSolutionDes.Size = New System.Drawing.Size(289, 77)
        Me.txtJobSolutionDes.TabIndex = 18
        '
        'lblJobProblemDes
        '
        Me.lblJobProblemDes.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblJobProblemDes.Location = New System.Drawing.Point(31, 139)
        Me.lblJobProblemDes.Name = "lblJobProblemDes"
        Me.lblJobProblemDes.Size = New System.Drawing.Size(62, 20)
        Me.lblJobProblemDes.TabIndex = 13
        Me.lblJobProblemDes.Text = "รายละเอียด"
        Me.lblJobProblemDes.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtJobProblemDes
        '
        Me.txtJobProblemDes.Location = New System.Drawing.Point(99, 139)
        Me.txtJobProblemDes.Multiline = True
        Me.txtJobProblemDes.Name = "txtJobProblemDes"
        Me.txtJobProblemDes.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtJobProblemDes.Size = New System.Drawing.Size(289, 77)
        Me.txtJobProblemDes.TabIndex = 14
        '
        'cboJobSolution
        '
        Me.cboJobSolution.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJobSolution.FormattingEnabled = True
        Me.cboJobSolution.Location = New System.Drawing.Point(99, 221)
        Me.cboJobSolution.Name = "cboJobSolution"
        Me.cboJobSolution.Size = New System.Drawing.Size(205, 21)
        Me.cboJobSolution.TabIndex = 16
        '
        'lblJobSolution
        '
        Me.lblJobSolution.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblJobSolution.Location = New System.Drawing.Point(28, 218)
        Me.lblJobSolution.Name = "lblJobSolution"
        Me.lblJobSolution.Size = New System.Drawing.Size(68, 20)
        Me.lblJobSolution.TabIndex = 15
        Me.lblJobSolution.Text = "การแก้ปัญหา"
        Me.lblJobSolution.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboJobProblem
        '
        Me.cboJobProblem.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboJobProblem.FormattingEnabled = True
        Me.cboJobProblem.Location = New System.Drawing.Point(99, 113)
        Me.cboJobProblem.Name = "cboJobProblem"
        Me.cboJobProblem.Size = New System.Drawing.Size(205, 21)
        Me.cboJobProblem.TabIndex = 12
        '
        'lblJobProblem
        '
        Me.lblJobProblem.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblJobProblem.Location = New System.Drawing.Point(28, 113)
        Me.lblJobProblem.Name = "lblJobProblem"
        Me.lblJobProblem.Size = New System.Drawing.Size(68, 20)
        Me.lblJobProblem.TabIndex = 11
        Me.lblJobProblem.Text = "ปัญหา"
        Me.lblJobProblem.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboResponsibleParty
        '
        Me.cboResponsibleParty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboResponsibleParty.FormattingEnabled = True
        Me.cboResponsibleParty.Location = New System.Drawing.Point(101, 20)
        Me.cboResponsibleParty.Name = "cboResponsibleParty"
        Me.cboResponsibleParty.Size = New System.Drawing.Size(153, 21)
        Me.cboResponsibleParty.TabIndex = 10
        '
        'lblResponsibleParty
        '
        Me.lblResponsibleParty.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblResponsibleParty.Location = New System.Drawing.Point(30, 20)
        Me.lblResponsibleParty.Name = "lblResponsibleParty"
        Me.lblResponsibleParty.Size = New System.Drawing.Size(68, 20)
        Me.lblResponsibleParty.TabIndex = 9
        Me.lblResponsibleParty.Text = "ผู้รับผิดชอบ"
        Me.lblResponsibleParty.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = CType(resources.GetObject("btnExit.Image"), System.Drawing.Image)
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(370, 458)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(107, 38)
        Me.btnExit.TabIndex = 2
        Me.btnExit.Text = "ออก"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnSave
        '
        Me.btnSave.Image = CType(resources.GetObject("btnSave.Image"), System.Drawing.Image)
        Me.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSave.Location = New System.Drawing.Point(14, 458)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(107, 38)
        Me.btnSave.TabIndex = 1
        Me.btnSave.Text = "       บันทึกข้อมูล"
        Me.btnSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'grbPaneltyFeeInfo
        '
        Me.grbPaneltyFeeInfo.Controls.Add(Me.chkComplaint_Delivered)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.chkDamage_Delivered)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.chkTransportPaid)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.cboResponsibleParty)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.lblResponsibleParty)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.chkTransportCharged)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.cboStatusManifest)
        Me.grbPaneltyFeeInfo.Controls.Add(Me.lblSOTransportStatus)
        Me.grbPaneltyFeeInfo.Location = New System.Drawing.Point(12, 343)
        Me.grbPaneltyFeeInfo.Name = "grbPaneltyFeeInfo"
        Me.grbPaneltyFeeInfo.Size = New System.Drawing.Size(464, 109)
        Me.grbPaneltyFeeInfo.TabIndex = 26
        Me.grbPaneltyFeeInfo.TabStop = False
        Me.grbPaneltyFeeInfo.Text = "ผู้รับผิดชอบ / ค่าปรับ"
        '
        'chkComplaint_Delivered
        '
        Me.chkComplaint_Delivered.AutoSize = True
        Me.chkComplaint_Delivered.Location = New System.Drawing.Point(302, 71)
        Me.chkComplaint_Delivered.Name = "chkComplaint_Delivered"
        Me.chkComplaint_Delivered.Size = New System.Drawing.Size(120, 17)
        Me.chkComplaint_Delivered.TabIndex = 355
        Me.chkComplaint_Delivered.Text = "Complaint Delivered"
        Me.chkComplaint_Delivered.UseVisualStyleBackColor = True
        '
        'chkDamage_Delivered
        '
        Me.chkDamage_Delivered.AutoSize = True
        Me.chkDamage_Delivered.Location = New System.Drawing.Point(302, 48)
        Me.chkDamage_Delivered.Name = "chkDamage_Delivered"
        Me.chkDamage_Delivered.Size = New System.Drawing.Size(114, 17)
        Me.chkDamage_Delivered.TabIndex = 354
        Me.chkDamage_Delivered.Text = "Damage Delivered"
        Me.chkDamage_Delivered.UseVisualStyleBackColor = True
        '
        'chkTransportPaid
        '
        Me.chkTransportPaid.AutoSize = True
        Me.chkTransportPaid.Location = New System.Drawing.Point(101, 75)
        Me.chkTransportPaid.Name = "chkTransportPaid"
        Me.chkTransportPaid.Size = New System.Drawing.Size(160, 17)
        Me.chkTransportPaid.TabIndex = 28
        Me.chkTransportPaid.Text = "ยกเว้นการจ่ายค่าขนส่งคนรถ"
        Me.chkTransportPaid.UseVisualStyleBackColor = True
        '
        'chkTransportCharged
        '
        Me.chkTransportCharged.AutoSize = True
        Me.chkTransportCharged.Location = New System.Drawing.Point(101, 52)
        Me.chkTransportCharged.Name = "chkTransportCharged"
        Me.chkTransportCharged.Size = New System.Drawing.Size(153, 17)
        Me.chkTransportCharged.TabIndex = 27
        Me.chkTransportCharged.Text = "ยกเว้นการคิดค่าขนส่งลูกค้า"
        Me.chkTransportCharged.UseVisualStyleBackColor = True
        '
        'cboStatusManifest
        '
        Me.cboStatusManifest.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboStatusManifest.FormattingEnabled = True
        Me.cboStatusManifest.Location = New System.Drawing.Point(329, 21)
        Me.cboStatusManifest.Name = "cboStatusManifest"
        Me.cboStatusManifest.Size = New System.Drawing.Size(119, 21)
        Me.cboStatusManifest.TabIndex = 94
        '
        'lblSOTransportStatus
        '
        Me.lblSOTransportStatus.AutoSize = True
        Me.lblSOTransportStatus.Location = New System.Drawing.Point(272, 25)
        Me.lblSOTransportStatus.Name = "lblSOTransportStatus"
        Me.lblSOTransportStatus.Size = New System.Drawing.Size(51, 13)
        Me.lblSOTransportStatus.TabIndex = 95
        Me.lblSOTransportStatus.Text = "สถานะบิล"
        '
        'frmTransportProblem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(489, 531)
        Me.Controls.Add(Me.grbPaneltyFeeInfo)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.grbProblemInfo)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmTransportProblem"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ปัญหางานขนส่ง"
        Me.grbProblemInfo.ResumeLayout(False)
        Me.grbProblemInfo.PerformLayout()
        Me.grbPaneltyFeeInfo.ResumeLayout(False)
        Me.grbPaneltyFeeInfo.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtSO_No As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Id As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomer_Name As System.Windows.Forms.TextBox
    Friend WithEvents lblSO_No As System.Windows.Forms.Label
    Friend WithEvents lblCustomer As System.Windows.Forms.Label
    Friend WithEvents txtTransportManifestNo As System.Windows.Forms.TextBox
    Friend WithEvents lblTransportManifestNo As System.Windows.Forms.Label
    Friend WithEvents txtVehicleID As System.Windows.Forms.TextBox
    Friend WithEvents lblVehicleID As System.Windows.Forms.Label
    Friend WithEvents grbProblemInfo As System.Windows.Forms.GroupBox
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents cboJobProblem As System.Windows.Forms.ComboBox
    Friend WithEvents lblJobProblem As System.Windows.Forms.Label
    Friend WithEvents cboJobSolution As System.Windows.Forms.ComboBox
    Friend WithEvents lblJobSolution As System.Windows.Forms.Label
    Friend WithEvents lblJobSolutionDes As System.Windows.Forms.Label
    Friend WithEvents txtJobSolutionDes As System.Windows.Forms.TextBox
    Friend WithEvents lblJobProblemDes As System.Windows.Forms.Label
    Friend WithEvents txtJobProblemDes As System.Windows.Forms.TextBox
    Friend WithEvents cboResponsibleParty As System.Windows.Forms.ComboBox
    Friend WithEvents lblResponsibleParty As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents lblCustomerShipping As System.Windows.Forms.Label
    Friend WithEvents txtCustomerShippingName As System.Windows.Forms.TextBox
    Friend WithEvents txtCustomerShippingID As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grbPaneltyFeeInfo As System.Windows.Forms.GroupBox
    Friend WithEvents chkTransportCharged As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransportPaid As System.Windows.Forms.CheckBox
    Friend WithEvents lblSOTransportStatus As System.Windows.Forms.Label
    Public WithEvents cboStatusManifest As System.Windows.Forms.ComboBox
    Friend WithEvents chkComplaint_Delivered As System.Windows.Forms.CheckBox
    Friend WithEvents chkDamage_Delivered As System.Windows.Forms.CheckBox
    Friend WithEvents btnJobSolution As System.Windows.Forms.Button
    Friend WithEvents btnJobProblem As System.Windows.Forms.Button
End Class
