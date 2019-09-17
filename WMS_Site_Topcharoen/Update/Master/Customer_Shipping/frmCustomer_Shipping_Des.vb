Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Imports System.Drawing
Public Class frmCustomer_Shipping_Des
    Dim _SaveType As Integer = 0
    Dim ClMsg As New Message
    Public _Customer_Shipping_Index As String = ""
    Public _Customer_Shipping_Id As String = ""
    Public _Customer_Index As String = ""

    Dim _CustomerIndex As String = ""
    Dim _CustomerID As String = ""
    Dim _CustomerName As String = ""
    Dim Begin_flag As String = ""

    Public Property CustomerIndex() As String
        Get
            Return _CustomerIndex
        End Get
        Set(ByVal Value As String)
            _CustomerIndex = Value
        End Set
    End Property

    'Public Property CustomerName() As String
    '    Get
    '        Return _CustomerName
    '    End Get
    '    Set(ByVal Value As String)
    '        _CustomerName = Value
    '    End Set
    'End Property

    'Public Property CustomerID() As String
    '    Get
    '        Return _CustomerID
    '    End Get
    '    Set(ByVal Value As String)
    '        _CustomerID = Value
    '    End Set
    'End Property

    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Index = Value
        End Set
    End Property


    Public Property SaveType() As Integer
        Get
            Return _SaveType
        End Get
        Set(ByVal Value As Integer)
            _SaveType = Value
        End Set
    End Property


    Public Property Customer_Shipping_Index() As String
        Get
            Return _Customer_Shipping_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Index = Value
        End Set
    End Property

    Private _USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION As Boolean = True
    Private _USE_CUSTOMER_CUSTOMERSHIPPING As Boolean = True

    Private Sub frmCustomer_Des_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2003)
            oFunction.SW_Language_Column(Me, Me.grdCustomer, 2003)

            ' Me.Icon = frmMain.Icon
            AddcbNameType()

            AddcbCustomerType()
            AddcbBillingType()

            Me.AddcbCountry() 'ประเทศ
            Me.AddProvince() 'จังหวัด
            Me.AddCity(Me.cboProvince.SelectedValue) 'อำเภอ
            Me.AddcbTown(Me.cboCity.SelectedValue) 'ตำบล

            cboCountry.Text = "THAILAND"
            Me.getComboRoute()
            Me.getComboRegion()
            
            'Me.USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION() 'COMMENT
            'Set for ksl.
            Me.grbCustomer.Visible = False
            Me.USE_CUSTOMER_CUSTOMERSHIPPING()


            Select Case SaveType
                Case 0 'Save
                    txtCustomer_Name.Text = _CustomerName
                    btnAddCustomer_Shipp.Enabled = False
                    BtnEditCustomer_Shipp.Enabled = False
                    btnDeleteCustomer_Shipp.Enabled = False
                Case 1 'Update
                    txtCustomer_Shipping_ID.Enabled = False
                    btnAddCustomer_Shipp.Enabled = True
                    BtnEditCustomer_Shipp.Enabled = True
                    btnDeleteCustomer_Shipp.Enabled = True
                    showCustomer_forEdit()
                    ShowGrid()
            End Select

            If Not (String.IsNullOrEmpty(Me._Customer_Index)) Then
                Me.getCustomer()
            End If


            'DEIMOS SETTING ***********************
            ' tabCustomer_Info.TabPages(1).Dispose()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
