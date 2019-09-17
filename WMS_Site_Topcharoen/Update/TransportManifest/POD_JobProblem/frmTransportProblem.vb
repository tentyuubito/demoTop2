Imports WMS_STD_Master_DataLayer
Imports WMS_STD_Master.W_Language
Imports WMS_STD_master.CurrencyTextBox
Imports WMS_STD_OUTB_Transport_Datalayer
Imports WMS_STD_Master
Public Class frmTransportProblem


    Private _TransportManifestItem_Index As String = ""
    Public Problem_Status As Boolean = False


    Public Property TransportManifestItem_Index() As String
        Get
            Return _TransportManifestItem_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifestItem_Index = Value
        End Set
    End Property

    Private _TransportManifest_Index As String = ""
    Public Property TransportManifest_Index() As String
        Get
            Return _TransportManifest_Index
        End Get
        Set(ByVal Value As String)
            _TransportManifest_Index = Value
        End Set
    End Property

    Private _SalesOrder_Index As String = ""
    Public Property SalesOrder_Index() As String
        Get
            Return _SalesOrder_Index
        End Get
        Set(ByVal Value As String)
            _SalesOrder_Index = Value
        End Set
    End Property
    Private _IsSubManifest As Integer = 0

    Public Property IsSubManifest() As Integer
        Get
            Return _IsSubManifest
        End Get
        Set(ByVal value As Integer)
            _IsSubManifest = value
        End Set
    End Property


    Private _Status_Manifest As Integer
    Public Property Status_Manifest() As Integer
        Get
            Return _Status_Manifest
        End Get
        Set(ByVal value As Integer)
            _Status_Manifest = value
        End Set
    End Property


    Private objStatus As WithDraw_Mode

    Public Enum WithDraw_Mode
        ADD
        EDIT
        NULL
    End Enum

    Public Sub New(ByVal Operation_Type As WithDraw_Mode)
        MyBase.New()
        'This call is required by the Windows Form Designer.
        InitializeComponent()
        'Add any initialization after the InitializeComponent() call
        objStatus = Operation_Type

    End Sub

    Private Sub frmTransportProblem_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Dim oFunction As New WMS_STD_Master.W_Language
            'oFunction.Insert(Me)
            oFunction.SwitchLanguage(Me)

            Me.AddCurrencyTextBox()
            Me.getJobSolution()
            Me.getJobProblem()
            Me.getResponsibleParty()
            Me.getComboSalesOrder_Status()

            Me.getGrdProblemDetail()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try

    End Sub
    Private Sub getComboSalesOrder_Status()

        Dim objDBType As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
        Dim objDTType As DataTable = New DataTable

        Try

            objDBType.getDeliveryResult_Status()
            objDTType = objDBType.DataTable
            With cboStatusManifest
                .DataSource = objDTType
                .DisplayMember = "Description"
                .ValueMember = "Status"

            End With
            '   cboSalesOrder_Status.ValueMember = objDTType.Rows(3).Item("SalesOrder_Status").ToString
        Catch ex As Exception
            Throw ex
        Finally
            objDBType = Nothing
            objDTType = Nothing

        End Try
    End Sub

    Sub AddCurrencyTextBox()
        Try
            'AddHandler Me.txtDriverPaidAmountAmount.KeyPress, AddressOf Me.keypressed

        Catch ex As Exception
            Throw ex
        End Try
    End Sub

    Public Sub keypressed(ByVal o As [Object], ByVal e As KeyPressEventArgs)
        e.Handled = CurrencyOnly(o, e.KeyChar)
    End Sub

    Private Sub getResponsibleParty()
        Dim objClassDB As New ms_ResponsibleParty(ms_ResponsibleParty.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            objClassDB.getProblemSolution_Process(18)
            objDT = objClassDB.DataTable

            With cboResponsibleParty
                .DisplayMember = "Description"
                .ValueMember = "ResponsibleParty_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getJobSolution()
        Dim objClassDB As New ms_JobProblem(ms_JobSolution.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            'objClassDB.getProblem_Process(18)
            objClassDB.getProblemSolution_Process(23)
            objDT = objClassDB.DataTable

            With cboJobSolution
                .DisplayMember = "Description"
                .ValueMember = "JobSolution_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub

    Private Sub getJobProblem()
        Dim objClassDB As New ms_JobSolution(ms_JobProblem.enuOperation_Type.SEARCH)
        Dim objDT As DataTable = New DataTable

        Try
            'objClassDB.getProblemSolution_Process(18)
            objClassDB.getProblem_Process(23)
            objDT = objClassDB.DataTable

            With cboJobProblem
                .DisplayMember = "Description"
                .ValueMember = "JobProblem_Index"
                .DataSource = objDT
            End With

        Catch ex As Exception
            Throw ex
        Finally
            objClassDB = Nothing
            objDT = Nothing
        End Try
    End Sub


    Private Sub getGrdProblemDetail()
        Try
            Dim objtbManifest As New tb_TransportManifest(tb_TransportManifest.enuOperation_Type.SEARCH)
            Dim dtManifest As New DataTable
            objtbManifest.getTransportManifest_OnTruck(" AND TransportManifestItem_Index='" & TransportManifestItem_Index & "'", _IsSubManifest, "")
            dtManifest = objtbManifest.DataTable
            For Each drItem As DataRow In dtManifest.Rows
                Me.txtTransportManifestNo.Text = drItem("TransportManifest_No").ToString
                Me.txtVehicleID.Text = drItem("Vehicle_Id").ToString
                Me.txtSO_No.Text = drItem("SalesOrder_No").ToString
                Me.txtCustomer_Id.Text = drItem("Customer_Id").ToString
                Me.txtCustomer_Name.Text = drItem("Customer_Name").ToString
                If drItem("JobProblem_Index").ToString <> "" Then
                    cboJobProblem.SelectedValue = drItem("JobProblem_Index").ToString
                End If
                Me.txtJobProblemDes.Text = drItem("JobProblem_Desc").ToString
                If drItem("ResponsibleParty_Index").ToString <> "" Then
                    cboResponsibleParty.SelectedValue = drItem("ResponsibleParty_Index").ToString
                End If
                If drItem("JobSolution_Index").ToString <> "" Then
                    cboJobSolution.SelectedValue = drItem("JobSolution_Index").ToString
                End If

                txtJobSolutionDes.Text = drItem("JobSolution_Desc").ToString
                If drItem("JobProblem_Index").ToString = "" Then
                    Problem_Status = False
                Else
                    Problem_Status = True
                End If

                Me.cboStatusManifest.SelectedValue = Me._Status_Manifest


                If drItem("isTransportCharged").ToString <> "" Then Me.chkTransportCharged.Checked = Not CBool(drItem("isTransportCharged"))
                If drItem("isTransportPaid").ToString <> "" Then Me.chkTransportPaid.Checked = Not CBool(drItem("isTransportPaid"))
                If drItem("IsDamage_Delivered").ToString <> "" Then Me.chkDamage_Delivered.Checked = CBool(drItem("IsDamage_Delivered"))
                If drItem("IsComplaint_Delivered").ToString <> "" Then Me.chkComplaint_Delivered.Checked = CBool(drItem("IsComplaint_Delivered"))

                'Me.txtDriverPaidAmountAmount.Text = drItem("DriverPaidAmount").ToString

            Next

        Catch ex As Exception
            Throw ex
        End Try
    End Sub


    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Try
            Me.Close()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try
            'If Not IsNumeric(txtDriverPaidAmountAmount.Text) Then txtDriverPaidAmountAmount.Text = 0
            'If chkDriverPaidAmount.Checked Then
            '    If CDbl(txtDriverPaidAmountAmount.Text) <= 0 Then
            '        W_MSG_Information("กรุณาระบุ " & chkDriverPaidAmount.Text & " มากกว่า 0")
            '        Exit Sub
            '    End If

            'End If

            If (cboJobProblem.SelectedValue Is Nothing) Or (cboJobSolution.SelectedValue Is Nothing) Or (cboResponsibleParty.SelectedValue Is Nothing) Then
                W_MSG_Information_ByIndex(46)
                Exit Sub
            End If
            Dim objTransportManifestItem As New tb_TransportManifestItem(tb_TransportManifestItem.enuOperation_Type.UPDATE)
            objTransportManifestItem.TransportManifestItem_Index = Me._TransportManifestItem_Index
            objTransportManifestItem.TransportManifest_Index = Me._TransportManifest_Index
            objTransportManifestItem.JobProblem_Index = cboJobProblem.SelectedValue.ToString
            objTransportManifestItem.JobProblem_Desc = txtJobProblemDes.Text
            objTransportManifestItem.ResponsibleParty_Index = cboResponsibleParty.SelectedValue.ToString
            objTransportManifestItem.JobSolution_Index = cboJobSolution.SelectedValue.ToString
            objTransportManifestItem.JobSolution_Desc = txtJobSolutionDes.Text
            objTransportManifestItem.SalesOrder_Index = Me._SalesOrder_Index

            objTransportManifestItem.IsTransportCharged = Not Me.chkTransportCharged.Checked
            objTransportManifestItem.IsTransportPaid = Not Me.chkTransportPaid.Checked

            objTransportManifestItem.IsDamage_Delivered = Me.chkDamage_Delivered.Checked
            objTransportManifestItem.IsComplaint_Delivered = Me.chkComplaint_Delivered.Checked

            objTransportManifestItem.Status = Me.cboStatusManifest.SelectedValue 'Keep Status Manifest
            objTransportManifestItem.chk_Problem = True
            Dim objSaveTransportManifestItem As New tb_TransportManifestItem(tb_TransportManifestItem.enuOperation_Type.ADDNEW, objTransportManifestItem)


            'So Satus Use: Manifest Status save temp
            'If chkDriverPaidAmount.Checked Then PaneltyFeeInfoType(2) = 1
            'If Not IsNumeric(txtDriverPaidAmountAmount.Text) Then
            '    txtDriverPaidAmountAmount.Text = 0
            'End If
            If objSaveTransportManifestItem.Update_JobPrblam() <> "" Then
                Problem_Status = True
                W_MSG_Information_ByIndex(1)
                Me.Close()
            Else
                Problem_Status = False
                W_MSG_Information_ByIndex(2)
            End If
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub



    Private Sub btnJobProblem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJobProblem.Click
        Try
            Dim frm As New frmMainJobProblem
            frm.Process_id = 23
            frm.ShowDialog()
            Me.getJobProblem()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub

    Private Sub btnJobSolution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnJobSolution.Click
        Try
            Dim frm As New frmMainJobSolution
            frm.Process_id = 23
            frm.ShowDialog()
            Me.getJobSolution()
        Catch ex As Exception
            W_MSG_Error(ex.Message)
        End Try
    End Sub
End Class