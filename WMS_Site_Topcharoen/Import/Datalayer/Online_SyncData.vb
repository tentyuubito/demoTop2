Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Formula

Public Class Online_SyncData : Inherits DBType_SQLServer

    Public Function GetListTableToSyncData() As DataTable
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT * "
            strSQL &= " FROM config_Online_SyncData WHERE Status_id <> -1"

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function GetListTableToSyncData(ByVal strWhere As String) As DataTable
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT * "
            strSQL &= " FROM config_Online_SyncData "
            strSQL &= strWhere
            strSQL &= " ORDER BY Seq,Table_Source"
            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetGroupListTableToSyncData(ByVal strWhere As String) As DataTable
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT convert(bit,0) as chkSelect,'' as Status, Group_Data "
            strSQL &= " FROM config_Online_SyncData "
            strSQL &= strWhere
            strSQL &= " Group BY Group_Data"
            strSQL &= " ORDER BY Group_Data"
            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetMasterData(ByVal pstrTableName As String, ByVal pintStratIndex As Integer, ByVal pintMaxRecord As Integer) As DataTable
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odsServer As New DataSet
            Dim odaServer As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT * "
            strSQL &= " FROM " & pstrTableName

            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odsServer, pintStratIndex, pintMaxRecord, pstrTableName)

            Return odsServer.Tables(0).Copy

        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function GetRowCount(ByVal pstrTableName As String) As Integer
        Try
            Dim oConn As New SqlConnection(WV_ConnectionString)
            Dim odt As New DataTable
            Dim oda As New SqlDataAdapter
            Dim strSQL As String

            strSQL = " SELECT Count(*)  as Total"
            strSQL &= " FROM " & pstrTableName

            oda.SelectCommand = New SqlCommand(strSQL, oConn)

            oda.Fill(odt)

            If odt.Rows.Count > 0 Then
                Return odt.Rows(0)("Total").ToString
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDescriptionData(ByVal pstrTableName As String) As DataTable
        Try
            Dim oConnServer As New SqlConnection(WV_ConnectionString)
            Dim odtServer As New DataTable
            Dim odaServer As New SqlDataAdapter


            Dim strSQL As String
            strSQL = " SELECT * "
            strSQL &= " FROM " & pstrTableName
            strSQL &= " WHERE  Status in (2,3)"
            odaServer.SelectCommand = New SqlCommand(strSQL, oConnServer)

            odaServer.Fill(odtServer)

            Return odtServer

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Sub Update_Status(ByVal pstrTable_Name As String)
        Dim oConnServer As New SqlConnection(WV_ConnectionString)
        Try


            Dim ocmd As New SqlCommand
            Dim strSQL As String = ""

            strSQL = " UPDATE  " & pstrTable_Name & " SET"
            'Update Status จาก 2 -- > 1
            ' 102 = คือสถานะจาก online รอส่งรอมูล
            ' 1 = คือสถานะ รอยืนยัน จาก WMS
            Select Case pstrTable_Name
                Case "tb_AdvanceShipNotice"
                    strSQL &= " Status = 1 "
                    strSQL &= " WHERE Status = 102 "

                Case "tb_AdvanceShipNoticeItem"
                    strSQL &= " Status = 1 "
                    strSQL &= " WHERE Status = 102"

                Case "tb_ASN_WithDraw"
                    strSQL &= " Status =1 "
                    strSQL &= " WHERE Status = 102 "

                Case "tb_ASN_WithDrawItem"
                    strSQL &= " Status = 1 "
                    strSQL &= " WHERE Status = 102 "

                Case "tb_SalesOrder"
                    strSQL &= " Status = 1 "
                    strSQL &= " WHERE Status = 102 "

                Case "tb_SalesOrderItem"
                    strSQL &= " Status = 1 "
                    strSQL &= " WHERE Status = 102 "

                    'Case "shtb_ShipmentReceived"
                    '    exit Sub
                    '    '    strSQL &= ""


                    '    'Case "shtb_Shipment"
                    '    '    strSQL &= " Status_Id = 2 "
                    '    '    strSQL &= " WHERE Status_Id = -3 "

                Case Else
                    Exit Sub
            End Select

            With ocmd
                .CommandType = CommandType.Text
                .CommandText = strSQL
                .Connection = oConnServer
                .Connection.Open()
                .ExecuteNonQuery()
            End With

        Catch ex As Exception
            Throw ex
        Finally
            oConnServer.Close()
        End Try

    End Sub
    Public Sub Update_DescriptionData(ByVal pstrTable_Name As String, ByVal podtSource As DataTable)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try
            strSQL = " SELECT     * "
            strSQL &= " FROM   " & pstrTable_Name

            Dim odaDestination As New SqlDataAdapter(strSQL, WV_ConnectionString)
            Dim ocmBuild As New SqlCommandBuilder(odaDestination)

            For Each odr As DataRow In podtSource.Rows
                odr.SetAdded()
            Next

            odaDestination.Update(podtSource)
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub Update_LastUpdate(ByVal pstrTable_Name)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " Update  config_Online_SyncData   "
            strSQL &= " Set  LastUpdate_Date   =getdate() "
            strSQL &= " WHERE Table_Source = '" & pstrTable_Name & "'"


            Dim odaOnline_SyncData As New SqlCommand(strSQL, New SqlConnection(WV_ConnectionString))

            odaOnline_SyncData.Connection.Open()
            odaOnline_SyncData.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub
    ''' <summary>
    ''' ja
    ''' 15-02-2010
    ''' Update_IsDefault to config_Online_SyncData
    ''' </summary>
    ''' <param name="pstrTable_Name"></param>
    ''' <remarks></remarks>
    Public Sub Update_IsDefault(ByVal pstrTable_Name)
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try

            strSQL = " Update  config_Online_SyncData   "
            strSQL &= " Set  IsDefault   =1 "
            strSQL &= " WHERE Table_Source = '" & pstrTable_Name & "'"


            Dim odaOnline_SyncData As New SqlCommand(strSQL, New SqlConnection(WV_ConnectionString))

            odaOnline_SyncData.Connection.Open()
            odaOnline_SyncData.ExecuteNonQuery()
        Catch ex As Exception
            Throw ex
        End Try

    End Sub

End Class
