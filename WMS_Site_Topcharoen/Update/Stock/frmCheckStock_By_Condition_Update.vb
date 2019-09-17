Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports Microsoft.Office.Interop
Imports WMS_STD_Master
Imports System.Globalization
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_Formula.W_Module

Public Class frmCheckStock_By_Condition_Update
    ' ================================================================= 
    ' VIEW used in this module:
    '  - VIEW_LocationBalance
    '   
    ' ================================================================= 

    Private _DEFAULT_ImagePath As String = ""
    Private gstrFileName As String = ""
    Private gstrLongFilePath As String = ""
    Private gstrAppPath As String = ""

#Region "+ Property +"
    Private _SkuName As String
    Public Property SkuName() As String
        Get
            Return _SkuName
        End Get
        Set(ByVal value As String)
            _SkuName = value
        End Set
    End Property

    Private _CallBy As String
    Public Property CallBy() As String
        Get
            Return _CallBy
        End Get
        Set(ByVal value As String)
            _CallBy = value
        End Set
    End Property

    Private _ItemStatus_Idex As String
    Public Property ItemStatus_Idex() As String
        Get
            Return _ItemStatus_Idex
        End Get
        Set(ByVal value As String)
            _ItemStatus_Idex = value
        End Set
    End Property

    Private _SkuID As String
    Public Property SkuID() As String
        Get
            Return _SkuID
        End Get
        Set(ByVal value As String)
            _SkuID = value
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


#End Region

#Region " FORM LOAD "

#End Region

    Private Sub GetReference()
        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getReference_Order(102)
            objDT = objClassDB.DataTable

            Dim str(8) As String
            str(0) = "-11"

            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    str(6) = "แสดงทุกรายการ"
                Case enmLanguage.English
                    str(7) = "Show All"
            End Select

            objDT.Rows.Add(str)

            Select Case W_Module.WV_Language
                Case enmLanguage.Thai
                    cboReference.DisplayMember = "Description_th"
                Case enmLanguage.English
                    cboReference.DisplayMember = "Description_en"
            End Select
            cboReference.ValueMember = "ValueMember"
            cboReference.DataSource = objDT

            cboReference.SelectedIndex = cboReference.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


#Region " NOT USED (BACKUP)"
    'Private Sub getInventory_For_CheckStock_By_Condition()
    '    Dim objClassDB As New Inventory
    '    Dim objDT As DataTable = New DataTable
    '    Dim strWHERE As String = ""

    '    Try

    '         *** Check Condition from Screen *** 
    '        If chkSKU_Description.Checked = True Then
    '            strWHERE = strWHERE + " AND SKU.Str1  LIKE '" & cboSKU_Description.Text & "'"
    '        End If

    '        If chkItemStatus.Checked = True Then
    '            strWHERE = strWHERE + " AND LB.ItemStatus_Index  = '" & cboItemStatus.SelectedValue & "'"
    '        End If

    '        If chkOther1.Checked = True Then
    '            Select Case cboOther1.Text
    '                Case "Lot ผลิต"
    '                    strWHERE = strWHERE + " AND LB.Plot LIKE '" & txtOther1.Text & "'"
    '                Case "ตำแหน่ง"
    '                    strWHERE = strWHERE + " AND L.Location_Alias LIKE '" & txtOther1.Text & "'"
    '            End Select
    '        End If


    '        If chkOther2.Checked = True Then
    '            Select Case cboOther2.Text
    '                Case "Lot ผลิต"
    '                    strWHERE = strWHERE + " AND LB.Plot LIKE '" & txtOther2.Text & "'"
    '                Case "ตำแหน่ง"
    '                    strWHERE = strWHERE + " AND L.Location_Alias LIKE '" & txtOther2.Text & "'"
    '            End Select
    '        End If

    '        If Not Me.txtCustomerID.Tag Is Nothing Then
    '             *** Define Only Customer *** 
    '            If Not Me.txtCustomerID.Tag = "" Then
    '                strWHERE = strWHERE + " AND C.Customer_Index ='" & Me.txtCustomerID.Tag & "' "
    '            End If

    '        End If

    '        objClassDB.getInventory_For_CheckStock_By_Condition(strWHERE, Me.cboGrouping.SelectedIndex)
    '        objDT = objClassDB.DataTable

    '         *** Clear datagridview ***
    '        Me.grdProductList.Columns.Clear()
    '        Me.grdProductList.Rows.Clear()
    '        Me.grdProductList.Refresh()
    '         **************************


    '        Select Case Me.cboGrouping.SelectedIndex
    '            Case 0

    '                 *** Add Header Colume Runtime *** 
    '                grdProductList.Columns.Add("Sku_Id", "รหัส SKU")
    '                grdProductList.Columns.Add("Sku_Description", "ชื่อรายการ")
    '                grdProductList.Columns.Add("Sum_Qty_Bal", "จำนวนรวม")
    '                grdProductList.Columns.Add("Sku_Package", "หน่วย")
    '                 **********************************
    '                 *** Add Value from datatable to gridview ***
    '                For i As Integer = 0 To objDT.Rows.Count - 1
    '                    With Me.grdProductList
    '                        Me.grdProductList.Rows.Add()
    '                        .Rows(i).Cells("Sku_Id").Value = objDT.Rows(i).Item("Sku_Id").ToString
    '                        .Rows(i).Cells("Sku_Description").Value = objDT.Rows(i).Item("SKU_Description").ToString
    '                        .Rows(i).Cells("Sum_Qty_Bal").Value = Format(Val(objDT.Rows(i).Item("SUM_Qty_Bal").ToString), "#,##0.00").ToString
    '                        .Rows(i).Cells("SKU_Package").Value = objDT.Rows(i).Item("SKU_Package").ToString
    '                    End With
    '                Next
    '                 ********************************************
    '            Case 1
    '                 *** Add Header Colume Runtime *** 
    '                grdProductList.Columns.Add("Sku_Id", "รหัส SKU")
    '                grdProductList.Columns.Add("Sku_Description", "ชื่อรายการ")
    '                grdProductList.Columns.Add("Plot", "Lot ผลิต")
    '                grdProductList.Columns.Add("Sum_Qty_Bal", "จำนวนรวม")
    '                grdProductList.Columns.Add("Sku_Package", "หน่วย")
    '                 **********************************
    '                 *** Add Value from datatable to gridview ***
    '                For i As Integer = 0 To objDT.Rows.Count - 1
    '                    With Me.grdProductList
    '                        Me.grdProductList.Rows.Add()
    '                        .Rows(i).Cells("Sku_Id").Value = objDT.Rows(i).Item("Sku_Id").ToString
    '                        .Rows(i).Cells("Sku_Description").Value = objDT.Rows(i).Item("SKU_Description").ToString
    '                        .Rows(i).Cells("Plot").Value = objDT.Rows(i).Item("Plot").ToString
    '                        .Rows(i).Cells("Sum_Qty_Bal").Value = Format(Val(objDT.Rows(i).Item("SUM_Qty_Bal").ToString), "#,##0.00").ToString
    '                        .Rows(i).Cells("SKU_Package").Value = objDT.Rows(i).Item("SKU_Package").ToString
    '                    End With
    '                Next
    '                 ********************************************
    '            Case 2
    '                 *** Add Header Colume Runtime *** 
    '                grdProductList.Columns.Add("Sku_Id", "รหัส SKU")
    '                grdProductList.Columns.Add("Sku_Description", "ชื่อรายการ")
    '                grdProductList.Columns.Add("Plot", "Lot ผลิต")
    '                grdProductList.Columns.Add("ItemStatus_Description", "สถานะสินค้า")
    '                grdProductList.Columns.Add("Sum_Qty_Bal", "จำนวนรวม")
    '                grdProductList.Columns.Add("Sku_Package", "หน่วย")
    '                 **********************************

    '                 *** Add Value from datatable to gridview ***
    '                For i As Integer = 0 To objDT.Rows.Count - 1
    '                    With Me.grdProductList
    '                        Me.grdProductList.Rows.Add()
    '                        .Rows(i).Cells("Sku_Id").Value = objDT.Rows(i).Item("Sku_Id").ToString
    '                        .Rows(i).Cells("Sku_Description").Value = objDT.Rows(i).Item("SKU_Description").ToString
    '                        .Rows(i).Cells("Plot").Value = objDT.Rows(i).Item("Plot").ToString
    '                        .Rows(i).Cells("ItemStatus_Description").Value = objDT.Rows(i).Item("ItemStatus_Description").ToString
    '                        .Rows(i).Cells("Sum_Qty_Bal").Value = Format(Val(objDT.Rows(i).Item("SUM_Qty_Bal").ToString), "#,##0.00").ToString
    '                        .Rows(i).Cells("SKU_Package").Value = objDT.Rows(i).Item("SKU_Package").ToString
    '                    End With
    '                Next
    '                 ********************************************
    '        End Select

    '        With objDT
    '            For intCol = 0 To .Columns.Count - 1
    '                grdProductList.Columns.Add(intCol.ToString, intCol.ToString)
    '            Next
    '        End With

    '        Me.btnUpdate.Enabled = True
    '        Me.btnJobOrder.Enabled = True

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objClassDB = Nothing
    '        objDT = Nothing
    '    End Try

    'End Sub
