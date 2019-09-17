Public Class frmPackSize_Popup


    Private _IsConfirm As Boolean
    Public Property IsConfirm() As Boolean
        Get
            Return _IsConfirm
        End Get
        Set(ByVal value As Boolean)
            _IsConfirm = value
        End Set
    End Property


    Private _PackSize_Index As String
    Public Property PackSize_Index() As String
        Get
            Return _PackSize_Index
        End Get
        Set(ByVal value As String)
            _PackSize_Index = value
        End Set
    End Property



    Private Sub frmPackSize_Popup_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me._IsConfirm = False
            Me._PackSize_Index = ""
            Me.getPackSize()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub getPackSize()
        Dim objPackSize As New WMS_STD_OUTB_Transport.tb_SalesOrderPacking
        Dim objDT As DataTable = New DataTable


        Try
            objDT = objPackSize.GetPackSize(" AND Status_id <> -1 ")
            With cboSizeBox
                .DisplayMember = "Description"
                .ValueMember = "Size_Index"
                .DataSource = objDT
            End With
            cboSizeBox.SelectedIndex = 0

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objPackSize = Nothing
        End Try
    End Sub

    Private Sub btnConfirm_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirm.Click
        Try
            Me._IsConfirm = True
            Me._PackSize_Index = Me.cboSizeBox.SelectedValue
            Me.Close()
        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub Barcode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtBarcode.KeyPress
        Try
            If e.KeyChar <> ChrW(13) Then
                Exit Sub
            End If

            Dim dtSource As New DataTable
            dtSource = CType(Me.cboSizeBox.DataSource, DataTable)

            Dim drArr() As DataRow
            drArr = dtSource.Select(String.Format("Barcode = '{0}'", Me.txtBarcode.Text.Trim.ToString))
            If drArr.Length = 0 Then
                Windows.Forms.MessageBox.Show("Barcode not matching !!!", "Validate", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Me.txtBarcode.SelectAll()
                Exit Sub
            Else
                Me._IsConfirm = True
                Me._PackSize_Index = Me.cboSizeBox.SelectedValue
                Me.Close()

            End If

        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub
End Class