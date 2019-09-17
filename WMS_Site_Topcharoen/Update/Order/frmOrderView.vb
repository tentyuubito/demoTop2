Imports WMS_STD_CONFIGURATION
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports System.Data.OleDb
Imports System.Windows.Forms
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_INB_Report

Public Class frmOrderView
    Public CustomerIndex As String = ""
    Public CustomerID As String = ""
    Public CustomerName As String = ""
    Public Location_Alias As String = ""

    Public _ReceiveType As Integer = 0

    Private DEFAULT_USE_REPORTPRINTOUT_BOND As String = ""
    Private _USE_ASSIGNEJOB_ITEM As Boolean = False

    Private objStatus As enuOperation_Type
    Dim _QCRequest_Index As String = ""
    Public SaveType As Integer = 0
    Private gintRowStart As Integer = 1
    Private gintRowEnd As Integer = 1
    Private _CountRow As Integer = 0
    Private _iNewRow As Integer = 0

    Public showBtn_cancel As Boolean = False

    Public Enum enuOperation_Type
        ADDNEW
        UPDATE
        NULL
    End Enum

    Public Property ReceiveType() As Integer
        Get
            Return _ReceiveType
        End Get
        Set(ByVal value As Integer)
            _ReceiveType = value
        End Set
    End Property

    Public Property QCRequest_Index() As String
        Get
            Return _QCRequest_Index
        End Get
        Set(ByVal value As String)
            _QCRequest_Index = value
        End Set
    End Property

    Private _LocationID As String


    Public Property LocationID() As String
        Get
            Return _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property


    Private _USE_PUTAWAY_BY_TAG As Boolean = False
    Public Property USE_PUTAWAY_BY_TAG() As Boolean
        Get
            Return _USE_PUTAWAY_BY_TAG
        End Get
        Set(ByVal value As Boolean)
            _USE_PUTAWAY_BY_TAG = value
        End Set
    End Property

    Private _USE_PUTAWAY_BTNCONFIRM As Boolean = False
    Public Property USE_PUTAWAY_BTNCONFIRM() As Boolean
        Get
            Return _USE_PUTAWAY_BTNCONFIRM
        End Get
        Set(ByVal value As Boolean)
            _USE_PUTAWAY_BTNCONFIRM = value
        End Set
    End Property

