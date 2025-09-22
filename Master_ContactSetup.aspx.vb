Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_ContactSetup
    Inherits System.Web.UI.Page

    Public rcno As String

    Public Sub MakeMeNull()
        lblMessage.Text = ""
        lblAlert.Text = ""
        txtMode.Text = ""

        chkCompDefaultAutoEmail.Checked = False
        txtCompSvcRecReportFormat.Text = ""
        chkCompAddedCompany.Checked = False
        chkCompNameEAlphabets.Checked = False
        chkCompRocNos.Checked = False
        txtCompAPTerm.Text = ""
        txtCompARTerm.SelectedIndex = 0

        chkPersonDefaultAutoEmail.Checked = False
        txtPersonSvcRecReportFormat.Text = ""
        chkPersonNameEAlphabets.Checked = False
        chkPersonNRIC.Checked = False
        txtPersonARTerm.SelectedIndex = 0
        txtPersonAPTerm.Text = ""
        chkDefaultAutoEmailInvoiceCompany.Checked = True
        chkDefaultAutoEmailInvoicePerson.Checked = True
        chkDefaultAutoEmailSOACompany.Checked = True
        chkDefaultAutoEmailSOAPerson.Checked = True
        chkDefaultHardCopyInvoiceCompany.Checked = True
        chkDefaultHardCopyInvoicePerson.Checked = True
        chkDefaultHardCopySOACompany.Checked = True
        chkDefaultHardCopySOAPerson.Checked = True

        chkPostalValidateCompany.Checked = True
        chkPostalValidatePerson.Checked = True
        txtPostalFormatCompany.Text = ""
        txtPostalFormatPerson.Text = ""

        txtRcno.Text = ""
        'txtCreatedBy.Text = ""
        txtCreatedOn.Text = ""
        ' txtLastModifiedBy.Text = ""
        txtLastModifiedOn.Text = ""
        ddlPersCurrency.SelectedIndex = 0
        ddlCompCurrency.SelectedIndex = 0

        ddlCompIndustry.SelectedIndex = 0
        ddlPersIndustry.SelectedIndex = 0

        txtCompMarketSegmentID.Text = ""
        txtPersMarketSegmentID.Text = ""

    End Sub

    Private Sub EnableControls()
        btnSave.Enabled = False
        btnSave.ForeColor = System.Drawing.Color.Gray
        btnCancel.Enabled = False
        btnCancel.ForeColor = System.Drawing.Color.Gray

        btnEdit.Enabled = True
        btnEdit.ForeColor = System.Drawing.Color.Black

        btnQuit.Enabled = True
        btnQuit.ForeColor = System.Drawing.Color.Black

        chkCompDefaultAutoEmail.Enabled = False
        txtCompSvcRecReportFormat.Enabled = False
        chkCompAddedCompany.Enabled = False
        chkCompNameEAlphabets.Enabled = False
        chkCompRocNos.Enabled = False
        txtCompAPTerm.Enabled = False
        txtCompARTerm.Enabled = False

        chkPersonDefaultAutoEmail.Enabled = False
        txtPersonSvcRecReportFormat.Enabled = False
        chkPersonNameEAlphabets.Enabled = False
        chkPersonNRIC.Enabled = False
        txtPersonAPTerm.Enabled = False
        txtPersonARTerm.Enabled = False
        ddlPersCurrency.Enabled = False
        ddlCompCurrency.Enabled = False

        ddlCompIndustry.Enabled = False
        ddlPersIndustry.Enabled = False

        txtCompMarketSegmentID.Enabled = False
        txtPersMarketSegmentID.Enabled = False

        chkDefaultAutoEmailInvoiceCompany.Enabled = False
        chkDefaultAutoEmailInvoicePerson.Enabled = False
        chkDefaultAutoEmailSOACompany.Enabled = False
        chkDefaultAutoEmailSOAPerson.Enabled = False
        chkDefaultHardCopyInvoiceCompany.Enabled = False
        chkDefaultHardCopyInvoicePerson.Enabled = False
        chkDefaultHardCopySOACompany.Enabled = False
        chkDefaultHardCopySOAPerson.Enabled = False

        chkPostalValidateCompany.Enabled = False
        chkPostalValidatePerson.Enabled = False

        txtPostalFormatCompany.Enabled = False
        txtPostalFormatPerson.Enabled = False

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

        chkCompDefaultAutoEmail.Enabled = True
        txtCompSvcRecReportFormat.Enabled = True
        chkCompAddedCompany.Enabled = True
        chkCompNameEAlphabets.Enabled = True
        chkCompRocNos.Enabled = True
        txtCompAPTerm.Enabled = True
        txtCompARTerm.Enabled = True

        chkPersonDefaultAutoEmail.Enabled = True
        txtPersonSvcRecReportFormat.Enabled = True
        chkPersonNameEAlphabets.Enabled = True
        chkPersonNRIC.Enabled = True
        txtPersonAPTerm.Enabled = True
        txtPersonARTerm.Enabled = True

        ddlPersCurrency.Enabled = True
        ddlCompCurrency.Enabled = True

        ddlCompIndustry.Enabled = True
        ddlPersIndustry.Enabled = True

        chkDefaultAutoEmailInvoiceCompany.Enabled = True
        chkDefaultAutoEmailInvoicePerson.Enabled = True
        chkDefaultAutoEmailSOACompany.Enabled = True
        chkDefaultAutoEmailSOAPerson.Enabled = True
        chkDefaultHardCopyInvoiceCompany.Enabled = True
        chkDefaultHardCopyInvoicePerson.Enabled = True
        chkDefaultHardCopySOACompany.Enabled = True
        chkDefaultHardCopySOAPerson.Enabled = True

        chkPostalValidateCompany.Enabled = True
        chkPostalValidatePerson.Enabled = True
        txtPostalFormatCompany.Enabled = True
        txtPostalFormatPerson.Enabled = True
    End Sub

    Public Sub PopulateDropDownList(ByVal query As String, ByVal textField As String, ByVal valueField As String, ByVal ddl As DropDownList)
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
        End Using
        'End Using
    End Sub
    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        txtCreatedOn.Attributes.Add("readonly", "readonly")
        If Not IsPostBack Then
            Dim UserID As String = Convert.ToString(Session("UserID"))
            txtCreatedBy.Text = UserID
            txtLastModifiedBy.Text = UserID

            Dim Query As String
            Query = "Select Terms, TermsDay from tblTerms order by Termsday,Terms"
            PopulateDropDownList(Query, "Terms", "Terms", txtCompARTerm)
            PopulateDropDownList(Query, "Terms", "Terms", txtPersonARTerm)


            Query = "Select Currency from tblCurrency order by Currency"
            PopulateDropDownList(Query, "Currency", "Currency", ddlCompCurrency)
            PopulateDropDownList(Query, "Currency", "Currency", ddlPersCurrency)



            Query = "Select Industry from tblIndustry order by Industry"
            PopulateDropDownList(Query, "Industry", "Industry", ddlCompIndustry)
            PopulateDropDownList(Query, "Industry", "Industry", ddlPersIndustry)

            Dim conn As MySqlConnection = New MySqlConnection()

            conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            conn.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text

            command1.CommandText = "SELECT * FROM tblcontactsetup where rcno <> 0"
            command1.Connection = conn


            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New DataTable
            dt.Load(dr)

            If dt.Rows.Count > 0 Then
                If dt.Rows(0)("CompDefAutoEmailServ").ToString = "1" Then
                    chkCompDefaultAutoEmail.Checked = True
                Else
                    chkCompDefaultAutoEmail.Checked = False
                End If
                txtCompSvcRecReportFormat.Text = dt.Rows(0)("CompDefRptFormatServ").ToString
                If dt.Rows(0)("NewAddedCompanyWithCompanyDefaultLedger").ToString = "1" Then
                    chkCompAddedCompany.Checked = True
                Else
                    chkCompAddedCompany.Checked = False
                End If
                If dt.Rows(0)("CompNameEwithAlphabets").ToString = "1" Then
                    chkCompAddedCompany.Checked = True
                Else
                    chkCompAddedCompany.Checked = False
                End If
                If dt.Rows(0)("CompRocNosBlank").ToString = "1" Then
                    chkCompRocNos.Checked = True
                Else
                    chkCompRocNos.Checked = False
                End If

                txtCompAPTerm.Text = dt.Rows(0)("CompAPTerms").ToString
                txtCompARTerm.Text = dt.Rows(0)("CompARTerms").ToString

                txtCompMarketSegmentID.Text = dt.Rows(0)("CompMarketSegmentID").ToString
                txtPersMarketSegmentID.Text = dt.Rows(0)("PersMarketSegmentID").ToString


                If dt.Rows(0)("PersDefAutoEmailServ").ToString = "1" Then
                    chkPersonDefaultAutoEmail.Checked = True
                Else
                    chkPersonDefaultAutoEmail.Checked = False
                End If
                txtPersonSvcRecReportFormat.Text = dt.Rows(0)("PersDefRptFormatServ").ToString

                If dt.Rows(0)("PersNameEwithAlphabets").ToString = "1" Then
                    chkPersonNameEAlphabets.Checked = True
                Else
                    chkPersonNameEAlphabets.Checked = False
                End If
                If dt.Rows(0)("PersNRICBlank").ToString = "1" Then
                    chkPersonNRIC.Checked = True
                Else
                    chkPersonNRIC.Checked = False
                End If

                '''''''''''''
                If dt.Rows(0)("CompCurrency").ToString = "" Then
                    ddlCompCurrency.SelectedIndex = 0
                Else
                    ddlCompCurrency.Text = dt.Rows(0)("CompCurrency").ToString
                End If


                If dt.Rows(0)("PersCurrency").ToString = "" Then
                    ddlPersCurrency.SelectedIndex = 0
                Else
                    ddlPersCurrency.Text = dt.Rows(0)("PersCurrency").ToString
                End If

                ''''''''''''


                If dt.Rows(0)("CompIndustry").ToString = "" Then
                    ddlCompIndustry.SelectedIndex = 0
                Else
                    ddlCompIndustry.Text = dt.Rows(0)("CompIndustry").ToString
                End If


                If dt.Rows(0)("PersIndustry").ToString = "" Then
                    ddlPersIndustry.SelectedIndex = 0
                Else
                    ddlPersIndustry.Text = dt.Rows(0)("PersIndustry").ToString
                End If




                ''''''''''''''''''''''''


                txtPersonAPTerm.Text = dt.Rows(0)("PersAPTerms").ToString
                txtPersonARTerm.Text = dt.Rows(0)("PersARTerms").ToString

                txtRcno.Text = dt.Rows(0)("RcNo").ToString


                If dt.Rows(0)("CompDefaultAutoEmailInvoice").ToString = "1" Then
                    chkDefaultAutoEmailInvoiceCompany.Checked = True
                Else
                    chkDefaultAutoEmailInvoiceCompany.Checked = False
                End If
                If dt.Rows(0)("CompDefaultAutoEmailSOA").ToString = "1" Then
                    chkDefaultAutoEmailSOACompany.Checked = True
                Else
                    chkDefaultAutoEmailSOACompany.Checked = False
                End If
                If dt.Rows(0)("CompHardCopyInvoice").ToString = "1" Then
                    chkDefaultHardCopyInvoiceCompany.Checked = True
                Else
                    chkDefaultHardCopyInvoiceCompany.Checked = False
                End If
                If dt.Rows(0)("CompHardCopySOA").ToString = "1" Then
                    chkDefaultHardCopySOACompany.Checked = True
                Else
                    chkDefaultHardCopySOACompany.Checked = False
                End If

                If dt.Rows(0)("PersDefaultAutoEmailInvoice").ToString = "1" Then
                    chkDefaultAutoEmailInvoicePerson.Checked = True
                Else
                    chkDefaultAutoEmailInvoicePerson.Checked = False
                End If
                If dt.Rows(0)("PersDefaultAutoEmailSOA").ToString = "1" Then
                    chkDefaultAutoEmailSOAPerson.Checked = True
                Else
                    chkDefaultAutoEmailSOAPerson.Checked = False
                End If
                If dt.Rows(0)("PersHardCopyInvoice").ToString = "1" Then
                    chkDefaultHardCopyInvoicePerson.Checked = True
                Else
                    chkDefaultHardCopyInvoicePerson.Checked = False
                End If
                If dt.Rows(0)("PersHardCopySOA").ToString = "1" Then
                    chkDefaultHardCopySOAPerson.Checked = True
                Else
                    chkDefaultHardCopySOAPerson.Checked = False
                End If

                If dt.Rows(0)("PostalValidate").ToString = "1" Then
                    chkPostalValidateCompany.Checked = True
                Else
                    chkPostalValidateCompany.Checked = False
                End If
                If dt.Rows(0)("PersPostalValidate").ToString = "1" Then
                    chkPostalValidatePerson.Checked = True
                Else
                    chkPostalValidatePerson.Checked = False
                End If
                txtPostalFormatCompany.Text = dt.Rows(0)("CompPostalFormat").ToString
                txtPostalFormatPerson.Text = dt.Rows(0)("PersPostalFormat").ToString

            Else

                Dim command As MySqlCommand = New MySqlCommand

                command.CommandType = CommandType.Text
                Dim qry As String

                qry = "INSERT INTO tblcontactsetup() VALUES();"


                command.CommandText = qry
                command.Parameters.Clear()


                command.Connection = conn

                command.ExecuteNonQuery()
            End If

            conn.Close()

            EnableControls()

        End If

    End Sub

    Protected Sub btnEdit_Click(sender As Object, e As EventArgs) Handles btnEdit.Click

        DisableControls()
        txtMode.Text = "Edit"
        lblMessage.Text = "ACTION: EDIT RECORD"

    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        'Me.ClientScript.RegisterClientScriptBlock(Me.[GetType](), "Close", "window.close()", True)
        Response.Redirect("Home.aspx")

    End Sub

    Protected Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        MakeMeNull()
        EnableControls()

    End Sub

    Protected Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click

        '     Try
        Dim conn As MySqlConnection = New MySqlConnection()

        conn.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
        conn.Open()


        Dim command1 As MySqlCommand = New MySqlCommand

        command1.CommandType = CommandType.Text

        command1.CommandText = "SELECT * FROM tblcontactsetup where rcno=" & Convert.ToInt32(txtRcno.Text)
        command1.Connection = conn

        Dim dr As MySqlDataReader = command1.ExecuteReader()
        Dim dt As New DataTable
        dt.Load(dr)

        If dt.Rows.Count > 0 Then

            Dim command As MySqlCommand = New MySqlCommand

            command.CommandType = CommandType.Text
            Dim qry As String

            qry = "UPDATE tblcontactsetup SET CompCurrency = @CompCurrency, PersCurrency = @PersCurrency, CompDefAutoEmailServ = @CompDefAutoEmailServ,CompDefRptFormatServ = @CompDefRptFormatServ,NewAddedCompanyWithCompanyDefaultLedger = @NewAddedCompanyWithCompanyDefaultLedger,CompNameEwithAlphabets = @CompNameEwithAlphabets,CompRocNosBlank = @CompRocNosBlank,CompAPTerms = @CompAPTerms,CompARTerms = @CompARTerms,PersDefAutoEmailServ = @PersDefAutoEmailServ,PersDefRptFormatServ = @PersDefRptFormatServ,PersNameEwithAlphabets = @PersNameEwithAlphabets,PersNRICBlank = @PersNRICBlank,PersAPTerms = @PersAPTerms,PersARTerms = @PersARTerms, CompIndustry = @CompIndustry, PersIndustry = @PersIndustry,  CompMarketSegmentID = @CompMarketSegmentID, PersMarketSegmentID = @PersMarketSegmentID, CompDefaultAutoEmailInvoice=@CompDefaultAutoEmailInvoice, PersDefaultAutoEmailInvoice=@PersDefaultAutoEmailInvoice,CompDefaultAutoEmailSOA=@CompDefaultAutoEmailSOA,PersDefaultAutoEmailSOA=@PersDefaultAutoEmailSOA,CompHardCopyInvoice=@CompHardCopyInvoice,PersHardCopyInvoice=@PersHardCopyInvoice,CompHardCopySOA=@CompHardCopySOA,PersHardCopySOA=@PersHardCopySOA,PostalValidate=@PostalValidate,PersPostalValidate=@PersPostalValidate,CompPostalFormat=@CompPostalFormat,PersPostalFormat=@PersPostalFormat,LastModifiedBy = @LastModifiedBy,LastModifiedOn = @LastModifiedOn WHERE RcNo =" & Convert.ToInt32(txtRcno.Text)


            command.CommandText = qry
            command.Parameters.Clear()
            If chkCompDefaultAutoEmail.Checked = True Then

                command.Parameters.AddWithValue("@CompDefAutoEmailServ", 1)
            Else

                command.Parameters.AddWithValue("@CompDefAutoEmailServ", 0)
            End If
            command.Parameters.AddWithValue("@CompDefRptFormatServ", txtCompSvcRecReportFormat.Text)
            If chkCompAddedCompany.Checked = True Then

                command.Parameters.AddWithValue("@NewAddedCompanyWithCompanyDefaultLedger", 1)
            Else

                command.Parameters.AddWithValue("@NewAddedCompanyWithCompanyDefaultLedger", 0)
            End If
            If chkCompNameEAlphabets.Checked = True Then

                command.Parameters.AddWithValue("@CompNameEwithAlphabets", 1)
            Else

                command.Parameters.AddWithValue("@CompNameEwithAlphabets", 0)
            End If
            If chkCompRocNos.Checked = True Then

                command.Parameters.AddWithValue("@CompRocNosBlank", 1)
            Else

                command.Parameters.AddWithValue("@CompRocNosBlank", 0)
            End If
            command.Parameters.AddWithValue("@CompAPTerms", txtCompAPTerm.Text)
            command.Parameters.AddWithValue("@CompARTerms", txtCompARTerm.Text)

            If chkPersonDefaultAutoEmail.Checked = True Then

                command.Parameters.AddWithValue("@PersDefAutoEmailServ", 1)
            Else

                command.Parameters.AddWithValue("@PersDefAutoEmailServ", 0)
            End If
            command.Parameters.AddWithValue("@PersDefRptFormatServ", txtPersonSvcRecReportFormat.Text)

            If chkPersonNameEAlphabets.Checked = True Then

                command.Parameters.AddWithValue("@PersNameEwithAlphabets", 1)
            Else

                command.Parameters.AddWithValue("@PersNameEwithAlphabets", 0)
            End If
            If chkPersonNRIC.Checked = True Then

                command.Parameters.AddWithValue("@PersNRICBlank", 1)
            Else

                command.Parameters.AddWithValue("@PersNRICBlank", 0)
            End If

            command.Parameters.AddWithValue("@PersAPTerms", txtPersonAPTerm.Text)
            command.Parameters.AddWithValue("@PersARTerms", txtPersonARTerm.Text)

            If ddlCompCurrency.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@CompCurrency", "")
            Else
                command.Parameters.AddWithValue("@CompCurrency", ddlCompCurrency.Text)
            End If

            If ddlPersCurrency.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@PersCurrency", "")
            Else
                command.Parameters.AddWithValue("@PersCurrency", ddlPersCurrency.Text)
            End If



            If ddlCompIndustry.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@CompIndustry", "")
            Else
                command.Parameters.AddWithValue("@CompIndustry", ddlCompIndustry.Text)
            End If

            If ddlPersIndustry.SelectedIndex = 0 Then
                command.Parameters.AddWithValue("@PersIndustry", "")
            Else
                command.Parameters.AddWithValue("@PersIndustry", ddlPersIndustry.Text)
            End If


            If chkDefaultAutoEmailInvoiceCompany.Checked = True Then
                command.Parameters.AddWithValue("@CompDefaultAutoEmailInvoice", 1)
            Else
                command.Parameters.AddWithValue("@CompDefaultAutoEmailInvoice", 0)
            End If

            If chkDefaultAutoEmailInvoicePerson.Checked = True Then
                command.Parameters.AddWithValue("@PersDefaultAutoEmailInvoice", 1)
            Else
                command.Parameters.AddWithValue("@PersDefaultAutoEmailInvoice", 0)
            End If


            If chkDefaultAutoEmailSOACompany.Checked = True Then
                command.Parameters.AddWithValue("@CompDefaultAutoEmailSOA", 1)
            Else
                command.Parameters.AddWithValue("@CompDefaultAutoEmailSOA", 0)
            End If

            If chkDefaultAutoEmailSOAPerson.Checked = True Then
                command.Parameters.AddWithValue("@PersDefaultAutoEmailSOA", 1)
            Else
                command.Parameters.AddWithValue("@PersDefaultAutoEmailSOA", 0)
            End If


            If chkDefaultHardCopyInvoiceCompany.Checked = True Then
                command.Parameters.AddWithValue("@CompHardCopyInvoice", 1)
            Else
                command.Parameters.AddWithValue("@CompHardCopyInvoice", 0)
            End If

            If chkDefaultHardCopyInvoicePerson.Checked = True Then
                command.Parameters.AddWithValue("@PersHardCopyInvoice", 1)
            Else
                command.Parameters.AddWithValue("@PersHardCopyInvoice", 0)
            End If


            If chkDefaultHardCopySOACompany.Checked = True Then
                command.Parameters.AddWithValue("@CompHardCopySOA", 1)
            Else
                command.Parameters.AddWithValue("@CompHardCopySOA", 0)
            End If


            If chkDefaultHardCopySOAPerson.Checked = True Then
                command.Parameters.AddWithValue("@PersHardCopySOA", 1)
            Else
                command.Parameters.AddWithValue("@PersHardCopySOA", 0)
            End If

            If chkPostalValidateCompany.Checked = True Then
                command.Parameters.AddWithValue("@PostalValidate", 1)
            Else
                command.Parameters.AddWithValue("@PostalValidate", 0)
            End If
            If chkPostalValidatePerson.Checked = True Then
                command.Parameters.AddWithValue("@PersPostalValidate", 1)
            Else
                command.Parameters.AddWithValue("@PersPostalValidate", 0)
            End If
            command.Parameters.AddWithValue("@CompPostalFormat", txtPostalFormatCompany.Text)
            command.Parameters.AddWithValue("@PersPostalFormat", txtPostalFormatPerson.Text)


            command.Parameters.AddWithValue("@CompMarketSegmentID", txtCompMarketSegmentID.Text)
            command.Parameters.AddWithValue("@PersMarketSegmentID", txtPersMarketSegmentID.Text)

            command.Parameters.AddWithValue("@LastModifiedBy", txtLastModifiedBy.Text.ToUpper)
            command.Parameters.AddWithValue("@LastModifiedOn", Convert.ToDateTime(txtCreatedOn.Text))

            command.Connection = conn

            command.ExecuteNonQuery()

            lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"

            'If txtExists.Text = "True" Then
            '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!! Record is in use, so City cannot be updated!!!", "str")
            '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED."
            '    lblAlert.Text = ""
            'Else
            '    ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
            '    lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
            '    lblAlert.Text = ""
            'End If
        End If

        conn.Close()

        txtMode.Text = ""


        CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "CONTACTSETUP", rcno, "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

        'Catch ex As Exception
        '    lblAlert.Text = ex.Message
        '    'MessageBox.Message.Alert(Page, ex.ToString, "str")
        'End Try
        EnableControls()
        txtMode.Text = ""

    End Sub

    'Protected Sub FindMarketSegmentID()
    '    Try


    '        Dim connBillingDetails As MySqlConnection = New MySqlConnection()

    '        connBillingDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
    '        connBillingDetails.Open()

    '        Dim command1 As MySqlCommand = New MySqlCommand

    '        command1.CommandType = CommandType.Text
    '        command1.CommandText = "SELECT marketsegmentid FROM tblindustry where industry= """ & ddlIndustrysvc.Text & """"
    '        command1.Connection = connBillingDetails

    '        Dim dr As MySqlDataReader = command1.ExecuteReader()
    '        Dim dt As New System.Data.DataTable
    '        dt.Load(dr)


    '        If dt.Rows.Count > 0 Then
    '            txtMarketSegmentIDsvc.Text = dt.Rows(0)("marketsegmentid").ToString
    '        End If
    '        connBillingDetails.Close()
    '        connBillingDetails.Dispose()
    '    Catch ex As Exception
    '        InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FindMarketSegmentID", ex.Message.ToString, ddlIndustrysvc.Text)
    '    End Try
    'End Sub

    Protected Sub ddlCompIndustry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlCompIndustry.SelectedIndexChanged
        Try


            Dim connBillingDetails As MySqlConnection = New MySqlConnection()

            connBillingDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connBillingDetails.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT marketsegmentid FROM tblindustry where industry= """ & ddlCompIndustry.Text & """"
            command1.Connection = connBillingDetails

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                txtCompMarketSegmentID.Text = dt.Rows(0)("marketsegmentid").ToString
            Else
                txtCompMarketSegmentID.Text = ""
            End If
            connBillingDetails.Close()
            connBillingDetails.Dispose()
        Catch ex As Exception
            'InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FindMarketSegmentID", ex.Message.ToString, ddlIndustrysvc.Text)
        End Try
    End Sub

    Protected Sub ddlPersIndustry_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlPersIndustry.SelectedIndexChanged
        Try


            Dim connBillingDetails As MySqlConnection = New MySqlConnection()

            connBillingDetails.ConnectionString = ConfigurationManager.ConnectionStrings("sitadataConnectionString").ConnectionString
            connBillingDetails.Open()

            Dim command1 As MySqlCommand = New MySqlCommand

            command1.CommandType = CommandType.Text
            command1.CommandText = "SELECT marketsegmentid FROM tblindustry where industry= """ & ddlPersIndustry.Text & """"
            command1.Connection = connBillingDetails

            Dim dr As MySqlDataReader = command1.ExecuteReader()
            Dim dt As New System.Data.DataTable
            dt.Load(dr)


            If dt.Rows.Count > 0 Then
                txtPersMarketSegmentID.Text = dt.Rows(0)("marketsegmentid").ToString
            Else
                txtPersMarketSegmentID.Text = ""
            End If
            connBillingDetails.Close()
            connBillingDetails.Dispose()
        Catch ex As Exception
            'InsertIntoTblWebEventLog("COMPANY - " + txtCreatedBy.Text, "FindMarketSegmentID", ex.Message.ToString, ddlIndustrysvc.Text)
        End Try
    End Sub
End Class
