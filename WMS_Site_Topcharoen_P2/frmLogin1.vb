Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports System.IO
Imports System.Configuration.ConfigurationManager
'Imports WMS_OP_Invoice
Imports System.Security.Cryptography
Imports System.Text
'Imports WMS_OP_PO.DBType_AS400
Imports System.Threading
Imports System.Globalization
'Imports WMS_SM_Version_Control_Assembly
Public Class frmLogin1

    Private EncStringBytes() As Byte
    Private Encoder As New UTF8Encoding
    Private MD5Hasher As New MD5CryptoServiceProvider
    Private _Status_Use_Main As Boolean = False
    Public Description_Th As String = ""
    Public Description_Eng As String = ""
    Dim IsChange As Boolean = False
    Private _userIndex As String = ""
    Private strPassWd As String = ""
    Private HardwareKey As String = ""
    Private DB_Name As String = ""
    Private ServerName As String = ""
    Private appSet As New Configuration.AppSettingsReader()

    Private Sub frmLogin1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'Dim aa As String = Format(Now, "yyyyMMddHHmmss")
            Try
                Dim objServiceControl As New ServiceControl.Service
                WV_HARDWAREKEY = objServiceControl.GetHardWareKey()
                HardwareKey = WV_HARDWAREKEY

            Catch ex As Exception
                W_MSG_Error("ไม่สามารถติดต่อ ServerControl ได้ !!")
                Exit Sub
            End Try


            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            Me.getBranch()
            lblVersation.Text &= Me.ProductVersion.ToString


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub getDepartment()
        Dim objms_Department As New ms_Department(ms_Department.enuOperation_Type.SEARCH)
        Dim objDTms_Department As DataTable = New DataTable

        Try
            objms_Department.SearchData_Click("", "")
            objDTms_Department = objms_Department.DataTable

            cboDepartment.DisplayMember = "Description"
            cboDepartment.ValueMember = "Department_Index"
            cboDepartment.DataSource = objDTms_Department

        Catch ex As Exception
            Throw ex
        Finally
            objms_Department = Nothing
            objDTms_Department = Nothing
        End Try

    End Sub

    Private Sub getBranch()
        Dim objms_Branch As New ms_Branch(ms_Branch.enuOperation_Type.SEARCH)
        Dim objDTms_Branch As DataTable = New DataTable

        Try
            objms_Branch.SearchData_Click("", "")
            objDTms_Branch = objms_Branch.DataTable

            cboDepartment.DisplayMember = "Branch_Name"
            cboDepartment.ValueMember = "Branch_Id"
            cboDepartment.DataSource = objDTms_Branch
            If objDTms_Branch.Rows.Count > 0 Then
                Dim dtServerKey As New DataTable
                dtServerKey = objms_Branch.GetServerKey()
                If dtServerKey.Rows.Count > 0 Then
                    DB_Name = dtServerKey.Rows(0).Item(0).ToString
                    ServerName = dtServerKey.Rows(0).Item(1).ToString
                End If
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Branch = Nothing
            objDTms_Branch = Nothing
        End Try

    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
          
            If Me.txtUserName.Text.Trim = "" Then
                W_MSG_Information_ByIndex("400055")
                Me.txtUserName.Focus()
                Exit Sub
            End If

            If Me.txtPassword.Text.Trim = "" Then
                W_MSG_Information_ByIndex("400056")
                Me.txtPassword.Focus()
                Exit Sub
            End If

            SelectDB()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub SelectDB()

        Dim Branch_Id As String = cboDepartment.SelectedValue.ToString

        Dim objms_Branch As New ms_Branch(ms_Branch.enuOperation_Type.SEARCH)
        Dim oDTms_Branch As New DataTable

        Try
            'Reset connect begin Origin connection
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            objms_Branch.SearchBranch("", "", Branch_Id)
            oDTms_Branch = objms_Branch.DataTable

            If oDTms_Branch.Rows.Count > 0 Then
                With oDTms_Branch.Rows(0)

                    WV_ConnectionString = .Item("Branch_DB_Conncet").ToString 'Set Select branch
                    Me.Description_Th = .Item("Description_Th").ToString
                    Me.Description_Eng = .Item("Description_Eng").ToString

                    If oDTms_Branch.Columns.Contains("Version") Then
                        If (Me.ProductVersion.ToString) <> (.Item("Version").ToString) Then
                            W_MSG_Information(GetMessage_Data("400057") & .Item("Version").ToString)
                            Me.UpdateVersion()
                            'Me.Close()
                        Else
                            Login()
                        End If
                    End If

                End With
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Sub Login()
        Try
            Dim oclsLogin As New clsLogin
            Dim statusLogin As Boolean = False

            With oclsLogin
                Dim strPassWd As String = ""

                strPassWd = setPassWD_To_MD5(txtPassword.Text.Trim)
                statusLogin = .CheckUser(txtUserName.Text.Trim.ToString, strPassWd)


                Dim dtBranch As New DataTable
                If statusLogin = True Then

                    'Comment fot multi warehouse login.
                    WV_Branch_ID = 1 'cboDepartment.SelectedValue.ToString, Fix for โปรแกรมไม่รองรับ Branch Id อื่น
                    WV_Department_Des = cboDepartment.Text

                    Dim objCustomSetting As New config_CustomSetting
                    Dim objDT As DataTable = New DataTable
                    objCustomSetting.GetConfig_Value("Report_Mode", "")
                    objDT = objCustomSetting.DataTable
                    If objDT.Rows.Count > 0 Then
                        ' 0 = Developer Mode (Get Report Path at table config_Report_Developer)
                        ' 1 = User Mode
                        If objDT.Rows(0).Item("Config_Value").ToString = "0" Then ' 
                            WV_Report_Path = ""
                        Else
                            WV_Report_Path = Application.StartupPath
                        End If
                    Else
                        WV_Report_Path = Application.StartupPath
                    End If

                    Dim oSession As New WMS_STD_Master.UserSession
                    Dim oBolCheck As Boolean = oSession.CheckSession
                    If oBolCheck = False Then
                        Exit Sub
                    End If

                    Dim oAudit_Log As New Sy_Audit_Log
                    oAudit_Log.Insert(Sy_Audit_Log.Log_Type.User_Login)
                    Me.Hide()

                    '-----------------------------------------------------------------
                    frmMainC1.Description_Th = Me.Description_Th
                    frmMainC1.Description_Eng = Me.Description_Eng
                    frmMainC1.ShowDialog()
                    '-----------------------------------------------------------------
                    'frmMain_Edu.ShowDialog()
                    Me.Show()
                    Me.txtUserName.Clear()
                    Me.txtPassword.Clear()
                    txtUserName.Focus()
                    _Status_Use_Main = True
                Else
                    W_MSG_Information_ByIndex("500019")

                    Me.txtPassword.Clear()
                    Me.txtUserName.Focus()
                    Me.txtUserName.SelectAll()
                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Function Login(ByVal UserName As String, ByVal Password As String) As Boolean
        Try
            Dim oclsLogin As New clsLogin
            Dim statusLogin As Boolean = False
            Dim oAudit_Log As New Sy_Audit_Log
            With oclsLogin


                strPassWd = setPassWD_To_MD5(Password)
                statusLogin = .CheckUser(UserName, strPassWd)
                _userIndex = oclsLogin.userIndex
                If statusLogin = True Then
                    Return True
                Else
                    W_MSG_Information_ByIndex("500019")
                    Me.txtPassword.Focus()
                    Return False

                End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Sub PassLogin()
        'Pass Login Mode Can't View Report Print Out
        Dim frmmain As New frmMainC1
        WV_User_Index = "0010000000000"
        WV_UserName = "admin"
        WV_UserFullName = "administator"

        WV_GroupUser_Index = "0010000000000"

        WV_Branch_ID = "0"
        Me.Hide()
        frmmain.ShowDialog()
        Me.Show()
        Me.txtUserName.Clear()
        Me.txtPassword.Clear()
        txtUserName.Focus()
    End Sub

    Private Sub LoginPass(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtUserName.KeyDown
        Try
            'By Pass Login Ctrl + Shift +  C (TextBox UserName only)
            If e.Shift = True And e.Control = True Then
                If e.KeyCode = Keys.C Then
                    PassLogin()
                End If
            End If

            If e.Control = True AndAlso (e.Shift = True AndAlso e.Alt = True AndAlso (e.KeyCode = Keys.B)) Then
                Dim frm As New frmUpdateBranch
                frm.SourceConfig = WV_ConnectionString
                frm.Branch_Id = cboDepartment.SelectedValue

                frm.ShowDialog()
            End If


            If e.KeyCode = Keys.Enter Then
                Me.txtPassword.Focus()
            End If

            If e.Control = True AndAlso ((e.KeyCode = Keys.D)) Then
                Me.txtUserName.Text = "dhong"
                Me.txtPassword.Text = "dhong"
                Me.SelectDB()
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub Rad_thai_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbthai.Click
        WV_Language = enmLanguage.Thai
    End Sub

    Private Sub Rad_Eng_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles rdbEng.Click
        WV_Language = enmLanguage.English
    End Sub

    Private Sub txtPassword_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPassword.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Me.txtUserName.Text.Trim = "" Then
                    W_MSG_Information_ByIndex("400055")
                    Me.txtUserName.Focus()
                    Exit Sub
                End If

                If Me.txtPassword.Text.Trim = "" Then
                    W_MSG_Information_ByIndex("400056")
                    Me.txtPassword.Focus()
                    Exit Sub
                End If

                SelectDB()

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub frmLogin_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If _Status_Use_Main = True Then
                Dim oAudit_Log As New Sy_Audit_Log
                oAudit_Log.Insert(Sy_Audit_Log.Log_Type.User_Logout)
                If IsDisplayAlert() = True Then
                    If W_MSG_Confirm_ByIndex(100009) = Windows.Forms.DialogResult.Yes Then
                        'Dim frm As New frmReCalCulate
                        'Me.Hide()
                        'frm.ShowDialog()
                    End If
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Function IsDisplayAlert() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_ALERT_DAYEND", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim blnShow As Boolean = False
            If objDT.Rows.Count > 0 Then
                blnShow = True
            Else
                blnShow = False
            End If

            Return blnShow

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function

    Private Function setPassWD_To_MD5(ByVal EncString As String) As String
        Try
            'Variable Declarations
            Dim RanGen As New Random
            Dim RanString As String = ""
            Dim MD5String As String

            EncStringBytes = Encoder.GetBytes(EncString & RanString)

            'Generates the MD5 Byte Array
            EncStringBytes = MD5Hasher.ComputeHash(EncStringBytes)

            'Affixing Salt information into the MD5 hash
            MD5String = BitConverter.ToString(EncStringBytes)
            MD5String = MD5String.Replace("-", Nothing)

            Return MD5String

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'user Edit Password By User Add by : Top  21/01/2014
#Region "Edit Password By User"
    Private Sub bntPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        PassLogin()
    End Sub

    Private Sub cmdSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSave.Click
        If txtOldUserName.Text.Trim() = "" Then
            MessageBox.Show("กรุณาป้อน UserName ก่อน !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtOldUserName.Focus()
            Exit Sub
        End If
        If txtOldPassword.Text.Trim() = "" Then
            MessageBox.Show("กรุณาป้อนรหัส Password !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtOldPassword.Focus()
            Exit Sub
        End If
        If txtNewPassword.Text.Trim() = "" Then
            MessageBox.Show("กรุณาป้อนรหัส Password ใหม่ !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNewPasswordAgain.Focus()
            Exit Sub
        End If
        If txtNewPasswordAgain.Text.Trim() = "" Then
            MessageBox.Show("กรุณาป้อนรหัส Password ใหม่อีกครั้ง !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNewPasswordAgain.Focus()
            Exit Sub
        End If
        If txtNewPassword.Text.Length < 5 Then
            MessageBox.Show("รหัส Password ต้องมีจำนวนตัวอักษรระหว่าง 5-16 ตัวอักษร !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPassword.Focus()
            txtPassword.SelectAll()
            Exit Sub
        End If
        If txtNewPasswordAgain.Text.Length < 5 Then
            MessageBox.Show("รหัส Password ต้องมีจำนวนตัวอักษรระหว่าง 5-16 ตัวอักษร !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNewPasswordAgain.Focus()
            txtNewPasswordAgain.SelectAll()
            Exit Sub
        End If
        If txtOldPassword.Text.Trim() = txtNewPassword.Text.Trim() Then
            MessageBox.Show("รหัส Password เก่ากับใหม่ ห้ามเหมือนกัน !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNewPassword.Text = ""
            txtNewPasswordAgain.Text = ""
            txtNewPassword.Focus()
            Exit Sub
        End If
        If txtNewPassword.Text.Trim() <> txtNewPasswordAgain.Text.Trim() Then
            MessageBox.Show("คุณป้อนรหัส Password ไม่เหมือนกัน กรุณาป้อนใหม่ !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtNewPasswordAgain.Focus()
            txtNewPasswordAgain.SelectAll()
            Exit Sub
        End If

        'Dim Score As Double = CheckStrongPassword(txtNewPassword.Text)
        'If Score <= 4 Then
        '    MessageBox.Show("รหัส Password ที่คุณกำหนด มีรูปแบบง่ายต่อการคาดเดา กรุณากำหนดรหัส Password ใหม่ !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Information)
        '    txtNewPassword.Text = ""
        '    txtNewPasswordAgain.Text = ""
        '    txtNewPassword.Focus()
        '    Exit Sub
        'End If

        If MessageBox.Show("คุณต้องการแก้ไขรหัส Password ใช่หรือไม่?", "คำยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
            Dim CLogin As Boolean = False

            SelectDB()
            ' Login()


            CLogin = Login(txtOldUserName.Text, txtOldPassword.Text)
            If CLogin = True Then
                'Update username And Password ใหม่

                Try
                    Dim objDB As New DBType_SQLServer
                    strPassWd = setPassWD_To_MD5(txtNewPassword.Text)
                    objDB.DBExeNonQuery(" Update se_User set userName='" & txtOldUserName.Text & "',userPasswd='" & strPassWd & "' where user_index='" & _userIndex & "' ")

                    MessageBox.Show("แก้ไขรหัส Password เรียบร้อยแล้ว !!!", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    ClearChangePassword()
                    ' btnEditPass.Focus()
                    btnEditPass_Click(sender, e)


                Catch ex As Exception
                    MessageBox.Show("ไม่สามารถแก้ไขรหัส Password ได้เนื่องจาก " & ex.Message, "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End Try
            Else
                MessageBox.Show("UserName หรือ Password ที่คุณป้อน ไม่ถูกต้อง ไม่สามารถเปลี่ยนรหัส Password ได้ !!!", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                ClearChangePassword()
                txtOldUserName.Focus()
            End If
        End If
    End Sub

    Private Function CheckStrongPassword(ByVal password As String) As Double
        Dim _i As Integer = 0
        Dim _point As Double = 0
        Dim _muliply As Double = 2
        Dim _PwdCount As Double = password.Length
        Dim _pwd As Char() = password.ToCharArray()
        _point = _PwdCount / 5

        If _PwdCount <= 4 Then
            _muliply -= 0.5
        End If
        If _PwdCount >= 12 Then
            _point += 0.4
            _muliply += 0.7
        ElseIf _PwdCount >= 8 Then
            _point += 0.7
            _muliply += 0.4
        ElseIf _PwdCount > 4 Then
            _point += 0.5
        End If

        Dim _IsDigit As Boolean = False
        Dim _IsLetter As Boolean = False
        Dim _IsPunctuation As Boolean = False
        Dim _IsSeparator As Boolean = False
        Dim _IsSymbol As Boolean = False
        For _i = 0 To _pwd.Length - 1
            If GetCharType(_pwd(_i)) = "Digit" Then
                _IsDigit = True
            End If
            If GetCharType(_pwd(_i)) = "Letter" Then
                _IsLetter = True
            End If
            If GetCharType(_pwd(_i)) = "Punctuation" Then
                _IsPunctuation = True
                _point += 0.1
                _muliply += 0.1
            End If
            If GetCharType(_pwd(_i)) = "Separator" Then
                _IsSeparator = True
                _point += 0.1
                _muliply += 0.1
            End If
            If GetCharType(_pwd(_i)) = "Symbol" Then
                _IsSymbol = True
                _point += 0.1
                _muliply += 0.1
            End If
        Next

        If (_IsDigit = True) AndAlso (_IsLetter = False) AndAlso (_IsPunctuation = False) AndAlso (_IsSeparator = False) AndAlso (_IsSymbol = False) Then
            _point -= 0.8
            _muliply -= 0.8
        End If
        If (_IsDigit = False) AndAlso (_IsLetter = True) AndAlso (_IsPunctuation = False) AndAlso (_IsSeparator = False) AndAlso (_IsSymbol = False) Then
            _point -= 0.8
            _muliply -= 0.8
        End If
        If (_IsDigit = True) AndAlso (_IsLetter = True) Then
            _point += 0.1
            _muliply += 0.1
        End If
        If (_IsPunctuation = True) AndAlso (_IsSeparator = True) AndAlso (_IsSymbol = True) Then
            _point += 1
            _muliply += 1
        End If
        If (_IsDigit = True) AndAlso (_IsLetter = True) AndAlso (_IsPunctuation = True) AndAlso (_IsSeparator = True) AndAlso (_IsSymbol = True) Then
            _point += 1.3
            _muliply += 1.3
        End If
        Dim _final As Double = 0
        _final = _point * _muliply
        If _final > 10 Then
            _final = 10
        ElseIf _final < 0 Then
            _final = 0
        End If
        Return _final
    End Function

    Private Function GetCharType(ByVal chr As Char) As String
        Dim _CharType As String = "Unknown"
        If Char.IsControl(chr) Then
            _CharType = "Control"
        ElseIf Char.IsDigit(chr) Then
            _CharType = "Digit"
        ElseIf Char.IsLetter(chr) Then
            _CharType = "Letter"
        ElseIf Char.IsPunctuation(chr) Then
            _CharType = "Punctuation"
        ElseIf Char.IsSeparator(chr) Then
            _CharType = "Separator"
        ElseIf Char.IsSymbol(chr) Then
            _CharType = "Symbol"
        ElseIf Char.IsWhiteSpace(chr) Then
            _CharType = "Whitespace"
        Else
            _CharType = "Unknown"
        End If
        Return _CharType
    End Function

    Private Sub ClearChangePassword()
        txtOldUserName.Text = ""
        txtOldPassword.Text = ""
        txtNewPassword.Text = ""
        txtNewPasswordAgain.Text = ""
    End Sub

    Private Sub btnEditPass_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditPass.Click

    End Sub

    Private Sub txtNewPasswordAgain_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtNewPasswordAgain.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                cmdSave_Click(sender, e)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles btnEditPass.LinkClicked
        IsChange = Not IsChange
        If IsChange = True Then
            Me.Width = 653
            txtOldUserName.Focus()
        Else
            Me.Width = 336
            txtUserName.Focus()
        End If
    End Sub

    Private Sub lblImportLicense_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblImportLicense.LinkClicked
        Try
            SelectDB()
            Dim frm As New WMS_STD_Master.frmImportLicense
            frm.HARDWAREKEY = HardwareKey
            frm.DB_Name = DB_Name
            frm.SERVERNAME = ServerName
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub lblUpdate_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles lblUpdate.LinkClicked
        Try
            Me.UpdateVersion()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub UpdateVersion()
        Try
            Dim _fileUpdateVersion As String = System.IO.Path.Combine(Application.StartupPath(), "WMS_UpdateVersion.exe")
            If System.IO.File.Exists(_fileUpdateVersion) Then
                If W_MSG_Confirm("ต้องการอัพเดทโปรแกรมหรือไม่?") = Windows.Forms.DialogResult.Yes Then

                    Shell(Application.StartupPath & "\" & "WMS_UpdateVersion.exe")
                    Application.Exit()
                End If
            Else
                W_MSG_Error(String.Format("ไม่พบไฟล์ {0}", _fileUpdateVersion))
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

 
End Class