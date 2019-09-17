Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPackingBag_SL

#Region " + Variable + "

    Private _Service As Tb_Packing_TopCharoen

    Private _DataSLBinding As BindingSource
    Private _DataCustomerShipping, _DataSL, _DataSave As DataTable

#End Region

#Region " + Property + "

    'Nothing

#End Region

#Region " + Funcation + "

    Private Function GetCurrentRow(ByVal DataGrid As DataGridView) As DataRow
        Try
            If DataGrid Is Nothing OrElse DataGrid.CurrentRow Is Nothing Then
                Return Nothing
            End If

            Return DataGrid.CurrentRow.DataBoundItem.Row

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

    Private Function GetCurrentRowValue(ByVal DataGrid As DataGridView, ByVal ColumnName As String) As String
        Try
            Dim DataRow As DataRow = GetCurrentRow(DataGrid)

            If DataRow IsNot Nothing AndAlso DataRow.Table.Columns.Contains(ColumnName) Then
                Return DataRow.Item(ColumnName).ToString
            Else
                Return Nothing
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

#End Region

#Region " + Sub + "

    Private Sub Initproperty()
        Try
            Me._Service = New Tb_Packing_TopCharoen
            Me._DataSLBinding = New BindingSource

            With Me.grdShipping
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
            End With

            With Me.grdSL
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
                .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.SelectionForeColor
            End With

            Me.WindowState = FormWindowState.Maximized

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub Save()
        Try



            If Me._DataSave Is Nothing OrElse Not Me._DataSave.Rows.Count > 0 Then
                Throw New Exception("ไม่พบข้อมูลที่จะบันทึก")
            End If


            If Me._DataSave.DefaultView.ToTable(True, "Urgent_Id", "Droppoint_Desc").Rows.Count > 1 Then
                Throw New Exception("ไม่สามารถบันทึกได้ เนื่องจากรหัสความด่วนและ Drop point ไม่อยู่ในกลุ่มเดียวกัน")
            End If


            Dim BarcodeGroup As String = Me._Service.SavePackingGroupSL(Me._DataSave)
            W_MSG_Information("บันทึกข้อมูลเสร็จสิ้น")

            Dim clsConfig_Report As New WMS_STD_Master.config_Report
            Dim frmReport As WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As ReportDocument

            'ใบคุมของ
            oCrystal = Me._Service.GetReportInfo("rptTransferDocRange", " AND Barcode_BAG = '" & BarcodeGroup & "' ")
            frmReport = New WMS_STD_OUTB_Report.frmReportViewerMain
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

            'Warranty
            oCrystal = Me._Service.GetReportInfo("rptWarranty_TOR", " AND Barcode_GROUP = '" & BarcodeGroup & "' ")
            frmReport = New WMS_STD_OUTB_Report.frmReportViewerMain
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListCustomerShipping()
        Try
            ClearDataSL()
            ClearDataSave()

            Using Data As DataTable = Me._Service.ListCustomerShippingPacking(Me.txtRoute.Text.Trim)
                Dim HaveDataCustomerShipping As Boolean = Data IsNot Nothing AndAlso Data.Rows.Count > 0
                If Not HaveDataCustomerShipping Then
                    W_MSG_Information("ไม่มีข้อมูล HUB สำหรับแพคลงถุง")
                End If

                If Me._DataCustomerShipping Is Nothing Then
                    Me._DataCustomerShipping = Data.Copy
                    Me.grdShipping.DataSource = Me._DataCustomerShipping
                Else
                    Me._DataCustomerShipping.Rows.Clear()
                    If HaveDataCustomerShipping Then
                        Me._DataCustomerShipping.Merge(Data)
                    End If
                End If
            End Using

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListDataSL()
        Dim CustomerShippingIndex As String = GetCurrentRowValue(Me.grdShipping, "Customer_Shipping_Index")
        Try
            ClearDataSave()

            Using Data As DataTable = Me._Service.ListTraySL(CustomerShippingIndex)
                Dim HaveData As Boolean = Data IsNot Nothing AndAlso Data.Rows.Count > 0
                If Not HaveData Then
                    Exit Sub
                End If

                If Me._DataSL Is Nothing Then
                    Me._DataSL = Data.Copy
                    Me._DataSave = Me._DataSL.Clone

                    Me._DataSLBinding.DataSource = Me._DataSL
                    Me._DataSLBinding.Sort = "Checked ASC, SalesOrder_No ASC"
                    Me.grdSL.DataSource = Me._DataSLBinding
                Else
                    Me._DataSL.Rows.Clear()
                    If HaveData Then
                        Me._DataSL.Merge(Data)
                    End If
                End If
            End Using

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ScanBarcodeSL(ByVal BarcodeSL As String)
        Try
            If String.IsNullOrEmpty(BarcodeSL) OrElse Me._DataSL Is Nothing OrElse Not Me._DataSL.Rows.Count > 0 Then
                Exit Sub
            End If

            Dim SelectedSL As DataRow() = Me._DataSL.Select(String.Format("Barcode_SL = '{0}'", BarcodeSL))
            If Not SelectedSL.Length > 0 Then
                Throw New Exception("ไม่พบ Barcode SL นี้")
            End If

            Dim IsCompleted As Boolean = Me._DataSL.Select(String.Format("Barcode_SL = '{0}' AND Checked = 0", BarcodeSL)).Length = 0
            If IsCompleted Then
                Throw New Exception("Barcode SL นี้ทำการแพคเรียบร้อยแล้ว")
            End If

            For Each Row As DataRow In SelectedSL
                Row.Item("Checked") = 1
                Me._DataSave.Rows.Add(Row.ItemArray)
            Next

            Me._DataSL.AcceptChanges()

            SetBackgroundColor()

            Me.btnSave.Enabled = True
            Me.btnClear.Enabled = True

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetBackgroundColor()
        Dim Data As DataRow

        For i As Integer = 0 To Me.grdSL.Rows.Count - 1
            With Me.grdSL.Rows.Item(i)
                Data = .DataBoundItem.Row()

                If Data.Item("Checked") = 1 Then
                    .DefaultCellStyle.BackColor = Color.LightGreen
                Else
                    .DefaultCellStyle.BackColor = Color.White
                End If
            End With
        Next
    End Sub

    Private Sub ClearDataSL()
        Try
            If Me._DataSL IsNot Nothing AndAlso Me._DataSL.Rows.Count > 0 Then
                Me._DataSL.Rows.Clear()
            End If

            Me.txtBarcodeSL.Clear()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ClearDataSave()
        Try
            If Me._DataSave IsNot Nothing AndAlso Me._DataSave.Rows.Count > 0 Then
                Me._DataSave.Rows.Clear()
            End If

            Me.btnSave.Enabled = False
            Me.btnClear.Enabled = False

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

#End Region

#Region " + Event + "

    Private Sub frmPackingBag_SL_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Initproperty()

            ListCustomerShipping()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            ListCustomerShipping()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub grdCustomerShippingLocation_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdShipping.SelectionChanged
        Try
            ListDataSL()

            Me.txtBarcodeSL.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub txtRoute_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRoute.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Try
            ListCustomerShipping()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub txtBarcodeSL_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcodeSL.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Try
            If String.IsNullOrEmpty(Me.txtBarcodeSL.Text.Trim) Then
                Throw New Exception("กรุณาระบุข้อมูล Barcode")
            End If

            ScanBarcodeSL(Me.txtBarcodeSL.Text.Trim)

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        Finally
            Me.txtBarcodeSL.Clear()
            Me.txtBarcodeSL.Focus()
        End Try
    End Sub

    Private Sub grdSL_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdSL.SelectionChanged
        Me.grdSL.ClearSelection()
        Me.txtBarcodeSL.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If W_MSG_Confirm("ยืนยันการปิดถุง ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Save()

            ListCustomerShipping()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If W_MSG_Confirm("ยืนยันล้างข้อมูลที่ Scan ทั้งหมด") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ClearDataSave()

            ListDataSL()

            Me.txtBarcodeSL.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

End Class