#Region "GET DATA TO DATAGRIDVIEW"

    Private Sub getOrderview()
        Dim objClassDB As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strWhere As String = ""
        Dim CaseLoadAll As Boolean
        CaseLoadAll = rdAll.Checked
        Dim CaseLoad As Boolean = False
        CaseLoad = rdTop.Checked
        If rdRowPage.Checked = True Then
            If cboRowPerPage.Text = "" Then
                cboRowPerPage.Text = 50
            End If

        End If
        

        Try
            ' *** WHERE STRING *** 
            If Me.rdbOrder_Date.Checked = True Then
                Dim StartDate As String = Format(dtpDate.Value, "MM/dd/yyyy").ToString() + " 00:00:00"
                Dim EndtDate As String = Format(dateEnd.Value, "MM/dd/yyyy").ToString() + "  23:59:00"
                strWhere = "  AND Order_Date between '" & StartDate & "' and  '" & EndtDate & "'"
            ElseIf Me.rdbOrder_No.Checked = True Then
                strWhere = " AND VIEW_Orderview.Order_No Like '" & Me.txtKeySearch.Text & "%'"

            ElseIf Me.rdbCustomer.Checked = True Then
                If Not txtKeySearch.Text = "" Then
                    If txtKeySearch.Tag Is Nothing Then
                        strWhere = " AND VIEW_Orderview.Customer_Name Like '" & Me.txtKeySearch.Text & "%' "
                    Else
                        strWhere = " AND VIEW_Orderview.customer_Index = '" & Me.txtKeySearch.Tag & "' "
                    End If
                End If
            ElseIf Me.rdbSupplier.Checked = True Then
                If txtKeySearch.Tag Is Nothing Then
                    strWhere = " AND VIEW_Orderview.Supplier_Name Like '" & Me.txtKeySearch.Text & "%' "
                Else
                    strWhere = " AND VIEW_Orderview.Supplier_Index = '" & Me.txtKeySearch.Tag & "' "
                End If


            ElseIf Me.rdbDepartment.Checked = True Then
                strWhere = " AND VIEW_Orderview.Department_Name not in ('') AND VIEW_Orderview.Department_Name Like '" & Me.txtKeySearch.Text & "%' "
            ElseIf Me.rdbReferent.Checked = True Then
                strWhere = " AND VIEW_Orderview.Ref_No1 like '" & Me.txtKeySearch.Text & "%' "
            ElseIf Me.rdbPo.Checked = True Then
                strWhere = " and order_Index in(Select  order_Index from tb_orderitem LEFT JOIN "
                strWhere += " ms_SKU on tb_orderitem.sku_index = ms_SKU.sku_index "
                strWhere += " where tb_orderitem.str1 like '" & Me.txtKeySearch.Text & "%' ) "
            ElseIf Me.rdb_Sku.Checked = True Then
                strWhere = " and order_Index in(Select  order_Index from tb_orderitem LEFT JOIN "
                strWhere += " ms_SKU on tb_orderitem.sku_index = ms_SKU.sku_index "
                strWhere += " where ms_SKU.Sku_Id like '" & Me.txtKeySearch.Text & "%' ) "
            End If
            ' ********************

            Select Case Me.cboDocumentStatus.SelectedValue
                Case 0
                    strWhere += " "
                Case Else
                    strWhere += " AND VIEW_Orderview.Status=" & Me.cboDocumentStatus.SelectedValue
            End Select

            Select Case Me.cbReciveType.SelectedValue
                Case 0
                    strWhere += " "
                Case Else
                    strWhere += " AND VIEW_Orderview.DocumentType_Index=" & Me.cbReciveType.SelectedValue
            End Select

            'Config Customer
            Dim oUser As New WMS_STD_Master_Datalayer.se_User(WMS_STD_Master_Datalayer.se_User.enuOperation_Type.SEARCH)
            strWhere &= oUser.GetUserByCustomer()
            'Config DistributionCenter
            strWhere &= New clsUserByDC().GetDistributionCenterByUser()

            Me.cboRowPerPage.SelectedIndex = IIf(Me.cboRowPerPage.SelectedIndex > 0, Me.cboRowPerPage.SelectedIndex, 0)

            If CaseLoadAll = False Then
                If CaseLoad = False Then ' True : Top100 || False : Paging All Update By ton 2015/04/24
                    Dim odtSO_Count As New DataTable
                    objClassDB.getOrderViewSearch_Count(strWhere)
                    odtSO_Count = objClassDB.DataTable

                    ' Get total records of the current search
                    If odtSO_Count.Rows.Count > 0 Then
                        Me.txtRowCount.Text = odtSO_Count.Rows(0)("Row_Total").ToString
                    Else
                        Me.txtRowCount.Text = 0
                    End If
                End If
            End If

            Call Calculate_Paging()

            Dim oReader As DataTable 'Data.SqlClient.SqlDataReader
            oReader = objClassDB.getOrderviewSearch_Reader(strWhere, gintRowStart, gintRowEnd, CaseLoad, txtTop.Text, CaseLoadAll)
            'objtb_SalesOrder.getSOViewSearch(strWhere)
            objDT = objClassDB.DataTable 'Load(oReader)
            'oReader.Close()
            'objDT = objtb_SalesOrder.DataTable

            'Update By Art 13-06-2012 : ค้นหาต่อท้ายของเก่า
            With objDT.Columns
                If Not .Contains("chkSelect") Then
                    .Add("chkSelect", GetType(Boolean))
                End If
            End With
            With objDT.Columns
                If Not .Contains("Count") Then
                    .Add("Count", GetType(Integer))
                End If
            End With

            'objClassDB.getOrderView(strWhere)  'Comment By ton 24/04/2015
            'objDT = objClassDB.DataTable

            ' Me.grdList.DataSource = objDT
            ' *** Clear datagridview ***
            Me.grdOrderView.Rows.Clear()
            Me.grdOrderView.Refresh()

            For i As Integer = 0 To objDT.Rows.Count - 1
                With Me.grdOrderView
                    Me.grdOrderView.Rows.Add()
                    .Rows(i).Cells("System_Index").Value = objDT.Rows(i).Item("Order_Index").ToString
                    .Rows(i).Cells("Order_Date").Value = Format(CDate(objDT.Rows(i).Item("Order_Date")), "dd/MM/yyyy").ToString '  ' Format(Today, "dd/MM/yyyy").ToString 
                    .Rows(i).Cells("Document_No").Value = objDT.Rows(i).Item("Order_No").ToString
                    .Rows(i).Cells("Customer_Name").Value = objDT.Rows(i).Item("Customer_Name").ToString
                    .Rows(i).Cells("Customer_Name").Tag = objDT.Rows(i).Item("Customer_Index").ToString
                    '  *** Reference By Tag property  to check Enable Button ***
                    .Rows(i).Cells("Status").Value = objDT.Rows(i).Item("StatusDescription").ToString
                    .Rows(i).Cells("StatusValue").Value = objDT.Rows(i).Item("Status").ToString
                    .Rows(i).Cells("Add_By").Value = objDT.Rows(i).Item("Add_By").ToString
                    .Rows(i).Cells("cl_Supplier").Value = objDT.Rows(i).Item("Supplier_Name").ToString
                    .Rows(i).Cells("cl_Referent").Value = objDT.Rows(i).Item("Ref_No1").ToString
                    .Rows(i).Cells("cl_OrderType").Value = objDT.Rows(i).Item("DocType").ToString
                    .Rows(i).Cells("col_Receive_Type").Value = objDT.Rows(i).Item("Receive_Type").ToString
                    .Rows(i).Cells("col_Declaration_No").Value = objDT.Rows(i).Item("Ref_No1").ToString
                    .Rows(i).Cells("col_DistributionCenter").Value = objDT.Rows(i).Item("DistributionCenter").ToString


                    Dim intStatus As Integer = objDT.Rows(i).Item("Status")
                    Select Case intStatus
                        Case -1
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.Pink
                        Case 2
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightGreen
                        Case 4
                            .Rows(i).Cells("Document_No").Style.BackColor = Color.LightYellow
                        Case Else

                    End Select
                End With
            Next

            Me.btnEditOrder.Enabled = True

            If grdOrderView.Rows.Count > 0 Then
                grdOrderView.Rows(0).Selected = False
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

