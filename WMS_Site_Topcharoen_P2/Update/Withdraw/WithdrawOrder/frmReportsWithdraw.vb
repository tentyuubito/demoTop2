Imports Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module

Public Class frmReportsWithdraw

    Dim ds As DataSet

    Private cnn As New SqlClient.SqlConnection(WV_ConnectionString)

    Private Sub frmReportsWithdraw_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oFunction As New W_Language
        oFunction.SwitchLanguage(Me, 2)
        'oFunction.SW_Language_Column(Me, Me.dgvData, 2)
        oFunction = Nothing

        Me.defaultOnLoad()
        Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getDgvData()
        Try
            Me.ds = Me.getDataSet()
            Me.dgvData.DataSource = Me.ds.Tables(0)
            If (Me.dgvData.Rows.Count() = 0) Then
                W_MSG_Information("ไม่พบข้อมูล")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Function getDataSet() As DataSet
        Dim ds As New DataSet()
        Try
            Dim SheetName As String = "ข้อมูล"
            Dim dt As New System.Data.DataTable
            Dim query As String = " select * from View_Excel_Withdraw_KingStella "
            Dim strWhere As String = ""

            Dim sqlCommand As New SqlCommand
            With sqlCommand
                .Parameters.Clear()
                If (Me.chkWithdraw_Date.Checked) Then
                    strWhere &= String.Format(" {0} cast(Withdraw_Date as date)>=@Withdraw_Date_S and cast(Withdraw_Date as date)<=@Withdraw_Date_E ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@Withdraw_Date_S", SqlDbType.Date).Value = Me.dtpWithdraw_Date_S.Value
                    .Parameters.Add("@Withdraw_Date_E", SqlDbType.Date).Value = Me.dtpWithdraw_Date_E.Value
                End If
                If (Me.chkWithdraw_No.Checked) Then
                    strWhere &= String.Format(" {0} Withdraw_No like @Withdraw_No ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@Withdraw_No", SqlDbType.VarChar, 50).Value = "" & Me.txtWithdraw_No.Text & "%"
                End If
                If (Me.chkRef_No.Checked) Then
                    strWhere &= String.Format(" {0} Ref_No like @Ref_No ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@Ref_No", SqlDbType.VarChar, 50).Value = "%" & Me.txtRef_No.Text & "%"
                End If
                .CommandType = CommandType.Text
                .CommandText = query & strWhere
                .Connection = Me.cnn
                .CommandTimeout = 0
            End With
            Dim da As New SqlClient.SqlDataAdapter(sqlCommand)
            dt.TableName = SheetName
            da.Fill(dt)
            ds.Tables.Add(dt)
        Catch ex As Exception
            Throw ex
        End Try
        Return ds
    End Function

    Private Sub export()
        Try
            Dim saveFileDialog As New SaveFileDialog
            saveFileDialog.Filter = "Excel File|*.xls"
            saveFileDialog.Title = "Save an Excel File"
            saveFileDialog.FileName = Me.Text
            If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Me.exportExcel(saveFileDialog.FileName, Me.ds)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer

    Private Sub exportExcel(ByVal strFileName As String, ByVal dsExport As DataSet)
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

        'start the application
        'Dim xlApp As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
        Dim xlApp As Object = CreateObject("Excel.Application")

        'get the window handle
        Dim xlHWND As Integer = xlApp.Hwnd

        'this will have the process ID after call to GetWindowThreadProcessId
        Dim xlProcessId As Integer = 0

        'get the process ID
        GetWindowThreadProcessId(xlHWND, xlProcessId)

        'get the process
        Dim xlProcess As Process = Process.GetProcessById(xlProcessId)

        Dim isFileOpen As Boolean = False

        'Dim xlWorkBook As Workbook
        'Dim xlWorkSheet As Worksheet
        Dim xlWorkBook As New Object
        Dim xlWorkSheet As New Object
        Try
            If xlApp Is Nothing Then
                W_MSG_Error("Excel is not properly installed")
                Return
            End If
            If IsNumeric(xlApp.Version) AndAlso CDec(xlApp.Version) < 11 Then
                W_MSG_Error("Excel is lower than version 11 (2003)")
                Return
            End If

            Dim misValue As Object = System.Reflection.Missing.Value
            'Dim chartRange As Range
            Dim sheetIndex As Integer = 0

            xlWorkBook = xlApp.Workbooks.Add(misValue)

            ' Copy each DataTable as a new Sheet
            For Each dt As System.Data.DataTable In dsExport.Tables
                Dim Title_Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)
                For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
                    With dt.Columns(iCol)
                        If .ColumnName.Contains("title_") Then
                            Title_Dict.Add(.ColumnName, dt.Rows(0).Item(.ColumnName).ToString())
                            dt.Columns.Remove(dt.Columns(iCol))
                        End If
                    End With
                Next
                sheetIndex += 1

                ' Copy the DataTable to an object array
                Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

                ' Copy the column names to the first row of the object array
                For col As Integer = 0 To dt.Columns.Count - 1
                    rawData(0, col) = dt.Columns(col).ColumnName
                Next

                ' Copy the values to the object array
                For col As Integer = 0 To dt.Columns.Count - 1
                    For row As Integer = 0 To dt.Rows.Count - 1
                        rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
                    Next
                Next

                Dim finalColLetter As String = ColumnIndexToColumnLetter(dt.Columns.Count)

                ' Create a new Sheet
                xlWorkSheet = CType(xlWorkBook.Sheets.Add(xlWorkBook.Sheets(sheetIndex), Type.Missing, 1, XlSheetType.xlWorksheet), Worksheet)

                'xlWorkSheet = xlWorkBook.Sheets(dt.TableName)
                xlWorkSheet.Name = dt.TableName

                ' Format
                'xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).NumberFormat = "@"
                For iCol As Integer = 0 To dt.Columns.Count - 1
                    Select Case dt.Columns(iCol).DataType.Name
                        Case "Boolean", "String"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                        Case "DateTime", "TimeSpan"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "dd/MM/yyyy"
                        Case "Decimal", "Double"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0.000000"
                        Case "Int16", "Int32", "Int64"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0"
                        Case Else
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                    End Select
                Next
                'xlWorkSheet.Range("B:B").NumberFormat = "dd/MM/yyyy"
                'xlWorkSheet.Range("C:D").NumberFormat = "@"
                'xlWorkSheet.Range("E:F").NumberFormat = "#,##0"
                'xlWorkSheet.Range("G:H").NumberFormat = "@"
                'xlWorkSheet.Range("I:K").NumberFormat = "#,##0.00"
                'xlWorkSheet.Range("L:L").NumberFormat = "@"

                ' data export to Excel
                Dim iStartRows As Integer = 1
                Dim excelRange As String = String.Format("A{0}:{1}{2}", iStartRows, finalColLetter, iStartRows + dt.Rows.Count)
                xlWorkSheet.Range(excelRange, Type.Missing).Value2 = rawData

                'Dim headerList As New List(Of String)
                '

                'For iHeader As Integer = 1 To headerList.Count
                '    xlWorkSheet.Cells(1, iHeader) = headerList.Item(iHeader - 1)
                'Next

                'For iCols As Integer = 1 To 18
                '    ' Merge Cell
                '    xlWorkSheet.Range(String.Format("{0}1:{0}3", ColumnIndexToColumnLetter(iCols))).Merge()
                'Next
                'xlWorkSheet.Range(String.Format("{0}1:{1}1", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(24))).Merge()
                'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(22))).Merge()
                'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(23), ColumnIndexToColumnLetter(24))).Merge()

                ' Font Bold
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Bold = True

                ' Align
                xlWorkSheet.Range("A:AU").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("B:B").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("C:D").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("E:F").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("G:H").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("I:K").HorizontalAlignment = XlHAlign.xlHAlignRight
                'xlWorkSheet.Range("L:L").HorizontalAlignment = XlHAlign.xlHAlignLeft

                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).VerticalAlignment = XlVAlign.xlVAlignCenter
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).HorizontalAlignment = XlHAlign.xlHAlignCenter

                ' Fill Color
                'xlWorkSheet.Range(String.Format("A1:{0}3", finalColLetter)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray)
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Interior.ColorIndex = 56

                ' Font Color
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

                ' Border Cell
                xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic)
                With xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).Borders(XlBordersIndex.xlInsideVertical)
                    .LineStyle = XlLineStyle.xlContinuous
                    .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = XlBorderWeight.xlThin
                End With
                With xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Borders(XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = XlLineStyle.xlContinuous
                    .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = XlBorderWeight.xlThin
                End With
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic)

                '' Read Data
                'Dim strCell As String = ""
                'Dim iCellTemp As Integer = 0
                'Dim strCellTemp As String = ""
                'Dim flag As Boolean = False
                'For iRows As Integer = iStartRows + 1 To iStartRows + dt.Rows.Count + 1
                '    strCell = xlWorkSheet.Cells(iRows, 1).value
                '    If iCellTemp = 0 Then
                '        iCellTemp = iRows
                '        strCellTemp = strCell
                '    End If
                '    If strCell <> strCellTemp Then
                '        If flag Then
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Transparent)
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 15
                '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
                '                .LineStyle = XlLineStyle.xlContinuous
                '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                '                .TintAndShade = 0
                '                .Weight = XlBorderWeight.xlThin
                '            End With
                '        Else
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver)
                '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 2
                '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
                '                .LineStyle = XlLineStyle.xlContinuous
                '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
                '                .TintAndShade = 0
                '                .Weight = XlBorderWeight.xlThin
                '            End With
                '        End If
                '        flag = Not flag
                '        iCellTemp = iRows
                '        strCellTemp = strCell
                '    End If
                'Next

                ' Set Font
                xlWorkSheet.Cells.Font.Name = "Tahoma"
                xlWorkSheet.Cells.Font.Size = 10
                'xlWorkSheet.Rows.RowHeight = 18
                xlWorkSheet.Cells.WrapText = False

                ' Set Column Autosize
                xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).EntireColumn.AutoFit()
                'xlWorkSheet.Range("D:D").EntireColumn.ColumnWidth = 20
                'xlWorkSheet.Range("E:F").EntireColumn.ColumnWidth = 10
                'xlWorkSheet.Range("G:G").EntireColumn.ColumnWidth = 50
                'xlWorkSheet.Range("H:H").EntireColumn.ColumnWidth = 10
                'xlWorkSheet.Range("I:K").EntireColumn.ColumnWidth = 12
                'xlWorkSheet.Range("L:L").EntireColumn.ColumnWidth = 20

                xlWorkSheet = Nothing
            Next

            '--------------------------------------------------------

            'Dim strFileName As String = fileName
            'Dim isFileOpen As Boolean = False
            Try
                Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
                fileTemp.Close()
            Catch ex As Exception
                isFileOpen = True
                MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            If Not isFileOpen Then
                If System.IO.File.Exists(strFileName) Then
                    System.IO.File.Delete(strFileName)
                End If
                xlWorkBook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
            End If
            isFileOpen = True
            xlApp.Visible = True

        Catch ex As Exception
            If Not xlProcess.HasExited Then
                xlProcess.Kill()
            End If
            MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")
            If Not isFileOpen AndAlso Not xlProcess.HasExited Then
                xlProcess.Kill()
            End If
            ReleaseObject(xlApp)
            ReleaseObject(xlWorkBook)
            ReleaseObject(xlWorkSheet)
        End Try
    End Sub

    Private Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
        Dim div As Integer = colIndex
        Dim colLetter As String = String.Empty
        Dim modnum As Integer = 0

        While div > 0
            modnum = (div - 1) Mod 26
            colLetter = Chr(65 + modnum) & colLetter
            div = CInt((div - modnum) \ 26)
        End While

        Return colLetter
    End Function

    Private Sub ReleaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Private Sub defaultOnLoad()
        Me.dtpWithdraw_Date_S.Value = New Date(Today.Year, Today.Month, Today.Day)
        Me.dtpWithdraw_Date_E.Value = New Date(Today.Year, Today.Month, Today.Day)
        Me.resetCondition()
    End Sub

    Private Sub resetCondition()
        Me.dtpWithdraw_Date_S.Enabled = Me.chkWithdraw_Date.Checked
        Me.dtpWithdraw_Date_E.Enabled = Me.chkWithdraw_Date.Checked
        Me.txtWithdraw_No.Enabled = Me.chkWithdraw_No.Checked
        Me.txtWithdraw_No.Clear()
        Me.txtRef_No.Enabled = Me.chkRef_No.Checked
        Me.txtRef_No.Clear()
    End Sub

    Private Sub chkWithdraw_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWithdraw_Date.CheckedChanged
        Me.dtpWithdraw_Date_S.Enabled = Me.chkWithdraw_Date.Checked
        Me.dtpWithdraw_Date_E.Enabled = Me.chkWithdraw_Date.Checked
    End Sub

    Private Sub chkWithdraw_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkWithdraw_No.CheckedChanged
        Me.txtWithdraw_No.Enabled = Me.chkWithdraw_No.Checked
        If Me.chkWithdraw_No.Checked Then
            Me.txtWithdraw_No.Focus()
        Else
            Me.txtWithdraw_No.Clear()
        End If
    End Sub

    Private Sub chkRef_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkRef_No.CheckedChanged
        Me.txtRef_No.Enabled = Me.chkRef_No.Checked
        If Me.chkRef_No.Checked Then
            Me.txtRef_No.Focus()
        Else
            Me.txtRef_No.Clear()
        End If
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If (Me.chkWithdraw_Date.Checked AndAlso Me.dtpWithdraw_Date_S.Value > Me.dtpWithdraw_Date_E.Value) Then
                W_MSG_Information(String.Format("{0}เริ่มต้นมีค่ามากกว่าสิ้นสุด", Me.chkWithdraw_Date.Text))
                Exit Sub
            End If
            If (Me.chkWithdraw_No.Checked AndAlso Me.txtWithdraw_No.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkWithdraw_No.Text))
                Exit Sub
            End If
            If (Me.chkRef_No.Checked AndAlso Me.txtRef_No.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkRef_No.Text))
                Exit Sub
            End If
            Me.getDgvData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try
            If (Me.dgvData.Rows.Count = 0) Then
                W_MSG_Information(String.Format("ไม่พบข้อมูล"))
                Exit Sub
            End If
            Me.export()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

End Class