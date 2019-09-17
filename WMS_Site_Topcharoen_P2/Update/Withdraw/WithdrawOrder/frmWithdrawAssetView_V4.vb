Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master
Imports WMS_STD_OUTB_WithDraw_Datalayer
'Imports WMS_INH_Receive

Public Class frmWithdrawAssetView_V4

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        NULL
    End Enum
    Dim objCustomSetting As New config_CustomSetting
    Private _USE_ASSIGNEJOB_ITEM As Boolean = False

    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 1
#Region "Property"
    Public Customer_Index As String = ""
    Public CustomerID As String = ""
    Public CustomerName As String = ""
    Private objStatus As enuOperation_Type

    Public _JobWithdraw_Index As String = ""
    Public _Withdraw_No As String = ""
    Public _withdraw_index As String = ""
    Public _withdrawType As Integer = 0
    Public _ConfigPrint As String = ""
    Private DEFAULT_USE_REPORTPRINTOUT_BOND As String = ""
    Private _USE_WITHDRAW_ASSIGNJOB_MULTI_USER As String = ""

    Public Property withdrawType() As Integer
        Get
            Return _withdrawType
        End Get
        Set(ByVal value As Integer)
            _withdrawType = value
        End Set
    End Property

    Public Property JobWithdraw_Index() As String
        Get
            Return _JobWithdraw_Index
        End Get
        Set(ByVal value As String)
            _JobWithdraw_Index = value
        End Set
    End Property

    Public Property Withdraw_No() As String
        Get
            Return _Withdraw_No
        End Get
        Set(ByVal value As String)
            _Withdraw_No = value
        End Set
    End Property

    Public Property Withdraw_Index() As String
        Get
            Return _withdraw_index
        End Get
        Set(ByVal value As String)
            _withdraw_index = value
        End Set
    End Property

    Public Property USE_WITHDRAW_ASSIGNJOB_MULTI_USER() As String
        Get
            Return _USE_WITHDRAW_ASSIGNJOB_MULTI_USER
        End Get
        Set(ByVal value As String)
            _USE_WITHDRAW_ASSIGNJOB_MULTI_USER = value
        End Set
    End Property

#End Region

