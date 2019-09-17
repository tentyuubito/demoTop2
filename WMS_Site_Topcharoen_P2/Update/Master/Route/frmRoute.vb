Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Public Class frmRoute
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _validate As New WMS_STD_Master.ValidateCharacter
    Private _Route_Index As String = ""
    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal value As String)
            _Route_Index = value
        End Set
    End Property


    Private _Route_Type_ID_Old As String = ""
    Public Property Route_Type_ID_Old() As String
        Get
            Return _Route_Type_ID_Old
        End Get
        Set(ByVal value As String)
            _Route_Type_ID_Old = value
        End Set
    End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmRouteT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2041)
            oFunction.SW_Language_Column(Me, Me.grdSubRoute, 2041)

            grdSubRoute.AutoGenerateColumns = False
            Select Case SaveType
                Case 0 'Save
                    'Dim objDBIndex As New Sy_AutoNumber
                    'Me.txtID.Text = objDBIndex.getSys_ID("Route_Id")
                    'objDBIndex = Nothing
                    EnableEvent(False)
                Case 1 'Update
                    Dim objms_Route As New ms_Route(ms_Route.enuOperation_Type.SEARCH)
                    objms_Route.SearchRoute(Route_Index)
                    Dim odtms_Route As New DataTable
                    odtms_Route = objms_Route.DataTable

                    If odtms_Route.Rows.Count > 0 Then
                        With odtms_Route.Rows(0)
                            Me.Route_Type_ID_Old = .Item("Route_No").ToString
                            Route_Index = .Item("Route_Index").ToString
                            txtID.Text = .Item("Route_No").ToString
                            txtDes.Text = .Item("Description").ToString
                        End With
                    End If
                    ShowgrdRoute()
                    EnableEvent(True)
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        Try
            If _validate.validateKey(txtID.Text, lbID.Text) Then Return
            If _validate.validateKey(txtDes.Text, lblDes.Text) Then Return
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


            saveRoute_Type(Route_Index, txtID.Text.Trim, txtDes.Text.Trim)
            ' Me.btnSave.Enabled = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub saveRoute_Type(ByVal Index As String, ByVal ID As String, ByVal Description As String)
        Try
            Select Case SaveType
                Case 0 'Add New

                    Dim objCheckIdms_Route As New ms_Route(ms_Route.enuOperation_Type.ADDNEW)
                    Dim BCheckId As Boolean = objCheckIdms_Route.isExistID(txtID.Text)

                    'If BCheckId Then
                    '    W_MSG_Information_ByIndex(45)
                    '    btnSave.Enabled = True
                    '    Exit Sub
                    'Else
                    Dim objms_Route As New ms_Route(ms_Route.enuOperation_Type.ADDNEW)
                    Dim objDBIndex As New Sy_AutoNumber
                    objms_Route.Route_Index = objDBIndex.getSys_Value("Route_Index")
                    Me._Route_Index = objms_Route.Route_Index
                    objms_Route.Route_No = txtID.Text.Trim
                    objms_Route.Description = txtDes.Text.Trim
                    objms_Route.Insert()
                    W_MSG_Information_ByIndex(1)
                    SaveType = 1
                    EnableEvent(True)
                    ' Me.Close()
                    'End If

                Case 1 'Update
                    Dim objms_Route As New ms_Route(ms_Route.enuOperation_Type.UPDATE)

                    If Me.Route_Type_ID_Old <> txtID.Text.Trim Then
                        If objms_Route.isExistID(txtID.Text) = True Then
                            W_MSG_Information_ByIndex(67)
                            Exit Sub
                        End If
                    End If

                    objms_Route.Route_Index = Route_Index
                    objms_Route.Route_No = txtID.Text.Trim
                    objms_Route.Description = txtDes.Text.Trim
                    objms_Route.Update()
                    W_MSG_Information_ByIndex(1)
                    SaveType = 1
                    EnableEvent(True)
                    ' Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnAddSub_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSub.Click
        Try
            Dim frm As New frmSubRoute
            frm.SaveType = 0
            frm.Route_Index = Me._Route_Index
            frm.ShowDialog()
            ShowgrdRoute()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdSubRoute.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            Dim SubRoute_Index As String = ""


            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.DELETE)
                SubRoute_Index = grdSubRoute.Rows(grdSubRoute.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objRoute.SubRoute_Index = SubRoute_Index
                objRoute.Delete()
            End If
            ShowgrdRoute()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdSubRoute.RowCount = 0 Then Exit Sub
        If grdSubRoute.Rows.Count <> 0 Then
            Dim frm As New frmSubRoute
            frm.SaveType = 1
            frm.Route_Index = Me._Route_Index
            frm.SubRoute_Index = grdSubRoute.Rows(grdSubRoute.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
        Else
            W_MSG_Information_ByIndex(77)
        End If
        ShowgrdRoute()
    End Sub

    Sub ShowgrdRoute()
        Dim objRoute As New ms_SubRoute(ms_SubRoute.enuOperation_Type.SEARCH)
        Try
            objRoute.GetAllAsDataTable(Me._Route_Index)
            grdSubRoute.DataSource = objRoute.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objRoute = Nothing
        End Try
    End Sub

    Sub EnableEvent(ByVal enable As Boolean)
        btnAddSub.Enabled = enable
        btnUpdate.Enabled = enable
        btnDelete.Enabled = enable
    End Sub

    Private Sub grdSubRoute_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSubRoute.CellDoubleClick
        btnUpdate_Click(sender, e)

    End Sub

    Private Sub frmRoute_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigRoute
                    frm.ShowDialog()
                    Dim oFunction As New WMS_STD_Master.W_Language
                    oFunction.SwitchLanguage(Me, 2041)
                    oFunction.SW_Language_Column(Me, Me.grdSubRoute, 2041)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class