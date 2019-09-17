
Imports System.Data
Imports WMS_STD_Formula

Public Class Sy_AutoyyyyMM : Inherits DBType_SQLServer

    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

    Public Shadows ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public Shadows ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property

    Public Function Auto_yyyyMM(ByVal pintProcess_Id As Integer, ByVal dates As String)
        Dim strSQLLeft As String = ""
        Dim strSQLMax As String = ""
        'Dim strWhere As String = ""
        Dim Running As String = ""
        Dim Auto_Num As String = ""


        '*************Split Date**************
        Dim yyyymm As String
        Dim da(10) As String
        Dim yyyy As String
        Dim mm As String
        Dim pId As String = ""
        Try
            da = dates.Split("/")
            yyyy = da(2)
            mm = da(1)
            yyyymm = yyyy.Substring(0, 4) + mm

            '*************************************

            Select Case pintProcess_Id

                Case 1 'Order
                    pId = "R" '= Order  substring
                    'left
                    strSQLLeft &= "select left(Order_No,7) as yyyyMM from tb_order where left(Order_No,7) ='" & pId + yyyymm & "'"
                    'Max
                    strSQLMax &= "select max(Right(Order_No,6)) as Maxi from tb_order  where left(Order_No,7) = '" & pId + yyyymm & "'"

                Case 2 ' withdraw
                    pId = "W" '= Withdraw
                    'left
                    strSQLLeft &= "select left(WithDraw_No,7) as yyyyMM from tb_WithDraw where left(WithDraw_No,7)  ='" & pId + yyyymm & "'"
                    'Max
                    strSQLMax &= "select max(Right(WithDraw_No,6)) as Maxi from tb_WithDraw  where left(WithDraw_No,7)  ='" & pId + yyyymm & "'"

            End Select

            Dim objms_ As New SQLCommands

            'left
            objms_.SQLComand(strSQLLeft)
            Dim _DT_yyyyMM As DataTable = New DataTable

            _DT_yyyyMM = objms_.DataTable

            If _DT_yyyyMM.Rows.Count > 0 Then

                'max
                objms_.SQLComand(strSQLMax)
                Dim _DT_Max As DataTable = New DataTable
                _DT_Max = objms_.DataTable

                Dim max As String
                max = _DT_Max.Rows(0).Item("Maxi")
                Running = Val(max) + 1 'max.Substring(2, 4)

                Dim num As Integer = 0
                If Running.Length = 5 Then
                    num = 1
                ElseIf Running.Length = 6 Then
                    num = 2
                End If

                Auto_Num = pId + yyyymm + Running.Substring(num, 4)

            Else

                Auto_Num = pId + yyyymm + "0001"


            End If

            Return Auto_Num

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

