Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Imports WMS_STD_Master

Public Class frmWareHouse_Popup
    Private _WareHouse_index As String
    Private _WareHouseName As String
    Private _WareHouseDes As String

    Public ReadOnly Property WareHouse_index() As String
        Get
            Return _WareHouse_index
        End Get
    End Property
    Public ReadOnly Property WareHouseName() As String
        Get
            Return _WareHouseName
        End Get
    End Property
    Public ReadOnly Property WareHouseDes() As String
        Get
            Return _WareHouseDes
        End Get
    End Property
   

#Region "GET DATA TO DATAGRIDVIEW"

    Private Sub getWareHouse()
        Dim objClassDB As New ms_Warehouse(ms_Warehouse.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim AddWhereString As String = ""
        Try


            If Not Trim(Me.txtCondition.Text) = "" Then
                Select Case Me.cboCondition.SelectedIndex
                    Case 0

                        AddWhereString = " AND  Warehouse_No Like '" & Me.txtCondition.Text.Replace("'", "''").Trim & "%' "
                    Case 1

                        AddWhereString = " AND Description Like '%" & Me.txtCondition.Text.Replace("'", "''").Trim & "%'"

                End Select
            End If

            objClassDB.getPopup_Search(AddWhereString)
            objDT = objClassDB.DataTable
            ' Me.grdList.DataSource = objDT


            Me.grdWareHouse_PopupList.Refresh()
            grdWareHouse_PopupList.AutoGenerateColumns = False
            grdWareHouse_PopupList.DataSource = objDT


        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub



#End Region
    Private Sub frmSupplier_Popup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'Dim objLang As New W_Language
            'objLang.SwitchLanguage(Me, 2049)
            'objLang.SW_Language_Column(Me, Me.grdWareHouse_PopupList, 2049)

            Me.cboCondition.SelectedIndex = 0
            ' *** Load Supplier to datagridveiw *** 
            Me.getWareHouse()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdSupplier_PopupList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdWareHouse_PopupList.CellDoubleClick
        Try
            If Me.grdWareHouse_PopupList.Rows.Count > 0 Then
                With Me.grdWareHouse_PopupList
                    _WareHouse_index = grdWareHouse_PopupList.Rows(grdWareHouse_PopupList.CurrentRow.Index).Cells("Col_Index").Value
                    _WareHouseName = grdWareHouse_PopupList.Rows(grdWareHouse_PopupList.CurrentRow.Index).Cells("Col_Warehouse_No").Value
                    _WareHouseDes = grdWareHouse_PopupList.Rows(grdWareHouse_PopupList.CurrentRow.Index).Cells("Col_Description").Value

                End With
            End If
            Me.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getWareHouse()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        _WareHouse_index = ""
        _WareHouseDes = ""
        _WareHouseName = ""
        Me.Close()
    End Sub

    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Try
            If Me.grdWareHouse_PopupList.Rows.Count > 0 Then
                _WareHouse_index = grdWareHouse_PopupList.Rows(grdWareHouse_PopupList.CurrentRow.Index).Cells("Col_Index").Value
                _WareHouseName = grdWareHouse_PopupList.Rows(grdWareHouse_PopupList.CurrentRow.Index).Cells("Col_Warehouse_No").Value
                _WareHouseDes = grdWareHouse_PopupList.Rows(grdWareHouse_PopupList.CurrentRow.Index).Cells("Col_Description").Value
            End If
            'frmParent.getCus(Cusid, Cusname)
            Me.Visible = False
            'Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frm As New frmWarehouse
            frm.ShowDialog()
            Me.getWareHouse()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtCondition_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCondition.KeyDown
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            btnSearch_Click(sender, e)
        End If
    End Sub

    Private Sub frmSupplier_Popup_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigSupplier_Popup
                    frm.ShowDialog()
                    Dim objLang As New W_Language
                    objLang.SwitchLanguage(Me, 2049)
                    objLang.SW_Language_Column(Me, Me.grdWareHouse_PopupList, 2049)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class