Private Sub getComboRoute()
        Dim objClassDB As New ms_Route(ms_Route.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            cboRoute.BeginUpdate()
            With cboRoute
                .DisplayMember = "Description"
                .ValueMember = "Route_Index"
                .DataSource = objDT
            End With

            cboRoute.EndUpdate()
            If cboRoute.Items.Count = 0 Then Exit Sub

            getComboSubRoute(cboRoute.SelectedValue.ToString)
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Private Sub getComboRegion()
        Dim objClassDB As New ms_TransportRegion(ms_TransportRegion.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable

            cboRegion.BeginUpdate()
            With cboRegion
                .DisplayMember = "Description"
                .ValueMember = "TransportRegion_Index"
                .DataSource = objDT
            End With

            cboRegion.EndUpdate()
            If cboRegion.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Private Sub getComboSubRoute(ByVal vRoute_Index As String)
        Dim objClassDB As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable(vRoute_Index)
            objDT = objClassDB.DataTable

            cboSubRoute.BeginUpdate()
            With cboSubRoute
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = objDT
            End With

            cboSubRoute.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Sub USE_CUSTOMER_CUSTOMERSHIPPING()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            If objCustomSetting.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                Me._USE_CUSTOMER_CUSTOMERSHIPPING = True
            Else
                Me._USE_CUSTOMER_CUSTOMERSHIPPING = False
                Me.grbCustomer.Visible = False
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            If objCustomSetting.getConfig_Key_USE("USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION") Then
                Me._USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION = True
            Else
                Me._USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION = False
                Me.tabCustomer_Info.TabPages.Remove(Me.tabCustomer_Receive_Location)
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub


#Region "Event Control"
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub cboNational_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
        Try
            If cboCountry.Text.ToString.Trim = "THAILAND" Then
                cboCity.Visible = True
                cboProvince.Visible = True
                txtCity.Visible = False
                txtProvince.Visible = False

            Else
                cboCity.Visible = False
                cboProvince.Visible = False
                txtCity.Visible = True
                txtProvince.Visible = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region
#Region "Function"
    Sub SaveData()
        Try
            Dim SaveStatus As Boolean
            Dim Province_Index As String = ""
            Dim District As String = ""
            Dim Country As String = ""

            Province_Index = Me.cboProvince.SelectedValue.ToString
            District = Me.cboCity.SelectedValue.ToString
            If Me.cboCountry.SelectedValue Is Nothing Then
                Country = ""
            Else
                Country = Me.cboCountry.SelectedValue.ToString
            End If


            If Country = "1000000203" Then
                Me.txtCity.Text = ""
                Me.txtProvince.Text = ""
            End If
            If String.IsNullOrEmpty(Me._Customer_Index) Then
                Me._Customer_Index = ""
            End If

            Select Case SaveType
                Case 0 'Save New
                    Dim objDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)

                    Dim ChekId As Boolean = objDB.isExistID(txtCustomer_Shipping_ID.Text.Trim, txtCustomer_Id.Text)
                    If ChekId Then

                        W_MSG_Information_ByIndex(45)
                        Exit Sub

                    End If

                    If cboRegion.SelectedValue <> Nothing Then
                        objDB.TransportRegion_Index = cboRegion.SelectedValue
                    End If
                    If cboRoute.SelectedValue <> Nothing Then
                        objDB.Route_Index = cboRoute.SelectedValue
                    End If
                    If cboSubRoute.SelectedValue <> Nothing Then
                        objDB.SubRoute_Index = cboSubRoute.SelectedValue
                    End If
                    SaveStatus = objDB.SaveData("", _Customer_Index, cboTitle.Text, txtName.Text, cboCustomerType.SelectedValue.ToString, txtAddress.Text, District, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtContactName1.Text, txtContactName2.Text, txtContactName3.Text, txtBarcode.Text, txtRemark.Text, txtCustomer_Shipping_ID.Text, Country, txtCity.Text, txtProvince.Text, txtName2.Text)
                    Dim _sql_ex As New DBType_SQLServer

                    _sql_ex.DBExeNonQuery(String.Format("Update ms_Customer_Shipping set Credit_Term = '{0}', INT_U = Null , Town_index = '{2}'  where Customer_Shipping_Index = '{1}' ", txtCredit_Term.Text, objDB.Customer_Shipping_Index, CboTown.SelectedValue))
                    _Customer_Shipping_Index = objDB.Customer_Shipping_Index()
                    _Customer_Shipping_Id = objDB.Str1()
                 
                    'Aoto Save Customer Shipping Location 
                    Dim objCus_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.ADDNEW)
                    objCus_Shipping_Location.SaveData("", _Customer_Shipping_Id, _Customer_Shipping_Index, txtName.Text, txtAddress.Text, District, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtRemark.Text, txtCity.Text, txtProvince.Text, txtContactName1.Text, txtContactName2.Text, txtContactName3.Text, objDB.Route_Index, cboCountry.SelectedValue.ToString, Begin_flag, txtCustomer_Shipping_ID.Text, objDB.SubRoute_Index, objDB.TransportRegion_Index) ', objDB.Route_Index, objDB.SubRoute_Index, objDB.TransportRegion_Index) ', txtCity.Text, txtProvince.Text)

                    Me.txtCustomer_Shipping_ID.Text = objDB.Str1
                   
                    If SaveStatus Then
                        SaveType = 1
                        W_MSG_Information_ByIndex(1)
                        Exit Sub

                    Else
                        W_MSG_Information_ByIndex(2)
                        Exit Sub
                    End If

                Case 1 'Update
                    Dim objDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.UPDATE)
                    If cboRegion.SelectedValue <> Nothing Then
                        objDB.TransportRegion_Index = cboRegion.SelectedValue
                    End If
                    If cboRoute.SelectedValue <> Nothing Then
                        objDB.Route_Index = cboRoute.SelectedValue
                    End If
                    If cboSubRoute.SelectedValue <> Nothing Then
                        objDB.SubRoute_Index = cboSubRoute.SelectedValue
                    End If
                    SaveStatus = objDB.SaveData(_Customer_Shipping_Index, _Customer_Index, cboTitle.Text, txtName.Text, cboCustomerType.SelectedValue.ToString, txtAddress.Text, District, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtContactName1.Text, txtContactName2.Text, txtContactName3.Text, txtBarcode.Text, txtRemark.Text, txtCustomer_Shipping_ID.Text, Country, txtCity.Text, txtProvince.Text, txtName2.Text)

                    Dim _Sql_ex As New DBType_SQLServer
                    _Sql_ex.DBExeNonQuery(String.Format("Update ms_Customer_Shipping set Credit_Term = {0}, INT_U = Null , Town_index = '{2}'  where Customer_Shipping_Index = '{1}' ", txtCredit_Term.Text, objDB.Customer_Shipping_Index, CboTown.SelectedValue))
                    'ADD
                    'Dim objCus_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.UPDATE)
                    'objCus_Shipping_Location.SaveData("", _Customer_Shipping_Id, _Customer_Shipping_Index, txtName.Text, txtAddress.Text, District, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtRemark.Text, txtCity.Text, txtProvince.Text, txtContactName1.Text, txtContactName2.Text, txtContactName3.Text, objDB.Route_Index, cboCountry.SelectedValue.ToString, Begin_flag, txtCustomer_Shipping_ID.Text, objDB.SubRoute_Index, objDB.TransportRegion_Index) ', objDB.Route_Index, objDB.SubRoute_Index, objDB.TransportRegion_Index) ', txtCity.Text, txtProvince.Text)

                    Me.txtCustomer_Shipping_ID.Text = objDB.Str1
                    'COMMENT BY TON
                    'Dim objCus_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.UPDATE)
                    'objCus_Shipping_Location.SaveData("", _Customer_Shipping_Id, _Customer_Shipping_Index, txtName.Text, txtAddress.Text, District, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtRemark.Text, txtCity.Text, txtProvince.Text, txtContactName1.Text, txtContactName2.Text, txtContactName3.Text, "", cboCountry.SelectedValue.ToString, Begin_flag, txtCustomer_Shipping_ID.Text) ', txtCity.Text, txtProvince.Text)

                    'Me.txtCustomer_Shipping_ID.Text = objDB.Str1
                    Dim objDBs As New DBType_SQLServer
                    'objDBs.DBExeNonQuery(String.Format("Update ms_Customer_Shipping set Invoice_Copy = {0}  where Customer_Shipping_Index = '{1}'", txtInvoice_Copy.Value, _Customer_Shipping_Index))
                    'Fix Hard code interface flag
                 
                    If SaveStatus Then
                        W_MSG_Information_ByIndex(1)
                        Exit Sub

                    Else
                        W_MSG_Information_ByIndex(2)
                        Exit Sub

                    End If

                Case Else
            End Select
            ' Update Invoice_Copy
           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub AddcbNameType()
        Dim objClassDB As New ms_Title(ms_Title.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboTitle.DataSource = objDT
            cboTitle.DisplayMember = "Title_Name"
            cboTitle.ValueMember = "Title_Index"




        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub


    Sub AddProvince()
        Dim objClassDB As New ms_Province(ms_Province.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboProvince.DataSource = objDT
            cboProvince.DisplayMember = "Province"
            cboProvince.ValueMember = "Province_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddCity(ByVal Province_Index As String)
        Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable


        Try
            objClassDB.SearchData_Click("", Province_Index)
            objDT = objClassDB.DataTable

            cboCity.DataSource = objDT
            cboCity.DisplayMember = "District"
            cboCity.ValueMember = "District_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbCustomerType()
        Dim objClassDB As New ms_CustomerType(ms_CustomerType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboCustomerType.DataSource = objDT
            cboCustomerType.DisplayMember = "Description"
            cboCustomerType.ValueMember = "CustomerType_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Sub AddcbCountry()
        Dim objClassDB As New ms_Country(ms_Title.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable


        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboCountry.DataSource = objDT
            cboCountry.DisplayMember = "Country_Name"
            cboCountry.ValueMember = "Country_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Sub AddcbTown(ByVal District_Index As String)

        Dim objDT As DataTable = New DataTable
        'Dim District_Index = ""
        If cboCity.SelectedValue IsNot Nothing Then
            District_Index = cboCity.SelectedValue.ToString()
        End If

        Dim _Excute As New DBType_SQLServer
        Try

            objDT = _Excute.DBExeQuery(String.Format("Select * from ms_Town where status_id <> -1  and District_Index = '{0}'", District_Index))

            CboTown.DataSource = objDT
            CboTown.DisplayMember = "Town_Name"
            CboTown.ValueMember = "Town_index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub

    Sub AddcbBillingType()
        Dim objClassDB As New ms_Billing_Type(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboBilling_Type.DataSource = objDT
            cboBilling_Type.DisplayMember = "Description"
            cboBilling_Type.ValueMember = "Billing_Type_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    ''' <summary>
    ''' Date 17/02/2010
    ''' By TaTa
    '''     Fix Bug การแสดงข้อมูล Fax ผิด (เอาค่าPostcode มาแสดง)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Sub showCustomer_forEdit()

        Dim objClassDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objClassDB.SelectEditData_Click(_Customer_Shipping_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then

                For Each objDr In objDT.Rows

                    txtCustomer_Shipping_ID.Text = objDr("Str1").ToString
                    cboTitle.Text = objDr("title").ToString
                    txtName.Text = objDr("Company_Name").ToString
                    cboCustomerType.SelectedValue = objDr("Customer_Type_Index").ToString

                    Me._Customer_Index = objDr("Customer_Index").ToString

                    txtTel.Text = objDr("Tel").ToString
                    txtMobile.Text = objDr("Mobile").ToString
                    txtEmail.Text = objDr("Email").ToString
                    txtFax.Text = objDr("Fax").ToString
                    txtContactName1.Text = objDr("Contact_Person").ToString
                    txtContactName2.Text = objDr("Contact_Person2").ToString
                    txtContactName3.Text = objDr("Contact_Person3").ToString

                    txtAddress.Text = objDr("Address").ToString
                    txtZipcode.Text = objDr("Postcode").ToString
                    txtBarcode.Text = objDr("Barcode").ToString

                    cboCountry.SelectedValue = objDr("str3").ToString

                    If Not objDr("Country_Code").ToString = "TH" Then
                        cboCity.Visible = False
                        cboProvince.Visible = False
                        txtCity.Visible = True
                        txtProvince.Visible = True
                        txtCity.Text = objDr("str4").ToString
                        txtProvince.Text = objDr("str5").ToString
                    Else
                        cboCity.Visible = True
                        cboProvince.Visible = True
                        cboProvince.SelectedValue = objDr("Province_Index").ToString
                        Me.AddCity(Me.cboProvince.SelectedValue) 'อำเภอ
                        cboCity.SelectedValue = objDr("District_Index").ToString
                        Me.AddcbTown(Me.cboCity.SelectedValue) 'ตำบล
                        CboTown.SelectedValue = objDr("Town_index").ToString


                        txtCity.Visible = False
                        txtProvince.Visible = False
                    End If

                    txtRemark.Text = objDr("Remark").ToString
                    txtName2.Text = objDr("str6").ToString
                    '#Code Winspeed
                    txtCode_Win1.Text = objDr("Code_WinSpeed_1").ToString
                    txtCode_Win2.Text = objDr("Code_WinSpeed_2").ToString
                    'If objDr("TransportRegion_Index").ToString <> "" Then
                    cboRegion.SelectedValue = objDr("TransportRegion_Index").ToString
                    'End If
                    'If objDr("Route_Index").ToString <> "" Then
                    cboRoute.SelectedValue = objDr("Route_Index").ToString
                    'End If
                    'If objDr("SubRoute_Index").ToString <> "" Then
                    cboSubRoute.SelectedValue = objDr("SubRoute_Index").ToString
                    txtCredit_Term.Text = IIf(IsDBNull(objDr("Credit_Term")), 0, objDr("Credit_Term"))
                    'End If
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Sub ShowGrid()
        Dim strWhere As String = ""
        Dim objClassDB As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow
        If cboSearchType.SelectedIndex = 0 Then
            strWhere = " AND Customer_Shipping_Location_Id Like '%" & txtSearchKey.Text & "%' "
        End If
        If cboSearchType.SelectedIndex = 1 Then
            strWhere = " AND Shipping_Location_Name Like '%" & txtSearchKey.Text & "%' "
        End If


        Try
            grdCustomer.Rows.Clear()

            objClassDB.SelectDataEditToCustomerShipping_Location(_Customer_Shipping_Index, strWhere)

            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    grdCustomer.Rows.Add()

                    grdCustomer.Rows(i).Cells("ColumnCustomerID").Value = objDr("Customer_Shipping_Location_Id").ToString
                    grdCustomer.Rows(i).Cells("ColumnCustomerID").Tag = objDr("Customer_Shipping_Location_Index").ToString
                    grdCustomer.Rows(i).Cells("ColumnName").Value = objDr("Shipping_Location_Name").ToString
                    grdCustomer.Rows(i).Cells("ColumnAddress").Value = objDr("Address").ToString
                    grdCustomer.Rows(i).Cells("ColumnTel").Value = objDr("Tel").ToString
                    grdCustomer.Rows(i).Cells("ColumnCountryName").Value = objDr("Country_Name").ToString

                    If objDr("Country_Code").ToString = "TH" Then
                        grdCustomer.Rows(i).Cells("ColumnThaidefinition").Value = objDr("District").ToString
                        grdCustomer.Rows(i).Cells("ColumnCity").Value = objDr("Province").ToString
                    Else

                        grdCustomer.Rows(i).Cells("ColumnThaidefinition").Value = objDr("str4").ToString
                        grdCustomer.Rows(i).Cells("ColumnCity").Value = objDr("str5").ToString

                    End If



                    grdCustomer.Rows(i).Cells("ColumnMobileTel").Value = objDr("Mobile").ToString
                    grdCustomer.Rows(i).Cells("ColumnIndex").Value = objDr("Customer_Shipping_Index").ToString
                    i = i + 1
                Next

            Else
                grdCustomer.Rows.Clear()
                grdCustomer.Refresh()
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try

        '  Cursor.Current = Cursors.Default
    End Sub
#End Region

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Begin_flag = 1
            'If txtCustomerID.Text = "" Then
            '    MessageBox.Show("กรุณากรอกรหัสคู่ค้า", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
            '    Exit Sub
            'End If
            If cboCustomerType.SelectedValue = "" Then
                W_MSG_Information_ByIndex(19)
                Exit Sub

            End If
            If cboCustomerType.SelectedValue = "" Or txtName.Text = "" Then
                W_MSG_Information_ByIndex(20)

                txtName.Focus()
                Exit Sub
            End If
            If cboProvince.SelectedValue Is Nothing Then
                W_MSG_Information_ByIndex(23)
                cboProvince.Focus()
                Exit Sub
            End If
            If cboCity.SelectedValue Is Nothing Then
                W_MSG_Information_ByIndex(22)
                cboCity.Focus()
                Exit Sub
            End If
            If CboTown.SelectedValue Is Nothing Then
                W_MSG_Information("กรุณาเลือก ตำบล")
                CboTown.Focus()
                Exit Sub
            End If

            'If cboRoute.SelectedIndex = 0 Then
            '    W_MSG_Information_ByIndex(83)
            '    Exit Sub
            'End If
            'If txtCustomer_Name.Text = "" Then
            '    W_MSG_Information("กรุณากรอกชื่อคู่ค้า")
            '    txtCustomer_Name.Focus()
            '    Exit Sub
            'End If

            Me.SaveData()

            btnAddCustomer_Shipp.Enabled = True
            BtnEditCustomer_Shipp.Enabled = True
            btnDeleteCustomer_Shipp.Enabled = True
            ShowGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
        '    Me.Close()

    End Sub

    Private Sub btnSave_Package_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCustomer_Shipp.Click


        Try
            Dim objfrm As New frmCustomer_Shipping_Location_V3

            objfrm._Customer_Shipping_Index = _Customer_Shipping_Index
            'objfrm.Customer_Index = _Customer_Index

            Dim strCusName As String = txtName.Text
            Dim strCusTile As String = cboTitle.Text

            _CustomerName = strCusTile & strCusName
            _CustomerID = txtCustomer_Shipping_ID.Text

            'objfrm.CustomerID = _CustomerID
            'objfrm.CustomerName = _CustomerName

            objfrm.SaveType = 0
            objfrm.ShowDialog()
            ShowGrid()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteCustomer_Shipp.Click
        Try
            If grdCustomer.RowCount <= 0 Then
                Exit Sub
            End If

            Dim objfrm As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.DELETE)
            Dim Index As String = grdCustomer.Rows(grdCustomer.CurrentRow.Index).Cells("ColumnCustomerID").Tag.ToString()

            If W_MSG_Confirm_ByIndex(100002) = Windows.Forms.DialogResult.Yes = True Then
                objfrm.Delete_Master(Index)
                ShowGrid()
                GC.Collect()
            End If
            'Clear All Obj is not use
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub EditCustomer()
        Try
            If grdCustomer.RowCount <= 0 Then
                Exit Sub
            End If
            Dim objfrm As New frmCustomer_Shipping_Location_V3
            objfrm.SaveType = 1
            Dim Customer_Shipping_Index As String = _Customer_Shipping_Index
            objfrm.Customer_Shipping_Index = grdCustomer.Rows(grdCustomer.CurrentRow.Index).Cells("ColumnIndex").Value.ToString()
            objfrm.Customer_Shipping_Location_Index = grdCustomer.Rows(grdCustomer.CurrentRow.Index).Cells("ColumnCustomerID").Tag.ToString()


            objfrm.ShowDialog()

            ShowGrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnEditCustomer_Shipp.Click
        Try
            EditCustomer()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Private Sub tabCustomer_Info_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tabCustomer_Info.SelectedIndexChanged
    '    Try
    '        If _Customer_Shipping_Index = "" And tabCustomer_Info.SelectedTab.Name = "tabShipping" Then
    '            tabShipping.Visible = False
    '            If W_MSG_Confirm_ByIndex(300001) = Windows.Forms.DialogResult.Yes = True Then
    '                Exit Sub
    '            End If
    '        Else
    '            tabShipping.Visible = True
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub cboProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProvince.SelectedIndexChanged
    '    Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable
    '    Dim objDr As DataRow
    '    Dim Province_Index As String
    '    Province_Index = cboProvince.SelectedValue.ToString
    '    Try
    '        objClassDB.SearchData_Click("", Province_Index)
    '        objDT = objClassDB.DataTable
    '        If objDT.Rows.Count > 0 Then
    '            Dim i As Integer = 0
    '            For Each objDr In objDT.Rows
    '                cboCity.DataSource = objDT
    '                cboCity.DisplayMember = "District"
    '                cboCity.ValueMember = "District_Index"
    '                i = i + 1
    '            Next
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDT = Nothing
    '        objClassDB = Nothing
    '    End Try
    'End Sub

    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Dim frm As New frmCustmer_Popup
        frm.ShowDialog()
        '    *** Recive value ****
        Dim tmpCustomer_Index As String = ""
        tmpCustomer_Index = frm.Customer_Index
        If Not tmpCustomer_Index = "" Then
            Me._Customer_Index = tmpCustomer_Index
            Me.getCustomer()
        Else
            Me._Customer_Index = ""
            Me.txtIDCustomer.Text = ""
            Me.txtCustomer_Name.Text = ""
        End If

        ' *********************
        frm.Close()
    End Sub

    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                Me.txtIDCustomer.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString
            Else
                Me._Customer_Index = ""
                Me.txtIDCustomer.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub grdCustomer_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdCustomer.CellDoubleClick
        If grdCustomer.RowCount > 0 Then
            EditCustomer()
        End If
    End Sub

   
    Private Sub cboRoute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectedIndexChanged
        Try
            If cboRoute.SelectedValue Is Nothing Then Exit Sub
            getComboSubRoute(cboRoute.SelectedValue.ToString)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ShowGrid()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmCustomer_Shipping_Des_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigCustomer_Shipping
                    frm.Text = "รายละเอียดลูกค้าบริษัท/ผู้รับสินค้า"
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2003)
                    oFunction.SW_Language_Column(Me, Me.grdCustomer, 2003)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtCredit_Term_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCredit_Term.KeyPress
        Select Case Asc(e.KeyChar)
            Case 48 To 57
                e.Handled = False
            Case 8, 13, 46
                e.Handled = False

            Case Else
                e.Handled = True
        End Select
    End Sub

    'Private Sub cboCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity.SelectedIndexChanged

    '    ' Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
    '    'Dim objDT As DataTable = New DataTable
    '    ''Dim objDr As DataRow
    '    'Dim _Excute As New DBType_SQLServer
    '    'Dim District_Index As String
    '    'District_Index = cboCity.SelectedValue.ToString
    '    Try

    '        Me.AddcbTown()
    '        'objDT = _Excute.DBExeQuery(String.Format("Select * from ms_Town where status_id <> -1  and District_Index = '{0}'", District_Index))

    '        'CboTown.DataSource = Nothing
    '        'CboTown.DataSource = objDT
    '        'CboTown.DisplayMember = "Country_Name"
    '        'CboTown.ValueMember = "Country_Index"


    '        'If objDT.Rows.Count > 0 Then
    '        'Dim i As Integer = 0
    '        'For Each objDr In objDT.Rows
    '        'CboTown.DataSource = objDT
    '        'CboTown.DisplayMember = "Town_Name"
    '        'CboTown.ValueMember = "Town_index"
    '        'i = i + 1
    '        'Next
    '        'End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    Finally
    '        'objDT = Nothing
    '        ' objClassDB = Nothing
    '    End Try


    'End Sub

    Private Sub cboProvince_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProvince.SelectionChangeCommitted
        Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        'Dim objDr As DataRow
        Dim Province_Index As String
        Province_Index = cboProvince.SelectedValue.ToString
        Try
            'objClassDB.SearchData_Click("", Province_Index)
            'objDT = objClassDB.DataTable
            ''If objDT.Rows.Count > 0 Then
            ''    Dim i As Integer = 0
            ''    For Each objDr In objDT.Rows
            'cboCity.DataSource = objDT
            'cboCity.DisplayMember = "District"
            'cboCity.ValueMember = "District_Index"
            ''        i = i + 1
            ''    Next
            ''End If
            Me.AddCity(Province_Index)
            Me.AddcbTown(Me.cboCity.SelectedValue)

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Private Sub cboCity_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity.SelectionChangeCommitted

        ' Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        'Dim objDT As DataTable = New DataTable
        ''Dim objDr As DataRow
        'Dim _Excute As New DBType_SQLServer
        'Dim District_Index As String
        'District_Index = cboCity.SelectedValue.ToString
        Try

            Me.AddcbTown(Me.cboCity.SelectedValue)
            'objDT = _Excute.DBExeQuery(String.Format("Select * from ms_Town where status_id <> -1  and District_Index = '{0}'", District_Index))

            'CboTown.DataSource = Nothing
            'CboTown.DataSource = objDT
            'CboTown.DisplayMember = "Country_Name"
            'CboTown.ValueMember = "Country_Index"


            'If objDT.Rows.Count > 0 Then
            'Dim i As Integer = 0
            'For Each objDr In objDT.Rows
            'CboTown.DataSource = objDT
            'CboTown.DisplayMember = "Town_Name"
            'CboTown.ValueMember = "Town_index"
            'i = i + 1
            'Next
            'End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            'objDT = Nothing
            ' objClassDB = Nothing
        End Try
    End Sub
End Class