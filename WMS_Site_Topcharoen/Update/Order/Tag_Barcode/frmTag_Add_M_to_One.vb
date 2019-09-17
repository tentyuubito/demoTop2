Imports WMS_STD_Master.W_Language
Imports WMS_STD_Formula
Imports WMS_STD_Master
Imports WMS_STD_master.W_Function
Imports WMS_STD_INB_Receive_Datalayer

Public Class frmTag_Add_M_to_One
    Private _Tag_No As String
    Public Property Tag_No() As String
        Get
            Return _Tag_No
        End Get
        Set(ByVal value As String)
            _Tag_No = value
        End Set
    End Property

    Private _Order_Index As String
    Public Property Order_index() As String
        Get
            Return _Order_Index
        End Get
        Set(ByVal value As String)
            _Order_Index = value
        End Set
    End Property

    Private _OrderItem_Index As String
    Public Property OrderItem_Index() As String
        Get
            Return _OrderItem_Index
        End Get
        Set(ByVal value As String)
            _OrderItem_Index = value
        End Set
    End Property

    Public Sub New(ByVal Tag_No_New As String, ByVal Order_Index_new As String, ByVal OrderItem_Index As String)
        MyBase.New()
        InitializeComponent()
        _Tag_No = Tag_No_New
        _Order_Index = Order_Index_new
        _OrderItem_Index = OrderItem_Index
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private _boolComplete As Boolean = False
    Public ReadOnly Property boolComplete() As Boolean
        Get
            Return _boolComplete
        End Get
    End Property

    Private _IsNew As Boolean = False
    Public Property IsNew() As Boolean
        Get
            Return _IsNew
        End Get
        Set(ByVal value As Boolean)
            _IsNew = value
        End Set
    End Property

    Private _GuID As String = ""
    Public Property GuID() As String
        Get
            Return _GuID
        End Get
        Set(ByVal value As String)
            _GuID = value
        End Set
    End Property

    Private Sub frmTag_Add_M_to_One_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            If Me._Tag_No = "" Then
                Me.txtTag_No.Visible = False
                Me.lblTag_No.Visible = False
            End If
            Me.grdOrderItem.AutoGenerateColumns = False
            Dim objTAG As New tb_TAG(tb_TAG.enuOperation_Type.SEARCH)
            'objTAG.getOrderDetail_Tag(" AND Qty > 0 AND Order_Index = '" & _Order_Index & "'")
            objTAG.getOrderDetail_Tag(" AND Qty > 0 AND OrderItem_Index IN ( " & _OrderItem_Index & " )")
            Me.grdOrderItem.DataSource = objTAG.DataTable
            Me.chkAllOrder.Checked = True
            Me.txtTag_No.Text = _Tag_No
            Me.grdOrderItem.ReadOnly = False
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub chkAllOrder_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkAllOrder.CheckedChanged
        Try
            If Me.grdOrderItem.Rows.Count > 0 Then
                Dim Dttmp As New DataTable
                Dttmp = CType(Me.grdOrderItem.DataSource, DataTable)
                If Me.chkAllOrder.Checked = True Then
                    For Each Dr As DataRow In Dttmp.Rows
                        Dr("chkSelect") = 1
                    Next
                Else
                    For Each Dr As DataRow In Dttmp.Rows
                        Dr("chkSelect") = 0
                    Next
                End If
            End If
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btn_OKManualTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_OKManualTag.Click
        Try
            _IsNew = True
            AddTag()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub btn_ADDManualTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_ADDManualTag.Click
        Try
            _IsNew = False
            AddTag()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Private Sub AddTag()
        Dim objClassDB As New WMS_STD_Master_Datalayer.ms_SKU( _
                                                        WMS_STD_Master_Datalayer.ms_SKU.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable
        Try
            Dim dr() As DataRow = CType(Me.grdOrderItem.DataSource, DataTable).Select(" chkSelect = 1 ")
            For Each drI As DataRow In dr
                If drI("Qty_Order") > drI("Qty") Then
                    W_MSG_Information("จำนวนเกินกว่าจำนวนที่สร้างได้")
                    Exit Sub
                End If
            Next

            Dim objDBTempIndex As New Sy_AutoNumber
            Dim objAutoNumber As New Sy_AutoNumber
            Dim objItemCollection As New List(Of tb_TAG_Update)

            If Me._Tag_No = "" Then
                If _IsNew Then
                    Me._Tag_No = objAutoNumber.getSys_Value("TAG_NO")
                Else
                    Me._Tag_No = objAutoNumber.getSys_Value("TAG_NO_TEMP")
                    If Me._Tag_No <> "" Then
                        Me._Tag_No = "T" + Me._Tag_No.Substring(1, Me._Tag_No.Length - 1)
                    End If
                End If

            End If

            For Each DrOrderItem As DataRow In dr
                Dim otb_TAG As New tb_TAG_Update(tb_TAG_Update.enuOperation_Type.ADDNEW)
                otb_TAG = Me.SetTagItem(DrOrderItem, otb_TAG)

                'objClassDB.getSKU_Detail(DrOrderItem("Sku_Id"))
                'objDT = objClassDB.DataTable

                'Assing ค่าให้ TAG
                With otb_TAG
                    .TAG_Index = objDBTempIndex.getSys_Value("TAG_Index")
                    .TAG_No = Me._Tag_No
                    .Qty_per_TAG = DrOrderItem("Qty_Order")
                    '.Weight_per_TAG = .Weight / .Qty
                    '.Volume_per_TAG = .Volume / .Qty

                    ''เบด 20160401T914
                    '.Weight_per_TAG = .Qty_per_TAG * CDbl(objDT.Rows(0).Item("UnitWeight").ToString)
                    '.Volume_per_TAG = CDbl(objDT.Rows(0).Item("Unit_Volume").ToString) * .Qty_per_TAG

                    .Weight_per_TAG = DrOrderItem("Qty_Order") * (CDbl(DrOrderItem("Weight")) / CDbl(DrOrderItem("Qty_Diff")))
                    .Volume_per_TAG = DrOrderItem("Qty_Order") * (CDbl(DrOrderItem("Volume")) / CDbl(DrOrderItem("Qty_Diff")))

                    .Ref_No3 = ""
                End With

                objItemCollection.Add(otb_TAG)
            Next

            If objItemCollection.Count > 0 Then
                Dim objItemA As New tb_TAG_Update(tb_TAG_Update.enuOperation_Type.ADDNEW)
                objItemA.objItemCollection = objItemCollection
                objItemA.InsertData()
                objDBTempIndex = Nothing
                objAutoNumber = Nothing
            End If

            Me._boolComplete = True
            Me.Close()

        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub

    Public Function SetTagItem(ByVal podrOrderItem As DataRow, ByVal poTagItem As tb_TAG_Update) As tb_TAG_Update
        Try
            With podrOrderItem
                poTagItem.Order_No = .Item("Order_No").ToString
                'poTagItem.Order_Index = Me._Order_Index
                poTagItem.Order_Index = .Item("Order_Index").ToString
                poTagItem.Order_Date = .Item("Order_Date").ToString
                poTagItem.Order_Time = .Item("Order_Time").ToString
                poTagItem.Customer_Index = .Item("Customer_Index").ToString
                If .Item("Supplier_Index").ToString IsNot Nothing Then
                    poTagItem.Supplier_Index = .Item("Supplier_Index").ToString
                Else
                    poTagItem.Supplier_Index = ""
                End If
                If .Item("OrderItem_Index").ToString IsNot Nothing Then
                    poTagItem.OrderItem_Index = .Item("OrderItem_Index").ToString
                Else
                    poTagItem.OrderItem_Index = ""
                End If
                poTagItem.OrderItemLocation_Index = ""
                If .Item("Sku_Index").ToString IsNot Nothing Then
                    poTagItem.Sku_Index = .Item("Sku_Index").ToString
                Else
                    poTagItem.Sku_Index = ""
                End If
                If .Item("PLot").ToString IsNot Nothing Then
                    poTagItem.PLot = .Item("PLot").ToString
                Else
                    poTagItem.PLot = ""
                End If
                If .Item("ItemStatus_Index").ToString IsNot Nothing Then
                    poTagItem.ItemStatus_Index = .Item("ItemStatus_Index").ToString
                Else
                    poTagItem.ItemStatus_Index = ""
                End If
                If .Item("Package_Index").ToString IsNot Nothing Then
                    poTagItem.Package_Index = .Item("Package_Index").ToString
                Else
                    poTagItem.Package_Index = ""
                End If
                poTagItem.Unit_Weight = 0
                poTagItem.Size_Index = -1
                If .Item("PalletType_Index").ToString IsNot Nothing Then
                    poTagItem.Pallet_No = .Item("PalletType_Index").ToString
                Else
                    poTagItem.Pallet_No = ""
                End If
                poTagItem.TAG_Status = 0
                If .Item("str1").ToString IsNot Nothing Then
                    poTagItem.Ref_No1 = .Item("str1").ToString
                Else
                    poTagItem.Ref_No1 = ""
                End If

                If .Item("str2").ToString IsNot Nothing Then
                    poTagItem.Ref_No2 = .Item("str2").ToString
                Else
                    poTagItem.Ref_No2 = ""
                End If
                If .Item("Qty").ToString IsNot Nothing Then
                    'เบด 18/07/2556
                    poTagItem.Qty = .Item("Qty_Diff").ToString 'Qty
                Else
                    poTagItem.Qty = 0
                End If
                If .Item("Weight").ToString IsNot Nothing Then
                    poTagItem.Weight = .Item("Weight").ToString
                Else
                    poTagItem.Weight = 0
                End If
                If .Item("Volume").ToString IsNot Nothing Then
                    poTagItem.Volume = .Item("Volume").ToString
                Else
                    poTagItem.Volume = 0
                End If

                If String.IsNullOrEmpty(.Item("ERP_Location").ToString) = False Then
                    poTagItem.ERP_Location = .Item("ERP_Location").ToString
                Else
                    poTagItem.ERP_Location = ""
                End If

                poTagItem.IsTemporaryTAG = Not _IsNew
                poTagItem.GuID = _GuID

            End With

            poTagItem.Qty_per_TAG = poTagItem.Qty
            poTagItem.Weight_per_TAG = poTagItem.Weight
            poTagItem.Volume_per_TAG = poTagItem.Volume

            poTagItem.Ref_No3 = "1/1"
            Return poTagItem
        Catch ex As Exception
            Throw ex
        End Try
    End Function
    Private Sub btnCancelManualTag_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelManualTag.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.ToString)
        End Try
    End Sub


    Private Sub txtEdit_Keypress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) ' Handles grdOrderItem.KeyPress
        Try
            Console.WriteLine("KeyPress " & e.KeyChar.ToString())

            ' 1 Int , 2 Double
            Dim Column_Index As String = grdOrderItem.CurrentCell.ColumnIndex
            Select Case Column_Index
                Case Is = grdOrderItem.Columns("Col_QtyOrder").Index
                    e.Handled = Check_GrdKeyPress(0, e, Column_Index)
            End Select
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Function Check_GrdKeyPress(ByVal format As Integer, ByVal e As System.Windows.Forms.KeyPressEventArgs, ByVal Column_Index As Integer) As Boolean
        Try
            Select Case format
                Case 0
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Or e.KeyChar = "." Then
                        If e.KeyChar = "." Then
                            If grdOrderItem.CurrentRow.Cells(Column_Index).EditedFormattedValue <> "" Then
                                Dim strData As String = grdOrderItem.CurrentRow.Cells(Column_Index).EditedFormattedValue
                                If strData.IndexOf(".") >= 0 Then Return True
                            Else
                                Return True
                            End If
                        Else
                            Return False
                        End If
                    Else
                        Return True
                    End If
                Case 1
                    If Char.IsNumber(e.KeyChar) Or e.KeyChar = ChrW(Keys.Back) Then
                        Return False
                    Else
                        Return True
                    End If
            End Select

        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Function

    Private Sub grdOrderItem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles grdOrderItem.EditingControlShowing
        Try
            Dim strName As String = grdOrderItem.Columns(grdOrderItem.CurrentCell.ColumnIndex).Name
            If (strName = "Col_QtyOrder") Then
                Dim txtEdit As TextBox = e.Control
                RemoveHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
                AddHandler txtEdit.KeyPress, AddressOf txtEdit_Keypress
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


End Class