Imports WMS_STD_Master
Imports WMS_STD_Master.W_Language

Public Class frmWithdrawSelected

    Private dtBind As DataTable
    Private dtBindItem As DataTable
    Private key As New Default_Key
    Private allowData As New cls_3PLEmployee
    Private search As New cls_WithdrawSearch

    Private _Customer_Index As String = ""
    Public Property Customer_Index() As String
        Get
            Return Me._Customer_Index
        End Get
        Set(ByVal value As String)
            Me._Customer_Index = value
        End Set
    End Property
    Private _drReturn() As DataRow
    Public ReadOnly Property drReturn() As DataRow()
        Get
            Return Me._drReturn
        End Get
    End Property
    Private _isSelect As Boolean = False
    Public ReadOnly Property isSelect() As Boolean
        Get
            Return Me._isSelect
        End Get
    End Property

    Private Sub frmWithdrawSelected_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.dgvWithdraw.AutoGenerateColumns = False
            Me.dgvWithdrawItem.AutoGenerateColumns = False
            Me.key.keyAndcheck(Me.txtWithdraw_No, Me.chkWithdrawNo)
            Me.key.keyAndcheck(Me.txtBookingNo, Me.chkBookingNo)
            Me.key.keyAndcheck(Me.txtInvoiceNo, Me.chkInvoiceNo)
            Me.key.keyAndcheck(Me.txtTagNo, Me.chkTagNo)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click
        Dim Withdraw_Date_Start As String = ""
        Dim Withdraw_Date_End As String = ""
        Dim Withdraw_No As String = ""
        Dim Booking_No As String = ""
        Dim Invoice_No As String = ""
        Dim Tag_No As String = ""
        Try
            Me.btnSearch.Enabled = False
            If chkWithdraw_Date.Checked Then
                Withdraw_Date_Start = Me.dtpStart.Value.ToString("dd/MM/yyyy")
                Withdraw_Date_End = Me.dtpEnd.Value.ToString("dd/MM/yyyy")
            End If
            If Me.chkWithdrawNo.Checked AndAlso Me.txtWithdraw_No.Text.Trim.Length > 0 Then
                Withdraw_No = Me.txtWithdraw_No.Text
            End If
            If Me.chkBookingNo.Checked AndAlso Me.txtBookingNo.Text.Trim.Length > 0 Then
                Booking_No = Me.txtBookingNo.Text
            End If
            If Me.chkInvoiceNo.Checked AndAlso Me.txtInvoiceNo.Text.Trim.Length > 0 Then
                Invoice_No = Me.txtInvoiceNo.Text
            End If
            If Me.chkTagNo.Checked AndAlso Me.txtTagNo.Text.Trim.Length > 0 Then
                Tag_No = Me.txtTagNo.Text
            End If
            Using dt As DataTable = search.getWithdraw(Withdraw_No, Withdraw_Date_Start, Withdraw_Date_End, Me._Customer_Index, Booking_No, Invoice_No, Tag_No)
                If Me.dgvWithdraw.DataSource Is Nothing Then
                    Me.dtBind = dt
                    Me.allowData.setDataColumnReadOnly(Me.dtBind)
                    Me.allowData.setDataColumnAllowNull(Me.dtBind, True)
                    Me.dgvWithdraw.DataSource = Me.dtBind
                Else
                    Me.dtBind.Clear()
                    Me.dtBind.Merge(dt)
                End If
                Me.dtBind.AcceptChanges()
            End Using
            If Me.dtBindItem IsNot Nothing Then Me.dtBindItem.Clear()
            Me.btnSearch.Enabled = True
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            Withdraw_Date_Start = Nothing
            Withdraw_Date_End = Nothing
            Withdraw_No = Nothing
            Booking_No = Nothing
            Invoice_No = Nothing
            Tag_No = Nothing
        End Try
    End Sub

    Private Sub dgvWithdraw_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvWithdraw.CellValueChanged
        Try
            If Me.dtBind Is Nothing OrElse Me.dtBind.Rows.Count = 0 Then Exit Sub
            Select Case Me.dgvWithdraw.Columns(e.ColumnIndex).Name.ToUpper()
                Case "col_chkSelect".ToUpper()
                    If Me.dtBind.Rows(e.RowIndex).Item("chk") Then
                        Me.dtBind.AcceptChanges()
                        Using dt As DataTable = search.getWithdrawItem(Me.dtBind.Rows(e.RowIndex).Item("Withdraw_No").ToString())
                            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                                MessageBox.Show("ไม่พบรายการใน เลขที่ใบเบิกนี้", "ไม่พบรายการ", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                                Exit Sub
                            Else
                                If Me.dgvWithdrawItem.DataSource Is Nothing Then
                                    Me.dtBindItem = dt
                                    Me.allowData.setDataColumnReadOnly(Me.dtBindItem)
                                    Me.allowData.setDataColumnAllowNull(Me.dtBindItem, True)
                                    Me.dgvWithdrawItem.DataSource = Me.dtBindItem
                                Else
                                    Me.dtBindItem.Merge(dt)
                                End If
                            End If
                            Me.dtBindItem.AcceptChanges()
                        End Using
                    Else
                        If Me.dtBindItem IsNot Nothing Then
                            Me.dtBindItem.AcceptChanges()
                            For Each dr As DataRow In Me.dtBindItem.Select(String.Format("Withdraw_No = '{0}'", Me.dtBind.Rows(e.RowIndex).Item("Withdraw_No").ToString()))
                                Me.dtBindItem.Rows.Remove(dr)
                            Next
                            Me.dtBindItem.AcceptChanges()
                        End If
                    End If
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmWithdrawSelected_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Me.key.Dispose()
        Me.allowData.Dispose()
        If Me.dtBind IsNot Nothing Then Me.dtBind.Dispose()
        If Me.dtBindItem IsNot Nothing Then Me.dtBindItem.Dispose()
    End Sub

    Private Sub dgvWithdraw_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvWithdraw.CellContentClick
        Try
            Select Case Me.dgvWithdraw.Columns(e.ColumnIndex).Name.ToUpper()
                Case "col_chkSelect".ToUpper()
                    Me.dgvWithdraw.CurrentCell = Me.dgvWithdraw.Rows(e.RowIndex).Cells(1)
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnOrderOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOrderOK.Click
        Try
            If Me.dtBindItem Is Nothing OrElse Me.dtBindItem.Rows.Count = 0 Then Exit Sub
            Me._isSelect = True
            Me.dgvWithdrawItem.CurrentCell = Me.dgvWithdrawItem.CurrentRow.Cells(1)
            Me.dtBindItem.AcceptChanges()
            Me._drReturn = Me.dtBindItem.Select("chk = true")
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub
End Class