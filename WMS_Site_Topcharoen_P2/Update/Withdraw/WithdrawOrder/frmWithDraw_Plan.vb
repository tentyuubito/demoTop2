Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master.W_Function
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_OUTB_Datalayer

Public Class frmWithDraw_Plan


    Private _dataTable As DataTable = New DataTable

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Private _Customer_Index As String = ""

    Public Property Customer_index() As String
        Get
            Return _Customer_Index
        End Get
        Set(ByVal value As String)
            _Customer_Index = value
        End Set
    End Property

    Private _StrSONO As String = ""
    Public Property StrSONO() As String
        Get
            Return _StrSONO
        End Get
        Set(ByVal value As String)
            _StrSONO = value
        End Set
    End Property

    Private _USE_PACKING_NEW_PRODUCTION As Boolean = False

    Public Property USE_PACKING_NEW_PRODUCTION() As Boolean
        Get
            Return _USE_PACKING_NEW_PRODUCTION
        End Get
        Set(ByVal value As Boolean)
            _USE_PACKING_NEW_PRODUCTION = value
        End Set
    End Property


    Private Sub frmWithDraw_Plan_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            grdPlan.AutoGenerateColumns = False
            Dim oFunction As New WMS_STD_Master.W_Language
            'oFunction.Insert(Me)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdPlan)
            oFunction.SW_Language_Column(Me, Me.grdPlan)
            oFunction.SwitchLanguage(Me)
            getProcess_Ref(2)


            'If _StrSONO <> "" Then
            '    Dim objPlan As New View_LocationBalance
            '    objPlan.SearchWithDraw_Plan("SalesOrder", "SALE", _StrSONO, 10)
            '    grdPlan.DataSource = GetRowSelected(objPlan.DataTable)
            '    'Me.ShowgrdSoAuto(_StrSONO)
            'End If


            'LoadRoute()
            'LoadSubRoute("-1")
            col_Document_Index.Visible = False
            col_Process_Id.Visible = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Sub ShowgrdSoAuto(ByVal SalesOrder_No As String)
    '    Dim objPlan As New View_LocationBalance
    '    Try
    '        objPlan.SearchWithDraw_Plan(StrSONO)
    '        grdPlan.DataSource = GetRowSelected(objPlan.DataTable)
    '    Catch ex As Exception
    '        Throw ex
    '    Finally

    '    End Try
    '  End Sub



    Private Sub getProcess_Ref(ByVal Process_Id As Integer)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx


        Dim objClassDB As New ms_DocumentType(ms_DocumentType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProcess_Reference(Process_Id)
            objDT = objClassDB.DataTable

            ' ***** Using add comboboxcolumn *****
            With cboPlanDocument
                Select Case W_Module.WV_Language
                    Case enmLanguage.Thai
                        .DisplayMember = "Description_th"
                    Case enmLanguage.English
                        .DisplayMember = "Description_en"
                End Select

                .ValueMember = "Ref_Process_Id"
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


    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Updated by: Ta - 10 Feb 2010
    ''' Change SearchWithDraw_Plan to use SearchWithDraw_Plan_SO_WithSubRoute for SO.
    ''' </remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Dim objPlan_Location_Bal As New View_LocationBalance
            Dim strDocument_No As String = txtPlanDocument_No.Text
            Select Case cboPlanDocument.SelectedValue
                Case 7
                    objPlan_Location_Bal.SearchWithDraw_Plan(strDocument_No, Me._USE_PACKING_NEW_PRODUCTION, Me._Customer_Index)
                Case 10
                    'objPlan.SearchWithDraw_Plan("SalesOrder", "SALE", strDocument_No, 10) '
                    'objPlan.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 10) '

                    '22/04/2010
                    'objPlan_Location_Bal.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 10, Me.cboRoute.SelectedValue, Me.cboSubRoute.SelectedValue, Me.cboDistributionCenter.SelectedValue) '
                    objPlan_Location_Bal.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 10, Me.cboRoute.SelectedValue, Me.cboSubRoute.SelectedValue, Me.cboDistributionCenter.SelectedValue, Me.Customer_index)
                Case 23
                    objPlan_Location_Bal.SearchWithDraw_Plan_SO_WithSubRoute("TM", strDocument_No, 10, "-11", "-11", "-11", Me.Customer_index)
                Case 25
                    objPlan_Location_Bal.SearchWithDraw_Plan_SO_WithSubRoute("DO", strDocument_No, 25, Me.cboRoute.SelectedValue, Me.cboSubRoute.SelectedValue, Me.cboDistributionCenter.SelectedValue) '
                Case 30
                    'objPlan_Location_Bal.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 30, "-11", "-11", "-11") '
                    objPlan_Location_Bal.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 30, "-11", "-11", "-11", Me.Customer_index)
            End Select

            grdPlan.DataSource = Nothing
            Dim dt_plan As New DataTable
            Dim dt_withdraw_real As New DataTable
            dt_plan = GetRowSelected(objPlan_Location_Bal.DataTable)
            If dt_plan.Rows.Count = 0 Then
                Exit Sub
            End If
            'check colum ว่ามีหรือไม่
            If dt_plan.Columns.Contains("Qty_Withdraw_Real") = False Then
                dt_plan.Columns.Add("Qty_Withdraw_Real")
            End If
            If dt_plan.Columns.Contains("Qty_Withdraw_Remain") = False Then
                dt_plan.Columns.Add("Qty_Withdraw_Remain")
            End If
            'loop หาค่าจำนวนที่เบิกได้
            Dim oLB As New View_LocationBalance
            Dim dtLB As New DataTable
            For Each dr As DataRow In dt_plan.Rows
                oLB.Fn_WithDraw_Real(dr("Document_Index"))
                dtLB = oLB.DataTable
                dr("Qty_Withdraw_Real") = dtLB.Rows(0)("Min_Bal_Per_Pck").ToString 'จำนวนที่เบิกได้ 'CInt(dr("Qty_Bal").ToString) 
                dr("Qty_Withdraw_Remain") = CInt(dr("Qty_Bal").ToString) 'ค้างเบิก
                dr("Qty_Bal") = CInt(dr("Qty_Bal").ToString) 'ยอดเบิก
            Next

            dt_plan.AcceptChanges()
            grdPlan.DataSource = GetRowSelected(dt_plan)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Function GetRowSelected(ByVal podtData As DataTable) As DataTable
        'Keep Selected Product
        Try
            'If podtData IsNot Nothing Then
            '    If Not podtData.Columns.Contains("Qty_Bal") Then
            '        podtData.Columns.Add("Qty_Bal", GetType(Double))
            '    End If
            'End If
            If Me.grdPlan.DataSource IsNot Nothing Then
                Dim odtTemp As New DataTable
                Dim odrTempSelected() As DataRow    '--- Array Datarow
                Dim odrDuplicate() As DataRow       '--- Array Datarow
                odtTemp = grdPlan.DataSource

                Dim odtTemp2 As New DataTable
                odtTemp2 = podtData.Clone
                odtTemp.Merge(odtTemp2)

                odrTempSelected = odtTemp.Select("chkSelect = 1")


                'Delete Duplicate LocationBalance
                podtData.Merge(odtTemp)
                For Each odrSelected As DataRow In odrTempSelected
                    odrDuplicate = podtData.Select("Document_Index = '" & odrSelected("Document_Index").ToString & "' and Process_Id = '" & odrSelected("Process_Id").ToString & "'")
                    If odrDuplicate.Length > 0 Then
                        podtData.Rows.Remove(odrDuplicate(0))
                    End If
                Next


                'Add Row Selected
                'Dim odtTemp2 As New DataTable
                'odtTemp2 = podtData.Clone

                For Each odrSelected As DataRow In odrTempSelected
                    Dim odrData As DataRow
                    odrData = podtData.NewRow
                    odrData.ItemArray = odrSelected.ItemArray.Clone
                    If podtData.Rows.Count > 0 Then
                        podtData.Rows.InsertAt(odrData, 0)
                    Else
                        podtData.Rows.Add(odrSelected.ItemArray)
                    End If
                Next

            End If
            Return podtData
        Catch ex As Exception
            Throw ex
            Return podtData
        End Try

    End Function

    Private Sub btnSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelect.Click
        Try
            If Me.grdPlan.DataSource Is Nothing Then
                W_MSG_Information_ByIndex(400060)
                Exit Sub
            End If

            Dim dt_packing As New DataTable
            Dim chk_valid As Boolean = False
            Dim strWhere As String = ""
            dt_packing = grdPlan.DataSource

            For Each dr As DataRow In dt_packing.Rows
                Select Case dr("Process_Id")
                    Case 7
                        If CBool(IIf(IsDBNull(dr("chkSelect")), 0, dr("chkSelect"))) = True Then
                            If dr("qty_bal") > dr("qty_withdraw_remain") Or dr("qty_bal") > dr("Qty_Withdraw_Real") Or dr("Qty_Bal") <= 0 Then
                                chk_valid = True
                                Exit For
                            End If
                        End If
                End Select
            Next
            If chk_valid Then
                W_MSG_Information("จำนวนที่ต้องการเบิกไม่ถูกต้อง")
                Exit Sub
            End If

            Me.grdPlan.EndEdit()
            CType(Me.grdPlan.DataSource, DataTable).AcceptChanges()

            Dim odtTemp As New DataTable
            Dim odrTemp() As DataRow

            odtTemp = Me.grdPlan.DataSource
            odrTemp = odtTemp.Select("chkSelect = 1", "chkSelect")


            Me._dataTable = odtTemp.Clone
            If odrTemp.Length = 0 Then
                W_MSG_Information_ByIndex(400060)
                Exit Sub
            End If
            For Each odrSelected As DataRow In odrTemp
                Me._dataTable.Rows.Add(odrSelected.ItemArray)
            Next

            Me.Close()


        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    '22/04/2010
    Private Sub LoadRoute()
        Try
            Dim oms_Route As New ms_Route(ms_SubRoute.enuOperation_Type.SEARCH)
            Dim odt As New DataTable
            oms_Route.GetAllAsDataTable()

            odt = oms_Route.GetDataTable
            Dim odr As DataRow
            odr = odt.NewRow
            odr("Route_Index") = "-11"
            odr("Description") = "ทั้งหมด"
            odt.Rows.InsertAt(odr, 0)

            With Me.cboRoute
                .DisplayMember = "Description"
                .ValueMember = "Route_Index"
                .DataSource = odt
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    '22/04/2010
    Private Sub LoadSubRoute(ByVal pstrRoute_Index As String)
        Try
            Dim oms_SubRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
            Dim odt As New DataTable
            oms_SubRoute.GetAllAsDataTable(pstrRoute_Index)

            odt = oms_SubRoute.GetDataTable
            Dim odr As DataRow
            odr = odt.NewRow
            odr("SubRoute_Index") = "-11"
            odr("Route_Index") = "-11"
            odr("Description") = "ทั้งหมด"
            odt.Rows.InsertAt(odr, 0)

            With Me.cboSubRoute
                .DisplayMember = "Description"
                .ValueMember = "SubRoute_Index"
                .DataSource = odt
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub LoadDistributionCenter()
        Try
            Dim oms_SubRoute As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
            Dim odt As New DataTable
            oms_SubRoute.GetAllAsDataTable("")

            odt = oms_SubRoute.GetDataTable
            Dim odr As DataRow
            odr = odt.NewRow
            odr("DistributionCenter_Index") = "-11"
            odr("Description") = "ทั้งหมด"
            odt.Rows.InsertAt(odr, 0)

            With Me.cboDistributionCenter
                .DisplayMember = "Description"
                .ValueMember = "DistributionCenter_Index"
                .DataSource = odt
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    '22-04-2010
    Private Sub cboRoute_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectionChangeCommitted
        Try
            LoadSubRoute(Me.cboRoute.SelectedValue)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    '22-04-2010
    Private Sub chkAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAll.CheckedChanged
        Try
            If grdPlan.RowCount <= 0 Then Exit Sub

            For i As Integer = 0 To grdPlan.RowCount - 1
                grdPlan.Rows(i).Cells("chk_Plan").Value = chkAll.Checked
            Next

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboPlanDocument_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboPlanDocument.SelectedIndexChanged
        Try
            cboRoute.Visible = False
            lblRoute.Visible = False
            lblSubRoute.Visible = False
            cboSubRoute.Visible = False
            lblDistributionCenter.Visible = False
            cboDistributionCenter.Visible = False
            col_Expected_Delivery_Date.Visible = False
            Col_Postcode.Visible = False
            Col_SubRoute_Desc.Visible = False
            'จำนวนชุด
            col_Qty_Bal.Visible = False
            btnBomDetail.Visible = False

            Select Case cboPlanDocument.SelectedValue
                Case 7
                    'จำนวนชุด
                    col_Qty_Bal.Visible = True
                    btnBomDetail.Visible = True
                Case 10
                    cboRoute.Visible = True
                    lblRoute.Visible = True
                    lblSubRoute.Visible = True
                    cboSubRoute.Visible = True
                    lblDistributionCenter.Visible = True
                    cboDistributionCenter.Visible = True
                    col_Expected_Delivery_Date.Visible = True
                    Col_Postcode.Visible = True
                    Col_SubRoute_Desc.Visible = True

                    LoadRoute()
                    LoadSubRoute("-11")
                    LoadDistributionCenter()
                Case 25
                    cboRoute.Visible = True
                    lblRoute.Visible = True
                    lblSubRoute.Visible = True
                    cboSubRoute.Visible = True
                    lblDistributionCenter.Visible = True
                    cboDistributionCenter.Visible = True
                    col_Expected_Delivery_Date.Visible = True
                    Col_Postcode.Visible = True
                    Col_SubRoute_Desc.Visible = True

                    LoadRoute()
                    LoadSubRoute("-11")
                    LoadDistributionCenter()

            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboRoute_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboRoute.SelectedIndexChanged
        Try
            If cboRoute.SelectedValue Is Nothing Then Exit Sub
            If cboRoute.SelectedValue = "-11" Then Exit Sub
            LoadSubRoute(cboRoute.SelectedValue)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPlanDocument_No_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPlanDocument_No.KeyDown
        Try
            If e.KeyCode = Keys.Enter Then
                Dim objPlan As New View_LocationBalance
                Dim strDocument_No As String = txtPlanDocument_No.Text
                Select Case cboPlanDocument.SelectedValue
                    Case 7

                        objPlan.SearchWithDraw_Plan(strDocument_No, Me._USE_PACKING_NEW_PRODUCTION)
                    Case 10
                        'objPlan.SearchWithDraw_Plan("SalesOrder", "SALE", strDocument_No, 10) '
                        'objPlan.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 10) '

                        '22/04/2010
                        objPlan.SearchWithDraw_Plan_SO_WithSubRoute("SALE", strDocument_No, 10, Me.cboRoute.SelectedValue, Me.cboSubRoute.SelectedValue, Me.cboDistributionCenter.SelectedValue) '

                End Select

                grdPlan.DataSource = GetRowSelected(objPlan.DataTable)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdPlan_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPlan.CellClick
        Try
            '25/01/2011 
            If e.RowIndex <= -1 Then Exit Sub
            If grdPlan.CurrentRow.Index < 0 Then Exit Sub
            Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
                Case "btnBomDetail"
                    'open popup frmBomDetail
                    Dim frm As New WMS_STD_OUTB.frmBomDetail
                    frm.Packing_Index = grdPlan.CurrentRow.Cells("col_Document_Index").Value
                    frm.ShowDialog()

            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

End Class