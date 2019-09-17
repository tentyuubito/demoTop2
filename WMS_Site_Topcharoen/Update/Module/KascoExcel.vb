Module KascoExcel

    Public Sub ExportToExcel(ByVal dgvData As DataGridView, ByVal headertext As String, Optional ByVal columnstart As Integer = 0, Optional ByVal rowstart As Integer = 0)
        System.Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-US")

        Dim ExcelApp As New Object
        Dim ExcelBooks As New Object
        Dim ExcelSheets As New Object

        Dim misValue As Object = System.Reflection.Missing.Value
        Try

            ExcelApp = CreateObject("Excel.Application")
            If ExcelApp Is Nothing Then Throw New Exception("excel not found")
            ExcelBooks = ExcelApp.Workbooks.Add
            ExcelSheets = ExcelBooks.Worksheets(1)

            Dim dtSource As New DataTable
            If dgvData.DataSource Is Nothing Then
                Throw New Exception("No Data Source")
            Else
                dtSource = DirectCast(dgvData.DataSource, DataTable).Copy
            End If


            'Header Text
            WriteHeaderText(ExcelSheets, rowstart, columnstart, headertext)

            'Column 
            Dim ColumnCount As Integer = WriteColumnStyle(ExcelSheets, rowstart, columnstart, headertext, dgvData.Columns, dtSource)

            Dim rawData(dtSource.Rows.Count, ColumnCount - 1) As Object

            Dim col, row As Integer
            row = 0
            col = 0

            For Each dc As DataGridViewColumn In dgvData.Columns
                If Not dc.Visible OrElse String.IsNullOrEmpty(dc.DataPropertyName) OrElse Not dtSource.Columns.Contains(dc.DataPropertyName) Then
                    Continue For
                End If

                row = 0
                For Each dr As DataRow In dtSource.Rows
                    rawData(row, col) = dr(dc.DataPropertyName).ToString
                    row += 1
                Next
                col += 1
            Next

            Dim excelRange As String = String.Format("A{0}:{1}{2}", rowstart, ExcelColName(ColumnCount), dtSource.Rows.Count + rowstart)

            ExcelSheets.Range(excelRange, Type.Missing).Value2 = rawData

            ExcelApp.Visible = True
            GC.Collect()
        Catch ex As Exception
            Try
                ExcelApp.Quit()
            Catch ex1 As Exception

            End Try
            Throw ex
        Finally
            Dispose(ExcelApp, ExcelBooks, ExcelSheets)
        End Try
    End Sub

    Public Sub WriteHeaderText(ByVal ExcelSheets As Object, ByRef rowstart As Integer, ByRef columnstart As Integer, ByVal headertext As String)
        Try
            Dim iColumnCurrent As Integer

            rowstart += 2
            iColumnCurrent = columnstart
            iColumnCurrent += 1
            With ExcelSheets
                .Cells(rowstart, iColumnCurrent) = headertext
                .Cells(rowstart, iColumnCurrent).ColumnWidth = 13.38
                .Cells(rowstart, iColumnCurrent).Font.Size = 22
            End With
            rowstart += 1

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Function WriteColumnStyle(ByVal ExcelSheets As Object, ByRef rowstart As Integer, ByRef columnstart As Integer, ByVal headertext As String, ByVal dgvc As DataGridViewColumnCollection, ByRef dt As DataTable) As Integer
        Try
            Dim ReturnColumnCount As Integer = 0

            rowstart += 1
            Dim iRowCurrent As Integer = rowstart
            Dim iColumnCurrent As Integer = columnstart
            With ExcelSheets
                For Each dc As DataGridViewColumn In dgvc
                    If Not dc.Visible OrElse String.IsNullOrEmpty(dc.DataPropertyName) OrElse Not dt.Columns.Contains(dc.DataPropertyName) Then
                        If dt.Columns.Contains(dc.DataPropertyName) Then dt.Columns.Remove(dc.DataPropertyName)
                        Continue For
                    End If
                    iColumnCurrent += 1
                    ReturnColumnCount += 1

                    .Cells(iRowCurrent, iColumnCurrent) = dc.HeaderText.ToString
                    .Cells(iRowCurrent, iColumnCurrent).ColumnWidth = 13.38
                    .Cells(iRowCurrent, iColumnCurrent).Borders.LineStyle = 1
                    .Cells(iRowCurrent, iColumnCurrent).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray)

                    Dim icellto As Integer = (dt.Rows.Count + rowstart)

                    Dim strExcelColName As String = ExcelColName(iColumnCurrent)
                    Select Case dt.Columns.Item(dc.DataPropertyName).DataType.Name
                        'dt.Columns(0).GetType.Name
                        Case GetType(String).Name
                            .Range(String.Format("{0}{1}:{0}{2}", strExcelColName, rowstart, icellto)).NumberFormat = "@"
                        Case GetType(Boolean).Name
                        Case GetType(Date).Name
                            .Range(String.Format("{0}{1}:{0}{2}", strExcelColName, rowstart, icellto)).NumberFormat = "dd/MM/yyyy"
                        Case GetType(DateTime).Name
                            .Range(String.Format("{0}{1}:{0}{2}", strExcelColName, rowstart, icellto)).NumberFormat = "dd/MM/yyyy"
                        Case GetType(Decimal).Name
                            .Range(String.Format("{0}{1}:{0}{2}", strExcelColName, rowstart, icellto)).NumberFormat = "#,##0"
                        Case Else
                            .Range(String.Format("{0}{1}:{0}{2}", strExcelColName, rowstart, icellto)).NumberFormat = "#,##0"
                    End Select
                    .Range(String.Format("{0}{1}:{0}{2}", strExcelColName, rowstart, icellto)).Borders.LineStyle = 1
                Next
            End With

            rowstart += 1
            Return ReturnColumnCount

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function ExcelColName(ByVal Col As Integer) As String
        Try
            If Col < 0 And Col > 256 Then
                MsgBox("Invalid Argument", MsgBoxStyle.Critical)
                Return Nothing
                Exit Function
            End If
            Dim i As Int16
            Dim r As Int16
            Dim S As String
            If Col <= 26 Then
                S = Chr(Col + 64)
            Else
                r = Col Mod 26
                i = System.Math.Floor(Col / 26)
                If r = 0 Then
                    r = 26
                    i = i - 1
                End If
                S = Chr(i + 64) & Chr(r + 64)
            End If
            ExcelColName = S
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub releaseObject(ByVal obj As Object)
        Try
            System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
            obj = Nothing
        Catch ex As Exception
            obj = Nothing
        Finally
            GC.Collect()
        End Try
    End Sub

    Public Sub Dispose(ByRef ExcelApp As Object, ByRef ExcelBooks As Object, ByRef ExcelSheets As Object)
        Try
            releaseObject(ExcelSheets)
            releaseObject(ExcelBooks)
            releaseObject(ExcelApp)
            ExcelSheets = Nothing
            ExcelBooks = Nothing
            ExcelApp = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Module
