
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer

Public Class frmMainUserManagement_Encode
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _User_Index As String = ""
    Public Property User_Index() As String
        Get
            Return _User_Index
        End Get
        Set(ByVal value As String)
            _User_Index = value
        End Set
    End Property

    Private Sub frmMainUsermanagement_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim objLang As New WMS_STD_Master.W_Language
            objLang.SwitchLanguage(Me, 2043)
            objLang.SW_Language_Column(Me, Me.grdUserManagement, 2043)

            grdUserManagement.AutoGenerateColumns = False
            cboSearchDocumentType.SelectedIndex = 0
            getSearchUsermanagement()

            grdUserManagement.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    'Sub ShowgrdUsermanagement()
    '    Dim objse_User As New se_User(se_User.enuOperation_Type.SEARCH)
    '    Try
    '        objse_User.SelectAll()
    '        grdUserManagement.DataSource = objse_User.DataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objse_User = Nothing
    '    End Try
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Dim frm As New frmUserManagement_Encode
        frm.SaveType = 0
        frm.ShowDialog()
        getSearchUsermanagement()
    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        If grdUserManagement.Rows.Count > 0 Then

            Dim frm As New frmUserManagement_Encode

            frm.SaveType = 1
            frm.User_Index = grdUserManagement.Rows(grdUserManagement.CurrentRow.Index).Cells("col_UserIndex").Value.ToString
            frm.ShowDialog()

        End If
        getSearchUsermanagement()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If Me.grdUsermanagement.Rows.Count <= 0 Then Exit Sub
            Dim User_Index As String = ""


            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objse_User As New se_User(se_User.enuOperation_Type.DELETE)
                User_Index = grdUserManagement.Rows(grdUserManagement.CurrentRow.Index).Cells("col_UserIndex").Value.ToString
                objse_User.DeleteByIndex(User_Index)
            End If
            getSearchUsermanagement()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Me.getSearchUsermanagement()
    End Sub
    ''' <summary>
    ''' </summary>
    ''' <remarks>
    ''' Date 12/02/2010
    ''' By TaTa 
    '''      แก้การค้นหา  User ระบบ (ไม่เอา User ที่เป็น Customer,Supplier มาแสดง)
    '''      ดักการคย์ข้อมูล (Replace("'", ""))
    ''' </remarks>
    Sub getSearchUsermanagement()
        Dim strSQL As String = ""
        Dim strWhere As String = ""

        'strSQL += "  SELECT  * FROM      se_User where  User_Index not in ('0') and status_id  not in (-1) "

        strSQL = " SELECT     dbo.se_User.*, dbo.se_Group.group_des AS GroupDes, dbo.ms_Department.Description AS DepartmentDes"
        strSQL &= "  FROM         dbo.se_User left JOIN"
        strSQL &= "               dbo.se_Group ON dbo.se_User.group_index = dbo.se_Group.group_index left JOIN"
        strSQL &= "               dbo.ms_Department ON dbo.se_User.Department_Index = dbo.ms_Department.Department_Index"
        strSQL &= " WHERE ((se_user.Customer_Index = '' AND  se_user.Supplier_Index = '' )"
        strSQL &= " Or (se_user.Customer_Index is null AND  se_user.Supplier_Index is null))"
        strSQL &= " AND  se_user.status_id <> -1 "


        If Me.txtSearchKey.Text.Trim <> "" Then
            Select Case cboSearchDocumentType.SelectedIndex

                Case 0
                    strWhere += " and User_ID = '" & Me.txtSearchKey.Text.Trim.Replace("'", "") & "' "
                Case 1
                    strWhere += " and userFullName like '" & Me.txtSearchKey.Text.Trim.Replace("'", "") & "%' "
                Case 2
                    strWhere += " and userName like '%" & Me.txtSearchKey.Text.Trim.Replace("'", "''") & "%' "
            End Select

        End If


        'add data to datagrid
        strSQL = strSQL + strWhere
        Dim objse_User As New SQLCommands

        objse_User.SQLComand(strSQL)
        grdUserManagement.DataSource = objse_User.DataTable
    End Sub


    Private Sub grdUserManagement_CellContentDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdUserManagement.CellContentDoubleClick
        btnUpdate_Click(sender, e)
    End Sub

    
    Private Sub frmMainUserManagement_Encode_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = System.Windows.Forms.Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigUser
                    frm.ShowDialog()
                    Dim oFunction As New WMS_STD_Master.W_Language
                    oFunction.SwitchLanguage(Me, 2043)
                    oFunction.SW_Language_Column(Me, Me.grdUserManagement, 2043)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnImportlicense_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImportlicense.Click
        Try
            If Me.grdUserManagement.Rows.Count <= 0 Then Exit Sub
            Dim User_Index As String = ""
            User_Index = grdUserManagement.Rows(grdUserManagement.CurrentRow.Index).Cells("col_UserIndex").Value.ToString

            Dim frm As New WMS_STD_Master.frmImportLicense()
            frm.ShowDialog()


            getSearchUsermanagement()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class