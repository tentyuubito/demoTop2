Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master.CurrencyTextBox
Imports WMS_STD_Formula
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master

Public Class frmTransportManifestChargePackSizeSetup

    Private CheckEndRate As Boolean = False
    Private Max As Double = 0
    Private _Customer_Index As String = ""
    'Private _TransportRegion_Index As String = ""
    Private _TransportJobType_Index As String = ""
    Private _Carrier_Index As String = ""

    Private _Customer_Shipping_Location_Index As String = ""
    Public Property Customer_Shipping_Location_Index() As String
        Get
            Return _Customer_Shipping_Location_Index
        End Get
        Set(ByVal value As String)
            _Customer_Shipping_Location_Index = value
        End Set
    End Property
    Private _TransportManifestChargePackSize_Index As String = ""
    Public Property TransportManifestChargePackSize_Index() As String
        Get
            Return _TransportManifestChargePackSize_Index
        End Get
        Set(ByVal value As String)
            _TransportManifestChargePackSize_Index = value
        End Set
    End Property


    Public Sub New(ByVal pstrCustomer_Index As String, ByVal pstrCarrier_Index As String, ByVal pstrCustomer_Shipping_Location_Index As String, ByVal pstrTransportJobType_Index As String)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        Me._Customer_Index = pstrCustomer_Index
        Me._Carrier_Index = pstrCarrier_Index
        Me._TransportJobType_Index = pstrTransportJobType_Index
        Me._Customer_Shipping_Location_Index = pstrCustomer_Shipping_Location_Index

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmServiceRateTransport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdTransportPaymentPerDrop.AutoGenerateColumns = False
            'Me.AddCurrencyTextBox()
            Me.getTransportJobType()
            'Me.getCurrency()
            'Me.getTransportRegion()
            'manule code
            'Me.getOverflowPerDropUnit()
            Me.getCustomer()
            Me.getCarrier()
            Me.getCus_Shipping_Location_Index()
            Me.grdShowService()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getCarrier()
        Dim objms_Carrier As New ms_Carrier(ms_Carrier.enuOperation_Type.SEARCH)
        Dim objDTms_Carrier As DataTable = New DataTable

        Try
            objms_Carrier.getPopup_Carrier("Carrier_Index", Me._Carrier_Index)
            objDTms_Carrier = objms_Carrier.DataTable
            If objDTms_Carrier.Rows.Count > 0 Then
                Me._Carrier_Index = objDTms_Carrier.Rows(0).Item("Carrier_Index").ToString
                Me.txtCarrier_Id.Text = objDTms_Carrier.Rows(0).Item("Carrier_Id").ToString
                Me.txtCarrier_Name.Text = objDTms_Carrier.Rows(0).Item("Description").ToString
            Else
                Me._Carrier_Index = ""
                Me.txtCarrier_Id.Text = ""
                Me.txtCarrier_Name.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Carrier = Nothing
            objDTms_Carrier = Nothing
        End Try
    End Sub

    Private Sub grdShowService()
        Try

            Me.cboTransportJobType.SelectedValue = Me._TransportJobType_Index
            'Me.cboTransportRegion.SelectedValue = Me._TransportRegion_Index

            Dim oTransportDetail As New svar_TransportManifestChargePackSize
            Dim dtTransportDetail As New DataTable
            oTransportDetail.GetPackSize(Me._TransportManifestChargePackSize_Index)
            dtTransportDetail = oTransportDetail.GetDataTable
            Me.grdTransportPaymentPerDrop.DataSource = dtTransportDetail
            'If Me._TransportManifestChargePackSize_Index = "" Then
            '    Exit Sub
            'Else
            'Item
            Dim dtTransportDetail2 As New DataTable
            Dim strSql As String = ""
            strSql &= " Customer_Index = '" & Me._Customer_Index & "' "
            strSql &= " AND Carrier_Index = '" & Me._Carrier_Index & "' "
            strSql &= " AND TransportJobType_Index ='" & Me._TransportJobType_Index & "' "
            strSql &= " AND Customer_Shipping_Location_Index ='" & Me._Customer_Shipping_Location_Index & "'"

            oTransportDetail.GetTransportManifestChargePackSize(" AND " & strSql)
            dtTransportDetail2 = oTransportDetail.GetDataTable
            For iRow As Integer = 0 To Me.grdTransportPaymentPerDrop.RowCount - 1
                Dim drSelect() As DataRow
                drSelect = dtTransportDetail2.Select(strSql & " AND PackSize_Index = '" & Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("PackSize_Index").Value & "'")
                If drSelect.Length > 0 Then
                    Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Rate").Value = drSelect(0)("Rate")
                    If drSelect(0)("Rate") <= 0 Then
                        Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Description").Style.BackColor = Color.White
                    Else
                        Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Description").Style.BackColor = Color.LightGreen
                    End If
                Else
                    Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Rate").Value = 0
                    Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Description").Style.BackColor = Color.White
                End If
            Next

            'End If

            'Loop Change Color

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Sub AddCurrencyTextBox()
    '    Try
    '        AddHandler Me.txtRateTransportPerTrip.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtPercent_TransportPaymentTopUp.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtFromDrop.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtToDrop.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtRateTransportPerDrop.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtOverflowPerDrop.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtOverflowRate.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtMinimum_Drop.KeyPress, AddressOf Me.keypressed
    '        AddHandler Me.txtMinimum_Rate.KeyPress, AddressOf Me.keypressed
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
    End Sub

    'Private Sub getOverflowPerDropUnit()
    '    'Dim objClass As New svms_Currency(svms_Currency.enuOperation_Type.SEARCH)
    '    Dim objDataTable As DataTable = New DataTable

    '    Try
    '        'objsvms_Currency.SearchData_Click("", "")
    '        objDataTable.Columns.Add("Description", GetType(String))
    '        objDataTable.Columns.Add("OverflowPerDropUnit_Index", GetType(Integer))

    '        Dim drRowNew As DataRow
    '        drRowNew = objDataTable.NewRow
    '        drRowNew("Description") = "CBM"
    '        drRowNew("OverflowPerDropUnit_Index") = 0
    '        objDataTable.Rows.Add(drRowNew)
    '        drRowNew = objDataTable.NewRow
    '        drRowNew("Description") = "กิโลกรัม (KG)"
    '        drRowNew("OverflowPerDropUnit_Index") = 1
    '        objDataTable.Rows.Add(drRowNew)

    '        'With Me.cboOverflowPerDropUnit
    '        '    .DisplayMember = "Description"
    '        '    .ValueMember = "OverflowPerDropUnit_Index"
    '        '    .DataSource = objDataTable
    '        'End With


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDataTable = Nothing

    '    End Try

    'End Sub

    'Sub SETDEFAULT_CUSTOMER_INDEX()
    '    Dim objCustomSetting As New config_CustomSetting
    '    Dim objDT As DataTable = New DataTable
    '    Try
    '        objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
    '        objDT = objCustomSetting.DataTable
    '        If objDT.Rows.Count > 0 Then
    '            Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
    '            Me.getCustomer()
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objDT = Nothing
    '        objCustomSetting = Nothing
    '    End Try
    'End Sub
    Private Sub getTransportJobType()
        Dim objClassDB As New ms_TransportJobType(ms_TransportJobType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllAsDataTable()
            objDT = objClassDB.DataTable
            cboTransportJobType.BeginUpdate()
            With cboTransportJobType
                .DisplayMember = "Description"
                .ValueMember = "TransportJobType_Index"
                .DataSource = objDT
            End With
            cboTransportJobType.EndUpdate()
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    'Private Sub getCurrency()
    '    Dim objsvms_Currency As New svms_Currency(svms_Currency.enuOperation_Type.SEARCH)
    '    Dim objDTsvms_Currency As DataTable = New DataTable

    '    Try
    '        objsvms_Currency.SearchData_Click("", "")
    '        objDTsvms_Currency = objsvms_Currency.DataTable
    '        With Me.cboCurrency
    '            .DisplayMember = "Description"
    '            .ValueMember = "Currency_Index"
    '            .DataSource = objDTsvms_Currency
    '        End With


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objsvms_Currency = Nothing
    '        objDTsvms_Currency = Nothing
    '    End Try

    'End Sub
    'Private Sub getTransportRegion()
    '    Dim objTransportRegion As New ms_TransportRegion(ms_TransportRegion.enuOperation_Type.SEARCH)
    '    Dim objDTTransportRegion As DataTable = New DataTable

    '    Try
    '        objTransportRegion.GetAllAsDataTable("")
    '        objDTTransportRegion = objTransportRegion.DataTable
    '        With Me.cboTransportRegion
    '            .DisplayMember = "Description"
    '            .ValueMember = "TransportRegion_Index"
    '            .DataSource = objDTTransportRegion
    '        End With


    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objTransportRegion = Nothing
    '        objDTTransportRegion = Nothing
    '    End Try

    'End Sub


    'Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
    '    Try
    '        Dim frm As New frmCustmer_Popup
    '        frm.ShowDialog()
    '        Me._Customer_Index = frm.Customer_Index
    '        If Me._Customer_Index = "" Then Exit Sub

    '        Me.tbcTransportPayment.SelectTab(Me.tbpTransportPaymentView)
    '        Me.getCustomer()
    '        Me.getShowTransportPayment()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

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


    'Private Sub btnAddRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRate.Click
    '    Try
    '        If Me.ValidateData() = False Then Exit Sub

    '        'ตรวจสอบรายการสุดท้าย
    '        If CheckEndRate = True Then
    '            W_MSG_Information_ByIndex(300013)
    '            Exit Sub
    '        End If
    '        'ตรวจสอบรายการสุดท้าย
    '        Dim GetMessage300159 As String = GetMessage_Data("300159")

    '        'If ChkDropLast.Checked = True Then
    '        '    If txtToDrop.Text = "" Or txtFromDrop.Text = "" Then
    '        '        'W_MSG_Information_ByIndex(300011)
    '        '        W_MSG_Information(GetMessage300159 & " : " & Me.lblDropCount.Text)
    '        '        Exit Sub
    '        '    End If
    '        'End If
    '        ''ตรวจสอบรายการสุดท้าย
    '        'If ChkDropLast.Checked = False Then
    '        '    If Convert.ToDouble(txtToDrop.Text.ToString) < Convert.ToDouble(txtFromDrop.Text.ToString) Or txtToDrop.Text = "" Or txtToDrop.Text = Max Or txtToDrop.Text = txtFromDrop.Text Then
    '        '        W_MSG_Information(GetMessage300159 & " : " & Me.lblDropCount.Text)
    '        '        Exit Sub
    '        '    End If
    '        'End If
    '        'ตรวจสอบราคาต่อหน่วย
    '        'If Not IsNumeric(Me.txtRateTransportPerDrop.Text) Then Me.txtRateTransportPerDrop.Text = 0
    '        'If CDbl(Me.txtRateTransportPerDrop.Text) <= 0 Then
    '        '    W_MSG_Information(GetMessage300159 & " : " & Me.lblRateTransportPerDrop.Text)
    '        '    Exit Sub
    '        'End If

    '        'If Not IsNumeric(Me.txtOverflowRate.Text) Then Me.txtOverflowRate.Text = 0
    '        'If Me.chkOverflow.Checked Then
    '        '    If CDbl(Me.txtOverflowRate.Text) <= 0 Then
    '        '        W_MSG_Information(GetMessage300159 & " : " & Me.lblOverflowRate.Text & " Overlow")
    '        '        Exit Sub
    '        '    End If
    '        'Else
    '        '    Me.txtOverflowRate.Text = "0"
    '        '    Me.txtOverflowPerDrop.Text = "0"
    '        'End If


    '        Me.grdTransportPaymentPerDrop.Rows.Add()
    '        Dim R As Integer = grdTransportPaymentPerDrop.Rows.Count - 1
    '        With Me.grdTransportPaymentPerDrop.Rows(R)
    '            'Begin end
    '            '.Cells("col_QtyDropStart").Value = Me.txtFromDrop.Text.ToString
    '            '.Cells("col_QtyDropEnd").Value = Me.txtToDrop.Text.ToString
    '            'If Me.ChkDropLast.Checked = True Then
    '            '    CheckEndRate = True
    '            '    .Cells("col_QtyDropEnd").Value = "=>"
    '            'Else
    '            '    CheckEndRate = False
    '            'End If
    '            'Max = Convert.ToDouble(txtToDrop.Text.ToString)
    '            Dim min As String = CalculateBegin(Max)
    '            'Me.txtFromDrop.Text = min
    '            'If ChkDropLast.Checked = True Then
    '            '    txtFromDrop.Text = Max
    '            'End If
    '            'rate
    '            '.Cells("col_RateTransportPerDrop").Value = FormatNumber(CDbl(Me.txtRateTransportPerDrop.Text), 2)
    '            ''overflow
    '            '.Cells("col_isOverflow").Value = Me.chkOverflow.Checked
    '            '.Cells("col_OverflowPerDrop").Value = FormatNumber(CDbl(Me.txtOverflowPerDrop.Text), 2)
    '            '.Cells("Col_OverflowPerDropUnit_desc").Value = Me.cboOverflowPerDropUnit.Text.ToString
    '            '.Cells("Col_OverflowPerDropUnit").Value = Me.cboOverflowPerDropUnit.SelectedValue.ToString
    '            '.Cells("col_OverflowRate").Value = FormatNumber(CDbl(Me.txtOverflowRate.Text), 2)

    '            'Me.cboOverflowPerDropUnit.Enabled = False

    '        End With


    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Function CalculateBegin(ByVal MAX As Double) As Double
        Try
            Dim min As Double
            min = MAX + 1
            Return min
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Private Sub ChkDropLast_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkDropLast.CheckedChanged
    '    Try
    '        If ChkDropLast.Checked = True Then
    '            txtToDrop.Visible = False
    '            If txtFromDrop.Text = "0" Then
    '                txtFromDrop.ReadOnly = True
    '                If cboOverflowPerDropUnit.SelectedValue = "1" Then
    '                    txtFromDrop.Text = "1"
    '                End If
    '            Else
    '                txtFromDrop.ReadOnly = True
    '            End If
    '            If grdTransportPaymentPerDrop.Rows.Count >= 1 And CheckEndRate = False Then
    '                txtFromDrop.Text = CDbl(grdTransportPaymentPerDrop.Rows(grdTransportPaymentPerDrop.Rows.Count - 1).Cells("col_QtyDropEnd").Value.ToString) + 1
    '            End If
    '            If grdTransportPaymentPerDrop.Rows.Count >= 1 And CheckEndRate = True Then
    '                txtFromDrop.Text = "1"
    '            End If
    '        Else
    '            If grdTransportPaymentPerDrop.Rows.Count >= 1 And CheckEndRate = False Then
    '                Max = Convert.ToDouble(grdTransportPaymentPerDrop.Rows(grdTransportPaymentPerDrop.Rows.Count - 1).Cells("col_QtyDropEnd").Value.ToString)
    '                Dim min As String = CalculateBegin(Max)
    '                'txtFromDrop.Text = Max
    '                txtFromDrop.Text = Max
    '                txtToDrop.Text = min

    '            End If
    '            'If grdTransportPaymentPerDrop.Rows.Count >= 1 And CheckEndRate = True Then
    '            '    txtFromDrop.Text = "0"
    '            'Else
    '            '    txtFromDrop.Text = "0"
    '            'End If

    '            txtFromDrop.ReadOnly = True
    '            txtToDrop.Visible = True
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub btnClearRate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRate.Click
    '    Try

    '        Me.grdTransportPaymentPerDrop.Rows.Clear()
    '        'Me.txtRateTransportPerDrop.Text = "0"
    '        'Me.cboOverflowPerDropUnit.Enabled = True
    '        Me.CheckEndRate = False
    '        'Me.txtFromDrop.Visible = True
    '        'Me.txtToDrop.Visible = True
    '        'If IsNumeric(Me.txtMinimum_Drop.Text) Then
    '        '    Me.txtFromDrop.Text = Me.txtMinimum_Drop.Text + 1
    '        'Else
    '        '    Me.txtFromDrop.Text = "1"
    '        'End If
    '        'Me.txtToDrop.Text = "0"
    '        'Me.Max = 0
    '        'Me.txtRateTransportPerTrip.Text = "0"
    '        'Me.txtPercent_TransportPaymentTopUp.Text = "0"
    '        'Me.txtOverflowRate.Text = "0"
    '        'Me.txtOverflowPerDrop.Text = "0"
    '        'Me.chkOverflow.Checked = False

    '        'ChkDropLast.Checked = False

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    'Private Sub rdbTransportPaymentDrop_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTransportPaymentDrop.CheckedChanged
    '    Try
    '        Me.grbTransportPaymentPerDrop.Enabled = rdbTransportPaymentDrop.Checked
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub


    Private Function ValidateData() As Boolean
        Try
            Dim GetMessage300159 As String = GetMessage_Data("300159")
            If Me.txtCustomer_Id.Text = "" Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblCustomer.Text)
                Return False
            End If
            If Me.txtShipping_Location_ID.Text = "" Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblReservedLocation.Text)
                Return False
            End If
            If Me.cboTransportJobType.SelectedValue Is Nothing Then
                W_MSG_Information(GetMessage300159 & " : " & Me.lblTransportJobType.Text)
                Return False
            End If


            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ValidateData() = False Then Exit Sub
            Dim strSave As String = ""
            'Header
            For iRow As Integer = 0 To Me.grdTransportPaymentPerDrop.RowCount - 1
                Dim oTransportPayment As New svar_TransportManifestChargePackSize
                oTransportPayment.Customer_Index = Me._Customer_Index
                oTransportPayment.Carrier_Index = Me._Carrier_Index
                oTransportPayment.Customer_Shipping_Location_Index = Me._Customer_Shipping_Location_Index
                oTransportPayment.TransportJobType_Index = Me.cboTransportJobType.SelectedValue
                oTransportPayment.PackSize_Index = Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("PackSize_Index").Value
                If IsNumeric(Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Rate").Value) Then
                    oTransportPayment.Rate = Me.grdTransportPaymentPerDrop.Rows(iRow).Cells("col_Rate").Value
                Else
                    W_MSG_Information("กรุณาระบุค่าบริการเป็ยตัวเลข")
                    Exit Sub
                End If

                strSave = oTransportPayment.Insert()
            Next

            If strSave = "PASS" Then
                W_MSG_Information_ByIndex(1)
                Me.grdShowService()
            Else
                W_MSG_Information(strSave)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    'Private Sub txtMinimum_Drop_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtMinimum_Drop.TextChanged
    '    Try
    '        If IsNumeric(Me.txtMinimum_Drop.Text) Then
    '            Me.txtFromDrop.Text = CDbl(Me.txtMinimum_Drop.Text) + 1
    '        End If

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub btnCustomer_Shipping_Location_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Shipping_Location.Click
        'TODO: HARDCODE-MSG
        Try
            Dim strWhere As String = ""
            Dim oConfig As New config_CustomSetting
            Dim frm As New frmCus_Ship_Location_Popup
            frm.strAddStrWhere = strWhere
            frm.ShowDialog()
            Dim tmpCustomer_Shipping_Location_Index As String = ""
            tmpCustomer_Shipping_Location_Index = frm.Customer_Shipping_Location_Index
            If tmpCustomer_Shipping_Location_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Shipping_Location_Index = "" Then
                Me._Customer_Shipping_Location_Index = tmpCustomer_Shipping_Location_Index
                Me.getCus_Shipping_Location_Index()
            End If


            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getCus_Shipping_Location_Index()
        Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objDTms_Shipping_Location As DataTable = New DataTable
        Try
            objms_Shipping_Location.getCus_Ship_Locartion_Search("Customer_Shipping_Location_Index", Me._Customer_Shipping_Location_Index)
            objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
            If objDTms_Shipping_Location.Rows.Count > 0 Then
                Me.txtShipping_Location_ID.Text = objDTms_Shipping_Location.Rows(0).Item("Company_Name").ToString
                Me.txtShipping_Location_Name.Text = objDTms_Shipping_Location.Rows(0).Item("Shipping_Location_Name").ToString
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Shipping_Location = Nothing
            objDTms_Shipping_Location = Nothing
        End Try
    End Sub
End Class