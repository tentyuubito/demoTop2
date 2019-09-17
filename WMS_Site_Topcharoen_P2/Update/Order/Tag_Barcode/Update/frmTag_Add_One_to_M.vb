Imports WMS_STD_Master
Imports WMS_STD_INB_Receive_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports System.Data
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer

Public Class frmTag_Add_One_to_M

    Private _Confirm As Boolean = False
    Public Property Confirm() As Boolean
        Get
            Return _Confirm
        End Get
        Set(ByVal value As Boolean)
            _Confirm = value
        End Set
    End Property


    Private _Qty As Double
    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property

    Private _Volume As Double
    Public Property Volume() As Double
        Get
            Return _Volume
        End Get
        Set(ByVal value As Double)
            _Volume = value
        End Set
    End Property

    Private _Weight As Double
    Public Property Weight() As Double
        Get
            Return _Weight
        End Get
        Set(ByVal value As Double)
            _Weight = value
        End Set
    End Property


    Private _DataSource As New DataTable
    Public Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value
        End Set
    End Property

    Private Sub frmTag_Add_One_to_M_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtQTY.Text = FormatNumber(Me._Qty, 4)
            Me.txtWeight.Text = FormatNumber(Me._Weight, 4)
            Me.txtVolume.Text = FormatNumber(Me._Volume, 4)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnGenTAG_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenTAG.Click
        Try
            Me._Confirm = False
            If txtQTY.Text.Trim = "" Or IsNumeric(txtQTY.Text) = False Then
                W_MSG_Information_ByIndex(77)
                txtQTY.Focus()
                Exit Sub
            End If

            Dim odtTag As New DataTable
            With odtTag.Columns
                If Not .Contains("Qty_per_TAG") Then .Add("Qty_per_TAG", GetType(Double))
                If Not .Contains("Weight_per_TAG") Then .Add("Weight_per_TAG", GetType(Double))
                If Not .Contains("Volume_per_TAG") Then .Add("Volume_per_TAG", GetType(Double))
                If Not .Contains("LPN_NO") Then .Add("LPN_NO", GetType(String))
            End With


            Dim dblSumQty As Double = 0
            Dim Amt As Double = 0.0

            Dim countTag As Integer = 0
            Dim callcountTag As Double = 0

            Dim Weight_Per_TAG, Volume_Per_TAG As Double
            Weight_Per_TAG = CDbl(txtWeight.Text) / CDbl(txtQTY.Text) 'FormatNumber(CDbl(txtWeight.Text) / CDbl(txtQTY.Text), 4)
            Volume_Per_TAG = CDbl(txtVolume.Text) / CDbl(txtQTY.Text) 'FormatNumber(CDbl(txtVolume.Text) / CDbl(txtQTY.Text), 4)


            If rdPerTag.Checked Then
                '----------------------------------------------------------
                Dim bError As Boolean = False
                If Not IsNumeric(txtPerTag.Text) Then
                    bError = True
                Else
                    If CDbl(txtPerTag.Text) > CDbl(txtQTY.Text) Then
                        bError = True
                    End If
                End If
                If bError Then
                    W_MSG_Information("กรุณาตรวจสอบ " & Me.rdPerTag.Text)
                    txtPerTag.Focus()
                    Exit Sub
                End If
                '----------------------------------------------------------
                callcountTag = CDbl(txtQTY.Text) / CDbl(txtPerTag.Text)
                countTag = Math.Ceiling(callcountTag)

                Amt = CDbl(txtPerTag.Text)
                '-------------------------------------------------------------

                '-------------------------------------------------------------
                For i As Integer = 1 To countTag 'CInt(txtCountTag.Text)
                    Dim odrTag As DataRow
                    odrTag = odtTag.NewRow
                    odrTag("Qty_per_TAG") = FormatNumber(Amt, 4)
                    odrTag("Weight_per_TAG") = FormatNumber(Weight_Per_TAG * Amt, 4)
                    odrTag("Volume_per_TAG") = FormatNumber(Volume_Per_TAG * Amt, 4)

                    If i = countTag Then
                        odrTag("Qty_per_TAG") = FormatNumber((Me.txtQTY.Text - dblSumQty), 4)
                        odrTag("Weight_per_TAG") = FormatNumber(odrTag("Qty_per_TAG") * Weight_Per_TAG, 4)
                        odrTag("Volume_per_TAG") = FormatNumber(odrTag("Qty_per_TAG") * Volume_Per_TAG, 4)
                    Else
                        dblSumQty += FormatNumber(Amt, 4)
                    End If

                    odtTag.Rows.Add(odrTag)
                Next
                '-------------------------------------------------------------

            ElseIf rdTag.Checked Then
                '----------------------------------------------------------
                Dim bError As Boolean = False
                If (Not IsNumeric(txtCountTag.Text)) Then
                    bError = True
                Else
                    If CDbl(txtCountTag.Text) > CDbl(txtQTY.Text) Then
                        bError = True
                    End If
                End If

                If bError Then
                    W_MSG_Information("กรุณาตรวจสอบ " & Me.rdPerTag.Text)
                    txtPerTag.Focus()
                    Exit Sub
                End If
                '----------------------------------------------------------
                countTag = txtCountTag.Text
                callcountTag = Math.Floor(CDbl(txtQTY.Text) / CDbl(txtCountTag.Text)) 'ปัดลง
                'callcountTag = Math.Ceiling(CDbl(txtQTY.Text) / CDbl(txtCountTag.Text)) 'ปัดขึ้น
                Amt = callcountTag

                For i As Integer = 1 To countTag 'CInt(txtCountTag.Text)
                    Dim odrTag As DataRow
                    odrTag = odtTag.NewRow
                    odrTag("Qty_per_TAG") = FormatNumber(Amt, 4)
                    odrTag("Weight_per_TAG") = FormatNumber(Weight_Per_TAG * Amt, 4)
                    odrTag("Volume_per_TAG") = FormatNumber(Volume_Per_TAG * Amt, 4)

                    If i = countTag Then
                        odrTag("Qty_per_TAG") = FormatNumber((Me.txtQTY.Text - dblSumQty), 4)
                        odrTag("Weight_per_TAG") = FormatNumber(odrTag("Qty_per_TAG") * Weight_Per_TAG, 4)
                        odrTag("Volume_per_TAG") = FormatNumber(odrTag("Qty_per_TAG") * Volume_Per_TAG, 4)
                    Else
                        dblSumQty += FormatNumber(Amt, 4)
                    End If
                    odtTag.Rows.Add(odrTag)
                Next

                If CDbl(odtTag.Rows(odtTag.Rows.Count - 1)("Qty_per_TAG")) > CDbl(odtTag.Rows(odtTag.Rows.Count - 2)("Qty_per_TAG")) Then
                    Dim Qty_Per_Tag As Double = odtTag.Rows(0)("Qty_per_TAG")
                    Dim Qty_Other As Double = CDbl(odtTag.Rows(odtTag.Rows.Count - 1)("Qty_per_TAG")) - CDbl(odtTag.Rows(odtTag.Rows.Count - 2)("Qty_per_TAG"))
                    odtTag.Rows(odtTag.Rows.Count - 1)("Qty_per_TAG") = FormatNumber(Qty_Per_Tag, 4)
                    odtTag.Rows(odtTag.Rows.Count - 1)("Weight_per_TAG") = FormatNumber(Weight_Per_TAG * Qty_Per_Tag, 4)
                    odtTag.Rows(odtTag.Rows.Count - 1)("Volume_per_TAG") = FormatNumber(Volume_Per_TAG * Qty_Per_Tag, 4)
                    For Each dr As DataRow In odtTag.Rows
                        dr("Qty_per_TAG") = FormatNumber(dr("Qty_per_TAG") + 1, 4)
                        dr("Weight_per_TAG") = FormatNumber(Weight_Per_TAG * dr("Qty_per_TAG"), 4)
                        dr("Volume_per_TAG") = FormatNumber(Volume_Per_TAG * dr("Qty_per_TAG"), 4)
                        Qty_Other -= 1
                        If Qty_Other <= 0 Then Exit For
                    Next
                End If
            Else
                MsgBox("กรุณาระบุจำนวน TAG")
                Exit Sub
            End If


            Me.grdTag.DataSource = odtTag



            If Me.grdTag.RowCount > 0 Then
                Me.lbCountRowsOrder.Text = "จำนวน " & Me.grdTag.RowCount & " รายการ"
            Else
                Me.lbCountRowsOrder.Text = "ไม่พบรายการ"
            End If

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Function checkNullLPN() As Boolean
        Try
            For Each dtrow As DataRow In Me._DataSource.Rows
                If IsDBNull(dtrow.Item("LPN_NO")) OrElse String.IsNullOrEmpty(dtrow.Item("LPN_NO")) Then

                    dtrow.Item("LPN_NO") = ""

                End If
            Next

            Return True

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            If Me.grdTag.RowCount > 0 Then
                Dim dblSumQtyPerTag As Double = 0
                For iRow As Integer = 0 To grdTag.Rows.Count - 1
                    dblSumQtyPerTag += CDbl(grdTag.Rows(iRow).Cells("col_Qty_per_TAG").Value)
                Next

                If dblSumQtyPerTag <= 0 Then
                    W_MSG_Information("จำนวนต่อ TAG ต้องมากกว่า 0") ' เปลียนเป็น จำนวนรับเข้า ไม่ถูกต้อง
                    Exit Sub
                ElseIf dblSumQtyPerTag.ToString("0.0000") <> CDbl(txtQTY.Text).ToString("0.0000") Then
                    W_MSG_Information("จำนวนใน TAG ไม่ตรงกับ จำนวนสินค้า") ' เปลียนเป็น จำนวนรับเข้า ไม่ถูกต้อง
                    Exit Sub
                End If


                CType(Me.grdTag.DataSource, DataTable).AcceptChanges()
            

                Me._DataSource = CType(Me.grdTag.DataSource, DataTable)

                checkNullLPN()

                Me._Confirm = True
            Else
                Me._Confirm = False
            End If
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub


    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me._Confirm = False
        Me.Close()

    End Sub

    Private Sub txtPerTag_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPerTag.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnGenTAG_Click(sender, New EventArgs)
        End If

    End Sub

    Private Sub txtCountTag_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtCountTag.KeyDown
        If e.KeyCode = Keys.Enter Then
            btnGenTAG_Click(sender, New EventArgs)
        End If

    End Sub

    Private Sub rdPerTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdPerTag.Click
        txtPerTag.Enabled = True
        txtCountTag.Enabled = False
    End Sub

    Private Sub rdTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdTag.Click
        txtCountTag.Enabled = True
        txtPerTag.Enabled = False
    End Sub

    Private Sub grdTag_CellEndEdit(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdTag.CellEndEdit


        If e.RowIndex < 0 Then Exit Sub

        Try
            Select Case e.ColumnIndex
                Case grdTag.Columns("col_Qty_per_TAG").Index

                    Dim Qty_Per_TAG, Weight_Per_Item, Volume_Per_Item, Weight_Per_TAG, Volume_per_TAG As Double

                    With grdTag.Rows(grdTag.CurrentRow.Index)

                        If .Cells("col_Qty_per_TAG").Value.ToString = "" Or Not IsNumeric(.Cells("col_Qty_per_TAG").Value) Then
                            .Cells("col_Qty_per_TAG").Value = FormatNumber(0, 4)
                            .Cells("col_Weight_per_TAG").Value = FormatNumber(0, 4)
                            .Cells("col_Volume_per_TAG").Value = FormatNumber(0, 4)
                            Exit Sub
                        End If

                        Qty_Per_TAG = .Cells("col_Qty_per_TAG").Value
                        Weight_Per_Item = CDbl(txtWeight.Text) / CDbl(txtQTY.Text)
                        Volume_Per_Item = CDbl(txtVolume.Text) / CDbl(txtQTY.Text)

                        Weight_Per_TAG = Qty_Per_TAG * Weight_Per_Item
                        Volume_per_TAG = Qty_Per_TAG * Volume_Per_Item

                        .Cells("col_Qty_per_TAG").Value = Qty_Per_TAG.ToString("0.0000")
                        .Cells("col_Weight_per_TAG").Value = Weight_Per_TAG.ToString("0.0000")
                        .Cells("col_Volume_per_TAG").Value = Volume_per_TAG.ToString("0.0000")

                    End With
                Case grdTag.Columns("LPN_NO").Index
                    Exit Sub
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try

    End Sub


End Class