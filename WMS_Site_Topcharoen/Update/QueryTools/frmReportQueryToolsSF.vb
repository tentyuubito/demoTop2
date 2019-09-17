Imports System.Globalization
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Public Class frmReportQueryToolsSF

    Private _Report_Index As String
    Private _Report_View As String
    Private _Data As DataTable

    Private _Service As cls_SP_Report
    Private _dt As DataTable
    Private _Condition1 As DataTable
    Private _Condition2 As DataTable

    Private Sub LoadCondition(ByVal pDataTable As DataTable, ByVal pOperationType As Integer)
        Try
            pDataTable.Rows.Clear()

            Dim DataCondiotion As DataTable = Me._Service.getConditionSP_Report(pOperationType)
            For Each Row As DataRow In DataCondiotion.Rows
                pDataTable.Rows.Add(Row.Item("operation").ToString, Row.Item("operation").ToString)
            Next

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub frmReportQueryToolsSF_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me._Service = New cls_SP_Report

            Me._Data = New DataTable
            With Me._Data.Columns
                .Add("Select", GetType(Boolean))
                .Add("Condition_1")
                .Add("Col_NameWhere")
                .Add("Column_Name")
                .Add("Condition_2")
                .Add("Value_1")
                .Add("Value_2")
                .Add("View_Name")
            End With

            Me._Condition1 = New DataTable
            With Me._Condition1.Columns
                .Add("Display")
                .Add("Value")
            End With

            LoadCondition(Me._Condition1, 1)

            With col_and_or
                .DisplayMember = "Display"
                .ValueMember = "Value"
                .DataSource = Me._Condition1
            End With

            Me._Condition2 = New DataTable
            With Me._Condition2.Columns
                .Add("Display")
                .Add("Value")
            End With

            LoadCondition(Me._Condition2, 2)

            With col_condition
                .DisplayMember = "Display"
                .ValueMember = "Value"
                .DataSource = Me._Condition2
            End With

            Me.grdSearchCondition.AutoGenerateColumns = False
            Me.grdSearchCondition.AllowUserToAddRows = False
            Me.grdSearchCondition.DataSource = Me._Data

        Catch ex As Exception
            MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub btnPopupSelectReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopupSelectReport.Click
        Try
            Dim frm As New frmSelectReport
            frm.ShowDialog()
            If frm.CloseType = False Then
                Exit Sub
            End If
            txtReportid.Text = frm.report_id
            txtReportname.Text = frm.report_name
            _Report_Index = frm.Report_Index
            _Report_View = frm.Report_View
            If _Report_Index <> "" Then
                getConditionReport(_Report_Index)
            End If
            _dt = Nothing
            grdData.DataSource = Nothing
            grdData.Refresh()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub getConditionReport(ByVal Report_Index As String)
        Try
            Dim Data As New DataTable
            Dim NewRow As DataRow
            _Data.Rows.Clear()
            Data = Me._Service.getView_SP_Report(Report_Index)
            For Each Row As DataRow In Data.Rows
                NewRow = Me._Data.NewRow
                NewRow.Item("Select") = False

                If Me._Condition1.Select(String.Format("Value = '{0}'", Row.Item("SP_Report_Condition_Default_Condition").ToString)).Length > 0 Then
                    NewRow.Item("Condition_1") = Row.Item("SP_Report_Condition_Default_Condition").ToString
                End If
                'col_name_where
                NewRow.Item("Col_NameWhere") = Row.Item("SP_Report_Condition_Name").ToString()
                NewRow.Item("Column_Name") = Row.Item("SP_Report_Condition_Description").ToString()

                If Me._Condition2.Select(String.Format("Value = '{0}'", Row.Item("SP_Report_Condition_Default_Operator").ToString)).Length > 0 Then
                    NewRow.Item("Condition_2") = Row.Item("SP_Report_Condition_Default_Operator").ToString
                End If


                NewRow.Item("Value_1") = Row.Item("SP_Report_Condition_Default_Value1").ToString
                NewRow.Item("Value_2") = Row.Item("SP_Report_Condition_Default_Value2").ToString
                NewRow.Item("View_Name") = Row.Item("SP_Report_View_Name").ToString

                Me._Data.Rows.Add(NewRow)
            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnRpt_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRpt.Click
        Try
            Dim Condition As String = ""
            Dim oBj As New cls_SP_Report

            If (Not _Report_View = Nothing) Then
                'Dim drGenTag() As DataRow
                'drGenTag = CType(Me.grdSearchCondition.DataSource, DataTable).Select("Select = 1")
                'If drGenTag.Length <= 0 Then
                '    MessageBox.Show("กรุณาเลือกรายการ", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                '    grdData.DataSource = Nothing
                '    _dt.Rows.Clear()
                '    Exit Sub
                'End If

                Condition = " SELECT * FROM " & _Report_View 'grdSearchCondition.Rows(0).Cells("col_SP_Report_View_Name").Value.ToString
                Condition &= " WHERE 1=1 "
                For i As Integer = 0 To grdSearchCondition.RowCount - 1
                    If grdSearchCondition.Rows(i).Cells("col_select").Value = "1" Then
                        Condition &= " " & grdSearchCondition.Rows(i).Cells("col_and_or").Value.ToString
                        Condition &= " " & grdSearchCondition.Rows(i).Cells("col_name_where").Value.ToString
                        Condition &= " " & grdSearchCondition.Rows(i).Cells("col_condition").Value.ToString
                        If grdSearchCondition.Rows(i).Cells("col_condition").Value.ToString = "Like" Then
                            Condition &= " '%" & grdSearchCondition.Rows(i).Cells("col_Data1").Value.ToString & "%'"
                        Else
                            Condition &= " '" & grdSearchCondition.Rows(i).Cells("col_Data1").Value.ToString & "'"
                        End If
                        If grdSearchCondition.Rows(i).Cells("col_condition").Value.ToString = "Between" Then
                            Condition &= " AND '" & grdSearchCondition.Rows(i).Cells("colData2").Value.ToString & "'"
                        End If
                    End If
                Next
                _dt = oBj.getData_Report(Condition)

                Me.lblCountRows.Text = FormatNumber(_dt.Rows.Count, 0) & " รายการ "


                If _dt.Rows.Count <= 0 Then
                    MessageBox.Show("ไม่พบข้อมูลที่ค้นหา", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    grdData.DataSource = Nothing
                    _dt = Nothing
                    Exit Sub
                Else
                    grdData.DataSource = _dt
                End If

                

            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExcel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExcel.Click
        Try
            If _dt Is Nothing Then
                MessageBox.Show("ไม่พบรายการ", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) 'W_MSG_Information("ไม่พบรายการสินค้า")
                Exit Sub
            End If
            If _dt.Rows.Count > 0 Then
                Dim ds As New DataSet
                Dim objExport As New Export_Excel_KC
                ds.Tables.Add(_dt)
                ds.Tables(0).TableName = Now.ToString("yyyyMMddHHmm")
                objExport.export(ds, "ExportExcel")

                'ExportToExcel(grdData)
                'ExportExcel(_dt)
                MessageBox.Show("เสร็จสิ้น", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("ไม่พบรายการ", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Exclamation) 'W_MSG_Information("ไม่พบรายการสินค้า")
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub WriteExcelSheet(ByVal ExcelSheet As Object, ByVal ColumnChar As String, ByVal CellValue As String, Optional ByVal FontColorRGB As Integer = 0, Optional ByVal Bold As Boolean = True, Optional ByVal Size As Integer = 14, Optional ByVal BackColorRGB As Integer = -1, Optional ByVal NumberFormat As String = "")
        Try
            With ExcelSheet.Range(ColumnChar)
                .Value = CellValue
                .Font.Bold = Bold
                .Font.Size = Size
                .Font.Color = FontColorRGB

                If Not BackColorRGB < 0 Then
                    .Interior.Color = BackColorRGB
                End If

                If Not String.IsNullOrEmpty(NumberFormat) Then
                    .NumberFormat = NumberFormat
                End If
            End With

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Function GetColumnChar(ByVal ColumnIndex As Integer) As String
        Dim ColumnChar As String = String.Empty
        Dim [Div] As Integer = ColumnIndex
        Dim [Mod] As Integer = 0

        'Char [65 = 'A'], [90 = 'Z']
        'Char ['A'] To ['Z'] = 26
        While [Div] > 0
            [Mod] = ([Div] - 1) Mod 26
            ColumnChar = Chr(65 + [Mod]) & ColumnChar
            [Div] = CInt(([Div] - [Mod]) \ 26)
        End While

        Return ColumnChar
    End Function

    Private Sub ExportExcel(ByVal Data As DataTable)
        Try
            Cursor = Cursors.WaitCursor

            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")
            Dim ExcelApp, ExcelBooks, ExcelSheet As Object
            Dim RowIndex As Integer = 0

            ExcelApp = CreateObject("Excel.Application")
            If ExcelApp Is Nothing Then
                Throw New Exception("Excel is not Properly Installed")
            End If
            If IsNumeric(ExcelApp.Version) AndAlso CDec(ExcelApp.Version) < 11 Then
                Throw New Exception("Excel is Lower than Version 11 (2003)")
            End If

            ExcelBooks = ExcelApp.Workbooks.Add

            For i As Integer = ExcelBooks.Worksheets.Count To 2 Step -1
                ExcelBooks.Worksheets(i).Delete()
            Next

            ExcelSheet = ExcelBooks.Worksheets(1)
            ExcelSheet.Columns().ColumnWidth = 22
            ExcelSheet.Name = Me.txtReportname.Text.Trim

            'Write Header Excel
            RowIndex += 1
            WriteExcelSheet(ExcelSheet, String.Format("A{0}", RowIndex), Me.txtReportname.Text.Trim)

            'Get DataArray
            Dim DataBinding(Data.Rows.Count, Data.Columns.Count - 1) As Object
            For Each Column As DataColumn In Data.Columns
                Select Case Column.DataType.Name.ToUpper
                    Case "DATETIME", "TIMESTAMP"
                        ExcelSheet.Columns.Item(Data.Columns.IndexOf(Column) + 1).NumberFormat = "dd/MM/yyyy HH:mm:ss"

                    Case "DECIMAL", "DOUBLE"
                        ExcelSheet.Columns.Item(Data.Columns.IndexOf(Column) + 1).NumberFormat = "#,##0.000000"

                    Case "INT16", "INT32", "INT64", "INTEGER"
                        ExcelSheet.Columns.Item(Data.Columns.IndexOf(Column) + 1).NumberFormat = "#,##0"

                    Case Else
                        ExcelSheet.Columns.Item(Data.Columns.IndexOf(Column) + 1).NumberFormat = "@"
                End Select

                DataBinding(0, Data.Columns.IndexOf(Column)) = Column.ColumnName
            Next
            For Each Row As DataRow In Data.Rows
                For Each Column As DataColumn In Data.Columns
                    DataBinding(Data.Rows.IndexOf(Row) + 1, Data.Columns.IndexOf(Column)) = Row.Item(Column)
                Next
            Next

            'Binding Data
            RowIndex += 2
            ExcelSheet.Range(String.Format("A{0}:{1}{2}", RowIndex, GetColumnChar(Data.Columns.Count), RowIndex + Data.Rows.Count), Type.Missing).Value2 = DataBinding

            'Set Font, Alignment Header Column
            With ExcelSheet.Range(String.Format("A{0}:{1}{2}", RowIndex, GetColumnChar(Data.Columns.Count), RowIndex), Type.Missing)
                .VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                .HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter
                .Font.Bold = True
                .Font.Size = 14
                .Interior.Color = RGB(192, 192, 192)
            End With

            ExcelApp.Visible = True

        Catch Ex As Exception
            Throw Ex
        Finally
            Cursor = Cursors.Default
        End Try
    End Sub

    Private Sub grdSearchCondition_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSearchCondition.CellClick
        Try
            If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then
                Exit Sub
            End If

            If Not Me.grdSearchCondition.Columns.Item(e.ColumnIndex).Name.Equals("Col_Add") Then
                Exit Sub
            End If

            Dim NewRow As DataRow = Me._Data.NewRow
            For Each Column As DataColumn In Me._Data.Columns
                NewRow.Item(Column) = Me._Data.Rows.Item(e.RowIndex).Item(Column)
            Next

            Me._Data.Rows.InsertAt(NewRow, e.RowIndex + 1)

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Private Sub btnOut_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOut.Click
        Me.Close()
    End Sub

End Class