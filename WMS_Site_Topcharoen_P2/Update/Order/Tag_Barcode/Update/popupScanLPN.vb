Imports WMS_STD_Master.W_Language
Public Class popupScanLPN


    Public strLPN As String = ""
    Private Sub popupScanLPN_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.txtLPN.Focus()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub txtLPN_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLPN.KeyPress
        If e.KeyChar <> ChrW(13) Then
            Exit Sub
        End If
        Try

            If txtLPN.Text.Trim().Length() > 0 Then
                If txtLPN.Text.Trim().Length() <> "13" Then
                    W_MSG_Error("กรอกให้ครบ 13 ตัว")
                    Exit Sub
                Else
                    strLPN = txtLPN.Text.Trim()
                    Me.Close()
                End If
            Else
                strLPN = txtLPN.Text.Trim()
                Me.Close()
            End If

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub
End Class