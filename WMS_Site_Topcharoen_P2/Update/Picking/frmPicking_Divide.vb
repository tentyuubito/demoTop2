Imports WMS_std_master_datalayer
Imports WMS_Std_master.W_Language
Imports WMS_std_Formula
Imports WMS_STD_Master



Public Class frmPicking_Item

    Private clMsg As New Message

    Private _withdraw_index As String
    Private _Order_No As String
    Private _Sku_Id As String
    Private _Product_Name As String

    '--- PCK
    Private _QtyBalance As String
    Private _weightBalance As String
    Private _VolumeBalance As String

    '--- PCK OUT
    Private _QtyOut As String

    '--- ITEM BAL
    Private _QtyAllBal As String
    Private _WeightAllBal As String
    Private _VolumeAllBal As String


    '--- PCK Per ITEM

    Private _Qty_Per_Pck As String
    Private _weight_Per_Pck As String
    Private _Volume_Per_Pck As String


    Private tmpQtyPck As Double
    Private tmpVolPck As Double
    Private tmpWeightPck As Double
    Private tmpPrice As Double
    Private tmpQtyAll As Double
    Private _Edit As String

    '--- PRICE
    Private _PriceOut As String
    Private _QtyAll_Per_Pck As String
    Private _PriceBal As String


    Public Property Edit() As String
        Get
            Return _Edit
        End Get
        Set(ByVal value As String)
            _Edit = value
        End Set
    End Property

    Public Property Qty_Per_Pck() As String
        Get
            Return _Qty_Per_Pck
        End Get
        Set(ByVal value As String)
            _Qty_Per_Pck = value
        End Set
    End Property
    Public Property weight_Per_Pck() As String
        Get
            Return _weight_Per_Pck
        End Get
        Set(ByVal value As String)
            _weight_Per_Pck = value
        End Set
    End Property
    Public Property Volume_Per_Pck() As String
        Get
            Return _Volume_Per_Pck
        End Get
        Set(ByVal value As String)
            _Volume_Per_Pck = value
        End Set
    End Property

    Public Property WeightAllBal() As String
        Get
            Return _WeightAllBal
        End Get
        Set(ByVal value As String)
            _WeightAllBal = value
        End Set
    End Property

    Public Property VolumeAllBal() As String
        Get
            Return _VolumeAllBal
        End Get
        Set(ByVal value As String)
            _VolumeAllBal = value
        End Set
    End Property

    Public Property QtyAllBal() As String
        Get
            Return _QtyAllBal
        End Get
        Set(ByVal value As String)
            _QtyAllBal = value
        End Set
    End Property
    'Public Property QtyAllOut() As String
    '    Get
    '        Return _QtyAllOut
    '    End Get
    '    Set(ByVal value As String)
    '        _QtyAllOut = value
    '    End Set
    'End Property
    Public Property weightBalance() As String
        Get
            Return _weightBalance
        End Get
        Set(ByVal value As String)
            _weightBalance = value
        End Set
    End Property

    Public Property VolumeBalance() As String
        Get
            Return _VolumeBalance
        End Get
        Set(ByVal value As String)
            _VolumeBalance = value
        End Set
    End Property


    Public Property QtyBalance() As String
        Get
            Return _QtyBalance
        End Get
        Set(ByVal value As String)
            _QtyBalance = value
        End Set
    End Property


    Public Property QtyOut() As String
        Get
            Return _QtyOut
        End Get
        Set(ByVal value As String)
            _QtyOut = value
        End Set
    End Property

    Public Property PriceOut() As String
        Get
            Return _PriceOut
        End Get
        Set(ByVal value As String)
            _PriceOut = value
        End Set
    End Property
    Public Property PriceBal() As String
        Get
            Return _PriceBal
        End Get
        Set(ByVal value As String)
            _PriceBal = value
        End Set
    End Property


    Public Property Product_Name() As String
        Get
            Return _Product_Name
        End Get
        Set(ByVal value As String)
            _Product_Name = value
        End Set
    End Property


    Public Property Sku_Id() As String
        Get
            Return _Sku_Id
        End Get
        Set(ByVal value As String)
            _Sku_Id = value
        End Set
    End Property

    Public Property Order_No() As String
        Get
            Return _Order_No
        End Get
        Set(ByVal value As String)
            _Order_No = value
        End Set
    End Property

    Public Property Withdraw_Index() As String
        Get
            Return _withdraw_index
        End Get
        Set(ByVal value As String)
            _withdraw_index = value
        End Set
    End Property

    Private _drArrSelect() As DataRow
    Public Property drArrSelect() As DataRow()
        Get
            Return _drArrSelect
        End Get
        Set(ByVal value As DataRow())
            _drArrSelect = value
        End Set
    End Property

    Private Sub frmWithDrawItem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            txtQtyOut.Focus()

            Dim oFunction As New W_Language

            'Insert
            'oFunction.Insert(Me)

            'SwitchLanguage
            oFunction.SwitchLanguage(Me)


            For Each drRow As DataRow In drArrSelect
                tmpQtyPck = drRow("Qty_Recieve_Package")
                tmpQtyAll = drRow("Qty_Item_Bal")
                tmpVolPck = drRow("Volume_Bal")
                tmpWeightPck = drRow("Weight_Bal")

                txtPerQty.Text = drRow("Qty_Per_Pck")
                txtPerVolume.Text = drRow("Volume_Per_Pck")
                txtPerWeight.Text = drRow("weight_Per_Pck")

                txtOrder_No.Text = drRow("Order_No")
                txtSku_Id.Text = drRow("Sku_Description")
                txtProductName.Text = drRow("Sku_Id")

                'txtQtyBalance.Text = drRow("Qty_bal")
                'txtWeight_Bal.Text = drRow("Weight_Bal")
                'txtVolume_Bal.Text = drRow("Volume_Bal")
                'txtQtyAll_Bal.Text = drRow("Qty_Item_Bal")
                'txtPriceBall.Text = 0
                txtQtyOut.Text = 0

                txtPriceOut.Text = drRow("OrderItem_Price_Bal")
                tmpPrice = Val(txtPriceOut.Text) / tmpQtyPck

                _QtyAll_Per_Pck = tmpQtyAll / tmpQtyPck

                txtQty.Text = drRow("Qty_bal")
                txtWeight.Text = drRow("Weight_Bal")
                txtVolume.Text = drRow("Volume_Bal")
                txtQtyAll.Text = drRow("Qty_Item_Bal")
                txtPrice.Text = 0

                txtQtyBalance.Text = 0
                txtWeight_Bal.Text = 0
                txtVolume_Bal.Text = 0
                txtQtyAll_Bal.Text = 0
                txtPriceBall.Text = 0


            Next

            'tmpQtyPck = _QtyBalance
            'tmpQtyAll = _QtyAllBal
            'tmpVolPck = _VolumeBalance
            'tmpWeightPck = _weightBalance
            'tmpPrice = _PriceOut / _QtyBalance
            '_QtyAll_Per_Pck = _QtyAllBal / _QtyBalance

            'txtPerQty.Text = _Qty_Per_Pck
            'txtPerVolume.Text = _Volume_Per_Pck
            'txtPerWeight.Text = _weight_Per_Pck

            'txtOrder_No.Text = _Order_No
            'txtSku_Id.Text = _Sku_Id
            'txtProductName.Text = _Product_Name

            'txtQtyBalance.Text = _QtyBalance
            'txtWeight_Bal.Text = _weightBalance
            'txtVolume_Bal.Text = _VolumeBalance
            'txtQtyAll_Bal.Text = _QtyAllBal

            'txtQtyOut.Text = _QtyOut
            'txtPriceBall.Text = 0
            'txtPriceOut.Text = _PriceOut

            ''''''''''''''''
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' Add for check 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>Add by: TOP 21/03/2013
    ''' </remarks>
    Private Sub btn_select_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_select.Click
        Try
            '--- Message ขอ Hard Code ไปก่อนนะครับ ถ้าใครมีโอกาศเข้ามา แก้ให้ด้วยนะครับ แฮ่ ๆ ๆ


            If CInt(txtQtyOut.Text.Trim) = 0 Then
                _Edit = 0
                Me.Close()
            Else

                If Val(txtWeight_Bal.Text) < 0 Then
                    W_MSG_Information_ByIndex("400061")
                    Exit Sub
                End If

                If Val(txtVolume_Bal.Text) < 0 Then
                    W_MSG_Information_ByIndex("400062")
                    Exit Sub
                End If
                If Val(txtQtyAll_Bal.Text) < 0 Then
                    W_MSG_Information_ByIndex("400063")
                    Exit Sub
                End If
                If Val(txtPriceBall.Text) < 0 Then
                    W_MSG_Information_ByIndex("400064")
                    Exit Sub
                End If

                _Edit = 1
                Me.Close()
            End If




        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub


    Private Sub txtQtyOut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyOut.TextChanged
        Try
            'txtQtyBalance.Text = tmpQtyPck - Val(txtQtyOut.Text)

            'txtWeightOut.Text = Val(txtQtyOut.Text) * _weight_Per_Pck
            'txtVolumeOut.Text = Val(txtQtyOut.Text) * _Volume_Per_Pck

            ''txtQtyAllOut.Text = Val(txtQtyOut.Text) * _Qty_Per_Pck
            ''   txtQtyAllOut.Text = _QtyAllBal - Val(txtQtyAllOut.Text)
            'txtQtyAllOut.Text = Val(_QtyAll_Per_Pck * Val(txtQtyOut.Text))

            'txtPriceOut.Text = tmpPrice * Val(txtQtyOut.Text)
            ''txtPerQty.Text = Val(txtPriceOut.Text) * _Qty_Per_Pck

            'If Val(txtQtyOut.Text) = 0 Or txtQtyOut.Text = "" Then
            '    txtWeight_Bal.Text = tmpWeightPck
            '    txtVolume_Bal.Text = tmpVolPck
            '    txtQtyBalance.Text = tmpQtyPck
            '    txtQtyAll_Bal.Text = _QtyAllBal
            '    txtPriceBall.Text = _PriceOut
            'End If
            ''''''''''''''''''''''''''''''''''''''''''''''
            If Val(txtQtyOut.Text) = 0 Or txtQtyOut.Text = "" Then
                txtQtyBalance.Text = 0
                txtWeight_Bal.Text = 0

                txtQtyAll_Bal.Text = 0
                txtPriceBall.Text = 0
                txtWeightOut.Text = 0
                txtVolumeOut.Text = 0
                txtQtyAllOut.Text = 0
                txtPriceOut.Text = 0
                txtVolume_Bal.Text = 0

            ElseIf Val(txtQtyOut.Text) > Val(txtQty.Text) Then
                W_MSG_Error_ByIndex("500020")
                txtQtyOut.Text = 0
                txtVolume_Bal.Text = 0
            Else
                txtQtyBalance.Text = Format(Val(txtQty.Text) - Val(txtQtyOut.Text), "###0.####")
                txtWeight_Bal.Text = Format(Val(txtWeight.Text) - Val(txtPerWeight.Text * txtQtyOut.Text), "###0.####")
                txtVolume_Bal.Text = Format(Val(txtVolume.Text) - Val(txtPerVolume.Text * txtQtyOut.Text), "###0.####")
                txtWeightOut.Text = Format(Val(txtPerWeight.Text * txtQtyOut.Text), "###0.####")
                txtVolumeOut.Text = Format(Val(txtPerVolume.Text * txtQtyOut.Text), "###0.####")

                txtQtyAllOut.Text = Format((CDbl(txtQtyAll.Text) / CDbl(txtQty.Text)) * CDbl(txtQtyOut.Text), "###0.####")
                txtQtyAll_Bal.Text = Format(CDbl(txtQtyAll.Text) - CDbl(txtQtyAllOut.Text), "###0.####")
                txtPriceOut.Text = Format((CDbl(txtPrice.Text) / CDbl(txtQty.Text)) * CDbl(txtQtyOut.Text), "###0.####")

            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try


    End Sub

    Private Sub btn_cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_cancel.Click
        Try
            _Edit = 0
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtWeightOut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeightOut.TextChanged
        Try
            txtWeight_Bal.Text = Format(tmpWeightPck - Val(txtWeightOut.Text), "###0.####")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtVolumeOut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtVolumeOut.TextChanged
        Try
            txtVolume_Bal.Text = Format(tmpVolPck - Val(txtVolumeOut.Text), "###0.####")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub txtQtyAllOut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtQtyAllOut.TextChanged
        Try
            txtQtyAll_Bal.Text = Format(_QtyAllBal - Val(txtQtyAllOut.Text), "###0.####")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPriceOut_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPriceOut.TextChanged
        Try
            txtPriceBall.Text = Format(_PriceOut - Val(txtPriceOut.Text), "###0.####")
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtQtyOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyOut.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtQtyOut, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtWeightOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWeightOut.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtWeightOut, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtVolumeOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtVolumeOut.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtVolumeOut, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtQtyAllOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQtyAllOut.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtQtyAllOut, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtPriceOut_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPriceOut.KeyPress
        Try
            e.Handled = CurrencyTextBox.CurrencyOnly(txtPriceOut, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtWeightOut_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtWeightOut.Leave, txtVolumeOut.Leave, txtQtyAllOut.Leave, txtPriceOut.Leave, txtQtyOut.Leave
        Try
            Dim txtbox As New TextBox
            txtbox = CType(sender, TextBox)
            If txtbox.Text = "" Then
                txtbox.Text = 0
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class