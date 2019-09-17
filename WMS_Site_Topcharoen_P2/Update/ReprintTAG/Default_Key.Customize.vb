Partial Class Default_Key
    Public Delegate Sub DelegateKey(ByVal sender As Object, ByVal e As System.EventArgs)
    Private delegate_key As DelegateKey
    Private key As Keys

    Public Function addKeyEvent(ByVal frm As Form, ByVal key As Keys, ByVal delegate_key As DelegateKey) As Boolean
        Try
            Me.delegate_key = delegate_key
            Me.key = key
            For Each ctrl As Control In frm.Controls
                If TypeOf ctrl Is GroupBox Then
                    Me.addEventGroupBox(ctrl, Me.delegate_key)
                End If
                AddHandler ctrl.KeyDown, AddressOf KeyDownEvent
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub addEventGroupBox(ByVal gb As GroupBox, ByVal delegate_key As DelegateKey)
        Try
            For Each ctrl As Control In gb.Controls
                If TypeOf ctrl Is GroupBox Then
                    Me.addEventGroupBox(ctrl, Me.delegate_key)
                End If
                AddHandler ctrl.KeyDown, AddressOf KeyDownEvent
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub KeyDownEvent(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        Try
            If e.KeyCode = key Then Me.delegate_key(New Object, New System.EventArgs)
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
End Class
