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
                command.CommandText = "SELECT x0177,  x0177Add, x0177Edit, X0177Delete FROM tblGroupAccess where GroupAccess = '" & Session("SecGroupAuthority") & "'"

                command.Connection = conn

                Dim dr As MySqlDataReader = command.ExecuteReader()
                Dim dt As New DataTable
                dt.Load(dr)
                conn.Close()

                If dt.Rows.Count > 0 Then
                    If Not IsDBNull(dt.Rows(0)("x0177")) Then
                        If String.IsNullOrEmpty(Convert.ToBoolean(dt.Rows(0)("x0177"))) = False Then
                            If Convert.ToBoolean(dt.Rows(0)("x0177")) = False Then
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
                    If Not IsDBNull(dt.Rows(0)("x0177Edit")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("x0177Edit")) = False Then
                            Me.btnEdit.Enabled = dt.Rows(0)("x0177Edit").ToString()
                        End If
                    End If

                    If Not IsDBNull(dt.Rows(0)("X0177Delete")) Then
                        If String.IsNullOrEmpty(dt.Rows(0)("X0177Delete")) = False Then
                            Me.btnDelete.Enabled = dt.Rows(0)("X0177Delete").ToString()
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
            lblAlert.Text = ex.Message
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
        txtInvoiceMaxRecs.Enabled = False
        txtReceiptMaxRecs.Enabled = False
        txtCreditNoteMaxRecs.Enabled = False
        txtJournalMaxRecs.Enabled = False
        chkShowGridRecords.Enabled = False

        chkServiceContract.Enabled = False

        chkARModulePost.Enabled = False
        rdbSvcRecOrder.Enabled = False
        chkSvcRecord.Enabled = False
        txtAttempts.Enabled = False
        chkCalendar.Enabled = False

        txtSupervisorRec.Enabled = False
        txtInvoiceEmailInterval.Enabled = False
        chkAutoEmailModules.Enabled = False
        ddlInvoiceBatchEmailFormat.Enabled = False
        chkAREMailSvcRpt.Enabled = False

        txtEmailAddressForReceiptListing.Enabled = False
        txtEmailAddressForSalesInvoiceListing.Enabled = False

        txtSOAEmailInterval.Enabled = False
        txtSOASchedule.Enabled = False
        txtSOAScheduleEnd.Enabled = False

        txtAutoEmailStartTime.Enabled = False
        txtAutoEmailEndTime.Enabled = False
        rdbInvoiceSvcReportFile.Enabled = False

        chkDocumentEditing.Enabled = False
        ddlCurrency.Enabled = False

        chkSOAWithCreditBalance.Enabled = False

        txtGST.Enabled = False
        chkARModuleApplyTax.Enabled = False

        chkBatchInvoiceOption.Enabled = False
        rdbEInvoicing.Enabled = False
        txtStartDate.Enabled = False

        ddlCategory.Enabled = False
        rdbCategoryBusiness.Enabled = False
        txtTIN.Enabled = False
        txtBRN.Enabled = False
        txtNewBRN.Enabled = False

        txtSSTNo.Enabled = False

        ddlMSICCode.Enabled = False
        txtMSICDesc.Enabled = False

        ddlEInvClassificationCode.Enabled = False
        txtEInvClassificationDesc.Enabled = False
        ddlConsolidatedClassificationCode.Enabled = False
        txtConsolidatedClassificationDesc.Enabled = False

        txtCancellationTimeLimit.Enabled = False
        txtEInvoiceEmailRecipients.Enabled = False

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
        txtInvoiceMaxRecs.Enabled = True
        txtReceiptMaxRecs.Enabled = True
        txtCreditNoteMaxRecs.Enabled = True
        txtJournalMaxRecs.Enabled = True
        chkShowGridRecords.Enabled = True

        chkServiceContract.Enabled = True
        chkARModulePost.Enabled = True
        rdbSvcRecOrder.Enabled = True
        chkSvcRecord.Enabled = True
        txtAttempts.Enabled = True
        chkCalendar.Enabled = True

        txtSupervisorRec.Enabled = True
        txtInvoiceEmailInterval.Enabled = True
        chkAutoEmailModules.Enabled = True
        ddlInvoiceBatchEmailFormat.Enabled = True
        chkAREMailSvcRpt.Enabled = True

        txtEmailAddressForReceiptListing.Enabled = True
        txtEmailAddressForSalesInvoiceListing.Enabled = True

        txtSOAEmailInterval.Enabled = True
        txtSOASchedule.Enabled = True
        txtSOAScheduleEnd.Enabled = True

        txtAutoEmailStartTime.Enabled = True
        txtAutoEmailEndTime.Enabled = True

        rdbInvoiceSvcReportFile.Enabled = True

        chkDocumentEditing.Enabled = True
        ddlCurrency.Enabled = True

        chkSOAWithCreditBalance.Enabled = True

        txtGST.Enabled = True

        chkARModuleApplyTax.Enabled = True

        chkBatchInvoiceOption.Enabled = True
        rdbEInvoicing.Enabled = True
        txtStartDate.Enabled = True

        ddlCategory.Enabled = True
        rdbCategoryBusiness.Enabled = True
        txtTIN.Enabled = True
        txtBRN.Enabled = True
        txtNewBRN.Enabled = True

        txtSSTNo.Enabled = True

        ddlMSICCode.Enabled = True
        txtMSICDesc.Enabled = True

        ddlEInvClassificationCode.Enabled = True
        txtEInvClassificationDesc.Enabled = True
        ddlConsolidatedClassificationCode.Enabled = True
        txtConsolidatedClassificationDesc.Enabled = True

        txtBusinessDesc.Enabled = True

        txtCancellationTimeLimit.Enabled = True
        txtEInvoiceEmailRecipients.Enabled = True
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EnableControls()

            Dim query As String

            Query = "SELECT Currency FROM tblCurrency order by Currency"
            PopulateDropDownList(query, "Currency", "Currency", ddlCurrency)

            query = "Select TaxType from tbltaxtype  order by taxtype"
            PopulateDropDownList(query, "TaxType", "TaxType", txtGST)

            query = "SELECT IndustrialClassificationCode,Description FROM tbleinvoicemalaysiaSICode where rcno<>0"
            PopulateDropDownList(query, "IndustrialClassificationCode", "Description", ddlMSICCode)

            query = "SELECT ClassificationCode,Description FROM tbleinvoicemalaysiaclassificationid where rcno<>0"
            PopulateDropDownList(query, "ClassificationCode", "Description", ddlEInvClassificationCode)

            PopulateDropDownList(query, "ClassificationCode", "Description", ddlConsolidatedClassificationCode)

            RetrieveData()
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

        End If
        txtCreatedOn.Attributes.Add("readonly", "readonly")

    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
        Try

            Dim con As MySqlConnection = New MySqlConnection()

            con.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            'Using con As New MySqlConnection(constr)
            Using cmd As New MySqlCommand(query)
                cmd.CommandType = CommandType.Text
                cmd.Connection = con
                con.Open()
                ddl.DataSource = cmd.ExecuteReader()
                ddl.DataTextField = textField.Trim()
                ddl.DataValueField = valueField.Trim()
                ddl.DataBind()
                con.Close()
                con.Dispose()
            End Using
            'End Using
        Catch ex As Exception
            InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "PopulateDropDownList", ex.Message.ToString, query + " , " + TryCast(ddl, DropDownList).Text + " , " + " , " + textField + " , " + valueField)
        End Try
    End Sub

    Public Sub InsertIntoTblWebEventLog(LoginID As String, events As String, errorMsg As String, ID As String)
        Try
            Dim conn As New MySqlConnection()
            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString

            Dim insCmds As New MySqlCommand()
            insCmds.CommandType = CommandType.Text

            Dim insQuery As String = "Insert into tblWebEventLog(LoginId, Event, Error,ID, CreatedOn)"
            insQuery += " values(@LoginId,@Event,@Error,@ID,@CreatedOn);"

            insCmds.CommandText = insQuery

            insCmds.Parameters.Clear()
            insCmds.Parameters.AddWithValue("@LoginId", LoginID)
            insCmds.Parameters.AddWithValue("@Event", events)
            insCmds.Parameters.AddWithValue("@Error", errorMsg)
            insCmds.Parameters.AddWithValue("@ID", ID)
            insCmds.Parameters.AddWithValue("@CreatedOn", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss", New System.Globalization.CultureInfo("en-GB")))

            conn.Open()
            insCmds.Connection = conn
            insCmds.ExecuteNonQuery()
            conn.Close()
            conn.Dispose()
            insCmds.Dispose()

            lblAlert.Text = errorMsg

        Catch ex As Exception
            InsertIntoTblWebEventLog("ARMODULESETUP" + txtCreatedBy.Text, "InsertIntoTblWebEventLog", ex.Message.ToString, "ERROR")
        End Try
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

            'MAXIMUM GRID RECORDS

            txtInvoiceMaxRecs.Text = dt.Rows(0)("InvoiceRecordMaxRec").ToString
            txtReceiptMaxRecs.Text = dt.Rows(0)("ReceiptRecordMaxRec").ToString
            txtCreditNoteMaxRecs.Text = dt.Rows(0)("CNRecordMaxRec").ToString
            txtJournalMaxRecs.Text = dt.Rows(0)("JournalRecordMaxRec").ToString
            txtInvoiceEmailInterval.Text = dt.Rows(0)("InvoiceEmailInterval").ToString
            txtSOAEmailInterval.Text = dt.Rows(0)("SOAEmailInterval").ToString
            txtSOASchedule.Text = dt.Rows(0)("SOAScheduleDate").ToString
            txtSOAScheduleEnd.Text = dt.Rows(0)("SOAScheduleLastDate").ToString
            txtAutoEmailStartTime.Text = dt.Rows(0)("AutoEmailSchStartTime").ToString
            txtAutoEmailEndTime.Text = dt.Rows(0)("AutoEmailSchEndTime").ToString

            txtEmailAddressForSalesInvoiceListing.Text = dt.Rows(0)("EmailforSalesInvoiceListing").ToString
            txtEmailAddressForReceiptListing.Text = dt.Rows(0)("EmailforReceiptListing").ToString

            ddlCurrency.Text = dt.Rows(0)("ARCurrency").ToString
            txtGST.Text = dt.Rows(0)("DefaultTaxCode").ToString

            'If txtGST.SelectedIndex = 0 Then
            '    Command.Parameters.AddWithValue("@DefaultTaxCode", DBNull.Value)
            'Else
            '    Command.Parameters.AddWithValue("@DefaultTaxCode", ddlCurrency.Text)
            'End If
            'GRID RECORDS ON SCREEN LOAD

            If dt.Rows(0)("ShowInvoiceOnScreenLoad").ToString = "1" Then
                chkShowGridRecords.Items.FindByValue("1").Selected = True
            Else
                chkShowGridRecords.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("ShowReceiptOnScreenLoad").ToString = "1" Then
                chkShowGridRecords.Items.FindByValue("2").Selected = True
            Else
                chkShowGridRecords.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("ShowCNOnScreenLoad").ToString = "1" Then
                chkShowGridRecords.Items.FindByValue("3").Selected = True
            Else
                chkShowGridRecords.Items.FindByValue("3").Selected = False
            End If
            If dt.Rows(0)("ShowJournalOnScreenLoad").ToString = "1" Then
                chkShowGridRecords.Items.FindByValue("4").Selected = True
            Else
                chkShowGridRecords.Items.FindByValue("4").Selected = False
            End If


            'POST RECORDS

            If dt.Rows(0)("PostInvoice").ToString = "1" Then
                chkARModulePost.Items.FindByValue("1").Selected = True
            Else
                chkARModulePost.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("PostReceipt").ToString = "1" Then
                chkARModulePost.Items.FindByValue("2").Selected = True
            Else
                chkARModulePost.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("PostCN").ToString = "1" Then
                chkARModulePost.Items.FindByValue("3").Selected = True
            Else
                chkARModulePost.Items.FindByValue("3").Selected = False
            End If
            If dt.Rows(0)("PostJournal").ToString = "1" Then
                chkARModulePost.Items.FindByValue("4").Selected = True
            Else
                chkARModulePost.Items.FindByValue("4").Selected = False
            End If





          
          


            'AUTO EMAIL

            If dt.Rows(0)("AutoEmailInvoice").ToString = "1" Then
                chkAutoEmailModules.Items.FindByValue("1").Selected = True
            Else
                chkAutoEmailModules.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("AutoEmailDebitNote").ToString = "1" Then
                chkAutoEmailModules.Items.FindByValue("2").Selected = True
            Else
                chkAutoEmailModules.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("AutoEmailCreditNote").ToString = "1" Then
                chkAutoEmailModules.Items.FindByValue("3").Selected = True
            Else
                chkAutoEmailModules.Items.FindByValue("3").Selected = False
            End If
            If dt.Rows(0)("AutoEmailReceipt").ToString = "1" Then
                chkAutoEmailModules.Items.FindByValue("4").Selected = True
            Else
                chkAutoEmailModules.Items.FindByValue("4").Selected = False
            End If

            'Creator

            If dt.Rows(0)("InvoiceOnlyEditableByCreator").ToString = "1" Then
                chkDocumentEditing.Items.FindByValue("1").Selected = True
            Else
                chkDocumentEditing.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("CreditNoteOnlyEditableByCreator").ToString = "1" Then
                chkDocumentEditing.Items.FindByValue("2").Selected = True
            Else
                chkDocumentEditing.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("DebitNoteOnlyEditableByCreator").ToString = "1" Then
                chkDocumentEditing.Items.FindByValue("3").Selected = True
            Else
                chkDocumentEditing.Items.FindByValue("3").Selected = False
            End If

            If dt.Rows(0)("ReceiptOnlyEditableByCreator").ToString = "1" Then
                chkDocumentEditing.Items.FindByValue("4").Selected = True
            Else
                chkDocumentEditing.Items.FindByValue("4").Selected = False
            End If

            If dt.Rows(0)("JournalOnlyEditableByCreator").ToString = "1" Then
                chkDocumentEditing.Items.FindByValue("5").Selected = True
            Else
                chkDocumentEditing.Items.FindByValue("5").Selected = False
            End If

            If dt.Rows(0)("InvoiceSvcReportIndividualFile").ToString = "1" Then
                rdbInvoiceSvcReportFile.Items.FindByValue("1").Selected = True
            Else
                rdbInvoiceSvcReportFile.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("InvoiceSvcReportMergedFile").ToString = "1" Then
                rdbInvoiceSvcReportFile.Items.FindByValue("2").Selected = True
            Else
                rdbInvoiceSvcReportFile.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("IncludeServiceReportsInAREmail").ToString = "1" Then
                chkAREMailSvcRpt.Checked = True
            Else
                chkAREMailSvcRpt.Checked = False
            End If

            If dt.Rows(0)("InvoiceBatchEmailFormat").ToString = "-1" Then
          
            Else
                ddlInvoiceBatchEmailFormat.Text = dt.Rows(0)("InvoiceBatchEmailFormat").ToString

            End If


            If dt.Rows(0)("SOAWithCreditBal").ToString = "1" Then
                chkSOAWithCreditBalance.Checked = True
            Else
                chkSOAWithCreditBalance.Checked = False
            End If


            If dt.Rows(0)("CorporateApplyTax").ToString = "1" Then
                chkARModuleApplyTax.Items.FindByValue("1").Selected = True
            Else
                chkARModuleApplyTax.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("ResidentialApplyTax").ToString = "1" Then
                chkARModuleApplyTax.Items.FindByValue("2").Selected = True
            Else
                chkARModuleApplyTax.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("BatchInvoiceCompanyGroupOptional").ToString = "1" Then
                chkBatchInvoiceOption.Items.FindByValue("1").Selected = True
            Else
                chkBatchInvoiceOption.Items.FindByValue("1").Selected = False
            End If


            '06.12.24

            If dt.Rows(0)("DisabledEInvoicing").ToString = "1" Then
                rdbEInvoicing.Items.FindByValue("1").Selected = True
            Else
                rdbEInvoicing.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("MalaysiaEinvoicing").ToString = "1" Then
                rdbEInvoicing.Items.FindByValue("2").Selected = True
            Else
                rdbEInvoicing.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("EInvoiceStartDate").ToString = DBNull.Value.ToString Then
            Else
                txtStartDate.Text = Convert.ToDateTime(dt.Rows(0)("EInvoiceStartDate")).ToString("dd/MM/yyyy")
            End If

            If dt.Rows(0)("Category").ToString = "-1" Then

            Else
                ddlCategory.Text = dt.Rows(0)("Category").ToString

            End If
            If dt.Rows(0)("BusinessType").ToString = "Local" Then
                rdbCategoryBusiness.Items.FindByValue("1").Selected = True
            ElseIf dt.Rows(0)("BusinessType").ToString = "Foreign" Then
                rdbCategoryBusiness.Items.FindByValue("2").Selected = True
           
            End If

            If dt.Rows(0)("TIN").ToString <> DBNull.Value.ToString Then
                txtTIN.Text = dt.Rows(0)("TIN").ToString
            End If

            If dt.Rows(0)("BusinessRegistrationNumber").ToString <> DBNull.Value.ToString Then
                txtBRN.Text = dt.Rows(0)("BusinessRegistrationNumber").ToString
            End If

            If dt.Rows(0)("NewBusinessRegNo").ToString <> DBNull.Value.ToString Then
                txtNewBRN.Text = dt.Rows(0)("NewBusinessRegNo").ToString
            End If

            If dt.Rows(0)("SSTRegistrationNumber").ToString <> DBNull.Value.ToString Then
                txtSSTNo.Text = dt.Rows(0)("SSTRegistrationNumber").ToString
            End If

            If dt.Rows(0)("MSICCode").ToString = "-1" Then

            Else
                ddlMSICCode.SelectedItem.Text = dt.Rows(0)("MSICCode").ToString
                txtMSICCode.Text = dt.Rows(0)("MSICCode").ToString
            End If

            If dt.Rows(0)("MSICDescription").ToString <> DBNull.Value.ToString Then
                txtMSICDesc.Text = dt.Rows(0)("MSICDescription").ToString
            End If

            If dt.Rows(0)("EInvClassificationCode").ToString = "-1" Then

            Else
                ddlEInvClassificationCode.SelectedItem.Text = dt.Rows(0)("EInvClassificationCode").ToString
                txtEInvClassificationCode.Text = dt.Rows(0)("EInvClassificationCode").ToString
            End If

            If dt.Rows(0)("EInvClassificationDesc").ToString <> DBNull.Value.ToString Then
                txtEInvClassificationDesc.Text = dt.Rows(0)("EInvClassificationDesc").ToString
            End If

            If dt.Rows(0)("ConsolidatedInvClassificationCode").ToString = "-1" Then

            Else
                ddlConsolidatedClassificationCode.SelectedItem.Text = dt.Rows(0)("ConsolidatedInvClassificationCode").ToString
                txtConsolidatedClassificationCode.Text = dt.Rows(0)("ConsolidatedInvClassificationCode").ToString
            End If

            If dt.Rows(0)("ConsolidatedInvClassificationDesc").ToString <> DBNull.Value.ToString Then
                txtConsolidatedClassificationDesc.Text = dt.Rows(0)("ConsolidatedInvClassificationDesc").ToString
            End If

            If dt.Rows(0)("BusinessActivityDescription").ToString <> DBNull.Value.ToString Then
                txtBusinessDesc.Text = dt.Rows(0)("BusinessActivityDescription").ToString
            End If

            txtEInvoiceEmailRecipients.Text = dt.Rows(0)("EInvoiceEmailRecipients").ToString
            txtCancellationTimeLimit.Text = dt.Rows(0)("CanCellationTimeLimit").ToString
        
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

    'Protected Sub ddlMSICCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMSICCode.SelectedIndexChanged
    '    If ddlMSICCode.Text = "-1" Then

    '        Dim command As MySqlCommand = New MySqlCommand

    '        Dim conn As MySqlConnection = New MySqlConnection()
    '        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        conn.Open()

    '        command.CommandType = CommandType.Text

    '        command.CommandText = "SELECT IndustrialClassificationCode,Description FROM tbleinvoicemalaysiaSICode where IndustrialClassificationCode=@CODE"
    '        command.parameters.AddwithValue("@Code", ddlMSICCode.Text)

    '        command.Connection = conn

    '        Dim dr As MySqlDataReader = command.ExecuteReader()
    '        Dim dt As New System.Data.DataTable
    '        dt.Load(dr)

    '        If dt.Rows.Count > 0 Then
    '            txtMSICDesc.Text = dt.rows(0)("Description").ToString
    '        End If

    '        command.Dispose()
    '        dt.Dispose()
    '        dr.Close()

    '        conn.Close()
    '        conn.Dispose()

    '    End If
    'End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        'Try
        Dim i As Integer = 0
        If Integer.TryParse(txtSOASchedule.Text, i) Then
            If i >= 1 And i <= 30 Then

            Else
                lblAlert.Text = "Enter SOA Schedule date in numbers between 1 and 30"
                Return

            End If
        Else
            lblAlert.Text = "Enter SOA Schedule date in numbers between 1 and 30"
            Return
        End If
        If Integer.TryParse(txtSOAScheduleEnd.Text, i) Then
            If i >= 1 And i <= 30 Then

            Else
                lblAlert.Text = "Enter SOA Schedule End date in numbers between 1 and 30"
                Return

            End If
        Else
            lblAlert.Text = "Enter SOA Schedule End date in numbers between 1 and 30"
            Return
        End If

        If rdbEInvoicing.Items.FindByValue("2").Selected = True Then
            If String.IsNullOrEmpty(txtStartDate.Text) Then
                lblAlert.Text = "Enter EInvoice Start Date if EInvoicing is enabled"
                Return
            End If
        End If
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
            qry = "UPDATE tblservicerecordmastersetup SET InvoiceRecordMaxRec = @InvoiceRecordMaxRec, ReceiptRecordMaxRec = @ReceiptRecordMaxRec, CNRecordMaxRec = @CNRecordMaxRec, JournalRecordMaxRec = @JournalRecordMaxRec, ShowInvoiceOnScreenLoad = @ShowInvoiceOnScreenLoad, ShowReceiptOnScreenLoad = @ShowReceiptOnScreenLoad, ShowCNOnScreenLoad = @ShowCNOnScreenLoad, ShowJournalOnScreenLoad = @ShowJournalOnScreenLoad, PostInvoice = @PostInvoice, PostReceipt = @PostReceipt, PostCN = @PostCN, PostJournal = @PostJournal, InvoiceEmailInterval=@InvoiceEmailInterval, AutoEmailInvoice=@AutoEmailInvoice, AutoEmailDebitNote=@AutoEmailDebitNote, AutoEmailCreditNote=@AutoEmailCreditNote,AutoEmailReceipt=@AutoEmailReceipt, InvoiceOnlyEditableByCreator=@InvoiceOnlyEditableByCreator, CreditNoteOnlyEditableByCreator=@CreditNoteOnlyEditableByCreator, DebitNoteOnlyEditableByCreator=@DebitNoteOnlyEditableByCreator, ReceiptOnlyEditableByCreator=@ReceiptOnlyEditableByCreator, JournalOnlyEditableByCreator=@JournalOnlyEditableByCreator, ARCurrency=@ARCurrency, DefaultTaxCode=@DefaultTaxCode,SOAEmailInterval=@SOAEmailInterval,SOAScheduleDate=@SOAScheduleDate,SOAScheduleLastDate=@SOAScheduleLastDate,AutoEmailSchStartTime=@AutoEmailSchStartTime,AutoEmailSchEndTime=@AutoEmailSchEndTime,InvoiceSvcReportIndividualFile=@InvoiceSvcReportIndividualFile,InvoiceSvcReportMergedFile=@InvoiceSvcReportMergedFile,IncludeServiceReportsInAREmail=@IncludeServiceReportsInAREmail,InvoiceBatchEmailFormat=@InvoiceBatchEmailFormat,SOAWithCreditBal=@SOAWithCreditBal, CorporateApplyTax=@CorporateApplyTax,ResidentialApplyTax=@ResidentialApplyTax,EmailforSalesInvoiceListing=@EmailforSalesInvoiceListing,EmailforReceiptListing=@EmailforReceiptListing, BatchInvoiceCompanyGroupOptional =@BatchInvoiceCompanyGroupOptional, DisabledEInvoicing =@DisabledEInvoicing, MalaysiaEinvoicing =@MalaysiaEinvoicing,EInvoiceStartDate=@EInvoiceStartDate,Category=@Category,BusinessType=@BusinessType,TIN=@TIN,BusinessRegistrationNumber=@BusinessRegistrationNo,SSTRegistrationNumber=SSTRegistrationNumber,MSICCode=@MSICCode,MSICDescription=@MSICDescription,BusinessActivityDescription=@BusinessActivityDescription,NewBusinessRegNo=@NewBusinessRegNo,EInvoiceEmailRecipients=@EInvoiceEmailRecipients,CancellationTimeLimit=@CancellationTimeLimit,EInvClassificationCode=@EInvClassificationCode,EInvClassificationDesc=@EInvClassificationDesc,ConsolidatedInvClassificationCode=@ConsolidatedInvClassificationCode,ConsolidatedInvClassificationDesc=@ConsolidatedInvClassificationDesc WHERE RcNo = 1;"

            command.CommandText = qry
            command.Parameters.Clear()

            command.Parameters.AddWithValue("@InvoiceRecordMaxRec", txtInvoiceMaxRecs.Text)
            command.Parameters.AddWithValue("@ReceiptRecordMaxRec", txtReceiptMaxRecs.Text)
            command.Parameters.AddWithValue("@CNRecordMaxRec", txtCreditNoteMaxRecs.Text)
            command.Parameters.AddWithValue("@JournalRecordMaxRec", txtJournalMaxRecs.Text)
            command.Parameters.AddWithValue("@InvoiceEmailInterval", txtInvoiceEmailInterval.Text)
            command.Parameters.AddWithValue("@SOAEmailInterval", txtSOAEmailInterval.Text)
            command.Parameters.AddWithValue("@SOAScheduleDate", txtSOASchedule.Text)
            command.Parameters.AddWithValue("@SOAScheduleLastDate", txtSOAScheduleEnd.Text)
            command.Parameters.AddWithValue("@AutoEmailSchStartTime", txtAutoEmailStartTime.Text)
            command.Parameters.AddWithValue("@AutoEmailSchEndTime", txtAutoEmailEndTime.Text)
            command.Parameters.AddWithValue("@EmailforSalesInvoiceListing", txtEmailAddressForSalesInvoiceListing.Text)
            command.Parameters.AddWithValue("@EmailforReceiptListing", txtEmailAddressForReceiptListing.Text)

            'GRID RECORDS ON SCREEN LOAD

            If chkShowGridRecords.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@ShowInvoiceOnScreenLoad", 1)
            Else
                command.Parameters.AddWithValue("@ShowInvoiceOnScreenLoad", 0)
            End If
            If chkShowGridRecords.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@ShowReceiptOnScreenLoad", 1)
            Else
                command.Parameters.AddWithValue("@ShowReceiptOnScreenLoad", 0)
            End If

            If chkShowGridRecords.Items.FindByValue("3").Selected = True Then
                command.Parameters.AddWithValue("@ShowCNOnScreenLoad", 1)
            Else
                command.Parameters.AddWithValue("@ShowCNOnScreenLoad", 0)
            End If

            If chkShowGridRecords.Items.FindByValue("4").Selected = True Then
                command.Parameters.AddWithValue("@ShowJournalOnScreenLoad", 1)
            Else
                command.Parameters.AddWithValue("@ShowJournalOnScreenLoad", 0)
            End If


            'POST OPTIONS

            If chkARModulePost.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@PostInvoice", 1)
            Else
                command.Parameters.AddWithValue("@PostInvoice", 0)
            End If
            If chkARModulePost.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@PostReceipt", 1)
            Else
                command.Parameters.AddWithValue("@PostReceipt", 0)
            End If

            If chkARModulePost.Items.FindByValue("3").Selected = True Then
                command.Parameters.AddWithValue("@PostCN", 1)
            Else
                command.Parameters.AddWithValue("@PostCN", 0)
            End If

            If chkARModulePost.Items.FindByValue("4").Selected = True Then
                command.Parameters.AddWithValue("@PostJournal", 1)
            Else
                command.Parameters.AddWithValue("@PostJournal", 0)
            End If


            'AUTO EMAIL

            If chkAutoEmailModules.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@AutoEmailInvoice", 1)
            Else
                command.Parameters.AddWithValue("@AutoEmailInvoice", 0)
            End If

            If chkAutoEmailModules.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@AutoEmailDebitNote", 1)
            Else
                command.Parameters.AddWithValue("@AutoEmailDebitNote", 0)
            End If

            If chkAutoEmailModules.Items.FindByValue("3").Selected = True Then
                command.Parameters.AddWithValue("@AutoEmailCreditNote", 1)
            Else
                command.Parameters.AddWithValue("@AutoEmailCreditNote", 0)
            End If

            If chkAutoEmailModules.Items.FindByValue("4").Selected = True Then
                command.Parameters.AddWithValue("@AutoEmailReceipt", 1)
            Else
                command.Parameters.AddWithValue("@AutoEmailReceipt", 0)
            End If



            'DOCUMENT EDITING

            If chkDocumentEditing.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@InvoiceOnlyEditableByCreator", 1)
            Else
                command.Parameters.AddWithValue("@InvoiceOnlyEditableByCreator", 0)
            End If
            If chkDocumentEditing.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@CreditNoteOnlyEditableByCreator", 1)
            Else
                command.Parameters.AddWithValue("@CreditNoteOnlyEditableByCreator", 0)
            End If

            If chkDocumentEditing.Items.FindByValue("3").Selected = True Then
                command.Parameters.AddWithValue("@DebitNoteOnlyEditableByCreator", 1)
            Else
                command.Parameters.AddWithValue("@DebitNoteOnlyEditableByCreator", 0)
            End If

            If chkDocumentEditing.Items.FindByValue("4").Selected = True Then
                command.Parameters.AddWithValue("@ReceiptOnlyEditableByCreator", 1)
            Else
                command.Parameters.AddWithValue("@ReceiptOnlyEditableByCreator", 0)
            End If

            If chkDocumentEditing.Items.FindByValue("5").Selected = True Then
                command.Parameters.AddWithValue("@JournalOnlyEditableByCreator", 1)
            Else
                command.Parameters.AddWithValue("@JournalOnlyEditableByCreator", 0)
            End If


            If ddlCurrency.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@ArCurrency", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@ArCurrency", ddlCurrency.Text)
            End If

            If txtGST.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@DefaultTaxCode", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DefaultTaxCode", txtGST.Text)
            End If

            If rdbInvoiceSvcReportFile.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@InvoiceSvcReportIndividualFile", 1)
            Else
                command.Parameters.AddWithValue("@InvoiceSvcReportIndividualFile", 0)
            End If

            If rdbInvoiceSvcReportFile.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@InvoiceSvcReportMergedFile", 1)
            Else
                command.Parameters.AddWithValue("@InvoiceSvcReportMergedFile", 0)
            End If

            If chkAREMailSvcRpt.Checked = True Then
                command.Parameters.AddWithValue("@IncludeServiceReportsInAREmail", 1)
            Else
                command.Parameters.AddWithValue("@IncludeServiceReportsInAREmail", 0)
            End If
            If ddlInvoiceBatchEmailFormat.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@InvoiceBatchEmailFormat", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@InvoiceBatchEmailFormat", ddlInvoiceBatchEmailFormat.Text)
            End If


            If chkSOAWithCreditBalance.Checked = True Then
                command.Parameters.AddWithValue("@SOAWithCreditBal", 1)
            Else
                command.Parameters.AddWithValue("@SOAWithCreditBal", 0)
            End If



            If chkARModuleApplyTax.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@CorporateApplyTax", 1)
            Else
                command.Parameters.AddWithValue("@CorporateApplyTax", 0)
            End If

            If chkARModuleApplyTax.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@ResidentialApplyTax", 1)
            Else
                command.Parameters.AddWithValue("@ResidentialApplyTax", 0)
            End If

            If chkBatchInvoiceOption.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@BatchInvoiceCompanyGroupOptional", 1)
            Else
                command.Parameters.AddWithValue("@BatchInvoiceCompanyGroupOptional", 0)
            End If


            '06.12.24

            If rdbEInvoicing.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@DisabledEInvoicing", 1)
                txtStartDate.Text = ""
            Else
                command.Parameters.AddWithValue("@DisabledEInvoicing", 0)
            End If

            If rdbEInvoicing.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@MalaysiaEinvoicing", 1)
                command.Parameters.AddWithValue("@EInvoiceStartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))

            Else
                command.Parameters.AddWithValue("@MalaysiaEinvoicing", 0)
                txtStartDate.Text = ""
                command.Parameters.AddWithValue("@EInvoiceStartDate", DBNull.Value)
            End If


            'If String.IsNullOrEmpty(txtStartDate.Text) Then
            '    command.Parameters.AddWithValue("@EInvoiceStartDate", DBNull.Value)
            'Else
            '    command.Parameters.AddWithValue("@EInvoiceStartDate", Convert.ToDateTime(txtStartDate.Text).ToString("yyyy-MM-dd"))

            'End If

            '06.12.24

            If ddlCategory.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@Category", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Category", ddlCategory.Text)
            End If

            If rdbCategoryBusiness.Items.FindByValue("1").Selected = True Then
                command.Parameters.AddWithValue("@BusinessType", "Local")

            ElseIf rdbCategoryBusiness.Items.FindByValue("2").Selected = True Then
                command.Parameters.AddWithValue("@BusinessType", "Foreign")
            End If

            command.Parameters.AddWithValue("@TIN", txtTIN.Text)
            command.Parameters.AddWithValue("@BusinessRegistrationNo", txtBRN.Text)
            command.Parameters.AddWithValue("@NewBusinessRegNo", txtNewBRN.Text)
            command.Parameters.AddWithValue("@SSTRegistrationNumber", txtSSTNo.Text)
            InsertIntoTblWebEventLog("ARModuleSetup", "btnSave", ddlMSICCode.SelectedIndex.ToString, txtMSICCode.Text)

            If ddlMSICCode.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@MSICCode", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@MSICCode", ddlMSICCode.SelectedItem.Text)
            End If
            command.Parameters.AddWithValue("@MSICDescription", txtMSICDesc.Text)

            'If ddlEInvClassificationCode.SelectedIndex = 0 Then
            '    command.Parameters.AddWithValue("@EInvClassificationCode", DBNull.Value)
            'Else
            '    command.Parameters.AddWithValue("@EInvClassificationCode", ddlEInvClassificationCode.SelectedItem.Text)
            'End If
            command.Parameters.AddWithValue("@EInvClassificationCode", txtEInvClassificationCode.Text)
            command.Parameters.AddWithValue("@EInvClassificationDesc", txtEInvClassificationDesc.Text)

            'If ddlConsolidatedClassificationCode.SelectedIndex = 0 Then
            '    command.Parameters.AddWithValue("@ConsolidatedInvClassificationCode", DBNull.Value)
            'Else
            '    command.Parameters.AddWithValue("@ConsolidatedInvClassificationCode", ddlConsolidatedClassificationCode.SelectedItem.Text)
            'End If

            command.Parameters.AddWithValue("@ConsolidatedInvClassificationCode", txtConsolidatedClassificationCode.Text)
            command.Parameters.AddWithValue("@ConsolidatedInvClassificationDesc", txtConsolidatedClassificationDesc.Text)

            command.Parameters.AddWithValue("@BusinessActivityDescription", txtBusinessDesc.Text)

            command.Parameters.AddWithValue("@EInvoiceEmailRecipients", txtEInvoiceEmailRecipients.Text)
            command.Parameters.AddWithValue("@CancellationTimeLimit", txtCancellationTimeLimit.Text)


            command.Connection = conn

            command.ExecuteNonQuery()

            ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
            lblAlert.Text = ""

        End If

        conn.Close()

        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "SVCMASTER", "MASTERSETUP", "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

        'Catch ex As Exception
        '    lblAlert.Text = ex.Message.ToString

        'End Try
        EnableControls()

    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")
    End Sub

    Protected Sub ddlMSICCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlMSICCode.SelectedIndexChanged
        txtMSICDesc.Text = ddlMSICCode.SelectedValue.ToString
        txtMSICCode.Text = ddlMSICCode.SelectedItem.Text
    End Sub

    Protected Sub ddlEInvClassificationCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlEInvClassificationCode.SelectedIndexChanged
        txtEInvClassificationDesc.Text = ddlEInvClassificationCode.SelectedValue.ToString
        txtEInvClassificationCode.Text = ddlEInvClassificationCode.SelectedItem.Text
    End Sub

    Protected Sub ddlConsolidatedClassificationCode_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlConsolidatedClassificationCode.SelectedIndexChanged
        txtConsolidatedClassificationDesc.Text = ddlConsolidatedClassificationCode.SelectedValue.ToString
        txtConsolidatedClassificationCode.Text = ddlConsolidatedClassificationCode.SelectedItem.Text
    End Sub

End Class
