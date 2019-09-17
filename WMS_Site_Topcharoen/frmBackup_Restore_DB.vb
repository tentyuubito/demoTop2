Imports System.Data.SqlClient
Imports Microsoft.SqlServer.Management.Smo
Imports WMS_STD_Formula
Imports System.Drawing.Drawing2D
Public Class frmBackup_Restore_DB
    'Backup & Restore of SQL Server database using VB.NET
    Dim IsChange As Boolean = False
    Private Sub cmbDataBase1_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbDataBase1.DropDown
        Try
            cmbDataBase1.Items.Clear()
            Dim serverName As String = cmbServerName1.Text.ToString()
            Dim serverConnection As Microsoft.SqlServer.Management.Common.ServerConnection = New Microsoft.SqlServer.Management.Common.ServerConnection()
            serverConnection.ServerInstance = serverName
            serverConnection.LoginSecure = True
            If cmbAuthe1.SelectedIndex = 1 Then
                serverConnection.LoginSecure = False
                serverConnection.Login = txtUserName1.Text
                serverConnection.Password = txtPassword1.Text
            End If
            Dim server As Server = New Server(serverConnection)
            Try
                For Each database As Database In server.Databases
                    cmbDataBase1.Items.Add(database.Name)
                Next
            Catch ex As Exception
                Dim exception As String = ex.Message
            End Try
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Private Sub cmbServerName1_DropDown(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmbServerName1.DropDown
        Dim dataTable1 = SmoApplication.EnumAvailableSqlServers(False)
        cmbServerName1.ValueMember = "Name"
        cmbServerName1.DataSource = dataTable1
    End Sub
    Dim Connect1 As Integer = 0
    Dim ServerName1 As String
    Dim UserID1 As String
    Dim Password1 As String
    Dim strConn As String
    Dim database As String
    Dim conn As SqlClient.SqlConnection
    Dim IsEdit As Integer = 0
    Enum Action
        BackUp
        Restore
    End Enum
    Private Sub btnBackUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBackUp.Click
        Execute(Action.BackUp)
    End Sub
    Private Sub btnRestore_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRestore.Click
        Execute(Action.Restore)
    End Sub
    Private Sub Execute(ByVal strAction As Action)
        Dim Filename As String
        If IsChange Then
            ServerName1 = cmbServerName1.Text.Trim()
            UserID1 = txtUserName1.Text.Trim()
            Password1 = txtPassword1.Text.Trim()
            database = cmbDataBase1.Text
        Else
            Backup_RestoreDB()
        End If
      
        'Data Source=server;Initial Catalog=WMS_Master_Version_4_1;User ID=sa;PWD=kascodb;Connect Timeout=1000
        strConn = "Data Source=" & ServerName1 & "; Initial Catalog=" & database & ";user id=" & UserID1 & ";PWD=" & Password1 & ";Connect Timeout=10000"

        conn = New SqlClient.SqlConnection(strConn)
        conn.Open()


        Dim strQuery As String = ""
        If strAction = Action.BackUp Then
            Dim objdlg As New SaveFileDialog
            objdlg.FileName = database
            If Not objdlg.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Filename = objdlg.FileName
                strQuery = "backup database " & database & " to disk='" & Filename & "'"
            End If

        Else
            Dim objdlg As New OpenFileDialog
            objdlg.FileName = database
            If Not objdlg.ShowDialog() = Windows.Forms.DialogResult.Cancel Then
                Filename = objdlg.FileName
                strQuery = "RESTORE DATABASE " & database & " FROM disk='" & Filename & "'"
            End If

        End If

        Dim cmd As SqlClient.SqlCommand
        cmd = New SqlClient.SqlCommand(strQuery, conn)
        If cmd.ExecuteNonQuery() Then
            MsgBox(strAction.ToString & " Success")
            conn.Close()
        End If
    End Sub
    Private Sub Backup_RestoreDB()

        Dim builder As New System.Data.Common.DbConnectionStringBuilder
        builder.ConnectionString = W_Module.WV_ConnectionString
        Dim strServerName As String = builder("Data Source")
        Dim strUserID As String = builder("User ID")
        Dim strPassword As String = builder("pwd")
        Dim strDatabaseName As String = builder("Initial Catalog")


        ServerName1 = strServerName
        UserID1 = strUserID
        Password1 = strPassword
        database = strDatabaseName

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()

        '        System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();

        'builder.ConnectionString = ConfigurationManager.ConnectionStrings["inventoryDBConnection"].ConnectionString; ;

        'string server = builder["Data Source"] as string;
        'string database = builder["Initial Catalog"] as string;
        'string UserID = builder["User ID"] as string;
        'string password = builder["Password"] as string;


    End Sub


    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim builder As New System.Data.Common.DbConnectionStringBuilder
        builder.ConnectionString = W_Module.WV_ConnectionString
        Dim strServerName As String = builder("Data Source")
        Dim strUserID As String = builder("User ID")
        Dim strPassword As String = builder("pwd")
        Dim strDatabaseName As String = builder("Initial Catalog")


        ServerName1 = builder("Data Source")
        UserID1 = builder("User ID")
        Password1 = builder("pwd")
        database = builder("Data Source")

        Me.cbServerName_HCB.Text = ServerName1
        Me.txtUserName_HCB.Text = UserID1
        Me.txtPassword_HCB.Text = Password1
        Me.cbDatabase_HCB.Text = database
        '  Me.cbAuthentication_HCB
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim lgb As New LinearGradientBrush(ClientRectangle, Color.Yellow, Color.YellowGreen, LinearGradientMode.Vertical)
        e.Graphics.FillRectangle(lgb, ClientRectangle)
        lgb.Dispose()
    End Sub
  
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        IsChange = Not IsChange
        If IsChange = True Then
            Me.Width = 687
            Me.cmbServerName1.Focus()
        Else
            Me.Width = 376

        End If

    End Sub
End Class
