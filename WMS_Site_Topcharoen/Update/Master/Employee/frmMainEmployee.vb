Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Formula
Imports System.Windows.Forms
Imports System.Configuration.ConfigurationSettings
Imports WMS_STD_Master
Public Class frmMainEmployee

    Private gintSaveType As Integer = 0 '0 as Insert, 1 as Update

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Sub ShowgrdEmployee()
        Dim objms_Employee As New ms_Employee(ms_Employee.enuOperation_Type.SEARCH)
        Try
            objms_Employee.SelectEmployee_Group()

            grdEmployee.DataSource = objms_Employee.DataTable

        Catch ex As Exception
            Throw ex
        Finally
            objms_Employee = Nothing
        End Try

    End Sub

    Private Sub frmEmployee_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 2006)
            oFunction.SW_Language_Column(Me, Me.grdEmployee, 2006)
          
            grdEmployee.AutoGenerateColumns = False
            ShowgrdEmployee()
            'Me.Icon = frmMain.Icon

            grdEmployee.Text = Me.Text
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Try
            Dim frmEm As New frmEmployee
            frmEm.SaveType = 0
            frmEm.ShowDialog()
            ShowgrdEmployee()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub btnUpdate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdate.Click
        Try
            If Me.grdEmployee.RowCount > 0 Then
                Dim frmEm As New frmEmployee
                frmEm.SaveType = 1
                frmEm.Employee_Index = grdEmployee.CurrentRow.Cells("colIndex").Value.ToString
                frmEm.ShowDialog()
            End If
            ShowgrdEmployee()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Try
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Dim objDB As New ms_Employee(ms_Employee.enuOperation_Type.DELETE)
                Dim Employee_Index As String = grdEmployee.CurrentRow.Cells("colIndex").Value.ToString
                objDB.Delete_Master(Employee_Index)
            End If
            ShowgrdEmployee()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub grdEmployee_CellDoubleClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdEmployee.CellDoubleClick
        btnUpdate_Click(sender, e)
    End Sub

    Private Sub frmMainEmployee_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try
            If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
                Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
                If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                    Dim frm As New WMS_STD_CONFIGURATION.frmConfigEmployee
                    frm.ShowDialog()
                    Dim oFunction As New W_Language
                    oFunction.SwitchLanguage(Me, 2006)
                    oFunction.SW_Language_Column(Me, Me.grdEmployee, 2006)
                    oFunction = Nothing
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class