#End Region

#Region " INITIAL CONTROL "

    Private Sub getItemStatus()

        Dim objClassDB As New ms_ItemStatus(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getItemStatus()
            objDT = objClassDB.DataTable

            'Dim str(1) As String
            'str(0) = "-11"
            'str(1) = "--- เลือกทั้งหมด ---" 'TODO: Support multilanguage
            'objDT.Rows.InsertAt(str, 0)

            Dim odr As DataRow
            odr = objDT.NewRow
            odr("ItemStatus_Index") = "-11"
            odr("ItemStatus") = "--- เลือกทั้งหมด ---"

            objDT.Rows.InsertAt(odr, 0)

            cboItemStatus.DisplayMember = "ItemStatus"
            cboItemStatus.ValueMember = "ItemStatus_Index"
            cboItemStatus.DataSource = objDT

            cboItemStatus.SelectedIndex = 0 'cboItemStatus.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getProductType()
        Dim objClassDB As New ms_ProductType(ms_ProductType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            'Dim str(6) As String
            'str(4) = "-11"
            'str(5) = " "
            'str(6) = "--- เลือกทั้งหมด ---" 'TODO: Support multilanguage

            Dim odr As DataRow
            odr = objDT.NewRow
            odr("ProductType_Index") = "-11"
            odr("Description") = "--- เลือกทั้งหมด ---"
            objDT.Rows.InsertAt(odr, 0)

            cbProductType.DisplayMember = "Description"
            cbProductType.ValueMember = "ProductType_Index"
            cbProductType.DataSource = objDT

            cbProductType.SelectedIndex = 0 'cbProductType.Items.Count - 1


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getWarehouse()
        Dim objClassDB As New ms_Warehouse(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cbStore.DisplayMember = "Description"
            cbStore.ValueMember = "Warehouse_Index"
            cbStore.DataSource = objDT

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
    Private Sub getRoom()
        Dim objClassDB As New ms_Room(ms_ItemStatus.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SelectByWareHouse(cbStore.SelectedValue.ToString)
            objDT = objClassDB.DataTable

            Dim dr As DataRow
            dr = objDT.NewRow
            dr("Description") = "ไม่ระบุ"
            dr("Room_Index") = "-1"

            objDT.Rows.InsertAt(dr, 0)


            cbRoom.DisplayMember = "Description"
            cbRoom.ValueMember = "Room_Index"
            cbRoom.DataSource = objDT

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Public Sub SetDEFAULT_CUSTOMER_INDEX()
        Try
            Dim tCustomer_Index As String
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            tCustomer_Index = oUser.GetUserByCustomerDefault()

            If Not String.IsNullOrEmpty(tCustomer_Index) Then
                Me.txtCustomerID.Tag = tCustomer_Index
                Me.getCustomer()
            End If

            '###################################
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally

        End Try

    End Sub

#End Region

#Region " BUTTON & COMBO EVENT "
    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Dim frm As New frmCustmer_Popup
        frm.ShowDialog()
        Dim tmpCustomer_Index As String = ""
        tmpCustomer_Index = frm.Customer_Index
        If Not tmpCustomer_Index = "" Then
            Me.txtCustomerID.Tag = tmpCustomer_Index
            Me.getCustomer()
        Else
            Me.txtCustomerID.Tag = ""
            Me.txtCustomerID.Text = ""
            Me.txtCustomer_Name.Text = ""
        End If
        If Me.txtCustomerID.Text = "" Or Me.txtCustomer_Name.Text = "" Then
            Me.txtCustomerID.Tag = ""
        End If
        frm.Close()
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        If Me.chkCustomer.Checked Then
            If txtCustomerID.Text = "" Then
                W_MSG_Information_ByIndex(8)
                Exit Sub

            End If

            Col_CustomerIndex.Visible = False
            Col_CustomerName.Visible = False

        Else
            Col_CustomerIndex.Visible = True
            Col_CustomerName.Visible = True
        End If
        Try
            'Visible Column
            Visible_DataGridColumn()

            'Grid Location 20161206
            If chkLoc.Checked Then
                If String.IsNullOrEmpty(txtLocS.Text.Trim) OrElse String.IsNullOrEmpty(txtLocE.Text.Trim) Then
                    W_MSG_Information("กรุณาระบุช่วงตำแหน่ง")
                    Exit Sub
                End If
            End If

            If chkShev.Checked Then
                If String.IsNullOrEmpty(txtShevS.Text.Trim) OrElse String.IsNullOrEmpty(txtShevE.Text.Trim) Then
                    W_MSG_Information("กรุณาระบุช่วงชั้น")
                    Exit Sub
                End If
            End If


            Dim Sql1 As String = SetCondition_forSearch()
            Dim Sql2 As String = SetCondition_forSearch_Sku()

            Load_Grd(Sql1)
            Load_Grd_Sku(Sql2)

            'Grid Sku
            Me.Get_SumData()
            SetnumRows()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub Load_Grd(ByVal SQL As String)
        Try
            ' แสดงทั้งหมด
            ' แสดงแยกตามรายการสินค้าเท่านั้น()
            ' แสดงแยกตาม Lot สินค้า
            ' แสดงแยกตาม Lot และสถานะสินค้า

            grdProductList.AutoGenerateColumns = False
            grdProductList.DataSource = Query(SQL)
            Select Case Me.cbShowMode.SelectedIndex
                Case 0, -1 ' แสดงทั้งหมด

                    'Grid ตัวที่สอง
                    col_Tag_no.Visible = True
                    ItemStatus.Visible = True
                    'Col_pallteNo.Visible = True
                    col_Order_No.Visible = True
                    col_Order_Date.Visible = True
                    Lot.Visible = True
                    Col_Mfg_Date.Visible = True
                    Col_Exp_Date.Visible = True
                    col_Ref_No1.Visible = True
                    col_Ref_No2.Visible = True
                    col_Ref_No3.Visible = True
                    col_Ref_No4.Visible = True
                    col_Ref_No5.Visible = True
                    Col_ERP_Location.Visible = True
                    colProductType.Visible = True
                    ColLocation_Alias.Visible = True
                    Me.col_AgeCountFromMfg.Visible = True
                    Me.col_AgeRemain.Visible = True
                Case 1 ' แสดงแยกตามรายการสินค้าเท่านั้น()
                    colProductType.Visible = True
                    'Col_Mfg_Date.Visible = True
                    Me.col_AgeCountFromMfg.Visible = False
                    Me.col_AgeRemain.Visible = False
                Case 2 ' แสดงแยกตาม Lot สินค้า
                    Lot.Visible = True
                    colProductType.Visible = True
                    Col_Exp_Date.Visible = True
                    Col_Mfg_Date.Visible = True
                    ColLocation_Alias.Visible = False
                    Me.col_AgeCountFromMfg.Visible = True
                    Me.col_AgeRemain.Visible = True
                Case 3 ' แสดงแยกตาม Lot และสถานะสินค้า
                    ItemStatus.Visible = True
                    Lot.Visible = True
                    colProductType.Visible = True
                    Col_Exp_Date.Visible = True
                    Col_Mfg_Date.Visible = True
                    ColLocation_Alias.Visible = False
                    Me.col_AgeCountFromMfg.Visible = True
                    Me.col_AgeRemain.Visible = True
                Case 4 ' 
                    ItemStatus.Visible = True
                    Lot.Visible = True
                    Col_ERP_Location.Visible = True
                    colProductType.Visible = False
                    Col_Exp_Date.Visible = True
                    Col_Mfg_Date.Visible = True
                    Me.col_AgeCountFromMfg.Visible = True
                    Me.col_AgeRemain.Visible = True

            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub Load_Grd_Sku(ByVal SQL As String)
        Try
            grdProductList_Sku.AutoGenerateColumns = False
            grdProductList_Sku.DataSource = Query(SQL)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Sub Visible_DataGridColumn()
        Try

            'Grid ตัวที่สอง
            col_Item_Qty.Visible = False
            col_Tag_no.Visible = False
            ItemStatus.Visible = False
            Col_pallteNo.Visible = False
            col_Order_No.Visible = False
            col_Order_Date.Visible = False
            Lot.Visible = False
            Col_Mfg_Date.Visible = False
            Col_Exp_Date.Visible = False
            col_Ref_No1.Visible = False
            col_Ref_No2.Visible = False
            col_Ref_No3.Visible = False
            col_Ref_No4.Visible = False
            col_Ref_No5.Visible = False

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Sub SetnumRows()
        Dim numRows As Integer = 0
        numRows = grdProductList.Rows.Count
        If numRows > 0 Then
            lbCountRows.Text = "รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows.Text = "ไม่พบรายการ"
        End If
        numRows = grdProductList_Sku.Rows.Count
        If numRows > 0 Then
            lbCountRows_Sku.Text = "รายการ แสดง " & numRows & " รายการ"
        Else
            lbCountRows_Sku.Text = "ไม่พบรายการ"
        End If
    End Sub

    Private Sub cbStore_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbStore.SelectedIndexChanged
        Me.getRoom()
    End Sub
#End Region

#Region " GENERIC FUNCTIONS AND SUBS "

    Private Sub getCustomer()
        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getPopup_Customer("Customer_Index", Me.txtCustomerID.Tag.ToString)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Me.txtCustomerID.Tag = objDT.Rows(0).Item("Customer_Index").ToString
                'Me.txtCustomer_Id.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomerID.Text = objDT.Rows(0).Item("Customer_Id").ToString
                Me.txtCustomer_Name.Text = objDT.Rows(0).Item("Customer_Name").ToString
                Me.chkCustomer.Checked = True
            Else
                Me.txtCustomerID.Tag = ""
                Me.txtCustomerID.Text = ""
                Me.txtCustomer_Name.Text = ""
            End If


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Function SetCondition_forSearch() As String
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim strRef As String = ""


            'strSQL = " SELECT VL.SKU_Index,Sku_Id,Product_Name_Th,str1 ,Product_Name_en, Sum(Qty_Bal) As Qty_Bal, Package_sku, Sum(Qty_Item_Bal) As Qty_Item_Bal, Item_Package,Sum(Weight_Bal) As Weight_Bal,Sum(Volume_Bal) As Volume_Bal,Sum(OrderItem_Price_Bal) As OrderItem_Price_Bal, PLot, ItemStatus_Des ,PO_No ,Ref_No1,Ref_No2,Ref_No3,Ref_No4,Ref_No5,str5,Mfg_Date,Exp_Date"

            ' แสดงทั้งหมด
            ' แสดงแยกตามรายการสินค้าเท่านั้น()
            ' แสดงแยกตาม Lot สินค้า
            ' แสดงแยกตาม Lot และสถานะสินค้า

            Select Case Me.cbShowMode.SelectedIndex
                Case 0, -1
                    strSQL = " SELECT *,(Qty_Bal - ReserveQty - Pallet_Qty) as Qty_Free  "
                    'strSQL &= " ,  CASE WHEN ISNULL(CARTONRatio, 0)= 0 THEN 0 ELSE CAST((Qty_Bal / CONVERT(numeric, ISNULL(CARTONRatio, 0))) AS int) END AS QtyCARTON  "
                    'strSQL &= " , CASE WHEN ISNULL(PCRatio, 0)= 0 THEN 0 ELSE (CAST((Qty_Bal % CASE WHEN isnull(CARTONRatio, 0) = 0 THEN Qty_Bal + 1 ELSE CONVERT(numeric, ISNULL(CARTONRatio, 1)) END) AS int) )/ PCRatio END AS QtyPC "

                Case 1
                    strSQL = " SELECT Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku,Item_Package ,Sum(Qty_Bal) As Qty_Bal,sum(ReserveQty) as ReserveQty,sum(Qty_Bal)-sum(ReserveQty)-sum(Pallet_Qty) as Qty_Free,Sum(Qty_Item_Bal) As Qty_Item_Bal,Sum(OrderItem_Price_Bal) As OrderItem_Price_Bal,Location_Alias,Customer_Id,Customer_Name,Sum(Weight_Bal) As Weight_Bal,Sum(Volume_Bal) As Volume_Bal"
                    strSQL &= " ,ProductType_Code"
                    'strSQL &= " ,  CASE WHEN ISNULL(CARTONRatio, 0)= 0 THEN 0 ELSE CAST((Sum(Qty_Bal) / CONVERT(numeric, ISNULL(CARTONRatio, 0))) AS int) END AS QtyCARTON  "
                    'strSQL &= " , CASE WHEN ISNULL(PCRatio, 0)= 0 THEN 0 ELSE (CAST((Sum(Qty_Bal) % CASE WHEN isnull(CARTONRatio, 0) = 0 THEN Sum(Qty_Bal) + 1 ELSE CONVERT(numeric, ISNULL(CARTONRatio, 1)) END) AS int) )/ PCRatio END AS QtyPC "
                    'strSQL &= " ,QtyChest,Case_Per_Pallet,PC_Per_Pallet"
                    strSQL &= " , ProductType_Des "
                    strSQL &= " , Warehouse_Des, Pallet_Qty "

                Case 2
                    strSQL = " SELECT Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku, PLot,Sum(Qty_Bal) As Qty_Bal,sum(ReserveQty) as ReserveQty,sum(Qty_Bal)-sum(ReserveQty)-sum(Pallet_Qty) as Qty_Free,Sum(Qty_Item_Bal) As Qty_Item_Bal,Sum(OrderItem_Price_Bal) As OrderItem_Price_Bal,Customer_Id,Customer_Name,Sum(Weight_Bal) As Weight_Bal,Sum(Volume_Bal) As Volume_Bal"
                    strSQL &= " ,Exp_Date,Mfg_Date,ProductType_Code"
                    'strSQL &= " ,CASE WHEN ISNULL(CARTONRatio, 0)= 0 THEN 0 ELSE CAST((Sum(Qty_Bal) / CONVERT(numeric, ISNULL(CARTONRatio, 0))) AS int) END AS QtyCARTON  "
                    'strSQL &= " , CASE WHEN ISNULL(PCRatio, 0)= 0 THEN 0 ELSE (CAST((Sum(Qty_Bal) % CASE WHEN isnull(CARTONRatio, 0) = 0 THEN Sum(Qty_Bal) + 1 ELSE CONVERT(numeric, ISNULL(CARTONRatio, 1)) END) AS int) )/ PCRatio END AS QtyPC "
                    'strSQL &= " ,QtyChest,Case_Per_Pallet,PC_Per_Pallet"
                    strSQL &= " , ProductType_Des "
                    strSQL &= " , AgeCountFromMfg"
                    strSQL &= " , AgeRemain "
                    strSQL &= " , Warehouse_Des , Pallet_Qty"

                Case 3
                    strSQL = " SELECT Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku, PLot, ItemStatus_Des,Item_Package,Sum(Qty_Bal) As Qty_Bal,sum(ReserveQty) as ReserveQty,sum(Qty_Bal)-sum(ReserveQty)-sum(Pallet_Qty) as Qty_Free,Sum(Qty_Item_Bal) As Qty_Item_Bal,Sum(OrderItem_Price_Bal) As OrderItem_Price_Bal,Customer_Id,Customer_Name,Sum(Weight_Bal) As Weight_Bal,Sum(Volume_Bal) As Volume_Bal"
                    strSQL &= " ,Exp_Date,Mfg_Date,ProductType_Code"
                    'strSQL &= " , CASE WHEN ISNULL(CARTONRatio, 0)= 0 THEN 0 ELSE CAST((Sum(Qty_Bal) / CONVERT(numeric, ISNULL(CARTONRatio, 0))) AS int) END AS QtyCARTON  "
                    'strSQL &= " , CASE WHEN ISNULL(PCRatio, 0)= 0 THEN 0 ELSE (CAST((Sum(Qty_Bal) % CASE WHEN isnull(CARTONRatio, 0) = 0 THEN Sum(Qty_Bal) + 1 ELSE CONVERT(numeric, ISNULL(CARTONRatio, 1)) END) AS int) )/ PCRatio END AS QtyPC "
                    'strSQL &= " ,QtyChest,Case_Per_Pallet,PC_Per_Pallet"
                    strSQL &= " , ProductType_Des"
                    strSQL &= " , AgeCountFromMfg"
                    strSQL &= " , AgeRemain "
                    strSQL &= " , Warehouse_Des , Pallet_Qty"

                Case 4
                    strSQL = " SELECT Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku, PLot, ItemStatus_Des,Item_Package,Sum(Qty_Bal) As Qty_Bal,sum(ReserveQty) as ReserveQty,sum(Qty_Bal)-sum(ReserveQty)-sum(Pallet_Qty) as Qty_Free,Sum(Qty_Item_Bal) As Qty_Item_Bal,Sum(OrderItem_Price_Bal) As OrderItem_Price_Bal,Location_Alias,Customer_Id,Customer_Name,ERP_Location,Sum(Weight_Bal) As Weight_Bal,Sum(Volume_Bal) As Volume_Bal"
                    strSQL &= " ,Exp_Date,Mfg_Date"
                    'strSQL &= " ,  CASE WHEN ISNULL(CARTONRatio, 0)= 0 THEN 0 ELSE CAST((Sum(Qty_Bal) / CONVERT(numeric, ISNULL(CARTONRatio, 0))) AS int) END AS QtyCARTON  "
                    'strSQL &= " , CASE WHEN ISNULL(PCRatio, 0)= 0 THEN 0 ELSE (CAST((Sum(Qty_Bal) % CASE WHEN isnull(CARTONRatio, 0) = 0 THEN Sum(Qty_Bal) + 1 ELSE CONVERT(numeric, ISNULL(CARTONRatio, 1)) END) AS int) )/ PCRatio END AS QtyPC "
                    'strSQL &= " ,QtyChest,Case_Per_Pallet,PC_Per_Pallet"
                    strSQL &= " , ProductType_Des"
                    strSQL &= " , AgeCountFromMfg"
                    strSQL &= " , AgeRemain "
                    strSQL &= " , Warehouse_Des , Pallet_Qty "
            End Select

            strSQL &= " FROM VIEW_CheckStock_By_Condition (nolock)    "
            strWhere = " WHERE (1=1) "

            If Me.chkDistributionCenter.Checked = True Then
                strWhere &= " AND (DistributionCenter_Index ='" & Me.cboDistributionCenter.SelectedValue & "') "
            End If

            If Me.chkNonStock.Checked = True Then
                strWhere &= " AND (isNonStock = 0) "
            End If

            If Me.chkCustomer.Checked = True Then
                strWhere &= " AND (Customer_Index ='" & Me.txtCustomerID.Tag & "') "
            End If

            If Me.cbProductType.SelectedValue <> "-11" Then
                strWhere &= " AND (ProductType_Index ='" & Me.cbProductType.SelectedValue & "') "
            End If
            If Me.cboItemStatus.SelectedValue <> "-11" Then
                strWhere &= " AND (ItemStatus_Index ='" & Me.cboItemStatus.SelectedValue & "') "
            End If

            If Me.chkStore.Checked = True Then
                strWhere &= " AND (Warehouse_Index ='" & Me.cbStore.SelectedValue & "') "
                If Me.cbRoom.SelectedValue <> -1 Then
                    strWhere &= " AND (Room_Index ='" & Me.cbRoom.SelectedValue & "') "
                End If
            End If

            If chkLoc.Checked Then
                If String.IsNullOrEmpty(txtLocS.Text.Trim) OrElse String.IsNullOrEmpty(txtLocE.Text.Trim) Then
                    Return ""
                End If
                strWhere &= " AND (Location_Alias BETWEEN '" & Me.txtLocS.Text.Trim & "' AND '" & Me.txtLocE.Text.Trim & "') "
            End If

            If chkShev.Checked Then
                If String.IsNullOrEmpty(txtShevS.Text.Trim) OrElse String.IsNullOrEmpty(txtShevE.Text.Trim) Then
                    Return ""
                End If
                strWhere &= " AND (Level BETWEEN " & Me.txtShevS.Text.Trim & " AND " & Me.txtShevE.Text.Trim & ") "
            End If

            If chkMfgDate.Checked = True Then
                strWhere &= " AND (Mfg_Date between '" & SetDateTime(dtpFromDate_M) & "' AND '" & SetDateTime(dtpToDate_M) & "')"
            End If

            If chkExpDate.Checked = True Then
                strWhere &= " AND (Exp_Date between '" & SetDateTime(dtpFromDate_Ex) & "' AND '" & SetDateTime(dtpToDate_Ex) & "')"
            End If

            If chkSku.Checked = True Then
                strWhere &= " AND (Sku_Id ='" & Me.txtSKU_ID.Text & "') "
            End If

            If chkOrderDate.Checked = True Then

                strWhere &= " AND (OrderDate between '" & SetDateTime(dtpFromOrderDate_M) & "' AND '" & SetDateTime(dtpToOrderDate_M) & "')"
            End If

            If chkOther1.Checked = True Then
                Select Case cboOther1.SelectedIndex
                    Case 0 'Lotผลิต
                        strWhere &= " AND (PLot LIKE '%" & Me.txtOther1.Text.Trim & "') "
                        'Case 1 'รหัส SKU
                        '    strWhere &= " AND (Sku_Id LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 1 'ตำแหน่ง
                        strWhere &= " AND (Location_Alias LIKE '" & Me.txtOther1.Text.Trim & "%') "

                    Case 2
                        strWhere &= " AND (Level  LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 3
                        strWhere &= " AND (TAG_NO LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 4
                        strWhere &= " AND (Lock  LIKE '" & Me.txtOther1.Text.Trim & "%') "
                End Select
            End If

            '--------- Reference ------------
            If chkReference.Checked = True Then
                If cboReference.SelectedValue <> "-11" Then
                    strWhere &= " And " & cboReference.SelectedValue.ToString & " like '" & Me.txtReference.Text.Trim.Replace("'", " ") & "%' "
                End If
            End If

            If chkAgeRemain.Checked Then
                If IsNumeric(Me.txtAgeRemain.Text.Trim) Then
                    strWhere &= String.Format(" AND AgeRemain {0} {1}", Me.cboAgeRemain.Text, CInt(Me.txtAgeRemain.Text.Trim))
                End If
            End If

            If Me.chkWarehouse_Between.Checked Then
                Dim _warehouse_Between_S As String = Me.txtWarehouse_Between_S.Text.Trim()
                Dim _warehouse_Between_E As String = Me.txtWarehouse_Between_E.Text.Trim()
                If (Not _warehouse_Between_S = Nothing) Then
                    strWhere &= String.Format(" and Warehouse_No>='{0}' ", _warehouse_Between_S)
                End If
                If (Not _warehouse_Between_E = Nothing) Then
                    strWhere &= String.Format(" and Warehouse_No<='{0}' ", _warehouse_Between_E)
                End If
            End If

            strWhere &= " AND (Qty_Bal > 0) "

            '--------- Details ------------
            Dim IsCheckDetails As Boolean = False
            IsCheckDetails = CheckDetailsClickCheckBox()
            If IsCheckDetails Then
                strWhere &= SetCondition_Details_forSearch(IsCheckDetails)
            End If
            strSQL = strSQL & strWhere

            ' แสดงทั้งหมด
            ' แสดงแยกตามรายการสินค้าเท่านั้น()
            ' แสดงแยกตาม Lot สินค้า
            ' แสดงแยกตาม Lot และสถานะสินค้า

            Select Case Me.cbShowMode.SelectedIndex
                Case 0
                    strSQL &= " ORDER BY Sku_Id,Order_Date,Tag_no ASC "
                    ' strSQL &= " QtyCARTON, QtyPC "
                Case 1
                    strSQL &= " GROUP BY Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku,Item_Package,Location_Alias,Customer_Id,Customer_Name"
                    'strSQL &= " ,CARTONRatio, PCRatio,QtyChest,Case_Per_Pallet,PC_Per_Pallet  "
                    strSQL &= " ,ProductType_Code ,ProductType_Des, Weight_Bal,Volume_Bal"
                    strSQL &= " , Warehouse_Des, Pallet_Qty "
                    strSQL &= " ORDER BY Sku_Id ASC "
                Case 2
                    strSQL &= " GROUP BY Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku, PLot,Customer_Id,Customer_Name"
                    'strSQL &= " ,CARTONRatio, PCRatio,QtyChest,Case_Per_Pallet,PC_Per_Pallet 
                    strSQL &= "  ,ProductType_Code,Exp_Date,Mfg_Date, Weight_Bal,Volume_Bal"
                    strSQL &= " ,ProductType_Des"
                    strSQL &= " , AgeCountFromMfg "
                    strSQL &= " , AgeRemain "
                    strSQL &= " , Warehouse_Des, Pallet_Qty "
                    strSQL &= " ORDER BY Sku_Id ASC "
                Case 3
                    strSQL &= " GROUP BY Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku, PLot, ItemStatus_Des,Item_Package,Customer_Id,Customer_Name"
                    'strSQL &= " ,CARTONRatio, PCRatio,QtyChest,Case_Per_Pallet,PC_Per_Pallet "
                    strSQL &= " ,ProductType_Code ,Exp_Date,Mfg_Date, Weight_Bal,Volume_Bal"
                    strSQL &= " ,ProductType_Des"
                    strSQL &= " , AgeCountFromMfg "
                    strSQL &= " , AgeRemain "
                    strSQL &= " , Warehouse_Des, Pallet_Qty "
                    strSQL &= " ORDER BY Sku_Id ASC "

                Case 4
                    strSQL &= " GROUP BY Sku_Index,Sku_Id, Product_Name_en,Product_Name_th, Package_sku, PLot, ItemStatus_Des,Item_Package,Location_Alias,Customer_Id,Customer_Name,ERP_Location"
                    'strSQL &= " ,CARTONRatio, PCRatio,QtyChest,Case_Per_Pallet,PC_Per_Pallet  "
                    strSQL &= " ,Exp_Date,Mfg_Date"
                    strSQL &= " ,ProductType_Des, Weight_Bal,Volume_Bal"
                    strSQL &= " , AgeCountFromMfg "
                    strSQL &= " , AgeRemain "
                    strSQL &= " , Warehouse_Des, Pallet_Qty "
                    strSQL &= " ORDER BY Sku_Id ASC "

            End Select


            Return strSQL
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Function SetCondition_forSearch_Sku() As String
        Try
            Dim strSQL As String = ""
            Dim strWhere As String = ""
            Dim strRef As String = ""
            Dim strGroup As String = ""

            Me.col_Lot.Visible = False
            Me.colExpDate.Visible = False

            Select Case cbShowModePage1.SelectedIndex
                Case 0
                    strGroup = ""
                Case 1
                    strGroup = ",PLot"
                    Me.col_Lot.Visible = True
                Case 2
                    strGroup = ",Exp_Date"
                    Me.colExpDate.Visible = True
                Case Else
                    strGroup = ""
            End Select



            strSQL = " SELECT   SKU_Index,Sku_Id,Package_Sku,Item_Package"
            strSQL &= "         ,Product_Name_en,Product_Name_Th,Customer_Id,Customer_Name,ProductType_Des"
            strSQL &= " ,ProductType_Code"
            strSQL &= strGroup
            strSQL &= "         ,Sum(Qty_Bal) as Qty_Bal"
            strSQL &= "         ,Sum(Qty_Item_Bal) as Qty_Item_Bal"
            strSQL &= "         ,Sum(Weight_Bal) as Weight_Bal"
            strSQL &= "         ,Sum(Volume_Bal) as Volume_Bal"
            strSQL &= "         ,Sum(OrderItem_Price_Bal) as OrderItem_Price_Bal"
            strSQL &= "         ,Sum(ReserveQty) as ReserveQty"
            strSQL &= "         ,sum(Qty_Bal)-sum(ReserveQty)-sum(Pallet_Qty) as Qty_Free"
            'strSQL &= " ,  CASE WHEN ISNULL(CARTONRatio, 0)= 0 THEN 0 ELSE CAST((Sum(Qty_Bal) / CONVERT(numeric, ISNULL(CARTONRatio, 0))) AS int) END AS QtyCARTON  "
            'strSQL &= " , CASE WHEN ISNULL(PCRatio, 0)= 0 THEN 0 ELSE (CAST((Sum(Qty_Bal) % CASE WHEN isnull(CARTONRatio, 0) = 0 THEN Sum(Qty_Bal) + 1 ELSE CONVERT(numeric, ISNULL(CARTONRatio, 1)) END) AS int) )/ PCRatio END AS QtyPC "

            strSQL &= "         FROM ("
            '*********************** Begin Normal Query ***********************
            strSQL &= " SELECT * "
            strSQL &= " FROM VIEW_CheckStock_By_Condition (nolock)  "
            strWhere = " WHERE (1=1) "


            If Me.chkDistributionCenter.Checked = True Then
                strWhere &= " AND (DistributionCenter_Index ='" & Me.cboDistributionCenter.SelectedValue & "') "
            End If


            If Me.chkNonStock.Checked = True Then
                strWhere &= " AND (isNonStock = 0) "
            End If


            If chkReference.Checked = True Then
                If cboReference.SelectedValue <> "-11" Then
                    strWhere &= " And (" & cboReference.SelectedValue.ToString & " like '" & Me.txtReference.Text.Trim.Replace("'", " ") & "%') "
                End If
            End If

            If Me.chkCustomer.Checked = True Then
                strWhere &= " AND (Customer_Index ='" & Me.txtCustomerID.Tag & "') "
            End If
            If Me.cbProductType.SelectedValue <> "-11" Then
                strWhere &= " AND (ProductType_Index ='" & Me.cbProductType.SelectedValue & "') "
            End If
            If Me.cboItemStatus.SelectedValue <> "-11" Then
                strWhere &= " AND (ItemStatus_Index ='" & Me.cboItemStatus.SelectedValue & "') "
            End If
            If Me.chkStore.Checked = True Then
                strWhere &= " AND (Warehouse_Index ='" & Me.cbStore.SelectedValue & "') "
                If Me.cbRoom.SelectedValue <> "-1" Then
                    strWhere &= " AND (Room_Index ='" & Me.cbRoom.SelectedValue & "') "
                End If
            End If
            If chkLoc.Checked Then
                If String.IsNullOrEmpty(txtLocS.Text.Trim) OrElse String.IsNullOrEmpty(txtLocE.Text.Trim) Then
                    Return ""
                End If
                strWhere &= " AND (Location_Alias BETWEEN '" & Me.txtLocS.Text.Trim & "' AND '" & Me.txtLocE.Text.Trim & "') "
            End If

            If chkShev.Checked Then
                If String.IsNullOrEmpty(txtShevS.Text.Trim) OrElse String.IsNullOrEmpty(txtShevE.Text.Trim) Then
                    Return ""
                End If
                strWhere &= " AND (Level BETWEEN " & Me.txtShevS.Text.Trim & " AND " & Me.txtShevE.Text.Trim & ") "
            End If
            If chkMfgDate.Checked = True Then
                strWhere &= " AND (Mfg_Date between '" & SetDateTime(dtpFromDate_M) & "' AND '" & SetDateTime(dtpToDate_M) & "')"
            End If
            If chkExpDate.Checked = True Then
                strWhere &= " AND (Exp_Date between '" & SetDateTime(dtpFromDate_Ex) & "' AND '" & SetDateTime(dtpToDate_Ex) & "')"
            End If
            If chkSku.Checked = True Then
                strWhere &= " AND (Sku_Id ='" & Me.txtSKU_ID.Text & "') "
            End If
            If chkOrderDate.Checked = True Then
                strWhere &= " AND (OrderDate between '" & SetDateTime(dtpFromOrderDate_M) & "' AND '" & SetDateTime(dtpToOrderDate_M) & "')"
            End If
            If chkOther1.Checked = True Then
                Select Case cboOther1.SelectedIndex
                    Case 0 'Lotผลิต
                        strWhere &= " AND (PLot LIKE '%" & Me.txtOther1.Text.Trim & "') "
                        'Case 1 'รหัส SKU
                        '    strWhere &= " AND (Sku_Id LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 1 'ตำแหน่ง
                        strWhere &= " AND (Location_Alias LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 2
                        strWhere &= " AND (Level  LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 3
                        strWhere &= " AND (TAG_NO LIKE '" & Me.txtOther1.Text.Trim & "%') "
                    Case 4
                        strWhere &= " AND (Lock  LIKE '" & Me.txtOther1.Text.Trim & "%') "

                End Select
            End If

            If chkAgeRemain.Checked Then
                If IsNumeric(Me.txtAgeRemain.Text.Trim) Then
                    strWhere &= String.Format(" AND AgeRemain {0} {1}", Me.cboAgeRemain.Text, CInt(Me.txtAgeRemain.Text.Trim))
                End If
            End If

            If chkReference.Checked = True Then
                If cboReference.SelectedValue <> "-11" Then
                    strWhere &= " And " & cboReference.SelectedValue.ToString & " like '" & Me.txtReference.Text.Trim.Replace("'", " ") & "%' "
                End If
            End If

            If Me.chkWarehouse_Between.Checked Then
                Dim _warehouse_Between_S As String = Me.txtWarehouse_Between_S.Text.Trim()
                Dim _warehouse_Between_E As String = Me.txtWarehouse_Between_E.Text.Trim()
                If (Not _warehouse_Between_S = Nothing) Then
                    strWhere &= String.Format(" and Warehouse_No>='{0}' ", _warehouse_Between_S)
                End If
                If (Not _warehouse_Between_E = Nothing) Then
                    strWhere &= String.Format(" and Warehouse_No<='{0}' ", _warehouse_Between_E)
                End If
            End If

            strWhere &= " AND (Qty_Bal > 0) "

            '--------- Details ------------
            Dim IsCheckDetails As Boolean = False
            IsCheckDetails = CheckDetailsClickCheckBox()
            If IsCheckDetails Then
                strWhere &= SetCondition_Details_forSearch(IsCheckDetails)
            End If
            strSQL = strSQL & strWhere

            '*********************** End Normal Query ***********************


            strSQL &= " ) DERIVEDTBL "
            strSQL &= " GROUP BY SKU_Index,Sku_Id,Package_Sku,Item_Package"
            strSQL &= "         ,Product_Name_en,Product_Name_Th,Customer_Id,Customer_Name,ProductType_Des"
            'strSQL &= " ,CARTONRatio, PCRatio"
            strSQL &= " ,ProductType_Code"
            strSQL &= strGroup
            strSQL &= " ORDER BY Sku_Id ASC "






            Return strSQL
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#Region " SumData "

    Sub Get_SumData()
        Try
            If grdProductList.Rows.Count > 0 Then
                Dim dtProductList As DataTable
                dtProductList = CType(grdProductList.DataSource, DataTable)
                txtSumQty.Text = Format(CDbl(dtProductList.Compute("sum(Qty_Bal) - sum(ReserveQty) - sum(Pallet_Qty)", "Sku_Index <> ''").ToString), "#,##0.00")
                txtSumNet_Wt.Text = Format(CDbl(dtProductList.Compute("sum(Weight_Bal)", "Sku_Index <> ''").ToString), "#,##0.0000")
                txtSumVolume.Text = Format(CDbl(dtProductList.Compute("sum(Volume_Bal)", "Sku_Index <> ''").ToString), "#,##0.0000")
                txtPackage.Text = Format(CDbl(dtProductList.Compute("sum(Qty_Item_Bal)", "Sku_Index <> ''").ToString), "#,##0.00")
                txtRate.Text = Format(CDbl(dtProductList.Compute("sum(OrderItem_Price_Bal)", "Sku_Index <> ''").ToString), "#,##0.00")
            End If
            If grdProductList_Sku.Rows.Count > 0 Then
                Dim dtProductList_Sku As DataTable
                dtProductList_Sku = CType(grdProductList_Sku.DataSource, DataTable)

                'txtQtyCT.Text = Format(CDbl(dtProductList_Sku.Compute("sum(QtyCARTON)", "Sku_Index <> ''").ToString), "#,##0")
                txtSumpck_BalSku.Text = Format(CDbl(dtProductList_Sku.Compute("sum(Qty_Free)", "Sku_Index <> ''").ToString), "#,##0.00")
                txtSumWeight_BalSku.Text = Format(CDbl(dtProductList_Sku.Compute("sum(Weight_Bal)", "Sku_Index <> ''").ToString), "#,##0.0000")
                txtSumVolume_BalSku.Text = Format(CDbl(dtProductList_Sku.Compute("sum(Volume_Bal)", "Sku_Index <> ''").ToString), "#,##0.0000")
                txtSumQty_BalSku.Text = Format(CDbl(dtProductList_Sku.Compute("sum(Qty_Item_Bal)", "Sku_Index <> ''").ToString), "#,##0.00")
                txtSumPrice_BalSku.Text = Format(CDbl(dtProductList_Sku.Compute("sum(OrderItem_Price_Bal)", "Sku_Index <> ''").ToString), "#,##0.00")
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region


    Function Query(ByVal SQL As String) As DataTable
        Try

            Dim objDB As New SQLCommands
            Dim DT As New DataTable

            objDB.SQLComand(SQL)
            DT = objDB.DataTable

            Return DT
        Catch ex As Exception
            Throw ex
        End Try

    End Function



    Function SetDateTime(ByVal DTP As DateTimePicker) As String

        'DTP.Format = DateTimePickerFormat.Custom
        '  DTP.CustomFormat = "yyyy/MM/dd"
        Dim strDate As String = DTP.Value.ToString("yyyy/MM/dd")
        'DTP.Format = DateTimePickerFormat.Long

        Return strDate
    End Function

#End Region


    Private Sub cmdExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdExport.Click, cmdExport_NoLocation.Click
        Try
            If W_MSG_Confirm("ต้องการ Export Excel ใช่หรือไม่") = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            Dim grdExport As New DataGridView
            If cmdExport.CanSelect Then
                grdExport = grdProductList
            Else
                grdExport = grdProductList_Sku
            End If
            If (grdExport.RowCount) <= 0 Then
                W_MSG_Information("ไม่พบข้อมูล Export")
                Exit Sub
            End If

            Dim ds As New DataSet
            Dim objExport As New Export_Excel_KC
            ds.Tables.Add(objExport.DataGridViewToDataTable(grdExport))
            ds.Tables(0).TableName = Now.ToString("yyyyMMdd_HHmm")
            objExport.export(ds, Me.Text)

            'ExportToExcel(grdExport)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ExportToExcel(ByVal grdExport As DataGridView)
        Dim CurrentThread As System.Threading.Thread
        CurrentThread = System.Threading.Thread.CurrentThread
        CurrentThread.CurrentCulture = New CultureInfo("en-US")
        Try
            Dim i As Integer = 0
            Dim j As Integer = 2

            'Dim ExcelApp As Excel.Application
            'Dim ExcelBooks As Excel.Workbook
            'Dim ExcelSheets As Excel.Worksheet
            Dim ExcelApp As Object
            Dim ExcelBooks As Object
            Dim ExcelSheets As Object

            ' ExcelApp = New Excel.Application
            ExcelApp = CreateObject("Excel.Application")
            ExcelBooks = ExcelApp.Workbooks.Add
            ExcelSheets = ExcelBooks.Worksheets(1)

            '

            ExcelApp.Visible = False
            'ExcelBooks = ExcelApp.Workbooks.Add()
            'ExcelSheets = DirectCast(ExcelBooks.Worksheets(1), Excel.Worksheet)

            i = 0
            j = 2

            Dim Div As Decimal = 0
            Dim Status As Decimal = 0
            Dim StatusDesc As String = ""
            Label1.Visible = True
            With ExcelSheets
                .Columns().ColumnWidth = 22


                .Range("D" & j.ToString()).Value = "รายงานสรุปสินค้า คงเหลือ"
                .Range("D" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("D" & j.ToString()).Font.Bold = True
                .Range("D" & j.ToString()).Font.Size = 14
                '.Range("A1").Interior.Color = RGB(224, 224, 224)

                j += 1

                .Range("B" & j.ToString()).Value = chkCustomer.Text & " : " & txtCustomer_Name.Text.ToString
                .Range("B" & j.ToString()).Font.Color = RGB(0, 0, 0)
                .Range("B" & j.ToString()).Font.Bold = True
                .Range("B" & j.ToString()).Font.Size = 14
                '.Range("A2").Interior.Color = RGB(224, 224, 224)

                j += 1

                Dim Col As Integer = 0
                Dim strCol As String = "A"
                Dim iChar As Integer = 65
                Dim iTwoChar As Integer = 65

               


                For Col = 0 To grdExport.ColumnCount - 1

                   
                    If iTwoChar > 90 Then 'เกิน Z
                        strCol = "A" & Chr(iChar)
                    Else
                        strCol = Chr(iChar)
                    End If

                    If iTwoChar = 90 And iChar = 90 Then
                        iChar = 65
                    End If

                    If grdExport.Columns(Col).Visible = False Then Continue For
                    .Range(strCol & j.ToString()).Value = grdExport.Columns(Col).HeaderText
                    .Range(strCol & j.ToString()).Font.Color = RGB(0, 0, 0)
                    .Range(strCol & j.ToString()).Font.Size = 9
                    .Range(strCol & j.ToString()).Font.Bold = True
                    .Range(strCol & j.ToString()).Interior.Color = RGB(192, 192, 192)
                    iChar += 1
                    iTwoChar += 1
                Next

                j += 1

                Dim dtgrdExport As New DataTable
                dtgrdExport = grdExport.DataSource


                Dim Row As Integer = 0
                'strCol = "A"
                'iChar = 65
                'Col = 0
                Div = 100 / (grdExport.RowCount - 1)

                For Row = 0 To grdExport.RowCount - 1

                    Status += Div
                    StatusDesc = "กำลัง Export Excel... " & FormatNumber(Status, 2).ToString & " %"
                    Label1.Text = StatusDesc


                    strCol = "A"
                    iChar = 65
                    iTwoChar = 65
                    Col = 0
                    For Col = 0 To grdExport.ColumnCount - 1
                        If iTwoChar > 90 Then 'เกิน Z
                            strCol = "A" & Chr(iChar)
                        Else
                            strCol = Chr(iChar)
                        End If

                        If iTwoChar = 90 And iChar = 90 Then
                            iChar = 65
                        End If

                        If grdExport.Columns(Col).Visible = False Then Continue For
                        Dim strData As String = ""
                        If grdExport.Rows(Row).Cells(Col).Value IsNot Nothing Then
                            strData = grdExport.Rows(Row).Cells(Col).Value.ToString
                        Else
                            strData = ""
                        End If
                        If grdExport.Columns(Col).CellType Is GetType(Date) Then

                        End If


                        If IsDate(strData) Then
                            .Range(strCol & j.ToString()).Value = "'" & CDate(strData).ToString("dd/MM/yyyy")
                        ElseIf grdExport.Columns(Col).Name.Contains("Sku_Id") = True OrElse grdExport.Columns(Col).Name.Contains("col_Sku_Sku_Id") = True OrElse grdExport.Columns(Col).Name.Contains("col_Tag_no") = True Then '
                            .Range(strCol & j.ToString()).Value = "'" & strData
                        Else
                            .Range(strCol & j.ToString()).Value = strData
                        End If
                        .Range(strCol & j.ToString()).Font.Size = 9

                       
                        iChar += 1
                        iTwoChar += 1
                    Next
                    j += 1
                Next


            End With

            Label1.Text = "เสร็จสิ้น"
            Status = 0
            Div = 0
            StatusDesc = ""
            Label1.Text = ""
            Label1.Visible = False

            ExcelApp.Visible = True
            ExcelSheets = Nothing
            ExcelBooks = Nothing
            ExcelApp = Nothing

        Catch ex As Exception
            Throw ex
        Finally
            CurrentThread.CurrentCulture = New CultureInfo("en-GB")
        End Try

    End Sub

    Private Sub btnSku_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Popup.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, txtCustomerID.Tag)
            frm.Customer_Index = txtCustomerID.Tag
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") Or (Not frm.Sku_Index Is Nothing) Then
                txtSKU_ID.Tag = frm.Sku_Index
                txtSKU_ID.Text = frm.Sku_ID
                txtSku_Name.Text = frm.Sku_Des_eng

            Else
                txtSKU_ID.Tag = ""
                txtSKU_ID.Text = ""
                txtSku_Name.Text = ""
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


    Private Sub frmCheckStock_By_Condition_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Try

            Dim oFunction As New W_Language

            oFunction.SwitchLanguage(Me, 1000)
            oFunction.SW_Language_Column(Me, Me.grdProductList, 1000)
            oFunction.SW_Language_Column(Me, Me.grdProductList_Sku, 1000)

            '--------------------------------------------
            'KSL Visible Fix code.
            Me.col_Sku_SKU_Index.Visible = False
            Me.col_CartonQty.Visible = False
            Me.col_PCQty.Visible = False
            Me.col_Sku_Item_Package.Visible = False
            Me.col_SKU_Index.Visible = False
            Me.col_CartonQty2.Visible = False
            Me.col_PCQty2.Visible = False
            Me.colQtyCase_Per_Pallet.Visible = False
            Me.colQtyPC_Per_Pallet.Visible = False
            Me.colQtyChest.Visible = False
            Me.col_AgeRemain.Visible = False
            Me.col_Item_Package.Visible = False
            '--------------------------------------------


            grdProductList.AutoGenerateColumns = False

            Application.CurrentCulture = New System.Globalization.CultureInfo("en-GB")

            'Add on KSL.
            Me.getDistributionCenter()

            Me.getItemStatus()
            Me.getProductType()
            Me.getWarehouse()
            Me.getRoom()

            Me.GetReference()

            SetDEFAULT_CUSTOMER_INDEX()

            cboAgeRemain.SelectedIndex = 0

            setLocationControl_By_Config()
            If _CallBy = "SO" Then
                '
                txtSKU_ID.Text = _SkuID
                txtSku_Name.Text = _SkuName

                Me.txtCustomerID.Tag = Me._Customer_Index
                Me.getCustomer()

                Try
                    Visible_DataGridColumn()


                    Load_Grd(SetCondition_forSearch())
                    Load_Grd_Sku(SetCondition_forSearch_Sku())


                    Me.Get_SumData()
                    SetnumRows()
                Catch ex As Exception
                    W_MSG_Error(ex.Message)
                End Try
            End If
            Label1.Text = ""
            Label1.Visible = False
            ' cbShowModePage1.SelectedIndex = 0
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub getDistributionCenter()
        Dim objms_DistributionCenter As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
        Dim objDTms_DistributionCenter As DataTable = New DataTable

        Try
            objms_DistributionCenter.GetAllAsDataTable("")
            objDTms_DistributionCenter = objms_DistributionCenter.DataTable

            cboDistributionCenter.DisplayMember = "Description"
            cboDistributionCenter.ValueMember = "DistributionCenter_Index"
            cboDistributionCenter.DataSource = objDTms_DistributionCenter

        Catch ex As Exception
            Throw ex
        Finally
            objms_DistributionCenter = Nothing
            objDTms_DistributionCenter = Nothing
        End Try

    End Sub
#Region "   Show  pic SKU "

    Private Sub grdProductList_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdProductList.MouseClick
        Try
            If grdProductList.Rows.Count = 0 Then
                Exit Sub
            End If


            'Dim frm As New frmPicSKU_Popup
            Dim strSku_Index As String = ""
            strSku_Index = grdProductList.Rows(grdProductList.CurrentRow.Index).Cells("col_SKU_Index").Value.ToString
            Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim odtSku As DataTable = New DataTable
            objSku.SelectData_For_Edit(strSku_Index)
            odtSku = objSku.DataTable
            If odtSku.Rows.Count = 0 Then
                picSKU.ImageLocation = ""
                Exit Sub
            End If

            If IsDBNull(odtSku.Rows(0).Item("Image_Path")) Then
                picSKU.ImageLocation = ""
                Exit Sub
            End If

            If odtSku.Rows(0).Item("Image_Path") = "" Then
                picSKU.ImageLocation = ""
                Exit Sub
            End If

            SetImage_Path()

            If Me._DEFAULT_ImagePath = "" Then
                gstrAppPath = Application.StartupPath
            Else
                gstrAppPath = Me._DEFAULT_ImagePath
            End If

            gstrAppPath = gstrAppPath & odtSku.Rows(0).Item("Image_Path")
            picSKU.ImageLocation = gstrAppPath



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub grdProductList_sku_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdProductList_Sku.MouseClick
        Try
            If grdProductList_Sku.Rows.Count = 0 Then
                Exit Sub
            End If


            'Dim frm As New frmPicSKU_Popup
            Dim strSku_Index As String = ""
            strSku_Index = grdProductList_Sku.CurrentRow.Cells("col_Sku_SKU_Index").Value

            Dim objSku As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim odtSku As DataTable = New DataTable
            objSku.SelectData_For_Edit(strSku_Index)
            odtSku = objSku.DataTable
            If odtSku.Rows.Count = 0 Then
                picSKU.ImageLocation = ""
                Exit Sub
            End If

            If IsDBNull(odtSku.Rows(0).Item("Image_Path")) Then
                picSKU.ImageLocation = ""
                Exit Sub
            End If

            If odtSku.Rows(0).Item("Image_Path") = "" Then
                picSKU.ImageLocation = ""
                Exit Sub
            End If

            SetImage_Path()

            If Me._DEFAULT_ImagePath = "" Then
                gstrAppPath = Application.StartupPath
            Else
                gstrAppPath = Me._DEFAULT_ImagePath
            End If

            gstrAppPath = gstrAppPath & odtSku.Rows(0).Item("Image_Path")
            picSKU.ImageLocation = gstrAppPath



        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub SetImage_Path()

        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try

            Dim strUseLocalPath As String = Configuration.ConfigurationManager.AppSettings("UseLocalPath").ToString

            If strUseLocalPath = 0 Then
                Me._DEFAULT_ImagePath = Configuration.ConfigurationManager.AppSettings("IMAGE_PATH_SKU").ToString
            Else

                objCustomSetting.GetConfig_Value("DEFAULT_IMAGE_PATH_SKU", "")
                objDT = objCustomSetting.DataTable

                If objDT.Rows.Count > 0 Then
                    Me._DEFAULT_ImagePath = objDT.Rows(0).Item("Config_Value").ToString
                Else
                    Me._DEFAULT_ImagePath = Application.StartupPath
                End If

            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

    End Sub

#End Region

    Private Sub cbShowMode_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowMode.SelectedIndexChanged
        Try
            btnSearch_Click(sender, e)
            'Load_Grd(SetCondition_forSearch())
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub setLocationControl_By_Config()
        Try
            Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
            If Not oconfig.getConfig_Key_USE("USE_SKU_ITEM_QTY") Then
                txtSumpck_BalSku.Location = New Point(txtSumpck_BalSku.Location.X + 91, txtSumpck_BalSku.Location.Y)
                lblSumpck_BalSku.Location = New Point(lblSumpck_BalSku.Location.X + 91, lblSumpck_BalSku.Location.Y)
                lblSumAll_BalSku.Location = New Point(lblSumAll_BalSku.Location.X + 91, lblSumAll_BalSku.Location.Y)

                lblSumQty_BalSku.Visible = False
                txtSumQty_BalSku.Visible = False

                txtSumQty.Location = New Point(txtSumQty.Location.X + 91, txtSumQty.Location.Y)
                lblSumPck.Location = New Point(lblSumPck.Location.X + 91, lblSumPck.Location.Y)
                lblSumAll.Location = New Point(lblSumAll.Location.X + 91, lblSumAll.Location.Y)

                txtPackage.Visible = False
                lbSumQty.Visible = False


            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub


    Private Sub frmCheckStock_By_Condition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigINVENTORY
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 1000)
                    oFunction.SW_Language_Column(Me, Me.grdProductList, 1000)
                    oFunction.SW_Language_Column(Me, Me.grdProductList_Sku, 1000)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub CheckReserveQtyToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckReserveQtyToolStripMenuItem.Click
        Try
            If grdProductList_Sku.RowCount <= 0 Then
                Exit Sub
            Else
                Dim frm As New frmCheckReserveQty_Update 'WMS_STD_OAW_INVENTORY.frmCheckReserveQty
                frm.SKU_Index = Me.grdProductList_Sku.Rows(Me.grdProductList_Sku.CurrentRow.Index).Cells("col_Sku_SKU_Index").Value.ToString
                frm.ShowDialog()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkReserve_KSL_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkReserve_KSL.Click
        Try
            If grdProductList_Sku.RowCount <= 0 Then
                Exit Sub
            Else
                Dim frm As New frmCheckReserv_KSL
                frm.SKU_Index = Me.grdProductList_Sku.Rows(Me.grdProductList_Sku.CurrentRow.Index).Cells("col_Sku_SKU_Index").Value.ToString
                frm.Show()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtKeyIdProductType_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtKeyIdProductType.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(13) Then
                'If Not chkProductType.Checked Then
                '    Exit Sub
                'End If
                Dim ClsGet As New tb_AssignJob_New
                Dim pId As String
                pId = ClsGet.CHK_ProductType(txtKeyIdProductType.Text.Trim)
                If Not pId Is Nothing Then
                    cbProductType.SelectedValue = pId
                Else
                    W_MSG_Information("ไม่พบรหัสประเภทสินค้า " & txtKeyIdProductType.Text)
                    txtKeyIdProductType.Text = ""
                    txtKeyIdProductType.Focus()
                    Exit Sub
                End If

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cbShowModePage1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbShowModePage1.SelectedIndexChanged
        Try
            btnSearch_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnProductType_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductType_Popup.Click
        Try
            Dim _popup As New frmProductType_Popup()
            _popup.ShowDialog()
            If (_popup.DialogResult = Windows.Forms.DialogResult.OK) Then
                Me.cbProductType.SelectedValue = _popup.ProductType_Index
                'Me.txtProductType.Tag = _popup.ProductType_Index
                'Me.txtProductType.Text = _popup.ProductType_Desc
            ElseIf (_popup.DialogResult = Windows.Forms.DialogResult.Cancel) Then
                'Me.txtProductType.Tag = ""
                'Me.txtProductType.Text = ""
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Function CheckDetailsClickCheckBox() As Boolean
        Try
            Dim IsCheckDetails As Boolean = False
            If (chkEye.Checked And txtEye.Text.ToString <> "") Or (chkAdd.Checked And txtAdd.Text.ToString <> "") _
            Or (chkTilted.Checked And txtTilted.Text.ToString <> "") Or (chkColor.Checked And txtColor.Text.ToString <> "") _
            Or (chkDegree.Checked And txtDegree.Text.ToString <> "") Or (chkBC.Checked And txtBC.Text.ToString <> "") _
            Or (chkVMI.Checked And txtVMI.Text.ToString <> "") Or (chkGeneration.Checked And txtGeneration.Text.ToString <> "") _
            Or (chkBrand.Checked And txtBrand.Text.ToString <> "") Then
                IsCheckDetails = True
            End If
            Return IsCheckDetails
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function SetCondition_Details_forSearch(ByVal IsCheckDetails As Boolean) As String
        Try
            Dim strWhere As String = ""
            strWhere = " AND Sku_Index IN ( "
            strWhere &= " SELECT Sku_Index FROM ms_SKU_Detail WHERE 1 = 1 "
            If chkEye.Checked And txtEye.Text.ToString <> "" Then strWhere &= " And [Eye] = N'" & txtEye.Text.ToString & "'"
            If chkAdd.Checked And txtAdd.Text.ToString <> "" Then strWhere &= " And [Add] = N'" & txtAdd.Text.ToString & "'"
            If chkTilted.Checked And txtTilted.Text.ToString <> "" Then strWhere &= " And [Tilted] = N'" & txtTilted.Text.ToString & "'"
            If chkColor.Checked And txtColor.Text.ToString <> "" Then strWhere &= " And [Color] = N'" & txtColor.Text.ToString & "'"
            If chkDegree.Checked And txtDegree.Text.ToString <> "" Then strWhere &= " And [Degree] = N'" & txtDegree.Text.ToString & "'"
            If chkBC.Checked And txtBC.Text.ToString <> "" Then strWhere &= " And [BC] = N'" & txtBC.Text.ToString & "'"
            If chkVMI.Checked And txtVMI.Text.ToString <> "" Then strWhere &= " And [VMI] = N'" & txtVMI.Text.ToString & "'"
            If chkGeneration.Checked And txtGeneration.Text.ToString <> "" Then strWhere &= " And [Generation] = N'" & txtGeneration.Text.ToString & "'"
            If chkBrand.Checked And txtBrand.Text.ToString <> "" Then strWhere &= " And [Brand] = N'" & txtBrand.Text.ToString & "'"
            strWhere &= " ) "
            Return strWhere
        Catch ex As Exception
            Throw ex
        End Try
        
    End Function
End Class