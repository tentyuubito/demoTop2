Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Threading
Imports System.Globalization
Imports System.Configuration.ConfigurationSettings
Imports System.Windows.Forms

Public Class frmMainConfig_Picking_KSL
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _Running_Index As String = ""
    Public Property Running_Index() As String
        Get
            Return _Running_Index
        End Get
        Set(ByVal value As String)
            _Running_Index = value
        End Set
    End Property

    Private Sub frmMainRouteT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2041)
            oFunction.SW_Language_Column(Me, Me.grd, 2041)
            'Me.Col_Index.Visible = False

            cboSearchDocumentType.SelectedIndex = 0
            getSearchData()

            grd.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Sub ShowgrdRoute()
    '    Dim objRoute As New ms_Route(ms_Route.enuOperation_Type.SEARCH)
    '    Try
    '        objRoute.GetAllAsDataTable()
    '        grd.DataSource = objRoute.DataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objRoute = Nothing
    '    End Try
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim frm As New frmConfig_Picking_KSL
            frm.SaveType = 0
            frm.Running_Id = ""
            frm.ShowDialog()
            getSearchData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If grd.Rows.Count <> 0 Then

                Dim frm As New frmConfig_Picking_KSL
                frm.SaveType = 1
                frm.Running_Id = grd.Rows(grd.CurrentRow.Index).Cells("Col_Id").Value.ToString
                frm.ShowDialog()
                getSearchData()
            Else
                'W_MSG_Information_ByIndex(77)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grd.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim xQuery As New cls_KSL
                xQuery.DeleteConfigPicking(Me.grd.Rows(grd.CurrentRow.Index).Cells("Col_Id").Value.ToString)
            End If
            getSearchData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getSearchData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub getSearchData()
        Try
            Dim strWhere As String = ""
            If Me.txtSearchKey.Text.Trim <> "" Then
                Select Case cboSearchDocumentType.SelectedIndex
                    Case 0 'Tag
                        strWhere += " and Running_Id like '%" & Me.txtSearchKey.Text.Trim & "%' "
                    Case 1 'Lotº≈‘µ
                        strWhere += " and Description like '%" & Me.txtSearchKey.Text.Trim & "%' "
                End Select

            End If

            Dim xQuery As New cls_KSL
            grd.AutoGenerateColumns = False
            grd.DataSource = xQuery.getSearchData_ConfigPicking_Group(strWhere)

        Catch ex As Exception
            Throw ex
        End Try


    End Sub




    Private Sub grdRoute_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grd.CellDoubleClick
        btnUpdate_Click(sender, e)

    End Sub

    Private Sub frmMainRoute_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
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

    Private Sub txtSearchKey_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtSearchKey.KeyDown

        Try
            If e.KeyCode = Keys.Enter Then
                Me.getSearchData()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboSearchDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSearchDocumentType.SelectedIndexChanged
        Try
            Me.getSearchData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
End Class