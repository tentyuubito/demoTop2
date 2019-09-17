Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_master.W_Function
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_INB_Barcode
Imports WMS_STD_INB_Report

Public Class frmTag_Main_V3

    Private _Printer_Barcode_Name As String = System.Configuration.ConfigurationSettings.AppSettings("Printer_Barcode_Name")
    Private DEFAULT_ViewTagBarcode As String = ""
    Private _USE_ASSIGNEJOB_ITEM As Boolean = False
    Private btApp As BarTender.ApplicationClass
    Private btFormat As BarTender.Format
    Private CheckFrmUSE As Boolean = False

    Private _boolMannaul As Boolean = False

    Private _Status As Integer = 0

    Private Validation As New WMS_STD_INB_Receive.Validate_Tag_Main_V3
#Region "   Property   "

    Enum enmGenTag_Type
        NotGen = 0
        NormalGen = 1
        GenPerQty = 2
    End Enum

    Private _Order_Index As String
    Private _Tag_Index As String

    Public Property Tag_Index() As String
        Get
            Return _Tag_Index
        End Get
        Set(ByVal value As String)
            _Tag_Index = value
        End Set
    End Property

    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property
    Private _DEFAULT_AUTO_TAG As enmGenTag_Type
    Public Property DEFAULT_AUTO_TAG() As enmGenTag_Type
        Get
            Return _DEFAULT_AUTO_TAG
        End Get
        Set(ByVal value As enmGenTag_Type)
            _DEFAULT_AUTO_TAG = value
        End Set
    End Property
    Private _Customer_Index As String
    Public Property Customer_Index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Private _SKU_Index As String
    Public Property Sku_Index() As String
        Get
            Return _SKU_Index
        End Get
        Set(ByVal value As String)
            _SKU_Index = value
        End Set
    End Property




#End Region

    Private Sub getOrder()
        Dim objOrder As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
        Dim odtOrder As DataTable = New DataTable
        Try
            objOrder.getOrderHeader(Me._Order_Index)
            odtOrder = objOrder.DataTable
            txtOrderNo.Text = odtOrder.Rows(0).Item("Order_No").ToString
            dtOrderDate.Value = CDate(odtOrder.Rows(0).Item("Order_Date"))
            txtCustomerName.Text = odtOrder.Rows(0).Item("Customer_Name").ToString
            txtCustomerID.Text = odtOrder.Rows(0).Item("Customer_Id").ToString
            txtOrderType.Text = odtOrder.Rows(0).Item("DocumentType").ToString

            Me._Status = odtOrder.Rows(0).Item("Status")
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Dim objOrder As New tb_Order
            objOrder.UserUseTag(_Order_Index, "NOTUSE")
            Me.Close()

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub frmTag_Main_V2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 1)
            oFunction.SW_Language_Column(Me, Me.grdOrderItem, 1)
            oFunction.SW_Language_Column(Me, Me.grdTag, 1)
            oFunction = Nothing

            Me.grdOrderItem.AutoGenerateColumns = False
            Me.grdTag.AutoGenerateColumns = False
            Me.cboQtyPerTagAuto.SelectedIndex = 0
            Me.cboQtyPerTagManual.SelectedIndex = 0
            'Dim Location As New System.Drawing.Point
            'Location.X = "284" '"352"
            'Location.Y = "72" '"200"
            'Me.Pl_manualTag.Location = Location


            Dim oConfig As New config_CustomSetting
            Me._USE_ASSIGNEJOB_ITEM = oConfig.getConfig_Key_USE("USE_ASSIGNEJOB_ITEM")
            Me.ButtonManagement() 'Me.getOrder()

            Dim objOrder As New tb_Order
            objOrder.Defalt_Search(" Where User_UseTAG = 1 AND Status not in (-1,2) AND Order_Index = '" & _Order_Index & "'", " Order_Index  ")


            If objOrder.DataTable.Rows.Count > 0 Then
                W_MSG_Information("ใบรับ " & Me.txtOrderNo.Text & " มีการใช่งานอยู่")
                Me.Close()
                Exit Sub
            Else
                objOrder.UserUseTag(_Order_Index, "USE")
            End If

            Me.getReportName(101)
            btnSearch_Click_1(sender, e)
            getTAG()
            CheckFrmUSE = True


        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub ButtonManagement()
        Try
            Me.getOrder()
            Select Case Me._Status
                Case -1, 2
                    Me.btnSaveTag.Enabled = False
                    Me.btnCancel.Enabled = False
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

