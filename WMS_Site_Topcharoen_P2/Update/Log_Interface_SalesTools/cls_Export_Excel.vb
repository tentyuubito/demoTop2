Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports System.Configuration.ConfigurationSettings
Imports Microsoft.Office.Interop
Imports WMS_STD_Master.W_Language

Public Class cls_Export_Excel

    Private Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer

    Public Sub export(ByVal ds As DataSet, ByVal FileName As String)
        Try
            Dim saveFileDialog As New SaveFileDialog
            saveFileDialog.Filter = "Excel File|*.xls"
            saveFileDialog.Title = "Save an Excel File"
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            saveFileDialog.FileName = FileName
            If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                Me.exportExcel(saveFileDialog.FileName, ds)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function DataGridViewToDataTable(ByVal dtg As DataGridView, Optional ByVal DataTableName As String = "myDataTable") As System.Data.DataTable
        Try
            Dim dt As New System.Data.DataTable(DataTableName)
            Dim row As DataRow
            Dim TotalDatagridviewColumns As Integer = dtg.ColumnCount - 1
            Dim visibleList As New List(Of String)
            'Add Datacolumn
            For Each c As DataGridViewColumn In dtg.Columns
                If c.Visible Then
                    visibleList.Add(c.HeaderText)
                End If
                Dim idColumn As DataColumn = New DataColumn()
                idColumn.ColumnName = c.HeaderText
                If (Not c.ValueType Is Nothing) Then
                    idColumn.DataType = c.ValueType
                End If
                dt.Columns.Add(idColumn)
            Next
            'Now Iterate thru Datagrid and create the data row
            For Each dr As DataGridViewRow In dtg.Rows
                'Iterate thru datagrid
                row = dt.NewRow 'Create new row
                'Iterate thru Column 1 up to the total number of columns
                For cn As Integer = 0 To TotalDatagridviewColumns
                    row.Item(cn) = IfNullObj(dr.Cells(cn).Value) ' This Will handle error datagridviewcell on NULL Values
                Next
                'Now add the row to Datarow Collection
                dt.Rows.Add(row)
            Next
            For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
                If Not visibleList.Contains(dt.Columns(iCol).ColumnName) Then
                    dt.Columns.Remove(dt.Columns(iCol))
                End If
            Next
            'Now return the data table
            Return dt
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
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
                xlWorkSheet = CType(xlWorkBook.Sheets.Add(xlWorkBook.Sheets(sheetIndex), Type.Missing, 1, Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet), Microsoft.Office.Interop.Excel.Worksheet)

                'xlWorkSheet = xlWorkBook.Sheets(dt.TableName)
                xlWorkSheet.Name = dt.TableName

                ' Format
                'xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).NumberFormat = "@"
                For iCol As Integer = 0 To dt.Columns.Count - 1
                    Select Case dt.Columns(iCol).DataType.Name
                        Case "Boolean", "String"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
                        Case "DateTime", "TimeSpan"
                            xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "dd/MM/yyyy HH:mm"
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
                xlWorkSheet.Range("A:AU").HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("B:B").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("C:D").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("E:F").HorizontalAlignment = XlHAlign.xlHAlignCenter
                'xlWorkSheet.Range("G:H").HorizontalAlignment = XlHAlign.xlHAlignLeft
                'xlWorkSheet.Range("I:K").HorizontalAlignment = XlHAlign.xlHAlignRight
                'xlWorkSheet.Range("L:L").HorizontalAlignment = XlHAlign.xlHAlignLeft

                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).VerticalAlignment = Microsoft.Office.Interop.Excel.XlVAlign.xlVAlignCenter
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter

                ' Fill Color
                xlWorkSheet.Range(String.Format("A1:{0}1", finalColLetter)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)
                ' xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Interior.ColorIndex = 56
                For iCol As Integer = 0 To dt.Rows.Count - 1
                    Select Case dt.Rows(iCol).Item("Status").ToString
                        Case "E"
                            xlWorkSheet.Range(String.Format("B{0}", iCol + 2)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
                        Case Else

                    End Select
                Next
                'xlWorkSheet.Range(String.Format("B2,B7")).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
                'xlWorkSheet.Range(String.Format("B9")).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red)
                ' Font Color
                'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

                ' Border Cell
                xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic)
                With xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideVertical)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                End With
                With xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Borders(Microsoft.Office.Interop.Excel.XlBordersIndex.xlInsideHorizontal)
                    .LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous
                    .ColorIndex = Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic
                    .TintAndShade = 0
                    .Weight = Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin
                End With
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).BorderAround(Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous, Microsoft.Office.Interop.Excel.XlBorderWeight.xlThin, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic, Microsoft.Office.Interop.Excel.XlColorIndex.xlColorIndexAutomatic)

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
                xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Size = 14
                'xlWorkSheet.Rows.RowHeight = 18
                xlWorkSheet.Cells.WrapText = False

                ' Set Column Autosize
                xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).EntireColumn.AutoFit()
                xlWorkSheet.Range("F:F").EntireColumn.ColumnWidth = 50
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
                xlWorkBook.SaveAs(strFileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
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

    Private Sub ExportToExcel(ByVal grdExport As DataGridView)
        Try
            Dim i As Integer = 0
            Dim j As Integer = 2
            Dim ExcelApp As Excel.Application
            Dim ExcelBooks As Excel.Workbook
            Dim ExcelSheets As Excel.Worksheet
            ExcelApp = New Excel.Application

            Dim CurrentThread As System.Threading.Thread
            CurrentThread = System.Threading.Thread.CurrentThread
            'CurrentThread.CurrentCulture = New CultureInfo("en-US")
            CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")

            ExcelApp.Visible = True
            ExcelBooks = ExcelApp.Workbooks.Add()
            ExcelSheets = DirectCast(ExcelBooks.Worksheets(1), Excel.Worksheet)

            i = 0
            j = 2

            With ExcelSheets
                .Columns().ColumnWidth = 22


                .Range("D" & j.ToString()).Value = "รายงานสรุปสินค้า คงเหลือ"
                .Range("D" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("D" & j.ToString()).Font.Bold = True
                .Range("D" & j.ToString()).Font.Size = 14
                '.Range("A1").Interior.Color = RGB(224, 224, 224)

                j += 1

                '.Range("B" & j.ToString()).Value = chkCustomer.Text & " : " & txtCustomer_Name.Text.ToString
                .Range("B" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("B" & j.ToString()).Font.Bold = True
                .Range("B" & j.ToString()).Font.Size = 14
                '.Range("A2").Interior.Color = RGB(224, 224, 224)
                j += 1
                Dim Col As Integer = 0
                Dim strCol As String = "A"
                Dim iChar As Integer = 65
                Dim iTwoChar As Integer = 65
                For Col = 0 To grdExport.ColumnCount - 1
                    If iTwoChar > 90 Then 'เกิน Z
                        strCol = "A" & Chr(iChar)
                    Else
                        strCol = Chr(iChar)
                    End If

                    If iTwoChar = 90 And iChar = 90 Then
                        iChar = 65
                    End If
                    If grdExport.Columns(Col).Visible = False Then Continue For
                    .Range(strCol & j.ToString()).Value = grdExport.Columns(Col).HeaderText
                    .Range(strCol & j.ToString()).Font.Color = RGB(0, 0, 0)
                    .Range(strCol & j.ToString()).Font.Size = 9
                    .Range(strCol & j.ToString()).Font.Bold = True
                    .Range(strCol & j.ToString()).Interior.Color = RGB(192, 192, 192)
                    iChar += 1
                    iTwoChar += 1
                Next
                j += 1
                Dim dtgrdExport As New System.Data.DataTable
                dtgrdExport = grdExport.DataSource
                Dim Row As Integer = 0
                For Row = 0 To grdExport.RowCount - 1
                    strCol = "A"
                    iChar = 65
                    iTwoChar = 65
                    Col = 0
                    For Col = 0 To grdExport.ColumnCount - 1
                        If iTwoChar > 90 Then 'เกิน Z
                            strCol = "A" & Chr(iChar)
                        Else
                            strCol = Chr(iChar)
                        End If
                        If iTwoChar = 90 And iChar = 90 Then
                            iChar = 65
                        End If

                        If grdExport.Columns(Col).Visible = False Then Continue For
                        Dim strData As String = ""
                        If grdExport.Rows(Row).Cells(Col).Value IsNot Nothing Then
                            strData = grdExport.Rows(Row).Cells(Col).Value.ToString
                        Else
                            strData = ""
                        End If

                        Select Case strCol
                            Case "A", "B", "C", "D", "E", "H", "K"
                                .Range(strCol & j.ToString()).Value = "'" & strData
                                .Range(strCol & j.ToString()).Font.Size = 9
                            Case Else
                                .Range(strCol & j.ToString()).Value = strData
                                .Range(strCol & j.ToString()).Font.Size = 9
                        End Select
                        iChar += 1
                        iTwoChar += 1
                    Next
                    j += 1
                Next
            End With
            ExcelSheets = Nothing
            ExcelBooks = Nothing
            ExcelApp = Nothing
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Function IfNullObj(ByVal o As Object, Optional ByVal DefaultValue As String = "") As String
        Dim ret As String = ""
        Try
            If o Is DBNull.Value Then
                ret = DefaultValue
            Else
                ret = o.ToString
            End If
            Return ret
        Catch ex As Exception
            Return ret
        End Try
    End Function
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

End Class
