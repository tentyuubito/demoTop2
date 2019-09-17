Imports WMS_STD_OAW_Adjust_Datalayer
Imports WMS_STD_Formula
Imports System.Windows.Forms
Imports WMS_STD_OAW_Report
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language

Imports WMS_STD_Master
Imports WMS_STD_OAW_Adjust

Public Class frmAdjustView
    Private fLoad As Boolean = False
    Dim Adjust_Index As String = ""
    Private _USE_ASSIGNEJOB_ITEM As Boolean = False
#Region "Get DATA to DATAGRID VIEW "
    Private Sub getAdjustView()
        If fLoad = False Then Exit Sub
        Dim objClassDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""
        Try
            If ChkActive.Checked = True Then
                strWhere &= " AND Status in (1,2,4) "
            Else
                If Me.rdbAdjust_No.Checked = True Then
                    If Not Me.txtKeySearch.Text = "" Then
                        strWhere = " AND Adjust_No='" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "'"
                    End If
                ElseIf Me.rdbCusName.Checked = True Then
                    strWhere = " AND Customer_Name LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"

                ElseIf Me.rdbRef1.Checked = True Then
                    strWhere = " AND Ref_No1 LIKE '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%'"
                End If

                '==============================
                If Me.rdbAdjust_Date.Checked = True Then

                    If ChkActive.Checked = True And Me.cboDocumentStatus.SelectedValue = 0 Then
                        strWhere = " AND ( "
                    Else
                        strWhere = " AND "
                    End If

                    If dtpDate.Text.Trim = dateEnd.Text.Trim Then
                        strWhere &= " Adjust_Date between '" & Format(dtpDate.Value, "yyyy/MM/dd").ToString() + " 00:00:00'"
                        strWhere &= " AND '" & Format(dateEnd.Value, "yyyy/MM/dd").ToString() + "  23:59:59'"
                    Else
                        Dim StartDate As String = Format(dtpDate.Value, "yyyy/MM/dd").ToString() + " 00:00:00"
                        Dim EndtDate As String = Format(dateEnd.Value, "yyyy/MM/dd").ToString() + "  23:59:59"
                        strWhere &= "  Adjust_Date between '" & StartDate & "' AND  '" & EndtDate & "'"
                    End If

                End If

                Select Case Me.cboDocumentStatus.SelectedValue
                    Case 0
                    Case Else
                        strWhere &= " AND Status=" & Me.cboDocumentStatus.SelectedValue
                End Select

            End If

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()
            strWhere = strWhere.Replace("Customer_Index", "VIEW_AdjustView.Customer_Index")

            strWhere &= New clsUserByDC().GetDistributionCenterByUser()
            strWhere = strWhere.Replace("DistributionCenter_Index", "VIEW_AdjustView.DistributionCenter_Index")

            objClassDB.getAdjustView(strWhere)
            objDT = objClassDB.DataTable
            Me.grdAdjustView.DataSource = objDT
            CType(Me.grdAdjustView.DataSource, DataTable).AcceptChanges()
            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdAdjustView.Rows(i)
                    Select Case objDT.Rows(i).Item("status")
                        Case 3 'เสร็จสิ้น()
                            .Cells("Document_No").Style.BackColor = Color.LightGreen
                        Case 1 'รอยืนยันเอกสาร
                            .Cells("Document_No").Style.BackColor = Color.White
                        Case 2 'รอยืนยันการปรับยอด()
                            .Cells("Document_No").Style.BackColor = Color.Yellow
                        Case -1 'ยกเลิก
                            .Cells("Document_No").Style.BackColor = Color.Red ' System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
                        Case 4 'กำลังตรวจนับ()
                            .Cells("Document_No").Style.BackColor = Color.Yellow
                    End Select
                End With
            Next



            Dim numRows As Integer = 0
            numRows = Me.grdAdjustView.Rows.Count
            If numRows > 0 Then
                Me.grdAdjustView.Rows(0).Selected = False
                lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"

                'GetAssignJob()
            Else
                lbCountRows.Text = "ไม่พบรายการ"
            End If



        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
#End Region

