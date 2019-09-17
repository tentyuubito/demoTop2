Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Public Class frmJobPackingDetailBarcode
#Region " Private variables "

    Private appSet As New Configuration.AppSettingsReader()

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _SalesOrderPacking_Index As String
    Private _SalesOrder_Index As String
    Private _SalesOrder_No As String
    'Private _BarcodePacking As String
    Private _PackSize_Index As String
    Private _Status_Print As String
    Private _Count_Print As Integer
    Private _SalesOrder_Index1 As String
    Private _TransportManifestItem_Index As String
    Private _DateAdd As Date

    Private _DTSOP As New DataTable
    Private _DtBox As New DataTable

    Private QtyPacked As Integer
    Private TotalQty As Integer

    Private _DeleteComplete As Boolean

#End Region
#Region " Properties "
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property
    Public Property SalesOrderPacking_Index() As String
        Get
            Return _SalesOrderPacking_Index
        End Get
        Set(ByVal Value As String)
            _SalesOrderPacking_Index = Value
        End Set
    End Property
    Public Property SalesOrder_No() As String
        Get
            Return _SalesOrder_No
        End Get
        Set(ByVal Value As String)
            _SalesOrder_No = Value
        End Set
    End Property
    Public Property SalesOrder_Index() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal Value As String)
            _SalesOrder_Index = Value
        End Set
    End Property

    'Public Property BarcodePacking() As String
    '    Get
    '        Return _BarcodePacking
    '    End Get
    '    Set(ByVal Value As String)
    '        _BarcodePacking = Value
    '    End Set
    'End Property

    Public Property PackSize_Index() As String
        Get
            Return _PackSize_Index
        End Get
        Set(ByVal Value As String)
            _PackSize_Index = Value
        End Set
    End Property

    Public Property Status_Print() As String
        Get
            Return _Status_Print
        End Get
        Set(ByVal Value As String)
            _Status_Print = Value
        End Set
    End Property

    Public Property Count_Print() As Integer
        Get
            Return _Count_Print
        End Get
        Set(ByVal Value As Integer)
            _Count_Print = Value
        End Set
    End Property

    Public Property SalesOrder_Index1() As String
        Get
            Return _SalesOrder_Index1
        End Get
        Set(ByVal Value As String)
            _SalesOrder_Index1 = Value
        End Set
    End Property

    Public Property TransportManifestItem_Index() As String
        Get
            Return _TransportManifestItem_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifestItem_Index = Value
        End Set
    End Property

    Public Property DateAdd() As Date
        Get
            Return _DateAdd
        End Get
        Set(ByVal Value As Date)
            _DateAdd = Value
        End Set
    End Property


