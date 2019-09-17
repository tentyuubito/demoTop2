Imports WMS_STD_Formula
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports System.Windows.Forms
Imports WMS_STD_Master

Public Class frmSupplier_Popup
    Private _supplier_index As String
  
    Private _SupplierName As String
    Private _SupplierName_eng As String
    Private _strSupplier_Id As String
    Public ReadOnly Property Supplier_Index() As String
        Get
            Return _supplier_index
        End Get
    End Property
    Public ReadOnly Property SupplierName() As String
        Get
            Return _SupplierName
        End Get
    End Property

    Public ReadOnly Property SupplierName_eng() As String
        Get
            Return _SupplierName_eng
        End Get
    End Property

    Public ReadOnly Property strSupplier_Id() As String
        Get
            Return _strSupplier_Id
        End Get
    End Property
    
#Region "GET DATA TO DATAGRIDVIEW"

    Private Sub getSupplier()
        Dim objClassDB As New ms_Supplier(ms_Supplier.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim AddWhereString As String = ""
        Try


            If Not Trim(Me.txtCondition.Text) = "" Then
                Select Case Me.cboCondition.SelectedIndex
                    Case 0
                        ' รหัสลูกค้า
                        AddWhereString = " AND  Supplier_Id Like '" & Me.txtCondition.Text.Replace("'", "''").Trim & "%' "
                    Case 1
                        ' ชื่อลูกค้า 
                        AddWhereString = " AND Supplier_Name Like '%" & Me.txtCondition.Text.Replace("'", "''").Trim & "%'"
                    Case 2
                        ' ชื่อลูกค้า 
                        AddWhereString = " AND Supplier_Name_eng Like '%" & Me.txtCondition.Text.Replace("'", "''").Trim & "%'"
                End Select
            End If

            objClassDB.getPopup_Search(AddWhereString)
            objDT = objClassDB.DataTable
            ' Me.grdList.DataSource = objDT

            Me.grdSupplier_PopupList.Rows.Clear()
            Me.grdSupplier_PopupList.Refresh()

            For i As Integer = 0 To objDT.Rows.Count - 1

                With Me.grdSupplier_PopupList
                    .Rows.Add()
                    .Rows(i).Cells("System_Index").Value = objDT.Rows(i).Item("Supplier_Index").ToString
                    .Rows(i).Cells("Supplier_Id").Value = objDT.Rows(i).Item("Supplier_Id").ToString
                    .Rows(i).Cells("Supplier_Name").Value = objDT.Rows(i).Item("Supplier_Name").ToString
                    .Rows(i).Cells("Supplier_Name_eng").Value = objDT.Rows(i).Item("Supplier_Name_eng").ToString
                    .Rows(i).Cells("Col_Title").Value = objDT.Rows(i).Item("Title").ToString
                End With

            Next

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
            Dim objLang As New W_Language
            objLang.SwitchLanguage(Me, 2049)
            objLang.SW_Language_Column(Me, Me.grdSupplier_PopupList, 2049)

            Me.cboCondition.SelectedIndex = 0
            ' *** Load Supplier to datagridveiw *** 
            Me.getSupplier()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub grdSupplier_PopupList_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdSupplier_PopupList.CellDoubleClick
        Try
            If Me.grdSupplier_PopupList.Rows.Count > 0 Then
                With Me.grdSupplier_PopupList
                    _supplier_index = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("System_Index").Value
                    _SupplierName = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("Supplier_Name").Value
                    _SupplierName_eng = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("Supplier_Name_eng").Value
                    _strSupplier_Id = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("Supplier_Id").Value
                End With
            End If
            Me.Visible = False
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Try
            Me.getSupplier()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        _supplier_index = ""
        _SupplierName = ""
        Me.Close()
    End Sub

    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Try
            If Me.grdSupplier_PopupList.Rows.Count > 0 Then
                _supplier_index = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("System_Index").Value
                _SupplierName = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("Supplier_Name").Value
                _SupplierName_eng = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("Supplier_Name_eng").Value
                _strSupplier_Id = grdSupplier_PopupList.Rows(grdSupplier_PopupList.CurrentRow.Index).Cells("Supplier_Id").Value
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
            Dim frm As New frmSupplier_Des
            frm.ShowDialog()
            Me.getSupplier()
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
                    objLang.SW_Language_Column(Me, Me.grdSupplier_PopupList, 2049)
                End If
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

End Class