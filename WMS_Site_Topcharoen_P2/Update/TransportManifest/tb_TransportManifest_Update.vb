Public Class tb_TransportManifest_Update : Inherits WMS_STD_OUTB_Transport_Datalayer.tb_TransportManifest
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String = ""

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

    Private _IsPack As Boolean
    Public Property IsPack() As Boolean
        Get
            Return _IsPack
        End Get
        Set(ByVal value As Boolean)
            _IsPack = value
        End Set
    End Property


    Public Function GetDocNo_TransportManifestItem(ByVal pstrTransportManifest_Index As String) As DataTable
        Dim strSQL As String = " "
        Try
            Dim DT_TEMP As New DataTable
            strSQL = " SELECT   1 as chkSelect,TransportManifest_No,SalesOrder_No,str1  "
            strSQL &= "  FROM VIEW_TransportManifest_OnTruck "
            strSQL &= "  WHERE TransportManifest_No = '" & pstrTransportManifest_Index & "' "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            DT_TEMP = GetDataTable

            Return DT_TEMP
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Shadows Sub getTransportManifest_Detail(ByVal TransportManifest_Index As String, ByVal isSubManifest As Integer)
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT    chkItemOnTruck, * "
            strSQL &= "  FROM VIEW_TransportManifest_OnTruck "
            strSQL &= "  WHERE TransportManifest_Index = '" & TransportManifest_Index & "' "
            strSQL &= "     and IsSubmanifest=" & isSubManifest
            SetSQLString = strSQL & " ORDER BY Str1 "
            connectDB()
            EXEC_DataAdapter()
            _DataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function GetBarcodeB1(ByVal Withdraw_Index As String) As DataTable
        Try
            If String.IsNullOrEmpty(Withdraw_Index) Then
                Throw New Exception("Withdraw_Index not found")
            End If

            Dim SQL As New System.Text.StringBuilder
            'With SQL
            '    .Append(" SELECT [รหัสแม่],[รหัสลูก],[BARCODE],SUM([จำนวน]) as [จำนวน],[เลขที่ ORDER],[เลขที่ใบคุมของ],[เลขที่ใบนำส่งพัสดุ],[วันที่ออกบิล] ")
            '    .Append(" FROM( ")
            '    .Append(" 	SELECT ")
            '    .Append(" 		(CASE SO.SAO_Type ")
            '    .Append(" 			WHEN 'SL' THEN CS.Str1 ")
            '    .Append(" 			WHEN 'CL' THEN CSL.Customer_Shipping_Location_Id END )as [รหัสแม่] ")
            '    .Append(" 		,CSL.Customer_Shipping_Location_Id as [รหัสลูก] ")
            '    .Append(" 		,(CASE  ")
            '    .Append(" 			WHEN SO_Type = 'Z' AND SOI.Str10 = 'L' THEN SK.ItemCode_1 ")
            '    .Append(" 			WHEN SO_Type = 'Z' AND SOI.Str10 = 'R' THEN SK.ItemCode_2 ")
            '    .Append(" 			WHEN SO_Type = 'Y' AND SOI.Str10 = 'L' THEN SO.ItemCode_1 ")
            '    .Append(" 			WHEN SO_Type = 'Y' AND SOI.Str10 = 'R' THEN SO.ItemCode_2  ")
            '    .Append(" 			WHEN SO_Type = 'X' THEN (select top 1 SOP.Barcode from tb_SalesOrderPacking_Barcode SOP WHERE SOP.SalesOrder_Index = SO.SalesOrder_Index) ")
            '    .Append(" 			END) as [BARCODE] ")
            '    .Append(" 		,SOI.Total_Qty as [จำนวน],SO.SalesOrder_No as [เลขที่ ORDER] ")
            '    .Append(" 		,(select top 1 SOP.Barcode_BAG from tb_SalesOrderPackingItem SOP WHERE SOP.SalesOrder_Index = SO.SalesOrder_Index) as [เลขที่ใบคุมของ] ")
            '    .Append(" 		,(select top 1 SOP.Barcode_BOX from tb_SalesOrderPackingItem SOP WHERE SOP.SalesOrder_Index = SO.SalesOrder_Index) as [เลขที่ใบนำส่งพัสดุ] ")
            '    .Append(" 		,(select top 1 SOP.DateAdd  ")
            '    .Append(" 			from tb_SalesOrderPacking SOP INNER JOIN tb_SalesOrderPackingItem SPI ON  SOP.SalesOrderPacking_Index = SPI.SalesOrderPacking_Index ")
            '    .Append(" 			WHERE SPI.SalesOrder_Index = SO.SalesOrder_Index) as [วันที่ออกบิล] ")
            '    .Append(" 		FROM tb_SalesOrder SO ")
            '    .Append(" 		INNER JOIN tb_SalesOrderItem SOI ON SO.SalesOrder_Index = SOI.SalesOrder_Index ")
            '    .Append(" 		INNER JOIN ms_SKU SK ON SOI.Sku_Index = SK.Sku_Index ")
            '    .Append(" 		LEFT JOIN ms_Customer_Shipping CS ON CS.Customer_Shipping_Index = SO.Customer_Shipping_Index ")
            '    .Append(" 		LEFT JOIN ms_Customer_Shipping_Location CSL ON CSL.Customer_Shipping_Location_Index = SO.Customer_Shipping_Location_Index ")
            '    .Append(" 		WHERE 1 = 1 ")
            '    .Append(" 		AND SO.SalesOrder_Index IN ( SELECT SalesOrder_Index FROM tb_TransportManifestItem WHERE Status <> -1 AND TransportManifest_Index = @TransportManifest_Index ) ")
            '    .Append(" 		AND (select count(*) from tb_SalesOrderPackingItem SOP WHERE SOP.SalesOrder_Index = SO.SalesOrder_Index AND ISNULL(SOP.Barcode_BOX,'') <> '') > 0 ")
            '    .Append(" ) DTW ")
            '    .Append(" GROUP BY [รหัสแม่], [รหัสลูก], [BARCODE], [เลขที่ใบคุมของ], [เลขที่ ORDER], [เลขที่ใบนำส่งพัสดุ], [วันที่ออกบิล] ")
            'End Withs

            With SQL
                .Append(" SELECT * FROM VIEW_RCP_EXPORT_MINIFEST_DTW ")
                .Append(" WHERE Withdraw_Index = @Withdraw_Index ")
            End With

            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Withdraw_Index", SqlDbType.VarChar).Value = Withdraw_Index
            End With

            Return DBExeQuery(SQL.ToString)

        Catch Ex As Exception
            Throw Ex
        End Try
    End Function

End Class
