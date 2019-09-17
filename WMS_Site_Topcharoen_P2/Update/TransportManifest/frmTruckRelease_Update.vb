Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language


Public Class frmTruckRelease_Update

    Private _IsUSE_TRANSPORT_DATETIME As Boolean = True
    Private _USE_SCALEWEIGHT_TRUCKRELEASE As Boolean = True

    Public dtJobLoading As DataTable

    Private _IsSubManifest As Integer = 0

    Public Property IsSubManifest() As Integer
        Get
            Return _IsSubManifest
        End Get
        Set(ByVal value As Integer)
            _IsSubManifest = value
        End Set
    End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Add Date : 29/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Dong_kk
    ''' </remarks>
    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            Dim dtSave As New DataTable
            dtSave = Me.grdJobLoading.DataSource

            Dim Service As New WMS_Site_Topcharoen_Interface.WMS_Interface
            Dim chkSave As Boolean = False
            Dim Result As String = ""

            For Each dr As DataRow In dtSave.Rows
                If Me._USE_SCALEWEIGHT_TRUCKRELEASE Then
                    If chk_Datarow(dr) = False Then
                        Exit Sub
                    End If
                End If

                Dim objDelectManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.UPDATE)
                objDelectManifest.TransportManifest_Index = dr("TransportManifest_Index").ToString
                objDelectManifest.Time_SourceOutGate = CDate(dtTime_SourceOutGate.Value.ToShortDateString & " " & tTime_SourceOutGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                objDelectManifest.Mile_AtSource = dr("Mile_AtSource").ToString

                If Me._USE_SCALEWEIGHT_TRUCKRELEASE Then
                    objDelectManifest.Time_Truck_FullLoad = CDate(dr("Time_Truck_FullLoad").ToString).ToString("yyyy/MM/dd HH:mm:ss")
                    objDelectManifest.Time_Truck_NoLoad = CDate(dr("Time_Truck_NoLoad").ToString).ToString("yyyy/MM/dd HH:mm:ss")
                    objDelectManifest.Weight_Truck_FullLoad = CDbl(dr("Weight_Truck_FullLoad").ToString) * 1000
                    objDelectManifest.Weight_Truck_NoLoad = CDbl(dr("Weight_Truck_NoLoad").ToString) * 1000
                Else
                    objDelectManifest.Time_Truck_FullLoad = Now.ToString("yyyy/MM/dd HH:mm:ss")
                    objDelectManifest.Time_Truck_NoLoad = Now.ToString("yyyy/MM/dd HH:mm:ss")
                    objDelectManifest.Weight_Truck_FullLoad = 0.0
                    objDelectManifest.Weight_Truck_NoLoad = 0.0
                End If

                objDelectManifest.Status = 4

                If objDelectManifest.Manifest_InGate(_IsSubManifest) Then
                    chkSave = True

INTERFACE_OMS:
                    Result = Service.ConfirmGI(dr("TransportManifest_Index").ToString, W_Module.WV_UserName)
                    If Not String.IsNullOrEmpty(Result) Then
                        If W_MSG_Confirm("Interface OMS ผิดพลาด" & vbCrLf & Result & vbCrLf & "ต้องการส่ง Interface OMS อีกครั้งใช่หรือไม่") = Windows.Forms.DialogResult.Yes Then
                            GoTo INTERFACE_OMS
                        End If
                    End If
                Else
                    Exit For
                End If
            Next

            If chkSave Then
                btnSave.Enabled = False
                W_MSG_Information_ByIndex(1)
            Else
                W_MSG_Information_ByIndex(2)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function chk_Datarow(ByVal dr As DataRow) As Boolean
        Try
            Dim testdate As New Date


            If dr("Weight_Truck_NoLoad").ToString = "" Then
                W_MSG_Information(dr("TransportManifest_No") & ": กรุณระบุน้ำหนัก")
                Return False
            End If
            If dr("Weight_Truck_FullLoad").ToString = "" Then
                W_MSG_Information(dr("TransportManifest_No") & ": กรุณระบุน้ำหนัก")
                Return False
            End If
            If CDbl(dr("Weight_Truck_NoLoad").ToString) > CDbl(dr("Weight_Truck_FullLoad").ToString) Then
                W_MSG_Information(dr("TransportManifest_No") & ": กรุณระบุน้ำหนักให้ถูกต้อง")
                Return False
            End If

            If dr("Time_Truck_NoLoad").ToString = "" Then
                W_MSG_Information("กรุณระบุเวลาชั่งเบา")
                Return False
            Else
                Try
                    testdate = CType(dr("Time_Truck_NoLoad").ToString, Date)
                Catch ex As Exception
                    W_MSG_Error(dr("TransportManifest_No") & ": ระบุรูปแบบวันเวลาชั่งเบาไม่ถูกต้อง")
                    Return False
                End Try
            End If
            If dr("Time_Truck_FullLoad").ToString = "" Then
                W_MSG_Information(dr("TransportManifest_No") & ": กรุณระบุเวลาชั่งหนัก")
                Return False
            Else
                Try
                    testdate = CType(dr("Time_Truck_FullLoad").ToString, Date)
                Catch ex As Exception
                    W_MSG_Error(dr("TransportManifest_No") & ": ระบุรูปแบบวันเวลาชั่งหนักไม่ถูกต้อง")
                    Return False
                End Try
            End If

            Return True
        Catch ex As Exception
            Throw ex
        End Try

    End Function

#Region "Key Number In Datagrid"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Add Date : 29/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In Double 
    ''' </remarks>
    Private Sub grdJobLoading_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdJobLoading.EditingControlShowing
        Try
            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            'Dim strName As String = grdJobLoading.Columns(grdJobLoading.CurrentCell.ColumnIndex).Name
            'If (strName <> "Col_Btn_GetSKU") And (strName <> "Col_UOM") Then
            Dim Column_Index As String = grdJobLoading.CurrentCell.ColumnIndex
            If Not (Column_Index = grdJobLoading.Columns("col_Time_Truck_NoLoad").Index Or Column_Index = grdJobLoading.Columns("col_Time_Truck_FullLoad").Index) Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End If

            'End If

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks>
    ''' Add Date : 29/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In Double 
    ''' </remarks>
    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 0 Double
            Dim Column_Index As String = grdJobLoading.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdJobLoading.Columns("col_Mile_AtSource").Index
                    e.Handled = Check_GrdKeyPress(1, e, Column_Index)

                Case Is = grdJobLoading.Columns("col_Weight_Truck_NoLoad").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
                Case Is = grdJobLoading.Columns("col_Weight_Truck_FullLoad").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="format"></param>
    ''' <param name="e"></param>
    ''' <param name="Column_Index"></param>
    ''' <returns></returns>
    ''' <remarks>
    ''' Add Date : 29/01/2010
    ''' Add By   : Dong_kk
    ''' Add For  : Key In Double 
    ''' </remarks>
    Function Check_GrdKeyPress(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdJobLoading.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdJobLoading.CurrentRow.Cells(Column_Index).EditedFormattedValue
                                If strData.IndexOf(".") >= 0 Then Return True
                            Else
                                Return True
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return True
                    End If
                Case 1
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
                        Return False
                    Else
                        Return True
                    End If
            End Select

        Catch ex As Exception

        End Try
    End Function

#End Region

    Private Sub frmTruckRelease_update_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            config_Transport()
            SetMaskText()
            Me.grdJobLoading.AutoGenerateColumns = False
            With dtJobLoading.Columns
                If Not .Contains("Time_Truck_NoLoad") Then
                    .Add("Time_Truck_NoLoad", GetType(String))
                End If
                If Not .Contains("Time_Truck_FullLoad") Then
                    .Add("Time_Truck_FullLoad", GetType(String))
                End If
                If Not .Contains("Weight_Truck_FullLoad") Then
                    .Add("Weight_Truck_FullLoad", GetType(String))
                End If
                If Not .Contains("Weight_Truck_NoLoad") Then
                    .Add("Weight_Truck_NoLoad", GetType(String))
                End If
            End With
            Me.grdJobLoading.DataSource = dtJobLoading
            dtTime_SourceOutGate.Value = Now
            tTime_SourceOutGate.Value = Now

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
    Private Sub config_Transport()
        Dim objClassDB As New tb_TransportManifest
        Dim objDT As DataTable = New DataTable
        Dim objDr As DataRow

        Try

            objDT = objClassDB.getFieldConfig()
            If objDT.Rows.Count > 0 Then
                Dim i As Integer = 0
                For Each objDr In objDT.Rows
                    Select Case Trim(objDr("Field_Name")).ToString
                        Case "USE_SCALEWEIGHT_TRUCKRELEASE"
                            Me._USE_SCALEWEIGHT_TRUCKRELEASE = False
                            Me.col_Time_Truck_FullLoad.Visible = False
                            Me.col_Time_Truck_NoLoad.Visible = False
                            Me.col_Weight_Truck_FullLoad.Visible = False
                            Me.col_Weight_Truck_NoLoad.Visible = False
                            Me.Size = New Size(525, 492)
                    End Select

                    i = i + 1
                Next

            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        Finally
            objDT = Nothing
            objClassDB = Nothing
        End Try
        '  Cursor.Current = Cursors.Default
    End Sub

    Private Sub SetMaskText()
        Try
            Dim strMaskText As String = "00/00/0000"
            If Not _IsUSE_TRANSPORT_DATETIME Then
                strMaskText = "00/00/0000"
            Else
                strMaskText = "00/00/0000 90:00"
            End If

            'MaskText TextBox


            'MaskText DataGridView
            Me.col_Time_Truck_NoLoad.Mask = strMaskText
            Me.col_Time_Truck_FullLoad.Mask = strMaskText

        Catch ex As Exception
            Throw ex
        End Try

    End Sub


End Class