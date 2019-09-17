'Dong_kk
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Data
Imports System.Data.SqlClient
Imports WMS_STD_OUTB_Transport_Datalayer

Public Class clsTransportSO_POD : Inherits DBType_SQLServer

#Region "  GLOBAL VAR  "

#End Region

#Region "  PROPERTY  "
    Private _datatable As New DataTable
    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _datatable
        End Get
    End Property

    Private _TransportManifest_Index As String = ""
    Public Property TransportManifest_Index() As String
        Get
            Return _TransportManifest_Index
        End Get
        Set(ByVal value As String)
            _TransportManifest_Index = value
        End Set
    End Property
    Private _TransportManifestItem_Index As String = ""
    Public Property TransportManifestItem_Index() As String
        Get
            Return _TransportManifestItem_Index
        End Get
        Set(ByVal value As String)
            _TransportManifestItem_Index = value
        End Set
    End Property
#End Region

#Region "  SELECT DATA  "
    Public Sub getDeliveryResult_Status()
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT     *   FROM config_TransportDespatchStatus   WHERE    DeliveryResult_Status = 1 or Status =5 order by Status "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    'update best 20-08-2012 data reader
    Public Sub getTransportManifest_DeliveryRrsult(ByVal pstrSearch As String, ByVal pintCaseSearch As Integer, ByVal pintConSortBy As Integer, ByVal pintConSort As Integer, ByVal IsComplete As Boolean)
        Dim strSQL As String = " "
        Dim oReader As Data.SqlClient.SqlDataReader
        Try
            'strSQL = " SELECT     * "
            'strSQL &= "  FROM VIEW_TransportManifest_OnTruck "

            'strSQL &= "    WHERE      Status_Id_Item <> -1 "
            'If Process_Id <> "" Then
            '    strSQL &= " and isnull(Process_Id,10) = " & Process_Id
            'End If
            'strSQL &= "  and   Status_Manifest IN"
            'strSQL &= "       (SELECT     Status"
            'strSQL &= "  FROM  dbo.config_TransportDespatchStatus "
            'strSQL &= "             WHERE      DeliveryResult_Status = 1)"

            'SetSQLString = strSQL & strWhere

            strSQL = "  SELECT * , Time_DeliveryToDestination as Time_DeliveryToDestination_Time "
            strSQL &= " FROM VIEW_TransportManifest_OnTruck  "

            Select Case pintCaseSearch
                Case 1
                    strSQL &= " WHERE TransportManifest_Index =  '" & pstrSearch & "' "
                Case 2
                    strSQL &= " WHERE Str1 =  '" & pstrSearch & "'"
            End Select

            If IsComplete = False Then
                strSQL &= " AND Status_Manifest <> 5 "
            Else

            End If

            Select Case pintConSortBy
                Case 1
                    strSQL &= " ORDER BY Str1"
                Case 2
                    strSQL &= " ORDER BY SalesOrder_No"
            End Select

            Select Case pintConSort
                Case 1
                    strSQL &= " ASC "
                Case 2
                    strSQL &= " DESC "

            End Select

            SetSQLString = strSQL
            connectDB()

            'EXEC_DataAdapter()
            '_datatable = GetDataTable

            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            EXEC_Command()
            oReader = Me.GetDataReader
            _datatable.Clear()
            _datatable.Load(oReader)
            oReader.Close()
        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
        End Try
    End Sub
    Public Sub getCus_Ship_Locartion_SearchPopUp(ByVal WhereString As String, Optional ByVal TopStr As String = "")
        '  
        Dim strSQL As String = ""
        Dim strWhere As String = ""
        Try


            strSQL = " select " & TopStr & " * from VIEW_MS_Cus_Ship_Location"


            SetSQLString = strSQL + WhereString

            connectDB()
            EXEC_DataAdapter()
            _datatable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub getTransportManifest_Noregister(ByVal pstrWhere As String)
        Dim strSQL As String = ""
        Try
            strSQL = " select *,ms_DistributionCenter.Description AS DistributionCenter "
            strSQL &= " from tb_TransportManifest "
            strSQL &= " LEFT  JOIN  ms_Vehicle ON tb_TransportManifest.Vehicle_Index = ms_Vehicle.Vehicle_Index"
            strSQL &= " LEFT JOIN ms_Route on tb_TransportManifest.Route_Index = ms_Route.Route_Index"
            strSQL &= " LEFT JOIN ms_SubRoute on tb_TransportManifest.SubRoute_Index = ms_SubRoute.SubRoute_Index"
            strSQL &= " LEFT JOIN ms_DistributionCenter on tb_TransportManifest.DistributionCenter_Index = ms_DistributionCenter.DistributionCenter_Index"
            strSQL &= " LEFT JOIN ms_Driver on tb_TransportManifest.Driver_Index = ms_Driver.Driver_Index "
            strSQL &= " where TransportManifest_Index in (	select	TMI.TransportManifest_Index"
            strSQL &= " from	tb_TransportManifestItem TMI inner join"
            strSQL &= " tb_SalesOrder SO ON TMI.SalesOrder_Index = SO.SalesOrder_Index"
            strSQL &= " WHERE	ISNULL(TMI.IsPOD,0) = 0 "
            'strSQL &= " WHERE	SO.Status_Manifest in (7,13,14,15)"
            strSQL &= "	) and tb_TransportManifest.Status  in (7,13,14,15)"


            SetSQLString = strSQL & pstrWhere & " Order by tb_TransportManifest.TransportManifest_No"
            connectDB()
            EXEC_DataAdapter()
            _datatable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Function GetTransportManifest_Index(ByVal pstrTransportManifest_No As String) As String
        Dim strSQL As String = ""
        Try
            strSQL = "  SELECT TransportManifest_Index "
            strSQL &= " FROM tb_TransportManifest"
            strSQL &= " WHERE TransportManifest_No = '" & pstrTransportManifest_No & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _datatable = GetDataTable

            If _datatable.Rows.Count > 0 Then
                Return _datatable.Rows(0)("TransportManifest_Index").ToString
            Else
                Return ""
            End If
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
#End Region

