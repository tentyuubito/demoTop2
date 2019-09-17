Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_OAW_INVENTORY_Datalayer

Public Class frmCheckReserveQty_Update



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
            Me.grdBorrow.AutoGenerateColumns = False
            Me.grdBorrowReturn.AutoGenerateColumns = False
            Me.grdTranferCode.AutoGenerateColumns = False
            Me.grdTranferOwner.AutoGenerateColumns = False
            Me.grdTranferStatus.AutoGenerateColumns = False
            Me.grdWithDraw.AutoGenerateColumns = False

            ViewWithDraw()
            ViewTransferStatus()
            ViewTransferOwner()
            ViewTransferCode()
            ViewBorrow()
            ViewBorrowReturn()
            Get_SumData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Sub Get_SumData()
        Try
            If Me.grdWithDraw.Rows.Count > 0 Then
                Dim dtWithDraw As DataTable
                dtWithDraw = CType(Me.grdWithDraw.DataSource, DataTable)
                txtWithDraw.Text = Format(CDbl(dtWithDraw.Compute("sum(Total_Qty)", "Withdraw_Index <> ''").ToString), "#,##0.00")
            End If
            If Me.grdTranferStatus.Rows.Count > 0 Then
                Dim dtTranferStatus As DataTable
                dtTranferStatus = CType(Me.grdTranferStatus.DataSource, DataTable)
                txtTranferStatus.Text = Format(CDbl(dtTranferStatus.Compute("sum(Total_Qty)", "TransferStatus_Index <> ''").ToString), "#,##0.00")
            End If
            If Me.grdBorrow.Rows.Count > 0 Then
                Dim dtBorrow As DataTable
                dtBorrow = CType(Me.grdBorrow.DataSource, DataTable)
                txtBorrow.Text = Format(CDbl(dtBorrow.Compute("sum(Total_Qty)", "Borrow_Index <> ''").ToString), "#,##0.00")
            End If
            If Me.grdBorrowReturn.Rows.Count > 0 Then
                Dim dtBorrowReturn As DataTable
                dtBorrowReturn = CType(Me.grdBorrowReturn.DataSource, DataTable)
                txtBorrowReturn.Text = Format(CDbl(dtBorrowReturn.Compute("sum(Total_Qty)", "BorrowReturn_Index <> ''").ToString), "#,##0.00")
            End If
            If Me.grdTranferCode.Rows.Count > 0 Then
                Dim dtTranferCode As DataTable
                dtTranferCode = CType(Me.grdTranferCode.DataSource, DataTable)
                txtTranferCode.Text = Format(CDbl(dtTranferCode.Compute("sum(Total_Qty)", "TransferCode_Index <> ''").ToString), "#,##0.00")
            End If
            If Me.grdTranferOwner.Rows.Count > 0 Then
                Dim dtTranferOwner As DataTable
                dtTranferOwner = CType(Me.grdTranferOwner.DataSource, DataTable)
                txtTranferOwner.Text = Format(CDbl(dtTranferOwner.Compute("sum(Total_Qty)", "TransferOwner_Index <> ''").ToString), "#,##0.00")
            End If
            Dim obj As New ml_CheckReserveQty
            obj.getTotal_ReserveQty_System(Me.SKU_Index)
            txtReserveQtySystem.Text = Format(obj.DataTable.Rows(0)("Total_ReserveQty"), "#,##0.00")
            txtSum.Text = Format((CDbl(txtWithDraw.Text) + CDbl(txtTranferStatus.Text) + CDbl(txtTranferOwner.Text) + CDbl(txtTranferCode.Text) + CDbl(txtBorrow.Text) + CDbl(txtBorrowReturn.Text)), "#,##0.00")
            txtDiff.Text = CDbl(txtSum.Text) - CDbl(txtReserveQtySystem.Text)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#Region "ViewDATA"
    Public Sub ViewWithDraw()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getWithDraw(Me.SKU_Index)
            Me.grdWithDraw.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ViewTransferStatus()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getTransferStatus(Me.SKU_Index)
            Me.grdTranferStatus.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub ViewTransferOwner()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getTransferOwner(Me.SKU_Index)
            Me.grdTranferOwner.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ViewTransferCode()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getTransferCode(Me.SKU_Index)
            Me.grdTranferCode.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ViewBorrow()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getBorrow(Me.SKU_Index)
            Me.grdBorrow.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Public Sub ViewBorrowReturn()
        Try
            Dim obj As New ml_CheckReserveQty
            obj.getBorrowReturn(Me.SKU_Index)
            Me.grdBorrowReturn.DataSource = obj.DataTable
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
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
End Class