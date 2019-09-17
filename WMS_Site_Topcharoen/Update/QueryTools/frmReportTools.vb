Imports System.Globalization

Public Class frmReportTools

    Private Sub frmReportTools_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim _strUrl As String = New WMS_STD_Formula.config_CustomSetting().getConfig_Key_DEFUALT("DEFAULT_REPORT_TOOLS")
            'System.Diagnostics.Process.Start(_strUrl)
            Me.wbsReportTools.Navigate(_strUrl)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class