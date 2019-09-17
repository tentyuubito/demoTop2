Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Imports System.ComponentModel
Imports WMS_STD_Formula
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Public Class frmImport_Order_StockBalance_std
    Private _boolResult As Boolean = False
    Private _dtDataSource As New DataTable
    Private _tmpRef_No3 As String = ""
    Private _Massege As String = ""

    Private Sub frmImport_Order_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Load Document Type (Receipt = 1)
        Me.grdPreviewData.AutoGenerateColumns = False
        Me.txtCustomer_Id.Tag = ""
        Dim oms_DocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        oms_DocumentType.getDocumentType(1)

        With cboDocumentType
            .DisplayMember = "Description"
            .ValueMember = "DocumentType_Index"
            .DataSource = oms_DocumentType.DataTable
        End With

        ''Load Customer 
        'Dim oms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        'oms_Customer.SearchData_Click("", " and status <> -1")

        'With cboCustomer
        '    .DisplayMember = "Customer_Name"
        '    .ValueMember = "Customer_Index"
        '    .DataSource = oms_Customer.GetDataTable
        'End With

        'Load Item Status
        Dim objDocumentType_Itemstatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
        objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocumentType.SelectedValue)

        With cboItemStatus
            .DisplayMember = "ItemStatusDes"
            .ValueMember = "ItemStatus_Index"
            .DataSource = objDocumentType_Itemstatus.DataTable
        End With

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            'checking 
            If Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาระบุ Work Sheet")
                Exit Sub
            End If

            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                'load data from excel
                Dim dtDataSource As New DataTable
                dtDataSource = LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
                Me.grdPreviewData.DataSource = dtDataSource
            End If

            'Epson_ProductGroup_Index
            Me.SetnumRows(True)
            Me.btnImport.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub
    Public Function LoadDataFromFile(ByVal pstrFileName As String, ByVal pstrWorkSheet As String) As DataTable
        Try


            Dim strConnString As String
            Dim odtTemp As New DataTable
            strConnString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & pstrFileName & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"

            If pstrFileName = "" Then
                Return odtTemp
            End If

            Dim oConnSource As New OleDb.OleDbConnection(strConnString)
            Dim odaSource As New OleDb.OleDbDataAdapter

            Dim strSQL As String = ""

            With odaSource
                strSQL = " SELECT '' AS Check_Data,'0.00' as Weight_PerPackage,'0.00' as M3_PerPackage,'' as Customer_Index,'' as ItemStatus_Index ,'' as DocumentType_Index,'' as Package_Index,'' as Sku_Index,'' as Epson_ProductGroup_Index "
                strSQL &= " ,'' as Supplier_Index,0 as Line_No,*"
                strSQL &= " FROM [" & pstrWorkSheet & "$] "
                strSQL &= " WHERE Sku_Id <> ''  "
                strSQL &= " ORDER BY Order_Date asc,Ref_No3 asc,SKU_Id asc,Plot asc"
                .SelectCommand = New OleDb.OleDbCommand(strSQL, oConnSource)
                .Fill(odtTemp)
            End With

            '.Cells("Customer_Index").Value = Me.cboCustomer.SelectedValue
            '.Cells("ItemStatus_Index").Value = Me.cboItemStatus.SelectedValue
            '.Cells("DocumentType_Index").Value = Me.cboDocumentType.SelectedValue
            '.Cells("Package_Index").Value()
            '.Cells("Sku_Index").Value()

            Return odtTemp

        Catch ex As Exception
            Throw ex
        End Try

    End Function


    Sub SetnumRows(Optional ByVal boolLoad As Boolean = False)
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = Me.grdPreviewData.Rows.Count
        If numRows > 0 Then

            Dim drArrFail() As DataRow = CType(Me.grdPreviewData.DataSource, DataTable).Select("Check_Data <> 'OK'")
            If drArrFail.Length > 0 Then
                lblCountRows.Text = "พบจำนวน " & numRows & " รายการ " & " ผิดพลาด " & drArrFail.Length & " รายการ "
                If drArrFail.Length = 0 Then Exit Sub
                If Not boolLoad Then
                    For Each drError As DataRow In drArrFail
                        lblCountRows.Text &= drError("Line_No") & ","
                    Next

                End If


            Else
                lblCountRows.Text = "พบจำนวน " & numRows & " รายการ " & " ผิดพลาด 0 รายการ "
            End If

        Else
            lblCountRows.Text = "ไม่พบรายการ"
        End If
    End Sub

    Private Sub btnCheckData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCheckData.Click
        Try
            If Me.txtCustomer_Id.Tag = "" Then
                W_MSG_Error("กรุณาป้อนระบุ : " & Me.Label6.Text)
                Exit Sub
            End If

            If Me.txtSupplier_Id.Tag = "" Then
                W_MSG_Error("กรุณาป้อนระบุ : " & Me.lblSupplier_Index.Text)
                Exit Sub
            End If

            'checking
            If Me.txtFilePath.Text.Trim = "" Or Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
                Exit Sub
            End If

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If

            Me.ManageButtom(False)
            Me.Cursor = Cursors.WaitCursor
            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            If Me.CheckingData() = "OK" Then
                W_MSG_Information("ข้อมูลถูกต้อง นำเข้าข้อมูลได้")
                SetnumRows()
                lblCountRows.Text = "พบจำนวน " & Me.grdPreviewData.RowCount & " รายการ " & " ผิดพลาด 0 รายการ "
                Me.btnImport.Enabled = True
            Else
                W_MSG_Error("พบข้อผิดพลาด ไม่สามารถนำเข้าข้อมูลได้")
                SetnumRows()
                Me.btnImport.Enabled = False
            End If


        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        Finally

            Me.ManageButtom(True)
            Me.Cursor = Cursors.Default

        End Try
    End Sub

    Public Function CheckingData() As String
        Dim strResult As String = "OK"
        Try
            'Dim oImport As New Import_Order_StockBalance
            Dim tempDataSku As New DataTable
            Dim strLocation As String = ""
            Dim strEpson_ProductGroup_Index As String = ""
            Dim tempItemStatus As New DataTable
            For i As Integer = 0 To Me.grdPreviewData.RowCount - 1
                'tempItemStatus = ""
                With grdPreviewData.Rows(i)
                    'Default
                    .Cells("col_No").Value = i + 1

                    .Cells("Customer_Index").Value = Me.txtCustomer_Id.Tag
                    .Cells("Supplier_Index").Value = Me.txtSupplier_Id.Tag
                    '.Cells("ItemStatus_Index").Value = Me.cboItemStatus.SelectedValue
                    .Cells("DocumentType_Index").Value = Me.cboDocumentType.SelectedValue

                    'Item Status
                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("ItemStatus").Value, GetType(String)) = "" Then
                        .Cells("ItemStatus_Index").Value = Me.cboItemStatus.SelectedValue
                    Else
                        tempItemStatus = GetItemStatus(.Cells("ItemStatus").Value)
                        If tempItemStatus.Rows.Count = 0 Then
                            .Cells("Check_Data").Value = "ไม่พบ Status นี้ !: " & .Cells("ItemStatus").Value
                            .Cells("Check_Data").Style.BackColor = Color.Red
                            strResult = .Cells("Check_Data").Value
                            Continue For
                        Else
                            .Cells("ItemStatus_Index").Value = tempItemStatus.Rows(0).Item("ItemStatus_Index").ToString
                        End If
                    End If

                    'Sku and Package
                    If .Cells("Sku_Id").Value = "ALUMINIUM (ADC-12) " Then
                        .Cells("Sku_Id").Value = "ALUMINIUM (ADC-12) "
                    End If
                    tempDataSku = IsExitDataSku_ByCustomer(.Cells("Sku_Id").Value, Me.txtCustomer_Id.Tag)
                    If tempDataSku.Rows.Count = 0 Then
                        .Cells("Check_Data").Value = "ไม่พบข้อมูล SKU นี้ ! ของลูกค้า : " & Me.txtCustomer_Name.Text
                        .Cells("Check_Data").Style.BackColor = Color.Red
                        strResult = .Cells("Check_Data").Value
                        Continue For
                    Else
                        .Cells("Sku_Index").Value = tempDataSku.Rows(0).Item("Sku_Index").ToString
                        .Cells("Package_Index").Value = tempDataSku.Rows(0).Item("Package_Index").ToString
                    End If

                    'ProductType
                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Epson_ProductGroup").Value, GetType(String)) = "" Then
                        .Cells("Epson_ProductGroup_Index").Value = 0
                    Else
                        strEpson_ProductGroup_Index = GetIndexByID("ms_ProductType", "ProductType_Index", "Description", .Cells("Epson_ProductGroup").Value)
                        If strEpson_ProductGroup_Index = "" Then
                            .Cells("Check_Data").Value = "กรุณาใหม่ป้อน Epson_ProductGroup ให้ถูกต้อง"
                            .Cells("Check_Data").Style.BackColor = Color.Red
                            strResult = .Cells("Check_Data").Value
                            Continue For
                        Else
                            .Cells("Epson_ProductGroup_Index").Value = strEpson_ProductGroup_Index
                        End If
                    End If

                    'Location
                    strLocation = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Str4").Value, GetType(String))
                    If getLocation_Index(strLocation) = "" Then
                        .Cells("Check_Data").Value = "กรุณาใหม่ป้อนตำแหน่งให้ถูกต้อง"
                        .Cells("Check_Data").Style.BackColor = Color.Red
                        strResult = .Cells("Check_Data").Value
                        Continue For
                    Else
                        .Cells("Check_Data").Value = "OK"
                    End If

                    'Qty
                    If IsNumeric(.Cells("Qty").Value) = False Then
                        .Cells("Check_Data").Value = "กรุณาใหม่ป้อนจำนวนให้ถูกต้อง"
                        .Cells("Check_Data").Style.BackColor = Color.Red
                        strResult = .Cells("Check_Data").Value
                        Continue For
                    End If

                    'Order_Date
                    If IsDate(.Cells("Order_Date").Value) = False Then
                        .Cells("Check_Data").Value = "กรุณาตรวจสอบวันที่รับ"
                        .Cells("Check_Data").Style.BackColor = Color.Red
                        strResult = .Cells("Check_Data").Value
                        Continue For
                    End If

                    'Exp_Date
                    Dim strExpdate As String = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Exp_Date").Value, GetType(String)).ToString
                    If strExpdate <> "" Then
                        If IsDate(.Cells("Exp_Date").Value) = False Then
                            .Cells("Check_Data").Value = "กรุณาตรวจสอบวันที่หมดอายุ"
                            .Cells("Check_Data").Style.BackColor = Color.Red
                            strResult = .Cells("Check_Data").Value
                            Continue For
                        End If
                    End If

                    'Weight
                    If IsNumeric(.Cells("Weight").Value) = False Then
                        '.Cells("Weight").Value = 0
                        getSKU_Detail(.Cells("Sku_Id").Value, i, .Cells("Customer_Index").Value, "W")
                    End If

                    'Net Weight
                    If IsNumeric(.Cells("Flo1").Value) = False Then
                        '.Cells("Flo1").Value = 0
                        'getSKU_Detail(.Cells("Sku_Id").Value, i, .Cells("Customer_Index").Value, "NW")
                        .Cells("Flo1").Value = .Cells("Weight").Value
                    End If

                    'M3
                    If IsNumeric(.Cells("M3").Value) = False Then
                        '.Cells("M3").Value = 0
                        getSKU_Detail(.Cells("Sku_Id").Value, i, .Cells("Customer_Index").Value, "M3")
                    End If


                    'Ref_No2
                    .Cells("Ref_No2").Value = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Ref_No2").Value, GetType(String))
                    'strResult = "กรุณาตรวจสอบ Ref_No2 ที่ Exel ให้ข้อมูลเป็น Text"
                    'Ref_No3
                    .Cells("Ref_No3").Value = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Ref_No3").Value, GetType(String))
                    'strResult = "กรุณาตรวจสอบ Ref_No3 ที่ Exel ให้ข้อมูลเป็น Text"
                    'Plot
                    .Cells("Plot").Value = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Cells("Plot").Value, GetType(String))
                    'strResult = "กรุณาตรวจสอบ Ref_No4 ที่ Exel ให้ข้อมูลเป็น Text"

                    'PASS
                    .Cells("Check_Data").Value = "OK"
                    .Cells("Check_Data").Style.BackColor = Color.White
                End With
            Next


            Return strResult

        Catch ex As Exception
            Return strResult
        End Try
    End Function

    Private Sub getSKU_Detail(ByVal Sku_Id As String, ByVal RowIndex As Integer, ByVal Customer_Index As String, ByVal SelData As String)
        Dim objClassDB As New Import_ASN
        Dim objDT As DataTable = New DataTable
        Dim strSku_Index As String = ""
        Try

            objClassDB.getSKU_Detail_Update(Sku_Id, Customer_Index)
            objDT = objClassDB.GetDataTable
            If objDT.Rows.Count = 0 Then
                If Sku_Id = "" Then Exit Sub
                W_MSG_Information("ไม่พบรหัส SKU " & Sku_Id & " ในฐานข้อมูล กรุณาตรวจสอบรหัส SKU ใหม่อีกครั้ง")

                'grdPreviewData.Rows(RowIndex).Cells("ColDescription").Value = ""
                'grdPreviewData.Rows(RowIndex).Cells("ColUnitPrice").Value = ""
                'grdPreviewData.Rows(RowIndex).Cells("colProductType").Value = ""

                '' *** Using Value of strSku_Index for Search Package
                'strSku_Index = grdPreviewData.Rows(RowIndex).Cells("col_Sku_Index").Value
                'grdPreviewData.Rows(RowIndex).Cells("col_Sku_Id").Value = ""
                Exit Sub
            End If

            If objDT.Rows.Count > 0 Then
                'grdPreviewData.Rows(RowIndex).Cells("col_Sku_Index").Value = objDT.Rows(0).Item("Sku_Index").ToString
                'grdPreviewData.Rows(RowIndex).Cells("col_Sku_Id").Value = objDT.Rows(0).Item("Sku_ID").ToString
                'grdPreviewData.Rows(RowIndex).Cells("ColDescription").Value = objDT.Rows(0).Item("Str1").ToString
                'grdPreviewData.Rows(RowIndex).Cells("cbo_Package_Index").Value = objDT.Rows(0).Item("Package").ToString
                'grdPreviewData.Rows(RowIndex).Cells("colProductType").Value = objDT.Rows(0).Item("ProductType").ToString

                'strSku_Index = grdPreviewData.Rows(RowIndex).Cells("col_Sku_Index").Value


                ''krit update
                'If objDT.Rows(0).Item("Sku_Index").ToString = 0 Then
                '    Me.grdPreviewData.Rows(RowIndex).Cells("ColUnitPrice").Value = 1
                'Else
                '    Me.grdPreviewData.Rows(RowIndex).Cells("ColUnitPrice").Value = objDT.Rows(0).Item("Price1").ToString
                'End If

                Dim _dblQty As Double = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdPreviewData.Rows(RowIndex).Cells("Qty").Value, GetType(Double))

                Select Case SelData
                    Case "W"
                        Me.grdPreviewData.Rows(RowIndex).Cells("Weight_PerPackage").Value = CDbl(ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(objDT.Rows(0).Item("UnitWeight"), GetType(Double))) '.ToString("#,##0.0000")
                        If _dblQty > 0 Then
                            Me.grdPreviewData.Rows(RowIndex).Cells("Weight").Value = CDbl(_dblQty * Me.grdPreviewData.Rows(RowIndex).Cells("Weight_PerPackage").Value) '.ToString("#,##0.0000")
                        Else
                            Me.grdPreviewData.Rows(RowIndex).Cells("Weight").Value = CDbl(0).ToString '("#,##0.0000")
                        End If

                        'Case "NW"
                        '    Me.grdPreviewData.Rows(RowIndex).Cells("Weight_PerPackage").Value = CDbl(ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(objDT.Rows(0).Item("UnitWeight"), GetType(Double)))'.ToString("#,##0.0000")
                        '    If _dblQty > 0 Then
                        '        Me.grdPreviewData.Rows(RowIndex).Cells("Flo1").Value = CDbl(_dblQty * Me.grdPreviewData.Rows(RowIndex).Cells("col_Weight_Per_Unit").Value).ToString '("#,##0.0000")
                        '    Else
                        '        Me.grdPreviewData.Rows(RowIndex).Cells("Flo1").Value = CDbl(0).ToString '("#,##0.0000")
                        '    End If

                    Case "M3"
                        Me.grdPreviewData.Rows(RowIndex).Cells("M3_PerPackage").Value = CDbl(ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(objDT.Rows(0).Item("Unit_Volume"), GetType(Double))) '.ToString("#,##0.0000")
                        If _dblQty > 0 Then
                            Me.grdPreviewData.Rows(RowIndex).Cells("M3").Value = CDbl(_dblQty * Me.grdPreviewData.Rows(RowIndex).Cells("M3_PerPackage").Value) '.ToString("#,##0.0000")
                        Else
                            Me.grdPreviewData.Rows(RowIndex).Cells("M3").Value = CDbl(0).ToString '("#,##0.0000")
                        End If

                        'Case Else
                        'Me.grdPreviewData.Rows(RowIndex).Cells("col_Price_Per_Unit").Value = CDbl(ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(objDT.Rows(0).Item("Price1"), GetType(Double))).ToString("#,##0.00")
                        'Me.grdPreviewData.Rows(RowIndex).Cells("ColUnitPrice").Value = CDbl(0).ToString("#,##0.00")

                End Select

            End If

            'If Not strSku_Index = "" Then
            '    ' Search SKU Package 

            '    If Me.getSKU_Package(strSku_Index, RowIndex) = False Then
            '        '  dgvASNItem.Rows(RowIndex).Cells("cbo_Package_ItemQty").Value = ""
            '        W_MSG_Information_ByIndex(500008)
            '        Exit Sub
            '    End If

            '    If Me.getSKU_PackageCal(strSku_Index, RowIndex) = False Then
            '        '  dgvASNItem.Rows(RowIndex).Cells("cbo_Package_ItemQty").Value = ""
            '        W_MSG_Information_ByIndex(500008)
            '        Exit Sub
            '    End If


            'End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try

            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value **** มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If

            If Not tmpCustomer_Index = "" Then
                Me.txtCustomer_Id.Tag = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me.txtCustomer_Id.Tag = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If
            ' *********************
            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            'objms_Customer.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objms_Customer.SearchData_Click("", " and Customer_Index='" & Me.txtCustomer_Id.Tag & "'")
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me.txtCustomer_Id.Tag = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString

            Else
                Me.txtCustomer_Id.Tag = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdPreviewData.Rows.Count = 0 Then Exit Sub
            Me.grdPreviewData.Rows.RemoveAt(Me.grdPreviewData.CurrentRow.Index)
            Me.SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteFailAll.Click
        Try
            If Me.grdPreviewData.Rows.Count = 0 Then Exit Sub
            Dim drArrFail() As DataRow = CType(Me.grdPreviewData.DataSource, DataTable).Select("Check_Data <> 'OK'")
            If drArrFail.Length = 0 Then
                W_MSG_Information("ไม่พบรายการผิดพลาด")
                Exit Sub
            Else
                For Each drDelete As DataRow In drArrFail
                    CType(Me.grdPreviewData.DataSource, DataTable).Rows.Remove(drDelete)
                Next
            End If

            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            W_MSG_Information("ลบรายการเรียบร้อยแล้ว")
            Me.btnImport.Enabled = True
            Me.SetnumRows()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    'Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
    '    Try

    '        'checking
    '        If Me.txtFilePath.Text.Trim = "" Or Me.txtWorkSheet.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
    '            Exit Sub
    '        End If

    '        If Me.grdPreviewData.RowCount = 0 Then
    '            W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
    '            Exit Sub
    '        End If

    '        Me.Cursor = Cursors.WaitCursor
    '        Dim oImport_Order As New Import_Order_StockBalance
    '        Dim dtDataSource As New DataTable
    '        CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
    '        dtDataSource = CType(Me.grdPreviewData.DataSource, DataTable)
    '        oImport_Order.StartImportData(dtDataSource)

    '        W_MSG_Information("นำข้อมูลเข้าเรียบร้อยแล้ว")

    '        Me.grdPreviewData.DataSource = Nothing
    '        Me.btnImport.Enabled = False
    '        Me.SetnumRows()
    '    Catch ex As Exception
    '        W_MSG_Information(ex.Message.ToString)
    '    Finally
    '        Me.Cursor = Cursors.Default
    '    End Try
    'End Sub
    Private Sub ManageButtom(ByVal _boolEnable As Boolean)
        Me.grbDefaultValue.Enabled = _boolEnable
        Me.grdPreviewData.ReadOnly = (Not _boolEnable)
        Me.grbFile.Enabled = _boolEnable
        Me.btnCheckData.Enabled = _boolEnable
        Me.btnDeleteFailAll.Enabled = _boolEnable
        Me.btnDelete.Enabled = _boolEnable
        'Me.btnImport.Enabled = _boolEnable

    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try

            'checking
            If Me.txtFilePath.Text.Trim = "" Or Me.txtWorkSheet.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
                Exit Sub
            End If

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If

            'If Me.btnCheckData.Enabled = True Then
            '    W_MSG_Information("กรุณา กดปุ่ม" & Me.btnCheckData.Text)
            '    Exit Sub
            'End If
            Me.Cursor = Cursors.WaitCursor
            Me.ManageButtom(False)

            CType(Me.grdPreviewData.DataSource, DataTable).AcceptChanges()
            Me._dtDataSource = CType(Me.grdPreviewData.DataSource, DataTable)

            Me.btnImport.Enabled = False

            Me.ProgressBar1.Style = ProgressBarStyle.Blocks
            Me.ProgressBar1.Value = 0
            Me.pnlProgressBar.Visible = True
            lblProgress.Text = Me.lblProgress.Tag & CStr(0) & "%"
            Me.BwgImport.RunWorkerAsync()

        Catch ex As Exception
            W_MSG_Information(ex.Message.ToString)
        Finally

        End Try
    End Sub