#Region "   Normal Gen Doc   "

    Private Function GetSQLAutoNumber(ByVal pstrTable As String, ByVal pstrField_No As String, ByVal pstrField_Date As String, ByVal pstrDocumentType_Index As String, ByVal pstrReset_Running_By As String, ByVal pdateDocument_Date As Date _
    , ByVal intLengthRightFormat_ALL As Integer _
                        , ByVal intLengthLeft_Running As Integer _
                        , ByVal intLengthLeft_YM As Integer _
                        , ByVal intLengthFormat_Y As Integer _
                        , ByVal intStartFromat_Y As Integer _
                        , ByVal StrFormat_Date As String) As String

        Dim strSQLAutoNumber As String = ""
        strSQLAutoNumber = "  SELECT " & pstrField_No
        strSQLAutoNumber &= "           ,right(" & pstrField_No & "," & intLengthRightFormat_ALL & ") as Format_All"
        strSQLAutoNumber &= "           ,right(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_Running & ") as Max_No"
        strSQLAutoNumber &= "           ,left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") as Format_YM"
        strSQLAutoNumber &= "  FROM    " & pstrTable

        Dim strformat_Y As String = ""
        Select Case pstrReset_Running_By.ToUpper
            Case "Y" 'Reset per year

                strformat_Y = Replace(StrFormat_Date, "M", "")
                strformat_Y = Replace(strformat_Y, "m", "")
                strformat_Y = Replace(strformat_Y, "D", "")
                strformat_Y = Replace(strformat_Y, "d", "")

                If intStartFromat_Y = 0 Then
                    strSQLAutoNumber &= "  WHERE   left(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ")," & strformat_Y.Length & ") ='" & pdateDocument_Date.ToString(strformat_Y) & "'"
                Else
                    strSQLAutoNumber &= "  WHERE   right(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") ," & strformat_Y.Length & ") ='" & pdateDocument_Date.ToString(strformat_Y) & "'"
                End If

            Case "M" 'Reset per month
                Dim strformat_M As String = ""
                strformat_Y = Replace(StrFormat_Date, "D", "")
                strformat_Y = Replace(strformat_Y, "d", "")
                If intStartFromat_Y = 0 Then
                    strSQLAutoNumber &= "  WHERE   left(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ")," & strformat_Y.Length & ") ='" & pdateDocument_Date.ToString(strformat_Y) & "'"
                Else
                    strSQLAutoNumber &= "  WHERE   right(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") ," & strformat_Y.Length & ") ='" & pdateDocument_Date.ToString(strformat_Y) & "'"
                End If

            Case "D" 'Reset per day
                Dim strformat_D As String = StrFormat_Date
                If intStartFromat_Y = 0 Then
                    strSQLAutoNumber &= "  WHERE   left(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ")," & strformat_D.Length & ") ='" & pdateDocument_Date.ToString(strformat_D) & "'"
                Else
                    strSQLAutoNumber &= "  WHERE   right(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") ," & strformat_D.Length & ") ='" & pdateDocument_Date.ToString(strformat_D) & "'"
                End If

          Case Else
                strSQLAutoNumber &= " WHERE 1=1   "
        End Select



        strSQLAutoNumber &= "       and DocumentType_Index = '" & pstrDocumentType_Index & "'"
        strSQLAutoNumber &= "       and " & pstrField_No & " not like ('%/%')"

        strSQLAutoNumber &= "  ORDER BY   right(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_Running & ") desc"

        Return strSQLAutoNumber
    End Function

    Private Function GetSQLAutoNumber(ByVal pstrTable As String, ByVal pstrField_No As String, ByVal pstrField_Date As String, ByVal pstrDocumentType_Index As String, ByVal pstrReset_Running_By As String, ByVal pdateDocument_Date As Date _
    , ByVal intLengthRightFormat_ALL As Integer _
                        , ByVal intLengthLeft_Running As Integer _
                        , ByVal intLengthLeft_YM As Integer _
                        , ByVal intLengthFormat_Y As Integer _
                        , ByVal intStartFromat_Y As Integer _
                        , ByVal StrFormat_Date As String _
                        , ByVal intLengthFormat_ALL As Integer) As String

        Dim strSQLAutoNumber As String = ""
        strSQLAutoNumber = "  SELECT " & pstrField_No
        strSQLAutoNumber &= "           ,right(" & pstrField_No & "," & intLengthRightFormat_ALL & ") as Format_All"
        strSQLAutoNumber &= "           ,right(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_Running & ") as Max_No"
        strSQLAutoNumber &= "           ,left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") as Format_YM"
        strSQLAutoNumber &= "  FROM    " & pstrTable

        Dim strformat_Y As String = ""
        Select Case pstrReset_Running_By.ToUpper
            Case "Y" 'Reset per year

                strformat_Y = Replace(StrFormat_Date, "M", "")
                strformat_Y = Replace(strformat_Y, "m", "")
                strformat_Y = Replace(strformat_Y, "D", "")
                strformat_Y = Replace(strformat_Y, "d", "")

                If intStartFromat_Y = 0 Then
                    strSQLAutoNumber &= "  WHERE   left(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ")," & strformat_Y.Length & ") ='" & pdateDocument_Date.ToString(strformat_Y) & "'"
                Else
                    strSQLAutoNumber &= "  WHERE   right(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") ," & strformat_Y.Length & ") ='" & pdateDocument_Date.ToString(strformat_Y) & "'"
                End If

            Case "M" 'Reset per month
                Dim strformat_M As String = StrFormat_Date
                strformat_M = Replace(strformat_M, "D", "")
                strformat_M = Replace(strformat_M, "d", "")
                If intStartFromat_Y = 0 Then
                    strSQLAutoNumber &= "  WHERE   left(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ")," & strformat_M.Length & ") ='" & pdateDocument_Date.ToString(strformat_M) & "'"
                Else
                    strSQLAutoNumber &= "  WHERE   right(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") ," & strformat_M.Length & ") ='" & pdateDocument_Date.ToString(strformat_M) & "'"
                End If

            Case "D" 'Reset per day
                Dim strformat_D As String = StrFormat_Date
                If intStartFromat_Y = 0 Then
                    strSQLAutoNumber &= "  WHERE   left(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ")," & strformat_D.Length & ") ='" & pdateDocument_Date.ToString(strformat_D) & "'"
                Else
                    strSQLAutoNumber &= "  WHERE   right(left(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_YM & ") ," & strformat_D.Length & ") ='" & pdateDocument_Date.ToString(strformat_D) & "'"
                End If

            Case Else
                strSQLAutoNumber &= " WHERE 1=1   "
        End Select

        strSQLAutoNumber &= String.Format(" and len({0})={1} ", pstrField_No, intLengthFormat_ALL)

        Select Case pstrDocumentType_Index
            Case "0010000000501", "0010000000502", "0010000000503"
            Case Else
                strSQLAutoNumber &= "       and DocumentType_Index = '" & pstrDocumentType_Index & "'"
        End Select

        strSQLAutoNumber &= "       and " & pstrField_No & " not like ('%/%')"

        strSQLAutoNumber &= "  ORDER BY   right(right(" & pstrField_No & "," & intLengthRightFormat_ALL & ")," & intLengthLeft_Running & ") desc"

        Return strSQLAutoNumber
    End Function


