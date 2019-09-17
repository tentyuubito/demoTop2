'Dong_kk
Imports System.Drawing
Imports System.ComponentModel
Imports System
Imports System.Windows.Forms
Imports System.Configuration.ConfigurationSettings
Imports System.Configuration
Imports System.Threading
Imports System.Globalization

Imports WMS_STD_Master
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language

Public Class frmInterface_Interval

    Dim Last_Index As Integer = 0
    Dim xConDB As New WMS_STD_Formula.DBType_SQLServer
    Dim xDT As New DataTable
    Dim MSG As String = ""
    Dim Display_Loag As Integer = 0
    Public Status_Work As Boolean = False

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            If Me.BackgroundWorker1.CancellationPending = False Then
                Me.BackgroundWorker1.CancelAsync()
            End If
            Me.TimerProcess.Enabled = False
            Me.btnOK.Enabled = True

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub frmEndDay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New System.Configuration.AppSettingsReader()
            WV_ConnectionString = My.Settings.WMS_ConnectionString 'appSet.GetValue("ConnectionString", GetType(String))
            chkReturn.Enabled = ConfigurationManager.AppSettings("chkReturn")
            chkTransferIN.Enabled = ConfigurationManager.AppSettings("chkTransferIN")
            chkSalesOrder.Enabled = ConfigurationManager.AppSettings("chkSalesOrder")
            chkTransferOut.Enabled = ConfigurationManager.AppSettings("chkTransferOut")
            chkmsCus.Enabled = ConfigurationManager.AppSettings("chkmsCus")
            chkProduct.Enabled = ConfigurationManager.AppSettings("chkProduct")
            chkCreditNote.Enabled = ConfigurationManager.AppSettings("chkCreditNote")
            chkGoodsIssue.Enabled = ConfigurationManager.AppSettings("chkGoodsIssue")
            ChkSelectAllINF.Enabled = ConfigurationManager.AppSettings("ChkSelectAllINF")
            Display_Loag = ConfigurationManager.AppSettings("Display_Log").ToString
            Me.Text &= " Version :  " & Me.ProductVersion.ToString
            Me.txtMaxLog.Text = Display_Loag

            'Interval = 60000 = 1 minute   
            Me.TimerProcess.Interval = ConfigurationManager.AppSettings("Timer_Interval").ToString
            Dim countApp As Integer = ConfigurationManager.AppSettings.Count = 0
            Me.txtTime.Value = CDate(Now.ToString("dd/MM/yyyy HH:mm"))
            Me.dtDate.Value = Now
            'Last record
            xConDB = New WMS_STD_Formula.DBType_SQLServer
            Dim _Index As String = xConDB.DBExeQuery_Scalar("select ISNULL(Max(_Index),0) from KSL_SY_LOG_INTERFACE_ST")
            If String.IsNullOrEmpty(_Index) Then
                Last_Index = CInt(_Index)
            Else
                Last_Index = 0
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btn_OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Try
            Try
                Display_Loag = Me.txtMaxLog.Text
            Catch ex As Exception
                W_MSG_Error("กรุณาระบุตัวเลขมากกว่า 0")
                Me.txtMaxLog.Focus()
                Me.txtMaxLog.SelectAll()
                Exit Sub
            End Try

            Me.grbTransactionDate.Text = Me.grbTransactionDate.Text & " Dispaly : " & FormatNumber(Display_Loag, 0).ToString & " Rows"

            MSG = "END"
            Me.btnOK.Enabled = False
            Me.TimerProcess.Enabled = True
            Me.BackgroundWorker1.RunWorkerAsync()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Try

            Dim bgw As BackgroundWorker = TryCast(sender, BackgroundWorker)
            Application.CurrentCulture = New System.Globalization.CultureInfo("en-GB")

            Dim xDate As String
            xDate = Now.ToString("yyyyMMdd")


            Dim c As New Cutomer_Service
            Dim p As New Product_Service
            'Dim o As New SalseOrder_Service
            'Dim t_out As New SalseOrder_Service
            'Dim t_in As New Purchase_Service
            'Dim r As New Purchase_Service
            'Dim issue As New SalseOrder_Service
            'Dim cr As New SalseOrder_Service
            Dim oms_Service As New OMS_Interface
            Dim DOCNO As String = ""

            If Not IsNumeric(txtSreach.Text.Trim) Then
                DOCNO = txtDocNo.Text
            End If

            '      Dim wms_Service As New WMS_Interface
            '   wms_Service.ConfirmGI()


            If chkmsCus.Checked Then
                'c.WebService_Customer(0, 0, DOCNO, "") 'Check
                oms_Service.GetMaster(OMS_Interface.eOMSMasterID.Branch)
                '           W_MSG_Information("Import Customers Success.")
            End If
            If chkProduct.Checked Then
                'p.WebService_Product(0, 0, DOCNO, "") 'Check
                oms_Service.GetMaster(OMS_Interface.eOMSMasterID.Product)
                '         W_MSG_Information("Import Products Success.")
            End If
            'SO

            If chkSalesOrder.Checked Then
                '     o.WebService_Salesorder(DOCNO, "", "") 'Sales
                oms_Service.GetSalesOrder("CL")
                oms_Service.GetSalesOrder("SL")
                '       W_MSG_Information("Import SalesOrder Success.")
            End If

            'If chkTransferOut.Checked Then
            '    t_out.WebService_TransferOut(DOCNO, "", "")
            'End If
            'If chkGoodsIssue.Checked Then
            '    issue.WebService_GoodsIssue(DOCNO, "", "")
            'End If
            'If chkCreditNote.Checked Then
            '    cr.WebService_CreditNote(DOCNO, "", "")
            'End If
            ''PO
            'If chkTransferIN.Checked Then
            '    t_in.WebService_TransferIn(DOCNO, "", "")
            'End If
            'If chkReturn.Checked Then
            '    r.WebService_Return(DOCNO, "", "")
            'End If
            'txtDocNo.Clear()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        Try
            If e.Error Is Nothing Then
                Status_Work = False
                MSG = "END"
            Else
                MsgBox(String.Format("Work fail with error: {0}", e.Error.Message))
                MSG = "END"
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub frmReCalculate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

        Try
            If Not W_MSG_Confirm("ต้องการให้โปรแกรมทำงานเบื้องหลัง ใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
                If Me.BackgroundWorker1.CancellationPending = False Then
                    Me.BackgroundWorker1.CancelAsync()
                    MSG = "END"
                End If
                e.Cancel = False
            Else
                Me.Visible = False
                NotifyIcon1.Visible = True
                NotifyIcon1.ShowBalloonTip(1, "NotifyIcon", "Running Minimized", ToolTipIcon.Info)
                e.Cancel = True
            End If
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try


    End Sub

    Private Sub ClearProcessedCol()
        For iRow As Integer = 0 To grdInterval.RowCount - 1
            grdInterval.Rows(iRow).Cells("Col_Done").Value = ""
        Next
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TimerProcess.Tick
        Try
            Dim boolCheckDay As Boolean = False
            'If ChkAutoProcess.Checked = True Then
            If Me.chkSunday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Sunday Then
                    boolCheckDay = True
                End If
            End If

            If Me.chkMonday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Monday Then
                    boolCheckDay = True
                End If
            End If

            If Me.chkTuesday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Tuesday Then
                    boolCheckDay = True
                End If
            End If
            If Me.chkWednesday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Wednesday Then
                    boolCheckDay = True
                End If
            End If
            If Me.chkThurseday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Thursday Then
                    boolCheckDay = True
                End If
            End If
            If Me.chkFriday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Friday Then
                    boolCheckDay = True
                End If
            End If
            If Me.chkSaturday.Checked Then
                If Now.DayOfWeek = DayOfWeek.Saturday Then
                    boolCheckDay = True
                End If
            End If

            If CDate(txtTime.Value).ToString("HH:mm") < CDate(Now).ToString("HH:mm") Then
                Me.txtTime.Value = Now
            End If

            If boolCheckDay Then
                If CDate(txtTime.Value).ToString("HH:mm") = CDate(Now).ToString("HH:mm") Then
                    If CDate(txtTime.Value).ToString("HH:mm") = CDate(Now).ToString("HH:mm") Then
                        'Recal

                        Me.grdInterval.AutoGenerateColumns = False
                        Me.grdInterval.DataSource = Nothing
                        Me.txtTime.Value = CDate(Me.txtTime.Value).AddMinutes(CInt(Me.txtInterval.Text))
                        MSG = ""
                        Me.btnOK.Enabled = False

                        If Status_Work = False Then
                            Status_Work = True
                            Me.BackgroundWorker1.RunWorkerAsync()
                        End If

                    End If
                End If

                If Not MSG = "END" Then
                    'update grid status
                    'xDT = SreachFor_GRD(" and _Index >= '" & Last_Index & "' or   add_date >= FORMAT(GETDATE(), 'yyyy-MM-dd')")
                    xDT = SreachFor_GRD(" and _Index >= '" & Last_Index & "' ")
                    If xDT.Rows.Count > 0 Then
                        'Last_Index = xDT.Rows(xDT.Rows.Count - 1)("_Index")
                        Last_Index = xDT.Rows(0)("_Index")
                    End If
                End If
            End If

            'End If
            'End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtInterval_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtInterval.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtInterval, e.KeyChar)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim c As New Cutomer_Service
            Dim Enddate As Date = Now.AddDays(625).ToString("yyyy-MM-dd")
            Dim Begindate As Date = "2015-01-01"
            Dim i As Integer = 0
            While Begindate <= Enddate
                c.WebService_Customer(0, 0, "", Begindate.ToString("yyyyMMdd"))
                Begindate = Begindate.AddDays(1)
                i += 1
            End While
            MsgBox(i)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub NotifyIcon1_MouseClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles NotifyIcon1.MouseClick
        Try
            Me.Visible = True
            Me.WindowState = FormWindowState.Normal
            NotifyIcon1.Visible = False
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

    Private Sub btnClosed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

    Private Function SreachFor_GRD(ByVal strwhere As String) As DataTable
        xDT = xConDB.DBExeQuery("select top " & txtMaxLog.Text.Trim & " * from KSL_SY_LOG_INTERFACE_ST where 1=1 " & strwhere & " order by _index desc")
        Me.grdInterval.DataSource = xDT
        For i As Integer = 0 To xDT.Rows.Count - 1
            If Not xDT.Rows(i).Item("Status").ToString = "S" Then
                Me.grdInterval.Rows(i).Cells("Col_Status").Style.BackColor = Color.Red
            End If
        Next
        grdInterval.AutoGenerateColumns = False
        Return xDT
    End Function

    Private Sub btnSreach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSreach.Click
        Try
            Dim strWhere As String = ""
            Dim Status_Save As String = ""
            If txtDocNo.Text.Length > 0 Then
                strWhere &= " and Type_Id like '%" & txtDocNo.Text.Trim & "%' "
            End If
            If txtSreach.Text.Length > 0 Then
                strWhere &= " and Description like '%" & txtSreach.Text.Trim & "%' "
            End If
            If dtDate.Checked Then
                strWhere &= " and add_date >= '" & CDate(dtDate.Text.Trim).ToString("yyyy-MM-dd") & "' "
            End If
            If ChkS.Checked And ChkE.Checked Then
                strWhere &= " and Status in ('S','E') "
            Else
                If ChkS.Checked Then
                    Status_Save = "'S'"
                    strWhere &= " and Status ='S' "
                End If
                If ChkE.Checked Then
                    Status_Save &= "'E'"
                    strWhere &= " and Status = 'E' "
                End If
            End If
            grdInterval.AutoGenerateColumns = False
            grdInterval.DataSource = SreachFor_GRD(strWhere)
            Me.grbTransactionDate.Text = "Interface Log"
            Me.grbTransactionDate.Text = Me.grbTransactionDate.Text & " Dispaly : " & FormatNumber(Display_Loag, 0).ToString & " Rows"
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If (Me.grdInterval.Rows.Count = 0) Then
                W_MSG_Information(String.Format("ไม่พบข้อมูล"))
                Exit Sub
            End If
            Dim ds As New DataSet

            Dim objExport As New cls_Export_Excel
            ds.Tables.Add(objExport.DataGridViewToDataTable(Me.grdInterval))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm")
            objExport.export(ds, "Daily Processing Management")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            'Dim WebService As New WebReference.WebServiceWMS
            'Dim xDateBegin As New Date
            'Dim xDate, str As String
            'Dim dsXML As New DataSet
            'xDateBegin = Now.AddYears(-1).ToShortDateString
            'While xDateBegin < Now.ToShortDateString
            '    xDate = xDateBegin.ToString("yyyyMMdd")
            '    str = WebService.Get_GoodsIssue("", "", xDate)
            '    'Json convert to dataset           
            '    dsXML = Newtonsoft.Json.JsonConvert.DeserializeObject(Of DataSet)(str.ToString)
            '    If dsXML.Tables(0).Rows.Count > 0 Then
            '        W_MSG_Information("เจอแล้ว " & xDateBegin)
            '        Exit While
            '    End If
            '    xDateBegin = xDateBegin.AddDays(1)
            'End While
            'W_MSG_Information("ไม่เจอ")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ChkSelectAllDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSelectAllDay.CheckedChanged
        Try
            chkMonday.Checked = ChkSelectAllDay.Checked
            chkTuesday.Checked = ChkSelectAllDay.Checked
            chkWednesday.Checked = ChkSelectAllDay.Checked
            chkThurseday.Checked = ChkSelectAllDay.Checked
            chkFriday.Checked = ChkSelectAllDay.Checked
            chkSaturday.Checked = ChkSelectAllDay.Checked
            chkSunday.Checked = ChkSelectAllDay.Checked

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ChkSelectAllINF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkSelectAllINF.CheckedChanged
        Try

            chkReturn.Checked = ChkSelectAllINF.Checked
            chkTransferIN.Checked = ChkSelectAllINF.Checked
            chkSalesOrder.Checked = ChkSelectAllINF.Checked
            chkTransferOut.Checked = ChkSelectAllINF.Checked
            chkmsCus.Checked = ChkSelectAllINF.Checked
            chkProduct.Checked = ChkSelectAllINF.Checked
            chkCreditNote.Checked = ChkSelectAllINF.Checked
            chkGoodsIssue.Checked = ChkSelectAllINF.Checked


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim frm As New frmInterface_log
        frm.ShowDialog()
    End Sub

    Private Sub btnUpdateSOTYPE_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateSOTYPE.Click
        Dim Import As New clsImport
        Import.UpdateSOType()
        MsgBox("Update SO Type เสร็จสิ้น")
    End Sub
End Class