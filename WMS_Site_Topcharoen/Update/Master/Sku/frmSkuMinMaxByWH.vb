Imports System.Data.SqlClient
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module

Public Class frmSkuMinMaxByWH

    Public Sub New(ByVal Sku_Index As String)
        InitializeComponent()
        Me._Sku_Index = Sku_Index
    End Sub

    Private _Sku_Index As String

    Private Sub frmSkuMinMaxByWH_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim oFunction As New W_Language
        'oFunction.SwitchLanguage(Me, Nothing)
        ''oFunction.SW_Language_Column(Me, Me.dgvData, Nothing)
        'oFunction = Nothing

        Try
            Me.defaultOnLoad()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Me.Close()
        End Try
    End Sub

    Private Sub defaultOnLoad()
        Try
            Me.dgvData.AutoGenerateColumns = False
            Me.getData()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub getData()
        Try
            Dim _dt As DataTable = New clsSkuMinMaxByWH().getSkuMinMaxByWH(Me._Sku_Index)
            Me.dgvData.DataSource = _dt
            Me.dgvData.ReadOnly = False
            If (_dt.Rows.Count() = 0) Then
                Me.txtSku_Id.Tag = "N/A"
                Me.txtSku_Id.Text = "N/A"
                Me.txtSku_Desc.Text = "N/A"
                Me.txtMinSku_Qty.Text = "N/A"
                Me.txtMinSku_Package.Text = "N/A"
                Me.txtMaxSku_Qty.Text = "N/A"
                Me.txtMaxSku_Package.Text = "N/A"
                Me.lblData_Count.Text = "ไม่พบข้อมูล"
                W_MSG_Information("ไม่พบข้อมูล")
            Else
                Me.txtSku_Id.Tag = _dt.Rows(0).Item("Sku_Index").ToString()
                Me.txtSku_Id.Text = _dt.Rows(0).Item("Sku_Id").ToString()
                Me.txtSku_Desc.Text = _dt.Rows(0).Item("Sku_Desc").ToString()
                Me.txtMinSku_Qty.Text = "N/A"
                Me.txtMinSku_Package.Text = _dt.Rows(0).Item("Package_Desc").ToString()
                Me.txtMaxSku_Qty.Text = "N/A"
                Me.txtMaxSku_Package.Text = _dt.Rows(0).Item("Package_Desc").ToString()
                Me.lblData_Count.Text = String.Format("พบข้อมูล : {0}", _dt.Rows.Count().ToString("#,##0"))
                Me.calcSumData(DirectCast(Me.dgvData.DataSource, DataTable))
                Me.setDgvDataStyle(Me.dgvData)
            End If
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub calcSumData(ByVal dt As DataTable)
        Try
            Dim _SumMinSku_Qty As Decimal = 0
            Dim _SumMaxSku_Qty As Decimal = 0
            If (dt.Rows.Count() > 0) Then
                _SumMinSku_Qty = dt.Compute(" sum(Min_Qty) ", " Min_Qty>=0 ")
                _SumMaxSku_Qty = dt.Compute(" sum(Max_Qty) ", " Max_Qty>=0 ")
            End If
            Me.txtMinSku_Qty.Text = _SumMinSku_Qty.ToString("#,##0.######")
            Me.txtMaxSku_Qty.Text = _SumMaxSku_Qty.ToString("#,##0.######")
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            DirectCast(Me.dgvData.DataSource, DataTable).AcceptChanges()
            Dim _IsSaved As Boolean = New clsSkuMinMaxByWH().saveSkuMinMaxByWH(DirectCast(Me.dgvData.DataSource, DataTable))
            If (_IsSaved) Then
                MessageBox.Show("บันทึกข้อมูลเสร็จสิ้น", Me.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Information)
                Me.Close()
            Else
                MessageBox.Show("ไม่สามารถบันทึกข้อมูลได้, กรุณาลองใหม่", Me.Text.Trim(), MessageBoxButtons.OK, MessageBoxIcon.Stop)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Try
            Me.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region " DGV Data "

    Private Sub dgvData_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvData.CellValueChanged
        Try
            If (e.ColumnIndex <= -1 Or e.RowIndex <= -1) Then
                Exit Sub
            End If

            Dim _dgv As DataGridView = CType(sender, DataGridView)
            With _dgv
                If (.RowCount < 0) Then Exit Sub
                Select Case .Columns(e.ColumnIndex).Name
                    Case "col_Min_Qty", "col_Max_Qty"
                        If (Not IsNumeric(.Rows(e.RowIndex).Cells(.Columns(e.ColumnIndex).Name).Value)) Then
                            .Rows(e.RowIndex).Cells(.Columns(e.ColumnIndex).Name).Value = "0"
                        End If
                        If (CDec(.Rows(e.RowIndex).Cells(.Columns(e.ColumnIndex).Name).Value) < 0) Then
                            .Rows(e.RowIndex).Cells(.Columns(e.ColumnIndex).Name).Value = "0"
                        End If

                        Dim _Temp_Qty As Decimal = 0
                        Decimal.TryParse(.Rows(e.RowIndex).Cells(.Columns(e.ColumnIndex).Name).Value, _Temp_Qty)
                        Decimal.TryParse(_Temp_Qty.ToString(.Columns(e.ColumnIndex).DefaultCellStyle.Format), _Temp_Qty)
                        .Rows(e.RowIndex).Cells(.Columns(e.ColumnIndex).Name).Value = _Temp_Qty

                End Select
            End With
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub dgvData_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvData.EditingControlShowing
        Try
            If (Me.dgvData.Columns(Me.dgvData.CurrentCell.ColumnIndex).Name = "col_Min_Qty") Then
                RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Min_Qty_Keypress
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Min_Qty_Keypress
            ElseIf (Me.dgvData.Columns(Me.dgvData.CurrentCell.ColumnIndex).Name = "col_Max_Qty") Then
                RemoveHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Max_Qty_Keypress
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf col_Max_Qty_Keypress
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub col_Min_Qty_Keypress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Try
            If Me.dgvData.CurrentCell.ColumnIndex = Me.dgvData.Columns("col_Min_Qty").Index Then
                If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = "." Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = ChrW(Keys.Delete)) Then e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub col_Max_Qty_Keypress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        Try
            If Me.dgvData.CurrentCell.ColumnIndex = Me.dgvData.Columns("col_Max_Qty").Index Then
                If Not (Char.IsDigit(CChar(CStr(e.KeyChar))) Or e.KeyChar = "." Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = ChrW(Keys.Delete)) Then e.Handled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub dgvData_CellValidating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellValidatingEventArgs) Handles dgvData.CellValidating
        Try
            If (Not (Me.dgvData.Columns(e.ColumnIndex).Name = "col_Min_Qty" Or Me.dgvData.Columns(e.ColumnIndex).Name = "col_Max_Qty")) Then Exit Sub
            If (Not IsNumeric(e.FormattedValue)) Then
                e.Cancel = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub setDgvDataStyle(ByVal dgv As DataGridView)
        Try
            For Each _dgvr As DataGridViewRow In dgv.Rows
                Dim _Running_Index As String = _dgvr.Cells("col_Running_Index").Value.ToString()
                If (_Running_Index = Nothing) Then
                    _dgvr.Cells("col_Warehouse_Desc").Style.BackColor = Nothing
                Else
                    _dgvr.Cells("col_Warehouse_Desc").Style.BackColor = Drawing.Color.LightGreen
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class