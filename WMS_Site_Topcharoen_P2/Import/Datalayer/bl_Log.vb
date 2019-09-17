Imports System.IO
Imports System.Text

Public Class bl_Log

#Region "   Property   "

    Private _Start_Process As String = ""
    Property Start_Process() As String
        Get
            Return _Start_Process
        End Get
        Set(ByVal value As String)
            _Start_Process = value
        End Set
    End Property

    Private _Process_Name As String = ""
    Property Process_Name() As String
        Get
            Return _Process_Name
        End Get
        Set(ByVal value As String)
            _Process_Name = value
        End Set
    End Property

    Private _Module_Name As String = ""
    Property Module_Name() As String
        Get
            Return _Module_Name
        End Get
        Set(ByVal value As String)
            _Module_Name = value
        End Set
    End Property

    Private _Target As String = ""
    Property Target() As String
        Get
            Return _Target
        End Get
        Set(ByVal value As String)
            _Target = value
        End Set
    End Property

    Private _Destination As String = ""
    Property Destination() As String
        Get
            Return _Destination
        End Get
        Set(ByVal value As String)
            _Destination = value
        End Set
    End Property

    Private _Total_Header As Integer = 0
    Property Total_Header() As Integer
        Get
            Return _Total_Header
        End Get
        Set(ByVal value As Integer)
            _Total_Header = value
        End Set
    End Property

    Private _Total_Detail As Integer = 0
    Property Total_Detail() As Integer
        Get
            Return _Total_Detail
        End Get
        Set(ByVal value As Integer)
            _Total_Detail = value
        End Set
    End Property

    Private _Complete_Header As Integer = 0
    Property Complete_Header() As Integer
        Get
            Return _Complete_Header
        End Get
        Set(ByVal value As Integer)
            _Complete_Header = value
        End Set
    End Property

    Private _Complete_Detail As Integer = 0
    Property Complete_Detail() As Integer
        Get
            Return _Complete_Detail
        End Get
        Set(ByVal value As Integer)
            _Complete_Detail = value
        End Set
    End Property

    Private _Incomplete_Header As Integer = 0
    Property Incomplete_Header() As Integer
        Get
            Return _Incomplete_Header
        End Get
        Set(ByVal value As Integer)
            _Incomplete_Header = value
        End Set
    End Property

    Private _Incomplete_Detail As Integer = 0
    Property Incomplete_Detail() As Integer
        Get
            Return _Incomplete_Detail
        End Get
        Set(ByVal value As Integer)
            _Incomplete_Detail = value
        End Set
    End Property

    Private _Error_List As New StringBuilder
    Property Error_List() As StringBuilder
        Get
            Return _Error_List
        End Get
        Set(ByVal value As StringBuilder)
            _Error_List = value
        End Set
    End Property

    Private _Write_To_Path As String = ""
    Property Write_To_Path() As String
        Get
            Return _Write_To_Path
        End Get
        Set(ByVal value As String)
            _Write_To_Path = value
        End Set
    End Property

#End Region

    Public Sub Write_Log()
        Dim oWrite As StreamWriter

        Try

            oWrite = File.CreateText(Me.Write_To_Path)

            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("[0] Event Log Kasco WMS Ver.3")
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("")
            oWrite.WriteLine("Start Process  = " & Me.Start_Process.Trim)
            oWrite.WriteLine("")
            oWrite.WriteLine("Process = " & Me.Process_Name.Trim)
            oWrite.WriteLine("")
            oWrite.WriteLine("Module = " & Me.Module_Name.Trim)
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("[1] Path File Data")
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("")
            oWrite.WriteLine("Target  = " & Me.Target.Trim)
            oWrite.WriteLine("")
            oWrite.WriteLine("Destination = " & Me.Destination.Trim)
            oWrite.WriteLine("")
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("[2] Result")
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("")
            oWrite.WriteLine("Total Header = " & Me.Total_Header)
            oWrite.WriteLine("")
            oWrite.WriteLine("Total Detail =" & Me.Total_Detail)
            oWrite.WriteLine("")
            oWrite.WriteLine("Complete (Header) =" & Me.Complete_Header)
            oWrite.WriteLine("")
            oWrite.WriteLine("Complete (Detail) =" & Me.Complete_Detail)
            oWrite.WriteLine("")
            oWrite.WriteLine("Incomplete (Header) = " & Me.Incomplete_Header)
            oWrite.WriteLine("")
            oWrite.WriteLine("Incomplete (Detail) = " & Me.Incomplete_Detail)
            oWrite.WriteLine("")
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("[3] Error List ")
            oWrite.WriteLine("******************************************************************************")
            oWrite.WriteLine("")
            oWrite.WriteLine(Me.Error_List.ToString)

        Catch ex As Exception
            Throw ex
        Finally

            'oWrite.Close()
            'oWrite.Dispose()
        End Try
    End Sub

End Class
