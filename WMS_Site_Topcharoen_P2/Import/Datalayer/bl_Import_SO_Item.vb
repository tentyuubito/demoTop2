Imports System.IO
Imports WMS_STD_Master
Imports WMS_STD_Master_Datalayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module

Imports System.Configuration.ConfigurationSettings

Public Class bl_Import_SO_Item : Inherits DBType_SQLServer

    Private _DEFAULT_IMPORT_PATH As String = ""
    Private _DataSource As New DataTable
    Private _dataTable As DataTable = New DataTable
    Private _FilePath As String = ""
    Private _SaleOrder_Index As String = ""
    Private _SalesOrderItemIndex As String = ""
    Private _Item_Seq As Integer
    Private _Sku_Index As String = ""
    Private _Sku_Id As String = ""
    Private _Qty As Double
    Private _Total_Qty As Double
    Private _Volume As Double
    Private _Str2 As String = ""
    Private _Remark As String = ""
    Private _Ref_No1 As String = ""
    Private _Package_Index As String = ""
    Private _Currency As String = ""
    Private _ProductType_Index As String = ""

    Private _Package_ID As String = ""
    Private _Itemstatus_index As String = ""
    Private _Plot As String = ""
#Region "    property   "

    Public Property Package_ID() As String
        Get
            Return _Package_ID
        End Get
        Set(ByVal value As String)
            _Package_ID = value
        End Set
    End Property

    Public Property Itemstatus_index() As String
        Get
            Return _Itemstatus_index
        End Get
        Set(ByVal value As String)
            _Itemstatus_index = value
        End Set
    End Property

    Public Property ProductTypeIndex() As String
        Get
            Return _ProductType_Index
        End Get
        Set(ByVal value As String)
            _ProductType_Index = value
        End Set
    End Property

    Public Property Currency() As String
        Get
            Return _Currency
        End Get
        Set(ByVal value As String)
            _Currency = value
        End Set
    End Property

    Public Property PackageIndex() As String
        Get
            Return _Package_Index
        End Get
        Set(ByVal value As String)
            _Package_Index = value
        End Set
    End Property

    Public Property DataSource() As DataTable
        Get
            Return _DataSource
        End Get
        Set(ByVal value As DataTable)
            _DataSource = value
        End Set
    End Property

    Public ReadOnly Property DataTable() As DataTable
        Get
            Return _dataTable
        End Get
    End Property

    Public Property SaleOrderIndex() As String
        Get
            Return _SaleOrder_Index
        End Get
        Set(ByVal value As String)
            _SaleOrder_Index = value
        End Set
    End Property

    Public Property SalesOrderItemIndex() As String
        Get
            Return _SalesOrderItemIndex
        End Get
        Set(ByVal value As String)
            _SalesOrderItemIndex = value
        End Set
    End Property

    Public Property ItemSeq() As Integer
        Get
            Return _Item_Seq
        End Get
        Set(ByVal value As Integer)
            _Item_Seq = value
        End Set
    End Property

    Public Property SkuIndex() As String
        Get
            Return _Sku_Index
        End Get
        Set(ByVal value As String)
            _Sku_Index = value
        End Set
    End Property

    Public Property SkuId() As String
        Get
            Return _Sku_Id
        End Get
        Set(ByVal value As String)
            _Sku_Id = value
        End Set
    End Property

    Public Property Qty() As Double
        Get
            Return _Qty
        End Get
        Set(ByVal value As Double)
            _Qty = value
        End Set
    End Property

    Public Property TotalQty() As Double
        Get
            Return _Total_Qty
        End Get
        Set(ByVal value As Double)
            _Total_Qty = value
        End Set
    End Property

    Public Property Volume() As Double
        Get
            Return _Volume
        End Get
        Set(ByVal value As Double)
            _Volume = value
        End Set
    End Property

    Public Property Str2() As String
        Get
            Return _Str2
        End Get
        Set(ByVal value As String)
            _Str2 = value
        End Set
    End Property

    Public Property Remark() As String
        Get
            Return _Remark
        End Get
        Set(ByVal value As String)
            _Remark = value
        End Set
    End Property

    Property FilePath() As String
        Get
            Return _FilePath
        End Get
        Set(ByVal value As String)
            _FilePath = value
        End Set
    End Property

    Public Property RefNo1() As String
        Get
            Return _Ref_No1
        End Get
        Set(ByVal value As String)
            _Ref_No1 = value
        End Set
    End Property
    Public Property Plot() As String
        Get
            Return _Plot
        End Get
        Set(ByVal value As String)
            _Plot = value
        End Set
    End Property

