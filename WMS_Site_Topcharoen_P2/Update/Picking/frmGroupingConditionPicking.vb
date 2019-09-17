Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
'Imports WMS_STD_OUTB
Imports System.ComponentModel
Imports System.Configuration.ConfigurationManager

Public Class frmGroupingConditionPicking


    Dim dtSalesOrderPlan As New DataTable
    Dim dtSalesOrderGroup As New DataTable


    Public Document_Group_Name As String = ""


    Private Withdraw_Index As String = ""

    Private DocumentType_Index As String = ""

    Private Customer_Index As String = ""

    Private ISHASWITHDRAW As Boolean = False

    Private _arrSalesOrder_Index As String()
    Public Property arrSalesOrder_Index() As String()
        Get
            Return _arrSalesOrder_Index
        End Get
        Set(ByVal value As String())
            _arrSalesOrder_Index = value
        End Set
    End Property
    Private _DocumentType_Index As String
    Public Property DocumentType_Index2() As String
        Get
            Return _DocumentType_Index
        End Get
        Set(ByVal value As String)
            _DocumentType_Index = value
        End Set
    End Property



    Private isDelete As Boolean = True
    Private isSave As Boolean = False
    'Private isChemical As Boolean = False

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub CreateWithdraw()
        Try
            If Withdraw_Index = "" Then
                Dim obj As New GrouppingConditionPicking()
                Withdraw_Index = obj.CreateWTH(Me.cboDocumentType.SelectedValue, Customer_Index)
                txtWithdraw_No.Text = obj.getWithdrawNo(Withdraw_Index)

                Me.cboDocumentType.Enabled = False
                DocumentType_Index = Me.cboDocumentType.SelectedValue
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub




    Public Sub UpdateIsWithdraw( _
              Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing)
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Dim obj As New DBType_SQLServer
        Try




            If _Connection Is Nothing Then
                _Connection = obj.Connection
                With obj.SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = obj.Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            Dim dt As New DataTable
            dt = utilDatatable.GroupDataTable(dtSalesOrderPlan, New String() {"SalesOrder_Index"})
            Dim objGrouppingConditionPicking As New GrouppingConditionPicking
            If dt.Rows.Count > 0 Then
                Dim SalesOrder_IndexL As New List(Of String)
                For Each dr As DataRow In dt.Rows
                    If objGrouppingConditionPicking.IsCreateWithdraw_Set(dr("SalesOrder_Index"), _Connection, _myTrans) Then
                        SalesOrder_IndexL.Add(dr("SalesOrder_Index"))
                    End If
                Next
                _arrSalesOrder_Index = SalesOrder_IndexL.ToArray
            End If




            If IsNotPassTransaction Then _myTrans.Commit()
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then obj.SQLServerCommand.Connection.Close()
        End Try
    End Sub



    Private Sub GetSalesOrderPlan(Optional ByVal strWhere As String = "")
        Try
            If _arrSalesOrder_Index.Length > 0 Then


                dtSalesOrderPlan = New GrouppingConditionPicking().GetSalesOrderPlan(_arrSalesOrder_Index, strWhere)
                dgvItem.DataSource = dtSalesOrderPlan

                setColordgvItem()

                If Me.dgvItem.Rows.Count <= 0 Then
                    chkFullFill.Enabled = False
                    btnCreateWithdraw.Enabled = False
                    btnSave.Enabled = False
                    btnPrinter.Enabled = False
                End If
            Else
                If dtSalesOrderPlan IsNot Nothing Then
                    dtSalesOrderPlan.Rows.Clear()
                End If

                chkFullFill.Enabled = False
                btnCreateWithdraw.Enabled = False
                btnSave.Enabled = False
                btnPrinter.Enabled = False
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub setColordgvItem()
        Try

            If Me.dgvItem.Rows.Count > 0 Then
                For i As Integer = 0 To Me.dgvItem.Rows.Count - 1
                    If CDec(Me.dgvItem.Rows(i).Cells("col_Total_Qty").Value) = 0 Then
                        Me.dgvItem.Rows(i).DefaultCellStyle.BackColor = Color.YellowGreen
                    End If
                Next
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub



    Private Sub GroupDatatableSalesOrderGroup()
        Try
            If dtSalesOrderPlan.Rows.Count > 0 Then
                'Dim Source As DataTable
                'dtSalesOrderGroup = Source.DefaultView.ToTable(True, "", "")
                'dtSalesOrderGroup.Columns.Add("Sum")
                'For Each row As DataRow In dtSalesOrderGroup.Rows
                '    row.Item("Sum") = Source.Compute("SUM(Qty)", String.Format("ProductType = '{0}' AND Condition2 = '{1}'", row.Item("ProductType"), row.Item("Condition2")))
                'Next

                dtSalesOrderGroup = New GrouppingConditionPicking().GroupDatatableSalesOrderGroup(dtSalesOrderPlan)
                dtSalesOrderGroup.AcceptChanges()

                'Dim xdrCheck() As DataRow = dtSalesOrderGroup.Select(String.Format("Sku_id='{0}'", "FA000202"), "")
                'If xdrCheck.Length > 1 Then
                '    Dim dt As New DataTable
                '    dt = dtSalesOrderGroup.Clone
                '    For Each dr As DataRow In xdrCheck
                '        dt.Rows.Add(dr.ItemArray)
                '    Next
                'End If

                If utilDatatable.GroupDataTable(dtSalesOrderGroup, New String() {"Customer_Index"}).Rows.Count > 1 Then
                    btnCreateWithdraw.Enabled = False
                    btnSave.Enabled = False
                    btnPrinter.Enabled = False
                    W_MSG_Information(" เจ้าของสินค้ามากกว่า 1 ไม่สามารถเบิกได้ ")
                Else
                    Customer_Index = dtSalesOrderGroup.Rows(0)("Customer_Index").ToString
                End If
            End If
            dgvItemGroup.DataSource = dtSalesOrderGroup
            'Show Record
            Me.lblRecord_so.Text = Me.dgvItem.RowCount & Me.lblRecord_so.Tag.ToString
            Me.lblRecord_sogroup.Text = Me.dgvItemGroup.RowCount & Me.lblRecord_sogroup.Tag.ToString

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub getDocumentType(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New WMS_STD_Master_Datalayer.ms_DocumentType(WMS_STD_Master_Datalayer.ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(Process_Id)
            objDT = objClassDB.DataTable


            With cboDocumentType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            Dim DocumentRef As String = ""
            Dim clsDOCType As New cls_KSL_SINO
            DocumentRef = clsDOCType.getReferenceDocSO(10, _DocumentType_Index)
            If Not DocumentRef Is Nothing Then
                cboDocumentType.SelectedValue = DocumentRef
            End If

            ' *************************************

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


    Private Sub frmGroupingConditionPicking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Me.pnlProgress.Visible = False

            'Call New utilCultureInfo().DefaultCulture()
            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")

            txtWV_User.Text = W_Module.WV_UserFullName

            dgvItem.AutoGenerateColumns = False
            dgvItemGroup.AutoGenerateColumns = False
            'KSL Visible ซ่อนไว้ก่อน
            'Me.col_Customer_Type_Desc.Visible = False
            Me.col_Branch_Id.Visible = False
            Me.col_PLot.Visible = False
            Me.col_SO_Exp_Date.Visible = False
            Me.col_QtyCT.Visible = False
            Me.col_QtyPC.Visible = False
            Me.col_MinAgeRemain.Visible = False
            Me.col_Shelf_Life.Visible = False
            Me.DataGridViewTextBoxColumn5.Visible = False
            Me.DataGridViewTextBoxColumn8.Visible = False
            'Me.Column1.Visible = False
            Me.col_btnEditSO.Visible = False

            Me.getDocumentType(2)

            Me.GetSalesOrderPlan(" AND IsCreateWithdraw = 0 ")
            Me.UpdateIsWithdraw()
            Me.GetSalesOrderPlan()

            Dim drr() As DataRow
            drr = dtSalesOrderPlan.Select("Total_Qty_Withdraw > 0")
            If drr.Length > 0 Then
                ISHASWITHDRAW = True
            End If


            Me.GroupDatatableSalesOrderGroup()

            'Add KSL
            Me.btnPrinter.Visible = False

            'Add on KSL : DEFAULT_DOCUMENT_TYPE_SALEORDER_CHEMICAL
            Dim xCon As New DBType_SQLServer
            Dim xDt As New DataTable
            xDt = xCon.DBExeQuery(String.Format("select Config_Value from config_CustomSetting  where Config_Key = '{0}'", "DEFAULT_DOCUMENT_TYPE_SALEORDER_CHEMICAL"))
            Dim xdrArr() As DataRow = xDt.Select(String.Format("Config_Value = '{0}'", Me._DocumentType_Index))
            If xdrArr.Length > 0 Then
                'Me.isChemical = True
                Me.chkFullFill.Enabled = False
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmGroupingConditionPicking_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        Try


            'If dtSalesOrderGroup.Rows.Count > 0 Then
            '    Dim Total_Qty_Withdraw As Decimal = dtSalesOrderGroup.Compute("SUM(Total_Qty_Withdraw)", "")
            '    If Total_Qty_Withdraw = 0 Then
            '        Call New GrouppingConditionPicking().DeleteWithdraw(Me.Withdraw_Index)
            '    Else
            '        If W_MSG_Confirm("ต้องการบันทึกหรือไม่?") = Windows.Forms.DialogResult.Yes Then Exit Sub
            '        Call New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA).Withdraw_Cancel(Me.Withdraw_Index)
            '    End If
            'End If


            'If isDelete Then
            '    Call New GrouppingConditionPicking().DeleteWithdraw(Me.Withdraw_Index)

            '    If isSave Then
            '        If W_MSG_Confirm("ต้องการบันทึกหรือไม่?") = Windows.Forms.DialogResult.Yes Then Exit Sub
            '        Call New WithdrawTransaction(WithdrawTransaction.enuOperation_Type.CHECK_DATA).Withdraw_Cancel(Me.Withdraw_Index)
            '    End If
            'End If


            FormCloseing(Me.Withdraw_Index)





        Catch ex As Exception
            W_MSG_Error(ex.Message)
            e.Cancel = True
        End Try

    End Sub

    Private Sub dgvItem_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dgvItem.Sorted
        Try


            setColordgvItem()

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub FormCloseing(ByVal Withdraw_index As String, _
              Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing)
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Dim obj As New DBType_SQLServer
        Try




            If _Connection Is Nothing Then
                _Connection = obj.Connection
                With obj.SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = obj.Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If



            If isSave = False Then
                Dim CountItem As Integer = New GrouppingConditionPicking().getCountItemWithdraw(Withdraw_index)
                If CountItem > 0 Then
                    ' If W_MSG_Confirm("ต้องการบันทึกหรือไม่?") = Windows.Forms.DialogResult.Yes Then
                    'Call New GrouppingConditionPicking().UpdateWithdrawNo(Me.Withdraw_Index, _Connection, _myTrans)
                    'Else
                    'Call New WMS_STD_OUTB_WithDraw_Datalayer.WithdrawTransaction(WMS_STD_OUTB_WithDraw_Datalayer.WithdrawTransaction.enuOperation_Type.CHECK_DATA).Withdraw_Cancel(Me.Withdraw_Index)
                    'End If

                    Call New WMS_Site_Topcharoen_P2.WithdrawTransaction(WMS_Site_Topcharoen_P2.WithdrawTransaction.enuOperation_Type.CHECK_DATA).Withdraw_Cancel(Me.Withdraw_Index)

                Else
                    Call New GrouppingConditionPicking().DeleteWithdraw(Me.Withdraw_Index)
                End If
            End If



            Dim objGrouppingConditionPicking As New GrouppingConditionPicking()

            For Each strSalesOrder_Index As String In _arrSalesOrder_Index
                objGrouppingConditionPicking.IsCreateWithdraw_Clear(strSalesOrder_Index, _Connection, _myTrans)
            Next



            If IsNotPassTransaction Then _myTrans.Commit()
        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then obj.SQLServerCommand.Connection.Close()
        End Try
    End Sub

    Public Function SaveWithdraw(ByVal Withdraw_index As String, _
                  Optional ByVal _Connection As SqlClient.SqlConnection = Nothing, Optional ByVal _myTrans As SqlClient.SqlTransaction = Nothing) As Boolean
        Dim IsNotPassTransaction As Boolean = False
        Dim Result As String = ""
        Dim obj As New DBType_SQLServer
        Try




            If _Connection Is Nothing Then
                _Connection = obj.Connection
                With obj.SQLServerCommand
                    .Connection = _Connection
                    If .Connection.State = ConnectionState.Open Then .Connection.Close()
                    .Connection.Open()
                    .Transaction = _myTrans
                End With
                _myTrans = obj.Connection.BeginTransaction()
                IsNotPassTransaction = True
            End If


            'If dtSalesOrderGroup.Compute("sum(Total_Qty)", "") > 0 Then Exit Sub

            If Withdraw_index.Length = 0 Then
                Throw New Exception("Withdraw_Index Not found")
            End If

            If New GrouppingConditionPicking().getWithdrawItemCount(Withdraw_index, _Connection, _myTrans) = 0 Then
                Throw New Exception("Withdraw Not found")
            End If



            If W_MSG_Confirm("ต้องการบันทึกหรือไม่?") = Windows.Forms.DialogResult.Yes Then
                If New GrouppingConditionPicking().UpdateWithdrawNo(Me.Withdraw_Index, Me.txtComment.Text.Trim, _Connection, _myTrans) <> "" Then
                    isSave = True
                    btnCreateWithdraw.Enabled = False
                    btnSave.Enabled = False
                    btnPrinter.Enabled = True

                    'KSL : Fix bug reset staus
                    Dim objcon As New DBType_SQLServer
                    Dim xSql As String = ""
                    xSql &= " Update tb_WithdrawItem "
                    xSql &= " set TransportManifest_Index = (select top 1  tb_TransportManifest.TransportManifest_Index"
                    xSql &= " 								from tb_SalesOrder SO "
                    xSql &= " 									inner join tb_TransportManifest on tb_TransportManifest.TransportManifest_No = SO.TransportManifest_No"
                    xSql &= " 								WHERE SO.SalesOrder_Index =tb_WithdrawItem.DocumentPlan_Index and  tb_WithdrawItem.Plan_Process = 10)"
                    xSql &= " "
                    xSql &= " where Withdraw_Index = '" & Me.Withdraw_Index & "'"
                    objcon.DBExeNonQuery(xSql, _Connection, _myTrans)
                    objcon = Nothing


                End If
            End If






            If IsNotPassTransaction Then _myTrans.Commit()

            Return True

        Catch ex As Exception
            If IsNotPassTransaction Then _myTrans.Rollback()
            Throw ex
        Finally
            If IsNotPassTransaction Then obj.SQLServerCommand.Connection.Close()
        End Try
    End Function

    Private Sub dgvItem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvItem.CellClick
        Try
            If e.RowIndex <= -1 Then
                Exit Sub
            End If
            If e.ColumnIndex <= -1 Then
                Exit Sub
            End If

            If isSave Then Exit Sub

            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToLower
                Case "col_btneditso"
                    Dim Total_Qty As Decimal = Me.dgvItem.Rows(e.RowIndex).Cells("col_Total_Qty").Value
                    If Total_Qty <= 0 Then Exit Sub


                    Dim frm As New frmEditSOItem
                    frm.SalesOrderItem_Index = Me.dgvItem.Rows(e.RowIndex).Cells("col_SalesOrderItem_Index").Value
                    frm.ShowDialog()

                    GetSalesOrderPlan()

                    GroupDatatableSalesOrderGroup()




                Case "col_btnmanual"
                    CreateWithdraw()

                    Dim Total_Qty As Decimal = Me.dgvItem.Rows(e.RowIndex).Cells("col_Total_Qty").Value
                    If Total_Qty <= 0 Then Exit Sub
                    Dim frm As New frmPicking_Reserv_V4(DocumentType_Index, Me.Withdraw_Index, Me.dgvItem.Rows(e.RowIndex).Cells("col_SalesOrder_No").Value, Me.dgvItem.Rows(e.RowIndex).Cells("col_SalesOrder_Index").Value, Me.dgvItem.Rows(e.RowIndex).Cells("col_SalesOrderItem_Index").Value, 10, WMS_STD_OUTB.frmPicking_Reserv_V4.Operation.Withdraw)

                    frm.DocumentPlan_Process = 2
                    frm.DocumentPlan_Index = Me.Withdraw_Index
                    frm.withdraw_index = Me.Withdraw_Index


                    frm.Customer_Id = Me.dgvItem.Rows(e.RowIndex).Cells("col_Customer_Id").Value.ToString.Trim
                    frm.Customer_Index = Me.dgvItem.Rows(e.RowIndex).Cells("col_Customer_Index").Value.ToString.Trim
                    frm.Customer_Name = Me.dgvItem.Rows(e.RowIndex).Cells("col_Customer_Name").Value.ToString.Trim


                    frm.WithDraw_Date = Now

                    frm.Sku_Id = Me.dgvItem.Rows(e.RowIndex).Cells("col_Sku_Id").Value.ToString.Trim
                    frm.Sku_Index = Me.dgvItem.Rows(e.RowIndex).Cells("col_Sku_Index").Value.ToString.Trim
                    frm.Sku_Name = Me.dgvItem.Rows(e.RowIndex).Cells("col_Sku_Name").Value.ToString.Trim


                    frm.Qty_Reserv = Total_Qty
                    frm.Max_Plan_Reserv = Total_Qty

                    frm.Package_Index_Begin = Me.dgvItem.Rows(e.RowIndex).Cells("col_Package_Index").Value.ToString.Trim


                    frm.txtQty_Reserv.ReadOnly = False
                    frm.txtQty_Reserv.BackColor = Color.White

                    frm.ItemStatus_Index = Me.dgvItem.Rows(e.RowIndex).Cells("col_ItemStatus_Index").Value.ToString.Trim

                    frm.Plot = Me.dgvItem.Rows(e.RowIndex).Cells("col_PLot").Value.ToString.Trim


                    frm.WithdrawType = 1
                    frm.ERP_location = ""


                    ' Fixed SKU
                    'frm.btnSeachSku.Enabled = False
                    frm.rdbAutoPicking.Checked = True
                    frm.VisibleAutoPick = False
                    frm.rdbCustom.Checked = True

                    Dim AgeRemain As Integer = 0
                    If CInt(Me.dgvItem.Rows(e.RowIndex).Cells("col_Shelf_Life").Value.ToString) >= 0 Then
                        AgeRemain = CInt(Me.dgvItem.Rows(e.RowIndex).Cells("col_Shelf_Life").Value.ToString)
                    ElseIf CInt(Me.dgvItem.Rows(e.RowIndex).Cells("col_Day_picking").Value.ToString) >= 0 Then
                        AgeRemain = CInt(Me.dgvItem.Rows(e.RowIndex).Cells("col_Day_picking").Value.ToString)
                    ElseIf CInt(Me.dgvItem.Rows(e.RowIndex).Cells("col_MinAgeRemain").Value.ToString) >= 0 Then
                        AgeRemain = CInt(Me.dgvItem.Rows(e.RowIndex).Cells("col_MinAgeRemain").Value.ToString)
                    End If

                    frm.UseAgeRemain = True
                    frm.AgeRemain = AgeRemain



                    If IsDate(Me.dgvItem.Rows(e.RowIndex).Cells("col_SO_Exp_Date").Value) Then
                        frm.isExp_Date = True
                        frm.Exp_Date = Me.dgvItem.Rows(e.RowIndex).Cells("col_SO_Exp_Date").Value.ToString

                    Else
                        If IsDate(Me.dgvItem.Rows(e.RowIndex).Cells("col_LastExp_date").Value.ToString) Then
                            frm.Exp_Date = Me.dgvItem.Rows(e.RowIndex).Cells("col_LastExp_date").Value.ToString
                        End If
                    End If







                    frm.ShowDialog()




                    GetSalesOrderPlan()
                    GroupDatatableSalesOrderGroup()


                Case "col_btncheckstock"
                    If Me.dgvItem.Rows(e.RowIndex).Cells("col_Sku_Id").Value Is Nothing Then
                        Exit Sub
                    End If
                    Dim frm As New frmCheckStock_By_Condition_Update
                    frm.SkuID = Me.dgvItem.Rows(e.RowIndex).Cells("col_Sku_Id").Value
                    frm.SkuName = Me.dgvItem.Rows(e.RowIndex).Cells("col_Sku_Name").Value
                    frm.Customer_Index = Me.dgvItem.Rows(e.RowIndex).Cells("col_Customer_Index").Value
                    frm.ItemStatus_Idex = Me.dgvItem.Rows(e.RowIndex).Cells("col_ItemStatus_Index").Value
                    'frm.chkCustomer.Checked = True
                    frm.chkSku.Checked = True
                    frm.CallBy = "SO"
                    frm.ShowDialog()

            End Select






        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If ISHASWITHDRAW Then
                If W_MSG_Confirm("ต้องการสร้างอีกครั้งหรือไม่ ") = Windows.Forms.DialogResult.No Then Exit Sub
            End If

            'If Document_Group_Name = "SALE" Then
            '    Dim chkConfirm As Boolean = True
            '    If Me.dgvItem.Rows.Count > 0 Then
            '        For i As Integer = 0 To Me.dgvItem.Rows.Count - 1
            '            If CDec(Me.dgvItem.Rows(i).Cells("col_Total_Qty").Value) > 0 Then
            '                chkConfirm = False
            '            End If
            '        Next
            '    End If

            '    If chkConfirm = False Then
            '        W_MSG_Information("การขายสินค้า ไม่สามารถสั่งหยิบสินค้าได้ถ้าสินค้าไม่ครบ")
            '        Exit Sub
            '    End If
            'End If


            If Me.SaveWithdraw(Me.Withdraw_Index) Then
                W_MSG_Information_ByIndex("1")
            Else
                W_MSG_Information_ByIndex("2")
            End If


            txtWithdraw_No.Text = New GrouppingConditionPicking().getWithdrawNo(Withdraw_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnPrinter_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrinter.Click
        Try

            If isSave Then

                If Me.Withdraw_Index <> "" Then

                    Dim oconfig_Report As New WMS_STD_Master.config_Report
                    Dim Report_Name As String = "wth_pickinglist"

                    If Report_Name.ToLower = "wth_pickinglist" Then

                        Dim config As New WMS_STD_Master.config_Report
                        Dim cry As New Object 'rptPrintOut_Picking_List
                        Dim odt As New DataTable
                        Dim ods As New DataSet

                        config.GetDataReport(Report_Name)
                        odt = config.GetDataTable

                        Dim dt As New DataTable
                        dt = config.GetReportData(odt.Rows(0)("View_Name").ToString, "And Withdraw_index ='" & Me.Withdraw_Index & "'")


                        Dim bar As New Barcode_New

                        dt.Columns.Add("Withdraw_No_PIC", GetType(Byte()))
                        dt.Columns.Add("BarcodeRack_PIC", GetType(Byte()))


                        For Each dr As DataRow In dt.Rows

                            dr("Withdraw_No_PIC") = bar.BarcodeByte(dr("Withdraw_No").ToString, False)
                            dr("BarcodeRack_PIC") = bar.BarcodeByte(dr("Room_Id").ToString, False)

                        Next

                        ods.Tables.Add(dt)
                        ods.DataSetName = odt.Rows(0)("DataSet_Name").ToString
                        ods.Tables(0).TableName = odt.Rows(0)("DataTable_Name").ToString

                        cry.SetDataSource(ods)

                        cry.PrintToPrinter(1, False, 0, 0)

                        cry.Database.Dispose()
                        cry.Close()
                        cry.Dispose()

                    End If

                    Report_Name = "rptgoods-check-list"

                    If Report_Name.ToLower = "rptgoods-check-list" Then

                        Dim config As New WMS_STD_Master.config_Report
                        Dim cry As New Object 'rptPrintOutGoods_check_list
                        Dim odt As New DataTable
                        Dim ods As New DataSet

                        config.GetDataReport(Report_Name)
                        odt = config.GetDataTable

                        Dim dt As New DataTable
                        dt = config.GetReportData(odt.Rows(0)("View_Name").ToString, "And Withdraw_index ='" & Me.Withdraw_Index & "'")


                        Dim bar As New Barcode_New

                        dt.Columns.Add("Withdraw_No_PIC", GetType(Byte()))
                        dt.Columns.Add("Zone_desc_PIC", GetType(Byte()))


                        For Each dr As DataRow In dt.Rows

                            dr("Withdraw_No_PIC") = bar.BarcodeByte(dr("Withdraw_No").ToString, False)
                            dr("Zone_desc_PIC") = bar.BarcodeByte(dr("Zone_desc").ToString, False)

                        Next

                        ods.Tables.Add(dt)
                        ods.DataSetName = odt.Rows(0)("DataSet_Name").ToString
                        ods.Tables(0).TableName = odt.Rows(0)("DataTable_Name").ToString

                        cry.SetDataSource(ods)

                        cry.PrintToPrinter(1, False, 0, 0)

                        cry.Database.Dispose()
                        cry.Close()
                        cry.Dispose()


                    Else
                        Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And Withdraw_index ='" & Me.Withdraw_Index & "'")
                        oReport.LoadReport().PrintToPrinter(1, False, 0, 0)
                    End If



                    Dim dtTF As DataTable = New GrouppingConditionPicking().getTransferStatus_IndexByWithdraw_Index(Me.Withdraw_Index)

                    For Each dr As DataRow In dtTF.Rows
                        Dim oReportTF As New WMS_STD_TMM_Report.Loading_Report("Tranfer_PrintOut", "And TransferStatus_Index ='" & dr("TransferStatus_Index").ToString & "'")
                        Dim cry As CrystalDecisions.CrystalReports.Engine.ReportDocument
                        cry = oReportTF.LoadReport()
                        cry.PrintToPrinter(1, False, 0, 0)
                        cry.Close()
                        cry.Dispose()
                    Next

                End If


            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnWithdrawView_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWithdrawView.Click
        Try
            'ซ่อนไว้กอ่นอาจมี Bug
            If Me.txtWithdraw_No.Text.Trim.ToString = "" Then Exit Sub
            If Me.txtWithdraw_No.Text.Trim.ToString.Substring(0, 3) = "TMP" Then
                W_MSG_Information("ไม่สามารถดูได้ ใบเบิกยังไม่บันทึก")
                Exit Sub
            End If

            Dim frm As New frmWithdrawAsset_V4(frmWithdrawAsset_V4.enuOperation_Type.UPDATE, Withdraw_Index)
            frm.DocumentPlan_Index = Withdraw_Index
            frm.DocumentPlan_No = Me.txtWithdraw_No.Text
            frm.ShowDialog()

            'Refresh
            Me.GetSalesOrderPlan()
            Me.GroupDatatableSalesOrderGroup()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#Region "   PregressBar Imports  "

    Private Sub btnCreateWithdraw_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCreateWithdraw.Click
        Try
            If Me.isSave Then Exit Sub
            
            Dim ConfigCustomSetting As New config_CustomSetting
            Dim AutoReplenish As Boolean = ConfigCustomSetting.getConfig_Key_USE("USE_AUTO_REPLENISH_SO")

            If AutoReplenish AndAlso Me._arrSalesOrder_Index IsNot Nothing AndAlso Me._arrSalesOrder_Index.Length > 0 Then
                Dim ServiceSo As New clsSO
                Dim DataSO As DataTable = ServiceSo.GetSO_Replenish(arrSalesOrder_Index)
                If DataSO Is Nothing OrElse Not DataSO.Rows.Count > 0 Then
                    Exit Sub
                End If

                Dim strListSku As String = ""
                For Each dr As DataRow In DataSO.Rows
                    strListSku += vbCrLf & dr("Sku_Id")
                Next

                If W_MSG_Confirm("มีสินค้าไม่พอ ต้องการสร้างใบโอนอัติโนมัติ" & vbCrLf & "ของสินค้า " & strListSku & vbCrLf & " ใช่หรือไม่ ?") = Windows.Forms.DialogResult.Yes Then
                    Dim ServiceTransfer As New clsTransfer
                    Dim TransferStatusNo As String = ServiceTransfer.Auto_Replenish_SO(DataSO)
                    W_MSG_Information(String.Format("บันทึกรายการเติมสินค้าเสร็จสิ้นแล้ว เลขที่ใบเติม [ {0} ]", TransferStatusNo))
                End If

                Exit Sub
            End If

            'สร้างใบเบิกรอไว้
            Me.CreateWithdraw()

            Me.ProgressBar1.Style = ProgressBarStyle.Marquee
            Me.pnlProgressBar.Visible = True
            ProgressBar1.Style = ProgressBarStyle.Marquee
            BwgImport.RunWorkerAsync()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally

        End Try
    End Sub
    Private Sub BackgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles BwgImport.RunWorkerCompleted
        Try

            'Progress 
            ProgressBar1.Style = ProgressBarStyle.Blocks
            Me.pnlProgressBar.Visible = False
            'end Progress 

            If strMSGEx <> "" Then
                W_MSG_Information(strMSGEx)
                Exit Sub
            End If
            Me.GetSalesOrderPlan()
            Me.GroupDatatableSalesOrderGroup()

            Dim strMSG As String = ""
            If dtSalesOrderGroup.Rows.Count > 0 Then
                For Each dr As DataRow In dtSalesOrderGroup.Rows
                    If dr("Total_Qty") > 0 Then
                        strMSG &= "SKU ID : " & dr("Sku_Id").ToString & " , NAME : " & dr("Sku_Name").ToString & vbNewLine
                    End If
                Next
            End If

            If strMSG.Length > 0 Then
                W_MSG_Information("รายการสินค้าไม่พอเบิก" & vbNewLine & strMSG)

                If Me.dgvItemGroup.Rows.Count > 0 Then
                    For i As Integer = 0 To Me.dgvItemGroup.Rows.Count - 1
                        If CDec(Me.dgvItemGroup.Rows(i).Cells("DataGridViewTextBoxColumn10").Value) = 0 Then
                            Me.dgvItemGroup.Rows(i).DefaultCellStyle.BackColor = Color.YellowGreen
                        End If
                    Next
                End If
            Else

            End If

            Dim dtTF As DataTable = New GrouppingConditionPicking().getTransferStatus_IndexByWithdraw_Index(Me.Withdraw_Index)
            If dtTF.Rows.Count > 0 Then
                lblAlert.Visible = True
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        Finally
            Me.Cursor = Cursors.Default
        End Try
    End Sub
    Private strMSGEx As String = ""
    Private Sub BackgroundWorker1_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles BwgImport.DoWork
        Try
            Threading.Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("en-GB")
            'Picking FIFO
            Dim oConfig As New config_CustomSetting
            Dim _USE_PICKFACE_RACK_TAG As Boolean = oConfig.getConfig_Key_USE("USE_PICKFACE_RACK_TAG")
            Dim _USE_PICKFACE_BUSKET_TAG As Boolean = oConfig.getConfig_Key_USE("USE_PICKFACE_BUSKET_TAG")

            Dim objGCP As New GrouppingConditionPicking
            'FIFO FG
            strMSGEx = objGCP.CreateAutoPickingWithdraw_v4(Withdraw_Index, DocumentType_Index, dtSalesOrderPlan, dtSalesOrderGroup, Me.chkFullFill.Checked, _USE_PICKFACE_RACK_TAG, _USE_PICKFACE_BUSKET_TAG)


        Catch ex As Exception
            e.Cancel = True
        End Try

    End Sub


#End Region

End Class