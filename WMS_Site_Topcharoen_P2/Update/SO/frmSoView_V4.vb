Imports System.Windows.Forms
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
'Imports WMS_STD_OUTB_WithDraw_Datalayer
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Imports WMS_STD_OUTB_SO_Datalayer
Imports WMS_STD_OUTB_WithDraw


Public Class frmSOView_V4

    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 1

    'Public Document_Group_Name As String = ""

#Region " FORM LOAD "

    ''' <summary>
    ''' Form Load Sub.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Todd 17 Jan 2010 - Add language switching code.
    ''' </remarks>
    Private Sub frmSOView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' ========== Manage Language Functions Begin ==========
        Dim oFunction As New W_Language
        'oFunction.Insert(Me)
        oFunction.SwitchLanguage(Me, 10)

        'Config config_DataGridColumn
        'oFunction.Insert_config_DataGridColumn(Me, Me.grdSOView)
        oFunction.SW_Language_Column(Me, Me.grdSOView, 10)
        oFunction = Nothing
        ' ========== Manage Language Functions End ==========

        'Fix for KSL
        Me.chkPending.Checked = True

        Me.grdSOView.AutoGenerateColumns = False
        Try
            Me.cboRowPerPage.SelectedIndex = 0
            Me.getProcessStatus()
            Me.getProcessStatus_Manifest()
            Me.getDocType()
            Me.getReportName(10)
            Me.getDeleveryDay()
            Me.getDistributionCenter()
            Me.getProductType()
            Me.getTransportRegion()
            Me.getPriority()
            'Me.btnImportSO.Enabled = False
            'Me.btnImport_PRQ.Enabled = False
            'Me.btnImportPRQ_txtfile.Enabled = False
            'Select Case Me.Document_Group_Name
            '    Case "SALE"
            '        Me.btnImportSO.Enabled = True
            '    Case "BOM"
            '        Me.btnImport_PRQ.Enabled = True
            '        Me.btnImportPRQ_txtfile.Enabled = True
            '        Me.col_TransportManifest_No.Visible = False
            'End Select
           
            'Me.getSOViewSearch()
            Me.cboRGB.SelectedIndex = 0
            'Me.cbDeleveryDay.Visible = False
            'Me.Label2.Visible = False
            Me.GBHide.Visible = False
            Me.grbVisible.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

#End Region

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

    Private Sub getDeleveryDay()

        Dim objClassDB As New ms_Customer_Shipping_Location_Day '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetAllDataDay("")
            objDT = objClassDB.DataTable


            Dim cbItem As DataRow
            cbItem = objDT.NewRow
            cbItem("Day_index") = "-11"
            cbItem("Day_Id") = "-11"
            cbItem("Description") = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)


            With cbDeleveryDay
                .DisplayMember = "Description"
                .ValueMember = "Day_index"
                .DataSource = objDT
            End With

            ' *************************************
            cbDeleveryDay.SelectedIndex = cbDeleveryDay.Items.Count - 1



        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

#Region " INITIALIZE CONTROL "
    Private Sub getDocType()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        'Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        'Dim objDT As DataTable = New DataTable
        Dim objClassDB As New DBType_SQLServer
        Dim objDT As DataTable = New DataTable
        Try

            'objClassDB.getDocumentType(10)
            'objDT = objClassDB.DataTable
            Dim xSQL As String = ""
            xSQL &= " SELECT  DocumentType_Index,  Description FROM     ms_DocumentType"
            xSQL &= " WHERE Process_Id= 10 and status_id not in (-1)"
            'xSQL &= " AND Document_Group_Name = '" & Me.Document_Group_Name & "'"
            objDT = objClassDB.DBExeQuery(xSQL)

            Dim cbItem As DataRow
            cbItem = objDT.NewRow
            cbItem("DocumentType_Index") = "-11"
            cbItem("Description") = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)

            With cbSOType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************
            cbSOType.SelectedIndex = cbSOType.Items.Count - 1
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub
    ''' <summary>
    ''' Get Process Status for SO.
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub getProcessStatus()

        Dim objClassDB As New tb_SalesOrder
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus()
            objDT = objClassDB.DataTable

            With cboDocumentStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getProcessStatus_Manifest()

        Dim objClassDB As New tb_SalesOrder
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus(24)
            objDT = objClassDB.DataTable

            Dim drRow As DataRow
            drRow = objDT.NewRow
            drRow("Description") = "ไม่ระบุ"
            drRow("Status") = "-11"
            objDT.Rows.Add(drRow)
            With cboDocumentStatus2
                .DisplayMember = "Description"
                .ValueMember = "Status"
                .DataSource = objDT
            End With

            cboDocumentStatus2.SelectedValue = "-11"

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub


    Private Sub getTransportRegion()

        Dim objClassDB As New ms_TransportRegion(ms_TransportRegion.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            objClassDB.GetAllAsDataTable("")
            objDT = objClassDB.DataTable

            Dim dtrow As DataRow = objDT.NewRow()
            dtrow.Item("TransportRegion_Index") = "-99"
            dtrow.Item("Description") = "ไม่ระบุ"
            objDT.Rows.InsertAt(dtrow, 0)
            cboRegion.BeginUpdate()
            With cboRegion
                .DisplayMember = "Description"
                .ValueMember = "TransportRegion_Index"
                .DataSource = objDT
            End With

            cboRegion.EndUpdate()
            If cboRegion.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub


    Private Sub getPriority()

        Dim objClassDB As New clsMasterPriority()
        Dim objDT As DataTable = New DataTable
        Try
            objDT = objClassDB.getPriority()
            Dim dtrow As DataRow = objDT.NewRow()
            dtrow.Item("Priority_Id") = "-99"
            dtrow.Item("Priority_Name") = "ไม่ระบุ"
            objDT.Rows.InsertAt(dtrow, 0)
            cboRegion2.BeginUpdate()

            With cboPriority
                .DisplayMember = "Priority_Name"
                .ValueMember = "Priority_Id"
                .DataSource = objDT
            End With

            cboPriority.EndUpdate()
            If cboPriority.Items.Count = 0 Then Exit Sub

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub



    Private Sub getProductType()

        Dim objClassDB As New clsSO
        Dim objDT As DataTable = New DataTable

        Try
            objDT = objClassDB.GetProductType()

            Dim drRow As DataRow
            drRow = objDT.NewRow
            drRow("ProductType_Id") = "ไม่ระบุ"
            drRow("ProductType_Index") = "-11"
            objDT.Rows.Add(drRow)
            objDT.DefaultView.Sort = "ProductType_Index ASC"
            With cboProductType
                .DisplayMember = "ProductType_Id"
                .ValueMember = "ProductType_Index"
                .DataSource = objDT
            End With

            cboProductType.SelectedValue = "-11"

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Get print out report.
    ''' </summary>
    ''' <param name="Process_Id"></param>
    ''' <remarks></remarks>
    Private Sub getReportName(ByVal Process_Id As Integer)

        Dim objClassDB As New config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            With cboPrint
                .DisplayMember = "Description"
                .ValueMember = "Report_Name"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

#End Region

#Region " RADIO EVENTS "

    ' ----------------------------------------------------
    ' ------ These sub/functions are for managing search 
    ' ------ criteria textboxes and datetime picker.
    ' ----------------------------------------------------
    Private Sub rdbSO_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSO_Date.CheckedChanged
        Me.dtpBeginDate.Visible = True
        Me.dtpEndDate.Visible = True
        Me.txtKeySearch.Visible = False
        'Me.txtKeySearch.Text = ""
        'Me.lbl_to.Visible = True
    End Sub

    Private Sub rdbSO_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSO_No.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub rdbCarrier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbConsignee.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

#End Region

