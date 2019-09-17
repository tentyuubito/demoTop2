Imports WMS_STD_Master.W_Language
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_TMM_Transfer_Datalayer
Imports WMS_STD_OUTB_Reserv_Datalayer
Imports WMS_STD_OUTB
Imports WMS_STD_OUTB_Datalayer
Imports WMS_STD_OUTB_Reserv
Imports WMS_STD_TMM_Report


Public Class frmTransferOwner_V2


    Private _Customer_index As String = ""
    Public Property Customer_index() As String
        Get
            Return _Customer_index
        End Get
        Set(ByVal value As String)
            _Customer_index = value
        End Set
    End Property


    Private _New_Customer_index As String = ""
    Public Property NEW_Customer_index() As String
        Get
            Return _New_Customer_index
        End Get
        Set(ByVal value As String)
            _New_Customer_index = value
        End Set
    End Property

    Private _AssetLocationBalance_Index As String = ""
    Private _Asset_No As String = ""
    Private _AssetSerial_No As String = ""
    Private _isSerail As Boolean = False
    Dim _tmpLocation_Alias As String = ""
    Dim _tmpTotalQty_PutAway As Decimal = 0
    Dim _tmpQty_PutAway As Decimal = 0

    Private _DEFAULT_SerialNO As Decimal
    Private objStatusCase As enuOperation_Type


    Sub New(ByVal CaseStatus As enuOperation_Type)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        objStatusCase = CaseStatus
    End Sub


#Region "  Property   "
    Private TranNo As String = ""
    Private _intDefaultProcess_ID As Integer = 50

    Private _strDefaultStatus_Index As String = ""

    Private _strDefaultLocation_Index As String = ""

    Private _strDefaultItemStatus As String = ""

    Private _strDefaultLocation_Alias As String = ""

    Public DocumentStatusValue As String = ""

    Dim _NewItem As Integer
    Dim _Reference As String
    Dim _Reference2 As String
    Dim _Plan_Process As Integer
    Dim _DocumentPlan_No As String
    Dim _DocumentPlanItem_Index As String
    Dim _DocumentPlan_Index As String
    Dim _Remark As String


    Private _TransferOwner_Index As String
    Public Property TransferOwner_Index() As String
        Get
            Return _TransferOwner_Index
        End Get
        Set(ByVal value As String)
            _TransferOwner_Index = value
        End Set
    End Property

    Public Enum enuOperation_Type
        ADDNEW
        DELETE
        SEARCH
        CANCEL
        NULL
        EDIT
    End Enum

    Dim TransferStatus_Document As status_Document
    Private Enum status_Document
        SO = 10
        Packing = 7
        Reserve = 17
        Transport = 25
    End Enum

    Public ArrSalesOrder_Index As New List(Of String)

#End Region

#Region "   Page Load   "

    Private Sub frmAssetTransferStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language

            oFunction.SwitchLanguage(Me, 50)
            oFunction.SW_Language_Column(Me, Me.grdTransferStatusLocation, 50)
            grdTransferStatusLocation.AutoGenerateColumns = False
            grdWithDrawPlan.AutoGenerateColumns = False

            SetDEFAULT_CUSTOMER_INDEX()

            LoadDocumentType()
            LoadItemStatus()
            LoadDefault_ItemStatusAndLocation()
            Me.getReportName(5)

            GetItemStatus_To_Gridview()

            Me.txtTimes.Text = Now.ToString("hh:mm")

            Select Case objStatusCase
                Case enuOperation_Type.ADDNEW
                    If ArrSalesOrder_Index IsNot Nothing Then
                        For Each strSalesOrder_Index As String In ArrSalesOrder_Index
                            LoadPlanWithdraw(strSalesOrder_Index, status_Document.SO)
                        Next
                        Dim ocus As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
                        ocus.getPopup_Search(" AND Customer_index='" & _New_Customer_index & "'")
                        Dim dtcus As New DataTable
                        dtcus = ocus.GetDataTable
                        If dtcus.Rows.Count > 0 Then
                            Me.txtCustomer_IdNew.Tag = dtcus.Rows(0)("Customer_Index")
                            Me.txtCustomer_IdNew.Text = dtcus.Rows(0)("Customer_Id")
                            Me.txtCustomer_NameNew.Text = dtcus.Rows(0)("Customer_Name")
                        End If
                        If ArrSalesOrder_Index.Capacity > 0 Then
                            TabControl1.SelectedTab = Me.tpSO
                        End If
                    End If

                    Dim Strtmp As String = ""
                    Strtmp = InsertTmpHaeder()
                    If Strtmp.ToUpper <> "S" Then
                        W_MSG_Information(Strtmp)
                        Me.Close()
                    End If


                Case enuOperation_Type.EDIT
                    GetTransferOwnerHeader(_TransferOwner_Index)
                    GetTransferOwnerLocation(_TransferOwner_Index)
                    cboPrint.Enabled = True
                    btnPrint.Enabled = True
                    Select Case DocumentStatusValue
                        Case "-1", "2"
                            Me.grbTransferStatus.Enabled = False
                            Me.txtSerial_No.Enabled = False
                            Me.txtAsset_No.Enabled = False
                            Me.btnAddItem.Enabled = False
                            Me.btnDeleteItem.Enabled = False
                            Me.grdTransferStatusLocation.ReadOnly = True
                            Me.btnSave.Enabled = False
                        Case Else
                            Dim objReserve As New Cl_TransferReserv
                            objReserve.UpdateUseDoc_Owner(Cl_TransferReserv.CaseReserve.Reserve, _TransferOwner_Index)
                    End Select


            End Select





        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Function InsertTmpHaeder() As String
        Try
            Dim oHeader As New tb_TransferOwner
            Dim objSy_AutoNumber As New Sy_AutoNumber

            With oHeader
                Dim _Transation_Date As DateTime = Format(Me.dtpTransferStatus_Date.Value, "dd/MM/yyyy").ToString()
                .Customer_Index = IIf(Me.txtCustomer_Id.Tag Is Nothing, "0010000000001", Me.txtCustomer_Id.Tag)
                .New_Customer_Index = IIf(Me.txtCustomer_Id.Tag Is Nothing, "0010000000001", Me.txtCustomer_Id.Tag)
                .TransferOwner_Date = _Transation_Date
                .Times = Me.txtTimes.Text
                .DocumentType_Index = Me.cboDocumentType.SelectedValue.ToString
                Me._TransferOwner_Index = .InsertTempHesder()
            End With
            Return "S"
        Catch ex As Exception
            Return ex.Message.ToString
        End Try
    End Function


    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
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

#End Region

