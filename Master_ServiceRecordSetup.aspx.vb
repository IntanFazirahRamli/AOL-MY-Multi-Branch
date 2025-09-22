Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_ServiceRecordSetup
    Inherits System.Web.UI.Page

    Public rcno As String


    Private Sub AccessControl()
        Try
            '''''''''''''''''''Access Control 
            If Session("SecGroupAuthority") <> "ADMINISTRATOR" Then
                Dim conn As MySqlConnection = New MySqlConnection()
                Dim command As MySqlCommand = New MySqlCommand

                conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
                conn.Open()

                command.CommandType = CommandType.Text
                'command.CommandText = "SELECT X0163,  X0163Add, X0163Edit, X0163Delete, X0163Print FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"
                command.CommandText = "SELECT x0176,  x0176Add, x0176Edit, X0176Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0176")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0176"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0176")) = False Then
                                Response.Redirect("Home.aspx")
                            End If
                        End If
                    End If

                    'If Not IsDBNull(dt.Rows(0)("X0163Add")) Then
                    '    If String.IsNullOrEmpty(dt.Rows(0)("X0163Add")) = False Then
                    '        Me.btnADD.Enabled = dt.Rows(0)("X0163Add").ToString()
                    '    End If
                    'End If

                    'If txtMode.Text = "View" Then
                    If Not IsDBNull(dt.Rows(0)("x0176Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0176Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0176Edit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("X0176Delete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X0176Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("X0176Delete").ToString()
                        End If
                    End If
                Else
                    Me.btnEdit.Enabled = False
                    Me.btnDelete.Enabled = False
                End If

                'If btnADD.Enabled = True Then
                '    btnADD.ForeColor = System.Drawing.Color.Black
                'Else
                '    btnADD.ForeColor = System.Drawing.Color.Gray
                'End If


                If btnEdit.Enabled = True Then
                    btnEdit.ForeColor = System.Drawing.Color.Black
                Else
                    btnEdit.ForeColor = System.Drawing.Color.Gray
                End If

                If btnDelete.Enabled = True Then
                    btnDelete.ForeColor = System.Drawing.Color.Black
                Else
                    btnDelete.ForeColor = System.Drawing.Color.Gray
                End If


                'If btnPrint.Enabled = True Then
                '    btnPrint.ForeColor = System.Drawing.Color.Black
                'Else
                '    btnPrint.ForeColor = System.Drawing.Color.Gray
                'End If


            End If
            'End If
            '''''''''''''''''''Access Control 
        Catch ex As Exception
            'MessageBox.Message.Alert(Page, ex.ToString, "str")
            lblAlert.Text = ex.Message.ToString
        End Try
    End Sub
    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

       btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black
        btnDelete.Enabled = True
        btnDelete.ForeColor = System.Drawing.Color.Black
        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        chkWebToZSoft.Enabled = False
        txtUploadDays.Enabled = False
        chkQuickServiceSetting.Enabled = False
        rdbScheduleAutoAssign.Enabled = False
        rdbAllocationMethod.Enabled = False
        txtAssignStart.Enabled = False
        txtAssignEnd.Enabled = False
        txtAssignDuration.Enabled = False
        txtAssignInterval.Enabled = False
        chkAutoUpdateClient.Enabled = False
        txtDefaultPrinter.Enabled = False
        txtApprovalScreenView.Enabled = False
        txtJobOrderRecord.Enabled = False
        txtSvcRecRecord.Enabled = False
        txtSvcContractRecord.Enabled = False
        chkDisableDelete.Enabled = False
        txtJobOrderMaxRecs.Enabled = False
        txtSvcRecMaxRecs.Enabled = False
        txtSvcContractMaxRecs.Enabled = False
        chkShowGridRecords.Enabled = False
        txtSchDaysLimit.Enabled = False

        chkServiceContract.Enabled = False
        rdbSvcRecOrder.Enabled = False
        chkSvcRecord.Enabled = False
        txtAttempts.Enabled = False
        chkCalendar.Enabled = False

        txtSupervisorRec.Enabled = False
        chkAttachServiceReport.Enabled = False
        chkDisplayRecordsLocationwise.Enabled = False

        txtweekdaystart.enabled = False
        txtweekdayend.enabled = False
        txtweekdayotrate.enabled = False

        txtsatstart.enabled = False
        txtsatend.enabled = False
        txtsatotrate.enabled = False

        txtsunstart.enabled = False
        txtsunend.enabled = False
        txtsunotrate.enabled = False

        txtholidaystart.enabled = False
        txtholidayend.enabled = False
        txtholidayotrate.enabled = False
        ddlTerminationCode.Enabled = False

        chkAutoRenewal.Enabled = False
        txtPrefixDocNoContract.Enabled = False
        txtPrefixDocNoService.Enabled = False

        txtPriceIncreaseLimit.Enabled = False
        txtPriceDecreaseLimit.Enabled = False

        chkEnableSaveUploadLater.Enabled = False
        'chkEnableAdhocReport.Enabled = False
        chkEnableAdhocReport.Enabled = False
        chkContinuousContract.Enabled = False
        ddlMobileAppServiceDateMethod.Enabled = False

        chkSMSServices.Enabled = False
        txtSMSDays.Enabled = False

        txtPrefixDocNoCustomerRequest.Enabled = False
        pnlRenewalNotification.Enabled = False
        pnlExpiredNotification.Enabled = False
        lblAlert.Text = ""

        ddlContractRenewalNewContractCode.Enabled = False
        ddlContractRevisionNewContractCode.Enabled = False
        ddlContractRenewalTerminationCode.Enabled = False
        ddlContractVoidCode.Enabled = False

        chkLoadContractAddButton.Enabled = False
        chkDisplayFixedDurationType.Enabled = False
        chkDisplayContinuousDurationType.Enabled = False

        txtDaysBeforeRenewalMessage.Enabled = False
        txtDaysBeforeScheduleGeneration.Enabled = False

        txtEmailNotiifcationsScheduleChange.Enabled = False

        'chkSendEmailNotificationForRenewal.Enabled = False
        'ddlContractGroup.Enabled = False
        'txtDaysBeforeExpiry.Enabled = False

        'txtContractGroupForEmailNotificationOfContractRenewal.Enabled = False

        'chkSendEmailNotificationForExpiredContracts.Enabled = False
        'txtEmailAddressForEmailNotificationForExpiredContracts.Enabled = False

        chkEmailNotificationforContractNoSchedule.Enabled = False
        txtEmailAddressforContractsNoSchedule.Enabled = False

        chkEmailNotificationForContractStatusChange.Enabled = False
        txtEmailAddressForContractStatusChange.Enabled = False
        chkEmailNotificationForContractStatusSalesman.Enabled = False

        AccessControl()
    End Sub

    Private Sub DisableControls()
        btnSave.Enabled = True
        btnSave.ForeColor = System.Drawing.Color.Black
        btnCancel.Enabled = True
        btnCancel.ForeColor = System.Drawing.Color.Black

        btnEdit.Enabled = False
        btnEdit.ForeColor = System.Drawing.Color.Gray

        btnQuit.Enabled = False
        btnQuit.ForeColor = System.Drawing.Color.Gray

        chkWebToZSoft.Enabled = True
        txtUploadDays.Enabled = True
        chkQuickServiceSetting.Enabled = True
        rdbScheduleAutoAssign.Enabled = True
        rdbAllocationMethod.Enabled = True
        txtAssignStart.Enabled = True
        txtAssignEnd.Enabled = True
        txtAssignDuration.Enabled = True
        txtAssignInterval.Enabled = True
        chkAutoUpdateClient.Enabled = True
        txtDefaultPrinter.Enabled = True
        txtApprovalScreenView.Enabled = True
        txtJobOrderRecord.Enabled = True
        txtSvcRecRecord.Enabled = True
        txtSvcContractRecord.Enabled = True
        chkDisableDelete.Enabled = True
        txtJobOrderMaxRecs.Enabled = True
        txtSvcRecMaxRecs.Enabled = True
        txtSvcContractMaxRecs.Enabled = True
        chkShowGridRecords.Enabled = True
        txtSchDaysLimit.Enabled = True

        chkServiceContract.Enabled = True
        rdbSvcRecOrder.Enabled = True
        chkSvcRecord.Enabled = True
        txtAttempts.Enabled = True
        chkCalendar.Enabled = True

        txtSupervisorRec.Enabled = True
        chkAttachServiceReport.Enabled = True
        chkDisplayRecordsLocationwise.Enabled = True

        txtweekdaystart.enabled = True
        txtweekdayend.enabled = True
        txtweekdayotrate.enabled = True

        txtsatstart.enabled = True
        txtsatend.enabled = True
        txtsatotrate.enabled = True

        txtsunstart.enabled = True
        txtsunend.enabled = True
        txtsunotrate.enabled = True

        txtholidaystart.enabled = True
        txtholidayend.enabled = True
        txtholidayotrate.enabled = True
        ddlTerminationCode.Enabled = True

        chkAutoRenewal.Enabled = True
        txtPrefixDocNoContract.Enabled = True
        txtPrefixDocNoService.Enabled = True
        txtPriceIncreaseLimit.Enabled = True
        txtPriceDecreaseLimit.Enabled = True

        chkEnableSaveUploadLater.Enabled = True
        chkEnableAdhocReport.Enabled = True
        chkContinuousContract.Enabled = True
        ddlMobileAppServiceDateMethod.Enabled = True
        chkSMSServices.Enabled = True
        txtSMSDays.Enabled = True

        txtPrefixDocNoCustomerRequest.Enabled = True

        pnlRenewalNotification.Enabled = True
        pnlExpiredNotification.Enabled = True

        ddlContractRenewalNewContractCode.Enabled = True
        ddlContractRevisionNewContractCode.Enabled = True
        ddlContractRenewalTerminationCode.Enabled = True
        ddlContractVoidCode.Enabled = True

        chkLoadContractAddButton.Enabled = True
        chkDisplayFixedDurationType.Enabled = True
        chkDisplayContinuousDurationType.Enabled = True
        txtDaysBeforeRenewalMessage.Enabled = True
        txtDaysBeforeScheduleGeneration.Enabled = True

        lblAlert.Text = ""

        txtEmailNotiifcationsScheduleChange.Enabled = True

        'chkSendEmailNotificationForRenewal.Enabled = True
        'ddlContractGroup.Enabled = True
        'txtDaysBeforeExpiry.Enabled = True

        'txtContractGroupForEmailNotificationOfContractRenewal.Enabled = True

        'chkSendEmailNotificationForExpiredContracts.Enabled = True
        'txtEmailAddressForEmailNotificationForExpiredContracts.Enabled = True

        chkEmailNotificationforContractNoSchedule.Enabled = True
        txtEmailAddressforContractsNoSchedule.Enabled = True

        chkEmailNotificationForContractStatusChange.Enabled = True
        chkEmailNotificationForContractStatusSalesman.Enabled = True

        txtEmailAddressForContractStatusChange.Enabled = True

    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EnableControls()


            SqlDSContractGroup.SelectCommand = "SELECT ContractGroup,CONCAT(ContractGroup, ' - ', ContractGroup) AS ContractGroup from tblContractGroup order by ContractGroup"
            SqlDSContractGroup.DataBind()

            ddlContractGroupForEmailNotificationOfContractRenewal.DataBind()

            RetrieveData()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID


            '            SELECT CONCAT(Code, ' - ', Description) AS TC FROM tblterminationcode 
            'where Status = 'Y' ORDER BY Code
        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub

    Private Sub RetrieveData()
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()

        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblservicerecordmastersetup where rcno=1"
        '  command1.Parameters.AddWithValue("@city", txtCity.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            'ZSOFT - WEB - TABLET SETTING

            If dt.Rows(0)("WebToZsoftSubmitStatus").ToString = "1" Then
                chkWebToZSoft.Items.FindByValue("1").Selected = True
            Else
                chkWebToZSoft.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("WebToZsoftFileStatus").ToString = "1" Then
                chkWebToZSoft.Items.FindByValue("2").Selected = True
            Else
                chkWebToZSoft.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("WebToZsoftDetStatus").ToString = "1" Then
                chkWebToZSoft.Items.FindByValue("3").Selected = True
            Else
                chkWebToZSoft.Items.FindByValue("3").Selected = False
            End If

            If dt.Rows(0)("WebToZsoftStatus").ToString = "1" Then
                chkWebToZSoft.Items.FindByValue("4").Selected = True
            Else
                chkWebToZSoft.Items.FindByValue("4").Selected = False
            End If

            If dt.Rows(0)("WebToZsoftGeneratePDF").ToString = "1" Then
                chkWebToZSoft.Items.FindByValue("5").Selected = True
            Else
                chkWebToZSoft.Items.FindByValue("5").Selected = False
            End If

            '    txtUploadDays.Text = dt.Rows(0)("UploadDayLimit").ToString

            'QUICK SERVICE SETTING

            If dt.Rows(0)("QuickServiceFileStatus").ToString = "1" Then
                chkQuickServiceSetting.Items.FindByValue("1").Selected = True
            Else
                chkQuickServiceSetting.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("QuickServiceGeneratePDF").ToString = "1" Then
                chkQuickServiceSetting.Items.FindByValue("2").Selected = True
            Else
                chkQuickServiceSetting.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("QuickServiceSubmitStatus").ToString = "1" Then
                chkQuickServiceSetting.Items.FindByValue("3").Selected = True
            Else
                chkQuickServiceSetting.Items.FindByValue("3").Selected = False
            End If

            'SCHEDULE AUTO ASSIGN

            rdbScheduleAutoAssign.SelectedValue = dt.Rows(0)("ScheduleAutoAssignBy").ToString
            rdbAllocationMethod.SelectedValue = dt.Rows(0)("AllocationMethod").ToString

            txtAssignStart.Text = dt.Rows(0)("AutoAssignStart").ToString
            txtAssignEnd.Text = dt.Rows(0)("AutoAssignEnd").ToString

            txtAssignInterval.Text = dt.Rows(0)("AutoAssignInterval").ToString
            txtAssignDuration.Text = dt.Rows(0)("AutoAssignDuration").ToString

            'AUTO UPDATE CLIENT DETAILS

            If dt.Rows(0)("AutoUpdateClientDetails").ToString = "1" Then
                chkAutoUpdateClient.Items.FindByValue("1").Selected = True
            Else
                chkAutoUpdateClient.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("AutoUpdateClientDetailsServiceContract").ToString = "1" Then
                chkAutoUpdateClient.Items.FindByValue("2").Selected = True
            Else
                chkAutoUpdateClient.Items.FindByValue("2").Selected = False
            End If

            'JOB ORDER

            txtDefaultPrinter.Text = dt.Rows(0)("PrinterNameJobOrder").ToString

            'GENERAL SETTINGS

            txtApprovalScreenView.Text = dt.Rows(0)("PastRecordNumber").ToString
            txtJobOrderRecord.Text = dt.Rows(0)("RecordPeriodJob").ToString
            txtSvcRecRecord.Text = dt.Rows(0)("RecordPeriodService").ToString
            txtSvcContractRecord.Text = dt.Rows(0)("RecordPeriodContract").ToString

            If dt.Rows(0)("DisableDeleteSR").ToString = "1" Then
                chkDisableDelete.Items.FindByValue("1").Selected = True
            Else
                chkDisableDelete.Items.FindByValue("1").Selected = False
            End If
            If dt.Rows(0)("DisableDeleteSC").ToString = "1" Then
                chkDisableDelete.Items.FindByValue("2").Selected = True
            Else
                chkDisableDelete.Items.FindByValue("2").Selected = False
            End If

            'MAXIMUM GRID RECORDS

            txtJobOrderMaxRecs.Text = dt.Rows(0)("JobOrderMaxRec").ToString
            txtSvcRecMaxRecs.Text = dt.Rows(0)("ServiceRecordMaxRec").ToString
            txtSvcContractMaxRecs.Text = dt.Rows(0)("ServiceContractMaxRec").ToString

            'GRID RECORDS ON SCREEN LOAD

            If dt.Rows(0)("ShowSROnScreenLoad").ToString = "1" Then
                chkShowGridRecords.Items.FindByValue("1").Selected = True
            Else
                chkShowGridRecords.Items.FindByValue("1").Selected = False
            End If
            If dt.Rows(0)("ShowSCOnScreenLoad").ToString = "1" Then
                chkShowGridRecords.Items.FindByValue("2").Selected = True
            Else
                chkShowGridRecords.Items.FindByValue("2").Selected = False
            End If

            'SERVICE CONTRACT

            If dt.Rows(0)("ScheduleSVAfterUpdateContr").ToString = "1" Then
                chkServiceContract.Items.FindByValue("1").Selected = True
            Else
                chkServiceContract.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("ConfirmForServiceAddressEButton").ToString = "1" Then
                chkServiceContract.Items.FindByValue("2").Selected = True
            Else
                chkServiceContract.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("IncOurRefYourRef").ToString = "1" Then
                chkServiceContract.Items.FindByValue("3").Selected = True
            Else
                chkServiceContract.Items.FindByValue("3").Selected = False
            End If



            If dt.Rows(0)("TeamIDMandatory").ToString = "1" Then
                chkServiceContract.Items.FindByValue("4").Selected = True
            Else
                chkServiceContract.Items.FindByValue("4").Selected = False
            End If

            If dt.Rows(0)("ServiceByMandatory").ToString = "1" Then
                chkServiceContract.Items.FindByValue("5").Selected = True
            Else
                chkServiceContract.Items.FindByValue("5").Selected = False
            End If

            If dt.Rows(0)("WindowsSVCAutoServiceSchedule").ToString = "1" Then
                chkServiceContract.Items.FindByValue("6").Selected = True
            Else
                chkServiceContract.Items.FindByValue("6").Selected = False
            End If

            If dt.Rows(0)("AllowTerminationBeforeLastService").ToString = "1" Then
                chkServiceContract.Items.FindByValue("7").Selected = True
            Else
                chkServiceContract.Items.FindByValue("7").Selected = False
            End If




            'If dt.Rows(0)("BackDateContract").ToString = "1" Then
            '    chkServiceContract.Items.FindByValue("4").Selected = True
            'Else
            '    chkServiceContract.Items.FindByValue("4").Selected = False
            'End If

            'If dt.Rows(0)("BackDateContractTermination").ToString = "1" Then
            '    chkServiceContract.Items.FindByValue("5").Selected = True
            'Else
            '    chkServiceContract.Items.FindByValue("5").Selected = False
            'End If

            If String.IsNullOrEmpty(dt.Rows(0)("ContractRevisionTerminationCode").ToString) = False Then
                '   ddlTerminationCode.Text = dt.Rows(0)("ContractRevisionTerminationCode").ToString

                ddlTerminationCode.SelectedItem.Text = dt.Rows(0)("ContractRevisionTerminationCode").ToString
            End If

            txtPrefixDocNoContract.Text = dt.Rows(0)("PrefixDocNoContract").ToString

            If dt.Rows(0)("AutoRenewal").ToString = "1" Then
                chkAutoRenewal.Checked = True
            Else
                chkAutoRenewal.Checked = False
            End If


            'SERVICE RECORD SETTINGS

            rdbSvcRecOrder.SelectedValue = dt.Rows(0)("SROrder").ToString

            If dt.Rows(0)("EApprovalAutoUpdateSR").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("1").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("NotAllowSaveIfDetStIsO").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("2").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("chkCalTotalDurationByTimeInOut").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("3").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("3").Selected = False
            End If

            If dt.Rows(0)("AutoEmailCheckAttempts").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("4").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("4").Selected = False
            End If

            If dt.Rows(0)("DisplayTimeInTimeOutInServiceReport").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("5").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("5").Selected = False
            End If

            If dt.Rows(0)("EnableEmailValidation").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("6").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("6").Selected = False
            End If


            If dt.Rows(0)("AllowToEditBilledAmtBillNo").ToString = "1" Then
                chkSvcRecord.Items.FindByValue("7").Selected = True
            Else
                chkSvcRecord.Items.FindByValue("7").Selected = False
            End If

            txtAttempts.Text = dt.Rows(0)("AutoEmailAttemptsLimit").ToString

            txtSupervisorRec.Text = dt.Rows(0)("SupervisorRecDays").ToString
            txtPrefixDocNoService.Text = dt.Rows(0)("PrefixDocNoService").ToString

            txtSchDaysLimit.Text = dt.Rows(0)("UploadDayLimit").ToString

            txtEmailAddressForEmailNotificationForExpiredContracts.Text = dt.Rows(0)("EmailAddressForEmailNotificationForExpiredContracts").ToString
            txtEmailAddressForEmailNotificationOfContractRenewal.Text = dt.Rows(0)("EmailAddressForEmailNotificationOfContractRenewal").ToString
            txtDaysBeforeExpiry.Text = dt.Rows(0)("NotificationDaysBeforeExpiry").ToString

            'txtPriceIncreaseLimit.Text = dt.Rows(0)("PriceIncreaseLimit").ToString
            'txtPriceDecreaseLimit.Text = dt.Rows(0)("PriceDecreaseLimit").ToString

            If dt.Rows(0)("EmailNotificationForContractRenewal").ToString = "1" Then
                chkSendEmailNotificationForRenewal.Checked = True
            Else
                chkSendEmailNotificationForRenewal.Checked = False
            End If

            If dt.Rows(0)("EmailNotificationForExpiredContract").ToString = "1" Then
                chkSendEmailNotificationForExpiredContracts.Checked = True
            Else
                chkSendEmailNotificationForExpiredContracts.Checked = False
            End If

            If dt.Rows(0)("EmailNotificationforContractsNoSchedule").ToString = "1" Then
                chkEmailNotificationforContractNoSchedule.Checked = True
            Else
                chkEmailNotificationforContractNoSchedule.Checked = False
            End If
            txtEmailAddressforContractsNoSchedule.Text = dt.Rows(0)("EmailAddressForContractsNoSchedule").ToString

            If dt.Rows(0)("EmailNotificationforContractStatusChange").ToString = "1" Then
                chkEmailNotificationForContractStatusChange.Checked = True
            Else
                chkEmailNotificationForContractStatusChange.Checked = False
            End If

            If dt.Rows(0)("EmailNotificationForContractStatusSalesman").ToString = "1" Then
                chkEmailNotificationForContractStatusSalesman.Checked = True
            Else
                chkEmailNotificationForContractStatusSalesman.Checked = False
            End If

            txtEmailAddressForContractStatusChange.Text = dt.Rows(0)("EmailAddressForContractStatusChange").ToString

            '''

            If String.IsNullOrEmpty(dt.Rows(0)("ContractGroupForEmailNotificationofContractRenewal").ToString) = False Then
                Dim stringList As List(Of String) = dt.Rows(0)("ContractGroupForEmailNotificationofContractRenewal").ToString.Split(","c).ToList()
                Dim YrStrList As List(Of [String]) = New List(Of String)()

                For Each str As String In stringList

                    'txtEmailAddressForEmailNotificationForExpiredContracts.Text = ddlContractGroupForEmailNotificationOfContractRenewal.Items.Count
                    For Each item As ListItem In ddlContractGroupForEmailNotificationOfContractRenewal.Items

                        'If item.Text.Substring(0, item.Text.IndexOf("-")).TrimEnd = str.Trim Then
                        If item.Text = str.Trim Then
                            item.Selected = True
                            Exit For
                        End If
                    Next
                Next
            End If

            '''


            'Dim lContractGroup As String
            'lContractGroup = ""

            'For Each item As ListItem In ddlContractGroupForEmailNotificationOfContractRenewal.Items

            '    If item.Selected Then
            '        lContractGroup += item.Value + ","

            '    End If
            'Next

            'If String.IsNullOrEmpty(lContractGroup) = False Then

            'Else

            'End If
            'ZSOFT CALENDAR

            If dt.Rows(0)("JobOrderAssignment").ToString = "1" Then
                chkCalendar.Checked = True
            Else
                chkCalendar.Checked = False
            End If


            'SERVICE REPORT

            If dt.Rows(0)("AttachServiceReport").ToString = "1" Then
                chkAttachServiceReport.Items.FindByValue("1").Selected = True
            Else
                chkAttachServiceReport.Items.FindByValue("1").Selected = False
            End If
            If dt.Rows(0)("AttachSupplServiceReport").ToString = "1" Then
                chkAttachServiceReport.Items.FindByValue("2").Selected = True
            Else
                chkAttachServiceReport.Items.FindByValue("2").Selected = False
            End If



            If dt.Rows(0)("DisplayRecordsLocationwise").ToString = "Y" Then
                chkDisplayRecordsLocationwise.Items.FindByValue("1").Selected = True
            Else
                chkDisplayRecordsLocationwise.Items.FindByValue("1").Selected = False
            End If

            txtWeekDayStart.Text = dt.Rows(0)("WeekDayStart").ToString
            txtWeekDayEnd.Text = dt.Rows(0)("WeekDayEnd").ToString
            txtWeekDayOTRate.Text = dt.Rows(0)("WeekDayOTRate").ToString

            txtSatStart.Text = dt.Rows(0)("SatStart").ToString
            txtSatEnd.Text = dt.Rows(0)("SatEnd").ToString
            txtSatOTRate.Text = dt.Rows(0)("SatOTRate").ToString

            txtSunStart.Text = dt.Rows(0)("SunStart").ToString
            txtSunEnd.Text = dt.Rows(0)("SunEnd").ToString
            txtSunOTRate.Text = dt.Rows(0)("SunOTRate").ToString

            txtHolidayStart.Text = dt.Rows(0)("HolidayStart").ToString
            txtHolidayEnd.Text = dt.Rows(0)("HolidayEnd").ToString
            txtHolidayOTRate.Text = dt.Rows(0)("HolidayOTRate").ToString


            If dt.Rows(0)("EnableMobileAppSaveUploadLater").ToString = "1" Then
                chkEnableSaveUploadLater.Checked = True
            Else
                chkEnableSaveUploadLater.Checked = False
            End If

            If dt.Rows(0)("EnableAdhocServiceReport").ToString = "1" Then
                chkEnableAdhocReport.Checked = True
            Else
                chkEnableAdhocReport.Checked = False
            End If

            If dt.Rows(0)("ContinuousContract").ToString = "1" Then
                chkContinuousContract.Checked = True
            Else
                chkContinuousContract.Checked = False
            End If


            If String.IsNullOrEmpty(dt.Rows(0)("MobileAppServiceDateMethod").ToString) = False Then
                ddlMobileAppServiceDateMethod.Text = dt.Rows(0)("MobileAppServiceDateMethod").ToString
            Else
                ddlMobileAppServiceDateMethod.SelectedIndex = 0
            End If

            If dt.Rows(0)("EnableSMSCorporate").ToString = "1" Then
                chkSMSServices.Items.FindByValue("2").Selected = True
            Else
                chkSMSServices.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("EnableSMSResidential").ToString = "1" Then
                chkSMSServices.Items.FindByValue("1").Selected = True
            Else
                chkSMSServices.Items.FindByValue("1").Selected = False
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("SMSReminderDays").ToString) = False Then
                txtSMSDays.Text = dt.Rows(0)("SMSReminderDays").ToString
            Else
                txtSMSDays.Text = ""
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("PrefixDocNoCustomerRequest").ToString) = False Then
                txtPrefixDocNoCustomerRequest.Text = dt.Rows(0)("PrefixDocNoCustomerRequest").ToString
            Else
                txtPrefixDocNoCustomerRequest.Text = ""
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("ContractRenewalNewContractCode").ToString) = False Then
                ddlContractRenewalNewContractCode.Text = dt.Rows(0)("ContractRenewalNewContractCode").ToString
            Else
                ddlContractRenewalNewContractCode.SelectedIndex = 0
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("ContractRenewalTerminationCode").ToString) = False Then
                '  ddlContractRenewalTerminationCode.Text = dt.Rows(0)("ContractRenewalTerminationCode").ToString
                ddlContractRenewalTerminationCode.SelectedItem.Text = dt.Rows(0)("ContractRenewalTerminationCode").ToString
            Else
                ddlContractRenewalTerminationCode.SelectedIndex = 0
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("ContractVoidCode").ToString) = False Then
                '  ddlContractVoidCode.Text = dt.Rows(0)("ContractVoidCode").ToString
                ddlContractVoidCode.SelectedItem.Text = dt.Rows(0)("ContractVoidCode").ToString
            Else
                ddlContractVoidCode.SelectedIndex = 0
            End If

            If String.IsNullOrEmpty(dt.Rows(0)("ContractRevisionNewContractCode").ToString) = False Then
                ddlContractRevisionNewContractCode.Text = dt.Rows(0)("ContractRevisionNewContractCode").ToString
            Else
                ddlContractRevisionNewContractCode.SelectedIndex = 0
            End If

            If dt.Rows(0)("EnableAddButtonInContractOnLoad").ToString = "Y" Then
                chkLoadContractAddButton.Checked = True
            Else
                chkLoadContractAddButton.Checked = False
            End If


            txtDaysBeforeRenewalMessage.Text = dt.Rows(0)("DaysBeforeRenewalMessage").ToString
            txtDaysBeforeScheduleGeneration.Text = dt.Rows(0)("DaysBeforeScheduleGeneration").ToString

            txtEmailNotiifcationsScheduleChange.Text = dt.Rows(0)("EmailsforNotificationofScheduleChange").ToString


            'If dt.Rows(0)("DisplayFixedDurationType").ToString = "Y" Then
            '    chkDisplayFixedDurationType.Checked = True
            'Else
            '    chkDisplayFixedDurationType.Checked = False
            'End If

            'If dt.Rows(0)("DisplayContinuousDurationType").ToString = "Y" Then
            '    chkDisplayContinuousDurationType.Checked = True
            'Else
            '    chkDisplayContinuousDurationType.Checked = False
            'End If
        End If

    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"
    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        EnableControls()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        Try
            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            '    'Dim ind As String
            '    'ind = txtcity.Text
            '    'ind = ind.Replace("'", "\\'")

            '    Dim command2 As MySqlCommand = New MySqlCommand

            '    command2.CommandType = CommandType.Text

            'command2.CommandText = "SELECT * FROM tblservicerecordmastersetup where city=@city and rcno<>" & Convert.ToInt32(txtRcno.Text)
            '    command2.Parameters.AddWithValue("@city", txtCity.Text)
            '    command2.Connection = conn

            '    Dim dr1 As MySqlDataReader = command2.ExecuteReader()
            '    Dim dt1 As New DataTable
            '    dt1.Load(dr1)

            '    If dt1.Rows.Count > 0 Then

            '        '  MessageBox.Message.Alert(Page, "city already exists!!!", "str")
            '        lblAlert.Text = "CITY ALREADY EXISTS"
            '        txtCity.Focus()
            '        Exit Sub
            '    Else

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblservicerecordmastersetup where rcno=1"
            command1.Connection = conn

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String
                qry = "UPDATE tblservicerecordmastersetup SET WebToZsoftSubmitStatus = @WebToZsoftSubmitStatus,WebToZsoftFileStatus = @WebToZsoftFileStatus,WebToZsoftGeneratePDF = @WebToZsoftGeneratePDF, "
                qry = qry + " QuickServiceFileStatus = @QuickServiceFileStatus,QuickServiceGeneratePDF = @QuickServiceGeneratePDF,QuickServiceSubmitStatus = @QuickServiceSubmitStatus,LastAddEditStaff = @LastAddEditStaff,LastAddEditDate = @LastAddEditDate,"
                qry = qry + " PastRecordNumber = @PastRecordNumber,AllocationMethod = @AllocationMethod,ScheduleAutoAssignBy = @ScheduleAutoAssignBy,JobOrderAssignment = @JobOrderAssignment,JobOrderMaxRec = @JobOrderMaxRec,ServiceRecordMaxRec = @ServiceRecordMaxRec,"
                qry = qry + " RecordPeriodJob = @RecordPeriodJob,RecordPeriodService = @RecordPeriodService,RecordPeriodContract = @RecordPeriodContract,ScheduleSVAfterUpdateContr = @ScheduleSVAfterUpdateContr,WebToZsoftStatus = @WebToZsoftStatus,UploadDayLimit = @UploadDayLimit,"
                qry = qry + " WebToZsoftDetStatus = @WebToZsoftDetStatus,DisableDeleteSR = @DisableDeleteSR,DisableDeleteSC = @DisableDeleteSC,SROrder = @SROrder,ShowSROnScreenLoad = @ShowSROnScreenLoad,ShowSCOnScreenLoad = @ShowSCOnScreenLoad,AutoUpdateClientDetails = @AutoUpdateClientDetails,"
                qry = qry + " PrinterNameJobOrder = @PrinterNameJobOrder,AutoAssignStart = @AutoAssignStart,AutoAssignEnd = @AutoAssignEnd,AutoAssignInterval = @AutoAssignInterval,AutoAssignDuration = @AutoAssignDuration,EApprovalAutoUpdateSR = @EApprovalAutoUpdateSR,chkCalTotalDurationByTimeInOut = @chkCalTotalDurationByTimeInOut,"
                qry = qry + " AutoEmailCheckAttempts = @AutoEmailCheckAttempts,AutoEmailAttemptsLimit = @AutoEmailAttemptsLimit,AutoUpdateClientDetailsServiceContract = @AutoUpdateClientDetailsServiceContract,ConfirmForServiceAddressEButton = @ConfirmForServiceAddressEButton,IncOurRefYourRef = @IncOurRefYourRef, "
                qry = qry + " SupervisorRecDays = @SupervisorRecDays, AttachServiceReport = @AttachServiceReport,AttachSupplServiceReport=@AttachSupplServiceReport, DisplayRecordsLocationwise=@DisplayRecordsLocationwise,Weekdaystart=@WeekdayStart,WeekDayEnd=@WeekdayEnd,WeekdayOTRate=@WeekdayOTRate,"
                qry = qry + " SatStart=@SatStart,SatEnd=@SatEnd,SatOTRate=@SatOTRate,SunStart=@SunStart,SunEnd=@SunEnd,SunOTRate=@SunOTRate,HolidayStart=@HolidayStart,HolidayEnd=@HolidayEnd,HolidayOTRate=@HolidayOTRate,  "
                qry = qry + " ContractRevisionTerminationCode=@ContractRevisionTerminationCode, PrefixDocNoContract=@PrefixDocNoContract, PrefixDocNoService=@PrefixDocNoService, AutoRenewal=@AutoRenewal, "
                qry = qry + " EnableMobileAppSaveUploadLater=@EnableMobileAppSaveUploadLater, EnableAdhocServiceReport=@EnableAdhocServiceReport,  ContinuousContract=@ContinuousContract, TeamIDMandatory=@TeamIDMandatory, ServiceByMandatory=@ServiceByMandatory, MobileAppServiceDateMethod =@MobileAppServiceDateMethod,"
                qry = qry + " EnableSMSCorporate=@EnableSMSCorporate,EnableSMSResidential=@EnableSMSResidential, WindowsSVCAutoServiceSchedule=@WindowsSVCAutoServiceSchedule, "
                qry = qry + " EmailNotificationForContractRenewal=@EmailNotificationForContractRenewal, ContractGroupForEmailNotificationOfContractRenewal=@ContractGroupForEmailNotificationOfContractRenewal, "
                qry = qry + " EmailAddressForEmailNotificationOfContractRenewal =@EmailAddressForEmailNotificationOfContractRenewal, EmailNotificationForExpiredContract =@EmailNotificationForExpiredContract, "
                qry = qry + " EmailAddressForEmailNotificationForExpiredContracts =@EmailAddressForEmailNotificationForExpiredContracts, "
                qry = qry + " NotificationDaysBeforeExpiry =@NotificationDaysBeforeExpiry, AllowTerminationBeforeLastService =@AllowTerminationBeforeLastService, PrefixDocNoCustomerRequest=@PrefixDocNoCustomerRequest,DisplayTimeInTimeOutInServiceReport=@DisplayTimeInTimeOutInServiceReport, "

                qry = qry + " ContractRevisionNewContractCode =@ContractRevisionNewContractCode, ContractRenewalNewContractCode =@ContractRenewalNewContractCode, ContractRenewalTerminationCode=@ContractRenewalTerminationCode,SMSReminderDays=@SMSReminderDays,EnableEmailValidation=@EnableEmailValidation, "
                qry = qry + " EnableAddButtonInContractOnLoad =@EnableAddButtonInContractOnLoad, DaysBeforeRenewalMessage=@DaysBeforeRenewalMessage, DaysBeforeScheduleGeneration=@DaysBeforeScheduleGeneration,EmailsforNotificationofScheduleChange=@EmailsforNotificationofScheduleChange, ContractVoidCode=@ContractVoidCode, "
                qry = qry + "EmailNotificationforContractsNoSchedule=@EmailNotificationforContractsNoSchedule,EmailAddressForContractsNoSchedule=@EmailAddressForContractsNoSchedule,"
                qry = qry + "EmailNotificationforContractStatusChange=@EmailNotificationforContractStatusChange,EmailNotificationforContractStatusSalesman=@EmailNotificationforContractStatusSalesman,EmailAddressForContractStatusChange=@EmailAddressForContractStatusChange,  AllowToEditBilledAmtBillNo= @AllowToEditBilledAmtBillNo"

                'ContractRevisionTerminationCode=@ContractRevisionTerminationCode,
                'qry = qry + " PriceIncreaseLimit =@PriceIncreaseLimit, PriceDecreaseLimit =@PriceDecreaseLimit "

                qry = qry + " WHERE RcNo = 1;"

                'qry = " SatStart=@SatStart,SatEnd=@SatEnd,SatOTRate=@SatOTRate,SunStart=@SunStart,SunEnd=@SunEnd,SunOTRate=@SunOTRate,HolidayStart=@HolidayStart,HolidayEnd=@HolidayEnd,HolidayOTRate=@HolidayOTRate, BackDateContract= @BackDateContract, BackDateContractTermination= @BackDateContractTermination, ContractRevisionTerminationCode=@ContractRevisionTerminationCode, PrefixDocNoContract=@PrefixDocNoContract, PrefixDocNoService=@PrefixDocNoService, AutoRenewal=@AutoRenewal WHERE RcNo = 1;"


                'qry = "UPDATE tblservicerecordmastersetup SET WebToZsoftSubmitStatus = @WebToZsoftSubmitStatus,WebToZsoftFileStatus = @WebToZsoftFileStatus,WebToZsoftGeneratePDF = @WebToZsoftGeneratePDF,QuickServiceFileStatus = @QuickServiceFileStatus,QuickServiceGeneratePDF = @QuickServiceGeneratePDF,QuickServiceSubmitStatus = @QuickServiceSubmitStatus,LastAddEditStaff = @LastAddEditStaff,LastAddEditDate = @LastAddEditDate,PastRecordNumber = @PastRecordNumber,AllocationMethod = @AllocationMethod,ScheduleAutoAssignBy = @ScheduleAutoAssignBy,JobOrderAssignment = @JobOrderAssignment,JobOrderMaxRec = @JobOrderMaxRec,ServiceRecordMaxRec = @ServiceRecordMaxRec,ServiceContractMaxRec = @ServiceContractMaxRec,RecordPeriodJob = @RecordPeriodJob,RecordPeriodService = @RecordPeriodService,RecordPeriodContract = @RecordPeriodContract,ScheduleSVAfterUpdateContr = @ScheduleSVAfterUpdateContr,WebToZsoftStatus = @WebToZsoftStatus,UploadDayLimit = @UploadDayLimit,WebToZsoftDetStatus = @WebToZsoftDetStatus,DisableDeleteSR = @DisableDeleteSR,DisableDeleteSC = @DisableDeleteSC,SROrder = @SROrder,ShowSROnScreenLoad = @ShowSROnScreenLoad,ShowSCOnScreenLoad = @ShowSCOnScreenLoad,AutoUpdateClientDetails = @AutoUpdateClientDetails,PrinterNameJobOrder = @PrinterNameJobOrder,AutoAssignStart = @AutoAssignStart,AutoAssignEnd = @AutoAssignEnd,AutoAssignInterval = @AutoAssignInterval,AutoAssignDuration = @AutoAssignDuration,EApprovalAutoUpdateSR = @EApprovalAutoUpdateSR,chkCalTotalDurationByTimeInOut = @chkCalTotalDurationByTimeInOut,AutoEmailCheckAttempts = @AutoEmailCheckAttempts,AutoEmailAttemptsLimit = @AutoEmailAttemptsLimit,AutoUpdateClientDetailsServiceContract = @AutoUpdateClientDetailsServiceContract,ConfirmForServiceAddressEButton = @ConfirmForServiceAddressEButton,IncOurRefYourRef = @IncOurRefYourRef, SupervisorRecDays = @SupervisorRecDays, AttachServiceReport = @AttachServiceReport,AttachSupplServiceReport=@AttachSupplServiceReport, DisplayRecordsLocationwise=@DisplayRecordsLocationwise,Weekdaystart=@WeekdayStart,WeekDayEnd=@WeekdayEnd,WeekdayOTRate=@WeekdayOTRate,SatStart=@SatStart,SatEnd=@SatEnd,SatOTRate=@SatOTRate,SunStart=@SunStart,SunEnd=@SunEnd,SunOTRate=@SunOTRate,HolidayStart=@HolidayStart,HolidayEnd=@HolidayEnd,HolidayOTRate=@HolidayOTRate, BackDateContract= @BackDateContract, BackDateContractTermination= @BackDateContractTermination, ContractRevisionTerminationCode=@ContractRevisionTerminationCode, PrefixDocNoContract=@PrefixDocNoContract, PrefixDocNoService=@PrefixDocNoService, AutoRenewal=@AutoRenewal WHERE RcNo = 1;"

                command.CommandText = qry
                command.Parameters.Clear()

                'ZSOFT - WEB - TABLET SETTING

                If chkWebToZSoft.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@WebToZsoftSubmitStatus", 1)
                Else
                    command.Parameters.AddWithValue("@WebToZsoftSubmitStatus", 0)
                End If
                If chkWebToZSoft.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@WebToZsoftFileStatus", 1)
                Else
                    command.Parameters.AddWithValue("@WebToZsoftFileStatus", 0)
                End If
                If chkWebToZSoft.Items.FindByValue("3").Selected = True Then
                    command.Parameters.AddWithValue("@WebToZsoftDetStatus", 1)
                Else
                    command.Parameters.AddWithValue("@WebToZsoftDetStatus", 0)
                End If
                If chkWebToZSoft.Items.FindByValue("4").Selected = True Then
                    command.Parameters.AddWithValue("@WebToZsoftStatus", 1)
                Else
                    command.Parameters.AddWithValue("@WebToZsoftStatus", 0)
                End If
                If chkWebToZSoft.Items.FindByValue("5").Selected = True Then
                    command.Parameters.AddWithValue("@WebToZsoftGeneratePDF", 1)
                Else
                    command.Parameters.AddWithValue("@WebToZsoftGeneratePDF", 0)
                End If
                command.Parameters.AddWithValue("@UploadDayLimit", Convert.ToInt16(txtSchDaysLimit.Text))

                'QUICK SERVICE SETTING

                If chkQuickServiceSetting.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@QuickServiceFileStatus", 1)
                Else
                    command.Parameters.AddWithValue("@QuickServiceFileStatus", 0)
                End If
                If chkQuickServiceSetting.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@QuickServiceGeneratePDF", 1)
                Else
                    command.Parameters.AddWithValue("@QuickServiceGeneratePDF", 0)
                End If
                If chkQuickServiceSetting.Items.FindByValue("3").Selected = True Then
                    command.Parameters.AddWithValue("@QuickServiceSubmitStatus", 1)
                Else
                    command.Parameters.AddWithValue("@QuickServiceSubmitStatus", 0)
                End If
                command.Parameters.AddWithValue("@LastAddEditStaff", txtCreatedBy.Text)
                command.Parameters.AddWithValue("@LastAddEditDate", Convert.ToDateTime(txtCreatedOn.Text))


                'SCHEDULE AUTO ASSIGN

                command.Parameters.AddWithValue("@ScheduleAutoAssignBy", rdbScheduleAutoAssign.SelectedValue.ToString)
                command.Parameters.AddWithValue("@AllocationMethod", rdbAllocationMethod.SelectedValue.ToString)
                command.Parameters.AddWithValue("@AutoAssignStart", txtAssignStart.Text)
                command.Parameters.AddWithValue("@AutoAssignEnd", txtAssignEnd.Text)
                If String.IsNullOrEmpty(txtAssignInterval.Text) = False Then
                    command.Parameters.AddWithValue("@AutoAssignInterval", txtAssignInterval.Text)
                Else
                    command.Parameters.AddWithValue("@AutoAssignInterval", 0)

                End If
                If String.IsNullOrEmpty(txtAssignDuration.Text) = False Then
                    command.Parameters.AddWithValue("@AutoAssignDuration", txtAssignDuration.Text)
                Else
                    command.Parameters.AddWithValue("@AutoAssignDuration", 0)
                End If

                'AUTO UPDATE CLIENT DETAILS

                If chkAutoUpdateClient.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@AutoUpdateClientDetails", 1)
                Else
                    command.Parameters.AddWithValue("@AutoUpdateClientDetails", 0)
                End If
                If chkAutoUpdateClient.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@AutoUpdateClientDetailsServiceContract", 1)
                Else
                    command.Parameters.AddWithValue("@AutoUpdateClientDetailsServiceContract", 0)
                End If

                'JOB ORDER

                command.Parameters.AddWithValue("@PrinterNameJobOrder", txtDefaultPrinter.Text)


                'GENERAL SETTINGS

                command.Parameters.AddWithValue("@PastRecordNumber", txtApprovalScreenView.Text)
                command.Parameters.AddWithValue("@RecordPeriodJob", txtJobOrderRecord.Text)
                command.Parameters.AddWithValue("@RecordPeriodService", txtSvcRecRecord.Text)
                command.Parameters.AddWithValue("@RecordPeriodContract", txtSvcContractRecord.Text)
                If chkDisableDelete.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@DisableDeleteSR", 1)
                Else
                    command.Parameters.AddWithValue("@DisableDeleteSR", 0)
                End If
                If chkDisableDelete.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@DisableDeleteSC", 1)
                Else
                    command.Parameters.AddWithValue("@DisableDeleteSC", 0)
                End If

                'MAXIMUM GRID RECORDS

                command.Parameters.AddWithValue("@JobOrderMaxRec", txtJobOrderMaxRecs.Text)
                command.Parameters.AddWithValue("@ServiceRecordMaxRec", txtSvcRecMaxRecs.Text)
                command.Parameters.AddWithValue("@ServiceContractMaxRec", txtSvcContractMaxRecs.Text)

                'GRID RECORDS ON SCREEN LOAD

                If chkShowGridRecords.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@ShowSROnScreenLoad", 1)
                Else
                    command.Parameters.AddWithValue("@ShowSROnScreenLoad", 0)
                End If
                If chkShowGridRecords.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@ShowSCOnScreenLoad", 1)
                Else
                    command.Parameters.AddWithValue("@ShowSCOnScreenLoad", 0)
                End If


                'SERVICE CONTRACT

                If chkServiceContract.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@ScheduleSVAfterUpdateContr", 1)
                Else
                    command.Parameters.AddWithValue("@ScheduleSVAfterUpdateContr", 0)
                End If
                If chkServiceContract.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@ConfirmForServiceAddressEButton", 1)
                Else
                    command.Parameters.AddWithValue("@ConfirmForServiceAddressEButton", 0)
                End If
                If chkServiceContract.Items.FindByValue("3").Selected = True Then
                    command.Parameters.AddWithValue("@IncOurRefYourRef", 1)
                Else
                    command.Parameters.AddWithValue("@IncOurRefYourRef", 0)
                End If


                If chkServiceContract.Items.FindByValue("4").Selected = True Then
                    command.Parameters.AddWithValue("@TeamIDMandatory", 1)
                Else
                    command.Parameters.AddWithValue("@TeamIDMandatory", 0)
                End If
                If chkServiceContract.Items.FindByValue("5").Selected = True Then
                    command.Parameters.AddWithValue("@ServiceByMandatory", 1)
                Else
                    command.Parameters.AddWithValue("@ServiceByMandatory", 0)
                End If
                If chkServiceContract.Items.FindByValue("6").Selected = True Then
                    command.Parameters.AddWithValue("@WindowsSVCAutoServiceSchedule", 1)
                Else
                    command.Parameters.AddWithValue("@WindowsSVCAutoServiceSchedule", 0)
                End If

                If chkServiceContract.Items.FindByValue("7").Selected = True Then
                    command.Parameters.AddWithValue("@AllowTerminationBeforeLastService", 1)
                Else
                    command.Parameters.AddWithValue("@AllowTerminationBeforeLastService", 0)
                End If

                'If chkServiceContract.Items.FindByValue("4").Selected = True Then
                '    command.Parameters.AddWithValue("@BackDateContract", 1)
                'Else
                '    command.Parameters.AddWithValue("@BackDateContract", 0)
                'End If
                'If chkServiceContract.Items.FindByValue("5").Selected = True Then
                '    command.Parameters.AddWithValue("@BackDateContractTermination", 1)
                'Else
                '    command.Parameters.AddWithValue("@BackDateContractTermination", 0)
                'End If

                command.Parameters.AddWithValue("@ContractRevisionTerminationCode", ddlTerminationCode.SelectedValue.ToString)

                command.Parameters.AddWithValue("@PrefixDocNoContract", txtPrefixDocNoContract.Text.Trim.ToUpper)

                If chkAutoRenewal.Checked = True Then
                    command.Parameters.AddWithValue("@AutoRenewal", 1)
                Else
                    command.Parameters.AddWithValue("@AutoRenewal", 0)
                End If


                'command.Parameters.AddWithValue("@PriceIncreaseLimit", txtPriceIncreaseLimit.Text)
                'command.Parameters.AddWithValue("@PriceDecreaseLimit", txtPriceDecreaseLimit.Text)

                'SERVICE RECORD SETTINGS

                command.Parameters.AddWithValue("@SROrder", rdbSvcRecOrder.SelectedValue.ToString)

                If chkSvcRecord.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@EApprovalAutoUpdateSR", 1)
                Else
                    command.Parameters.AddWithValue("@EApprovalAutoUpdateSR", 0)
                End If
                If chkSvcRecord.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@NotAllowSaveIfDetStIsO", 1)
                Else
                    command.Parameters.AddWithValue("@NotAllowSaveIfDetStIsO", 0)
                End If
                If chkSvcRecord.Items.FindByValue("3").Selected = True Then
                    command.Parameters.AddWithValue("@chkCalTotalDurationByTimeInOut", 1)
                Else
                    command.Parameters.AddWithValue("@chkCalTotalDurationByTimeInOut", 0)
                End If
                If chkSvcRecord.Items.FindByValue("4").Selected = True Then
                    command.Parameters.AddWithValue("@AutoEmailCheckAttempts", 1)
                Else
                    command.Parameters.AddWithValue("@AutoEmailCheckAttempts", 0)
                End If
                If chkSvcRecord.Items.FindByValue("5").Selected = True Then
                    command.Parameters.AddWithValue("@DisplayTimeInTimeOutInServiceReport", 1)
                Else
                    command.Parameters.AddWithValue("@DisplayTimeInTimeOutInServiceReport", 0)
                End If

                If chkSvcRecord.Items.FindByValue("6").Selected = True Then
                    command.Parameters.AddWithValue("@EnableEmailValidation", 1)
                Else
                    command.Parameters.AddWithValue("@EnableEmailValidation", 0)
                End If

                If chkSvcRecord.Items.FindByValue("7").Selected = True Then
                    command.Parameters.AddWithValue("@AllowToEditBilledAmtBillNo", 1)
                Else
                    command.Parameters.AddWithValue("@AllowToEditBilledAmtBillNo", 0)
                End If

                command.Parameters.AddWithValue("@AutoEmailAttemptsLimit", txtAttempts.Text)


                'Tablet

                command.Parameters.AddWithValue("@SupervisorRecDays", txtSupervisorRec.Text)


                'Service Report

                If chkAttachServiceReport.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@AttachServiceReport", 1)
                Else
                    command.Parameters.AddWithValue("@AttachServiceReport", 0)
                End If

                If chkAttachServiceReport.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@AttachSupplServiceReport", 1)
                Else
                    command.Parameters.AddWithValue("@AttachSupplServiceReport", 0)
                End If

                If chkDisplayRecordsLocationwise.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@DisplayRecordsLocationwise", "Y")
                Else
                    command.Parameters.AddWithValue("@DisplayRecordsLocationwise", "N")
                End If

                command.Parameters.AddWithValue("@PrefixDocNoService", txtPrefixDocNoService.Text.Trim.ToUpper)

                'ZSOFT CALENDAR

                If chkCalendar.Checked = True Then
                    command.Parameters.AddWithValue("@JobOrderAssignment", 1)
                Else
                    command.Parameters.AddWithValue("@JobOrderAssignment", 0)
                End If

                If String.IsNullOrEmpty(txtWeekDayStart.Text) Then
                    command.Parameters.AddWithValue("@WeekDayStart", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@WeekDayStart", Convert.ToDateTime(txtWeekDayStart.Text).ToString("HH:mm"))
                End If
                If String.IsNullOrEmpty(txtWeekDayEnd.Text) Then
                    command.Parameters.AddWithValue("@WeekDayEnd", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@WeekDayEnd", Convert.ToDateTime(txtWeekDayEnd.Text).ToString("HH:mm"))
                End If

                If String.IsNullOrEmpty(txtWeekDayOTRate.Text) Then
                    command.Parameters.AddWithValue("@WeekDayOTRate", 0)
                Else
                    command.Parameters.AddWithValue("@WeekDayOTRate", txtWeekDayOTRate.Text)
                End If

                If String.IsNullOrEmpty(txtSatStart.Text) Then
                    command.Parameters.AddWithValue("@SatStart", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@SatStart", Convert.ToDateTime(txtSatStart.Text).ToString("HH:mm"))
                End If
                If String.IsNullOrEmpty(txtSatEnd.Text) Then
                    command.Parameters.AddWithValue("@SatEnd", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@SatEnd", Convert.ToDateTime(txtSatEnd.Text).ToString("HH:mm"))
                End If

                If String.IsNullOrEmpty(txtSatOTRate.Text) Then
                    command.Parameters.AddWithValue("@SatOTRate", 0)
                Else
                    command.Parameters.AddWithValue("@SatOTRate", txtSatOTRate.Text)
                End If

                If String.IsNullOrEmpty(txtSunStart.Text) Then
                    command.Parameters.AddWithValue("@SunStart", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@SunStart", Convert.ToDateTime(txtSunStart.Text).ToString("HH:mm"))
                End If
                If String.IsNullOrEmpty(txtSunEnd.Text) Then
                    command.Parameters.AddWithValue("@SunEnd", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@SunEnd", Convert.ToDateTime(txtSunEnd.Text).ToString("HH:mm"))
                End If

                If String.IsNullOrEmpty(txtSunOTRate.Text) Then
                    command.Parameters.AddWithValue("@SunOTRate", 0)
                Else
                    command.Parameters.AddWithValue("@SunOTRate", txtSunOTRate.Text)
                End If

                If String.IsNullOrEmpty(txtHolidayStart.Text) Then
                    command.Parameters.AddWithValue("@HolidayStart", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@HolidayStart", Convert.ToDateTime(txtHolidayStart.Text).ToString("HH:mm"))
                End If
                If String.IsNullOrEmpty(txtHolidayEnd.Text) Then
                    command.Parameters.AddWithValue("@HolidayEnd", DBNull.Value)
                Else
                    command.Parameters.AddWithValue("@HolidayEnd", Convert.ToDateTime(txtHolidayEnd.Text).ToString("HH:mm"))
                End If

                If String.IsNullOrEmpty(txtHolidayOTRate.Text) Then
                    command.Parameters.AddWithValue("@HolidayOTRate", 0)
                Else
                    command.Parameters.AddWithValue("@HolidayOTRate", txtHolidayOTRate.Text)
                End If

                If chkEnableSaveUploadLater.Checked = True Then
                    command.Parameters.AddWithValue("@EnableMobileAppSaveUploadLater", 1)
                Else
                    command.Parameters.AddWithValue("@EnableMobileAppSaveUploadLater", 0)
                End If

                If chkEnableAdhocReport.Checked = True Then
                    command.Parameters.AddWithValue("@EnableAdhocServiceReport", 1)
                Else
                    command.Parameters.AddWithValue("@EnableAdhocServiceReport", 0)
                End If

                If chkContinuousContract.Checked = True Then
                    command.Parameters.AddWithValue("@ContinuousContract", 1)
                Else
                    command.Parameters.AddWithValue("@ContinuousContract", 0)
                End If

                If ddlMobileAppServiceDateMethod.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@MobileAppServiceDateMethod", "")
                Else
                    command.Parameters.AddWithValue("@MobileAppServiceDateMethod", ddlMobileAppServiceDateMethod.SelectedValue.ToString)
                End If
                If chkSMSServices.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@EnableSMSResidential", 1)
                Else
                    command.Parameters.AddWithValue("@EnableSMSResidential", 0)
                End If
                If chkSMSServices.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@EnableSMSCorporate", 1)
                Else
                    command.Parameters.AddWithValue("@EnableSMSCorporate", 0)
                End If

                If String.IsNullOrEmpty(txtSMSDays.Text) = True Then
                    command.Parameters.AddWithValue("@SMSReminderDays", "")
                Else
                    command.Parameters.AddWithValue("@SMSReminderDays", txtSMSDays.Text)
                End If

                If chkSendEmailNotificationForRenewal.Checked = True Then
                    command.Parameters.AddWithValue("@EmailNotificationForContractRenewal", 1)
                Else
                    command.Parameters.AddWithValue("@EmailNotificationForContractRenewal", 0)
                End If


                If chkSendEmailNotificationForExpiredContracts.Checked = True Then
                    command.Parameters.AddWithValue("@EmailNotificationForExpiredContract", 1)
                Else
                    command.Parameters.AddWithValue("@EmailNotificationForExpiredContract", 0)
                End If

                If String.IsNullOrEmpty(txtEmailAddressForEmailNotificationOfContractRenewal.Text) = True Then
                    command.Parameters.AddWithValue("@EmailAddressForEmailNotificationOfContractRenewal", "")
                Else
                    command.Parameters.AddWithValue("@EmailAddressForEmailNotificationOfContractRenewal", txtEmailAddressForEmailNotificationOfContractRenewal.Text)
                End If

                If String.IsNullOrEmpty(txtEmailAddressForEmailNotificationForExpiredContracts.Text) = True Then
                    command.Parameters.AddWithValue("@EmailAddressForEmailNotificationForExpiredContracts", "")
                Else
                    command.Parameters.AddWithValue("@EmailAddressForEmailNotificationForExpiredContracts", txtEmailAddressForEmailNotificationForExpiredContracts.Text)
                End If

                If String.IsNullOrEmpty(txtDaysBeforeExpiry.Text) = True Then
                    command.Parameters.AddWithValue("@NotificationDaysBeforeExpiry", 0)
                Else
                    command.Parameters.AddWithValue("@NotificationDaysBeforeExpiry", txtDaysBeforeExpiry.Text)
                End If

                If String.IsNullOrEmpty(txtEmailAddressforContractsNoSchedule.Text) = True Then
                    command.Parameters.AddWithValue("@EmailAddressForContractsNoSchedule", "")
                Else
                    command.Parameters.AddWithValue("@EmailAddressForContractsNoSchedule", txtEmailAddressforContractsNoSchedule.Text)
                End If

                If chkEmailNotificationforContractNoSchedule.Checked = True Then
                    command.Parameters.AddWithValue("@EmailNotificationforContractsNoSchedule", 1)
                Else
                    command.Parameters.AddWithValue("@EmailNotificationforContractsNoSchedule", 0)
                End If

                If String.IsNullOrEmpty(txtEmailAddressForContractStatusChange.Text) = True Then
                    command.Parameters.AddWithValue("@EmailAddressForContractStatusChange", "")
                Else
                    command.Parameters.AddWithValue("@EmailAddressForContractStatusChange", txtEmailAddressForContractStatusChange.Text)
                End If

                If chkEmailNotificationForContractStatusChange.Checked = True Then
                    command.Parameters.AddWithValue("@EmailNotificationforContractStatusChange", 1)
                Else
                    command.Parameters.AddWithValue("@EmailNotificationforContractStatusChange", 0)
                End If

                If chkEmailNotificationForContractStatusSalesman.Checked = True Then
                    command.Parameters.AddWithValue("@EmailNotificationforContractStatusSalesman", 1)
                Else
                    command.Parameters.AddWithValue("@EmailNotificationforContractStatusSalesman", 0)
                End If
                'qry = qry + " ContractRevisionNewContractCode =@ContractRevisionNewContractCode, `ContractRenewalNewContractCode =@ContractRenewalNewContractCode, ContractRenewalTerminationCode=@ContractRenewalTerminationCode "


                'If (ddlContractGroupForEmailNotificationOfContractRenewal.SelectedIndex > 0) Then
                '    command.Parameters.AddWithValue("@ContractGroupForEmailNotificationOfContractRenewal", 0)
                'Else
                '    command.Parameters.AddWithValue("@ContractGroupForEmailNotificationOfContractRenewal", txtHolidayOTRate.Text)
                'End If


                Dim lContractGroup As String
                lContractGroup = ""

                For Each item As ListItem In ddlContractGroupForEmailNotificationOfContractRenewal.Items

                    If item.Selected Then
                        lContractGroup += item.Value + ","
                    End If
                Next

                'lContractGroup.Remove(lContractGroup.IndexOf(","))
                'lContractGroup.Remove(lContractGroup.Length - 1)

                If String.IsNullOrEmpty(lContractGroup) = False Then
                    lContractGroup = lContractGroup.Remove(lContractGroup.Length - 1)
                End If

                If String.IsNullOrEmpty(lContractGroup) = True Then
                    command.Parameters.AddWithValue("@ContractGroupForEmailNotificationOfContractRenewal", "")
                Else
                    command.Parameters.AddWithValue("@ContractGroupForEmailNotificationOfContractRenewal", lContractGroup)
                End If

                If String.IsNullOrEmpty(txtPrefixDocNoCustomerRequest.Text) = True Then
                    command.Parameters.AddWithValue("@PrefixDocNoCustomerRequest", "")
                Else
                    command.Parameters.AddWithValue("@PrefixDocNoCustomerRequest", txtPrefixDocNoCustomerRequest.Text.ToUpper)
                End If


                If ddlContractRenewalNewContractCode.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractRenewalNewContractCode", "")
                Else
                    command.Parameters.AddWithValue("@ContractRenewalNewContractCode", ddlContractRenewalNewContractCode.SelectedValue.ToString)
                End If

                If ddlContractRenewalTerminationCode.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractRenewalTerminationCode", "")
                Else
                    command.Parameters.AddWithValue("@ContractRenewalTerminationCode", ddlContractRenewalTerminationCode.SelectedValue.ToString)
                End If

                If ddlContractVoidCode.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractVoidCode", "")
                Else
                    command.Parameters.AddWithValue("@ContractVoidCode", ddlContractVoidCode.SelectedValue.ToString)
                End If


                If ddlContractRevisionNewContractCode.SelectedIndex = 0 Then
                    command.Parameters.AddWithValue("@ContractRevisionNewContractCode", "")
                Else
                    command.Parameters.AddWithValue("@ContractRevisionNewContractCode", ddlContractRevisionNewContractCode.SelectedValue.ToString)
                End If

                If chkLoadContractAddButton.Checked = True Then
                    command.Parameters.AddWithValue("@EnableAddButtonInContractOnLoad", "Y")
                Else
                    command.Parameters.AddWithValue("@EnableAddButtonInContractOnLoad", "N")
                End If

                '28.09.21

                If String.IsNullOrEmpty(txtDaysBeforeRenewalMessage.Text) Then
                    command.Parameters.AddWithValue("@DaysBeforeRenewalMessage", 0)
                Else
                    command.Parameters.AddWithValue("@DaysBeforeRenewalMessage", txtDaysBeforeRenewalMessage.Text)
                End If


                If String.IsNullOrEmpty(txtDaysBeforeScheduleGeneration.Text) Then
                    command.Parameters.AddWithValue("@DaysBeforeScheduleGeneration", 0)
                Else
                    command.Parameters.AddWithValue("@DaysBeforeScheduleGeneration", txtDaysBeforeScheduleGeneration.Text)
                End If


                If String.IsNullOrEmpty(txtEmailNotiifcationsScheduleChange.Text) = True Then
                    command.Parameters.AddWithValue("@EmailsforNotificationofScheduleChange", "")
                Else
                    command.Parameters.AddWithValue("@EmailsforNotificationofScheduleChange", txtEmailNotiifcationsScheduleChange.Text)
                End If
                '28.09.21

                'If chkDisplayFixedDurationType.Checked = True Then
                '    command.Parameters.AddWithValue("@DisplayFixedDurationType", "Y")
                'Else
                '    command.Parameters.AddWithValue("@DisplayFixedDurationType", "N")
                'End If

                'If chkDisplayContinuousDurationType.Checked = True Then
                '    command.Parameters.AddWithValue("@DisplayContinuousDurationType", "Y")
                'Else
                '    command.Parameters.AddWithValue("@DisplayContinuousDurationType", "N")
                'End If

                '                  command.Parameters.AddWithValue("@ContractGrpRequired", 1)
                '                command.Parameters.AddWithValue("@AllowedToCloseUnbillJob", 1)
                '                command.Parameters.AddWithValue("@RFScheduleType", 1)
                '               command.Parameters.AddWithValue("@CloseJobOrderOnApprovalCompleteScrRecord", 1)
                '                command.Parameters.AddWithValue("@SalesManRequired", 1)
                '                 command.Parameters.AddWithValue("@ServiceReqContractNo", 1)
                '                 command.Parameters.AddWithValue("@ServiceReqServiceIDDet", 1)
                '                command.Parameters.AddWithValue("@ExcludeToBillAnd0Amount", 1)
                '                 command.Parameters.AddWithValue("@WeekDayStart", 1)
                '                command.Parameters.AddWithValue("@WeekDayEnd", 1)
                '                command.Parameters.AddWithValue("@SatStart", 1)
                '                command.Parameters.AddWithValue("@SatEnd", 1)
                '                command.Parameters.AddWithValue("@SunStart", 1)
                '                command.Parameters.AddWithValue("@SunEnd", 1)
                '                command.Parameters.AddWithValue("@HolidayStart", 1)
                '                command.Parameters.AddWithValue("@HolidayEnd", 1)
                '                command.Parameters.AddWithValue("@WeekDayOTRate", 1)
                '                command.Parameters.AddWithValue("@SatOTRate", 1)
                '                command.Parameters.AddWithValue("@SunOTRate", 1)
                '                command.Parameters.AddWithValue("@HolidayOTRate", 1)
                '                command.Parameters.AddWithValue("@TabletFirstRecordDuration", 1)
                '                command.Parameters.AddWithValue("@CompanyNo", 1)
                '                command.Parameters.AddWithValue("@ServerNo", 1)
                '                command.Parameters.AddWithValue("@NotAllowSaveContractLessThan0", 1)
                'command.Parameters.AddWithValue("@CommissionDays

                command.Connection = conn

                command.ExecuteNonQuery()

                ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""

            End If

            conn.Close()
            EnableControls()
            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SVCMASTER", "MASTERSETUP", "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

        End Try


    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub chkSMSServices_SelectedIndexChanged(sender As Object, e As EventArgs) Handles chkSMSServices.SelectedIndexChanged

    End Sub

    'Protected Sub txtPriceIncreaseLimit_TextChanged(sender As Object, e As EventArgs) Handles txtPriceIncreaseLimit.TextChanged
    '    Try
    '        Dim d As Double

    '        If String.IsNullOrEmpty(txtPriceIncreaseLimit.Text) = True Then
    '            lblAlert.Text = "PRICE INCREASE LIMIT CANNOT BE BLANK"
    '            txtPriceIncreaseLimit.Text = "0.00"
    '            Exit Sub
    '        End If


    '        If Double.TryParse(txtPriceIncreaseLimit.Text, d) = False Then
    '            lblAlert.Text = "PRICE INCREASE LIMIT IS INVALID"
    '            txtPriceIncreaseLimit.Text = "0.00"
    '            'Return False
    '            Exit Sub
    '        End If

    '        If Convert.ToDecimal(txtPriceIncreaseLimit.Text) < 0.0 Then
    '            lblAlert.Text = "PRICE INCREASE LIMIT SHOULD BE POSITIVE"
    '            txtPriceIncreaseLimit.Text = "0.00"
    '            'Return False
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString

    '    End Try
    'End Sub

    'Protected Sub txtPriceDecreaseLimit_TextChanged(sender As Object, e As EventArgs) Handles txtPriceDecreaseLimit.TextChanged
    '    Try
    '        lblAlert.Text = ""

    '        Dim d As Double

    '        If String.IsNullOrEmpty(txtPriceDecreaseLimit.Text) = True Then
    '            lblAlert.Text = "PRICE DECREASE LIMIT CANNOT BE BLANK"
    '            txtPriceDecreaseLimit.Text = "0.00"
    '            Exit Sub
    '        End If


    '        If Double.TryParse(txtPriceDecreaseLimit.Text, d) = False Then
    '            lblAlert.Text = "PRICE DECREASE LIMIT IS INVALID"
    '            txtPriceDecreaseLimit.Text = "0.00"
    '            'Return False
    '            Exit Sub
    '        End If

    '        If Convert.ToDecimal(txtPriceDecreaseLimit.Text) > 0.0 Then
    '            lblAlert.Text = "PRICE DECREASE LIMIT SHOULD BE NEGATIVE"
    '            txtPriceDecreaseLimit.Text = "0.00"
    '            'Return False
    '            Exit Sub
    '        End If
    '    Catch ex As Exception
    '        lblAlert.Text = ex.Message.ToString

    '    End Try
    'End Sub
End Class
