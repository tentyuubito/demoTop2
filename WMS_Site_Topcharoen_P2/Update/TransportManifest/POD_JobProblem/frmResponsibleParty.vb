Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer


Public Class frmResponsibleParty

    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _ResponsibleParty_Id_Old As String
    Public Property ResponsibleParty_Id_Old() As String
        Get
            Return _ResponsibleParty_Id_Old
        End Get
        Set(ByVal value As String)
            _ResponsibleParty_Id_Old = value
        End Set
    End Property


    Private _ResponsibleParty_Index As String = ""
    Public Property ResponsibleParty_Index() As String
        Get
            Return _ResponsibleParty_Index
        End Get
        Set(ByVal value As String)
            _ResponsibleParty_Index = value
        End Set
    End Property

    Private Sub frmResponsibleParty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
           

            AddProcessID()
            Select Case SaveType
                Case 0 'Save

                Case 1 'Update
                    Dim objms_ResponsibleParty As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.SEARCH)
                    objms_ResponsibleParty.SearchResponsibleParty(ResponsibleParty_Index)
                    Dim odtms_producttype As New DataTable
                    odtms_producttype = objms_ResponsibleParty.DataTable

                    If odtms_producttype.Rows.Count > 0 Then
                        With odtms_producttype.Rows(0)
                            ResponsibleParty_Id_Old = .Item("ResponsibleParty_Id").ToString
                            ResponsibleParty_Index = .Item("ResponsibleParty_Index").ToString
                            txtID.Text = .Item("ResponsibleParty_Id").ToString
                            txtDes.Text = .Item("Description").ToString
                            cboProcess.SelectedValue = .Item("Process_Id").ToString
                        End With
                    End If
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub AddProcessID()
        Dim objClassDB As New ms_Process(ms_Process.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.SearchData_Click("", "")
            objDT = objClassDB.DataTable
            cboProcess.DisplayMember = "process_Name"
            cboProcess.ValueMember = "Process_Id"
            cboProcess.DataSource = objDT
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If txtID.Text.Trim = "" Then
            '    W_MSG_Information_ByIndex(74)
            '    txtID.Focus()
            '    Exit Sub
            'End If

            If txtDes.Text.Trim = "" Then
                W_MSG_Information_ByIndex(75)
                txtDes.Focus()
                Exit Sub
            End If

            saveResponsibleParty(ResponsibleParty_Index, txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text)

            ' Me.btnSave.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub saveResponsibleParty(ByVal Index As String, ByVal ID As String, ByVal Process As Integer, ByVal Description As String)
        Try
            Select Case SaveType
                Case 0 'Add New

                    Dim objCheckIdms_ResponsibleParty As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.SEARCH)
                    Dim BCheckId As Boolean = objCheckIdms_ResponsibleParty.isChckID(txtID.Text)

                    If BCheckId Then
                        W_MSG_Information_ByIndex(45)
                        Me.btnSave.Enabled = True

                        Exit Sub
                    Else
                        Dim objms_ResponsibleParty As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.ADDNEW)
                        objms_ResponsibleParty.SaveData("", txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text)

                        W_MSG_Information_ByIndex(1)
                        Me.Close()
                    End If

                Case 1 'Update
                    If Me.txtID.Text.Trim = "" Then
                        W_MSG_Information("กรุณาป้อน" & lbID.Text)
                        Exit Sub
                    End If

                    Dim objms_ResponsibleParty As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.UPDATE)
                    If Me.ResponsibleParty_Id_Old <> txtID.Text.Trim Then
                        If objms_ResponsibleParty.isExistID(txtID.Text) = True Then
                            W_MSG_Information_ByIndex(67)
                            Exit Sub
                        End If
                    End If
                    objms_ResponsibleParty.SaveData(ResponsibleParty_Index, txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text)

                    W_MSG_Information_ByIndex(1)
                    Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class