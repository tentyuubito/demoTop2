Imports System.IO
Imports System.Configuration.ConfigurationSettings
Imports System.Text
Imports System.Threading
Imports System.Globalization
Imports System.Data.OleDb

Public Class frmDroppoint

    Private Sub btnExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExport.Click
        Try

            Dim ds As New DataSet
            Dim dt As New DataTable
            Dim dbContext As New DBType_SQLServer
            dt = dbContext.DBExeQuery("select * from tranf_drop_point")
            ds.Tables.Add(dt)
            ds.Tables(0).TableName = "tranf_drop_point"

            Dim objExport As New Export_Excel_KC

            objExport.export(ds, "tranf_drop_point")

        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub

    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try

            Dim oImport_SO As New bl_Import_SO
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim objWS As DataTable = New DataTable
                Dim ExcelConnString As String = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & OpenFileDialog1.FileName.Trim & ";Extended Properties=""Excel 8.0;HDR=Yes;IMEX=1"";"
                Dim oConnSource As New OleDbConnection(ExcelConnString)
                Dim odaSource As New OleDbDataAdapter
                '=============
                oConnSource.Open()
                objWS = oConnSource.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)


                '====================


                'Dim strWorkSheet As String = Me.cboWorkSheet.Text

                Dim odtTemp As New DataTable
                Dim sQuery As String = ""

                sQuery = " SELECT trandp_branch, trandp_group, trandp_type, trandp_vehicle_type, trandp_droppoint, trandp_route, trandp_text, trandp_person, cdate, cby, istat, iseq"
                sQuery &= " FROM [" & objWS.Rows(objWS.Rows.Count - 1)("TABLE_NAME").ToString & "] "
                odaSource.SelectCommand = New OleDbCommand(sQuery, oConnSource)
                odaSource.Fill(odtTemp)

                '===================
                oConnSource.Close()

                If odtTemp.Rows.Count > 0 Then
                    Dim xdate As System.DateTime
                    xdate = Now
                    Dim xQuery As String = ""
                    Dim xDb As New DBType_SQLServer

                    xQuery = " select *"
                    xQuery &= " from tranf_drop_point"
                    xQuery &= " where 1=1 "

                    Dim dtDroppoint As New DataTable
                    dtDroppoint = xDb.DBExeQuery(xQuery)
                    For Each drRow As DataRow In odtTemp.Rows
                        Dim strCheckdrop As String = ""
                        strCheckdrop = String.Format("trandp_branch = '{0}' ", drRow("trandp_branch").ToString)
                        strCheckdrop &= String.Format(" and trandp_group = '{0}' ", drRow("trandp_group").ToString)
                        strCheckdrop &= String.Format(" and trandp_type = '{0}' ", drRow("trandp_type").ToString)
                        Dim drDroppoint() As DataRow
                        drDroppoint = dtDroppoint.Select(strCheckdrop)
                        If drDroppoint.Length > 0 Then
                            Continue For
                        End If

                        xQuery = " INSERT INTO tranf_drop_point" & Chr(13)
                        xQuery &= " SELECT "
                        xQuery &= "'" & drRow("trandp_branch").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_group").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_type").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_vehicle_type").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_droppoint").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_route").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_text").ToString & "'"
                        xQuery &= ",'" & drRow("trandp_person").ToString & "'"
                        xQuery &= ",'" & xdate.ToString("yyyy/MM/dd HH:mm:ss") & "'"
                        xQuery &= ",'" & drRow("cby").ToString & "'"
                        xQuery &= ",'" & drRow("istat").ToString & "'"
                        xQuery &= ",'" & drRow("iseq").ToString & "'"
                        xDb = New DBType_SQLServer
                        xDb.DBExeNonQuery(xQuery)
                        Thread.Sleep(100)
                    Next

                    'xQuery = " SELECT * "
                    'xQuery &= " INTO tranf_drop_point" & xdate.ToString("yyyyMMddHHmmss") & Chr(13)
                    'xQuery &= " FROM tranf_drop_point " & Chr(13)
                    'xQuery &= " WHERE cdate <> '" & xdate.ToString("yyyy/MM/dd HH:mm:ss") & "'"

                    'xDb = New DBType_SQLServer
                    'xDb.DBExeNonQuery(xQuery)


                    'xQuery = " DELETE tranf_drop_point " & Chr(13)
                    'xQuery &= " WHERE cdate <> '" & xdate.ToString("yyyy/MM/dd HH:mm:ss") & "'"
                    'xDb = New DBType_SQLServer
                    'xDb.DBExeNonQuery(xQuery)

                End If


            Else
                Exit Sub
            End If

        Catch ex As Exception
            Windows.Forms.MessageBox.Show(ex.Message.ToString)
        End Try
    End Sub
End Class