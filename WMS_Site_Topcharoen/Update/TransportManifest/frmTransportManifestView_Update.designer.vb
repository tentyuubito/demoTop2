<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmTransportManifestView_Update
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTransportManifestView_Update))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Me.pnLeftmenu = New System.Windows.Forms.Panel
        Me.gbFilter = New System.Windows.Forms.GroupBox
        Me.grbPrintReport = New System.Windows.Forms.GroupBox
        Me.cboPrint = New System.Windows.Forms.ComboBox
        Me.btnPrint = New System.Windows.Forms.Button
        Me.cboDocumentType = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.cboVehicleType = New System.Windows.Forms.ComboBox
        Me.lblVehicleType = New System.Windows.Forms.Label
        Me.cboDistributionCenter = New System.Windows.Forms.ComboBox
        Me.lblDistributionCenter = New System.Windows.Forms.Label
        Me.cboSubRoute = New System.Windows.Forms.ComboBox
        Me.lblSubRoute = New System.Windows.Forms.Label
        Me.cboRoute = New System.Windows.Forms.ComboBox
        Me.lblTransportJobType = New System.Windows.Forms.Label
        Me.lblRoute = New System.Windows.Forms.Label
        Me.cboTransportJobType = New System.Windows.Forms.ComboBox
        Me.gbCondition = New System.Windows.Forms.GroupBox
        Me.rdoDriver = New System.Windows.Forms.RadioButton
        Me.btnPop_Search = New System.Windows.Forms.Button
        Me.rdoContainerNo = New System.Windows.Forms.RadioButton
        Me.rdoVehicleNo = New System.Windows.Forms.RadioButton
        Me.rdoTransportManifest_No = New System.Windows.Forms.RadioButton
        Me.rdoCustomerShipping = New System.Windows.Forms.RadioButton
        Me.dtpDate = New System.Windows.Forms.DateTimePicker
        Me.lb_to = New System.Windows.Forms.Label
        Me.dateEnd = New System.Windows.Forms.DateTimePicker
        Me.txtKeySearch = New System.Windows.Forms.TextBox
        Me.btnSearch = New System.Windows.Forms.Button
        Me.rdbCustomer = New System.Windows.Forms.RadioButton
        Me.rdbTransportManifest_Date = New System.Windows.Forms.RadioButton
        Me.cboDocumentStatus = New System.Windows.Forms.ComboBox
        Me.lblStatus = New System.Windows.Forms.Label
        Me.tabTransportManifest = New System.Windows.Forms.TabControl
        Me.tbpJobLoading = New System.Windows.Forms.TabPage
        Me.SplitContainer4 = New System.Windows.Forms.SplitContainer
        Me.chkSelectAll = New System.Windows.Forms.CheckBox
        Me.grdJobLoading = New System.Windows.Forms.DataGridView
        Me.chkItem_JobLoading = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_Index_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_No_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DocumentType_desc = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_No_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_ID_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Driver_Name_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Route_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SubRoute_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportJobType_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sum_Volume_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sum_Weight_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_VehicleType_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_CustomerShipping_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_CustomerShippingLocation_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Sum_Qty_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Count_Document_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ContainerNo1_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ContainerNo2_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Ref1_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Comment_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Add_By_JobLoading = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel4 = New System.Windows.Forms.Panel
        Me.lbCountRows = New System.Windows.Forms.Label
        Me.btnAddManifest = New System.Windows.Forms.Button
        Me.btnEditManifest = New System.Windows.Forms.Button
        Me.btnCopy = New System.Windows.Forms.Button
        Me.btnReleaseTruck = New System.Windows.Forms.Button
        Me.lblSumWeight_JobLoading = New System.Windows.Forms.Label
        Me.txtSumVolume_JobLoading = New System.Windows.Forms.TextBox
        Me.txtSumWeight_JobLoading = New System.Windows.Forms.TextBox
        Me.txtSumQty_JobLoading = New System.Windows.Forms.TextBox
        Me.lblSumDoc_JobLoading = New System.Windows.Forms.Label
        Me.lblSumAll = New System.Windows.Forms.Label
        Me.txtSumDoc_JobLoading = New System.Windows.Forms.TextBox
        Me.lblSumVolume_JobLoading = New System.Windows.Forms.Label
        Me.lblSumQty_JobLoading = New System.Windows.Forms.Label
        Me.tbpJobInTransport = New System.Windows.Forms.TabPage
        Me.SplitContainer3 = New System.Windows.Forms.SplitContainer
        Me.chkSelectAll1 = New System.Windows.Forms.CheckBox
        Me.grdJobInTransport = New System.Windows.Forms.DataGridView
        Me.chkSelect_JobInTransport = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_TransportManifest_No_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_SourceOutGate_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_DestinationInGate_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_ReturnTruckInGate_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_Id_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_No_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Route_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SubRoute_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_VehicleType_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Driver_Name_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_Shipping_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ShippingLocation_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Bill_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_weight_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Volume_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Container_No1_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Container_No2_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportJopType_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Ref_No_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Comment_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Add_by_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_Id = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Index_JobInTransport = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel3 = New System.Windows.Forms.Panel
        Me.lbCountRows2 = New System.Windows.Forms.Label
        Me.btnEditJobInTransport = New System.Windows.Forms.Button
        Me.lblSumVolume_JobInTransport = New System.Windows.Forms.Label
        Me.btnReceiveDestination = New System.Windows.Forms.Button
        Me.txtSumVolume_JobInTransport = New System.Windows.Forms.TextBox
        Me.btnReturn = New System.Windows.Forms.Button
        Me.lblSumDoc_JobInTransport = New System.Windows.Forms.Label
        Me.txtSumWeight_JobInTransport = New System.Windows.Forms.TextBox
        Me.txtSumDoc_JobInTransport = New System.Windows.Forms.TextBox
        Me.txtSumQty_JobInTransport = New System.Windows.Forms.TextBox
        Me.lblSumQty_JobInTransport = New System.Windows.Forms.Label
        Me.Label7 = New System.Windows.Forms.Label
        Me.lblSumWeight_JobInTransport = New System.Windows.Forms.Label
        Me.tbpJobReturn = New System.Windows.Forms.TabPage
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer
        Me.grdJobReturn = New System.Windows.Forms.DataGridView
        Me.chkSelect_JobReturn = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.col_TransportManifest_No_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_SourceOutGate_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_DestinationInGate_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_ReturnTruckInGate_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_Id_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_No_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Route_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_SubRoute_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_VehicleType_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Driver_Name_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_CustomerShipping_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ShippingLocation_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Bill_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Weight_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Volume_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Container_No2_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Container_No1_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportJoptype_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Ref_No_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Comment_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Add_By_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Col_status_Chk = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Index_JobReturn = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.Panel2 = New System.Windows.Forms.Panel
        Me.lbCountRows3 = New System.Windows.Forms.Label
        Me.lblSumDoc_JobReturn = New System.Windows.Forms.Label
        Me.btnEditJobReturn = New System.Windows.Forms.Button
        Me.txtSumDoc_JobReturn = New System.Windows.Forms.TextBox
        Me.lblSumWeight_JobReturn = New System.Windows.Forms.Label
        Me.btnConfirm = New System.Windows.Forms.Button
        Me.txtSumWeight_JobReturn = New System.Windows.Forms.TextBox
        Me.txtSumVolume_JobReturn = New System.Windows.Forms.TextBox
        Me.lblSumQty_JobReturn = New System.Windows.Forms.Label
        Me.txtSumQty_JobReturn = New System.Windows.Forms.TextBox
        Me.lbltxtSumVolume_JobReturn = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.tabManifestComplete = New System.Windows.Forms.TabPage
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer
        Me.grdJob_Complete = New System.Windows.Forms.DataGridView
        Me.col_TransportManifest_No_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportManifest_Date_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Index_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_SourceOutGate_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_DestinationInGate_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Time_ReturnTruckInGate_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_Id_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Vehicle_No_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_VehicleType_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Driver_Name_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_CustomerShipping_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_ShippingLocation_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Status_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Weight_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Volume_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Qty_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Bill_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Container_No2_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Container_No1_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_TransportJoptype_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Customer_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Route_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_DistributionCenter_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Ref_No_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Comment_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.col_Add_By_JobComplete = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.PanelOrderItemButton = New System.Windows.Forms.Panel
        Me.btnInterface = New System.Windows.Forms.Button
        Me.lblSumWeight_JobComplete = New System.Windows.Forms.Label
        Me.lbCountRows4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.lblSumDoc_JobComplete = New System.Windows.Forms.Label
        Me.txtSumWeight_JobComplete = New System.Windows.Forms.TextBox
        Me.txtSumDoc_JobComplete = New System.Windows.Forms.TextBox
        Me.txtSumQty_Jobcomplete = New System.Windows.Forms.TextBox
        Me.lblSumVolume_JobComplete = New System.Windows.Forms.Label
        Me.lblSumQty_JobComplete = New System.Windows.Forms.Label
        Me.txtSumVolume_JobComplete = New System.Windows.Forms.TextBox
        Me.Button1 = New System.Windows.Forms.Button
        Me.pnlHide = New System.Windows.Forms.Panel
        Me.DataGridViewCheckBoxColumn1 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn6 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn7 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn8 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn9 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn10 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn11 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn12 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn13 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn14 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn15 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn21 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn22 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn23 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn24 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn25 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewCheckBoxColumn2 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn26 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn27 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn28 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn29 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn30 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn31 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn32 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn33 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn34 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn35 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn36 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn37 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn38 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn39 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn40 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn41 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn42 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn43 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn44 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn45 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn46 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn47 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn48 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn49 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn50 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn51 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn52 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn53 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewCheckBoxColumn3 = New System.Windows.Forms.DataGridViewCheckBoxColumn
        Me.DataGridViewTextBoxColumn54 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn55 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn56 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn57 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn58 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn59 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn60 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn61 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn62 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn63 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn64 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn65 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn66 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn67 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn68 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn69 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn70 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn71 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn72 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn73 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn74 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn75 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn76 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn77 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn78 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn79 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn80 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn81 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn82 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn83 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn84 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn85 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn86 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn87 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn88 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn89 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn90 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn91 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn92 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn93 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn94 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn95 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn96 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn97 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn98 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn99 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn100 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn101 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn102 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn103 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn104 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn105 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn106 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.DataGridViewTextBoxColumn107 = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.pnLeftmenu.SuspendLayout()
        Me.gbFilter.SuspendLayout()
        Me.grbPrintReport.SuspendLayout()
        Me.gbCondition.SuspendLayout()
        Me.tabTransportManifest.SuspendLayout()
        Me.tbpJobLoading.SuspendLayout()
        Me.SplitContainer4.Panel1.SuspendLayout()
        Me.SplitContainer4.Panel2.SuspendLayout()
        Me.SplitContainer4.SuspendLayout()
        CType(Me.grdJobLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel4.SuspendLayout()
        Me.tbpJobInTransport.SuspendLayout()
        Me.SplitContainer3.Panel1.SuspendLayout()
        Me.SplitContainer3.Panel2.SuspendLayout()
        Me.SplitContainer3.SuspendLayout()
        CType(Me.grdJobInTransport, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel3.SuspendLayout()
        Me.tbpJobReturn.SuspendLayout()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        CType(Me.grdJobReturn, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.tabManifestComplete.SuspendLayout()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.grdJob_Complete, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.PanelOrderItemButton.SuspendLayout()
        Me.pnlHide.SuspendLayout()
        Me.SuspendLayout()
        '
        'pnLeftmenu
        '
        Me.pnLeftmenu.BackColor = System.Drawing.SystemColors.Control
        Me.pnLeftmenu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.pnLeftmenu.Controls.Add(Me.gbFilter)
        Me.pnLeftmenu.Controls.Add(Me.gbCondition)
        Me.pnLeftmenu.Location = New System.Drawing.Point(0, 9)
        Me.pnLeftmenu.Margin = New System.Windows.Forms.Padding(4)
        Me.pnLeftmenu.Name = "pnLeftmenu"
        Me.pnLeftmenu.Size = New System.Drawing.Size(191, 796)
        Me.pnLeftmenu.TabIndex = 15
        '
        'gbFilter
        '
        Me.gbFilter.Controls.Add(Me.grbPrintReport)
        Me.gbFilter.Controls.Add(Me.cboDocumentType)
        Me.gbFilter.Controls.Add(Me.Label1)
        Me.gbFilter.Controls.Add(Me.cboVehicleType)
        Me.gbFilter.Controls.Add(Me.lblVehicleType)
        Me.gbFilter.Controls.Add(Me.cboDistributionCenter)
        Me.gbFilter.Controls.Add(Me.lblDistributionCenter)
        Me.gbFilter.Controls.Add(Me.cboSubRoute)
        Me.gbFilter.Controls.Add(Me.lblSubRoute)
        Me.gbFilter.Controls.Add(Me.cboRoute)
        Me.gbFilter.Controls.Add(Me.lblTransportJobType)
        Me.gbFilter.Controls.Add(Me.lblRoute)
        Me.gbFilter.Controls.Add(Me.cboTransportJobType)
        Me.gbFilter.Location = New System.Drawing.Point(9, 369)
        Me.gbFilter.Margin = New System.Windows.Forms.Padding(4)
        Me.gbFilter.Name = "gbFilter"
        Me.gbFilter.Padding = New System.Windows.Forms.Padding(4)
        Me.gbFilter.Size = New System.Drawing.Size(175, 422)
        Me.gbFilter.TabIndex = 2
        Me.gbFilter.TabStop = False
        Me.gbFilter.Text = "กรองรายการ"
        '
        'grbPrintReport
        '
        Me.grbPrintReport.Controls.Add(Me.cboPrint)
        Me.grbPrintReport.Controls.Add(Me.btnPrint)
        Me.grbPrintReport.Location = New System.Drawing.Point(8, 315)
        Me.grbPrintReport.Margin = New System.Windows.Forms.Padding(4)
        Me.grbPrintReport.Name = "grbPrintReport"
        Me.grbPrintReport.Padding = New System.Windows.Forms.Padding(4)
        Me.grbPrintReport.Size = New System.Drawing.Size(167, 103)
        Me.grbPrintReport.TabIndex = 294
        Me.grbPrintReport.TabStop = False
        Me.grbPrintReport.Text = "พิมพ์เอกสาร"
        '
        'cboPrint
        '
        Me.cboPrint.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboPrint.FormattingEnabled = True
        Me.cboPrint.Items.AddRange(New Object() {"ใบสั่งงานนำสินค้าออก"})
        Me.cboPrint.Location = New System.Drawing.Point(4, 21)
        Me.cboPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.cboPrint.Name = "cboPrint"
        Me.cboPrint.Size = New System.Drawing.Size(151, 24)
        Me.cboPrint.TabIndex = 1
        '
        'btnPrint
        '
        Me.btnPrint.Image = CType(resources.GetObject("btnPrint.Image"), System.Drawing.Image)
        Me.btnPrint.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnPrint.Location = New System.Drawing.Point(4, 54)
        Me.btnPrint.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(143, 47)
        Me.btnPrint.TabIndex = 2
        Me.btnPrint.Text = "พิมพ์"
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'cboDocumentType
        '
        Me.cboDocumentType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentType.FormattingEnabled = True
        Me.cboDocumentType.Location = New System.Drawing.Point(13, 287)
        Me.cboDocumentType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDocumentType.Name = "cboDocumentType"
        Me.cboDocumentType.Size = New System.Drawing.Size(149, 24)
        Me.cboDocumentType.TabIndex = 293
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 268)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(79, 17)
        Me.Label1.TabIndex = 20
        Me.Label1.Text = "ประเภทใบคุม"
        '
        'cboVehicleType
        '
        Me.cboVehicleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVehicleType.FormattingEnabled = True
        Me.cboVehicleType.Location = New System.Drawing.Point(13, 239)
        Me.cboVehicleType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboVehicleType.Name = "cboVehicleType"
        Me.cboVehicleType.Size = New System.Drawing.Size(149, 24)
        Me.cboVehicleType.TabIndex = 19
        '
        'lblVehicleType
        '
        Me.lblVehicleType.AutoSize = True
        Me.lblVehicleType.Location = New System.Drawing.Point(9, 219)
        Me.lblVehicleType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblVehicleType.Name = "lblVehicleType"
        Me.lblVehicleType.Size = New System.Drawing.Size(64, 17)
        Me.lblVehicleType.TabIndex = 18
        Me.lblVehicleType.Text = "ประเภทรถ"
        '
        'cboDistributionCenter
        '
        Me.cboDistributionCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistributionCenter.FormattingEnabled = True
        Me.cboDistributionCenter.Location = New System.Drawing.Point(13, 39)
        Me.cboDistributionCenter.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDistributionCenter.Name = "cboDistributionCenter"
        Me.cboDistributionCenter.Size = New System.Drawing.Size(149, 24)
        Me.cboDistributionCenter.TabIndex = 17
        '
        'lblDistributionCenter
        '
        Me.lblDistributionCenter.AutoSize = True
        Me.lblDistributionCenter.Location = New System.Drawing.Point(8, 20)
        Me.lblDistributionCenter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDistributionCenter.Name = "lblDistributionCenter"
        Me.lblDistributionCenter.Size = New System.Drawing.Size(80, 17)
        Me.lblDistributionCenter.TabIndex = 16
        Me.lblDistributionCenter.Text = "ศูนย์ปลายทาง"
        '
        'cboSubRoute
        '
        Me.cboSubRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSubRoute.FormattingEnabled = True
        Me.cboSubRoute.Location = New System.Drawing.Point(13, 142)
        Me.cboSubRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.cboSubRoute.Name = "cboSubRoute"
        Me.cboSubRoute.Size = New System.Drawing.Size(149, 24)
        Me.cboSubRoute.TabIndex = 8
        '
        'lblSubRoute
        '
        Me.lblSubRoute.AutoSize = True
        Me.lblSubRoute.Location = New System.Drawing.Point(11, 122)
        Me.lblSubRoute.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSubRoute.Name = "lblSubRoute"
        Me.lblSubRoute.Size = New System.Drawing.Size(69, 17)
        Me.lblSubRoute.TabIndex = 7
        Me.lblSubRoute.Text = "เส้นทางย่อย"
        '
        'cboRoute
        '
        Me.cboRoute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboRoute.FormattingEnabled = True
        Me.cboRoute.Location = New System.Drawing.Point(13, 91)
        Me.cboRoute.Margin = New System.Windows.Forms.Padding(4)
        Me.cboRoute.Name = "cboRoute"
        Me.cboRoute.Size = New System.Drawing.Size(149, 24)
        Me.cboRoute.TabIndex = 6
        '
        'lblTransportJobType
        '
        Me.lblTransportJobType.AutoSize = True
        Me.lblTransportJobType.Location = New System.Drawing.Point(12, 171)
        Me.lblTransportJobType.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblTransportJobType.Name = "lblTransportJobType"
        Me.lblTransportJobType.Size = New System.Drawing.Size(100, 17)
        Me.lblTransportJobType.TabIndex = 3
        Me.lblTransportJobType.Text = "ประเภทงานขนส่ง"
        '
        'lblRoute
        '
        Me.lblRoute.AutoSize = True
        Me.lblRoute.Location = New System.Drawing.Point(11, 71)
        Me.lblRoute.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblRoute.Name = "lblRoute"
        Me.lblRoute.Size = New System.Drawing.Size(72, 17)
        Me.lblRoute.TabIndex = 5
        Me.lblRoute.Text = "เส้นทางหลัก"
        '
        'cboTransportJobType
        '
        Me.cboTransportJobType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTransportJobType.FormattingEnabled = True
        Me.cboTransportJobType.Location = New System.Drawing.Point(13, 191)
        Me.cboTransportJobType.Margin = New System.Windows.Forms.Padding(4)
        Me.cboTransportJobType.Name = "cboTransportJobType"
        Me.cboTransportJobType.Size = New System.Drawing.Size(149, 24)
        Me.cboTransportJobType.TabIndex = 4
        '
        'gbCondition
        '
        Me.gbCondition.Controls.Add(Me.rdoDriver)
        Me.gbCondition.Controls.Add(Me.btnPop_Search)
        Me.gbCondition.Controls.Add(Me.rdoContainerNo)
        Me.gbCondition.Controls.Add(Me.rdoVehicleNo)
        Me.gbCondition.Controls.Add(Me.rdoTransportManifest_No)
        Me.gbCondition.Controls.Add(Me.rdoCustomerShipping)
        Me.gbCondition.Controls.Add(Me.dtpDate)
        Me.gbCondition.Controls.Add(Me.lb_to)
        Me.gbCondition.Controls.Add(Me.dateEnd)
        Me.gbCondition.Controls.Add(Me.txtKeySearch)
        Me.gbCondition.Controls.Add(Me.btnSearch)
        Me.gbCondition.Controls.Add(Me.rdbCustomer)
        Me.gbCondition.Controls.Add(Me.rdbTransportManifest_Date)
        Me.gbCondition.Location = New System.Drawing.Point(9, 2)
        Me.gbCondition.Margin = New System.Windows.Forms.Padding(4)
        Me.gbCondition.Name = "gbCondition"
        Me.gbCondition.Padding = New System.Windows.Forms.Padding(4)
        Me.gbCondition.Size = New System.Drawing.Size(175, 359)
        Me.gbCondition.TabIndex = 1
        Me.gbCondition.TabStop = False
        Me.gbCondition.Text = "เงื่อนไข"
        '
        'rdoDriver
        '
        Me.rdoDriver.AutoSize = True
        Me.rdoDriver.Location = New System.Drawing.Point(16, 137)
        Me.rdoDriver.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoDriver.Name = "rdoDriver"
        Me.rdoDriver.Size = New System.Drawing.Size(107, 21)
        Me.rdoDriver.TabIndex = 29
        Me.rdoDriver.Text = "พนักงานขับรถ"
        Me.rdoDriver.UseVisualStyleBackColor = True
        '
        'btnPop_Search
        '
        Me.btnPop_Search.Location = New System.Drawing.Point(135, 190)
        Me.btnPop_Search.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPop_Search.Name = "btnPop_Search"
        Me.btnPop_Search.Size = New System.Drawing.Size(32, 28)
        Me.btnPop_Search.TabIndex = 28
        Me.btnPop_Search.Text = "..."
        Me.btnPop_Search.UseVisualStyleBackColor = True
        Me.btnPop_Search.Visible = False
        '
        'rdoContainerNo
        '
        Me.rdoContainerNo.AutoSize = True
        Me.rdoContainerNo.Location = New System.Drawing.Point(16, 193)
        Me.rdoContainerNo.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoContainerNo.Name = "rdoContainerNo"
        Me.rdoContainerNo.Size = New System.Drawing.Size(86, 21)
        Me.rdoContainerNo.TabIndex = 27
        Me.rdoContainerNo.Text = "หมายเลขตู้"
        Me.rdoContainerNo.UseVisualStyleBackColor = True
        '
        'rdoVehicleNo
        '
        Me.rdoVehicleNo.AutoSize = True
        Me.rdoVehicleNo.Location = New System.Drawing.Point(16, 165)
        Me.rdoVehicleNo.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoVehicleNo.Name = "rdoVehicleNo"
        Me.rdoVehicleNo.Size = New System.Drawing.Size(86, 21)
        Me.rdoVehicleNo.TabIndex = 26
        Me.rdoVehicleNo.Text = "ทะเบียนรถ"
        Me.rdoVehicleNo.UseVisualStyleBackColor = True
        '
        'rdoTransportManifest_No
        '
        Me.rdoTransportManifest_No.AutoSize = True
        Me.rdoTransportManifest_No.Location = New System.Drawing.Point(15, 53)
        Me.rdoTransportManifest_No.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoTransportManifest_No.Name = "rdoTransportManifest_No"
        Me.rdoTransportManifest_No.Size = New System.Drawing.Size(101, 21)
        Me.rdoTransportManifest_No.TabIndex = 25
        Me.rdoTransportManifest_No.Text = "เลขที่ใบคุมรถ"
        Me.rdoTransportManifest_No.UseVisualStyleBackColor = True
        '
        'rdoCustomerShipping
        '
        Me.rdoCustomerShipping.AutoSize = True
        Me.rdoCustomerShipping.Location = New System.Drawing.Point(16, 110)
        Me.rdoCustomerShipping.Margin = New System.Windows.Forms.Padding(4)
        Me.rdoCustomerShipping.Name = "rdoCustomerShipping"
        Me.rdoCustomerShipping.Size = New System.Drawing.Size(119, 21)
        Me.rdoCustomerShipping.TabIndex = 23
        Me.rdoCustomerShipping.Text = "ชื่อผู้รับ/ปลายทาง"
        Me.rdoCustomerShipping.UseVisualStyleBackColor = True
        '
        'dtpDate
        '
        Me.dtpDate.CustomFormat = "dd/MM/yyyy"
        Me.dtpDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpDate.Location = New System.Drawing.Point(15, 223)
        Me.dtpDate.Margin = New System.Windows.Forms.Padding(4)
        Me.dtpDate.Name = "dtpDate"
        Me.dtpDate.Size = New System.Drawing.Size(140, 22)
        Me.dtpDate.TabIndex = 22
        '
        'lb_to
        '
        Me.lb_to.AutoSize = True
        Me.lb_to.Location = New System.Drawing.Point(64, 251)
        Me.lb_to.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lb_to.Name = "lb_to"
        Me.lb_to.Size = New System.Drawing.Size(22, 17)
        Me.lb_to.TabIndex = 16
        Me.lb_to.Text = "ถึง"
        '
        'dateEnd
        '
        Me.dateEnd.CustomFormat = "dd/MM/yyyy"
        Me.dateEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateEnd.Location = New System.Drawing.Point(12, 271)
        Me.dateEnd.Margin = New System.Windows.Forms.Padding(4)
        Me.dateEnd.Name = "dateEnd"
        Me.dateEnd.Size = New System.Drawing.Size(143, 22)
        Me.dateEnd.TabIndex = 15
        '
        'txtKeySearch
        '
        Me.txtKeySearch.Location = New System.Drawing.Point(16, 223)
        Me.txtKeySearch.Margin = New System.Windows.Forms.Padding(4)
        Me.txtKeySearch.Name = "txtKeySearch"
        Me.txtKeySearch.Size = New System.Drawing.Size(121, 22)
        Me.txtKeySearch.TabIndex = 6
        '
        'btnSearch
        '
        Me.btnSearch.Image = CType(resources.GetObject("btnSearch.Image"), System.Drawing.Image)
        Me.btnSearch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSearch.Location = New System.Drawing.Point(16, 303)
        Me.btnSearch.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSearch.Name = "btnSearch"
        Me.btnSearch.Size = New System.Drawing.Size(133, 47)
        Me.btnSearch.TabIndex = 3
        Me.btnSearch.Text = "ค้นหา"
        Me.btnSearch.UseVisualStyleBackColor = True
        '
        'rdbCustomer
        '
        Me.rdbCustomer.AutoSize = True
        Me.rdbCustomer.Location = New System.Drawing.Point(16, 81)
        Me.rdbCustomer.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbCustomer.Name = "rdbCustomer"
        Me.rdbCustomer.Size = New System.Drawing.Size(103, 21)
        Me.rdbCustomer.TabIndex = 3
        Me.rdbCustomer.Text = "ชื่อเจ้าของงาน"
        Me.rdbCustomer.UseVisualStyleBackColor = True
        '
        'rdbTransportManifest_Date
        '
        Me.rdbTransportManifest_Date.AutoSize = True
        Me.rdbTransportManifest_Date.Checked = True
        Me.rdbTransportManifest_Date.Location = New System.Drawing.Point(16, 25)
        Me.rdbTransportManifest_Date.Margin = New System.Windows.Forms.Padding(4)
        Me.rdbTransportManifest_Date.Name = "rdbTransportManifest_Date"
        Me.rdbTransportManifest_Date.Size = New System.Drawing.Size(98, 21)
        Me.rdbTransportManifest_Date.TabIndex = 3
        Me.rdbTransportManifest_Date.TabStop = True
        Me.rdbTransportManifest_Date.Text = "วันที่ใบคุมรถ"
        Me.rdbTransportManifest_Date.UseVisualStyleBackColor = True
        '
        'cboDocumentStatus
        '
        Me.cboDocumentStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDocumentStatus.FormattingEnabled = True
        Me.cboDocumentStatus.Location = New System.Drawing.Point(20, 33)
        Me.cboDocumentStatus.Margin = New System.Windows.Forms.Padding(4)
        Me.cboDocumentStatus.Name = "cboDocumentStatus"
        Me.cboDocumentStatus.Size = New System.Drawing.Size(149, 24)
        Me.cboDocumentStatus.TabIndex = 21
        Me.cboDocumentStatus.Visible = False
        '
        'lblStatus
        '
        Me.lblStatus.AutoSize = True
        Me.lblStatus.Location = New System.Drawing.Point(16, 14)
        Me.lblStatus.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblStatus.Name = "lblStatus"
        Me.lblStatus.Size = New System.Drawing.Size(83, 17)
        Me.lblStatus.TabIndex = 20
        Me.lblStatus.Text = "สถานะเอกสาร"
        Me.lblStatus.Visible = False
        '
        'tabTransportManifest
        '
        Me.tabTransportManifest.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabTransportManifest.Controls.Add(Me.tbpJobLoading)
        Me.tabTransportManifest.Controls.Add(Me.tbpJobInTransport)
        Me.tabTransportManifest.Controls.Add(Me.tbpJobReturn)
        Me.tabTransportManifest.Controls.Add(Me.tabManifestComplete)
        Me.tabTransportManifest.Location = New System.Drawing.Point(199, 9)
        Me.tabTransportManifest.Margin = New System.Windows.Forms.Padding(4)
        Me.tabTransportManifest.Name = "tabTransportManifest"
        Me.tabTransportManifest.SelectedIndex = 0
        Me.tabTransportManifest.Size = New System.Drawing.Size(1155, 796)
        Me.tabTransportManifest.TabIndex = 16
        '
        'tbpJobLoading
        '
        Me.tbpJobLoading.Controls.Add(Me.SplitContainer4)
        Me.tbpJobLoading.Location = New System.Drawing.Point(4, 25)
        Me.tbpJobLoading.Margin = New System.Windows.Forms.Padding(4)
        Me.tbpJobLoading.Name = "tbpJobLoading"
        Me.tbpJobLoading.Padding = New System.Windows.Forms.Padding(4)
        Me.tbpJobLoading.Size = New System.Drawing.Size(1147, 767)
        Me.tbpJobLoading.TabIndex = 2
        Me.tbpJobLoading.Text = "รถที่กำลังจัดสินค้า"
        Me.tbpJobLoading.UseVisualStyleBackColor = True
        '
        'SplitContainer4
        '
        Me.SplitContainer4.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer4.Location = New System.Drawing.Point(4, 4)
        Me.SplitContainer4.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer4.Name = "SplitContainer4"
        Me.SplitContainer4.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer4.Panel1
        '
        Me.SplitContainer4.Panel1.Controls.Add(Me.chkSelectAll)
        Me.SplitContainer4.Panel1.Controls.Add(Me.grdJobLoading)
        '
        'SplitContainer4.Panel2
        '
        Me.SplitContainer4.Panel2.Controls.Add(Me.Panel4)
        Me.SplitContainer4.Size = New System.Drawing.Size(1139, 759)
        Me.SplitContainer4.SplitterDistance = 680
        Me.SplitContainer4.SplitterWidth = 5
        Me.SplitContainer4.TabIndex = 304
        '
        'chkSelectAll
        '
        Me.chkSelectAll.AutoSize = True
        Me.chkSelectAll.Location = New System.Drawing.Point(5, 5)
        Me.chkSelectAll.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSelectAll.Name = "chkSelectAll"
        Me.chkSelectAll.Size = New System.Drawing.Size(18, 17)
        Me.chkSelectAll.TabIndex = 24
        Me.chkSelectAll.UseVisualStyleBackColor = True
        '
        'grdJobLoading
        '
        Me.grdJobLoading.AllowUserToAddRows = False
        Me.grdJobLoading.AllowUserToDeleteRows = False
        Me.grdJobLoading.AllowUserToOrderColumns = True
        DataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdJobLoading.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grdJobLoading.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobLoading.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grdJobLoading.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdJobLoading.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkItem_JobLoading, Me.col_Index_JobLoading, Me.col_TransportManifest_No_JobLoading, Me.col_DocumentType_desc, Me.col_TransportManifest_Date_JobLoading, Me.col_Vehicle_No_JobLoading, Me.col_Vehicle_ID_JobLoading, Me.col_Driver_Name_JobLoading, Me.col_Route_JobLoading, Me.col_SubRoute_JobLoading, Me.col_Status_JobLoading, Me.col_TransportJobType_JobLoading, Me.col_Sum_Volume_JobLoading, Me.col_Sum_Weight_JobLoading, Me.col_DistributionCenter_JobLoading, Me.col_VehicleType_JobLoading, Me.col_CustomerShipping_JobLoading, Me.col_CustomerShippingLocation_JobLoading, Me.col_Sum_Qty_JobLoading, Me.col_Count_Document_JobLoading, Me.col_ContainerNo1_JobLoading, Me.col_ContainerNo2_JobLoading, Me.col_Customer_JobLoading, Me.col_Ref1_JobLoading, Me.col_Comment_JobLoading, Me.col_Add_By_JobLoading})
        Me.grdJobLoading.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdJobLoading.Location = New System.Drawing.Point(0, 0)
        Me.grdJobLoading.Margin = New System.Windows.Forms.Padding(4)
        Me.grdJobLoading.Name = "grdJobLoading"
        Me.grdJobLoading.RowHeadersVisible = False
        Me.grdJobLoading.RowTemplate.Height = 24
        Me.grdJobLoading.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdJobLoading.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdJobLoading.Size = New System.Drawing.Size(1139, 680)
        Me.grdJobLoading.TabIndex = 23
        '
        'chkItem_JobLoading
        '
        Me.chkItem_JobLoading.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.chkItem_JobLoading.DataPropertyName = "chkSelect"
        Me.chkItem_JobLoading.FalseValue = "0"
        Me.chkItem_JobLoading.Frozen = True
        Me.chkItem_JobLoading.HeaderText = ""
        Me.chkItem_JobLoading.Name = "chkItem_JobLoading"
        Me.chkItem_JobLoading.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkItem_JobLoading.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkItem_JobLoading.TrueValue = "1"
        Me.chkItem_JobLoading.Width = 19
        '
        'col_Index_JobLoading
        '
        Me.col_Index_JobLoading.DataPropertyName = "TransportManifest_Index"
        Me.col_Index_JobLoading.HeaderText = "รหัสระบบ "
        Me.col_Index_JobLoading.Name = "col_Index_JobLoading"
        Me.col_Index_JobLoading.Visible = False
        Me.col_Index_JobLoading.Width = 110
        '
        'col_TransportManifest_No_JobLoading
        '
        Me.col_TransportManifest_No_JobLoading.DataPropertyName = "TransportManifest_No"
        Me.col_TransportManifest_No_JobLoading.Frozen = True
        Me.col_TransportManifest_No_JobLoading.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No_JobLoading.Name = "col_TransportManifest_No_JobLoading"
        '
        'col_DocumentType_desc
        '
        Me.col_DocumentType_desc.DataPropertyName = "DocumentType_desc"
        Me.col_DocumentType_desc.HeaderText = "ประเภทเอกสาร"
        Me.col_DocumentType_desc.Name = "col_DocumentType_desc"
        Me.col_DocumentType_desc.Visible = False
        Me.col_DocumentType_desc.Width = 150
        '
        'col_TransportManifest_Date_JobLoading
        '
        Me.col_TransportManifest_Date_JobLoading.DataPropertyName = "TransportManifest_Date"
        Me.col_TransportManifest_Date_JobLoading.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date_JobLoading.Name = "col_TransportManifest_Date_JobLoading"
        '
        'col_Vehicle_No_JobLoading
        '
        Me.col_Vehicle_No_JobLoading.DataPropertyName = "Vehicle_License_No"
        Me.col_Vehicle_No_JobLoading.HeaderText = "ทะเบียนรถ"
        Me.col_Vehicle_No_JobLoading.Name = "col_Vehicle_No_JobLoading"
        Me.col_Vehicle_No_JobLoading.Width = 80
        '
        'col_Vehicle_ID_JobLoading
        '
        Me.col_Vehicle_ID_JobLoading.DataPropertyName = "Vehicle_Id"
        Me.col_Vehicle_ID_JobLoading.HeaderText = "หมายเลขรถ"
        Me.col_Vehicle_ID_JobLoading.Name = "col_Vehicle_ID_JobLoading"
        Me.col_Vehicle_ID_JobLoading.Visible = False
        '
        'col_Driver_Name_JobLoading
        '
        Me.col_Driver_Name_JobLoading.DataPropertyName = "Driver_Name"
        Me.col_Driver_Name_JobLoading.HeaderText = "พนักงานขับรถ"
        Me.col_Driver_Name_JobLoading.Name = "col_Driver_Name_JobLoading"
        '
        'col_Route_JobLoading
        '
        Me.col_Route_JobLoading.DataPropertyName = "Route"
        Me.col_Route_JobLoading.HeaderText = "เส้นทางหลัก"
        Me.col_Route_JobLoading.Name = "col_Route_JobLoading"
        '
        'col_SubRoute_JobLoading
        '
        Me.col_SubRoute_JobLoading.DataPropertyName = "SubRoute"
        Me.col_SubRoute_JobLoading.HeaderText = "เส้นทางย่อย"
        Me.col_SubRoute_JobLoading.Name = "col_SubRoute_JobLoading"
        '
        'col_Status_JobLoading
        '
        Me.col_Status_JobLoading.DataPropertyName = "Status"
        Me.col_Status_JobLoading.HeaderText = "สถานะใบคุม"
        Me.col_Status_JobLoading.Name = "col_Status_JobLoading"
        '
        'col_TransportJobType_JobLoading
        '
        Me.col_TransportJobType_JobLoading.DataPropertyName = "TransportJobType"
        Me.col_TransportJobType_JobLoading.HeaderText = "ประเภทงาน"
        Me.col_TransportJobType_JobLoading.Name = "col_TransportJobType_JobLoading"
        Me.col_TransportJobType_JobLoading.Width = 90
        '
        'col_Sum_Volume_JobLoading
        '
        Me.col_Sum_Volume_JobLoading.DataPropertyName = "Volume_Sum"
        Me.col_Sum_Volume_JobLoading.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.col_Sum_Volume_JobLoading.Name = "col_Sum_Volume_JobLoading"
        Me.col_Sum_Volume_JobLoading.Width = 120
        '
        'col_Sum_Weight_JobLoading
        '
        Me.col_Sum_Weight_JobLoading.DataPropertyName = "Weight_Sum"
        Me.col_Sum_Weight_JobLoading.HeaderText = "นน. สินค้า/พิกัด"
        Me.col_Sum_Weight_JobLoading.Name = "col_Sum_Weight_JobLoading"
        Me.col_Sum_Weight_JobLoading.Width = 110
        '
        'col_DistributionCenter_JobLoading
        '
        Me.col_DistributionCenter_JobLoading.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter_JobLoading.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter_JobLoading.Name = "col_DistributionCenter_JobLoading"
        Me.col_DistributionCenter_JobLoading.Visible = False
        '
        'col_VehicleType_JobLoading
        '
        Me.col_VehicleType_JobLoading.DataPropertyName = "VehicleType"
        Me.col_VehicleType_JobLoading.HeaderText = "ประเภทรถ"
        Me.col_VehicleType_JobLoading.Name = "col_VehicleType_JobLoading"
        Me.col_VehicleType_JobLoading.Width = 80
        '
        'col_CustomerShipping_JobLoading
        '
        Me.col_CustomerShipping_JobLoading.DataPropertyName = "Company_Name"
        Me.col_CustomerShipping_JobLoading.HeaderText = "ผู้รับ"
        Me.col_CustomerShipping_JobLoading.Name = "col_CustomerShipping_JobLoading"
        Me.col_CustomerShipping_JobLoading.Visible = False
        '
        'col_CustomerShippingLocation_JobLoading
        '
        Me.col_CustomerShippingLocation_JobLoading.DataPropertyName = "Shipping_Location_Name"
        Me.col_CustomerShippingLocation_JobLoading.HeaderText = "ปลายทาง"
        Me.col_CustomerShippingLocation_JobLoading.Name = "col_CustomerShippingLocation_JobLoading"
        '
        'col_Sum_Qty_JobLoading
        '
        Me.col_Sum_Qty_JobLoading.DataPropertyName = "Qty_Sum"
        Me.col_Sum_Qty_JobLoading.HeaderText = "จน.ชิ้น"
        Me.col_Sum_Qty_JobLoading.Name = "col_Sum_Qty_JobLoading"
        Me.col_Sum_Qty_JobLoading.Width = 70
        '
        'col_Count_Document_JobLoading
        '
        Me.col_Count_Document_JobLoading.DataPropertyName = "Bill_Sum"
        Me.col_Count_Document_JobLoading.HeaderText = "จำนวนบิล"
        Me.col_Count_Document_JobLoading.Name = "col_Count_Document_JobLoading"
        Me.col_Count_Document_JobLoading.Width = 90
        '
        'col_ContainerNo1_JobLoading
        '
        Me.col_ContainerNo1_JobLoading.DataPropertyName = "Container_No1"
        Me.col_ContainerNo1_JobLoading.HeaderText = "หมายเลขตู้ 1"
        Me.col_ContainerNo1_JobLoading.Name = "col_ContainerNo1_JobLoading"
        '
        'col_ContainerNo2_JobLoading
        '
        Me.col_ContainerNo2_JobLoading.DataPropertyName = "Container_No2"
        Me.col_ContainerNo2_JobLoading.HeaderText = "หมายเลขตู้ 2"
        Me.col_ContainerNo2_JobLoading.Name = "col_ContainerNo2_JobLoading"
        '
        'col_Customer_JobLoading
        '
        Me.col_Customer_JobLoading.DataPropertyName = "Customer_Name"
        Me.col_Customer_JobLoading.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.col_Customer_JobLoading.Name = "col_Customer_JobLoading"
        Me.col_Customer_JobLoading.Visible = False
        Me.col_Customer_JobLoading.Width = 120
        '
        'col_Ref1_JobLoading
        '
        Me.col_Ref1_JobLoading.DataPropertyName = "Str1"
        Me.col_Ref1_JobLoading.HeaderText = "อ้างอิง 1"
        Me.col_Ref1_JobLoading.Name = "col_Ref1_JobLoading"
        Me.col_Ref1_JobLoading.Visible = False
        '
        'col_Comment_JobLoading
        '
        Me.col_Comment_JobLoading.DataPropertyName = "Comment"
        Me.col_Comment_JobLoading.HeaderText = "หมายเหตุ"
        Me.col_Comment_JobLoading.Name = "col_Comment_JobLoading"
        '
        'col_Add_By_JobLoading
        '
        Me.col_Add_By_JobLoading.DataPropertyName = "Add_By"
        Me.col_Add_By_JobLoading.HeaderText = "ผู้ออกเอกสาร"
        Me.col_Add_By_JobLoading.Name = "col_Add_By_JobLoading"
        Me.col_Add_By_JobLoading.Width = 150
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel4.Controls.Add(Me.lbCountRows)
        Me.Panel4.Controls.Add(Me.btnAddManifest)
        Me.Panel4.Controls.Add(Me.btnEditManifest)
        Me.Panel4.Controls.Add(Me.btnCopy)
        Me.Panel4.Controls.Add(Me.btnReleaseTruck)
        Me.Panel4.Controls.Add(Me.lblSumWeight_JobLoading)
        Me.Panel4.Controls.Add(Me.txtSumVolume_JobLoading)
        Me.Panel4.Controls.Add(Me.txtSumWeight_JobLoading)
        Me.Panel4.Controls.Add(Me.txtSumQty_JobLoading)
        Me.Panel4.Controls.Add(Me.lblSumDoc_JobLoading)
        Me.Panel4.Controls.Add(Me.lblSumAll)
        Me.Panel4.Controls.Add(Me.txtSumDoc_JobLoading)
        Me.Panel4.Controls.Add(Me.lblSumVolume_JobLoading)
        Me.Panel4.Controls.Add(Me.lblSumQty_JobLoading)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel4.Location = New System.Drawing.Point(0, 3)
        Me.Panel4.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(1139, 71)
        Me.Panel4.TabIndex = 102
        '
        'lbCountRows
        '
        Me.lbCountRows.AutoSize = True
        Me.lbCountRows.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows.Location = New System.Drawing.Point(4, 4)
        Me.lbCountRows.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRows.Name = "lbCountRows"
        Me.lbCountRows.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRows.TabIndex = 24
        Me.lbCountRows.Text = "ไม่พบรายการ"
        '
        'btnAddManifest
        '
        Me.btnAddManifest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnAddManifest.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.เพิ่มรายการ
        Me.btnAddManifest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnAddManifest.Location = New System.Drawing.Point(4, 21)
        Me.btnAddManifest.Margin = New System.Windows.Forms.Padding(4)
        Me.btnAddManifest.Name = "btnAddManifest"
        Me.btnAddManifest.Size = New System.Drawing.Size(143, 47)
        Me.btnAddManifest.TabIndex = 303
        Me.btnAddManifest.Text = "เพิ่มใบคุมรถ"
        Me.btnAddManifest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnAddManifest.UseVisualStyleBackColor = True
        '
        'btnEditManifest
        '
        Me.btnEditManifest.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEditManifest.Image = CType(resources.GetObject("btnEditManifest.Image"), System.Drawing.Image)
        Me.btnEditManifest.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditManifest.Location = New System.Drawing.Point(155, 21)
        Me.btnEditManifest.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEditManifest.Name = "btnEditManifest"
        Me.btnEditManifest.Size = New System.Drawing.Size(143, 47)
        Me.btnEditManifest.TabIndex = 26
        Me.btnEditManifest.Text = "แก้ไขรายการ"
        Me.btnEditManifest.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditManifest.UseVisualStyleBackColor = True
        '
        'btnCopy
        '
        Me.btnCopy.Image = CType(resources.GetObject("btnCopy.Image"), System.Drawing.Image)
        Me.btnCopy.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCopy.Location = New System.Drawing.Point(305, 21)
        Me.btnCopy.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCopy.Name = "btnCopy"
        Me.btnCopy.Size = New System.Drawing.Size(143, 47)
        Me.btnCopy.TabIndex = 79
        Me.btnCopy.Text = "    คัดลอก"
        Me.btnCopy.UseVisualStyleBackColor = True
        '
        'btnReleaseTruck
        '
        Me.btnReleaseTruck.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnReleaseTruck.Image = CType(resources.GetObject("btnReleaseTruck.Image"), System.Drawing.Image)
        Me.btnReleaseTruck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReleaseTruck.Location = New System.Drawing.Point(456, 21)
        Me.btnReleaseTruck.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReleaseTruck.Name = "btnReleaseTruck"
        Me.btnReleaseTruck.Size = New System.Drawing.Size(143, 47)
        Me.btnReleaseTruck.TabIndex = 29
        Me.btnReleaseTruck.Text = "ปล่อยรถ"
        Me.btnReleaseTruck.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReleaseTruck.UseVisualStyleBackColor = True
        '
        'lblSumWeight_JobLoading
        '
        Me.lblSumWeight_JobLoading.AutoSize = True
        Me.lblSumWeight_JobLoading.Location = New System.Drawing.Point(1017, 21)
        Me.lblSumWeight_JobLoading.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumWeight_JobLoading.Name = "lblSumWeight_JobLoading"
        Me.lblSumWeight_JobLoading.Size = New System.Drawing.Size(60, 17)
        Me.lblSumWeight_JobLoading.TabIndex = 75
        Me.lblSumWeight_JobLoading.Text = "นน.สินค้า"
        '
        'txtSumVolume_JobLoading
        '
        Me.txtSumVolume_JobLoading.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumVolume_JobLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumVolume_JobLoading.Location = New System.Drawing.Point(901, 39)
        Me.txtSumVolume_JobLoading.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumVolume_JobLoading.Name = "txtSumVolume_JobLoading"
        Me.txtSumVolume_JobLoading.ReadOnly = True
        Me.txtSumVolume_JobLoading.Size = New System.Drawing.Size(109, 23)
        Me.txtSumVolume_JobLoading.TabIndex = 72
        Me.txtSumVolume_JobLoading.Text = "0.0000"
        Me.txtSumVolume_JobLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumWeight_JobLoading
        '
        Me.txtSumWeight_JobLoading.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumWeight_JobLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumWeight_JobLoading.Location = New System.Drawing.Point(1020, 39)
        Me.txtSumWeight_JobLoading.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumWeight_JobLoading.Name = "txtSumWeight_JobLoading"
        Me.txtSumWeight_JobLoading.ReadOnly = True
        Me.txtSumWeight_JobLoading.Size = New System.Drawing.Size(109, 23)
        Me.txtSumWeight_JobLoading.TabIndex = 71
        Me.txtSumWeight_JobLoading.Text = "0.0000"
        Me.txtSumWeight_JobLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumQty_JobLoading
        '
        Me.txtSumQty_JobLoading.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumQty_JobLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumQty_JobLoading.Location = New System.Drawing.Point(780, 39)
        Me.txtSumQty_JobLoading.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumQty_JobLoading.Name = "txtSumQty_JobLoading"
        Me.txtSumQty_JobLoading.ReadOnly = True
        Me.txtSumQty_JobLoading.Size = New System.Drawing.Size(109, 23)
        Me.txtSumQty_JobLoading.TabIndex = 70
        Me.txtSumQty_JobLoading.Text = "0"
        Me.txtSumQty_JobLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSumDoc_JobLoading
        '
        Me.lblSumDoc_JobLoading.AutoSize = True
        Me.lblSumDoc_JobLoading.Location = New System.Drawing.Point(660, 22)
        Me.lblSumDoc_JobLoading.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumDoc_JobLoading.Name = "lblSumDoc_JobLoading"
        Me.lblSumDoc_JobLoading.Size = New System.Drawing.Size(61, 17)
        Me.lblSumDoc_JobLoading.TabIndex = 78
        Me.lblSumDoc_JobLoading.Text = "จำนวนบิล"
        '
        'lblSumAll
        '
        Me.lblSumAll.AutoSize = True
        Me.lblSumAll.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.lblSumAll.Location = New System.Drawing.Point(613, 44)
        Me.lblSumAll.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumAll.Name = "lblSumAll"
        Me.lblSumAll.Size = New System.Drawing.Size(39, 20)
        Me.lblSumAll.TabIndex = 73
        Me.lblSumAll.Text = "รวม"
        '
        'txtSumDoc_JobLoading
        '
        Me.txtSumDoc_JobLoading.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumDoc_JobLoading.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumDoc_JobLoading.Location = New System.Drawing.Point(661, 39)
        Me.txtSumDoc_JobLoading.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumDoc_JobLoading.Name = "txtSumDoc_JobLoading"
        Me.txtSumDoc_JobLoading.ReadOnly = True
        Me.txtSumDoc_JobLoading.Size = New System.Drawing.Size(109, 23)
        Me.txtSumDoc_JobLoading.TabIndex = 77
        Me.txtSumDoc_JobLoading.Text = "0"
        Me.txtSumDoc_JobLoading.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSumVolume_JobLoading
        '
        Me.lblSumVolume_JobLoading.AutoSize = True
        Me.lblSumVolume_JobLoading.Location = New System.Drawing.Point(899, 22)
        Me.lblSumVolume_JobLoading.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumVolume_JobLoading.Name = "lblSumVolume_JobLoading"
        Me.lblSumVolume_JobLoading.Size = New System.Drawing.Size(82, 17)
        Me.lblSumVolume_JobLoading.TabIndex = 76
        Me.lblSumVolume_JobLoading.Text = "ปริมาตรสินค้า"
        '
        'lblSumQty_JobLoading
        '
        Me.lblSumQty_JobLoading.AutoSize = True
        Me.lblSumQty_JobLoading.Location = New System.Drawing.Point(776, 22)
        Me.lblSumQty_JobLoading.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumQty_JobLoading.Name = "lblSumQty_JobLoading"
        Me.lblSumQty_JobLoading.Size = New System.Drawing.Size(63, 17)
        Me.lblSumQty_JobLoading.TabIndex = 74
        Me.lblSumQty_JobLoading.Text = "จำนวนชิ้น"
        '
        'tbpJobInTransport
        '
        Me.tbpJobInTransport.Controls.Add(Me.SplitContainer3)
        Me.tbpJobInTransport.Location = New System.Drawing.Point(4, 25)
        Me.tbpJobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.tbpJobInTransport.Name = "tbpJobInTransport"
        Me.tbpJobInTransport.Padding = New System.Windows.Forms.Padding(4)
        Me.tbpJobInTransport.Size = New System.Drawing.Size(1147, 767)
        Me.tbpJobInTransport.TabIndex = 3
        Me.tbpJobInTransport.Text = "รถอยู่ระหว่างจัดส่ง/ถึงปลายทาง"
        Me.tbpJobInTransport.UseVisualStyleBackColor = True
        '
        'SplitContainer3
        '
        Me.SplitContainer3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer3.Location = New System.Drawing.Point(4, 4)
        Me.SplitContainer3.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer3.Name = "SplitContainer3"
        Me.SplitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer3.Panel1
        '
        Me.SplitContainer3.Panel1.Controls.Add(Me.chkSelectAll1)
        Me.SplitContainer3.Panel1.Controls.Add(Me.grdJobInTransport)
        '
        'SplitContainer3.Panel2
        '
        Me.SplitContainer3.Panel2.Controls.Add(Me.Panel3)
        Me.SplitContainer3.Size = New System.Drawing.Size(1139, 759)
        Me.SplitContainer3.SplitterDistance = 680
        Me.SplitContainer3.SplitterWidth = 5
        Me.SplitContainer3.TabIndex = 80
        '
        'chkSelectAll1
        '
        Me.chkSelectAll1.AutoSize = True
        Me.chkSelectAll1.Location = New System.Drawing.Point(5, 5)
        Me.chkSelectAll1.Margin = New System.Windows.Forms.Padding(4)
        Me.chkSelectAll1.Name = "chkSelectAll1"
        Me.chkSelectAll1.Size = New System.Drawing.Size(18, 17)
        Me.chkSelectAll1.TabIndex = 31
        Me.chkSelectAll1.UseVisualStyleBackColor = True
        '
        'grdJobInTransport
        '
        Me.grdJobInTransport.AllowUserToAddRows = False
        Me.grdJobInTransport.AllowUserToDeleteRows = False
        Me.grdJobInTransport.AllowUserToOrderColumns = True
        DataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdJobInTransport.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        Me.grdJobInTransport.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobInTransport.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.grdJobInTransport.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdJobInTransport.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect_JobInTransport, Me.col_TransportManifest_No_JobInTransport, Me.col_TransportManifest_Date_JobInTransport, Me.col_Time_SourceOutGate_JobInTransport, Me.col_Time_DestinationInGate_JobInTransport, Me.col_Time_ReturnTruckInGate_JobInTransport, Me.col_Vehicle_Id_JobInTransport, Me.col_Vehicle_No_JobInTransport, Me.col_Route_JobInTransport, Me.col_SubRoute_JobInTransport, Me.col_DistributionCenter_JobInTransport, Me.col_VehicleType_JobInTransport, Me.col_Driver_Name_JobInTransport, Me.col_Customer_Shipping_JobInTransport, Me.col_ShippingLocation_JobInTransport, Me.col_Status_JobInTransport, Me.col_Qty_JobInTransport, Me.col_Bill_JobInTransport, Me.col_weight_JobInTransport, Me.col_Volume_JobInTransport, Me.col_Container_No1_JobInTransport, Me.col_Container_No2_JobInTransport, Me.col_TransportJopType_JobInTransport, Me.col_Customer_JobInTransport, Me.col_Ref_No_JobInTransport, Me.col_Comment_JobInTransport, Me.col_Add_by_JobInTransport, Me.col_Status_Id, Me.col_Index_JobInTransport})
        Me.grdJobInTransport.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdJobInTransport.Location = New System.Drawing.Point(0, 0)
        Me.grdJobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.grdJobInTransport.Name = "grdJobInTransport"
        Me.grdJobInTransport.RowHeadersVisible = False
        Me.grdJobInTransport.RowTemplate.Height = 24
        Me.grdJobInTransport.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdJobInTransport.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdJobInTransport.Size = New System.Drawing.Size(1139, 680)
        Me.grdJobInTransport.TabIndex = 30
        '
        'chkSelect_JobInTransport
        '
        Me.chkSelect_JobInTransport.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.chkSelect_JobInTransport.DataPropertyName = "chkSelect"
        Me.chkSelect_JobInTransport.FalseValue = "0"
        Me.chkSelect_JobInTransport.Frozen = True
        Me.chkSelect_JobInTransport.HeaderText = ""
        Me.chkSelect_JobInTransport.Name = "chkSelect_JobInTransport"
        Me.chkSelect_JobInTransport.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkSelect_JobInTransport.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkSelect_JobInTransport.TrueValue = "1"
        Me.chkSelect_JobInTransport.Width = 19
        '
        'col_TransportManifest_No_JobInTransport
        '
        Me.col_TransportManifest_No_JobInTransport.DataPropertyName = "TransportManifest_No"
        Me.col_TransportManifest_No_JobInTransport.Frozen = True
        Me.col_TransportManifest_No_JobInTransport.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No_JobInTransport.Name = "col_TransportManifest_No_JobInTransport"
        Me.col_TransportManifest_No_JobInTransport.ReadOnly = True
        '
        'col_TransportManifest_Date_JobInTransport
        '
        Me.col_TransportManifest_Date_JobInTransport.DataPropertyName = "TransportManifest_Date"
        Me.col_TransportManifest_Date_JobInTransport.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date_JobInTransport.Name = "col_TransportManifest_Date_JobInTransport"
        Me.col_TransportManifest_Date_JobInTransport.ReadOnly = True
        '
        'col_Time_SourceOutGate_JobInTransport
        '
        Me.col_Time_SourceOutGate_JobInTransport.DataPropertyName = "Time_SourceOutGate"
        Me.col_Time_SourceOutGate_JobInTransport.HeaderText = "เวลาปล่อยรถ"
        Me.col_Time_SourceOutGate_JobInTransport.Name = "col_Time_SourceOutGate_JobInTransport"
        Me.col_Time_SourceOutGate_JobInTransport.ReadOnly = True
        '
        'col_Time_DestinationInGate_JobInTransport
        '
        Me.col_Time_DestinationInGate_JobInTransport.DataPropertyName = "Time_DestinationInGate"
        Me.col_Time_DestinationInGate_JobInTransport.HeaderText = "เวลาถึงปลายทาง"
        Me.col_Time_DestinationInGate_JobInTransport.Name = "col_Time_DestinationInGate_JobInTransport"
        Me.col_Time_DestinationInGate_JobInTransport.ReadOnly = True
        Me.col_Time_DestinationInGate_JobInTransport.Width = 110
        '
        'col_Time_ReturnTruckInGate_JobInTransport
        '
        Me.col_Time_ReturnTruckInGate_JobInTransport.DataPropertyName = "Time_ReturnTruckInGate"
        Me.col_Time_ReturnTruckInGate_JobInTransport.HeaderText = "เวลากลับต้นทาง"
        Me.col_Time_ReturnTruckInGate_JobInTransport.Name = "col_Time_ReturnTruckInGate_JobInTransport"
        Me.col_Time_ReturnTruckInGate_JobInTransport.ReadOnly = True
        Me.col_Time_ReturnTruckInGate_JobInTransport.Visible = False
        Me.col_Time_ReturnTruckInGate_JobInTransport.Width = 110
        '
        'col_Vehicle_Id_JobInTransport
        '
        Me.col_Vehicle_Id_JobInTransport.DataPropertyName = "Vehicle_Id"
        Me.col_Vehicle_Id_JobInTransport.HeaderText = "หมายเลขรถ"
        Me.col_Vehicle_Id_JobInTransport.Name = "col_Vehicle_Id_JobInTransport"
        Me.col_Vehicle_Id_JobInTransport.ReadOnly = True
        Me.col_Vehicle_Id_JobInTransport.Visible = False
        '
        'col_Vehicle_No_JobInTransport
        '
        Me.col_Vehicle_No_JobInTransport.DataPropertyName = "Vehicle_License_No"
        Me.col_Vehicle_No_JobInTransport.HeaderText = "ทะเบียนรถ"
        Me.col_Vehicle_No_JobInTransport.Name = "col_Vehicle_No_JobInTransport"
        Me.col_Vehicle_No_JobInTransport.ReadOnly = True
        Me.col_Vehicle_No_JobInTransport.Width = 80
        '
        'col_Route_JobInTransport
        '
        Me.col_Route_JobInTransport.DataPropertyName = "Route"
        Me.col_Route_JobInTransport.HeaderText = "เส้นทางหลัก"
        Me.col_Route_JobInTransport.Name = "col_Route_JobInTransport"
        Me.col_Route_JobInTransport.ReadOnly = True
        '
        'col_SubRoute_JobInTransport
        '
        Me.col_SubRoute_JobInTransport.DataPropertyName = "SubRoute"
        Me.col_SubRoute_JobInTransport.HeaderText = "เส้นทางย่อย"
        Me.col_SubRoute_JobInTransport.Name = "col_SubRoute_JobInTransport"
        '
        'col_DistributionCenter_JobInTransport
        '
        Me.col_DistributionCenter_JobInTransport.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter_JobInTransport.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter_JobInTransport.Name = "col_DistributionCenter_JobInTransport"
        Me.col_DistributionCenter_JobInTransport.ReadOnly = True
        '
        'col_VehicleType_JobInTransport
        '
        Me.col_VehicleType_JobInTransport.DataPropertyName = "VehicleType"
        Me.col_VehicleType_JobInTransport.HeaderText = "ประเภทรถ"
        Me.col_VehicleType_JobInTransport.Name = "col_VehicleType_JobInTransport"
        Me.col_VehicleType_JobInTransport.ReadOnly = True
        Me.col_VehicleType_JobInTransport.Visible = False
        Me.col_VehicleType_JobInTransport.Width = 80
        '
        'col_Driver_Name_JobInTransport
        '
        Me.col_Driver_Name_JobInTransport.DataPropertyName = "Driver_Name"
        Me.col_Driver_Name_JobInTransport.HeaderText = "พนักงานขับรถ"
        Me.col_Driver_Name_JobInTransport.Name = "col_Driver_Name_JobInTransport"
        Me.col_Driver_Name_JobInTransport.ReadOnly = True
        '
        'col_Customer_Shipping_JobInTransport
        '
        Me.col_Customer_Shipping_JobInTransport.DataPropertyName = "Company_Name"
        Me.col_Customer_Shipping_JobInTransport.HeaderText = "ผู้รับ"
        Me.col_Customer_Shipping_JobInTransport.Name = "col_Customer_Shipping_JobInTransport"
        Me.col_Customer_Shipping_JobInTransport.ReadOnly = True
        Me.col_Customer_Shipping_JobInTransport.Visible = False
        '
        'col_ShippingLocation_JobInTransport
        '
        Me.col_ShippingLocation_JobInTransport.DataPropertyName = "Shipping_Location_Name"
        Me.col_ShippingLocation_JobInTransport.HeaderText = "ปลายทาง"
        Me.col_ShippingLocation_JobInTransport.Name = "col_ShippingLocation_JobInTransport"
        Me.col_ShippingLocation_JobInTransport.ReadOnly = True
        Me.col_ShippingLocation_JobInTransport.Visible = False
        '
        'col_Status_JobInTransport
        '
        Me.col_Status_JobInTransport.DataPropertyName = "Status"
        Me.col_Status_JobInTransport.HeaderText = "สถานะใบคุม"
        Me.col_Status_JobInTransport.Name = "col_Status_JobInTransport"
        Me.col_Status_JobInTransport.ReadOnly = True
        '
        'col_Qty_JobInTransport
        '
        Me.col_Qty_JobInTransport.DataPropertyName = "Qty_Sum"
        Me.col_Qty_JobInTransport.HeaderText = "จน.ชิ้น"
        Me.col_Qty_JobInTransport.Name = "col_Qty_JobInTransport"
        Me.col_Qty_JobInTransport.ReadOnly = True
        Me.col_Qty_JobInTransport.Width = 70
        '
        'col_Bill_JobInTransport
        '
        Me.col_Bill_JobInTransport.DataPropertyName = "Bill_Sum"
        Me.col_Bill_JobInTransport.HeaderText = "จำนวนบิล"
        Me.col_Bill_JobInTransport.Name = "col_Bill_JobInTransport"
        Me.col_Bill_JobInTransport.ReadOnly = True
        Me.col_Bill_JobInTransport.Width = 90
        '
        'col_weight_JobInTransport
        '
        Me.col_weight_JobInTransport.DataPropertyName = "Weight_Sum"
        Me.col_weight_JobInTransport.HeaderText = "นน. สินค้า/พิกัด"
        Me.col_weight_JobInTransport.Name = "col_weight_JobInTransport"
        Me.col_weight_JobInTransport.ReadOnly = True
        Me.col_weight_JobInTransport.Width = 110
        '
        'col_Volume_JobInTransport
        '
        Me.col_Volume_JobInTransport.DataPropertyName = "Volume_Sum"
        Me.col_Volume_JobInTransport.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.col_Volume_JobInTransport.Name = "col_Volume_JobInTransport"
        Me.col_Volume_JobInTransport.ReadOnly = True
        Me.col_Volume_JobInTransport.Width = 120
        '
        'col_Container_No1_JobInTransport
        '
        Me.col_Container_No1_JobInTransport.DataPropertyName = "Container_No1"
        Me.col_Container_No1_JobInTransport.HeaderText = "หมายเลขตู้ 1"
        Me.col_Container_No1_JobInTransport.Name = "col_Container_No1_JobInTransport"
        Me.col_Container_No1_JobInTransport.ReadOnly = True
        '
        'col_Container_No2_JobInTransport
        '
        Me.col_Container_No2_JobInTransport.DataPropertyName = "Container_No2"
        Me.col_Container_No2_JobInTransport.HeaderText = "หมายเลขตู้ 2"
        Me.col_Container_No2_JobInTransport.Name = "col_Container_No2_JobInTransport"
        Me.col_Container_No2_JobInTransport.ReadOnly = True
        '
        'col_TransportJopType_JobInTransport
        '
        Me.col_TransportJopType_JobInTransport.DataPropertyName = "TransportJobType"
        Me.col_TransportJopType_JobInTransport.HeaderText = "ประเภทงาน"
        Me.col_TransportJopType_JobInTransport.Name = "col_TransportJopType_JobInTransport"
        Me.col_TransportJopType_JobInTransport.ReadOnly = True
        Me.col_TransportJopType_JobInTransport.Width = 90
        '
        'col_Customer_JobInTransport
        '
        Me.col_Customer_JobInTransport.DataPropertyName = "Customer_Name"
        Me.col_Customer_JobInTransport.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.col_Customer_JobInTransport.Name = "col_Customer_JobInTransport"
        Me.col_Customer_JobInTransport.ReadOnly = True
        Me.col_Customer_JobInTransport.Visible = False
        Me.col_Customer_JobInTransport.Width = 120
        '
        'col_Ref_No_JobInTransport
        '
        Me.col_Ref_No_JobInTransport.DataPropertyName = "Str1"
        Me.col_Ref_No_JobInTransport.HeaderText = "อ้างอิง 1"
        Me.col_Ref_No_JobInTransport.Name = "col_Ref_No_JobInTransport"
        Me.col_Ref_No_JobInTransport.ReadOnly = True
        Me.col_Ref_No_JobInTransport.Visible = False
        '
        'col_Comment_JobInTransport
        '
        Me.col_Comment_JobInTransport.DataPropertyName = "Comment"
        Me.col_Comment_JobInTransport.HeaderText = "หมายเหตุ"
        Me.col_Comment_JobInTransport.Name = "col_Comment_JobInTransport"
        Me.col_Comment_JobInTransport.ReadOnly = True
        '
        'col_Add_by_JobInTransport
        '
        Me.col_Add_by_JobInTransport.DataPropertyName = "Add_by"
        Me.col_Add_by_JobInTransport.HeaderText = "ผู้ออกเอกสาร"
        Me.col_Add_by_JobInTransport.Name = "col_Add_by_JobInTransport"
        Me.col_Add_by_JobInTransport.ReadOnly = True
        Me.col_Add_by_JobInTransport.Width = 150
        '
        'col_Status_Id
        '
        Me.col_Status_Id.DataPropertyName = "Status_Id"
        Me.col_Status_Id.HeaderText = "Status_Id"
        Me.col_Status_Id.Name = "col_Status_Id"
        Me.col_Status_Id.ReadOnly = True
        Me.col_Status_Id.Visible = False
        '
        'col_Index_JobInTransport
        '
        Me.col_Index_JobInTransport.DataPropertyName = "TransportManifest_Index"
        Me.col_Index_JobInTransport.HeaderText = "รหัสระบบ "
        Me.col_Index_JobInTransport.Name = "col_Index_JobInTransport"
        Me.col_Index_JobInTransport.ReadOnly = True
        Me.col_Index_JobInTransport.Width = 110
        '
        'Panel3
        '
        Me.Panel3.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel3.Controls.Add(Me.lbCountRows2)
        Me.Panel3.Controls.Add(Me.btnEditJobInTransport)
        Me.Panel3.Controls.Add(Me.lblSumVolume_JobInTransport)
        Me.Panel3.Controls.Add(Me.btnReceiveDestination)
        Me.Panel3.Controls.Add(Me.txtSumVolume_JobInTransport)
        Me.Panel3.Controls.Add(Me.btnReturn)
        Me.Panel3.Controls.Add(Me.lblSumDoc_JobInTransport)
        Me.Panel3.Controls.Add(Me.txtSumWeight_JobInTransport)
        Me.Panel3.Controls.Add(Me.txtSumDoc_JobInTransport)
        Me.Panel3.Controls.Add(Me.txtSumQty_JobInTransport)
        Me.Panel3.Controls.Add(Me.lblSumQty_JobInTransport)
        Me.Panel3.Controls.Add(Me.Label7)
        Me.Panel3.Controls.Add(Me.lblSumWeight_JobInTransport)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1139, 74)
        Me.Panel3.TabIndex = 102
        '
        'lbCountRows2
        '
        Me.lbCountRows2.AutoSize = True
        Me.lbCountRows2.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows2.Location = New System.Drawing.Point(4, 4)
        Me.lbCountRows2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRows2.Name = "lbCountRows2"
        Me.lbCountRows2.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRows2.TabIndex = 31
        Me.lbCountRows2.Text = "ไม่พบรายการ"
        '
        'btnEditJobInTransport
        '
        Me.btnEditJobInTransport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEditJobInTransport.Image = CType(resources.GetObject("btnEditJobInTransport.Image"), System.Drawing.Image)
        Me.btnEditJobInTransport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditJobInTransport.Location = New System.Drawing.Point(4, 21)
        Me.btnEditJobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEditJobInTransport.Name = "btnEditJobInTransport"
        Me.btnEditJobInTransport.Size = New System.Drawing.Size(143, 47)
        Me.btnEditJobInTransport.TabIndex = 32
        Me.btnEditJobInTransport.Text = "แก้ไขรายการ"
        Me.btnEditJobInTransport.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditJobInTransport.UseVisualStyleBackColor = True
        '
        'lblSumVolume_JobInTransport
        '
        Me.lblSumVolume_JobInTransport.AutoSize = True
        Me.lblSumVolume_JobInTransport.Location = New System.Drawing.Point(899, 21)
        Me.lblSumVolume_JobInTransport.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumVolume_JobInTransport.Name = "lblSumVolume_JobInTransport"
        Me.lblSumVolume_JobInTransport.Size = New System.Drawing.Size(82, 17)
        Me.lblSumVolume_JobInTransport.TabIndex = 76
        Me.lblSumVolume_JobInTransport.Text = "ปริมาตรสินค้า"
        '
        'btnReceiveDestination
        '
        Me.btnReceiveDestination.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnReceiveDestination.Image = CType(resources.GetObject("btnReceiveDestination.Image"), System.Drawing.Image)
        Me.btnReceiveDestination.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReceiveDestination.Location = New System.Drawing.Point(305, 21)
        Me.btnReceiveDestination.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReceiveDestination.Name = "btnReceiveDestination"
        Me.btnReceiveDestination.Size = New System.Drawing.Size(143, 47)
        Me.btnReceiveDestination.TabIndex = 34
        Me.btnReceiveDestination.Text = "รับรถปลายทาง"
        Me.btnReceiveDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReceiveDestination.UseVisualStyleBackColor = True
        '
        'txtSumVolume_JobInTransport
        '
        Me.txtSumVolume_JobInTransport.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumVolume_JobInTransport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumVolume_JobInTransport.Location = New System.Drawing.Point(901, 38)
        Me.txtSumVolume_JobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumVolume_JobInTransport.Name = "txtSumVolume_JobInTransport"
        Me.txtSumVolume_JobInTransport.ReadOnly = True
        Me.txtSumVolume_JobInTransport.Size = New System.Drawing.Size(109, 23)
        Me.txtSumVolume_JobInTransport.TabIndex = 72
        Me.txtSumVolume_JobInTransport.Text = "0.0000"
        Me.txtSumVolume_JobInTransport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'btnReturn
        '
        Me.btnReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnReturn.Image = CType(resources.GetObject("btnReturn.Image"), System.Drawing.Image)
        Me.btnReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnReturn.Location = New System.Drawing.Point(155, 21)
        Me.btnReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.btnReturn.Name = "btnReturn"
        Me.btnReturn.Size = New System.Drawing.Size(143, 47)
        Me.btnReturn.TabIndex = 36
        Me.btnReturn.Text = "รับรถกลับ"
        Me.btnReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnReturn.UseVisualStyleBackColor = True
        '
        'lblSumDoc_JobInTransport
        '
        Me.lblSumDoc_JobInTransport.AutoSize = True
        Me.lblSumDoc_JobInTransport.Location = New System.Drawing.Point(664, 21)
        Me.lblSumDoc_JobInTransport.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumDoc_JobInTransport.Name = "lblSumDoc_JobInTransport"
        Me.lblSumDoc_JobInTransport.Size = New System.Drawing.Size(61, 17)
        Me.lblSumDoc_JobInTransport.TabIndex = 78
        Me.lblSumDoc_JobInTransport.Text = "จำนวนบิล"
        '
        'txtSumWeight_JobInTransport
        '
        Me.txtSumWeight_JobInTransport.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumWeight_JobInTransport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumWeight_JobInTransport.Location = New System.Drawing.Point(1020, 38)
        Me.txtSumWeight_JobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumWeight_JobInTransport.Name = "txtSumWeight_JobInTransport"
        Me.txtSumWeight_JobInTransport.ReadOnly = True
        Me.txtSumWeight_JobInTransport.Size = New System.Drawing.Size(109, 23)
        Me.txtSumWeight_JobInTransport.TabIndex = 71
        Me.txtSumWeight_JobInTransport.Text = "0.0000"
        Me.txtSumWeight_JobInTransport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumDoc_JobInTransport
        '
        Me.txtSumDoc_JobInTransport.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumDoc_JobInTransport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumDoc_JobInTransport.Location = New System.Drawing.Point(665, 38)
        Me.txtSumDoc_JobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumDoc_JobInTransport.Name = "txtSumDoc_JobInTransport"
        Me.txtSumDoc_JobInTransport.ReadOnly = True
        Me.txtSumDoc_JobInTransport.Size = New System.Drawing.Size(109, 23)
        Me.txtSumDoc_JobInTransport.TabIndex = 77
        Me.txtSumDoc_JobInTransport.Text = "0"
        Me.txtSumDoc_JobInTransport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumQty_JobInTransport
        '
        Me.txtSumQty_JobInTransport.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumQty_JobInTransport.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumQty_JobInTransport.Location = New System.Drawing.Point(784, 38)
        Me.txtSumQty_JobInTransport.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumQty_JobInTransport.Name = "txtSumQty_JobInTransport"
        Me.txtSumQty_JobInTransport.ReadOnly = True
        Me.txtSumQty_JobInTransport.Size = New System.Drawing.Size(109, 23)
        Me.txtSumQty_JobInTransport.TabIndex = 70
        Me.txtSumQty_JobInTransport.Text = "0"
        Me.txtSumQty_JobInTransport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSumQty_JobInTransport
        '
        Me.lblSumQty_JobInTransport.AutoSize = True
        Me.lblSumQty_JobInTransport.Location = New System.Drawing.Point(780, 21)
        Me.lblSumQty_JobInTransport.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumQty_JobInTransport.Name = "lblSumQty_JobInTransport"
        Me.lblSumQty_JobInTransport.Size = New System.Drawing.Size(63, 17)
        Me.lblSumQty_JobInTransport.TabIndex = 74
        Me.lblSumQty_JobInTransport.Text = "จำนวนชิ้น"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label7.Location = New System.Drawing.Point(617, 39)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(39, 20)
        Me.Label7.TabIndex = 73
        Me.Label7.Text = "รวม"
        '
        'lblSumWeight_JobInTransport
        '
        Me.lblSumWeight_JobInTransport.AutoSize = True
        Me.lblSumWeight_JobInTransport.Location = New System.Drawing.Point(1016, 21)
        Me.lblSumWeight_JobInTransport.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumWeight_JobInTransport.Name = "lblSumWeight_JobInTransport"
        Me.lblSumWeight_JobInTransport.Size = New System.Drawing.Size(60, 17)
        Me.lblSumWeight_JobInTransport.TabIndex = 75
        Me.lblSumWeight_JobInTransport.Text = "นน.สินค้า"
        '
        'tbpJobReturn
        '
        Me.tbpJobReturn.Controls.Add(Me.SplitContainer2)
        Me.tbpJobReturn.Location = New System.Drawing.Point(4, 25)
        Me.tbpJobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.tbpJobReturn.Name = "tbpJobReturn"
        Me.tbpJobReturn.Padding = New System.Windows.Forms.Padding(4)
        Me.tbpJobReturn.Size = New System.Drawing.Size(1147, 767)
        Me.tbpJobReturn.TabIndex = 4
        Me.tbpJobReturn.Text = "รถที่กลับถึงต้นทาง"
        Me.tbpJobReturn.UseVisualStyleBackColor = True
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(4, 4)
        Me.SplitContainer2.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.grdJobReturn)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.Panel2)
        Me.SplitContainer2.Size = New System.Drawing.Size(1139, 759)
        Me.SplitContainer2.SplitterDistance = 680
        Me.SplitContainer2.SplitterWidth = 5
        Me.SplitContainer2.TabIndex = 88
        '
        'grdJobReturn
        '
        Me.grdJobReturn.AllowUserToAddRows = False
        Me.grdJobReturn.AllowUserToDeleteRows = False
        Me.grdJobReturn.AllowUserToOrderColumns = True
        DataGridViewCellStyle5.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdJobReturn.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        Me.grdJobReturn.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJobReturn.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.grdJobReturn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdJobReturn.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.chkSelect_JobReturn, Me.col_TransportManifest_No_JobReturn, Me.col_TransportManifest_Date_JobReturn, Me.col_Time_SourceOutGate_JobReturn, Me.col_Time_DestinationInGate_JobReturn, Me.col_Time_ReturnTruckInGate_JobReturn, Me.col_Vehicle_Id_JobReturn, Me.col_Vehicle_No_JobReturn, Me.col_Route_JobReturn, Me.col_SubRoute_JobReturn, Me.col_DistributionCenter_JobReturn, Me.col_VehicleType_JobReturn, Me.col_Driver_Name_JobReturn, Me.col_CustomerShipping_JobReturn, Me.col_ShippingLocation_JobReturn, Me.col_Status_JobReturn, Me.col_Qty_JobReturn, Me.col_Bill_JobReturn, Me.col_Weight_JobReturn, Me.col_Volume_JobReturn, Me.col_Container_No2_JobReturn, Me.col_Container_No1_JobReturn, Me.col_TransportJoptype_JobReturn, Me.col_Customer_JobReturn, Me.col_Ref_No_JobReturn, Me.col_Comment_JobReturn, Me.col_Add_By_JobReturn, Me.Col_status_Chk, Me.col_Index_JobReturn})
        Me.grdJobReturn.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdJobReturn.Location = New System.Drawing.Point(0, 0)
        Me.grdJobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.grdJobReturn.Name = "grdJobReturn"
        Me.grdJobReturn.RowHeadersVisible = False
        Me.grdJobReturn.RowTemplate.Height = 24
        Me.grdJobReturn.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdJobReturn.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdJobReturn.Size = New System.Drawing.Size(1139, 680)
        Me.grdJobReturn.TabIndex = 37
        '
        'chkSelect_JobReturn
        '
        Me.chkSelect_JobReturn.DataPropertyName = "chkSelect"
        Me.chkSelect_JobReturn.FalseValue = "0"
        Me.chkSelect_JobReturn.Frozen = True
        Me.chkSelect_JobReturn.HeaderText = ""
        Me.chkSelect_JobReturn.Name = "chkSelect_JobReturn"
        Me.chkSelect_JobReturn.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.chkSelect_JobReturn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.chkSelect_JobReturn.TrueValue = "1"
        Me.chkSelect_JobReturn.Visible = False
        Me.chkSelect_JobReturn.Width = 19
        '
        'col_TransportManifest_No_JobReturn
        '
        Me.col_TransportManifest_No_JobReturn.DataPropertyName = "TransportManifest_No"
        Me.col_TransportManifest_No_JobReturn.Frozen = True
        Me.col_TransportManifest_No_JobReturn.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No_JobReturn.Name = "col_TransportManifest_No_JobReturn"
        Me.col_TransportManifest_No_JobReturn.ReadOnly = True
        '
        'col_TransportManifest_Date_JobReturn
        '
        Me.col_TransportManifest_Date_JobReturn.DataPropertyName = "TransportManifest_Date"
        Me.col_TransportManifest_Date_JobReturn.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date_JobReturn.Name = "col_TransportManifest_Date_JobReturn"
        Me.col_TransportManifest_Date_JobReturn.ReadOnly = True
        '
        'col_Time_SourceOutGate_JobReturn
        '
        Me.col_Time_SourceOutGate_JobReturn.DataPropertyName = "Time_SourceOutGate"
        Me.col_Time_SourceOutGate_JobReturn.HeaderText = "เวลาปล่อยรถ"
        Me.col_Time_SourceOutGate_JobReturn.Name = "col_Time_SourceOutGate_JobReturn"
        Me.col_Time_SourceOutGate_JobReturn.ReadOnly = True
        '
        'col_Time_DestinationInGate_JobReturn
        '
        Me.col_Time_DestinationInGate_JobReturn.DataPropertyName = "Time_DestinationInGate"
        Me.col_Time_DestinationInGate_JobReturn.HeaderText = "เวลาถึงปลายทาง"
        Me.col_Time_DestinationInGate_JobReturn.Name = "col_Time_DestinationInGate_JobReturn"
        Me.col_Time_DestinationInGate_JobReturn.ReadOnly = True
        Me.col_Time_DestinationInGate_JobReturn.Width = 110
        '
        'col_Time_ReturnTruckInGate_JobReturn
        '
        Me.col_Time_ReturnTruckInGate_JobReturn.DataPropertyName = "Time_ReturnTruckInGate"
        Me.col_Time_ReturnTruckInGate_JobReturn.HeaderText = "เวลากลับต้นทาง"
        Me.col_Time_ReturnTruckInGate_JobReturn.Name = "col_Time_ReturnTruckInGate_JobReturn"
        Me.col_Time_ReturnTruckInGate_JobReturn.ReadOnly = True
        Me.col_Time_ReturnTruckInGate_JobReturn.Width = 110
        '
        'col_Vehicle_Id_JobReturn
        '
        Me.col_Vehicle_Id_JobReturn.DataPropertyName = "Vehicle_Id"
        Me.col_Vehicle_Id_JobReturn.HeaderText = "หมายเลขรถ"
        Me.col_Vehicle_Id_JobReturn.Name = "col_Vehicle_Id_JobReturn"
        Me.col_Vehicle_Id_JobReturn.ReadOnly = True
        Me.col_Vehicle_Id_JobReturn.Visible = False
        '
        'col_Vehicle_No_JobReturn
        '
        Me.col_Vehicle_No_JobReturn.DataPropertyName = "Vehicle_License_No"
        Me.col_Vehicle_No_JobReturn.HeaderText = "ทะเบียนรถ"
        Me.col_Vehicle_No_JobReturn.Name = "col_Vehicle_No_JobReturn"
        Me.col_Vehicle_No_JobReturn.ReadOnly = True
        Me.col_Vehicle_No_JobReturn.Width = 80
        '
        'col_Route_JobReturn
        '
        Me.col_Route_JobReturn.DataPropertyName = "Route"
        Me.col_Route_JobReturn.HeaderText = "เส้นทางหลัก"
        Me.col_Route_JobReturn.Name = "col_Route_JobReturn"
        '
        'col_SubRoute_JobReturn
        '
        Me.col_SubRoute_JobReturn.DataPropertyName = "SubRoute"
        Me.col_SubRoute_JobReturn.HeaderText = "เส้นทางย่อย"
        Me.col_SubRoute_JobReturn.Name = "col_SubRoute_JobReturn"
        '
        'col_DistributionCenter_JobReturn
        '
        Me.col_DistributionCenter_JobReturn.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter_JobReturn.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter_JobReturn.Name = "col_DistributionCenter_JobReturn"
        '
        'col_VehicleType_JobReturn
        '
        Me.col_VehicleType_JobReturn.DataPropertyName = "VehicleType"
        Me.col_VehicleType_JobReturn.HeaderText = "ประเภทรถ"
        Me.col_VehicleType_JobReturn.Name = "col_VehicleType_JobReturn"
        Me.col_VehicleType_JobReturn.ReadOnly = True
        Me.col_VehicleType_JobReturn.Visible = False
        Me.col_VehicleType_JobReturn.Width = 80
        '
        'col_Driver_Name_JobReturn
        '
        Me.col_Driver_Name_JobReturn.DataPropertyName = "Driver_Name"
        Me.col_Driver_Name_JobReturn.HeaderText = "พนักงานขับรถ"
        Me.col_Driver_Name_JobReturn.Name = "col_Driver_Name_JobReturn"
        Me.col_Driver_Name_JobReturn.ReadOnly = True
        '
        'col_CustomerShipping_JobReturn
        '
        Me.col_CustomerShipping_JobReturn.DataPropertyName = "Company_Name"
        Me.col_CustomerShipping_JobReturn.HeaderText = "ผู้รับ"
        Me.col_CustomerShipping_JobReturn.Name = "col_CustomerShipping_JobReturn"
        Me.col_CustomerShipping_JobReturn.ReadOnly = True
        Me.col_CustomerShipping_JobReturn.Visible = False
        '
        'col_ShippingLocation_JobReturn
        '
        Me.col_ShippingLocation_JobReturn.DataPropertyName = "Shipping_Location_Name"
        Me.col_ShippingLocation_JobReturn.HeaderText = "ปลายทาง"
        Me.col_ShippingLocation_JobReturn.Name = "col_ShippingLocation_JobReturn"
        Me.col_ShippingLocation_JobReturn.ReadOnly = True
        '
        'col_Status_JobReturn
        '
        Me.col_Status_JobReturn.DataPropertyName = "Status"
        Me.col_Status_JobReturn.HeaderText = "สถานะใบคุม"
        Me.col_Status_JobReturn.Name = "col_Status_JobReturn"
        Me.col_Status_JobReturn.ReadOnly = True
        '
        'col_Qty_JobReturn
        '
        Me.col_Qty_JobReturn.DataPropertyName = "Qty_Sum"
        Me.col_Qty_JobReturn.HeaderText = "จน.ชิ้น"
        Me.col_Qty_JobReturn.Name = "col_Qty_JobReturn"
        Me.col_Qty_JobReturn.ReadOnly = True
        Me.col_Qty_JobReturn.Width = 70
        '
        'col_Bill_JobReturn
        '
        Me.col_Bill_JobReturn.DataPropertyName = "Bill_Sum"
        Me.col_Bill_JobReturn.HeaderText = "จำนวนบิล"
        Me.col_Bill_JobReturn.Name = "col_Bill_JobReturn"
        Me.col_Bill_JobReturn.ReadOnly = True
        Me.col_Bill_JobReturn.Width = 90
        '
        'col_Weight_JobReturn
        '
        Me.col_Weight_JobReturn.DataPropertyName = "Weight_Sum"
        Me.col_Weight_JobReturn.HeaderText = "นน. สินค้า/พิกัด"
        Me.col_Weight_JobReturn.Name = "col_Weight_JobReturn"
        Me.col_Weight_JobReturn.ReadOnly = True
        Me.col_Weight_JobReturn.Width = 110
        '
        'col_Volume_JobReturn
        '
        Me.col_Volume_JobReturn.DataPropertyName = "Volume_Sum"
        Me.col_Volume_JobReturn.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.col_Volume_JobReturn.Name = "col_Volume_JobReturn"
        Me.col_Volume_JobReturn.ReadOnly = True
        Me.col_Volume_JobReturn.Width = 120
        '
        'col_Container_No2_JobReturn
        '
        Me.col_Container_No2_JobReturn.DataPropertyName = "Container_No1"
        Me.col_Container_No2_JobReturn.HeaderText = "หมายเลขตู้ 1"
        Me.col_Container_No2_JobReturn.Name = "col_Container_No2_JobReturn"
        Me.col_Container_No2_JobReturn.ReadOnly = True
        '
        'col_Container_No1_JobReturn
        '
        Me.col_Container_No1_JobReturn.DataPropertyName = "Container_No2"
        Me.col_Container_No1_JobReturn.HeaderText = "หมายเลขตู้ 2"
        Me.col_Container_No1_JobReturn.Name = "col_Container_No1_JobReturn"
        Me.col_Container_No1_JobReturn.ReadOnly = True
        '
        'col_TransportJoptype_JobReturn
        '
        Me.col_TransportJoptype_JobReturn.DataPropertyName = "TransportJobType"
        Me.col_TransportJoptype_JobReturn.HeaderText = "ประเภทงาน"
        Me.col_TransportJoptype_JobReturn.Name = "col_TransportJoptype_JobReturn"
        Me.col_TransportJoptype_JobReturn.Width = 90
        '
        'col_Customer_JobReturn
        '
        Me.col_Customer_JobReturn.DataPropertyName = "Customer_Name"
        Me.col_Customer_JobReturn.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.col_Customer_JobReturn.Name = "col_Customer_JobReturn"
        Me.col_Customer_JobReturn.Visible = False
        Me.col_Customer_JobReturn.Width = 120
        '
        'col_Ref_No_JobReturn
        '
        Me.col_Ref_No_JobReturn.DataPropertyName = "Str1"
        Me.col_Ref_No_JobReturn.HeaderText = "อ้างอิง 1"
        Me.col_Ref_No_JobReturn.Name = "col_Ref_No_JobReturn"
        Me.col_Ref_No_JobReturn.Visible = False
        '
        'col_Comment_JobReturn
        '
        Me.col_Comment_JobReturn.DataPropertyName = "Comment"
        Me.col_Comment_JobReturn.HeaderText = "หมายเหตุ"
        Me.col_Comment_JobReturn.Name = "col_Comment_JobReturn"
        '
        'col_Add_By_JobReturn
        '
        Me.col_Add_By_JobReturn.DataPropertyName = "Add_By"
        Me.col_Add_By_JobReturn.HeaderText = "ผู้ออกเอกสาร"
        Me.col_Add_By_JobReturn.Name = "col_Add_By_JobReturn"
        Me.col_Add_By_JobReturn.Width = 150
        '
        'Col_status_Chk
        '
        Me.Col_status_Chk.DataPropertyName = "status_id"
        Me.Col_status_Chk.HeaderText = "status_id"
        Me.Col_status_Chk.Name = "Col_status_Chk"
        Me.Col_status_Chk.Visible = False
        '
        'col_Index_JobReturn
        '
        Me.col_Index_JobReturn.DataPropertyName = "TransportManifest_Index"
        Me.col_Index_JobReturn.HeaderText = "รหัสระบบ "
        Me.col_Index_JobReturn.Name = "col_Index_JobReturn"
        Me.col_Index_JobReturn.ReadOnly = True
        Me.col_Index_JobReturn.Visible = False
        Me.col_Index_JobReturn.Width = 110
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.Panel2.Controls.Add(Me.lbCountRows3)
        Me.Panel2.Controls.Add(Me.lblSumDoc_JobReturn)
        Me.Panel2.Controls.Add(Me.btnEditJobReturn)
        Me.Panel2.Controls.Add(Me.txtSumDoc_JobReturn)
        Me.Panel2.Controls.Add(Me.lblSumWeight_JobReturn)
        Me.Panel2.Controls.Add(Me.btnConfirm)
        Me.Panel2.Controls.Add(Me.txtSumWeight_JobReturn)
        Me.Panel2.Controls.Add(Me.txtSumVolume_JobReturn)
        Me.Panel2.Controls.Add(Me.lblSumQty_JobReturn)
        Me.Panel2.Controls.Add(Me.txtSumQty_JobReturn)
        Me.Panel2.Controls.Add(Me.lbltxtSumVolume_JobReturn)
        Me.Panel2.Controls.Add(Me.Label12)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1139, 74)
        Me.Panel2.TabIndex = 102
        '
        'lbCountRows3
        '
        Me.lbCountRows3.AutoSize = True
        Me.lbCountRows3.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows3.Location = New System.Drawing.Point(4, 4)
        Me.lbCountRows3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRows3.Name = "lbCountRows3"
        Me.lbCountRows3.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRows3.TabIndex = 38
        Me.lbCountRows3.Text = "ไม่พบรายการ"
        '
        'lblSumDoc_JobReturn
        '
        Me.lblSumDoc_JobReturn.AutoSize = True
        Me.lblSumDoc_JobReturn.Location = New System.Drawing.Point(665, 23)
        Me.lblSumDoc_JobReturn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumDoc_JobReturn.Name = "lblSumDoc_JobReturn"
        Me.lblSumDoc_JobReturn.Size = New System.Drawing.Size(61, 17)
        Me.lblSumDoc_JobReturn.TabIndex = 87
        Me.lblSumDoc_JobReturn.Text = "จำนวนบิล"
        '
        'btnEditJobReturn
        '
        Me.btnEditJobReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnEditJobReturn.Image = CType(resources.GetObject("btnEditJobReturn.Image"), System.Drawing.Image)
        Me.btnEditJobReturn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnEditJobReturn.Location = New System.Drawing.Point(4, 21)
        Me.btnEditJobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEditJobReturn.Name = "btnEditJobReturn"
        Me.btnEditJobReturn.Size = New System.Drawing.Size(143, 47)
        Me.btnEditJobReturn.TabIndex = 39
        Me.btnEditJobReturn.Text = "แก้ไขรายการ"
        Me.btnEditJobReturn.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnEditJobReturn.UseVisualStyleBackColor = True
        '
        'txtSumDoc_JobReturn
        '
        Me.txtSumDoc_JobReturn.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumDoc_JobReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumDoc_JobReturn.Location = New System.Drawing.Point(667, 41)
        Me.txtSumDoc_JobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumDoc_JobReturn.Name = "txtSumDoc_JobReturn"
        Me.txtSumDoc_JobReturn.ReadOnly = True
        Me.txtSumDoc_JobReturn.Size = New System.Drawing.Size(109, 23)
        Me.txtSumDoc_JobReturn.TabIndex = 86
        Me.txtSumDoc_JobReturn.Text = "0"
        Me.txtSumDoc_JobReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSumWeight_JobReturn
        '
        Me.lblSumWeight_JobReturn.AutoSize = True
        Me.lblSumWeight_JobReturn.Location = New System.Drawing.Point(1021, 20)
        Me.lblSumWeight_JobReturn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumWeight_JobReturn.Name = "lblSumWeight_JobReturn"
        Me.lblSumWeight_JobReturn.Size = New System.Drawing.Size(60, 17)
        Me.lblSumWeight_JobReturn.TabIndex = 84
        Me.lblSumWeight_JobReturn.Text = "นน.สินค้า"
        '
        'btnConfirm
        '
        Me.btnConfirm.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnConfirm.Image = CType(resources.GetObject("btnConfirm.Image"), System.Drawing.Image)
        Me.btnConfirm.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnConfirm.Location = New System.Drawing.Point(155, 21)
        Me.btnConfirm.Margin = New System.Windows.Forms.Padding(4)
        Me.btnConfirm.Name = "btnConfirm"
        Me.btnConfirm.Size = New System.Drawing.Size(147, 47)
        Me.btnConfirm.TabIndex = 43
        Me.btnConfirm.Text = "บันทึกผลการส่ง"
        Me.btnConfirm.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnConfirm.UseVisualStyleBackColor = True
        '
        'txtSumWeight_JobReturn
        '
        Me.txtSumWeight_JobReturn.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumWeight_JobReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumWeight_JobReturn.Location = New System.Drawing.Point(1021, 39)
        Me.txtSumWeight_JobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumWeight_JobReturn.Name = "txtSumWeight_JobReturn"
        Me.txtSumWeight_JobReturn.ReadOnly = True
        Me.txtSumWeight_JobReturn.Size = New System.Drawing.Size(109, 23)
        Me.txtSumWeight_JobReturn.TabIndex = 80
        Me.txtSumWeight_JobReturn.Text = "0.0000"
        Me.txtSumWeight_JobReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumVolume_JobReturn
        '
        Me.txtSumVolume_JobReturn.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumVolume_JobReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumVolume_JobReturn.Location = New System.Drawing.Point(903, 41)
        Me.txtSumVolume_JobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumVolume_JobReturn.Name = "txtSumVolume_JobReturn"
        Me.txtSumVolume_JobReturn.ReadOnly = True
        Me.txtSumVolume_JobReturn.Size = New System.Drawing.Size(109, 23)
        Me.txtSumVolume_JobReturn.TabIndex = 81
        Me.txtSumVolume_JobReturn.Text = "0.0000"
        Me.txtSumVolume_JobReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSumQty_JobReturn
        '
        Me.lblSumQty_JobReturn.AutoSize = True
        Me.lblSumQty_JobReturn.Location = New System.Drawing.Point(781, 23)
        Me.lblSumQty_JobReturn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumQty_JobReturn.Name = "lblSumQty_JobReturn"
        Me.lblSumQty_JobReturn.Size = New System.Drawing.Size(63, 17)
        Me.lblSumQty_JobReturn.TabIndex = 83
        Me.lblSumQty_JobReturn.Text = "จำนวนชิ้น"
        '
        'txtSumQty_JobReturn
        '
        Me.txtSumQty_JobReturn.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumQty_JobReturn.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumQty_JobReturn.Location = New System.Drawing.Point(785, 41)
        Me.txtSumQty_JobReturn.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumQty_JobReturn.Name = "txtSumQty_JobReturn"
        Me.txtSumQty_JobReturn.ReadOnly = True
        Me.txtSumQty_JobReturn.Size = New System.Drawing.Size(109, 23)
        Me.txtSumQty_JobReturn.TabIndex = 79
        Me.txtSumQty_JobReturn.Text = "0"
        Me.txtSumQty_JobReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lbltxtSumVolume_JobReturn
        '
        Me.lbltxtSumVolume_JobReturn.AutoSize = True
        Me.lbltxtSumVolume_JobReturn.Location = New System.Drawing.Point(900, 23)
        Me.lbltxtSumVolume_JobReturn.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbltxtSumVolume_JobReturn.Name = "lbltxtSumVolume_JobReturn"
        Me.lbltxtSumVolume_JobReturn.Size = New System.Drawing.Size(82, 17)
        Me.lbltxtSumVolume_JobReturn.TabIndex = 85
        Me.lbltxtSumVolume_JobReturn.Text = "ปริมาตรสินค้า"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label12.Location = New System.Drawing.Point(619, 46)
        Me.Label12.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(39, 20)
        Me.Label12.TabIndex = 82
        Me.Label12.Text = "รวม"
        '
        'tabManifestComplete
        '
        Me.tabManifestComplete.Controls.Add(Me.SplitContainer1)
        Me.tabManifestComplete.Location = New System.Drawing.Point(4, 25)
        Me.tabManifestComplete.Margin = New System.Windows.Forms.Padding(4)
        Me.tabManifestComplete.Name = "tabManifestComplete"
        Me.tabManifestComplete.Padding = New System.Windows.Forms.Padding(4)
        Me.tabManifestComplete.Size = New System.Drawing.Size(1147, 767)
        Me.tabManifestComplete.TabIndex = 5
        Me.tabManifestComplete.Text = "จัดส่งเรียบร้อยแล้ว"
        Me.tabManifestComplete.UseVisualStyleBackColor = True
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(4, 4)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.grdJob_Complete)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.PanelOrderItemButton)
        Me.SplitContainer1.Size = New System.Drawing.Size(1139, 759)
        Me.SplitContainer1.SplitterDistance = 680
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 101
        '
        'grdJob_Complete
        '
        Me.grdJob_Complete.AllowUserToAddRows = False
        Me.grdJob_Complete.AllowUserToDeleteRows = False
        Me.grdJob_Complete.AllowUserToOrderColumns = True
        DataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.grdJob_Complete.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        Me.grdJob_Complete.BackgroundColor = System.Drawing.Color.White
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle8.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        DataGridViewCellStyle8.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle8.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle8.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grdJob_Complete.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        Me.grdJob_Complete.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdJob_Complete.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.col_TransportManifest_No_JobComplete, Me.col_TransportManifest_Date_JobComplete, Me.col_Index_JobComplete, Me.col_Time_SourceOutGate_JobComplete, Me.col_Time_DestinationInGate_JobComplete, Me.col_Time_ReturnTruckInGate_JobComplete, Me.col_Vehicle_Id_JobComplete, Me.col_Vehicle_No_JobComplete, Me.col_VehicleType_JobComplete, Me.col_Driver_Name_JobComplete, Me.col_CustomerShipping_JobComplete, Me.col_ShippingLocation_JobComplete, Me.col_Status_JobComplete, Me.col_Weight_JobComplete, Me.col_Volume_JobComplete, Me.col_Qty_JobComplete, Me.col_Bill_JobComplete, Me.col_Container_No2_JobComplete, Me.col_Container_No1_JobComplete, Me.col_TransportJoptype_JobComplete, Me.col_Customer_JobComplete, Me.col_Route_JobComplete, Me.col_DistributionCenter_JobComplete, Me.col_Ref_No_JobComplete, Me.col_Comment_JobComplete, Me.col_Add_By_JobComplete})
        Me.grdJob_Complete.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grdJob_Complete.Location = New System.Drawing.Point(0, 0)
        Me.grdJob_Complete.Margin = New System.Windows.Forms.Padding(4)
        Me.grdJob_Complete.Name = "grdJob_Complete"
        Me.grdJob_Complete.RowHeadersVisible = False
        Me.grdJob_Complete.RowTemplate.Height = 24
        Me.grdJob_Complete.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.grdJob_Complete.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grdJob_Complete.Size = New System.Drawing.Size(1139, 680)
        Me.grdJob_Complete.TabIndex = 88
        '
        'col_TransportManifest_No_JobComplete
        '
        Me.col_TransportManifest_No_JobComplete.DataPropertyName = "TransportManifest_No"
        Me.col_TransportManifest_No_JobComplete.Frozen = True
        Me.col_TransportManifest_No_JobComplete.HeaderText = "เลขที่ใบคุมรถ"
        Me.col_TransportManifest_No_JobComplete.Name = "col_TransportManifest_No_JobComplete"
        '
        'col_TransportManifest_Date_JobComplete
        '
        Me.col_TransportManifest_Date_JobComplete.DataPropertyName = "TransportManifest_Date"
        Me.col_TransportManifest_Date_JobComplete.Frozen = True
        Me.col_TransportManifest_Date_JobComplete.HeaderText = "วันที่ใบคุมรถ"
        Me.col_TransportManifest_Date_JobComplete.Name = "col_TransportManifest_Date_JobComplete"
        '
        'col_Index_JobComplete
        '
        Me.col_Index_JobComplete.DataPropertyName = "TransportManifest_Index"
        Me.col_Index_JobComplete.HeaderText = "รหัสระบบ "
        Me.col_Index_JobComplete.Name = "col_Index_JobComplete"
        Me.col_Index_JobComplete.Visible = False
        Me.col_Index_JobComplete.Width = 110
        '
        'col_Time_SourceOutGate_JobComplete
        '
        Me.col_Time_SourceOutGate_JobComplete.DataPropertyName = "Time_SourceOutGate"
        Me.col_Time_SourceOutGate_JobComplete.HeaderText = "เวลาปล่อยรถ"
        Me.col_Time_SourceOutGate_JobComplete.Name = "col_Time_SourceOutGate_JobComplete"
        '
        'col_Time_DestinationInGate_JobComplete
        '
        Me.col_Time_DestinationInGate_JobComplete.DataPropertyName = "Time_DestinationInGate"
        Me.col_Time_DestinationInGate_JobComplete.HeaderText = "เวลาถึงปลายทาง"
        Me.col_Time_DestinationInGate_JobComplete.Name = "col_Time_DestinationInGate_JobComplete"
        Me.col_Time_DestinationInGate_JobComplete.Width = 110
        '
        'col_Time_ReturnTruckInGate_JobComplete
        '
        Me.col_Time_ReturnTruckInGate_JobComplete.DataPropertyName = "Time_ReturnTruckInGate"
        Me.col_Time_ReturnTruckInGate_JobComplete.HeaderText = "เวลากลับต้นทาง"
        Me.col_Time_ReturnTruckInGate_JobComplete.Name = "col_Time_ReturnTruckInGate_JobComplete"
        Me.col_Time_ReturnTruckInGate_JobComplete.Width = 110
        '
        'col_Vehicle_Id_JobComplete
        '
        Me.col_Vehicle_Id_JobComplete.DataPropertyName = "Vehicle_Id"
        Me.col_Vehicle_Id_JobComplete.HeaderText = "หมายเลขรถ"
        Me.col_Vehicle_Id_JobComplete.Name = "col_Vehicle_Id_JobComplete"
        Me.col_Vehicle_Id_JobComplete.Visible = False
        '
        'col_Vehicle_No_JobComplete
        '
        Me.col_Vehicle_No_JobComplete.DataPropertyName = "Vehicle_License_No"
        Me.col_Vehicle_No_JobComplete.HeaderText = "ทะเบียนรถ"
        Me.col_Vehicle_No_JobComplete.Name = "col_Vehicle_No_JobComplete"
        Me.col_Vehicle_No_JobComplete.Width = 80
        '
        'col_VehicleType_JobComplete
        '
        Me.col_VehicleType_JobComplete.DataPropertyName = "VehicleType"
        Me.col_VehicleType_JobComplete.HeaderText = "ประเภทรถ"
        Me.col_VehicleType_JobComplete.Name = "col_VehicleType_JobComplete"
        Me.col_VehicleType_JobComplete.Visible = False
        Me.col_VehicleType_JobComplete.Width = 80
        '
        'col_Driver_Name_JobComplete
        '
        Me.col_Driver_Name_JobComplete.DataPropertyName = "Driver_Name"
        Me.col_Driver_Name_JobComplete.HeaderText = "พนักงานขับรถ"
        Me.col_Driver_Name_JobComplete.Name = "col_Driver_Name_JobComplete"
        '
        'col_CustomerShipping_JobComplete
        '
        Me.col_CustomerShipping_JobComplete.DataPropertyName = "Company_Name"
        Me.col_CustomerShipping_JobComplete.HeaderText = "ผู้รับ"
        Me.col_CustomerShipping_JobComplete.Name = "col_CustomerShipping_JobComplete"
        Me.col_CustomerShipping_JobComplete.Visible = False
        '
        'col_ShippingLocation_JobComplete
        '
        Me.col_ShippingLocation_JobComplete.DataPropertyName = "Shipping_Location_Name"
        Me.col_ShippingLocation_JobComplete.HeaderText = "ปลายทาง"
        Me.col_ShippingLocation_JobComplete.Name = "col_ShippingLocation_JobComplete"
        '
        'col_Status_JobComplete
        '
        Me.col_Status_JobComplete.DataPropertyName = "Status"
        Me.col_Status_JobComplete.HeaderText = "สถานะ"
        Me.col_Status_JobComplete.Name = "col_Status_JobComplete"
        '
        'col_Weight_JobComplete
        '
        Me.col_Weight_JobComplete.DataPropertyName = "Weight_Sum"
        Me.col_Weight_JobComplete.HeaderText = "นน. สินค้า/พิกัด"
        Me.col_Weight_JobComplete.Name = "col_Weight_JobComplete"
        Me.col_Weight_JobComplete.Width = 110
        '
        'col_Volume_JobComplete
        '
        Me.col_Volume_JobComplete.DataPropertyName = "Volume_Sum"
        Me.col_Volume_JobComplete.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.col_Volume_JobComplete.Name = "col_Volume_JobComplete"
        Me.col_Volume_JobComplete.Width = 120
        '
        'col_Qty_JobComplete
        '
        Me.col_Qty_JobComplete.DataPropertyName = "Qty_Sum"
        Me.col_Qty_JobComplete.HeaderText = "จน.ชิ้น"
        Me.col_Qty_JobComplete.Name = "col_Qty_JobComplete"
        Me.col_Qty_JobComplete.Width = 70
        '
        'col_Bill_JobComplete
        '
        Me.col_Bill_JobComplete.DataPropertyName = "Bill_Sum"
        Me.col_Bill_JobComplete.HeaderText = "จำนวนบิล"
        Me.col_Bill_JobComplete.Name = "col_Bill_JobComplete"
        Me.col_Bill_JobComplete.Width = 90
        '
        'col_Container_No2_JobComplete
        '
        Me.col_Container_No2_JobComplete.DataPropertyName = "Container_No1"
        Me.col_Container_No2_JobComplete.HeaderText = "หมายเลขตู้ 1"
        Me.col_Container_No2_JobComplete.Name = "col_Container_No2_JobComplete"
        '
        'col_Container_No1_JobComplete
        '
        Me.col_Container_No1_JobComplete.DataPropertyName = "Container_No2"
        Me.col_Container_No1_JobComplete.HeaderText = "หมายเลขตู้ 2"
        Me.col_Container_No1_JobComplete.Name = "col_Container_No1_JobComplete"
        '
        'col_TransportJoptype_JobComplete
        '
        Me.col_TransportJoptype_JobComplete.DataPropertyName = "TransportJobType"
        Me.col_TransportJoptype_JobComplete.HeaderText = "ประเภทงาน"
        Me.col_TransportJoptype_JobComplete.Name = "col_TransportJoptype_JobComplete"
        Me.col_TransportJoptype_JobComplete.Width = 90
        '
        'col_Customer_JobComplete
        '
        Me.col_Customer_JobComplete.DataPropertyName = "Customer_Name"
        Me.col_Customer_JobComplete.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.col_Customer_JobComplete.Name = "col_Customer_JobComplete"
        Me.col_Customer_JobComplete.Width = 120
        '
        'col_Route_JobComplete
        '
        Me.col_Route_JobComplete.DataPropertyName = "Route"
        Me.col_Route_JobComplete.HeaderText = "เส้นทาง"
        Me.col_Route_JobComplete.Name = "col_Route_JobComplete"
        '
        'col_DistributionCenter_JobComplete
        '
        Me.col_DistributionCenter_JobComplete.DataPropertyName = "DistributionCenter"
        Me.col_DistributionCenter_JobComplete.HeaderText = "ศูนย์กระจาย"
        Me.col_DistributionCenter_JobComplete.Name = "col_DistributionCenter_JobComplete"
        '
        'col_Ref_No_JobComplete
        '
        Me.col_Ref_No_JobComplete.DataPropertyName = "Str1"
        Me.col_Ref_No_JobComplete.HeaderText = "อ้างอิง 1"
        Me.col_Ref_No_JobComplete.Name = "col_Ref_No_JobComplete"
        '
        'col_Comment_JobComplete
        '
        Me.col_Comment_JobComplete.DataPropertyName = "Comment"
        Me.col_Comment_JobComplete.HeaderText = "หมายเหตุ"
        Me.col_Comment_JobComplete.Name = "col_Comment_JobComplete"
        '
        'col_Add_By_JobComplete
        '
        Me.col_Add_By_JobComplete.DataPropertyName = "Add_By"
        Me.col_Add_By_JobComplete.HeaderText = "ผู้ออกเอกสาร"
        Me.col_Add_By_JobComplete.Name = "col_Add_By_JobComplete"
        Me.col_Add_By_JobComplete.Width = 150
        '
        'PanelOrderItemButton
        '
        Me.PanelOrderItemButton.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.PanelOrderItemButton.Controls.Add(Me.btnInterface)
        Me.PanelOrderItemButton.Controls.Add(Me.lblSumWeight_JobComplete)
        Me.PanelOrderItemButton.Controls.Add(Me.lbCountRows4)
        Me.PanelOrderItemButton.Controls.Add(Me.Label5)
        Me.PanelOrderItemButton.Controls.Add(Me.lblSumDoc_JobComplete)
        Me.PanelOrderItemButton.Controls.Add(Me.txtSumWeight_JobComplete)
        Me.PanelOrderItemButton.Controls.Add(Me.txtSumDoc_JobComplete)
        Me.PanelOrderItemButton.Controls.Add(Me.txtSumQty_Jobcomplete)
        Me.PanelOrderItemButton.Controls.Add(Me.lblSumVolume_JobComplete)
        Me.PanelOrderItemButton.Controls.Add(Me.lblSumQty_JobComplete)
        Me.PanelOrderItemButton.Controls.Add(Me.txtSumVolume_JobComplete)
        Me.PanelOrderItemButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PanelOrderItemButton.Location = New System.Drawing.Point(0, 0)
        Me.PanelOrderItemButton.Margin = New System.Windows.Forms.Padding(4)
        Me.PanelOrderItemButton.Name = "PanelOrderItemButton"
        Me.PanelOrderItemButton.Size = New System.Drawing.Size(1139, 74)
        Me.PanelOrderItemButton.TabIndex = 100
        '
        'btnInterface
        '
        Me.btnInterface.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.btnInterface.Image = Global.WMS_Site_TopCharoen.My.Resources.Resources.ดึงข้อมูล
        Me.btnInterface.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnInterface.Location = New System.Drawing.Point(7, 22)
        Me.btnInterface.Margin = New System.Windows.Forms.Padding(4)
        Me.btnInterface.Name = "btnInterface"
        Me.btnInterface.Size = New System.Drawing.Size(143, 47)
        Me.btnInterface.TabIndex = 100
        Me.btnInterface.Text = "Interface OMS"
        Me.btnInterface.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnInterface.UseVisualStyleBackColor = True
        '
        'lblSumWeight_JobComplete
        '
        Me.lblSumWeight_JobComplete.AutoSize = True
        Me.lblSumWeight_JobComplete.Location = New System.Drawing.Point(1013, 22)
        Me.lblSumWeight_JobComplete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumWeight_JobComplete.Name = "lblSumWeight_JobComplete"
        Me.lblSumWeight_JobComplete.Size = New System.Drawing.Size(60, 17)
        Me.lblSumWeight_JobComplete.TabIndex = 96
        Me.lblSumWeight_JobComplete.Text = "นน.สินค้า"
        '
        'lbCountRows4
        '
        Me.lbCountRows4.AutoSize = True
        Me.lbCountRows4.ForeColor = System.Drawing.Color.Blue
        Me.lbCountRows4.Location = New System.Drawing.Point(4, 4)
        Me.lbCountRows4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbCountRows4.Name = "lbCountRows4"
        Me.lbCountRows4.Size = New System.Drawing.Size(81, 17)
        Me.lbCountRows4.TabIndex = 89
        Me.lbCountRows4.Text = "ไม่พบรายการ"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.Label5.Location = New System.Drawing.Point(612, 41)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(39, 20)
        Me.Label5.TabIndex = 94
        Me.Label5.Text = "รวม"
        '
        'lblSumDoc_JobComplete
        '
        Me.lblSumDoc_JobComplete.AutoSize = True
        Me.lblSumDoc_JobComplete.Location = New System.Drawing.Point(659, 22)
        Me.lblSumDoc_JobComplete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumDoc_JobComplete.Name = "lblSumDoc_JobComplete"
        Me.lblSumDoc_JobComplete.Size = New System.Drawing.Size(61, 17)
        Me.lblSumDoc_JobComplete.TabIndex = 99
        Me.lblSumDoc_JobComplete.Text = "จำนวนบิล"
        '
        'txtSumWeight_JobComplete
        '
        Me.txtSumWeight_JobComplete.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumWeight_JobComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumWeight_JobComplete.Location = New System.Drawing.Point(1016, 39)
        Me.txtSumWeight_JobComplete.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumWeight_JobComplete.Name = "txtSumWeight_JobComplete"
        Me.txtSumWeight_JobComplete.ReadOnly = True
        Me.txtSumWeight_JobComplete.Size = New System.Drawing.Size(109, 23)
        Me.txtSumWeight_JobComplete.TabIndex = 92
        Me.txtSumWeight_JobComplete.Text = "0.0000"
        Me.txtSumWeight_JobComplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumDoc_JobComplete
        '
        Me.txtSumDoc_JobComplete.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumDoc_JobComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumDoc_JobComplete.Location = New System.Drawing.Point(660, 39)
        Me.txtSumDoc_JobComplete.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumDoc_JobComplete.Name = "txtSumDoc_JobComplete"
        Me.txtSumDoc_JobComplete.ReadOnly = True
        Me.txtSumDoc_JobComplete.Size = New System.Drawing.Size(109, 23)
        Me.txtSumDoc_JobComplete.TabIndex = 98
        Me.txtSumDoc_JobComplete.Text = "0"
        Me.txtSumDoc_JobComplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSumQty_Jobcomplete
        '
        Me.txtSumQty_Jobcomplete.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumQty_Jobcomplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumQty_Jobcomplete.Location = New System.Drawing.Point(779, 39)
        Me.txtSumQty_Jobcomplete.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumQty_Jobcomplete.Name = "txtSumQty_Jobcomplete"
        Me.txtSumQty_Jobcomplete.ReadOnly = True
        Me.txtSumQty_Jobcomplete.Size = New System.Drawing.Size(109, 23)
        Me.txtSumQty_Jobcomplete.TabIndex = 91
        Me.txtSumQty_Jobcomplete.Text = "0"
        Me.txtSumQty_Jobcomplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSumVolume_JobComplete
        '
        Me.lblSumVolume_JobComplete.AutoSize = True
        Me.lblSumVolume_JobComplete.Location = New System.Drawing.Point(897, 22)
        Me.lblSumVolume_JobComplete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumVolume_JobComplete.Name = "lblSumVolume_JobComplete"
        Me.lblSumVolume_JobComplete.Size = New System.Drawing.Size(82, 17)
        Me.lblSumVolume_JobComplete.TabIndex = 97
        Me.lblSumVolume_JobComplete.Text = "ปริมาตรสินค้า"
        '
        'lblSumQty_JobComplete
        '
        Me.lblSumQty_JobComplete.AutoSize = True
        Me.lblSumQty_JobComplete.Location = New System.Drawing.Point(775, 22)
        Me.lblSumQty_JobComplete.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblSumQty_JobComplete.Name = "lblSumQty_JobComplete"
        Me.lblSumQty_JobComplete.Size = New System.Drawing.Size(63, 17)
        Me.lblSumQty_JobComplete.TabIndex = 95
        Me.lblSumQty_JobComplete.Text = "จำนวนชิ้น"
        '
        'txtSumVolume_JobComplete
        '
        Me.txtSumVolume_JobComplete.BackColor = System.Drawing.Color.LemonChiffon
        Me.txtSumVolume_JobComplete.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(222, Byte))
        Me.txtSumVolume_JobComplete.Location = New System.Drawing.Point(897, 39)
        Me.txtSumVolume_JobComplete.Margin = New System.Windows.Forms.Padding(4)
        Me.txtSumVolume_JobComplete.Name = "txtSumVolume_JobComplete"
        Me.txtSumVolume_JobComplete.ReadOnly = True
        Me.txtSumVolume_JobComplete.Size = New System.Drawing.Size(109, 23)
        Me.txtSumVolume_JobComplete.TabIndex = 93
        Me.txtSumVolume_JobComplete.Text = "0.0000"
        Me.txtSumVolume_JobComplete.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Button1
        '
        Me.Button1.Image = CType(resources.GetObject("Button1.Image"), System.Drawing.Image)
        Me.Button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Button1.Location = New System.Drawing.Point(144, 21)
        Me.Button1.Margin = New System.Windows.Forms.Padding(4)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(143, 47)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "พิมพ์ใบคุมของ"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'pnlHide
        '
        Me.pnlHide.Controls.Add(Me.Button1)
        Me.pnlHide.Controls.Add(Me.lblStatus)
        Me.pnlHide.Controls.Add(Me.cboDocumentStatus)
        Me.pnlHide.Location = New System.Drawing.Point(204, 812)
        Me.pnlHide.Margin = New System.Windows.Forms.Padding(4)
        Me.pnlHide.Name = "pnlHide"
        Me.pnlHide.Size = New System.Drawing.Size(308, 85)
        Me.pnlHide.TabIndex = 22
        '
        'DataGridViewCheckBoxColumn1
        '
        Me.DataGridViewCheckBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewCheckBoxColumn1.DataPropertyName = "chkSelect"
        Me.DataGridViewCheckBoxColumn1.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn1.Frozen = True
        Me.DataGridViewCheckBoxColumn1.HeaderText = ""
        Me.DataGridViewCheckBoxColumn1.Name = "DataGridViewCheckBoxColumn1"
        Me.DataGridViewCheckBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn1.TrueValue = "1"
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "TransportManifest_Index"
        Me.DataGridViewTextBoxColumn1.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.Visible = False
        Me.DataGridViewTextBoxColumn1.Width = 110
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "TransportManifest_No"
        Me.DataGridViewTextBoxColumn2.Frozen = True
        Me.DataGridViewTextBoxColumn2.HeaderText = "เลขที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "TransportManifest_Date"
        Me.DataGridViewTextBoxColumn3.HeaderText = "วันที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.Visible = False
        Me.DataGridViewTextBoxColumn3.Width = 150
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "Vehicle_Id"
        Me.DataGridViewTextBoxColumn4.HeaderText = "หมายเลขรถ"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.Visible = False
        Me.DataGridViewTextBoxColumn4.Width = 80
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "Vehicle_No"
        Me.DataGridViewTextBoxColumn5.HeaderText = "ทะเบียนรถ"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.Visible = False
        Me.DataGridViewTextBoxColumn5.Width = 80
        '
        'DataGridViewTextBoxColumn6
        '
        Me.DataGridViewTextBoxColumn6.DataPropertyName = "VehicleType"
        Me.DataGridViewTextBoxColumn6.HeaderText = "ประเภทรถ"
        Me.DataGridViewTextBoxColumn6.Name = "DataGridViewTextBoxColumn6"
        Me.DataGridViewTextBoxColumn6.Visible = False
        Me.DataGridViewTextBoxColumn6.Width = 80
        '
        'DataGridViewTextBoxColumn7
        '
        Me.DataGridViewTextBoxColumn7.DataPropertyName = "Driver_Name"
        Me.DataGridViewTextBoxColumn7.HeaderText = "พนักงานขับรถ"
        Me.DataGridViewTextBoxColumn7.Name = "DataGridViewTextBoxColumn7"
        '
        'DataGridViewTextBoxColumn8
        '
        Me.DataGridViewTextBoxColumn8.DataPropertyName = "Company_Name"
        Me.DataGridViewTextBoxColumn8.HeaderText = "ผู้รับ"
        Me.DataGridViewTextBoxColumn8.Name = "DataGridViewTextBoxColumn8"
        Me.DataGridViewTextBoxColumn8.Visible = False
        '
        'DataGridViewTextBoxColumn9
        '
        Me.DataGridViewTextBoxColumn9.DataPropertyName = "Shipping_Location_Name"
        Me.DataGridViewTextBoxColumn9.HeaderText = "ปลายทาง"
        Me.DataGridViewTextBoxColumn9.Name = "DataGridViewTextBoxColumn9"
        Me.DataGridViewTextBoxColumn9.Visible = False
        '
        'DataGridViewTextBoxColumn10
        '
        Me.DataGridViewTextBoxColumn10.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn10.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn10.Name = "DataGridViewTextBoxColumn10"
        Me.DataGridViewTextBoxColumn10.Width = 80
        '
        'DataGridViewTextBoxColumn11
        '
        Me.DataGridViewTextBoxColumn11.DataPropertyName = "Weight_Sum"
        Me.DataGridViewTextBoxColumn11.HeaderText = "นน. สินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn11.Name = "DataGridViewTextBoxColumn11"
        Me.DataGridViewTextBoxColumn11.Visible = False
        Me.DataGridViewTextBoxColumn11.Width = 110
        '
        'DataGridViewTextBoxColumn12
        '
        Me.DataGridViewTextBoxColumn12.DataPropertyName = "Volume_Sum"
        Me.DataGridViewTextBoxColumn12.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn12.Name = "DataGridViewTextBoxColumn12"
        Me.DataGridViewTextBoxColumn12.Width = 120
        '
        'DataGridViewTextBoxColumn13
        '
        Me.DataGridViewTextBoxColumn13.DataPropertyName = "Qty_Sum"
        Me.DataGridViewTextBoxColumn13.HeaderText = "จน.ชิ้น"
        Me.DataGridViewTextBoxColumn13.Name = "DataGridViewTextBoxColumn13"
        Me.DataGridViewTextBoxColumn13.Width = 70
        '
        'DataGridViewTextBoxColumn14
        '
        Me.DataGridViewTextBoxColumn14.DataPropertyName = "Bill_Sum"
        Me.DataGridViewTextBoxColumn14.HeaderText = "จำนวนบิล"
        Me.DataGridViewTextBoxColumn14.Name = "DataGridViewTextBoxColumn14"
        Me.DataGridViewTextBoxColumn14.Visible = False
        Me.DataGridViewTextBoxColumn14.Width = 90
        '
        'DataGridViewTextBoxColumn15
        '
        Me.DataGridViewTextBoxColumn15.DataPropertyName = "Container_No1"
        Me.DataGridViewTextBoxColumn15.HeaderText = "หมายเลขตู้ 1"
        Me.DataGridViewTextBoxColumn15.Name = "DataGridViewTextBoxColumn15"
        Me.DataGridViewTextBoxColumn15.Width = 90
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "Container_No2"
        Me.DataGridViewTextBoxColumn16.HeaderText = "หมายเลขตู้ 2"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.Visible = False
        Me.DataGridViewTextBoxColumn16.Width = 110
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "TransportJobType"
        Me.DataGridViewTextBoxColumn17.HeaderText = "ประเภทงาน"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.Width = 90
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn18.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.Width = 120
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "Route"
        Me.DataGridViewTextBoxColumn19.HeaderText = "เส้นทาง"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.Width = 90
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn20.HeaderText = "ศูนย์กระจาย"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.Width = 90
        '
        'DataGridViewTextBoxColumn21
        '
        Me.DataGridViewTextBoxColumn21.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn21.HeaderText = "อ้างอิง 1"
        Me.DataGridViewTextBoxColumn21.Name = "DataGridViewTextBoxColumn21"
        Me.DataGridViewTextBoxColumn21.Visible = False
        Me.DataGridViewTextBoxColumn21.Width = 120
        '
        'DataGridViewTextBoxColumn22
        '
        Me.DataGridViewTextBoxColumn22.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn22.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn22.Name = "DataGridViewTextBoxColumn22"
        Me.DataGridViewTextBoxColumn22.Visible = False
        Me.DataGridViewTextBoxColumn22.Width = 120
        '
        'DataGridViewTextBoxColumn23
        '
        Me.DataGridViewTextBoxColumn23.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn23.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn23.Name = "DataGridViewTextBoxColumn23"
        Me.DataGridViewTextBoxColumn23.Visible = False
        Me.DataGridViewTextBoxColumn23.Width = 150
        '
        'DataGridViewTextBoxColumn24
        '
        Me.DataGridViewTextBoxColumn24.DataPropertyName = "TransportManifest_Index"
        Me.DataGridViewTextBoxColumn24.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn24.Name = "DataGridViewTextBoxColumn24"
        Me.DataGridViewTextBoxColumn24.ReadOnly = True
        Me.DataGridViewTextBoxColumn24.Visible = False
        Me.DataGridViewTextBoxColumn24.Width = 110
        '
        'DataGridViewTextBoxColumn25
        '
        Me.DataGridViewTextBoxColumn25.DataPropertyName = "Time_SourceOutGate"
        Me.DataGridViewTextBoxColumn25.HeaderText = "เวลาปล่อยรถ"
        Me.DataGridViewTextBoxColumn25.Name = "DataGridViewTextBoxColumn25"
        Me.DataGridViewTextBoxColumn25.ReadOnly = True
        Me.DataGridViewTextBoxColumn25.Visible = False
        Me.DataGridViewTextBoxColumn25.Width = 110
        '
        'DataGridViewCheckBoxColumn2
        '
        Me.DataGridViewCheckBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells
        Me.DataGridViewCheckBoxColumn2.DataPropertyName = "chkSelect"
        Me.DataGridViewCheckBoxColumn2.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn2.Frozen = True
        Me.DataGridViewCheckBoxColumn2.HeaderText = ""
        Me.DataGridViewCheckBoxColumn2.Name = "DataGridViewCheckBoxColumn2"
        Me.DataGridViewCheckBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn2.TrueValue = "1"
        '
        'DataGridViewTextBoxColumn26
        '
        Me.DataGridViewTextBoxColumn26.DataPropertyName = "Time_DestinationInGate"
        Me.DataGridViewTextBoxColumn26.Frozen = True
        Me.DataGridViewTextBoxColumn26.HeaderText = "เวลาถึงปลายทาง"
        Me.DataGridViewTextBoxColumn26.Name = "DataGridViewTextBoxColumn26"
        Me.DataGridViewTextBoxColumn26.ReadOnly = True
        Me.DataGridViewTextBoxColumn26.Visible = False
        Me.DataGridViewTextBoxColumn26.Width = 110
        '
        'DataGridViewTextBoxColumn27
        '
        Me.DataGridViewTextBoxColumn27.DataPropertyName = "Time_ReturnTruckInGate"
        Me.DataGridViewTextBoxColumn27.HeaderText = "เวลากลับต้นทาง"
        Me.DataGridViewTextBoxColumn27.Name = "DataGridViewTextBoxColumn27"
        Me.DataGridViewTextBoxColumn27.ReadOnly = True
        Me.DataGridViewTextBoxColumn27.Visible = False
        Me.DataGridViewTextBoxColumn27.Width = 110
        '
        'DataGridViewTextBoxColumn28
        '
        Me.DataGridViewTextBoxColumn28.DataPropertyName = "TransportManifest_No"
        Me.DataGridViewTextBoxColumn28.HeaderText = "เลขที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn28.Name = "DataGridViewTextBoxColumn28"
        Me.DataGridViewTextBoxColumn28.ReadOnly = True
        '
        'DataGridViewTextBoxColumn29
        '
        Me.DataGridViewTextBoxColumn29.DataPropertyName = "TransportManifest_Date"
        Me.DataGridViewTextBoxColumn29.HeaderText = "วันที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn29.Name = "DataGridViewTextBoxColumn29"
        Me.DataGridViewTextBoxColumn29.ReadOnly = True
        Me.DataGridViewTextBoxColumn29.Width = 110
        '
        'DataGridViewTextBoxColumn30
        '
        Me.DataGridViewTextBoxColumn30.DataPropertyName = "Vehicle_Id"
        Me.DataGridViewTextBoxColumn30.HeaderText = "หมายเลขรถ"
        Me.DataGridViewTextBoxColumn30.Name = "DataGridViewTextBoxColumn30"
        Me.DataGridViewTextBoxColumn30.ReadOnly = True
        Me.DataGridViewTextBoxColumn30.Visible = False
        Me.DataGridViewTextBoxColumn30.Width = 110
        '
        'DataGridViewTextBoxColumn31
        '
        Me.DataGridViewTextBoxColumn31.DataPropertyName = "Vehicle_No"
        Me.DataGridViewTextBoxColumn31.HeaderText = "ทะเบียนรถ"
        Me.DataGridViewTextBoxColumn31.Name = "DataGridViewTextBoxColumn31"
        Me.DataGridViewTextBoxColumn31.ReadOnly = True
        Me.DataGridViewTextBoxColumn31.Visible = False
        Me.DataGridViewTextBoxColumn31.Width = 80
        '
        'DataGridViewTextBoxColumn32
        '
        Me.DataGridViewTextBoxColumn32.DataPropertyName = "VehicleType"
        Me.DataGridViewTextBoxColumn32.HeaderText = "ประเภทรถ"
        Me.DataGridViewTextBoxColumn32.Name = "DataGridViewTextBoxColumn32"
        Me.DataGridViewTextBoxColumn32.ReadOnly = True
        Me.DataGridViewTextBoxColumn32.Visible = False
        Me.DataGridViewTextBoxColumn32.Width = 80
        '
        'DataGridViewTextBoxColumn33
        '
        Me.DataGridViewTextBoxColumn33.DataPropertyName = "Driver_Name"
        Me.DataGridViewTextBoxColumn33.HeaderText = "พนักงานขับรถ"
        Me.DataGridViewTextBoxColumn33.Name = "DataGridViewTextBoxColumn33"
        Me.DataGridViewTextBoxColumn33.ReadOnly = True
        Me.DataGridViewTextBoxColumn33.Width = 80
        '
        'DataGridViewTextBoxColumn34
        '
        Me.DataGridViewTextBoxColumn34.DataPropertyName = "Company_Name"
        Me.DataGridViewTextBoxColumn34.HeaderText = "ผู้รับ"
        Me.DataGridViewTextBoxColumn34.Name = "DataGridViewTextBoxColumn34"
        Me.DataGridViewTextBoxColumn34.ReadOnly = True
        Me.DataGridViewTextBoxColumn34.Visible = False
        '
        'DataGridViewTextBoxColumn35
        '
        Me.DataGridViewTextBoxColumn35.DataPropertyName = "Shipping_Location_Name"
        Me.DataGridViewTextBoxColumn35.HeaderText = "ปลายทาง"
        Me.DataGridViewTextBoxColumn35.Name = "DataGridViewTextBoxColumn35"
        Me.DataGridViewTextBoxColumn35.ReadOnly = True
        Me.DataGridViewTextBoxColumn35.Visible = False
        '
        'DataGridViewTextBoxColumn36
        '
        Me.DataGridViewTextBoxColumn36.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn36.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn36.Name = "DataGridViewTextBoxColumn36"
        Me.DataGridViewTextBoxColumn36.ReadOnly = True
        Me.DataGridViewTextBoxColumn36.Visible = False
        Me.DataGridViewTextBoxColumn36.Width = 80
        '
        'DataGridViewTextBoxColumn37
        '
        Me.DataGridViewTextBoxColumn37.DataPropertyName = "Weight_Sum"
        Me.DataGridViewTextBoxColumn37.HeaderText = "นน. สินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn37.Name = "DataGridViewTextBoxColumn37"
        Me.DataGridViewTextBoxColumn37.ReadOnly = True
        Me.DataGridViewTextBoxColumn37.Visible = False
        Me.DataGridViewTextBoxColumn37.Width = 110
        '
        'DataGridViewTextBoxColumn38
        '
        Me.DataGridViewTextBoxColumn38.DataPropertyName = "Volume_Sum"
        Me.DataGridViewTextBoxColumn38.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn38.Name = "DataGridViewTextBoxColumn38"
        Me.DataGridViewTextBoxColumn38.ReadOnly = True
        Me.DataGridViewTextBoxColumn38.Visible = False
        Me.DataGridViewTextBoxColumn38.Width = 120
        '
        'DataGridViewTextBoxColumn39
        '
        Me.DataGridViewTextBoxColumn39.DataPropertyName = "Qty_Sum"
        Me.DataGridViewTextBoxColumn39.HeaderText = "จน.ชิ้น"
        Me.DataGridViewTextBoxColumn39.Name = "DataGridViewTextBoxColumn39"
        Me.DataGridViewTextBoxColumn39.ReadOnly = True
        Me.DataGridViewTextBoxColumn39.Visible = False
        Me.DataGridViewTextBoxColumn39.Width = 70
        '
        'DataGridViewTextBoxColumn40
        '
        Me.DataGridViewTextBoxColumn40.DataPropertyName = "Bill_Sum"
        Me.DataGridViewTextBoxColumn40.HeaderText = "จำนวนบิล"
        Me.DataGridViewTextBoxColumn40.Name = "DataGridViewTextBoxColumn40"
        Me.DataGridViewTextBoxColumn40.ReadOnly = True
        Me.DataGridViewTextBoxColumn40.Visible = False
        Me.DataGridViewTextBoxColumn40.Width = 90
        '
        'DataGridViewTextBoxColumn41
        '
        Me.DataGridViewTextBoxColumn41.DataPropertyName = "Container_No1"
        Me.DataGridViewTextBoxColumn41.HeaderText = "หมายเลขตู้ 1"
        Me.DataGridViewTextBoxColumn41.Name = "DataGridViewTextBoxColumn41"
        Me.DataGridViewTextBoxColumn41.ReadOnly = True
        Me.DataGridViewTextBoxColumn41.Width = 70
        '
        'DataGridViewTextBoxColumn42
        '
        Me.DataGridViewTextBoxColumn42.DataPropertyName = "Container_No2"
        Me.DataGridViewTextBoxColumn42.HeaderText = "หมายเลขตู้ 2"
        Me.DataGridViewTextBoxColumn42.Name = "DataGridViewTextBoxColumn42"
        Me.DataGridViewTextBoxColumn42.ReadOnly = True
        Me.DataGridViewTextBoxColumn42.Width = 90
        '
        'DataGridViewTextBoxColumn43
        '
        Me.DataGridViewTextBoxColumn43.DataPropertyName = "TransportJobType"
        Me.DataGridViewTextBoxColumn43.HeaderText = "ประเภทงาน"
        Me.DataGridViewTextBoxColumn43.Name = "DataGridViewTextBoxColumn43"
        Me.DataGridViewTextBoxColumn43.ReadOnly = True
        Me.DataGridViewTextBoxColumn43.Width = 90
        '
        'DataGridViewTextBoxColumn44
        '
        Me.DataGridViewTextBoxColumn44.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn44.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.DataGridViewTextBoxColumn44.Name = "DataGridViewTextBoxColumn44"
        Me.DataGridViewTextBoxColumn44.ReadOnly = True
        Me.DataGridViewTextBoxColumn44.Width = 120
        '
        'DataGridViewTextBoxColumn45
        '
        Me.DataGridViewTextBoxColumn45.DataPropertyName = "Route"
        Me.DataGridViewTextBoxColumn45.HeaderText = "เส้นทาง"
        Me.DataGridViewTextBoxColumn45.Name = "DataGridViewTextBoxColumn45"
        Me.DataGridViewTextBoxColumn45.ReadOnly = True
        Me.DataGridViewTextBoxColumn45.Width = 120
        '
        'DataGridViewTextBoxColumn46
        '
        Me.DataGridViewTextBoxColumn46.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn46.HeaderText = "ศูนย์กระจาย"
        Me.DataGridViewTextBoxColumn46.Name = "DataGridViewTextBoxColumn46"
        Me.DataGridViewTextBoxColumn46.ReadOnly = True
        '
        'DataGridViewTextBoxColumn47
        '
        Me.DataGridViewTextBoxColumn47.DataPropertyName = "TransportJobType"
        Me.DataGridViewTextBoxColumn47.HeaderText = "อ้างอิง 1"
        Me.DataGridViewTextBoxColumn47.Name = "DataGridViewTextBoxColumn47"
        Me.DataGridViewTextBoxColumn47.ReadOnly = True
        Me.DataGridViewTextBoxColumn47.Width = 90
        '
        'DataGridViewTextBoxColumn48
        '
        Me.DataGridViewTextBoxColumn48.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn48.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn48.Name = "DataGridViewTextBoxColumn48"
        Me.DataGridViewTextBoxColumn48.ReadOnly = True
        Me.DataGridViewTextBoxColumn48.Visible = False
        Me.DataGridViewTextBoxColumn48.Width = 120
        '
        'DataGridViewTextBoxColumn49
        '
        Me.DataGridViewTextBoxColumn49.DataPropertyName = "Add_by"
        Me.DataGridViewTextBoxColumn49.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn49.Name = "DataGridViewTextBoxColumn49"
        Me.DataGridViewTextBoxColumn49.ReadOnly = True
        Me.DataGridViewTextBoxColumn49.Visible = False
        Me.DataGridViewTextBoxColumn49.Width = 150
        '
        'DataGridViewTextBoxColumn50
        '
        Me.DataGridViewTextBoxColumn50.DataPropertyName = "TransportManifest_Index"
        Me.DataGridViewTextBoxColumn50.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn50.Name = "DataGridViewTextBoxColumn50"
        Me.DataGridViewTextBoxColumn50.ReadOnly = True
        Me.DataGridViewTextBoxColumn50.Visible = False
        Me.DataGridViewTextBoxColumn50.Width = 110
        '
        'DataGridViewTextBoxColumn51
        '
        Me.DataGridViewTextBoxColumn51.DataPropertyName = "Time_SourceOutGate"
        Me.DataGridViewTextBoxColumn51.HeaderText = "เวลาปล่อยรถ"
        Me.DataGridViewTextBoxColumn51.Name = "DataGridViewTextBoxColumn51"
        Me.DataGridViewTextBoxColumn51.ReadOnly = True
        Me.DataGridViewTextBoxColumn51.Visible = False
        Me.DataGridViewTextBoxColumn51.Width = 110
        '
        'DataGridViewTextBoxColumn52
        '
        Me.DataGridViewTextBoxColumn52.DataPropertyName = "Time_DestinationInGate"
        Me.DataGridViewTextBoxColumn52.HeaderText = "เวลาถึงปลายทาง"
        Me.DataGridViewTextBoxColumn52.Name = "DataGridViewTextBoxColumn52"
        Me.DataGridViewTextBoxColumn52.ReadOnly = True
        Me.DataGridViewTextBoxColumn52.Visible = False
        Me.DataGridViewTextBoxColumn52.Width = 110
        '
        'DataGridViewTextBoxColumn53
        '
        Me.DataGridViewTextBoxColumn53.DataPropertyName = "Time_ReturnTruckInGate"
        Me.DataGridViewTextBoxColumn53.HeaderText = "เวลากลับต้นทาง"
        Me.DataGridViewTextBoxColumn53.Name = "DataGridViewTextBoxColumn53"
        Me.DataGridViewTextBoxColumn53.ReadOnly = True
        Me.DataGridViewTextBoxColumn53.Visible = False
        Me.DataGridViewTextBoxColumn53.Width = 110
        '
        'DataGridViewCheckBoxColumn3
        '
        Me.DataGridViewCheckBoxColumn3.DataPropertyName = "chkSelect"
        Me.DataGridViewCheckBoxColumn3.FalseValue = "0"
        Me.DataGridViewCheckBoxColumn3.Frozen = True
        Me.DataGridViewCheckBoxColumn3.HeaderText = ""
        Me.DataGridViewCheckBoxColumn3.Name = "DataGridViewCheckBoxColumn3"
        Me.DataGridViewCheckBoxColumn3.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.DataGridViewCheckBoxColumn3.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic
        Me.DataGridViewCheckBoxColumn3.TrueValue = "1"
        Me.DataGridViewCheckBoxColumn3.Visible = False
        Me.DataGridViewCheckBoxColumn3.Width = 19
        '
        'DataGridViewTextBoxColumn54
        '
        Me.DataGridViewTextBoxColumn54.DataPropertyName = "TransportManifest_No"
        Me.DataGridViewTextBoxColumn54.Frozen = True
        Me.DataGridViewTextBoxColumn54.HeaderText = "เลขที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn54.Name = "DataGridViewTextBoxColumn54"
        Me.DataGridViewTextBoxColumn54.ReadOnly = True
        Me.DataGridViewTextBoxColumn54.Visible = False
        Me.DataGridViewTextBoxColumn54.Width = 110
        '
        'DataGridViewTextBoxColumn55
        '
        Me.DataGridViewTextBoxColumn55.DataPropertyName = "TransportManifest_Date"
        Me.DataGridViewTextBoxColumn55.HeaderText = "วันที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn55.Name = "DataGridViewTextBoxColumn55"
        Me.DataGridViewTextBoxColumn55.ReadOnly = True
        '
        'DataGridViewTextBoxColumn56
        '
        Me.DataGridViewTextBoxColumn56.DataPropertyName = "Vehicle_Id"
        Me.DataGridViewTextBoxColumn56.HeaderText = "หมายเลขรถ"
        Me.DataGridViewTextBoxColumn56.Name = "DataGridViewTextBoxColumn56"
        Me.DataGridViewTextBoxColumn56.ReadOnly = True
        Me.DataGridViewTextBoxColumn56.Visible = False
        '
        'DataGridViewTextBoxColumn57
        '
        Me.DataGridViewTextBoxColumn57.DataPropertyName = "Vehicle_No"
        Me.DataGridViewTextBoxColumn57.HeaderText = "ทะเบียนรถ"
        Me.DataGridViewTextBoxColumn57.Name = "DataGridViewTextBoxColumn57"
        Me.DataGridViewTextBoxColumn57.ReadOnly = True
        Me.DataGridViewTextBoxColumn57.Visible = False
        Me.DataGridViewTextBoxColumn57.Width = 80
        '
        'DataGridViewTextBoxColumn58
        '
        Me.DataGridViewTextBoxColumn58.DataPropertyName = "VehicleType"
        Me.DataGridViewTextBoxColumn58.HeaderText = "ประเภทรถ"
        Me.DataGridViewTextBoxColumn58.Name = "DataGridViewTextBoxColumn58"
        Me.DataGridViewTextBoxColumn58.ReadOnly = True
        Me.DataGridViewTextBoxColumn58.Visible = False
        Me.DataGridViewTextBoxColumn58.Width = 80
        '
        'DataGridViewTextBoxColumn59
        '
        Me.DataGridViewTextBoxColumn59.DataPropertyName = "Driver_Name"
        Me.DataGridViewTextBoxColumn59.HeaderText = "พนักงานขับรถ"
        Me.DataGridViewTextBoxColumn59.Name = "DataGridViewTextBoxColumn59"
        Me.DataGridViewTextBoxColumn59.ReadOnly = True
        Me.DataGridViewTextBoxColumn59.Visible = False
        Me.DataGridViewTextBoxColumn59.Width = 80
        '
        'DataGridViewTextBoxColumn60
        '
        Me.DataGridViewTextBoxColumn60.DataPropertyName = "Company_Name"
        Me.DataGridViewTextBoxColumn60.HeaderText = "ผู้รับ"
        Me.DataGridViewTextBoxColumn60.Name = "DataGridViewTextBoxColumn60"
        Me.DataGridViewTextBoxColumn60.ReadOnly = True
        Me.DataGridViewTextBoxColumn60.Visible = False
        Me.DataGridViewTextBoxColumn60.Width = 80
        '
        'DataGridViewTextBoxColumn61
        '
        Me.DataGridViewTextBoxColumn61.DataPropertyName = "Shipping_Location_Name"
        Me.DataGridViewTextBoxColumn61.HeaderText = "ปลายทาง"
        Me.DataGridViewTextBoxColumn61.Name = "DataGridViewTextBoxColumn61"
        Me.DataGridViewTextBoxColumn61.ReadOnly = True
        Me.DataGridViewTextBoxColumn61.Visible = False
        Me.DataGridViewTextBoxColumn61.Width = 80
        '
        'DataGridViewTextBoxColumn62
        '
        Me.DataGridViewTextBoxColumn62.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn62.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn62.Name = "DataGridViewTextBoxColumn62"
        Me.DataGridViewTextBoxColumn62.ReadOnly = True
        Me.DataGridViewTextBoxColumn62.Visible = False
        '
        'DataGridViewTextBoxColumn63
        '
        Me.DataGridViewTextBoxColumn63.DataPropertyName = "Weight_Sum"
        Me.DataGridViewTextBoxColumn63.HeaderText = "นน. สินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn63.Name = "DataGridViewTextBoxColumn63"
        Me.DataGridViewTextBoxColumn63.ReadOnly = True
        Me.DataGridViewTextBoxColumn63.Width = 110
        '
        'DataGridViewTextBoxColumn64
        '
        Me.DataGridViewTextBoxColumn64.DataPropertyName = "Volume_Sum"
        Me.DataGridViewTextBoxColumn64.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn64.Name = "DataGridViewTextBoxColumn64"
        Me.DataGridViewTextBoxColumn64.ReadOnly = True
        Me.DataGridViewTextBoxColumn64.Visible = False
        Me.DataGridViewTextBoxColumn64.Width = 120
        '
        'DataGridViewTextBoxColumn65
        '
        Me.DataGridViewTextBoxColumn65.DataPropertyName = "Qty_Sum"
        Me.DataGridViewTextBoxColumn65.HeaderText = "จน.ชิ้น"
        Me.DataGridViewTextBoxColumn65.Name = "DataGridViewTextBoxColumn65"
        Me.DataGridViewTextBoxColumn65.ReadOnly = True
        Me.DataGridViewTextBoxColumn65.Visible = False
        Me.DataGridViewTextBoxColumn65.Width = 70
        '
        'DataGridViewTextBoxColumn66
        '
        Me.DataGridViewTextBoxColumn66.DataPropertyName = "Bill_Sum"
        Me.DataGridViewTextBoxColumn66.HeaderText = "จำนวนบิล"
        Me.DataGridViewTextBoxColumn66.Name = "DataGridViewTextBoxColumn66"
        Me.DataGridViewTextBoxColumn66.ReadOnly = True
        Me.DataGridViewTextBoxColumn66.Visible = False
        Me.DataGridViewTextBoxColumn66.Width = 90
        '
        'DataGridViewTextBoxColumn67
        '
        Me.DataGridViewTextBoxColumn67.DataPropertyName = "Container_No1"
        Me.DataGridViewTextBoxColumn67.HeaderText = "หมายเลขตู้ 1"
        Me.DataGridViewTextBoxColumn67.Name = "DataGridViewTextBoxColumn67"
        Me.DataGridViewTextBoxColumn67.ReadOnly = True
        Me.DataGridViewTextBoxColumn67.Visible = False
        Me.DataGridViewTextBoxColumn67.Width = 90
        '
        'DataGridViewTextBoxColumn68
        '
        Me.DataGridViewTextBoxColumn68.DataPropertyName = "Container_No2"
        Me.DataGridViewTextBoxColumn68.HeaderText = "หมายเลขตู้ 2"
        Me.DataGridViewTextBoxColumn68.Name = "DataGridViewTextBoxColumn68"
        Me.DataGridViewTextBoxColumn68.ReadOnly = True
        '
        'DataGridViewTextBoxColumn69
        '
        Me.DataGridViewTextBoxColumn69.DataPropertyName = "TransportJobType"
        Me.DataGridViewTextBoxColumn69.HeaderText = "ประเภทงาน"
        Me.DataGridViewTextBoxColumn69.Name = "DataGridViewTextBoxColumn69"
        Me.DataGridViewTextBoxColumn69.ReadOnly = True
        Me.DataGridViewTextBoxColumn69.Width = 90
        '
        'DataGridViewTextBoxColumn70
        '
        Me.DataGridViewTextBoxColumn70.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn70.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.DataGridViewTextBoxColumn70.Name = "DataGridViewTextBoxColumn70"
        Me.DataGridViewTextBoxColumn70.ReadOnly = True
        Me.DataGridViewTextBoxColumn70.Width = 120
        '
        'DataGridViewTextBoxColumn71
        '
        Me.DataGridViewTextBoxColumn71.DataPropertyName = "Route"
        Me.DataGridViewTextBoxColumn71.HeaderText = "เส้นทาง"
        Me.DataGridViewTextBoxColumn71.Name = "DataGridViewTextBoxColumn71"
        Me.DataGridViewTextBoxColumn71.ReadOnly = True
        Me.DataGridViewTextBoxColumn71.Width = 120
        '
        'DataGridViewTextBoxColumn72
        '
        Me.DataGridViewTextBoxColumn72.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn72.HeaderText = "ศูนย์กระจาย"
        Me.DataGridViewTextBoxColumn72.Name = "DataGridViewTextBoxColumn72"
        Me.DataGridViewTextBoxColumn72.ReadOnly = True
        Me.DataGridViewTextBoxColumn72.Width = 120
        '
        'DataGridViewTextBoxColumn73
        '
        Me.DataGridViewTextBoxColumn73.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn73.HeaderText = "อ้างอิง 1"
        Me.DataGridViewTextBoxColumn73.Name = "DataGridViewTextBoxColumn73"
        Me.DataGridViewTextBoxColumn73.ReadOnly = True
        Me.DataGridViewTextBoxColumn73.Width = 120
        '
        'DataGridViewTextBoxColumn74
        '
        Me.DataGridViewTextBoxColumn74.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn74.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn74.Name = "DataGridViewTextBoxColumn74"
        Me.DataGridViewTextBoxColumn74.ReadOnly = True
        '
        'DataGridViewTextBoxColumn75
        '
        Me.DataGridViewTextBoxColumn75.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn75.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn75.Name = "DataGridViewTextBoxColumn75"
        Me.DataGridViewTextBoxColumn75.ReadOnly = True
        Me.DataGridViewTextBoxColumn75.Width = 150
        '
        'DataGridViewTextBoxColumn76
        '
        Me.DataGridViewTextBoxColumn76.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn76.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn76.Name = "DataGridViewTextBoxColumn76"
        Me.DataGridViewTextBoxColumn76.Visible = False
        Me.DataGridViewTextBoxColumn76.Width = 150
        '
        'DataGridViewTextBoxColumn77
        '
        Me.DataGridViewTextBoxColumn77.DataPropertyName = "TransportManifest_Index"
        Me.DataGridViewTextBoxColumn77.HeaderText = "รหัสระบบ "
        Me.DataGridViewTextBoxColumn77.Name = "DataGridViewTextBoxColumn77"
        Me.DataGridViewTextBoxColumn77.Visible = False
        Me.DataGridViewTextBoxColumn77.Width = 110
        '
        'DataGridViewTextBoxColumn78
        '
        Me.DataGridViewTextBoxColumn78.DataPropertyName = "Time_SourceOutGate"
        Me.DataGridViewTextBoxColumn78.HeaderText = "เวลาปล่อยรถ"
        Me.DataGridViewTextBoxColumn78.Name = "DataGridViewTextBoxColumn78"
        Me.DataGridViewTextBoxColumn78.Visible = False
        '
        'DataGridViewTextBoxColumn79
        '
        Me.DataGridViewTextBoxColumn79.DataPropertyName = "Time_DestinationInGate"
        Me.DataGridViewTextBoxColumn79.HeaderText = "เวลาถึงปลายทาง"
        Me.DataGridViewTextBoxColumn79.Name = "DataGridViewTextBoxColumn79"
        Me.DataGridViewTextBoxColumn79.Width = 110
        '
        'DataGridViewTextBoxColumn80
        '
        Me.DataGridViewTextBoxColumn80.DataPropertyName = "Time_ReturnTruckInGate"
        Me.DataGridViewTextBoxColumn80.HeaderText = "เวลากลับต้นทาง"
        Me.DataGridViewTextBoxColumn80.Name = "DataGridViewTextBoxColumn80"
        Me.DataGridViewTextBoxColumn80.Visible = False
        Me.DataGridViewTextBoxColumn80.Width = 110
        '
        'DataGridViewTextBoxColumn81
        '
        Me.DataGridViewTextBoxColumn81.DataPropertyName = "TransportManifest_No"
        Me.DataGridViewTextBoxColumn81.HeaderText = "เลขที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn81.Name = "DataGridViewTextBoxColumn81"
        Me.DataGridViewTextBoxColumn81.ReadOnly = True
        Me.DataGridViewTextBoxColumn81.Visible = False
        Me.DataGridViewTextBoxColumn81.Width = 110
        '
        'DataGridViewTextBoxColumn82
        '
        Me.DataGridViewTextBoxColumn82.DataPropertyName = "TransportManifest_Date"
        Me.DataGridViewTextBoxColumn82.Frozen = True
        Me.DataGridViewTextBoxColumn82.HeaderText = "วันที่ใบคุมรถ"
        Me.DataGridViewTextBoxColumn82.Name = "DataGridViewTextBoxColumn82"
        Me.DataGridViewTextBoxColumn82.Visible = False
        Me.DataGridViewTextBoxColumn82.Width = 110
        '
        'DataGridViewTextBoxColumn83
        '
        Me.DataGridViewTextBoxColumn83.DataPropertyName = "Vehicle_Id"
        Me.DataGridViewTextBoxColumn83.Frozen = True
        Me.DataGridViewTextBoxColumn83.HeaderText = "หมายเลขรถ"
        Me.DataGridViewTextBoxColumn83.Name = "DataGridViewTextBoxColumn83"
        Me.DataGridViewTextBoxColumn83.Visible = False
        Me.DataGridViewTextBoxColumn83.Width = 110
        '
        'DataGridViewTextBoxColumn84
        '
        Me.DataGridViewTextBoxColumn84.DataPropertyName = "Vehicle_No"
        Me.DataGridViewTextBoxColumn84.HeaderText = "ทะเบียนรถ"
        Me.DataGridViewTextBoxColumn84.Name = "DataGridViewTextBoxColumn84"
        Me.DataGridViewTextBoxColumn84.Visible = False
        Me.DataGridViewTextBoxColumn84.Width = 80
        '
        'DataGridViewTextBoxColumn85
        '
        Me.DataGridViewTextBoxColumn85.DataPropertyName = "VehicleType"
        Me.DataGridViewTextBoxColumn85.HeaderText = "ประเภทรถ"
        Me.DataGridViewTextBoxColumn85.Name = "DataGridViewTextBoxColumn85"
        Me.DataGridViewTextBoxColumn85.Visible = False
        Me.DataGridViewTextBoxColumn85.Width = 80
        '
        'DataGridViewTextBoxColumn86
        '
        Me.DataGridViewTextBoxColumn86.DataPropertyName = "Driver_Name"
        Me.DataGridViewTextBoxColumn86.HeaderText = "พนักงานขับรถ"
        Me.DataGridViewTextBoxColumn86.Name = "DataGridViewTextBoxColumn86"
        Me.DataGridViewTextBoxColumn86.Visible = False
        Me.DataGridViewTextBoxColumn86.Width = 110
        '
        'DataGridViewTextBoxColumn87
        '
        Me.DataGridViewTextBoxColumn87.DataPropertyName = "Company_Name"
        Me.DataGridViewTextBoxColumn87.HeaderText = "ผู้รับ"
        Me.DataGridViewTextBoxColumn87.Name = "DataGridViewTextBoxColumn87"
        Me.DataGridViewTextBoxColumn87.Visible = False
        Me.DataGridViewTextBoxColumn87.Width = 80
        '
        'DataGridViewTextBoxColumn88
        '
        Me.DataGridViewTextBoxColumn88.DataPropertyName = "Shipping_Location_Name"
        Me.DataGridViewTextBoxColumn88.HeaderText = "ปลายทาง"
        Me.DataGridViewTextBoxColumn88.Name = "DataGridViewTextBoxColumn88"
        Me.DataGridViewTextBoxColumn88.Visible = False
        Me.DataGridViewTextBoxColumn88.Width = 80
        '
        'DataGridViewTextBoxColumn89
        '
        Me.DataGridViewTextBoxColumn89.DataPropertyName = "Status"
        Me.DataGridViewTextBoxColumn89.HeaderText = "สถานะ"
        Me.DataGridViewTextBoxColumn89.Name = "DataGridViewTextBoxColumn89"
        Me.DataGridViewTextBoxColumn89.Visible = False
        Me.DataGridViewTextBoxColumn89.Width = 80
        '
        'DataGridViewTextBoxColumn90
        '
        Me.DataGridViewTextBoxColumn90.DataPropertyName = "Weight_Sum"
        Me.DataGridViewTextBoxColumn90.HeaderText = "นน. สินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn90.Name = "DataGridViewTextBoxColumn90"
        Me.DataGridViewTextBoxColumn90.Visible = False
        Me.DataGridViewTextBoxColumn90.Width = 110
        '
        'DataGridViewTextBoxColumn91
        '
        Me.DataGridViewTextBoxColumn91.DataPropertyName = "Volume_Sum"
        Me.DataGridViewTextBoxColumn91.HeaderText = "ปริมาตรสินค้า/พิกัด"
        Me.DataGridViewTextBoxColumn91.Name = "DataGridViewTextBoxColumn91"
        Me.DataGridViewTextBoxColumn91.Visible = False
        Me.DataGridViewTextBoxColumn91.Width = 120
        '
        'DataGridViewTextBoxColumn92
        '
        Me.DataGridViewTextBoxColumn92.DataPropertyName = "Qty_Sum"
        Me.DataGridViewTextBoxColumn92.HeaderText = "จน.ชิ้น"
        Me.DataGridViewTextBoxColumn92.Name = "DataGridViewTextBoxColumn92"
        Me.DataGridViewTextBoxColumn92.Visible = False
        Me.DataGridViewTextBoxColumn92.Width = 70
        '
        'DataGridViewTextBoxColumn93
        '
        Me.DataGridViewTextBoxColumn93.DataPropertyName = "Bill_Sum"
        Me.DataGridViewTextBoxColumn93.HeaderText = "จำนวนบิล"
        Me.DataGridViewTextBoxColumn93.Name = "DataGridViewTextBoxColumn93"
        Me.DataGridViewTextBoxColumn93.Width = 90
        '
        'DataGridViewTextBoxColumn94
        '
        Me.DataGridViewTextBoxColumn94.DataPropertyName = "Container_No1"
        Me.DataGridViewTextBoxColumn94.HeaderText = "หมายเลขตู้ 1"
        Me.DataGridViewTextBoxColumn94.Name = "DataGridViewTextBoxColumn94"
        Me.DataGridViewTextBoxColumn94.Width = 120
        '
        'DataGridViewTextBoxColumn95
        '
        Me.DataGridViewTextBoxColumn95.DataPropertyName = "Container_No2"
        Me.DataGridViewTextBoxColumn95.HeaderText = "หมายเลขตู้ 2"
        Me.DataGridViewTextBoxColumn95.Name = "DataGridViewTextBoxColumn95"
        Me.DataGridViewTextBoxColumn95.Width = 70
        '
        'DataGridViewTextBoxColumn96
        '
        Me.DataGridViewTextBoxColumn96.DataPropertyName = "TransportJobType"
        Me.DataGridViewTextBoxColumn96.HeaderText = "ประเภทงาน"
        Me.DataGridViewTextBoxColumn96.Name = "DataGridViewTextBoxColumn96"
        Me.DataGridViewTextBoxColumn96.Width = 90
        '
        'DataGridViewTextBoxColumn97
        '
        Me.DataGridViewTextBoxColumn97.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn97.HeaderText = "ลูกค้า/เจ้าของงาน"
        Me.DataGridViewTextBoxColumn97.Name = "DataGridViewTextBoxColumn97"
        Me.DataGridViewTextBoxColumn97.Width = 120
        '
        'DataGridViewTextBoxColumn98
        '
        Me.DataGridViewTextBoxColumn98.DataPropertyName = "Route"
        Me.DataGridViewTextBoxColumn98.HeaderText = "เส้นทาง"
        Me.DataGridViewTextBoxColumn98.Name = "DataGridViewTextBoxColumn98"
        Me.DataGridViewTextBoxColumn98.Width = 90
        '
        'DataGridViewTextBoxColumn99
        '
        Me.DataGridViewTextBoxColumn99.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn99.HeaderText = "ศูนย์กระจาย"
        Me.DataGridViewTextBoxColumn99.Name = "DataGridViewTextBoxColumn99"
        Me.DataGridViewTextBoxColumn99.Width = 90
        '
        'DataGridViewTextBoxColumn100
        '
        Me.DataGridViewTextBoxColumn100.DataPropertyName = "Customer_Name"
        Me.DataGridViewTextBoxColumn100.HeaderText = "อ้างอิง 1"
        Me.DataGridViewTextBoxColumn100.Name = "DataGridViewTextBoxColumn100"
        Me.DataGridViewTextBoxColumn100.Width = 120
        '
        'DataGridViewTextBoxColumn101
        '
        Me.DataGridViewTextBoxColumn101.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn101.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn101.Name = "DataGridViewTextBoxColumn101"
        Me.DataGridViewTextBoxColumn101.Width = 120
        '
        'DataGridViewTextBoxColumn102
        '
        Me.DataGridViewTextBoxColumn102.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn102.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn102.Name = "DataGridViewTextBoxColumn102"
        Me.DataGridViewTextBoxColumn102.Width = 150
        '
        'DataGridViewTextBoxColumn103
        '
        Me.DataGridViewTextBoxColumn103.DataPropertyName = "DistributionCenter"
        Me.DataGridViewTextBoxColumn103.HeaderText = "อ้างอิง 1"
        Me.DataGridViewTextBoxColumn103.Name = "DataGridViewTextBoxColumn103"
        '
        'DataGridViewTextBoxColumn104
        '
        Me.DataGridViewTextBoxColumn104.DataPropertyName = "Comment"
        Me.DataGridViewTextBoxColumn104.HeaderText = "หมายเหตุ"
        Me.DataGridViewTextBoxColumn104.Name = "DataGridViewTextBoxColumn104"
        '
        'DataGridViewTextBoxColumn105
        '
        Me.DataGridViewTextBoxColumn105.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn105.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn105.Name = "DataGridViewTextBoxColumn105"
        Me.DataGridViewTextBoxColumn105.Width = 150
        '
        'DataGridViewTextBoxColumn106
        '
        Me.DataGridViewTextBoxColumn106.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn106.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn106.Name = "DataGridViewTextBoxColumn106"
        Me.DataGridViewTextBoxColumn106.Width = 150
        '
        'DataGridViewTextBoxColumn107
        '
        Me.DataGridViewTextBoxColumn107.DataPropertyName = "Add_By"
        Me.DataGridViewTextBoxColumn107.HeaderText = "ผู้ออกเอกสาร"
        Me.DataGridViewTextBoxColumn107.Name = "DataGridViewTextBoxColumn107"
        Me.DataGridViewTextBoxColumn107.Width = 150
        '
        'frmTransportManifestView_Update
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1357, 912)
        Me.Controls.Add(Me.pnlHide)
        Me.Controls.Add(Me.tabTransportManifest)
        Me.Controls.Add(Me.pnLeftmenu)
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmTransportManifestView_Update"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show
        Me.Text = "รายการใบคุมรถ"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pnLeftmenu.ResumeLayout(False)
        Me.gbFilter.ResumeLayout(False)
        Me.gbFilter.PerformLayout()
        Me.grbPrintReport.ResumeLayout(False)
        Me.gbCondition.ResumeLayout(False)
        Me.gbCondition.PerformLayout()
        Me.tabTransportManifest.ResumeLayout(False)
        Me.tbpJobLoading.ResumeLayout(False)
        Me.SplitContainer4.Panel1.ResumeLayout(False)
        Me.SplitContainer4.Panel1.PerformLayout()
        Me.SplitContainer4.Panel2.ResumeLayout(False)
        Me.SplitContainer4.ResumeLayout(False)
        CType(Me.grdJobLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel4.ResumeLayout(False)
        Me.Panel4.PerformLayout()
        Me.tbpJobInTransport.ResumeLayout(False)
        Me.SplitContainer3.Panel1.ResumeLayout(False)
        Me.SplitContainer3.Panel1.PerformLayout()
        Me.SplitContainer3.Panel2.ResumeLayout(False)
        Me.SplitContainer3.ResumeLayout(False)
        CType(Me.grdJobInTransport, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.tbpJobReturn.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        Me.SplitContainer2.ResumeLayout(False)
        CType(Me.grdJobReturn, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.tabManifestComplete.ResumeLayout(False)
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.ResumeLayout(False)
        CType(Me.grdJob_Complete, System.ComponentModel.ISupportInitialize).EndInit()
        Me.PanelOrderItemButton.ResumeLayout(False)
        Me.PanelOrderItemButton.PerformLayout()
        Me.pnlHide.ResumeLayout(False)
        Me.pnlHide.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents pnLeftmenu As System.Windows.Forms.Panel
    Friend WithEvents gbFilter As System.Windows.Forms.GroupBox
    Friend WithEvents lblTransportJobType As System.Windows.Forms.Label
    Friend WithEvents cboTransportJobType As System.Windows.Forms.ComboBox
    Friend WithEvents gbCondition As System.Windows.Forms.GroupBox
    Friend WithEvents dtpDate As System.Windows.Forms.DateTimePicker
    Friend WithEvents lb_to As System.Windows.Forms.Label
    Friend WithEvents dateEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents txtKeySearch As System.Windows.Forms.TextBox
    Friend WithEvents btnSearch As System.Windows.Forms.Button
    Friend WithEvents rdbCustomer As System.Windows.Forms.RadioButton
    Friend WithEvents rdbTransportManifest_Date As System.Windows.Forms.RadioButton
    Friend WithEvents rdoTransportManifest_No As System.Windows.Forms.RadioButton
    Friend WithEvents rdoCustomerShipping As System.Windows.Forms.RadioButton
    Friend WithEvents rdoVehicleNo As System.Windows.Forms.RadioButton
    Friend WithEvents cboDistributionCenter As System.Windows.Forms.ComboBox
    Friend WithEvents lblDistributionCenter As System.Windows.Forms.Label
    Friend WithEvents cboSubRoute As System.Windows.Forms.ComboBox
    Friend WithEvents lblSubRoute As System.Windows.Forms.Label
    Friend WithEvents cboRoute As System.Windows.Forms.ComboBox
    Friend WithEvents lblRoute As System.Windows.Forms.Label
    Friend WithEvents tabTransportManifest As System.Windows.Forms.TabControl
    Friend WithEvents cboVehicleType As System.Windows.Forms.ComboBox
    Friend WithEvents lblVehicleType As System.Windows.Forms.Label
    Friend WithEvents grdJobLoading As System.Windows.Forms.DataGridView
    Friend WithEvents rdoContainerNo As System.Windows.Forms.RadioButton
    Friend WithEvents cboDocumentStatus As System.Windows.Forms.ComboBox
    Friend WithEvents lblStatus As System.Windows.Forms.Label
    Friend WithEvents tbpJobInTransport As System.Windows.Forms.TabPage
    Friend WithEvents tbpJobReturn As System.Windows.Forms.TabPage
    Friend WithEvents btnReleaseTruck As System.Windows.Forms.Button
    Friend WithEvents btnEditManifest As System.Windows.Forms.Button
    Friend WithEvents lbCountRows As System.Windows.Forms.Label
    Friend WithEvents btnReceiveDestination As System.Windows.Forms.Button
    Friend WithEvents btnEditJobInTransport As System.Windows.Forms.Button
    Friend WithEvents lbCountRows2 As System.Windows.Forms.Label
    Friend WithEvents grdJobInTransport As System.Windows.Forms.DataGridView
    Friend WithEvents btnReturn As System.Windows.Forms.Button
    Friend WithEvents btnEditJobReturn As System.Windows.Forms.Button
    Friend WithEvents lbCountRows3 As System.Windows.Forms.Label
    Friend WithEvents grdJobReturn As System.Windows.Forms.DataGridView
    Friend WithEvents btnConfirm As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn6 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn7 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn8 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn9 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn10 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn11 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn12 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn13 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn14 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn15 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn21 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn22 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn23 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn24 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn25 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn26 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn27 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn28 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn29 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn30 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn31 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn32 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn33 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn34 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn35 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn36 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn37 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn38 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn39 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn40 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn41 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn42 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn43 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn44 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn45 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn46 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn47 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn48 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn49 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn50 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn51 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn52 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn53 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn54 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn55 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn56 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn57 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn58 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn59 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn60 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn61 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn62 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn63 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn64 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn65 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn66 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn67 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn68 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn69 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn70 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn71 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn72 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn73 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn74 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn75 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents lblSumDoc_JobLoading As System.Windows.Forms.Label
    Friend WithEvents txtSumDoc_JobLoading As System.Windows.Forms.TextBox
    Friend WithEvents lblSumQty_JobLoading As System.Windows.Forms.Label
    Friend WithEvents lblSumWeight_JobLoading As System.Windows.Forms.Label
    Friend WithEvents lblSumVolume_JobLoading As System.Windows.Forms.Label
    Friend WithEvents lblSumAll As System.Windows.Forms.Label
    Friend WithEvents txtSumQty_JobLoading As System.Windows.Forms.TextBox
    Friend WithEvents txtSumVolume_JobLoading As System.Windows.Forms.TextBox
    Friend WithEvents txtSumWeight_JobLoading As System.Windows.Forms.TextBox
    Friend WithEvents lblSumDoc_JobInTransport As System.Windows.Forms.Label
    Friend WithEvents txtSumDoc_JobInTransport As System.Windows.Forms.TextBox
    Friend WithEvents lblSumQty_JobInTransport As System.Windows.Forms.Label
    Friend WithEvents lblSumWeight_JobInTransport As System.Windows.Forms.Label
    Friend WithEvents lblSumVolume_JobInTransport As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents txtSumQty_JobInTransport As System.Windows.Forms.TextBox
    Friend WithEvents txtSumVolume_JobInTransport As System.Windows.Forms.TextBox
    Friend WithEvents txtSumWeight_JobInTransport As System.Windows.Forms.TextBox
    Friend WithEvents lblSumDoc_JobReturn As System.Windows.Forms.Label
    Friend WithEvents txtSumDoc_JobReturn As System.Windows.Forms.TextBox
    Friend WithEvents lblSumQty_JobReturn As System.Windows.Forms.Label
    Friend WithEvents lblSumWeight_JobReturn As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents txtSumVolume_JobReturn As System.Windows.Forms.TextBox
    Friend WithEvents txtSumWeight_JobReturn As System.Windows.Forms.TextBox
    Friend WithEvents txtSumQty_JobReturn As System.Windows.Forms.TextBox
    Friend WithEvents lbltxtSumVolume_JobReturn As System.Windows.Forms.Label
    Friend WithEvents btnPop_Search As System.Windows.Forms.Button
    Friend WithEvents rdoDriver As System.Windows.Forms.RadioButton
    Friend WithEvents tabManifestComplete As System.Windows.Forms.TabPage
    Friend WithEvents lblSumDoc_JobComplete As System.Windows.Forms.Label
    Friend WithEvents txtSumDoc_JobComplete As System.Windows.Forms.TextBox
    Friend WithEvents lblSumQty_JobComplete As System.Windows.Forms.Label
    Friend WithEvents lblSumWeight_JobComplete As System.Windows.Forms.Label
    Friend WithEvents lblSumVolume_JobComplete As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtSumQty_Jobcomplete As System.Windows.Forms.TextBox
    Friend WithEvents txtSumVolume_JobComplete As System.Windows.Forms.TextBox
    Friend WithEvents txtSumWeight_JobComplete As System.Windows.Forms.TextBox
    Friend WithEvents lbCountRows4 As System.Windows.Forms.Label
    Friend WithEvents grdJob_Complete As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn76 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn77 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn78 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn79 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn80 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn81 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn82 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn83 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn84 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn85 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn86 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn87 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn88 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn89 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn90 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn91 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn92 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn93 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn94 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn95 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn96 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn97 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn98 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn99 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn100 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn101 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn102 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnCopy As System.Windows.Forms.Button
    Friend WithEvents btnAddManifest As System.Windows.Forms.Button
    Friend WithEvents DataGridViewTextBoxColumn103 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn104 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn105 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents cboDocumentType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents DataGridViewTextBoxColumn106 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents PanelOrderItemButton As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer4 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer3 As System.Windows.Forms.SplitContainer
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents chkSelectAll As System.Windows.Forms.CheckBox
    Friend WithEvents chkSelectAll1 As System.Windows.Forms.CheckBox
    Friend WithEvents chkItem_JobLoading As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_Index_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_No_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DocumentType_desc As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_No_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_ID_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Driver_Name_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Route_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SubRoute_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportJobType_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sum_Volume_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sum_Weight_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_VehicleType_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_CustomerShipping_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_CustomerShippingLocation_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Sum_Qty_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Count_Document_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ContainerNo1_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ContainerNo2_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Ref1_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Comment_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Add_By_JobLoading As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSelect_JobInTransport As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_TransportManifest_No_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_SourceOutGate_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_DestinationInGate_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_ReturnTruckInGate_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_Id_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_No_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Route_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SubRoute_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_VehicleType_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Driver_Name_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_Shipping_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ShippingLocation_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Bill_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_weight_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Volume_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Container_No1_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Container_No2_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportJopType_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Ref_No_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Comment_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Add_by_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_Id As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Index_JobInTransport As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_No_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Index_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_SourceOutGate_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_DestinationInGate_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_ReturnTruckInGate_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_Id_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_No_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_VehicleType_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Driver_Name_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_CustomerShipping_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ShippingLocation_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Weight_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Volume_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Bill_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Container_No2_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Container_No1_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportJoptype_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Route_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Ref_No_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Comment_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Add_By_JobComplete As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents chkSelect_JobReturn As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents col_TransportManifest_No_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportManifest_Date_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_SourceOutGate_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_DestinationInGate_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Time_ReturnTruckInGate_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_Id_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Vehicle_No_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Route_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_SubRoute_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_DistributionCenter_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_VehicleType_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Driver_Name_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_CustomerShipping_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_ShippingLocation_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Status_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Qty_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Bill_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Weight_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Volume_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Container_No2_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Container_No1_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_TransportJoptype_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Customer_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Ref_No_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Comment_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Add_By_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Col_status_Chk As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents col_Index_JobReturn As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents grbPrintReport As System.Windows.Forms.GroupBox
    Friend WithEvents cboPrint As System.Windows.Forms.ComboBox
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents pnlHide As System.Windows.Forms.Panel
    Friend WithEvents tbpJobLoading As System.Windows.Forms.TabPage
    Friend WithEvents DataGridViewCheckBoxColumn1 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn2 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewCheckBoxColumn3 As System.Windows.Forms.DataGridViewCheckBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn107 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents btnInterface As System.Windows.Forms.Button
End Class