#End Region

    Sub SetDEFAULT_SHARP_IMPORT_PATH()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            'ดึงจาก database ตาราง config_CustomSetting
            objCustomSetting.GetConfig_Value("DEFAULT_IMPORT_PATH", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.FilePath = objDT.Rows(0).Item("Config_Value").ToString
            End If


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub

    Sub SetDEFAULT_PRODUCTTYPE_INDEX()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            'ดึงจาก database ตาราง config_CustomSetting
            objCustomSetting.GetConfig_Value("DEFAULT_PRODUCT_TYPE_INDEX", "")
            objDT = objCustomSetting.DataTable

            If objDT.Rows.Count > 0 Then
                Me.ProductTypeIndex = objDT.Rows(0).Item("Config_Value").ToString
            End If


            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub


    ''' <summary>
    ''' ค้นหา Sku_Index จาก Sku_Id
    ''' </summary>
    ''' <param name="pstrModel"></param>
    ''' <returns>
    ''' sku_index Or ""
    ''' </returns>
    ''' <remarks>
    ''' Date : 19/02/2010
    ''' Creater : Hun
    ''' </remarks>
    Public Function GetSKU_Index(ByVal pstrModel As String, ByVal pstrCustomerIndex As String) As String
        'ค้นหา sku_index จาก model
        Try

            Dim strSQL As String = ""
            Dim strReturn As String
            'Dim strSKU_Index As String = ""
            pstrModel = pstrModel.Replace("'", "''").ToString()
            strSQL &= " SELECT * " 'get sku_index
            strSQL &= " FROM ms_SKU "
            strSQL &= " WHERE SKU_Id = '" & pstrModel & "' and customer_index = '" & pstrCustomerIndex & "' and Status_id <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                '   strSKU_Index = _dataTable.Rows(0)("SKU_Index").ToString
                strReturn = _dataTable.Rows(0)("SKU_Index").ToString
                '   strReturn(1) = _dataTable.Rows(0)("Package_Index").ToString
            Else
                strReturn = ""
                ' strReturn(1) = ""
            End If
            Return strReturn
            'Return strSKU_Index
        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetPackage_Index(ByVal pstrSKU_Index As String, ByVal pstrPackage_ID As String) As String


        'ค้นหา sku_index จาก model
        Try

            Dim strSQL As String = ""
            Dim strReturn As String
            'Dim strSKU_Index As String = ""


            If pstrPackage_ID = "" Then
                strSQL = "SELECT * FROM ms_SKU WHERE SKU_Index = '" & pstrSKU_Index & "'"

            Else
                strSQL = " SELECT ms_Package.* " 'get sku_index
                strSQL &= " FROM ms_SKURatio INNER JOIN ms_Package ON  ms_SKURatio.Package_Index = ms_Package.Package_Index "

                strSQL &= " WHERE ms_Package.Package_ID = '" & pstrPackage_ID & "' "
                strSQL &= " AND ms_SKURatio.SKU_Index = '" & pstrSKU_Index & "' "

            End If




            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                '   strSKU_Index = _dataTable.Rows(0)("SKU_Index").ToString
                strReturn = _dataTable.Rows(0)("Package_Index").ToString
                '   strReturn(1) = _dataTable.Rows(0)("Package_Index").ToString
            Else
                strReturn = ""
                ' strReturn(1) = ""
            End If
            Return strReturn
            'Return strSKU_Index
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    Public Function GetPackage_Index_Import(ByVal pstrSKU_Index As String, ByVal pstrCustomer_Index As String) As String


        'ค้นหา sku_index จาก model
        Try

            Dim strSQL As String = ""
            Dim strReturn As String
            'Dim strSKU_Index As String = ""


            'If pstrPackage_ID = "" Then
            strSQL = "SELECT * FROM ms_SKU WHERE SKU_Index = '" & pstrSKU_Index & "' and Customer_Index = '" & pstrCustomer_Index & "'"

            'Else
            'strSQL = " SELECT ms_Package.* " 'get sku_index
            'strSQL &= " FROM ms_SKURatio INNER JOIN ms_Package ON  ms_SKURatio.Package_Index = ms_Package.Package_Index "

            'strSQL &= " WHERE ms_Package.Package_ID = '" & pstrPackage_ID & "' "
            'strSQL &= " AND ms_SKURatio.SKU_Index = '" & pstrSKU_Index & "' "

            'End If




            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                '   strSKU_Index = _dataTable.Rows(0)("SKU_Index").ToString
                strReturn = _dataTable.Rows(0)("Package_Index").ToString
                '   strReturn(1) = _dataTable.Rows(0)("Package_Index").ToString
            Else
                strReturn = ""
                ' strReturn(1) = ""
            End If
            Return strReturn
            'Return strSKU_Index
        Catch ex As Exception
            Throw ex
        End Try


    End Function

    ''' <summary>
    ''' ค้นหา SalesOrderIndex
    ''' </summary>
    ''' <param name="pstrSaleOrder_Id"></param>
    ''' <returns>SalesOrder_Index OR ""</returns>
    ''' <remarks>
    ''' Date : 19/02/2010
    ''' Creater : Hun
    ''' </remarks>
    Public Function GetSalesOrder_Index(ByVal pstrSaleOrder_Id As String) As String()

        Try
            Dim strReturn(2) As String
            ' Dim strRef As String = ""
            Dim strSQL = ""

            strSQL &= " SELECT * "
            strSQL &= " FROM    tb_SalesOrder   "
            strSQL &= " WHERE Str1 = '" & pstrSaleOrder_Id & "' and status <> -1"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                '    strRef = _dataTable.Rows(0)("Str1").ToString
                '   Me.SaleOrderIndex = _dataTable.Rows(0)("SalesOrder_Index").ToString
                strReturn(0) = _dataTable.Rows(0)("SalesOrder_Index").ToString
                strReturn(1) = _dataTable.Rows(0)("Currency_Index").ToString
            Else
                '  Me.SaleOrderIndex = ""
                strReturn(0) = ""
                strReturn(1) = ""
            End If

            Return strReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    Public Function GetSalesOrder_Index_TIMCO(ByVal pstrCustomerOrderNo As String, ByVal pstrCustomerIndex As String) As String()

        Try
            Dim strReturn(2) As String
            ' Dim strRef As String = ""
            Dim strSQL = ""

            strSQL = " select * "
            strSQL &= "from tb_Salesorder "
            strSQL &= " where status <> -1 and Ref_No2 = '" & pstrCustomerOrderNo & "' and Customer_Index = '" & pstrCustomerIndex & "' "

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                '    strRef = _dataTable.Rows(0)("Str1").ToString
                '   Me.SaleOrderIndex = _dataTable.Rows(0)("SalesOrder_Index").ToString
                strReturn(0) = _dataTable.Rows(0)("SalesOrder_Index").ToString
                strReturn(1) = _dataTable.Rows(0)("Currency_Index").ToString
            Else
                '  Me.SaleOrderIndex = ""
                strReturn(0) = ""
                strReturn(1) = ""
            End If

            Return strReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    ''' <summary>
    ''' ทำการ Insert ms_package,ms_Product,ms_Sku ,ms_skuRatio 
    ''' </summary>
    ''' <remarks>
    ''' Date : 19/02/2010
    ''' Creater : Hun
    ''' </remarks>
    Public Function InsertSKU() As String
        Dim strSQL As String = ""
        Dim strSku_Index As String = ""
        Dim strPackage_Index As String = ""
        Dim strSkuRatio_Index As String = ""
        Dim strProduct_Index As String = ""
        '  Dim strReturn As String

        SetDEFAULT_PRODUCTTYPE_INDEX()
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans

        Dim objSy_AutoNumber As New Sy_AutoNumber
        Try

            strPackage_Index = objSy_AutoNumber.getSys_Value("Package_Index")
            ' strReturn(0) = strPackage_Index
            strSQL = "   INSERT INTO ms_Package (                                                           "
            strSQL &= "                         Package_Index,Package_Id,Description,  "
            strSQL &= "                         Dimension_Hi,Dimension_Wd,Dimension_Len,Dimension_Unit_Id , "
            strSQL &= "                         add_by,add_date,add_branch,status_id ,                      "
            strSQL &= "                         Pkg_Description,isItem_Package,Weight                       "
            strSQL &= "                         )                                                           "
            strSQL &= "  VALUES (                                                                           "
            strSQL &= "             @Package_Index, @Package_Id,@Description ,                              "
            strSQL &= "             @Dimension_Hi,@Dimension_Wd,@Dimension_Len,@Dimension_Unit_Id ,         "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id ,                              "
            strSQL &= "             @Pkg_Description,@isItem_Package,@Weight                                "
            strSQL &= "          )                                                                          "


            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Package_ID
                .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Package_ID

                .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = 0
                .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = 0
                .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = 0
                .Parameters.Add("@Dimension_Unit_Id", SqlDbType.VarChar).Value = "0"

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                '.Parameters.Add("@add_date_Package", SqlDbType.SmallDateTime).Value = Now
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

                .Parameters.Add("@Pkg_Description", SqlDbType.NVarChar).Value = Package_ID
                .Parameters.Add("@isItem_Package", SqlDbType.Int).Value = 0
                .Parameters.Add("@Weight", SqlDbType.Float).Value = 0

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()


            'Insert ms_Product
            strProduct_Index = objSy_AutoNumber.getSys_Value("Product_Index")

            strSQL = "   INSERT INTO ms_Product (                                                           "

            strSQL &= "                         Product_Index,Product_Id,Product_Name_th,Product_Name_en ,  "
            strSQL &= "                         ProductType_Index,Std_Unit_Cost,Default_Exp_Day ,                             "
            strSQL &= "                         add_by,add_date,add_branch,status_id    "

            strSQL &= "                         )                                                           "


            strSQL &= "  VALUES (                                                                           "

            strSQL &= "             @Product_Index, @Product_Id,@Product_Name_th,@Product_Name_en ,         "
            strSQL &= "             @ProductType_Index,@Std_Unit_Cost,@Default_Exp_Day ,                                       "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id            "

            strSQL &= "          )                                                                          "



            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Product_Index", SqlDbType.VarChar).Value = strProduct_Index
                .Parameters.Add("@Product_Id", SqlDbType.NVarChar).Value = Me.SkuId
                .Parameters.Add("@Product_Name_th", SqlDbType.NVarChar).Value = Me.SkuId
                .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Me.SkuId
                .Parameters.Add("@ProductType_Index", SqlDbType.VarChar).Value = Me.ProductTypeIndex
                .Parameters.Add("@Std_Unit_Cost", SqlDbType.Float).Value = 0
                .Parameters.Add("@Default_Exp_Day", SqlDbType.Float).Value = 0

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                '.Parameters.Add("@add_date", SqlDbType.SmallDateTime).Value = Now
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            'Insert ms_SKU
            strSku_Index = objSy_AutoNumber.getSys_Value("Sku_Index")

            ' strReturn = strSku_Index

            strSQL = "  INSERT INTO ms_SKU (Sku_Index,Sku_Id,Product_Index,Size_Index,Package_Index,UnitWeight_Index, "
            strSQL &= " Color_Index,Model_Index,Price1,Price2,Price3,ItemLife_y,ItemLife_m,ItemLife_d, "
            strSQL &= " Str1,Str2,Str10,Min_Qty,Min_Weight,Min_Volume,Max_Qty,Max_Weight,Max_Volume,Qty_Per_Pallet, "
            strSQL &= " add_by,add_date,add_branch,isPLot,status_id,PalletType_Index,Pick_Method,Picking_Type   "
            strSQL &= " )    "
            strSQL &= " VALUES (                                                                            "
            strSQL &= "         @Sku_Index, @Sku_Id  ,@Product_Index                                                      "
            strSQL &= "         ,@Size_Index,@Package_Index,@UnitWeight_Index, @Color_Index,@Model_Index   "
            strSQL &= "         ,@Price1,@Price2,@Price3                                                  "
            strSQL &= "         ,@ItemLife_y ,@ItemLife_m ,@ItemLife_d                                      "
            strSQL &= "         ,@Str1  ,@Str2  ,@Str10                                                     "
            strSQL &= "         ,@Min_Qty ,@Min_Weight ,@Min_Volume                                         "
            strSQL &= "         ,@Max_Qty ,@Max_Weight ,@Max_Volume                                         "
            strSQL &= "         ,@Qty_Per_Pallet                                                            "
            strSQL &= "         ,@add_by,getdate(),@add_branch                                              "
            strSQL &= "         ,@isPLot                                                                    "
            strSQL &= "         ,@status_id                                                                 "
            strSQL &= "         ,@PalletType_Index                                                          "
            strSQL &= "         ,@Pick_Method                                                               "
            strSQL &= "         ,@Picking_Type                                                            "
            strSQL &= "         ) "

            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                .Parameters.Add("@Sku_Id", SqlDbType.NVarChar).Value = Me.SkuId
                .Parameters.Add("@Product_Index", SqlDbType.NVarChar).Value = strProduct_Index
                .Parameters.Add("@Size_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@UnitWeight_Index", SqlDbType.VarChar).Value = "0"
                .Parameters.Add("@Color_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Model_Index", SqlDbType.VarChar).Value = "-1"
                .Parameters.Add("@Price1", SqlDbType.Float).Value = 0
                .Parameters.Add("@Price2", SqlDbType.Float).Value = 0
                .Parameters.Add("@Price3", SqlDbType.Float).Value = 0
                .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = Me.SkuId
                .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Me.SkuId
                .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = "1"
                .Parameters.Add("@Min_Qty", SqlDbType.Float).Value = 0
                .Parameters.Add("@Min_Weight", SqlDbType.Float).Value = 0
                .Parameters.Add("@Min_Volume", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = 0
                .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = 0
                .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = 0
                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                '.Parameters.Add("@add_date", SqlDbType.SmallDateTime).Value = Now
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@isPLot", SqlDbType.Bit).Value = 0
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                .Parameters.Add("@PalletType_Index", SqlDbType.VarChar).Value = "1"
                .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = 1
                .Parameters.Add("@Picking_Type", SqlDbType.Int).Value = 0
            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()



            strSkuRatio_Index = objSy_AutoNumber.getSys_Value("SkuRatio_Index")
            objSy_AutoNumber = Nothing

            strSQL = "   INSERT INTO ms_SkuRatio (                                                          "
            strSQL &= "                         SkuRatio_Index,Sku_Index,Package_Index,Ratio,               "
            strSQL &= "                         add_by,add_date,add_branch,status_id                        "
            strSQL &= "                         )                                                           "
            strSQL &= "  VALUES (                                                                           "
            strSQL &= "             @SkuRatio_Index, @Sku_Index,@Package_Index,@Ratio,                      "
            strSQL &= "             @add_by,getdate(),@add_branch,@status_id                                "
            strSQL &= "          )                                                                          "

            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@SkuRatio_Index", SqlDbType.VarChar).Value = strSkuRatio_Index
                .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                .Parameters.Add("@Ratio", SqlDbType.Float).Value = 1

                .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                '.Parameters.Add("@add_date", SqlDbType.SmallDateTime).Value = Now
                .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                .Parameters.Add("@status_id", SqlDbType.Int).Value = 0


            End With

            SetSQLString = strSQL
            SetCommandType = DBType_SQLServer.enuCommandType.Text
            SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
            EXEC_Command()

            myTrans.Commit()
            Return strSku_Index
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally

            disconnectDB()
        End Try
    End Function

    ''' <summary>
    ''' Insert tb_SalesOrderItem
    ''' </summary>
    ''' <remarks>
    ''' Date : 19/02/2010
    ''' Creater : Hun
    ''' </remarks>
    Public Sub SalesOrder_ItemInsert()

        Try


            Dim otb_SalesOrder_Item As New bl_SalesOrderItem
            With otb_SalesOrder_Item

                'SalesOrderItem_Index 
                .SalesOrderItem_Index = Me.SalesOrderItemIndex
                'Invoice_No 
                .SalesOrder_Index = Me.SaleOrderIndex

                'Line_No
                .Item_Seq = Me.ItemSeq
                .Itemstatus_index = Me.Itemstatus_index

                'Model
                .Sku_Index = Me.SkuIndex
                '.itemst

                'Class
                'MsgBox(" Class " & Mid(Current_Text_Line, iIndex, 1) & " Len : " & Mid(Current_Text_Line, iIndex, 1).Length)

                'Package
                .Package_Index = Me.PackageIndex

                'Ratio
                .Ratio = 0

                'Weight
                .Weight = 0

                'Serial_No
                .Serial_No = ""

                'Total_Withdraw_Qty
                '.Total_Withdraw_Qty = 0

                'Withdraw_Qty
                .Qty_Withdraw = 0

                'Withdraw_Weight
                .Weight_Withdraw = 0

                'Withdraw_Volume
                .Volume_Withdraw = 0

                .Currency_Index = Me.Currency
                .Discount_Amt = 0
                .Status = 1
                .Flo1 = 0
                .Flo2 = 0
                .Flo3 = 0
                .Flo4 = 0
                .Flo5 = 0
                .add_by = WV_UserName
                .add_branch = WV_Branch_ID
                '.update_date = ""
                '.cancel_date = ""
                'Qty
                .Qty = Me.Qty
                'Total Qty
                .Total_Qty = Me.TotalQty

                .UnitPrice = 1
                .Amount = .UnitPrice * .Qty
                .Total_Amt = .Amount
                'Volume
                .Volume = Me.Volume

                'Storage Location
                .Str2 = Me.Str2
                .Remark = Me.Remark
                .Plot = Me.Plot

                Dim Amount As Double = Amount + .Amount
                Dim TotalAmt As Double = TotalAmt + .Total_Amt
                Dim NetAmt As Double = NetAmt + .Total_Amt

                If .SalesOrder_Index <> "" Then
                    .Insert_Import()
                    UpdateSaleOrder(Me.SaleOrderIndex, Amount, TotalAmt, NetAmt, Me.Str2)
                End If

            End With

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Public Sub UpdateSaleOrder(ByVal pstrSalesOrder_Index As String, ByVal dblAmount As Double, ByVal dblTotalAmt As Double, ByVal dblNetAmt As Double, ByVal pstrGrade As String)
        Try
            Dim strSQL As String = ""
            strSQL &= "  UPDATE tb_SalesOrder "
            strSQL &= "  SET Amount = @Amount   "
            strSQL &= "  ,Total_Amt = @Total_Amt "
            strSQL &= "  ,Net_Amt = @Net_Amt "
            'strSQL &= "  ,Remark = @Remark "
            strSQL &= "  WHERE SalesOrder_Index = @SalesOrder_Index   "


            With SQLServerCommand

                .Parameters.Clear()

                .Parameters.Add("@Amount", SqlDbType.Float).Value = dblAmount
                .Parameters.Add("@Total_Amt", SqlDbType.Float).Value = dblTotalAmt
                .Parameters.Add("@Net_Amt", SqlDbType.Float).Value = dblNetAmt
                '.Parameters.Add("@Remark", SqlDbType.VarChar).Value = pstrGrade
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = pstrSalesOrder_Index

            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub


    '26/08/2010 krit update
    Public Sub UpdateSalesOrder_Cancel_From_Error_Interface(ByVal pstrSalesOrder_Index As String)
        Try
            Dim strSQL As String = ""
            strSQL &= "  UPDATE tb_SalesOrder "
            strSQL &= "  SET Status = -1   "
            strSQL &= "  ,Remark = 'ยกเลิก จาก Interface ไม่สำเร็จ'"
            strSQL &= "  WHERE SalesOrder_Index = @SalesOrder_Index   "


            With SQLServerCommand
                .Parameters.Clear()
                .Parameters.Add("@SalesOrder_Index", SqlDbType.VarChar).Value = pstrSalesOrder_Index
            End With

            SetSQLString = strSQL
            SetCommandType = enuCommandType.Text
            SetEXEC_TYPE = EXEC.NonQuery
            connectDB()
            EXEC_Command()
        Catch ex As Exception
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Sub

    Public Function CheckDup_SO_Item(ByVal pstrSO_Index As String, ByVal pstrSKU_ID As String, ByVal pstrItem_No As String) As Boolean
        Try

            Dim strSQL As String = ""
            strSQL = "SELECT * "
            strSQL &= " FROM tb_SalesOrderItem SOI INNER JOIN ms_SKU SKU ON SOI.SKU_Index = SKU.SKU_Index "
            strSQL &= " WHERE SOI.Status <> -1 AND SalesOrder_Index = '" & pstrSO_Index & "'"
            strSQL &= " AND SKU.SKU_ID = '" & pstrSKU_ID & "'"
            strSQL &= " AND Item_Seq = '" & pstrItem_No & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Public Function GetLast_ItemSeq(ByVal pstrSO_Index As String) As Integer
        Try

            Dim strSQL As String = ""
            strSQL = "SELECT max(Item_seq) As Max_Item_seq "
            strSQL &= " FROM tb_SalesOrderItem SOI INNER JOIN ms_SKU SKU ON SOI.SKU_Index = SKU.SKU_Index "
            strSQL &= " WHERE SOI.Status <> -1 AND SalesOrder_Index = '" & pstrSO_Index & "'"

            SetSQLString = strSQL
            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable

            If _dataTable.Rows.Count > 0 Then
                If (ValidateValueType.IsNullOrEmptyOrDBNullOrNothingGetData(_dataTable.Rows(0)("Max_Item_seq"), GetType(String)).ToString = "") Then
                    Return 0
                Else
                    Return _dataTable.Rows(0)("Max_Item_seq")
                End If
            Else
                Return 0
            End If

        Catch ex As Exception
            Throw ex
        End Try
    End Function

End Class
