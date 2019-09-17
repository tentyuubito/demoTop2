Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports CrystalDecisions.CrystalReports.Engine
Imports GenCode128
Imports WMS_STD_OUTB_Transport

Public Class frmJobPacking
#Region " Private variables "

    Private appSet As New Configuration.AppSettingsReader()
    Private _boolPackOne As Boolean = False

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String
    Private _SalesOrderPacking_Index As String
    Private _SalesOrder_Index As String
    Private _SalesOrder_No As String
    Private _BarcodePacking As String
    Private BarcodePrint As String
    Private _PackSize_Index As String
    Private _Status_Print As String
    Private _Count_Print As Integer
    Private _SalesOrder_Index1 As String
    Private _TransportManifestItem_Index As String
    Private _DateAdd As Date
    Private _strPacking_Condition As String

    Private _DTSOP As New DataTable

    Private _DtBox As New DataTable

    Private _DtBarCode As New DataTable

    Private QtyPacked As Integer
    Private TotalQty As Integer

    Private TotoltalQtyToSave As Integer

    Private sku_index_current_Cell As String = ""

    'Report New
    'Private PathReport_Box As String = ""
    'Private PathReport_BoxDetail As String = ""


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

    Public Property SalesOrder_Index() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal Value As String)
            _SalesOrder_Index = Value
        End Set
    End Property

    Public Property BarcodePacking() As String
        Get
            Return _BarcodePacking
        End Get
        Set(ByVal Value As String)
            _BarcodePacking = Value
        End Set
    End Property

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



    Private Sub frmJobPacking_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Me.txtInvoiceNo.Focus()
    End Sub

    Private Sub frmJobPacking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


            Me.dgvDataBarcode.AutoGenerateColumns = False
            Me.dgvData.AutoGenerateColumns = False
            Me.dgvDataHeader.AutoGenerateColumns = False

            Me.lblRownumPack.Visible = False
            Me.lblRownumPack.Text = Me.lblRownumPack.Tag
            Me.getPackSize()
            Me.GetConfigQtyBarcode()
            Me.SetnumRows()
            Me.SetnumRowsBarcode()

            For Each Col As DataGridViewColumn In Me.dgvDataBarcode.Columns
                Me._DtBarCode.Columns.Add(Col.DataPropertyName)
            Next

            Me._DtBarCode.Columns("Qty_Pack").DataType = GetType(Double)

            Me.dgvDataBarcode.DataSource = Me._DtBarCode

            'KSL
            Me.chkPack.Visible = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub GetConfigQtyBarcode()
        Try
            Dim objCustomSetting As New config_CustomSetting
            Dim bl As Boolean
            bl = objCustomSetting.getConfig_Key_USE("USE_CHECK_DEFUALT_QTY_BARCODE")

            If bl Then
                chkQtyDb.Visible = True
                txtQty.Enabled = True
            Else
                chkQtyDb.Visible = False
                txtQty.Enabled = False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub getPackSize()
        Dim objPackSize As New tb_SalesOrderPacking
        Dim objDT As DataTable = New DataTable


        Try
            objDT = objPackSize.GetPackSize(" AND Status_id <> -1 ")
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

    Private Sub txtInvoiceNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtInvoiceNo.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                btnSeach_Click(sender, e)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub FormatGrid(ByVal tempGrd As DataGridView)
        For i As Integer = 0 To CType(tempGrd.DataSource, DataTable).Rows.Count - 1
            With tempGrd
                .Rows(i).Cells("Col_SalesOrder_Date").Value = Format(CDate(CType(tempGrd.DataSource, DataTable).Rows(i).Item("SalesOrder_Date")), "dd/MM/yyyy").ToString
                Dim intStatus As Integer = CType(tempGrd.DataSource, DataTable).Rows(i).Item("Status_Pack")
                Select Case intStatus
                    Case 1 'รอเพ็ค
                        .Rows(i).Cells("Col_SalesOrder_No2").Style.BackColor = Drawing.Color.Yellow
                    Case 2 'เสร็จสิ้น
                        .Rows(i).Cells("Col_SalesOrder_No2").Style.BackColor = Drawing.Color.LightGreen
                End Select
            End With
        Next
    End Sub
    Private Sub getSalesOrderItem(ByVal strIndex As String)
        'Dim objtb_SalesOrderPacking As New tb_SalesOrderPacking
        Dim objtb_SalesOrderPacking As New PackingTransaction(PackingTransaction.enuOperation_Type.SEARCH)
        Dim objDTDetailPacking As DataTable = New DataTable
        Try
            'Add on KSL
            Me.GetCustomerType(strIndex)


            Dim strWhere As String = ""
            If strIndex <> "" Then
                strWhere = " AND (SalesOrder_Index = '" & strIndex & "') "
            End If
            objDTDetailPacking = objtb_SalesOrderPacking.GetPackingDetail(strWhere)
            If objDTDetailPacking.Rows.Count > 0 Then
                _SalesOrder_No = objDTDetailPacking.Rows(0).Item("SalesOrder_No").ToString
                _SalesOrder_Index = objDTDetailPacking.Rows(0).Item("SalesOrder_Index").ToString
                'btnPackDetail.Enabled = True
                MN_Button(1)
            End If
            Me.dgvData.AutoGenerateColumns = False
            Me.dgvData.DataSource = objDTDetailPacking

            'HACK: Set Color

            'For i As Integer = 0 To dgvData.Rows.Count - 1
            '    With Me.dgvData
            '        If .Rows(i).Cells("col_QtyTotal").Value = .Rows(i).Cells("col_QtyPacked").Value Then
            '            .Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
            '        End If
            '    End With
            'Next

            Me.SetnumRows()
            Me.showpack()

            If Me.dgvDataHeader.RowCount = 0 Then
                Me.txtInvoiceNo.Text = ""
                Me.txtInvoiceNo.Focus()
            End If

            setColorField()

        Catch ex As Exception
            Throw ex
        Finally
            objtb_SalesOrderPacking = Nothing
            objDTDetailPacking = Nothing
        End Try
    End Sub
    Private Sub getSalesOrderPacking()
        Dim objtb_SalesOrderPacking As New PackingTransaction(PackingTransaction.enuOperation_Type.SEARCH)
        Dim objDTHeader As DataTable = New DataTable
        Dim strWhere As String = ""

        Try
            If Me.txtInvoiceNo.Text <> "" Then
                strWhere &= " AND (SalesOrder_No Like '%" & Me.txtInvoiceNo.Text.Trim.Replace("'", "''") & "%') "
            End If

            'strWhere &= String.Format(" AND (Status_Pack = '{0}') ", IIf(Me.chkPack.Checked, "2", "1").ToString)

            Select Case Me.chkPack.Checked
                Case True
                    strWhere &= " AND Status_Manifest = 16"
                Case False
                    strWhere &= " AND Status_Manifest Not in (16)"
            End Select

            objDTHeader = objtb_SalesOrderPacking.GetPackingHeader(strWhere)
            _strPacking_Condition = strWhere

            Me.dgvDataHeader.AutoGenerateColumns = False
            Me.dgvDataHeader.DataSource = objDTHeader


            FormatGrid(dgvDataHeader)

            btnSeach.Focus()

            'If Me.dgvDataHeader.RowCount = 0 Then
            '    Me.txtInvoiceNo.Text = ""
            '    Me.txtInvoiceNo.Focus()
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_SalesOrderPacking = Nothing
            'objDTtb_PurchaseOrder = Nothing
        End Try
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

    Sub SetnumRowsBarcode()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = dgvDataBarcode.Rows.Count
        If numRows > 0 Then
            lblResultBarcodeItem.Text = "จำนวน " & numRows & " รายการ "
        Else
            lblResultBarcodeItem.Text = "ไม่พบรายการ"
            'lblResultBarcodeItem.Text = "0/0"
        End If
    End Sub

    Private Sub btnSeach_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeach.Click
        Try
            Me.lblCT.Text = ""
            'If Me.txtInvoiceBox.Text <> "" Then
            '    Dim obj As New tb_SalesOrderPacking
            '    Dim dtTNP As New DataTable
            '    obj.getCheckPick_Pack_SO(Me.txtInvoiceBox.Text.Trim.Replace("'", "''"))
            '    dtTNP = obj.GetDataTable

            '    'If FormatNumber(dtTNP.Rows(0)("WQTY"), 2) <> FormatNumber(dtTNP.Rows(0)("PQTY"), 2) Then
            '    '    W_MSG_Information(Me.Label1.Text & " : " & Me.txtInvoiceBox.Text.Trim & " ยังแพ็คสินค้าไม่เสร็จ " & Chr(13) & "คุณต้องการปิดหน้าจอใช่หรือไม่ ?")
            '    'End If


            'End If

            Me.getSalesOrderPacking()

            If dgvDataHeader.CurrentRow Is Nothing Then Exit Sub
            Dim strIndex As String
            strIndex = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_Index2").Value()
            Me._SalesOrder_Index = strIndex
            getSalesOrderItem(strIndex)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub GetCustomerType(ByVal SalesOrder_Index As String)
        Try
            Dim Conn As New DBType_SQLServer
            Dim dtConn As New DataTable
            Dim strConn As String = ""
            strConn &= " SELECT CT.Description as CustomerType"
            strConn &= " FROM	tb_SalesOrder SO "
            strConn &= "	inner join ms_Customer_Shipping_Location MCL ON SO.Customer_Shipping_Location_Index = MCL.Customer_Shipping_Location_Index"
            strConn &= "	inner join ms_CustomerType CT ON MCL.CustomerType_Index = CT.CustomerType_Index"
            strConn &= String.Format(" WHERE  SO.SalesOrder_Index = '{0}'", SalesOrder_Index)
            dtConn = Conn.DBExeQuery(strConn)

            Me.lblCT.Text = ""
            If dtConn.Rows.Count > 0 Then
                Me.lblCT.Text = "ประเภทลูกค้า : " & dtConn.Rows(0)("CustomerType").ToString
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnOpenBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBox.Click
        Try
            Me.lblRownumPack.Text = Me.lblRownumPack.Tag
            If Me.dgvData.Rows.Count = 0 Then Exit Sub
            Dim dtBarcode As New DataTable
            MN_Button(0)



            Me._SalesOrder_No = dgvData.CurrentRow.Cells("col_SalesOrder_No").Value
            Me._SalesOrder_Index = dgvData.CurrentRow.Cells("col_SalesOrder_Index").Value
            Me.txtInvoiceBox.Text = dgvData.CurrentRow.Cells("col_InvoiceNo").Value
            'Me.TransportManifestItem_Index = dgvData.CurrentRow.Cells("col_TransportManifestItem_Index").Value



            CType(Me.dgvData.DataSource, DataTable).AcceptChanges()
            Me._DTSOP = New DataTable
            Me._DtBox = New DataTable
            Me._DtBox = CType(Me.dgvData.DataSource, DataTable).Clone
            Me._DTSOP = CType(Me.dgvData.DataSource, DataTable).Clone

            Dim drSelect() As DataRow = CType(Me.dgvData.DataSource, DataTable).Select("SalesOrder_Index='" & Me._SalesOrder_Index & "'")
            For Each drsub As DataRow In drSelect
                _DTSOP.Rows.Add(drsub.ItemArray)
            Next

            Me._boolPackOne = False

            Me.btnOpenBox.BackColor = Color.Red
            Me.btnOpenBox2.BackColor = Color.Gray

            setColorField()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Function Packing() As Boolean
        Try
            Dim oService As New ml_MappingBarcode

            Dim oDataBarcodeSKU As DataTable
            oDataBarcodeSKU = oService.GetCheckingBarcodePackageDupp(Me.txtBarcodeBox.Text.Trim, Me._SalesOrder_Index)

            If oDataBarcodeSKU Is Nothing OrElse Not oDataBarcodeSKU.Rows.Count > 0 Then
                W_MSG_Error("ไม่พบข้อมูล Barcode นี้")
                Me.txtBarcodeBox.Clear()
                Return False
            End If

            Dim boolCheck As Boolean = True
            'Dim oDataBarcode As New DataTable
            'oDataBarcode = oDataBarcodeSKU.Clone
            Dim iRatio As Double = oDataBarcodeSKU.Rows(0)("Ratio")
            Dim iSku_Index As String = oDataBarcodeSKU.Rows(0)("Sku_Index")
            For Each dr As DataRow In oDataBarcodeSKU.Rows
                If dr("Sku_Index") <> iSku_Index Then
                    boolCheck = False
                End If
                If dr("Ratio") <> iRatio Then
                    boolCheck = False
                End If
            Next


            Dim DrSelect As DataRow
            If boolCheck = False Then
                'Barcode เดียวกันแต่อัตราส่วนไม่เท่ากัน
                Dim frm As New frmPopUp_SelectSku(oDataBarcodeSKU)
                frm.ShowDialog()
                DrSelect = frm.ReturnDatarow
                If DrSelect Is Nothing Then Return False
            Else
                DrSelect = oDataBarcodeSKU.Rows(0)


            End If

            sku_index_current_Cell = DrSelect.Item("Sku_index").ToString

            'Dim DrSelect As DataRow
            'DrSelect = oDataBarcode.Rows(0)
            'Dim drCheckPKSO() As DataRow
            ''Step 1 : Barcode ถูกต้อง Master 1:1
            'drCheckPKSO = oDataBarcode.Select("Barcode = '" & Me.txtBarcodeBox.Text.Trim & "'", "")
            'If drCheckPKSO.Length = 1 Then
            '    DrSelect = drCheckPKSO(0)
            'Else
            '    'Step 1 : Barcode ถูกต้อง Master 1:M
            '    drCheckPKSO = oDataBarcode.Select("SOI_Package_Index = Package_Index and Ratio = 1 and Barcode = '" & Me.txtBarcodeBox.Text.Trim & "'", "")
            '    If drCheckPKSO.Length = 1 Then
            '        DrSelect = drCheckPKSO(0)
            '    End If
            'End If



            ''Step 1 : มีใน SO หรือไม่
            'drCheckPKSO = Me._DTSOP.Select("Barcode = '" & Me.txtBarcodeBox.Text.Trim & "'", "")
            'If drCheckPKSO.Length > 0 Then
            '    'Step 1 : มีใน SO และมีในหน่วยสินค้า
            '    drCheckPKSO = oDataBarcode.Select("Barcode = '" & Me.txtBarcodeBox.Text.Trim & "'", "")
            '    If drCheckPKSO.Length = 1 Then
            '        DrSelect = drCheckPKSO(0)
            '    End If
            'End If

            'drCheckPKSO = oDataBarcode.Select("Barcode = '" & Me.txtBarcodeBox.Text.Trim & "'", "")
            'If drCheckPKSO.Length = 1 Then
            '    DrSelect = drCheckPKSO(0)
            'Else
            '    drCheckPKSO = oDataBarcode.Select("SOI_Package_Index = Package_Index and Ratio = 1 and Barcode = '" & Me.txtBarcodeBox.Text.Trim & "'", "")
            '    If drCheckPKSO.Length = 1 Then
            '        DrSelect = drCheckPKSO(0)
            '    End If
            'End If


            'If drCheckPKSO.Length = 0 Then
            '    If oDataBarcode.Rows.Count = 1 Then
            '        DrSelect = oDataBarcode.Rows(0)
            '    Else
            '        Dim frm As New frmPopUp_SelectSku(oDataBarcode)
            '        frm.ShowDialog()
            '        DrSelect = frm.ReturnDatarow
            '        If DrSelect Is Nothing Then Return False
            '    End If
            'End If


            With DrSelect
                If Not Me._DTSOP.Select(String.Format("Sku_Index = '{0}'", .Item("Sku_Index").ToString)).Length > 0 Then
                    W_MSG_Error("ไม่พบข้อมูล Barcode นี้ตรงกับในรายการเอกสาร")
                    Me.txtBarcodeBox.Clear()
                    Return False
                End If

                Dim oQty As Object

                Dim MaxSkuQty As Double
                Dim PackedSkuQty As Double
                Dim GridBarcodeSkuQty As Double


                'If oDataBarcode.Rows(0).Item("IsScanItemAll") Then Me.txtQty.Text = 1

                Dim BarCodeQty As Double = Me.txtQty.Text.Trim * .Item("Ratio")

                oQty = Me._DTSOP.Compute("SUM(Total_Qty)", String.Format("Sku_Index = '{0}'", .Item("Sku_Index").ToString))
                If oQty IsNot Nothing AndAlso Not TypeOf oQty Is DBNull Then
                    MaxSkuQty = oQty
                End If

                oQty = Me._DTSOP.Compute("SUM(Qty_Pack)", String.Format("Sku_Index = '{0}'", .Item("Sku_Index").ToString))
                If oQty IsNot Nothing AndAlso Not TypeOf oQty Is DBNull Then
                    PackedSkuQty = oQty
                End If

                oQty = Me._DtBarCode.Compute("SUM(Qty_Pack)", String.Format("Sku_Index = '{0}'", .Item("Sku_Index").ToString))
                If oQty IsNot Nothing AndAlso Not TypeOf oQty Is DBNull Then
                    GridBarcodeSkuQty = oQty
                End If

                If GridBarcodeSkuQty <> MaxSkuQty - PackedSkuQty Then
                    If BarCodeQty + GridBarcodeSkuQty > MaxSkuQty - PackedSkuQty Then
                        W_MSG_Information("แพ็คสินค้าเกิน กรุณาตรวจสอบจำนวน แพ็คได้อีก " & MaxSkuQty - PackedSkuQty - GridBarcodeSkuQty & Space(1) & .Item("Package_Des"))
                        Me.txtBarcodeBox.Clear()
                        Me.txtBarcodeBox.Focus()
                        Return False
                    End If
                Else
                    W_MSG_Information("แพ็คสินค้าครบจำนวนแล้ว")
                    Me.txtBarcodeBox.Clear()
                    Me.txtBarcodeBox.Focus()
                    Return False
                End If

                Dim oBarcodeRow As DataRow() = Me._DtBarCode.Select(String.Format("Sku_Index = '{0}'", .Item("Sku_Index").ToString))

                If Me._boolPackOne Then
                    Dim intCount As Integer = 1
                    If Me._boolPackOne Then

                        intCount = IIf(IsNumeric(Me.txtQty.Text), Me.txtQty.Text, 1)
                        BarCodeQty = BarCodeQty / intCount
                    End If

                    For i As Integer = 0 To intCount - 1
                        Dim oNewRow As DataRow = Me._DtBarCode.NewRow
                        oNewRow.Item("ibarcode") = Me.txtBarcodeBox.Text.Trim
                        oNewRow.Item("Sku_Index") = .Item("Sku_Index").ToString
                        oNewRow.Item("Qty_Pack") = BarCodeQty
                        oNewRow.Item("Total_Qty") = BarCodeQty
                        oNewRow.Item("Package_Des") = .Item("Package")
                        'oNewRow.Item("Package_Des") = .Item("Package_Des")
                        oNewRow.Item("Sku_Id") = .Item("Sku_Id").ToString
                        oNewRow.Item("Str1") = .Item("Sku_Name").ToString
                        'oNewRow.Item("Ratio") = .Item("Ratio")
                        Me._DtBarCode.Rows.Add(oNewRow)
                    Next
                Else
                    If oBarcodeRow.Length > 0 Then
                        With Me._DtBarCode.Rows.Item(Me._DtBarCode.Rows.IndexOf(oBarcodeRow(0)))
                            .Item("Qty_Pack") += BarCodeQty
                            .Item("Total_Qty") += BarCodeQty
                        End With
                    Else
                        Dim intCount As Integer = 1
                        'If oDataBarcode.Rows(0).Item("IsNoPack") Then
                        If Me._boolPackOne Then
                            intCount = IIf(IsNumeric(Me.txtQty.Text), Me.txtQty.Text, 1)
                            BarCodeQty = BarCodeQty / intCount
                        End If


                        For i As Integer = 0 To intCount - 1
                            Dim oNewRow As DataRow = Me._DtBarCode.NewRow
                            oNewRow.Item("ibarcode") = Me.txtBarcodeBox.Text.Trim
                            oNewRow.Item("Sku_Index") = .Item("Sku_Index").ToString
                            oNewRow.Item("Qty_Pack") = BarCodeQty
                            oNewRow.Item("Total_Qty") = BarCodeQty
                            oNewRow.Item("Package_Des") = .Item("Package")
                            'oNewRow.Item("Package_Des") = .Item("Package_Des")
                            oNewRow.Item("Sku_Id") = .Item("Sku_Id").ToString
                            oNewRow.Item("Str1") = .Item("Sku_Name").ToString
                            'oNewRow.Item("Ratio") = .Item("Ratio")
                            Me._DtBarCode.Rows.Add(oNewRow)
                        Next


                    End If
                End If



                Me.txtBarcodeBox.Clear()
            End With
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    'status 
    ' 0 =  เปิดกล่อง
    ' 1 =  ปิดกล่อง
    Private Sub MN_Button(ByVal status As Integer)
        Try
            If status = 0 Then


                Me.txtBarcodeBox.Enabled = True
                Me.txtQty.Enabled = True

                Me.btnOpenBox.Enabled = False
                Me.btnOpenBox2.Enabled = False
                Me.btnCloseBox.Enabled = True
                Me.txtBarcodeBox.Focus()
            Else


                Me.txtBarcodeBox.Enabled = False
                Me.txtQty.Enabled = False

                Me.btnOpenBox.Enabled = True
                Me.btnOpenBox2.Enabled = True
                Me.btnCloseBox.Enabled = False

                Me._DtBarCode.Rows.Clear()
            End If
            SetnumRowsBarcode()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnCloseBox_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseBox.Click
        Dim strResult As String = ""
        Dim strResultL As New List(Of String)
        Dim SalesOrder_No As String = ""
        SalesOrder_No = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_No2").Value()
        Try

            If Not ValidateDataBeforeSave() Then
                Exit Sub
            Else
                If Not dgvDataBarcode.RowCount > 0 Then
                    W_MSG_Information("ตรวจสอบข้อมูลก่อนทำการบันทึก")
                    Me.txtBarcodeBox.Clear()
                    MN_Button(1)
                    Exit Sub
                End If

                If _boolPackOne Then
                    For Each Row As DataGridViewRow In Me.dgvDataBarcode.Rows
                        strResultL.Add(DoSaveDocument(Row))
                    Next
                    If strResultL.Count > 0 Then


                        MN_Button(1)

                        'ไม่ถามก่อนพิมพ์
                        Dim omlPacking As New ml_Packing
                        omlPacking._isPrint = Me.chkPreview.Checked
                        Dim strArr As String = ""
                        Dim intC As Integer = 0
                        For Each str As String In strResultL
                            'omlPacking.PrintBarcodePack(str)
                            If intC = 0 Then
                                strArr = "'" & str & "'"
                            Else
                                strArr &= ",'" & str & "'"
                            End If
                            intC += 1
                        Next

                        omlPacking.PrintBarcodePack(strArr, True)

                        For Each SalesOrderPacking_Index As String In strResultL

                            'omlPacking.PrintBarcodePack(SalesOrderPacking_Index) 'Print Barcode TAG PACK
                            Me.txtSeq.Text = omlPacking.Seq
                            omlPacking.UpdateStatusPrint(SalesOrderPacking_Index)

                        Next




                        If Me._boolPackOne = False Then
                            'omlPacking.PrintBarcodePackDetail(Me._SalesOrderPacking_Index) 'Print Barcode Detail TAG PACK
                        End If


                        Me.getSalesOrderPacking()

                        If dgvDataHeader.CurrentRow IsNot Nothing Then
                            Dim strIndex As String
                            strIndex = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_Index2").Value()
                            getSalesOrderItem(strIndex)

                            Me.txtQty.Text = "1"
                        Else
                            Me.dgvData.DataSource = Nothing

                            btnClear_Click(Nothing, Nothing)

                            SetnumRows()
                        End If

                    End If

                Else

                    strResult = DoSaveDocument()
                    If strResult <> "" Then

                        ' Me.getSalesOrderPacking()
                        MN_Button(1)
                        'Dim BarcodePack As String = ""

                        ''ถามก่อนพิมพ์
                        ''สติ๊กเกอร์แพ็คสินค้า
                        'If MessageBox.Show("คุณต้องการพิมพ์บาร์โค้ดแพ็คหรือไม่", "พิมพ์บาร์โค้ดแพ็ค", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes = True Then
                        '    PrintBarcodePack(Me._SalesOrderPacking_Index)
                        '    UpdateStatusPrint()
                        'End If
                        ''รายการ pack สินค้า
                        'If BarcodePack <> "" Then
                        '    If MessageBox.Show("คุณต้องการพิมพ์รายการแพ็คสินค้าบาร์โค้ด   " & BarcodePack & "แพ็คหรือไม่", "พิมพ์บาร์โค้ดแพ็ค", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes = True Then
                        '        PrintBarcodePackDetail(BarcodePack)
                        '    End If
                        'End If

                        'ไม่ถามก่อนพิมพ์
                        Dim omlPacking As New ml_Packing
                        omlPacking._isPrint = Me.chkPreview.Checked
                        omlPacking.PrintBarcodePack(Me._SalesOrderPacking_Index) 'Print Barcode TAG PACK
                        Me.txtSeq.Text = omlPacking.Seq
                        omlPacking.UpdateStatusPrint(Me._SalesOrderPacking_Index)


                        If Me._boolPackOne = False Then
                            'omlPacking.PrintBarcodePackDetail(Me._SalesOrderPacking_Index) 'Print Barcode Detail TAG PACK
                        End If
                        Me.getSalesOrderPacking()

                        If dgvDataHeader.CurrentRow IsNot Nothing Then
                            Dim strIndex As String
                            strIndex = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_Index2").Value()
                            getSalesOrderItem(strIndex)
                            Me.txtQty.Text = "1"
                        Else
                            Me.dgvData.DataSource = Nothing
                            btnClear_Click(Nothing, Nothing)
                            SetnumRows()

                            Me.txtInvoiceNo.Text = ""
                            Me.txtInvoiceNo.Focus()

                        End If
                    Else
                        W_MSG_Information("ไม่สามารถบันทึกข้อมูลได้")
                        Exit Sub
                    End If
                End If


                'ย้ายไปใน Function Print By : Dong

                'Dim cls As New WithdrawTransaction_Update(WithdrawTransaction_Update.enuOperation_Type.NULL)
                ''Dim DT As New DataTable
                'Dim Withdraw_Index As String
                ''DT = Me.dgvData.DataSource
                ''If Me.dgvData.DataSource Is Nothing Then
                'Withdraw_Index = cls.Get_Withdraw_Index(SalesOrder_No)
                'If cls.Get_QtyPack(Withdraw_Index) = "0" Then
                '    cls.UpdateWithdraw(Withdraw_Index)
                'End If
                ''End If
                ''If DT.Select("(Qty - Qty_Pack) <> 0").Length = 0 Then
                ''    cls.UpdateSalesOrder(Me._SalesOrder_Index)
                ''End If

                Me.btnOpenBox.BackColor = Color.Red
                Me.btnOpenBox2.BackColor = Color.Yellow

            End If

            Me.SetnumRowsBarcode()
            Me.setColorField()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub setColorField(Optional ByVal currentRow As String = "")
        Try


            For i As Integer = 0 To dgvData.Rows.Count - 1
                With dgvData

                    If .Rows(i).Cells("col_QtyTotal").Value = .Rows(i).Cells("col_QtyPacked").Value Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.LightGreen
                    ElseIf .Rows(i).Cells("col_QtyTotal").Value <> .Rows(i).Cells("col_QtyPacked").Value And .Rows(i).Cells("col_QtyPacked").Value <> 0 Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.Yellow
                    ElseIf .Rows(i).Cells("col_QtyPacked").Value = 0 Then
                        .Rows(i).DefaultCellStyle.BackColor = Color.White
                    End If


                    If currentRow <> "" Then
                        If .Rows(i).Cells("col_Sku_Index").Value = currentRow Then
                            .Rows(i).Cells("col_QtyPacked").Style.BackColor = Color.YellowGreen

                        End If

                        If currentRow = "-999" And .Rows(i).Cells("col_QtyPacked").Style.BackColor = Color.YellowGreen Then
                            .Rows(i).Cells("col_QtyPacked").Style.BackColor = .Rows(i).Cells("col_SKU").Style.BackColor
                        End If
                    End If

                End With
            Next
            sku_index_current_Cell = ""
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ChkStatusComplete(ByVal SOP_Index As String)
        Dim objtb_SalesOrderPacking As New tb_SalesOrderPacking
        Dim objDT As DataTable = New DataTable
        'Dim objDTtb_PurchaseOrder As DataTable = New DataTable
        Dim strSO_Index As String = ""

        Try

            Dim iQty_Pack As Integer
            Dim iTotal_Pack As Integer
            objtb_SalesOrderPacking.ChkSOP(SOP_Index)
            objDT = objtb_SalesOrderPacking.DataTable

            If objDT.Rows.Count > 0 Then
                Dim objx As New PackingTransaction(PackingTransaction.enuOperation_Type.UPDATE)
                iQty_Pack = CInt(objDT.Rows(0).Item("Qty_Pack"))
                iTotal_Pack = CInt(objDT.Rows(0).Item("Total_Qty"))
                If iQty_Pack = iTotal_Pack Then
                    'UPdate Status = 16\
                    objx.UpdateStatus(SOP_Index)
                End If



            Else

            End If

            '            Me.dgvData.Rows.Clear()
            '           Me.dgvData.Refresh()
            'Me.dgvData.AutoGenerateColumns = False
            'Me.dgvData.DataSource = Nothing

            'btnSeach.Focus()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_SalesOrderPacking = Nothing
            'objDTtb_PurchaseOrder = Nothing
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
    Private Function ValidateDataBeforeSave() As Boolean

        Dim i As Integer
        Dim dblQty As Double = 0
        Dim dblReceivedQty As Double = 0


        ' ------ STEP 1: Validate required fields

        ' ------ STEP 2: Item Quantity and Unit Price Comparison.


        If dgvDataBarcode.RowCount > 0 Then
            For i = 0 To dgvDataBarcode.Rows.Count - 1
                With dgvDataBarcode

                    ' Check if Qty is numeric value
                    If Not IsNumeric(.Rows(i).Cells("col_qtypack_bx").Value) Then
                        ValidateDataBeforeSave = False
                        .Rows(i).Selected = True
                        W_MSG_Information("ไม่สามารถทำรายการได้ จำนวนแพ็คเป็นตัวเลขเท่านั้น")
                        Exit Function
                    End If

                    ' Check if Qty is <= 0
                    dblQty = .Rows(i).Cells("col_qtypack_bx").Value
                    If dblQty <= 0 Then
                        ValidateDataBeforeSave = False
                        .Rows(i).Selected = True
                        W_MSG_Information("ไม่สามารถทำรายการได้ จำนวนที่แพ็คต้องมากกว่าศูนย์")
                        Exit Function
                    Else

                    End If


                End With


            Next

        End If

        ValidateDataBeforeSave = True

    End Function

    Private Function DoSaveDocument() As String


        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim strTempSKUIndex = ""
        Dim strTempPackageIndex = ""
        Dim dblSKURatio As Double = 1

        ' ------ STEP 1: Declare Data Table Objects
        Dim objtb_SalesOrderPacking As New tb_SalesOrderPacking
        Dim objtb_SalesOrderPackingItem As New tb_SalesOrderPackingItem
        Dim objtb_SalesOrderPackingCollection As New List(Of tb_SalesOrderPackingItem)

        Try
            ' ------ STEP 2: Prepare values for PO Header
            objtb_SalesOrderPacking.SalesOrder_Index = Me.SalesOrder_Index
            ' objtb_SalesOrderPacking.TransportManifestItem_Index = Me.TransportManifestItem_Index

            If Me.cbSizeBox.SelectedValue Is Nothing Then
                objtb_SalesOrderPacking.PackSize_Index = ""
            Else
                objtb_SalesOrderPacking.PackSize_Index = Me.cbSizeBox.SelectedValue.ToString
            End If

            Dim strDocumentTypeIndex As String = "0010000000204"

            ' ja 4 Jan 2010 - Update Run Document Number by DocumentType

            Dim objDBTempIndex As New Sy_AutoNumber
            objtb_SalesOrderPacking.SalesOrderPacking_Index = objDBTempIndex.getSys_Value("SalesOrderPacking_Index")
            Me._SalesOrderPacking_Index = objtb_SalesOrderPacking.SalesOrderPacking_Index
            objDBTempIndex = Nothing


            'บาร์โค้ด Pack Gen Auto
            Dim objDocumentNumber As New WMS_STD_Formula.Sy_DocumentNumber
            objtb_SalesOrderPacking.BarcodePacking = objDocumentNumber.Auto_DocumentType_Number(strDocumentTypeIndex, "")
            BarcodePrint = objtb_SalesOrderPacking.BarcodePacking
            objDocumentNumber = Nothing

            objtb_SalesOrderPacking.Status_Print = 0
            objtb_SalesOrderPacking.Count_Print = 0
            Dim oSeq As New tb_SalesOrderPacking
            objtb_SalesOrderPacking.Seq = oSeq.getSeq_SalesOrderPack(Me.SalesOrder_Index)

            'If Status = 2 Then
            '    objtb_PurchaseOrder.Status = 2
            'Else
            '    objtb_PurchaseOrder.Status = 1
            'End If

            ' ------ STEP 3: Prepare values for PO Items

            Dim oVal As Object
            Dim oRow As DataRow()

            Dim Seq As Integer = 0
            Dim currQty As Double
            Dim MaxSkuQty As Double
            Dim PackedSkuQty As Double

            Dim oAutoIndex As New Sy_AutoNumber

            For Each Row As DataGridViewRow In Me.dgvDataBarcode.Rows
                oRow = Me._DTSOP.Select(String.Format("Sku_Index = '{0}' AND (Total_Qty - Qty_pack) > 0", Row.Cells("col_Sku_Index_Bx").Value.ToString), "Total_Qty ASC")

                oVal = Row.Cells("col_qtypack_Bx").Value
                If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                    currQty = CDbl(oVal.ToString)
                Else
                    currQty = 0
                End If

                For skuRow As Integer = 0 To oRow.Length - 1
                    If Not currQty > 0 Then
                        Exit For
                    End If

                    objtb_SalesOrderPackingItem = New tb_SalesOrderPackingItem

                    With objtb_SalesOrderPackingItem
                        oVal = Row.Cells("col_SalesOrderPackingItem_Index").Value
                        If oVal Is Nothing OrElse String.IsNullOrEmpty(oVal.ToString) Then
                            oVal = oAutoIndex.getSys_Value("SalesOrderPackingItem_Index")
                        End If
                        .SalesOrderPackingItem_Index = oVal.ToString

                        .SalesOrderPacking_Index = objtb_SalesOrderPacking.SalesOrderPacking_Index
                        .SalesOrder_Index = objtb_SalesOrderPacking.SalesOrder_Index

                        .SalesOrderItem_Index = oRow(skuRow).Item("SalesOrderItem_Index").ToString
                        '.TransportManifestItem_Index = objtb_SalesOrderPacking.TransportManifestItem_Index

                        .seq = Seq + 1

                        oVal = Row.Cells("col_Sku_Index_Bx").Value
                        If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                            .Sku_Index = oVal.ToString
                        Else
                            .Sku_Index = String.Empty
                        End If

                        oVal = oRow(skuRow).Item("Total_Qty")
                        If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                            MaxSkuQty = CDbl(oVal.ToString)
                        Else
                            MaxSkuQty = 0
                        End If

                        oVal = oRow(skuRow).Item("Qty_Pack")
                        If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                            PackedSkuQty = CDbl(oVal.ToString)
                        Else
                            PackedSkuQty = 0
                        End If

                        If currQty > MaxSkuQty - PackedSkuQty Then
                            .Qty_Pack = MaxSkuQty - PackedSkuQty
                            .Total_Qty = MaxSkuQty - PackedSkuQty
                            currQty -= MaxSkuQty - PackedSkuQty
                        Else
                            .Qty_Pack = currQty
                            .Total_Qty = currQty
                            currQty -= currQty
                        End If
                    End With

                    objtb_SalesOrderPackingCollection.Add(objtb_SalesOrderPackingItem)

                    Seq += 1
                Next
            Next

            'For i = 0 To dgvDataBarcode.Rows.Count - 1

            '    With dgvDataBarcode
            '        objtb_SalesOrderPackingItem = New tb_SalesOrderPackingItem

            '        'col_qtypack_Bx
            '        'col_SkuID_Bx
            '        'col_str1_Bx
            '        'col_Barcode1_Bx

            '        If .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value = "" Then
            '            Dim objDBIndex As New Sy_AutoNumber
            '            .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value = objDBIndex.getSys_Value("SalesOrderPackingItem_Index")
            '            objtb_SalesOrderPackingItem.SalesOrderPackingItem_Index = .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value
            '            objDBIndex = Nothing
            '        Else
            '            objtb_SalesOrderPackingItem.SalesOrderPackingItem_Index = .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value.ToString
            '        End If
            '        objtb_SalesOrderPackingItem.SalesOrderPacking_Index = objtb_SalesOrderPacking.SalesOrderPacking_Index
            '        objtb_SalesOrderPackingItem.SalesOrder_Index = objtb_SalesOrderPacking.SalesOrder_Index
            '        ' objtb_SalesOrderPackingItem.TransportManifestItem_Index = objtb_SalesOrderPacking.TransportManifestItem_Index

            '        objtb_SalesOrderPackingItem.seq = i + 1


            '        ' objtb_SalesOrderPackingItem.Total_Qty = .Rows(i).Cells("col_qtypack_Bx").Value.ToString

            '        If .Rows(i).Cells("col_qtypack_Bx").Value IsNot Nothing Then
            '            objtb_SalesOrderPackingItem.Qty_Pack = CDbl(.Rows(i).Cells("col_qtypack_Bx").Value.ToString)
            '        Else
            '            objtb_SalesOrderPackingItem.Qty_Pack = 0
            '        End If

            '        If .Rows(i).Cells("col_TotalQty").Value IsNot Nothing Then
            '            objtb_SalesOrderPackingItem.Total_Qty = CDbl(.Rows(i).Cells("col_TotalQty").Value.ToString)
            '        Else
            '            objtb_SalesOrderPackingItem.Total_Qty = 0
            '        End If


            '        If .Rows(i).Cells("col_Sku_Index_Bx").Value Is Nothing Then
            '            objtb_SalesOrderPackingItem.Sku_Index = ""
            '        Else
            '            objtb_SalesOrderPackingItem.Sku_Index = .Rows(i).Cells("col_Sku_Index_Bx").Value.ToString
            '        End If


            '        'If .Rows(i).Cells("col_SalesOrderItem_Index_Bx").Value Is Nothing Then
            '        '    objtb_SalesOrderPackingItem.SalesOrderItem_Index = ""
            '        'Else
            '        '    objtb_SalesOrderPackingItem.SalesOrderItem_Index = .Rows(i).Cells("col_SalesOrderItem_Index_Bx").Value.ToString
            '        'End If

            '    End With

            '    objtb_SalesOrderPackingCollection.Add(objtb_SalesOrderPackingItem)

            'Next


            ' ------ STEP 6: Call the actual saving function in POTransaction class. 

            Dim objDBPOTransaction As New PackingTransaction(PackingTransaction.enuOperation_Type.ADDNEW, objtb_SalesOrderPacking, objtb_SalesOrderPackingCollection)
            Me._SalesOrderPacking_Index = objDBPOTransaction.SaveData
            objDBPOTransaction = Nothing



            'Check & Update Status SO
            Me.ChkStatusComplete(SalesOrder_Index)

            ' If save succeeded, _PurchaseOrder_Index will not be ""
            DoSaveDocument = Me._SalesOrderPacking_Index



            'KSL
            Dim strSQL As String = ""
            strSQL = "UPDATE tb_SalesOrderPacking "
            strSQL &= " SET addby_user_Index = '" & WV_User_Index & "'"
            strSQL &= " WHERE SalesOrderPacking_Index ='" & Me._SalesOrderPacking_Index & "'"
            Dim objDB As New DBType_SQLServer
            objDB.DBExeQuery(strSQL)

        Catch ex As Exception
            Throw ex
            DoSaveDocument = ""
            'objtb_PurchaseOrder.Dispose()
            'objtb_PurchaseOrderItemCollection = Nothing
        Finally
            objtb_SalesOrderPacking.disconnectDB()
            objtb_SalesOrderPackingCollection = Nothing
        End Try

    End Function


    Private Function DoSaveDocument(ByVal Row As DataGridViewRow) As String


        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim k As Integer = 0
        Dim strTempSKUIndex = ""
        Dim strTempPackageIndex = ""
        Dim dblSKURatio As Double = 1

        ' ------ STEP 1: Declare Data Table Objects
        Dim objtb_SalesOrderPacking As New tb_SalesOrderPacking
        Dim objtb_SalesOrderPackingItem As New tb_SalesOrderPackingItem
        Dim objtb_SalesOrderPackingCollection As New List(Of tb_SalesOrderPackingItem)

        Try
            ' ------ STEP 2: Prepare values for PO Header
            objtb_SalesOrderPacking.SalesOrder_Index = Me.SalesOrder_Index
            ' objtb_SalesOrderPacking.TransportManifestItem_Index = Me.TransportManifestItem_Index
            If Me._boolPackOne Then
                objtb_SalesOrderPacking.TransportManifestItem_Index = "NoPack"
            End If
            If Me.cbSizeBox.SelectedValue Is Nothing Then
                objtb_SalesOrderPacking.PackSize_Index = ""
            Else
                objtb_SalesOrderPacking.PackSize_Index = Me.cbSizeBox.SelectedValue.ToString
            End If

            Dim strDocumentTypeIndex As String = "0010000000204"

            ' ja 4 Jan 2010 - Update Run Document Number by DocumentType

            Dim objDBTempIndex As New Sy_AutoNumber
            objtb_SalesOrderPacking.SalesOrderPacking_Index = objDBTempIndex.getSys_Value("SalesOrderPacking_Index")
            Me._SalesOrderPacking_Index = objtb_SalesOrderPacking.SalesOrderPacking_Index
            objDBTempIndex = Nothing


            'บาร์โค้ด Pack Gen Auto
            Dim objDocumentNumber As New WMS_STD_Formula.Sy_DocumentNumber
            objtb_SalesOrderPacking.BarcodePacking = objDocumentNumber.Auto_DocumentType_Number(strDocumentTypeIndex, "")
            BarcodePrint = objtb_SalesOrderPacking.BarcodePacking
            objDocumentNumber = Nothing

            objtb_SalesOrderPacking.Status_Print = 0
            objtb_SalesOrderPacking.Count_Print = 0
            Dim oSeq As New tb_SalesOrderPacking
            objtb_SalesOrderPacking.Seq = oSeq.getSeq_SalesOrderPack(Me.SalesOrder_Index)

            'If Status = 2 Then
            '    objtb_PurchaseOrder.Status = 2
            'Else
            '    objtb_PurchaseOrder.Status = 1
            'End If

            ' ------ STEP 3: Prepare values for PO Items

            Dim oVal As Object
            Dim oRow As DataRow()

            Dim Seq As Integer = 0
            Dim currQty As Double
            Dim MaxSkuQty As Double
            Dim PackedSkuQty As Double

            Dim oAutoIndex As New Sy_AutoNumber

            'For Each Row As DataRow In pRow
            oRow = Me._DTSOP.Select(String.Format("Sku_Index = '{0}' AND (Total_Qty - Qty_pack) > 0", Row.Cells("col_Sku_Index_Bx").Value.ToString), "Total_Qty ASC")

            oVal = Row.Cells("col_qtypack_Bx").Value
            If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                currQty = CDbl(oVal.ToString)
            Else
                currQty = 0
            End If

            For skuRow As Integer = 0 To oRow.Length - 1
                If Not currQty > 0 Then
                    Exit For
                End If

                objtb_SalesOrderPackingItem = New tb_SalesOrderPackingItem

                With objtb_SalesOrderPackingItem
                    oVal = Row.Cells("col_SalesOrderPackingItem_Index").Value
                    If oVal Is Nothing OrElse String.IsNullOrEmpty(oVal.ToString) Then
                        oVal = oAutoIndex.getSys_Value("SalesOrderPackingItem_Index")
                    End If
                    .SalesOrderPackingItem_Index = oVal.ToString

                    .SalesOrderPacking_Index = objtb_SalesOrderPacking.SalesOrderPacking_Index
                    .SalesOrder_Index = objtb_SalesOrderPacking.SalesOrder_Index

                    .SalesOrderItem_Index = oRow(skuRow).Item("SalesOrderItem_Index").ToString
                    '.TransportManifestItem_Index = objtb_SalesOrderPacking.TransportManifestItem_Index

                    .seq = Seq + 1

                    oVal = Row.Cells("col_Sku_Index_Bx").Value
                    If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                        .Sku_Index = oVal.ToString
                    Else
                        .Sku_Index = String.Empty
                    End If

                    oVal = oRow(skuRow).Item("Total_Qty")
                    If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                        MaxSkuQty = CDbl(oVal.ToString)
                    Else
                        MaxSkuQty = 0
                    End If

                    oVal = oRow(skuRow).Item("Qty_Pack")
                    If oVal IsNot Nothing AndAlso Not String.IsNullOrEmpty(oVal.ToString) Then
                        PackedSkuQty = CDbl(oVal.ToString)
                    Else
                        PackedSkuQty = 0
                    End If

                    If currQty > MaxSkuQty - PackedSkuQty Then
                        .Qty_Pack = MaxSkuQty - PackedSkuQty
                        .Total_Qty = MaxSkuQty - PackedSkuQty
                        currQty -= MaxSkuQty - PackedSkuQty
                    Else
                        .Qty_Pack = currQty
                        .Total_Qty = currQty
                        currQty -= currQty
                    End If
                End With

                objtb_SalesOrderPackingCollection.Add(objtb_SalesOrderPackingItem)

                Seq += 1
                ' Next
            Next

            'For i = 0 To dgvDataBarcode.Rows.Count - 1

            '    With dgvDataBarcode
            '        objtb_SalesOrderPackingItem = New tb_SalesOrderPackingItem

            '        'col_qtypack_Bx
            '        'col_SkuID_Bx
            '        'col_str1_Bx
            '        'col_Barcode1_Bx

            '        If .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value = "" Then
            '            Dim objDBIndex As New Sy_AutoNumber
            '            .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value = objDBIndex.getSys_Value("SalesOrderPackingItem_Index")
            '            objtb_SalesOrderPackingItem.SalesOrderPackingItem_Index = .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value
            '            objDBIndex = Nothing
            '        Else
            '            objtb_SalesOrderPackingItem.SalesOrderPackingItem_Index = .Rows(i).Cells("col_SalesOrderPackingItem_Index").Value.ToString
            '        End If
            '        objtb_SalesOrderPackingItem.SalesOrderPacking_Index = objtb_SalesOrderPacking.SalesOrderPacking_Index
            '        objtb_SalesOrderPackingItem.SalesOrder_Index = objtb_SalesOrderPacking.SalesOrder_Index
            '        ' objtb_SalesOrderPackingItem.TransportManifestItem_Index = objtb_SalesOrderPacking.TransportManifestItem_Index

            '        objtb_SalesOrderPackingItem.seq = i + 1


            '        ' objtb_SalesOrderPackingItem.Total_Qty = .Rows(i).Cells("col_qtypack_Bx").Value.ToString

            '        If .Rows(i).Cells("col_qtypack_Bx").Value IsNot Nothing Then
            '            objtb_SalesOrderPackingItem.Qty_Pack = CDbl(.Rows(i).Cells("col_qtypack_Bx").Value.ToString)
            '        Else
            '            objtb_SalesOrderPackingItem.Qty_Pack = 0
            '        End If

            '        If .Rows(i).Cells("col_TotalQty").Value IsNot Nothing Then
            '            objtb_SalesOrderPackingItem.Total_Qty = CDbl(.Rows(i).Cells("col_TotalQty").Value.ToString)
            '        Else
            '            objtb_SalesOrderPackingItem.Total_Qty = 0
            '        End If


            '        If .Rows(i).Cells("col_Sku_Index_Bx").Value Is Nothing Then
            '            objtb_SalesOrderPackingItem.Sku_Index = ""
            '        Else
            '            objtb_SalesOrderPackingItem.Sku_Index = .Rows(i).Cells("col_Sku_Index_Bx").Value.ToString
            '        End If


            '        'If .Rows(i).Cells("col_SalesOrderItem_Index_Bx").Value Is Nothing Then
            '        '    objtb_SalesOrderPackingItem.SalesOrderItem_Index = ""
            '        'Else
            '        '    objtb_SalesOrderPackingItem.SalesOrderItem_Index = .Rows(i).Cells("col_SalesOrderItem_Index_Bx").Value.ToString
            '        'End If

            '    End With

            '    objtb_SalesOrderPackingCollection.Add(objtb_SalesOrderPackingItem)

            'Next


            ' ------ STEP 6: Call the actual saving function in POTransaction class. 

            Dim objDBPOTransaction As New PackingTransaction(PackingTransaction.enuOperation_Type.ADDNEW, objtb_SalesOrderPacking, objtb_SalesOrderPackingCollection)
            Me._SalesOrderPacking_Index = objDBPOTransaction.SaveData
            objDBPOTransaction = Nothing


            'Check & Update Status SO
            Me.ChkStatusComplete(SalesOrder_Index)

            ' If save succeeded, _PurchaseOrder_Index will not be ""
            DoSaveDocument = Me._SalesOrderPacking_Index

        Catch ex As Exception
            Throw ex
            DoSaveDocument = ""
            'objtb_PurchaseOrder.Dispose()
            'objtb_PurchaseOrderItemCollection = Nothing
        Finally
            objtb_SalesOrderPacking.disconnectDB()
            objtb_SalesOrderPackingCollection = Nothing
        End Try

    End Function

    Private Sub txtBarcodeBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtBarcodeBox.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If String.IsNullOrEmpty(Me.txtQty.Text.Trim) OrElse Not IsNumeric(Me.txtQty.Text.Trim) Then
                    W_MSG_Information("กรุณาระบุตัวเลข !!")
                    Me.txtQty.Focus()
                    Exit Sub
                End If

                If String.IsNullOrEmpty(Me.txtBarcodeBox.Text.Trim) Then
                    W_MSG_Information("กรุณาระบุข้อมูล Barcode !!")
                    Exit Sub
                End If

                If Packing() Then
                    If Me._boolPackOne = True Then
                        Me.btnCloseBox_Click(sender, e)
                    End If
                    Me.txtQty.Text = 1
                End If
            End If


            Me.SetnumRowsBarcode()

            setColorField(sku_index_current_Cell)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cbSizeBox_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbSizeBox.SelectedIndexChanged
        Try
            Me.txtBarcodeBox.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub dgvDataBarcode_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles dgvDataBarcode.RowsAdded
        SetnumRowsBarcode()
    End Sub

    Private Sub dgvDataBarcode_RowsRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles dgvDataBarcode.RowsRemoved
        SetnumRowsBarcode()
    End Sub

    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        'If MessageBox.Show("คุณต้องการออกจากเมนูการแพ็คสินค้าใช่หรือไม่", "ออกเมนู", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes = True Then
        Me.Close()
        'Else
        '    Exit Sub
        'End If
        'Try
        '    If Me.txtInvoiceBox.Text.Trim = "" Then Exit Sub
        '    Dim objtb_SalesOrderPacking As New tb_SalesOrderPacking
        '    Dim objDTHeader As DataTable = New DataTable
        '    Dim strWhere As String = ""

        '    If Me.txtInvoiceNo.Text <> "" Then
        '        strWhere &= " AND (SalesOrder_No Like '%" & Me.txtInvoiceNo.Text.Trim.Replace("'", "''") & "%') "
        '    End If
        '    strWhere &= String.Format(" AND (Status_Pack = '{0}') ", "1")
        '    objDTHeader = objtb_SalesOrderPacking.GetPackingHeader(strWhere)

        '    If objDTHeader.Rows.Count > 0 Then
        '        If W_MSG_Confirm(Me.Label1.Text & " : " & Me.txtInvoiceNo.Text.Trim & "ยังแพ็คสินค้าไม่เสร็จ" & Chr(13) & "คุณต้องการปิดหน้าจอใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
        '            Exit Sub
        '        End If
        '    End If
        '    Me.Close()
        'Catch ex As Exception
        '    W_MSG_Error(ex.Message)
        'End Try

    End Sub

    Private Sub btnPackDetail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPackDetail.Click
        Try
            Dim frm As New frmJobPackingDetailBarcode
            frm.Icon = Me.Icon
            frm.SalesOrder_No = ""
            frm.SalesOrder_Index = ""
            If dgvData.RowCount > 0 Then
                If _SalesOrder_No <> "" Then
                    frm.SalesOrder_No = dgvData.CurrentRow.Cells("col_InvoiceNo").Value
                Else
                    frm.SalesOrder_No = ""
                End If
                frm.SalesOrder_Index = dgvData.CurrentRow.Cells("col_SalesOrder_Index").Value
            End If

            '    frm.SalesOrderPacking_Index = dgvData.CurrentRow.Cells("col_InvoiceNo").Value
            frm.ShowDialog()
            '   Me.getPOViewSearch()

            If frm.DialogResult = Windows.Forms.DialogResult.OK Then
                Me.getSalesOrderPacking()

                If dgvDataHeader.CurrentRow IsNot Nothing Then
                    Dim strIndex As String
                    strIndex = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_Index2").Value()
                    getSalesOrderItem(strIndex)

                    Me.txtQty.Text = "1"
                Else
                    Me.dgvData.DataSource = Nothing

                    btnClear_Click(Nothing, Nothing)

                    SetnumRows()
                End If
            End If

            btnSeach_Click(Nothing, Nothing)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            Me._DtBarCode.Rows.Clear()
            'Me._DtBarCode.AcceptChanges()
            'Me.dgvDataBarcode.Update()

            _DtBox = Nothing
            Me.btnOpenBox.Enabled = True
            Me.btnOpenBox2.Enabled = True



            Me.btnCloseBox.Enabled = False

            Me.txtQty.Text = "1"
            Me.txtBarcodeBox.Text = ""
            Me.txtInvoiceBox.Text = ""
            Me.txtBarcodeBox.Enabled = False

            Me.SetnumRowsBarcode()

            Me.btnOpenBox.BackColor = Color.Red
            Me.btnOpenBox2.BackColor = Color.Yellow

            setColorField("-999")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub ChkQty()
        Try
            Dim obj As New ml_MappingBarcode
            Dim oDTSku_Dupp As New DataTable
            If chkQtyDb.Checked Then
                If Me.txtBarcodeBox.Text <> "" Then
                    oDTSku_Dupp = obj.GetCheckingBarcodeDupp(Me.txtBarcodeBox.Text.Trim)
                    If oDTSku_Dupp.Rows.Count > 0 Then
                        If IsNumeric(oDTSku_Dupp.Rows(0).Item("Qty_Barcode")) Then
                            If oDTSku_Dupp.Rows(0).Item("Qty_Barcode") > 0 Then
                                'Me.txtQty.Text = oDTSku_Dupp.Rows(0).Item("Qty_Barcode").ToString
                            End If
                        End If

                    End If
                Else
                    W_MSG_Information("กรอกข้อมูล Barcode ก่อน")
                End If
            Else
                'Me.txtQty.Text = 1
            End If



        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub chkQtyDb_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkQtyDb.CheckedChanged
        Try
            If chkQtyDb.Checked Then

            Else
                Me.txtQty.Text = 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnOpenBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOpenBox2.Click
        Try
            Me.btnOpenBox_Click(sender, e)
            Me._boolPackOne = True

            Me.btnOpenBox.BackColor = Color.Gray
            Me.btnOpenBox2.BackColor = Color.Yellow

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub dgvDataHeader_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvDataHeader.CellClick
        Try
            If dgvDataHeader.CurrentRow Is Nothing Then Exit Sub
            Dim strIndex As String
            strIndex = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_Index2").Value()
            getSalesOrderItem(strIndex)

            Me.txtBarcodeBox.Clear()
            If Me.dgvDataHeader.RowCount > 0 Then
                Me.txtInvoiceNo.Text = dgvDataHeader.CurrentRow.Cells("Col_SalesOrder_No2").Value()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtQty_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtQty.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If Not IsNumeric(Me.txtQty.Text) Then
                    W_MSG_Information("กรุณาระบุตัวเลข")
                    Me.txtQty.Text = 1
                    Me.txtQty.Focus()
                    Exit Sub
                End If
                Me.txtBarcodeBox.Focus()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkPack_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkPack.CheckedChanged
        Try
            Me.dgvDataHeader.DataSource = Nothing
            Me.dgvData.DataSource = Nothing

            btnClear_Click(sender, e)

            SetnumRows()

        Catch Ex As Exception
            W_MSG_Error(Ex.Message)
        End Try
    End Sub

    Private Sub frmJobPacking_Update_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If Me.txtInvoiceBox.Text.Trim = "" Then Exit Sub

            If Me.txtInvoiceBox.Text <> "" Then
                Dim oBJ As New tb_SalesOrderPacking
                Dim dtTNP As New DataTable
                oBJ.getCheckPick_Pack_SO(Me.txtInvoiceBox.Text.Trim.Replace("'", "''"))
                dtTNP = oBJ.GetDataTable
                If FormatNumber(dtTNP.Rows(0)("WQTY"), 2) <> FormatNumber(dtTNP.Rows(0)("PQTY"), 2) Then
                    If W_MSG_Confirm(Me.Label1.Text & " : " & Me.txtInvoiceBox.Text.Trim & " ยังแพ็คสินค้าไม่เสร็จ " & Chr(13) & "คุณต้องการปิดหน้าจอใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                        e.Cancel = True
                        Exit Sub
                    End If
                End If


            End If

            e.Cancel = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub



    Private Sub txtInvoiceBox_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtInvoiceNo.TextChanged
        Try
            Me.showpack()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub showpack()
        Try
            Dim dblPack As Double = 0
            Dim dblNoPack As Double = 0

            If Me.txtInvoiceNo.Text.Trim <> "" Then
                Dim opack As New tb_SalesOrderPackingItem
                Dim dtTemp As New DataTable
                Dim strWhere As String = " AND (SalesOrder_No = '" & Me.txtInvoiceNo.Text & "') "
                dtTemp = opack.GetAllDataSOP_CountPack(strWhere)

                dblPack = dtTemp.Rows(0)("Pack")
                dblNoPack = dtTemp.Rows(0)("NoPAck")
            End If

            Me.lblPack.Text = String.Format("ลงกล่อง : {0} " & Chr(13) & "ไม่ลงกล่อง : {1}", dblPack.ToString, dblNoPack.ToString)

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class

