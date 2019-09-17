Imports WMS_STD_Master
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master.CurrencyTextBox

Public Class frmHandlingChargeByDocTypeSetup

    Private _Process_Id As Integer = -11
    Private _DocumentType_Index As String = ""
    Private _Customer_Index As String = ""
    Private _HandlingChargeByDocType_Index As String = ""

    Private countTypeUnload As Integer = 1
    Private countTypeload As Integer = 1

    Public Sub New(ByVal pstrCustomer_Index As String, ByVal pintProcess_Id As Integer, ByVal pstrDocumentType_Index As String, ByVal pstrHandlingChargeByDocType_Index As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        Me._Customer_Index = pstrCustomer_Index
        Me._Process_Id = pintProcess_Id
        Me._DocumentType_Index = pstrDocumentType_Index
        Me._HandlingChargeByDocType_Index = pstrHandlingChargeByDocType_Index

    End Sub

  
    Private Sub frmHandlingChargeByDocTypeSetup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Load Master and Default
            Me.AddCurrencyTextBox()
            Me.getCurrentCy()
            Me.getCboHandlingType()
            Me.getUnit()
            Me.getCustomer()

            Me.getDocumentType(Me._Process_Id)
            Me.getHandlingChargeByDoc()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getHandlingChargeByDoc()
        Try
            Me.cboDocumentType.SelectedValue = Me._DocumentType_Index

            Dim objClassDB As New svar_HandlingChargeByDocType
            Dim objDT As DataTable = New DataTable

            Dim objDr As DataRow


            '--- Unload
            Dim R As Integer = 0
            objClassDB.GetHandlingChargeByDocType_ServiceGroup(Me._HandlingChargeByDocType_Index, 1)
            objDT = objClassDB.DataTable

            For Each objDr In objDT.Rows
                With dgvUnLoadFee
                    .Rows.Add()
                    .Rows(R).Cells("ColIndexUnload").Value = objDr("HandlingChargeByDocType_Index").ToString
                    .Rows(R).Cells("ColTypeUnload").Value = objDr("HandlingType").ToString
                    .Rows(R).Cells("ColTypeUnload").Tag = objDr("HandlingType_Index").ToString
                    .Rows(R).Cells("ColRateUnload").Value = objDr("Rate").ToString
                    .Rows(R).Cells("ColMinimumrateUnload").Value = objDr("Minimum_rate").ToString
                    .Rows(R).Cells("ColUnitUnload").Value = objDr("Unit").ToString
                    .Rows(R).Cells("ColUnitUnload").Tag = objDr("ServiceFee_Unit_Index").ToString
                    Me.cboCurrency.SelectedValue = objDr("Currency_Index").ToString
                End With
                R = R + 1
            Next
            '--- load
            R = 0
            objClassDB.GetHandlingChargeByDocType_ServiceGroup(Me._HandlingChargeByDocType_Index, 2)
            objDT = objClassDB.DataTable

            For Each objDr In objDT.Rows
                With dgvLoadFee
                    .Rows.Add()
                    .Rows(R).Cells("ColIndexload").Value = objDr("HandlingChargeByDocType_Index").ToString
                    .Rows(R).Cells("ColTypeload").Value = objDr("HandlingType").ToString
                    .Rows(R).Cells("ColTypeload").Tag = objDr("HandlingType_Index").ToString
                    .Rows(R).Cells("ColRateload").Value = objDr("Rate").ToString
                    .Rows(R).Cells("ColMinimumrateload").Value = objDr("Minimum_rate").ToString
                    .Rows(R).Cells("ColUnitload").Value = objDr("Unit").ToString
                    .Rows(R).Cells("ColUnitload").Tag = objDr("ServiceFee_Unit_Index").ToString
                    Me.cboCurrency.SelectedValue = objDr("Currency_Index").ToString
                End With
                R = R + 1
            Next

           
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "   Load Master and Default   "
    Sub AddCurrencyTextBox()
        Try
            AddHandler Me.txtRateUnLoadFee.KeyPress, AddressOf Me.keypressed
            AddHandler Me.txtMinimumrateUnload.KeyPress, AddressOf Me.keypressed
            AddHandler Me.txtRateloadFee.KeyPress, AddressOf Me.keypressed
            AddHandler Me.txtMinimumrateLoad.KeyPress, AddressOf Me.keypressed
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
    End Sub
    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            objms_Customer.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me._Customer_Index = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString

            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub
    Private Sub getUnit()
        Dim objClassDB4 As New svar_HandlingChargeByDocType
        Dim objDT4 As DataTable = New DataTable
        Dim objClassDB5 As New svar_HandlingChargeByDocType
        Dim objDT5 As DataTable = New DataTable
        Try
            objClassDB4.SelectUnitbyGroup("1")
            objDT4 = objClassDB4.DataTable
            objClassDB5.SelectUnitbyGroup("1")
            objDT5 = objClassDB5.DataTable

            cboUnitUnloadFee.BeginUpdate()
            With cboUnitUnloadFee
                .DisplayMember = "Description"
                .ValueMember = "ServiceFee_Unit_Index"
                .DataSource = objDT4
            End With
            cboUnitUnloadFee.EndUpdate()

            cboUnitloadFee.BeginUpdate()
            With cboUnitloadFee
                .DisplayMember = "Description"
                .ValueMember = "ServiceFee_Unit_Index"
                .DataSource = objDT5
            End With
            cboUnitloadFee.EndUpdate()

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB4 = Nothing
            objDT4 = Nothing
            objClassDB5 = Nothing
            objDT5 = Nothing

        End Try
    End Sub

    Private Sub getCboHandlingType()
        Dim objDBUnload As New tb_HandlingType
        Dim objDTUnload As DataTable = New DataTable

        Dim objDBload As New tb_HandlingType
        Dim objDTload As DataTable = New DataTable

        Try
            '--- Unload
            objDBUnload.GetAllAsDataTable()
            objDTUnload = objDBUnload.DataTable
            cboTypeUnLoad.BeginUpdate()
            With cboTypeUnLoad
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTUnload
            End With
            cboTypeUnLoad.EndUpdate()

            '--- load
            objDBload.GetAllAsDataTable()
            objDTload = objDBload.DataTable
            cboTypeLoad.BeginUpdate()
            With cboTypeLoad
                .DisplayMember = "Description"
                .ValueMember = "HandlingType_Index"
                .DataSource = objDTload
            End With
            cboTypeLoad.EndUpdate()


        Catch ex As Exception
            Throw ex
        Finally
            objDBUnload = Nothing
            objDTUnload = Nothing
            objDBload = Nothing
            objDTload = Nothing

        End Try

    End Sub
    Private Sub getCurrentCy()
        Dim objClassDB As New svms_Currency
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable

            cboCurrency.BeginUpdate()
            With cboCurrency
                .DisplayMember = "Description"
                .ValueMember = "Currency_Index"
                .DataSource = objDT
            End With

            cboCurrency.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


    Private Sub getDocumentType(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

#End Region

    Private Function ValidateData() As Boolean
        Try
            Dim GetMessage300159 As String = GetMessage_Data("300159") 'กรุณาตรวจสอบ

            If Me.txtCustomer_Id.Text = "" Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblCustomer.Text)
                Return False
            End If

            If Me.cboDocumentType.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblDocumentType.Text)
                Return False
            End If

            If Me.cboCurrency.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblCurrency.Text)
                Return False
            End If

            If Me.cboTypeUnLoad.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblTypUnload.Text)
                Return False
            End If

            If Me.cboTypeLoad.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblTypload.Text)
                Return False
            End If

            If Me.cboUnitUnloadFee.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : Unit " & Me.grbUnLoadFee.Text)
                Return False
            End If

            If Me.cboUnitloadFee.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : Unit " & Me.grbLoad.Text)
                Return False
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAddUnloadFee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddUnloadFee.Click
        Try
            If Me.ValidateData() = False Then Exit Sub

            If txtRateUnLoadFee.Text = "" Or txtMinimumrateUnload.Text = "" Then
                W_MSG_Information_ByIndex(300014)
                Exit Sub
            End If
            Dim R As Integer
            Dim i As Integer

            'If countTypeUnload = 0 Then
            '    'clMsg.SettingMsg(300013, 0)
            '    'If MessageBox.Show(clMsg.Data, clMsg.Header, MessageBoxButtons.OK, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK = True Then Exit Sub

            '    W_MSG_Information_ByIndex(300013)
            '    Exit Sub
            'End If

            For i = 0 To dgvUnLoadFee.RowCount - 1
                If dgvUnLoadFee.Rows(i).Cells("ColTypeUnload").Value = cboTypeUnLoad.Text.ToString Then
                    'clMsg.SettingMsg(300029, 0)
                    'If MessageBox.Show(clMsg.Data, clMsg.Header, MessageBoxButtons.OK, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK = True Then Exit Sub
                    W_MSG_Information_ByIndex(300029)
                    Exit Sub
                End If
            Next

            countTypeUnload = countTypeUnload - 1
            ' ADD Row

            dgvUnLoadFee.Rows.Add()
            R = dgvUnLoadFee.Rows.Count - 1
            dgvUnLoadFee.Rows(R).Cells("ColTypeUnload").Value = cboTypeUnLoad.Text.ToString
            dgvUnLoadFee.Rows(R).Cells("ColTypeUnload").Tag = cboTypeUnLoad.SelectedValue
            dgvUnLoadFee.Rows(R).Cells("ColRateUnload").Value = txtRateUnLoadFee.Text.ToString
            dgvUnLoadFee.Rows(R).Cells("ColMinimumrateUnload").Value = txtMinimumrateUnload.Text.ToString
            dgvUnLoadFee.Rows(R).Cells("ColUnitUnload").Value = cboUnitUnloadFee.Text.ToString
            dgvUnLoadFee.Rows(R).Cells("ColUnitUnload").Tag = cboUnitUnloadFee.SelectedValue
            'cboUnitUnloadFee.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClearUnload_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearUnload.Click
        Try
            'If W_MSG_Confirm_ByIndex(37) = Windows.Forms.DialogResult.Yes Then
            dgvUnLoadFee.Rows.Clear()
            countTypeUnload = cboTypeUnLoad.Items.Count
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnAddLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddLoad.Click
        Try
            If Me.ValidateData() = False Then Exit Sub

            If txtRateloadFee.Text = "" Or txtMinimumrateLoad.Text = "" Then
                W_MSG_Information_ByIndex(300014)
                Exit Sub
            End If
            Dim R As Integer
            Dim i As Integer
            'If countTypeload = 0 Then
            '    W_MSG_Information_ByIndex(300013)
            '    Exit Sub
            'End If
            For i = 0 To dgvLoadFee.RowCount - 1
                If dgvLoadFee.Rows(i).Cells("ColTypeload").Value = cboTypeLoad.Text.ToString Then
                    'clMsg.SettingMsg(300029, 0)
                    'If MessageBox.Show(clMsg.Data, clMsg.Header, MessageBoxButtons.OK, MessageBoxIcon.Question) = Windows.Forms.DialogResult.OK = True Then Exit Sub

                    W_MSG_Information_ByIndex(300029)
                    Exit Sub
                End If
            Next

            countTypeload = countTypeload - 1
            ' ADD Row

            dgvLoadFee.Rows.Add()
            R = dgvLoadFee.Rows.Count - 1
            dgvLoadFee.Rows(R).Cells("ColTypeload").Value = cboTypeLoad.Text.ToString
            dgvLoadFee.Rows(R).Cells("ColTypeload").Tag = cboTypeLoad.SelectedValue
            dgvLoadFee.Rows(R).Cells("ColRateload").Value = txtRateloadFee.Text.ToString
            dgvLoadFee.Rows(R).Cells("ColMinimumrateload").Value = txtMinimumrateLoad.Text.ToString
            dgvLoadFee.Rows(R).Cells("ColUnitload").Value = cboUnitloadFee.Text.ToString
            dgvLoadFee.Rows(R).Cells("ColUnitload").Tag = cboUnitloadFee.SelectedValue
            'cboUnitloadFee.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClearLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLoad.Click
        Try
            'If W_MSG_Confirm_ByIndex(37) = Windows.Forms.DialogResult.Yes Then
            dgvLoadFee.Rows.Clear()
            countTypeload = cboTypeLoad.Items.Count
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.ValidateData() = False Then Exit Sub
            '---------------------------------------------------------------------------------------------------
            Dim R As Integer

            Dim objItemHanding As New List(Of svar_HandlingChargeByDocType)
            Dim objDBTempIndex As New Sy_AutoNumber
            If Me._HandlingChargeByDocType_Index = "" Then
                Me._HandlingChargeByDocType_Index = objDBTempIndex.getSys_Value("HandlingChargeByDocType_Index")
            End If
            objDBTempIndex = Nothing

            '--- UnLoad
            With dgvUnLoadFee
                For R = 0 To .Rows.Count - 1
                    Dim objHanding As New svar_HandlingChargeByDocType

                    objHanding.HandlingChargeByDocType_Index = Me._HandlingChargeByDocType_Index
                    objHanding.ServiceGroup_Id = 1
                    objHanding.Currency_Index = cboCurrency.SelectedValue.ToString
                    objHanding.Customer_Index = Me._Customer_Index
                    objHanding.DocumentType_Index = cboDocumentType.SelectedValue.ToString
                    objHanding.ServiceFee_Unit_Index = cboUnitUnloadFee.SelectedValue.ToString
                    objHanding.HandlingType_Index = .Rows(R).Cells("ColTypeUnload").Tag
                    objHanding.Rate = .Rows(R).Cells("ColRateUnload").Value
                    objHanding.Minimum_Rate = .Rows(R).Cells("ColMinimumrateUnload").Value
                    objHanding.Description = Me.grbUnLoadFee.Text
                    objItemHanding.Add(objHanding)
                    'cboUnitUnloadFee.Enabled = False
                Next
            End With
            '--- Load
            With dgvLoadFee
                For R = 0 To .Rows.Count - 1
                    Dim objHanding As New svar_HandlingChargeByDocType

                    objHanding.HandlingChargeByDocType_Index = Me._HandlingChargeByDocType_Index
                    objHanding.ServiceGroup_Id = 2
                    objHanding.Currency_Index = cboCurrency.SelectedValue.ToString
                    objHanding.Customer_Index = Me._Customer_Index
                    objHanding.DocumentType_Index = cboDocumentType.SelectedValue.ToString
                    objHanding.ServiceFee_Unit_Index = cboUnitloadFee.SelectedValue.ToString

                    objHanding.HandlingType_Index = .Rows(R).Cells("ColTypeload").Tag
                    objHanding.Rate = .Rows(R).Cells("ColRateload").Value
                    objHanding.Minimum_Rate = .Rows(R).Cells("ColMinimumrateload").Value

                    objHanding.Description = Me.grbLoad.Text
                    objItemHanding.Add(objHanding)
                    'cboUnitloadFee.Enabled = False
                Next
            End With

            'SAVE
            Dim osvarSaveTransportCharge As New svar_HandlingChargeByDocTypeTransaction(svar_HandlingChargeByDocTypeTransaction.enuOperation_Type.ADDNEW, objItemHanding)
            osvarSaveTransportCharge.HandlingChargeByDocType_Index = Me._HandlingChargeByDocType_Index
            Dim strSave As String = osvarSaveTransportCharge.SaveData()

            If strSave = "PASS" Then
                W_MSG_Information_ByIndex(1)
                Me.Close()
            Else
                W_MSG_Information(strSave)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class