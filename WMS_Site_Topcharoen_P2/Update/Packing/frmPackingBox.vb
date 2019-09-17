Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmPackingBox

#Region " + Variable + "

    Private _Service As Tb_Packing_TopCharoen

    Private _DataBinding As BindingSource
    Private _Data, _DataCustomerShipping, _DataSave As DataTable

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
            Me._DataBinding = New BindingSource

            With Me.grdData
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
                Throw New Exception("��辺�����ŷ��кѹ�֡")
            End If



            If Me._DataSave.DefaultView.ToTable(True, "Urgent_Id", "droppoint").Rows.Count > 1 Then
                Throw New Exception("�������ö�ѹ�֡�� ���ͧ�ҡ���ʤ�����ǹ��� Drop point �������㹡�������ǡѹ")
            End If


            'check droppoint
            Dim drArr() As DataRow
            drArr = Me._DataSave.Select(String.Format("trim(isnull(droppoint,'')) = ''", ""))
            If drArr.Length > 0 Then
                Throw New Exception("�������ö��㺹��������ͧ�ҡ droppoint ����դ��")
            End If
            drArr = Me._DataSave.Select(String.Format("droppoint <> '{0}'", Me._DataSave.Rows(0).Item("droppoint").ToString))
            If drArr.Length > 0 Then
                Throw New Exception("�������ö��㺹��������ͧ�ҡ droppoint ���ç�ѹ")
            End If
            Dim frmPackSize As New frmPackSize_Popup
            frmPackSize.ShowDialog()
            If frmPackSize.IsConfirm = False Then
                Exit Sub
            End If

            Dim BarcodeBox As String = Me._Service.SavePackingBox(Me._DataSave, frmPackSize.PackSize_Index)
            W_MSG_Information("�ѹ�֡�������������" & vbCrLf & "�Ţ�����ͧ : [ " & BarcodeBox & " ]")

            Dim clsConfig_Report As New WMS_STD_Master.config_Report
            Dim frmReport As New WMS_STD_OUTB_Report.frmReportViewerMain
            Dim oCrystal As New ReportDocument

            'oCrystal = Me._Service.GetReportInfo("rptTransferDocRange", " and Barcode_Box = '" & BarcodeBox & "'")
            'frmReport.CrystalReportViewer1.ReportSource = oCrystal
            'frmReport.ShowDialog()

            oCrystal = Me._Service.GetReportInfo("rptTransferPackingDocRange", " and Barcode_Box = '" & BarcodeBox & "' ")
            frmReport.CrystalReportViewer1.ReportSource = oCrystal
            frmReport.ShowDialog()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListData()
        Try
            ClearDataSave()

            Dim HaveData As Boolean
            Using Data As DataTable = Me._Service.ListPackingBag(Me.txtRoute.Text.Trim, "", "")
                HaveData = Data IsNot Nothing AndAlso Data.Rows.Count > 0
                If Not HaveData Then
                    W_MSG_Error("Data not Found")
                End If

                If Me._Data Is Nothing Then
                    Me._Data = Data.Copy
                    Me._DataSave = Me._Data.Clone

                    Me._DataBinding.DataSource = Me._Data
                    Me.grdData.DataSource = Me._DataBinding
                    Me._DataBinding.Sort = "Checked ASC, Barcode_Bag ASC, Customer_Shipping_Location_Index ASC"
                Else
                    Me._Data.Rows.Clear()
                    If HaveData Then
                        Me._Data.Merge(Data)
                    End If
                End If
            End Using

            'ComboBox CustomerShipping
            If Me._DataCustomerShipping Is Nothing Then
                Me._DataCustomerShipping = New DataTable
                With Me._DataCustomerShipping.Columns
                    .Add("Display", GetType(String))
                    .Add("Value", GetType(String))
                    .Add("Value_Id", GetType(String))
                End With

                With Me.cboCustomerShipping
                    .DisplayMember = "Display"
                    .ValueMember = "Value"
                    .DataSource = Me._DataCustomerShipping
                End With
            Else
                Me._DataCustomerShipping.Rows.Clear()
            End If

            If HaveData Then
                Me._DataCustomerShipping.Rows.Add("������", "")

                Using GroupCustomer As DataTable = Me._Data.DefaultView.ToTable(True, "Shipping", "Company_Name", "Customer_Shipping_Index")
                    Dim arrC() As DataRow = GroupCustomer.Select("", "Shipping")
                    For Each Row As DataRow In arrC 'GroupCustomer.Rows
                        Me._DataCustomerShipping.Rows.Add(Row.Item("Shipping").ToString & "  :  " & Row.Item("Company_Name").ToString, Row.Item("Customer_Shipping_Index").ToString, Row.Item("Shipping").ToString)
                    Next
                End Using

                Me.cboCustomerShipping.SelectedIndex = 0
            Else
                Me.cboCustomerShipping.SelectedIndex = -1
            End If

            SetLabelSum()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ListDataByCustomerShipping()
        If Me._Data Is Nothing OrElse Not Me._Data.Rows.Count > 0 Then
            Exit Sub
        End If

        Try
            ClearDataSave()

            Dim CustomerShippingIndex As String = Me.cboCustomerShipping.SelectedValue
            If String.IsNullOrEmpty(CustomerShippingIndex) Then
                Me._DataBinding.Filter = String.Empty
            Else
                Me._DataBinding.Filter = String.Format(" Customer_Shipping_Index = '{0}' ", CustomerShippingIndex)
            End If

            SetLabelSum()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetLabelSum()
        Try
            'Label Sum
            If Me._DataBinding.Count > 0 Then
                Me.lblSum.Text = "��� : " & FormatNumber(Me._DataBinding.Count, 0) & " ��¡��"
            Else
                Me.lblSum.Text = "��辺��¡�öا"
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ScanBarcodeBag(ByVal BarcodeBag As String)
        Try
            If String.IsNullOrEmpty(BarcodeBag) OrElse Me._Data Is Nothing OrElse Not Me._Data.Rows.Count > 0 Then
                Exit Sub
            End If

            Dim RowFilter As String = String.Format(" Barcode_Bag = '{0}' ", BarcodeBag)
            If Not String.IsNullOrEmpty(Me._DataBinding.Filter) Then
                RowFilter &= " AND " & Me._DataBinding.Filter
            End If

            Dim SelectedBag As DataRow() = Me._Data.Select(RowFilter)
            If SelectedBag.Length > 0 Then
                Dim IsCompleted As Boolean = Me._Data.Select(RowFilter & " AND Checked = 0 ").Length = 0
                If IsCompleted Then
                    Throw New Exception("Barcode Bag ���ӡ��ᾤŧ���º��������")
                End If

                For Each Row As DataRow In SelectedBag
                    Row.Item("Checked") = 1
                    Me._DataSave.Rows.Add(Row.ItemArray)
                Next

                Me._Data.AcceptChanges()
                Dim CheckedCL As Integer = Me._Data.Select("Checked = 1").Length

                'If CheckedCL > 0 AndAlso String.IsNullOrEmpty(Me._DataBinding.Sort) Then
                '    Me._DataBinding.Sort = "Checked ASC, Barcode_Bag ASC, Customer_Shipping_Location_Index ASC"
                'End If

                SetBackgroundColor()

                Me.lblSumScan.Text = "Scan : " & FormatNumber(CheckedCL, 0) & " / " & FormatNumber(Me._Data.Rows.Count, 0) & " ��¡��"

                Me.btnSave.Enabled = True
                Me.btnClear.Enabled = True
            Else
                Throw New Exception("��辺������ Barcode Bag")
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub SetBackgroundColor()
        Dim Data As DataRow

        For i As Integer = 0 To Me.grdData.Rows.Count - 1
            With Me.grdData.Rows.Item(i)
                Data = .DataBoundItem.Row()

                If Data.Item("Checked") = 1 Then
                    .DefaultCellStyle.BackColor = Color.LightGreen
                Else
                    .DefaultCellStyle.BackColor = Color.White
                End If
            End With
        Next
    End Sub

    Private Sub ClearSearchCondition()
        Try
            Me.txtRoute.Clear()

            If Me._DataCustomerShipping IsNot Nothing Then
                Me._DataCustomerShipping.Rows.Clear()
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub ClearDataSave()
        Try
            If Me._DataSave IsNot Nothing AndAlso Me._DataSave.Rows.Count > 0 Then
                Me._DataSave.Rows.Clear()
            End If

            If Me._Data IsNot Nothing AndAlso Me._Data.Rows.Count > 0 Then
                Dim Checked As DataRow() = Me._Data.Select("Checked = 1")
                If Checked.Length > 0 Then
                    For Each Row As DataRow In Checked
                        Row.Item("Checked") = 0
                    Next
                End If
            End If


            Me.lblSumScan.Text = "�������¡�� Scan"

            Me.btnSave.Enabled = False
            Me.btnClear.Enabled = False

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