#Region "   PregressBar Imports  "
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BwgImport.DoWork
        Dim bgw As BackgroundWorker = TryCast(sender, BackgroundWorker)
        Dim countAll As Integer = 0
        Dim strDeleteOrder_Index As String = ""
        Dim tmpDeleteH As New List(Of String)
        Try
            Dim objHeader As New tb_Order
            Dim objItem As New tb_OrderItem
            Dim objItemCollection As New List(Of tb_OrderItem)
            Dim objPalletType As New WMS_STD_Master_Datalayer.tb_PalletType_History
            Dim objPalletTypeCollection As New List(Of WMS_STD_Master_Datalayer.tb_PalletType_History)


            Dim ItemLife_Total_Day As Integer = 0

            Dim strSQL As String = ""

            '-------------- Begin : Group Data For Primary Key
            Dim dtPrimaryKey As New DataTable
            dtPrimaryKey.Columns.Add("Order_Date", GetType(Date))
            dtPrimaryKey.Columns.Add("Ref_No3", GetType(String))
            'dtPrimaryKey.PrimaryKey = New DataColumn() {dtPrimaryKey.Columns("Order_Date")}

            For Each odrOrder As DataRow In Me._dtDataSource.Rows
                Dim drArrRow() As DataRow = dtPrimaryKey.Select("Order_Date='" & odrOrder("Order_Date") & "' and Ref_No3='" & odrOrder("Ref_No3").ToString & "'")
                If drArrRow.Length = 0 Then
                    Dim drNewRow As DataRow
                    drNewRow = dtPrimaryKey.NewRow
                    drNewRow("Order_Date") = odrOrder("Order_Date")
                    drNewRow("Ref_No3") = odrOrder("Ref_No3")
                    dtPrimaryKey.Rows.Add(drNewRow)
                End If
            Next
            '-------------- End : Group Data For Primary Key
            Dim objDBIndex As New Sy_AutoNumber
            Dim objSys_MaxID As New Sy_Temp_AutoNumber
            Dim oSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim dtTemp As New DataTable
            Dim intSeq As Integer = 0



            'Loop Primary Key
            For Each drPrimarykey As DataRow In dtPrimaryKey.Rows
                'Select Data with primary key
                Me._tmpRef_No3 = drPrimarykey("Ref_No3").ToString
                Dim drArrOrder() As DataRow = Me._dtDataSource.Select("Order_Date='" & CDate(drPrimarykey("Order_Date")).ToString("dd/MM/yyyy") & "' and isnull(Ref_No3,'') ='" & drPrimarykey("Ref_No3").ToString & "'")
                'If drArrOrder.Length = 0 Then Continue For
                Dim iRow As Integer = 0

                '*************** New Order *************** 
                objItemCollection = New List(Of tb_OrderItem)
                objHeader = New tb_Order
                objPalletType = New WMS_STD_Master_Datalayer.tb_PalletType_History
                objPalletTypeCollection = New List(Of WMS_STD_Master_Datalayer.tb_PalletType_History)

                For Each odrOrderItem As DataRow In drArrOrder
                    'Conut Progress
                    countAll += 1
                    'Header
                    intSeq += 1 'count item

                    If iRow = 0 Then
                        intSeq = 1
                        iRow += 1
                        ' *********** Define Value for Header ***********
                        objHeader.Order_No = ""
                        objHeader.Order_Index = objSys_MaxID.getSys_Value("Order_Index")
                        strDeleteOrder_Index = objHeader.Order_Index  'Temp For delete
                        objHeader.Order_Date = CDate(odrOrderItem("Order_Date").ToString).ToString("yyyy/MM/dd")
                        objHeader.DocumentType_Index = odrOrderItem("DocumentType_Index").ToString
                        objHeader.Customer_Index = odrOrderItem("Customer_Index").ToString
                        objHeader.Supplier_Index = odrOrderItem("Supplier_Index").ToString '"" 'odrOrderItem("Customer_Index").ToString

                        objHeader.Ref_No1 = odrOrderItem("Ref_No2").ToString '""
                        objHeader.Ref_No2 = odrOrderItem("Ref_No2").ToString
                        objHeader.Ref_No3 = odrOrderItem("Ref_No3").ToString
                        objHeader.Ref_No4 = ""
                        objHeader.Ref_No5 = ""
                        objHeader.Str1 = ""
                        objHeader.Str2 = ""
                        objHeader.Str3 = ""
                        objHeader.Str4 = ""
                        objHeader.Str5 = ""

                        objHeader.Str2 = "0010000000000" ' Me.txtStr2.SelectedValue.ToString
                        objHeader.Str9 = "0010000000000" 'Me.cboTypeTruck.SelectedValue.ToString
                        objHeader.HandlingType_Index = "3"

                        objHeader.Comment = "Import data"
                        'objHeader.Epson_ProductGroup_Index = odrOrderItem("Epson_ProductGroup_Index").ToString
                        objHeader.Status = 1
                    End If


                    'Detail
                    ' *** New Object *********
                    objItem = New tb_OrderItem
                    objItem.Seq = intSeq * 10 'Timco
                    objItem.OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
                    objItem.Order_Index = objHeader.Order_Index
                    objItem.Sku_Index = odrOrderItem("Sku_Index").ToString
                    objItem.Package_Index = odrOrderItem("Package_Index").ToString
                    objItem.HandlingType_Index = "3" 'Null
                    objItem.Invoice_No = odrOrderItem("Ref_No2").ToString 'Invoice_No
                    'Qty
                    objItem.Qty = odrOrderItem("Qty").ToString
                    objItem.Ratio = 1
                    objItem.Total_Qty = objItem.Qty * objItem.Ratio

                    ' ***************************
                    'Get Default From SKU
                    'Ding Comment
                    'oSku.getSKU_Detail_Update(odrOrderItem("Sku_Id").ToString, odrOrderItem("Customer_Index").ToString)
                    'dtTemp = oSku.DataTable
                    dtTemp = IsExitDataSku_ByCustomer(odrOrderItem("Sku_Id"), odrOrderItem("Customer_Index"))
                    'W

                    objItem.Flo2 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Width"), GetType(Double))
                    'L
                    objItem.Flo3 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Length"), GetType(Double))
                    'H
                    objItem.Flo4 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Height"), GetType(Double))
                    'Weight_Per_Pck
                    objItem.Weight_Per_Pck = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Width"), GetType(Double))
                    'Volume_Per_Pck
                    objItem.Volume_Per_Pck = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Volume"), GetType(Double))

                    'Qty_Per_Pck
                    objItem.Qty_Per_Pck = 1 'objItem.Qty
                    'Item_Qty
                    objItem.Item_Qty = objItem.Qty  '0
                    'OderItem_Price
                    objItem.OrderItem_Price = FormatNumber(odrOrderItem("Flo1"), 4) 'FormatNumber((objItem.Qty * objItem.Price_Per_Pck), 2)
                    'Price_Per_Pck
                    objItem.Price_Per_Pck = FormatNumber(odrOrderItem("Flo1") / objItem.Qty, 4)  'ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Price1"), GetType(Double))

                    'PACKAGE ITEM
                    objItem.Item_Package_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Item_Package_Index"), GetType(String))

                    'Weight
                    objItem.Weight = FormatNumber(odrOrderItem("Weight"), 4)
                    'col_Net_Weight
                    objItem.Flo1 = FormatNumber(odrOrderItem("Flo1"), 4) 'objItem.Flo1 = FormatNumber(odrOrderItem("Weight"), 4)
                    'Volume
                    objItem.Volume = FormatNumber(odrOrderItem("M3"), 4) 'FormatNumber((objItem.Qty * objItem.Volume_Per_Pck), 4) ' ไม่มีมาตั้งแต่แรก
                    'PLot
                    objItem.Plot = odrOrderItem("Plot").ToString
                    'Item Status Fix (Good)
                    objItem.ItemStatus_Index = odrOrderItem("ItemStatus_Index").ToString

                    objItem.Mfg_Date = CDate(odrOrderItem("Order_Date").ToString).ToString("yyyy/MM/dd")
                    objItem.IsMfg_Date = False
                    If odrOrderItem("Exp_Date").ToString <> "" Then
                        objItem.Exp_date = CDate(odrOrderItem("Exp_Date").ToString).ToString("yyyy/MM/dd")
                        objItem.IsExp_Date = True
                    Else
                        objItem.Exp_date = Now
                        objItem.IsExp_Date = False
                    End If

                    objItem.Str4 = odrOrderItem("Str4").ToString 'Location
                    objItem.Comment = "Import Data"
                    ' *** Add value ***
                    objItemCollection.Add(objItem)

                    If ProgressBar1.Value < 100 Then
                        'เอาแค่ 70 % อีก 30 ไปใช้ในการจัดเก็บ
                        Dim dblPercen As Double = (((countAll / _dtDataSource.Rows.Count)) * 100)
                        If dblPercen <= 95 Then
                            If ShowProgress(dblPercen, bgw, e) = False Then
                            End If
                        End If
                    End If

                Next

                'Save data per Order

                'Not Controls Pallet
                'Insert Order and OrderItem
                'Creat Order
                objPalletTypeCollection.Add(objPalletType)
                Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
                objHeader.Order_Index = objDB.SaveData()

                strDeleteOrder_Index = objHeader.Order_Index
                tmpDeleteH.Add(strDeleteOrder_Index)

                'objDB = Nothing
                'TAG
                Me.AutoGenTag(objHeader.Order_Index)
                'PutAway
                _Massege = Me.PutAwayWith_Suggest_Location(objHeader.Order_Index)
                If _Massege <> "PASS" Then
                    'W_MSG_Information(strMassege)
                    'TODO :: Delete Data Oreder,OrerItem,Tag,OderItemLoation
                    For Each DeleteOrder_Index As String In tmpDeleteH
                        Dim oblst As New bl_ImportOrder_Stock
                        oblst.Delete_OrderTransactionImport(DeleteOrder_Index)
                    Next
                    objDB = Nothing
                    'ProgressBar1.Value = 0
                    e.Cancel = True
                    Exit Sub
                End If
                objDB = Nothing
            Next

            If ProgressBar1.Value < 100 Then
                If ShowProgress(100, bgw, e) = False Then
                End If
            End If

        Catch ex As Exception
            _Massege = ex.Message & ": Row " & countAll.ToString & " :: Ref_No3 = " & _tmpRef_No3
            'Delete When Error
            Dim objDBdelete As New OrderTransaction(OrderTransaction.enuOperation_Type.DELETE)
            For Each DeleteOrder_Index As String In tmpDeleteH
                Dim oblst As New bl_ImportOrder_Stock
                oblst.Delete_OrderTransactionImport(DeleteOrder_Index)
            Next
            objDBdelete = Nothing
            'W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Function ShowProgress(ByVal Pvalue As Integer, ByVal wk As BackgroundWorker, ByVal e As DoWorkEventArgs) As Boolean
        Try

            If Not wk.CancellationPending Then
                If Pvalue >= 100 Then
                    Pvalue = 100
                End If

                wk.ReportProgress(Pvalue) ' <<<<<<<<<<<<<<   Error จากตรงนี้ครับ ก็เด้งไปเข้า exception ของ Sub AppendData
                Return True
            Else
                wk.ReportProgress(0)
                Me.Cursor = Cursors.Default
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub BackgroundWorker1_ProgressChanged(ByVal sender As System.Object, ByVal e As System.ComponentModel.ProgressChangedEventArgs) Handles BwgImport.ProgressChanged
        Try
            ProgressBar1.Value = e.ProgressPercentage
            lblProgress.Text = Me.lblProgress.Tag & CStr(e.ProgressPercentage) & "%"

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BwgImport.RunWorkerCompleted
        Try

            If e.Cancelled = True Then
                Me.pnlProgressBar.Visible = False
                Me.ManageButtom(True)
                Me.btnImport.Enabled = True
                W_MSG_Error(Me._Massege)
            Else
                If e.Error Is Nothing Then
                    'ไม่มี Error แต่ Error จากเงื่อนไขของเรา
                    If Me._Massege = "PASS" Then
                        Me.ManageButtom(True)
                        Me.ProgressBar1.Value = 0
                        Me.pnlProgressBar.Visible = False
                        Me.grdPreviewData.DataSource = Nothing
                        Me.SetnumRows()
                        W_MSG_Information("นำข้อมูลเข้าเรียบร้อยแล้ว")
                    Else
                        Me.pnlProgressBar.Visible = False
                        Me.ManageButtom(True)
                        Me.btnImport.Enabled = True
                        W_MSG_Error(Me._Massege)
                    End If
                Else
                    MsgBox(String.Format("Work fail with error: {0}", e.Error.Message))
                    Me.pnlProgressBar.Visible = False
                    Me.ManageButtom(True)
                    Me.btnImport.Enabled = True
                End If
            End If


        Catch ex As Exception
            W_MSG_Error(Me._Massege)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub

#End Region


#Region "   Datalayer   "

    'Public Function StartImportData(ByVal pdtDataSource As DataTable) As Boolean
    '    Dim count As Integer = 0
    '    Try
    '        Dim objHeader As New tb_Order
    '        Dim objItem As New tb_OrderItem
    '        Dim objItemCollection As New List(Of tb_OrderItem)
    '        Dim objPalletType As New WMS_SM_Basic_Data.tb_PalletType_History
    '        Dim objPalletTypeCollection As New List(Of WMS_SM_Basic_Data.tb_PalletType_History)


    '        Dim ItemLife_Total_Day As Integer = 0

    '        Dim strSQL As String = ""

    '        -------------- Begin : Group Data For Primary Key
    '        Dim dtPrimaryKey As New DataTable
    '        dtPrimaryKey.Columns.Add("Order_Date", GetType(Date))
    '        dtPrimaryKey.Columns.Add("Ref_No3", GetType(String))
    '        dtPrimaryKey.PrimaryKey = New DataColumn() {dtPrimaryKey.Columns("Order_Date")}

    '        For Each odrOrder As DataRow In pdtDataSource.Rows
    '            Dim drArrRow() As DataRow = dtPrimaryKey.Select("Order_Date='" & odrOrder("Order_Date") & "' and Ref_No3='" & odrOrder("Ref_No3").ToString & "'")
    '            If drArrRow.Length = 0 Then
    '                Dim drNewRow As DataRow
    '                drNewRow = dtPrimaryKey.NewRow
    '                drNewRow("Order_Date") = odrOrder("Order_Date")
    '                drNewRow("Ref_No3") = odrOrder("Ref_No3")
    '                dtPrimaryKey.Rows.Add(drNewRow)
    '            End If
    '        Next
    '        -------------- End : Group Data For Primary Key
    '        Dim objDBIndex As New Sy_AutoNumber
    '        Dim objSys_MaxID As New Sy_Temp_AutoNumber
    '        Dim oSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
    '        Dim dtTemp As New DataTable

    '        Loop Primary Key
    '    For Each drPrimarykey As DataRow In dtPrimaryKey.Rows
    '            Select Data with primary key
    '            Dim drArrOrder() As DataRow = pdtDataSource.Select("Order_Date='" & CDate(drPrimarykey("Order_Date")).ToString("dd/MM/yyyy") & "' and isnull(Ref_No3,'') ='" & drPrimarykey("Ref_No3").ToString & "'")
    '            If drArrOrder.Length = 0 Then Continue For
    '            Dim iRow As Integer = 0
    '            count = 0
    '            *************** New Order *************** 
    '            objItemCollection = New List(Of tb_OrderItem)
    '            objHeader = New tb_Order
    '            objPalletType = New WMS_SM_Basic_Data.tb_PalletType_History
    '            objPalletTypeCollection = New List(Of WMS_SM_Basic_Data.tb_PalletType_History)

    '            For Each odrOrderItem As DataRow In drArrOrder
    '                Header
    '                count += 1 'count item

    '                If iRow = 0 Then
    '                    iRow += 1
    '                     *********** Define Value for Header ***********
    '                    objHeader.Order_No = ""
    '                    objHeader.Order_Index = objSys_MaxID.getSys_Value("Order_Index")
    '                    objHeader.Order_Date = CDate(odrOrderItem("Order_Date").ToString).ToString("yyyy/MM/dd")
    '                    objHeader.DocumentType_Index = odrOrderItem("DocumentType_Index").ToString
    '                    objHeader.Customer_Index = odrOrderItem("Customer_Index").ToString

    '                    objHeader.Ref_No1 = ""
    '                    objHeader.Ref_No2 = odrOrderItem("Ref_No2").ToString
    '                    objHeader.Ref_No3 = odrOrderItem("Ref_No3").ToString
    '                    objHeader.Ref_No4 = ""
    '                    objHeader.Ref_No5 = ""
    '                    objHeader.Str1 = ""
    '                    objHeader.Str2 = ""
    '                    objHeader.Str3 = ""
    '                    objHeader.Str4 = ""
    '                    objHeader.Str5 = ""

    '                    objHeader.Str2 = "0010000000000" ' Me.txtStr2.SelectedValue.ToString
    '                    objHeader.Str9 = "0010000000000" 'Me.cboTypeTruck.SelectedValue.ToString
    '                    objHeader.HandlingType_Index = "3"

    '                    objHeader.Comment = "Import data"
    '                    objHeader.Epson_ProductGroup_Index = odrOrderItem("Epson_ProductGroup_Index").ToString

    '                End If


    '                Detail
    '                 *** New Object *********
    '                objItem = New tb_OrderItem
    '                objItem.Seq = count * 10 'Timco
    '                objItem.OrderItem_Index = objDBIndex.getSys_Value("OrderItem_Index")
    '                objItem.Order_Index = objHeader.Order_Index
    '                objItem.Sku_Index = odrOrderItem("Sku_Index").ToString
    '                objItem.Package_Index = odrOrderItem("Package_Index").ToString
    '                objItem.HandlingType_Index = "3" 'Null
    '                objItem.Invoice_No = odrOrderItem("Ref_No2").ToString 'Invoice_No
    '                Qty
    '                objItem.Qty = odrOrderItem("Qty").ToString
    '                objItem.Ratio = 1
    '                objItem.Total_Qty = objItem.Qty * objItem.Ratio

    '                 ***************************
    '                Get Default From SKU
    '                oSku.getSKU_Detail_Equal_Update(odrOrderItem("Sku_Id").ToString, odrOrderItem("Customer_Index").ToString)
    '                dtTemp = oSku.DataTable
    '                W
    '                objItem.Flo2 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Width"), GetType(Double))
    '                L
    '                objItem.Flo3 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Length"), GetType(Double))
    '                H
    '                objItem.Flo4 = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Height"), GetType(Double))
    '                Weight_Per_Pck
    '                objItem.Weight_Per_Pck = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("UnitWeight"), GetType(Double))
    '                Volume_Per_Pck
    '                objItem.Volume_Per_Pck = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Unit_Volume"), GetType(Double))
    '                Price_Per_Pck
    '                objItem.Price_Per_Pck = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Price1"), GetType(Double))

    '                Qty_Per_Pck
    '                objItem.Qty_Per_Pck = 1 'objItem.Qty
    '                Item_Qty
    '                objItem.Item_Qty = 0 'objItem.Qty
    '                OderItem_Price
    '                objItem.OrderItem_Price = FormatNumber((objItem.Qty * objItem.Price_Per_Pck), 2)
    '                PACKAGE ITEM
    '                objItem.Item_Package_Index = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(dtTemp.Rows(0)("Item_Package_Index"), GetType(String))

    '                Weight
    '                objItem.Weight = FormatNumber(odrOrderItem("Weight"), 4)
    '                col_Net_Weight
    '                objItem.Flo1 = FormatNumber(odrOrderItem("Weight"), 4)
    '                Volume
    '                objItem.Volume = FormatNumber(objItem.Volume_Per_Pck, 4) ' ไม่มีมาตั้งแต่แรก
    '                PLot
    '                objItem.Plot = odrOrderItem("Plot").ToString
    '                Item Status Fix (Good)
    '                objItem.ItemStatus_Index = odrOrderItem("ItemStatus_Index").ToString

    '                objItem.Mfg_Date = CDate(odrOrderItem("Order_Date").ToString).ToString("yyyy/MM/dd")
    '                objItem.IsMfg_Date = False
    '                If odrOrderItem("Exp_Date").ToString <> "" Then
    '                    objItem.Exp_date = CDate(odrOrderItem("Exp_Date").ToString).ToString("yyyy/MM/dd")
    '                    objItem.IsExp_Date = True
    '                Else
    '                    objItem.Exp_date = Now
    '                    objItem.IsExp_Date = False
    '                End If

    '                objItem.Str4 = odrOrderItem("Str4").ToString 'Location
    '                objItem.Comment = "Import Data"
    '                 *** Add value ***
    '                objItemCollection.Add(objItem)
    '            Next

    '            Save data per Order

    '            Not Controls Pallet
    '            Insert Order and OrderItem
    '            Creat Order
    '            objPalletTypeCollection.Add(objPalletType)
    '            Dim objDB As New OrderTransaction_update(OrderTransaction_update.enuOperation_Type.ADDNEW, objHeader, objItemCollection, objPalletTypeCollection)
    '            objHeader.Order_Index = objDB.SaveData()
    '            objDB = Nothing
    '            TAG
    '            Me.AutoGenTag(objHeader.Order_Index)
    '            PutAway
    '            Me.PutAwayWith_Suggest_Location(objHeader.Order_Index)
    '        Next


    '        Return True
    '    Catch ex As Exception
    '        W_MSG_Error(count)
    '        Throw ex
    '    End Try

    'End Function

    Private Sub AutoGenTag(ByVal pstrOrder_Index As String)
        Dim objItemCollection As New List(Of tb_TAG)

        Dim objDBTempIndex As New Sy_AutoNumber
        Dim objAutoNumber As New Sy_AutoNumber

        Try
            Dim odtOrderItemNonTag As DataTable = GetOrderItemNonTag(pstrOrder_Index)
            If odtOrderItemNonTag.Rows.Count = 0 Then

                Exit Sub
            End If

            For Each odrOrderItem As DataRow In odtOrderItemNonTag.Rows
                'Normal Gen
                Dim otb_TAG As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                otb_TAG = SetTagItem(odrOrderItem, otb_TAG)
                otb_TAG.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                objItemCollection.Add(otb_TAG)

            Next
            Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

            objItemA.objItemCollection = objItemCollection
            objItemA.InsertData()


        Catch ex As Exception
            Throw ex
        Finally
            objDBTempIndex = Nothing
            objAutoNumber = Nothing
        End Try

    End Sub

    Private Function GetOrderItemNonTag(ByVal pstrOrder_Index As String) As DataTable

        Dim objItem As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
        Try
            objItem.getOrderItemNonTag(pstrOrder_Index)
            Dim odtItem As DataTable
            odtItem = objItem.GetDataTable
            Return odtItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SetTagItem(ByVal podrOrderItem As DataRow, ByVal poTagItem As tb_TAG) As tb_TAG
        Try

            With podrOrderItem
                'poTagItem.Suggest_Location_Index = getLocation_Index(.Item("Str4").ToString) 'Location_Alias From OrderItem in Str4
                poTagItem.Order_No = .Item("Order_No").ToString
                poTagItem.Order_Index = .Item("Order_Index").ToString
                poTagItem.Order_Date = .Item("Order_Date").ToString
                poTagItem.Order_Time = .Item("Order_Time").ToString
                poTagItem.Customer_Index = .Item("Customer_Index").ToString

                If .Item("Supplier_Index").ToString IsNot Nothing Then
                    poTagItem.Supplier_Index = .Item("Supplier_Index").ToString
                Else
                    poTagItem.Supplier_Index = ""
                End If

                If .Item("OrderItem_Index").ToString IsNot Nothing Then
                    poTagItem.OrderItem_Index = .Item("OrderItem_Index").ToString
                Else
                    poTagItem.OrderItem_Index = ""
                End If

                poTagItem.OrderItemLocation_Index = ""
                If .Item("Sku_Index").ToString IsNot Nothing Then
                    poTagItem.Sku_Index = .Item("Sku_Index").ToString
                Else
                    poTagItem.Sku_Index = ""
                End If

                If .Item("PLot").ToString IsNot Nothing Then
                    poTagItem.PLot = .Item("PLot").ToString
                Else
                    poTagItem.PLot = ""
                End If

                If .Item("ItemStatus_Index").ToString IsNot Nothing Then
                    poTagItem.ItemStatus_Index = .Item("ItemStatus_Index").ToString
                Else
                    poTagItem.ItemStatus_Index = ""
                End If

                If .Item("Package_Index").ToString IsNot Nothing Then
                    poTagItem.Package_Index = .Item("Package_Index").ToString
                Else
                    poTagItem.Package_Index = ""
                End If

                poTagItem.Unit_Weight = 0
                poTagItem.Size_Index = -1


                If .Item("PalletType_Index").ToString IsNot Nothing Then
                    poTagItem.Pallet_No = .Item("PalletType_Index").ToString
                Else
                    poTagItem.Pallet_No = ""
                End If


                poTagItem.TAG_Status = 0

                If .Item("str1").ToString IsNot Nothing Then
                    poTagItem.Ref_No1 = .Item("str1").ToString
                Else
                    poTagItem.Ref_No1 = ""
                End If

                If .Item("str2").ToString IsNot Nothing Then
                    poTagItem.Ref_No2 = .Item("str2").ToString
                Else
                    poTagItem.Ref_No2 = ""
                End If


                If .Item("Qty").ToString IsNot Nothing Then
                    poTagItem.Qty = .Item("Qty").ToString
                Else
                    poTagItem.Qty = 0
                End If
                If .Item("Weight").ToString IsNot Nothing Then
                    poTagItem.Weight = .Item("Weight").ToString
                Else
                    poTagItem.Weight = 0
                End If
                If .Item("Volume").ToString IsNot Nothing Then
                    poTagItem.Volume = .Item("Volume").ToString
                Else
                    poTagItem.Volume = 0
                End If

                'dong_Edit cal weigth,Volume
                Dim objOrderItem As New tb_OrderItem
                Dim dt As New DataTable
                objOrderItem.searchByOrderItem_Index(poTagItem.OrderItem_Index)
                dt = objOrderItem.GetDataTable()
                poTagItem.Qty_per_TAG = poTagItem.Qty
                If dt.Rows.Count > 0 Then
                    poTagItem.Weight_per_TAG = dt.Rows(0).Item("Weight_Bal") 'poTagItem.Weight
                    poTagItem.Volume_per_TAG = dt.Rows(0).Item("Volume_Bal") ' poTagItem.Volume
                Else
                    poTagItem.Weight_per_TAG = poTagItem.Weight
                    poTagItem.Volume_per_TAG = poTagItem.Volume
                End If
                poTagItem.Ref_No3 = "1/1"
            End With

            Return poTagItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function PutAwayWith_Suggest_Location(ByVal pstrOrder_Index As String) As String
        Dim dtTemDataSource As DataTable = New DataTable
        Dim objOrderItemLocation As New tb_OrderItemLocation
        Dim objCollection As New List(Of tb_OrderItemLocation)
        Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
        Dim intjRow As Integer = 0

        Try
            Dim dbmMaxPalletFix As Double = 99

            Dim oTag As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
            oTag.getPutawayWithTAG(pstrOrder_Index)
            dtTemDataSource = oTag.DataTable

            If dtTemDataSource.Rows.Count = 0 Then
                If Me.BwgImport.CancellationPending = False Then
                    Me.BwgImport.CancelAsync()
                    Return "System Error"
                End If
            End If

            'Checking Location

            '*** Begin : Generate Pallet Seq
            'Dim oml_Timgo As New ml_Timco(ml_Timco.enuOperation_Type.SEARCH)
            Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            Dim strTo_Location_Index As String = ""
            'Dim odtTempPallet_Location As New DataTable


            Dim strTempPallet_No As String = ""
            Dim dblMax_Pallet As Double = 0

            Dim drArrCheckData() As DataRow
            'Dim oGentPallet As New ml_Timco(ml_Timco.enuOperation_Type.SEARCH)
            'Dim oObject() As Object

            For Each drSaveOIL As DataRow In dtTemDataSource.Rows
                intjRow += 1
                If intjRow = 26 Then
                    intjRow = 26
                End If
                dtTemDataSource.AcceptChanges()
                drArrCheckData = dtTemDataSource.Select("Suggest_Location_Alias='" & drSaveOIL("Suggest_Location_Alias").ToString & "'")
                If drArrCheckData.Length > dbmMaxPalletFix Then 'For Timco Only MaxPallet = 99
                    Return ("ตำแหน่ง : " & drSaveOIL("Suggest_Location_Alias").ToString & " รายการเกิน Max Pallet")
                    'Exit Function
                End If

                'oObject = oGentPallet.GernaratePallet_Seq(odtTempPallet_Location, drSaveOIL("Suggest_Location_Index").ToString, Nothing, Nothing)
                'If oObject(0).ToString <> "PASS" Then
                '    Return oObject(0).ToString
                '    'Exit Function
                'Else
                '    drSaveOIL("Pallet_No") = oObject(1)
                '    odtTempPallet_Location = oObject(2)
                'End If

            Next

            dtTemDataSource.AcceptChanges()
            drArrCheckData = dtTemDataSource.Select("isnull(Suggest_Location_Alias,'') = '' or isnull(Pallet_No,'')=''")
            If drArrCheckData.Length > 0 Then
                Return ("ตำแหน่ง : " & drArrCheckData(0)("Suggest_Location_Alias").ToString & Chr(13) & "Pallet No เกิน กรุณาเลือก Location ใหม่")
                'Exit Function
            End If

            '*** END : Generate Pallet Seq

            '*********************************** begin : Save PutAway and Generate Pallet  ***********************************
            'Save Transaction
            'Before New Gen
            'Update เพราะว่าถ้ามีการ save หลายครั้งอาจทำ
            Dim oblst As New bl_ImportOrder_Stock
            oblst.UpdateSys_Value_MaxIndexTable("tb_OrderItemLocation", "OrderItemLocation_Index") 'objOrderItemLocation.OrderItemLocation_Index = objDBIndex.getSys_Value("OrderItemLocation_Index")
            oblst = Nothing

            Dim objCollection_CollOrderItemLocation As New List(Of List(Of tb_OrderItemLocation))
            Dim objCollection_JobOrder As New List(Of tb_JobOrder)

            'Dim oml_Timco As New ml_Timco(ml_Timco.enuOperation_Type.UPDATE)
            For Each drSaveOIL As DataRow In dtTemDataSource.Rows
                If drSaveOIL("Suggest_Location_Alias").ToString <> "" And drSaveOIL("Pallet_No").ToString <> "" Then
                    Dim objReturn() As Object
                    objReturn = Me.SetLocation_fromitemAll(drSaveOIL, pstrOrder_Index) 'Dong_kk Modify Date. 2012/02/29 Move in Transaction
                    If objReturn Is Nothing Then
                        Return "System Error"
                    Else
                        objCollection_CollOrderItemLocation.Add(objReturn(0))
                        objCollection_JobOrder.Add(objReturn(1))
                    End If
                    'Update For Timco 'Dong_kk Modify Date. 2012/02/29 Move in Transaction
                    'oml_Timco.Update_PalletNo(drSaveOIL("Tag_No"), drSaveOIL("OrderItem_Index"), drSaveOIL("Pallet_No").ToString.ToUpper, False)
                Else
                    Return ("Location . " & drSaveOIL("Suggest_Location_Alias").ToString & Chr(13) & "Pallet No เกิน กรุณาเลือก Location ใหม่")
                End If
            Next

            '*********************************** begin : PutAway  ***********************************

            'PutAway
            Dim objJobOrder As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
            objJobOrder.SaveData_PutAway(pstrOrder_Index, objCollection_CollOrderItemLocation, objCollection_JobOrder)

            '*********************************** End : Save PutAway and Generate Pallet   ***********************************

            'dong_kk()
            'update_date :: 03/08/2011
            'TODO: For StorageShage TIMCO
            '*********************************** begin : คิดเงิน  ***********************************
            'Dim oSaveBilling As New svar_TransactionLocationBilling(svar_TransactionLocationBilling.enuOperation_Type.RECIEVE, Me._Order_Index, Me._Customer_Index)
            'oSaveBilling.Document_Index = Me._Order_Index
            'Dim strSave As String = oSaveBilling.SaveData()
            'Select Case strSave
            '    Case "OVER"
            '        If W_MSG_Confirm_ByIndex("100049") = Windows.Forms.DialogResult.Yes Then
            '            Dim frmStci As New frmStorageChargeItem(frmStorageChargeItem.enuOperation_Type.RECEIVE)
            '            Dim dtOverFow As New DataTable
            '            dtOverFow = oSaveBilling.DataTable
            '            frmStci.dataTable = dtOverFow
            '            frmStci.ShowDialog()
            '        End If
            '    Case "PASS", ""
            '        Exit Select
            '    Case Else
            '        W_MSG_Information(strSave)
            'End Select
            '*********************************** End : คิดเงิน  ***********************************

            Return "PASS"

        Catch ex As Exception
            Return ex.Message.ToString & ": Row " & intjRow & " :: Ref_No3 = " & _tmpRef_No3
        Finally

        End Try

    End Function

    Function SetLocation_fromitemAll(ByVal drSaveOIL As DataRow, ByVal pstrOrder_Index As String) As Object()
        Try
            Dim objOrderItemLocation As New tb_OrderItemLocation
            Dim objOrderItem As New tb_OrderItem
            Dim objCollection As New List(Of tb_OrderItemLocation)
            Dim clsJobOrder As New tb_JobOrder

            clsJobOrder = setObjJobOrder(drSaveOIL)
            '  objOrderItem = getProductSelection(Me.OrderItem_Id) '(Me.grdOrderItem.Rows(RowIndex).Cells("OrderItem_Index").Value)
            objCollection.Clear()

            Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            Dim sumQty As Double = 0
            Dim sumTotal_Qty As Double = 0
            Dim Recieve_Qty As Double = CDbl(drSaveOIL("Qty_per_TAG").ToString)
            Dim Recieve_Total_Qty As Double = CDbl(drSaveOIL("Total_Qty").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Total_Qty").Value)

            objOrderItemLocation = New tb_OrderItemLocation

            With objOrderItemLocation
                .Order_Index = pstrOrder_Index
                .OrderItem_Index = drSaveOIL("OrderItem_Index").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Col_OrderItem_Index").Value.ToString
                .Tag_No = drSaveOIL("Tag_No").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Tag_No").Value.ToString
                .Package_Index = drSaveOIL("Package_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_Package_Index").Value.ToString
                .PLot = drSaveOIL("Plot").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("clPlot").Value.ToString
                .ItemStatus_Index = drSaveOIL("ItemStatus_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_ItemStatus_Index").Value.ToString
                .Serial_No = ""
                .Location_Index = drSaveOIL("Suggest_Location_Index").ToString 'objClassDB.getLocation_Index(drSaveOIL("Suggest_Location_Alias").ToString)
                .PalletType_Index = ""
                .Pallet_Qty = 0
                .Sku_Index = drSaveOIL("Sku_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("Col_Sku_Index").Value.ToString
                .Qty = Recieve_Qty
                .Total_Qty = Recieve_Total_Qty
                .Ratio = CDbl(drSaveOIL("Ratio").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Ratio").Value)
                .Weight = CDbl(drSaveOIL("Weight_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_weight").Value.ToString)
                .Volume = CDbl(drSaveOIL("Volume_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_Volume").Value.ToString)
                .MixPallet = 0

                .Add_by = drSaveOIL("Pallet_No").ToString

                If IsNumeric(drSaveOIL("Qty_Per_Pck").ToString) Then .Qty_Item = CDbl(drSaveOIL("Qty_Per_Pck").ToString) * .Qty
                If IsNumeric(drSaveOIL("Price_Per_Pck").ToString) Then .OrderItem_Price = CDbl(drSaveOIL("Price_Per_Pck").ToString) * .Qty


                ' *** Sum Balance ***
                sumQty = sumQty + .Qty
                sumTotal_Qty = sumTotal_Qty + .Total_Qty

            End With

            objCollection.Add(objOrderItemLocation)


            Dim objReturn(1) As Object
            objReturn(0) = objCollection
            objReturn(1) = clsJobOrder

            Return objReturn
            ' *** Check Balance ***
            'If Not Recieve_Qty = sumQty Then
            '    MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            '    Exit Sub
            'End If



            ' *** Save Data ***
            'Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
            'objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
            'objJobLocation = Nothing

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function setObjJobOrder(ByVal drSaveOIL As DataRow) As Object

        Dim objJobOrder As New tb_JobOrder
        objJobOrder.JobOrder_Index = drSaveOIL("Order_Index").ToString
        objJobOrder.JobOrder_Date = Now
        objJobOrder.JobOrder_No = drSaveOIL("Order_No").ToString

        ' *************************************
        ' Current System Use tb_Order with tb_JobOrder by 1:1  
        '  Value of in  tb_JobOrder.JobOrder_Index field  >> tb_JobOrder.JobOrder_Index =tb_Order.Order_Index 
        objJobOrder.Order_Index = drSaveOIL("Order_Index").ToString
        ' *************************************

        objJobOrder.Str1 = ""
        objJobOrder.Str2 = ""
        objJobOrder.Str3 = ""
        objJobOrder.Str4 = ""
        objJobOrder.Str5 = ""

        Return objJobOrder
    End Function

    Public Function getLocation_Index(ByVal Location_Alias As String) As String

        Dim strSQL As String
        Try
            Dim oConnServer As New SqlClient.SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlClient.SqlDataAdapter
            strSQL = " select Location_Index from ms_Location where Location_Alias = @Location_Alias  "

            Dim selectCMD As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, oConnServer)
            odaServer.SelectCommand = selectCMD
            ' Add parameters and set values.
            selectCMD.Parameters.Add("@Location_Alias", SqlDbType.NVarChar, 50).Value = Location_Alias
            odaServer.Fill(odtServer)

            If odtServer.Rows.Count = 0 Then
                Return ""
            Else
                Return odtServer.Rows(0).Item("Location_Index").ToString
            End If

        Catch ex As Exception
            Throw ex

        End Try
    End Function

    Public Function IsExitDataSku_ByCustomer(ByVal pstrSku_Id As String, ByVal pstrCustomer_Index As String) As DataTable
        Try
            Dim oConnServer As New SqlClient.SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlClient.SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT * "
            strSQL &= " FROM ms_Sku "
            strSQL &= " WHERE Sku_Id = @Sku_Id"
            strSQL &= " And Customer_Index = @Customer_Index"
            strSQL &= " And Status_Id <> -1"


            Dim selectCMD As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, oConnServer)
            odaServer.SelectCommand = selectCMD
            ' Add parameters and set values.
            selectCMD.Parameters.Add("@Customer_Index", SqlDbType.NVarChar, 15).Value = pstrCustomer_Index
            selectCMD.Parameters.Add("@Sku_Id", SqlDbType.NVarChar, 500).Value = pstrSku_Id

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetItemStatus(ByVal pstrItemStatus As String) As DataTable
        Try
            Dim oConnServer As New SqlClient.SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlClient.SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT * "
            strSQL &= " FROM ms_ItemStatus "
            strSQL &= " WHERE Description = @ItemStatus"
            strSQL &= " And Status_Id <> -1"

            Dim selectCMD As SqlClient.SqlCommand = New SqlClient.SqlCommand(strSQL, oConnServer)
            odaServer.SelectCommand = selectCMD
            ' Add parameters and set values.
            selectCMD.Parameters.Add("@ItemStatus", SqlDbType.NVarChar, 15).Value = pstrItemStatus

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetIndexByID(ByVal pstrTableName As String, ByVal pstrField_Index As String, ByVal pstrField_ID As String, ByVal pstrField_ID_Value As String) As String
        Try
            Dim oConnServer As New SqlClient.SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlClient.SqlDataAdapter
            Dim strSQL As String

            strSQL = "SELECT " & pstrField_Index & " FROM " & pstrTableName & " WHERE " & pstrField_ID & " = '" & pstrField_ID_Value.Trim & "' and Status_Id <> -1 "

            odaServer.SelectCommand = New SqlClient.SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            If odtServer.Rows.Count > 0 Then
                Return odtServer.Rows(0)(pstrField_Index).ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region

    Private Sub frmImport_Order_StockBalance_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Me.BwgImport.CancellationPending = False Then
                Me.BwgImport.CancelAsync()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSeachSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachSupplier.Click

        'เปลี่ยน Supplier_Popup เป็น frmCustomer_Receive_Location_PopUp_Update
        'TODO: HARDCODE-MSG
        Try
            If txtCustomer_Id.Text = "" Then
                W_MSG_Information_ByIndex(8)
                Exit Sub
            End If
            Dim frm As New frmCustomer_Receive_Location_PopUp 'frmCus_Ship_Location_Popup
            frm.Customer_Index = txtCustomer_Id.Tag ' Me._CustomerIndex
            frm.ShowDialog()

            Dim tmpCustomer_Shipping_Location_Index As String = ""
            tmpCustomer_Shipping_Location_Index = frm.Customer_Receive_Location_Index 'frm.Customer_Shipping_Location_Index

            If tmpCustomer_Shipping_Location_Index = "" Then
                txtSupplier_Name.Text = ""
                Me.txtSupplier_Id.Tag = ""
                Me.txtSupplier_Id.Text = ""
                Exit Sub
            End If

            If Not tmpCustomer_Shipping_Location_Index = "" Then
                Me.txtSupplier_Id.Tag = tmpCustomer_Shipping_Location_Index
                Me.getCus_Shipping_Location_Index()
            Else
                Me.txtSupplier_Id.Tag = ""
                Me.txtSupplier_Id.Text = ""
                Me.txtSupplier_Id.Text = ""
            End If
            'big
            txtSupplier_Name.Text = frm.Receive_Location_Name
            txtSupplier_Id.Text = frm.Customer_Receive_Location_Id
            frm.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub getCus_Shipping_Location_Index()
        'Dim objms_Shipping_Location As New ms_Customer_Shipping_Location(ms_Customer_Shipping_Location.enuOperation_Type.SEARCH)
        Dim objms_Shipping_Location As New ms_Customer_Receive_Location(ms_Customer_Receive_Location.enuOperation_Type.SEARCH)

        Dim objDTms_Shipping_Location As DataTable = New DataTable

        Try
            objms_Shipping_Location.getPopup_Search(" WHERE Customer_Receive_Location_Index='" & Me.txtSupplier_Id.Tag.ToString & "'")
            'objms_Shipping_Location.getCus_Ship_Locartion_SearchPopUp(" WHERE Customer_Shipping_Location_Index='" & Me.txtSupplier_Id.Tag.ToString & "'")
            objDTms_Shipping_Location = objms_Shipping_Location.GetDataTable
            If objDTms_Shipping_Location.Rows.Count > 0 Then
                Me.txtSupplier_Id.Tag = objDTms_Shipping_Location.Rows(0).Item("Customer_Receive_Location_Index").ToString
                Me.txtSupplier_Id.Text = objDTms_Shipping_Location.Rows(0).Item("Customer_Receive_Location_ID").ToString
                Me.txtSupplier_Name.Text = objDTms_Shipping_Location.Rows(0).Item("Receive_Location_Name").ToString

            Else
                Me.txtSupplier_Name.Tag = ""
                Me.txtSupplier_Id.Text = ""
                Me.txtSupplier_Name.Text = ""
                'Me.txtShip_Address.Text = ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Shipping_Location = Nothing
            objDTms_Shipping_Location = Nothing
        End Try
    End Sub

End Class