#Region " STATUS OF DOCUMENT "
    Private Sub getProcessStatus()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcessStatus()
            objDT = objClassDB.DataTable

            With cboDocumentStatus
                .DisplayMember = "Description"
                .ValueMember = "Status"
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

    Private Sub getReportName(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New WMS_STD_Master.config_Report '(enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.GetListReportName(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cbPrint
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

    Private Sub getReciveType()
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        Dim objClassDB As New ms_DocumentType(OrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getDocumentType(1)
            objDT = objClassDB.DataTable

            Dim cbItem(1) As String
            cbItem(0) = "0"
            cbItem(1) = "ไม่ระบุ"
            objDT.Rows.Add(cbItem)

            With cbReciveType
                .DisplayMember = "Description"
                .ValueMember = "DocumentType_Index"
                .DataSource = objDT
            End With

            ' *************************************
            cbReciveType.SelectedIndex = cbReciveType.Items.Count - 1
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub
#End Region

#End Region

    Private Sub frmOrderView_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New frmConfigReceive
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 1)
                    oFunction.SW_Language_Column(Me, Me.grdOrderView, 1)
                    oFunction = Nothing
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

  

    Private Sub frmOderView_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            ' ========== Manage Language Functions End ==========
            Dim CustomerID As String
            CustomerID = GetDEFAULT_CUSTOMER_INDEX()


            Me.GetDEFAULT_CUSTOMER_INDEX()
            SETUSE_CUSTOMSETTING()
            SetAUTO_GENTAG()
            ' *** Load data to gridview ***
            Me.getOrderview()
            ' *****************************
            ' *** Load status of document ***
            Me.getProcessStatus()
            Me.getReciveType()
            Me.getReportName(1)

            Select Case Me._USE_PUTAWAY_BTNCONFIRM
                Case True
                    Me.btnConfrim_PutAway.Visible = True
                    Me.btnTag.Visible = False
                Case False
                    Me.btnConfrim_PutAway.Visible = False
                    Me.btnStoreIn.Visible = False
                    Dim objDraw As New System.Drawing.Point((btnTag.Location.X - 116), btnTag.Location.Y)
                    Me.btnTag.Location = objDraw
                    objDraw = New System.Drawing.Point((btnCancel.Location.X - 116), btnCancel.Location.Y)
                    Me.btnCancel.Location = objDraw
                    objDraw = New System.Drawing.Point((btnAssignJob.Location.X - 116), btnAssignJob.Location.Y)
                    Me.btnAssignJob.Location = objDraw
            End Select


            ' ========== Manage Language Functions Begin ==========
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 1)
            oFunction.SW_Language_Column(Me, Me.grdOrderView, 1)
            oFunction = Nothing

            btnCancel.Visible = showBtn_cancel
          

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New frmDeposit_WMS_V2(frmDeposit_WMS_V2.enuOperation_Type.ADDNEW)
        frm.ShowDialog()
        Me.getOrderview()
    End Sub

    Private Sub btnTestJobOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New WMS_STD_INB_Receive.frmJobOrderProductList(WMS_STD_INB_Receive.frmJobOrderProductList.enuOperation_Type.SEARCH, Me.grdOrderView.CurrentRow.Cells("System_Index").Value)
        frm.ShowDialog()
    End Sub


    Private Sub btnJobOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim frm As New WMS_STD_INB_Receive.frmJobOrderProductList(WMS_STD_INB_Receive.frmJobOrderProductList.enuOperation_Type.SEARCH, Me.grdOrderView.CurrentRow.Cells("System_Index").Value)
        frm.ShowDialog()
        Me.getOrderview()
    End Sub

    Private Sub grdOrderView_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles grdOrderView.SelectionChanged
        Try
            Me.ButtonManage()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub grdOrderView_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderView.CellClick
        Try
            Me.ButtonManage()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub ButtonManage()
        Try
            If Me.grdOrderView.RowCount = 0 Then Exit Sub
            Select Case grdOrderView.CurrentRow.Cells("StatusValue").Value
                Case "-1" 'Cancel
                    Me.btnCancel.Enabled = False
                    Me.btnStoreIn.Enabled = False
                    Me.btnEditOrder.Enabled = True
                    Me.btnTag.Enabled = True
                    Me.btnConfrim_PutAway.Enabled = False
                Case "1" 'Not Putaway
                    Me.btnCancel.Enabled = True
                    Me.btnStoreIn.Enabled = True
                    Me.btnEditOrder.Enabled = True
                    Me.btnTag.Enabled = True
                    Me.btnConfrim_PutAway.Enabled = True

                Case "2" 'Putaway complete
                    Me.btnCancel.Enabled = True
                    Me.btnStoreIn.Enabled = False
                    Me.btnEditOrder.Enabled = True
                    Me.btnTag.Enabled = True
                    Me.btnConfrim_PutAway.Enabled = False
                Case "5", "4" 'Putaway Not complete
                    Me.btnCancel.Enabled = True
                    Me.btnStoreIn.Enabled = True
                    Me.btnEditOrder.Enabled = True
                    Me.btnTag.Enabled = True
                    Me.btnConfrim_PutAway.Enabled = True
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "Search"

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            ' *** Load data to gridview ***
            Me.getOrderview()
            ' *****************************
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


