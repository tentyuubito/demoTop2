Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Threading
Imports System.Globalization
Imports System.Configuration.ConfigurationSettings
Imports System.Windows.Forms

Public Class frmMainRoute
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _Route_Index As String = ""
    Public Property Route_Index() As String
        Get
            Return _Route_Index
        End Get
        Set(ByVal value As String)
            _Route_Index = value
        End Set
    End Property

    Private Sub frmMainRouteT_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2041)
            oFunction.SW_Language_Column(Me, Me.grdRoute, 2041)

            grdRoute.AutoGenerateColumns = False
            cboSearchDocumentType.SelectedIndex = 0
            ShowgrdRoute()

            grdRoute.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub ShowgrdRoute()
        Dim objRoute As New ms_Route(ms_Route.enuOperation_Type.SEARCH)
        Try
            objRoute.GetAllAsDataTable()
            grdRoute.DataSource = objRoute.DataTable
        Catch ex As Exception
            Throw ex
        Finally
            objRoute = Nothing
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmRoute
        frm.SaveType = 0
        frm.ShowDialog()
        ShowgrdRoute()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdRoute.Rows.Count <> 0 Then

            Dim frm As New frmRoute

            frm.SaveType = 1
            frm.Route_Index = grdRoute.Rows(grdRoute.CurrentRow.Index).Cells("Col_Index").Value.ToString
            frm.ShowDialog()
        Else
            W_MSG_Information_ByIndex(77)
        End If
        ShowgrdRoute()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdRoute.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            Dim Route_Index As String = ""


            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objRoute As New ms_Route(ms_Route.enuOperation_Type.DELETE)
                Route_Index = grdRoute.Rows(grdRoute.CurrentRow.Index).Cells("Col_Index").Value.ToString
                objRoute.Route_Index = Route_Index
                objRoute.Delete()
            End If
            ShowgrdRoute()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.getSearchRoute_Type()
    End Sub

    Sub getSearchRoute_Type()
        Dim strSQL As String = ""
        Dim strWhere As String = ""

        strSQL += "  SELECT  * FROM      ms_Route where  Route_Index not in ('0') and status_id  not in (-1) "

        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchDocumentType.SelectedIndex

                Case 0 'Tag
                    strWhere += " and Route_NO like '%" & Me.txtSearchKey.Text.Trim & "%' "
                Case 1 'Lotº≈‘µ
                    strWhere += " and Description like '%" & Me.txtSearchKey.Text.Trim & "%' "
            End Select

        End If


        'add data to datagrid
        strSQL = strSQL + strWhere
        Dim objRoute As New SQLCommands

        objRoute.SQLComand(strSQL)
        grdRoute.DataSource = objRoute.DataTable
    End Sub




    Private Sub grdRoute_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdRoute.CellDoubleClick
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
                    oFunction.SW_Language_Column(Me, Me.grdRoute, 2041)
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
                Me.getSearchRoute_Type()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboSearchDocumentType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSearchDocumentType.SelectedIndexChanged
        Me.getSearchRoute_Type()
    End Sub
End Class