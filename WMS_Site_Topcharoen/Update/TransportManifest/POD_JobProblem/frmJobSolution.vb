Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Master_Datalayer


Public Class frmJobSolution
    Public Process_id As Integer = 23
    Public SaveType As Integer = 0 '0 as Insert, 1 as Update

    Private _JobSolution_Id_Old As String
    Public Property JobSolution_Id_Old() As String
        Get
            Return _JobSolution_Id_Old
        End Get
        Set(ByVal value As String)
            _JobSolution_Id_Old = value
        End Set
    End Property


    Private _JobSolution_Index As String = ""
    Public Property JobSolution_Index() As String
        Get
            Return _JobSolution_Index
        End Get
        Set(ByVal value As String)
            _JobSolution_Index = value
        End Set
    End Property

    Private Sub frmJobSolution_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            

            AddProcessID()
            Select Case SaveType
                Case 0 'Save
                    Me.cboProcess.SelectedValue = Process_id

                Case 1 'Update
                    Dim objms_JobSolution As New ms_JobSolution(ms_JobSolution.enuOperation_Type.SEARCH)
                    objms_JobSolution.SearchJobSolution(JobSolution_Index)
                    Dim odtms_producttype As New DataTable
                    odtms_producttype = objms_JobSolution.DataTable

                    If odtms_producttype.Rows.Count > 0 Then
                        With odtms_producttype.Rows(0)
                            JobSolution_Id_Old = .Item("JobSolution_Id").ToString
                            JobSolution_Index = .Item("JobSolution_Index").ToString
                            txtID.Text = .Item("JobSolution_Id").ToString
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

            saveJobSolution(JobSolution_Index, txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text)

            ' Me.btnSave.Enabled = False

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Sub saveJobSolution(ByVal Index As String, ByVal ID As String, ByVal Process As Integer, ByVal Description As String)
        Try
            Select Case SaveType
                Case 0 'Add New

                    Dim objCheckIdms_JobSolution As New ms_JobSolution(ms_JobSolution.enuOperation_Type.SEARCH)
                    Dim BCheckId As Boolean = objCheckIdms_JobSolution.isChckID(txtID.Text)

                    If BCheckId Then
                        W_MSG_Information_ByIndex(45)
                        Me.btnSave.Enabled = True

                        Exit Sub
                    Else
                        Dim objms_JobSolution As New ms_JobSolution(ms_JobSolution.enuOperation_Type.ADDNEW)
                        objms_JobSolution.SaveData("", txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text)

                        W_MSG_Information_ByIndex(1)
                        Me.Close()
                    End If

                Case 1 'Update
                    If Me.txtID.Text.Trim = "" Then
                        W_MSG_Information("กรุณาป้อน" & lbID.Text)
                        Exit Sub
                    End If

                    Dim objms_JobSolution As New ms_JobSolution(ms_JobSolution.enuOperation_Type.UPDATE)
                    If Me.JobSolution_Id_Old <> txtID.Text.Trim Then
                        If objms_JobSolution.isExistID(txtID.Text) = True Then
                            W_MSG_Information_ByIndex(67)
                            Exit Sub
                        End If
                    End If
                    objms_JobSolution.SaveData(JobSolution_Index, txtID.Text, cboProcess.SelectedValue.ToString, txtDes.Text)

                    W_MSG_Information_ByIndex(1)
                    Me.Close()
            End Select
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

End Class