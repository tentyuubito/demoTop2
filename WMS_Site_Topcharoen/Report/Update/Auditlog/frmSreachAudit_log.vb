Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Public Class frmSreachAudit_log


    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Try
            Dim strWhere As String = ""
            If chkdate.Checked Then
                strWhere &= String.Format("  And user_index = '{0}' ", cboUser.SelectedValue)
            End If
            If chkUser.Checked Then
                strWhere &= String.Format("  And ( Log_Date  >= '{0}' And  Log_Date  <= '{1}') ", dtStartDate.Value.ToString("yyyy-MM-dd 01:00:00"), dtEndDate.Value.ToString("yyyy-MM-dd 23:59:59"))
            End If
            If chkDocument_No.Checked Then
                strWhere &= String.Format("  And Document_No like '%{0}%' ", Me.txtDocument_No.Text)
            End If
            If Me.chkLog_Type.Checked Then
                strWhere &= String.Format("  And Log_Type_ID = '{0}'", Me.cboLog_Type.SelectedValue)
            End If
            Dim Report_Name = "Audit_log"
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, strWhere)
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oReport.LoadReport()
            cry.SetParameterValue("Bdate", dtStartDate.Value.ToString("dd/MM/yyyy"))
            cry.SetParameterValue("Edate", dtEndDate.Value.ToString("dd/MM/yyyy"))
            frm.CrystalReportViewer1.ReportSource = cry
            frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
   
    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getLog_Type()
        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        objDT = objClassDB.DBExeQuery("Select * from Sy_Log_Type where Status <> -1 Order by Log_Type_ID")
        Try
            With Me.cboLog_Type
                .DisplayMember = "Log_Type"
                .ValueMember = "Log_Type_ID"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub

    Private Sub getUser()
        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        objDT = objClassDB.DBExeQuery("Select * from se_user where status_id <> -1")
        Try
            With cboUser
                .DisplayMember = "userFullName"
                .ValueMember = "user_index"
                .DataSource = objDT
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
        End Try
    End Sub
    Private Sub frmSreachAudit_log_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.getUser()
            Me.getLog_Type()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class