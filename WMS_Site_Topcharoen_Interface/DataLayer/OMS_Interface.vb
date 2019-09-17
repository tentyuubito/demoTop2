Public Class OMS_Interface

    Private Const SERVICE_RESULT_FAILED As String = "ไม่สามารถเชื่อมต่อ WebService ได้ กรุณาตรวจสอบใหม่อีกครั้ง"

    Public Enum eOMSMasterID
        Branch = 0
        Product
    End Enum

    Public Sub GetMaster(ByVal MasterID As eOMSMasterID)
        Try
            Dim Response As OMS.ReturnValue
            Dim InterfaceName As String

            Using Service As New OMS.OMSApi
                Service.Timeout = 30000
                Select Case MasterID
                    Case eOMSMasterID.Branch
                        Response = Service.GetBranchList()
                        InterfaceName = "GetBranchList"

                    Case eOMSMasterID.Product
                        Response = Service.GetProductList()
                        InterfaceName = "GetProductList"

                    Case Else
                        Throw New Exception("Invalid MasterID")
                End Select
            End Using

            If Response Is Nothing Then
                Throw New Exception(SERVICE_RESULT_FAILED)
            End If

            Dim DataCount As UInteger
            If WMS_Function.DataTableHasValue(Response.Data) Then
                DataCount = Response.Data.Rows.Count
            Else
                DataCount = 0
            End If

            Dim GuID_Log As String = String.Empty

            Dim MyInterface As New WMS_Interface
            Try
                GuID_Log = MyInterface.InsertOMS_API_Master_Log(InterfaceName, Response.Result, Response.Message, DataCount)

            Catch Ex As Exception
                'Do Nothing when Exception
                'Just Logging
            End Try

            If Not Response.Result OrElse DataCount = 0 Then
                'Result is False Or Data is 0 Record
                Exit Sub
            End If

            MyInterface.ImportMaster(MasterID, GuID_Log, Response.Data.Copy)

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

    Public Sub GetSalesOrder(ByVal SO_Type As String)
        Try
            Dim Response As OMS.ReturnValue
            Dim InterfaceName As String

            Using Service As New OMS.OMSApi
                Response = Service.GetOrderList(SO_Type)
                InterfaceName = "GetOrderList"
                Dim count = Response.Data.Rows.Count
            End Using

            If Response Is Nothing Then
                Throw New Exception(SERVICE_RESULT_FAILED)
            End If

            Dim DataCount As UInteger
            If WMS_Function.DataTableHasValue(Response.Data) Then
                DataCount = Response.Data.Rows.Count
            Else
                DataCount = 0
            End If

            Dim GuID_Log As String = String.Empty

            Dim MyInterface As New WMS_Interface
            Try
                GuID_Log = MyInterface.InsertOMS_API_Master_Log(InterfaceName, Response.Result, Response.Message, DataCount)

            Catch Ex As Exception
                'Do Nothing when Exception
                'Just Logging
            End Try

            If Not Response.Result OrElse DataCount = 0 Then
                'Result is False Or Data is 0 Record
                Exit Sub
            End If

            MyInterface.ImportSalesOrder(GuID_Log, Response.Data.Copy, SO_Type)

            Dim Import As New clsImport
            Import.UpdateSOType()

        Catch Ex As Exception
            Throw Ex
        End Try
    End Sub

End Class
