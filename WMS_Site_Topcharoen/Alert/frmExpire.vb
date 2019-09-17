Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master
Public Class frmExpire

    Dim WH_LocationSearch As String = ""
    Dim _blnCondIncludeLocation As Boolean = True

    'Deflaut Exp Date 
    Private _ExpStartDate As String = ""
    Private _ExpEndDate As String = ""
    Dim intCountVisible As Integer = 0

#Region " FORM LOAD "
    Private Sub frmWMSAlert_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language

            'Insert
            'oFunction.Insert(Me)
            'oFunction.Insert_config_DataGridColumn(Me, Me.grdSKUResult)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me)
            oFunction.SW_Language_Column(Me, Me.grdSKUResult)
            '---addnew dong ---


            getConfig_Value()
            SetDate()

            Me.getCustomer()
            Me.getWarehouse()
            Me.getRoom()
            Me.getProductType()

            '--- show --- Default
            Me.getMinOrder()
            Me.getMaxOrder()


            If intCountVisible = 3 Then
                Exit Sub
            End If
            '------------------
            'CheckExpiredItem()
        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
    End Sub

    Private Sub getConfig_Value()
        Try
            Dim oconfig_CustomSetting As New WMS_STD_Formula.config_CustomSetting

            If oconfig_CustomSetting.getConfig_Key_USE("USE_ALERT_EXPIRED") = False Then
                tabExpiredItem.Dispose()
                intCountVisible += 1
            End If
            If oconfig_CustomSetting.getConfig_Key_USE("USE_ALERT_MAX") = False Then
                tabMax.Dispose()
                intCountVisible += 1
            End If
            If oconfig_CustomSetting.getConfig_Key_USE("USE_ALERT_MIN") = False Then
                tabMin.Dispose()
                intCountVisible += 1
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub SetDate()
        Try
            Dim oconfig_CustomSetting As New config_CustomSetting
            With oconfig_CustomSetting
                Me.txtStartDateExp.Text = .GetValue_Config("EXPIRE_DATE_START")
                Me.txtEndDateExp.Text = .GetValue_Config("EXPIRE_DATE_END")
            End With
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub frmExpire_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        Me.getExpiredItemAlert(0)
    End Sub
#End Region

