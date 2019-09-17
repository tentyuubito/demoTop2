Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Public Class frmSubRoute
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update


    Private _SubRoute_Index As String = ""
    Public Property SubRoute_Index() As String
        Get
            Return _SubRoute_Index
        End Get
        Set(ByVal value As String)
            _SubRoute_Index = value
        End Set
    End Property
    Private _Route_Index As String = ""
    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal value As String)
            _Route_Index = value
        End Set
    End Property

    Private _SubRoute_Type_ID_Old As String = ""
    Public Property SubRoute_Type_ID_Old() As String
        Get
            Return _SubRoute_Type_ID_Old
        End Get
        Set(ByVal value As String)
            _SubRoute_Type_ID_Old = value
        End Set
    End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmSubRouteT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            

            grdPostcode.AutoGenerateColumns = False

            Dim objms_SubRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
            objms_SubRoute = New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
            objms_SubRoute.SearchSubRoute_Postcode(SubRoute_Index)
            grdPostcode.DataSource = objms_SubRoute.GetDataTable
            'grdDistributionCenter.AutoGenerateColumns = False
            Me.getDistributionCenter()
            Select Case SaveType
                Case 0 'Save

                    'EnableEvent(False)
                Case 1 'Update

                    objms_SubRoute.SearchSubRoute(SubRoute_Index)
                    Dim odtms_SubRoute As New DataTable
                    odtms_SubRoute = objms_SubRoute.DataTable

                    If odtms_SubRoute.Rows.Count > 0 Then
                        With odtms_SubRoute.Rows(0)
                            Me.SubRoute_Type_ID_Old = .Item("SubRoute_No").ToString
                            SubRoute_Index = .Item("SubRoute_Index").ToString
                            txtID.Text = .Item("SubRoute_No").ToString
                            txtDes.Text = .Item("Description").ToString
                            '  txtZipCode.Text = .Item("str1").ToString
                            Me.cboDistributionCenter.SelectedValue = .Item("DistributionCenter_Index")
                        End With
                    End If


                    'ShowgrdRoute()
                    ''EnableEvent(True)
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            'If txtID.Text.Trim = "" Then
            '    W_MSG_Information_ByIndex(74)
            '    txtID.Focus()
            '    Exit Sub
            'End If

            If txtDes.Text.Trim = "" Then
                W_MSG_Information_ByIndex(75)
                txtDes.Focus()
                Exit Sub
            End If


            saveSubRoute_Type()
            ' Me.btnSave.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub saveSubRoute_Type()
        Try
            Select Case SaveType
                Case 0 'Add New

                    Dim objCheckIdms_SubRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.ADDNEW)
                    Dim BCheckId As Boolean = objCheckIdms_SubRoute.isExistID(Me._Route_Index, txtID.Text)

                    'If BCheckId Then
                    '    W_MSG_Information_ByIndex(45)
                    '    btnSave.Enabled = True
                    '    Exit Sub
                    'Else
                    Dim objms_SubRoute As New ms_SubRoute_Update(ms_SubRoute_Update.enuOperation_Type.ADDNEW)
                    Dim objDBIndex As New Sy_AutoNumber
                    objms_SubRoute.Route_Index = Me._Route_Index
                    objms_SubRoute.SubRoute_Index = objDBIndex.getSys_Value("SubRoute_Index")
                    objms_SubRoute.SubRoute_No = txtID.Text.Trim
                    objms_SubRoute.Description = txtDes.Text.Trim
                    '     objms_SubRoute.Str1 = txtZipCode.Text
                    If (Me.cboDistributionCenter.SelectedIndex >= 0) Then
                        objms_SubRoute.DistributionCenter_Index = Me.cboDistributionCenter.SelectedValue
                    End If
                    objms_SubRoute.Insert()
                    Dim dtPostcode As New DataTable
                    dtPostcode = CType(grdPostcode.DataSource, DataTable)
                    objms_SubRoute.InsertSubRoute_Postcode(objms_SubRoute.SubRoute_Index, dtPostcode)
                    W_MSG_Information_ByIndex(1)
                    SaveType = 1
                    'EnableEvent(True)

                    Me.Close()
                    ' End If

                Case 1 'Update
                    Dim objms_SubRoute As New ms_SubRoute_Update(ms_SubRoute_Update.enuOperation_Type.UPDATE)

                    If Me.SubRoute_Type_ID_Old <> txtID.Text.Trim Then
                        If objms_SubRoute.isExistID(Me._Route_Index, txtID.Text) = True Then
                            W_MSG_Information_ByIndex(67)
                            Exit Sub
                        End If
                    End If
                    objms_SubRoute.Route_Index = Me._Route_Index
                    objms_SubRoute.SubRoute_Index = SubRoute_Index
                    objms_SubRoute.SubRoute_No = txtID.Text.Trim
                    objms_SubRoute.Description = txtDes.Text.Trim
                    '     objms_SubRoute.Str1 = txtZipCode.Text
                    If (Me.cboDistributionCenter.SelectedIndex >= 0) Then
                        objms_SubRoute.DistributionCenter_Index = Me.cboDistributionCenter.SelectedValue
                    End If
                    Dim dtPostcode As New DataTable
                    dtPostcode = CType(grdPostcode.DataSource, DataTable)
                    objms_SubRoute.InsertSubRoute_Postcode(objms_SubRoute.SubRoute_Index, dtPostcode)

                    objms_SubRoute.Update()
                    W_MSG_Information_ByIndex(1)
                    SaveType = 1
                    'EnableEvent(True)
                    Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAddSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Dim frm As New WMS_STD_Master.frmDistributionCenter
            frm.SaveType = 0
            'frm.SubRoute_Index = Me._SubRoute_Index
            frm.ShowDialog()
            'ShowgrdRoute()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If grdDistributionCenter.RowCount = 0 Then Exit Sub
    '    If grdDistributionCenter.Rows.Count <> 0 Then
    '        Dim frm As New frmDistributionCenter
    '        frm.SaveType = 1
    '        frm.SubRoute_Index = Me._SubRoute_Index
    '        frm.DistributionCenter_Index = grdDistributionCenter.Rows(grdDistributionCenter.CurrentRow.Index).Cells("Col_Index").Value.ToString
    '        frm.ShowDialog()
    '    Else
    '        W_MSG_Information_ByIndex(77)
    '    End If
    '    ShowgrdRoute()
    'End Sub

    'Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        If grdDistributionCenter.Rows.Count = 0 Then
    '            W_MSG_Error_ByIndex(76)
    '            Exit Sub
    '        End If
    '        Dim SubRoute_Index As String = ""


    '        If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
    '            Dim objRoute As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.DELETE)
    '            SubRoute_Index = grdDistributionCenter.Rows(grdDistributionCenter.CurrentRow.Index).Cells("Col_Index").Value.ToString
    '            objRoute.SubRoute_Index = SubRoute_Index
    '            objRoute.Delete_Master(SubRoute_Index)
    '        End If
    '        ShowgrdRoute()
    '    Catch ex As Exception
    '        W_MSG_Error(ex.Message)
    '    End Try
    'End Sub

    'Sub ShowgrdRoute()
    '    Dim objRoute As New ms_DistributionCenter(ms_DistributionCenter.enuOperation_Type.SEARCH)
    '    Try
    '        objRoute.GetAllAsDataTable(Me._SubRoute_Index)
    '        grdDistributionCenter.DataSource = objRoute.DataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objRoute = Nothing
    '    End Try
    'End Sub

    'Sub 'EnableEvent(ByVal enable As Boolean)
    '    btnAddSub.Enabled = enable
    '    btnUpdate.Enabled = enable
    '    btnDelete.Enabled = enable
    'End Sub

    Private Sub grdPostcode_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPostcode.CellValueChanged
        Try
            If e.RowIndex <= -1 Then Exit Sub
            If grdPostcode.RowCount = 1 Then Exit Sub


            Dim oSubRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)

            Dim strPostcode As String = ""
            If grdPostcode.CurrentRow.Cells("col_Postcode").Value.ToString = "" Then Exit Sub
            Dim strSubRoute As String = ""
            If grdPostcode.RowCount > 1 Then
                strSubRoute = oSubRoute.CheckPostcode(strPostcode)
                If strSubRoute <> "" Then
                    W_MSG_Information("Postcode is Use In : " & strSubRoute)
                    grdPostcode.CurrentRow.Cells("col_Postcode").Value = ""
                    Exit Sub
                Else
                    Dim intC As Integer = e.RowIndex
                    For i As Integer = 0 To grdPostcode.Rows.Count - 2
                        If intC = i Then Continue For
                        If (grdPostcode.Rows(i).Cells("col_Postcode").Value.ToString = grdPostcode.CurrentRow.Cells("col_Postcode").Value.ToString) Then
                            W_MSG_Information_ByIndex(400012)
                            grdPostcode.CurrentRow.Cells("col_Postcode").Value = ""
                            Exit Sub
                        End If
                    Next
                End If

             


            End If
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

End Class