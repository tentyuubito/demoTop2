Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_OAW_INVENTORY_Datalayer

Public Class frmCheckReserveQty


 
    Private _ReserveQty As String
    Public Property ReserveQty() As String
        Get
            Return _ReserveQty
        End Get
        Set(ByVal value As String)
            _ReserveQty = value
        End Set
    End Property
    Private _LocationBalance_index As String
    Public Property LocationBalance_index() As String
        Get
            Return _LocationBalance_index
        End Get
        Set(ByVal value As String)
            _LocationBalance_index = value
        End Set
    End Property

    Private _SKU_Index As String
    Public Property SKU_Index() As String
        Get
            Return _SKU_Index
        End Get
        Set(ByVal value As String)
            _SKU_Index = value
        End Set
    End Property

  

    Private Sub CheckReserveQty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If SKU_Index = "" Or SKU_Index Is Nothing Then
                'btnExit_Click(sender, e)
            Else
                ViewAll()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Public Sub ViewAll()
        Try

            Me.grdReserveReturn.AutoGenerateColumns = False
            ViewReserveAll()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


#Region "ViewDATA"
    Public Sub ViewReserveAll()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getReserveAll(Me.SKU_Index)
            Me.grdReserveReturn.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            If SKU_Index = "" Or SKU_Index Is Nothing Then
                'btnExit_Click(sender, e)
            Else
                ViewAll()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.grdReserveReturn.RowCount = 0 Then Exit Sub

            If W_MSG_Confirm("คุณมั่นใจใช่หรือไม่ว่าไม่มีการกระบวนการที่กำลังหยิบสินค้าอยู่") = Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
            For irow As Integer = 0 To Me.grdReserveReturn.RowCount - 1
                If CBool(Me.grdReserveReturn.Rows(irow).Cells("chkSelect").Value) = True Then
                    Dim obj As New ml_CheckReserveQty
                    obj.UPDATE_RESERVE_LOCATIONBALANCE(grdReserveReturn.Rows(irow).Cells("col_LocationBalance_Index").Value, grdReserveReturn.Rows(irow).Cells("col_ReserveQtyAll").Value)

                End If
            Next

            W_MSG_Information_ByIndex("1")

            'ViewReserveAll()


            Me.btnRefresh_Click(sender, e)

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            For irow As Integer = 0 To Me.grdReserveReturn.RowCount - 1
                Me.grdReserveReturn.Rows(irow).Cells("chkSelect").Value = chkSelectAll.Checked
            Next
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub
End Class