#Region " GetReportName "

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New WMS_STD_Master.config_Report '(enuOperation_Type.SEARCH)
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
    Private Sub getTAG()
        Dim objTAG As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""
        strWhere &= " And Order_Index = '" & _Order_Index & "'"
        strWhere &= " AND Tag_Process_Id = 1 "
        objTAG.getView_Tag_Header(strWhere)
        objDT = objTAG.DataTable

        Me.grdTag.Refresh()
        grdTag.DataSource = objDT
        Dim Irow As Integer = 1
        For Each dr As DataRow In objDT.Rows
            dr("Row") = Irow
            Irow = Irow + 1
        Next

        For i As Integer = 0 To Me.grdTag.Rows.Count - 1
            Select Case Me.grdTag.Rows(i).Cells("Col_TAG_Status_Index").Value
                Case "2"
                    Me.grdTag.Rows(i).Cells("col_Status").Style.BackColor = Color.LightGreen
                    Me.grdTag.Rows(i).Cells("col_TAG_NO").Style.BackColor = Color.LightGreen
                Case Else
                    Select Case Me.grdTag.Rows(i).Cells("Col_Print_Status").Value
                        Case 1
                            Me.grdTag.Rows(i).Cells("col_Status").Style.BackColor = Color.LightSkyBlue
                            Me.grdTag.Rows(i).Cells("col_TAG_NO").Style.BackColor = Color.LightSkyBlue
                        Case Else
                            Me.grdTag.Rows(i).Cells("col_Status").Style.BackColor = Color.White
                            Me.grdTag.Rows(i).Cells("col_TAG_NO").Style.BackColor = Color.White
                    End Select
            

            End Select
        Next

        If objDT.Rows.Count > 0 Then
            Me.lbCountRowsTag.Text = "จำนวน " & objDT.Compute(" Count(TAG_Index)", "").ToString & " รายการ"
        Else
            Me.lbCountRowsTag.Text = "ไม่พบรายการ"
        End If


    End Sub


    Private Sub btnPopSku_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPopSku_Search.Click
        Try
            Dim frmPopup As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, Me._Customer_Index)
            frmPopup.Customer_Index = Me._Customer_Index

            frmPopup.ShowDialog()
            If frmPopup.Sku_Index <> "" Then
                _SKU_Index = frmPopup.Sku_Index
                Me.txtSKU_Id.Text = frmPopup.Sku_ID
                Me.txtSKU_Des.Text = frmPopup.Sku_Des_th
                Me.chkSKU.Checked = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub chkLot_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkLot.CheckedChanged
        Try
            If Me.chkLot.Checked = True Then
                Me.txtLot.Enabled = True
            Else
                Me.txtLot.Enabled = False
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnSearch_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim StrSQL As String = " AND (Qty > 0) AND Order_Index = '" & Me.Order_Index & "' "
        Try
            If Me.chkLot.Checked = True Then
                StrSQL &= " AND PLot = '" & Me.txtLot.Text.Trim & "'"
            End If
            If Me.chkSKU.Checked = True Then
                StrSQL &= " AND SKU_Index = '" & Me._SKU_Index & "'"
            End If

            Dim objTAG As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
            objTAG.getOrderDetail_Tag(StrSQL)
            Me.grdOrderItem.DataSource = objTAG.DataTable
            getTAG()

            If objTAG.DataTable.Rows.Count > 0 Then
                Me.lbCountRowsOrder.Text = "จำนวน " & objTAG.DataTable.Compute(" Count(OrderItem_Index)", "").ToString & " รายการ"
            Else
                Me.lbCountRowsOrder.Text = "ไม่พบรายการ"
            End If


        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub chkSKU_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSKU.CheckedChanged
        Try
            'If Me.chkSKU.Checked = True Then

            'Else
            '    Me.txtSKU_Id.Text = ""
            '    Me.txtSKU_Des.Text = ""
            '    Me.Sku_Index = ""
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnManualGenTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManualGenTag.Click
        Try
            If Me.grdOrderItem.RowCount = 0 Then Exit Sub


            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If


            Dim drGenTag() As DataRow
            Dim objDBTempIndex As New Sy_AutoNumber
            Dim objAutoNumber As New Sy_AutoNumber
            Dim objItemCollection As New List(Of tb_TAG)
            drGenTag = CType(Me.grdOrderItem.DataSource, DataTable).Select("chkSelect = 1 AND Qty > 0")
            If drGenTag.Length > 0 Then
                Dim RowError As Integer = Validation.Validate(drGenTag)
                If RowError = -1 Then ' Check Validate -1 = Okay
                    Select Case Me.cboQtyPerTagManual.SelectedIndex
                        Case 0 ' 1 : 1
                            drGenTag = CType(Me.grdOrderItem.DataSource, DataTable).Select("chkSelect = 1")
                            If drGenTag.Length = 0 Then
                                W_MSG_Information_ByIndex("300032")
                                Exit Sub
                            End If
                            For Each DrOrderItem As DataRow In drGenTag
                                Dim otb_TAG As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                                otb_TAG = SetTagItem(DrOrderItem, otb_TAG)
                                otb_TAG.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                objItemCollection.Add(otb_TAG)
                            Next
                        Case 1 '1 : M
                            Dim WeightOrder As Double = IIf(Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_WeightOrder").Value Is Nothing, "0", Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_WeightOrder").Value)
                            Dim VolumeOder As Double = IIf(Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_VolumeOrder").Value Is Nothing, "0", Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_VolumeOrder").Value)
                            Dim QtyOrder As Double = IIf(Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_QtyOrder").Value Is Nothing, "1", Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_QtyOrder").Value)
                            Dim QtyTagComplet As Double = IIf(Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_QtyTagComplet").Value Is Nothing, "1", Me.grdOrderItem.Rows(Me.grdOrderItem.CurrentRow.Index).Cells("Col_QtyTagComplet").Value)

                            WeightOrder = (WeightOrder / QtyOrder) * QtyTagComplet
                            VolumeOder = (VolumeOder / QtyOrder) * QtyTagComplet

                            Dim frm As New WMS_STD_INB_Receive.frmTag_Add_One_to_M
                            frm.Qty = QtyTagComplet
                            frm.Weight = WeightOrder
                            frm.Volume = VolumeOder
                            frm.ShowDialog()

                            If frm.Confirm Then
                                For Each DrOrderItem As DataRow In frm.DataSource.Rows
                                    Dim drNewrow As DataRow
                                    drNewrow = CType(Me.grdOrderItem.DataSource, DataTable).NewRow
                                    drNewrow.ItemArray = CType(Me.grdOrderItem.DataSource, DataTable).Rows(Me.grdOrderItem.CurrentRow.Index).ItemArray

                                    Dim otb_TAG As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                                    otb_TAG = SetTagItem(drNewrow, otb_TAG)
                                    otb_TAG.Qty_per_TAG = DrOrderItem("Qty_per_TAG")
                                    otb_TAG.Weight_per_TAG = DrOrderItem("Weight_per_TAG")
                                    otb_TAG.Volume_per_TAG = DrOrderItem("Volume_per_TAG")

                                    otb_TAG.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                    otb_TAG.TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                    objItemCollection.Add(otb_TAG)
                                Next
                            Else
                                Exit Sub
                            End If

                        Case 2
                            Dim frm As New WMS_STD_INB_Receive.frmPopUpTagDuplicate("", _Order_Index)
                            frm.ShowDialog()
                            If frm.boolComplete = False Then
                                Exit Sub
                            End If

                    End Select


                    If objItemCollection.Count > 0 Then
                        Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                        objItemA.objItemCollection = objItemCollection
                        objItemA.InsertData()
                        objDBTempIndex = Nothing
                        objAutoNumber = Nothing
                    End If

                    btnSearch_Click_1(sender, e)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnAutoGenTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoGenTag.Click
        Try
            If Me.grdOrderItem.RowCount = 0 Then Exit Sub

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If


            Dim drGenTag() As DataRow
            'drGenTag = CType(Me.grdOrderItem.DataSource, DataTable).Select("chkSelect = 1")
            'If drGenTag.Length = 0 Then
            '    W_MSG_Information_ByIndex("300032")
            '    Exit Sub
            'End If

            'Dim drGenTag() As DataRow
            Dim objDBTempIndex As New Sy_AutoNumber
            Dim objAutoNumber As New Sy_AutoNumber
            Dim objItemCollection As New List(Of tb_TAG)
            drGenTag = CType(Me.grdOrderItem.DataSource, DataTable).Select("chkSelect = 1 AND Qty > 0")
            If drGenTag.Length > 0 Then
                Dim RowError As Integer = Validation.Validate(drGenTag)
                If RowError = -1 Then ' Check Validate -1 = Okay
                    Select Case Me.cboQtyPerTagAuto.SelectedIndex
                        Case 0 'Qty Per Pallet 
                            Dim ObjGetQtyPerPallet As New WMS_STD_Master_Datalayer.ms_SKU(WMS_STD_Master_Datalayer.ms_SKU.enuOperation_Type.SEARCH)
                            For Each DrOrderItem As DataRow In drGenTag
                                ObjGetQtyPerPallet.SearchColume_ByIndex(DrOrderItem("SKU_Index").ToString, "Qty_Per_Pallet")
                                Dim tmpQty As Integer = CInt(DrOrderItem("Qty"))
                                Dim tmpQty_Per_Pallet As Integer = CInt(ObjGetQtyPerPallet.DataTable.Rows(0)("Qty_Per_Pallet").ToString)
                                If tmpQty_Per_Pallet <= 0 Then
                                    W_MSG_Information("สินค้า : " & DrOrderItem("SKU_Id").ToString & " ไม่มีการกำหนดจำนวน/พาเลท")
                                    Exit Sub
                                End If

                                While 0 < tmpQty
                                    Dim objItemPerQty As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                                    objItemPerQty = SetTagItem(DrOrderItem, objItemPerQty)
                                    With objItemPerQty
                                        If tmpQty < tmpQty_Per_Pallet Then
                                            .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                            .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                            .Qty_per_TAG = tmpQty
                                            .Weight_per_TAG = (.Weight / .Qty) * tmpQty
                                            .Volume_per_TAG = (.Volume / .Qty) * tmpQty
                                            .Ref_No3 = tmpQty_Per_Pallet
                                        Else
                                            .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                            .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                            .Qty_per_TAG = tmpQty_Per_Pallet
                                            .Weight_per_TAG = (.Weight / .Qty) * tmpQty_Per_Pallet
                                            .Volume_per_TAG = (.Volume / .Qty) * tmpQty_Per_Pallet
                                            .Ref_No3 = tmpQty_Per_Pallet
                                        End If
                                    End With
                                    objItemCollection.Add(objItemPerQty)
                                    tmpQty = tmpQty - tmpQty_Per_Pallet
                                End While
                            Next
                        Case 1 ' 1 : 1
                            For Each DrOrderItem As DataRow In drGenTag
                                For iRowQty As Integer = 1 To DrOrderItem("Qty").ToString
                                    Dim objItemPerQty As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                                    objItemPerQty = SetTagItem(DrOrderItem, objItemPerQty)
                                    With objItemPerQty
                                        .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                        .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                        .Qty_per_TAG = 1
                                        .Weight_per_TAG = .Weight / .Qty
                                        .Volume_per_TAG = .Volume / .Qty
                                        .Ref_No3 = iRowQty & "/" & .Qty
                                    End With
                                    objItemCollection.Add(objItemPerQty)
                                Next
                            Next
                    End Select
                    If objItemCollection.Count > 0 Then
                        Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                        objItemA.objItemCollection = objItemCollection
                        objItemA.InsertData()
                        objDBTempIndex = Nothing
                        objAutoNumber = Nothing
                    End If
                    btnSearch_Click_1(sender, e)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Public Function SetTagItem(ByVal podrOrderItem As DataRow, ByVal poTagItem As tb_TAG) As tb_TAG
        Try
            With podrOrderItem
                poTagItem.Order_No = .Item("Order_No").ToString
                poTagItem.Order_Index = Me._Order_Index
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

                If String.IsNullOrEmpty(.Item("ERP_Location").ToString) = False Then
                    poTagItem.ERP_Location = .Item("ERP_Location").ToString
                Else
                    poTagItem.ERP_Location = ""
                End If

            End With

            poTagItem.Qty_per_TAG = poTagItem.Qty
            poTagItem.Weight_per_TAG = poTagItem.Weight
            poTagItem.Volume_per_TAG = poTagItem.Volume

            poTagItem.Ref_No3 = "1/1"
            Return poTagItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            If Me.grdTag.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If


            If Me.grdTag.Rows.Count <= 0 Then Exit Sub
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim dtDelTag As New DataTable
                dtDelTag = CType(grdTag.DataSource, DataTable)
                dtDelTag.AcceptChanges()
                Dim drDelTagArr() As DataRow = dtDelTag.Select("chkSelect=1 and TAG_Status = 1")
                Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.DELETE)
                For Each drDelTag As DataRow In drDelTagArr
                    Me.Tag_Index = drDelTag("TAG_Index").ToString
                    objTag.DeleteAllTagForOrderItem(Me.Tag_Index)
                Next
                getTAG()
                btnSearch_Click_1(sender, e)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllOrder.CheckedChanged
        Try
            If Me.grdOrderItem.Rows.Count > 0 Then
                Dim Dttmp As New DataTable
                Dttmp = CType(Me.grdOrderItem.DataSource, DataTable)
                If Me.chkAllOrder.Checked = True Then
                    For Each Dr As DataRow In Dttmp.Rows
                        Dr("chkSelect") = 1
                    Next
                Else
                    For Each Dr As DataRow In Dttmp.Rows
                        Dr("chkSelect") = 0
                    Next
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub ChkAllTag_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ChkAllTag.CheckedChanged
        Try
            If Me.grdTag.Rows.Count > 0 Then
                Dim Dttmp As New DataTable
                Dttmp = CType(Me.grdTag.DataSource, DataTable)
                If Me.ChkAllTag.Checked = True Then
                    For Each Dr As DataRow In Dttmp.Rows
                        Dr("chkSelect") = 1
                    Next
                Else
                    For Each Dr As DataRow In Dttmp.Rows
                        Dr("chkSelect") = 0
                    Next
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub frmTag_Main_V2_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If CheckFrmUSE = True Then
                Dim objOrder As New tb_Order
                objOrder.UserUseTag(_Order_Index, "NOTUSE")
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub


    Private Sub grdTag_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTag.CellClick
        Try
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
                Case "Col_BtnAdd"
                    Dim frm As New WMS_STD_INB_Receive.frmPopUpTagDuplicate(Me.grdTag.Rows(Me.grdTag.CurrentRow.Index).Cells("col_TAG_NO").Value.ToString, _Order_Index)
                    frm.ShowDialog()
                    If frm.boolComplete Then
                        btnSearch_Click_1(sender, e)
                    End If

                Case Else
                    Exit Sub
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnSaveTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSaveTag.Click
        Try
            Me.ButtonManagement()

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If


            Dim drGenTag() As DataRow
            Dim ListTag_Index As New List(Of String)
            CType(Me.grdTag.DataSource, DataTable).AcceptChanges()
            drGenTag = CType(Me.grdTag.DataSource, DataTable).Select("chkSelect = 1 AND TAG_Status = 1 ")
            'drGenTag = CType(Me.grdTag.DataSource, DataTable).Select("TAG_Status = 1 ")
            For Each dr As DataRow In drGenTag
                ListTag_Index.Add(dr("TAG_Index"))
            Next
            If ListTag_Index.Count > 0 Then
                Dim frm As New WMS_STD_INB_Receive.frmPutawayWithTAG
                'Dim frm As New frmPutawayWithTAG
                frm.Order_Index = grdTag.Rows(grdTag.CurrentRow.Index).Cells("col_Order_Index").Value.ToString
                frm.TAG_Index = ListTag_Index
                frm.ShowDialog()
                btnSearch_Click_1(sender, e)
            Else
                W_MSG_Information("กรุณาเลือกรายการที่ต้องการ จัดเก็บ")
            End If

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub



    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
        Try
            'Dim dtTAG As New DataTable
            'dtTAG = grdTag.DataSource
            'dtTAG.AcceptChanges()
            'Dim drTagArr() As DataRow = dtTAG.Select("chkSelect=1 and TAG_Status = 1")
            '' Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.DELETE)
            'For Each drTag As DataRow In drTagArr
            '    Me.Tag_Index = drTagArr("TAG_Index").ToString
            '    'objTag.DeleteAllTagForOrderItem(Me.Tag_Index)


            'Next
            PrintBarcode()
            getTAG()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub PrintBarcode()
        Try

            Dim oconfig_Report As New config_Report
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Dim dtTAG As New DataTable

            Try
                Select Case Report_Name.ToUpper
                    Case "TAGSTICKERPRINTOUT", "TAGSTICKERPRINTOUT_MINI", "TAGSTICKERCHEMICALPRINTOUT_MINI", "TAGSTICKERSPBRPRINTOUT_MINI"
                        Dim oReport As New clsReport()
                        Dim TAG_Index_IN As String = ""
                        Dim drArr() As DataRow = DirectCast(Me.grdTag.DataSource, DataTable).Select(" chkSelect=1 and TAG_Status <> -1 ")
                        If drArr.Length > 0 Then
                            For Each dr As DataRow In drArr
                                TAG_Index_IN &= "'" & dr("TAG_Index").ToString() & "',"

                                ' BGN Update PrintCount
                                Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.NULL)
                                objTag.UpdatePrintStatus(dr("TAG_Index").ToString())
                                ' END Update PrintCount
                            Next
                        Else
                            W_MSG_Information_ByIndex("300032")
                            Exit Sub
                        End If
                        TAG_Index_IN = TAG_Index_IN.Substring(0, TAG_Index_IN.Trim.Length - 1)
                        Dim strWhere As String = " and TAG_Index in(" & TAG_Index_IN & ")"

                        Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                        Dim rpt As New CrystalDecisions.CrystalReports.Engine.ReportDocument()
                        rpt = oReport.GetReportInfo(Report_Name, strWhere)

                        frm.CrystalReportViewer1.ReportSource = rpt
                        frm.ShowDialog()
                    Case Else
                        dtTAG = grdTag.DataSource
                        Dim drTagArr() As DataRow = dtTAG.Select("chkSelect=1 and TAG_Status <> -1")

                        Dim strBarcode1 As String = ""
                        Dim odtImage As New DataTable
                        odtImage.Columns.Add("pic", GetType(System.Byte()))

                        Dim strWherePlus As String = " AND "
                        Dim tag_index As String = ""
                        Dim TAG_No As String = ""
                        Dim wh As String
                        Dim chk_valid As Boolean = False
                        Dim objTag As New tb_TAG(tb_TAG.enuOperation_Type.NULL)
                        For Each drTag As DataRow In drTagArr
                            tag_index = drTag("TAG_Index").ToString
                            objTag.UpdatePrintStatus(tag_index)
                            TAG_No = drTag("TAG_No").ToString
                            'objTag.DeleteAllTagForOrderItem(Me.Tag_Index)
                            strWherePlus &= "  TAG_Index = '" & tag_index & "' OR"
                            chk_valid = True

                            'Generate Barcode
                            strBarcode1 = TAG_No
                            Dim objBarcode As New Barcode
                            objBarcode.GenBarcode(strBarcode1)
                            Dim odr As DataRow
                            odr = odtImage.NewRow
                            odr("pic") = ConvertFileToByte(Application.StartupPath & "\" & strBarcode1 & ".bmp")
                            odtImage.Rows.Add(odr)
                        Next
                        If chk_valid = False Then
                            W_MSG_Information_ByIndex("300032") 'กรุณาเลือกรายการ
                            Exit Sub
                        End If

                        wh = strWherePlus.Substring(1, strWherePlus.Length - 3)


                        Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
                        Dim oReport As New WMS_STD_INB_Report.Loading_Report(Report_Name, wh, odtImage)
                        frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                        frm.ShowDialog()

                        '###################################
                End Select
            Catch ex As Exception
                MessageBox.Show(ex.Message)
            Finally
            End Try

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub frmTag_Main_V3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigReceive
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 1)
                    oFunction.SW_Language_Column(Me, Me.grdOrderItem, 1)
                    oFunction.SW_Language_Column(Me, Me.grdTag, 1)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnCloseDocument_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCloseDocument.Click
        Try
            If W_MSG_Confirm("ต้องการปิดเอกสาร ใช่หรือไม่ ?") <> Windows.Forms.DialogResult.Yes Then
                Exit Sub
            End If

            Dim obj As New OrderTransaction(OrderTransaction.enuOperation_Type.UPDATE)
            Dim strMAG As String = obj.CloseDocument(Me._Order_Index)

            If Not String.IsNullOrEmpty(strMAG) Then
                W_MSG_Information(strMAG)
            Else
                W_MSG_Information("ปิดเอกสารเสร็จสิ้น !!")
                Me.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class