#Region "   Function   "

    Private Function CheckDuplicateAsset(ByVal pstrColumns As String, ByVal Asset As String) As Boolean
        Try
            Dim Columns As String = ""
            Select Case pstrColumns.ToLower()
                Case "serial no"
                    Columns = "col_serial_No"
                Case "asset no"
                    Columns = "col_Asset_No"
                Case "tid"
                    Columns = "col_Str1"
            End Select
            For i As Integer = 0 To grdTransferStatusLocation.Rows.Count - 1
                If grdTransferStatusLocation.Rows(i).Cells(Columns).Value = Asset Then
                    W_MSG_Information("ข้อมูล " & pstrColumns & " : " & Asset & " ซ้ำ")
                    Return False
                End If
            Next
            Return True

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Function Clear(ByVal iRow As Integer) As Boolean
        Try
            With grdTransferStatusLocation
                .Rows(iRow).Cells("col_SKU_Index").Value() = ""
                .Rows(iRow).Cells("col_SKU_ID").Value() = ""
                .Rows(iRow).Cells("col_serial_No").Value() = ""
                .Rows(iRow).Cells("col_Asset_No").Value = ""
                .Rows(iRow).Cells("col_Asset_NoType").Value = ""
                .Rows(iRow).Cells("col_Brand").Value = ""
                .Rows(iRow).Cells("col_Model").Value = ""
                .Rows(iRow).Cells("col_Str1").Value = ""
                .Rows(iRow).Cells("col_Str2").Value = ""
                .Rows(iRow).Cells("col_Qty").Value = ""
                .Rows(iRow).Cells("Col_ItemStatus_Des").Value = ""
                .Rows(iRow).Cells("Col_From_Location_Alias").Value = ""
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function DefineValue_To_TransferStatusHeader() As tb_TransferOwner
        Dim oHeaderTransferOwner As New tb_TransferOwner
        With oHeaderTransferOwner
            .TransferOwner_Index = _TransferOwner_Index
            .TransferOwner_No = Me.txtTransferStatus_No.Text
            Dim _Transation_Date As DateTime = Format(Me.dtpTransferStatus_Date.Value, "dd/MM/yyyy").ToString()
            .TransferOwner_Date = _Transation_Date
            .Customer_Index = Me.txtCustomer_Id.Tag
            .Ref_No1 = Me.txtRef_No1.Text
            .Ref_No2 = Me.txtRef_No2.Text
            .Str2 = ""
            .Times = Me.txtTimes.Text
            .Comment = Me.txtComment.Text
            .DocumentType_Index = Me.cboDocumentType.SelectedValue
            .New_Customer_Index = txtCustomer_IdNew.Tag
            'If Me.txtCustomer_Ship_Name.Tag Is Nothing Then
            '    .Str1 = ""
            'Else
            '    .Str1 = Me.txtCustomer_Ship_Name.Tag
            'End If


        End With
        Return oHeaderTransferOwner
    End Function

    Private Function CheckData(ByVal pirow As Integer) As Boolean
        Try
            With grdTransferStatusLocation.Rows(pirow)

                'If Cdec(.Cells("Col_Move_Qty").Value) > Cdec(.Cells("Col_Qty_Bal").Value) Then
                '    W_MSG_Information("จำนวนที่ย้ายมากกว่าสินค้าในคลัง")

                '    .Cells("Col_Move_Qty").Value = .Cells("Col_Qty_Bal").Value
                '    Return False
                'End If

                If CDec(.Cells("Col_Move_Qty").Value) = 0 Then
                    W_MSG_Information("กรุณาป้อนจำนวนสินค้าที่ย้าย")

                    Return False
                End If


                If .Cells("Col_To_Location_Alias").Value Is Nothing Then
                    W_MSG_Information("กรุณาป้อนตำแหน่งให้ครบตามที่เลือก")
                    Return False
                Else
                    'Find Location_Index by Location_Alias in ms_location
                    Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    Dim strTo_Location_Index As String = oms_Location.getLocation_Index(.Cells("Col_To_Location_Alias").Value)

                    .Cells("Col_To_Location_Index").Value = strTo_Location_Index

                    If strTo_Location_Index = "" Then
                        W_MSG_Information("กรุณาป้อนตำแหน่งใหม่ ไม่พบตำแหน่งที่ระบุ  " & .Cells("Col_To_Location_Alias").Value)
                        Return False
                    Else
                        If oms_Location.isOverFlow_Qty(.Cells("Col_To_Location_Alias").Value, CDec(.Cells("Col_Move_Qty").Value)) = True Then
                            W_MSG_Information("ตำแหน่งใหม่ " & .Cells("Col_To_Location_Index").Value & " พื้นที่ว่างไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                            Return False
                        End If
                    End If

                End If
            End With

            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Sub SetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then

                txtCustomer_Id.Tag = objDT.Rows(0).Item("Customer_Index").ToString
                txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString

            End If

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

    Private Sub GetItemStatus_To_Gridview()

        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatus()
            objDT = objClassDB.DataTable

            With Col_New_ItemStatus_Des

                .DataSource = objDT
                .DisplayMember = "ItemStatus"
                .ValueMember = "ItemStatus_Index"

            End With

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Sub CheckCustomConfig_Use_Serial()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetValue_Config("Use_Serial")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then

                If objDT.Rows(0)("Config_Value").ToString = 0 Then
                    'Not use Serial Mode
                    Me.grdTransferStatusLocation.Location = New Point(6, 19)
                    Me.grdTransferStatusLocation.Size = New Size(939, 350)

                Else
                    'Use Serial Mode
                    Me.grdTransferStatusLocation.Location = New Point(6, 64)
                    Me.grdTransferStatusLocation.Size = New Size(939, 305)
                End If
            Else
                'Not use Serial Mode
                Me.grdTransferStatusLocation.Location = New Point(6, 19)
                Me.grdTransferStatusLocation.Size = New Size(939, 350)
            End If

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

#End Region

#Region "   Button   "

    Private Sub btnSeachCustomer_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value ****

            If frm.Customer_Index = "" Then
                Me.txtCustomer_Id.Tag = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""

            Else
                Me.txtCustomer_Id.Tag = frm.Customer_Index
                Me.txtCustomer_Id.Text = frm.strCustomer_Name_Id
                Me.txtCustomer_Name.Text = frm.customerName
            End If
            ' *********************
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnDeleteItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteItem.Click

        Dim oSession As New WMS_STD_Master.UserSession
        Dim oBolCheck As Boolean = oSession.CheckSession
        If oBolCheck = False Then
            Exit Sub
        End If


        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0

        If grdTransferStatusLocation.Rows.Count = 0 Then
            W_MSG_Information("กรุณาเลือกรายการที่ต้องการลบ")
            Exit Sub
        End If


        Try

            If W_MSG_Confirm("ท่านต้องการลบรายการนี้ ใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then

                objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)

                Dim objDB As New TransferOwnerTransaction
                If Me.grdTransferStatusLocation.CurrentRow.Cells("col_TransferOwnerLocation_Index").Value.ToString <> "" Then
                    objDB.Delete_TransferLocation(Me.grdTransferStatusLocation.CurrentRow.Cells("col_TransferOwnerLocation_Index").Value.ToString, objcon.Connection, myTrans)
                    objDB = Nothing
                End If

                Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                Dim LocationBal_Index As String = Me.grdTransferStatusLocation.CurrentRow.Cells("Col_LocationBalance_Index").Value
                Dim Oty_Reserv As Decimal = CDec(Me.grdTransferStatusLocation.CurrentRow.Cells("Col_Move_Qty").Value)
                Dim Weight_Reserv As Decimal = CDec(Me.grdTransferStatusLocation.CurrentRow.Cells("Col_Weight").Value)
                Dim Volume_Reserv As Decimal = CDec(Me.grdTransferStatusLocation.CurrentRow.Cells("Col_Volume").Value)
                Dim ItemQty_Reserv As Decimal = CDec(Me.grdTransferStatusLocation.CurrentRow.Cells("col_ItemQty").Value)
                Dim Price_Reserv As Decimal = CDec(Me.grdTransferStatusLocation.CurrentRow.Cells("col_Price").Value)
                Dim Package_Index As String = grdTransferStatusLocation.CurrentRow.Cells("col_Package_Index").Value.ToString
                Dim Sku_Index As String = grdTransferStatusLocation.CurrentRow.Cells("col_Sku_Index").Value.ToString
                Dim Ratio As Decimal = 1
                Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                Ratio = objRatio.getRatio(Sku_Index, Package_Index)
                objRatio = Nothing
                Oty_Reserv = Oty_Reserv * Ratio
                objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 50, Me._TransferOwner_Index, "ลบรายการโอนเจ้าของ", LocationBal_Index, 0, 0, 0, 0, 0, 0, Oty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                With grdTransferStatusLocation
                    Dim strPlan_Process As String = grdTransferStatusLocation.CurrentRow.Cells("col_TranPlan_Process").Value.ToString
                    Dim strDocumentPlanItem_Index As String = .CurrentRow.Cells("col_TranDocumentPlanItem_Index").Value.ToString
                    Dim strDocumentPlan_Index As String = .CurrentRow.Cells("col_TranDocumentPlan_Index").Value.ToString

                    If strPlan_Process <> -9 Then

                        Dim oWithDrawItem As New TransferOwnerTransaction()
                        Dim dblWithDrawWeight As Decimal = CDec(.CurrentRow.Cells("Col_Weight").Value.ToString)
                        Dim dblWithDrawVolume As Decimal = CDec(.CurrentRow.Cells("Col_Volume").Value.ToString)
                        Dim dblWithDrawQty As Decimal = CDec(.CurrentRow.Cells("Col_Move_Qty").Value.ToString)
                        Dim dblWItemQty As Decimal = CDec(.CurrentRow.Cells("col_ItemQty").Value.ToString)
                        Dim dblWTotalQty As Decimal = CDec(.CurrentRow.Cells("Col_Total_Qty").Value.ToString)

                        Dim intRowPlan As Integer
                        If grdWithDrawPlan.RowCount > 0 Then
                            If .CurrentRow.Cells("col_TranDocumentPlanItem_Index").Value IsNot Nothing Then
                                For intRowPlan = 0 To grdWithDrawPlan.Rows.Count - 1
                                    If (strDocumentPlanItem_Index = grdWithDrawPlan.Rows(intRowPlan).Cells("col_DocumentPlanItem_Index").Value.ToString) And (strPlan_Process = grdWithDrawPlan.Rows(intRowPlan).Cells("col_Plan_Process").Value.ToString) Then
                                        '        '--- จำนวนที่ต้องเบิก ก่อนลบจาก WithDraw
                                        Dim dblPlanQty As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value.ToString)
                                        Dim dblPlanItemQty As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Item_Qty").Value.ToString)
                                        Dim dblQtyNeed As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_Plan").Value.ToString)
                                        Dim dblPlanTotalQty As Decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Total_Qty_Paln").Value.ToString)
                                        Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value = dblPlanQty - dblWithDrawQty
                                        Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Item_Qty").Value = dblPlanItemQty - dblWItemQty
                                        Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Total_Qty_Paln").Value = dblPlanTotalQty - dblWTotalQty
                                        If grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value < dblQtyNeed Then
                                            grdWithDrawPlan.Rows(intRowPlan).DefaultCellStyle.BackColor = Color.Gainsboro
                                            grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Style.BackColor = Color.WhiteSmoke
                                        End If
                                    End If
                                Next
                            End If
                        End If
                        oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, dblWithDrawQty, dblWTotalQty, dblWItemQty, dblWithDrawWeight, dblWithDrawVolume, strPlan_Process, objcon.Connection, myTrans)
                    End If
                End With


                grdTransferStatusLocation.Rows.RemoveAt(grdTransferStatusLocation.CurrentRow.Index)
            End If

            myTrans.Commit()
        Catch ex As Exception
            myTrans.Rollback()
            W_MSG_Error(ex.Message.ToString)
        Finally
            objcon.disconnectDB()
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If grdTransferStatusLocation.Rows.Count - 1 < 0 Then
                W_MSG_Information("ไม่พบรายการที่จะทำการบันทึก กรุณาตรวจสอบข้อมูล")
                Exit Sub
            End If

            If txtCustomer_IdNew.Tag Is Nothing Then
                W_MSG_Information("กรุณาเลือกเจ้าของสินค้าใหม่")
                Exit Sub
            End If
            If txtCustomer_IdNew.Tag = "" Then
                W_MSG_Information("กรุณาเลือกเจ้าของสินค้าใหม่")
                Exit Sub
            End If
            For j As Integer = 0 To grdTransferStatusLocation.Rows.Count - 1
                If grdTransferStatusLocation.Rows(j).Cells("col_SKU_Index_New").Value Is Nothing Then
                    W_MSG_Information("กรุณาเลือกระบุรหัสสินค้าใหม่")
                    Exit Sub
                End If
                If grdTransferStatusLocation.Rows(j).Cells("col_SKU_Index_New").Value = "" Then
                    W_MSG_Information("กรุณาเลือกระบุรหัสสินค้าใหม่")
                    Exit Sub
                End If
            Next


            If Me.txtCustomer_Id.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนเลือกข้อมูล" & Me.lblCustomer.Text.Trim)
                Exit Sub
            End If

            For irow As Integer = 0 To grdTransferStatusLocation.Rows.Count - 1
                If CheckData(irow) = False Then
                    Exit Sub
                End If
            Next

            Dim objHeader As New tb_TransferOwner
            Dim objLocation(grdTransferStatusLocation.Rows.Count - 1) As tb_TransferOwnerLocation
            Dim objms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            Dim objms_ItemStatus As New ms_ItemStatus(ms_Location.enuOperation_Type.SEARCH)
            Dim newTransferStatus_Index As String = ""
            Dim iToLocation_Index As String = ""

            objHeader = DefineValue_To_TransferStatusHeader()


            If Me.txtTransferStatus_No.Text = "" And objStatusCase = enuOperation_Type.ADDNEW Then
                Dim strWhere As String = ""
                Dim objDocumentNumber As New clsDocumentNumber()
                objHeader.TransferOwner_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, Me.dtpTransferStatus_Date.Value) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                txtTransferStatus_No.Text = objHeader.TransferOwner_No
                objDocumentNumber = Nothing
            End If


            Dim i As Integer = 0
            Dim objClassDB As New TransferOwnerTransaction
            'Dim strTransferOwnerLocation_Index As String = ""

            For i = 0 To grdTransferStatusLocation.Rows.Count - 1
                objLocation(i) = New tb_TransferOwnerLocation
                With objLocation(i)

                    'Dim objDBIndex As New Sy_AutoNumber
                    'strTransferOwnerLocation_Index = objDBIndex.getSys_Value("TransferOwnerLocation_Index")
                    'objDBIndex = Nothing

                    .TransferOwnerLocation_Index = grdTransferStatusLocation.Rows(i).Cells("col_TransferOwnerLocation_Index").Value
                    .From_LocationBalance_Index = grdTransferStatusLocation.Rows(i).Cells("Col_LocationBalance_Index").Value
                    .To_LocationBalance_Index = grdTransferStatusLocation.Rows(i).Cells("Col_LocationBalance_Index").Value
                    .Old_Sku_Index = grdTransferStatusLocation.Rows(i).Cells("Col_SKU_Index").Value.ToString
                    .New_Sku_Index = grdTransferStatusLocation.Rows(i).Cells("col_SKU_Index_New").Value.ToString
                    .Package_Index = grdTransferStatusLocation.Rows(i).Cells("Col_Package_Index").Value.ToString
                    .Order_Index = grdTransferStatusLocation.Rows(i).Cells("Col_Order_Index").Value.ToString
                    .OrderItem_Index = grdTransferStatusLocation.Rows(i).Cells("Col_OrderItem_Index").Value.ToString

                    .Old_ItemStatus_Index = grdTransferStatusLocation.Rows(i).Cells("Col_ItemStatus_Index").Value
                    .New_ItemStatus_Index = grdTransferStatusLocation.Rows(i).Cells("Col_New_ItemStatus_Des").Value

                    .From_Location_Index = grdTransferStatusLocation.Rows(i).Cells("Col_From_Location_Index").Value
                    .To_Location_Index = grdTransferStatusLocation.Rows(i).Cells("Col_To_Location_Index").Value


                    .Lot_No = grdTransferStatusLocation.Rows(i).Cells("Col_Lot_No").Value
                    .Plot = grdTransferStatusLocation.Rows(i).Cells("Col_Plot").Value
                    .new_Plot = grdTransferStatusLocation.Rows(i).Cells("col_newLot").Value

                    .Tag_No = grdTransferStatusLocation.Rows(i).Cells("Col_Tag_No").Value
                    .TAG_Index = grdTransferStatusLocation.Rows(i).Cells("Col_TAG_Index").Value
                    .Qty = grdTransferStatusLocation.Rows(i).Cells("Col_Move_Qty").Value
                    Dim Ratio As Decimal = 1
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    Ratio = objRatio.getRatio(.Old_Sku_Index, .Package_Index)
                    objRatio = Nothing
                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    '--- Total_Qty จำนวนของหน่วยหลัก 
                    .Total_Qty = CDec(grdTransferStatusLocation.Rows(i).Cells("Col_Move_Qty").Value) * Ratio
                    .Ratio = Ratio


                    .Weight = grdTransferStatusLocation.Rows(i).Cells("Col_Weight").Value
                    .Volume = grdTransferStatusLocation.Rows(i).Cells("Col_Volume").Value
                    .Item_Package_Index = grdTransferStatusLocation.Rows(i).Cells("Col_Item_Package_Index").Value.ToString
                    .Item_Qty = grdTransferStatusLocation.Rows(i).Cells("col_ItemQty").Value
                    .Price = grdTransferStatusLocation.Rows(i).Cells("col_Price").Value
                    .PalletType_Index = grdTransferStatusLocation.Rows(i).Cells("Col_PalletType_Index").Value
                    .Pallet_Qty = grdTransferStatusLocation.Rows(i).Cells("Col_Pallet_Qty").Value

                    .Serial_No = grdTransferStatusLocation.Rows(i).Cells("col_serial_No").Value
                    .Asset_No = grdTransferStatusLocation.Rows(i).Cells("col_Asset_No").Value

                    .MfgDate = grdTransferStatusLocation.Rows(i).Cells("Col_Mfg_Date").Value
                    .ExpDate = grdTransferStatusLocation.Rows(i).Cells("Col_Exp_Date").Value
                    .Pallet_No = "" 'grdTransferStatusLocation.Rows(i).Cells("Col_Pall_No").Value
                    .ERP_LOCATION = grdTransferStatusLocation.Rows(i).Cells("Col_ERP_LOCATION").Value
                    .IsMfg_Date = grdTransferStatusLocation.Rows(i).Cells("Col_IsMfg_Date").Value
                    .IsExp_Date = grdTransferStatusLocation.Rows(i).Cells("Col_IsExp_Date").Value

                    .Status = 1

                    If grdTransferStatusLocation.Rows(i).Cells("col_TranPlan_Process").Value Is Nothing Then
                        .Plan_Process = -9
                    Else
                        If grdTransferStatusLocation.Rows(i).Cells("col_TranPlan_Process").Value = 0 Then
                            .Plan_Process = -9
                        Else
                            .Plan_Process = grdTransferStatusLocation.Rows(i).Cells("col_TranPlan_Process").Value
                        End If
                    End If
                    If grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlan_Index").Value Is Nothing Then
                        .DocumentPlan_Index = ""
                    Else
                        If IsDBNull(grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlan_Index").Value) Then
                            .DocumentPlan_Index = ""
                        Else
                            .DocumentPlan_Index = grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlan_Index").Value
                        End If
                    End If
                    If grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlan_No").Value Is Nothing Then
                        .DocumentPlan_No = ""
                    Else
                        If IsDBNull(grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlan_No").Value) Then
                            .DocumentPlan_No = ""
                        Else
                            .DocumentPlan_No = grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlan_No").Value
                        End If
                    End If
                    If grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlanItem_Index").Value Is Nothing Then
                        .DocumentPlanItem_Index = ""
                    Else
                        If IsDBNull(grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlanItem_Index").Value) Then
                            .DocumentPlanItem_Index = ""
                        Else
                            .DocumentPlanItem_Index = grdTransferStatusLocation.Rows(i).Cells("col_TranDocumentPlanItem_Index").Value
                        End If

                    End If

                    If SetAUTO_REFERENCE() = 1 Then
                        If grdTransferStatusLocation.Rows(i).Cells("col_Str1").Value.ToString = "" Then
                            MessageBox.Show("Reference", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.grdTransferStatusLocation.Rows(i).Selected = True
                            Exit Sub
                        Else
                            .Str1 = grdTransferStatusLocation.Rows(i).Cells("col_Str1").Value.ToString
                        End If
                        '--- Referecne 2
                        If grdTransferStatusLocation.Rows(i).Cells("col_Str2").Value = "" Then
                            MessageBox.Show("Reference2", "การตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                            Me.grdTransferStatusLocation.Rows(i).Selected = True
                            Exit Sub
                        Else
                            .Str2 = grdTransferStatusLocation.Rows(i).Cells("col_Str2").Value.ToString
                        End If

                    Else

                        If grdTransferStatusLocation.Rows(i).Cells("col_Str1").Value IsNot Nothing Then
                            .Str1 = grdTransferStatusLocation.Rows(i).Cells("col_Str1").Value.ToString
                        Else
                            .Str1 = ""
                        End If
                        '--- Referecne 2
                        If grdTransferStatusLocation.Rows(i).Cells("col_Str2").Value IsNot Nothing Then
                            .Str2 = grdTransferStatusLocation.Rows(i).Cells("col_Str2").Value.ToString
                        Else
                            .Str2 = ""
                        End If

                    End If

                    .Str4 = ""
                    .Str5 = ""
                    .ERP_LOCATION = grdTransferStatusLocation.Rows(i).Cells("Col_ERP_LOCATION").Value
                    .AssetLocationBalance_Index = grdTransferStatusLocation.Rows(i).Cells("Col_AssetLocationBalance_Index").Value
                    .Flo1 = 0
                    .Flo2 = 0
                    .Flo3 = 0
                    .Flo4 = 0
                    .Flo5 = 0

                End With

            Next

            Select Case objStatusCase
                Case enuOperation_Type.ADDNEW
                    newTransferStatus_Index = objClassDB.UpdateTransferOwner_V2(objHeader, objLocation)
                Case enuOperation_Type.EDIT
                    newTransferStatus_Index = objClassDB.UpdateTransferOwner_V2(objHeader, objLocation)
            End Select


            If Not newTransferStatus_Index = "" Then
                _TransferOwner_Index = newTransferStatus_Index
                Me.GetTransferOwnerHeader(_TransferOwner_Index)
                Me.GetTransferOwnerLocation(_TransferOwner_Index)

                W_MSG_Information_ByIndex(1)
                objStatusCase = enuOperation_Type.EDIT
            Else
                W_MSG_Information_ByIndex(2)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
        cboPrint.Enabled = True
        btnPrint.Enabled = True
    End Sub

#End Region

    Function SetAUTO_REFERENCE() As Integer
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_AUTO_REFERENCE", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim intBlock As Integer = 0
            If objDT.Rows.Count > 0 Then
                intBlock = 1
            Else
                intBlock = 0
            End If

            Return intBlock

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function

#Region "   GridView   "

    'Private Sub ShowDataGridView(ByVal pdtAsset As DataTable)

    '    Dim iRow_Index As Integer = 0
    '    Dim odrAsset As DataRow = pdtAsset.Rows(0)
    '    Try


    '        Me.grdTransferStatusLocation.Rows.Insert(0, 1)
    '        With grdTransferStatusLocation.Rows(0)

    '            .Cells("col_TransferStatusLocation_Index").Value = ""
    '            .Cells("Col_LocationBalance_Index").Value = odrAsset("LocationBalance_Index").ToString

    '            .Cells("Col_Sku_ID").Value = odrAsset("Sku_Id").ToString
    '            .Cells("Col_Sku_Index").Value = odrAsset("Sku_Index").ToString
    '            .Cells("Col_Sku_Description").Value = odrAsset("Sku_Des").ToString
    '            .Cells("Col_Plot").Value = odrAsset("Plot").ToString

    '            .Cells("Col_From_Location_Index").Value = odrAsset("Location_Index").ToString
    '            .Cells("Col_From_Location_Alias").Value = odrAsset("Location_Alias").ToString
    '            .Cells("Col_To_Location_Index").Value = _strDefaultLocation_Index
    '            .Cells("Col_To_Location_Alias").Value = _strDefaultLocation_Alias

    '            .Cells("Col_ItemStatus_Index").Value = odrAsset("ItemStatus_Index").ToString
    '            .Cells("Col_ItemStatus_Des").Value = odrAsset("ItemStatus_Des").ToString

    '            .Cells("Col_New_ItemStatus_Index").Value = _strDefaultStatus_Index
    '            .Cells("Col_New_ItemStatus_Des").Value = _strDefaultStatus_Index

    '            .Cells("Col_Qty_Bal").Value = odrAsset("Qty_Bal").ToString
    '            .Cells("Col_Move_Qty").Value = odrAsset("Qty_Bal").ToString
    '            .Cells("Col_Sku_Package").Value = odrAsset("Package_sku").ToString
    '            .Cells("Col_Weight").Value = odrAsset("Weight_Bal").ToString
    '            .Cells("Col_Volume").Value = odrAsset("Volume_Bal").ToString


    '            .Cells("Col_Sku_Package").Value = odrAsset("Package_sku").ToString
    '            .Cells("Col_Weight").Value = odrAsset("Weight_Bal").ToString
    '            .Cells("Col_Volume").Value = odrAsset("Volume_Bal").ToString

    '            .Cells("Col_Pallet_Qty").Value = odrAsset("Pallet_Qty").ToString
    '            .Cells("Col_Pall_No").Value = odrAsset("Str5").ToString
    '            .Cells("Col_PalletType_Index").Value = odrAsset("PalletType_Index").ToString

    '            .Cells("Col_Tag_No").Value = odrAsset("Tag_No").ToString

    '            .Cells("Col_Customer_Name").Value = odrAsset("Customer_Name").ToString
    '            .Cells("Col_CustomerIndex").Value = odrAsset("Customer_Index").ToString


    '            .Cells("Col_Mfg_Date").Value = CDate(odrAsset("Mfg_Date").ToString).ToString("dd/MM/yyyy")
    '            .Cells("Col_Exp_Date").Value = CDate(odrAsset("Exp_Date").ToString).ToString("dd/MM/yyyy")

    '            If dtpTransferStatus_Date.Value >= CDate(odrAsset("Exp_Date")) Then
    '                .Cells("Col_Exp_Date").Style.BackColor = Color.Pink
    '            End If

    '            .Cells("Col_Order_Index").Value = odrAsset("Order_Index").ToString
    '            .Cells("Col_OrderItem_Index").Value = odrAsset("OrderItem_Index").ToString
    '            .Cells("Col_Lot_No").Value = odrAsset("Lot_No").ToString

    '            'Column for SN Mode
    '            .Cells("col_serial_No").Value = odrAsset("serial_No").ToString
    '            .Cells("col_serial_No").ReadOnly = True
    '            .Cells("col_serial_No").Style.BackColor = Color.Gainsboro

    '            .Cells("col_Asset_No").Value = odrAsset("Asset_No").ToString
    '            .Cells("col_AssetLocationBalance_Index").Value = odrAsset("AssetLocationBalance_Index").ToString

    '            .Cells("col_Brand").Value = odrAsset("Brand_Des").ToString
    '            .Cells("col_Model").Value = odrAsset("Model_Des").ToString

    '            'column for TSC
    '            .Cells("col_Str1").Value = odrAsset("Str1").ToString
    '            .Cells("col_Str2").Value = odrAsset("Str2").ToString

    '        End With
    '        Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
    '        objdelPick.UPDATE_RESERV_ASSETLOCATIONBALANCE(odrAsset("AssetLocationBalance_Index").ToString, 1)

    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

#End Region

#Region "   Combobox   "

    Private Sub LoadDocumentType()

        Dim objDocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim odtDocumentType As New DataTable

        Try
            objDocumentType.getDocumentType(Me._intDefaultProcess_ID)
            odtDocumentType = objDocumentType.DataTable
            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = odtDocumentType
            End With
            cboDocumentType.SelectedIndex = 0
        Catch ex As Exception
            Throw ex

        End Try
    End Sub

    Private Sub LoadItemStatus()
        Try
            Dim omsItemStatus As New ms_ItemStatus(ms_DocumentType.enuOperation_Type.SEARCH)
            Dim odtItemStatus As New DataTable

            omsItemStatus.SearchData_Click("", "")
            odtItemStatus = omsItemStatus.GetDataTable

            With Col_New_ItemStatus_Des
                .DisplayMember = "Description"
                .ValueMember = "ItemStatus_Index"
                .DataSource = odtItemStatus
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

#Region "   GetData   "

    Private Sub GetTransferOwnerHeader(ByVal pstrTransferOwner_Index As String)

        ' *** Lock Customer :  Cannot Edit ***

        Dim objClassDB As New TransferOwnerTransaction
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getTransferOwnerHeader(pstrTransferOwner_Index)
            objDT = objClassDB.DataTable
            Dim StrCustomer_Name As String = ""


            If objDT.Rows.Count > 0 Then

                With objDT.Rows(0)

                    StrCustomer_Name = .Item("Title").ToString & " " & .Item("Customer_Name").ToString
                    txtTransferStatus_No.Text = .Item("TransferOwner_No").ToString
                    TransferOwner_Index = .Item("TransferOwner_Index").ToString
                    txtComment.Text = .Item("Comment").ToString
                    txtRef_No1.Text = .Item("Ref_No1").ToString
                    txtRef_No2.Text = .Item("Ref_No2").ToString
                    dtpTransferStatus_Date.Value = .Item("TransferOwner_Date").ToString
                    txtTimes.Text = .Item("Times").ToString
                    Me.txtCustomer_Id.Tag = .Item("Customer_Index").ToString
                    Me.txtCustomer_Id.Text = .Item("Customer_Id").ToString


                    txtCustomer_IdNew.Text = .Item("New_Customer_Id").ToString
                    txtCustomer_IdNew.Tag = .Item("New_Customer_Index").ToString
                    txtCustomer_NameNew.Text = .Item("New_Title").ToString & " " & .Item("New_Customer_Name").ToString


                    'TSC
                    Dim oCustomer_Ship As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
                    Dim odt As New DataTable

                    oCustomer_Ship.SelectFor_EditData_Click(.Item("Str1").ToString)
                    odt = oCustomer_Ship.GetDataTable

                    'If odt.Rows.Count > 0 Then
                    '    Me.txtCustomer_Ship_Name.Text = odt.Rows(0)("Company_Name").ToString
                    '    Me.txtCustomer_Ship_Name.Tag = odt.Rows(0)("Customer_Shipping_index").ToString
                    'End If

                    Me.cboDocumentType.SelectedValue = .Item("DocumentType_Index").ToString
                End With
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub GetTransferOwnerLocation(ByVal pstrTransferOwner_Index As String)
        Try
            Dim objTran As New TransferOwnerTransaction
            objTran.GetTransferOwnerLocation(pstrTransferOwner_Index)
            Dim odtTransfer As New DataTable
            odtTransfer = objTran.DataTable
            grdTransferStatusLocation.DataSource = odtTransfer

            'For i As Integer = i To odtTransfer.Rows.Count - 1
            '    With grdTransferStatusLocation.Rows(i)
            '        If odtTransfer.Rows(i).Item("AssetLocationBalance_Index").ToString <> "" Then
            '            '.Cells("col_serial_No").Value = odtTransfer.Rows(i).Item("AssetSerial_No").ToString
            '            '.Cells("col_serial_No").ReadOnly = True
            '            '.Cells("col_serial_No").Style.BackColor = Color.Gainsboro

            '            .Cells("col_Asset_No").Value = odtTransfer.Rows(i).Item("Asset_No").ToString
            '            .Cells("col_Asset_No").ReadOnly = False
            '            .Cells("col_Asset_No").Style.BackColor = Color.White
            '        Else
            '            .Cells("col_serial_No").Value = ""
            '            .Cells("col_Asset_No").Value = ""
            '        End If
            '    End With
            'Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    'Private Sub GetAssetLocationBalance(ByVal pstrFiledName As String, ByVal pstrValuseName As String)
    '    Dim objAsset As New tb_AssetLocationBalance

    '    Dim StrFiledName As String = ""
    '    Dim StrFiledNameShow As String = ""
    '    Dim StrValuseName As String = pstrValuseName
    '    Try
    '        Select Case pstrFiledName.ToLower()
    '            Case "txtserialno"
    '                StrFiledName = "Serial_No"
    '                StrFiledNameShow = "Serial No"
    '            Case "txtassetno"
    '                StrFiledName = "Asset_No"
    '                StrFiledNameShow = "Asset No"
    '        End Select

    '        objAsset.getAssetData(" WHERE  " & StrFiledName & " = '" & StrValuseName.Replace("'", "''").Trim & "' AND Customer_Index = '" & Me.txtCustomer_Id.Tag & "' AND Status > 0")
    '        Dim odtAsset As DataTable = objAsset.GetDataTable

    '        If odtAsset.Rows.Count = 0 Then

    '            W_MSG_Information(" ไม่พบข้อมูล  " & StrFiledNameShow & " " & StrValuseName & " กรุณาป้อนข้อมูลใหม่")
    '            txtSerial_No.Text = ""
    '            txtAssetNo.Text = ""
    '            Exit Sub
    '        Else
    '            If CInt(odtAsset.Rows(0).Item("ReserveQty")) = 1 Then

    '                Dim objTFF As New TransferStatusTransaction
    '                Dim odtTFF As DataTable
    '                objTFF.GetTransferNo(" AND TFSL." & StrFiledName & " = '" & StrValuseName.Replace("'", "''").Trim & " '")
    '                odtTFF = objTFF.GetDataTable

    '                If odtTFF.Rows.Count > 0 Then
    '                    W_MSG_Information(StrFiledNameShow & " " & StrValuseName & " กำลังทำรายการอยู่ที่ใบโอนเลขที่ " & odtTFF.Rows(0).Item("TransferStatus_No").ToString)
    '                Else
    '                    W_MSG_Information(StrFiledNameShow & " " & StrValuseName & " กำลังทำรายการอื่นอยู่")
    '                End If

    '                txtSerial_No.Text = ""
    '                txtAssetNo.Text = ""

    '                Exit Sub
    '            Else
    '                'Alert Auto Return Borrow
    '                Dim oBorrowTransaction As New TransferStatusTransaction
    '                Dim odtBorrowTransaction As New DataTable

    '                oBorrowTransaction.CheckDuplicateBorrow(" AND BL." & StrFiledName & " = '" & StrValuseName.Replace("'", "''").Trim & "' AND B.Customer_Index = '" & Me.txtCustomer_Id.Tag & "' ")
    '                odtBorrowTransaction = oBorrowTransaction.GetDataTable

    '                If odtBorrowTransaction.Rows.Count > 0 Then
    '                    If W_MSG_Confirm("พบรายการนี้ ยังคงค้างอยู่ในใบยืมเลขที่ " & odtBorrowTransaction.Rows(0)("Borrow_No").ToString & vbNewLine & "  ระบบจะทำการคืนให้อัตโนมัติ ท่านต้องการทำรายการต่อหรือไม่ ") = Windows.Forms.DialogResult.No Then
    '                        Exit Sub
    '                    End If
    '                End If

    '                'Display Data Serial on Grid
    '                ShowDataGridView(odtAsset)
    '                txtSerial_No.Text = ""
    '                txtAssetNo.Text = ""

    '            End If
    '        End If


    '    Catch ex As Exception
    '        Throw ex
    '    End Try
    'End Sub

#End Region

#Region "   Load Config   "

    Private Sub config_Transfer()
        Dim objClassDB As New tb_TransferStatus
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "Ref_No1"
                            'lblRef_No1.Visible = False
                            'txtRef_No1.Visible = False
                        Case "Str1"
                            'lblStr1.Visible = False
                            'txtStr1.Visible = False
                        Case "Str3"
                            'grdTransferStatusLocation.Columns("ColRef1").Visible = False
                        Case "Str4"
                            'grdTransferStatusLocation.Columns("ColRef2").Visible = False
                    End Select

                    i = i + 1
                Next

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Private Sub config_TransferItem()
        Dim objClassDB As New tb_TransferStatusLocation
        Dim objDT As DataTable = New DataTable
        '  Dim objDr As DataRow

        Try
            objDT = objClassDB.getFieldConfig()

            For Each odr As DataRow In objDT.Rows
                If grdTransferStatusLocation.Columns.Contains(odr("Controls_Name").ToString) Then
                    grdTransferStatusLocation.Columns(odr("Controls_Name").ToString).Visible = False
                End If
            Next

            'If objDT.Rows.Count > 0 Then
            '    For Each objDr In objDT.Rows
            '        Select Case Trim(objDr("Field_Name")).ToString
            '            Case "Str1"
            '                grdTransferStatusLocation.Columns("ColPall1").Visible = False
            '            Case "Str2"
            '                grdTransferStatusLocation.Columns("ColMfg").Visible = False
            '            Case "Str3"
            '                grdTransferStatusLocation.Columns("ColExp").Visible = False
            '            Case "PLot"
            '                grdTransferStatusLocation.Columns("PLot").Visible = False
            '        End Select
            '    Next
            'End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

#End Region

#Region "   TextBox   "

    Private Sub txtSerialNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSerial_No.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                _isSerail = True
                If txtSerial_No.Text.Trim = "" Then
                    W_MSG_Information_ByIndex(400006)
                    txtSerial_No.Text = ""
                    Exit Sub
                End If

                If PickByAsset_Serial(" AND AssetSerial_No = '" & txtSerial_No.Text.Trim.Replace("'", "''").ToString & "' AND Qty_Bal > 0  ") = True Then
                    Me.txtAsset_No.Text = ""
                    Me.txtSerial_No.Text = ""
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Function PickByAsset_Serial(ByVal pstrSQLWhere As String) As Boolean
        Try
            Dim objPicking As New PICKING(PICKING.enmPicking_Type.SERIAL)
            ' Dim Serial_No As String = 

            Dim strSqlCondition As String = pstrSQLWhere
            Dim odtPickAsset As New DataTable
            odtPickAsset = objPicking.SEARCHASSETLOCATIONBALANCE_PICKING(strSqlCondition)

            'If Not (odtPickAsset Is Nothing) Then
            '    If odtPickAsset.Rows.Count <= 0 Then
            '        W_MSG_Information_ByIndex(400004)
            '        Exit Sub
            '    End If

            'If odtPickAsset.Rows.Count > 0 Then
            '    '************************** BEGIN PICKING **************************
            '    '--- LocationBalance Assign Condition For Pick LocationBalance
            '    '--- AssetLocationBalance Assign Property AssetLocationBalance For Pick AssetLocationBalance

            '    Dim strSku_Index As String = odtPickAsset.Rows(0).Item("Sku_Index").ToString
            '    Dim strPackage_Index As String = odtPickAsset.Rows(0).Item("Package_Index").ToString
            '    Dim strLocationBalance_Index As String = odtPickAsset.Rows(0).Item("LocationBalance_Index").ToString
            '    _AssetLocationBalance_Index = odtPickAsset.Rows(0).Item("AssetLocationBalance_Index").ToString
            '    _Asset_No = odtPickAsset.Rows(0).Item("Asset_No").ToString
            '    _AssetSerial_No = odtPickAsset.Rows(0).Item("AssetSerial_No").ToString


            '    objPicking = New WMS_STD_Master_Datalayer.PICKING(WMS_STD_Master_Datalayer.PICKING.enmPicking_Type.SERIAL, _AssetLocationBalance_Index, strLocationBalance_Index, strSku_Index, strPackage_Index, 1, "") '--- PICK 1 FOR SERIAL
            '    If objPicking.CHEK_QTY_BALANCE_Asset() = False Then Exit Function

            '    setDataSoucreItemLocation(objPicking.fnPICKING())

            '    Return True
            'Else
            '    'Not Found
            '    Select Case _isSerail
            '        Case True
            '            objPicking.CHECKSERIALASSET_STATTUS_PICKING(True, " AND AssetSerial_No = '" & txtSerial_No.Text.Trim.Replace("'", "''").ToString & "'")

            '        Case False
            '            objPicking.CHECKSERIALASSET_STATTUS_PICKING(False, " AND Asset_No = '" & txtAsset_No.Text.Trim.Replace("'", "''").ToString & "'")

            '    End Select


            '    Me.txtAsset_No.Text = ""
            '    Me.txtSerial_No.Text = ""

            '    Return False
            'End If

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Private Sub txtAssetNo_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtAsset_No.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                _isSerail = False
                If txtAsset_No.Text.Trim = "" Then
                    W_MSG_Information_ByIndex(400007)
                    txtAsset_No.Text = ""
                    Exit Sub
                End If

                If PickByAsset_Serial(" AND Asset_No = '" & txtAsset_No.Text.Trim.Replace("'", "''").ToString & "' AND Qty_Bal > 0 ") = True Then
                    Me.txtAsset_No.Text = ""
                    Me.txtSerial_No.Text = ""
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

#End Region

    'Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Dim frm As New frmReportViewerMain
    '    frm.Report_Name = "rptTranfer"
    '    frm.Document_Index = TransferStatus_Index
    '    frm.Show()
    'End Sub

    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            If txtCustomer_Id.Tag = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If


            Dim frm As New frmPicking_Reserv_V4(Me.cboDocumentType.SelectedValue.ToString, Me._TransferOwner_Index, frmPicking_Reserv_V4.Operation.TransferOwner)
            Dim odtSelected As New DataTable
            frm.DocumentPlan_Index = _TransferOwner_Index
            frm.DocumentPlan_Process = 50
            frm.IS_TransferOwner = True

            frm.Customer_Id = txtCustomer_Id.Text
            frm.Customer_Index = txtCustomer_Id.Tag
            frm.Customer_Name = txtCustomer_Name.Text
            frm.WithDraw_Date = Me.dtpTransferStatus_Date.Value


            frm.ShowDialog()

            Dim dtLocationBalance As DataTable = frm.objTmpWithDrawItem

            If dtLocationBalance Is Nothing Then
                Exit Sub
            End If

            dtLocationBalance.AcceptChanges()


            _AssetLocationBalance_Index = ""
            _Asset_No = ""
            _AssetSerial_No = ""

            'setDataSoucreItemLocation(dtLocationBalance)

            frm.Close()

            GetTransferOwnerLocation(_TransferOwner_Index)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub setDataSoucreItemLocation(ByVal dtLocationBalance As DataTable)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub
            Dim i As Integer = 0

            With dtLocationBalance.Columns
                If Not .Contains("AssetLocationBalance_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetLocationBalance_Index", GetType(String)))
                End If
                If Not .Contains("TransferStatusLocation_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("TransferStatusLocation_Index", GetType(String)))
                End If
                If Not .Contains("Asset_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Asset_No", GetType(String)))
                End If
                If Not .Contains("AssetSerial_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetSerial_No", GetType(String)))
                End If
                If Not .Contains("Old_ItemStatus_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Index", GetType(String)))
                End If
                If Not .Contains("Old_ItemStatus_Description") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Description", GetType(String)))
                End If
                If Not .Contains("New_ItemStatus_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("New_ItemStatus_Index", GetType(String)))
                End If
                If Not .Contains("From_Location_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("From_Location_Index", GetType(String)))
                End If
                If Not .Contains("From_Location") Then
                    dtLocationBalance.Columns.Add(New DataColumn("From_Location", GetType(String)))
                End If
                If Not .Contains("To_Location") Then
                    dtLocationBalance.Columns.Add(New DataColumn("To_Location", GetType(String)))
                End If
                If Not .Contains("To_Location_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("To_Location_Index", GetType(String)))
                End If

                If Not .Contains("Plan_Process") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))
                End If
                If Not .Contains("DocumentPlan_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
                End If
                If Not .Contains("DocumentPlan_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_Index", GetType(String)))
                End If
                If Not .Contains("DocumentPlanItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
                End If
                If Not .Contains("new_Plot") Then
                    dtLocationBalance.Columns.Add(New DataColumn("new_Plot", GetType(String)))
                End If
            End With

            If dtLocationBalance Is Nothing Then Exit Sub


            If Me.grdTransferStatusLocation.DataSource Is Nothing Then
                For Each odr As DataRow In dtLocationBalance.Rows
                    odr("Plan_Process") = _Plan_Process
                    odr("DocumentPlan_No") = _DocumentPlan_No
                    odr("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                    odr("DocumentPlan_Index") = _DocumentPlan_Index
                    odr("new_Plot") = odr("Plot")

                    odr("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                    odr("Asset_No") = _Asset_No
                    odr("AssetSerial_No") = _AssetSerial_No
                    odr("TransferStatusLocation_Index") = odr("TransferStatusLocation_Index").ToString

                    odr("Old_ItemStatus_Index") = odr("ItemStatus_Index").ToString
                    odr("Old_ItemStatus_Description") = odr("ItemStatus_Description").ToString

                    odr("Old_ItemStatus_Description") = odr("ItemStatus_Description").ToString
                    If _strDefaultStatus_Index.Trim <> "" Then
                        odr("New_ItemStatus_Index") = _strDefaultStatus_Index
                    Else
                        odr("New_ItemStatus_Index") = odr("ItemStatus_Index").ToString
                    End If

                    odr("From_Location_Index") = odr("Location_Index").ToString
                    odr("From_Location") = odr("Location_Alias").ToString

                    If _strDefaultLocation_Index.Trim <> "" Then
                        odr("To_Location_Index") = _strDefaultLocation_Index
                        odr("To_Location") = _strDefaultLocation_Alias
                    Else
                        odr("To_Location_Index") = odr("Location_Index").ToString
                        odr("To_Location") = odr("Location_Alias").ToString
                    End If

                    odr("str1") = txtRef_No1.Text
                    odr("str2") = txtRef_No2.Text
                Next
                Me.grdTransferStatusLocation.DataSource = dtLocationBalance
            Else
                Dim odtTempItemLocation As New DataTable
                odtTempItemLocation = Me.grdTransferStatusLocation.DataSource
                For Each odrTemp As DataRow In dtLocationBalance.Rows
                    odrTemp("Plan_Process") = _Plan_Process
                    odrTemp("DocumentPlan_No") = _DocumentPlan_No
                    odrTemp("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                    odrTemp("DocumentPlan_Index") = _DocumentPlan_Index
                    odrTemp("new_Plot") = odrTemp("Plot")

                    odrTemp("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                    odrTemp("Asset_No") = _Asset_No
                    odrTemp("AssetSerial_No") = _AssetSerial_No
                    odrTemp("TransferStatusLocation_Index") = odrTemp("TransferStatusLocation_Index").ToString

                    odrTemp("Old_ItemStatus_Index") = odrTemp("ItemStatus_Index").ToString
                    odrTemp("Old_ItemStatus_Description") = odrTemp("ItemStatus_Description").ToString


                    If _strDefaultStatus_Index.Trim <> "" Then
                        odrTemp("New_ItemStatus_Index") = _strDefaultStatus_Index
                    Else
                        odrTemp("New_ItemStatus_Index") = odrTemp("ItemStatus_Index").ToString
                    End If

                    odrTemp("From_Location_Index") = odrTemp("Location_Index").ToString
                    odrTemp("From_Location") = odrTemp("Location_Alias").ToString

                    If _strDefaultLocation_Index.Trim <> "" Then
                        odrTemp("To_Location_Index") = _strDefaultLocation_Index
                        odrTemp("To_Location") = _strDefaultLocation_Alias
                    Else
                        odrTemp("To_Location_Index") = odrTemp("Location_Index").ToString
                        odrTemp("To_Location") = odrTemp("Location_Alias").ToString
                    End If

                    odrTemp("str1") = txtRef_No1.Text
                    odrTemp("str2") = txtRef_No2.Text

                Next


                '  odtTempItemLocation.Merge(dtLocationBalance)
                dtLocationBalance.Merge(odtTempItemLocation)
                Me.grdTransferStatusLocation.DataSource = dtLocationBalance


            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub LoadDefault_ItemStatusAndLocation()

        If Me.cboDocumentType.SelectedValue Is Nothing Then Exit Sub

        If Me.cboDocumentType.SelectedValue.ToString.Trim = "" Then Exit Sub


        Dim strDocumenytType_Index As String = Me.cboDocumentType.SelectedValue

        Dim objDocumentType As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim odtDocumentType_ItemStatusAndLocation As DataTable

        Try
            objDocumentType.getStatusAndLocationByDocumentType(_intDefaultProcess_ID, strDocumenytType_Index)
            odtDocumentType_ItemStatusAndLocation = objDocumentType.GetDataTable

            If odtDocumentType_ItemStatusAndLocation.Rows.Count > 0 Then
                _strDefaultStatus_Index = odtDocumentType_ItemStatusAndLocation.Rows(0)("ItemStatus_Index").ToString
                _strDefaultItemStatus = odtDocumentType_ItemStatusAndLocation.Rows(0)("ItemStatusDes").ToString

                _strDefaultLocation_Index = odtDocumentType_ItemStatusAndLocation.Rows(0)("Location_Alias").ToString
                _strDefaultLocation_Alias = odtDocumentType_ItemStatusAndLocation.Rows(0)("Location_Alias").ToString
            End If
            '' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objDocumentType = Nothing
            odtDocumentType_ItemStatusAndLocation = Nothing
        End Try

    End Sub

    Private Sub grdTransferStatusLocation_EditingControlShowing(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdTransferStatusLocation.EditingControlShowing

        Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
            Case "Col_New_ItemStatus_Des"
                If TypeOf e.Control Is ComboBox Then
                    Dim comboBoxColumn As DataGridViewComboBoxColumn = grdTransferStatusLocation.Columns("Col_New_ItemStatus_Des")
                    If (Me.grdTransferStatusLocation.CurrentCellAddress.X = comboBoxColumn.DisplayIndex) Then
                        Dim cboItemStatus As ComboBox = CType(e.Control, ComboBox)
                        If cboItemStatus IsNot Nothing Then
                            AddHandler cboItemStatus.SelectedIndexChanged, New EventHandler(AddressOf Me.Select_ItemStatus)
                        End If
                    End If
                End If
        End Select

    End Sub

    Private Sub Select_ItemStatus(ByVal sender As Object, ByVal e As System.EventArgs)

        If CType(sender, ComboBox).SelectedValue IsNot Nothing Then
            Me.grdTransferStatusLocation.Rows(Me.grdTransferStatusLocation.CurrentCellAddress.Y).Cells("Col_New_ItemStatus_Des").Tag = CType(sender, ComboBox).SelectedValue
        End If

    End Sub

    Private Sub btnCustomer_Ship_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If txtCustomer_Id.Tag = "" Then
            W_MSG_Information("กรุณาเลือกลูกค้า")
            Exit Sub
        End If

        Dim ofrm As New frmCusship_Popup
        ofrm.Customer_ID = Me.txtCustomer_Id.Text
        ofrm.Customer_Name = Me.txtCustomer_Name.Text
        ofrm.Customer_Index = Me.txtCustomer_Id.Tag


        ofrm.ShowDialog()

        'Me.txtCustomer_Ship_Name.Text = ofrm.strCustomer_Shippingr_Name
        'Me.txtCustomer_Ship_Name.Tag = ofrm.Customer_Shipping_Index

        'If Me.txtCustomer_Ship_Name.Text <> "" Then
        '    Me.txtSerial_No.Focus()
        'End If

    End Sub


    Private Sub cboDocumentType_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectionChangeCommitted
        LoadDefault_ItemStatusAndLocation()
    End Sub

    Private Sub Config_AssetTransferLocation()
        Dim objClassDB As New tb_TransferStatusLocation
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objDT = objClassDB.getFieldConfig_Asset()
            If objDT.Rows.Count > 0 Then
                For Each objDr In objDT.Rows
                    grdTransferStatusLocation.Columns(objDr("Controls_Name").ToString).Visible = False
                Next

            End If
        Catch ex As Exception
            W_MSG_Information(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub


    Private Sub Config_AssetTransfer()
        Dim objClassDB As New tb_TransferStatus
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try
            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                For Each objDr In objDT.Rows
                    Select Case objDr("Field_Name").ToString.ToUpper
                        Case "Ref1".ToUpper
                            lblRef_No1.Visible = False
                            txtRef_No1.Visible = False

                        Case "Ref2".ToUpper
                            lblRef_No2.Visible = False
                            txtRef_No2.Visible = False

                            'Case "str1".ToUpper
                            '    lblStr1.Visible = False
                            '    txtCustomer_Ship_Name.Visible = False
                            '    btnCustomer_Ship.Visible = False

                        Case "str3".ToUpper
                        Case "str4".ToUpper
                    End Select

                Next

            End If

        Catch ex As Exception
            W_MSG_Information(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub



    Private Sub dtpTransferStatus_Date_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpTransferStatus_Date.ValueChanged
        If grdTransferStatusLocation.Rows.Count < 1 Then Exit Sub

        For iRow As Integer = 0 To grdTransferStatusLocation.Rows.Count - 1
            With grdTransferStatusLocation.Rows(iRow)
                If dtpTransferStatus_Date.Value >= CDate(.Cells("Col_Exp_Date").Value) Then
                    .Cells("Col_Exp_Date").Style.BackColor = Color.Pink
                End If
            End With
        Next
    End Sub

    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try

            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Try
                'TODO : Wait Report
                'Dim frm As New frmReportViewerMain
                'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo(Report_Name, "And TransferStatus_Index ='" & _TransferOwner_Index & "'")
                'frm.ShowDialog()
                Dim frm As New frmReportViewerMain
                Dim oReport As New WMS_STD_TMM_Report.Loading_Report(Report_Name, "And TransferStatus_Index ='" & _TransferOwner_Index & "'")
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()

                '###################################
            Catch ex As Exception
                W_MSG_Error(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmAssetTransfer_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0

        Try
            Dim objDeleteTransfer As New Cl_TransferReserv

            If grdTransferStatusLocation.RowCount > 0 Then

                Dim dtSaveTransfer As New DataTable
                dtSaveTransfer = CType(grdTransferStatusLocation.DataSource, DataTable)
                dtSaveTransfer.AcceptChanges()
                Dim drArrSaveTransfer() As DataRow = dtSaveTransfer.Select("NewItemFlag='1'")
                For Each drSaveWithDraw As DataRow In drArrSaveTransfer
                    objcon.DBExeNonQuery(String.Format("update tb_LocationBalance set LocationBalance_Index = LocationBalance_Index where LocationBalance_Index = '{0}'", drSaveWithDraw("LocationBalance_Index").ToString), objcon.Connection, myTrans)
                    Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                    Dim LocationBalance_Index As String = drSaveWithDraw("LocationBalance_Index").ToString
                    Dim Total_Qty_Reserv As Decimal = CDec(drSaveWithDraw("Total_Qty").ToString)
                    Dim Weight_Reserv As Decimal = CDec(drSaveWithDraw("WeightOut").ToString)
                    Dim Volume_Reserv As Decimal = CDec(drSaveWithDraw("VolumeOut").ToString)
                    Dim ItemQty_Reserv As Decimal = CDec(drSaveWithDraw("QtyItemOut").ToString)
                    Dim Price_Reserv As Decimal = CDec(drSaveWithDraw("Price_Out").ToString)
                    Dim Qty_Reserv As Decimal = CDec(drSaveWithDraw("Qty").ToString)

                    objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 50, Me._TransferOwner_Index, "ออกโดยไม่บันทึก WithdrawAsset", LocationBalance_Index, _
                                     0, 0, 0, 0, 0, 0, _
                                     Total_Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                    If String.IsNullOrEmpty(drSaveWithDraw("TransferOwnerLocation_Index")) = False Then
                        objDeleteTransfer.DeleteTransferItem_Owner(objcon.Connection, myTrans, drSaveWithDraw("TransferOwnerLocation_Index"))
                    End If

                    Dim strDocumentPlanItem_Index As String = drSaveWithDraw("DocumentPlanItem_Index").ToString
                    Dim strPlan_Process As String = drSaveWithDraw("Plan_Process").ToString
                    Dim strDocumentPlan_Index As String = drSaveWithDraw("DocumentPlan_Index").ToString
                    Select Case strPlan_Process
                        Case "10"
                            Dim oWithDrawItem As New TransferOwnerTransaction
                            oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, Qty_Reserv, Total_Qty_Reserv, ItemQty_Reserv, Weight_Reserv, Volume_Reserv, strPlan_Process, objcon.Connection, myTrans)
                        Case "7"

                        Case Else

                    End Select


                Next

            End If

            Select Case objStatusCase
                Case enuOperation_Type.ADDNEW
                    objDeleteTransfer.DeleteTransfer_Owner(objcon.Connection, myTrans, Me.TransferOwner_Index)
                Case enuOperation_Type.EDIT

            End Select

            objDeleteTransfer.UpdateUseDoc_Owner(objcon.Connection, myTrans, Cl_WithdrawReserv.CaseReserve.ClearReserve, Me.TransferOwner_Index)

            myTrans.Commit()



        Catch ex As Exception
            myTrans.Rollback()
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdTransferStatusLocation_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTransferStatusLocation.CellEndEdit
        Try
            Dim Column_Index As String = grdTransferStatusLocation.CurrentCell.ColumnIndex
            'txtEdit_Keypress(sender, grdTransferStatusLocation.CurrentCell.ColumnIndex)
            Select Case Column_Index
                Case Is = grdTransferStatusLocation.Columns("Col_To_Location_Alias").Index
                    If grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value Is Nothing Then Exit Sub
                    Dim oChk_LocationAlias As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    If oChk_LocationAlias.Chk_Alias(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString) = False Then
                        W_MSG_Information_ByIndex("300019")
                        grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                        Exit Sub
                    Else
                        If oChk_LocationAlias.ChkQty_Alias(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString, _tmpTotalQty_PutAway) = False Then
                            W_MSG_Information_ByIndex("300020")
                            grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                            Exit Sub
                        End If
                    End If

                    grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Index").Value = oChk_LocationAlias.getLocation_Index(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString)

                Case Is = grdTransferStatusLocation.Columns("Col_Move_Qty").Index
                    If grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value Is Nothing Then Exit Sub
                    Dim oChk_LocationAlias As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
                    If oChk_LocationAlias.Chk_Alias(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString) = False Then
                        W_MSG_Information_ByIndex("300019")
                        grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                        Exit Sub
                    Else
                        If oChk_LocationAlias.ChkQty_Alias(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString, _tmpTotalQty_PutAway) = False Then
                            W_MSG_Information_ByIndex("300020")
                            grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                            grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value = _tmpTotalQty_PutAway
                            Exit Sub
                        End If
                    End If
                    grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Index").Value = oChk_LocationAlias.getLocation_Index(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString)


            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdTransferStatusLocation_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTransferStatusLocation.CellClick
        Dim objAsset As New tb_AssetLocationBalance
        If e.RowIndex <= -1 Then
            Exit Sub
        End If
        If e.ColumnIndex <= -1 Then
            Exit Sub
        End If

        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
                Case "Col_To_Location_Alias"
                    If (grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value Is Nothing) Or (grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value Is Nothing) Then
                        Exit Sub
                    End If
                    Dim vPackage_Index As String = ""
                    Dim vSku_Index As String = ""
                    Dim vdblRatio As Decimal = 1
                    Dim vdblQty As Decimal = 1
                    vPackage_Index = grdTransferStatusLocation.Rows(e.RowIndex).Cells("col_Package_Index").Value.ToString
                    vSku_Index = grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Sku_Index").Value.ToString
                    _tmpLocation_Alias = grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString
                    vdblQty = CDec(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value.ToString)
                    ' *** Get Retio ***
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    vdblRatio = objRatio.getRatio(vSku_Index, vPackage_Index)
                    objRatio = Nothing

                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    _tmpTotalQty_PutAway = vdblQty * vdblRatio
                    _tmpQty_PutAway = vdblQty

                Case "Col_Move_Qty"


                    If (grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value Is Nothing) Or (grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value Is Nothing) Then
                        Exit Sub
                    End If

                    If (grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value.ToString = "") Then
                        Exit Sub
                    End If


                    Dim vPackage_Index As String = ""
                    Dim vSku_Index As String = ""
                    Dim vdblRatio As Decimal = 1
                    Dim vdblQty As Decimal = 1
                    vPackage_Index = grdTransferStatusLocation.Rows(e.RowIndex).Cells("col_Package_Index").Value.ToString
                    vSku_Index = grdTransferStatusLocation.Rows(e.RowIndex).Cells("col_SKU_Index").Value.ToString

                    If grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value IsNot Nothing Then
                        _tmpLocation_Alias = grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString
                    End If

                    vdblQty = CDec(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value.ToString)
                    ' *** Get Retio ***
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    vdblRatio = objRatio.getRatio(vSku_Index, vPackage_Index)
                    objRatio = Nothing

                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    _tmpTotalQty_PutAway = vdblQty * vdblRatio
                    _tmpQty_PutAway = vdblQty


                Case "col_SKU_New"

                    If txtCustomer_IdNew.Tag Is Nothing Then
                        W_MSG_Information("กรุณาเลือกเจ้าของสินค้าปลายทาง")
                        Exit Sub
                    End If
                    If txtCustomer_IdNew.Tag.ToString = "" Then
                        W_MSG_Information("กรุณาเลือกเจ้าของสินค้าปลายทาง")
                        Exit Sub
                    End If

                    Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.SEARCH, txtCustomer_IdNew.Tag)
                    'If txtCustomer_IdNew.Tag IsNot Nothing Then
                    '    If txtCustomer_IdNew.Tag <> "" Then
                    '        frm.Customer_Index = txtCustomer_IdNew.Tag
                    '    End If
                    'End If

                    frm.ShowDialog()
                    grdTransferStatusLocation.CurrentRow.Cells("col_SKU_Index_New").Value = frm.Sku_Index
                    grdTransferStatusLocation.CurrentRow.Cells("col_SKU_ID_New").Value = frm.Sku_ID
                    grdTransferStatusLocation.CurrentRow.Cells("col_SKU_Name_New").Value = frm.Sku_Des_eng

                    changeSKU_InGrid(grdTransferStatusLocation.CurrentRow.Index)


            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub changeSKU_InGrid(ByVal row_index As Integer)
        Try

            For irow As Integer = 0 To grdTransferStatusLocation.RowCount - 1
                If grdTransferStatusLocation.Rows(irow).Cells("col_SKU_Index").Value = grdTransferStatusLocation.Rows(row_index).Cells("col_SKU_Index").Value Then
                    grdTransferStatusLocation.Rows(irow).Cells("col_SKU_Index_New").Value = grdTransferStatusLocation.Rows(row_index).Cells("col_SKU_Index_New").Value
                    grdTransferStatusLocation.Rows(irow).Cells("col_SKU_ID_New").Value = grdTransferStatusLocation.Rows(row_index).Cells("col_SKU_ID_New").Value
                    grdTransferStatusLocation.Rows(irow).Cells("col_SKU_Name_New").Value = grdTransferStatusLocation.Rows(row_index).Cells("col_SKU_Name_New").Value
                End If
            Next

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnSeachCustomerNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomerNew.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()
            '    *** Recive value ****

            If frm.Customer_Index = "" Then
                Me.txtCustomer_IdNew.Tag = ""
                Me.txtCustomer_IdNew.Text = ""
                Me.txtCustomer_NameNew.Text = ""

            Else
                Me.txtCustomer_IdNew.Tag = frm.Customer_Index
                Me.txtCustomer_IdNew.Text = frm.strCustomer_Name_Id
                Me.txtCustomer_NameNew.Text = frm.customerName
            End If
            ' *********************
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub grdWithDrawPlan_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWithDrawPlan.CellClick
        Try
            Dim CurrentRowPlan_Index As Integer
            If e.RowIndex <= -1 Then Exit Sub
            If grdWithDrawPlan.CurrentRow.Index < 0 Then Exit Sub
            If txtCustomer_Id.Tag Is Nothing Then
                W_MSG_Information("กรุณาเลือกเจ้าของสินค้า")
                Exit Sub
            End If
            If txtCustomer_Id.Tag = "" Then
                W_MSG_Information("กรุณาเลือกเจ้าของสินค้า")
                Exit Sub
            End If

            If txtCustomer_Id.Tag = "" Or txtCustomer_Id.Tag Is Nothing Then
                W_MSG_Information_ByIndex(8)
                Exit Sub
            End If



            CurrentRowPlan_Index = grdWithDrawPlan.CurrentRow.Index

            'Dim frm As New frmPicking_Reserv(Me.cboDocumentType.SelectedValue.ToString)

            Dim frm As New frmPicking_Reserv_V4(Me.cboDocumentType.SelectedValue.ToString, _TransferOwner_Index, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_No").Value.ToString, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_Index").Value.ToString, Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value.ToString, 10, frmPicking_Reserv_V4.Operation.TransferOwner)

            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name

                Case "btn_Reserv"

                    frm.withdraw_index = _TransferOwner_Index
                    frm.Customer_Id = txtCustomer_Id.Text
                    If txtCustomer_Id.Tag Is Nothing Then
                        W_MSG_Information("กรุณาเลือกเจ้าของสินค้า")
                        Exit Sub
                    End If
                    If txtCustomer_Id.Tag = "" Then
                        W_MSG_Information("กรุณาเลือกเจ้าของสินค้า")
                        Exit Sub
                    End If

                    frm.DocumentPlan_Process = 50
                    frm.DocumentPlan_Index = Me._TransferOwner_Index

                    frm.Customer_Index = txtCustomer_Id.Tag
                    frm.Customer_Name = txtCustomer_Name.Text
                    frm.WithDraw_Date = dtpTransferStatus_Date.Value
                    If Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_ID_Plan").Value Is Nothing Then Exit Sub
                    frm.Sku_Id = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_ID_Plan").Value
                    frm.Sku_Index = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_Index_Plan").Value
                    frm.Sku_Name = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Sku_Des_Plan").Value
                    frm.Qty_Reserv = CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value) - CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value)
                    frm.Max_Plan_Reserv = CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value)
                    frm.Package_Index_Begin = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Index_Plan").Value
                    frm.txtQty_Reserv.ReadOnly = False
                    frm.txtQty_Reserv.BackColor = Color.White
                    frm.ItemStatus_Index = ""

                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ItemStatus_Index_Plan").Value, GetType(String)) <> "" Then
                        frm.ItemStatus_Index = grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ItemStatus_Index_Plan").Value.ToString
                    End If
                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Status_Plan").Value, GetType(String)) <> "" Then
                        Dim objClassDB1 As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
                        Dim objDt1 As New DataTable
                        objClassDB1.SelectData_For_Edit(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Status_Plan").Value)
                        objDt1 = objClassDB1.GetDataTable
                        If objDt1.Rows.Count > 0 Then
                            frm.ItemStatus_Index = objDt1.Rows(0).Item("ItemStatus_Index")
                        End If
                    End If


                    _Remark = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("Col_Remark").Value


                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Lot_Plan").Value, GetType(String)) <> "" Then
                        frm.Plot = grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Lot_Plan").Value.ToString
                    Else
                        frm.Plot = ""
                    End If

                    If ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ERP_Location_Plan").Value, GetType(String)) <> "" Then
                        frm.ERP_location = grdWithDrawPlan.Rows(e.RowIndex).Cells("col_ERP_Location_Plan").Value.ToString
                    Else
                        frm.ERP_location = ""
                    End If
                    ' Fixed SKU
                    frm.btnSeachSku.Enabled = False
                    frm.rdbAutoPicking.Checked = True
                    frm.ShowDialog()
                    If frm.objTmpWithDrawItem IsNot Nothing Then
                        frm.objTmpWithDrawItem.AcceptChanges()
                    End If

                    '--- Color 
                    Dim dtLocationBalance As DataTable = frm.objTmpWithDrawItem
                    Dim i As Integer = 0
                    If dtLocationBalance Is Nothing Then
                        Exit Sub
                    End If
                    If dtLocationBalance.Rows.Count > 0 Then

                        'Bo Update 25/06/2011 
                        Dim odtPicked As New DataTable
                        Dim dblTotal_Qty_Picking As Decimal = 0

                        If dtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty >= 0") IsNot Nothing Then
                            dblTotal_Qty_Picking = CDec(dtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty >= 0"))
                        End If

                        odtPicked = Me.grdTransferStatusLocation.DataSource

                        If odtPicked IsNot Nothing Then
                            Dim dblTotal_Qty As Decimal = 0
                            Dim drSelectSum() As DataRow = odtPicked.Select("DocumentPlanItem_Index = '" & Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value & "' AND NewItemFlag = '1' ")
                            If drSelectSum.Length > 0 Then
                                dblTotal_Qty = odtPicked.Compute("SUM(Total_Qty)", "DocumentPlanItem_Index = '" & Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value & "'")
                                Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = Math.Round((dblTotal_Qty / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6) + Math.Round((dblTotal_Qty_Picking / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6)
                            Else
                                Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = Math.Round((dblTotal_Qty_Picking / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6)
                            End If
                        Else
                            Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value = Math.Round((dblTotal_Qty_Picking / Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Ratio").Value), 6)
                        End If
                        '********************

                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Item_Qty").Value = CDec(dtLocationBalance.Compute("sum(QtyItemOut)", "QtyItemOut >= 0"))
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Total_Qty_Paln").Value = CDec(dtLocationBalance.Compute("sum(Total_Qty)", "Total_Qty  >= 0"))

                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Qty").Value = dtLocationBalance.Rows(0)("Sku_PackageDescription").ToString
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Package_Item").Value = dtLocationBalance.Rows(0)("Item_Package").ToString
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Pakage_Qty").Value = dtLocationBalance.Rows(0)("Description").ToString
                    End If



                    If CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Value.ToString) >= CDec(Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_Plan").Value) Then
                        Me.grdWithDrawPlan.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.YellowGreen
                    Else
                        Me.grdWithDrawPlan.Rows(e.RowIndex).DefaultCellStyle.BackColor = Color.Gainsboro
                        Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Qty_WithDraw").Style.BackColor = Color.WhiteSmoke
                    End If

                    Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_No").Style.BackColor = Color.Yellow


                    TransferStatus_Document = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_Plan_Process").Value.ToString
                    Dim strDocumentPlan_No As String = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_No").Value.ToString
                    Dim strDocumentPlanItem_Index As String = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlanItem_Index").Value.ToString
                    Dim strDocumentPlan_Index As String = Me.grdWithDrawPlan.Rows(e.RowIndex).Cells("col_DocumentPlan_Index").Value.ToString

                    Select Case TransferStatus_Document
                        Case status_Document.Packing
                            _Plan_Process = status_Document.Packing
                            _DocumentPlan_No = strDocumentPlan_No
                            _DocumentPlanItem_Index = strDocumentPlanItem_Index
                            _DocumentPlan_Index = strDocumentPlan_Index
                        Case status_Document.SO
                            _Plan_Process = status_Document.SO
                            _DocumentPlan_No = strDocumentPlan_No
                            _DocumentPlanItem_Index = strDocumentPlanItem_Index
                            _DocumentPlan_Index = strDocumentPlan_Index
                    End Select

                    _Reference = ""
                    _Reference2 = ""

                    _AssetLocationBalance_Index = ""
                    _Asset_No = ""
                    _AssetSerial_No = ""
                    _NewItem = 1
                    If SetAUTO_REFERENCE() = 1 Then
                        If txtRef_No1.Text <> "" Then
                            _Reference = txtRef_No1.Text
                            _Reference2 = txtRef_No2.Text
                        End If
                    End If

                    ' setDataSoucreItemLocation(dtLocationBalance)
                    frm.Close()

                    GetTransferOwnerLocation(_TransferOwner_Index)

            End Select



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub setDataSoucreWithdrawItemLocation(ByVal dtLocationBalance As DataTable)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub

            Dim i As Integer = 0
            With dtLocationBalance.Columns
                If Not .Contains("Reference") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Reference", GetType(String)))
                End If
                If Not .Contains("Reference2") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Reference2", GetType(String)))
                End If
                If Not .Contains("Plan_Process") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Plan_Process", GetType(Integer)))
                End If
                If Not .Contains("DocumentPlan_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_No", GetType(String)))
                End If
                If Not .Contains("DocumentPlan_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlan_Index", GetType(String)))

                End If
                If Not .Contains("DocumentPlanItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("DocumentPlanItem_Index", GetType(String)))
                End If
                If Not .Contains("AssetLocationBalance_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetLocationBalance_Index", GetType(String)))
                End If
                If Not .Contains("Asset_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Asset_No", GetType(String)))

                End If
                If Not .Contains("AssetSerial_No") Then
                    dtLocationBalance.Columns.Add(New DataColumn("AssetSerial_No", GetType(String)))
                End If
                If Not .Contains("Seq") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Seq", GetType(Integer)))
                End If
                If Not .Contains("comment") Then
                    dtLocationBalance.Columns.Add(New DataColumn("comment", GetType(String)))
                End If
                If Not .Contains("WithdrawItemLocation_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("WithdrawItemLocation_Index", GetType(String)))
                End If
                If Not .Contains("Status") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Status", GetType(Integer)))
                End If
                If Not .Contains("NewItem") Then
                    dtLocationBalance.Columns.Add(New DataColumn("NewItem", GetType(Integer)))
                End If
                If Not .Contains("Declaration_No_Out") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Declaration_No_Out", GetType(String)))
                End If

                'Consinee_Index
                If Not .Contains("ConsigneeItem_Index") Then
                    dtLocationBalance.Columns.Add(New DataColumn("ConsigneeItem_Index", GetType(String)))
                End If

                'Consinee_Name
                If Not .Contains("Company_Name") Then
                    dtLocationBalance.Columns.Add(New DataColumn("Company_Name", GetType(String)))
                End If
            End With


            Dim Seq As Integer = grdTransferStatusLocation.RowCount + 1
            If Me.grdTransferStatusLocation.RowCount = 0 Then
                For Each odr As DataRow In dtLocationBalance.Rows
                    odr("Plan_Process") = _Plan_Process
                    odr("DocumentPlan_No") = _DocumentPlan_No
                    odr("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                    odr("DocumentPlan_Index") = _DocumentPlan_Index
                    odr("Reference") = _Reference
                    odr("Reference2") = _Reference2

                    odr("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                    odr("Asset_No") = _Asset_No
                    odr("AssetSerial_No") = _AssetSerial_No
                    odr("Seq") = Seq
                    odr("WithdrawItemLocation_Index") = ""
                    odr("comment") = _Remark
                    odr("Status") = -9
                    odr("NewItem") = _NewItem

                    odr("Declaration_No_Out") = txtRef_No1.Text

                    '**************ผู้รับสินค้า*************
                    odr("ConsigneeItem_Index") = "" '_TConsinee_Index
                    odr("Company_Name") = "" '_TConsinee_Name

                    Seq += 1
                Next
                Me.grdTransferStatusLocation.DataSource = dtLocationBalance
            Else
                Dim odtTempItemLocation As New DataTable
                odtTempItemLocation = Me.grdTransferStatusLocation.DataSource
                For Each odrTemp As DataRow In dtLocationBalance.Rows
                    odrTemp("Plan_Process") = _Plan_Process
                    odrTemp("DocumentPlan_No") = _DocumentPlan_No
                    odrTemp("DocumentPlanItem_Index") = _DocumentPlanItem_Index
                    odrTemp("DocumentPlan_Index") = _DocumentPlan_Index
                    odrTemp("Reference") = _Reference
                    odrTemp("Reference2") = _Reference2
                    odrTemp("AssetLocationBalance_Index") = _AssetLocationBalance_Index
                    odrTemp("Asset_No") = _Asset_No
                    odrTemp("AssetSerial_No") = _AssetSerial_No
                    odrTemp("Seq") = Seq
                    odrTemp("WithdrawItemLocation_Index") = ""
                    odrTemp("comment") = _Remark
                    odrTemp("Status") = -9
                    odrTemp("NewItem") = _NewItem

                    '**************เลขที่ใบขนขาออก************
                    odrTemp("Declaration_No_Out") = txtRef_No1.Text

                    '**************เลขที่ใบขนขาออก*************



                    '**************ผู้รับสินค้า*************
                    odrTemp("ConsigneeItem_Index") = "" ' _TConsinee_Index
                    odrTemp("Company_Name") = "" ' _TConsinee_Name


                    Seq += 1
                Next
                odtTempItemLocation.Merge(dtLocationBalance)


            End If
            'Get_SumData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnAddPlan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddPlan.Click
        Try
            If txtCustomer_Id.Tag Is Nothing Then
                W_MSG_Information("กรุณาเลือกเจ้าของสินค้า")
                Exit Sub
            End If
            If txtCustomer_Id.Tag = "" Then
                W_MSG_Information("กรุณาเลือกเจ้าของสินค้า")
                Exit Sub
            End If

            If grdWithDrawPlan.RowCount > 0 Then
                If W_MSG_Confirm_ByIndex(100038) = Windows.Forms.DialogResult.Yes Then
                    For irowDel As Integer = grdWithDrawPlan.RowCount - 1 To 0 Step -1
                        btnDelSO(irowDel)
                    Next
                Else
                    Exit Sub
                End If
            End If

            Dim frm As New frmWithDraw_Plan
            'frm.USE_PACKING_NEW_PRODUCTION = Me._USE_PACKING_NEW_PRODUCTION
            '  frm.StrSONO = SO_Index
            frm.ShowDialog()


            Dim odrPlan As DataRow
            Dim odtPlan As New DataTable

            odtPlan = frm.DataTable
            If odtPlan Is Nothing Then
                Exit Sub
            End If


            For Each odrPlan In odtPlan.Rows
                Dim dblQty_Bal As Decimal = ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(odrPlan("Qty_Bal").ToString, GetType(Decimal))
                LoadPlanWithdraw(odrPlan("Document_Index").ToString, odrPlan("Process_Id").ToString, dblQty_Bal)
            Next
            'cboAutoPicking.Enabled = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub LoadPlanWithdraw(ByVal pstrDocumnet_Index As String, ByVal pintProcess_ID As Integer, Optional ByVal dblQty_Bal As Decimal = 0)
        Try
            Dim strDocumentPlan_Index As String
            Dim dtDataPlan As New DataTable
            strDocumentPlan_Index = "DocumentPlan_Index = '" & pstrDocumnet_Index & "'"
            Dim objDB As New View_LocationBalance

            Select Case pintProcess_ID
                Case status_Document.Packing
                    'If Me._USE_PACKING_NEW_PRODUCTION = False Then
                    '    objDB.SearchWithDraw_PackingItem(strDocumentPlan_Index)
                    '    dtDataPlan = objDB.GetDataTable
                    '    If dblQty_Bal > 0 Then
                    '        For Each drcalQty As DataRow In dtDataPlan.Rows
                    '            drcalQty("Qty_Plan") = drcalQty("Qty_Per_Pack") * dblQty_Bal
                    '        Next
                    '    End If
                    'Else
                    '    objDB.SearchWithDraw_PackingHeader(strDocumentPlan_Index)
                    '    dtDataPlan = objDB.GetDataTable
                    'End If
                Case status_Document.SO
                    objDB.SearchWithDraw_SO(strDocumentPlan_Index)
                    dtDataPlan = objDB.GetDataTable
                    Dim ocus As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
                    ocus.getPopup_Search(" AND Customer_index='" & _Customer_index & "'")
                    Dim dtcus As New DataTable
                    dtcus = ocus.GetDataTable
                    If dtcus.Rows.Count > 0 Then
                        Me.txtCustomer_Id.Tag = dtcus.Rows(0)("Customer_Index")
                        Me.txtCustomer_Id.Text = dtcus.Rows(0)("Customer_Id")
                        Me.txtCustomer_Name.Text = dtcus.Rows(0)("Customer_Name")
                    End If

                Case status_Document.Reserve '15-01-2010 ja add auto reserve
                    objDB.SearchWithDraw_Reserve(strDocumentPlan_Index)
                    dtDataPlan = objDB.GetDataTable
                Case status_Document.Transport
                    objDB.SearchWithDraw_SO(strDocumentPlan_Index)
                    dtDataPlan = objDB.GetDataTable
            End Select

            Show_grdTransferPlan(dtDataPlan)
        Catch ex As Exception
            Throw ex
        End Try


    End Sub
    Sub Show_grdTransferPlan(ByVal dtPlanWithDraw As DataTable)
        Try

            If Me.grdWithDrawPlan.DataSource Is Nothing Then

                Me.grdWithDrawPlan.DataSource = dtPlanWithDraw
            Else
                'Dim i As Integer
                'For i = 0 To dtPlanWithDraw.Rows.Count - 1
                '    Dim odtTempItemLocation As New DataTable
                '    Dim odrTemp As DataRow
                '    odtTempItemLocation = Me.grdWithDrawPlan.DataSource
                '    odrTemp = odtTempItemLocation.NewRow
                '    odrTemp.ItemArray = dtPlanWithDraw.Rows(i).ItemArray.Clone
                '    odtTempItemLocation.Rows.Add(odrTemp)
                'Next

                Dim odtTempItemLocation As New DataTable
                odtTempItemLocation = Me.grdWithDrawPlan.DataSource
                'For Each odrTemp As DataRow In dtPlanWithDraw.Rows
                '    odrTemp("Plan_Process") = _Plan_Process
                'Next
                odtTempItemLocation.Merge(dtPlanWithDraw)


            End If
            'Get_SumDataPlan()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Private Sub btnDelSO(ByVal irowSO As Integer)
        Try
            If grdWithDrawPlan.RowCount <= 0 Then
                Exit Sub
            End If
            Dim strDocumentPlanItem_Index As String = grdWithDrawPlan.Rows(irowSO).Cells("col_DocumentPlanItem_Index").Value.ToString
            Dim strPlan_Process As String = grdWithDrawPlan.Rows(irowSO).Cells("col_Plan_Process").Value.ToString
            Dim intRowPlan As Integer = grdWithDrawPlan.Rows(irowSO).Index

            If grdTransferStatusLocation.Rows.Count = 0 Then
                grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.CurrentRow.Index)
            Else
                If strPlan_Process <> -9 Then
                    For intRow As Integer = (grdTransferStatusLocation.Rows.Count - 1) To 0 Step -1
                        If (strDocumentPlanItem_Index = grdTransferStatusLocation.Rows(intRow).Cells("col_DocumentPlanItem_Index2").Value.ToString) And (strPlan_Process = grdTransferStatusLocation.Rows(intRow).Cells("col_Plan_Process2").Value.ToString) Then
                            DELETE_GRIDWITHDRAWITEM(intRow, True)
                            grdTransferStatusLocation.Rows.RemoveAt(intRow)
                        End If
                    Next
                End If

                grdWithDrawPlan.Rows.RemoveAt(grdWithDrawPlan.CurrentRow.Index)

            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub DELETE_GRIDWITHDRAWITEM(ByVal iRow As Integer, ByVal isDeleteWI As Boolean)
        'Try
        '    If grdWithdrawItemLocation.Rows(iRow).Cells("Col_LocationBalance_Index2").Value IsNot Nothing Then
        '        Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
        '        Dim LocationBalance_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("Col_LocationBalance_Index2").Value.ToString
        '        Dim Qty_Reserv As decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Qty_sku2").Value.ToString)
        '        Dim Weight_Reserv As decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Weight2").Value.ToString)
        '        Dim Volume_Reserv As decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Volume2").Value.ToString)
        '        Dim ItemQty_Reserv As decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("col_ItemQty2").Value.ToString)
        '        Dim Price_Reserv As decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("col_Price2").Value)

        '        'Dong_kk Add Date : 2011/06/7
        '        Dim Package_Index_AC As String = grdWithdrawItemLocation.Rows(iRow).Cells("Col_Package_IndexWithdraw2").Value.ToString
        '        Dim Qty_Recieve_Package_AC As decimal = CDec(grdWithdrawItemLocation.Rows(iRow).Cells("Col_Withdraw_Package_Qty2").Value)
        '        Dim ReserveQty_AC As decimal = Qty_Reserv
        '        Dim ReserveWeight_AC As decimal = Weight_Reserv
        '        Dim ReserveVolume_AC As decimal = Volume_Reserv
        '        Dim ReserveQty_Item_AC As decimal = ItemQty_Reserv
        '        Dim ReserveOrderItem_Price_AC As decimal = Price_Reserv
        '        'objdelPick.InsertLocationBalanceTransaction(PICKING.enmPicking_Action.DELRESERVE, False _
        '        '    , LocationBalance_Index _
        '        '    , 2 _
        '        '    , Me._Withdraw_index _
        '        '    , Package_Index_AC _
        '        '    , Qty_Recieve_Package_AC _
        '        '    , ReserveQty_AC _
        '        '    , ReserveWeight_AC _
        '        '    , ReserveVolume_AC _
        '        '    , ReserveQty_Item_AC _
        '        '    , ReserveOrderItem_Price_AC _
        '        '    , "ลบสินค้าจากหน้าเบิกสินค้า PC")

        '        objdelPick.DELETE_RESERVLOCATIONBALANCE(LocationBalance_Index, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

        '        If grdWithdrawItemLocation.Rows(iRow).Cells("col_AssetLocationBalance_Index").Value IsNot Nothing Then
        '            Dim _AssetLocationBalance_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("col_AssetLocationBalance_Index").Value.ToString
        '            objdelPick.DELETE_RESERV_ASSETLOCATIONBALANCE(_AssetLocationBalance_Index, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)
        '        End If
        '    End If
        '    If isDeleteWI Then
        '        If grdWithdrawItemLocation.Rows(iRow).Cells("col_WithDrawItem_Index2").Value IsNot Nothing Then
        '            Dim strWithDrawItem_Index As String = grdWithdrawItemLocation.Rows(iRow).Cells("col_WithDrawItem_Index2").Value.ToString
        '            Dim objWithDraw As New tb_WithdrawItemLocation_Update
        '            objWithDraw.delete_WithDrawItem(strWithDrawItem_Index)
        '        End If
        '    End If


        '    '--- CHECK DELETE PLAN

        '    With grdWithdrawItemLocation

        '        Dim strDocumentPlanItem_Index As String = .Rows(iRow).Cells("col_DocumentPlanItem_Index2").Value.ToString
        '        Dim strPlan_Process As String = .Rows(iRow).Cells("col_Plan_Process2").Value.ToString
        '        Dim strDocumentPlan_Index As String = .Rows(iRow).Cells("col_DocumentPlan_Index2").Value.ToString
        '        Dim intRowPlan As Integer

        '        If strPlan_Process <> -9 Then

        '            Dim oWithDrawItem As New WithdrawTransaction_Update(WithdrawTransaction_Update.enuOperation_Type.DELETE)
        '            '        '--- จำนวนที่ต้องคืนค่า เมื่อลบ
        '            Dim dblWithDrawWeight As decimal = CDec(.Rows(iRow).Cells("Col_Weight2").Value.ToString)
        '            Dim dblWithDrawVolume As decimal = CDec(.Rows(iRow).Cells("Col_Volume2").Value.ToString)
        '            Dim dblWithDrawQty As decimal = CDec(.Rows(iRow).Cells("Col_Withdraw_Package_Qty2").Value.ToString)
        '            Dim dblWItemQty As decimal = CDec(.Rows(iRow).Cells("col_ItemQty2").Value.ToString)
        '            Dim dblWTotalQty As decimal = CDec(.Rows(iRow).Cells("Col_Qty_sku2").Value.ToString)



        '            If grdWithdrawItemLocation.Rows(iRow).Cells("col_WithDrawItem_Index2").Value.ToString <> "" Then
        '                '--- Update Total_Qty_Withdraw,Qty_Withdraw,Weight_Withdraw,Volume_Withdraw To tb_SalesOrderItem
        '                oWithDrawItem.UpdatePlanDocument_Reserve(strDocumentPlan_Index, strDocumentPlanItem_Index, dblWithDrawQty, dblWTotalQty, dblWItemQty, dblWithDrawWeight, dblWithDrawVolume, strPlan_Process)
        '            Else 'New Item 
        '                If grdWithDrawPlan.RowCount > 0 Then
        '                    If .Rows(iRow).Cells("col_DocumentPlanItem_Index2").Value IsNot Nothing Then
        '                        For intRowPlan = 0 To grdWithDrawPlan.Rows.Count - 1
        '                            If (strDocumentPlanItem_Index = grdWithDrawPlan.Rows(intRowPlan).Cells("col_DocumentPlanItem_Index").Value.ToString) And (strPlan_Process = grdWithDrawPlan.Rows(intRowPlan).Cells("col_Plan_Process").Value.ToString) Then
        '                                '        '--- จำนวนที่ต้องเบิก ก่อนลบจาก WithDraw
        '                                Dim dblPlanQty As decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value.ToString)
        '                                Dim dblPlanItemQty As decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Item_Qty").Value.ToString)
        '                                Dim dblQtyNeed As decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_Plan").Value.ToString)
        '                                Dim dblPlanTotalQty As decimal = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Total_Qty_Paln").Value.ToString)
        '                                ' Dim intProcess_Id As Integer = CDec(grdWithDrawPlan.Rows(intRowPlan).Cells("col_Plan_Process").Value.ToString)

        '                                '        '--- จำนวนที่ต้องเบิกคงเหลือ
        '                                Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value = dblPlanQty - dblWithDrawQty
        '                                Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Item_Qty").Value = dblPlanItemQty - dblWItemQty
        '                                Me.grdWithDrawPlan.Rows(intRowPlan).Cells("col_Total_Qty_Paln").Value = dblPlanTotalQty - dblWTotalQty


        '                                If grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Value < dblQtyNeed Then
        '                                    grdWithDrawPlan.Rows(intRowPlan).DefaultCellStyle.BackColor = Color.Gainsboro
        '                                    grdWithDrawPlan.Rows(intRowPlan).Cells("col_Qty_WithDraw").Style.BackColor = Color.WhiteSmoke
        '                                End If
        '                            End If
        '                        Next
        '                    End If

        '                End If

        '            End If

        '        End If

        '    End With

        'Catch ex As Exception
        '    Throw ex
        'End Try
    End Sub

    Private Sub frmTransferOwner_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransferOwner
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 50)
                    oFunction.SW_Language_Column(Me, Me.grdTransferStatusLocation, 50)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



End Class