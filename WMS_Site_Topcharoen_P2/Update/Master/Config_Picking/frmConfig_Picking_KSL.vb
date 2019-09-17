Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Public Class frmConfig_Picking_KSL
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update
    Dim xQuery As New cls_KSL
    Dim xData As New DataTable
    Private _validate As New WMS_STD_Master.ValidateCharacter
    Private _Running_Id As String = ""
    Public Property Running_Id() As String
        Get
            Return _Running_Id
        End Get
        Set(ByVal value As String)
            _Running_Id = value
        End Set
    End Property


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmRouteT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2041)
            oFunction.SW_Language_Column(Me, Me.grd, 2041)


            AddcbCustomerType()
            AddcbDistributionCenter()

            xQuery = New cls_KSL
            xData = New DataTable
            xData = xQuery.getProductType("")
            xData.PrimaryKey = New DataColumn() {xData.Columns("ProductType_Index")}

            'xData.Columns.Add("chkSelect", GetType(Boolean))
            Dim column As DataColumn
            column = New DataColumn
            With column
                .DataType = System.Type.GetType("System.Boolean")
                .DefaultValue = False
                .ColumnName = "chkSelect"
            End With
            xData.Columns.Add(column)


            Me.grd.AutoGenerateColumns = False
            Me.grd.DataSource = xData
            Me.Col_Index.Visible = False

            Select Case SaveType
                Case 0 'Save
                    'Dim objDBIndex As New Sy_AutoNumber
                    'Me.txtID.Text = objDBIndex.getSys_ID("Route_Id")
                    'objDBIndex = Nothing
                    'EnableEvent(False)
                Case 1 'Update
                    xQuery = New cls_KSL
                    xData = New DataTable
                    Dim xCondition As String = String.Format("AND Running_Id = '{0}'", _Running_Id)
                    xData = xQuery.getSearchData_ConfigPicking(xCondition)
                    If xData.Rows.Count > 0 Then
                        Me.txtID.Text = xData.Rows(0)("Running_Id").ToString
                        Me.txtDes.Text = xData.Rows(0)("Description").ToString
                        Me.txtMFG_Day_Count.Text = xData.Rows(0)("MFG_Day_Count").ToString
                        Me.cboCustomerType.SelectedValue = xData.Rows(0)("CustomerType_Index").ToString
                        Me.cboDistributionCenter.SelectedValue = xData.Rows(0)("DistributionCenter_Index").ToString

                        Dim xData2 As New DataTable
                        xData2 = CType(Me.grd.DataSource, DataTable)

                        For Each drRow As DataRow In xData.Rows
                            Dim drNewrow As DataRow
                            drNewrow = xData2.NewRow
                            drNewrow = xData2.Rows.Find(drRow("ProductType_Index").ToString)
                            drNewrow.BeginEdit()
                            drNewrow("chkSelect") = True
                            drNewrow.EndEdit()
                        Next

                        'Set Color
                        For iRow As Integer = 0 To Me.grd.RowCount - 1
                            If Me.grd.Rows(iRow).Cells("chkSelect").Value = True Then
                                Me.grd.Rows(iRow).DefaultCellStyle.BackColor = Color.YellowGreen
                            End If
                            Me.grd.Rows(iRow).Cells("chkSelect").ReadOnly = False
                        Next

                    End If
                    'EnableEvent(True)
            End Select
            Me.txtMFG_Day_Count.Focus()
            'Me.WindowState = FormWindowState.Maximized
            Me.Col_Description.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub AddcbDistributionCenter()
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

    Sub AddcbCustomerType()
        Dim objClassDB As New ms_CustomerType(ms_CustomerType.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable

            cboCustomerType.DataSource = objDT
            cboCustomerType.DisplayMember = "Description"
            cboCustomerType.ValueMember = "CustomerType_Index"

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            Dim xAlert As String = "กรุณาตรวจสอบ : "
            If txtDes.Text.Trim = "" Then
                W_MSG_Error(xAlert & Me.lblDes.Text)
                txtDes.Focus()
                Exit Sub
            End If
            If Not IsNumeric(Me.txtMFG_Day_Count.Text) Then
                W_MSG_Error(xAlert & Me.Label1.Text)
                Me.txtMFG_Day_Count.Focus()
                Exit Sub
            End If
            'If Me.cboDistributionCenter.Text = "N/A" Then
            '    W_MSG_Error(xAlert & Me.lblDistributionCenter.Text)
            '    Me.cboDistributionCenter.Focus()
            '    Exit Sub
            'End If
            If Me.cboCustomerType.Text = "N/A" Then
                W_MSG_Error(xAlert & Me.lblCustomerType.Text)
                Me.cboCustomerType.Focus()
                Exit Sub
            End If



            Dim xData2 As New DataTable
            xData2 = CType(Me.grd.DataSource, DataTable)
            Dim drArrSave() As DataRow = xData2.Select("chkSelect = True")
            If drArrSave.Length = 0 Then
                W_MSG_Error(xAlert & Me.grbProducType.Text)
                Me.grbProducType.Focus()
                Exit Sub
            End If

            xQuery = New cls_KSL
            Me.Running_Id = IIf(Me.Running_Id = "", xQuery.MaxConfigPicking(), Me.Running_Id)
            xQuery.DeleteConfigPicking(Me.Running_Id)
            For Each drRow As DataRow In drArrSave
                xQuery.SaveConfigPicking(Me.Running_Id, Me.cboCustomerType.SelectedValue, Me.cboDistributionCenter.SelectedValue, drRow("ProductType_Index").ToString, Me.txtDes.Text, Me.txtMFG_Day_Count.Text)
            Next
            Me.txtID.Text = Me.Running_Id
            W_MSG_Information_ByIndex("1")
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub




    'Sub ShowgrdRoute()
    '    Dim objRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
    '    Try
    '        objRoute.GetAllAsDataTable(Me._Running_Index)
    '        grdSubRoute.DataSource = objRoute.DataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objRoute = Nothing
    '    End Try
    'End Sub

    'Sub EnableEvent(ByVal enable As Boolean)
    '    btnAddSub.Enabled = enable
    '    btnUpdate.Enabled = enable
    '    btnDelete.Enabled = enable
    'End Sub

    'Private Sub grdSubRoute_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSubRoute.CellDoubleClick
    '    btnUpdate_Click(sender, e)

    'End Sub

    Private Sub frmRoute_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigRoute
                    frm.ShowDialog()
                    Dim oFunction As New WMS_STD_Master.W_Language
                    oFunction.SwitchLanguage(Me, 2041)
                    oFunction.SW_Language_Column(Me, Me.grd, 2041)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            Dim xData2 As New DataTable
            xData2 = CType(Me.grd.DataSource, DataTable)

            For Each drRow As DataRow In xData2.Rows
                drRow("chkSelect") = Me.chkSelectAll.Checked
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub grd_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd.CellClick
    '    Try
    '        Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name.ToUpper
    '            Case "chkSelect".ToUpper
    '                Dim xData2 As New DataTable
    '                CType(Me.grd.DataSource, DataTable).AcceptChanges()
    '                xData2 = CType(Me.grd.DataSource, DataTable)

    '                Dim drNewrow As DataRow
    '                drNewrow = xData2.NewRow
    '                drNewrow = xData2.Rows.Find(xData2.Rows(grd.CurrentRow.Index)("ProductType_Index").ToString)
    '                drNewrow.BeginEdit()
    '                drNewrow("chkSelect") = Not IIf(drNewrow("chkSelect").ToString = "", False, drNewrow("chkSelect"))
    '                drNewrow.EndEdit()

    '                CType(Me.grd.DataSource, DataTable).AcceptChanges()
    '        End Select


    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub
End Class