Imports MySql.Data
Imports MySql.Data.MySqlClient
Imports System.Data

Partial Class Master_LogDetailsSetup
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
        'txtInvoiceMaxRecs.Enabled = False
        'txtReceiptMaxRecs.Enabled = False
        'txtCreditNoteMaxRecs.Enabled = False
        'txtJournalMaxRecs.Enabled = False
        'chkShowGridRecords.Enabled = False

        'chkServiceContract.Enabled = False

        'chkARModulePost.Enabled = False
        'rdbSvcRecOrder.Enabled = False
        'chkSvcRecord.Enabled = False
        'txtAttempts.Enabled = False
        chkCalendar.Enabled = False

        'txtSupervisorRec.Enabled = False
        'txtInvoiceEmailInterval.Enabled = False
        'chkAutoEmailModules.Enabled = False
        chkLogDetail.Enabled = False
        'ddlCurrency.Enabled = False
        'txtGST.Enabled = False
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
        'txtInvoiceMaxRecs.Enabled = True
        'txtReceiptMaxRecs.Enabled = True
        'txtCreditNoteMaxRecs.Enabled = True
        'txtJournalMaxRecs.Enabled = True
        'chkShowGridRecords.Enabled = True

        'chkServiceContract.Enabled = True
        'chkARModulePost.Enabled = True
        'rdbSvcRecOrder.Enabled = True
        'chkSvcRecord.Enabled = True
        'txtAttempts.Enabled = True
        chkCalendar.Enabled = True

        'txtSupervisorRec.Enabled = True
        'txtInvoiceEmailInterval.Enabled = True
        'chkAutoEmailModules.Enabled = True
        chkLogDetail.Enabled = True
        'ddlCurrency.Enabled = True
        'txtGST.Enabled = True
    End Sub

    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        If Not IsPostBack Then
            EnableControls()

            Dim query As String

            'query = "SELECT Currency FROM tblCurrency order by Currency"
            'PopulateDropDownList(query, "Currency", "Currency", ddlCurrency)

            'query = "Select TaxType from tbltaxtype  order by taxtype"
            'PopulateDropDownList(query, "TaxType", "TaxType", txtGST)


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

            'txtInvoiceMaxRecs.Text = dt.Rows(0)("InvoiceRecordMaxRec").ToString
            'txtReceiptMaxRecs.Text = dt.Rows(0)("ReceiptRecordMaxRec").ToString
            'txtCreditNoteMaxRecs.Text = dt.Rows(0)("CNRecordMaxRec").ToString
            'txtJournalMaxRecs.Text = dt.Rows(0)("JournalRecordMaxRec").ToString
            'txtInvoiceEmailInterval.Text = dt.Rows(0)("InvoiceEmailInterval").ToString

            'ddlCurrency.Text = dt.Rows(0)("ARCurrency").ToString
            'txtGST.Text = dt.Rows(0)("DefaultTaxCode").ToString

            'If txtGST.SelectedIndex = 0 Then
            '    Command.Parameters.AddWithValue("@DefaultTaxCode", DBNull.Value)
            'Else
            '    Command.Parameters.AddWithValue("@DefaultTaxCode", ddlCurrency.Text)
            'End If
            'GRID RECORDS ON SCREEN LOAD

            'If dt.Rows(0)("ShowInvoiceOnScreenLoad").ToString = "1" Then
            '    chkShowGridRecords.Items.FindByValue("1").Selected = True
            'Else
            '    chkShowGridRecords.Items.FindByValue("1").Selected = False
            'End If

            'If dt.Rows(0)("ShowReceiptOnScreenLoad").ToString = "1" Then
            '    chkShowGridRecords.Items.FindByValue("2").Selected = True
            'Else
            '    chkShowGridRecords.Items.FindByValue("2").Selected = False
            'End If

            'If dt.Rows(0)("ShowCNOnScreenLoad").ToString = "1" Then
            '    chkShowGridRecords.Items.FindByValue("3").Selected = True
            'Else
            '    chkShowGridRecords.Items.FindByValue("3").Selected = False
            'End If
            'If dt.Rows(0)("ShowJournalOnScreenLoad").ToString = "1" Then
            '    chkShowGridRecords.Items.FindByValue("4").Selected = True
            'Else
            '    chkShowGridRecords.Items.FindByValue("4").Selected = False
            'End If


            'POST RECORDS

            'If dt.Rows(0)("PostInvoice").ToString = "1" Then
            '    chkARModulePost.Items.FindByValue("1").Selected = True
            'Else
            '    chkARModulePost.Items.FindByValue("1").Selected = False
            'End If

            'If dt.Rows(0)("PostReceipt").ToString = "1" Then
            '    chkARModulePost.Items.FindByValue("2").Selected = True
            'Else
            '    chkARModulePost.Items.FindByValue("2").Selected = False
            'End If

            'If dt.Rows(0)("PostCN").ToString = "1" Then
            '    chkARModulePost.Items.FindByValue("3").Selected = True
            'Else
            '    chkARModulePost.Items.FindByValue("3").Selected = False
            'End If
            'If dt.Rows(0)("PostJournal").ToString = "1" Then
            '    chkARModulePost.Items.FindByValue("4").Selected = True
            'Else
            '    chkARModulePost.Items.FindByValue("4").Selected = False
            'End If


            'AUTO EMAIL

            'If dt.Rows(0)("AutoEmailInvoice").ToString = "1" Then
            '    chkAutoEmailModules.Items.FindByValue("1").Selected = True
            'Else
            '    chkAutoEmailModules.Items.FindByValue("1").Selected = False
            'End If

            'If dt.Rows(0)("AutoEmailDebitNote").ToString = "1" Then
            '    chkAutoEmailModules.Items.FindByValue("2").Selected = True
            'Else
            '    chkAutoEmailModules.Items.FindByValue("2").Selected = False
            'End If

            'If dt.Rows(0)("AutoEmailCreditNote").ToString = "1" Then
            '    chkAutoEmailModules.Items.FindByValue("3").Selected = True
            'Else
            '    chkAutoEmailModules.Items.FindByValue("3").Selected = False
            'End If


            'Creator

            If dt.Rows(0)("EnableLogforCustomer").ToString = "1" Then
                chkLogDetail.Items.FindByValue("1").Selected = True
            Else
                chkLogDetail.Items.FindByValue("1").Selected = False
            End If

            If dt.Rows(0)("EnableLogforContract").ToString = "1" Then
                chkLogDetail.Items.FindByValue("2").Selected = True
            Else
                chkLogDetail.Items.FindByValue("2").Selected = False
            End If

            If dt.Rows(0)("EnableLogforService").ToString = "1" Then
                chkLogDetail.Items.FindByValue("3").Selected = True
            Else
                chkLogDetail.Items.FindByValue("3").Selected = False
            End If

            If dt.Rows(0)("EnableLogforInvoice").ToString = "1" Then
                chkLogDetail.Items.FindByValue("4").Selected = True
            Else
                chkLogDetail.Items.FindByValue("4").Selected = False
            End If

            If dt.Rows(0)("EnableLogforReceipt").ToString = "1" Then
                chkLogDetail.Items.FindByValue("5").Selected = True
            Else
                chkLogDetail.Items.FindByValue("5").Selected = False
            End If

            If dt.Rows(0)("EnableLogforCNDN").ToString = "1" Then
                chkLogDetail.Items.FindByValue("6").Selected = True
            Else
                chkLogDetail.Items.FindByValue("6").Selected = False
            End If

            If dt.Rows(0)("EnableLogforJournal").ToString = "1" Then
                chkLogDetail.Items.FindByValue("7").Selected = True
            Else
                chkLogDetail.Items.FindByValue("7").Selected = False
            End If
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
                qry = "UPDATE tblservicerecordmastersetup SET EnableLogforCustomer = @EnableLogforCustomer, EnableLogforContract = @EnableLogforContract, EnableLogforService = @EnableLogforService, EnableLogforInvoice = @EnableLogforInvoice, EnableLogforReceipt = @EnableLogforReceipt, EnableLogforCNDN = @EnableLogforCNDN, EnableLogforJournal = @EnableLogforJournal  WHERE RcNo = 1;"

                command.CommandText = qry
                command.Parameters.Clear()


                'DOCUMENT EDITING

                If chkLogDetail.Items.FindByValue("1").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforCustomer", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforCustomer", 0)
                End If

                If chkLogDetail.Items.FindByValue("2").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforContract", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforContract", 0)
                End If

                If chkLogDetail.Items.FindByValue("3").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforService", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforService", 0)
                End If

                If chkLogDetail.Items.FindByValue("4").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforInvoice", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforInvoice", 0)
                End If

                If chkLogDetail.Items.FindByValue("5").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforReceipt", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforReceipt", 0)
                End If

                If chkLogDetail.Items.FindByValue("6").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforCNDN", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforCNDN", 0)
                End If

                If chkLogDetail.Items.FindByValue("7").Selected = True Then
                    command.Parameters.AddWithValue("@EnableLogforJournal", 1)
                Else
                    command.Parameters.AddWithValue("@EnableLogforJournal", 0)
                End If


                command.Connection = conn

                command.ExecuteNonQuery()

                ' MessageBox.Message.Alert(Page, "Record updated successfully!!!", "str")
                lblMessage.Text = "EDIT: RECORD SUCCESSFULLY UPDATED"
                lblAlert.Text = ""

            End If

            conn.Close()

            CType(Me.Master, MasterPage).EventLog_Insert(Session("UserID"), "LOGDETAIL", "MASTERSETUP", "EDIT", Convert.ToDateTime(txtCreatedOn.Text), 0, 0, 0, "", "", txtRcno.Text)

        Catch ex As Exception
            lblAlert.Text = ex.Message.ToString

        End Try
        EnableControls()

    End Sub

    Protected Sub btnQuit_Click(sender As Object, e As EventArgs) Handles btnQuit.Click
        Response.Redirect("Home.aspx")

    End Sub
End Class
