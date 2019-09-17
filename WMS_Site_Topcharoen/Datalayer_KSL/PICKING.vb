Imports WMS_Std_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_Std_master.W_Language
Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports System.Data
Imports WMS_STD_OUTB_Datalayer

Public Class PICKING : Inherits DBType_SQLServer
    'Public useAutoAdjust As Boolean = False
    Private Document_Index As String = ""
    Private Process_Id As Integer = -9

    Private _DocumentType_Index As String = ""
    Private _dtPicking As New DataTable ' PICKING
    Private _enmPicking_Type As enmPicking_Type  ' Need
    Private _dataTable As DataTable = New DataTable
    Private _strCondition As String = "" ' Need
    Private _dblQty_PICKING As Decimal = 0 ' Need
    Private chkBalance As Boolean = False ' Need

    Private _dblQty_BALANCE As Decimal = 0 ' Need
    Private _strSku_Index As String = "" ' Need
    Private _strPackage_Index As String = "" ' Need
    Private _AssetLocationBalance_Index As String = "" ' Need
    '--ADDNEW V3--
    Private _strOrderForV3 As String
    Public Property strOrderForV3() As String
        Get
            Return _strOrderForV3
        End Get
        Set(ByVal value As String)
            _strOrderForV3 = value
        End Set
    End Property
    '--END--

    Enum enmPicking_Type
        CUSTOM = 0
        FIFO = 1
        LIFO = 2
        FEFO = 3
        SERIAL = 4
        PICKFACE = 5
        FEFO1 = 6
    End Enum

    Enum enmPicking_Action
        ADDRESERVE = 0
        DELRESERVE = 1
        ADDBALANCE = 2
        DELBALANCE = 3
        ADDBALANCE_RESERVE = 4
        DELBALANCE_RESERVE = 5
    End Enum

    Enum enmMode_Pick
        EQUALS
        OVER
        LESS
        OVER_EQUALS
        LESS_EQUALS
    End Enum

    Private _scalarOutput As String = ""
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

    Public Sub ReWeightBalance()
        Dim strSQL As String = ""
        Try
            strSQL = " EXEC sp_ReWeightBalance"
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub
    Public Sub getSKU_PackageReceive(ByVal Sku_Index As String, ByVal Package_Index As String)

        ' *** define value ***


        Dim strSQL As String = ""
        Try

            strSQL = "  SELECT      ms_SKURatio.Sku_Index,  ms_SKURatio.Package_Index, ms_Package.Description AS Package ,  ms_SKURatio.Ratio, ms_SKURatio.Package_Index as Recieve_Package_Index"
            strSQL &= "             FROM        ms_SKURatio INNER JOIN  "
            strSQL &= "                         ms_SKU ON ms_SKURatio.Sku_Index =ms_SKU.Sku_Index INNER JOIN "
            strSQL &= "                         ms_Package ON ms_SKURatio.Package_Index = ms_Package.Package_Index"
            strSQL &= "             WHERE       ms_SKURatio.Sku_Index ='" & Sku_Index & "'"
            strSQL &= "                         AND ms_SKURatio.status_id <> -1 AND ms_SKU.Status_Id <> -1 AND  isNull(isItem_Package,0) = 0"
            'strSQL &= "             and        ((select ratio from ms_SKURatio where Package_Index='" & Package_Index & "') >= ms_SKURatio.Ratio)"
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


    Public Property DBLQTY_BALANCE() As Decimal
        Get
            Return _dblQty_BALANCE
        End Get
        Set(ByVal value As Decimal)
            _dblQty_BALANCE = value
        End Set
    End Property

    Public Sub New(ByVal enmPicking_Type As enmPicking_Type)
        MyBase.New()
        _enmPicking_Type = enmPicking_Type
    End Sub

    Public Sub New(ByVal enmPicking_Type As enmPicking_Type, ByVal strCondition As String)
        MyBase.New()
        _strCondition = strCondition
        _enmPicking_Type = enmPicking_Type

    End Sub

    'Public Sub New(ByVal enmPicking_Type As enmPicking_Type, ByVal strSku_Index As String, ByVal strPackage_Index As String, ByVal dblQty_PICKING As Decimal, ByVal strCondition As String)
    '    MyBase.New()
    '    _strCondition = strCondition & " AND SKU_Index='" & strSku_Index & "'"

    '    _enmPicking_Type = enmPicking_Type
    '    _strPackage_Index = strPackage_Index
    '    _strSku_Index = strSku_Index
    '    _dblQty_PICKING = dblQty_PICKING

    'End Sub
    Public Sub New(ByVal enmPicking_Type As enmPicking_Type, ByVal strSku_Index As String, ByVal strPackage_Index As String, ByVal dblQty_PICKING As Decimal, ByVal strCondition As String, ByVal strDocumentType_Index As String)
        MyBase.New()
        _strCondition = strCondition & " AND SKU_Index='" & strSku_Index & "'"

        _enmPicking_Type = enmPicking_Type
        _strPackage_Index = strPackage_Index
        _strSku_Index = strSku_Index
        _dblQty_PICKING = dblQty_PICKING
        _DocumentType_Index = strDocumentType_Index

    End Sub


#Region "SEARCH PICKING"
    Function fnPICKING_KSL(ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand, ByVal pProcess_Id As Integer, ByVal pDocument_Index As String, ByVal AdditionalCondition As String) As DataTable
        Try
            Me.Process_Id = pProcess_Id
            Me.Document_Index = pDocument_Index

            If USE_WITHDRAW_PICKING_NEWLOT() = True Then
                _strCondition &= Set_WITHDRAW_NEWLOT()
            End If
            Select Case _enmPicking_Type
                Case enmPicking_Type.CUSTOM
                    Dim objPicking As New config_CustomSetting
                    Dim strAuto As String = objPicking.GetConfig_Picking("CUSTOM")
                    Dim strWhere As String = objPicking.GetOther_Where("CUSTOM", _strCondition)
                    '_strCondition &= strWhere
                    '_strCondition &= strAuto
                    '--- NO Define Pick Manual
                    Dim xCondition As String = ""
                    xCondition = _strCondition & strWhere & AdditionalCondition & strAuto
                    '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
                    Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_CUSTOM(xCondition, Connection, myTrans, SQLServerCommand)
                    For Each dr As DataRow In odtLocationBalance.Rows
                        dr("Qty_Recieve_Package") = Format(dr("Qty_Recieve_Package"), "###0.000000")
                    Next
                    Return odtLocationBalance

                Case enmPicking_Type.FIFO
                    Dim objPicking As New config_CustomSetting
                    Dim strAuto As String = objPicking.GetConfig_Picking("FIFO")
                    Dim strWhere As String = objPicking.GetOther_Where("FIFO", _strCondition)
                    If pProcess_Id = 4 Then
                        strWhere = ""
                    End If

                    '---- Add on
                    Dim xCondition As String = ""
                    xCondition = _strCondition & strWhere & AdditionalCondition & strAuto
                    '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
                    Dim odtLocationBalance As New DataTable
                    odtLocationBalance = SEARCHLOCATIONBALANCE_PICKING_AUTO(xCondition, Connection, myTrans, SQLServerCommand)
                    '--- PICKING AND RESERV
                    _dtPicking = New DataTable 'Reset picking
                    PICKING_RESERV_KSL(odtLocationBalance, Connection, myTrans, SQLServerCommand)
                    '--- RETURN TO WITHDRAW ITEM
                    Return _dtPicking

            End Select

            Return _dtPicking

        Catch ex As Exception
            Throw ex
        End Try


    End Function

    'Function fnPICKING(ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As DataTable
    '    '--- CHECK BLOCK LOT,SKU,ORDER
    '    Try

    '        If USE_WITHDRAW_PICKING_NEWLOT() = True Then
    '            _strCondition &= Set_WITHDRAW_NEWLOT()
    '        End If

    '        Select Case _enmPicking_Type
    '            Case enmPicking_Type.CUSTOM
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("CUSTOM")
    '                Dim strWhere As String = objPicking.GetOther_Where("CUSTOM", _strCondition)
    '                _strCondition &= strWhere
    '                _strCondition &= strAuto
    '                '--- NO Define Pick Manual

    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_CUSTOM(_strCondition, Connection, myTrans, SQLServerCommand)

    '                For Each dr As DataRow In odtLocationBalance.Rows
    '                    dr("Qty_Recieve_Package") = Format(dr("Qty_Recieve_Package"), "###0.000000")
    '                Next

    '                Return odtLocationBalance

    '            Case enmPicking_Type.FIFO
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("FIFO")
    '                Dim strWhere As String = objPicking.GetOther_Where("FIFO", _strCondition)
    '                _strCondition &= strWhere

    '                '--ADDNEW V3
    '                '>>a.เงื่อนไขคลังสินค้าหลัก และข้อกำหนด “จ่ายจากคลังหลักเท่านั้น”
    '                If _strOrderForV3 <> "" Then
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH}", _strOrderForV3)
    '                Else
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH},", _strOrderForV3)

    '                End If

    '                '--END V3

    '                _strCondition &= strAuto
    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition, Connection, myTrans, SQLServerCommand)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV_KSL(odtLocationBalance, Connection, myTrans, SQLServerCommand)
    '                '--- RETURN TO WITHDRAW ITEM
    '                Return _dtPicking
    '            Case enmPicking_Type.LIFO
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("LIFO")
    '                Dim strWhere As String = objPicking.GetOther_Where("LIFO", _strCondition)
    '                _strCondition &= strWhere

    '                '--ADDNEW V3
    '                '>>a.เงื่อนไขคลังสินค้าหลัก และข้อกำหนด “จ่ายจากคลังหลักเท่านั้น”
    '                If _strOrderForV3 <> "" Then
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH}", _strOrderForV3)
    '                Else
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH},", _strOrderForV3)

    '                End If
    '                '--END V3

    '                _strCondition &= strAuto
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition, Connection, myTrans, SQLServerCommand)
    '                PICKING_RESERV_KSL(odtLocationBalance, Connection, myTrans, SQLServerCommand)
    '                Return _dtPicking
    '            Case enmPicking_Type.FEFO
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("FEFO")
    '                Dim strWhere As String = objPicking.GetOther_Where("FEFO", _strCondition)
    '                _strCondition &= strWhere

    '                '--ADDNEW V3
    '                '>>a.เงื่อนไขคลังสินค้าหลัก และข้อกำหนด “จ่ายจากคลังหลักเท่านั้น”
    '                If _strOrderForV3 <> "" Then
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH}", _strOrderForV3)
    '                Else
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH},", _strOrderForV3)
    '                End If

    '                _strCondition &= strAuto
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition, Connection, myTrans, SQLServerCommand)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV_KSL(odtLocationBalance, Connection, myTrans, SQLServerCommand)
    '                Return _dtPicking
    '            Case enmPicking_Type.FEFO1
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("FEFO1")
    '                Dim strWhere As String = objPicking.GetOther_Where("FEFO1", _strCondition)
    '                _strCondition &= strWhere

    '                '--ADDNEW V3
    '                '>>a.เงื่อนไขคลังสินค้าหลัก และข้อกำหนด “จ่ายจากคลังหลักเท่านั้น”
    '                If _strOrderForV3 <> "" Then
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH}", _strOrderForV3)
    '                Else
    '                    strAuto = strAuto.Replace("{ISPRIMARYWH},", _strOrderForV3)
    '                End If

    '                _strCondition &= strAuto
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition, Connection, myTrans, SQLServerCommand)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV_KSL(odtLocationBalance, Connection, myTrans, SQLServerCommand)
    '                Return _dtPicking
    '            Case enmPicking_Type.SERIAL
    '                'Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                'PICKING_RESERV(odtLocationBalance)
    '                'Return _dtPicking

    '            Case enmPicking_Type.PICKFACE
    '                'Dim objPicking As New config_CustomSetting
    '                'Dim strAuto As String = objPicking.GetConfig_Picking("Pickface")
    '                'Dim strWhere As String = objPicking.GetOther_Where("Pickface", _strCondition)
    '                '_strCondition &= strWhere
    '                'If _strOrderForV3 <> "" Then
    '                '    strAuto = strAuto.Replace("{ISPRIMARYWH}", _strOrderForV3)
    '                'Else
    '                '    strAuto = strAuto.Replace("{ISPRIMARYWH},", _strOrderForV3)
    '                'End If
    '                '_strCondition &= strAuto
    '                'Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                'PICKING_RESERV_Pickface(odtLocationBalance, _strCondition)
    '                'Return _dtPicking

    '        End Select

    '        Return _dtPicking

    '    Catch ex As Exception
    '        Throw ex
    '    End Try


    'End Function

    'Function fnPICKING() As DataTable
    '    '--- CHECK BLOCK LOT,SKU,ORDER
    '    Try

    '        If USE_WITHDRAW_PICKING_NEWLOT() = True Then
    '            _strCondition &= Set_WITHDRAW_NEWLOT()
    '        End If

    '        Select Case _enmPicking_Type
    '            Case enmPicking_Type.CUSTOM
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("CUSTOM")
    '                Dim strWhere As String = objPicking.GetOther_Where("CUSTOM", _strCondition)
    '                _strCondition &= strWhere
    '                _strCondition &= strAuto.Replace("{ISPRIMARYWH},", "")

    '                '--- NO Define Pick Manual

    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_CUSTOM(_strCondition)

    '                For Each dr As DataRow In odtLocationBalance.Rows
    '                    dr("Qty_Recieve_Package") = Format(dr("Qty_Recieve_Package"), "###0.0000")
    '                Next
    '                Return odtLocationBalance

    '            Case enmPicking_Type.FIFO
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("FIFO")
    '                Dim strWhere As String = objPicking.GetOther_Where("FIFO", _strCondition)
    '                _strCondition &= strWhere
    '                _strCondition &= strAuto.Replace("{ISPRIMARYWH},", "")

    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV(odtLocationBalance)
    '                '--- RETURN TO WITHDRAW ITEM
    '                Return _dtPicking
    '            Case enmPicking_Type.LIFO
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("LIFO")
    '                Dim strWhere As String = objPicking.GetOther_Where("LIFO", _strCondition)
    '                _strCondition &= strWhere
    '                _strCondition &= strAuto.Replace("{ISPRIMARYWH},", "")


    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV(odtLocationBalance)
    '                '--- RETURN TO WITHDRAW ITEM
    '                Return _dtPicking
    '            Case enmPicking_Type.FEFO
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("FEFO")
    '                Dim strWhere As String = objPicking.GetOther_Where("FEFO", _strCondition)
    '                _strCondition &= strWhere
    '                _strCondition &= strAuto.Replace("{ISPRIMARYWH},", "")


    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV(odtLocationBalance)
    '                '--- RETURN TO WITHDRAW ITEM
    '                Return _dtPicking
    '            Case enmPicking_Type.FEFO1
    '                Dim objPicking As New config_CustomSetting
    '                Dim strAuto As String = objPicking.GetConfig_Picking("FEFO1")
    '                Dim strWhere As String = objPicking.GetOther_Where("FEFO1", _strCondition)
    '                _strCondition &= strWhere
    '                _strCondition &= strAuto.Replace("{ISPRIMARYWH},", "")


    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV(odtLocationBalance)
    '                '--- RETURN TO WITHDRAW ITEM
    '                Return _dtPicking
    '            Case enmPicking_Type.SERIAL
    '                '--- NO Define Pick Manual Pass dblQty_PICKING = 1
    '                '--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                '--- PICKING AND RESERV
    '                PICKING_RESERV(odtLocationBalance)
    '                '--- RETURN TO WITHDRAW ITEM
    '                Return _dtPicking

    '            Case enmPicking_Type.PICKFACE
    '                'Dim objPicking As New config_CustomSetting
    '                'Dim strAuto As String = objPicking.GetConfig_Picking("Pickface")
    '                'Dim strWhere As String = objPicking.GetOther_Where("Pickface", _strCondition)
    '                '_strCondition &= strWhere
    '                '_strCondition &= strAuto.Replace("{ISPRIMARYWH},", "")


    '                ''--- SEARCH DATA FROM VIEW_WithDrawReservLocation
    '                ''Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strConditio)
    '                'Dim odtLocationBalance As DataTable = SEARCHLOCATIONBALANCE_PICKING_AUTO(_strCondition)
    '                ''--- PICKING AND RESERV
    '                'PICKING_RESERV_Pickface(odtLocationBalance, _strCondition)
    '                ''--- RETURN TO WITHDRAW ITEM
    '                'Return _dtPicking
    '        End Select

    '        If CHK_RESERV_FAIL() = True Then
    '            'TODO : Retrun Fail.
    '            'W_MSG_Information_ByIndex(" Please Contact Support [Qty Bal < 0 ] ")
    '            Return Nothing
    '        End If
    '        Return _dtPicking

    '    Catch ex As Exception
    '        Throw ex
    '    End Try


    'End Function


