<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSreachAudit_log
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
        Me.gbCriteria = New System.Windows.Forms.GroupBox
        Me.chkDocument_No = New System.Windows.Forms.CheckBox
        Me.txtDocument_No = New System.Windows.Forms.TextBox
        Me.chkdate = New System.Windows.Forms.CheckBox
        Me.cboUser = New System.Windows.Forms.ComboBox
        Me.chkUser = New System.Windows.Forms.CheckBox
        Me.lblToDate = New System.Windows.Forms.Label
        Me.dtEndDate = New System.Windows.Forms.DateTimePicker
        Me.dtStartDate = New System.Windows.Forms.DateTimePicker
        Me.Cancel = New System.Windows.Forms.Button
        Me.OK = New System.Windows.Forms.Button
        Me.cboLog_Type = New System.Windows.Forms.ComboBox
        Me.chkLog_Type = New System.Windows.Forms.CheckBox
        Me.gbCriteria.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbCriteria
        '
        Me.gbCriteria.Controls.Add(Me.cboLog_Type)
        Me.gbCriteria.Controls.Add(Me.chkLog_Type)
        Me.gbCriteria.Controls.Add(Me.chkDocument_No)
        Me.gbCriteria.Controls.Add(Me.txtDocument_No)
        Me.gbCriteria.Controls.Add(Me.chkdate)
        Me.gbCriteria.Controls.Add(Me.cboUser)
        Me.gbCriteria.Controls.Add(Me.chkUser)
        Me.gbCriteria.Controls.Add(Me.lblToDate)
        Me.gbCriteria.Controls.Add(Me.dtEndDate)
        Me.gbCriteria.Controls.Add(Me.dtStartDate)
        Me.gbCriteria.Location = New System.Drawing.Point(12, 12)
        Me.gbCriteria.Name = "gbCriteria"
        Me.gbCriteria.Size = New System.Drawing.Size(419, 159)
        Me.gbCriteria.TabIndex = 11
        Me.gbCriteria.TabStop = False
        Me.gbCriteria.Text = "เงื่อนไขกรองรายการ"
        '
        'chkDocument_No
        '
        Me.chkDocument_No.AutoSize = True
        Me.chkDocument_No.Location = New System.Drawing.Point(14, 82)
        Me.chkDocument_No.Name = "chkDocument_No"
        Me.chkDocument_No.Size = New System.Drawing.Size(79, 17)
        Me.chkDocument_No.TabIndex = 44
        Me.chkDocument_No.Text = "เลขที่สั่งขาย"
        Me.chkDocument_No.UseVisualStyleBackColor = True
        '
        'txtDocument_No
        '
        Me.txtDocument_No.Location = New System.Drawing.Point(127, 80)
        Me.txtDocument_No.Name = "txtDocument_No"
        Me.txtDocument_No.Size = New System.Drawing.Size(279, 20)
        Me.txtDocument_No.TabIndex = 45
        '
        'chkdate
        '
        Me.chkdate.AutoSize = True
        Me.chkdate.Location = New System.Drawing.Point(14, 29)
        Me.chkdate.Name = "chkdate"
        Me.chkdate.Size = New System.Drawing.Size(74, 17)
        Me.chkdate.TabIndex = 43
        Me.chkdate.Text = "ตั้งแต่วันที่"
        Me.chkdate.UseVisualStyleBackColor = True
        '
        'cboUser
        '
        Me.cboUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUser.FormattingEnabled = True
        Me.cboUser.Location = New System.Drawing.Point(127, 53)
        Me.cboUser.Name = "cboUser"
        Me.cboUser.Size = New System.Drawing.Size(279, 21)
        Me.cboUser.TabIndex = 42
        '
        'chkUser
        '
        Me.chkUser.AutoSize = True
        Me.chkUser.Location = New System.Drawing.Point(14, 55)
        Me.chkUser.Name = "chkUser"
        Me.chkUser.Size = New System.Drawing.Size(46, 17)
        Me.chkUser.TabIndex = 21
        Me.chkUser.Text = "ผู้ใช้"
        Me.chkUser.UseVisualStyleBackColor = True
        '
        'lblToDate
        '
        Me.lblToDate.AutoSize = True
        Me.lblToDate.Location = New System.Drawing.Point(257, 31)
        Me.lblToDate.Name = "lblToDate"
        Me.lblToDate.Size = New System.Drawing.Size(19, 13)
        Me.lblToDate.TabIndex = 19
        Me.lblToDate.Text = "ถึง"
        '
        'dtEndDate
        '
        Me.dtEndDate.CustomFormat = ""
        Me.dtEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtEndDate.Location = New System.Drawing.Point(287, 27)
        Me.dtEndDate.Name = "dtEndDate"
        Me.dtEndDate.Size = New System.Drawing.Size(119, 20)
        Me.dtEndDate.TabIndex = 6
        '
        'dtStartDate
        '
        Me.dtStartDate.CustomFormat = ""
        Me.dtStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtStartDate.Location = New System.Drawing.Point(127, 27)
        Me.dtStartDate.Name = "dtStartDate"
        Me.dtStartDate.Size = New System.Drawing.Size(119, 20)
        Me.dtStartDate.TabIndex = 5
        '
        'Cancel
        '
        Me.Cancel.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.ออกจากระบบ
        Me.Cancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Cancel.Location = New System.Drawing.Point(331, 177)
        Me.Cancel.Name = "Cancel"
        Me.Cancel.Size = New System.Drawing.Size(100, 38)
        Me.Cancel.TabIndex = 13
        Me.Cancel.Text = "ยกเลิก"
        Me.Cancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Cancel.UseVisualStyleBackColor = True
        '
        'OK
        '
        Me.OK.Image = Global.WMS_Site_Topcharoen_P2.My.Resources.Resources.print
        Me.OK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.OK.Location = New System.Drawing.Point(12, 177)
        Me.OK.Name = "OK"
        Me.OK.Size = New System.Drawing.Size(100, 38)
        Me.OK.TabIndex = 12
        Me.OK.Text = "แสดงผล"
        Me.OK.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.OK.UseVisualStyleBackColor = True
        '
        'cboLog_Type
        '
        Me.cboLog_Type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLog_Type.FormattingEnabled = True
        Me.cboLog_Type.Location = New System.Drawing.Point(127, 106)
        Me.cboLog_Type.Name = "cboLog_Type"
        Me.cboLog_Type.Size = New System.Drawing.Size(279, 21)
        Me.cboLog_Type.TabIndex = 47
        '
        'chkLog_Type
        '
        Me.chkLog_Type.AutoSize = True
        Me.chkLog_Type.Location = New System.Drawing.Point(14, 108)
        Me.chkLog_Type.Name = "chkLog_Type"
        Me.chkLog_Type.Size = New System.Drawing.Size(111, 17)
        Me.chkLog_Type.TabIndex = 46
        Me.chkLog_Type.Text = "ประเภททำรายการ"
        Me.chkLog_Type.UseVisualStyleBackColor = True
        '
        'frmSreachAudit_log
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(443, 227)
        Me.Controls.Add(Me.Cancel)
        Me.Controls.Add(Me.gbCriteria)
        Me.Controls.Add(Me.OK)
        Me.Name = "frmSreachAudit_log"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "ค้นหา : Audit_Log"
        Me.gbCriteria.ResumeLayout(False)
        Me.gbCriteria.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbCriteria As System.Windows.Forms.GroupBox
    Friend WithEvents lblToDate As System.Windows.Forms.Label
    Friend WithEvents dtEndDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtStartDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents Cancel As System.Windows.Forms.Button
    Friend WithEvents OK As System.Windows.Forms.Button
    Friend WithEvents chkUser As System.Windows.Forms.CheckBox
    Friend WithEvents cboUser As System.Windows.Forms.ComboBox
    Friend WithEvents chkdate As System.Windows.Forms.CheckBox
    Friend WithEvents chkDocument_No As System.Windows.Forms.CheckBox
    Friend WithEvents txtDocument_No As System.Windows.Forms.TextBox
    Friend WithEvents cboLog_Type As System.Windows.Forms.ComboBox
    Friend WithEvents chkLog_Type As System.Windows.Forms.CheckBox
End Class