#End Region

    Private Sub rdbOrder_Date_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOrder_Date.CheckedChanged
        Me.dtpDate.Visible = True
        Me.txtKeySearch.Visible = False
        Me.txtKeySearch.Text = ""

        If rdbOrder_Date.Checked = True Then
            Me.lb_to.Visible = True
            Me.dateEnd.Visible = True
        Else
            Me.lb_to.Visible = False
            Me.dateEnd.Visible = False
        End If
    End Sub

    Private Sub rdbOrder_No_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbOrder_No.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub rdbCustomer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbCustomer.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        Me.btnPop_Search.Visible = rdbCustomer.Checked
    End Sub

    Private Sub rdbSupplier_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbSupplier.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
        Me.btnPop_Search.Visible = rdbSupplier.Checked
    End Sub

    Private Sub rdbDepartment_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbDepartment.CheckedChanged, rdb_Sku.CheckedChanged, rdbPo.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub cboDocumentStatus_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboDocumentStatus.SelectedIndexChanged, cbReciveType.SelectedIndexChanged
        btnSearch_Click(Me, e)
    End Sub

    Sub SetnumRows()
        Dim numRows As Integer = 0

        numRows = grdOrderView.Rows.Count
        If numRows > 0 Then
            lbCountRows.Text = "พบจำนวน " & numRows & " รายการ แสดง " & numRows & " รายการ"
            txtRowCount.Text = numRows
        Else
            lbCountRows.Text = "ไม่พบรายการ"
        End If
    End Sub

    Private Sub grdOrderView_RowsAdded(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsAddedEventArgs) Handles grdOrderView.RowsAdded
        SetnumRows()
    End Sub

    Private Sub grdOrderView_RowsRemoved(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewRowsRemovedEventArgs) Handles grdOrderView.RowsRemoved
        SetnumRows()
    End Sub

