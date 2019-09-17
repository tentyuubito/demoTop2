Imports Microsoft.Office.Interop.Excel
Imports System.Data.SqlClient
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module

Public Class frmReportsPO

    Dim ds As DataSet

    Private cnn As New SqlClient.SqlConnection(WV_ConnectionString)

    Private Sub frmReportsPO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oFunction As New W_Language
        oFunction.SwitchLanguage(Me, 9)
        'oFunction.SW_Language_Column(Me, Me.dgvData, 9)
        oFunction = Nothing

        Try
            Me.defaultOnLoad()
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
            Dim SheetName As String = Now.ToString("yyyyMMdd_HHmm")
            Dim dt As New System.Data.DataTable
            Dim query As String = " select * from View_Excel_PO_KingStella "
            Dim strWhere As String = ""

            Dim sqlCommand As New SqlCommand
            With sqlCommand
                .Parameters.Clear()
                If (Me.chkPO_Date.Checked) Then
                    strWhere &= String.Format(" {0} cast(PO_Date as date)>=@PO_Date_S and cast(PO_Date as date)<=@PO_Date_E ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@PO_Date_S", SqlDbType.Date).Value = Me.dtpPO_Date_S.Value
                    .Parameters.Add("@PO_Date_E", SqlDbType.Date).Value = Me.dtpPO_Date_E.Value
                End If
                If (Me.chkCustomer.Checked) Then
                    strWhere &= String.Format(" {0} Customer_Id like @Customer_Id ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@Customer_Id", SqlDbType.VarChar, 50).Value = "" & Me.txtCustomer.Text.Trim() & "%"
                End If
                If (Me.chkStatus.Checked And Not Me.cboStatus.SelectedValue = Nothing) Then
                    strWhere &= String.Format(" {0} Status = @Status ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@Status", SqlDbType.Int).Value = Me.cboStatus.SelectedValue
                End If
                If (Me.chkProductType.Checked) Then
                    Dim arrProductType() As String = txtProductType.Text.Split(vbNewLine)
                    Dim ProductTypeList As New List(Of String)
                    For Each str As String In arrProductType
                        If (Not str.Trim() = Nothing) Then
                            ProductTypeList.Add(str.Trim())
                        End If
                    Next
                    If (ProductTypeList.Count = 1) Then
                        strWhere &= String.Format(" {0} ProductType_Id like @ProductType_Id ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@ProductType_Id", SqlDbType.VarChar, 50).Value = "%" & Me.txtProductType.Text.Trim() & "%"
                    ElseIf (ProductTypeList.Count > 1) Then
                        Dim str As String = String.Join("','", ProductTypeList.ToArray())
                        strWhere &= String.Format(" {0} ProductType_Id in ('{1}') ", IIf(strWhere.Trim() = Nothing, "where", "and"), str)
                    End If
                End If
                'If (Me.chkProductType.Checked And Not Me.cboProductType.SelectedValue = Nothing) Then
                '    strWhere &= String.Format(" {0} ProductType_Index like @ProductType_Index ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                '    .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = Me.cboProductType.SelectedValue
                'End If
                If (Me.chkSku.Checked) Then
                    Dim arrSku() As String = txtSku.Text.Split(vbNewLine)
                    Dim skuList As New List(Of String)
                    For Each str As String In arrSku
                        If (Not str.Trim() = Nothing) Then
                            skuList.Add(str.Trim())
                        End If
                    Next
                    If (skuList.Count = 1) Then
                        strWhere &= String.Format(" {0} Sku_Id like @Sku_Id ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@Sku_Id", SqlDbType.VarChar, 50).Value = "%" & Me.txtSku.Text.Trim() & "%"
                    ElseIf (skuList.Count > 1) Then
                        Dim str As String = String.Join("','", skuList.ToArray())
                        strWhere &= String.Format(" {0} Sku_Id in ('{1}') ", IIf(strWhere.Trim() = Nothing, "where", "and"), str)
                    End If
                End If
                If (Me.chkSku_Cust.Checked) Then
                    Dim arrSku_Cust() As String = txtSku_Cust.Text.Split(vbNewLine)
                    Dim sku_CustList As New List(Of String)
                    For Each str As String In arrSku_Cust
                        If (Not str.Trim() = Nothing) Then
                            sku_CustList.Add(str.Trim())
                        End If
                    Next
                    If (sku_CustList.Count = 1) Then
                        strWhere &= String.Format(" {0} Sku_Cust like @Sku_Cust ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@Sku_Cust", SqlDbType.VarChar, 50).Value = "%" & Me.txtSku_Cust.Text.Trim() & "%"
                    ElseIf (sku_CustList.Count = 1) Then
                        Dim str As String = String.Join("','", sku_CustList.ToArray())
                        strWhere &= String.Format(" {0} Sku_Cust in ('{1}') ", IIf(strWhere.Trim() = Nothing, "where", "and"), str)
                    End If
                End If
                If (Me.chkSku_Between.Checked) Then
                    Dim Sku_Between_S As String = txtSku_Between_S.Text.Trim()
                    Dim Sku_Between_E As String = txtSku_Between_E.Text.Trim()
                    If (Not Sku_Between_S = Nothing) Then
                        strWhere &= String.Format(" {0} Sku_Id>=@Sku_Id_Between_S ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@Sku_Id_Between_S", SqlDbType.VarChar, 50).Value = Sku_Between_S
                    End If
                    If (Not Sku_Between_E = Nothing) Then
                        strWhere &= String.Format(" {0} Sku_Id<=@Sku_Id_Between_E ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@Sku_Id_Between_E", SqlDbType.VarChar, 50).Value = Sku_Between_E
                    End If
                End If
                If (Me.chkPO_No.Checked) Then
                    Dim arrPO_No() As String = txtPO_No.Text.Split(vbNewLine)
                    Dim PO_NoList As New List(Of String)
                    For Each str As String In arrPO_No
                        If (Not str.Trim() = Nothing) Then
                            PO_NoList.Add(str.Trim())
                        End If
                    Next
                    If (PO_NoList.Count = 1) Then
                        strWhere &= String.Format(" {0} PurchaseOrder_No like @PurchaseOrder_No ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@PurchaseOrder_No", SqlDbType.VarChar, 50).Value = "%" & Me.txtPO_No.Text.Trim() & "%"
                    ElseIf (PO_NoList.Count > 1) Then
                        Dim str As String = String.Join("','", PO_NoList.ToArray())
                        strWhere &= String.Format(" {0} PurchaseOrder_No in ('{1}') ", IIf(strWhere.Trim() = Nothing, "where", "and"), str)
                    End If
                End If
                If (Me.chkExpected_Delivery_Date.Checked) Then
                    strWhere &= String.Format(" {0} cast(Expected_Delivery_Date as date)>=@Expected_Delivery_Date_S and cast(Expected_Delivery_Date as date)<=@Expected_Delivery_Date_E ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                    .Parameters.Add("@Expected_Delivery_Date_S", SqlDbType.Date).Value = Me.dtpExpected_Delivery_Date_S.Value
                    .Parameters.Add("@Expected_Delivery_Date_E", SqlDbType.Date).Value = Me.dtpExpected_Delivery_Date_E.Value
                End If
                If (Me.chkSupplier.Checked) Then
                    Dim arrSupplier() As String = txtSupplier.Text.Split(vbNewLine)
                    Dim SupplierList As New List(Of String)
                    For Each str As String In arrSupplier
                        If (Not str.Trim() = Nothing) Then
                            SupplierList.Add(str.Trim())
                        End If
                    Next
                    If (SupplierList.Count = 1) Then
                        strWhere &= String.Format(" {0} Supplier_Id like @Supplier_Id ", IIf(strWhere.Trim() = Nothing, "where", "and"))
                        .Parameters.Add("@Supplier_Id", SqlDbType.VarChar, 50).Value = "%" & Me.txtSupplier.Text.Trim() & "%"
                    ElseIf (SupplierList.Count > 1) Then
                        Dim str As String = String.Join("','", SupplierList.ToArray())
                        strWhere &= String.Format(" {0} Supplier_Id in ('{1}') ", IIf(strWhere.Trim() = Nothing, "where", "and"), str)
                    End If
                End If
                Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
                strWhere &= IIf(strWhere.Trim() = Nothing, " where 1=1 ", "") & oUser.GetUserByCustomer()

                strWhere &= " order by PO_Date asc, Customer_Id asc, PurchaseOrder_No asc, Sku_Id asc "
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

    'Private Sub export(ByVal ds As DataSet)
    '    Try
    '        Dim saveFileDialog As New SaveFileDialog
    '        saveFileDialog.Filter = "Excel File|*.xls"
    '        saveFileDialog.Title = "Save an Excel File"
    '        saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
    '        saveFileDialog.FileName = Me.Text
    '        If saveFileDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
    '            Me.exportExcel(saveFileDialog.FileName, ds)
    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

    'Private Declare Auto Function GetWindowThreadProcessId Lib "user32.dll" (ByVal hwnd As IntPtr, ByRef lpdwProcessId As Integer) As Integer

    'Private Sub exportExcel(ByVal strFileName As String, ByVal dsExport As DataSet)
    '    System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-US")

    '    'start the application
    '    'Dim xlApp As Microsoft.Office.Interop.Excel.Application = New Microsoft.Office.Interop.Excel.Application()
    '    Dim xlApp As Object = CreateObject("Excel.Application")

    '    'get the window handle
    '    Dim xlHWND As Integer = xlApp.Hwnd

    '    'this will have the process ID after call to GetWindowThreadProcessId
    '    Dim xlProcessId As Integer = 0

    '    'get the process ID
    '    GetWindowThreadProcessId(xlHWND, xlProcessId)

    '    'get the process
    '    Dim xlProcess As Process = Process.GetProcessById(xlProcessId)

    '    Dim isFileOpen As Boolean = False

    '    'Dim xlWorkBook As Workbook
    '    'Dim xlWorkSheet As Worksheet
    '    Dim xlWorkBook As New Object
    '    Dim xlWorkSheet As New Object
    '    Try
    '        If xlApp Is Nothing Then
    '            W_MSG_Error("Excel is not properly installed")
    '            Return
    '        End If
    '        If IsNumeric(xlApp.Version) AndAlso CDec(xlApp.Version) < 11 Then
    '            W_MSG_Error("Excel is lower than version 11 (2003)")
    '            Return
    '        End If

    '        Dim misValue As Object = System.Reflection.Missing.Value
    '        'Dim chartRange As Range
    '        Dim sheetIndex As Integer = 0

    '        xlWorkBook = xlApp.Workbooks.Add(misValue)

    '        ' Copy each DataTable as a new Sheet
    '        For Each dt As System.Data.DataTable In dsExport.Tables
    '            Dim Title_Dict As Dictionary(Of String, String) = New Dictionary(Of String, String)
    '            For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
    '                With dt.Columns(iCol)
    '                    If .ColumnName.Contains("title_") Then
    '                        Title_Dict.Add(.ColumnName, dt.Rows(0).Item(.ColumnName).ToString())
    '                        dt.Columns.Remove(dt.Columns(iCol))
    '                    End If
    '                End With
    '            Next
    '            sheetIndex += 1

    '            ' Copy the DataTable to an object array
    '            Dim rawData(dt.Rows.Count, dt.Columns.Count - 1) As Object

    '            ' Copy the column names to the first row of the object array
    '            For col As Integer = 0 To dt.Columns.Count - 1
    '                rawData(0, col) = dt.Columns(col).ColumnName
    '            Next

    '            ' Copy the values to the object array
    '            For col As Integer = 0 To dt.Columns.Count - 1
    '                For row As Integer = 0 To dt.Rows.Count - 1
    '                    rawData(row + 1, col) = dt.Rows(row).ItemArray(col)
    '                Next
    '            Next

    '            Dim finalColLetter As String = ColumnIndexToColumnLetter(dt.Columns.Count)

    '            ' Create a new Sheet
    '            xlWorkSheet = CType(xlWorkBook.Sheets.Add(xlWorkBook.Sheets(sheetIndex), Type.Missing, 1, XlSheetType.xlWorksheet), Worksheet)

    '            'xlWorkSheet = xlWorkBook.Sheets(dt.TableName)
    '            xlWorkSheet.Name = dt.TableName

    '            ' Format
    '            'xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).NumberFormat = "@"
    '            For iCol As Integer = 0 To dt.Columns.Count - 1
    '                Select Case dt.Columns(iCol).DataType.Name
    '                    Case "Boolean", "String"
    '                        xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
    '                    Case "DateTime", "TimeSpan"
    '                        xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "dd/MM/yyyy"
    '                    Case "Decimal", "Double"
    '                        xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0.000000"
    '                    Case "Int16", "Int32", "Int64"
    '                        xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "#,##0"
    '                    Case Else
    '                        xlWorkSheet.Range(String.Format("{0}:{0}", ColumnIndexToColumnLetter(iCol + 1))).NumberFormat = "@"
    '                End Select
    '            Next
    '            'xlWorkSheet.Range("B:B").NumberFormat = "dd/MM/yyyy"
    '            'xlWorkSheet.Range("C:D").NumberFormat = "@"
    '            'xlWorkSheet.Range("E:F").NumberFormat = "#,##0"
    '            'xlWorkSheet.Range("G:H").NumberFormat = "@"
    '            'xlWorkSheet.Range("I:K").NumberFormat = "#,##0.00"
    '            'xlWorkSheet.Range("L:L").NumberFormat = "@"

    '            ' data export to Excel
    '            Dim iStartRows As Integer = 1
    '            Dim excelRange As String = String.Format("A{0}:{1}{2}", iStartRows, finalColLetter, iStartRows + dt.Rows.Count)
    '            xlWorkSheet.Range(excelRange, Type.Missing).Value2 = rawData

    '            'Dim headerList As New List(Of String)
    '            '

    '            'For iHeader As Integer = 1 To headerList.Count
    '            '    xlWorkSheet.Cells(1, iHeader) = headerList.Item(iHeader - 1)
    '            'Next

    '            'For iCols As Integer = 1 To 18
    '            '    ' Merge Cell
    '            '    xlWorkSheet.Range(String.Format("{0}1:{0}3", ColumnIndexToColumnLetter(iCols))).Merge()
    '            'Next
    '            'xlWorkSheet.Range(String.Format("{0}1:{1}1", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(24))).Merge()
    '            'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(19), ColumnIndexToColumnLetter(22))).Merge()
    '            'xlWorkSheet.Range(String.Format("{0}2:{1}2", ColumnIndexToColumnLetter(23), ColumnIndexToColumnLetter(24))).Merge()

    '            ' Font Bold
    '            xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Bold = True

    '            ' Align
    '            xlWorkSheet.Range("A:AU").HorizontalAlignment = XlHAlign.xlHAlignLeft
    '            'xlWorkSheet.Range("B:B").HorizontalAlignment = XlHAlign.xlHAlignCenter
    '            'xlWorkSheet.Range("C:D").HorizontalAlignment = XlHAlign.xlHAlignLeft
    '            'xlWorkSheet.Range("E:F").HorizontalAlignment = XlHAlign.xlHAlignCenter
    '            'xlWorkSheet.Range("G:H").HorizontalAlignment = XlHAlign.xlHAlignLeft
    '            'xlWorkSheet.Range("I:K").HorizontalAlignment = XlHAlign.xlHAlignRight
    '            'xlWorkSheet.Range("L:L").HorizontalAlignment = XlHAlign.xlHAlignLeft

    '            xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).VerticalAlignment = XlVAlign.xlVAlignCenter
    '            xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).HorizontalAlignment = XlHAlign.xlHAlignCenter

    '            ' Fill Color
    '            'xlWorkSheet.Range(String.Format("A1:{0}3", finalColLetter)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gray)
    '            'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Interior.ColorIndex = 56

    '            ' Font Color
    '            'xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White)

    '            ' Border Cell
    '            xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic)
    '            With xlWorkSheet.Range(String.Format("A{1}:{0}{2}", finalColLetter, iStartRows, iStartRows + dt.Rows.Count)).Borders(XlBordersIndex.xlInsideVertical)
    '                .LineStyle = XlLineStyle.xlContinuous
    '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
    '                .TintAndShade = 0
    '                .Weight = XlBorderWeight.xlThin
    '            End With
    '            With xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).Borders(XlBordersIndex.xlInsideHorizontal)
    '                .LineStyle = XlLineStyle.xlContinuous
    '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
    '                .TintAndShade = 0
    '                .Weight = XlBorderWeight.xlThin
    '            End With
    '            xlWorkSheet.Range(String.Format("A{1}:{0}{1}", finalColLetter, iStartRows)).BorderAround(XlLineStyle.xlContinuous, XlBorderWeight.xlThin, XlColorIndex.xlColorIndexAutomatic, XlColorIndex.xlColorIndexAutomatic)

    '            '' Read Data
    '            'Dim strCell As String = ""
    '            'Dim iCellTemp As Integer = 0
    '            'Dim strCellTemp As String = ""
    '            'Dim flag As Boolean = False
    '            'For iRows As Integer = iStartRows + 1 To iStartRows + dt.Rows.Count + 1
    '            '    strCell = xlWorkSheet.Cells(iRows, 1).value
    '            '    If iCellTemp = 0 Then
    '            '        iCellTemp = iRows
    '            '        strCellTemp = strCell
    '            '    End If
    '            '    If strCell <> strCellTemp Then
    '            '        If flag Then
    '            '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Transparent)
    '            '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 15
    '            '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
    '            '                .LineStyle = XlLineStyle.xlContinuous
    '            '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
    '            '                .TintAndShade = 0
    '            '                .Weight = XlBorderWeight.xlThin
    '            '            End With
    '            '        Else
    '            '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Silver)
    '            '            'xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Interior.ColorIndex = 2
    '            '            With xlWorkSheet.Range(String.Format("A{0}:{1}{2}", iCellTemp, finalColLetter, iRows - 1)).Borders(XlBordersIndex.xlEdgeTop)
    '            '                .LineStyle = XlLineStyle.xlContinuous
    '            '                .ColorIndex = XlColorIndex.xlColorIndexAutomatic
    '            '                .TintAndShade = 0
    '            '                .Weight = XlBorderWeight.xlThin
    '            '            End With
    '            '        End If
    '            '        flag = Not flag
    '            '        iCellTemp = iRows
    '            '        strCellTemp = strCell
    '            '    End If
    '            'Next

    '            ' Set Font
    '            xlWorkSheet.Cells.Font.Name = "Tahoma"
    '            xlWorkSheet.Cells.Font.Size = 10
    '            'xlWorkSheet.Rows.RowHeight = 18
    '            xlWorkSheet.Cells.WrapText = False

    '            ' Set Column Autosize
    '            xlWorkSheet.Range(String.Format("A:{0}", finalColLetter)).EntireColumn.AutoFit()
    '            xlWorkSheet.Range("F:F").EntireColumn.ColumnWidth = 50
    '            'xlWorkSheet.Range("D:D").EntireColumn.ColumnWidth = 20
    '            'xlWorkSheet.Range("E:F").EntireColumn.ColumnWidth = 10
    '            'xlWorkSheet.Range("G:G").EntireColumn.ColumnWidth = 50
    '            'xlWorkSheet.Range("H:H").EntireColumn.ColumnWidth = 10
    '            'xlWorkSheet.Range("I:K").EntireColumn.ColumnWidth = 12
    '            'xlWorkSheet.Range("L:L").EntireColumn.ColumnWidth = 20

    '            xlWorkSheet = Nothing
    '        Next

    '        '--------------------------------------------------------

    '        'Dim strFileName As String = fileName
    '        'Dim isFileOpen As Boolean = False
    '        Try
    '            Dim fileTemp As System.IO.FileStream = System.IO.File.OpenWrite(strFileName)
    '            fileTemp.Close()
    '        Catch ex As Exception
    '            isFileOpen = True
    '            MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        End Try

    '        If Not isFileOpen Then
    '            If System.IO.File.Exists(strFileName) Then
    '                System.IO.File.Delete(strFileName)
    '            End If
    '            xlWorkBook.SaveAs(strFileName, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue)
    '        End If
    '        isFileOpen = True
    '        xlApp.Visible = True

    '    Catch ex As Exception
    '        If Not xlProcess.HasExited Then
    '            xlProcess.Kill()
    '        End If
    '        MessageBox.Show(ex.Message, "Export Excel", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture("en-GB")
    '        If Not isFileOpen AndAlso Not xlProcess.HasExited Then
    '            xlProcess.Kill()
    '        End If
    '        ReleaseObject(xlApp)
    '        ReleaseObject(xlWorkBook)
    '        ReleaseObject(xlWorkSheet)
    '    End Try
    'End Sub

    'Private Function ColumnIndexToColumnLetter(ByVal colIndex As Integer) As String
    '    Dim div As Integer = colIndex
    '    Dim colLetter As String = String.Empty
    '    Dim modnum As Integer = 0

    '    While div > 0
    '        modnum = (div - 1) Mod 26
    '        colLetter = Chr(65 + modnum) & colLetter
    '        div = CInt((div - modnum) \ 26)
    '    End While

    '    Return colLetter
    'End Function

    'Private Sub ReleaseObject(ByVal obj As Object)
    '    Try
    '        System.Runtime.InteropServices.Marshal.ReleaseComObject(obj)
    '        obj = Nothing
    '    Catch ex As Exception
    '        obj = Nothing
    '    Finally
    '        GC.Collect()
    '    End Try
    'End Sub

    Private Sub defaultOnLoad()
        Me.dgvData.AutoGenerateColumns = False
        Me.getStatus()
        'Me.getProductType()
        Me.dtpPO_Date_S.Value = New Date(Today.Year, Today.Month, Today.Day)
        Me.dtpPO_Date_E.Value = New Date(Today.Year, Today.Month, Today.Day)
        Me.dtpExpected_Delivery_Date_S.Value = New Date(Today.Year, Today.Month, Today.Day)
        Me.dtpExpected_Delivery_Date_E.Value = New Date(Today.Year, Today.Month, Today.Day)
        Me.resetCondition()
    End Sub

    Private Sub resetCondition()
        Me.dtpPO_Date_S.Enabled = Me.chkPO_Date.Checked
        Me.dtpPO_Date_E.Enabled = Me.chkPO_Date.Checked
        Me.txtCustomer.Enabled = Me.chkCustomer.Checked
        Me.txtCustomer.Clear()
        Me.btnCustomer_Popup.Enabled = Me.chkCustomer.Checked
        Me.cboStatus.Enabled = Me.chkStatus.Checked
        Me.txtProductType.Enabled = Me.chkProductType.Checked
        Me.txtProductType.Clear()
        Me.btnProductType_Popup.Enabled = Me.chkProductType.Checked
        'Me.cboProductType.Enabled = Me.chkProductType.Checked
        Me.txtSku.Enabled = Me.chkSku.Checked
        Me.txtSku.Clear()
        Me.btnSku_Popup.Enabled = Me.chkSku.Checked
        Me.txtSku_Cust.Enabled = Me.chkSku_Cust.Checked
        Me.txtSku_Cust.Clear()
        Me.btnSku_Cust_Popup.Enabled = Me.chkSku_Cust.Checked
        Me.txtSku_Between_S.Clear()
        Me.txtSku_Between_S.Enabled = Me.chkSku_Between.Checked
        Me.btnPopupSku_Between_S.Enabled = Me.chkSku_Between.Checked
        Me.txtSku_Between_E.Clear()
        Me.txtSku_Between_E.Enabled = Me.chkSku_Between.Checked
        Me.btnPopupSku_Between_E.Enabled = Me.chkSku_Between.Checked
        Me.txtPO_No.Enabled = Me.chkPO_No.Checked
        Me.txtPO_No.Clear()
        Me.dtpExpected_Delivery_Date_S.Enabled = Me.chkExpected_Delivery_Date.Checked
        Me.dtpExpected_Delivery_Date_E.Enabled = Me.chkExpected_Delivery_Date.Checked
        Me.txtSupplier.Enabled = Me.chkSupplier.Checked
        Me.txtSupplier.Clear()
        Me.btnSupplier_Popup.Enabled = Me.chkSupplier.Checked
    End Sub

    Private Sub chkPO_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPO_Date.CheckedChanged
        Me.dtpPO_Date_S.Enabled = Me.chkPO_Date.Checked
        Me.dtpPO_Date_E.Enabled = Me.chkPO_Date.Checked
    End Sub

    Private Sub chkCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkCustomer.CheckedChanged
        Me.txtCustomer.Enabled = Me.chkCustomer.Checked
        Me.btnCustomer_Popup.Enabled = Me.chkCustomer.Checked
        If Me.chkCustomer.Checked Then
            Me.txtCustomer.Focus()
        Else
            Me.txtCustomer.Clear()
        End If
    End Sub

    Private Sub chkStatus_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkStatus.CheckedChanged
        Me.cboStatus.Enabled = Me.chkStatus.Checked
    End Sub

    Private Sub chkProductType_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkProductType.CheckedChanged
        Me.txtProductType.Enabled = Me.chkProductType.Checked
        Me.btnProductType_Popup.Enabled = Me.chkProductType.Checked
        If Me.chkProductType.Checked Then
            Me.txtProductType.Focus()
        Else
            Me.txtProductType.Clear()
        End If
        'Me.cboProductType.Enabled = Me.chkProductType.Checked
    End Sub

    Private Sub chkSku_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku.CheckedChanged
        Me.txtSku.Enabled = Me.chkSku.Checked
        Me.btnSku_Popup.Enabled = Me.chkSku.Checked
        If Me.chkSku.Checked Then
            Me.txtSku.Focus()
        Else
            Me.txtSku.Clear()
        End If
    End Sub

    Private Sub chkSku_Cust_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku_Cust.CheckedChanged
        Me.txtSku_Cust.Enabled = Me.chkSku_Cust.Checked
        Me.btnSku_Cust_Popup.Enabled = Me.chkSku_Cust.Checked
        If Me.chkSku_Cust.Checked Then
            Me.txtSku_Cust.Focus()
        Else
            Me.txtSku_Cust.Clear()
        End If
    End Sub

    Private Sub chkSku_Between_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSku_Between.CheckedChanged
        Me.txtSku_Between_S.Enabled = Me.chkSku_Between.Checked
        Me.btnPopupSku_Between_S.Enabled = Me.chkSku_Between.Checked
        Me.txtSku_Between_E.Enabled = Me.chkSku_Between.Checked
        Me.btnPopupSku_Between_E.Enabled = Me.chkSku_Between.Checked
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            If (Me.chkPO_Date.Checked AndAlso Me.dtpPO_Date_S.Value > Me.dtpPO_Date_E.Value) Then
                W_MSG_Information(String.Format("{0}เริ่มต้นมีค่ามากกว่าสิ้นสุด", Me.chkPO_Date.Text))
                Exit Sub
            End If
            If (Me.chkCustomer.Checked AndAlso Me.txtCustomer.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkCustomer.Text))
                Exit Sub
            End If
            If (Me.chkProductType.Checked AndAlso Me.txtProductType.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkProductType.Text))
                Exit Sub
            End If
            If (Me.chkSku.Checked AndAlso Me.txtSku.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkSku.Text))
                Exit Sub
            End If
            If (Me.chkSku_Cust.Checked AndAlso Me.txtSku_Cust.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkSku_Cust.Text))
                Exit Sub
            End If
            If (Me.chkSku_Between.Checked AndAlso (Me.txtSku_Between_S.Text.Trim() = Nothing AndAlso Me.txtSku_Between_E.Text.Trim() = Nothing)) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkSku_Between.Text))
                Exit Sub
            End If
            If (Me.chkPO_No.Checked AndAlso Me.txtPO_No.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkPO_No.Text))
                Exit Sub
            End If
            If (Me.chkSupplier.Checked AndAlso Me.txtSupplier.Text.Trim() = Nothing) Then
                W_MSG_Information(String.Format("กรุณาระบุ{0}", Me.chkSupplier.Text))
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
            Dim ds As New DataSet

            Dim objExport As New Export_Excel_KC
            ds.Tables.Add(objExport.DataGridViewToDataTable(Me.dgvData))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm")
            objExport.export(ds, Me.Text)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
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

    'Private Function DataGridViewToDataTable(ByVal dtg As DataGridView, Optional ByVal DataTableName As String = "myDataTable") As System.Data.DataTable
    '    Try
    '        Dim dt As New System.Data.DataTable(DataTableName)
    '        Dim row As DataRow
    '        Dim TotalDatagridviewColumns As Integer = dtg.ColumnCount - 1
    '        Dim visibleList As New List(Of String)
    '        'Add Datacolumn
    '        For Each c As DataGridViewColumn In dtg.Columns
    '            If c.Visible Then
    '                visibleList.Add(c.HeaderText)
    '            End If
    '            Dim idColumn As DataColumn = New DataColumn()
    '            idColumn.ColumnName = c.HeaderText
    '            If (Not c.ValueType Is Nothing) Then
    '                idColumn.DataType = c.ValueType
    '            End If
    '            dt.Columns.Add(idColumn)
    '        Next
    '        'Now Iterate thru Datagrid and create the data row
    '        For Each dr As DataGridViewRow In dtg.Rows
    '            'Iterate thru datagrid
    '            row = dt.NewRow 'Create new row
    '            'Iterate thru Column 1 up to the total number of columns
    '            For cn As Integer = 0 To TotalDatagridviewColumns
    '                row.Item(cn) = IfNullObj(dr.Cells(cn).Value) ' This Will handle error datagridviewcell on NULL Values
    '            Next
    '            'Now add the row to Datarow Collection
    '            dt.Rows.Add(row)
    '        Next
    '        For iCol As Integer = dt.Columns.Count - 1 To 0 Step -1
    '            If Not visibleList.Contains(dt.Columns(iCol).ColumnName) Then
    '                dt.Columns.Remove(dt.Columns(iCol))
    '            End If
    '        Next
    '        'Now return the data table
    '        Return dt
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

    Private Sub getStatus()
        Dim objtb_PurchaseOrder As New WMS_STD_INB_PO_Datalayer.tb_PurchaseOrder
        Dim objDTtb_PurchaseOrder As System.Data.DataTable = New System.Data.DataTable
        Try
            objtb_PurchaseOrder.getProcessStatus()
            objDTtb_PurchaseOrder = objtb_PurchaseOrder.DataTable
            With cboStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDTtb_PurchaseOrder
            End With
        Catch ex As Exception
            Throw ex
        Finally
            objtb_PurchaseOrder = Nothing
            objDTtb_PurchaseOrder = Nothing
        End Try
    End Sub

    'Private Sub getProductType()
    '    Dim objClassDB As New WMS_STD_Master_Datalayer.ms_ProductType(WMS_STD_Master_Datalayer.ms_ProductType.enuOperation_Type.SEARCH)
    '    Dim objDT As System.Data.DataTable = New System.Data.DataTable
    '    Try
    '        objClassDB.getProductType()
    '        objDT = objClassDB.DataTable
    '        With Me.cboProductType
    '            .DisplayMember = "Description"
    '            .ValueMember = "ProductType_Index"
    '            .DataSource = objDT
    '        End With
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objClassDB = Nothing
    '        objDT = Nothing
    '    End Try
    'End Sub

    Private Sub btnCustomer_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Popup.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            If frm.Customer_Index = Nothing Then
                Exit Sub
            Else
                Dim objClassDB As New WMS_STD_Master_Datalayer.ms_Customer(WMS_STD_Master_Datalayer.ms_Customer.enuOperation_Type.SEARCH)
                Dim objDT As System.Data.DataTable = New System.Data.DataTable
                objClassDB.getPopup_Customer("Customer_Index", frm.Customer_Index)
                objDT = objClassDB.DataTable
                If objDT.Rows.Count > 0 Then
                    Dim _strCustomer As String = Me.txtCustomer.Text.ToString().Trim()
                    If (Not _strCustomer = Nothing) Then
                        _strCustomer = objDT.Rows(0).Item("Customer_Id").ToString() + vbCrLf + _strCustomer
                    Else
                        _strCustomer = objDT.Rows(0).Item("Customer_Id").ToString()
                    End If
                    Me.txtCustomer.Text = _strCustomer
                    'Me.txtCustomer.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Else
                    Exit Sub
                    'Me.txtCustomer.Text = ""
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSku_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Popup.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            frm.Customer_Index = ""
            frm.ShowDialog()
            If frm.Sku_Index = Nothing Then
                Exit Sub
            Else
                Dim _strSku As String = Me.txtSku.Text.ToString().Trim()
                If (Not _strSku = Nothing) Then
                    _strSku = frm.Sku_ID + vbCrLf + _strSku
                Else
                    _strSku = frm.Sku_ID
                End If
                Me.txtSku.Text = _strSku
                'Me.txtSku.Text = frm.Sku_ID
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSku_Cust_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Cust_Popup.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            frm.Customer_Index = ""
            frm.ShowDialog()
            If frm.Sku_Index = Nothing Then
                Exit Sub
            Else
                Dim objClass As New WMS_STD_Master_Datalayer.ms_SKU(WMS_STD_Master_Datalayer.ms_SKU.enuOperation_Type.SEARCH)
                Dim objDt As System.Data.DataTable = New System.Data.DataTable
                objClass.getSKU_Detail(frm.Sku_ID)
                objDt = objClass.DataTable
                If (objDt.Rows.Count() > 0) Then
                    With objDt.Rows(0)
                        Dim _strSku_Cust As String = Me.txtSku_Cust.Text.ToString().Trim()
                        If (Not _strSku_Cust = Nothing) Then
                            _strSku_Cust = .Item("Str4") + vbCrLf + _strSku_Cust
                        Else
                            _strSku_Cust = .Item("Str4")
                        End If
                        Me.txtSku_Cust.Text = _strSku_Cust
                        'Me.txtSku_Cust.Text = .Item("Str4")
                    End With
                Else
                    Exit Sub
                    'Me.txtSku_Cust.Text = ""
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPopupSku_Between_S_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopupSku_Between_S.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            frm.Customer_Index = ""
            frm.ShowDialog()
            If frm.Sku_Index = Nothing Then
                Exit Sub
            Else
                Me.txtSku_Between_S.Text = frm.Sku_ID
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPopupSku_Between_E_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopupSku_Between_E.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            frm.Customer_Index = ""
            frm.ShowDialog()
            If frm.Sku_Index = Nothing Then
                Exit Sub
            Else
                Me.txtSku_Between_E.Text = frm.Sku_ID
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnProductType_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductType_Popup.Click
        Try
            Dim _popup As New frmProductType_Popup()
            _popup.ShowDialog()
            If (_popup.DialogResult = System.Windows.Forms.DialogResult.OK) Then
                Dim _strProductType As String = Me.txtProductType.Text.ToString().Trim()
                If (Not _strProductType = Nothing) Then
                    _strProductType = _popup.ProductType_Id + vbCrLf + _strProductType
                Else
                    _strProductType = _popup.ProductType_Id
                End If
                Me.txtProductType.Text = _strProductType
            ElseIf (_popup.DialogResult = System.Windows.Forms.DialogResult.Cancel) Then
                Exit Sub
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkPO_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkPO_No.CheckedChanged
        Me.txtPO_No.Enabled = Me.chkPO_No.Checked
        If Me.chkPO_No.Checked Then
            Me.txtPO_No.Focus()
        Else
            Me.txtPO_No.Clear()
        End If
    End Sub

    Private Sub chkExpected_Delivery_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkExpected_Delivery_Date.CheckedChanged
        Me.dtpExpected_Delivery_Date_S.Enabled = Me.chkExpected_Delivery_Date.Checked
        Me.dtpExpected_Delivery_Date_E.Enabled = Me.chkExpected_Delivery_Date.Checked
    End Sub

    Private Sub chkSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSupplier.CheckedChanged
        Me.txtSupplier.Enabled = Me.chkSupplier.Checked
        Me.btnSupplier_Popup.Enabled = Me.chkSupplier.Checked
        If Me.chkSupplier.Checked Then
            Me.txtSupplier.Focus()
        Else
            Me.txtSupplier.Clear()
        End If
    End Sub

    Private Sub btnSupplier_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSupplier_Popup.Click
        Try
            Dim frm As New frmSupplier_Popup()
            frm.ShowDialog()
            If frm.Supplier_Index = Nothing Then
                Exit Sub
            Else
                Dim _strSupplier As String = Me.txtSupplier.Text.ToString().Trim()
                If (Not _strSupplier = Nothing) Then
                    _strSupplier = frm.strSupplier_Id + vbCrLf + _strSupplier
                Else
                    _strSupplier = frm.strSupplier_Id
                End If
                Me.txtSupplier.Text = _strSupplier
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class