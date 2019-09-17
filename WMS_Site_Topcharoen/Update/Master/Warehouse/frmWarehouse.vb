Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports System.Windows.Forms

Public Class frmWarehouse

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update
    Private _validate As New WMS_STD_Master.ValidateCharacter
    Private _Warehouse_Index As String = ""
    Private _Province_Id_Old As String
    Public Property Province_Id_Old() As String
        Get
            Return _Province_Id_Old
        End Get
        Set(ByVal value As String)
            _Province_Id_Old = value
        End Set
    End Property

    Public Property Warehouse_Index() As String
        Get
            Return _Warehouse_Index
        End Get
        Set(ByVal value As String)
            _Warehouse_Index = value
        End Set
    End Property

    Private Sub frmProvince_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language
            oFunction.SwitchLanguage(Me, 2024)

            txtID.Text.Trim()
            txtDes.Text.Trim()
            Me.getDistributionCenter()

            Select Case SaveType
                Case 0 'Save

                Case 1 'Update
                    Dim objms_Warehouse As New ms_Warehouse(ms_Warehouse.enuOperation_Type.SEARCH)
                    objms_Warehouse.SearchWarehouse("", "", Warehouse_Index)
                    Dim odtms_Province As New DataTable
                    odtms_Province = objms_Warehouse.DataTable

                    If odtms_Province.Rows.Count > 0 Then
                        With odtms_Province.Rows(0)
                            Province_Id_Old = .Item("Warehouse_No").ToString
                            Warehouse_Index = .Item("Warehouse_Index").ToString
                            txtID.Text = .Item("Warehouse_No").ToString
                            txtDes.Text = .Item("Description").ToString
                            Me.cboDistributionCenter.SelectedValue = .Item("DistributionCenter_Index")
                        End With
                    End If
            End Select
            txtID.Text.Trim()
            txtDes.Text.Trim()
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
                W_MSG_Information("กรุณาระบุ WareHouse")
                txtDes.Focus()
                Exit Sub
            End If

            If (Me.cboDistributionCenter.SelectedIndex >= 0) Then
                Me.saveWarehouse(Warehouse_Index, txtID.Text, txtDes.Text, Me.cboDistributionCenter.SelectedValue)
            Else
                saveWarehouse(Warehouse_Index, txtID.Text, txtDes.Text) 'saveProvince
            End If
            ' Me.btnSave.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub saveWarehouse(ByVal Index As String, ByVal ID As String, ByVal Description As String, Optional ByVal DC_Index As String = "")
        Try
            Select Case SaveType
                Case 0 'Add New

                    Dim objCheckIdms_Warehouse As New ms_Warehouse(ms_Warehouse.enuOperation_Type.SEARCH)
                    Dim BCheckId As Boolean = objCheckIdms_Warehouse.isChckID(txtID.Text)

                    If BCheckId Then
                        W_MSG_Information_ByIndex(45)

                        btnSave.Enabled = True
                        Exit Sub
                    Else
                        Dim objms_Province As New ms_Warehouse(ms_Warehouse.enuOperation_Type.ADDNEW)
                        objms_Province.SaveData("", txtID.Text, txtDes.Text, DC_Index)

                        W_MSG_Information_ByIndex(1)

                        Me.Close()
                    End If

                Case 1 'Update
                    Dim objms_Province As New ms_Warehouse(ms_Warehouse.enuOperation_Type.UPDATE)
                    If Me.Province_Id_Old <> txtID.Text.Trim Then
                        If objms_Province.isExistID(txtID.Text) = True Then
                            W_MSG_Information_ByIndex(67)
                            Exit Sub
                        End If
                    End If
                    objms_Province.SaveData(Warehouse_Index, txtID.Text, txtDes.Text, DC_Index)

                    W_MSG_Information_ByIndex(1)

                    Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub frmWarehouse_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigWarehouse
                    frm.ShowDialog()
                    Dim oFunction As New WMS_STD_Master.W_Language
                    oFunction.SwitchLanguage(Me, 2024)
                    oFunction = Nothing
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