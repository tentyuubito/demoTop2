Imports System.Xml
Imports System.Threading
Imports System.IO

Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula

Public Class frmImport_OrderXML


    Dim progressThread As Thread


    Private _DEFAULT_MOVEFILE As String = ""

    Public Property DEFAULT_MOVEFILE() As String
        Get
            Return _DEFAULT_MOVEFILE
        End Get
        Set(ByVal value As String)
            _DEFAULT_MOVEFILE = value
        End Set
    End Property


    Private Sub btnImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnImport.Click
        Try
            If grdXMLData.Rows.Count <= 0 Then
                W_MSG_Information("กรุณาระบุ File ที่ต้องการ Import")
                Exit Sub
            End If
           
            btnImport.Enabled = False

            Dim oImport_OrderXML As New Import_OrderXML

            If CheckFile(_DEFAULT_MOVEFILE) <> "" Then

                Dim i As Integer = 0
                For i = 0 To Me.grdXMLData.Rows.Count - 1
                    If CType(grdXMLData.Rows(i).Cells("chk_Select").Value, Boolean) = True Then

                        Dim strFileName_Dr As String = grdXMLData.Rows(i).Cells("Col_Url").Value.ToString
                        Dim strFileName As String = grdXMLData.Rows(i).Cells("Col_FileName").Value.ToString
                        oImport_OrderXML.StartImport_OrderXML(strFileName_Dr)

                        Dim fFile1 As New FileInfo(strFileName_Dr) 'moveFile
                        fFile1.MoveTo(_DEFAULT_MOVEFILE & strFileName)

                    End If
                Next
                W_MSG_Information("นำข้อมูลเข้าเรียบร้อยแล้ว")
            Else
                W_MSG_Information("กรุณาตรวจสอบ Path ")
            End If

            btnImport.Enabled = True

        Catch ex As Exception
            W_MSG_Information("กรุณาตรวจสอบ Path ")
            '  W_MSG_Information(ex.Message.ToString)
        End Try

    End Sub
#Region "CheckPath "
    Function CheckFile(ByVal FileOrFolder As String) As String
        Dim ret As Long

        ret = GetAttr(FileOrFolder)
        CheckFile = "#" & ret

        If CheckFile = "" Then
            CheckFile = ""
        Else
            CheckFile = CheckFile
        End If

    End Function
#End Region



    Sub setDEFAULT_MOVEFILE()

        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("DEFAULT_MOVEFILE", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me._DEFAULT_MOVEFILE = objDT.Rows(0).Item("Config_Value").ToString
            Else
                Me._DEFAULT_MOVEFILE = ""
            End If

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub btnBrowse_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBrowse.Click
        Try
            If OpenFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK Then
                Dim txtfile() As String = OpenFileDialog1.FileNames

                Dim ifile As Integer = txtfile.Length

                For i As Integer = 0 To ifile - 1
                    Dim strFile As String = OpenFileDialog1.FileNames(i)
                    Dim strFileNew() As String = strFile.Split("\")
                    Dim oImport_OrderXML As New Import_OrderXML

                    With Me.grdXMLData

                        Me.grdXMLData.Rows.Add()

                        Dim strReferenceNo As String = strFileNew(5)
                        Dim strReferenceNo1() As String = strReferenceNo.Split(".")

                        Dim strInvoice_No As String = strReferenceNo1(0)

                        If strInvoice_No <> "" Then
                            If oImport_OrderXML.GetReferenceNo(strInvoice_No) = True Then
                                W_MSG_Information("หมายเลข ReferenceNo มีการนำข้อมูลเข้าเรียบร้อยแล้ว ")
                                Exit For
                            End If
                        End If

                        .Rows(grdXMLData.RowCount - 1).Cells("Col_Url").Value = strFile
                        .Rows(grdXMLData.RowCount - 1).Cells("Col_FileName").Value = strFileNew(5)

                    End With
                Next
            End If

        Catch ex As Exception
            W_MSG_Error(ex.Message.ToString)
        End Try

    End Sub

    Private Sub chkSelectAll_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkSelectAll.CheckedChanged
        Try
            Dim i As Integer = 0
            For i = 0 To grdXMLData.Rows.Count - 1
                grdXMLData.Rows(i).Cells("chk_Select").Value = chkSelectAll.Checked
            Next

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmImport_OrderXML_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.setDEFAULT_MOVEFILE()
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class