<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmInterface_Interval
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
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmInterface_Interval))
        Me.grbTransactionDate = New System.Windows.Forms.GroupBox
        Me.grdInterval = New System.Windows.Forms.DataGridView
        Me.col_Num = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Status = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_Type_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Co_Description = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_DateTime = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button2 = New System.Windows.Forms.Button
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.TimerProcess = New System.Windows.Forms.Timer(Me.components)
        Me.txtInterval = New System.Windows.Forms.TextBox
        Me.lblTime = New System.Windows.Forms.Label
        Me.chkMonday = New System.Windows.Forms.CheckBox
        Me.chkTuesday = New System.Windows.Forms.CheckBox
        Me.chkWednesday = New System.Windows.Forms.CheckBox
        Me.chkThurseday = New System.Windows.Forms.CheckBox
        Me.chkFriday = New System.Windows.Forms.CheckBox
        Me.chkSaturday = New System.Windows.Forms.CheckBox
        Me.chkSunday = New System.Windows.Forms.CheckBox
        Me.grdSelectDay = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblMaxlog = New System.Windows.Forms.Label
        Me.txtMaxLog = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtTime = New System.Windows.Forms.DateTimePicker
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.ChkSelectAllDay = New System.Windows.Forms.CheckBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.ChkSelectAllINF = New System.Windows.Forms.CheckBox
        Me.chkTransferOut = New System.Windows.Forms.CheckBox
        Me.chkCreditNote = New System.Windows.Forms.CheckBox
        Me.chkSalesOrder = New System.Windows.Forms.CheckBox
        Me.chkmsCus = New System.Windows.Forms.CheckBox
        Me.chkTransferIN = New System.Windows.Forms.CheckBox
        Me.chkProduct = New System.Windows.Forms.CheckBox
        Me.chkReturn = New System.Windows.Forms.CheckBox
        Me.chkGoodsIssue = New System.Windows.Forms.CheckBox
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnOK = New System.Windows.Forms.Button
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnSreach = New System.Windows.Forms.Button
        Me.txtSreach = New System.Windows.Forms.TextBox
        Me.grpSreach = New System.Windows.Forms.GroupBox
        Me.ChkE = New System.Windows.Forms.CheckBox
        Me.ChkS = New System.Windows.Forms.CheckBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.dtDate = New System.Windows.Forms.DateTimePicker
        Me.Label2 = New System.Windows.Forms.Label
        Me.lbDescrip = New System.Windows.Forms.Label
        Me.txtDocNo = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Button3 = New System.Windows.Forms.Button
        Me.btnUpdateSOTYPE = New System.Windows.Forms.Button
        Me.grbTransactionDate.SuspendLayout()
        CType(Me.grdInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grdSelectDay.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.grpSreach.SuspendLayout()
        Me.SuspendLayout()
        '
        'grbTransactionDate
        '
        Me.grbTransactionDate.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grbTransactionDate.Controls.Add(Me.grdInterval)
        Me.grbTransactionDate.Location = New System.Drawing.Point(401, 81)
        Me.grbTransactionDate.Name = "grbTransactionDate"
        Me.grbTransactionDate.Size = New System.Drawing.Size(685, 257)
        Me.grbTransactionDate.TabIndex = 0
        Me.grbTransactionDate.TabStop = False
        Me.grbTransactionDate.Text = "Interface Log"
        '
        'grdInterval
        '
        Me.grdInterval.AllowUserToAddRows = False
        Me.grdInterval.AllowUserToDeleteRows = False
        Me.grdInterval.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdInterval.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdInterval.BackgroundColor = System.Drawing.Color.White
        Me.grdInterval.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdInterval.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_Num, Me.Col_Status, Me.Col_Type_Id, Me.Co_Description, Me.Col_DateTime})
        Me.grdInterval.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdInterval.Location = New System.Drawing.Point(3, 16)
        Me.grdInterval.MultiSelect = False
        Me.grdInterval.Name = "grdInterval"
        Me.grdInterval.RowHeadersVisible = False
        Me.grdInterval.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdInterval.Size = New System.Drawing.Size(679, 238)
        Me.grdInterval.TabIndex = 15
        '
        'col_Num
        '
        Me.col_Num.DataPropertyName = "_Index"
        Me.col_Num.HeaderText = "No"
        Me.col_Num.Name = "col_Num"
        Me.col_Num.Width = 50
        '
        'Col_Status
        '
        Me.Col_Status.DataPropertyName = "Status"
        Me.Col_Status.HeaderText = "Status"
        Me.Col_Status.Name = "Col_Status"
        Me.Col_Status.ReadOnly = True
        Me.Col_Status.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.Col_Status.Width = 50
        '
        'Col_Type_Id
        '
        Me.Col_Type_Id.DataPropertyName = "Type_Id"
        Me.Col_Type_Id.HeaderText = "Document No"
        Me.Col_Type_Id.Name = "Col_Type_Id"
        '
        'Co_Description
        '
        Me.Co_Description.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.Co_Description.DataPropertyName = "Description"
        Me.Co_Description.HeaderText = "Description"
        Me.Co_Description.Name = "Co_Description"
        '
        'Col_DateTime
        '
        Me.Col_DateTime.DataPropertyName = "add_date"
        Me.Col_DateTime.HeaderText = "DateADD"
        Me.Col_DateTime.Name = "Col_DateTime"
        Me.Col_DateTime.ReadOnly = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(217, 365)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(85, 23)
        Me.Button2.TabIndex = 85
        Me.Button2.Text = "TestDate"
        Me.Button2.UseVisualStyleBackColor = True
        Me.Button2.Visible = False
        '
        'BackgroundWorker1
        '
        Me.BackgroundWorker1.WorkerReportsProgress = True
        Me.BackgroundWorker1.WorkerSupportsCancellation = True
        '
        'TimerProcess
        '
        Me.TimerProcess.Interval = 60000
        '
        'txtInterval
        '
        Me.txtInterval.BackColor = System.Drawing.Color.White
        Me.txtInterval.Location = New System.Drawing.Point(300, 30)
        Me.txtInterval.MaxLength = 2
        Me.txtInterval.Name = "txtInterval"
        Me.txtInterval.Size = New System.Drawing.Size(30, 20)
        Me.txtInterval.TabIndex = 22
        Me.txtInterval.Text = "5"
        '
        'lblTime
        '
        Me.lblTime.Location = New System.Drawing.Point(252, 34)
        Me.lblTime.Name = "lblTime"
        Me.lblTime.Size = New System.Drawing.Size(44, 13)
        Me.lblTime.TabIndex = 62
        Me.lblTime.Text = "Interval"
        Me.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkMonday
        '
        Me.chkMonday.AutoSize = True
        Me.chkMonday.Location = New System.Drawing.Point(91, 21)
        Me.chkMonday.Name = "chkMonday"
        Me.chkMonday.Size = New System.Drawing.Size(64, 17)
        Me.chkMonday.TabIndex = 64
        Me.chkMonday.Text = "Monday"
        Me.chkMonday.UseVisualStyleBackColor = True
        '
        'chkTuesday
        '
        Me.chkTuesday.AutoSize = True
        Me.chkTuesday.Location = New System.Drawing.Point(91, 44)
        Me.chkTuesday.Name = "chkTuesday"
        Me.chkTuesday.Size = New System.Drawing.Size(67, 17)
        Me.chkTuesday.TabIndex = 65
        Me.chkTuesday.Text = "Tuesday"
        Me.chkTuesday.UseVisualStyleBackColor = True
        '
        'chkWednesday
        '
        Me.chkWednesday.AutoSize = True
        Me.chkWednesday.Location = New System.Drawing.Point(91, 67)
        Me.chkWednesday.Name = "chkWednesday"
        Me.chkWednesday.Size = New System.Drawing.Size(83, 17)
        Me.chkWednesday.TabIndex = 66
        Me.chkWednesday.Text = "Wednesday"
        Me.chkWednesday.UseVisualStyleBackColor = True
        '
        'chkThurseday
        '
        Me.chkThurseday.AutoSize = True
        Me.chkThurseday.Location = New System.Drawing.Point(183, 21)
        Me.chkThurseday.Name = "chkThurseday"
        Me.chkThurseday.Size = New System.Drawing.Size(76, 17)
        Me.chkThurseday.TabIndex = 67
        Me.chkThurseday.Text = "Thurseday"
        Me.chkThurseday.UseVisualStyleBackColor = True
        '
        'chkFriday
        '
        Me.chkFriday.AutoSize = True
        Me.chkFriday.Location = New System.Drawing.Point(183, 44)
        Me.chkFriday.Name = "chkFriday"
        Me.chkFriday.Size = New System.Drawing.Size(54, 17)
        Me.chkFriday.TabIndex = 68
        Me.chkFriday.Text = "Friday"
        Me.chkFriday.UseVisualStyleBackColor = True
        '
        'chkSaturday
        '
        Me.chkSaturday.AutoSize = True
        Me.chkSaturday.Location = New System.Drawing.Point(183, 67)
        Me.chkSaturday.Name = "chkSaturday"
        Me.chkSaturday.Size = New System.Drawing.Size(68, 17)
        Me.chkSaturday.TabIndex = 69
        Me.chkSaturday.Text = "Saturday"
        Me.chkSaturday.UseVisualStyleBackColor = True
        '
        'chkSunday
        '
        Me.chkSunday.AutoSize = True
        Me.chkSunday.Location = New System.Drawing.Point(183, 90)
        Me.chkSunday.Name = "chkSunday"
        Me.chkSunday.Size = New System.Drawing.Size(62, 17)
        Me.chkSunday.TabIndex = 70
        Me.chkSunday.Text = "Sunday"
        Me.chkSunday.UseVisualStyleBackColor = True
        '
        'grdSelectDay
        '
        Me.grdSelectDay.Controls.Add(Me.Label3)
        Me.grdSelectDay.Controls.Add(Me.lblMaxlog)
        Me.grdSelectDay.Controls.Add(Me.txtMaxLog)
        Me.grdSelectDay.Controls.Add(Me.Label1)
        Me.grdSelectDay.Controls.Add(Me.Label4)
        Me.grdSelectDay.Controls.Add(Me.txtTime)
        Me.grdSelectDay.Controls.Add(Me.lblTime)
        Me.grdSelectDay.Controls.Add(Me.txtInterval)
        Me.grdSelectDay.Controls.Add(Me.GroupBox1)
        Me.grdSelectDay.Controls.Add(Me.GroupBox2)
        Me.grdSelectDay.Location = New System.Drawing.Point(12, 5)
        Me.grdSelectDay.Name = "grdSelectDay"
        Me.grdSelectDay.Size = New System.Drawing.Size(383, 295)
        Me.grdSelectDay.TabIndex = 71
        Me.grdSelectDay.TabStop = False
        Me.grdSelectDay.Text = "Select the time and day you want this time with start."
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(11, 33)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(148, 13)
        Me.Label3.TabIndex = 80
        Me.Label3.Text = "Start to automatic process at :"
        '
        'lblMaxlog
        '
        Me.lblMaxlog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblMaxlog.Location = New System.Drawing.Point(202, 59)
        Me.lblMaxlog.Name = "lblMaxlog"
        Me.lblMaxlog.Size = New System.Drawing.Size(92, 13)
        Me.lblMaxlog.TabIndex = 79
        Me.lblMaxlog.Text = "Dispaly"
        Me.lblMaxlog.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMaxLog
        '
        Me.txtMaxLog.BackColor = System.Drawing.Color.White
        Me.txtMaxLog.Location = New System.Drawing.Point(300, 56)
        Me.txtMaxLog.MaxLength = 1000
        Me.txtMaxLog.Name = "txtMaxLog"
        Me.txtMaxLog.Size = New System.Drawing.Size(77, 20)
        Me.txtMaxLog.TabIndex = 78
        Me.txtMaxLog.Text = "5000"
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(333, 33)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(44, 13)
        Me.Label1.TabIndex = 77
        Me.Label1.Text = "Minute"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.Red
        Me.Label4.Location = New System.Drawing.Point(50, 270)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(294, 16)
        Me.Label4.TabIndex = 76
        Me.Label4.Text = "* ถ้าต้องการย่อโปรแกรมให้กด Close แล้ว ตอบ Yes"
        '
        'txtTime
        '
        Me.txtTime.CustomFormat = "HH:mm"
        Me.txtTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtTime.Location = New System.Drawing.Point(169, 30)
        Me.txtTime.Name = "txtTime"
        Me.txtTime.ShowUpDown = True
        Me.txtTime.Size = New System.Drawing.Size(77, 20)
        Me.txtTime.TabIndex = 72
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.ChkSelectAllDay)
        Me.GroupBox1.Controls.Add(Me.chkFriday)
        Me.GroupBox1.Controls.Add(Me.chkThurseday)
        Me.GroupBox1.Controls.Add(Me.chkWednesday)
        Me.GroupBox1.Controls.Add(Me.chkSaturday)
        Me.GroupBox1.Controls.Add(Me.chkTuesday)
        Me.GroupBox1.Controls.Add(Me.chkSunday)
        Me.GroupBox1.Controls.Add(Me.chkMonday)
        Me.GroupBox1.Location = New System.Drawing.Point(11, 76)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(358, 115)
        Me.GroupBox1.TabIndex = 87
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "       Select the day(s) of the week below"
        '
        'ChkSelectAllDay
        '
        Me.ChkSelectAllDay.AutoSize = True
        Me.ChkSelectAllDay.BackColor = System.Drawing.Color.Transparent
        Me.ChkSelectAllDay.Location = New System.Drawing.Point(12, 0)
        Me.ChkSelectAllDay.Name = "ChkSelectAllDay"
        Me.ChkSelectAllDay.Size = New System.Drawing.Size(15, 14)
        Me.ChkSelectAllDay.TabIndex = 85
        Me.ChkSelectAllDay.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.ChkSelectAllINF)
        Me.GroupBox2.Controls.Add(Me.chkTransferOut)
        Me.GroupBox2.Controls.Add(Me.chkCreditNote)
        Me.GroupBox2.Controls.Add(Me.chkSalesOrder)
        Me.GroupBox2.Controls.Add(Me.chkmsCus)
        Me.GroupBox2.Controls.Add(Me.chkTransferIN)
        Me.GroupBox2.Controls.Add(Me.chkProduct)
        Me.GroupBox2.Controls.Add(Me.chkReturn)
        Me.GroupBox2.Controls.Add(Me.chkGoodsIssue)
        Me.GroupBox2.Location = New System.Drawing.Point(11, 193)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(358, 74)
        Me.GroupBox2.TabIndex = 88
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "       Select interface below"
        '
        'ChkSelectAllINF
        '
        Me.ChkSelectAllINF.AutoSize = True
        Me.ChkSelectAllINF.BackColor = System.Drawing.Color.Transparent
        Me.ChkSelectAllINF.Location = New System.Drawing.Point(12, 0)
        Me.ChkSelectAllINF.Name = "ChkSelectAllINF"
        Me.ChkSelectAllINF.Size = New System.Drawing.Size(15, 14)
        Me.ChkSelectAllINF.TabIndex = 86
        Me.ChkSelectAllINF.UseVisualStyleBackColor = False
        '
        'chkTransferOut
        '
        Me.chkTransferOut.AutoSize = True
        Me.chkTransferOut.Enabled = False
        Me.chkTransferOut.Location = New System.Drawing.Point(81, 42)
        Me.chkTransferOut.Name = "chkTransferOut"
        Me.chkTransferOut.Size = New System.Drawing.Size(82, 17)
        Me.chkTransferOut.TabIndex = 81
        Me.chkTransferOut.Text = "TransferOut"
        Me.chkTransferOut.UseVisualStyleBackColor = True
        Me.chkTransferOut.Visible = False
        '
        'chkCreditNote
        '
        Me.chkCreditNote.AutoSize = True
        Me.chkCreditNote.Enabled = False
        Me.chkCreditNote.Location = New System.Drawing.Point(272, 19)
        Me.chkCreditNote.Name = "chkCreditNote"
        Me.chkCreditNote.Size = New System.Drawing.Size(81, 17)
        Me.chkCreditNote.TabIndex = 83
        Me.chkCreditNote.Text = "CreditNotes"
        Me.chkCreditNote.UseVisualStyleBackColor = True
        Me.chkCreditNote.Visible = False
        '
        'chkSalesOrder
        '
        Me.chkSalesOrder.AutoSize = True
        Me.chkSalesOrder.Location = New System.Drawing.Point(81, 19)
        Me.chkSalesOrder.Name = "chkSalesOrder"
        Me.chkSalesOrder.Size = New System.Drawing.Size(81, 17)
        Me.chkSalesOrder.TabIndex = 81
        Me.chkSalesOrder.Text = "Sales Order"
        Me.chkSalesOrder.UseVisualStyleBackColor = True
        '
        'chkmsCus
        '
        Me.chkmsCus.AutoSize = True
        Me.chkmsCus.Location = New System.Drawing.Point(166, 19)
        Me.chkmsCus.Name = "chkmsCus"
        Me.chkmsCus.Size = New System.Drawing.Size(108, 17)
        Me.chkmsCus.TabIndex = 81
        Me.chkmsCus.Text = "Customer Master "
        Me.chkmsCus.UseVisualStyleBackColor = True
        '
        'chkTransferIN
        '
        Me.chkTransferIN.AutoSize = True
        Me.chkTransferIN.Enabled = False
        Me.chkTransferIN.Location = New System.Drawing.Point(10, 42)
        Me.chkTransferIN.Name = "chkTransferIN"
        Me.chkTransferIN.Size = New System.Drawing.Size(74, 17)
        Me.chkTransferIN.TabIndex = 81
        Me.chkTransferIN.Text = "TransferIn"
        Me.chkTransferIN.UseVisualStyleBackColor = True
        Me.chkTransferIN.Visible = False
        '
        'chkProduct
        '
        Me.chkProduct.AutoSize = True
        Me.chkProduct.Location = New System.Drawing.Point(166, 42)
        Me.chkProduct.Name = "chkProduct"
        Me.chkProduct.Size = New System.Drawing.Size(101, 17)
        Me.chkProduct.TabIndex = 81
        Me.chkProduct.Text = "Product Master "
        Me.chkProduct.UseVisualStyleBackColor = True
        '
        'chkReturn
        '
        Me.chkReturn.AutoSize = True
        Me.chkReturn.Enabled = False
        Me.chkReturn.Location = New System.Drawing.Point(10, 19)
        Me.chkReturn.Name = "chkReturn"
        Me.chkReturn.Size = New System.Drawing.Size(58, 17)
        Me.chkReturn.TabIndex = 82
        Me.chkReturn.Text = "Return"
        Me.chkReturn.UseVisualStyleBackColor = True
        Me.chkReturn.Visible = False
        '
        'chkGoodsIssue
        '
        Me.chkGoodsIssue.AutoSize = True
        Me.chkGoodsIssue.Enabled = False
        Me.chkGoodsIssue.Location = New System.Drawing.Point(272, 42)
        Me.chkGoodsIssue.Name = "chkGoodsIssue"
        Me.chkGoodsIssue.Size = New System.Drawing.Size(82, 17)
        Me.chkGoodsIssue.TabIndex = 84
        Me.chkGoodsIssue.Text = "GoodsIssue"
        Me.chkGoodsIssue.UseVisualStyleBackColor = True
        Me.chkGoodsIssue.Visible = False
        '
        'btnExit
        '
        Me.btnExit.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnExit.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ลบรายการ
        Me.btnExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnExit.Location = New System.Drawing.Point(112, 306)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(85, 38)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "   Stop"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnOK
        '
        Me.btnOK.Image = CType(resources.GetObject("btnOK.Image"), System.Drawing.Image)
        Me.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnOK.Location = New System.Drawing.Point(14, 306)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(85, 38)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "   Start"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Click To Show Again"
        Me.NotifyIcon1.Visible = True
        '
        'btnClose
        '
        Me.btnClose.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ออกจากระบบ
        Me.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnClose.Location = New System.Drawing.Point(308, 306)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(85, 38)
        Me.btnClose.TabIndex = 72
        Me.btnClose.Text = "   Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnSreach
        '
        Me.btnSreach.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnSreach.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ค้นหา
        Me.btnSreach.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSreach.Location = New System.Drawing.Point(953, 9)
        Me.btnSreach.Name = "btnSreach"
        Me.btnSreach.Size = New System.Drawing.Size(130, 50)
        Me.btnSreach.TabIndex = 73
        Me.btnSreach.Text = "  SEARCH"
        Me.btnSreach.UseVisualStyleBackColor = True
        '
        'txtSreach
        '
        Me.txtSreach.Location = New System.Drawing.Point(252, 40)
        Me.txtSreach.Name = "txtSreach"
        Me.txtSreach.Size = New System.Drawing.Size(100, 20)
        Me.txtSreach.TabIndex = 74
        '
        'grpSreach
        '
        Me.grpSreach.Controls.Add(Me.ChkE)
        Me.grpSreach.Controls.Add(Me.ChkS)
        Me.grpSreach.Controls.Add(Me.Label6)
        Me.grpSreach.Controls.Add(Me.dtDate)
        Me.grpSreach.Controls.Add(Me.Label2)
        Me.grpSreach.Controls.Add(Me.lbDescrip)
        Me.grpSreach.Controls.Add(Me.txtDocNo)
        Me.grpSreach.Controls.Add(Me.txtSreach)
        Me.grpSreach.Location = New System.Drawing.Point(404, 5)
        Me.grpSreach.Name = "grpSreach"
        Me.grpSreach.Size = New System.Drawing.Size(543, 72)
        Me.grpSreach.TabIndex = 75
        Me.grpSreach.TabStop = False
        Me.grpSreach.Text = "Sreach"
        '
        'ChkE
        '
        Me.ChkE.AutoSize = True
        Me.ChkE.Location = New System.Drawing.Point(396, 16)
        Me.ChkE.Name = "ChkE"
        Me.ChkE.Size = New System.Drawing.Size(81, 17)
        Me.ChkE.TabIndex = 79
        Me.ChkE.Text = "Status Error"
        Me.ChkE.UseVisualStyleBackColor = True
        '
        'ChkS
        '
        Me.ChkS.AutoSize = True
        Me.ChkS.Location = New System.Drawing.Point(396, 42)
        Me.ChkS.Name = "ChkS"
        Me.ChkS.Size = New System.Drawing.Size(100, 17)
        Me.ChkS.TabIndex = 78
        Me.ChkS.Text = "Status Success"
        Me.ChkS.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(10, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(30, 13)
        Me.Label6.TabIndex = 77
        Me.Label6.Text = "Date"
        '
        'dtDate
        '
        Me.dtDate.Checked = False
        Me.dtDate.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtDate.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtDate.Location = New System.Drawing.Point(43, 19)
        Me.dtDate.Name = "dtDate"
        Me.dtDate.ShowCheckBox = True
        Me.dtDate.Size = New System.Drawing.Size(115, 20)
        Me.dtDate.TabIndex = 76
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(176, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(70, 13)
        Me.Label2.TabIndex = 75
        Me.Label2.Text = "DocumentNo"
        '
        'lbDescrip
        '
        Me.lbDescrip.AutoSize = True
        Me.lbDescrip.Location = New System.Drawing.Point(186, 44)
        Me.lbDescrip.Name = "lbDescrip"
        Me.lbDescrip.Size = New System.Drawing.Size(60, 13)
        Me.lbDescrip.TabIndex = 75
        Me.lbDescrip.Text = "Description"
        '
        'txtDocNo
        '
        Me.txtDocNo.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocNo.Location = New System.Drawing.Point(252, 14)
        Me.txtDocNo.Name = "txtDocNo"
        Me.txtDocNo.Size = New System.Drawing.Size(100, 20)
        Me.txtDocNo.TabIndex = 74
        '
        'Button1
        '
        Me.Button1.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ส่งข้อมูล
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(308, 365)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(85, 38)
        Me.Button1.TabIndex = 76
        Me.Button1.Text = "   Export" & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & "   Excel"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "No"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Width = 50
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Status"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewTextBoxColumn2.Width = 50
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "Description"
        Me.DataGridViewTextBoxColumn3.HeaderText = "Description"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "add_date"
        Me.DataGridViewTextBoxColumn4.HeaderText = "DateADD"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'Button3
        '
        Me.Button3.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ค้นหา
        Me.Button3.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button3.Location = New System.Drawing.Point(203, 306)
        Me.Button3.Name = "Button3"
        Me.Button3.Size = New System.Drawing.Size(85, 38)
        Me.Button3.TabIndex = 77
        Me.Button3.Text = "   Log"
        Me.Button3.UseVisualStyleBackColor = True
        '
        'btnUpdateSOTYPE
        '
        Me.btnUpdateSOTYPE.Image = Global.WMS_Site_Topcharoen_Interface.My.Resources.Resources.ค้นหา
        Me.btnUpdateSOTYPE.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnUpdateSOTYPE.Location = New System.Drawing.Point(203, 306)
        Me.btnUpdateSOTYPE.Name = "btnUpdateSOTYPE"
        Me.btnUpdateSOTYPE.Size = New System.Drawing.Size(99, 38)
        Me.btnUpdateSOTYPE.TabIndex = 86
        Me.btnUpdateSOTYPE.Text = "   UpdateSO TYPE"
        Me.btnUpdateSOTYPE.UseVisualStyleBackColor = True
        '
        'frmInterface_Interval
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1035, 350)
        Me.Controls.Add(Me.btnUpdateSOTYPE)
        Me.Controls.Add(Me.Button3)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnSreach)
        Me.Controls.Add(Me.grdSelectDay)
        Me.Controls.Add(Me.grbTransactionDate)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpSreach)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.KeyPreview = True
        Me.Name = "frmInterface_Interval"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Daily Processing Management"
        Me.grbTransactionDate.ResumeLayout(False)
        CType(Me.grdInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grdSelectDay.ResumeLayout(False)
        Me.grdSelectDay.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.grpSreach.ResumeLayout(False)
        Me.grpSreach.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents grbTransactionDate As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents grdInterval As System.Windows.Forms.DataGridView
    Friend WithEvents TimerProcess As System.Windows.Forms.Timer
    Friend WithEvents txtInterval As System.Windows.Forms.TextBox
    Friend WithEvents lblTime As System.Windows.Forms.Label
    Friend WithEvents chkMonday As System.Windows.Forms.CheckBox
    Friend WithEvents chkTuesday As System.Windows.Forms.CheckBox
    Friend WithEvents chkWednesday As System.Windows.Forms.CheckBox
    Friend WithEvents chkThurseday As System.Windows.Forms.CheckBox
    Friend WithEvents chkFriday As System.Windows.Forms.CheckBox
    Friend WithEvents chkSaturday As System.Windows.Forms.CheckBox
    Friend WithEvents chkSunday As System.Windows.Forms.CheckBox
    Friend WithEvents grdSelectDay As System.Windows.Forms.GroupBox
    Friend WithEvents txtTime As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblMaxlog As System.Windows.Forms.Label
    Friend WithEvents txtMaxLog As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkProduct As System.Windows.Forms.CheckBox
    Friend WithEvents chkSalesOrder As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransferOut As System.Windows.Forms.CheckBox
    Friend WithEvents chkmsCus As System.Windows.Forms.CheckBox
    Friend WithEvents chkTransferIN As System.Windows.Forms.CheckBox
    Friend WithEvents NotifyIcon1 As System.Windows.Forms.NotifyIcon
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents chkReturn As System.Windows.Forms.CheckBox
    Friend WithEvents btnSreach As System.Windows.Forms.Button
    Friend WithEvents txtSreach As System.Windows.Forms.TextBox
    Friend WithEvents grpSreach As System.Windows.Forms.GroupBox
    Friend WithEvents lbDescrip As System.Windows.Forms.Label
    Friend WithEvents dtDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents ChkE As System.Windows.Forms.CheckBox
    Friend WithEvents ChkS As System.Windows.Forms.CheckBox
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents chkGoodsIssue As System.Windows.Forms.CheckBox
    Friend WithEvents chkCreditNote As System.Windows.Forms.CheckBox
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ChkSelectAllINF As System.Windows.Forms.CheckBox
    Friend WithEvents ChkSelectAllDay As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtDocNo As System.Windows.Forms.TextBox
    Friend WithEvents col_Num As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Status As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_Type_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Co_Description As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_DateTime As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button3 As System.Windows.Forms.Button
    Friend WithEvents btnUpdateSOTYPE As System.Windows.Forms.Button
End Class
