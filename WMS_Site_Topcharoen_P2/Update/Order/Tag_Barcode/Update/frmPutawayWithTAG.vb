Imports WMS_STD_Master
Imports WMS_STD_Formula
Imports WMS_STD_Master.W_Language
Imports WMS_STD_INB_Receive_Datalayer
'Imports WMS_SM_VR_WareHouse
Imports WMS_STD_Master_Datalayer

Public Class frmPutawayWithTAG


    Private _TAG_Index As List(Of String)
    Public Property TAG_Index() As List(Of String)
        Get
            Return _TAG_Index
        End Get
        Set(ByVal value As List(Of String))
            _TAG_Index = value
        End Set
    End Property

    Private _Order_Index As String
    Public Property Order_Index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property

    Private _LocationID As String


    Public Property LocationID() As String
        Get
            Return _LocationID
        End Get
        Set(ByVal value As String)
            _LocationID = value
        End Set
    End Property

    Private _USE_Warehouse_NEW_VERSION As String = ""

    Public Property USE_Warehouse_NEW_VERSION() As String
        Get
            Return _USE_Warehouse_NEW_VERSION
        End Get
        Set(ByVal value As String)
            _USE_Warehouse_NEW_VERSION = value
        End Set
    End Property



    Private Sub frmPutawayWithTAG_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            ' ------ Set Language Begin ------
            Dim oFunction As New W_Language
            oFunction.SwitchLanguage(Me, 1)
            oFunction.SW_Language_Column(Me, Me.grdAllocate_Qty, 1)
            ' ------ Set Language End ------

            Me.SetWarehouse_NEW_VERSION()

            grdAllocate_Qty.AutoGenerateColumns = False
            'Me.getPutawayWithTAGHeader(Order_Index)
            Me.getPutawayWithTAGItem(Order_Index)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub SetWarehouse_NEW_VERSION()
        Dim objCustomSetting As New config_CustomSetting
        Dim objDT As DataTable = New DataTable

        Try
            objCustomSetting.GetConfig_Value("USE_Warehouse_NEW_VERSION", "")
            objDT = objCustomSetting.DataTable
            If objDT.Rows.Count > 0 Then
                Me._USE_Warehouse_NEW_VERSION = objDT.Rows(0).Item("Config_Value").ToString

            Else
                Me._USE_Warehouse_NEW_VERSION = ""
            End If

            '###################################
        Catch ex As Exception
            Throw ex
        Finally
            objDT = Nothing
            objCustomSetting = Nothing
        End Try
    End Sub
    'Private Sub getPutawayWithTAGHeader(ByVal Order_Index As String)
    '    'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

    '    Dim objClassDB As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
    '    Dim objDT As DataTable = New DataTable

    '    Try
    '        objClassDB.getPutawayWithTAG(Order_Index)
    '        objDT = objClassDB.DataTable

    '        If objDT.Rows.Count > 0 Then
    '            txtReciveNo.Text = objDT.Rows(0).Item("Order_No").ToString
    '            txtReciveDate.Text = objDT.Rows(0).Item("Order_Date").ToString
    '            txtCustomer.Text = objDT.Rows(0).Item("Customer_Name").ToString

    '            Format(objDT.Rows(0).Item("Order_Date").ToString, "MM/dd/yyyy").ToString()

    '        End If
    '    Catch ex As Exception
    '        Throw ex
    '    Finally
    '        objClassDB = Nothing
    '        objDT = Nothing
    '    End Try

    'End Sub

    Private Sub getPutawayWithTAGItem(ByVal Order_Index As String)
        'xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx

        Dim objClassDB As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            If Not _TAG_Index Is Nothing Then
                objClassDB.getPutawayWithTAG(Order_Index, _TAG_Index)
            Else
                objClassDB.getPutawayWithTAG(Order_Index)
            End If
            objDT = objClassDB.DataTable
            'If Not Create Tag 
            If objDT.Rows.Count = 0 Then
                W_MSG_Information("กรุณาสร้าง TAG ก่อนทำการจัดเก็บ")
                Me.Close()
            Else
                txtReciveNo.Text = objDT.Rows(0).Item("Order_No").ToString
                txtReciveDate.Text = objDT.Rows(0).Item("Order_Date").ToString
                txtCustomer.Text = objDT.Rows(0).Item("Customer_Name").ToString

                '  Format(objDT.Rows(0).Item("Order_Date").ToString, "dd/MM/yyyy").ToString()

            End If

            objDT.Columns.Add(New DataColumn("Row_Index"))

            For i As Integer = 0 To objDT.Rows.Count - 1
                objDT.Rows(i)("Row_Index") = i + 1
            Next


            If objDT.Columns.Contains("Location_Alias") = False Then
                objDT.Columns.Add("Location_Alias", GetType(String))
            End If



            grdAllocate_Qty.DataSource = objDT

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

    End Sub

    Private Sub btnDeleteItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeleteItem.Click
        Try
            'ถ้าต้องการลบจากฐานข้อมูลให้ลบจากหน้าจัดการ TAG
            If grdAllocate_Qty.Rows.Count = 0 Then
                W_MSG_Error_ByIndex(76)
                Exit Sub
            End If
            Dim strTag_Index As String = Me.grdAllocate_Qty.CurrentRow.Cells("col_Tag_Index").Value
            If W_MSG_Confirm_ByIndex(5) = Windows.Forms.DialogResult.Yes Then
                Me.grdAllocate_Qty.Rows.RemoveAt(Me.grdAllocate_Qty.CurrentRow.Index)
                CType(Me.grdAllocate_Qty.DataSource, DataTable).AcceptChanges()
            End If

        Catch ex As Exception
            Throw ex
        End Try

    End Sub

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Me.Close()
    End Sub

    Private Sub grdAllocate_Qty_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAllocate_Qty.CellClick
        Select Case CType(sender, DataGridView).CurrentCell.OwningColumn.Name
            Case "btnSearhLocation"
                'Dim frmVR As New frmVRWH_PutAway
                'frmVR.ShowDialog()
                'With Me.grdAllocate_Qty
                '    .Rows(e.RowIndex).Cells("Location_Index").Value = frmVR.Location_Alias
                'End With
                'frmVR.Close()
                'If _USE_Warehouse_NEW_VERSION = 0 Then
                '    Dim frmVR As New frmVRmain
                '    frmVR.ShowDialog()
                '    With Me.grdAllocate_Qty
                '        .Rows(e.RowIndex).Cells("Location_Index").Value = frmVR.Location_Alias
                '    End With
                '    frmVR.Close()

                'Else
                'TODO: รอ ฟอร์ม
                Dim frmVR As New WMS_STD_VRWareHouse.frmVRWH_PutAway
                frmVR.ShowDialog()
                With Me.grdAllocate_Qty
                    .Rows(e.RowIndex).Cells("Location_Index").Value = frmVR.Location_Alias
                End With
                frmVR.Close()
                'End If

        End Select
    End Sub

    'Private Sub grdAllocate_Qty_CellValueChanged(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAllocate_Qty.CellValueChanged
    '    Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
    '    Dim strTo_Location_Index As String = oms_Location.getLocation_Index(grdAllocate_Qty.CurrentRow.Cells("Location_Index").ToString)


    '    If strTo_Location_Index = "" Then
    '        W_MSG_Information("กรุณาป้อนตำแหน่งใหม่ ไม่พบตำแหน่งที่ระบุ  " & grdAllocate_Qty.CurrentRow.Cells("Location_Index").ToString)
    '        Exit Sub
    '    Else
    '        If oms_Location.isOverFlow_Qty(grdAllocate_Qty.CurrentRow.Cells("Location_Index").ToString, Val(grdAllocate_Qty.CurrentRow.Cells("Location_Qty").ToString)) = True Then
    '            W_MSG_Information("ตำแหน่งใหม่ " & grdAllocate_Qty.CurrentRow.Cells("Location_Index").ToString & " พื้นที่ว่างไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
    '            Exit Sub
    '        End If
    '    End If
    'End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        ' Dim objOrderItemLocationCollection As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
        If grdAllocate_Qty.RowCount = 0 Then Exit Sub

        Dim objOrderItemLocation As New tb_OrderItemLocation
        Dim objCollection As New List(Of tb_OrderItemLocation)
        Dim clsJobOrder As New tb_JobOrder

        Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)

        clsJobOrder = setObjJobOrder()



        Try

            'Checking Location


            For i As Integer = 0 To grdAllocate_Qty.Rows.Count - 1
                If grdAllocate_Qty.Rows(i).Cells("Location_Index").Value Is Nothing Then
                    W_MSG_Information("กรุณาป้อนตำแหน่งให้ครบ")
                    Exit Sub
                End If
                _LocationID = grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString
                If _LocationID.Trim = "" Then
                    W_MSG_Information("กรุณาป้อนตำแหน่งให้ครบ")
                    Exit Sub
                End If

                If CheckLocation(_LocationID) = False Then
                    Exit Sub
                End If
            Next
     
            'For Each drSaveOIL As DataRow In CType(grdAllocate_Qty.DataSource, DataTable).Rows
            '    If drSaveOIL("Location_Alias").ToString <> "" Then
            '        SetLocation_fromitemAll(drSaveOIL)
            '    End If
            'Next

            'Dim objJobOrder As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
            'objJobOrder.SaveData(Me._Order_Index)
            'W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")

            '***********************************************************************************************
            'dong comment and add new 2012/07/30
            Dim objCollection_CollOrderItemLocation As New List(Of List(Of tb_OrderItemLocation))
            Dim objCollection_JobOrder As New List(Of tb_JobOrder)

            For Each drSaveOIL As DataRow In CType(grdAllocate_Qty.DataSource, DataTable).Rows
                If drSaveOIL("Location_Alias").ToString <> "" Then
                    Dim objReturn() As Object
                    objReturn = Me.SetLocation_fromitemAll(drSaveOIL)
                    If objReturn Is Nothing Then
                        Exit Sub
                    Else
                        objCollection_CollOrderItemLocation.Add(objReturn(0))
                        objCollection_JobOrder.Add(objReturn(1))
                    End If

                End If
            Next

            Dim objJobOrder As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.CHECK_BALANCE)
            objJobOrder.SaveData_PutAway(Me._Order_Index, objCollection_CollOrderItemLocation, objCollection_JobOrder)
            W_MSG_Information("บันทึกข้อมูลเรียบร้อยแล้ว")

        Catch ex As Exception
            W_MSG_Error(ex.Message)

        End Try
        btnSave.Enabled = False
        btnPrintReport.Enabled = True
    End Sub


    
