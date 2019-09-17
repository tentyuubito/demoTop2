Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports System.Windows.Forms
Imports System.Drawing


Public Class frmConfigPrint_Invoice
    Dim _dbSQL As New DBType_SQLServer

    Dim _Dt_Index As New DataTable
    Public _Customer_Shipping_Index As String = ""
    Public _Customer_Shipping_Id As String = ""
    Public _Customer_Index As String = ""
    Public _Invoice_No As String = ""

    Private _SalseOrder_Index As String
    Public Property SalseOrder_Index() As String
        Get
            Return _SalseOrder_Index
        End Get
        Set(ByVal value As String)
            _SalseOrder_Index = value
        End Set
    End Property


    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal Value As String)
            _Customer_Index = Value
        End Set
    End Property

    Public Property Invoice_No() As String
        Get
            Return _Invoice_No
        End Get
        Set(ByVal Value As String)
            _Invoice_No = Value
        End Set
    End Property
  

    Public Property Customer_Shipping_Id() As String
        Get
            Return _Customer_Shipping_Id
        End Get
        Set(ByVal Value As String)
            _Customer_Shipping_Id = Value
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

    Private Sub ShowValues(Optional ByVal ChkPrintFrom As Boolean = False)

        'strSQL  = "Select  Config_Rpt_Index,config_RPT_Print.Report_Name,Description ,SalesOrder_Index,Unit_Print,Total_Print,'" & _Customer_Shipping_Id & "' Customer_Shipping_Id,'" & _Customer_Shipping_Index & "' Customer_Shipping_Index"
        'strSQL &= " from config_RPT_Print "
        'strSQL &= " inner join config_report on config_RPT_Print.Report_Name = config_Report.Report_Name and  config_Report.Report_Group = 'H_Invoice' "
        'strSQL &= " where SalesOrder_Index = '" & _SalseOrder_Index & "'  and config_RPT_Print.status_id <> -1"
        ''strSQL &= " group by config_RPT_Print.Report_Name,Unit_Print,SalesOrder_Index,Description,Config_Rpt_Index "
        Dim dt_from As New DataTable
        Dim dt As New DataTable
        Dim Report_Des As String = ""
        Dim strSQL As String = ""

        strSQL = "Select  '' Description ,Customer_Shipping_Index,str1 Customer_Shipping_Id,'' as Report_Name,0 as Unit_Print, 0 as Total_Print, '' as SalseOrder_index,isnull(RPT_KSL_Invoice,0) RPT_KSL_Invoice,isnull(RPT_KSL_Invoice_Copy,0) RPT_KSL_Invoice_Copy,isnull(RPT_KSL_Invoice_Dup,0) RPT_KSL_Invoice_Dup,isnull(RPT_KSL_Recept,0) RPT_KSL_Recept ,isnull(RPT_KSL_Invoice_PrintFrom,0) RPT_KSL_Invoice_PrintFrom  from ms_Customer_Shipping where Customer_Shipping_Index = '" & _Customer_Shipping_Index & "'"

        

        dt_from = _dbSQL.DBExeQuery(strSQL)

        dt = _dbSQL.DBExeQuery("select Report_Name,Description FROM config_Report WHERE Report_Group = 'H_Invoice'")
        Dim displayView = New DataView(dt_from)
        Dim subset_grd As DataTable = displayView.ToTable(False, "Customer_Shipping_Index", "Customer_Shipping_Id", "Report_Name", "Description", "Unit_Print", "Total_Print", "SalseOrder_index")
        Dim subset_Rpt As New DataTable
        If ChkPrintFrom = False Then
            subset_Rpt = displayView.ToTable(False, "RPT_KSL_Invoice", "RPT_KSL_Invoice_Copy", "RPT_KSL_Invoice_Dup", "RPT_KSL_Recept")
        Else
            subset_Rpt = displayView.ToTable(False, "RPT_KSL_Invoice_PrintFrom")

        End If


        For i As Integer = 0 To subset_grd.Columns.Count - 1
            subset_grd.Columns(i).ReadOnly = False
            If subset_grd.Columns(i).DataType.Name.ToString = "String" Then
                subset_grd.Columns(i).MaxLength = 255
            End If
        Next

        For i As Integer = 0 To subset_Rpt.Columns.Count - 1
            Report_Des = _dbSQL.DBExeQuery_Scalar(String.Format("select Description FROM config_Report WHERE Report_Group = 'H_Invoice' and Report_Name = '{0}'", subset_Rpt.Columns(i).ColumnName.ToString))
            If ChkPrintFrom Then
                If subset_Rpt.Columns(i).ColumnName.ToString.ToUpper = "RPT_KSL_INVOICE_PRINTFROM" Then
                    Report_Des = "(สำเนา)ใบกำกับภาษี / ใบส่งของ"
                End If
            End If

            subset_grd.Rows.Add(dt_from.Rows(0).Item("Customer_Shipping_Index").ToString, dt_from.Rows(0).Item("Customer_Shipping_Id").ToString, subset_Rpt.Columns(i).ColumnName.ToString, Report_Des.ToString, subset_Rpt.Rows(0).Item(i).ToString, subset_Rpt.Rows(0).Item(i).ToString, _SalseOrder_Index)
        Next
        subset_grd.Rows.RemoveAt(0)
        grdPrintInvoice.AutoGenerateColumns = False
        '_Dt_Index = subset_grd.Copy
        grdPrintInvoice.DataSource = subset_grd

        'If dt_from.Rows.Count > 0 Then
        '    _Dt_Index = dt_from.Copy
        '    grdPrintInvoice.DataSource = dt_from
        'Else
        '    dt = _dbSQL.DBExeQuery("select Report_Name,Description FROM config_Report WHERE Report_Group = 'H_Invoice'")
        '    For i As Integer = 0 To dt.Rows.Count - 1
        '        dt_from.Rows.Add(-1, dt.Rows(i).Item("Report_Name").ToString(), dt.Rows(i).Item("Description").ToString(), _SalseOrder_Index, 0, 0, _Customer_Shipping_Id, _Customer_Shipping_Index)
        '    Next

        '    grdPrintInvoice.DataSource = dt_from
        'End If
    End Sub
    Sub ShowMain()
        Try
            Dim cls As New cls_syAditlog()
            Dim _dt As New DataTable
            _dt = cls.CheckDupSOinINV(SalseOrder_Index)
            If Not IsNothing(_dt) Then

                For i As Integer = 0 To _dt.Columns.Count - 1
                    For Each drCol As DataRow In CType(grdPrintInvoice.DataSource, DataTable).Rows
                        If drCol("Report_Name").ToString.Contains(_dt.Columns(i).ColumnName.ToString) Then
                            drCol("Total_Print") = _dt.Rows(0).Item(i).ToString
                        End If
                    Next
                Next

            Else
                cls.SetNewSO_Report(SalseOrder_Index)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub frmConfigPrint_Invoice_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ShowValues()
            ShowMain()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print.Click
        Try
            'Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim printDialog1 As New PrintDialog
            Dim checkSelect As Boolean = False
            For i As Integer = 0 To grdPrintInvoice.Rows.Count - 1
                If grdPrintInvoice.Rows(i).Cells("Col_Select").Value Then
                    If grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value = 0 Then
                        grdPrintInvoice.CurrentCell = grdPrintInvoice(4, i)
                        W_MSG_Information("กรุณา ระบุจำนวนปริ้น")
                        Exit Sub
                    End If
                    checkSelect = True
                End If

            Next
            If checkSelect = False Then
                W_MSG_Information("เลือก Invoice ที่จะปริ้นก่อน")
                Exit Sub
            End If
            printDialog1.Document = Me.PrintDocument1

            Dim drl As DialogResult = printDialog1.ShowDialog()
            If drl = Windows.Forms.DialogResult.OK Then


                For i As Integer = 0 To grdPrintInvoice.Rows.Count - 1
                    If grdPrintInvoice.Rows(i).Cells("Col_Select").Value Then

                        Dim sPage As Integer = Me.PrintDocument1.PrinterSettings.FromPage
                        'Get the number of End Page
                        Dim ePage As Integer = Me.PrintDocument1.PrinterSettings.ToPage
                        'Get the printer name
                        Dim PrinterName As String = Me.PrintDocument1.PrinterSettings.PrinterName
                        'Dim oReport As New Loading_Report_Invoice(grdPrintInvoice.Rows(i).Cells("Col_Report_Name").Value.ToString, "And SalesOrder_Index ='" & _SalseOrder_Index & "' order by Seq")
                        Dim obj_config_RPT As New config_Report_Invoice
                        Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                        cry = obj_config_RPT.GetReportInfo(grdPrintInvoice.Rows(i).Cells("Col_Report_Name").Value.ToString, "And SalesOrder_Index ='" & _SalseOrder_Index & "' order by Seq", Customer_Index)
                        'cry.SetParameterValue("Report_Name", "")
                        cry.SetParameterValue("Customer_Index", Customer_Index)
                        cry.PrintOptions.PrinterName = PrinterName
                        'Start the printing process. Provide details of the print job
                        'using the arguments.
                        cry.PrintToPrinter(grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value, False, sPage, ePage)
                        Dim sy_log As New cls_syAditlog
                        'If _Dt_Index.Rows.Count = 0 Then
                        '    sy_log.Insert_Config_RPT(grdPrintInvoice.Rows(i).Cells("Col_Report_Name").Value.ToString, grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value _
                        '                           , grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value, _SalseOrder_Index)
                        'Else

                        '    sy_log.Update_Config_RPT(grdPrintInvoice.Rows(i).Cells("Col_Report_Name").Value.ToString, grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value _
                        '                                                       , grdPrintInvoice.Rows(i).Cells("Col_Print_Total").Value + grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value, _SalseOrder_Index, _Dt_Index.Rows(i).Item("Config_Rpt_Index").ToString)
                        'End If

                        sy_log.Process_ID = 10
                        sy_log.Description = "Invoice No : " & Me.Invoice_No & " Print Invoice : " & grdPrintInvoice.Rows(i).Cells("Col_Report_Name").Value.ToString & " Unit : " & grdPrintInvoice.Rows(i).Cells("Col_Unit_Print").Value
                        sy_log.Document_Index = _SalseOrder_Index
                        sy_log.Log_Type_ID = "-1"
                        sy_log.Insert_Master()


                        cry.Dispose()

                    End If
                Next
                Dim str As String = ""
                str = " Update tb_SalesOrder_PrintINV SET ,"

                For Each drCol As DataRow In CType(grdPrintInvoice.DataSource, DataTable).Rows
                    str &= "," & drCol("Report_Name").ToString & " += " & drCol("Unit_Print")
                Next
                str = str.Replace(",,", "")
                str &= String.Format(" Where SalesOrder_Index = '{0}' ", Me.SalseOrder_Index)
                _dbSQL.DBExeNonQuery(str)
            End If
            ShowMain()

            'cry.SetParameterValue("Head_Rpt", Head_Report.Rows(0).Item(0).ToString)
            'frm.CrystalReportViewer1.ReportSource = cry
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If grdPrintInvoice.RowCount > 0 Then
                For iRow As Integer = 0 To grdPrintInvoice.RowCount - 1
                    grdPrintInvoice.Rows(iRow).Cells("Col_Select").Value = Me.chkSelectAll.Checked
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdPrintInvoice_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPrintInvoice.CellClick
        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "Col_btn_View"
                    Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                    Dim obj_config_RPT As New config_Report_Invoice
                    'Dim oReport As New Loading_Report_Invoice(grdPrintInvoice.CurrentRow.Cells("Col_Report_Name").Value.ToString, "And SalesOrder_Index ='" & _SalseOrder_Index & "' order by Seq")
                    Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
                    cry = obj_config_RPT.GetReportInfo(grdPrintInvoice.CurrentRow.Cells("Col_Report_Name").Value.ToString, "And SalesOrder_Index ='" & _SalseOrder_Index & "' order by Seq", Customer_Index)
                    cry.SetParameterValue("Customer_Index", Customer_Index)
                    frm.CrystalReportViewer1.ReportSource = cry
                    frm.ShowDialog()
                    cry.Dispose()
                    frm.CrystalReportViewer1.Dispose()
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub ChkPrintFrom_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkPrintFrom.CheckedChanged
        Try
            ShowValues(ChkPrintFrom.Checked)
            ShowMain()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class