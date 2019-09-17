Imports System.IO
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master

Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Globalization
'Imports Microsoft.Office.Interop

Public Class frmImport_SO

    'Private _DEFAULT_IMPORT_SO_PATH As String
    'Private _DEFAULT_DocumentType As String
    'Private _DEFAULT_SALE_NORMAL As String
    'Private _DEFAULT_SALE_RETURN As String
    'Private _DEFAULT_CURRENCY_INDEX As String
    Private _boolResult As String
    'Dim strDestination As String = ""
    'Dim SubFolder As String = "DO-" & Now.ToString("yyyyMMdd_HHmmss")
    'Dim _osbNewSKU As New StringBuilder
    'Dim _osbErr_Dup As New StringBuilder
    'Dim _osbErr_Data_Incomplete As New StringBuilder
    Private _Customer_Index As String = ""
    Private _DocumentType_Index As String = ""
    Private _EPSON_Location_Index As String = ""
#Region "Operation Type "
    Private objStatus As enuOperation_Type

    Public Enum enuOperation_Type
        SO
        SO_CN2
        CANCEL
        NULL
    End Enum
#End Region

#Region " CONSTRUCTOR ,DESTRUCTOR ,DISPOSE ,CLEAR "

    Public Sub New(ByVal Operation_Type As enuOperation_Type)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub
