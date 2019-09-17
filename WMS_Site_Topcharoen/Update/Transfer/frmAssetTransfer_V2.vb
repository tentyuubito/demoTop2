Imports WMS_STD_TMM_Transfer_Datalayer
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_OUTB_Datalayer
Imports WMS_STD_OUTB_Reserv
Imports WMS_STD_OUTB_Reserv_Datalayer
Imports WMS_STD_OUTB
Imports WMS_STD_Master.W_Language
Imports WMS_STD_TMM_Report


Public Class frmAssetTransfer_V2


    Private _isSerail As Boolean = False
    Dim _tmpLocation_Alias As String = ""
    Dim _tmpTotalQty_PutAway As Decimal = 0
    Dim _tmpQty_PutAway As Decimal = 0

    Private _DEFAULT_SerialNO As Decimal
    Private objStatusCase As enuOperation_Type
    Private _Customer_Index As String

    Sub New(ByVal CaseStatus As enuOperation_Type)
        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        objStatusCase = CaseStatus
    End Sub


#Region "  Property   "
    Private TranNo As String = ""
    Private _intDefaultProcess_ID As Integer = 5

    Private _strDefaultStatus_Index As String = ""

    Private _strDefaultLocation_Index As String = ""

    Private _strDefaultItemStatus As String = ""

    Private _strDefaultLocation_Alias As String = ""

    Public DocumentStatusValue As String = ""

    Private _TransferStatus_Index As String
    Public Property TransferStatus_Index() As String
        Get
            Return _TransferStatus_Index
        End Get
        Set(ByVal value As String)
            _TransferStatus_Index = value
        End Set
    End Property
    Private _TransferStatus_No As String
    Public Property TransferStatus_No() As String
        Get
            Return _TransferStatus_No
        End Get
        Set(ByVal value As String)
            _TransferStatus_No = value
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



#End Region

