Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_Master.W_Language
Public Class frmTruckReturn


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

    Private Sub frmReleaseTruck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Me.grdJobInTransport.AutoGenerateColumns = False
            Me.grdJobInTransport.DataSource = dtJobLoading
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
            dtSave = Me.grdJobInTransport.DataSource
            Dim chkSave As Boolean = False
            For Each dr As DataRow In dtSave.Rows
                Dim objDelectManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.UPDATE)
                objDelectManifest.TransportManifest_Index = dr("TransportManifest_Index").ToString
                objDelectManifest.Time_ReturnTruckInGate = CDate(dtTime_ReturnTruckInGate.Value.ToShortDateString & " " & tTime_ReturnTruckInGate.Value.ToLongTimeString).ToString("yyyy/MM/dd HH:mm:ss")
                objDelectManifest.Mile_Return = dr("Mile_Return").ToString
                'objDelectManifest.Status = 5
                objDelectManifest.Status = 13 'KSL
                If objDelectManifest.Manifest_ReturnGate(_IsSubManifest) Then
                    chkSave = True
                End If
            Next
            If chkSave Then
                W_MSG_Information_ByIndex(1)
            Else
                W_MSG_Information_ByIndex(2)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
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
    Private Sub grdJobInTransport_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdJobInTransport.EditingControlShowing
        Try
            ' Dong_kk 
            '***************เปิดใช้ keyPress ของ grdcell*****************
            'Dim strName As String = grdJobInTransport.Columns(grdJobInTransport.CurrentCell.ColumnIndex).Name
            'If (strName <> "Col_Btn_GetSKU") And (strName <> "Col_UOM") Then
            Dim txtEdit As TextBox = e.Control
            RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
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

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdJobInTransport.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdJobInTransport.Columns("col_Mile_Return").Index
                    e.Handled = Check_GrdKeyPress(1, e, Column_Index)

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
                            If grdJobInTransport.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdJobInTransport.CurrentRow.Cells(Column_Index).EditedFormattedValue
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
            W_MSG_Error(ex.Message)
        End Try
    End Function
#End Region

End Class