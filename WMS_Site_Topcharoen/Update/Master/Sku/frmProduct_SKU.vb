Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms
Imports System.Configuration.ConfigurationSettings

Public Class frmProduct_SKU

    ' 0 Defalt
    ' 1 GetData
    Public Mode As Integer = 0  ' Todd 24-01-2009 This variable is no longer used.
    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 50
    Private gboolSearch As Boolean = False ' Search data or not

    Private _DEFAULT_ImagePath As String = ""
    Private gstrFileName As String = ""
    Private gstrLongFilePath As String = ""
    Private gstrAppPath As String = ""

#Region "FORM LOAD"
    ''' <summary>
    ''' Form Load
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ProductExclusive_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2001)
            oFunction.SW_Language_Column(Me, Me.grdSKU, 2001)



            Me.btnPagePrev.Enabled = True   'Start at first page
            cboRowPerPage.SelectedIndex = 0
            Me.grdSKU.AutoGenerateColumns = False

            'Me.getProductType()
            Me.cbProductType.Visible = False

            SetProductClass()
            SetProductSubClassByIndex()

            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
#End Region


#Region "MAIN FUNCTIONS AND SUBS"
    ''' <summary>
    ''' Show SKU data in main Grid View.
    ''' This function allows paging and filtering from search criteria.
    ''' </summary>
    ''' <remarks></remarks>
    Sub ShowGrdSKU()

        Try
            Dim objms_SKU_Count As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim odtms_SKU_Count As DataTable = New DataTable
            Dim strFillter As String = ""

            ' There is no search filter, if this page is first loaded.
            ' Otherwise get search criteria
            If gboolSearch Then
                strFillter = GetConditionSearch()
            End If

            objms_SKU_Count.SearchData_Count(strFillter)
            odtms_SKU_Count = objms_SKU_Count.DataTable

            ' Get total records of the current search
            If odtms_SKU_Count.Rows.Count > 0 Then
                Me.txtRowCount.Text = odtms_SKU_Count.Rows(0)("Row_Total").ToString
            Else
                Me.txtRowCount.Text = 0
            End If

            ' Calculate Paging
            Call Calculate_Paging()

            Me.grdSKU.DataSource = Nothing

            Dim objClassDB As New ms_SKU(ms_SKU.enuOperation_Type.SEARCH)
            Dim objDT As DataTable = New DataTable

            Dim objDB2 As New tb_PackingBom

            objClassDB.SearchData_Sku("", strFillter, gintRowStart, gintRowEnd)
            objDT = objClassDB.DataTable
            grdSKU.DataSource = objDT

            ' Loop to add BOM image
            ' TODO: Improve performance! No need to loop.
            Dim i As Integer = 0
            For Each objDr As DataRow In objDT.Rows
                With grdSKU
                    If .Rows(i).Cells("colBom_Index").Value.ToString.Trim <> "" Then
                        .Rows(i).Cells("colBom_Pic").Value = Global.WMS_Site_TopCharoen.My.Resources.Resources.bomb
                    End If
                End With
                '
                i = i + 1
            Next
            grdSKU.Update()
            grdSKU.Refresh()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally

        End Try
        grdSKU.ClearSelection()
    End Sub
    ''' <summary>
    ''' Create SQL where clause from selected criteria.
    ''' </summary>
    ''' <returns></returns>

    Private Function GetConditionSearch() As String
        Try
            Dim str_Search As String = ""
            '02-02-2010 ja Update search

            If txtSearchSKUID.Text.Trim <> "" Then
                str_Search &= " AND (ms_SKU.Sku_ID like '" & txtSearchSKUID.Text.Trim.Replace("'", "''").Trim & "%')"
            End If

            'If txtSearchProductID.Text.Trim <> "" Then
            '    str_Search &= " AND (ms_Product.Product_Id like '" & txtSearchProductID.Text.Trim.Replace("'", "''").Trim & "%')"
            'End If
            If txtSearchProductID.Text.Trim <> "" Then
                str_Search &= " AND (ms_SKU.Str4 like '%" & txtSearchProductID.Text.Trim.Replace("'", "''").Trim & "%')"
            End If

            If (Not Me.txtProductType.Tag = Nothing) Then
                str_Search &= " AND (ProductType_Index ='" & Me.txtProductType.Tag & "') "
            End If
            'If Me.cbProductType.SelectedValue <> "-11" Then
            '    str_Search &= " AND (ProductType_Index ='" & Me.cbProductType.SelectedValue & "') "
            'End If
            ' We search name, both Thai and English, from Str1 and Str2.
            Select Case cbSearchSelectLang.SelectedIndex
                Case 1

                    str_Search &= " AND (ms_SKU.Str1 LIKE '%" & txtSearchSKUName.Text.Trim.Replace("'", "''").Trim & "%')"
                Case 2
                    str_Search &= " AND (ms_SKU.Str2 LIKE '%" & txtSearchSKUName.Text.Trim.Replace("'", "''").Trim & "%')"
            End Select

            If txtSearchPackage.Text.Trim <> "" Then
                str_Search &= " AND (ms_Package.Description LIKE '" & txtSearchPackage.Text.Trim.Replace("'", "''").Trim & "%')"
            End If

            If (cbOperator.Text <> "") And (txtSearchUnitWeight.Text <> "") Then
                str_Search &= " AND (ms_SKU.Max_Weight " & cbOperator.Text & " " & txtSearchUnitWeight.Text.Trim.Replace("'", "''").Trim & ")"
            End If

            If Me.chkBOMOnly.Checked Then
                str_Search &= " AND (tb_PackingBom.PackingBom_Index Is Not NULL) "
            End If

            ' Check if user select a customer
            If Me.chkCustomer.Checked = True Then
                If txtCustomer_Name.Text <> "" Then
                    Dim strCustomer_Index As String = txtCustomer_Name.Tag.ToString
                    str_Search &= " AND (ms_SKU.Customer_Index = '" & strCustomer_Index & "') "
                End If

            End If

            If Me.txtSearchSize.Text.Trim <> "" Then
                str_Search &= " AND (ms_Size.Description LIKE '" & Me.txtSearchSize.Text.Replace("'", "''").Trim & "%')"
            End If

            ' Check if user select a supplier
            If Me.chkSupplier.Checked = True Then
                If txtSupplierName.Text <> "" Then
                    Dim strSupplier_Index As String = txtSupplierName.Tag.ToString
                    str_Search &= " AND (ms_SKU.Supplier_Index = '" & strSupplier_Index & "') "
                End If

            End If

            With Me.cboProductClass
                If Not .SelectedIndex < 0 AndAlso .SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(.SelectedValue.ToString) Then
                    str_Search &= " AND (ms_SKU.ProductClass_Index = '" & .SelectedValue.ToString & "') "
                End If
            End With

            With Me.cboProductSubClass
                If Not .SelectedIndex < 0 AndAlso .SelectedValue IsNot Nothing AndAlso Not String.IsNullOrEmpty(.SelectedValue.ToString) Then
                    str_Search &= " AND (ms_SKU.ProductSubClass_Index = '" & .SelectedValue.ToString & "') "
                End If
            End With

            If Me.chkINT_U.Checked Then
                str_Search &= " AND ( isnull(ms_SKU.INT_U,0) = 1 )"
            End If

            Return str_Search
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Private Sub SetProductClass()
        Dim ProductClass As ms_Product_Class
        Try
            ProductClass = New ms_Product_Class(ms_Product_Class.enuOperation_Type.SEARCH)
            ProductClass.SearchData_Click("", "")

            Dim Row As DataRow = ProductClass.DataTable.NewRow

            With Row
                .Item("ProductClass_ID") = "--- ไม่ระบุ ---"
                .Item("ProductClass_Index") = String.Empty
                .Item("Customer_Index") = String.Empty
            End With

            With ProductClass
                .DataTable.Rows.InsertAt(Row, 0)
                '.DataTable.DefaultView.RowFilter = "customer_index = '" & Me.txtCustomer_Name.Tag.ToString & "'"
                '.DataTable = objDT.DefaultView.Table
                '.DataTable.AcceptChanges()
            End With

            With Me.cboProductClass
                .DisplayMember = "ProductClass_ID"
                .ValueMember = "ProductClass_Index"
                .DataSource = ProductClass.DataTable

                If .Items.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

        Catch Ex As Exception
            Throw Ex
        Finally
            ProductClass = Nothing
        End Try
    End Sub

    Private Sub SetProductSubClassByIndex(Optional ByVal pIndex As String = "")
        Dim ProductSubClass As ms_Product_SubClass
        Try
            ProductSubClass = New ms_Product_SubClass(ms_Product_SubClass.enuOperation_Type.SEARCH)
            ProductSubClass.SearchData_Index("", "", pIndex)

            Dim Row As DataRow = ProductSubClass.DataTable.NewRow

            With Row
                .Item("ProductSubClass_ID") = "--- ไม่ระบุ ---"
                .Item("ProductSubClass_Index") = String.Empty
            End With

            ProductSubClass.DataTable.Rows.InsertAt(Row, 0)

            With Me.cboProductSubClass
                .DisplayMember = "ProductSubClass_ID"
                .ValueMember = "ProductSubClass_Index"
                .DataSource = ProductSubClass.DataTable

                If .Items.Count > 0 Then
                    .SelectedIndex = 0
                End If
            End With

        Catch Ex As Exception
            Throw Ex
        Finally
            ProductSubClass = Nothing
        End Try
    End Sub

#End Region

#Region "EVENT CONTROL"

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmProductSKU_Des
            frm.SaveType = 0 ' Mode for adding new SKU
            frm.ShowDialog()

            Me.ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' Double click is the same as Edit button.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSKU_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSKU.CellDoubleClick
        ' Check Row
        Try
            If Me.grdSKU.Rows.Count > 0 And e.RowIndex >= 0 Then
                Dim frm As New frmProductSKU_Des
                ' Use label to store selected SKU Index
                '  frm.lbSKUIndex.Text = grdSKU.Rows(e.RowIndex).Cells("ColumnSKU_Index").Value.ToString
                frm.Sku_Index = grdSKU.Rows(e.RowIndex).Cells("ColumnSKU_Index").Value.ToString
                frm.SaveType = 1 ' Mode for editing SKU data
                frm.ShowDialog()

                Me.ShowGrdSKU()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' This function deletes the selected SKU record from system.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks> The deleted record is in fact still in database.
    ''' We only set status id = -1.
    ''' For auto adding Product mode, we will also delete product.</remarks>
    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdSKU.Rows.Count <= 0 Then Exit Sub
            Dim blnResult As Boolean = False
            Dim blnResultRatio As Boolean = False
            Dim ProductRatio_Type As String = "0"

            'TODO: DO NOT use SQL Command here.
            Dim objDB As New SQLCommands
            objDB.SQLComand("SELECT Config_Value FROM config_CustomSetting WHERE Config_Key = 'Product_SkuRatio'")

            If objDB.DataTable.Rows.Count > 0 Then
                ProductRatio_Type = objDB.DataTable.Rows(0).Item("Config_Value").ToString()
            End If

            If grdSKU.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If

            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                ' First Delete SKU by updating status = -1
                Dim objSKU As New ms_SKU(ms_SKU.enuOperation_Type.DELETE)

                'dong Add 2010/01/05 ตรวจสอบว่ามีการใช้งานสินค้านี้อยู่ใหม
                If (objSKU.chkSKU_USED(grdSKU.CurrentRow.Cells("ColumnSKU_Index").Value.ToString)) Then
                    Dim dtChkSku As New DataTable
                    dtChkSku = objSKU.GetDataTable
                    With dtChkSku.Rows(0)
                        MessageBox.Show("Receiving(กำลังรับ)       : " & .Item("Receive") & Environment.NewLine _
                                      & "Picking(กำลังเบิก)         : " & .Item("WithDraw") & Environment.NewLine _
                                      & "Transferring(กำลังโอน) : " & .Item("Transfer") & Environment.NewLine _
                                      & "Borrowing(กำลังยืม)      : " & .Item("Borrow") & Environment.NewLine _
                                      & "Returning(กำลังคืน)       : " & .Item("BorrowReturn") & Environment.NewLine _
                                      & "Balance(คงเหลือ)           : " & .Item("Balance") & Environment.NewLine _
                        , "สินค้ากำลังใช้งานไม่สามรถลบได้")
                    End With

                    Exit Sub
                End If

                blnResult = objSKU.DeleteSKU_Master(grdSKU.CurrentRow.Cells("ColumnSKU_Index").Value.ToString())

                If blnResult Then
                    ' If SKU is deleted, then delete SKU Ratio
                    Dim objSKURatio As New ms_SKURatio(ms_SKURatio.enuOperation_Type.DELETE)
                    blnResultRatio = objSKURatio.Delete_AllRatio(grdSKU.CurrentRow.Cells("ColumnSKU_Index").Value.ToString())
                    ' Then if we are in the mode to auto create product, then auto delete product as well.
                    If ProductRatio_Type = "1" Then
                        Dim objProduct As New ms_Product(ms_Product.enuOperation_Type.DELETE)
                        blnResult = objProduct.Delete_Master(grdSKU.CurrentRow.Cells("ColumnProduct_Index").Value.ToString())
                    End If
                End If


                'KSL : Add on fix bug duplicate sku_id
                If blnResult Then
                    Dim xCon As New DBType_SQLServer
                    xCon.DBExeNonQuery(String.Format("UPDATE ms_Product set status_id = -1,Product_Id = Product_Id + '-X'  where Product_Id = '{0}'", grdSKU.CurrentRow.Cells("ColumnSKU_ID").Value.ToString()))
                    'add for extend delete sku.
                    xCon.DBExeNonQuery(String.Format("UPDATE ms_SKU set Sku_Id = Sku_Id + '-X' where Sku_Index = '{0}'", grdSKU.CurrentRow.Cells("ColumnSKU_Index").Value.ToString()))
                End If

            

                'Me.ShowgrdSKU(gintRowStart, gintRowEnd)
            End If
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If grdSKU.Rows.Count <= 0 Then Exit Sub
            Dim frm As New frmProductSKU_Des
            ' Use label to store selected SKU Index
            ' frm.lbSKUIndex.Text = lbSkuIndex.Text
            frm.Sku_Index = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnSKU_Index").Value.ToString
            frm.SaveType = 1
            frm.ShowDialog()

            ShowGrdSKU()
            '   Me.ShowgrdSKU(gintRowStart, gintRowEnd)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            gboolSearch = True
            Me.txtPageIndex.Text = 1
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub btnClear_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear_Search.Click
        Try
            gboolSearch = False
            For Each crl As Control In gbSearch.Controls
                Select Case True
                    Case TypeOf crl Is TextBox
                        crl.Tag = String.Empty
                        crl.Text = String.Empty
                    Case TypeOf crl Is ComboBox
                        Select Case crl.Name
                            Case "cbSearchSelectLang", "cbOperator"
                                CType(crl, ComboBox).SelectedIndex = -1
                            Case "cbProductType", "cboProductClass", "cboProductSubClass"
                                If (CType(crl, ComboBox).Items.Count > 0) Then CType(crl, ComboBox).SelectedIndex = 0
                        End Select
                End Select
            Next
            'cbSupplier.SelectedIndex = 0
            'cbCustomer.SelectedIndex = 0
            chkCustomer.Checked = False
            chkSupplier.Checked = False
            'cbCustomer.Enabled = False
            'cbSupplier.Enabled = False

            'Me.ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    ''' <summary>
    ''' This sub is in fact to pop up BOM screen.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAddBom_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddBom.Click
        Try
            If grdSKU.Rows.Count <= 0 Then Exit Sub

            Dim frmBom As New frmBomItem
            Dim Sku_Index As String
            Dim objDB1 As New tb_PackingBom

            Sku_Index = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnSKU_Index").Value.ToString


            frmBom.Sku_Index = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnSKU_Index").Value.ToString
            frmBom.weight = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnWeight").Value.ToString
            frmBom.Unit = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnPackage").Value.ToString
            frmBom.Unit_Index = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnPackage_Index").Value.ToString

            If objDB1.isExistID(Sku_Index) = True Then
                'Update Bom
                frmBom.Bom_Index = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColBom_Index").Value.ToString
                frmBom.SaveType = 1
            Else
                'Insert Bom
                frmBom.SaveType = 0
            End If

            frmBom.ShowDialog()

            Me.ShowGrdSKU()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub grdSKU_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSKU.SelectionChanged
        Try
            If grdSKU.CurrentRow IsNot Nothing Then
                If Me.grdSKU.Rows.Count > 0 Then
                    lbSkuIndex.Text = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnSKU_Index").Value.ToString
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
            ' TODO: Do something. Leaving  will not show error!!!
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

    ''' <summary>
    ''' This sub is used when user enter number of page to go directly.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtPageIndex_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPageIndex.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Enter Then
                If IsNumeric(Me.txtPageIndex.Text) Then
                    If CInt(Me.txtPageIndex.Text) > CInt(Me.txtTotalPage.Text) Then
                        Me.txtPageIndex.Text = Me.txtTotalPage.Text
                    End If
                    Me.txtPageIndex.Text = Trim(Me.txtPageIndex.Text)
                    '  Calculate_Paging()
                    ShowGrdSKU()
                Else
                    Me.txtPageIndex.Text = 1
                    ' Calculate_Paging()
                    ShowGrdSKU()
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

#Region "PAGING CONTROL"

    Private Sub cborow_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRowPerPage.SelectionChangeCommitted
        Try
            Me.txtPageIndex.Text = 1
            'Calculate_Paging()
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            Me.txtPageIndex.Text += 1
            'Calculate_Paging()
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            Me.txtPageIndex.Text = Me.txtTotalPage.Text
            'Calculate_Paging()
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            Me.txtPageIndex.Text -= 1
            'Calculate_Paging()
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnPageFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageFirst.Click
        Try
            Me.txtPageIndex.Text = 1
            'Calculate_Paging()
            ShowGrdSKU()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub Calculate_Paging()
        ' Calculate Paging 
        Try
            Dim intRowCount As Integer
            Dim intRowPerPage As Integer

            intRowCount = CInt(Me.txtRowCount.Text)
            intRowPerPage = CInt(Me.cboRowPerPage.Text)

            ' row count = 0 ; page = 1 : total page = 1
            If intRowCount = 0 Or (intRowCount <= intRowPerPage) Then
                Me.txtTotalPage.Text = 1
                Me.txtPageIndex.Text = 1
            Else
                Me.txtTotalPage.Text = CInt(intRowCount / intRowPerPage)

                If CInt(Me.txtTotalPage.Text) * intRowPerPage < intRowCount Then
                    Me.txtTotalPage.Text = CInt(Me.txtTotalPage.Text) + 1
                End If
            End If

            Me.txtPageIndex.Text = IIf(IsNumeric(txtPageIndex.Text), Me.txtPageIndex.Text, 1)

            ' Enable button
            If CInt(Me.txtPageIndex.Text) = 1 Then
                gintRowStart = 1
                gintRowEnd = intRowPerPage

                Me.btnPagePrev.Enabled = False
                Me.btnPageFirst.Enabled = False
            Else
                gintRowEnd = CInt(Me.txtPageIndex.Text) * intRowPerPage
                gintRowStart = gintRowEnd - intRowPerPage + 1

                Me.btnPagePrev.Enabled = True
                Me.btnPageFirst.Enabled = True

            End If

            If CInt(Me.txtPageIndex.Text) = CInt(Me.txtTotalPage.Text) Then
                Me.btnPageNext.Enabled = False
                Me.btnPageLast.Enabled = False
            Else
                Me.btnPageNext.Enabled = True
                Me.btnPageLast.Enabled = True
            End If
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

#End Region

    Private Sub btnSeachCustomer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeachCustomer.Click
        Try
            Dim frm As New frmCustmer_Popup
            frm.ShowDialog()

            Dim tmpCustomer_Name As String = ""
            Dim tmpCustomer_Index As String = ""
            tmpCustomer_Name = frm.customerName
            tmpCustomer_Index = frm.Customer_Index

            If Not tmpCustomer_Name = "" Then
                Me.txtCustomer_Name.Text = tmpCustomer_Name
                Me.txtCustomer_Name.Tag = tmpCustomer_Index
            Else
                Me.txtCustomer_Name.Text = ""
            End If

            frm.Close()

            If chkCustomer.Checked Then
                btnSearch_Click(sender, New EventArgs)
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSearchSupplier_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchSupplier.Click

        Try
            Dim frm As New frmSupplier_Popup
            frm.ShowDialog()
            Dim tmpSupplier_Name As String = ""
            Dim tmpSupplier_Index As String = ""
            tmpSupplier_Name = frm.SupplierName
            tmpSupplier_Index = frm.Supplier_Index
            If tmpSupplier_Name <> "" Then
                txtSupplierName.Text = tmpSupplier_Name
                txtSupplierName.Tag = tmpSupplier_Index
            Else
                txtSupplierName.Text = ""
            End If

            frm.Close()

            If Me.chkSupplier.Checked Then
                btnSearch_Click(sender, New EventArgs)
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region "   Show  pic SKU "

    Private Sub grdSKU_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles grdSKU.MouseClick
        Try
            If grdSKU.RowCount <= 0 Then Exit Sub

            'Dim frm As New frmPicSKU_Popup
            Dim strSku_Index As String = ""
            strSku_Index = grdSKU.Rows(grdSKU.CurrentRow.Index).Cells("ColumnSKU_Index").Value.ToString
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
            Dim appSet As New Configuration.AppSettingsReader()
            Dim strUseLocalPath As String = appSet.GetValue("UseLocalPath", GetType(String)) 'AppSettings("UseLocalPath").ToString


            If strUseLocalPath = 0 Then
                Me._DEFAULT_ImagePath = appSet.GetValue("IMAGE_PATH_SKU", GetType(String)) ' AppSettings("IMAGE_PATH_SKU").ToString
            Else

                objCustomSetting.GetConfig_Value("DEFAULT_IMAGE_PATH_SKU", "")
                objDT = objCustomSetting.DataTable

                If objDT.Rows.Count > 0 Then
                    Me._DEFAULT_ImagePath = objDT.Rows(0).Item("Config_Value").ToString
                Else
                    Me._DEFAULT_ImagePath = Application.StartupPath
                End If

                If Not IO.Directory.Exists(Me._DEFAULT_ImagePath) Then
                    IO.Directory.CreateDirectory(Me._DEFAULT_ImagePath)
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

    Private Sub txtPageIndex_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPageIndex.KeyPress
        e.Handled = CurrencyTextBox.NumbericOnly(sender, e.KeyChar, False)
    End Sub

    Private Sub txtSearchSKUID_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchUnitWeight.KeyDown, txtSearchSKUName.KeyDown, txtSearchSKUID.KeyDown, txtSearchSize.KeyDown, txtSearchProductID.KeyDown, txtSearchPackage.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            btnSearch_Click(sender, New EventArgs)
        End If
    End Sub

    Private Sub btnCusRefId_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCusRefId.Click
        Try
            If grdSKU.Rows.Count <= 0 Then
                Exit Sub
            End If
            Dim frm As New frmProduct_SKU_CusRefId
            frm.Sku_Index = grdSKU.CurrentRow.Cells("ColumnSKU_Index").Value
            frm.Sku_Id = grdSKU.CurrentRow.Cells("ColumnSKU_ID").Value
            frm.Sku_Name = grdSKU.CurrentRow.Cells("ColumnDes").Value
            frm.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub cboProductClass_SelectionChangeCommitted(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboProductClass.SelectionChangeCommitted
        Try
            If Me.cboProductClass.SelectedValue IsNot Nothing Then
                SetProductSubClassByIndex(Me.cboProductClass.SelectedValue.ToString)
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub frmProduct_SKU_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigProduct_SKU
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2001)
                    oFunction.SW_Language_Column(Me, Me.grdSKU, 2001)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnProductType_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProductType_Popup.Click
        Try
            Dim _popup As New frmProductType_Popup()
            _popup.ShowDialog()
            If (_popup.DialogResult = Windows.Forms.DialogResult.OK) Then
                Me.txtProductType.Tag = _popup.ProductType_Index
                Me.txtProductType.Text = _popup.ProductType_Desc
            ElseIf (_popup.DialogResult = Windows.Forms.DialogResult.Cancel) Then
                Me.txtProductType.Tag = ""
                Me.txtProductType.Text = ""
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSkuMinMaxByWH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSkuMinMaxByWH.Click
        Try
            If (Me.grdSKU.Rows.Count <= 0) Then Exit Sub
            If (Me.grdSKU.CurrentRow Is Nothing) Then Exit Sub
            Dim Sku_Index As String = Me.grdSKU.CurrentRow.Cells("ColumnSKU_Index").Value
            Dim _frm As New frmSkuMinMaxByWH(Sku_Index)
            _frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

End Class