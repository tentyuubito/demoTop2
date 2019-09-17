Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_Formula.W_Module

Public Class frmStickerBarcode

    Private Sub btnPrintLPN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintLPN.Click
        Try
            Dim objDBTempIndex As New Sy_AutoNumber

          
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New CrystalDecisions.CrystalReports.Engine.ReportDocument

            Dim odt As New DataTable
            odt = New DataTable("VIEW_RPT_LocationBar")
            Dim ods As New DataSet

            Dim column1 As DataColumn = New DataColumn("Location_Alias")
            column1.DataType = System.Type.GetType("System.String")

            odt.Columns.Add(column1)

            oCrystal.Load(WV_Report_Path & "\Report\TopCharoen\rptPrintRunning.rpt")
            Dim LPN_No As String = ""


            For i As Integer = 0 To CInt(txtValue.Text)

                Dim Row1 As DataRow
                Row1 = odt.NewRow()
                Row1.Item("Location_Alias") = objDBTempIndex.getSys_Value("LPN_No")
                odt.Rows.Add(Row1)
            Next
           
            ods.Tables.Add(odt)

            ods.DataSetName = "dsLocationBar"
            ods.Tables(0).TableName = "VIEW_RPT_LocationBar"

            oCrystal.SetDataSource(ods)


            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub
End Class
