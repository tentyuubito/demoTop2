Imports System.Windows.Forms
Imports WMS_STD_Master
Imports WMS_STD_Formula
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_master.W_Language
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_OUTB_Reserv
Imports WMS_STD_TMM_Report
Imports WMS_STD_TMM_TransferStatus
Public Class frmAssetTransferView_V2

    Dim objHeader As New tb_TransferStatus
    Dim TransferStatus_Index As String = ""
    Private gGroup_Index As String = "0010000000011"
    Private _USE_ASSIGNEJOB_ITEM As Boolean = False
    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 1

#Region "Get DATA to DATAGRID VIEW "
    Private Sub getTransferStatusView()
        Dim objClassDB As New cls_KSL_SINO'TransferStatusTransaction
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""

        Try

            ' *** WHERE STRING *** 
            If Me.rdbTransferStatus_Date.Checked = True Then

                Dim StartDate As String = Format(dtpDate.Value, "yyyy/MM/dd").ToString()
                Dim EndtDate As String = Format(dateEnd.Value, "yyyy/MM/dd").ToString()
                strWhere = "  AND tb_TransferStatus.TransferStatus_Date between '" & StartDate & "' AND  '" & EndtDate & "'"

            ElseIf Me.rdbTransferStatus_No.Checked = True Then
                If Not Me.txtKeySearch.Text = "" Then
                    strWhere = " AND tb_TransferStatus.TransferStatus_No='" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "'"
                End If
            ElseIf Me.Rad_CusName.Checked = True Then
                strWhere = " AND ms_Customer.Customer_Name LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
            ElseIf Me.Rad_Ref1.Checked = True Then
                strWhere = " AND tb_TransferStatus.Ref_No1 LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
            End If
            ' ********************

            Select Case Me.cboDocumentStatus.SelectedValue
                Case 0
                    strWhere += " "
                Case Else
                    strWhere += " AND tb_TransferStatus.Status = " & Me.cboDocumentStatus.SelectedValue
            End Select


            Select Case Me.cboDocument_Type.SelectedValue
                Case "-1"
                    strWhere += " "
                Case Else
                    strWhere += " AND tb_TransferStatus.DocumentType_Index = '" & Me.cboDocument_Type.SelectedValue & "'"

            End Select

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()
            strWhere = strWhere.Replace("Customer_Index", "ms_Customer.Customer_Index")
            strWhere &= New clsUserByDC().GetDistributionCenterByUser()
            strWhere = strWhere.Replace("DistributionCenter_Index", "ms_DistributionCenter.DistributionCenter_Index")


            'ADD BY Pong 28/04/2015
            Me.cboRowPerPage.SelectedIndex = IIf(Me.cboRowPerPage.SelectedIndex > 0, Me.cboRowPerPage.SelectedIndex, 0)
            If rdTop.Checked Then
                Me.txtTop.Text = IIf(IsNumeric(Me.txtTop.Text), Me.txtTop.Text, 1)
                Me.txtTop.Text = IIf(CInt(Me.txtTop.Text) > 0, CInt(Me.txtTop.Text), 1)
                objClassDB.getTransferStatusView(strWhere, 1, CInt(Me.txtTop.Text))
            ElseIf rdRowPage.Checked Then
                Me.txtPageIndex.Text = IIf(IsNumeric(Me.txtPageIndex.Text), Me.txtPageIndex.Text, 1)
                Me.txtPageIndex.Text = IIf(CInt(Me.txtPageIndex.Text) > 0, CInt(Me.txtPageIndex.Text), 1)
                objClassDB.getTransferStatusView(strWhere, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)) - CInt(Me.cboRowPerPage.Text) + 1, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)))
            Else
                objClassDB.getTransferStatusView(strWhere, -1, 0)
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
            Me.grdTranferStatusView.Rows.Clear()
            Me.grdTranferStatusView.Refresh()

            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdTranferStatusView
                    Me.grdTranferStatusView.Rows.Add()
                    .Rows(i).Cells("System_Index").Value = objDT.Rows(i).Item("TransferStatus_Index").ToString
                    .Rows(i).Cells("Document_Date").Value = Format(CDate(objDT.Rows(i).Item("TransferStatus_Date")), "dd/MM/yyyy").ToString '  ' Format(Today, "dd/MM/yyyy").ToString 
                    .Rows(i).Cells("Document_No").Value = objDT.Rows(i).Item("TransferStatus_No").ToString
                    .Rows(i).Cells("Customer_Name").Value = objDT.Rows(i).Item("Customer_Name").ToString
                    .Rows(i).Cells("StatusValue").Value = objDT.Rows(i).Item("Status").ToString
                    .Rows(i).Cells("Activity").Value = objDT.Rows(i).Item("Activity").ToString
                    .Rows(i).Cells("Status").Value = objDT.Rows(i).Item("StatusDescription").ToString
                    .Rows(i).Cells("Add_By").Value = objDT.Rows(i).Item("Add_By").ToString
                    .Rows(i).Cells("Document_type").Value = objDT.Rows(i).Item("Description").ToString
                    .Rows(i).Cells("Col_Ref1").Value = objDT.Rows(i).Item("Ref_No1").ToString
                    .Rows(i).Cells("Col_Ref2").Value = objDT.Rows(i).Item("Ref_No2").ToString
                    .Rows(i).Cells("Col_Comment").Value = objDT.Rows(i).Item("Comment").ToString
                    .Rows(i).Cells("status_ID").Value = objDT.Rows(i).Item("Status").ToString
                    '.Rows(i).Cells("col_AssignJob_Index").Value = objDT.Rows(i).Item("AssignJob_Index").ToString
                    '.Rows(i).Cells("col_UserName").Value = objDT.Rows(i).Item("UserName").ToString
                    .Rows(i).Cells("col_DistributionCenter").Value = objDT.Rows(i).Item("DistributionCenter").ToString

                    Dim intStatus As Integer = objDT.Rows(i).Item("Status")
                    Select Case intStatus
                        Case -1
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.Pink
                        Case 2
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightGreen
                        Case Else

                    End Select

                    Dim intStatus_Fullfill As Integer = IIf(objDT.Rows(i).Item("Status_Fullfill").ToString = "", 0, objDT.Rows(i).Item("Status_Fullfill").ToString)
                    Select Case intStatus_Fullfill
                        Case 1
                            .Rows(i).Cells("col_Status_Fullfill").Value = "รอเติมสินค้า"
                            .Rows(i).Cells("col_Status_Fullfill").Style.BackColor = Color.Yellow
                        Case Else
                    End Select


                End With
            Next
            If grdTranferStatusView.Rows.Count > 0 Then
                grdTranferStatusView.Rows(0).Selected = False
            End If

            SetnumRows()
            Me.But_Cancel.Enabled = False
            Me.But_Summit.Enabled = False
            Me.btnAssignJob.Enabled = False

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Function SetDateTime(ByVal DTP As DateTimePicker) As String

        DTP.Format = DateTimePickerFormat.Custom
        DTP.CustomFormat = "MM/dd/yyyy"
        Dim strDate As String = DTP.Text.Trim
        DTP.Format = DateTimePickerFormat.Long

        Return strDate
    End Function

