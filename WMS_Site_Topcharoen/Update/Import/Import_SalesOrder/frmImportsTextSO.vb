Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration.ConfigurationManager
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_Formula
Imports System.IO
Imports System.Text
Public Class frmImportsTextSO

    Private _Result As Boolean = True
    Private _DtHD As New DataTable
    Private _DtRM As New DataTable
    Private _DtDT As New DataTable
    Private _FileName As String = ""
    Private _DtTemp As New DataTable

    Private Sub btnFileSOHD_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSOHD.Click
        Try
            Me._DtHD = New DataTable
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim strFileName As String

            fd.Title = "Open File Dialog"
            fd.InitialDirectory = "C:\"
            fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
            fd.FilterIndex = 2
            fd.RestoreDirectory = True

            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.btnImports.Enabled = False

                strFileName = fd.FileName
                Me.TextBox1.Text = strFileName
                Me._DtHD = ConvertToDataTable(strFileName, AppSettings("numberOfColumnsHeader").ToString)
                Me.DataGridView1.DataSource = Me._DtHD
            Else
                Me.DataGridView1.DataSource = Me._DtHD
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFileSORM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSORM.Click
        Try
            Me._DtRM = New DataTable
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim strFileName As String

            fd.Title = "Open File Dialog"
            fd.InitialDirectory = "C:\"
            fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
            fd.FilterIndex = 2
            fd.RestoreDirectory = True

            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.btnImports.Enabled = False

                strFileName = fd.FileName
                Me.TextBox3.Text = strFileName
                Me._DtRM = ConvertToDataTable(strFileName, AppSettings("numberOfColumnsRemark").ToString)
                Me.DataGridView3.DataSource = Me._DtRM
            Else
                Me.DataGridView3.DataSource = Me._DtRM
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub btnFileSODT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileSODT.Click
        Try
            Me._DtDT = New DataTable
            Dim fd As OpenFileDialog = New OpenFileDialog()
            Dim strFileName As String

            fd.Title = "Open File Dialog"
            fd.InitialDirectory = "C:\"
            fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
            fd.FilterIndex = 2
            fd.RestoreDirectory = True

            If fd.ShowDialog() = Windows.Forms.DialogResult.OK Then
                Me.btnImports.Enabled = False

                strFileName = fd.FileName
                Me.TextBox2.Text = strFileName
                Me._DtDT = ConvertToDataTable(strFileName, AppSettings("numberOfColumnsDetail").ToString)
                Me.DataGridView2.DataSource = Me._DtDT
            Else
                Me.DataGridView2.DataSource = Me._DtDT
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Sub frmImportsTextSO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.btnImports.Enabled = False
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub

    Private Function ConvertToDataTable(ByVal filePath As String, ByVal numberOfColumns As Integer) As DataTable

        Dim strCommandLine As String = "Header"
        Dim tbl = New DataTable()

        Dim strText As String = System.IO.File.ReadAllText(filePath, UnicodeEncoding.GetEncoding(0))
        Dim lines() As String = strText.Replace(vbLf, "").Split(vbCr)

        For Each line As String In lines
            Dim cols = line.Split(vbTab)

            If strCommandLine = "Header" Then
                For i As Integer = 0 To numberOfColumns
                    tbl.Columns.Add(New DataColumn(cols(i).ToString()))
                Next
                strCommandLine = "Detail"
            Else
                Dim dr As DataRow = tbl.NewRow()

                If line.Length <> 0 Then
                    For cIndex As Integer = 0 To numberOfColumns
                        dr(cIndex) = cols(cIndex)
                    Next

                    tbl.Rows.Add(dr)
                End If
            End If
        Next

        Return tbl

    End Function

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        _Result = True

        'SO Header
        Try
            Dim objDB As New clsImportMaster
            For Each rowHD As DataGridViewRow In DataGridView1.Rows
                If rowHD.Cells("DocuNo").Value = "" Then
                    rowHD.Cells("DocuNo").Style.BackColor = Color.Red
                    _Result = False
                End If

                'If rowHD.Cells("custcode").Value = "" Then
                '    rowHD.Cells("custcode").Style.BackColor = Color.Red
                '    _Result = False

                'End If

                'If rowHD.Cells("ShipToCode").Value = "" Then
                '    rowHD.Cells("ShipToCode").Style.BackColor = Color.Red
                '    _Result = False
                'End If

                If rowHD.Cells("BILLTO").Value = "" Then
                    rowHD.Cells("BILLTO").Style.BackColor = Color.Red
                    _Result = False
                Else
                    If objDB.checkSoldTo(rowHD.Cells("BILLTO").Value.ToString) = False Then
                        '_Result = False
                        'MsgBox("ไม่พบ BILLTO  : " & rowHD.Cells("BILLTO").Value.ToString)
                        'Exit Sub
                        ' BGN Pong 2016-11-23 Call Import Customer Shipping
                        Dim _frm As New frmImportCustomer_Shipping()
                        Dim _dt As New DataTable
                        _dt.Columns.Add("รหัสเจ้าของสินค้า", GetType(String))
                        _dt.Columns.Add("Sold To Id", GetType(String))
                        _dt.Columns.Add("Sold To Name", GetType(String))
                        _dt.Columns.Add("Address", GetType(String))
                        _dt.Columns.Add("ตำบล/แขวง", GetType(String))
                        _dt.Columns.Add("อำเภอ/เขต", GetType(String))
                        _dt.Columns.Add("จังหวัด", GetType(String))
                        _dt.Columns.Add("Postcode", GetType(String))
                        _dt.Columns.Add("Tel", GetType(String))
                        _dt.Columns.Add("Fax", GetType(String))
                        _dt.Columns.Add("Mobile", GetType(String))
                        _dt.Columns.Add("Email", GetType(String))
                        _dt.Columns.Add("หมายเหตุ", GetType(String))
                        _dt.Columns.Add("ชื่อผู้ติดต่อ 1", GetType(String))
                        _dt.Columns.Add("ชื่อผู้ติดต่อ 2", GetType(String))
                        _dt.Columns.Add("ชื่อผู้ติดต่อ 3", GetType(String))
                        Dim _dr As DataRow = _dt.NewRow()
                        _dr("รหัสเจ้าของสินค้า") = rowHD.Cells("OWNER").Value.ToString
                        _dr("Sold To Id") = rowHD.Cells("BILLTO").Value.ToString
                        _dr("Sold To Name") = rowHD.Cells("CustName").Value.ToString
                        _dr("Address") = rowHD.Cells("ShipToAddr1").Value.ToString
                        _dr("ตำบล/แขวง") = rowHD.Cells("District").Value.ToString
                        _dr("อำเภอ/เขต") = rowHD.Cells("Amphur").Value.ToString
                        _dr("จังหวัด") = rowHD.Cells("Province").Value.ToString
                        _dr("Postcode") = rowHD.Cells("PostCode").Value.ToString
                        _dr("Tel") = rowHD.Cells("Tel").Value.ToString
                        _dr("Fax") = rowHD.Cells("Fax").Value.ToString
                        _dr("Mobile") = ""
                        _dr("Email") = ""
                        _dr("หมายเหตุ") = ""
                        _dr("ชื่อผู้ติดต่อ 1") = rowHD.Cells("ContactName").Value.ToString
                        _dr("ชื่อผู้ติดต่อ 2") = ""
                        _dr("ชื่อผู้ติดต่อ 3") = ""
                        _dt.Rows.Add(_dr)
                        _frm.DT = _dt
                        _frm.ShowDialog()
                        If (_frm.IsProcess) Then
                            If objDB.checkSoldTo(rowHD.Cells("BILLTO").Value.ToString) = False Then
                                _Result = False
                                MsgBox("ไม่พบ BILLTO  : " & rowHD.Cells("BILLTO").Value.ToString)
                                Exit Sub
                            End If
                        Else
                            _Result = False
                            'MsgBox("ไม่พบ BILLTO  : " & rowHD.Cells("BILLTO").Value.ToString)
                            Exit Sub
                        End If
                        ' END Pong 2016-11-23 Call Import Customer Shipping
                    End If
                End If

                If rowHD.Cells("SHIPTO").Value = "" Then
                    rowHD.Cells("SHIPTO").Style.BackColor = Color.Red
                    _Result = False


                Else
                    If objDB.checkShipTo(rowHD.Cells("SHIPTO").Value.ToString, rowHD.Cells("BILLTO").Value.ToString) = False Then
                        '_Result = False
                        'MsgBox("ไม่พบ SHIPTO  : " & rowHD.Cells("SHIPTO").Value.ToString)
                        'Exit Sub
                        ' BGN Pong 2016-11-23 Call Import Customer Shipping Location
                        Dim _frm As New frmImportCustomer_Shipping_Location()
                        Dim _dt As New DataTable
                        _dt.Columns.Add("รหัสเจ้าของสินค้า", GetType(String))
                        _dt.Columns.Add("Sold To Id", GetType(String))
                        _dt.Columns.Add("Ship To Id", GetType(String))
                        _dt.Columns.Add("Ship To Name", GetType(String))
                        _dt.Columns.Add("Address", GetType(String))
                        _dt.Columns.Add("ตำบล", GetType(String))
                        _dt.Columns.Add("อำเภอ", GetType(String))
                        _dt.Columns.Add("จังหวัด", GetType(String))
                        _dt.Columns.Add("Postcode", GetType(String))
                        _dt.Columns.Add("Tel", GetType(String))
                        _dt.Columns.Add("Fax", GetType(String))
                        _dt.Columns.Add("Mobile", GetType(String))
                        _dt.Columns.Add("Email", GetType(String))
                        _dt.Columns.Add("หมายเหตุ", GetType(String))
                        _dt.Columns.Add("ชื่อผู้ติดต่อ 1", GetType(String))
                        _dt.Columns.Add("ชื่อผู้ติดต่อ 2", GetType(String))
                        _dt.Columns.Add("ชื่อผู้ติดต่อ 3", GetType(String))
                        _dt.Columns.Add("Route_Id", GetType(String))
                        _dt.Columns.Add("SubRoute_Id", GetType(String))
                        Dim _dr As DataRow = _dt.NewRow()
                        _dr("รหัสเจ้าของสินค้า") = rowHD.Cells("OWNER").Value.ToString
                        _dr("Sold To Id") = rowHD.Cells("BILLTO").Value.ToString
                        _dr("Ship To Id") = rowHD.Cells("SHIPTO").Value.ToString
                        _dr("Ship To Name") = rowHD.Cells("CustName").Value.ToString
                        _dr("Address") = rowHD.Cells("ShipToAddr1").Value.ToString
                        _dr("ตำบล") = rowHD.Cells("District").Value.ToString
                        _dr("อำเภอ") = rowHD.Cells("Amphur").Value.ToString
                        _dr("จังหวัด") = rowHD.Cells("Province").Value.ToString
                        _dr("Postcode") = rowHD.Cells("PostCode").Value.ToString
                        _dr("Tel") = rowHD.Cells("Tel").Value.ToString
                        _dr("Fax") = rowHD.Cells("Fax").Value.ToString
                        _dr("Mobile") = ""
                        _dr("Email") = ""
                        _dr("หมายเหตุ") = ""
                        _dr("ชื่อผู้ติดต่อ 1") = rowHD.Cells("ContactName").Value.ToString
                        _dr("ชื่อผู้ติดต่อ 2") = ""
                        _dr("ชื่อผู้ติดต่อ 3") = ""
                        _dr("Route_Id") = ""
                        _dr("SubRoute_Id") = ""
                        _dt.Rows.Add(_dr)
                        _frm.DT = _dt
                        _frm.ShowDialog()
                        If (_frm.IsProcess) Then
                            If objDB.checkSoldTo(rowHD.Cells("SHIPTO").Value.ToString) = False Then
                                _Result = False
                                MsgBox("ไม่พบ SHIPTO  : " & rowHD.Cells("SHIPTO").Value.ToString)
                                Exit Sub
                            End If
                        Else
                            _Result = False
                            'MsgBox("ไม่พบ SHIPTO  : " & rowHD.Cells("SHIPTO").Value.ToString)
                            Exit Sub
                        End If
                        ' END Pong 2016-11-23 Call Import Customer Shipping Location
                    Else
                        ' CustName
                        ' ShipToAddr1
                        Dim _dt As DataTable = objDB.GetDataTable()
                        If (_dt.Rows.Count > 0) Then
                            Dim _customer_Shipping_Location_Index As String = _dt.Rows(0).Item("Customer_Shipping_Location_Index")
                            Dim _flagUpdate As Boolean = New clsImports().updateCustomer_Shipping_Location(_customer_Shipping_Location_Index, rowHD.Cells("CustName").Value.ToString(), rowHD.Cells("ShipToAddr1").Value.ToString())
                        End If
                    End If
                End If

                ' BGN Pong 2016-11-24 Call Edit Customer Shipping Location
                If objDB.checkRoute(rowHD.Cells("SHIPTO").Value.ToString, rowHD.Cells("BILLTO").Value.ToString) = True Then
                    Dim _dt As DataTable = objDB.GetDataTable()
                    If (_dt.Rows.Count > 0) Then
                        Dim _customer_Shipping_Index As String = _dt.Rows(0).Item("Customer_Shipping_Index")
                        Dim _customer_Shipping_Location_Index As String = _dt.Rows(0).Item("Customer_Shipping_Location_Index")
                        Dim _route_Index As String = _dt.Rows(0).Item("Route_Index")
                        Dim _subRoute_Index As String = _dt.Rows(0).Item("SubRoute_Index")
                        Dim _customerType_Index As String = _dt.Rows(0).Item("CustomerType_Index").ToString()
                        If (_route_Index = "0010000000000" OrElse _subRoute_Index = "0010000000000" OrElse _customerType_Index = "0010000000000" OrElse _customerType_Index = Nothing) Then
                            Dim _frm As New frmCustomer_Shipping_Location_V3()
                            _frm.SaveType = 1
                            _frm.Customer_Shipping_Index = _customer_Shipping_Index
                            _frm.Customer_Shipping_Location_Index = _customer_Shipping_Location_Index
                            _frm.ShowDialog()
                            ' BGN Pong 2017-04-28 
                            If objDB.checkRoute(rowHD.Cells("SHIPTO").Value.ToString, rowHD.Cells("BILLTO").Value.ToString) = True Then
                                _dt = objDB.GetDataTable()
                                If (_dt.Rows.Count > 0) Then
                                    _customer_Shipping_Index = _dt.Rows(0).Item("Customer_Shipping_Index")
                                    _customer_Shipping_Location_Index = _dt.Rows(0).Item("Customer_Shipping_Location_Index")
                                    _route_Index = _dt.Rows(0).Item("Route_Index")
                                    _subRoute_Index = _dt.Rows(0).Item("SubRoute_Index")
                                    _customerType_Index = _dt.Rows(0).Item("CustomerType_Index").ToString()
                                    If (_route_Index = "0010000000000" OrElse _subRoute_Index = "0010000000000" OrElse _customerType_Index = "0010000000000" OrElse _customerType_Index = Nothing) Then
                                        _Result = False
                                    End If
                                End If
                            End If
                            ' END Pong 2017-04-28 
                        End If
                    Else
                        _Result = False
                        MsgBox("ไม่พบ SHIPTO  : " & rowHD.Cells("SHIPTO").Value.ToString)
                        Exit Sub
                    End If
                End If
                ' END Pong 2016-11-24 Call Edit Customer Shipping Location

                If rowHD.Cells("OWNER").Value = "" Then
                    rowHD.Cells("OWNER").Style.BackColor = Color.Red
                    _Result = False
                Else
                    If objDB.checkOwner(rowHD.Cells("OWNER").Value.ToString) = False Then
                        _Result = False
                        MsgBox("ไม่พบ Owner  : " & rowHD.Cells("OWNER").Value.ToString)
                        Exit Sub
                    End If
                End If

                If _Result Then
                    rowHD.DefaultCellStyle.BackColor = Color.GreenYellow
                End If

            Next

            'SO Remark
            For Each rowHD As DataGridViewRow In DataGridView3.Rows
                If rowHD.Cells("DocuNo").Value = "" Then
                    rowHD.Cells("DocuNo").Style.BackColor = Color.Red
                    _Result = False
                End If

                If _Result Then
                    rowHD.DefaultCellStyle.BackColor = Color.GreenYellow
                End If
            Next

            'SO Detail
            For Each rowHD As DataGridViewRow In DataGridView2.Rows
                If rowHD.Cells("DocuNo").Value = "" Then
                    rowHD.Cells("DocuNo").Style.BackColor = Color.Red
                    _Result = False
                End If

                If rowHD.Cells("goodcode").Value = "" Then
                    rowHD.Cells("goodcode").Style.BackColor = Color.Red
                    _Result = False
                Else

                    If objDB.checkSku_Id(rowHD.Cells("goodcode").Value.ToString) = False Then
                        _Result = False
                        MsgBox("ไม่พบ Sku Id  : " & rowHD.Cells("goodcode").Value.ToString)
                        Exit Sub
                    End If

                End If

                If rowHD.Cells("goodunitcode").Value = "" Then
                    rowHD.Cells("goodunitcode").Style.BackColor = Color.Red
                    _Result = False
                Else

                    If objDB.checkPackage(rowHD.Cells("goodunitcode").Value.ToString, rowHD.Cells("goodcode").Value.ToString) = False Then
                        _Result = False
                        MsgBox("ไม่พบ Package Id  : " & rowHD.Cells("goodunitcode").Value.ToString)
                        Exit Sub
                    End If

                End If

                If rowHD.Cells("GoodStockRate2").Value = "" Then
                    rowHD.Cells("GoodStockRate2").Style.BackColor = Color.Red
                    _Result = False
                End If

                If rowHD.Cells("GoodQty2").Value = "" Then
                    rowHD.Cells("GoodQty2").Style.BackColor = Color.Red
                    _Result = False
                End If

                If _Result Then
                    rowHD.DefaultCellStyle.BackColor = Color.GreenYellow
                End If
            Next

            If _Result Then
                Me.btnImports.Enabled = True
            Else
                MsgBox("ตรวจพบข้อมูลไม่สมบูรณ์")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    
    End Sub

    Private Sub btnImports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImports.Click
        Try

            If _Result Then
                _FileName = Me.TextBox1.Text & ";" & Me.TextBox2.Text & ";" & Me.TextBox3.Text

                Dim numberOfColumns As Integer = 0
                numberOfColumns = Convert.ToInt16(AppSettings("numberOfColumnsHeader")) + Convert.ToInt16(AppSettings("numberOfColumnsDetail")) + 2
                _DtTemp = New DataTable
                For i As Integer = 0 To numberOfColumns
                    _DtTemp.Columns.Add(New DataColumn("[" & i & "]"))
                Next

                For Each Header As DataRow In Me._DtHD.Rows
                    ' Presuming the DataTable has a column named Date.
                    Dim expression As String = "DocuNo = '" & Header("DocuNo").ToString() & "'"

                    ' Sort descending by column named CompanyName.
                    'Dim sortOrder As String = "CompanyName ASC"
                    Dim foundRows As DataRow()

                    ' Use the Select method to find all rows matching the filter.
                    foundRows = Me._DtDT.Select(expression) ', sortOrder)

                    ' Print column 0 of each returned row.
                    'For i As Integer = 0 To foundRows.Length - 1
                    '    Console.WriteLine(foundRows(i)(2))
                    'Next

                    Dim foundRowsRemark As DataRow() = Nothing
                    If Me._DtRM.Rows.Count > 0 Then
                        foundRowsRemark = Me._DtRM.Select(expression)
                    End If

                    For Each Detail As DataRow In foundRows
                        Dim dr As DataRow = _DtTemp.NewRow()

                        For i As Integer = 0 To numberOfColumns
                            '0-97
                            If i <= Convert.ToInt16(AppSettings("numberOfColumnsHeader")) Then
                                dr(i) = Header(i)

                                '98-184
                            ElseIf i <= numberOfColumns - 1 Then
                                dr(i) = Detail(i - (Convert.ToInt16(AppSettings("numberOfColumnsHeader")) + 1))
                                'If i = 184 Then
                                '    MsgBox("s")
                                'End If
                            Else
                                '185
                                If foundRowsRemark IsNot Nothing AndAlso foundRowsRemark.Length > 0 Then
                                    dr(i) = foundRowsRemark(0)("Remark")
                                Else
                                    dr(i) = ""
                                End If

                            End If
                        Next

                        _DtTemp.Rows.Add(dr)
                    Next
                Next

                ImportSO()
                'MsgBox("Import Success.")
            Else
                'MsgBox("Import ERROR.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub ImportSO()
        Try
            Dim objTempTable As New Temp_Table
            Dim GUid = System.Guid.NewGuid.ToString()

            If objTempTable.InsertTemp(_DtTemp, _FileName, GUid) = True Then

                _DtTemp = objTempTable.getFromTempTable(_FileName, GUid)

                Dim objClsimportSo As New clsImportSO
                GetConfig_Default_Import("SO", 10)
                objClsimportSo.Process_ID = 10
                objClsimportSo.File_Prefix = "SO"
                objClsimportSo.DocumentType_Index = DocumentType_Index
                objClsimportSo.ItemStatus_Index = ItemStatus_Index
                objClsimportSo.DtTemp = _DtTemp
                If objClsimportSo.ReadData() = True Then
                    For iRow As Integer = 0 To objClsimportSo.DtHeader.Rows.Count - 1
                        objClsimportSo.SalesOrder_No = objClsimportSo.DtHeader.Rows(iRow)(1).ToString.Trim
                        objClsimportSo.Insert_SO()
                    Next
                    If objClsimportSo.NoSKU = True Then
                        MessageBox.Show("กรุณานำเข้าข้อมูล SKU ก่อนดำเนินการ " & vbNewLine & objClsimportSo.SkuID_Error, "Error SKU", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Else
                        If objClsimportSo.NoPackage = True Then
                            MessageBox.Show("Package ในเอกสารไม่ตรงกับ Package ฐานข้อมูล กรุณาตรวจสอบก่อนดำเนินการ " & vbNewLine & objClsimportSo.SkuID_Error, "Error Package", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                        End If
                    End If

                    For iRow As Integer = 0 To _DtTemp.Rows.Count - 1
                        Dim iDatarow As DataRow = _DtTemp.Rows(iRow)
                        objTempTable.UpdateStatus(iDatarow("GuID").ToString, _FileName, "2")
                        iDatarow = Nothing
                    Next
                    If objClsimportSo.NoSKU = False And objClsimportSo.NoPackage = False Then
                        MessageBox.Show("Import Complete", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            Else
                MsgBox("ไม่สามารถ Import ได้")
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        Finally

        End Try

    End Sub

    Private Sub btnOpenFolder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFolder.Click
        Try
            Me._DtHD = New DataTable
            Me._DtRM = New DataTable
            Me._DtDT = New DataTable
            Dim dialog = New FolderBrowserDialog()
            dialog.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop)
            Dim Result As Windows.Forms.DialogResult = dialog.ShowDialog()
            If Result = Windows.Forms.DialogResult.OK Then
                Me.btnImports.Enabled = False
                Dim _pathHD As String = System.IO.Path.Combine(dialog.SelectedPath(), "SOHD.txt")
                If System.IO.File.Exists(_pathHD) Then
                    Me._DtHD = ConvertToDataTable(_pathHD, AppSettings("numberOfColumnsHeader").ToString)
                    Me.DataGridView1.DataSource = Me._DtHD
                Else
                    Me.DataGridView1.DataSource = Me._DtHD
                End If
                Dim _pathRM As String = System.IO.Path.Combine(dialog.SelectedPath(), "SOHDREMARK.txt")
                If System.IO.File.Exists(_pathRM) Then
                    Me._DtRM = ConvertToDataTable(_pathRM, AppSettings("numberOfColumnsRemark").ToString)
                    Me.DataGridView3.DataSource = Me._DtRM
                Else
                    Me.DataGridView3.DataSource = Me._DtRM
                End If
                Dim _pathDT As String = System.IO.Path.Combine(dialog.SelectedPath(), "SODT.txt")
                If System.IO.File.Exists(_pathDT) Then
                    Me._DtDT = ConvertToDataTable(_pathDT, AppSettings("numberOfColumnsDetail").ToString)
                    Me.DataGridView2.DataSource = Me._DtDT
                Else
                    Me.DataGridView2.DataSource = Me._DtDT
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message.ToString)
        End Try
    End Sub

End Class