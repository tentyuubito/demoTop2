Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Imports System.Drawing


Public Class frmConfigPrintReportBy_Customer_Shipping

    Private _Customer_Shipping_Index As String
    Public Property Customer_Shipping_Index() As String
        Get
            Return _Customer_Shipping_Index
        End Get
        Set(ByVal value As String)
            _Customer_Shipping_Index = value
        End Set
    End Property

    Dim exc As New DBType_SQLServer
    Private Sub frmConfigPrintReportBy_Customer_Shipping_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim strSQL As String = ""
            Dim dt As New DataTable
            Dim dt_nameRpt As New DataTable
            strSQL = "select * from config_Report where report_name like '%RPT_KSL%' and Report_Group= 'H_Invoice'"
            dt_nameRpt = exc.DBExeQuery(strSQL)
            For Each dr As DataRow In dt_nameRpt.Rows
                If dr("Report_Name").ToString = lblRPT_KSL_Invoice.Tag Then
                    lblRPT_KSL_Invoice.Text = dr("Str1").ToString.Replace(vbNewLine, " ")
                End If
                If dr("Report_Name").ToString = lblRPT_KSL_Invoice_Copy.Tag Then
                    lblRPT_KSL_Invoice_Copy.Text = dr("Str1").ToString.Replace(vbNewLine, " ")
                End If
                If dr("Report_Name").ToString = lblRPT_KSL_Invoice_Dup.Tag Then
                    lblRPT_KSL_Invoice_Dup.Text = dr("Str1").ToString.Replace(vbNewLine, " ")
                End If
                If dr("Report_Name").ToString = lblRPT_KSL_Invoice_PrintFrom.Tag Then
                    lblRPT_KSL_Invoice_PrintFrom.Text = dr("Str1").ToString.Replace(vbNewLine, " ")
                End If
                If dr("Report_Name").ToString = lblRPT_KSL_Recept.Tag Then
                    lblRPT_KSL_Recept.Text = dr("Str1").ToString.Replace(vbNewLine, " ")
                End If
            Next

            strSQL = "SELECT isnull(RPT_KSL_Invoice,0) RPT_KSL_Invoice"
            strSQL &= ",isnull(RPT_KSL_Invoice_Copy,0) RPT_KSL_Invoice_Copy"
            strSQL &= ",isnull(RPT_KSL_Invoice_Dup,0) RPT_KSL_Invoice_Dup"
            strSQL &= ",isnull(RPT_KSL_Invoice_PrintFrom,0) RPT_KSL_Invoice_PrintFrom"
            strSQL &= ",isnull(RPT_KSL_Recept,0) RPT_KSL_Recept "
            strSQL &= String.Format(" FROM ms_customer_shipping where isnull(status_id,0)<> -1 and Customer_Shipping_Index = {0}", _Customer_Shipping_Index)
            dt = exc.DBExeQuery(strSQL)

            If dt.Rows.Count > 0 Then
                txtRPT_KSL_Invoice.Text = dt.Rows(0).Item("RPT_KSL_Invoice").ToString
                txtRPT_KSL_Invoice_Copy.Text = dt.Rows(0).Item("RPT_KSL_Invoice_Copy").ToString
                txtRPT_KSL_Invoice_Dup.Text = dt.Rows(0).Item("RPT_KSL_Invoice_Dup").ToString
                txtRPT_KSL_Invoice_PrintFrom.Text = dt.Rows(0).Item("RPT_KSL_Invoice_PrintFrom").ToString
                txtRPT_KSL_Recept.Text = dt.Rows(0).Item("RPT_KSL_Recept").ToString
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtrpt1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) _
    Handles txtRPT_KSL_Invoice.KeyPress, txtRPT_KSL_Invoice_Copy.KeyPress, txtRPT_KSL_Invoice_Dup.KeyPress, txtRPT_KSL_Invoice_PrintFrom.KeyPress, txtRPT_KSL_Recept.KeyPress
        Try
            Select Case Asc(e.KeyChar)
                Case 48 To 57
                    e.Handled = False
                Case 8, 13, 46
                    e.Handled = False

                Case Else
                    e.Handled = True
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim strsql As String = ""
            strsql = "Update ms_customer_shipping set "
            strsql &= String.Format("RPT_KSL_Invoice = {0}", IIf(String.IsNullOrEmpty(txtRPT_KSL_Invoice.Text), 0, txtRPT_KSL_Invoice.Text))
            strsql &= String.Format(",RPT_KSL_Invoice_Copy= {0}", IIf(String.IsNullOrEmpty(txtRPT_KSL_Invoice_Copy.Text), 0, txtRPT_KSL_Invoice_Copy.Text))
            strsql &= String.Format(",RPT_KSL_Invoice_Dup= {0}", IIf(String.IsNullOrEmpty(txtRPT_KSL_Invoice_Dup.Text), 0, txtRPT_KSL_Invoice_Dup.Text))
            strsql &= String.Format(",RPT_KSL_Invoice_PrintFrom= {0}", IIf(String.IsNullOrEmpty(txtRPT_KSL_Invoice_PrintFrom.Text), 0, txtRPT_KSL_Invoice_PrintFrom.Text))
            strsql &= String.Format(",RPT_KSL_Recept  = {0}", IIf(String.IsNullOrEmpty(txtRPT_KSL_Recept.Text), 0, txtRPT_KSL_Recept.Text))
            strsql &= String.Format(" where Customer_Shipping_index = '{0}' ", _Customer_Shipping_Index)
            exc.DBExeNonQuery(strsql)
            W_MSG_Information_ByIndex(1)
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class