#Region " STATUS OF DOCUMENT "
    Private Sub getProcessStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New TransferStatusTransaction
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
#End Region
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmTransferStatusView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language

            'Insert
            'oFunction.Insert(Me)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdTranferStatusView)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me, 5)
            oFunction.SW_Language_Column(Me, Me.grdTranferStatusView, 5)

            Dim objCustomSetting As New config_CustomSetting
            Me._USE_ASSIGNEJOB_ITEM = objCustomSetting.getConfig_Key_USE("USE_ASSIGNEJOB_ITEM")

            ' Comprient.SelectedIndex = 0
            Me.cboRowPerPage.SelectedIndex = 0
            Me.GetDocumentType()
            Me.getProcessStatus()
            Me.getTransferStatusView()
            Me.getReportName(5)

            GetAssignJob()

            Me.getTransferStatusView()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cbPrint
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

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getTransferStatusView()
            btnSearch.Focus()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try


    End Sub

    Private Sub ToolStripLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

#Region "Edit,summit,add,exit,cancel,print"

    Private Sub But_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Edit.Click
        Try
            If Me.grdTranferStatusView.CurrentRow Is Nothing Then Exit Sub
            If Me.grdTranferStatusView.CurrentRow.Index < 0 Then Exit Sub

            Dim TransferStatus_Index As String = grdTranferStatusView.Rows(grdTranferStatusView.CurrentRow.Index).Cells("System_Index").Value.ToString

            Dim objCheck As New WMS_STD_OUTB_Datalayer.Cl_TransferReserv
            Dim tmpString As String = objCheck.CheckReserve(TransferStatus_Index)
            If tmpString <> "S" Then
                W_MSG_Information(tmpString)
                Me.getTransferStatusView()
                Exit Sub
            End If


            Dim frm As New frmAssetTransfer_V2(frmAssetTransfer_V2.enuOperation_Type.EDIT)
            frm.TransferStatus_Index = TransferStatus_Index
            ' load Document_Now ไปยังหัว
            frm.DocumentStatusValue = grdTranferStatusView.CurrentRow.Cells("StatusValue").Value
            'frm.txtTransferStatus_No.Text = Me.grdTranferStatusView.Rows(grdTranferStatusView.CurrentRow.Index).Cells("Document_No").Value
            frm.ShowDialog()

            Me.getTransferStatusView()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Sub But_Summit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Summit.Click
        If Me.grdTranferStatusView.CurrentRow Is Nothing Then Exit Sub

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        If W_MSG_Confirm("คุณต้องการยืนยันใบโอนสินค้าใช่หรือไม่") = Windows.Forms.DialogResult.No Then Exit Sub

        Try
            Dim objClassDB As New TransferStatusTransaction
            Dim strConfirm As String = ""
            If objClassDB.GetData_For_CheckStatus(Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value, "") <> 1 Then
                W_MSG_Information("ไม่สามารถยืนยันรายการได้")
                Me.getTransferStatusView()
                Exit Sub
            End If

            'insert Log
            Dim oAudit_Log As New Sy_Audit_Log
            oAudit_Log.Document_Index = Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value
            oAudit_Log.Document_No = Me.grdTranferStatusView.CurrentRow.Cells("Document_No").Value
            oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Confirm_Transfer)

            strConfirm = objClassDB.BalTran(Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value)
            objClassDB = Nothing
            If strConfirm = "PASS" Then
                W_MSG_Information("ยืนยันรายการเรียบร้อยแล้ว")
            Else
                W_MSG_Error("ยืนยันรายการผิดพลาด " & Chr(13) & strConfirm)
                Exit Sub
            End If


            grdTranferStatusView.Refresh()
            grdTranferStatusView.Rows.Clear()
            Me.getTransferStatusView()
        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        End Try

    End Sub

    Private Sub But_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Exit.Click
        Me.Close()
    End Sub

    Private Sub But_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Add.Click
        Try
            Dim frm As New frmAssetTransfer_V2(frmAssetTransfer_V2.enuOperation_Type.ADDNEW)
            frm.ShowDialog()
            Me.getTransferStatusView()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try


    End Sub


    Private Function ConfrimAdminPassword() As Boolean
        Try
            Select Case grdTranferStatusView.CurrentRow.Cells("StatusValue").Value.ToString
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
   

    Private Sub But_Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Cancel.Click
        If Me.grdTranferStatusView.CurrentRow Is Nothing Then Exit Sub

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        If grdTranferStatusView.Rows.Count <= 0 Then Exit Sub

        Try

            If ConfrimAdminPassword() = True Then

                If W_MSG_Confirm("คุณต้องการยกเลิกรายการเลขที่เอกสาร " & Me.grdTranferStatusView.CurrentRow.Cells("Document_No").Value & "   ใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then

                    Dim objDB As New TransferStatusTransaction

                    ''Check Status
                    'If objDB.GetData_For_CheckStatus(Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value, "") <> 1 Or objDB.GetData_For_CheckStatus(Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value, "") <> -2 Then
                    '    W_MSG_Information("ไม่สามารถยกเลิกรายการได้")
                    '    Me.getTransferStatusView()
                    '    Exit Sub
                    'End If

                    'insert Log
                    Dim oAudit_Log As New Sy_Audit_Log
                    oAudit_Log.Document_Index = Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value
                    oAudit_Log.Document_No = Me.grdTranferStatusView.CurrentRow.Cells("Document_No").Value
                    oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_Transfer)


                    Dim NewTransferStatus_Index As String = ""
                    NewTransferStatus_Index = Me.grdTranferStatusView.Rows(grdTranferStatusView.CurrentRow.Index).Cells("System_Index").Value

                    Dim strConfirm As String = objDB.Cancel_Tranfer(NewTransferStatus_Index)
                    objDB = Nothing

                    If strConfirm = "PASS" Then
                        W_MSG_Information("ยกเลิกรายการเรียบร้อยแล้ว")
                    Else
                        W_MSG_Error("ยืนยันรายการผิดพลาด " & Chr(13) & strConfirm)
                        Exit Sub
                    End If

                    ' *** Refresh data *** 
                    Me.getTransferStatusView()
                    Exit Sub

                End If

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub But_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_print.Click

        Try
            Dim oconfig_Report As New WMS_STD_Master.config_Report
            Dim Report_Name As String = Me.cbPrint.SelectedValue.ToString

            Try
                'TODO : Wait Report

                'Dim frm As New frmReportViewerMain
                ' frm.CrystalReportViewer.ReportSource = oconfig_Report.GetReportInfo("WTH_PrintOut", "And Withdraw_Index ='" & Me.lblWithdraw_Index.Text & "'")
                'If oconfig_Report.GetReportInfo(Report_Name, "And TransferStatus_Index ='" & Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value & "'").Rows.Count = 0 Then
                '    W_MSG_Information_ByIndex(300038)
                'Else
                'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And TransferStatus_Index ='" & Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value & "'")
                'frm.ShowDialog()
                'End If

                Dim frm As New WMS_STD_TMM_Report.frmReportViewerMain
                Dim oReport As New WMS_STD_TMM_Report.Loading_Report(Report_Name, "And TransferStatus_Index ='" & Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value & "'")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
                '###################################
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
#End Region