#End Region

    Private Sub frmJobPackingDetailBarcode_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me._DeleteComplete = False
            Me.txtDocNo.Text = Me._SalesOrder_No
            Me.getPackSize()
            Me.getData()
            getCarrier()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub getCarrier()
        Dim objPackSize As New tb_SalesOrderPacking
        Dim objDT As DataTable = New DataTable


        Try
            objDT = objPackSize.getCarrier()
            With cmbCarrier
                .DisplayMember = "Description"
                .ValueMember = "Carrier_Index"
                .DataSource = objDT
            End With
            If objDT.Rows.Count > 0 Then
                cmbCarrier.SelectedIndex = 0
            End If


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objPackSize = Nothing
        End Try
    End Sub

    Private Sub getPackSize()
        Dim objPackSize As New tb_SalesOrderPacking
        Dim objDT As DataTable = New DataTable


        Try
            objDT = objPackSize.GetPackSize("")
            With cbSizeBox
                .DisplayMember = "Description"
                .ValueMember = "Size_Index"
                .DataSource = objDT
            End With
            cbSizeBox.SelectedIndex = 0

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objPackSize = Nothing
        End Try
    End Sub

    Private Sub getData()
        Dim objtb_SalesOrderPacking As New PackingTransaction(PackingTransaction.enuOperation_Type.SEARCH)
        'Dim objtb_SalesOrderPacking As New tb_SalesOrderPackingItem
        Dim objDT As DataTable = New DataTable
        'Dim objDTtb_PurchaseOrder As DataTable = New DataTable
        Dim strWhere As String = ""

        Try


            If Me.txtDocNo.Text.Trim <> "" Then
                strWhere = " AND (SalesOrder_No = '" & Me.txtDocNo.Text & "') "
            End If


            objtb_SalesOrderPacking.GetAllDataSOP(strWhere)
            objDT = objtb_SalesOrderPacking.DataTable
            _DTSOP = objDT

            '            Me.dgvData.Rows.Clear()
            '           Me.dgvData.Refresh()
            Me.dgvData.AutoGenerateColumns = False
            Me.dgvData.DataSource = objDT


            Me.SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_SalesOrderPacking = Nothing
            'objDTtb_PurchaseOrder = Nothing
        End Try
    End Sub

    Private Sub getDataDetial()
        Dim objtb_SalesOrderPacking As New tb_SalesOrderPackingItem
        Dim objDT As DataTable = New DataTable
        'Dim objDTtb_PurchaseOrder As DataTable = New DataTable
        Dim strWhere As String = ""

        Try

            If Me.dgvData.CurrentRow.Cells("col_SalesOrderPacking_Index").Value IsNot Nothing Then
                strWhere = " AND (SalesOrderPacking_Index = '" & Me.dgvData.CurrentRow.Cells("col_SalesOrderPacking_Index").Value & "') "
            End If


            objtb_SalesOrderPacking.GetAllDataSOP_Item(strWhere)
            objDT = objtb_SalesOrderPacking.DataTable
            Me.dgvDataBarcode.AutoGenerateColumns = False
            Me.dgvDataBarcode.DataSource = objDT
            Me._SalesOrderPacking_Index = ""
            If dgvDataBarcode.RowCount > 0 Then
                Me.btnRemove.Enabled = True
                Me.btnPrint.Enabled = True
                Me.btnPrintDetail.Enabled = True
                Me._SalesOrderPacking_Index = objDT.Rows(0).Item("SalesOrderPacking_Index").ToString

            Else
                Me.btnRemove.Enabled = False
                Me.btnPrint.Enabled = False
                Me.btnPrintDetail.Enabled = False
            End If





            Me.SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_SalesOrderPacking = Nothing
            'objDTtb_PurchaseOrder = Nothing
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        If _DeleteComplete Then
            Me.DialogResult = Windows.Forms.DialogResult.OK
        Else
            Me.DialogResult = Windows.Forms.DialogResult.Cancel
        End If
    End Sub
    Sub SetnumRows()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = dgvData.Rows.Count
        If numRows > 0 Then
            lblResult.Text = "จำนวน " & numRows & " รายการ "
        Else
            lblResult.Text = "ไม่พบรายการ"
        End If
    End Sub

    Sub SetnumRowsDetial()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = dgvDataBarcode.Rows.Count
        If numRows > 0 Then
            lblResultDetial.Text = "จำนวน " & numRows & " รายการ "
        Else
            lblResultDetial.Text = "ไม่พบรายการ"
        End If
    End Sub

    'Private Sub dgvData_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellContentDoubleClick
    '    'btnDetail_Click(sender, e)
    'End Sub





    Private Sub btnDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetail.Click
        Try
            If dgvData.RowCount > 0 Then
                If Me.dgvData.CurrentRow.Cells("col_BarcodePack").Value IsNot Nothing Then
                    txtBarcodeBox.Text = Me.dgvData.CurrentRow.Cells("col_BarcodePack").Value

                End If
                If Me.dgvData.CurrentRow.Cells("col_packSize_index").Value IsNot Nothing Then
                    cbSizeBox.SelectedValue = Me.dgvData.CurrentRow.Cells("col_packSize_index").Value
                End If
                Me.getDataDetial()

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If dgvData.RowCount > 0 Then

                If txtBarcodeBox.Text <> "" Then
                    If MessageBox.Show("คุณต้องการพิมพ์บาร์โค้ด   " & txtBarcodeBox.Text & "แพ็คหรือไม่", "พิมพ์บาร์โค้ดแพ็ค", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes = True Then
                        'PrintBarcodePack(txtBarcodeBox.Text)
                        'UpdateStatusPrint()
                        ''ไม่ถามก่อนพิมพ์
                        Dim omlPacking As New ml_Packing
                        omlPacking._isPrint = Me.chkPreview.Checked
                        omlPacking.PrintBarcodePack(Me._SalesOrderPacking_Index) 'Print Barcode TAG PACK
                        omlPacking.UpdateStatusPrint(Me._SalesOrderPacking_Index)
                    End If

                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'Private Sub PrintBarcodePack(ByVal BarcodePacking As String)
    '    Try
    '        Dim frmReportPreview As New frmReportViewerMain
    '        Dim cry As New ReportDocument
    '        'MsgBox(Application.StartupPath & appSet.GetValue("PathReport_Box", GetType(String)))

    '        cry.Load(Application.StartupPath & appSet.GetValue("PathReport_Box", GetType(String)))

    '        Dim ds As New dsBarcodeBox
    '        Dim dr As DataRow
    '        ' Dim tmpByte() As Byte = Nothing
    '        '     Dim tmpPackage_Name As String = DirectCast(Me.cboPackage.SelectedItem, DataRowView).Item("Package")



    '        'View_TSS_SalesOrderPackingBarcodePrint
    '        Dim objtb_SOP As New tb_SalesOrderPacking
    '        Dim dtObj As New DataTable
    '        Dim strWhere As String
    '        strWhere = " And  BarcodePacking='" & BarcodePacking & "'"
    '        dtObj = objtb_SOP.GetBarcodePackingPrint(strWhere)

    '        If dtObj.Rows.Count > 0 Then
    '            With dtObj.Rows(0)
    '                dr = ds.Tables(0).NewRow
    '                dr("InvoiceNo") = .Item("SalesOrder_No").ToString
    '                dr("ShiptoName") = .Item("Company_Name").ToString
    '                dr("ShiptoAddress") = .Item("Address").ToString
    '                dr("Packsize") = .Item("Description").ToString
    '                dr("BarcodeName") = .Item("BarcodePacking").ToString
    '                dr("Barcode") = GenCodeToByte(.Item("BarcodePacking").ToString)
    '                ds.Tables(0).Rows.Add(dr)
    '            End With

    '        End If






    '        'Set Printer
    '        Dim strPirnterName As String = appSet.GetValue("Printer_Name_Box", GetType(String))
    '        If strPirnterName <> "" Then
    '            'Set Printer
    '            cry.PrintOptions.PrinterName = strPirnterName
    '            'Set DataSource
    '            cry.SetDataSource(ds)
    '            'Print

    '            cry.PrintToPrinter(1, False, 0, 0)
    '        Else

    '            'Set DataSource
    '            cry.SetDataSource(ds)
    '            'Print

    '            'cry.PrintToPrinter(1, False, 0, 0)

    '            'prview
    '            frmReportPreview.CrystalReportViewer1.ReportSource = cry
    '            frmReportPreview.Show()
    '        End If

    '        ' frmReportPreview.CrystalReportViewer1.Zoom(2)



    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Private Sub PrintBarcodePackDetail(ByVal BarcodePacking As String)
    '    Try
    '        Dim frmReportPreview As New frmReportViewerMain
    '        Dim cry As New ReportDocument
    '        'MsgBox(Application.StartupPath & appSet.GetValue("PathReport_Box", GetType(String)))

    '        cry.Load(Application.StartupPath & appSet.GetValue("PathReport_BoxDetail", GetType(String)))

    '        Dim ds As New dsBarcodeBoxDetail
    '        Dim dr As DataRow
    '        ' Dim tmpByte() As Byte = Nothing
    '        '     Dim tmpPackage_Name As String = DirectCast(Me.cboPackage.SelectedItem, DataRowView).Item("Package")



    '        'View_TSS_SalesOrderPackingBarcodePrint
    '        Dim objtb_SOP As New tb_SalesOrderPackingItem
    '        Dim dtObj As New DataTable
    '        Dim strWhere As String
    '        strWhere = " And  BarcodePacking='" & BarcodePacking & "'"
    '        dtObj = objtb_SOP.GetAllDataSOPDetail(strWhere)

    '        If dtObj.Rows.Count > 0 Then


    '            For i As Integer = 0 To dtObj.Rows.Count - 1
    '                With dtObj.Rows(i)
    '                    dr = ds.Tables(0).NewRow
    '                    dr("Invoice_No") = .Item("Invoice_No").ToString
    '                    dr("strBox") = .Item("strBox").ToString
    '                    dr("Str1") = .Item("Str1").ToString
    '                    dr("Sku_Id") = .Item("Sku_Id").ToString
    '                    dr("Qty_Pack") = .Item("Qty_Pack").ToString
    '                    dr("BarcodePacking") = .Item("BarcodePacking").ToString

    '                    ' dr("Barcode") = GenCodeToByte(.Item("BarcodePacking").ToString)
    '                    ds.Tables(0).Rows.Add(dr)
    '                End With
    '            Next



    '        End If






    '        'Set Printer
    '        Dim strPirnterName As String = appSet.GetValue("Printer_Name_Box", GetType(String))
    '        If strPirnterName <> "" Then
    '            '   cry.PrintOptions.PrinterName = strPirnterName
    '        End If

    '        'Set DataSource
    '        cry.SetDataSource(ds)
    '        'Print

    '        'cry.PrintToPrinter(1, False, 0, 0)

    '        'prview
    '        frmReportPreview.CrystalReportViewer1.ReportSource = cry
    '        frmReportPreview.Show()
    '        ' frmReportPreview.CrystalReportViewer1.Zoom(2)



    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub UpdateStatusPrint()
        ' ====== TODO: HARDCODE-MSG   
        ' ====== Todd: 26 Dec 2009 - Need to get rid of hardcode text message in Thai.
        If dgvDataBarcode.Rows.Count <= 0 Then Exit Sub

        Dim objPOTransaction As New PackingTransaction(PackingTransaction.enuOperation_Type.UPDATE)
        Dim oPo As New tb_SalesOrderPacking
        Dim dr() As DataRow

        dr = _DTSOP.Select("BarcodePacking ='" & txtBarcodeBox.Text.Trim & "'", "")
        If dr.Length > 0 Then
            'Me._BarcodePacking = dr(0).Item("BarcodePacking")
            If dr(0).Item("Count_Print") > 0 Then
                oPo.Status_Print = 1
                oPo.Count_Print = dr(0).Item("Count_Print") + 1
            Else
                oPo.Status_Print = 1
                oPo.Count_Print = 1
            End If
        Else
            oPo.Status_Print = 1
            oPo.Count_Print = 1
        End If
        oPo.BarcodePacking = Me.txtBarcodeBox.Text.Trim 'Me._BarcodePacking
        Try

            If objPOTransaction.UpdatePrint(oPo) Then
                '                MessageBox.Show("ยืนยันรายการเรียบร้อยแล้ว", "ผลการยกเลิก", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.getData()
            Else
                MessageBox.Show("ไม่อัพเดทรายการพิมพ์ได้ ระบบทำงานผิดพลาด", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            objPOTransaction = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            objPOTransaction = Nothing
        End Try
    End Sub

    Private Sub UpdateStatusPrintRemove()
        ' ====== TODO: HARDCODE-MSG   
        ' ====== Todd: 26 Dec 2009 - Need to get rid of hardcode text message in Thai.
        If dgvDataBarcode.Rows.Count <= 0 Then Exit Sub

        Dim objPOTransaction As New PackingTransaction(PackingTransaction.enuOperation_Type.UPDATE)
        Dim oPo As New tb_SalesOrderPacking
        oPo.Status_Print = -1
        oPo.BarcodePacking = Me.txtBarcodeBox.Text.Trim 'Me.BarcodePacking
        oPo.SalesOrder_Index = Me._SalesOrder_Index
        Try
            If objPOTransaction.UpdatePrint(oPo) Then
                Dim oAudit_Log As New Sy_Audit_Log
                oAudit_Log.Document_Index = _SalesOrder_Index
                oAudit_Log.Document_No = Me.txtBarcodeBox.Text.Trim
                oAudit_Log.Insert(Sy_Audit_Log.Log_Type.Cancel_Packing)
                Me.getData()
            Else
                MessageBox.Show("ไม่อัพเดทรายการพิมพ์ได้ ระบบทำงานผิดพลาด", "ผลการทำงาน", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
            objPOTransaction = Nothing

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            objPOTransaction = Nothing
        End Try
    End Sub

    Private Function GenCodeToByte(ByVal pText As String) As Byte()
        Try

            Dim ResultByte() As Byte

            Dim pic As System.Drawing.Image = Code128Rendering.MakeBarcodeImage(pText, 1, False)
            Dim BitmapConverter As System.ComponentModel.TypeConverter

            'Me.PictureBox1.Image = pic


            BitmapConverter = System.ComponentModel.TypeDescriptor.GetConverter(pic.[GetType]())
            ResultByte = DirectCast(BitmapConverter.ConvertTo(pic, GetType(Byte())), Byte())
            Return ResultByte

        Catch ex As Exception

            Return Nothing
        End Try

    End Function

    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        Try
            If txtBarcodeBox.Text <> "" Then
                If MessageBox.Show("คุณต้องการลบบาร์โค้ด   " & txtBarcodeBox.Text & "แพ็คหรือไม่", "พิมพ์บาร์โค้ดแพ็ค", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes = True Then
                    'คุณต้องการลบบาร์โค้ดแพ็คหรือไม่'
                    UpdateStatusPrintRemove()
                    Me.getData()

                    If Me.dgvData.DataSource Is Nothing OrElse Not CType(Me.dgvData.DataSource, DataTable).Rows.Count > 0 Then
                        Me.dgvDataBarcode.DataSource = Nothing

                        Me.txtBarcodeBox.Clear()
                        Me.cbSizeBox.SelectedIndex = -1

                        Me.btnRemove.Enabled = False
                        Me.btnPrint.Enabled = False
                        Me.btnPrintDetail.Enabled = False

                        Call SetnumRows()

                        _DeleteComplete = True
                    Else
                        _DeleteComplete = False
                    End If
                End If

            End If

        Catch ex As Exception

        End Try
    End Sub

    'Private Sub dgvData_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellContentClick
    '    Try
    '        btnDetail_Click(sender, e)
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub txtDocNo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtDocNo.TextChanged

    End Sub

    Private Sub txtDocNo_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtDocNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.getData()

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintDetail.Click
        Try
            If dgvData.RowCount > 0 Then

                If txtBarcodeBox.Text <> "" Then
                    If MessageBox.Show("คุณต้องการพิมพ์รายการแพ็คสินค้าบาร์โค้ด   " & txtBarcodeBox.Text & "แพ็คหรือไม่", "พิมพ์บาร์โค้ดแพ็ค", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes = True Then
                        'PrintBarcodePackDetail(txtBarcodeBox.Text)
                        Dim omlPacking As New ml_Packing
                        ' omlPacking.PrintBarcodePackDetail(Me._SalesOrderPacking_Index) 'Print Barcode TAG PACK
                        omlPacking.PrintBarcodePackDetail_SNP(Me._SalesOrderPacking_Index) 'Print Barcode TAG PACK

                    End If

                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub dgvData_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellClick
    '    Try
    '        If e.RowIndex <= -1 Then Exit Sub
    '        btnDetail_Click(sender, e)
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub dgvData_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvData.SelectionChanged
        Try
            btnDetail_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnRFID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRFID.Click
        Try
            Dim frm As New frmUpdateRFID
            frm.Document_Index = _SalesOrderPacking_Index
            frm.SalesOrder_No = Me._SalesOrder_No
            frm.Packing_No = txtBarcodeBox.Text
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class