#Region " BUTTON EVENTS "

    ''' <summary>
    ''' Button to add new SO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmSO
            frm.Icon = Me.Icon
            frm.objStatus = frmSO.enuOperation_Type.ADDNEW
            'frm.Document_Group_Name = Document_Group_Name
            frm.ShowDialog()

            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Button to edit SO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click

        'If grdSOView.Rows.Count = 0 Then Exit Sub
        Try

            If grdSOView.Rows.Count > 0 Then
                'Dim frm As New frmSO
                Dim frm As New frmSO
                ' For editing SO, you must set value of "SalesOrder_Index" and "Status"
                frm.objStatus = frmSO.enuOperation_Type.UPDATE
                frm.SalesOrder_Index = grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value.ToString
                frm.Status = grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_Status").Value.ToString
                'frm.Document_Group_Name = Document_Group_Name
                frm.ShowDialog()
                getSOViewSearch()
            End If

            'If grdSOView.CurrentRow.Cells("Col_Status").Value = "2" Or _
            '    grdSOView.CurrentRow.Cells("Col_Status").Value = "3" Or _
            '    grdSOView.CurrentRow.Cells("Col_Status").Value = "-1" Then

            '    Dim frm As New frmSO
            '    frm.SalesOrder_Index = grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value.ToString
            '    frm.objStatus = frmSO.enuOperation_Type.UPDATE
            '    frm.SalesOrderStatus = True
            '    frm.Icon = Me.Icon
            '    frm.ShowDialog()
            '    Me.getSOViewSearch()

            'ElseIf Me.grdSOView.Rows.Count > 0 Then
            '    Dim frm As New frmSO
            '    frm.SalesOrder_Index = grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value.ToString
            '    frm.objStatus = frmSO.enuOperation_Type.UPDATE
            '    frm.Icon = Me.Icon
            '    frm.ShowDialog()
            '    Me.getSOViewSearch()

            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Button to confirm SO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    ''' ''' 
    ''' 

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            ' ====== TODO: HARDCODE-MSG 
            If grdSOView.Rows.Count <= 0 Then Exit Sub
            Dim SO_Type, SalesOrder_Index, SalesOrder_No, SkuIndex, ItemStatusIndex, DocSalesOrder As String
            CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            'Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.DELETE)
            'objSOTransaction.newSaleOrder_Index = Me.grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value
            'validate chkSelect
            Dim bool_chkSelect As Boolean = False

            For i As Integer = 0 To grdSOView.Rows.Count - 1
                Try
                    Dim strSku_Id_Alert As String = ""
                    If (grdSOView.Rows(i).Cells("chkSelect").Value IsNot Nothing AndAlso grdSOView.Rows(i).Cells("chkSelect").Value.ToString = "1") Then
                        bool_chkSelect = True

                        Dim Order As String = ""
                        Dim oSalesReq As New clsSO
                        Dim DataSalesOrder As DataTable = oSalesReq.GetSalesOrder(Me.grdSOView.Rows(i).Cells("Col_SalesOrder_Index").Value)
                        Dim DataSalesOrderBalance As DataTable = oSalesReq.GetSalesOrderItemBalance()
                        Dim SalesOrderItem, SalesOrderBalance As DataRow()
                        Dim SO_Total_Qty As Decimal

                        Dim CountX, CountZ As Integer
                        SalesOrder_Index = Me.grdSOView.Rows(i).Cells("Col_SalesOrder_Index").Value
                        SalesOrder_No = Me.grdSOView.Rows(i).Cells("Col_SalesOrder_No").Value
                        SalesOrderItem = DataSalesOrder.Select(String.Format("SalesOrder_Index = '{0}'", SalesOrder_Index))

                        SO_Type = "Y" 'Default SO Type
                        CountX = 0
                        CountZ = 0

                        For Each Item_Row As DataRow In SalesOrderItem
                            SkuIndex = Item_Row("Sku_Index").ToString
                            ItemStatusIndex = Item_Row("ItemStatus_Index").ToString
                            SO_Total_Qty = Decimal.Parse(Item_Row("Total_Qty").ToString)


                            If DataSalesOrderBalance.Rows.Count > 0 Then
                                DataSalesOrderBalance.Columns.Item("Total_Qty").ReadOnly = False

                                SalesOrderBalance = DataSalesOrderBalance.Select(String.Format("Sku_Index = '{0}' AND ItemStatus_Index = '{1}' AND Total_Qty >= '{2}'", SkuIndex, ItemStatusIndex, SO_Total_Qty))
                                If SalesOrderBalance.Length > 0 Then
                                    For Each Row As DataRow In SalesOrderBalance
                                        If SO_Total_Qty = 0 Then
                                            Exit For
                                        End If

                                        If SO_Total_Qty > Row.Item("Total_Qty") Then
                                            SO_Total_Qty -= Row.Item("Total_Qty")
                                            Row.Item("Total_Qty") = 0
                                        Else
                                            Row.Item("Total_Qty") -= SO_Total_Qty
                                            SO_Total_Qty = 0
                                        End If
                                    Next

                                    CountX += 1
                                Else
                                    strSku_Id_Alert = strSku_Id_Alert & " " & Item_Row("sku_id").ToString
                                End If
                            End If

                            If Item_Row("IsCaseZ").ToString = "1" Then
                                CountZ += 1
                            End If
                        Next


                        If CountX <> SalesOrderItem.Length Then
                            SO_Type = "X"

                            Dim strError As String = GetMessage_Data("400013") & vbNewLine & "==================" & vbNewLine
                            strError.Trim()
                            strError &= SalesOrder_No & vbNewLine & " สินค้า :" & strSku_Id_Alert & " ไม่พอเบิก " & vbNewLine
                            strError &= "==================" & vbNewLine & GetMessage_Data("400014")
                            W_MSG_Information(strError)
                            'If DocSalesOrder = "" Then
                            '    DocSalesOrder = SalesOrder_No
                            'Else
                            '    If Order <> SalesOrder_No Then
                            '        Order = SalesOrder_No
                            '        DocSalesOrder = DocSalesOrder + " , " + SalesOrder_No
                            '    End If
                            'End If

                            'Next For
                        Else
                            SO_Type = "Z"

                            If Not bool_chkSelect Then
                                W_MSG_Information_ByIndex(400060)
                                Exit Sub
                            End If
                            'ตรวจสอบเส้นทาง
                            Dim xsql As String = ""
                            xsql &= "chkSelect=1"
                            xsql &= " AND (ISNULL(Route_Index,'') IN ('0010000000000',''))"
                            xsql &= " AND (ISNULL(SubRoute_Index,'') IN ('0010000000000',''))"
                            xsql &= " AND (ISNULL(DistributionCenter_Index,'') IN ('0010000000000',''))"
                            Dim drArrCheckRoute() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select(xsql)
                            If drArrCheckRoute.Length > 0 Then
                                W_MSG_Information("กรุณาตรวจสอบ เส้นทางหลัก เส้นทางย่อยและศูนย์กระจาย")
                                Exit Sub
                            End If

                            If W_MSG_Confirm("คุณต้องการยินยันใบสั่งขาย " & SalesOrder_No & " นี้ใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
                                Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status not in (1) and SalesOrder_Index = '" & SalesOrder_Index & "'")
                                'If drArr.Length > 0 Then
                                '    Dim strError As String = GetMessage_Data("400013") & vbNewLine & "==================" & vbNewLine
                                '    For Each drso As DataRow In drArr
                                '        strError &= drso("SalesOrder_No").ToString & vbNewLine
                                '    Next
                                '    strError &= "==================" & vbNewLine & GetMessage_Data("400014")
                                '    W_MSG_Information(strError)
                                '    Exit Sub
                                'End If

                                drArr = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status in (1) and SalesOrder_Index = '" & SalesOrder_Index & "'")
                                If drArr.Length > 0 Then
                                    Dim objSOTransaction As New cls_KSL
                                    For Each drso As DataRow In drArr

                                        If (New clsSO().canConfirmSO(drso("SalesOrder_Index").ToString())) Then
                                            'If Document_Group_Name = "SALE" Then
                                            'Dim dtStock As New DataTable
                                            'dtStock = objSOTransaction.getToltalSum(drso("SalesOrder_Index").ToString(), drso("Customer_index").ToString(), drso("DistributionCenter_Index").ToString())
                                            'If dtStock.Rows.Count > 0 Then
                                            '    Dim frm As New frmSO_CheckStock
                                            '    frm.DataGridView1.AutoGenerateColumns = False
                                            '    frm.DataGridView1.DataSource = dtStock
                                            '    frm.ShowDialog()
                                            'End If
                                            'Dim objSOTransaction As New cls_KSL
                                            Dim dtStock As New DataTable
                                            dtStock = objSOTransaction.getCheckStock_Sales(drso("SalesOrder_Index").ToString, drso("Customer_index").ToString, drso("DistributionCenter_Index").ToString)
                                            If dtStock.Rows.Count > 0 Then
                                                objSOTransaction.ResetColor_Sales(dtStock, drso("SalesOrder_Index").ToString())
                                                'Dim frm As New frmSO_CheckStock
                                                'frm.SalesOrder_No = drso("SalesOrder_No").ToString
                                                'frm.DataGridView1.AutoGenerateColumns = False
                                                'frm.DataGridView1.DataSource = dtStock
                                                'frm.ShowDialog()
                                            End If
                                            'End If
                                            objSOTransaction.Confirm_SO(drso("SalesOrder_Index").ToString, drso("SalesOrder_No").ToString)
                                        End If
                                    Next
                                    W_MSG_Information_ByIndex(300036)
                                    objSOTransaction = Nothing
                                End If
                            Else
                                W_MSG_Information_ByIndex(300032)
                            End If
                        End If

                    End If

                Catch xx As Exception
                    W_MSG_Error(xx.Message.ToString)
                    Exit Sub
                End Try

            Next

            'If SO_Type = "X" Then
            '    W_MSG_Information("เอกสาร " & DocSalesOrder & " ไม่พบข้อมูล ...")
            'End If


            Me.getSOViewSearch()
        Catch ex As Exception
            'objSOTransaction = Nothing
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    ''' <summary>
    ''' Button to Cancel SO (ยกเลิก)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        ' ====== TODO: HARDCODE-MSG 
        If grdSOView.Rows.Count <= 0 Then Exit Sub
        Dim objSOTransaction As New SOTransaction(SOTransaction.enuOperation_Type.DELETE)
        objSOTransaction.newSaleOrder_Index = Me.grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value

        Dim oSo As New tb_SalesOrder
        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If
            Dim SalseUnit As String = ""
            If W_MSG_Confirm(GetMessage_Data("100039") & Me.grdSOView.CurrentRow.Cells("Col_SalesOrder_No").Value & GetMessage_Data("100040")) = Windows.Forms.DialogResult.Yes Then

                oSo.SalesOrder_Index = Me.grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value.ToString
                oSo.SalesOrder_No = Me.grdSOView.CurrentRow.Cells("Col_SalesOrder_No").Value.ToString
                SalseUnit = IIf(String.IsNullOrEmpty(Me.grdSOView.CurrentRow.Cells("Col_SalesUnit").Value.ToString), "", Me.grdSOView.CurrentRow.Cells("Col_SalesUnit").Value.ToString)
                If Not objSOTransaction.CheckCancelSO(oSo.SalesOrder_Index) Then
                    W_MSG_Information(String.Format("เอกสาร {0} ไม่สามารถทำรายการได้เนื่องจากมีการสร้างเอกสารเบิกแล้ว !!", oSo.SalesOrder_No))
                    Exit Sub
                End If

                If objSOTransaction.Cancel_SO(oSo) = True Then
                    W_MSG_Information_ByIndex("300034")

                    getSOViewSearch()
                Else
                    W_MSG_Information_ByIndex("300035")
                    'objSOTransaction = Nothing
                    '  Exit Sub
                End If
                objSOTransaction = Nothing
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
            objSOTransaction = Nothing
        End Try

    End Sub

    ''' <summary>
    ''' Button to refresh search criteria.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getSOViewSearch(True)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' Button to close this form.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    ''' <summary>
    ''' Button to print document
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print.Click

        Try
            If Me.grdSOView.RowCount = 0 Then Exit Sub
            Dim Report_Name As String = Me.cboPrint.SelectedValue.ToString
            Dim Customer_Index As String = grdSOView.CurrentRow.Cells("Col_Customer_Index").Value.ToString

            If Report_Name = "RPTSALEORDER_REQ" Then
                CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()
                Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status not in (2,6)")
                If drArr.Length > 0 Then
                    'Me.Chang_RGB()
                    W_MSG_Information("กรุณาเลือกรายการ รอเบิกหรือค้างจ่ายเท่านั้น")
                    Exit Sub
                End If
                drArr = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status in (2,6)")

                Dim ArrSalesOrder_Index As New List(Of String)
                Dim ArrSalesOrder_No As New List(Of String)
                Dim xDistributionCenter As String = drArr(0)("DistributionCenter").ToString
                For Each drso As DataRow In drArr
                    If xDistributionCenter <> drso("DistributionCenter").ToString Then
                        'Me.Chang_RGB()
                        W_MSG_Information("กรุณาเลือกศูนย์กระจายเดียวกัน")
                        Exit Sub
                    End If
                    ArrSalesOrder_Index.Add(drso("SalesOrder_Index").ToString)
                    ArrSalesOrder_No.Add(drso("SalesOrder_No").ToString)
                Next
                'Report สินค้าไม่พอ

                Dim strSales As String = ""
                Dim iRow As Integer = 0
                For Each drSales As String In ArrSalesOrder_Index
                    strSales &= "'" & drSales & "'"
                    iRow += 1
                    If iRow <> ArrSalesOrder_Index.Count Then
                        strSales &= ","
                    End If
                Next

                Dim strSales_No As String = ""
                iRow = 0
                For Each drSales As String In ArrSalesOrder_No
                    strSales_No &= "" & drSales & ""
                    iRow += 1
                    If iRow <> ArrSalesOrder_No.Count Then
                        strSales_No &= ", "
                    End If
                Next
                Dim oSalesReq As New clsSO
                Dim dsSalesReq As New DataSet
                Dim strWhere As String = ""

                strWhere &= " and SO.SalesOrder_Index IN (" & strSales & ")"

                'Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
                'strWhere &= oUser.GetUserByCustomer()
                'strWhere = strWhere.Replace("Customer_Index", "SO.Customer_Index")

                strWhere &= New clsUserByDC().GetDistributionCenterByUser()
                strWhere = strWhere.Replace("DistributionCenter_Index", "SO.DistributionCenter_Index")

                strWhere &= " AND SOI.RGB_Check = 2"
                'strWhere &= " AND DT.Document_Group_Name = '" & Me.Document_Group_Name & "'"
                dsSalesReq = oSalesReq.getReportSalsOrder_Req(strWhere, strSales)


                If dsSalesReq.Tables(0).Rows.Count = 0 Then
                    W_MSG_Information("ไม่พบรายการสินค้าไม่พอ")
                    Exit Sub
                End If
                Dim oCrystal As New rptSaleOrder_Req
                Dim frmrpt As New frmReportViewerMain
                frmrpt.Report_Name = "RPTSALEORDER_REQ"
                dsSalesReq.DataSetName = "dsSaleOrder_Req"
                dsSalesReq.Tables(0).TableName = "SaleOrder_Req"
                oCrystal.SetDataSource(dsSalesReq)
                oCrystal.SetParameterValue("User", W_Module.WV_UserName)
                oCrystal.SetParameterValue("SalesOrder_No", strSales_No)
                frmrpt.CrystalReportViewer1.ReportSource = oCrystal
                frmrpt.ShowDialog()
                'Me.Chang_RGB()
            Else
                Dim oReport As New WMS_STD_OUTB_Report.Loading_Report(Report_Name, "And SalesOrder_Index ='" & grdSOView.CurrentRow.Cells("Col_SalesOrder_Index").Value & "'")
                Dim frm As New WMS_STD_OUTB_Report.frmReportViewerMain
                frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
                frm.ShowDialog()
                'Me.Chang_RGB()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            Me.Chang_RGB()
        End Try


    End Sub

    ''' <summary>
    ''' This button creates Picking List from SO
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnPicking_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPicking.Click
        Try
            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If grdSOView.Rows.Count = 0 Then Exit Sub
            Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status not in (2,6)")
            For Each drso As DataRow In drArr
                W_MSG_Information(GetMessage_Data("400030") & drso("SalesOrder_No") & GetMessage_Data("400059"))
                Exit Sub
            Next

            drArr = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status in (2,6)")

            Dim ArrSalesOrder_Index As New List(Of String)
            For Each drso As DataRow In drArr
                ArrSalesOrder_Index.Add(drso("SalesOrder_Index").ToString)
            Next


            If ArrSalesOrder_Index.Count > 0 Then
                '============================================================================================
                Dim objDB As New config_CustomSetting
                If objDB.getConfig_Key_USE("USE_SO_STOCK_WARNING_RPT") = True Then
                    'Report สินค้าไม่พอ
                    Dim strSales As String = ""
                    Dim iRow As Integer = 0
                    For Each drSales As String In ArrSalesOrder_Index
                        strSales &= "''" & drSales & "''"
                        iRow += 1
                        If iRow <> ArrSalesOrder_Index.Count Then
                            strSales &= ","
                        End If
                    Next
                    Dim oSalesReq As New WMS_STD_OUTB_SO.ml_SalesOrder
                    Dim dsSalesReq As New DataSet
                    dsSalesReq = oSalesReq.getReportSalsOrder_Req(" and SO.SalesOrder_Index IN (" & strSales & ")")
                    If dsSalesReq.Tables(0).Rows.Count > 0 Then
                        If W_MSG_Confirm("มีรายการที่สินค้าไม่พอที่คลัง คุณต้องการเบิกสินค้าหรือไม่") = Windows.Forms.DialogResult.No Then Exit Sub
                        Dim oCrystal As New WMS_STD_OUTB_SO.rptSaleOrder_Req
                        Dim frmrpt As New WMS_STD_OUTB_SO.frmReportViewerMain_SO
                        frmrpt.Report_Name = "RPTSALEORDER_REQ"
                        dsSalesReq.DataSetName = "dsSaleOrder_Req"
                        dsSalesReq.Tables(0).TableName = "SaleOrder_Req"
                        oCrystal.SetDataSource(dsSalesReq)
                        oCrystal.SetParameterValue("User", W_Module.WV_UserName)
                        frmrpt.CrystalReportViewer1.ReportSource = oCrystal
                        frmrpt.ShowDialog()
                        If W_MSG_Confirm("คุณต้องการทำรายการเบิกสินค้าต่อ ใช่หรือไม่") = Windows.Forms.DialogResult.No Then Exit Sub
                    End If
                End If
                objDB = Nothing

                Dim objSalesOrder As New tb_SalesOrder
                Dim CheckArrSalesOrder_Index() As String = New String(ArrSalesOrder_Index.Count - 1) {}
                ArrSalesOrder_Index.CopyTo(CheckArrSalesOrder_Index)

                For Each StrItem As String In CheckArrSalesOrder_Index
                    If Not objSalesOrder.IsCreateWithdraw_Set(StrItem) Then
                        ArrSalesOrder_Index.Remove(StrItem)
                    End If
                Next

                If ArrSalesOrder_Index.Count <= 0 Then Exit Sub

                '=================================================================================
                'TODO: 'รอฟอร์ม
                Dim frm As New frmWithdrawAsset_V4(frmWithdrawAsset_V4.enuOperation_Type.ADDNEW)
                frm.ArrSalesOrder_Index = ArrSalesOrder_Index
                frm.ShowDialog()
                Me.getSOViewSearch()
            Else
                W_MSG_Information_ByIndex("400060")
            End If


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

#End Region

#Region " DATAGRID EVENTS "

    ''' <summary>
    ''' This sub manages button controls when we change row selection in datagrid.
    '''   Dong_kk Edit Add CellClick เพราะ ถ้ามีบันทัดเดียวไม่ทำงาน
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOView_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSOView.SelectionChanged
        setSelecttionChange()
    End Sub
    Private Sub grdSOView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOView.CellClick
        setSelecttionChange()
    End Sub
    Sub setSelecttionChange()
        If grdSOView.CurrentRow Is Nothing Then Exit Sub
        If grdSOView.CurrentRow.Selected Then


            Select Case grdSOView.CurrentRow.Cells("Col_Status").Value

                Case "-1"
                    ' ====== Case of "CANCELLED" Document (ยกเลิก)
                    Me.btnCancel.Enabled = False
                    Me.btnEdit.Enabled = True
                    Me.btnConfirm.Enabled = False
                    Me.btnPicking.Enabled = False

                Case "1"

                    ' ====== Case of "TO BE CONFIRMED" Document (รอยืนยัน)
                    Me.btnCancel.Enabled = True
                    Me.btnEdit.Enabled = True
                    Me.btnConfirm.Enabled = True
                    Me.btnPicking.Enabled = False
                Case "2", "6"
                    ' ====== Case of "WAITING TO WITHDRAW" Document (รอเบิก / ค้างจ่าย)
                    Me.btnCancel.Enabled = True
                    Me.btnEdit.Enabled = True
                    Me.btnConfirm.Enabled = False
                    Me.btnPicking.Enabled = True
                Case "3"
                    ' ====== Case of "WAITING TO DELIVER" Document (รอส่ง)
                    Me.btnCancel.Enabled = True
                    Me.btnEdit.Enabled = True
                    Me.btnConfirm.Enabled = False
                    Me.btnPicking.Enabled = False
                Case "4", "5"
                    ' ====== Case of "IN-TRANSAIT / COMPLETED" Document (กำลังจัดส่ง / เสร็จสิ้น)
                    Me.btnCancel.Enabled = False
                    Me.btnEdit.Enabled = True
                    Me.btnConfirm.Enabled = False
                    Me.btnPicking.Enabled = False
                Case Else
                    Me.btnCancel.Enabled = False
                    Me.btnEdit.Enabled = False
                    Me.btnConfirm.Enabled = False
                    Me.btnPicking.Enabled = False
            End Select

        End If
    End Sub

    ''' <summary>
    ''' Double click to Edit SO.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSOView.CellDoubleClick
        Try
            btnEdit_Click(sender, e)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' We just refresh number of rows when a row is removed.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOView_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdSOView.RowsRemoved
        SetnumRows()
    End Sub

    ''' <summary>
    ''' We just refresh number of rows when a row is added.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub grdSOView_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdSOView.RowsAdded
        SetnumRows()
    End Sub

    'Private Sub cboDocumentStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentStatus.SelectedIndexChanged
    '    Try
    '        getSOViewSearch()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try

    'End Sub