#Region "GET DATA TO DATAGRIDVIEW"

    Private Sub getWithdrawView()
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""
        Try
            ' *** WHERE STRING *** 
            If Me.rdbWithdraw_Date.Checked = True Then
                Dim StartDate As String = Format(dtpDate.Value, "MM/dd/yyyy").ToString() & " 00:00:00"
                Dim EndtDate As String = Format(dateEnd.Value, "MM/dd/yyyy").ToString() & "  23:59:00"

                If CDate(dtpDate.Value) > CDate(dateEnd.Value) Then
                    W_MSG_Error_ByIndex("500016")
                    dtpDate.Focus()
                    Exit Sub

                Else
                    strWhere = "  AND VIEW_WithDrawView.Withdraw_Date between '" & StartDate & "' and  '" & EndtDate & "'"
                End If

                '  strWhere = " AND tb_Order.Order_Date >= '" & dtpDate.Value.ToString("MM/dd/yyyy") & "' And '" & dtpDate.Value.AddDays(1).ToString("MM/dd/yyyy") & "'"
            ElseIf Me.rdbWithdraw_No.Checked = True Then
                strWhere = " AND VIEW_WithDrawView.Withdraw_No like '" & Me.txtKeySearch.Text & "%'"
            ElseIf Me.rdbCustomer.Checked = True Then
                If Not txtKeySearch.Text = "" Then
                    If Not txtKeySearch.Text = "" Then
                        If txtKeySearch.Tag Is Nothing Then
                            strWhere = " AND VIEW_WithDrawView.Customer_Name Like '" & Me.txtKeySearch.Text & "%' "
                        Else
                            strWhere = " AND VIEW_WithDrawView.customer_Index = '" & Me.txtKeySearch.Tag & "' "
                        End If
                    End If
                End If
            ElseIf Me.rdbCustomerShipping.Checked = True Then
                If Not String.IsNullOrEmpty(txtKeySearch.Text.Trim) Then
                    If txtKeySearch.Tag Is Nothing Then
                        strWhere = " AND VIEW_WithDrawView.Company_Name Like '" & Me.txtKeySearch.Text & "%' "
                    Else
                        strWhere = " AND VIEW_WithDrawView.Customer_Shipping_Index = '" & Me.txtKeySearch.Tag & "' "
                    End If
                End If
            ElseIf rdbDepartment.Checked = True Then
                strWhere = " AND VIEW_WithDrawView.Department_Des Like '" & Me.txtKeySearch.Text & "%' "
            ElseIf rdbReferent.Checked = True Then
                strWhere = " AND VIEW_WithDrawView.Ref_No1 Like '" & Me.txtKeySearch.Text & "%' "
            ElseIf rdb_Sku.Checked = True Then
                strWhere = " and VIEW_WithDrawView.Withdraw_Index in(Select  Withdraw_Index from tb_WithdrawItem LEFT JOIN "
                strWhere &= " ms_SKU on tb_WithdrawItem.sku_index = ms_SKU.sku_index "
                strWhere &= " where ms_SKU.Sku_Id Like '" & Me.txtKeySearch.Text & "%' ) "
            End If
            ' ********************
            If rdbSO.Checked = True Then
                strWhere = " AND VIEW_WithDrawView.WithDraw_Index In (Select WI.WithDraw_Index From tb_WithDrawItem WI where WI.DocumentPlan_No Like '" & Me.txtKeySearch.Text & "%' AND WI.WithDraw_Index = VIEW_WithDrawView.WithDraw_Index ) "
            End If

            Select Case Me.cboDocumentStatus.SelectedValue
                Case 0
                    strWhere &= " "
                Case Else
                    strWhere &= " AND VIEW_WithDrawView.Status=" & Me.cboDocumentStatus.SelectedValue
            End Select


            Select Case Me.cbWithDrawType.SelectedValue
                Case 0
                    strWhere &= " "
                Case Else
                    strWhere &= " AND VIEW_WithDrawView.DocumentType_Index=" & Me.cbWithDrawType.SelectedValue
            End Select

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()
            strWhere &= New clsUserByDC().GetDistributionCenterByUser()

            'objClassDB.getViewWithdrawAsset(strWhere)
            'objDT = objClassDB.DataTable

            'ADD BY Pong 28/04/2015
            Me.cboRowPerPage.SelectedIndex = IIf(Me.cboRowPerPage.SelectedIndex > 0, Me.cboRowPerPage.SelectedIndex, 0)
            If rdTop.Checked Then
                Me.txtTop.Text = IIf(IsNumeric(Me.txtTop.Text), Me.txtTop.Text, 1)
                Me.txtTop.Text = IIf(CInt(Me.txtTop.Text) > 0, CInt(Me.txtTop.Text), 1)
                objClassDB.getViewWithdrawAsset(strWhere, 1, CInt(Me.txtTop.Text))
            ElseIf rdRowPage.Checked Then
                Me.txtPageIndex.Text = IIf(IsNumeric(Me.txtPageIndex.Text), Me.txtPageIndex.Text, 1)
                Me.txtPageIndex.Text = IIf(CInt(Me.txtPageIndex.Text) > 0, CInt(Me.txtPageIndex.Text), 1)
                objClassDB.getViewWithdrawAsset(strWhere, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)) - CInt(Me.cboRowPerPage.Text) + 1, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)))
            Else
                objClassDB.getViewWithdrawAsset(strWhere, -1, 0)
            End If
            Calculate_Paging()

            objDT = objClassDB.GetDataTable

            If objDT.Rows.Count > 0 Then
                Me.txtRowCount.Text = objDT.Rows(0)("ROW_COUNT").ToString
            Else
                Me.txtRowCount.Text = 0
            End If

            ' Me.grdList.DataSource = objDT
            ' *** Clear datagridview ***
            Me.grdWithdrawView.Rows.Clear()
            Me.grdWithdrawView.Refresh()

            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdWithdrawView
                    Me.grdWithdrawView.Rows.Add()
                    .Rows(i).Cells("System_Index").Value = objDT.Rows(i).Item("Withdraw_Index").ToString
                    .Rows(i).Cells("Document_Date").Value = Format(CDate(objDT.Rows(i).Item("Withdraw_Date")), "dd/MM/yyyy").ToString '  ' Format(Today, "dd/MM/yyyy").ToString 
                    .Rows(i).Cells("Document_No").Value = objDT.Rows(i).Item("Withdraw_No").ToString
                    .Rows(i).Cells("Customer_Name").Value = objDT.Rows(i).Item("Customer_Name").ToString
                    '  *** Reference By Tag property  to check Enable Button ***
                    .Rows(i).Cells("Status").Tag = objDT.Rows(i).Item("Status").ToString
                    .Rows(i).Cells("Status").Value = objDT.Rows(i).Item("StatusDescription").ToString
                    .Rows(i).Cells("Add_By").Value = objDT.Rows(i).Item("Add_By").ToString
                    '.Rows(i).Cells("col_Consignee").Value = objDT.Rows(i).Item("Consignee").ToString

                    .Rows(i).Cells("clWithdrawType").Value = objDT.Rows(i).Item("DocumentType").ToString
                    .Rows(i).Cells("clReferrent").Value = objDT.Rows(i).Item("Ref_No1").ToString
                    .Rows(i).Cells("Col_Customer_Shipping").Value = objDT.Rows(i).Item("Company_Name").ToString
                    .Rows(i).Cells("Col_So").Value = objDT.Rows(i).Item("SO_No").ToString
                    '.Rows(i).Cells("Col_AssignJob_Index").Value = objDT.Rows(i).Item("AssignJob_Index").ToString
                    '.Rows(i).Cells("Col_userName").Value = objDT.Rows(i).Item("userFullName").ToString

                    ' ------ Jua 2011/09/15 ------
                    'เพิ่ม col_statusConfirm,col_statusCancel เพื่อเปิด ปิด ปุ่มยืนยันและยกเลิกตาม  DocumentType 
                    If objDT.Columns.Contains("statusConfirm") Then
                        .Rows(i).Cells("col_statusConfirm").Value = objDT.Rows(i).Item("statusConfirm").ToString
                    Else
                        .Rows(i).Cells("col_statusConfirm").Value = "-99"
                    End If
                    If objDT.Columns.Contains("statusCancel") Then
                        .Rows(i).Cells("col_statusCancel").Value = objDT.Rows(i).Item("statusCancel").ToString
                    Else
                        .Rows(i).Cells("col_statusCancel").Value = "-99"
                    End If
                    '----------------------------------

                    Dim intStatus As Integer = objDT.Rows(i).Item("Status")
                    Select Case intStatus
                        Case -1
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.Pink
                        Case 2
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightGreen
                        Case 4
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightYellow
                        Case Else

                    End Select
                    'เติมสินค้า
                    Dim intStatus_Fullfill As Integer = IIf(objDT.Rows(i).Item("xFullFill").ToString = "", 0, objDT.Rows(i).Item("xFullFill").ToString)
                    Select Case intStatus_Fullfill
                        Case 0
                        Case Else
                            .Rows(i).Cells("col_Status_Fullfill").Value = "รอเติมสินค้า"
                            .Rows(i).Cells("col_Status_Fullfill").Style.BackColor = Color.Yellow
                    End Select
                    .Rows(i).Cells("col_DistributionCenter_Desc").Value = objDT.Rows(i).Item("DistributionCenter_Desc").ToString
                    .Rows(i).Cells("col_Activity").Value = objDT.Rows(i).Item("Activity").ToString
                End With

            Next


            If grdWithdrawView.Rows.Count > 0 Then
                grdWithdrawView.Rows(0).Selected = False

            End If

            btnEdit_Admin.Enabled = False
            btnConfirm.Enabled = False
            btnCancel.Enabled = False
            btnAssignJob.Enabled = False
            btnEditOrder.Enabled = False



            'If Not objCustomSetting.getConfig_Key_USE("USE_WITHDRAW_ASSIGNJOB_MULTI_USER") Then
            '    If grdWithdrawView.Rows.Count > 0 Then
            '        grdWithdrawView.Rows(0).Selected = False
            '        GetAssignJob()
            '    End If
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try



    End Sub

#End Region

#Region "STATUS OF DOCUMENT "
    Private Sub getProcessStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus()
            objDT = objClassDB.DataTable

            With cboDocumentStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
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



    Private Sub getDocumentType()
        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(2)
            objDT = objClassDB.DataTable

            Dim cbItem(1) As String
            cbItem(0) = "0"
            cbItem(1) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            With cbWithDrawType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************
            cbWithDrawType.SelectedIndex = cbWithDrawType.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
#End Region

#Region "Function"
    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
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

    Sub SetnumRows()
        Try
            Dim numRows As Integer = 0

            numRows = grdWithdrawView.Rows.Count
            If numRows > 0 Then
                'lbCountRows.Text = GetMessage_Data(400028) & numRows & GetMessage_Data(400029) & numRows & GetMessage_Data(400030)
                lbCountRows.Text = GetMessage_Data(400028) & CInt(Me.txtRowCount.Text) & GetMessage_Data(400029) & numRows & GetMessage_Data(400030)
            Else
                lbCountRows.Text = GetMessage_Data(400031)
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Sub SetDEFAULT_USE_REPORTPRINTOUT_BOND()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_REPORTPRINTOUT_BOND", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.DEFAULT_USE_REPORTPRINTOUT_BOND = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DEFAULT_USE_REPORTPRINTOUT_BOND = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Function GetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        'Public CustomerID As String = ""
        'Public CustomerName As String = ""

        Try
            objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                CustomerID = objDT.Rows(0).Item("Customer_Id").ToString
                CustomerName = objDT.Rows(0).Item("Customer_Name").ToString

            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

        Return CustomerID
        Return CustomerName
        Return Customer_Index

    End Function