#End Region

#Region " + Event + "

    Private Sub frmPackingBox_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Initproperty()
            ClearSearchCondition()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.Close()
        End Try
    End Sub

    Private Sub frmPackingBox_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.btnSearch.Focus()
    End Sub

    Private Sub cboCustomerShipping_SelectedValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboCustomerShipping.SelectedValueChanged
        Try
            ListDataByCustomerShipping()

            Me.txtBarcodeBag.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ListData()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub grdData_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdData.SelectionChanged
        Me.grdData.ClearSelection()

        If Not String.IsNullOrEmpty(Me.txtBarcodeBag.Text.Trim) Then
            Me.txtBarcodeBag.SelectAll()
        End If

        Me.txtBarcodeBag.Focus()
    End Sub

    Private Sub txtBarcodeTray_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcodeBag.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Try
            If String.IsNullOrEmpty(Me.txtBarcodeBag.Text.Trim) Then
                Throw New Exception("��س��кآ����� Barcode Bag")
            End If

            ScanBarcodeBag(Me.txtBarcodeBag.Text.Trim)
            Me.txtBarcodeBag.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
            Me.txtBarcodeBag.Focus()
        Finally
            Me.txtBarcodeBag.Clear()
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If W_MSG_Confirm("�׹�ѹ��ûԴ���ͧ �� ���� ���") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Save()

            ListData()

            Me.txtBarcodeBag.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If W_MSG_Confirm("�׹�ѹ��ҧ�����ŷ�� Scan ������") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            ClearDataSave()
            'ListData()
            SetBackgroundColor()
            Me.txtBarcodeBag.Focus()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