#Region "Save Auto OrderItemLocation "
    Function SetLocation_fromitemAll(ByVal drSaveOIL As DataRow) As Object()
        Try
            Dim objOrderItemLocation As New tb_OrderItemLocation
            Dim objOrderItem As New tb_OrderItem
            Dim objCollection As New List(Of tb_OrderItemLocation)
            Dim clsJobOrder As New tb_JobOrder

            clsJobOrder = setObjJobOrder()
            '  objOrderItem = getProductSelection(Me.OrderItem_Id) '(Me.grdOrderItem.Rows(RowIndex).Cells("OrderItem_Index").Value)
            objCollection.Clear()

            Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
            Dim sumQty As Double = 0
            Dim sumTotal_Qty As Double = 0
            Dim Recieve_Qty As Double = CDbl(drSaveOIL("Qty_per_TAG").ToString)
            Dim Recieve_Total_Qty As Double = CDbl(drSaveOIL("Total_Qty").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Total_Qty").Value)




            objOrderItemLocation = New tb_OrderItemLocation

            With objOrderItemLocation
                .TAG_Index = drSaveOIL("TAG_Index").ToString
                .Order_Index = Me._Order_Index
                .OrderItem_Index = drSaveOIL("OrderItem_Index").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Col_OrderItem_Index").Value.ToString
                .Tag_No = drSaveOIL("Tag_No").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Tag_No").Value.ToString
                .Package_Index = drSaveOIL("Package_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_Package_Index").Value.ToString
                .PLot = drSaveOIL("Plot").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("clPlot").Value.ToString
                .ItemStatus_Index = drSaveOIL("ItemStatus_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_ItemStatus_Index").Value.ToString
                .Serial_No = ""
                .Location_Index = objClassDB.getLocation_Index(drSaveOIL("Location_Alias").ToString)
                .PalletType_Index = ""
                .Pallet_Qty = 0
                .Sku_Index = drSaveOIL("Sku_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("Col_Sku_Index").Value.ToString
                .Qty = Recieve_Qty
                .Total_Qty = Recieve_Total_Qty
                .Ratio = CDbl(drSaveOIL("Ratio").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Ratio").Value)
                .Weight = CDbl(drSaveOIL("Weight_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_weight").Value.ToString)
                .Volume = CDbl(drSaveOIL("Volume_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_Volume").Value.ToString)
                .MixPallet = 0

                .ERP_Location = drSaveOIL("ERP_Location").ToString

                If IsNumeric(drSaveOIL("Qty_Per_Pck").ToString) Then .Qty_Item = CDbl(drSaveOIL("Qty_Per_Pck").ToString) * .Qty
                If IsNumeric(drSaveOIL("Price_Per_Pck").ToString) Then .OrderItem_Price = CDbl(drSaveOIL("Price_Per_Pck").ToString) * .Qty



                ' *** Sum Balance ***
                sumQty = sumQty + .Qty
                sumTotal_Qty = sumTotal_Qty + .Total_Qty

            End With

            objCollection.Add(objOrderItemLocation)

            ' *** Check Balance ***
            If Not Recieve_Qty = sumQty Then
                MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return Nothing
            End If



            'Move in Transaction

            '' *** Save Data ***
            'Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
            'objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
            'objJobLocation = Nothing
            Dim objReturn(1) As Object
            objReturn(0) = objCollection
            objReturn(1) = clsJobOrder

            Return objReturn

        Catch ex As Exception
            Throw ex
        End Try

    End Function

    'Sub SetLocation_fromitemAll(ByVal drSaveOIL As DataRow)
    '    Dim objOrderItemLocation As New tb_OrderItemLocation
    '    Dim objOrderItem As New tb_OrderItem
    '    Dim objCollection As New List(Of tb_OrderItemLocation)
    '    Dim clsJobOrder As New tb_JobOrder

    '    clsJobOrder = setObjJobOrder()
    '    '  objOrderItem = getProductSelection(Me.OrderItem_Id) '(Me.grdOrderItem.Rows(RowIndex).Cells("OrderItem_Index").Value)
    '    objCollection.Clear()

    '    Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
    '    Dim sumQty As Double = 0
    '    Dim sumTotal_Qty As Double = 0
    '    Dim Recieve_Qty As Double = CDbl(drSaveOIL("Qty_per_TAG").ToString)
    '    Dim Recieve_Total_Qty As Double = CDbl(drSaveOIL("Total_Qty").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Total_Qty").Value)




    '    objOrderItemLocation = New tb_OrderItemLocation

    '    With objOrderItemLocation

    '        .Order_Index = Me._Order_Index
    '        .OrderItem_Index = drSaveOIL("OrderItem_Index").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Col_OrderItem_Index").Value.ToString
    '        .Tag_No = drSaveOIL("Tag_No").ToString 'grdAllocate_Qty.Rows(RowIndex).Cells("Tag_No").Value.ToString
    '        .Package_Index = drSaveOIL("Package_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_Package_Index").Value.ToString
    '        .PLot = drSaveOIL("Plot").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("clPlot").Value.ToString
    '        .ItemStatus_Index = drSaveOIL("ItemStatus_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("col_ItemStatus_Index").Value.ToString
    '        .Serial_No = ""
    '        .Location_Index = objClassDB.getLocation_Index(drSaveOIL("Location_Alias").ToString)
    '        .PalletType_Index = ""
    '        .Pallet_Qty = 0
    '        .Sku_Index = drSaveOIL("Sku_Index").ToString ' grdAllocate_Qty.Rows(RowIndex).Cells("Col_Sku_Index").Value.ToString
    '        .Qty = Recieve_Qty
    '        .Total_Qty = Recieve_Total_Qty
    '        .Ratio = CDbl(drSaveOIL("Ratio").ToString) ' Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Ratio").Value)
    '        .Weight = CDbl(drSaveOIL("Weight_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_weight").Value.ToString)
    '        .Volume = CDbl(drSaveOIL("Volume_Per_Tag").ToString) ' Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_Volume").Value.ToString)
    '        .MixPallet = 0

    '        If IsNumeric(drSaveOIL("Qty_Per_Pck").ToString) Then .Qty_Item = CDbl(drSaveOIL("Qty_Per_Pck").ToString) * .Qty
    '        If IsNumeric(drSaveOIL("Price_Per_Pck").ToString) Then .OrderItem_Price = CDbl(drSaveOIL("Price_Per_Pck").ToString) * .Qty



    '        ' *** Sum Balance ***
    '        sumQty = sumQty + .Qty
    '        sumTotal_Qty = sumTotal_Qty + .Total_Qty

    '    End With

    '    objCollection.Add(objOrderItemLocation)

    '    ' *** Check Balance ***
    '    If Not Recieve_Qty = sumQty Then
    '        MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    'If Not Recieve_Total_Qty = sumTotal_Qty Then
    '    '    MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วย  SKU)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '    '    Exit Sub
    '    'End If
    '    ' ******************

    '    ' *** Save Data ***
    '    Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
    '    objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
    '    objJobLocation = Nothing
    '    'MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)

    'End Sub

    'Sub SetLocation_fromitemAll(ByVal RowIndex As String)
    '    Dim objOrderItemLocation As New tb_OrderItemLocation
    '    Dim objOrderItem As New tb_OrderItem
    '    Dim objCollection As New List(Of tb_OrderItemLocation)
    '    Dim clsJobOrder As New tb_JobOrder

    '    clsJobOrder = setObjJobOrder()
    '    '  objOrderItem = getProductSelection(Me.OrderItem_Id) '(Me.grdOrderItem.Rows(RowIndex).Cells("OrderItem_Index").Value)
    '    objCollection.Clear()

    '    Dim objClassDB As New ms_Location(ms_Location.enuOperation_Type.SEARCH)
    '    Dim sumQty As Double = 0
    '    Dim sumTotal_Qty As Double = 0
    '    Dim Recieve_Qty As Double = Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Location_Qty").Value)
    '    Dim Recieve_Total_Qty As Double = Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Total_Qty").Value)


    '    Dim Qty_PerPackage As Double = 0
    '    Dim Price_PerPackage As Double = 0

    '    objOrderItemLocation = New tb_OrderItemLocation

    '    With objOrderItemLocation

    '        .Order_Index = Me._Order_Index
    '        .OrderItem_Index = grdAllocate_Qty.Rows(RowIndex).Cells("Col_OrderItem_Index").Value.ToString
    '        .Tag_No = grdAllocate_Qty.Rows(RowIndex).Cells("Tag_No").Value.ToString
    '        .Package_Index = grdAllocate_Qty.Rows(RowIndex).Cells("col_Package_Index").Value.ToString
    '        .PLot = grdAllocate_Qty.Rows(RowIndex).Cells("clPlot").Value.ToString
    '        .ItemStatus_Index = grdAllocate_Qty.Rows(RowIndex).Cells("col_ItemStatus_Index").Value.ToString
    '        .Serial_No = ""
    '        .Tag_No = grdAllocate_Qty.Rows(RowIndex).Cells("Tag_No").Value.ToString
    '        .Location_Index = objClassDB.getLocation_Index(grdAllocate_Qty.Rows(RowIndex).Cells("Location_Index").Value.ToString)
    '        .PalletType_Index = ""
    '        .Pallet_Qty = 0
    '        .Sku_Index = grdAllocate_Qty.Rows(RowIndex).Cells("Col_Sku_Index").Value.ToString
    '        .Qty = Recieve_Qty
    '        .Total_Qty = Recieve_Total_Qty
    '        .Ratio = Val(Me.grdAllocate_Qty.Rows(RowIndex).Cells("Col_Ratio").Value)
    '        .Weight = Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_weight").Value.ToString)
    '        .Volume = Val(grdAllocate_Qty.Rows(RowIndex).Cells("Col_Volume").Value.ToString)
    '        .MixPallet = 0


    '        .Qty_Item = 0
    '        .OrderItem_Price = 0

    '        ' *** Sum Balance ***
    '        sumQty = sumQty + .Qty
    '        sumTotal_Qty = sumTotal_Qty + .Total_Qty

    '    End With

    '    objCollection.Add(objOrderItemLocation)

    '    ' *** Check Balance ***
    '    If Not Recieve_Qty = sumQty Then
    '        MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วยสินค้ารับเข้า)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '        Exit Sub
    '    End If

    '    'If Not Recieve_Total_Qty = sumTotal_Qty Then
    '    '    MessageBox.Show("รายการสินค้าที่เข้ากับรายการทึ่จัดเก็บไม่เท่ากัน (หน่วย  SKU)", "ตรวจสอบข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    '    '    Exit Sub
    '    'End If
    '    ' ******************

    '    ' *** Save Data ***
    '    Dim objJobLocation As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.ADDNEW)
    '    objJobLocation.Insert_OrderItemLocation(objCollection, clsJobOrder)
    '    objJobLocation = Nothing
    '    'MessageBox.Show("บันทึกข้อมูลเรียบร้อย", "บันทึกข้อมูล", MessageBoxButtons.OK, MessageBoxIcon.Information)

    'End Sub

    Function getProductSelection(ByVal OrderItem_Index As String) As Object

        Dim objOrderItem As New tb_OrderItem

        Dim objClassDB As New JobOrderTransaction(JobOrderTransaction.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProductSelection(OrderItem_Index)
            objDT = objClassDB.DataTable
            If objDT.Rows.Count > 0 Then

                '  *** Define value to medel *** 
                objOrderItem = New tb_OrderItem
                objOrderItem.Sku_Index = objDT.Rows(0).Item("SKU_Index").ToString
                objOrderItem.OrderItem_Index = objDT.Rows(0).Item("OrderItem_Index").ToString
                objOrderItem.Order_Index = objDT.Rows(0).Item("Order_Index").ToString
                objOrderItem.Plot = objDT.Rows(0).Item("Plot").ToString
                objOrderItem.Lot_No = objDT.Rows(0).Item("Lot_No").ToString
                objOrderItem.Serial_No = objDT.Rows(0).Item("Serial_No").ToString
                objOrderItem.ItemStatus_Index = objDT.Rows(0).Item("ItemStatus_Index").ToString
                objOrderItem.Package_Index = objDT.Rows(0).Item("Package_Index").ToString

            End If
        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try

        Return objOrderItem

    End Function

    Function setObjJobOrder() As Object

        Dim objJobOrder As New tb_JobOrder
        objJobOrder.JobOrder_Index = _Order_Index
        objJobOrder.JobOrder_Date = dtpJobOrder_Date.Value
        objJobOrder.JobOrder_No = txtReciveNo.Text

        ' *************************************
        ' Current System Use tb_Order with tb_JobOrder by 1:1  
        '  Value of in  tb_JobOrder.JobOrder_Index field  >> tb_JobOrder.JobOrder_Index =tb_Order.Order_Index 
        objJobOrder.Order_Index = Me._Order_Index
        ' *************************************

        objJobOrder.Str1 = ""
        objJobOrder.Str2 = ""
        objJobOrder.Str3 = ""
        objJobOrder.Str4 = ""
        objJobOrder.Str5 = ""

        Return objJobOrder
    End Function
#End Region

    Private Sub grdAllocate_Qty_CellEndEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdAllocate_Qty.CellEndEdit
        If Me.grdAllocate_Qty.Rows(e.RowIndex).Cells("Location_Index").ColumnIndex = e.ColumnIndex Then

            If Me.grdAllocate_Qty.Rows(e.RowIndex).Cells("Location_Index").Value Is Nothing Then Exit Sub

            If Me.grdAllocate_Qty.Rows(e.RowIndex).Cells("Location_Index").Value.ToString.Trim = "" Then Exit Sub

            If Me.CheckLocation(Me.grdAllocate_Qty.Rows(e.RowIndex).Cells("Location_Index").Value.ToString.Trim) = False Then
                Me.grdAllocate_Qty.Rows(e.RowIndex).Cells("Location_Index").Value = ""
            End If
        End If
    End Sub

    Private Function CheckLocation(ByVal pstrLocation_Alias As String) As Boolean
        Try
            Dim oms_Location As New ms_Location(ms_Location.enuOperation_Type.SEARCH)

            Dim strTo_Location_Index As String = oms_Location.getLocation_Index(pstrLocation_Alias).ToString

            If strTo_Location_Index = "" Then
                W_MSG_Information("ไม่พบตำแหน่ง  " & pstrLocation_Alias & " นี้ในฐานข้อมูล กรุณาป้อนตำแหน่งใหม่อีกครั้ง")
                Return False
            End If

            Dim All_QTY As Double = 0
            Dim All_Weight As Double = 0
            Dim dtPutAway As New DataTable
            dtPutAway = grdAllocate_Qty.DataSource
            dtPutAway.AcceptChanges()
            Dim obj As Object
            Dim dr() As DataRow

            obj = dtPutAway.Compute(" sum(Qty_per_TAG) ", "Location_Alias='" & pstrLocation_Alias & "' ")
            dr = dtPutAway.Select("Location_Alias='" & pstrLocation_Alias & "'")
            If dtPutAway.Rows.Count > 1 Then
                All_QTY = dtPutAway.Compute(" sum(Qty_per_TAG) ", " Location_Alias='" & pstrLocation_Alias & "' ")
                'If oms_Location.isOverFlow_Qty(pstrLocation_Alias, Val(grdAllocate_Qty.CurrentRow.Cells("Location_Qty").Value.ToString)) = True Then
                If oms_Location.isOverFlow_Qty(pstrLocation_Alias, All_QTY) = True Then
                    W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " พื้นที่ว่างไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                    grdAllocate_Qty.CurrentRow.Cells("Location_Index").Value = ""
                    Return False
                End If

                All_Weight = dtPutAway.Compute(" sum(Weight_Per_Tag) ", " Location_Alias='" & pstrLocation_Alias & "' ")
                'If oms_Location.isOverFlow_Weight(pstrLocation_Alias, Val(grdAllocate_Qty.CurrentRow.Cells("Col_weight").Value.ToString)) = True Then
                If oms_Location.isOverFlow_Weight(pstrLocation_Alias, All_Weight) = True Then
                    W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " น้ำหนักที่จุได้ไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                    grdAllocate_Qty.CurrentRow.Cells("Location_Index").Value = ""
                    Return False
                End If
                Return True
            Else
                If oms_Location.isOverFlow_Qty(pstrLocation_Alias, Val(grdAllocate_Qty.CurrentRow.Cells("Location_Qty").Value.ToString)) = True Then
                    W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " พื้นที่ว่างไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                    grdAllocate_Qty.CurrentRow.Cells("Location_Index").Value = ""
                    Return False
                End If
                If oms_Location.isOverFlow_Weight(pstrLocation_Alias, Val(grdAllocate_Qty.CurrentRow.Cells("Col_weight").Value.ToString)) = True Then
                    W_MSG_Information("ตำแหน่ง " & pstrLocation_Alias & " น้ำหนักที่จุได้ไม่เพียงพอ " & vbNewLine & " กรุณาป้อนตำแหน่งใหม่")
                    grdAllocate_Qty.CurrentRow.Cells("Location_Index").Value = ""
                    Return False
                End If
                Return True
            End If
            Return True
        Catch ex As Exception
            Throw ex
        End Try
    End Function

    Private Sub btnAutoPutAway_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAutoPutAway.Click

        Try
            'If grdAllocate_Qty.RowCount = 0 Then
            '    Exit Sub
            'End If

            'Dim _USE_Suggest_By_Config As Boolean = False
            'Dim oconfig As New config_CustomSetting
            ''0. Get การตั้งค่า ว่าจะใช้พื้นที่ว่างเท่านั้นหรือไม่ 
            ''   Get เงื่อนไขที่จะไม่อนุญาติใน Ms_Location
            '_USE_Suggest_By_Config = oconfig.getConfig_Key_USE("USE_Suggest_By_Config")

            'If _USE_Suggest_By_Config = False Then
            '    Dim dtPutAway As New DataTable
            '    dtPutAway = grdAllocate_Qty.DataSource
            '    Dim oPutAway As New PutAway(PutAway.enuOperation_Type.SEACH, dtPutAway)
            '    dtPutAway = oPutAway.AutoPutAway()
            'Else
            '    If grdAllocate_Qty.RowCount = 0 Then
            '        Exit Sub
            '    End If

            '    Suggest_Location()
            'End If

            Dim objPutawaySuggestLocation As New PutawaySuggestLocation
            Dim dtPutAway As New DataTable
            dtPutAway = grdAllocate_Qty.DataSource
            If dtPutAway.Rows.Count = 0 Then Exit Sub

            If dtPutAway.Columns.Contains("Location_Alias") = False Then
                dtPutAway.Columns.Add("Location_Alias", GetType(String))
            End If

            For Each dr As DataRow In dtPutAway.Rows
                dr("Location_Alias") = objPutawaySuggestLocation.Suggest_Location(dr("Tag_Index").ToString, dr("Sku_Index").ToString, dr("ProductType_Index").ToString, dr("ItemStatus_Index").ToString, dr("DocumentType_Index").ToString, dr("Customer_Index").ToString, _
                                                                                dr("Total_Qty").ToString, dr("Weight_per_TAG").ToString, dr("Volume_per_TAG").ToString)
            Next






        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Sub Suggest_Location()
        Try

            Dim AutoPutAway As New PutAway_Pallet(PutAway_Pallet.enuOperation_Type.SEACH)
            'Dim AutoPutAway As New bl_PutAway_Pallet(bl_PutAway_Pallet.enuOperation_Type.SEACH)

            'GetTAG()
            Dim DT_TAG As New DataTable
            DT_TAG = grdAllocate_Qty.DataSource

            Dim i As Integer = 1
            For Each drtag As DataRow In DT_TAG.Rows
                drtag("Location_Alias") = AutoPutAway.AutoPutAway_SingleLocation( _
                                                                    drtag("TAG_NO"), _
                                                                    drtag("Customer_Index"), _
                                                                    drtag("ProductType_Index"), _
                                                                    drtag("DocumentType_Index"), _
                                                                    drtag("ItemStatus_Index"), _
                                                                    drtag("Plot"), _
                                                                    CDbl(drtag("QTY_Per_Tag")), _
                                                                    0, 0, False, "", drtag("SKU_Index"))

            Next

            grdAllocate_Qty.DataSource = DT_TAG

        Catch ex As Exception
            Throw ex
        End Try
    End Sub
    Private Sub btnPrintReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintReport.Click
        Try
            Dim frm As New WMS_STD_INB_Report.frmReportViewerMain
            Dim oReport As New WMS_STD_INB_Report.Loading_Report("ORD_PrintOut", "And Order_Index ='" & _Order_Index & "'")
            frm.CrystalReportViewer1.ReportSource = oReport.LoadReport()
            frm.ShowDialog()
            'Dim oconfig_Report As New config_Report
            'Dim frm As New frmReportJobOrder
            'frm.CrystalReportViewer1.ReportSource = oconfig_Report.GetReportInfo("ORD_PrintOut", "And Order_Index ='" & Me.Order_Index & "'")
            'frm.ShowDialog()
            'Dim frm As New frmReport
            'frm.Document_Index = Order_Index
            'frm.Report_Id = 0
            'frm.ShowDialog()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub txtLocation_Begin_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation_Begin.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtLocation_Begin, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtLocation_End_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation_End.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtLocation_End, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenLocation.Click
        Try

            Select Case True
                Case radLocation_All.Checked
                    genLocation_All()
                Case radLocation_Range.Checked
                    genLocation_Range(CInt(txtLocation_Begin.Text), CInt(txtLocation_End.Text))
                Case radLocation_Next.Checked
                    genLocation_Next(CInt(txtLocation_Next.Text))
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub genLocation_All()
        Try
            If grdAllocate_Qty.RowCount = 0 Then Exit Sub
            For i As Integer = 0 To grdAllocate_Qty.Rows.Count - 1
                ' Gen Location
                Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value = txtLocation_All.Text
                ' Check Location
                If Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value Is Nothing Then Exit Sub
                If Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString.Trim = "" Then Exit Sub
                If CheckLocation(Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString.Trim) = False Then
                    Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value = ""
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub genLocation_Range(ByVal intRow_Begin As Integer, ByVal intRow_End As Integer)
        Try
            If grdAllocate_Qty.RowCount = 0 Then Exit Sub

            If (CInt(txtLocation_End.Text)) > grdAllocate_Qty.Rows.Count Then
                W_MSG_Information("ช่วงของตำแหน่งผิด")
                Exit Sub
            End If

            Dim i As Integer = 0
            For i = (intRow_Begin - 1) To (intRow_End - 1)
                ' Gen Location
                Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value = txtLocation_All.Text
                ' Check Location
                If Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value Is Nothing Then Exit Sub
                If Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString.Trim = "" Then Exit Sub
                If CheckLocation(Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString.Trim) = False Then
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Private Sub genLocation_Next(ByVal intNumRow_Next As Integer)
        Try
            If grdAllocate_Qty.RowCount = 0 Then Exit Sub
            Dim intNextRow As Integer = 0
            intNextRow = CInt(txtLocation_Next.Text) + (grdAllocate_Qty.CurrentRow.Index + 1)
            If (intNextRow) > grdAllocate_Qty.Rows.Count Then
                W_MSG_Information("ช่วงของตำแหน่งมากเกินไป")
                Exit Sub
            End If

            Dim i As Integer = 0

            Dim intCurrentRow As Integer = grdAllocate_Qty.CurrentRow.Index
            intNextRow = (grdAllocate_Qty.CurrentRow.Index) + intNumRow_Next

            For i = (intCurrentRow) To (intNextRow - 1)
                ' Gen Location
                Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value = txtLocation_All.Text
                ' Check Location
                If Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value Is Nothing Then Exit Sub
                If Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString.Trim = "" Then Exit Sub
                If CheckLocation(Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value.ToString.Trim) = False Then
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnClearLocation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearLocation.Click
        Try
            If grdAllocate_Qty.RowCount = 0 Then Exit Sub
            For i As Integer = 0 To grdAllocate_Qty.Rows.Count - 1
                Me.grdAllocate_Qty.Rows(i).Cells("Location_Index").Value = ""
            Next
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub txtLocation_Next_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLocation_Next.KeyPress
        Try
            e.Handled = CurrencyTextBox.NumbericOnly(txtLocation_Next, e.KeyChar)
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub frmPutawayWithTAG_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.Control AndAlso (e.Shift AndAlso (e.KeyCode = Keys.C)) Then
            Dim oconfig As New WMS_STD_Formula.config_CustomSetting()
            If oconfig.getConfig_Key_USE("USE_WINDOWNS_CONFIG") Then
                Dim frm As New WMS_STD_CONFIGURATION.frmConfigReceive
                frm.ShowDialog()
                ' ------ Set Language Begin ------
                Dim oFunction As New W_Language
                oFunction.SwitchLanguage(Me, 1)
                oFunction.SW_Language_Column(Me, Me.grdAllocate_Qty, 1)
                ' ------ Set Language End ------
            End If
        End If
    End Sub
End Class