#End Region
    'เพิ่ม config USE_WITHDRAW_ASSIGNJOB_MULTI_USER 0 = false ไม่ใช้   1 = trueใช้ (ปุ่มมอบหมายงาน) 23-06-2011
    Private Sub frmOderView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim oFunction As New W_Language
            'oFunction.SwitchLanguage(Me, 2)
            'oFunction.SW_Language_Column(Me, Me.grdWithdrawView, 2)
            'oFunction = Nothing
            Me._USE_ASSIGNEJOB_ITEM = objCustomSetting.getConfig_Key_USE("USE_ASSIGNEJOB_ITEM")
            If objCustomSetting.getConfig_Key_USE("USE_WITHDRAW_ASSIGNJOB_MULTI_USER") Then
                btnAssignJob.Visible = True
                grdWithdrawView.ContextMenuStrip = Nothing
                grdWithdrawView.Columns(0).Visible = False
            Else
                btnAssignJob.Visible = False
                Me.grdWithdrawView.ContextMenuStrip = Me.mnuAssignJob
                grdWithdrawView.Columns(0).Visible = True

            End If
            oFunction.SW_Language_Column(Me, Me.grdWithdrawView, 2)
            oFunction.SwitchLanguage(Me, 2)

            Me.cboRowPerPage.SelectedIndex = 0
            Dim CustomerID As String

            CustomerID = GetDEFAULT_CUSTOMER_INDEX()

            Me.getReportName(2)
            Me.GetDEFAULT_CUSTOMER_INDEX()

            '   cbPrint.SelectedIndex = 0
            Me.getProcessStatus()
            Me.getDocumentType()
            Me.getWithdrawView()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnConfirmJobWithdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'If W_MSG_Confirm("คุณต้องการยืนยันใบเบิกสินค้าใช่หรือไม่") = Windows.Forms.DialogResult.No Then
        If W_MSG_Confirm_ByIndex("100041") = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        ' *** Call Function for Withdraw balance ***
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
        Try
            If objClassDB.Withdraw_Confirm(Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value) = True Then
                GetMessage_Data("400002")
            Else
                ' W_MSG_Information("ไม่สามารถบันทึกข้อมูลได้")
            End If
            Me.getWithdrawView()
        Catch ex As Exception
            Dim strMsg1 As String = vbNewLine
            Dim strMsg2 As String = GetMessage_Data("500010")
            Dim strMsg3 As String = ex.Message
            Dim strMsg4 As String = strMsg1 & " " & strMsg2 & " " & strMsg3
            W_MSG_Error(strMsg4)
        Finally
            objClassDB = Nothing

        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbWithdraw_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbWithdraw_Date.CheckedChanged
        Try
            ' Me.dtpDate.Visible = True
            Me.txtKeySearch.Text = ""

            If rdbWithdraw_Date.Checked = True Then
                Me.lb_to.Visible = True
                Me.dtpDate.Visible = True
                Me.dateEnd.Visible = True
                Me.txtKeySearch.Visible = False
            Else
                Me.lb_to.Visible = False
                Me.dtpDate.Visible = False
                Me.dateEnd.Visible = False
                Me.txtKeySearch.Visible = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub rdbWithdraw_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbWithdraw_No.CheckedChanged

        Try
            Me.dtpDate.Visible = False
            Me.txtKeySearch.Visible = True
            Me.txtKeySearch.Text = ""
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged
        Try
            Me.dtpDate.Visible = False
            Me.txtKeySearch.Visible = True
            Me.txtKeySearch.Text = ""
            Me.btnPop_Search.Visible = rdbCustomer.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub cboDocumentStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentStatus.SelectionChangeCommitted
        Try
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdWithdrawView_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdWithdrawView.RowsAdded
        Try
            SetnumRows()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdWithdrawView_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdWithdrawView.RowsRemoved
        Try
            SetnumRows()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnNewWithdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewWithdraw.Click
        Try
            Dim frm As New frmWithdrawAsset_V4(frmWithdrawAsset_V4.enuOperation_Type.ADDNEW)
            frm.withdrawType = _withdrawType

            With frm
                .CustomerID = Me.CustomerID
                .Customer_Index = Me.Customer_Index
                .CustomerName = Me.CustomerName
                ._ConfigPrint = Me._ConfigPrint
            End With

            frm.Icon = Me.Icon
            frm.ShowDialog()
            btnEditOrder.Enabled = True

            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnEditOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditOrder.Click
        Try
            If grdWithdrawView.Rows.Count <= 0 Then Exit Sub
            Dim Whitdraw_Index As String = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString

            Dim objCheck As New WMS_STD_OUTB_Datalayer.Cl_WithdrawReserv
            Dim tmpString As String = objCheck.CheckReserve(Whitdraw_Index)
            If tmpString <> "S" Then
                W_MSG_Information(tmpString)
                Me.getWithdrawView()
                Exit Sub
            End If



            If grdWithdrawView.CurrentRow.Cells("Status").Tag = "2" Or grdWithdrawView.CurrentRow.Cells("Status").Tag = "-1" Then
                Dim frm As New frmWithdrawAsset_V4(frmWithdrawAsset_V4.enuOperation_Type.UPDATE, Whitdraw_Index)
                frm.WithdrawStatus = True
                frm._ConfigPrint = Me._ConfigPrint
                frm.ShowDialog()
                Me.getWithdrawView()
            Else
                Whitdraw_Index = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString
                Dim frm As New frmWithdrawAsset_V4(frmWithdrawAsset_V4.enuOperation_Type.UPDATE, Whitdraw_Index)
                frm.DocumentPlan_Index = Whitdraw_Index
                frm.DocumentPlan_No = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("Document_No").Value.ToString
                frm._ConfigPrint = Me._ConfigPrint
                frm.ShowDialog()
                Me.getWithdrawView()

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Function ConfrimAdminPassword() As Boolean
        Try
            Select Case grdWithdrawView.CurrentRow.Cells("Status").Tag.ToString
                Case "-2"
                    Dim frmpassword As New WMS_STD_Master.PopupEnterPassword
                    frmpassword.ShowDialog()
                    If frmpassword.passwordistrue = False Then
                        Return False
                    Else
                        Return True
                    End If
                Case Else
                    Return True
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If grdWithdrawView.Rows.Count <= 0 Then Exit Sub
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
        Dim objSerial As New tb_WithdrawItemSerial
        Try

            Dim strStrAlert1 As String = GetMessage_Data("100039")
            Dim strStrAlert2 As String = Me.grdWithdrawView.CurrentRow.Cells("Document_No").Value
            Dim strStrAlert3 As String = GetMessage_Data("100040")
            Dim strStrAlert4 As String = strStrAlert1 & " " & strStrAlert2 & " " & strStrAlert3

            If ConfrimAdminPassword() = True Then
                If W_MSG_Confirm(strStrAlert4) = Windows.Forms.DialogResult.Yes Then

                    'Dim SerialDuplicate As String = objSerial.CheckCancleWithdraw(Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value.ToString)
                    'If Not String.IsNullOrEmpty(SerialDuplicate) Then
                    '    W_MSG_Information(String.Format("ไม่สามารถยกเลิกรายการได้ เนื่องจากมีการรับคืนของ Serial {0} !! ", SerialDuplicate))
                    '    Exit Sub
                    'End If


                    Dim strchkWithDraw As String = objClassDB.Withdraw_Cancel(Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value)
                    If strchkWithDraw = "PASS" Then
                        Dim objcon As New DBType_SQLServer
                        Dim xSql As String = ""
                        xSql &= String.Format("update tb_Withdraw set Activity_Id = 7,Activity = 'เสร็จสิ้น' where Withdraw_Index = '{0}'", Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value)
                        xSql &= String.Format(" delete tb_SalesOrderPackingItem where SalesOrderItem_Index in ( select DocumentPlanItem_Index from tb_WithdrawItem where Withdraw_Index = '{0}')", Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value)
                        xSql &= String.Format(" delete tb_SalesOrderPacking  where SalesOrderPacking_Index not in (select SalesOrderPacking_Index from tb_SalesOrderPackingItem)  ", "")
                        xSql &= String.Format(" delete tb_SalesOrderPacking_Barcode  where SalesOrderPacking_Index not in (select SalesOrderPacking_Index from tb_SalesOrderPackingItem)  ", "")
                        xSql &= String.Format(" delete tb_SalesOrderPacking_Withdraw  where SalesOrderPacking_Index not in (select SalesOrderPacking_Index from tb_SalesOrderPackingItem)  ", "")


                        objcon.DBExeNonQuery(xSql)

                        W_MSG_Information_ByIndex("300034")
                    Else
                        W_MSG_Information(strchkWithDraw)
                    End If
                    Me.getWithdrawView()
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print.Click

        Try
            If grdWithdrawView.RowCount <= 0 Then
                Exit Sub
            End If
            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString

            Try
                Select Case Report_Name.ToUpper
                    Case "WTH_PRINTOUT_V1"
                        Dim oReport As New clsReport()
                        Dim Withdraw_Index_IN As String = ""

                        For intRow As Integer = 0 To grdWithdrawView.Rows.Count - 1
                            If grdWithdrawView.Rows(intRow).Cells("chkSelect").Value = True Then
                                Withdraw_Index_IN &= "'" & grdWithdrawView.Rows(intRow).Cells("System_Index").Value.ToString() & "',"
                            End If
                        Next
                        If Withdraw_Index_IN.Length = 0 Then
                            W_MSG_Information("กรุณาเลือกรายการ")
                            Exit Sub
                        End If
                        Withdraw_Index_IN = Withdraw_Index_IN.Substring(0, Withdraw_Index_IN.Trim.Length - 1)
                        Dim strWhere As String = " and Withdraw_Index in(" & Withdraw_Index_IN & ")"

                        Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                        rpt = oReport.GetReportInfo(Report_Name, strWhere)

                        frm.CrystalReportViewer1.ReportSource = rpt
                        frm.ShowDialog()
                    Case "WTH_PRINTOUT_V2"
                        Dim oReport As New clsReport()
                        Dim Withdraw_Index_IN As String = ""

                        For intRow As Integer = 0 To grdWithdrawView.Rows.Count - 1
                            If grdWithdrawView.Rows(intRow).Cells("chkSelect").Value = True Then
                                Withdraw_Index_IN &= "'" & grdWithdrawView.Rows(intRow).Cells("System_Index").Value.ToString() & "',"
                            End If
                        Next
                        If Withdraw_Index_IN.Length = 0 Then
                            W_MSG_Information("กรุณาเลือกรายการ")
                            Exit Sub
                        End If
                        Withdraw_Index_IN = Withdraw_Index_IN.Substring(0, Withdraw_Index_IN.Trim.Length - 1)
                        Dim strWhere As String = " and Withdraw_Index in(" & Withdraw_Index_IN & ")"

                        Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                        rpt = oReport.GetReportInfo(Report_Name, strWhere)

                        frm.CrystalReportViewer1.ReportSource = rpt
                        frm.ShowDialog()
                    Case Else
                        Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                        Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And Withdraw_index ='" & Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value & "'")
                        frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                        frm.ShowDialog()
                End Select

                'SetDEFAULT_USE_REPORTPRINTOUT_BOND()
                'Select Case DEFAULT_USE_REPORTPRINTOUT_BOND ' Standard
                '    Case 0
                '        Dim frm As New frmWithdrawReport
                '        frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And Withdraw_Index ='" & grdWithdrawView.CurrentRow.Cells("System_Index").Value & "'")
                '        frm.ShowDialog()
                '    Case 1 ' BOND 

                '        If cboPrint.SelectedIndex = 0 Then
                '            ' todo :  รอฟอร์ม
                '            Dim frm As New Object '  frmReportMain_Bond
                '            frm.Document_Index = Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value
                '            frm.Report_Id = "DELIVERY_NOTE_BOND"
                '            frm.ShowDialog()

                '        ElseIf cboPrint.SelectedIndex = 1 Then
                '            Dim frm As New frmWithdrawReport
                '            frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And Withdraw_Index ='" & grdWithdrawView.CurrentRow.Cells("System_Index").Value & "'")
                '            frm.ShowDialog()

                '        ElseIf cboPrint.SelectedIndex = 2 Then
                '            'Dim frm As New frmWithdrawReport
                '            'frm.Document_Index = Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value
                '            'frm.Report_Name = "WTH_PrintOut"
                '            'frm.ShowDialog()
                '            Dim frm As New frmWithdrawReport
                '            frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And Withdraw_Index ='" & _withdraw_index & "'")
                '            frm.ShowDialog()
                '        End If

                'End Select
                '###################################
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnOldWithdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If grdWithdrawView.CurrentRow.Cells("Status").Tag = "2" Or grdWithdrawView.CurrentRow.Cells("Status").Tag = "-1" Then
                Dim Whitdraw_Index1 As String = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString
                Dim frm1 As New WMS_STD_OUTB_WithDraw.frmWithdraw_ItemView(Whitdraw_Index1)
                frm1.WithdrawStatus = 2
                frm1.SetWithdraw_Index = Whitdraw_Index1
                frm1.ShowDialog()

            Else

                If grdWithdrawView.Rows.Count <= 0 Then Exit Sub
                Dim Whitdraw_Index As String = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString
                Dim frm As New WMS_STD_OUTB_WithDraw.frmWithdraw_ItemView(Whitdraw_Index)
                frm.SetWithdraw_Index = Whitdraw_Index
                frm.ShowDialog()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub cbWithDrawType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbWithDrawType.SelectionChangeCommitted
        Try
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPop_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPop_Search.Click
        Try
            If rdbCustomer.Checked = True Then
                Dim frm As New frmCustmer_Popup
                frm.ShowDialog()
                '    *** Recive value ****
                Dim tmpCustomer_Index As String = ""
                tmpCustomer_Index = frm.Customer_Index 'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                If Not tmpCustomer_Index = "" Then
                    Me.txtKeySearch.Text = frm.customerName
                    Me.txtKeySearch.Tag = frm.Customer_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
                ' *********************
                frm.Close()
            End If
            If rdbCustomerShipping.Checked = True Then
                Dim frm As New frmCusship_Popup
                frm.ShowDialog()
                '    *** Recive value ****
                Dim tmpCustomerShipping_Index As String = String.Empty
                tmpCustomerShipping_Index = frm.Customer_Shipping_Index  'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                If Not String.IsNullOrEmpty(tmpCustomerShipping_Index) Then
                    Me.txtKeySearch.Text = frm.strCustomer_Shippingr_Name
                    Me.txtKeySearch.Tag = frm.Customer_Shipping_Index
                Else
                    Me.txtKeySearch.Text = String.Empty
                    Me.txtKeySearch.Tag = String.Empty
                End If
                ' *********************
                frm.Close()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdWithdrawView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithdrawView.CellDoubleClick
        Try
            btnEditOrder_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdWithdrawView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithdrawView.CellClick
        Try
            If grdWithdrawView.RowCount <= 0 Then Exit Sub
            Me.btnEdit_Admin.Enabled = False
            If grdWithdrawView.CurrentRow.Cells("Status").Tag = "-1" Then
                Me.btnConfirm.Enabled = False
                Me.btnConfirm_BarCode.Enabled = False
                Me.btnCancel.Enabled = False
                btnAssignJob.Enabled = False
                Me.btnEditOrder.Enabled = True

            ElseIf grdWithdrawView.CurrentRow.Cells("Status").Tag = "2" Then
                Me.btnConfirm.Enabled = False
                Me.btnConfirm_BarCode.Enabled = False
                Me.btnEditOrder.Enabled = True
                Me.btnAssignJob.Enabled = False
                Me.btnCancel.Enabled = True
                Me.btnEdit_Admin.Enabled = True
            Else
                Me.btnConfirm.Enabled = True
                Me.btnConfirm_BarCode.Enabled = True
                Me.btnEditOrder.Enabled = True
                Me.btnAssignJob.Enabled = True
                Me.btnCancel.Enabled = True
            End If


            ' ------ Jua 2011/09/15 ------
            'เปิด ปิดปุ่ม ยืนยันและยกเลิกตาม  ms_DocumentType จาก Fild Allow_Confirm, Allow_Cancel_Status
            Dim arrstatus() As String
            arrstatus = grdWithdrawView.CurrentRow.Cells("col_statusConfirm").Value.ToString.Split(",")
            If Not (arrstatus.Length = 1 And (arrstatus(0) = "-99" Or arrstatus(0) = "")) Then
                'If (arrstatus(0) = "" Or arrstatus(0) = "0") Then
                Dim liststatus As New List(Of String)
                liststatus.AddRange(arrstatus)
                If liststatus.Contains(grdWithdrawView.CurrentRow.Cells("Status").Tag) Then
                    Me.btnConfirm.Enabled = True
                Else
                    Me.btnConfirm.Enabled = False
                End If
                'End If
            End If
            Dim arrstatuscancel() As String
            arrstatuscancel = grdWithdrawView.CurrentRow.Cells("col_statusCancel").Value.ToString.Split(",")
            If Not (arrstatuscancel.Length = 1 And (arrstatuscancel(0) = "-99" Or arrstatuscancel(0) = "")) Then
                'If (arrstatuscancel(0) = "" Or arrstatuscancel(0) = "0") Then
                Dim liststatusCancel As New List(Of String)
                liststatusCancel.AddRange(arrstatuscancel)
                If liststatusCancel.Contains(grdWithdrawView.CurrentRow.Cells("Status").Tag) Then
                    Me.btnCancel.Enabled = True
                Else
                    Me.btnCancel.Enabled = False
                End If
                'End If
            End If
            ' -----------------------------

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnConfirm_BarCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm_BarCode.Click
        Try
            Dim frm As New WMS_STD_OUTB_WithDraw.frmWithDraw_BarcodeConfirm
            frm.WithDraw_Index = Me.grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString
            frm.WithDraw_No = Me.grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("Document_No").Value.ToString
            frm.ShowDialog()
            Me.getWithdrawView()
            Me.btnConfirm.Enabled = False
            Me.btnConfirm_BarCode.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbCustomerShipping_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomerShipping.CheckedChanged
        Try
            Me.dtpDate.Visible = False
            Me.txtKeySearch.Visible = True
            Me.txtKeySearch.Text = String.Empty
            Me.btnPop_Search.Visible = rdbCustomerShipping.Checked
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region " Assign Job  "

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa
    '''     - Select User  (ไม่เอา User ที่เป็น Customer,Supplier )
    '''     - Sum จำนวนเอกสารของแต่ละ User โดยเอาเฉพาะเอกสารใบเบิกที่มีสถานะเท่ากับ 'รอยืนยัน')
    ''' 
    ''' Update Date : 31/08/2010
    ''' Update By : Krit
    ''' เพิ่มเรื่องมอบหมายงานให้ทุกคนได้โดยไม่ระบุ แต่ต้อง config DB ใน se_User.User_Index = '0010000000001' เท่านั้น
    ''' </remarks>
    ''' 
    Private Sub GetAssignJob()
        Try
            Dim oUserAssignJob As New tb_AssignJob
            Dim odtUser As DataTable

            oUserAssignJob.getUser_AssignJobWithDraw()
            odtUser = oUserAssignJob.DataTable

            Me.AssignToToolStripMenuItem.DropDownItems.Clear()

            'Menu มอบหมายงานให้ทุกคน
            Dim odrAssigAll() As DataRow
            odrAssigAll = odtUser.Select("user_index = '0010000000001'")
            If odrAssigAll.Length > 0 Then
                Dim oMenuItem_AssignAll As New Windows.Forms.ToolStripMenuItem
                oMenuItem_AssignAll.Text = odrAssigAll(0)("userFullName").ToString & "( " & odrAssigAll(0)("Sum_DocAssign").ToString & " )"
                oMenuItem_AssignAll.Tag = odrAssigAll(0)("user_index").ToString

                Me.AssignToToolStripMenuItem.DropDownItems.Add(oMenuItem_AssignAll)

                AddHandler oMenuItem_AssignAll.Click, AddressOf AssignTo_Click
            End If

            '=====================================================

            'Menu ยกเลิก
            Dim oMenuItem_Cancel As New Windows.Forms.ToolStripMenuItem
            Dim oToolStripSeparator As New Windows.Forms.ToolStripSeparator
            oMenuItem_Cancel.Text = "ยกเลิก"
            oMenuItem_Cancel.Tag = -1

            Me.AssignToToolStripMenuItem.DropDownItems.Add(oMenuItem_Cancel)
            Me.AssignToToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {oToolStripSeparator})

            AddHandler oMenuItem_Cancel.Click, AddressOf AssignTo_Click
            '=====================================================

            'Menu Assign งานให้แต่ละคน
            If odtUser.Rows.Count > 0 Then
                For Each odr As DataRow In odtUser.Select("user_index <> '0010000000001'", "userFullName")
                    Dim oMenuItem As New Windows.Forms.ToolStripMenuItem

                    oMenuItem.Text = odr("userFullName").ToString & "( " & odr("Sum_DocAssign").ToString & " )"
                    oMenuItem.Tag = odr("user_index").ToString

                    Me.AssignToToolStripMenuItem.DropDownItems.Add(oMenuItem)

                    AddHandler oMenuItem.Click, AddressOf AssignTo_Click

                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa
    '''     - Check Status (จะ AssignJob ได้เอกสารต้องมีสถานะ รอยืนยัน เท่านั้น)
    '''     - Innsert/Update Data To tb_AssignJob   
    ''' </remarks>
    Private Sub AssignTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim oAssign As New tb_AssignJob
            Dim objDBIndex As New Sy_AutoNumber
            Dim booStatusAssign As Boolean = False
            Dim booStatusChkSelect As Boolean = False

            grdWithdrawView.EndEdit()
            If grdWithdrawView.RowCount = 0 Then
                Exit Sub
            End If

            ' Check Status (จะ AssignJob ได้เอกสารต้องมีสถานะ รอยืนยัน เท่านั้น)
            For intRow As Integer = 0 To grdWithdrawView.Rows.Count - 1
                If grdWithdrawView.Rows(intRow).Cells("chkSelect").Value = True Then
                    booStatusChkSelect = True
                    If grdWithdrawView.Rows(intRow).Cells("Status").Tag <> 1 Then
                        W_MSG_Information(GetMessage_Data("400036") & grdWithdrawView.Rows(intRow).Cells("Document_No").Value & GetMessage_Data("400037"))
                        Exit Sub
                    End If

                End If

            Next
            If booStatusChkSelect = False Then
                W_MSG_Information(GetMessage_Data("400038"))
                Exit Sub
            End If

            ' Innsert/Update Data To tb_AssignJob
            For intRow As Integer = 0 To grdWithdrawView.Rows.Count - 1
                If grdWithdrawView.Rows(intRow).Cells("chkSelect").Value = True Then
                    With oAssign
                        .User_Index = sender.tag
                        .Assign_Date = Now
                        .DocumentPlan_No = Me.grdWithdrawView.Rows(intRow).Cells("Document_No").Value.ToString
                        .DocumentPlan_Index = Me.grdWithdrawView.Rows(intRow).Cells("System_Index").Value.ToString
                        .Plan_Process = 2
                        .Priority = 3

                        If Me.grdWithdrawView.Rows(intRow).Cells("col_AssignJob_Index").Value.ToString = "" Then
                            .AssignJob_Index = objDBIndex.getSys_Value("AssignJob_Index")
                            booStatusAssign = .InsertData()
                        Else
                            .AssignJob_Index = Me.grdWithdrawView.Rows(intRow).Cells("col_AssignJob_Index").Value.ToString
                            booStatusAssign = .UpdateData()
                        End If

                    End With

                End If

            Next
            ' Check Status การ Assign
            If booStatusAssign = True Then
                W_MSG_Information(GetMessage_Data("400039"))

            Else
                W_MSG_Information(GetMessage_Data("400040"))
            End If

            Me.getWithdrawView()
            Me.GetAssignJob()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="pPriority"></param>
    ''' <remarks>
    ''' Create Date :  2/04/2010
    ''' Create By  : TaTa
    '''    Set Priority
    '''        1 = เร่งด่วนมาก
    '''        2 = เร่งด่วน
    '''        3 = ปกติ
    '''        4 = ไม่เร่งด่วน
    '''        5 = ระงับ
    ''' </remarks>

    Private Sub SetPriority(ByVal pPriority As Integer)

        Dim oAssign As New tb_AssignJob
        Try
            If grdWithdrawView.Rows.Count = 0 Then
                Exit Sub
            End If

            grdWithdrawView.EndEdit()
            'For intRow As Integer = 0 To grdWithdrawView.Rows.Count - 1
            oAssign.SetPriority(pPriority, Me.grdWithdrawView.CurrentRow.Cells("col_AssignJob_Index").Value.ToString)
            'Next

            W_MSG_Information("แก้ไขข้อมูลเรียบร้อยแล้ว")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub VeryHightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles VeryHightToolStripMenuItem.Click
        Try
            SetPriority(1)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub HightToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HightToolStripMenuItem.Click
        Try
            SetPriority(2)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub NornalToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NornalToolStripMenuItem.Click
        Try
            SetPriority(3)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub LowToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LowToolStripMenuItem.Click
        Try
            SetPriority(4)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub HOLDToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles HOLDToolStripMenuItem.Click
        Try
            SetPriority(5)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

    Private Sub btnAssignJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignJob.Click
        Try
            If grdWithdrawView.Rows.Count < 0 Then
                Exit Sub
            End If

            If Me._USE_ASSIGNEJOB_ITEM Then
                Dim f As New frmAssignJobItem
                f.Process_ID = 2
                f.DocumentPlan_No = Me.grdWithdrawView.CurrentRow.Cells("Document_No").Value
                f.ShowDialog()
            Else
                If grdWithdrawView.CurrentRow.Cells("Status").Tag <> "2" Or grdWithdrawView.CurrentRow.Cells("Status").Tag <> "-1" Then
                    Dim frm As New WMS_STD_OUTB_WithDraw.frmPopup_AssignJob
                    frm.Process_ID = 2
                    frm.DocumentPlan_Index = Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value
                    frm.DocumentPlan_No = Me.grdWithdrawView.CurrentRow.Cells("Document_No").Value
                    frm.ShowDialog()

                    Me.getWithdrawView()

                End If
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtKeySearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.getWithdrawView()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnEdit_Admin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit_Admin.Click
        Try
            If grdWithdrawView.Rows.Count <= 0 Then Exit Sub
            Dim Whitdraw_Index As String = ""

            Dim frmpassword As New PopupEnterPassword
            'frmpassword.Group = WV_UserName
            frmpassword.ShowDialog()
            If frmpassword.passwordistrue = False Then
                Exit Sub
            End If

            Whitdraw_Index = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString
            Dim frm As New WMS_STD_OUTB_WithDraw.frmWithdrawAsset(WMS_STD_OUTB_WithDraw.frmWithdrawAsset.enuOperation_Type.UPDATE, Whitdraw_Index)
            Select Case grdWithdrawView.CurrentRow.Cells("Status").Tag
                Case "-1", "9"
                    frm.WithdrawStatus = True
                Case "2"
                    frm.WithdrawStatus = True
                    frm.IsAdmin = True
                Case Else

            End Select
            frm.ShowDialog()

            'If grdWithdrawView.CurrentRow.Cells("Status").Tag = "2" Or grdWithdrawView.CurrentRow.Cells("Status").Tag = "-1" Or grdWithdrawView.CurrentRow.Cells("Status").Tag = "9" Then
            '    frm.WithdrawStatus = True
            '    frm.ShowDialog()
            'Else
            '    Whitdraw_Index = grdWithdrawView.Rows(grdWithdrawView.CurrentRow.Index).Cells("System_Index").Value.ToString
            '    Dim frm As New frmWithdrawAsset_Update(frmWithdrawAsset_Update.enuOperation_Type.UPDATE, Whitdraw_Index)
            '    frm.ShowDialog()
            'End If
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdWithdrawView_CellEnter(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithdrawView.CellEnter
        Try
            If grdWithdrawView.RowCount <= 0 Then Exit Sub
            Me.btnEdit_Admin.Enabled = False
            If grdWithdrawView.CurrentRow.Cells("Status").Tag = "-1" Then
                Me.btnConfirm.Enabled = False
                Me.btnConfirm_BarCode.Enabled = False
                Me.btnCancel.Enabled = False
                btnAssignJob.Enabled = False
                Me.btnEditOrder.Enabled = True

            ElseIf grdWithdrawView.CurrentRow.Cells("Status").Tag = "2" Then
                Me.btnConfirm.Enabled = False
                Me.btnConfirm_BarCode.Enabled = False
                Me.btnEditOrder.Enabled = True
                Me.btnAssignJob.Enabled = False
                Me.btnCancel.Enabled = True
                Me.btnEdit_Admin.Enabled = True
            Else
                Me.btnConfirm.Enabled = True
                Me.btnConfirm_BarCode.Enabled = True
                Me.btnEditOrder.Enabled = True
                Me.btnAssignJob.Enabled = True
                Me.btnCancel.Enabled = True
            End If


            '' ------ Jua 2011/09/15 ------
            ''เปิด ปิดปุ่ม ยืนยันและยกเลิกตาม  ms_DocumentType จาก Fild Allow_Confirm, Allow_Cancel_Status
            'Dim arrstatus() As String
            'arrstatus = grdWithdrawView.CurrentRow.Cells("col_statusConfirm").Value.ToString.Split(",")
            'If Not (arrstatus.Length = 1 And (arrstatus(0) = "-99" Or arrstatus(0) = "")) Then
            '    'If (arrstatus(0) = "" Or arrstatus(0) = "0") Then
            '    Dim liststatus As New List(Of String)
            '    liststatus.AddRange(arrstatus)
            '    If liststatus.Contains(grdWithdrawView.CurrentRow.Cells("Status").Tag) Then
            '        Me.btnConfirm.Enabled = True
            '    Else
            '        Me.btnConfirm.Enabled = False
            '    End If
            '    'End If
            'End If
            'Dim arrstatuscancel() As String
            'arrstatuscancel = grdWithdrawView.CurrentRow.Cells("col_statusCancel").Value.ToString.Split(",")
            'If Not (arrstatuscancel.Length = 1 And (arrstatuscancel(0) = "-99" Or arrstatuscancel(0) = "")) Then
            '    'If (arrstatuscancel(0) = "" Or arrstatuscancel(0) = "0") Then
            '    Dim liststatusCancel As New List(Of String)
            '    liststatusCancel.AddRange(arrstatuscancel)
            '    If liststatusCancel.Contains(grdWithdrawView.CurrentRow.Cells("Status").Tag) Then
            '        Me.btnCancel.Enabled = True
            '    Else
            '        Me.btnCancel.Enabled = False
            '    End If
            '    'End If
            'End If
            ' -----------------------------

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmWithdrawAssetView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigWithdraw
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2)
                    oFunction.SW_Language_Column(Me, Me.grdWithdrawView, 2)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
   
        Dim str2 As String = Me.grdWithdrawView.CurrentRow.Cells("Document_No").Value
        Dim str1 As String = GetMessage_Data("100041")
        Dim str3 As String = GetMessage_Data("400022")
        Dim str4 As String = str1 & " " & str2 & " " & str3

        If W_MSG_Confirm(str4) = Windows.Forms.DialogResult.No Then
            Exit Sub
        End If
        ' *** Call Function for Withdraw balance ***
        Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
        Dim objCheckSerial As New tb_WithdrawItemSerial
        Try

            'If objCheckSerial.CheckWithdrawCreateSerialIsComplete(Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value) = False Then
            '    W_MSG_Information(" พบรายการที่ ระบุ Serial ไม่สมบุรณ์ กรุณาตรวจสอบ !! ")
            '    Exit Sub
            'End If

            '-----------------------------------------------------------------------------------
            Try
                'TODO : รีบใช้เลยฝัง Code ถ้้า Dev. มีเวลารบกวนย้ายให้ด้วยครับ
                'KSL : ตรวจสอบ
                '6	กำลังจัดสินค้า,8	รอส่งมอบสินค้า,9	รอแพ็คสินค้า,16	รอLoadสินค้า
                Dim objcon As New DBType_SQLServer
                Dim dtcon As New DataTable
                Dim xSql As String = ""
                xSql = " 	select count(*)"
                xSql &= " 	from tb_WithdrawItem"
                xSql &= " 	where Withdraw_Index = '" & Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value & "' and Status <> -9 "
                If objcon.DBExeQuery_Scalar(xSql) > 0 Then
                    If W_MSG_Confirm("หยิบสินค้าด้วย mobile ไม่ครบคุณต้องการตัด Stock ทั้งหมดใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
                objcon = Nothing
                dtcon = Nothing
            Catch ex1 As Exception
                W_MSG_Information(ex1.Message.ToString)
                Exit Sub
            End Try
            '-----------------------------------------------------------------------------------
            Try
                'TODO : รีบใช้เลยฝัง Code ถ้้า Dev. มีเวลารบกวนย้ายให้ด้วยครับ
                'KSL : ตรวจสอบ
                '6	กำลังจัดสินค้า,8	รอส่งมอบสินค้า,9	รอแพ็คสินค้า,16	รอLoadสินค้า
                Dim objcon As New DBType_SQLServer
                Dim dtcon As New DataTable
                Dim xSql As String = ""
                xSql = " 	select WI.* "
                xSql &= " 	from tb_WithdrawItem WI inner join tb_Withdraw W ON W.Withdraw_Index = WI.Withdraw_Index"
                xSql &= " 	where WI.Withdraw_Index = '" & Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value & "' and WI.Plan_Process = 10"
                xSql &= " 	and W.Activity_Id <> 6 "
                dtcon = objcon.DBExeQuery(xSql)
                If dtcon.Rows.Count > 0 Then
                    If W_MSG_Confirm("ใบสั่งยังโหลดสินค้าไม่ครบคุณต้องการยืนยันเอกสารใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If
                End If
                objcon = Nothing
                dtcon = Nothing
            Catch ex1 As Exception
                W_MSG_Information(ex1.Message.ToString)
                Exit Sub
            End Try

            '-----------------------------------------------------------------------------------


            Dim strchkWithDraw As String = objClassDB.Withdraw_Confirm(Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value)
            If strchkWithDraw = "PASS" Then

                'Auto confirm loading
                Dim objcon As New DBType_SQLServer
                Dim dtcon As New DataTable
                Dim xSql As String = ""
                xSql &= " Update tb_SalesOrderPacking Set Status = 2"
                xSql &= " where SalesOrder_Index in "
                xSql &= " ("
                xSql &= " 	select DocumentPlan_Index"
                xSql &= " 	from tb_WithdrawItem WI"
                xSql &= " 	where WI.Withdraw_Index = '" & Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value & "' and WI.Plan_Process = 10"
                xSql &= " 	Group by DocumentPlan_Index"
                xSql &= " )"
                objcon.DBExeNonQuery(xSql)

                xSql = String.Format("update tb_Withdraw set Activity_Id = 7,Activity = 'เสร็จสิ้น' where Withdraw_Index = '{0}'", Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value)
                objcon.DBExeNonQuery(xSql)

                xSql = String.Format("update tb_TransportManifest set Status = 13 where TransportManifest_Index in (select TransportManifest_Index from tb_withdrawitem where withdraw_index = '{0}')", Me.grdWithdrawView.CurrentRow.Cells("System_Index").Value)
                objcon.DBExeNonQuery(xSql)

                W_MSG_Information_ByIndex("400018")
            Else
                W_MSG_Information(strchkWithDraw)
            End If
            Me.getWithdrawView()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        Finally
            objClassDB = Nothing
        End Try

        btnEditOrder.Enabled = False

    End Sub




    'Private Sub btnUnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUnConfirm.Click

    '    With Me.grdWithdrawView
    '        If .Rows.Count > 0 Then
    '            If W_MSG_Confirm("ต้องการยกเลิกการยืนยันใบเบิกหรือไม่?") = Windows.Forms.DialogResult.No Then
    '                Exit Sub
    '            End If
    '            Dim objClassDB As New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA)
    '            Dim Lwithdraw_Index As New List(Of String)
    '            For i As Integer = 0 To .RowCount - 1
    '                If .Rows(i).Cells("chkselect").Value = True Then
    '                    If objClassDB.CheckStatusWithDraw(.Rows(i).Cells("System_Index").Value) Then
    '                        Lwithdraw_Index.Add(.Rows(i).Cells("System_Index").Value)
    '                    End If
    '                End If
    '            Next
    '            If Lwithdraw_Index.Count > 0 Then
    '                objClassDB.UpdateUnConfirmFlagWithdraw(Lwithdraw_Index)
    '            End If
    '        End If
    '    End With
    'End Sub

    'Top Control ADD BY Pong 28/04/2015
    Private Sub btnPageFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageFirst.Click
        Try
            txtPageIndex.Text = 1
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            txtPageIndex.Text -= 1
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            txtPageIndex.Text += 1
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            txtPageIndex.Text = txtTotalPage.Text
            Me.getWithdrawView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Calculate_Paging()
        ' Calculate Paging 
        Try
            Dim intRowCount As Integer
            Dim intRowPerPage As Integer

            intRowCount = CInt(Me.txtRowCount.Text)
            intRowPerPage = CInt(Me.cboRowPerPage.Text)

            ' row count = 0 ; page = 1 : total page = 1
            If intRowCount = 0 Or (intRowCount <= intRowPerPage) Then
                Me.txtTotalPage.Text = 1
                Me.txtPageIndex.Text = 1
            Else
                Me.txtTotalPage.Text = CInt(intRowCount / intRowPerPage)

                If CInt(Me.txtTotalPage.Text) * intRowPerPage < intRowCount Then
                    Me.txtTotalPage.Text = CInt(Me.txtTotalPage.Text) + 1
                End If
            End If

            Me.txtPageIndex.Text = IIf(IsNumeric(txtPageIndex.Text), Me.txtPageIndex.Text, 1)

            'Enable Button
            If CInt(Me.txtPageIndex.Text) = 1 Then
                gintRowStart = 1
                gintRowEnd = intRowPerPage

                Me.btnPagePrev.Enabled = False
                Me.btnPageFirst.Enabled = False
            Else
                gintRowEnd = CInt(Me.txtPageIndex.Text) * intRowPerPage
                gintRowStart = gintRowEnd - intRowPerPage + 1

                Me.btnPagePrev.Enabled = True
                Me.btnPageFirst.Enabled = True
            End If

            If CInt(Me.txtPageIndex.Text) = CInt(Me.txtTotalPage.Text) Then
                Me.btnPageNext.Enabled = False
                Me.btnPageLast.Enabled = False
            Else
                Me.btnPageNext.Enabled = True
                Me.btnPageLast.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub txtNubmeric_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTop.KeyPress, txtPageIndex.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(sender, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'end Top Control

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try
            Dim frm As New frmReportsWithdraw()
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnBarcodeB1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBarcodeB1.Click
        Try
            Dim frm As New frmCheckWithdraw
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAddTransportChargePackSize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddTransportChargePackSize.Click
        Try
            'If String.IsNullOrEmpty(Me.txtCarrier_ID.Tag) Then
            '    W_MSG_Information("กรุณาระบุ " & Me.lblCarrier.Text)
            '    Exit Sub
            'End If

            'Dim frm As New frmMainTransportManifestChargePackSize
            'frm.Carrier_Index = Me.txtCarrier_ID.Tag
            'frm.ShowDialog()

            Dim Service As New tb_TransportManifest_Update
            Dim Data As DataTable = Service.GetBarcodeB1(Me._withdraw_index)

            Data.Columns.Remove("Withdraw_Index")
            If Data IsNot Nothing AndAlso Data.Rows.Count > 0 Then
                Dim frmExport As New frmPopup_for_ExportExcel(Data)
                frmExport.ShowDialog()
            Else
                Throw New Exception("ไม่พบข้อมูล Barcode B1")
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
End Class