#End Region
#Region "   Transaction Gen Doc.   "
    Public Function Auto_DocumentType_Number(ByRef Connection As SqlClient.SqlConnection, ByRef myTrans As SqlClient.SqlTransaction, ByVal pstrDocumentType_index As String, ByVal pdateDocument_Date As Date, ByVal pstrWhere As String)
        Dim DSGNDOC As New DataSet
        Try
            Dim strSQL As String = ""
            Dim strSQLAutoNumber As String = ""
            Dim strSQLCHKAutoNumber As String = ""
            Dim iDocumentValue As Integer = 0
            Dim StrFormat_Date As String = ""
            Dim strFormat_Running As String = ""
            Dim strFormat_Document As String = ""
            Dim strSys_Key_Name As String = ""
            Dim strDate As String = ""
            Dim NewDocumentValue As String = ""
            Dim NewDocumentID As String = ""

            Dim strReset_Running_By As String = ""
            Dim intProcess_ID As Integer = 0



            strSQL = " SELECT Process_ID,Format_Date,Format_Running,Format_Document,Sys_Key_Name,Reset_Running_By "
            strSQL &= " FROM ms_DocumentType(nolock) "
            strSQL &= " WHERE DocumentType_Index ='" & pstrDocumentType_index & "'"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(DSGNDOC, "GENDOC")


            If DSGNDOC.Tables("GENDOC").Rows.Count > 0 Then
                Dim drGrndoc As DataRow = DSGNDOC.Tables("GENDOC").Rows(0)
                StrFormat_Date = drGrndoc("Format_Date").ToString
                strDate = pdateDocument_Date.ToString(StrFormat_Date) 'Date parameter
                strFormat_Running = drGrndoc("Format_Running").ToString
                strFormat_Document = drGrndoc("Format_Document").ToString
                strSys_Key_Name = drGrndoc("Sys_Key_Name").ToString

                strReset_Running_By = drGrndoc("Reset_Running_By").ToString
                intProcess_ID = drGrndoc("Process_ID").ToString
            End If

            Dim strTable_Name As String = ""
            Dim strFeildDoc_Name As String = ""
            Dim strFeildDate_Name As String = ""
            Select Case intProcess_ID
                Case 1 ' การรับสินค้า
                    strTable_Name = "tb_Order"
                    strFeildDoc_Name = "Order_No"
                    strFeildDate_Name = "Order_Date"
                Case 2 ' การเบิกสินค้า
                    strTable_Name = "tb_Withdraw"
                    strFeildDoc_Name = "Withdraw_No"
                    strFeildDate_Name = "Withdraw_Date"
                Case 4 ' ตรวจนับ
                    strTable_Name = "tb_Adjust"
                    strFeildDoc_Name = "Adjust_No"
                    strFeildDate_Name = "Adjust_Date"
                Case 5 ' โอนย้าย
                    strTable_Name = "tb_TransferStatus"
                    strFeildDoc_Name = "TransferStatus_No"
                    strFeildDate_Name = "TransferStatus_Date"
                Case 7
                    strTable_Name = "tb_Packing"
                    strFeildDoc_Name = "Packing_No"
                    strFeildDate_Name = "Packing_Date"
                Case 9
                    strTable_Name = "tb_PurchaseOrder"
                    strFeildDoc_Name = "PurchaseOrder_No"
                    strFeildDate_Name = "PurchaseOrder_Date"
                Case 10 'Sales Order
                    strTable_Name = "tb_SalesOrder"
                    strFeildDoc_Name = "SalesOrder_No"
                    strFeildDate_Name = "SalesOrder_Date"
                Case 16 ' แจ้งรับสินค้าล่วงหน้า (ASN)
                    strTable_Name = "tb_AdvanceShipNotice"
                    strFeildDoc_Name = "AdvanceShipNotice_No"
                    strFeildDate_Name = "AdvanceShipNotice_Date"
                Case 21 'แจ้งหนี้ Invoice
                    strTable_Name = "tb_Invoice"
                    strFeildDoc_Name = "Invoice_No"
                    strFeildDate_Name = "Invoice_Date"
                Case 30 'ใบจอง Reservation
                    strTable_Name = "tb_SalesOrder"
                    strFeildDoc_Name = "SalesOrder_No"
                    strFeildDate_Name = "SalesOrder_Date"
                Case 201 ' Memo
                    strTable_Name = "tb_MemoHeader"
                    strFeildDoc_Name = "Memo_Id"
                    strFeildDate_Name = "Memo_Date"
                Case 202 ' ProductionOrder
                    strTable_Name = "tb_ProductionOrder"
                    strFeildDoc_Name = "ProductionOrder_No"
                    strFeildDate_Name = "ProductionOrder_date"
                Case 210 ' โอนคลัง
                    strTable_Name = "tb_TransferWareHouse"
                    strFeildDoc_Name = "TransferWareHouse_No"
                    strFeildDate_Name = "TransferWareHouse_Date"
                Case 24
                    strTable_Name = "tb_TransportManifest"
                    strFeildDoc_Name = "TransportManifest_No"
                    strFeildDate_Name = "TransportManifest_Date"
                Case 18 'pack deliverty
                    strTable_Name = "tb_PackingPallet"
                    strFeildDoc_Name = "PackingPallet_No"
                    strFeildDate_Name = "PackingPallet_Date"
                Case 91
                    strTable_Name = "tb_PurchaseOrder_Request"
                    strFeildDoc_Name = "PurchaseOrder_Request_No"
                    strFeildDate_Name = "PurchaseOrder_Request_Date"
                Case 50
                    strTable_Name = "tb_TransferOwner"
                    strFeildDoc_Name = "TransferOwner_No"
                    strFeildDate_Name = "TransferOwner_Date"
                Case -1010
                    strTable_Name = "tb_SalesOrder"
                    strFeildDoc_Name = "Invoice_No"
                    strFeildDate_Name = "Invoice_Date"
            End Select

            If strSys_Key_Name = "" Then
                Select Case intProcess_ID
                    Case 1, 2, 4, 5, 9, 10, 16, 21, 30, 201, 202, 210, 24, 18, 91, 50, -1010

                        Dim intLengthRightFormat_ALL As Integer = StrFormat_Date.Length + strFormat_Running.Length ' right(AdvanceShipNotice_No,8)
                        Dim intLengthLeft_Running As Integer = strFormat_Running.Length ' left(right(AdvanceShipNotice_No,8),4)
                        Dim intLengthLeft_YM As Integer = StrFormat_Date.Length

                        Dim strformat_Y As String = ""
                        strformat_Y = Replace(StrFormat_Date, "M", "")
                        strformat_Y = Replace(strformat_Y, "m", "")
                        strformat_Y = Replace(strformat_Y, "D", "")
                        strformat_Y = Replace(strformat_Y, "d", "")

                        Dim intStartFromat_Y As Integer = StrFormat_Date.IndexOf(strformat_Y) 'start format
                        Dim intLengthLeft_Y As Integer = strformat_Y.Length

                        Dim _strFormat_Document As String = strFormat_Document
                        _strFormat_Document = Replace(_strFormat_Document, "[Format_Date]", StrFormat_Date)
                        _strFormat_Document = Replace(_strFormat_Document, "[Format_Running]", strFormat_Running)
                        Dim intLengthFormat_ALL As Integer = _strFormat_Document.Length

                        strSQLAutoNumber = GetSQLAutoNumber(strTable_Name, strFeildDoc_Name, strFeildDate_Name, pstrDocumentType_index, strReset_Running_By, pdateDocument_Date _
                        , intLengthRightFormat_ALL _
                        , intLengthLeft_Running _
                        , intLengthLeft_YM _
                        , intLengthLeft_Y _
                        , intStartFromat_Y _
                        , StrFormat_Date _
                        , intLengthFormat_ALL)


                        With SQLServerCommand
                            .Connection = Connection
                            .Transaction = myTrans
                            .CommandText = strSQLAutoNumber
                            .CommandTimeout = 0
                        End With
                        DataAdapter.SelectCommand = SQLServerCommand
                        DataAdapter.SelectCommand.Transaction = myTrans
                        DataAdapter.Fill(DSGNDOC, "GENDOCMAX")


                        If strFormat_Document <> "" Then
                            Dim strNewDocument As String = ""
                            If NewDocumentValue = "" Then
                                NewDocumentValue = 0
                                NewDocumentValue = CDbl(NewDocumentValue) + 1
                                NewDocumentValue = StrDup(strFormat_Running.Length - NewDocumentValue.Length, "0") & NewDocumentValue
                            End If
                            strNewDocument = Replace(strFormat_Document, "[Format_Date]", strDate)
                            strNewDocument = Replace(strNewDocument, "[Format_Running]", NewDocumentValue)
                            NewDocumentID = strNewDocument
                        End If

                        '----------------------------------------------------------------------------------------------
                        'If Me.Auto_DocumentType_Number_Reset(pstrDocumentType_index, NewDocumentID, "") = False Then
                        If DSGNDOC.Tables("GENDOCMAX").Rows.Count > 0 Then
                            Dim objDrAutoNumber As DataRow = DSGNDOC.Tables("GENDOCMAX").Rows(0)
                            Dim boolDup As Boolean = False
                            Dim i As Integer = 1
                            While (boolDup = False)
                                If strSys_Key_Name = "" Then
                                    'Running แบบปกติโดยใช้ข้อมูล Max เลขที่เอกสารในตารางในแต่ละ Process
                                    Dim strMax_No As String = ""
                                    strMax_No = objDrAutoNumber("Max_No").ToString

                                    If strMax_No <> "" Then
                                        NewDocumentValue = Right(strMax_No, strFormat_Running.Length)
                                        If Not IsNumeric(NewDocumentValue) Then
                                            NewDocumentValue = 0
                                        End If
                                        NewDocumentValue = CDbl(NewDocumentValue) + i
                                        NewDocumentValue = StrDup(strFormat_Running.Length - NewDocumentValue.Length, "0") & NewDocumentValue
                                    Else
                                        If strFormat_Running = "" Then strFormat_Running = "xxxx"
                                        NewDocumentValue = StrDup(strFormat_Running.Length - i, "0") & "1"
                                    End If
                                Else
                                    'Running แบบปกติโดยใช้ข้อมูลใน Sy_AutoNumber
                                    iDocumentValue = CDbl(objDrAutoNumber("Sys_Value").ToString) + i
                                    NewDocumentValue = iDocumentValue.ToString(StrDup(strFormat_Running.Length, "0"))
                                    Dim objDBIndex As New Sy_AutoNumber
                                    objDBIndex.UpdateSys_Value(strSys_Key_Name, iDocumentValue)
                                End If
                                If strFormat_Document <> "" Then
                                    Dim strNewDocument As String = ""
                                    If NewDocumentValue = "" Then
                                        NewDocumentValue = 0
                                        NewDocumentValue = CDbl(NewDocumentValue) + 1
                                        NewDocumentValue = StrDup(strFormat_Running.Length - NewDocumentValue.Length, "0") & NewDocumentValue
                                    End If
                                    strNewDocument = Replace(strFormat_Document, "[Format_Date]", strDate)
                                    strNewDocument = Replace(strNewDocument, "[Format_Running]", NewDocumentValue)
                                    NewDocumentID = strNewDocument
                                End If
                                'Loop
                                i += 1
                                If Me.Auto_DocumentType_Number_Reset(pstrDocumentType_index, NewDocumentID, "") = True Then
                                    boolDup = True
                                End If
                            End While
                        End If
                        'End If
                        '----------------------------------------------------------------------------------------------
                    Case Else
                        'Running แบบปกติโดยใช้ข้อมูลใน Sy_AutoNumber
                        If pstrWhere <> "" Then
                            strSQLAutoNumber = "select * from Sy_AutoNumber where Sys_Key = '" & strSys_Key_Name & "'  And " & pstrWhere & ""
                        Else
                            strSQLAutoNumber = "select * from Sy_AutoNumber where Sys_Key = '" & strSys_Key_Name & "' "
                        End If
                End Select
            Else
                'Running แบบปกติโดยใช้ข้อมูลใน Sy_AutoNumber
                If pstrWhere <> "" Then
                    strSQLAutoNumber = "select * from Sy_AutoNumber where Sys_Key = '" & strSys_Key_Name & "'  And " & pstrWhere & ""
                Else
                    strSQLAutoNumber = "select * from Sy_AutoNumber where Sys_Key = '" & strSys_Key_Name & "' "
                End If

                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQLAutoNumber
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DSGNDOC, "GENDOCMAX")

                If DSGNDOC.Tables("GENDOCMAX").Rows.Count > 0 Then
                    Dim objDrAutoNumber As DataRow = DSGNDOC.Tables("GENDOCMAX").Rows(0)
                    'Running แบบปกติโดยใช้ข้อมูลใน Sy_AutoNumber
                    iDocumentValue = CDbl(objDrAutoNumber("Sys_Value").ToString) + 1
                    NewDocumentValue = iDocumentValue.ToString(StrDup(strFormat_Running.Length, "0"))
                    Dim objDBIndex As New Sy_AutoNumber
                    objDBIndex.UpdateSys_Value(strSys_Key_Name, iDocumentValue)
                Else
                    Return ""
                End If

                Dim strNewDocument As String = ""
                If NewDocumentValue = "" Then
                    NewDocumentValue = 0
                    NewDocumentValue = CDbl(NewDocumentValue) + 1
                    NewDocumentValue = StrDup(strFormat_Running.Length - NewDocumentValue.Length, "0") & NewDocumentValue
                End If
                strNewDocument = Replace(strFormat_Document, "[Format_Date]", strDate)
                strNewDocument = Replace(strNewDocument, "[Format_Running]", NewDocumentValue)
                NewDocumentID = strNewDocument
            End If

            Return NewDocumentID

        Catch ex As Exception
            Throw ex
        Finally
            DSGNDOC = Nothing
        End Try

    End Function

    Public Function Auto_DocumentType_Number_Reset(ByVal pstrDocumentType_index As String, ByVal pstrDocument_No As String, ByVal pstrDocument_Index As String) As Boolean
        Dim DSGNDOC As New DataSet
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim strSQL As String = ""

            Dim strTable_Name As String = ""
            Dim strFeildDoc_Name As String = ""
            Dim strFeildDocIndex_Name As String = ""
            Dim strFeildDate_Name As String = ""

            Dim intProcess_ID As Integer = 0
            Dim strSQLCHKAutoNumber As String = ""

            strSQL = " SELECT Process_ID,Format_Date,Format_Running,Format_Document,Sys_Key_Name,Reset_Running_By "
            strSQL &= " FROM ms_DocumentType(nolock) "
            strSQL &= " WHERE DocumentType_Index ='" & pstrDocumentType_index & "'"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(DSGNDOC, "CHKDOC")


            If DSGNDOC.Tables("CHKDOC").Rows.Count > 0 Then
                Dim drGrndoc As DataRow = DSGNDOC.Tables("CHKDOC").Rows(0)
                intProcess_ID = drGrndoc("Process_ID").ToString
                Select Case intProcess_ID
                    Case 1 ' การรับสินค้า
                        strTable_Name = "tb_Order"
                        strFeildDoc_Name = "Order_No"
                        strFeildDate_Name = "Order_Date"
                        strFeildDocIndex_Name = "Order_Index"
                    Case 2 ' การเบิกสินค้า
                        strTable_Name = "tb_Withdraw"
                        strFeildDoc_Name = "Withdraw_No"
                        strFeildDate_Name = "Withdraw_Date"
                        strFeildDocIndex_Name = "Withdraw_Index"
                    Case 4 ' ตรวจนับ
                        strTable_Name = "tb_Adjust"
                        strFeildDoc_Name = "Adjust_No"
                        strFeildDate_Name = "Adjust_Date"
                        strFeildDocIndex_Name = "Adjust_Index"
                    Case 5 ' โอนย้าย
                        strTable_Name = "tb_TransferStatus"
                        strFeildDoc_Name = "TransferStatus_No"
                        strFeildDate_Name = "TransferStatus_Date"
                        strFeildDocIndex_Name = "TransferStatus_Index"
                    Case 7
                        strTable_Name = "tb_Packing"
                        strFeildDoc_Name = "Packing_No"
                        strFeildDate_Name = "Packing_Date"
                        strFeildDocIndex_Name = "Packing_Index"
                    Case 9 'PurchaseOrder Order
                        strTable_Name = "tb_PurchaseOrder"
                        strFeildDoc_Name = "PurchaseOrder_No"
                        strFeildDate_Name = "PurchaseOrder_Date"
                        strFeildDocIndex_Name = "PurchaseOrder_Index"
                    Case 10 'Sales Order
                        strTable_Name = "tb_SalesOrder"
                        strFeildDoc_Name = "SalesOrder_No"
                        strFeildDate_Name = "SalesOrder_Date"
                        strFeildDocIndex_Name = "SalesOrder_Index"
                    Case 16 ' แจ้งรับสินค้าล่วงหน้า (ASN)
                        strTable_Name = "tb_AdvanceShipNotice"
                        strFeildDoc_Name = "AdvanceShipNotice_No"
                        strFeildDate_Name = "AdvanceShipNotice_Date"
                        strFeildDocIndex_Name = "AdvanceShipNotice_Index"
                    Case 21 'แจ้งหนี้ Invoice
                        strTable_Name = "tb_Invoice"
                        strFeildDoc_Name = "Invoice_No"
                        strFeildDate_Name = "Invoice_Date"
                        strFeildDocIndex_Name = "Invoice_Index"
                    Case 30 'Reservation
                        strTable_Name = "tb_SalesOrder"
                        strFeildDoc_Name = "SalesOrder_No"
                        strFeildDate_Name = "SalesOrder_Date"
                        strFeildDocIndex_Name = "SalesOrder_Index"
                    Case 201 ' Memo
                        strTable_Name = "tb_MemoHeader"
                        strFeildDoc_Name = "Memo_Id"
                        strFeildDate_Name = "Memo_Date"
                        strFeildDocIndex_Name = "Memo_Index"
                    Case 202 ' ProductionOrder
                        strTable_Name = "tb_ProductionOrder"
                        strFeildDoc_Name = "ProductionOrder_No"
                        strFeildDate_Name = "ProductionOrder_date"
                        strFeildDocIndex_Name = "ProductionOrder_Index"

                    Case 210 ' โอนคลัง
                        strTable_Name = "tb_TransferWareHouse"
                        strFeildDoc_Name = "TransferWareHouse_No"
                        strFeildDate_Name = "TransferWareHouse_Date"
                        strFeildDocIndex_Name = "TransferWareHouse_Index"
                    Case 24 ' โอนคลัง
                        strTable_Name = "tb_TransportManifest"
                        strFeildDoc_Name = "TransportManifest_No"
                        strFeildDate_Name = "TransportManifest_Date"
                        strFeildDocIndex_Name = "TransportManifest_Index"
                    Case 18 'packing delivery
                        strTable_Name = "tb_PackingPallet"
                        strFeildDoc_Name = "PackingPallet_No"
                        strFeildDate_Name = "PackingPallet_Date"
                        strFeildDocIndex_Name = "PackingPallet_Index"
                    Case 91
                        strTable_Name = "tb_PurchaseOrder_Request"
                        strFeildDoc_Name = "PurchaseOrder_Request_No"
                        strFeildDate_Name = "PurchaseOrder_Request_Date"
                        strFeildDocIndex_Name = "PurchaseOrder_Request_Index"
                    Case 50
                        strTable_Name = "tb_TransferOwner"
                        strFeildDoc_Name = "TransferOwner_No"
                        strFeildDate_Name = "TransferOwner_Date"
                    Case -1010
                        strTable_Name = "tb_SalesOrder"
                        strFeildDoc_Name = "Invoice_No"
                        strFeildDate_Name = "SalesOrder_Date"
                        strFeildDocIndex_Name = "Invoice_Index"
                    Case Else
                        Return False
                End Select

                strSQLCHKAutoNumber = GetSQLCHKAutoNumber(strTable_Name, strFeildDoc_Name, pstrDocument_No, strFeildDocIndex_Name, pstrDocument_Index)

                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = strSQLCHKAutoNumber
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(DSGNDOC, "DUPDOC")
                If DSGNDOC.Tables("DUPDOC").Rows.Count > 0 Then
                    Return False
                End If

            End If


            myTrans.Commit()
            Return True
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex

        Finally
            disconnectDB()
            DSGNDOC = Nothing
        End Try

        'Catch ex As Exception
        '    Throw ex
        'Finally

        'End Try

    End Function

    Private Function GetSQLCHKAutoNumber(ByVal pstrTable As String, ByVal pstrField_No As String, ByVal pstrDocument_No As String, ByVal pstrField_Index As String, ByVal pstrDocument_Index As String) As String

        Dim strSQLAutoNumber As String = ""
        strSQLAutoNumber = " select * "
        strSQLAutoNumber &= "  from " & pstrTable & "(nolock)"
        strSQLAutoNumber &= "  WHERE " & pstrField_No & "='" & pstrDocument_No & "'"

        If pstrDocument_Index <> "" Then
            strSQLAutoNumber &= "   AND " & pstrField_Index & " <> '" & pstrDocument_Index & "'"
        End If

        Return strSQLAutoNumber

    End Function
#End Region


End Class
