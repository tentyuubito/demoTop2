Imports WMS_STD_Formula
Imports System.ComponentModel
Imports System.IO
Imports WMS_STD_Master
Public Class frmImport_SO_New_V2

    Private _isBusy As Boolean = False
    Private _strFileName As String = ""
    Private _dt As DataTable = New DataTable()
    Private _dr As DataRow
    Private _no As Integer

    Private _config_PO_No As Integer = 0
    Private _config_PO_Date As Integer = 0
    Private _config_Supplier_Code As Integer = 0
    Private _config_Supplier_Name As Integer = 0
    Private _config_Supplier_Address As Integer = 0
    Private _config_PO_Exp As Integer = 0
    Private _config_Supplier_Tel As Integer = 0
    Private _config_Supplier_Fax As Integer = 0
    Private _config_Sku_Id As Integer = 0
    Private _config_Sku_Name As Integer = 0
    Private _config_Sku_Package As Integer = 0
    Private _config_Sku_Qty As Integer = 0
    Private _config_Sku_Qty2 As Integer = 0
    Private _config_Sku_Price As Integer = 0
    Private _config_Remark As Integer = 0
    Private _config_Remark2 As Integer = 0

    Public strFileName As String = String.Empty
    Public strGUID As String = String.Empty

    Private _DocumentType_Index As String = String.Empty

    Private Sub btnOpenFileDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenFileDialog.Click
        Dim dialogOpenFiles As New OpenFileDialog
        dialogOpenFiles.Multiselect = True
        dialogOpenFiles.Filter = "ไฟล์แบบที่ 1 xls|*.xls|ไฟล์แบบที่ 2 csv|*.csv|ไฟล์แบบที่ 3 *|*.*"
        dialogOpenFiles.InitialDirectory = ""
        If dialogOpenFiles.ShowDialog Then
            _dt.Rows.Clear()
            _no = _dt.Rows.Count

            Dim CountFile As Integer = 0
            Dim i As String

            For Each i In dialogOpenFiles.FileNames
                _strFileName = i

                CountFile += 1
                GetCSV2DataTable()
                'ReadCSV()
            Next i

            If _dt.Columns.Contains("Order_By") = False Then
                _dt.Columns.Add("Order_By", GetType(Integer))
            End If

            If _dt.Rows.Count > 0 Then
                Dim sku_id As String = ""
                For Each dr As DataRow In _dt.Rows
                    sku_id = ""
                    sku_id = dr("Sku_Id")
                    If sku_id = "" Then Continue For
                    If sku_id.Length < 3 Then Continue For
                    If sku_id(sku_id.Length - 2) = "-" And IsNumeric(sku_id(sku_id.Length - 1)) Then
                        dr("Sku_Id") = sku_id.Remove(sku_id.Length - 2, 2)
                    End If

                    If Not dr("Remark").ToString.Trim = "" Then
                        Select Case dr("Remark").ToString.Substring("0", "2")
                            Case "CM"
                                dr("Order_By") = 0
                            Case "PM"
                                dr("Order_By") = 1
                            Case Else
                                dr("Order_By") = 999
                        End Select
                    Else
                        dr("Order_By") = 999
                    End If

                    'If dr("Remark").ToString.Substring("0", "1") = "CM" Then
                    '    dr("Order_By") = ""
                    'End If
                Next

            End If

            Dim dataView As New DataView(_dt)
            dataView.Sort = "Order_By ASC"
            _dt = dataView.ToTable()



            grdImportPO.DataSource = _dt
            lblOpenFileDialog.Text = "จำนวนไฟล์นำเข้า     " & CountFile.ToString
        End If
    End Sub

    Private Sub GetCSV2DataTable()

        Dim thaiEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("windows-874")
        Dim dt As New DataTable
        For i As Integer = 0 To 200
            dt.Columns.Add(i)
        Next
        Try

            ' _FileName = "D:\CSVFile\SO.csv"

            Using MyReader As New Microsoft.VisualBasic.FileIO.TextFieldParser(_strFileName, thaiEncoding)

                MyReader.TextFieldType = FileIO.FieldType.Delimited
                MyReader.SetDelimiters(",")
                Dim currentRow As String()

                Dim iCol As Integer = 0
                Dim iRows As Integer = 0

                While Not MyReader.EndOfData
                    Try
                        dt.Rows.Add()
                        currentRow = MyReader.ReadFields()
                        iCol = 0
                        Dim currentField As String
                        For Each currentField In currentRow
                            dt.Rows(iRows).Item(iCol) = currentField
                            'MsgBox(currentField)
                            iCol = iCol + 1
                        Next
                        iRows = iRows + 1

                    Catch ex As Microsoft.VisualBasic.FileIO.MalformedLineException
                        '  MsgBox("Line " & ex.Message & "is not valid and will be skipped.")
                    End Try
                End While


            End Using

            Dim check As String = System.IO.Path.GetExtension(_strFileName)
            'grdData.DataSource = dt

            For i As Integer = 0 To dt.Rows.Count - 1

                If dt.Rows(i)(0).ToString.Trim = "" Then Continue For

                _dr = _dt.NewRow()
                _no += 1
                _dr("No") = _no.ToString.Trim
                _dr("Validate") = check.ToString.Trim
                _dr("SOLD_TO_ID") = IIf(_config_PO_No = 0, "", dt.Rows(i)(_config_PO_No).ToString.Replace("""", "").Trim)
                _dr("SO_NO") = IIf(_config_PO_Date = 0, "", dt.Rows(i)(_config_PO_Date).ToString.Replace("""", "").Trim)
                _dr("SOLD_TO_NAME") = IIf(_config_Supplier_Code = 0, "", dt.Rows(i)(_config_Supplier_Code).ToString.Replace("""", "").Trim)
                _dr("DOC_DATE") = IIf(_config_Supplier_Name = 0, Now.ToString("dd/MM/yyyy"), dt.Rows(i)(_config_Supplier_Name).ToString.Replace("""", "").Trim)

                If (CDate(_dr("DOC_DATE")).Year) > 2500 Then
                    _dr("DOC_DATE") = (CDate(_dr("DOC_DATE")).AddYears(-543))
                End If

                _dr("SOLD_TO_ADD1") = IIf(_config_Supplier_Address = 0, "", dt.Rows(i)(_config_Supplier_Address).ToString.Replace("""", "").Trim)
                _dr("SALE_NAME") = IIf(_config_PO_Exp = 0, "", dt.Rows(i)(_config_PO_Exp).ToString.Replace("""", "").Trim)
                _dr("SOLD_TO_ADD2") = IIf(_config_Supplier_Tel = 0, "", dt.Rows(i)(_config_Supplier_Tel).ToString.Replace("""", "").Trim)
                _dr("SOLD_TO_TEL") = IIf(_config_Supplier_Fax = 0, "", dt.Rows(i)(_config_Supplier_Fax).ToString.Replace("""", "").Trim)
                _dr("EXPECT_DELIVERY_DATE") = IIf(_config_Sku_Id = 0, Now.ToString("dd/MM/yyyy"), dt.Rows(i)(_config_Sku_Id).ToString.Replace("""", "").Trim)

                If (CDate(_dr("EXPECT_DELIVERY_DATE")).Year) > 2500 Then
                    _dr("EXPECT_DELIVERY_DATE") = (CDate(_dr("EXPECT_DELIVERY_DATE")).AddYears(-543))
                End If

                _dr("SEQ") = IIf(_config_Sku_Name = 0, 0, dt.Rows(i)(_config_Sku_Name).ToString.Replace("""", "").Trim)
                _dr("SKU_ID") = IIf(_config_Sku_Package = 0, "", dt.Rows(i)(_config_Sku_Package).ToString.Replace("""", "").Trim)

                _dr("QTY") = IIf(_config_Sku_Qty = 0, 0, dt.Rows(i)(_config_Sku_Qty).ToString.Replace("""", "").Trim)
                If IsNumeric(_dr("QTY")) Then
                    If _dr("QTY") = 0 Then
                        _dr("QTY") = dt.Rows(i)(_config_Sku_Qty2).ToString.Replace("""", "").Trim
                    End If
                End If

                'HACK: ใบจองสินค้า ตรวจสอบของพรีเมียม
                Dim MsgVslidate As String = ""
                Select Case Me.ddlFormat_Import.SelectedValue
                    Case Nothing
                    Case "0010000000001"
                        'ตรวจสอบข้อมูลช่องที่ 2 ถ้าเป็นจำนวนที่มากกว่า 0 ช่องต่อจากหน่วยสินค้าต้องเป็น (PM : Premiam)
                        If IsNumeric(dt.Rows(i)(_config_Sku_Qty2).ToString.Replace("""", "").Trim) Then
                            If dt.Rows(i)(_config_Sku_Qty2).ToString.Replace("""", "").Trim > 0 Then
                                If dt.Rows(i)(_config_Sku_Price + 1).ToString.Replace("""", "").Trim <> "PM" Then
                                    MsgVslidate = "กรุณาตรวจสอบเอกสาร : " & Me.ddlFormat_Import.Text
                                    MsgVslidate &= Chr(13) & "รหัสสินค้า : " & _dr("SKU_ID").ToString
                                    MsgVslidate &= Chr(13) & "จำนวน : " & dt.Rows(i)(_config_Sku_Qty2).ToString.Replace("""", "").Trim
                                    MsgVslidate &= Chr(13) & "ประเภท : " & dt.Rows(i)(_config_Sku_Price + 1).ToString.Replace("""", "").Trim
                                    MsgVslidate &= Chr(13) & " *** ของพรีเมียม ประเภทต้องเป็น PM "
                                    W_Language.W_MSG_Error(MsgVslidate)
                                    Exit Sub
                                End If
                            End If
                        End If

                        If dt.Rows(i)(_config_Sku_Price + 1).ToString.Replace("""", "").Trim = "PM" Then
                            Continue For
                        End If

                End Select


                _dr("PACKAGE") = IIf(_config_Sku_Price = 0, "", dt.Rows(i)(_config_Sku_Price).ToString.Replace("""", "").Trim)
                '_dr("Remark") = IIf(_config_Remark2 = 0, "", dt.Rows(i)(_config_Remark2).ToString.Replace("""", "").Trim & " : " & dt.Rows(i)(_config_Remark).ToString.Replace("""", "").Trim)
                _dr("Remark") = IIf(_config_Remark = 0, "", dt.Rows(i)(_config_Remark).ToString.Replace("""", "").Trim)
                _dt.Rows.Add(_dr)

            Next
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub ReadCSV()
        Dim thaiEncoding As System.Text.Encoding = System.Text.Encoding.GetEncoding("windows-874")
        'Dim sr As StreamReader = New StreamReader("C:\Filename.txt", thaiEncoding)
        Dim SR As StreamReader = New StreamReader(_strFileName, thaiEncoding)
        Dim i As Long = 0
        Dim line As String = SR.ReadLine
        Dim strArray As String() = line.Split(",")

        'My.Computer.FileSystem.GetFiles("C:\TestDir")
        Dim check As String = System.IO.Path.GetExtension(_strFileName)
        'MsgBox("The file extension is " & check)

        _dr = _dt.NewRow()
        _no += 1
        _dr("No") = _no.ToString.Trim
        _dr("Validate") = check.ToString.Trim
        _dr("PO_No") = strArray(_config_PO_No).ToString.Replace("""", "").Trim
        _dr("PO_Date") = strArray(_config_PO_Date).ToString.Replace("""", "").Trim
        _dr("Supplier_Code") = strArray(_config_Supplier_Code).ToString.Replace("""", "").Trim
        _dr("Supplier_Name") = strArray(_config_Supplier_Name).ToString.Replace("""", "").Trim
        _dr("Supplier_Address") = strArray(_config_Supplier_Address).ToString.Replace("""", "").Trim
        _dr("PO_Exp") = strArray(_config_PO_Exp).ToString.Replace("""", "").Trim
        _dr("Supplier_Tel") = strArray(_config_Supplier_Tel).ToString.Replace("""", "").Trim
        _dr("Supplier_Fax") = strArray(_config_Supplier_Fax).ToString.Replace("""", "").Trim

        Do
            line = SR.ReadLine
            If line <> Nothing Then
                strArray = line.Split(",")
                If strArray.Length = 38 Then
                    _dr = _dt.NewRow
                    _no += 1
                    _dr("No") = _no.ToString.Trim
                    check = System.IO.Path.GetExtension(_strFileName)
                    _dr("Validate") = check.ToString.Trim
                    _dr("PO_No") = strArray(_config_PO_No).ToString.Replace("""", "").Trim
                    _dr("PO_Date") = strArray(_config_PO_Date).ToString.Replace("""", "").Trim
                    _dr("Supplier_Code") = strArray(_config_Supplier_Code).ToString.Replace("""", "").Trim
                    _dr("Supplier_Name") = strArray(_config_Supplier_Name).ToString.Replace("""", "").Trim
                    _dr("Supplier_Address") = strArray(_config_Supplier_Address).ToString.Replace("""", "").Trim
                    _dr("PO_Exp") = strArray(_config_PO_Exp).ToString.Replace("""", "").Trim
                    _dr("Supplier_Tel") = strArray(_config_Supplier_Tel).ToString.Replace("""", "").Trim
                    _dr("Supplier_Fax") = strArray(_config_Supplier_Fax).ToString.Replace("""", "").Trim
                Else
                    _dr("Sku_Id") = strArray(_config_Sku_Id).ToString.Replace("""", "").Trim
                    _dr("Sku_Name") = strArray(_config_Sku_Name).ToString.Replace("""", "").Trim
                    _dr("Sku_Package") = strArray(_config_Sku_Package).ToString.Replace("""", "").Trim
                    _dr("Sku_Qty") = strArray(_config_Sku_Qty).ToString.Replace("""", "").Trim
                    _dr("Sku_Price") = strArray(_config_Sku_Price).ToString.Replace("""", "").Trim
                    _dr("Remark") = strArray(_config_Remark).ToString.Replace("""", "").Trim

                    _dt.Rows.Add(_dr)
                    'Else
                    '    _dr = _dt.NewRow
                    '    _no += 1
                    '    _dr("No") = _no.ToString
                    '    check = System.IO.Path.GetExtension(_strFileName)
                    '    _dr("Validate") = check.ToString
                    '    _dr("PO_No") = strArray(_config_PO_No).ToString
                    '    _dr("PO_Date") = strArray(_config_PO_Date).ToString
                    '    _dr("Supplier_Code") = strArray(_config_Supplier_Code).ToString
                    '    _dr("Supplier_Name") = strArray(_config_Supplier_Name).ToString
                    '    _dr("Supplier_Address") = strArray(_config_Supplier_Address).ToString
                    '    _dr("PO_Exp") = strArray(_config_PO_Exp).ToString
                    '    _dr("Supplier_Tel") = strArray(_config_Supplier_Tel).ToString
                    '    _dr("Supplier_Fax") = strArray(_config_Supplier_Fax).ToString
                    '    _dr("Sku_Id") = strArray(_config_Sku_Id).ToString
                    '    _dr("Sku_Name") = strArray(_config_Sku_Name).ToString
                    '    _dr("Sku_Package") = strArray(_config_Sku_Package).ToString
                    '    _dr("Sku_Qty") = strArray(_config_Sku_Qty).ToString
                    '    _dr("Sku_Price") = strArray(_config_Sku_Price).ToString
                    '    _dr("Remark") = strArray(_config_Remark).ToString

                    '    _dt.Rows.Add(_dr)
                End If
            End If
        Loop While Not line = String.Empty
    End Sub

    Private Sub frmImport_PO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdImportPO.AutoGenerateColumns = False
            Me.btnSave.Enabled = False
            Timer1.Start()
            'Dim ObjImportPO As New bl_Import_PO
            'ObjImportPO.GetVIEW_Import_PO()
            '_dt = ObjImportPO.DataTable
            '_dt.Rows.Clear()

            Dim ObjImportSO As New bl_Import_SO_V2
            ObjImportSO.GetVIEW_Import_SO()
            _dt = ObjImportSO.DataTable
            _dt.Rows.Clear()

            Me.Load_Config_Format_Import()

            'LoadConfigCSV()

            GetConfig_Default_Import()
            GetDateServer()

        Catch ex As Exception
            W_Language.W_MSG_Error(ex.Message.ToString)
        End Try



    End Sub

    Private Sub Load_Config_Format_Import()
        Try
            Dim ObjImportSO As New bl_Import_SO_V2
            ObjImportSO.Getconfig_Format_Import()
            Dim dt As New DataTable
            dt = ObjImportSO.DataTable

            With Me.ddlFormat_Import
                .DisplayMember = "Format_Import"
                .ValueMember = "Format_Import_Index"
                .DataSource = dt
            End With

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub LoadConfigCSV()
        Try

            Dim drArrDoc() As DataRow
            drArrDoc = CType(Me.ddlFormat_Import.DataSource, DataTable).Select("Format_Import_Index='" & Me.ddlFormat_Import.SelectedValue & "'")
            Me._DocumentType_Index = drArrDoc(0)("DocumentType_Index").ToString

            Dim ObjImportSO As New bl_Import_SO_V2
            ObjImportSO.GetConfig_Import_SO(Me.ddlFormat_Import.SelectedValue)
            Dim dt As New DataTable
            dt = ObjImportSO.DataTable
            If dt.Rows.Count = 0 Then Exit Sub
            Me._config_PO_No = dt.Rows(0).Item("SOLD_TO_ID").ToString
            Me._config_PO_Date = dt.Rows(0).Item("SO_NO").ToString
            Me._config_Supplier_Code = dt.Rows(0).Item("SOLD_TO_NAME").ToString
            Me._config_Supplier_Name = dt.Rows(0).Item("DOC_DATE").ToString
            Me._config_Supplier_Address = dt.Rows(0).Item("SOLD_TO_ADD1").ToString
            Me._config_PO_Exp = dt.Rows(0).Item("SALE_NAME").ToString
            Me._config_Supplier_Tel = dt.Rows(0).Item("SOLD_TO_ADD2").ToString
            Me._config_Supplier_Fax = dt.Rows(0).Item("SOLD_TO_TEL").ToString
            Me._config_Sku_Id = dt.Rows(0).Item("EXPECT_DELIVERY_DATE").ToString
            Me._config_Sku_Name = dt.Rows(0).Item("SEQ").ToString
            Me._config_Sku_Package = dt.Rows(0).Item("SKU_ID").ToString
            Me._config_Sku_Qty = dt.Rows(0).Item("QTY").ToString
            '_config_Sku_Qty2 = dt.Rows(0).Item("QTY2").ToString
            Me._config_Sku_Price = dt.Rows(0).Item("PACKAGE").ToString
            Me._config_Remark = dt.Rows(0).Item("Remark").ToString
            '_config_Remark2 = dt.Rows(0).Item("Remark2").ToString


        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnValidate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnValidate.Click
        Try


            '_isBusy = True
            'Timer1.Start()

            'Dim CountError As Integer = 0
            'For i As Integer = 0 To grdImportPO.Rows.Count - 1
            '    If grdImportPO.Rows(i).Cells("col_No").Value <> "" Then
            '        If grdImportPO.Rows(i).Cells("col_Validate").Value = ".csv" Then
            '            For Each col As DataColumn In _dt.Columns
            '                If grdImportPO.Rows(i).Cells("col_" & col.ColumnName).Value <> Nothing Then

            '                    If grdImportPO.Rows(i).DefaultCellStyle.BackColor <> Color.Red Then
            '                        grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
            '                    End If
            '                    Me.btnSave.Enabled = True
            '                Else
            '                    If col.ColumnName <> "Remark" Then
            '                        grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
            '                        grdImportPO.Rows(i).Cells("col_Remark").Value = "*ข้อมูลไม่ครบ*"
            '                        CountError += 1
            '                        Me.btnSave.Enabled = False
            '                    End If
            '                End If
            '            Next
            '        Else
            '            'format ไม่ถูกต้อง
            '            grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
            '            grdImportPO.Rows(i).Cells("col_Remark").Value = "*Format File ไม่ถูกต้อง*"
            '            CountError += 1
            '            Me.btnSave.Enabled = False
            '        End If
            '    End If
            'Next

            'lblFile.Text = "จำนวนรายการ     " & _dt.Rows.Count
            'lblError.Text = "จำนวนผิดพลาด    " & CountError
            '_isBusy = False


            _isBusy = True
            Me.btnSave.Enabled = False
            Timer1.Start()

            If Me.grdImportPO.Rows.Count > 1 Then
                DirectCast(Me.grdImportPO.DataSource, DataTable).AcceptChanges()
            End If

            Dim CountError As Integer = 0
            Dim CheckError As Boolean = False
            For i As Integer = 0 To grdImportPO.Rows.Count - 2
                grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = ""
                grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.White
                CheckError = False
                If grdImportPO.Rows(i).Cells("col_No").Value <> "" Then
                    If grdImportPO.Rows(i).Cells("col_Validate").Value = ".csv" Then
                        For Each col As DataColumn In _dt.Columns
                            If (grdImportPO.Rows(i).Cells("col_" & col.ColumnName).Value <> Nothing) Or (col.ColumnName <> "") Then

                                ' If grdImportPO.Rows(i).DefaultCellStyle.BackColor <> Color.Red Then
                                'grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
                                '  End If
                                ' Me.btnSave.Enabled = True
                            Else
                                If (col.ColumnName <> "Remark") Then
                                    'grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                                    grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = "*ข้อมูลไม่ครบ*"
                                    CountError += 1
                                    CheckError = True
                                    '  Me.btnSave.Enabled = False
                                End If
                            End If
                        Next
                    Else
                        'format ไม่ถูกต้อง
                        '  grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                        grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = "*Format File ไม่ถูกต้อง*"
                        CountError += 1
                        CheckError = True
                        'Me.btnSave.Enabled = False
                    End If

                    'col_Sku_Qty
                    If IsNumeric(grdImportPO.Rows(i).Cells("col_QTY").Value.ToString.Trim) = False Then
                        ' grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                        grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* จำนวนไม่ถูกต้อง!!! *"
                        CountError += 1
                        CheckError = True
                    End If

                    'col_DOC_DATE
                    If IsDate(grdImportPO.Rows(i).Cells("col_DOC_DATE").Value.ToString.Trim) = False Then
                        grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* วันที่เอกสาร ถูกต้อง!!! *"
                        CountError += 1
                        CheckError = True
                    End If

                    'col_EXPECT_DELIVERY_DATE

                    If IsDate(grdImportPO.Rows(i).Cells("col_EXPECT_DELIVERY_DATE").Value.ToString.Trim) = False Then
                        grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* วันที่ครบกำหนดส่ง ถูกต้อง!!! *"
                        CountError += 1
                        CheckError = True
                    End If

                    'Col_SO_NO
                    If grdImportPO.Rows(i).Cells("Col_SO_NO").Value.ToString.Trim <> "" Then
                        Dim objCheck As New bl_Import_SO_V2
                        If objCheck.fnExitsDocNoDup("tb_SalesOrder", "SalesOrder_No", grdImportPO.Rows(i).Cells("Col_SO_NO").Value.ToString.Trim, " AND Status <> -1 ") Then
                            grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* เลขที่เอกสารซ้ำในระบบ!!! *"
                            CountError += 1
                            CheckError = True
                        End If
                    End If









                    'Recheck Master Sku and Package  Request By p'Dong 2014-09-16 Edit by Neung 2014-09-16
                    Dim objImport As New bl_Import_SO_V2
                    Dim obDTSKUPACKAGE As New DataTable

                    If grdImportPO.Rows(i).Cells("col_SKU_ID").Value.ToString.Trim <> "" Then
                        Dim sku_id$ = ""
                        sku_id = grdImportPO.Rows(i).Cells("col_SKU_ID").Value.ToString.Trim()
                        obDTSKUPACKAGE = objImport.GetDataSKUPACKAGEByBarcode(sku_id)

                        If obDTSKUPACKAGE.Rows.Count > 0 Then
                            If grdImportPO.Rows(i).Cells("col_PACKAGE").Value.ToString.Trim <> "" Then
                                Dim package$ = ""
                                package = grdImportPO.Rows(i).Cells("col_PACKAGE").Value.ToString.Trim()
                                If obDTSKUPACKAGE.Rows.Count > 0 Then
                                    Dim drr() As DataRow
                                    drr = obDTSKUPACKAGE.Select(String.Format(" Package_Id = '{0}'", package))
                                    If drr.Length = 0 Then
                                        ' grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                                        grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* ไม่พบหน่วยสินค้านี้!!! *"
                                        CountError += 1
                                        CheckError = True
                                        '  Me.btnSave.Enabled = False
                                    End If

                                Else
                                    ' grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                                    grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* ไม่พบหน่วยสินค้านี้!!! *"
                                    CountError += 1
                                    CheckError = True
                                    '  Me.btnSave.Enabled = False

                                End If
                            End If
                        Else

                            'grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                            grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* ไม่พบสินค้า!!! *"
                            CountError += 1
                            CheckError = True
                            '  Me.btnSave.Enabled = False

                        End If
                    Else
                        ' grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                        grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value = grdImportPO.Rows(i).Cells("Col_ValidateRamark").Value.ToString & "* ไม่พบสินค้า!!! *"
                        CountError += 1
                        CheckError = True
                        ' Me.btnSave.Enabled = False

                    End If

                End If

                If CheckError Then
                    grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red
                Else
                    grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.GreenYellow
                End If

            Next
            If CountError > 0 Or Me.grdImportPO.RowCount = 1 Then
                Me.btnSave.Enabled = False
            Else
                Me.btnSave.Enabled = True
            End If


            lblFile.Text = "จำนวนรายการ     " & _dt.Rows.Count
            lblError.Text = "จำนวนผิดพลาด    " & CountError
            _isBusy = False
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try

    End Sub

    Private Sub grdImportPO_CellToolTipTextNeeded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellToolTipTextNeededEventArgs) Handles grdImportPO.CellToolTipTextNeeded
        If (e.RowIndex <> -1) Then
            If grdImportPO.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Red Then
                e.ToolTipText = Me.grdImportPO.Rows(e.RowIndex).Cells("col_Remark").Value
            End If
        End If

    End Sub

    Private Sub btnConfigCSV_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfigCSV.Click

        Try
            Dim objConfigImportSO As New frmConfigImport_SO
            objConfigImportSO.Format_Import_Index = Me.ddlFormat_Import.SelectedValue
            objConfigImportSO.ShowDialog()
            LoadConfigCSV()

            Me.grdImportPO.DataSource = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        btnValidate_Click(sender, e)
        If btnSave.Enabled = False Then Exit Sub


        _isBusy = True
        'If Me._isBusy = False Then
        '    MessageBox.Show("กรุณาตรวจสอบข้อมูลก่อน")
        '    Exit Sub
        'End If
        Timer1.Start()
        Dim ObjReadFile As New clsReadFile



        Try
            For i As Integer = 0 To grdImportPO.Rows.Count - 1
                If (grdImportPO.Rows(i).DefaultCellStyle.BackColor = Color.Red) Then
                    MessageBox.Show("มีไฟล์ที่ผิดพลาด ไม่สามารถนำข้อมูลเข้าได้")
                    grdImportPO.DataSource = Nothing

                    _isBusy = False
                    Exit Sub
                End If
            Next


            'Generate GUID
            strGUID = ObjReadFile.GenGUID()

            ' Insert Data to temp table
            _dt = Nothing
            _dt = grdImportPO.DataSource
            InsertTemp(_dt, _strFileName, strGUID)
            Dim _dtData As New DataTable
            ' get Data from Temp table And Update Status in Temp table
            _dtData = GetTmpDataUpdateStatus(_strFileName, strGUID)
            ' Insert data 
            Insert_SO(_dtData)
            'ImportSO()

            _isBusy = False
            MessageBox.Show("นำเข้าข้อมูลเรียบร้อย")
            grdImportPO.DataSource = Nothing

            lblFile.Text = "จำนวนรายการ     " & 0
            lblError.Text = "จำนวนผิดพลาด    " & 0

            Me.btnSave.Enabled = False
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
            _isBusy = False
        End Try


    End Sub

    Public Function InsertTemp(ByVal DtDataToInsertTemp As DataTable, ByVal strFileName As String, ByVal strGUid As String) As Boolean
        Try
            Dim objDB As New SaveData_Import
            objDB.DtTemp = DtDataToInsertTemp
            objDB.FileName = strFileName
            objDB.GUid = strGUid

            If objDB.InsertTemp() Then
                Return True
                'objDB.Status = 2 'จองเพื่อ  Insert Temp To PO
                'If objDB.UpdateStatus() Then
                'If Insert_PO(DtDataToInsertTemp) Then
                '    'Insert Complete
                'Else
                '    'Insert Error
                'End If
                'Else
                '    'Update Error
                'End If
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetTmpDataUpdateStatus(ByVal strFileName As String, ByVal strGUid As String) As DataTable
        Try
            Dim objDB As New SaveData_Import
            objDB.FileName = strFileName
            objDB.GUid = strGUid


            Return objDB.GetTmpDataUpdateStatus(strGUid, strFileName)

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'Public Function Insert_PO(ByVal DtDataTemp As DataTable) As Boolean
    '    Try
    '        Dim ObjImport_PO As New Import_PO
    '        ObjImport_PO.DtTemp = DtDataTemp
    '        Return ObjImport_PO.ReadDetail()
    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Function

    Public Function Insert_SO(ByVal DtDataTemp As DataTable) As Boolean
        'Try
        '    Dim ObjImport_PO As New Import_PO
        '    ObjImport_PO.DtTemp = DtDataTemp
        '    Return ObjImport_PO.ReadDetail()
        'Catch ex As Exception
        '    Throw ex
        'End Try

        Dim objImportSO As New clsImportSO_V2
        objImportSO.DtTemp = DtDataTemp
        objImportSO.GUid = strGUID
        objImportSO.ReadData()

        Dim DTHead As New DataTable
        Dim DTDetail As New DataTable

        DTHead = objImportSO._dtHeader
        DTDetail = objImportSO._dtDetail

        For index As Integer = 0 To DTHead.Rows.Count - 1
            objImportSO.SalesOrder_No = DTHead.Rows(index).Item("SalesOrder_No")
            objImportSO.DocumentType_Index = Me._DocumentType_Index ' New Req 2015/02/10

            If Not objImportSO.Insert_SO() Then
                Continue For
            End If
        Next

    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        ProgressBar1.Visible = True
        If ProgressBar1.Value < 100 Then
            ProgressBar1.Value += 1
        End If
        If _isBusy = False Then
            Timer1.Stop()
            ProgressBar1.Visible = False
        End If
    End Sub

    Private Sub ddlFormat_Import_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFormat_Import.SelectedIndexChanged
        Try
            Me.LoadConfigCSV()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub ddlFormat_Import_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ddlFormat_Import.SelectionChangeCommitted
        Try
            Me.grdImportPO.DataSource = Nothing
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub
End Class