#Region " INITIAL CONTROL"

    Private Sub getCustomer()

        Dim objClassDB As New ms_Customer(ms_Customer.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getCus()
            objDT = objClassDB.DataTable

            Dim str(2) As String
            str(0) = "-11"
            str(1) = " "
            str(2) = "-- แสดงทุกรายการ --"
            objDT.Rows.Add(str)

            With Me.cboCustomer
                .DisplayMember = "Customer_Name"
                .ValueMember = "Customer_Index"
                .DataSource = objDT
            End With
            cboCustomer.SelectedIndex = cboCustomer.Items.Count - 1

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
            objClassDB.getProductType()
            objDT = objClassDB.DataTable

            Dim str(2) As String
            str(0) = "-11"
            str(1) = " "
            str(2) = "-- แสดงทุกรายการ --"
            objDT.Rows.Add(str)

            With Me.cboProductType
                .DisplayMember = "Description"
                .ValueMember = "ProductType_Index"
                .DataSource = objDT
            End With
            cboProductType.SelectedIndex = cboProductType.Items.Count - 1

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getWarehouse()
        Dim objClassDB As New ms_Warehouse(ms_Warehouse.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim strSQL_Where As String = ""
        Dim strWH_Index As String = ""

        Try

            objClassDB.SearchData_Click("", strSQL_Where)
            objDT = objClassDB.DataTable

            If strWH_Index = "ALL" Then
                objDT.Rows.Add("-11", 0, 0, 0, "-- แสดงทุกรายการ --")
            End If

            cboStore.DisplayMember = "Description"
            cboStore.ValueMember = "Warehouse_Index"
            cboStore.DataSource = objDT
            cboStore.SelectedIndex = cboStore.Items.Count - 1



        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub getRoom()
        Dim objClassDB As New ms_Room(ms_Room.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SelectByWareHouse(cboStore.SelectedValue.ToString)
            objDT = objClassDB.DataTable

            Dim str(7) As String
            str(0) = "-11"
            str(5) = "--แสดงทุกรายการ --"
            objDT.Rows.Add(str)

            cboRoom.DisplayMember = "Description"
            cboRoom.ValueMember = "Room_Index"
            cboRoom.DataSource = objDT

            cboRoom.SelectedIndex = cboRoom.Items.Count - 1
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

#End Region

#Region " BUTTON EVENTS "

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Me.txtStartDateExp.Enabled = False
        Me.txtEndDateExp.Enabled = False

        'If rdbTrue.Checked = True Then
        txtStartDateExp.Enabled = True
        txtEndDateExp.Enabled = True
        Me.getExpiredItemAlert(1)
        Me.getMaxOrder()
        Me.getMinOrder()
        'Else
        'txtStartDateExp.Enabled = False
        'txtEndDateExp.Enabled = False
        'Me.getHalfLifeAlert()
        'End If
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintExpiredItem.Click
        Try

_blnCondIncludeLocation = True
        Dim oconfig_Report As New WMS_STD_Master.config_Report

        If grdSKUResult.Rows.Count <= 0 Then
            W_MSG_Information("ไม่สามารถดูรายงานได้ เนื่องจากไม่พบข้อมูลที่ค้นหา")
            Exit Sub
        End If

            'If rdbTrue.Checked = True Then
            Dim frmReport As New frmReportViewerMain
            'frmReport.Report_Name = "rptExpiredProductSummary"
            'frmReport.ParametercbCustomer = cboCustomer.Text
            'frmReport.ParameterNumLocation = txtTotalLoc.Text
            'frmReport.parameterWarehouse = cboStore.Text
            'frmReport.paremeterRoom = cboRoom.Text
            'frmReport.parameterAge = txtStartDateExp.Text
            'frmReport.parameterAgeEng = txtEndDateExp.Text
            'frmReport.Document_Index = 
            'Set_strSQL_ForNormalExpired(1)

            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oconfig_Report.GetReportInfo_ReportMaster("rptExpiredProductSummary", getExpiredItem)
            cry.SetParameterValue("Ccustomer", cboCustomer.Text)
            'cry.SetParameterValue("NumLocation", txtTotalLoc.Text)
            cry.SetParameterValue("Warehouse", cboStore.Text)
            cry.SetParameterValue("Room", cboRoom.Text)
            cry.SetParameterValue("Age", txtStartDateExp.Text)

            frmReport.CrystalReportViewer1.ReportSource = cry
            frmReport.ShowDialog()
            'Else
            '    ' TODO: 
            '    '   Dong_kk : ไม่มี Report
            '    Dim frmReport As New frmReportViewerMain
            '    frmReport.Report_Name = "rpt"
            '    frmReport.ParametercbCustomer = cboCustomer.Text
            '    frmReport.ParameterNumLocation = txtTotalLoc.Text
            '    frmReport.parameterWarehouse = cboStore.Text
            '    frmReport.paremeterRoom = cboRoom.Text
            '    frmReport.Document_Index = Set_strSQL_ForHalfLife()

            '    frmReport.ShowDialog()
            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try
        '_blnCondIncludeLocation = True
        'Dim oconfig_Report As New WMS_STD_Master.config_Report

        'If grdSKUResult.Rows.Count <= 0 Then
        '    W_MSG_Information("ไม่สามารถดูรายงานได้ เนื่องจากไม่พบข้อมูลที่ค้นหา")
        '    Exit Sub
        'End If

        'If rdbTrue.Checked = True Then
        '    Dim frmReport As New frmReportViewerMain
        '    'frmReport.Report_Name = "rptExpiredProductSummary"
        '    'frmReport.ParametercbCustomer = cboCustomer.Text
        '    'frmReport.ParameterNumLocation = txtTotalLoc.Text
        '    'frmReport.parameterWarehouse = cboStore.Text
        '    'frmReport.paremeterRoom = cboRoom.Text
        '    'frmReport.parameterAge = txtStartDateExp.Text
        '    'frmReport.parameterAgeEng = txtEndDateExp.Text
        '    'frmReport.Document_Index = 
        '    'Set_strSQL_ForNormalExpired(1)

        '    Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
        '    cry = oconfig_Report.GetReportInfo_ReportMaster("rptExpiredProductSummary", getExpiredItem)
        '    cry.SetParameterValue("Ccustomer", cboCustomer.Text)
        '    cry.SetParameterValue("NumLocation", txtTotalLoc.Text)
        '    cry.SetParameterValue("Warehouse", cboStore.Text)
        '    cry.SetParameterValue("Room", cboRoom.Text)
        '    cry.SetParameterValue("Age", txtStartDateExp.Text)

        '    frmReport.CrystalReportViewer1.ReportSource = cry
        '    frmReport.ShowDialog()
        '    'Else
        '    '    ' TODO: 
        '    '    '   Dong_kk : ไม่มี Report
        '    '    Dim frmReport As New frmReportViewerMain
        '    '    frmReport.Report_Name = "rpt"
        '    '    frmReport.ParametercbCustomer = cboCustomer.Text
        '    '    frmReport.ParameterNumLocation = txtTotalLoc.Text
        '    '    frmReport.parameterWarehouse = cboStore.Text
        '    '    frmReport.paremeterRoom = cboRoom.Text
        '    '    frmReport.Document_Index = Set_strSQL_ForHalfLife()

        '    '    frmReport.ShowDialog()
        'End If
    End Sub

    Private Sub btnPrintMax_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintMax.Click
        Try
            _blnCondIncludeLocation = False

            If grdMaxOrder.Rows.Count <= 0 Then
                W_MSG_Information("ไม่สามารถดูรายงานได้ เนื่องจากไม่พบข้อมูลที่ค้นหา")
                Exit Sub
            End If

            'Dim frm As New frmReportViewerMain
            'frm.Report_Name = "rptMaxOrder"
            'frm.Document_Index = Set_strSQL_MaxOrder()
            'frm.ShowDialog()

            Dim oconfig_Report As New WMS_STD_Master.config_Report
            Dim frm As New frmReportViewerMain
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oconfig_Report.GetReportInfo_ReportMaster("rptMaxOrder", getMaxOrder_Datatable)
            frm.CrystalReportViewer1.ReportSource = cry
            frm.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnPrintMin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintMin.Click
        Try
            _blnCondIncludeLocation = False

            If grdMinOrder.Rows.Count <= 0 Then
                W_MSG_Information("ไม่สามารถดูรายงานได้ เนื่องจากไม่พบข้อมูลที่ค้นหา")
                Exit Sub
            End If
            Dim oconfig_Report As New WMS_STD_Master.config_Report
            Dim frm As New frmReportViewerMain
            Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            cry = oconfig_Report.GetReportInfo_ReportMaster("rptMinOrder", getMinOrder_Datatable)
            frm.CrystalReportViewer1.ReportSource = cry
            frm.ShowDialog()


            'Dim cry As New CrystalDecisions.CrystalReports.Engine.ReportDocument
            'cry = oconfig_Report.GetReportInfo_ReportMaster("rptExpiredProductSummary", "", getExpiredItem)
            'cry.SetParameterValue("Ccustomer", cboCustomer.Text)
            'cry.SetParameterValue("NumLocation", txtTotalLoc.Text)
            'cry.SetParameterValue("Warehouse", cboStore.Text)
            'cry.SetParameterValue("Room", cboRoom.Text)
            'cry.SetParameterValue("Age", txtStartDateExp.Text)

            'frmReport.CrystalReportViewer1.ReportSource = cry
            'frmReport.ShowDialog()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnVR_WH_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnVR_WH.Click

        If grdSKUResult.Rows.Count <= 0 Then
            If MessageBox.Show("ไม่สามารถดูคลังได้ เนื่องจากไม่พบข้อมูลที่ค้นหา", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning) = Windows.Forms.DialogResult.OK Then Exit Sub
        End If

        Dim strSQL As New SQLCommands
        Dim SQLcommand As String = "select distinct ms_location.wareHouse_index,ms_Warehouse.Description as WH_Des,ms_location.room,ms_Room.Description as Room_Des from ms_location "
        SQLcommand &= " INNER JOIN ms_Warehouse ON ms_location.wareHouse_index = ms_Warehouse.Warehouse_Index "
        SQLcommand &= " INNER JOIN ms_Room ON ms_location.room= ms_Room.Room_Index"
        SQLcommand &= " where ms_location.Location_Index in (" & WH_LocationSearch & ")"

        strSQL.SQLComand(SQLcommand)
        If strSQL.DataTable.Rows.Count <= 0 Then Exit Sub

        'Dim frm As New frmVRWH_PutAway

        ''frm.cbStore.DisplayMember = "WH_Des"
        ''frm.cbStore.ValueMember = "wareHouse_index"
        ''frm.cbStore.DataSource = strSQL.DataTable

        ''frm.cbRoom.DisplayMember = "Room_Des"
        ''frm.cbRoom.ValueMember = "room"
        ''frm.cbRoom.DataSource = strSQL.DataTable


        'If strSQL.DataTable.Rows.Count = 1 Then

        'Else

        'End If

        'frm.Location_Index = WH_LocationSearch

        'frm.ShowDialog()

    End Sub
#End Region

#Region " TAB / RADIO EVENTS "

    Private Sub cbStore_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboStore.SelectedIndexChanged
        Me.getRoom()
    End Sub

    Private Sub rdbTrue_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdbTrue.CheckedChanged
        If rdbTrue.Checked = True Then
            txtStartDateExp.Enabled = True
            txtEndDateExp.Enabled = True
        Else
            txtStartDateExp.Enabled = False
            txtEndDateExp.Enabled = False
        End If
    End Sub

    Private Sub TabResult_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabResult.Click
        Try
            Dim intTabIndex As Integer = TabResult.SelectedIndex
            If intTabIndex = 1 Then
                txtWeight.Enabled = False
                lblweight.Enabled = False
                Me.getMinOrder()
            ElseIf intTabIndex = 2 Then
                txtWeight.Enabled = False
                lblweight.Enabled = False
                Me.getMaxOrder()
            Else
                txtWeight.Enabled = True
                lblweight.Enabled = True
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdSKU_Package_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSKUResult.CellValueChanged
        Dim grdSKU_Package As DataGridView = CType(sender, DataGridView)
        Dim ProductSum As String = ""
        ProductSum = grdSKU_Package.Rows.Count
        txtTotalLoc.Text = ProductSum

        Select Case grdSKU_Package.Columns(e.ColumnIndex).Name
            Case "ColQty"
                Dim ProductSum1 As Double = 0
                For i As Integer = 0 To grdSKU_Package.Rows.Count - 1
                    Dim tempSumQty As String = grdSKU_Package.Rows(i).Cells("ColQty").Value

                    If IsNumeric(tempSumQty) Then
                        ProductSum1 += CDbl(tempSumQty)
                    End If
                Next
                txtQty.Text = ProductSum1

            Case "ColWeight"
                Dim ProductSum2 As Double = 0
                For i As Integer = 0 To grdSKU_Package.Rows.Count - 1
                    Dim tempSumQty As String = grdSKU_Package.Rows(i).Cells("ColWeight").Value

                    If IsNumeric(tempSumQty) Then
                        ProductSum2 += CDbl(tempSumQty)
                    End If
                Next
                txtWeight.Text = ProductSum2
        End Select

    End Sub

#End Region

#Region " GENERIC FUNCTIONS AND SUBS / SQL STATEMENTS "

    Private Sub FillExpiredItemGridView(ByRef strSQL As String)

        If TabResult.TabPages("tabExpiredItem") Is Nothing Then
            Exit Sub
        End If
        Dim i As Integer
        Dim objDb As New SQLCommands
        Dim objDT As New DataTable
        objDb.SQLComand(strSQL)
        objDT = objDb.DataTable

        Me.grdSKUResult.Rows.Clear()
        Me.grdSKUResult.Refresh()

        '--- จำนวนตำแหน่ง --
        Dim intLocCount As Integer = 0
        intLocCount = grdSKUResult.Rows.Count
        txtTotalLoc.Text = intLocCount.ToString

        '----จำนวนสินค้า---
        Dim dblSumQty As Double = 0
        For i = 0 To grdSKUResult.Rows.Count - 1
            Dim tempSumQty As String = grdSKUResult.Rows(i).Cells("ColQty").Value

            If IsNumeric(tempSumQty) Then
                dblSumQty += CDbl(tempSumQty)
            End If
        Next
        txtQty.Text = dblSumQty

        '---น้ำหนักสินค้า
        Dim dblSumWeight As Double = 0
        For i = 0 To grdSKUResult.Rows.Count - 1
            Dim tempSumQty As String = grdSKUResult.Rows(i).Cells("ColWeight").Value

            If IsNumeric(tempSumQty) Then
                dblSumWeight += CDbl(tempSumQty)
            End If
        Next
        txtWeight.Text = dblSumWeight
        '----
        If objDT.Rows.Count = 0 Then
            MessageBox.Show("ไม่พบรายการที่ค้นหา", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If

        For i = 0 To objDT.Rows.Count - 1
            With Me.grdSKUResult
                Me.grdSKUResult.Rows.Add()
                .Rows(i).Cells("LocationBalance_Index").Value = objDT.Rows(i).Item("LocationBalance_Index").ToString
                .Rows(i).Cells("Tag_No").Value = objDT.Rows(i).Item("Tag_No").ToString
                .Rows(i).Cells("ColSku").Value = objDT.Rows(i).Item("Sku_Id").ToString
                .Rows(i).Cells("ColProName").Value = objDT.Rows(i).Item("SKU_Name_th").ToString
                .Rows(i).Cells("ColCus").Value = objDT.Rows(i).Item("Customer_Name").ToString
                .Rows(i).Cells("ColExpDate").Value = Format(CDate(objDT.Rows(i).Item("Exp_Date")), "dd/MM/yyyy").ToString
                .Rows(i).Cells("ColLocation").Value = objDT.Rows(i).Item("Location_Alias").ToString
                .Rows(i).Cells("ColQty").Value = objDT.Rows(i).Item("Qty_Bal").ToString

                .Rows(i).Cells("ColStatus").Value = objDT.Rows(i).Item("ItemStatus_Des").ToString
                .Rows(i).Cells("ColAgeSum").Value = objDT.Rows(i).Item("AgeRemain").ToString
                .Rows(i).Cells("ColWeight").Value = objDT.Rows(i).Item("Weight_Bal").ToString

                .Rows(i).Cells("Col_JobNo").Value = objDT.Rows(i).Item("Ref_No1").ToString
                .Rows(i).Cells("Col_MAWB").Value = objDT.Rows(i).Item("Ref_No2").ToString
                .Rows(i).Cells("Col_HAWB").Value = objDT.Rows(i).Item("Ref_No3").ToString
            End With
        Next
    End Sub

    Private Function BuildSQLConditionFromCombo() As String
        Dim strWhere As String = ""

        If Me.cboCustomer.SelectedValue <> "-11" Then
            strWhere &= " AND (Customer_Index ='" & Me.cboCustomer.SelectedValue & "') "
        End If
        If Me.cboProductType.SelectedValue <> "-11" Then
            strWhere &= " AND (ProductType_Index ='" & Me.cboProductType.SelectedValue & "') "
        End If
        If Me.cboRoom.SelectedValue <> "-11" And _blnCondIncludeLocation Then
            strWhere &= " AND (Room_Index ='" & Me.cboRoom.SelectedValue & "') "
        End If
        If Me.cboStore.SelectedValue <> "-11" And _blnCondIncludeLocation Then
            strWhere &= " AND (Warehouse_Index ='" & Me.cboStore.SelectedValue & "') "
        End If
        Return strWhere

    End Function

    Private Sub getExpiredItemAlert(ByVal LoadType As Integer)

        Dim strSql As String = ""
        _blnCondIncludeLocation = True
        strSql = Set_strSQL_ForNormalExpired(LoadType)
        strSql &= " ORDER BY AgeRemain ASC, Location_Alias ASC, SKU_Id ASC"

        WH_LocationSearch = "SELECT DISTINCT Location_Index FROM VIEW_StockAgeSummary_ByTag "
        WH_LocationSearch &= "WHERE (Qty_Bal > 0) "
        If rdbFalse.Checked Then
            WH_LocationSearch &= " AND (AgeRemain BETWEEN '" & Me.txtStartDateExp.Text & "' AND '" & Me.txtEndDateExp.Text & "') "
        Else
            WH_LocationSearch &= " AND (AgeRemain <= 0) "
        End If

        WH_LocationSearch &= BuildSQLConditionFromCombo()

        Call FillExpiredItemGridView(strSql)
    End Sub

    Private Sub getHalfLifeAlert()

        Dim strSql As String = ""
        _blnCondIncludeLocation = True
        strSql = Set_strSQL_ForHalfLife()
        strSql &= " ORDER BY AgeRemain ASC, Location_Alias ASC, SKU_Id ASC"

        WH_LocationSearch = "SELECT DISTINCT Location_Index FROM VIEW_StockAgeSummary_ByTag "
        WH_LocationSearch &= "WHERE (Qty_Bal > 0) AND (AgeHalfLife >= AgeRemain) "
        WH_LocationSearch &= BuildSQLConditionFromCombo()

        Call FillExpiredItemGridView(strSql)
    End Sub

    Private Function Set_strSQL_ForNormalExpired(ByVal intLoadType As Integer) As String

        Dim strSQL As String = ""

        strSQL = "SELECT * FROM VIEW_StockAgeSummary_ByTag WHERE (Qty_Bal > 0) "
        If rdbFalse.Checked Then
            strSQL &= " AND (AgeRemain <=0 ) "
        Else
            strSQL &= " AND (AgeRemain BETWEEN '" & Me.txtStartDateExp.Text & "' AND '" & Me.txtEndDateExp.Text & "') "
        End If

        If intLoadType = 1 Then
            strSQL &= BuildSQLConditionFromCombo()
        End If

        Return strSQL

    End Function

    Private Function Set_strSQL_ForHalfLife() As String

        Dim strSQL As String = ""

        strSQL = "SELECT * FROM VIEW_StockAgeSummary_ByTag "
        strSQL &= "WHERE (Qty_Bal > 0) AND (AgeHalfLife >= AgeRemain) "
        strSQL &= BuildSQLConditionFromCombo()

        Return strSQL
    End Function

    Private Sub getMinOrder()

        Dim strSql As String = ""
        Dim dblSumQty As Double = 0
        Dim i As Integer

        Try
            strSql = Set_strSQL_MinOrder()
            Dim objDb As New SQLCommands
            Dim objDT As New DataTable
            objDb.SQLComand(strSql)
            grdMinOrder.DataSource = objDb.DataTable
            txtMinOrderCount.Text = grdMinOrder.Rows.Count

            '----จำนวนสินค้า---
            For i = 0 To grdMinOrder.Rows.Count - 1
                Dim tempSumQty As String = grdMinOrder.Rows(i).Cells("ColBal").Value

                If IsNumeric(tempSumQty) Then
                    dblSumQty += CDbl(tempSumQty)
                End If
            Next
            txtQty.Text = dblSumQty

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getMaxOrder()

        Dim strSql As String = ""
        Dim dblSumQty As Double = 0
        Dim i As Integer

        Try
            strSql = Set_strSQL_MaxOrder()

            Dim objDb As New SQLCommands
            Dim objDT As New DataTable
            objDb.SQLComand(strSql)
            grdMaxOrder.DataSource = objDb.DataTable
            txtMaxOrderCount.Text = grdMaxOrder.Rows.Count

            '----จำนวนสินค้า---
            For i = 0 To grdMaxOrder.Rows.Count - 1
                Dim tempSumQty As String = grdMaxOrder.Rows(i).Cells("bal").Value

                If IsNumeric(tempSumQty) Then
                    dblSumQty += CDbl(tempSumQty)
                End If
            Next
            txtQty.Text = dblSumQty

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Function Set_strSQL_MinOrder() As String

        Dim strSQL As String = ""

        _blnCondIncludeLocation = False

        strSQL = "SELECT Sku_Id, Product_Name_th, package, Min_Qty, Sum_Qty_Bal, PercentQty_BelowMin "
        strSQL &= "FROM VIEW_RPT_ProductSafetyStock "
        strSQL &= "WHERE (Sum_Qty_Bal > 0) "
        strSQL &= BuildSQLConditionFromCombo()

        strSQL &= " ORDER BY SKU_Id ASC"
        Return strSQL
    End Function

    Private Function Set_strSQL_MaxOrder() As String

        Dim strSQL As String = ""

        _blnCondIncludeLocation = False

        strSQL = "SELECT Sku_Id, Product_Name_th, package, Max_Qty, Sum_Qty_Bal, PercentQty_AboveMax "
        strSQL &= "FROM VIEW_RPT_ProductOverStock "
        strSQL &= "WHERE (Sum_Qty_Bal > 0) "
        strSQL &= BuildSQLConditionFromCombo()

        strSQL &= " ORDER BY SKU_Id ASC"
        Return strSQL
    End Function


    Public Function CheckExpiredItem() As Boolean
        Dim objDb As New SQLCommands
        Dim objDT As New DataTable
        Dim strSQL As String

        SetDate()

        strSQL = Set_strSQL_ForNormalExpired(0)

        objDb.SQLComand(strSQL)
        objDT = objDb.DataTable

        If objDT.Rows.Count > 0 Then
            Return True
        Else
            strSQL = Set_strSQL_MinOrder()
            objDb.SQLComand(strSQL)
            objDT = New DataTable
            objDT = objDb.DataTable
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                strSQL = Set_strSQL_MaxOrder()
                objDb.SQLComand(strSQL)
                objDT = New DataTable
                objDT = objDb.DataTable
                If objDT.Rows.Count > 0 Then
                    Return True
                Else : Return False
                End If
            End If
        End If
    End Function

    Private Function getMinOrder_Datatable() As DataTable

        Dim objDb As New SQLCommands
        Dim objDT As New DataTable
        _blnCondIncludeLocation = False
        objDb.SQLComand(Set_strSQL_MinOrder)
        objDT = objDb.DataTable
        Return objDT
    End Function

    Private Function getMaxOrder_Datatable() As DataTable

        Dim objDb As New SQLCommands
        Dim objDT As New DataTable
        'Dim strSQL As String

        _blnCondIncludeLocation = False

        objDb.SQLComand(Set_strSQL_MaxOrder)
        objDT = objDb.DataTable
        Return objDT
    End Function

    Public Function getExpiredItem() As DataTable
        Dim objDb As New SQLCommands
        Dim objDT As New DataTable
        Dim strSQL As String

        SetDate()

        strSQL = Set_strSQL_ForNormalExpired(0)

        objDb.SQLComand(strSQL)
        objDT = objDb.DataTable

        Return objDT

    End Function

#End Region

End Class