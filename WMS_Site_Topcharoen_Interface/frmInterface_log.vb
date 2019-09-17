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

Public Class frmInterface_log
    Dim Last_Index As Integer = 0
    Dim xConDB As New WMS_STD_Formula.DBType_SQLServer
    Dim xDT As New DataTable
    Dim MSG As String = ""
    Dim Display_Loag As Integer = 0
    Public Status_Work As Boolean = False

    'Public Enum enuOperation_Type
    '    SAVING
    '    ENDING
    '    NULL
    'End Enum

    'Public Sub New(ByVal Operation_Type As enuOperation_Type)
    '    MyBase.New()
    '    'This call is required by the Windows Form Designer.
    '    InitializeComponent()
    '    Status_Save = Operation_Type
    'End Sub



    Private Sub frmEndDay_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New System.Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            Display_Loag = ConfigurationManager.AppSettings("Display_Log").ToString
            Me.Text &= " Version :  " & Me.ProductVersion.ToString
            Me.txtMaxLog.Text = Display_Loag

            Me.dtDate.Value = Now
            'Interval = 60000 = 1 minute   
            'Me.TimerProcess.Interval = ConfigurationManager.AppSettings("Timer_Interval").ToString
            Dim countApp As Integer = ConfigurationManager.AppSettings.Count = 0
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

    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs)
        Try

            Dim bgw As BackgroundWorker = TryCast(sender, BackgroundWorker)
            Application.CurrentCulture = New System.Globalization.CultureInfo("en-GB")

            'Call ProgressBar
            'Me.ShowProgress(((intCuntTrDate / lstTransactionDate.Count) * 100) + 5, bgw, e)

            'Dim c As New Cutomer_Service
            'Dim xMSG As String = c.WebService_Customer(0, 0, "", "20170601")
            'If xMSG = "S" Then
            '    W_MSG_Information("Success !!!")
            'Else
            '    W_MSG_Error(xMSG)
            'End If

            'Dim p As New Product_Service
            'Dim xMSG As String = p.WebService_Product(0, 0, "", "20170601")
            'If xMSG = "S" Then
            '    BackgroundWorker1.ReportProgress(100)
            '    MSG = "END"
            'Else
            '    BackgroundWorker1.CancelAsync()
            '    MSG = "END"
            'End If

            Dim xDate As String
            xDate = Now.ToString("yyyyMMdd")
            'xDate = "20170908"

            'Dim c As New Cutomer_Service
            'Dim p As New Product_Service
            'Dim o As New SalseOrder_Service
            'Dim t_out As New SalseOrder_Service
            'Dim t_in As New Purchase_Service
            'Dim r As New Purchase_Service
            'Dim issue As New SalseOrder_Service
            'Dim cr As New SalseOrder_Service
            Dim DOCNO As String = ""
            If Not IsNumeric(txtSreach.Text.Trim) Then
                DOCNO = txtDocNo.Text
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Try
            If e.Error Is Nothing Then
                'ProgressBar1.Value = 0
                'Label1.Text = "ประมวลผลเรียบร้อย"

                'update work
                'BackgroundWorker1.ReportProgress(0)
                'ChkAutoProcess.Checked = False
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



    'Private Sub frmReCalculate_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing

    '    Try
    '        If Not W_MSG_Confirm("ต้องการให้โปรแกรมทำงานเบื้องหลัง ใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
    '            If Me.BackgroundWorker1.CancellationPending = False Then
    '                Me.BackgroundWorker1.CancelAsync()
    '                MSG = "END"
    '            End If
    '            e.Cancel = False
    '        Else
    '            'Me.Visible = False
    '            'NotifyIcon1.Visible = True
    '            'NotifyIcon1.ShowBalloonTip(1, "NitifyIcon", "Running Minimized", ToolTipIcon.Info)
    '            e.Cancel = True
    '        End If
    '    Catch ex As Exception
    '        W_MSG_Information(ex.Message)
    '    End Try


    'End Sub

    Private Sub ClearProcessedCol()
        For iRow As Integer = 0 To grdInterval.RowCount - 1
            grdInterval.Rows(iRow).Cells("Col_Done").Value = ""
        Next
    End Sub

    'Private Sub ChkAutoProcess_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If ChkAutoProcess.Checked = True Then

    '        If IsDate(txtTime.Value) = False Then
    '            MessageBox.Show("Please check time format (HH:mm).")
    '            ChkAutoProcess.Checked = False
    '            Exit Sub
    '        End If
    '    End If

    'End Sub




    Private Sub btnTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            'Me.Hide()
            'NotifyIcon1.Visible = True
            'Dim xDate As String = Now.ToString("yyyyMMdd")

            Dim c As New Cutomer_Service
            'Dim p As New Product_Service
            'Dim o As New OrderPicking_Service
            'Dim t_in As New OrderPicking_Service
            'Dim t_out As New OrderPicking_Service
            Dim Enddate As Date = Now.AddDays(625).ToString("yyyy-MM-dd")
            Dim Begindate As Date = "2015-01-01"
            ' Begindate = Begindate.ToString("yyyyMMdd")
            Dim i As Integer = 0
            While Begindate <= Enddate
                c.WebService_Customer(0, 0, "", Begindate.ToString("yyyyMMdd"))
                Begindate = Begindate.AddDays(1)
                i += 1
            End While
            MsgBox(i)

            'p.WebService_Product(0, 0, "", "20170601")
            'o.WebService_Salesorder("", "", "20170908")
            't_out.WebService_TransferOut("", "", "20170902")
            't_in.WebService_TransferIn("RL001700154", "", "")
            'If strCheck = "S" Then
            '    W_MSG_Information("Import สำเร็จ")
            'ElseIf strCheck = "E" Then
            '    W_MSG_Error("รหัส OrderIDซ้ำ")
            'Else
            '    W_MSG_Error("ไม่พบข้อมูล ที่จะ Import")
            'End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

  

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

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
        'For Each drRow As DataRow In xDT.Rows
        '    'Me.grdInterval.Rows.Insert(0)
        '    'Me.grdInterval.Rows(0).Cells("col_Num").Value = drRow("_Index").ToString
        '    'Me.grdInterval.Rows(0).Cells("Col_Status").Value = drRow("Status").ToString
        '    'Me.grdInterval.Rows(0).Cells("Co_Description").Value = drRow("Description").ToString
        'Next
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

    'Private Sub ChkSelectAllDay_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        chkMonday.Checked = ChkSelectAllDay.Checked
    '        chkTuesday.Checked = ChkSelectAllDay.Checked
    '        chkWednesday.Checked = ChkSelectAllDay.Checked
    '        chkThurseday.Checked = ChkSelectAllDay.Checked
    '        chkFriday.Checked = ChkSelectAllDay.Checked
    '        chkSaturday.Checked = ChkSelectAllDay.Checked
    '        chkSunday.Checked = ChkSelectAllDay.Checked

    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub ChkSelectAllINF_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try

    '        chkReturn.Checked = ChkSelectAllINF.Checked
    '        chkTransferIN.Checked = ChkSelectAllINF.Checked
    '        chkSalesOrder.Checked = ChkSelectAllINF.Checked
    '        chkTransferOut.Checked = ChkSelectAllINF.Checked
    '        chkmsCus.Checked = ChkSelectAllINF.Checked
    '        chkProduct.Checked = ChkSelectAllINF.Checked
    '        chkCreditNote.Checked = ChkSelectAllINF.Checked
    '        chkGoodsIssue.Checked = ChkSelectAllINF.Checked


    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
End Class