#End Region

#Region " GENERIC FUNCTIONS AND SUBS "

    ''' <summary>
    ''' Count display rows and show result.
    ''' </summary>
    ''' <remarks></remarks>
    Sub SetnumRows()
        ' ====== TODO: HARDCODE-MSG
        Dim numRows As Integer = 0

        numRows = grdSOView.Rows.Count
        If numRows > 0 Then
            'lblCountRows.Text = GetMessage_Data("400028") & numRows & GetMessage_Data("400029") & numRows & GetMessage_Data("400030")
            lblCountRows.Text = GetMessage_Data("400028") & CInt(Me.txtRowCount.Text) & GetMessage_Data("400029") & numRows & GetMessage_Data("400030")
        Else
            lblCountRows.Text = GetMessage_Data("400031")
        End If
    End Sub

    ''' <summary>
    ''' This is the main function to show all data in data grid view.
    ''' SQL statement is builded in this sub.
    ''' </summary>
    ''' <remarks>
    ''' Updated by: Todd 7 Jan 2010
    ''' 
    ''' ====== TODO: REMOVE SQL from presentation layer.
    ''' </remarks>
    Private Sub getSOViewSearch(Optional ByVal Refresh As Boolean = False)
        Dim objtb_SalesOrder As New tb_SalesOrder
        Dim objDT As DataTable = New DataTable
        'Dim objDTtb_SalesOrder As DataTable = New DataTable
        Dim strWhere As String = ""

        Try


            If Me.chkPending.Checked Then
                'งานค้าง
                strWhere &= " AND (Status not in (-1,3,5)) "
            Else

            End If

            If (Me.rdbSO_Date.Checked) Then
                strWhere &= String.Format(" and (cast(SalesOrder_Date as date)>='{0}' and cast(SalesOrder_Date as date)<='{1}') ", Me.dtpBeginDate.Value.ToString("yyyy-MM-dd"), Me.dtpEndDate.Value.ToString("yyyy-MM-dd"))
            End If
            'If Me.rdbSO_Date.Checked = True Then
            '    ' ------ Filter by Date Range
            '    Dim StartDate As String = Format(dtpBeginDate.Value, "MM/dd/yyyy").ToString() + " 00:00:00"
            '    Dim EndtDate As String = Format(dtpEndDate.Value.AddDays(1), "MM/dd/yyyy").ToString() + " 00:00:00"
            '    'strWhere &= " AND (SalesOrder_Date between '" & StartDate & "' AND  '" & EndtDate & "'"
            '    strWhere = " AND ((SalesOrder_Date >= '" & StartDate & "') AND (SalesOrder_Date < '" & EndtDate & "'))"
            'End If

            Select Case Me.cbDeleveryDay.SelectedValue
                Case "-11", Nothing
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (DayIndex like '%" & Me.cbDeleveryDay.SelectedValue & "%')"
            End Select

            If Me.rdbSO_No.Checked = True Then
                strWhere &= " AND (SalesOrder_No like '%" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%' or Interface_No like '%" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%') "
            End If

            If Me.rdbTM_No.Checked = True Then
                strWhere &= " AND (TransportManifest_No like '" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "%') "
            End If

            If (Me.rdbExpected_Delivery_Date.Checked) Then
                strWhere &= String.Format(" and (cast(Expected_Delivery_Date as date)>='{0}' and cast(Expected_Delivery_Date as date)<='{1}') ", Me.dtpBeginDate.Value.ToString("yyyy-MM-dd"), Me.dtpEndDate.Value.ToString("yyyy-MM-dd"))
            End If

            If Me.rdbCustomer.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere &= " AND (Customer_Name Like '" & Me.txtKeySearch.Text & "%') "
                End If
            End If

            If Me.rdbConsignee.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere &= " AND (Company_Name Like '" & Me.txtKeySearch.Text & "%') "
                End If
            End If

            If Me.rdbCustomerShippingLocation.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere &= " AND (Shipping_Location_Name Like '" & Me.txtKeySearch.Text & "%') "
                End If
            End If

            If Me.rdbCustomerShippingLocationID.Checked = True Then
                If txtKeySearch.Text <> "" Then
                    strWhere &= " AND (Customer_Shipping_Location_ID Like '" & Me.txtKeySearch.Text & "%') "
                End If
            End If


            Select Case Me.cboDocumentStatus2.SelectedValue
                Case "-11", Nothing
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (Status_Manifest IN (" & Me.cboDocumentStatus2.SelectedValue & ")) "
            End Select
            Select Case Me.cbSOType.SelectedValue
                Case "-11", Nothing
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (documentType_index='" & Me.cbSOType.SelectedValue & "') "
            End Select
            Select Case Me.cboProductType.SelectedValue
                Case "-11", Nothing
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (ProductType_Index='" & Me.cboProductType.SelectedValue & "') "
            End Select






            Select Case Me.cboDocumentStatus.SelectedValue
                Case 0
                Case Else
                    strWhere &= " AND (Status=" & Me.cboDocumentStatus.SelectedValue & ") "
            End Select

            strWhere &= " AND (Process_Id=10) "

            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()

            'ADD BY Pong 22/02/2017
            ' strWhere &= New clsUserByDC().GetDistributionCenterByUser()


            'objtb_SalesOrder.getSOViewSearch(strWhere)
            'objDT = objtb_SalesOrder.DataTable

            'KSL
            'strWhere &= " AND DocumentType_Index in (SELECT  DocumentType_Index FROM ms_DocumentType WHERE Document_Group_Name = '" & Me.Document_Group_Name & "')"

            'If Me.chkSKU.Checked = True Then
            'If Me.txtSKU_ID.Text.Trim <> "" Then
            'strWhere &= " AND SalesOrder_Index in (SELECT  SSI.SalesOrder_Index FROM tb_SalesOrderItem SSI inner join ms_SKU UI ON SSI.Sku_Index = UI.Sku_Index WHERE UI.Sku_Id = '" & Me.txtSKU_ID.Text & "')"
            'End If
            'End If



            Select Case Me.cboDistributionCenter.SelectedValue
                Case "0010000000000", Nothing
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (distributionCenter_index ='" & Me.cboDistributionCenter.SelectedValue & "') "
            End Select



            If rdbSellArea.Checked = True Then
                strWhere &= " AND (SalesAreaCode Like '" & Me.txtKeySearch.Text & "%') "
            End If

            If rdbSellerId.Checked = True Then
                strWhere &= " AND (SaleCode Like '" & Me.txtKeySearch.Text & "%') "
            End If

            If rdbPO_NO.Checked = True Then
                strWhere &= " AND (Interface_No Like '" & Me.txtKeySearch.Text & "%') "
            End If

            If rdbHubId.Checked = True Then
                strWhere &= " AND (Customer_Shipping_Id Like '" & Me.txtKeySearch.Text & "%') "
            End If


            If rdbAdd_Date.Checked = True Then
                strWhere &= String.Format(" and (cast(Add_Date as date)>='{0}' and cast(Add_Date as date)<='{1}') ", Me.dtpBeginDate.Value.ToString("yyyy-MM-dd"), Me.dtpEndDate.Value.ToString("yyyy-MM-dd"))

            End If


            If Me.chkRGB.Checked = True Then
                Select Case Me.cboRGB.SelectedIndex
                    Case 0
                        strWhere &= "  AND (RGB_Check = 2)"
                    Case 1
                        strWhere &= "  AND (RGB_Check = 1)"
                    Case Else
                        strWhere &= "  AND (RGB_Check not in (1,2))"
                End Select

            End If
            If Me.cboRegion.SelectedValue <> -99 Then
                strWhere &= "  AND (TransportRegion_Index =  '" & Me.cboRegion.SelectedValue & "')"
            End If

            Dim SoTypeWhere As String = ""
            'If cbkX.Checked Then
            'SoTypeWhere &= "'X',"
            'End If

            'If cbkY.Checked Then
            'SoTypeWhere &= "'Y',"

            'End If

            'If cbkZ.Checked Then
            'SoTypeWhere &= "'Z',"
            'End If

            ''  If cbkX.Checked Or cbkY.Checked Or cbkZ.Checked Then
            '' SoTypeWhere = SoTypeWhere.Substring(0, SoTypeWhere.Length() - 1)
            '' strWhere &= "  AND SO_Type in (" & SoTypeWhere & ")"
            '' End If


            If cboPriority.SelectedValue <> "-99" Then
                strWhere &= "  AND Urgent_Id = '" & cboPriority.SelectedValue & "'"
            End If


            'ADD BY Pong 28/04/2015
            Me.cboRowPerPage.SelectedIndex = IIf(Me.cboRowPerPage.SelectedIndex > 0, Me.cboRowPerPage.SelectedIndex, 0)
            If rdTop.Checked Then
                Me.txtTop.Text = IIf(IsNumeric(Me.txtTop.Text), Me.txtTop.Text, 1)
                Me.txtTop.Text = IIf(CInt(Me.txtTop.Text) > 0, CInt(Me.txtTop.Text), 1)
                objtb_SalesOrder.getSOViewSearch(strWhere, 1, CInt(Me.txtTop.Text))
            ElseIf rdRowPage.Checked Then
                Me.txtPageIndex.Text = IIf(IsNumeric(Me.txtPageIndex.Text), Me.txtPageIndex.Text, 1)
                Me.txtPageIndex.Text = IIf(CInt(Me.txtPageIndex.Text) > 0, CInt(Me.txtPageIndex.Text), 1)
                objtb_SalesOrder.getSOViewSearch(strWhere, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)) - CInt(Me.cboRowPerPage.Text) + 1, (CInt(Me.txtPageIndex.Text) * CInt(Me.cboRowPerPage.Text)))
            Else
                objtb_SalesOrder.getSOViewSearch(strWhere, -1, 0)
            End If

            Calculate_Paging()

            objDT = objtb_SalesOrder.GetDataTable
            objDT.AcceptChanges()
            _iNewRow = objDT.Rows.Count
            If Refresh = True Then
                objDT = Me.GetRowSelected(objDT) 'Function Select ค้าง
            End If
            objDT.AcceptChanges()
            Me.grdSOView.DataSource = objDT
            Me.grdSOView.Update()
            Me.Chang_RGB()


            If objDT.Rows.Count > 0 Then
                Me.txtRowCount.Text = objDT.Rows(0)("ROW_COUNT").ToString
            Else
                Me.txtRowCount.Text = 0
            End If

            'With objDT.Columns
            '    If Not .Contains("chkSelect") Then
            '        .Add("chkSelect", GetType(Boolean))
            '    End If
            '    If Not .Contains("DayDesc") Then
            '        .Add("DayDesc")
            '    End If
            'End With


            'Dim strDay() As String
            'Dim DayIndex As String = ""
            'Dim dayResult As String = ""
            'For i As Integer = 0 To objDT.Rows.Count - 1
            '    With objDT.Rows(i)
            '        If .Item("DayIndex").ToString <> "" Then
            '            DayIndex = .Item("DayIndex").ToString()
            '            strDay = DayIndex.Split(",")
            '            If strDay.Length > 0 Then
            '                dayResult = ""
            '                For Each str As String In strDay
            '                    dayResult &= CheckStrDay(str) & ","
            '                Next
            '            End If
            '            .Item("DayDesc") = dayResult.Substring(0, dayResult.Length - 1)
            '        End If
            '    End With
            'Next
            '     Dim drArrChkSelect() As DataRow
            'Dim drArrChkSelect() As DataRow = Nothing

            'If Me.grdSOView.Rows.Count > 0 Then
            '    drArrChkSelect = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1")
            'End If

            'objDT.AcceptChanges()

            'Me.grdSOView.DataSource = objDT

            ''Me.setChkSelectCurrent(drArrChkSelect)

            'Me.grdSOView.Update()
            'Me.Chang_RGB()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objtb_SalesOrder = Nothing
        End Try
    End Sub

    Private _CountRow As Integer = 0
    Private _iNewRow As Integer = 0

    Private Function GetRowSelected(ByVal podtData As DataTable) As DataTable
        'Keep Selected Product
        Try
            _iNewRow = podtData.Rows.Count

            Dim odtTemp As New DataTable

            '  If Me.grdItem_ToLoad.DataSource IsNot Nothing Then

            Dim odrTemNoSelected() As DataRow    '--- Array Datarow
            Dim odrDuplicate() As DataRow       '--- Array Datarow

            If Me.grdSOView.DataSource IsNot Nothing Then
                CType(grdSOView.DataSource, DataTable).AcceptChanges()
                odtTemp = grdSOView.DataSource
                odrTemNoSelected = odtTemp.Select("chkSelect=0")
                'Step 1: ลบตัวที่ไม่เลือก
                For Each drNoSelect As DataRow In odrTemNoSelected
                    odtTemp.Rows.Remove(drNoSelect)
                Next
                'Step 1: ตัวที่เลือก
                odrTemNoSelected = odtTemp.Select("chkSelect=1")
                For Each odrSelected As DataRow In odrTemNoSelected
                    odrDuplicate = podtData.Select("SalesOrder_Index = '" & odrSelected("SalesOrder_Index").ToString & "'")
                    If odrDuplicate.Length > 0 Then
                        'ลบตัวเก่าที่ซ้ำจากดาต้ากริด คีย์ใหม่
                        podtData.Rows.Remove(odrDuplicate(0))
                        _CountRow += 1
                        odrSelected("count") = _CountRow
                    End If
                Next

                'Step 2: ตัวที่เหลือจากการลบเป็นตัวใหม่ ให้แสดงบนสุด
                For Each odrSelected As DataRow In podtData.Rows
                    _CountRow += 1
                    odrSelected("count") = _CountRow
                Next

            End If
            'Step 3: รวมกันตัวใหม่  กับ ตัวที่เดิมที่ลบตัวที่ซ้ำกับตัวใหม่และตัวเดิมที่ไม่ได้เลือก 
            odtTemp.Merge(podtData)

            Return odtTemp
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Sub setChkSelectCurrent(Optional ByVal drArrChkSelect As DataRow() = Nothing)
        Try

            If Not IsNothing(drArrChkSelect) Then
                For Each dtrow As DataRow In drArrChkSelect
                    For i As Integer = 0 To Me.grdSOView.Rows.Count - 1
                        If dtrow.Item("SalesOrder_index") = Me.grdSOView.Rows(i).Cells("Col_SalesOrder_Index").Value Then
                            Me.grdSOView.Rows(i).Cells("chkSelect").Value = True
                        End If
                    Next
                Next
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Sub Chang_RGB()
        Dim objDT As New DataTable
        objDT = CType(grdSOView.DataSource, DataTable)
        For i As Integer = 0 To Me.grdSOView.Rows.Count - 1 'objDT.Rows.Count - 1
            With Me.grdSOView
                '.Rows(i).Cells("Col_SalesOrder_Date").Value = CDate(objDT.Rows(i).Item("SalesOrder_Date"))
                '.Rows(i).Cells("Col_SalesOrder_Date").Value = Format(CDate(objDT.Rows(i).Item("SalesOrder_Date")), "dd/MM/yyyy").ToString
                Dim intStatus As Integer = .Rows(i).Cells("Col_Status").Value 'objDT.Rows(i).Item("Status")
                Dim RGB As Integer = .Rows(i).Cells("Col_RGB").Value 'IIf(objDT.Rows(i).Item("RGB_Check").ToString = Nothing, 0, objDT.Rows(i).Item("RGB_Check"))
                'Select Case RGB
                '    Case 1 'พอเบิก
                '        .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.LightGreen
                '    Case 2 'ไม่พอเบิก
                '        .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.Red
                '    Case Else
                'End Select
                Select Case intStatus
                    Case -1 'ยกเลิก
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.Gray
                        .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.Gray
                    Case 2 'รอเบิก
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightYellow
                    Case 3 'รอส่ง
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightGreen
                    Case 5 'เสร็จสิ้น
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightGreen
                    Case 6 'ค้างจ่าย
                        .Rows(i).Cells("Col_Status_Desc").Style.BackColor = Drawing.Color.LightYellow
                    Case Else
                End Select

                If objDT.Rows(i).Item("Confirm_By").ToString.Trim.Length > 0 Then
                    .Rows(i).Cells("col_Confirm_By").Style.BackColor = Drawing.Color.LightGreen
                    .Rows(i).Cells("col_Confirm_Date").Style.BackColor = Drawing.Color.LightGreen
                End If
            End With
        Next
        Me.grdSOView.ClearSelection()
    End Sub
    Private Function CheckStrDay(ByVal strDay As String) As String
        Try
            Dim obj As New ms_Customer_Shipping_Location_Day
            Return obj.getDayDesc(" And Day_index='" & strDay & "'")
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If grdSOView.RowCount > 0 Then
                For iRow As Integer = 0 To grdSOView.RowCount - 1
                    grdSOView.Rows(iRow).Cells("chkSelect").Value = Me.chkSelectAll.Checked
                Next
            End If
        Catch ex As Exception

        End Try
    End Sub
#End Region

#Region " UNUSED & BACKUP FUNCTIONS(TEMPORARY) "
    'Sub getSOView(Optional ByVal pstrStatus As String = "")
    '    Try

    '        Dim objtb_SalesOrder As New tb_SalesOrder
    '        Try
    '            objtb_SalesOrder.getSOMain("" & pstrStatus)
    '            grdSOView.DataSource = objtb_SalesOrder.DataTable

    '            Me.grdSOView.Refresh()
    '        Catch ex As Exception
    '            Throw ex
    '        Finally
    '            objtb_SalesOrder = Nothing
    '        End Try
    '    Catch ex As Exception

    '    Finally
    '    End Try
    'End Sub

    'Sub config_txtField(ByVal dtLanguage As DataTable)
    '    Dim strtxt_Control As String
    '    Dim strControl_name As String
    '    Dim i As Integer = 0

    '    For i = 0 To dtLanguage.Rows.Count - 1

    '        strControl_name = dtLanguage.Rows(i).Item("Name").ToString()
    '        strtxt_Control = dtLanguage.Rows(i).Item("txt").ToString()

    '        Me.Controls(strControl_name).Text = strtxt_Control
    '    Next
    'End Sub
#End Region



    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            grdSOView.DataSource = Nothing
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Dong_kk Update For Sharp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub txtKeySearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) 'Handles txtKeySearch.KeyDown
        Try
            If Not (e.KeyCode = Keys.Enter) Then Exit Sub

            Dim objtb_SalesOrder As New tb_SalesOrder
            Dim objDT As DataTable = New DataTable
            Dim strWhere As String = ""
            If Me.rdbSO_No.Checked = True Then
                strWhere = " AND (SalesOrder_No ='" & Me.txtKeySearch.Text.Replace("'", "''").Trim & "') "
            Else
                Exit Sub
            End If
            Select Case Me.cboDocumentStatus.SelectedValue
                Case 0
                    strWhere &= " "
                Case Else
                    strWhere &= " AND (Status=" & Me.cboDocumentStatus.SelectedValue & ") "
            End Select


            objtb_SalesOrder.getSOViewSearch(strWhere)
            objDT = objtb_SalesOrder.DataTable
            Dim dtDataSouce As New DataTable
            dtDataSouce = CType(Me.grdSOView.DataSource, DataTable)
            If objDT.Rows.Count = 0 Then Exit Sub

            If dtDataSouce IsNot Nothing Then
                Dim drSoArr() As DataRow = dtDataSouce.Select("SalesOrder_Index ='" & objDT.Rows(0).Item("SalesOrder_Index").ToString & "'")
                If drSoArr.Length > 0 Then Exit Sub
            End If

            dtDataSouce.Merge(objDT)
            dtDataSouce.AcceptChanges()
            Me.grdSOView.DataSource = dtDataSouce
            Me.grdSOView.Update()

            For i As Integer = 0 To grdSOView.Rows.Count - 1
                With Me.grdSOView
                    .Rows(i).Cells("Col_SalesOrder_Date").Value = Format(CDate(.Rows(i).Cells("col_SalesOrder_Date").Value), "dd/MM/yyyy").ToString
                    Dim intStatus As Integer = .Rows(i).Cells("Col_Status").Value
                    Select Case intStatus
                        Case -1 'ยกเลิก
                            .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.Pink
                        Case 2 'รอเบิก
                            .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.LightYellow
                        Case 3 'รอส่ง
                            .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.LightGreen
                        Case 5 'เสร็จสิ้น
                            .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.LightGreen
                        Case 6 'ค้างจ่าย
                            .Rows(i).Cells("Col_SalesOrder_No").Style.BackColor = Drawing.Color.LightYellow
                        Case Else
                    End Select
                End With
            Next

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtKeySearch_KeyDown_1(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.getSOViewSearch()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub frmSOView_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigSalesOrder
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 10)
                    oFunction.SW_Language_Column(Me, Me.grdSOView, 10)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Top Control ADD BY Pong 28/04/2015
    Private Sub btnPageFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageFirst.Click
        Try
            txtPageIndex.Text = 1
            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            txtPageIndex.Text -= 1
            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            txtPageIndex.Text += 1
            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            txtPageIndex.Text = txtTotalPage.Text
            Me.getSOViewSearch()
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

            'Enable Button
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

    Private Sub txtNubmeric_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTop.KeyPress, txtPageIndex.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(sender, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    'end Top Control

    Private Sub rdbTM_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTM_No.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub


    Private Sub rdbSellerId_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSellerId.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub


    Private Sub rdbSellArea_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSellArea.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub rdbPO_NO_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbPO_NO.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub btnImportSO_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportSO.Click
        Try
            Dim frm As New frmImportsTextSO
            frm.ShowDialog()
            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub



    Private Sub btnWithdrawNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnWithdrawNew.Click
        Try
            CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()


            Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1")

            'If Document_Group_Name = "SALE" Then
            '    'ใบสั่งขายต้องมีใบคุมรถเท่านั้น
            '    'Dim drArr2() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and ISNULL(TransportManifest_No,'') = ''")
            '    'If drArr2.Length > 0 Then
            '    '    W_MSG_Information("ใบสั่งขายไม่มีใบคุมรถ กรุณาสร้างใบคุมรถ")
            '    '    Exit Sub
            '    'End If

            '    'Dim objDB As New DBType_SQLServer
            '    'Dim dtDB As New DataTable
            '    'Dim sqlDB As String = ""
            '    'sqlDB &= " SELECT * FROM tb_SalesOrder WHERE Status not in (-1) "
            '    'sqlDB &= String.Format(" AND TransportManifest_No = '{0}'", drArr(0)("TransportManifest_No").ToString)
            '    'dtDB = objDB.DBExeQuery(sqlDB)
            '    'If dtDB.Rows.Count <> drArr.Length Then
            '    '    W_MSG_Information("กรุณาเลือกใบสั่งขายให้ครบทั้งใบคุมรถ")
            '    '    Exit Sub
            '    'End If

            'End If

            If drArr.Length > 0 Then

                Dim SalesOrder_IndexL As New List(Of String)
                Dim _DocumentType_Index As String = ""
                _DocumentType_Index = drArr(0)("DocumentType_Index")
                Dim ArrSalesOrder_No As String = ""
                For Each dr As DataRow In drArr
                    If _DocumentType_Index <> dr("DocumentType_Index") Then
                        W_MSG_Information("ประเภทเอกสารไม่ตรงกัน กรุณาตรวจสอบ")
                        Exit Sub
                    End If
                    If dr("Invoice_No").ToString <> "" Then
                        W_MSG_Information("มีการออก Invoice แล้วไม่สามารถเบิกสินค้าได้ กรุณาตรวจสอบ")
                        Exit Sub
                    End If
                    '------------------------------------------------
                    'KSL : Add new lert
                    If dr("Document_Group_Name").ToString.Trim.ToUpper = "SALE" Then 'ขายสินค้า
                        If dr("TransportManifest_No").ToString = "" Then
                            ArrSalesOrder_No &= Chr(13) & dr("SalesOrder_No").ToString & Chr(13)
                        End If
                    End If
                    '------------------------------------------------

                    SalesOrder_IndexL.Add(dr("SalesOrder_Index"))
                Next

                'If ArrSalesOrder_No.Trim.Length > 0 Then
                '    If W_MSG_Confirm("ใบสั่งขาย " & ArrSalesOrder_No.ToString & " ยังไม่จัดใบคุมรถ คุณต้องการเบิกสินค้าใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then
                '        Exit Sub
                '    End If
                'End If

                Dim objSO As New clsSO
                objSO.CheckSOGroupProductType(SalesOrder_IndexL.ToArray)

                Dim frm As New frmGroupingConditionPicking
                frm.DocumentType_Index2 = _DocumentType_Index
                frm.arrSalesOrder_Index = SalesOrder_IndexL.ToArray
                'frm.Document_Group_Name = Document_Group_Name
                frm.ShowDialog()
                Me.getSOViewSearch()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPicking_EnabledChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPicking.EnabledChanged
        Try
            Me.btnWithdrawNew.Enabled = btnPicking.Enabled
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnImport_PRQ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport_PRQ.Click
        Try
            'Dim frm As New frmImport_SO_New_V2
            'Dim frm As New WMS_Site_Topcharoen_P2.frmImport_SO_PRQ
            'Dim frm As New WMS_Site_Topcharoen_P2.frmImport_SO_Top
            ''frm.Document_Group_Name = Document_Group_Name
            'frm.ShowDialog()

            'New for RCP
            Dim frm As New frmImportSO_P2
            frm.ShowDialog()

            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnCopy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCopy.Click
        Try

            If grdSOView.Rows.Count > 0 Then
                Dim objDB As New DBType_SQLServer
                If objDB.DBExeQuery(String.Format(" select * from tb_SalesOrder where SalesOrder_Index = '{0}' ", grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value.ToString)).Rows(0).Item("Status") = 1 Then
                    W_MSG_Information("เอกสารรอยืนยัน Copy ไม่ได้")
                    Exit Sub
                End If
                'เอา frmCopy_SO ออก
                'Dim frm1 As New frmCopy_SO
                'frm1.ShowDialog()
                'If frm1.isCancel = True Then
                '    Exit Sub
                'End If

                'Dim frm As New frmSO
                Dim frm As New frmSO
                'If frm1.radCopy_All.Checked Then
                '    frm.Copy_Red = False
                '    frm.Copy_All = True
                'Else
                '    frm.Copy_All = False
                '    frm.Copy_Red = True
                'End If

                'If frm1.radCopy_RedNonStock.Checked Then
                '    frm.Copy_All = True
                '    frm.Copy_Red = True
                'End If

                '== Comment Standard Code 20181026
                ''รายการแดงทั้งหมดยอดที่ไม่พอ ลบรายการเก่าและคำนวณต้นฉบับ
                'If frm1.radCopy_RedNonStock.Checked Then
                '    frm.Copy_Red = True
                '    frm.Copy_All = False
                '    frm.Copy_Red_NotStock = True
                'End If
                ''รายการแดงทั้งหมด ลบรายการเก่าและคำนวณต้นฉบับ
                'If frm1.radCopy_Red.Checked Then
                '    frm.Copy_Red = True
                '    frm.Copy_All = False
                '    frm.Copy_Red_NotStock = False
                'End If
                ''ทุกรายการ ไม่ลบรายการเก่าและคำนวณต้นฉบับ
                'If frm1.radCopy_AllMaster.Checked Then
                '    frm.Copy_Red = True
                '    frm.Copy_All = True
                '    frm.Copy_Red_NotStock = False
                'End If
                ''ทุกรายการ
                'If frm1.radCopy_All.Checked Then
                '    frm.Copy_Red = False
                '    frm.Copy_All = True
                '    frm.Copy_Red_NotStock = False
                'End If

                'frm.Copy_All = False
                'frm.Copy_Red = True
                'frm.Copy_Red_NotStock = True

                frm.objStatus = frmSO.enuOperation_Type.COPY
                frm.SalesOrder_Index = grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value.ToString
                frm.Status = grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_Status").Value.ToString
                'frm.Document_Group_Name = Document_Group_Name
                frm.ShowDialog()
                Me.getSOViewSearch()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub frmSOView_V4_Activated(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Activated
        Try
            Me.getSOViewSearch()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        'Try
        '    If Me.CheckPreview = False Then
        '        'Open the PrintDialog
        '        Me.printDialog1.Document = Me.printDocument1
        '        Dim drl As DialogResult = Me.printDialog1.ShowDialog()
        '        If drl = Windows.Forms.DialogResult.OK Then
        '            '---------------------------------------------------------------------------------------------
        '            'update when print
        '            Dim objReprint As New tb_ProductionOrderTAG(tb_ProductionOrderTAG.enuOperation_Type.UPDATE)
        '            objReprint.UpdatePrintStatus(Document_Index, 1, "")
        '            'Reload Print After update
        '            objReport.GetVIEW_SAICO_RPT_PALLETSLIP_PD(" And ProductionOrderTAG_Index = '" & Document_Index & "' ")
        '            oDTReport = objReport.GetDataTable
        '            oDTReport.Columns.Add("BARCODE", GetType(System.Byte()))
        '            oDTReport.Columns.Add("BARCODEQTY", GetType(System.Byte()))
        '            For Each dr As DataRow In oDTReport.Rows
        '                objBarcode.GenBarcode(dr("ProductionOrderTAG_No").ToString)
        '                objBarcode.GenBarcode(dr("Qty_Per_Tag").ToString)
        '                dr("BARCODEQTY") = ConvertFilePathToByte(Application.StartupPath & "\" & dr("Qty_Per_Tag").ToString & ".bmp")
        '                dr("BARCODE") = ConvertFilePathToByte(Application.StartupPath & "\" & dr("ProductionOrderTAG_No").ToString & ".bmp")
        '                System.IO.File.Delete(Application.StartupPath & "\" & dr("Qty_Per_Tag").ToString & ".bmp")
        '                System.IO.File.Delete(Application.StartupPath & "\" & dr("ProductionOrderTAG_No").ToString & ".bmp")
        '            Next
        '            odsTAG = New DataSet
        '            odtTAG.TableName = "VIEW_WINDOW_THAIO_FG_PRINT_PALLET_TAG"
        '            odsTAG.DataSetName = "dsPalletSlip_PD"
        '            odsTAG.Tables.Add(oDTReport)
        '            cry = oconfig_Report.GetReportInfo("PALLETSLIP_PD", odsTAG)
        '            '---------------------------------------------------------------------------------------------

        '            'Get the Copy times
        '            Dim nCopy As Integer = Me.printDocument1.PrinterSettings.Copies
        '            'Get the number of Start Page
        '            Dim sPage As Integer = Me.printDocument1.PrinterSettings.FromPage
        '            'Get the number of End Page
        '            Dim ePage As Integer = Me.printDocument1.PrinterSettings.ToPage
        '            'Get the printer name
        '            Dim PrinterName As String = Me.printDocument1.PrinterSettings.PrinterName
        '            'Dim crReportDocument As New ReportDocument
        '            ''Create an instance of a report
        '            'crReportDocument = New Chart()
        '            Try
        '                'Set the printer name to print the report to. By default the sample
        '                'report does not have a defult printer specified. This will tell the
        '                'engine to use the specified printer to print the report. Print out 
        '                'a test page (from Printer properties) to get the correct value.
        '                cry.PrintOptions.PrinterName = PrinterName
        '                'Start the printing process. Provide details of the print job
        '                'using the arguments.
        '                cry.PrintToPrinter(nCopy, False, sPage, ePage)
        '                'Let the user know that the print job is completed
        '                'MessageBox.Show("Report finished printing!")

        '            Catch err As Exception
        '                MessageBox.Show(err.ToString())
        '            End Try
        '        End If
        '    End If
        'Catch ex As Exception
        '    MessageBox.Show(ex.ToString())
        'End Try
    End Sub

    Private Sub btnImportPRQ_txtfile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportPRQ_txtfile.Click
        Try
            Dim frm As New WMS_Site_Topcharoen_P2.frmImport_SO_PRQ_TextFile
            'frm.Document_Group_Name = Document_Group_Name
            frm.ShowDialog()
            Me.getSOViewSearch()
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub

    Private Sub btnClear_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        Try
            If grdSOView.Rows.Count = 0 Then Exit Sub
            'CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()
            'Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 and Status not in (2,6)")
            'For Each drso As DataRow In drArr
            '    W_MSG_Information(GetMessage_Data("400030") & drso("SalesOrder_No") & GetMessage_Data("400059"))
            '    Exit Sub
            'Next
            Select Case grdSOView.CurrentRow.Cells("Col_Status").Value
                Case "2", "6" 'รอเบิก
                    Dim objDB As New DBType_SQLServer
                    Dim dtDB As New DataTable
                    dtDB = objDB.DBExeQuery(String.Format("select * from tb_SalesOrderItem where isnull(Total_Qty_Withdraw,0) > 0 and SalesOrder_Index = '{0}' ", Me.grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value))
                    If dtDB.Rows.Count > 0 Then
                        W_MSG_Confirm("มีรายการเบิกสินค้าไม่สามารถคืนสถานะได้")
                        Exit Sub
                    End If
                    If W_MSG_Confirm("คุณต้องการปรับแก้ไขเอกสารใช่หรือไม่ ?") = Windows.Forms.DialogResult.No Then Exit Sub
                    Dim tSaleOrder_Index As String = Me.grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_Index").Value
                    Dim tSaleOrder_No As String = Me.grdSOView.Rows(grdSOView.CurrentRow.Index).Cells("Col_SalesOrder_No").Value
                    Dim oupdate As New ml_TSS
                    oupdate.UPDATE_STATUS_SO(tSaleOrder_Index, 1)
                    ''เขียว
                    objDB.DBExeNonQuery(String.Format("Update tb_salesorderitem set RGB_Check = 0 where SalesOrder_Index = '{0}'", tSaleOrder_Index))
                    objDB.DBExeNonQuery(String.Format("Update tb_salesorder set RGB_Check = 0 where SalesOrder_Index = '{0}'", tSaleOrder_Index))



                    ' --- STEP 3: Update status in Sy_Audit_Log
                    'insert log
                    Dim obj_cls As New cls_syAditlog
                    obj_cls.Process_ID = 10
                    obj_cls.Description = "คืนสถานะเป็นรอยืนยัน"
                    obj_cls.Document_Index = tSaleOrder_Index
                    obj_cls.Document_No = tSaleOrder_No
                    obj_cls.Log_Type_ID = 154
                    obj_cls.Insert_Master()

                    W_MSG_Information_ByIndex(1)
                    Me.getSOViewSearch()

                Case Else
                    W_MSG_Information("สถานะต้องเป็น 'รอเบิก','ค้างจ่าย' เท่านั้น")
            End Select


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdSOView_Sorted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdSOView.Sorted
        Try
            Me.Chang_RGB()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub gbPrintReport_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub chkSKU_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnSku_Popup_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSku_Popup.Click
        Try
            Dim frm As New frmProduct_Popup(frmProduct_Popup.enuOperation_Type.ADDNEW, "")
            'frm.Customer_Index = ""
            frm.ShowDialog()
            frm.Close()
            If (frm.Sku_Index <> "") Or (Not frm.Sku_Index Is Nothing) Then
                txtSKU_ID.Text = frm.Sku_ID
                'Me.chkSKU.Checked = True
            Else
                txtSKU_ID.Text = ""
                ' Me.chkSKU.Checked = False
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnExportExcelRemain_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExportExcelRemain.Click
        Try
            CType(Me.grdSOView.DataSource, DataTable).AcceptChanges()
            If CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1").Length = 0 Then
                W_MSG_Information("กรุณาเลือก รายการที่จะ Export")
                Exit Sub
            End If


            Dim ds As New DataSet

            Dim objExport As New Export_Excel_KC
            Dim clsKsl As New cls_KSL
            Dim SO_NO As New List(Of String)

            For Each dtrow As DataRow In CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1 Or chkSelect=True")
                ' If dtrow.Item("chkselect") = True Then
                SO_NO.Add(dtrow("SalesOrder_No"))
                'End If
            Next
            If SO_NO.Count = 0 Then Exit Sub
            Dim xdt As DataTable = clsKsl.getReportRemain(" and SalesOrder_No in ('" & String.Join("','", SO_NO.ToArray) & "')")
            If xdt.Rows.Count = 0 Then
                W_MSG_Information("รายการที่เลือก ไม่มีงานค้าง")
                Exit Sub
            End If
            ds.Tables.Add(xdt)
            ds.Tables(0).TableName = Now.ToString("yyyyMMddHHmm")
            objExport.export(ds, "รายงานใบสั่งขาย")

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub rdbExpected_Delivery_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbExpected_Delivery_Date.CheckedChanged
        Me.dtpBeginDate.Visible = True
        Me.dtpEndDate.Visible = True
        Me.txtKeySearch.Visible = False
    End Sub

    Private Sub rdbHubId_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbHubId.CheckedChanged
        Me.dtpBeginDate.Visible = False
        Me.dtpEndDate.Visible = False
        'Me.lbl_to.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub rdbAdd_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbAdd_Date.CheckedChanged
        Me.dtpBeginDate.Visible = True
        Me.dtpEndDate.Visible = True
        Me.txtKeySearch.Visible = False
        'Me.txtKeySearch.Text = ""
        'Me.lbl_to.Visible = True
    End Sub

    'Private Sub btnPreY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreY.Click
    '    Try

    '        updateSOandStatus("Y")
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try
    'End Sub

    Sub updateSOandStatus(ByVal so_type As String)
        Try

            If W_MSG_Confirm("คุณต้องการย้อนสถานะ " & so_type & "หรือไม่ ") = Windows.Forms.DialogResult.Yes Then

                Dim drArr() As DataRow = CType(Me.grdSOView.DataSource, DataTable).Select("chkSelect=1")
                If drArr.Length <= 0 Then
                    W_MSG_Information("กรุณาเลือกรายการที่สามารถย้อนได้เท่านั้น !!")
                    Exit Sub
                End If

                Dim listError As New List(Of String)

                Dim oClsSO As New clsSO

                For Each drAll As DataRow In drArr
                    If oClsSO.updateStatusAndSoType(so_type, drAll.Item("SalesOrder_Index")) <= 0 Then
                        listError.Add(drAll.Item("SalesOrder_No"))
                    End If
                Next

                If listError.Count > 0 Then
                    Dim strError As String = ""
                    strError = "มีรายการที่ไม่สามารถย้อนสถานะได้คือ " & vbCrLf & String.Join(vbCrLf, listError.ToArray)
                    W_MSG_Error(strError)

                    Me.getSOViewSearch(False)
                Else
                    W_MSG_Information("ย้อนสถานะเสร็จสิ้น")
                    Me.getSOViewSearch(False)
                End If

            End If
        Catch ex As Exception

            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    'Private Sub btnPreZ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreZ.Click
    '    Try

    '        updateSOandStatus("Z")
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message.ToString)
    '    End Try
    'End Sub
End Class


