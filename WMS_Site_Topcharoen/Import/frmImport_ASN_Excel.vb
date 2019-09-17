Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Language
Imports WMS_STD_INB_ASN_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_Master

Public Class frmImport_ASN_Excel
#Region "Operation Type "
    Private objStatus As enuOperation_Type
    Public Enum enuOperation_Type
        ASN
        ASN_CN2
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


#Region "   Property  "

    Dim CurrentLine As String
    Dim strValue() As String
    Private _isItem_Package As Integer = 0
    Private _Package_Index As String
    Private _ProductType_Index As String = ""
    Private _Sku_Index As String
    Private _Product_Index As String
    Private _DEFAULT_IMASNRT_PATH As String
    Private _DEFAULT_DocumentType As String
    Private _FileName As String

    Private ProductTypt_Index As String = ""
    Public _boolResult As Boolean = False
    Private _Item_Package_Index As String = ""
    Private _Customer_Index As String = ""
    Private _DocumentType_Index As String = ""
    Private _EPSON_Location_Index As String = ""
    Private _DocumentItem_Status_Index As String = ""
    Private _dtDataSource As New DataTable
    Private _ImportComplete As Boolean = True
    Private _strAdvanceShipNotice_Index As String = ""

    Property strAdvanceShipNotice_Index() As String
        Get
            Return _strAdvanceShipNotice_Index
        End Get
        Set(ByVal value As String)
            _strAdvanceShipNotice_Index = value
        End Set
    End Property


    Property ImportComplete() As Boolean
        Get
            Return _ImportComplete
        End Get
        Set(ByVal value As Boolean)
            _ImportComplete = value
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

    Property DocumentItem_Status_Index() As String
        Get
            Return _DocumentItem_Status_Index
        End Get
        Set(ByVal value As String)
            _DocumentItem_Status_Index = value
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

    Public Property Sku_Index() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal value As String)
            _Sku_Index = value
        End Set
    End Property
    Public Property ProductType_Index() As String
        Get
            Return _ProductType_Index
        End Get
        Set(ByVal value As String)
            _ProductType_Index = value
        End Set
    End Property

    Public ReadOnly Property Package_Index() As String
        Get
            Return _Package_Index
        End Get
    End Property

    Public Property Product_Index() As String
        Get
            Return _Product_Index
        End Get
        Set(ByVal value As String)
            _Product_Index = value
        End Set
    End Property
    Public Property Item_Package_Index() As String
        Get
            Return _Item_Package_Index
        End Get
        Set(ByVal value As String)
            _Item_Package_Index = value
        End Set
    End Property

    Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