#Region "BTN Down"
    Private Sub btnNewOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNewOrder.Click
        Try
            Dim frm As New frmDeposit_WMS_V2(frmDeposit_WMS_V2.enuOperation_Type.ADDNEW)
            With frm
                .CustomerID = Me.CustomerID
                .CustomerIndex = Me.CustomerIndex
                .CustomerName = Me.CustomerName
                .Location_Alias = Location_Alias
                .USE_PUTAWAY_BY_TAG = Me._USE_PUTAWAY_BY_TAG
                .USE_PUTAWAY_BTNCONFIRM = Me.USE_PUTAWAY_BTNCONFIRM
            End With

            frm.Icon = Me.Icon
            frm.ShowDialog()
            Me.getOrderview()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Function GetDEFAULT_LOCATION()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        'Public CustomerID As String = ""
        'Public CustomerName As String = ""

        Try
            objCustomSetting.GetConfig_Value("DEFAULT_Location", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Location_Alias = objDT.Rows(0).Item("Config_Value").ToString
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

        Return Location_Alias

    End Function

    Function GetDEFAULT_CUSTOMER_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        'Public CustomerID As String = ""
        'Public CustomerName As String = ""

        Try
            objCustomSetting.GetDEFAULT_CUSTOMER_INDEX()
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                CustomerIndex = objDT.Rows(0).Item("Customer_Index").ToString
                CustomerID = objDT.Rows(0).Item("Customer_Id").ToString
                CustomerName = objDT.Rows(0).Item("Customer_Name").ToString

            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try

        Return CustomerID
        'Return CustomerName
        'Return CustomerIndex
    End Function

    Sub SETUSE_CUSTOMSETTING()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            Me._USE_PUTAWAY_BY_TAG = objCustomSetting.getConfig_Key_USE("USE_PUTAWAY_BY_TAG")
            Me._USE_PUTAWAY_BTNCONFIRM = objCustomSetting.getConfig_Key_USE("USE_PUTAWAY_BTNCONFIRM")
            Me._USE_ASSIGNEJOB_ITEM = objCustomSetting.getConfig_Key_USE("USE_ASSIGNEJOB_ITEM")
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub


    Private Sub btnEditOrder_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEditOrder.Click
        If grdOrderView.Rows.Count <= 0 Then Exit Sub
        Try

            Dim strStatus As String = grdOrderView.CurrentRow.Cells("StatusValue").Value
            Dim boolReadOnly As Boolean = False
            Select Case strStatus
                Case Nothing
                    Exit Sub
                    'Case "-1", "2", "3", "5"
                    '    boolReadOnly = True
                Case "1"
                    boolReadOnly = False
                Case Else
                    boolReadOnly = True
            End Select

            Dim frm As New frmDeposit_WMS_V2(frmDeposit_WMS_V2.enuOperation_Type.UPDATE, Me.grdOrderView.CurrentRow.Cells("System_Index").Value)
            With frm
                .Icon = Me.Icon
                .WithdrawStatus = boolReadOnly
                .Location_Alias = Location_Alias
                .USE_PUTAWAY_BY_TAG = Me._USE_PUTAWAY_BY_TAG
                .USE_PUTAWAY_BTNCONFIRM = Me.USE_PUTAWAY_BTNCONFIRM
                .ShowDialog()
            End With
            Me.getOrderview()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub btnStoreIn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnStoreIn.Click
        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If grdOrderView.Rows.Count <= 0 Then Exit Sub
            If grdOrderView.CurrentRow.Cells("StatusValue").Value = "-1" Or grdOrderView.CurrentRow.Cells("StatusValue").Value = "2" Or grdOrderView.CurrentRow.Cells("StatusValue").Value = "3" Or grdOrderView.CurrentRow.Cells("StatusValue").Value = "" Then
                Exit Sub
            End If

            If grdOrderView.Rows.Count = 0 Then
                Exit Sub
            End If

            'Select Case _USE_PUTAWAY_BY_TAG
            '    Case 0
            '        Dim frm As New frmJobOrderProductList(frmJobOrderProductList.enuOperation_Type.SEARCH, Me.grdOrderView.CurrentRow.Cells("System_Index").Value)
            '        frm.ShowDialog()
            '    Case 1
            Dim frm As New WMS_STD_INB_Receive.frmTag_Main
            frm.Icon = Me.Icon
            frm.Order_Index = Me.grdOrderView.CurrentRow.Cells("System_Index").Value
            frm.ShowDialog()
            'Dim frm As New frmPutawayWithTAG
            'frm.Order_Index = grdOrderView.Rows(grdOrderView.CurrentRow.Index).Cells("System_Index").Value.ToString
            'frm.ShowDialog()
            'End Select
            Me.getOrderview()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If grdOrderView.Rows.Count <= 0 Then Exit Sub
        If grdOrderView.CurrentRow.Cells("StatusValue").Value = "-1" Then Exit Sub
        Dim objDB As New OrderTransaction(OrderTransaction.enuOperation_Type.CANCEL)
        If CHECK_CANCEL(Me.grdOrderView.CurrentRow.Cells("System_Index").Value) < 0 Then Exit Sub

        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If W_MSG_Confirm("คุณต้องการยกเลิกรายการเลขที่เอกสาร " & Me.grdOrderView.CurrentRow.Cells("Document_No").Value & "   ใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then

                If objDB.Cancel_Order(Me.grdOrderView.CurrentRow.Cells("System_Index").Value) = True Then

                    W_MSG_Information("ยกเลิกรายการเรียบร้อยแล้ว")
                    objDB = Nothing
                    ' *** Refresh data *** 
                    Me.getOrderview()

                    Exit Sub
                Else
                    W_MSG_Information("ไม่สามารถยกเลิกรายการได้ ระบบทำงานผิดพลาด")
                    objDB = Nothing
                    Exit Sub
                End If
                ' Else
                ' W_MSG_Information(strMsg)
                'objDB = Nothing
                'Exit Sub
                'End If
                'objDB = Nothing
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Function CHECK_CANCEL(ByVal strOrder_Index As String) As Integer 'If < 0 = False
        Try
            Dim sqlDB As New SQLCommands
            Dim sqlDBMsg As New SQLCommands
            Dim objDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.SEARCH)
            Dim dtiOrderdate As DateTime
            Dim strOrderStatus As String = ""
            sqlDB.SQLComand("select * from tb_Order where Order_Index='" & strOrder_Index & "'")
            strOrderStatus = sqlDB.DataTable.Rows(0).Item("Status")
            dtiOrderdate = sqlDB.DataTable.Rows(0).Item("Order_Date")
            sqlDB = Nothing

            '#### รายการยัง ไม่ยืนยัน  = ยกเลิกได้เลย
            If strOrderStatus = 1 Then
                Return 1
            End If

            '#### OrdrDate +- x วันจากวัน ปัจจุบัน
            'If objDB.isMaxDay(dtiOrderdate) < 0 Then
            '    clMsg.SettingMsg(500006)
            '    sqlDBMsg.SQLComand("select convert(integer,config_value) as MaxDay from config_CustomSetting where Config_Key = 'MaxDay'")
            '    MessageBox.Show(clMsg.Data + "  " + sqlDBMsg.DataTable.Rows(0).Item("MaxDay").ToString + "  " + clMsg.Data2, clMsg.Header, MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    sqlDBMsg = Nothing
            '    Return -1
            'End If

            '#### Order มีการใช้งานหรือไม่  ,ใช้งานไม่สามารถ ยกเลิกได้
            If objDB.isUseData(strOrder_Index) < 0 Then
                W_MSG_Information_ByIndex(500007)
                sqlDBMsg = Nothing
                Return -1
            End If


            Return 1

        Catch ex As Exception
            W_MSG_Error(ex.Message)
            Return -10
        End Try
    End Function

    Private Sub btnExit_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

#End Region

#Region "Print Reports"

    Private Sub btn_Print_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Print.Click

        If grdOrderView.RowCount <= 0 Then Exit Sub
        Dim oconfig_Report As New WMS_STD_Master.config_Report
        Dim Report_Name As String = Me.cbPrint.SelectedValue.ToString
        Try
            Dim frm As New frmReportViewerMain
            Dim oReport As New Loading_Report(Report_Name, "And Order_Index ='" & grdOrderView.CurrentRow.Cells("System_Index").Value & "'")
            frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            frm.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
        End Try
    End Sub
#End Region

    Sub SetDEFAULT_USE_REPORTPRINTOUT_BOND()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_REPORTPRINTOUT_BOND", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me.DEFAULT_USE_REPORTPRINTOUT_BOND = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me.DEFAULT_USE_REPORTPRINTOUT_BOND = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Private Sub rdbReferent_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbReferent.CheckedChanged
        Me.dtpDate.Visible = False
        Me.txtKeySearch.Visible = True
        Me.txtKeySearch.Text = ""
    End Sub

    Private Sub btnTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTag.Click
        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            'If Me.grdOrderView.Rows.Count > 0 Then
            Dim frm As New frmTag_Main_V4 'frmTag_Main_V3
            frm.Icon = Me.Icon
            frm.Order_Index = "" 'Me.grdOrderView.CurrentRow.Cells("System_Index").Value
            frm.ShowDialog()
            Me.getOrderview()
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)

        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnPop_Search_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPop_Search.Click
        Try
            If rdbCustomer.Checked = True Then
                Dim frm As New frmCustmer_Popup
                frm.ShowDialog()
                '    *** Recive value ****
                Dim tmpCustomer_Index As String = ""
                tmpCustomer_Index = frm.Customer_Index 'เรียก frm.Customer_Index ที่ Customer_Index ที่เราส่งค่ามา

                If Not tmpCustomer_Index = "" Then
                    Me.txtKeySearch.Text = frm.customerName
                    Me.txtKeySearch.Tag = frm.Customer_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
                ' *********************
                frm.Close()
            End If
            If rdbSupplier.Checked = True Then
                Dim frm As New frmSupplier_Popup
                frm.ShowDialog()
                '    *** Recive value **** มาแสดงในตัวแปล 
                Dim tmpSupplier_Index As String = ""
                tmpSupplier_Index = frm.Supplier_Index
                If Not tmpSupplier_Index = "" Then

                    Me.txtKeySearch.Text = frm.SupplierName_eng
                    Me.txtKeySearch.Tag = frm.Supplier_Index
                Else
                    Me.txtKeySearch.Text = ""
                    Me.txtKeySearch.Tag = ""
                End If
                ' *********************
                frm.Close()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub grdOrderView_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderView.CellDoubleClick
        btnEditOrder_Click(sender, e)
    End Sub
