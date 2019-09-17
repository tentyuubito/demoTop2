Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Public Class clsLogin

    Public userIndex As String = ""
    Public GroupIndex As String = ""
    Public userLogin As String = ""
    Public DepartmentIndex As String = ""
    Public BranchIndex As Integer

    Function CheckUser(ByVal loginname As String, ByVal passwd As String) As Boolean


        Dim objClassDB As New se_User(se_User.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Dim username As String = loginname
        Dim userpasswd As String = passwd
        Dim loginSta As Boolean = False

        Try
            objClassDB.CheckUser(username, userpasswd)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then
                For Each objDr In objDT.Rows
                    loginSta = True
                    userIndex = objDr("user_index").ToString
                    userLogin = objDr("userFullName").ToString
                    GroupIndex = objDr("group_index").ToString
                    DepartmentIndex = objDr("Department_Index").ToString
                    BranchIndex = CInt(objDr("add_branch").ToString)

                    WV_UserName = userLogin

                    'WV_Branch_ID = objDr("add_branch").ToString
                    WV_GroupUser_Index = objDr("group_index").ToString
                    WV_User_Index = objDr("user_index").ToString
                    WV_UserFullName = objDr("userFullName").ToString
                    WV_UserName = objDr("userName").ToString


                    'WV_Branch_ID = objDr("userName").ToString
                    'WV_Department_Des = cboDepartment.Text

                Next
            Else
                loginSta = False
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            Throw ex
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        Return loginSta
    End Function
End Class
