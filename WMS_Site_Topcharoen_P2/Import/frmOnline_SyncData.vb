Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module

Imports System.Data.SqlClient
Imports System.Data


Public Class frmOnline_SyncData

    Private strTotalRecord As String = ""
    Private strOnlineSyncDataType As String = ""

    Private _Table_SyncData As New DataTable

    Private Sub frmOnline_SyncData_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.GetListTableToSyncData()

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    Private Sub GetListTableToSyncData()
        Try
            Dim oOnline_SyncData As New Online_SyncData
            Dim odtListTable As New DataTable
            Dim ods As New DataSet
            odtListTable = oOnline_SyncData.GetGroupListTableToSyncData(" WHERE IsVisible = 1")

            Me._Table_SyncData = oOnline_SyncData.GetListTableToSyncData(" WHERE IsVisible = 1")


            ods.Tables.Add(odtListTable)
            grdPreviewData.AutoGenerateColumns = False
            With grdPreviewData
                .DataSource = odtListTable
            End With

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnSyncData_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSyncData.Click
        Try
            Me.Online_SyncData()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>

    Private Sub Online_SyncData()
        Try

            'Dim oWebService As New com.kascoit.Service1
            'Dim oWebService As New kascoserver.Service1
            Dim oWebService As New Object 'com.wmstracking.rtn.WebService

            Dim oupdate_branch As New tb_Online_SyncData_Log
            Dim oOnline_SyncData As New Online_SyncData
            Dim odtListTable As New DataTable
            Dim odtMasterData As New DataTable
            Dim strTotalRecord As New DataTable
            Dim odtDescriptionData As New DataTable
            Dim strDescription As String = ""
            Dim strTotal_Record As String = ""

            Dim strLastUpdate_Date As String = ""
            '==== Get List Table To Sync Data ====
            '    odtListTable = oOnline_SyncData.GetListTableToSyncData
            odtListTable = Me._Table_SyncData 'grdPreviewData.DataSource

            Dim iRowCount As Integer = 0
            Dim iPageSize As Integer = 1000
            Dim iPageTotal As Integer = 0
            Dim str As String = WV_Branch_ID
            ' 12/01/2010 By Select DB Online


            Dim ods As New DataSet

            Dim drArrGroupdata() As DataRow
            Dim dtDataSurce As New DataTable
            CType(grdPreviewData.DataSource, DataTable).AcceptChanges()
            dtDataSurce = CType(grdPreviewData.DataSource, DataTable)

            dtDataSurce.PrimaryKey = New DataColumn() {dtDataSurce.Columns("Group_Data")}

            drArrGroupdata = dtDataSurce.Select("chkSelect = 1")
            If drArrGroupdata.Length = 0 Then
                W_MSG_Information("กรุณาเลือกรายการ")
                Exit Sub
            End If

            For Each odrDroup As DataRow In drArrGroupdata

                '--------------------- update data ------------------
                Dim drNewrow As DataRow
                drNewrow = dtDataSurce.NewRow
                drNewrow = dtDataSurce.Rows.Find(odrDroup("Group_Data").ToString) 'Key_No = "001"
                drNewrow.BeginEdit()
                drNewrow("Status") = "Wait Syndaya"
                drNewrow.EndEdit()
                '-----------------------------------------------------

                Dim drAListTable() As DataRow = odtListTable.Select("Group_Data = '" & odrDroup("Group_Data").ToString & "' AND IsVisible = 1")

                For Each odrListTable As DataRow In drAListTable
                    'Master
                    If odrListTable("DataType").ToString = "1" Then 'ms
                        ods.Reset()

                        'Delete master online
                        oWebService.SyncData_DeleteMaster(odrListTable("Table_Destination").ToString, WV_Branch_ID)

                        '============== Loop Insert ==============
                        iRowCount = oOnline_SyncData.GetRowCount(odrListTable("Table_Source").ToString)
                        iPageTotal = iRowCount \ iPageSize

                        If iRowCount Mod iPageSize > 0 Then
                            iPageTotal += 1
                        End If

                        For iPageIndex As Integer = 0 To iPageTotal - 1

                            oupdate_branch.Update_add_branch(odrListTable("Table_Destination"))
                            'Get data from local 
                            odtMasterData = oOnline_SyncData.GetMasterData(odrListTable("Table_Source").ToString, (iPageIndex * iPageSize), iPageSize)


                            'Insert master online
                            oWebService.SyncData_InsertMaster(odrListTable("Table_Destination").ToString, odtMasterData, WV_Branch_ID)
                        Next

                        ods.Tables.Add(odtMasterData)

                        strDescription = "SyncData Complete"

                        UpdateLogSyncData(odrListTable("Table_Source").ToString, odtMasterData.Rows.Count, strDescription, "ส่ง")


                    ElseIf odrListTable("DataType").ToString = "0" Then
                        ' Table ASN,SO
                        ods.Reset()
                        strLastUpdate_Date = odrListTable("LastUpdate_Date").ToString()

                        odtDescriptionData = oWebService.SyncDescription_Table(odrListTable("Table_Destination").ToString, CDate(strLastUpdate_Date).ToString("yyyy/MM/dd HH:mm:ss"), WV_Branch_ID)
                        ods.Tables.Add(odtDescriptionData)
                        strDescription = Online_SyncDescriptionData(odrListTable("Table_Destination").ToString, odtDescriptionData)


                        If strDescription = "" Then
                            strDescription = "SyncData Complete"
                        End If

                        UpdateLogSyncData(odrListTable("Table_Source").ToString, odtDescriptionData.Rows.Count, strDescription, "รับ")

                        'Update LastUpdate To config_Online_SyncData
                        oOnline_SyncData.Update_LastUpdate(odrListTable("Table_Source").ToString)

                        Select Case odrListTable("Table_Source").ToString

                            Case "shtb_Shipment", "shtb_ShipmentReceived"

                            Case Else

                                'Update Qty And status To Local
                                oOnline_SyncData.Update_Status(odrListTable("Table_Source").ToString)



                                'Update Qty And status To Online
                                odtDescriptionData = oOnline_SyncData.GetDescriptionData(odrListTable("Table_Source").ToString)

                        End Select


                        If odtDescriptionData.Rows.Count > 0 Then
                            odtDescriptionData.TableName = odrListTable("Table_Source").ToString
                            Select Case odrListTable("Table_Source").ToString

                                Case "shtb_Shipment", "shtb_ShipmentReceived"
                                    'strDescription = oWebService.UpdateQtyAndStatus(odrListTable("Table_Source").ToString, odtDescriptionData)
                                Case Else
                                    strDescription = oWebService.UpdateQtyAndStatus(odrListTable("Table_Source").ToString, odtDescriptionData, WV_Branch_ID)
                            End Select


                            If strDescription <> "SyncData Complete" Then
                                strDescription = "SyncData Complete"
                            End If

                            UpdateLogSyncData(odrListTable("Table_Source").ToString, odtDescriptionData.Rows.Count, strDescription, "รับ")
                        End If

                    End If





                Next

                '--------------------- update data ------------------
                'Dim drNewrow As DataRow
                drNewrow = dtDataSurce.NewRow
                drNewrow = dtDataSurce.Rows.Find(odrDroup("Group_Data").ToString) 'Key_No = "001"
                drNewrow.BeginEdit()
                drNewrow("Status") = "SyncData Complete"
                drNewrow.EndEdit()
                '-----------------------------------------------------


            Next



            W_MSG_Information_ByIndex("400058")

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Function Online_SyncDescriptionData(ByVal pstrTableName As String, ByVal podtDescriptionData As DataTable) As String
        Try
            Dim oOnline_SyncData As New Online_SyncData

           
            oOnline_SyncData.Update_DescriptionData(pstrTableName, podtDescriptionData)
            strTotalRecord = podtDescriptionData.Rows.Count

            Return ""
        Catch ex As Exception
            Return ex.Message.ToString
            Throw ex
        End Try
    End Function

    Private Sub UpdateLogSyncData(ByVal pstrTableName As String, ByVal pstrTotalRecord As String, ByVal pDescription As String, ByVal pstrOnlineSyncDataType As String)
        Try

            Dim oLog_SyncData As New tb_Online_SyncData_Log
            Dim odtLog_SyncData As New DataTable
            Dim ods As New DataSet
            odtLog_SyncData = oLog_SyncData.GetOnline_SyncData(pstrTableName)
            ods.Tables.Add(odtLog_SyncData)

            With oLog_SyncData
                .Online_SyncData_Log_Index = ""
                .Table_Source = pstrTableName
                .Table_Destination = pstrTableName
                .Total_Record = pstrTotalRecord
                .Online_SyncData_Type = pstrOnlineSyncDataType
                .Description = pDescription
                'If odtLog_SyncData.Rows.Count = 0 Then
                .Insert_Online_SyncData_Log()
                ' Else
                ' .Online_SyncData_Log_Index = odtLog_SyncData.Rows(0).Item("Online_SyncData_Log_Index").ToString
                ' .Update_Online_SyncData_Log()
                ' End If
            End With
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub chkSeletAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSeletAll.CheckedChanged
        Try
            If grdPreviewData.Rows.Count = 0 Then Exit Sub
            Dim dtPreviewData As New DataTable
            dtPreviewData = grdPreviewData.DataSource
            For Each drPreviewData As DataRow In dtPreviewData.Rows
                drPreviewData("chkSelect") = chkSeletAll.Checked
            Next

        Catch ex As Exception
            W_MSG_Information(ex.Message)
        End Try
    End Sub

End Class