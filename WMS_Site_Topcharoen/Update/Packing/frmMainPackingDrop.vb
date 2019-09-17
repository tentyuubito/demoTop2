Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Imports WMS_STD_Master_Datalayer

Public Class frmMainPackingDrop

    Public Enum ePackingType
        CL = 0
        SL
    End Enum

    Private _PackingType As ePackingType

    Public dtSalesOrder As New DataTable
    Public dtSalesOrderGroupItem As New DataTable
    Private _strSalesOrder_Index As String = ""
    Private _strProductType_Index As String = ""
    Private _customer_shipping_index As String = ""
    Private _Customer_Shipping_Location_Index As String = ""

    Public Sub New(ByVal PackingType As ePackingType)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me._PackingType = PackingType
        Select Case Me._PackingType
            Case ePackingType.CL
                Me.lblPackingType.Text = "C L"
                Me.lblPackingType.ForeColor = Color.Blue
                Me.btnPrintWarranty.Visible = False

            Case ePackingType.SL
                Me.lblPackingType.Text = "S L"
                Me.lblPackingType.ForeColor = Color.Green
                Me.btnPrintWarranty.Visible = True
        End Select
    End Sub

    Private Sub btnClosedPacking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Select Case Me._PackingType
                Case ePackingType.CL
                    Dim frm As New frmPackingBag
                    frm.ShowDialog()

                Case ePackingType.SL
                    Dim frm As New frmPackingBag_SL
                    frm.ShowDialog()
            End Select
            
            getSo()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnFrmClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFrmClose.Click
        Me.Close()

    End Sub



    Private Sub getSo()
        Try
            _strProductType_Index = ""
            _strSalesOrder_Index = ""

            Me.lblSumItem.Text = ""
            Dim clsPacking As New Tb_Packing_TopCharoen

            Dim conditionSearch As String = ""

            If chkDate.Checked Then
                'conditionSearch &= " and  (tb_salesOrderpacking.[DateAdd] >=  '" & dateStart.Value.ToString("yyyy-MM-dd 00:00:00") & "' AND tb_salesOrderpacking.[DateAdd] <= '" & dateEnd.Value.ToString("yyyy-MM-dd 23:00") & "' )"
                conditionSearch &= String.Format(" and CONVERT(VARCHAR(10), tb_SalesOrderPackingItem.group_date,111) Between   '{0}' AND '{1}'  ", dateStart.Value.ToString("yyyy/MM/dd"), dateEnd.Value.ToString("yyyy/MM/dd"))
            End If

       



            If Not String.IsNullOrEmpty(Me.txtSalesOrderNo.Text.Trim) Then
                conditionSearch &= " AND tb_salesOrderpacking.SalesOrderPacking_Index IN ( "
                conditionSearch &= "    SELECT SalesOrderPacking_Index "
                conditionSearch &= "    FROM tb_SalesOrderPackingItem "
                conditionSearch &= "    WHERE SalesOrder_Index IN ( "
                conditionSearch &= " 	    SELECT SalesOrder_Index "
                conditionSearch &= " 	    FROM tb_SalesOrder "
                conditionSearch &= " 	    WHERE SalesOrder_No = '" & Me.txtSalesOrderNo.Text.Trim & "' "
                conditionSearch &= "    ) "
                conditionSearch &= " ) "
            End If

            Select Case Me._PackingType
                Case ePackingType.CL
                    If Not String.IsNullOrEmpty(txtBarcode.Text.Trim) Then
                        conditionSearch &= " and  tb_salesOrderpacking.BarcodePacking Like  '%" & txtBarcode.Text.Trim & "%'"
                    End If

                    dtSalesOrder = clsPacking.getSalesOrderMainPacking(conditionSearch, "")

                Case ePackingType.SL
                    If Not String.IsNullOrEmpty(txtBarcode.Text.Trim) Then
                        conditionSearch &= " and  tb_SalesOrderPackingItem.Barcode_GROUP Like  '%" & txtBarcode.Text.Trim & "%'"
                    End If

                    dtSalesOrder = clsPacking.getSalesOrderMainPackingSL(conditionSearch, "")
            End Select

            Me.grdPackingSoGroupView.DataSource = Nothing
            Me.grdPackingSoGroupItem.DataSource = dtSalesOrder

            Me.lblSum.Text = "รวม  : " & FormatNumber(Me.dtSalesOrder.Rows.Count, 0) & " รายการ"
            If dtSalesOrder.Rows.Count <= 0 Then
                Me.grdPackingSoGroupView.DataSource = Nothing
            End If

            Me.BtnDel.Enabled = dtSalesOrder.Rows.Count > 0
            Me.btnPrint.Enabled = dtSalesOrder.Rows.Count > 0
            Me.btnPrintWarranty.Enabled = dtSalesOrder.Rows.Count > 0

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub



    Private Sub SetBackgroundColor()
        Dim Data As DataRow
        For i As Integer = 0 To Me.grdPackingSoGroupView.Rows.Count - 1
            Data = Me.grdPackingSoGroupView.Rows.Item(i).DataBoundItem.Row()

            If Data("Barcode_Qty") = Data("Barcode_Qty_Confirm") Then
                Me.grdPackingSoGroupView.Rows.Item(i).DefaultCellStyle.BackColor = Color.LightGreen
                Data("CheckHeader") = 1
            Else
                Me.grdPackingSoGroupView.Rows.Item(i).DefaultCellStyle.BackColor = Color.White
                Data("CheckHeader") = 0
            End If
        Next
    End Sub

    Private Sub frmMainPackingDrop_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        Me.chkDate.Checked = True
        grdPackingSoGroupView.AutoGenerateColumns = False
        getSo()
    End Sub



    Private Sub grdSOView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPackingSoGroupView.Sorted
        SetBackgroundColor()
    End Sub

    Private Sub grdPackingSoGroupItem_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdPackingSoGroupItem.SelectionChanged
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim dtSalesOrderItem As New DataTable

            Select Case Me._PackingType
                Case ePackingType.CL
                    Dim index As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_SalesOrderPacking_Index").Value
                    dtSalesOrderItem = clsPacking.getSalesOrderMainPackingItem(index, "")

                Case ePackingType.SL
                    Dim Barcode As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Group").Value
                    dtSalesOrderItem = clsPacking.getSalesOrderMainPackingItemSL(Barcode)
            End Select

            Me.grdPackingSoGroupView.DataSource = dtSalesOrderItem
            lblSumItem.Text = "จำนวน " & dtSalesOrderItem.Rows.Count & " รายการ"

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            getSo()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub BtnDel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnDel.Click
        Try
            If W_MSG_Confirm("คุณต้องการลบรายการนี้หรือไม่ ?") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim dtSalesOrderItem As New DataTable

            Dim index As String = ""
            Select Case Me._PackingType
                Case ePackingType.CL
                    index = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_SalesOrderPacking_Index").Value
                    If Not clsPacking.CancelSalesOrderPackingBag(index) Then
                        Exit Sub
                    End If

                Case ePackingType.SL
                    index = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Group").Value
                    If Not clsPacking.CancelSalesOrderPackingBagSL(index) Then
                        Exit Sub
                    End If
            End Select

            W_MSG_Information("ลบเสร็จสิ้น")
            getSo()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument

            Dim Index As String = ""
            Select Case Me._PackingType
                Case ePackingType.CL
                    Index = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_SalesOrderPacking_Index").Value
                    oCrystal = clsPacking.GetReportInfo("rptBarcodeBagMini", " and SalesOrderPacking_index = '" & Index & "' ")
                    frmReport.CrystalReportViewer1.ReportSource = oCrystal
                    frmReport.ShowDialog()

                    oCrystal = clsPacking.GetReportInfo("rptTransferDocRange", " and SalesOrderPacking_index = '" & Index & "'")

                Case ePackingType.SL
                    Index = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Group").Value
                    oCrystal = clsPacking.GetReportInfo("rptTransferDocRange", " AND Barcode_BAG = '" & index & "' ")
            End Select

            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnPrintWarranty_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintWarranty.Click
        Try
            Dim clsPacking As New Tb_Packing_TopCharoen
            Dim clsConfig_Report As New config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument

            Dim BarcodeGroup As String = Me.grdPackingSoGroupItem.Rows(grdPackingSoGroupItem.CurrentRow.Index).Cells("Col_Group").Value

            Dim frmReport2 As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal2 As New ReportDocument

            oCrystal = clsPacking.GetReportInfo("rptWarranty_TOR", " AND Barcode_GROUP = '" & BarcodeGroup & "' ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

End Class

