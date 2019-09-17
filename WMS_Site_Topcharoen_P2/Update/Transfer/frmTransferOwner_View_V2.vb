Imports WMS_STD_Formula
Imports System.Windows.Forms
Imports WMS_STD_OUTB_Reserv
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_TMM_Report
Public Class frmTransferOwner_View_V2

    Dim objHeader As New tb_TransferStatus
    Dim TransferStatus_Index As String = ""
    Private gGroup_Index As String = "0010000000011"
    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 1

#Region "Get DATA to DATAGRID VIEW "
    Private Sub getTransferOwnerView()
        Dim objClassDB As New TransferOwnerTransaction
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""

        Try

            ' *** WHERE STRING *** 
            If Me.rdbTransferStatus_Date.Checked = True Then

                Dim StartDate As String = Format(dtpDate.Value, "yyyy/MM/dd").ToString()
                Dim EndtDate As String = Format(dateEnd.Value, "yyyy/MM/dd").ToString()
                strWhere = "  AND TransferOwner_Date between '" & StartDate & "' AND  '" & EndtDate & "'"

            ElseIf Me.rdbTransferStatus_No.Checked = True Then
                If Not Me.txtKeySearch.Text = "" Then
                    strWhere = " AND TransferOwner_No='" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "'"
                End If
            ElseIf Me.Rad_CusName.Checked = True Then
                strWhere = " AND Customer_Name LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
            ElseIf Me.Rad_Ref1.Checked = True Then
                strWhere = " AND Ref1 LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
            End If

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()

            'ADD BY Pong 28/04/2015
            Me.cboRowPerPage.SelectedIndex = IIf(Me.cboRowPerPage.SelectedIndex > 0, Me.cboRowPerPage.SelectedIndex, 0)
            If rdTop.Checked Then
                Me.txtTop.Text = IIf(IsNumeric(Me.txtTop.Text), Me.txtTop.Text, 1)
                Me.txtTop.Text = IIf(CInt(Me.txtTop.Text) > 0, CInt(Me.txtTop.Text), 1)
                objClassDB.getTransferOwnerView(strWhere, 1, CInt(Me.txtTop.Text))
            ElseIf rdRowPage.Checked Then
                Me.txtPageIndex.Text = IIf(IsNumeric(Me.txtPageIndex.Text), Me.txtPageIndex.Text, 1)
                Me.txtPageIndex.Text = IIf(CInt(Me.txtPageIndex.Text) > 0, CInt(Me.txtPageIndex.Text), 1)
                objClassDB.getTransferOwnerView(strWhere, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)) - CInt(Me.cboRowPerPage.Text) + 1, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)))
            Else
                objClassDB.getTransferOwnerView(strWhere, -1, 0)
            End If
            Calculate_Paging()

            objDT = objClassDB.GetDataTable

            If objDT.Rows.Count > 0 Then
                Me.txtRowCount.Text = objDT.Rows(0)("ROW_COUNT").ToString
            Else
                Me.txtRowCount.Text = 0
            End If


            grdTranferOwnerView.DataSource = objDT


            'For i As Integer = 0 To objDT.Rows.Count - 1
            For i As Integer = 0 To grdTranferOwnerView.RowCount - 1
                With Me.grdTranferOwnerView
                    Dim intStatus As Integer = .Rows(i).Cells("status_ID").Value
                    Select Case intStatus
                        Case -1
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.Pink
                        Case 2
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightGreen
                        Case Else
                    End Select
                End With
            Next
            If grdTranferOwnerView.Rows.Count > 0 Then
                grdTranferOwnerView.Rows(0).Selected = False
            End If

            SetnumRows()
            Me.But_Summit.Enabled = False
            Me.But_Cancel.Enabled = False
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    Sub setColor()
        Try
            For i As Integer = 0 To grdTranferOwnerView.RowCount - 1
                With Me.grdTranferOwnerView
                    Dim intStatus As Integer = .Rows(i).Cells("status_ID").Value
                    Select Case intStatus
                        Case -1
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.Pink
                        Case 2
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightGreen
                        Case Else
                    End Select
                End With
            Next
        Catch ex As Exception
            Throw ex
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
            grdTranferOwnerView.AutoGenerateColumns = False

            oFunction.SwitchLanguage(Me, 50)
            oFunction.SW_Language_Column(Me, Me.grdTranferOwnerView, 50)

            ' Comprient.SelectedIndex = 0
            Me.cboRowPerPage.SelectedIndex = 0
            GetDocumentType()
            Me.getProcessStatus()
            Me.getTransferOwnerView()
            Me.getReportName(50)

            GetAssignJob()
            Me.getTransferOwnerView()
            grdTranferOwnerView.Sort(grdTranferOwnerView.Columns("Document_Date"), System.ComponentModel.ListSortDirection.Ascending)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New WMS_STD_Master.config_Report '(enuOperation_Type.SEARCH)
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
            getTransferOwnerView()
            btnSearch.Focus()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ToolStripLabel1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