#Region "Visible"

    Private Sub Rad_CusName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rad_CusName.CheckedChanged
        Me.dtpDate.Visible = False
        Me.dateEnd.Visible = False
        Me.L_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub Rad_Ref1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Rad_Ref1.CheckedChanged
        Me.dtpDate.Visible = False
        Me.dateEnd.Visible = False
        Me.L_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub rdbMovement_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTransferStatus_Date.CheckedChanged
        Me.dtpDate.Visible = True
        Me.dateEnd.Visible = True
        Me.txtKeySearch.Visible = False
        Me.txtKeySearch.Text = ""
        Me.L_to.Visible = True

    End Sub

    Private Sub rdbMovement_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTransferStatus_No.CheckedChanged
        Me.dtpDate.Visible = False
        Me.dateEnd.Visible = False
        Me.L_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

#End Region

#Region "DataGrid ,set numrow"

    Private Sub grdTranferStatusView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTranferStatusView.CellDoubleClick
        Try
            But_Edit_Click(sender, e)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub grdTranferStatusView_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTranferStatusView.SelectionChanged

        If grdTranferStatusView.CurrentRow Is Nothing Then Exit Sub

        If grdTranferStatusView.CurrentRow.Selected = True Then

            Select Case grdTranferStatusView.CurrentRow.Cells("StatusValue").Value
                Case "-1", "2"
                    Me.But_Summit.Enabled = False
                    Me.But_Cancel.Enabled = False
                    Me.But_Edit.Enabled = True
                    Me.btnAssignJob.Enabled = False
                Case "1", "-2"
                    Me.But_Summit.Enabled = True
                    Me.But_Edit.Enabled = True
                    Me.But_Cancel.Enabled = True
                    Me.btnAssignJob.Enabled = True
            End Select
        End If

    End Sub

    Sub SetnumRows()
        Dim numRows As Integer = 0
        numRows = grdTranferStatusView.Rows.Count
        If numRows > 0 Then
            'lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
            lbCountRows.Text = "พบจำนวน " & CInt(Me.txtRowCount.Text) & " รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows.Text = "ไม่พบรายการ"
        End If
    End Sub

