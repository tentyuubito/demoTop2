Public Class Form1

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim a As New OMS_Interface
        a.GetMaster(OMS_Interface.eOMSMasterID.Product)
    End Sub
End Class