#End Region

#Region "PICKING"

    'Sub PICKING_RESERV(ByVal odtLocationBalance As DataTable)
    '    Dim _dblTemp_Total_Qty_PICKING As Decimal = 0.0
    '    Dim _dblSkuRatio As Decimal = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
    '    Dim ichk As Integer = 0
    '    For Each odrLocation As DataRow In odtLocationBalance.Rows
    '        'If ((ichk = 0) And (_dblSkuRatio > 1)) And (_dblSkuRatio <= CDec(odrLocation("Ratio"))) Then
    '        '    _dblTemp_Total_Qty_PICKING = _dblQty_PICKING * CDec(odrLocation("Ratio"))
    '        'ElseIf ((ichk = 0) And (_dblSkuRatio > 1)) And (_dblSkuRatio > CDec(odrLocation("Ratio"))) Then
    '        '    _dblTemp_Total_Qty_PICKING = _dblQty_PICKING * _dblSkuRatio
    '        'ElseIf (ichk = 0) Then
    '        '    _dblTemp_Total_Qty_PICKING = _dblQty_PICKING
    '        'End If

    '        'BY NEUNG
    '        If ((ichk = 0) And (_dblSkuRatio > 1)) And (_dblSkuRatio <= CDec(odrLocation("Ratio"))) Then
    '            _dblTemp_Total_Qty_PICKING = Math.Round(_dblQty_PICKING * _dblSkuRatio)
    '        ElseIf ((ichk = 0) And (_dblSkuRatio < 1)) And (_dblSkuRatio < CDec(odrLocation("Ratio"))) Then
    '            _dblTemp_Total_Qty_PICKING = Math.Round(_dblQty_PICKING * _dblSkuRatio)
    '        ElseIf ((ichk = 0) And (_dblSkuRatio > 1)) And (_dblSkuRatio > CDec(odrLocation("Ratio"))) Then
    '            _dblTemp_Total_Qty_PICKING = Math.Round(_dblQty_PICKING * _dblSkuRatio)
    '        ElseIf (ichk = 0) Then
    '            _dblTemp_Total_Qty_PICKING = _dblQty_PICKING
    '        End If



    '        _dblQty_BALANCE = CDec(odrLocation("Qty_Bal"))
    '        'Decrease dblQty_Bal
    '        If _dblQty_BALANCE >= (_dblTemp_Total_Qty_PICKING) Then
    '            _dblQty_BALANCE = (_dblTemp_Total_Qty_PICKING)
    '        End If
    '        '******** RESERV LOCATION BALANCE ********
    '        RESERV_LOCATION(odrLocation)
    '        'Decrease QTY Picking 
    '        ' _dblTemp_Total_Qty_PICKING -= _dblQty_BALANCE

    '        ' Ta Update      08/09/2011
    '        _dblTemp_Total_Qty_PICKING = Format(_dblTemp_Total_Qty_PICKING, "###0.0000") - Format(_dblQty_BALANCE, "###0.0000")
    '        _dblTemp_Total_Qty_PICKING = Format(_dblTemp_Total_Qty_PICKING, "###0.0000")

    '        ichk += 1
    '        If _dblTemp_Total_Qty_PICKING <= 0 Then Exit For

    '        ' CDec(odrLocation("Ratio"))
    '    Next
    'End Sub

    Sub PICKING_RESERV_KSL(ByVal odtLocationBalance As DataTable, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand)
        Dim _dblTemp_Total_Qty_PICKING As Decimal = 0.0
        Dim _dblSkuRatio As Decimal = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
        Dim ichk As Integer = 0
        For Each odrLocation As DataRow In odtLocationBalance.Rows


            'BY NEUNG
            If ((ichk = 0) And (_dblSkuRatio > 1)) And (_dblSkuRatio <= CDec(odrLocation("Ratio"))) Then
                _dblTemp_Total_Qty_PICKING = Math.Round(_dblQty_PICKING * _dblSkuRatio)
            ElseIf ((ichk = 0) And (_dblSkuRatio < 1)) And (_dblSkuRatio < CDec(odrLocation("Ratio"))) Then
                _dblTemp_Total_Qty_PICKING = Math.Round(_dblQty_PICKING * _dblSkuRatio)
            ElseIf ((ichk = 0) And (_dblSkuRatio > 1)) And (_dblSkuRatio > CDec(odrLocation("Ratio"))) Then
                _dblTemp_Total_Qty_PICKING = Math.Round(_dblQty_PICKING * _dblSkuRatio)
            ElseIf (ichk = 0) Then
                _dblTemp_Total_Qty_PICKING = _dblQty_PICKING
            End If



            _dblQty_BALANCE = CDec(odrLocation("Qty_Bal"))
            'Decrease dblQty_Bal
            If _dblQty_BALANCE >= (_dblTemp_Total_Qty_PICKING) Then
                _dblQty_BALANCE = (_dblTemp_Total_Qty_PICKING)
            End If
            '******** RESERV LOCATION BALANCE ********
            RESERV_LOCATION_KSL(odrLocation, Connection, myTrans, SQLServerCommand)
            'Decrease QTY Picking 
            ' _dblTemp_Total_Qty_PICKING -= _dblQty_BALANCE

            ' Ta Update      08/09/2011
            _dblTemp_Total_Qty_PICKING = Format(_dblTemp_Total_Qty_PICKING, "###0.000000") - Format(_dblQty_BALANCE, "###0.000000")
            _dblTemp_Total_Qty_PICKING = Format(_dblTemp_Total_Qty_PICKING, "###0.000000")

            ichk += 1

            'If Not useAutoAdjust Then
            If _dblTemp_Total_Qty_PICKING <= 0 Then Exit For
            'End If


            ' CDec(odrLocation("Ratio"))
        Next
    End Sub

    Function RESERV_LOCATION_KSL(ByVal odrLocation As DataRow, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As Integer
        Try
            Dim ItemQtyPerPck As Decimal = 0
            Dim PriceperPck As Decimal = 0
            Dim weightperPck As Decimal = 0
            Dim volumeperPck As Decimal = 0


            Dim strProcessText As String = "การจองโดย System "
            Select Case _enmPicking_Type
                Case enmPicking_Type.CUSTOM
                    strProcessText &= " CUSTOM"
                Case enmPicking_Type.FIFO
                    strProcessText &= " FIFO"
                Case enmPicking_Type.FEFO
                    strProcessText &= " FEFO"
                Case enmPicking_Type.FEFO1
                    strProcessText &= " FEFO1"
                Case enmPicking_Type.LIFO
                    strProcessText &= " LIFO"
                Case enmPicking_Type.PICKFACE
                    strProcessText &= " PICKFACE"
                Case enmPicking_Type.SERIAL
                    strProcessText &= " SERIAL"
            End Select
            Dim LocationBal_Index As String = odrLocation("locationbalance_index").ToString
            Dim Qty_Reserv As Decimal = 0
            Dim Weight_Reserv As Decimal = 0
            Dim Volume_Reserv As Decimal = 0
            Dim ItemQty_Reserv As Decimal = 0
            Dim Price_Reserv As Decimal = 0
            'If useAutoAdjust = False Then

            PriceperPck = CDec(odrLocation("OrderItem_Price_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
            weightperPck = CDec(odrLocation("Weight_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
            volumeperPck = CDec(odrLocation("Volume_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
            ItemQtyPerPck = CDec(odrLocation("Qty_Item_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
            Qty_Reserv = FormatNumber(_dblQty_BALANCE, 6) 'CDec(odrLocation("Total_Qty").ToString)
            Weight_Reserv = FormatNumber(weightperPck * _dblQty_BALANCE, 6) ' CDec(odrLocation("WeightOut").ToString) --volumeperPck
            Volume_Reserv = FormatNumber(volumeperPck * _dblQty_BALANCE, 6) 'CDec(odrLocation("VolumeOut").ToString) --weightperPck
            ItemQty_Reserv = FormatNumber(ItemQtyPerPck * _dblQty_BALANCE, 6) ' CDec(odrLocation("QtyItemOut").ToString)
            Price_Reserv = FormatNumber(PriceperPck * _dblQty_BALANCE, 6) 'CDec(odrLocation("Price_Out").ToString)
            Dim objPicking As New PICKING(enmPicking_Type.CUSTOM)
            objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(Connection, myTrans, enmPicking_Action.ADDRESERVE, Me.Process_Id, Me.Document_Index, strProcessText, LocationBal_Index, 0, 0, 0, 0, 0, 0, _
            Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)
            odrLocation("Qty") = _dblQty_BALANCE
            Dim _dblSkuRatio As Decimal = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            If ((_dblSkuRatio > 1)) And (_dblSkuRatio <= CDec(odrLocation("Ratio"))) Then
                odrLocation("Qty") = FormatNumber(_dblQty_BALANCE / CDec(odrLocation("Ratio").ToString), 6)
            Else
                odrLocation("Qty") = Math.Round(_dblQty_BALANCE / _dblSkuRatio, 6)
            End If
            Dim objDB_Package As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
            objDB_Package.SelectData_ByIndex(_strPackage_Index)
            odrLocation("Description") = objDB_Package.DataTable.Rows(0).Item("Description").ToString()
            odrLocation("Package_Index") = objDB_Package.DataTable.Rows(0).Item("Package_Index").ToString()
            odrLocation("Invoice_Out") = ""
            odrLocation("Total_Qty") = FormatNumber(_dblQty_BALANCE, 6) ' CDec(Format(_dblQty_BALANCE, "#,##0.0000")) '* GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            odrLocation("VolumeOut") = FormatNumber(volumeperPck * _dblQty_BALANCE, 6) 'CDec(Format(volumeperPck * _dblQty_BALANCE, "#,##0.0000"))
            odrLocation("WeightOut") = FormatNumber(weightperPck * _dblQty_BALANCE, 6) 'CDec(Format(weightperPck * _dblQty_BALANCE, "#,##0.0000"))
            odrLocation("QtyItemOut") = FormatNumber(ItemQtyPerPck * _dblQty_BALANCE, 6) 'CDec(Format(ItemQtyPerPck * _dblQty_BALANCE, "#,##0.0000"))
            odrLocation("Price_Out") = FormatNumber(PriceperPck * _dblQty_BALANCE, 6) 'CDec(Format(PriceperPck * _dblQty_BALANCE, "#,##0.0000"))
            odrLocation("NewItem") = 1
            If _dtPicking Is Nothing Then
                _dtPicking = SEARCHLOCATIONBALANCE_ALLBALANCE("  AND Locationbalance_index = '" & LocationBal_Index & "'", Connection, myTrans, SQLServerCommand)
                _dtPicking.Rows(0).Item("Qty") = FormatNumber(CDec(odrLocation("QTY")), 6)
                _dtPicking.Rows(0).Item("Total_Qty") = FormatNumber(Qty_Reserv, 6)
                _dtPicking.Rows(0).Item("WeightOut") = FormatNumber(Weight_Reserv, 6)
                _dtPicking.Rows(0).Item("VolumeOut") = FormatNumber(Volume_Reserv, 6)
                _dtPicking.Rows(0).Item("QtyItemOut") = FormatNumber(ItemQty_Reserv, 6)
                _dtPicking.Rows(0).Item("Price_Out") = FormatNumber(Price_Reserv, 6)
                _dtPicking.Rows(0).Item("Description") = odrLocation("Description")
                _dtPicking.Rows(0).Item("Package_Index") = odrLocation("Package_Index")
            Else
                Dim tmpdtPicking As New DataTable
                tmpdtPicking = SEARCHLOCATIONBALANCE_ALLBALANCE("  AND Locationbalance_index = '" & LocationBal_Index & "'", Connection, myTrans, SQLServerCommand)
                tmpdtPicking.Rows(0).Item("Qty") = FormatNumber(CDec(odrLocation("QTY")), 6)
                tmpdtPicking.Rows(0).Item("Total_Qty") = FormatNumber(Qty_Reserv, 6)
                tmpdtPicking.Rows(0).Item("WeightOut") = FormatNumber(Weight_Reserv, 6)
                tmpdtPicking.Rows(0).Item("VolumeOut") = FormatNumber(Volume_Reserv, 6)
                tmpdtPicking.Rows(0).Item("QtyItemOut") = FormatNumber(ItemQty_Reserv, 6)
                tmpdtPicking.Rows(0).Item("Price_Out") = FormatNumber(Price_Reserv, 6)
                tmpdtPicking.Rows(0).Item("Description") = odrLocation("Description")
                tmpdtPicking.Rows(0).Item("Package_Index") = odrLocation("Package_Index")
                _dtPicking.Merge(tmpdtPicking)
            End If
            'Else
            '    'Adsign value
            '    '----------------------------------------------------------------------------
            '    Dim tmpdtPicking As New DataTable
            '    tmpdtPicking = SEARCHLOCATIONBALANCE_ALLBALANCE("  AND Locationbalance_index = '" & LocationBal_Index & "'", Connection, myTrans, SQLServerCommand)
            '    PriceperPck = 1
            '    weightperPck = 1
            '    volumeperPck = 1
            '    ItemQtyPerPck = 1
            '    If odrLocation("Qty_Bal_Begin") > 0 Then
            '        PriceperPck = CDec(odrLocation("OrderItem_Price_Begin").ToString) / CDec(odrLocation("Qty_Bal_Begin").ToString)
            '        weightperPck = CDec(odrLocation("Weight_Bal_Begin").ToString) / CDec(odrLocation("Qty_Bal_Begin").ToString)
            '        volumeperPck = CDec(odrLocation("Volume_Bal_Begin").ToString) / CDec(odrLocation("Qty_Bal_Begin").ToString)
            '        ItemQtyPerPck = CDec(odrLocation("Qty_Item_Begin").ToString) / CDec(odrLocation("Qty_Bal_Begin").ToString)
            '    End If

            '    Dim _dblSkuRatio As Decimal = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            '    If ((_dblSkuRatio > 1)) And (_dblSkuRatio <= CDec(odrLocation("Ratio"))) Then
            '        odrLocation("Qty") = FormatNumber(_dblQty_PICKING / CDec(odrLocation("Ratio").ToString), 6)
            '    Else
            '        odrLocation("Qty") = Math.Round(_dblQty_PICKING / _dblSkuRatio, 6)
            '    End If
            '    Qty_Reserv = FormatNumber(_dblQty_PICKING, 6)
            '    Weight_Reserv = FormatNumber(weightperPck * _dblQty_PICKING, 6)
            '    Volume_Reserv = FormatNumber(volumeperPck * _dblQty_PICKING, 6)
            '    ItemQty_Reserv = FormatNumber(ItemQtyPerPck * _dblQty_PICKING, 6)
            '    Price_Reserv = FormatNumber(PriceperPck * _dblQty_PICKING, 6)

            '    tmpdtPicking.Rows(0).Item("Qty") = FormatNumber(CDec(odrLocation("Qty")), 6)
            '    tmpdtPicking.Rows(0).Item("Total_Qty") = FormatNumber(Qty_Reserv, 6)
            '    tmpdtPicking.Rows(0).Item("WeightOut") = FormatNumber(Weight_Reserv, 6)
            '    tmpdtPicking.Rows(0).Item("VolumeOut") = FormatNumber(Volume_Reserv, 6)
            '    tmpdtPicking.Rows(0).Item("QtyItemOut") = FormatNumber(ItemQty_Reserv, 6)
            '    tmpdtPicking.Rows(0).Item("Price_Out") = FormatNumber(Price_Reserv, 6)
            '    tmpdtPicking.Rows(0).Item("Description") = odrLocation("Description")
            '    tmpdtPicking.Rows(0).Item("Package_Index") = odrLocation("Package_Index")

            '    'Add New for auto adjust
            '    If Not tmpdtPicking.Columns.Contains("From_Location_Index") Then tmpdtPicking.Columns.Add("From_Location_Index", GetType(String))
            '    If Not tmpdtPicking.Columns.Contains("To_Location_Index") Then tmpdtPicking.Columns.Add("To_Location_Index", GetType(String))
            '    If Not tmpdtPicking.Columns.Contains("Old_ItemStatus_Index") Then tmpdtPicking.Columns.Add("Old_ItemStatus_Index", GetType(String))
            '    If Not tmpdtPicking.Columns.Contains("New_ItemStatus_Index") Then tmpdtPicking.Columns.Add("New_ItemStatus_Index", GetType(String))

            '    tmpdtPicking.Rows(0).Item("From_Location_Index") = "0010000000000" 'KSL : Fix Frozen
            '    tmpdtPicking.Rows(0).Item("Old_ItemStatus_Index") = "0090000000000" 'KSL : Fix Frozen
            '    'tmpdtPicking.Rows(0).Item("New_ItemStatus_Index") = odrLocation("ItemStatus_Index").ToString

            '    '----------------------------------------------------------------------------
            '    'Delete balance
            '    Dim xSQL As String = ""
            '    'If odrLocation("Qty_Bal") > 0 Then
            '    '    xSQL = "  UPDATE tb_LocationBalance    "
            '    '    xSQL &= " SET Qty_Recieve_Package = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,Qty_Bal = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,Weight_Bal = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,Volume_Bal = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,Qty_Item_Bal = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,OrderItem_Price_Bal = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,ReserveQty  = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,ReserveWeight = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,ReserveVolume = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,ReserveQty_Item  = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,ReserveOrderItem_Price  = " & FormatNumber(0, 6)
            '    '    xSQL &= "   ,Location_Index = '0010000000000'" 'KSL : Fix Frozen
            '    '    xSQL &= "   ,ItemStatus_Index = '0010000000000'" 'KSL : Fix Frozen
            '    '    xSQL &= "   ,TagPack_No = 'Auto adjust reset zero'" 'KSL : Fix Frozen
            '    '    xSQL &= " where LocationBalance_Index='" & LocationBal_Index & "'"
            '    '    DBExeNonQuery(xSQL, Connection, myTrans)
            '    'End If

            '    'If _dtPicking.Rows.Count = 0 Then


            '    'จอง
            '    '----------------------------------------------------------------------------
            '    'Reserve all. move all to location adjust
            '    xSQL = "  UPDATE tb_LocationBalance    "
            '    xSQL &= " SET "
            '    xSQL &= "   Qty_Recieve_Package= " & FormatNumber(tmpdtPicking.Rows(0).Item("Qty"), 6)
            '    xSQL &= "   ,Qty_Bal =  " & FormatNumber(tmpdtPicking.Rows(0).Item("Total_Qty"), 6)
            '    xSQL &= "   ,Weight_Bal= " & FormatNumber(tmpdtPicking.Rows(0).Item("WeightOut"), 6)
            '    xSQL &= "   ,Volume_Bal= " & FormatNumber(tmpdtPicking.Rows(0).Item("VolumeOut"), 6)
            '    xSQL &= "   ,Qty_Item_Bal = " & FormatNumber(tmpdtPicking.Rows(0).Item("QtyItemOut"), 6)
            '    xSQL &= "   ,OrderItem_Price_Bal= " & FormatNumber(tmpdtPicking.Rows(0).Item("Price_Out"), 6)
            '    xSQL &= "   ,ReserveQty = " & FormatNumber(tmpdtPicking.Rows(0).Item("Total_Qty"), 6)
            '    xSQL &= "   ,ReserveWeight = " & FormatNumber(tmpdtPicking.Rows(0).Item("WeightOut"), 6)
            '    xSQL &= "   ,ReserveVolume = " & FormatNumber(tmpdtPicking.Rows(0).Item("VolumeOut"), 6)
            '    xSQL &= "   ,ReserveQty_Item = " & FormatNumber(tmpdtPicking.Rows(0).Item("QtyItemOut"), 6)
            '    xSQL &= "   ,ReserveOrderItem_Price = " & FormatNumber(tmpdtPicking.Rows(0).Item("Price_Out"), 6)
            '    xSQL &= "   ,Location_Index = '0010000000000'" 'KSL : Fix Frozen
            '    xSQL &= "   ,ItemStatus_Index = '0090000000000'" 'KSL : Fix Frozen
            '    xSQL &= " where LocationBalance_Index='" & LocationBal_Index & "'"
            '    DBExeNonQuery(xSQL, Connection, myTrans)
            '    'End If
            '    If FormatNumber(tmpdtPicking.Rows(0).Item("Qty"), 6) > 0 Then
            '        xSQL = "  UPDATE tb_LocationBalance    "
            '        xSQL &= " SET Qty_Bal_Begin =  " & FormatNumber(tmpdtPicking.Rows(0).Item("Total_Qty"), 6)
            '        xSQL &= "   ,Weight_Bal_Begin = " & FormatNumber(tmpdtPicking.Rows(0).Item("WeightOut"), 6)
            '        xSQL &= "   ,Volume_Bal_Begin= " & FormatNumber(tmpdtPicking.Rows(0).Item("VolumeOut"), 6)
            '        xSQL &= "   ,Qty_Item_Begin = " & FormatNumber(tmpdtPicking.Rows(0).Item("QtyItemOut"), 6)
            '        xSQL &= "   ,OrderItem_Price_Begin = " & FormatNumber(tmpdtPicking.Rows(0).Item("Price_Out"), 6)
            '        xSQL &= " where LocationBalance_Index='" & LocationBal_Index & "'"
            '        DBExeNonQuery(xSQL, Connection, myTrans)

            '        If _dtPicking.Rows.Count > 0 Then
            '            Dim drArr() As DataRow = _dtPicking.Select("TAG_No = '" & odrLocation("TAG_No").ToString & "' and Sku_Index = '" & odrLocation("Sku_Index").ToString & "'")
            '            If drArr.Length > 0 Then
            '                For Each drDel As DataRow In drArr
            '                    xSQL = "  UPDATE tb_LocationBalance    "
            '                    xSQL &= " SET "
            '                    xSQL &= "   Qty_Recieve_Package = 0"
            '                    xSQL &= "   ,Qty_Bal  = 0"
            '                    xSQL &= "   ,Weight_Bal  = 0"
            '                    xSQL &= "   ,Volume_Bal =  0"
            '                    xSQL &= "   ,Qty_Item_Bal =  0"
            '                    xSQL &= "   ,OrderItem_Price_Bal =  0"
            '                    xSQL &= "   ,ReserveQty =  0"
            '                    xSQL &= "   ,ReserveWeight =  0"
            '                    xSQL &= "   ,ReserveVolume =  0"
            '                    xSQL &= "   ,ReserveQty_Item =  0"
            '                    xSQL &= "   ,ReserveOrderItem_Price =  0"
            '                    xSQL &= " where LocationBalance_Index='" & drDel("LocationBalance_Index").ToString & "'"
            '                    DBExeNonQuery(xSQL, Connection, myTrans)
            '                    _dtPicking.Rows.Remove(drDel)
            '                Next
            '            End If
            '        End If

            '        _dtPicking.Merge(tmpdtPicking)

            '    End If


            'End If


        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Function RESERV_LOCATION(ByVal odrLocation As DataRow) As Integer
    '    Try
    '        Dim ItemQtyPerPck As Decimal = 0
    '        Dim PriceperPck As Decimal = 0
    '        Dim weightperPck As Decimal = 0
    '        Dim volumeperPck As Decimal = 0


    '        Dim strProcessText As String = "การจองโดย "
    '        Select Case _enmPicking_Type
    '            Case enmPicking_Type.CUSTOM
    '                strProcessText &= " CUSTOM"
    '            Case enmPicking_Type.FIFO
    '                strProcessText &= " FIFO"
    '            Case enmPicking_Type.FEFO
    '                strProcessText &= " FEFO"
    '            Case enmPicking_Type.FEFO1
    '                strProcessText &= " FEFO1"
    '            Case enmPicking_Type.LIFO
    '                strProcessText &= " LIFO"
    '            Case enmPicking_Type.PICKFACE
    '                strProcessText &= " PICKFACE"
    '            Case enmPicking_Type.SERIAL
    '                strProcessText &= " SERIAL"
    '        End Select

    '        'odrLocation("Total_Qty") = FormatNumber(_dblQty_BALANCE, 4) ' CDec(Format(_dblQty_BALANCE, "#,##0.0000")) '* GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
    '        'odrLocation("VolumeOut") = FormatNumber(volumeperPck * _dblQty_BALANCE, 4) 'CDec(Format(volumeperPck * _dblQty_BALANCE, "#,##0.0000"))
    '        'odrLocation("WeightOut") = FormatNumber(weightperPck * _dblQty_BALANCE, 4) 'CDec(Format(weightperPck * _dblQty_BALANCE, "#,##0.0000"))
    '        'odrLocation("QtyItemOut") = FormatNumber(ItemQtyPerPck * _dblQty_BALANCE, 4) 'CDec(Format(ItemQtyPerPck * _dblQty_BALANCE, "#,##0.0000"))
    '        'odrLocation("Price_Out") = FormatNumber(PriceperPck * _dblQty_BALANCE, 4) 'CDec(Format(PriceperPck * _dblQty_BALANCE, "#,##0.0000"))

    '        PriceperPck = CDec(odrLocation("OrderItem_Price_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
    '        weightperPck = CDec(odrLocation("Weight_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
    '        volumeperPck = CDec(odrLocation("Volume_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)
    '        ItemQtyPerPck = CDec(odrLocation("Qty_Item_Bal").ToString) / CDec(odrLocation("Qty_Bal").ToString)

    '        Dim LocationBal_Index As String = odrLocation("locationbalance_index").ToString
    '        Dim Qty_Reserv As Decimal = FormatNumber(_dblQty_BALANCE, 4) 'CDec(odrLocation("Total_Qty").ToString)
    '        Dim Weight_Reserv As Decimal = FormatNumber(weightperPck * _dblQty_BALANCE, 4) ' CDec(odrLocation("WeightOut").ToString) --volumeperPck
    '        Dim Volume_Reserv As Decimal = FormatNumber(volumeperPck * _dblQty_BALANCE, 4) 'CDec(odrLocation("VolumeOut").ToString) --weightperPck
    '        Dim ItemQty_Reserv As Decimal = FormatNumber(ItemQtyPerPck * _dblQty_BALANCE, 4) ' CDec(odrLocation("QtyItemOut").ToString)
    '        Dim Price_Reserv As Decimal = FormatNumber(PriceperPck * _dblQty_BALANCE, 4) 'CDec(odrLocation("Price_Out").ToString)


    '        'Select Case _enmPicking_Type
    '        '    Case enmPicking_Type.PICKFACE
    '        '    Case Else
    '        Dim objPicking As New PICKING(enmPicking_Type.CUSTOM)
    '        objPicking.UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL(Nothing, Nothing, enmPicking_Action.ADDRESERVE, 2, Me._Document_Index, strProcessText, LocationBal_Index, 0, 0, 0, 0, 0, 0, _
    '        Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)
    '        'End Select







    '        '   dblItemQty_sku = CDec(odrLocation("Qty_Item_Bal").ToString)


    '        odrLocation("Qty") = _dblQty_BALANCE
    '        Dim _dblSkuRatio As Decimal = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)



    '        If ((_dblSkuRatio > 1)) And (_dblSkuRatio <= CDec(odrLocation("Ratio"))) Then
    '            odrLocation("Qty") = FormatNumber(_dblQty_BALANCE / CDec(odrLocation("Ratio").ToString), 4) 'CDec(Format(_dblQty_BALANCE / CDec(odrLocation("Ratio").ToString), "#,##0.0000"))
    '        Else
    '            odrLocation("Qty") = FormatNumber(_dblQty_BALANCE, 4) ' CDec(Format(_dblQty_BALANCE, "#,##0.0000"))
    '        End If


    '        Dim objDB_Package As New ms_Package(ms_Package.enuOperation_Type.SEARCH)
    '        objDB_Package.SelectData_ByIndex(_strPackage_Index)

    '        odrLocation("Description") = objDB_Package.DataTable.Rows(0).Item("Description").ToString()
    '        odrLocation("Package_Index") = objDB_Package.DataTable.Rows(0).Item("Package_Index").ToString()

    '        odrLocation("Invoice_Out") = ""
    '        odrLocation("Total_Qty") = FormatNumber(_dblQty_BALANCE, 4) ' CDec(Format(_dblQty_BALANCE, "#,##0.0000")) '* GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
    '        odrLocation("VolumeOut") = FormatNumber(volumeperPck * _dblQty_BALANCE, 4) 'CDec(Format(volumeperPck * _dblQty_BALANCE, "#,##0.0000"))
    '        odrLocation("WeightOut") = FormatNumber(weightperPck * _dblQty_BALANCE, 4) 'CDec(Format(weightperPck * _dblQty_BALANCE, "#,##0.0000"))
    '        odrLocation("QtyItemOut") = FormatNumber(ItemQtyPerPck * _dblQty_BALANCE, 4) 'CDec(Format(ItemQtyPerPck * _dblQty_BALANCE, "#,##0.0000"))
    '        odrLocation("Price_Out") = FormatNumber(PriceperPck * _dblQty_BALANCE, 4) 'CDec(Format(PriceperPck * _dblQty_BALANCE, "#,##0.0000"))
    '        odrLocation("NewItem") = 1





    '        '--- ADD odrNewRow FOR RETURN TO grdWithdrawItemLocation
    '        'Dim odrNewRow As DataRow
    '        If _dtPicking Is Nothing Then
    '            _dtPicking = SEARCHLOCATIONBALANCE_ALLBALANCE("  AND Locationbalance_index = '" & LocationBal_Index & "'")
    '            _dtPicking.Rows(0).Item("Qty") = FormatNumber(CDec(odrLocation("Qty")), 4) '_dblQty_BALANCE
    '            _dtPicking.Rows(0).Item("Total_Qty") = FormatNumber(Qty_Reserv, 4) ' CDec(Format(Qty_Reserv, "#,##0.0000"))
    '            _dtPicking.Rows(0).Item("WeightOut") = FormatNumber(Weight_Reserv, 4) 'CDec(Format(Weight_Reserv, "#,##0.0000"))
    '            _dtPicking.Rows(0).Item("VolumeOut") = FormatNumber(Volume_Reserv, 4) 'CDec(Format(Volume_Reserv, "#,##0.0000"))
    '            _dtPicking.Rows(0).Item("QtyItemOut") = FormatNumber(ItemQty_Reserv, 4) ' CDec(Format(ItemQty_Reserv, "#,##0.0000"))
    '            _dtPicking.Rows(0).Item("Price_Out") = FormatNumber(Price_Reserv, 4) 'CDec(Format(Price_Reserv, "#,##0.0000"))
    '            _dtPicking.Rows(0).Item("Description") = odrLocation("Description")
    '            _dtPicking.Rows(0).Item("Package_Index") = odrLocation("Package_Index")
    '        Else
    '            'odrNewRow = odrLocation.ItemArray.Clone
    '            _dtPicking.Rows.Add(odrLocation.ItemArray)
    '        End If
    '        '--- New RESERV No Insert tb_WithDrawItemLocation
    '        'UPDATE_RESERVLOCATIONBALANCE(LocationBal_Index, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)






    '        Select Case _enmPicking_Type
    '            Case enmPicking_Type.SERIAL
    '                UPDATE_RESERV_ASSETLOCATIONBALANCE(_AssetLocationBalance_Index, Qty_Reserv, Weight_Reserv, Volume_Reserv, ItemQty_Reserv, Price_Reserv)
    '        End Select

    '    Catch ex As Exception
    '        Throw ex
    '    End Try

    'End Function

    Function CHK_RESERV_FAIL() As Boolean
        Try
            Dim strSQL As String = ""
            strSQL &= "select count(*) From tb_LocationBalance where ReserveQty < 0 "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()


            If Val(GetScalarOutput) = 0 Then
                Return False
            Else
                Return True
            End If


        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '''' <summary>
    '''' upeate 02-07-2010 (krit ทำให้ sharp)
    '''' -------------------------------------
    '''' Update Date : 08-07-2010
    '''' Update By   : Dong
    '''' -------------------------------------
    '''' </summary>
    '''' <param name="podtLocationBalance"></param>
    '''' <remarks></remarks>
    'Sub PICKING_RESERV_Pickface(ByVal podtLocationBalance As DataTable, ByVal strWhere As String)
    '    ' Gobal Variable
    '    If podtLocationBalance.Rows.Count = 0 Then Exit Sub
    '    Dim boolBlock_Lot As Boolean = USE_BLOCK_LOT()
    '    Dim strSQL As String = ""
    '    Dim sGUID As String
    '    sGUID = System.Guid.NewGuid.ToString()
    '    Dim dblTotal_Qty As Decimal = 0.0
    '    Dim stdPackage_Index As String = ""
    '    dblTotal_Qty = _dblQty_PICKING * GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
    '    stdPackage_Index = GET_StandardPackage_Index(_strSku_Index)

    '    Dim objPicking As New config_CustomSetting
    '    Dim strDEFUALT_PICKFACE_STORE As String = ""
    '    strDEFUALT_PICKFACE_STORE = objPicking.getConfig_Key_DEFUALT("DEFUALT_PICKFACE_STOREPROC")
    '    connectDB()
    '    Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
    '    SQLServerCommand.Transaction = myTrans
    '    Try

    '        strWhere = strWhere.Replace("'", "''")
    '        If strDEFUALT_PICKFACE_STORE = "" Then
    '            strSQL = "  EXEC sp_GetDataPickFace '" & sGUID & "','" & _strSku_Index & "', " & dblTotal_Qty & ",'" & strWhere & "'"
    '        Else
    '            strSQL = "  EXEC " & strDEFUALT_PICKFACE_STORE & " '" & sGUID & "','" & _strSku_Index & "', " & dblTotal_Qty & ",'" & strWhere & "'"
    '        End If

    '        With SQLServerCommand
    '            .Connection = Connection
    '            .Transaction = myTrans
    '            .CommandText = strSQL
    '            .CommandTimeout = 0
    '        End With

    '        DataAdapter.SelectCommand = SQLServerCommand
    '        DataAdapter.SelectCommand.Transaction = myTrans
    '        DS = New DataSet
    '        DataAdapter.Fill(DS, "PICKFACE")

    '        myTrans.Commit()

    '        ' Other Transaction

    '        Dim odrSelect() As DataRow
    '        For Each drPickface As DataRow In DS.Tables("PICKFACE").Rows
    '            odrSelect = podtLocationBalance.Select("chkSelect = 0 and LocationBalance_Index='" & drPickface("LocationBalance_Index").ToString & "'", "Qty_Bal")
    '            _dblQty_BALANCE = CDec(drPickface("PickTotal_Qty"))
    '            _strPackage_Index = stdPackage_Index
    '            RESERV_LOCATION(odrSelect(0))
    '        Next

    '    Catch ex As Exception
    '        myTrans.Rollback()
    '        Throw ex
    '    Finally
    '        disconnectDB()
    '    End Try
    'End Sub

    'Sub PICKING_RESERV_Pickface_Bak(ByVal podtLocationBalance As DataTable)

    '    If podtLocationBalance.Rows.Count = 0 Then Exit Sub

    '    Dim dblMax_Qty As Decimal = podtLocationBalance.Compute("Max(Qty_Bal)", "")
    '    Dim odrSelect() As DataRow

    '    While _dblQty_PICKING >= 0

    '        'case มากกว่าหรือ เท่ากับ
    '        If _dblQty_PICKING >= dblMax_Qty Then
    '            odrSelect = podtLocationBalance.Select("chkSelect = 0 and Qty_Bal >= " & dblMax_Qty, "Qty_Bal DESC,Order_Date, Location_Alias")
    '        Else
    '            'case น้อยกว่า
    '            odrSelect = podtLocationBalance.Select("chkSelect = 0 and Qty_Bal <= " & dblMax_Qty, "Qty_Bal ASC,Order_Date, Location_Alias")

    '        End If

    '        If odrSelect.Length <= 0 Then
    '            Exit Sub
    '        End If

    '        _dblQty_BALANCE = CDec(odrSelect(0)("Qty_Bal"))
    '        odrSelect(0)("chkSelect") = 1

    '        If _dblQty_BALANCE >= _dblQty_PICKING Then
    '            _dblQty_BALANCE = _dblQty_PICKING
    '        End If

    '        _dblQty_PICKING -= _dblQty_BALANCE

    '        '******** RESERV LOCATION BALANCE ********
    '        RESERV_LOCATION(odrSelect(0))

    '        If _dblQty_PICKING <= 0 Then Exit While
    '    End While

    'End Sub

    Function Set_WITHDRAW_NEWLOT() As String
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable
        Dim sqlBlock As String = ""
        Try
            Select Case _enmPicking_Type
                Case enmPicking_Type.SERIAL
                    sqlBlock = " and (Plot not in (  Select      tb_WithdrawItemLocation.Plot   "
                    sqlBlock &= " FROM        tb_WithdrawItemLocation INNER JOIN               "
                    sqlBlock &= " 	tb_LocationBalance  ON tb_WithdrawItemLocation.LocationBalance_Index = tb_LocationBalance.LocationBalance_Index INNER JOIN               "
                    sqlBlock &= " 		tb_Withdraw ON tb_WithdrawItemLocation.Withdraw_Index = tb_Withdraw.Withdraw_Index   "
                    sqlBlock &= " 	WHERE   (tb_Withdraw.Status in (1,2,3)) AND (tb_WithdrawItemLocation.Plot <> '') "
                    sqlBlock &= " 	AND (temp_Asset.Consignee_Index <> '')                "
                    sqlBlock &= " 	AND (temp_Asset.Mfg_Date < tb_LocationBalance.Mfg_Date)               "
                    sqlBlock &= " 	AND (temp_Asset.Consignee_Index = tb_Withdraw.Customer_Shipping_Index) ))   "

                Case Else
                    sqlBlock = " and (Plot not in (  Select      tb_WithdrawItemLocation.Plot   "
                    sqlBlock &= " FROM        tb_WithdrawItemLocation INNER JOIN               "
                    sqlBlock &= " 	tb_LocationBalance  ON tb_WithdrawItemLocation.LocationBalance_Index = tb_LocationBalance.LocationBalance_Index INNER JOIN               "
                    sqlBlock &= " 		tb_Withdraw ON tb_WithdrawItemLocation.Withdraw_Index = tb_Withdraw.Withdraw_Index   "
                    sqlBlock &= " 	WHERE   (tb_Withdraw.Status in (1,2,3)) AND (tb_WithdrawItemLocation.Plot <> '') "
                    sqlBlock &= " 	AND (VIEW_WithdrawReserveLocation.Consignee_Index <> '')                "
                    sqlBlock &= " 	AND (VIEW_WithdrawReserveLocation.Mfg_Date < tb_LocationBalance.Mfg_Date)               "
                    sqlBlock &= " 	AND (VIEW_WithdrawReserveLocation.Consignee_Index = tb_Withdraw.Customer_Shipping_Index) ))   "


            End Select



            Return sqlBlock

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function

    Function GET_SKU_RATIO(ByVal strSku_index As String, ByVal strPackage_index As String) As Decimal
        Try
            Dim dblRation As Decimal = 0
            Dim objDB As New ms_SKURatio(ms_SKURatio.enuOperation_Type.SEARCH)
            Dim objDT As New DataTable
            objDB.SelectData_ByPackage(strSku_index, strPackage_index)
            objDT = objDB.DataTable

            If objDT.Rows.Count > 0 Then
                dblRation = objDT.Rows(0).Item("Ratio")
            End If

            Return dblRation
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Function GET_StandardPackage_Index(ByVal strSku_index As String) As String
        Try
            Dim dblRation As Integer = 0
            Dim objDT As New DataTable

            Dim strSQL As String = ""
            strSQL = " select Package_Index from ms_sku where Sku_index = '" & strSku_index & "' and Status_id <> -1 "

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()
            _scalarOutput = GetScalarOutput
            Return _scalarOutput
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function SEARCHLOCATIONBALANCE_PICKING_AUTO(ByVal strWhere As String, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As DataTable
        Dim strSQL As String = ""

        Try
            If USE_BLOCK_LOT() = True Then
                strSQL = " SELECT  Top 500 *, BLOCK = CASE "
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) >= 1 THEN 'BLOCK'"
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 THEN ''"
                strSQL &= "     End,priority_index=99999"

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select

                If strWhere <> "" Then
                    strSQL &= " WHERE ((select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 ) and  Qty_Bal > 0 " & strWhere
                End If

            Else
                If _DocumentType_Index = "" Then
                    strSQL = " SELECT  Top 500 *,BLOCK = '',priority_index=99999"
                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select
                    If strWhere <> "" Then
                        strSQL &= " WHERE Qty_Bal > 0 " & strWhere
                    End If
                Else
                    strSQL = " select Top 500 VIEW_WithdrawReserveLocation.* from "
                    strSQL &= " (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation inner join (select '1' Temp_Col) b on 1=1 "
                    If strWhere <> "" Then
                        strSQL &= " WHERE Qty_Bal > 0 " & strWhere
                    End If
                End If

            End If

            'Use for adjust : ระบบตรวจนับไม่สนใจยอดปัจจุบัน
            'If useAutoAdjust Then
            '    strSQL = strSQL.Replace("Qty_Bal > 0", "1=1")
            'End If

            Dim dt As New DataTable
            dt = DBExeQuery(strSQL, Connection, myTrans)
            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()


            Return dt

            'connectDB()
            'SetSQLString = strSQL
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'EXEC_Command()
            'Dim objDrAutoNumber As SqlClient.SqlDataReader = Nothing
            'objDrAutoNumber = GetDataReader
            '_dataTable = New DataTable
            '_dataTable.Load(objDrAutoNumber)
            'Return _dataTable

        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
        End Try
    End Function


    Public Function SEARCHLOCATIONBALANCE_PICKING_AUTO(ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""

        Try
            If USE_BLOCK_LOT() = True Then
                strSQL = " SELECT  Top 500 *, BLOCK = CASE "
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) >= 1 THEN 'BLOCK'"
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 THEN ''"
                strSQL &= "     End,priority_index=99999"

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select

                If strWhere <> "" Then
                    strSQL &= " WHERE ((select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 ) and  Qty_Bal > 0 " & strWhere
                End If

            Else
                If _DocumentType_Index = "" Then
                    strSQL = " SELECT  Top 500 *,BLOCK = '',priority_index=99999"
                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select
                    If strWhere <> "" Then
                        strSQL &= " WHERE Qty_Bal > 0 " & strWhere
                    End If
                Else
                    strSQL = " select Top 500 VIEW_WithdrawReserveLocation.* from "
                    strSQL &= " (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation inner join (select '1' Temp_Col) b on 1=1 "
                    If strWhere <> "" Then
                        strSQL &= " WHERE Qty_Bal > 0 " & strWhere
                    End If
                End If

            End If


            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Return GetDataTable

            'connectDB()
            'SetSQLString = strSQL
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Reader
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'EXEC_Command()
            'Dim objDrAutoNumber As SqlClient.SqlDataReader = Nothing
            'objDrAutoNumber = GetDataReader
            '_dataTable = New DataTable
            '_dataTable.Load(objDrAutoNumber)
            'Return _dataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function SEARCHLOCATIONBALANCE_PICKING_CUSTOM(ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""

        Try
            If USE_BLOCK_LOT() = True Then
                strSQL = " SELECT  *, BLOCK = CASE "
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) >= 1 THEN 'BLOCK'"
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 THEN ''"
                strSQL &= "     End,priority_index=99999"

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select


            Else


                If _DocumentType_Index = "" Then
                    strSQL = " SELECT   *,BLOCK = '',priority_index=99999"
                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select

                Else
                    strSQL = " select  VIEW_WithdrawReserveLocation.* from "
                    strSQL &= " (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation inner join (select '1' Temp_Col) b on 1=1 "

                End If

            End If
            If strWhere <> "" Then
                strSQL &= "  WHERE Qty_Bal > 0 " & strWhere
            End If

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Return GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function


    Public Function SEARCHLOCATIONBALANCE_PICKING_CUSTOM(ByVal strWhere As String, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As DataTable
        Dim strSQL As String = ""

        Try
            If USE_BLOCK_LOT() = True Then
                strSQL = " SELECT  *, BLOCK = CASE "
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) >= 1 THEN 'BLOCK'"
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 THEN ''"
                strSQL &= "     End,priority_index=99999"

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select


            Else


                If _DocumentType_Index = "" Then
                    strSQL = " SELECT   *,BLOCK = '',priority_index=99999"
                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select

                Else
                    strSQL = " select  VIEW_WithdrawReserveLocation.* from "
                    strSQL &= " (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation inner join (select '1' Temp_Col) b on 1=1 "

                End If

            End If
            If strWhere <> "" Then
                strSQL &= "  WHERE Qty_Bal > 0 " & strWhere
            End If

            Dim dt As New DataTable
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(dt)

            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function SEARCHLOCATIONBALANCE_ALLBALANCE(ByVal strWhere As String, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As DataTable
        Dim strSQL As String = ""

        Try
            If USE_BLOCK_LOT() = True Then
                strSQL = " SELECT Top 500 *, BLOCK = CASE "
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) >= 1 THEN 'BLOCK'"
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 THEN ''"
                strSQL &= "     End"

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select


            Else
                'strSQL = " SELECT  *,BLOCK = ''"
                'Select Case _enmPicking_Type
                '    Case enmPicking_Type.SERIAL
                '        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                '    Case Else
                '        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                'End Select

                If _DocumentType_Index = "" Then
                    strSQL = " SELECT  Top 500 *,BLOCK = '',priority_index=99999"
                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select

                Else
                    strSQL = " select Top 500 VIEW_WithdrawReserveLocation.* from "
                    strSQL &= " (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation inner join (select '1' Temp_Col) b on 1=1 "

                End If


            End If
            If strWhere <> "" Then
                strSQL &= " WHERE 1=1 " & strWhere
            End If

            Dim dt As New DataTable

            'SetSQLString = strSQL
            'connectDB()
            'EXEC_DataAdapter()
            dt = DBExeQuery(strSQL, Connection, myTrans)

            Return dt



        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
        End Try
    End Function

    Public Function SEARCHLOCATIONBALANCE_ALLBALANCE(ByVal strWhere As String) As DataTable
        Dim strSQL As String = ""

        Try
            If USE_BLOCK_LOT() = True Then
                strSQL = " SELECT Top 500 *, BLOCK = CASE "
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) >= 1 THEN 'BLOCK'"
                strSQL &= "     WHEN (select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 THEN ''"
                strSQL &= "     End"

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select


            Else
                'strSQL = " SELECT  *,BLOCK = ''"
                'Select Case _enmPicking_Type
                '    Case enmPicking_Type.SERIAL
                '        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                '    Case Else
                '        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                'End Select

                If _DocumentType_Index = "" Then
                    strSQL = " SELECT  Top 500 *,BLOCK = '',priority_index=99999"
                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select

                Else
                    strSQL = " select Top 500 VIEW_WithdrawReserveLocation.* from "
                    strSQL &= " (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation inner join (select '1' Temp_Col) b on 1=1 "

                End If


            End If
            If strWhere <> "" Then
                strSQL &= " WHERE 1=1 " & strWhere
            End If

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Return GetDataTable
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Private Function USE_BLOCK_LOT() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_BLOCK_LOT", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim intBlock As Integer = 0
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False

            End If

            'Return intBlock

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function
    '*************************** ASSET *******************
    Public Function SEARCHASSETLOCATIONBALANCE_PICKING(ByVal strWhere As String) As DataTable
        Dim strSQL As String

        Try
            strSQL = "  SELECT  * FROM VIEW_WithDrawReserveAssetLocationBalance "

            If strWhere <> "" Then
                strSQL &= " WHERE 1=1 " & strWhere
            End If
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            Return GetDataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function SET_RESERV_ASSETLOCATIONBALANCE(ByVal AssetLocationBalance_Index As String, ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True
        Try

            '#### WithdrawReserve ##### 28/10/2008
            strSQL &= " Update tb_AssetLocationBalance "
            strSQL &= " SET ReserveQty = " & Qty_Reserv
            strSQL &= " ,ReserveWeight = " & Weight_Reserv
            strSQL &= " ,ReserveVolume =  " & Volume_Reserv
            strSQL &= " ,ReserveQty_Item =  " & ItemQty_Reserv
            strSQL &= " ,ReserveOrderItem_Price =  " & Price_Reserv
            strSQL &= " where AssetLocationBalance_Index='" & AssetLocationBalance_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return True
        Catch ex As Exception
            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try

        'Return check
    End Function

    Public Function SET_RESERV_LOCATIONBALANCE(ByVal LocationBalance_Index As String, ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True
        Try

            '#### WithdrawReserve ##### 28/10/2008

            strSQL &= " Update tb_LocationBalance "
            strSQL &= " SET ReserveQty = " & Qty_Reserv
            strSQL &= " ,ReserveWeight = " & Weight_Reserv
            strSQL &= " ,ReserveVolume =  " & Volume_Reserv
            strSQL &= " ,ReserveQty_Item =  " & ItemQty_Reserv
            strSQL &= " ,ReserveOrderItem_Price =  " & Price_Reserv
            strSQL &= " where LocationBalance_Index='" & LocationBalance_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return True

        Catch ex As Exception
            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function UPDATE_RESERV_ASSETLOCATIONBALANCE(ByVal AssetLocationBalance_Index As String, ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True
        Dim tmpQty_Reserv As Decimal = 0
        Dim tmpWeight_Reserv As Decimal = 0
        Dim tmpVolume_Reserv As Decimal = 0
        Dim tmpItemQty_Reserv As Decimal = 0
        Dim tmpPrice_Reserv As Decimal = 0

        Try


            'Update By neung 12/10/2012#
            strSQL = " SELECT ReserveQty,ReserveWeight,ReserveVolume,ReserveQty_Item,ReserveOrderItem_Price FROM tb_AssetLocationBalance Where AssetLocationBalance_Index = '" & AssetLocationBalance_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then
                Return False
                Exit Function
            Else

                With _dataTable.Rows(0)
                    tmpQty_Reserv = .Item("ReserveQty")
                    tmpWeight_Reserv = .Item("ReserveWeight")
                    tmpVolume_Reserv = .Item("ReserveVolume")
                    tmpItemQty_Reserv = .Item("ReserveQty_Item")
                    tmpPrice_Reserv = .Item("ReserveOrderItem_Price")
                End With
            End If
            tmpQty_Reserv = FormatNumber((tmpQty_Reserv + Qty_Reserv), 4)
            tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv + Weight_Reserv), 4)
            tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv + Volume_Reserv), 4)
            tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv + ItemQty_Reserv), 4)
            tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv + Price_Reserv), 4)


            '#### WithdrawReserve ##### 28/10/2008
            strSQL &= " Update tb_AssetLocationBalance "
            'strSQL &= " SET ReserveQty = ReserveQty + " & Qty_Reserv
            'strSQL &= " ,ReserveWeight = ReserveWeight + " & Weight_Reserv
            'strSQL &= " ,ReserveVolume = ReserveVolume + " & Volume_Reserv
            'strSQL &= " ,ReserveQty_Item = ReserveQty_Item + " & ItemQty_Reserv
            'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price + " & Price_Reserv
            strSQL &= " SET ReserveQty = " & tmpQty_Reserv & ""
            strSQL &= " ,ReserveWeight = " & tmpWeight_Reserv & ""
            strSQL &= " ,ReserveVolume = " & tmpVolume_Reserv & ""
            strSQL &= " ,ReserveQty_Item = " & tmpItemQty_Reserv & ""
            strSQL &= " ,ReserveOrderItem_Price = " & tmpPrice_Reserv & ""
            strSQL &= " where AssetLocationBalance_Index='" & AssetLocationBalance_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            '#


            Return True

        Catch ex As Exception

            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try

        'Return check
    End Function

    Public Function DELETE_RESERV_ASSETLOCATIONBALANCE(ByVal AssetLocationBalance_Index As String, ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True
        Dim tmpQty_Reserv As Decimal = 0
        Dim tmpWeight_Reserv As Decimal = 0
        Dim tmpVolume_Reserv As Decimal = 0
        Dim tmpItemQty_Reserv As Decimal = 0
        Dim tmpPrice_Reserv As Decimal = 0


        Try

            'Update By neung 12/10/2012#
            strSQL = " SELECT ReserveQty,ReserveWeight,ReserveVolume,ReserveQty_Item,ReserveOrderItem_Price FROM tb_AssetLocationBalance Where AssetLocationBalance_Index = '" & AssetLocationBalance_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then
                Return False
                Exit Function
            Else

                With _dataTable.Rows(0)
                    tmpQty_Reserv = .Item("ReserveQty")
                    tmpWeight_Reserv = .Item("ReserveWeight")
                    tmpVolume_Reserv = .Item("ReserveVolume")
                    tmpItemQty_Reserv = .Item("ReserveQty_Item")
                    tmpPrice_Reserv = .Item("ReserveOrderItem_Price")
                End With
            End If

            tmpQty_Reserv = FormatNumber((tmpQty_Reserv - Qty_Reserv), 4)
            tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv - Weight_Reserv), 4)
            tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv - Volume_Reserv), 4)
            tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv - ItemQty_Reserv), 4)
            tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv - Price_Reserv), 4)



            '#### WithdrawReserve ##### 28/10/2008
            strSQL &= " Update tb_AssetLocationBalance "
            'strSQL &= " SET ReserveQty = ReserveQty - " & Qty_Reserv
            'strSQL &= " ,ReserveWeight = ReserveWeight - " & Weight_Reserv
            'strSQL &= " ,ReserveVolume = ReserveVolume - " & Volume_Reserv
            'strSQL &= " ,ReserveQty_Item = ReserveQty_Item - " & ItemQty_Reserv
            'strSQL &= " ,ReserveOrderItem_Price = ReserveOrderItem_Price - " & Price_Reserv
            strSQL &= " SET ReserveQty = " & tmpQty_Reserv & ""
            strSQL &= " ,ReserveWeight = " & tmpWeight_Reserv & ""
            strSQL &= " ,ReserveVolume = " & tmpVolume_Reserv & ""
            strSQL &= " ,ReserveQty_Item = " & tmpItemQty_Reserv & ""
            strSQL &= " ,ReserveOrderItem_Price = " & tmpPrice_Reserv & ""
            strSQL &= " where AssetLocationBalance_Index='" & AssetLocationBalance_Index & "'"

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()
            '#

            Return True
        Catch ex As Exception

            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try

        'Return check
    End Function

    Public Function UPDATE_RESERVLOCATIONBALANCE(ByVal LocationBalance_Index As String, ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True

        Dim tmpQty_Reserv As Decimal = 0
        Dim tmpWeight_Reserv As Decimal = 0
        Dim tmpVolume_Reserv As Decimal = 0
        Dim tmpItemQty_Reserv As Decimal = 0
        Dim tmpPrice_Reserv As Decimal = 0


        Try

            'Update By neung 12/10/2012#
            strSQL = " SELECT ReserveQty,ReserveWeight,ReserveVolume,ReserveQty_Item,ReserveOrderItem_Price FROM tb_LocationBalance Where LocationBalance_Index = '" & LocationBalance_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then
                Return False
                Exit Function
            Else

                With _dataTable.Rows(0)
                    tmpQty_Reserv = .Item("ReserveQty")
                    tmpWeight_Reserv = .Item("ReserveWeight")
                    tmpVolume_Reserv = .Item("ReserveVolume")
                    tmpItemQty_Reserv = .Item("ReserveQty_Item")
                    tmpPrice_Reserv = .Item("ReserveOrderItem_Price")
                End With
            End If



            '#### WithdrawReserve ##### 28/10/2008
            tmpQty_Reserv = FormatNumber((tmpQty_Reserv + Qty_Reserv), 4)
            tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv + Weight_Reserv), 4)
            tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv + Volume_Reserv), 4)
            tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv + ItemQty_Reserv), 4)
            tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv + Price_Reserv), 4)

            strSQL = " "
            strSQL &= " Update tb_LocationBalance "
            strSQL &= " SET ReserveQty = " & tmpQty_Reserv & ""
            strSQL &= " ,ReserveWeight = " & tmpWeight_Reserv & ""
            strSQL &= " ,ReserveVolume = " & tmpVolume_Reserv & ""
            strSQL &= " ,ReserveQty_Item = " & ItemQty_Reserv & ""
            strSQL &= " ,ReserveOrderItem_Price = " & tmpPrice_Reserv & ""
            strSQL &= " where LocationBalance_Index='" & LocationBalance_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '#


            Return True
        Catch ex As Exception

            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try

        'Return check
    End Function

    Public Function DELETE_RESERVLOCATIONBALANCE(ByVal LocationBalance_Index As String, ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean
        Dim strSQL As String = ""
        Dim check As Boolean = True

        Dim tmpQty_Reserv As Decimal = 0
        Dim tmpWeight_Reserv As Decimal = 0
        Dim tmpVolume_Reserv As Decimal = 0
        Dim tmpItemQty_Reserv As Decimal = 0
        Dim tmpPrice_Reserv As Decimal = 0

        Try
            'Update By neung 12/10/2012#
            strSQL = " SELECT ReserveQty,ReserveWeight,ReserveVolume,ReserveQty_Item,ReserveOrderItem_Price FROM tb_LocationBalance Where LocationBalance_Index = '" & LocationBalance_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count = 0 Then
                Return False
                Exit Function
            Else

                With _dataTable.Rows(0)
                    tmpQty_Reserv = .Item("ReserveQty")
                    tmpWeight_Reserv = .Item("ReserveWeight")
                    tmpVolume_Reserv = .Item("ReserveVolume")
                    tmpItemQty_Reserv = .Item("ReserveQty_Item")
                    tmpPrice_Reserv = .Item("ReserveOrderItem_Price")
                End With
            End If



            '#### WithdrawReserve ##### 28/10/2008
            tmpQty_Reserv = FormatNumber((tmpQty_Reserv - Qty_Reserv), 4)
            tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv - Weight_Reserv), 4)
            tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv - Volume_Reserv), 4)
            tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv - ItemQty_Reserv), 4)
            tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv - Price_Reserv), 4)



            strSQL = " "
            strSQL &= " Update tb_LocationBalance "
            strSQL &= " SET ReserveQty = " & tmpQty_Reserv & ""
            strSQL &= " ,ReserveWeight = " & tmpWeight_Reserv & ""
            strSQL &= " ,ReserveVolume = " & tmpVolume_Reserv & ""
            strSQL &= " ,ReserveQty_Item = " & tmpItemQty_Reserv & ""
            strSQL &= " ,ReserveOrderItem_Price = " & tmpPrice_Reserv & ""
            strSQL &= " where LocationBalance_Index='" & LocationBalance_Index & "'"


            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            '#


            Return True

        Catch ex As Exception

            Throw ex
            'check = False
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function UPDATE_RESERVLOCATIONBALANCE_TRANSACTION_TRAN_KSL_ADDLOG(ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, _
                  ByVal enmAction As enmPicking_Action, ByVal pProcess_id As Integer, ByVal pDocument_Index As String, ByVal pProcess_Text As String, ByVal LocationBalance_Index As String, _
                  ByVal Qty_Recieve_Package As Decimal, ByVal Qty_Bal As Decimal, ByVal Weight_Bal As Decimal, ByVal Volume_Bal As Decimal, ByVal Qty_Item_Bal As Decimal, ByVal OrderItem_Price_Bal As Decimal, _
                  ByVal Qty_Reserv As Decimal, ByVal Weight_Reserv As Decimal, ByVal Volume_Reserv As Decimal, ByVal ItemQty_Reserv As Decimal, ByVal Price_Reserv As Decimal) As Boolean

        Dim strSQL As String = ""

        Dim tmpQty_Recieve_Package As Decimal = 0
        Dim tmpQty_Bal As Decimal = 0
        Dim tmpWeight_Bal As Decimal = 0
        Dim tmpVolume_Bal As Decimal = 0
        Dim tmpQty_Item_Bal As Decimal = 0
        Dim tmpOrderItem_Price_Bal As Decimal = 0

        Dim tmpQty_Reserv As Decimal = 0
        Dim tmpWeight_Reserv As Decimal = 0
        Dim tmpVolume_Reserv As Decimal = 0
        Dim tmpItemQty_Reserv As Decimal = 0
        Dim tmpPrice_Reserv As Decimal = 0

        'Variable Temp Log
        Dim tmpQty_Recieve_Package_Log As Decimal = 0
        Dim tmpQty_Bal_Log As Decimal = 0
        Dim tmpWeight_Bal_Log As Decimal = 0
        Dim tmpVolume_Bal_Log As Decimal = 0
        Dim tmpQty_Item_Bal_Log As Decimal = 0
        Dim tmpOrderItem_Price_Bal_Log As Decimal = 0

        Dim tmpQty_Reserv_Log As Decimal = 0
        Dim tmpWeight_Reserv_Log As Decimal = 0
        Dim tmpVolume_Reserv_Log As Decimal = 0
        Dim tmpItemQty_Reserv_Log As Decimal = 0
        Dim tmpPrice_Reserv_Log As Decimal = 0

        Dim boolTran As Boolean = False
        If myTrans Is Nothing Then
            boolTran = True
        End If


        If boolTran Then
            Connection = New SqlClient.SqlConnection(WV_ConnectionString)
            'connectDB()
            Connection.Open()
            myTrans = Connection.BeginTransaction(IsolationLevel.Serializable)
            SQLServerCommand.Transaction = myTrans
            SQLServerCommand.CommandTimeout = 0

            DBExeNonQuery(" update tb_LocationBalance set LocationBalance_Index = '0010000000001' where LocationBalance_Index = '0010000000001' ", Connection, myTrans)
        End If

        Try

            strSQL = " "
            strSQL = " SELECT Qty_Recieve_Package,Qty_Bal,Weight_Bal,Volume_Bal,Qty_Item_Bal,OrderItem_Price_Bal," & _
            " ReserveQty,ReserveWeight,ReserveVolume,ReserveQty_Item,ReserveOrderItem_Price FROM tb_LocationBalance(nolock) " & _
            " Where LocationBalance_Index = '" & LocationBalance_Index & "'"

            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = strSQL
                .CommandTimeout = 0
            End With

            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(_dataTable)


            If _dataTable.Rows.Count = 0 Then
                Return False
                Exit Function
            Else
                With _dataTable.Rows(0)
                    tmpQty_Recieve_Package = .Item("Qty_Recieve_Package")
                    tmpQty_Bal = .Item("Qty_Bal")
                    tmpWeight_Bal = .Item("Weight_Bal")
                    tmpVolume_Bal = .Item("Volume_Bal")
                    tmpQty_Item_Bal = .Item("Qty_Item_Bal")
                    tmpOrderItem_Price_Bal = .Item("OrderItem_Price_Bal")
                    tmpQty_Reserv = .Item("ReserveQty")
                    tmpWeight_Reserv = .Item("ReserveWeight")
                    tmpVolume_Reserv = .Item("ReserveVolume")
                    tmpItemQty_Reserv = .Item("ReserveQty_Item")
                    tmpPrice_Reserv = .Item("ReserveOrderItem_Price")
                End With
            End If

            tmpQty_Recieve_Package_Log = (FormatNumber(tmpQty_Recieve_Package, 6))
            tmpQty_Bal_Log = (FormatNumber(tmpQty_Bal, 6))
            tmpWeight_Bal_Log = (FormatNumber(tmpWeight_Bal, 6))
            tmpVolume_Bal_Log = (FormatNumber(tmpVolume_Bal, 6))
            tmpQty_Item_Bal_Log = (FormatNumber(tmpQty_Item_Bal, 6))
            tmpOrderItem_Price_Bal_Log = (FormatNumber(tmpOrderItem_Price_Bal, 6))


            Select Case enmAction
                Case enmPicking_Action.ADDBALANCE 'BALANCE
                    tmpQty_Recieve_Package_Log = (FormatNumber(Qty_Recieve_Package, 6))
                    tmpQty_Bal_Log = (FormatNumber(Qty_Bal, 6))
                    tmpWeight_Bal_Log = (FormatNumber(Weight_Bal, 6))
                    tmpVolume_Bal_Log = (FormatNumber(Volume_Bal, 6))
                    tmpQty_Item_Bal_Log = (FormatNumber(Qty_Item_Bal, 6))
                    tmpOrderItem_Price_Bal_Log = (FormatNumber(OrderItem_Price_Bal, 6))

                    tmpQty_Recieve_Package = FormatNumber((tmpQty_Recieve_Package + Qty_Recieve_Package), 6)
                    tmpQty_Bal = FormatNumber((tmpQty_Bal + Qty_Bal), 6)
                    tmpWeight_Bal = FormatNumber((tmpWeight_Bal + Weight_Bal), 6)
                    tmpVolume_Bal = FormatNumber((tmpVolume_Bal + Volume_Bal), 6)
                    tmpQty_Item_Bal = FormatNumber((tmpQty_Item_Bal + Qty_Item_Bal), 6)
                    tmpOrderItem_Price_Bal = FormatNumber((tmpOrderItem_Price_Bal + OrderItem_Price_Bal), 6)

                Case enmPicking_Action.DELBALANCE
                    tmpQty_Recieve_Package = FormatNumber((tmpQty_Recieve_Package - Qty_Recieve_Package), 6)
                    tmpQty_Bal = FormatNumber((tmpQty_Bal - Qty_Bal), 6)
                    tmpWeight_Bal = FormatNumber((tmpWeight_Bal - Weight_Bal), 6)
                    tmpVolume_Bal = FormatNumber((tmpVolume_Bal - Volume_Bal), 6)
                    tmpQty_Item_Bal = FormatNumber((tmpQty_Item_Bal - Qty_Item_Bal), 6)
                    tmpOrderItem_Price_Bal = FormatNumber((tmpOrderItem_Price_Bal - OrderItem_Price_Bal), 6)

                    tmpQty_Recieve_Package_Log = -(FormatNumber(Qty_Recieve_Package, 6))
                    tmpQty_Bal_Log = -(FormatNumber(Qty_Bal, 6))
                    tmpWeight_Bal_Log = -(FormatNumber(Weight_Bal, 6))
                    tmpVolume_Bal_Log = -(FormatNumber(Volume_Bal, 6))
                    tmpQty_Item_Bal_Log = -(FormatNumber(Qty_Item_Bal, 6))
                    tmpOrderItem_Price_Bal_Log = -(FormatNumber(OrderItem_Price_Bal, 6))
                Case enmPicking_Action.ADDRESERVE 'RESERVE
                    tmpQty_Reserv = FormatNumber((tmpQty_Reserv + Qty_Reserv), 6)
                    tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv + Weight_Reserv), 6)
                    tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv + Volume_Reserv), 6)
                    tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv + ItemQty_Reserv), 6)
                    tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv + Price_Reserv), 6)

                    tmpQty_Reserv_Log = (FormatNumber(Qty_Reserv, 6))
                    tmpWeight_Reserv_Log = (FormatNumber(Weight_Reserv, 6))
                    tmpVolume_Reserv_Log = (FormatNumber(Volume_Reserv, 6))
                    tmpItemQty_Reserv_Log = (FormatNumber(ItemQty_Reserv, 6))
                    tmpPrice_Reserv_Log = (FormatNumber(Price_Reserv, 6))


                Case enmPicking_Action.DELRESERVE
                    tmpQty_Reserv = FormatNumber((tmpQty_Reserv - Qty_Reserv), 6)
                    tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv - Weight_Reserv), 6)
                    tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv - Volume_Reserv), 6)
                    tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv - ItemQty_Reserv), 6)
                    tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv - Price_Reserv), 6)

                    tmpQty_Reserv_Log = -(FormatNumber(Qty_Reserv, 6))
                    tmpWeight_Reserv_Log = -(FormatNumber(Weight_Reserv, 6))
                    tmpVolume_Reserv_Log = -(FormatNumber(Volume_Reserv, 6))
                    tmpItemQty_Reserv_Log = -(FormatNumber(ItemQty_Reserv, 6))
                    tmpPrice_Reserv_Log = -(FormatNumber(Price_Reserv, 6))

                Case enmPicking_Action.ADDBALANCE_RESERVE 'BALANCE & RESERVE
                    tmpQty_Recieve_Package = FormatNumber((tmpQty_Recieve_Package + Qty_Recieve_Package), 6)
                    tmpQty_Bal = FormatNumber((tmpQty_Bal + Qty_Bal), 6)
                    tmpWeight_Bal = FormatNumber((tmpWeight_Bal + Weight_Bal), 6)
                    tmpVolume_Bal = FormatNumber((tmpVolume_Bal + Volume_Bal), 6)
                    tmpQty_Item_Bal = FormatNumber((tmpQty_Item_Bal + Qty_Item_Bal), 6)
                    tmpOrderItem_Price_Bal = FormatNumber((tmpOrderItem_Price_Bal + OrderItem_Price_Bal), 6)
                    tmpQty_Reserv = FormatNumber((tmpQty_Reserv + Qty_Reserv), 6)
                    tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv + Weight_Reserv), 6)
                    tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv + Volume_Reserv), 6)
                    tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv + ItemQty_Reserv), 6)
                    tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv + Price_Reserv), 6)

                    tmpQty_Recieve_Package_Log = (FormatNumber(Qty_Recieve_Package, 6))
                    tmpQty_Bal_Log = (FormatNumber(Qty_Bal, 6))
                    tmpWeight_Bal_Log = (FormatNumber(Weight_Bal, 6))
                    tmpVolume_Bal_Log = (FormatNumber(Volume_Bal, 6))
                    tmpQty_Item_Bal_Log = (FormatNumber(Qty_Item_Bal, 6))
                    tmpOrderItem_Price_Bal_Log = (FormatNumber(OrderItem_Price_Bal, 6))
                    tmpQty_Reserv_Log = (FormatNumber(Qty_Reserv, 6))
                    tmpWeight_Reserv_Log = (FormatNumber(Weight_Reserv, 6))
                    tmpVolume_Reserv_Log = (FormatNumber(Volume_Reserv, 6))
                    tmpItemQty_Reserv_Log = (FormatNumber(ItemQty_Reserv, 6))
                    tmpPrice_Reserv_Log = (FormatNumber(Price_Reserv, 6))
                Case enmPicking_Action.DELBALANCE_RESERVE
                    tmpQty_Recieve_Package = FormatNumber((tmpQty_Recieve_Package - Qty_Recieve_Package), 6)
                    tmpQty_Bal = FormatNumber((tmpQty_Bal - Qty_Bal), 6)
                    tmpWeight_Bal = FormatNumber((tmpWeight_Bal - Weight_Bal), 6)
                    tmpVolume_Bal = FormatNumber((tmpVolume_Bal - Volume_Bal), 6)
                    tmpQty_Item_Bal = FormatNumber((tmpQty_Item_Bal - Qty_Item_Bal), 6)
                    tmpOrderItem_Price_Bal = FormatNumber((tmpOrderItem_Price_Bal - OrderItem_Price_Bal), 6)
                    tmpQty_Reserv = FormatNumber((tmpQty_Reserv - Qty_Reserv), 6)
                    tmpWeight_Reserv = FormatNumber((tmpWeight_Reserv - Weight_Reserv), 6)
                    tmpVolume_Reserv = FormatNumber((tmpVolume_Reserv - Volume_Reserv), 6)
                    tmpItemQty_Reserv = FormatNumber((tmpItemQty_Reserv - ItemQty_Reserv), 6)
                    tmpPrice_Reserv = FormatNumber((tmpPrice_Reserv - Price_Reserv), 6)

                    tmpQty_Recieve_Package_Log = -(FormatNumber(Qty_Recieve_Package, 6))
                    tmpQty_Bal_Log = -(FormatNumber(Qty_Bal, 6))
                    tmpWeight_Bal_Log = -(FormatNumber(Weight_Bal, 6))
                    tmpVolume_Bal_Log = -(FormatNumber(Volume_Bal, 6))
                    tmpQty_Item_Bal_Log = -(FormatNumber(Qty_Item_Bal, 6))
                    tmpOrderItem_Price_Bal_Log = -(FormatNumber(OrderItem_Price_Bal, 6))
                    tmpQty_Reserv_Log = -(FormatNumber(Qty_Reserv, 6))
                    tmpWeight_Reserv_Log = -(FormatNumber(Weight_Reserv, 6))
                    tmpVolume_Reserv_Log = -(FormatNumber(Volume_Reserv, 6))
                    tmpItemQty_Reserv_Log = -(FormatNumber(ItemQty_Reserv, 6))
                    tmpPrice_Reserv_Log = -(FormatNumber(Price_Reserv, 6))
            End Select
            If enmAction = enmPicking_Action.ADDBALANCE Or enmAction = enmPicking_Action.DELBALANCE Then
                strSQL = " "
                strSQL = "  UPDATE tb_LocationBalance    "
                strSQL &= " SET Qty_Recieve_Package= " & tmpQty_Recieve_Package & " "
                strSQL &= " ,Qty_Bal= " & tmpQty_Bal & " "
                strSQL &= " ,Weight_Bal=" & tmpWeight_Bal & " "
                strSQL &= " ,Volume_Bal= " & tmpVolume_Bal & " "
                strSQL &= " ,Qty_Item_Bal = " & tmpQty_Item_Bal & " "
                strSQL &= " ,OrderItem_Price_Bal=" & tmpOrderItem_Price_Bal & " "
                strSQL &= " where LocationBalance_Index='" & LocationBalance_Index & "'"

            ElseIf enmAction = enmPicking_Action.ADDRESERVE Or enmAction = enmPicking_Action.DELRESERVE Then
                strSQL = " "
                strSQL &= " Update tb_LocationBalance "
                strSQL &= " SET ReserveQty = " & tmpQty_Reserv & ""
                strSQL &= " ,ReserveWeight = " & tmpWeight_Reserv & ""
                strSQL &= " ,ReserveVolume = " & tmpVolume_Reserv & ""
                strSQL &= " ,ReserveQty_Item = " & tmpItemQty_Reserv & ""
                strSQL &= " ,ReserveOrderItem_Price = " & tmpPrice_Reserv & ""
                strSQL &= " where LocationBalance_Index='" & LocationBalance_Index & "'"
            ElseIf enmAction = enmPicking_Action.ADDBALANCE_RESERVE Or enmAction = enmPicking_Action.DELBALANCE_RESERVE Then
                strSQL = " "
                strSQL = "  UPDATE tb_LocationBalance    "
                strSQL &= " SET Qty_Recieve_Package= " & tmpQty_Recieve_Package & " "
                strSQL &= " ,Qty_Bal= " & tmpQty_Bal & " "
                strSQL &= " ,Weight_Bal=" & tmpWeight_Bal & " "
                strSQL &= " ,Volume_Bal= " & tmpVolume_Bal & " "
                strSQL &= " ,Qty_Item_Bal = " & tmpQty_Item_Bal & " "
                strSQL &= " ,OrderItem_Price_Bal=" & tmpOrderItem_Price_Bal & " "
                strSQL &= " ,ReserveQty = " & tmpQty_Reserv & ""
                strSQL &= " ,ReserveWeight = " & tmpWeight_Reserv & ""
                strSQL &= " ,ReserveVolume = " & tmpVolume_Reserv & ""
                strSQL &= " ,ReserveQty_Item = " & tmpItemQty_Reserv & ""
                strSQL &= " ,ReserveOrderItem_Price = " & tmpPrice_Reserv & ""
                strSQL &= " where LocationBalance_Index='" & LocationBalance_Index & "'"
            End If

            With SQLServerCommand
                .CommandType = CommandType.Text
                .CommandText = strSQL
                .Connection = Connection
                .ExecuteNonQuery()
            End With

            '**********************************************************************************************
            'Insert Log
            strSQL = " Insert Into tb_LocationBalanceTransaction "
            strSQL &= "(Process_Id,DocumentType_Index,Document_Index,LocationBalance_Index,"
            strSQL &= " Qty_Recieve_Package,Qty_Bal ,Weight_Bal,Volume_Bal,Qty_Item_Bal,OrderItem_Price_Bal, "
            strSQL &= " ReserveQty,ReserveWeight,ReserveVolume,ReserveQty_Item,ReserveOrderItem_Price, "
            strSQL &= " Process_Text,add_by,add_date,add_ip,add_ComputerName)"
            strSQL &= " VALUES "
            strSQL &= "(@Process_Id,@DocumentType_Index,@Document_Index, @LocationBalance_Index,"
            strSQL &= " @Qty_Recieve_Package,@Qty_Bal,@Weight_Bal,@Volume_Bal,@Qty_Item_Bal,@OrderItem_Price_Bal,"
            strSQL &= " @ReserveQty,@ReserveWeight,@ReserveVolume,@ReserveQty_Item,@ReserveOrderItem_Price,"
            strSQL &= " @Process_Text,@add_by,getdate(),@add_ip,@add_ComputerName)"

            'log balance
            With SQLServerCommand
                .Parameters.Clear()
                '.Parameters.Add("@TransectionLog_Index", SqlDbType.VarChar, 13).Value = Transectionlog_index
                .Parameters.Add("@Process_Id", SqlDbType.Int).Value = pProcess_id
                .Parameters.Add("@DocumentType_Index", SqlDbType.VarChar, 13).Value = "" ' pDocumentType_Index' dong remove 2017/06/21
                .Parameters.Add("@Document_Index", SqlDbType.VarChar, 13).Value = pDocument_Index
                .Parameters.Add("@LocationBalance_Index", SqlDbType.VarChar, 13).Value = LocationBalance_Index
                .Parameters.Add("@Qty_Recieve_Package", SqlDbType.Float).Value = tmpQty_Recieve_Package_Log
                .Parameters.Add("@Qty_Bal", SqlDbType.Float).Value = tmpQty_Bal_Log
                .Parameters.Add("@Weight_Bal", SqlDbType.Float).Value = tmpWeight_Bal_Log
                .Parameters.Add("@Volume_Bal", SqlDbType.Float).Value = tmpVolume_Bal_Log
                .Parameters.Add("@Qty_Item_Bal", SqlDbType.Float).Value = tmpQty_Item_Bal_Log
                .Parameters.Add("@OrderItem_Price_Bal", SqlDbType.Float).Value = tmpOrderItem_Price_Bal_Log
                .Parameters.Add("@ReserveQty", SqlDbType.Float).Value = tmpQty_Reserv_Log
                .Parameters.Add("@ReserveWeight", SqlDbType.Float).Value = tmpWeight_Reserv_Log
                .Parameters.Add("@ReserveVolume", SqlDbType.Float).Value = tmpVolume_Reserv_Log
                .Parameters.Add("@ReserveQty_Item", SqlDbType.Float).Value = tmpItemQty_Reserv
                .Parameters.Add("@ReserveOrderItem_Price", SqlDbType.Float).Value = tmpPrice_Reserv_Log
                .Parameters.Add("@Process_Text", SqlDbType.VarChar, 200).Value = pProcess_Text
                .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                .Parameters.Add("@add_ip", SqlDbType.VarChar, 100).Value = WV_Host_Ip
                .Parameters.Add("@add_ComputerName", SqlDbType.VarChar, 100).Value = WV_Host_Name
            End With

            With SQLServerCommand
                .CommandType = CommandType.Text
                .CommandText = strSQL
                .Connection = Connection
                .ExecuteNonQuery()
            End With
            '**********************************************************************************************


            If boolTran Then
                myTrans.Commit()
            End If

            Return True
        Catch ex As Exception
            If boolTran Then
                myTrans.Rollback()
                Throw ex
            Else
                Throw ex
            End If
        Finally
            If boolTran Then
                'disconnectDB()
                Connection.Close()
            End If

        End Try
    End Function

#End Region

#Region "CHECK PICKING"

    Public Function CHEK_QTY_BALANCE_For_Makalin(ByVal strCondition As String) As Boolean 'ja add 02-03-2010

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  Sum(Qty_Bal) as Qty_Bal"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation "

            If _strCondition <> "" Then
                strSQL &= " WHERE 1=1 " & _strCondition
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()


            If GetScalarOutput.Trim = "0" Or GetScalarOutput.Trim = "" Then
                _dblQty_BALANCE = 0
            Else
                _dblQty_BALANCE = GetScalarOutput
            End If

            Dim _dblRatio As Decimal = 1
            _dblRatio = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            If _dblRatio <= 1 Then _dblRatio = 1
            If _dblQty_BALANCE < (_dblQty_PICKING * _dblRatio) Then
                chkBalance = True
                'W_MSG_Information("สินค้าในคลัง มีจำนวนไม่พอ คงเหลือ " & _dblQty_BALANCE)
                Return False
            Else
                chkBalance = False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CHEK_QTY_BALANCE(ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As Boolean

        Dim strSQL As String = ""
        Dim _GetScalarOutput As String = ""
        Try
            strSQL = " SELECT isnull( Sum(isnull(Qty_Bal,0)),0) as Qty_Bal"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation "

            If _strCondition <> "" Then
                strSQL &= " WHERE 1=1 " & _strCondition
            End If

            _GetScalarOutput = DBExeQuery_Scalar(strSQL, Connection, myTrans, eCommandType.Text)



            'SetSQLString = strSQL
            'SetCommandType = DBType_SQLServer.enuCommandType.Text
            'SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            'connectDB()
            'EXEC_Command()


            If _GetScalarOutput.Trim = "0" Or _GetScalarOutput.Trim = "" Then
                _dblQty_BALANCE = 0
            Else
                _dblQty_BALANCE = _GetScalarOutput
            End If

            Dim _dblRatio As Decimal = 1
            _dblRatio = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            If _dblRatio <= 1 Then _dblRatio = 1
            If _dblQty_BALANCE < (_dblQty_PICKING * _dblRatio) Then
                chkBalance = True
                'W_MSG_Information("สินค้าในคลัง มีจำนวนไม่พอ คงเหลือ " & _dblQty_BALANCE)
                Return False
            Else
                chkBalance = False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function


    Public Function CHEK_QTY_BALANCE() As Boolean

        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  Sum(Qty_Bal) as Qty_Bal"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation "

            If _strCondition <> "" Then
                strSQL &= " WHERE 1=1 " & _strCondition
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()


            If GetScalarOutput.Trim = "0" Or GetScalarOutput.Trim = "" Then
                _dblQty_BALANCE = 0
            Else
                _dblQty_BALANCE = GetScalarOutput
            End If

            Dim _dblRatio As Decimal = 1
            _dblRatio = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            If _dblRatio <= 1 Then _dblRatio = 1
            If _dblQty_BALANCE < (_dblQty_PICKING * _dblRatio) Then
                chkBalance = True
                'W_MSG_Information("สินค้าในคลัง มีจำนวนไม่พอ คงเหลือ " & _dblQty_BALANCE)
                Return False
            Else
                chkBalance = False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function CHEK_QTY_BALANCE_Asset() As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  Sum(Qty_Bal) as Qty_Bal"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "

            If _strCondition <> "" Then
                strSQL &= " WHERE 1=1 " & _strCondition
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()


            If GetScalarOutput.Trim = "0" Or GetScalarOutput.Trim = "" Then
                _dblQty_BALANCE = 0
            Else
                _dblQty_BALANCE = GetScalarOutput
            End If

            Dim _dblRatio As Decimal = 1
            _dblRatio = GET_SKU_RATIO(_strSku_Index, _strPackage_Index)
            If _dblRatio <= 1 Then _dblRatio = 1
            If _dblQty_BALANCE < (_dblQty_PICKING * _dblRatio) Then
                chkBalance = True
                'W_MSG_Information("สินค้าในคลัง มีจำนวนไม่พอ คงเหลือ " & _dblQty_BALANCE)
                Return False
            Else
                chkBalance = False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Public Function CHEK_SUMQTY_BALANCE(ByVal strAndCondition As String) As Decimal
        Dim strSQL As String = ""
        Dim dblQty As Decimal = 0
        Try
            strSQL = " SELECT  Sum(Qty_Bal) as Qty_Bal"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation "

            If strAndCondition <> "" Then
                strSQL &= " WHERE Qty_Bal > 0 " & strAndCondition
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()


            If GetScalarOutput.Trim = "0" Or GetScalarOutput.Trim = "" Then
                dblQty = 0
            Else
                dblQty = GetScalarOutput
            End If

            Return dblQty
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Function USE_WITHDRAW_PICKING_NEWLOT() As Boolean
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_WITHDRAW_PICKING_NEWLOT", " AND Config_Value = 1 ")
            objDT = objCustomSetting.DataTable
            Dim intBlock As Integer = 0
            If objDT.Rows.Count > 0 Then
                Return True
            Else
                Return False

            End If

            'Return intBlock

        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Function

    Public Function CHEK_QTY_BALANCE_Update_Mobile() As Boolean
        Dim strSQL As String = ""
        Try
            strSQL = " SELECT  Sum(Qty_Bal) as Qty_Bal"
            strSQL &= " FROM  VIEW_WithdrawReserveLocation "

            If _strCondition <> "" Then
                strSQL &= " WHERE 1=1 " & _strCondition
            End If

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.Scalar
            connectDB()
            EXEC_Command()


            If GetScalarOutput.Trim = "0" Or GetScalarOutput.Trim = "" Then
                _dblQty_BALANCE = 0
            Else
                _dblQty_BALANCE = GetScalarOutput
            End If

            If _dblQty_BALANCE < _dblQty_PICKING Then
                chkBalance = True
                Return False
            Else
                chkBalance = False
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function
#End Region


#Region "   Config Show Hide Column   "
    Public Function getFieldConfig_WithDraw() As DataTable
        Dim strSQL As String
        Try
            strSQL = " SELECT *   " & _
                     " FROM   config_Withdraw " & _
                     " WHERE IsUse=0 "
            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function getFieldConfigWithDrawAsset_Reserv(ByVal seq_Control As Integer) As DataTable
        Dim strSQL As String
        Dim strseq = " and seq = " & seq_Control
        Try

            strSQL = " SELECT *   " & _
                     " FROM   config_Withdraw_Reserv " & _
                     " WHERE IsUse=0 "


            SetSQLString = strSQL & strseq
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            Return _dataTable

        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

#End Region

    '--ADDNEW V3
    Public Function GetDataLastIssueOption(ByVal pSku_Index As String, ByVal pCustomer_Shipping_Location_Index As String, ByVal Connection As SqlClient.SqlConnection, ByVal Mytrans As SqlClient.SqlTransaction) As DataTable
        Try
            Dim dt As DataTable
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = pSku_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = pCustomer_Shipping_Location_Index
            End With

            dt = DBExeQuery(" SELECT * FROM VIEW_LastIssueOption Where Sku_Index=@Sku_Index AND Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index  ", Connection, Mytrans)

            Return dt
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetDataCustomer_Shipping_Location_SKU_Block(ByVal pSku_Index As String, ByVal pCustomer_Shipping_Location_Index As String, ByVal Connection As SqlClient.SqlConnection, ByVal Mytrans As SqlClient.SqlTransaction) As DataTable
        Try
            Dim dt As DataTable
            With SQLServerCommand.Parameters
                .Clear()
                .Add("@Sku_Index", SqlDbType.VarChar, 13).Value = pSku_Index
                .Add("@Customer_Shipping_Location_Index", SqlDbType.VarChar, 13).Value = pCustomer_Shipping_Location_Index
            End With

            dt = DBExeQuery(" SELECT * FROM ms_Customer_Shipping_Location_SKU_Block Where status_id <> -1 AND Sku_Index=@Sku_Index AND Customer_Shipping_Location_Index=@Customer_Shipping_Location_Index  ", Connection, Mytrans)
            Return dt

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    '--END V3


    Public Function FIFO_FULL_TAG_SEARCH_PICKING(ByVal Mode As enmMode_Pick, ByVal Picking_Qty As Double, ByVal FlagMix_Lot As Boolean, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand) As DataTable
        Dim strSQL As String = ""
        Try
            Dim objPicking As New config_CustomSetting
            Dim strAuto As String = objPicking.GetConfig_Picking("FIFO")
            Dim strWhere As String = objPicking.GetOther_Where("FIFO", _strCondition)
            strWhere &= _strCondition
            '--ADDNEW V3
            '>>a.เงื่อนไขคลังสินค้าหลัก และข้อกำหนด “จ่ายจากคลังหลักเท่านั้น”
            If strOrderForV3 <> "" Then
                strAuto = strAuto.Replace("{ISPRIMARYWH}", strOrderForV3)
            Else
                strAuto = strAuto.Replace("{ISPRIMARYWH},", strOrderForV3)
            End If
            '--END V3
            strWhere = strWhere & strAuto
            'Dong. ไม่สามารถ Group แล้วใช้ได้เลยเพราะถ้า Group แล้วจะไม่สามารถ Sort จาก FIFO ได้
            If FlagMix_Lot Then
                strSQL &= " DECLARE @tempbalTAG_No "
                strSQL &= " TABLE (     TAG_No varchar(50) ,Sku_Index varchar(50) ,Mfg_Date smalldatetime,Qty_Bal float ,RowNum  int) "
                strSQL &= " INSERT INTO @tempbalTAG_No(TAG_No,Sku_Index,Mfg_Date,Qty_Bal ,RowNum)"
            Else
                strSQL &= " DECLARE @tempbalTAG_No TABLE ("
                strSQL &= "     TAG_No varchar(50) ,DocumentType_Index varchar(50),Sku_Index varchar(50),LocationType_Index varchar(50),Qty_Bal float,Weight_Bal float,Volume_Bal float,RowNum  int)"
                strSQL &= " INSERT INTO @tempbalTAG_No(TAG_No,DocumentType_Index,Sku_Index,LocationType_Index,Qty_Bal,Weight_Bal,Volume_Bal,RowNum)"
            End If

            If USE_BLOCK_LOT() = True Then
                If FlagMix_Lot Then
                    strSQL &= " SELECT  TAG_No,Sku_Index,CONVERT(Varchar, Mfg_Date, 111) Mfg_Date,Qty_Bal,ROW_NUMBER() OVER(ORDER BY getdate() ASC) RowNum"
                Else
                    strSQL &= " SELECT  TAG_No,DocumentType_Index,Sku_Index,LocationType_Index,Qty_Bal,Weight_Bal,Volume_Bal,ROW_NUMBER() OVER(ORDER BY getdate() ASC)"
                End If

                Select Case _enmPicking_Type
                    Case enmPicking_Type.SERIAL
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                    Case Else
                        strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                End Select
                If strWhere <> "" Then
                    strSQL &= " WHERE ((select  dbo.fnBLOCKLOT(Consignee_Index,Sku_index,PLot,Mfg_Date)) <= 0 ) and  Qty_Bal > 0 " & strWhere
                End If
            Else
                If _DocumentType_Index = "" Then
                    If FlagMix_Lot Then
                        strSQL &= " SELECT  TAG_No,Sku_Index,CONVERT(Varchar, Mfg_Date, 111) Mfg_Date,Qty_Bal,ROW_NUMBER() OVER(ORDER BY getdate() ASC) RowNum"
                    Else
                        strSQL &= " SELECT  TAG_No,DocumentType_Index,Sku_Index,LocationType_Index,Qty_Bal,Weight_Bal,Volume_Bal,ROW_NUMBER() OVER(ORDER BY getdate() ASC)"
                    End If

                    Select Case _enmPicking_Type
                        Case enmPicking_Type.SERIAL
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation_Asset "
                        Case Else
                            strSQL &= " FROM  VIEW_WithdrawReserveLocation "
                    End Select
                    If strWhere <> "" Then
                        strSQL &= " WHERE Qty_Bal > 0 " & strWhere
                    End If
                Else
                    If FlagMix_Lot Then
                        strSQL &= " SELECT  TAG_No,Sku_Index,CONVERT(Varchar, Mfg_Date, 111) Mfg_Date,Qty_Bal,ROW_NUMBER() OVER(ORDER BY getdate() ASC) RowNum"
                    Else
                        strSQL &= " SELECT  TAG_No,DocumentType_Index,Sku_Index,LocationType_Index,Qty_Bal,Weight_Bal,Volume_Bal,ROW_NUMBER() OVER(ORDER BY getdate() ASC)"
                    End If

                    strSQL &= " FROM (	SELECT  VIEW_WithdrawReserveLocation.*,BLOCK = '' ,isnull(ms_DocumentType_ItemStatus.priority_index,99999) as priority_index "
                    strSQL &= " 		FROM  VIEW_WithdrawReserveLocation  inner join ms_DocumentType_ItemStatus  on ms_DocumentType_ItemStatus.DocumentType_Index='" & _DocumentType_Index & "'  AND ms_DocumentType_ItemStatus.ItemStatus_Index=VIEW_WithdrawReserveLocation.ItemStatus_Index   "
                    strSQL &= "  ) VIEW_WithdrawReserveLocation "
                    If strWhere <> "" Then
                        strSQL &= " WHERE Qty_Bal > 0 " & strWhere
                    End If
                End If
            End If

            If FlagMix_Lot Then
                strSQL &= "  DECLARE @tempPickTAG_No  "
                strSQL &= "  TABLE (     TAG_No varchar(50),Sku_Index varchar(50),Mfg_Date smalldatetime,Qty_Bal float ,RowNum  int)  "
                strSQL &= "  INSERT INTO @tempPickTAG_No(TAG_No,Sku_Index,Mfg_Date,Qty_Bal,RowNum)  "
                strSQL &= "  SELECT		TAG_No,Sku_Index,Mfg_Date,SUM(Qty_Bal) as Qty_Bal,min(RowNum) as SessionTime  "
                strSQL &= "  FROM		@tempbalTAG_No WHERE Qty_Bal > 0  "
                strSQL &= "  GROUP BY	TAG_No,Sku_Index,Mfg_Date "
                strSQL &= "  HAVING SUM(Qty_Bal) >= 0  "
                strSQL &= "  ORDER BY	min(RowNum)  "

                strSQL &= "  SELECT * FROM @tempPickTAG_No"

            Else
                strSQL &= " DECLARE @tempPickTAG_No TABLE ("
                strSQL &= "     TAG_No varchar(50) ,DocumentType_Index varchar(50),Sku_Index varchar(50),LocationType_Index varchar(50),Qty_Bal float,Weight_Bal float,Volume_Bal float,RowNum  int)"
                strSQL &= "  INSERT INTO @tempPickTAG_No(TAG_No,DocumentType_Index,Sku_Index,LocationType_Index,Qty_Bal,Weight_Bal,Volume_Bal,RowNum)"
                strSQL &= "  SELECT		TAG_No,MAX(DocumentType_Index) as DocumentType_Index,Sku_Index,MAX(LocationType_Index) as LocationType_Index"
                strSQL &= "             ,SUM(Qty_Bal) as Qty_Bal,SUM(Weight_Bal) as Weight_Bal,SUM(Volume_Bal) as Volume_Bal,min(RowNum) as SessionTime"
                strSQL &= "  FROM		@tempbalTAG_No WHERE Qty_Bal > 0"
                strSQL &= "  GROUP BY	TAG_No,Sku_Index"
                'Select Case Mode
                '    Case enmMode_Pick.EQUALS
                '        strSQL &= "  HAVING SUM(Qty_Bal) = " & Picking_Qty
                '    Case enmMode_Pick.OVER
                '        strSQL &= "  HAVING SUM(Qty_Bal) > " & Picking_Qty
                '    Case enmMode_Pick.LESS
                '        strSQL &= "  HAVING SUM(Qty_Bal) < " & Picking_Qty
                '    Case enmMode_Pick.OVER_EQUALS
                '        strSQL &= "  HAVING SUM(Qty_Bal) >= " & Picking_Qty
                '    Case enmMode_Pick.LESS_EQUALS
                '        strSQL &= "  HAVING SUM(Qty_Bal) <= " & Picking_Qty
                '    Case Else
                'End Select
                strSQL &= "  ORDER BY	min(RowNum)"
                strSQL &= "  SELECT * FROM @tempPickTAG_No"
            End If



            Dim dt As New DataTable
            dt = DBExeQuery(strSQL, Connection, myTrans)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
        End Try
    End Function


    Public Function SEARCH_SKU_RATIO(ByVal Sku_Index As String, ByVal Total_Qty As Double, ByVal Connection As SqlClient.SqlConnection, ByVal myTrans As SqlClient.SqlTransaction, ByVal SQLServerCommand As SqlClient.SqlCommand, Optional ByVal Showall As Boolean = False) As DataTable
        Dim strSQL As String = ""
        Try
            strSQL = " DECLARE @Qty numeric(19,6)"
            strSQL &= " SET     @Qty = " & Total_Qty
            strSQL &= " SELECT	S.Sku_Index,P.Package_Index,S.Sku_Id,P.Description UOM,R.Ratio"
            strSQL &= " 		,P.Weight,P.Dimension_Hi,P.Dimension_Wd,P.Dimension_Len, ((P.Dimension_Hi*P.Dimension_Wd*P.Dimension_Len*P.Dimension_Len) * DT.Ratio) as Volume "
            strSQL &= " 		, @Qty as QTY" 'จำนวน
            strSQL &= " 		, Floor(@Qty / R.Ratio) as xFloor" 'จำนวนกล่อง
            strSQL &= " 		, Floor(@Qty / R.Ratio) * @Qty  as xFloor_full" 'จำนวนในกล่อง
            strSQL &= " 		, Floor(cast(@Qty as numeric(19,6))  % cast(R.Ratio as numeric(19,6))) as xModulo" 'จำนวนเศษ
            strSQL &= " FROM	ms_SKU S"
            strSQL &= " 	    inner join ms_SKURatio R on S.Sku_Index = R.Sku_Index"
            strSQL &= " 	    inner join ms_Package P on P.Package_Index = R.Package_Index"
            strSQL &= " 	    left outer join ms_DimensionType DT on P.DimensionType_Index = DT.DimensionType_Index"
            strSQL &= " WHERE S.Sku_Index = '" & Sku_Index & "'"
            If Showall = False Then
                strSQL &= "         AND (R.Ratio <= @Qty) "
                strSQL &= "         AND (R.Ratio > 1)"
            End If

            strSQL &= " ORDER BY R.Ratio desc "

            Dim dt As New DataTable
            dt = DBExeQuery(strSQL, Connection, myTrans)
            Return dt
        Catch ex As Exception
            Throw ex
        Finally
            'disconnectDB()
        End Try
    End Function

End Class