#Region "   CONFIRM PUTAWAY   "
    Enum conenmGenTag_Type
        NotGen = 0
        NormalGen = 1
        GenPerQty = 2
    End Enum
    Private _DEFAULT_AUTO_TAG As conenmGenTag_Type
    Public Property DEFAULT_AUTO_TAG() As conenmGenTag_Type
        Get
            Return _DEFAULT_AUTO_TAG
        End Get
        Set(ByVal value As conenmGenTag_Type)
            _DEFAULT_AUTO_TAG = value
        End Set
    End Property
    Sub SetAUTO_GENTAG()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("AUTO_GENTAG", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._DEFAULT_AUTO_TAG = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me._DEFAULT_AUTO_TAG = 0
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfrim_PutAway.Click

        Try

            Dim oSession As New WMS_STD_Master.UserSession
            Dim oBolCheck As Boolean = oSession.CheckSession
            If oBolCheck = False Then
                Exit Sub
            End If

            If grdOrderView.RowCount = 0 Then Exit Sub

            If W_MSG_Confirm("คุณต้องการยืนยันการจัดเก็บรายการเลขที่เอกสาร " & Me.grdOrderView.CurrentRow.Cells("Document_No").Value & "   ใช่หรือไม่ ") = Windows.Forms.DialogResult.Yes Then
                Dim strOrder_Index As String = Me.grdOrderView.CurrentRow.Cells("System_Index").Value.ToString
                Dim objClassDB As New OrderTransaction(OrderTransaction.enuOperation_Type.SEARCH)
                Dim objDT As DataTable = New DataTable
                objClassDB.getOrderDetail_ConfrimPutAway(strOrder_Index)
                objDT = objClassDB.DataTable

                For Each drOrderItem As DataRow In objDT.Rows
                    If drOrderItem("str4").ToString = "" Then
                        W_MSG_Information("พบรายการที่ไม่ได้ ระบุตำแหน่ง")
                        Exit Sub
                    Else
                        Dim objSerial As New tb_OrderItemSerial
                        If objSerial.CheckIsSerial(drOrderItem("Sku_Index").ToString) = True Then
                            If objSerial.CheckOrderItemCreateSerialIsComplete(drOrderItem("OrderItem_Index").ToString) = False Then
                                W_MSG_Information("พบรายการที่ ระบุ Serial ไม่สมบุรณ์ กรุณาตรวจสอบ !! ")
                                Exit Sub
                            End If
                        End If
                        'Create Tag and OrderItemLocation
                        SetLocation_fromitemAll(drOrderItem)
                    End If
                Next
                'Confrim
                Dim objClassDBSave As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
                objClassDBSave.SaveData(strOrder_Index)
                Me.getOrderview()
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub SetLocation_fromitemAll(ByVal drOrderItem As DataRow)
        Try
            Dim clsJobOrder As New tb_JobOrder
            Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            Dim objOrderItemLocation As New tb_OrderItemLocation
            Dim objCollection As New List(Of tb_OrderItemLocation)

            Dim Recieve_Qty As Double
            Dim Recieve_Total_Qty As Double

            Dim sumQty As Double = 0
            Dim sumTotal_Qty As Double = 0

            'Create Job
            clsJobOrder = setObjJobOrder(drOrderItem)

            objCollection.Clear()

            Recieve_Qty = CDbl(drOrderItem("Qty").ToString) 'Val(Me.grdOrderItem.Rows(pRowIndex).Cells("col_Qty").Value)
            Recieve_Total_Qty = CDbl(drOrderItem("Total_Qty").ToString)

            ' *** insert data to tb_OrderItemlocation ***
            objOrderItemLocation = New tb_OrderItemLocation

            Dim objDBTempIndex As New Sy_AutoNumber
            Dim strOrderItemLocation_Index As String = ""

            With objOrderItemLocation

                .Order_Index = drOrderItem("Order_Index").ToString
                .OrderItem_Index = drOrderItem("OrderItem_Index").ToString

                .Sku_Index = drOrderItem("Sku_Index").ToString
                .Package_Index = drOrderItem("Package_Index").ToString
                .Lot_No = drOrderItem("Lot_No").ToString
                .PLot = drOrderItem("Plot").ToString

                .ItemStatus_Index = drOrderItem("ItemStatus_Index").ToString
                .Serial_No = drOrderItem("Serial_No").ToString

                ' *** Define value from datagridview ***
                .Tag_No = "" 'Me.grdOrderItem.Rows(pRowIndex).Cells("COL_Tag").Value
                ' *** Location Index  ***
                .Location_Index = objClassDB.getLocation_Index(drOrderItem("Str4").ToString).ToString
                .PalletType_Index = drOrderItem("PalletType_Index").ToString
                .Pallet_Qty = drOrderItem("Pallet_Qty").ToString
                '   .PalletType_Index = drOrderItem("Str5").ToString
                .Tag_No = "" 'Me.grdOrderItem.Rows(pRowIndex).Cells("COL_Tag").Value
                .Qty = drOrderItem("Qty").ToString
                .Total_Qty = drOrderItem("Total_Qty").ToString
                .Weight = drOrderItem("Weight").ToString
                .Volume = drOrderItem("Volume").ToString
                ' *** Reference by Tag property ***
                .Ratio = drOrderItem("Ratio").ToString
                'New 23/7/2008 11.56
                .MixPallet = 0
                .Qty_Item = drOrderItem("Item_Qty").ToString
                .OrderItem_Price = drOrderItem("OrderItem_Price").ToString

                ' *** Sum Balance ***
                sumQty = sumQty + .Qty
                sumTotal_Qty = sumTotal_Qty + .Total_Qty



                '--------------- Dong_kk add New Tag  ---------------------

                Dim objItemCollection As New List(Of tb_TAG)
                Dim objItem As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

                Dim objAutoNumber As New Sy_AutoNumber

                SetTagItem(objItem, drOrderItem)
                Select Case Me._DEFAULT_AUTO_TAG
                    Case conenmGenTag_Type.NormalGen
                        objItem.TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                        objItem.TAG_No = objAutoNumber.getSys_Value("TAG_NO")

                        .Tag_No = objItem.TAG_No
                        strOrderItemLocation_Index = objDBTempIndex.getSys_Value("OrderItemLocation_Index")
                        .OrderItemLocation_Index = strOrderItemLocation_Index
                        objCollection.Add(objOrderItemLocation)

                        objItem.OrderItemLocation_Index = strOrderItemLocation_Index
                        objItemCollection.Add(objItem)

                    Case conenmGenTag_Type.GenPerQty

                        ' objItemPerQty = objItem
                        For i As Integer = 1 To CInt(drOrderItem("Qty").ToString)
                            Dim objItemPerQty As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)
                            'Set Value 
                            SetTagItem(objItemPerQty, drOrderItem)

                            'Set Value Per Tag
                            With objItemPerQty
                                .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                                .TAG_No = objAutoNumber.getSys_Value("TAG_NO")
                                .Qty_per_TAG = 1
                                .Weight_per_TAG = .Weight / .Qty
                                .Volume_per_TAG = .Volume / .Qty
                                .Ref_No3 = (i - 1) + 1 & "/" & .Qty
                            End With



                            ' set new OrderItemLocation

                            Dim newobjOrderItemLocation As New tb_OrderItemLocation
                            newobjOrderItemLocation.Order_Index = .Order_Index
                            newobjOrderItemLocation.OrderItem_Index = .OrderItem_Index

                            newobjOrderItemLocation.Sku_Index = .Sku_Index
                            newobjOrderItemLocation.Package_Index = .Package_Index
                            newobjOrderItemLocation.Lot_No = .Lot_No
                            newobjOrderItemLocation.PLot = .PLot

                            newobjOrderItemLocation.ItemStatus_Index = .ItemStatus_Index
                            newobjOrderItemLocation.Serial_No = .Serial_No

                            ' *** Define value from datagridview ***
                            ' *** Location Index  ***
                            newobjOrderItemLocation.Location_Index = .Location_Index
                            newobjOrderItemLocation.PalletType_Index = .PalletType_Index

                            newobjOrderItemLocation.Tag_No = objItemPerQty.TAG_No



                            newobjOrderItemLocation.Ratio = drOrderItem("Ratio").ToString

                            If newobjOrderItemLocation.Pallet_Qty <> 0 Then
                                newobjOrderItemLocation.Pallet_Qty = 0
                            Else
                                newobjOrderItemLocation.Pallet_Qty = 0
                            End If

                            newobjOrderItemLocation.Qty = 1
                            newobjOrderItemLocation.Total_Qty = newobjOrderItemLocation.Qty * .Ratio
                            newobjOrderItemLocation.Weight = objItemPerQty.Weight
                            newobjOrderItemLocation.Volume = objItemPerQty.Volume

                            If .MixPallet <> 0 Then
                                newobjOrderItemLocation.MixPallet = 0
                            Else
                                newobjOrderItemLocation.MixPallet = 0
                            End If

                            newobjOrderItemLocation.Qty_Item = drOrderItem("Item_Qty").ToString

                            newobjOrderItemLocation.OrderItem_Price = drOrderItem("OrderItem_Price").ToString


                            strOrderItemLocation_Index = objDBTempIndex.getSys_Value("OrderItemLocation_Index")
                            newobjOrderItemLocation.OrderItemLocation_Index = strOrderItemLocation_Index
                            objCollection.Add(newobjOrderItemLocation)


                            objItemPerQty.OrderItemLocation_Index = strOrderItemLocation_Index
                            objItemCollection.Add(objItemPerQty)
                        Next
                End Select

                objDBTempIndex = Nothing
                objAutoNumber = Nothing

                Dim objItemA As New tb_TAG(tb_TAG.enuOperation_Type.ADDNEW)

                objItemA.objItemCollection = objItemCollection

                Select Case Me._DEFAULT_AUTO_TAG
                    Case conenmGenTag_Type.NotGen
                    Case Else
                        objItemA.InsertData()
                End Select

                ' ''------------------------------------------------------
                ''strOrderItemLocation_Index = objDBTempIndex.getSys_Value("OrderItemLocation_Index")
                ''.OrderItemLocation_Index = strOrderItemLocation_Index
                ''objCollection.Add(objOrderItemLocation)
            End With




            ' *** Check Balance ***
            If Not Recieve_Qty = sumQty Then
                MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Exit Sub
            End If

            ' *** Save Data ***
            Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
            objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
            objJobLocation = Nothing

        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    Function setObjJobOrder(ByVal drOrderItem As DataRow) As Object
        Try
            Dim objJobOrder As New tb_JobOrder
            Dim objDBTempIndex As New Sy_AutoNumber

            objJobOrder.JobOrder_Index = objDBTempIndex.getSys_Value("JobOrder_Index")
            objJobOrder.JobOrder_Date = drOrderItem("Order_Date").ToString
            objJobOrder.JobOrder_No = drOrderItem("Order_No").ToString

            ' *************************************
            ' Current System Use tb_Order with tb_JobOrder by 1:1  
            '  Value of in  tb_JobOrder.JobOrder_Index field  >> tb_JobOrder.JobOrder_Index =tb_Order.Order_Index 
            objJobOrder.Order_Index = drOrderItem("Order_index").ToString
            ' *************************************

            objJobOrder.Str1 = drOrderItem("Ref_No1").ToString
            objJobOrder.Str2 = drOrderItem("Ref_No2").ToString
            objJobOrder.Str3 = drOrderItem("Ref_No3").ToString
            objJobOrder.Str4 = drOrderItem("Ref_No4").ToString
            objJobOrder.Str5 = drOrderItem("Ref_No5").ToString

            Return objJobOrder
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SetTagItem(ByVal poTagItem As tb_TAG, ByVal drOrderItem As DataRow) As tb_TAG
        Try

            poTagItem.Order_No = drOrderItem("Order_No").ToString
            poTagItem.Order_Index = drOrderItem("Order_index").ToString
            poTagItem.Order_Date = CDate(drOrderItem("Order_Date").ToString).ToString("yyyy/MM/dd")
            poTagItem.Order_Time = ""
            poTagItem.Customer_Index = drOrderItem("Customer_Index").ToString
            poTagItem.Supplier_Index = drOrderItem("Supplier_Index").ToString
            poTagItem.OrderItem_Index = drOrderItem("OrderItem_Index").ToString
            poTagItem.OrderItemLocation_Index = ""
            poTagItem.Sku_Index = drOrderItem("Sku_Index").ToString
            poTagItem.PLot = drOrderItem("PLot").ToString
            poTagItem.ItemStatus_Index = drOrderItem("ItemStatus_Index").ToString
            poTagItem.Package_Index = drOrderItem("Package_Index").ToString
            poTagItem.Unit_Weight = 0
            poTagItem.Size_Index = -1
            poTagItem.Pallet_No = drOrderItem("str5").ToString
            poTagItem.TAG_Status = 1
            poTagItem.Ref_No1 = drOrderItem("Ref_No1").ToString
            poTagItem.Ref_No2 = drOrderItem("Ref_No1").ToString

            poTagItem.Qty = CDbl(drOrderItem("Qty").ToString)
            poTagItem.Weight = CDbl(drOrderItem("Weight").ToString)
            poTagItem.Volume = CDbl(drOrderItem("Volume").ToString)


            poTagItem.Qty_per_TAG = poTagItem.Qty
            poTagItem.Weight_per_TAG = poTagItem.Weight
            poTagItem.Volume_per_TAG = poTagItem.Volume

            poTagItem.Ref_No3 = "1/1"
            Return poTagItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


    Private Sub txtKeySearch_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtKeySearch.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Me.getOrderview()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnAssignJob_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAssignJob.Click
        Try
            If grdOrderView.Rows.Count < 1 Then
                Exit Sub
            End If

            If Me._USE_ASSIGNEJOB_ITEM Then
                If grdOrderView.CurrentRow.Cells("Status").Tag <> "2" Or grdOrderView.CurrentRow.Cells("Status").Tag <> "-1" Then
                    Dim f As New frmAssignJobItem
                    f.Process_ID = 1
                    f.DocumentPlan_No = Me.grdOrderView.CurrentRow.Cells("Document_No").Value
                    f.ShowDialog()
                End If
            Else
                If grdOrderView.CurrentRow.Cells("Status").Tag <> "2" Or grdOrderView.CurrentRow.Cells("Status").Tag <> "-1" Then
                    Dim frm As New WMS_STD_INB_Receive.frmPopup_AssignJob
                    frm.Process_ID = 1
                    frm.DocumentPlan_Index = Me.grdOrderView.CurrentRow.Cells("System_Index").Value
                    frm.DocumentPlan_No = Me.grdOrderView.CurrentRow.Cells("Document_No").Value
                    frm.ShowDialog()
                    Me.getOrderview()
                End If
            End If






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

    Private Sub btnPageFirst_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageFirst.Click
        Try
            Me.txtPageIndex.Text = 1
            Me.getOrderview()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPagePrev_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPagePrev.Click
        Try
            Me.txtPageIndex.Text -= 1
            Me.getOrderview()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPageNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageNext.Click
        Try
            Me.txtPageIndex.Text += 1
            Me.getOrderview()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnPageLast_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPageLast.Click
        Try
            Me.txtPageIndex.Text = Me.txtTotalPage.Text
            Me.getOrderview()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtPageIndex_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPageIndex.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                If IsNumeric(Me.txtPageIndex.Text) Then
                    If CInt(Me.txtPageIndex.Text) > CInt(Me.txtTotalPage.Text) Then
                        Me.txtPageIndex.Text = Me.txtTotalPage.Text
                    End If
                    Me.txtPageIndex.Text = Trim(Me.txtPageIndex.Text)
                    '  Calculate_Paging()
                    Me.getOrderview()
                Else
                    Me.txtPageIndex.Text = 1
                    ' Calculate_Paging()
                    Me.getOrderview()
                End If

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdOrderView_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdOrderView.CellContentClick

    End Sub
End Class