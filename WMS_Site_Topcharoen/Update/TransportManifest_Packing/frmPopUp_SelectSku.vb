Imports WMS_STD_Master_Datalayer
Imports WMS_STD_master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master
Public Class frmPopUp_SelectSku

    Private _DtData As New DataTable
    Sub New(ByVal pDtData As DataTable)
        InitializeComponent()
        _DtData = pDtData
    End Sub

    Private _ReturnDatarow As DataRow
    Public ReadOnly Property ReturnDatarow() As DataRow
        Get
            Return _ReturnDatarow
        End Get
    End Property

    Private Sub frmPopUp_SelectSku_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdPopupList.AutoGenerateColumns = False
            If _DtData.Rows.Count <= 0 Then Me.Close()
            Me.grdPopupList.DataSource = _DtData
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Me.Close()
    End Sub


    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Try
            Me.Selection()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub grdPopupList_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdPopupList.CellDoubleClick
        Try
            Me.Selection()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Sub Selection()
        Try
            Dim Dr() As DataRow
            'Dr = CType(Me.grdPopupList.DataSource, DataTable).Select(String.Format(" Sku_Index = '{0}' ", Me.grdPopupList.CurrentRow.Cells("Col_Sku_Index").Value.ToString))
            Dr = CType(Me.grdPopupList.DataSource, DataTable).Select(String.Format(" Package_Index = '{0}' ", Me.grdPopupList.CurrentRow.Cells("col_Package_Index").Value.ToString))
            _ReturnDatarow = Dr(0)
            Me.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


End Class