#End Region

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try

            'checking
            'If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text.Trim = "" Then
            '    W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
            '    Exit Sub
            'End If

            'If Me.grdPreviewData.RowCount = 0 Then
            '    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
            '    Exit Sub
            'End If

            If Me.grdPreviewData.RowCount = 0 Then
                W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                Exit Sub
            End If
            Select Case objStatus
                Case enuOperation_Type.ASN
                    ''===============
                    Dim oImport_ASN As New Import_ASN
                    ''===============
                    'Me.grdPreviewData.DataSource = oImport_ASN.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

                    'If Me.grdPreviewData.RowCount = 0 Then
                    '    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                    '    Exit Sub
                    'End If
                    ''===============

                    With oImport_ASN
                        .DataSource = Me.grdPreviewData.DataSource

                        .Customer_Index = Me.Customer_Index
                        .DocumentType_Index = Me.DocumentType_Index
                        .EPSON_Location_Index = Me.EPSON_Location_Index
                        .DocumentItem_Status_Index = Me.DocumentItem_Status_Index

                        ''เพิ่มการบันทึกการใช้รถของtimco
                        'If Me.rd_Timco.Checked = True Then
                        '    .Use_car = 1
                        'ElseIf Me.rd_Customer.Checked = True Then
                        '    .Use_car = 2
                        'ElseIf Me.rd_None.Checked = True Then
                        '    .Use_car = 3
                        'End If

                        'CheckingData()
                        If Check_Data() = False Then Exit Sub
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
                            Me._dtDataSource = CType(Me.grdPreviewData.DataSource, DataTable)
                            Me.ImportComplete = False
                            Me.StartImportASNExcel()
                            If Me.ImportComplete = True Then
                                MsgBox("Import Data Completed.")
                                Me.ImportComplete = False
                            Else
                                MsgBox("ยกเลิก Receiving Doc. จากการ Interface ไม่สำเร็จ")
                                Me.ImportComplete = False
                            End If
                        Else
                            MsgBox("Please Check Data.")
                            Me.ImportComplete = False
                        End If
                    End With
                Case enuOperation_Type.ASN_CN2

                    'If txtCustomerShippingLocation_ID.Text = "" Or txtCustomerShippingLocation_ID.Tag = "" Then
                    '    W_MSG_Information("กรุณาระบุ " & lblChstomerShippingLocation.Text)
                    '    txtCustomerShippingLocation_ID.Focus()
                    '    Exit Sub
                    'End If

                    ' ''===============
                    'Dim oImport_ASN_CN2 As New Import_ASN_CN2
                    ' ''===============
                    ''Me.grdPreviewData.DataSource = oImport_ASN.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

                    ''If Me.grdPreviewData.RowCount = 0 Then
                    ''    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                    ''    Exit Sub
                    ''End If
                    ' ''===============

                    'With oImport_ASN_CN2
                    '    .DataSource = Me.grdPreviewData.DataSource

                    '    .Customer_Index = Me.Customer_Index
                    '    .DocumentType_Index = Me.DocumentType_Index
                    '    .EPSON_Location_Index = Me.EPSON_Location_Index
                    '    .DocumentItem_Status_Index = Me.DocumentItem_Status_Index

                    '    If Check_Data() = False Then Exit Sub
                    '    Dim i As Integer
                    '    Dim All_OK As Boolean = True
                    '    For i = 0 To grdPreviewData.Rows.Count - 1
                    '        With grdPreviewData
                    '            If .Rows(i).Cells("Check_Result").Value() <> "OK" Then
                    '                All_OK = False
                    '                Exit For
                    '            End If
                    '        End With
                    '    Next
                    '    '==============
                    '    If All_OK = True Then
                    '        Me._dtDataSource = CType(Me.grdPreviewData.DataSource, DataTable)
                    '        Me.ImportComplete = False
                    '        Me.StartImportASNExcel_CN2()
                    '        If Me.ImportComplete = True Then
                    '            MsgBox("Import Data Completed.")
                    '            Me.ImportComplete = False
                    '        Else
                    '            MsgBox("ยกเลิก Receiving Doc. จากการ Interface ไม่สำเร็จ")
                    '            Me.ImportComplete = False
                    '        End If
                    '    Else
                    '        MsgBox("Please Check Data.")
                    '        Me.ImportComplete = False
                    '    End If
                    'End With
            End Select
         
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub
    Private Sub StartImportASNExcel()
        Dim countAll As Integer = 0

        Try
            Dim objHeader As New tb_AdvanceShipNotice
            Dim objItem As New tb_AdvanceShipNoticeItem
            Dim objItemCollection As New List(Of tb_AdvanceShipNoticeItem)
            Dim objPalletType As New WMS_STD_Master_Datalayer.tb_PalletType_History
            Dim objPalletTypeCollection As New List(Of WMS_STD_Master_Datalayer.tb_PalletType_History)

            Dim ItemLife_Total_Day As Integer = 0

            Dim strSQL As String = ""

            Me.ImportComplete = False

            '-------------- Begin : Group Data For Primary Key
            Dim dtPrimaryKey As New DataTable
            dtPrimaryKey.Columns.Add("DocRef_No", GetType(String))

            For Each odrASN As DataRow In Me._dtDataSource.Rows
                Dim drArrRow() As DataRow = dtPrimaryKey.Select("DocRef_No='" & odrASN("DocRef_No").ToString & "'")
                If drArrRow.Length = 0 Then
                    Dim drNewRow As DataRow
                    drNewRow = dtPrimaryKey.NewRow
                    drNewRow("DocRef_No") = odrASN("DocRef_No")
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
                Dim drArrOrder() As DataRow = Me._dtDataSource.Select("isnull(DocRef_No,'') ='" & drPrimarykey("DocRef_No").ToString & "'")
                'If drArrOrder.Length = 0 Then Continue For
                Dim iRow As Integer = 0

                '*************** New Order *************** 
                objItemCollection = New List(Of tb_AdvanceShipNoticeItem)
                objHeader = New tb_AdvanceShipNotice
                objPalletType = New WMS_STD_Master_Datalayer.tb_PalletType_History
                objPalletTypeCollection = New List(Of WMS_STD_Master_Datalayer.tb_PalletType_History)

                For Each odrASNItem As DataRow In drArrOrder
                    'Conut Progress
                    countAll += 1
                    'Header
                    intSeq += 1 'count item

                    If iRow = 0 Then
                        intSeq = 1
                        iRow += 1
                        ' *********** Define Value for Header ***********
                        ' Gen Index ต้องใช้ objSys_MaxID เพราะใน Function Save มันมีการหา Index จริงตลอด
                        objHeader.AdvanceShipNotice_No = ""
                        objHeader.AdvanceShipNotice_Index = objSys_MaxID.getSys_Value("AdvanceShipNotice_Index")
                        Me._strAdvanceShipNotice_Index = objHeader.AdvanceShipNotice_Index
                        objHeader.AdvanceShipNotice_Date = CDate(odrASNItem("Doc_Date").ToString).ToString("yyyy/MM/dd")
                        objHeader.DocumentType_Index = Me.DocumentType_Index
                        objHeader.Customer_Index = Me.Customer_Index
                        objHeader.Carrier_Index = ""
                        objHeader.Department_Index = ""
                        objHeader.Customer_Shipping_Index = odrASNItem("Source_No").ToString
                        Dim oasn As New Import_ASN
                        objHeader.Customer_Shipping_Location_Index = oasn.GetSOURCE_Index(odrASNItem("Source_No").ToString.Trim, Me.Customer_Index).Trim.ToString
                        objHeader.Expected_Delivery_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.FullPaid_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.Departure_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.Arrival_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.update_date = Format(Now, "yyyy/MM/dd")
                        objHeader.Bond_Declaration_Date = objHeader.AdvanceShipNotice_Date

                        'เพิ่มการบันทึกการใช้รถของtimco
                        If Me.rd_Timco.Checked = True Then
                            objHeader.Use_car = 1
                        ElseIf Me.rd_Customer.Checked = True Then
                            objHeader.Use_car = 2

                        ElseIf Me.rd_None.Checked = True Then
                            objHeader.Use_car = 3
                        End If

                        objHeader.Supplier_Index = ""
                        objHeader.Currency_Index = "0000000000002"
                        objHeader.PaymentMethod_Index = ""
                        objHeader.Payment_Ref = ""
                        objHeader.Str1 = odrASNItem("Cust_PO_No").ToString
                        objHeader.Str2 = "" ' (Receiving INV.No.)  ไว้ที่ Detail
                        objHeader.Str3 = ""
                        objHeader.Str4 = ""
                        objHeader.Str5 = ""
                        objHeader.Str6 = ""
                        objHeader.Str7 = ""
                        objHeader.Str9 = ""
                        objHeader.add_by = WV_UserName
                        objHeader.Remark = "Import data [ " & odrASNItem("DocRef_No").ToString & "]"
                        'objHeader.Epson_ProductGroup_Index = Me.EPSON_Location_Index
                        objHeader.Vehicle_No = ""
                        objHeader.Trip_Qty = 1
                        'objHeader.EDI_Gen_XP_Req = 0
                        'objHeader.EDI_Not_Gen_XP_Req = 0
                        objHeader.Str3 = odrASNItem("H_ContainerNo").ToString
                        objHeader.Str6 = odrASNItem("H_PermitNo").ToString

                        '' *************************************************
                    End If
                    'Detail
                    ' *** New Object *********
                    '=================== มาจากหน้า ASN
                    objItem = New tb_AdvanceShipNoticeItem

                    With objItem
                        .AdvanceShipNoticeItem_Index = objDBIndex.getSys_Value("AdvanceShipNoticeItem_Index")
                        .AdvanceShipNotice_Index = objHeader.AdvanceShipNotice_Index
                        Dim oasn As New Import_ASN
                        .Sku_Index = oasn.GetSKU_Index(odrASNItem("Product_No").ToString, Me.Customer_Index)
                        .Package_Index = oasn.GetPackage_Index_Import(.Sku_Index, Me.Customer_Index)  'strArray(1)
                        .Qty = odrASNItem("Qty")
                        '.EDI_Gen_XP_TotalQty = 0
                        .Received_Qty = 0
                        '=====================================================
                        Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                        Dim dblRatio As Double = 0
                        dblRatio = objRatio.getRatio(.Sku_Index, .Package_Index)
                        objRatio = Nothing
                        ' ****** Calculate Tatal Qty *** 
                        .Ratio = dblRatio
                        .Total_Qty = Val(.Qty) * Val(dblRatio)
                        ' ***************************
                        'Get Default From SKU
                        'Dim oasn As New Import_ASN
                        oasn.getSKU_Detail_Update(odrASNItem("Product_No").ToString, Me.Customer_Index)
                        dtTemp = oSku.DataTable
                        'W
                        odrASNItem("Tot_GWeight") = IIf(IsNumeric(odrASNItem("Tot_GWeight")), odrASNItem("Tot_GWeight"), 0)
                        If odrASNItem("Tot_GWeight") = 0 Then
                            .Weight = FormatNumber(Val(.Qty) * odrASNItem("Tot_GWeight"), 4)
                        Else
                            .Weight = FormatNumber(Val(odrASNItem("Tot_GWeight")), 4)
                        End If
                        '.NetWeight = .Weight
                        odrASNItem("TotNetWeight") = IIf(IsNumeric(odrASNItem("TotNetWeight")), odrASNItem("TotNetWeight"), 0)
                        If odrASNItem("TotNetWeight") = 0 Then
                            .Net_Weight = .Weight
                        Else
                            .Net_Weight = FormatNumber(Val(odrASNItem("TotNetWeight")), 4)
                        End If
                        .Flo3 = .Weight

                        odrASNItem("Unit_Volume") = IIf(IsNumeric(odrASNItem("Unit_Volume")), odrASNItem("Unit_Volume"), 0)

                        If odrASNItem("M3") > 0 Then
                            .Volume = FormatNumber(odrASNItem("M3"), 4)
                        Else
                            .Volume = FormatNumber(Val(.Qty) * odrASNItem("Unit_Volume"), 4)
                        End If

                        .UnitPrice = 0
                        .Flo1 = 0
                        .Flo2 = 0
                        .Amount = 0
                        .Total_Amt = 0
                        .Remark = odrASNItem("Remark").ToString.Trim
                        .PLot = ""
                        .Ref_No1 = odrASNItem("L_CustomerRef").ToString.Trim
                        .Ref_No2 = odrASNItem("L_Reference2").ToString.Trim
                        .Str1 = ""
                        .Str5 = ""
                        .Item_Seq = intSeq * 10 'Timco Val(odrASNItem("Line_No"))
                        .Str4 = ""
                        .Str7 = ""
                        .Str8 = odrASNItem("RCV_Inv_No").ToString
                        .PLot = odrASNItem("Lot").ToString
                    End With

                    objItemCollection.Add(objItem)
                Next
                'Save data per Order
                'Not Controls Pallet
                'Insert Order and OrderItem
                'Creat Order
                Dim objDB As New AdvanceShipNoticeTransaction(AdvanceShipNoticeTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection)
                objDB.SaveData()
                Me._strAdvanceShipNotice_Index = objDB.Asn_Index
                objDB = Nothing
                Me.ImportComplete = True
            Next

        Catch ex As Exception
            Dim objASNTransaction As New AdvanceShipNoticeTransaction(AdvanceShipNoticeTransaction.enuOperation_Type.UPDATE, Nothing, Nothing)
            objASNTransaction.Cancel(Me._strAdvanceShipNotice_Index)
            ''====================
            Me.ImportComplete = False
            Throw ex
        End Try
    End Sub
    Private Sub StartImportASNExcel_CN2()
        Dim countAll As Integer = 0
        Try
            Dim objHeader As New tb_AdvanceShipNotice
            Dim objItem As New tb_AdvanceShipNoticeItem
            Dim objItemCollection As New List(Of tb_AdvanceShipNoticeItem)
            Dim objPalletType As New WMS_STD_Master_Datalayer.tb_PalletType_History
            Dim objPalletTypeCollection As New List(Of WMS_STD_Master_Datalayer.tb_PalletType_History)

            Dim ItemLife_Total_Day As Integer = 0

            Dim strSQL As String = ""

            Me.ImportComplete = False

            '-------------- Begin : Group Data For Primary Key
            Dim dtPrimaryKey As New DataTable
            dtPrimaryKey.Columns.Add("DocRef_No", GetType(String))

            For Each odrASN As DataRow In Me._dtDataSource.Rows
                Dim drArrRow() As DataRow = dtPrimaryKey.Select("DocRef_No='" & "" & "'")
                If drArrRow.Length = 0 Then
                    Dim drNewRow As DataRow
                    drNewRow = dtPrimaryKey.NewRow
                    drNewRow("DocRef_No") = ""
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
                Dim drArrOrder() As DataRow = Me._dtDataSource.Select("isnull(DocRef_No,'') ='" & drPrimarykey("DocRef_No").ToString & "'")
                'If drArrOrder.Length = 0 Then Continue For
                Dim iRow As Integer = 0

                '*************** New Order *************** 
                objItemCollection = New List(Of tb_AdvanceShipNoticeItem)
                objHeader = New tb_AdvanceShipNotice
                objPalletType = New WMS_STD_Master_Datalayer.tb_PalletType_History
                objPalletTypeCollection = New List(Of WMS_STD_Master_Datalayer.tb_PalletType_History)

                For Each odrASNItem As DataRow In drArrOrder
                    'Conut Progress
                    countAll += 1
                    'Header
                    intSeq += 1 'count item

                    If iRow = 0 Then
                        intSeq = 1
                        iRow += 1
                        ' *********** Define Value for Header ***********
                        ' Gen Index ต้องใช้ objSys_MaxID เพราะใน Function Save มันมีการหา Index จริงตลอด
                        objHeader.AdvanceShipNotice_No = ""
                        objHeader.AdvanceShipNotice_Index = objSys_MaxID.getSys_Value("AdvanceShipNotice_Index")
                        Me._strAdvanceShipNotice_Index = objHeader.AdvanceShipNotice_Index
                        'objHeader.AdvanceShipNotice_Date = CDate(odrASNItem("Doc_Date").ToString).ToString("yyyy/MM/dd")
                        objHeader.AdvanceShipNotice_Date = CDate(Date.Now).ToString("yyyy/MM/dd")
                        objHeader.DocumentType_Index = Me.DocumentType_Index
                        objHeader.Customer_Index = Me.Customer_Index
                        objHeader.Carrier_Index = ""
                        objHeader.Department_Index = ""
                        objHeader.Customer_Shipping_Index = Me.txtCustomerShippingLocation_ID.Text 'odrASNItem("Source_No").ToString
                        objHeader.Customer_Shipping_Location_Index = Me.txtCustomerShippingLocation_ID.Tag.ToString 'objHeader.GetSOURCE_Index(odrASNItem("Source_No").ToString.Trim, Me.Customer_Index).Trim.ToString
                        objHeader.Expected_Delivery_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.FullPaid_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.Departure_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.Arrival_Date = objHeader.AdvanceShipNotice_Date
                        objHeader.update_date = Format(Now, "yyyy/MM/dd")
                        objHeader.Bond_Declaration_Date = objHeader.AdvanceShipNotice_Date

                        'เพิ่มการบันทึกการใช้รถของtimco
                        If Me.rd_Timco.Checked = True Then
                            objHeader.Use_car = 1
                        ElseIf Me.rd_Customer.Checked = True Then
                            objHeader.Use_car = 2

                        ElseIf Me.rd_None.Checked = True Then
                            objHeader.Use_car = 3
                        End If

                        objHeader.Supplier_Index = ""
                        objHeader.Currency_Index = "0000000000002" '<-?
                        objHeader.PaymentMethod_Index = ""
                        objHeader.Payment_Ref = ""
                        'objHeader.Str1 = odrASNItem("Cust_PO_No").ToString  '<-?
                        objHeader.Str1 = ""
                        objHeader.Str2 = "" ' (Receiving INV.No.)  ไว้ที่ Detail
                        objHeader.Str3 = ""
                        objHeader.Str4 = ""
                        objHeader.Str5 = ""
                        objHeader.Str6 = ""
                        objHeader.Str7 = ""
                        objHeader.Str9 = ""
                        objHeader.add_by = WV_UserName

                        Dim filename2 As String = ""
                        Dim filename1() As String
                        If Me.txtFilePath.Text.Trim <> "" Then
                            filename1 = Me.txtFilePath.Text.Split("\")
                            filename2 = filename1(filename1.Length - 1)
                        End If


                        objHeader.Remark = filename2
                        'objHeader.Epson_ProductGroup_Index = Me.EPSON_Location_Index
                        objHeader.Vehicle_No = ""
                        objHeader.Trip_Qty = 1
                        'objHeader.EDI_Gen_XP_Req = 0
                        'objHeader.EDI_Not_Gen_XP_Req = 0
                        'objHeader.Str3 = odrASNItem("H_ContainerNo").ToString
                        'objHeader.Str6 = odrASNItem("H_PermitNo").ToString
                        objHeader.Str3 = ""
                        objHeader.Str6 = ""
                        '' *************************************************
                    End If
                    'Detail
                    ' *** New Object *********
                    '=================== มาจากหน้า ASN
                    objItem = New tb_AdvanceShipNoticeItem

                    With objItem
                        Dim oasn As New Import_ASN
                        .AdvanceShipNoticeItem_Index = objDBIndex.getSys_Value("AdvanceShipNoticeItem_Index")
                        .AdvanceShipNotice_Index = objHeader.AdvanceShipNotice_Index
                        .Sku_Index = oasn.GetSKU_Index(odrASNItem("Product_No").ToString, Me.Customer_Index)
                        .Package_Index = oasn.GetPackage_Index_Import(.Sku_Index, Me.Customer_Index)  'strArray(1)
                        .Qty = odrASNItem("Qty")
                        '.EDI_Gen_XP_TotalQty = 0
                        .Received_Qty = 0
                        '=====================================================
                        Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                        Dim dblRatio As Double = 0
                        dblRatio = objRatio.getRatio(.Sku_Index, .Package_Index)
                        objRatio = Nothing
                        ' ****** Calculate Tatal Qty *** 
                        .Ratio = dblRatio
                        .Total_Qty = Val(.Qty) * Val(dblRatio)
                        ' ***************************
                        'Get Default From SKU
                        'Dim oasn As New Import_ASN
                        oasn.getSKU_Detail_Update(odrASNItem("Product_No").ToString, Me.Customer_Index)
                        dtTemp = oSku.DataTable
                        'W
                        odrASNItem("Tot_GWeight") = IIf(IsNumeric(odrASNItem("Tot_GWeight")), odrASNItem("Tot_GWeight"), 0)
                        If odrASNItem("Tot_GWeight") = 0 Then
                            .Weight = FormatNumber(Val(.Qty) * odrASNItem("Tot_GWeight"), 4)
                        Else
                            .Weight = FormatNumber(Val(odrASNItem("Tot_GWeight")), 4)
                        End If
                        '.NetWeight = .Weight
                        odrASNItem("TotNetWeight") = IIf(IsNumeric(odrASNItem("TotNetWeight")), odrASNItem("TotNetWeight"), 0)
                        If odrASNItem("TotNetWeight") = 0 Then
                            .Net_Weight = .Weight
                        Else
                            .Net_Weight = FormatNumber(Val(odrASNItem("TotNetWeight")), 4)
                        End If
                        .Flo3 = .Weight
                        If odrASNItem("M3") > 0 Then
                            .Volume = FormatNumber(odrASNItem("M3"), 4)
                        Else
                            .Volume = FormatNumber(Val(.Qty) * odrASNItem("TotNetWeight"), 4)
                        End If

                        .UnitPrice = 0
                        .Flo1 = 0
                        .Flo2 = 0
                        .Amount = 0
                        .Total_Amt = 0
                        .Remark = odrASNItem("Line_Remarks").ToString.Trim
                        .PLot = ""
                        '.Ref_No1 = odrASNItem("L_CustomerRef").ToString.Trim
                        '.Ref_No2 = odrASNItem("L_Reference2").ToString.Trim
                        .Ref_No1 = ""
                        .Ref_No2 = ""
                        .Str1 = ""
                        .Str5 = ""
                        .Item_Seq = intSeq * 10 'Timco Val(odrASNItem("Line_No"))
                        .Str4 = ""
                        .Str7 = ""
                        '.Str8 = odrASNItem("RCV_Inv_No").ToString
                        .Str8 = ""
                        .PLot = odrASNItem("Lot").ToString
                    End With

                    objItemCollection.Add(objItem)
                Next
                'Save data per Order
                'Not Controls Pallet
                'Insert Order and OrderItem
                'Creat Order
                Dim objDB As New AdvanceShipNoticeTransaction(AdvanceShipNoticeTransaction.enuOperation_Type.ADDNEW, objHeader, objItemCollection)
                objDB.SaveData()
                Me._strAdvanceShipNotice_Index = objDB.Asn_Index
                objDB = Nothing
                Me.ImportComplete = True
            Next

        Catch ex As Exception
            Dim objASNTransaction As New AdvanceShipNoticeTransaction(AdvanceShipNoticeTransaction.enuOperation_Type.UPDATE, Nothing, Nothing)
            objASNTransaction.Cancel(Me._strAdvanceShipNotice_Index)
            ''====================
            Me.ImportComplete = False
            Throw ex
        End Try
    End Sub
    Private Function Check_Data() As Boolean
        Try



            Check_Data = True
            Select Case objStatus
                Case enuOperation_Type.ASN
                    Dim oImport_ASN As New Import_ASN
                    Dim i As Integer
                    For i = 0 To grdPreviewData.Rows.Count - 1
                        With grdPreviewData
                            ' 1 = ใบรับสินค้า (Document No.)                    
                            If .Rows(i).Cells("DocRef_No").Value Is Nothing Then
                                .Rows(i).Cells("Check_Result").Value = "RCV.No. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("DocRef_No").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "RCV.No. ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                Dim objSo As New tb_SalesOrder
                                Dim dtSO As New DataTable

                                If oImport_ASN.IsExitData("tb_AdvanceShipNotice", "AdvanceShipNotice_No", .Rows(i).Cells("DocRef_No").Value.ToString) = True Then
                                    .Rows(i).Cells("Check_Result").Value = "พบ RCV. No.)"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If

                            End If

                            ' 2 = Source (Source)
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Source_No").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "SOURCE ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Source_No").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "SOURCE ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                If oImport_ASN.GetSOURCE_Index(.Rows(i).Cells("Source_No").Value.ToString, Me.Customer_Index).Trim.ToString = "" Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ SOURCE"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            ' 3 = RCV.Date
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Doc_Date").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "RCV.Date ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Doc_Date").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "RCV.Date ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                If IsDate(.Rows(i).Cells("Doc_Date").Value) = False Then
                                    .Rows(i).Cells("Check_Result").Value = "RCV.Date ผิดพลาด"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            ' 7 = SKU (Product number)
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Product_No").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Product_No").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                                Check_Data = False
                                Continue For
                            Else
                                If oImport_ASN.GetSKU_Index(.Rows(i).Cells("Product_No").Value.ToString, Me.Customer_Index).Trim.ToString = "" Then
                                    .Rows(i).Cells("Check_Result").Value = "ไม่พบ Product Code"
                                    Check_Data = False
                                    Continue For
                                Else
                                    .Rows(i).Cells("Check_Result").Value = "OK"
                                End If
                            End If

                            ' 8 = Qty
                            If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Qty").Value, GetType(String)).ToString = "") Then
                                .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf .Rows(i).Cells("Qty").Value.ToString = "" Then
                                .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                                Check_Data = False
                                Continue For
                            ElseIf IsNumeric(.Rows(i).Cells("Qty").Value) = False Then
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

                            ' 8 = TotNetWeight
                            If IsNumeric(.Rows(i).Cells("TotNetWeight").Value) = False Then
                                .Rows(i).Cells("TotNetWeight").Value = 0
                            End If

                            ' 9 = Tot_GWeight
                            If IsNumeric(.Rows(i).Cells("Tot_GWeight").Value) = False Then
                                .Rows(i).Cells("Tot_GWeight").Value = 0
                            End If
                            '10
                            If IsNumeric(.Rows(i).Cells("M3").Value) = False Then
                                .Rows(i).Cells("M3").Value = 0
                            End If
                            '10
                            If IsDBNull(.Rows(i).Cells("Lot").Value) Then
                                .Rows(i).Cells("Lot").Value = ""
                            End If

                        End With
                    Next
                    Exit Function
                Case enuOperation_Type.ASN_CN2
                    'Dim oImport_ASN_CN2 As New Import_ASN_CN2
                    'Dim i As Integer
                    'For i = 0 To grdPreviewData.Rows.Count - 1
                    '    With grdPreviewData
                    '        ' 7 = SKU (Product number)
                    '        If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Product_No").Value, GetType(String)).ToString = "") Then
                    '            .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        ElseIf .Rows(i).Cells("Product_No").Value.ToString = "" Then
                    '            .Rows(i).Cells("Check_Result").Value = "Product Code ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        Else
                    '            If oImport_ASN_CN2.GetSKU_Index(.Rows(i).Cells("Product_No").Value.ToString, Me.Customer_Index).Trim.ToString = "" Then
                    '                .Rows(i).Cells("Check_Result").Value = "ไม่พบ Product Code ของเจ้าของ : " & Me.txtCustomer_Id.Text
                    '                Check_Data = False
                    '                Continue For
                    '            Else
                    '                .Rows(i).Cells("Check_Result").Value = "OK"
                    '            End If
                    '        End If
                    '        ' 8 = Qty
                    '        If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(.Rows(i).Cells("Qty").Value, GetType(String)).ToString = "") Then
                    '            .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        ElseIf .Rows(i).Cells("Qty").Value.ToString = "" Then
                    '            .Rows(i).Cells("Check_Result").Value = "Qty ผิดพลาด"
                    '            Check_Data = False
                    '            Continue For
                    '        ElseIf IsNumeric(.Rows(i).Cells("Qty").Value) = False Then
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
                    '        ' 8 = TotNetWeight
                    '        If IsNumeric(.Rows(i).Cells("TotNetWeight").Value) = False Then
                    '            .Rows(i).Cells("TotNetWeight").Value = 0
                    '        End If

                    '        ' 9 = Tot_GWeight
                    '        If IsNumeric(.Rows(i).Cells("Tot_GWeight").Value) = False Then
                    '            .Rows(i).Cells("Tot_GWeight").Value = 0
                    '        End If
                    '        '10
                    '        If IsNumeric(.Rows(i).Cells("M3").Value) = False Then
                    '            .Rows(i).Cells("M3").Value = 0
                    '        End If
                    '        '10
                    '        If IsDBNull(.Rows(i).Cells("Lot").Value) Then
                    '            .Rows(i).Cells("Lot").Value = ""
                    '        End If
                    '        If IsDBNull(.Rows(i).Cells("Line_Remarks").Value) Then
                    '            .Rows(i).Cells("Line_Remarks").Value = ""
                    '        End If
                    '    End With
                    'Next
            End Select
           

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
        Try
            'checking 
            If Me.txtCustomer_Id.Text = "" Then
                W_MSG_Information("กรุณาระบุ Customer ")
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
            Select Case objStatus
                Case enuOperation_Type.ASN
                    Dim oImport_ASN As New Import_ASN
                    If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                        Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

                        Dim objWS As DataTable = New DataTable

                        objWS = oImport_ASN.LoadWorkSheet(Me.txtFilePath.Text)

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
                Case enuOperation_Type.ASN_CN2
                    'Dim oImport_ASN_CN2 As New Import_ASN_CN2
                    'If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                    '    Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim
                    '    'Me.txtFilePath.Tag = OpenFileDialog1.SafeFileName.Trim

                    '    'Dim filename() As String = Me.txtFilePath.Text.Split("\")
                    '    'Dim filename2 As String = filename(filename.Length - 1)

                    '    Dim objWS As DataTable = New DataTable

                    '    objWS = oImport_ASN_CN2.LoadWorkSheet(Me.txtFilePath.Text)

                    '    With cboWorkSheet
                    '        .DataSource = objWS
                    '        .DisplayMember = "TABLE_NAME"
                    '        .ValueMember = "TABLE_NAME"
                    '    End With

                    '    cboWorkSheet.SelectedIndex = 0

                    '    LoadData()
                    '    Me.grdPreviewData.Columns("DocRef_No").Visible = False
                    '    '====================
                    'Else
                    '    Exit Sub
                    'End If
            End Select
           

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    'Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles s.Click
    '    Try
    '        'checking 
    '        If Me.txtCustomer_Id.Text = "" Then
    '            W_MSG_Information("กรุณาระบุ Customer ")
    '            Exit Sub
    '            Exit Sub
    '        End If

    '        If Me.txtWorkSheet.Text.Trim = "" Then
    '            W_MSG_Information("กรุณาระบุ Work Sheet")
    '            Exit Sub
    '        End If
    '        Dim oImport_ASN As New Import_ASN
    '        If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
    '            Me.txtFilePath.Text = OpenFileDialog1.FileName.Trim

    '            'load data from excel


    '            Me.grdPreviewData.DataSource = oImport_ASN.LoadDataFromFile(Me.txtFilePath.Text, Me.txtWorkSheet.Text)
    '        Else
    '            Exit Sub
    '        End If

    '        With oImport_ASN
    '            .DataSource = Me.grdPreviewData.DataSource
    '            '.Customer_Index = Me.cboCustomer.SelectedValue
    '            '.DocumentType_Index = Me.cboDocumentType.SelectedValue
    '            '.ItemStatus_Index = Me.cboItemStatus.SelectedValue


    '            If .CheckingData Then

    '                _boolResult = True
    '            Else
    '                _boolResult = False
    '            End If
    '        End With
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try

    'End Sub

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

    Private Sub frmImport_ASN_Excel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim oFunction As New W_Language

        ''Insert
        'oFunction.Insert(Me)

        'SwitchLanguage
        oFunction.SwitchLanguage(Me)

        Me.SET_DEFAULT_CUSTOMER_BYUSER()
        'Me.AddcbProductType()
        Me.getDocumentType(16)
        Me.getProductStatus()

        Select Case objStatus
            Case enuOperation_Type.ASN
                Me.Text = "Import Receiving Data [EXCEL]"
            Case enuOperation_Type.ASN_CN2
                Me.lblChstomerShippingLocation.Visible = True
                Me.txtCustomerShippingLocation_ID.Visible = True
                Me.btnSearchCustomerShippingLocation.Visible = True
                Me.txtCustomerShippingLocation_Name.Visible = True

                Me.Text = "Import CN2_Receiving Data [EXCEL]"
        End Select

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

    Private Sub getProductStatus()
        'Load Item Status
        Dim objDocumentType_Itemstatus As New ms_DocumentType_ItemStatus(ms_DocumentType_ItemStatus.enuOperation_Type.SEARCH)
        objDocumentType_Itemstatus.SearchDocumentType("", "", cboDocumentType.SelectedValue)

        With cboItemStatus
            .DisplayMember = "ItemStatusDes"
            .ValueMember = "ItemStatus_Index"
            .DataSource = objDocumentType_Itemstatus.DataTable
        End With
    End Sub

    Private Sub getDocumentType(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
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

            ' *************************************
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub frmImport_ASN_Excel_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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
                Case enuOperation_Type.ASN_CN2
                    If txtCustomerShippingLocation_ID.Text.Trim <> "" Then
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

    Private Sub BntLoaddata_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BntLoaddata.Click
        LoadData()
    End Sub

    Private Sub LoadData()
        'checking
        If Me.txtFilePath.Text.Trim = "" Or Me.cboWorkSheet.Text = "" Then 'Or Me.txtWorkSheet.Text.Trim = "" Then
            W_MSG_Information("กรุณาป้อนระบุ File Path และ Work sheet ให้ครบ")
            Exit Sub
        End If

        Select Case objStatus
            Case enuOperation_Type.ASN
                Dim oImport_ASN As New Import_ASN

                '===============
                Me.grdPreviewData.DataSource = oImport_ASN.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

                If Me.grdPreviewData.RowCount = 0 Then
                    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                    Exit Sub
                End If

                '===============

                oImport_ASN.DataSource = grdPreviewData.DataSource
            Case enuOperation_Type.ASN_CN2
                'Dim oImport_ASN_CN2 As New Import_ASN_CN2
                ''===============
                'Me.grdPreviewData.DataSource = oImport_ASN_CN2.LoadDataFromFile(Me.txtFilePath.Text, Me.cboWorkSheet.Text)

                'If Me.grdPreviewData.RowCount = 0 Then
                '    W_MSG_Information("ไม่พบข้อมูล กรุณาเลือกข้อมูลนำเข้าใหม่อีกครั้ง")
                '    Exit Sub
                'End If

                ''===============
                'oImport_ASN_CN2.DataSource = grdPreviewData.DataSource
                'Me.grdPreviewData.Columns("DocRef_No").Visible = False
        End Select
 

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

    Private Sub cboItemStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboItemStatus.SelectedIndexChanged
        Try
            Me.DocumentItem_Status_Index = cboItemStatus.SelectedValue.ToString
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtFilePath_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFilePath.DoubleClick
        Try
            Select Case objStatus
                Case enuOperation_Type.ASN
                    Dim AppPath As String
                    AppPath = Application.StartupPath
                    Dim ps As New ProcessStartInfo()
                    With ps
                        'Dim strApppath As String = ""
                        'strApppath = AppPath.Replace("\bin", "")
                        .FileName = AppPath + "\Import\Format\PO_IMPORT_TIMCO.xls"
                        'D:\Project\WMS_Site\WMS_Site_TIMCO\WMS_Site_TIMCO\Import\Format\PO_IMPORT_TIMCO.xls
                        .UseShellExecute = True
                    End With

                    Dim p As New Process
                    p.StartInfo = ps
                    p.Start()
                Case enuOperation_Type.ASN_CN2
                    Dim AppPath As String
                    AppPath = Application.StartupPath
                    Dim ps As New ProcessStartInfo()
                    With ps
                        'Dim strApppath As String = ""
                        'strApppath = AppPath.Replace("\bin", "")
                        .FileName = AppPath + "\Import\Format\ASN_IMPORT_TIMCO_CN2_Receiving.xls"
                        'D:\Project\WMS_Site\WMS_Site_TIMCO\WMS_Site_TIMCO\Import\Format\PO_IMPORT_TIMCO.xls
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
    Private Sub btnSearchCustomerShippingLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchCustomerShippingLocation.Click
        'TODO: HARDCODE-MSG
        Try
            If txtCustomer_Id.Text = "" Then
                W_MSG_Information_ByIndex(8)
                Exit Sub
            End If
            Dim frm As New frmCustomer_Receive_Location_PopUp 'frmCus_Ship_Location_Popup
            frm.Customer_Index = _Customer_Index
            frm.Customer_Receive_Location_Index = "" 'Me.txtCustomerShipping_ID.Tag
            frm.ShowDialog()

            Dim tmpCustomer_Shipping_Location_Index As String = ""
            tmpCustomer_Shipping_Location_Index = frm.Customer_Receive_Location_Index 'frm.Customer_Shipping_Location_Index

            If tmpCustomer_Shipping_Location_Index = "" Then
                Me.txtCustomerShippingLocation_ID.Tag = ""
                Me.txtCustomerShippingLocation_ID.Text = ""
                Me.txtCustomerShippingLocation_Name.Text = ""
                'Me.txtSourceAddress.Text = ""
            Else
                Me.txtCustomerShippingLocation_ID.Tag = tmpCustomer_Shipping_Location_Index
                Me.txtCustomerShippingLocation_ID.Text = frm.Customer_Receive_Location_Id
                Me.txtCustomerShippingLocation_Name.Text = frm.Receive_Location_Name
                'Me.txtSourceAddress.Text = frm.Cust_Rec_Address
            End If

            'frm.Close()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


End Class