#Region "  INSERT DATA  "

#End Region

#Region "  UPDATE DATA  "

    Public Function Update(ByVal _objCollectionItem As List(Of tb_TransportManifestItem)) As String
        Dim strSQL As String = " "
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Dim objReader As SqlClient.SqlDataReader = Nothing

        Try

            'Time_DocReturnedToDC
            'Time_DocReturnedToSource
            'Time_DocReturnedToOwner
            'Time_DocConfirmedByOwner

            For Each _objItem As tb_TransportManifestItem In _objCollectionItem
                'strSQL = " UPDATE tb_TransportManifest" & _
                '                         " SET     TotalTransportCharged=@TotalTransportCharged" & _
                '                         "          ,TotalTransportPaid=@TotalTransportPaid" & _
                '                         "          ,DriverPaidAmount=@DriverPaidAmount" & _
                '                "           WHERE          TransportManifest_Index = @TransportManifest_Index"
                'With SQLServerCommand.Parameters
                '    .Clear()
                '    .Add("@TransportManifest_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifest_Index
                '    .Add("@TotalTransportCharged", SqlDbType.Float, 15).Value = _objItem.Flo1
                '    .Add("@TotalTransportPaid", SqlDbType.Float, 15).Value = _objItem.Flo2
                '    .Add("@DriverPaidAmount", SqlDbType.Float, 15).Value = _objItem.Flo3
                'End With
                'SetSQLString = strSQL
                'SetCommandType = enuCommandType.Text
                'SetEXEC_TYPE = EXEC.NonQuery
                'EXEC_Command()




                strSQL = " UPDATE tb_TransportManifestItem" & _
                           " SET  Time_DeliveryToDestination=@Time_DeliveryToDestination" & _
                           "     ,Time_DocReturnedToDC=@Time_DocReturnedToDC" & _
                           "     ,Time_DocReturnedToSource=@Time_DocReturnedToSource" & _
                           "     ,Time_DocReturnedToOwner=@Time_DocReturnedToOwner" & _
                           "     ,Time_DocConfirmedByOwner=@Time_DocConfirmedByOwner" & _
                           "     ,chk_Problem=@chk_Problem" & _
                           "     ,Mile_AtDestination=@Mile_AtDestination" & _
                           "     ,Comment=@Comment,IsPOD=@IsPOD" & _
                           "     ,Invoice_No=@Invoice_No" & _
                           "           WHERE          TransportManifestItem_Index = @TransportManifestItem_Index"
                With SQLServerCommand.Parameters
                    .Clear()
                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifestItem_Index
                    If _objItem.Time_DeliveryToDestination = Nothing Then
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    End If
                    If _objItem.Time_DocReturnedToDC = Nothing Then
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    End If
                    If _objItem.Time_DocReturnedToSource = Nothing Then
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    End If
                    If _objItem.Time_DocReturnedToOwner = Nothing Then
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    End If
                    If _objItem.Time_DocConfirmedByOwner = Nothing Then
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    End If
                    '   .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    '   .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    '  .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    '  .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    ' .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    .Add("@Mile_AtDestination", SqlDbType.Int, 10).Value = _objItem.Mile_AtDestination
                    .Add("@chk_Problem", SqlDbType.Bit, 1).Value = _objItem.chk_Problem
                    .Add("@Comment", SqlDbType.VarChar, 100).Value = _objItem.Comment

                    'Add new 2017
                    .Add("@IsPOD", SqlDbType.Bit, 1).Value = 1
                    .Add("@Invoice_No", SqlDbType.VarChar, 100).Value = _objItem.Invoice_No
                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                EXEC_Command()


                strSQL = " UPDATE tb_SalesOrderTrip" & _
                      " SET     TransportManifestItem_Index=@TransportManifestItem_Index" & _
                      "     ,Time_DeliveryToDestination=@Time_DeliveryToDestination" & _
                      "     ,Time_DocReturnedToSource=@Time_DocReturnedToSource" & _
                        "     ,Time_DocReturnedToDC=@Time_DocReturnedToDC" & _
                      "     ,Time_DocReturnedToOwner=@Time_DocReturnedToOwner" & _
                      "     ,Time_DocConfirmedByOwner=@Time_DocConfirmedByOwner" & _
                      "     ,Mile_AtDestination=@Mile_AtDestination" & _
                    "     ,str1=@str1" & _
                   "     ,Status=@Status" & _
                   "     ,Status_Manifest=@Status_Manifest" & _
                      "           WHERE          TransportManifestItem_Index = @TransportManifestItem_Index"
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@TransportManifestItem_Index", SqlDbType.VarChar, 13).Value = _objItem.TransportManifestItem_Index

                    If _objItem.Time_DeliveryToDestination = Nothing Then
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    End If
                    If _objItem.Time_DocReturnedToDC = Nothing Then
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    End If
                    If _objItem.Time_DocReturnedToSource = Nothing Then
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    End If
                    If _objItem.Time_DocReturnedToOwner = Nothing Then
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    End If
                    If _objItem.Time_DocConfirmedByOwner = Nothing Then
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    End If

                    '   .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    '.Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    '.Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    '.Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    ' .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    .Add("@Mile_AtDestination", SqlDbType.Int, 10).Value = _objItem.Mile_AtDestination
                    .Add("@str1", SqlDbType.VarChar, 100).Value = _objItem.Comment
                    .Add("@Status", SqlDbType.VarChar, 100).Value = _objItem.Status
                    .Add("@Status_Manifest", SqlDbType.VarChar, 100).Value = _objItem.Status
                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                EXEC_Command()



                'strSQL = "    Update tb_SalesOrder set Status_Manifest = " & _objItem.Status
                'strSQL &= "    where SalesOrder_Index = '" & _objItem.SalesOrder_Index & "'"

                strSQL = " UPDATE tb_SalesOrder" & _
                    " SET Time_DeliveryToDestination=@Time_DeliveryToDestination" & _
                    "     ,Time_DocReturnedToSource=@Time_DocReturnedToSource" & _
                    "     ,Time_DocReturnedToDC=@Time_DocReturnedToDC" & _
                    "     ,Time_DocReturnedToOwner=@Time_DocReturnedToOwner" & _
                    "     ,Time_DocConfirmedByOwner=@Time_DocConfirmedByOwner" & _
                    "     ,Status_Manifest=@Status_Manifest" & _
                    "     ,Invoice_No=@Invoice_No" & _
                    "     ,Str1=(case when ((isnull(Str1,'')='') or (isnull(Str1,'') = Invoice_No)) then  @Invoice_No else Str1 end )" & _
                    "           WHERE          SalesOrder_Index = @SalesOrder_Index"


                '"     ,Str1=@Str1" & _
                With SQLServerCommand.Parameters
                    .Clear()

                    .Add("@SalesOrder_Index", SqlDbType.VarChar, 13).Value = _objItem.SalesOrder_Index

                    If _objItem.Time_DocConfirmedByOwner = Nothing Then
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DeliveryToDestination", SqlDbType.DateTime, 23).Value = _objItem.Time_DeliveryToDestination
                    End If
                    If _objItem.Time_DocReturnedToDC = Nothing Then
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToDC", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToDC
                    End If
                    If _objItem.Time_DocReturnedToSource = Nothing Then
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToSource", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToSource
                    End If
                    If _objItem.Time_DocReturnedToOwner = Nothing Then
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocReturnedToOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocReturnedToOwner
                    End If
                    If _objItem.Time_DocConfirmedByOwner = Nothing Then
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = DBNull.Value
                    Else
                        .Add("@Time_DocConfirmedByOwner", SqlDbType.DateTime, 23).Value = _objItem.Time_DocConfirmedByOwner
                    End If
                    .Add("@Status_Manifest", SqlDbType.VarChar, 100).Value = _objItem.Status

                    .Add("@Invoice_No", SqlDbType.VarChar, 100).Value = _objItem.Invoice_No


                End With
                SetSQLString = strSQL
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                EXEC_Command()


                SetSQLString = String.Format(" UPDATE tb_SalesOrder SET TransportManifest_No = '' WHERE SalesOrder_Index = '{0}'", _objItem.SalesOrder_Index)
                SetCommandType = enuCommandType.Text
                SetEXEC_TYPE = EXEC.NonQuery
                EXEC_Command()


                'Update All complete
                strSQL = " select	TransportManifest_Index"
                strSQL &= " from	tb_TransportManifestItem "
                strSQL &= " WHERE	ISNULL(IsPOD,0) = 0"
                strSQL &= "         AND TransportManifest_Index = '" & _objItem.TransportManifest_Index & "' "
                With SQLServerCommand
                    '.Connection = _Connection
                    .Transaction = myTrans
                    .CommandText = strSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                If DS.Tables.Contains("TFS") Then
                    DS.Tables("TFS").Clear()
                End If
                DataAdapter.Fill(DS, "TFS")
                If DS.Tables("TFS").Rows.Count = 0 Then
                    SetSQLString = " UPDATE tb_TransportManifest SET Status = 5 WHERE TransportManifest_Index = '" & _objItem.TransportManifest_Index & "' "
                    SetCommandType = enuCommandType.Text
                    SetEXEC_TYPE = EXEC.NonQuery
                    EXEC_Command()

                End If



            Next
            '*** Commit transaction
            myTrans.Commit()
            Return "Complete"

        Catch ex As Exception
            myTrans.Rollback()
            Throw ex

        Finally
            disconnectDB()
        End Try



    End Function

#End Region

#Region "  DELETE DATA  "

#End Region
End Class