#Region "   Page Load   "

    Private Sub frmAssetTransferStatus_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language

            oFunction.SwitchLanguage(Me, 5)
            oFunction.SW_Language_Column(Me, Me.grdTransferStatusLocation, 5)


            grdTransferStatusLocation.AutoGenerateColumns = False

            CheckCustomConfig_Use_Serial()

            SetDEFAULT_CUSTOMER_INDEX()

            LoadDocumentType()
            LoadItemStatus()
            LoadDefault_ItemStatusAndLocation()
            Me.getReportName(5)

            GetItemStatus_To_Gridview()

            Me.txtTimes.Text = Now.ToString("HH:mm")

            If objStatusCase = enuOperation_Type.EDIT Then
                txtTransferStatus_No.Text = TranNo
                GetTransferStatusHeader(_TransferStatus_Index)
                GetTransferStatusLocation(_TransferStatus_Index)
                cboPrint.Enabled = True
                btnPrint.Enabled = True
                Select Case DocumentStatusValue
                    Case "-1", "2"
                        Me.grbTransferStatus.Enabled = False
                        Me.btnAddItem.Enabled = False
                        Me.btnDeleteItem.Enabled = False
                        Me.grdTransferStatusLocation.ReadOnly = True
                        Me.btnSave.Enabled = False
                    Case Else
                        Dim objReserve As New Cl_TransferReserv
                        objReserve.UpdateUseDoc(Cl_TransferReserv.CaseReserve.Reserve, _TransferStatus_Index)

                End Select


            ElseIf objStatusCase = enuOperation_Type.ADDNEW Then
                Dim Strtmp As String = ""
                Strtmp = InsertTmpHaeder()

                If Strtmp.ToUpper <> "S" Then
                    W_MSG_Information(Strtmp)
                    Me.Close()
                End If
            End If

            SetBtnCutomer()

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me._Customer_Index.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me._Customer_Index = objDT.Rows(0).Item("Customer_Index").ToString
                Me.txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString
            Else
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Function InsertTmpHaeder() As String
        Try
            Dim oHeaderTransferStatus As New tb_TransferStatus
            With oHeaderTransferStatus
                .TransferStatus_Index = Me.TransferStatus_Index
                .TransferStatus_No = Me.txtTransferStatus_No.Text
                Dim _Transation_Date As DateTime = Format(Me.dtpTransferStatus_Date.Value, "dd/MM/yyyy").ToString()
                .TransferStatus_Date = _Transation_Date
                .Customer_Index = IIf(Me._Customer_Index Is Nothing, "0010000000001", Me._Customer_Index)
                .DocumentType_Index = Me.cboDocumentType.SelectedValue
                If Me.txtCustomer_Ship_Name.Tag Is Nothing Then
                    .Str1 = ""
                Else
                    .Str1 = Me.txtCustomer_Ship_Name.Tag
                End If
            End With

            Me._TransferStatus_Index = oHeaderTransferStatus.InsertTempHesder()

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

    Private Function DefineValue_To_TransferStatusHeader() As tb_TransferStatus
        Dim oHeaderTransferStatus As New tb_TransferStatus
        With oHeaderTransferStatus
            .TransferStatus_Index = Me.TransferStatus_Index
            .TransferStatus_No = Me.txtTransferStatus_No.Text
            Dim _Transation_Date As DateTime = Format(Me.dtpTransferStatus_Date.Value, "dd/MM/yyyy").ToString()
            .TransferStatus_Date = _Transation_Date
            .Customer_Index = Me._Customer_Index
            .Ref_No1 = Me.txtRef_No1.Text
            .Ref_No2 = Me.txtRef_No2.Text
            .Str2 = ""
            .Times = Me.txtTimes.Text
            .Comment = Me.txtComment.Text
            .DocumentType_Index = Me.cboDocumentType.SelectedValue

            If Me.txtCustomer_Ship_Name.Tag Is Nothing Then
                .Str1 = ""
            Else
                .Str1 = Me.txtCustomer_Ship_Name.Tag
            End If


        End With
        Return oHeaderTransferStatus
    End Function

    Private Function CheckData(ByVal pirow As Integer) As Boolean
        Try
            With grdTransferStatusLocation.Rows(pirow)

                'If Val(.Cells("Col_Move_Qty").Value) > Val(.Cells("Col_Qty_Bal").Value) Then
                '    W_MSG_Information("จำนวนที่ย้ายมากกว่าสินค้าในคลัง")

                '    .Cells("Col_Move_Qty").Value = .Cells("Col_Qty_Bal").Value
                '    Return False
                'End If

                If Val(.Cells("Col_Move_Qty").Value) = 0 Then
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
                        If oms_Location.isOverFlow_Qty(.Cells("Col_To_Location_Alias").Value, Val(.Cells("Col_Move_Qty").Value)) = True Then
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

        Try
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            Me._Customer_Index = oUser.GetUserByCustomerDefault()

            If Not String.IsNullOrEmpty(Me._Customer_Index) Then
                Me.getCustomer()
            End If

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            'objDT = Nothing
            'objCustomSetting = Nothing
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
                Me._Customer_Index = ""
                Me.txtCustomer_Id.Text = ""
                Me.txtCustomer_Name.Text = ""

            Else
                Me._Customer_Index = frm.Customer_Index
                Me.getCustomer()
            End If
            ' *********************
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
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



        If grdTransferStatusLocation.Rows.Count = 0 Then
            W_MSG_Information("กรุณาเลือกรายการที่ต้องการลบ")
            Exit Sub
        End If

        Dim objcon As New DBType_SQLServer
        objcon.connectDB()
        Dim myTrans As SqlClient.SqlTransaction = objcon.Connection.BeginTransaction(IsolationLevel.Serializable)
        objcon.SQLServerCommand.Transaction = myTrans
        objcon.SQLServerCommand.CommandTimeout = 0




        Try
            If W_MSG_Confirm("ท่านต้องการลบรายการนี้ ใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then

                objcon.DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", objcon.Connection, myTrans)
                Dim objDB As New TransferStatusTransaction
                If Me.grdTransferStatusLocation.CurrentRow.Cells("col_TransferStatusLocation_Index").Value.ToString <> "" Then
                    objDB.Delete_TransferLocation(Me.grdTransferStatusLocation.CurrentRow.Cells("col_TransferStatusLocation_Index").Value.ToString, objcon.Connection, myTrans, objcon.SQLServerCommand)
                    objDB = Nothing
                End If
                Dim objdelPick As New PICKING(PICKING.enmPicking_Type.CUSTOM)
                Dim LocationBal_Index As String = Me.grdTransferStatusLocation.CurrentRow.Cells("Col_LocationBalance_Index").Value
                Dim Oty_Reserv As Decimal = Val(Me.grdTransferStatusLocation.CurrentRow.Cells("Col_Move_Qty").Value)
                Dim Weight_Reserv As Decimal = Val(Me.grdTransferStatusLocation.CurrentRow.Cells("Col_Weight").Value)
                Dim Volume_Reserv As Decimal = Val(Me.grdTransferStatusLocation.CurrentRow.Cells("Col_Volume").Value)
                Dim ItemQty_Reserv As Decimal = Val(Me.grdTransferStatusLocation.CurrentRow.Cells("col_ItemQty").Value)
                Dim Price_Reserv As Decimal = Val(Me.grdTransferStatusLocation.CurrentRow.Cells("col_Price").Value)

                Dim Package_Index As String = grdTransferStatusLocation.CurrentRow.Cells("col_Package_Index").Value.ToString
                Dim Sku_Index As String = grdTransferStatusLocation.CurrentRow.Cells("col_Sku_Index").Value.ToString
                Dim Ratio As Decimal = 1
                Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                Ratio = objRatio.getRatio(Sku_Index, Package_Index)
                objRatio = Nothing
                Oty_Reserv = Oty_Reserv * Ratio

                objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 5, Me._TransferStatus_Index, "ลบรายการโอนสถานะ", LocationBal_Index, 0, 0, 0, 0, 0, 0, Oty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                If grdTransferStatusLocation.CurrentRow.Cells("col_AssetLocationBalance_Index").Value.ToString <> "" Then
                    Dim _AssetLocationBalance_Index As String = grdTransferStatusLocation.CurrentRow.Cells("col_AssetLocationBalance_Index").Value.ToString
                    objdelPick.DELETE_RESERV_ASSETLOCATIONBALANCE(_AssetLocationBalance_Index, Oty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)
                End If

                grdTransferStatusLocation.Rows.RemoveAt(grdTransferStatusLocation.CurrentRow.Index)
            End If

            myTrans.Commit()

            Call SetBtnCutomer()

        Catch ex As Exception
            myTrans.Rollback()
            W_MSG_Error(ex.Message.ToString)
        Finally
            objcon.disconnectDB()
        End Try
    End Sub

    Private Sub SetBtnCutomer()
        Try
            If grdTransferStatusLocation.RowCount > 0 Then
                Me.btnSeachCustomer.Enabled = False
            Else
                Me.btnSeachCustomer.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If grdTransferStatusLocation.RowCount <= 0 Then
                W_MSG_Information("ไม่พบรายการที่จะทำการบันทึก กรุณาตรวจสอบข้อมูล")
                Exit Sub
            End If

            If Me.txtCustomer_Id.Text.Trim = "" Then
                W_MSG_Information("กรุณาป้อนเลือกข้อมูล" & Me.lblCustomer.Text.Trim)
                Exit Sub
            End If

            For irow As Integer = 0 To grdTransferStatusLocation.Rows.Count - 1
                If CheckData(irow) = False Then
                    Exit Sub
                End If
            Next

            Dim VaridateText As New W_SetValidate()
            Dim tmpMsg As String = ""
            tmpMsg = VaridateText.MessageTextValidate(Me, 5)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""

            tmpMsg = VaridateText.Validate_DataGridView(Me, Me.grdTransferStatusLocation, 5)
            If tmpMsg <> "S" Then
                W_MSG_Information(tmpMsg)
                Exit Sub
            End If
            tmpMsg = ""


            Dim dtpMinOrder_Date As Date
            Dim dtTemp As New DataTable
            dtTemp = CType(grdTransferStatusLocation.DataSource, DataTable)
            dtpMinOrder_Date = CDate(dtTemp.Compute("max(Order_Date)", "Sku_Index <> ''"))

            If CDate(dtpTransferStatus_Date.Value.ToString("dd/MM/yyyy")) < CDate(dtpMinOrder_Date.ToString("dd/MM/yyyy")) Then
                dtpTransferStatus_Date.Value = Now.ToString("dd/MM/yyyy")
                W_MSG_Information(GetMessage_Data("400020") & CDate(dtpMinOrder_Date).ToString("dd/MM/yyyy"))
                Exit Sub
            End If

            Dim objHeader As New tb_TransferStatus
            Dim objLocation(grdTransferStatusLocation.Rows.Count - 1) As tb_TransferStatusLocation
            Dim objms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            Dim objms_ItemStatus As New ms_ItemStatus(ms_Location.enuOperation_Type.SEARCH)
            Dim newTransferStatus_Index As String = ""
            Dim iToLocation_Index As String = ""

            objHeader = DefineValue_To_TransferStatusHeader()

            Dim i As Integer = 0
            Dim objClassDB As New TransferStatusTransaction

            Dim strTransferStatusLocation_Index As String = ""

            If Me.txtTransferStatus_No.Text = "" And objStatusCase = enuOperation_Type.ADDNEW Then
                Dim strWhere As String = ""
                Dim objDocumentNumber As New Sy_DocumentNumber
                objHeader.TransferStatus_No = objDocumentNumber.Auto_DocumentType_Number(objHeader.DocumentType_Index, strWhere, Me.dtpTransferStatus_Date.Value) '(objDocumentNumber.getDocument_Number_Auto("R", "Order_No", "tb_Order"))
                txtTransferStatus_No.Text = objHeader.TransferStatus_No
                objDocumentNumber = Nothing
            End If

            For i = 0 To grdTransferStatusLocation.Rows.Count - 1
                objLocation(i) = New tb_TransferStatusLocation
                With objLocation(i)

                    .TransferStatusLocation_Index = grdTransferStatusLocation.Rows(i).Cells("col_TransferStatusLocation_Index").Value
                    .From_LocationBalance_Index = grdTransferStatusLocation.Rows(i).Cells("Col_LocationBalance_Index").Value
                    .To_LocationBalance_Index = grdTransferStatusLocation.Rows(i).Cells("Col_LocationBalance_Index").Value
                    .Sku_Index = grdTransferStatusLocation.Rows(i).Cells("Col_SKU_Index").Value.ToString
                    .Package_Index = grdTransferStatusLocation.Rows(i).Cells("Col_Package_Index").Value.ToString
                    .Order_Index = grdTransferStatusLocation.Rows(i).Cells("Col_Order_Index").Value.ToString
                    .OrderItem_Index = grdTransferStatusLocation.Rows(i).Cells("Col_OrderItem_Index").Value.ToString

                    .Old_ItemStatus_Index = grdTransferStatusLocation.Rows(i).Cells("Col_ItemStatus_Index").Value
                    .New_ItemStatus_Index = grdTransferStatusLocation.Rows(i).Cells("Col_New_ItemStatus_Des").Value

                    .From_Location_Index = grdTransferStatusLocation.Rows(i).Cells("Col_From_Location_Index").Value
                    .To_Location_Index = grdTransferStatusLocation.Rows(i).Cells("Col_To_Location_Index").Value


                    If Not DBNull.Value.Equals(grdTransferStatusLocation.Rows(i).Cells("Col_Lot_No").Value) Then
                        .Lot_No = grdTransferStatusLocation.Rows(i).Cells("Col_Lot_No").Value
                    Else
                        .Lot_No = ""
                    End If

                    .Plot = grdTransferStatusLocation.Rows(i).Cells("Col_Plot").Value

                    .TAG_Index = grdTransferStatusLocation.Rows(i).Cells("Col_TAG_Index").Value
                    .Tag_No = grdTransferStatusLocation.Rows(i).Cells("Col_Tag_No").Value
                    .Qty = grdTransferStatusLocation.Rows(i).Cells("Col_Move_Qty").Value

                    Dim Ratio As Decimal = 1
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    Ratio = objRatio.getRatio(.Sku_Index, grdTransferStatusLocation.Rows(i).Cells("Col_Package_Index").Value.ToString)
                    objRatio = Nothing
                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    '--- Total_Qty จำนวนของหน่วยหลัก 
                    .Total_Qty = grdTransferStatusLocation.Rows(i).Cells("Col_Total_Qty").Value 'Val(grdTransferStatusLocation.Rows(i).Cells("Col_Move_Qty").Value) * Ratio
                    .Ratio = Ratio

                    '.Total_Qty = Val(grdTransferStatusLocation.Rows(i).Cells("Col_Move_Qty").Value)
                    .Weight = grdTransferStatusLocation.Rows(i).Cells("Col_Weight").Value
                    .Volume = grdTransferStatusLocation.Rows(i).Cells("Col_Volume").Value
                    .Item_Package_Index = grdTransferStatusLocation.Rows(i).Cells("Col_Item_Package_Index").Value.ToString
                    .Item_Qty = grdTransferStatusLocation.Rows(i).Cells("col_ItemQty").Value
                    .Price = grdTransferStatusLocation.Rows(i).Cells("col_Price").Value


                    If Not DBNull.Value.Equals(grdTransferStatusLocation.Rows(i).Cells("Col_PalletType_Index").Value) Then
                        .PalletType_Index = grdTransferStatusLocation.Rows(i).Cells("Col_PalletType_Index").Value
                    Else
                        .PalletType_Index = ""
                    End If


                    .Pallet_Qty = grdTransferStatusLocation.Rows(i).Cells("Col_Pallet_Qty").Value

                    .Serial_No = grdTransferStatusLocation.Rows(i).Cells("col_serial_No").Value
                    .Asset_No = grdTransferStatusLocation.Rows(i).Cells("col_Asset_No").Value

                    .MfgDate = grdTransferStatusLocation.Rows(i).Cells("Col_Mfg_Date").Value
                    .ExpDate = grdTransferStatusLocation.Rows(i).Cells("Col_Exp_Date").Value



                    If Not DBNull.Value.Equals(grdTransferStatusLocation.Rows(i).Cells("Col_Pall_No").Value) Then
                        .PallNo = grdTransferStatusLocation.Rows(i).Cells("Col_Pall_No").Value
                    Else
                        .PallNo = ""
                    End If


                    .ERP_Location_From = grdTransferStatusLocation.Rows(i).Cells("Col_ERP_Location_From").Value.ToString
                    .ERP_Location_TO = grdTransferStatusLocation.Rows(i).Cells("Col_ERP_Location_TO").Value.ToString
                    .Status = 1


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

                    '.Str1 = grdTransferStatusLocation.Rows(i).Cells("col_Str1").Value
                    '.Str2 = grdTransferStatusLocation.Rows(i).Cells("col_Str2").Value


                    .Str3 = Me.txtCustomer_Ship_Name.Tag
                    .Str4 = ""
                    .Str5 = ""
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
                    ' newTransferStatus_Index = objClassDB.SaveTransferStatus(objHeader, objLocation)
                    newTransferStatus_Index = objClassDB.UpdateTransferStatus_V2(objHeader, objLocation)
                Case enuOperation_Type.EDIT
                    newTransferStatus_Index = objClassDB.UpdateTransferStatus_V2(objHeader, objLocation)

            End Select


            If Not newTransferStatus_Index = "" Then
                Me.TransferStatus_Index = newTransferStatus_Index
                Me.GetTransferStatusHeader(_TransferStatus_Index)
                Me.GetTransferStatusLocation(_TransferStatus_Index)
                objStatusCase = enuOperation_Type.EDIT

                W_MSG_Information_ByIndex(1)
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

    Private Sub GetTransferStatusHeader(ByVal pstrTransferStatus_Index As String)

        ' *** Lock Customer :  Cannot Edit ***

        Dim objClassDB As New TransferStatusTransaction
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getTransferStatusHeader(pstrTransferStatus_Index)
            objDT = objClassDB.DataTable
            Dim StrCustomer_Name As String = ""


            If objDT.Rows.Count > 0 Then

                With objDT.Rows(0)

                    StrCustomer_Name = .Item("Title").ToString & " " & .Item("Customer_Name").ToString
                    txtTransferStatus_No.Text = .Item("TransferStatus_No").ToString
                    TransferStatus_Index = .Item("TransferStatus_Index").ToString
                    txtComment.Text = .Item("Comment").ToString
                    txtRef_No1.Text = .Item("Ref_No1").ToString
                    txtRef_No2.Text = .Item("Ref_No2").ToString
                    dtpTransferStatus_Date.Value = .Item("TransferStatus_Date").ToString
                    txtTimes.Text = .Item("Times").ToString
                    Me._Customer_Index = .Item("Customer_Index").ToString
                    Me.txtCustomer_Id.Text = .Item("Customer_Id").ToString

                    'TSC
                    Dim oCustomer_Ship As New ms_Customer_Shipping(ms_Customer_Shipping.enuOperation_Type.SEARCH)
                    Dim odt As New DataTable

                    oCustomer_Ship.SelectFor_EditData_Click(.Item("Str1").ToString)
                    odt = oCustomer_Ship.GetDataTable

                    If odt.Rows.Count > 0 Then
                        Me.txtCustomer_Ship_Name.Text = odt.Rows(0)("Company_Name").ToString
                        Me.txtCustomer_Ship_Name.Tag = odt.Rows(0)("Customer_Shipping_index").ToString
                    End If

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

    Private Sub GetTransferStatusLocation(ByVal pstrTransferStatus_Index As String)
        Try
            Dim objTran As New TransferStatusTransaction
            objTran.GetTransferAssetLocation(pstrTransferStatus_Index)
            Dim odtTransfer As New DataTable
            odtTransfer = objTran.DataTable
            grdTransferStatusLocation.DataSource = odtTransfer

            For i As Integer = i To odtTransfer.Rows.Count - 1
                With grdTransferStatusLocation.Rows(i)
                    If odtTransfer.Rows(i).Item("AssetLocationBalance_Index").ToString <> "" Then
                        '.Cells("col_serial_No").Value = odtTransfer.Rows(i).Item("AssetSerial_No").ToString
                        '.Cells("col_serial_No").ReadOnly = True
                        '.Cells("col_serial_No").Style.BackColor = Color.Gainsboro

                        .Cells("col_Asset_No").Value = odtTransfer.Rows(i).Item("Asset_No").ToString
                        .Cells("col_Asset_No").ReadOnly = False
                        .Cells("col_Asset_No").Style.BackColor = Color.White
                    Else
                        .Cells("col_serial_No").Value = ""
                        .Cells("col_Asset_No").Value = ""
                    End If
                    If odtTransfer.Rows(i).Item("Tag_NoNew").ToString <> "" Then
                        Me.col_Tag_NoNew.Visible = True

                    Else
                        Me.col_Tag_NoNew.Visible = False

                    End If
                End With
            Next
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

    '        objAsset.getAssetData(" WHERE  " & StrFiledName & " = '" & StrValuseName.Replace("'", "''").Trim & "' AND Customer_Index = '" & Me._Customer_Index & "' AND Status > 0")
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

    '                oBorrowTransaction.CheckDuplicateBorrow(" AND BL." & StrFiledName & " = '" & StrValuseName.Replace("'", "''").Trim & "' AND B.Customer_Index = '" & Me._Customer_Index & "' ")
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






    Private Sub btnAddItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddItem.Click
        Try
            If Me._Customer_Index = "" Then
                W_MSG_Information("กรุณาเลือกลูกค้า")
                Exit Sub
            End If

            Dim frm As New frmPicking_Reserv_V4(Me.cboDocumentType.SelectedValue.ToString, Me._TransferStatus_Index, frmPicking_Reserv_V4.Operation.Transfer)
            Dim odtSelected As New DataTable

            frm.DocumentPlan_Process = 5
            frm.DocumentPlan_Index = Me._TransferStatus_Index
            frm.Customer_Id = txtCustomer_Id.Text
            frm.Customer_Index = Me._Customer_Index
            frm.Customer_Name = txtCustomer_Name.Text
            frm.WithDraw_Date = Me.dtpTransferStatus_Date.Value

            If Me.txtCustomer_Ship_Name.Tag IsNot Nothing Then
                frm.Consignee_Index = Me.txtCustomer_Ship_Name.Tag.ToString
            End If

            frm.ShowDialog()

            Dim dtLocationBalance As DataTable = frm.objTmpWithDrawItem

            If dtLocationBalance Is Nothing Then
                Exit Sub
            End If



            Dim ObjConfig As New config_CustomSetting

            dtLocationBalance.Columns.Add("ERP_Location_TO", GetType(String))
            If ObjConfig.getConfig_Key_USE("USE_COPY_ERP_LOCATION_TRANSFERSTATUS") Then
                For Each dr As DataRow In dtLocationBalance.Rows
                    dr("ERP_Location_TO") = dr("ERP_Location")
                Next
            Else

            End If

            dtLocationBalance.AcceptChanges()


            'setDataSoucreItemLocation(dtLocationBalance)



            frm.Close()


            'GetTransferStatusHeader(_TransferStatus_Index)
            GetTransferStatusLocation(_TransferStatus_Index)

            SetBtnCutomer()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub setDataSoucreItemLocation(ByVal dtLocationBalance As DataTable)
        Try
            If dtLocationBalance Is Nothing Then Exit Sub

            'dtLocationBalance.Columns.Add(New DataColumn("AssetLocationBalance_Index", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("TransferStatusLocation_Index", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("Asset_No", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("AssetSerial_No", GetType(String)))

            'dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Index", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("Old_ItemStatus_Description", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("New_ItemStatus_Index", GetType(String)))

            'dtLocationBalance.Columns.Add(New DataColumn("From_Location_Index", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("From_Location", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("To_Location", GetType(String)))
            'dtLocationBalance.Columns.Add(New DataColumn("To_Location_Index", GetType(String)))

            If Not dtLocationBalance.Columns.Contains("str1") Then
                dtLocationBalance.Columns.Add(New DataColumn("str1", GetType(String)))
            End If
            If Not dtLocationBalance.Columns.Contains("str2") Then
                dtLocationBalance.Columns.Add(New DataColumn("str2", GetType(String)))
            End If

            If dtLocationBalance Is Nothing Then Exit Sub


            If Me.grdTransferStatusLocation.DataSource Is Nothing Then
                For Each odr As DataRow In dtLocationBalance.Rows
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

    Private Sub btnCustomer_Ship_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCustomer_Ship.Click

        If Me._Customer_Index = "" Then
            W_MSG_Information("กรุณาเลือกลูกค้า")
            Exit Sub
        End If

        Dim ofrm As New frmCusship_Popup
        ofrm.Customer_ID = Me.txtCustomer_Id.Text
        ofrm.Customer_Name = Me.txtCustomer_Name.Text
        ofrm.Customer_Index = Me._Customer_Index


        ofrm.ShowDialog()

        Me.txtCustomer_Ship_Name.Text = ofrm.strCustomer_Shippingr_Name
        Me.txtCustomer_Ship_Name.Tag = ofrm.Customer_Shipping_Index



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

                        Case "str1".ToUpper
                            lblStr1.Visible = False
                            txtCustomer_Ship_Name.Visible = False
                            btnCustomer_Ship.Visible = False

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

                Dim frm As New frmReportViewerMain
                Dim oReport As New WMS_STD_TMM_Report.Loading_Report(Report_Name, "And TransferStatus_Index ='" & Me.TransferStatus_Index & "'")
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

                    objdelPick.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(objcon.Connection, myTrans, PICKING.enmPicking_Action.DELRESERVE, 5, Me._TransferStatus_Index, "ออกโดยไม่บันทึก Transfer", LocationBalance_Index, _
                                   0, 0, 0, 0, 0, 0, _
                                   Total_Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)

                    If String.IsNullOrEmpty(drSaveWithDraw("TransferStatusLocation_Index")) = False Then
                        objDeleteTransfer.DeleteTransferItem(objcon.Connection, myTrans, drSaveWithDraw("TransferStatusLocation_Index"))
                    End If

                Next
            End If


            Select Case objStatusCase
                Case enuOperation_Type.ADDNEW
                    objDeleteTransfer.DeleteTransfer(objcon.Connection, myTrans, Me.TransferStatus_Index)
                Case enuOperation_Type.EDIT

            End Select

            objDeleteTransfer.UpdateUseDoc(objcon.Connection, myTrans, Cl_WithdrawReserv.CaseReserve.ClearReserve, Me.TransferStatus_Index)

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

                        'If oChk_LocationAlias.isOverFlow_Qty(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString, _tmpTotalQty_PutAway) Then
                        '    W_MSG_Information_ByIndex("300020")
                        '    grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                        '    Exit Sub
                        'End If
                        'If oChk_LocationAlias.isOverFlow_Weight(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString, grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Weight").Value) Then
                        '    W_MSG_Information_ByIndex("กรุณากรอกตำแหน่งใหม่ น้ำหนักเกิน")
                        '    grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                        '    Exit Sub
                        'End If

                        Dim All_QTY As Decimal = 0
                        Dim All_Weight As Decimal = 0

                        Dim dtPutAway As New DataTable
                        dtPutAway = grdTransferStatusLocation.DataSource
                        dtPutAway.AcceptChanges()
                        'Dim obj As Object
                        Dim dr() As DataRow

                        Dim pstrLocation_Alias As String = grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_To_Location_Alias").Value.ToString()
                        Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)

                        'obj = dtPutAway.Compute(" sum(Qty_per_TAG) ", "Location_Alias='" & pstrLocation_Alias & "' ")
                        dr = dtPutAway.Select("To_Location='" & pstrLocation_Alias & "'")
                        If dtPutAway.Rows.Count > 1 Then
                            All_QTY = dtPutAway.Compute(" sum(Qty) ", " To_Location='" & pstrLocation_Alias & "' ")
                            'If oms_Location.isOverFlow_Qty(pstrLocation_Alias, Val(grdAllocate_Qty.CurrentRow.Cells("Location_Qty").Value.ToString)) = True Then
                            If oms_Location.isOverFlow_Qty(pstrLocation_Alias, All_QTY) = True Then
                                W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " พื้นที่ว่างไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                                grdTransferStatusLocation.CurrentRow.Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                            End If

                            All_Weight = dtPutAway.Compute(" sum(WeightOut) ", " To_Location='" & pstrLocation_Alias & "' ")
                            'If oms_Location.isOverFlow_Weight(pstrLocation_Alias, Val(grdAllocate_Qty.CurrentRow.Cells("Col_weight").Value.ToString)) = True Then
                            If oms_Location.isOverFlow_Weight(pstrLocation_Alias, All_Weight) = True Then
                                W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " น้ำหนักที่จุได้ไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                                grdTransferStatusLocation.CurrentRow.Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                            End If

                        Else
                            If oms_Location.isOverFlow_Qty(pstrLocation_Alias, Val(grdTransferStatusLocation.CurrentRow.Cells("Col_Move_Qty").Value.ToString)) = True Then
                                W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " พื้นที่ว่างไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                                grdTransferStatusLocation.CurrentRow.Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                            End If
                            If oms_Location.isOverFlow_Weight(pstrLocation_Alias, Val(grdTransferStatusLocation.CurrentRow.Cells("Col_Weight").Value.ToString)) = True Then
                                W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " น้ำหนักที่จุได้ไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                                grdTransferStatusLocation.CurrentRow.Cells("Col_To_Location_Alias").Value = _tmpLocation_Alias
                            End If
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
                    vdblQty = Val(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value.ToString)
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

                    vdblQty = Val(grdTransferStatusLocation.Rows(e.RowIndex).Cells("Col_Move_Qty").Value.ToString)
                    ' *** Get Retio ***
                    Dim objRatio As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
                    vdblRatio = objRatio.getRatio(vSku_Index, vPackage_Index)
                    objRatio = Nothing

                    ' *****************
                    ' *** Calculate Tatal Qty *** 
                    _tmpTotalQty_PutAway = vdblQty * vdblRatio
                    _tmpQty_PutAway = vdblQty


            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub cboDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentType.SelectedIndexChanged

    End Sub

    Private Sub frmAssetTransfer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigTransfer
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 5)
                    oFunction.SW_Language_Column(Me, Me.grdTransferStatusLocation, 5)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class