#Region " STATUS OF DOCUMENT "

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report_Adjust '(enuOperation_Type.SEARCH)
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

    Private Sub getProcessStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
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

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmAdjust(frmAdjust.enuOperation_Type.ADDNEW)
            frm.ShowDialog()
            Me.getAdjustView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub frmAdjustView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            Me.grdAdjustView.AutoGenerateColumns = False

            oFunction.SwitchLanguage(Me, 4)
            oFunction.SW_Language_Column(Me, Me.grdAdjustView, 4)
            oFunction = Nothing

            'Dim objCustomSetting As New config_CustomSetting
            'Me._USE_ASSIGNEJOB_ITEM = objCustomSetting.getConfig_Key_USE("USE_ASSIGNEJOB_ITEM")
            'If Me._USE_ASSIGNEJOB_ITEM = False Then
            '    Me.btnAssignJob.Visible = False
            'End If

            Me.getProcessStatus()
            Me.getReportName(4)
            Me.btn_PrintAdjust_Act.Visible = False
            Me.btnAssignJob.Visible = False

            fLoad = True
            Me.getAdjustView()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try

            Dim frm As New frmAdjust(frmAdjust.enuOperation_Type.UPDATE, Me.grdAdjustView.CurrentRow.Cells("System_Index").Value)
            frm.ShowDialog()
            Me.getAdjustView()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub



    Private Sub rdbAdjust_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAdjust_Date.CheckedChanged
        Try
            Me.dtpDate.Visible = True
            Me.dateEnd.Visible = True
            Me.txtKeySearch.Visible = False
            Me.txtKeySearch.Text = ""
            Me.L_to.Visible = True
            ChkActive.Checked = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub rdbMovement_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAdjust_No.CheckedChanged
        Me.dtpDate.Visible = False
        Me.dateEnd.Visible = False
        Me.L_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        ChkActive.Checked = False
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getAdjustView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Private Sub cboDocumentStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentStatus.SelectedIndexChanged
    '    Try
    '        If cboDocumentStatus.SelectedValue Is Nothing Then Exit Sub

    '        Me.getAdjustView()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    'Private Sub btnPrintOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Dim frm As New frmReportViewer1_bk
    '        frm.Report_Id = 8
    '        frm.Document_Index = Me.grdAdjustView.CurrentRow.Cells("System_Index").Value
    '        frm.Show()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

    Private Sub But_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub But_print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim oconfig_Report As New config_Report_TNP
            Dim Report_Name As String = Me.cbPrint.SelectedValue.ToString
            Try
                If grdAdjustView.RowCount = 0 Then
                    Exit Sub
                End If
                Select Case Report_Name.ToUpper
                    Case "RPTCHECKSTOCK"
                        Dim strwhere As String = ""
                        Dim strAdjust_Index As String = ""

                        For i As Integer = 0 To grdAdjustView.RowCount - 1
                            If grdAdjustView.Rows(i).Cells("Col_chkSelect").Value = True Then
                                strAdjust_Index &= "'" & grdAdjustView.Rows(i).Cells("System_Index").Value & "',"
                            End If
                        Next
                        If strAdjust_Index = "" Then
                            strAdjust_Index &= "'" & grdAdjustView.CurrentRow.Cells("System_Index").Value & "',"
                            strAdjust_Index &= "''"
                            strwhere &= " Where Adjust_Index in (" & strAdjust_Index & ") and QTY_Diff<>0 "
                        Else

                            strAdjust_Index &= "''"
                            strwhere &= " Where Adjust_Index in (" & strAdjust_Index & ") and QTY_Diff<>0 "
                        End If

                        Dim objreport As New frmReportViewerMain
                        objreport.Report_Name = "rptCheckStock"
                        objreport.Document_Index = strwhere
                        objreport.ShowDialog()

                    Case Else


                        Dim frm As New frmReportViewerMain
                        frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo2(Report_Name, " And Adjust_Index ='" & grdAdjustView.CurrentRow.Cells("System_Index").Value & "'", " order by Location_Alias asc ")
                        frm.ShowDialog()

                End Select



            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Rad_CusName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCusName.CheckedChanged
        Me.dtpDate.Visible = False
        Me.dateEnd.Visible = False
        Me.L_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        ChkActive.Checked = False
    End Sub

    Private Sub Rad_Ref1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbRef1.CheckedChanged
        Me.dtpDate.Visible = False
        Me.dateEnd.Visible = False
        Me.L_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        ChkActive.Checked = False
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If Me.grdAdjustView.Rows.Count = 0 Then
            Exit Sub
        End If

        Dim objDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.SEARCH)
        Dim NewAdjustView_Index As String = ""
        objDB.NewAdjustView_Index = Me.grdAdjustView.Rows(grdAdjustView.CurrentRow.Index).Cells("System_Index").Value

        Dim intStatus As Integer = grdAdjustView.CurrentRow.Cells("col_Status_Id").Value
        Select Case intStatus
            Case 2, 3
                'Dim frmpassword As New PopupEnterPassword
                'frmpassword.ShowDialog()
                'If frmpassword.passwordistrue = False Then
                '    Exit Sub
                'End If
        End Select

        Try
            If MessageBox.Show("คุณต้องการยกเลิกรายการเลขที่เอกสาร " & Me.grdAdjustView.CurrentRow.Cells("Document_No").Value & "   ใช่หรือไม่ ", "ยืนยันการยกเลิกรายการ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                If objDB.Cancel_AdjustView(Me.grdAdjustView.CurrentRow.Cells("System_Index").Value) = True Then
                    MessageBox.Show("ยกเลิกรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    objDB = Nothing

                    Me.getAdjustView()
                    Exit Sub
                Else
                    MessageBox.Show("ไม่สามารถยกเลิกรายการได้ ระบบทำงานผิดพลาด", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    objDB = Nothing
                    Exit Sub
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnShowItemList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowItemList.Click
        If Me.grdAdjustView.Rows.Count = 0 Then
            Exit Sub
        End If
        Try
            'Dim frm As New frmCycle_Count(frmCycle_Count.enuOperation_Type.ADDNEW)
            Dim frm As New frmAdjust_Confirm
            ' frm._ForReadOnly = False
            frm.Adjust_Index = grdAdjustView.CurrentRow.Cells("System_Index").Value
            frm.ShowDialog()
            Me.getAdjustView()

            'If grdAdjustView.CurrentRow.Cells("Status").Tag = 2 Then
            '    Dim frm As New frmAdjust_Confirm_Update
            '    frm._ForReadOnly = False
            '    frm.Adjust_Index = grdAdjustView.CurrentRow.Cells("System_Index").Value
            '    frm.ShowDialog()
            '    Me.getAdjustView()
            'ElseIf grdAdjustView.CurrentRow.Cells("Status").Tag = 3 Then
            '    Dim frm As New frmAdjust_Confirm_Update
            '    frm._ForReadOnly = True
            '    frm.Adjust_Index = grdAdjustView.CurrentRow.Cells("System_Index").Value
            '    frm.ShowDialog()
            '    Me.getAdjustView()
            'Else
            '    W_MSG_Information("ยังทำการตรวจนับไม่เสร็จสิ้น")
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click

    '    If Me.grdAdjustView.CurrentRow Is Nothing Then Exit Sub
    '    If W_MSG_Confirm("คุณต้องการยืนยันใบตรวจสินค้าใช่หรือไม่") = Windows.Forms.DialogResult.No Then Exit Sub

    '    Try
    '        Dim objClassDB As New AdjustTransaction(AdjustTransaction.enuOperation_Type.CONFIRM_ADJUST)

    '        If objClassDB.SaveAdjust_Confirm(Me.grdAdjustView.CurrentRow.Cells("System_Index").Value) Then
    '            MessageBox.Show("บันทึกข้อมูลการปรับยอดเรียบร้อย", "บันทีกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '        Else
    '            MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้", "บันทีกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        End If

    '        grdAdjustView.Refresh()
    '        grdAdjustView.Rows.Clear()
    '        Me.getAdjustView()

    '    Catch ex As Exception
    '        W_MSG_Information(ex.Message.ToString)
    '    End Try
    'End Sub

    Private Sub grdAdjustView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAdjustView.CellDoubleClick
        Try
            Me.btnUpdate_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPop_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If rdbCusName.Checked = True Then
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

            oUserAssignJob.getUser_AssignJobAdjust()
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

            grdAdjustView.EndEdit()
            If grdAdjustView.RowCount = 0 Then
                Exit Sub
            End If

            ' Check Status (จะ AssignJob ได้เอกสารต้องมีสถานะ รอยืนยัน เท่านั้น)
            'For intRow As Integer = 0 To grdAdjustView.Rows.Count - 1
            'If grdAdjustView.Rows(intRow).Cells("chkSelect").Value = True Then
            booStatusChkSelect = True
            If grdAdjustView.CurrentRow.Cells("Status").Tag <> 1 Then
                W_MSG_Information("รายการเลขที่เอกสาร" & grdAdjustView.CurrentRow.Cells("Document_No").Value & " มีสถานะเสร็จสิ้นแล้ว กรุณาตรวจสอบรายการใหม่")
                Exit Sub
            End If
            ' End If
            'Next

            If booStatusChkSelect = False Then
                W_MSG_Information("กรุณาเลือกเอกสารที่ต้องการมอบหมาย")
                Exit Sub
            End If

            Dim obj_Adjust As New AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE)
            Dim Adjust_Index As String = Me.grdAdjustView.CurrentRow.Cells("System_Index").Value.ToString
            obj_Adjust.Update_Use(sender.tag, Adjust_Index)

            ' Innsert/Update Data To tb_AssignJob
            'For intRow As Integer = 0 To grdAdjustView.Rows.Count - 1
            '    If grdAdjustView.Rows(intRow).Cells("chkSelect").Value = True Then
            With oAssign
                .User_Index = sender.tag
                .Assign_Date = Now
                .DocumentPlan_No = Me.grdAdjustView.CurrentRow.Cells("Document_No").Value.ToString
                .DocumentPlan_Index = Me.grdAdjustView.CurrentRow.Cells("System_Index").Value.ToString
                .Plan_Process = 4
                .Priority = 3

                If Me.grdAdjustView.CurrentRow.Cells("col_AssignJob_Index").Value = Nothing Then
                    .AssignJob_Index = objDBIndex.getSys_Value("AssignJob_Index")
                    booStatusAssign = .InsertData()
                Else
                    .AssignJob_Index = Me.grdAdjustView.CurrentRow.Cells("col_AssignJob_Index").Value.ToString
                    booStatusAssign = .UpdateData()
                End If
            End With


            'End If
            'Next

            'update tb_Adjust

            ' Check Status การ Assign
            If booStatusAssign = True Then
                W_MSG_Information("มอบหมายงานเรียบร้อยแล้ว")

            Else
                W_MSG_Information("ไม่สามารถมอบหมายงานได้ ระบบทำงานผิดพลาด")
            End If

            Me.getAdjustView()
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
            If grdAdjustView.Rows.Count = 0 Then
                Exit Sub
            End If

            grdAdjustView.EndEdit()
            For intRow As Integer = 0 To grdAdjustView.Rows.Count - 1
                oAssign.SetPriority(pPriority, Me.grdAdjustView.Rows(intRow).Cells("col_AssignJob_Index").Value.ToString)
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


    'Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If Me.grdAdjustView.Rows.Count > 0 Then
    '        If MessageBox.Show("คุณต้องการยืนยันใบตรวจนับใช่หรือไม่", "ยืนยันใบตรวจนับ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.No = True Then
    '            Exit Sub
    '        End If
    '        ' *** Call Function for Withdraw balance ***
    '        Dim objClassDB As New AdjustTransaction_Update(AdjustTransaction_Update.enuOperation_Type.UPDATE)
    '        Try
    '            If objClassDB.Update_Adjust_Status(Me.grdAdjustView.CurrentRow.Cells("System_Index").Value, "2") = True Then
    '                MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            Else
    '                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)
    '            End If
    '            Me.getAdjustView()
    '        Catch ex As Exception
    '            W_MSG_Error(ex.Message.ToString)
    '        Finally
    '            objClassDB = Nothing

    '        End Try
    '    End If
    'End Sub


    Private Sub btn_PrintAdjust_Act_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_PrintAdjust_Act.Click
        Try
            'Dim strwhere As String = ""
            'Dim strAdjust_Index As String = ""

            'For i As Integer = 0 To grdAdjustView.RowCount - 1
            '    If grdAdjustView.Rows(i).Cells("Col_chkSelect").Value = True Then
            '        strAdjust_Index &= "'" & grdAdjustView.Rows(i).Cells("System_Index").Value & "',"
            '    End If
            'Next
            'If strAdjust_Index = "" Then
            '    W_MSG_Information("กรุณาเลือกรายการเพื่อพิมพ์ ")
            '    Exit Sub
            'End If

            'strAdjust_Index &= "''"
            'strwhere &= " Where Adjust_Index in (" & strAdjust_Index & ") and QTY_Diff<>0 "

            ''SetDEFAULT_USE_REPORTPRINTOUT_OAW()
            'Dim frm As New frmReportViewerMain
            '' Dim oconfig_Report As New WMS_STD_OAW_Report.config_Report

            'Dim oReport As New Loading_Report("rptCheckStock", strwhere)
            'frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            'frm.ShowDialog()

            Dim strwhere As String = ""
            Dim strAdjust_Index As String = ""

            For i As Integer = 0 To grdAdjustView.RowCount - 1
                If grdAdjustView.Rows(i).Cells("Col_chkSelect").Value = True Then
                    strAdjust_Index &= "'" & grdAdjustView.Rows(i).Cells("System_Index").Value & "',"
                End If
            Next
            If strAdjust_Index = "" Then
                W_MSG_Information("กรุณาเลือกรายการเพื่อพิมพ์ ")
                Exit Sub
            End If

            strAdjust_Index &= "''"
            strwhere &= " Where Adjust_Index in (" & strAdjust_Index & ") and QTY_Diff<>0 "

            Dim objreport As New frmReportViewerMain
            objreport.Report_Name = "rptCheckStock"
            objreport.Document_Index = strwhere
            objreport.ShowDialog()



        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub grdAdjustView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAdjustView.CellClick
        Try
            ManageBotton()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdAdjustView_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdAdjustView.SelectionChanged
        Try

            ManageBotton()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmAdjustView_Update_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigAdjust
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 4)
                    oFunction.SW_Language_Column(Me, Me.grdAdjustView, 4)
                    oFunction = Nothing
                End If
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub frmAdjustView_Update_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
    '    Me.getAdjustView()
    'End Sub

    Private Sub txtKeySearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch.KeyDown
        Try

            If e.KeyCode = Keys.Enter Then
                btnSearch_Click(sender, e)
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            If W_MSG_Confirm("ท่านต้องการยืนยันเอกสารเลขที่ " & Me.grdAdjustView.CurrentRow.Cells("Document_No").Value & " ใช่หรือไม่ ") = Windows.Forms.DialogResult.No Then Exit Sub

            Dim NewAdjustView_Index As String = Me.grdAdjustView.CurrentRow.Cells("System_Index").Value
            Dim oAdjust As New AdjustTransaction(AdjustTransaction.enuOperation_Type.UPDATE)
            oAdjust.Update_Adjust_Status(NewAdjustView_Index, 4)
            W_MSG_Information_ByIndex("1")
            Me.getAdjustView()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ManageBotton()
        Try
            Me.btnCancel.Enabled = False
            Me.btnAdd.Enabled = False
            Me.btnUpdate.Enabled = False
            Me.btnShowItemList.Enabled = False
            Me.btn_PrintAdjust_Act.Enabled = False
            Me.btnPrint.Enabled = False
            Me.btnConfirm.Enabled = False

            If grdAdjustView.RowCount = 0 Then Exit Sub
            If grdAdjustView.CurrentRow.Cells("col_Status_Id").Value Is Nothing Then Exit Sub
            Dim intStatus As Integer = grdAdjustView.CurrentRow.Cells("col_Status_Id").Value
            Select Case intStatus
                Case "-1"
                    Me.btnAdd.Enabled = True
                    Me.btnUpdate.Enabled = True
                Case "1"
                    Me.btnCancel.Enabled = True
                    Me.btnUpdate.Enabled = True
                    Me.btnAdd.Enabled = True
                    Me.btnConfirm.Enabled = True
                    Me.btn_PrintAdjust_Act.Enabled = True
                    Me.btnPrint.Enabled = True
                Case "2"
                    Me.btnUpdate.Enabled = True
                    Me.btnCancel.Enabled = True
                    Me.btnAdd.Enabled = True
                    Me.btnShowItemList.Enabled = True
                    Me.btn_PrintAdjust_Act.Enabled = True
                    Me.btnPrint.Enabled = True
                Case "3"
                    Me.btnUpdate.Enabled = True
                    Me.btnAdd.Enabled = True
                    Me.btnShowItemList.Enabled = True
                    Me.btnUpdate.Enabled = True
                    Me.btn_PrintAdjust_Act.Enabled = True
                    Me.btnPrint.Enabled = True
                Case "4"
                    Me.btnCancel.Enabled = True
                    Me.btnUpdate.Enabled = True
                    Me.btnAdd.Enabled = True
                    Me.btnShowItemList.Enabled = True
                    Me.btn_PrintAdjust_Act.Enabled = True
                    Me.btnPrint.Enabled = True
            End Select

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub chkall_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkall.CheckedChanged
        Try

            If grdAdjustView.RowCount > 0 Then
                For iRow As Integer = 0 To grdAdjustView.RowCount - 1
                    grdAdjustView.Rows(iRow).Cells("Col_chkSelect").Value = Me.chkall.Checked
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAssignJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignJob.Click

        Try
            If Me._USE_ASSIGNEJOB_ITEM Then
                If grdAdjustView.CurrentRow.Cells("Status").Tag <> "2" Or grdAdjustView.CurrentRow.Cells("Status").Tag <> "-1" Then
                    Dim f As New frmAssignJobItem
                    f.Process_ID = 5
                    f.DocumentPlan_No = Me.grdAdjustView.CurrentRow.Cells("Document_No").Value
                    f.ShowDialog()
                End If
            Else

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmAdjustView_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            Me.getAdjustView()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class