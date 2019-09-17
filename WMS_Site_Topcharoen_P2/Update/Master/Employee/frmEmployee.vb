Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula
Imports System.Configuration.ConfigurationSettings
Imports System.Drawing
Imports System.Windows.Forms
Imports WMS_STD_Master
Public Class frmEmployee

#Region "   Property"

    Public SaveType As Integer = 0
    Private _Employee_Index As String
    Private _DEFAULT_ImagePath As String = ""

    ' Variables for product images.
    Private gstrFileName As String = ""
    Private gstrLongFilePath As String = ""
    Private gstrAppPath As String = ""

    Public Property Employee_Index() As String
        Get
            Return _Employee_Index
        End Get
        Set(ByVal value As String)
            _Employee_Index = value
        End Set
    End Property

    Public Property DEFAULT_ImagePath() As String
        Get
            Return _DEFAULT_ImagePath
        End Get
        Set(ByVal value As String)
            _DEFAULT_ImagePath = value
        End Set
    End Property

#End Region
   
#Region "   FORM LOAD"

    Private Sub frmEmployeeHeader_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2006)

            Me.OpenFileDialog1.Filter = "Bmp files (*.bmp)|*.bmp|" + "Gif files (*.gif)|*.gif|" + "Jpeg files (*.jpg;*.jpeg)|*.jpg;*.jpeg|" + "Png files (*.png)|*.png|" + "All graphic files (*.bmp;*.gif;*.jpg;*.jpeg;*.png)|*.bmp;*.gif;*.jpg;*.jpeg;*.png"

            AddcbProvince()
            AddcbDistrict()
            AddcbDepartment()
            AddcbGroup()
            Me.cboDepartment.SelectedIndex = 0
            Me.cboDistrict.SelectedIndex = 0
            Me.cboEmployeeGroup.SelectedIndex = 0
            Me.cboProvince.SelectedIndex = 0
            Me.SetImage_Path()
            Select Case SaveType
                Case 0
                    'Dim objAutoNumber As New Sy_AutoNumber
                    'txtEmpID.Text = objAutoNumber.getSys_Value("Customer_Index")

                Case 1
                    Dim objms_Employee As New ms_Employee(ms_Employee.enuOperation_Type.SEARCH)
                    Dim odtms_CustomerType As New DataTable
                    objms_Employee.SearchData_Click("", _Employee_Index)
                    odtms_CustomerType = objms_Employee.DataTable

                    If odtms_CustomerType.Rows.Count > 0 Then
                        With odtms_CustomerType.Rows(0)
                            Me.txtEmpID.Text = .Item("Employee_Id").ToString
                            Me.txtEmpName.Text = .Item("Employee_name").ToString
                            Me._Employee_Index = .Item("Employee_Index").ToString
                            Me.txtEmpLastName.Text = .Item("Employee_lastname").ToString
                            If IsDBNull(.Item("Employee_Group_Index")) Then
                                Me.cboEmployeeGroup.SelectedValue = "-11"
                            Else
                                Me.cboEmployeeGroup.SelectedValue = .Item("Employee_Group_Index").ToString
                            End If
                            If IsDBNull(.Item("Department_Index")) Then
                                Me.cboDepartment.SelectedValue = "-11"
                            Else
                                Me.cboDepartment.SelectedValue = .Item("Department_Index").ToString
                            End If
                            If IsDBNull(.Item("Province_Index")) Then
                                Me.cboProvince.SelectedValue = "-11"
                            Else
                                Me.cboProvince.SelectedValue = .Item("Province_Index").ToString
                            End If
                            If IsDBNull(.Item("District_Index")) Then
                                Me.cboDistrict.SelectedValue = "-11"
                            Else
                                Me.cboDistrict.SelectedValue = .Item("District_Index").ToString
                            End If

                            Me.txtPosition.Text = .Item("Employee_Position").ToString
                            Me.txtAddress.Text = .Item("Address").ToString
                            Me.txtPostcode.Text = .Item("Postcode").ToString
                            Me.txtTel.Text = .Item("Tel").ToString
                            Me.txtFax.Text = .Item("Fax").ToString
                            Me.txtMobile.Text = .Item("Mobile").ToString
                            Me.txtEmail.Text = .Item("Email").ToString
                           
                            If IsDBNull(.Item("Image_Path").ToString) Then
                                picEmployee.ImageLocation = ""
                            ElseIf .Item("Image_Path").ToString = "" Then
                                picEmployee.ImageLocation = ""
                            Else
                                ' gstrAppPath = Application.StartupPath
                                ' gstrAppPath = gstrAppPath & "\images\" & .Item("Image_Path").ToString
                                gstrAppPath = .Item("Image_Path").ToString
                                picEmployee.ImageLocation = gstrAppPath
                            End If
                            Me.txtNational_Id.Text = .Item("National_Id").ToString
                            Me.txtDriver_License_No.Text = .Item("Driver_License_No").ToString
                            If IsDBNull(.Item("Birth_Date")) Then
                                Me.dtpBirth_Date.Value = Now.Date
                            Else
                                Me.dtpBirth_Date.Value = CDate(.Item("Birth_Date").ToString).ToString("dd/MM/yyyy")
                            End If
                            If IsDBNull(.Item("Black_list")) Then
                                Me.chkBlackList.Checked = False
                            Else
                                Me.chkBlackList.Checked = .Item("Black_list")
                            End If


                            If .Item("Gender").ToString = "M" Then
                                Me.rdbGender_M.Checked = True
                            Else
                                Me.rdbGender_W.Checked = True
                            End If
                            Me.txtAttachment1.Text = .Item("Attachment1").ToString
                            Me.txtAttachment2.Text = .Item("Attachment2").ToString
                            Me.txtAttachment3.Text = .Item("Attachment3").ToString
                            If IsDBNull(.Item("Recruit_Date")) Then
                                Me.dtpRecruit_Date.Value = Now.Date
                            Else
                                Me.dtpRecruit_Date.Value = CDate(.Item("Recruit_Date").ToString).ToString("dd/MM/yyyy")
                            End If

                            Me.txtRecruit_By.Text = .Item("Recruit_By").ToString
                        End With
                    End If
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   LOAD MASTER TO COMBO FUNCTIONS"
    Sub AddcbDepartment()
        Try
            Dim objDpm As New ms_Department(ms_Department.enuOperation_Type.SEARCH)
            Dim dtDpm As New DataTable
            objDpm.SearchData_Click("", "")
            dtDpm = objDpm.GetDataTable
            Dim str(2) As String
            str(0) = "-11"
            str(1) = "-11"
            str(2) = "--- ไม่กรอกข้อมูล ---"
            dtDpm.Rows.Add(str)
            With cboDepartment
                .DisplayMember = "Description"
                .ValueMember = "Department_Index"
                .DataSource = dtDpm
            End With
         
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AddcbGroup()
        Try
            Dim objGroup As New ms_Employee_Group(ms_Employee_Group.enuOperation_Type.SEARCH)
            Dim dtGroup As New DataTable
            objGroup.SearchData_Click("", "")
            dtGroup = objGroup.GetDataTable
            Dim str(2) As String
            str(0) = "-11"
            str(1) = "-11"
            str(2) = "--- ไม่กรอกข้อมูล ---"
            dtGroup.Rows.Add(str)
            With cboEmployeeGroup
                .DisplayMember = "Description"
                .ValueMember = "Employee_Group_Index"
                .DataSource = dtGroup
            End With
           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AddcbCountry()
        Dim objClassDB As New ms_Country(ms_Title.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable


        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            'cboCountry.DataSource = objDT
            'cboCountry.DisplayMember = "Country_Name"
            'cboCountry.ValueMember = "Country_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Sub AddcbProvince()
        Dim objProvince As New ms_Province(ms_Province.enuOperation_Type.SEARCH)
        Dim objProvinceDT As DataTable = New DataTable
        Try
            objProvince.SearchData_Click("", "")
            objProvinceDT = objProvince.DataTable
            Dim str(2) As String
            str(0) = "-11"
            str(1) = "-11"
            str(2) = "--- ไม่กรอกข้อมูล ---"
            objProvinceDT.Rows.Add(str)
            With cboProvince
                .DisplayMember = "Province"
                .ValueMember = "Province_Index"
                .DataSource = objProvinceDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objProvinceDT = Nothing
            objProvince = Nothing
        End Try

    End Sub

    Sub AddcbDistrict()
        Dim objDistrict As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDistrictDT As DataTable = New DataTable
        If cboProvince.SelectedValue = "-11" Then
            Exit Sub
        End If
        objDistrict.SearchData_Click("", cboProvince.SelectedValue.ToString)
        objDistrictDT = objDistrict.DataTable

        Dim str(4) As String

        str(0) = "-11"
        str(1) = "--- ไม่กรอกข้อมูล ---"
        str(2) = "-11"
        str(3) = "-11"
        str(4) = "--- ไม่กรอกข้อมูล ---"
        objDistrictDT.Rows.Add(str)
        With cboDistrict
            .DataSource = objDistrictDT
            .DisplayMember = "District"
            .ValueMember = "District_Index"
        End With
      
    End Sub
#End Region

#Region "   Get CustomSetting"

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

#End Region

#Region "   ValueChanged"

    Private Sub DateTimePicker1_ValueChanged_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.txtPosition.Text = dtpRecruit_Date.Value.ToString("yyyy/MM/dd")

    End Sub

    Private Sub cboProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProvince.SelectedIndexChanged
        Dim objDistrict As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            If cboProvince.SelectedValue = Nothing Then
                Exit Sub
            End If
            objDistrict.SearchData_Click("", cboProvince.SelectedValue.ToString)
            objDT = objDistrict.DataTable
            Dim str(4) As String
            Str(0) = "-11"
            Str(1) = "--- ไม่กรอกข้อมูล ---"
            Str(2) = "-11"
            Str(3) = "-11"
            Str(4) = "--- ไม่กรอกข้อมูล ---"
            objDT.Rows.Add(str)
            With cboDistrict
                .DataSource = objDT
                .DisplayMember = "District"
                .ValueMember = "District_Index"
            End With
           

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objDistrict = Nothing
        End Try

    End Sub

#End Region

#Region "   Event TextBox"

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtEmpName.Text = "" Then
                W_MSG_Information("กรุณาระบุ ชื่อพนักงาน")
                Exit Sub

            End If

            saveEmployee()
            W_MSG_Information_ByIndex(1)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAddPic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPic.Click

        Try
            'If _Employee_Index = "" Then
            '    W_MSG_Information("กรุณาบันทึกข้อมูล พนักงาน ก่อน")
            'End If

            ' Dim intFileNameLength As Integer = 0

            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                ' Display image
                picEmployee.ImageLocation = OpenFileDialog1.FileName
                gstrLongFilePath = OpenFileDialog1.FileName

                'intFileNameLength = InStr(1, StrReverse(gstrLongFilePath), "\")
                'gstrFileName = Mid(gstrLongFilePath, (Len(gstrLongFilePath) - intFileNameLength) + 2)
                gstrFileName = txtEmpID.Text & ".JPG" 'lbSKUIndex.Text.ToString & ".JPG"
                ' Path Program
                ' gstrAppPath = Application.StartupPath

                gstrAppPath = _DEFAULT_ImagePath & gstrFileName


            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnRemovePic_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemovePic.Click
        Try
            IO.File.Delete(gstrAppPath)
            picEmployee.ImageLocation = ""
            gstrAppPath = ""
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region "   SAVE Employee   "

    Sub saveEmployee()
        'Dim objEmployee As New ms_Employee(ms_Employee.enuOperation_Type.ADDNEW)
        Try
            Dim objEmployee As New ms_Employee(ms_Employee.enuOperation_Type.NULL)

            With objEmployee
                .Employee_Id = Me.txtEmpID.Text
                .Employee_name = Me.txtEmpName.Text
                .Employee_lastname = Me.txtEmpLastName.Text
                .Employee_Position = Me.txtPosition.Text
                .Department_Index = Me.cboDepartment.SelectedValue.ToString
                .Employee_Group_Index = Me.cboEmployeeGroup.SelectedValue.ToString
                .Address = Me.txtAddress.Text
                .Country_Index = "1000000203"
                .District_Index = Me.cboDistrict.SelectedValue.ToString
                .Province_Index = Me.cboProvince.SelectedValue.ToString
                .Postcode = Me.txtPostcode.Text
                .Tel = Me.txtTel.Text
                .Fax = Me.txtFax.Text
                .Mobile = Me.txtMobile.Text
                .Email = Me.txtEmail.Text
                '.Str1 = Me.txtStr1.Text
                '.Str2 = Me.txtStr2.Text
                '.Str3 = Me.txtStr3.Text
                '.Str4 = Me.txtStr4.Text
                '.Str5 = Me.txtStr5.Text
                '.Str6 = Me.txtStr6.Text
                '.Str7 = Me.txtStr7.Text
                '.Str8 = Me.txtStr8.Text
                '.Str9 = Me.txtStr9.Text
                '.Str10 = Me.txtStr10.Text
                .add_by = WV_UserName
                .add_branch = WV_Branch_ID
                .update_by = WV_UserName
                .update_branch = WV_Branch_ID
                .Image_Path = gstrAppPath

                .National_Id = Me.txtNational_Id.Text
                .Driver_License_No = Me.txtDriver_License_No.Text
                .Birth_Date = Me.dtpBirth_Date.Value
                'If Me.chkBlacklist.Checked = True Then
                '    .Black_List = 1
                'Else
                '    .Black_List = 0
                'End If
                .Black_List = Me.chkBlackList.Checked
                If Me.rdbGender_M.Checked = True Then
                    .Gender = "M"
                ElseIf rdbGender_W.Checked = True Then
                    .Gender = "W"
                End If
                .Attachment1 = Me.txtAttachment1.Text
                .Attachment2 = Me.txtAttachment2.Text
                .Attachment3 = Me.txtAttachment3.Text
                .Recruit_Date = Me.dtpRecruit_Date.Value
                .Recruit_By = Me.txtRecruit_By.Text

                'Copy File Image 
                Saveimages()
            End With

            Select Case SaveType
                Case 0 'Add New
                    objEmployee.Operation = ms_Employee.enuOperation_Type.ADDNEW
                    objEmployee.SaveData()

                    Me.txtEmpID.Text = objEmployee.Employee_Id
                    Me.txtEmpID.ReadOnly = True

                Case 1 'Update
                    If Me.txtEmpID.Text.Trim = "" Then
                        W_MSG_Information("กรุณาป้อนรหัสพนักงาน")
                        Exit Sub
                    End If
                    objEmployee.Operation = ms_Employee.enuOperation_Type.UPDATE
                    objEmployee.Employee_Index = Me._Employee_Index
                    objEmployee.SaveData()
            End Select

            _Employee_Index = ""

            Me.Close()
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
#End Region


    Private Sub btnGroupEmployee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupEmployee.Click
        Try
            Dim frm As New frmEmployee_Group
            frm.Icon = Me.Icon
            frm.ShowDialog()
            AddcbGroup()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmEmployee_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigEmployee
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2006)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class