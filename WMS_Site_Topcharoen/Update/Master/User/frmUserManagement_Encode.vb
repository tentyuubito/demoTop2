Imports System.Windows
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports System.Security.Cryptography
Imports System.Text
Imports System.Windows.Forms

Public Class frmUserManagement_Encode
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private EncStringBytes() As Byte
    Private Encoder As New UTF8Encoding
    Private MD5Hasher As New MD5CryptoServiceProvider
    Private gstrFileName As String = ""
    Private gstrLongFilePath As String = ""
    Private gstrAppPath As String = ""
    Private _DEFAULT_ImagePath As String = ""
    Private _User_Index As String = ""
    Public Property User_Index() As String
        Get
            Return _User_Index
        End Get
        Set(ByVal value As String)
            _User_Index = value
        End Set
    End Property


    Private _User_ID_Old As String = ""
    Public Property User_ID_Old() As String
        Get
            Return _User_ID_Old
        End Get
        Set(ByVal value As String)
            _User_ID_Old = value
        End Set
    End Property


    Private _User_Name_Old As String
    Public Property User_Name_Old() As String
        Get
            Return _User_Name_Old
        End Get
        Set(ByVal value As String)
            _User_Name_Old = value
        End Set
    End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmUserT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ------ Set Language Begin ------

            Dim objLang As New WMS_STD_Master.W_Language
            objLang.SwitchLanguage(Me, 2043)
            objLang.SW_Language_Column(Me, Me.grdConfigItem, 2043)
            SetImage_Path()
            set_UserComboboxGroup()
            getDepartment()
            Me.getDistributionCenter()
            grdConfigItem.AutoGenerateColumns = False

            Dim strWV_GroupType As String = WV_GroupType
            Select Case strWV_GroupType
                Case "0", "4"

                Case Else
                    'TabControl1.TabPages.Remove(TabPage1)
            End Select

            Select Case SaveType
                Case 0 'Save

                Case 1 'Update
                    Dim objse_User As New se_User(se_User.enuOperation_Type.SEARCH)
                    objse_User.SelectByIndex(User_Index)
                    Dim odtse_User As New DataTable
                    odtse_User = objse_User.DataTable

                    If odtse_User.Rows.Count > 0 Then
                        With odtse_User.Rows(0)
                            Me.User_ID_Old = .Item("User_ID").ToString
                            User_Name_Old = .Item("UserName").ToString
                            User_Index = .Item("User_Index").ToString
                            txtUserID.Text = .Item("User_ID").ToString
                            txtUserName.Text = .Item("UserName").ToString
                            txtFullName.Text = .Item("UserFullName").ToString
                            txtPasswd.Text = ""
                            txtPasswdConfirm.Text = ""
                            cbGroupIndex.SelectedValue = .Item("Group_index").ToString
                            cboDepartMent.SelectedValue = .Item("Department_index").ToString
                            Me.cboDistributionCenter.SelectedValue = .Item("DistributionCenter_Index")
                            If Not IsDBNull(.Item("Image_Path")) Then
                                Me.picEmployee.ImageLocation = .Item("Image_Path")
                                Me.gstrAppPath = .Item("Image_Path")

                            End If

                        End With
                    End If
                    GetConfigItem()
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub GetConfigItem()


        Dim objConfig As New config_UserByCustomer(config_UserByCustomer.enuOperation_Type.SEARCH)
        Dim DTtb_Order As DataTable = New DataTable
        Dim AddWhereString As String = " AND se_User.User_index = '" & _User_Index & "'"

        Try
            objConfig.GetDataForConfig(AddWhereString)
            'objConfig.GetDataForConfig("")
            objConfig.User_Index = _User_Index
            objConfig.CheckConfigIsNull()
            objConfig.GetDataForConfig("And config_UserByCustomer.user_index='" & _User_Index & "'")

            Dim dtDataSource As New DataTable
            dtDataSource = objConfig.DataTable
            dtDataSource.PrimaryKey = New DataColumn() {dtDataSource.Columns("Customer_Index")}
            Me.grdConfigItem.DataSource = objConfig.DataTable

        Catch ex As Exception
            Throw ex
        Finally
            objConfig = Nothing
            DTtb_Order = Nothing
        End Try

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            'If txtUserID.Text.Trim = "" Then
            '    W_MSG_Information_ByIndex(74)
            '    txtUserID.Focus()
            '    Exit Sub
            'End If

            'If txtPasswd.Text.Trim = "" Then
            '    W_MSG_Information_ByIndex(75)
            '    txtPasswd.Focus()
            '    Exit Sub
            'End If

            If txtUserName.Text.Trim = "" Then
                W_MSG_Information_ByIndex(75)
                txtUserName.Focus()
                Exit Sub
            End If

            ' Check Data Username and PassWD 
            Dim strLenUserName As Integer = 0
            Dim strLenPasswd As Integer = 0
            If Me.txtUserName.Text.Trim <> "" Then

                Dim objUser As New se_User_Update(se_User_Update.enuOperation_Type.SEARCH)
                Dim odtUser As DataTable
                ' 12/02/2010 By TaTa Check Data Username 
                objUser.CheckUserNameBypUserId(txtUserName.Text.Trim, txtUserID.Text)
                odtUser = objUser.GetDataTable
                If odtUser.Rows.Count > 0 Then
                    W_MSG_Information("UserName ซ้ำ")
                    Exit Sub
                End If
                strLenUserName = Me.txtUserName.Text.Length

                If strLenUserName < 5 Then
                    W_MSG_Information("ความยาว UserName ต้องมีมากกว่า 5 ตัวอักษร")
                    Exit Sub
                End If
            End If

            If Me.txtPasswd.Text.Trim <> "" Then
                strLenPasswd = Me.txtPasswd.Text.Length
                If strLenPasswd < 5 Then
                    W_MSG_Information("ความยาว Password ต้องมีมากกว่า 5 ตัวอักษร")
                    Exit Sub
                End If
            End If

            If Me.txtPasswd.Text.Trim <> "" Then
                If Me.txtPasswd.Text.Trim <> Me.txtPasswdConfirm.Text.Trim Then
                    W_MSG_Information("ข้อมูล Password ไม่ถูกต้อง")
                    Exit Sub
                End If
            End If
            SaveUserNameAndPassWD()
            ' saveUser(User_Index, txtUserID.Text.Trim, txtPasswd.Text.Trim)
            ' Me.btnSave.Enabled = False

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' </summary>
    ''' <remarks>
    ''' Date 12/02/2010
    ''' By TaTa 
    '''      - Fix bug Check ข้อมูล Nothing
    '''      - เพิ่ม Function การป้อนข้อมล  User Id (ป้อนหรือไม่ก้อได้)
    '''      - แก้ไขการ Function Add / Edit มันส่งข้อมูลไม่ถูกต้อง
    ''' </remarks>
    Sub SaveUserNameAndPassWD()

        Try
            ' Save UserName/ PassWD To se_Uuser 
            ' If Me.txtPasswd.Text.Trim <> "" Then
            ' Check Data
            Dim ose_UserSEARCH As New se_User(se_User.enuOperation_Type.SEARCH)
            Dim odtUserCustomer As DataTable
            ose_UserSEARCH.SearchByUserName(txtUserID.Text)
            odtUserCustomer = ose_UserSEARCH.GetDataTable
            Dim strUserName As String = ""
            Dim strPassWD As String = ""
            Dim StrStatusUser As Integer = 0



            If Me.txtUserName.Text.Trim <> "" Then
                strUserName = Me.txtUserName.Text.Trim
            Else
                strUserName = Me.txtFullName.Text.Trim
            End If
            'If Me.chkStatus.Checked = True Then
            '    StrStatusUser = 0
            'Else
            '    StrStatusUser = -1
            'End If

            If odtUserCustomer.Rows.Count = 0 Then
                strPassWD = setPassWD_To_MD5(Me.txtPasswd.Text.Trim)
            Else
                If Me.txtPasswd.Text.Trim <> "" Then
                    strPassWD = setPassWD_To_MD5(Me.txtPasswd.Text.Trim)
                Else
                    strPassWD = odtUserCustomer.Rows(0).Item("userPasswd").ToString
                End If
            End If
            Dim strGroupIndex As String = ""
            If Not cbGroupIndex.SelectedValue Is Nothing Then
                strGroupIndex = cbGroupIndex.SelectedValue
            Else
                strGroupIndex = ""
            End If
            Dim strDepartMent As String = ""
            If Not cboDepartMent.SelectedValue Is Nothing Then
                strDepartMent = cboDepartMent.SelectedValue
            Else
                strDepartMent = ""
            End If
            Dim strDistributionCenter_Index As String = ""
            If (Me.cboDistributionCenter.SelectedIndex >= 0) Then
                strDistributionCenter_Index = Me.cboDistributionCenter.SelectedValue
            End If
            Saveimages()
            Select Case SaveType

                Case 0 'Add New
                    Dim ose_UserInsert As New se_User_Update(se_User_Update.enuOperation_Type.ADDNEW)
                    ose_UserInsert.SaveData_Encode("", "", strGroupIndex, strUserName, strPassWD, txtFullName.Text, strDepartMent, "", "", 0, strDistributionCenter_Index, gstrAppPath)
                Case 1 'Edit
                    Dim ose_UserUpdate As New se_User_Update(se_User_Update.enuOperation_Type.UPDATE)
                    ose_UserUpdate.SaveData_Encode(odtUserCustomer.Rows(0).Item("user_index").ToString, odtUserCustomer.Rows(0).Item("user_ID").ToString, strGroupIndex, strUserName, strPassWD, txtFullName.Text, strDepartMent, "", "", 0, strDistributionCenter_Index, gstrAppPath)
            End Select


            Me.Close()

            'Me.txtUserName.Text = strUserName
            'Me.txtPasswd.Text = ""
            'Me.txtPasswdConfirm.Text = ""

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub Saveimages()
        Try
            'Dim objFile As IO.File
            If gstrLongFilePath = "" Then
                Exit Sub
            End If

            picEmployee.Image.Save(gstrAppPath, Imaging.ImageFormat.Jpeg)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Sub saveUser(ByVal Index As String, ByVal ID As String, ByVal Description As String)
    '    Try
    '        Dim strPassWD As String = ""
    '        If Me.txtPasswd.Text.Trim <> "" Then
    '            strPassWD = setPassWD_To_MD5(Me.txtPasswd.Text.Trim)
    '        End If


    '        Select Case SaveType
    '            Case 0 'Add New

    '                Dim objCheckIdse_User As New se_User(se_User.enuOperation_Type.SEARCH)
    '                Dim BCheckId As Boolean = objCheckIdse_User.isChckID(txtUserID.Text)
    '                BCheckId = objCheckIdse_User.isChckUserName(txtUserName.Text)

    '                If BCheckId Then
    '                    W_MSG_Information_ByIndex(45)
    '                    btnSave.Enabled = True
    '                    Exit Sub
    '                Else
    '                    Dim objse_User As New se_User(se_User.enuOperation_Type.ADDNEW)
    '                    'If Me.User_Name_Old <> txtUserName.Text.Trim Then
    '                    '    If objse_User.isChckUserName(txtUserName.Text) = True Then
    '                    '        W_MSG_Information_ByIndex(67)
    '                    '        Exit Sub
    '                    '    End If
    '                    'End If
    '                    objse_User.SaveData("", txtUserID.Text, cbGroupIndex.SelectedValue.ToString, txtUserName.Text.Trim, strPassWD, txtFullName.Text.Trim, cbGroupIndex.SelectedValue.ToString)
    '                    W_MSG_Information_ByIndex(1)
    '                    Me.Close()
    '                End If

    '            Case 1 'Update
    '                Dim objse_User As New se_User(se_User.enuOperation_Type.UPDATE)

    '                If Me.User_ID_Old <> txtUserID.Text.Trim Then
    '                    If objse_User.isChckID(txtUserID.Text) = True Then
    '                        W_MSG_Information_ByIndex(67)
    '                        Exit Sub
    '                    End If
    '                End If
    '                If Me.User_Name_Old <> txtUserName.Text.Trim Then
    '                    If objse_User.isChckUserName(txtUserName.Text) = True Then
    '                        W_MSG_Information_ByIndex(67)
    '                        Exit Sub
    '                    End If
    '                End If
    '                objse_User.SearchByUserName(txtUserName.Text)
    '                Dim strUserName As String = ""
    '                Dim strPassWD As String = ""
    '                Dim StrStatusUser As Integer = 0

    '                If Me.chkStatus.Checked = True Then
    '                    StrStatusUser = 0
    '                Else
    '                    StrStatusUser = -1
    '                End If


    '                W_MSG_Information_ByIndex(1)
    '                Me.Close()
    '        End Select
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub



    Sub set_UserComboboxGroup()
        Dim objSeGroup As New se_Group(se_Group.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            cbGroupIndex.Items.Clear()
            objSeGroup.SelectAll()
            objDT = objSeGroup.DataTable
            cbGroupIndex.DataSource = objDT
            cbGroupIndex.DisplayMember = "group_des"
            cbGroupIndex.ValueMember = "group_index"
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objSeGroup = Nothing
        End Try
    End Sub

    Private Sub getDepartment()
        Dim objms_Department As New ms_Department(ms_Department.enuOperation_Type.SEARCH)
        Dim objDTms_Department As DataTable = New DataTable

        Try
            objms_Department.SearchData_Click("", "")
            objDTms_Department = objms_Department.DataTable

            cboDepartMent.DisplayMember = "Description"
            cboDepartMent.ValueMember = "Department_Index"
            cboDepartMent.DataSource = objDTms_Department

        Catch ex As Exception
            Throw ex
        Finally
            objms_Department = Nothing
            objDTms_Department = Nothing
        End Try

    End Sub

    Private Sub btnResetPassword_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnResetPassword.Click
        Try
            txtPasswd.Text = "123456"
            txtPasswdConfirm.Text = "123456"
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


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

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim objConfig As New config_UserByCustomer(config_UserByCustomer.enuOperation_Type.SEARCH)
        Dim DTtb_Order As DataTable = New DataTable
        Try
            objConfig.DataSource = grdConfigItem.DataSource
            objConfig.User_Index = Me._User_Index

            objConfig.Update()
            MessageBox.Show("บันทึกเรียบร้อย", "บันทึกข้อมูล")

        Catch ex As Exception
            Throw ex
        Finally
            objConfig = Nothing
            DTtb_Order = Nothing
        End Try
    End Sub

    Private Sub grdConfigItem_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdConfigItem.CellClick
        Try
            'Dong(เขียนใหม่)
            If e.RowIndex <= -1 Then
                Exit Sub
            End If
            If e.ColumnIndex <= -1 Then
                Exit Sub
            End If

            If grdConfigItem.RowCount > 0 Then

                Dim Column_Index As String = grdConfigItem.CurrentCell.ColumnIndex
                Select Case Column_Index
                    Case Is = grdConfigItem.Columns("col_IsDefault").Index
                        Dim strCustomer_Index As String = Me.grdConfigItem.Rows(e.RowIndex).Cells("col_Customer_Index").Value.ToString
                        CType(Me.grdConfigItem.DataSource, DataTable).AcceptChanges()

                        Dim dtDataSource As New DataTable
                        dtDataSource = CType(Me.grdConfigItem.DataSource, DataTable)
                        dtDataSource.PrimaryKey = New DataColumn() {dtDataSource.Columns("Customer_Index")}

                        'Update Old Row
                        Dim drArrRow() As DataRow = dtDataSource.Select("IsDefault=1")
                        If drArrRow.Length > 0 Then
                            Dim drRow As DataRow
                            drRow = dtDataSource.Rows.Find(drArrRow(0)("Customer_Index"))
                            drRow.BeginEdit()
                            drRow("IsDefault") = False
                            drRow.EndEdit()
                        End If

                        'Update New Row
                        Dim drNewRow As DataRow
                        drNewRow = dtDataSource.Rows.Find(strCustomer_Index)
                        drNewRow.BeginEdit()
                        drNewRow("IsDefault") = True
                        drNewRow.EndEdit()

                    Case Is = grdConfigItem.Columns("col_IsUse").Index

                        If grdConfigItem.Rows(e.RowIndex).Cells("Col_IsUse").Value = False Then
                            'grdConfigItem.Rows(e.RowIndex).Cells("col_IsDefault").Value = True
                        Else
                            grdConfigItem.Rows(e.RowIndex).Cells("col_IsDefault").Value = False
                        End If

                End Select


            End If


            'Dim dt As DataTable
            'dt = grdConfigItem.DataSource
            'Dim objAsset As New tb_AssetLocationBalance
            'If e.RowIndex <= -1 Then
            '    Exit Sub
            'End If
            'If e.ColumnIndex <= -1 Then
            '    Exit Sub
            'End If

            'Dim boolCheck As Boolean = False

            'Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
            '    Case "col_IsDefault"
            '        Dim i As Integer
            '        If grdConfigItem.Rows(e.RowIndex).Cells("col_IsDefault").Value = 0 Then
            '            boolCheck = False
            '        Else
            '            boolCheck = True
            '        End If

            '        For i = 0 To grdConfigItem.Rows.Count - 1
            '            'If i = e.RowIndex Then
            '            '    If dt.Rows(i).Item("Isuse") = True Then
            '            '        If dt.Rows(i).Item("Isdefault") = True Then
            '            '            dt.Rows(i).Item("Isdefault") = 0
            '            '        Else
            '            '            dt.Rows(i).Item("Isdefault") = 1
            '            '        End If
            '            '    Else
            '            '        dt.Rows(i).Item("Isdefault") = 0
            '            '    End If
            '            'Else
            '            '    dt.Rows(i).Item("Isdefault") = 0
            '            'End If

            '            grdConfigItem.Rows(i).Cells("col_IsDefault").Value = 0

            '        Next
            '        If grdConfigItem.Rows(e.RowIndex).Cells("Col_IsUse").Value = 0 Then
            '            grdConfigItem.Rows(e.RowIndex).Cells("col_IsDefault").Value = 0
            '        Else
            '            grdConfigItem.Rows(e.RowIndex).Cells("col_IsDefault").Value = Not (boolCheck)
            '        End If

            '        'dt.Rows(e.RowIndex).Item("Isdefault") = 1
            '        dt.AcceptChanges()
            '        grdConfigItem.DataSource = dt
            '        '=================
            '    Case "Col_IsUse"
            '        'If grdConfigItem.Rows(e.RowIndex).Cells("Col_IsUse").Value = True Then
            '        '    grdConfigItem.Rows(e.RowIndex).Cells("col_IsDefault").Value = False
            '        'End If
            '        'dt.AcceptChanges()
            'End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If grdConfigItem.RowCount > 0 Then
                'For iRow As Integer = 0 To grdConfigItem.RowCount - 1
                '    grdConfigItem.Rows(iRow).Cells("Col_IsUse").Value = Me.chkSelectAll.Checked
                'Next
                CType(Me.grdConfigItem.DataSource, DataTable).AcceptChanges()

                For Each drRow As DataRow In CType(Me.grdConfigItem.DataSource, DataTable).Rows
                    drRow("IsUse") = CBool(Me.chkSelectAll.Checked)
                Next

            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub frmUserManagement_Encode_Update_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
        
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigUser
                    frm.ShowDialog()
                    Dim oFunction As New WMS_STD_Master.W_Language
                    oFunction.SwitchLanguage(Me, 2043)
                    oFunction.SW_Language_Column(Me, Me.grdConfigItem, 2043)
                    oFunction = Nothing
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getDistributionCenter()
        Dim objms_DistributionCenter As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
        Dim objDTms_DistributionCenter As DataTable = New DataTable

        Try
            objms_DistributionCenter.GetAllAsDataTable("")
            objDTms_DistributionCenter = objms_DistributionCenter.DataTable

            cboDistributionCenter.DisplayMember = "Description"
            cboDistributionCenter.ValueMember = "DistributionCenter_Index"
            cboDistributionCenter.DataSource = objDTms_DistributionCenter

        Catch ex As Exception
            Throw ex
        Finally
            objms_DistributionCenter = Nothing
            objDTms_DistributionCenter = Nothing
        End Try

    End Sub
    Sub SetImage_Path()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Dim appSet As New Configuration.AppSettingsReader()
            Dim strUseLocalPath As String = appSet.GetValue("UseLocalPath", GetType(String)) 'AppSettings("UseLocalPath").ToString

            If strUseLocalPath = 0 Then
                Me._DEFAULT_ImagePath = appSet.GetValue("IMAGE_PATH_Employee", GetType(String)) 'AppSettings("IMAGE_PATH_Employee").ToString
            Else

                objCustomSetting.GetConfig_Value("DEFAULT_Image_Path", "")
                objDT = objCustomSetting.DataTable
                If objDT.Rows.Count > 0 Then
                    Me._DEFAULT_ImagePath = objDT.Rows(0).Item("Config_Value").ToString
                Else
                    Me._DEFAULT_ImagePath = ""
                End If
            End If


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    Private Sub btnAddPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPic.Click
        Try
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                ' Display image
                picEmployee.ImageLocation = OpenFileDialog1.FileName
                gstrLongFilePath = OpenFileDialog1.FileName

                'intFileNameLength = InStr(1, StrReverse(gstrLongFilePath), "\")
                'gstrFileName = Mid(gstrLongFilePath, (Len(gstrLongFilePath) - intFileNameLength) + 2)
                gstrFileName = txtUserID.Text & ".JPG" 'lbSKUIndex.Text.ToString & ".JPG"
                ' Path Program
                ' gstrAppPath = Application.StartupPath

                gstrAppPath = _DEFAULT_ImagePath & gstrFileName


            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class