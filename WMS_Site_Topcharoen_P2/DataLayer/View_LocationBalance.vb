Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports System.Text

Public Class View_LocationBalance : Inherits DBType_SQLServer
    Private _dataTable As DataTable = New DataTable
    Private _scalarOutput As String

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property
    Public ReadOnly Property ScalarOutput() As String
        Get
            Return _scalarOutput
        End Get
    End Property


    Public Sub SearchData_Click(ByVal strWhere As String)
        Dim strSQL As String = ""

        Try

            strSQL = " SELECT  *"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
            ''  If strWhere <> "" Then
            'If strWhere <> Nothing Then

            '    strSQL &= " WHERE 1=1 " & strWhere
            'End If

            SetSQLString = strSQL & strWhere
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    'Public Function CHECK_QTY_BALANCE(ByVal strWhere As String) As decimal
    '    Dim strSQL As String = ""

    '    Try

    '        strSQL = " SELECT  Sum(Qty_Bal) as Qty_Bal"
    '        strSQL &= " FROM  VIEW_WithdrawReserveLocation "

    '        If strWhere <> "" Then
    '            strSQL &= " WHERE 1=1 " & strWhere
    '        End If

    '        SetSQLString = strSQL
    '        SetCommandType = DBType_SQLServer.enuCommandType.Text
    '        SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
    '        connectDB()
    '        EXEC_Command()
    '        _scalarOutput = GetScalarOutput

    '        If _scalarOutput.Trim = "0" Or _scalarOutput.Trim = "" Then
    '            Return 0
    '        Else
    '            Return Val(_scalarOutput)
    '        End If

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Function

    'Public Sub SearchLocationBalance_Picking(ByVal strWhere As String)
    '    Dim strSQL As String = ""

    '    Try
    '        'strSQL = " SELECT  Withdraw_Index, WithdrawItem_Index, Order_Index, Sku_Index, Lot_No, Plot, ItemStatus_Index, Tag_No, LocationBalance_Index, Location_Index,Package_Index,NewItem ,"
    '        'strSQL &= " Serial_No,QTY_BAL, Qty, Sku_Package_Index, Total_Qty,WeightOut, VolumeOut, Pallet_Qty,  QtyItemOut, Price_Out, Sku_Id, Sku_Description, ItemStatus_Description, "
    '        'strSQL &= " Mfg_Date,Exp_Date,IsMfg_Date,IsExp_Date, Location_Alias,MixPallet,Pallet_Name,OrderItem_Index, "
    '        'strSQL &= " Pallet_No, Invoice_No, Invoice_Out, Declaration_No,HandlingType_Index, Item_Package_Index,Description, Sku_PackageDescription"
    '        'strSQL &= " FROM  VIEW_WithdrawReserveLocation "

    '        strSQL = " SELECT  *"
    '        strSQL &= " FROM  VIEW_WithdrawReserveLocation "

    '        If strWhere <> "" Then
    '            strSQL &= " WHERE 1=1 " & strWhere
    '        End If

    '        SetSQLString = strSQL
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub


    Public Sub SearchWithDraw_PackingItem(ByVal strWhere As String)
        Dim sbSQL As New StringBuilder()
        Dim strOrder As String = " Order By DocumentPlan_NO"
        Try
            sbSQL.Append(" select *")
            sbSQL.Append(" from VIEW_WithDraw_Packing")
            If strWhere <> "" Then
                sbSQL.Append(" WHERE " & strWhere)
            End If

            SetSQLString = sbSQL.ToString & strOrder
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub SearchWithDraw_PackingHeader(ByVal strWhere As String)
        Dim sbSQL As New StringBuilder()
        Dim strOrder As String = " Order By DocumentPlan_NO"
        Try
            sbSQL.Append(" select *")
            sbSQL.Append(" from VIEW_Packing_WithDraw")
            If strWhere <> "" Then
                sbSQL.Append(" WHERE " & strWhere)
            End If

            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchWithDraw_SO(ByVal strWhere As String)
        Dim sbSQL As New StringBuilder()
        Dim strOrder As String = " Order By DocumentPlan_NO "
        Try

            sbSQL.Append(" select *")
            sbSQL.Append(" from VIEW_WithDraw_SalesOrder ")

            If strWhere <> "" Then
                sbSQL.Append(" WHERE " & strWhere)
            End If

            SetSQLString = sbSQL.ToString & strOrder
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' 15-01-2010
    ''' ja
    ''' update  SearchWithDraw_Reserve auto withdraw
    ''' <remarks></remarks>
    Public Sub SearchWithDraw_Reserve(ByVal strWhere As String)
        Dim sbSQL As New StringBuilder()
        Dim strOrder As String = " Order By DocumentPlan_No "
        Try

            sbSQL.Append(" select *")
            sbSQL.Append(" from VIEW_WithDraw_Reserve ")

            If strWhere <> "" Then
                sbSQL.Append(" WHERE " & strWhere)
            End If

            SetSQLString = sbSQL.ToString & strOrder
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchWithdrawItemLocation_ReserveWithdrawItemLocation(ByVal Reserve_Index As String)
        Dim sbSQL As New StringBuilder()

        Try

            sbSQL.Append(" select NewItem=0,*")
            sbSQL.Append(" from  View_WithDrawReserveLocation_AutoReserve ")
            sbSQL.Append(" where Reserve_Index = '" & Reserve_Index & "' ")

            'sbSQL.Append(" where LocationBalance_Index in ")
            'sbSQL.Append(" (select LocationBalance_Index from tb_ReserveLocation ")
            'sbSQL.Append(" where Reserve_Index = '" & Reserve_Index & "') ")

            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    Public Sub SearchWithDraw_Plan_SO_WithSubRoute(ByVal strType_Plan As String, ByVal strDocument_No As String, ByVal Process_Id As Integer, Optional ByVal pstrRoute_Index As String = "-11", Optional ByVal pstrSubRoute_Index As String = "-11", Optional ByVal pstrDistributionCenter_Index As String = "-11", Optional ByVal pstrCustomer_Index As String = "")
        Dim sbSQL As New StringBuilder()
        Try
            Dim _strType_Plan As String = strType_Plan
            If (strType_Plan = "TM") Then
                strType_Plan = "SALE"
            End If
            sbSQL.Append("  SELECT  chkSelect = 0, SO1.SalesOrder_Index as Document_Index, SO1.SalesOrder_No as Document_No, SO1.SalesOrder_date as Document_Date, ms_DocumentType.Description AS Document_Type,'" & strType_Plan & "' AS Plan_Type,'" & Process_Id & "' AS Process_Id,0.0 as Qty_Bal")
            sbSQL.Append("  , SO1.SubRoute_Desc, SO1.Route_Desc, SO1.Postcode, SO1.Province ,SO1.Expected_Delivery_Date ")
            sbSQL.Append("  , SO1.TransportManifest_No, SO1.TransportManifest_Date ")
            sbSQL.Append("  FROM    VIEW_SO_WithSubRoute_Postcode SO1  LEFT OUTER  JOIN ")
            sbSQL.Append("              ms_DocumentType ON SO1.DocumentType_Index = ms_DocumentType.DocumentType_Index")
            sbSQL.Append("  WHERE 1 = 1 ")


            If pstrRoute_Index <> "-11" Then
                sbSQL.Append(" AND SO1.Route_Index = '" & pstrRoute_Index & "'")
            End If


            If pstrSubRoute_Index <> "-11" Then
                sbSQL.Append(" AND SO1.SubRoute_Index = '" & pstrSubRoute_Index & "'")
            End If

            If pstrDistributionCenter_Index <> "-11" Then
                sbSQL.Append(" AND SO1.DistributionCenter_Index = '" & pstrDistributionCenter_Index & "'")
            End If

            If strDocument_No <> "" Then
                If (_strType_Plan = "TM") Then
                    sbSQL.Append(" AND SO1.TransportManifest_No like '%" & strDocument_No & "%'")
                Else
                    sbSQL.Append(" AND SO1.SalesOrder_No like '%" & strDocument_No & "%'")
                End If
            End If

            If pstrCustomer_Index <> "" Then
                sbSQL.Append(" AND SO1.Customer_Index='" & pstrCustomer_Index & "'")
            End If

            'sbSQL.Append(" and  SO1.Process_Id in (10) and SO1.Status in (2,6,8)  and  SO1.SalesOrder_Index  in (")
            sbSQL.Append(" and  SO1.Process_Id in (" & Process_Id & ") and SO1.Status in (2,6)  and  SO1.SalesOrder_Index  in (")
            sbSQL.Append(" Select  SalesOrder_Index")
            sbSQL.Append(" from  tb_SalesOrderitem ")
            sbSQL.Append(" group by SalesOrder_Index ")
            sbSQL.Append(" having (sum(Total_Qty)-sum(Total_Qty_WithDraw))  > 0)")

            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub SearchWithDraw_Plan(ByVal strDocument_No As String, ByVal USE_PACKING_NEW_PRODUCTION As Boolean, Optional ByVal strCustomer_Index As String = "")
        Dim sbSQL As New StringBuilder()
        Try
            Select Case USE_PACKING_NEW_PRODUCTION
                Case False
                    ''''''' ของเก่า ''''''
                    'sbSQL.Append("  SELECT  chkSelect = 0, tb_Packing.Packing_Index as Document_Index, tb_Packing.Packing_No as Document_No, tb_Packing.Packing_date as Document_Date, ms_DocumentType.Description AS Document_Type,'PACKING' AS Plan_Type,'7' AS Process_Id")
                    ''sbSQL.Append("          , dbo.tb_Packing.Qty_Product as Qty_Bal")
                    ''sbSQL.Append("          , (SUM(dbo.tb_PackingItem.Qty))  / COUNT(dbo.tb_Packing.Packing_Index)  AS Qty_Per_Row")
                    ''sbSQL.Append("          , (SUM(dbo.tb_PackingItem.Qty)) / dbo.tb_Packing.Qty_Product / COUNT(dbo.tb_Packing.Packing_Index)  AS Qty_Per_Pack")
                    'sbSQL.Append("          , convert(float,((SUM(dbo.tb_PackingItem.Qty) - SUM(dbo.tb_PackingItem.Qty_WithDraw)) / (COUNT(dbo.tb_Packing.Packing_Index))) / ((SUM(dbo.tb_PackingItem.Qty) / dbo.tb_Packing.Qty_Product) / COUNT(dbo.tb_Packing.Packing_Index))) AS Qty_Bal")
                    'sbSQL.Append("  FROM         dbo.tb_Packing INNER JOIN")
                    'sbSQL.Append("                       dbo.tb_PackingItem ON dbo.tb_Packing.Packing_Index = dbo.tb_PackingItem.Packing_Index LEFT OUTER JOIN")
                    'sbSQL.Append("                       dbo.ms_DocumentType ON dbo.tb_Packing.DocumentType_Index = dbo.ms_DocumentType.DocumentType_Index")
                    'sbSQL.Append("  WHERE     (1 = 1) AND (dbo.tb_Packing.status IN (2, 6))")
                    'If strDocument_No <> "" Then
                    '    sbSQL.Append(" AND tb_Packing.Packing_No like '" & strDocument_No & "%'")
                    'End If
                    'sbSQL.Append("  GROUP BY dbo.tb_Packing.Packing_Index, dbo.tb_Packing.Packing_No, dbo.tb_Packing.Packing_date, dbo.ms_DocumentType.Description, dbo.tb_Packing.Qty_Product")
                    'sbSQL.Append("                      HAVING(SUM(dbo.tb_PackingItem.Qty) - SUM(dbo.tb_PackingItem.Qty_WithDraw) > 0)")
                    ''''''''''''''''''''''''''''''''''''''''''''''''''''''''

                    sbSQL.Append("  SELECT  chkSelect = 0, tb_Packing.Packing_Index as Document_Index, tb_Packing.Packing_No as Document_No, tb_Packing.Packing_date as Document_Date, ms_DocumentType.Description AS Document_Type,'PACKING' AS Plan_Type,'7' AS Process_Id")
                    sbSQL.Append("          , dbo.tb_Packing.Qty_Product as Qty_Product") 'จำนวนที่สั่งเบิก
                    sbSQL.Append("          , (SUM(dbo.tb_PackingItem.Qty))  / Qty_Product AS Qty_Per_Pack") 'จำนวนต่อ Pack
                    sbSQL.Append("          , (SUM(dbo.tb_PackingItem.Qty_WithDraw)) / dbo.tb_Packing.Qty_Product as Qty_WithDraw ") 'จำนวนที่เบิกแล้ว
                    sbSQL.Append("          , (SUM(dbo.tb_PackingItem.Qty) - SUM(dbo.tb_PackingItem.Qty_WithDraw)) / ((SUM(dbo.tb_PackingItem.Qty))  / Qty_Product)  AS Qty_Bal") 'จำนวนที่เบิกได้
                    sbSQL.Append("  FROM         dbo.tb_Packing INNER JOIN")
                    sbSQL.Append("                       dbo.tb_PackingItem ON dbo.tb_Packing.Packing_Index = dbo.tb_PackingItem.Packing_Index LEFT OUTER JOIN ")
                    sbSQL.Append("                       dbo.ms_DocumentType ON dbo.tb_Packing.DocumentType_Index = dbo.ms_DocumentType.DocumentType_Index ")
                    sbSQL.Append(" WHERE  1=1 ")
                    sbSQL.Append("  AND (dbo.tb_Packing.status IN (2, 6))")
                    If strDocument_No <> "" Then
                        sbSQL.Append(" AND tb_Packing.Packing_No like '" & strDocument_No & "%'")
                    End If

                    If strCustomer_Index <> "" Then
                        sbSQL.Append(" AND tb_Packing.Customer_Index = '" & strCustomer_Index & "'")
                    End If

                    sbSQL.Append("  GROUP BY dbo.tb_Packing.Packing_Index, dbo.tb_Packing.Packing_No, dbo.tb_Packing.Packing_date, dbo.ms_DocumentType.Description, dbo.tb_Packing.Qty_Product")
                    sbSQL.Append("                      HAVING(SUM(dbo.tb_PackingItem.Qty) - SUM(dbo.tb_PackingItem.Qty_WithDraw) > 0)")

                Case True
                    sbSQL.Append("  SELECT  chkSelect = 0, tb_Packing.Packing_Index as Document_Index, tb_Packing.Packing_No as Document_No, tb_Packing.Packing_date as Document_Date, ms_DocumentType.Description AS Document_Type,'PACKING' AS Plan_Type,'7' AS Process_Id")
                    'sbSQL.Append("  , 0 as Qty_Bal")
                    sbSQL.Append("  FROM    tb_Packing LEFT OUTER  JOIN ")
                    sbSQL.Append("              ms_DocumentType ON tb_Packing.DocumentType_Index = ms_DocumentType.DocumentType_Index")
                    sbSQL.Append("  WHERE 1 = 1 ")
                    If strDocument_No <> "" Then
                        sbSQL.Append(" AND tb_Packing.Packing_No like '" & strDocument_No & "%'")
                    End If

                    sbSQL.Append("  and  tb_Packing.Status in (2,6) ")
                    sbSQL.Append("  and  tb_Packing.Packing_Index not in (  SELECT  tb_WithdrawItem.DocumentPlan_Index")
                    sbSQL.Append("                                          FROM    tb_WithdrawItem INNER JOIN")
                    sbSQL.Append("                                                  tb_Withdraw ON tb_WithdrawItem.Withdraw_Index = tb_Withdraw.Withdraw_Index")
                    sbSQL.Append("                                          WHERE   tb_WithdrawItem.Plan_Process = 7 and tb_Withdraw.Status not in (-1) ) ")
            End Select


            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub



    'Public Sub SearchWithDraw_Plan(ByVal tableName_Notb As String, ByVal strType_Plan As String, ByVal strDocument_No As String, ByVal Process_Id As Integer)
    '    Dim sbSQL As New StringBuilder()
    '    Try

    '        sbSQL.Append("  SELECT  chkSelect = 0, tb_" & tableName_Notb & "." & tableName_Notb & "_Index as Document_Index, tb_" & tableName_Notb & "." & tableName_Notb & "_No as Document_No, tb_" & tableName_Notb & "." & tableName_Notb & "_date as Document_Date, ms_DocumentType.Description AS Document_Type,'" & strType_Plan & "' AS Plan_Type,'" & Process_Id & "' AS Process_Id")
    '        sbSQL.Append("  FROM    tb_" & tableName_Notb & "  LEFT OUTER  JOIN ")
    '        sbSQL.Append("              ms_DocumentType ON tb_" & tableName_Notb & ".DocumentType_Index = ms_DocumentType.DocumentType_Index")
    '        sbSQL.Append("  WHERE 1 = 1 ")
    '        If strDocument_No <> "" Then
    '            sbSQL.Append(" AND tb_" & tableName_Notb & "." & tableName_Notb & "_No like '" & strDocument_No & "%'")
    '        End If

    '        sbSQL.Append(" and  tb_" & tableName_Notb & ".Status = 2  and  tb_" & tableName_Notb & "." & tableName_Notb & "_Index  in (")
    '        sbSQL.Append(" Select  " & tableName_Notb & "_Index")
    '        sbSQL.Append(" from  tb_" & tableName_Notb & "item ")
    '        sbSQL.Append(" group by " & tableName_Notb & "_Index ")
    '        sbSQL.Append(" having (sum(Qty)-sum(Qty_WithDraw))  > 0)")

    '        SetSQLString = sbSQL.ToString
    '        connectDB()
    '        EXEC_DataAdapter()
    '        _dataTable = GetDataTable

    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub


    Public Sub SearchWithDraw_Plan(ByVal DocumentPlan_NO As String)

        Dim objPlan As New View_LocationBalance
        Dim sbSQL As New StringBuilder()
        Try

            sbSQL.Append("  SELECT  * from VIEW_WithDraw_SalesOrder where DocumentPlan_NO ='" & DocumentPlan_NO & "'")

            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub



    Public Sub Fn_WithDraw_Real(ByVal pstrPackingBom_Index As String)
        'Dim strSQL As New StringBuilder()
        Dim strSQL As String = ""
        Try
            'sbSQL.Append("  SELECT ISNULL(Floor(SUM(ISNULL(LB.Qty_Bal / Qty,0))),0) As Qty_Min_Pack ")
            'sbSQL.Append("  FROM   tb_PackingItem BI LEFT JOIN  tb_LocationBalance LB  ON BI.SKU_Index = LB.SKU_Index ")
            'sbSQL.Append("  WHERE BI.Packing_Index = '" & pstrPackingBom_Index & "'")

            strSQL = " SELECT ISNULL(Floor(MIN(ISNULL(Qty_Bal_Per_Pck,0))),0) as  Min_Bal_Per_Pck"
            strSQL &= "  FROM ("
            strSQL &= "     Select LB.SKU_Index"
            strSQL &= " 	,PKI.Qty as Qty"
            strSQL &= " 	,PK.Qty_Product as Qty_Product"
            strSQL &= " 	,PKI.Qty / PK.Qty_Product as Qty_Per_Pck"
            strSQL &= " 	,SUM(ISNULL(LB.Qty_Bal,0)) as Qty_Bal"
            strSQL &= " 	,SUM(ISNULL(LB.Qty_Bal,0)) / (PKI.Qty / PK.Qty_Product)  as Qty_Bal_Per_Pck"
            strSQL &= " 	FROM   tb_PackingItem PKI LEFT JOIN"
            strSQL &= " 			View_WithDrawReserveLocation LB  ON PKI.SKU_Index = LB.SKU_Index   LEFT JOIN"
            strSQL &= " 			tb_Packing PK  ON PK.Packing_Index = PKI.Packing_Index"
            strSQL &= " 	WHERE PKI.Packing_Index = '" & pstrPackingBom_Index & "'"
            strSQL &= " 	GROUP by  LB.SKU_Index,PK.Qty_Product,PKI.Qty"
            strSQL &= " ) PCK"



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


    Public Sub getView_Packing_Header(ByVal Packing_Index As String)
        Dim sbSQL As New System.Text.StringBuilder()
        Try
            sbSQL.Append(" select *")
            sbSQL.Append(" from VIEW_Packing_Header")
            sbSQL.Append(" where Packing_Index ='" & Packing_Index & "'")

            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Sub getView_Packing_DetailBalance(ByVal Packing_Index As String)
        Dim sbSQL As New System.Text.StringBuilder()
        Try

            sbSQL.Append(" select *,(SELECT SUM(ISNULL(LB.Qty_Bal,0)) FROM tb_LocationBalance LB WHERE VIEW_Packing_Detail.SKU_Index = LB.SKU_Index) as Qty_Bal")
            sbSQL.Append(" from VIEW_Packing_Detail")
            sbSQL.Append(" where Packing_Index ='" & Packing_Index & "' order by Packing_Index asc")

            SetSQLString = sbSQL.ToString
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
End Class
