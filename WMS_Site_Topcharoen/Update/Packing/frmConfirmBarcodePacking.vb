Imports WMS_STD_Master.W_Language

Public Class frmConfirmBarcodePacking

    Private _Data As DataTable
    Private _TotalQty As Integer

    Private _ItemCode_1 As String
    Private _ItemCode_2 As String
    Private _ItemStr10 As String

    Private _SalesOrderIndex, _SalesOrderItemIndex As String

    Public Property Data() As DataTable
        Get
            Return _Data
        End Get
        Set(ByVal value As DataTable)
            _Data = value
        End Set
    End Property

    Public Sub New(ByVal TotalQty As Integer, Optional ByVal SalesOrderIndex As String = "", Optional ByVal SalesOrderItemIndex As String = "", Optional ByVal SalesOrderNo As String = "", Optional ByVal SkuDescription As String = "", Optional ByVal ItemCode_1 As String = "", Optional ByVal ItemCode_2 As String = "", Optional ByVal ItemStr10 As String = "")

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        If TotalQty <= 0 Then
            Throw New Exception("TotalQty must greater than 0")
        End If

        _ItemCode_1 = ItemCode_1
        _ItemCode_2 = ItemCode_2
        _ItemStr10 = ItemStr10



        Me._TotalQty = TotalQty
        Me._SalesOrderIndex = IIf(SalesOrderIndex Is Nothing, String.Empty, SalesOrderIndex.Trim)
        Me._SalesOrderItemIndex = IIf(SalesOrderItemIndex Is Nothing, String.Empty, SalesOrderItemIndex.Trim)

        If SalesOrderNo IsNot Nothing AndAlso Not String.IsNullOrEmpty(SalesOrderNo.Trim) Then
            'Me.lblSO.Text = SalesOrderNo
        End If
        If SkuDescription IsNot Nothing AndAlso Not String.IsNullOrEmpty(SkuDescription.Trim) Then
            Me.lblSku.Text = SkuDescription
        End If
    End Sub

    Public Sub InitialProperty()
        Try
            If Me._Data Is Nothing Then
                Me._Data = New DataTable
                Me._Data.Columns.Add("SalesOrder_Index", GetType(String))
                Me._Data.Columns.Add("SalesOrderItem_Index", GetType(String))
                Me._Data.Columns.Add("Barcode", GetType(String))
            End If

            Me.grdData.AutoGenerateColumns = False
            Me.grdData.DataSource = Me._Data


        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Public Sub SetLabelSum()
        If Me._Data IsNot Nothing AndAlso Me._Data.Rows.Count >= 0 Then
            Me.lblSum.Text = String.Format("รวม : {0} / {1} Barcode", FormatNumber(Me._Data.Rows.Count, 0), FormatNumber(Me._TotalQty, 0))
            Me.btnDelete.Enabled = True
        Else
            Me.lblSum.Text = "ไม่พบ Barcode"
            Me.btnDelete.Enabled = False
        End If
    End Sub

    Private Sub frmConfirmBarcodePacking_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        InitialProperty()
        SetLabelSum()


    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        If W_MSG_Confirm("ยืนยันการออกจากหน้าจอ ใช่ หรือ ไม่") = Windows.Forms.DialogResult.Yes Then
            Me.Close()
        End If
    End Sub

    Private Sub txtBarcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcode.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Me.txtQty.Clear()
        Me.txtQty.Focus()
        'Me.AddItem()

    End Sub

    Sub AddItem()
        Try
            If Not IsNumeric(Me.txtQty.Text) Then
                W_MSG_Error("กรุณาระบุตัวเลข")
                Me.txtQty.Focus()
                Me.txtQty.SelectAll()
                exit Sub
            End If
            If Me.txtQty.Text <= 0 Then
                W_MSG_Error("กรุณาระบุตัวเลขมากกว่า 0")
                Me.txtQty.Focus()
                Me.txtQty.SelectAll()
                Exit Sub
            End If
            If Me.txtBarcode.Text.Trim = "" Then
                W_MSG_Error("กรุณาระบุ" & Me.lblBarcode.Text)
                Me.txtBarcode.Focus()
                Me.txtBarcode.SelectAll()
                Exit Sub
            End If

            For i As Integer = 0 To CInt(Me.txtQty.Text) - 1
                Me._Data.Rows.Add(Me._SalesOrderIndex, Me._SalesOrderItemIndex, Me.txtBarcode.Text.Trim)
            Next

            Me.txtBarcode.Clear()
            Me.txtQty.Clear()
            Me.txtBarcode.Focus()

            SetLabelSum()

            If Me._Data.Rows.Count = Me._TotalQty Then
                W_MSG_Information("สแกน Barcode เสร็จสิ้น")
                Me.DialogResult = Windows.Forms.DialogResult.OK
                Me.Close()
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        If Me.grdData.Rows.Count > 0 Then
            Dim Data As DataRow = Me.grdData.CurrentRow.DataBoundItem.Row()
            Data.Delete()

            Me._Data.AcceptChanges()
            SetLabelSum()

            Me.txtBarcode.Focus()
        End If
    End Sub

    Private Sub frmConfirmBarcodePacking_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        'Me.txtBarcode.Focus()

        Select Case _ItemStr10
            Case "L"
                Me.txtBarcode.Text = _ItemCode_1
            Case "R"
                Me.txtBarcode.Text = _ItemCode_2
            Case Else
                'Normal case
                Me.txtBarcode.Text = _ItemCode_1
        End Select

        If Me.txtBarcode.Text.Trim = "" Then
            Me.txtBarcode.Clear()
            Me.txtQty.Clear()
            Me.txtBarcode.Focus()
            Me.txtBarcode.SelectAll()
        Else
            Me.txtQty.Clear()
            Me.txtQty.Focus()
            Me.txtQty.SelectAll()
        End If

        'If (_ItemCode_1 = "" Or _ItemCode_2 = "") And (_ItemStr10 = "R" Or _ItemStr10 = "L") Then
        '    'Normal case
        '    Me.txtBarcode.Clear()
        '    Me.txtQty.Clear()
        '    Me.txtBarcode.Focus()
        '    Me.txtBarcode.SelectAll()
        'End If

    End Sub

    Private Sub txtQty_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtQty.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If

        Me.AddItem()

    End Sub
End Class