#End Region


    Private Sub GetDocumentType()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(5)
            objDT = objClassDB.DataTable


            Dim odr As DataRow
            odr = objDT.NewRow
            odr("Description") = "ไม่ระบุ"
            odr("DocumentType_Index") = "-1"

            objDT.Rows.InsertAt(odr, 0)

            With cboDocument_Type
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************
            ' cboDocument_Type.SelectedIndex = cboDocument_Type.Items.Count - 1
            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


    Private Sub txtKeySearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.getTransferStatusView()
            End If
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
            odtUser = oUserAssignJob.GetDataTable

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

            grdTranferStatusView.EndEdit()
            If grdTranferStatusView.RowCount = 0 Then
                Exit Sub
            End If

            ' Check Status (จะ AssignJob ได้เอกสารต้องมีสถานะ รอยืนยัน เท่านั้น)

            If grdTranferStatusView.CurrentRow.Cells("status_ID").Value <> 1 Then
                W_MSG_Information("รายการเลขที่เอกสาร" & grdTranferStatusView.CurrentRow.Cells("Document_No").Value & " มีสถานะที่ไม่สามารถสั่งงานได้ กรุณาตรวจสอบรายการใหม่")
                Exit Sub
            End If


            ' Innsert/Update Data To tb_AssignJob

            With oAssign
                .User_Index = sender.tag
                .Assign_Date = Now
                .DocumentPlan_No = Me.grdTranferStatusView.CurrentRow.Cells("Document_No").Value.ToString
                .DocumentPlan_Index = Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value.ToString
                .Plan_Process = 5
                .Priority = 3

                If Me.grdTranferStatusView.CurrentRow.Cells("col_AssignJob_Index").Value.ToString = "" Then
                    .AssignJob_Index = objDBIndex.getSys_Value("AssignJob_Index")
                    booStatusAssign = .InsertData()
                Else
                    .AssignJob_Index = Me.grdTranferStatusView.CurrentRow.Cells("col_AssignJob_Index").Value.ToString
                    booStatusAssign = .UpdateData()
                End If

            End With

            ' Check Status การ Assign
            If booStatusAssign = True Then
                W_MSG_Information("มอบหมายงานเรียบร้อยแล้ว")

            Else
                W_MSG_Information("ไม่สามารถมอบหมายงานได้ ระบบทำงานผิดพลาด")
            End If

            Me.getTransferStatusView()
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
            If grdTranferStatusView.Rows.Count = 0 Then
                Exit Sub
            End If

            grdTranferStatusView.EndEdit()
            For intRow As Integer = 0 To grdTranferStatusView.Rows.Count - 1
                oAssign.SetPriority(pPriority, Me.grdTranferStatusView.Rows(intRow).Cells("col_AssignJob_Index").Value.ToString)
            Next

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



    Private Sub frmAssetTransferView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransfer
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 5)
                    oFunction.SW_Language_Column(Me, Me.grdTranferStatusView, 5)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAssignJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignJob.Click
        Try
            If grdTranferStatusView.Rows.Count < 1 Then
                Exit Sub
            End If
            If Me._USE_ASSIGNEJOB_ITEM Then
                If grdTranferStatusView.CurrentRow.Cells("Status").Tag <> "2" Or grdTranferStatusView.CurrentRow.Cells("Status").Tag <> "-1" Then
                    Dim f As New frmAssignJobItem
                    f.Process_ID = 5
                    f.DocumentPlan_No = Me.grdTranferStatusView.CurrentRow.Cells("Document_No").Value
                    f.ShowDialog()
                End If
            Else
                If grdTranferStatusView.CurrentRow.Cells("Status").Tag <> "2" Or grdTranferStatusView.CurrentRow.Cells("Status").Tag <> "-1" Then
                    Dim frm As New frmPopup_AssignJob
                    frm.Process_ID = 5
                    frm.DocumentPlan_Index = Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value
                    frm.DocumentPlan_No = Me.grdTranferStatusView.CurrentRow.Cells("Document_No").Value
                    frm.ShowDialog()

                    Me.getTransferStatusView()

                End If
            End If




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Top Control ADD BY Pong 28/04/2015
    Private Sub btnPageFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageFirst.Click
        Try
            txtPageIndex.Text = 1
            Me.getTransferStatusView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            txtPageIndex.Text -= 1
            Me.getTransferStatusView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            txtPageIndex.Text += 1
            Me.getTransferStatusView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            txtPageIndex.Text = txtTotalPage.Text
            Me.getTransferStatusView()
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

    Private Sub btnTransfer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTransfer.Click
        Try
            Dim objTransfer As New clsTransfer
            If objTransfer.Auto_TransferQty() Then
                W_MSG_Information("เติมสินค้า pickface อัตโนมัติเรียบร้อยแล้ว")
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class