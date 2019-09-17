Public Class Form1

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            '  Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_SalesOrderPacking_Index").Value
            'Dim index As String = ""
            'For i As Integer = 0 To grdPackingSoGroupView.Rows.Count - 1
            '    index &= "'" & Me.grdPackingSoGroupView.Rows(i).Cells("Col_SalesOrderPacking_index2").Value & "',"
            'Next


            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            oCrystal = clsPacking.GetReportInfo("rptTransferDocRange", "")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try

    End Sub
   

End Class