#End Region

    Private Sub txtBranch_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBranch.KeyPress
        Try
            If e.KeyChar <> ChrW(13) Then
                Exit Sub
            End If
            Dim dt As New DataTable
            dt = CType(Me.cboCustomerShipping.DataSource, DataTable)
            Dim drArr() As DataRow = dt.Select(String.Format("Value_Id='{0}'", Me.txtBranch.Text))
            If drArr.Length > 0 Then
                Me.cboCustomerShipping.SelectedValue = drArr(0)("Value").ToString
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExportExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcel.Click
        Try

            Dim objExport As New Export_Excel_KC
            Dim clsKsl As New cls_KSL

            Dim ds As New DataSet

            Dim dt As New DataTable
            If grdData.Rows.Count <= 0 Then
                Exit Sub
            End If

            dt = grdData.Rows(0).DataBoundItem.ROW.Table
            Dim dtExport As New DataTable
            dtExport = dt.Copy
            dtExport.Columns("ProductType_Desc").ColumnName = "�������Թ���"
            dtExport.Columns("SO_Type").ColumnName = "������"
            dtExport.Columns("Urgent_Id").ColumnName = "��ǹ"
            dtExport.Columns("Route").ColumnName = "���ö"
            dtExport.Columns("Shipping").ColumnName = "�Ң����"
            dtExport.Columns("Shipping_Location").ColumnName = "�Ң�����"
            dtExport.Columns("Shipping_Desc").ColumnName = "��������´�Ң�"
            dtExport.Columns("Total_Qty").ColumnName = "�ӹǹ"
            dtExport.Columns("Group_Date").ColumnName = "�ѹ���"

            dtExport.Columns.RemoveAt(dtExport.Columns("Checked").Ordinal)

            dtExport.Columns.RemoveAt(dtExport.Columns("Company_Name").Ordinal)

            dtExport.Columns.RemoveAt(dtExport.Columns("Customer_Shipping_Index").Ordinal)

            dtExport.Columns.RemoveAt(dtExport.Columns("Customer_Shipping_Location_Index").Ordinal)

            dtExport.Columns("�������Թ���").SetOrdinal(2)
            dtExport.Columns("������").SetOrdinal(3)
            dtExport.Columns("��ǹ").SetOrdinal(4)
            dtExport.Columns("droppoint").SetOrdinal(5)
            dtExport.Columns("�Ң����").SetOrdinal(6)
            dtExport.Columns("�Ң�����").SetOrdinal(8)
            dtExport.Columns("��������´�Ң�").SetOrdinal(9)
            dtExport.Columns("�ӹǹ").SetOrdinal(9)
            dtExport.Columns("���ö").SetOrdinal(4)
            dtExport.Columns("�ѹ���").SetOrdinal(1)

            '        dt.Columns("�������Թ���").Ordinal = 2


            ds.Tables.Add(dtExport.Copy)
            ds.Tables(0).TableName = Now.ToString("yyyyMMddHHmm")
            objExport.export(ds, "exportExcel")

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class

