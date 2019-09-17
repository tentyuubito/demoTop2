Imports System.Data.SqlClient
Imports WMS_STD_Formula
Imports System.Text
Public Class frmUpdateBranch
#Region "Property"


    Private _SourceConfig As String
    Public Property SourceConfig() As String
        Get
            Return _SourceConfig
        End Get
        Set(ByVal value As String)
            _SourceConfig = value
        End Set
    End Property
    Private _Branch_Id As String
    Public Property Branch_Id() As String
        Get
            Return _Branch_Id
        End Get
        Set(ByVal value As String)
            _Branch_Id = value
        End Set
    End Property
    Private _Version As String
    Public Property Version() As String
        Get
            Return _Version
        End Get
        Set(ByVal value As String)
            _Version = value
        End Set
    End Property



#End Region


    Dim cmd As New SqlCommand
    Dim dat As New SqlDataAdapter
    Dim con As New SqlConnection

    Private Sub frmUpdateBranch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            GetData()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Sub GetData()

        txtSourceAppConfig.Text = SourceConfig
        con.ConnectionString = SourceConfig

        Dim dt As New DataTable
        cmd.CommandText = String.Format("SELECT TOP 1 * from ms_branch where Branch_Id = {0}", _Branch_Id)
        cmd.CommandType = CommandType.Text
        cmd.Connection = con
        dat.SelectCommand = cmd
        dat.Fill(dt)

        If dt.Rows.Count > 0 Then


            With dt.Rows(0)
                txtBranchName.Text = .Item("Branch_Name")
                txtBranchDB.Text = .Item("Branch_DB_Conncet")
                txtWindowsVersion.Text = .Item("Version")
                txtMobileVersion.Text = .Item("Mobile_Version")
                If IsDBNull(.Item("UpdateFromPath")) Then
                    txtUpdateFromPath.Text = ""
                Else
                    txtUpdateFromPath.Text = .Item("UpdateFromPath")
                End If

                If IsDBNull(.Item("ShortCutFile")) Then
                    txtShortCutFile.Text = ""
                Else
                    txtShortCutFile.Text = .Item("ShortCutFile")
                End If
                If IsDBNull(.Item("ShortCutName")) Then
                    txtShortCutName.Text = ""
                Else
                    txtShortCutName.Text = .Item("ShortCutName")
                End If

                If IsDBNull(.Item("ShortCutName")) Then
                    chbReadyToUpdate.Checked = False
                Else
                    If Not CBool(.Item("ReadyToUpdate")) Then
                        chbReadyToUpdate.Checked = False
                    Else
                        chbReadyToUpdate.Checked = True
                    End If

                End If
             
                lblAppVersion.Text = (Me.ProductVersion.ToString)
            End With




        End If
    End Sub






    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        If MessageBox.Show("ต้องการบันทึก?", "ต้องการบันทึก?", MessageBoxButtons.YesNo) = Windows.Forms.DialogResult.Yes Then
            Dim objUpdate As New DBType_SQLServer

            ' Dim strSQL As String = String.Format("Update ms_branch Set Where where Branch_Id = {0}", _Branch_Id)


            Dim sb As New StringBuilder()
            sb.Remove(0, sb.Length)
            sb.Append("Update ms_branch ")
            sb.Append(String.Format("set  Branch_Name='{0}'", txtBranchName.Text))
            sb.Append(String.Format(",Branch_DB_Conncet='{0}'", txtBranchDB.Text))
            sb.Append(String.Format(",Version='{0}'", txtWindowsVersion.Text))
            sb.Append(String.Format(",Mobile_Version='{0}'", txtMobileVersion.Text))
            sb.Append(String.Format(",UpdateFromPath='{0}'", txtUpdateFromPath.Text))
            sb.Append(String.Format(",ShortCutFile='{0}'", txtShortCutFile.Text))
            sb.Append(String.Format(",ShortCutName='{0}'", txtShortCutName.Text))
            sb.Append(String.Format(",ReadyToUpdate='{0}'", IIf(chbReadyToUpdate.Checked, "1", "0")))
            sb.Append(String.Format(" Where  Branch_Id = '{0}'", _Branch_Id))
            objUpdate.DBExeNonQuery(sb.ToString)
            'If objUpdate.DBExeNonQuery(sb.ToString) Then
            MsgBox("Update Success")
            'GetData()
            'Else
            '    MsgBox("Update UnSuccess")
            'End If




        End If


    End Sub

    Private Sub UpdateBranch()

        Dim Branch_Name As String = ""
        Dim Branch_DB_Conncet As String = ""
        Dim Version As String = ""
        Dim Mobile_Version As String = ""
        Dim UpdateFromPath As String = ""
        Dim ShortCutName As String = ""
        Dim ShortCutFile As String = ""
        Dim ReadyToUpdate As String = ""





        Dim strSQL As String = ""
        strSQL = String.Format(" UPDATE  ms_Branch SET " & _
        "Branch_Name = '{0}'" & _
        "Branch_DB_Conncet = '{1}'" & _
        "Version = '{2}'" & _
        "Mobile_Version = '{3}'" & _
        "UpdateFromPath = '{4}'" & _
        "ShortCutName='{5}'" & _
        "ShortCutFile='{6}'" & _
        "ReadyToUpdate='{7}'")


        cmd.CommandText = strSQL
        cmd.CommandType = CommandType.Text
        cmd.ExecuteNonQuery()




    End Sub


    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        txtWindowsVersion.Text = lblAppVersion.Text
    End Sub
End Class