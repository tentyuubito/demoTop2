Imports Microsoft.VisualBasic 

Public Class cl_ServerControl
    Private Function identifier(ByVal wmiClass As String, ByVal wmiProperty As String, ByVal wmiMustBeTrue As String) As String
        Dim result As String = ""
        Dim mc As New System.Management.ManagementClass(wmiClass)
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances()
        For Each mo As System.Management.ManagementObject In moc
            If mo(wmiMustBeTrue).ToString() = "True" Then
                'Only get the first one
                If result = "" Then
                    Try
                        result = mo(wmiProperty).ToString()
                        Exit Try
                    Catch
                    End Try
                End If
            End If
        Next
        Return result
    End Function

    Private Shared Function identifier(ByVal wmiClass As String, ByVal wmiProperty As String) As String
        Dim result As String = ""
        Dim mc As New System.Management.ManagementClass(wmiClass)
        Dim moc As System.Management.ManagementObjectCollection = mc.GetInstances()
        For Each mo As System.Management.ManagementObject In moc
            'Only get the first one
            If result = "" Then
                Try
                    result = mo(wmiProperty).ToString()
                    Exit Try
                Catch
                End Try
            End If
        Next
        Return result
    End Function

    Public Function GetProcessorID() As String
        Return identifier("Win32_Processor", "ProcessorID")
    End Function

End Class
