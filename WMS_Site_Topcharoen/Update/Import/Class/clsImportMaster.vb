Imports WMS_STD_Formula
Imports WMS_STD_Formula.W_Module
Imports WMS_STD_Master_Datalayer
Public Class clsImportMaster : Inherits DBType_SQLServer
#Region "OptionSKU"
    Private _DimenstionRatio As Double = 1
    Private _DimenstionRatioPakcage As Double = 1
    Private _Digit As Integer = 4
    Private _DigitPackage As Integer = 4
    Private _Sku_Index As String
#End Region
    Public Function getConfig_Import_Text(ByVal strProcessID As String) As DataTable
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT      * " & _
             " FROM Config_Import_Text " & _
             " WHERE    isuse = 1   And Process_ID=" & strProcessID.ToString
            SetSQLString = strSQL

            connectDB()
            EXEC_DataAdapter()
            _dataTable = GetDataTable
            Return _dataTable
        Catch ex As Exception
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function


    Public Function ImportMasterSKU(ByVal Dr As DataRow) As String
        Dim StrSQL As String = ""
        Dim strSku_Index As String = ""
        Dim strPackage_Index As String = ""
        Dim strSkuRatio_Index As String = ""
        Dim strProduct_Index As String = ""
        Dim strProductType_Index As String = ""
        Dim objCustomer_Setting As New config_CustomSetting


        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Dim objSy_AutoNumber As New Sy_AutoNumber
        Try



            StrSQL = ""
            strSku_Index = ""
            strPackage_Index = ""
            strSkuRatio_Index = ""
            strProduct_Index = ""
            strProductType_Index = ""
            strProduct_Index = ""

            Dim dtCheck As New DataTable
            StrSQL = "Select * from ms_Sku Where Sku_Id = '" & Dr("Sku_Id") & "' AND Status_Id <> -1"
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = StrSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(dtCheck)
            If dtCheck.Rows.Count > 0 Then

                If Dr("ProductType_ID").ToString <> "" Then
                    StrSQL = "  SELECT  * "
                    StrSQL &= "  FROM   ms_ProductType  "
                    StrSQL &= "   WHERE  ProductType_Id = '" & Dr("ProductType_ID") & "'  AND Status_Id <> -1 "
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = StrSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    DataAdapter.Fill(DS, "PD")
                    If DS.Tables("PD").Rows.Count <= 0 Then 'AutoInsertProductType
                        StrSQL = "  INSERT INTO ms_ProductType(                                                             "
                        StrSQL &= "                              ProductType_Index, ProductType_ID, Description, add_by     "
                        StrSQL &= "	                            ,add_date,add_branch,status_id,ItemLife_y,ItemLife_m        "
                        StrSQL &= "		                        ,ItemLife_d                                                 "
                        StrSQL &= "	                           )   VALUES                                                   "
                        StrSQL &= "                         (   @ProductType_Index,@ProductType_Id,@Description             "
                        StrSQL &= "                             ,@add_by,getdate(),@add_branch,@status_id                   "
                        StrSQL &= "                             ,@ItemLife_y,@ItemLife_m,@ItemLife_d                        "
                        StrSQL &= "                         )                                                               "

                        strProductType_Index = objSy_AutoNumber.getSys_Value("ProductType_Index")
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = strProductType_Index
                            .Parameters.Add("@ProductType_Id", SqlDbType.VarChar, 50).Value = Dr("ProductType_ID").ToString
                            If Dr("ProductType_Name").ToString = "" Then
                                .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = Dr("ProductType_ID").ToString
                            Else
                                .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = Dr("ProductType_Name").ToString
                            End If
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                            .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                            .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                            .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                            .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                        End With
                        SetSQLString = StrSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Else
                        strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
                    End If
                Else
                    strProductType_Index = ""
                End If

                If strProductType_Index <> "" Then
                    StrSQL = " update ms_product "
                    StrSQL &= " Set ProductType_Index = @ProductType_Index "
                    StrSQL &= " Where Product_Index = @Product_Index "
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Product_Index", SqlDbType.NVarChar).Value = dtCheck.Rows(0)("Product_Index").ToString
                        .Parameters.Add("@ProductType_Index", SqlDbType.VarChar).Value = strProductType_Index
                    End With
                    SetSQLString = StrSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                End If

                Dim objDiman As New ms_DimensionType
                Dim dtDimensionTypeData As New DataTable
                Dim DimensionType_Index As String = ""
                If Dr("Dimension_Unit").ToString <> "" Then
                    dtDimensionTypeData = objDiman.GetDimensionTypeData(" AND DimensionType_Id = '" & Dr("Dimension_Unit").ToString & "'")
                    DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                Else
                    dtDimensionTypeData = objDiman.GetDimensionTypeData("")
                    DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                End If

                StrSQL = " Update ms_Package Set "
                StrSQL &= " Package_Id = @Package_Id "
                StrSQL &= " ,Description = @Description "
                StrSQL &= " ,Dimension_Hi = @Dimension_Hi "
                StrSQL &= " ,Dimension_Wd = @Dimension_Wd "
                StrSQL &= " ,Dimension_Len = @Dimension_Len "
                StrSQL &= " ,DimensionType_Index = @DimensionType_Index "
                StrSQL &= " where Package_Index = @Package_Index "
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = dtCheck.Rows(0)("Package_Index").ToString
                    .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Dr("Package").ToString
                    .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Dr("Package").ToString
                    .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = Dr("Dimension_Hi")
                    .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = Dr("Dimension_Wd")
                    .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = Dr("Dimension_Len")
                    .Parameters.Add("@DimensionType_Index", SqlDbType.VarChar, 13).Value = DimensionType_Index
                End With
                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()




                StrSQL = " Update ms_SKU Set "
                StrSQL &= " Sku_Id = @Sku_Id"
                StrSQL &= " ,UnitWeight_Index = @UnitWeight_Index"
                StrSQL &= " ,Unit_Volume = @Unit_Volume"
                StrSQL &= " ,Barcode1 = @Barcode1"
                StrSQL &= " ,Barcode2 = @Barcode2"
                'StrSQL &= " ,ItemLife_y = @ItemLife_y"
                'StrSQL &= " ,ItemLife_m = @ItemLife_m"
                StrSQL &= " ,ItemLife_d = @ItemLife_d"
                StrSQL &= " ,Str1 = @Str1"
                StrSQL &= " ,Str2 = @Str2"
                StrSQL &= " ,update_by = @update_by"
                StrSQL &= " ,update_date = getdate()"
                StrSQL &= " ,Unit_Width = @Unit_Width"
                StrSQL &= " ,Unit_Length = @Unit_Length"
                StrSQL &= " ,Unit_Height = @Unit_Height"
                StrSQL &= " ,isPLot = @isPLot "
                StrSQL &= " WHERE Sku_Index = @Sku_Index"
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = dtCheck.Rows(0)("Sku_Index").ToString
                    .Parameters.Add("@Sku_Id", SqlDbType.NVarChar).Value = Dr("Sku_Id").ToString
                    .Parameters.Add("@UnitWeight_Index", SqlDbType.Float).Value = Dr("Weight")
                    .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = Dr("Volume")
                    Dim Barcode2 As String = ""
                    If Dr("BarCode") = "" Then
                        .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = dtCheck.Rows(0)("Barcode1").ToString
                        Barcode2 = dtCheck.Rows(0)("Barcode1").ToString
                    Else
                        .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Dr("BarCode").ToString
                        Barcode2 = Dr("BarCode").ToString
                    End If
                    .Parameters.Add("@Barcode2", SqlDbType.VarChar).Value = Barcode2
                    '.Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                    '.Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                    .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = Dr("ItemLife_d")
                    .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = Dr("Sku_Name_TH").ToString
                    If Dr("Sku_Name_EN") = "" Then
                        .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = dtCheck.Rows(0)("Str2").ToString
                    Else
                        .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Dr("Sku_Name_EN").ToString
                    End If
                    .Parameters.Add("@update_by", SqlDbType.VarChar).Value = WV_UserName
                    .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = Dr("Dimension_Wd")
                    .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = Dr("Dimension_Len")
                    .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = Dr("Dimension_Hi")
                    .Parameters.Add("@isPLot", SqlDbType.Bit).Value = Dr("isPlot")
                End With
                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

            Else
                If Dr("ProductType_ID").ToString <> "" Then
                    StrSQL = "  SELECT  * "
                    StrSQL &= "  FROM   ms_ProductType  "
                    StrSQL &= "   WHERE  ProductType_Id = '" & Dr("ProductType_ID") & "'  AND Status_Id <> -1 "
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = StrSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    DataAdapter.Fill(DS, "PD")
                    If DS.Tables("PD").Rows.Count <= 0 Then 'AutoInsertProductType
                        StrSQL = "  INSERT INTO ms_ProductType(                                                             "
                        StrSQL &= "                              ProductType_Index, ProductType_ID, Description, add_by     "
                        StrSQL &= "	                            ,add_date,add_branch,status_id,ItemLife_y,ItemLife_m        "
                        StrSQL &= "		                        ,ItemLife_d                                                 "
                        StrSQL &= "	                           )   VALUES                                                   "
                        StrSQL &= "                         (   @ProductType_Index,@ProductType_Id,@Description             "
                        StrSQL &= "                             ,@add_by,getdate(),@add_branch,@status_id                   "
                        StrSQL &= "                             ,@ItemLife_y,@ItemLife_m,@ItemLife_d                        "
                        StrSQL &= "                         )                                                               "

                        strProductType_Index = objSy_AutoNumber.getSys_Value("ProductType_Index")
                        With SQLServerCommand
                            .Parameters.Clear()
                            .Parameters.Add("@ProductType_Index", SqlDbType.VarChar, 13).Value = strProductType_Index
                            .Parameters.Add("@ProductType_Id", SqlDbType.VarChar, 50).Value = Dr("ProductType_ID").ToString
                            If Dr("ProductType_Name").ToString = "" Then
                                .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = Dr("ProductType_ID").ToString
                            Else
                                .Parameters.Add("@Description", SqlDbType.NVarChar, 200).Value = Dr("ProductType_Name").ToString
                            End If
                            .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                            .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                            .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                            .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                            .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                            .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                        End With
                        SetSQLString = StrSQL
                        SetCommandType = DBType_SQLServer.enuCommandType.Text
                        SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                        EXEC_Command()
                    Else
                        strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
                    End If
                Else
                    StrSQL = "  SELECT  top 1  * "
                    StrSQL &= "  FROM   ms_ProductType Where Status_Id <> -1 "
                    With SQLServerCommand
                        .Connection = Connection
                        .Transaction = myTrans
                        .CommandText = StrSQL
                        .CommandTimeout = 0
                    End With
                    DataAdapter.SelectCommand = SQLServerCommand
                    DataAdapter.SelectCommand.Transaction = myTrans
                    DataAdapter.Fill(DS, "PD")
                    strProductType_Index = DS.Tables("PD").Rows(0)("ProductType_Index").ToString
                End If

                Dim objDiman As New ms_DimensionType
                Dim dtDimensionTypeData As New DataTable
                Dim DimensionType_Index As String = ""
                Dim Volume As Double = 0
                If Dr("Dimension_Unit").ToString <> "" Then
                    dtDimensionTypeData = objDiman.GetDimensionTypeData(" AND DimensionType_Id = '" & Dr("Dimension_Unit").ToString & "'")
                    If dtDimensionTypeData.Rows.Count > 0 Then
                        DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                    Else
                        dtDimensionTypeData = objDiman.GetDimensionTypeData("")
                        DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                    End If
                Else
                    dtDimensionTypeData = objDiman.GetDimensionTypeData("")
                    DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                End If

                _DimenstionRatio = getRatioM3(DimensionType_Index)
                _Digit = getDigitM3(DimensionType_Index)
                Dim douDimension_Wd As Double = 0
                If Dr("Dimension_Wd").ToString.Length > 0 Then
                    douDimension_Wd = Convert.ToInt64(Dr("Dimension_Wd"))
                End If
                Dim douDimension_Len As Double = 0
                If Dr("Dimension_Len").ToString.Length > 0 Then
                    douDimension_Len = Convert.ToInt64(Dr("Dimension_Len"))
                End If
                Dim douDimension_Hi As Double = 0
                If Dr("Dimension_Hi").ToString.Length > 0 Then
                    douDimension_Hi = Convert.ToInt64(Dr("Dimension_Hi"))
                End If

                Volume = Me.CalculateToM3(_DimenstionRatio, douDimension_Wd, douDimension_Len, douDimension_Hi)

                strPackage_Index = objSy_AutoNumber.getSys_Value("Package_Index")
                StrSQL = "   INSERT INTO ms_Package (                                                           "
                StrSQL &= "                         Package_Index,Package_Id,Description,                       "
                StrSQL &= "                         Dimension_Hi,Dimension_Wd,Dimension_Len,Dimension_Unit_Id , "
                StrSQL &= "                         add_by,add_date,add_branch,status_id ,                      "
                StrSQL &= "                         Pkg_Description,isItem_Package,Weight,DimensionType_Index   "
                StrSQL &= "                         )                                                           "
                StrSQL &= "  VALUES (                                                                           "
                StrSQL &= "             @Package_Index, @Package_Id,@Description ,                              "
                StrSQL &= "             @Dimension_Hi,@Dimension_Wd,@Dimension_Len,@Dimension_Unit_Id ,         "
                StrSQL &= "             @add_by,getdate(),@add_branch,@status_id ,                              "
                StrSQL &= "             @Pkg_Description,@isItem_Package,@Weight,@DimensionType_Index           "
                StrSQL &= "          )                                                                          "

                With SQLServerCommand

                    .Parameters.Clear()

                    .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                    .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Dr("Package").ToString
                    .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Dr("Package").ToString

                    .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = douDimension_Hi
                    .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = douDimension_Wd
                    .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = douDimension_Len

                    .Parameters.Add("@Dimension_Unit_Id", SqlDbType.VarChar).Value = "0"

                    .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName

                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@status_id", SqlDbType.Int).Value = 0

                    .Parameters.Add("@Pkg_Description", SqlDbType.NVarChar).Value = ""
                    .Parameters.Add("@isItem_Package", SqlDbType.Int).Value = 0
                    If IsDBNull(Dr("Weight")) = True Then
                        .Parameters.Add("@Weight", SqlDbType.Float).Value = Dr("Weight")
                    Else
                        If Dr("Weight").ToString.Length > 0 Then
                            .Parameters.Add("@Weight", SqlDbType.Float).Value = Dr("Weight")
                        Else
                            .Parameters.Add("@Weight", SqlDbType.Float).Value = 0
                        End If

                    End If

                    .Parameters.Add("@DimensionType_Index", SqlDbType.VarChar, 13).Value = DimensionType_Index
                End With

                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                'Insert ms_Product
                strProduct_Index = objSy_AutoNumber.getSys_Value("Product_Index")

                StrSQL = "   INSERT INTO ms_Product (                                                           "
                StrSQL &= "                         Product_Index,Product_Id,Product_Name_th,Product_Name_en ,  "
                StrSQL &= "                         ProductType_Index,Std_Unit_Cost,Default_Exp_Day ,                             "
                StrSQL &= "                         add_by,add_date,add_branch,status_id    "
                StrSQL &= "                         )                                                           "
                StrSQL &= "  VALUES (                                                                           "
                StrSQL &= "             @Product_Index, @Product_Id,@Product_Name_th,@Product_Name_en ,         "
                StrSQL &= "             @ProductType_Index,@Std_Unit_Cost,@Default_Exp_Day ,                                       "
                StrSQL &= "             @add_by,getdate(),@add_branch,@status_id            "
                StrSQL &= "          )                                                                          "

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Product_Index", SqlDbType.VarChar).Value = strProduct_Index
                    .Parameters.Add("@Product_Id", SqlDbType.NVarChar).Value = Dr("Sku_Id")
                    .Parameters.Add("@Product_Name_th", SqlDbType.NVarChar).Value = Dr("ProductType_Name")
                    If IsDBNull(Dr("ProductType_Name")) = True Then
                        .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Dr("Sku_Name_TH")
                    Else
                        If Dr("ProductType_Name") = "" Then
                            .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Dr("Sku_Name_TH")
                        Else
                            .Parameters.Add("@Product_Name_en", SqlDbType.NVarChar).Value = Dr("ProductType_Name")
                        End If
                    End If
                    .Parameters.Add("@ProductType_Index", SqlDbType.VarChar).Value = strProductType_Index
                    .Parameters.Add("@Std_Unit_Cost", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Default_Exp_Day", SqlDbType.Float).Value = 0
                    .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                End With

                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()


                '----------------------------------------------------------------------------------------------------------------------------------
                'Insert ms_SKU
                strSku_Index = objSy_AutoNumber.getSys_Value("Sku_Index")
                _Sku_Index = strSku_Index


                StrSQL = "  INSERT INTO ms_SKU (Sku_Index,Sku_Id,Product_Index,Size_Index,Package_Index,UnitWeight_Index,Unit_Volume, "
                StrSQL &= " Color_Index,Model_Index,Brand_Index,Barcode1,Barcode2,Price1,Price2,Price3,ItemLife_y,ItemLife_m,ItemLife_d, "
                StrSQL &= " Str1,Str2,Str10,Min_Qty,Min_Weight,Min_Volume,Max_Qty,Max_Weight,Max_Volume,Qty_Per_Pallet, "
                StrSQL &= " add_by,add_date,add_branch,isPLot,status_id,PalletType_Index,Pick_Method,Picking_Type,Unit_Width,Unit_Length,Unit_Height,Flo1   "
                StrSQL &= " ,RFIDCode,Location_Index)    "
                StrSQL &= " VALUES (                                                                            "
                StrSQL &= "         @Sku_Index, @Sku_Id  ,@Product_Index                                                      "
                StrSQL &= "         ,@Size_Index,@Package_Index,@UnitWeight_Index, @Unit_Volume,@Color_Index,@Model_Index ,@Brand_Index ,@Barcode1,@Barcode2 "
                StrSQL &= "         ,@Price1,@Price2,@Price3                                                  "
                StrSQL &= "         ,@ItemLife_y ,@ItemLife_m ,@ItemLife_d                                      "
                StrSQL &= "         ,@Str1  ,@Str2  ,@Str10                                                     "
                StrSQL &= "         ,@Min_Qty ,@Min_Weight ,@Min_Volume                                         "
                StrSQL &= "         ,@Max_Qty ,@Max_Weight ,@Max_Volume                                         "
                StrSQL &= "         ,@Qty_Per_Pallet                                                            "
                StrSQL &= "         ,@add_by,getdate(),@add_branch                                              "
                StrSQL &= "         ,@isPLot                                                                    "
                StrSQL &= "         ,@status_id                                                                 "
                StrSQL &= "         ,@PalletType_Index                                                          "
                StrSQL &= "         ,@Pick_Method                                                               "
                StrSQL &= "         ,@Picking_Type                                                            "
                StrSQL &= "         ,@Unit_Width,@Unit_Length,@Unit_Height ,@Flo1,@RFIDCode,@Location_Index ) "

                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                    .Parameters.Add("@Sku_Id", SqlDbType.NVarChar).Value = Dr("Sku_Id").ToString
                    .Parameters.Add("@Product_Index", SqlDbType.NVarChar).Value = strProduct_Index
                    .Parameters.Add("@Size_Index", SqlDbType.VarChar).Value = "-1"
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                    If IsDBNull(Dr("Weight")) Then
                        .Parameters.Add("@UnitWeight_Index", SqlDbType.Float).Value = 0
                    Else
                        .Parameters.Add("@UnitWeight_Index", SqlDbType.Float).Value = Dr("Weight")
                    End If
                    If IsDBNull(Dr("Volume")) = True Then
                        .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = 0
                    Else
                        .Parameters.Add("@Unit_Volume", SqlDbType.Float).Value = Dr("Volume")
                    End If
                    .Parameters.Add("@Color_Index", SqlDbType.VarChar).Value = "-1"
                    .Parameters.Add("@Model_Index", SqlDbType.VarChar).Value = "-1"
                    .Parameters.Add("@Brand_Index", SqlDbType.VarChar).Value = "-1"
                    Dim Barcode2 As String = ""
                    If Dr("BarCode") = "" Then
                        .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Dr("Sku_Id").ToString
                        Barcode2 = Dr("Sku_Id").ToString
                    Else
                        .Parameters.Add("@Barcode1", SqlDbType.VarChar).Value = Dr("BarCode")
                        Barcode2 = Dr("BarCode").ToString
                    End If
                    .Parameters.Add("@Barcode2", SqlDbType.VarChar).Value = Barcode2
                    .Parameters.Add("@Price1", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Price2", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Price3", SqlDbType.Float).Value = 0
                    .Parameters.Add("@ItemLife_y", SqlDbType.Int).Value = 0
                    .Parameters.Add("@ItemLife_m", SqlDbType.Int).Value = 0
                    If IsDBNull(Dr("ItemLife_d")) = True Then
                        .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = 0
                    Else
                        .Parameters.Add("@ItemLife_d", SqlDbType.Int).Value = Dr("ItemLife_d")
                    End If

                    .Parameters.Add("@Str1", SqlDbType.NVarChar).Value = Dr("Sku_Name_TH")
                    If IsDBNull(Dr("Sku_Name_EN")) = True Then
                        .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Dr("Sku_Name_TH")
                    Else
                        If Dr("Sku_Name_EN") = "" Then
                            .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Dr("Sku_Name_TH")
                        Else
                            .Parameters.Add("@Str2", SqlDbType.NVarChar).Value = Dr("Sku_Name_EN")
                        End If
                    End If
                    .Parameters.Add("@Str10", SqlDbType.NVarChar).Value = "1"
                    .Parameters.Add("@Min_Qty", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Min_Weight", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Min_Volume", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Max_Qty", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Max_Weight", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Max_Volume", SqlDbType.Float).Value = 0
                    .Parameters.Add("@Qty_Per_Pallet", SqlDbType.Float).Value = 1
                    .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    If IsDBNull(Dr("isPlot")) = True Then
                        .Parameters.Add("@isPLot", SqlDbType.Bit).Value = 1
                    Else
                        .Parameters.Add("@isPLot", SqlDbType.Bit).Value = Dr("isPlot")
                    End If

                    .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                    .Parameters.Add("@PalletType_Index", SqlDbType.VarChar).Value = "1"
                    .Parameters.Add("@Pick_Method", SqlDbType.Int).Value = 1
                    .Parameters.Add("@Picking_Type", SqlDbType.Int).Value = 0



                    .Parameters.Add("@Unit_Width", SqlDbType.Float).Value = douDimension_Wd
                    .Parameters.Add("@Unit_Length", SqlDbType.Float).Value = douDimension_Len
                    .Parameters.Add("@Unit_Height", SqlDbType.Float).Value = douDimension_Hi
                    .Parameters.Add("@Flo1", SqlDbType.Float).Value = 0
                    .Parameters.Add("@RFIDCode", SqlDbType.VarChar).Value = ""
                    .Parameters.Add("@Location_Index", SqlDbType.VarChar).Value = ""
                 
                End With

                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()

                '----------------------------------------------------------------------------------------------------------------------------------
                strSkuRatio_Index = objSy_AutoNumber.getSys_Value("SkuRatio_Index")
                objSy_AutoNumber = Nothing

                StrSQL = "   INSERT INTO ms_SkuRatio (                                                          "
                StrSQL &= "                         SkuRatio_Index,Sku_Index,Package_Index,Ratio,               "
                StrSQL &= "                         add_by,add_date,add_branch,status_id                        "
                StrSQL &= "                         )                                                           "
                StrSQL &= "  VALUES (                                                                           "
                StrSQL &= "             @SkuRatio_Index, @Sku_Index,@Package_Index,@Ratio,                      "
                StrSQL &= "             @add_by,getdate(),@add_branch,@status_id                                "
                StrSQL &= "          )                                                                          "
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@SkuRatio_Index", SqlDbType.VarChar).Value = strSkuRatio_Index
                    .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = strSku_Index
                    .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                    .Parameters.Add("@Ratio", SqlDbType.Float).Value = 1
                    .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                End With
                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
                '----------------------------------------------------------------------------------------------------------------------------------

            End If

            myTrans.Commit()
            Return "PASS"
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Public Function ImportMasterSkuRatio(ByVal Dr As DataRow) As String
        Dim StrSQL As String = ""
        Dim objSy_AutoNumber As New Sy_AutoNumber
        Dim DimensionType_Index As String = ""
        Dim dtDimensionTypeData As New DataTable
        Dim objDiman As New ms_DimensionType
        Dim strPackage_Index As String = ""
        Dim Volume As Double = 0
        Dim strSkuRatio_Index As String = ""
        Dim skuIndex As String = ""
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try
            Dim dsCheck As New DataSet
            StrSQL = " Select * from ms_Sku Where Sku_Id = '" & Dr("Sku_Id").ToString.Trim & "' AND Status_Id <> -1 "
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = StrSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(dsCheck, "SKU_TABLE")
            If dsCheck.Tables("SKU_TABLE").Rows.Count > 0 Then
                skuIndex = dsCheck.Tables("SKU_TABLE").Rows(0)("Sku_Index").ToString
                StrSQL = " Select top 1 * from ms_Package Where "
                StrSQL &= "   Package_Id = '" & Dr("Package").ToString.Trim & "'  AND Status_Id <> -1  Order by add_date DESC "
                With SQLServerCommand
                    .Connection = Connection
                    .Transaction = myTrans
                    .CommandText = StrSQL
                    .CommandTimeout = 0
                End With
                DataAdapter.SelectCommand = SQLServerCommand
                DataAdapter.SelectCommand.Transaction = myTrans
                DataAdapter.Fill(dsCheck, "PACKAGE_TABLE")

                If dsCheck.Tables("PACKAGE_TABLE").Rows.Count <= 0 Then
                    If Dr("Dimension_Unit").ToString <> "" Then
                        dtDimensionTypeData = objDiman.GetDimensionTypeData(" AND DimensionType_Id = '" & Dr("Dimension_Unit").ToString & "'")
                        DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                    Else
                        dtDimensionTypeData = objDiman.GetDimensionTypeData("")
                        DimensionType_Index = dtDimensionTypeData.Rows(0)("DimensionType_Index").ToString
                    End If
                    _DimenstionRatio = getRatioM3(DimensionType_Index)
                    _Digit = getDigitM3(DimensionType_Index)

                    Dim douDimension_Wd As Double = 0
                    If IsDBNull(Dr("Dimension_Wd")) = False Then
                        If Dr("Dimension_Wd").ToString.Length > 0 Then
                            douDimension_Wd = Convert.ToInt64(Dr("Dimension_Wd"))
                        End If
                    End If

                    Dim douDimension_Len As Double = 0
                    If IsDBNull(Dr("Dimension_Len")) = False Then
                        If Dr("Dimension_Len").ToString.Length > 0 Then
                            douDimension_Len = Convert.ToInt64(Dr("Dimension_Len"))
                        End If
                    End If

                    Dim douDimension_Hi As Double = 0
                    If IsDBNull(Dr("Dimension_Hi")) = False Then
                        If Dr("Dimension_Hi").ToString.Length > 0 Then
                            douDimension_Hi = Convert.ToInt64(Dr("Dimension_Hi"))
                        End If
                    End If


                    Volume = Me.CalculateToM3(_DimenstionRatio, douDimension_Wd, douDimension_Len, douDimension_Hi)

                    strPackage_Index = objSy_AutoNumber.getSys_Value("Package_Index")
                    StrSQL = "   INSERT INTO ms_Package (                                                           "
                    StrSQL &= "                         Package_Index,Package_Id,Description,                       "
                    StrSQL &= "                         Dimension_Hi,Dimension_Wd,Dimension_Len,Dimension_Unit_Id , "
                    StrSQL &= "                         add_by,add_date,add_branch,status_id ,                      "
                    StrSQL &= "                         Pkg_Description,isItem_Package,Weight,DimensionType_Index   "
                    StrSQL &= "                         )                                                           "
                    StrSQL &= "  VALUES (                                                                           "
                    StrSQL &= "             @Package_Index, @Package_Id,@Description ,                              "
                    StrSQL &= "             @Dimension_Hi,@Dimension_Wd,@Dimension_Len,@Dimension_Unit_Id ,         "
                    StrSQL &= "             @add_by,getdate(),@add_branch,@status_id ,                              "
                    StrSQL &= "             @Pkg_Description,@isItem_Package,@Weight,@DimensionType_Index           "
                    StrSQL &= "          )                                                                          "
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                        .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Dr("Package").ToString
                        .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Dr("Package").ToString
                        .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = douDimension_Hi
                        .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = douDimension_Wd
                        .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = douDimension_Len
                        .Parameters.Add("@Dimension_Unit_Id", SqlDbType.VarChar).Value = "0"
                        .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                        .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                        .Parameters.Add("@Pkg_Description", SqlDbType.NVarChar).Value = ""
                        .Parameters.Add("@isItem_Package", SqlDbType.Int).Value = 0
                        If IsDBNull(Dr("Weight")) = False Then
                            .Parameters.Add("@Weight", SqlDbType.Float).Value = Dr("Weight")
                        Else
                            .Parameters.Add("@Weight", SqlDbType.Float).Value = 0
                        End If

                        .Parameters.Add("@DimensionType_Index", SqlDbType.VarChar, 13).Value = DimensionType_Index
                    End With
                    SetSQLString = StrSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                    '----------------------------------------------------------------------------------------------------------------------------------
                    strSkuRatio_Index = objSy_AutoNumber.getSys_Value("SkuRatio_Index")
                    objSy_AutoNumber = Nothing
                    StrSQL = "   INSERT INTO ms_SkuRatio (                                                          "
                    StrSQL &= "                         SkuRatio_Index,Sku_Index,Package_Index,Ratio,               "
                    StrSQL &= "                         add_by,add_date,add_branch,status_id                        "
                    StrSQL &= "                         )                                                           "
                    StrSQL &= "  VALUES (                                                                           "
                    StrSQL &= "             @SkuRatio_Index, @Sku_Index,@Package_Index,@Ratio,                      "
                    StrSQL &= "             @add_by,getdate(),@add_branch,@status_id                                "
                    StrSQL &= "          )                                                                          "
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@SkuRatio_Index", SqlDbType.VarChar).Value = strSkuRatio_Index
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = dsCheck.Tables("SKU_TABLE").Rows(0)("Sku_Index").ToString
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = strPackage_Index
                        .Parameters.Add("@Ratio", SqlDbType.Float).Value = 1
                        .Parameters.Add("@add_by", SqlDbType.VarChar).Value = WV_UserName
                        .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                        .Parameters.Add("@status_id", SqlDbType.Int).Value = 0
                    End With
                    SetSQLString = StrSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                    '----------------------------------------------------------------------------------------------------------------------------------
                Else
                    StrSQL = " Update ms_Package Set "
                    StrSQL &= " Package_Id = @Package_Id "
                    StrSQL &= " ,Description = @Description "
                    StrSQL &= " ,Dimension_Hi = @Dimension_Hi "
                    StrSQL &= " ,Dimension_Wd = @Dimension_Wd "
                    StrSQL &= " ,Dimension_Len = @Dimension_Len "
                    StrSQL &= " ,DimensionType_Index = @DimensionType_Index "
                    StrSQL &= " where Package_Index = @Package_Index "

                    Dim douDimension_Wd As Double = 0
                    If IsDBNull(Dr("Dimension_Wd")) = False Then
                        If Dr("Dimension_Wd").ToString.Length > 0 Then
                            douDimension_Wd = Convert.ToInt64(Dr("Dimension_Wd"))
                        End If
                    End If

                    Dim douDimension_Len As Double = 0
                    If IsDBNull(Dr("Dimension_Len")) = False Then
                        If Dr("Dimension_Len").ToString.Length > 0 Then
                            douDimension_Len = Convert.ToInt64(Dr("Dimension_Len"))
                        End If
                    End If

                    Dim douDimension_Hi As Double = 0
                    If IsDBNull(Dr("Dimension_Hi")) = False Then
                        If Dr("Dimension_Hi").ToString.Length > 0 Then
                            douDimension_Hi = Convert.ToInt64(Dr("Dimension_Hi"))
                        End If
                    End If

                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = dsCheck.Tables("PACKAGE_TABLE").Rows(0)("Package_Index").ToString
                        .Parameters.Add("@Package_Id", SqlDbType.NVarChar).Value = Dr("Package").ToString
                        .Parameters.Add("@Description", SqlDbType.NVarChar).Value = Dr("Package").ToString
                        .Parameters.Add("@Dimension_Hi", SqlDbType.Float).Value = douDimension_Hi
                        .Parameters.Add("@Dimension_Wd", SqlDbType.Float).Value = douDimension_Wd
                        .Parameters.Add("@Dimension_Len", SqlDbType.Float).Value = douDimension_Len
                        .Parameters.Add("@DimensionType_Index", SqlDbType.VarChar, 13).Value = DimensionType_Index
                    End With
                    SetSQLString = StrSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()


                    StrSQL = " Update ms_SkuRatio Set "
                    StrSQL &= " Ratio = @Ratio "
                    StrSQL &= " where Package_Index = @Package_Index And Sku_Index = @Sku_Index "
                    With SQLServerCommand
                        .Parameters.Clear()
                        .Parameters.Add("@Sku_Index", SqlDbType.VarChar).Value = dsCheck.Tables("SKU_TABLE").Rows(0)("Sku_Index").ToString
                        .Parameters.Add("@Package_Index", SqlDbType.VarChar).Value = dsCheck.Tables("PACKAGE_TABLE").Rows(0)("Package_Index").ToString
                        .Parameters.Add("@Ratio", SqlDbType.Float).Value = Dr("Ratio")
                    End With
                    SetSQLString = StrSQL
                    SetCommandType = DBType_SQLServer.enuCommandType.Text
                    SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                    EXEC_Command()
                End If




            Else
                Return "Nothing SKU : Insert SKURatio"
            End If
            myTrans.Commit()
            Return "PASS"
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function

    Public Function ImportMasterCustomer(ByVal Dr As DataRow) As String
        Dim StrSQL As String = ""
        Dim _Customer_Index As String = ""
        Dim objSy_AutoNumber As New Sy_AutoNumber
        connectDB()
        Dim myTrans As SqlClient.SqlTransaction = Connection.BeginTransaction()
        SQLServerCommand.Transaction = myTrans
        Try

            Dim dtCheck As New DataTable
            StrSQL = "Select Customer_Index from ms_Customer Where Customer_Id = '" & Dr("Customer_Id") & "' AND Status_Id <> -1"
            With SQLServerCommand
                .Connection = Connection
                .Transaction = myTrans
                .CommandText = StrSQL
                .CommandTimeout = 0
            End With
            DataAdapter.SelectCommand = SQLServerCommand
            DataAdapter.SelectCommand.Transaction = myTrans
            DataAdapter.Fill(dtCheck)

            If dtCheck.Rows.Count > 0 Then

            Else
                _Customer_Index = objSy_AutoNumber.getSys_Value("Customer_Index")

                StrSQL = " insert into ms_Customer (customer_Index,customer_Id,title,customer_Name,billing_Location,Customer_Type_Index,address,district_Index,province_Index,postcode,tel,fax,mobile,email,contact_Person,contact_Person2,contact_Person3,barcode,billing_Type_Index,credit_Term,vat_Inc,billing_Date,remark,price1,price_Unit1,status,add_by,add_date,add_branch,str1,str2,str3,str4,str5,str6) "
                StrSQL &= " values (@customer_Index,@customer_Id,@title,@customer_Name,@billing_Location,@customer_Type_Id,@address,@district_Index,@province_Index,@postcode,@tel,@fax,@mobile,@email,@contact_Person,@contact_Person2,@contact_Person3,@barcode,@billing_Type,@credit_Term,@vat_Inc,@billing_Date,@remark,@price1,@price_Unit1,@status,@add_by,getdate(),@add_branch,@str1,@str2,@str3,@str4,@str5,@str6)"

                StrSQL = StrSQL
                With SQLServerCommand
                    .Parameters.Clear()
                    .Parameters.Add("@customer_Index", SqlDbType.VarChar, 13).Value = _Customer_Index
                    .Parameters.Add("@customer_Id", SqlDbType.VarChar, 50).Value = Dr("Customer_Id").ToString.Trim
                    .Parameters.Add("@title", SqlDbType.VarChar, 50).Value = ""
                    .Parameters.Add("@customer_Name", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@billing_Location", SqlDbType.VarChar, 255).Value = Dr("")
                    .Parameters.Add("@customer_Type_Id", SqlDbType.VarChar, 13).Value = Dr("")
                    .Parameters.Add("@address", SqlDbType.VarChar, 255).Value = Dr("")
                    .Parameters.Add("@district_Index", SqlDbType.VarChar, 13).Value = Dr("")
                    .Parameters.Add("@province_Index", SqlDbType.VarChar, 13).Value = Dr("")
                    .Parameters.Add("@postcode", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@tel", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@fax", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@mobile", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@email", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@contact_Person", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@contact_Person2", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@contact_Person3", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@barcode", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@billing_Type", SqlDbType.VarChar, 30).Value = Dr("")
                    .Parameters.Add("@credit_Term", SqlDbType.Int).Value = Dr("")
                    .Parameters.Add("@vat_Inc", SqlDbType.Int).Value = Dr("")
                    .Parameters.Add("@billing_Date", SqlDbType.Int).Value = Dr("")
                    .Parameters.Add("@remark", SqlDbType.VarChar, 255).Value = Dr("")
                    .Parameters.Add("@price1", SqlDbType.Float).Value = Dr("")
                    .Parameters.Add("@price_Unit1", SqlDbType.NVarChar, 100).Value = Dr("")
                    .Parameters.Add("@status", SqlDbType.Int).Value = Dr("")
                    .Parameters.Add("@add_by", SqlDbType.VarChar, 50).Value = WV_UserName
                    .Parameters.Add("@add_branch", SqlDbType.Int).Value = WV_Branch_ID
                    .Parameters.Add("@str1", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@str2", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@str3", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@str4", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@str5", SqlDbType.VarChar, 100).Value = Dr("")
                    .Parameters.Add("@str6", SqlDbType.VarChar, 100).Value = Dr("")
                End With

                SetSQLString = StrSQL
                SetCommandType = DBType_SQLServer.enuCommandType.Text
                SetEXEC_TYPE = DBType_SQLServer.EXEC.NonQuery
                EXEC_Command()
            End If

           

            myTrans.Commit()
            Return "PASS"
        Catch ex As Exception
            myTrans.Rollback()
            Throw ex
        Finally
            disconnectDB()
        End Try
    End Function
    Private Function getRatioM3(ByVal pstrDimensionType_Index As String) As Double
        Dim objDB As New ms_DimensionType
        Dim _Ratio As Double = 1
        Try
            Return objDB.GetRatioDimensionTypeData(pstrDimensionType_Index)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Function getDigitM3(ByVal pstrDimensionType_Index As String) As Integer
        Dim objDB As New ms_DimensionType
        Try
            Return objDB.GetDigitDimensionTypeData(pstrDimensionType_Index)
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Function CalculateToM3(ByVal Ratio As Double, ByVal Width As Double, ByVal Length As Double, ByVal Height As Double) As Double
        Dim Volume As Double = 0
        Volume = (CDbl(Width) * CDbl(Length) * CDbl(Height)) / Ratio
        If Volume <> 0 Then
            Volume = Math.Round(Volume, _Digit).ToString
        End If
        Return Volume
    End Function

    Public Function checkShipTo(ByVal pShipTo_ID As String, ByVal pSoldTo_Id As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT   * " & _
             " FROM  ms_Customer_Shipping_Location  " & _
             " WHERE   Customer_Shipping_Location_Id = '" & pShipTo_ID.ToString & "' And Status_Id <> -1 and Customer_Shipping_Index=(select top 1 Customer_Shipping_Index from ms_Customer_Shipping where str1='" & pSoldTo_Id.ToString & "' and status_id <> -1) "

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
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function checkSoldTo(ByVal pSoldTo_Id As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT   * " & _
             " FROM  ms_Customer_Shipping  " & _
             " WHERE   str1 = '" & pSoldTo_Id.ToString & "' And Status_Id <> -1"

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
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function checkRoute(ByVal pShipTo_ID As String, ByVal pSoldTo_Id As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT   * " & _
             " FROM  ms_Customer_Shipping_Location  " & _
             " WHERE   Customer_Shipping_Location_Id = '" & pShipTo_ID.ToString & "' And Status_Id <> -1 and Customer_Shipping_Index=(select top 1 Customer_Shipping_Index from ms_Customer_Shipping where str1='" & pSoldTo_Id.ToString & "' and status_id <> -1) "

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
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

    Public Function checkOwner(ByVal pOwner_Id As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT   * " & _
             " FROM  ms_Customer  " & _
             " WHERE   Customer_Id = '" & pOwner_Id.ToString & "' And Status_Id <> -1"

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
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function


    Public Function checkSku_Id(ByVal pSku_Id As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try
            strSQL = " SELECT   * " & _
             " FROM  ms_Sku  " & _
             " WHERE   Sku_Id = '" & pSku_Id.ToString & "' And Status_Id <> -1"

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
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function


    Public Function checkPackage(ByVal pPackage_Id As String, ByVal pSku_Id As String) As Boolean
        Dim _dataTable As New DataTable
        Dim strSQL As String = " "
        Try

            strSQL = "  SELECT        dbo.ms_SKU.Sku_Index, dbo.ms_SKU.Sku_Id, dbo.ms_Package.Package_Id, dbo.ms_Package.Description, dbo.ms_SKURatio.Ratio"
            strSQL &= " FROM            dbo.ms_SKU INNER JOIN"
            strSQL &= "                   dbo.ms_SKURatio ON dbo.ms_SKU.Sku_Index = dbo.ms_SKURatio.Sku_Index INNER JOIN"
            strSQL &= "                  dbo.ms_Package ON dbo.ms_SKURatio.Package_Index = dbo.ms_Package.Package_Index"
            strSQL &= "     WHERE (dbo.ms_SKU.status_id <> -1)"
            strSQL &= " And   ms_SKU.Sku_Id = '" & pSku_Id & "'      "
            strSQL &= " And   ms_Package.Description = '" & pPackage_Id & "'      "

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
            Return Nothing
            Throw ex
        Finally
            disconnectDB()
        End Try

    End Function

End Class
