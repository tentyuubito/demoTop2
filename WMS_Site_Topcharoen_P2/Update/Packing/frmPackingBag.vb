Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPackingBag

#Region " + Variable + "

    Private _Service As Tb_Packing_TopCharoen

    Private _DataShippingBinding, _DataTrayBinding, _DataCLBinding As BindingSource
    Private _DataComboBoxCustomerShipping, _DataCustomerShipping, _Data, _DataTray, _DataCL, _DataSave, _DataB1 As DataTable

    Private _SelectedTray As String

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
            Me._DataCLBinding = New BindingSource
            Me._DataTrayBinding = New BindingSource
            Me._DataShippingBinding = New BindingSource

            With Me.grdShipping
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
            End With

            With Me.grdTray
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToResizeRows = False
                .DefaultCellStyle.SelectionBackColor = .DefaultCellStyle.BackColor
                .DefaultCellStyle.SelectionForeColor = .DefaultCellStyle.SelectionForeColor
            End With

            With Me.grdCL
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


            If Me._DataSave.DefaultView.ToTable(True, "Urgent_Id", "OMS_DropPoint_Desc").Rows.Count > 1 Then
                Throw New Exception("ไม่สามารถบันทึกได้ เนื่องจากรหัสความด่วนและ Drop point ไม่อยู่ในกลุ่มเดียวกัน")
            End If

            Dim CustomerShippingIndex As String = GetCurrentRowValue(Me.grdShipping, "Customer_Shipping_Location_Index")

            Dim PackingBagIndex As String = Me._Service.SavePackingBag(CustomerShippingIndex, Me._DataSave, Me._DataB1)
            W_MSG_Information("บันทึกข้อมูลเสร็จสิ้น")

            Dim clsConfig_Report As New WMS_STD_Master.config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument

            oCrystal = Me._Service.GetReportInfo("rptBarcodeBagMini", " and SalesOrderPacking_index = '" & PackingBagIndex & "' ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

            oCrystal = Me._Service.GetReportInfo("rptTransferDocRange", " and SalesOrderPacking_index = '" & PackingBagIndex & "'")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListCustomerShippingLocation()
        Try
            Using Data As DataTable = Me._Service.ListCustomerShippingLocationPacking(Me.txtRoute.Text.Trim)
                Dim HaveDataCustomerShipping As Boolean = Data IsNot Nothing AndAlso Data.Rows.Count > 0
                If Not HaveDataCustomerShipping Then
                    W_MSG_Information("ไม่มีข้อมูลสาขาสำหรับแพคลงถุง")
                End If

                Dim DataComboBox As DataTable = Data.Copy.DefaultView.ToTable(True, "Customer_Shipping", "Customer_Shipping_Index")
                Dim NewRow As DataRow = DataComboBox.NewRow
                NewRow.Item("Customer_Shipping") = "ทั้งหมด"
                NewRow.Item("Customer_Shipping_Index") = ""
                DataComboBox.Rows.InsertAt(NewRow, 0)

                If Me._DataComboBoxCustomerShipping Is Nothing Then
                    Me._DataComboBoxCustomerShipping = DataComboBox
                    With Me.cboShipping
                        .DisplayMember = "Customer_Shipping"
                        .ValueMember = "Customer_Shipping_Index"
                        .DataSource = Me._DataComboBoxCustomerShipping
                    End With
                Else
                    Me._DataComboBoxCustomerShipping.Rows.Clear()
                    If HaveDataCustomerShipping Then
                        Me._DataComboBoxCustomerShipping.Merge(DataComboBox)
                        Me.cboShipping.SelectedIndex = 0
                    End If
                End If

                If Me._DataCustomerShipping Is Nothing Then
                    Me._DataCustomerShipping = Data.Copy

                    Me._DataShippingBinding.DataSource = Me._DataCustomerShipping
                    Me.grdShipping.DataSource = Me._DataShippingBinding
                Else
                    Me._DataCustomerShipping.Rows.Clear()
                    If HaveDataCustomerShipping Then
                        Me._DataCustomerShipping.Merge(Data)
                    End If
                End If

                If Me.cboShipping.SelectedIndex < 0 OrElse String.IsNullOrEmpty(Me.cboShipping.SelectedValue) Then
                    Me._DataShippingBinding.Filter = ""
                Else
                    Me._DataShippingBinding.Filter = String.Format("Customer_Shipping_Index = '{0}'", Me.cboShipping.SelectedValue)
                End If
            End Using

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListData()
        Dim CustomerShippingIndex As String = GetCurrentRowValue(Me.grdShipping, "Customer_Shipping_Location_Index")
        Try
            ClearTray()
            ClearDataSave()

            Using Data As DataTable = Me._Service.ListTray(CustomerShippingIndex)
                Dim HaveData As Boolean = Data IsNot Nothing AndAlso Data.Rows.Count > 0
                If Not HaveData Then
                    Exit Sub
                End If

                If Me._Data Is Nothing Then
                    Me._Data = Data.Copy
                    Me._DataSave = Me._Data.Clone
                Else
                    Me._Data.Rows.Clear()
                    If HaveData Then
                        Me._Data.Merge(Data)
                    End If
                End If

                If Me._DataTray Is Nothing Then
                    Me._DataTray = New DataTable
                    Me._DataTray.Columns.Add("Checked", GetType(Integer))
                    Me._DataTray.Columns.Add("Barcode_Tray", GetType(String))
                    Me._DataTray.Columns.Add("Qty_Barcode_CL", GetType(Integer))
                    Me._DataTray.Columns.Add("Qty_CL", GetType(Integer))

                    Me._DataTrayBinding.DataSource = Me._DataTray
                    Me.grdTray.DataSource = Me._DataTrayBinding
                Else
                    Me._DataTray.Rows.Clear()
                End If

                Dim TotalBarcodeCL, TotalCL As Integer
                For Each Row As DataRow In Me._Data.DefaultView.ToTable(True, "Barcode_Tray").Rows
                    Using DataGroup As DataTable = Me._Data.DefaultView.ToTable(True, "Barcode_Tray", "Barcode_CL")
                        TotalBarcodeCL = DataGroup.Select(String.Format("Barcode_Tray = '{0}'", Row.Item("Barcode_Tray").ToString)).Length
                    End Using

                    TotalCL = Me._Data.Compute("SUM(Total_Qty)", String.Format("Barcode_Tray = '{0}'", Row.Item("Barcode_Tray").ToString))

                    Me._DataTray.Rows.Add(0, Row.Item("Barcode_Tray").ToString, TotalBarcodeCL, TotalCL)
                Next

                Me.txtBarcodeTray.ReadOnly = Not Me._DataTray.Rows.Count > 0
                Me._DataTrayBinding.Sort = ""
            End Using

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListCL(ByVal TrayNo As String)
        Try
            ClearCL()

            If String.IsNullOrEmpty(TrayNo) OrElse Me._Data Is Nothing OrElse Not Me._Data.Rows.Count > 0 Then
                Exit Sub
            End If

            Dim SelectedTray As DataRow() = Me._Data.Select(String.Format("Barcode_Tray = '{0}'", TrayNo))
            If SelectedTray.Length > 0 Then

                Dim arArrdrop() As DataRow
                arArrdrop = Me._Data.Select("OMS_Droppoint_Index = ''")
                If arArrdrop.Length > 0 Then
                    Dim strOrder As String = ""
                    For Each dr As DataRow In arArrdrop
                        strOrder += Chr(13) + dr("SalesOrder_No").ToString
                    Next
                    If W_MSG_Confirm("มีรายการที่ยังไม่มี drop point คุณต้องการแพ็คสินค้าหรือไม่" & strOrder) = Windows.Forms.DialogResult.No Then
                        Exit Sub
                    End If

                End If

                Me._SelectedTray = TrayNo

                If Me._DataCL Is Nothing Then
                    Me._DataCL = Me._Data.Clone

                    Me._DataCLBinding.DataSource = Me._DataCL
                    Me.grdCL.DataSource = Me._DataCLBinding
                Else
                    Me._DataCL.Rows.Clear()
                End If

                For Each Row As DataRow In SelectedTray
                    Me._DataCL.Rows.Add(Row.ItemArray)
                Next

                Me.txtBarcodeCL.ReadOnly = Not Me._DataCL.Rows.Count > 0
                Me._DataCLBinding.Sort = ""
            Else
                Me.txtBarcodeTray.Clear()
                Throw New Exception("ไม่พบ Tray No. ในสาขานี้")
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ScanBarcodeCL(ByVal BarcodeCL As String)
        Try
            If String.IsNullOrEmpty(BarcodeCL) OrElse Me._DataCL Is Nothing OrElse Not Me._DataCL.Rows.Count > 0 Then
                Exit Sub
            End If

            Dim SelectedCL As DataRow() = Me._DataCL.Select(String.Format("Barcode_CL = '{0}'", BarcodeCL))
            If SelectedCL.Length > 0 Then
                Dim IsCompleted As Boolean = Me._DataCL.Select(String.Format("Barcode_CL = '{0}' AND Checked = 0", BarcodeCL)).Length = 0
                If IsCompleted Then
                    Throw New Exception("Barcode นี้ทำการแพคเรียบร้อยแล้ว")
                End If

                Dim ConfirmQty As Long

                For Each Row As DataRow In SelectedCL
                    ConfirmQty = Row.Item("Total_Qty")

                    If Row.Item("IsScanBarcode") = 1 Then
                        Using frm As New frmConfirmBarcodePacking(ConfirmQty, Row.Item("SalesOrder_Index").ToString, Row.Item("SalesOrderItem_Index").ToString, Row.Item("SalesOrder_No").ToString, Row.Item("Sku_Name").ToString, Row.Item("ItemCode_1").ToString, Row.Item("ItemCode_2").ToString, Row.Item("Str10").ToString)
                            frm.ShowDialog()

                            If frm.DialogResult <> Windows.Forms.DialogResult.OK Then
                                Continue For
                            End If

                            If Me._DataB1 Is Nothing Then
                                Me._DataB1 = frm.Data.Copy
                            Else
                                Me._DataB1.Merge(frm.Data.Copy)
                            End If
                        End Using
                    Else
                        Using frm As New frmPopupConfirmQty(ConfirmQty, Row.Item("SalesOrder_No").ToString, Row.Item("Sku_Name").ToString)
                            frm.ShowDialog()

                            If frm.DialogResult <> Windows.Forms.DialogResult.OK Then
                                Continue For
                            End If
                        End Using
                    End If

                    Row.Item("Checked") = 1
                    Me._DataSave.Rows.Add(Row.ItemArray)
                Next

                Me._DataCL.AcceptChanges()
                Dim CheckedCL As Integer = Me._DataCL.Select("Checked = 1").Length

                If CheckedCL > 0 AndAlso String.IsNullOrEmpty(Me._DataCLBinding.Sort) Then
                    Me._DataCLBinding.Sort = "Checked ASC, Barcode_CL ASC, SalesOrder_No ASC"
                End If

                If CheckedCL = Me._DataCL.Rows.Count Then
                    Dim SelectedTray As DataRow() = Me._DataTray.Select(String.Format("Barcode_Tray = '{0}'", Me._SelectedTray))
                    For Each Row As DataRow In SelectedTray
                        Row.Item("Checked") = 1
                    Next

                    Me._DataTray.AcceptChanges()
                    Me._DataTrayBinding.Sort = "Checked ASC, Barcode_Tray ASC"
                End If

                SetBackgroundColor()

                Me.btnSave.Enabled = True
                Me.btnClear.Enabled = True
            Else
                Throw New Exception("ไม่พบ Barcode ใน Tray นี้")
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetBackgroundColor()
        Dim Data As DataRow

        For i As Integer = 0 To Me.grdTray.Rows.Count - 1
            With Me.grdTray.Rows.Item(i)
                Data = .DataBoundItem.Row()

                If Data.Item("Checked") = 1 Then
                    .DefaultCellStyle.BackColor = Color.LightGreen
                Else
                    .DefaultCellStyle.BackColor = Color.White
                End If
            End With
        Next

        For i As Integer = 0 To Me.grdCL.Rows.Count - 1
            With Me.grdCL.Rows.Item(i)
                Data = .DataBoundItem.Row()

                If Data.Item("Checked") = 1 Then
                    .DefaultCellStyle.BackColor = Color.LightGreen
                Else
                    .DefaultCellStyle.BackColor = Color.White
                End If
            End With
        Next
    End Sub

    Private Sub ClearTray()
        Try
            If Me._DataTray IsNot Nothing AndAlso Me._DataTray.Rows.Count > 0 Then
                Me._DataTray.Rows.Clear()
            End If

            Me.txtBarcodeTray.Clear()
            Me.txtBarcodeTray.ReadOnly = True
            ClearCL()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ClearCL()
        Try
            If Me._DataCL IsNot Nothing AndAlso Me._DataCL.Rows.Count > 0 Then
                Me._DataCL.Rows.Clear()
            End If

            Me._SelectedTray = Nothing

            Me.txtBarcodeCL.Clear()
            Me.txtBarcodeCL.ReadOnly = True

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ClearDataSave()
        Try
            If Me._DataSave IsNot Nothing AndAlso Me._DataSave.Rows.Count > 0 Then
                Me._DataSave.Rows.Clear()
            End If

            If Me._DataB1 IsNot Nothing AndAlso Me._DataB1.Rows.Count > 0 Then
                Me._DataB1.Rows.Clear()
            End If

            Me.btnSave.Enabled = False
            Me.btnClear.Enabled = False

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

#End Region

#Region " + Event + "

    Private Sub frmPackingBag_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Initproperty()

            ListCustomerShippingLocation()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        Try
            ListCustomerShippingLocation()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub cboShipping_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboShipping.SelectedIndexChanged
        Try
            If Me.cboShipping.SelectedIndex < 0 OrElse String.IsNullOrEmpty(Me.cboShipping.SelectedValue) Then
                Me._DataShippingBinding.Filter = ""
            Else
                Me._DataShippingBinding.Filter = String.Format("Customer_Shipping_Index = '{0}'", Me.cboShipping.SelectedValue)
            End If

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub grdCustomerShippingLocation_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdShipping.SelectionChanged
        Try
            ListData()

            Me.txtBarcodeTray.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub txtBarcodeTray_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcodeTray.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Try
            If String.IsNullOrEmpty(Me.txtBarcodeTray.Text.Trim) Then
                Throw New Exception("กรุณาระบุข้อมูล Tray No.")
            End If

            ListCL(Me.txtBarcodeTray.Text.Trim)
            Me.txtBarcodeCL.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.txtBarcodeTray.Focus()
        Finally
            Me.txtBarcodeTray.Clear()
        End Try
    End Sub

    Private Sub grdTray_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdTray.SelectionChanged
        Me.grdTray.ClearSelection()

        If String.IsNullOrEmpty(Me._SelectedTray) Then
            Me.txtBarcodeTray.Focus()
        Else
            Me.txtBarcodeCL.Focus()
        End If
    End Sub

    Private Sub txtBarcodeCL_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcodeCL.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Try
            If String.IsNullOrEmpty(Me.txtBarcodeCL.Text.Trim) Then
                Throw New Exception("กรุณาระบุข้อมูล Barcode")
            End If

            ScanBarcodeCL(Me.txtBarcodeCL.Text.Trim)

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        Finally
            Me.txtBarcodeCL.Clear()
            Me.txtBarcodeCL.Focus()
        End Try
    End Sub

    Private Sub grdCL_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdCL.SelectionChanged
        Me.grdCL.ClearSelection()
        Me.txtBarcodeCL.Focus()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If W_MSG_Confirm("ยืนยันการปิดถุง ใช่ หรือ ไม่") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Save()

            ListCustomerShippingLocation()

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

            ListCL(Me._SelectedTray)
            Me.txtBarcodeCL.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

End Class

