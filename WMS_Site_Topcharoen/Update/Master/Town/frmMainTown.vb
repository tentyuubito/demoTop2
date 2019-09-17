Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms
Imports WMS_STD_Master
Public Class frmMainTown

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _District_Index As String = ""
    Public Property District_Index() As String
        Get
            Return _District_Index
        End Get
        Set(ByVal value As String)
            _District_Index = value
        End Set
    End Property

    Private Sub frmMainDistrict_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdDocumentType.AutoGenerateColumns = False

            'Dim oFunction As New W_Language
            'oFunction.SwitchLanguage(Me, 2031)
            'oFunction.SW_Language_Column(Me, Me.grdDocumentType, 2031)

            grdDocumentType.AutoGenerateColumns = False
            cboSearchDocumentType.SelectedIndex = 0
            ShowgrdDistrict()

            grdDocumentType.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub ShowgrdDistrict()
        Dim objms_District As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Try
            'objms_District.SearchData_Click("", "")
            'grdDocumentType.DataSource = objms_District.DataTable

            Dim _exc As New DBType_SQLServer
            Dim _dt As New DataTable
            Dim strSQL As String = ""
            'Dim Town_index As String = New Sy_AutoNumber().getSys_Value("Town_index")
            strSQL &= " select ms_District.District,ms_Province.Province,ms_Town.* "
            strSQL &= " from ms_Town "
            strSQL &= "     inner join ms_District on ms_District.District_Index = ms_Town.District_Index"
            strSQL &= "     inner join ms_Province on ms_District.Province_Index = ms_Province.Province_Index"
            strSQL &= " where ms_Town.status_id not in (-1)"
            Dim strWhere As String = ""
            If Me.txtSearchKey.Text.Trim <> "" Then
                Select Case cboSearchDocumentType.SelectedIndex
                    Case 0
                        strWhere += " and Town_Id like '%" & Me.txtSearchKey.Text.Trim & "%' "
                    Case 1
                        strWhere += " and Town_Name like '%" & Me.txtSearchKey.Text.Trim & "%' "
                    Case 2
                        strWhere += " and District like '%" & Me.txtSearchKey.Text.Trim & "%' "
                    Case 3
                        strWhere += " and Province like '%" & Me.txtSearchKey.Text.Trim & "%' "
                End Select
            End If

            _dt = _exc.DBExeQuery(strSQL & strWhere & " order by ms_Province.Province,ms_District.District,ms_Town.Town_Name")
            Me.grdDocumentType.DataSource = _dt

        Catch ex As Exception
            Throw ex
        Finally
            objms_District = Nothing
        End Try
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.ShowgrdDistrict()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    'Sub getSearchDistrict()
    '    Dim strSQL As String = ""
    '    Dim strWhere As String = ""

    '    strSQL += "  SELECT     dbo.ms_Province.Province AS Province, dbo.ms_District.District_Id  AS District_Id, dbo.ms_District.District_Index AS District_Index, dbo.ms_District.District  AS District, dbo.ms_District.status_id FROM         dbo.ms_District INNER JOIN dbo.ms_Province  ON dbo.ms_District.Province_Index = dbo.ms_Province.Province_Index  where  dbo.ms_District.District_Index not in ('0') and dbo.ms_District.status_id  not in (-1) "
    '    If Me.txtSearchKey.Text.Trim <> "" Then
    '        Select Case cboSearchDocumentType.SelectedIndex

    '            Case 0
    '                strWhere += " and District_Id like '%" & Me.txtSearchKey.Text.Trim & "%' "
    '            Case 1
    '                strWhere += " and District like '%" & Me.txtSearchKey.Text.Trim & "%' "
    '            Case 2
    '                strWhere += " and Province like '%" & Me.txtSearchKey.Text.Trim & "%' "
    '        End Select
    '    End If
    '    'add data to datagrid
    '    strSQL = strSQL + strWhere
    '    Dim objms_DocumentType As New SQLCommands

    '    objms_DocumentType.SQLComand(strSQL)
    '    grdDocumentType.DataSource = objms_DocumentType.DataTable
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim frm As New frmTown
            frm.SaveType = 0
            frm.ShowDialog()
            ShowgrdDistrict()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If grdDocumentType.Rows.Count > 0 Then
                Exit Sub
            End If
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim _exc As New DBType_SQLServer
                Dim strSQL As String = ""
                Dim Town_index As String = grdDocumentType.Rows(grdDocumentType.CurrentRow.Index).Cells("Col_Index").Value.ToString

                strSQL &= String.Format(" update ms_Town set status_id = -1 where Town_index = '{0}'", Town_index)
                _exc.DBExeNonQuery(strSQL)

            End If
            ShowgrdDistrict()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    'Private Sub grdDocumentType_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDocumentType.CellDoubleClick
    '    btnUpdate_Click(sender, e)
    'End Sub

    Private Sub frmMainDistrict_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigDistrict
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2031)
                    oFunction.SW_Language_Column(Me, Me.grdDocumentType, 2031)
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
                Me.ShowgrdDistrict()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class