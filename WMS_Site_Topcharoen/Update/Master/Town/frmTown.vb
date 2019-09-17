Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master
Imports WMS_STD_Master.ValidateCharacter
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms
Public Class frmTown

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update
    Private _validate As New ValidateCharacter
    'Private _District_Id_Old As String
    'Public Property District_Id_Old() As String
    '    Get
    '        Return _District_Id_Old
    '    End Get
    '    Set(ByVal value As String)
    '        _District_Id_Old = value
    '    End Set
    'End Property

    'Private _District_Index As String = ""
    'Public Property District_Index() As String
    '    Get
    '        Return _District_Index
    '    End Get
    '    Set(ByVal value As String)
    '        _District_Index = value
    '    End Set
    'End Property

    Private Sub frmDistrict_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            AddcbProvince()
            AddcbDistrict()
            'Dim oFunction As New W_Language
            'oFunction.SwitchLanguage(Me, 2031)

            'Insert
            'oFunction.Insert(Me)

            'SwitchLanguage


            Select Case SaveType
                Case 0 'Save
                Case 1 'Update
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Sub AddcbProvince()
        Dim objClassDB As New ms_Province(ms_Province.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    cboProvince.DataSource = objDT
                    cboProvince.DisplayMember = "Province"
                    cboProvince.ValueMember = "Province_Index"
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Sub AddcbDistrict()
        Dim objClassDB As New ms_District(ms_District.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objClassDB.SearchData_Click("", Me.cboProvince.SelectedValue)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    cboDistrict.DataSource = objDT
                    cboDistrict.DisplayMember = "District"
                    cboDistrict.ValueMember = "District_Index"
                    i = i + 1
                Next
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub
    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If _validate.validateKey(txtID.Text, lbID.Text) Then Return
            'If _validate.validateKey(txtDes.Text, lblDes.Text) Then Return

            If txtID.Text = "" Then
                W_MSG_Information("กรุณาเลือก รหัส")

                Exit Sub
            End If
            If txtDes.Text = "" Then
                W_MSG_Information("กรุณาเลือก ตำบล")

                Exit Sub
            End If

            If cboProvince.SelectedIndex = 0 Then
                W_MSG_Information("กรุณาเลือก จังหวัด")

                Exit Sub
            End If

            If cboDistrict.SelectedIndex = 0 Then
                W_MSG_Information("กรุณาเลือก อำเภอ")

                Exit Sub
            End If

            Dim _exc As New DBType_SQLServer
            Dim strSQL As String = ""
            Dim Town_index As String = New Sy_AutoNumber().getSys_Value("Town_index")
            strSQL = "Insert into ms_Town (Town_index,Town_ID,Town_Name,District_Index,status_id) "
            strSQL &= String.Format(" Values ('{0}','{1}','{2}','{3}',0)", Town_index, Me.txtID.Text.Trim, Me.txtDes.Text.Trim, Me.cboDistrict.SelectedValue)
            _exc.DBExeNonQuery(strSQL)

            W_MSG_Information_ByIndex("1")

            Me.Close()
            'saveDistrict(District_Index, txtID.Text, cboProvince.SelectedValue.ToString, txtDes.Text)
            'Me.btnSave.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmDistrict_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigDistrict
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2031)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub cboProvince_SelectionChangeCommitted(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboProvince.SelectionChangeCommitted
        Try
            AddcbDistrict()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class