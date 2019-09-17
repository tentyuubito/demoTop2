Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Imports WMS_STD_Master_Datalayer
Imports CrystalDecisions.CrystalReports.Engine

Public Class frmMainLocation

    Private cry As New ReportDocument
    Private frm As New frmReportViewerMain
    Private Sub frmLocationType_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2028)
            oFunction.SW_Language_Column(Me, Me.grdLocationType, 2028)

            Me.grdLocationType.AutoGenerateColumns = False
            Me.cboSearchType.SelectedIndex = 0
            ShowGrid("", "")

            Me.getReportName(999)

        Catch ex As Exception
            W_MSG_Error(ex.Message)
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

    Private Sub ShowGrid(ByVal condition As String, ByVal key As String)
        Dim objLocation As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            grdLocationType.DataSource = Nothing 'Rows.Clear()
            objLocation.SelectShowData(condition, key)
            objDT = objLocation.DataTable
            objDT.Columns.Add("chk_select", GetType(Integer))
            Me.grdLocationType.AutoGenerateColumns = False
            grdLocationType.Refresh()
            grdLocationType.DataSource = objDT
            lblCount_location.Text = "จำนวนทั้งหมด " & objDT.Rows.Count & " ตำแหน่ง"
            'If objDT.Rows.Count > 0 Then
            '    Dim i As Integer = 0
            '    For Each objDr In objDT.Rows
            '        grdLocationType.Rows.Add()
            '        grdLocationType.Rows(i).Cells("ColAlias").Value = objDr("Location_Alias").ToString
            '        grdLocationType.Rows(i).Cells("ColAlias").Tag = objDr("Location_Index").ToString
            '        grdLocationType.Rows(i).Cells("ColWarehouse").Value = objDr("warehose").ToString
            '        grdLocationType.Rows(i).Cells("ColRoom").Value = objDr("Description").ToString
            '        grdLocationType.Rows(i).Cells("ColLock").Value = objDr("Lock").ToString
            '        grdLocationType.Rows(i).Cells("ColMaxQty").Value = objDr("Max_Qty").ToString
            '        grdLocationType.Rows(i).Cells("ColLocationType").Value = objDr("LocationType").ToString
            '        grdLocationType.Rows(i).Cells("ColMaxWeight").Value = objDr("Max_Weight").ToString
            '        grdLocationType.Rows(i).Cells("ColMaxvolume").Value = objDr("Max_Volume").ToString
            '        i = i + 1
            '    Next

            'Else
            '    grdLocationType.Rows.Clear()
            '    grdLocationType.Refresh()
            'End If
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objLocation = Nothing
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub fn_search()
        Dim objLocationType As New ms_LocationType
        Try
            If Not cboSearchType.Text = "" Then
                Select Case cboSearchType.SelectedIndex
                    Case 0 'all
                        ShowGrid("", "0")
                        'ShowgrdLocationType(objLocationType.GetDataByID(txtSearchKey.Text.ToString()))
                    Case 1
                        ShowGrid(txtSearchKey.Text.Trim, "1")
                        'ShowgrdLocationType(objLocationType.GetDataByDesc(txtSearchKey.Text.ToString()))
                    Case 2
                        ShowGrid(txtSearchKey.Text.Trim, "2")
                        'ShowgrdLocationType(objLocationType.GetDataByDesc(txtSearchKey.Text.ToString()))
                    Case 3
                        ShowGrid(txtSearchKey.Text.Trim, "4") 'ตำแหน่ง
                    Case 4
                        ShowGrid(txtSearchKey.Text.Trim, "5") 'room
                    Case 5
                        ShowGrid(txtSearchKey.Text.Trim, "6") 'Zone
                End Select
            Else
                ShowGrid("", "")
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        Try
            fn_search()
        Catch ex As Exception
            Throw ex
        End Try

        'If Not cboSearchType.Text = "" Then
        '    If cboSearchType.SelectedIndex = 0 Then
        '    ShowGrid(txtSearchKey.Text, "0")
        '    End If
        '    If cboSearchType.SelectedIndex = 1 Then
        '        ShowGrid(txtSearchKey.Text, "1")
        '    End If
        '    If cboSearchType.SelectedIndex = 2 Then
        '        ShowGrid(txtSearchKey.Text, "2")
        '    End If
        'End If
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim frmLocation As New frmLocation
            frmLocation.ShowDialog()
            'Dim objLocation As New frmLocation
            'With objLocation
            '    .appStatus = 0 'add
            '    .ShowDialog()
            '    fn_search()
            'End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub BtnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnUpdate.Click
        Dim objLocation As New frmLocation
        Try
            With objLocation
                .location_index = grdLocationType.CurrentRow.Cells(1).Value.ToString
                .appStatus = 1 'edit
                .ShowDialog()
                fn_search()
                'ShowgrdLocationType()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click

        Dim objLocation As New ms_Location(ms_Size.enuOperation_Type.DELETE)
        Try
            If Me.grdLocationType.DataSource IsNot Nothing AndAlso W_MSG_Confirm("คุณต้องการลบข้อมูล") = Windows.Forms.DialogResult.Yes Then

                ' Update Date : 01/02/2012
                ' Update By : TATA
                ' Check ตำแหน่งจัดเก็บก่อนลบว่ามี Transaction หรือป่าว
                CType(Me.grdLocationType.DataSource, DataTable).AcceptChanges()
                Using dt As DataTable = CType(Me.grdLocationType.DataSource, DataTable).Copy()
                    If dt.Rows.Count = 0 Then Exit Sub
                    For Each dr As DataRow In dt.Select("chk_select = 1")
                        If objLocation.isUseLocation_Index(dr("Location_Index").ToString()) Then
                            W_MSG_Information("ตำแหน่งจัดเก็บ:" & dr("Location_Alias").ToString() & " ขณะนี้ใช้งานอยู่")
                            Exit Sub
                        End If
                    Next
                    For Each dr As DataRow In dt.Select("chk_select = 1")
                        objLocation.deleteLocation_Index(dr("Location_Index").ToString())
                    Next
                    W_MSG_Information("ลบข้อมูลเรียบร้อย")
                    fn_search()
                End Using


                'If objLocation.isUSE_Location(grdLocationType.Rows(grdLocationType.CurrentRow.Index).Cells("ColAlias").Value()) = True Then
                '    W_MSG_Information("ตำแหน่งจัดเก็บ:" & grdLocationType.Rows(grdLocationType.CurrentRow.Index).Cells("ColAlias").Value() & " ขณะนี้ใช้งานอยู่")
                '    Exit Sub
                'Else
                '    objLocation.Delete_Location(grdLocationType.Rows(grdLocationType.CurrentRow.Index).Cells("ColAlias").Value())
                '    W_MSG_Information("ลบข้อมูลเรียบร้อย")
                '    fn_search()
                'End If

                'With objLocation
                '    .Location_Index = grdLocationType.CurrentRow.Cells(0).Value.ToString
                '    .Delete(grdLocationType.CurrentRow.Cells(0).Value.ToString)
                '    fn_search()
                'End With
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub ShowgrdLocationType()
        Dim objLocationType As New ms_LocationType()
        Try
            objLocationType.GetAllAsDataTable()
            grdLocationType.DataSource = objLocationType.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objLocationType = Nothing
        End Try
    End Sub

    Sub ShowgrdLocationType(ByVal pDataTable As DataTable)
        Dim objLocationType As New ms_LocationType()
        Try
            grdLocationType.DataSource = pDataTable
        Catch ex As Exception
            Throw ex
        Finally
            objLocationType = Nothing
        End Try
    End Sub


    Private Sub btn_clear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_clear.Click
        Try
            txtSearchKey.Text = ""
        Catch ex As Exception

        End Try
    End Sub


    Private Sub grdLocationType_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grdLocationType.DoubleClick
        Dim objLocation As New frmLocation
        Try
            With objLocation
                .location_index = grdLocationType.CurrentRow.Cells(1).Value.ToString
                .appStatus = 1 'edit
                .ShowDialog()
                fn_search()
                'ShowgrdLocationType()
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown
        Try
            If e.KeyCode = Windows.Forms.Keys.Enter Then
                Me.fn_search()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdLocationType_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs)
        BtnUpdate_Click(sender, e)
    End Sub

    Private Sub frmMainLocation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigLocation
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2028)
                    oFunction.SW_Language_Column(Me, Me.grdLocationType, 2028)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            If grdLocationType.RowCount > 0 Then
                For iRow As Integer = 0 To grdLocationType.RowCount - 1
                    grdLocationType.Rows(iRow).Cells("col_select").Value = Me.chkSelectAll.Checked
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
        Try

            '   Select Case Me.cboPrint.SelectedValue
            If cboPrint.SelectedValue = "BarcodeLocation" Or cboPrint.SelectedValue = "BarcodeLocation2" Then

                '  Case "BarcodeLocation" Or "BarcodeLocation2"
                Dim strWhere As String = ""
                If grdLocationType.RowCount > 0 Then
                    Dim Dt As DataTable = CType(grdLocationType.DataSource, DataTable)
                    Dt.AcceptChanges()
                    Dim Drr() As DataRow = Dt.Select("chk_select = 1")
                    If Drr.Length <= 0 Then
                        W_MSG_Information("กรุณาเลือกรายการพิมพ์")
                        Exit Sub
                    Else
                        strWhere &= "("
                        For i As Integer = 0 To Drr.Length - 1
                            strWhere &= "'" & Drr(i)("Location_Index").ToString & "'"
                            If i <> Drr.Length - 1 Then
                                strWhere &= ","
                            End If
                        Next
                        strWhere &= ")"

                        'Set Default Printer

                        Dim objConfig As New config_Report
                        cry = objConfig.GetReportInfo(Me.cboPrint.SelectedValue, " AND Location_Index IN " & strWhere)
                        frm.CrystalReportViewer1.ReportSource = cry
                        frm.ShowDialog()

                    End If
                End If
            End If


            ' End Select
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnAddM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddM.Click
        Try
            Dim frmLocation As New frmAddLockRack
            frmLocation.ShowDialog()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class