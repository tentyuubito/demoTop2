Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_master.CurrencyTextBox
Imports System.Drawing
Imports System.Windows.Forms
Public Class frmCustomer_Shipping_Location_V3

    Public SaveType As Integer = 0

    Public _Customer_Shipping_Location_Index As String

    Public _Customer_Shipping_Index As String

    Private _Sku_Index As String = ""
    Private _Sku_Index_Block As String = ""
    'Dim _Customer_Index As String
    'Dim _CustomerID As String
    'Dim _CustomerName As String

    Private _Isbangkok As Integer = 0

    Private DTProduct_Rule As New DataTable
    Private DTProduct_Block As New DataTable


    Public Property Customer_Shipping_Index() As String
        Get
            Return _Customer_Shipping_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Index = Value
        End Set
    End Property

    Public Property Customer_Shipping_Location_Index() As String
        Get
            Return _Customer_Shipping_Location_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Location_Index = Value
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

    'Public Property Customer_Index() As String
    '    Get
    '        Return _Customer_Index
    '    End Get
    '    Set(ByVal Value As String)
    '        _Customer_Index = Value
    '    End Set
    'End Property

    Private _USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION As Boolean = True
    Private _USE_CUSTOMER_CUSTOMERSHIPPING As Boolean = True

    Private strTmpName As String = ""
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
    Sub AddcbTown(ByVal District_Index As String)

        Dim objDT As DataTable = New DataTable

        Dim _Excute As New DBType_SQLServer
        Try
            objDT = _Excute.DBExeQuery(String.Format("Select * from ms_Town where status_id <> -1  AND  District_Index = '" & cboCity.SelectedValue.ToString & "'"))

            CboTown.DataSource = objDT
            CboTown.DisplayMember = "Town_Name"
            CboTown.ValueMember = "Town_index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub
    Private Sub frmGet_CustomerShipping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2004)

            strTmpName = Me.btnGroupSKU.Text

            'txtCustomerID.Tag = _Customer_Index
            'txtCustomerID.Text = _CustomerID
            'txtCustomerName.Text = _CustomerName
            Me.getComboRegion()
            GetDataWarehouse()

            Me.AddcbCountry() 'ประเทศ
            Set_cbCity() 'จังหวัด
            Me.Set_SubCity(Me.cboProvince.SelectedValue) 'อำเภอ
            Me.AddcbTown(Me.cboCity.SelectedValue) 'ตำบล

            AddcbCustomerType()
            cboCountry.Text = "THAILAND"

            USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION()
            USE_CUSTOMER_CUSTOMERSHIPPING()
            Select Case SaveType
                Case 0 'Save

                Case 1 'Update
                    txtCustomer_Shipping_Location_ID.Enabled = False
                    showCustomer_Shipping_forEdit()
            End Select


            If Not (String.IsNullOrEmpty(Me._Customer_Shipping_Index)) Then
                Me.getCustomer_Shipping()
                Me.getCustomer_Shipping_day(_Customer_Shipping_Index)
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
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
    Private Sub GetDataWarehouse()
        Try
            Dim objWH As New ms_Warehouse(ms_Warehouse.enuOperation_Type.NULL)
            Dim objDT As New DataTable
            objWH.SearchData_Click("", "")
            objDT = objWH.DataTable


            With cboWarehouse
                .DisplayMember = "Description"
                .ValueMember = "Warehouse_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub getCustomer_Shipping()

        Dim objClassDB As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.SelectEditData_Click(_Customer_Shipping_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me.txtCustomer_Shipping_ID.Text = objDT.Rows(0)("Str1").ToString
                Me.txtCustomer_Shipping_Name.Text = objDT.Rows(0)("Company_Name").ToString
            End If



        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub


    Sub getCustomer_Shipping_day(ByVal strCustomer_Shipping_Index As String)

        Dim objClassDB As New ms_Customer_Shipping_Location_Day
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""
        Dim strDay As String = ""

        Try
            strWhere = " And Customer_Shipping_Location_Index='" & strCustomer_Shipping_Index & "'"
            objClassDB.GetAllData(strWhere)
            objDT = objClassDB.DataTable
            Dim chk As New Windows.Forms.CheckBox

            If objDT.Rows.Count > 0 Then


                For i As Integer = 0 To objDT.Rows.Count - 1
                    strDay = objDT.Rows(i)("str1").ToString
                    If chk01.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk01.Checked = CBool(CInt(strDay))
                    End If
                    If chk02.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk02.Checked = CBool(CInt(strDay))
                    End If
                    If chk03.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk03.Checked = CBool(CInt(strDay))
                    End If
                    If chk04.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk04.Checked = CBool(CInt(strDay))
                    End If
                    If chk05.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk05.Checked = CBool(CInt(strDay))
                    End If
                    If chk06.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk06.Checked = CBool(CInt(strDay))
                    End If
                    If chk07.Tag = objDT.Rows(i)("ShippingDay_Index") Then '  AndAlso strDay = "1" 
                        chk07.Checked = CBool(CInt(strDay))
                    End If
                Next

            End If



        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub


    Sub USE_CUSTOMER_CUSTOMERSHIPPING()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            If objCustomSetting.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                Me._USE_CUSTOMER_CUSTOMERSHIPPING = True
                Me.grbCustomer.Visible = True

            Else
                Me._USE_CUSTOMER_CUSTOMERSHIPPING = False
                'Me.lblOwner.Visible = False
                'Me.txtIDCustomer.Visible = False
                'Me.lblCustomerName.Visible = False
                'Me.txtCustomer_Name.Visible = False
                'Me.btnSeachCustomer.Visible = False
                Me.grbCustomer.Visible = False

                'Me.grbShipping.Location = New Point(8, 3)
                'Me.grbShipping.Size = New Size(558, 473)
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
                Me.grbCustomer.Visible = True
            Else
                Me._USE_CUSTOMERSHIPPING_CUSTOMERSHIPPINGLOCATION = False
                Me.grbCustomer.Visible = False
                'Me.grbShipping.Location = New Point(8, 3)
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub showCustomer_Shipping_forEdit()
        Dim objClassDB As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDTSKU_Rule As New DataTable
        Dim objDTSKU_BLOCK As New DataTable
        Dim objDr As DataRow

        Try
            objClassDB.SelectFor_EditData_Click(_Customer_Shipping_Location_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    If i = 0 Then
                        'add for KSL
                        cboCustomerType.SelectedValue = objDr("CustomerType_Index").ToString
                        If objDr("CustomerType_Index").ToString = "" Then
                            cboCustomerType.SelectedValue = "0010000000001"
                        End If

                        Me.txtDistributionCenter.Text = objDr("DistributionCenter").ToString


                        txtCustomer_Shipping_Location_ID.Text = objDr("Customer_Shipping_Location_Id").ToString
                        txtName.Text = objDr("Shipping_Location_Name").ToString

                        txtTel.Text = objDr("Tel").ToString
                        txtMobile.Text = objDr("Mobile").ToString
                        txtEmail.Text = objDr("Email").ToString
                        txtFax.Text = objDr("Fax").ToString
                        txtAddress.Text = objDr("Address").ToString
                        txtZipcode.Text = objDr("Postcode").ToString
                        'txtAmtWH.Text = objDr("AmtWH").ToString
                        'If IsDBNull(objDr("isBangkok")) Then
                        '    chbIsBangkok.Checked = False
                        'Else
                        '    If CInt(objDr("isBangkok")) = 1 Then
                        '        chbIsBangkok.Checked = True
                        '    Else
                        '        chbIsBangkok.Checked = False
                        '    End If
                        'End If


                        txtRemark.Text = objDr("Remark").ToString

                        txtContact_Person1.Text = objDr("Contact_Person1").ToString
                        txtContact_Person2.Text = objDr("Contact_Person2").ToString
                        txtContact_Person3.Text = objDr("Contact_Person3").ToString
                        txtRoute.Text = objDr("Route").ToString
                        txtRoute.Tag = objDr("Route_Index").ToString
                        cboSubRoute.SelectedItem = objDr("subRoute_Index").ToString 'cboSubRoute
                        SetcboSubRoute(txtRoute.Tag)

                        Dim dt As New DataTable
                        dt = DirectCast(cboSubRoute.DataSource, DataTable)
                        If Not dt Is Nothing Then
                            Dim drr() As DataRow
                            drr = dt.Select(String.Format(" SubRoute_Index = '{0}'", objDr("SubRoute_Index").ToString))
                            If drr.Length > 0 Then
                                cboSubRoute.SelectedValue = objDr("SubRoute_Index").ToString
                            End If
                        End If


                        cboRegion.SelectedValue = objDr("TransportRegion_Index").ToString
                        Me.cboCountry.SelectedValue = objDr("str3").ToString
                        If Not objDr("Country_Code").ToString = "TH" Then
                            cboCity.Visible = False
                            cboProvince.Visible = False
                            txtCity.Visible = True
                            txtProvince.Visible = True
                            txtCity.Text = objDr("str4").ToString
                            txtProvince.Text = objDr("str5").ToString
                        Else
                            cboCity.Visible = True
                            cboCity.Visible = True

                            cboProvince.SelectedValue = objDr("Province_Index").ToString
                            Me.Set_SubCity(cboProvince.SelectedValue)
                            cboCity.SelectedValue = objDr("District_Index").ToString
                            Me.AddcbTown(cboCity.SelectedValue)
                            CboTown.SelectedValue = objDr("Town_Index").ToString

                            txtCity.Visible = False
                            txtProvince.Visible = False
                        End If



                        '--ADDNEW V3
                        If IsDBNull(objDr("Is_GI_PrimaryWH")) Then
                            Me.chkIs_GI_PrimaryWH.Checked = False
                        Else
                            Me.chkIs_GI_PrimaryWH.Checked = objDr("Is_GI_PrimaryWH")
                        End If
                        If IsDBNull(objDr("Warehouse_Index")) Then

                        Else
                            Me.cboWarehouse.SelectedValue = objDr("Warehouse_Index")
                        End If

                        If IsDBNull(objDr("Is_GI_PrimaryWHOnly")) Then
                            Me.chkIs_GI_PrimaryWHOnly.Checked = False
                        Else
                            Me.chkIs_GI_PrimaryWHOnly.Checked = objDr("Is_GI_PrimaryWHOnly")
                        End If

                        If IsDBNull(objDr("Is_GI_RemainingAge")) Then
                            Me.chkIs_GI_RemainingAge.Checked = False
                        Else
                            Me.chkIs_GI_RemainingAge.Checked = objDr("Is_GI_RemainingAge")
                        End If


                        If IsDBNull(objDr("RemainingAge_Value")) And IsDBNull(objDr("RemainingAge_Unit")) Then

                        Else
                            If CInt(objDr("RemainingAge_Value")) > 0 Then
                                Me.rdbRemainingAge_Value.Checked = True
                                Me.txtRemainingAge_Value.Text = objDr("RemainingAge_Value")
                            ElseIf CInt(objDr("RemainingAge_Unit")) > 0 Then
                                Me.rdbRemainingAge_Unit.Checked = True
                                Me.txtRemainingAge_Unit.Text = objDr("RemainingAge_Unit")
                            End If
                        End If

                        If IsDBNull(objDr("Is_GI_NotOlderThanLastIssue")) Then
                            Me.chkIs_GI_NotOlderThanLastIssue.Checked = False
                        Else
                            Me.chkIs_GI_NotOlderThanLastIssue.Checked = objDr("Is_GI_NotOlderThanLastIssue")
                        End If

                        If IsDBNull(objDr("LastIssue_Option")) Then

                        Else
                            Select Case objDr("LastIssue_Option").ToString.ToUpper
                                Case "E"
                                    Me.rdbLastIssue_OptionE.Checked = True
                                Case "M"
                                    Me.rdbLastIssue_OptionM.Checked = True
                                Case "L"
                                    Me.rdbLastIssue_OptionL.Checked = True
                                Case Else
                            End Select

                        End If


                        If IsDBNull(objDr("Is_GI_COA_Req")) Then
                            Me.chkIs_GI_COA_Req.Checked = False
                        Else
                            Me.chkIs_GI_COA_Req.Checked = objDr("Is_GI_COA_Req")
                        End If


                        If IsDBNull(objDr("Is_DL_Mon")) Then
                            Me.chkIs_DL_Mon.Checked = False
                        Else
                            Me.chkIs_DL_Mon.Checked = objDr("Is_DL_Mon")
                        End If

                        If IsDBNull(objDr("Is_DL_Tue")) Then
                            Me.chkIs_DL_Tue.Checked = False
                        Else
                            Me.chkIs_DL_Tue.Checked = objDr("Is_DL_Tue")
                        End If

                        If IsDBNull(objDr("Is_DL_Wed")) Then
                            Me.chkIs_DL_Wed.Checked = False
                        Else
                            Me.chkIs_DL_Wed.Checked = objDr("Is_DL_Wed")
                        End If

                        If IsDBNull(objDr("Is_DL_Thu")) Then
                            Me.chkIs_DL_Thu.Checked = False
                        Else
                            Me.chkIs_DL_Thu.Checked = objDr("Is_DL_Thu")
                        End If
                        If IsDBNull(objDr("Is_DL_Fri")) Then
                            Me.chkIs_DL_Fri.Checked = False
                        Else
                            Me.chkIs_DL_Fri.Checked = objDr("Is_DL_Fri")
                        End If

                        If IsDBNull(objDr("Is_DL_Sat")) Then
                            Me.chkIs_DL_Sat.Checked = False
                        Else
                            Me.chkIs_DL_Sat.Checked = objDr("Is_DL_Sat")
                        End If

                        If IsDBNull(objDr("Is_DL_Sun")) Then
                            Me.chkIs_DL_Sun.Checked = False
                        Else
                            Me.chkIs_DL_Sun.Checked = objDr("Is_DL_Sun")
                        End If


                        Me.txtDL_Mon_Remark.Text = objDr("DL_Mon_Remark").ToString
                        Me.txtDL_Tue_Remark.Text = objDr("DL_Tue_Remark").ToString
                        Me.txtDL_Wed_Remark.Text = objDr("DL_Wed_Remark").ToString
                        Me.txtDL_Thu_Remark.Text = objDr("DL_Thu_Remark").ToString
                        Me.txtDL_Fri_Remark.Text = objDr("DL_Fri_Remark").ToString
                        Me.txtDL_Sat_Remark.Text = objDr("DL_Sat_Remark").ToString
                        Me.txtDL_Sun_Remark.Text = objDr("DL_Sun_Remark").ToString
                        If IsDBNull(objDr("MinDeliveryPerOrder")) Then
                            Me.txtMinDeliveryPerOrder.Text = 0
                        Else
                            Me.txtMinDeliveryPerOrder.Text = objDr("MinDeliveryPerOrder")
                        End If

                        'KSL
                        If IsDBNull(objDr("FlagMix_Lot")) Then
                            Me.FlagMix_Lot.Checked = False
                        Else
                            Me.FlagMix_Lot.Checked = objDr("FlagMix_Lot")
                        End If
                        If IsNumeric(objDr("NumFlagMix_Lot")) Then
                            Me.NumFlagMix_Lot.Text = objDr("NumFlagMix_Lot")
                        Else
                            Me.NumFlagMix_Lot.Text = 0
                        End If
                        If IsDBNull(objDr("FlagDont_Reverse_LOT")) Then
                            Me.FlagDont_Reverse_LOT.Checked = False
                        Else
                            Me.FlagDont_Reverse_LOT.Checked = objDr("FlagDont_Reverse_LOT")
                        End If


                    End If
                    'i += 1


                Next


                objClassDB.GetDataCustomer_Shipping_Location_SKU_Rule(_Customer_Shipping_Location_Index)
                objDTSKU_Rule = objClassDB.DataTable
                If objDTSKU_Rule.Rows.Count > 0 Then
                    For Each objDr In objDTSKU_Rule.Rows


                        Dim iRow As Integer = 0
                        With Me.dgvConfigAgeSKU
                            iRow = .Rows.Add()
                            .Rows(iRow).Cells("col_Customer_Shipping_Location_SKU_Rule_Index").Value = objDr("Customer_Shipping_Location_SKU_Rule_Index")
                            .Rows(iRow).Cells("col_SKU_Index").Value = objDr("SKU_Index")
                            .Rows(iRow).Cells("col_Is_GI_RemainingAge").Value = objDr("Is_GI_RemainingAge")
                            .Rows(iRow).Cells("col_Sku_Id").Value = objDr("Sku_Id")
                            .Rows(iRow).Cells("col_Sku_Name").Value = objDr("Sku_Name")

                            If IsDBNull(objDr("RemainingAge_Value")) = False Then
                                If CInt(objDr("RemainingAge_Value")) > 0 Then
                                    .Rows(iRow).Cells("col_RemainingAge_Value").Value = objDr("RemainingAge_Value")
                                    .Rows(iRow).Cells("col_RemainingAge_Unit").Value = 0
                                    .Rows(iRow).Cells("col_MoreAge").Value = objDr("RemainingAge_Value")
                                    .Rows(iRow).Cells("col_Package").Value = "วัน"
                                ElseIf CInt(objDr("RemainingAge_Unit")) > 0 Then
                                    .Rows(iRow).Cells("col_RemainingAge_Value").Value = 0
                                    .Rows(iRow).Cells("col_RemainingAge_Unit").Value = objDr("RemainingAge_Unit")
                                    .Rows(iRow).Cells("col_MoreAge").Value = objDr("RemainingAge_Unit")
                                    .Rows(iRow).Cells("col_Package").Value = "%"
                                End If
                            End If



                        End With



                    Next
                End If

                objClassDB.GetDatams_Customer_Shipping_Location_SKU_Block(_Customer_Shipping_Location_Index)
                objDTSKU_BLOCK = objClassDB.DataTable

                If objDTSKU_BLOCK.Rows.Count > 0 Then
                    For Each dr As DataRow In objDTSKU_BLOCK.Rows
                        Dim iRowSKUBLOCK As Integer = 0
                        With Me.dgvSKU_Block
                            iRowSKUBLOCK = .Rows.Add()
                            .Rows(iRowSKUBLOCK).Cells("col_Customer_Shipping_Location_SKU_Block_Index").Value = dr("Customer_Shipping_Location_SKU_Block_Index").ToString
                            .Rows(iRowSKUBLOCK).Cells("col_SKU_Block_SKU_INDEX").Value = dr("Sku_Index").ToString
                            .Rows(iRowSKUBLOCK).Cells("col_Block_Type").Value = dr("Block_Type")
                            Select Case dr("Block_Type").ToString
                                Case "0"
                                    .Rows(iRowSKUBLOCK).Cells("col_LotBatch").Value = dr("Plot").ToString
                                Case "1"
                                    .Rows(iRowSKUBLOCK).Cells("col_ExpDate").Value = CDate(dr("Exp_Date")).ToString("dd/MM/yyyy")
                                Case "2"
                                    .Rows(iRowSKUBLOCK).Cells("col_MfgDate").Value = CDate(dr("Mfg_Date")).ToString("dd/MM/yyyy")
                                Case Else
                            End Select

                            .Rows(iRowSKUBLOCK).Cells("col_SKU_Block_Sku_Id").Value = dr("Sku_Id").ToString
                            .Rows(iRowSKUBLOCK).Cells("col_SKU_Block_Sku_Name").Value = dr("Sku_Name").ToString

                        End With

                    Next

                End If


                '  getCustomer_Shipping_day(_Customer_Shipping_Location_Index)


            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try

    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If txtName.Text = "" Then
                W_MSG_Information("กรุณาระบุ ชื่อผู้รับ")
                btnSeach_Route.Focus()
                Exit Sub
            End If

            'If chbIsBangkok.Checked Then
            '    _Isbangkok = 1
            'Else
            '    _Isbangkok = 0
            'End If

            If Me.cboCustomerType.SelectedValue Is Nothing Then
                W_MSG_Information("กรุณาเลือก " & lblCustomerType.Text)
                cboCustomerType.Focus()
                Exit Sub
            End If
            If txtRoute.Text = "" Then
                W_MSG_Information("กรุณาเลือก เส้นทางหลัก")
                txtRoute.Focus()
                Exit Sub
            End If
            If txtCity.Text = "" Or txtProvince.Text = "" Then
                If cboCity.SelectedValue = "" Then
                    W_MSG_Information_ByIndex(22)
                    cboCity.Focus()

                    Exit Sub
                End If

                If cboProvince.SelectedValue = "" Then
                    W_MSG_Information_ByIndex(23)

                    cboProvince.Focus()

                    Exit Sub
                End If
            End If

            Dim Province_Index As String = ""
            Dim District_Index As String = ""
            Dim Town_index As String = ""

            If cboProvince.SelectedValue IsNot Nothing Then
                Province_Index = cboProvince.SelectedValue.ToString
            Else
                W_MSG_Information("กรุณาเลือก จังหวัด")
                cboProvince.Focus()
                Exit Sub
            End If
            If cboCity.SelectedValue IsNot Nothing Then
                District_Index = cboCity.SelectedValue.ToString
            Else
                W_MSG_Information("กรุณาเลือก อำเภอ")
                cboCity.Focus()
                Exit Sub
            End If

            If CboTown.SelectedValue IsNot Nothing Then
                Town_index = CboTown.SelectedValue.ToString
            Else
                W_MSG_Information("กรุณาเลือก ตำบล")
                CboTown.Focus()
                Exit Sub
            End If

            If cboCountry.SelectedValue Is Nothing Then
                cboCountry.SelectedValue = "1000000203"
            End If
            Dim Route_Index As String = txtRoute.Tag

            'If txtRoute.Tag IsNot Nothing Then  'ton  คอมเม้น
            '    Route_Index = txtRoute.Tag.ToString
            'End If

            'Dim strDataDayIndex As String = ""
            'Dim strDayIndex As New List(Of String)
            'strDayIndex.Add(chk01.Tag)
            'strDayIndex.Add(chk02.Tag)
            'strDayIndex.Add(chk03.Tag)
            'strDayIndex.Add(chk04.Tag)
            'strDayIndex.Add(chk05.Tag)
            'strDayIndex.Add(chk06.Tag)
            'strDayIndex.Add(chk07.Tag)
            'Dim str As New List(Of String)
            'If chk01.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk01.Tag & ","
            'Else
            '    str.Add("0")
            'End If
            'If chk02.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk02.Tag & ","
            'Else
            '    str.Add("0")
            'End If
            'If chk03.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk03.Tag & ","
            'Else
            '    str.Add("0")
            'End If
            'If chk04.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk04.Tag & ","
            'Else
            '    str.Add("0")
            'End If
            'If chk05.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk05.Tag & ","
            'Else
            '    str.Add("0")
            'End If
            'If chk06.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk06.Tag & ","
            'Else
            '    str.Add("0")
            'End If
            'If chk07.Checked = True Then
            '    str.Add("1")
            '    strDataDayIndex &= chk07.Tag & ","
            'Else
            '    str.Add("0")
            'End If

            'If strDataDayIndex.Length > 0 Then
            '    strDataDayIndex = strDataDayIndex.Substring(0, strDataDayIndex.Length - 1)
            'End If
            Dim objCustomer_Shipping_Location_SKU_Rule As New ms_Customer_Shipping_Location_SKU_Rule
            Dim objCustomer_Shipping_Location_SKU_RuleL As New List(Of ms_Customer_Shipping_Location_SKU_Rule)

            Dim objCustomer_Shipping_Location_SKU_Block As New ms_Customer_Shipping_Location_SKU_Block
            Dim objCustomer_Shipping_Location_SKU_BlockL As New List(Of ms_Customer_Shipping_Location_SKU_Block)


            Dim SaveStatus As Boolean = True
            Select Case SaveType
                Case 0 'Add
                    Dim objDB As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.ADDNEW) 'ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.ADDNEW)
                
                    Dim ChekId As Boolean = objDB.isExistID(txtCustomer_Shipping_Location_ID.Text.Trim) ', _Customer_Index)
                    If ChekId Then
                        W_MSG_Information_ByIndex(45)
                        Exit Sub
                    End If

                    ' SaveStatus = objDB.SaveData("", txtCustomer_Shipping_Location_ID.Text, _Customer_Shipping_Index, txtName.Text, txtAddress.Text, District_Index, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtRemark.Text, txtCity.Text, txtProvince.Text, txtContact_Person1.Text, txtContact_Person2.Text, txtContact_Person3.Text, Route_Index, cboCountry.SelectedValue.ToString) ', txtCity.Text, txtProvince.Text)
                    With objDB
                        .Customer_Shipping_Location_Index = ""
                        .Customer_Shipping_Location_Id = txtCustomer_Shipping_Location_ID.Text
                        .Customer_Shipping_Index = _Customer_Shipping_Index
                        .Shipping_Location_Name = txtName.Text
                        .Address = txtAddress.Text
                        .District_Index = District_Index
                        .Province_Index = Province_Index
                        .Town_index = Town_index
                        .Postcode = txtZipcode.Text
                        .Tel = txtTel.Text
                        .Fax = txtFax.Text
                        .Mobile = txtMobile.Text
                        .Email = txtEmail.Text
                        .Remark = txtRemark.Text
                        .Str3 = cboCountry.SelectedValue.ToString
                        .Str4 = txtCity.Text
                        .Str5 = txtProvince.Text
                        .Contact_Person1 = txtContact_Person1.Text
                        .Contact_Person2 = txtContact_Person2.Text
                        .Contact_Person3 = txtContact_Person3.Text
                        .Route_Index = Route_Index
                        .Subroute_Index = IIf(cboSubRoute.SelectedValue Is Nothing, "", cboSubRoute.SelectedValue)
                        'Me.cboSubRoute.SelectedValue.ToString
                        .TransportRegion_Index = cboRegion.SelectedValue
                        .Is_GI_PrimaryWH = chkIs_GI_PrimaryWH.Checked
                        .Warehouse_Index = Me.cboWarehouse.SelectedValue
                        .Is_GI_PrimaryWHOnly = Me.chkIs_GI_PrimaryWHOnly.Checked
                        .Is_GI_RemainingAge = Me.chkIs_GI_RemainingAge.Checked

                        If rdbRemainingAge_Value.Checked Then
                            .RemainingAge_Value = Me.txtRemainingAge_Value.Text
                            .RemainingAge_Unit = 0
                        ElseIf rdbRemainingAge_Unit.Checked Then
                            .RemainingAge_Unit = Me.txtRemainingAge_Unit.Text
                            .RemainingAge_Value = 0
                        End If

                        .Is_GI_NotOlderThanLastIssue = Me.chkIs_GI_NotOlderThanLastIssue.Checked
                        If rdbLastIssue_OptionE.Checked Then .LastIssue_Option = "E"
                        If rdbLastIssue_OptionL.Checked Then .LastIssue_Option = "L"
                        If rdbLastIssue_OptionM.Checked Then .LastIssue_Option = "M"
                        .Is_GI_COA_Req = Me.chkIs_GI_COA_Req.Checked
                        .Is_DL_Mon = Me.chkIs_DL_Mon.Checked
                        .Is_DL_Tue = Me.chkIs_DL_Tue.Checked
                        .Is_DL_Wed = Me.chkIs_DL_Wed.Checked
                        .Is_DL_Thu = Me.chkIs_DL_Thu.Checked
                        .Is_DL_Fri = Me.chkIs_DL_Fri.Checked
                        .Is_DL_Sat = Me.chkIs_DL_Sat.Checked
                        .Is_DL_Sun = Me.chkIs_DL_Sun.Checked
                        .DL_Mon_Remark = Me.txtDL_Mon_Remark.Text.Trim
                        .DL_Tue_Remark = Me.txtDL_Tue_Remark.Text.Trim
                        .DL_Wed_Remark = Me.txtDL_Wed_Remark.Text.Trim
                        .DL_Thu_Remark = Me.txtDL_Thu_Remark.Text.Trim
                        .DL_Fri_Remark = Me.txtDL_Fri_Remark.Text.Trim
                        .DL_Sat_Remark = Me.txtDL_Sat_Remark.Text.Trim
                        .DL_Sun_Remark = Me.txtDL_Sun_Remark.Text.Trim
                        .MinDeliveryPerOrder = Me.txtMinDeliveryPerOrder.Text.Trim
                    End With

                    If dgvConfigAgeSKU.RowCount > 0 Then
                        For i As Integer = 0 To Me.dgvConfigAgeSKU.Rows.Count - 1
                            objCustomer_Shipping_Location_SKU_Rule = New ms_Customer_Shipping_Location_SKU_Rule

                            objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index = ""
                            objCustomer_Shipping_Location_SKU_Rule.SKU_Index = dgvConfigAgeSKU.Rows(i).Cells("col_SKU_Index").Value.ToString
                            objCustomer_Shipping_Location_SKU_Rule.Is_GI_RemainingAge = dgvConfigAgeSKU.Rows(i).Cells("col_Is_GI_RemainingAge").Value
                            objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Value = dgvConfigAgeSKU.Rows(i).Cells("col_RemainingAge_Value").Value
                            objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Unit = dgvConfigAgeSKU.Rows(i).Cells("col_RemainingAge_Unit").Value
                            objCustomer_Shipping_Location_SKU_Rule.status_id = 1

                            objCustomer_Shipping_Location_SKU_Rule.Str1 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str2 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str3 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str4 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str5 = ""

                            objCustomer_Shipping_Location_SKU_RuleL.Add(objCustomer_Shipping_Location_SKU_Rule)
                        Next
                    End If


                    If dgvSKU_Block.Rows.Count > 0 Then
                        For i As Integer = 0 To Me.dgvSKU_Block.Rows.Count - 1
                            objCustomer_Shipping_Location_SKU_Block = New ms_Customer_Shipping_Location_SKU_Block
                            objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_SKU_Block_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.Block_Type = dgvSKU_Block.Rows(i).Cells("col_Block_Type").Value.ToString
                            objCustomer_Shipping_Location_SKU_Block.SKU_Index = dgvSKU_Block.Rows(i).Cells("col_SKU_Block_SKU_INDEX").Value.ToString
                            objCustomer_Shipping_Location_SKU_Block.PLot = dgvSKU_Block.Rows(i).Cells("col_LotBatch").Value.ToString

                            If dgvSKU_Block.Rows(i).Cells("col_MfgDate").Value <> "" Then
                                objCustomer_Shipping_Location_SKU_Block.Mfg_Date = dgvSKU_Block.Rows(i).Cells("col_MfgDate").Value.ToString
                            Else
                                objCustomer_Shipping_Location_SKU_Block.Mfg_Date = Now.ToString("dd/MM/yyyy")
                            End If

                            If dgvSKU_Block.Rows(i).Cells("col_ExpDate").Value <> "" Then
                                objCustomer_Shipping_Location_SKU_Block.Exp_Date = dgvSKU_Block.Rows(i).Cells("col_ExpDate").Value.ToString
                            Else
                                objCustomer_Shipping_Location_SKU_Block.Exp_Date = Now.ToString("dd/MM/yyyy")
                            End If

                            objCustomer_Shipping_Location_SKU_Block.Order_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.OrderItem_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.Str1 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str2 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str3 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str4 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str5 = ""
                            objCustomer_Shipping_Location_SKU_Block.status_id = 1
                            objCustomer_Shipping_Location_SKU_BlockL.Add(objCustomer_Shipping_Location_SKU_Block)
                        Next
                    End If




                    objDB.Customer_Shipping_Location_SKU_BlockL = objCustomer_Shipping_Location_SKU_BlockL
                    objDB.Customer_Shipping_Location_SKU_RuleL = objCustomer_Shipping_Location_SKU_RuleL


                    SaveStatus = objDB.SaveDataV3()







                    'Save Day Customer Shipping Add by: Top
                    'Dim objCls As New ms_Customer_Shipping_Location_Day
                    'For i As Integer = 0 To strDayIndex.Count - 1
                    '    Dim objDBIndex As New Sy_AutoNumber
                    '    objCls.Customer_Shipping_Location_day_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_day_Index")
                    '    objDBIndex = Nothing

                    '    objCls.ShippingDay_Index = strDayIndex(i)
                    '    objCls.Str1 = str(i)
                    '    objCls.Customer_Shipping_Location_Index = objDB.Customer_Shipping_Location_Index
                    '    objCls.InsertData()
                    'Next
                    'If SaveStatus Then
                    '    SaveStatus = objCls.updateCustomer_Shipping_Location_DayIndex(strDataDayIndex, objDB.Customer_Shipping_Location_Index)
                    'End If


                    If SaveStatus Then
                        SaveType = 1
                        W_MSG_Information_ByIndex(1)
                        Me.Close()
                    Else
                        W_MSG_Information_ByIndex(2)
                        Exit Sub
                    End If

                Case 1 'Update

                    ' Me.Customer_Shipping_Location_Index = _Customer_Shipping_Location_Index


                    Dim objDB As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.UPDATE)

                    'SaveStatus = objDB.SaveData(_Customer_Shipping_Location_Index, txtCustomer_Shipping_Location_ID.Text, _Customer_Shipping_Index, txtName.Text, txtAddress.Text, District_Index, Province_Index, txtZipcode.Text, txtTel.Text, txtFax.Text, txtMobile.Text, txtEmail.Text, txtRemark.Text, txtCity.Text, txtProvince.Text, txtContact_Person1.Text, txtContact_Person2.Text, txtContact_Person3.Text, Route_Index, cboCountry.SelectedValue.ToString) ', txtCity.Text, txtProvince.Text)


                    With objDB
                        .Customer_Shipping_Location_Index = _Customer_Shipping_Location_Index
                        .Customer_Shipping_Location_Id = txtCustomer_Shipping_Location_ID.Text
                        .Customer_Shipping_Index = _Customer_Shipping_Index
                        .Shipping_Location_Name = txtName.Text
                        .Address = txtAddress.Text
                        .District_Index = District_Index
                        .Province_Index = Province_Index
                        .Postcode = txtZipcode.Text
                        .Tel = txtTel.Text
                        .Fax = txtFax.Text
                        .Mobile = txtMobile.Text
                        .Email = txtEmail.Text
                        .Remark = txtRemark.Text
                        .Str3 = cboCountry.SelectedValue.ToString
                        .Str4 = txtCity.Text
                        .Str5 = txtProvince.Text
                        .Contact_Person1 = txtContact_Person1.Text
                        .Contact_Person2 = txtContact_Person2.Text
                        .Contact_Person3 = txtContact_Person3.Text

                        .Route_Index = Route_Index
                        .Subroute_Index = IIf(cboSubRoute.SelectedValue Is Nothing, "", cboSubRoute.SelectedValue)
                        .TransportRegion_Index = cboRegion.SelectedValue.ToString
                        .Is_GI_PrimaryWH = chkIs_GI_PrimaryWH.Checked
                        .Warehouse_Index = Me.cboWarehouse.SelectedValue
                        .Is_GI_PrimaryWHOnly = Me.chkIs_GI_PrimaryWHOnly.Checked
                        .Is_GI_RemainingAge = Me.chkIs_GI_RemainingAge.Checked

                        If rdbRemainingAge_Value.Checked Then
                            .RemainingAge_Value = Me.txtRemainingAge_Value.Text
                            .RemainingAge_Unit = 0
                        ElseIf rdbRemainingAge_Unit.Checked Then
                            .RemainingAge_Unit = Me.txtRemainingAge_Unit.Text
                            .RemainingAge_Value = 0
                        End If

                        .Is_GI_NotOlderThanLastIssue = Me.chkIs_GI_NotOlderThanLastIssue.Checked
                        If rdbLastIssue_OptionE.Checked Then .LastIssue_Option = "E"
                        If rdbLastIssue_OptionL.Checked Then .LastIssue_Option = "L"
                        If rdbLastIssue_OptionM.Checked Then .LastIssue_Option = "M"
                        .Is_GI_COA_Req = Me.chkIs_GI_COA_Req.Checked
                        .Is_DL_Mon = Me.chkIs_DL_Mon.Checked
                        .Is_DL_Tue = Me.chkIs_DL_Tue.Checked
                        .Is_DL_Wed = Me.chkIs_DL_Wed.Checked
                        .Is_DL_Thu = Me.chkIs_DL_Thu.Checked
                        .Is_DL_Fri = Me.chkIs_DL_Fri.Checked
                        .Is_DL_Sat = Me.chkIs_DL_Sat.Checked
                        .Is_DL_Sun = Me.chkIs_DL_Sun.Checked
                        .DL_Mon_Remark = Me.txtDL_Mon_Remark.Text.Trim
                        .DL_Tue_Remark = Me.txtDL_Tue_Remark.Text.Trim
                        .DL_Wed_Remark = Me.txtDL_Wed_Remark.Text.Trim
                        .DL_Thu_Remark = Me.txtDL_Thu_Remark.Text.Trim
                        .DL_Fri_Remark = Me.txtDL_Fri_Remark.Text.Trim
                        .DL_Sat_Remark = Me.txtDL_Sat_Remark.Text.Trim
                        .DL_Sun_Remark = Me.txtDL_Sun_Remark.Text.Trim
                        .MinDeliveryPerOrder = Me.txtMinDeliveryPerOrder.Text.Trim
                    End With

                    If dgvConfigAgeSKU.RowCount > 0 Then
                        For i As Integer = 0 To Me.dgvConfigAgeSKU.Rows.Count - 1
                            objCustomer_Shipping_Location_SKU_Rule = New ms_Customer_Shipping_Location_SKU_Rule

                            objCustomer_Shipping_Location_SKU_Rule.Customer_Shipping_Location_SKU_Rule_Index = dgvConfigAgeSKU.Rows(i).Cells("col_Customer_Shipping_Location_SKU_Rule_Index").Value.ToString
                            objCustomer_Shipping_Location_SKU_Rule.SKU_Index = dgvConfigAgeSKU.Rows(i).Cells("col_SKU_Index").Value.ToString
                            objCustomer_Shipping_Location_SKU_Rule.Is_GI_RemainingAge = dgvConfigAgeSKU.Rows(i).Cells("col_Is_GI_RemainingAge").Value
                            objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Value = dgvConfigAgeSKU.Rows(i).Cells("col_RemainingAge_Value").Value
                            objCustomer_Shipping_Location_SKU_Rule.RemainingAge_Unit = dgvConfigAgeSKU.Rows(i).Cells("col_RemainingAge_Unit").Value
                            objCustomer_Shipping_Location_SKU_Rule.status_id = 1

                            objCustomer_Shipping_Location_SKU_Rule.Str1 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str2 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str3 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str4 = ""
                            objCustomer_Shipping_Location_SKU_Rule.Str5 = ""

                            objCustomer_Shipping_Location_SKU_RuleL.Add(objCustomer_Shipping_Location_SKU_Rule)
                        Next
                    End If



                    If dgvSKU_Block.Rows.Count > 0 Then
                        For i As Integer = 0 To Me.dgvSKU_Block.Rows.Count - 1
                            objCustomer_Shipping_Location_SKU_Block = New ms_Customer_Shipping_Location_SKU_Block
                            If dgvSKU_Block.Rows(i).Cells("col_Customer_Shipping_Location_SKU_Block_Index").Value <> "" Then
                                objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_SKU_Block_Index = dgvSKU_Block.Rows(i).Cells("col_Customer_Shipping_Location_SKU_Block_Index").Value.ToString
                            Else
                                objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_SKU_Block_Index = ""
                            End If
                         
                            objCustomer_Shipping_Location_SKU_Block.Customer_Shipping_Location_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.Block_Type = dgvSKU_Block.Rows(i).Cells("col_Block_Type").Value.ToString
                            objCustomer_Shipping_Location_SKU_Block.SKU_Index = dgvSKU_Block.Rows(i).Cells("col_SKU_Block_SKU_INDEX").Value.ToString
                            If dgvSKU_Block.Rows(i).Cells("col_LotBatch").Value <> "" Then
                                objCustomer_Shipping_Location_SKU_Block.PLot = dgvSKU_Block.Rows(i).Cells("col_LotBatch").Value.ToString
                            Else
                                objCustomer_Shipping_Location_SKU_Block.PLot = ""
                            End If


                            If dgvSKU_Block.Rows(i).Cells("col_MfgDate").Value <> "" Then
                                objCustomer_Shipping_Location_SKU_Block.Mfg_Date = dgvSKU_Block.Rows(i).Cells("col_MfgDate").Value.ToString
                            Else
                                objCustomer_Shipping_Location_SKU_Block.Mfg_Date = Now.ToString("dd/MM/yyyy")
                            End If

                            If dgvSKU_Block.Rows(i).Cells("col_ExpDate").Value <> "" Then
                                objCustomer_Shipping_Location_SKU_Block.Exp_Date = dgvSKU_Block.Rows(i).Cells("col_ExpDate").Value.ToString
                            Else
                                objCustomer_Shipping_Location_SKU_Block.Exp_Date = Now.ToString("dd/MM/yyyy")
                            End If

                            objCustomer_Shipping_Location_SKU_Block.Order_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.OrderItem_Index = ""
                            objCustomer_Shipping_Location_SKU_Block.Str1 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str2 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str3 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str4 = ""
                            objCustomer_Shipping_Location_SKU_Block.Str5 = ""
                            objCustomer_Shipping_Location_SKU_Block.status_id = 1
                            objCustomer_Shipping_Location_SKU_BlockL.Add(objCustomer_Shipping_Location_SKU_Block)
                        Next
                    End If




                    objDB.Customer_Shipping_Location_SKU_BlockL = objCustomer_Shipping_Location_SKU_BlockL

                    objDB.Customer_Shipping_Location_SKU_RuleL = objCustomer_Shipping_Location_SKU_RuleL


                    SaveStatus = objDB.SaveDataV3()

                    If Not IsNumeric(Me.NumFlagMix_Lot.Text) Then
                        Me.NumFlagMix_Lot.Text = "0"
                    End If
                    'dong :: Add for KSL.
                    'CustomerType_Index,FlagMix_Lot,NumFlagMix_Lot,FlagDont_Reverse_LOT
                    Dim objCon As New WMS_STD_Formula.DBType_SQLServer
                    Dim xSQL As String = ""
                    xSQL &= " UPDATE ms_Customer_Shipping_Location "
                    xSQL &= " SET CustomerType_Index = @CustomerType_Index "
                    xSQL &= "       ,FlagMix_Lot = @FlagMix_Lot"
                    xSQL &= "       ,NumFlagMix_Lot = @NumFlagMix_Lot"
                    xSQL &= "       ,FlagDont_Reverse_LOT = @FlagDont_Reverse_LOT "
                    xSQL &= "       ,INT_U = Null , Town_index = @Town_index "
                    xSQL &= "  WHERE Customer_Shipping_Location_Index = @Customer_Shipping_Location_Index"
                    With objCon.SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@CustomerType_Index", SqlDbType.VarChar).Value = Me.cboCustomerType.SelectedValue
                        .Parameters.Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar).Value = Me.Customer_Shipping_Location_Index
                        .Parameters.Add("@FlagMix_Lot", SqlDbType.Bit).Value = Me.FlagMix_Lot.Checked
                        .Parameters.Add("@NumFlagMix_Lot", SqlDbType.Int).Value = Me.NumFlagMix_Lot.Text
                        .Parameters.Add("@FlagDont_Reverse_LOT", SqlDbType.Bit).Value = Me.FlagDont_Reverse_LOT.Checked
                        .Parameters.Add("@Town_index", SqlDbType.VarChar).Value = Me.CboTown.SelectedValue
                    End With
                    objCon.DBExeNonQuery(xSQL)

  

                    'Dim objCls As New ms_Customer_Shipping_Location_Day
                    'If objCls.Delete_Master(_Customer_Shipping_Location_Index) Then
                    '    For i As Integer = 0 To strDayIndex.Count - 1

                    '        Dim objDBIndex As New Sy_AutoNumber
                    '        objCls.Customer_Shipping_Location_day_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_day_Index")
                    '        objDBIndex = Nothing

                    '        objCls.ShippingDay_Index = strDayIndex(i)
                    '        objCls.Str1 = str(i)
                    '        objCls.Customer_Shipping_Location_Index = objDB.Customer_Shipping_Location_Index
                    '        objCls.InsertData()
                    '    Next
                    'Else
                    '    For i As Integer = 0 To strDayIndex.Count - 1

                    '        Dim objDBIndex As New Sy_AutoNumber
                    '        objCls.Customer_Shipping_Location_day_Index = objDBIndex.getSys_Value("Customer_Shipping_Location_day_Index")
                    '        objDBIndex = Nothing

                    '        objCls.ShippingDay_Index = strDayIndex(i)
                    '        objCls.Str1 = str(i)
                    '        objCls.Customer_Shipping_Location_Index = objDB.Customer_Shipping_Location_Index
                    '        objCls.InsertData()
                    '    Next
                    'End If
                    'If SaveStatus Then
                    '    SaveStatus = objCls.updateCustomer_Shipping_Location_DayIndex(strDataDayIndex, objDB.Customer_Shipping_Location_Index)
                    'End If


                    If SaveStatus Then
                        W_MSG_Information_ByIndex(1)
                        Me.Close()
                    Else
                        W_MSG_Information_ByIndex(2)
                        Exit Sub
                    End If
            End Select

            'Fix Hard code interface flag ย้ายไปด้านบน
            'Dim xobjDB As New DBType_SQLServer
            'xobjDB.DBExeNonQuery(String.Format("update ms_Customer_Shipping_Location  set INT_U = Null where Customer_Shipping_Location_Index = '{0}'", Customer_Shipping_Location_Index))



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region "Function"
    Sub AddcbCountry()
        Dim objClassDB As New ms_Country(ms_Title.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    cboCountry.DataSource = objDT
                    cboCountry.DisplayMember = "Country_Name"
                    cboCountry.ValueMember = "Country_Index"
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub


    Sub Set_cbCity()
        Dim objClassDB As New ms_Province(ms_Title.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    cboProvince.DataSource = objDT
                    cboProvince.DisplayMember = "Province"
                    cboProvince.ValueMember = "Province_Index"
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub Set_SubCity(ByVal Province_Index As String)
        Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objClassDB.SearchData_Click("", Province_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    cboCity.DataSource = objDT
                    cboCity.DisplayMember = "District"
                    cboCity.ValueMember = "District_Index"
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    '**************************************************

#End Region

    Private Sub cboCountry_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCountry.SelectedIndexChanged
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

    Private Sub cbProvince_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow
        Dim Province_Index As String
        Province_Index = cboProvince.SelectedValue.ToString
        Try
            objClassDB.SearchData_Click("", Province_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    cboCity.DataSource = objDT
                    cboCity.DisplayMember = "District"
                    cboCity.ValueMember = "District_Index"
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Private Sub btnSeach_Route_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeach_Route.Click
        Try
            Dim tmpRoute_Des As String = txtRoute.Text
            Dim tmmRoute_Index As String = txtRoute.Tag
            Dim frm As New frmRoute_Popup
            frm.ShowDialog()
            If frm.Route_Index <> "" Then
                txtRoute.Text = frm.Description
                txtRoute.Tag = frm.Route_Index
            Else
                txtRoute.Text = tmpRoute_Des
                txtRoute.Tag = tmmRoute_Index
            End If
            SetcboSubRoute(txtRoute.Tag)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub SetcboSubRoute(ByVal pRoute_Index As String)
        Try
          

            Dim objsubRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
            Dim objDT As New DataTable
            objsubRoute.GetSubRoute_BystrAnd(String.Format(" AND Route_Index = '{0}'", pRoute_Index))
            objDT = objsubRoute.DataTable
            With Me.cboSubRoute
                .ValueMember = "SubRoute_Index"
                .DisplayMember = "Description"
                .DataSource = objDT

            End With




        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub txtAmtWH_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(sender, e.KeyChar)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub btnSku_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Popup.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            'frm.Customer_Index = txtCustomerID.Tag
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") Or (Not frm.Sku_Index Is Nothing) Then

                _Sku_Index = frm.Sku_Index
                txtSKU_ID.Text = frm.Sku_ID
                txtSku_Name.Text = frm.Sku_Des_eng

            Else
                _Sku_Index = ""
                txtSKU_ID.Text = ""
                txtSku_Name.Text = ""
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try



    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try




            If DTProduct_Rule.Rows.Count > 0 Then
                AddDataSKUDLGroup()
            Else
                If _Sku_Index = "" Then Exit Sub
                AddDataSKUDL()
            End If








        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ClearData()

        _Sku_Index = ""
        txtSKU_ID.Text = ""
        txtSku_Name.Text = ""

    End Sub

    Private Sub AddDataSKUBLOCKGroup()
        Try
            If rdbBlockType0.Checked = False And rdbBlockType1.Checked = False And rdbBlockType2.Checked = False Then Exit Sub

            If dgvSKU_Block.Rows.Count > 0 Then
                Dim drr() As DataRow
                For i As Integer = 0 To Me.dgvSKU_Block.RowCount - 1
                    drr = DTProduct_Block.Select(String.Format("Sku_Index = '{0}' ", dgvSKU_Block.Rows(i).Cells("col_SKU_Block_SKU_INDEX").Value))
                    If drr.Length > 0 Then
                        For Each dr As DataRow In drr
                            dr.Delete()
                        Next
                    End If
                Next
            End If

            DTProduct_Block.AcceptChanges()

            For Each dr As DataRow In DTProduct_Block.Rows
                Dim iRow As Integer = 0
                With Me.dgvSKU_Block
                    iRow = .Rows.Add()
                    .Rows(iRow).Cells("col_SKU_Block_SKU_INDEX").Value = dr("Sku_Index").ToString
                    If rdbBlockType0.Checked Then
                        .Rows(iRow).Cells("col_Block_Type").Value = 0
                        .Rows(iRow).Cells("col_LotBatch").Value = Me.txtPlot.Text
                    ElseIf rdbBlockType1.Checked Then
                        .Rows(iRow).Cells("col_Block_Type").Value = 1
                        .Rows(iRow).Cells("col_ExpDate").Value = dtpExpDate.Value.ToString("dd/MM/yyyy")
                    ElseIf rdbBlockType2.Checked Then
                        .Rows(iRow).Cells("col_Block_Type").Value = 2
                        .Rows(iRow).Cells("col_MfgDate").Value = dtpMfgDate.Value.ToString("dd/MM/yyyy")
                    End If


                    .Rows(iRow).Cells("col_SKU_Block_Sku_Id").Value = dr("Product_Id").ToString
                    .Rows(iRow).Cells("col_SKU_Block_Sku_Name").Value = dr("Product_Name_th").ToString




                End With
            Next
            DTProduct_Block.Clear()
            Me.btnGroupSKUBlock.Text = strTmpName & " (0)"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub AddDataSKUBLOCK()
        Try

            If rdbBlockType0.Checked = False And rdbBlockType1.Checked = False And rdbBlockType2.Checked = False Then Exit Sub
            If dgvSKU_Block.Rows.Count > 0 Then




                Dim BlockType As Integer = 0


                With Me.dgvSKU_Block

                    For i As Integer = 0 To .Rows.Count - 1
                        If rdbBlockType0.Checked Then
                            If .Rows(i).Cells("col_SKU_Block_SKU_INDEX").Value = _Sku_Index_Block And .Rows(i).Cells("col_LotBatch").Value = Me.txtPlot.Text.Trim Then
                                W_MSG_Information("ข้อมูลซ้ำ!!!")
                                Exit Sub
                            End If
                        ElseIf rdbBlockType1.Checked Then
                            If .Rows(i).Cells("col_SKU_Block_SKU_INDEX").Value = _Sku_Index_Block And .Rows(i).Cells("col_Expdate").Value = Me.dtpExpDate.Value.ToString("dd/MM/yyyy") Then
                                W_MSG_Information("ข้อมูลซ้ำ!!!")
                                Exit Sub
                            End If

                        ElseIf rdbBlockType2.Checked Then

                            If .Rows(i).Cells("col_SKU_Block_SKU_INDEX").Value = _Sku_Index_Block And .Rows(i).Cells("col_MfgDate").Value = Me.dtpMfgDate.Value.ToString("dd/MM/yyyy") Then
                                W_MSG_Information("ข้อมูลซ้ำ!!!")
                                Exit Sub
                            End If
                        End If






                    Next

                End With

            End If



            Dim iRow As Integer = 0
            With Me.dgvSKU_Block
                iRow = .Rows.Add()
                .Rows(iRow).Cells("col_SKU_Block_SKU_INDEX").Value = _Sku_Index_Block
                If rdbBlockType0.Checked Then
                    .Rows(iRow).Cells("col_Block_Type").Value = 0
                    .Rows(iRow).Cells("col_LotBatch").Value = Me.txtPlot.Text
                ElseIf rdbBlockType1.Checked Then
                    .Rows(iRow).Cells("col_Block_Type").Value = 1
                    .Rows(iRow).Cells("col_ExpDate").Value = dtpExpDate.Value.ToString("dd/MM/yyyy")
                ElseIf rdbBlockType2.Checked Then
                    .Rows(iRow).Cells("col_Block_Type").Value = 2
                    .Rows(iRow).Cells("col_MfgDate").Value = dtpMfgDate.Value.ToString("dd/MM/yyyy")
                End If


                .Rows(iRow).Cells("col_SKU_Block_Sku_Id").Value = Me.txtSku_Id_Block.Text
                .Rows(iRow).Cells("col_SKU_Block_Sku_Name").Value = Me.txtSku_Name_Block.Text




            End With



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub AddDataSKUDLGroup()
        Try
            If DTProduct_Rule.Rows.Count = 0 Then Exit Sub

            If Me.dgvConfigAgeSKU.RowCount > 0 Then

                Dim drr() As DataRow
                For i As Integer = 0 To Me.dgvConfigAgeSKU.Rows.Count - 1
                    drr = DTProduct_Rule.Select(String.Format("Sku_Index = '{0}' ", dgvConfigAgeSKU.Rows(i).Cells("col_SKU_Index").Value))
                    If drr.Length > 0 Then
                        For Each dr As DataRow In drr
                            dr.Delete()
                        Next
                    End If
                Next
            End If
            DTProduct_Rule.AcceptChanges()



            For Each dr As DataRow In DTProduct_Rule.Rows
                Dim iRow As Integer = 0
                With Me.dgvConfigAgeSKU
                    iRow = .Rows.Add()
                    .Rows(iRow).Cells("col_Customer_Shipping_Location_SKU_Rule_Index").Value = ""
                    .Rows(iRow).Cells("col_SKU_Index").Value = dr("Sku_Index").ToString
                    .Rows(iRow).Cells("col_Is_GI_RemainingAge").Value = chkSKUIs_GI_RemainingAge.Checked
                    .Rows(iRow).Cells("col_Sku_Id").Value = dr("Product_Id").ToString
                    .Rows(iRow).Cells("col_Sku_Name").Value = dr("Product_Name_th").ToString
                    If rdbSKURemainingAge_Value.Checked Then
                        .Rows(iRow).Cells("col_RemainingAge_Value").Value = Me.txtSKURemainingAge_Value.Text
                        .Rows(iRow).Cells("col_RemainingAge_Unit").Value = 0
                        .Rows(iRow).Cells("col_MoreAge").Value = Me.txtSKURemainingAge_Value.Text
                        .Rows(iRow).Cells("col_Package").Value = "วัน"
                    ElseIf rdbSKURemainingAge_Unit.Checked Then
                        .Rows(iRow).Cells("col_RemainingAge_Value").Value = 0
                        .Rows(iRow).Cells("col_RemainingAge_Unit").Value = Me.txtSKURemainingAge_Unit.Text
                        .Rows(iRow).Cells("col_MoreAge").Value = Me.txtSKURemainingAge_Unit.Text
                        .Rows(iRow).Cells("col_Package").Value = "%"
                    End If
                End With
            Next

            DTProduct_Rule.Clear()
            Me.btnGroupSKU.Text = strTmpName & " (0)"

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub AddDataSKUDL()
        Try
            If CInt(txtSKURemainingAge_Value.Text) = 0 And CInt(txtSKURemainingAge_Unit.Text) = 0 Then Exit Sub
            With Me.dgvConfigAgeSKU

                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If rdbSKURemainingAge_Value.Checked Then
                            If .Rows(i).Cells("col_SKU_Index").Value = _Sku_Index And .Rows(i).Cells("col_RemainingAge_Value").Value > 0 Then
                                W_MSG_Information("ข้อมูลซ้ำ!!!")
                                Exit Sub
                            End If
                        ElseIf rdbSKURemainingAge_Unit.Checked Then
                            If .Rows(i).Cells("col_SKU_Index").Value = _Sku_Index And .Rows(i).Cells("col_RemainingAge_Unit").Value > 0 Then
                                W_MSG_Information("ข้อมูลซ้ำ!!!")
                                Exit Sub
                            End If

                        End If



                    Next
                End If
            End With


            Dim iRow As Integer = 0
            With Me.dgvConfigAgeSKU
                iRow = .Rows.Add()
                .Rows(iRow).Cells("col_Customer_Shipping_Location_SKU_Rule_Index").Value = ""
                .Rows(iRow).Cells("col_SKU_Index").Value = _Sku_Index
                .Rows(iRow).Cells("col_Is_GI_RemainingAge").Value = chkSKUIs_GI_RemainingAge.Checked
                .Rows(iRow).Cells("col_Sku_Id").Value = txtSKU_ID.Text
                .Rows(iRow).Cells("col_Sku_Name").Value = txtSku_Name.Text

                If rdbSKURemainingAge_Value.Checked Then
                    .Rows(iRow).Cells("col_RemainingAge_Value").Value = Me.txtSKURemainingAge_Value.Text
                    .Rows(iRow).Cells("col_RemainingAge_Unit").Value = 0
                    .Rows(iRow).Cells("col_MoreAge").Value = Me.txtSKURemainingAge_Value.Text
                    .Rows(iRow).Cells("col_Package").Value = "วัน"
                ElseIf rdbSKURemainingAge_Unit.Checked Then
                    .Rows(iRow).Cells("col_RemainingAge_Value").Value = 0
                    .Rows(iRow).Cells("col_RemainingAge_Unit").Value = Me.txtSKURemainingAge_Unit.Text
                    .Rows(iRow).Cells("col_MoreAge").Value = Me.txtSKURemainingAge_Unit.Text
                    .Rows(iRow).Cells("col_Package").Value = "%"
                End If


            End With


        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try

            If Me.dgvConfigAgeSKU.Rows.Count = 0 Then Exit Sub







        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel.Click
        Try

            If Me.dgvConfigAgeSKU.Rows.Count = 0 Then Exit Sub
            Me.dgvConfigAgeSKU.Rows.Remove(dgvConfigAgeSKU.Rows(Me.dgvConfigAgeSKU.CurrentRow.Index))




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSKU_Block_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSKU_Block.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            'frm.Customer_Index = txtCustomerID.Tag
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") Or (Not frm.Sku_Index Is Nothing) Then

                _Sku_Index_Block = frm.Sku_Index
                txtSku_Id_Block.Text = frm.Sku_ID
                txtSku_Name_Block.Text = frm.Sku_Des_eng

            Else
                _Sku_Index_Block = ""
                txtSku_Id_Block.Text = ""
                txtSku_Name_Block.Text = ""
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnAdd_Block_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd_Block.Click
        Try

            If DTProduct_Block.Rows.Count > 0 Then
                AddDataSKUBLOCKGroup()
            Else
                AddDataSKUBLOCK()
            End If




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnDel_Block_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDel_Block.Click

        If Me.dgvSKU_Block.Rows.Count = 0 Then Exit Sub
        Me.dgvSKU_Block.Rows.Remove(dgvSKU_Block.Rows(Me.dgvSKU_Block.CurrentRow.Index))

    End Sub

    Private Sub btnGroupSKU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupSKU.Click
        Try

            Dim frm As New frmGroupProductTypePopup
            frm.ShowDialog()
            If frm.dtResult.Rows.Count > 0 Then
                Me.btnGroupSKU.Text = strTmpName & " (" & frm.dtResult.Rows.Count & ")"
                DTProduct_Rule = frm.dtResult
            Else
                Me.btnGroupSKU.Text = strTmpName & " (0)"
            End If



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnGroupSKUBlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGroupSKUBlock.Click
        Try

            Dim frm As New frmGroupProductTypePopup
            frm.ShowDialog()
            If frm.dtResult.Rows.Count > 0 Then
                Me.btnGroupSKUBlock.Text = strTmpName & " (" & frm.dtResult.Rows.Count & ")"
                DTProduct_Block = frm.dtResult
            Else
                Me.btnGroupSKUBlock.Text = strTmpName & " (0)"
            End If



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmCustomer_Shipping_Location_V3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigCustomer_Shipping_Location_V3
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2004)

                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grbShipping_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grbShipping.Enter

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If Not String.IsNullOrEmpty(Me.txtRoute.Tag) Then
                If Me.cboSubRoute.SelectedValue IsNot Nothing Then
                    Dim frm As New frmSubRoute
                    frm.SaveType = 1
                    frm.Route_Index = Me.txtRoute.Tag.ToString
                    frm.SubRoute_Index = Me.cboSubRoute.SelectedValue
                    frm.ShowDialog()
                End If
            End If
         
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub cboCity_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity.SelectedIndexChanged

    '    ' Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable
    '    Dim objDr As DataRow
    '    Dim _Excute As New DBType_SQLServer
    '    Dim District_Index As String
    '    District_Index = cboCity.SelectedValue.ToString
    '    Try

    '        objDT = _Excute.DBExeQuery(String.Format("Select * from ms_Town where status_id <> -1  and District_Index = '{0}'", District_Index))
    '        If objDT.Rows.Count > 0 Then
    '            Dim i As Integer = 0
    '            For Each objDr In objDT.Rows
    '                CboTown.DataSource = objDT
    '                CboTown.DisplayMember = "Town_Name"
    '                CboTown.ValueMember = "Town_index"
    '                i = i + 1
    '            Next
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        ' objClassDB = Nothing
    '    End Try
    'End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            Dim frm As New frmCustomer_Shipping_Des
            frm.SaveType = 1
            frm.Customer_Shipping_Index = Me._Customer_Shipping_Index
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub cboProvince_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProvince.SelectionChangeCommitted
        Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        'Dim objDr As DataRow
        Dim Province_Index As String
        Province_Index = cboProvince.SelectedValue.ToString
        Try
            Me.Set_SubCity(Province_Index)
            Me.AddcbTown(Me.cboCity.SelectedValue)

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    'Private Sub cboCity_SelectedValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity.SelectedValueChanged

    'End Sub

    Private Sub cboCity_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCity.SelectionChangeCommitted

        Try
            Me.AddcbTown(Me.cboCity.SelectedValue)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
        End Try
    End Sub



End Class