#End Region
    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Property DocumentType_Index() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property

    Property EPSON_Location_Index() As String
        Get
            Return _EPSON_Location_Index
        End Get
        Set(ByVal value As String)
            _EPSON_Location_Index = value
        End Set
    End Property

    Private Sub getCustomer()
        Dim objms_Customer As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDTms_Customer As DataTable = New DataTable

        Try
            'objms_Customer.getPopup_Customer("Customer_Index", Me.txtCustomer_Id.Tag.ToString)
            objms_Customer.SearchData_Click("", " and Customer_Index='" & Me._Customer_Index & "'")
            objDTms_Customer = objms_Customer.DataTable
            If objDTms_Customer.Rows.Count > 0 Then
                Me._Customer_Index = objDTms_Customer.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDTms_Customer.Rows(0).Item("Customer_Id").ToString
                'Me.txtCustomer_Name.Text = objDTms_Customer.Rows(0).Item("Customer_Name").ToString

            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                'Me.txtCustomer_Name.Text = ""

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objms_Customer = Nothing
            objDTms_Customer = Nothing
        End Try
    End Sub

    Sub SET_DEFAULT_CUSTOMER_BYUSER()
        Try
            Dim tCustomer_Index As String
            Dim objconfig As New config_UserByCustomer(config_UserByCustomer.enuOperation_Type.SEARCH)
            objconfig.GetCustomerDefault(WV_User_Index)
            tCustomer_Index = objconfig.ScalarOutput
            If tCustomer_Index <> "" Then
                Me._Customer_Index = tCustomer_Index
                Me.getCustomer()
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub AddcbProductType()
    '    Dim objClassDB As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable
    '    Try
    '        objClassDB.SearchData_Click("TIMCO_ShowBarcode", " AND TIMCO_ShowBarcode > 0")
    '        objDT = objClassDB.DataTable

    '        cbProductGroup.DisplayMember = "ProductType_ID"
    '        cbProductGroup.ValueMember = "ProductType_Index"
    '        cbProductGroup.DataSource = objDT

    '        If objDT.Rows.Count > 0 Then
    '            cbProductGroup.SelectedIndex = 0
    '        End If
    '    Catch ex As Exception
    '        MessageBox.Show(ex.Message)
    '    Finally
    '        objDT = Nothing
    '        objClassDB = Nothing
    '    End Try
    'End Sub

    Private Sub frmImport_DO_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language

            ''Insert
            'oFunction.Insert(Me)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me)

            Me.SET_DEFAULT_CUSTOMER_BYUSER()

            '===============================
            Thread.CurrentThread.CurrentCulture = New CultureInfo("en-GB")
            Dim appSet As New Configuration.AppSettingsReader()
            WV_ConnectionString = appSet.GetValue("ConnectionString", GetType(String))
            'Me._DEFAULT_IMPORT_SO_PATH = appSet.GetValue("DEFAULT_IMPORT_SO_PATH", GetType(String)) 'AppSettings("DEFAULT_IMPORT_SO_PATH")
            'Me._DEFAULT_SALE_NORMAL = appSet.GetValue("DEFAULT_SALE_NORMAL", GetType(String)) ' AppSettings("DEFAULT_SALE_NORMAL")
            'Me._DEFAULT_SALE_RETURN = appSet.GetValue("DEFAULT_SALE_RETURN", GetType(String)) ' AppSettings("DEFAULT_SALE_RETURN")
            'Me._DEFAULT_CURRENCY_INDEX = appSet.GetValue("DEFAULT_CURRENCY_INDEX", GetType(String)) 'AppSettings("DEFAULT_CURRENCY_INDEX")

            'Me.Label1.Text &= Me._DEFAULT_IMPORT_SO_PATH
            Me.getDocumentType(10)
            'Me.AddcbProductType()

            dtpSO_Date.Value = Now
            Select Case objStatus
                Case enuOperation_Type.SO
                    Me.Text = "Import Shipment Data [EXCEL]"
                Case enuOperation_Type.SO_CN2
                    Me.lblConsignee.Visible = True
                    Me.txtConsignee_Id.Visible = True
                    Me.btnConsignee.Visible = True
                    Me.txtConsignee_Name.Visible = True


                    Me.Text = "Import CN2_Shipment Data [EXCEL]"
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)

        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable

            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    'OldCode
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

    '        'set sub folder
    '        'SubFolder = "DO-" & Now.ToString("yyyyMMdd_HHmmss")
    '        Dim oImport_SO As New bl_Import_SO

    '        '===============
    '        Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
    '        '===============

    '        oImport_SO.DataSource = grdPreviewData.DataSource
    '        'oImport_SO.ReadText_DO()
    '        oImport_SO.ImportSOExcel()
    '        'Me.Label2.Text &= oImport_SO.FileMove
    '        MsgBox("Import Data Completed.")
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try

    'End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            ''checking
            'If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
            '    Exit Sub
            'End If

            '

            ''===============
            'Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If

            ''===============

            'oImport_SO.DataSource = grdPreviewData.DataSource
            Select Case objStatus
                Case enuOperation_Type.SO
                    Dim oImport_SO As New bl_Import_SO

                    With oImport_SO
                        .DataSource = Me.grdPreviewData.DataSource
                        .Customer_Index = Me.Customer_Index
                        .DocumentType_Index = Me.DocumentType_Index
                        .EPSON_Location_Index = Me.EPSON_Location_Index
                        .So_Date = Me.dtpSO_Date.Value

                        'เพิ่มการบันทึกการใช้รถของtimco
                        If Me.rd_Timco.Checked = True Then
                            .Use_car = 1
                        ElseIf Me.rd_Customer.Checked = True Then
                            .Use_car = 2
                        ElseIf Me.rd_None.Checked = True Then
                            .Use_car = 3
                        End If

                    End With
                    'Check Data
                    If Check_Data() = False Then Exit Sub

                    '
                    Dim i As Integer
                    Dim All_OK As Boolean = True
                    For i = 0 To grdPreviewData.Rows.Count - 1
                        With grdPreviewData
                            If .Rows(i).Cells("Check_Result").Value() <> "OK" Then
                                All_OK = False
                                Exit For
                            End If
                        End With
                    Next
                    '==============
                    If All_OK = True Then
                        oImport_SO.ImportSOExcel()
                        If oImport_SO.ImportComplete = True Then
                            MsgBox("Import Data Completed.")
                        Else
                            MsgBox("ยกเลิก Shipment จากการ Interface ไม่สำเร็จ")
                        End If

                    Else
                        MsgBox("Please Check Data.")
                    End If
                Case enuOperation_Type.SO_CN2

                    'If txtConsignee_Id.Text = "" Or txtConsignee_Id.Tag = "" Then
                    '    W_MSG_Information_ByIndex(9)
                    '    txtConsignee_Id.Focus()
                    '    Exit Sub
                    'End If

                    'Dim oImport_SO_CN2 As New bl_Import_SO_CN2

                    'With oImport_SO_CN2
                    '    .DataSource = Me.grdPreviewData.DataSource
                    '    .Customer_Index = Me.Customer_Index
                    '    .DocumentType_Index = Me.DocumentType_Index
                    '    .EPSON_Location_Index = Me.EPSON_Location_Index
                    '    .So_Date = Me.dtpSO_Date.Value
                    '    .Careof_No = Me.txtConsignee_Id.Text

                    '    'เพิ่มการบันทึกการใช้รถของtimco
                    '    If Me.rd_Timco.Checked = True Then
                    '        .Use_car = 1
                    '    ElseIf Me.rd_Customer.Checked = True Then
                    '        .Use_car = 2
                    '    ElseIf Me.rd_None.Checked = True Then
                    '        .Use_car = 3
                    '    End If

                    '    Dim filename2 As String = ""
                    '    Dim filename1() As String
                    '    If Me.txtFilePath.Text.Trim <> "" Then
                    '        filename1 = Me.txtFilePath.Text.Split("\")
                    '        filename2 = filename1(filename1.Length - 1)
                    '    End If

                    '    .Str1 = filename2

                    'End With
                    ''Check Data
                    'If Check_Data() = False Then Exit Sub

                    ''
                    'Dim i As Integer
                    'Dim All_OK As Boolean = True
                    'For i = 0 To grdPreviewData.Rows.Count - 1
                    '    With grdPreviewData
                    '        If .Rows(i).Cells("Check_Result").Value() <> "OK" Then
                    '            All_OK = False
                    '            Exit For
                    '        End If
                    '    End With
                    'Next
                    ''==============
                    'If All_OK = True Then
                    '    oImport_SO_CN2.ImportSOExcel()
                    '    If oImport_SO_CN2.ImportComplete = True Then
                    '        MsgBox("Import Data Completed.")
                    '    Else
                    '        MsgBox("ยกเลิก Shipment จากการ Interface ไม่สำเร็จ")
                    '    End If

                    'Else
                    '    MsgBox("Please Check Data.")
                    'End If

            End Select

           
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Function Check_Data() As Boolean
        Try
            Select Case objStatus
                Case enuOperation_Type.SO
                    Dim i As Integer
                    Dim oImport_SO As New bl_Import_SO
                    'Dim LocalCheck As Boolean = True

                    Check_Data = True

                    For i = 0 To grdPreviewData.Rows.Count - 1
                        With grdPreviewData

                            ' 1 = InvoiceNumber (Invoice Number)                    
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Cust_Inv_No").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "Customer Order No. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Cust_Inv_No").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "Customer Order No. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                Dim objSo As New tb_SalesOrder
                                Dim dtSO As New DataTable

                                dtSO = oImport_SO.GetSaleOrder(.Rows(i).Cells("Cust_Inv_No").Value.ToString, Me.Customer_Index)
                                If dtSO.Rows.Count > 0 Then
                                    .Rows(i).Cells("Check_Result").Value = "พบ Cust.Order No. ที่ (" & dtSO.Rows(0)("SalesOrder_No").ToString & ")"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            ' 2 = Care of (Sold to number)
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Careof_No").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "Care of ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Careof_No").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "Care of ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                If oImport_SO.GetCareOf_Index(.Rows(i).Cells("Careof_No").Value.ToString, Me.Customer_Index).Trim.ToString = "" Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ Care of"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            ' 3 = SKU (Product number)
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Product_No").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Product_No").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                If oImport_SO.GetSKU_Index(.Rows(i).Cells("Product_No").Value.ToString, Me.Customer_Index).Trim.ToString = "" Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ Product Code"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            ' 4 = Qty
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Qty").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Qty").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                If Val(.Rows(i).Cells("Qty").Value) <= 0 Then
                                    .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            '5
                            If IsDBNull(.Rows(i).Cells("Lot").Value) Then
                                .Rows(i).Cells("Lot").Value = ""
                            End If

                            '6 Check Inventory
                            If ChkInventory.Checked = True Then
                                .Rows(i).Cells("Check_Result").Value = SearchInv(Me.Customer_Index, .Rows(i).Cells("Product_No").Value.ToString, .Rows(i).Cells("Qty").Value)
                            End If

                        End With
                    Next

                Case enuOperation_Type.SO_CN2
                    'Dim i As Integer
                    'Dim oImport_SO_CN2 As New bl_Import_SO_CN2
                    ''Dim LocalCheck As Boolean = True

                    'Check_Data = True
                    'For i = 0 To grdPreviewData.Rows.Count - 1
                    '    With grdPreviewData
                    '        ' 3 = SKU (Product number)
                    '        If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Product_No").Value, GetType(String)).ToString = "") Then
                    '            .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        ElseIf .Rows(i).Cells("Product_No").Value.ToString = "" Then
                    '            .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        Else
                    '            If oImport_SO_CN2.GetSKU_Index(.Rows(i).Cells("Product_No").Value.ToString, Me.Customer_Index).Trim.ToString = "" Then
                    '                .Rows(i).Cells("Check_Result").Value = "ไม่พบ Product Code"
                    '                Check_Data = False
                    '                Continue For
                    '            Else
                    '                .Rows(i).Cells("Check_Result").Value = "OK"
                    '            End If
                    '        End If

                    '        ' 4 = Qty
                    '        If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Qty").Value, GetType(String)).ToString = "") Then
                    '            .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        ElseIf .Rows(i).Cells("Qty").Value.ToString = "" Then
                    '            .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        Else
                    '            If Val(.Rows(i).Cells("Qty").Value) <= 0 Then
                    '                .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                    '                Check_Data = False
                    '                Continue For
                    '            Else
                    '                .Rows(i).Cells("Check_Result").Value = "OK"
                    '            End If
                    '        End If
                    '        '5
                    '        If IsDBNull(.Rows(i).Cells("Lot").Value) Then
                    '            .Rows(i).Cells("Lot").Value = ""
                    '        End If
                    '        '5
                    '        If IsDBNull(.Rows(i).Cells("Line_Remark").Value) Then
                    '            .Rows(i).Cells("Line_Remark").Value = ""
                    '        End If
                    '    End With                   
                    'Next
            End Select
           
            Exit Function

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function SearchInv(ByVal tmpCustomer_Index As String, ByVal tmpSkuID As String, ByVal tmpSkuQty As Double) As String

        Dim objClassDB As New bl_Import_SO
        Dim objDT As DataTable = New DataTable
        Dim intRatio As Integer = 0

        Try
            SearchInv = "-"

            objClassDB.SearchSkuInv(tmpCustomer_Index, tmpSkuID)
            objDT = objClassDB.GetDataTable

            If objDT.Rows.Count > 0 Then
                If Val(tmpSkuQty) > Val(objDT.Rows(0).Item("Qty_Free").ToString) Then
                    SearchInv = "จำนวนสินค้าที่เบิกได้ (Available) : " & Val(objDT.Rows(0).Item("Qty_Free").ToString)
                Else
                    SearchInv = "OK"
                End If
                Exit Function
            Else
                SearchInv = "จำนวนสินค้าคงเหลือ (Inventory) : 0 "
                Exit Function
            End If

            SearchInv = "-"

        Catch ex As Exception
            Throw ex
        Finally

            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    '#Region "   ReadTxT Import SO   "
    '    Public Function ReadAndSplit(ByVal myReader As String, ByVal strDelimited As String)
    '        Try

    '            Dim CurrLine As String = myReader
    '            Dim strSplit() As String
    '            Dim strSaleOrderNo As String = ""

    '            strSplit = CurrLine.Split(strDelimited)

    '            Return strSplit
    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Sub ReadText_DO()
    '        Try
    '            Dim DetailFileList() As String = Directory.GetFiles(Me._DEFAULT_IMPORT_SO_PATH, "*.txt")
    '            For i As Integer = 0 To DetailFileList.Length - 1
    '                ReadDetail(DetailFileList(i))
    '            Next

    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Sub

    '    Public Sub ReadDetail(ByVal pstrFilePath As String)
    '        Dim strSalesOrder_No As String = ""
    '        Dim strDelimited As String = vbTab
    '        Dim obl_Import_SO_Item As New bl_Import_SO_Item

    '        Try
    '            Dim myReader As StreamReader
    '            Dim Current_Text_Line As String = ""
    '            Dim strCustomerShipping_Index As String = ""
    '            Dim strInvoice_No As String = ""
    '            Dim strSplit() As String
    '            Dim strTempText As New ArrayList
    '            Dim strSku_Index As String = ""
    '            Dim strArray(2) As String

    '            Dim iTotal_Header As Integer = 0
    '            Dim iTotal_Detail As Integer = 0

    '            Dim iCount_Comp_Header As Integer = 0
    '            Dim iCount_Comp_Detail As Integer = 0

    '            Dim iCount_Error_Header As Integer = 0
    '            Dim iCount_Error_Detail As Integer = 0
    '            ' Dim osb As New StringBuilder

    '            Dim iCurrent_Line_Number As Integer = 0

    '            Dim obl_Log As New bl_Log

    '            myReader = New StreamReader(pstrFilePath, System.Text.UnicodeEncoding.Default)


    '            _osbNewSKU = New StringBuilder
    '            _osbErr_Dup = New StringBuilder
    '            _osbErr_Data_Incomplete = New StringBuilder


    '            _osbErr_Dup.AppendLine("Error : Duplicate data")
    '            _osbErr_Dup.AppendLine("")

    '            _osbErr_Data_Incomplete.AppendLine("")
    '            _osbErr_Data_Incomplete.AppendLine("Error : Data Incomplete")
    '            _osbErr_Data_Incomplete.AppendLine("")

    '            _osbNewSKU.AppendLine("******************************************************************************")
    '            _osbNewSKU.AppendLine("[4] New SKU")
    '            _osbNewSKU.AppendLine("******************************************************************************")
    '            _osbNewSKU.AppendLine("")

    '            Dim strSplitDate As String = ""

    '            While myReader.Peek <> -1
    '                iTotal_Detail += 1
    '                iCurrent_Line_Number += 1
    '                Current_Text_Line = myReader.ReadLine
    '                strSplit = ReadAndSplit(Current_Text_Line, strDelimited)

    '                If strSplit(0).ToString = "ZZZ" Then
    '                    Continue While
    '                End If

    '                If strSalesOrder_No <> strSplit(0).ToString Then
    '                    '*********** BEGIN HEADER ********************
    '                    Dim obl_Import_HDO As New bl_Import_SO
    '                    If obl_Import_HDO.CheckSalesOrder_No(strSplit(0)) Then
    '                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        iCount_Error_Header += 1

    '                        'Cancel
    '                        If strSplit(15).Trim <> "1" Then
    '                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                            Dim oSo As New tb_SalesOrder
    '                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                            oSo.SalesOrder_Index = strArray(0)
    '                            oSo.SalesOrder_No = strSplit(0).ToString
    '                            oSaleorder.Cancel_SO(oSo)
    '                        End If

    '                        Continue While
    '                    Else 'ถ้าไม่ใช่ให้เก็บค่า DO_No ไว้ใน strSaleOrderNo
    '                        strSalesOrder_No = strSplit(0).Trim
    '                        'Cancel
    '                        If strSplit(15).Trim <> "1" Then
    '                            Dim oSaleorder As New SOTransaction(SOTransaction.enuOperation_Type.CANCEL)
    '                            Dim oSo As New tb_SalesOrder
    '                            strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSplit(0).ToString)
    '                            oSo.SalesOrder_Index = strArray(0)
    '                            oSo.SalesOrder_No = strSplit(0).ToString
    '                            oSaleorder.Cancel_SO(oSo)
    '                            Continue While
    '                        End If
    '                    End If
    '                    obl_Import_HDO.InvoiceNo = strSplit(0).Trim
    '                    obl_Import_HDO.Str2 = strSplit(3).Trim
    '                    obl_Import_HDO.DoNo = strSplit(0).Trim
    '                    strSplitDate = strSplit(4).Trim 'For Split Date Sure DD MM YYYY
    '                    obl_Import_HDO.DoDate = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))

    '                    strSplitDate = strSplit(13).Trim 'For Split Date Sure DD MM YYYY
    '                    obl_Import_HDO.Expected_Delivery_Date = ConvertToDate(strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4))
    '                    strCustomerShipping_Index = obl_Import_HDO.GetCustomerShipping_Index(strSplit(5).Trim, strSplit(6).Trim, "", _
    '                                                                   strSplit(7).Trim, strSplit(8).Trim)
    '                    obl_Import_HDO.Str8 = strSplit(9).Trim
    '                    obl_Import_HDO.CustomerShippingNo = strSplit(5).Trim
    '                    obl_Import_HDO.CustomerShippingIndex = strCustomerShipping_Index
    '                    obl_Import_HDO.PostCode = strSplit(8).Trim
    '                    obl_Import_HDO.Remark = strSplit(14).Trim
    '                    obl_Import_HDO.Currency = Me._DEFAULT_CURRENCY_INDEX
    '                    If strSplit(2).Trim = "1" Then
    '                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_NORMAL
    '                    Else
    '                        obl_Import_HDO.DocumentTypeIndex = Me._DEFAULT_SALE_RETURN
    '                    End If
    '                    obl_Import_HDO.SalesOrderInsert()
    '                    iCount_Comp_Header += 1
    '                    '*********** END HEADER ********************
    '                End If

    '                '*********** BEGIN ITEM ********************

    '                obl_Import_SO_Item.RefNo1 = strSalesOrder_No
    '                strArray = obl_Import_SO_Item.GetSalesOrder_Index(strSalesOrder_No)
    '                obl_Import_SO_Item.SaleOrderIndex = strArray(0)
    '                obl_Import_SO_Item.Currency = strArray(1)

    '                'ถ้าไม่มี SalesOrder_Index ให้ข้ามไปอ่านบรรทัดถัดไป
    '                If obl_Import_SO_Item.SaleOrderIndex = "" Then
    '                    'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    _osbErr_Data_Incomplete.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                    iCount_Error_Detail += 1

    '                    'อ่านบรรทัดใหม่
    '                    Continue While

    '                Else

    '                    'Check Duplicate data in Sales Order Item
    '                    If obl_Import_SO_Item.CheckDup_SO_Item(obl_Import_SO_Item.SaleOrderIndex, strSplit(2).Trim, strSplit(1).Trim) Then
    '                        'osb.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)
    '                        _osbErr_Dup.AppendLine(iCurrent_Line_Number & " : " & Current_Text_Line)

    '                        iCount_Error_Detail += 1

    '                        'อ่านบรรทัดใหม่
    '                        Continue While
    '                    End If

    '                    Dim objSy_AutoNumber As New Sy_AutoNumber
    '                    obl_Import_SO_Item.SalesOrderItemIndex = objSy_AutoNumber.getSys_Value("SalesOrderItem_Index")
    '                    objSy_AutoNumber = Nothing
    '                    obl_Import_SO_Item.ItemSeq = CInt(strSplit(1).Trim)
    '                    obl_Import_SO_Item.Qty = CDbl(strSplit(11).Trim)
    '                    obl_Import_SO_Item.TotalQty = obl_Import_SO_Item.Qty
    '                    obl_Import_SO_Item.Volume = 0 '
    '                    obl_Import_SO_Item.Str2 = "" ' strSplit(6).Trim
    '                    obl_Import_SO_Item.Remark = strSplit(14).Trim

    '                    '*** BEGIN SKU ***
    '                    obl_Import_SO_Item.SkuId = strSplit(10).Trim
    '                    obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.GetSKU_Index(obl_Import_SO_Item.SkuId)
    '                    'Insert New Sku and Package
    '                    If obl_Import_SO_Item.SkuIndex = "" Then
    '                        obl_Import_SO_Item.Package_ID = strSplit(12).Trim
    '                        obl_Import_SO_Item.SkuIndex = obl_Import_SO_Item.InsertSKU()
    '                        obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, "")  'strArray(1)
    '                        _osbNewSKU.AppendLine("SKU : " & obl_Import_SO_Item.SkuId)
    '                    End If

    '                    obl_Import_SO_Item.PackageIndex = obl_Import_SO_Item.GetPackage_Index(obl_Import_SO_Item.SkuIndex, strSplit(12).Trim)  'strArray(1)

    '                    If obl_Import_SO_Item.PackageIndex = "" Then
    '                        ' Auto Insert Package were Package and Update Package For Sku This
    '                        obl_Import_SO_Item.PackageIndex = SavePackage(strSplit(12).Trim)
    '                        Me.SaveSKURatio(obl_Import_SO_Item.SkuIndex, obl_Import_SO_Item.PackageIndex, 1) 'Default Ratio=1 ?
    '                    End If
    '                    'Get Package_Index with Package_Id



    '                    '*** END SKU ***

    '                    'Set property and Insert
    '                    obl_Import_SO_Item.SalesOrder_ItemInsert()

    '                    iCount_Comp_Detail += 1
    '                End If

    '                '*********** END ITEM ********************
    '            End While

    '            myReader.Close()

    '            strDestination = MoveFile(pstrFilePath)

    '            'Write Log
    '            With obl_Log
    '                'Move file
    '                .Write_To_Path = strDestination & "\" & pstrFilePath.ToUpper.Replace(Me._DEFAULT_IMPORT_SO_PATH.ToUpper, "") & ".log"
    '                .Process_Name = "Import"
    '                .Module_Name = "DO"
    '                .Start_Process = Now.ToString("dd/MM/yyyy HH:mm:ss")

    '                .Target = pstrFilePath
    '                .Destination = strDestination

    '                .Total_Header = iTotal_Header
    '                .Total_Detail = iTotal_Detail

    '                .Complete_Header = iCount_Comp_Header
    '                .Complete_Detail = iCount_Comp_Detail

    '                .Incomplete_Header = iCount_Error_Header
    '                .Incomplete_Detail = iCount_Error_Detail

    '                .Error_List = New StringBuilder

    '                .Error_List.AppendLine(_osbErr_Dup.ToString)
    '                .Error_List.AppendLine(_osbErr_Data_Incomplete.ToString)
    '                .Error_List.AppendLine(_osbNewSKU.ToString)

    '                .Write_Log()
    '            End With

    '        Catch ex As Exception
    '            obl_Import_SO_Item.UpdateSalesOrder_Cancel_From_Error_Interface(strSalesOrder_No)
    '            Throw ex
    '        End Try

    '    End Sub

    '    Sub SaveSKURatio(ByVal pSku_Index As String, ByVal pPackage_Index As String, ByVal ratio As Double)
    '        Try

    '            Dim objDBSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.ADDNEW)
    '            objDBSKURatio.SaveData("", pSku_Index, pPackage_Index, CDbl(ratio))

    '        Catch ex As Exception
    '            MessageBox.Show(ex.Message)
    '        End Try
    '    End Sub

    '    Function SavePackage(ByVal ppackage_Id As String) As String

    '        Try
    '            Dim Package_Index As String = ""
    '            Dim package_Id As String = ppackage_Id
    '            Dim description As String = ppackage_Id
    '            Dim dimension_Hi As Double = 0.0
    '            Dim dimension_Wd As Double = 0.0
    '            Dim dimension_Len As Double = 0.0
    '            Dim Weight As Double = 0.0

    '            Dim objms_Package As New ms_Package(ms_Package.enuOperation_Type.ADDNEW)
    '            Package_Index = objms_Package.SaveData("", package_Id, description, dimension_Hi, dimension_Wd, dimension_Len, 0, Weight, 0) ', txtUnit_id.Text

    '            Return Package_Index

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Function MoveFile(ByVal pstrSourceFile As String) As String
    '        Try

    '            Dim ImportFolder As String = Me._DEFAULT_IMPORT_SO_PATH


    '            Dim strResultFileName As String
    '            Dim filemove As String

    '            If Directory.Exists(ImportFolder & SubFolder) = False Then
    '                Directory.CreateDirectory(ImportFolder & SubFolder)
    '            End If

    '            If pstrSourceFile = "" Then
    '                Return ""
    '                'Continue For
    '            End If


    '            'For i As Integer = 0 To pstrSourceFile.Length - 1
    '            strResultFileName = Path.GetFileName(pstrSourceFile)

    '            filemove = ImportFolder & SubFolder & "\" & strResultFileName
    '            File.Move(pstrSourceFile, filemove)
    '            'Next

    '            Return ImportFolder & SubFolder

    '        Catch ex As Exception
    '            Throw ex
    '        End Try
    '    End Function

    '    Public Sub ShowError(ByVal pstrErrorList As ArrayList, ByVal pintCountSucc As Integer)

    '        Try

    '            Dim strResult As String
    '            Dim strError As String = ""
    '            For Each strResult In pstrErrorList
    '                strError &= strResult & vbNewLine
    '            Next
    '            If strError = "" Then
    '                strError = "Not found."
    '            End If

    '            MsgBox("ระบบทำการ Import ข้อมูลสำเร็จ " & vbNewLine _
    '            & "Total : " & pintCountSucc & " record." & vbNewLine & vbNewLine _
    '            & "----------------------------------------------Error list------------------------------------------" & vbNewLine _
    '            & strError & vbNewLine _
    '            & "Total : " & pstrErrorList.Count & " record.")

    '        Catch ex As Exception
    '            Throw ex
    '        End Try

    '    End Sub

    '    Public Function ConvertToDate(ByVal pstrValue As String) As Date

    '        Try
    '            'strSplitDate.Substring(6, 2) & strSplitDate.Substring(4, 2) & strSplitDate.Substring(0, 4)
    '            pstrValue = pstrValue & "0"
    '            Dim strDate As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 1, 1), Mid(pstrValue, 1, 2))
    '            Dim strMonth As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 2, 2), Mid(pstrValue, 3, 2))
    '            Dim strYear As String = IIf(pstrValue.Length = 8, Mid(pstrValue, 4, 4), Mid(pstrValue, 5, 4))

    '            Return CDate(strYear & "/" & strMonth & "/" & strDate)

    '        Catch ex As Exception

    '            Throw ex

    '        End Try

    '    End Function
    '#End Region

    'Old Code
    'Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
    '    Try
    '        'checking 
    '        If Me.txtCustomer_Id.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Customer")
    '            Exit Sub
    '        End If

    '        If Me.txtWorkSheet.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Work Sheet")
    '            Exit Sub
    '        End If
    '        Dim oImport_SO As New bl_Import_SO
    '        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
    '            Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

    '            'load data from excel

    '            Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
    '        Else
    '            Exit Sub
    '        End If

    '        With oImport_SO
    '            .DataSource = Me.grdPreviewData.DataSource
    '            '.Customer_Index = Me.cboCustomer.SelectedValue
    '            '.DocumentType_Index = Me.cboDocumentType.SelectedValue
    '            '.ItemStatus_Index = Me.cboItemStatus.SelectedValue


    '            'If .CheckingData Then

    '            '    _boolResult = True
    '            'Else
    '            '    _boolResult = False
    '            'End If
    '        End With
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try

    'End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
        Try
            'checking 
            If Me.txtCustomer_Id.Text.Trim = "" Then
                W_MSG_Information("กรุณาระบุ Customer")
                Exit Sub
            End If

            If rd_Timco.Checked = False And rd_Customer.Checked = False And rd_None.Checked = False Then
                W_MSG_Information("กรุณาระบุ " & Label4.Text)
                Exit Sub
            End If

            'If Me.txtWorkSheet.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาระบุ Work Sheet")
            '    Exit Sub
            'End If

            'If Me.cbProductGroup.SelectedValue Is Nothing Then
            '    W_MSG_Information("กรุณาระบุ " & Label25.Text)
            '    Me.cbProductGroup.Focus()
            '    Exit Sub
            'End If
            Select Case objStatus
                Case enuOperation_Type.SO
                    Dim oImport_SO As New bl_Import_SO
                    If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                        Dim objWS As DataTable = New DataTable

                        objWS = oImport_SO.LoadWorkSheet(Me.txtFilePath.Text)

                        With cboWorkSheet
                            .DataSource = objWS
                            .DisplayMember = "TABLE_NAME"
                            .ValueMember = "TABLE_NAME"
                        End With
                        cboWorkSheet.SelectedIndex = 0
                        LoadData()
                        '====================
                    Else
                        Exit Sub
                    End If
                Case enuOperation_Type.SO_CN2
                    'Dim oImport_SO_CN2 As New bl_Import_SO_CN2
                    'If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    '    Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim
                    '    'Me.txtFilePath.Tag = OpenFileDialog1.SafeFileName.Trim
                    '    Dim objWS As DataTable = New DataTable

                    '    objWS = oImport_SO_CN2.LoadWorkSheet(Me.txtFilePath.Text)

                    '    With cboWorkSheet
                    '        .DataSource = objWS
                    '        .DisplayMember = "TABLE_NAME"
                    '        .ValueMember = "TABLE_NAME"
                    '    End With
                    '    cboWorkSheet.SelectedIndex = 0
                    '    LoadData()
                    '    '====================
                    'Else
                    '    Exit Sub
                    'End If
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Sub frmImport_SO_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.KeyCode = Keys.Escape Then
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer.Click
        Try
            Select Case objStatus
                Case enuOperation_Type.SO_CN2
                    If txtConsignee_Id.Text.Trim <> "" Then
                        W_MSG_Error_ByIndex("500021")
                        Exit Sub
                    End If

            End Select


            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value **** มาแสดงในตัวแปล 
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Index = frm.Customer_Index

            If tmpCustomer_Index = "" Then
                Exit Sub
            End If
            If Not tmpCustomer_Index = "" Then
                Me._Customer_Index = tmpCustomer_Index
                Me.getCustomer()
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                'Me.txtCustomer_Name.Text = ""
            End If
            ' *********************
            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboWorkSheet_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboWorkSheet.SelectedIndexChanged
        grdPreviewData.DataSource = Nothing
    End Sub

    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged
        Try
            Me._DocumentType_Index = cboDocumentType.SelectedValue.ToString
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub cbProductGroup_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        Me._EPSON_Location_Index = cbProductGroup.SelectedValue.ToString
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    Private Sub LoadData()
        'checking
        If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
            W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
            Exit Sub
        End If

        Select Case objStatus
            Case enuOperation_Type.SO
                Dim oImport_SO As New bl_Import_SO

                '===============
                Me.grdPreviewData.DataSource = oImport_SO.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

                If Me.grdPreviewData.RowCount = 0 Then
                    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                    Exit Sub
                End If

                '===============

                oImport_SO.DataSource = grdPreviewData.DataSource
            Case enuOperation_Type.SO_CN2
                'Dim oImport_SO_CN2 As New bl_Import_SO_CN2
                ''===============
                'Me.grdPreviewData.DataSource = oImport_SO_CN2.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

                'If Me.grdPreviewData.RowCount = 0 Then
                '    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                '    Exit Sub
                'End If

                ''===============
                'oImport_SO_CN2.DataSource = grdPreviewData.DataSource
        End Select


    End Sub

    Private Sub BntLoaddata_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntLoaddata.Click
        LoadData()
    End Sub

    Private Sub txtFilePath_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePath.DoubleClick
        Try
            Select Case objStatus
                Case enuOperation_Type.SO
                    Dim AppPath As String
                    AppPath = Application.StartupPath
                    Dim ps As New ProcessStartInfo()
                    With ps
                        'Dim strApppath As String = ""
                        'strApppath = AppPath.Replace("\bin", "")
                        .FileName = AppPath + "\Import\Format\SO_IMPORT_TIMCO.xls"
                        'D:\Project\WMS_Site\WMS_Site_TIMCO\WMS_Site_TIMCO\Import\Format\SO_IMPORT_TIMCO.xls
                        .UseShellExecute = True
                    End With

                    Dim p As New Process
                    p.StartInfo = ps
                    p.Start()
                Case enuOperation_Type.SO_CN2
                    Dim AppPath As String
                    AppPath = Application.StartupPath
                    Dim ps As New ProcessStartInfo()
                    With ps
                        'Dim strApppath As String = ""
                        'strApppath = AppPath.Replace("\bin", "")
                        .FileName = AppPath + "\Import\Format\SO_IMPORT_TIMCO_CN2_Delivery.xls"
                        'D:\Project\WMS_Site\WMS_Site_TIMCO\WMS_Site_TIMCO\Import\Format\SO_IMPORT_TIMCO.xls
                        .UseShellExecute = True
                    End With

                    Dim p As New Process
                    p.StartInfo = ps
                    p.Start()
            End Select
           
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub btnConsignee_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConsignee.Click
        Try
            Dim oConfig As New config_CustomSetting
            If oConfig.getConfig_Key_USE("USE_CUSTOMER_CUSTOMERSHIPPING") Then
                If Me._Customer_Index = "" Or Me._Customer_Index Is Nothing Then
                    W_MSG_Information_ByIndex(8)
                    Exit Sub
                End If

            End If

            Dim frm As New frmConsignee_Popup
            frm.Customer_Index = Me._Customer_Index
            frm.ShowDialog()

            Dim tmp_Index As String = ""
            tmp_Index = frm.Consignee_Index

            If Not tmp_Index = "" Then
                Me.txtConsignee_Id.Tag = tmp_Index
                txtConsignee_Id.Text = frm.Consignee_ID
                txtConsignee_Name.Text = frm.Consignee_Name


            Else
                Me.txtConsignee_Id.Tag = ""
                txtConsignee_Id.Text = ""
                txtConsignee_Name.Text = ""

            End If

            frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class