#Region "Edit,summit,add,exit,cancel,print"

    Private Sub But_Edit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Edit.Click
        Try
            If Me.grdTranferOwnerView.CurrentRow Is Nothing Then Exit Sub

            If Me.grdTranferOwnerView.CurrentRow.Index < 0 Then Exit Sub


            Dim TransferOwner_Index As String = grdTranferOwnerView.Rows(grdTranferOwnerView.CurrentRow.Index).Cells("System_Index").Value.ToString

            Dim objCheck As New WMS_STD_OUTB_Datalayer.Cl_TransferReserv
            Dim tmpString As String = objCheck.CheckReserve_Owner(TransferOwner_Index)
            If tmpString <> "S" Then
                W_MSG_Information(tmpString)
                Me.getTransferOwnerView()
                Exit Sub
            End If

            Dim frm As New frmTransferOwner_V2(frmTransferOwner_V2.enuOperation_Type.EDIT)
            frm.TransferOwner_Index = TransferOwner_Index
            ' load Document_Now ��ѧ���
            frm.DocumentStatusValue = grdTranferOwnerView.CurrentRow.Cells("StatusValue").Value
            frm.txtTransferStatus_No.Text = Me.grdTranferOwnerView.Rows(grdTranferOwnerView.CurrentRow.Index).Cells("Document_No").Value
            frm.ShowDialog()

            getTransferOwnerView()
        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        End Try
    End Sub

    Private Sub But_Summit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Summit.Click
        If Me.grdTranferOwnerView.CurrentRow Is Nothing Then Exit Sub

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        If W_MSG_Confirm("�س��ͧ����׹�ѹ��͹�Թ������������") = Windows.Forms.DialogResult.No Then Exit Sub

        Try
            Dim objClassDB As New TransferOwnerTransaction
            Dim NewTransferStatus_Index As String = ""
            If grdTranferOwnerView.CurrentRow.Cells("StatusValue").Value <> -1 And grdTranferOwnerView.CurrentRow.Cells("StatusValue").Value <> 2 Then
                Dim strError As String = ""
                strError = objClassDB.TranOwner(Me.grdTranferOwnerView.CurrentRow.Cells("System_Index").Value)
                If "TRUE" = strError Then
                    W_MSG_Information("�׹�ѹ��¡�����º����")
                Else
                    W_MSG_Information(strError)
                End If
            Else
                W_MSG_Information("ʶҹ��͡����������ö�׹�ѹ��")
            End If


            objClassDB = Nothing

            grdTranferOwnerView.Refresh()
            'grdTranferOwnerView.Rows.Clear()
            getTransferOwnerView()
        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        End Try

    End Sub

    Private Sub But_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Exit.Click
        Me.Close()
    End Sub

    Private Sub But_Add_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_Add.Click
        Try
            Dim frm As New frmTransferOwner_V2(frmTransferOwner_V2.enuOperation_Type.ADDNEW)
            frm.ShowDialog()
            getTransferOwnerView()
        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        End Try
    End Sub

    Private Function ConfrimAdminPassword() As Boolean
        Try
            Select Case grdTranferOwnerView.CurrentRow.Cells("status_ID").Value.ToString
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
        If Me.grdTranferOwnerView.CurrentRow Is Nothing Then Exit Sub

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If

        If grdTranferOwnerView.Rows.Count <= 0 Then Exit Sub

        Try


            If ConfrimAdminPassword() = True Then
                If W_MSG_Confirm("�س��ͧ���¡��ԡ��¡���Ţ����͡��� " & Me.grdTranferOwnerView.CurrentRow.Cells("Document_No").Value & "   ��������� ") = Windows.Forms.DialogResult.Yes Then
                    Dim objDB As New TransferOwnerTransaction
                    If objDB.Cancel_Tranfer(Me.grdTranferOwnerView.CurrentRow.Cells("System_Index").Value) = True Then
                        W_MSG_Information("¡��ԡ��¡�����º��������")
                        objDB = Nothing
                        ' *** Refresh data *** 
                        getTransferOwnerView()
                        Exit Sub
                    Else
                        W_MSG_Information("�������ö¡��ԡ��¡���� �к��ӧҹ�Դ��Ҵ")
                        objDB = Nothing
                        Exit Sub
                    End If
                    objDB = Nothing
                End If
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub But_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles But_print.Click

        Try
            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cbPrint.SelectedValue.ToString

            Try
                'TODO : Wait Report
                'Dim frm As New frmReportViewerMain
                ' frm.CrystalReportViewer.ReportSource = oconfig_Report.GetReportInfo("WTH_PrintOut", "And Withdraw_Index ='" & Me.lblWithdraw_Index.Text & "'")
                'If oconfig_Report.GetReportInfo(Report_Name, "And TransferStatus_Index ='" & Me.grdTranferStatusView.CurrentRow.Cells("System_Index").Value & "'").Rows.Count = 0 Then
                '    W_MSG_Information_ByIndex(300038)
                'Else
                'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And TransferStatus_Index ='" & Me.grdTranferOwnerView.CurrentRow.Cells("System_Index").Value & "'")
                'frm.ShowDialog()
                'End If

                Dim frm As New frmReportViewerMain
                Dim oReport As New WMS_STD_TMM_Report.Loading_Report(Report_Name, "And TransferStatus_Index ='" & Me.grdTranferOwnerView.CurrentRow.Cells("System_Index").Value & "'")
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

    Private Sub grdTranferStatusView_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTranferOwnerView.CellDoubleClick
        But_Edit_Click(sender, e)
    End Sub

    Private Sub grdTranferStatusView_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTranferOwnerView.SelectionChanged

        If grdTranferOwnerView.CurrentRow Is Nothing Then Exit Sub

        If grdTranferOwnerView.CurrentRow.Selected = True Then

            Select Case grdTranferOwnerView.CurrentRow.Cells("StatusValue").Value
                Case "-1", "2"
                    Me.But_Summit.Enabled = False
                    Me.But_Cancel.Enabled = False
                    Me.But_Edit.Enabled = True
                Case "1"
                    Me.But_Summit.Enabled = True
                    Me.But_Edit.Enabled = True
                    Me.But_Cancel.Enabled = True
            End Select
        End If

    End Sub

    Sub SetnumRows()
        Dim numRows As Integer = 0
        numRows = grdTranferOwnerView.Rows.Count
        If numRows > 0 Then
            'lbCountRows.Text = "���ӹǹ " & numRows & " ��¡�� �ʴ� " & numRows & " ��¡��"
            lbCountRows.Text = "���ӹǹ " & CInt(Me.txtRowCount.Text) & " ��¡�� �ʴ� " & numRows & " ��¡��"
        Else
            lbCountRows.Text = "��辺��¡��"
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
            odr("Description") = "����к�"
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
                getTransferOwnerView()
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
    '''     - Select User  (������ User ����� Customer,Supplier )
    '''     - Sum �ӹǹ�͡��âͧ���� User �����੾���͡�����ԡ�����ʶҹ���ҡѺ '���׹�ѹ')
    ''' 
    ''' Update Date : 31/08/2010
    ''' Update By : Krit
    ''' ��������ͧ�ͺ���§ҹ���ء����������к� ���ͧ config DB � se_User.User_Index = '0010000000001' ��ҹ��
    ''' </remarks>
    ''' 
    Private Sub GetAssignJob()
        Try
            Dim oUserAssignJob As New tb_AssignJob
            Dim odtUser As DataTable

            oUserAssignJob.getUser_AssignJobWithDraw()
            odtUser = oUserAssignJob.GetDataTable

            Me.AssignToToolStripMenuItem.DropDownItems.Clear()

            'Menu �ͺ���§ҹ���ء��
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

            'Menu ¡��ԡ
            Dim oMenuItem_Cancel As New Windows.Forms.ToolStripMenuItem
            Dim oToolStripSeparator As New Windows.Forms.ToolStripSeparator
            oMenuItem_Cancel.Text = "¡��ԡ"
            oMenuItem_Cancel.Tag = -1

            Me.AssignToToolStripMenuItem.DropDownItems.Add(oMenuItem_Cancel)
            Me.AssignToToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {oToolStripSeparator})

            AddHandler oMenuItem_Cancel.Click, AddressOf AssignTo_Click
            '=====================================================

            'Menu Assign �ҹ������Ф�
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
    '''     - Check Status (�� AssignJob ���͡��õ�ͧ��ʶҹ� ���׹�ѹ ��ҹ��)
    '''     - Innsert/Update Data To tb_AssignJob   
    ''' </remarks>
    Private Sub AssignTo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim oAssign As New tb_AssignJob
            Dim objDBIndex As New Sy_AutoNumber
            Dim booStatusAssign As Boolean = False
            Dim booStatusChkSelect As Boolean = False

            grdTranferOwnerView.EndEdit()
            If grdTranferOwnerView.RowCount = 0 Then
                Exit Sub
            End If

            ' Check Status (�� AssignJob ���͡��õ�ͧ��ʶҹ� ���׹�ѹ ��ҹ��)

            If grdTranferOwnerView.CurrentRow.Cells("status_ID").Value <> 1 Then
                W_MSG_Information("��¡���Ţ����͡���" & grdTranferOwnerView.CurrentRow.Cells("Document_No").Value & " ��ʶҹз���������ö��觧ҹ�� ��سҵ�Ǩ�ͺ��¡������")
                Exit Sub
            End If


            ' Innsert/Update Data To tb_AssignJob

            With oAssign
                .User_Index = sender.tag
                .Assign_Date = Now
                .DocumentPlan_No = Me.grdTranferOwnerView.CurrentRow.Cells("Document_No").Value.ToString
                .DocumentPlan_Index = Me.grdTranferOwnerView.CurrentRow.Cells("System_Index").Value.ToString
                .Plan_Process = 5
                .Priority = 3

                If Me.grdTranferOwnerView.CurrentRow.Cells("col_AssignJob_Index").Value.ToString = "" Then
                    .AssignJob_Index = objDBIndex.getSys_Value("AssignJob_Index")
                    booStatusAssign = .InsertData()
                Else
                    .AssignJob_Index = Me.grdTranferOwnerView.CurrentRow.Cells("col_AssignJob_Index").Value.ToString
                    booStatusAssign = .UpdateData()
                End If

            End With

            ' Check Status ��� Assign
            If booStatusAssign = True Then
                W_MSG_Information("�ͺ���§ҹ���º��������")

            Else
                W_MSG_Information("�������ö�ͺ���§ҹ�� �к��ӧҹ�Դ��Ҵ")
            End If

            getTransferOwnerView()
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
    '''        1 = ��觴�ǹ�ҡ
    '''        2 = ��觴�ǹ
    '''        3 = ����
    '''        4 = �����觴�ǹ
    '''        5 = �ЧѺ
    ''' </remarks>

    Private Sub SetPriority(ByVal pPriority As Integer)

        Dim oAssign As New tb_AssignJob
        Try
            If grdTranferOwnerView.Rows.Count = 0 Then
                Exit Sub
            End If

            grdTranferOwnerView.EndEdit()
            For intRow As Integer = 0 To grdTranferOwnerView.Rows.Count - 1
                oAssign.SetPriority(pPriority, Me.grdTranferOwnerView.Rows(intRow).Cells("col_AssignJob_Index").Value.ToString)
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

    Private Sub grdTranferOwnerView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTranferOwnerView.CellClick
        Try
            Select Case grdTranferOwnerView.CurrentRow.Cells("status_ID").Value.ToString
                Case "-1"
                    But_Cancel.Enabled = False
                    But_Summit.Enabled = False
                Case "2"
                    But_Cancel.Enabled = False
                    But_Summit.Enabled = False
                Case Else
                    But_Cancel.Enabled = True
                    But_Summit.Enabled = True

            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdTranferOwnerView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdTranferOwnerView.Sorted
        Try
            setColor()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub frmTransferOwner_View_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransferOwner
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 50)
                    oFunction.SW_Language_Column(Me, Me.grdTranferOwnerView, 50)
                    oFunction = Nothing
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
            Me.getTransferOwnerView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            txtPageIndex.Text -= 1
            Me.getTransferOwnerView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            txtPageIndex.Text += 1
            Me.getTransferOwnerView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            txtPageIndex.Text = txtTotalPage.Text
            Me.getTransferOwnerView()
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

End Class